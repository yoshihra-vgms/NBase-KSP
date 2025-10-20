using NBaseData.DAC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrapeCity.Win.CalendarGrid;
using NBaseData.DS;

namespace Senin
{
    public partial class 配乗計画長期Form : 配乗計画BaseForm
    {
        private static 配乗計画長期Form instance;


        #region ツールチップ(日付)表示で使う
        private ToolTip ToolTip1 = new ToolTip();
        private Timer Timer1 = new Timer();
        private String ToolTipText;
        private List<string> WeekList = new List<string>() { "(日)", "(月)", "(火)", "(水)", "(木)", "(金)", "(土)" };
        #endregion





        private 配乗計画長期Form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// インスタンス
        /// </summary>
        /// <returns></returns>
        public static 配乗計画長期Form Instance()
        {
            if (instance == null)
            {
                instance = new 配乗計画長期Form();
            }

            return instance;
        }

        private void 配乗計画長期Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }

        public static void DisposeInstance()
        {
            instance = null;
        }

        private void 配乗計画長期Form_Load(object sender, EventArgs e)
        {
            #region カレンダーの体裁

            gcCalendarGrid1.Template.ColumnHeader.Columns[0].Width = 8;
            gcCalendarGrid1.Template.ColumnHeader.Rows[0].Cells[0].AutoMergeMode = AutoMergeMode.Horizontal;
            gcCalendarGrid1.Template.ColumnHeader.Rows[1].Cells[0].AutoMergeMode = AutoMergeMode.Horizontal;
            gcCalendarGrid1.Template.ColumnHeader.Rows[0].CellStyle.ForeColor = Color.Black;
            gcCalendarGrid1.Template.ColumnHeader.Rows[1].CellStyle.ForeColor = Color.Black;

            InitCalendarStyle();//セルごとのスタイルのセット

            var listView = new CalendarListView();
            listView.Orientation = Orientation.Horizontal;
            listView.DayCount = 190;
            gcCalendarGrid1.CalendarView = listView;

            #endregion

            #region ツールチップ設定
            //ToolTipTextの設定を無効にする ShowCellToolTips=false
            gcCalendarGrid1.Template.ColumnHeader.Rows[0].Tag = "ツールチップ";

            Timer1.Tick += new System.EventHandler(this.OnTimerTick);
            #endregion

            InitializeForm(MsPlanType.PlanTypeHarfPeriod, Shokumei.内航,  gcCalendarGrid1, calendarTitleCaption3, calendarTitleCaption4);
        }


        #region 条件によるセルのスタイル設定
        private void InitCalendarStyle()
        {
            // ユーザー定義のセルスタイルを設定します。
            CalendarDynamicCellStyle cellStyle = new CalendarDynamicCellStyle();
            cellStyle.Condition = new DynamicCellStyleCondition(GetDayStyle);
            cellStyle.Name = "style";

            gcCalendarGrid1.Styles.Add(cellStyle);
            gcCalendarGrid1.Template.Content.CellStyleName = "style";
        }

        public CalendarCellStyle GetDayStyle(CellStyleContext context)
        {

            CalendarCellStyle cellStyle = new CalendarCellStyle();
            CalendarCell cell = gcCalendarGrid1[context.CellPosition.Date][context.CellPosition.RowIndex, context.CellPosition.ColumnIndex];

            if (context.CellPosition.Date.Day == 1)
            {
                cellStyle.LeftBorder = new CalendarBorderLine(Color.Gray, BorderLineStyle.Thin);
            }
            cellStyle.RightBorder = new CalendarBorderLine(Color.Gray, BorderLineStyle.Dotted);

            return cellStyle;
        }
        #endregion

        private new void gcCalendarGrid1_AppointmentCellDragging(object sender, GrapeCity.Win.CalendarGrid.AppointmentCellDraggingEventArgs e)
        {
            base.gcCalendarGrid1_AppointmentCellDragging(sender, e);
        }

        private new void gcCalendarGrid1_CellMouseClick(object sender, GrapeCity.Win.CalendarGrid.CalendarCellMouseEventArgs e)
        {
            base.gcCalendarGrid1_CellMouseClick(sender, e);
        }

        private new void gcCalendarGrid1_CellMouseDoubleClick(object sender, GrapeCity.Win.CalendarGrid.CalendarCellMouseEventArgs e)
        {
            base.gcCalendarGrid1_CellMouseDoubleClick(sender, e);
        }

        private new void gcCalendarGrid1_CellMouseUp(object sender, GrapeCity.Win.CalendarGrid.CalendarCellMouseEventArgs e)
        {
            base.gcCalendarGrid1_CellMouseUp(sender, e);
        }

        private new void gcCalendarGrid1_FirstDateInViewChanged(object sender, EventArgs e)
        {
            base.gcCalendarGrid1_FirstDateInViewChanged(sender, e);
        }









        #region ツールチップ(日付)表示
        private void gcCalendarGrid1_CellMouseEnter(object sender, CalendarCellEventArgs e)
        {
            if ((!e.CellPosition.IsEmpty && e.CellPosition.Scope == CalendarTableScope.ColumnHeader))
            {
                //object text = gcCalendarGrid1.Content[e.CellPosition.Date][e.CellPosition.RowIndex, e.CellPosition.ColumnIndex].Tag;


                DateTime[] dtlist = e.CellPosition.RelativeDates;

                if (dtlist != null && dtlist.Length > 0)
                {
                    //System.Diagnostics.Debug.WriteLine("a");
                    int youbi = (int)dtlist[0].DayOfWeek;
                    this.ToolTipText = dtlist[0].ToShortDateString() + WeekList[youbi];//text.ToString();
                    Timer1.Interval = 500;
                    Timer1.Start();
                }
                else
                {
                    this.ToolTipText = null;
                    ToolTip1.Hide(gcCalendarGrid1);
                    Timer1.Stop();
                }
            }
        }

        private void gcCalendarGrid1_CellMouseLeave(object sender, CalendarCellEventArgs e)
        {
            ToolTip1.Hide(gcCalendarGrid1);
            Timer1.Stop();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            Point point = gcCalendarGrid1.PointToClient(Control.MousePosition);
            point.Y += 20;
            ToolTip1.Show(this.ToolTipText, gcCalendarGrid1, point, 5000);
            Timer1.Stop();
        }
        #endregion

    }
}
