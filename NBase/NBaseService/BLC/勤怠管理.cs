using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using NBaseData.DAC;
using NBaseData.BLC;
using NBaseData.DS;
using NBaseUtil;
using NBaseCommon.Senin.Excel;
using WtmData;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_勤怠管理_InsertOrUpdate(MsUser loginUser, SiLaborManagementRecordBook recordBook, List<SiRequiredNumberOfDays> requireds, List<SiNightSetting> settings);

        [OperationContract]
        byte[] BLC_Excel_労務管理記録簿出力(string connectionKey, MsUser loginUser, int vesselId, int msSeninID, DateTime fromDate, DateTime toDate);
    }

    public partial class Service
    {
        public bool BLC_勤怠管理_InsertOrUpdate(MsUser loginUser, SiLaborManagementRecordBook recordBook, List<SiRequiredNumberOfDays> requireds, List<SiNightSetting> settings)
        {
            return 勤怠管理.BLC_勤怠管理登録(loginUser, recordBook, requireds, settings);
        }

        public byte[] BLC_Excel_労務管理記録簿出力(string connectionKey, MsUser loginUser, int vesselId, int msSeninID, DateTime fromDate, DateTime toDate)
        {
            //TEK帳票
            string baseFileName = "労務管理記録簿";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            WtmAccessor wtmAccessor = WtmAccessor.Instance(false);
            wtmAccessor.DacProxy = new LocalAccessor(connectionKey);
            new 労務管理記録簿出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, wtmAccessor, vesselId, msSeninID, fromDate, toDate);

            return FileUtils.ToBytes(outputFilePath);
        }

    }
}