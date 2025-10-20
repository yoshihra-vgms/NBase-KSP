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
    [TableAttribute("BG_NRK_KANRYOU")]
    public class BgNrkKanryou
    {
        //YOSAN_HEAD_ID                  NUMBER(9,0) NOT NULL,
        //MS_USER_BUMON_ID               VARCHAR2(40) NOT NULL,
        //NRK_KANRYO                     DATE,
        //NRK_KANRYOU_USER_ID            VARCHAR2(40),
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
        /// 入力完了日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("NRK_KANRYO")]
        public DateTime NrkKanryo { get; set; }


        /// <summary>
        /// 入力完了者ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("NRK_KANRYOU_USER_ID")]
        public string NrkKanryoUserID { get; set; }

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


        /// <summary>
        /// 部門名
        /// </summary>
        [DataMember]
        [ColumnAttribute("BUMON_NAME")]
        public string BumonName { get; set; }


        /// <summary>
        /// 入力完了者
        /// </summary>
        [DataMember]
        [ColumnAttribute("NRK_KANRYOU_USER_NAME")]
        public string NrkKanryouUserName { get; set; }
        
        #endregion

        public BgNrkKanryou()
        {
        }


        public static List<BgNrkKanryou> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgNrkKanryou), MethodBase.GetCurrentMethod());

            List<BgNrkKanryou> ret = new List<BgNrkKanryou>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgNrkKanryou> mapping = new MappingBase<BgNrkKanryou>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        //ユーザーマスタ
        public static List<BgNrkKanryou> GetRecordsByMsUserID(MsUser loginUser, string ms_user_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgNrkKanryou), MethodBase.GetCurrentMethod());

            List<BgNrkKanryou> ret = new List<BgNrkKanryou>();

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_USER_ID", ms_user_id));

            MappingBase<BgNrkKanryou> mapping = new MappingBase<BgNrkKanryou>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        
        public static List<BgNrkKanryou> GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgNrkKanryou), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgNrkKanryou), "ByYosanHeadID");

            List<BgNrkKanryou> ret = new List<BgNrkKanryou>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgNrkKanryou> mapping = new MappingBase<BgNrkKanryou>();

            Params.Add(new DBParameter(":YOSAN_HEAD_ID", yosanHeadId));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        
        public static BgNrkKanryou GetRecord(MsUser loginUser, int yosan_head_id, string ms_user_bumon_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgNrkKanryou), MethodBase.GetCurrentMethod());

            List<BgNrkKanryou> ret = new List<BgNrkKanryou>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgNrkKanryou> mapping = new MappingBase<BgNrkKanryou>();

            Params.Add(new DBParameter(":YOSAN_HEAD_ID", yosan_head_id));
            Params.Add(new DBParameter(":MS_BUMON_ID", ms_user_bumon_id));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgNrkKanryou), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("MS_BUMON_ID", this.MsBumonID));
            Params.Add(new DBParameter("NRK_KANRYO", this.NrkKanryo));
            Params.Add(new DBParameter("NRK_KANRYOU_USER_ID", this.NrkKanryoUserID));
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
            return UpdateRecord(null, loginUser);
        }

        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgNrkKanryou), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("NRK_KANRYO", this.NrkKanryo));
            Params.Add(new DBParameter("NRK_KANRYOU_USER_ID", this.NrkKanryoUserID));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));

            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("MS_BUMON_ID", this.MsBumonID));
            Params.Add(new DBParameter("TS", this.Ts));

            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }
    }
}
