using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;
using System.IO;
using NBaseData.DAC;
using NBaseData.DS;
using System.Collections.Generic;
using System.Text;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_Excel動静表_取得(NBaseData.DAC.MsUser loginUser, DateTime today);
    }

    public partial class Service
    {
        public byte[] BLC_Excel動静表_取得(NBaseData.DAC.MsUser loginUser, DateTime today)
        {
            string BaseFileName = "動静表";

            #region 元になるファイルの確認と出力ファイル生成
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string templateName = path + "Template_" + BaseFileName + ".xlsx";
            string outPutFileName = path + "outPut_[" + loginUser.FullName + "]_" + BaseFileName + ".xlsx";

            bool exists = System.IO.File.Exists(templateName);
            if (exists == false)
            {
                return null;
            }
            #endregion

            NBaseData.BLC.簡易動静表.動静表_取得(loginUser, today, path, templateName, outPutFileName);

            byte[] bytBuffer;
            #region バイナリーへ変換
            using (FileStream objFileStream = new FileStream(outPutFileName, FileMode.Open))
            {
                long lngFileSize = objFileStream.Length;

                bytBuffer = new byte[(int)lngFileSize];
                objFileStream.Read(bytBuffer, 0, (int)lngFileSize);
                objFileStream.Close();
            }
            #endregion

            return bytBuffer;
        }

    }
}
