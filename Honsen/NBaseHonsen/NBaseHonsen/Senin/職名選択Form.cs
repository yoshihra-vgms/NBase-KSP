using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SyncClient;
using NBaseData.DAC;
using ORMapping;
using NBaseData.DS;
using NBaseUtil;
using NBaseHonsen.Senin.util;
using NBaseHonsen.util;

namespace NBaseHonsen.Senin
{
    public partial class 職名選択Form : Form
    {
        private readonly MsSenin senin;
        private string seninName_下船;
        private DateTime startDate;


        public 職名選択Form(MsSenin senin, string seninName_下船)
        {
            this.senin = senin;
            this.seninName_下船 = seninName_下船;

            InitializeComponent();
            Init();
        }

        private void Init()
        {
            InitComboBox職名();
            label1.Text = senin.Sei + " " + senin.Mei + " さんの乗船時の職名を選択して下さい";
            dateTimePicker乗船日.Value = DateTimeUtils.ToFrom(DateTime.Now);
        }


        private void InitComboBox職名()
        {
            //foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            //{
            //    checkedListBox職名.Items.Add(s);
            //}

            if (同期Client.LOGIN_VESSEL.IsPlanType(MsPlanType.PlanTypeHarfPeriod))
            {
                foreach (var s in SeninTableCache.instance().GetShokumeiList(NBaseCommon.Common.LoginUser, Shokumei.内航))
                {
                    comboBox職名.Items.Add(s);
                }
            }
            else
            {
                foreach (var s in SeninTableCache.instance().GetShokumeiList(NBaseCommon.Common.LoginUser, Shokumei.フェリー))
                {
                    comboBox職名.Items.Add(s);
                }
            }

        }

        
        private void button乗船_Click(object sender, EventArgs e)
        {
            //if (checkedListBox職名.CheckedItems.Count == 0)
            //{
            //    MessageBox.Show("職名を一つ以上選択して下さい", "乗船", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            if (comboBox職名.SelectedItem == null)
            {
                MessageBox.Show("職名を選択して下さい", "乗船", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dateTimePicker乗船日.Value > DateTimeUtils.ToTo(DateTime.Now).AddSeconds(-1))
            {
                dateTimePicker乗船日.BackColor = Color.Pink;
                MessageBox.Show("乗船日に未来の日付は指定できません", "乗船", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker乗船日.BackColor = Color.White;
                return;
            }

            if (InsertRecord())
            {
                if (seninName_下船 == null)
                {
                    MessageBox.Show("乗船登録が完了しました", "乗船", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("交代が完了しました", "交代", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                DialogResult = DialogResult.OK;

                Dispose();
            }
            else
            {
                MessageBox.Show("乗船登録に失敗しました", "乗船", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private bool InsertRecord()
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    startDate = DateTimeUtils.ToFrom(dateTimePicker乗船日.Value);

                    SiCard card_現在 = GetInstance_現在();

                    if (card_現在 != null)
                    {
                        SyncTableSaver.InsertOrUpdate(card_現在, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                    }



                    //=============================================================
                    // 乗船処理
                    //=============================================================
                    SiCard card_乗船 = CreateInstance_乗船();

                    SyncTableSaver.InsertOrUpdate(card_乗船, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);

                    foreach (SiLinkShokumeiCard link in card_乗船.SiLinkShokumeiCards)
                    {
                        SyncTableSaver.InsertOrUpdate(link, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                    }

                    card_乗船.SeninName = senin.Sei + " " + senin.Mei;



                    //=============================================================
                    // 本船更新情報
                    //=============================================================
                    PtHonsenkoushinInfo honsenkoushinInfo = null;

                    if (seninName_下船 == null)
                    {
                        honsenkoushinInfo = 本船更新情報.CreateInstance(card_乗船, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.乗船);
                    }
                    else
                    {
                        honsenkoushinInfo = 本船更新情報.CreateInstance(card_乗船, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.交代, seninName_下船);
                    }
                    SyncTableSaver.InsertOrUpdate(honsenkoushinInfo, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);




                    dbConnect.Commit();

                    return true;
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                    return false;
                }
            }
        }


        private SiCard GetInstance_現在()
        {
            SiCardFilter filter = new SiCardFilter();
            filter.MsSeninID = senin.MsSeninID;
            filter.Start = filter.End = DateTime.Now;

            foreach (MsSiShubetsu s in SeninTableCache.instance().GetMsSiShubetsuList(NBaseCommon.Common.LoginUser))
            {
                if (!SeninTableCache.instance().Is_休暇管理(NBaseCommon.Common.LoginUser, s.MsSiShubetsuID))
                {
                    filter.MsSiShubetsuIDs.Add(s.MsSiShubetsuID);
                }
            }
           
            List<SiCard> cards = SiCard.GetRecordsByFilter(同期Client.LOGIN_USER, filter);

            if (cards.Count > 0)
            {
                if (cards[0].MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.転船))
                {
                    // 「旅行日(転船)」の場合
                    if (cards[0].StartDate.ToShortDateString() == startDate.ToShortDateString())
                    {
                        // 「旅行日(転船)」の開始日が、「乗船」の開始日と一緒の場合
                        // （下船日に乗船した場合に、このようになる）
                        cards[0].StartDate = startDate.AddDays(-1);
                        cards[0].EndDate = cards[0].StartDate;
                        cards[0].Days = -1;
                    }
                    else
                    {
                        // 1日前に終了にする.
                        cards[0].EndDate = startDate.AddDays(-1);

                        // 2011.04.13:aki ＤＡＹＳを算出
                        cards[0].Days = (DateTimeUtils.ToTo(cards[0].EndDate) - DateTimeUtils.ToFrom(cards[0].StartDate)).Days;
                    }
                }
                else
                {
                    // 1日前に終了にする.
                    cards[0].EndDate = startDate.AddDays(-1);

                    // 2011.04.13:aki ＤＡＹＳを算出
                    cards[0].Days = (DateTimeUtils.ToTo(cards[0].EndDate) - DateTimeUtils.ToFrom(cards[0].StartDate)).Days;
                }
                cards[0].RenewUserID = 同期Client.LOGIN_USER.MsUserID;
                cards[0].RenewDate = DateTime.Now;

                return cards[0];
            }
            else
            {
                return null;
            }
        }

        
        private SiCard CreateInstance_乗船()
        {
            SiCard card = new SiCard();

            card.SiCardID = System.Guid.NewGuid().ToString();

            card.MsSeninID = senin.MsSeninID;
            card.MsVesselID = (short)同期Client.LOGIN_VESSEL.MsVesselID;
            card.MsSiShubetsuID = SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船);

            card.StartDate = startDate;

            card.RenewUserID = 同期Client.LOGIN_USER.MsUserID;
            card.RenewDate = DateTime.Now;

            //foreach (MsSiShokumei s in checkedListBox職名.CheckedItems)
            //{
            //    SiLinkShokumeiCard link = new SiLinkShokumeiCard();

            //    link.SiLinkShokumeiCardID = System.Guid.NewGuid().ToString();

            //    link.MsSiShokumeiID = s.MsSiShokumeiID;
            //    link.SiCardID = card.SiCardID;

            //    link.RenewUserID = 同期Client.LOGIN_USER.MsUserID;
            //    link.RenewDate = DateTime.Now;

            //    card.SiLinkShokumeiCards.Add(link);
            //}
            {
                Shokumei s = (comboBox職名.SelectedItem as Shokumei);

                SiLinkShokumeiCard link = new SiLinkShokumeiCard();

                link.SiLinkShokumeiCardID = System.Guid.NewGuid().ToString();

                link.MsSiShokumeiID = s.MsSiShokumeiID;
                link.MsSiShokumeiShousaiID = s.MsSiShokumeiShousaiID;
                link.SiCardID = card.SiCardID;

                link.RenewUserID = 同期Client.LOGIN_USER.MsUserID;
                link.RenewDate = DateTime.Now;

                card.SiLinkShokumeiCards.Add(link);
            }
            {
                List<MsSiShubetsuShousai> shousaiList = MsSiShubetsuShousai.GetRecords(NBaseCommon.Common.LoginUser);
                if (shousaiList.Any(o => o.MsVesselID == 同期Client.LOGIN_VESSEL.MsVesselID))
                {
                    card.MsSiShubetsuShousaiID = shousaiList.Where(o => o.MsVesselID == 同期Client.LOGIN_VESSEL.MsVesselID).FirstOrDefault().MsSiShubetsuShousaiID;
                }
            }

            card.VesselName = 同期Client.LOGIN_VESSEL.VesselName;
            card.CompanyName = NBaseCommon.Common.船員_自社名;
            card.MsCrewMatrixTypeID = 同期Client.LOGIN_VESSEL.MsCrewMatrixTypeID;




            if (同期Client.LOGIN_VESSEL.IsPlanType(MsPlanType.PlanTypeHarfPeriod))
            {
                card.LaborOnBoarding = (int)SiCard.LABOR.労働;
            }
            else
            {
                card.LaborOnBoarding = (int)SiCard.LABOR.半休;
            }



            return card;
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();
        }
    }
}
