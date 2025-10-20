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

namespace DeficiencyControl.MOI
{
    /// <summary>
    /// 検船管理
    /// </summary>
    public class MoiManager
    {

        /// <summary>
        /// ヘッダーデータの取得
        /// </summary>
        /// <param name="hcon"></param>
        /// <returns></returns>
        private static MoiHeaderData CreateHeaderData(MoiData srcdata, MoiHeaderControl hcon)
        {
            int eval = BaseDac.EVal;

            MoiHeaderData ans = new MoiHeaderData();
            ans.Moi = new DcMoi();
            ans.AttachmentList = new List<DcAttachment>();

            string keyword = "";

            //元があるなら使用する
            if (srcdata != null)
            {
                ans.Moi = (DcMoi)srcdata.Header.Moi.Clone();
            }
            DcMoi moi = ans.Moi;

            //入力データの取得
            MoiHeaderControl.MoiHeaderControlOutputData odata = hcon.GetInputData();

            //船
            moi.ms_vessel_id = eval;
            if (odata.Vessel != null)
            {
                moi.ms_vessel_id = odata.Vessel.ms_vessel_id;
                keyword += odata.Vessel.ToString();
            }

            //PIC
            moi.ms_user_id = "";
            if (odata.PIC != null)
            {
                moi.ms_user_id = odata.PIC.ms_user_id;
                keyword += odata.PIC.ToString();
            }

            //Port
            moi.ms_basho_id = "";
            if (odata.Port != null)
            {
                moi.ms_basho_id = odata.Port.ms_basho_id;
                keyword += odata.Port.ToString();
            }

            //Terminal
            moi.terminal = odata.Terminal;
            keyword += odata.Terminal;

            //Country
            moi.ms_regional_code = "";
            if (odata.Country != null)
            {
                moi.ms_regional_code = odata.Country.ms_regional_code;
                keyword += odata.Country.ToString();
            }

            //Date
            moi.date = odata.Date;

            //受領日
            moi.receipt_date = odata.ReceiptDate;

            //指摘件数
            moi.observation = odata.Observation;

            //検船種別
            moi.inspection_category_id = eval;
            if (odata.InspectionCategory != null)
            {
                moi.inspection_category_id = odata.InspectionCategory.inspection_category_id;
                keyword += odata.InspectionCategory.ToString();
            }

            //申請先
            moi.appointed_ms_customer_id = "";
            if (odata.AppointedCompany != null)
            {
                moi.appointed_ms_customer_id = odata.AppointedCompany.ms_customer_id;
                keyword += odata.AppointedCompany.ToString();
            }

            //検船会社
            moi.inspection_ms_customer_id = "";
            if (odata.InspectionCompany != null)
            {
                moi.inspection_ms_customer_id = odata.InspectionCompany.ms_customer_id;
                keyword += odata.InspectionCompany.ToString();
            }

            //検船員
            moi.inspection_name = odata.InspectionName;
            keyword += odata.InspectionName;

            //報告書
            ans.AttachmentList.AddRange(odata.InspectionReportAttachmentList);

            //備考
            moi.remarks = odata.Remarks;
            keyword += odata.Remarks;

            //立会者
            moi.attend = odata.Attend;
            keyword += odata.Attend;

            //キーワード設定
            moi.search_keyword = keyword;

            return ans;
        }


