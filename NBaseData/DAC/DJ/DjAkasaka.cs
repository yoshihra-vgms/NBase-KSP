using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using NBaseData.DAC;
using NBaseData.DS;
namespace NBaseData.DAC.DJ
{
    [DataContract()]
    public class DjAkasaka
    {
        #region プロパティ
        /// <summary>
        /// データ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA")]
        public string Data { get; set; }

        #endregion

        public static DjAkasaka GetRecord(MsUser loginUser,int DJAkasakaID)
        {
            List<DjAkasaka> ret = new List<DjAkasaka>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgHankanhi), "GetRecord");

            ParameterConnection Params = new ParameterConnection();
            MappingBase<DjAkasaka> mapping = new MappingBase<DjAkasaka>();
            Params.Add(new DBParameter("DJ_AKASAKA_ID", DJAkasakaID));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count > 0)
            {
                return ret[0];
            }
            return null;
        }

        /// <summary>
        /// 通信データをDBに保存する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="NaviCode"></param>
        /// <param name="RawData"></param>
        /// <returns></returns>
        public static bool InsertRecord(MsUser loginUser,string NaviCode,string RawData)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(DjAkasaka), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("NAVICODE", NaviCode));
            Params.Add(new DBParameter("DATA", RawData));
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

            return true;
        }
    }
}
