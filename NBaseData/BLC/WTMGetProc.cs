using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBaseData.DAC;
using System.IO;
using WtmModelBase;
using WtmModels;
//using WtmModels.Utt;

namespace NBaseData.BLC
{
    public class WTMGetProc
    {
        public static int Excecute(MsUser loginUser, string appName)
        {
            int ret = -1;
            try
            {
                string GetExePath = System.Configuration.ConfigurationManager.AppSettings["GetExePath"];
                if (GetExePath != null && GetExePath.Length > 0)
                {
                    var proc = new System.Diagnostics.Process();

                    proc.StartInfo.FileName = GetExePath;
                    proc.StartInfo.Arguments = appName;
                    proc.StartInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
                    proc.StartInfo.UseShellExecute = false; // シェル機能を使用しない
                    proc.Start();

                    proc.WaitForExit();

                    ret = proc.ExitCode;
                }
            }
            catch (Exception ex)
            {
                ret = -1;
            }
            return ret;
        }



        public static List<WorkContent> GetWorkContents(MsUser loginUser, string appName)
        {
            List<WorkContent> list = WtmModelGetter.GetWorkContents(appName);


            List<WorkContent> ret = new List<WorkContent>();

            foreach (WorkContent wc in list)
            {
                WorkContent w = new WorkContent();

                w.SiteID = wc.SiteID;
                w.WorkContentID = wc.WorkContentID;

                w.Name = wc.Name;
                w.DspName = wc.DspName;
                w.BgColor = wc.BgColor;
                w.FgColor = wc.FgColor;

                w.IsIncludeWorkTime = wc.IsIncludeWorkTime;
                w.IsSafetyTemporaryLabor = wc.IsSafetyTemporaryLabor;

                ret.Add(w);
            }



            return ret;
        }

        public static List<Work> GetWorks(MsUser loginUser, string appName, int vesselId, DateTime date)
        {
            return GetWorks(loginUser, appName, vesselId, date, DateTime.MinValue);
        }


        public static List<Work> GetWorks(MsUser loginUser, string appName, int vesselId, DateTime date1, DateTime date2)
        {
            System.Diagnostics.Debug.WriteLine($"  [GetWorks]:GetWorks:開始：{DateTime.Now.ToString("HH:mm:ss")}");
            System.Diagnostics.Debug.WriteLine($"  [GetWorks]:WtmModelGetter.GetWorks:開始：{DateTime.Now.ToString("HH:mm:ss")}");

            List<Work> list = null;
            if (date2 == DateTime.MinValue)
                list = WtmModelGetter.GetWorks(appName, date1);
            else
                list = WtmModelGetter.GetWorks(appName, vesselId, date1, date2);

            System.Diagnostics.Debug.WriteLine($"  [GetWorks]:WtmModelGetter.GetWorks:終了：{DateTime.Now.ToString("HH:mm:ss")}");

            List<Work> ret = new List<Work>();

            foreach (Work wk in list)
            {
                if (wk.IsDelete)
                    continue;

                if (vesselId != 0 && wk.VesselID != vesselId.ToString())
                    continue;


                Work w = new Work();

                w.SiteID = wk.SiteID;
                w.WorkID = wk.WorkID;

                w.CrewNo = wk.CrewNo;
                w.VesselID = wk.VesselID;
                w.StartWork = wk.StartWork;
                w.FinishWork = wk.FinishWork;


                w.WorkContentDetails = new List<WorkContentDetail>();
                foreach (WorkContentDetail wcd in wk.WorkContentDetails)
                {
                    WorkContentDetail wd = new WorkContentDetail();

                    wd.WorkContentID = wcd.WorkContentID;
                    wd.WorkDate = wcd.WorkDate;
                    wd.NightTime = wcd.NightTime;

                    w.WorkContentDetails.Add(wd);

                }


                w.Deviations = new List<Deviation>();
                foreach (Deviation d in wk.Deviations)
                {
                    Deviation dev = new Deviation();

                    dev.Kind = d.Kind;
                    dev.WorkDate = d.WorkDate;

                    w.Deviations.Add(dev);

                }


                ret.Add(w);
            }

            System.Diagnostics.Debug.WriteLine($"  [GetWorks]:GetWorks:終了：{DateTime.Now.ToString("HH:mm:ss")}");

            return ret;

            //List<Work> ret = new List<Work>();
            //return ret;
        }
    }
}
