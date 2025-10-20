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
    [TableAttribute("MS_SI_KOUSHU")]
    public class MsSiKoushu : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 講習ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_KOUSHU_ID", true)]
        public int MsSiKoushuID { get; set; }

        
        
        
        /// <summary>
        /// 講習名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 有効期限（表示用）
        /// </summary>
        [DataMember]
        [ColumnAttribute("YUKOKIGEN_STR")]
        public string YukokigenStr { get; set; }

        /// <summary>
        /// 有効期限（日数）
        /// </summary>
        [DataMember]
        [ColumnAttribute("YUKOKIGEN_DAYS")]
        public int YukokigenDays { get; set; }

        
        
        
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


        public static List<MsSiKoushu> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiKoushu), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiKoushu), "OrderByName");

            List<MsSiKoushu> ret = new List<MsSiKoushu>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiKoushu> mapping = new MappingBase<MsSiKoushu>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static MsSiKoushu GetRecord(MsUser loginUser, int msSiKoushuID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiKoushu), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiKoushu), "ByMsSiKoushuID");

            List<MsSiKoushu> ret = new List<MsSiKoushu>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SI_KOUSHU_ID", msSiKoushuID));
            MappingBase<MsSiKoushu> mapping = new MappingBase<MsSiKoushu>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


        public static List<MsSiKoushu> SearchRecords(MsUser loginUser, string name)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiKoushu), "GetRecords");

            List<MsSiKoushu> ret = new List<MsSiKoushu>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiKoushu> mapping = new MappingBase<MsSiKoushu>();

            if (name != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiKoushu), "SearchByName");
                Params.Add(new DBParameter("NAME", "%" + name + "%"));
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiKoushu), "OrderByName");
            
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }




        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        #region ISyncTable メンバ

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            if (!IsNew())
            {
                Params.Add(new DBParameter("MS_SI_KOUSHU_ID", MsSiKoushuID));
            }
            
            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("YUKOKIGEN_STR", YukokigenStr));
            Params.Add(new DBParameter("YUKOKIGEN_DAYS", YukokigenDays));

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

            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("YUKOKIGEN_STR", YukokigenStr));
            Params.Add(new DBParameter("YUKOKIGEN_DAYS", YukokigenDays));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SI_KOUSHU_ID", MsSiKoushuID));
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
            Params.Add(new DBParameter("PK", MsSiKoushuID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect,loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return MsSiKoushuID == 0;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
