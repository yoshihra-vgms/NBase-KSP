using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;
using NBaseData.DS;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<SiKoushu> SiKoushu_GetRecordsByFilter(MsUser loginUser, SiKoushuFilter filter);
        
        [OperationContract]
        List<SiKoushu> SiKoushu_GetRecordsBashoOnly(MsUser loginUser);

        [OperationContract]
        SiKoushu SiKoushu_InsertOrUpdate(MsUser loginUser, SiKoushu s);
    }

    public partial class Service
    {
        public List<SiKoushu> SiKoushu_GetRecordsByFilter(MsUser loginUser, SiKoushuFilter filter)
        {
            return SiKoushu.GetRecordsByFilter(loginUser, filter);
        }

        public List<SiKoushu> SiKoushu_GetRecordsBashoOnly(MsUser loginUser)
        {
            return SiKoushu.GetRecordsBashoOnly(loginUser);
        }

        public SiKoushu SiKoushu_InsertOrUpdate(MsUser loginUser, SiKoushu s)
        {
            bool ret = true;
            s.RenewUserID = loginUser.MsUserID;
            s.RenewDate = DateTime.Now;

            if (s.IsNew())
            {
                s.SiKoushuID = System.Guid.NewGuid().ToString();
                ret = s.InsertRecord(loginUser);
            }
            else
            {
                ret = s.UpdateRecord(loginUser);
            }
            if (ret)
            {
                if (s.DeleteFlag == 1)
                {
                    return s;
                }
                else
                {
                    SiKoushuFilter filter = new SiKoushuFilter();
                    filter.SiKoushuID = s.SiKoushuID;
                    List<SiKoushu> retKoushu = SiKoushu.GetRecordsByFilter(loginUser, filter);

                    if (retKoushu.Count > 0)
                    {
                        return retKoushu[0];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }
    }
}
