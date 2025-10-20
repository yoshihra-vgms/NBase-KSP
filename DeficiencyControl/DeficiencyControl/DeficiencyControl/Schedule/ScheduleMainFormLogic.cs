using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using CIsl.DB.WingDAC;
using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Controls.CommentItem;

using DeficiencyControl.Logic;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;

namespace DeficiencyControl.Schedule
{
    /// <summary>
    /// スケジュールメイン画面ロジック
    /// </summary>
    public class ScheduleMainFormLogic : BaseFormLogic
    {
        public ScheduleMainFormLogic(ScheduleMainForm f, ScheduleMainForm.ScheduleMainFormData fd)
        {
            this.Form = f;
            this.FData = fd;
        }
        
        /// <summary>
        /// 管理画面
        /// </summary>
        private ScheduleMainForm Form = null;

        /// <summary>
        /// データ
        /// </summary>
        private ScheduleMainForm.ScheduleMainFormData FData = null;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            //ScheduleMainPlanControl.InitData idata = new ScheduleMainPlanControl.InitData();

            //タブを消しておく
            this.Form.scheduleMainScheduleControl1.Visible = false;
            this.Form.scheduleMainPlanControl1.Visible = false;
            this.Form.scheduleMainCompanyControl1.Visible = false;
            this.Form.scheduleMainOtherControl1.Visible = false;
            
        }

        /// <summary>
        /// 他のタブの挿入更新
        /// </summary>
        /// <param name="cate"></param>
        private void UpdateDelegateProc(EScheduleCategory cate)
        {
            MsYear year = this.FData.Year;

            //スケジュールの再描画
            this.InitScheduleControl(year);
            /*
            //個別でやるときはこっち
            switch (cate)
            {
                case EScheduleCategory.予定実績:
                    break;

                case EScheduleCategory.会社:
                    break;

                case EScheduleCategory.その他:
                    break;
            }*/
        }


        /// <summary>
        /// スケジュールタブの初期化
        /// </summary>
        private void InitScheduleControl(MsYear year)
        {
            ScheduleMainForm f = this.Form;            

            ScheduleMainScheduleControl.InitData idata = new ScheduleMainScheduleControl.InitData();
            idata.Year = year;
            f.scheduleMainScheduleControl1.Visible = true;
            f.scheduleMainScheduleControl1.InitControl(idata);
        }

        /// <summary>
        /// データの検索 WaitStateせよ
        /// </summary>
        public void Search()
        {
            ScheduleMainForm f = this.Form;
            MsYear year = f.comboBoxYear.SelectedItem as MsYear;

            //検索した年度を保存
            this.FData.Year = year;

            //各タブで検索を行う
            //スケジュール
            {
                this.InitScheduleControl(year);
            }
            //予定実績
            {
                ScheduleMainPlanControl.InitData idata = new ScheduleMainPlanControl.InitData();
                idata.Year = year;

                f.scheduleMainPlanControl1.UpdateDelegateProc = this.UpdateDelegateProc;
                f.scheduleMainPlanControl1.Visible = true;
                f.scheduleMainPlanControl1.InitControl(idata);
            }
            //会社
            {
                ScheduleMainCompanyControl.InitData idata = new ScheduleMainCompanyControl.InitData();
                idata.Year = year;

                f.scheduleMainCompanyControl1.UpdateDelegateProc = this.UpdateDelegateProc;
                f.scheduleMainCompanyControl1.Visible = true;
                f.scheduleMainCompanyControl1.InitControl(idata);
            }
            //その他
            {
                ScheduleMainOtherControl.InitData idata = new ScheduleMainOtherControl.InitData();
                idata.Year = year;

                f.scheduleMainOtherControl1.UpdateDelegateProc = this.UpdateDelegateProc;
                f.scheduleMainOtherControl1.Visible = true;
                f.scheduleMainOtherControl1.InitControl(idata);
            }

        }
    }
}
