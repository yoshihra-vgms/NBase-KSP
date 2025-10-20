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
    /// 検船報告書　指摘改善報告書
    /// </summary>
    public class MoiReportFileObservation
    {
        #region Excel変数名定義

        public const string OutputDate = "**OutputDate";
        public const string DestCompany = "**DestCompany";
        public const string DestGroup = "**DestGroup";

        public const string UserBumon = "**UserBumon";
        public const string UserName = "**UserName";


        public const string Date = "**Date";
        public const string InspectionDetail = "**InspectionDetail";

        public const string VesselName = "**VesselName";
        public const string IMONo = "**IMONo";
        public const string OfficialNo = "**OfficialNo";
        
        public const string Port = "**Port";
        public const string Terminal = "**Terminal";

        public const string VIQNo = "**VIQNo";
        public const string Observation = "**Observation";

        public const string WriteComment = "**WriteComment";
        public const string PreventiveAction = "**PreventiveAction";


        #endregion

        /// <summary>
        /// 書き込みデータ
        /// </summary>
        public class WriteData
        {
            /// <summary>
            /// 検船データ
            /// </summary>
            public MoiData Moi = null;

            /// <summary>
            /// 作成者
            /// </summary>
            public UserData CreateUser = null;
        }


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
                byte[] data = SvcManager.SvcMana.GetMoiReportObservationTemplate();
                if (data == null)
                {
                    throw new Exception("GetMoiReportObservationTemplate NULL");
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
        /// 対象データの検索
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private WriteData SearchWriteData(MoiReportOutputParameter odata)
        {
            WriteData ans = new WriteData();

            //書き込みデータ
            ans.Moi = SvcManager.SvcMana.MoiData_GetDataByMoiObservationID(odata.moi_observation_id);
            if (ans.Moi == null)
            {
                return null;
            }

            //作成者情報
            ans.CreateUser = SvcManager.SvcMana.UserData_GetDataByMsUserID(odata.CreateUser.ms_user_id);
            if (ans.CreateUser == null)
            {
                return null;
            }

            return ans;
        }

        /// <summary>
        /// 検船データの書き込み本体
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="odata"></param>
        /// <param name="wdata"></param>
        private void WriteMoiData(XlsxCreator crea, MoiReportOutputParameter odata, WriteData wdata)
        {
            DBDataCache db = DcGlobal.Global.DBCache;
            string tag = "";

            DcMoi moi = wdata.Moi.Header.Moi;
            DcMoiObservation obs = wdata.Moi.Observation.Observation;

            //出力日
            crea.Cell(OutputDate).Value = DcGlobal.DateTimeToString(DateTime.Now);

            //宛先
            crea.Cell(DestCompany).Value = odata.DistCompany;
            crea.Cell(DestGroup).Value = odata.DestGroup;

            //作成者
            crea.Cell(UserBumon).Value = wdata.CreateUser.Bumon.ToString();
            crea.Cell(UserName).Value = wdata.CreateUser.User.ToString();

            //Date
            crea.Cell(Date).Value = DcGlobal.DateTimeToString(moi.date);

            //検船詳細
            crea.Cell(InspectionDetail).Value = odata.InspectionDetail;

            //------------------------------------------
            //船名
            MsVessel ves = db.GetMsVessel(moi.ms_vessel_id);
            if (ves != null)
            {
                crea.Cell(VesselName).Value = ves.ToString();
            }


            //IMO No
            if (ves.imo_no != MsVessel.EVal)
            {
                crea.Cell(IMONo).Value = ves.imo_no.ToString();
            }

            //OfficialNo
            crea.Cell(OfficialNo).Value = ves.official_number;

            //Date
            crea.Cell(Date).Value = DcGlobal.DateTimeToString(moi.date);

            //港名
            MsBasho ba = db.GetMsBasho(moi.ms_basho_id);
            if (ba != null)
            {
                crea.Cell(Port).Value = ba.ToString();
            }

            //terminal
            crea.Cell(Terminal).Value = moi.terminal;
            //----------------------------------------------------------------------
            //VIQNo
            MsViqNo vno = db.GetMsViqNo(obs.viq_no_id);
            if (vno != null)
            {
                crea.Cell(VIQNo).Value = vno.viq_no;
            }

            //指摘事項
            crea.Cell(Observation).Value = obs.observation;
            
            //コメント
            crea.Cell(WriteComment).Value = obs.WriteComment;

            //再発防止策
            crea.Cell(PreventiveAction).Value = obs.preventive_action;
        }

        /// <summary>
        /// Excel書き込み本体
        /// </summary>
        /// <param name="filename">ファイル名</param>
        /// <param name="odata">パラメータ</param>
        /// <param name="wdata">書き込みデータ</param>
        private void WriteExcel(string filename, MoiReportOutputParameter odata, WriteData wdata)
        {
            using (XlsxCreator crea = new XlsxCreator())
            {
                try
                {
                    crea.OpenBook(filename, filename);

                    //データの書き込み
                    this.WriteMoiData(crea, odata, wdata);

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
                WriteData wdata = this.SearchWriteData(odata);
                if (wdata == null)
                {
                    throw new Exception("SearchWriteData NULL moi_observation_id=" + odata.moi_observation_id.ToString());
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

                throw new Exception("MoiReportObservation Exception", e);
            }

        }
    }
}
