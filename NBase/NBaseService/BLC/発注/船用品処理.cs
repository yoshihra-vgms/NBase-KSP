using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
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
using NBaseUtil;
using NBaseCommon.Senyouhin;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_Excel_船用品注文書_取得(NBaseData.DAC.MsUser loginUser, string odThiId);
    }
    public partial class Service
    {
        public　byte[] BLC_Excel_船用品注文書_取得(NBaseData.DAC.MsUser loginUser, string odThiId)
        {
            string baseFileName = "船用品注文書";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            string outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";

            new VesselItemWriter(templateFilePath, outputFilePath).CreateFile(loginUser, odThiId);

            return FileUtils.ToBytes(outputFilePath);
        }
    }
 }
