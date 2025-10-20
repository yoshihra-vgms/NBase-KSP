using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ORMapping;

namespace NBaseData.BLC
{
    public class 勤怠管理
    { 

        public static bool BLC_勤怠管理登録(MsUser loginUser, SiLaborManagementRecordBook recordBook, List<SiRequiredNumberOfDays> requireds, List<SiNightSetting> settings)
        {
            bool ret = true;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    ret = SiLaborManagementRecordBook_InsertOrUpdate(dbConnect, loginUser, recordBook);
                    if (ret)
                    {
                        foreach (SiRequiredNumberOfDays obj in requireds)
                        {
                            ret = SiRequiredNumberOfDays_InsertOrUpdate(dbConnect, loginUser, obj);
                            if (ret == false)
                                break;
                        }
                    }
                    if (ret)
                    {
                        foreach (SiNightSetting obj in settings)
                        {
                            ret = SiNightSetting_InsertOrUpdate(dbConnect, loginUser, obj);
                            if (ret == false)
                                break;
                        }
                    }

                    dbConnect.Commit();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                }
            }
            return ret;
        }

        private static bool SiLaborManagementRecordBook_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiLaborManagementRecordBook info)
        {
            bool ret = true;

            info.RenewUserID = loginUser.MsUserID;
            info.RenewDate = DateTime.Now;

            if (info.IsNew())
            {
                info.SiLaborManagementRecordBookID = System.Guid.NewGuid().ToString();
                ret = info.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                ret = info.UpdateRecord(dbConnect, loginUser);
            }

            return ret;
        }


        private static bool SiRequiredNumberOfDays_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiRequiredNumberOfDays info)
        {
            bool ret = true;

            info.RenewUserID = loginUser.MsUserID;
            info.RenewDate = DateTime.Now;

            if (info.IsNew())
            {
                info.SiRequiredNumberOfDaysID = System.Guid.NewGuid().ToString();
                ret = info.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                ret = info.UpdateRecord(dbConnect, loginUser);
            }

            return ret;
        }


        private static bool SiNightSetting_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, SiNightSetting info)
        {
            bool ret = true;

            info.RenewUserID = loginUser.MsUserID;
            info.RenewDate = DateTime.Now;

            if (info.IsNew())
            {
                info.SiNightSettingID = System.Guid.NewGuid().ToString();
                ret = info.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                ret = info.UpdateRecord(dbConnect, loginUser);
            }

            return ret;
        }

    }
}
