using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WingData.DAC;
using WingData.DAC.Batch;

namespace WingData.BLC
{
    public class 動静処理
    {
        List<PtKanidouseiInfo> PtKanidouseiInfos = new List<PtKanidouseiInfo>();
        public List<LogInfo> logInfos = new List<LogInfo>();

        public void 簡易動静連携(MsUser loginUser, DjDousei ParentDousei)
        {

            #region 積み１
            DjDousei tsumi1 = ParentDousei.積み(1);
            if (tsumi1 != null)
            {
                PtKanidouseiInfo ret = SetKandiDousei(loginUser,tsumi1);
                PtKanidouseiInfos.Add(ret);
            }
            #endregion

            #region 積み２
            DjDousei tsumi2 = ParentDousei.積み(2);
            if (tsumi2 != null)
            {
                PtKanidouseiInfo ret = SetKandiDousei(loginUser,tsumi2);
                PtKanidouseiInfos.Add(ret);
            }
            #endregion

            #region 揚げ１
            DjDousei age1 = ParentDousei.揚げ(1);
            if (age1 != null)
            {
                PtKanidouseiInfo ret = SetKandiDousei(loginUser, age1);
                PtKanidouseiInfos.Add(ret);
            }
            #endregion

            #region 揚げ２
            DjDousei age2 = ParentDousei.揚げ(2);
            if (age2 != null)
            {
                PtKanidouseiInfo ret = SetKandiDousei(loginUser, age2);
                PtKanidouseiInfos.Add(ret);
            }
            #endregion

            #region 揚げ３
            DjDousei age3 = ParentDousei.揚げ(3);
            if (age3 != null)
            {
                PtKanidouseiInfo ret = SetKandiDousei(loginUser, age3);
                PtKanidouseiInfos.Add(ret);
            }
            #endregion

            #region 揚げ４
            DjDousei age4 = ParentDousei.揚げ(4);
            if (age4 != null)
            {
                PtKanidouseiInfo ret = SetKandiDousei(loginUser, age4);
                PtKanidouseiInfos.Add(ret);
            }
            #endregion

            #region 揚げ５
            DjDousei age5 = ParentDousei.揚げ(5);
            if (age5 != null)
            {
                PtKanidouseiInfo ret = SetKandiDousei(loginUser, age5);
                PtKanidouseiInfos.Add(ret);
            }
            #endregion

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
        }

        private PtKanidouseiInfo SetKandiDousei(MsUser loginUser, DjDousei dousei)
        {
            PtKanidouseiInfo ki = PtKanidouseiInfo.GetRecordByDjDouseiID(loginUser,dousei.DjDouseiID);
            if (ki == null)
            {
                ki = new PtKanidouseiInfo();
                ki.PtKanidouseiInfoId = "";
                ki.DjDouseiID = dousei.DjDouseiID;
            }
            ki.MsVesselID = dousei.MsVesselID;
            ki.EventDate = dousei.DouseiDate;
            ki.MsBashoId = dousei.MsBashoID;
            ki.MsKitiId = dousei.MsKichiID;
            ki.UserKey = "0";
            ki.VesselID = dousei.MsVesselID;
            ki.RenewDate = DateTime.Now;
            ki.RenewUserID = loginUser.MsUserID;

            if (dousei.DjDouseiCargos.Count == 0)
            {
                //貨物が無い場合は積みか揚げが判断できないので不明にする
                ki.MsKanidouseiInfoShubetuId = MsKanidouseiInfoShubetu.不明ID;
            }
            else
            {
                ki.MsKanidouseiInfoShubetuId = dousei.MsKanidouseiInfoShubetuID;
            }
            if (ki.PtKanidouseiInfoId == "")
            {
                int k = KomaSearch(loginUser, ki);
                if (k >= 0)
                {
                    ki.Koma = k;
                }
            }

            return ki;
        }

        private int KomaSearch(MsUser loginUser,PtKanidouseiInfo ki)
        {
            int[] koma = new int[2];

            //データベースに登録されている簡易動静の情報からコマ数を検索する
            List<PtKanidouseiInfo> KanidouseiInfos = PtKanidouseiInfo.GetRecordsByEventDateVessel(loginUser, ki.MsVesselID, ki.EventDate);

            foreach (PtKanidouseiInfo k in KanidouseiInfos)
            {
                if (k.Koma < 2)
                {
                    koma[k.Koma] = 1;
                }
                else
                {
                    return -1;
                }
            }

            //処理中のPtKanidouseiInfosを検索しコマ数はじき出す
            var query = from dousei in PtKanidouseiInfos
                        where dousei.MsVesselID == ki.MsVesselID && dousei.EventDate == ki.EventDate
                        select dousei;

            foreach (PtKanidouseiInfo kd in query)
            {
                koma[kd.Koma] = 1;
            }

            if (koma[0] == 0)
            {
                return 0;
            }
            else if (koma[1] == 0)
            {
                return 1;
            }

            int queryCount = query.Count();

            if (KanidouseiInfos.Count > queryCount)
            {
                return KanidouseiInfos.Count;
            }
            else
            {
                return queryCount;
            }
        }
    }
}