        /// <summary>
        /// 指摘事項データの取得
        /// </summary>
        /// <param name="detailcon"></param>
        /// <returns></returns>
        private static MoiObservationData CreateObservationData(MoiData srcdata, MoiDetailControl detailcon)
        {
            int eval = BaseDac.EVal;

            MoiObservationData ans = new MoiObservationData();
            ans.Observation = new DcMoiObservation();
            ans.AttachmentList = new List<DcAttachment>();

            string keyword = "";

            //元があるなら使用する
            if (srcdata != null)
            {
                ans.Observation = (DcMoiObservation)srcdata.Observation.Observation.Clone();
            }
            DcMoiObservation obs = ans.Observation;

            //入力取得
            MoiDetailControl.MoiDetailControlOutputData odata = detailcon.GetInputData();

            //No
            obs.observation_no = odata.No;

            //状態
            obs.moi_status_id = (int)odata.Status;

            //VIQ Code
            obs.viq_code_id = eval;
            if (odata.ViqCode != null)
            {
                obs.viq_code_id = odata.ViqCode.viq_code_id;
                keyword += odata.ViqCode.ToString();
                keyword += odata.ViqCode.description;
            }

            //VIQ No
            obs.viq_no_id = eval;
            if (odata.ViqNo != null)
            {
                obs.viq_no_id = odata.ViqNo.viq_no_id;
                keyword += odata.ViqNo.ToString();
                keyword += odata.ViqNo.description;
            }

            //指摘事項
            obs.observation = odata.Observation;
            keyword += odata.Observation;

            //根本原因
            obs.root_cause = odata.RootCause;
            keyword += odata.RootCause;

            //1stComment
            obs.comment_1st = odata.Comment1st;
            keyword += odata.Comment1st;
            obs.comment_1st_check = odata.Comment1stCheck;
            ans.AttachmentList.AddRange(odata.Comment1stAttachmentList);


            //2ndComment
            obs.comment_2nd = odata.Comment2nd;
            keyword += odata.Comment2nd;
            obs.comment_2nd_check = odata.Comment2ndCheck;
            ans.AttachmentList.AddRange(odata.Comment2ndAttachmentList);

            //再発防止策
            obs.preventive_action = odata.PreventiveAction;
            keyword += odata.PreventiveAction;


            //特記事項
            obs.special_notes = odata.SpecialNotes;
            keyword += odata.SpecialNotes;

            //キーワード
            obs.search_keyword = keyword;

            return ans;
        }


        /// <summary>
        /// MOIデータの作成
        /// </summary>
        /// <param name="hcon"></param>
        /// <param name="detailcon"></param>
        /// <returns></returns>
        private static MoiData CreateMoiData(MoiData srcdata, MoiHeaderControl hcon, MoiDetailControl detailcon)
        {
            MoiData ans = new MoiData();

            ans.Header = MoiManager.CreateHeaderData(srcdata, hcon);

            //0件データの場合、更新データを再取得しない
            if (ans.Header.Moi.observation <= 0)
            {
                ans.Observation = srcdata.Observation;
            }
            else
            {
                ans.Observation = MoiManager.CreateObservationData(srcdata, detailcon);
            }


            return ans;
        }


        /// <summary>
        /// 0件データの時はダミーのObservationを作成する。これによって検索に引っかかるようになる
        /// </summary>
        /// <returns></returns>
        private static MoiObservationData CreateZeroDummyObservation()
        {
            MoiObservationData ans = new MoiObservationData();
            ans.Observation = new DcMoiObservation();
            ans.AttachmentList = new List<DcAttachment>();

            //必要なステータスを設定する
            ans.Observation.moi_status_id = (int)EMoiStatus.Pending;
            


            return ans;
        }

        /// <summary>
        /// データの挿入
        /// </summary>
        /// <param name="hcon"></param>
        /// <param name="detacon"></param>
        public static void Insert(MoiHeaderControl hcon, List<MoiDetailControl> detaillist)
        {
            try
            {
                //入力の取得
                //ヘッダー
                MoiHeaderData hdata = MoiManager.CreateHeaderData(null, hcon);                



                //挿入詳細一式
                List<MoiObservationData> obslist = new List<MoiObservationData>();                
                if (hdata.Moi.observation <= 0)
                {
                    //0件データの時はダミーのデータを作成する
                    MoiObservationData du = MoiManager.CreateZeroDummyObservation();
                    obslist.Add(du);

                }
                else
                {
                    foreach (MoiDetailControl de in detaillist)
                    {
                        MoiObservationData obs = MoiManager.CreateObservationData(null, de);
                        obslist.Add(obs);
                    }
                }

                
                
                //挿入
                int ans = SvcManager.SvcMana.Moi_Insert(hdata, obslist, DcGlobal.Global.LoginMsUser);
                if (ans < 0)
                {
                    throw new Exception("Moi_Insert FALSE");
                }
                
            }
            catch (Exception e)
            {
                //throw new Exception("MoiManager Insert", e);
                throw e;
            }
        }


