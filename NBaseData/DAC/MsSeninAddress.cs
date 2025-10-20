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
using NBaseUtil;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("MS_SENIN_ADDRESS")]
    public class MsSeninAddress : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }


        //==================================
        // 現住所
        //==================================

        /// <summary>
        /// 現住所：郵便番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("ZIP_CODE")]
        public string ZipCode { get; set; }

        /// <summary>
        ///  現住所：都道府県
        /// </summary>
        [DataMember]
        [ColumnAttribute("PREFECTURES")]
        public string Prefectures { get; set; }

        /// <summary>
        /// 現住所：市区町村名
        /// </summary>
        [DataMember]
        [ColumnAttribute("CITY_TOWN")]
        public string CityTown { get; set; }

        /// <summary>
        /// 現住所：番地、町名
        /// </summary>
        [DataMember]
        [ColumnAttribute("STREET")]
        public string Street { get; set; }


        //==================================
        // 住民票
        //==================================

        /// <summary>
        /// 住民票：郵便番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("A_ZIP_CODE")]
        public string A_ZipCode { get; set; }

        /// <summary>
        ///  住民票：都道府県
        /// </summary>
        [DataMember]
        [ColumnAttribute("A_PREFECTURES")]
        public string A_Prefectures { get; set; }

        /// <summary>
        /// 住民票：市区町村名
        /// </summary>
        [DataMember]
        [ColumnAttribute("A_CITY_TOWN")]
        public string A_CityTown { get; set; }

        /// <summary>
        /// 住民票：番地、町名
        /// </summary>
        [DataMember]
        [ColumnAttribute("A_STREET")]
        public string A_Street { get; set; }



        //==================================
        // 本籍
        //==================================

        /// <summary>
        /// 本籍：郵便番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("P_ZIP_CODE")]
        public string P_ZipCode { get; set; }

        /// <summary>
        ///  本籍：都道府県
        /// </summary>
        [DataMember]
        [ColumnAttribute("P_PREFECTURES")]
        public string P_Prefectures { get; set; }

        /// <summary>
        /// 本籍：市区町村名
        /// </summary>
        [DataMember]
        [ColumnAttribute("P_CITY_TOWN")]
        public string P_CityTown { get; set; }

        /// <summary>
        /// 本籍：番地、町名
        /// </summary>
        [DataMember]
        [ColumnAttribute("P_STREET")]
        public string P_Street { get; set; }




        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("REMARKS")]
        public string Remarks { get; set; }


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

        public bool EditFlag = false;


        public string PrefecturesStr { get; set; }

        public string A_PrefecturesStr { get; set; }

        public string P_PrefecturesStr { get; set; }


        public void MakeFullAddress(List<MsSiOptions> prefecturesList)
        {
            PrefecturesStr = "";
            var p = prefecturesList.Where(o => o.MsSiOptionsID == Prefectures).FirstOrDefault();
            if (p != null)
            {
                PrefecturesStr += p.Name;
            }
            if (PrefecturesStr.Length > 0)
            {
                PrefecturesStr += " ";
            }
            PrefecturesStr += CityTown;


            A_PrefecturesStr = "";
            p = prefecturesList.Where(o => o.MsSiOptionsID == A_Prefectures).FirstOrDefault();
            if (p != null)
            {
                A_PrefecturesStr += p.Name;
            }
            if (A_PrefecturesStr.Length > 0)
            {
                A_PrefecturesStr += " ";
            }
            A_PrefecturesStr += A_CityTown;


            P_PrefecturesStr = "";
            p = prefecturesList.Where(o => o.MsSiOptionsID == P_Prefectures).FirstOrDefault();
            if (p != null)
            {
                P_PrefecturesStr += p.Name;
            }
            if (P_PrefecturesStr.Length > 0)
            {
                P_PrefecturesStr += " ";
            }
            P_PrefecturesStr += P_CityTown;
        }




        public static List<MsSeninAddress> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSeninAddress), "GetRecords");

            List<MsSeninAddress> ret = new List<MsSeninAddress>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSeninAddress> mapping = new MappingBase<MsSeninAddress>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }




        public static MsSeninAddress GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSeninAddress), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSeninAddress), "ByMsSeninID");

            List<MsSeninAddress> ret = new List<MsSeninAddress>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSeninAddress> mapping = new MappingBase<MsSeninAddress>();
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
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

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));

            Params.Add(new DBParameter("ZIP_CODE", ZipCode));
            Params.Add(new DBParameter("PREFECTURES", Prefectures));
            Params.Add(new DBParameter("CITY_TOWN", CityTown));
            Params.Add(new DBParameter("STREET", Street));

            Params.Add(new DBParameter("P_ZIP_CODE", P_ZipCode));
            Params.Add(new DBParameter("P_PREFECTURES", P_Prefectures));
            Params.Add(new DBParameter("P_CITY_TOWN", P_CityTown));
            Params.Add(new DBParameter("P_STREET", P_Street));

            Params.Add(new DBParameter("A_ZIP_CODE", A_ZipCode));
            Params.Add(new DBParameter("A_PREFECTURES", A_Prefectures));
            Params.Add(new DBParameter("A_CITY_TOWN", A_CityTown));
            Params.Add(new DBParameter("A_STREET", A_Street));

            Params.Add(new DBParameter("REMARKS", Remarks));

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

            Params.Add(new DBParameter("ZIP_CODE", ZipCode));
            Params.Add(new DBParameter("PREFECTURES", Prefectures));
            Params.Add(new DBParameter("CITY_TOWN", CityTown));
            Params.Add(new DBParameter("STREET", Street));

            Params.Add(new DBParameter("P_ZIP_CODE", P_ZipCode));
            Params.Add(new DBParameter("P_PREFECTURES", P_Prefectures));
            Params.Add(new DBParameter("P_CITY_TOWN", P_CityTown));
            Params.Add(new DBParameter("P_STREET", P_Street));

            Params.Add(new DBParameter("A_ZIP_CODE", A_ZipCode));
            Params.Add(new DBParameter("A_PREFECTURES", A_Prefectures));
            Params.Add(new DBParameter("A_CITY_TOWN", A_CityTown));
            Params.Add(new DBParameter("A_STREET", A_Street));

            Params.Add(new DBParameter("REMARKS", Remarks));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
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
            Params.Add(new DBParameter("PK", MsSeninID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return MsSeninID == 0;
        }
    }
}
