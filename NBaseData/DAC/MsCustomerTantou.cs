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
    public class MsCustomerTantou
    {
         #region データメンバ
    //MS_CUSTOMER_TANTOU_ID          NUMBER(9,0) NOT NULL,
    //MS_CUSTOMER_ID                 VARCHAR2(40),
    //NAME                           NUMBER(9,0),
    //MAIL_ADDRESS                   NUMBER(9,0),
    //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
    //RENEW_DATE                     DATE NOT NULL,
    //TS                             TIMESTAMP(6),

        /// <summary>
        /// 顧客担当者ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_TANTOU_ID")]
        public int MsCustomerTantouID { get; set; }

        /// <summary>
        /// 顧客ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_ID")]
        public string MsCustomerID { get; set; }

        /// <summary>
        /// 顧客担当者名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 顧客担当者メールアドレス
        /// </summary>
        [DataMember]
        [ColumnAttribute("MAIL_ADDRESS")]
        public string MailAddress { get; set; }

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
        /// 電話番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("TEL")]
        public string Tel { get; set; }

        /// <summary>
        /// FAX番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("FAX")]
        public string Fax { get; set; }
        #endregion

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MsCustomerTantou()
        {
        }

        public static List<MsCustomerTantou> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomerTantou), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCustomerTantou), "GetRecordsOrder");
            List<MsCustomerTantou> ret = new List<MsCustomerTantou>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCustomerTantou> mapping = new MappingBase<MsCustomerTantou>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsCustomerTantou> GetRecordsByCustomerID(NBaseData.DAC.MsUser loginUser, string customerId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomerTantou), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCustomerTantou), "ByCustomerID");
            List<MsCustomerTantou> ret = new List<MsCustomerTantou>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_CUSTOMER_ID", customerId));
            MappingBase<MsCustomerTantou> mapping = new MappingBase<MsCustomerTantou>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static List<MsCustomerTantou> GetRecordsByCustomerIDAndName( NBaseData.DAC.MsUser loginUser, string customerId, string name)
        {
            return GetRecordsByCustomerIDAndName(null, loginUser, customerId, name);
        }
        public static List<MsCustomerTantou> GetRecordsByCustomerIDAndName(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string customerId, string name)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomerTantou), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCustomerTantou), "ByCustomerIDAndName");
            List<MsCustomerTantou> ret = new List<MsCustomerTantou>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_CUSTOMER_ID", customerId));
            Params.Add(new DBParameter("NAME", name));
            MappingBase<MsCustomerTantou> mapping = new MappingBase<MsCustomerTantou>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static MsCustomerTantou GetRecord(NBaseData.DAC.MsUser loginUser, int MsCustomerTantouID)
        {
            return GetRecord(null, loginUser, MsCustomerTantouID);
        }
        public static MsCustomerTantou GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, int MsCustomerTantouID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomerTantou), MethodBase.GetCurrentMethod());
            List<MsCustomerTantou> ret = new List<MsCustomerTantou>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_CUSTOMER_TANTOU_ID", MsCustomerTantouID));
            MappingBase<MsCustomerTantou> mapping = new MappingBase<MsCustomerTantou>();
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomerTantou), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            //Params.Add(new DBParameter("MS_CUSTOMER_TANTOU_ID", MsCustomerTantouID)); //SQLにてSEQより割当(2009/08/30:aki)
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("MAIL_ADDRESS", MailAddress));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            Params.Add(new DBParameter("TEL", Tel));
            Params.Add(new DBParameter("FAX", Fax));

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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCustomerTantou), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_CUSTOMER_ID", MsCustomerID));
            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("MAIL_ADDRESS", MailAddress));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
 
            Params.Add(new DBParameter("TEL", Tel));
            Params.Add(new DBParameter("FAX", Fax));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_CUSTOMER_TANTOU_ID", MsCustomerTantouID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
