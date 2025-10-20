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
    [TableAttribute("MS_SI_MENJOU_KIND")]
    public class MsSiMenjouKind : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 免許／免状種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_MENJOU_KIND_ID", true)]
        public int MsSiMenjouKindID { get; set; }

        /// <summary>
        /// 免許／免状ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_MENJOU_ID")]
        public int MsSiMenjouID { get; set; }

        /// <summary>
        /// 種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 種別名略称
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

        /// <summary>
        /// 免許／免状名 (JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MENJOU_NAME")]
        public string MenjouName { get; set; }


        // 2014.02 2013年度改造
        /// <summary>
        /// 除外対象
        /// </summary>
        [DataMember]
        public List<MsSiExcludeMenjouKind> ExcludeMenjouKinds { get; set; }

        #endregion


        // 2014.02 2013年度改造
        public MsSiMenjouKind()
        {
            this.ExcludeMenjouKinds = new List<MsSiExcludeMenjouKind>();
        }


        public static List<MsSiMenjouKind> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjouKind), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjouKind), "OrderByShowOrder");

            List<MsSiMenjouKind> ret = new List<MsSiMenjouKind>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiMenjouKind> mapping = new MappingBase<MsSiMenjouKind>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsSiMenjouKind> GetRecordsByMsSiMenjouID(MsUser loginUser, int ms_si_menjou_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjouKind), MethodBase.GetCurrentMethod());
            
            List<MsSiMenjouKind> ret = new List<MsSiMenjouKind>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SI_MENJOU_ID", ms_si_menjou_id));
            MappingBase<MsSiMenjouKind> mapping = new MappingBase<MsSiMenjouKind>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
                     
            return ret;
        }

        public static MsSiMenjouKind GetRecord(MsUser loginUser, int msSiMenjouKindID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjouKind), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjouKind), "ByMsSiMenjouKindID");

            List<MsSiMenjouKind> ret = new List<MsSiMenjouKind>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SI_MENJOU_KIND_ID", msSiMenjouKindID));
            MappingBase<MsSiMenjouKind> mapping = new MappingBase<MsSiMenjouKind>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<MsSiMenjouKind> SearchRecords(MsUser loginUser, int msSiMenjouId, string name, string nameAbbr)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjouKind), "GetRecords");

            List<MsSiMenjouKind> ret = new List<MsSiMenjouKind>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiMenjouKind> mapping = new MappingBase<MsSiMenjouKind>();

            if (msSiMenjouId != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjouKind), "ByMsSiMenjouID");
                Params.Add(new DBParameter("MS_SI_MENJOU_ID", msSiMenjouId));
            }

            if (name != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjouKind), "SearchByName");
                Params.Add(new DBParameter("NAME", "%" + name + "%"));
            }

            if (nameAbbr != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjouKind), "SearchByNameAbbr");
                Params.Add(new DBParameter("NAME_ABBR", "%" + nameAbbr + "%"));
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiMenjouKind), "OrderByShowOrder");

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
                Params.Add(new DBParameter("MS_SI_MENJOU_KIND_ID", MsSiMenjouKindID));
            }
            
            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("NAME_ABBR", NameAbbr));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            Params.Add(new DBParameter("MS_SI_MENJOU_ID", MsSiMenjouID));

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

            // 2014.02 2013年度改造
            if (IsNew())
            {
                MsSiMenjouKindID = Sequences.GetMsSiMenjouKindId(dbConnect, loginUser);
            }

            foreach (MsSiExcludeMenjouKind obj in ExcludeMenjouKinds)
            {
                obj.MsSiMenjouKindID = MsSiMenjouKindID;
                obj.RenewDate = RenewDate;
                obj.RenewUserID = RenewUserID;
                bool ret = obj.InsertRecord(dbConnect, loginUser);
                if (ret == false)
                    return false;
            }
            // <==

            return true;
        }

        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("NAME_ABBR", NameAbbr));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            Params.Add(new DBParameter("MS_SI_MENJOU_ID", MsSiMenjouID));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SI_MENJOU_KIND_ID", MsSiMenjouKindID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;


            // 2014.02 2013年度改造
            foreach (MsSiExcludeMenjouKind obj in ExcludeMenjouKinds)
            {
                obj.RenewDate = RenewDate;
                obj.RenewUserID = RenewUserID;

                bool ret = true;
                if (obj.IsNew())
                {
                    ret = obj.InsertRecord(dbConnect, loginUser);
                }
                else
                {
                    ret = obj.UpdateRecord(dbConnect, loginUser);
                }
                if (ret == false)
                    return false;
            }
            // <==

            return true;
        }

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsSiMenjouKindID));

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
            Params.Add(new DBParameter("PK", MsSiMenjouKindID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return MsSiMenjouKindID == 0;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
