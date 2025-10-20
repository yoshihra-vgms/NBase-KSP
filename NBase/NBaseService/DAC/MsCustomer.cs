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

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<NBaseData.DAC.MsCustomer> MsCustomer_GetRecords(NBaseData.DAC.MsUser loginUser);
        
        [OperationContract]
        List<NBaseData.DAC.MsCustomer> MsCustomer_GetRecords削除を含む(NBaseData.DAC.MsUser loginUser);

        [OperationContract]
        NBaseData.DAC.MsCustomer MsCustomer_GetRecord(NBaseData.DAC.MsUser loginUser, string MsCustomerID);

        [OperationContract]
        List<NBaseData.DAC.MsCustomer> MsCustomer_SearchRecords(NBaseData.DAC.MsUser loginUser, string customerID, string customerName, bool isClient, bool isAgency, bool isConsignor, bool isComapny, bool isSchool, bool isAppointed, bool isInspection);
        //List<NBaseData.DAC.MsCustomer> MsCustomer_SearchRecords(NBaseData.DAC.MsUser loginUser, string customerID, string customerName, int shubetsuID);

        [OperationContract]
        bool MsCustomer_InsertRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsCustomer customer);

        [OperationContract]
        bool MsCustomer_UpdateRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsCustomer customer);

        [OperationContract]
        bool MsCustomer_DeleteFlagRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsCustomer customer);

        [OperationContract]
        NBaseData.DAC.MsCustomer MsCustomer_GetRecordByLoginId(NBaseData.DAC.MsUser loginUser, string LoginId);

        [OperationContract]
        List<NBaseData.DAC.MsCustomer> MsCustomer_GetRecords_代理店(NBaseData.DAC.MsUser loginUser);
    }

    public partial class Service
    {
        public List<NBaseData.DAC.MsCustomer> MsCustomer_GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsCustomer> ret;
            ret = NBaseData.DAC.MsCustomer.GetRecords(loginUser);
            return ret;
        }

        public List<NBaseData.DAC.MsCustomer> MsCustomer_GetRecords削除を含む(NBaseData.DAC.MsUser loginUser)
        {
            List<NBaseData.DAC.MsCustomer> ret;
            ret = NBaseData.DAC.MsCustomer.GetRecords削除を含む(loginUser);
            return ret;
        }

        public NBaseData.DAC.MsCustomer MsCustomer_GetRecord(NBaseData.DAC.MsUser loginUser, string MsCustomerID)
        {
            NBaseData.DAC.MsCustomer ret;
            ret = NBaseData.DAC.MsCustomer.GetRecord(loginUser, MsCustomerID);
            return ret;
        }

        public NBaseData.DAC.MsCustomer MsCustomer_GetRecordByLoginId(NBaseData.DAC.MsUser loginUser, string LoginId)
        {
            NBaseData.DAC.MsCustomer ret;
            ret = NBaseData.DAC.MsCustomer.GetRecordByLoginId(loginUser, LoginId);
            return ret;
        }

        //public List<NBaseData.DAC.MsCustomer> MsCustomer_SearchRecords(NBaseData.DAC.MsUser loginUser, string customerID, string customerName, int shubetsuID)
        //{
        //    List<NBaseData.DAC.MsCustomer> ret;
        //    ret = NBaseData.DAC.MsCustomer.SearchRecords(loginUser, customerID, customerName, shubetsuID);
        //    return ret;
        //}
        public List<NBaseData.DAC.MsCustomer> MsCustomer_SearchRecords(NBaseData.DAC.MsUser loginUser, string customerID, string customerName, bool isClient, bool isAgancy, bool isConsignor, bool isComapny, bool isSchool, bool isAppointed, bool isInspection)
        {
            List<NBaseData.DAC.MsCustomer> ret;
            ret = NBaseData.DAC.MsCustomer.SearchRecords(loginUser, customerID, customerName, isClient, isAgancy, isConsignor, isComapny, isSchool, isAppointed, isInspection);

            List<NBaseData.DAC.MsSchool> schools = NBaseData.DAC.MsSchool.GetRecords(loginUser);
            foreach (NBaseData.DAC.MsCustomer c in ret)
            {
                if (schools.Any(obj => obj.MsCustomerID == c.MsCustomerID))
                {
                    c.MsSchools.AddRange(schools.Where(obj => obj.MsCustomerID == c.MsCustomerID).ToList());
                }
            }

            return ret;
        }

        public bool MsCustomer_InsertRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsCustomer customer)
        {
            customer.InsertRecord(loginUser, customer);
            foreach (NBaseData.DAC.MsSchool school in customer.MsSchools)
            {
                if (school.DeleteFlag == 1)
                    continue;

                school.MsSchoolID = System.Guid.NewGuid().ToString();
                school.MsCustomerID = customer.MsCustomerID;
                school.RenewDate = customer.RenewDate;
                school.RenewUserID = customer.RenewUserID;

                school.InsertRecord(loginUser);
            }
            return true;
        }

        public bool MsCustomer_UpdateRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsCustomer customer)
        {
            customer.UpdateRecord(loginUser, customer);

            foreach(NBaseData.DAC.MsSchool school in customer.MsSchools)
            {
                school.RenewDate = customer.RenewDate;
                school.RenewUserID = customer.RenewUserID;

                if (school.IsNew())
                {
                    school.MsSchoolID = System.Guid.NewGuid().ToString();
                    school.MsCustomerID = customer.MsCustomerID;
                    school.InsertRecord(loginUser);
                }
                else
                {
                    school.MsCustomerID = customer.MsCustomerID;
                    school.UpdateRecord(loginUser);
                }
            }
            return true;
        }

        public bool MsCustomer_DeleteFlagRecord(NBaseData.DAC.MsUser loginUser, NBaseData.DAC.MsCustomer customer)
        {
            customer.DeleteFlagRecord(loginUser, customer);
            return true;
        }

        public List<NBaseData.DAC.MsCustomer> MsCustomer_GetRecords_代理店(NBaseData.DAC.MsUser loginUser)
        {
            return NBaseData.DAC.MsCustomer.GetRecords_代理店(loginUser);
        }
    }
}
