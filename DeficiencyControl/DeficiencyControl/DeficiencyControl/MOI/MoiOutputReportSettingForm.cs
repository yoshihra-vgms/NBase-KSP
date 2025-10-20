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
using DcCommon.Output;

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
    /// 検船報告書出力画面
    /// </summary>
    public partial class MoiOutputReportSettingForm : BaseForm
    {
        public MoiOutputReportSettingForm()
            : base(EFormNo.MoiOutputReportSetting, false)
        {
            InitializeComponent();


            this.FData = new MoiOutputReportSettingFormData();
        }
        /// <summary>
        /// これのﾃﾞｰﾀ
        /// </summary>
        public class MoiOutputReportSettingFormData
        {
            public MoiData SrcData = null;
        }


        MoiOutputReportSettingFormData FData = null;


        /// <summary>
        /// 出力デフォルトファイル名　検船コメント
        /// </summary>
        const string MoiReportCommentListFileName = "検船コメントリスト";


        /// <summary>
        /// 出力デフォルトファイル名 改善報告書
        /// </summary>
        //const string MoiReportObservationFileName = "他社検船報告書";
        const string MoiReportObservationFileName = "検船指摘事項に対する改善報告書";
        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面コントロール初期化
        /// </summary>
        private void InitDisplayControl()
        {
            //ユーザー            
            ControlItemCreator.CreateUser(this.singleLineComboUser);

            //有効可否の設定
            this.panelReportObservation.Enabled = this.radioButtonReportObservation.Checked;
        }


        /// <summary>
        /// エラーチェック
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            BaseFormLogic lo = new BaseFormLogic();


            try
            {


                lo.ErList = new List<Control>();

                //改善報告書がチェックされていた
                if (this.radioButtonReportObservation.Checked == true)
                {
                    //入力チェック
                    Control[] ckvec = {
                                      
                                      this.textBoxDestCompany,
                                      this.textBoxDestGroup,
                                      this.singleLineComboUser,
                                      this.textBoxInspectionDetail,                                      


                                  };
                    lo.ErList.AddRange(ComLogic.CheckInput(ckvec));
                }
               


                //エラーがないなら終わり
                if (lo.ErList.Count <= 0)
                {
                    return true;
                }


                throw new Exception("InputErrorException");

            }
            catch (Exception e)
            {

                lo.DispError();
                DcMes.ShowMessage(this, EMessageID.MI_60);

            }
            finally
            {
                lo.ResetError();
            }

            return false;
        }



        /// <summary>
        /// 入力データの取得+
        /// </summary>
        /// <returns></returns>
        private MoiReportOutputParameter GetInputData()
        {
            MoiReportOutputParameter ans = new MoiReportOutputParameter();

            ans.moi_observation_id = this.FData.SrcData.Observation.Observation.moi_observation_id;

            ans.Kind = EOutputReportKind.コメントリスト;
            if (this.radioButtonReportObservation.Checked == true)
            {
                ans.Kind = EOutputReportKind.改善報告書;
            }
            
            //宛先会社名
            ans.DistCompany = this.textBoxDestCompany.Text.Trim();
            //部署名
            ans.DestGroup = this.textBoxDestGroup.Text.Trim();

            //作成者
            ans.CreateUser = this.singleLineComboUser.SelectedItem as MsUser;

            //検船詳細
            ans.InspectionDetail = this.textBoxInspectionDetail.Text.Trim();

            return ans;

        }


        /// <summary>
        /// 出力
        /// </summary>
        private bool WriteExcel()
        {
            //入力チェック
            bool ret = this.CheckInput();
            if (ret == false)
            {
                return false;
            }

            try
            {
                //入力データ取得
                MoiReportOutputParameter param = this.GetInputData();


                //ファイル名の確定
                string filename = MoiReportCommentListFileName;
                if (param.Kind == EOutputReportKind.改善報告書)
                {
                    filename = MoiReportObservationFileName;
                }

                //ファイル選択
                this.saveFileDialog1.AddExtension = true;
                this.saveFileDialog1.Filter = Common.ExcelFileter;
                this.saveFileDialog1.FileName = filename;
                DialogResult dret = this.saveFileDialog1.ShowDialog(this);
                if (dret != System.Windows.Forms.DialogResult.OK)
                {
                    return true;
                }

                //ファイル名取得
                filename = this.saveFileDialog1.FileName;


                //保存
                using (WaitingState es = new WaitingState(this))
                {
                    switch (param.Kind)
                    {

                        case EOutputReportKind.コメントリスト:
                            MoiReportFileCommentList clfile = new MoiReportFileCommentList();
                            clfile.OutputExcel(filename, param);
                            break;



                        case EOutputReportKind.改善報告書:
                            MoiReportFileObservation obfile = new MoiReportFileObservation();
                            obfile.OutputExcel(filename, param);
                            break;


                        default:
                            throw new Exception("EOutputReportKind FALSE");
                    }

                }

                //開く
                DcGlobal.ExecuteDefaultApplication(filename);


            }
            catch (Exception e)
            {
                DcMes.ShowMessage(this, EMessageID.MI_61);
                return false;
            }

            return true;

        }


        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// ﾃﾞｰﾀの設定
        /// </summary>
        /// <param name="fsetdata">MOIData</param>
        /// <returns></returns>
        public override bool SetFormSettingData(object fsetdata)
        {
            this.FData.SrcData = fsetdata as MoiData;

            return true;
        }

        /// <summary>
        /// 画面の初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            //画面初期化
            this.InitDisplayControl();

            //初期チェック
            this.radioButtonReportCommentList.Checked = true;

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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoiOutputReportSettingForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "MoiOutputReportSettingForm_Load");
        }

        /// <summary>
        /// 出力ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOutputReport_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonOutputReport_Click");

            this.WriteExcel();
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
        /// 改善報告書のチェックが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonReportObservation_CheckedChanged(object sender, EventArgs e)
        {
            this.panelReportObservation.Enabled = this.radioButtonReportObservation.Checked;

        }

        
    }
}
