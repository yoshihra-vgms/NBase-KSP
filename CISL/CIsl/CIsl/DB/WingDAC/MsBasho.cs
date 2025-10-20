using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

namespace CIsl.DB.WingDAC
{
    /// <summary>
    /// 場所マスタ　ポートマスタ
    /// ms_basho
    /// </summary>
    public class MsBasho : BaseWingDac
    {
        #region メンバ変数
        /// <summary>
        /// 場所ID
        /// </summary>
        public string ms_basho_id = "";

        /// <summary>
        /// 場所番号
        /// </summary>
        public string ms_basho_no = "";

        /// <summary>
        /// 場所名
        /// </summary>
        public string basho_name = "";


        public string ms_basho_kubun_id = "";
        public string akasaka_port_no = "";
        
        public decimal send_flag = EVal;
        public decimal vessel_id = EVal;
        public decimal data_no = EVal;
        public string user_key = "";        
        public decimal gaichi_flag = EVal;

        #endregion


        public override string ToString()
        {
            return this.basho_name;
        }
        
        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_basho.ms_basho_id,
ms_basho.ms_basho_no,
ms_basho.basho_name,
ms_basho.ms_basho_kubun_id,
ms_basho.akasaka_port_no,
ms_basho.delete_flag,
ms_basho.send_flag,
ms_basho.vessel_id,
ms_basho.data_no,
ms_basho.user_key,
ms_basho.renew_date,
ms_basho.renew_user_id,
ms_basho.ts,
ms_basho.gaichi_flag
FROM
ms_basho
";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <returns></returns>
        public static List<MsBasho> GetRecords(NpgsqlConnection cone)
        {
            List<MsBasho> anslist = new List<MsBasho>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_basho.delete_flag = 0

";

                //取得
                anslist = GetRecordsList<MsBasho>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsBasho GetRecords", e);
            }

            return anslist;
        }
    }
}
