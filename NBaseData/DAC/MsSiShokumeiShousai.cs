using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using System.Reflection;
using ORMapping;
using System.Runtime.Serialization;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("MS_SI_SHOKUMEI_SHOUSAI")]
    public class MsSiShokumeiShousai : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 職名（乗船職）ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_SHOUSAI_ID", true)]
        public int MsSiShokumeiShousaiID { get; set; }

        /// <summary>
        /// 職名ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID", true)]
        public int MsSiShokumeiID { get; set; }



        /// <summary>
        /// 職名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 職名略称
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME_ABBR")]
        public string NameAbbr { get; set; }

        /// <summary>
        /// 職名(英語)
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME_ENG")]
        public string NameEng { get; set; }


        /// <summary>
        /// 表示順序（職位）
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOW_ORDER")]
        public int ShowOrder { get; set; }

        
        
        
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




        public static List<MsSiShokumeiShousai> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiShokumeiShousai), MethodBase.GetCurrentMethod());

            List<MsSiShokumeiShousai> ret = new List<MsSiShokumeiShousai>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiShokumeiShousai> mapping = new MappingBase<MsSiShokumeiShousai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        
        
        #region ISyncTable メンバ

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            if (!IsNew())
            {
                Params.Add(new DBParameter("MS_SI_SHOKUMEI_SHOUSAI_ID", MsSiShokumeiShousaiID));
            }

            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("NAME_ABBR", NameAbbr));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("NAME_ABBR", NameAbbr));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SI_SHOKUMEI_SHOUSAI_ID", MsSiShokumeiShousaiID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", MsSiShokumeiShousaiID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return MsSiShokumeiShousaiID == 0;
        }
        
        
        public override string ToString()
        {
            return Name;
        }
    }
}
