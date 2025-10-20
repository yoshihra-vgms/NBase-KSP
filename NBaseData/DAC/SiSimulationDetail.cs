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
    [TableAttribute("SI_SIMULATION_DETAIL")]
    public class SiSimulationDetail : ISyncTable
    {
        public enum EnumType { 乗船者, 予定者 };
        //public enum EnumSignOnOff { 乗船, 下船 };

        #region データメンバ

        /// <summary>
        /// 交代計画詳細ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_SIMULATION_DETAIL_ID", true)]
        public string SiSimulationDetailID { get; set; }

        /// <summary>
        /// 交代計画ヘッダID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_SIMULATION_HEADER_ID")]
        public string SiSimulationHeaderID { get; set; }

        /// <summary>
        /// TYPE ０：乗船者、１：予定者
        /// </summary>
        [DataMember]
        [ColumnAttribute("TYPE")]
        public int Type { get; set; }


        /// <summary>
        /// 船員ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

        /// <summary>
        /// 職名ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID")]
        public int MsSiShokumeiID { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("YEARS_WITH_OPERATOR")]
        public decimal YearsWithOperator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("YEARS_IN_RANK")]
        public decimal YearsInRank { get; set; }

        /// <summary>
        /// 船員カードID（SignOn）
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_ON_SI_CARD_ID")]
        public string SignOnSiCardID { get; set; }

        /// <summary>
        /// 船員カードID（SignOff）
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_OFF_SI_CARD_ID")]
        public string SignOffSiCardID { get; set; }

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



        /// <summary>
        /// 船員名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_NAME")]
        public string SeninName { get; set; }


        /// <summary>
        /// 乗船予定者名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_ON_SENIN_NAME")]
        public string SignOnSeninName { get; set; }

        /// <summary>
        /// 乗船予定日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_ON_YOTEI")]
        public DateTime SignOnYotei { get; set; }

        /// <summary>
        /// 乗船日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_ON_DATE")]
        public DateTime SignOnDate { get; set; }

        /// <summary>
        /// 出国日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_ON_DEPARTURE_DAY")]
        public DateTime SignOnDepartureDay { get; set; }



        /// <summary>
        /// 下船予定者(船員ID)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_OFF_SENIN_ID")]
        public int SignOffSeninId { get; set; }

        /// <summary>
        /// 下船予定者名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_OFF_SENIN_NAME")]
        public string SignOffSeninName { get; set; }

        /// <summary>
        /// 下船予定日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_OFF_YOTEI")]
        public DateTime SignOffYotei { get; set; }

        /// <summary>
        /// 下船日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_OFF_DATE")]
        public DateTime SignOffDate { get; set; }

        /// <summary>
        /// 帰国日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_OFF_RETURN_DAY")]
        public DateTime SignOffReturnDay { get; set; }

        /// <summary>
        /// 下船事由
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_OFF_REASON")]
        public string SignOffReason { get; set; }


        [DataMember]
        public List<SiSimulationDetailItem> Items;

        [DataMember]
        public string CompanyName { get; set; }


        public string TmpID { get; set; }



        // 2015.09   (5) 乗下船交代表示方法変更およびデータ出力（SignOnOffRecord)
        /// <summary>
        /// SignOn備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_ON_REMARKS")]
        public string SignOnRemarks { get; set; }

        /// <summary>
        /// SignOff備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("SIGN_OFF_REMARKS")]
        public string SignOffRemarks { get; set; }

        /// <summary>
        /// Promotion備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("PROMOTION_REMARKS")]
        public string PromotionRemarks { get; set; }


        // 2016.01.07  追加
        /// <summary>
        /// 生年月日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_BIRTHDAY")]
        public DateTime SeninBirthday { get; set; }


        #endregion


        public SiSimulationDetail()
        {
            this.Items = new List<SiSimulationDetailItem>();
        }


        public static SiSimulationDetail GetRecord交代予定(MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSimulationDetail), "GetRecords");

            List<SiSimulationDetail> ret = new List<SiSimulationDetail>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSimulationDetail> mapping = new MappingBase<SiSimulationDetail>();
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSimulationDetail), "ByMsSeninId");
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            Params.Add(new DBParameter("TYPE", (int)EnumType.予定者));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }
        
        

        public static List<SiSimulationDetail> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSimulationDetail), MethodBase.GetCurrentMethod());

            List<SiSimulationDetail> ret = new List<SiSimulationDetail>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSimulationDetail> mapping = new MappingBase<SiSimulationDetail>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static List<SiSimulationDetail> GetRecordsByHeaderId(MsUser loginUser, string siSimulationHeaderId)
        {
            return GetRecordsByHeaderId(null, loginUser, siSimulationHeaderId);
        }
        public static List<SiSimulationDetail> GetRecordsByHeaderId(DBConnect dbConnect, MsUser loginUser, string siSimulationHeaderId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSimulationDetail), "GetRecords");

            List<SiSimulationDetail> ret = new List<SiSimulationDetail>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSimulationDetail> mapping = new MappingBase<SiSimulationDetail>();

            if (siSimulationHeaderId != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSimulationDetail), "BySiSimulationHeaderID");
                Params.Add(new DBParameter("SI_SIMULATION_HEADER_ID", siSimulationHeaderId));
            }
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            foreach (SiSimulationDetail d in ret)
            {
                d.Items = SiSimulationDetailItem.GetRecordsByDetailId(dbConnect, loginUser, d.SiSimulationDetailID);
            }

            return ret;
        }

        public static List<SiSimulationDetail> GetRecordsBySiCardId(DBConnect dbConnect, MsUser loginUser, string siCardId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSimulationDetail), "GetRecords");

            List<SiSimulationDetail> ret = new List<SiSimulationDetail>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSimulationDetail> mapping = new MappingBase<SiSimulationDetail>();

            if (siCardId != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSimulationDetail), "BySiCardID");
                Params.Add(new DBParameter("SI_CARD_ID", siCardId));
            }
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

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

            Params.Add(new DBParameter("SI_SIMULATION_DETAIL_ID", SiSimulationDetailID));
            Params.Add(new DBParameter("SI_SIMULATION_HEADER_ID", SiSimulationHeaderID));
            
            Params.Add(new DBParameter("TYPE", Type));
            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));

            Params.Add(new DBParameter("YEARS_WITH_OPERATOR", YearsWithOperator));
            Params.Add(new DBParameter("YEARS_IN_RANK", YearsInRank));
            
            Params.Add(new DBParameter("SIGN_ON_SI_CARD_ID", SignOnSiCardID));
            Params.Add(new DBParameter("SIGN_OFF_SI_CARD_ID", SignOffSiCardID));
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

            Params.Add(new DBParameter("SI_SIMULATION_HEADER_ID", SiSimulationHeaderID));
            
            Params.Add(new DBParameter("TYPE", Type));
            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));

            Params.Add(new DBParameter("YEARS_WITH_OPERATOR", YearsWithOperator));
            Params.Add(new DBParameter("YEARS_IN_RANK", YearsInRank));

            Params.Add(new DBParameter("SIGN_ON_SI_CARD_ID", SignOnSiCardID));
            Params.Add(new DBParameter("SIGN_OFF_SI_CARD_ID", SignOffSiCardID));
            Params.Add(new DBParameter("REMARKS", Remarks));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_SIMULATION_DETAIL_ID", SiSimulationDetailID));
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
            Params.Add(new DBParameter("PK", SiSimulationDetailID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiSimulationDetailID == null;
        }
    }
}
