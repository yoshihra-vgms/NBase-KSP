using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Controls.CommentItem;

using DeficiencyControl.Forms;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;
using CIsl.DB.WingDAC;
using DeficiencyControl.Logic;
using CIsl.DB;

namespace DeficiencyControl.Accident
{
    /// <summary>
    /// 事故トラブル一括管理
    /// </summary>
    public class AccidentManager
    {






        /// <summary>
        /// 入力データの取得
        /// </summary>
        /// <param name="srcdata">元データ 無いときはnull</param>
        /// <param name="hcon"></param>
        /// <param name="detacon"></param>
        /// <returns></returns>
        private static AccidentData GetInputData(AccidentData srcdata, AccidentHeaderControl hcon, AccidentDetailControl detacon)
        {
            AccidentData ans = new AccidentData();
            ans.Accident = new DcAccident();
            if (srcdata != null)
            {
                ans.Accident = (DcAccident)srcdata.Accident.Clone();
            }

            ans.AttachmentList = new List<DcAttachment>();

            DcAccident ac = ans.Accident;
            

            int eval = BaseDac.EVal;

            string keyword = "";

            //ヘッダー
            #region ヘッダーの取得
            {
                AccidentHeaderControl.AccidentHeaderControlOutputData hdata = hcon.GetInputData();
                ac.date = hdata.Date;

                ac.ms_user_id = "";
                if (hdata.PIC != null)
                {
                    ac.ms_user_id = hdata.PIC.ms_user_id;
                    keyword += hdata.PIC.ToString();
                }

                //Kind
                ac.accident_kind_id = eval;
                if (hdata.AccidentKind != null)
                {
                    ac.accident_kind_id = hdata.AccidentKind.accident_kind_id;
                    keyword += hdata.AccidentKind.ToString();
                }
                
                //kind of accident
                ac.kind_of_accident_id = eval;
                if (hdata.KindOfAccident != null)
                {
                    ac.kind_of_accident_id = hdata.KindOfAccident.kind_of_accident_id;
                    keyword += hdata.KindOfAccident.ToString();
                }

                //発生状況
                ac.accident_situation_id = eval;
                if (hdata.AccidentSituation != null)
                {
                    ac.accident_situation_id = hdata.AccidentSituation.accident_situation_id;
                    keyword += hdata.AccidentSituation.ToString();
                }

                //発生場所
                ac.ms_basho_id = "";
                if (hdata.Site != null)
                {
                    ac.ms_basho_id = hdata.Site.ms_basho_id;
                    keyword += hdata.Site.ToString();
                }

                //船
                ac.ms_vessel_id = eval;
                if (hdata.Vessel != null)
                {
                    ac.ms_vessel_id = hdata.Vessel.ms_vessel_id;
                    keyword += hdata.Vessel.ToString();
                }

                //国名
                ac.ms_regional_code = "";
                if (hdata.Country != null)
                {
                    ac.ms_regional_code = hdata.Country.ms_regional_code;
                    keyword += hdata.Country.ToString();
                }

                //タイトル
                ac.title = hdata.Title;


            }
            #endregion

            //詳細
            #region 詳細の取得
            {
                AccidentDetailControl.AccidentDetailControlOutputData ddata = detacon.GetInputData();

                //No
                ac.accident_report_no = ddata.AccidentReportNo;

                //Importance
                ac.accident_importance_id = eval;
                if (ddata.Importance != null)
                {
                    ac.accident_importance_id = ddata.Importance.accident_importance_id;
                    keyword += ddata.Importance.ToString();
                }

                //Status
                ac.accident_status_id = (int)ddata.Status;
                keyword += ddata.Status.ToString();

                //Accident事故概要
                ac.accident = ddata.Accident;
                keyword += ddata.Accident;
                ans.AttachmentList.AddRange(ddata.AccidentAttachmentList);

                //現場報告
                ac.spot_report = ddata.SpotReport;
                keyword += ddata.SpotReport;
                ans.AttachmentList.AddRange(ddata.SpotReportAttachmentList);

                //進捗状況
                ans.ProgressList.AddRange(ddata.ProgressList);

                //調査結果
                ac.cause_of_accident = ddata.CauseOfAccident;
                keyword += ddata.CauseOfAccident;
                ans.AttachmentList.AddRange(ddata.CauseOfAccidentAttachmentList);
                                
                //再発防止策
                ac.preventive_action = ddata.PreventiveAction;
                keyword += ddata.PreventiveAction;
                ans.AttachmentList.AddRange(ddata.PreventiveActionAttachmentList);


                //報告書提出先
                ans.ReportsList.AddRange(ddata.ReportsList);
                                
                ac.influence = ddata.Influence;
                ac.remarks = ddata.Remarks;

            }
            #endregion

            //キーワード
            ans.AttachmentList.ForEach(x => keyword += x.filename);
            ac.search_keyword = keyword;
            
            

            return ans;

        }







        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <param name="hcon">対象ヘッダー</param>
        /// <param name="detacon">対象詳細</param>
        public static void CheckInputError(AccidentHeaderControl hcon, AccidentDetailControl detacon)
        {
            bool ret = false;

            ret |= (!hcon.CheckError(true));
            ret |= (!detacon.CheckError(true));

            if (ret == true)
            {
                throw new ControlInputErrorException("AccidentManager CheckInput");
            }
        }

        /// <summary>
        ///エラーリセット
        /// </summary>
        /// <param name="hcon"></param>
        /// <param name="detacon"></param>
        public static void ResetError(AccidentHeaderControl hcon, AccidentDetailControl detacon)
        {
            hcon.ResetError();
            detacon.ResetError();
        }




        /// <summary>
        /// データ挿入処理 CheckInputErrorでエラーチェック済みであること
        /// </summary>
        /// <param name="hcon"></param>
        /// <param name="detacon"></param>
        public static void Insert(AccidentHeaderControl hcon, AccidentDetailControl detacon)
        {
            try
            {
                //データ取得
                AccidentData acdata = GetInputData(null, hcon, detacon);

                //処理
                int id = SvcManager.SvcMana.AccidentData_InsertData(acdata, DcGlobal.Global.LoginMsUser);
                if (id <= 0)
                {
                    throw new Exception("AccidentData_InsertData FALSE");
                }
            }
            catch (Exception e)
            {
                //throw new Exception("AccidentManager Insert", e);
                throw e;
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="srcdata"></param>
        /// <param name="hcon"></param>
        /// <param name="detacon"></param>
        public static void Update(AccidentData srcdata, AccidentHeaderControl hcon, AccidentDetailControl detacon)
        {
            try
            {
                //データ取得
                AccidentData acdata = GetInputData(srcdata, hcon, detacon);

                //処理
                bool ret = SvcManager.SvcMana.AccidentData_UpdateData(acdata, DcGlobal.Global.LoginMsUser);
                if (ret == false)
                {
                    throw new Exception("AccidentData_UpdateData FALSE");
                }
            }
            catch (Exception e)
            {
                //throw new Exception("AccidentManager Update", e);
                throw e;
            }
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="srcdata"></param>
        public static void Delete(AccidentData srcdata)
        {
            try
            {                
                //処理
                bool ret = SvcManager.SvcMana.AccidentData_DeleteData(srcdata.Accident.accident_id, DcGlobal.Global.LoginMsUser);
                if (ret == false)
                {
                    throw new Exception("AccidentData_DeleteData FALSE");
                }
            }
            catch (Exception e)
            {
                throw new Exception("AccidentManager Delete", e);
            }
        }


        
    }
}
