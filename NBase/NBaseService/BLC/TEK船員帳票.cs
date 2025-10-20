using NBaseCommon.Senin.Excel;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_Excel_手当一覧出力(MsUser loginUser, string from, string to, int vesselId, string allowanceName);

        [OperationContract]
        byte[] BLC_Excel_手当支給依頼書出力(MsUser loginUser, SiAllowance allowance, List<SiAllowanceDetail> details);

        [OperationContract]
        byte[] BLC_Excel_手当帳票出力(MsUser loginUser, DateTime ym);

    }



    public partial class Service
    {
        public byte[] BLC_Excel_手当一覧出力(MsUser loginUser, string from, string to, int vesselId, string allowanceName)
        {
            string baseFileName = "手当一覧";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new tek手当一覧出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, from, to, vesselId, allowanceName);

            return FileUtils.ToBytes(outputFilePath);
        }



        public byte[] BLC_Excel_手当支給依頼書出力(MsUser loginUser, SiAllowance allowance, List<SiAllowanceDetail> details)
        {
            string baseFileName = "手当支給依頼書";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new tek手当支給依頼書出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, allowance, details);

            return FileUtils.ToBytes(outputFilePath);
        }


        public byte[] BLC_Excel_手当帳票出力(MsUser loginUser, DateTime ym)
        {
            //TEK帳票
            string baseFileName = "手当帳票";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();

            new tek手当帳票出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, ym);

            return FileUtils.ToBytes(outputFilePath);
        }


    }

}