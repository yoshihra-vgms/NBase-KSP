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
    public partial class 手配依頼Form : BaseUserControl//2021/07/12 BaseForm
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

        //private float panelMinHeight = 36;
        //private float panelMaxHeight = 234 + 6;
        private float panelMinHeight = 0;
        private float panelMaxHeight = 164;

        /// <summary>
        /// 呼び出し元の区別　2021/08/04
        /// </summary>
        private bool 呼び出し元_依頼種別Form = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowStyle">BaseUserControl.WINDOW_STYLE</param>
        /// <param name="info">手配情報</param>
        /// <param name="b">呼び出し元が依頼種別ormならtrue 2021/08/06 m.yoshihara 追加</param>
        #region public 手配依頼Form(int windowStyle, ListInfo手配依頼 info, bool b)
        public 手配依頼Form(int windowStyle, ListInfo手配依頼 info, bool b)
        {
            InitializeComponent();

            this.WindowStyle = windowStyle;
            対象手配依頼 = info.info;

            EnableComponents();

            //2021/08/05 m.yoshihara
            呼び出し元_依頼種別Form = b;
        }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <param name="info"></param>
        #region public 手配依頼Form(int windowStyle, OdThi info)
        public 手配依頼Form(int windowStyle, OdThi info)
        {
            InitializeComponent();

            this.WindowStyle = windowStyle;
            対象手配依頼 = info;

            EnableComponents();
        }
        #endregion


        private void EnableComponents()
        {
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "発注状況一覧", "手配依頼"))
            {
                button手配依頼作成.Enabled = true;
                button発注.Enabled = true;
                button見積依頼作成.Enabled = true;
                button保存.Enabled = true;
                button更新.Enabled = true;
                button取消.Enabled = true;

                comboBox手配依頼者.Enabled = true;
                comboBox事務担当者.Enabled = true;
                comboBox現状.Enabled = true;
                textBox手配内容.Enabled = true;
                textBox備考.Enabled = true;

                comboBox見積依頼先.Enabled = true;
                textBox担当者.Enabled = true;
                textBox場所.Enabled = true;
                dateTimePicker手配依頼日.Enabled = true;
                dateTimePicker希望納期.Enabled = true;
                textBox希望港.Enabled = true;

                button品目編集.Enabled = true;
                button読込.Enabled = true;
                button出力.Enabled = true;
            }
        }


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
        #region private void 手配依頼Form_Load(object sender, EventArgs e)
        private void 手配依頼Form_Load(object sender, EventArgs e)
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
            // Formを閉じる
            //BaseFormClosing();
            //Close();
            if (this.ClosingEvent != null)
            {
                //-----------------------------
                //処理まとめた　2021/08/05 
                #region コメントアウト
                //ListInfo手配依頼 info = null;
                //if (IsCreated)
                //{
                //    info = new ListInfo手配依頼();
                //    info.info = 対象手配依頼;
                //}
                //InfoUpdating(info);
                //
                #endregion
                CallInfoUpdating();
                //-----------------------------

                ClosingEvent();
            }
        }
        #endregion


        /// <summary>
        /// 「手配依頼作成」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button手配依頼作成_Click(object sender, EventArgs e)
        private void button手配依頼作成_Click(object sender, EventArgs e)
        {
            if (手配依頼作成処理() == false)
            {
                return;
            }

            IsCreated = true;

            MessageBox.Show("手配依頼を作成しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //ListInfo手配依頼 info = new ListInfo手配依頼();
            //info.info = 対象手配依頼;
            //InfoUpdating(info);

            //呼び出し元により、動作をわける 2021/08/04 m.yoshihara
            //Formに情報をセットする(); コメントアウト 2021/08/04
            if (呼び出し元_依頼種別Form == false)
            {//リストクリックから
               
                Formに情報をセットする();

                //一覧を更新したい
                CallInfoUpdating();
            }
            else//依頼種別Formから
            {
                //画面を閉じる
                button閉じる_Click(sender, e);
            }
        }
        #endregion


        /// <summary>
        /// 「発注」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button発注_Click(object sender, EventArgs e)
        private void button発注_Click(object sender, EventArgs e)
        {
            bool isNewItem = false;
            if (対象手配依頼.RenewUserID == "")
            {
                isNewItem = true;
            }

            if (MessageBox.Show("発注処理をします。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            OdMm 見積依頼 = new OdMm();
            OdMk 見積回答 = new OdMk();
            OdJry 受領 = new OdJry();
            発注処理準備(ref 見積依頼, ref 見積回答);
            if (発注処理(ref 見積依頼, ref 見積回答, ref 受領) == false)
            {
                return;
            }
            MessageBox.Show("発注しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            受領.OdMkNouki = 見積回答.Nouki;

            ListInfo手配依頼 info = new ListInfo手配依頼();
            if (isNewItem)
            {
                info.info = 対象手配依頼;
                InfoUpdating(info);
                
                info = new ListInfo手配依頼();
            }
            info.NextStatus = true;
            info.info = 対象手配依頼;
            info.child = 見積依頼;
            info.child2 = 見積回答;
            info.child3 = 受領;
            InfoUpdating(info);
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
            if (MessageBox.Show("手配依頼を登録後、見積依頼を作成します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            OdMm 見積依頼 = new OdMm();
            if (見積依頼作成処理(ref 見積依頼) == false)
            {
                return;
            }
            MessageBox.Show("見積依頼を作成しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //---------コメントアウト2021/07/12-----------
            //if (this.MdiParent == null)
            //{
            //    // 「アラーム情報」からの呼び出しの場合
            //    Close();
            //}
            if (this.Parent == null)
            {
                // 「アラーム情報」からの呼び出しの場合
                ;
            }
            else
            {
                ListInfo手配依頼 info = new ListInfo手配依頼();
                info.info = 対象手配依頼;
                info.child = 見積依頼;
                info.NextStatus = true;
                InfoUpdating(info);
            }
        }
        #endregion

        /// <summary>
        /// 「保存」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button保存_Click(object sender, EventArgs e)
        private void button保存_Click(object sender, EventArgs e)
        {
            if (保存処理() == false)
            {
                return;
            }

            MessageBox.Show("保存しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //ListInfo手配依頼 info = new ListInfo手配依頼();
            //info.info = 対象手配依頼;
            //InfoUpdating(info);

            if (button閉じる.Visible)
            {
                IsCreated = true;
            }
            #region コメントアウト 2021/08/05 後の処理CallInfoUpdating()やbutton閉じる_Click()で行う
            //else
            //{
            //    ListInfo手配依頼 info = new ListInfo手配依頼();
            //    info.info = 対象手配依頼;
            //    InfoUpdating(info);
            //}
            #endregion

            //----------------------------------------------------------
            //呼び出し元により、動作をわける 2021/08/04 m.yoshihara
            //Formに情報をセットする(); コメントアウト 2021/08/04
            if (呼び出し元_依頼種別Form == false)
            {//リストクリックから

                Formに情報をセットする();

                //一覧更新
                CallInfoUpdating();
            }
            else
            {//依頼種別Formから

                button閉じる_Click(sender, e);
            }
            //----------------------------------------------------------
        }
        #endregion

        /// <summary>
        /// 「更新」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button更新_Click(object sender, EventArgs e)
        private void button更新_Click(object sender, EventArgs e)
        {
            if (更新処理() == false)
            {
                return;
            }

            MessageBox.Show("更新しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //---------コメントアウト2021/07/12-----------
            //if (this.MdiParent == null)
            //{
            //    // 「アラーム情報」からの呼び出しの場合
            //    Close();
            //}
            if (this.Parent == null)
            {
                // 「アラーム情報」からの呼び出しの場合
                ;
            }
            else
            {
                ListInfo手配依頼 info = new ListInfo手配依頼();
                info.info = 対象手配依頼;
                InfoUpdating(info);

                Formに情報をセットする();
            }
        }
        #endregion

        /// <summary>
        /// 「取消」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button取消_Click(object sender, EventArgs e)
        private void button取消_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("この手配依頼を取消します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // 取消し処理
            if (取消処理() == false)
            {
                return;
            }
            MessageBox.Show("取消しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //---------コメントアウト2021/07/12-----------
            //if (this.MdiParent == null)
            //{
            //    // 「アラーム情報」からの呼び出しの場合
            //    Close();
            //}
            if (this.Parent == null)
            {
                // 「アラーム情報」からの呼び出しの場合
                ;
            }
            else
            {
                ListInfo手配依頼 info = new ListInfo手配依頼();
                info.Remove = true;
                info.info = 対象手配依頼;
                InfoUpdating(info);

                // Formを閉じる
                //---------コメントアウト2021/07/12-----------
                //Close();
            }
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
            if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                燃料潤滑油編集Form form = new 燃料潤滑油編集Form(手配品目s);
                if (form.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                ResetTreeListView();
            }
            else
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

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "発注状況一覧", "手配依頼"))
            {
                品目TreeList.DoubleClick(対象手配依頼, ref 手配品目s, ref 削除手配品目s);

                // 処理前の位置をセットする
                treeListView.SetScrollPos(orgPoint);
            }
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

                    ////2013/12/18 追加 m.y
                    ////サーバーエラー時のフラグ
                    //bool serverError = false;

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

                    ////--------------------------------
                    ////2013/12/18 追加 m.y 
                    //if (serverError == true)
                    //    return;
                    ////--------------------------------

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



        /// <summary>
        /// 「発注メール再送」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button発注メール再送_Click(object sender, EventArgs e)
        private void button発注メール再送_Click(object sender, EventArgs e)
        {
            //発注メール送信Form form = new 発注メール送信Form(対象見積回答, 発注メール送信Form.メール送信Enum.再送信);
            //form.ShowDialog();
            発注メール送信Form form = new 発注メール送信Form(ref 対象見積回答, 発注メール送信Form.メール送信Enum.再送信);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Formに情報をセットする();
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

                if (対象手配依頼.MsThiIraiShousaiID != NBaseCommon.Common.MsThiIraiShousai_小修理ID)
                {
                    // 2012.02 -->
                    button読込.Text = "ﾄﾞｯｸｵｰﾀﾞｰ読込";
                    button出力.Text = "ﾄﾞｯｸｵｰﾀﾞｰ出力";

                    button読込.Width = 92;
                    button出力.Width = 92;
                    // <-- 2012.02 

                    button読込.Visible = true;
                    button出力.Visible = true;
                }
                else
                {
                    button読込.Visible = false;
                    button出力.Visible = false;
                }
                品目TreeList.Enabled = true;

                button発注.Visible = false;
                if (対象手配依頼.Status == 対象手配依頼.OdStatusValue.Values[(int)OdThi.STATUS.手配依頼済].Value)
                {
                    button手配依頼作成.Visible = false;
                    button保存.Visible = false;

                    if (対象手配依頼.MmFlag == (int)OdThi.MM_FLAG.なし)
                    {
                        button見積依頼作成.Visible = false;
                        button更新.Visible = false;
                        button取消.Visible = false;

                        button品目編集.Enabled = false;
                        品目TreeList.Enabled = false;

                        button注文書出力.Visible = true;
                        button発注メール再送.Visible = true;
                    }
                    else
                    {
                        button見積依頼作成.Visible = true;
                        button更新.Visible = true;
                        button取消.Visible = true;
                        button注文書出力.Visible = false;
                        button発注メール再送.Visible = false;
                    }
                }
                else
                {
                    button手配依頼作成.Visible = true;
                    button保存.Visible = true;

                    button見積依頼作成.Visible = false;
                    button更新.Visible = false;
                    if (対象手配依頼.RenewUserID == "")
                    {
                        button取消.Visible = false;
                    }
                    else
                    {
                        button取消.Visible = true;
                    }
                    button注文書出力.Visible = false;
                    button発注メール再送.Visible = false;
                }
                // 2009.11.26:aki 更新はいつでもOKとする
                //button更新.Visible = true;
                // 2010.02.16:aki 新規の場合、更新はなし
                if (対象手配依頼.RenewUserID == "")
                {
                    button更新.Visible = false;
                }
                else
                {
                    button更新.Visible = true;
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

                button読込.Visible = false;
                button出力.Visible = false;
                button品目編集.Enabled = false;
                品目TreeList.Enabled = true;

                if (対象手配依頼.Status == 対象手配依頼.OdStatusValue.Values[(int)OdThi.STATUS.手配依頼済].Value)
                {
                    button手配依頼作成.Visible = false;
                    button保存.Visible = false;
                    button更新.Visible = true;
                    button取消.Visible = true;
                    if (対象手配依頼.MmFlag == (int)OdThi.MM_FLAG.なし)
                    {
                        panel発注.Visible = true;

                        button発注.Text = "発注";
                        button発注.Width = 100;
                        button発注.Visible = true;
                        button見積依頼作成.Visible = false;
                    }
                    else
                    {
                        button発注.Visible = false;
                        button見積依頼作成.Visible = true;
                    }
                }
                else
                {
                    button手配依頼作成.Visible = true;
                    button手配依頼作成.Text = "手配依頼のみ作成";
                    button手配依頼作成.Width = 114;
                    button保存.Visible = true;
                    button見積依頼作成.Visible = false;
                    button更新.Visible = false;
                    button更新.Visible = false;
                    if (対象手配依頼.RenewUserID == "")
                    {
                        button取消.Visible = false;
                    }
                    else
                    {
                        button取消.Visible = true;
                    }

                    if (対象手配依頼.MmFlag == (int)OdThi.MM_FLAG.なし)
                    {
                        //button発注.Visible = true;
                        //button発注.Text = "手配依頼後、発注";
                        //button発注.Width = 114;
                        button発注.Visible = false; // 2009.09.17 いきなりの発注はさせない
                    }
                    else
                    {
                        button発注.Visible = false;
                    }
                }
                button注文書出力.Visible = false;
                button発注メール再送.Visible = false;
                #endregion


                //===========================
                // 2014.1 [2013年度改造]
                if (!Hachu.Common.CommonDefine.Is新規(対象手配依頼.OdThiID))
                    button品目編集.Enabled = true;
            }
            else 
            {
                // 船用品の場合
                #region
                label手配依頼詳細種別.Visible = false;
                textBox手配依頼詳細種別.Visible = false;
                panel発注.Visible = false;
                panel燃料潤滑油.Visible = false;

                // 2012.02 -->
                //button読込.Visible = false;
                //button出力.Visible = false;
                button読込.Text = "船用品注文書読込";
                button出力.Text = "船用品注文書出力";
                button出力.Enabled = false;

                button読込.Width = 112;
                button出力.Width = 112;

                button読込.Visible = true;
                button出力.Visible = true;
                // <-- 2012.02 

                品目TreeList.Enabled = true;

                button発注.Visible = false;
                if (対象手配依頼.Status == 対象手配依頼.OdStatusValue.Values[(int)OdThi.STATUS.手配依頼済].Value)
                {
                    button手配依頼作成.Visible = false;
                    button保存.Visible = false;
                    button出力.Enabled = true;

                    if (対象手配依頼.MmFlag == (int)OdThi.MM_FLAG.なし)
                    {
                        button見積依頼作成.Visible = false;
                        button更新.Visible = false;
                        button取消.Visible = false;

                        button品目編集.Enabled = false;
                        品目TreeList.Enabled = false;

                        button注文書出力.Visible = true;
                        button発注メール再送.Visible = true;
                    }
                    else
                    {
                        button見積依頼作成.Visible = true;
                        button更新.Visible = true;
                        button取消.Visible = true;

                        button注文書出力.Visible = false;
                        button発注メール再送.Visible = false;
                    }
                }
                else
                {
                    button手配依頼作成.Visible = true;
                    button保存.Visible = true;

                    button見積依頼作成.Visible = false;
                    button更新.Visible = false;
                    if (対象手配依頼.RenewUserID == "")
                    {
                        button取消.Visible = false;
                    }
                    else
                    {
                        button取消.Visible = true;
                    }

                    button注文書出力.Visible = false;
                    button発注メール再送.Visible = false;
                }
                // 2009.11.26:aki 更新はいつでもOKとする
                //button更新.Visible = true;
                // 2010.02.16:aki 新規の場合、更新はなし
                if (対象手配依頼.RenewUserID == "")
                {
                    button更新.Visible = false;
                }
                else
                {
                    button更新.Visible = true;
                }
                #endregion
            }


            if (this.ClosingEvent == null)
            {
                button閉じる.Visible = false;
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
            
            品目TreeList.AddNodes(viewHeader, 手配品目s);

            品目TreeList.LinkClickEvent += new ItemTreeListView.LinkClickEventHandler(View添付ファイル);

            削除手配品目s = new List<Item手配依頼品目>();
        }
        #endregion

        /// <summary>
        /// 手配依頼作成処理
        /// </summary>
        /// <returns></returns>
        #region private bool 手配依頼作成処理()
        private bool 手配依頼作成処理()
        {
            try
            {
                対象手配依頼.Status = 対象手配依頼.OdStatusValue.Values[(int)OdThi.STATUS.手配依頼済].Value;
                if (Hachu.Common.CommonDefine.Is新規(対象手配依頼.OdThiID) == true)
                {
                    対象手配依頼.OdThiID = Hachu.Common.CommonDefine.RemovePrefix(対象手配依頼.OdThiID);
                }
                if (入力情報の取得確認() == false)
                {
                    return false;
                }
                if (対象手配依頼.TehaiIraiNo.Length == 0)
                {
                    対象手配依頼.TehaiIraiNo = 手配依頼番号を振る();
                }
                if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                {
                    対象手配依頼.MailSend = true;
                }
                else
                {
                    対象手配依頼.MailSend = false;
                }

                bool ret = false;
                if (対象手配依頼.RenewUserID == "")
                {
                    ret = 手配依頼更新処理.新規(ref 対象手配依頼, 手配品目s);
                }
                else
                {
                    ret = 手配依頼更新処理.更新(ref 対象手配依頼, 手配品目s, 削除手配品目s);
                }
                if (ret == false)
                {
                    MessageBox.Show("手配依頼の作成に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("手配依頼の作成に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion
        
        /// <summary>
        /// 見積依頼作成処理
        /// </summary>
        /// <param name="見積依頼"></param>
        /// <returns></returns>
        #region private bool 見積依頼作成処理(ref OdMm 見積依頼)
        private bool 見積依頼作成処理(ref OdMm 見積依頼)
        {
            try
            {
                if (入力情報の取得確認() == false)
                {
                    return false;
                }
                if (対象手配依頼.TehaiIraiNo.Length == 0)
                {
                    対象手配依頼.TehaiIraiNo = 手配依頼番号を振る();
                }

                bool ret = false;

                ret = 手配依頼更新処理.更新(ref 対象手配依頼, 手配品目s, 削除手配品目s);
                if (ret == false)
                {
                    MessageBox.Show("更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                List<Item手配依頼品目> 見積対象品目s = null;
                if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                {
                    見積対象品目s = 手配品目s;
                }
                else
                {
                    // 手配依頼品目/詳細品目の内、チェックされているもののみ見積依頼へ
                    見積対象品目s = 品目TreeList.GetCheckedNodes();
                }
                ret = 見積依頼更新処理.手配依頼から作成(ref 対象手配依頼, ref 見積依頼, 見積対象品目s);
                if (ret == false)
                {
                    MessageBox.Show("見積依頼の作成に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
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
        /// 発注処理の準備をする
        /// </summary>
        /// <param name="見積依頼"></param>
        /// <param name="見積回答"></param>
        #region private void 発注処理準備(ref OdMm 見積依頼, ref OdMk 見積回答)
        private void 発注処理準備(ref OdMm 見積依頼, ref OdMk 見積回答)
        {
            DateTime RenewDate = DateTime.Now;
            string RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            // 見積依頼を用意する
            #region
            string OdThiID = 対象手配依頼.OdThiID;
            if (Hachu.Common.CommonDefine.Is新規(OdThiID) == true)
            {
                OdThiID = Hachu.Common.CommonDefine.RemovePrefix(OdThiID);
            }

            見積依頼.OdMmID = Hachu.Common.CommonDefine.新規ID(false);
            見積依頼.OdThiID = OdThiID;
            見積依頼.Status = 見積依頼.OdStatusValue.Values[(int)OdMm.STATUS.見積依頼済].Value;
            見積依頼.VesselID = 対象手配依頼.MsVesselID;
            見積依頼.MmDate = RenewDate;
            見積依頼.MmSakuseisha = RenewUserID;
            #endregion

            // 見積回答を用意する
            #region
            見積回答.OdMkID = Hachu.Common.CommonDefine.新規ID(false);
            見積回答.OdMmID = 見積依頼.OdMmID;
            見積回答.Status = 見積回答.OdStatusValue.Values[(int)OdMk.STATUS.発注済み].Value;
            見積回答.VesselID = 対象手配依頼.MsVesselID;
            見積回答.MsThiIraiSbtID = 対象手配依頼.MsThiIraiSbtID;
            見積回答.MsVesselID = 対象手配依頼.MsVesselID;
            見積回答.HachuNo = "0";
            見積回答.HachuDate = RenewDate;
            #endregion
        }
        #endregion

        /// <summary>
        /// 発注処理
        /// </summary>
        /// <param name="見積依頼"></param>
        /// <param name="見積回答"></param>
        /// <param name="受領"></param>
        /// <returns></returns>
        #region private bool 発注処理(ref OdMm 見積依頼, ref OdMk 見積回答, ref OdJry 受領)
        private bool 発注処理(ref OdMm 見積依頼, ref OdMk 見積回答, ref OdJry 受領)
        {
            try
            {
                if (入力情報の取得確認(見積回答) == false)
                {
                    return false;
                }
                bool ret = false;
                if (対象手配依頼.Status == 対象手配依頼.OdStatusValue.Values[(int)OdThi.STATUS.手配依頼済].Value)
                {
                    // 既に手配依頼がある状態
                    対象手配依頼.MailSend = false;
                    ret = 新規発注更新処理.手配依頼から発注(false, ref 対象手配依頼, ref 見積依頼, ref 見積回答, ref 受領, 手配品目s);
                }
                else
                {
                    // 手配依頼登録 → 発注登録
                    対象手配依頼.MailSend = true;
                    対象手配依頼.Status = 対象手配依頼.OdStatusValue.Values[(int)OdThi.STATUS.手配依頼済].Value;
                    if (Hachu.Common.CommonDefine.Is新規(対象手配依頼.OdThiID) == true)
                    {
                        対象手配依頼.OdThiID = Hachu.Common.CommonDefine.RemovePrefix(対象手配依頼.OdThiID);
                    }
                    if (対象手配依頼.TehaiIraiNo.Length == 0)
                    {
                        対象手配依頼.TehaiIraiNo = 手配依頼番号を振る();
                    }
                    ret = 新規発注更新処理.手配依頼から発注(true, ref 対象手配依頼, ref 見積依頼, ref 見積回答, ref 受領, 手配品目s);
                }
                if (ret == false)
                {
                    MessageBox.Show("発注に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("発注に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 保存処理
        /// </summary>
        /// <returns></returns>
        #region private bool 保存処理()
        private bool 保存処理()
        {
            try
            {
                対象手配依頼.Status = 対象手配依頼.OdStatusValue.Values[(int)OdThi.STATUS.事務所未手配].Value;
                if (Hachu.Common.CommonDefine.Is新規(対象手配依頼.OdThiID) == true)
                {
                    対象手配依頼.OdThiID = Hachu.Common.CommonDefine.RemovePrefix(対象手配依頼.OdThiID);
                }
                if (入力情報の取得確認() == false)
                {
                    return false;
                }

                bool ret = false;
                if (対象手配依頼.RenewUserID == "")
                {
                    ret = 手配依頼更新処理.新規(ref 対象手配依頼, 手配品目s);
                }
                else
                {
                    ret = 手配依頼更新処理.更新(ref 対象手配依頼, 手配品目s, 削除手配品目s);
                }
                if (ret == false)
                {
                    MessageBox.Show("保存に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("保存に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
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
                if (対象手配依頼.TehaiIraiNo.Length == 0)
                {
                    対象手配依頼.TehaiIraiNo = 手配依頼番号を振る();
                }
                if (入力情報の取得確認() == false)
                {
                    return false;
                }
                bool ret = 手配依頼更新処理.更新(ref 対象手配依頼, 手配品目s, 削除手配品目s);
                if (ret == false)
                {
                    MessageBox.Show("更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 取消処理
        /// </summary>
        /// <returns></returns>
        #region private bool 取消処理()
        private bool 取消処理()
        {
            try
            {
                bool ret = 手配依頼更新処理.取消(ref 対象手配依頼);
                if (ret == false)
                {
                    MessageBox.Show("取消に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("取消に失敗しました。\n致命的なエラーです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            return 入力情報の取得確認(null);
        }
        private bool 入力情報の取得確認(OdMk 回答)
        {
            string errMessage = "";

            DateTime thiIraiDate = DateTime.MinValue;
            string basho = "";
            MsUser thiUser = null;
            MsUser jimTantou = null;
            MsThiIraiStatus thiIraiStatus = null;
            string naiyou = "";
            string bikou = "";
            DateTime kiboubi = DateTime.MinValue;
            string kiboukou = "";

            // 手配依頼日
            try
            {
                thiIraiDate = DateTime.Parse(dateTimePicker手配依頼日.Text);
            }
            catch
            {
                errMessage += "・手配依頼日が不正です。\n";
            }
            // 場所
            if (textBox場所.Text.Length > 0)
            {
                basho = textBox場所.Text;
                if (basho.Length > 20)
                {
                    errMessage += "・場所は２０文字までで入力してください。\n";
                }
            }
            // 手配依頼者
            try
            {
                thiUser = comboBox手配依頼者.SelectedItem as MsUser;
            }
            catch
            {
            }
            // 事務担当者
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
            try
            {
                thiIraiStatus = comboBox現状.SelectedItem as MsThiIraiStatus;
            }
            catch
            {
            }
            // 手配内容
            if (textBox手配内容.Text.Length == 0)
            {
                errMessage += "・手配内容を入力してください。\n";
            }
            else
            {
                naiyou = StringUtils.Escape(textBox手配内容.Text);
                if (naiyou.Length > 50)
                {
                    errMessage += "・手配内容は５０文字までで入力してください。\n";
                    return false;
                }
            }
            // 備考
            if (textBox備考.Text.Length > 0)
            {
                bikou = StringUtils.Escape(textBox備考.Text);
                if (bikou.Length > 500)
                {
                    errMessage += "・備考は５００文字までで入力してください。\n";
                    return false;
                }
            }
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
            MsCustomer customer = null;
            string tantou = "";
            if (panel燃料潤滑油.Visible == true && 回答 != null)
            {
                // 見積依頼先
                if (comboBox見積依頼先.SelectedItem is MsCustomer)
                {
                    customer = comboBox見積依頼先.SelectedItem as MsCustomer;
                }
                else
                {
                    errMessage += "・見積依頼先を選択してください。\n";
                }
                // 担当者
                tantou = textBox担当者.Text;
                if (tantou == null || tantou.Length == 0)
                {
                    errMessage += "・担当者を入力してください。\n";
                }

            }

            if (errMessage.Length > 0)
            {
                errMessage = "入力項目に不備があります。\n====================================\n" + errMessage;
                MessageBox.Show(errMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

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
            対象手配依頼.Naiyou = naiyou;
            対象手配依頼.Bikou = bikou;
            対象手配依頼.Kiboubi = kiboubi;
            対象手配依頼.Kiboukou = kiboukou;
            if (回答 != null)
            {
                回答.MsCustomerID = customer.MsCustomerID;
                回答.Tantousha = tantou;
                回答.Nouki = kiboubi;
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

            if (対象手配依頼.ThiIraiDate != null && 対象手配依頼.ThiIraiDate != DateTime.MinValue)
            {
                iraiNo += 対象手配依頼.ThiIraiDate.Year.ToString().Substring(2); // 年は下２桁
                iraiNo += 対象手配依頼.ThiIraiDate.Month.ToString("00");
                iraiNo += 対象手配依頼.ThiIraiDate.Day.ToString("00");
                // 最後のシーケンス番号は、NBaseService.BLC のインサート時に
            }
            return iraiNo;
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


        /// <summary>
        /// InfoUpdating()を呼び出す 2021/08/05 m.yoshihara
        /// </summary>
        private void CallInfoUpdating()
        {
            ListInfo手配依頼 info = null;
            if (IsCreated)
            {
                info = new ListInfo手配依頼();
                info.info = 対象手配依頼;
                InfoUpdating(info);
            }
        }



        /// <summary>
        /// 「注文書出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button注文書出力_Click(object sender, EventArgs e)
        private void button注文書出力_Click(object sender, EventArgs e)
        {
            ////this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            ////注文書 chumon = new 注文書();
            ////chumon.Output(対象見積回答.OdMkID);
            ////this.Cursor = System.Windows.Forms.Cursors.Default;
            //注文書出力Form form = new 注文書出力Form(対象見積回答);
            //if (form.ShowDialog() == DialogResult.Cancel)
            //{
            //    return;
            //}

            KK発注帳票出力.注文書Output(対象見積回答.OdMkID);
        }
        #endregion

        private void button査定表出力_Click(object sender, EventArgs e)
        {
            KK発注帳票出力.査定表Output(対象手配依頼.OdThiID);
        }

        private void button請求書出力_Click(object sender, EventArgs e)
        {
            KK発注帳票出力.請求書Output(対象手配依頼.OdThiID); 
        }

    }
}
