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
        SiGetsujiShime SiGetsujiShime_GetRecordByLastDate(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<SiGetsujiShime> SiGetsujiShime_GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id);
    }


    public partial class Service
    {
        /// <summary>
        /// 締めの最終月を取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public SiGetsujiShime SiGetsujiShime_GetRecordByLastDate(NBaseData.DAC.MsUser loginUser)
        {       
            SiGetsujiShime ret = SiGetsujiShime.GetRecordByLastDate(loginUser);
            return ret;
        
        }


        public List<SiGetsujiShime> SiGetsujiShime_GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id)
        {
            return SiGetsujiShime.GetRecordsByMsUserID(loginUser, ms_user_id);
        }

    }
}
