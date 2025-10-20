using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeficiencyControl.AdminMaintenance
{
    /// <summary>
    /// 管理者メンテナンスメニュー画面
    /// </summary>
    public partial class AdminMaintenanceMenuForm : BaseForm
    {
        public AdminMaintenanceMenuForm() : base( EFormNo.AdminMaintenanceMenu)
        {
            InitializeComponent();
        }



        


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdminMaintenanceMenuForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// RegistCountボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRegistCount_Click(object sender, EventArgs e)
        {
            RegistCountListForm rf = new RegistCountListForm();
            DialogResult dret = rf.ShowDialog(this);
        }
    }
}
