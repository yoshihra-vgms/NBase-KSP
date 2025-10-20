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
    [TableAttribute("SI_REQUIRED_NUMBER_OF_DAYS")]
    public class SiRequiredNumberOfDays : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 必須日数ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_REQUIRED_NUMBER_OF_DAYS_ID", true)]
        public string SiRequiredNumberOfDaysID { get; set; }

        /// <summary>
        /// 種別　陸上休暇（合計）、陸上休暇A
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIND")]
        public int Kind { get; set; }

        /// <summary>
        /// 船員会社ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_COMPANY_ID")]
        public string MsSeninCompanyID { get; set; }

        /// <summary>
        /// 日数
        /// </summary>
        [DataMember]
        [ColumnAttribute("DAYS")]
        public int Days { get; set; }


        
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




        public SiRequiredNumberOfDays()
        {
            this.SiRequiredNumberOfDaysID = null;
        }



        public static List<SiRequiredNumberOfDays> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            return GetRecords(null, loginUser);
        }

        public static List<SiRequiredNumberOfDays> GetRecords(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiRequiredNumberOfDays), "GetRecords");

            List<SiRequiredNumberOfDays> ret = new List<SiRequiredNumberOfDays>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiRequiredNumberOfDays> mapping = new MappingBase<SiRequiredNumberOfDays>();

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

            Params.Add(new DBParameter("SI_REQUIRED_NUMBER_OF_DAYS_ID", SiRequiredNumberOfDaysID));

            Params.Add(new DBParameter("KIND", Kind));
            Params.Add(new DBParameter("MS_SENIN_COMPANY_ID", MsSeninCompanyID));
            Params.Add(new DBParameter("DAYS", Days));

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

            Params.Add(new DBParameter("KIND", Kind));
            Params.Add(new DBParameter("MS_SENIN_COMPANY_ID", MsSeninCompanyID));
            Params.Add(new DBParameter("DAYS", Days));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_REQUIRED_NUMBER_OF_DAYS_ID", SiRequiredNumberOfDaysID));
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
            Params.Add(new DBParameter("PK", SiRequiredNumberOfDaysID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiRequiredNumberOfDaysID == null;
        }
    }
}
