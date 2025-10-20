using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.ServiceModel;

using NBaseData.DAC;


namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        OdGetsujiShime OdGetsujiShime_GetRecordByLastDate(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<OdGetsujiShime> OdGetsujiShime_GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id);
    
    }


    public partial class Service
    {
        /// <summary>
        /// 締めの最終月を取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public OdGetsujiShime OdGetsujiShime_GetRecordByLastDate(NBaseData.DAC.MsUser loginUser)
        {       
            OdGetsujiShime ret = OdGetsujiShime.GetRecordByLastDate(loginUser);
            return ret;
        }


        public List<OdGetsujiShime> OdGetsujiShime_GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id)
        {
            return OdGetsujiShime.GetRecordsByMsUserID(loginUser, ms_user_id);
        }
    }
}
