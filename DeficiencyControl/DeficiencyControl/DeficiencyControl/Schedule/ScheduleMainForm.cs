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

using CIsl;
using CIsl.DB.WingDAC;

namespace DeficiencyControl.Schedule
{
    /// <summary>
    /// スケジュールメイン画面
    /// </summary>
    public partial class ScheduleMainForm : BaseForm
    {
        public ScheduleMainForm()
            : base(EFormNo.ScheduleMain)
        {
            InitializeComponent();

            //タブはダブルバッファ有効にならないらしい。WS_EX_COMPOSITEDを無理やり有効にするが手だが問題ありらしい？
            DcGlobal.EnableDoubleBuffering(this.tabControlSchedule);
            DcGlobal.EnableDoubleBuffering(this.tabPageSchedule);
            DcGlobal.EnableDoubleBuffering(this.tabPagePlan);
            DcGlobal.EnableDoubleBuffering(this.tabPageCompany);
            DcGlobal.EnableDoubleBuffering(this.tabPageOther);
            
        }

        /// <summary>
        /// 出力デフォルトファイル名 スケジュール
        /// </summary>
        const string ScheduleFileName = "有効期限一覧表";

        /// <summary>
        /// 画面データ
        /// </summary>
        public class ScheduleMainFormData
        {
            /// <summary>
            /// 現在の選択年度
            /// </summary>
            public MsYear Year = null;
        }

        /// <summary>
        /// 画面データ
        /// </summary>
        ScheduleMainFormData FData = null;

        /// <summary>
        /// 画面処理
        /// </summary>
        ScheduleMainFormLogic Logic = null;

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面コントロール初期化
        /// </summary>
        private void InitDisplayControl()
        {
            ControlItemCreator.CreateMsYear(this.comboBoxYear);

            //今年度の選択
            int ny = DcCommon.CommonLogic.CalcuYearStart(DateTime.Now.Date).Year;
            foreach (object item in this.comboBoxYear.Items)
            {
                MsYear y = item as MsYear;
                if (y == null)
                {
                    continue;
                }

                //対象の選択
                if (y.year == ny)
                {
                    this.comboBoxYear.SelectedItem = y;
                    break;
                }
            }


        }


        /// <summary>
        /// 出力
        /// </summary>
        private bool WriteExcel()
        {
            if (this.FData.Year == null)
            {
                return false;
            }
            try
            {                

                //ファイル選択
                this.saveFileDialog1.AddExtension = true;
                this.saveFileDialog1.Filter = Common.ExcelFileter;
                this.saveFileDialog1.FileName = ScheduleFileName;
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
                    ScheduleListExcelFile fp = new ScheduleListExcelFile();
                    fp.OutputExcel(filename, this.FData.Year);
                }

                //開く
                DcGlobal.ExecuteDefaultApplication(filename);


            }
            catch (Exception e)
            {
                DcLog.WriteLog(e, "WriteExcel");
                DcMes.ShowMessage(this, EMessageID.MI_62);
                return false;
            }

            return true;

        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 画面の初期化
        /// </summary>
        /// <returns></returns>
        protected override bool InitForm()
        {
            this.FData = new ScheduleMainFormData();
            this.Logic = new ScheduleMainFormLogic(this, this.FData);

            //画面初期化
            this.InitDisplayControl();

            this.Logic.Init();

            return true;
        }

        /// <summary>
        /// ユーザー権限処理
        /// </summary>
        /// <returns></returns>
        protected override bool ControlUserRole()
        {
            //権限取得
            List<MsRole> rolelist = DcGlobal.Global.LoginUser.RoleList;

            RoleData.ControlRoleDelegate dele = new RoleData.ControlRoleDelegate((data, ena) =>
            {
                if (ena == false)
                {
                    this.tabControlSchedule.Controls.Remove(data.Control);
                }
            });

            List<RoleData> rdlist = new List<RoleData>();
            rdlist.Add(new RoleData(ERoleName1.指摘事項管理, ERoleName2.スケジュール, ERoleName3.スケジュール, this.tabPageSchedule, dele));
            rdlist.Add(new RoleData(ERoleName1.指摘事項管理, ERoleName2.スケジュール, ERoleName3.予定実績, this.tabPagePlan, dele));
            rdlist.Add(new RoleData(ERoleName1.指摘事項管理, ERoleName2.スケジュール, ERoleName3.会社, this.tabPageCompany, dele));
            rdlist.Add(new RoleData(ERoleName1.指摘事項管理, ERoleName2.スケジュール, ERoleName3.その他, this.tabPageOther, dele));
            RoleController.ControlRole(rolelist, rdlist);

            

            return true;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleMainForm_Load(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "ScheduleMainForm_Load");
        }

        /// <summary>
        /// 検索ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            using (WaitingState st = new WaitingState(this))
            {
                this.Logic.Search();
            }
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
        /// Excel出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOutput_Click(object sender, EventArgs e)
        {
            DcLog.WriteLog(this, "buttonOutput_Click");

            //Excelの出力
            this.WriteExcel();
        }

       
    }
}
