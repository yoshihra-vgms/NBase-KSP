using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using NBaseData.DAC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_動静帳票_動静報告一覧_取得(NBaseData.DAC.MsUser loginUser, DateTime date, List<DjDouseiHoukoku> houkokuList);
    }
    public partial class Service
    {
        public byte[] BLC_動静帳票_動静報告一覧_取得(NBaseData.DAC.MsUser logiuser, DateTime date, List<DjDouseiHoukoku> houkokuList)
        {
            NBaseData.BLC.動静帳票.動静報告一覧 report = new NBaseData.BLC.動静帳票.動静報告一覧();
            return report.動静報告一覧取得(logiuser, date, houkokuList);
        }
    }
}
