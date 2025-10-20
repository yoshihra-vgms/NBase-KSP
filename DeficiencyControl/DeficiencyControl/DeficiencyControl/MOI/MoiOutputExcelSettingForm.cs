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
    /// 検船帳票出力画面
    /// </summary>
    public partial class MoiOutputExcelSettingForm : BaseForm
    {
        public MoiOutputExcelSettingForm()
            : base(EFormNo.MoiOutputExcelSetting, false)
        {
            InitializeComponent();
        }


        /// <summary>
        /// 出力デフォルトファイル名 検船章別
        /// </summary>
        const string MoiExcelCategoryFileName = "検船章別指摘数";

        /// <summary>
        /// 出力デフォルトファイル名是正対応
        /// </summary>
        const string MoiExcelListFileName = "指摘内容・是正対応リスト";


        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面コントロール初期化
        /// </summary>
        private void InitDisplayControl()
        {
            //特になし            
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

                //年度チェック
                bool ret = this.yearPeriodInputControl1.CheckError(true);
                if (ret == false)
                {
                    lo.ErList.Add(this.yearPeriodInputControl1);
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
                DcMes.ShowMessage(this, EMessageID.MI_58);

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
        private MoiExcelOutputParameter GetInputData()
        {
            MoiExcelOutputParameter ans = new MoiExcelOutputParameter();

            

            //種別
            ans.Kind = EMoiExcelOutputKind.項目別指摘数;
            if (this.radioButtonExcelList.Checked == true)
            {
                ans.Kind = EMoiExcelOutputKind.是正対応リスト;
            }

            //年度
            ans.StartYear = this.yearPeriodInputControl1.GetStartYear();
            ans.EndYear = this.yearPeriodInputControl1.GetEndYear();


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
                MoiExcelOutputParameter param = this.GetInputData();


                //デフォルトファイル名の設定
                string filename = MoiExcelCategoryFileName;
                if (param.Kind == EMoiExcelOutputKind.是正対応リスト)
                {
                    filename = MoiExcelListFileName;
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

                        case EMoiExcelOutputKind.項目別指摘数:
                            MoiExcelCategoryFile catefile = new MoiExcelCategoryFile();
                            catefile.OutputExcel(filename, param);
                            break;



                        case EMoiExcelOutputKind.是正対応リスト:
                            MoiExcelListFile lifile = new MoiExcelListFile();
                            lifile.OutputExcel(filename, param);
                            break;


                        default:
                            throw new Exception("EMoiExcelOutputKind FALSE");
                    }
                
                }

                //開く
                DcGlobal.ExecuteDefaultApplication(filename);


            }
            catch (Exception e)
            {
                DcMes.ShowMessage(this, EMessageID.MI_59);
                return false;
            }

            return true;

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面の初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            //画面初期化
            this.InitDisplayControl();

            //初期チェック
            this.radioButtonExcelCategory.Checked = true;

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

        ////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoiOutputExcelForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "MoiOutputExcelForm_Load");
        }



        /// <summary>
        /// 出力ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOutputExcel_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonOutputExcel_Click");

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
    }
}
