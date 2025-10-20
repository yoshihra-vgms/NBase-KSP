using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NBaseData.DAC;
using ORMapping;
using NBaseData.DS;

namespace NBaseData.BLC
{
    public class 事務所更新情報処理
    {
        private static string 新規ID()
        {
            return System.Guid.NewGuid().ToString();
        }

        public static bool 見積依頼登録(DBConnect dbConnect, MsUser loginUser, string odThiId)
        {
            PtJimushokoushinInfo info = new PtJimushokoushinInfo();
            OdThi odThi = OdThi.GetRecord(dbConnect, loginUser, odThiId);

            info.PtJimushokoushinInfoId = 新規ID();

            info.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString();
            info.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.手配依頼).ToString();
            info.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.事務所更新).ToString();
            info.SanshoumotoId = odThi.OdThiID;
            info.EventDate = DateTime.Now;
            info.MsVesselId = odThi.MsVesselID;
            info.VesselID = odThi.MsVesselID;
            info.HonsenkoushinInfoUser = loginUser.MsUserID;

            info.Shubetsu = "発注管理";
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
            info.Naiyou = infoFormat.Naiyou;
            info.KoushinNaiyou = String.Format(infoFormat.Shousai, odThi.TehaiIraiNo, odThi.Naiyou);

            info.RenewDate = DateTime.Now;
            info.RenewUserID = loginUser.MsUserID;

