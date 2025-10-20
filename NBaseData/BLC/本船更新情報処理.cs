using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NBaseData.DAC;

namespace NBaseData.BLC
{
    public class 本船更新情報処理
    {
        #region 船員管理

        public static bool 船員_乗船_登録(NBaseData.DAC.MsUser logiuser, SiCard siCard)
        {
            // 種別
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.船員);
            // 項目
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.乗船);
            // 区分
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.本船更新);
            // 船員
            MsSenin msSenin = MsSenin.GetRecord(new MsUser(), siCard.MsSeninID);
            // フォーマット
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            PtHonsenkoushinInfo ptHonsenkoushinInfo = new PtHonsenkoushinInfo();

            ptHonsenkoushinInfo.PtHonsenkoushinInfoId = Guid.NewGuid().ToString();
            ptHonsenkoushinInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptHonsenkoushinInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptHonsenkoushinInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptHonsenkoushinInfo.SanshoumotoId = siCard.SiCardID;
            ptHonsenkoushinInfo.EventDate = DateTime.Now;
            ptHonsenkoushinInfo.MsVesselId = siCard.MsVesselID;
            ptHonsenkoushinInfo.HonsenkoushinInfoUser = logiuser.MsUserID;
            ptHonsenkoushinInfo.KoushinNaiyou = String.Format(infoFormat.Shousai, msSenin.Sei + msSenin.Mei);

            ptHonsenkoushinInfo.DeleteFlag = 0;
            ptHonsenkoushinInfo.SendFlag = 0;
            ptHonsenkoushinInfo.VesselID = siCard.VesselID;
            ptHonsenkoushinInfo.DataNo = 0;
            ptHonsenkoushinInfo.UserKey = "1";
            ptHonsenkoushinInfo.RenewDate = DateTime.Now;
            ptHonsenkoushinInfo.RenewUserID = logiuser.MsUserID;

            return ptHonsenkoushinInfo.InsertRecord(logiuser);
        }

        public static bool 船員_下船_登録(NBaseData.DAC.MsUser logiuser, SiCard siCard)
        {
            // 種別
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.船員);
            // 項目
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.下船);
            // 区分
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.本船更新);
            // 船員
            MsSenin msSenin = MsSenin.GetRecord(new MsUser(), siCard.MsSeninID);
            // フォーマット
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            PtHonsenkoushinInfo ptHonsenkoushinInfo = new PtHonsenkoushinInfo();

            ptHonsenkoushinInfo.PtHonsenkoushinInfoId = Guid.NewGuid().ToString();
            ptHonsenkoushinInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptHonsenkoushinInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptHonsenkoushinInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptHonsenkoushinInfo.SanshoumotoId = siCard.SiCardID;
            ptHonsenkoushinInfo.EventDate = DateTime.Now;
            ptHonsenkoushinInfo.MsVesselId = siCard.MsVesselID;
            ptHonsenkoushinInfo.HonsenkoushinInfoUser = logiuser.MsUserID;
            ptHonsenkoushinInfo.KoushinNaiyou = String.Format(infoFormat.Shousai, msSenin.Sei + msSenin.Mei);

            ptHonsenkoushinInfo.DeleteFlag = 0;
            ptHonsenkoushinInfo.SendFlag = 0;
            ptHonsenkoushinInfo.VesselID = siCard.VesselID;
            ptHonsenkoushinInfo.DataNo = 0;
            ptHonsenkoushinInfo.UserKey = "1";
            ptHonsenkoushinInfo.RenewDate = DateTime.Now;
            ptHonsenkoushinInfo.RenewUserID = logiuser.MsUserID;

            return ptHonsenkoushinInfo.InsertRecord(logiuser);
        }

        public static bool 船員_下船_交代(NBaseData.DAC.MsUser logiuser, SiCard siCard下船, SiCard siCard乗船)
        {
            // 種別
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.船員);
            // 項目
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.交代);
            // 区分
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.本船更新);
            // 船員
            MsSenin msSenin下船 = MsSenin.GetRecord(new MsUser(), siCard下船.MsSeninID);
            MsSenin msSenin乗船 = MsSenin.GetRecord(new MsUser(), siCard乗船.MsSeninID);
            // フォーマット
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            PtHonsenkoushinInfo ptHonsenkoushinInfo = new PtHonsenkoushinInfo();

            ptHonsenkoushinInfo.PtHonsenkoushinInfoId = Guid.NewGuid().ToString();
            ptHonsenkoushinInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptHonsenkoushinInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptHonsenkoushinInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptHonsenkoushinInfo.SanshoumotoId = siCard下船.SiCardID;
            ptHonsenkoushinInfo.EventDate = DateTime.Now;
            ptHonsenkoushinInfo.MsVesselId = siCard下船.MsVesselID;
            ptHonsenkoushinInfo.HonsenkoushinInfoUser = logiuser.MsUserID;
            ptHonsenkoushinInfo.KoushinNaiyou = String.Format(infoFormat.Shousai, (msSenin下船.Sei + msSenin下船.Mei), (msSenin乗船.Sei + msSenin乗船.Mei));

            ptHonsenkoushinInfo.DeleteFlag = 0;
            ptHonsenkoushinInfo.SendFlag = 0;
            ptHonsenkoushinInfo.VesselID = siCard下船.VesselID;
            ptHonsenkoushinInfo.DataNo = 0;
            ptHonsenkoushinInfo.UserKey = "1";
            ptHonsenkoushinInfo.RenewDate = DateTime.Now;
            ptHonsenkoushinInfo.RenewUserID = logiuser.MsUserID;

            return ptHonsenkoushinInfo.InsertRecord(logiuser);
        }

        #endregion

        #region 発注管理

        public static bool 発注_手配依頼_登録(NBaseData.DAC.MsUser logiuser, OdThi odThi)
        {
            // 種別
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.発注);
            // 項目
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.手配依頼);
            // 区分
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.本船更新);
            // フォーマット
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            PtHonsenkoushinInfo ptHonsenkoushinInfo = new PtHonsenkoushinInfo();

            ptHonsenkoushinInfo.PtHonsenkoushinInfoId = Guid.NewGuid().ToString();
            ptHonsenkoushinInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptHonsenkoushinInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptHonsenkoushinInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptHonsenkoushinInfo.SanshoumotoId = odThi.OdThiID;
            ptHonsenkoushinInfo.EventDate = DateTime.Now;
            ptHonsenkoushinInfo.MsVesselId = odThi.MsVesselID;
            ptHonsenkoushinInfo.HonsenkoushinInfoUser = logiuser.MsUserID;
            ptHonsenkoushinInfo.KoushinNaiyou = String.Format(infoFormat.Shousai, odThi.TehaiIraiNo, odThi.Naiyou);

            ptHonsenkoushinInfo.DeleteFlag = 0;
            ptHonsenkoushinInfo.SendFlag = 0;
            ptHonsenkoushinInfo.VesselID = 0;
            ptHonsenkoushinInfo.DataNo = 0;
            ptHonsenkoushinInfo.UserKey = "1";
            ptHonsenkoushinInfo.RenewDate = DateTime.Now;
            ptHonsenkoushinInfo.RenewUserID = logiuser.MsUserID;

            return ptHonsenkoushinInfo.InsertRecord(logiuser);
        }

        #endregion
    }
}
