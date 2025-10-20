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
        List<MsAlarmDate> MsAlarmDate_GetRecords(MsUser loginUser);

        [OperationContract]
        MsAlarmDate MsAlarmDate_GetRecord(MsUser loginUser, MsAlarmDate.MsAlarmDateIDNo id);

        [OperationContract]
        bool MsAlarmDate_InsertRecord(MsUser loginUser, MsAlarmDate data);

        [OperationContract]
        bool MsAlarmDate_UpdateRecord(MsUser loginUser, MsAlarmDate data);
        
        [OperationContract]
        bool MsAlarmDate_UpdateRecords(MsUser loginUser, List<MsAlarmDate> datas);
    }

    public partial class Service
    {
        public List<MsAlarmDate> MsAlarmDate_GetRecords(MsUser loginUser)
        {
            return MsAlarmDate.GetRecords(loginUser);
        }

        public MsAlarmDate MsAlarmDate_GetRecord(MsUser loginUser, MsAlarmDate.MsAlarmDateIDNo id)
        {
            return MsAlarmDate.GetRecord(loginUser, id);
        }

        public bool MsAlarmDate_InsertRecord(MsUser loginUser, MsAlarmDate data)
        {
            return data.InsertRecord(loginUser);
        }

        public bool MsAlarmDate_UpdateRecord(MsUser loginUser, MsAlarmDate data)
        {
            return data.UpdateRecord(loginUser);
        }

        public bool MsAlarmDate_UpdateRecords(MsUser loginUser, List<MsAlarmDate> datas)
        {
            bool ret = false;
            using (DBConnect dbConnect = new DBConnect())
            {
                foreach (MsAlarmDate data in datas)
                {
                    ret = data.UpdateRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        break;
                    }
                }
            }
            return ret;
        }

    }
}
