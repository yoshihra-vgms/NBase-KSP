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

namespace DeficiencyControl.Forms
{
    /// <summary>
    /// PSC帳票出力設定画面
    /// </summary>
    public partial class PscOutputSettingForm : BaseForm
    {
        public PscOutputSettingForm() : base(EFormNo.PSCOutputSetting, false)
        {
            InitializeComponent();
        }

        /// <summary>
        /// 最大出力年数
        /// </summary>
        const int MAXOUTPUT_YEAR = 10;

        /// <summary>
        /// 出力デフォルトファイル名
        /// </summary>
        const string PscListFileName = "PSC実績一覧表";

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面コントロール初期化
        /// </summary>
        private void InitDisplayControl()
        {
            ControlItemCreator.CreateVesselOutput(this.singleLineComboVessel, "ALL");
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

                int st = this.yearPeriodInputControl1.GetStartYear();
                int ed = this.yearPeriodInputControl1.GetEndYear();
                int sa = ed - st;
                if (sa > MAXOUTPUT_YEAR)
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
                DcMes.ShowMessage(this, EMessageID.MI_45);

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
        private PscOutputParameter GetInputData()
        {
            PscOutputParameter ans = new PscOutputParameter();

            //船
            MsVessel ves = this.singleLineComboVessel.SelectedItem as MsVessel;
            if (ves != null)
            {
                ans.ms_vessel_id = ves.ms_vessel_id;
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
                PscOutputParameter idata = this.GetInputData();

                //ファイル選択
                this.saveFileDialog1.AddExtension = true;
                this.saveFileDialog1.Filter = Common.ExcelFileter;
                this.saveFileDialog1.FileName = PscListFileName;
                DialogResult dret = this.saveFileDialog1.ShowDialog(this);
                if (dret != System.Windows.Forms.DialogResult.OK)
                {
                    return true;
                }

                //ファイル名取得
                string filename = this.saveFileDialog1.FileName;


                //保存
                using (WaitingState es = new WaitingState(this))
                {
                    PscExcelFile file = new PscExcelFile();
                    file.OutputExcel(filename, idata);
                }

                //開く
                DcGlobal.ExecuteDefaultApplication(filename);


            }
            catch (Exception e)
            {
                DcLog.WriteLog(e, "WriteExcel");
                DcMes.ShowMessage(this, EMessageID.MI_46);                
                return false;
            }

            return true;

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            //画面初期化
            this.InitDisplayControl();


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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PscOutputSettingForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "PscOutputSettingForm_Load");
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

            //this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
