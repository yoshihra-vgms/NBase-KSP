using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NBaseData.DAC;

namespace NBaseData.BLC
{
    public class Alarm処理
    {
        #region 検査証書_検査
        public static bool 検査証書_検査_削除(NBaseData.DAC.MsUser logiuser, KsKensa kensa, string kubunName)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.検査);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), kubunName);

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, kensa.KsKensaID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.DeleteFlag = 1;

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_検査_停止(NBaseData.DAC.MsUser logiuser, KsKensa kensa, string kubunName)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.検査);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), kubunName);

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, kensa.KsKensaID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.AlarmShowFlag = 1;
            ptAlarmInfo.AlarmStopDate = DateTime.Now;
            ptAlarmInfo.AlarmStopUser = logiuser.MsUserID;

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_検査_更新(NBaseData.DAC.MsUser logiuser, KsKensa kensa, string kubunName)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(logiuser, MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(logiuser, MsPortalInfoKoumoku.検査);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(logiuser, kubunName);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(logiuser, kensa.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(logiuser, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, kensa.KsKensaID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

			string s = "";
            if (kubunName == MsPortalInfoKubun.アラーム90日前)
            {
                ptAlarmInfo.HasseiDate = kensa.Alarm90Date;

				//90日前のフォーマットを作る
				//s = String.Format(infoFormat.Shousai, vessel.VesselName, kensa.KensaName, "90日前");
                MsAlarmDate MsAlarmDate90日前 = MsAlarmDate.GetRecord(logiuser, MsAlarmDate.MsAlarmDateIDNo.検査アラーム2アラームID);
                s = String.Format(infoFormat.Shousai, vessel.VesselName, kensa.KensaName, MsAlarmDate90日前.DayOffset.ToString());
            }
            else if (kubunName == MsPortalInfoKubun.アラーム180日前)
            {
                ptAlarmInfo.HasseiDate = kensa.Alarm180Date;

				//180日前のフォーマットを作る
				//s = String.Format(infoFormat.Shousai, vessel.VesselName, kensa.KensaName, "180日前");
                MsAlarmDate MsAlarmDate180日前 = MsAlarmDate.GetRecord(logiuser, MsAlarmDate.MsAlarmDateIDNo.検査アラーム1アラームID);
                s = String.Format(infoFormat.Shousai, vessel.VesselName, kensa.KensaName, MsAlarmDate180日前.DayOffset.ToString());
            }
            ptAlarmInfo.Yuukoukigen = kensa.ShinsaDate;
			ptAlarmInfo.Shousai = s;

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_検査_登録(NBaseData.DAC.MsUser logiuser, KsKensa kensa, string kubunName)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(logiuser, MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(logiuser, MsPortalInfoKoumoku.検査);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(logiuser, kubunName);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(logiuser, kensa.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(logiuser, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");
            NBaseData.DAC.PtAlarmInfo ptAlarmInfo = new NBaseData.DAC.PtAlarmInfo();

            ptAlarmInfo.PtAlarmInfoId = Guid.NewGuid().ToString();
            ptAlarmInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptAlarmInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptAlarmInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptAlarmInfo.SanshoumotoId = kensa.KsKensaID;

			string s = "";
			if (kubunName == MsPortalInfoKubun.アラーム90日前)
			{
				ptAlarmInfo.HasseiDate = kensa.Alarm90Date;

				//90日前のフォーマットを作る
                // 2010.03.29:aki
                //s = String.Format(infoFormat.Shousai, vessel.VesselName, kensa.KensaName, "90日前");
                MsAlarmDate MsAlarmDate90日前 = MsAlarmDate.GetRecord(logiuser, MsAlarmDate.MsAlarmDateIDNo.検査アラーム2アラームID);
                s = String.Format(infoFormat.Shousai, vessel.VesselName, kensa.KensaName, MsAlarmDate90日前.DayOffset.ToString());
			}
			else if (kubunName == MsPortalInfoKubun.アラーム180日前)
			{
				ptAlarmInfo.HasseiDate = kensa.Alarm180Date;

				//180日前のフォーマットを作る
                // 2010.03.29:aki
				//s = String.Format(infoFormat.Shousai, vessel.VesselName, kensa.KensaName, "180日前");
                MsAlarmDate MsAlarmDate180日前 = MsAlarmDate.GetRecord(logiuser, MsAlarmDate.MsAlarmDateIDNo.検査アラーム1アラームID);
                s = String.Format(infoFormat.Shousai, vessel.VesselName, kensa.KensaName, MsAlarmDate180日前.DayOffset.ToString());
			}


            ptAlarmInfo.MsVesselId = kensa.MsVesselID;
            ptAlarmInfo.Yuukoukigen = kensa.ShinsaDate;
            ptAlarmInfo.Naiyou = infoFormat.Naiyou;
			ptAlarmInfo.Shousai = s;
            ptAlarmInfo.AlarmShowFlag = 0;

            ptAlarmInfo.DeleteFlag = 0;
            ptAlarmInfo.SendFlag = 0;
            ptAlarmInfo.VesselID = kensa.MsVesselID;
            ptAlarmInfo.DataNo = 0;
            ptAlarmInfo.UserKey = "1";
            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.InsertRecord(logiuser);
        }
        #endregion

        #region 検査証書_証書
        public static bool 検査証書_証書_削除(NBaseData.DAC.MsUser logiuser, KsShousho shousho)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
			MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.証書);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.有効期限);

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, shousho.KsShoushoID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.DeleteFlag = 1;

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_証書_停止(NBaseData.DAC.MsUser logiuser, KsShousho shousho)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.証書);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.有効期限);

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, shousho.KsShoushoID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.AlarmShowFlag = 1;
            ptAlarmInfo.AlarmStopDate = DateTime.Now;
            ptAlarmInfo.AlarmStopUser = logiuser.MsUserID;

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_証書_更新(NBaseData.DAC.MsUser logiuser, KsShousho shousho)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
			MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.証書);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.有効期限);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), shousho.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, shousho.KsShoushoID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.HasseiDate = shousho.AlarmDate;
            ptAlarmInfo.Yuukoukigen = shousho.Yukoukigen;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, shousho.MsShoushoName);

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_証書_登録(NBaseData.DAC.MsUser logiuser, KsShousho shousho)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.証書);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.有効期限);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), shousho.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            NBaseData.DAC.PtAlarmInfo ptAlarmInfo = new NBaseData.DAC.PtAlarmInfo();

            ptAlarmInfo.PtAlarmInfoId = Guid.NewGuid().ToString();
            ptAlarmInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptAlarmInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptAlarmInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptAlarmInfo.SanshoumotoId = shousho.KsShoushoID;
            ptAlarmInfo.HasseiDate = shousho.AlarmDate;
            ptAlarmInfo.MsVesselId = shousho.MsVesselID;
            ptAlarmInfo.Yuukoukigen = shousho.Yukoukigen;
            ptAlarmInfo.Naiyou = infoFormat.Naiyou;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, shousho.MsShoushoName);
            ptAlarmInfo.AlarmShowFlag = 0;

            ptAlarmInfo.DeleteFlag = 0;
            ptAlarmInfo.SendFlag = 0;
            ptAlarmInfo.VesselID = shousho.MsVesselID;
            ptAlarmInfo.DataNo = 0;
            ptAlarmInfo.UserKey = "1";
            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.InsertRecord(new MsUser());
        }

        #endregion

        #region 審査
        public static bool 検査証書_審査_削除(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa, string kubunName)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.審査日);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), kubunName);

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, shinsa.KsShinsaID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.DeleteFlag = 1;

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_審査_停止(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa, string kubunName)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.審査日);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), kubunName);

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, shinsa.KsShinsaID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.AlarmShowFlag = 1;
            ptAlarmInfo.AlarmStopDate = DateTime.Now;
            ptAlarmInfo.AlarmStopUser = logiuser.MsUserID;

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_審査_更新(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa, string kubunName)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.審査日);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), kubunName);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), shinsa.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, shinsa.KsShinsaID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            if (kubunName == MsPortalInfoKubun.審査)
            {
                ptAlarmInfo.HasseiDate = shinsa.ShinsaAlarmDate;
                ptAlarmInfo.Yuukoukigen = shinsa.ShinsaDate;
            }
            else if (kubunName == MsPortalInfoKubun.内部審査)
            {
                ptAlarmInfo.HasseiDate = shinsa.NaibuAlarmDate;
                ptAlarmInfo.Yuukoukigen = shinsa.NaibuDate;
            }
            else if (kubunName == MsPortalInfoKubun.レビュー)
            {
                ptAlarmInfo.HasseiDate = shinsa.ReviewAlarmDate;
                ptAlarmInfo.Yuukoukigen = shinsa.ReviewDate;
            }
            // 2010.03.29:aki 
            //ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, shinsa.MsShinsaName);
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, shinsa.MsShinsaName, kubunName);

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_審査_登録(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa, string kubunName)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.審査日);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), kubunName);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), shinsa.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            NBaseData.DAC.PtAlarmInfo ptAlarmInfo = new NBaseData.DAC.PtAlarmInfo();

            ptAlarmInfo.PtAlarmInfoId = Guid.NewGuid().ToString();
            ptAlarmInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptAlarmInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptAlarmInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptAlarmInfo.SanshoumotoId = shinsa.KsShinsaID;
            //ptAlarmInfo.HasseiDate = shinsa.ShinsaAlarmDate;
            ptAlarmInfo.MsVesselId = shinsa.MsVesselID;
            //ptAlarmInfo.Yuukoukigen = shinsa.ShinsaDate;
            ptAlarmInfo.Naiyou = infoFormat.Naiyou;
            // 2010.03.29:aki 
            //ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, shinsa.MsShinsaName);
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, shinsa.MsShinsaName, kubunName);
            ptAlarmInfo.AlarmShowFlag = 0;

            ptAlarmInfo.DeleteFlag = 0;
            ptAlarmInfo.SendFlag = 0;
            ptAlarmInfo.VesselID = shinsa.MsVesselID;
            ptAlarmInfo.DataNo = 0;
            ptAlarmInfo.UserKey = "1";
            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;


			if (kubunName == MsPortalInfoKubun.審査)
			{
				ptAlarmInfo.HasseiDate = shinsa.ShinsaAlarmDate;
				ptAlarmInfo.Yuukoukigen = shinsa.ShinsaDate;
			}
			else if (kubunName == MsPortalInfoKubun.内部審査)
			{
				ptAlarmInfo.HasseiDate = shinsa.NaibuAlarmDate;
				ptAlarmInfo.Yuukoukigen = shinsa.NaibuDate;
			}
			else if (kubunName == MsPortalInfoKubun.レビュー)
			{
				ptAlarmInfo.HasseiDate = shinsa.ReviewAlarmDate;
				ptAlarmInfo.Yuukoukigen = shinsa.ReviewDate;
			}

            return ptAlarmInfo.InsertRecord(logiuser);
        }

        #endregion

        #region 救命設備
        public static bool 検査証書_救命設備_削除(NBaseData.DAC.MsUser logiuser, KsKyumeisetsubi kyumei)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.救命設備);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.点検);

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, kyumei.KsKyumeisetsubiID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.DeleteFlag = 1;

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_救命設備_停止(NBaseData.DAC.MsUser logiuser, KsKyumeisetsubi kyumei)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.救命設備);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.点検);

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, kyumei.KsKyumeisetsubiID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.AlarmShowFlag = 1;
            ptAlarmInfo.AlarmStopDate = DateTime.Now;
            ptAlarmInfo.AlarmStopUser = logiuser.MsUserID;

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_救命設備_更新(NBaseData.DAC.MsUser logiuser, KsKyumeisetsubi kyumei)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.救命設備);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.点検);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), kyumei.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, kyumei.KsKyumeisetsubiID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.HasseiDate = kyumei.AlarmDate;
            ptAlarmInfo.Yuukoukigen = kyumei.TenkenDate;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, kyumei.MsKyumeisetsubiName);

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_救命設備_登録(NBaseData.DAC.MsUser logiuser, KsKyumeisetsubi kyumei)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.救命設備);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.点検);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), kyumei.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            NBaseData.DAC.PtAlarmInfo ptAlarmInfo = new NBaseData.DAC.PtAlarmInfo();

            ptAlarmInfo.PtAlarmInfoId = Guid.NewGuid().ToString();
            ptAlarmInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptAlarmInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptAlarmInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptAlarmInfo.SanshoumotoId = kyumei.KsKyumeisetsubiID;
            ptAlarmInfo.HasseiDate = kyumei.AlarmDate;
            ptAlarmInfo.MsVesselId = kyumei.MsVesselID;
            ptAlarmInfo.Yuukoukigen = kyumei.TenkenDate;
            ptAlarmInfo.Naiyou = infoFormat.Naiyou;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, kyumei.MsKyumeisetsubiName);
            ptAlarmInfo.AlarmShowFlag = 0;

            ptAlarmInfo.DeleteFlag = 0;
            ptAlarmInfo.SendFlag = 0;
            ptAlarmInfo.VesselID = kyumei.MsVesselID;
            ptAlarmInfo.DataNo = 0;
            ptAlarmInfo.UserKey = "1";
            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.InsertRecord(logiuser);
        }
        #endregion

        #region 荷役安全設備
        public static bool 検査証書_荷役安全設備_削除(NBaseData.DAC.MsUser logiuser, KsNiyaku niyaku)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.荷役安全設備);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.点検);

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, niyaku.KsNiyakuID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.DeleteFlag = 1;

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_荷役安全設備_停止(NBaseData.DAC.MsUser logiuser, KsNiyaku niyaku)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.荷役安全設備);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.点検);

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, niyaku.KsNiyakuID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.AlarmShowFlag = 1;
            ptAlarmInfo.AlarmStopDate = DateTime.Now;
            ptAlarmInfo.AlarmStopUser = logiuser.MsUserID;

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_荷役安全設備_更新(NBaseData.DAC.MsUser logiuser, KsNiyaku niyaku)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.荷役安全設備);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.点検);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), niyaku.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, niyaku.KsNiyakuID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.HasseiDate = niyaku.AlarmDate;
            ptAlarmInfo.Yuukoukigen = niyaku.TenkenDate;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, niyaku.MsNiyakusetsubiName);

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_荷役安全設備_登録(NBaseData.DAC.MsUser logiuser, KsNiyaku niyaku)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.荷役安全設備);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.点検);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), niyaku.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            NBaseData.DAC.PtAlarmInfo ptAlarmInfo = new NBaseData.DAC.PtAlarmInfo();

            ptAlarmInfo.PtAlarmInfoId = Guid.NewGuid().ToString();
            ptAlarmInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptAlarmInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptAlarmInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptAlarmInfo.SanshoumotoId = niyaku.KsNiyakuID;
            ptAlarmInfo.HasseiDate = niyaku.AlarmDate;
            ptAlarmInfo.MsVesselId = niyaku.MsVesselID;
            ptAlarmInfo.Yuukoukigen = niyaku.TenkenDate;
            ptAlarmInfo.Naiyou = infoFormat.Naiyou;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, niyaku.MsNiyakusetsubiName);
            ptAlarmInfo.AlarmShowFlag = 0;

            ptAlarmInfo.DeleteFlag = 0;
            ptAlarmInfo.SendFlag = 0;
            ptAlarmInfo.VesselID = niyaku.MsVesselID;
            ptAlarmInfo.DataNo = 0;
            ptAlarmInfo.UserKey = "1";
            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.InsertRecord(logiuser);
        }
        #endregion

        #region 検船
        public static bool 検査証書_検船_削除(NBaseData.DAC.MsUser logiuser, KsKensen kensen)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.検船);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.点検);

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, kensen.KsKensenID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.DeleteFlag = 1;

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_検船_停止(NBaseData.DAC.MsUser logiuser, KsKensen kensen)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.検船);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.点検);

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, kensen.KsKensenID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.AlarmShowFlag = 1;
            ptAlarmInfo.AlarmStopDate = DateTime.Now;
            ptAlarmInfo.AlarmStopUser = logiuser.MsUserID;

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_検船_更新(NBaseData.DAC.MsUser logiuser, KsKensen kensen)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.検船);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.点検);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), kensen.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            PtAlarmInfo ptAlarmInfo
                = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(logiuser, kensen.KsKensenID, shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, kubun.MsPortalInfoKubunId);

            if (ptAlarmInfo == null)
            {
                return false;
            }

            ptAlarmInfo.HasseiDate = kensen.AlarmDate;
            ptAlarmInfo.Yuukoukigen = kensen.ShinsaDate;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, kensen.MsKensenName);

            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.UpdateRecord(logiuser);
        }

        public static bool 検査証書_検船_登録(NBaseData.DAC.MsUser logiuser, KsKensen kensen)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.検船);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.点検);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), kensen.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            NBaseData.DAC.PtAlarmInfo ptAlarmInfo = new NBaseData.DAC.PtAlarmInfo();

            ptAlarmInfo.PtAlarmInfoId = Guid.NewGuid().ToString();
            ptAlarmInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptAlarmInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptAlarmInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptAlarmInfo.SanshoumotoId = kensen.KsKensenID;
            ptAlarmInfo.HasseiDate = kensen.AlarmDate;
            ptAlarmInfo.MsVesselId = kensen.MsVesselID;
            ptAlarmInfo.Yuukoukigen = kensen.ShinsaDate;
            ptAlarmInfo.Naiyou = infoFormat.Naiyou;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, kensen.MsKensenName);
            ptAlarmInfo.AlarmShowFlag = 0;

            ptAlarmInfo.DeleteFlag = 0;
            ptAlarmInfo.SendFlag = 0;
            ptAlarmInfo.VesselID = kensen.MsVesselID;
            ptAlarmInfo.DataNo = 0;
            ptAlarmInfo.UserKey = "1";
            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.InsertRecord(logiuser);
        }
        #endregion

        #region
        public static bool 検査証書_検査_90日前_登録(NBaseData.DAC.MsUser logiuser, KsKensa kensa)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.検査);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.アラーム90日前);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), kensa.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            NBaseData.DAC.PtAlarmInfo ptAlarmInfo = new NBaseData.DAC.PtAlarmInfo();

            ptAlarmInfo.PtAlarmInfoId = Guid.NewGuid().ToString();
            ptAlarmInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptAlarmInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptAlarmInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptAlarmInfo.SanshoumotoId = kensa.KsKensaID;
            ptAlarmInfo.HasseiDate = kensa.Alarm90Date;
            ptAlarmInfo.MsVesselId = kensa.MsVesselID;
            ptAlarmInfo.Yuukoukigen = kensa.ShinsaDate;
            ptAlarmInfo.Naiyou = infoFormat.Naiyou;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, kensa.KensaName);
            ptAlarmInfo.AlarmShowFlag = 0;

            ptAlarmInfo.DeleteFlag = 0;
            ptAlarmInfo.SendFlag = 0;
            ptAlarmInfo.VesselID = kensa.MsVesselID;
            ptAlarmInfo.DataNo = 0;
            ptAlarmInfo.UserKey = "1";
            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.InsertRecord(new MsUser());
        }

        public static bool 検査証書_検査_180日前_登録(NBaseData.DAC.MsUser logiuser, KsKensa kensa)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.検査);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.アラーム180日前);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), kensa.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            NBaseData.DAC.PtAlarmInfo ptAlarmInfo = new NBaseData.DAC.PtAlarmInfo();

            ptAlarmInfo.PtAlarmInfoId = Guid.NewGuid().ToString();
            ptAlarmInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptAlarmInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptAlarmInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptAlarmInfo.SanshoumotoId = kensa.KsKensaID;
            ptAlarmInfo.HasseiDate = kensa.Alarm180Date;
            ptAlarmInfo.MsVesselId = kensa.MsVesselID;
            ptAlarmInfo.Yuukoukigen = kensa.ShinsaDate;
            ptAlarmInfo.Naiyou = infoFormat.Naiyou;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, kensa.KensaName);
            ptAlarmInfo.AlarmShowFlag = 0;

            ptAlarmInfo.DeleteFlag = 0;
            ptAlarmInfo.SendFlag = 0;
            ptAlarmInfo.VesselID = kensa.MsVesselID;
            ptAlarmInfo.DataNo = 0;
            ptAlarmInfo.UserKey = "1";
            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.InsertRecord(new MsUser());
        }
        

        public static bool 検査証書_審査_審査_登録(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.審査日);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.検査);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), shinsa.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            NBaseData.DAC.PtAlarmInfo ptAlarmInfo = new NBaseData.DAC.PtAlarmInfo();

            ptAlarmInfo.PtAlarmInfoId = Guid.NewGuid().ToString();
            ptAlarmInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptAlarmInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptAlarmInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptAlarmInfo.SanshoumotoId = shinsa.KsShinsaID;
            ptAlarmInfo.HasseiDate = shinsa.ShinsaAlarmDate;
            ptAlarmInfo.MsVesselId = shinsa.MsVesselID;
            ptAlarmInfo.Yuukoukigen = shinsa.ShinsaDate;
            ptAlarmInfo.Naiyou = infoFormat.Naiyou;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, shinsa.MsShinsaName);
            ptAlarmInfo.AlarmShowFlag = 0;

            ptAlarmInfo.DeleteFlag = 0;
            ptAlarmInfo.SendFlag = 0;
            ptAlarmInfo.VesselID = shinsa.MsVesselID;
            ptAlarmInfo.DataNo = 0;
            ptAlarmInfo.UserKey = "1";
            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.InsertRecord(new MsUser());
        }

        public static bool 検査証書_審査_内部審査_登録(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.審査日);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.内部審査);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), shinsa.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            NBaseData.DAC.PtAlarmInfo ptAlarmInfo = new NBaseData.DAC.PtAlarmInfo();

            ptAlarmInfo.PtAlarmInfoId = Guid.NewGuid().ToString();
            ptAlarmInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptAlarmInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptAlarmInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptAlarmInfo.SanshoumotoId = shinsa.KsShinsaID;
            ptAlarmInfo.HasseiDate = shinsa.NaibuAlarmDate;
            ptAlarmInfo.MsVesselId = shinsa.MsVesselID;
            ptAlarmInfo.Yuukoukigen = shinsa.NaibuDate;
            ptAlarmInfo.Naiyou = infoFormat.Naiyou;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, shinsa.MsShinsaName);
            ptAlarmInfo.AlarmShowFlag = 0;

            ptAlarmInfo.DeleteFlag = 0;
            ptAlarmInfo.SendFlag = 0;
            ptAlarmInfo.VesselID = shinsa.MsVesselID;
            ptAlarmInfo.DataNo = 0;
            ptAlarmInfo.UserKey = "1";
            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.InsertRecord(new MsUser());
        }

        public static bool 検査証書_審査_レビュー_登録(NBaseData.DAC.MsUser logiuser, KsShinsa shinsa)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.審査日);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.レビュー);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), shinsa.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            NBaseData.DAC.PtAlarmInfo ptAlarmInfo = new NBaseData.DAC.PtAlarmInfo();

            ptAlarmInfo.PtAlarmInfoId = Guid.NewGuid().ToString();
            ptAlarmInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptAlarmInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptAlarmInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptAlarmInfo.SanshoumotoId = shinsa.KsShinsaID;
            ptAlarmInfo.HasseiDate = shinsa.ReviewAlarmDate;
            ptAlarmInfo.MsVesselId = shinsa.MsVesselID;
            ptAlarmInfo.Yuukoukigen = shinsa.ReviewDate;
            ptAlarmInfo.Naiyou = infoFormat.Naiyou;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, shinsa.MsShinsaName);
            ptAlarmInfo.AlarmShowFlag = 0;

            ptAlarmInfo.DeleteFlag = 0;
            ptAlarmInfo.SendFlag = 0;
            ptAlarmInfo.VesselID = shinsa.MsVesselID;
            ptAlarmInfo.DataNo = 0;
            ptAlarmInfo.UserKey = "1";
            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.InsertRecord(new MsUser());
        }

        public static bool 検査証書_救命設備_登録1(NBaseData.DAC.MsUser logiuser, KsKyumeisetsubi kyumei)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.救命設備);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.点検);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), kyumei.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            NBaseData.DAC.PtAlarmInfo ptAlarmInfo = new NBaseData.DAC.PtAlarmInfo();

            ptAlarmInfo.PtAlarmInfoId = Guid.NewGuid().ToString();
            ptAlarmInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptAlarmInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptAlarmInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptAlarmInfo.SanshoumotoId = kyumei.KsKyumeisetsubiID;
            ptAlarmInfo.HasseiDate = kyumei.AlarmDate;
            ptAlarmInfo.MsVesselId = kyumei.MsVesselID;
            ptAlarmInfo.Yuukoukigen = kyumei.TenkenDate;
            ptAlarmInfo.Naiyou = infoFormat.Naiyou;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, kyumei.MsKyumeisetsubiName);
            ptAlarmInfo.AlarmShowFlag = 0;

            ptAlarmInfo.DeleteFlag = 0;
            ptAlarmInfo.SendFlag = 0;
            ptAlarmInfo.VesselID = kyumei.MsVesselID;
            ptAlarmInfo.DataNo = 0;
            ptAlarmInfo.UserKey = "1";
            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.InsertRecord(new MsUser());
        }

        public static bool 検査証書_荷役安全設備_登録1(NBaseData.DAC.MsUser logiuser, KsNiyaku niyaku)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.荷役安全設備);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.点検);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), niyaku.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            NBaseData.DAC.PtAlarmInfo ptAlarmInfo = new NBaseData.DAC.PtAlarmInfo();

            ptAlarmInfo.PtAlarmInfoId = Guid.NewGuid().ToString();
            ptAlarmInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptAlarmInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptAlarmInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptAlarmInfo.SanshoumotoId = niyaku.KsNiyakuID;
            ptAlarmInfo.HasseiDate = niyaku.AlarmDate;
            ptAlarmInfo.MsVesselId = niyaku.MsVesselID;
            ptAlarmInfo.Yuukoukigen = niyaku.TenkenDate;
            ptAlarmInfo.Naiyou = infoFormat.Naiyou;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, niyaku.MsNiyakusetsubiName);
            ptAlarmInfo.AlarmShowFlag = 0;

            ptAlarmInfo.DeleteFlag = 0;
            ptAlarmInfo.SendFlag = 0;
            ptAlarmInfo.VesselID = niyaku.MsVesselID;
            ptAlarmInfo.DataNo = 0;
            ptAlarmInfo.UserKey = "1";
            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.InsertRecord(new MsUser());
        }

        public static bool 検査証書_検船_登録1(NBaseData.DAC.MsUser logiuser, KsKensen kensen)
        {
            MsPortalInfoShubetu shubetu = MsPortalInfoShubetu.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoShubetu.検査証書);
            MsPortalInfoKoumoku koumoku = MsPortalInfoKoumoku.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKoumoku.検船);
            MsPortalInfoKubun kubun = MsPortalInfoKubun.GetRecordByPortalInfoSyubetuName(new MsUser(), MsPortalInfoKubun.検査);
            MsVessel vessel = MsVessel.GetRecordByMsVesselID(new MsUser(), kensen.MsVesselID);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(new MsUser(), shubetu.MsPortalInfoShubetuId, koumoku.MsPortalInfoKoumokuId, "");

            NBaseData.DAC.PtAlarmInfo ptAlarmInfo = new NBaseData.DAC.PtAlarmInfo();

            ptAlarmInfo.PtAlarmInfoId = Guid.NewGuid().ToString();
            ptAlarmInfo.MsPortalInfoShubetuId = shubetu.MsPortalInfoShubetuId;
            ptAlarmInfo.MsPortalInfoKoumokuId = koumoku.MsPortalInfoKoumokuId;
            ptAlarmInfo.MsPortalInfoKubunId = kubun.MsPortalInfoKubunId;
            ptAlarmInfo.SanshoumotoId = kensen.KsKensenID;
            ptAlarmInfo.HasseiDate = kensen.AlarmDate;
            ptAlarmInfo.MsVesselId = kensen.MsVesselID;
            ptAlarmInfo.Yuukoukigen = kensen.ShinsaDate;
            ptAlarmInfo.Naiyou = infoFormat.Naiyou;
            ptAlarmInfo.Shousai = String.Format(infoFormat.Shousai, vessel.VesselName, kensen.MsKensenName);
            ptAlarmInfo.AlarmShowFlag = 0;

            ptAlarmInfo.DeleteFlag = 0;
            ptAlarmInfo.SendFlag = 0;
            ptAlarmInfo.VesselID = kensen.MsVesselID;
            ptAlarmInfo.DataNo = 0;
            ptAlarmInfo.UserKey = "1";
            ptAlarmInfo.RenewDate = DateTime.Now;
            ptAlarmInfo.RenewUserID = logiuser.MsUserID;

            return ptAlarmInfo.InsertRecord(new MsUser());
        }
        #endregion
    }
}
