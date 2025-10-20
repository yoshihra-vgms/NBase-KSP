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
    public partial class 下船_乗船休暇Form : Form
    {
        public enum Type { 下船, 乗船休暇, 転船 }
        private Type type;
        private bool Is_交代;
        private string typeStr;
        
        private TreeListViewDelegate下船_乗船休暇 treeListViewDelegate;


        public 下船_乗船休暇Form(Type type)
            : this(type, false)
        {
        }


        public 下船_乗船休暇Form(Type type, bool Is_交代)
        {
            this.type = type;
            this.Is_交代 = Is_交代;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            if (type == Type.下船)
            {
                typeStr = "下船";
            }
            else if (type == Type.乗船休暇)
            {
                typeStr = "乗船休暇";
            }
            else if (type == Type.転船)
            {
                typeStr = "転船";
            }

            this.Text = typeStr;
            label1.Text = typeStr + "する船員を選択して下さい";
            label2.Text = typeStr + "日";
            buttonOK.Text = typeStr;
         
            InitFields();

            treeListViewDelegate = new TreeListViewDelegate下船_乗船休暇(treeListView1);

            Search();
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

            if (type == Type.乗船休暇)
            {
                cards = Refine_乗船休暇対象(cards);
            }

            treeListViewDelegate.SetRows(cards);
        }


        private List<SiCard> Refine_乗船休暇対象(List<SiCard> cards)
        {
            List<SiCard> result = new List<SiCard>();

            HashSet<int> 乗船休暇_SeninIds = new HashSet<int>();

            foreach (SiCard c in cards)
            {
                if (c.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船休暇))
                {
                    乗船休暇_SeninIds.Add(c.MsSeninID);
                }
            }

            foreach (SiCard c in cards)
            {
                if (c.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船休暇) || 乗船休暇_SeninIds.Contains(c.MsSeninID))
                {
                    continue;
                }

                result.Add(c);
            }

            return result;
        }


        private void InitFields()
        {
            dateTimePicker日.Value = DateTime.Now;
        }

        
        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        

        private void button下船_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                if (UpdateRecords())
                {
                    MessageBox.Show(typeStr + "処理が完了しました", typeStr, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;

                    Dispose();

                    if (Is_交代)
                    {
                        乗船Form 乗船Form = new 乗船Form(((SiCard)treeListView1.SelectedNode.Tag).SeninName);
                        乗船Form.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show(typeStr + "処理に失敗しました", typeStr, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private bool ValidateFields()
        {
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
                        SiCard c = node.Tag as SiCard;

                        SiCard hCard = new SiCard();
                        hCard.SiCardID = System.Guid.NewGuid().ToString();
                        hCard.MsSeninID = c.MsSeninID;
                        hCard.StartDate = DateTimeUtils.ToFrom(dateTimePicker日.Value);
                        hCard.MsSiShubetsuID = 0;

                        if (type == Type.下船)
                        {
                            // 「乗船」の SiCard を UPDATE する.
                            c.EndDate = DateTimeUtils.ToFrom(dateTimePicker日.Value);　// 下船日
                            c.Days = (DateTimeUtils.ToTo(c.EndDate) - DateTimeUtils.ToFrom(c.StartDate)).Days;
                            c.RenewUserID = 同期Client.LOGIN_USER.MsUserID;
                            c.RenewDate = DateTime.Now;
                            if (同期Client.LOGIN_VESSEL.IsPlanType(MsPlanType.PlanTypeHarfPeriod))
                            {
                                c.LaborOnDisembarking = (int)SiCard.LABOR.労働;
                            }
                            else
                            {
                                c.LaborOnDisembarking = (int)SiCard.LABOR.半休;
                            }

                            SyncTableSaver.InsertOrUpdate(c, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);



                            hCard.StartDate = hCard.StartDate.AddDays(1); // 休暇開始日

                            MsSenin senin = MsSenin.GetRecord(dbConnect, 同期Client.LOGIN_USER, hCard.MsSeninID);
                            hCard.MsSiShubetsuID = SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.有給休暇);

                            if (hCard.MsSiShubetsuID == 0)
                            {

                                MessageBox.Show(typeStr + "船員の所属データ未整備のため、下船後の休暇レコードを作成できませんでした", typeStr, MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }

                            if (!Is_交代)
                            {
                                PtHonsenkoushinInfo honsenkoushinInfo = 本船更新情報.CreateInstance(c, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.下船);

                                SyncTableSaver.InsertOrUpdate(honsenkoushinInfo, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                            }
                        }
                        else if (type == Type.乗船休暇)
                        {
                            hCard.MsSiShubetsuID = SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船休暇);
                            hCard.MsVesselID = (short)同期Client.LOGIN_VESSEL.MsVesselID;
                            hCard.EndDate = DateTimeUtils.ToTo(hCard.StartDate).AddSeconds(-1);

                            foreach (SiLinkShokumeiCard l in c.SiLinkShokumeiCards)
                            {
                                SiLinkShokumeiCard link = new SiLinkShokumeiCard();
                                link.SiLinkShokumeiCardID = System.Guid.NewGuid().ToString();
                                link.SiCardID = hCard.SiCardID;
                                link.MsSiShokumeiID = l.MsSiShokumeiID;

                                hCard.SiLinkShokumeiCards.Add(link);
                            }
                        }
                        else if (type == Type.転船)
                        {
                            // 「乗船」の SiCard を UPDATE する.
                            // 下船日を選択日とする
                            c.EndDate = DateTimeUtils.ToFrom(dateTimePicker日.Value);　// 下船日
                            c.Days = (DateTimeUtils.ToTo(c.EndDate) - DateTimeUtils.ToFrom(c.StartDate)).Days;
                            c.RenewUserID = 同期Client.LOGIN_USER.MsUserID;
                            c.RenewDate = DateTime.Now;

                            if (同期Client.LOGIN_VESSEL.IsPlanType(MsPlanType.PlanTypeHarfPeriod))
                            {
                                c.LaborOnDisembarking = (int)SiCard.LABOR.労働;
                            }
                            else
                            {
                                c.LaborOnDisembarking = (int)SiCard.LABOR.半休;
                            }

                            SyncTableSaver.InsertOrUpdate(c, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);

                            // 本船更新情報(下船)
                            PtHonsenkoushinInfo honsenkoushinInfo = 本船更新情報.CreateInstance(c, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.下船);
                            SyncTableSaver.InsertOrUpdate(honsenkoushinInfo, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);


                            // 下船日の翌日から「旅行日(転船)」とする
                            hCard.StartDate = hCard.StartDate.AddDays(1); // 旅行日(転船)開始日
                            hCard.MsSiShubetsuID = SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.転船);

                        }

                        hCard.RenewUserID = 同期Client.LOGIN_USER.MsUserID;
                        hCard.RenewDate = DateTime.Now;


                        if (hCard.MsSiShubetsuID != 0)
                        {
                            // 「有給休暇」または「乗船休暇」または「旅行日(転船)」の SiCard を INSERT する.
                            SyncTableSaver.InsertOrUpdate(hCard, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);

                            foreach (SiLinkShokumeiCard l in hCard.SiLinkShokumeiCards)
                            {
                                l.RenewUserID = 同期Client.LOGIN_USER.MsUserID;
                                l.RenewDate = DateTime.Now;

                                SyncTableSaver.InsertOrUpdate(l, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                            }
                        }
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

        
        private void treeListView1_AfterSelect(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            buttonOK.Enabled = true;
        }
    }
}
