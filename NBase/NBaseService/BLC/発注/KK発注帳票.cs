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
        byte[] BLC_KK発注帳票_取得(MsUser loginUser, 発注帳票Common.kubun発注帳票 kubun, string Id);
    }

    public partial class Service
    {
        public byte[] BLC_KK発注帳票_取得(MsUser loginUser, 発注帳票Common.kubun発注帳票 kubun, string Id)
        {
            return _BLC_KK発注帳票_取得(loginUser, kubun, Id);
        }




        public byte[] _BLC_KK発注帳票_取得(MsUser loginUser, 発注帳票Common.kubun発注帳票 kubun, string Id, bool pdf = false)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string baseFileName = null;
            string templateFilePath = null;
            string outputFilePath = null;
            string outputPdfFilePath = null;



            OdThi odThi = null;

            if (kubun == 発注帳票Common.kubun発注帳票.注文書 || kubun == 発注帳票Common.kubun発注帳票.見積依頼書)
            {
                odThi = OdThi.GetRecordByOdMkID(loginUser, Id);

            }
            else
            {
                odThi = OdThi.GetRecord(loginUser, Id);
            }


            if (odThi.MsThiIraiSbtID == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.修繕))
            {
                if (kubun == 発注帳票Common.kubun発注帳票.見積依頼書)
                {
                    baseFileName = "KK修繕見積依頼書";
                }
                else
                {
                    if (odThi.MsThiIraiShousaiID == MsThiIraiShousai.ToId(MsThiIraiShousai.ThiIraiShousaiEnum.入渠))
                    {
                        baseFileName = "KK修繕申込書";
                    }
                    else
                    {
                        baseFileName = "KK修繕注文書";
                    }
                    if (kubun == 発注帳票Common.kubun発注帳票.査定表)
                    {
                        baseFileName += "(査定)";
                    }
                }
                //templateFilePath = path + "Template_" + baseFileName + ".xlsx";
                //outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName;
                //if (pdf)
                //{
                //    outputFilePath += ".pdf";
                //}
                //else
                //{
                //    outputFilePath += ".xlsx";
                //}

                //new KK修繕注文書出力(templateFilePath, outputFilePath).CreateFile(loginUser, kubun, Id);
            }

            if (odThi.MsThiIraiSbtID == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品))
            {
                if (kubun == 発注帳票Common.kubun発注帳票.注文書)
                {
                    baseFileName = "KK船用品注文書";
                }
                else if (kubun == 発注帳票Common.kubun発注帳票.見積依頼書)
                {
                    baseFileName = "KK船用品見積依頼書";
                }
                else
                {
                    baseFileName = "KK船用品請求書";
                }
                //templateFilePath = path + "Template_" + baseFileName + ".xlsx";
                //outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName;
                //if (pdf)
                //{
                //    outputFilePath += ".pdf";
                //}
                //else
                //{
                //    outputFilePath += ".xlsx";
                //}

                //new KK船用品請求書出力(templateFilePath, outputFilePath).CreateFile(loginUser, kubun, Id);
            }


            templateFilePath = path + "Template_" + baseFileName + ".xlsx";
            outputFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".xlsx";
            if (pdf)
            {
                outputPdfFilePath = path + "outPut_[" + loginUser.FullName + "]_" + baseFileName + ".pdf";
            }


            if (odThi.MsThiIraiSbtID == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.修繕))
            {
                new KK修繕注文書出力(templateFilePath, outputFilePath, outputPdfFilePath).CreateFile(loginUser, kubun, Id);
            }
            else if (odThi.MsThiIraiSbtID == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品))
            {
                new KK船用品請求書出力(templateFilePath, outputFilePath, outputPdfFilePath).CreateFile(loginUser, kubun, Id);
            }


            if (outputPdfFilePath != null && File.Exists(outputPdfFilePath))
                return FileUtils.ToBytes(outputPdfFilePath);
            else if (File.Exists(outputFilePath))
                return FileUtils.ToBytes(outputFilePath);
            else
                return FileUtils.ToBytes(templateFilePath);
        }
    }
}
