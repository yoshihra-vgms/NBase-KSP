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
    [TableAttribute("BG_YOSAN_HEAD")]
    public class BgYosanHead
    {
        //YOSAN_HEAD_ID                  NUMBER(9,0) NOT NULL,
        //YEAR                           NUMBER(4,0) NOT NULL,
        //YOSAN_SBT_ID                   NUMBER(2,0),
        //REVISION                       NUMBER(3,0),
        //FIX_DATE                       DATE,
        //MS_USER_ID                     VARCHAR2(40),
        //ZENTEI_JOUKEN                  NVARCHAR2(2000),
        //REVISION_BIKOU                 NVARCHAR2(1000),
        

        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),
        #region データメンバ

        /// <summary>
        /// 予算ヘッダＩＤ
        /// </summary>
        [DataMember]
        [ColumnAttribute("YOSAN_HEAD_ID")]
        public int YosanHeadID { get; set; }


        /// <summary>
        /// 年度
        /// </summary>
        [DataMember]
        [ColumnAttribute("YEAR")]
        public int Year { get; set; }

        /// <summary>
        /// 予算種別ＩＤ
        /// </summary>
        [DataMember]
        [ColumnAttribute("YOSAN_SBT_ID")]
        public int YosanSbtID { get; set; }


        /// <summary>
        /// リビジョン
        /// </summary>
        [DataMember]
        [ColumnAttribute("REVISION")]
        public int Revision { get; set; }


        /// <summary>
        /// Fix日
        /// </summary>
        [DataMember]
        [ColumnAttribute("FIX_DATE")]
        public DateTime FixDate { get; set; }
        
        /// <summary>
        /// 予算FixユーザーID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_USER_ID")]        
        public string MsUserID { get; set; }

        /// <summary>
        /// 前提条件
        /// </summary>
        [DataMember]
        [ColumnAttribute("ZENTEI_JOUKEN")]
        public string ZenteiJouken { get; set; }

        /// <summary>
        /// リビジョン備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("REVISION_BIKOU")]
        public string RevisionBikou { get; set; }
        
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

        public BgYosanHead()
        {
        }


        public static List<BgYosanHead> GetRecords(MsUser loginUser)
        {
            return GetRecords(null, loginUser);
        }

        public static List<BgYosanHead> GetRecords(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanHead), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgYosanHead), "OrderBy");

            List<BgYosanHead> ret = new List<BgYosanHead>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanHead> mapping = new MappingBase<BgYosanHead>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //ユーザーマスタID
        public static List<BgYosanHead> GetRecordsByMsUserID(DBConnect dbConnect, MsUser loginUser, string ms_user_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanHead), MethodBase.GetCurrentMethod());
            

            List<BgYosanHead> ret = new List<BgYosanHead>();

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_USER_ID", ms_user_id));

            MappingBase<BgYosanHead> mapping = new MappingBase<BgYosanHead>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static BgYosanHead GetRecord(MsUser loginUser, int yosan_head_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanHead), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgYosanHead), "ByYosanHeadId");

            List<BgYosanHead> ret = new List<BgYosanHead>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanHead> mapping = new MappingBase<BgYosanHead>();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosan_head_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }

        public static BgYosanHead GetRecordByYear(MsUser loginUser, string year)
        {
            return GetRecordByYear(null, loginUser, year);
        }

        public static BgYosanHead GetRecordByYear(DBConnect dbConnect, MsUser loginUser, string year)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanHead), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgYosanHead), "ByYear");

            List<BgYosanHead> ret = new List<BgYosanHead>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanHead> mapping = new MappingBase<BgYosanHead>();

            Params.Add(new DBParameter("YEAR", year));

            ret = mapping.FillRecrods(dbConnect,loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }


        //public static BgYosanHead GetRecord_直近(MsUser loginUser, int yosanSbtId)
        public static BgYosanHead GetRecord_直近(MsUser loginUser)
        {
            //return GetRecord_直近(null, loginUser, yosanSbtId);
            return GetRecord_直近(null, loginUser);
        }


        //public static BgYosanHead GetRecord_直近(DBConnect dbConnect, MsUser loginUser, int yosanSbtId)
        public static BgYosanHead GetRecord_直近(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanHead), "GetRecords");
            //SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgYosanHead), "_直近");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgYosanHead), "OrderBy");

            List<BgYosanHead> ret = new List<BgYosanHead>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanHead> mapping = new MappingBase<BgYosanHead>();

            // 2013.01 : 予算IDがシーケンスなので、種別を見る必要はない
            //Params.Add(new DBParameter("YOSAN_SBT_ID", yosanSbtId));

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }


        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanHead), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YEAR", this.Year));
            Params.Add(new DBParameter("YOSAN_SBT_ID", this.YosanSbtID));
            Params.Add(new DBParameter("REVISION", this.Revision));
            Params.Add(new DBParameter("FIX_DATE", this.FixDate));
            Params.Add(new DBParameter("MS_USER_ID", this.MsUserID));
            Params.Add(new DBParameter("ZENTEI_JOUKEN", this.ZenteiJouken));
            Params.Add(new DBParameter("REVISION_BIKOU", this.RevisionBikou));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("TS", this.Ts));

            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }


        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanHead), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YEAR", this.Year));
            Params.Add(new DBParameter("YOSAN_SBT_ID", this.YosanSbtID));
            Params.Add(new DBParameter("REVISION", this.Revision));
            Params.Add(new DBParameter("FIX_DATE", this.FixDate));
            Params.Add(new DBParameter("MS_USER_ID", this.MsUserID));
            Params.Add(new DBParameter("ZENTEI_JOUKEN", this.ZenteiJouken));
            Params.Add(new DBParameter("REVISION_BIKOU", this.RevisionBikou));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));


            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("TS", this.Ts));

            #endregion

            int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }


        public bool IsFixed()
        {
            if (NBaseUtil.Common.Gray())
            {
                return false;
            }

            return FixDate != DateTime.MinValue;
        }
    }   
}
