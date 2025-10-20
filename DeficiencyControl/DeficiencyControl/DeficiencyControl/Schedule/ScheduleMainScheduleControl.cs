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

using DeficiencyControl.Schedule.ListGrid;
using DcCommon;

namespace DeficiencyControl.Schedule
{
    /// <summary>
    /// スケジュールメイン画面、スケジュールタブコントロール
    /// </summary>
    public partial class ScheduleMainScheduleControl : BaseControl
    {
        public ScheduleMainScheduleControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// これの入力データ
        /// </summary>
        public class InitData
        {
            public MsYear Year = null;
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        public class ScheduleMainScheduleControlData
        {
            /// <summary>
            /// 選択年度
            /// </summary>
            public MsYear Year = null;


            /// <summary>
            /// これの表示データ [月, 月データ]
            /// </summary>
            public Dictionary<int, MonthVesselData> DispDic = null;

            /// <summary>
            /// 表示対象の船一式
            /// </summary>
            public List<MsVessel> VesselList = null;
             
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        public ScheduleMainScheduleControlData FData = null;


        ScheduleMainScheduleControlLogic Logic = null;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        


        


        
        


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="inputdata">InitData</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new ScheduleMainScheduleControlData();
            this.Logic = new ScheduleMainScheduleControlLogic(this, this.FData);

            InitData idata = inputdata as InitData;
            this.FData.Year = idata.Year;


            //表示対象となれる船を一覧取得・・・これは一部だけ表示を考慮するためにこうする
            this.FData.VesselList = DcGlobal.Global.DBCache.VesselList;

            //検索・・・今年度の予定を検索
            //船予定
            SchedulePlanSearchData psdata = new SchedulePlanSearchData();
            psdata.estimate_date_start = this.FData.Year.start_date;
            psdata.estimate_date_end = this.FData.Year.end_date;
            List<DcSchedulePlan> plist = SvcManager.SvcMana.DcSchedulePlan_GetRecordsBySearchData(psdata);

            //その他予定
            ScheduleOtherSearchData osdata = new ScheduleOtherSearchData();
            osdata.estimate_date_start = this.FData.Year.start_date;
            osdata.estimate_date_end = this.FData.Year.end_date;
            List<DcScheduleOther> otlist = SvcManager.SvcMana.DcScheduleOther_GetRecordsBySearchData(osdata);


            //今年度表示する形式に変換
            this.FData.DispDic = MonthVesselData.CreateMonthVesselDataDic(this.FData.Year.year, plist, otlist, this.FData.VesselList);


            //表示
            this.Logic.DisplayScheduleList();
           

            return true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleMainScheduleControl_Load(object sender, EventArgs e)
        {

        }
    }
}
