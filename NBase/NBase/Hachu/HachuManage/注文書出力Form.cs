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
using System.Net.Mail;
using Hachu.Reports;
using NBaseUtil;


namespace Hachu.HachuManage
{
    public partial class 注文書出力Form : BaseForm
    {
        
        /// <summary>
        /// 対象見積回答
        /// </summary>
        private OdMk 対象見積回答;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mk"></param>
        /// <param name="mode"></param>
        #region public 注文書出力Form(OdMk mk)
        public 注文書出力Form(OdMk mk)
        {
            InitializeComponent();

            対象見積回答 = mk;
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
        #region private void 注文書出力Form_Load(object sender, EventArgs e)
        private void 注文書出力Form_Load(object sender, EventArgs e)
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
            string msgStr = "注文書の出力をキャンセルします。よろしいですか？";
            if (MessageBox.Show(msgStr, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // Formを閉じる
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion

        /// <summary>
        /// 「出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button出力_Click(object sender, EventArgs e)
        private void button出力_Click(object sender, EventArgs e)
        {
            注文書出力処理();
            
            // Formを閉じる
            DialogResult = DialogResult.OK;
            Close();
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
            List<MsCustomerTantou> customerTantous = null;
            MsCustomer customer = null;
            OdMm odMm = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                // 顧客担当者
                customerTantous = serviceClient.MsCustomerTantou_GetRecordsByCustomerID(NBaseCommon.Common.LoginUser, 対象見積回答.MsCustomerID);

                // 顧客
                customer = serviceClient.MsCustomer_GetRecord(NBaseCommon.Common.LoginUser, 対象見積回答.MsCustomerID);

                // 見積依頼
                odMm = serviceClient.OdMm_GetRecord(NBaseCommon.Common.LoginUser, 対象見積回答.OdMmID);
            }

            //=========================================
            // 対象見積回答の内容を画面にセットする
            //=========================================

            // 発注番号
            textBox発注番号.Text = 対象見積回答.HachuNo;
            // 発注先
            textBox発注先.Text = 対象見積回答.MsCustomerName;
            // 担当者
            MsCustomerTantou tanto = null;
            foreach (MsCustomerTantou ct in customerTantous)
            {
                if (ct.Name == 対象見積回答.Tantousha)
                {
                    tanto = ct;
                }
            }
            if (tanto != null)
            {
                // 担当者情報がある場合、その電話番号、FAX番号をセットする
                if (tanto.Tel != null && tanto.Tel.Length > 0)
                {
                    textBoxTelNo.Text = tanto.Tel;
                }
                if (tanto.Fax != null && tanto.Fax.Length > 0)
                {
                    textBoxFaxNo.Text = tanto.Fax;
                }
            }
            // 担当者情報に電話番号、FAX番号がなかった場合、顧客の電話番号、FAX番号をセットする
            if (textBoxTelNo.Text == null || textBoxTelNo.Text.Length == 0)
            {
                textBoxTelNo.Text = customer.Tel;
            }
            if (textBoxFaxNo.Text == null || textBoxFaxNo.Text.Length == 0)
            {
                textBoxFaxNo.Text = customer.Fax;
            }

            // 希望納期
            dateTimePicker希望納期.Text = 対象見積回答.Nouki.ToShortDateString();
            // 送り先
            textBox納品場所.Text = odMm.Okurisaki;
            // 備考
            textBox備考.Text = "";
        }
        #endregion

        /// <summary>
        /// 注文書出力処理
        /// </summary>
        /// <returns></returns>
        #region private bool 注文書出力処理()
        private void 注文書出力処理()
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

            string telNo = textBoxTelNo.Text;
            string faxNo = textBoxFaxNo.Text;
            DateTime nouki = DateTime.MinValue;
            try
            {
                nouki = DateTime.Parse(dateTimePicker希望納期.Text);
            }
            catch
            {
            }
            string nouhinBasho = textBox納品場所.Text;
            //string bikou = textBox備考.Text;
            string bikou = StringUtils.Escape(textBox備考.Text);

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                bool ret = serviceClient.BLC_注文書_準備(NBaseCommon.Common.LoginUser, 対象見積回答.OdMkID, telNo, faxNo, nouki, nouhinBasho);
                if (ret == false)
                {
                    MessageBox.Show("注文書の出力準備に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            注文書 chumon = new 注文書();
            chumon.Output(対象見積回答.OdMkID,telNo,faxNo,bikou);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }
        #endregion
    }
}
