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
    public class OdHachuMail
    {
        #region データメンバ

        /// <summary>
        /// 発注メール情報ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_HACHU_MAIL_ID")]
        public string OdHachuMailID { get; set; }

        /// <summary>
        /// 見積回答ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_MK_ID")]
        public string OdMkID { get; set; }

        /// <summary>
        /// 担当者
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANTOUSHA")]
        public string Tantousha { get; set; }

        /// <summary>
        /// 担当者メールアドレス
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANTOU_MAIL_ADDRESS")]
        public string TantouMailAddress { get; set; }

        /// <summary>
        /// 件名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SUBJECT")]
        public string Subject { get; set; }

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
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }


        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OdHachuMail()
        {
        }

        public static OdHachuMail GetRecord(NBaseData.DAC.MsUser loginUser, string OdMkID, string Tantousha, string TantouMailAdress, string Subject)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdHachuMail), "GetRecord");

            List<OdHachuMail> ret = new List<OdHachuMail>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MK_ID", OdMkID));
            Params.Add(new DBParameter("TANTOUSHA", Tantousha));
            Params.Add(new DBParameter("TANTOU_MAIL_ADDRESS", TantouMailAdress));
            Params.Add(new DBParameter("SUBJECT", Subject));
            MappingBase<OdHachuMail> mapping = new MappingBase<OdHachuMail>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }
        public static List<OdHachuMail> GetRecords(NBaseData.DAC.MsUser loginUser, string OdMkID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdHachuMail), "GetRecords");

            List<OdHachuMail> ret = new List<OdHachuMail>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MK_ID", OdMkID));
            MappingBase<OdHachuMail> mapping = new MappingBase<OdHachuMail>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdHachuMail), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_HACHU_MAIL_ID", OdHachuMailID));
            Params.Add(new DBParameter("OD_MK_ID", OdMkID));
            Params.Add(new DBParameter("TANTOUSHA", Tantousha));
            Params.Add(new DBParameter("TANTOU_MAIL_ADDRESS", TantouMailAddress));
            Params.Add(new DBParameter("SUBJECT", Subject));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdHachuMail), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MK_ID", OdMkID));
            Params.Add(new DBParameter("TANTOUSHA", Tantousha));
            Params.Add(new DBParameter("TANTOU_MAIL_ADDRESS", TantouMailAddress));
            Params.Add(new DBParameter("SUBJECT", Subject));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("OD_HACHU_MAIL_ID", OdHachuMailID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
