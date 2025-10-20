using System;
using System.Linq;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;
using System.IO;
using NBaseData.DAC;
using NBaseData.BLC.動静帳票;
using NBaseData.DS;
using System.Collections.Generic;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_動静帳票_改正省エネ法エネルギー報告書_取得(NBaseData.DAC.MsUser loginUser, DateTime DateMonth);
    }
    public partial class Service
    {
        public byte[] BLC_動静帳票_改正省エネ法エネルギー報告書_取得(NBaseData.DAC.MsUser loginUser, DateTime DateMonth)
        {
            List<MsVessel> msVessels = MsVessel.GetRecords(loginUser);
            List<NBaseData.BLC.動静帳票.改正省エネ法エネルギー報告書.データ> datas = null;
            //using (WingCoreServer.Service1Client serviceClient = new WingCoreServer.Service1Client())
            //{
            //    datas = serviceClient.BLC_基幹システム連携読み込み処理_エネルギー報告書(loginUser, msVessels, DateMonth);
            //}

            NBaseData.BLC.動静帳票.改正省エネ法エネルギー報告書 report = new 改正省エネ法エネルギー報告書();
            return report.改正省エネ法エネルギー報告書取得(loginUser, DateMonth, datas);
        }
    }
}
