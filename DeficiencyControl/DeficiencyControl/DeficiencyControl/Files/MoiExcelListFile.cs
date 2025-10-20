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
    /// 検船帳票 指摘是正対応リスト
    /// </summary>
    public class MoiExcelListFile : BaseExcelFile
    {
        #region Excel変数名定義

        public const string StartYear = "**StartYear";
        public const string EndYear = "**EndYear";


        public const string VesselName = "**VesselName";
        public const string VIQ_Code = "**VIQ_Code";
        public const string VIQ_No = "**VIQ_No";
        public const string Observation = "**Observation";
        public const string Commet = "**Commet";
        public const string PreventiveAction = "**PreventiveAction";
        public const string SpecialNotes = "**SpecialNotes";

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
                byte[] data = SvcManager.SvcMana.GetMoiExcelListTemplate();
                if (data == null)
                {
                    throw new Exception("GetAccidentExcelTemplate NULL");
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
        /// 書き込みデータの検索
        /// </summary>
        /// <param name="odata"></param>
        /// <returns></returns>
        private List<MoiData> SearchOutputData(MoiExcelOutputParameter odata)
        {
            List<MoiData> anslist = null;
            MoiSearchData sdata = new MoiSearchData();
            sdata.date_start = DcCommon.CommonLogic.CalcuYearStart(odata.StartYear);
            sdata.date_end = DcCommon.CommonLogic.CalcuYearEnd(odata.EndYear);
            
            //対象の取得
            List<MoiData> datalist = SvcManager.SvcMana.MoiData_GetDataListBySearchData(sdata);
            if (datalist == null)
            {
                return null;
            }

            DBDataCache db = DcGlobal.Global.DBCache;

            //並べ替え
            //船　VIQCode VIQNo
            var n = datalist.OrderBy(x => x.Header.Moi.ms_vessel_id).ThenBy(x => {

                MsViqCode code = db.GetMsViqCode(x.Observation.Observation.viq_code_id);
                if(code == null){
                    return 0;
                }

                return code.order_no;
            }).ThenBy(x => {
                MsViqNo vno = db.GetMsViqNo(x.Observation.Observation.viq_no_id);
                if (vno == null)
                {
                    return 0;
                }

                return vno.order_no;

            });
            anslist = n.ToList();

            return anslist;
        }


        /// <summary>
        /// 一行の書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="no">番号</param>
        /// <param name="data">書き込みデータ</param>
        private void WriteLineData(XlsxCreator crea, int no, MoiData data)
        {
            DBDataCache db = DcGlobal.Global.DBCache;
            string tag = "";

            DcMoi moi = data.Header.Moi;
            DcMoiObservation obs = data.Observation.Observation;


            //船名
            tag = this.CreateTemplateNo(VesselName, no);
            MsVessel ves = db.GetMsVessel(moi.ms_vessel_id);
            if (ves != null)
            {
                crea.Cell(tag).Value = ves.ToString();
            
            }

            //VIQCode
            tag = this.CreateTemplateNo(VIQ_Code, no);
            MsViqCode code = db.GetMsViqCode(obs.viq_code_id);
            if (code != null)
            {
                crea.Cell(tag).Value = code.viq_code;
            }


            //VIQ No
            tag = this.CreateTemplateNo(VIQ_No, no);
            MsViqNo vno = db.GetMsViqNo(obs.viq_no_id);
            if (vno != null)
            {
                crea.Cell(tag).Value = vno.viq_no;
            }
            

            //指摘事項
            tag = this.CreateTemplateNo(Observation, no);
            crea.Cell(tag).Value = obs.observation;

            //対策
            tag = this.CreateTemplateNo(Commet, no);
            crea.Cell(tag).Value = obs.WriteComment;

            //再発防止策
            tag = this.CreateTemplateNo(PreventiveAction, no);
            crea.Cell(tag).Value = obs.preventive_action;

            //特記事項
            tag = this.CreateTemplateNo(SpecialNotes, no);
            crea.Cell(tag).Value = obs.special_notes;
        }


        /// <summary>
        /// ﾃﾞｰﾀの書き込み
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="odata"></param>
        /// <param name="wdatalist"></param>
        private void WriteExcel(string filename, MoiExcelOutputParameter odata, List<MoiData> wdatalist)
        {
            using (XlsxCreator crea = new XlsxCreator())
            {
                try
                {
                    crea.OpenBook(filename, filename);

                    //ヘッダー
                    //年度
                    crea.Cell(StartYear).Value = odata.StartYear;
                    crea.Cell(EndYear).Value = odata.EndYear;

                    //全行の書き込み
                    int no = 1;
                    foreach (MoiData wdata in wdatalist)
                    {
                        this.WriteLineData(crea, no, wdata);
                        no++;
                    }


                    //削除する
                    int rpos = wdatalist.Count + 4;
                    crea.RowDelete(rpos, 10000);

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
        public void OutputExcel(string filename, MoiExcelOutputParameter odata)
        {
            try
            {
                
                //情報収集
                List<MoiData> datalist = this.SearchOutputData(odata);
                if (datalist == null)
                {
                    throw new Exception("SearchOutputData NULL");
                }

                //テンプレートダウンロード
                this.DownloadTemplateFile(filename);

                //書き込み
                this.WriteExcel(filename, odata, datalist);
            }
            catch (Exception e)
            {
                //失敗したら保存したテンプレートが残るので消す
                if (File.Exists(filename) == true)
                {
                    File.Delete(filename);
                }

                throw new Exception("MoiExcelListFile Exception", e);
            }

        }


    }
}
