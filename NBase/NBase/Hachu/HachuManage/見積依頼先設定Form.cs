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


namespace Hachu.HachuManage
{
    public partial class 見積依頼先設定Form : BaseForm
    {
        /// <summary>
        /// 対象手配依頼
        /// </summary>
        private OdThi 対象手配依頼;
        
        /// <summary>
        /// 対象見積依頼
        /// </summary>
        private OdMm 対象見積依頼;

        /// <summary>
        /// 対象見積回答
        /// </summary>
        private OdMk 対象見積回答;

        /// <summary>
        /// 対象見積依頼の品目
        /// </summary>
        private List<Item見積依頼品目> 見積品目s;

        /// <summary>
        /// 見積依頼先
        /// </summary>
        List<MsCustomer> 見積依頼先s = null;


        /// <summary>
        /// 
        /// </summary>
        ItemTreeListView見積依頼先設定 品目TreeList;


        /// <summary>
        /// 「見積作成」ボタンクリックで作成される見積回答
        /// </summary>
        public OdMk 見積回答;

        /// <summary>
        /// 「見積作成」ボタンクリックで作成される見積回答の品目
        /// </summary>
        public List<Item見積回答品目> 見積回答品目s;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="thi"></param>
        /// <param name="mm"></param>
        /// <param name="mmItems"></param>
        #region public 見積依頼先設定Form(OdThi thi, OdMm mm, List<見積品目> mmItems)
        public 見積依頼先設定Form(OdThi thi, OdMm mm, List<Item見積依頼品目> mmItems)
        {
            InitializeComponent();

            対象手配依頼 = thi;
            対象見積依頼 = mm;
            見積品目s = mmItems;

            singleLineCombo見積依頼先.selected += new NBaseUtil.SingleLineCombo.SelectedEventHandler(comboBox見積依頼先_SelectedIndexChanged);
        }
        #endregion 

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="thi"></param>
        /// <param name="mm"></param>
        /// <param name="mmItems"></param>
        #region public 見積依頼先設定Form(OdMk mk, OdMm mm, List<見積品目> mmItems)
        public 見積依頼先設定Form(OdMk mk, OdMm mm, List<Item見積依頼品目> mmItems)
        {
            InitializeComponent();

            対象見積回答 = mk;
            対象見積依頼 = mm;
            見積品目s = mmItems;

            singleLineCombo見積依頼先.selected += new NBaseUtil.SingleLineCombo.SelectedEventHandler(comboBox見積依頼先_SelectedIndexChanged);
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
        #region private void 見積依頼先設定Form_Load(object sender, EventArgs e)
        private void 見積依頼先設定Form_Load(object sender, EventArgs e)
        {
            Formに情報をセットする();
        }
        #endregion

        /// <summary>
        /// 「キャンセル」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonキャンセル_Click(object sender, EventArgs e)
        private void buttonキャンセル_Click(object sender, EventArgs e)
        {
            string msgStr = "";
            if (対象見積回答 == null)
            {
                msgStr = "見積依頼作成をキャンセルします。よろしいですか？";
            }
            else
            {
                msgStr = "見積依頼メール送信をキャンセルします。よろしいですか？";
            }
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
        /// 「見積依頼作成」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button見積依頼作成_Click(object sender, EventArgs e)
        private void button見積依頼作成_Click(object sender, EventArgs e)
        {
            if (見積依頼作成処理() == false)
            {
                return;
            }

            // Formを閉じる
            DialogResult = DialogResult.OK;
            Close();
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

                textBox電話番号.Text = selectedCustomer.Tel;
                textBoxFAX番号.Text = selectedCustomer.Fax;
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
        /// 「すべて選択」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button選択_Click(object sender, EventArgs e)
        private void button選択_Click(object sender, EventArgs e)
        {
            品目TreeList.AllCheck();
        }
        #endregion

        /// <summary>
        /// 「すべて解除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button解除_Click(object sender, EventArgs e)
        private void button解除_Click(object sender, EventArgs e)
        {
            品目TreeList.AllUncheck();
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
            if (対象見積回答 == null)
            {
                //=========================================
                // 「見積依頼」-「見積依頼作成」からの呼び出し
                //=========================================
                this.Text = NBaseCommon.Common.WindowTitle("JM040303", "見積依頼作成", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

                #region
                //=========================================
                // ＤＢから必要な情報を取得する
                //=========================================
                見積依頼先s = null;
                List<MsCustomerTantou> customerTantous = null;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    // 顧客
                    見積依頼先s = serviceClient.MsCustomer_GetRecords(NBaseCommon.Common.LoginUser);
                    // 顧客担当者
                    customerTantous = serviceClient.MsCustomerTantou_GetRecords(NBaseCommon.Common.LoginUser);
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

                //=========================================
                // 対象見積依頼の内容を画面にセットする
                //=========================================

                // 見積依頼先
                //comboBox見積依頼先.Location = new Point(106, 15);
                //comboBox見積依頼先.Items.Clear();
                textBox見積依頼先.Visible = false;
                singleLineCombo見積依頼先.Location = new Point(106, 15);
                singleLineCombo見積依頼先.Width = 214;
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
                // 見積回答期限
                textBox見積回答期限.Text = 対象見積依頼.MmKigen;
                // メールアドレス
                textBoxメールアドレス.Text = "";
                // 見積依頼日
                dateTimePicker見積依頼日.Text = 対象見積依頼.MmDate.ToShortDateString(); // 初期値は、見積依頼の見積依頼日
                // メール件名
                textBoxメール件名.Text = "";
                // 手配内容
                textBox手配内容.Text = 対象手配依頼.Naiyou;  // 手配依頼から
                // 備考 
                textBox備考.Text = 対象手配依頼.Bikou;   // 手配依頼から
                // 希望納期
                dateTimePicker希望納期.Text = DateTime.Now.ToShortDateString();

                // 電話番号
                textBox電話番号.Text = "";
                // FAX番号
                textBoxFAX番号.Text = "";

                #endregion
            }
            else
            {
                //=========================================
                // 「見積回答」-「見積依頼メール送信」からの呼び出し
                //=========================================
                this.Text = NBaseCommon.Common.WindowTitle("JM040406", "見積依頼メール送信", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

                #region
                //=========================================
                // 対象見積依頼の内容を画面にセットする
                //=========================================
                // 見積依頼先
                textBox見積依頼先.Location = new Point(106, 15);
                //comboBox見積依頼先.Visible = false;
                singleLineCombo見積依頼先.Visible = false;
                textBox見積依頼先.Text = 対象見積回答.MsCustomerName;
                // 担当者
                comboBox担当者.Text = 対象見積回答.Tantousha;
                // メールアドレス
                textBoxメールアドレス.Text = 対象見積回答.TantouMailAddress;
                // 見積依頼日
                dateTimePicker見積依頼日.Text = 対象見積回答.MmDate.ToShortDateString();
                // 見積回答期限
                textBox見積回答期限.Text = 対象見積回答.MkKigen;
                // メール件名
                textBoxメール件名.Text = "";
                // 手配内容
                textBox手配内容.Text = 対象見積回答.OdThiNaiyou;
                // 備考 
                textBox備考.Text = 対象見積回答.OdThiBikou;
                // 希望納期
                dateTimePicker希望納期.Text = 対象見積回答.Kiboubi.ToShortDateString();

                // 電話番号
                textBox電話番号.Text = "";
                // FAX番号
                textBoxFAX番号.Text = "";

                button選択.Visible = false;
                button解除.Visible = false;

                button見積依頼作成.Text = "送信";
                #endregion
            }
            InitItemTreeListView();
        }
        #endregion

        /// <summary>
        /// 「品目/詳細品目一覧」初期化
        /// </summary>
        #region private void InitItemTreeListView()
        private void InitItemTreeListView()
        {
            int noColumIndex = 0;
            bool checkBoxes = false;
            bool viewHeader = false;
            object[,] columns = null;
            string msThiIraiSbtID = null;
            if (対象見積回答 == null)
            {
                checkBoxes = true;
                noColumIndex = 1;

                msThiIraiSbtID = 対象手配依頼.MsThiIraiSbtID;
                //if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                if (msThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                {
                    viewHeader = false;
                    columns = new object[,] {
                                           {"見積対象", 65, null, null},
                                           {"No", 35, null, HorizontalAlignment.Right},
                                           {"仕様・型式 /　詳細品目", 275, null, null},
                                           {"単位", 45, null, null},
                                           {"依頼数", 52, null, HorizontalAlignment.Right},
                                         };
                }
                else
                {
                    viewHeader = true;
                    columns = new object[,] {
                                           {"見積対象", 85, null, null},
                                           {"No", 35, null, HorizontalAlignment.Right},
                                           {"区分 /　仕様・型式 /　詳細品目", 275, null, null},
                                           {"単位", 45, null, null},
                                           {"依頼数", 52, null, HorizontalAlignment.Right},
                                           {"添付", 40, null, HorizontalAlignment.Center},
                                           {"添付対象", 70, null, HorizontalAlignment.Center},
                                         };
                }
            }
            else
            {
                checkBoxes = false;
                noColumIndex = 0;
                msThiIraiSbtID = 対象見積回答.MsThiIraiSbtID;
                //if (対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                if (msThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                {
                    viewHeader = false;
                    columns = new object[,] {
                                               {"No", 65, null, HorizontalAlignment.Right},
                                               {"仕様・型式 /　詳細品目", 275, null, null},
                                               {"単位", 45, null, null},
                                               {"依頼数", 52, null, HorizontalAlignment.Right},
                                             };
                }
                else
                {
                    viewHeader = true;
                    columns = new object[,] {
                                               {"No", 85, null, HorizontalAlignment.Right},
                                               {"区分 /　仕様・型式 /　詳細品目", 275, null, null},
                                               {"単位", 45, null, null},
                                               {"依頼数", 52, null, HorizontalAlignment.Right},
                                               {"添付", 40, null, HorizontalAlignment.Center},
                                               {"添付対象", 70, null, HorizontalAlignment.Center},
                                             };
                }
            }
            //品目TreeList = new ItemTreeListView見積依頼先設定(treeListView, checkBoxes);
            //品目TreeList = new ItemTreeListView見積依頼先設定(対象手配依頼.MsThiIraiSbtID, treeListView, checkBoxes);
            品目TreeList = new ItemTreeListView見積依頼先設定(msThiIraiSbtID, treeListView, checkBoxes);
            品目TreeList.SetColumns(noColumIndex, columns);
            品目TreeList.LinkClickEvent += new ItemTreeListView.LinkClickEventHandler(View添付ファイル);

            foreach (Item見積依頼品目 品目 in 見積品目s)
            {
                if (品目.品目.CancelFlag == 1)
                    continue;
                if (品目.品目.OdAttachFileID != null && 品目.品目.OdAttachFileID.Length > 0)
                {
                    品目.品目.IsAttached = true;
                }
                foreach (OdMmShousaiItem 詳細品目 in 品目.詳細品目s)
                {
                    if (詳細品目.CancelFlag == 1)
                        continue;
                    if (詳細品目.OdAttachFileID != null && 詳細品目.OdAttachFileID.Length > 0)
                    {
                        詳細品目.IsAttached = true;
                    }
                }
            }
            品目TreeList.AddNodes(viewHeader, 見積品目s);
        }
        #endregion

        /// <summary>
        /// 見積依頼作成処理
        /// </summary>
        /// <returns></returns>
        #region private bool 見積依頼作成処理()
        private bool 見積依頼作成処理()
        {
            bool ret = false;
            try
            {
                // 見積依頼作成 = 見積回答を作成
                見積回答 = new OdMk();
                if (入力情報の取得確認() == false)
                {
                    return false;
                }

                // 見積依頼品目/詳細品目の内、チェックされているもののみ見積回答へ
                // 2013.02 2013年度改造
                //見積回答品目s = 品目TreeList.GetCheckedNodes();
                //if (対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID)
                if ((対象見積回答 == null && 対象手配依頼.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID) ||
                    (対象見積回答 != null && 対象見積回答.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID))
                {
                    見積回答品目s = Build見積回答品目();
                }
                else
                {
                    見積回答品目s = 品目TreeList.GetCheckedNodes();
                }
                if (対象見積回答 == null)
                {
                    ret = 見積回答更新処理.新規(ref 見積回答, 見積回答品目s);
                    if (ret == false)
                    {
                        MessageBox.Show("更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                // メールアドレス入力されている場合、メール送信を実施する
                if (見積回答.TantouMailAddress.Length > 0)
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        string errMessage = "";
                        ret = serviceClient.BLC_見積依頼メール送信(
                                    NBaseCommon.Common.LoginUser,
                                    見積回答.OdMkID,
                                    見積回答.MsCustomerName,
                                    見積回答.Tantousha,
                                    textBoxメール件名.Text,
                                    見積回答.TantouMailAddress,
                                    見積回答.MkNo,
                                    見積回答.MkKigen,
                                    見積回答.WebKey,
                                    見積回答.MmDate,
                                    見積回答.Kiboubi,
                                    ref errMessage);
                        if (ret)
                        {
                            見積回答 = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, 見積回答.OdMkID);

                            MessageBox.Show("見積依頼メールを送信しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        else
                        {
                            MessageBox.Show(errMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (対象見積回答 == null)
                {
                    MessageBox.Show("見積依頼の作成に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

            // 見積依頼先
            MsCustomer customer = null;
            if (対象見積回答 == null)
            {
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
            }
            else
            {
                customer = new MsCustomer();
                customer.MsCustomerID = 対象見積回答.MsCustomerID;
                customer.CustomerName = 対象見積回答.MsCustomerName;
            }
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
            // 見積依頼日
            DateTime mmIraiDate = DateTime.MinValue;
            try
            {
                mmIraiDate = DateTime.Parse(dateTimePicker見積依頼日.Text);
            }
            catch
            {
                errMessage += "・見積依頼日が不正です。\n";
            }
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
            // 見積回答期限
            string mkKigen = "";
            mkKigen = textBox見積回答期限.Text;
            // 希望納期
            DateTime kiboubi = DateTime.MinValue;
            try
            {
                kiboubi = DateTime.Parse(dateTimePicker希望納期.Text);
            }
            catch
            {
                errMessage += "・希望納期が不正です。\n";
            }

            string telNo = textBox電話番号.Text;
            string faxNo = textBoxFAX番号.Text;

            if (errMessage.Length > 0)
            {
                errMessage = "入力項目に不備があります。\n====================================\n" + errMessage;
                MessageBox.Show(errMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (対象見積回答 != null)
            {
                見積回答.OdMkID = 対象見積回答.OdMkID;
            }
            else
            {
                見積回答.OdMkID = Hachu.Common.CommonDefine.新規ID(false);
            }
            見積回答.Status = 見積回答.OdStatusValue.Values[(int)OdMk.STATUS.未回答].Value;
            見積回答.OdMmID = 対象見積依頼.OdMmID;
            見積回答.MsCustomerID = customer.MsCustomerID;
            見積回答.MsCustomerName = customer.CustomerName;
            見積回答.Tantousha = tantou;
            見積回答.MkNo = 対象見積依頼.MmNo;
            見積回答.TantouMailAddress = mailAddress;
            //見積回答.MsNyukyoKamokuID = 対象見積依頼.MsNyukyoKamokuID; // (2009.09.14:aki) Honsenとの問題を含むところか？
            見積回答.HachuNo = "0";  // Null不可なので？
            見積回答.VesselID = 対象見積依頼.VesselID;
            見積回答.MkKigen = mkKigen;
            見積回答.Kiboubi = kiboubi;
            見積回答.Nouki = kiboubi; // 納期に希望納期をセットしておく
            見積回答.MmDate = mmIraiDate;
            if (mailAddress != null && mailAddress.Length > 0)
            {
                if (対象見積回答 != null && 対象見積回答.WebKey != null && 対象見積回答.WebKey.Length > 0)
                {
                    見積回答.WebKey = 対象見積回答.WebKey;
                }
                else
                {
                    見積回答.WebKey = Hachu.Common.CommonDefine.新規ID(false);
                }
            }
            見積回答.Tel = telNo;
            見積回答.Fax = faxNo;

            return true;
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

                        if (ct.Tel != null && ct.Tel.Length > 0)
                        {
                            textBox電話番号.Text = selectedCustomer.Tel;
                        }
                        if (ct.Fax != null && ct.Fax.Length > 0)
                        {
                            textBoxFAX番号.Text = selectedCustomer.Fax;
                        }
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
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    // サーバから添付データを取得する
                    odAttachFile = serviceClient.OdAttachFile_GetRecord(NBaseCommon.Common.LoginUser, odAttachFileId);
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


        // 2013.02 2013年度改造
        private List<Item見積回答品目> Build見積回答品目()
        {
            List<Item見積回答品目> ret = new List<Item見積回答品目>();


            foreach (Item見積依頼品目 見積依頼品目 in 見積品目s)
            {
                Item見積回答品目 見積回答品目 = new Item見積回答品目();
                見積回答品目.品目 = new OdMkItem();
                見積回答品目.品目.Header = 見積依頼品目.品目.Header;
                見積回答品目.品目.OdMmItemID = 見積依頼品目.品目.OdMmItemID;
                見積回答品目.品目.MsItemSbtID = 見積依頼品目.品目.MsItemSbtID;
                見積回答品目.品目.ItemName = 見積依頼品目.品目.ItemName;
                見積回答品目.品目.Bikou = 見積依頼品目.品目.Bikou;

                foreach (OdMmShousaiItem 依頼詳細品目 in 見積依頼品目.詳細品目s)
                {
                    OdMkShousaiItem 見積回答詳細品目 = new OdMkShousaiItem();
                    見積回答詳細品目.OdMmShousaiItemID = 依頼詳細品目.OdMmShousaiItemID;
                    見積回答詳細品目.ShousaiItemName = 依頼詳細品目.ShousaiItemName;
                    見積回答詳細品目.MsVesselItemID = 依頼詳細品目.MsVesselItemID;
                    見積回答詳細品目.MsLoID = 依頼詳細品目.MsLoID;
                    見積回答詳細品目.Count = 依頼詳細品目.Count;
                    見積回答詳細品目.MsTaniID = 依頼詳細品目.MsTaniID;
                    見積回答詳細品目.Bikou = 依頼詳細品目.Bikou;

                    if (依頼詳細品目.IsAttached)
                    {
                        見積回答詳細品目.OdAttachFileID = 依頼詳細品目.OdAttachFileID;
                    }
                    else
                    {
                        見積回答詳細品目.OdAttachFileID = null;
                    }

                    見積回答品目.詳細品目s.Add(見積回答詳細品目);
                }

                ret.Add(見積回答品目);

            }
            return ret;
        }
    }
}
