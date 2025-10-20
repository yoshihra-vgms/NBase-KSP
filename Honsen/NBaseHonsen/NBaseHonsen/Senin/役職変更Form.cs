using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseHonsen.Senin.util;
using NBaseData.DAC;
using SyncClient;
using ORMapping;
using NBaseData.DS;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using NBaseHonsen.util;

namespace NBaseHonsen.Senin
{
    public partial class 役職変更Form : Form
    {
        private TreeListViewDelegate下船_乗船休暇 treeListViewDelegate;


        public 役職変更Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {       
            treeListViewDelegate = new TreeListViewDelegate下船_乗船休暇(treeListView1);

            Search();

            InitComboBox職名();
            dateTimePicker日.Value = DateTime.Now;
        }


        private void Search()
        {
            SiCardFilter filter = new SiCardFilter();
            filter.MsVesselIDs.Add(同期Client.LOGIN_VESSEL.MsVesselID);
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船));
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船休暇));

            filter.Start = DateTime.Now;
            filter.End = DateTime.Now;

            filter.RetireFlag = 0;
           
            List<SiCard> cards = SiCard.GetRecordsByFilter(同期Client.LOGIN_USER, filter);

            treeListViewDelegate.SetRows(cards);
        }

        private void InitComboBox職名()
        {

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

        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        

        private void button役職変更_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                if (UpdateRecords())
                {
                    MessageBox.Show("役職変更が完了しました", "役職変更", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;

                    Dispose();
                }
                else
                {
                    MessageBox.Show("役職変更に失敗しました", "役職変更", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private bool ValidateFields()
        {
            if (comboBox職名.SelectedItem == null)
            {
                MessageBox.Show("職名を選択して下さい", "乗船", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dateTimePicker日.Value > DateTimeUtils.ToTo(DateTime.Now).AddSeconds(-1))
            {
                dateTimePicker日.BackColor = Color.Pink;
                MessageBox.Show(this.Text + "日に未来の日付は指定できません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker日.BackColor = Color.White;
                return false;
            }

            return true;
        }

        
        private bool UpdateRecords()
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    foreach(TreeListViewNode node in treeListView1.SelectedNodes)
                    {
                        //=============================================================
                        // 下船情報
                        //=============================================================
                        SiCard card_下船 = node.Tag as SiCard;
                        card_下船.EndDate = DateTimeUtils.ToFrom(dateTimePicker日.Value.AddDays(-1));　// 下船日
                        card_下船.Days = (DateTimeUtils.ToTo(card_下船.EndDate) - DateTimeUtils.ToFrom(card_下船.StartDate)).Days;
                        card_下船.RenewUserID = 同期Client.LOGIN_USER.MsUserID;
                        card_下船.RenewDate = DateTime.Now;

                        if (同期Client.LOGIN_VESSEL.IsPlanType(MsPlanType.PlanTypeHarfPeriod))
                        {
                            card_下船.LaborOnDisembarking = (int)SiCard.LABOR.労働;
                        }
                        else
                        {
                            card_下船.LaborOnDisembarking = (int)SiCard.LABOR.半休;
                        }

                        //=============================================================
                        // 乗船情報
                        //=============================================================
                        SiCard card_乗船 = CreateInstance_乗船(card_下船);


                        // 兼務通信長
                        if (card_下船.KenmTushincyo == 1)
                        {
                            DateTime s = card_下船.KenmTushincyoStart;
                            DateTime e = card_下船.KenmTushincyoEnd;

                            if (s < card_下船.EndDate)
                            {
                                if (e != DateTime.MinValue && e < card_乗船.StartDate)
                                {
                                    // 役職変更前に兼務通信長の開始、終了があるので何もしない
                                }
                                else
                                {
                                    // 役職変更前の兼務通信長の終了日に乗船終了日とする
                                    card_下船.KenmTushincyoEnd = card_下船.EndDate;

                                    // 役職変更後の兼務通信長の開始日は、乗船開始日とする
                                    card_乗船.KenmTushincyo = 1;
                                    card_乗船.KenmTushincyoStart = card_乗船.StartDate;
                                }
                            }
                            else
                            {
                                // 役職変更前に兼務通信長の開始日が、役職変更後の開始日以降の場合
                                // 
                                // 役職変更前の兼務通信長の開始、終了を役職変更後にセット
                                card_乗船.KenmTushincyo = 1;
                                card_乗船.KenmTushincyoStart = s;
                                card_乗船.KenmTushincyoEnd = e;

                                // 役職変更前の兼務通信長の開始、終了はクリアする
                                card_下船.KenmTushincyo = 0;
                                card_下船.KenmTushincyoStart = DateTime.MinValue;
                                card_下船.KenmTushincyoEnd = DateTime.MinValue;
                            }

                        }

                        //=============================================================
                        // 下船処理
                        //=============================================================

                        SyncTableSaver.InsertOrUpdate(card_下船, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);

                        // 本船更新情報(下船)
                        PtHonsenkoushinInfo honsenkoushinInfo = 本船更新情報.CreateInstance(card_下船, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.下船);
                        SyncTableSaver.InsertOrUpdate(honsenkoushinInfo, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);


                        //=============================================================
                        // 乗船処理
                        //=============================================================

                        SyncTableSaver.InsertOrUpdate(card_乗船, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);

                        foreach (SiLinkShokumeiCard link in card_乗船.SiLinkShokumeiCards)
                        {
                            SyncTableSaver.InsertOrUpdate(link, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                        }

                        // 本船更新情報(乗船)
                        honsenkoushinInfo = 本船更新情報.CreateInstance(card_乗船, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.乗船);
                        SyncTableSaver.InsertOrUpdate(honsenkoushinInfo, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);

                    }

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

        private SiCard CreateInstance_乗船(SiCard card_下船)
        {
            SiCard card = new SiCard();

            card.SiCardID = System.Guid.NewGuid().ToString();

            card.MsSeninID = card_下船.MsSeninID;
            card.MsVesselID = (short)同期Client.LOGIN_VESSEL.MsVesselID;
            card.MsSiShubetsuID = SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船);

            card.StartDate = card_下船.EndDate.AddDays(1); // 下船日の翌日から

            card.RenewUserID = 同期Client.LOGIN_USER.MsUserID;
            card.RenewDate = DateTime.Now;

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
            card.NavigationArea = card_下船.NavigationArea;

            card.MsVesselTypeID = card_下船.MsVesselTypeID;
            card.GrossTon = card_下船.GrossTon;
            card.OwnerName = card_下船.OwnerName;


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



        private void treeListView1_AfterSelect(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            buttonOK.Enabled = true;
        }
    }
}
