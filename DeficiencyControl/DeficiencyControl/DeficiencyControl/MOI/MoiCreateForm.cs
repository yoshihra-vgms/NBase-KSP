using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Controls.CommentItem;

using DeficiencyControl.Logic;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;

namespace DeficiencyControl.MOI
{
    /// <summary>
    /// 検船新規作成画面
    /// </summary>
    public partial class MoiCreateForm : BaseForm
    {
        public MoiCreateForm() : base(EFormNo.MoiCreate, false)
        {
            InitializeComponent();
        }


        /// <summary>
        /// 画面データ
        /// </summary>
        public class MoiCreateFormData
        {

        }

        /// <summary>
        /// 画面データ
        /// </summary>
        public MoiCreateFormData FData = null;

        /// <summary>
        /// ロジック
        /// </summary>
        public MoiCreateFormLogic Logic = null;


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 登録処理 中でwaitstateします。
        /// </summary>
        private bool RegistData()
        {
            try
            {
                using (WaitingState es = new WaitingState(this))
                {
                    //登録処理
                    this.Logic.RegistData();
                }
            }
            catch (Exception e)
            {
                DcLog.WriteLog(e, "RegistData");
                return false;
            }


            return true;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面の初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            this.FData = new MoiCreateFormData();
            this.Logic = new MoiCreateFormLogic(this, this.FData);


            //
            this.Logic.Init();

            return true;
        }

        /// <summary>
        /// ユーザー権限処理
        /// </summary>
        /// <returns></returns>
        protected override bool ControlUserRole()
        {
            return true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoiCreateForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "MoiCreateForm_Load");
        }


        /// <summary>
        /// 更新ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonUpdate_Click");

            //データ登録処理
            bool ret = this.RegistData();
            if (ret == false)
            {
                return;
            }


            this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
    }
}
