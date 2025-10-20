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


namespace Hachu.HachuManage
{
    public partial class 落成Form : BaseUserControl//2021/07/12 BaseForm
    {
        private OdMk 対象見積回答;
        private OdJry 対象受領;
        
        /// <summary>
        /// 対象支払
        /// </summary>
        private OdShr 対象支払;

        /// <summary>
        /// 対象支払の品目
        /// </summary>
        private List<Item支払品目> 支払品目s;

        /// <summary>
        /// 削除とマークされた品目
        /// </summary>
        private List<Item支払品目> 削除支払品目s;

        /// <summary>
        /// 
        /// </summary>
        ItemTreeListView支払 品目TreeList;

        /// <summary>
        /// 
        /// </summary>
        private int ステータスはそのまま = -1;

        private decimal preNebiki;
        private decimal preTax;

        //private float panelMinHeight = 36;
        //private float panelMaxHeight = 339 + 6;
        private float panelMinHeight = 0;
        private float panelMaxHeight = 201;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <param name="info"></param>
        #region public 落成Form(int windowStyle, ListInfo支払 info)
        public 落成Form(int windowStyle, ListInfo支払 info)
        {
            InitializeComponent();

            this.WindowStyle = windowStyle;
            対象受領 = info.parent;
            対象支払 = info.info;
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
        #region private void 支払Form_Load(object sender, EventArgs e)
        private void 支払Form_Load(object sender, EventArgs e)
        {
            //=========================================
            // ＤＢから必要な情報を取得する
            //=========================================
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (対象受領 == null)
                {
                    対象受領 = serviceClient.OdJry_GetRecord(NBaseCommon.Common.LoginUser, 対象支払.OdJryID);
                }
                if (対象見積回答 == null)
                {
                    対象見積回答 = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, 対象受領.OdMkID);
                }
            }
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
            Formを閉じる();
        }
        #endregion
        
