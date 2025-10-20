using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DS;

namespace NBaseMaster.MsCustomer
{
    public partial class 免許Form : Form
    {
        private string DepartmentOf;
        public NBaseData.DAC.MsSchool School;

        public 免許Form(string DepartmentOf)
        {
            InitializeComponent();

            this.DepartmentOf = DepartmentOf;
        }

        private void 免許Form_Load(object sender, EventArgs e)
        {
            textBox学科.Text = DepartmentOf;

            foreach(NBaseData.DAC.MsSiMenjou menjou in SeninTableCache.instance().GetMsSiMenjouList(NBaseCommon.Common.LoginUser))
            {
                comboBox免許.Items.Add(menjou);
            }
        }

        private void comboBox免許_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox種別.Items.Clear();

            NBaseData.DAC.MsSiMenjou menjou = comboBox免許.SelectedItem as NBaseData.DAC.MsSiMenjou;
            var kinds = SeninTableCache.instance().GetMsSiMenjouKindList(NBaseCommon.Common.LoginUser).Where(obj => obj.MsSiMenjouID == menjou.MsSiMenjouID);
            foreach(NBaseData.DAC.MsSiMenjouKind kind in kinds)
            {
                comboBox種別.Items.Add(kind);
            }
        }

        private void Update_Btn_Click(object sender, EventArgs e)
        {
            if (!(comboBox免許.SelectedItem is NBaseData.DAC.MsSiMenjou))
            {
                MessageBox.Show("免許／免状を選択してください");
                return;
            }
            if (comboBox種別.Items.Count > 0 && !(comboBox種別.SelectedItem is NBaseData.DAC.MsSiMenjouKind))
            {
                MessageBox.Show("種別を選択してください");
                return;
            }


            NBaseData.DAC.MsSiMenjou menjou = comboBox免許.SelectedItem as NBaseData.DAC.MsSiMenjou;
            NBaseData.DAC.MsSiMenjouKind kind = null;
            if (comboBox種別.Items.Count > 0)
            {
                kind = comboBox種別.SelectedItem as NBaseData.DAC.MsSiMenjouKind;
            }

            School = new NBaseData.DAC.MsSchool();

            School.DepartmentOf = DepartmentOf;
            School.MsSiMenjouID = menjou.MsSiMenjouID;
            if (kind != null)
                School.MsSiMenjouKindID = kind.MsSiMenjouKindID;

            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();

        }

        private void Cancel_Btn_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
