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
    [TableAttribute("MS_SENIN_ETC")]
    public class MsSeninEtc : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }


        //==================================
        // 口座情報
        //==================================


        /// <summary>
        /// 銀行名1
        /// </summary>
        [DataMember]
        [ColumnAttribute("BANK_NAME1")]
        public string BankName1 { get; set; }

        /// <summary>
        /// 支店名1
        /// </summary>
        [DataMember]
        [ColumnAttribute("BRANCH_NAME1")]
        public string BranchName1 { get; set; }

        /// <summary>
        /// 口座番号1
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACCOUNT_NO1")]
        public string AccountNo1 { get; set; }

        /// <summary>
        /// 銀行名2
        /// </summary>
        [DataMember]
        [ColumnAttribute("BANK_NAME2")]
        public string BankName2 { get; set; }

        /// <summary>
        /// 支店名2
        /// </summary>
        [DataMember]
        [ColumnAttribute("BRANCH_NAME2")]
        public string BranchName2 { get; set; }

        /// <summary>
        /// 口座番号2
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACCOUNT_NO2")]
        public string AccountNo2 { get; set; }



        //==================================
        // 作業服
        //==================================

        /// <summary>
        /// 身長
        /// </summary>
        [DataMember]
        [ColumnAttribute("HEIGHT")]
        public string Height { get; set; }

        /// <summary>
        ///  体重
        /// </summary>
        [DataMember]
        [ColumnAttribute("WEIGHT")]
        public string Weight { get; set; }

        /// <summary>
        /// ウェスト
        /// </summary>
        [DataMember]
        [ColumnAttribute("WAIST")]
        public string Waist { get; set; }

        /// <summary>
        /// 股下
        /// </summary>
        [DataMember]
        [ColumnAttribute("INSEAM")]
        public string Inseam { get; set; }

        /// <summary>
        /// 安全靴
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOE_SIZE")]
        public string ShoeSize { get; set; }

        /// <summary>
        /// つなぎ
        /// </summary>
        [DataMember]
        [ColumnAttribute("WORKWEAR")]
        public string Workwear { get; set; }





        //==================================
        // 共通
        //==================================
        #region
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

        public bool EditFlag = false;






        public static List<MsSeninEtc> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSeninEtc), "GetRecords");

            List<MsSeninEtc> ret = new List<MsSeninEtc>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSeninEtc> mapping = new MappingBase<MsSeninEtc>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }


        public static MsSeninEtc GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSeninEtc), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSeninEtc), "ByMsSeninID");

            List<MsSeninEtc> ret = new List<MsSeninEtc>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSeninEtc> mapping = new MappingBase<MsSeninEtc>();
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

            Params.Add(new DBParameter("BANK_NAME1", BankName1));
            Params.Add(new DBParameter("BRANCH_NAME1", BranchName1));
            Params.Add(new DBParameter("ACCOUNT_NO1", AccountNo1));
            Params.Add(new DBParameter("BANK_NAME2", BankName2));
            Params.Add(new DBParameter("BRANCH_NAME2", BranchName2));
            Params.Add(new DBParameter("ACCOUNT_NO2", AccountNo2));

            Params.Add(new DBParameter("HEIGHT", Height));
            Params.Add(new DBParameter("WEIGHT", Weight));
            Params.Add(new DBParameter("WAIST", Waist));
            Params.Add(new DBParameter("INSEAM", Inseam));
            Params.Add(new DBParameter("SHOE_SIZE", ShoeSize));
            Params.Add(new DBParameter("WORKWEAR", Workwear));

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
            Params.Add(new DBParameter("BANK_NAME1", BankName1));
            Params.Add(new DBParameter("BRANCH_NAME1", BranchName1));
            Params.Add(new DBParameter("ACCOUNT_NO1", AccountNo1));
            Params.Add(new DBParameter("BANK_NAME2", BankName2));
            Params.Add(new DBParameter("BRANCH_NAME2", BranchName2));
            Params.Add(new DBParameter("ACCOUNT_NO2", AccountNo2));

            Params.Add(new DBParameter("HEIGHT", Height));
            Params.Add(new DBParameter("WEIGHT", Weight));
            Params.Add(new DBParameter("WAIST", Waist));
            Params.Add(new DBParameter("INSEAM", Inseam));
            Params.Add(new DBParameter("SHOE_SIZE", ShoeSize));
            Params.Add(new DBParameter("WORKWEAR", Workwear));

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
