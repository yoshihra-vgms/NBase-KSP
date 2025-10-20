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
        byte[] BLC_動静帳票_内航海運輸送実績調査票_取得(NBaseData.DAC.MsUser loginUser, DateTime DateMonth);
    }
    public partial class Service
    {
        public byte[] BLC_動静帳票_内航海運輸送実績調査票_取得(NBaseData.DAC.MsUser loginUser, DateTime DateMonth)
        {
            //NBaseData.BLC.動静帳票.内航海運輸送実績調査票 report = new 内航海運輸送実績調査票();
            //return report.内航船舶輸送実績調査票取得(loginUser, DateMonth);


            string jiko = DateMonth.ToString("yyMM");
            decimal decJiko = decimal.Parse(jiko);

            // 対象となる船は、WING船マスタのDouseiReport2 = 1の船のみ
            List<MsVessel> msVessels = MsVessel.GetRecords(loginUser);
            List<MsVessel> targetVessels = msVessels.Where(obj => obj.DouseiReport2 == 1).ToList();

            // 対象となる輸送品目
            List<MsCargoYusoItem> msCargoYusoItems = MsCargoYusoItem.GetRecords(loginUser);
            List<MsCargoYusoItem> targetCargoYusoItems = msCargoYusoItems.Where(obj => (decimal.Parse(obj.StartDay.ToString("yyMM")) <= decJiko && (obj.EndDay == DateTime.MinValue || decimal.Parse(obj.EndDay.ToString("yyMM")) >= decJiko))).ToList();
            
            // 対象となる港
            List<MsBasho> msBashos = MsBasho.GetRecords(loginUser);
            List<string> targetBashoNos = msBashos.Where(obj => obj.GaichiFlag == 0).Select(obj => obj.MsBashoNo).ToList();

            List<MsCargo> msCargos = MsCargo.GetRecords(loginUser);

            List<NBaseData.DAC.DJ.内航海運輸送実績調査票Data> datas = null;
            //using (WingCoreServer.Service1Client serviceClient = new WingCoreServer.Service1Client())
            //{
            //    datas = serviceClient.BLC_基幹システム連携読み込み処理_内航海運輸送実績調査票データ(loginUser, jiko, targetVessels, targetCargoYusoItems, msCargos, targetBashoNos);
            //}

            NBaseData.BLC.動静帳票.内航海運輸送実績調査票 report = new 内航海運輸送実績調査票();
            return report.内航船舶輸送実績調査票取得(loginUser, DateMonth, datas);
        }
    }
}
