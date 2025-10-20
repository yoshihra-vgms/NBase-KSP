using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<MsUserSettings> MsUserSettings_GetRecords(MsUser loginUser);

        [OperationContract]
        bool MsUserSettings_InsertOrUpdateRecords(MsUser loginUser, string key, string value);
    }



    public partial class Service
    {
        public List<MsUserSettings> MsUserSettings_GetRecords(MsUser loginUser)
        {
            return MsUserSettings.GetRecords(loginUser, loginUser.MsUserID);
        }


        public bool MsUserSettings_InsertOrUpdateRecords(MsUser loginUser, string key, string value)
        {
            MsUserSettings settings = null;

            var all = MsUserSettings.GetRecords(loginUser, loginUser.MsUserID);

            if (all.Any(o => o.Key == key))
            {
                settings = all.Where(o => o.Key == key).First();

                settings.Value = value;
                settings.RenewDate = DateTime.Now;
                settings.RenewUserID = loginUser.MsUserID;

                return settings.UpdateRecord(null, loginUser);

            }
            else
            {
                settings = new MsUserSettings();

                settings.MsUserID = loginUser.MsUserID;
                settings.Key = key;
                settings.Value = value;
                settings.RenewDate = DateTime.Now;
                settings.RenewUserID = loginUser.MsUserID;

                return settings.InsertRecord(null, loginUser);
            }
        }
    }
}
