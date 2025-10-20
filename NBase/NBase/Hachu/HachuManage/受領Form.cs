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
using NBaseData.DS;


namespace Hachu.HachuManage
{
    public partial class 受領Form : BaseUserControl//2021/07/12 BaseForm
    {
        private OdMk 対象見積回答;
        
        /// <summary>
        /// 対象受領
        /// </summary>
        private OdJry 対象受領;

        /// <summary>
        /// 対象受領の品目
        /// </summary>
        private List<Item受領品目> 受領品目s;

        /// <summary>
        /// 削除とマークされた品目
        /// </summary>
        private List<Item受領品目> 削除受領品目s;

        /// <summary>
        /// 支払（対象受領の子供）
        /// </summary>
        private List<OdShr> odShrs;

        /// <summary>
        /// 
        /// </summary>
        ItemTreeListView受領 品目TreeList;

        /// <summary>
        /// 
        /// </summary>
        private int ステータスはそのまま = -1;

        private enum CHECK_STATE { 問題なし, 下方修正, 上方修正 };

        private decimal preNebiki;
        private decimal preTax;

        private decimal 上方修正しきい値 = 1000000;

        //private float panelMinHeight = 39;
        //private float panelMaxHeight = 359 + 6;
        private float panelMinHeight = 0;
        private float panelMaxHeight = 223;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <param name="info"></param>
        #region public 受領Form(int windowStyle, ListInfo受領 info)
        public 受領Form(int windowStyle, ListInfo受領 info)
        {
            InitializeComponent();

            this.WindowStyle = windowStyle;
            対象見積回答 = info.parent;
            対象受領 = info.info;

            EnableComponents();
        }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <param name="info"></param>
        /// <param name="parent"></param>
        #region public 受領Form(int windowStyle, OdJry info, OdMk parent)
        public 受領Form(int windowStyle, OdJry info, OdMk parent)
        {
            InitializeComponent();

            this.WindowStyle = windowStyle;
            対象受領 = info;
            対象見積回答 = parent;

            EnableComponents();
        }
        #endregion


