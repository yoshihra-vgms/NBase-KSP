using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


using Npgsql;
using CIsl.DB;
using DcCommon.DB.DAC.Search;
using CIsl.DB.WingDAC;
using DcCommon.DB.DAC;
using System.ServiceModel.Channels;

namespace WcfServiceDeficiencyControl.Logic
{
    /// <summary>
    /// 履歴作成
    /// </summary>
    public class OperationHistoryCreator
    {

        /// <summary>
        /// 接続ホストの文字列を取得する
        /// </summary>
        /// <returns></returns>
        public static string CreateHostString()
        {
            string ans = "";

            //IPとポートを取得する
            MessageProperties pro = OperationContext.Current.IncomingMessageProperties;

            RemoteEndpointMessageProperty rem = pro[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (rem != null)
            {
                ans = string.Format("{0}:{1}", rem.Address, rem.Port);
            }

            return ans;
        }


        /// <summary>
        /// データの挿入本体
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <param name="user">ユーザー</param>
        /// <param name="kind">種別</param>
        private static bool InsertOperationHistory(DBConnect cone, MsUser user, EUserOperationKind kind)
        {
            try
            {
                


                //データの挿入を行う
                DcOperationHistory ope = new DcOperationHistory();
                ope.ms_user_id = user.ms_user_id;
                ope.host = CreateHostString();
                ope.UserOperationKind = kind;
                ope.InsertRecord(cone.DBCone, user);

            }
            catch (Exception e)
            {
                //履歴をとるだけなのでエラーではないとする
                return false;

            }

            return true;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// ログイン処理
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="user"></param>
        public static bool Login(DBConnect cone, MsUser user)
        {
            return InsertOperationHistory(cone, user, EUserOperationKind.Login);

        }

        /// <summary>
        /// ログアウト処理
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="user"></param>
        public static bool Logout(DBConnect cone, MsUser user)
        {
            return InsertOperationHistory(cone, user, EUserOperationKind.Logout);

        }
    }
}
