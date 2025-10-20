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
        OdAttachFile OdAttachFile_GetRecord(MsUser loginUser, string odAttachFileId);
        
        [OperationContract]
        OdAttachFile OdAttachFile_GetRecordNoData(MsUser loginUser, string odAttachFileId);
       
        [OperationContract]
        List<OdAttachFile> OdAttachFile_GetRecordsByOdThiID(MsUser loginUser, string odThiId);
        
        //[OperationContract]
        //List<OdAttachFile> OdAttachFile_GetRecordsByOdMmID(MsUser loginUser, string odMmId);
        
        //[OperationContract]
        //List<OdAttachFile> OdAttachFile_GetRecordsByOdMkID(MsUser loginUser, string odMkId);
    }

	public partial class Service
	{
        public OdAttachFile OdAttachFile_GetRecord(MsUser loginUser, string odAttachFileId)
		{
			return OdAttachFile.GetRecord(loginUser, odAttachFileId);
		}
        public OdAttachFile OdAttachFile_GetRecordNoData(MsUser loginUser, string odAttachFileId)
        {
            return OdAttachFile.GetRecordNoData(loginUser, odAttachFileId);
        }
        public List<OdAttachFile> OdAttachFile_GetRecordsByOdThiID(MsUser loginUser, string odThiId)
        {
            return OdAttachFile.GetRecordsByOdThiId(loginUser, odThiId);
        }
        //public List<OdAttachFile> OdAttachFile_GetRecordsByOdMmID(MsUser loginUser, string odMmId)
        //{
        //    return OdAttachFile.GetRecordsByOdMmId(loginUser, odMmId);
        //}
        //public List<OdAttachFile> OdAttachFile_GetRecordsByOdMkID(MsUser loginUser, string odMkId)
        //{
        //    return OdAttachFile.GetRecordsByOdMkId(loginUser, odMkId);
        //}
    }
}
