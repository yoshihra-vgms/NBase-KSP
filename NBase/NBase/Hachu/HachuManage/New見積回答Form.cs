using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WingData.DAC;
using Hachu.Models;
using Hachu.Utils;
using Hachu.BLC;
using Hachu.Reports;
using WingData.DS;


namespace Hachu.HachuManage
{
    public partial class New見積回答Form : BaseForm
    {
        private OdMm 対象見積依頼;
        
        /// <summary>
        /// 対象見積回答
        /// </summary>
        private OdMk 対象見積回答;

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
        ItemTreeListView見積回答 品目TreeList;


        /// <summary>
        /// 
        /// </summary>
        private int ステータスはそのまま = -1;

        private decimal preNebiki;
        private decimal preTax;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <param name="info"></param>
        #region public New見積回答Form(int windowStyle, ListInfo見積回答 info)
        public New見積回答Form(int windowStyle, ListInfo見積回答 info)
        {
            InitializeComponent();

            this.WindowStyle = windowStyle;
            対象見積依頼 = info.parent;
            対象見積回答 = info.info;

            EnableComponents();
        }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <param name="info"></param>
        /// <param name="parent"></param>
        #region public New見積回答Form(int windowStyle, OdMk info, OdMm parent)
        public New見積回答Form(int windowStyle, OdMk info, OdMm parent)
        {
            InitializeComponent();

            this.WindowStyle = windowStyle;
            対象見積回答 = info;
            対象見積依頼 = parent;

            EnableComponents();
        }
        #endregion


