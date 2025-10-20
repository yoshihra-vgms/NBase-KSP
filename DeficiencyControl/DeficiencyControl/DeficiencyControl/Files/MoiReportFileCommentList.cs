using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon.Output;
using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;
using AdvanceSoftware.ExcelCreator.Xlsx;
using CIsl.DB;
using CIsl.DB.WingDAC;
using System.IO;
using DeficiencyControl.Util;

namespace DeficiencyControl.Files
{
    /// <summary>
    /// 検船報告書 コメントリスト
    /// </summary>
    public class MoiReportFileCommentList
    {

        #region Excel変数名定義

        public const string VesselName = "**VesselName";
        public const string PIC = "**PIC";
        public const string Port = "**Port";
        public const string Country = "**Country";

        public const string Date = "**Date";
        public const string ReceiptDate = "**ReceiptDate";
        public const string InspectionCategory = "**InspectionCategory";
        public const string InspectionCompany = "**InspectionCompany";
        public const string AppointedCompany = "**AppointedCompany";
        public const string InspectionName = "**InspectionName";
        public const string Attend = "**Attend";

        public const string VIQCode = "**VIQCode";
        public const string VIQCodeText = "**VIQCodeText";
        public const string VIQNo = "**VIQNo";
        public const string VIQNoText = "**VIQNoText";
        public const string Observation = "**Observation";

        public const string RootCause = "**RootCause";
        public const string Comment1st = "**Comment1st";
        public const string Comment2nd = "**Comment2nd";
        public const string PreventiveAction = "**PreventiveAction";


        #endregion



        /// <summary>
        /// テンプレートファイルの保存
        /// </summary>
        /// <param name="filename">保存名</param>
        private void DownloadTemplateFile(string filename)
        {
            DcLog.WriteLog("DownloadTemplateFile Start filename=" + filename);
            try
            {
                //テンプレート取得
                byte[] data = SvcManager.SvcMana.GetMoiReportCommentListTemplate();
                if (data == null)
                {
                    throw new Exception("GetMoiReportCommentListTemplate NULL");
                }

                DcGlobal.ByteArrayToFile(filename, data);

            }
            catch (Exception e)
            {
                throw new Exception("DownloadTemplateFile失敗", e);
            }

            DcLog.WriteLog("DownloadTemplateFile End");

            return;
        }

        /// <summary>
        /// データ書き込み本体
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="wdata">書き込み本体</param>
        private void WriteMoiData(XlsxCreator crea, MoiData wdata)
        {
            DBDataCache db = DcGlobal.Global.DBCache;
            string tag = "";

            DcMoi moi = wdata.Header.Moi;
            DcMoiObservation obs = wdata.Observation.Observation;

            #region ヘッダー
            //船名
            MsVessel ves = db.GetMsVessel(moi.ms_vessel_id);
            if (ves != null)
            {
                crea.Cell(VesselName).Value = ves.ToString();
            }

            //担当者
            MsUser user = db.GetMsUser(moi.ms_user_id);
            if (user != null)
            {
                crea.Cell(PIC).Value = user.ToString();
            }

            //港名
            MsBasho ba = db.GetMsBasho(moi.ms_basho_id);
            if (ba != null)
            {
                crea.Cell(Port).Value = ba.ToString();
            }

            //国名
            MsRegional reg = db.GetMsRegional(moi.ms_regional_code);
            if (reg != null)
            {
                crea.Cell(Country).Value = reg.ToString();
            }
            #endregion


            #region 検船概要
            //受検日
            crea.Cell(Date).Value = DcGlobal.DateTimeToString(moi.date);

            //レポート受領日
            crea.Cell(ReceiptDate).Value = DcGlobal.DateTimeToString(moi.receipt_date);

            //検船種別
            MsInspectionCategory incate = db.GetMsInspectionCategory(moi.inspection_category_id);
            if (incate != null)
            {
                crea.Cell(InspectionCategory).Value = incate.ToString();
            }


            //検船実施会社
            MsCustomer inscom = db.GetMsCustomerInspection(moi.inspection_ms_customer_id);
            if (inscom != null)
            {
                crea.Cell(InspectionCompany).Value = inscom.ToString();
            }

            //申請先
            MsCustomer appcom = db.GetMsCustomerAppointed(moi.appointed_ms_customer_id);
            if (appcom != null)
            {
                crea.Cell(AppointedCompany).Value = appcom.ToString();
            }

            //検船員
            crea.Cell(InspectionName).Value = moi.inspection_name;

            //立会者
            crea.Cell(Attend).Value = moi.attend;


            #endregion


            #region 指摘外概要
            //VIQ Code
            {
                MsViqCode vcode = db.GetMsViqCode(obs.viq_code_id);
                if (vcode != null)
                {
                    crea.Cell(VIQCode).Value = vcode.viq_code;
                    crea.Cell(VIQCodeText).Value = vcode.description;
                }
            }

            //VIQNo
            {
                MsViqNo vno = db.GetMsViqNo(obs.viq_no_id);
                if (vno != null)
                {
                    crea.Cell(VIQNo).Value = vno.viq_no;
                    crea.Cell(VIQNoText).Value = vno.description;
                }
            }

            //指摘事項
            crea.Cell(Observation).Value = obs.observation;

            #endregion


            #region コメント是正

            //根本原因
            crea.Cell(RootCause).Value = obs.root_cause;

            //是正措置 
            crea.Cell(Comment1st).Value = obs.comment_1st;
            crea.Cell(Comment2nd).Value = obs.comment_2nd;

            //再発防止策
            crea.Cell(PreventiveAction).Value = obs.preventive_action;

            #endregion
        }

        /// <summary>
        /// Excel書き込み本体
        /// </summary>
        /// <param name="filename">ファイル名</param>
        /// <param name="odata">パラメータ</param>
        /// <param name="wdata">書き込みデータ</param>
        private void WriteExcel(string filename, MoiReportOutputParameter odata, MoiData wdata)
        {
            using (XlsxCreator crea = new XlsxCreator())
            {
                try
                {
                    crea.OpenBook(filename, filename);

                    //データの書き込み
                    this.WriteMoiData(crea, wdata);


                    crea.FullCalcOnLoad = true;

                }
                catch (Exception e)
                {
                    throw new Exception("WriteExcel Exception", e);
                }
                finally
                {
                    crea.CloseBook(true);
                }
            }
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// エクセルの出力 通信します waitestateせよ
        /// </summary>
        /// <param name="filename">出力ファイル名</param>
        /// <param name="idata">入力情報</param>
        public void OutputExcel(string filename, MoiReportOutputParameter odata)
        {
            try
            {

                //書き込み用情報収集
                MoiData wdata = SvcManager.SvcMana.MoiData_GetDataByMoiObservationID(odata.moi_observation_id);
                if (wdata == null)
                {
                    throw new Exception("MoiData_GetDataByMoiObservationID FALSE moi_observation_id=" + odata.moi_observation_id.ToString());
                }

                //テンプレートダウンロード
                this.DownloadTemplateFile(filename);

                //書き込み
                this.WriteExcel(filename, odata, wdata);

            }
            catch (Exception e)
            {
                //失敗したら保存したテンプレートが残るので消す
                if (File.Exists(filename) == true)
                {
                    File.Delete(filename);
                }

                throw new Exception("MoiReportCommentList Exception", e);
            }

        }
    }
}
