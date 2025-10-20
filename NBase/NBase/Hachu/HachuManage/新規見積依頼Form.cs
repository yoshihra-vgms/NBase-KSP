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
using Hachu.Reports;
using NBaseCommon.Nyukyo;
using NBaseCommon.Senyouhin;
using System.IO;
using NBaseUtil;


namespace Hachu.HachuManage
{
    public partial class 新規見積依頼Form : BaseUserControl
    {
        private bool IsCreated = false;
        public delegate void ClosingEventHandler();
        public event ClosingEventHandler ClosingEvent;

        /// <summary>
        /// 対象手配依頼
        /// </summary>
        private OdThi 対象手配依頼;
        
        /// <summary>
        /// 対象見積依頼
        /// </summary>
        private OdMm 対象見積依頼;

        /// <summary>
        /// 対象見積依頼
        /// </summary>
        private OdMk 対象見積回答;

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
        /// 見積依頼先
        /// </summary>
        List<MsCustomer> 見積依頼先s = null;

        //private float panelMinHeight = 36;
        //private float panelMaxHeight = 400 + 6;
        private float panelMinHeight = 0;
        private float panelMaxHeight = 323;

        /// <summary>
        /// 呼び出し元の区別　2021/08/04
        /// </summary>
        private bool 呼び出し元_依頼種別Form = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowStyle">BaseUserControl.WINDOW_STYLE</param>
        /// <param name="info">見積もり情報</param>
        /// <param name="b">呼び出し元が依頼種別ormならtrue 2021/08/06 m.yoshihara 追加</param>
        #region public 新規見積依頼Form(int windowStyle, ListInfoListInfo見積依頼 info, bool b)
        public 新規見積依頼Form(int windowStyle, ListInfo見積依頼 info, bool b) 
        {
            InitializeComponent();

            this.WindowStyle = windowStyle;
            対象手配依頼 = info.parent;
            対象見積依頼 = info.info;
            対象見積回答 = info.child;

            singleLineCombo見積依頼先.selected += new NBaseUtil.SingleLineCombo.SelectedEventHandler(comboBox見積依頼先_SelectedIndexChanged);

            呼び出し元_依頼種別Form = b;
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
        #region private void 新規見積依頼Form_Load(object sender, EventArgs e)
        private void 新規見積依頼Form_Load(object sender, EventArgs e)
        {
            Formに情報をセットする();
        }
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button閉じる_Click(object sender, EventArgs e)
        private void button閉じる_Click(object sender, EventArgs e)
        {
            //// Formを閉じる
            //BaseFormClosing();
            //Close();
            if (this.ClosingEvent != null)
            {
                ListInfo見積依頼 info = null;
                if (IsCreated)
                {
                    info = new ListInfo見積依頼();
                    info.parent = 対象手配依頼;
                    info.info = 対象見積依頼;
                    info.child = 対象見積回答;
                    InfoUpdating(info);
                }
                //InfoUpdating(info);

                ClosingEvent();
            }
        }
        #endregion

        /// <summary>
        /// 「見積依頼作成」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button見積依頼作成_Click(object sender, EventArgs e)
        private void button見積依頼作成_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("見積依頼を作成します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (見積依頼作成処理() == false)
            {
                return;
            }
            MessageBox.Show("見積依頼を作成しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //ListInfo見積依頼 info = new ListInfo見積依頼();
            //info.parent = 対象手配依頼;
            //info.info = 対象見積依頼;
            //info.child = 対象見積回答;
            //InfoUpdating(info);
            IsCreated = true;

            // 画面の入力を不可に
            #region
            comboBox手配依頼者.Enabled = false;
            dateTimePicker手配依頼日.Enabled = false;
            textBox場所.ReadOnly = true;
            comboBox事務担当者.Enabled = false;
            comboBox現状.Enabled = false;
            if (panel燃料潤滑油.Visible == true)
            {
                dateTimePicker希望納期.Enabled = false;
                textBox希望港.ReadOnly = true;
            }
            textBox手配内容.ReadOnly = true;
            textBox備考.ReadOnly = true;
            comboBox支払条件.Enabled = false;
            textBox送り先.ReadOnly = true;
            if (panel入渠.Visible == true)
            {
                comboBox入渠科目.Enabled = false;
                textBox送り先.ReadOnly = true;
            }
            //comboBox見積依頼先.Enabled = false;
            singleLineCombo見積依頼先.Enabled = false;
            comboBox担当者.Enabled = false;
            textBox見積回答期限.ReadOnly = true;
            textBoxメールアドレス.ReadOnly = true;
            textBoxメール件名.ReadOnly = true;

            button品目編集.Enabled = false;
            treeListView.Enabled = false;

            //「見積依頼作成」ボタンを使用不可に
            //「見積依頼書出力」ボタンを使用可に
            button見積依頼作成.Enabled = false;
            button見積依頼書出力.Enabled = true;

            // 読込、出力ボタン
            if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
            {
                if (対象手配依頼.MsThiIraiShousaiID != NBaseCommon.Common.MsThiIraiShousai_小修理ID)
                {
                    button読込.Enabled = false;
                    button出力.Enabled = false;
                }
            }
            else if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
            {
                button読込.Enabled = false;
                button出力.Enabled = true;
            }

            #endregion


            //呼び出し元により、動作をわける 2021/08/04 m.yoshihara
            //Formに情報をセットする(); コメントアウト 2021/08/04
            if (呼び出し元_依頼種別Form == false)
            {//リストクリックから

                Formに情報をセットする();
            }
            else
            {//依頼種別Formから

                button閉じる_Click(sender, e);
            }
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
            //if (comboBox見積依頼先.SelectedItem is MsCustomer)
            if (singleLineCombo見積依頼先.SelectedItem is MsCustomer)
            {
                //selectedCustomer = comboBox見積依頼先.SelectedItem as MsCustomer;
                selectedCustomer = singleLineCombo見積依頼先.SelectedItem as MsCustomer;
                if (selectedCustomer.MsCustomerTantous.Count > 0)
                {
                    comboBox担当者.AutoCompleteCustomSource.Clear();
                    comboBox担当者.Items.Clear();
                    foreach (MsCustomerTantou ct in selectedCustomer.MsCustomerTantous)
                    {
                        comboBox担当者.AutoCompleteCustomSource.Add(ct.Name);
                        comboBox担当者.Items.Add(ct.Name);
                    }

                    MsCustomerTantou tantou = selectedCustomer.MsCustomerTantous[0];
                    comboBox担当者.Text = tantou.Name;
                    textBoxメールアドレス.Text = tantou.MailAddress;
                }
                else
                {
                    comboBox担当者.Text = "";
                    textBoxメールアドレス.Text = "";
                }
            }
        }
        #endregion

        /// <summary>
        /// 「担当者」ＤＤＬの選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox担当者_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox担当者_SelectedIndexChanged(object sender, EventArgs e)
        {
            setMailAddress();
        }
        #endregion

        /// <summary>
        /// 「担当者」入力時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox担当者_TextChanged(object sender, EventArgs e)
        private void comboBox担当者_TextChanged(object sender, EventArgs e)
        {
            setMailAddress();
        }
        #endregion

        /// <summary>
        /// 「担当者」DDLからフォーカスが離れた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox担当者_Leave(object sender, EventArgs e)
        private void comboBox担当者_Leave(object sender, EventArgs e)
        {
            setMailAddress();
        }
        #endregion

        /// <summary>
        /// 「メールアドレス」入力時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBoxメールアドレス_TextChanged(object sender, EventArgs e)
        private void textBoxメールアドレス_TextChanged(object sender, EventArgs e)
        {
            string adderss = textBoxメールアドレス.Text;
            if (adderss.Length > 0)
            {
                label必須_件名.Visible = true;
            }
            else
            {
                label必須_件名.Visible = false;
            }
        }
        #endregion

        /// <summary>
        /// 「見積依頼書出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button見積依頼書出力_Click(object sender, EventArgs e)
        private void button見積依頼書出力_Click(object sender, EventArgs e)
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            //見積り依頼書 依頼書 = new 見積り依頼書();
            //依頼書.Output(対象見積回答.OdMkID);
            KK発注帳票出力.見積依頼書Output(対象見積回答.OdMkID);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }
        #endregion

        /// <summary>
        /// 「品目編集」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button品目編集_Click(object sender, EventArgs e)
        private void button品目編集_Click(object sender, EventArgs e)
        {
            // 処理前の位置を保持する
            Point orgPoint = treeListView.GetScrollPos();

            OdThiItem odThiItem = new OdThiItem();
            List<OdAttachFile> odAttachFiles = new List<OdAttachFile>();
            Form form = new 品目編集Form((int)品目編集Form.EDIT_MODE.新規, 対象手配依頼.MsThiIraiSbtID, 対象手配依頼.MsVesselID, ref odThiItem, ref odAttachFiles);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            Item手配依頼品目 追加品目 = new Item手配依頼品目();
            追加品目.品目 = odThiItem;
            foreach (OdThiShousaiItem shousaiItem in odThiItem.OdThiShousaiItems)
            {
                追加品目.詳細品目s.Add(shousaiItem);
            }
            追加品目.添付Files = odAttachFiles;

            //手配品目s.Add(追加品目);
            //品目TreeList.AddNodes(追加品目);
            // 2009.09.15 ヘッダ対応　↑　コメント
            // 2009.09.15 ヘッダ対応　↓　コード置き換え
            int insertPos = 0;
            bool sameHeader = false;
            foreach (Item手配依頼品目 品目 in 手配品目s)
            {
                if (品目.品目.Header == 追加品目.品目.Header)
                {
                    sameHeader = true;
                }
                else if (sameHeader)
                {
                    break;
                }
                insertPos++;
            }
            if (insertPos >= 手配品目s.Count)
            {
                手配品目s.Add(追加品目);
            }
            else
            {
                手配品目s.Insert(insertPos, 追加品目);
            }
            品目TreeList.NodesClear();
            品目TreeList.AddNodes(true, 手配品目s);

            // 処理前の位置をセットする
            treeListView.SetScrollPos(orgPoint);
        }
        #endregion

        /// <summary>
        /// 「品目/詳細品目一覧」ダブルクリック
        /// ・品目 または 詳細品目の変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void treeListView_DoubleClick(object sender, EventArgs e)
        private void treeListView_DoubleClick(object sender, EventArgs e)
        {
        }

        private void treeListView_Click(object sender, EventArgs e)
        {
            if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                return;

            // 処理前の位置を保持する
            Point orgPoint = treeListView.GetScrollPos();

            品目TreeList.DoubleClick(対象手配依頼, ref 手配品目s, ref 削除手配品目s);

            // 処理前の位置をセットする
            treeListView.SetScrollPos(orgPoint);
        }
        #endregion

        /// <summary>
        /// 「ドックオーダー読込」ボタンクリック
        /// 「船用品注文書読込」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button読込_Click(object sender, EventArgs e)
        private void button読込_Click(object sender, EventArgs e)
        {
            if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
            {
                #region ドックオーダー読込
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Filter = "ドックオーダーファイル(*.xlsx)|*.xlsx";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.FileName = "";

                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                // ドックオーダ読込
                try
                {
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    DockOrderReader reader = new DockOrderReader(openFileDialog1.FileName,
                                                                 new WCFDockOrderDacProxy());

                    List<OdThiItem> odThiItems = reader.Read();
                    List<Item手配依頼品目> 入渠品目s = Item手配依頼品目.ConvertRecords(対象手配依頼.MsVesselID, odThiItems);

                    手配品目s.AddRange(入渠品目s);

                    品目TreeList.NodesClear();
                    品目TreeList.AddNodes(true, 手配品目s);
                }
                catch (InvalidFormatException ex)
                {
                    MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                }
                #endregion
            }
            else if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
            {
                #region 船用品注文書読込
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Filter = "船用品注文書ファイル(*.xlsx)|*.xlsx";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.FileName = "";

                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                // 船用品注文書読込
                try
                {
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    VesselItemReader reader = new VesselItemReader(openFileDialog1.FileName,
                                                                 new WCFVesselItemDacProxy(),
                                                                 対象手配依頼.MsVesselID);

                    List<OdThiItem> odThiItems = reader.Read();
                    List<Item手配依頼品目> 品目s = Item手配依頼品目.ConvertRecords船用品(対象手配依頼.MsVesselID, odThiItems);

                    手配品目s.AddRange(品目s);

                    品目TreeList.NodesClear();
                    品目TreeList.AddNodes(true, 手配品目s);
                }
                catch (InvalidFormatException ex)
                {
                    MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                }
                #endregion
            }
        }
        #endregion

        /// <summary>
        /// 「ドックオーダー出力」ボタンクリック
        /// 「船用品注文書出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button出力_Click(object sender, EventArgs e)
        private void button出力_Click(object sender, EventArgs e)
        {
            if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
            {
                #region ドックオーダー出力
                入渠種別Form form = new 入渠種別Form();
                if (form.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                MsThiIraiShousai 検査種類 = form.SelectedThiIraiShousai;

                saveFileDialog1.CreatePrompt = true;       //新規作成確認
                saveFileDialog1.OverwritePrompt = true;    //上書き確認
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.Filter = "ドックオーダーファイル(*.xlsx)|*.xlsx";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.FileName = "ドックオーダー[" + 検査種類.ThiIraiShousaiName + "].xlsx";

                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                try
                {
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    DockOrderWriter writer = new DockOrderWriter(saveFileDialog1.FileName,
                                                                 new WCFDockOrderDacProxy());
                    writer.Write(対象手配依頼.MsVesselID, 検査種類.MsThiIraiShousaiID);

                    string message = "「" + saveFileDialog1.FileName + "」へ出力しました";
                    MessageBox.Show(message, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (NoDataException ex)
                {
                    MessageBox.Show(検査種類.ThiIraiShousaiName + "データがありません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ドックオーダーの出力に失敗しました。\n (Err:" + ex.Message + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                }
                #endregion
            }
            else if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
            {
                #region 船用品注文書出力
                saveFileDialog1.CreatePrompt = true;       //新規作成確認
                saveFileDialog1.OverwritePrompt = true;    //上書き確認
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.Filter = "船用品注文書ファイル(*.xlsx)|*.xlsx";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.FileName = "船用品注文書.xlsx";

                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    byte[] result = null;
                
                    //2013/12/18 追加 m.y
                    //サーバーエラー時のフラグ
                    bool serverError = false;
                
                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            //--------------------------------
                            //2013/12/18 コメントアウト m.y
                            //result = serviceClient.BLC_Excel_船用品注文書_取得(NBaseCommon.Common.LoginUser, 対象手配依頼.OdThiID);
                            //--------------------------------
                            try
                            {
                                result = serviceClient.BLC_Excel_船用品注文書_取得(NBaseCommon.Common.LoginUser, 対象手配依頼.OdThiID);
                            }
                            catch (Exception exp)
                            {
                                //MessageBox.Show(exp.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //serverError = true;//ここでreturnしても関数から抜け出せないのでフラグを用いる
                            }
                            //--------------------------------
                        }
                    }, "船用品注文書を出力中です...");
                    progressDialog.ShowDialog();

                    //--------------------------------
                    //2013/12/18 追加 m.y 
                    if (serverError == true)
                        return;
                    //--------------------------------

                    if (result == null)
                    {
                        MessageBox.Show("船用品注文書の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = System.Windows.Forms.Cursors.Default;
                        return;
                    }

                    System.IO.FileStream filest = new System.IO.FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    filest.Write(result, 0, result.Length);
                    filest.Close();

                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("船用品注文書の出力に失敗しました。\n (Err:" + ex.Message + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                }
                #endregion
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
            List<MsShrJouken> shrJoukens = null;
            List<MsNyukyoKamoku> nyukyoKamokus = null;
            見積依頼先s = null;
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
                見積依頼先s = serviceClient.MsCustomer_GetRecords(NBaseCommon.Common.LoginUser);
                // 顧客担当者
                customerTantous = serviceClient.MsCustomerTantou_GetRecords(NBaseCommon.Common.LoginUser);
                // 支払条件
                shrJoukens = serviceClient.MsShrJouken_GetRecords(NBaseCommon.Common.LoginUser);
                // 入渠科目
                nyukyoKamokus = serviceClient.MsNyukyoKamoku_GetRecords(NBaseCommon.Common.LoginUser);
            }
            foreach (MsCustomer c in 見積依頼先s)
            {
                foreach (MsCustomerTantou ct in customerTantous)
                {
                    if (ct.MsCustomerID == c.MsCustomerID)
                    {
                        c.MsCustomerTantous.Add(ct);
                    }
                }
            }


            this.Text = NBaseCommon.Common.WindowTitle("JM040301", "新規見積依頼", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            //=========================================
            // 手配依頼の内容を初期化
            //=========================================
            textBox手配依頼種.Text = 対象手配依頼.ThiIraiSbtName;
            textBox手配依頼詳細種別.Text = 対象手配依頼.ThiIraiShousaiName;
            textBox見積依頼番号.Text = "見積依頼作成時に自動採番します";
            try
            {
                dateTimePicker手配依頼日.Text = 対象手配依頼.ThiIraiDate.ToShortDateString();
            }
            catch
            {
            }
            textBox船.Text = 対象手配依頼.VesselName;
            textBox場所.Text = "";

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

            // 状況
            comboBox現状.Items.Clear();
            string 現状ID = Hachu.Common.CommonDefine.MsThiIraiStatus_見積中;
            foreach (MsThiIraiStatus s in statuses)
            {
                comboBox現状.Items.Add(s);
                if (s.MsThiIraiStatusID == 現状ID)
                {
                    comboBox現状.SelectedItem = s;
                }
            }
            textBox手配内容.Text = "";
            textBox備考.Text = "";


            //=========================================
            // 見積依頼の内容を初期化
            //=========================================
            // 入渠科目
            comboBox入渠科目.Items.Clear();
            foreach (MsNyukyoKamoku nk in nyukyoKamokus)
            {
                comboBox入渠科目.Items.Add(nk);
                if (nk.MsNyukyoKamokuID == 対象見積依頼.MsNyukyoKamokuID)
                {
                    comboBox入渠科目.SelectedItem = nk;
                }
            }

            // 支払条件
            comboBox支払条件.Items.Clear();
            foreach (MsShrJouken sj in shrJoukens)
            {
                comboBox支払条件.Items.Add(sj);
                if (sj.MsShrJoukenID == 対象見積依頼.MsShrJoukenID)
                {
                    comboBox支払条件.SelectedItem = sj;
                }
            }
            // 送り先
            textBox送り先.Text = 対象見積依頼.Okurisaki;

            //=========================================
            // 見積回答の内容を初期化
            //=========================================
            // 見積依頼先
            //comboBox見積依頼先.Items.Clear();
            singleLineCombo見積依頼先.Items.Clear();
            foreach (MsCustomer c in 見積依頼先s)
            {
                // 2013.08.07 : 取引先のみセットする
                //// 2013.03.04 : 曖昧検索ができる様に自前のコンポーネントを利用する
                ////comboBox見積依頼先.Items.Add(c);
                ////comboBox見積依頼先.AutoCompleteCustomSource.Add(c.CustomerName);
                //singleLineCombo見積依頼先.Items.Add(c);
                //singleLineCombo見積依頼先.AutoCompleteCustomSource.Add(c.CustomerName);
                //if (c.Shubetsu == (int)MsCustomer.種別.取引先)
                if (c.Is取引先())
                {
                    singleLineCombo見積依頼先.Items.Add(c);
                    singleLineCombo見積依頼先.AutoCompleteCustomSource.Add(c.CustomerName);
                }
            }
            // 担当者
            comboBox担当者.Text = "";
            // メールアドレス
            textBoxメールアドレス.Text = "";
            // 見積回答期限
            textBox見積回答期限.Text = "";



            // 画面のコンポーネントの表示/非表示
            if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
            {
                label手配依頼詳細種別.Visible = true;
                textBox手配依頼詳細種別.Visible = true;
                panel燃料潤滑油.Visible = false;
                panel希望日.Visible = true;
                if (対象手配依頼.MsThiIraiShousaiID == NBaseCommon.Common.MsThiIraiShousai_小修理ID)
                {
                    panel入渠.Visible = false;
                    button読込.Visible = false;
                    button出力.Visible = false;
                }
                else
                {
                    panel入渠.Visible = true;
                    button読込.Visible = true;
                    button出力.Visible = true;

                    // 2012.02 -->
                    button読込.Text = "ﾄﾞｯｸｵｰﾀﾞｰ読込";
                    button出力.Text = "ﾄﾞｯｸｵｰﾀﾞｰ出力";

                    button読込.Width = 92;
                    button出力.Width = 92;
                    // <-- 2012.02 
                }
            }
            else if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                label手配依頼詳細種別.Visible = false;
                textBox手配依頼詳細種別.Visible = false;
                panel燃料潤滑油.Visible = true;
                panel入渠.Visible = false;
                panel希望日.Visible = false;
                button読込.Visible = false;
                button出力.Visible = false;

                //===========================
                // 2014.1 [2013年度改造]
                button品目編集.Enabled = false;
            }
            else
            {
                label手配依頼詳細種別.Visible = false;
                textBox手配依頼詳細種別.Visible = false;
                panel燃料潤滑油.Visible = false;
                panel入渠.Visible = false;
                panel希望日.Visible = true;

                // 2012.02 -->
                //button読込.Visible = false;
                //button出力.Visible = false;
                button読込.Text = "船用品注文書読込";
                button出力.Text = "船用品注文書出力";

                button読込.Width = 112;
                button出力.Width = 112;

                button読込.Visible = true;
                button出力.Visible = true;

                button出力.Enabled = false;
                // <-- 2012.02 
            }

            InitTreeListView();

            品目TreeList.Enabled = true;

            button見積依頼書出力.Enabled = false;

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
            bool viewHeader = false;
            object[,] columns = null;
            if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                手配品目s = Item手配依頼品目.GetRecords(対象手配依頼.MsVesselID,対象手配依頼.OdThiID);
                noColumIndex = 0;
                viewHeader = false;
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
                手配品目s = Item手配依頼品目.GetRecords(対象手配依頼.OdThiID);
                viewHeader = true;
                noColumIndex = 0;
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
            品目TreeList = new ItemTreeListView手配依頼(対象手配依頼.MsThiIraiSbtID,treeListView, false);
            品目TreeList.SetColumns(noColumIndex, columns);
            品目TreeList.AddNodes(viewHeader, 手配品目s);

            品目TreeList.LinkClickEvent += new ItemTreeListView.LinkClickEventHandler(View添付ファイル);

            削除手配品目s = new List<Item手配依頼品目>();
        }
        #endregion

        /// <summary>
        /// 見積依頼作成処理
        /// </summary>
        /// <param name="見積依頼"></param>
        /// <returns></returns>
        #region private bool 見積依頼作成処理()
        private bool 見積依頼作成処理()
        {
            try
            {
                if (入力情報の取得確認() == false)
                {
                    return false;
                }

                bool ret = false;

                ret = 新規見積依頼更新処理.新規(ref 対象手配依頼, ref 対象見積依頼, ref 対象見積回答, 手配品目s);
                if (ret == false)
                {
                    MessageBox.Show("見積依頼の作成に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }


                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    string errMessage = "";
                    ret = serviceClient.BLC_見積依頼メール送信(
                                NBaseCommon.Common.LoginUser,
                                対象見積回答.OdMkID,
                                対象見積回答.MsCustomerName,
                                対象見積回答.Tantousha,
                                textBoxメール件名.Text,
                                対象見積回答.TantouMailAddress,
                                対象見積回答.MkNo,
                                対象見積回答.MkKigen,
                                対象見積回答.WebKey,
                                対象見積回答.MmDate,
                                対象見積回答.Kiboubi,
                                ref errMessage);
                    if (ret == false)
                    {
                        MessageBox.Show(errMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("見積依頼の作成に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 入力情報の取得確認
        /// </summary>
        /// <returns></returns>
        #region private bool 入力情報の取得確認()
        private bool 入力情報の取得確認()
        {
            string errMessage = "";

            //=========================================================================
            // 手配依頼の情報をチェックする
            //=========================================================================
            // 手配依頼者
            MsUser thiUser = null;
            try
            {
                thiUser = comboBox手配依頼者.SelectedItem as MsUser;
            }
            catch
            {
            }
            // 手配依頼日
            DateTime thiIraiDate = DateTime.MinValue;
            try
            {
                thiIraiDate = DateTime.Parse(dateTimePicker手配依頼日.Text);
            }
            catch
            {
                errMessage += "・手配依頼日が不正です。\n";
            }
            // 場所
            string basho = "";
            if (textBox場所.Text.Length > 0)
            {
                basho = textBox場所.Text;
                if (basho.Length > 20)
                {
                    errMessage += "・場所は２０文字までで入力してください。\n";
                }
            }
            // 事務担当者
            MsUser jimTantou = null;
            try
            {
                jimTantou = comboBox事務担当者.SelectedItem as MsUser;
            }
            catch
            {
                // 2014.11.07 メール送信時に、事務担当者が必要になることから必須とする
                errMessage += "・事務担当者を選択してください。\n";
            }
            if (jimTantou == null)
            {
                // 2014.11.07 メール送信時に、事務担当者が必要になることから必須とする
                errMessage += "・事務担当者を選択してください。\n";
            }
            
            // 現状
            MsThiIraiStatus thiIraiStatus = null;
            try
            {
                thiIraiStatus = comboBox現状.SelectedItem as MsThiIraiStatus;
            }
            catch
            {
            }
            // 手配内容
            string tehaiNaiyou = "";
            if (textBox手配内容.Text.Length == 0)
            {
                errMessage += "・手配内容を入力してください。\n";
            }
            else
            {
                //tehaiNaiyou = textBox手配内容.Text;
                tehaiNaiyou = StringUtils.Escape(textBox手配内容.Text);
                if (tehaiNaiyou.Length > 50)
                {
                    errMessage += "・手配内容は５０文字までで入力してください。\n";
                }
            }
            // 備考
            string bikou = "";
            if (textBox備考.Text.Length > 0)
            {
                //bikou = textBox備考.Text;
                bikou = StringUtils.Escape(textBox備考.Text);
                if (bikou.Length > 500)
                {
                    errMessage += "・備考は５００文字までで入力してください。\n";
                }
            }
            DateTime kiboubi = DateTime.MinValue;
            string kiboukou = "";
            if (panel燃料潤滑油.Visible == true)
            {
                // 希望納期
                try
                {
                    kiboubi = DateTime.Parse(dateTimePicker希望納期.Text);
                }
                catch
                {
                    errMessage += "・希望納期が不正です。\n";
                }
                // 希望港
                if (textBox希望港.Text.Length > 0)
                {
                    kiboukou = textBox希望港.Text;
                    if (kiboukou.Length > 30)
                    {
                        errMessage += "・希望港は３０文字までで入力してください。\n";
                    }
                }
            }
            if (panel希望日.Visible == true)
            {
                // 希望納期
                try
                {
                    kiboubi = DateTime.Parse(dateTimePicker希望日.Text);
                }
                catch
                {
                    errMessage += "・希望納期が不正です。\n";
                }
            }

            //=========================================================================
            // 見積依頼の情報をチェックする
            //=========================================================================
            // 支払条件
            MsShrJouken shrJouken = null;
            if (comboBox支払条件.SelectedItem is MsShrJouken)
            {
                shrJouken = comboBox支払条件.SelectedItem as MsShrJouken;
            }
            // 送り先
            string okurisaki = null;
            try
            {
                okurisaki = textBox送り先.Text;
            }
            catch
            {
            }
            MsNyukyoKamoku nyukyoKamoku = null;
            string naiyou = null;
            if (panel入渠.Visible == true)
            {
                if (comboBox入渠科目.SelectedItem is MsNyukyoKamoku)
                {
                    nyukyoKamoku = comboBox入渠科目.SelectedItem as MsNyukyoKamoku;
                }
                if (nyukyoKamoku == null)
                {
                    errMessage += "・入渠科目を選択してください。\n";
                }
                naiyou = textBox送り先.Text;
            }
            // 見積回答期限
            string mmKigen = textBox見積回答期限.Text;


            //=========================================================================
            // 見積回答の情報をチェックする
            //=========================================================================
            // 見積依頼先
            MsCustomer customer = null;
            //if (comboBox見積依頼先.SelectedItem is MsCustomer)
            if (singleLineCombo見積依頼先.SelectedItem is MsCustomer)
            {
                //customer = comboBox見積依頼先.SelectedItem as MsCustomer;
                customer = singleLineCombo見積依頼先.SelectedItem as MsCustomer;
            }
            else
            {
                errMessage += "・見積依頼先を選択してください。\n";
            }
            // 担当者
            string tantou = comboBox担当者.Text;
            if (tantou == null || tantou.Length == 0)
            {
                errMessage += "・担当者を入力してください。\n";
            }
            // メールアドレス
            string mailAddress = textBoxメールアドレス.Text;
            // メール件名
            string subject = textBoxメール件名.Text;
            if (mailAddress != null && mailAddress.Length > 0)
            {
                if (subject == null || subject.Length == 0)
                {
                    errMessage += "・メール件名を入力してください。\n";
                }
                else if (subject.Length > 50)
                {
                    errMessage += "・メール件名は５０文字までで入力してください。\n";
                }
            }

            if (errMessage.Length > 0)
            {
                errMessage = "入力項目に不備があります。\n====================================\n" + errMessage;
                MessageBox.Show(errMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            //=========================================================================
            // 手配依頼にセットする
            //=========================================================================
            対象手配依頼.ThiIraiDate = thiIraiDate;
            対象手配依頼.Basho = basho;
            if (thiUser != null)
            {
                対象手配依頼.ThiUserID = thiUser.MsUserID;
            }
            if (jimTantou != null)
            {
                対象手配依頼.JimTantouID = jimTantou.MsUserID;
            }
            if (thiIraiStatus != null)
            {
                対象手配依頼.MsThiIraiStatusID = thiIraiStatus.MsThiIraiStatusID;
            }
            対象手配依頼.Naiyou = tehaiNaiyou;
            対象手配依頼.Bikou = bikou;
            対象手配依頼.TehaiIraiNo = 手配依頼番号を振る();
            対象手配依頼.Kiboubi = kiboubi;
            対象手配依頼.Kiboukou = kiboukou;

            //=========================================================================
            // 見積依頼にセットする
            //=========================================================================
            if (shrJouken != null)
                対象見積依頼.MsShrJoukenID = shrJouken.MsShrJoukenID;
            対象見積依頼.Okurisaki = okurisaki;
            if (nyukyoKamoku != null)
                対象見積依頼.MsNyukyoKamokuID = nyukyoKamoku.MsNyukyoKamokuID;
            対象見積依頼.Naiyou = naiyou;
            対象見積依頼.MmKigen = mmKigen;

            //=========================================================================
            // 見積回答にセットする
            //=========================================================================
            対象見積回答.MsCustomerID = customer.MsCustomerID;
            対象見積回答.MsCustomerName = customer.CustomerName;
            対象見積回答.Tantousha = tantou;
            対象見積回答.TantouMailAddress = mailAddress;
            対象見積回答.HachuNo = "0";  // Null不可なので？
            対象見積回答.Tax = 0;
            対象見積回答.MkKigen = mmKigen;
            対象見積回答.Kiboubi = kiboubi;
            対象見積回答.Nouki = kiboubi; // 納期に希望納期をセットしておく
            対象見積回答.MmDate = DateTime.Today;  // 2009.11.13:aki 新規見積り時は、処理日を見積日とする
            if (mailAddress != null && mailAddress.Length > 0)
            {
                対象見積回答.WebKey = Hachu.Common.CommonDefine.新規ID(false);
            }
            MsCustomerTantou customerTantou = null;
            foreach (MsCustomerTantou ct in customer.MsCustomerTantous)
            {
                if (ct.Name == tantou)
                {
                    customerTantou = ct;
                    break;
                }
            }
            if (customerTantou != null)
            {
                対象見積回答.Tel = customerTantou.Tel;
                対象見積回答.Fax = customerTantou.Fax;
            }
            if (対象見積回答.Tel == null || 対象見積回答.Tel.Length == 0)
            {
                対象見積回答.Tel = customer.Tel;
            }
            if (対象見積回答.Fax == null || 対象見積回答.Fax.Length == 0)
            {
                対象見積回答.Fax = customer.Fax;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 手配依頼番号を振る
        /// </summary>
        /// <returns></returns>
        #region private string 手配依頼番号を振る()
        private string 手配依頼番号を振る()
        {
            string iraiNo = "";

            iraiNo += 対象手配依頼.ThiIraiDate.Year.ToString().Substring(2); // 年は下２桁
            iraiNo += 対象手配依頼.ThiIraiDate.Month.ToString("00");
            iraiNo += 対象手配依頼.ThiIraiDate.Day.ToString("00");
            // 最後のシーケンス番号は、NBaseService.BLC のインサート時に

            return iraiNo;
        }
        #endregion

        /// <summary>
        /// メールアドレスをセットする
        /// </summary>
        #region private void setMailAddress()
        private void setMailAddress()
        {
            //if (comboBox見積依頼先.Visible == false)
            if (singleLineCombo見積依頼先.Visible == false)
                return;


            string tantouName = comboBox担当者.Text;

            //MsCustomer selectedCustomer = comboBox見積依頼先.SelectedItem as MsCustomer;
            MsCustomer selectedCustomer = singleLineCombo見積依頼先.SelectedItem as MsCustomer;
            if (selectedCustomer != null && selectedCustomer.MsCustomerTantous.Count > 0)
            {
                foreach (MsCustomerTantou ct in selectedCustomer.MsCustomerTantous)
                {
                    if (ct.Name == tantouName)
                    {
                        textBoxメールアドレス.Text = ct.MailAddress;
                        break;
                    }
                }
            }
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
                    MessageBox.Show("対象ファイルを開けません：添付ファイルがみつかりません", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //if (対象見積依頼.MmNo == null || 対象見積依頼.MmNo.Length == 0)
            //{
            //    tableLayoutPanel1.RowStyles[1].Height = panelMaxHeight;
            //    button_OpenClose.Text = "▲";
            //}
            //else
            //{
            //    tableLayoutPanel1.RowStyles[1].Height = panelMinHeight;
            //    button_OpenClose.Text = "▼";
            //}
            tableLayoutPanel1.RowStyles[2].Height = panelMaxHeight;
            button_OpenClose.Text = "▲";
        }

        private void button_OpenClose_Click(object sender, EventArgs e)
        {
            //if (tableLayoutPanel1.RowStyles[1].Height == panelMinHeight)
            //{
            //    tableLayoutPanel1.RowStyles[1].Height = panelMaxHeight;
            //    button_OpenClose.Text = "▲";
            //}
            //else
            //{
            //    tableLayoutPanel1.RowStyles[1].Height = panelMinHeight;
            //    button_OpenClose.Text = "▼";
            //}
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
    }
}
