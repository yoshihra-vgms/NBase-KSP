using System;
using System.Linq;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;
using System.IO;
using NBaseData.BLC;
using NBaseData.DAC;
using NBaseData.DS;
using System.Collections.Generic;


namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_Excel未提出確認一覧_取得(MsUser loginUser, int businessYear);
    }


    public partial class Service
    {
        public byte[] BLC_Excel未提出確認一覧_取得(MsUser loginUser, int businessYear)
        {
            NBaseData.BLC.Doc.未提出確認一覧出力 report = new NBaseData.BLC.Doc.未提出確認一覧出力();
            return report.未提出確認一覧取得(loginUser, businessYear);
        }
    }
}
