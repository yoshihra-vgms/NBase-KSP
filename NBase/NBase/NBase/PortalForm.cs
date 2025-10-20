using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using LidorSystems.IntegralUI.Lists;
using System.Collections;
using System.Diagnostics;
using System.IO;

//using NBase.Controls;
using NBaseData.DAC;
using NBaseCommon;
using Senin;
using System.Threading;
using NBaseData.DS;
using Senin.util;
using NBase.util;
using WtmModelBase;
using WTM;
using WtmData;

namespace NBase
{
    public partial class PortalForm : ExForm
    {
        private DateTime ColStartDate = DateTime.Today;


        public List<船選択Form.ListItem> 船選択_アラーム情報;
        public List<船選択Form.ListItem> 船選択_本船更新情報;

        public List<MsVessel> msVessel_list = null;
        public List<MsKanidouseiInfoShubetu> msKanidouseiInfoShubetu_list = null;
        public List<MsBasho> msBasho_list = null;
        public List<MsKichi> msKichi_list = null;
        public List<MsCargo> msCargo_list = null;
        public List<MsDjTani> msDjTani_list = null;
        public List<MsCustomer> msCustomer_list = null;

        private bool IsFirstClick = true;
        private int KomaPerVessel = 4;
        private List<MsVessel> ViewVesselList = new List<MsVessel>();

        public class DouseiInfo
        {
            public int MsVesselId { set; get; }
            public DateTime EventDate { set; get; }
            public int Koma { set; get; }
            public PtKanidouseiInfo PtKanidouseiInfo { set; get; }

            public DouseiInfo(int vesselId, DateTime eventDate, int koma, PtKanidouseiInfo ptKanidouseiInfo)
            {
                MsVesselId = vesselId;
                EventDate = eventDate;
                Koma = koma;
                PtKanidouseiInfo = ptKanidouseiInfo;
            }
        }
        private List<DouseiInfo> DouseiInfoList = null;

        private int SelectVesselPos = 0;



        private DateTime NOW = DateTime.Now; 
        private List<Image> AlarmImg = new List<Image>();



