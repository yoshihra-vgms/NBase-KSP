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
    [TableAttribute("MS_KICHI")]
    public class MsKichi : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 基地ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KICHI_ID", true)]
        public string MsKichiId { get; set; }

        /// <summary>
        /// 基地NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("KICHI_NO")]
        public string KichiNo { get; set; }

        /// <summary>
        /// 基地名
        /// </summary>
        [DataMember]
        [ColumnAttribute("KICHI_NAME")]
        public string KichiName { get; set; }

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




        [DataMember]
        [ColumnAttribute("MS_BASHO_ID")]
        public string MsBashoID { get; set; }

        [DataMember]
        [ColumnAttribute("FOR_NYUKO_TO_CHAKUSAN")]
        public decimal ForNyukoToChakusan { get; set; }

        [DataMember]
        [ColumnAttribute("AVAILABLE_FOR_CHAKUSAN_FROM")]
        public decimal AvailableForChakusanFrom { get; set; }

        [DataMember]
        [ColumnAttribute("AVAILABLE_FOR_CHAKUSAN_TO")]
        public decimal AvailableForChakusanTo { get; set; }

        [DataMember]
        [ColumnAttribute("FOR_CHAKUSAN_TO_NIYAKU")]
        public decimal ForChakusanToNiyaku { get; set; }

        [DataMember]
        [ColumnAttribute("AVAILABLE_FOR_NIYAKU_FROM")]
        public decimal AvailableForNiyakuFrom { get; set; }

        [DataMember]
        [ColumnAttribute("AVAILABLE_FOR_NIYAKU_TO")]
        public decimal AvailableForNiyakuTo { get; set; }

        [DataMember]
        [ColumnAttribute("FOR_NIYAKU_TO_RISAN")]
        public decimal ForNiyakuToRisan { get; set; }

        [DataMember]
        [ColumnAttribute("AVAILABLE_FOR_RISAN_FROM")]
        public decimal AvailableForRisanFrom { get; set; }

        [DataMember]
        [ColumnAttribute("AVAILABLE_FOR_RISAN_TO")]
        public decimal AvailableForRisanTo { get; set; }

        [DataMember]
        [ColumnAttribute("FOR_RISAN_TO_SHUKOU")]
        public decimal ForRisanToShukou{ get; set; }

        [DataMember]
        [ColumnAttribute("AVAILABLE_FOR_SHUKOU_FROM")]
        public decimal AvailableForShukouFrom { get; set; }

        [DataMember]
        [ColumnAttribute("AVAILABLE_FOR_SHUKOU_TO")]
        public decimal AvailableForShukouTo { get; set; }


        /// <summary>
        /// 積揚可能貨物
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARGOS")]
        public string Cargos { get; set; }


        #endregion

        public MsKichi()
        {
            KichiName = "";
        }

        public override string ToString()
        {
            return KichiName;
        }
        public static List<MsKichi> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKichi), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKichi), "OrderBy");
            List<MsKichi> ret = new List<MsKichi>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsKichi> mapping = new MappingBase<MsKichi>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsKichi> GetRecordsByKichiNoKichiName(NBaseData.DAC.MsUser loginUser, string kichiNo, string kichiName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKichi), "GetRecords");
            SQL += SqlMapper.SqlMapper.GetSql(typeof(MsKichi), "ByKichiNoKichiName");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKichi), "OrderBy");
            List<MsKichi> ret = new List<MsKichi>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("KICHI_NO", "%" + kichiNo + "%"));
            Params.Add(new DBParameter("KICHI_NAME", "%" + kichiName + "%"));

            MappingBase<MsKichi> mapping = new MappingBase<MsKichi>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsKichi GetRecordByBerthName(NBaseData.DAC.MsUser loginUser, string kichiName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKichi), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKichi), "ByKichiName");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKichi), "OrderBy");
            List<MsKichi> ret = new List<MsKichi>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("KICHI_NAME", kichiName));
            MappingBase<MsKichi> mapping = new MappingBase<MsKichi>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static MsKichi GetRecordByKichiNo(NBaseData.DAC.MsUser loginUser, string kichiNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKichi), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKichi), "ByKichiNo");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKichi), "OrderBy");
            List<MsKichi> ret = new List<MsKichi>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("KICHI_NO", kichiNo));
            MappingBase<MsKichi> mapping = new MappingBase<MsKichi>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
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
            Params.Add(new DBParameter("MS_KICHI_ID", MsKichiId));
            Params.Add(new DBParameter("KICHI_NO", KichiNo));
            Params.Add(new DBParameter("KICHI_NAME", KichiName));

            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            {
                Params.Add(new DBParameter("MS_BASHO_ID", MsBashoID));
                Params.Add(new DBParameter("FOR_NYUKO_TO_CHAKUSAN", ForNyukoToChakusan));
                Params.Add(new DBParameter("AVAILABLE_FOR_CHAKUSAN_FROM", AvailableForChakusanFrom));
                Params.Add(new DBParameter("AVAILABLE_FOR_CHAKUSAN_TO", AvailableForChakusanTo));
                Params.Add(new DBParameter("FOR_CHAKUSAN_TO_NIYAKU", ForChakusanToNiyaku));
                Params.Add(new DBParameter("AVAILABLE_FOR_NIYAKU_FROM", AvailableForNiyakuFrom));
                Params.Add(new DBParameter("AVAILABLE_FOR_NIYAKU_TO", AvailableForNiyakuTo));
                Params.Add(new DBParameter("FOR_NIYAKU_TO_RISAN", ForNiyakuToRisan));
                Params.Add(new DBParameter("AVAILABLE_FOR_RISAN_FROM", AvailableForRisanFrom));
                Params.Add(new DBParameter("AVAILABLE_FOR_RISAN_TO", AvailableForRisanTo));
                Params.Add(new DBParameter("FOR_RISAN_TO_SHUKOU", ForRisanToShukou));
                Params.Add(new DBParameter("AVAILABLE_FOR_SHUKOU_FROM", AvailableForShukouFrom));
                Params.Add(new DBParameter("AVAILABLE_FOR_SHUKOU_TO", AvailableForShukouTo));
                Params.Add(new DBParameter("CARGOS", Cargos));
            }

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
            Params.Add(new DBParameter("KICHI_NO", KichiNo));
            Params.Add(new DBParameter("KICHI_NAME", KichiName));

            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            {
                Params.Add(new DBParameter("MS_BASHO_ID", MsBashoID));
                Params.Add(new DBParameter("FOR_NYUKO_TO_CHAKUSAN", ForNyukoToChakusan));
                Params.Add(new DBParameter("AVAILABLE_FOR_CHAKUSAN_FROM", AvailableForChakusanFrom));
                Params.Add(new DBParameter("AVAILABLE_FOR_CHAKUSAN_TO", AvailableForChakusanTo));
                Params.Add(new DBParameter("FOR_CHAKUSAN_TO_NIYAKU", ForChakusanToNiyaku));
                Params.Add(new DBParameter("AVAILABLE_FOR_NIYAKU_FROM", AvailableForNiyakuFrom));
                Params.Add(new DBParameter("AVAILABLE_FOR_NIYAKU_TO", AvailableForNiyakuTo));
                Params.Add(new DBParameter("FOR_NIYAKU_TO_RISAN", ForNiyakuToRisan));
                Params.Add(new DBParameter("AVAILABLE_FOR_RISAN_FROM", AvailableForRisanFrom));
                Params.Add(new DBParameter("AVAILABLE_FOR_RISAN_TO", AvailableForRisanTo));
                Params.Add(new DBParameter("FOR_RISAN_TO_SHUKOU", ForRisanToShukou));
                Params.Add(new DBParameter("AVAILABLE_FOR_SHUKOU_FROM", AvailableForShukouFrom));
                Params.Add(new DBParameter("AVAILABLE_FOR_SHUKOU_TO", AvailableForShukouTo));
                Params.Add(new DBParameter("CARGOS", Cargos));
            }

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_KICHI_ID", MsKichiId));
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
        //    Params.Add(new DBParameter("PK", MsKichiId));

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
            Params.Add(new DBParameter("PK", MsKichiId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
