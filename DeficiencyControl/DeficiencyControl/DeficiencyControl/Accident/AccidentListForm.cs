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

namespace DeficiencyControl.Accident
{
    /// <summary>
    /// 事故トラブル一覧画面
    /// </summary>
    public partial class AccidentListForm : BaseForm
    {
        public AccidentListForm() : base(EFormNo.AccidentList, false)
        {
            InitializeComponent();
        }

        /// <summary>
        /// この画面のデータ
        /// </summary>
        public class AccidentListFormData
        {
            /// <summary>
            /// 表示データリスト
            /// </summary>
            public List<DcAccident> DispList = new List<DcAccident>();
        }


        /// <summary>
        /// ロジック
        /// </summary>
        private AccidentListFormLogic Logic = null;

        /// <summary>
        /// この画面のデータ
        /// </summary>
        private AccidentListFormData FData = null;

        /// <summary>
        /// グリッド管理
        /// </summary>
        public AccidentListGrid Grid = null;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitDisplayControl()
        {
            //Vessel
            ControlItemCreator.CreateVessel(this.singleLineComboVessel);

            //Kind
            ControlItemCreator.CretaeMsKindOfAccident(this.comboBoxAccidentKind, true);

            //Port
            ControlItemCreator.CreateBasho(this.singleLineComboPort);

            //国
            ControlItemCreator.CreateRegional(this.singleLineComboCountry);

            //PIC
            ControlItemCreator.CreateUser(this.singleLineComboUser);


            //Kind of Accident
            ControlItemCreator.CretaeMsKindOfAccident(this.comboBoxKindOfAccident, true);

            //Situation
            ControlItemCreator.CreateMsAccidentSituation(this.comboBoxSituation, true);

        }


        /// <summary>
        /// データの検索 中でWaitStateしてます。注意
        /// </summary>
        private void SearchData()
        {
            using (WaitingState st = new WaitingState(this))
            {
                this.Logic.Search();
            }
            
        }



        /// <summary>
        /// 詳細画面の起動
        /// </summary>
        private void SetupDetailForm()
        {
            //現在の選択を取得する
            DcAccident ac = this.Grid.GetSelectDataObject() as DcAccident;
            if (ac == null)
            {
                return;
            }

            //画面の設定
            AccidentDetailForm.AccidentDetailFormInputData idata = new AccidentDetailForm.AccidentDetailFormInputData();
            idata.Accident = ac;
            AccidentDetailForm f = new AccidentDetailForm();
            f.SetFormSettingData(idata);

            //起動
            DialogResult dret = f.ShowDialog(this);
            if (dret == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }


            //変更したら再検索を行う
            this.SearchData();

        }

        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 画面初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            this.FData = new AccidentListFormData();
            this.Logic = new AccidentListFormLogic(this, this.FData);

            //画面コントロール初期化
            this.InitDisplayControl();

            //グリッド管理初期化
            this.Grid = new AccidentListGrid(this.dataGridViewAccident);

            //検索クリア
            this.Logic.ClearSearchControl();

            return true;
        }


        /// <summary>
        /// ユーザー権限処理
        /// </summary>
        /// <returns></returns>
        protected override bool ControlUserRole()
        {
            //MsUserRole rdata = DcGlobal.Global.LoginUser.Role;

            //機能制御
            //this.buttonCreate.Enabled = rdata.;

            return true;
        }


        /// <summary>
        /// 裏モード置き換え
        /// </summary>
        protected override void ChangeDeficiencyMode()
        {
            #region 青モード
            {
                //必要なコントロールを表示させる。
                this.checkBoxBlueReverse.Visible = false;
                this.buttonBlueUpdate.Visible = false;
                if (AppConfig.Config.ConfigData.DeficiencyControlBlueMode == true)
                {
                    this.checkBoxBlueReverse.Visible = true;
                    this.buttonBlueUpdate.Visible = true;
                }
            }
            #endregion

            return;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccidentListForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "AccidentListForm_Load");
        }


        /// <summary>
        /// 検索ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonSearch_Click");

            this.SearchData();
        }

        /// <summary>
        /// クリアボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonClear_Click");

            this.Logic.ClearSearchControl();
        }

        /// <summary>
        /// 作成ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonCreate_Click");

            //作成画面起動
            AccidentCreateForm f = new AccidentCreateForm();
            DialogResult det = f.ShowDialog(this);
            if(det != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            //ここで検索を行うこと
            this.SearchData();

        }


        /// <summary>
        /// 編集ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDetail_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonDetail_Click");

            //詳細画面の起動
            this.SetupDetailForm();
        }

        /// <summary>
        /// Excel出力ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOutputExcel_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonOutputExcel_Click");

            AccidentOutputSettingForm f = new AccidentOutputSettingForm();
            DialogResult dret = f.ShowDialog(this);
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
        /// 一覧が選択されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewAccident_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DcLog.WriteLog(this, "dataGridViewAccident_CellDoubleClick");

            //詳細画面の起動
            this.SetupDetailForm();
        }


        /// <summary>
        /// 青モード、チェックの反転処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxBlueReverse_CheckedChanged(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "checkBoxBlueReverse_CheckedChanged");

            //モード設定
            bool f = this.checkBoxBlueReverse.Checked;
            this.Grid.SetAllCheck(f);

        }


        /// <summary>
        /// 青モード、データの更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBlueUpdate_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonBlueUpdate_Click");


            using (WaitingState st = new WaitingState(this))
            {
                //変更物の取得
                List<DcAccident> aclist = this.Grid.GetEditCheckList<DcAccident>();
     

                //更新処理
                bool ret = SvcManager.SvcMana.DcAccident_UpdateStatus(aclist, DcGlobal.Global.LoginMsUser);
                if (ret == false)
                {
                    DcLog.WriteLog("DcAccident_UpdateStatus FALSE");
                    return;
                }

            }

            this.SearchData();
        }
    }
}
