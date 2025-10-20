using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DeficiencyControl.Forms;
using DcCommon.DB;

namespace DeficiencyControl.AdminMaintenance
{
    /// <summary>
    /// RegistCount一覧画面
    /// </summary>
    public partial class RegistCountListForm : BaseForm
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RegistCountListForm() : base( EFormNo.AdminMaintenanceRegistCount)
        {
            InitializeComponent();
        }


        /// <summary>
        /// 現在の表示データ
        /// </summary>
        private List<RegistCountData> DispData = new List<RegistCountData>();


        /// <summary>
        /// グリッド管理
        /// </summary>
        private RegistCountListGrid Grid = null;


        /// <summary>
        /// 検索
        /// </summary>ad
        private void SearchRegistCount()
        {
            try
            {
                using (WaitingState es = new WaitingState(this))
                {
                    //データ取得
                    //this.DispData = SvcManager.SvcMana.RegistCountData_GetRegistCount();

                    //グリッド表示
                    this.Grid.DispData(this.DispData);
                }
            }
            catch (Exception e)
            {
                DcLog.WriteLog(e, "RegistCountListForm SearchRegistCount()");
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {


            //グリッド管理作成
            this.Grid = new RegistCountListGrid(this.dataGridViewRegistCount);

            return true;
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegistCountListForm_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// RegistCountボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRegistCount_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonRegistCount_Click");

            using (WaitingState st = new WaitingState(this))
            {
                //データ検索処理
                this.SearchRegistCount();
            }
        }
    }
}