        private void EnableComponents()
        {
            if (MsRoleTableCache.instance().Enabled(WingCommon.Common.LoginUser, "発注管理", "発注状況一覧", "見積回答"))
            {
                button発注.Enabled = true;
                button注文書出力.Enabled = true;
                button発注メール再送.Enabled = true;
                button発注承認依頼.Enabled = true;
                button更新.Enabled = true;
                button取消.Enabled = true;
                button再見積.Enabled = true;
                button見積依頼メール.Enabled = true;
                button合見積比較.Enabled = true;
                button差戻し.Enabled = true;
                button承認.Enabled = true;

                dateTimePicker見積回答日.Enabled = true;
                dateTimePicker納期.Enabled = true;

                textBox担当者.Enabled = true;
                textBox見積有効期限.Enabled = true;
                textBox工期.Enabled = true;
                textBox消費税.Enabled = true;
                textBox見積値引き.Enabled = true;

                button品目編集.Enabled = true;
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
        #region private void 見積回答Form_Load(object sender, EventArgs e)
        private void 見積回答Form_Load(object sender, EventArgs e)
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
            Formを閉じる();
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
            if (MessageBox.Show("発注処理をします。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            対象見積回答.HachuDate = DateTime.Now;
            対象見積回答.HachuNo = 対象見積回答.MkNo;

            OdJry 受領 = new OdJry();
            MsThiIraiStatus newStatus = new MsThiIraiStatus();
            if (受領作成処理(ref 受領, ref newStatus) == false)
            {
                return;
            }
            MessageBox.Show("発注しました。","確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            発注メール送信Form form = new 発注メール送信Form(ref 対象見積回答, 発注メール送信Form.メール送信Enum.新規送信);
            form.ShowDialog();

            if (this.MdiParent == null)
            {
                // 「アラーム情報」からの呼び出しの場合
                Close();
            }
            else
            {
                ListInfo見積回答 info = new ListInfo見積回答();
                info.parent = 対象見積依頼;
                info.info = 対象見積回答;
                InfoUpdating(info);


                受領.OdMkNouki = 対象見積回答.Nouki;

                //ListInfo見積回答 info = new ListInfo見積回答();
                info = new ListInfo見積回答();
                info.ChangeStatus = true;
                info.SetStatus = newStatus;
                info.info = 対象見積回答;
                info.child = 受領;
                info.NextStatus = true;
                InfoUpdating(info);
            }
        }
        #endregion

        /// <summary>
        /// 「再見積」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button再見積_Click(object sender, EventArgs e)
        private void button再見積_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("再見積をします。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            OdMk 見積回答 = new OdMk();
            if (見積回答作成処理(ref 見積回答) == false)
            {
                return;
            }

            ListInfo見積回答 info = new ListInfo見積回答();
            info.AddNode = true;
            info.parent = 対象見積依頼;
            info.info = 見積回答;
            InfoUpdating(info);
        }
        #endregion

        /// <summary>
        /// 「発注承認依頼」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button発注承認依頼_Click(object sender, EventArgs e)
        private void button発注承認依頼_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("この見積回答で発注承認を依頼します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (更新処理(対象見積回答.OdStatusValue.Values[(int)OdMk.STATUS.発注承認依頼中].Value) == false)
            {
                return;
            }

            MessageBox.Show("発注承認を依頼しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ListInfo見積回答 info = new ListInfo見積回答();
            info.parent = 対象見積依頼;
            info.info = 対象見積回答;
            InfoUpdating(info);

            Formに情報をセットする();
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
            if (MessageBox.Show("この発注承認を承認します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (更新処理(対象見積回答.OdStatusValue.Values[(int)OdMk.STATUS.発注承認済み].Value) == false)
            {
                return;
            }

            if (this.MdiParent == null)
            {
                // 「アラーム情報」からの呼び出しの場合
                MessageBox.Show("承認しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                ListInfo見積回答 info = new ListInfo見積回答();
                info.Remove = true;
                info.parent = null;
                info.info = 対象見積回答;
                InfoUpdating(info);

                MessageBox.Show("承認しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Formを閉じる();
            }
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
            if (MessageBox.Show("この発注承認を差戻します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (更新処理(対象見積回答.OdStatusValue.Values[(int)OdMk.STATUS.回答済み].Value) == false)
            {
                return;
            }

            if (this.MdiParent == null)
            {
                // 「アラーム情報」からの呼び出しの場合
                MessageBox.Show("差戻しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                ListInfo見積回答 info = new ListInfo見積回答();
                info.Remove = true;
                info.parent = null;
                info.info = 対象見積回答;
                InfoUpdating(info);

                MessageBox.Show("差戻しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                Formを閉じる();
            }
        }
        #endregion

        /// <summary>
        /// 「見積依頼メール」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button見積依頼メール_Click(object sender, EventArgs e)
        private void button見積依頼メール_Click(object sender, EventArgs e)
        {
            List<Item見積依頼品目> 見積品目s = Item見積依頼品目.GetRecordsByOdMkID(対象見積回答.OdMkID);
            見積依頼先設定Form form = new 見積依頼先設定Form(対象見積回答, 対象見積依頼, 見積品目s);
            if (form.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            using (WingServiceReferences.WingServer.ServiceClient serviceClient = new WingServiceReferences.WingServer.ServiceClient())
            {
                対象見積回答 = serviceClient.OdMk_GetRecord(WingCommon.Common.LoginUser, 対象見積回答.OdMkID);
                
                ListInfo見積回答 info = new ListInfo見積回答();
                info.parent = 対象見積依頼;
                info.info = 対象見積回答;
                InfoUpdating(info);

                Formに情報をセットする();
            }
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

            MessageBox.Show("更新しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (this.MdiParent == null)
            {
                // 「アラーム情報」からの呼び出しの場合
                Close();
            }
            else
            {
                ListInfo見積回答 info = new ListInfo見積回答();
                info.parent = 対象見積依頼;
                info.info = 対象見積回答;
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
            if (MessageBox.Show("この見積回答を取消します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // 取消し処理
            if (取消処理() == false)
            {
                return;
            }
            MessageBox.Show("取消しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (this.MdiParent == null)
            {
                // 「アラーム情報」からの呼び出しの場合
                Close();
            }
            else
            {
                ListInfo見積回答 info = new ListInfo見積回答();
                info.Remove = true;
                info.parent = 対象見積依頼;
                info.info = 対象見積回答;
                InfoUpdating(info);

                // Formを閉じる
                Close();
            }
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
            注文書出力Form form = new 注文書出力Form(対象見積回答);
            if (form.ShowDialog() == DialogResult.OK)
            {
                using (WingServiceReferences.WingServer.ServiceClient serviceClient = new WingServiceReferences.WingServer.ServiceClient())
                {
                    //対象見積依頼 = serviceClient.OdMm_GetRecord(WingCommon.Common.LoginUser, 対象見積依頼.OdMmID);
                    対象見積依頼 = serviceClient.OdMm_GetRecord(WingCommon.Common.LoginUser, 対象見積回答.OdMmID);
                    対象見積回答 = serviceClient.OdMk_GetRecord(WingCommon.Common.LoginUser, 対象見積回答.OdMkID);
                }
                ListInfo見積回答 info = new ListInfo見積回答();
                info.ChangeParent = true;
                info.parent = 対象見積依頼;
                info.info = 対象見積回答;
                InfoUpdating(info);

                Formに情報をセットする();
            }
        }
        #endregion

        /// <summary>
        /// 「合見積比較」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button合見積比較_Click(object sender, EventArgs e)
        private void button合見積比較_Click(object sender, EventArgs e)
        {
            合見積比較Form form = new 合見積比較Form(対象見積依頼.OdMmID);
            form.MdiParent = this.MdiParent;
            form.Show();
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
                ListInfo見積回答 info = new ListInfo見積回答();
                info.parent = 対象見積依頼;
                info.info = 対象見積回答;
                InfoUpdating(info);

                Formに情報をセットする();
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
            preNebiki = 0;
            try
            {
                preNebiki = WingCommon.Common.金額表示を数値へ変換(textBox見積値引き.Text);
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
        /// 「見積値引き」の入力後、金額欄の再計算、再表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox見積値引き_KeyUp(object sender, KeyEventArgs e)
        private void textBox見積値引き_KeyUp(object sender, KeyEventArgs e)
        {
            if (金額入力確認(見積金額(), "見積金額", textBox見積値引き, "見積値引き") == false)
            {
                textBox見積値引き.Text = preNebiki.ToString();
                textBox見積値引き.SelectionStart = textBox見積値引き.Text.Length;
                e.Handled = false;
                return;
            }
            金額欄再表示();
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
            if (textBox見積値引き.ReadOnly == true)
                return;

            try
            {
                decimal nebiki = WingCommon.Common.金額表示を数値へ変換(textBox見積値引き.Text);
                textBox見積値引き.Text = nebiki.ToString();
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
                decimal nebiki = decimal.Parse(textBox見積値引き.Text);
                textBox見積値引き.Text = WingCommon.Common.金額出力(nebiki);
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
            preTax = 0;
            try
            {
                preTax = WingCommon.Common.金額表示を数値へ変換(textBox消費税.Text);
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
        #region private void textBox消費税_KeyUp(object sender, KeyEventArgs e)
        private void textBox消費税_KeyUp(object sender, KeyEventArgs e)
        {
            if (金額入力確認(見積金額(), "見積金額", textBox消費税, "消費税") == false )
            {
                textBox消費税.Text = preTax.ToString();
                textBox消費税.SelectionStart = textBox消費税.Text.Length; 
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
        #region private void textBox消費税_Enter(object sender, EventArgs e)
        private void textBox消費税_Enter(object sender, EventArgs e)
        {
            if (textBox消費税.ReadOnly == true)
                return;

            try
            {
                decimal tax = WingCommon.Common.金額表示を数値へ変換(textBox消費税.Text);
                textBox消費税.Text = tax.ToString();
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
                decimal tax = decimal.Parse(textBox消費税.Text);
                textBox消費税.Text = WingCommon.Common.金額出力(tax);
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
            if (textBox送料運搬料.ReadOnly == true)
                return;

            try
            {
                decimal tax = WingCommon.Common.金額表示を数値へ変換(textBox送料運搬料.Text);
                textBox送料運搬料.Text = tax.ToString();
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
                decimal carriage = decimal.Parse(textBox送料運搬料.Text);
                textBox送料運搬料.Text = WingCommon.Common.金額出力(carriage);
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
            Form form = new 品目編集Form((int)品目編集Form.EDIT_MODE.新規, 対象見積回答.MsThiIraiSbtID, 対象見積回答.MsVesselID, ref odMkItem);
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

            金額欄再表示();

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
            if (対象見積回答.MsThiIraiSbtID == WingCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                return;

            // 処理前の位置を保持する
            Point orgPoint = treeListView.GetScrollPos();

            if (品目TreeList.DoubleClick(対象見積回答, ref 見積回答品目s, ref 削除回答品目s) == true)
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
            MsCustomer customer = null;
            OdThi thi = null;
            OdMm mm = null;
            using (WingServiceReferences.WingServer.ServiceClient serviceClient = new WingServiceReferences.WingServer.ServiceClient())
            {
                customer = serviceClient.MsCustomer_GetRecord(WingCommon.Common.LoginUser, 対象見積回答.MsCustomerID);
                thi = serviceClient.OdThi_GetRecordByOdMkID(WingCommon.Common.LoginUser, 対象見積回答.OdMkID);

                mm = serviceClient.OdMm_GetRecord(WingCommon.Common.LoginUser, 対象見積回答.OdMmID);
                if (mm != null)
                {
                    対象見積回答.MsNyukyoKamokuID = mm.MsNyukyoKamokuID;
                }
            }

            //=========================================
            // 対象見積回答の内容を画面にセットする
            //=========================================
            #region Windowタイトル
            this.Text = WingCommon.Common.WindowTitle("JM040401", "見積回答");
            #endregion
            #region 入力エリア
            label状況.Text = 対象見積回答.StatusName;
            textBox船.Text = 対象見積回答.MsVesselName;
            textBox手配依頼者.Text = thi.ThiUserName;
            textBox事務担当者.Text = thi.JimTantouName;
            textBox見積回答番号.Text = 対象見積回答.MkNo;
            textBox見積回答者.Text = 対象見積回答.MsCustomerName;
            textBox担当者.Text = 対象見積回答.Tantousha;
            if (customer.LoginID.Length > 0)
            {
                textBoxログインＩＤ.Text = customer.LoginID;
            }
            else
            {
                textBoxログインＩＤ.Text = "設定されていません";
            }
            if (customer.Password.Length > 0)
            {
                textBoxパスワード.Text = customer.Password;
            }
            else
            {
                textBoxパスワード.Text = "設定されていません";
            }
            textBox見積回答期限.Text = 対象見積回答.MkKigen;
            try
            {
                dateTimePicker見積回答日.Text = 対象見積回答.MkDate.ToShortDateString();
                textBox見積回答日.Text = 対象見積回答.MkDate.ToShortDateString();
            }
            catch
            {
            }
            textBox見積有効期限.Text = 対象見積回答.MkYukouKigen;
            try
            {
                dateTimePicker納期.Text = 対象見積回答.Nouki.ToShortDateString();
                textBox納期.Text = 対象見積回答.Nouki.ToShortDateString();
            }
            catch
            {
            }
            textBox工期.Text = 対象見積回答.Kouki;
            textBox手配内容.Text = 対象見積回答.OdThiNaiyou;
            textBox備考.Text = 対象見積回答.OdThiBikou;
            textBox消費税.Text = WingCommon.Common.金額出力(対象見積回答.Tax);
            textBox見積値引き.Text = WingCommon.Common.金額出力(対象見積回答.MkAmount);
            textBox送料運搬料.Text = WingCommon.Common.金額出力(対象見積回答.Carriage);
            #endregion
            #region 品目/詳細品目TreeList
            InitItemTreeListView();
            #endregion
            #region 金額エリア
            金額欄再表示();
            #endregion

            #region 画面のコンポーネントの表示/非表示
            if (WindowStyle == (int)WINDOW_STYLE.通常)
            {
                button見積依頼メール.Visible = true;
                button合見積比較.Visible = true;
                button品目編集.Enabled = false;
                品目TreeList.Enabled = false;

                if (対象見積回答.Status == 対象見積回答.OdStatusValue.Values[(int)OdMk.STATUS.未回答].Value)
                {
                    button発注.Visible = false;
                    button注文書出力.Visible = false;
                    button承認.Visible = false;
                    button差戻し.Visible = false;
                    button発注承認依頼.Visible = false;
                    button更新.Visible = true;
                    button取消.Visible = true;
                    button再見積.Visible = false;
                    button発注メール再送.Visible = false;

                    if (対象見積回答.MsThiIraiSbtID != WingCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                    {
                        if (MsRoleTableCache.instance().Enabled(WingCommon.Common.LoginUser, "発注管理", "発注状況一覧", "見積回答"))
                        {
                            button品目編集.Enabled = true;
                        }
                    }
                    品目TreeList.Enabled = true;
                }
                else if (対象見積回答.Status == 対象見積回答.OdStatusValue.Values[(int)OdMk.STATUS.回答済み].Value)
                {
                    button発注.Visible = false;
                    button注文書出力.Visible = false;
                    button承認.Visible = false;
                    button差戻し.Visible = false;
                    button発注承認依頼.Visible = true;
                    button更新.Visible = true;
                    button取消.Visible = true;
                    button再見積.Visible = true;
                    button発注メール再送.Visible = false;

                    if (対象見積回答.MsThiIraiSbtID != WingCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                    {
                        if (MsRoleTableCache.instance().Enabled(WingCommon.Common.LoginUser, "発注管理", "発注状況一覧", "見積回答"))
                        {
                            button品目編集.Enabled = true;
                        }
                    }
                    品目TreeList.Enabled = true;
                }
                else if (対象見積回答.Status == 対象見積回答.OdStatusValue.Values[(int)OdMk.STATUS.発注承認依頼中].Value)
                {
                    button発注.Visible = false;
                    button注文書出力.Visible = false;
                    button承認.Visible = false;
                    button差戻し.Visible = false;
                    button発注承認依頼.Visible = false;
                    button更新.Visible = false;
                    button取消.Visible = false;
                    button再見積.Visible = false;
                    button発注メール再送.Visible = false;

                    入力エリアをReadOnlyにする();
                }
                else if (対象見積回答.Status == 対象見積回答.OdStatusValue.Values[(int)OdMk.STATUS.発注承認済み].Value)
                {
                    button発注.Visible = true;
                    //button注文書出力.Visible = true;
                    button注文書出力.Visible = false; // 2009.10.25:aki 発注日を出力するので、出力は発注済みになってから。
                    button承認.Visible = false;
                    button差戻し.Visible = false;
                    button発注承認依頼.Visible = false;
                    button更新.Visible = false;
                    button取消.Visible = false;
                    button再見積.Visible = false;
                    button発注メール再送.Visible = false;

                    入力エリアをReadOnlyにする();
                }
                else if (対象見積回答.Status == 対象見積回答.OdStatusValue.Values[(int)OdMk.STATUS.発注済み].Value)
                {
                    button発注.Visible = false;
                    button注文書出力.Visible = true;
                    button承認.Visible = false;
                    button差戻し.Visible = false;
                    button発注承認依頼.Visible = false;
                    button更新.Visible = false;
                    button取消.Visible = false;
                    button再見積.Visible = false;
                    button発注メール再送.Visible = true;

                    入力エリアをReadOnlyにする();
                }
            }
            else if (WindowStyle == (int)WINDOW_STYLE.承認)
            {
                button再見積.Visible = false;
                button見積依頼メール.Visible = false;
                button更新.Visible = false;
                button合見積比較.Visible = false;
                button注文書出力.Visible = false;
                button発注.Visible = false;
                button承認.Visible = true;
                button差戻し.Visible = true;
                button発注承認依頼.Visible = false;
                button取消.Visible = false;
                button発注メール再送.Visible = false;

                button品目編集.Enabled = false;
                品目TreeList.Enabled = false;

                入力エリアをReadOnlyにする();
            }
            #endregion
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        #region private void 入力エリアをReadOnlyにする()
        private void 入力エリアをReadOnlyにする()
        {
            textBox担当者.ReadOnly = true;
            dateTimePicker見積回答日.Visible = false;
            textBox見積回答日.Visible = true;
            dateTimePicker納期.Visible = false;
            textBox納期.Visible = true;
            textBox工期.ReadOnly = true;
            textBox消費税.ReadOnly = true;
            textBox見積値引き.ReadOnly = true;
            textBox見積有効期限.ReadOnly = true;
            textBox送料運搬料.ReadOnly = true;
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

            int noColumIndex = 0;
            bool viewHeader = false;
            object[,] columns = null;

            見積回答品目s = Item見積回答品目.GetRecords(対象見積回答.OdMkID);
            削除回答品目s = new List<Item見積回答品目>();

            if (対象見積回答.MsThiIraiSbtID == WingCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                noColumIndex = 0;
                viewHeader = false;
                columns = new object[,] {
                                           {"No", 65, null, HorizontalAlignment.Right},
                                           {"仕様・型式 /　詳細品目", 275, null, null},
                                           {"単位", 45, null, null},
                                           {"見積数", 50, null, HorizontalAlignment.Right},
                                           {"回答数", 50, null, HorizontalAlignment.Right},
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
                                           {"部署名 /　仕様・型式 /　詳細品目", 275, null, null},
                                           {"単位", 45, null, null},
                                           {"見積数", 50, null, HorizontalAlignment.Right},
                                           {"回答数", 50, null, HorizontalAlignment.Right},
                                           {"単価", 90, null, HorizontalAlignment.Right},
                                           {"金額", 100, null, HorizontalAlignment.Right},
                                           {"添付", 40, null, HorizontalAlignment.Center},
                                           {"備考（品名、規格等）", 200, null, null}
                                         };

            }
            品目TreeList = new ItemTreeListView見積回答(treeListView);
            品目TreeList.SetColumns(noColumIndex, columns);
            品目TreeList.TextChangeEvent += new ItemTreeListView見積回答.TextChangeEventHandler(金額欄再表示);
            品目TreeList.LinkClickEvent += new ItemTreeListView.LinkClickEventHandler(View添付ファイル);
            if (対象見積回答.Status > 対象見積回答.OdStatusValue.Values[(int)OdMk.STATUS.回答済み].Value)
            {
                品目TreeList.editable = false;
            }
            品目TreeList.AddNodes(viewHeader, 見積回答品目s);
        }
        #endregion

        /// <summary>
        /// Formを閉じる
        /// </summary>
        #region private void Formを閉じる(
        private void Formを閉じる()
        {
            BaseFormClosing();
            Close();
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

                // 更新
                bool ret = 見積回答更新処理.更新(ref 対象見積回答, 見積回答品目s, 削除回答品目s);
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
                bool ret = 見積回答更新処理.取消(ref 対象見積回答);
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
        /// 受領作成処理
        /// </summary>
        /// <param name="受領"></param>
        /// <returns></returns>
        #region private bool 受領作成処理(ref OdJry 受領, ref MsThiIraiStatus newStatus)
        private bool 受領作成処理(ref OdJry 受領, ref MsThiIraiStatus newStatus)
        {
            try
            {
                if (入力情報の取得確認(対象見積回答.OdStatusValue.Values[(int)OdMk.STATUS.発注済み].Value) == false)
                {
                    return false;
                }
                bool ret = false;

                // 2009.09.23:aki  見積回答の更新は、下の受領作成時に行うように変更
                //ret = 見積回答更新処理.更新(ref 対象見積回答, 見積回答品目s, 削除回答品目s);
                //if (ret == false)
                //{
                //    MessageBox.Show("更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return false;
                //}

                ret = 受領更新処理.見積回答から作成(ref 受領, ref newStatus, ref 対象見積回答, 見積回答品目s);
                if (ret == false)
                {
                    MessageBox.Show("受領の作成に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("受領の作成に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 見積回答作成処理
        /// </summary>
        /// <param name="見積回答"></param>
        /// <returns></returns>
        #region private bool 見積回答作成処理(ref OdMk 見積回答)
        private bool 見積回答作成処理(ref OdMk 見積回答)
        {
            try
            {
                bool ret = false;

                ret = 見積回答更新処理.未回答作成(対象見積回答, 見積回答品目s, ref 見積回答);
                if (ret == false)
                {
                    MessageBox.Show("再見積に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("再見積に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
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

            // 担当者
            string tantousha = null;
            try
            {
                tantousha = textBox担当者.Text;
            }
            catch
            {
            }
            if (tantousha.Length == 0)
            {
                errMessage += "・担当者を入力してください。\n";
            }
            // 見積回答日
            DateTime mkDate = DateTime.MinValue;
            try
            {
                mkDate = DateTime.Parse(dateTimePicker見積回答日.Text);
            }
            catch
            {
                errMessage += "・見積回答日が不正です。\n";
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
            }
            // 見積有効期限
            string mkYukouKigen = null;
            try
            {
                mkYukouKigen = textBox見積有効期限.Text;
            }
            catch
            {
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
                nebiki = WingCommon.Common.金額表示を数値へ変換(textBox見積値引き.Text);
            }
            catch
            {
            }
            // 消費税
            decimal tax = 0;
            try
            {
                tax = WingCommon.Common.金額表示を数値へ変換(textBox消費税.Text);
            }
            catch
            {
            }
            // 送料・運搬料
            decimal carriage = 0;
            try
            {
                carriage = WingCommon.Common.金額表示を数値へ変換(textBox送料運搬料.Text);
            }
            catch
            {
            }
            // 見積金額
            decimal kingaku = 見積金額();
            if (kingaku + tax < nebiki)
            {
                errMessage += "・金額が不正です。\n";
            }
            // 品目/詳細品目
            if (setStatus == 対象見積回答.OdStatusValue.Values[(int)OdMk.STATUS.発注承認依頼中].Value)
            {
                int shousaiCount = 0;
                foreach (Item見積回答品目 見積回答品目 in 見積回答品目s)
                {
                    if (見積回答品目.品目.CancelFlag == WingCommon.Common.CancelFlag_キャンセル)
                    {
                        continue;
                    }
                    foreach (OdMkShousaiItem 詳細品目 in 見積回答品目.詳細品目s)
                    {
                        if (詳細品目.CancelFlag == WingCommon.Common.CancelFlag_キャンセル)
                        {
                            continue;
                        }
                        shousaiCount++;
                    }
                }
                if (shousaiCount == 0)
                {
                    errMessage += "・仕様・型式/詳細品目がない場合、発注承認依頼は行えません。\n";
                }
            }
            if (errMessage.Length > 0)
            {
                errMessage = "入力項目に不備があります。\n====================================\n" + errMessage;
                MessageBox.Show(errMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (対象見積回答.Status == 対象見積回答.OdStatusValue.Values[(int)OdMk.STATUS.未回答].Value)
            {
                if (品目TreeList.すべての回答が入力されたかチェック() == true)
                {
                    対象見積回答.Status = 対象見積回答.OdStatusValue.Values[(int)OdMk.STATUS.回答済み].Value;
                    if (tax == 0)
                    {
                        if (MessageBox.Show("消費税が入力されていないか、０が入力されています。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return false;
                        }
                    }
                }
            }
            else if ( setStatus > 0 )
            {
                対象見積回答.Status = setStatus;
            }
            対象見積回答.Tantousha = tantousha;
            対象見積回答.MkDate = mkDate;
            対象見積回答.Nouki = nouki;
            対象見積回答.Kouki = kouki;
            対象見積回答.MkYukouKigen = mkYukouKigen;
            対象見積回答.MkAmount = nebiki;
            対象見積回答.Tax = tax;
            対象見積回答.Amount = kingaku;
            対象見積回答.Carriage = carriage;

            return true;
        }
        #endregion

        /// <summary>
        /// 「品目リスト」上の詳細品目の金額が入力されたときに
        /// 「見積合計」「消費税」「合計金額」を再計算、再表示する
        /// </summary>
        #region public void 金額欄再表示()
        public void 金額欄再表示()
        {
            decimal kingaku = 見積金額();
            textBox見積金額.Text = WingCommon.Common.金額出力(kingaku);

            decimal nebiki = 0;
            try
            {
                nebiki = WingCommon.Common.金額表示を数値へ変換(textBox見積値引き.Text);
            }
            catch
            {
            }
            decimal tax = 0;
            try
            {
                tax = WingCommon.Common.金額表示を数値へ変換(textBox消費税.Text);
            }
            catch
            {
            }
            decimal carriage = 0;
            try
            {
                carriage = WingCommon.Common.金額表示を数値へ変換(textBox送料運搬料.Text);
            }
            catch
            {
            }
            // 2010.01.25:aki  W090228
            //textBox合計金額.Text = WingCommon.Common.金額出力(kingaku + tax - nebiki);
            // 2012.03 
            //textBox合計金額.Text = WingCommon.Common.金額出力(kingaku - nebiki);
            textBox合計金額.Text = WingCommon.Common.金額出力(kingaku - nebiki + carriage);

            textBox合計金額_税込.Text = WingCommon.Common.金額出力(kingaku - nebiki + carriage + tax);
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

        public void View添付ファイル(string odAttachFileId)
        {
            string id = "";
            string fileName = "";
            byte[] fileData = null;

            try
            {
                Cursor = Cursors.WaitCursor;

                OdAttachFile odAttachFile = null;
                using (WingServiceReferences.WingServer.ServiceClient serviceClient = new WingServiceReferences.WingServer.ServiceClient())
                {
                    // サーバから添付データを取得する
                    odAttachFile = serviceClient.OdAttachFile_GetRecord(WingCommon.Common.LoginUser, odAttachFileId);
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
                WingCommon.FileView.View(id, fileName, fileData);
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

        private void button1_Click(object sender, EventArgs e)
        {
            float minHeight = 32;

            if (tableLayoutPanel1.RowStyles[1].Height == minHeight)
            {
                tableLayoutPanel1.RowStyles[1].Height = 440;
                button1.Text = "▲";
            }
            else
            {
                tableLayoutPanel1.RowStyles[1].Height = minHeight;
                button1.Text = "▼"; 
            }
        }
    }
}
