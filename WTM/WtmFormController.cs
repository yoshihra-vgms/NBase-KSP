using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTM
{
    public class WtmFormController
    {
        public static bool Displayed { set; get; } = false;


        private static 日表示Form DayForm { set; get; }
        private static 個人表示Form ParsonalForm { set; get; }
        private static 月表示Form MonthlyForm { set; get; }

        public static void Show_日表示Form()
        {
            DisposeForms();
            if (DayForm != null)
            {
                DayForm.Dispose();
            }
            DayForm = new 日表示Form();
            DayForm.Show();
            Displayed = true;
        }

        public static void Show_日表示Form(int vesselId, DateTime date)
        {
            HideForms();
            if (DayForm != null)
            {
                DayForm.Dispose();
            }
            DayForm = new 日表示Form(vesselId, date);
            DayForm.Show();
        }

        public static void Show_個人表示Form(int vesselId, int seninId, DateTime date)
        {
            HideForms();
            if (ParsonalForm != null)
            {
                ParsonalForm.Dispose();
            }
            ParsonalForm = new 個人表示Form(vesselId, seninId, date);
            ParsonalForm.Show();
        }

        public static void Show_月表示Form(DateTime date)
        {
            HideForms();
            if (MonthlyForm != null)
            {
                MonthlyForm.Dispose();
            }
            MonthlyForm = new 月表示Form(date);
            MonthlyForm.Show();
        }

        private static void HideForms()
        {
            if (DayForm != null)
            {
                DayForm.Hide();
            }
            if (MonthlyForm != null)
            {
                MonthlyForm.Hide();
            }
            if (ParsonalForm != null)
            {
                ParsonalForm.Hide();
            }
        }

        public static void DisposeForms()
        {
            if (DayForm != null)
            {
                DayForm.Dispose();
            }
            if (MonthlyForm != null)
            {
                MonthlyForm.Dispose();
            }
            if (ParsonalForm != null)
            {
                ParsonalForm.Dispose();
            }
        }
    }
}
