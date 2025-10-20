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
    [TableAttribute("MS_SI_MENJOU")]
    public class MsSiMenjou : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 免許／免状ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_MENJOU_ID", true)]
        public int MsSiMenjouID { get; set; }

        
        
        
        /// <summary>
        /// 免許／免状名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 免許／免状名略称
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME_ABBR")]
        public string NameAbbr { get; set; }

        /// <summary>
        /// 表示順序
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



        // 2014.02 2013年度改造
        [DataMember]
        public List<MsSiMenjouKind> menjouKinds { get; set; }
        #endregion




        public static List<MsSiMenjou> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjou), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjou), "OrderByShowOrder");

            List<MsSiMenjou> ret = new List<MsSiMenjou>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiMenjou> mapping = new MappingBase<MsSiMenjou>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static MsSiMenjou GetRecord(MsUser loginUser, int msSiMenjouID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjou), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjou), "ByMsSiMenjouID");

            List<MsSiMenjou> ret = new List<MsSiMenjou>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SI_MENJOU_ID", msSiMenjouID));
            MappingBase<MsSiMenjou> mapping = new MappingBase<MsSiMenjou>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


        public static List<MsSiMenjou> SearchRecords(MsUser loginUser, string name, string nameAbbr)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjou), "GetRecords");

            List<MsSiMenjou> ret = new List<MsSiMenjou>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiMenjou> mapping = new MappingBase<MsSiMenjou>();

            if (name != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjou), "SearchByName");
                Params.Add(new DBParameter("NAME", "%" + name + "%"));
            }

            if (nameAbbr != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjou), "SearchByNameAbbr");
                Params.Add(new DBParameter("NAME_ABBR", "%" + nameAbbr + "%"));
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjou), "OrderByShowOrder");
            
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
                Params.Add(new DBParameter("MS_SI_MENJOU_ID", MsSiMenjouID));
            }
            
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

            Params.Add(new DBParameter("MS_SI_MENJOU_ID", MsSiMenjouID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsSiMenjouID));

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
            Params.Add(new DBParameter("PK", MsSiMenjouID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect,loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return MsSiMenjouID == 0;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
