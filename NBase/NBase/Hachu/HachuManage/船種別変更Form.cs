using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Hachu.Models;
using Hachu.Utils;
using Hachu.BLC;
using NBaseCommon.Nyukyo;
using NBaseCommon.Senyouhin;
using Hachu.Reports;
using NBaseData.DS;
using System.IO;
using NBaseUtil;



namespace Hachu.HachuManage
{
    public partial class 船種別変更Form : BaseUserControl
    {
        private bool IsCreated = false;
        public delegate void ClosingEventHandler();
        public event ClosingEventHandler ClosingEvent;

        /// <summary>
        /// 対象手配依頼
        /// </summary>
        private OdThi 対象手配依頼;

        /// <summary>
        /// 対象手配依頼の品目
        /// </summary>
        private List<Item手配依頼品目> 手配品目s;

        /// <summary>
        /// 削除とマークされた品目
        /// </summary>
        private List<Item手配依頼品目> 削除手配品目s;


        /// <summary>
        /// 
        /// </summary>
        ItemTreeListView手配依頼 品目TreeList;


        /// <summary>
        /// 対象見積回答
        /// </summary>
        private OdMk 対象見積回答;

        private float panelMinHeight = 0;
        private float panelMaxHeight = 164;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowStyle">BaseUserControl.WINDOW_STYLE</param>
        /// <param name="info">手配情報</param>
        #region public 船種別変更Form(int windowStyle, ListInfo手配依頼 info)
        public 船種別変更Form(int windowStyle, ListInfo手配依頼 info)
        {
            InitializeComponent();

            this.WindowStyle = windowStyle;
            対象手配依頼 = info.info;

        }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <param name="info"></param>
        #region public 船種別変更Form(int windowStyle, OdThi info)
        public 船種別変更Form(int windowStyle, OdThi info)
        {
            InitializeComponent();

            this.WindowStyle = windowStyle;
            対象手配依頼 = info;
        }
        #endregion

        //======================================================================================
        //
        // コールバック
        //
        //======================================================================================

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 船種別変更Form_Load(object sender, EventArgs e)
        private void 船種別変更Form_Load(object sender, EventArgs e)
        {
            Formに情報をセットする();
        }
        #endregion



