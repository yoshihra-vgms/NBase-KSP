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
        //[OperationContract]
        //byte[] BLC_Excel月次収支報告書_取得_全社(MsUser loginUser, BgYosanHead selectedYosanHead, decimal unit, int selectindex, bool is前年度実績出力);

        //[OperationContract]
        //byte[] BLC_Excel月次収支報告書_取得_グループ(MsUser loginUser, BgYosanHead selectedYosanHead, decimal unit, int selectindex, MsVesselType selectitem, bool is前年度実績出力);

        //[OperationContract]
        //byte[] BLC_Excel月次収支報告書_取得_船(MsUser loginUser, BgYosanHead selectedYosanHead, decimal unit, int selectindex, MsVessel selectitem, bool is前年度実績出力);
        
        
        [OperationContract]
        byte[] BLC_Excel月次収支報告書_取得(MsUser loginUser, BgYosanHead selectedYosanHead, decimal unit, string month, bool is累計);
    }

    public partial class Service
    {
        //public byte[] BLC_Excel月次収支報告書_取得_全社(MsUser loginUser, BgYosanHead selectedYosanHead, decimal unit, int selectindex, bool is前年度実績出力)
        //{
        //    return _BLC_Excel月次収支報告書_取得(loginUser, selectedYosanHead, unit, selectindex, null, is前年度実績出力);
        //}


        //public byte[] BLC_Excel月次収支報告書_取得_グループ(MsUser loginUser, BgYosanHead selectedYosanHead, decimal unit, int selectindex, MsVesselType selectItem, bool is前年度実績出力)
        //{
        //    return _BLC_Excel月次収支報告書_取得(loginUser, selectedYosanHead, unit, selectindex, selectItem, is前年度実績出力);
        //}


        //public byte[] BLC_Excel月次収支報告書_取得_船(MsUser loginUser, BgYosanHead selectedYosanHead, decimal unit, int selectindex, MsVessel selectItem, bool is前年度実績出力)
        //{
        //    return _BLC_Excel月次収支報告書_取得(loginUser, selectedYosanHead, unit, selectindex, selectItem, is前年度実績出力);
        //}


        //private byte[] _BLC_Excel月次収支報告書_取得(MsUser loginUser, BgYosanHead selectedYosanHead, decimal unit, int selectindex, object selectitem, bool is前年度実績出力)
        //{     
        //    string outname = "outPut_[" + loginUser.FullName + "]_月次収支.xlsx";
        //    string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
        //    string filename = path + outname;

        //    GetsujiShuushiWriter writer = new GetsujiShuushiWriter(filename, loginUser);
        //    bool ret = writer.Write(selectedYosanHead, unit, selectindex, selectitem, is前年度実績出力);

        //    if (ret == false)
        //    {
        //        return null;
        //    }

        //    byte[] bytBuffer;
        //    #region バイナリーへ変換
        //    using (FileStream objFileStream = new FileStream(filename, FileMode.Open))
        //    {
        //        long lngFileSize = objFileStream.Length;

        //        bytBuffer = new byte[(int)lngFileSize];
        //        objFileStream.Read(bytBuffer, 0, (int)lngFileSize);
        //        objFileStream.Close();
        //    }
        //    #endregion
            
        //    return bytBuffer;         
        //}

        public byte[] BLC_Excel月次収支報告書_取得(MsUser loginUser, BgYosanHead selectedYosanHead, decimal unit, string month, bool is累計)
        {
            string outname = "outPut_[" + loginUser.FullName + "]_月次収支.xlsx";
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string filename = path + outname;

            GetsujiShuushiWriter2 writer = new GetsujiShuushiWriter2(filename, loginUser);
            bool ret = writer.Write(selectedYosanHead, unit, month, is累計);

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
