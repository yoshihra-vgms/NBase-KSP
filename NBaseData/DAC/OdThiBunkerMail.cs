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
    [TableAttribute("OD_THI_BUNKER_MAIL")]
    public class OdThiBunkerMail
    {
        #region データメンバ

        /// <summary>
        /// 手配依頼ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_ID", true)]
        public string OdThiID { get; set; }

        /// <summary>
        /// ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("STATUS")]
        public int Status { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 手配依頼日
        /// </summary>
        [DataMember]
        [ColumnAttribute("THI_IRAI_DATE")]
        public DateTime ThiIraiDate { get; set; }

        /// <summary>
        /// 手配依頼番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("TEHAI_IRAI_NO")]
        public string TehaiIraiNo { get; set; }

        /// <summary>
        /// 手配更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("THI_RENEW_DATE")]
        public DateTime ThiRenewDate { get; set; }

        /// <summary>
        /// 手配更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("THI_RENEW_USER_ID")]
        public string ThiRenewUserID { get; set; }
        
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
        /// 本文
        /// </summary>
        [DataMember]
        [ColumnAttribute("MAIL_BODY")]
        public string MailBody { get; set; }

        /// <summary>
        /// 送信フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("MAIL_SEND_FLAG")]
        public int MailSendFlag { get; set; }
 
        #endregion

        public static int 未送信 = 0;
        public static int 送信済み = 1;


        /// コンストラクタ
        /// </summary>
        public OdThiBunkerMail()
        {
        }

        public static OdThiBunkerMail GetRecord(NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            return GetRecord(null, loginUser, OdThiID);
        }
        public static OdThiBunkerMail GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdThiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiBunkerMail), "GetRecord");

            List<OdThiBunkerMail> ret = new List<OdThiBunkerMail>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdThiBunkerMail> mapping = new MappingBase<OdThiBunkerMail>();
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdThiBunkerMail), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_THI_ID", OdThiID));
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("THI_IRAI_DATE", ThiIraiDate));
            Params.Add(new DBParameter("TEHAI_IRAI_NO", TehaiIraiNo));
            Params.Add(new DBParameter("THI_RENEW_DATE", ThiRenewDate));
            Params.Add(new DBParameter("THI_RENEW_USER_ID", ThiRenewUserID));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            string mailBody = MailBody;
            if (MailBody.Length > 2000)
            {
                mailBody = MailBody.Substring(0, 2000);
            }
            Params.Add(new DBParameter("MAIL_BODY", mailBody));
            Params.Add(new DBParameter("MAIL_SEND_FLAG", MailSendFlag));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

    }
}