        /// <summary>
        /// 「変更」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button変更_Click(object sender, EventArgs e)
        private void button変更_Click(object sender, EventArgs e)
        {

            // 船種別変更ダイアログを開く
            船種別変更_依頼種別Form form = new 船種別変更_依頼種別Form(対象手配依頼.MsVesselID, 対象手配依頼.MsThiIraiSbtID, 対象手配依頼.MsThiIraiShousaiID);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            int changeVesselID = form.SelectedVesselID;
            string changeThiIraiSbtID = form.SelectedThiIraiSbtID;
            string changeThiIraiShousaiID = form.SelectedThiIraiShousaiID;

            if (対象手配依頼.MsVesselID == changeVesselID &&
                対象手配依頼.MsThiIraiSbtID == changeThiIraiSbtID &&
                対象手配依頼.MsThiIraiShousaiID == changeThiIraiShousaiID)
                return;


            対象手配依頼.MsVesselID = changeVesselID;
            対象手配依頼.MsThiIraiSbtID = changeThiIraiSbtID;
            対象手配依頼.MsThiIraiShousaiID = changeThiIraiShousaiID;


            if (更新処理() == false)
            {
                return;
            }

            MessageBox.Show("更新しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);


            ListInfo手配依頼 info = new ListInfo手配依頼();
            info.info = 対象手配依頼;
            InfoUpdating(info);

            Formに情報をセットする();
        }
        #endregion

        /// <summary>
        /// 「見積依頼先」ＤＤＬの選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox見積依頼先_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox見積依頼先_SelectedIndexChanged(object sender, EventArgs e)
        {
            MsCustomer selectedCustomer = null;
            if (comboBox見積依頼先.SelectedItem is MsCustomer)
            {
                selectedCustomer = comboBox見積依頼先.SelectedItem as MsCustomer;
                if (selectedCustomer.MsCustomerTantous.Count > 0)
                {
                    MsCustomerTantou tantou = selectedCustomer.MsCustomerTantous[0];
                    textBox担当者.Text = tantou.Name;

                    textBox担当者.AutoCompleteCustomSource.Clear();
                    foreach (MsCustomerTantou ct in selectedCustomer.MsCustomerTantous)
                    {
                        textBox担当者.AutoCompleteCustomSource.Add(ct.Name);
                    }
                }
                else
                {
                    textBox担当者.Text = "";
                }
            }
        }
        #endregion


        //======================================================================================
        //
        // 処理メソッド
        //
        //======================================================================================

        /// <summary>
        /// 
        /// </summary>
        #region private void Formに情報をセットする()
        private void Formに情報をセットする()
        {
            //=========================================
            // ＤＢから必要な情報を取得する
            //=========================================
            List<MsUser> 全users = null;
            List<MsUser> 事務所users = null;
            List<MsThiIraiStatus> statuses = null;
            List<MsCustomer> customers = null;
            List<MsCustomerTantou> customerTantous = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                // 手配依頼者
                全users = serviceClient.MsUser_GetRecords(NBaseCommon.Common.LoginUser);
                // 事務担当者
                事務所users = serviceClient.MsUser_GetRecordsByUserKbn事務所(NBaseCommon.Common.LoginUser);
                // 現状
                statuses = serviceClient.MsThiIraiStatus_GetRecords(NBaseCommon.Common.LoginUser);

                // 顧客
                customers = serviceClient.MsCustomer_GetRecords(NBaseCommon.Common.LoginUser);
                
                // 顧客担当者
                customerTantous = serviceClient.MsCustomerTantou_GetRecords(NBaseCommon.Common.LoginUser);


                if (対象手配依頼.MmFlag == (int)OdThi.MM_FLAG.なし)
                {
                    対象見積回答 = serviceClient.OdMk_GetRecordByOdThiID(NBaseCommon.Common.LoginUser, 対象手配依頼.OdThiID);
                }
            }
            foreach (MsCustomer c in customers)
            {
                foreach (MsCustomerTantou ct in customerTantous)
                {
                    if (ct.MsCustomerID == c.MsCustomerID)
                    {
                        c.MsCustomerTantous.Add(ct);
                    }
                }
            }

            //=========================================
            // 対象手配依頼の内容を画面にセットする
            //=========================================
            #region Windowタイトル
            if (対象手配依頼.Status == 対象手配依頼.OdStatusValue.Values[(int)OdThi.STATUS.手配依頼済].Value)
            {
                this.Text = NBaseCommon.Common.WindowTitle("JM040201", "手配依頼", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            }
            else
            {
                this.Text = NBaseCommon.Common.WindowTitle("JM040201", "新規手配依頼", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            }
            #endregion
            #region 入力エリア

            textBox手配依頼種.Text = 対象手配依頼.ThiIraiSbtName;
            textBox手配依頼詳細種別.Text = 対象手配依頼.ThiIraiShousaiName;
            if (対象手配依頼.TehaiIraiNo == null || 対象手配依頼.TehaiIraiNo.Length == 0)
            {
                textBox手配依頼番号.Text = "手配依頼作成時に自動採番します";
            }
            else
            {
                textBox手配依頼番号.Text = 対象手配依頼.TehaiIraiNo;
            }
            try
            {
                dateTimePicker手配依頼日.Text = 対象手配依頼.ThiIraiDate.ToShortDateString();
            }
            catch
            {
            }
            textBox船.Text = 対象手配依頼.VesselName;
            textBox場所.Text = 対象手配依頼.Basho;

            // 手配依頼者
            comboBox手配依頼者.Items.Clear();
            foreach (MsUser u in 全users)
            {
                comboBox手配依頼者.Items.Add(u);
                if (u.MsUserID == 対象手配依頼.ThiUserID)
                {
                    comboBox手配依頼者.SelectedItem = u;
                }
            }

            // 事務担当者
            MsUser dmyUser = new MsUser();
            dmyUser.MsUserID = "";
            dmyUser.LoginID = "";
            comboBox事務担当者.Items.Clear();
            // 2014.11.07 メール送信時に、事務担当者が必要になることからDMYはなしとする
            //comboBox事務担当者.Items.Add(dmyUser);
            //comboBox事務担当者.SelectedItem = dmyUser;
            foreach (MsUser u in 事務所users)
            {
                comboBox事務担当者.Items.Add(u);
                if (u.MsUserID == 対象手配依頼.JimTantouID)
                {
                    comboBox事務担当者.SelectedItem = u;
                }
            }

            // 現状
            comboBox現状.Items.Clear();
            string 現状ID = Hachu.Common.CommonDefine.MsThiIraiStatus_未対応;
            if (対象手配依頼.MsThiIraiStatusID != null && 対象手配依頼.MsThiIraiStatusID.Length > 0)
            {
                現状ID = 対象手配依頼.MsThiIraiStatusID;
            }
            foreach (MsThiIraiStatus s in statuses)
            {
                comboBox現状.Items.Add(s);
                if (s.MsThiIraiStatusID == 現状ID)
                {
                    comboBox現状.SelectedItem = s;
                }
            }

            textBox手配内容.Text = 対象手配依頼.Naiyou;
            textBox備考.Text = 対象手配依頼.Bikou;


            // 見積依頼先
            comboBox見積依頼先.Items.Clear();
            foreach (MsCustomer c in customers)
            {
                comboBox見積依頼先.Items.Add(c);
                comboBox見積依頼先.AutoCompleteCustomSource.Add(c.CustomerName);
            }

            // 担当者
            textBox担当者.Text = "";

            // 希望納期
            try
            {
                dateTimePicker希望納期.Text = 対象手配依頼.Kiboubi.ToShortDateString();
            }
            catch
            {
            }
            // 希望港
            textBox希望港.Text = 対象手配依頼.Kiboukou;

            #endregion
            #region 品目/詳細品目TreeList
            InitTreeListView();
            #endregion

            #region 画面のコンポーネントの表示/非表示
            if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
            {
                // 修繕の場合
                #region
                label手配依頼詳細種別.Visible = true;
                textBox手配依頼詳細種別.Visible = true;
                panel発注.Visible = false;
                panel燃料潤滑油.Visible = false;

                品目TreeList.Enabled = true;

                
                // 2009.11.26:aki 更新はいつでもOKとする
                //button更新.Visible = true;
                // 2010.02.16:aki 新規の場合、更新はなし
                if (対象手配依頼.RenewUserID == "")
                {
                    button変更.Visible = false;
                }
                else
                {
                    button変更.Visible = true;
                }
                #endregion
            }
            else if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                // 燃料_潤滑油の場合
                #region
                label手配依頼詳細種別.Visible = false;
                textBox手配依頼詳細種別.Visible = false;
                panel発注.Visible = false;
                panel燃料潤滑油.Visible = true;

                品目TreeList.Enabled = true;

                #endregion
            }
            else 
            {
                // 船用品の場合
                #region
                label手配依頼詳細種別.Visible = false;
                textBox手配依頼詳細種別.Visible = false;
                panel発注.Visible = false;
                panel燃料潤滑油.Visible = false;

                品目TreeList.Enabled = true;

                // 2009.11.26:aki 更新はいつでもOKとする
                //button更新.Visible = true;
                // 2010.02.16:aki 新規の場合、更新はなし
                if (対象手配依頼.RenewUserID == "")
                {
                    button変更.Visible = false;
                }
                else
                {
                    button変更.Visible = true;
                }
                #endregion
            }

            #endregion

            InitPaneSize();
        }
        #endregion

        /// <summary>
        /// 「品目/詳細品目一覧」初期化
        /// </summary>
        #region private void InitTreeListView()
        private void InitTreeListView()
        {
            if (品目TreeList != null)
                品目TreeList.Clear();

            int noColumIndex = 0;
            bool checkBoxes = false;
            bool viewHeader = false;
            object[,] columns = null;
            if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                手配品目s = Item手配依頼品目.GetRecords(対象手配依頼.MsVesselID,対象手配依頼.OdThiID);
                noColumIndex = 0;
                checkBoxes = false;
                viewHeader = false;
                if (対象手配依頼.MmFlag == (int)OdThi.MM_FLAG.なし)
                {
                    columns = new object[,] {
                                           {"No", 65, null, HorizontalAlignment.Right},
                                           {"仕様・型式 /　詳細品目", 275, null, null},
                                           {"単位", 45, null, null},
                                           {"数量", 50, null, HorizontalAlignment.Right},
                                           {"単価", 90, null, HorizontalAlignment.Right}
                                         };
                }
                else
                {
                    columns = new object[,] {
                                           {"No", 65, null, HorizontalAlignment.Right},
                                           {"仕様・型式 /　詳細品目", 275, null, null},
                                           {"単位", 45, null, null},
                                           {"数量", 50, null, HorizontalAlignment.Right},
                                         };
                }

            } 
            else
            {
                viewHeader = true;
                手配品目s = Item手配依頼品目.GetRecords(対象手配依頼.OdThiID);
                if (対象手配依頼.Status == 対象手配依頼.OdStatusValue.Values[(int)OdThi.STATUS.手配依頼済].Value)
                {
                    noColumIndex = 1;
                    checkBoxes = true;

                    columns = new object[,] {
                                            {"見積対象", 85, null, null},
                                            {"No", 35, null, HorizontalAlignment.Right},
                                            {"区分 /　仕様・型式 /　詳細品目", 275, null, null},
                                            {"単位", 45, null, null},
                                            {"在庫数", 52, null, HorizontalAlignment.Right},
                                            {"依頼数", 52, null, HorizontalAlignment.Right},
                                            {"査定数", 52, null, HorizontalAlignment.Right},
                                            {"添付", 40, null, HorizontalAlignment.Center},
                                            {"備考（品名、規格等）", 200, null, null}
                                            };
                }
                else
                {
                    noColumIndex = 0;
                    checkBoxes = false;
                    columns = new object[,] {
                                           {"No", 85, null, HorizontalAlignment.Right},
                                           {"区分 /　仕様・型式 /　詳細品目", 275, null, null},
                                           {"単位", 45, null, null},
                                           {"在庫数", 52, null, HorizontalAlignment.Right},
                                           {"依頼数", 52, null, HorizontalAlignment.Right},
                                           {"査定数", 52, null, HorizontalAlignment.Right},
                                           {"添付", 40, null, HorizontalAlignment.Center},
                                           {"備考（品名、規格等）", 200, null, null}
                                         };
                }
            }

            品目TreeList = new ItemTreeListView手配依頼(対象手配依頼.MsThiIraiSbtID, treeListView, checkBoxes);
            
            //===========================
            // 2014.02 2013年度改造 ==>
            if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                if (Hachu.Common.CommonDefine.Is新規(対象手配依頼.OdThiID))
                {
                    品目TreeList.Enum表示方式 = ItemTreeListView手配依頼.表示方式enum.Zeroを表示;
                }
                else
                {
                    品目TreeList.Enum表示方式 = ItemTreeListView手配依頼.表示方式enum.Zero以外を表示;
                }
            }
            // <==

