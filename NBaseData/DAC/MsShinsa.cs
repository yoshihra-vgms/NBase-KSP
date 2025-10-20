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
    [TableAttribute("MS_SHINSA")]
    public class MsShinsa
    {
        //MS_SHINSA_ID                   NVARCHAR2(40) NOT NULL,
        //MS_SHINSA_NAME                 NUMBER(9,0),
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
        /// 審査マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SHINSA_ID")]
        public string MsshinsaID { get; set; }

        /// <summary>
        /// 審査名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SHINSA_NAME")]
        public string MsShinsaName { get; set; }

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
            return this.MsShinsaName;
        }

        public static List<MsShinsa> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsShinsa), MethodBase.GetCurrentMethod());
            SQL += SqlMapper.SqlMapper.GetSql(typeof(MsShinsa), "OrderBy");
            List<MsShinsa> ret = new List<MsShinsa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsShinsa> mapping = new MappingBase<MsShinsa>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsShinsa> GetRecordsByName(MsUser loginUser,string Name)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsShinsa), "GetRecords") + System.Environment.NewLine;
            SQL += SqlMapper.SqlMapper.GetSql(typeof(MsShinsa), "ByName") + System.Environment.NewLine;
            SQL += SqlMapper.SqlMapper.GetSql(typeof(MsShinsa), "OrderBy");

            List<MsShinsa> ret = new List<MsShinsa>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("NAME", "%" + Name + "%"));

            MappingBase<MsShinsa> mapping = new MappingBase<MsShinsa>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static MsShinsa GetRecord(MsUser loginUser, string ms_shinsa_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsShinsa), MethodBase.GetCurrentMethod());
            List<MsShinsa> ret = new List<MsShinsa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsShinsa> mapping = new MappingBase<MsShinsa>();

            Params.Add(new DBParameter("MS_SHINSA_ID", ms_shinsa_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }



        public bool InsertRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsShinsa), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();


            Params.Add(new DBParameter("MS_SHINSA_ID", this.MsshinsaID));
            Params.Add(new DBParameter("MS_SHINSA_NAME", this.MsShinsaName));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsShinsa), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            int cnt = 0;
            Params.Add(new DBParameter("MS_SHINSA_ID", this.MsshinsaID));
            Params.Add(new DBParameter("MS_SHINSA_NAME", this.MsShinsaName));
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
        /// 証書リンクしているデータを取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="ks_shousho_id"></param>
        /// <returns></returns>
        public static List<MsShinsa> GetRecordsBy証書リンク(MsUser loginUser, string ks_shousho_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsShinsa), MethodBase.GetCurrentMethod());
            List<MsShinsa> ret = new List<MsShinsa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsShinsa> mapping = new MappingBase<MsShinsa>();

            Params.Add(new DBParameter("KS_SHOUSHO_ID", ks_shousho_id));
            
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
    }
}
