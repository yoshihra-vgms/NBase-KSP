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
    [TableAttribute("MS_KENSEN")]
    public class MsKensen
    {
        //MS_KENSEN_ID                   NVARCHAR2(40) NOT NULL,
        //MS_KENSEN_NAME                 NVARCHAR2(40),
        //KANKAKU                        NUMBER(9,0),
        
        //DELETE_FLAG                    NUMBER(1,0) NOT NULL,
        //SEND_FLAG                      NUMBER(1,0) NOT NULL,
        //VESSEL_ID                      NUMBER(4,0) NOT NULL,
        //DATA_NO                        NUMBER(13,0),
        //USER_KEY                       VARCHAR2(40),
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),
        #region データメンバ
        /// <summary>
        /// 検船マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KENSEN_ID")]
        public string MsKensenID { get; set; }

        /// <summary>
        /// 検船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KENSEN_NAME")]
        public string MsKensenName { get; set; }


        /// <summary>
        /// 間隔
        /// </summary>
        [DataMember]
        [ColumnAttribute("KANKAKU")]
        public int Kankaku { get; set; }
        //------------------------------------------------------------------

        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }

        /// <summary>
        /// 同期:送信フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEND_FLAG")]
        public int SendFlag { get; set; }

        /// <summary>
        /// 同期:船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_ID")]
        public int VesselID { get; set; }

        /// <summary>
        /// 同期:データNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }

        /// <summary>
        /// 同期:ユーザキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_KEY")]
        public string UserKey { get; set; }

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

        public override string ToString()
        {
            return this.MsKensenName;
        }

        public static List<MsKensen> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKensen), MethodBase.GetCurrentMethod());
            List<MsKensen> ret = new List<MsKensen>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsKensen> mapping = new MappingBase<MsKensen>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsKensen> GetRecordsByName(MsUser loginUser,string Name)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKensen), "GetRecords") + System.Environment.NewLine;
            SQL += SqlMapper.SqlMapper.GetSql(typeof(MsKensen), "ByName") + System.Environment.NewLine;

            List<MsKensen> ret = new List<MsKensen>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("NAME", "%" + Name + "%"));

            MappingBase<MsKensen> mapping = new MappingBase<MsKensen>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsKensen GetRecord(MsUser loginUser, string ms_kensen_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKensa), MethodBase.GetCurrentMethod());
            List<MsKensen> ret = new List<MsKensen>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsKensen> mapping = new MappingBase<MsKensen>();

            Params.Add(new DBParameter("MS_KENSEN_ID", ms_kensen_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKensen), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_KENSEN_ID", this.MsKensenID));
            Params.Add(new DBParameter("MS_KENSEN_NAME", this.MsKensenName));
            Params.Add(new DBParameter("KANKAKU", this.Kankaku));

            Params.Add(new DBParameter("DELETE_FLAG", this.DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", this.SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", this.VesselID));
            Params.Add(new DBParameter("DATA_NO", this.DataNo));
            Params.Add(new DBParameter("USER_KEY", this.UserKey));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("TS", this.Ts));

            DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            return true;
        }


        public bool UpdateRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKensen), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            int cnt = 0;
            Params.Add(new DBParameter("MS_KENSEN_ID", this.MsKensenID));
            Params.Add(new DBParameter("MS_KENSEN_NAME", this.MsKensenName));
            Params.Add(new DBParameter("KANKAKU", this.Kankaku));

            Params.Add(new DBParameter("DELETE_FLAG", this.DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", this.SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", this.VesselID));
            Params.Add(new DBParameter("DATA_NO", this.DataNo));
            Params.Add(new DBParameter("USER_KEY", this.UserKey));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("TS", this.Ts));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }


        /// <summary>
        /// 指定証書データにリンクした検船を取得する。
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static List<MsKensen> GetRecordsBy証書リンク(MsUser loginUser, string ks_shousho_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKensen), MethodBase.GetCurrentMethod());
            List<MsKensen> ret = new List<MsKensen>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsKensen> mapping = new MappingBase<MsKensen>();

            Params.Add(new DBParameter("KS_SHOUSHO_ID", ks_shousho_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


    }
}
