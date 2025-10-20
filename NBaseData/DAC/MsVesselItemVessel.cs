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
using ORMapping.Atts;
using NBaseData.DS;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("MS_VESSEL_ITEM_VESSEL")]
    public class MsVesselItemVessel : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 船用品船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ITEM_VESSEL_ID", true)]
        public string MsVesselItemVesselID { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 船用品ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ITEM_ID")]
        public string MsVesselItemID { get; set; }

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
        /// 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_NAME")]
        public string VesselName { get; set; }

        /// <summary>
        /// 船用品名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_ITEM_NAME")]
        public string VesselItemName { get; set; }

        /// <summary>
        /// 単位ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TANI_ID")]
        public string MsTaniID { get; set; }

        /// <summary>
        /// 船用品のカテゴリー番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("CATEGORY_NUMBER")]
        public int CategoryNumber { get; set; }

        /// <summary>
        /// 単位名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TANI_NAME")]
        public string MsTaniName { get; set; }


        // 2015.03 
        /// <summary>
        /// 規定在庫数
        /// </summary>
        [DataMember]
        [ColumnAttribute("ZAIKO_COUNT")]
        public int ZaikoCount { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        /// <summary>
        /// 特定船用品フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("SPECIFIC_FLAG")]
        public int SpecificFlag { get; set; }
        
        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public int Count { get; set; }
        #endregion

        public override string ToString()
        {
            return VesselItemName;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MsVesselItemVessel()
        {
        }

        public static List<MsVesselItemVessel> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), "OrderBy");
            List<MsVesselItemVessel> ret = new List<MsVesselItemVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVesselItemVessel> mapping = new MappingBase<MsVesselItemVessel>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static List<MsVesselItemVessel> GetRecordsByMsVesselID(NBaseData.DAC.MsUser loginUser, int MsVesselID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), "OrderBy");
            List<MsVesselItemVessel> ret = new List<MsVesselItemVessel>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            MappingBase<MsVesselItemVessel> mapping = new MappingBase<MsVesselItemVessel>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static List<MsVesselItemVessel> GetRecordsByMsVesselID(NBaseData.DAC.MsUser loginUser, int MsVesselID, int categoryNumber)
        {
            return GetRecordsByMsVesselID(null, loginUser, MsVesselID, categoryNumber);
        }
        public static List<MsVesselItemVessel> GetRecordsByMsVesselID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int MsVesselID, int categoryNumber)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), "ByCategoryNumber");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), "OrderBy");
            List<MsVesselItemVessel> ret = new List<MsVesselItemVessel>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("CATEGORY_NUMBER", categoryNumber));
            MappingBase<MsVesselItemVessel> mapping = new MappingBase<MsVesselItemVessel>();
            if (dbConnect == null)
            {
                ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            }
            else
            {
                ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);
            }

            return ret;
        }

        public static List<MsVesselItemVessel> GetRecordsByMsVesselIDVesselItemName(NBaseData.DAC.MsUser loginUser, int MsVesselID, int CategoryNumber, string VesselItemName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), MethodBase.GetCurrentMethod());
            List<MsVesselItemVessel> ret = new List<MsVesselItemVessel>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("CATEGORY_NUMBER", CategoryNumber));
            if (VesselItemName.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), "ByMsVesselItemName");
                Params.Add(new DBParameter("VESSEL_ITEM_NAME", "%" + VesselItemName + "%"));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), "OrderBy");
            MappingBase<MsVesselItemVessel> mapping = new MappingBase<MsVesselItemVessel>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsVesselItemVessel> GetRecordsByMsVesselIDVesselItemName2(NBaseData.DAC.MsUser loginUser, int MsVesselID, int CategoryNumber, string VesselItemName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), "GetRecordsByMsVesselID");
            List<MsVesselItemVessel> ret = new List<MsVesselItemVessel>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            if (CategoryNumber != Int32.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), "ByCategoryNumber");
                Params.Add(new DBParameter("CATEGORY_NUMBER", CategoryNumber));
            }
            if (VesselItemName.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), "ByMsVesselItemName");
                Params.Add(new DBParameter("VESSEL_ITEM_NAME", "%" + VesselItemName + "%"));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), "OrderBy");
            MappingBase<MsVesselItemVessel> mapping = new MappingBase<MsVesselItemVessel>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsVesselItemVessel GetRecord(string MsVesselItemVesselID, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), MethodBase.GetCurrentMethod());
            List<MsVesselItemVessel> ret = new List<MsVesselItemVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVesselItemVessel> mapping = new MappingBase<MsVesselItemVessel>();
            Params.Add(new DBParameter("MS_VESSEL_ITEM_VESSEL_ID", MsVesselItemVesselID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        //MsVesselItemIDで取得
        public static List<MsVesselItemVessel> GetRecordsByMsVesselItemID(MsUser loginUser, string msvesselitem_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), MethodBase.GetCurrentMethod());
            List<MsVesselItemVessel> ret = new List<MsVesselItemVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVesselItemVessel> mapping = new MappingBase<MsVesselItemVessel>();

            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", msvesselitem_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ITEM_VESSEL_ID", MsVesselItemVesselID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));

            // 2015.03
            Params.Add(new DBParameter("ZAIKO_COUNT", ZaikoCount));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("SPECIFIC_FLAG", SpecificFlag));

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

        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));

            // 2015.03
            Params.Add(new DBParameter("ZAIKO_COUNT", ZaikoCount));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("SPECIFIC_FLAG", SpecificFlag));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_VESSEL_ID", MsVesselItemVesselID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool DeleteRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItemVessel), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ITEM_VESSEL_ID", MsVesselItemVesselID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ


        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsVesselItemVesselID));

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
            Params.Add(new DBParameter("PK", MsVesselItemVesselID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

    }
}
