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
    [TableAttribute("MS_KANIDOUSEI_INFO_SHUBETU")]
    public class MsKanidouseiInfoShubetu : ISyncTable
    {
        public static string 揚積 = "揚積";
        public static string 積み = "積み";
        public static string 揚げ = "揚げ";
        public static string 待機 = "待機";
        public static string 避泊 = "避泊";
        public static string パージ = "パージ";
        public static string その他 = "その他";

        //public static string 不明 = "不明";

        public static string 積みID = "0";
        public static string 揚げID = "1";
        public static string 待機ID = "3";
        public static string 不明ID = "10";

        #region データメンバ

        /// <summary>
        /// 簡易動静情報種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KANIDOUSEI_INFO_SHUBETU_ID", true)]
        public string MsKanidouseiInfoShubetuId { get; set; }

        /// <summary>
        /// 簡易動静情報種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("KANIDOUSEI_INFO_SHUBETU_NAME")]
        public string KanidouseiInfoShubetuName { get; set; }

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
            return KanidouseiInfoShubetuName;
        }

        public MsKanidouseiInfoShubetu()
        {
        }

        public static List<MsKanidouseiInfoShubetu> GetRecordsByDouseiEnabled(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKanidouseiInfoShubetu), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKanidouseiInfoShubetu), "ByDouseiEnabled");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKanidouseiInfoShubetu), "OrderBy");

            List<MsKanidouseiInfoShubetu> ret = new List<MsKanidouseiInfoShubetu>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsKanidouseiInfoShubetu> mapping = new MappingBase<MsKanidouseiInfoShubetu>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsKanidouseiInfoShubetu> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKanidouseiInfoShubetu), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKanidouseiInfoShubetu), "ByKaniDouseiEnabled");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKanidouseiInfoShubetu), "OrderBy");

            List<MsKanidouseiInfoShubetu> ret = new List<MsKanidouseiInfoShubetu>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsKanidouseiInfoShubetu> mapping = new MappingBase<MsKanidouseiInfoShubetu>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsKanidouseiInfoShubetu GetRecordByKanidouseiShubetuName(NBaseData.DAC.MsUser loginUser, string ShubetuName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKanidouseiInfoShubetu), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKanidouseiInfoShubetu), "ByKanidouseiShubetuName");
            List<MsKanidouseiInfoShubetu> ret = new List<MsKanidouseiInfoShubetu>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("KANIDOUSEI_INFO_SHUBETU_NAME", ShubetuName));
            MappingBase<MsKanidouseiInfoShubetu> mapping = new MappingBase<MsKanidouseiInfoShubetu>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static MsKanidouseiInfoShubetu GetRecordByKanidouseiShubetuName(List<MsKanidouseiInfoShubetu> msKanidouseiInfoShubetu_List, string ShubetuName)
        {
            foreach (MsKanidouseiInfoShubetu shubetu in msKanidouseiInfoShubetu_List)
            {
                if (shubetu.KanidouseiInfoShubetuName == ShubetuName)
                {
                    return shubetu;
                }
            }

            return null;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbcone, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_KANIDOUSEI_INFO_SHUBETU_ID", MsKanidouseiInfoShubetuId));
            Params.Add(new DBParameter("KANIDOUSEI_INFO_SHUBETU_NAME", KanidouseiInfoShubetuName));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            //cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            cnt = DBConnect.ExecuteNonQuery(dbcone, loginUser.MsUserID, SQL, Params);
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
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("KANIDOUSEI_INFO_SHUBETU_NAME", KanidouseiInfoShubetuName));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_KANIDOUSEI_INFO_SHUBETU_ID", MsKanidouseiInfoShubetuId));
            Params.Add(new DBParameter("TS", Ts));

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
        //    Params.Add(new DBParameter("PK", MsKanidouseiInfoShubetuId));

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
            Params.Add(new DBParameter("PK", MsKanidouseiInfoShubetuId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
