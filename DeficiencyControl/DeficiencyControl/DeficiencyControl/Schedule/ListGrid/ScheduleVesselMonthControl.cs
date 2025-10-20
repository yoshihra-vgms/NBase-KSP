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

namespace DeficiencyControl.Schedule.ListGrid
{
    /// <summary>
    /// スケジュール一か月のデータを表示する物体
    /// </summary>
    public partial class ScheduleVesselMonthControl : UserControl
    {
        public ScheduleVesselMonthControl()
        {
            InitializeComponent();

            DcGlobal.EnableDoubleBuffering(this.panelHeader);
            DcGlobal.EnableDoubleBuffering(this.labelMonth);
            DcGlobal.EnableDoubleBuffering(this.tableLayoutPanelHeader);
            DcGlobal.EnableDoubleBuffering(this.tableLayoutPanelData);
        }

        /// <summary>
        /// これのデータ
        /// </summary>
        public class ScheduleVesselMonthControlData
        {
            /// <summary>
            /// 表示船一覧
            /// </summary>
            public List<MsVessel> VesselList = null;

            /// <summary>
            /// 今月表示データ
            /// </summary>
            public MonthVesselData VData = null;

            /// <summary>
            /// 実データを書くオフセット X=Col Y=Row
            /// </summary>
            public Point DataOffset = new Point();

            /// <summary>
            /// 全データ空でも1は入る。
            /// </summary>
            public int MaxDataCount = 0;

        }

        protected ScheduleVesselMonthControlData FData = null;

        //////////////////////




        /// <summary>
        /// テーブルの初期化
        /// </summary>
        /// <param name="item">ADDするアイテムの基準値</param>
        /// <param name="colcout">列数</param>
        /// <param name="rowcount">行数</param>
        protected void InitTable(ScheduleItemControl item, int colcout, int rowcount)
        {
            //サイズ設定
            this.tableLayoutPanelData.ColumnCount = colcout;
            this.tableLayoutPanelData.RowCount = rowcount;

            //横と縦のサイズを確定し、テーブルを作成
            foreach (ColumnStyle st in this.tableLayoutPanelData.ColumnStyles)
            {
                st.SizeType = SizeType.Absolute;
                st.Width = item.Width;
            }

            foreach (RowStyle st in this.tableLayoutPanelData.RowStyles)
            {
                st.SizeType = SizeType.Absolute;
                st.Height = item.Height;
            }


            //テーブルサイズのコントロールサイズを計算
            this.tableLayoutPanelData.Width = item.Width * colcout;
            this.tableLayoutPanelData.Height = item.Height * rowcount;
        }


        /// <summary>
        /// テーブルコントロールのサイズとテーブルのサイズを確定する
        /// </summary>
        private void InitTableControl()
        {
            //テーブルの参考基準となるサイズを計算する
            ScheduleItemControl item = new ScheduleItemControl();

            //縦のデータ数と横のデータ数を計算
            //横は船数 + その他分
            int w = this.FData.VesselList.Count + 1;

            //縦はデータ数 + 船表示可否
            this.FData.MaxDataCount = this.FData.VData.CalcuMaxDataCount();
            int h = this.FData.MaxDataCount;


            //データオフセットを計算しておく            
            this.FData.DataOffset.Y = 0;
            this.FData.DataOffset.X = 0;

            //ヘッダー
            {
                TableLayoutPanel tpane = this.tableLayoutPanelHeader;
                tpane.ColumnCount = 1;
                tpane.RowCount = this.FData.MaxDataCount;

                tpane.Width = item.Width * 1;
                tpane.Height = item.Height * h;
            }

            //データ
            {
                this.InitTable(item, w, h);

            }

        }

        


        /// <summary>
        /// ヘッダーの表示
        /// </summary>
        private void DispHeader()
        {

            TableLayoutPanel tpane = this.tableLayoutPanelHeader;            
            MonthVesselData vdata = this.FData.VData;

            tpane.Controls.Clear();
            
            //表示
            int ypos = this.FData.DataOffset.Y;
            for (int i = 0; i < this.FData.MaxDataCount; i++)
            {
                ypos += i;

                ScheduleItemControl rh = new ScheduleItemControl();
                rh.TitleText = "種別";
                rh.DetailText = "実績日";
                tpane.Controls.Add(rh, 0, ypos);
                
            }


        }

