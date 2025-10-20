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
    [TableAttribute("MS_KENSA")]
    public class MsKensa
    {
        //MS_KENSA_ID           NVARCHAR2(40) NOT NULL,
        //KENSA_NAME            NVARCHAR2(50),
        //KANKAKU               NUMBER(9),
        //DELETE_FLAG           NUMBER(1) NOT NULL,
        
        //SEND_FLAG             NUMBER(1) NOT NULL,
        //VESSEL_ID             NUMBER(4) NOT NULL,
        //DATA_NO               NUMBER(13),
        //USER_KEY              VARCHAR2(40),
        //RENEW_DATE            DATE NOT NULL,
        //RENEW_USER_ID         VARCHAR2(40) NOT NULL,
        //TS                    VARCHAR2(20)
        #region データメンバ

        /// <summary>
        /// 検査マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KENSA_ID")]
        public string MsKensaID { get; set; }

        /// <summary>
        /// 検査名
        /// </summary>
        [DataMember]
        [ColumnAttribute("KENSA_NAME")]
        public string KensaName { get; set; }

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
            return this.KensaName;
        }

        public static List<MsKensa> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKensa), MethodBase.GetCurrentMethod());
            List<MsKensa> ret = new List<MsKensa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsKensa> mapping = new MappingBase<MsKensa>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsKensa> GetRecordsBy証書リンクデータ(MsUser loginUser, string ks_shousho_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKensa), MethodBase.GetCurrentMethod());
            List<MsKensa> ret = new List<MsKensa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsKensa> mapping = new MappingBase<MsKensa>();

            Params.Add(new DBParameter("KS_SHOUSHO_ID", ks_shousho_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsKensa> GetRecordsBy荷役リンクデータ(MsUser loginUser, string ks_niyaku_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKensa), MethodBase.GetCurrentMethod());
            List<MsKensa> ret = new List<MsKensa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsKensa> mapping = new MappingBase<MsKensa>();

            Params.Add(new DBParameter("KS_NIYAKU_ID", ks_niyaku_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //検査名をもとにデータを取得する
        public static List<MsKensa> GetRecordsBy検査名(MsUser loginUser, string kensa_name)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKensa), MethodBase.GetCurrentMethod());
            List<MsKensa> ret = new List<MsKensa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsKensa> mapping = new MappingBase<MsKensa>();

            Params.Add(new DBParameter("KENSA_NAME", kensa_name));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsKensa GetRecord(MsUser loginUser, string kensa_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKensa), MethodBase.GetCurrentMethod());
            List<MsKensa> ret = new List<MsKensa>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsKensa> mapping = new MappingBase<MsKensa>();

            Params.Add(new DBParameter("MS_KENSA_ID", kensa_id));
            
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];            
        }


        public bool InsertRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKensa), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_KENSA_ID", this.MsKensaID));
            Params.Add(new DBParameter("KENSA_NAME", this.KensaName));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKensa), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            int cnt = 0;
            Params.Add(new DBParameter("MS_KENSA_ID", this.MsKensaID));
            Params.Add(new DBParameter("KENSA_NAME", this.KensaName));
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


    }
}
