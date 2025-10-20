using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_発注アラーム処理_見積回答アラーム停止(NBaseData.DAC.MsUser loginUser,string odMkID);
    }
    public partial class Service
    {
        public bool BLC_発注アラーム処理_見積回答アラーム停止(NBaseData.DAC.MsUser loginUser,string odMkID)
        {
            return NBaseData.BLC.発注アラーム処理.見積回答アラーム停止(null, loginUser, odMkID);
        }
    }
 }