            return info.InsertRecord(dbConnect, loginUser);
        }

        public static bool 発注登録(DBConnect dbConnect, MsUser loginUser, OdJry odJry)
        {
            PtJimushokoushinInfo info = new PtJimushokoushinInfo();

            OdThi odThi = OdThi.GetRecordByOdJryID(dbConnect, loginUser, odJry.OdJryID);

            info.PtJimushokoushinInfoId = 新規ID();

            info.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString();
            info.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.発注).ToString();
            info.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.事務所更新).ToString();
            info.SanshoumotoId = odThi.OdThiID;
            info.EventDate = DateTime.Now;
            info.MsVesselId = odThi.MsVesselID;
            info.VesselID = odThi.MsVesselID;
            info.HonsenkoushinInfoUser = loginUser.MsUserID;

            info.Shubetsu = "発注管理";
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
            info.Naiyou = infoFormat.Naiyou;
            info.KoushinNaiyou = String.Format(infoFormat.Shousai, odThi.TehaiIraiNo, odThi.Naiyou);

            info.RenewDate = DateTime.Now;
            info.RenewUserID = loginUser.MsUserID;

            return info.InsertRecord(dbConnect, loginUser);
        }

        public static bool 受領登録(DBConnect dbConnect, MsUser loginUser, OdJry odJry)
        {
            PtJimushokoushinInfo info = new PtJimushokoushinInfo();

            OdThi odThi = OdThi.GetRecordByOdJryID(dbConnect, loginUser, odJry.OdJryID);

            info.PtJimushokoushinInfoId = 新規ID();

            info.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString();
            info.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.受領).ToString();
            info.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.事務所更新).ToString();
            info.SanshoumotoId = odThi.OdThiID;
            info.EventDate = DateTime.Now;
            info.MsVesselId = odThi.MsVesselID;
            info.VesselID = odThi.MsVesselID;
            info.HonsenkoushinInfoUser = loginUser.MsUserID;

            info.Shubetsu = "発注管理";
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
            info.Naiyou = infoFormat.Naiyou;
            info.KoushinNaiyou = String.Format(infoFormat.Shousai, odThi.TehaiIraiNo, odThi.Naiyou);

            info.RenewDate = DateTime.Now;
            info.RenewUserID = loginUser.MsUserID;

            return info.InsertRecord(dbConnect, loginUser);
        }

        public static bool 管理記録登録(DBConnect dbConnect, MsUser loginUser, DmKanriKiroku kanriKiroku, List<DmKoukaiSaki> koukaisaki)
        {
            PtJimushokoushinInfo info = new PtJimushokoushinInfo();

            info.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.文書).ToString();
            info.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.管理文書登録).ToString();
            info.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.事務所更新).ToString();
            //info.SanshoumotoId 　// 参照元はなし
            info.EventDate = DateTime.Now;

            info.HonsenkoushinInfoUser = loginUser.MsUserID;

            info.Shubetsu = "文書管理";
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
            info.Naiyou = infoFormat.Naiyou;
            info.KoushinNaiyou = String.Format(infoFormat.Shousai, kanriKiroku.BunruiName, kanriKiroku.BunshoName);

            info.RenewDate = DateTime.Now;
            info.RenewUserID = loginUser.MsUserID;

            // 公開先対象の船分だけ登録する
            bool ret = true;
            foreach (DmKoukaiSaki kk in koukaisaki)
            {
                if (kk.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                {
                    info.PtJimushokoushinInfoId = 新規ID();
                    info.MsVesselId = kk.MsVesselID;
                    info.VesselID = kk.MsVesselID;

                    ret = info.InsertRecord(dbConnect, loginUser);
                    if (ret == false)
                        break;
                }
            }

            return ret;
        }

        public static bool 公文書規則登録(DBConnect dbConnect, MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku, List<DmKoukaiSaki> koukaisaki)
        {
            PtJimushokoushinInfo info = new PtJimushokoushinInfo();


            info.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.文書).ToString();
            info.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.公文書_規則登録).ToString();
            info.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.事務所更新).ToString();
            //info.SanshoumotoId 　// 参照元はなし
            info.EventDate = DateTime.Now;

            info.HonsenkoushinInfoUser = loginUser.MsUserID;

            info.Shubetsu = "文書管理";
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
            info.Naiyou = infoFormat.Naiyou;
            info.KoushinNaiyou = String.Format(infoFormat.Shousai, koubunshoKisoku.BunruiName, koubunshoKisoku.BunshoName);

            info.RenewDate = DateTime.Now;
            info.RenewUserID = loginUser.MsUserID;

            // 公開先対象の船分だけ登録する
            bool ret = true;
            foreach (DmKoukaiSaki kk in koukaisaki)
            {
                if (kk.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                {
                    info.PtJimushokoushinInfoId = 新規ID();
                    info.MsVesselId = kk.MsVesselID;
                    info.VesselID = kk.MsVesselID;

                    ret = info.InsertRecord(dbConnect, loginUser);
                    if (ret == false)
                        break;
                }
            }

            return ret;
        }

        public static bool コメント登録(DBConnect dbConnect, MsUser loginUser, DmKanriKiroku kanriKiroku)
        {
            List<DmKoukaiSaki> koukaisaki = DmKoukaiSaki.GetRecordsByLinkSakiID(dbConnect, loginUser, kanriKiroku.DmKanriKirokuID);

            PtJimushokoushinInfo info = new PtJimushokoushinInfo();

            info.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.文書).ToString();
            info.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.コメント登録).ToString();
            info.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.事務所更新).ToString();
            //info.SanshoumotoId 　// 参照元はなし
            info.EventDate = DateTime.Now;

            info.HonsenkoushinInfoUser = loginUser.MsUserID;

            info.Shubetsu = "文書管理";
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
            info.Naiyou = infoFormat.Naiyou;
            info.KoushinNaiyou = String.Format(infoFormat.Shousai, kanriKiroku.BunruiName, kanriKiroku.BunshoName);

            info.RenewDate = DateTime.Now;
            info.RenewUserID = loginUser.MsUserID;

            // 公開先対象の船分だけ登録する
            bool ret = true;
            foreach (DmKoukaiSaki kk in koukaisaki)
            {
                if (kk.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                {
                    info.PtJimushokoushinInfoId = 新規ID();
                    info.MsVesselId = kk.MsVesselID;
                    info.VesselID = kk.MsVesselID;

                    ret = info.InsertRecord(dbConnect, loginUser);
                    if (ret == false)
                        break;
                }
            }
            return ret;
        }

        public static bool コメント登録(DBConnect dbConnect, MsUser loginUser, DmKoubunshoKisoku koubunshoKisoku)
        {
            List<DmKoukaiSaki> koukaisaki = DmKoukaiSaki.GetRecordsByLinkSakiID(dbConnect, loginUser, koubunshoKisoku.DmKoubunshoKisokuID);

            PtJimushokoushinInfo info = new PtJimushokoushinInfo();

            info.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.文書).ToString();
            info.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.コメント登録).ToString();
            info.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.事務所更新).ToString();
            //info.SanshoumotoId 　// 参照元はなし
            info.EventDate = DateTime.Now;

            info.HonsenkoushinInfoUser = loginUser.MsUserID;

            info.Shubetsu = "文書管理";
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
            info.Naiyou = infoFormat.Naiyou;
            info.KoushinNaiyou = String.Format(infoFormat.Shousai, koubunshoKisoku.BunruiName, koubunshoKisoku.BunshoName);

            info.RenewDate = DateTime.Now;
            info.RenewUserID = loginUser.MsUserID;

            // 公開先対象の船分だけ登録する
            bool ret = true;
            foreach (DmKoukaiSaki kk in koukaisaki)
            {
                if (kk.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                {
                    info.PtJimushokoushinInfoId = 新規ID();
                    info.MsVesselId = kk.MsVesselID;
                    info.VesselID = kk.MsVesselID;

                    ret = info.InsertRecord(dbConnect, loginUser);
                    if (ret == false)
                        break;
                }
            }
            return ret;
        }

        public static bool 船用金送金登録(DBConnect dbConnect, MsUser loginUser, SiSoukin soukin)
        {
            PtJimushokoushinInfo info = new PtJimushokoushinInfo();

            info.PtJimushokoushinInfoId = 新規ID();

            info.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.船員).ToString();
            info.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.船用金送金).ToString();
            info.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.事務所更新).ToString();
            info.SanshoumotoId = soukin.SiSoukinID;
            info.EventDate = DateTime.Now;
            info.MsVesselId = soukin.MsVesselID;
            info.VesselID = soukin.MsVesselID;
            info.HonsenkoushinInfoUser = loginUser.MsUserID;

            info.Shubetsu = "船員管理";
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
            info.Naiyou = infoFormat.Naiyou;
            info.KoushinNaiyou = infoFormat.Shousai;

            info.RenewDate = DateTime.Now;
            info.RenewUserID = loginUser.MsUserID;

            return info.InsertRecord(dbConnect, loginUser);
        }

        public static bool 配乗表更新登録(DBConnect dbConnect, MsUser loginUser, SeninTableCache seninTableCache)
        {
            PtJimushokoushinInfo info = new PtJimushokoushinInfo();

            info.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.船員).ToString();
            info.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.配乗表更新).ToString();
            info.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.事務所更新).ToString();
            //info.SanshoumotoId 　// 参照元はなし
            info.EventDate = DateTime.Now;
            info.HonsenkoushinInfoUser = loginUser.MsUserID;

            info.Shubetsu = "船員管理";
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, info.MsPortalInfoShubetuId, info.MsPortalInfoKoumokuId, info.MsPortalInfoKubunId);
            info.Naiyou = infoFormat.Naiyou;
            info.KoushinNaiyou = infoFormat.Shousai;

            info.RenewDate = DateTime.Now;
            info.RenewUserID = loginUser.MsUserID;

            // 公開先対象の船分だけ登録する
            bool ret = true;
            foreach (MsVessel v in seninTableCache.GetMsVesselList(loginUser))
            {
                info.PtJimushokoushinInfoId = 新規ID();
                info.MsVesselId = v.MsVesselID;
                info.VesselID = v.MsVesselID;

                ret = info.InsertRecord(dbConnect, loginUser);
                if (ret == false)
                    break;
            }

            return ret;
        }
    }
}
