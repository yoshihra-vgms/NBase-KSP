using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBaseMaster.MsCustomer
{
    public partial class 学科Form : Form
    {
        private string orgDepartment = "";
        public string dstDepartment = "";

        public 学科Form(string department)
        {
            InitializeComponent();

            orgDepartment = department;
        }

        private void Update_Btn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("学部・学科を入力してください");
                return;
            }
            if (orgDepartment != null && MessageBox.Show("学部・学科を変更します。よろしいですか？") != DialogResult.OK)
            {
                return;
            }
            dstDepartment = textBox1.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Btn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 学科Form_Load(object sender, EventArgs e)
        {

        }
    }
}