        private void EnableComponents()
        {
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "発注状況一覧", "受領"))
            {
                button更新.Enabled = true;
                button取消.Enabled = true;
                button承認.Enabled = true;
                button差戻し.Enabled = true;
                button支払依頼.Enabled = true;
                button受領.Enabled = true;
                button分納.Enabled = true;
                button落成.Enabled = true;
                button変更受領.Enabled = true;

                dateTimePicker受領日.Enabled = true;

                textBox消費税_納品.Enabled = true;
                textBox見積値引き_納品.Enabled = true;

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
        #region private void 受領Form_Load(object sender, EventArgs e)
        private void 受領Form_Load(object sender, EventArgs e)
        {
            //=========================================
            // ＤＢから必要な情報を取得する
            //=========================================
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (対象見積回答 == null)
                {
                    対象見積回答 = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, 対象受領.OdMkID);
                }
                odShrs = serviceClient.OdShr_GetRecordByOdJryID(NBaseCommon.Common.LoginUser, 対象受領.OdJryID);
                
                // 2010.05:aki 支払合算対応のため、以下５行追加
                List<OdShr> gassanShr = serviceClient.OdShr_GetRecordByGassanItem(NBaseCommon.Common.LoginUser, 対象受領.OdJryID);
                if (gassanShr != null)
                {
                    odShrs.AddRange(gassanShr);
                }
            }
            Formに情報をセットする();
        }
        #endregion


        /// <summary>
        /// 「支払依頼」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button支払依頼_Click(object sender, EventArgs e)
        private void button支払依頼_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("支払依頼をします。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            OdShr 支払 = new OdShr();
            支払.Sbt = (int)OdShr.SBT.支払;
            if (支払作成処理(ref 支払) == false)
            {
                return;
            }
            MessageBox.Show("支払依頼をしました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                ListInfo受領 info = new ListInfo受領();
                info.info = 対象受領;
                info.child = 支払;
                info.NextStatus = true;
                InfoUpdating(info);
            }
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
            if (MessageBox.Show("落成見積を作成します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            OdShr 落成 = new OdShr();
            落成.Sbt = (int)OdShr.SBT.落成;
            if (支払作成処理(ref 落成) == false)
            {
                return;
            }
            MessageBox.Show("落成見積を作成をしました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                ListInfo受領 info = new ListInfo受領();
                info.info = 対象受領;
                info.child = 落成;
                info.NextStatus = true;
                InfoUpdating(info);
            }
        }
        #endregion

        /// <summary>
        /// 「受領」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button受領_Click(object sender, EventArgs e)
        private void button受領_Click(object sender, EventArgs e)
        {
            int checkState = (int)CHECK_STATE.問題なし;
            string message = "受領します。よろしいですか？";
            if (受領チェック(ref checkState, ref message) == false)
            {
                MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show(message, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int setStatus = -1;
            string finMessage = "受領しました。";
            if (checkState == (int)CHECK_STATE.上方修正)
            {
                setStatus = 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領承認依頼中].Value;
                finMessage = "受領承認を依頼しました。";
            }
            else
            {
                setStatus = 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領承認済み].Value;
                // 下方修正は見ない（2010.02.01:aki W090196）
                //if (checkState == (int)CHECK_STATE.下方修正)
                //{
                //    finMessage = "変更受領しました。";
                //}
            }
            MsThiIraiStatus newStatus = new MsThiIraiStatus();
            if (更新処理(setStatus, ref newStatus) == false)
            {
                return;
            }

            MessageBox.Show(finMessage, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                ListInfo受領 info = null;
                if (newStatus.MsThiIraiStatusID != null && newStatus.MsThiIraiStatusID.Length > 0)
                {
                    // 手配依頼のステータスを変更
                    info = new ListInfo受領();
                    info.ChangeStatus = true;
                    info.SetStatus = newStatus;
                    info.info = 対象受領;
                    InfoUpdating(info);
                }
                info = new ListInfo受領();
                info.parent = 対象見積回答;
                対象受領.OdMkNouki = 対象見積回答.Nouki;
                info.info = 対象受領;
                InfoUpdating(info);

                Formに情報をセットする();
            }
        }
        #endregion

        /// <summary>
        /// 「変更受領」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button変更受領_Click(object sender, EventArgs e)
        private void button変更受領_Click(object sender, EventArgs e)
        {
            int checkState = (int)CHECK_STATE.問題なし;
            string message = "変更受領します。よろしいですか？";
            if (受領チェック(ref checkState, ref message) == false)
            {
                MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show(message, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int setStatus = -1;
            string finMessage = "受領しました。";
            if (checkState == (int)CHECK_STATE.上方修正)
            {
                setStatus = 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領承認依頼中].Value;
                finMessage = "受領承認を依頼しました。";
            }
            else
            {
                setStatus = 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領承認済み].Value;
            }
            MsThiIraiStatus newStatus = new MsThiIraiStatus();
            if (更新処理(setStatus, ref newStatus) == false)
            {
                return;
            }

            MessageBox.Show(finMessage, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                ListInfo受領 info = null;
                if (newStatus.MsThiIraiStatusID != null && newStatus.MsThiIraiStatusID.Length > 0)
                {
                    // 手配依頼のステータスを変更
                    info = new ListInfo受領();
                    info.ChangeStatus = true;
                    info.SetStatus = newStatus;
                    info.info = 対象受領;
                    InfoUpdating(info);
                }
                info = new ListInfo受領();
                info.parent = 対象見積回答;
                対象受領.OdMkNouki = 対象見積回答.Nouki;
                info.info = 対象受領;
                InfoUpdating(info);

                Formに情報をセットする();
            }
        }
        #endregion

        /// <summary>
        /// 「分納」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button分納_Click(object sender, EventArgs e)
        private void button分納_Click(object sender, EventArgs e)
        {
            if (分納チェック() == false)
            {
                MessageBox.Show("分納できる品目/詳細品目がありません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int checkState = (int)CHECK_STATE.問題なし;
            string message = "分納します。よろしいですか？";
            if (受領チェック(ref checkState, ref message) == false)
            {
                MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show(message, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            int setStatus = -1;
            string finMessage = "分納受領しました。";
            if (checkState == (int)CHECK_STATE.上方修正)
            {
                setStatus = 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領承認依頼中].Value;
                finMessage = "受領承認を依頼しました。";
            }
            else
            {
                setStatus = 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領承認済み].Value;
            }

            OdJry 受領残分 = new OdJry();
            if (分納処理(setStatus, ref 受領残分) == false)
            {
                return;
            }

            MessageBox.Show(finMessage, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                ListInfo受領 curInfo = new ListInfo受領();
                curInfo.parent = 対象見積回答;
                対象受領.OdMkNouki = 対象見積回答.Nouki;
                curInfo.info = 対象受領;
                InfoUpdating(curInfo);

                ListInfo受領 newInfo = new ListInfo受領();
                newInfo.AddNode = true;
                newInfo.parent = 対象見積回答;
                受領残分.OdMkNouki = 対象見積回答.Nouki;
                newInfo.info = 受領残分;
                InfoUpdating(newInfo);

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
            MsThiIraiStatus newStatus = new MsThiIraiStatus();
            if (更新処理(ステータスはそのまま, ref newStatus) == false)
            {
                return;
            }

            ListInfo受領 info = null;
            if (newStatus.MsThiIraiStatusID != null && newStatus.MsThiIraiStatusID.Length > 0)
            {
                // 手配依頼のステータスを変更
                info = new ListInfo受領();
                info.ChangeStatus = true;
                info.SetStatus = newStatus;
                info.info = 対象受領;
                InfoUpdating(info);
            }

            //---------コメントアウト2021/07/12-----------
            //if (this.MdiParent == null)
            //{
            //    // 「アラーム情報」からの呼び出しの場合
            //    MessageBox.Show("更新しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Close();
            //}
            if (this.Parent == null)
            {
                // 「アラーム情報」からの呼び出しの場合
                MessageBox.Show("更新しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ;
            }
            else
            {
                info = new ListInfo受領();
                info.parent = 対象見積回答;
                対象受領.OdMkNouki = 対象見積回答.Nouki;
                info.info = 対象受領;
                InfoUpdating(info);

                InitItemTreeListView();

                MessageBox.Show("更新しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (MessageBox.Show("この受領を取消します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // 取消し処理
            MsThiIraiStatus newStatus = new MsThiIraiStatus();
            if (取消処理(ref newStatus) == false)
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
                ListInfo受領 info = null;
                if (対象見積回答.MkNo.Substring(0, 7) == "Enabled")
                {
                    // 手配依頼、受領を削除
                    info = new ListInfo受領();
                    info.Remove = true;
                    info.RemoveTop = true;
                    info.parent = 対象見積回答;
                    info.info = 対象受領;
                    InfoUpdating(info);
                }
                else
                {
                    // 手配依頼のステータスを変更
                    info = new ListInfo受領();
                    info.ChangeStatus = true;
                    info.SetStatus = newStatus;
                    info.info = 対象受領;
                    InfoUpdating(info);

                    // 見積回答のステータスを変更、受領を削除
                    info = new ListInfo受領();
                    info.Remove = true;
                    info.parent = 対象見積回答;
                    info.info = 対象受領;
                    InfoUpdating(info);
                }

                // Formを閉じる
                //---------コメントアウト2021/07/12-----------
                //Close();
            }
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
            if (MessageBox.Show("この受領を承認します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (更新処理(対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領承認済み].Value) == false)
            {
                return;
            }

            ListInfo受領 info = new ListInfo受領();
            info.Remove = true;
            info.parent = null;
            info.info = 対象受領;
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
            if (MessageBox.Show("この受領を差戻します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (更新処理(対象受領.OdStatusValue.Values[(int)OdJry.STATUS.未受領].Value) == false)
            {
                return;
            }

            ListInfo受領 info = new ListInfo受領();
            info.Remove = true;
            info.parent = null;
            info.info = 対象受領;
            InfoUpdating(info);

            MessageBox.Show("差戻しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Formを閉じる();
        }
        #endregion

        /// <summary>
        /// 「受領解除」ボタンクリック
        ///  2011.05 Add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button解除_Click(object sender, EventArgs e)
        private void button解除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("この受領を解除します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            MsThiIraiStatus newStatus = new MsThiIraiStatus();
            if (受領解除処理(対象受領.OdStatusValue.Values[(int)OdJry.STATUS.未受領].Value, ref newStatus) == false)
            {
                return;
            }


            ListInfo受領 info = null;
            if (newStatus.MsThiIraiStatusID != null && newStatus.MsThiIraiStatusID.Length > 0)
            {
                // 手配依頼のステータスを変更
                info = new ListInfo受領();
                info.ChangeStatus = true;
                info.SetStatus = newStatus;
                info.info = 対象受領;
                InfoUpdating(info);
            }

            info = new ListInfo受領();
            info.parent = 対象見積回答;
            対象受領.OdMkNouki = 対象見積回答.Nouki;
            info.info = 対象受領;
            InfoUpdating(info);

            MessageBox.Show("解除しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Formに情報をセットする();
        }
        #endregion

        /// <summary>
        /// 「見積値引き」の入力キーを制限する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox見積値引き_納品_KeyPress(object sender, KeyPressEventArgs e)
        private void textBox見積値引き_納品_KeyPress(object sender, KeyPressEventArgs e)
        {
            preNebiki = 0;
            try
            {
                preNebiki = NBaseCommon.Common.金額表示を数値へ変換(textBox見積値引き_納品.Text);
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
        #region private void textBox見積値引き_納品_KeyUp(object sender, KeyEventArgs e)
        private void textBox見積値引き_納品_KeyUp(object sender, KeyEventArgs e)
        {
            if (金額入力確認(受領合計金額(), "納品額", textBox見積値引き_納品, "見積値引き") == false)
            {
                textBox見積値引き_納品.Text = preNebiki.ToString();
                textBox見積値引き_納品.SelectionStart = textBox見積値引き_納品.Text.Length;
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
        #region private void textBox見積値引き_納品_Enter(object sender, EventArgs e)
        private void textBox見積値引き_納品_Enter(object sender, EventArgs e)
        {
            if (textBox見積値引き_納品.ReadOnly == true)
                return;

            try
            {
                decimal nebiki = NBaseCommon.Common.金額表示を数値へ変換(textBox見積値引き_納品.Text);
                textBox見積値引き_納品.Text = nebiki.ToString();
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
        #region private void textBox見積値引き_納品_Leave(object sender, EventArgs e)
        private void textBox見積値引き_納品_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal nebiki = decimal.Parse(textBox見積値引き_納品.Text);
                textBox見積値引き_納品.Text = NBaseCommon.Common.金額出力(nebiki);
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
        #region private void textBox消費税_納品_KeyPress(object sender, KeyPressEventArgs e)
        private void textBox消費税_納品_KeyPress(object sender, KeyPressEventArgs e)
        {
            preTax = 0;
            try
            {
                preTax = NBaseCommon.Common.金額表示を数値へ変換(textBox消費税_納品.Text);
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
        #region private void textBox消費税_納品_KeyUp(object sender, KeyEventArgs e)
        private void textBox消費税_納品_KeyUp(object sender, KeyEventArgs e)
        {
            if (金額入力確認(受領合計金額(), "納品額", textBox消費税_納品, "消費税") == false)
            {
                textBox消費税_納品.Text = preTax.ToString();
                textBox消費税_納品.SelectionStart = textBox消費税_納品.Text.Length;
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
        #region private void textBox消費税_納品_Enter(object sender, EventArgs e)
        private void textBox消費税_納品_Enter(object sender, EventArgs e)
        {
            if (textBox消費税_納品.ReadOnly == true)
                return;

            try
            {
                decimal tax = NBaseCommon.Common.金額表示を数値へ変換(textBox消費税_納品.Text);
                textBox消費税_納品.Text = tax.ToString();
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
        #region private void textBox消費税_納品_Leave(object sender, EventArgs e)
        private void textBox消費税_納品_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal tax = decimal.Parse(textBox消費税_納品.Text);
                textBox消費税_納品.Text = NBaseCommon.Common.金額出力(tax);
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
            if (textBox送料運搬料_納品.ReadOnly == true)
                return;

            try
            {
                decimal tax = NBaseCommon.Common.金額表示を数値へ変換(textBox送料運搬料_納品.Text);
                textBox送料運搬料_納品.Text = tax.ToString();
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
                decimal carriage = decimal.Parse(textBox送料運搬料_納品.Text);
                textBox送料運搬料_納品.Text = NBaseCommon.Common.金額出力(carriage);
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
                燃料潤滑油編集Form form = new 燃料潤滑油編集Form(受領品目s);
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

                OdJryItem odkJryItem = new OdJryItem();
                Form form = new 品目編集Form((int)品目編集Form.EDIT_MODE.新規, 対象見積回答.MsThiIraiSbtID, 対象見積回答.MsVesselID, ref odkJryItem);
                if (form.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                Item受領品目 追加品目 = new Item受領品目();
                追加品目.品目 = odkJryItem;
                foreach (OdJryShousaiItem shousaiItem in odkJryItem.OdJryShousaiItems)
                {
                    追加品目.詳細品目s.Add(shousaiItem);
                }

                //受領品目s.Add(追加品目);
                //品目TreeList.AddNodes(追加品目);
                // 2009.09.15 ヘッダ対応　↑　コメント
                // 2009.09.15 ヘッダ対応　↓　コード置き換え
                int insertPos = 0;
                bool sameHeader = false;
                foreach (Item受領品目 品目 in 受領品目s)
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
                if (insertPos >= 受領品目s.Count)
                {
                    受領品目s.Add(追加品目);
                }
                else
                {
                    受領品目s.Insert(insertPos, 追加品目);
                }
                品目TreeList.NodesClear();
                品目TreeList.AddNodes(true, 受領品目s);

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

            if (品目TreeList.DoubleClick(対象受領, 対象見積回答.MsThiIraiSbtID, 対象見積回答.MsVesselID, ref 受領品目s, ref 削除受領品目s) == true)
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
            OdJryGaisan jryGaisan = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                thi = serviceClient.OdThi_GetRecordByOdJryID(NBaseCommon.Common.LoginUser, 対象受領.OdJryID);

                jryGaisan = serviceClient.OdJryGaisan_GetRecordByOdJryId(NBaseCommon.Common.LoginUser, 対象受領.OdJryID);
            }

            //=========================================
            // 対象受領の内容を画面にセットする
            //=========================================
            #region Windowタイトル
            this.Text = NBaseCommon.Common.WindowTitle("JM040701", "受領", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            #endregion
            #region 入力エリア
            label状況.Text = 対象受領.StatusName;
            textBox船.Text = 対象見積回答.MsVesselName;
            textBox手配依頼者.Text = thi.ThiUserName;
            textBox事務担当者.Text = thi.JimTantouName;
            textBox受領番号.Text = 対象受領.JryNo;
            textBox発注先.Text = 対象見積回答.MsCustomerName;
            textBox担当者.Text = 対象見積回答.Tantousha;
            textBox納期.Text = 対象見積回答.Nouki.ToShortDateString();
            textBox工期.Text = 対象見積回答.Kouki;
            if (対象受領.JryDate != DateTime.MinValue)
            {
                dateTimePicker受領日.Text = 対象受領.JryDate.ToShortDateString(); 
                textBox受領日.Text = 対象受領.JryDate.ToShortDateString();
            }
            else
            {
                dateTimePicker受領日.Text = "";
                textBox受領日.Text = "";
            }
            textBox手配内容.Text = 対象見積回答.OdThiNaiyou;
            textBox備考.Text = 対象見積回答.OdThiBikou;

            #endregion
            #region 品目/詳細品目TreeList
            InitItemTreeListView();
            #endregion
            #region 金額エリア
            if (jryGaisan != null)
            {
                // 一番最新の、概算金額を使用する
                対象受領.GaisanAmount = jryGaisan.Amount;
            }
            else
            {
                // 概算変更していない場合、概算金額は、納品額
                //対象受領.GaisanAmount = 受領合計金額();
                // 概算変更していない場合、概算金額は、納品額+送料・運搬料
                対象受領.GaisanAmount = 受領合計金額() + 対象見積回答.Carriage;
            }
            // 2014.02 2013年度改造
            //if (対象受領.Status == 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領済み].Value ||
            if (対象受領.Status == 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.船受領].Value ||
                対象受領.Status == 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領承認済み].Value)
            {
                textBox概算金額.Text = NBaseCommon.Common.金額出力(対象受領.GaisanAmount);
            }
            decimal mmKingaku = 対象見積回答.Amount;
            textBox見積額.Text = NBaseCommon.Common.金額出力(mmKingaku);
            textBox消費税_見積.Text = NBaseCommon.Common.金額出力(対象見積回答.Tax);
            textBox送料運搬料_見積.Text = NBaseCommon.Common.金額出力(対象見積回答.Carriage);
            textBox見積値引き_見積.Text = NBaseCommon.Common.金額出力(対象見積回答.MkAmount);
            // 2010.01.25:aki W090228
            //textBox合計金額_見積.Text = NBaseCommon.Common.金額出力(mmKingaku - 対象見積回答.MkAmount + 対象見積回答.Tax);
            //textBox合計金額_見積.Text = NBaseCommon.Common.金額出力(mmKingaku - 対象見積回答.MkAmount);
            textBox合計金額_見積.Text = NBaseCommon.Common.金額出力(mmKingaku - 対象見積回答.MkAmount + 対象見積回答.Carriage);
            textBox合計金額_税込_見積.Text = NBaseCommon.Common.金額出力(mmKingaku - 対象見積回答.MkAmount + 対象見積回答.Carriage + 対象見積回答.Tax);

            textBox消費税_納品.Text = NBaseCommon.Common.金額出力(対象受領.Tax);
            textBox送料運搬料_納品.Text = NBaseCommon.Common.金額出力(対象受領.Carriage);
            textBox見積値引き_納品.Text = NBaseCommon.Common.金額出力(対象受領.NebikiAmount);

            金額欄再表示();
            #endregion

            #region 画面のコンポーネントの表示/非表示
            if (WindowStyle == (int)WINDOW_STYLE.通常)
            {
                button承認.Visible = false;
                button差戻し.Visible = false;
                button解除.Visible = false; // 2011.05 Add

                dateTimePicker受領日.Visible = false;
                textBox受領日.Visible = true;

                textBox消費税_納品.ReadOnly = true;
                textBox送料運搬料_納品.ReadOnly = true;
                textBox見積値引き_納品.ReadOnly = true;

                button品目編集.Enabled = false;
                品目TreeList.Enabled = false;
                品目TreeList.editable = false;

                //if (対象受領.Status == 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.未受領].Value)
                if (対象受領.Status == 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.未受領].Value ||
                    対象受領.Status == 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.船受領].Value)
                {
                    button更新.Visible = true;
                    button取消.Visible = true;

                    button受領.Visible = true;
                    // 2010.04.30:aki 受領処理の整理に伴い、以下ボタンの制御を変更
                    //button変更受領.Visible = true;
                    button変更受領.Visible = false;
                    button分納.Visible = true;

                    button支払依頼.Visible = false;
                    button落成.Visible = false;

                    // 2014.03.18: 内藤氏コメントに編集可とする
                    //if (対象見積回答.MsThiIraiSbtID != NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                    //{
                        textBox消費税_納品.ReadOnly = false;
                        textBox送料運搬料_納品.ReadOnly = false;
                        textBox見積値引き_納品.ReadOnly = false;
                    //    //button品目編集.Enabled = true;
                    //}

                    button品目編集.Enabled = true;
                    品目TreeList.Enabled = true;
                    品目TreeList.editable = true;

                    dateTimePicker受領日.Visible = true;
                    textBox受領日.Visible = false;

                    button概算金額変更.Enabled = false;
                }
                // 2014.02 2013年度改造
                //else if (対象受領.Status == 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領済み].Value)
                //{
                //    button更新.Visible = false;
                //    button取消.Visible = false;

                //    button受領.Visible = true;
                //    // 2010.04.30:aki 受領処理の整理に伴い、以下ボタンの制御を変更
                //    //button変更受領.Visible = true;
                //    button変更受領.Visible = false;
                //    button分納.Visible = true;

                //    button支払依頼.Visible = true;
                //    button落成.Visible = true;

                //    button概算金額変更.Enabled = true;
                //}
                else if (対象受領.Status == 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領承認依頼中].Value)
                {
                    button更新.Visible = false;
                    button取消.Visible = false;

                    button受領.Visible = false;
                    button変更受領.Visible = false;
                    button分納.Visible = false;

                    button支払依頼.Visible = false;
                    button落成.Visible = false;

                    button概算金額変更.Enabled = false;
                }
                else if (対象受領.Status == 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領承認済み].Value)
                {
                    button更新.Visible = false;
                    button取消.Visible = false;

                    // 2010.04.30:aki 受領処理の整理に伴い、以下ボタンの制御を変更
                    //button受領.Visible = true;
                    //button変更受領.Visible = true;
                    //button分納.Visible = true;
                    button受領.Visible = false;
                    button変更受領.Visible = false;
                    button分納.Visible = false;

                    button支払依頼.Visible = true;
                    button落成.Visible = true;

                    // 2011.05 Add 4Lines
                    if (odShrs.Count == 0)
                    {
                        button解除.Visible = true;
                    }

                    button概算金額変更.Enabled = true;
                }

                int 支払count = 0;
                int 落成済count = 0;

                foreach (OdShr odShr in odShrs)
                {
                    if (odShr.Sbt == (int)OdShr.SBT.支払)
                    {
                        支払count++;
                        button落成.Visible = false;
                        if (odShr.Status != odShr.OdStatusValue.Values[(int)OdShr.STATUS.支払作成済み].Value)
                        {
                            button更新.Visible = false;
                            button取消.Visible = false;

                            button受領.Visible = false;
                            button変更受領.Visible = false;
                            button分納.Visible = false;

                            button支払依頼.Visible = false;
                            button落成.Visible = false;

                            break;
                        }
                    }
                    if (odShr.Sbt == (int)OdShr.SBT.落成)
                    {
                        if (odShr.Status == (int)OdShr.STATUS.落成済み)
                        {
                            落成済count++;
                        }
                        button更新.Visible = false;
                        button取消.Visible = false;

                        button受領.Visible = false;
                        button変更受領.Visible = false;
                        button分納.Visible = false;

                        button支払依頼.Visible = false;
                        button落成.Visible = false;

                        break;
                    }
                }

                if (支払count > 0 || 落成済count > 0)
                {
                    button概算金額変更.Enabled = false;
                }
            }
            else if (WindowStyle == (int)WINDOW_STYLE.承認)
            {
                button更新.Visible = false;
                button取消.Visible = false;

                button受領.Visible = false;
                button変更受領.Visible = false;
                button分納.Visible = false;

                button承認.Visible = true;
                button差戻し.Visible = true;

                button支払依頼.Visible = false;
                button落成.Visible = false;

                dateTimePicker受領日.Visible = false;
                textBox受領日.Visible = true;

                textBox消費税_納品.ReadOnly = true;
                textBox送料運搬料_納品.ReadOnly = true;
                textBox見積値引き_納品.ReadOnly = true;

                button品目編集.Enabled = false;
                品目TreeList.Enabled = false;

                button概算金額変更.Enabled = false;
            }
            else if (WindowStyle == (int)WINDOW_STYLE.概算)
            {
                button更新.Visible = false;
                button取消.Visible = false;

                button受領.Visible = false;
                button変更受領.Visible = false;
                button分納.Visible = false;

                button承認.Visible = false;
                button差戻し.Visible = false;

                button支払依頼.Visible = false;
                button落成.Visible = false;

                dateTimePicker受領日.Visible = false;
                textBox受領日.Visible = true;

                textBox消費税_納品.ReadOnly = true;
                textBox送料運搬料_納品.ReadOnly = true;
                textBox見積値引き_納品.ReadOnly = true;

                button品目編集.Enabled = false;
                品目TreeList.Enabled = false;

                button概算金額変更.Enabled = false;
            }
            #endregion

            // 2015.04 コメントあり：仕様・型式の新規追加はなしとする
            button品目編集.Enabled = false;                

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
                //品目TreeList = new ItemTreeListView受領(treeListView);
                品目TreeList = new ItemTreeListView受領(対象受領.MsThiIraiSbtID, treeListView);
                if (対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                {
                    品目TreeList.Enum表示方式 = ItemTreeListView受領.表示方式enum.Zero以外を表示;
                }
                // <==
            }

            int noColumIndex = 0;
            bool viewHeader = false;
            object[,] columns = null;

            受領品目s = Item受領品目.GetRecords(対象受領.OdJryID);
            削除受領品目s = new List<Item受領品目>();

            if (対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            {
                noColumIndex = 0;
                viewHeader = false;
                columns = new object[,] {
                                           {"No", 65, null, HorizontalAlignment.Right},
                                           {"仕様・型式 /　詳細品目", 275, null, null},
                                           {"単位", 45, null, null},
                                           {"発注数", 52, null, HorizontalAlignment.Right},
                                           {"受領数", 52, null, HorizontalAlignment.Right},
                                           {"単価", 90, null, HorizontalAlignment.Right},
                                           {"金額", 100, null, HorizontalAlignment.Right},
                                           {"受領日", 100, null, null},
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
                                           {"発注数", 52, null, HorizontalAlignment.Right},
                                           {"受領数", 52, null, HorizontalAlignment.Right},
                                           {"単価", 90, null, HorizontalAlignment.Right},
                                           {"金額", 100, null, HorizontalAlignment.Right},
                                           {"受領日", 100, null, null},
                                           {"備考（品名、規格等）", 200, null, null}
                                         };
            }

            品目TreeList.SetColumns(noColumIndex, columns);
            品目TreeList.TextChangeEvent += new ItemTreeListView受領.TextChangeEventHandler(金額欄再表示);
            品目TreeList.editable = true;
            if (WindowStyle != (int)WINDOW_STYLE.通常)
            {
                品目TreeList.editable = false;
            }
            if (対象受領.Status > 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.未受領].Value)
            {
                品目TreeList.editable = false;
            }
            品目TreeList.AddNodes(viewHeader, 受領品目s);
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

            // 受領日
            DateTime jryDate = DateTime.MinValue;
            try
            {
                jryDate = DateTime.Parse(dateTimePicker受領日.Text);
                if (Hachu.Common.CommonCheck.Check保存(jryDate) == false)
                {
                    errMessage += "・指定された受領日は、既に月次確定済みのため指定できません。\n";
                }
            }
            catch
            {
                errMessage += "・受領日が不正です。\n";
            }

            // 受領時値引き
            decimal nebiki = 0;
            try
            {
                nebiki = NBaseCommon.Common.金額表示を数値へ変換(textBox見積値引き_納品.Text);
            }
            catch
            {
            }
            // 消費税
            decimal tax = 0;
            try
            {
                tax = NBaseCommon.Common.金額表示を数値へ変換(textBox消費税_納品.Text);
            }
            catch
            {
            }
            // 送料・運搬料
            decimal carriage = 0;
            try
            {
                carriage = NBaseCommon.Common.金額表示を数値へ変換(textBox送料運搬料_納品.Text);
            }
            catch
            {
            }
            // 受領金額
            decimal kingaku = 受領合計金額();
            //if (kingaku + tax < nebiki)
            if (kingaku + tax + carriage < nebiki)
            {
                errMessage += "・金額が不正です。\n";
            }
            // 品目/詳細品目
            if (対象受領.Status == 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.未受領].Value && setStatus != ステータスはそのまま)
            {
                int shousaiCount = 0;
                int nullCount = 0;
                foreach (Item受領品目 受領品目 in 受領品目s)
                {
                    if (受領品目.品目.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        continue;
                    }
                    foreach (OdJryShousaiItem 詳細品目 in 受領品目.詳細品目s)
                    {
                        if (詳細品目.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                        {
                            continue;
                        }

                        //===========================
                        // 2014.2 [2013年度改造]
                        //if (詳細品目.JryCount == int.MinValue)
                        //{
                        //    nullCount++;
                        //}
                        if (対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                        {
                            if (詳細品目.Count > 1 && 詳細品目.JryCount == int.MinValue)
                            {
                                nullCount++;
                            }
                        }
                        else if (詳細品目.JryCount == int.MinValue)
                        {
                            nullCount++;
                        }
                        //<==

                        shousaiCount++;
                    }
                }
                if (shousaiCount == 0)
                {
                    errMessage += "・仕様・型式/詳細品目がない場合、受領は行えません。\n";
                }
                if (setStatus == 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領承認済み].Value && nullCount > 0)
                {
                    errMessage += "・受領数が入力されていない品目があります。受領は行えません。\n";
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
                if (対象受領.Status == 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.未受領].Value)
                {
                    if (tax == 0)
                    {
                        if (MessageBox.Show("消費税が入力されていないか、０が入力されています。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return false;
                        }
                    }
                }
                // 2009.09.26:aki 入力可としたので、コメントアウト
                //if (setStatus == 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領済み].Value||
                //    setStatus == 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.受領承認済み].Value)
                //{
                //    対象受領.JryDate = DateTime.Now;
                //}
                対象受領.Status = setStatus;
            }
            対象受領.JryDate = jryDate;
            対象受領.Amount = kingaku;
            対象受領.NebikiAmount = nebiki;
            対象受領.Tax = tax;
            対象受領.Carriage = carriage;

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
            MsThiIraiStatus newStatus = new MsThiIraiStatus();
            return 更新処理(setStatus, ref newStatus);
        }
        #endregion
        #region private bool 更新処理(int setStatus, ref MsThiIraiStatus newStatus)
        private bool 更新処理(int setStatus, ref MsThiIraiStatus newStatus)
        {
            try
            {
                if (入力情報の取得確認(setStatus) == false)
                {
                    return false;
                }
                bool ret = 受領更新処理.更新(ref 対象受領, 受領品目s, 削除受領品目s, ref newStatus);
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
        #region private bool 取消処理(ref MsThiIraiStatus newStatus)
        private bool 取消処理(ref MsThiIraiStatus newStatus)
        {
            try
            {
                bool ret = 受領更新処理.取消(ref 対象受領, ref 対象見積回答, ref newStatus);
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
        /// 分納処理
        /// </summary>
        /// <returns></returns>
        #region private bool 分納処理(int setStatus, ref OdJry 受領残分)
        private bool 分納処理(int setStatus, ref OdJry 受領残分)
        {
            try
            {
                if (入力情報の取得確認(setStatus) == false)
                {
                    return false;
                }
                bool ret = 受領更新処理.分納(ref 対象受領, ref 受領残分, 受領品目s, 削除受領品目s);
                if (ret == false)
                {
                    MessageBox.Show("分納に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("分納に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 支払作成処理
        /// </summary>
        /// <param name="支払"></param>
        /// <returns></returns>
        #region private bool 支払作成処理(ref OdShr 支払)
        private bool 支払作成処理(ref OdShr 支払)
        {
            try
            {
                bool ret = false;

                ret = 支払更新処理.受領から作成(ref 支払, 対象受領, 対象見積回答, 受領品目s);
                if (ret == false)
                {
                    MessageBox.Show("支払の作成に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("支払の作成に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        ///  受領解除処理
        ///  2011.05 Add
        /// </summary>
        /// <param name="setStatus"></param>
        /// <param name="newStatus"></param>
        /// <returns></returns>
        #region private bool 受領解除処理(int setStatus, ref MsThiIraiStatus newStatus)
        private bool 受領解除処理(int setStatus, ref MsThiIraiStatus newStatus)
        {
            try
            {
                if (入力情報の取得確認(setStatus) == false)
                {
                    return false;
                }
                bool ret = 受領更新処理.受領解除(ref 対象受領, ref newStatus);
                if (ret == false)
                {
                    MessageBox.Show("受領解除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("受領解除に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 「品目リスト」上の詳細品目の金額が入力されたときに
        /// 「納品額」「消費税」「合計金額」を再計算、再表示する
        /// </summary>
        #region public void 金額欄再表示()
        public void 金額欄再表示()
        {
            decimal kingaku = 受領合計金額();
            textBox納品額.Text = NBaseCommon.Common.金額出力(kingaku);

            decimal nebiki = 0;
            try
            {
                nebiki = NBaseCommon.Common.金額表示を数値へ変換(textBox見積値引き_納品.Text);
            }
            catch
            {
            }
            decimal tax = 0;
            try
            {
                tax = NBaseCommon.Common.金額表示を数値へ変換(textBox消費税_納品.Text);
            }
            catch
            {
            }
            decimal carriage = 0;
            try
            {
                carriage = NBaseCommon.Common.金額表示を数値へ変換(textBox送料運搬料_納品.Text);
            }
            catch
            {
            }
            // 2010.01.25:aki  W090228
            //textBox合計金額_納品.Text = NBaseCommon.Common.金額出力(kingaku + tax - nebiki);
            // 2012.03 
            //textBox合計金額_納品.Text = NBaseCommon.Common.金額出力(kingaku - nebiki);
            textBox合計金額_納品.Text = NBaseCommon.Common.金額出力(kingaku - nebiki + carriage);

            textBox合計金額_税込_納品.Text = NBaseCommon.Common.金額出力(kingaku - nebiki + carriage + tax);
        }
        #endregion

        /// <summary>
        /// 「品目リスト」上の詳細品目の受領金額合計を返す
        /// </summary>
        /// <returns></returns>
        #region public decimal 受領合計金額()
        public decimal 受領合計金額()
        {
            return 品目TreeList.受領合計金額();
        }
        #endregion


        /// <summary>
        /// 受領チェック
        /// （数量、金額の両方をチェックする）
        /// </summary>
        /// <param name="checkState"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        #region private bool 受領チェック(ref int checkState, ref string message)
        private bool 受領チェック(ref int checkState, ref string message)
        {
            //===============================================
            // 数量のチェック
            //===============================================
            int jryCount = 0;
            foreach (Item受領品目 受領品目 in 受領品目s)
            {
                foreach (OdJryShousaiItem shousaiItem in 受領品目.詳細品目s)
                {
                    jryCount += shousaiItem.JryCount;
                }
            }
            if (jryCount == 0)
            {
                message = "受領数が入力されていません。";
                return false;
            }

            //===============================================
            // 金額のチェック
            //===============================================
            // 見積額（見積回答時の金額）
            //decimal mmKingaku = 対象見積回答.Amount - 対象見積回答.MkAmount;
            decimal mmKingaku = 対象見積回答.Amount - 対象見積回答.MkAmount - 対象見積回答.Carriage;   
  
            // 同じ見積回答の受領データ
            decimal jrysKingaku = 0;
            List<OdJry> odJrys = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                odJrys = serviceClient.OdJry_GetRecordsByOdMkId(NBaseCommon.Common.LoginUser, 対象受領.OdMkID);
            }
            if (odJrys != null)
            {
                foreach (OdJry jry in odJrys)
                {
                    if (jry.OdJryID == 対象受領.OdJryID)
                    {
                        continue;
                    }
                    if (jry.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        continue;
                    }
                    //jrysKingaku += (jry.Amount - jry.NebikiAmount);
                    jrysKingaku += (jry.Amount - jry.NebikiAmount + jry.Carriage);
                }
            }
            if (mmKingaku + 上方修正しきい値 <= jrysKingaku)
            {
                // 同じ見積回答の受領データの合計金額が、既に、発注金額+上方修正しきい値より大きい場合
                // 上方修正の承認済みなので、今回はスルーする
                return true;
            }
     

            // 受領金額（今回分の受領金額）
            decimal jryKingaku = 受領合計金額();
            // 受領値引き（今回分の値引き）
            decimal nebiki = 0;
            try
            {
                nebiki = NBaseCommon.Common.金額表示を数値へ変換(textBox見積値引き_納品.Text);
            }
            catch
            {
            }
            decimal carriage = 0;
            try
            {
                carriage = NBaseCommon.Common.金額表示を数値へ変換(textBox送料運搬料_納品.Text);
            }
            catch
            {
            }
            //jryKingaku = jryKingaku - nebiki;　// 今回合計金額（納品額-値引き額）
            jryKingaku = jryKingaku - nebiki + carriage;　// 今回合計金額（納品額-値引き額+送料・運搬料）


            if (mmKingaku + 上方修正しきい値 <= jrysKingaku + jryKingaku)
            {
                checkState = (int)CHECK_STATE.上方修正;
                message = "上方修正されています。\n承認が必要となります。よろしいですか。";
            }

            return true;
        }
        #endregion
        #region private bool 受領チェック(ref int checkState, ref string message)　旧コード
        //private bool 受領チェック(ref int checkState, ref string message)
        //{
        //    //
        //    // 数量のチェック
        //    //
        //    int jryCount = 0;
        //    bool 増えた品目あり = false;
        //    bool 減った品目あり = false;

        //    if (削除受領品目s.Count > 0)
        //    {
        //        減った品目あり = true;
        //    }
        //    foreach (Item受領品目 受領品目 in 受領品目s)
        //    {
        //        if (受領品目.削除詳細品目s.Count > 0)
        //        {
        //            減った品目あり = true;
        //        }
        //        foreach (OdJryShousaiItem shousaiItem in 受領品目.詳細品目s)
        //        {
        //            if (Hachu.Common.CommonDefine.Is新規(shousaiItem.OdJryShousaiItemID))
        //            {
        //                増えた品目あり = true;
        //                jryCount += shousaiItem.JryCount;
        //            }
        //            else
        //            {
        //                jryCount += shousaiItem.JryCount;
        //                if (shousaiItem.JryCount > shousaiItem.Count)
        //                {
        //                    増えた品目あり = true;
        //                }
        //            }
        //        }
        //    }

        //    //
        //    // 金額のチェック
        //    //

        //    // 見積額
        //    decimal mmKingaku = 対象見積回答.Amount - 対象見積回答.MkAmount + 対象見積回答.Tax;

        //    // 受領金額
        //    decimal jryKingaku = 受領合計金額();
        //    // 受領値引き
        //    decimal nebiki = 0;
        //    try
        //    {
        //        nebiki = NBaseCommon.Common.金額表示を数値へ変換(textBox見積値引き_納品.Text);
        //    }
        //    catch
        //    {
        //    }
        //    // 消費税
        //    decimal tax = 0;
        //    try
        //    {
        //        tax = NBaseCommon.Common.金額表示を数値へ変換(textBox消費税_納品.Text);
        //    }
        //    catch
        //    {
        //    }
        //    jryKingaku = jryKingaku + tax - nebiki;

        //    if (jryCount == 0)
        //    {
        //        message = "受領数が入力されていません。";
        //        return false;
        //    }
        //    if (減った品目あり == true)
        //    {
        //        checkState = (int)CHECK_STATE.下方修正;
        //        message = "下方修正されています。\n （このまま実施すると変更受領となります）\n よろしいですか。";
        //    }
        //    if (増えた品目あり == true)
        //    {
        //        checkState = (int)CHECK_STATE.上方修正;
        //        message = "上方修正されています。\n 承認が必要となります。よろしいですか。";
        //    }
        //    if (jryKingaku > mmKingaku)
        //    {
        //        checkState = (int)CHECK_STATE.上方修正;
        //        message = "上方修正されています。\n 承認が必要となります。よろしいですか。";
        //    }
        //    // 数量変更なしで、金額だけ下がっている場合（これも変更受領として扱うなら以下のコードをいかす）
        //    //if (checkState == (int)CHECK_STATE.問題なし && jryKingaku < mmKingaku)
        //    //{
        //    //    checkState = (int)CHECK_STATE.下方修正;
        //    //    message = "下方修正されています。\n （このまま実施すると変更受領となります）\n よろしいですか。";
        //    //}

        //    return true;
        //}
        #endregion


        /// <summary>
        /// 分納チェック
        /// </summary>
        /// <returns></returns>
        #region private bool 分納チェック()
        private bool 分納チェック()
        {
            //
            // 数量のチェック
            //
            int 受領Count = 0;
            int 残Count = 0;

            foreach (Item受領品目 受領品目 in 受領品目s)
            {
                if (受領品目.品目.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                {
                    continue;
                }

                foreach (OdJryShousaiItem shousaiItem in 受領品目.詳細品目s)
                {
                    if (shousaiItem.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        continue;
                    }

                    //受領Count += shousaiItem.JryCount; // 2009.10.27:aki 未受領をNULLにしたので
                    if (shousaiItem.JryCount > 0)
                    {
                        受領Count += shousaiItem.JryCount;
                    }
                    if (shousaiItem.JryCount == int.MinValue) // 2009.10.27:aki 受領数が未入力の場合
                    {
                        残Count += shousaiItem.Count;
                    }
                    else if (shousaiItem.JryCount < shousaiItem.Count)
                    {
                        残Count += (shousaiItem.Count - shousaiItem.JryCount);
                    }
                }
            }

            if (受領Count > 0 && 残Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        
        
        private void button概算金額変更_Click(object sender, EventArgs e)
        {
            //概算金額変更Form form = new 概算金額変更Form(対象受領);
            //if (form.ShowDialog() == DialogResult.OK)
            //{
            //    textBox概算金額.Text = NBaseCommon.Common.金額出力(対象受領.GaisanAmount);
            //}
        }


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


        private void ResetTreeListView()
        {
            if (品目TreeList != null)
            {
                品目TreeList.Clear();
            }
            else
            {
                品目TreeList = new ItemTreeListView受領(対象受領.MsThiIraiSbtID, treeListView);
            }

            int noColumIndex = 0;
            bool viewHeader = false;
            object[,] columns = null;

            //if (対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
            //{
                noColumIndex = 0;
                viewHeader = false;
                columns = new object[,] {
                                           {"No", 65, null, HorizontalAlignment.Right},
                                           {"仕様・型式 /　詳細品目", 275, null, null},
                                           {"単位", 45, null, null},
                                           {"発注数", 52, null, HorizontalAlignment.Right},
                                           {"受領数", 52, null, HorizontalAlignment.Right},
                                           {"単価", 90, null, HorizontalAlignment.Right},
                                           {"金額", 100, null, HorizontalAlignment.Right},
                                           {"受領日", 100, null, null},
                                           {"備考（品名、規格等）", 200, null, null}
                                         };
            //}
            //else
            //{
            //    noColumIndex = 0;
            //    viewHeader = true;
            //    columns = new object[,] {
            //                               {"No", 85, null, HorizontalAlignment.Right},
            //                               {"区分 /　仕様・型式 /　詳細品目", 275, null, null},
            //                               {"単位", 45, null, null},
            //                               {"発注数", 50, null, HorizontalAlignment.Right},
            //                               {"受領数", 50, null, HorizontalAlignment.Right},
            //                               {"単価", 90, null, HorizontalAlignment.Right},
            //                               {"金額", 100, null, HorizontalAlignment.Right},
            //                               {"受領日", 100, null, null},
            //                               {"備考（品名、規格等）", 200, null, null}
            //                             };
            //}

            品目TreeList.Enum表示方式 = ItemTreeListView受領.表示方式enum.Zero以外を表示;
            品目TreeList.Enabled = true;

            品目TreeList.SetColumns(noColumIndex, columns);
            品目TreeList.TextChangeEvent += new ItemTreeListView受領.TextChangeEventHandler(金額欄再表示);
            品目TreeList.editable = true;
            if (WindowStyle != (int)WINDOW_STYLE.通常)
            {
                品目TreeList.editable = false;
            }
            if (対象受領.Status > 対象受領.OdStatusValue.Values[(int)OdJry.STATUS.未受領].Value)
            {
                品目TreeList.editable = false;
            }
            品目TreeList.AddNodes(viewHeader, 受領品目s);
        }
    }
}
