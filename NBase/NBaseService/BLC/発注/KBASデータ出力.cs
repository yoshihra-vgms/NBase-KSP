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
using ORMapping;
using System.ServiceModel;
using System.IO;
using NBaseData.DAC;
using NBaseData.DS;
using System.Collections.Generic;
using System.Text;
using NBaseUtil;
using NBaseCommon.Hachu.Excel;
using ExcelCreator = AdvanceSoftware.ExcelCreator;


namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_KBASデータ出力_取得(MsUser loginUser, DateTime date, MsVessel vessel);
    }

    public partial class Service
    { 

        public byte[] BLC_KBASデータ出力_取得(MsUser loginUser, DateTime date, MsVessel vessel)
        {
            string baseFileName = "K-BAS";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            new KBASデータ出力(templateFilePath, outputFilePath).CreateFile(loginUser, date, vessel);

            return FileUtils.ToBytes(outputFilePath);
        }
    }
}
