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
    [TableAttribute("MS_HIMOKU")]
    public class MsHimoku
    {
        //MS_HIMOKU_ID                   NUMBER(9,0) NOT NULL,
        //HIMOKU_NAME                    NVARCHAR2(30),
        //MS_BUMON_ID                    VARCHAR2(40),
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),
        #region データメンバ

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_HIMOKU_ID")]
        public int MsHimokuID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("HIMOKU_NAME")]
        public string HimokuName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BUMON_ID")]
        public string MsBumonID { get; set; }
        
        /// <summary>
        /// 基幹費目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIKAN_HIMOKU_ID")]
        public string KikanHimokuID { get; set; }

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
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KAMOKU_ID")]
        public int MsKamokuID  { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("FUGOU")]
        public int Fugou { get; set; }
        #endregion

        /// <summary>
        /// 手配依頼詳細品目
        /// </summary>
        [DataMember]
        private List<MsKamoku> msKamokus;
        public List<MsKamoku> MsKamokus
        {
            get
            {
                if (msKamokus == null)
                {
                    msKamokus = new List<MsKamoku>();
                }

                return msKamokus;
            }
        }

        
        public MsHimoku()
        {
        }
        
        
        public static List<MsHimoku> GetRecords(MsUser loginUser)
        {
            return GetRecords(null, loginUser);
        }


        public static List<MsHimoku> GetRecords(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsHimoku), MethodBase.GetCurrentMethod());

            List<MsHimoku> ret = new List<MsHimoku>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsHimoku> mapping = new MappingBase<MsHimoku>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsHimoku> GetRecordsWithMsKamoku(MsUser loginUser)
        {
            return GetRecordsWithMsKamoku(null, loginUser);
        }
        public static List<MsHimoku> GetRecordsWithMsKamoku(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsHimoku), MethodBase.GetCurrentMethod());

            List<MsHimoku> ret = new List<MsHimoku>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsHimoku> mapping = new MappingBase<MsHimoku>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return BuildRecords(ret);
        }

        
        private static List<MsHimoku> BuildRecords(List<MsHimoku> himokus)
        {
            var himokuDic = new Dictionary<int, MsHimoku>();
            
            foreach (MsHimoku h in himokus)
            {
                if (!himokuDic.ContainsKey(h.MsHimokuID))
                {
                    himokuDic[h.MsHimokuID] = h;
                }
            
                if(h.MsKamokuID > 0)
                {
                    MsKamoku k = new MsKamoku();
                    k.MsKamokuId = h.MsKamokuID;
                    k.Fugou = h.Fugou;
                    himokuDic[h.MsHimokuID].MsKamokus.Add(k);
                }
            }

            return new List<MsHimoku>(himokuDic.Values);
        }

        
        public static MsHimoku GetRecord(MsUser loginUser, int msHimokuId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsHimoku), MethodBase.GetCurrentMethod());

            List<MsHimoku> ret = new List<MsHimoku>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsHimoku> mapping = new MappingBase<MsHimoku>();

            Params.Add(new DBParameter("MS_HIMOKU_ID", msHimokuId));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);


            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }


		//部門を指定してデータを取得する
		public static List<MsHimoku> GetRecordsByMsBumonID(MsUser loginUser, int bumonid)
		{
			string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsHimoku), MethodBase.GetCurrentMethod());

			List<MsHimoku> ret = new List<MsHimoku>();

			ParameterConnection Params = new ParameterConnection();
			MappingBase<MsHimoku> mapping = new MappingBase<MsHimoku>();

			
			//パラメーターADD
			Params.Add(new DBParameter("MS_BUMON_ID", bumonid));


			ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

			return ret;
		}


        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsHimoku), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_HIMOKU_ID", this.MsHimokuID));
            Params.Add(new DBParameter("HIMOKU_NAME", this.HimokuName));
            Params.Add(new DBParameter("MS_BUMON_ID", this.MsBumonID));
            Params.Add(new DBParameter("KIKAN_HIMOKU_ID", this.KikanHimokuID));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsHimoku), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("HIMOKU_NAME", this.HimokuName));
            Params.Add(new DBParameter("MS_BUMON_ID", this.MsBumonID));
            Params.Add(new DBParameter("KIKAN_HIMOKU_ID", this.KikanHimokuID));

            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));

            Params.Add(new DBParameter("MS_HIMOKU_ID", this.MsHimokuID));
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
