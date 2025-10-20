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
        byte[] BLC_Excel確認状況一覧_取得(MsUser loginUser, List<状況確認一覧Row> 状況確認一覧Rows);
    }


    public partial class Service
    {
        public byte[] BLC_Excel確認状況一覧_取得(MsUser loginUser, List<状況確認一覧Row> 状況確認一覧Rows)
        {
            NBaseData.BLC.Doc.確認状況一覧出力 report = new NBaseData.BLC.Doc.確認状況一覧出力();
            return report.確認状況一覧取得(loginUser, 状況確認一覧Rows);
        }
    }
}
