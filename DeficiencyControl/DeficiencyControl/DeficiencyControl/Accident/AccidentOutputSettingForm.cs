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

namespace DeficiencyControl.Accident
{
    /// <summary>
    /// 事故トラブル帳票出力設定画面
    /// </summary>
    public partial class AccidentOutputSettingForm : BaseForm
    {
        public AccidentOutputSettingForm()
            : base(EFormNo.AccidentOutputSetting)
        {
            InitializeComponent();
        }


        /// <summary>
        /// 出力デフォルトファイル名
        /// </summary>
        const string AccidentOutputFileName = "事故分類・発生状況集計表";


        const int MAXOUTPUT_YEAR = 20;
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面コントロール初期化
        /// </summary>
        private void InitDisplayControl()
        {
            //ControlItemCreator.CreateVessel(this.singleLineComboVessel);            
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

                //最大年数のチェック
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
                DcMes.ShowMessage(this, EMessageID.MI_43);

            }
            finally
            {
                lo.ResetError();
            }

            return false;
        }


        /// <summary>
        /// 選択Kindの取得
        /// </summary>
        /// <returns></returns>
        private EAccidentOutputKind GetSelectKind()
        {
            EAccidentOutputKind ans = new EAccidentOutputKind();

            RadioButton[] buvec = {
                                      this.radioButtonKindVessel,
                                      this.radioButtonKindIGTOwnner,
                                      this.radioButtonKindIGTOperate,
                                      this.radioButtonKindCharter
                                  };

            foreach (RadioButton bu in buvec)
            {
                if (bu.Checked == true)
                {
                    int n = Convert.ToInt32(bu.Tag);
                    ans = (EAccidentOutputKind)n;
                }
            }

            return ans;
        }

        /// <summary>
        /// 入力データの取得+
        /// </summary>
        /// <returns></returns>
        private AccidentOutputParameter GetInputData()
        {
            AccidentOutputParameter ans = new AccidentOutputParameter();

            //選択種別の取得
            ans.OutputKind = this.GetSelectKind();
            

            //船
            MsVessel ves = this.singleLineComboVessel.SelectedItem as MsVessel;
            if(ves!= null)
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
                AccidentOutputParameter param = this.GetInputData();

                //ファイル選択
                this.saveFileDialog1.AddExtension = true;                
                this.saveFileDialog1.Filter = Common.ExcelFileter;
                this.saveFileDialog1.FileName = AccidentOutputFileName;
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
                    AccidentExcelFile file = new AccidentExcelFile();
                    file.OutputExcel(filename, param);
                }
                
                //開く
                DcGlobal.ExecuteDefaultApplication(filename);


            }
            catch (Exception e)
            {
                DcMes.ShowMessage(this, EMessageID.MI_44);
                return false;
            }

            return true;

        }
        //----------------------------------------------------------------------------------------------------

        /// <summary>
        /// 画面初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            //画面初期化
            this.InitDisplayControl();


            //初期チェック
            this.radioButtonKindVessel.Checked = true;

            

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
        

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccidentOutputSettingForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "AccidentOutputSettingForm_Load");
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


        /// <summary>
        /// 出力種別でチェックのプロパティが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonKind_CheckedChanged(object sender, EventArgs e)
        {
            EAccidentOutputKind k = this.GetSelectKind();
            this.singleLineComboVessel.Enabled = false;
            if (k == EAccidentOutputKind.船毎)
            {
                this.singleLineComboVessel.Enabled = true;
            }
        }
    }
}
