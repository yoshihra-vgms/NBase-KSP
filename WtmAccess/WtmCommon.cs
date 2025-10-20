using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WtmModelBase;

namespace WtmData
{
    public class WtmCommon
    {
        public static List<WorkContent> WorkContentList { set; get; }

        public static List<RankCategory> RankCategoryList { set; get; }

        public static List<Role> RoleList { set; get; }




        public static string ConnectionKey { set; get; } = "";

        public static bool VesselMode { set; get; } = false;


        /// <summary>
        /// 日表示画面：1日当りの労働時間～休憩時間(長い) の列を表示するかどうか
        /// </summary>
        public static bool FlgSummaryTimes { set; get; } = false;


        public static bool FlgSummaryEdit { set; get; } = false;


        /// <summary>
        /// 
        /// </summary>
        public static bool FlgNightTime { set; get; } = false;

        /// <summary>
        /// 
        /// </summary>
        public static bool FlgVesselMovement { set; get; } = false;

        /// <summary>
        /// 
        /// </summary>
        public static bool FlgAnchorage { set; get; } = false;

        /// <summary>
        /// 
        /// </summary>
        public static bool FlgShowRankCategory { set; get; } = false;

        /// <summary>
        /// 
        /// </summary>
        public static bool FlgShowApproval { set; get; } = false;


        /// <summary>
        /// １日の作業時間
        /// </summary>
        public static double WorkingHours { set; get; } = 8.0;

        /// <summary>
        /// 分単位
        /// </summary>
        public static double WorkRange { set; get; } = 15;


        /// <summary>
        /// あらゆる２４時間の労働時間
        /// </summary>
        public static int Deviation { set; get; } = 14;

        /// <summary>
        /// あらゆる１週間の労働時間
        /// </summary>
        public static int Deviation1Week { set; get; } = 72;

        /// <summary>
        /// あらゆる４週間の時間外労働時間
        /// </summary>
        public static int Deviation4Week { set; get; } = 56;

        /// <summary>
        /// ２４時間の休息時間
        /// </summary>
        public static int DeviationRestTime { set; get; } = 10;

        /// <summary>
        /// ２４時間の休息時間
        /// </summary>
        public static int DeviationLongRestTime { set; get; } = 6;

        /// <summary>
        /// 休息時間
        /// </summary>
        public static int DeviationRestTimeCount { set; get; } = 2;



        public static void SetSetting(Setting setting)
        {
            if (setting != null)
            {
                FlgSummaryTimes = setting.FlgSummaryTimes;
                FlgSummaryEdit = setting.FlgSummaryEdit;
                FlgNightTime = setting.FlgNightTime;
                FlgVesselMovement = setting.FlgVesselMovement;
                FlgAnchorage = setting.FlgAnchorage;
                FlgShowRankCategory = setting.FlgShowRankCategory;
                FlgShowApproval = setting.FlgShowApproval;

                WorkingHours = setting.WorkingHours;
                WorkRange = setting.WorkRange;

                Deviation = (int)setting.Deviation;
                Deviation1Week = (int)setting.Deviation1Week;
                Deviation4Week = (int)setting.Deviation4Week;
                DeviationRestTime = (int)setting.DeviationRestTime;
                DeviationLongRestTime = (int)setting.DeviationLongRestTime;
                DeviationRestTimeCount = (int)setting.DeviationRestTimeCount;
            }
        }


        public static void ReadConfig()
        {
            ConnectionKey = GetString("ConnectionKey");

            VesselMode = GetBool("VesselMode");


            //FlgSummaryTimes = GetBool("FlgSummaryTimes");
            //FlgSummaryEdit = GetBool("FlgSummaryEdit");
            //FlgNightTime = GetBool("FlgNightTime");
            //FlgVesselMovement = GetBool("FlgVesselMovement");
            //FlgAnchorage = GetBool("FlgAnchorage");
            //FlgShowRankCategory = GetBool("FlgShowRankCategory");
            //FlgShowApproval = GetBool("FlgShowApproval");

            //WorkingHours = GetDouble("WorkingHours");
            //WorkRange = GetDouble("WorkRange");

            //Deviation = GetInt("Deviation");
            //Deviation1Week = GetInt("Deviation1Week");
            //Deviation4Week = GetInt("Deviation4Week");
            //DeviationRestTime = GetInt("DeviationRestTime");
            //DeviationLongRestTime = GetInt("DeviationLongRestTime");
            //DeviationRestTimeCount = GetInt("DeviationRestTimeCount");
        }

        public static bool GetBool(string key, bool def = false)
        {
            bool ans = true;

            try
            {
                //値を取得して変換
                string s = ConfigurationManager.AppSettings[key];
                ans = Convert.ToBoolean(s);
            }
            catch (Exception e)
            {
                return def;
            }

            return ans;
        }
        public static string GetString(string key, string def = "")
        {
            string ans = def;

            try
            {
                ans = ConfigurationManager.AppSettings[key];
                if (ans == null)
                {
                    return def;
                }

            }
            catch (Exception e)
            {
                return def;
            }

            return ans;
        }
        public static int GetInt(string key, int def = 0)
        {
            int ans = def;

            try
            {
                //値を取得して変換
                string s = ConfigurationManager.AppSettings[key];
                ans = Convert.ToInt32(s);
            }
            catch (Exception e)
            {
                return def;
            }
            return ans;
        }
        public static double GetDouble(string key, double def = 0)
        {
            double ans = def;

            try
            {
                //値を取得して変換
                string s = ConfigurationManager.AppSettings[key];
                ans = Convert.ToDouble(s);
            }
            catch (Exception e)
            {
                return def;
            }
            return ans;
        }
    }
}
