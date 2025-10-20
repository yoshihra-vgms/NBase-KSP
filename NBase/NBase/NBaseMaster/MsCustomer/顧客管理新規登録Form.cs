using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using WingMaster.MsCustomer;

namespace WingMaster.MsCustomer
{
    public partial class 顧客管理新規登録Form : Form
    {

        public 顧客管理新規登録Form()
        {
            InitializeComponent();
            this.Text = WingCommon.Common.WindowTitle("番号不明", "顧客管理新規登録", WingServiceReferences.WcfServiceWrapper.ConnectedServerID);

            // 種別ラジオボタン初期値設定
            //shubetu1_radioButton.Checked = true;
        }

        private void Add_Btn_Click(object sender, EventArgs e)
        {
            if (!入力値チェック())
            {
                return;
            }
            
            //----------------------------------------------
            // 種別の値を取得
            //int shubetu = 0;
            //if (radioButton1.Checked == true)
            //{
            //    Customer.Shubetsu = 0;
            //}
            //else if (radioButton2.Checked == true)
            //{
            //    Customer.Shubetsu = 1;
            //}
            //else
            //{
            //    Customer.Shubetsu = 2;
            //}
            int shubetu取引先 = 0;
            int shubetu代理店 = 0;
            int shubetu荷主 = 0;
            if (checkBox取引先.Checked)
            {
                shubetu取引先 = 1;
            }
            if (checkBox代理店.Checked)
            {
                shubetu代理店 = 1;
            }
            if (checkBox荷主.Checked)
            {
                shubetu荷主 = 1;
            }
            string shubetsuStr = shubetu取引先.ToString() + "," + shubetu代理店.ToString() + "," + shubetu荷主.ToString();

            //----------------------------------------------
            // Insert
            try
            {
                WingData.DAC.MsCustomer customer = new WingData.DAC.MsCustomer();
                customer.MsCustomerID = CustomerId_textBox.Text;
                customer.CustomerName = CustomerName_textBox.Text;
                customer.Tel = Tel_textBox.Text;
                customer.Fax = Fax_textBox.Text;
                customer.ZipCode = ZipCode_textBox.Text;
                customer.Address1 = Address1_textBox.Text;
                customer.Address2 = Address2_textBox.Text;
                customer.BuildingName = BuildingName_textBox.Text;
                customer.LoginID = LoginID_textBox.Text;
                customer.Password = Password_textBox.Text;
                //customer.Shubetsu = shubetu;
                customer.Shubetsu = shubetsuStr;

                customer.BankName = this.textBox1.Text; ;  //銀行名
                customer.BranchName = this.textBox2.Text;    //支店名
                customer.AccountNo = this.textBox4.Text;     //口座番号
                customer.AccountId =this.textBox3.Text;     //口座名義
                

                customer.RenewDate = DateTime.Today;
                customer.RenewUserID = WingCommon.Common.LoginUser.MsUserID;
                customer.SendFlag = 0;
                customer.VesselID = 0;

                using (WingServiceReferences.WingServer.ServiceClient serviceClient = WingServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    serviceClient.MsCustomer_InsertRecord(WingCommon.Common.LoginUser, customer);

                    Message.Show確認("更新しました。");
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Message.Showエラー(ex.Message);
            }
        }

        private bool 入力値チェック()
        {
            //---------------------------------------------------
            // 必須入力確認
            //---------------------------------------------------

            // 顧客ID
            if (CustomerId_textBox.Text.Length < 1)
            {
                Message.Showエラー("顧客Noを入力してください。");
                return false;
            }

            // 顧客名
            if (CustomerName_textBox.Text.Length < 1)
            {
                Message.Showエラー("顧客名を入力してください。");
                return false;
            }
            //　種別
            int shubetu取引先 = 0;
            int shubetu代理店 = 0;
            int shubetu荷主 = 0;
            if (checkBox取引先.Checked)
            {
                shubetu取引先 = 1;
            }
            if (checkBox代理店.Checked)
            {
                shubetu代理店 = 1;
            }
            if (checkBox荷主.Checked)
            {
                shubetu荷主 = 1;
            }
            if (shubetu取引先 == 0 && shubetu代理店 == 0 && shubetu荷主 == 0)
            {
                Message.Showエラー("種別を入力してください。");
                return false;
            }

            //--------------------------------------------------
            // 重複確認
            //--------------------------------------------------

            using (WingServiceReferences.WingServer.ServiceClient serviceClient = WingServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                // 顧客ID
                WingData.DAC.MsCustomer msCustomer1 = serviceClient.MsCustomer_GetRecord(WingCommon.Common.LoginUser, CustomerId_textBox.Text);
                if (msCustomer1 != null)
                {
                    Message.Showエラー("入力された顧客Noは登録されています。");
                    return false;
                }

                // ログインID
                WingData.DAC.MsCustomer msCustomer2 = serviceClient.MsCustomer_GetRecordByLoginId(WingCommon.Common.LoginUser, LoginID_textBox.Text);
                if (msCustomer2 != null)
                {
                    Message.Showエラー("入力されたログインIDは登録されています。");
                    return false;
                }
            }

            return true;
        }

        private void Cancel_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
