using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;

namespace WcfServiceDeficiencyControl
{
    /// <summary>
    /// Web.config管理クラス
    /// </summary>
    public class WebConfigManager
    {
        /// <summary>
        /// DB接続文字列取得
        /// </summary>
        public static string DBConnectString
        {
            get
            {
                return ConfigurationManager.AppSettings["DBConnectString"];
            }
        }

        /// <summary>
        /// ShipsWingDB接続先
        /// </summary>
        public static string ShipsWingDBConnectString
        {
            get
            {
                return ConfigurationManager.AppSettings["ShipsWingDBConnectString"];
            }
        }

        /// <summary>
        /// ログファイルパス
        /// </summary>
        public static string LogFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["LogFilePath"];
            }
        }
        
        /// <summary>
        /// ログ出力可否
        /// </summary>
        public static bool LogEnable
        {
            get
            {
                string s = ConfigurationManager.AppSettings["LogEnable"];
                int n = Convert.ToInt32(s);
                if (n == 0)
                {
                    return false;
                }

                return true;

            }
        }

        /// <summary>
        /// PSC帳票テンプレートファイルのパス
        /// </summary>
        public static string PSCTemplateFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["PSCTemplateFilePath"];
            }
        }



        /// <summary>
        /// 事故トラブルエクセルテンプレートファイルのパス
        /// </summary>
        public static string AccidentTemplateFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["AccidentTemplateFilePath"];
            }
        }


        /// <summary>
        /// 検船帳票 章別指摘数テンプレート
        /// </summary>
        public static string MoiExcelCateroryTemplateFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["MoiExcelCateroryTemplateFilePath"];
            }
        }

        /// <summary>
        /// 検船帳票 是正対応リストテンプレート
        /// </summary>
        public static string MoiExcelListTemplateFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["MoiExcelListTemplateFilePath"];
            }
        }


        /// <summary>
        /// 検船報告書 検船コメントリストテンプレート
        /// </summary>
        public static string MoiReportCommentListTemplateFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["MoiReportCommentListTemplateFilePath"];
            }
        }

        /// <summary>
        /// 検船報告書 改善報告書テンプレート
        /// </summary>
        public static string MoiReportObservationTemplateFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["MoiReportObservationTemplateFilePath"];
            }
        }



        /// <summary>
        /// 船種別 IGT船のマスタID
        /// </summary>
        public static string VesselKindID_IGT
        {
            get
            {
                return ConfigurationManager.AppSettings["VesselKindID_IGT"];
            }
        }

        /// <summary>
        /// 船種別 外航船のマスタID
        /// </summary>
        public static string VesselKindID_OGS
        {
            get
            {
                return ConfigurationManager.AppSettings["VesselKindID_OGS"];
            }
        }


        /// <summary>
        /// 船種別 傭船のマスタID
        /// </summary>
        public static string VesselKindID_Chartering
        {
            get
            {
                return ConfigurationManager.AppSettings["VesselKindID_Chartering"];
            }
        }

        /// <summary>
        /// スケジュール一覧テンプレート取得
        /// </summary>
        public static string ScheduleListTemplateFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["ScheduleListTemplateFilePath"];
            }
        }
    }
}