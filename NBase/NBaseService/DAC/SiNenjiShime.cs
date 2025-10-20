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
        SiNenjiShime SiNenjiShime_GetRecordByLastDate(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        List<SiNenjiShime> SiNenjiShime_GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id);
    }


    public partial class Service
    {
        /// <summary>
        /// 締めの最終年を取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public SiNenjiShime SiNenjiShime_GetRecordByLastDate(NBaseData.DAC.MsUser loginUser)
        {       
            SiNenjiShime ret = SiNenjiShime.GetRecordByLastDate(loginUser);
            return ret;
        }

        public List<SiNenjiShime> SiNenjiShime_GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id)
        {
            return SiNenjiShime.GetRecordsByMsUserID(loginUser, ms_user_id);
        }
    }
}
