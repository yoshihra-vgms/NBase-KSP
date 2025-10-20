using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using NBaseData.DS;

namespace NBaseMaster.MsCustomer
{
    public partial class 顧客管理詳細Form : Form
    {
        private NBaseData.DAC.MsCustomer msCustomer;
        private List<NBaseData.DAC.MsSchool> msSchools = new List<NBaseData.DAC.MsSchool>();

        public 顧客管理詳細Form(NBaseData.DAC.MsCustomer target)
        {
            InitializeComponent();

            msCustomer = target;
            msSchools.AddRange(msCustomer.MsSchools);

        }

        private void 顧客管理詳細Form_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("免許／免状名", typeof(string)));
            dt.Columns.Add(new DataColumn("種別名", typeof(string)));
            dt.Columns.Add(new DataColumn("obj", typeof(object)));

            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].Visible = false; 
            
            
            if (msCustomer.IsNew())
            {
                this.Text = NBaseCommon.Common.WindowTitle("番号不明", "顧客管理新規登録", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            }
            else
            {
                this.Text = NBaseCommon.Common.WindowTitle("番号不明", "顧客管理詳細", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
                SetItems(msCustomer);
           }


        }

        private void SetItems(NBaseData.DAC.MsCustomer customer)
        {
            //-----------------------------------------------------
            // プロパティ設定
            //-----------------------------------------------------
            CustomerId_textBox.ReadOnly = true;
            CustomerId_textBox.Enabled = false;


            LoginID_textBox.Enabled = false;
            Password_textBox.Enabled = false;

            // 取引先
            if (customer.Is取引先())
            {
                checkBox取引先.Checked = true;

                LoginID_textBox.Enabled = true;
                Password_textBox.Enabled = true;
            } 
            // 代理店
            if (customer.Is代理店())
            {
                checkBox代理店.Checked = true;
            }
            // 荷主
            if (customer.Is荷主())
            {
                checkBox荷主.Checked = true;
            }
            // 企業
            if (customer.Is企業())
            {
                checkBox企業.Checked = true;
            }
            // 学校
            if (customer.Is学校())
            {
                checkBox学校.Checked = true;
            }

            if (customer.AppointedFlag == 1)
            {
                checkBox申請先.Checked = true;
            }
            if (customer.InspectionFlag == 1)
            {
                checkBox検船実施会社.Checked = true;
            }

            //-----------------------------------------------------
            // GUIへ値を設定する
            //-----------------------------------------------------
            CustomerId_textBox.Text = customer.MsCustomerID;
            CustomerName_textBox.Text = customer.CustomerName;
            Tel_textBox.Text = customer.Tel;
            Fax_textBox.Text = customer.Fax;
            ZipCode_textBox.Text = customer.ZipCode;
            Address1_textBox.Text = customer.Address1;
            Address2_textBox.Text = customer.Address2;
            BuildingName_textBox.Text = customer.BuildingName;
            LoginID_textBox.Text = customer.LoginID;
            Password_textBox.Text = customer.Password;
            BankName_textBox.Text = customer.BankName;
            BranchName_textBox.Text = customer.BranchName;
            AccountNo_textBox.Text = customer.AccountNo;
            AccountId_textBox.Text = customer.AccountId;

            textBox校長先生名.Text = customer.Teacher1;
            textBox進路指導部先生名.Text = customer.Teacher2;
            textBox備考.Text = customer.Bikou;

            Set学科();
            Set免許免状();
            if (listBox学科.Items.Count > 0)
                listBox学科.SelectedIndex = 0;
        }

        private void Update_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!入力値チェック())
                {
                    return;
                }

                //--------------------------------------------------------
                // UpDate処理
                //--------------------------------------------------------
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    NBaseData.DAC.MsCustomer Customer = new NBaseData.DAC.MsCustomer();

                    Customer.CustomerName = CustomerName_textBox.Text;
                    Customer.Tel = Tel_textBox.Text;
                    Customer.Fax = Fax_textBox.Text;
                    Customer.ZipCode = ZipCode_textBox.Text;
                    Customer.Address1 = Address1_textBox.Text;
                    Customer.Address2 = Address2_textBox.Text;
                    Customer.BuildingName = BuildingName_textBox.Text;

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
                    Customer.Shubetsu = shubetsuStr;

                    int shubetu企業 = 0;
                    int shubetu学校 = 0;
                    if (checkBox企業.Checked)
                    {
                        shubetu企業 = 1;
                    }
                    if (checkBox学校.Checked)
                    {
                        shubetu学校 = 1;
                    }
                    string shubetsuStr2 = shubetu企業.ToString() + "," + shubetu学校.ToString();
                    Customer.Shubetsu2 = shubetsuStr2;

                    if (checkBox申請先.Checked)
                    {
                        Customer.AppointedFlag = 1;
                    }
                    else
                    {
                        Customer.AppointedFlag = 0;
                    }

                    if (checkBox検船実施会社.Checked)
                    {
                        Customer.InspectionFlag = 1;
                    }
                    else
                    {
                        Customer.InspectionFlag = 0;
                    }

                    Customer.LoginID = LoginID_textBox.Text;
                    Customer.Password = Password_textBox.Text;
                    Customer.RenewDate = DateTime.Now;
                    Customer.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                    Customer.Ts = msCustomer.Ts;
                    Customer.BankName = BankName_textBox.Text;
                    Customer.BranchName = BranchName_textBox.Text;
                    Customer.AccountNo = AccountNo_textBox.Text;
                    Customer.AccountId = AccountId_textBox.Text;

                    Customer.Teacher1 = textBox校長先生名.Text;
                    Customer.Teacher2 = textBox進路指導部先生名.Text;
                    Customer.Bikou = textBox備考.Text;

                    Customer.MsSchools.AddRange(msSchools);

                    Customer.RenewDate = DateTime.Today;
                    Customer.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

                    if (msCustomer.IsNew())
                    {
                        Customer.MsCustomerID = CustomerId_textBox.Text;


                        serviceClient.MsCustomer_InsertRecord(NBaseCommon.Common.LoginUser, Customer);
                    }
                    else
                    {
                        Customer.MsCustomerID = msCustomer.MsCustomerID;
                        serviceClient.MsCustomer_UpdateRecord(NBaseCommon.Common.LoginUser, Customer);
                    }
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

        private void Delete_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                //-------------------------------------------------------
                // 確認
                //-------------------------------------------------------
                if (Message.Show問合せ("この顧客を削除します。よろしいですか？") == false)
                {
                    return;
                }

                //--------------------------------------------------------
                // Delete処理
                //--------------------------------------------------------
                NBaseData.DAC.MsCustomer Customer = new NBaseData.DAC.MsCustomer();
                Customer.MsCustomerID = msCustomer.MsCustomerID;
                Customer.Ts = msCustomer.Ts;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    serviceClient.MsCustomer_DeleteFlagRecord(NBaseCommon.Common.LoginUser, Customer);

                    Message.Show確認("削除しました。");
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
            if (msCustomer.IsNew() && CustomerId_textBox.Text.Length < 1)
            {
                Message.Showエラー("顧客Noを入力してください。");
                return false;
            }
            if (msCustomer.IsNew())
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    // 顧客ID
                    NBaseData.DAC.MsCustomer msCustomer1 = serviceClient.MsCustomer_GetRecord(NBaseCommon.Common.LoginUser, CustomerId_textBox.Text);
                    if (msCustomer1 != null)
                    {
                        Message.Showエラー("入力された顧客Noは登録されています。");
                        return false;
                    }
                }
            }

            // 顧客名
            if (CustomerName_textBox.Text.Length < 1)
            {
                Message.Showエラー("顧客名を入力してください。");
                return false;
            }

            //--------------------------------------------------
            // 重複確認
            //--------------------------------------------------
            // 2009.10.25:aki ログインIDをチェックする場合、入力されている場合のみにする
            if (LoginID_textBox.Text.Length > 0)
            {

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    // ログインID
                    NBaseData.DAC.MsCustomer msCustomer1 = serviceClient.MsCustomer_GetRecordByLoginId(NBaseCommon.Common.LoginUser, LoginID_textBox.Text);
                    if (msCustomer1 != null)
                    {
                        // 更新対象のレコード自身なら。。
                        if (msCustomer1.MsCustomerID == msCustomer.MsCustomerID)
                        {
                            // 自身なら、更新対象。
                        }
                        else
                        {
                            Message.Showエラー("入力されたログインIDは登録されています。");
                            return false;
                        }
                    }
                }
            }

            //　種別
            int shubetu取引先 = 0;
            int shubetu代理店 = 0;
            int shubetu荷主 = 0;
            int shubetu企業 = 0;
            int shubetu学校 = 0;
            int shubetu申請先 = 0;
            int shubetu検船実施会社 = 0;
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
            if (checkBox企業.Checked)
            {
                shubetu企業 = 1;
            }
            if (checkBox学校.Checked)
            {
                shubetu学校 = 1;
            }
            if (checkBox申請先.Checked)
            {
                shubetu申請先 = 1;
            }
            if (checkBox検船実施会社.Checked)
            {
                shubetu検船実施会社 = 1;
            }
            if (shubetu取引先 == 0 && shubetu代理店 == 0 && shubetu荷主 == 0 && shubetu企業 == 0 && shubetu学校 == 0 && shubetu申請先 == 0 && shubetu検船実施会社 == 0)
            {
                Message.Showエラー("種別を入力してください。");
                return false;
            }

            return true;
        }

        private void Cancel_Btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void button学科追加_Click(object sender, EventArgs e)
        {
            学科Form form = new 学科Form(null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                NBaseData.DAC.MsSchool msSchool = new NBaseData.DAC.MsSchool();

                msSchool.DepartmentOf = form.dstDepartment;
                msSchool.MsSiMenjouID = 0;
                msSchool.MsSiMenjouKindID = 0;

                msSchools.Add(msSchool);

                Set学科();
            }
        }

        private void button学科削除_Click(object sender, EventArgs e)
        {
            if (listBox学科.SelectedItem == null)
                return;

            if (MessageBox.Show("学部・学科を削除します。よろしいですか？") != DialogResult.OK)
                return;

            string dept = (string)listBox学科.SelectedItem;
            foreach (NBaseData.DAC.MsSchool school in msSchools)
            {
                if (school.DepartmentOf == dept)
                    school.DeleteFlag = 1;
            }

            listBox学科.Items.Remove(dept);

            Set学科();
        }

        private void button免許追加_Click(object sender, EventArgs e)
        {
            if (listBox学科.Items.Count == 0)
                return;

            if (listBox学科.SelectedIndex < 0)
                return;

            string dept = (string)listBox学科.SelectedItem;

            免許Form form = new 免許Form(dept);
            if (form.ShowDialog() == DialogResult.OK)
            {
                NBaseData.DAC.MsSchool newSchool = form.School;

                if (msSchools.Any(obj => obj.DeleteFlag == 0 && obj.MsSiMenjouID == 0 && obj.MsSiMenjouKindID == 0 && obj.DepartmentOf == newSchool.DepartmentOf))
                {
                    var school = msSchools.Where(obj => obj.DeleteFlag == 0 && obj.MsSiMenjouID == 0 && obj.MsSiMenjouKindID == 0 && obj.DepartmentOf == newSchool.DepartmentOf).First();
                    school.MsSiMenjouID = newSchool.MsSiMenjouID;
                    school.MsSiMenjouKindID = newSchool.MsSiMenjouKindID;
                }
                else
                {
                    msSchools.Add(newSchool);
                }

                Set免許免状();
            }
        }

        private void button免許削除_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows == null || dataGridView1.SelectedRows.Count == 0)
                return;

            if (MessageBox.Show("免許/免状を削除します。よろしいですか？") != DialogResult.OK)
                return;

            NBaseData.DAC.MsSchool school = dataGridView1.SelectedRows[0].Cells[2].Value as NBaseData.DAC.MsSchool;

            if (school.IsNew())
            {
                msSchools.Remove(school);
            }
            else
            {
                if (msSchools.Any(obj => obj.MsSchoolID == school.MsSchoolID))
                {

                    var del = msSchools.Where(obj => obj.MsSchoolID == school.MsSchoolID).First();
                    del.DeleteFlag = 1;

                }
            }
            Set免許免状();
        }



        private void Set学科()
        {
            listBox学科.Items.Clear();

            var departmentOfs = msSchools.Where(obj => obj.DeleteFlag == 0).Select(obj => obj.DepartmentOf).Distinct();

            foreach(string dept in departmentOfs)
            {
                listBox学科.Items.Add(dept);
            }
        }

        private void listBox学科_SelectedIndexChanged(object sender, EventArgs e)
        {
            Set免許免状();
        }

        private void Set免許免状()
        {
            string dept = (string)listBox学科.SelectedItem;

            DataTable dt = dataGridView1.DataSource as DataTable;

            if (dt != null)
                dt.Clear();

            if (msSchools.Any(obj => obj.DeleteFlag == 0 && obj.DepartmentOf == dept))
            {
                var schools = msSchools.Where(obj => obj.DeleteFlag == 0 && obj.DepartmentOf == dept);
                foreach (NBaseData.DAC.MsSchool school in schools)
                {
                    if (!(school.MsSiMenjouID > 0))
                        continue;

                    DataRow row = dt.NewRow();

                    row[0] = SeninTableCache.instance().GetMsSiMenjouName(NBaseCommon.Common.LoginUser, school.MsSiMenjouID);
                    row[1] = SeninTableCache.instance().GetMsSiMenjouKindName(NBaseCommon.Common.LoginUser, school.MsSiMenjouKindID);
                    row[2] = school;

                    dt.Rows.Add(row);
                }
            }

        }
    }
}
