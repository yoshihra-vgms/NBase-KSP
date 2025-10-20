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
    [TableAttribute("MS_SHOUSHO")]
    public class MsShousho
    {
        //MS_SHOUSHO_ID                  NVARCHAR2(40) NOT NULL,
        //MS_SHOUSHO_NAME                NVARCHAR2(40),
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
        /// 証書マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SHOUSHO_ID")]
        public string MsShoushoID { get; set; }
        

        /// <summary>
        /// 証書名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SHOUSHO_NAME")]
        public string MsShoushoName { get; set; }

        /// <summary>
        /// 間隔
        /// </summary>
        [DataMember]
        [ColumnAttribute("KANKAKU")]
        public int Kanakaku { get; set; }

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
            return this.MsShoushoName;
        }

        public static List<MsShousho> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsShousho), MethodBase.GetCurrentMethod());
            List<MsShousho> ret = new List<MsShousho>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsShousho> mapping = new MappingBase<MsShousho>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsShousho> GetRecordsByName(MsUser loginUser,string Name)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsShousho), "GetRecords") + System.Environment.NewLine;
            SQL += SqlMapper.SqlMapper.GetSql(typeof(MsShousho), "ByName") + System.Environment.NewLine;

            List<MsShousho> ret = new List<MsShousho>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("NAME", "%" + Name + "%"));

            MappingBase<MsShousho> mapping = new MappingBase<MsShousho>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsShousho GetRecord(MsUser loginUser, string ms_hsousho_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsShousho), MethodBase.GetCurrentMethod());
            List<MsShousho> ret = new List<MsShousho>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsShousho> mapping = new MappingBase<MsShousho>();

            Params.Add(new DBParameter("MS_SHOUSHO_ID", ms_hsousho_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


        public bool InsertRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsShousho), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            //MS_SHOUSHO_ID                  NVARCHAR2(40) NOT NULL,
            //MS_SHOUSHO_NAME                NVARCHAR2(40),
            //KANKAKU                        NUMBER(9,0),
            Params.Add(new DBParameter("MS_SHOUSHO_ID", this.MsShoushoID));
            Params.Add(new DBParameter("MS_SHOUSHO_NAME", this.MsShoushoName));
            Params.Add(new DBParameter("KANKAKU", this.Kanakaku));            

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsShousho), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            int cnt = 0;
            Params.Add(new DBParameter("MS_SHOUSHO_ID", this.MsShoushoID));
            Params.Add(new DBParameter("MS_SHOUSHO_NAME", this.MsShoushoName));
            Params.Add(new DBParameter("KANKAKU", this.Kanakaku));

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

    }
}
