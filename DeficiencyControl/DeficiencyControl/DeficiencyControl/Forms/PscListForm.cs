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

namespace DeficiencyControl.Forms
{
    /// <summary>
    /// PSC一覧画面
    /// </summary>
    public partial class PscListForm : BaseForm
    {
        public PscListForm() : base(EFormNo.PSCList, false)
        {
            InitializeComponent();
        }


        /// <summary>
        /// この画面のデータクラス
        /// </summary>
        public class PscListFormData
        {
            /// <summary>
            /// データ表示リスト
            /// </summary>
            public List<PSCInspectionData> DispList = new List<PSCInspectionData>();
        }


        #region メンバ変数
        /// <summary>
        /// 画面処理
        /// </summary>
        private PscListFormLogic Logic = null;
        
        /// <summary>
        /// 画面データ
        /// </summary>
        private PscListFormData FData = null;


        /// <summary>
        /// グリッド管理
        /// </summary>
        public PSCListGrid Grid = null;
        #endregion 

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitDisplayControl()
        {
            //Vessel
            ControlItemCreator.CreateVessel(this.singleLineComboVessel);

            //Kind
            ControlItemCreator.CreateItemKind(this.comboBoxItemKind);

            //Port
            ControlItemCreator.CreateBasho(this.singleLineComboPort);
            
            //国
            ControlItemCreator.CreateRegional(this.singleLineComboCountry);

            //PIC
            ControlItemCreator.CreateUser(this.singleLineComboUser);

        }

        /// <summary>
        /// 検索 中でwaitstateしています 注意
        /// </summary>
        private void Search()
        {
            //再検索
            using (WaitingState st = new WaitingState(this))
            {
                this.Logic.SearchData();
            }
        }


        /// <summary>
        /// 詳細画面の起動
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private void StartupDatailForm(PSCInspectionData data)
        {
            //詳細データの設定
            CommentItemDetailForm.CommentItemDetailFormInitData indata = new CommentItemDetailForm.CommentItemDetailFormInitData();
            indata.SrcItem = data.PscInspection;


            //詳細画面の起動
            CommentItemDetailForm f = new CommentItemDetailForm();
            f.SetFormSettingData(indata);
            DialogResult dret = f.ShowDialog(this);

            if (dret == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }


            //再検索
            this.Search();
        }

        //------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 画面の初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            //管理の作成
            this.FData = new PscListFormData();
            this.Logic = new PscListFormLogic(this, this.FData);

            //画面コントロールの初期化
            this.InitDisplayControl();

            //グリッド管理
            this.Grid = new PSCListGrid(this.dataGridViewPSC);

            //コントロールクリア
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
            //this.buttonCreate.Enabled = rdata.comment_regist;            

            return true;
        }


        /// <summary>
        /// 裏モード置き換え
        /// </summary>
        protected override void ChangeDeficiencyMode()
        {
            #region 青モード
            {
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


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PscListForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "PscListForm_Load");
        }

        /// <summary>
        /// 新規作成ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonCreate_Click");

            CommentItemCreateForm f = new CommentItemCreateForm();

            DialogResult dret = f.ShowDialog(this);
            if (dret != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            //再検索
            this.Search();

        }

        /// <summary>
        /// 編集ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDetail_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonDetail_Click");

            //選択データ取得
            PSCInspectionData data = this.Grid.GetSelectDataObject() as PSCInspectionData;
            if (data == null)
            {
                return;
            }

            //詳細画面の起動
            this.StartupDatailForm(data);
            

            

        }

        /// <summary>
        /// /Excel出力ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOutputExcel_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonOutputExcel_Click");

            PscOutputSettingForm f = new PscOutputSettingForm();
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
        /// 検索ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonSearch_Click");


            //検索
            this.Search();

        }

        /// <summary>
        /// クリアボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonClear_Click");

            //検索クリア
            this.Logic.ClearSearchControl();
        }


        /// <summary>
        /// グリッドが選択されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewPSC_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DcLog.WriteLog(this, "dataGridViewPSC_CellDoubleClick");


            //選択データ取得
            PSCInspectionData data = this.Grid.GetSelectDataObject() as PSCInspectionData;
            if (data == null)
            {
                return;
            }

            //詳細画面の起動
            this.StartupDatailForm(data);

        }



        /// <summary>
        /// Deficiency裏モード青 チェック反転ボタンが押されたとき
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
        /// Deficiency裏モード青 更新ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBlueUpdate_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonBlueUpdate_Click");

            using (WaitingState st = new WaitingState(this))
            {
                //変更物の取得
                List<PSCInspectionData> upsrclist = this.Grid.GetEditCheckList<PSCInspectionData>();
                var n = from f in upsrclist select f.PscInspection;

                List<DcCiPscInspection> psclist = n.ToList();


                //更新処理
                bool ret = SvcManager.SvcMana.DcCiPscInspection_UpdateStatus(psclist, DcGlobal.Global.LoginMsUser);
                if (ret == false)
                {
                    DcLog.WriteLog("DcCiPscInspection_UpdateStatus FALSE");
                    return;
                }

                
            }

            //再検索を行う
            this.Search();
        }

        
    }
}
