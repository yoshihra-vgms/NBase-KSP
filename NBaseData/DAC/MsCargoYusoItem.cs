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
    [TableAttribute("MS_CARGO_YUSO_ITEM")]
    public class MsCargoYusoItem
    {
        #region データメンバ
        /// <summary>
        /// 貨物-輸送品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CARGO_YUSO_ITEM_ID")]
        public int MsCargoYusoItemID { get; set; }

        /// <summary>
        /// 開始日
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_DAY")]
        public DateTime StartDay { get; set; }

        /// <summary>
        /// 終了日
        /// </summary>
        [DataMember]
        [ColumnAttribute("END_DAY")]
        public DateTime EndDay { get; set; }
        
        /// <summary>
        /// 貨物ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CARGO_ID")]
        public int MsCargoID { get; set; }

        /// <summary>
        /// 輸送品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_YUSO_ITEM_ID")]
        public int MsYusoItemID { get; set; }

        /// <summary>
        /// 輸送品目コード
        /// </summary>
        [DataMember]
        [ColumnAttribute("YUSO_ITEM_CODE")]
        public string YusoItemCode { get; set; }

        /// <summary>
        /// 輸送品目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("YUSO_ITEM_NAME")]
        public string YusoItemName { get; set; }

        /// <summary>
        /// 船種コード
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENSHU_CODE")]
        public string SenshuCode { get; set; }

        /// <summary>
        /// 船種名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENSHU_NAME")]
        public string SenshuName { get; set; }

        /// <summary>
        /// 更新者
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
        /// 貨物NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARGO_NO")]
        public string CargoNo { get; set; }

        #endregion

        public static List<MsCargoYusoItem> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargoYusoItem), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCargoYusoItem), "OrderBy");

            List<MsCargoYusoItem> ret = new List<MsCargoYusoItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCargoYusoItem> mapping = new MappingBase<MsCargoYusoItem>();

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsCargoYusoItem> GetRecordsByJikoNo(MsUser loginUser, string jiko)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargoYusoItem), "GetRecordsByJikoNo");

            List<MsCargoYusoItem> ret = new List<MsCargoYusoItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCargoYusoItem> mapping = new MappingBase<MsCargoYusoItem>();
            Params.Add(new DBParameter("JIKO_NO", jiko));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsCargoYusoItem GetRecord(MsUser loginUser,int MsCargoYusoItemID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargoYusoItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCargoYusoItem), "ByMsCargoYusoItemID");

            List<MsCargoYusoItem> ret = new List<MsCargoYusoItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCargoYusoItem> mapping = new MappingBase<MsCargoYusoItem>();
            Params.Add(new DBParameter("MS_CARGO_YUSO_ITEM_ID", MsCargoYusoItemID));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count > 0)
            {
                return ret[0];
            }
            return null;
        }

        public static MsCargoYusoItem GetRecordByMsCargoID(MsUser loginUser, int MsCargoID)
        {
            return GetRecordByMsCargoID(null, loginUser, MsCargoID);
        }
        public static MsCargoYusoItem GetRecordByMsCargoID(DBConnect dbConnect, MsUser loginUser, int MsCargoID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargoYusoItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCargoYusoItem), "ByMsCargoID");

            List<MsCargoYusoItem> ret = new List<MsCargoYusoItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCargoYusoItem> mapping = new MappingBase<MsCargoYusoItem>();
            Params.Add(new DBParameter("MS_CARGO_ID", MsCargoID));

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count > 0)
            {
                return ret[0];
            }
            return null;
        }


        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargoYusoItem), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("START_DAY", StartDay));
            Params.Add(new DBParameter("END_DAY", EndDay));
            Params.Add(new DBParameter("MS_CARGO_ID", MsCargoID));
            Params.Add(new DBParameter("MS_YUSO_ITEM_ID", MsYusoItemID));
            Params.Add(new DBParameter("YUSO_ITEM_CODE", YusoItemCode));
            Params.Add(new DBParameter("YUSO_ITEM_NAME", YusoItemName));
            Params.Add(new DBParameter("SENSHU_CODE", SenshuCode));
            Params.Add(new DBParameter("SENSHU_NAME", SenshuName));
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("USER_KEY", "0"));

            int cnt = 0;
            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;

        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }
        public bool UpdateRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargoYusoItem), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("START_DAY", StartDay));
            Params.Add(new DBParameter("END_DAY", EndDay));
            Params.Add(new DBParameter("MS_CARGO_ID", MsCargoID));
            Params.Add(new DBParameter("MS_YUSO_ITEM_ID", MsYusoItemID));
            Params.Add(new DBParameter("YUSO_ITEM_CODE", YusoItemCode));
            Params.Add(new DBParameter("YUSO_ITEM_NAME", YusoItemName));
            Params.Add(new DBParameter("SENSHU_CODE", SenshuCode));
            Params.Add(new DBParameter("SENSHU_NAME", SenshuName));
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("USER_KEY", "0"));
            
            Params.Add(new DBParameter("MS_CARGO_YUSO_ITEM_ID", MsCargoYusoItemID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = 0;
            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;

        }
    }
}
