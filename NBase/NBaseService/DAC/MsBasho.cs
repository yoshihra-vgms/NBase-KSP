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
        List<MsBasho> MsBasho_GetRecords(MsUser loginUser);

        [OperationContract]
        List<MsBasho> MsBasho_GetRecordsBy港(MsUser loginUser);

        [OperationContract]
        List<MsBasho> MsBasho_GetRecordLikeBashoName(MsUser loginUser, string bashoName);

        [OperationContract]
        List<MsBasho> MsBasho_GetRecordsByBashoNoBashoNameBashoKubun(NBaseData.DAC.MsUser loginUser, string bashoNo, string bashoName, string bashoKubunId);

        [OperationContract]
        bool MsBasho_InsertRecord(NBaseData.DAC.MsUser loginUser, MsBasho msBasho);

        [OperationContract]
        bool MsBasho_UpdateRecord(NBaseData.DAC.MsUser loginUser, MsBasho msBasho);

        [OperationContract]
        List<MsBasho> MsBasho_GetRecordsByBashoKubun(NBaseData.DAC.MsUser loginUser, string bashoKubunId);
    }

    public partial class Service
    {
        public List<MsBasho> MsBasho_GetRecords(MsUser loginUser)
        {
            return MsBasho.GetRecords(loginUser);
        }

        public List<MsBasho> MsBasho_GetRecordsBy港(MsUser loginUser)
        {
            return MsBasho.GetRecordsBy港(loginUser);
        }

        public List<MsBasho> MsBasho_GetRecordLikeBashoName(MsUser loginUser, string bashoName)
        {
            return MsBasho.GetRecordLikeBashoName(loginUser, bashoName);
        }

        public List<MsBasho> MsBasho_GetRecordsByBashoNoBashoNameBashoKubun(NBaseData.DAC.MsUser loginUser, string bashoNo, string bashoName, string bashoKubunId)
        {
            return MsBasho.GetRecordsByBashoNoBashoNameBashoKubun(loginUser, bashoNo, bashoName, bashoKubunId);
        }
        public bool MsBasho_InsertRecord(NBaseData.DAC.MsUser loginUser, MsBasho msBasho)
        {
            return msBasho.InsertRecord(loginUser);
        }
        public bool MsBasho_UpdateRecord(NBaseData.DAC.MsUser loginUser, MsBasho msBasho)
        {
            return msBasho.UpdateRecord(loginUser);
        }



        public List<MsBasho> MsBasho_GetRecordsByBashoKubun(NBaseData.DAC.MsUser loginUser, string bashoKubunId)
        {
            return MsBasho.GetRecordsByBashoKubun(loginUser, bashoKubunId);
        }

    }
}
