using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DcCommon.Output;
using DcCommon.DB.DAC;
using AdvanceSoftware.ExcelCreator.Xlsx;
using CIsl.DB;
using CIsl.DB.WingDAC;
using System.IO;


namespace DeficiencyControl.Files
{
    

    /// <summary>
    /// 事故トラブル帳票
    /// </summary>
    public class AccidentExcelFile : BaseExcelFile
    {
        #region Excel変数名定義

        public const string Category = "**Category";

        public const string Year = "**Year";

        public const string Kind = "**Kind";

        public const string Situation = "**Situation";

        public const string KindData = "**KindData_Y{0}_K{1}";

        public const string SituData = "**SituData_Y{0}_S{1}";

        /// <summary>
        /// 最大出力年数
        /// </summary>
        public const int MaxYearCount = 21;
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
                byte[] data = SvcManager.SvcMana.GetAccidentExcelTemplate();
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
        /// 事故分類の書き込み
        /// </summary>
        /// <param name="crea"></param>
        private void WriteKindOfAccidentHeader(XlsxCreator crea)
        {
            List<MsKindOfAccident> kindlist = DcGlobal.Global.DBCache.MsKindOfAccidentList;

            int no = 1;
            foreach (MsKindOfAccident kind in kindlist)
            {
                string tag = this.CreateTemplateNo(Kind, no);
                crea.Cell(tag).Value = kind.ToString();

                no++;
            }
        }


        /// <summary>
        /// 発生状況の書き込み
        /// </summary>
        /// <param name="crea">書き込みデータ</param>        
        private void WriteAccidentSituationHeader(XlsxCreator crea)
        {
            List<MsAccidentSituation> sitlist = DcGlobal.Global.DBCache.MsAccidentSituationList;

            int no = 1;
            foreach (MsAccidentSituation sit in sitlist)
            {
                string tag = this.CreateTemplateNo(Situation, no);
                crea.Cell(tag).Value = sit.ToString();

                no++;
            }
        }


        /// <summary>
        /// 対象年度のデータ作成
        /// </summary>
        /// <param name="crea">書き込み場所</param>
        /// <param name="yno">年度通し番号</param>
        /// <param name="odata">書き込みデータ</param>
        private void WriteYear(XlsxCreator crea, int yno, AccidentOutputData odata)
        {
            List<MsKindOfAccident> kindlist = DcGlobal.Global.DBCache.MsKindOfAccidentList;
            List<MsAccidentSituation> sitlist = DcGlobal.Global.DBCache.MsAccidentSituationList;
            
            //年度の書き込み
            string yeartag = this.CreateTemplateNo(Year, yno);
            crea.Cell(yeartag).Value = string.Format("{0}年度", odata.Year);

            //事故分類
            {
                int i = 0;
                foreach (MsKindOfAccident kind in kindlist)
                {
                    i++;

                    int kid = kind.kind_of_accident_id;

                    //含まれている？
                    bool ret = odata.KindDic.ContainsKey(kid);
                    if (ret == false)
                    {
                        continue;
                    }


                    List<DcAccident> aclist = odata.KindDic[kid];
                    
                    //タグの作成
                    string tag = string.Format(KindData, yno, i);
                    crea.Cell(tag).Value = aclist.Count;
                    
                }

            }

            //事故状況
            {
                int i = 0;
                foreach (MsAccidentSituation sit in sitlist)
                {
                    i++;

                    int aid = sit.accident_situation_id;

                    //含まれている？
                    bool ret = odata.SituationDic.ContainsKey(aid);
                    if (ret == false)
                    {
                        continue;
                    }


                    List<DcAccident> aclist = odata.SituationDic[aid];

                    //タグの作成
                    string tag = string.Format(SituData, yno, i);
                    crea.Cell(tag).Value = aclist.Count;

                }

            }



        }


        


        /// <summary>
        /// エクセル書き込み本体
        /// </summary>
        /// <param name="filename">書き込みファイル名</param>
        /// <param name="idata">パラメータ</param>
        /// <param name="datalist">書き込みデータ</param>
        private void WriteExcel(string filename, AccidentOutputParameter idata, List<AccidentOutputData> datalist)
        {
            using (XlsxCreator crea = new XlsxCreator())
            {
                try
                {
                    crea.OpenBook(filename, filename);

                    {
                        string catename = "";
                        //種別の書き込み                    
                        if (idata.OutputKind == EAccidentOutputKind.船毎)
                        {
                            if (idata.ms_vessel_id == null)
                            {
                                catename = "ALL";
                            }
                            else
                            {
                                MsVessel ves = DcGlobal.Global.DBCache.GetMsVessel(idata.ms_vessel_id.Value);
                                catename = ves.ToString();
                            }
                        }
                        else
                        {
                            catename = idata.OutputKind.ToString();    //関数化する？
                            
                        }

                        //書き込み
                        crea.Cell(Category).Value = catename;
                    }


                    //事故分類書き込み
                    this.WriteKindOfAccidentHeader(crea);

                    //発生状況書き込み
                    this.WriteAccidentSituationHeader(crea);

                    //全データの書き込み
                    int yno = 1;
                    foreach (AccidentOutputData odata in datalist)
                    {
                        //対象年度の書き込み
                        this.WriteYear(crea, yno, odata);

                        yno++;
                        if (yno > MaxYearCount)
                        {
                            //最大出力まで行った
                            break;
                        }

                    }


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


        /// <summary>
        /// エクセルの出力 通信します waitestateせよ
        /// </summary>
        /// <param name="filename">出力ファイル名</param>
        /// <param name="idata">入力情報</param>
        public void OutputExcel(string filename, AccidentOutputParameter idata)
        {
            try
            {
                //情報収集
                List<AccidentOutputData> datalist = SvcManager.SvcMana.GetAccidentOutputData(idata);
                if (datalist == null)
                {
                    throw new Exception("GetAccidentOutputData NULL");
                }
                
                //テンプレートダウンロード
                this.DownloadTemplateFile(filename);

                //書き込み
                this.WriteExcel(filename, idata, datalist);
            }
            catch (Exception e)
            {
                //失敗したら保存したテンプレートが残るので消す
                if (File.Exists(filename) == true)
                {
                    File.Delete(filename);
                }

                throw new Exception("AccidentExcelFile Exception", e);
            }

        }
    }
}
