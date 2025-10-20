using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_添付ファイル作成(NBaseData.DAC.MsUser logiuser, string odMkId);
    }
    public partial class Service
    {
        public bool BLC_添付ファイル作成(NBaseData.DAC.MsUser logiuser, string odMkId)
        {
            return NBaseData.BLC.添付ファイル.作成(logiuser, odMkId);
        }
    }
}
