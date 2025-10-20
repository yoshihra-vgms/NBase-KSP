using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using System.Reflection;
using ORMapping;

namespace NBaseData.DAC
{
    [DataContract()]
    public class MsRole
    {
        public static string KEY_共通管理 = "共通";
        public static string KEY_予実管理 = "予実管理";
        public static string KEY_動静管理 = "動静管理";
        public static string KEY_指摘事項管理 = "指摘事項管理";
        public static string KEY_文書管理 = "文書管理";
        public static string KEY_文書管理2 = "ドキュメント管理";
        public static string KEY_発注管理 = "発注管理";
        public static string KEY_船員管理 = "船員管理";



        #region データメンバ
        //MS_ROLE_ID                     NUMBER(9,0) NOT NULL,
        //MS_BUMON_ID                    VARCHAR2(40),
        //ADMIN_FLAG                     NUMBER(1,0),
        //NAME1                          NVARCHAR2(25),
        //NAME2                          NVARCHAR2(25),
        //NAME3                          NVARCHAR2(25),
        //ENABLE_FLAG                    NUMBER(1,0) DEFAULT 0,
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),

        /// <summary>
        /// 船員ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_ROLE_ID", true)]
        public int MsRoleID { get; set; }




        /// <summary>
        /// 部門ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BUMON_ID")]
        public string MsBumonID { get; set; }




        /// <summary>
        /// 管理者フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("ADMIN_FLAG")]
        public int AdminFlag { get; set; }

        /// <summary>
        /// 機能名1
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME1")]
        public string Name1 { get; set; }

        /// <summary>
        /// 機能名2
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME2")]
        public string Name2 { get; set; }

        /// <summary>
        /// 機能名3
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME3")]
        public string Name3 { get; set; }

        /// <summary>
        /// 利用可フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("ENABLE_FLAG")]
        public int EnableFlag { get; set; }
       
        
        
        
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




        public static List<MsRole> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsRole), MethodBase.GetCurrentMethod());

            List<MsRole> ret = new List<MsRole>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsRole> mapping = new MappingBase<MsRole>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<MsRole> SearchRecords(MsUser loginUser, string name)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsRole), "GetRecords");

            List<MsRole> ret = new List<MsRole>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsRole> mapping = new MappingBase<MsRole>();

            if (name != string.Empty)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsRole), "SearchByName");
                Params.Add(new DBParameter("NAME", "%" + name + "%"));
            }

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsRole), "OrderBy");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        
        
        
        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_ROLE_ID", MsRoleID));

            Params.Add(new DBParameter("MS_BUMON_ID", MsBumonID));

            Params.Add(new DBParameter("ADMIN_FLAG", AdminFlag));
            Params.Add(new DBParameter("NAME1", Name1));
            Params.Add(new DBParameter("NAME2", Name2));
            Params.Add(new DBParameter("NAME3", Name3));
            Params.Add(new DBParameter("ENABLE_FLAG", EnableFlag));

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

            Params.Add(new DBParameter("MS_BUMON_ID", MsBumonID));

            Params.Add(new DBParameter("ADMIN_FLAG", AdminFlag));
            Params.Add(new DBParameter("NAME1", Name1));
            Params.Add(new DBParameter("NAME2", Name2));
            Params.Add(new DBParameter("NAME3", Name3));
            Params.Add(new DBParameter("ENABLE_FLAG", EnableFlag));

            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_ROLE_ID", MsRoleID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }


        public static bool InsertOrUpdate(MsUser loginUser, List<MsRole> roles)
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    foreach (MsRole r in roles)
                    {
                        if (r.MsRoleID == int.MinValue)
                        {
                            r.InsertRecord(dbConnect, loginUser);
                        }
                        else
                        {
                            r.UpdateRecord(dbConnect, loginUser);
                        }
                    }

                    dbConnect.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                    return false;
                }
            }
        }
    }
}
