using NBaseData.DAC;
using NBaseData.DS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Senin
{
    public partial class 配乗計画短期Form : 配乗計画BaseForm
    {
        private static 配乗計画短期Form instance;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private 配乗計画短期Form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// インスタンス
        /// </summary>
        /// <returns></returns>
        public static 配乗計画短期Form Instance()
        {
            if (instance == null)
            {
                instance = new 配乗計画短期Form();
            }

            return instance;
        }


        /// <summary>
        /// 画面閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 配乗計画短期Form_FormClosed(object sender, FormClosedEventArgs e)
        private void 配乗計画短期Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }
        #endregion

        public static void DisposeInstance()
        {
            instance = null;
        }


        private void 配乗計画短期Form_Load(object sender, EventArgs e)
        {
            InitializeForm(MsPlanType.PlanTypeOneMonth, Shokumei.フェリー, gcCalendarGrid1, calendarTitleCaption1, calendarTitleCaption2);
        }

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
    }
}
