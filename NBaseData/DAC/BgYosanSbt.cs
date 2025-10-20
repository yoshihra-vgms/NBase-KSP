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
using NBaseData.DS;
using ORMapping.Atts;


namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("BG_YOSAN_SBT")]
    public class BgYosanSbt
    {
        //DBの中身
        //YOSAN_SBT_ID      NUMBER(2.0)
        //YOSAN_SBT_NAME    NVARCHAR2(10)

        //RENEW_DATE        DATE
        //RENEW_USER_ID     VARCHAR2(40)
        //TS                VARCHAR2(40)

        #region データメンバ

        /// <summary>
        /// 予算種別ＩＤ
        /// </summary>
        [DataMember]
        [ColumnAttribute("YOSAN_SBT_ID")]
        public int YosanSbtID { get; set; }

        /// <summary>
        /// 種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("YOSAN_SBT_NAME")]
        public string YosanSbtName { get; set; }


        /// <summary>
        /// 更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_DATE")]
        public DateTime RenewDate { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_USER_ID")]
        public string RenewUserID { get; set; }

        /// <summary>
        /// 排他制御
        /// </summary>
        [DataMember]
        [ColumnAttribute("TS")]
        public string Ts { get; set; }
        #endregion

        public enum BgYosanSbtEnum { 当初, 見直し, RevUp };
        private static readonly Dictionary<int, string> idToName =
                                                            new Dictionary<int, string>
                                                                {{(int)(BgYosanSbtEnum.当初), BgYosanSbtEnum.当初.ToString()},
                                                                 {(int)(BgYosanSbtEnum.見直し), BgYosanSbtEnum.見直し.ToString()},
                                                                };

        public static string ToName(int id)
        {
            return idToName[id];
        }

        public static int ToId(BgYosanSbtEnum en)
        {
            return (int)en;
        }

        public BgYosanSbt()
        {

        }
        /////////////////////////////////////////////////////////

        public static List<BgYosanSbt> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanSbt), MethodBase.GetCurrentMethod());
            
            List<BgYosanSbt> ret = new List<BgYosanSbt>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanSbt> mapping = new MappingBase<BgYosanSbt>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static BgYosanSbt GetRecord(MsUser loginUser, int yosan_sbt_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanSbt), MethodBase.GetCurrentMethod());

            List<BgYosanSbt> ret = new List<BgYosanSbt>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanSbt> mapping = new MappingBase<BgYosanSbt>();

            Params.Add(new DBParameter("YOSAN_SBT_ID", yosan_sbt_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanSbt), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YOSAN_SBT_ID", this.YosanSbtID));
            Params.Add(new DBParameter("YOSAN_SBT_NAME", this.YosanSbtName));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("TS", this.Ts));            
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
                        
            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanSbt), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();            
            Params.Add(new DBParameter("YOSAN_SBT_NAME", this.YosanSbtName));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));

            Params.Add(new DBParameter("YOSAN_SBT_ID", this.YosanSbtID));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;

        }
    }
}
