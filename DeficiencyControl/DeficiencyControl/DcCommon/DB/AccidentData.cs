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
    /// 事故トラブルデータまとめ
    /// </summary>
    public class AccidentData
    {
        public AccidentData()
        {
        }

        /// <summary>
        /// Accident
        /// </summary>
        public DcAccident Accident = null;

        /// <summary>
        /// 進捗一式
        /// </summary>
        public List<AccidentProgressData> ProgressList = new List<AccidentProgressData>();


        /// <summary>
        /// 報告書提出先一式
        /// </summary>
        public List<AccidentReportsData> ReportsList = new List<AccidentReportsData>();


        /// <summary>
        /// 添付ファイル一式
        /// </summary>
        public List<DcAttachment> AttachmentList = new List<DcAttachment>();

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 対象のデータの一式を作成する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="ac"></param>
        /// <returns></returns>
        private static AccidentData SearchSelectData(NpgsqlConnection cone, DcAccident ac)
        {
            AccidentData ans = new AccidentData();

            if (ac == null)
            {
                return null;
            }

            //元設定
            ans.Accident = ac;

            //進捗
            ans.ProgressList = AccidentProgressData.GetDataListByAccidentID(cone, ac.accident_id);

            //提出先
            ans.ReportsList = AccidentReportsData.GetDataListByAccidentID(cone, ac.accident_id);

            //添付ファイル
            ans.AttachmentList = DcAttachment.GetRecrodsByAccidentID(cone, ac.accident_id);

            return ans;

        }

        //----------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 対象のデータを取得する
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="accident_id"></param>
        /// <returns></returns>
        public static AccidentData GetDataByAccidentID(NpgsqlConnection cone, int accident_id)
        {
            AccidentData ans = null;

            try
            {
                //対象のデータを取得
                DcAccident ac = DcAccident.GetRecordsByAccidentID(cone, accident_id);
                if (ac == null)
                {
                    return null;
                }

                //付随物をまとめて取得
                ans = AccidentData.SearchSelectData(cone, ac);
            }
            catch (Exception e)
            {
                throw new Exception("AccidentData GetDataByAccidentID", e);
            }

            return ans;
        }



        /// <summary>
        /// データの挿入
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public int InsertData(NpgsqlConnection cone, MsUser requser)
        {
            int ansid = DcAccident.EVal;

            try
            {
                //本体挿入
                ansid = this.Accident.InsertRecord(cone, requser);

                //進捗
                foreach (AccidentProgressData prog in this.ProgressList)
                {
                    prog.InsertData(cone, requser, ansid);
                }

                //報告書
                foreach (AccidentReportsData rep in this.ReportsList)
                {
                    //親設定                    
                    rep.InsertData(cone, requser, ansid);
                }

                //ファイル挿入
                foreach (DcAttachment att in this.AttachmentList)
                {
                    att.InsertRecordAccident(cone, requser, ansid);
                }


            }
            catch (Exception e)
            {
                throw new Exception("AccidentData InsertData", e);
            }


            return ansid;
        }


        /// <summary>
        /// データの更新
        /// </summary>
        /// <param name="cone"></param>
        /// <param name="requser"></param>
        /// <returns></returns>
        public bool UpdateData(NpgsqlConnection cone, MsUser requser)
        {
            bool ret = true;

            try
            {
                //本体挿入
                ret = this.Accident.UpdateRecord(cone, requser);

                //進捗
                foreach (AccidentProgressData prog in this.ProgressList)
                {
                    if (prog.Progress.accident_progress_id == DcAccidentProgress.EVal)
                    {
                        prog.InsertData(cone, requser, this.Accident.accident_id);
                    }
                    else
                    {
                        prog.UpdateData(cone, requser, this.Accident.accident_id);
                    }
                }

                //報告書
                foreach (AccidentReportsData rep in this.ReportsList)
                {
                    //親設定
                    rep.Reports.accident_id = this.Accident.accident_id;
                    if (rep.Reports.accident_reports_id == DcAccidentReports.EVal)
                    {
                        rep.InsertData(cone, requser, this.Accident.accident_id);
                    }
                    else
                    {
                        rep.UpdateData(cone, requser, this.Accident.accident_id);
                    }
                }

                //ファイル挿入
                foreach (DcAttachment att in this.AttachmentList)
                {
                    if (att.delete_flag == true)
                    {
                        att.DeleteRecord(cone, requser);
                    }
                    else
                    {
                        att.InsertRecordAccident(cone, requser, this.Accident.accident_id);
                    }
                }


            }
            catch (Exception e)
            {
                throw new Exception("AccidentData UpdateData", e);
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
                ret = this.Accident.DeleteRecord(cone, requser);

                //進捗
                foreach (AccidentProgressData prog in this.ProgressList)
                {   
                    prog.DeleteData(cone, requser);
                }

                //報告書
                foreach (AccidentReportsData rep in this.ReportsList)
                {
                    rep.DeleteData(cone, requser);
                }

                //ファイル挿入
                foreach (DcAttachment att in this.AttachmentList)
                {
                    att.DeleteRecord(cone, requser);
                }


            }
            catch (Exception e)
            {
                throw new Exception("AccidentData DeleteData", e);
            }


            return ret;
        }
    }
}
