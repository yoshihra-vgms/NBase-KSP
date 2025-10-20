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
using DeficiencyControl.Controls;

using DeficiencyControl.Logic;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;
using CIsl.DB.WingDAC;
using DcCommon;


namespace DeficiencyControl.Schedule.ListGrid
{
    /// <summary>
    /// スケジュールリスト部分表示コントロール
    /// </summary>
    /// <remarks>ScheduleVesselMonthControlの統括者</remarks>
    public partial class ScheduleListControl : BaseControl
    {
        public ScheduleListControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// これの表示データ
        /// </summary>
        public class InitData
        {
            /// <summary>
            /// 表示船一覧
            /// </summary>
            public List<MsVessel> VesselList = null;


            /// <summary>
            /// これの表示データ [月, 月データ]
            /// </summary>
            public Dictionary<int, MonthVesselData> DispDic = null;
        }


        /// <summary>
        /// これのデータ
        /// </summary>
        private class ScheduleListControlData
        {
            /// <summary>
            /// 表示船一覧
            /// </summary>
            public List<MsVessel> VesselList = null;


            /// <summary>
            /// これの表示データ [月, 月データ]
            /// </summary>
            public Dictionary<int, MonthVesselData> DispDic = null;

            /// <summary>
            /// 管理データコントロール一式
            /// </summary>
            public List<ScheduleVesselMonthControl> VesselControlList = new List<ScheduleVesselMonthControl>();
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        private ScheduleListControlData FData = null;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 全データの描画
        /// </summary>
        private void DispMonthVessel()
        {
            FlowLayoutPanel fpane = this.customFlowLayoutMonthVesselData;
            fpane.Controls.Clear();
            this.FData.VesselControlList = new List<ScheduleVesselMonthControl>();
            


            //ヘッダー
            this.scheduleListHeaderControl1.Display(this.FData.VesselList, null);
            this.FData.VesselControlList.Add(this.scheduleListHeaderControl1);


            List<int> monthvec = CommonLogic.CreateMonthOrder();

            int i = 0;
            foreach (int month in monthvec)
            {
                MonthVesselData vdata = this.FData.DispDic[month];
              

                //データ表示
                ScheduleVesselMonthControl vcon = new ScheduleVesselMonthControl();
                vcon.Display(this.FData.VesselList, vdata);

                fpane.Controls.Add(vcon);
                fpane.Controls.SetChildIndex(vcon, i);

                //管理へ
                this.FData.VesselControlList.Add(vcon);

                i++;
            }


            this.scheduleListHeaderControl1.Top = 0;
            this.FData.VesselControlList.ForEach(x =>
            {

                x.SetOffsetX(this.HorizontalScroll.Value);

            });

        }


        
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// コントロールの初期化
        /// </summary>
        /// <param name="inputdata">InitData</param>
        /// <returns></returns>
        public override bool InitControl(object inputdata)
        {
            this.FData = new ScheduleListControlData();

            InitData idata = inputdata as InitData;
            this.FData.VesselList = idata.VesselList;
            this.FData.DispDic = idata.DispDic;


            //描画処理
            try
            {
                this.SuspendLayout();
                this.DispMonthVessel();
            }
            finally
            {
                this.ResumeLayout();
            }
            
            
            return true;
        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleListControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// スクロールしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleListControl_Scroll(object sender, ScrollEventArgs e)
        {
            this.scheduleListHeaderControl1.Top = 0;
            
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                this.FData.VesselControlList.ForEach(x =>
                {
                    x.SetOffsetX(e.NewValue);
                });
                
            }
            
            
        }

      
    }
}
