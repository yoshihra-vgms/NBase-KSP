using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DeficiencyControl.AdminMaintenance;

namespace DeficiencyControl.MasterMaintenance
{
    /// <summary>
    /// マスターメンテナンスメニュー画面
    /// </summary>
    public partial class MasterMenuForm : BaseForm
    {
        //コンストラクタ
        public MasterMenuForm() : base( EFormNo.MasterMenu)
        {
            InitializeComponent();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 初期化関数
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            return true;
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterMenuForm_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 閉じるボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonClose_Click");

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }


      

        /// <summary>
        /// MsAlarmColorが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMsAlarmColor_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonMsAlarmColor_Click");

            MsAlarmColorListForm f = new MsAlarmColorListForm();
            DialogResult dret = f.ShowDialog(this);
        }

   

        
        /// <summary>
        /// キーボードが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterMenuForm_KeyDown(object sender, KeyEventArgs e)
        {
            ////Ctrl + M
            //if (e.Control == true && e.KeyCode == Keys.M)
            //{
            //    //Adminユーザー？
            //    if (DcGlobal.Global.LoginUserID == AppConfig.Config.ConfigData.ms_user_admin)
            //    {
            //        AdminMaintenanceMenuForm f = new AdminMaintenanceMenuForm();
            //        DialogResult dret = f.ShowDialog(this);
            //    }
            //}
            
        }


    }
}