        public PortalForm()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("JM0201", "ポータル", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            SeninTableCache.instance().DacProxy = new WcfSeninDacProxy();
           
            // 簡易動静情報の初期表示は、昨日
            ColStartDate = DateTime.Today.AddDays(-1);
            dateTimePicker_動静表.Value = ColStartDate;


            ｼﾐｭﾚｰｼｮﾝToolStripMenuItem.Visible = false;

        }
        /// <summary>
        /// 画面が呼び出された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void PortalForm_Load(object sender, EventArgs e)
        private void PortalForm_Load(object sender, EventArgs e)
        {
            label_date.Text = DateTime.Today.ToString("yyyy/MM/dd dddd");


            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.動静))
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    msVessel_list = serviceClient.MsVessel_GetRecordsByKanidouseiEnabled(NBaseCommon.Common.LoginUser);
                    msKanidouseiInfoShubetu_list = serviceClient.MsKanidouseiInfoShubetu_GetRecords(NBaseCommon.Common.LoginUser);
                    msBasho_list = serviceClient.MsBasho_GetRecordsBy港(NBaseCommon.Common.LoginUser);
                    msKichi_list = serviceClient.MsKiti_GetRecords(NBaseCommon.Common.LoginUser);
                    msCargo_list = serviceClient.MsCargo_GetRecords(NBaseCommon.Common.LoginUser);
                    msDjTani_list = serviceClient.MsDjTani_GetRecords(NBaseCommon.Common.LoginUser);
                    msCustomer_list = serviceClient.MsCustomer_GetRecords(NBaseCommon.Common.LoginUser);
                }

                label_積み.BackColor = NBaseCommon.Common.ColorTumi;
                label_揚げ.BackColor = NBaseCommon.Common.ColorAge;
                label_待機.BackColor = NBaseCommon.Common.ColorTaiki;
                label_避泊.BackColor = NBaseCommon.Common.ColorHihaku;
                label_パージ.BackColor = NBaseCommon.Common.ColorPurge;
                label_その他.BackColor = NBaseCommon.Common.ColorEtc;


                // リスト項目設定の準備
                if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員) ||
                    NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注))
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員))
                        {
                            // 船員一覧
                            NBaseCommon.Common.SeninListItemList = serviceClient.MsListItem_GetRecords(NBaseCommon.Common.LoginUser, (int)MsListItem.enumKind.船員);

                            NBaseCommon.Common.SeninListItemUserList = serviceClient.BLC_GetUserListSetting(NBaseCommon.Common.LoginUser, (int)MsListItem.enumKind.船員, NBaseCommon.Common.LoginUser.MsUserID);

                            foreach (UserListItems uItem in NBaseCommon.Common.SeninListItemUserList)
                            {
                                uItem.ListItem = NBaseCommon.Common.SeninListItemList.Where(o => o.MsListItemID == uItem.MsListItemID).FirstOrDefault();
                            }
                        }

                        if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注))
                        {
                            // 発注一覧
                            NBaseCommon.Common.HachuListItemList = serviceClient.MsListItem_GetRecords(NBaseCommon.Common.LoginUser, (int)MsListItem.enumKind.発注);

                            NBaseCommon.Common.HachuListItemUserList = serviceClient.BLC_GetUserListSetting(NBaseCommon.Common.LoginUser, (int)MsListItem.enumKind.発注, NBaseCommon.Common.LoginUser.MsUserID);

                            foreach (UserListItems uItem in NBaseCommon.Common.HachuListItemUserList)
                            {
                                uItem.ListItem = NBaseCommon.Common.HachuListItemList.Where(o => o.MsListItemID == uItem.MsListItemID).FirstOrDefault();
                            }
                        }
                    }
                    if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員) &&
                        NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注))
                    {
                        settingListControl2.Width = this.Width / 2;
                    }
                    else if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員))
                    {
                        splitContainer1.Panel2Collapsed = true;
                    }
                    else
                    {
                        splitContainer1.Panel1Collapsed = true;
                    }
                }

                簡易動静_tableLayoutPanel.RowStyles[2].Height = 0;


                //Alarm画像
                for (int i = 0; i < 16; i++)
                {
                    Image wkimg = Image.FromFile(@"Resources\alarm" + i.ToString() + ".png");

                    AlarmImg.Add(wkimg);
                }
            }

            MakeCheckedList_船();

            //if (NbaseContractFunctionTableCache.instance().IsContract(NbaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunctionNo.共通))
            //{
            Init_アラーム情報();
            Init_本船更新情報();
            //}

            SetViewVesselList();

            EnableComponents();


            KanidouseiRefresh();
            IsFirstClick = false;
        }
        #endregion

        /// <summary>
        /// 権限による画面部品の表示/非表示
        /// </summary>
        #region private void EnableComponents()
        private void EnableComponents()
        {
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "ポータル", "予実管理", null))
            {
                button予実管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "ポータル", "発注管理", null))
            {
                button発注管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "ポータル", "船員管理", null))
            {
                button船員管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "ポータル", "指摘事項管理", null))
            {
                button指摘事項管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "ポータル", "動静管理", null))
            {
                button動静管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "ポータル", "文書管理", null))
            {
                buttonドキュメント管理.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "ポータル", "共通", null))
            {
                button共通.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "ポータル", "同期確認", null))
            {
                button同期確認.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "ポータル", "勤怠管理", null))
            {
                button勤怠管理.Enabled = true;

                // 設定読込
                NBaseCommon.Common.Read();
                WtmCommon.ReadConfig();

                // Wtmアクセッサ
                WtmAccessor.Instance().DacProxy = new RemoteAccessor(WtmCommon.ConnectionKey);
            }

            //==============================
            // 契約によるボタンの制御
            //==============================
            button予実管理.Visible = NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.予実);
            button発注管理.Visible = NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注);
            button船員管理.Visible = NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員);
            button指摘事項管理.Visible = NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.指摘);
            button動静管理.Visible = NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.動静);
            buttonドキュメント管理.Visible = NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.文書);
            button共通.Visible = NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.共通);
            button同期確認.Visible = (NbaseContractFunctionTableCache.instance().HonsenContractCount(NBaseCommon.Common.LoginUser) > 0);
            
            button勤怠管理.Visible = NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.勤怠);


            //if (NbaseContractFunctionTableCache.instance().IsContract(NbaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunctionNo.共通) == false)
            //{
            //    tabControl1.TabPages.RemoveAt(2);
            //    tabControl1.TabPages.RemoveAt(1);
            //}
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.動静) == false)
            {
                tabControl1.TabPages.RemoveAt(0);
                簡易動静_tableLayoutPanel.RowStyles[2].Height = 0;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "ポータル", "動静管理", null) == false)
            {
                button_new.Enabled = false;
                button_file_output.Enabled = false;
            }


            int visibleCount = 0;
            if (button予実管理.Visible) visibleCount++;
            if (button発注管理.Visible) visibleCount++;
            if (button船員管理.Visible) visibleCount++;
            if (button指摘事項管理.Visible) visibleCount++;
            if (button動静管理.Visible) visibleCount++;
            if (buttonドキュメント管理.Visible) visibleCount++;
            if (button共通.Visible) visibleCount++;
            if (button同期確認.Visible) visibleCount++;
            if (button勤怠管理.Visible) visibleCount++;


            if (visibleCount <= 5)
            {
                this.Width -= 200;
            }

        }
        #endregion

        /// <summary>
        /// 船のドロップダウンリストを作成する
        /// </summary>
        #region private void MakeCheckedList_船()
        private void MakeCheckedList_船()
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //List<MsVessel> msVessel_list = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
                List<MsVessel> msVessel_list = serviceClient.MsVessel_GetRecords(NBaseCommon.Common.LoginUser);

                List<int> seninEnabledVessels = new List<int>();
                List<int> hachuEnabledVessels = new List<int>();
                List<int> bunshoEnabledVessels = new List<int>();

                if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員))
                {
                    seninEnabledVessels = msVessel_list.Where(o => o.SeninEnabled == 1).Select(o => o.MsVesselID).ToList();
                }
                if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注))
                {
                    hachuEnabledVessels = msVessel_list.Where(o => o.HachuEnabled == 1).Select(o => o.MsVesselID).ToList();
                }
                if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.文書))
                {
                    bunshoEnabledVessels = msVessel_list.Where(o => o.DocumentEnabled == 1).Select(o => o.MsVesselID).ToList();
                }

                var enableVessels = msVessel_list.Where(o => seninEnabledVessels.Contains(o.MsVesselID) || hachuEnabledVessels.Contains(o.MsVesselID) || bunshoEnabledVessels.Contains(o.MsVesselID));

                //-----------------------------------------------------
                // アラーム情報
                //-----------------------------------------------------
                船選択_アラーム情報 = new List<船選択Form.ListItem>();

                // 検索条件の船
                //foreach (MsVessel vessel in msVessel_list)
                foreach (MsVessel vessel in enableVessels)
                {
                    船選択Form.ListItem item = new 船選択Form.ListItem(vessel.VesselName, vessel.MsVesselID, true);
                    船選択_アラーム情報.Add(item);
                }

                //-----------------------------------------------------
                // 本船更新情報
                //-----------------------------------------------------
                船選択_本船更新情報 = new List<船選択Form.ListItem>();

                // 検索条件の船
                foreach (MsVessel vessel in msVessel_list)
                {
                    if (vessel.HonsenEnabled == 0)
                        continue;
    
                    船選択Form.ListItem item = new 船選択Form.ListItem(vessel.VesselName, vessel.MsVesselID, true);
                    船選択_本船更新情報.Add(item);
                }




                //-----------------------------------------------------
                // 動静情報　m.yoshihara miho 2017/5/16
                //-----------------------------------------------------
                List<MsVessel> msVessel_list2 = serviceClient.MsVessel_GetRecordsByKanidouseiEnabled(NBaseCommon.Common.LoginUser);
                foreach (MsVessel v in msVessel_list2)
                {
                    checkedListBox船_動静表.Items.Add(v);
                    if (v.KanidouseiEnabled == 1)
                    {
                        checkedListBox船_動静表.SetItemChecked(checkedListBox船_動静表.Items.Count - 1, true);
                    }
                    else if (v.KanidouseiEnabled == 0)
                    {
                        checkedListBox船_動静表.SetItemChecked(checkedListBox船_動静表.Items.Count - 1, false);
                    }
                }

            }
        }
        #endregion


        #region メニューボタンのコールバック

        /// <summary>
        /// 「予実管理」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button予実管理_Click(object sender, EventArgs e)
        private void button予実管理_Click(object sender, EventArgs e)
        {
#if 予実含む場合
            TraceLogging("予実管理");

            this.Cursor = Cursors.WaitCursor;

            TopForm form = new TopForm();
            form.Show();

            this.Cursor = Cursors.Default;
#endif
        }
        #endregion

        /// <summary>
        /// 「発注管理」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button発注管理_Click(object sender, EventArgs e)
        private void button発注管理_Click(object sender, EventArgs e)
        {
            TraceLogging("発注管理");

            Hachu.MainForm mainForm = Hachu.MainForm.GetInstance();
            //mainForm.Show();
            //mainForm.WindowState = FormWindowState.Maximized;

            mainForm.parentForm = this;
            mainForm.Show();
            mainForm.WindowState = FormWindowState.Maximized;
        }
        #endregion

        /// <summary>
        /// 船員管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button船員管理_Click(object sender, EventArgs e)
        private void button船員管理_Click(object sender, EventArgs e)
        {
            TraceLogging("船員管理");

            SeninPortalForm form = SeninPortalForm.GetInstance();
            form.parentForm = this;
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }
        #endregion

        /// <summary>
        /// 指摘事項管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button指摘事項管理_Click(object sender, EventArgs e)
        private void button指摘事項管理_Click(object sender, EventArgs e)
        {
#if Deficiency含む場合
            TraceLogging("指摘事項管理");

            DcCommon.DB.UserData udata = DeficiencyControl.SvcManager.InitLogin(NBaseCommon.Common.LoginUser.LoginID, NBaseCommon.Common.LoginUser.Password);
            DeficiencyControl.DcGlobal.Global.LoginUser = udata;
            DeficiencyControl.Forms.PortalForm form = new DeficiencyControl.Forms.PortalForm();
            form.ShowDialog();
#endif
        }
        #endregion

        /// <summary>
        /// 動静管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button動静管理_Click(object sender, EventArgs e)
        private void button動静管理_Click(object sender, EventArgs e)
        {
            TraceLogging("動静管理");

            Dousei.動静報告一覧Form form = Dousei.動静報告一覧Form.Instance();
            //form.Show();

            form.parentForm = this;
            form.Show();
            form.WindowState = FormWindowState.Normal;
        }
        #endregion

        /// <summary>
        /// ドキュメント管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonドキュメント管理_Click(object sender, EventArgs e)
        private void buttonドキュメント管理_Click(object sender, EventArgs e)
        {
            TraceLogging("文書管理");

            Document.状況確認一覧Form form = Document.状況確認一覧Form.GetInstance();
            form.parentForm = this;
            form.Show();
            form.WindowState = FormWindowState.Normal;
        }
        #endregion

        /// <summary>
        /// 共通機能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button共通_Click(object sender, EventArgs e)
        private void button共通_Click(object sender, EventArgs e)
        {
            TraceLogging("共通");

            NBaseMaster.共通メニューForm menuForm = new NBaseMaster.共通メニューForm();
            menuForm.Show();
        }
        #endregion

        /// <summary>
        /// パスワード変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonパスワード変更_Click(object sender, EventArgs e)
        private void buttonパスワード変更_Click(object sender, EventArgs e)
        {
            Password form = new Password();
            form.ShowDialog();

        }
        #endregion

        /// <summary>
        /// Honsen同期確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button同期確認_Click(object sender, EventArgs e)
        private void button同期確認_Click(object sender, EventArgs e)
        {
            同期確認Form form = new 同期確認Form();
            form.Show();
        }
        #endregion


        #endregion


        /// <summary>
        /// 【<<】ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void linkLabel_Prev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        private void linkLabel_Prev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (c1FlexGrid1.Visible == false)
            {
                return;
            }

            SetViewVesselList();
            if (ViewVesselList.Count == 0)
            {
                MessageBox.Show("船を１つ以上選択してください。");
                return;
            }

            // －１日する
            ColStartDate = ColStartDate.AddDays(-1);
            dateTimePicker_動静表.Value = ColStartDate;

            KanidouseiRefresh();

        }
        #endregion

        /// <summary>
        /// 【>>】ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void linkLabel_Next_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        private void linkLabel_Next_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (c1FlexGrid1.Visible == false)
            {
                return;
            }

            SetViewVesselList();
            if (ViewVesselList.Count == 0)
            {
                MessageBox.Show("船を１つ以上選択してください。");
                return;
            }

            // ＋１日する
            ColStartDate = ColStartDate.AddDays(1);
            dateTimePicker_動静表.Value = ColStartDate;

            KanidouseiRefresh();
        }
        #endregion

        /// <summary>
        /// 「新規動静予定追加」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_new_Click(object sender, EventArgs e)
        private void button_new_Click(object sender, EventArgs e)
        {
            if (IsFirstClick)
            {
                MessageBox.Show("動静情報が描画されていません", "ポータル", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //動静予定Form2 form = 動静予定Form2.Instance();
            動静予定Form3 form = 動静予定Form3.Instance();
            form.Set動静予定(null);

            form.MsKanidouseiInfoShubetu_List = msKanidouseiInfoShubetu_list;
            form.MsVessel_List = msVessel_list;
            form.MsBasho_list = msBasho_list;
            form.MsKichi_list = msKichi_list;
            form.MsCargo_list = msCargo_list;
            form.MsDjTani_list = msDjTani_list;
            form.MsCustomer_list = msCustomer_list;

            if (form.ShowDialog() == DialogResult.OK)
            {
                KanidouseiRefresh();
            }
            this.WindowState = FormWindowState.Normal;
        }
        #endregion

        /// <summary>
        /// 動静表出力ボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_file_output_Click(object sender, EventArgs e)
        private void button_file_output_Click(object sender, EventArgs e)
        {
            if (IsFirstClick)
            {
                return;
            }

            OutputFile(ColStartDate);
        }
        #endregion

        /// <summary>
        /// 簡易動静情報を最新にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Refresh_button_Click(object sender, EventArgs e)
        private void Refresh_button_Click(object sender, EventArgs e)
        {
            簡易動静_tableLayoutPanel.RowStyles[2].Height = 0;

            SetViewVesselList();
            if (ViewVesselList.Count == 0)
            {
                MessageBox.Show("船を１つ以上選択してください。");
                return;
            }

            ColStartDate = dateTimePicker_動静表.Value;

            if (c1FlexGrid1.Visible)
            {
                KanidouseiRefresh();

                // 再表示時、選択を左上にし、表示位置をリセットする
                if (ViewVesselList.Count > 0)
                {
                    c1FlexGrid1.Select(1, 1);
                }
                c1FlexGrid1.TopRow = 1;
                c1FlexGrid1.LeftCol = 1;
            }
        }
        #endregion


        private void SetViewVesselList()
        {
            //------------------------------------------------------------------------------------
            //船選択チェックリストでチェックされてたものだけ表示する
            ViewVesselList = new List<MsVessel>();
            foreach (object obj in checkedListBox船_動静表.CheckedItems)
            {
                MsVessel v = (MsVessel)obj;
                ViewVesselList.Add(v);
            }
        }



        private void userControl_簡易動静_VesselClickEvent(int VesselId)
        {
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員) == false &&
                NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注) == false)
            {
                return;
            }


            List<MsSenin> seninList = null;

            MsSeninFilter seninFilter = new MsSeninFilter();
            seninFilter.MsVesselID = VesselId;
            seninFilter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船));
            seninFilter.joinSiCard = MsSeninFilter.JoinSiCard.LEFT_JOIN;


            List<OdThi> thiList = null;
            List<OdMm> mmList = null;
            List<OdMk> mkList = null;
            List<OdJry> jryList = null;
            List<OdShr> shrList = null;

            OdThiFilter thiFilter = new OdThiFilter();
            thiFilter.MsVesselID = VesselId;
            thiFilter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_未対応);
            thiFilter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_見積中);
            thiFilter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_発注済);
            thiFilter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_受領済);
            //thiFilter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_完了);
            thiFilter.JryStatus = (int)OdJry.STATUS.船受領;

            NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員))
                    {
                        seninList = serviceClient.BLC_船員検索(NBaseCommon.Common.LoginUser, seninFilter);
                    }
                    if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注))
                    {
                        thiList = serviceClient.OdThi_GetRecordsByFilter(NBaseCommon.Common.LoginUser, thiFilter);
                        mmList = serviceClient.OdMm_GetRecordsByFilter(NBaseCommon.Common.LoginUser, thiFilter);
                        mkList = serviceClient.OdMk_GetRecordsByFilter(NBaseCommon.Common.LoginUser, int.MinValue, thiFilter);
                        jryList = serviceClient.OdJry_GetRecordsByOdThiFilter(NBaseCommon.Common.LoginUser, int.MinValue, thiFilter);
                        shrList = serviceClient.OdShr_GetRecordsByOdThiFilter(NBaseCommon.Common.LoginUser, int.MinValue, thiFilter);
                    }
                }
            }, "データ取得中です...");
            progressDialog.ShowDialog();


            // 一覧表示（船員）
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員))
            {
                // 検索結果を一覧用の情報に変換
                List<SettingListRowInfo> seninRowInfoList = 船員Form.Instance().ConvertRowInfo(seninList);

                // リスト項目を一覧にセットする
                settingListControl1.SelectedUserListItemsList = NBaseCommon.Common.SeninListItemUserList.Where(o => o.Title == NBaseCommon.Common.SeninListTitle).ToList();

                // 一覧情報を一覧にセットする
                settingListControl1.RowInfoList = seninRowInfoList;
                settingListControl1.DrawList();

            }

            // 一覧表示（発注）
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注))
            {
                // 検索結果を一覧用の情報に変換
                List<SettingListRowInfo> hachuRowInfoList = new Hachu.Controllers.発注一覧情報Controller().ConvertRowInfo(true, thiList, mmList, mkList, jryList, shrList);

                // リスト項目を一覧にセットする
                settingListControl2.SelectedUserListItemsList = NBaseCommon.Common.HachuListItemUserList.Where(o => o.Title == NBaseCommon.Common.HachuListTitle).ToList();

                // 一覧情報を一覧にセットする
                settingListControl2.RowInfoList = hachuRowInfoList;
                settingListControl2.DrawList();
            }

            簡易動静_tableLayoutPanel.RowStyles[2].Height = 300;
        }


        #region 動静表出力

        string BaseFileName = "動静表";

        private void OutputFile(DateTime today)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = BaseFileName + " (*.xlsx)|*.xlsx";
            fd.FileName = BaseFileName + ".xlsx";

            if (fd.ShowDialog() == DialogResult.OK)
            {
                string message = "";
                bool outputResult = false;
                try
                {
                    byte[] excelData = null;
                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        excelData = MakeFile(today);
                    }, "動静表を出力中です...");
                    progressDialog.ShowDialog();
                    if (excelData == null)
                    {
                        MessageBox.Show("動静表の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    System.IO.FileStream filest = new System.IO.FileStream(fd.FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                    filest.Write(excelData, 0, excelData.Length);
                    filest.Close();

                    outputResult = true;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    outputResult = false;
                }
                if (outputResult == true)
                {
                    // 成功
                    message = "「" + fd.FileName + "」へ出力しました";
                    MessageBox.Show(message, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // 失敗 
                    MessageBox.Show("動静表の出力に失敗しました。\n (Err:" + message + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private byte[] MakeFile(DateTime today)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                byte[] excelData = serviceClient.BLC_Excel動静表_取得(NBaseCommon.Common.LoginUser, today);

                return excelData;
            }
        }

        #endregion

        /// <summary>
        /// SubItemを追加する
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        #region private TreeListViewSubItem AddSubItem(string text)
        private TreeListViewSubItem AddSubItem(string text)
        {
            TreeListViewSubItem subItem = new TreeListViewSubItem();
            subItem.StyleFromParent = false;
            subItem.Text = text;
            subItem.FormatStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);

            return subItem;
        }
        #endregion



        private void TraceLogging(string function)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                serviceClient.TraceLogging(NBaseCommon.Common.LoginUser, NBaseCommon.Common.LoginUser.MsUserID, NBaseCommon.Common.LoginUser.BumonID, function, NBaseCommon.Common.HostName);
            }
        }




        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsFirstClick)
            {
                var tabName = tabControl1.SelectedTab.Name;
                if (tabName == "動静情報")
                {
                    KanidouseiRefresh();

                    IsFirstClick = false;
                }
            }
        }


        private void KanidouseiRefresh()
        {
            var range = c1FlexGrid1.Selection;

            c1FlexGrid1.Visible = false;

            this.Invoke((MethodInvoker)delegate ()
            {
                描画中メッセージ_label.Refresh();
            });

            // 色設定
            #region

            if (IsFirstClick)
            {
                c1FlexGrid1.Rows.DefaultSize = 35;

                c1FlexGrid1.Styles.Add("積み");
                c1FlexGrid1.Styles.Add("揚げ");
                c1FlexGrid1.Styles.Add("待機");
                c1FlexGrid1.Styles.Add("パージ");
                c1FlexGrid1.Styles.Add("避泊");
                c1FlexGrid1.Styles.Add("その他");

                c1FlexGrid1.Styles.Add("過去");
                c1FlexGrid1.Styles.Add("選択");

                c1FlexGrid1.Styles["積み"].BackColor = NBaseCommon.Common.ColorTumi;
                c1FlexGrid1.Styles["揚げ"].BackColor = NBaseCommon.Common.ColorAge;
                c1FlexGrid1.Styles["待機"].BackColor = NBaseCommon.Common.ColorTaiki;
                c1FlexGrid1.Styles["パージ"].BackColor = NBaseCommon.Common.ColorPurge;
                c1FlexGrid1.Styles["避泊"].BackColor = NBaseCommon.Common.ColorHihaku;
                c1FlexGrid1.Styles["その他"].BackColor = NBaseCommon.Common.ColorEtc;

                //過去の色
                c1FlexGrid1.Styles["過去"].BackColor = Color.Gainsboro;

                //選択された船の色
                c1FlexGrid1.Styles["選択"].BackColor = Color.Violet;

            }
            #endregion

            // アイコン
            #region
            Image img = pictureBox_Check.Image;
            Bitmap imgbitmap = new Bitmap(img);
            Image checkdImage = (Image)(new Bitmap(imgbitmap, new Size(15, 15)));

            img = pictureBox_Ikari.Image;
            imgbitmap = new Bitmap(img);
            imgbitmap.MakeTransparent();
            Image stayImage = (Image)(new Bitmap(imgbitmap, new Size(15, 15)));

            img = pictureBox_Null.Image;
            imgbitmap = new Bitmap(img);
            Image nullImage = (Image)(new Bitmap(imgbitmap, new Size(15, 15)));
            #endregion


            // 横軸は日付
            int days = 11;

            // 船の表示期間のレコードを一括で取得する
            DateTime fromDatetime = new DateTime(ColStartDate.Year, ColStartDate.Month, ColStartDate.Day);

            DateTime toDatetime = fromDatetime.AddDays(days);
            toDatetime = toDatetime.AddSeconds(-1);

            List<PtKanidouseiInfo> ptKanidouseiInfo_list = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ptKanidouseiInfo_list = serviceClient.PtKanidouseiInfo_GetRecordByEventDate(NBaseCommon.Common.LoginUser, fromDatetime, toDatetime, -1);
            }



            System.Diagnostics.Debug.WriteLine($"  [GetWorks]:GetWorks:開始：{DateTime.Now.ToString("HH:mm:ss")}");

            //List<NBaseData.BLC.SimulationProc.DeviationAlarmInfo> AlarmInfos = new List<NBaseData.BLC.SimulationProc.DeviationAlarmInfo>();


            //string appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            //{
            //    DateTime d1 = fromDatetime.AddMonths(-1).AddDays(-2);
            //    DateTime d2 = toDatetime;


            //    foreach(MsVessel v in ViewVesselList)
            //    {
            //        AlarmInfos.AddRange(serviceClient.BLC_GetDeviationInfos(NBaseCommon.Common.LoginUser, appName, v.MsVesselID, ColStartDate, ColStartDate.AddDays(days),null));
            //    }
            //}

            System.Diagnostics.Debug.WriteLine($"  [GetWorks]:GetWorks:終了：{DateTime.Now.ToString("HH:mm:ss")}");


            c1FlexGrid1.Cols.Count = 1;
            c1FlexGrid1.Rows.Count = 1;


            c1FlexGrid1.Cols.Count = days + 1;
            for (int i = 1; i <= days; i++)
            {
                c1FlexGrid1.Cols[i].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1FlexGrid1.Cols[i].Width = 150;
                c1FlexGrid1[0, i] = ColStartDate.AddDays(i - 1).ToString("M/d(ddd)");
            }

            c1FlexGrid1.Rows[0].Height = 35;

            // 縦軸は船
            int vesselParRow = KomaPerVessel;
            c1FlexGrid1.Rows.Count = ViewVesselList.Count() * vesselParRow;
            c1FlexGrid1.Rows.Count += 1;

            int idx = 0;
            c1FlexGrid1.Cols[0].Width = 125;
            c1FlexGrid1.Cols[0].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            c1FlexGrid1[idx, 0] = "船";
            idx++;

            c1FlexGrid1.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            c1FlexGrid1.Cols[0].AllowMerging = true;


            DouseiInfoList = new List<DouseiInfo>();

            foreach (MsVessel vessel in ViewVesselList)
            {
                // 各船の最終行は、アラーム表示行
                c1FlexGrid1.Rows[idx + (vesselParRow - 1)].Height = 25;

                for (int i = 1; i <= days; i++)
                {
                    int rowIdx = idx;

                    int offset = 0;
                    if (ColStartDate.AddDays(i - 1) < DateTime.Today)
                    {
                        for (offset = 0; offset < vesselParRow; offset++)
                            c1FlexGrid1.SetCellStyle(rowIdx + offset, i, "過去");
                    }

                    DateTime eventDate = ColStartDate.AddDays(i - 1);

                    var infos = ptKanidouseiInfo_list.Where(o => o.MsVesselID == vessel.MsVesselID && o.EventDate.ToShortDateString() == eventDate.ToShortDateString());

                    offset = 0;
                    foreach (PtKanidouseiInfo p in infos)
                    {
                        DouseiInfoList.Add(new DouseiInfo(vessel.MsVesselID, eventDate, GetKoma(rowIdx + offset), p));



                        bool pictreOn = false;

                        if (p.HonsenCheckDate != DateTime.MinValue)
                        {
                            c1FlexGrid1.SetCellImage(rowIdx + offset, i, checkdImage);
                            pictreOn = true;
                        }

                        var cellStr = "";
                        if (p.BashoName.Length > 6)
                        {
                            cellStr = p.BashoName.Substring(0, 6);
                        }
                        else
                        {
                            cellStr = p.BashoName;
                        }
                        if (p.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.積み || p.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.揚げ)
                        {
                            if (p.KitiName != null && p.KitiName.Length > 0)
                            {
                                if (p.KitiName.Length > 6)
                                {
                                    cellStr += " " + p.KitiName.Substring(0, 6);
                                }
                                else
                                {
                                    cellStr += " " + p.KitiName;
                                }
                            }

                            cellStr += System.Environment.NewLine;

                            if (p.MsCargoName != null && p.MsCargoName.Length > 0)
                            {
                                if (p.MsCargoName.Length > 6)
                                {
                                    cellStr += " " + p.MsCargoName.Substring(0, 6);
                                }
                                else
                                {
                                    cellStr += " " + p.MsCargoName;
                                }
                            }
                            if (p.Qtty > 0)
                            {
                                cellStr += " " + p.Qtty.ToString(".000");
                            }
                        }
                        c1FlexGrid1[rowIdx + offset, i] = cellStr;
                        if (p.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.積み)
                        {
                            c1FlexGrid1.SetCellStyle(rowIdx + offset, i, "積み");
                        }
                        else if (p.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.揚げ)
                        {
                            c1FlexGrid1.SetCellStyle(rowIdx + offset, i, "揚げ");
                        }
                        else if (p.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.待機)
                        {
                            c1FlexGrid1.SetCellStyle(rowIdx + offset, i, "待機");

                            c1FlexGrid1.SetCellImage(rowIdx + offset, i, stayImage);
                            pictreOn = true;
                        }
                        else if (p.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.パージ)
                        {
                            c1FlexGrid1.SetCellStyle(rowIdx + offset, i, "パージ");
                        }
                        else if (p.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.避泊)
                        {
                            c1FlexGrid1.SetCellStyle(rowIdx + offset, i, "避泊");
                        }
                        else if (p.KanidouseiInfoShubetuName == MsKanidouseiInfoShubetu.その他)
                        {
                            c1FlexGrid1.SetCellStyle(rowIdx + offset, i, "その他");
                        }

                        if (pictreOn == false)
                        {
                            c1FlexGrid1.SetCellImage(rowIdx + offset, i, nullImage);
                        }
                        offset++;

                        if (offset == vesselParRow - 1)
                            break;
                    }

                    //var aInfo = AlarmInfos.Where(o => o.VesselId == vessel.MsVesselID && o.Date == eventDate).FirstOrDefault();
                    //アラーム行セット(idx + vesselParRow - 1, i, aInfo);
                }

                for (int i = 0; i < vesselParRow; i++)
                {
                    var vesselCellStr = vessel.VesselName + System.Environment.NewLine;
                    vesselCellStr += vessel.CaptainName + System.Environment.NewLine;
                    vesselCellStr += vessel.Tel + System.Environment.NewLine;
                    vesselCellStr += vessel.HpTel + System.Environment.NewLine;
                    vesselCellStr += System.Environment.NewLine;
                    vesselCellStr += System.Environment.NewLine;

                    c1FlexGrid1[idx, 0] = vesselCellStr;

                    idx++;
                }
            }
            Thread PaintThread = new Thread(
            delegate ()
            {
                try
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        c1FlexGrid1.Visible = true;
                        label_表示時刻.Text = "  " + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + " 時点のデータを表示";

                        if (range.c1 < c1FlexGrid1.Cols.Count && range.r1 < c1FlexGrid1.Rows.Count)
                            c1FlexGrid1.Select(range);

                    });
                }
                catch (InvalidOperationException ex)
                {
                }
            });

            PaintThread.Start();

        }

        //private void アラーム行セット(int row, int col, NBaseData.BLC.SimulationProc.DeviationAlarmInfo aInfo)
        //{
        //    //c1FlexGrid1.Cols[col].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;
        //    c1FlexGrid1.Rows[row].ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.Stretch;

        //    int alarm24h = 0;
        //    int alarm1w = 0;
        //    int alarm4w = 0;
        //    int alarmrest = 0;

        //    //
        //    if (aInfo != null)
        //    {
        //        if (aInfo.Alarm24H) alarm24h = 8;
        //        if (aInfo.Alarm1W) alarm1w = 4;
        //        if (aInfo.Alarm4W) alarm4w = 2;
        //        if (aInfo.AlarmRest) alarmrest = 1;
        //    }
        //    int imageno = alarm24h + alarm1w + alarm4w + alarmrest;

        //    c1FlexGrid1.SetCellImage(row, col, AlarmImg[imageno]);

        //}


        private void c1FlexGrid1_Click(object sender, EventArgs e)
        {
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "ポータル", "動静管理", null) == false)
            {
                return;
            }

            int col = c1FlexGrid1.MouseCol;

            if (col != 0)
                return;

            // クリックしたポジションから、船、日付を割り出す
            int row = c1FlexGrid1.MouseRow;
            int vesselPos = ((row - 1) / KomaPerVessel);

            var vessel = ViewVesselList[vesselPos];

            //MessageBox.Show($"クリックしたのは「{vessel.VesselName}」です");

            if (SelectVesselPos >= 0)
            {
                c1FlexGrid1.GetCellRange((SelectVesselPos * KomaPerVessel) + 1, 0).StyleNew.Border.Color = c1FlexGrid1.Styles.Fixed.Border.Color;
                c1FlexGrid1.GetCellRange((SelectVesselPos * KomaPerVessel) + 1, 0).StyleNew.Border.Style = c1FlexGrid1.Styles.Fixed.Border.Style;
            }
            SelectVesselPos = vesselPos;

            c1FlexGrid1.GetCellRange((SelectVesselPos * KomaPerVessel) + 1, 0).StyleNew.Border.Color = Color.Red;
            c1FlexGrid1.GetCellRange((SelectVesselPos * KomaPerVessel) + 1, 0).StyleNew.Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.Double;

            //VesselClickEvent(vessel.MsVesselID);

            //this.ｼﾐｭﾚｰｼｮﾝToolStripMenuItem.Visible = false; // ｼﾐｭﾚｰｼｮﾝは非表示
            System.Drawing.Point p = System.Windows.Forms.Cursor.Position;
            this.contextMenuStrip1.Show(p);
        }

        private void c1FlexGrid1_DoubleClick(object sender, EventArgs e)
        {
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "ポータル", "動静管理", null) == false)
            {
                return;
            }

            int col = c1FlexGrid1.MouseCol;

            if (col == 0)
                return;

            // クリックしたポジションから、船、日付を割り出す
            int row = c1FlexGrid1.MouseRow;
            int vesselPos = ((row - 1) / KomaPerVessel);

            int koma = GetKoma(row);

            DateTime date = ColStartDate.AddDays(col - 1);


            var vessel = ViewVesselList[vesselPos];

            //MessageBox.Show($"ダブルクリックしたのは「{vessel.VesselName}」の「{date.ToShortDateString()}」(コマ{koma})です");

            DouseiInfo info = DouseiInfoList.Where(o => o.MsVesselId == vessel.MsVesselID && o.EventDate.ToShortDateString() == date.ToShortDateString() && o.Koma == koma).FirstOrDefault();
            PtKanidouseiInfo kanidouseiInfo = null;
            if (info == null)
            {
                kanidouseiInfo = new PtKanidouseiInfo();

                kanidouseiInfo.MsVesselID = vessel.MsVesselID;
                kanidouseiInfo.EventDate = date;
                kanidouseiInfo.Koma = koma - 1;
            }
            else
            {
                kanidouseiInfo = info.PtKanidouseiInfo;
            }

            //動静予定Form2 form = 動静予定Form2.Instance();
            動静予定Form3 form = 動静予定Form3.Instance();
            form.Set動静予定(kanidouseiInfo);
            form.MsKanidouseiInfoShubetu_List = msKanidouseiInfoShubetu_list;
            form.MsVessel_List = msVessel_list;
            form.MsBasho_list = msBasho_list;
            form.MsKichi_list = msKichi_list;
            form.MsCargo_list = msCargo_list;
            form.MsDjTani_list = msDjTani_list;
            form.MsCustomer_list = msCustomer_list;

            if (form.ShowDialog() == DialogResult.OK)
            {
                KanidouseiRefresh();
            }
            this.WindowState = FormWindowState.Normal;
        }

        private int GetKoma(int rowNo)
        {
            int vesselPos = ((rowNo - 1) / KomaPerVessel);

            int koma = rowNo - (KomaPerVessel * vesselPos);

            return koma;
        }


        private void VesselClickEvent(int VesselId)
        {
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員) == false &&
                NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注) == false)
            {
                return;
            }


            List<MsSenin> seninList = null;

            MsSeninFilter seninFilter = new MsSeninFilter();
            seninFilter.MsVesselID = VesselId;
            seninFilter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser, MsSiShubetsu.SiShubetsu.乗船));
            seninFilter.joinSiCard = MsSeninFilter.JoinSiCard.LEFT_JOIN;


            List<OdThi> thiList = null;
            List<OdMm> mmList = null;
            List<OdMk> mkList = null;
            List<OdJry> jryList = null;
            List<OdShr> shrList = null;

            OdThiFilter thiFilter = new OdThiFilter();
            thiFilter.MsVesselID = VesselId;
            thiFilter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_未対応);
            thiFilter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_見積中);
            thiFilter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_発注済);
            thiFilter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_受領済);
            //thiFilter.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_完了);
            thiFilter.JryStatus = (int)OdJry.STATUS.船受領;

            NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員))
                    {
                        seninList = serviceClient.BLC_船員検索(NBaseCommon.Common.LoginUser, seninFilter);
                    }
                    if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注))
                    {
                        thiList = serviceClient.OdThi_GetRecordsByFilter(NBaseCommon.Common.LoginUser, thiFilter);
                        mmList = serviceClient.OdMm_GetRecordsByFilter(NBaseCommon.Common.LoginUser, thiFilter);
                        mkList = serviceClient.OdMk_GetRecordsByFilter(NBaseCommon.Common.LoginUser, int.MinValue, thiFilter);
                        jryList = serviceClient.OdJry_GetRecordsByOdThiFilter(NBaseCommon.Common.LoginUser, int.MinValue, thiFilter);
                        shrList = serviceClient.OdShr_GetRecordsByOdThiFilter(NBaseCommon.Common.LoginUser, int.MinValue, thiFilter);
                    }
                }
            }, "データ取得中です...");
            progressDialog.ShowDialog();


            // 一覧表示（船員）
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.船員))
            {
                // 検索結果を一覧用の情報に変換
                List<SettingListRowInfo> seninRowInfoList = 船員Form.Instance().ConvertRowInfo(seninList);

                // リスト項目を一覧にセットする
                settingListControl1.SelectedUserListItemsList = NBaseCommon.Common.SeninListItemUserList.Where(o => o.Title == NBaseCommon.Common.SeninListTitle).ToList();

                // 一覧情報を一覧にセットする
                settingListControl1.RowInfoList = seninRowInfoList;
                settingListControl1.DrawList();

            }

            // 一覧表示（発注）
            if (NbaseContractFunctionTableCache.instance().IsContract(NBaseCommon.Common.LoginUser, NbaseContractFunction.EnumFunction.発注))
            {
                // 検索結果を一覧用の情報に変換
                List<SettingListRowInfo> hachuRowInfoList = new Hachu.Controllers.発注一覧情報Controller().ConvertRowInfo(true, thiList, mmList, mkList, jryList, shrList);

                // リスト項目を一覧にセットする
                settingListControl2.SelectedUserListItemsList = NBaseCommon.Common.HachuListItemUserList.Where(o => o.Title == NBaseCommon.Common.HachuListTitle).ToList();

                // 一覧情報を一覧にセットする
                settingListControl2.RowInfoList = hachuRowInfoList;
                settingListControl2.DrawList();
            }


            簡易動静_tableLayoutPanel.RowStyles[2].Height = 300;

            c1FlexGrid1.TopRow = (SelectVesselPos * KomaPerVessel) + 1;
        }

        private void 詳細ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var vessel = ViewVesselList[SelectVesselPos];
            VesselClickEvent(vessel.MsVesselID);
        }

        private void ｼﾐｭﾚｰｼｮﾝToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("ｼﾐｭﾚｰｼｮﾝが押されました");


            //int days = c1FlexGrid1.Cols.Count - c1FlexGrid1.Cols.Fixed;
            //MsVessel vessel = ViewVesselList[SelectVesselPos];


            //動静シミュレーションForm frm = new 動静シミュレーションForm(this, KomaPerVessel - 1, vessel, ColStartDate, days);
            //frm.Show();
        }

        public void RefreshGrid()
        {
            KanidouseiRefresh();
        }








        private void button勤怠管理_Click(object sender, EventArgs e)
        {
            if (WtmFormController.Displayed == false)
            {
                NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        SeninTableCache.instance().DacProxy = new WcfSeninDacProxy();

                        //Vessel
                        NBaseCommon.Common.VesselList = serviceClient.MsVessel_GetRecordsBySeninEnabled(NBaseCommon.Common.LoginUser);

                        // Shokumei
                        NBaseCommon.Common.ShokumeiList = serviceClient.MsSiShokumei_GetRecords(NBaseCommon.Common.LoginUser).OrderBy(o => o.ShowOrder).ToList();



                        //// 設定読込
                        //NBaseCommon.Common.Read();
                        //WtmCommon.ReadConfig();


                        //// Wtmアクセッサ
                        //WtmAccessor.Instance().DacProxy = new RemoteAccessor(WtmCommon.ConnectionKey);


                        //ランクカテゴリ
                        WtmCommon.RankCategoryList = WtmAccessor.Instance().GetRankCategories();

                        //権限
                        WtmCommon.RoleList = WtmAccessor.Instance().GetRoles();

                        //ワークコンテンツ
                        WtmCommon.WorkContentList = WtmAccessor.Instance().GetWorkContents();

                        // 設定
                        WtmCommon.SetSetting(WtmAccessor.Instance().GetSetting());

                        WTM.Common.LoginUser = NBaseCommon.Common.LoginUser;
                    }
                }, "勤怠管理初期化中です...");
                progressDialog.ShowDialog();
            }

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                // Senin
                NBaseCommon.Common.SeninList = serviceClient.MsSenin_GetRecords(NBaseCommon.Common.LoginUser);
            }

            if (NBaseCommon.Common.VesselList.Count == 0)
            {
                MessageBox.Show("勤怠管理の対象となる船が登録されていません");
                return;
            }

            WtmFormController.Show_日表示Form();
        }
    }
}
