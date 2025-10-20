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
using NBaseData.BLC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_Excel予算対比表_取得(MsUser loginUser, BgYosanHead yosanHead1, BgYosanHead yosanHead2, decimal unit);
    }

    public partial class Service
    {
        public byte[] BLC_Excel予算対比表_取得(MsUser loginUser, BgYosanHead yosanHead1, BgYosanHead yosanHead2, decimal unit)
        {     
            string outname = "outPut_[" + loginUser.FullName + "]_予算対比表.xlsx";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string filename = path + outname;

            YosanTaihihyoWriter writer = new YosanTaihihyoWriter(filename, loginUser);
            bool ret = writer.Write(yosanHead1, yosanHead2, unit);

            if (ret == false)
            {
                return null;
            }

            byte[] bytBuffer;
            #region バイナリーへ変換
            using (FileStream objFileStream = new FileStream(filename, FileMode.Open))
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
