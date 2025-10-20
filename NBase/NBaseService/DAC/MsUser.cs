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
using System.Collections.Generic;
using System.ServiceModel;
using NBaseData.DAC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.DAC.MsUser> MsUser_GetRecords(MsUser loginUser);
        
        [OperationContract]
        List<NBaseData.DAC.MsUser> MsUser_GetRecordsByUserKbn事務所(MsUser loginUser);
        
        [OperationContract]
        List<NBaseData.DAC.MsUser> MsUser_GetRecordsByUserKbn船員(MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.MsUser MsUser_GetRecordsByUserID(MsUser loginUser,string msUserID);

        [OperationContract]
        NBaseData.DAC.MsUser MsUser_GetRecordsByLoginIDPassword(string loginID, string password);

        [OperationContract]
        NBaseData.DAC.MsUser MsUser_GetRecordsByLoginID(MsUser loginUser,string loginID);

        [OperationContract]
        List<NBaseData.DAC.MsUser> MsUser_SearchRecords(MsUser loginUser, string loginID, int kbnID, int adminFlag, string bumonID, string sei, string mei);

        [OperationContract]
        List<NBaseData.DAC.MsUser> MsUser_SearchRecords2(MsUser loginUser, string seiKana, string meiKana);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsUser> MsUser_GetRecords(MsUser loginUser)
        {
            //NBaseData.BLC.LoginSession.セッションキー認証(loginUser);

            List<NBaseData.DAC.MsUser> ret;
            ret = NBaseData.DAC.MsUser.GetRecords(loginUser);
            return ret;
        }
        public List<NBaseData.DAC.MsUser> MsUser_GetRecordsByUserKbn事務所(MsUser loginUser)
        {
            List<NBaseData.DAC.MsUser> ret;
            ret = NBaseData.DAC.MsUser.GetRecordsByUserKbn事務所(loginUser);
            return ret;
        }
        public List<NBaseData.DAC.MsUser> MsUser_GetRecordsByUserKbn船員(MsUser loginUser)
        {
            List<NBaseData.DAC.MsUser> ret;
            ret = NBaseData.DAC.MsUser.GetRecordsByUserKbn船員(loginUser);
            return ret;
        }

        public NBaseData.DAC.MsUser MsUser_GetRecordsByUserID(MsUser loginUser,string msUserID)
        {
            NBaseData.DAC.MsUser ret;
            ret = NBaseData.DAC.MsUser.GetRecordsByUserID(loginUser,msUserID);
            return ret;
        }

        public NBaseData.DAC.MsUser MsUser_GetRecordsByLoginIDPassword(string loginID, string password)
        {
            NBaseData.DAC.MsUser ret;
            ret = NBaseData.DAC.MsUser.GetRecordsByLoginIDPassword(loginID, password);
            return ret;
        }

        public NBaseData.DAC.MsUser MsUser_GetRecordsByLoginID(MsUser loginUser,string loginID)
        {
            NBaseData.DAC.MsUser ret;
            ret = NBaseData.DAC.MsUser.GetRecordsByLoginID(loginUser,loginID);
            return ret;
        }

        public List<NBaseData.DAC.MsUser> MsUser_SearchRecords(NBaseData.DAC.MsUser loginUser, string loginID, int kbnID, int adminFlag, string bumonID, string sei, string mei)
        {
            List<NBaseData.DAC.MsUser> ret;
            ret = NBaseData.DAC.MsUser.SearchRecords(loginUser, loginID, kbnID, adminFlag, bumonID, sei, mei);
            return ret;
        }
        public List<NBaseData.DAC.MsUser> MsUser_SearchRecords2(NBaseData.DAC.MsUser loginUser, string seiKana, string meiKana)
        {
            List<NBaseData.DAC.MsUser> ret;
            ret = NBaseData.DAC.MsUser.SearchRecords(loginUser, seiKana, meiKana);
            return ret;
        }
    }
}