        /// <summary>
        /// 「落成」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button落成_Click(object sender, EventArgs e)
        private void button落成_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("落成をします。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            OdShr 支払 = new OdShr();
            if (落成処理(ref 支払) == false)
            {
                return;
            }

            MessageBox.Show("落成をしました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ListInfo支払 info = new ListInfo支払();
            info.parent = 対象受領;
            info.info = 対象支払;
            InfoUpdating(info);

            ListInfo支払 newInfo = new ListInfo支払();
            newInfo.AddNode = true;
            newInfo.parent = 対象受領;
            newInfo.info = 支払;
            InfoUpdating(newInfo);

            Formに情報をセットする();
        }
        #endregion

        /// <summary>
        /// 「承認依頼」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button承認依頼_Click(object sender, EventArgs e)
        private void button承認依頼_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("この落成見積で承認を依頼します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (更新処理(対象支払.OdStatusValue.Values[(int)OdShr.STATUS.落成承認依頼中].Value) == false)
            {
                return;
            }

            MessageBox.Show("承認を依頼しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ListInfo支払 info = new ListInfo支払();
            info.parent = 対象受領;
            info.info = 対象支払;
            InfoUpdating(info);

            Formに情報をセットする();
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
            if (更新処理(ステータスはそのまま) == false)
            {
                return;
            }

            ListInfo支払 info = new ListInfo支払();
            info.parent = 対象受領;
            info.info = 対象支払;
            InfoUpdating(info);

            MessageBox.Show("更新しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            InitItemTreeListView();
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
            if (MessageBox.Show("この落成を取消します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // 取消し処理
            if (取消処理() == false)
            {
                return;
            }
            MessageBox.Show("取消しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ListInfo支払 info = new ListInfo支払();
            info.Remove = true;
            info.parent = 対象受領;
            info.info = 対象支払;
            InfoUpdating(info);

            // Formを閉じる
            //---------コメントアウト2021/07/12-----------
            //Close();
        }
        #endregion

        /// <summary>
        /// 「承認」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button承認_Click(object sender, EventArgs e)
        private void button承認_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("この落成見積を承認します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (更新処理(対象支払.OdStatusValue.Values[(int)OdShr.STATUS.落成承認済み].Value) == false)
            {
                return;
            }

            ListInfo支払 info = new ListInfo支払();
            info.Remove = true;
            info.parent = null;
            info.info = 対象支払;
            InfoUpdating(info);

            MessageBox.Show("承認しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Formを閉じる();
        }
        #endregion

        /// <summary>
        /// 「差戻し」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button差戻し_Click(object sender, EventArgs e)
        private void button差戻し_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("この落成見積を差戻します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (更新処理(対象支払.OdStatusValue.Values[(int)OdShr.STATUS.未落成].Value) == false)
            {
                return;
            }

            ListInfo支払 info = new ListInfo支払();
            info.Remove = true;
            info.parent = null;
            info.info = 対象支払;
            InfoUpdating(info);

            MessageBox.Show("差戻しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Formを閉じる();
        }
        #endregion

        /// <summary>
        /// 「請求値引き」の入力キーを制限する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox請求値引き_KeyPress(object sender, KeyPressEventArgs e)
        private void textBox請求値引き_KeyPress(object sender, KeyPressEventArgs e)
        {
            preNebiki = 0;
            try
            {
                preNebiki = NBaseCommon.Common.金額表示を数値へ変換(textBox請求値引き.Text);
            }
            catch
            {
            }
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        #endregion

        /// <summary>
        /// 「請求値引き」の入力後、金額欄の再計算、再表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox請求値引き_KeyUp(object sender, KeyEventArgs e)
        private void textBox請求値引き_KeyUp(object sender, KeyEventArgs e)
        {
            if (金額入力確認(請求合計金額(), "落成額", textBox請求値引き, "請求値引き") == false)
            {
                textBox請求値引き.Text = preTax.ToString();
                textBox請求値引き.SelectionStart = textBox請求値引き.Text.Length;
                e.Handled = false;
                return;
            }
            金額欄再表示();
        }
        #endregion

        /// <summary>
        /// 「請求値引き」にフォーカスインした場合、値引き額を数字のみに
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox請求値引き_Enter(object sender, EventArgs e)
        private void textBox請求値引き_Enter(object sender, EventArgs e)
        {
            if (textBox請求値引き.ReadOnly == true)
                return;

            try
            {
                decimal nebiki = NBaseCommon.Common.金額表示を数値へ変換(textBox請求値引き.Text);
                textBox請求値引き.Text = nebiki.ToString();
            }
            catch
            {
            }
        }
        #endregion

        /// <summary>
        /// 「請求値引き」からフォーカスアウトした場合、値引き額を金額フォーマットへ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox請求値引き_Leave(object sender, EventArgs e)
        private void textBox請求値引き_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal nebiki = decimal.Parse(textBox請求値引き.Text);
                textBox請求値引き.Text = NBaseCommon.Common.金額出力(nebiki);
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
        #region private void textBox消費税_支払_KeyPress(object sender, KeyPressEventArgs e)
        private void textBox消費税_支払_KeyPress(object sender, KeyPressEventArgs e)
        {
            preTax = 0;
            try
            {
                preTax = NBaseCommon.Common.金額表示を数値へ変換(textBox消費税_支払.Text);
            }
            catch
            {
            }
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
        #region private void textBox消費税_支払_KeyUp(object sender, KeyEventArgs e)
        private void textBox消費税_支払_KeyUp(object sender, KeyEventArgs e)
        {
            if (金額入力確認(請求合計金額(), "落成額", textBox消費税_支払, "消費税") == false)
            {
                textBox消費税_支払.Text = preTax.ToString();
                textBox消費税_支払.SelectionStart = textBox消費税_支払.Text.Length;
                e.Handled = false;
                return;
            }
            金額欄再表示();
        }
        #endregion

        /// <summary>
        /// 「消費税」にフォーカスインした場合、消費税を数字のみに
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox消費税_支払_Enter(object sender, EventArgs e)
        private void textBox消費税_支払_Enter(object sender, EventArgs e)
        {
            if (textBox消費税_支払.ReadOnly == true)
                return;

            try
            {
                decimal tax = NBaseCommon.Common.金額表示を数値へ変換(textBox消費税_支払.Text);
                textBox消費税_支払.Text = tax.ToString();
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
        #region private void textBox消費税_支払_Leave(object sender, EventArgs e)
        private void textBox消費税_支払_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal tax = decimal.Parse(textBox消費税_支払.Text);
                textBox消費税_支払.Text = NBaseCommon.Common.金額出力(tax);
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
            金額欄再表示();
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
            if (textBox送料運搬料_支払.ReadOnly == true)
                return;

            try
            {
                decimal tax = NBaseCommon.Common.金額表示を数値へ変換(textBox送料運搬料_支払.Text);
                textBox送料運搬料_支払.Text = tax.ToString();
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
                decimal carriage = decimal.Parse(textBox送料運搬料_支払.Text);
                textBox送料運搬料_支払.Text = NBaseCommon.Common.金額出力(carriage);
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
            if (対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                燃料潤滑油編集Form form = new 燃料潤滑油編集Form(支払品目s);
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

                OdShrItem odShrItem = new OdShrItem();
                Form form = new 品目編集Form((int)品目編集Form.EDIT_MODE.新規, 対象支払.Sbt, 対象見積回答.MsThiIraiSbtID, 対象見積回答.MsVesselID, ref odShrItem);
                if (form.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                Item支払品目 追加品目 = new Item支払品目();
                追加品目.品目 = odShrItem;
                foreach (OdShrShousaiItem shousaiItem in odShrItem.OdShrShousaiItems)
                {
                    追加品目.詳細品目s.Add(shousaiItem);
                }

                //支払品目s.Add(追加品目);
                //品目TreeList.AddNodes(追加品目);
                // 2009.09.15 ヘッダ対応　↑　コメント
                // 2009.09.15 ヘッダ対応　↓　コード置き換え
                int insertPos = 0;
                bool sameHeader = false;
                foreach (Item支払品目 品目 in 支払品目s)
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
                if (insertPos >= 支払品目s.Count)
                {
                    支払品目s.Add(追加品目);
                }
                else
                {
                    支払品目s.Insert(insertPos, 追加品目);
                }
                品目TreeList.NodesClear();
                品目TreeList.AddNodes(true, 支払品目s);

                金額欄再表示();

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
            if (対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                return;

            // 処理前の位置を保持する
            Point orgPoint = treeListView.GetScrollPos();

            if (品目TreeList.DoubleClick(対象支払, 対象見積回答.MsThiIraiSbtID, 対象見積回答.MsVesselID, ref 支払品目s, ref 削除支払品目s) == true)
            {
                金額欄再表示();

                // 処理前の位置をセットする
                treeListView.SetScrollPos(orgPoint);
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
            OdThi thi = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                thi = serviceClient.OdThi_GetRecordByOdJryID(NBaseCommon.Common.LoginUser, 対象受領.OdJryID);
            }

            //=========================================
            // 対象支払の内容を画面にセットする
            //=========================================
            #region Windowタイトル
            this.Text = NBaseCommon.Common.WindowTitle("JM040801", "落成", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            #endregion
            #region 入力エリア
            label状況.Text = 対象支払.StatusName;
            textBox船.Text = 対象見積回答.MsVesselName;
            textBox手配依頼者.Text = thi.ThiUserName;
            textBox事務担当者.Text = thi.JimTantouName;
            textBox発注先.Text = 対象見積回答.MsCustomerName;
            textBox担当者.Text = 対象見積回答.Tantousha;
            textBox納期.Text = 対象見積回答.Nouki.ToShortDateString();
            textBox工期.Text = 対象見積回答.Kouki;
            textBox手配内容.Text = 対象支払.Naiyou;
            textBox備考.Text = 対象支払.Bikou;

            if (対象支払.ShrDate != DateTime.MinValue)
            {
                textBox支払日.Text = 対象支払.ShrDate.ToShortDateString();
            }
            else
            {
                textBox支払日.Text = "";
            }
            #endregion
            #region 品目/詳細品目TreeList
            InitItemTreeListView();
            #endregion
            #region 金額エリア
            textBox納品額.Text = NBaseCommon.Common.金額出力(対象受領.Amount);
            textBox消費税_納品.Text = NBaseCommon.Common.金額出力(対象受領.Tax);
            textBox送料運搬料_納品.Text = NBaseCommon.Common.金額出力(対象受領.Carriage);
            textBox見積値引き_納品.Text = NBaseCommon.Common.金額出力(対象受領.NebikiAmount);
            // 2010.01.25:aki  W090228
            //textBox合計金額_納品.Text = NBaseCommon.Common.金額出力(対象受領.Amount + 対象受領.Tax - 対象受領.NebikiAmount);
            //textBox合計金額_納品.Text = NBaseCommon.Common.金額出力(対象受領.Amount - 対象受領.NebikiAmount);
            textBox合計金額_納品.Text = NBaseCommon.Common.金額出力(対象受領.Amount - 対象受領.NebikiAmount + 対象受領.Carriage);
            textBox合計金額_税込_納品.Text = NBaseCommon.Common.金額出力(対象受領.Amount - 対象受領.NebikiAmount + 対象受領.Tax + 対象受領.Carriage);

            textBox消費税_支払.Text = NBaseCommon.Common.金額出力(対象支払.Tax);
            textBox送料運搬料_支払.Text = NBaseCommon.Common.金額出力(対象支払.Carriage);
            textBox請求値引き.Text = NBaseCommon.Common.金額出力(対象支払.NebikiAmount);        

            金額欄再表示();
            #endregion

            #region 画面のコンポーネントの表示/非表示
            if (WindowStyle == (int)WINDOW_STYLE.通常)
            {
                button承認.Visible = false;
                button差戻し.Visible = false;

                button落成.Visible = false;
                button承認依頼.Visible = false;

                button更新.Visible = false;
                button取消.Visible = false;

                textBox消費税_支払.ReadOnly = true;
                textBox送料運搬料_支払.ReadOnly = true;
                textBox請求値引き.ReadOnly = true;
                button品目編集.Enabled = false;
                品目TreeList.Enabled = false;
                品目TreeList.editable = false;

                if (対象支払.Status == 対象支払.OdStatusValue.Values[(int)OdShr.STATUS.未落成].Value)
                {
                    button承認依頼.Visible = true;

                    button更新.Visible = true;
                    button取消.Visible = true;

                    textBox消費税_支払.ReadOnly = false;
                    textBox送料運搬料_支払.ReadOnly = false;
                    textBox請求値引き.ReadOnly = false;
                    button品目編集.Enabled = true;
                    品目TreeList.Enabled = true;
                    品目TreeList.editable = true;
                }
                else if (対象支払.Status == 対象支払.OdStatusValue.Values[(int)OdShr.STATUS.落成承認依頼中].Value)
                {
                }
                else if (対象支払.Status == 対象支払.OdStatusValue.Values[(int)OdShr.STATUS.落成承認済み].Value)
                {
                    button落成.Visible = true;
                }
                else if (対象支払.Status == 対象支払.OdStatusValue.Values[(int)OdShr.STATUS.落成済み].Value)
                {
                    // 2011.02.25
                    // 支払合算のため、取消をOKとする
                    button取消.Visible = true;
                }
            }
            else if (WindowStyle == (int)WINDOW_STYLE.承認)
            {
                button承認.Visible = true;
                button差戻し.Visible = true;

                button落成.Visible = false;
                button承認依頼.Visible = false;
                button更新.Visible = false;
                button取消.Visible = false;

                textBox消費税_支払.ReadOnly = true;
                textBox送料運搬料_支払.ReadOnly = true;
                textBox請求値引き.ReadOnly = true;
                button品目編集.Enabled = false;
                品目TreeList.Enabled = false;
            }
            else if (WindowStyle == (int)WINDOW_STYLE.概算)
            {
                button承認.Visible = false;
                button差戻し.Visible = false;

                button落成.Visible = false;
                button承認依頼.Visible = false;
                button更新.Visible = false;
                button取消.Visible = false;

                textBox消費税_支払.ReadOnly = true;
                textBox送料運搬料_支払.ReadOnly = true;
                textBox請求値引き.ReadOnly = true;
                button品目編集.Enabled = false;
                品目TreeList.Enabled = false;
            }
            #endregion

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
            {
                品目TreeList.Clear();
            }
            else
            {
                // 2014.02 2013年度改造 ==>
                //品目TreeList = new ItemTreeListView支払(treeListView);
                品目TreeList = new ItemTreeListView支払(対象見積回答.MsThiIraiSbtID, treeListView);
                if (対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                {
                    品目TreeList.Enum表示方式 = ItemTreeListView支払.表示方式enum.Zero以外を表示;
                }
                // <==
            }

            int noColumIndex = 0;
            bool viewHeader = false;
            object[,] columns = null;

            支払品目s = Item支払品目.GetRecords(対象支払.OdShrID);
            削除支払品目s = new List<Item支払品目>();

            if (対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                noColumIndex = 0;
                viewHeader = false;
                columns = new object[,] {
                                       {"No", 60, null, HorizontalAlignment.Right},
                                       {"仕様・型式 /　詳細品目", 275, null, null},
                                       {"単位", 45, null, null},
                                       {"数量", 50, null, HorizontalAlignment.Right},
                                       {"単価", 90, null, HorizontalAlignment.Right},
                                       {"金額", 100, null, HorizontalAlignment.Right},
                                       {"備考（品名、規格等）", 200, null, null}
                                     };
            }
            else
            {
                noColumIndex = 0;
                viewHeader = true;
                columns = new object[,] {
                                       {"No", 85, null, HorizontalAlignment.Right},
                                       {"区分 /　仕様・型式 /　詳細品目", 275, null, null},
                                       {"単位", 45, null, null},
                                       {"数量", 50, null, HorizontalAlignment.Right},
                                       {"単価", 90, null, HorizontalAlignment.Right},
                                       {"金額", 100, null, HorizontalAlignment.Right},
                                       {"備考（品名、規格等）", 200, null, null}
                                     };

            }
            品目TreeList.SetColumns(noColumIndex, columns);
            品目TreeList.TextChangeEvent += new ItemTreeListView支払.TextChangeEventHandler(金額欄再表示);
            if (WindowStyle != (int)WINDOW_STYLE.通常)
            {
                品目TreeList.editable = false;
            }
            if (対象支払.Status != 対象支払.OdStatusValue.Values[(int)OdShr.STATUS.未落成].Value)
            {
                品目TreeList.editable = false;
            }
            品目TreeList.AddNodes(viewHeader, 支払品目s);
        }
        #endregion

        /// <summary>
        /// Formを閉じる
        /// </summary>
        #region private void Formを閉じる(
        private void Formを閉じる()
        {
            //---------コメントアウト2021/07/12-----------
            //BaseFormClosing();
            //Close();
        }
        #endregion

        /// <summary>
        /// 入力情報の取得確認
        /// </summary>
        /// <returns></returns>
        #region private bool 入力情報の取得確認(int setStatus)
        private bool 入力情報の取得確認(int setStatus)
        {
            string errMessage = "";

            // 値引き
            decimal nebiki = 0;
            try
            {
                nebiki = NBaseCommon.Common.金額表示を数値へ変換(textBox請求値引き.Text);
            }
            catch
            {
            }
            // 消費税
            decimal tax = 0;
            try
            {
                tax = NBaseCommon.Common.金額表示を数値へ変換(textBox消費税_支払.Text);
            }
            catch
            {
            }
            // 送料・運搬料
            decimal carriage = 0;
            try
            {
                carriage = NBaseCommon.Common.金額表示を数値へ変換(textBox送料運搬料_支払.Text);
            }
            catch
            {
            }
            // 金額
            decimal kingaku = 請求合計金額();
            //if (kingaku + tax < nebiki)
            if (kingaku + tax + carriage < nebiki)
            {
                errMessage += "・金額が不正です。\n";
            }
            // 品目/詳細品目
            if (対象支払.Status == 対象支払.OdStatusValue.Values[(int)OdShr.STATUS.未落成].Value && setStatus != ステータスはそのまま)
            {
                int shousaiCount = 0;
                foreach (Item支払品目 支払品目 in 支払品目s)
                {
                    if (支払品目.品目.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        continue;
                    }
                    foreach (OdShrShousaiItem 詳細品目 in 支払品目.詳細品目s)
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
                    errMessage += "・仕様・型式/詳細品目がない場合、承認依頼は行えません。\n";
                }
            }

            if (errMessage.Length > 0)
            {
                errMessage = "入力項目に不備があります。\n====================================\n" + errMessage;
                MessageBox.Show(errMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (setStatus != ステータスはそのまま)
            {
                if (対象支払.Status == 対象支払.OdStatusValue.Values[(int)OdShr.STATUS.未落成].Value)
                {
                    if (tax == 0)
                    {
                        if (MessageBox.Show("消費税が入力されていないか、０が入力されています。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return false;
                        }
                    }
                }

                if (setStatus == 対象支払.OdStatusValue.Values[(int)OdShr.STATUS.落成済み].Value)
                {
                    対象支払.ShrDate = DateTime.Now;
                }
                対象支払.Status = setStatus;
            }
            対象支払.Amount = kingaku;
            対象支払.NebikiAmount = nebiki;
            対象支払.Tax = tax;
            対象支払.Carriage = carriage;

            return true;
        }
        #endregion

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <returns></returns>
        #region private bool 更新処理(int setStatus)
        private bool 更新処理(int setStatus)
        {
            try
            {
                if (入力情報の取得確認(setStatus) == false)
                {
                    return false;
                }
                bool ret = 支払更新処理.更新(ref 対象支払, 支払品目s, 削除支払品目s);
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
        /// 落成処理     
        /// </summary>
        /// <param name="支払"></param>
        /// <returns></returns>
        #region private bool 落成処理(ref OdShr 支払)
        private bool 落成処理(ref OdShr 支払)
        {
            try
            {
                int setStatus = 対象支払.OdStatusValue.Values[(int)OdShr.STATUS.落成済み].Value;
                if (入力情報の取得確認(setStatus) == false)
                {
                    return false;
                }
                対象支払.Status = setStatus;
                支払.ShrNo = 対象受領.JryNo;
                bool ret = 支払更新処理.落成(ref 支払, ref 対象支払, 支払品目s);
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
                bool ret = 支払更新処理.取消(ref 対象支払);
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
        /// 「品目リスト」上の詳細品目の金額が入力されたときに
        /// 「支払額」「消費税」「合計金額」を再計算、再表示する
        /// </summary>
        #region public void 金額欄再表示()
        public void 金額欄再表示()
        {
            decimal kingaku = 請求合計金額();
            textBox支払額.Text = NBaseCommon.Common.金額出力(kingaku);

            decimal nebiki = 0;
            try
            {
                nebiki = NBaseCommon.Common.金額表示を数値へ変換(textBox請求値引き.Text);
            }
            catch
            {
            }
            decimal tax = 0;
            try
            {
                tax = NBaseCommon.Common.金額表示を数値へ変換(textBox消費税_支払.Text);
            }
            catch
            {
            }
            decimal carriage = 0;
            try
            {
                carriage = NBaseCommon.Common.金額表示を数値へ変換(textBox送料運搬料_支払.Text);
            }
            catch
            {
            }

            // 2010.01.25;:aki  W090228
            //textBox合計金額_支払.Text = NBaseCommon.Common.金額出力(kingaku + tax - nebiki);
            //textBox合計金額_支払.Text = NBaseCommon.Common.金額出力(kingaku - nebiki);
            textBox合計金額_支払.Text = NBaseCommon.Common.金額出力(kingaku - nebiki + carriage);

            textBox合計金額_税込_支払.Text = NBaseCommon.Common.金額出力(kingaku - nebiki + carriage + tax);
        }
        #endregion

        /// <summary>
        /// 「品目リスト」上の詳細品目の請求金額合計を返す
        /// </summary>
        /// <returns></returns>
        #region public decimal 請求合計金額()
        public decimal 請求合計金額()
        {
            return 品目TreeList.請求合計金額();
        }
        #endregion


        //======================================================================
        // パネルの開閉
        //======================================================================
        #region
        private void InitPaneSize()
        {
            //tableLayoutPanel1.RowStyles[1].Height = panelMinHeight;
            //button_OpenClose.Text = "▼";
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


        #region private void ResetTreeListView()
        private void ResetTreeListView()
        {
            if (品目TreeList != null)
            {
                品目TreeList.Clear();
            }
            else
            {
                品目TreeList = new ItemTreeListView支払(対象見積回答.MsThiIraiSbtID, treeListView);
            }

            int noColumIndex = 0;
            bool viewHeader = false;
            object[,] columns = null;

            //if (対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            //{
                noColumIndex = 0;
                viewHeader = false;
                columns = new object[,] {
                                       {"No", 60, null, HorizontalAlignment.Right},
                                       {"仕様・型式 /　詳細品目", 275, null, null},
                                       {"単位", 45, null, null},
                                       {"数量", 50, null, HorizontalAlignment.Right},
                                       {"単価", 90, null, HorizontalAlignment.Right},
                                       {"金額", 100, null, HorizontalAlignment.Right},
                                       {"備考（品名、規格等）", 200, null, null}
                                     };
            //}
            //else
            //{
            //    noColumIndex = 0;
            //    viewHeader = true;
            //    columns = new object[,] {
            //                           {"No", 85, null, HorizontalAlignment.Right},
            //                           {"区分 /　仕様・型式 /　詳細品目", 275, null, null},
            //                           {"単位", 45, null, null},
            //                           {"数量", 50, null, HorizontalAlignment.Right},
            //                           {"単価", 90, null, HorizontalAlignment.Right},
            //                           {"金額", 100, null, HorizontalAlignment.Right},
            //                           {"備考（品名、規格等）", 200, null, null}
            //                         };

            //}
            品目TreeList.Enum表示方式 = ItemTreeListView支払.表示方式enum.Zero以外を表示;
            品目TreeList.Enabled = true;

            品目TreeList.SetColumns(noColumIndex, columns);
            品目TreeList.TextChangeEvent += new ItemTreeListView支払.TextChangeEventHandler(金額欄再表示);
            if (WindowStyle != (int)WINDOW_STYLE.通常)
            {
                品目TreeList.editable = false;
            }
            if (対象支払.Status != 対象支払.OdStatusValue.Values[(int)OdShr.STATUS.未落成].Value)
            {
                品目TreeList.editable = false;
            }
            品目TreeList.AddNodes(viewHeader, 支払品目s);
        }
        #endregion
    }
}
