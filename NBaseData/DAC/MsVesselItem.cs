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
    [TableAttribute("MS_VESSEL_ITEM")]
    public class MsVesselItem : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 船用品ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ITEM_ID", true)]
        public string MsVesselItemID { get; set; }

        /// <summary>
        /// 船用品名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_ITEM_NAME")]
        public string VesselItemName { get; set; }

        /// <summary>
        /// 科目NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("KAMOKU_NO")]
        public string KamokuNo { get; set; }

        /// <summary>
        /// 単位ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TANI_ID")]
        public string MsTaniID { get; set; }

        /// <summary>
        /// カテゴリ番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("CATEGORY_NUMBER")]
        public int CategoryNumber { get; set; }

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
        /// 単位名
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANI_NAME")]
        public string TaniName { get; set; }


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

        #endregion

        public MsVesselItem()
        {
        }

        public static List<MsVesselItem> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItem), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVesselItem), "OrderBy");
            List<MsVesselItem> ret = new List<MsVesselItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVesselItem> mapping = new MappingBase<MsVesselItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsVesselItem GetRecord(NBaseData.DAC.MsUser loginUser, string MsVesselItemID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItem), MethodBase.GetCurrentMethod());
            List<MsVesselItem> ret = new List<MsVesselItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVesselItem> mapping = new MappingBase<MsVesselItem>();
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));
            Params.Add(new DBParameter("VESSEL_ITEM_NAME", VesselItemName));
            Params.Add(new DBParameter("KAMOKU_NO", KamokuNo));
            Params.Add(new DBParameter("MS_TANI_ID", MsTaniID));
            Params.Add(new DBParameter("CATEGORY_NUMBER", CategoryNumber));

            // 2015.03
            Params.Add(new DBParameter("ZAIKO_COUNT", ZaikoCount));
            Params.Add(new DBParameter("BIKOU", Bikou));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("VESSEL_ITEM_NAME", VesselItemName));
            Params.Add(new DBParameter("KAMOKU_NO", KamokuNo));
            Params.Add(new DBParameter("MS_TANI_ID", MsTaniID));
            Params.Add(new DBParameter("CATEGORY_NUMBER", CategoryNumber));

            // 2015.03
            Params.Add(new DBParameter("ZAIKO_COUNT", ZaikoCount));
            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public static List<MsVesselItem> SearchRecords(MsUser loginUser, string msVesselItemId, string vesselItem, int categoryNumber)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItem), "GetRecords");
            List<MsVesselItem> ret = new List<MsVesselItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVesselItem> mapping = new MappingBase<MsVesselItem>();

            SQL += " Where MS_VESSEL_ITEM.MS_VESSEL_ITEM_ID = MS_VESSEL_ITEM.MS_VESSEL_ITEM_ID";
            if (msVesselItemId != "")
            {
                SQL += " and MS_VESSEL_ITEM.MS_VESSEL_ITEM_ID like :MS_VESSEL_ITEM_ID";
                Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", "%" + msVesselItemId + "%"));
            }
            if (vesselItem != "")
            {
                SQL += "  and MS_VESSEL_ITEM.VESSEL_ITEM_NAME like :VESSEL_ITEM_NAME";
                Params.Add(new DBParameter("VESSEL_ITEM_NAME", "%" + vesselItem + "%"));
            }
            if (categoryNumber > 0)
            {
                SQL += " and MS_VESSEL_ITEM.CATEGORY_NUMBER = :CATEGORY_NUMBER";
                Params.Add(new DBParameter("CATEGORY_NUMBER", categoryNumber));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVesselItem), "OrderBy");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;

        }

        public bool DeleteRecord(NBaseData.DAC.MsUser loginUser)
        {
            return DeleteRecord(null, loginUser);
        }
        public bool DeleteRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselItem), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsVesselItemID));

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
            Params.Add(new DBParameter("PK", MsVesselItemID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
