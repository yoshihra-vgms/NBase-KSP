
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
    [TableAttribute("MS_DM_SHOUBUNRUI")]
    public class MsDmShoubunrui : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 小分類ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_SHOUBUNRUI_ID", true)]
        public string MsDmShoubunruiID { get; set; }
        
        /// <summary>
        /// 小分類ｺｰﾄﾞ
        /// </summary>
        [DataMember]
        [ColumnAttribute("CODE")]
        public string Code { get; set; }

        /// <summary>
        /// 小分類名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }
        
        /// <summary>
        /// 分類ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_BUNRUI_ID")]
        public string MsDmBunruiID { get; set; }

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


        /// <summary>
        /// 分類名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_BUNRUI_NAME")]
        public string BunruiName { get; set; }
        
        #endregion

        public override string ToString()
        {
            return Name;
        }

        public MsDmShoubunrui()
        {
        }

        public static List<MsDmShoubunrui> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmShoubunrui), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmShoubunrui), "OrderBy");
            List<MsDmShoubunrui> ret = new List<MsDmShoubunrui>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsDmShoubunrui> mapping = new MappingBase<MsDmShoubunrui>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsDmShoubunrui> GetRecordsByBunruiID(NBaseData.DAC.MsUser loginUser, string msDmBunruiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmShoubunrui), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmShoubunrui), "ByMsDmBunruiID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmShoubunrui), "OrderBy");
            List<MsDmShoubunrui> ret = new List<MsDmShoubunrui>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_DM_BUNRUI_ID", msDmBunruiID));
            MappingBase<MsDmShoubunrui> mapping = new MappingBase<MsDmShoubunrui>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsDmShoubunrui> GetRecordsByNameAndBunruiID(NBaseData.DAC.MsUser loginUser, string name, string msDmBunruiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmShoubunrui), "GetRecords");
            List<MsDmShoubunrui> ret = new List<MsDmShoubunrui>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsDmShoubunrui> mapping = new MappingBase<MsDmShoubunrui>();
            if (name.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmShoubunrui), "LikeName");
                Params.Add(new DBParameter("NAME", "%" + name + "%"));
            }
            if (msDmBunruiID.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmShoubunrui), "ByMsDmBunruiID");
                Params.Add(new DBParameter("MS_DM_BUNRUI_ID", msDmBunruiID));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmShoubunrui), "OrderBy");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsDmShoubunrui GetRecord(NBaseData.DAC.MsUser loginUser, string MsDmShoubunruiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmShoubunrui), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmShoubunrui), "ByMsDmShoubunruiID");
            List<MsDmShoubunrui> ret = new List<MsDmShoubunrui>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsDmShoubunrui> mapping = new MappingBase<MsDmShoubunrui>();
            Params.Add(new DBParameter("MS_DM_SHOUBUNRUI_ID", MsDmShoubunruiID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmShoubunrui), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_DM_SHOUBUNRUI_ID", MsDmShoubunruiID));
            Params.Add(new DBParameter("CODE", Code));
            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("MS_DM_BUNRUI_ID", MsDmBunruiID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }

        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmShoubunrui), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("CODE", Code));
            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("MS_DM_BUNRUI_ID", MsDmBunruiID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_DM_SHOUBUNRUI_ID", MsDmShoubunruiID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsDmShoubunruiID));

        //    int count = Convert.ToInt32(DBConnect.ExecuteScalar(loginUser.MsUserID, SQL, Params));

        //    return count > 0 ? true : false;
        //}
        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", MsDmShoubunruiID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
