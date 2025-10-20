
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseCommon.Senin.Excel;
using NBaseUtil;
using ORMapping;

namespace NBaseService
{
    public partial interface IService
    {
        // 2014.02 2013年度改造
        [OperationContract]
        List<SiMenjou> BLC_免許免状管理_検索(MsUser loginUser, SiMenjouFilter filter);

        // 2014.02 2013年度改造
        [OperationContract]
        byte[] BLC_Excel_免許免状一覧出力(MsUser loginUser, SiMenjouFilter filter, List<SiMenjou> menjouList);

        // 2014.02 2013年度改造
        [OperationContract]
        SiMenjou BLC_免許免状管理_更新(MsUser loginUser, SiMenjou menjou);
    }

    public partial class Service
    {
        // 2014.02 2013年度改造
        public List<SiMenjou> BLC_免許免状管理_検索(MsUser loginUser, SiMenjouFilter filter)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();
            return NBaseData.BLC.免許免状管理.BLC_免許免状管理_検索(loginUser, seninTableCache, filter);
        }

        // 2014.02 2013年度改造
        public byte[] BLC_Excel_免許免状一覧出力(MsUser loginUser, SiMenjouFilter filter, List<SiMenjou> menjouList)
        {
            string baseFileName = "免許・免状一覧";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();
            new 免許免状一覧出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, filter, menjouList);

            return FileUtils.ToBytes(outputFilePath);
        }

        // 2014.02 2013年度改造
        public SiMenjou BLC_免許免状管理_更新(MsUser loginUser, SiMenjou menjou)
        {
            bool ret = true;
            SiMenjou retMenjou = menjou;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    menjou.RenewUserID = loginUser.MsUserID;
                    menjou.RenewDate = DateTime.Now;

                    if (menjou.IsNew())
                    {
                        menjou.SiMenjouID = System.Guid.NewGuid().ToString();
                        ret = menjou.InsertRecord(loginUser);
                    }
                    else
                    {
                        ret = menjou.UpdateRecord(loginUser);
                    }
                    if (ret)
                    {
                        ret = PtAlarmInfo_InsertOrUpdate(dbConnect, loginUser, menjou.AlarmInfoList[0], menjou.SiMenjouID, 0);
                    }
                    if (ret && menjou.DeleteFlag == 0)
                    {
                        retMenjou = SiMenjou.GetRecord(dbConnect, loginUser, menjou.SiMenjouID);
                    }

                    dbConnect.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                }
                return retMenjou;
            }
        }
        private static bool PtAlarmInfo_InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, PtAlarmInfo alarm, string sanshoumotoId, int deleteFlag)
        {
            if (deleteFlag == 1)
            {
                alarm.DeleteFlag = 1;
            }
            alarm.RenewUserID = loginUser.MsUserID;
            alarm.RenewDate = DateTime.Now;

            if (alarm.IsNew())
            {
                alarm.PtAlarmInfoId = System.Guid.NewGuid().ToString();
                alarm.SanshoumotoId = sanshoumotoId;

                if (!alarm.InsertRecord(dbConnect, loginUser))
                {
                    return false;
                }
            }
            else
            {
                if (!alarm.UpdateRecord(dbConnect, loginUser))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
