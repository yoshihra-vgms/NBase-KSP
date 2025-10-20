using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ORMapping;

namespace NBaseData.BLC
{
    public class 発注アラーム処理
    {
        private enum CancelFlagEnum { ＯＦＦ, ＯＮ };
        private enum AlarmShowFlagEnum { アラームＯＮ, アラームＯＦＦ };

        private static string 新規ID()
        {
            return System.Guid.NewGuid().ToString();
        }

        public static bool 手配アラーム処理(DBConnect dbConnect, MsUser loginUser, OdThi odThi)
        {
            bool ret = true;
            PtAlarmInfo alarm = null;

            try
            {
                alarm = PtAlarmInfo.GetRecordsBySanshoumotoId(dbConnect, loginUser, odThi.OdThiID)[0];
                MsAlarmDate alarmDate = MsAlarmDate.GetRecord(dbConnect, loginUser, MsAlarmDate.MsAlarmDateIDNo.手配依頼アラームID);

                // ＤＢから取れた場合、アラーム情報を更新する
                alarm.HasseiDate = odThi.ThiIraiDate.AddDays(alarmDate.DayOffset);

                if (odThi.CancelFlag == (int)CancelFlagEnum.ＯＮ)
                {
                    ret = アラーム削除(dbConnect, loginUser, alarm);
                }
                else
                {
                    ret = 手配アラーム更新(dbConnect, loginUser, odThi, alarm);
                }
            }
            catch
            {
                // ＤＢから取れない場合、新規にアラーム情報を追加する
                alarm = new PtAlarmInfo();
                ret = 手配アラーム登録(dbConnect, loginUser, odThi, alarm);
            }

            return ret;
        }

