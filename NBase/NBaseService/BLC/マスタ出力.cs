using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
using NBaseCommon.Master.Excel;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_Excel_個人予定一覧表出力(MsUser loginUser, List<SiPersonalSchedule> personalScheduleList);

        [OperationContract]
        byte[] BLC_Excel_乗り合わせ一覧表出力(MsUser loginUser, List<SiFellowPassengers> fellowPassengersList);

        [OperationContract]
        byte[] BLC_Excel_顧客管理一覧表出力(MsUser loginUser, List<MsCustomer> customerList);

        [OperationContract]
        byte[] BLC_Excel_報告書管理一覧表出力(MsUser loginUser, List<MsDmHoukokusho> houkokushoList);
    }

    public partial class Service
    {
        public byte[] BLC_Excel_個人予定一覧表出力(MsUser loginUser, List<SiPersonalSchedule> personalScheduleList)
        {
            string baseFileName = "個人予定一覧表";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 個人予定一覧出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, personalScheduleList);

            return FileUtils.ToBytes(outputFilePath);
        }

        public byte[] BLC_Excel_乗り合わせ一覧表出力(MsUser loginUser, List<SiFellowPassengers> fellowPassengersList)
        {
            string baseFileName = "乗り合わせ一覧表";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new 乗り合わせ一覧出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, fellowPassengersList);

            return FileUtils.ToBytes(outputFilePath);
        }

        public byte[] BLC_Excel_顧客管理一覧表出力(MsUser loginUser, List<MsCustomer> customerList)
        {
            string baseFileName = "顧客管理一覧表";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            new 顧客管理一覧出力(templateFilePath, outputFilePath).CreateFile(loginUser, customerList);

            return FileUtils.ToBytes(outputFilePath);
        }



        public byte[] BLC_Excel_報告書管理一覧表出力(MsUser loginUser, List<MsDmHoukokusho> houkokushoList)
        {
            string baseFileName = "報告書管理一覧表";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            new 報告書管理一覧出力(templateFilePath, outputFilePath).CreateFile(loginUser, houkokushoList);

            return FileUtils.ToBytes(outputFilePath);
        }
    }
}