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
    [TableAttribute("BG_YOSAN_MEMO")]
    public class BgYosanMemo
    {
        //YOSAN_HEAD_ID                  NUMBER(9,0) NOT NULL,
        //MS_USER_BUMON_ID               VARCHAR2(40) NOT NULL,
        //MEMO                           NVARCHAR2(2000),
        
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),

        #region データメンバ

        /// <summary>
        /// 予算ヘッダID
        /// </summary>
        [DataMember]
        [ColumnAttribute("YOSAN_HEAD_ID")]
        public int YosanHeadID { get; set; }

        /// <summary>
        /// 部門ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BUMON_ID")]
        public string MsBumonID { get; set; }

        /// <summary>
        /// メモ
        /// </summary>
        [DataMember]
        [ColumnAttribute("MEMO")]
        public string Memo { get; set; }


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


        public BgYosanMemo()
        {
        }


        public static List<BgYosanMemo> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanMemo), MethodBase.GetCurrentMethod());

            List<BgYosanMemo> ret = new List<BgYosanMemo>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanMemo> mapping = new MappingBase<BgYosanMemo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<BgYosanMemo> GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanMemo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgYosanMemo), "ByYosanHeadID");

            List<BgYosanMemo> ret = new List<BgYosanMemo>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanMemo> mapping = new MappingBase<BgYosanMemo>();

            Params.Add(new DBParameter(":YOSAN_HEAD_ID", yosanHeadId));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        
        public static BgYosanMemo GetRecord(MsUser loginUser, int yosan_head_id, string ms_user_bumon_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanMemo), MethodBase.GetCurrentMethod());

            List<BgYosanMemo> ret = new List<BgYosanMemo>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgYosanMemo> mapping = new MappingBase<BgYosanMemo>();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosan_head_id));
            Params.Add(new DBParameter("MS_BUMON_ID", ms_user_bumon_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanMemo), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("MS_BUMON_ID", this.MsBumonID));
            Params.Add(new DBParameter("MEMO", this.Memo));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanMemo), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            
            Params.Add(new DBParameter("MEMO", this.Memo));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            

            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("MS_BUMON_ID", this.MsBumonID));
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