        private static bool 手配アラーム登録(DBConnect dbConnect, MsUser loginUser, OdThi odThi, PtAlarmInfo alarm)
        {
            MsAlarmDate alarmDate = MsAlarmDate.GetRecord(dbConnect, loginUser, MsAlarmDate.MsAlarmDateIDNo.手配依頼アラームID);
            alarm.PtAlarmInfoId = 新規ID();
            alarm.AlarmShowFlag = (int)AlarmShowFlagEnum.アラームＯＮ;

            alarm.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString();
            alarm.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.手配依頼).ToString();
            alarm.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未対応).ToString();
            alarm.SanshoumotoId =odThi.OdThiID;
            //alarm.Yuukoukigen = null;
            alarm.HasseiDate = odThi.ThiIraiDate.AddDays(alarmDate.DayOffset);
            alarm.MsVesselId = odThi.MsVesselID;
            alarm.VesselName = odThi.VesselName;

            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, alarm.MsPortalInfoShubetuId, alarm.MsPortalInfoKoumokuId, "");
            alarm.Naiyou = infoFormat.Naiyou;
            alarm.Shousai = String.Format(infoFormat.Shousai, odThi.VesselName, odThi.TehaiIraiNo, odThi.Naiyou);

            return アラーム登録(dbConnect, loginUser, alarm);
        }

        private static bool 手配アラーム更新(DBConnect dbConnect, MsUser loginUser, OdThi odThi, PtAlarmInfo alarm)
        {
            MsAlarmDate alarmDate = MsAlarmDate.GetRecord(dbConnect, loginUser, MsAlarmDate.MsAlarmDateIDNo.手配依頼アラームID);
            alarm.HasseiDate = odThi.ThiIraiDate.AddDays(alarmDate.DayOffset);
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, alarm.MsPortalInfoShubetuId, alarm.MsPortalInfoKoumokuId, "");
            alarm.Naiyou = infoFormat.Naiyou;
            alarm.Shousai = String.Format(infoFormat.Shousai, odThi.VesselName, odThi.TehaiIraiNo, odThi.Naiyou);
            alarm.RenewUserID = loginUser.MsUserID;
            alarm.RenewDate = DateTime.Now;

            return alarm.UpdateRecord(dbConnect, loginUser);
        }

        public static bool 手配アラーム停止(DBConnect dbConnect, MsUser loginUser, string odThiId)
        {
            bool ret = true;
            try
            {
                PtAlarmInfo alarm = PtAlarmInfo.GetRecordsBySanshoumotoId(dbConnect, loginUser, odThiId)[0];
                ret = アラーム停止(dbConnect, loginUser, alarm);
            }
            catch
            {
            }
            return ret;
        }

        public static bool 手配アラーム削除(DBConnect dbConnect, MsUser loginUser, string odThiId)
        {
            bool ret = true;
            try
            {
                List<PtAlarmInfo> thiAlarms = PtAlarmInfo.GetRecordsBySanshoumotoId(dbConnect, loginUser, odThiId);
                if (thiAlarms != null)
                {
                    foreach (PtAlarmInfo alarm in thiAlarms)
                    {
                        ret = アラーム削除(dbConnect, loginUser, alarm);
                    }
                }

                // ぶら下がっているOD_MKがある場合、そのアラームも削除する
                List<OdMk> odMks = OdMk.GetRecordsByOdThiID(dbConnect, loginUser, odThiId);
                foreach (OdMk odmk in odMks)
                {
                    try
                    {
                        List<PtAlarmInfo> alarms = PtAlarmInfo.GetRecordsBySanshoumotoId(dbConnect, loginUser, odmk.OdMkID);
                        foreach (PtAlarmInfo alarm in alarms)
                        {
                            ret = アラーム削除(dbConnect, loginUser, alarm);
                        }
                    }
                    catch
                    {
                    }
                }
                // ぶら下がっているOD_JRYがある場合、そのアラームも削除する
                List<OdJry> odJrys = OdJry.GetRecordsByOdThiId(dbConnect, loginUser, odThiId);
                foreach (OdJry odjry in odJrys)
                {
                    try
                    {
                        List<PtAlarmInfo> alarms = PtAlarmInfo.GetRecordsBySanshoumotoId(dbConnect, loginUser, odjry.OdJryID);
                        foreach (PtAlarmInfo alarm in alarms)
                        {
                            ret = アラーム削除(dbConnect, loginUser, alarm);
                        }
                    }
                    catch
                    {
                    }
                }
                // ぶら下がっているOD_SHRがある場合、そのアラームも削除する
                List<OdShr> odShrs = OdShr.GetRecordsByOdThiId(dbConnect, loginUser, odThiId);
                foreach (OdShr odshr in odShrs)
                {
                    try
                    {
                        List<PtAlarmInfo> alarms = PtAlarmInfo.GetRecordsBySanshoumotoId(dbConnect, loginUser, odshr.OdShrID);
                        foreach (PtAlarmInfo alarm in alarms)
                        {
                            ret = アラーム削除(dbConnect, loginUser, alarm);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
            return ret;
        }

        public static bool 見積回答アラーム登録(DBConnect dbConnect, MsUser loginUser, OdMk odMk)
        {
            PtAlarmInfo alarm = new PtAlarmInfo();
            MsAlarmDate alarmDate = MsAlarmDate.GetRecord(dbConnect, loginUser, MsAlarmDate.MsAlarmDateIDNo.見積中アラームID);

            alarm.PtAlarmInfoId = 新規ID();
            alarm.AlarmShowFlag = (int)AlarmShowFlagEnum.アラームＯＮ;

            alarm.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString();
            alarm.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.見積回答).ToString();
            alarm.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未回答).ToString();
            alarm.SanshoumotoId = odMk.OdMkID;
            //alarm.Yuukoukigen = null;
            alarm.HasseiDate = odMk.MmDate.AddDays(alarmDate.DayOffset);
            OdMk tmpOdMk = OdMk.GetRecord(dbConnect, loginUser, odMk.OdMkID);
            alarm.MsVesselId = tmpOdMk.MsVesselID;
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, alarm.MsPortalInfoShubetuId, alarm.MsPortalInfoKoumokuId, alarm.MsPortalInfoKubunId);
            alarm.Naiyou = infoFormat.Naiyou;
            alarm.Shousai = String.Format(infoFormat.Shousai, tmpOdMk.MsVesselName, odMk.MkNo, tmpOdMk.OdThiNaiyou);

            return アラーム登録(dbConnect, loginUser, alarm);
        }

        public static bool 見積回答アラーム停止(DBConnect dbConnect, MsUser loginUser, string odMkId)
        {
            PtAlarmInfo alarm = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(dbConnect, loginUser, odMkId,
                ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.見積回答).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未回答).ToString());

            if (alarm == null)
            {
                return false;
            }
            return アラーム停止(dbConnect, loginUser, alarm);
        }

        public static bool 発注承認アラーム登録(DBConnect dbConnect, MsUser loginUser, OdMk odMk)
        {
            PtAlarmInfo alarm = new PtAlarmInfo();
            MsAlarmDate alarmDate = MsAlarmDate.GetRecord(dbConnect, loginUser, MsAlarmDate.MsAlarmDateIDNo.承認依頼アラームID);

            alarm.PtAlarmInfoId = 新規ID();
            alarm.AlarmShowFlag = (int)AlarmShowFlagEnum.アラームＯＮ;

            alarm.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString();
            alarm.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.承認).ToString();
            alarm.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未承認).ToString();
            alarm.SanshoumotoId = odMk.OdMkID;
            //alarm.Yuukoukigen = null;
            alarm.HasseiDate = DateTime.Today.AddDays(alarmDate.DayOffset);
            alarm.MsVesselId = odMk.MsVesselID;
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, alarm.MsPortalInfoShubetuId, alarm.MsPortalInfoKoumokuId, alarm.MsPortalInfoKubunId);
            alarm.Naiyou = infoFormat.Naiyou;
            alarm.Shousai = String.Format(infoFormat.Shousai, odMk.MsVesselName, odMk.MkNo, odMk.OdThiNaiyou);

            return アラーム登録(dbConnect, loginUser, alarm);
        }

        public static bool 発注承認アラーム停止(DBConnect dbConnect, MsUser loginUser, string odMkId)
        {
            PtAlarmInfo alarm = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(dbConnect, loginUser, odMkId,
                ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.承認).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未承認).ToString());

            if (alarm == null)
            {
                return false;
            }
            return アラーム停止(dbConnect, loginUser, alarm);
        }

        public static bool 発注アラーム登録(DBConnect dbConnect, MsUser loginUser, OdMk odMk)
        {
            PtAlarmInfo alarm = new PtAlarmInfo();
            MsAlarmDate alarmDate = MsAlarmDate.GetRecord(dbConnect, loginUser, MsAlarmDate.MsAlarmDateIDNo.承認済みアラームID);

            alarm.PtAlarmInfoId = 新規ID();
            alarm.AlarmShowFlag = (int)AlarmShowFlagEnum.アラームＯＮ;

            alarm.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString();
            alarm.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.承認).ToString();
            alarm.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未発注).ToString();
            alarm.SanshoumotoId = odMk.OdMkID;
            //alarm.Yuukoukigen = null;
            alarm.HasseiDate = DateTime.Today.AddDays(alarmDate.DayOffset);
            alarm.MsVesselId = odMk.MsVesselID;
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, alarm.MsPortalInfoShubetuId, alarm.MsPortalInfoKoumokuId, alarm.MsPortalInfoKubunId);
            alarm.Naiyou = infoFormat.Naiyou;
            alarm.Shousai = String.Format(infoFormat.Shousai, odMk.MsVesselName, odMk.MkNo, odMk.OdThiNaiyou);

            return アラーム登録(dbConnect, loginUser, alarm);
        }

        public static bool 発注アラーム停止(DBConnect dbConnect, MsUser loginUser, string odMkId)
        {
            PtAlarmInfo alarm = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(dbConnect, loginUser, odMkId,
                ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.承認).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未発注).ToString());

            if (alarm == null)
            {
                return false;
            }
            return アラーム停止(dbConnect, loginUser, alarm);
        }

        public static bool 受領アラーム登録(DBConnect dbConnect, MsUser loginUser, OdJry odJry)
        {
            PtAlarmInfo alarm = new PtAlarmInfo();
            MsAlarmDate alarmDate = MsAlarmDate.GetRecord(dbConnect, loginUser, MsAlarmDate.MsAlarmDateIDNo.発注済みアラームID);

            alarm.PtAlarmInfoId = 新規ID();
            alarm.AlarmShowFlag = (int)AlarmShowFlagEnum.アラームＯＮ;

            alarm.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString();
            alarm.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.発注).ToString();
            alarm.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未受領).ToString();
            alarm.SanshoumotoId = odJry.OdJryID;
            //alarm.Yuukoukigen = null;
            alarm.HasseiDate = DateTime.Today.AddDays(alarmDate.DayOffset);
            OdMk odMk = OdMk.GetRecord(dbConnect, loginUser, odJry.OdMkID);
            alarm.MsVesselId = odMk.MsVesselID;
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, alarm.MsPortalInfoShubetuId, alarm.MsPortalInfoKoumokuId, alarm.MsPortalInfoKubunId);
            alarm.Naiyou = infoFormat.Naiyou;
            alarm.Shousai = String.Format(infoFormat.Shousai, odMk.MsVesselName, odJry.JryNo, odMk.OdThiNaiyou);

            return アラーム登録(dbConnect, loginUser, alarm);
        }

        public static bool 受領アラーム停止(DBConnect dbConnect, MsUser loginUser, string odJryId)
        {
            PtAlarmInfo alarm = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(dbConnect, loginUser, odJryId,
                ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.発注).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未受領).ToString());

            if (alarm == null)
            {
                return false;
            }
            return アラーム停止(dbConnect, loginUser, alarm);
        }

        public static bool 受領アラーム削除(DBConnect dbConnect, MsUser loginUser, string odJryId)
        {
            PtAlarmInfo alarm = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(dbConnect, loginUser, odJryId,
                ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.発注).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未受領).ToString());

            if (alarm == null)
            {
                return false;
            }
            return アラーム削除(dbConnect, loginUser, alarm);
        }

        public static bool 支払作成アラーム登録(DBConnect dbConnect, MsUser loginUser, OdJry odJry)
        {
            PtAlarmInfo alarm = new PtAlarmInfo();
            MsAlarmDate alarmDate = MsAlarmDate.GetRecord(dbConnect, loginUser, MsAlarmDate.MsAlarmDateIDNo.受領済みアラームID);

            alarm.PtAlarmInfoId = 新規ID();
            alarm.AlarmShowFlag = (int)AlarmShowFlagEnum.アラームＯＮ;

            alarm.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString();
            alarm.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.受領).ToString();
            alarm.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未作成).ToString();
            alarm.SanshoumotoId = odJry.OdJryID;
            //alarm.Yuukoukigen = null;
            alarm.HasseiDate = DateTime.Today.AddDays(alarmDate.DayOffset);
            OdMk odMk = OdMk.GetRecord(dbConnect, loginUser, odJry.OdMkID);
            alarm.MsVesselId = odMk.MsVesselID;
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, alarm.MsPortalInfoShubetuId, alarm.MsPortalInfoKoumokuId, alarm.MsPortalInfoKubunId);
            alarm.Naiyou = infoFormat.Naiyou;
            alarm.Shousai = String.Format(infoFormat.Shousai, odMk.MsVesselName, odJry.JryNo, odMk.OdThiNaiyou);

            return アラーム登録(dbConnect, loginUser, alarm);
        }

        public static bool 支払作成アラーム停止(DBConnect dbConnect, MsUser loginUser, string odJryId)
        {
            PtAlarmInfo alarm = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(dbConnect, loginUser, odJryId,
                ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.受領).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未作成).ToString());

            if (alarm == null)
            {
                return false;
            }
            return アラーム停止(dbConnect, loginUser, alarm);
        }

        public static bool 支払作成アラーム削除(DBConnect dbConnect, MsUser loginUser, string odJryId)
        {
            PtAlarmInfo alarm = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(dbConnect, loginUser, odJryId,
                ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.受領).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未作成).ToString());

            if (alarm == null)
            {
                return false;
            }
            return アラーム削除(dbConnect, loginUser, alarm);
        }

        public static bool 支払依頼アラーム登録(DBConnect dbConnect, MsUser loginUser, OdShr odShr)
        {
            PtAlarmInfo alarm = new PtAlarmInfo();
            MsAlarmDate alarmDate = MsAlarmDate.GetRecord(dbConnect, loginUser, MsAlarmDate.MsAlarmDateIDNo.支払作成済みアラームID);

            alarm.PtAlarmInfoId = 新規ID();
            alarm.AlarmShowFlag = (int)AlarmShowFlagEnum.アラームＯＮ;

            alarm.MsPortalInfoShubetuId = ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString();
            alarm.MsPortalInfoKoumokuId = ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.支払).ToString();
            alarm.MsPortalInfoKubunId = ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未払).ToString();
            alarm.SanshoumotoId = odShr.OdShrID;
            //alarm.Yuukoukigen = null;
            alarm.HasseiDate = DateTime.Today.AddDays(alarmDate.DayOffset);
            OdJry odJry = OdJry.GetRecord(dbConnect, loginUser, odShr.OdJryID);
            OdMk odMk = OdMk.GetRecord(dbConnect, loginUser, odJry.OdMkID);
            alarm.MsVesselId = odMk.MsVesselID;
            PtPortalInfoFormat infoFormat = PtPortalInfoFormat.GetRecordByShubet_Koumoku_Kubun(dbConnect, loginUser, alarm.MsPortalInfoShubetuId, alarm.MsPortalInfoKoumokuId, alarm.MsPortalInfoKubunId);
            alarm.Naiyou = infoFormat.Naiyou;
            alarm.Shousai = String.Format(infoFormat.Shousai, odMk.MsVesselName, odShr.ShrNo, odMk.OdThiNaiyou);

            return アラーム登録(dbConnect, loginUser, alarm);
        }

        public static bool 支払依頼アラーム停止(DBConnect dbConnect, MsUser loginUser, string odShrId)
        {
            PtAlarmInfo alarm = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(dbConnect, loginUser, odShrId,
                ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.支払).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未払).ToString());

            if (alarm == null)
            {
                return false;
            }
            return アラーム停止(dbConnect, loginUser, alarm);
        }

        public static bool 支払依頼アラーム削除(DBConnect dbConnect, MsUser loginUser, string odShrId)
        {
            PtAlarmInfo alarm = PtAlarmInfo.GetRecordsBySanshoumotoId_shubetu_koumoku_kubun(dbConnect, loginUser, odShrId,
                ((int)NBaseData.DAC.MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.発注).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.支払).ToString(),
                ((int)NBaseData.DAC.MsPortalInfoKubun.MsPortalInfoKubunIdEnum.未払).ToString());

            if (alarm == null)
            {
                return false;
            }
            return アラーム削除(dbConnect, loginUser, alarm);
        }


        private static bool アラーム登録(DBConnect dbConnect, MsUser loginUser, PtAlarmInfo alarm)
        {
            alarm.AlarmShowFlag = (int)AlarmShowFlagEnum.アラームＯＮ;
            alarm.VesselID = alarm.MsVesselId;
            alarm.RenewUserID = loginUser.MsUserID;
            alarm.RenewDate = DateTime.Now;

            return alarm.InsertRecord(dbConnect, loginUser);
        }
        private static bool アラーム停止(DBConnect dbConnect, MsUser loginUser, PtAlarmInfo alarm)
        {
            alarm.AlarmShowFlag = (int)AlarmShowFlagEnum.アラームＯＦＦ;
            alarm.AlarmStopDate = DateTime.Now;
            alarm.AlarmStopUser = loginUser.MsUserID;

            alarm.RenewUserID = loginUser.MsUserID;
            alarm.RenewDate = DateTime.Now;

            return alarm.UpdateRecord(dbConnect, loginUser);
        }
        private static bool アラーム削除(DBConnect dbConnect, MsUser loginUser, PtAlarmInfo alarm)
        {
            alarm.DeleteFlag = (int)CancelFlagEnum.ＯＮ;
            alarm.RenewUserID = loginUser.MsUserID;
            alarm.RenewDate = DateTime.Now;

            return alarm.UpdateRecord(dbConnect, loginUser);
        }
    }
}
