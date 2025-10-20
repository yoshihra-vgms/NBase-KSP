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
using NBaseUtil;


namespace Hachu.HachuManage
{
    public partial class 振替取立Form : BaseForm
    {
        /// <summary>
        /// 対象振替取立
        /// </summary>
        private OdFurikaeToritate 対象振替取立;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <param name="info"></param>
        #region public 振替取立Form(int windowStyle, ListInfo振替取立 info)
        public 振替取立Form(int windowStyle, ListInfo振替取立 info)
        {
            InitializeComponent();

            this.WindowStyle = windowStyle;
            対象振替取立 = info.info;
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
        #region private void 振替取立Form_Load(object sender, EventArgs e)
        private void 振替取立Form_Load(object sender, EventArgs e)
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
            BaseFormClosing();
            Close();
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
            string btnTxt = button更新.Text;
            if (MessageBox.Show("取立・振替項目を" + btnTxt + "します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (振替取立作成処理(btnTxt) == false)
            {
                return;
            }
            MessageBox.Show("取立・振替項目を" + btnTxt + "しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ListInfo振替取立 info = new ListInfo振替取立();
            info.info = 対象振替取立;
            InfoUpdating(info);

            // Formを閉じる
            Close();
        }
        #endregion

        /// <summary>
        /// 「削除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button削除_Click(object sender, EventArgs e)
        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("この取立・振替項目を削除します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (削除処理() == false)
            {
                return;
            }
            MessageBox.Show("削除しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ListInfo振替取立 info = new ListInfo振替取立();
            info.Remove = true;
            info.info = 対象振替取立;
            InfoUpdating(info);

            // Formを閉じる
            Close();
        }
        #endregion

        /// <summary>
        /// 「数量」「単価」の入力キーを制限する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
        #endregion

        /// <summary>
        /// 「数量」「単価」の入力後、金額欄の再計算、再表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox_KeyUp(object sender, KeyEventArgs e)
        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            金額欄再表示();
        }
        #endregion

        /// <summary>
        /// 「単価」にフォーカスインした場合、単価を数字のみに
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox単価_Enter(object sender, EventArgs e)
        private void textBox単価_Enter(object sender, EventArgs e)
        {
            try
            {
                decimal value = NBaseCommon.Common.金額表示を数値へ変換(textBox単価.Text);
                textBox単価.Text = value.ToString();
            }
            catch
            {
            }

        }
        #endregion

        /// <summary>
        /// 「単価」からフォーカスアウトした場合、単価を金額フォーマットへ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox単価_Leave(object sender, EventArgs e)
        private void textBox単価_Leave(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            try
            {
                decimal value = decimal.Parse(textBox単価.Text);
                textBox単価.Text = NBaseCommon.Common.金額出力(value);
            }
            catch
            {
            }
        }
        #endregion

        /// <summary>
        /// 「＋－」の選択時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBoxPlusMinus_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBoxPlusMinus_SelectedIndexChanged(object sender, EventArgs e)
        {
            金額欄再表示();
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
            List<MsCustomer> customers = null;
            List<MsItemSbt> MsItemSbts = null;
            List<MsNyukyoKamoku> nyukyoKamokus = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                customers = serviceClient.MsCustomer_GetRecords(NBaseCommon.Common.LoginUser);
                MsItemSbts = serviceClient.MsItemSbt_GetRecords(NBaseCommon.Common.LoginUser);
                nyukyoKamokus = serviceClient.MsNyukyoKamoku_GetRecords(NBaseCommon.Common.LoginUser);
            }


            this.Text = NBaseCommon.Common.WindowTitle("JM041402", "取立・振替項目", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            textBox手配依頼種.Text = 対象振替取立.MsThiIraiSbtName;
            textBox手配依頼詳細種別.Text = 対象振替取立.MsThiIraiShousaiName;
            textBox船.Text = 対象振替取立.MsVesselName;

            panel小修理.Visible = false;
            panel入渠.Visible = false;
            if (対象振替取立.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
            {
                if (対象振替取立.MsThiIraiShousaiID == NBaseCommon.Common.MsThiIraiShousai_小修理ID)
                {
                    panel小修理.Visible = true;
                    panel小修理.Location = new Point(313, 61);
                    comboBox区分.Items.Clear();
                    foreach (MsItemSbt itemSbt in MsItemSbts)
                    {
                        comboBox区分.Items.Add(itemSbt);
                        if (itemSbt.MsItemSbtID == 対象振替取立.MsItemSbtID)
                        {
                            comboBox区分.SelectedItem = itemSbt;
                        }
                    }
                }
                else
                {
                    panel入渠.Visible = true;
                    panel入渠.Location = new Point(313, 61);
                    comboBox入渠科目.Items.Clear();
                    foreach (MsNyukyoKamoku nk in nyukyoKamokus)
                    {
                        comboBox入渠科目.Items.Add(nk);
                        if (nk.MsNyukyoKamokuID == 対象振替取立.MsNyukyoKamokuID)
                        {
                            comboBox入渠科目.SelectedItem = nk;
                        }
                    }
                }
            }

            try
            {
                dateTimePicker発注日.Text = 対象振替取立.HachuDate.ToShortDateString();
            }
            catch
            {
            }
            textBox項目.Text = 対象振替取立.Koumoku;

            comboBox業者.Items.Clear();
            foreach (MsCustomer c in customers)
            {
                // 2013.08.07 : 取引先のみセットする
                //comboBox取引先.Items.Add(c);
                //comboBox取引先.AutoCompleteCustomSource.Add(c.CustomerName);
                //if (c.Shubetsu == (int)MsCustomer.種別.取引先)
                if (c.Is取引先())
                {
                    comboBox業者.Items.Add(c);
                    comboBox業者.AutoCompleteCustomSource.Add(c.CustomerName);
                }
                if (c.MsCustomerID == 対象振替取立.MsCustomerID)
                {
                    comboBox業者.SelectedItem = c;
                }
            }
            // 2010.06.28 業者が削除されている場合の対応
            if (対象振替取立.MsCustomerID != null && 対象振替取立.MsCustomerID.Length > 0 && comboBox業者.SelectedItem == null)
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    MsCustomer customer = serviceClient.MsCustomer_GetRecord(NBaseCommon.Common.LoginUser, 対象振替取立.MsCustomerID);
                    if (customer != null)
                    {
                        comboBox業者.Items.Add(customer);
                        comboBox業者.AutoCompleteCustomSource.Add(customer.CustomerName);
                        comboBox業者.SelectedItem = customer;
                    }
                }
            }


            try
            {
                if (対象振替取立.Count > int.MinValue)
                {
                    textBox数量.Text = 対象振替取立.Count.ToString();
                }
            }
            catch
            {
            }
            if (対象振替取立.Tanka > decimal.MinusOne || 対象振替取立.Tanka == decimal.MinValue)
            {
                comboBoxPlusMinus.SelectedIndex = 0;
            }
            else
            {
                comboBoxPlusMinus.SelectedIndex = 1;
            }
            try
            {
                if (対象振替取立.Tanka > decimal.MinValue)
                {
                    textBox単価.Text = NBaseCommon.Common.金額出力(Math.Abs(対象振替取立.Tanka));
                }
            }
            catch
            {
            }
            try
            {
                if (対象振替取立.Amount > decimal.MinValue)
                {
                    textBox金額.Text = NBaseCommon.Common.金額出力2(対象振替取立.Amount);
                }
            }
            catch
            {
            }
            try
            {
                dateTimePicker完了日.Text = 対象振替取立.Kanryobi.ToShortDateString();
            }
            catch
            {
            }
            try
            {
                dateTimePicker請求書日.Text = 対象振替取立.Seikyushobi.ToShortDateString();
            }
            catch
            {
            }
            try
            {
                dateTimePicker起票日.Text = 対象振替取立.Kihyobi.ToShortDateString();
            }
            catch
            {
            }

            textBox備考.Text = 対象振替取立.Bikou;

            button更新.Enabled = true;
            if (対象振替取立.OdFurikaeToritateID == null)
            {
                button更新.Text = "登録";
                button更新.Enabled = true;
                button削除.Visible = false;
            }
            else
            {
                button更新.Text = "更新";
                button削除.Visible = true;
                button削除.Enabled = true;
            }
        }
        #endregion

        /// <summary>
        /// 振替取立作成処理
        /// </summary>
        /// <returns></returns>
        #region private bool 振替取立作成処理(string btnTxt)
        private bool 振替取立作成処理(string btnTxt)
        {
            try
            {
                if (入力情報の取得確認() == false)
                {
                    return false;
                }

                bool ret = false;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    if (対象振替取立.OdFurikaeToritateID == null)
                    {
                        対象振替取立.OdFurikaeToritateID = Hachu.Common.CommonDefine.新規ID(false);
                        対象振替取立.CreateDate = DateTime.Now;
                        ret = serviceClient.OdFurikaeToritate_Insert(NBaseCommon.Common.LoginUser, 対象振替取立);
                        if (ret == false)
                        {
                            対象振替取立.OdFurikaeToritateID = null;
                        }
                    }
                    else
                    {
                        ret = serviceClient.OdFurikaeToritate_Update(NBaseCommon.Common.LoginUser, 対象振替取立);
                    }
                    if (ret == false)
                    {
                        MessageBox.Show("取立・振替項目の" + btnTxt + "に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        対象振替取立 = serviceClient.OdFurikaeToritate_GetRecord(NBaseCommon.Common.LoginUser, 対象振替取立.OdFurikaeToritateID);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("取立・振替項目の" + btnTxt + "に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <returns></returns>
        #region private bool 削除処理()
        private bool 削除処理()
        {
            try
            {
                bool ret = false;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    対象振替取立.CancelFlag = NBaseCommon.Common.CancelFlag_キャンセル;
                    ret = serviceClient.OdFurikaeToritate_Update(NBaseCommon.Common.LoginUser, 対象振替取立);
                    if (ret == false)
                    {
                        対象振替取立.CancelFlag = NBaseCommon.Common.CancelFlag_有効;
                        MessageBox.Show("削除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("削除に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            #region 入力情報の確認
            // 発注日
            DateTime hachuDate = DateTime.MinValue;
            try
            {
                hachuDate = DateTime.Parse(dateTimePicker発注日.Text);
            }
            catch
            {
                errMessage += "・発注日が不正です。\n";
            }
            // 区分
            MsItemSbt itemSbt = null;
            if (panel小修理.Visible == true)
            {
                if (comboBox区分.SelectedItem is MsItemSbt)
                {
                    itemSbt = comboBox区分.SelectedItem as MsItemSbt;
                }
                else
                {
                    errMessage += "・区分を選択してください。\n";
                }
            }
            // 入渠科目
            MsNyukyoKamoku nyukyoKamoku = null;
            if (panel入渠.Visible == true)
            {
                if (comboBox入渠科目.SelectedItem is MsNyukyoKamoku)
                {
                    nyukyoKamoku = comboBox入渠科目.SelectedItem as MsNyukyoKamoku;
                }
                else
                {
                    errMessage += "・入渠科目を選択してください。\n";
                }
            }
            // 項目
            string koumoku = "";
            if (textBox項目.Text.Length == 0)
            {
                errMessage += "・項目を入力してください。\n";
            }
            else
            {
                koumoku = textBox項目.Text;
                if (koumoku.Length > 50)
                {
                    errMessage += "・項目は５０文字までで入力してください。\n";
                }
            }
            // 業者
            MsCustomer customer = null;
            if (comboBox業者.SelectedItem is MsCustomer)
            {
                customer = comboBox業者.SelectedItem as MsCustomer;
            }
            else
            {
                errMessage += "・業者を選択してください。\n";
            }
            // 数量
            int count = 0;
            if (textBox数量.Text.Length > 0)
            {
                try
                {
                    count = int.Parse(textBox数量.Text);
                }
                catch
                {
                    errMessage += "・数量が不正です。\n";
                }
            }
            else
            {
                errMessage += "・数量を入力してください。\n";
            }
            // 単価
            decimal tanka = 0;
            if (textBox数量.Text.Length > 0)
            {
                try
                {
                    tanka = NBaseCommon.Common.金額表示を数値へ変換(textBox単価.Text);
                }
                catch
                {
                    errMessage += "・単価が不正です。\n";
                }
            }
            else
            {
                errMessage += "・単価を入力してください。\n";
            }
            if (comboBoxPlusMinus.SelectedIndex == 1)
            {
                // マイナスにする
                tanka = 0 - tanka;
            }

            // 金額
            decimal amount = 0;
            try
            {
                amount = count * tanka;
            }
            catch
            {
            }
            // 完了日
            DateTime kanryoDate = DateTime.MinValue;
            try
            {
                kanryoDate = DateTime.Parse(dateTimePicker完了日.Text);
            }
            catch
            {
                errMessage += "・完了日が不正です。\n";
            }
            // 請求書日
            DateTime seikyusyoDate = DateTime.MinValue;
            try
            {
                seikyusyoDate = DateTime.Parse(dateTimePicker請求書日.Text);
            }
            catch
            {
                errMessage += "・請求書日が不正です。\n";
            }
            // 起票日
            DateTime kihyoDate = DateTime.MinValue;
            try
            {
                kihyoDate = DateTime.Parse(dateTimePicker起票日.Text);
            }
            catch
            {
                errMessage += "・起票日が不正です。\n";
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
            #endregion
            if (errMessage.Length > 0)
            {
                errMessage = "入力項目に不備があります。\n====================================\n" + errMessage;
                MessageBox.Show(errMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            //=========================================================================
            // セットする
            //=========================================================================
            対象振替取立.HachuDate = hachuDate;
            if (itemSbt != null)
            {
                対象振替取立.MsItemSbtID = itemSbt.MsItemSbtID;
                対象振替取立.MsItemSbtName = itemSbt.ItemSbtName;
            }
            if (nyukyoKamoku != null)
            {
                対象振替取立.MsNyukyoKamokuID = nyukyoKamoku.MsNyukyoKamokuID;
                対象振替取立.MsNyukyoKamokuName = nyukyoKamoku.NyukyoKamokuName;
            }
            対象振替取立.Koumoku = koumoku;
            対象振替取立.MsCustomerID = customer.MsCustomerID;
            対象振替取立.MsCustomerName = customer.CustomerName;
            対象振替取立.Count = count;
            対象振替取立.Tanka = tanka;
            対象振替取立.Amount = amount;
            対象振替取立.Kanryobi = kanryoDate;
            対象振替取立.Seikyushobi = seikyusyoDate;
            対象振替取立.Kihyobi = kihyoDate;
            対象振替取立.Bikou = bikou;
            対象振替取立.RenewDate = DateTime.Now;
            

         
            return true;
        }
        #endregion

        /// <summary>
        /// 「金額」を再計算、再表示する
        /// </summary>
        #region public void 金額欄再表示()
        public void 金額欄再表示()
        {
            int count = 0;
            try
            {
                count = int.Parse(textBox数量.Text);
            }
            catch
            {
            }
            decimal tanka = 0;
            try
            {
                tanka = NBaseCommon.Common.金額表示を数値へ変換(textBox単価.Text);
            }
            catch
            {
            }

            if (comboBoxPlusMinus.SelectedIndex == 0)
            {
                textBox金額.Text = NBaseCommon.Common.金額出力2(count * tanka);
            }
            else
            {
                textBox金額.Text = NBaseCommon.Common.金額出力2(count * (-tanka));
            }
        }
        #endregion
    }
}
