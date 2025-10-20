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
    [TableAttribute("SI_SHOBYO")]
    public class SiShobyo : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 傷病ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_SHOBYO_ID", true)]
        public string SiShobyoID { get; set; }

        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

        /// <summary>
        /// 保険番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("HOKEN_NO")]
        public string HokenNo { get; set; }

        /// <summary>
        /// 職名ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID")]
        public int MsSiShokumeiID { get; set; }

        /// <summary>
        /// 種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIND")]
        public int Kind { get; set; }     
    
        /// <summary>
        /// 船ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }


        /// <summary>
        /// ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("STATUS")]
        public int Status { get; set; }   

        /// <summary>
        /// 傷病名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOBYO_NAME")]
        public string ShobyoName { get; set; }
               
        /// <summary>
        /// 期間（開始日）
        /// </summary>
        [DataMember]
        [ColumnAttribute("FROM_DATE")]
        public DateTime FromDate { get; set; }

        /// <summary>
        /// 期間（終了日）
        /// </summary>
        [DataMember]
        [ColumnAttribute("TO_DATE")]
        public DateTime ToDate { get; set; }

        /// <summary>
        /// 等級
        /// </summary>
        [DataMember]
        [ColumnAttribute("TOKYU")]
        public int Tokyu { get; set; }

        /// <summary>
        /// 日額
        /// </summary>
        [DataMember]
        [ColumnAttribute("NITIGAKU")]
        public int Nitigaku { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("KINGAKU")]
        public int Kingaku { get; set; }

        /// <summary>
        /// 口座
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOUZA")]
        public string Kouza { get; set; }



        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEND_DOCUMENT")]
        public DateTime SendDocument { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("DOCUMENT_RETURN")]
        public DateTime DocumentReturn { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("FILING_DATE")]
        public DateTime FilingDate { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("NOTIFICATION")]
        public DateTime Notification { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("ADVANCE_VOUCHER")]
        public DateTime AdvanceVoucher { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("DEPOSIT_SLIP")]
        public DateTime DepositSlip { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MONEY_TRANSFER")]
        public DateTime MoneyTransfer { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MAIL_TO_PRINCIPAL")]
        public DateTime MailToPrincipal { get; set; }
        
        
        
        #region 共通項目

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

        #endregion

        /// <summary>
        /// 添付ファイル
        /// </summary>
        [DataMember]
        public List<SiShobyoAttachFile> AttachFiles { get; set; }



        public static string[] KIND = new string[] { "傷病", "労災" };
        public static string[] STATUS = new string[] { "傷病（手続）中", "退職後支給", "受取人本人以外等", "完了" };


        public SiShobyo()
        {
            this.MsSeninID = Int32.MinValue;
            this.MsSiShokumeiID = Int32.MinValue;
            this.AttachFiles = new List<SiShobyoAttachFile>();

            this.HokenNo = "";
        }
 

        public static List<SiShobyo> GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiShobyo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiShobyo), "ByMsSeninID");
            SQL = SQL.Replace("#APPEND_JOIN_STR#", "");

            List<SiShobyo> ret = new List<SiShobyo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiShobyo> mapping = new MappingBase<SiShobyo>();
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            foreach (SiShobyo m in ret)
            {
                if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
                {
                    m.AttachFiles = SiShobyoAttachFile.GetRecordByKoushuID(loginUser, m.SiShobyoID);
                }
            }

            return ret;
        }


        public static List<SiShobyo> SearchRecords(MsUser loginUser, DateTime fromDate, DateTime toDate, int msSiShukumeiId, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiShobyo), "GetRecords");

            ParameterConnection Params = new ParameterConnection();

            if (fromDate != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiShobyo), "ByFromDate");
                Params.Add(new DBParameter("FROM_DATE", fromDate));
            }

            if (toDate != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiShobyo), "ByToDate");
                Params.Add(new DBParameter("TO_DATE", toDate));
            }

            if (msSiShukumeiId > 0)
            {
                SQL = SQL.Replace("#APPEND_JOIN_STR#", SqlMapper.SqlMapper.GetSql(typeof(SiShobyo), "ByMsSiShokumeiID"));
                Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", msSiShukumeiId));
            }
            else
            {
                SQL = SQL.Replace("#APPEND_JOIN_STR#", "");
            }

            if (msSeninId > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiShobyo), "ByMsSeninID");
                Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            }

            List<SiShobyo> ret = new List<SiShobyo>();
            MappingBase<SiShobyo> mapping = new MappingBase<SiShobyo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);


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

            Params.Add(new DBParameter("SI_SHOBYO_ID", SiShobyoID));

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("HOKEN_NO", HokenNo));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("KIND", Kind));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("SHOBYO_NAME", ShobyoName));
            Params.Add(new DBParameter("FROM_DATE", FromDate));
            Params.Add(new DBParameter("TO_DATE", ToDate));
            Params.Add(new DBParameter("TOKYU", Tokyu));
            Params.Add(new DBParameter("NITIGAKU", Nitigaku));
            Params.Add(new DBParameter("KINGAKU", Kingaku));
            Params.Add(new DBParameter("KOUZA", Kouza));

            Params.Add(new DBParameter("SEND_DOCUMENT", SendDocument));
            Params.Add(new DBParameter("DOCUMENT_RETURN", DocumentReturn));
            Params.Add(new DBParameter("FILING_DATE", FilingDate));
            Params.Add(new DBParameter("NOTIFICATION", Notification));
            Params.Add(new DBParameter("ADVANCE_VOUCHER", AdvanceVoucher));
            Params.Add(new DBParameter("DEPOSIT_SLIP", DepositSlip));
            Params.Add(new DBParameter("MONEY_TRANSFER", MoneyTransfer));
            Params.Add(new DBParameter("MAIL_TO_PRINCIPAL", MailToPrincipal));

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

            foreach (SiShobyoAttachFile a in AttachFiles)
            {
                a.SiShobyoID = SiShobyoID;
                a.RenewDate = RenewDate;
                a.RenewUserID = RenewUserID;
                bool ret = a.InsertRecord(dbConnect, loginUser);
                if (ret == false)
                    return false;
            }

            return true;
        }

        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("HOKEN_NO", HokenNo));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("KIND", Kind));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("STATUS", Status));
            Params.Add(new DBParameter("SHOBYO_NAME", ShobyoName));
            Params.Add(new DBParameter("FROM_DATE", FromDate));
            Params.Add(new DBParameter("TO_DATE", ToDate));
            Params.Add(new DBParameter("TOKYU", Tokyu));
            Params.Add(new DBParameter("NITIGAKU", Nitigaku));
            Params.Add(new DBParameter("KINGAKU", Kingaku));
            Params.Add(new DBParameter("KOUZA", Kouza));

            Params.Add(new DBParameter("SEND_DOCUMENT", SendDocument));
            Params.Add(new DBParameter("DOCUMENT_RETURN", DocumentReturn));
            Params.Add(new DBParameter("FILING_DATE", FilingDate));
            Params.Add(new DBParameter("NOTIFICATION", Notification));
            Params.Add(new DBParameter("ADVANCE_VOUCHER", AdvanceVoucher));
            Params.Add(new DBParameter("DEPOSIT_SLIP", DepositSlip));
            Params.Add(new DBParameter("MONEY_TRANSFER", MoneyTransfer));
            Params.Add(new DBParameter("MAIL_TO_PRINCIPAL", MailToPrincipal));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_SHOBYO_ID", SiShobyoID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;

            foreach (SiShobyoAttachFile a in AttachFiles)
            {
                a.RenewDate = RenewDate;
                a.RenewUserID = RenewUserID;

                bool ret = true;
                if (a.SiShobyoID == SiShobyoID)
                {
                    ret = a.UpdateRecord(dbConnect, loginUser);
                }
                else
                {
                    a.SiShobyoID = SiShobyoID;
                    ret = a.InsertRecord(dbConnect, loginUser);
                }
                if (ret == false)
                    return false;
            }

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
            Params.Add(new DBParameter("PK", SiShobyoID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiShobyoID == null;
        }
    }
}
