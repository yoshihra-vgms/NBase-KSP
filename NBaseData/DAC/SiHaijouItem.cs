using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using System.Runtime.Serialization;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("SI_HAIJOU_ITEM")]
    public class SiHaijouItem : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 配乗表アイテムID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_HAIJOU_ITEM_ID", true)]
        public string SiHaijouItemID { get; set; }




        /// <summary>
        /// 配乗表ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_HAIJOU_ID")]
        public string SiHaijouID { get; set; }

        /// <summary>
        /// 船ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 職名ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID")]
        public int MsSiShokumeiID { get; set; }

        /// <summary>
        /// 種別ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHUBETSU_ID")]
        public int MsSiShubetsuID { get; set; }

        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }




        /// <summary>
        /// アイテム種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("ITEM_KIND")]
        public int ItemKind { get; set; }

        /// <summary>
        /// 乗船日数
        /// </summary>
        [DataMember]
        [ColumnAttribute("WORKDAYS")]
        public int WorkDays { get; set; }

        /// <summary>
        /// 休暇日数
        /// </summary>
        [DataMember]
        [ColumnAttribute("HOLIDAYS")]
        public int HoliDays { get; set; }

        
        
        
        
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
        /// 船員名 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_NAME")]
        public string SeninName { get; set; }


        /// <summary>
        /// 船員カードID (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_CARD_ID")]
        public string SiCardID { get; set; }



        #endregion




        public SiHaijouItem()
        {
            this.MsVesselID = Int32.MinValue;
            this.MsSiShokumeiID = Int32.MinValue;
            this.MsSeninID = Int32.MinValue;
        }

        
        

        public static List<SiHaijouItem> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiHaijouItem), MethodBase.GetCurrentMethod());

            List<SiHaijouItem> ret = new List<SiHaijouItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiHaijouItem> mapping = new MappingBase<SiHaijouItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        
        //船ID
        public static List<SiHaijouItem> GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiHaijouItem), MethodBase.GetCurrentMethod());

            List<SiHaijouItem> ret = new List<SiHaijouItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiHaijouItem> mapping = new MappingBase<SiHaijouItem>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<SiHaijouItem> GetRecordsBySiHaijouID(MsUser loginUser, string siHaijouId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiHaijouItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiHaijouItem), "BySiHaijouID");

            List<SiHaijouItem> ret = new List<SiHaijouItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiHaijouItem> mapping = new MappingBase<SiHaijouItem>();
            Params.Add(new DBParameter("SI_HAIJOU_ID", siHaijouId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }




        #region ISyncTable メンバ

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_HAIJOU_ITEM_ID", SiHaijouItemID));

            Params.Add(new DBParameter("SI_HAIJOU_ID", SiHaijouID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("MS_SI_SHUBETSU_ID", MsSiShubetsuID));
            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
           
            Params.Add(new DBParameter("ITEM_KIND", ItemKind));
            Params.Add(new DBParameter("WORKDAYS", WorkDays));
            Params.Add(new DBParameter("HOLIDAYS", HoliDays));

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

            Params.Add(new DBParameter("SI_HAIJOU_ID", SiHaijouID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("MS_SI_SHUBETSU_ID", MsSiShubetsuID));
            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));

            Params.Add(new DBParameter("ITEM_KIND", ItemKind));
            Params.Add(new DBParameter("WORKDAYS", WorkDays));
            Params.Add(new DBParameter("HOLIDAYS", HoliDays));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_HAIJOU_ITEM_ID", SiHaijouItemID));
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
        //    Params.Add(new DBParameter("PK", SiHaijouItemID));

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
            Params.Add(new DBParameter("PK", SiHaijouItemID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiHaijouItemID == null;
        }


        public void SetItemKind(bool Is_兼, bool Is_執, bool Is_融)
        {
            ItemKind = 0;

            if (Is_兼)
            {
                ItemKind += 1;
            }

            if (Is_執)
            {
                ItemKind += 1 << 1;
            }

            if (Is_融)
            {
                ItemKind += 1 << 2;
            }
        }


        public string GetItemKindString()
        {
            string result = string.Empty;

            if (Is_兼(ItemKind))
            {
                result += "兼";
            }

            if (Is_執(ItemKind))
            {
                result += "執";
            }

            if (Is_融(ItemKind))
            {
                result += "融";
            }

            if (result.Length > 0)
            {
                result += ":";
            }

            return result;
        }


        public static bool Is_兼(int itemKind)
        {
            return (itemKind & 1) > 0;
        }


        public static bool Is_執(int itemKind)
        {
            return (itemKind & 1 << 1) > 0;
        }


        public static bool Is_融(int itemKind)
        {
            return (itemKind & 1 << 2) > 0;
        }
    }
}
