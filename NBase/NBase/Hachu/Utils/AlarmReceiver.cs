using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseCommon.Alarm;
using Hachu.HachuManage;

namespace Hachu.Utils
{
    public class AlarmReceiver
    {
        public class 手配依頼Form起動 : IAlarmReceiver
        {
            public bool AlarmCall(NBaseData.DAC.PtAlarmInfo alarmInfo)
            {
                OdThi thi = null;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    thi = serviceClient.OdThi_GetRecord(NBaseCommon.Common.LoginUser, alarmInfo.SanshoumotoId);
                }

                if (thi == null)
                {
                    return false;
                }

                //フォーム起動
                手配依頼Form form = new 手配依頼Form((int)BaseForm.WINDOW_STYLE.通常, thi);
                //form.ShowDialog();

                return true;
            }
        }

        public class 見積回答Form起動 : IAlarmReceiver
        {
            public bool AlarmCall(NBaseData.DAC.PtAlarmInfo alarmInfo)
            {
                OdMk mk = null;
                OdMm mm = null;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    mk = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, alarmInfo.SanshoumotoId);
                    mm = serviceClient.OdMm_GetRecord(NBaseCommon.Common.LoginUser, mk.OdMmID);
                }

                if (mk == null || mm == null)
                {
                    return false;
                }

                //フォーム起動
                見積回答Form form = new 見積回答Form((int)BaseForm.WINDOW_STYLE.通常, mk, mm);
                //form.ShowDialog();

                return true;
            }
        }

        public class 承認Form起動 : IAlarmReceiver
        {
            public bool AlarmCall(NBaseData.DAC.PtAlarmInfo alarmInfo)
            {
                OdMk mk = null;
                OdMm mm = null;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    mk = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, alarmInfo.SanshoumotoId);
                    mm = serviceClient.OdMm_GetRecord(NBaseCommon.Common.LoginUser, mk.OdMmID);
                }

                if (mk == null || mm == null)
                {
                    return false;
                }

                //フォーム起動
                見積回答Form form = new 見積回答Form((int)BaseForm.WINDOW_STYLE.承認, mk, mm);
                //form.ShowDialog();

                return true;
            }
        }

        public class 受領Form起動 : IAlarmReceiver
        {
            public bool AlarmCall(NBaseData.DAC.PtAlarmInfo alarmInfo)
            {
                OdJry jry = null;
                OdMk mk = null;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    jry = serviceClient.OdJry_GetRecord(NBaseCommon.Common.LoginUser, alarmInfo.SanshoumotoId);
                    mk = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, jry.OdMkID);
                }

                if (jry == null || mk == null)
                {
                    return false;
                }

                //フォーム起動
                受領Form form = new 受領Form((int)BaseForm.WINDOW_STYLE.通常, jry, mk);
                //form.ShowDialog();

                return true;
            }
        }

        public class 支払Form起動 : IAlarmReceiver
        {
            public bool AlarmCall(NBaseData.DAC.PtAlarmInfo alarmInfo)
            {
                OdShr shr = null;
                OdJry jry = null;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    shr = serviceClient.OdShr_GetRecord(NBaseCommon.Common.LoginUser, alarmInfo.SanshoumotoId);
                    jry = serviceClient.OdJry_GetRecord(NBaseCommon.Common.LoginUser, shr.OdJryID);
                }

                if (shr == null || jry == null)
                {
                    return false;
                }

                //フォーム起動
                支払Form form = new 支払Form((int)BaseForm.WINDOW_STYLE.通常, shr, jry);
                //form.ShowDialog();

                return true;
            }
        }
    }
}
