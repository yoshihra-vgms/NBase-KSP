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
    [TableAttribute("OD_HACHU_TANKA")]
    public class OdHachuTanka : ISyncTable
    {
        
        //DBの要素
        //OD_HACHU_TANKA_ID         VARCHAR2(40)    必須    発注単価ID
        //MS_VESSEL_ITEM_ID         VARCHAR2(40)            船用品ID
        //MS_LO_ID                  VARCHAR2(40)            潤滑油ID
        //TANKA                     NUMBER(16,3)    必須    単価
        //TANKA_SETEIBI             DATE            必須    単価設定日
        //OD_JRY_SHOUSAI_ITEM_ID    VARCHAR2(40)            受領詳細品目ID
        //SHR_SHOUSAI_ITEM_ID       VARCHAR2(40)            支払い詳細品目ID
        //SEND_FLAG                 NUMBER(1,0)     必須    同期：送信フラグ        省略時：0
        //VESSEL_ID                 NUMBER(4,0)     必須    同期:船ID
        //DATA_NO                   NUMBER(13,0)            同期:データNO
        //USER_KEY                  VARCHAR2(40)            同期:ユーザキー         省略時：0
        //RENEW_DATE                DATE            必須    更新日
        //RENEW_USER_ID             VARCHAR2(40)    必須    更新者
        //TS                        TIMESTANP(6)            排他制御

        #region データメンバ

        /// <summary>
        /// 発注単価ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_HACHU_TANKA_ID", true)]
        public string OdHachuTankaID { get; set; }

        /// <summary>
        /// 船用品ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ITEM_ID")]
        public string MsVesselItemID { get; set; }

        /// <summary>
        /// 潤滑油ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_LO_ID")]
        public string MsLoID { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANKA")]
        public decimal Tanka { get; set; }

        /// <summary>
        /// 単価設定日
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANKA_SETEIBI")]
        public DateTime TankaSeteibi { get; set; }

        /// <summary>
        /// 受領詳細品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_SHOUSAI_ITEM_ID")]
        public string OdJryShousaiItemID { get; set; }

        /// <summary>
        /// 支払い詳細品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_SHR_SHOUSAI_ITEM_ID")]
        public string OdShrShousaiItemID { get; set; }

        /// <summary>
        /// 送信フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEND_FLAG")]
        public int SendFlag { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_ID")]
        public int VesselID { get; set; }

        /// <summary>
        /// データNo
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
        /// 更新者(UserID)
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

        public OdHachuTanka()
        {
            
        }

        //////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////
        public static List<OdHachuTanka> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdHachuTanka), MethodBase.GetCurrentMethod());
            List<OdHachuTanka> ret = new List<OdHachuTanka>();
            ParameterConnection Params = new ParameterConnection();            
            MappingBase<OdHachuTanka> mapping = new MappingBase<OdHachuTanka>();            
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        //潤滑油
        public static List<OdHachuTanka> GetRecordsByMaLoID(MsUser loginUser, string ms_lo_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdHachuTanka), MethodBase.GetCurrentMethod());
            List<OdHachuTanka> ret = new List<OdHachuTanka>();
            ParameterConnection Params = new ParameterConnection();

            MappingBase<OdHachuTanka> mapping = new MappingBase<OdHachuTanka>();


            Params.Add(new DBParameter("MS_LO_ID", ms_lo_id));


            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }


        //船用品
        public static List<OdHachuTanka> GetRecordsByMsVesselItemID(MsUser loginUser, string ms_vesselitem_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdHachuTanka), MethodBase.GetCurrentMethod());
            List<OdHachuTanka> ret = new List<OdHachuTanka>();
            ParameterConnection Params = new ParameterConnection();

            MappingBase<OdHachuTanka> mapping = new MappingBase<OdHachuTanka>();


            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", ms_vesselitem_id));
            

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }


        /// <summary>
        /// 指定期間と船用品IDか潤滑油IDのどちらかを指定して期間内設定最終日を取得する関数
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static OdHachuTanka GetRecordByMsLoID_MsVesselItemID_Date
            (NBaseData.DAC.MsUser loginUser, string mslovesselid, string msvesselitemid, DateTime startdate, DateTime enddate)
        {

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdHachuTanka), MethodBase.GetCurrentMethod());
            List<OdHachuTanka> ret = new List<OdHachuTanka>();
            ParameterConnection Params = new ParameterConnection();
            
            MappingBase<OdHachuTanka> mapping = new MappingBase<OdHachuTanka>();

            Params.Add(new DBParameter("MS_LO_ID", mslovesselid));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", msvesselitemid));
            Params.Add(new DBParameter("START_DATE", startdate));
            Params.Add(new DBParameter("END_DATE", enddate));
            
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            
            return ret[0];            
        }

        /// <summary>
        /// 指定船と期間を指定して
        /// 指定船対応潤滑油の最終単価を取得する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="msvesselid"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public static List<OdHachuTanka> GetRecordsByMsVesselItemID_Date_LO
            (NBaseData.DAC.MsUser loginUser, int msvesselid, DateTime startdate, DateTime enddate)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdHachuTanka), MethodBase.GetCurrentMethod());
            List<OdHachuTanka> ret = new List<OdHachuTanka>();
            ParameterConnection Params = new ParameterConnection();

            MappingBase<OdHachuTanka> mapping = new MappingBase<OdHachuTanka>();

            
            Params.Add(new DBParameter("START_DATE", startdate));
            Params.Add(new DBParameter("END_DATE", enddate));
            Params.Add(new DBParameter("MS_VESSEL_ID", msvesselid));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret; 
        }

        //指定船と期間を指定して船用品の最終単価を取得する。
        public static List<OdHachuTanka> GetRecordsByMsVesselItemID_Date_VesselItem
            (NBaseData.DAC.MsUser loginUser, int msvesselid, DateTime startdate, DateTime enddate)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdHachuTanka), MethodBase.GetCurrentMethod());
            List<OdHachuTanka> ret = new List<OdHachuTanka>();
            ParameterConnection Params = new ParameterConnection();

            MappingBase<OdHachuTanka> mapping = new MappingBase<OdHachuTanka>();


            Params.Add(new DBParameter("START_DATE", startdate));
            Params.Add(new DBParameter("END_DATE", enddate));
            Params.Add(new DBParameter("MS_VESSEL_ID", msvesselid));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }



        //指定船と期間を指定して関連アイテム(潤滑油と船用品)の最終単価をすべて取得する

        #region ISyncTable メンバ


        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", OdHachuTankaID));

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
            Params.Add(new DBParameter("PK", OdHachuTankaID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdHachuTanka), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_HACHU_TANKA_ID", OdHachuTankaID));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("TANKA", Tanka));
            Params.Add(new DBParameter("TANKA_SETEIBI", TankaSeteibi));
            Params.Add(new DBParameter("OD_JRY_SHOUSAI_ITEM_ID", OdJryShousaiItemID));
            Params.Add(new DBParameter("OD_SHR_SHOUSAI_ITEM_ID", OdShrShousaiItemID));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdHachuTanka), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", MsVesselItemID));
            Params.Add(new DBParameter("MS_LO_ID", MsLoID));
            Params.Add(new DBParameter("TANKA", Tanka));
            Params.Add(new DBParameter("TANKA_SETEIBI", TankaSeteibi));
            Params.Add(new DBParameter("OD_JRY_SHOUSAI_ITEM_ID", OdJryShousaiItemID));
            Params.Add(new DBParameter("OD_SHR_SHOUSAI_ITEM_ID", OdShrShousaiItemID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("OD_HACHU_TANKA_ID", OdHachuTankaID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #endregion
    }
}
