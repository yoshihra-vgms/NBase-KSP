using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

using Npgsql;
using CIsl.DB;

namespace DcCommon.DB.DAC
{
    /// <summary>
    /// 添付ファイルの種別
    /// </summary>    
    public enum EAttachmentType
    {
        

        //-------------------------------------------------------------        
        CI_Report_Record = 1,
        CI_ActionTakenByVessel,
        CI_ActionTakenByCompany,
        CI_ClassInvolved,
        CI_CorrectiveAction,

        AC_Accident,
        AC_SpotReport,
        AC_Progress,
        AC_CauseOfAccident,
        AC_PreventiveAction,
        AC_Reports,

        MO_InspectionReport,
        MO_1stComemnt,
        MO_2ndtComemnt,
    
        //------------------------------

        MAX,
    }


    /// <summary>
    /// 添付ファイル種別
    /// ms_attachment_type
    /// </summary>
    public class MsAttachmentType : BaseDac
    {
        #region メンバ変数
        /// <summary>
        /// 添付ファイル種別ID
        /// </summary>
        public int attachment_type_id = EVal;

        /// <summary>
        /// 添付ファイル種別名
        /// </summary>
        public string attachment_type_name = "";

        #endregion

        public override string ToString()
        {
            return this.attachment_type_name;
        }

        /// <summary>
        /// 基本SELEC文
        /// </summary>
        private static string DefaultSelect = @"
SELECT
ms_attachment_type.attachment_type_id,
ms_attachment_type.attachment_type_name,
ms_attachment_type.delete_flag,
ms_attachment_type.create_ms_user_id,
ms_attachment_type.update_ms_user_id,
ms_attachment_type.create_date,
ms_attachment_type.update_date
FROM
ms_attachment_type

";


        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="cone">DB接続</param>
        /// <returns></returns>
        public static List<MsAttachmentType> GetRecords(NpgsqlConnection cone)
        {
            List<MsAttachmentType> anslist = new List<MsAttachmentType>();

            try
            {
                //SQ
                string sql = DefaultSelect + @"
WHERE
ms_attachment_type.delete_flag = false

";

                //取得
                anslist = GetRecordsList<MsAttachmentType>(cone, sql);
            }
            catch (Exception e)
            {
                throw new Exception("MsAttachmentType GetRecords", e);
            }

            return anslist;
        }
    }
}
