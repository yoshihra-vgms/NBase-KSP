using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace NBaseData.DAC.DJ
{
    [DataContract()]
    public class 内航船舶輸送実績調査票DetailData
    {
        [DataMember]
        public string LastPortNo = null;

        [DataMember]
        public TKJNAIPLAN Tumi = null;

        [DataMember]
        public TKJNAIPLAN Age = null;

        [DataMember]
        public TKJNAIPLAN_AMT_BILL Tumini = null;

        [DataMember]
        public double Qtty = 0;
    }

    [DataContract()]
    public class 内航船舶輸送実績調査票Data
    {
        #region データメンバ
        [DataMember]
        public TKJSHIP TKJSHIP = null;

        //[DataMember]
        //public List<TKJNAIPLAN> LdDsSetNos = null;

        //[DataMember]
        //public List<TKJNAIPLAN> LineDatas = null;


        [DataMember]
        public List<内航船舶輸送実績調査票DetailData> Details = null;



        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public 内航船舶輸送実績調査票Data()
        {
        }


        public static List<内航船舶輸送実績調査票Data> GetRecords(MsUser loginUser, DateTime date, List<MsVessel> targetVessels, List<string> targetBashoNos, List<string> gaichiBashoNos)
        {
            List<内航船舶輸送実績調査票Data> ret = new List<内航船舶輸送実績調査票Data>();


            List<TKJSHIP> Ships = TKJSHIP.GetRecords(loginUser);
            //foreach (TKJSHIP ship in Ships)
            foreach (MsVessel vessel in targetVessels)
            {
                //if (内航船舶輸送実績調査票船(targetVessels, ship.FuneNo) == false)
                //    continue;
                TKJSHIP ship = 内航船舶輸送実績調査票船(Ships, vessel);
                if (ship == null)
                    continue;

                ship.VesselName = vessel.VesselName;
                ship.DWT = vessel.DWT;
                ship.CargoWeight = vessel.CargoWeight;
                ship.OfficialNumber = vessel.OfficialNumber;
                ship.GRT = vessel.GRT;

                内航船舶輸送実績調査票Data data = new 内航船舶輸送実績調査票Data();

                data.TKJSHIP = ship;
                List<TKJNAIPLAN> LdDsSetNos = TKJNAIPLAN.GetRecordsByVesselVoyageNo(loginUser, ship.FuneNo, date.ToString("yyMM"));

                string 前月港NO = "";
                #region 前月最後の港を取得
                {
                    string 前月VoyageNo = date.AddMonths(-1).ToString("yyMM");
                    TKJNAIPLAN 前月港TKJNAIPLAN = TKJNAIPLAN.GetRecordBy前月港(loginUser, ship.FuneNo, 前月VoyageNo, targetBashoNos);
                    if (前月港TKJNAIPLAN != null)
                    {
                        //List<TKJNAIPLAN> 前月LineDatas = TKJNAIPLAN.GetRecordsByLdDsSetNo(loginUser, ship.FuneNo, 前月港TKJNAIPLAN.LdDsSetNo);
                        List<TKJNAIPLAN> tmp = TKJNAIPLAN.GetRecordsByLdDsSetNo(loginUser, ship.FuneNo, 前月港TKJNAIPLAN.LdDsSetNo);
                        List<TKJNAIPLAN> 前月LineDatas = tmp.Where(obj => targetBashoNos.Contains(obj.PortNo)).ToList();

                        TKJNAIPLAN Age1 = null;

                        #region 揚げ1を取得
                        var AgeDatas = from l in 前月LineDatas
                                       where l.SgKbn == "2"

                                       orderby l.ScYmd, l.KomaNo
                                       select l;
                        if (AgeDatas.Count<TKJNAIPLAN>() > 0)
                        {
                            Age1 = AgeDatas.First<TKJNAIPLAN>();
                        }
                        #endregion
                        if (Age1 != null)
                        {
                            前月港NO = Age1.PortNo;
                        }
                    }
                }
                #endregion

                //data.LineDatas = new List<TKJNAIPLAN>();
                data.Details = new List<内航船舶輸送実績調査票DetailData>();

                //foreach (TKJNAIPLAN LdDsSetNo in data.LdDsSetNos)
                foreach (TKJNAIPLAN LdDsSetNo in LdDsSetNos)
                {
                    //List<TKJNAIPLAN> LineDatas = TKJNAIPLAN.GetRecordsByLdDsSetNo(loginUser, LdDsSetNo.FuneNo, LdDsSetNo.LdDsSetNo);
                    List<TKJNAIPLAN> tmp = TKJNAIPLAN.GetRecordsByLdDsSetNo(loginUser, LdDsSetNo.FuneNo, LdDsSetNo.LdDsSetNo);
                    List<TKJNAIPLAN> LineDatas = tmp.Where(obj => targetBashoNos.Contains(obj.PortNo)).ToList();

                    if (Is外港(LineDatas, gaichiBashoNos) == true)
                        continue;

                    内航船舶輸送実績調査票DetailData detail = new 内航船舶輸送実績調査票DetailData();

                    detail.LastPortNo = 前月港NO;

                    #region 積み1を取得
                    var TsumiDatas = from l in LineDatas
                                        where l.SgKbn == "1"

                                        orderby l.ScYmd, l.KomaNo
                                        select l;
                    if (TsumiDatas.Count<TKJNAIPLAN>() > 0)
                    {
                        detail.Tumi = TsumiDatas.First<TKJNAIPLAN>();
                    }
                    #endregion

                    #region 揚げ1を取得
                    var AgeDatas = from l in LineDatas
                                    where l.SgKbn == "2"

                                    orderby l.ScYmd, l.KomaNo
                                    select l;
                    if (AgeDatas.Count<TKJNAIPLAN>() > 0)
                    {
                        detail.Age = AgeDatas.First<TKJNAIPLAN>();
                    }
                    #endregion

                    #region 貨物
                    List<TKJNAIPLAN_AMT_BILL> TKJNAIPLAN_AMT_BILLs = TKJNAIPLAN_AMT_BILL.GetRecords(loginUser, detail.Tumi);
                    if (TKJNAIPLAN_AMT_BILLs.Count > 0)
                    {
                        detail.Tumini = TKJNAIPLAN_AMT_BILLs.First();

                        detail.Qtty = 貨物数量(loginUser, LineDatas);
                    }
                    #endregion

                    data.Details.Add(detail);

                    // 次のデータの準備
                    前月港NO = detail.Age.PortNo;

                }

              　ret.Add(data);
            }



            return ret;
        }

        //private static bool 内航船舶輸送実績調査票船(List<MsVessel> targetVessels, string FuneNo)
        //{
        //    var ret = from v in targetVessels
        //              where v.VesselNo == FuneNo
        //              select v;

        //    if (ret.Count<MsVessel>() != 0)
        //    {
        //        MsVessel msVessel = ret.First<MsVessel>();
        //        if (msVessel.DouseiReport1 == 1)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}
        private static TKJSHIP 内航船舶輸送実績調査票船(List<TKJSHIP> ships, MsVessel vessel)
        {
            if (vessel.DouseiReport1 != 1)
                return null;

            var ret = from s in ships
                      where s.FuneNo == vessel.VesselNo
                      select s;

            if (ret.Count<TKJSHIP>() != 0)
            {
                return ret.First<TKJSHIP>();
            }

            return null;
        }
        //private static bool Is外港(List<TKJNAIPLAN> LineDatas)
        //{
        //    var ret = from p in LineDatas
        //              where p.GaichiFlag == 1
        //              select p;
        //    if (ret.Count<TKJNAIPLAN>() > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        private static bool Is外港(List<TKJNAIPLAN> LineDatas, List<string> gaichiBashoNos)
        {
            var ret = LineDatas.Where(obj => gaichiBashoNos.Contains(obj.PortNo)).ToList();
            if (ret.Count() > 0)
            {
                return true;
            }
            return false;
        }
        private static double 貨物数量(MsUser loginUser, List<TKJNAIPLAN> datas)
        {
            double Qtty = 0;
            foreach (TKJNAIPLAN data in datas)
            {
                if (data.SgKbn == "1")
                {
                    List<TKJNAIPLAN_AMT_BILL> details = TKJNAIPLAN_AMT_BILL.GetRecords(loginUser, data);

                    foreach (TKJNAIPLAN_AMT_BILL detail in details)
                    {
                        Qtty += detail.Qtty;
                    }
                }
            }
            return Qtty;
        }
    }
}