        /// <summary>
        /// データの更新
        /// </summary>
        /// <param name="srcdata"></param>
        /// <param name="hcon"></param>
        /// <param name="detailcon"></param>
        public static void Update(MoiData srcdata, MoiHeaderControl hcon, MoiDetailControl detailcon)
        {
            try
            {   

                //データの作成
                MoiData mdata = MoiManager.CreateMoiData(srcdata, hcon, detailcon);

                //データ更新処理
                bool ret = SvcManager.SvcMana.MoiData_UpdatetData(mdata, DcGlobal.Global.LoginMsUser);
                if (ret == false)
                {
                    throw new Exception("MoiData_UpdatetData FALSE");
                }
                
            }
            catch (Exception e)
            {
                //throw new Exception("MoiManager Update", e);
                throw e;
            }
        }


        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="srcdata"></param>
        public static void Delete(MoiData srcdata)
        {
            try
            {
                //処理
                bool ret = SvcManager.SvcMana.MoiData_DeleteData(srcdata.Observation.Observation.moi_observation_id, DcGlobal.Global.LoginMsUser);
                if (ret == false)
                {
                    throw new Exception("MoiData_DeleteData FALSE");
                }
            }
            catch (Exception e)
            {
                throw new Exception("MoiManager Delete", e);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// エラーのチェック　単品版
        /// </summary>
        /// <param name="hcon"></param>
        /// <param name="detailcon"></param>
        public static void CheckError(MoiHeaderControl hcon, MoiDetailControl detailcon)
        {
            List<MoiDetailControl> delist = new List<MoiDetailControl>();
            delist.Add(detailcon);

            MoiManager.CheckError(hcon, delist);

        }

        /// <summary>
        /// エラーのチェック リスト版
        /// </summary>
        /// <param name="hcon"></param>
        /// <param name="detaillist"></param>
        public static void CheckError(MoiHeaderControl hcon, List<MoiDetailControl> detaillist)
        {
            bool ret = false;

            ret |= (!hcon.CheckError(true));

            foreach (MoiDetailControl detail in detaillist)
            {
                ret |= (!detail.CheckError(true));
            }

            if (ret == true)
            {
                throw new ControlInputErrorException("MoiManager CheckInput");
            }
        }

        /// <summary>
        /// エラーの初期化 単品版
        /// </summary>
        /// <param name="hcon"></param>
        /// <param name="detacon"></param>
        public static void ResetError(MoiHeaderControl hcon, MoiDetailControl detacon)
        {
            List<MoiDetailControl> delist = new List<MoiDetailControl>();
            delist.Add(detacon);

            MoiManager.ResetError(hcon, delist);
        }


        /// <summary>
        /// エラーの初期化
        /// </summary>
        /// <param name="hcon"></param>
        /// <param name="detaillist"></param>
        public static void ResetError(MoiHeaderControl hcon, List<MoiDetailControl> detaillist)
        {   
            hcon.ResetError();

            foreach (MoiDetailControl detail in detaillist)
            {
                detail.ResetError();
            }
        }

        /// <summary>
        /// 指定された日付で該当のVIQ Versionを検索・取得する。
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static MsViqVersion SearchViqVersion(DateTime dateTime)
        {
            MsViqVersion resultVer = null;

            // 全データ取得
            List<MsViqVersion> datalist = DcGlobal.Global.DBCache.MsViqVersionList;

            foreach (MsViqVersion ver in datalist)
            {
                // 開始日・終了日のデフォルト設定・取得
                DateTime sDate = DateTime.MinValue;
                DateTime eDate = DateTime.MaxValue;
                if (ver.start_date != MsViqVersion.EDate)
                {
                    sDate = ver.start_date;
                }
                if (ver.end_date != MsViqVersion.EDate)
                {
                    eDate = ver.end_date;
                }

                // 範囲内に入るデータを見つける
                if (sDate <= dateTime && dateTime <= eDate)
                {
                    resultVer = ver;
                    break;
                }
            }

            return resultVer;
        }

        
        
    }
}
