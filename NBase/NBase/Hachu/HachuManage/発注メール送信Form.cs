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
using NBaseUtil;


namespace Hachu.HachuManage
{
    public partial class 発注メール送信Form : BaseForm
    {
        public enum メール送信Enum
        {
            新規送信,
            再送信
        };

        private メール送信Enum メール送信Mode = メール送信Enum.新規送信;
        
        /// <summary>
        /// 対象見積回答
        /// </summary>
        private OdMk 対象見積回答;

        /// <summary>
        /// 発注先の担当者情報
        /// </summary>
        List<MsCustomerTantou> customerTantous = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mk"></param>
        /// <param name="mode"></param>
        #region public 発注メール送信Form(ref OdMk mk, メール送信Enum mode)
        public 発注メール送信Form(ref OdMk mk, メール送信Enum mode)
        {
            InitializeComponent();

            メール送信Mode = mode;
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
        #region private void 発注メール送信Form_Load(object sender, EventArgs e)
        private void 発注メール送信Form_Load(object sender, EventArgs e)
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
            string msgStr = "発注メール送信をキャンセルします。よろしいですか？";
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
        /// 「送信」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button送信_Click(object sender, EventArgs e)
        private void button送信_Click(object sender, EventArgs e)
        {
            if (発注メール処理() == false)
            {
                return;
            }

            // Formを閉じる
            DialogResult = DialogResult.OK;
            Close();
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
            this.Text = NBaseCommon.Common.WindowTitle("JM040408", "発注メール送信", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            //=========================================
            // ＤＢから必要な情報を取得する
            //=========================================
            List<OdHachuMail> hacuMail = null;
            MsCustomer customer = null;
            OdMm odMm = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                hacuMail = serviceClient.OdHachuMail_GetRecords(NBaseCommon.Common.LoginUser, 対象見積回答.OdMkID); 
                
                // 顧客担当者
                customerTantous = serviceClient.MsCustomerTantou_GetRecordsByCustomerID(NBaseCommon.Common.LoginUser,対象見積回答.MsCustomerID);

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
            comboBox担当者.AutoCompleteCustomSource.Clear();
            comboBox担当者.Items.Clear();
            foreach (MsCustomerTantou ct in customerTantous)
            {
                comboBox担当者.AutoCompleteCustomSource.Add(ct.Name);
                comboBox担当者.Items.Add(ct.Name);
            }
            if (hacuMail.Count > 0)
            {
                comboBox担当者.Text = hacuMail[0].Tantousha;
                // メールアドレス
                textBoxメールアドレス.Text = hacuMail[0].TantouMailAddress;
                // メール件名
                textBoxメール件名.Text = hacuMail[0].Subject;
            }
            else
            {
                comboBox担当者.Text = 対象見積回答.Tantousha;
                // メールアドレス
                textBoxメールアドレス.Text = 対象見積回答.TantouMailAddress;
                // メール件名
                textBoxメール件名.Text = "";
            }

            MsCustomerTantou tanto = null;
            foreach (MsCustomerTantou ct in customerTantous)
            {
                if (ct.Name == comboBox担当者.Text)
                {
                    tanto = ct;
                }
            }
            if (tanto != null)
            {
                // 電話番号
                if (tanto.Tel != null && tanto.Tel.Length > 0)
                {
                    textBoxTelNo.Text = tanto.Tel;
                }
                // FAX番号
                if (tanto.Fax != null && tanto.Fax.Length > 0)
                {
                    textBoxFaxNo.Text = tanto.Fax;
                }
            }
            else
            {
                // 電話番号
                textBoxTelNo.Text = customer.Tel;
                // FAX番号
                textBoxFaxNo.Text = customer.Fax;
            }

            // 希望納期
            dateTimePicker希望納期.Text = 対象見積回答.Kiboubi.ToShortDateString();
            // 送り先
            textBox納品場所.Text = odMm.Okurisaki;
            // 備考
            textBox備考.Text = "";
        }
        #endregion

        /// <summary>
        /// 発注メール処理
        /// </summary>
        /// <returns></returns>
        #region private bool 発注メール処理()
        private bool 発注メール処理()
        {
            this.Cursor = Cursors.WaitCursor;

            bool ret = false;
            try
            {
                OdMk 見積回答 = new OdMk();
                if (入力情報の取得確認(ref 見積回答) == false)
                {
                    return false;
                }

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
                    ret = serviceClient.BLC_注文書_準備(NBaseCommon.Common.LoginUser, 見積回答.OdMkID, telNo, faxNo, nouki, nouhinBasho);
                    if (ret == false)
                    {
                        MessageBox.Show("発注メールの送信準備に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return true;
                    }
                }

                // メールアドレス入力されている場合、メール送信を実施する
                if (見積回答.TantouMailAddress.Length > 0)
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        string errMessage = "";
                        ret = serviceClient.BLC_発注メール送信(
                                    NBaseCommon.Common.LoginUser,
                                    見積回答.OdMkID,
                                    見積回答.MsCustomerName,
                                    見積回答.Tantousha,
                                    見積回答.TantouMailAddress,
                                    見積回答.Subject,
                                    見積回答.HachuNo,
                                    見積回答.MsVesselName,
                                    telNo,
                                    faxNo,
                                    bikou,
                                    ref errMessage);
                        if (ret)
                        {
                            MessageBox.Show("発注メールを送信しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        else
                        {
                            MessageBox.Show(errMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return true;
                        }
                    }
                }

                対象見積回答.Nouki = nouki;
                対象見積回答.Kiboubi = nouki;
            }
            catch (Exception ex)
            {
                MessageBox.Show("発注メールの送信に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 入力情報の取得確認
        /// </summary>
        /// <returns></returns>
        #region private bool 入力情報の取得確認(OdMk 見積回答)
        private bool 入力情報の取得確認(ref OdMk 見積回答)
        {
            string errMessage = "";

            // 担当者
            string tantou = comboBox担当者.Text;
            if (tantou == null || tantou.Length == 0)
            {
                errMessage += "・担当者を入力してください。\n";
            }
            // メールアドレス
            string mailAddress = textBoxメールアドレス.Text;
            if (mailAddress != null && mailAddress.Length > 0)
            {
                try
                {
                    MailAddress ma = new MailAddress(mailAddress);
                }
                catch
                {
                    errMessage += "・メールアドレスが不正です。\n";
                }
            }
            else
            {
                errMessage += "・メールアドレスを入力してください。\n";
            }
            // メール件名
            string subject = textBoxメール件名.Text;
            if (subject == null || subject.Length == 0)
            {
                errMessage += "・メール件名を入力してください。\n";
            }
            else if (subject.Length > 50)
            {
                errMessage += "・メール件名は５０文字までで入力してください。\n";
            }
            if (errMessage.Length > 0)
            {
                errMessage = "入力項目に不備があります。\n====================================\n" + errMessage;
                MessageBox.Show(errMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            見積回答 = 対象見積回答.Clone();
            見積回答.Tantousha = tantou;
            見積回答.TantouMailAddress = mailAddress;
            見積回答.Subject = subject;

            return true;
        }
        #endregion

        /// <summary>
        /// メールアドレスをセットする
        /// </summary>
        #region private void setMailAddress()
        private void setMailAddress()
        {
            string tantouName = comboBox担当者.Text;
            foreach (MsCustomerTantou ct in customerTantous)
            {
                if (ct.Name == tantouName)
                {
                    textBoxメールアドレス.Text = ct.MailAddress;
                    if (ct.Tel != null && ct.Tel.Length > 0)
                    {
                        textBoxTelNo.Text = ct.Tel;
                    }
                    if (ct.Fax != null && ct.Fax.Length > 0)
                    {
                        textBoxFaxNo.Text = ct.Fax;
                    }
                    break;
                }
            }
        }
        #endregion
   }
}
