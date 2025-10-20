using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using SqlMapper;
using System.Reflection;
using NBaseData.DAC;
using NBaseData.DS;
using ORMapping.Atts;

namespace NBaseData.BLC
{
    public class ユーザパスワード
    {
        public const int チェック世代数 = 3;

        public enum STATUS
        {
            正常,
            履歴重複
        };

        public STATUS パスワード変更(MsUser loginUser, MsUser ChangeUser, string Password)
        {
            if (履歴チェック(loginUser, ChangeUser, Password) <= チェック世代数)
            {
                return STATUS.履歴重複;
            }

            MsUserPassHis msUserPassHis = new MsUserPassHis();

            msUserPassHis.MsUserPassHisID = Guid.NewGuid().ToString();
            msUserPassHis.MsUserID = ChangeUser.MsUserID;
            msUserPassHis.Password = Password;

            msUserPassHis.InsertRecord(loginUser);
            ChangeUser.Password = Password;
            ChangeUser.UpdateRecord(loginUser);

            return STATUS.正常;
        }


        /// <summary>
        /// 設定するパスワードが履歴にないか調べ、合った場合は世代数を返す
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="ChangeUser"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        private int 履歴チェック(MsUser loginUser, MsUser ChangeUser, string Password)
        {
            List<MsUserPassHis> msUserPassHis = MsUserPassHis.GetRecords(loginUser, ChangeUser);

            for (int i = 0; i < msUserPassHis.Count; i++)
            {
                if (msUserPassHis[i].Password == Password)
                {
                    return i++;
                }
            }

            return int.MaxValue;
        }
    }
}
