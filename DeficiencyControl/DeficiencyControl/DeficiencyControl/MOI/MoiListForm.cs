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
using CIsl.DB.WingDAC;

namespace DeficiencyControl.MOI
{
    /// <summary>
    /// 検船一覧画面
    /// </summary>
    public partial class MoiListForm : BaseForm
    {
        public MoiListForm() :base(EFormNo.MoiList, false)
        {
            InitializeComponent();
        }

        
        /// <summary>
        /// 画面データ
        /// </summary>
        public class MoiListFormData
        {
            /// <summary>
            /// 検船データ
            /// </summary>
            public List<MoiData> DispList = new List<MoiData>();
        }

        
        /// <summary>
        /// 画面データ
        /// </summary>
        private MoiListFormData FData = null;


        /// <summary>
        /// ロジック
        /// </summary>
        private MoiListFormLogic Logic = null;

        /// <summary>
        /// グリッド管理
        /// </summary>
        public MoiListGrid Grid = null;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitDisplayControl()
        {
            //Vessel
            ControlItemCreator.CreateVessel(this.singleLineComboVessel);

            //検船種別
            ControlItemCreator.CreateMsInspectionCategory(this.comboBoxInspectionCategory, true);
            
            //Port
            ControlItemCreator.CreateBasho(this.singleLineComboPort);

            //国
            ControlItemCreator.CreateRegional(this.singleLineComboCountry);

            //検船会社
            ControlItemCreator.CreateMsCustomerInspection(this.singleLineComboInspectionCompany);
            
            //PIC
            ControlItemCreator.CreateUser(this.singleLineComboUser);

            //VIQ Version
            ControlItemCreator.CreateMsViqVersion(this.comboBoxViqVersion, true);

            //VIQ Code・・・Version確定を持って対象が表示されるため、デフォルト作成を行わない
            //ControlItemCreator.CreateMsViqCode(this.comboBoxViqCode, true);

            //VIQ No・・・Code確定を持って対象が表示されるため、デフォルト作成を行わない
            //ControlItemCreator.CreateMsViqNo(this.singleLineComboViqNo);

            

        }

        /// <summary>
        /// データの検索
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
            MoiData moi = this.Grid.GetSelectDataObject() as MoiData;
            if (moi == null)
            {
                return;
            }

            //画面初期データの設定
            MoiDetailForm.MoiDetailFormInputData idata = new MoiDetailForm.MoiDetailFormInputData();
            idata.moi_observation_id = moi.Observation.Observation.moi_observation_id;
            
            MoiDetailForm f = new MoiDetailForm();
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


        //----------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 画面の初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            //初期化
            this.FData = new MoiListFormData();
            this.Logic = new MoiListFormLogic(this, this.FData);

            //グリッド
            this.Grid = new MoiListGrid(this.dataGridViewMoi);

            //コントロール初期化
            this.InitDisplayControl();

            //検索条件をクリア
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
                //青モードコントロール
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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoiListForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "MoiListForm_Load");

        }

        /// <summary>
        /// 検索ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonSearch_Click");

            //検索処理
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

            //クリア
            this.Logic.ClearSearchControl();
        }

        /// <summary>
        /// 新規作成ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonCreate_Click");

            MoiCreateForm f = new MoiCreateForm();
            DialogResult dret = f.ShowDialog(this);
            if (dret != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

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
        /// 報告書出力ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOutputReport_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonOutputReport_Click");


            //現在の選択を取得する
            MoiData moi = this.Grid.GetSelectDataObject() as MoiData;
            if (moi == null)
            {
                return;
            }

            MoiOutputReportSettingForm rf = new MoiOutputReportSettingForm();
            rf.SetFormSettingData(moi);
            DialogResult dret = rf.ShowDialog(this);


        }

        /// <summary>
        /// 帳票出力ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOutputExcel_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonOutputExcel_Click");

            MoiOutputExcelSettingForm f = new MoiOutputExcelSettingForm();
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
        private void dataGridViewMoi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DcLog.WriteLog(this, "dataGridViewMoi_CellDoubleClick");

            //詳細画面の起動
            this.SetupDetailForm();
        }


        /// <summary>
        /// VIQ Codeの選択が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxViqCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //現在の選択を取得
            MsViqCode vc = this.comboBoxViqCode.SelectedItem as MsViqCode;
            if (vc == null)
            {
                this.singleLineComboViqNo.Clear();
                return;
            }

            //VIQ No
            ControlItemCreator.CreateMsViqNo(this.singleLineComboViqNo, vc.viq_code_id);
        }


        /// <summary>
        /// 青モードチェック反転
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxBlueReverse_CheckedChanged(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "checkBoxBlueReverse_CheckedChanged");

            //
            //モード設定
            bool f = this.checkBoxBlueReverse.Checked;
            this.Grid.SetAllCheck(f);

        }

        /// <summary>
        /// 青モード更新処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBlueUpdate_Click(object sender, EventArgs e)
        {
            //

            DcLog.WriteLog(this, "buttonBlueUpdate_Click");

            using (WaitingState st = new WaitingState(this))
            {
                //変更物の取得
                List<MoiData> upsrclist = this.Grid.GetEditCheckList<MoiData>();
                var n = from f in upsrclist select f.Observation.Observation;

                List<DcMoiObservation> obslist = n.ToList();


                //更新処理
                bool ret = SvcManager.SvcMana.DcMoiObservation_UpdateStatus(obslist, DcGlobal.Global.LoginMsUser);
                if (ret == false)
                {
                    DcLog.WriteLog("DcMoiObservation_UpdateStatus FALSE");
                    return;
                }


            }

            //再検索を行う
            this.SearchData();
        }

        /// <summary>
        /// VIQ Versionの選択が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxViqVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //現在の選択を取得
            MsViqVersion vv = this.comboBoxViqVersion.SelectedItem as MsViqVersion;
            if (vv == null)
            {
                this.comboBoxViqCode.Items.Clear();
                this.singleLineComboViqNo.Clear();
                return;
            }

            //VIQ Code
            ControlItemCreator.CreateMsViqCode(this.comboBoxViqCode, vv.viq_version_id, true);
        }

        
    }
}
