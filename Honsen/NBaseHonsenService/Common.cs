using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace NBaseHonsenService
{
    public class Common
    {
        private static string CommonVesselId;
        private static string SchemaVersion;
        private static string DBCS;
        
        public static string 共通船番号
        {
            get
            {
                return CommonVesselId != null ? 
                CommonVesselId : ConfigurationSettings.AppSettings["CommonVesselId"];
            }
            set
            {
                CommonVesselId = value;
            }
        }

        public static string スキーマバージョン
        {
            get
            {
                return SchemaVersion != null ?
                SchemaVersion : ConfigurationSettings.AppSettings["SchemaVersion"];
            }
            set
            {
                SchemaVersion = value;
            }
        }

        public static string 接続文字列
        {
            get
            {
                return DBCS != null ?
                DBCS : ORMapping.Common.DBCS;
            }
            set
            {
                DBCS = value;
            }
        }

        private static string ID;
        private static string ConnectionKey;
        private static string WtmNamespaceKey;

        public static string KEY
        {
            get
            {
                return ConnectionKey != null ?
                ConnectionKey : ConfigurationSettings.AppSettings["ConnectionKey"];
            }
            set
            {
                ConnectionKey = value;
            }
        }

        public static string 識別ID
        {
            get
            {
                return ID != null ?
                ID : ConfigurationSettings.AppSettings["SiteID"];
            }
            set
            {
                ID = value;
            }
        }
        public static string WTM_NAMESPACE_KEY
        {
            get
            {
                return WtmNamespaceKey != null ?
                WtmNamespaceKey : ConfigurationSettings.AppSettings["WtmNameSpaceKey"];
            }
            set
            {
                WtmNamespaceKey = value;
            }
        }

    }
}
