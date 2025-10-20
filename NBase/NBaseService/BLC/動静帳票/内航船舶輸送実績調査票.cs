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
        byte[] BLC_動静帳票_内航船舶輸送実績調査票_取得(NBaseData.DAC.MsUser loginUser, DateTime DateMonth);
    }
    public partial class Service
    {
        public byte[] BLC_動静帳票_内航船舶輸送実績調査票_取得(NBaseData.DAC.MsUser loginUser, DateTime DateMonth)
        {
            //NBaseData.BLC.動静帳票.内航船舶輸送実績調査票 report = new 内航船舶輸送実績調査票();
            //return report.内航船舶輸送実績調査票取得(logiuser, DateMonth);

            // 対象となる船は、WING船マスタのDouseiReport1 = 1の船のみ
            List<MsVessel> msVessels = MsVessel.GetRecords(loginUser);
            List<MsVessel> targetVessels = msVessels.Where(obj => obj.DouseiReport1 == 1).ToList();

            // 対象となる港, 対象から外す港
            List<MsBasho> msBashos = MsBasho.GetRecords(loginUser);
            List<string> targetBashoNos = msBashos.Select(obj => obj.MsBashoNo).ToList();
            List<string> gaichiBashoNos = msBashos.Where(obj => obj.GaichiFlag == 1).Select(obj => obj.MsBashoNo).ToList();

            List<NBaseData.DAC.DJ.内航船舶輸送実績調査票Data> datas = null;        
            //using (WingCoreServer.Service1Client serviceClient = new WingCoreServer.Service1Client())
            //{
            //    datas = serviceClient.BLC_基幹システム連携読み込み処理_内航船舶輸送実績調査票データ(loginUser, DateMonth, targetVessels, targetBashoNos, gaichiBashoNos);
            //}

            NBaseData.BLC.動静帳票.内航船舶輸送実績調査票 report = new 内航船舶輸送実績調査票();
            return report.内航船舶輸送実績調査票取得(loginUser, DateMonth, datas);
        }
    }
}
