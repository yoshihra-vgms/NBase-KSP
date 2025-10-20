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
    public partial class 新規発注Form : BaseUserControl
    {
        private bool IsCreated = false;
        public delegate void ClosingEventHandler();
        public event ClosingEventHandler ClosingEvent;

        /// <summary>
        /// 状態値
        /// </summary>
        private enum enum状態 { 新規, 注文書出力済, 発注済み };

        /// <summary>
        /// 
        /// </summary>
        private enum状態 状態 = enum状態.新規;

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
        /// 対象受領
        /// </summary>
        private OdJry 対象受領;

        /// <summary>
        /// 対象見積回答の品目
        /// </summary>
        private List<Item見積回答品目> 見積回答品目s;

        /// <summary>
        /// 削除とマークされた品目
        /// </summary>
        private List<Item見積回答品目> 削除回答品目s;

        /// <summary>
        /// 
        /// </summary>
        ItemTreeListView新規発注 品目TreeList;

        /// <summary>
        /// 見積依頼先
        /// </summary>
        List<MsCustomer> 見積依頼先s = null;

        //private float panelMinHeight = 36;
        //private float panelMaxHeight = 340 + 6;
        private float panelMinHeight = 0;
        private float panelMaxHeight1 = 285;
        private float panelMaxHeight2 = 70;

        /// <summary>
        /// 呼び出し元の区別　2021/08/04
        /// </summary>
        private bool 呼び出し元_依頼種別Form = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowStyle">BaseUserControl.WINDOW_STYLE</param>
        /// <param name="info">発注情報</param>
        /// <param name="b">呼び出し元が依頼種別ormならtrue 2021/08/06 m.yoshihara 追加</param>
        #region public 新規発注Form(int windowStyle, ListInfo新規発注 info, bool b)
        public 新規発注Form(int windowStyle, ListInfo新規発注 info, bool b)
        {
            InitializeComponent();

            this.WindowStyle = windowStyle;
            対象手配依頼 = info.parent;
            対象見積依頼 = info.info;
            対象見積回答 = info.child;

            singleLineCombo見積依頼先.selected += new NBaseUtil.SingleLineCombo.SelectedEventHandler(comboBox見積依頼先_SelectedIndexChanged);

            //追加 2021/08/05 m.yoshihara
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
        #region private void 新規発注Form_Load(object sender, EventArgs e)
        private void 新規発注Form_Load(object sender, EventArgs e)
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
                ListInfo手配依頼 info = null;
                if (IsCreated)
                {
                    info = new ListInfo手配依頼();
                    info.info = 対象手配依頼;
                    info.child = 対象見積依頼;
                    info.child2 = 対象見積回答;
                    info.child3 = 対象受領;

                    //二重に呼ばれているのでコメント　m.yoshihara 2021/08/06 InfoUpdating(info);

                    InfoUpdating(info);
                }
                //InfoUpdating(info);

                ClosingEvent();
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
            if (!Validate単価())
            {
                return;
            }
            if (MessageBox.Show("発注処理をします。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (発注処理() == false)
            {
                return;
            }
            MessageBox.Show("発注しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //発注メール送信Form form = new 発注メール送信Form(対象見積回答, 発注メール送信Form.メール送信Enum.新規送信);
            //form.ShowDialog();
            発注メール送信Form form = new 発注メール送信Form(ref 対象見積回答, 発注メール送信Form.メール送信Enum.新規送信);
            if (form.ShowDialog() == DialogResult.OK)
            {
                // 2012.03 : 
                // 発注メール送信画面で「送り先」「希望納期」が変更できるようになるので
                // 再度取得して、画面の表示を更新する必要がある
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    対象見積依頼 = serviceClient.OdMm_GetRecord(NBaseCommon.Common.LoginUser, 対象見積依頼.OdMmID);
                    対象見積回答 = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, 対象見積回答.OdMkID);
                }

                textBox送り先.Text = 対象見積依頼.Okurisaki;
                dateTimePicker納期.Text = 対象見積回答.Nouki.ToShortDateString();
            }

            状態 = enum状態.発注済み;

            対象受領.OdMkNouki = 対象見積回答.Nouki;

            IsCreated = true;

            //ListInfo手配依頼 info = new ListInfo手配依頼();
            //info.info = 対象手配依頼;
            //info.child = 対象見積依頼;
            //info.child2 = 対象見積回答;
            //info.child3 = 対象受領;
            //InfoUpdating(info);

            //新規発注ボタンから呼ばれているなら閉じる 2021/08/06 m.yoshihara 
            if (呼び出し元_依頼種別Form == true)
            {
                button閉じる_Click(sender, e);
                return;
            }

            // 画面の入力を不可に
            #region
            comboBox手配依頼者.Enabled = false;
            textBox場所.ReadOnly = true;
            comboBox事務担当者.Enabled = false;
            textBox手配内容.ReadOnly = true;
            textBox備考.ReadOnly = true;

            //comboBox見積依頼先.Enabled = false;
            singleLineCombo見積依頼先.Enabled = false;
            comboBox担当者.Enabled = false;
            comboBox支払条件.Enabled = false;
            textBox送り先.ReadOnly = true;
            textBox工期.ReadOnly = true;
            dateTimePicker納期.Enabled = false;

            textBox見積値引き.ReadOnly = true;
            textBox見積値引き2.ReadOnly = true;
            textBox消費税.ReadOnly = true;
            textBox消費税2.ReadOnly = true;
            textBox送料運搬料.ReadOnly = true;
            textBox送料運搬料2.ReadOnly = true;

            button品目編集.Enabled = false;
            treeListView.Enabled = false;

            if (panel入渠.Visible == true)
            {
                comboBox入渠科目.Enabled = false;
                textBox内容.ReadOnly = true;
            }

            //「発注」ボタンを使用不可に
            button発注.Enabled = false;
            //「注文書出力」ボタンを使用可に
            button注文書出力.Enabled = true;
            #endregion

        }
        #endregion

        /// <summary>
        /// 「注文書出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button注文書出力_Click(object sender, EventArgs e)
        private void button注文書出力_Click(object sender, EventArgs e)
        {
            // 発注済みでない場合、注文書を出すために、一時保存をする
            if (状態 != enum状態.発注済み)
            {
                if (一時保存処理() == false)
                {
                    return;
                }
                状態 = enum状態.注文書出力済;
            }

            ////// 注文書を出力する
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

            // 発注済みでない場合、一時保存した情報を削除する
            if (状態 != enum状態.発注済み)
            {
                新規発注更新処理.保存情報削除(対象手配依頼, 対象見積依頼, 対象見積回答);
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
                }
                else
                {
                    comboBox担当者.Text = "";
                }
            }
        }
        #endregion

        /// <summary>
        /// 「見積値引き」の入力キーを制限する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox見積値引き_KeyPress(object sender, KeyPressEventArgs e)
        private void textBox見積値引き_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        #endregion

        /// <summary>
        /// 「見積値引き」の入力後、金額欄の再計算、再表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox見積値引き_KeyUp(object sender, KeyEventArgs e)
        private void textBox見積値引き_KeyUp(object sender, KeyEventArgs e)
        {
            金額欄再表示(sender);
        }
        #endregion

        /// <summary>
        /// 「見積値引き」にフォーカスインした場合、値引き額を数字のみに
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox見積値引き_Enter(object sender, EventArgs e)
        private void textBox見積値引き_Enter(object sender, EventArgs e)
        {
            TextBox tb = (sender as TextBox);
            //if (textBox見積値引き.ReadOnly == true)
            if (tb.ReadOnly == true)
                return;

            try
            {
                //decimal nebiki = NBaseCommon.Common.金額表示を数値へ変換(textBox見積値引き.Text);
                //textBox見積値引き.Text = nebiki.ToString();
                decimal nebiki = NBaseCommon.Common.金額表示を数値へ変換(tb.Text);
                tb.Text = nebiki.ToString();
            }
            catch
            {
            }
        }
        #endregion

        /// <summary>
        /// 「見積値引き」からフォーカスアウトした場合、値引き額を金額フォーマットへ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox見積値引き_Leave(object sender, EventArgs e)
        private void textBox見積値引き_Leave(object sender, EventArgs e)
        {
            try
            {
                //decimal nebiki = decimal.Parse(textBox見積値引き.Text);
                //textBox見積値引き.Text = NBaseCommon.Common.金額出力(nebiki);
                decimal nebiki = decimal.Parse((sender as TextBox).Text);
                textBox見積値引き.Text = NBaseCommon.Common.金額出力(nebiki);
                textBox見積値引き2.Text = NBaseCommon.Common.金額出力(nebiki);
            }
            catch
            {
            }
        }
        #endregion

        /// <summary>
        /// 「消費税」の入力キーを制限する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox消費税_KeyPress(object sender, KeyPressEventArgs e)
        private void textBox消費税_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        #endregion

        /// <summary>
        /// 「消費税」の入力後、金額欄の再計算、再表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox消費税_KeyUp(object sender, KeyEventArgs e)
        private void textBox消費税_KeyUp(object sender, KeyEventArgs e)
        {
            金額欄再表示(sender);
        }
        #endregion

        /// <summary>
        /// 「消費税」にフォーカスインした場合、消費税を数字のみに
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox消費税_Enter(object sender, EventArgs e)
        private void textBox消費税_Enter(object sender, EventArgs e)
        {
            TextBox tb = (sender as TextBox);

            //if (textBox消費税.ReadOnly == true)
            if (tb.ReadOnly == true)
                return;

            try
            {
                //decimal tax = NBaseCommon.Common.金額表示を数値へ変換(textBox消費税.Text);
                //textBox消費税.Text = tax.ToString();
                decimal tax = NBaseCommon.Common.金額表示を数値へ変換(tb.Text);
                tb.Text = tax.ToString();
            }
            catch
            {
            }

        }
        #endregion

        /// <summary>
        /// 「消費税」からフォーカスアウトした場合、消費税を金額フォーマットへ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox消費税_Leave(object sender, EventArgs e)
        private void textBox消費税_Leave(object sender, EventArgs e)
        {
            try
            {
                //decimal tax = decimal.Parse(textBox消費税.Text);
                //textBox消費税.Text = NBaseCommon.Common.金額出力(tax);
                decimal tax = decimal.Parse((sender as TextBox).Text);
                textBox消費税.Text = NBaseCommon.Common.金額出力(tax);
                textBox消費税2.Text = NBaseCommon.Common.金額出力(tax);
            }
            catch
            {
            }
        }
        #endregion

        /// <summary>
        /// 「送料運搬料」の入力キーを制限する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox送料運搬料_KeyPress(object sender, KeyPressEventArgs e)
        private void textBox送料運搬料_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        #endregion

        /// <summary>
        /// 「送料運搬料」の入力後、金額欄の再計算、再表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox送料運搬料_KeyUp(object sender, KeyEventArgs e)
        private void textBox送料運搬料_KeyUp(object sender, KeyEventArgs e)
        {
            金額欄再表示(sender);
        }
        #endregion

        /// <summary>
        /// 「送料運搬料」にフォーカスインした場合、送料運搬料を数字のみに
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox送料運搬料_Enter(object sender, EventArgs e)
        private void textBox送料運搬料_Enter(object sender, EventArgs e)
        {
            TextBox tb = (sender as TextBox);
            //if (textBox送料運搬料.ReadOnly == true)
            if (tb.ReadOnly == true)
                return;

            try
            {
                //decimal tax = NBaseCommon.Common.金額表示を数値へ変換(textBox送料運搬料.Text);
                //textBox送料運搬料.Text = tax.ToString();
                decimal tax = NBaseCommon.Common.金額表示を数値へ変換(tb.Text);
                tb.Text = tax.ToString();
            }
            catch
            {
            }

        }
        #endregion

        /// <summary>
        /// 「送料運搬料」からフォーカスアウトした場合、送料運搬料を金額フォーマットへ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox送料運搬料_Leave(object sender, EventArgs e)
        private void textBox送料運搬料_Leave(object sender, EventArgs e)
        {
            try
            {
                //decimal carriage = decimal.Parse(textBox送料運搬料.Text);
                //textBox送料運搬料.Text = NBaseCommon.Common.金額出力(carriage);
                decimal carriage = decimal.Parse((sender as TextBox).Text);
                textBox送料運搬料.Text = NBaseCommon.Common.金額出力(carriage);
                textBox送料運搬料2.Text = NBaseCommon.Common.金額出力(carriage);
            }
            catch
            {
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
            // 処理前の位置を保持する
            Point orgPoint = treeListView.GetScrollPos();

            OdMkItem odMkItem = new OdMkItem();
            Form form = new 品目編集Form((int)品目編集Form.EDIT_MODE.新規, 対象見積回答.MsThiIraiSbtID, 対象見積回答.MsVesselID, ref odMkItem, 1);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            Item見積回答品目 追加品目 = new Item見積回答品目();
            追加品目.品目 = odMkItem;
            foreach (OdMkShousaiItem shousaiItem in odMkItem.OdMkShousaiItems)
            {
                追加品目.詳細品目s.Add(shousaiItem);
            }

            //見積回答品目s.Add(追加品目);
            //品目TreeList.AddNodes(追加品目);
            // 2009.09.15 ヘッダ対応　↑　コメント
            // 2009.09.15 ヘッダ対応　↓　コード置き換え
            int insertPos = 0;
            bool sameHeader = false;
            foreach (Item見積回答品目 品目 in 見積回答品目s)
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
            if (insertPos >= 見積回答品目s.Count)
            {
                見積回答品目s.Add(追加品目);
            }
            else
            {
                見積回答品目s.Insert(insertPos, 追加品目);
            }
            品目TreeList.NodesClear();
            品目TreeList.AddNodes(true, 見積回答品目s);

            金額欄再表示(null);

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
            // 処理前の位置を保持する
            Point orgPoint = treeListView.GetScrollPos();

            if (品目TreeList.DoubleClick(対象見積回答, ref 見積回答品目s, ref 削除回答品目s) == true)
            {
                金額欄再表示(null);

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
                    List<Item見積回答品目> 入渠品目s = Item見積回答品目.ConvertRecords(対象手配依頼.MsVesselID, odThiItems);

                    見積回答品目s.AddRange(入渠品目s);

                    品目TreeList.NodesClear();
                    品目TreeList.AddNodes(true, 見積回答品目s);
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
                    List<Item見積回答品目> 品目s = Item見積回答品目.ConvertRecords(対象手配依頼.MsVesselID, odThiItems);

                    見積回答品目s.AddRange(品目s);

                    品目TreeList.NodesClear();
                    品目TreeList.AddNodes(true, 品目s);
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


            this.Text = NBaseCommon.Common.WindowTitle("JM040401", "新規発注", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            //=========================================
            // 手配依頼の内容を初期化
            //=========================================
            textBox手配依頼種.Text = 対象手配依頼.ThiIraiSbtName;
            textBox手配依頼詳細種別.Text = 対象手配依頼.ThiIraiShousaiName;
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

            try
            {
                dateTimePicker納期.Text = 対象見積回答.Nouki.ToShortDateString();
            }
            catch
            {
            }
            textBox工期.Text = 対象見積回答.Kouki;

            textBox見積値引き.Text = NBaseCommon.Common.金額出力(対象見積回答.MkAmount);
            textBox見積値引き2.Text = NBaseCommon.Common.金額出力(対象見積回答.MkAmount);
           
            textBox送料運搬料.Text = NBaseCommon.Common.金額出力(対象見積回答.Carriage);
            textBox送料運搬料2.Text = NBaseCommon.Common.金額出力(対象見積回答.Carriage);
           
            InitItemTreeListView();

            金額欄再表示(null);

            // 画面のコンポーネントの表示/非表示
            if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
            {
                label手配依頼詳細種別.Visible = true;
                textBox手配依頼詳細種別.Visible = true;
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
            else
            {
                label手配依頼詳細種別.Visible = false;
                textBox手配依頼詳細種別.Visible = false;
                panel入渠.Visible = false;
                
                // 2012.02 -->
                button読込.Visible = false;
                button出力.Visible = false;
                //button読込.Text = "船用品注文書読込";
                //button出力.Text = "船用品注文書出力";

                //button読込.Width = 112;
                //button出力.Width = 112;

                //button読込.Visible = true;
                //button出力.Visible = true;
                // <-- 2012.02 
            }

            button注文書出力.Enabled = true;

            InitPaneSize();
        }
        #endregion

        /// <summary>
        /// 「品目/詳細品目一覧」初期化
        /// </summary>
        #region private void InitItemTreeListView()
        private void InitItemTreeListView()
        {
            if (品目TreeList != null)
                品目TreeList.Clear();

            int noColumIndex = 1;
            {
                見積回答品目s = Item見積回答品目.GetRecords(対象見積回答.OdMkID);
                削除回答品目s = new List<Item見積回答品目>();
                noColumIndex = 0;
                object[,] columns = new object[,] {
                                                   {"No", 85, null, HorizontalAlignment.Right},
                                                   {"区分 /　仕様・型式 /　詳細品目", 275, null, null},
                                                   {"単位", 45, null, null},
                                                   {"発注数", 52, null, HorizontalAlignment.Right},
                                                   {"単価", 65, null, HorizontalAlignment.Right},
                                                   {"金額", 80, null, HorizontalAlignment.Right},
                                                   {"備考（品名、規格等）", 200, null, null}
                                                 };

                品目TreeList = new ItemTreeListView新規発注(treeListView);
                品目TreeList.SetColumns(noColumIndex, columns);
                品目TreeList.Enabled = true;
                品目TreeList.AddNodes(見積回答品目s);
            }
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
                errMessage += "・発注先を選択してください。\n";
            }
            // 担当者
            string tantou = comboBox担当者.Text;
            if (tantou == null || tantou.Length == 0)
            {
                errMessage += "・担当者を入力してください。\n";
            }
            // 納期
            DateTime nouki = DateTime.MinValue;
            try
            {
                nouki = DateTime.Parse(dateTimePicker納期.Text);
            }
            catch
            {
                errMessage += "・納期が不正です。\n";
                return false;
            }
            // 工期
            string kouki = null;
            try
            {
                kouki = textBox工期.Text;
            }
            catch
            {
            }
            // 見積値引き
            decimal nebiki = 0;
            try
            {
                nebiki = NBaseCommon.Common.金額表示を数値へ変換(textBox見積値引き.Text);
            }
            catch
            {
            }
            // 消費税
            decimal tax = 0;
            try
            {
                tax = NBaseCommon.Common.金額表示を数値へ変換(textBox消費税.Text);
            }
            catch
            {
            }
            // 送料・運搬料
            decimal carriage = 0;
            try
            {
                carriage = NBaseCommon.Common.金額表示を数値へ変換(textBox送料運搬料.Text);
            }
            catch
            {
            }
            // 見積金額
            decimal kingaku = 見積金額();
            //if (kingaku + tax < nebiki)
            if (kingaku + tax + carriage < nebiki)
            {
                errMessage += "・金額が不正です。\n";
                return false;
            }
            // 品目/詳細品目
            int shousaiCount = 0;
            foreach (Item見積回答品目 見積回答品目 in 見積回答品目s)
            {
                if (見積回答品目.品目.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                {
                    continue;
                }
                foreach (OdMkShousaiItem 詳細品目 in 見積回答品目.詳細品目s)
                {
                    if (詳細品目.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        continue;
                    }
                    shousaiCount++;
                }
            }
            if (shousaiCount == 0)
            {
                errMessage += "・仕様・型式/詳細品目がない場合、発注は行えません。\n";
            }
            if (errMessage.Length > 0)
            {
                errMessage = "入力項目に不備があります。\n====================================\n" + errMessage;
                MessageBox.Show(errMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (tax == 0)
            {
                if (MessageBox.Show("消費税が入力されていないか、０が入力されています。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return false;
                }
            }


            //=========================================================================
            // 手配依頼にセットする
            //=========================================================================
            if (thiUser != null)
            {
                対象手配依頼.ThiUserID = thiUser.MsUserID;
            }
            対象手配依頼.ThiIraiDate = DateTime.Now;
            対象手配依頼.Basho = basho;
            if (jimTantou != null)
            {
                対象手配依頼.JimTantouID = jimTantou.MsUserID;
            }
            対象手配依頼.MsThiIraiStatusID = Hachu.Common.CommonDefine.MsThiIraiStatus_発注済;
            対象手配依頼.Naiyou = tehaiNaiyou;
            対象手配依頼.Bikou = bikou;
            対象手配依頼.TehaiIraiNo = 手配依頼番号を振る();

            //=========================================================================
            // 見積依頼にセットする
            //=========================================================================
            if (shrJouken != null)
                対象見積依頼.MsShrJoukenID = shrJouken.MsShrJoukenID;
            対象見積依頼.Okurisaki = okurisaki;
            if (nyukyoKamoku != null)
                対象見積依頼.MsNyukyoKamokuID = nyukyoKamoku.MsNyukyoKamokuID;
            対象見積依頼.Naiyou = naiyou;

            //=========================================================================
            // 見積回答にセットする
            //=========================================================================
            対象見積回答.MsCustomerID = customer.MsCustomerID;
            対象見積回答.MsCustomerName = customer.CustomerName;
            対象見積回答.Tantousha = tantou;
            対象見積回答.HachuNo = "0";  // Null不可なので？
            対象見積回答.Nouki = nouki;
            対象見積回答.Kouki = kouki;
            対象見積回答.MkAmount = nebiki;
            対象見積回答.Tax = tax;
            対象見積回答.Amount = kingaku;
            対象見積回答.Kiboubi = nouki; // 希望日に納期をセットしておく
            対象見積回答.Carriage = carriage;

            // 2011.02.24
            対象見積回答.MsNyukyoKamokuID = 対象見積依頼.MsNyukyoKamokuID;
         
            return true;
        }
        #endregion

        /// <summary>
        /// 発注処理
        /// </summary>
        /// <returns></returns>
        #region private bool 発注処理()
        private bool 発注処理()
        {
            try
            {
                if (入力情報の取得確認() == false)
                {
                    return false;
                }

                対象受領 = new OdJry();
                bool ret = false;
                ret = 新規発注更新処理.新規発注から発注(ref 対象手配依頼, ref 対象見積依頼, ref 対象見積回答, ref 対象受領, 見積回答品目s);
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
        /// 一時保存処理
        /// </summary>
        /// <returns></returns>
        #region private bool 一時保存処理()
        private bool 一時保存処理()
        {
            try
            {
                if (入力情報の取得確認() == false)
                {
                    return false;
                }

                bool ret = false;
                ret = 新規発注更新処理.一時保存(ref 対象手配依頼, ref 対象見積依頼, ref 対象見積回答, 見積回答品目s);
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
        /// 「品目リスト」上の詳細品目の金額が入力されたときに
        /// 「見積合計」「消費税」「合計金額」を再計算、再表示する
        /// </summary>
        #region public void 金額欄再表示(object sender)
        public void 金額欄再表示(object sender)
        {
            decimal kingaku = 見積金額();
            textBox見積金額.Text = NBaseCommon.Common.金額出力(kingaku);
            textBox見積金額2.Text = NBaseCommon.Common.金額出力(kingaku);

            decimal nebiki = 0;
            try
            {
                if (sender != null && (sender is TextBox) && (sender as TextBox) == textBox見積値引き2)
                {
                    nebiki = NBaseCommon.Common.金額表示を数値へ変換(textBox見積値引き2.Text);
                }
                else
                {
                    nebiki = NBaseCommon.Common.金額表示を数値へ変換(textBox見積値引き.Text);
                }
            }
            catch
            {
            }
            decimal tax = 0;
            try
            {
                if (sender != null && (sender is TextBox) && (sender as TextBox) == textBox消費税2)
                {
                    tax = NBaseCommon.Common.金額表示を数値へ変換(textBox消費税2.Text);
                }
                else
                {
                    tax = NBaseCommon.Common.金額表示を数値へ変換(textBox消費税.Text);
                }
            }
            catch
            {
            }
            decimal carriage = 0;
            try
            {
                if (sender != null && (sender is TextBox) && (sender as TextBox) == textBox送料運搬料2)
                {
                    carriage = NBaseCommon.Common.金額表示を数値へ変換(textBox送料運搬料2.Text);
                }
                else
                {
                    carriage = NBaseCommon.Common.金額表示を数値へ変換(textBox送料運搬料.Text);
                }
            }
            catch
            {
            }
            // 2010.01.26:aki W090228
            //textBox合計金額.Text = NBaseCommon.Common.金額出力(kingaku + tax - nebiki);
            // 2012.03 
            //textBox合計金額.Text = NBaseCommon.Common.金額出力(kingaku - nebiki);
            textBox合計金額.Text = NBaseCommon.Common.金額出力(kingaku - nebiki + carriage);
            textBox合計金額2.Text = NBaseCommon.Common.金額出力(kingaku - nebiki + carriage);

            textBox合計金額_税込.Text = NBaseCommon.Common.金額出力(kingaku - nebiki + carriage + tax);
            textBox合計金額_税込2.Text = NBaseCommon.Common.金額出力(kingaku - nebiki + carriage + tax);
        }
        #endregion

        /// <summary>
        /// 「品目リスト」上の詳細品目の金額合計を返す
        /// </summary>
        /// <returns></returns>
        #region public decimal 見積金額()
        public decimal 見積金額()
        {
            return 品目TreeList.見積合計();
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

        private bool Validate単価()
        {
            if (!isValidate対象種別())
                return true;

            foreach (Item見積回答品目 見積回答品目 in 見積回答品目s)
            {
                if (!isValidate対象仕様型式(見積回答品目))
                    continue;

                foreach (OdMkShousaiItem 詳細品目 in 見積回答品目.詳細品目s)
                {
                    if (詳細品目.Count > 0 && 詳細品目.Tanka == 0)
                    {
                        // 数量が入力されていて、単価が "０" の場合、無効としたい
                        MessageBox.Show("[" + 詳細品目.ShousaiItemName + "]の単価が入力されていません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }

            return true;
        }
        private bool isValidate対象種別()
        {
            if (対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                return true;

            if (対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
                return true;

            return false;
        }
        private bool isValidate対象仕様型式(Item見積回答品目 品目)
        {
            if (対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                if (品目.品目.ItemName == NBaseCommon.Common.MsLo_LO_String)
                    return true;
                else
                    return false;
            }
            else if (対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_船用品ID)
            {
                if (品目.品目.ItemName == MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント.ToString())
                    return true;
                else
                    return false;
            }
            else
            {
                return true;
            }
        }

        //======================================================================
        // パネルの開閉
        //======================================================================
        #region
        private void InitPaneSize()
        {
            //if (対象見積回答.MkNo == null || 対象見積回答.MkNo.Length == 0)
            //{
            //    tableLayoutPanel1.RowStyles[1].Height = panelMaxHeight;
            //    button_OpenClose.Text = "▲";
            //}
            //else
            //{
            //    tableLayoutPanel1.RowStyles[1].Height = panelMinHeight;
            //    button_OpenClose.Text = "▼";
            //}
            tableLayoutPanel1.RowStyles[2].Height = panelMaxHeight1;
            tableLayoutPanel1.RowStyles[3].Height = panelMinHeight;
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
                tableLayoutPanel1.RowStyles[2].Height = panelMaxHeight1;
                tableLayoutPanel1.RowStyles[3].Height = panelMinHeight;
                button_OpenClose.Text = "▲";
            }
            else
            {
                tableLayoutPanel1.RowStyles[2].Height = panelMinHeight;
                tableLayoutPanel1.RowStyles[3].Height = panelMaxHeight2;
                button_OpenClose.Text = "▼";
            }
        }
        #endregion
    }
}
