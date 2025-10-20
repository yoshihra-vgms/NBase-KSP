using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// 顧客マスタ
    /// ms_customer
    /// </summary>
    public class MsCustomer : BaseWingDac
    {
        #region メンバ変数
        /// <summary>
        /// 顧客ID
        /// </summary>
        public string ms_customer_id = "";

        /// <summary>
        /// 名前
        /// </summary>
        public string customer_name = "";

        /// <summary>
        /// 申請先
        /// </summary>
        public decimal appointed_flag = EVal;

        /// <summary>
        /// 検船会社
        /// </summary>
        public decimal inspection_flag = EVal;
        
        
        #endregion


        public override string ToString()
        {
            return this.customer_name;
        }

        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_customer.ms_customer_id,
ms_customer.customer_name,
ms_customer.tel,
ms_customer.fax,
ms_customer.zip_code,
ms_customer.address1,
ms_customer.address2,
ms_customer.building_name,
ms_customer.login_id,
ms_customer.password,
ms_customer.delete_flag,
ms_customer.send_flag,
ms_customer.vessel_id,
ms_customer.data_no,
ms_customer.user_key,
ms_customer.renew_date,
ms_customer.renew_user_id,
ms_customer.ts,
ms_customer.bank_name,
ms_customer.branch_name,
ms_customer.account_no,
ms_customer.account_id,
ms_customer.shubetsu,
ms_customer.bikou,
ms_customer.shubetsu2,
ms_customer.teacher1,
ms_customer.teacher2,
ms_customer.appointed_flag,
ms_customer.inspection_flag
FROM
ms_customer
";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <returns></returns>
        public static List<MsCustomer> GetRecords(NpgsqlConnection cone)
        {
            List<MsCustomer> anslist = new List<MsCustomer>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_customer.delete_flag = 0

";

                //取得
                anslist = GetRecordsList<MsCustomer>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsCustomer GetRecords", e);
            }

            return anslist;
        }



        /// <summary>
        /// 検船会社一覧取得
        /// </summary>
        /// <param name="cone"></param>
        /// <returns></returns>
        public static List<MsCustomer> GetRecordsInspectionCompany(NpgsqlConnection cone)
        {
            List<MsCustomer> anslist = new List<MsCustomer>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_customer.delete_flag = 0
AND
ms_customer.inspection_flag = 1
";

                //取得
                anslist = GetRecordsList<MsCustomer>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsCustomer GetRecordsInspectionCompany", e);
            }

            return anslist;
        }


        /// <summary>
        /// 申請先一覧取得
        /// </summary>
        /// <param name="cone"></param>
        /// <returns></returns>
        public static List<MsCustomer> GetRecordsAppointedCompany(NpgsqlConnection cone)
        {
            List<MsCustomer> anslist = new List<MsCustomer>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_customer.delete_flag = 0
AND
ms_customer.appointed_flag = 1

";

                //取得
                anslist = GetRecordsList<MsCustomer>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsCustomer GetRecordsAppointedCompany", e);
            }

            return anslist;
        }


    }
}