        /// <summary>
        /// 一個のデータを表示する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private ScheduleItemControl CreatePlanItemControl(DcSchedulePlan data)
        {
            DBDataCache db = DcGlobal.Global.DBCache;

            ScheduleItemControl ans = new ScheduleItemControl();
            if (data == null)
            {
                return ans;
            }


            //タイトルとして詳細を表示
            {
                MsScheduleKindDetail kde = db.GetMsScheduleKindDetail(data.schedule_kind_detail_id);
                if (kde != null)
                {
                    ans.TitleText = kde.ToString();
                }

                //色を設定
                ans.TitleColor = data.DataColor;
            }

            //詳細
            {
                string s = "";
                //日付と実績メモを入れる
                if (data.inspection_date != DcSchedule.EDate)
                {
                    s = data.inspection_date.ToString("MM/dd");
                }

                //追加
                s += " " + data.record_memo;


                ans.DetailText = s;
            }

            return ans;

        }

        /// <summary>
        /// データの表示
        /// </summary>
        private void DispPlanList()
        {
            TableLayoutPanel tpane = this.tableLayoutPanelData;
            MonthVesselData vdata = this.FData.VData;

            tpane.Controls.Clear();
            
            //位置計算
            int colpos = this.FData.DataOffset.X;
            
            //全船データ
            foreach (MsVessel ves in this.FData.VesselList)
            {
                //今回の描画データ取得
                List<DcSchedulePlan> plist = vdata.VesselDic[ves.ms_vessel_id];

                //揃えるため、データ最大数回す
                for (int i = 0; i < this.FData.MaxDataCount; i++)
                {
                    int rowpos = this.FData.DataOffset.Y + i;
                    
                    //対象データの算出                 
                    DcSchedulePlan data = null;
                    if (plist.Count > i)
                    {
                        data = plist[i];                        
                    }

                    //コントロールの作成とADD
                    ScheduleItemControl item = this.CreatePlanItemControl(data);
                    tpane.Controls.Add(item, colpos, rowpos);
                }

                colpos += 1;
            }
            
            

        }





        /// <summary>
        /// その他イベントの表示
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private ScheduleItemControl CreateOtherItemControl(DcScheduleOther data)
        {
            DBDataCache db = DcGlobal.Global.DBCache;

            ScheduleItemControl ans = new ScheduleItemControl();
            if (data == null)
            {
                return ans;
            }


            //タイトル
            {
                ans.TitleText = data.event_memo;

                //色を設定
                ans.TitleColor = data.DataColor;
            }

            //詳細
            {
                string s = "";
                //日付と実績メモを入れる
                if (data.inspection_date != DcSchedule.EDate)
                {
                    s = data.inspection_date.ToString("MM/dd");
                }

                //追加
                s += " " + data.record_memo;


                ans.DetailText = s;
            }

            return ans;

        }


        /// <summary>
        /// その他の表示
        /// </summary>
        private void DispOther()
        {
            TableLayoutPanel tpane = this.tableLayoutPanelData;
            MonthVesselData vdata = this.FData.VData;


            //揃えるため、データ最大数回す
            for (int i = 0; i < this.FData.MaxDataCount; i++)
            {
                int rowpos = this.FData.DataOffset.Y + i;

                //対象データの算出                 
                DcScheduleOther data = null;
                if (vdata.OtherList.Count > i)
                {
                    data = vdata.OtherList[i];
                }

                ScheduleItemControl item = this.CreateOtherItemControl(data);
                tpane.Controls.Add(item, tpane.ColumnCount - 1, rowpos);

            }
        }
       

        /// <summary>
        /// データの描画
        /// </summary>
        /// <param name="veslist">船の数の並び順</param>
        /// <param name="ddata">表示データ一式</param>
        public virtual void Display(List<MsVessel> veslist, MonthVesselData ddata)
        {
            this.FData = new ScheduleVesselMonthControlData();
            this.FData.VesselList = veslist;
            this.FData.VData = ddata;

            try
            {
                this.SuspendLayout();

                //月の描画
                this.labelMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.labelMonth.Text = string.Format("{0}月", ddata.Month);

                //描画領域のおぜん立て
                this.InitTableControl();

                //ヘッダーの表示
                this.DispHeader();


                //予定表示
                this.DispPlanList();

                //その他表示
                this.DispOther();
            }
            finally
            {
                this.ResumeLayout();
            }

        }


        public void SetOffsetX(int x)
        {
            this.panelHeader.Left = x;            
            
        }


        public void VisibleHeader(bool v)
        {
            this.panelHeader.Visible = v;

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleVesselMonth_Load(object sender, EventArgs e)
        {

        }
    }
}
