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
        MsKichi MsKichi_GetRecordByKichiNo(NBaseData.DAC.MsUser loginUser, string kichiNo);
        
        [OperationContract]
        List<MsKichi> MsKichi_GetRecordsByKichiNoKichiName(NBaseData.DAC.MsUser loginUser, string kichiNo, string kichiName);

        [OperationContract]
        List<MsKichi> MsKichi_GetRecords(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        bool MsKichi_InsertRecord(NBaseData.DAC.MsUser loginUser, MsKichi msKichi);

        [OperationContract]
        bool MsKichi_UpdateRecord(NBaseData.DAC.MsUser loginUser, MsKichi msKichi);
    }

    public partial class Service
    {
        public MsKichi MsKichi_GetRecordByKichiNo(NBaseData.DAC.MsUser loginUser, string kichiNo)
        {
            return MsKichi.GetRecordByKichiNo(loginUser, kichiNo);
        }

        public List<MsKichi> MsKichi_GetRecordsByKichiNoKichiName(NBaseData.DAC.MsUser loginUser, string kichiNo, string kichiName)
        {
            return MsKichi.GetRecordsByKichiNoKichiName(loginUser, kichiNo, kichiName);
        }

        public List<MsKichi> MsKichi_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return MsKichi.GetRecords(loginUser);
        }

        public bool MsKichi_InsertRecord(NBaseData.DAC.MsUser loginUser, MsKichi msKichi)
        {
            return msKichi.InsertRecord(loginUser);
        }

        public bool MsKichi_UpdateRecord(NBaseData.DAC.MsUser loginUser, MsKichi msKichi)
        {
            return msKichi.UpdateRecord(loginUser);
        }
    }
}
