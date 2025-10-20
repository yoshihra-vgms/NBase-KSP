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
    [TableAttribute("MS_YUSO_ITEM")]
    public class MsYusoItem
    {
        #region データメンバ
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

        #endregion

        public override string ToString()
        {
            return YusoItemName;
        }

        public static List<MsYusoItem> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsYusoItem), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsYusoItem), "OrderBy");

            List<MsYusoItem> ret = new List<MsYusoItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsYusoItem> mapping = new MappingBase<MsYusoItem>();

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static List<MsYusoItem> GetRecordsByYusoItemName(MsUser loginUser, string yusoItemName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsYusoItem), "GetRecords");
            if (yusoItemName != null && yusoItemName.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsYusoItem), "ByYusoItemName");
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsYusoItem), "OrderBy");

            List<MsYusoItem> ret = new List<MsYusoItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsYusoItem> mapping = new MappingBase<MsYusoItem>();
            if (yusoItemName != null && yusoItemName.Length > 0)
            {
                Params.Add(new DBParameter("YUSO_ITEM_NAME", "%" + yusoItemName + "%"));
            }
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsYusoItem GetRecord(MsUser loginUser, int MsYusoItemID)
        {
            return GetRecord(null, loginUser, MsYusoItemID);
        }
        public static MsYusoItem GetRecord(DBConnect dbConnect, MsUser loginUser, int MsYusoItemID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsYusoItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsYusoItem), "ByMsYusoItemID");

            List<MsYusoItem> ret = new List<MsYusoItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsYusoItem> mapping = new MappingBase<MsYusoItem>();
            Params.Add(new DBParameter("MS_YUSO_ITEM_ID", MsYusoItemID));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsYusoItem), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsYusoItem), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YUSO_ITEM_CODE", YusoItemCode));
            Params.Add(new DBParameter("YUSO_ITEM_NAME", YusoItemName));
            Params.Add(new DBParameter("SENSHU_CODE", SenshuCode));
            Params.Add(new DBParameter("SENSHU_NAME", SenshuName));
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            Params.Add(new DBParameter("MS_YUSO_ITEM_ID", MsYusoItemID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = 0;
            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool DeleteRecord(MsUser loginUser)
        {
            DeleteFlag = 1;
            return UpdateRecord(loginUser);
        }
    }
}
