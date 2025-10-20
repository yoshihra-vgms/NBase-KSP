
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseCommon.Senin.Excel;
using NBaseUtil;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        List<SiKoushu> BLC_講習管理_検索(MsUser loginUser, SiKoushuFilter filter);

        [OperationContract]
        byte[] BLC_Excel_講習一覧出力(MsUser loginUser, List<SiKoushu> koushuList);


        // 2014.02 2013年度改造
        [OperationContract]
        byte[] BLC_Excel_未受講者一覧出力(MsUser loginUser, SiKoushuFilter filter, List<SiKoushu> koushuList);
    }

    public partial class Service
    {
        public List<SiKoushu> BLC_講習管理_検索(MsUser loginUser, SiKoushuFilter filter)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();
            return NBaseData.BLC.講習管理.BLC_講習管理_検索(loginUser, seninTableCache, filter);
        }

        public byte[] BLC_Excel_講習一覧出力(MsUser loginUser, List<SiKoushu> koushuList)
        {
            string baseFileName = "講習管理一覧表";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            new 講習管理一覧表出力(templateFilePath, outputFilePath).CreateFile(loginUser, koushuList);

            return FileUtils.ToBytes(outputFilePath);
        }

        // 2014.02 2013年度改造
        public byte[] BLC_Excel_未受講者一覧出力(MsUser loginUser, SiKoushuFilter filter, List<SiKoushu> koushuList)
        {
            string baseFileName = "受講状況一覧";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();
            new 未受講者一覧出力(templateFilePath, outputFilePath).CreateFile(loginUser, seninTableCache, filter, koushuList);

            return FileUtils.ToBytes(outputFilePath);
        }
    }
}
