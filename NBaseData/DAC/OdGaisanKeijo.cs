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

namespace NBaseData.DAC
{
    [DataContract()]
    public class OdGaisanKeijo
    {
        #region データメンバ

        /// <summary>
        /// 概算計上ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("GAISAN_KEIJO_ID")]
        public int GaisanKeijoID { get; set; }

        /// <summary>
        /// 概算計上月
        /// </summary>
        [DataMember]
        [ColumnAttribute("GAISAN_KEIJO_ZUKI")]
        public string GaisanKeijoZuki { get; set; }

        /// <summary>
        /// 受領ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_ID")]
        public string OdJryID { get; set; }

        /// <summary>
        /// ユーザID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_USER_ID")]
        public string MsUserID { get; set; }

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
        /// 支払ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_SHR_ID")]
        public string OdShrID { get; set; }

        /// <summary>
        /// 支払金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("AMOUNT")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 請求値引き額
        /// </summary>
        [DataMember]
        [ColumnAttribute("NEBIKI_AMOUNT")]
        public decimal NebikiAmount { get; set; }

        /// <summary>
        /// 消費税額
        /// </summary>
        [DataMember]
        [ColumnAttribute("TAX")]
        public decimal Tax { get; set; }

        /// <summary>
        /// 科目NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("KAMOKU_NO")]
        public string KamokuNo { get; set; }

        /// <summary>
        /// 内訳科目NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("UTIWAKE_KAMOKU_NO")]
        public string UtiwakeKamokuNo { get; set; }



        /// <summary>
        /// 手配依頼種別ID
        /// </summary>
        [DataMember]
        public string ThiIraiSbtID { get; set; }
        
        /// <summary>
        /// 手配依頼詳細種別ID
        /// </summary>
        [DataMember]
        public string ThiIraiShousaiID { get; set; }

        /// <summary>
        /// 手配内容
        /// </summary>
        [DataMember]
        public string Naiyou { get; set; }

        #endregion

        public OdGaisanKeijo()
        {
        }

        public static List<OdGaisanKeijo> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdGaisanKeijo), MethodBase.GetCurrentMethod());
            List<OdGaisanKeijo> ret = new List<OdGaisanKeijo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdGaisanKeijo> mapping = new MappingBase<OdGaisanKeijo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //ユーザーマスタ
        public static List<OdGaisanKeijo> GetRecordsByMsUserID(NBaseData.DAC.MsUser loginUser, string ms_user_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdGaisanKeijo), MethodBase.GetCurrentMethod());
            List<OdGaisanKeijo> ret = new List<OdGaisanKeijo>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_USER_ID", ms_user_id));

            MappingBase<OdGaisanKeijo> mapping = new MappingBase<OdGaisanKeijo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static OdGaisanKeijo GetRecord(NBaseData.DAC.MsUser loginUser, int GaisanKeijoID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdGaisanKeijo), MethodBase.GetCurrentMethod());
            List<OdGaisanKeijo> ret = new List<OdGaisanKeijo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("GAISAN_KEIJO_ID", GaisanKeijoID));
            MappingBase<OdGaisanKeijo> mapping = new MappingBase<OdGaisanKeijo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static OdGaisanKeijo GetRecordByOdJryID(NBaseData.DAC.MsUser loginUser, string OdJryID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdGaisanKeijo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdGaisanKeijo), "ByOdJryID");

            List<OdGaisanKeijo> ret = new List<OdGaisanKeijo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdGaisanKeijo> mapping = new MappingBase<OdGaisanKeijo>();
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static string GetLatestNengetsu(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdGaisanKeijo), MethodBase.GetCurrentMethod());

            List<OdGaisanKeijo> ret = new List<OdGaisanKeijo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdGaisanKeijo> mapping = new MappingBase<OdGaisanKeijo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0].GaisanKeijoZuki;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {

            return InsertRecord(null,loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdGaisanKeijo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            //Params.Add(new DBParameter("GAISAN_KEIJO_ID", GaisanKeijoID)); // SQLにてSEQより割当(2009/08/30:aki)
            Params.Add(new DBParameter("GAISAN_KEIJO_ZUKI", GaisanKeijoZuki));
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            Params.Add(new DBParameter("OD_SHR_ID", OdShrID));
            Params.Add(new DBParameter("AMOUNT", Amount));
            Params.Add(new DBParameter("NEBIKI_AMOUNT", NebikiAmount));
            Params.Add(new DBParameter("TAX", Tax));
            Params.Add(new DBParameter("KAMOKU_NO", KamokuNo));
            Params.Add(new DBParameter("UTIWAKE_KAMOKU_NO", UtiwakeKamokuNo));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdGaisanKeijo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("GAISAN_KEIJO_ZUKI", GaisanKeijoZuki));
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));
            Params.Add(new DBParameter("OD_SHR_ID", OdShrID));
            Params.Add(new DBParameter("AMOUNT", Amount));
            Params.Add(new DBParameter("NEBIKI_AMOUNT", NebikiAmount));
            Params.Add(new DBParameter("TAX", Tax));
            Params.Add(new DBParameter("KAMOKU_NO", KamokuNo));
            Params.Add(new DBParameter("UTIWAKE_KAMOKU_NO", UtiwakeKamokuNo));
            Params.Add(new DBParameter("MS_USER_ID", MsUserID));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("GAISAN_KEIJO_ID", GaisanKeijoID));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
