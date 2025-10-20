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
using System.ServiceModel;
using System.Collections.Generic;
using NBaseData.DAC;
using ORMapping;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<BgYosanHead> BgYosanHead_GetRecords(MsUser loginUser);

        [OperationContract]
        BgYosanHead BgYosanHead_GetRecord(MsUser loginUser, int yosanHeadId);

        [OperationContract]
        bool BgYosanHead_UpdateRecord(MsUser loginUser, BgYosanHead yosanHead);

        [OperationContract]
        BgYosanHead BgYosanHead_GetRecordByYear(MsUser loginUser, string year);

        [OperationContract]
        BgYosanHead BgYosanHead_GetRecord_直近(MsUser loginUser);
        //BgYosanHead BgYosanHead_GetRecord_直近(MsUser loginUser, int yosanSbtId);

        [OperationContract]
        List<BgYosanHead> BgYosanHead_GetRecordsByMsUserID(MsUser loginUser, string ms_user_id);
    }

    public partial class Service
    {
        public List<BgYosanHead> BgYosanHead_GetRecords(MsUser loginUser)
        {
            return BgYosanHead.GetRecords(loginUser);
        }

        public BgYosanHead BgYosanHead_GetRecord(MsUser loginUser, int yosanHeadId)
        {
            return BgYosanHead.GetRecord(loginUser, yosanHeadId);
        }

        public bool BgYosanHead_UpdateRecord(MsUser loginUser, BgYosanHead yosanHead)
        {
            return yosanHead.UpdateRecord(loginUser);
        }

        public BgYosanHead BgYosanHead_GetRecordByYear(MsUser loginUser, string year)
        {
            return BgYosanHead.GetRecordByYear(loginUser, year);
        }

        //public BgYosanHead BgYosanHead_GetRecord_直近(MsUser loginUser, int yosanSbtId)
        public BgYosanHead BgYosanHead_GetRecord_直近(MsUser loginUser)
        {
            //return BgYosanHead.GetRecord_直近(loginUser, yosanSbtId);
            return BgYosanHead.GetRecord_直近(loginUser);
        }

        public List<BgYosanHead> BgYosanHead_GetRecordsByMsUserID( MsUser loginUser, string ms_user_id)
        {
            return BgYosanHead.GetRecordsByMsUserID(null, loginUser, ms_user_id);
        }
    }
}
