using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseCommon.Alarm;

namespace Senin.util
{
    public class AlarmReceiver
    {
        public class 免状免許詳細起動 : IAlarmReceiver
        {
            public bool AlarmCall(NBaseData.DAC.PtAlarmInfo alarmInfo)
            {
                SiMenjou menjou = null;
                MsSenin senin = null;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    menjou = serviceClient.SiMenjou_GetRecord(NBaseCommon.Common.LoginUser, alarmInfo.SanshoumotoId);
                    senin = serviceClient.MsSenin_GetRecord(NBaseCommon.Common.LoginUser, menjou.MsSeninID);
                }

                if (menjou == null)
                {
                    return false;
                }

                menjou.AlarmInfoList = new List<PtAlarmInfo>();
                menjou.AlarmInfoList.Add(alarmInfo);
                menjou.AlarmInfoList.Add(new PtAlarmInfo());

                //フォーム起動
                免状免許詳細Form form = new 免状免許詳細Form(senin.Sei + " " + senin.Mei, menjou, false);
                form.ShowDialog();

                return true;
            }
        }
    }
}
