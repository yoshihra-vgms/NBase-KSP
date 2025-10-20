using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Npgsql;

using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
using CIsl.DB.WingDAC;

namespace DcCommon.DB
{
    /// <summary>
    /// 事故トラブル報告書提出先データまとめ
    /// </summary>
    public class AccidentReportsData
    {
        public AccidentReportsData()
        {
        }


        /// <summary>
        /// AccidentReports
        /// </summary>
        public DcAccidentReports Reports = null;


        /// <summary>
        /// 添付ファイル一式
        /// </summary>
        public List<DcAttachment> AttachmentList = new List<DcAttachment>();

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 親に関連するものを一覧で取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="accident_id"></param>
        /// <returns></returns>
        public static List<AccidentReportsData> GetDataListByAccidentID(NpgsqlConnection cone, int accident_id)
        {
            List<AccidentReportsData> anslist = new List<AccidentReportsData>();


            try
            {
                //本体取得
                List<DcAccidentReports> replist = DcAccidentReports.GetRecordsByAccidentID(cone, accident_id);

                foreach (DcAccidentReports rep in replist)
                {
                    AccidentReportsData ans = new AccidentReportsData();
                    ans.Reports = rep;

                    //添付ファイル
                    ans.AttachmentList = DcAttachment.GetRecrodsByAccidentReportsID(cone, rep.accident_reports_id);


                    //------
                    anslist.Add(ans);
                }



            }
            catch (Exception e)
            {
                throw new Exception("AccidentReportsData GetDataListByAccidentID");

            }

            return anslist;
        }


        /// <summary>
        /// データの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="accident_id">親ID</param>
        /// <returns></returns>
        public int InsertData(NpgsqlConnection cone, MsUser requser, int accident_id)
        {
            int ansid = DcAccidentReports.EVal;

            try
            {
                //本体挿入
                this.Reports.accident_id = accident_id;
                ansid = this.Reports.InsertRecord(cone, requser);

                //ファイル挿入
                foreach (DcAttachment att in this.AttachmentList)
                {
                    att.InsertRecordAccidentReports(cone, requser, ansid);
                }


            }
            catch (Exception e)
            {
                throw new Exception("AccidentReportsData InsertData", e);
            }


            return ansid;
        }


        /// <summary>
        /// データの更新
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <param name="accident_id">親ID</param>
        /// <returns></returns>
        public bool UpdateData(NpgsqlConnection cone, MsUser requser, int accident_id)
        {
            bool ret = true;

            try
            {
                //本体
                this.Reports.accident_id = accident_id;
                ret = this.Reports.UpdateRecord(cone, requser);

                //ファイル挿入、削除
                foreach (DcAttachment att in this.AttachmentList)
                {
                    //DeleteFlagで削除する もしくは、親の削除で削除する
                    if (att.delete_flag == true || this.Reports.delete_flag == true)
                    {
                        att.DeleteRecord(cone, requser);
                    }
                    else
                    {
                        att.InsertRecordAccidentReports(cone, requser, this.Reports.accident_reports_id);
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("AccidentReportsData UpdateData", e);
            }


            return ret;
        }



        /// <summary>
        /// データの削除
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool DeleteData(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = true;

            try
            {
                //本体削除
                ret = this.Reports.DeleteRecord(cone, requser);

                //ファイル削除
                foreach (DcAttachment att in this.AttachmentList)
                {

                    att.DeleteRecord(cone, requser);

                }

            }
            catch (Exception e)
            {
                throw new Exception("AccidentReportsData DeleteData", e);
            }


            return ret;
        }
    }
}
