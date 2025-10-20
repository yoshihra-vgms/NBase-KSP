using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Attrs;
using ORMapping;
using SqlMapper;
using System.Reflection;
using System.Configuration;
using System.ServiceModel;

namespace NBaseData.BLC
{
    public class LoginSession
    {
        public static void 新規作成(DAC.MsUser user)
        {
            InsertRecord(user);
        }

        private static void InsertRecord(DAC.MsUser user)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(LoginSession), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SESSION_KEY", user.SessionKey));
            Params.Add(new DBParameter("MS_USER_ID", user.MsUserID));

            DBConnect.ExecuteNonQuery(user.MsUserID, SQL, Params);

        }

        public static void セッションキー認証(DAC.MsUser user)
        {
            string SQL = @"
select
count(*)  

from
LOGIN_SESSION
where SESSION_KEY = :SESSION_KEY
and RENEW_DATE > sysdate - :SECOND / 86400
";

            string UPDATE_SQL = @"
UPDATE LOGIN_SESSION set 
RENEW_DATE = sysdate
where SESSION_KEY = :SESSION_KEY
";

            int TimeOutSecond = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ログインセッション有効時間"]);
            decimal cnt = 0;
            using (DBConnect dbConnect = new DBConnect())
            {
                ParameterConnection Params = new ParameterConnection();
                Params.Add(new DBParameter("SESSION_KEY", user.SessionKey));
                Params.Add(new DBParameter("SECOND", TimeOutSecond));

                cnt = (decimal)DBConnect.ExecuteScalar(user.MsUserID, SQL, Params);


                if (cnt == 0)
                {
                    throw new FaultException("ログインセッションエラー", new FaultCode("ログインセッションエラー"));
                }

                //更新日の更新
                ParameterConnection Params2 = new ParameterConnection();
                Params2.Add(new DBParameter("SESSION_KEY", user.SessionKey));

                DBConnect.ExecuteNonQuery(user.MsUserID, UPDATE_SQL, Params2);
            }
        }

        
    }
}
