using NBaseData.BLC;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WTM
{
    public class Common
    {
        public static MsUser LoginUser { set; get; }


        public static MsVessel Vessel { set; get; }

        public static MsSenin Senin { set; get; }


        public const int VesselFontSize = 10;


        public static List<SiCard> GetOnSigner(int vesselId, DateTime fromDate, DateTime toDate)
        {
            // 該当日が、当日を含む過去、または、乗船計画がない場合
            // 乗船実績(SiCard）から
            SiCardFilter filter = new SiCardFilter();
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(Common.LoginUser, MsSiShubetsu.SiShubetsu.乗船));
            filter.Start = fromDate;
            filter.End = DateTimeUtils.ToTo(toDate);
            filter.MsVesselIDs.Add(vesselId);

            List<SiCard> cards = null;
#if HONSEN
            cards = 船員.BLC_船員カード検索(Common.LoginUser, SeninTableCache.instance(), filter);

#else
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                cards = serviceClient.BLC_船員カード検索(Common.LoginUser, filter);
            }
#endif

            return cards;
        }
        public static void SetFormSize(Form form)
        {
            //System.Windows.Forms.Screen s = System.Windows.Forms.Screen.FromControl(form);
            //if (s.Bounds.Width < form.Width)
            //    this.Width = s.Bounds.Width;
            //if (s.Bounds.Height < form.Height)
            //    this.Height = s.Bounds.Height;

            int h = System.Windows.Forms.Screen.GetWorkingArea(form).Height;
            int w = System.Windows.Forms.Screen.GetWorkingArea(form).Width;

            var ms = form.MinimumSize;
            if (w < ms.Width)
                ms.Width = w;
            if (h < ms.Height)
                ms.Height = h;

            form.MinimumSize = ms;
            form.Size = ms;

            //var str = $@"this.width[{form.Width}], this.Height[{form.Height}]" + System.Environment.NewLine +
            //    $"s.Bounds.Width[{s.Bounds.Width}], s.Bounds.Height[{s.Bounds.Height}]" + System.Environment.NewLine +
            //    $"GetWorkingArea.Width[{w}], GetWorkingArea.Height[{h}]" + System.Environment.NewLine +
            //    $"MinimumSize.Width[{form.MinimumSize.Width}], MinimumSize.Height[{form.MinimumSize.Height}]";
            //MessageBox.Show(str);

            form.WindowState = FormWindowState.Maximized;
        }
    }
}
