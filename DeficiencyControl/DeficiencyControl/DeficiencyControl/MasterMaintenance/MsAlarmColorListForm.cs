using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Forms;
using DeficiencyControl.Util;
using DeficiencyControl.Grid;

using DeficiencyControl.MasterMaintenance.Logic;

namespace DeficiencyControl.MasterMaintenance
{
    /// <summary>
    /// MsAlarmColor一覧画面
    /// </summary>
    public partial class MsAlarmColorListForm : BaseForm
    {
        public MsAlarmColorListForm():base( EFormNo.MsAlarmColorList)
        {
            InitializeComponent();        
        }

        /// <summary>
        /// このフォームのデータ
        /// </summary>
        class MsAlarmColorListFormData
        {
            /// <summary>
            /// 表示データ
            /// </summary>
            public List<MsAlarmColor> ACList = new List<MsAlarmColor>();
                
        }


        /// <summary>
        /// このフォームのデータ
        /// </summary>
        private MsAlarmColorListFormData FData = null;


        /// <summary>
        /// グリッド管理
        /// </summary>
        private MsAlarmColorListGrid Grid = null;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// データ検索
        /// </summary>
        /// <returns></returns>
        private bool Search()
        {
            //アラームカラーの検索
            this.FData.ACList = SvcManager.SvcMana.MsAlarmColor_GetRecords();


            //表示
            this.Grid.DispData(this.FData.ACList);

            return true;
        }

        /// <summary>
        /// クリア
        /// </summary>
        private void Clear()
        {
            this.FData.ACList = new List<MsAlarmColor>();
            this.Grid.DispData(this.FData.ACList);
        }


        /// <summary>
        /// 詳細画面の起動から再検索までを行う
        /// </summary>
        /// <param name="col">渡すデータ 新規はnull</param>
        /// <returns></returns>
        private bool CreateDetailForm(MsAlarmColor col)
        {
            //詳細の表示
            MsAlarmColorDetailForm f = new MsAlarmColorDetailForm();
            f.SetFormSettingData(col);

            //起動
            DialogResult dret = f.ShowDialog();
            if (dret != System.Windows.Forms.DialogResult.Cancel)
            {
                //必要なら再検索
                this.buttonSearch_Click(null, null);
            }

            return true;
        }

        //--------------------------------------------------------------------------------------------------
        /// <summary>
        /// このフォームの初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            //データ作成
            this.FData = new MsAlarmColorListFormData();


            //グリッド管理作成
            this.Grid = new MsAlarmColorListGrid(this.dataGridViewAlarmColor);

            return true;
        }

        /// <summary>
        /// ユーザー権限制御
        /// </summary>
        /// <returns></returns>
        protected override bool ControlUserRole()
        {
            //MsUserRole rdata = DcGlobal.Global.LoginUser.Role;


            //this.buttonCreatae.Enabled = rdata.master_regist;

            return true;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MsAlarmColorListForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Searchボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            using (WaitingState st = new WaitingState(this))
            {
                this.Search();
            }
        }


        /// <summary>
        /// Clearボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.Clear();
        }

        /// <summary>
        /// Createが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreatae_Click(object sender, EventArgs e)
        {
            //詳細起動
            this.CreateDetailForm(null);
        }

        /// <summary>
        /// セルが選択されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewAlarmColor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MsAlarmColor data = this.Grid.GetSelectDataObject() as MsAlarmColor;
            if (data == null)
            {
                return;
            }

            //詳細起動
            this.CreateDetailForm(data);
        }
    }
}
