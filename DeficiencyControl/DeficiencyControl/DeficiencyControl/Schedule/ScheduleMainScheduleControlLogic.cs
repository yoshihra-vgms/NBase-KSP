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

namespace DeficiencyControl.Schedule
{
    /// <summary>
    /// ScheduleMainScheduleControlのロジック コントロールの表示部を受け持つ予定名前の変更は考慮する
    /// </summary>
    public class ScheduleMainScheduleControlLogic
    {
        public ScheduleMainScheduleControlLogic(ScheduleMainScheduleControl c, ScheduleMainScheduleControl.ScheduleMainScheduleControlData fd)
        {
            this.Con = c;
            this.FData = fd;
        }

        /// <summary>
        /// 管理紺コントロール
        /// </summary>
        ScheduleMainScheduleControl Con = null;

        /// <summary>
        /// データ
        /// </summary>
        ScheduleMainScheduleControl.ScheduleMainScheduleControlData FData = null;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        


        /// <summary>
        /// データの表示 this.FData.DispDicに対しての御膳立てが終了していること、またVesselListに対して表示船と順番の御膳立てが終了していること
        /// </summary>
        public void DisplayScheduleList()
        {
            ScheduleListControl.InitData idata = new ScheduleListControl.InitData();
            idata.VesselList = this.FData.VesselList;
            idata.DispDic = this.FData.DispDic;

            //表示
            this.Con.scheduleListControl1.InitControl(idata);
        }
    }
}