            品目TreeList.SetColumns(noColumIndex, columns);


            品目TreeList.editable = false;//編集不可


            品目TreeList.AddNodes(viewHeader, 手配品目s);

            品目TreeList.LinkClickEvent += new ItemTreeListView.LinkClickEventHandler(View添付ファイル);

            削除手配品目s = new List<Item手配依頼品目>();

        }
        #endregion

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <returns></returns>
        #region private bool 更新処理()
        private bool 更新処理()
        {
            try
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    bool ret = serviceClient.BLC_KK船_種別変更(NBaseCommon.Common.LoginUser, 対象手配依頼);
                    if (ret == false)
                    {
                        MessageBox.Show("変更に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    対象手配依頼 = serviceClient.OdThi_GetRecord(NBaseCommon.Common.LoginUser, 対象手配依頼.OdThiID);
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("変更に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        public void View添付ファイル(string odAttachFileId)
        {
            string id = "";
            string fileName = "";
            byte[] fileData = null;

            try
            {
                Cursor = Cursors.WaitCursor;

                OdAttachFile odAttachFile = null;
                foreach (Item手配依頼品目 品目 in 手配品目s)
                {
                    foreach (OdAttachFile file in 品目.添付Files)
                    {
                        if (file.OdAttachFileID == odAttachFileId)
                        {
                            odAttachFile = file;
                            break;
                        }
                    }
                }
                if (odAttachFile == null)
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        // サーバから添付データを取得する
                        odAttachFile = serviceClient.OdAttachFile_GetRecord(NBaseCommon.Common.LoginUser, odAttachFileId);
                    }
                }
                if (odAttachFile == null)
                {
                    MessageBox.Show("対象ファイルを開けません：添付ファイルがみつかりません" , "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                fileName = odAttachFile.FileName;
                fileData = odAttachFile.Data;
                id = odAttachFile.OdAttachFileID;

                // ファイルの表示
                NBaseCommon.FileView.View(id, fileName, fileData);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("対象ファイルを開けません：" + Ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        //======================================================================
        // パネルの開閉
        //======================================================================
        #region
        private void InitPaneSize()
        {
            tableLayoutPanel1.RowStyles[2].Height = panelMaxHeight;
            button_OpenClose.Text = "▲";
        }

        private void button_OpenClose_Click(object sender, EventArgs e)
        {

            if (tableLayoutPanel1.RowStyles[2].Height == panelMinHeight)
            {
                tableLayoutPanel1.RowStyles[2].Height = panelMaxHeight;
                button_OpenClose.Text = "▲";
            }
            else
            {
                tableLayoutPanel1.RowStyles[2].Height = panelMinHeight;
                button_OpenClose.Text = "▼";
            }
        }
        #endregion

        #region private void ResetTreeListView()
        private void ResetTreeListView()
        {
            品目TreeList.Clear();

            int noColumIndex = 0;
            bool checkBoxes = false;
            bool viewHeader = false;
            object[,] columns = null;

            noColumIndex = 0;
            checkBoxes = false;
            viewHeader = false;
            if (対象手配依頼.MmFlag == (int)OdThi.MM_FLAG.なし)
            {
                columns = new object[,] {
                                        {"No", 65, null, HorizontalAlignment.Right},
                                        {"仕様・型式 /　詳細品目", 275, null, null},
                                        {"単位", 45, null, null},
                                        {"数量", 50, null, HorizontalAlignment.Right},
                                        {"単価", 90, null, HorizontalAlignment.Right}
                                        };
            }
            else
            {
                columns = new object[,] {
                                        {"No", 65, null, HorizontalAlignment.Right},
                                        {"仕様・型式 /　詳細品目", 275, null, null},
                                        {"単位", 45, null, null},
                                        {"数量", 50, null, HorizontalAlignment.Right},
                                        };
            }

            品目TreeList = new ItemTreeListView手配依頼(対象手配依頼.MsThiIraiSbtID, treeListView, checkBoxes);
            品目TreeList.Enabled = true;
            品目TreeList.Enum表示方式 = ItemTreeListView手配依頼.表示方式enum.Zero以外を表示;

            品目TreeList.SetColumns(noColumIndex, columns);

            品目TreeList.AddNodes(viewHeader, 手配品目s);

            品目TreeList.LinkClickEvent += new ItemTreeListView.LinkClickEventHandler(View添付ファイル);
        }
        #endregion

    }
}
