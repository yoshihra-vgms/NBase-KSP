using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.DAC.Batch;
using ORMapping;
using NBaseData.DS;
using NBaseUtil;

namespace NBaseData.BLC
{
    public class 動静処理
    {
        List<PtKanidouseiInfo> PtKanidouseiInfos = new List<PtKanidouseiInfo>();
        public List<LogInfo> logInfos = new List<LogInfo>();


        public bool 動静予定登録(MsUser loginUser, List<DjDousei> Douseis)
        {
            // 次航海番号を決定する
            // 動静情報に既に次航海番号がある場合、その番号を使用
            // 無い場合には、同じ船内で最大の番号を取得し使用する
            string voyageNo = null;
            foreach (DjDousei dousei in Douseis)
            {
                if (dousei.VoyageNo != null && dousei.VoyageNo.Length > 0)
                {
                    voyageNo = dousei.VoyageNo;
                    break;
                }
            }
            if (voyageNo == null)
            {
                int vesselId = Douseis[0].MsVesselID;
                voyageNo = DjDousei.GetNextVoyageNo(loginUser, vesselId);
            }

            // 動静情報を登録する
            bool ret = true;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                foreach (DjDousei dousei in Douseis)
                {
                    foreach (DjDouseiCargo cargo in dousei.DjDouseiCargos)
                    {
                        if (cargo.DjDouseiCargoID == null || cargo.DjDouseiCargoID == "")
                        {
                            cargo.DjDouseiCargoID = System.Guid.NewGuid().ToString();
                        }
                        cargo.RenewDate = DateTime.Now;
                        cargo.RenewUserID = loginUser.MsUserID;
                    }

                    dousei.RenewDate = DateTime.Now;
                    dousei.RenewUserID = loginUser.MsUserID;
                    dousei.VesselID = dousei.MsVesselID;
                    if (dousei.DjDouseiID == null || dousei.DjDouseiID == "")
                    {
                        dousei.DjDouseiID = System.Guid.NewGuid().ToString();
                        dousei.VoyageNo = voyageNo;

                        dousei.InsertRecord(dbConnect, loginUser);          
                    }
                    else
                    {
                        if(dousei.DeleteFlag == 1)
                        {
                            var delDousei = DjDousei.GetRecord(dbConnect, loginUser, dousei.DjDouseiID);

                            delDousei.DeleteFlag = 1;
                            delDousei.RenewDate = dousei.RenewDate;
                            delDousei.RenewUserID = dousei.RenewUserID;
                            delDousei.UpdateDetailRecords(dbConnect, loginUser);
                        }
                       else
                        {
                            if (dousei.VoyageNo == null || dousei.VoyageNo.Length == 0)
                            {
                                dousei.VoyageNo = voyageNo;
                            }
                            dousei.UpdateDetailRecords(dbConnect, loginUser);
                        }

                    }

                    // 簡易動静情報にセットする
                    PtKanidouseiInfo kanidousei = SetKandiDousei(dbConnect, loginUser, dousei);
                    PtKanidouseiInfos.Add(kanidousei);
                }

                foreach (PtKanidouseiInfo k in PtKanidouseiInfos)
                {
                    if (k.PtKanidouseiInfoId == "")
                    {
                        k.PtKanidouseiInfoId = Guid.NewGuid().ToString();
                        k.InsertRecord(loginUser);
                    }
                    else
                    {
                        k.HonsenCheckDate = DateTime.MinValue; // 予定更新時、Honsen確認をクリアする
                        k.UpdateRecord(loginUser);
                    }
                }


                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }
            }
            return ret;
        }

        public bool 動静予定削除(MsUser loginUser, PtKanidouseiInfo kanidousei)
        {
            DjDousei dousei = DjDousei.GetRecord(loginUser, kanidousei.DjDouseiID);
            if (dousei == null)
            {
                return true;
            }

            // 動静情報を削除する
            bool ret1 = true;
            bool ret2 = true;
            bool ret3 = true;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                int msVesselId = dousei.MsVesselID;
                string voyageNo = dousei.VoyageNo;

                ret1 = PtKanidouseiInfo.DeleteByVoyageNo(dbConnect, loginUser, msVesselId, voyageNo);
                ret2 = DjDousei.DeleteByVoyageNo(dbConnect, loginUser, msVesselId, voyageNo);
                ret3 = DjDouseiCargo.DeleteByVoyageNo(dbConnect, loginUser, msVesselId, voyageNo);
                if (ret1 && ret2)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }
            }
            return (ret1 && ret2);
        }

        public bool 動静実績登録(MsUser loginUser, List<DjDousei> Douseis)
        {
            // 次航海番号を決定する
            // 動静情報に既に次航海番号がある場合、その番号を使用
            // 無い場合には、同じ船内で最大の番号を取得し使用する
            string voyageNo = null;
            foreach (DjDousei dousei in Douseis)
            {
                if (dousei.VoyageNo != null && dousei.VoyageNo.Length > 0)
                {
                    voyageNo = dousei.VoyageNo;
                    break;
                }
            }
            if (voyageNo == null)
            {
                int vesselId = Douseis[0].MsVesselID;
                voyageNo = DjDousei.GetNextVoyageNo(loginUser, vesselId);
            }

            // 動静情報を登録する
            bool ret = true;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                foreach (DjDousei dousei in Douseis)
                {
                    foreach (DjDouseiCargo cargo in dousei.DjDouseiCargos)
                    {
                        if (cargo.DjDouseiCargoID == null || cargo.DjDouseiCargoID == "")
                        {
                            cargo.DjDouseiCargoID = System.Guid.NewGuid().ToString();
                        }
                        cargo.RenewDate = DateTime.Now;
                        cargo.RenewUserID = loginUser.MsUserID;
                    }
                    foreach (DjDouseiCargo cargo in dousei.ResultDjDouseiCargos)
                    {
                        if (cargo.DjDouseiCargoID == null || cargo.DjDouseiCargoID == "")
                        {
                            cargo.DjDouseiCargoID = System.Guid.NewGuid().ToString();
                        }
                        cargo.RenewDate = DateTime.Now;
                        cargo.RenewUserID = loginUser.MsUserID;
                    }

                    dousei.RenewDate = DateTime.Now;
                    dousei.RenewUserID = loginUser.MsUserID;
                    if (dousei.DjDouseiID == null || dousei.DjDouseiID == "")
                    {
                        dousei.DjDouseiID = System.Guid.NewGuid().ToString();
                        dousei.VoyageNo = voyageNo;
                        dousei.VesselID = dousei.MsVesselID;

                        dousei.InsertRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        if (dousei.DeleteFlag == 1)
                        {
                            var delDousei = DjDousei.GetRecord(dbConnect, loginUser, dousei.DjDouseiID);

                            delDousei.DeleteFlag = 1;
                            delDousei.RenewDate = dousei.RenewDate;
                            delDousei.RenewUserID = dousei.RenewUserID;
                            delDousei.UpdateDetailRecords(dbConnect, loginUser);
                        }
                        else
                        {
                            dousei.UpdateDetailRecords(dbConnect, loginUser);
                        }
                    }

                    // 簡易動静情報にセットする
                    PtKanidouseiInfo kanidousei = SetKandiDousei(dbConnect, loginUser, dousei);
                    PtKanidouseiInfos.Add(kanidousei);
                }

                foreach (PtKanidouseiInfo k in PtKanidouseiInfos)
                {
                    if (k.PtKanidouseiInfoId == "")
                    {
                        k.PtKanidouseiInfoId = Guid.NewGuid().ToString();
                        k.InsertRecord(loginUser);
                    }
                    else
                    {
                        k.UpdateRecord(loginUser);
                    }
                }


                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }
            }
            return ret;
        }
        




        public bool Honsen動静実績登録(MsUser loginUser, List<DjDousei> Douseis)
        {
            // 次航海番号を決定する
            // 動静情報に既に次航海番号がある場合、その番号を使用
            // 無い場合には、同じ船内で最大の番号を取得し使用する
            string voyageNo = null;
            foreach (DjDousei dousei in Douseis)
            {
                if (dousei.VoyageNo != null && dousei.VoyageNo.Length > 0)
                {
                    voyageNo = dousei.VoyageNo;
                    break;
                }
            }
            if (voyageNo == null)
            {
                int vesselId = Douseis[0].MsVesselID;
                voyageNo = DjDousei.GetNextVoyageNo(loginUser, vesselId);
            }

            // 動静情報を登録する
            bool ret = true;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                foreach (DjDousei dousei in Douseis)
                {
                    if (dousei.DjDouseiID == null || dousei.DjDouseiID == "")
                    {
                        dousei.DjDouseiID = System.Guid.NewGuid().ToString();
                        dousei.VoyageNo = voyageNo;
                        dousei.VesselID = dousei.MsVesselID;

                    }
                    dousei.RenewDate = DateTime.Now;
                    dousei.RenewUserID = loginUser.MsUserID;

                    foreach (DjDouseiCargo cargo in dousei.ResultDjDouseiCargos)
                    {
                        if (cargo.DjDouseiCargoID == null || cargo.DjDouseiCargoID == "")
                        {
                            cargo.DjDouseiCargoID = System.Guid.NewGuid().ToString();
                        }
                        cargo.DjDouseiID = dousei.DjDouseiID;
                        cargo.VesselID = dousei.VesselID;
                        cargo.RenewDate = DateTime.Now;
                        cargo.RenewUserID = loginUser.MsUserID;
                        SyncTableSaver.InsertOrUpdate(cargo, loginUser, StatusUtils.通信状況.未同期, dbConnect);
                    }
                    dousei.DjDouseiCargos = null;
                    dousei.ResultDjDouseiCargos = null;
                    SyncTableSaver.InsertOrUpdate(dousei, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                    // 簡易動静情報にセットする
                    PtKanidouseiInfo kanidousei = SetKandiDousei(dbConnect, loginUser, dousei);
                    PtKanidouseiInfos.Add(kanidousei);
                }

                foreach (PtKanidouseiInfo k in PtKanidouseiInfos)
                {
                    //k.SendFlag = (int)StatusUtils.通信状況.未同期;
                    //k.UserKey = StringUtils.CreateHash(SyncTableSaver.CreateHashSource(k));
                    if (k.PtKanidouseiInfoId == "")
                    {
                        k.PtKanidouseiInfoId = Guid.NewGuid().ToString();
                    }
                    k.HonsenCheckDate = DateTime.Now;
                    SyncTableSaver.InsertOrUpdate(k, loginUser, StatusUtils.通信状況.未同期, dbConnect);
                }


                if (ret)
                {
                    try
                    {
                        dbConnect.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error:" + e.Message);
                    }
                }
                else
                {
                    dbConnect.RollBack();
                }
            }
            return ret;
        }

        public bool Honsen動静実績削除(MsUser loginUser, PtKanidouseiInfo kanidousei)
        {
            DjDousei dousei = DjDousei.GetRecord(loginUser, kanidousei.DjDouseiID);
            if (dousei == null)
            {
                return true;
            }

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                if (dousei.PlanNiyakuStart != DateTime.MinValue)
                {
                    // 予定情報がある場合、実績情報のみクリアする

                    // 簡易動静
                    kanidousei.EventDate = dousei.PlanNiyakuStart;
                    kanidousei.MsBashoId = dousei.MsBashoID;
                    kanidousei.MsKitiId = dousei.MsKichiID;
                    if (dousei.DjDouseiCargos != null && dousei.DjDouseiCargos.Count > 0)
                    {
                        kanidousei.MsCargoID = dousei.DjDouseiCargos[0].MsCargoID;
                        kanidousei.Qtty = dousei.DjDouseiCargos[0].Qtty;
                    }
                    kanidousei.RenewDate = DateTime.Now;
                    kanidousei.RenewUserID = loginUser.MsUserID;
                    SyncTableSaver.InsertOrUpdate(kanidousei, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                    // 動静
                    dousei.ResultNyuko = DateTime.MinValue;
                    dousei.ResultChakusan = DateTime.MinValue;
                    dousei.ResultNiyakuStart = DateTime.MinValue;
                    dousei.ResultNiyakuEnd = DateTime.MinValue;
                    dousei.ResultRisan = DateTime.MinValue;
                    dousei.ResultShukou = DateTime.MinValue;
                    dousei.RenewDate = DateTime.Now;
                    dousei.RenewUserID = loginUser.MsUserID;
                    SyncTableSaver.InsertOrUpdate(dousei, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                    // 積荷情報
                    List<DjDouseiCargo> douseiCargos = DjDouseiCargo.GetRecords(dbConnect, loginUser, dousei);
                    foreach (DjDouseiCargo dc in douseiCargos)
                    {
                        if (dc.PlanResultFlag == (int)DjDouseiCargo.PLAN_RESULT_FLAG.RESULT)
                        {
                            dc.DeleteFlag = 1;
                            dc.RenewDate = DateTime.Now;
                            dc.RenewUserID = loginUser.MsUserID;
                        }
                        SyncTableSaver.InsertOrUpdate(dc, loginUser, StatusUtils.通信状況.未同期, dbConnect);
                    }
                }
                else
                {
                    // 予定情報が無い場合、動静情報を削除する

                    // 簡易動静
                    kanidousei.DeleteFlag = 1;
                    kanidousei.RenewDate = DateTime.Now;
                    kanidousei.RenewUserID = loginUser.MsUserID;
                    SyncTableSaver.InsertOrUpdate(kanidousei, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                    // 動静
                    dousei.DeleteFlag = 1;
                    dousei.RenewDate = DateTime.Now;
                    dousei.RenewUserID = loginUser.MsUserID;
                    SyncTableSaver.InsertOrUpdate(dousei, loginUser, StatusUtils.通信状況.未同期, dbConnect);

                    // 積荷情報
                    List<DjDouseiCargo> douseiCargos = DjDouseiCargo.GetRecords(dbConnect, loginUser, dousei);
                    foreach (DjDouseiCargo dc in douseiCargos)
                    {
                        dc.DeleteFlag = 1;
                        dc.RenewDate = DateTime.Now;
                        dc.RenewUserID = loginUser.MsUserID;

                        SyncTableSaver.InsertOrUpdate(dc, loginUser, StatusUtils.通信状況.未同期, dbConnect);
                    }
                }
                dbConnect.Commit();
            }
            return true;
        }





        private PtKanidouseiInfo SetKandiDousei(DBConnect dbConnect, MsUser loginUser, DjDousei dousei)
        {
            PtKanidouseiInfo ki = PtKanidouseiInfo.GetRecordByDjDouseiID(dbConnect, loginUser, dousei.DjDouseiID);
            if (ki == null)
            {
                ki = new PtKanidouseiInfo();
                ki.PtKanidouseiInfoId = "";
                ki.DjDouseiID = dousei.DjDouseiID;
            }

            if (dousei.DeleteFlag == 0)
            {
                ki.MsVesselID = dousei.MsVesselID;
                if (dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.積みID
                    || dousei.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.揚げID )
                {
                    if (dousei.ResultNiyakuEnd != DateTime.MinValue)
                    {
                        // 2013.07.22 : 実績日は荷役終了日であるとコメントを受けたので改造
                        // 2013.03.23 : 実績日は着棧日であるとコメントを受けたので改造
                        // 実績入港日時がある場合、実績として扱う
                        // 簡易動静の日付は、実績入港日時
                        //ki.EventDate = dousei.ResultNyuko;
                        //ki.EventDate = dousei.ResultChakusan;
                        ki.EventDate = dousei.ResultNiyakuEnd;
                        ki.MsBashoId = dousei.ResultMsBashoID;
                        ki.MsKitiId = dousei.ResultMsKichiID;

                        if (dousei.ResultDjDouseiCargos != null && dousei.ResultDjDouseiCargos.Count > 0)
                        {
                            ki.MsCargoID = dousei.ResultDjDouseiCargos[0].MsCargoID;
                            ki.Qtty = dousei.ResultDjDouseiCargos[0].Qtty;
                        }
                    }
                    else
                    {
                        // 実績入港日時がない場合、予定として扱う
                        // 簡易動静の日付は、予定荷役開始日時
                        ki.EventDate = dousei.PlanNiyakuStart;
                        ki.MsBashoId = dousei.MsBashoID;
                        ki.MsKitiId = dousei.MsKichiID;

                        if (dousei.DjDouseiCargos != null && dousei.DjDouseiCargos.Count > 0)
                        {
                            ki.MsCargoID = dousei.DjDouseiCargos[0].MsCargoID;
                            ki.Qtty = dousei.DjDouseiCargos[0].Qtty;
                        }
                    }
                }
                else
                {
                    // 待機/入渠/パージ
                    ki.EventDate = dousei.DouseiDate;
                    ki.MsBashoId = dousei.MsBashoID;
                    ki.MsKitiId = dousei.MsKichiID;
                }
            }

            ki.UserKey = "0";
            ki.VesselID = 0;
            ki.RenewDate = DateTime.Now;
            ki.RenewUserID = loginUser.MsUserID;
            ki.DeleteFlag = dousei.DeleteFlag;

            ki.MsKanidouseiInfoShubetuId = dousei.MsKanidouseiInfoShubetuID;
            ki.Koma = 0; // コマNoは使用しないので、とりあえず０を入れておく

            return ki;
        }

    }
}
