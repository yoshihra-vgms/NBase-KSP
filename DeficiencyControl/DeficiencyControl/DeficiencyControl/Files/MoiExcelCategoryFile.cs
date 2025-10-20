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
    /// 検船帳票 カテゴリ別ファイル
    /// </summary>
    public class MoiExcelCategoryFile : BaseExcelFile
    {
        #region Excel変数名定義

        public const string StartYear = "**StartYear";
        public const string EndYear = "**EndYear";


        public const string MOICount = "**MOICount";

        public const string VesselCount = "**VesselCount";

        public const string AverageCount = "**AverageCount";


        public const string VIQ_Code = "**VIQ_Code";
        public const string VIQ_Code_Text = "**VIQ_Code_Text";
        public const string VIQ_Code_Count = "**VIQ_Code_Count";

        



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
                byte[] data = SvcManager.SvcMana.GetMoiExcelCategoryTemplate();
                if (data == null)
                {
                    throw new Exception("GetMoiExcelCategoryTemplate NULL");
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
        /// ヘッダーの出力
        /// </summary>
        /// <param name="crea">出力場所</param>
        /// <param name="odata">データ</param>
        private void WriteHeader(XlsxCreator crea, MoiExcelOutputDataCategory odata)
        {
            string tag = "";

            //開始年度
            tag = StartYear;
            crea.Cell(tag).Value = odata.StartYear;
            //終了年度
            tag = EndYear;
            crea.Cell(tag).Value = odata.EndYear;

            //指摘総数
            tag = MOICount;
            crea.Cell(tag).Value = odata.MoiCount;

            //船数
            tag = VesselCount;
            crea.Cell(tag).Value = odata.VesselCount;


            //平均指摘数
            tag = AverageCount;
            crea.Cell(tag).Value = odata.AverageCount;
        }


        /// <summary>
        /// データ書き込み
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="odata"></param>
        private void WriteData(XlsxCreator crea, MoiExcelOutputDataCategory odata)
        {
            string tag = "";
            List<MsViqCodeName> vnamelist = DcGlobal.Global.DBCache.MsViqCodeNameList;

            int no = 1;
            foreach (MsViqCodeName vname in vnamelist)
            {
                //ID
                tag = this.CreateTemplateNo(VIQ_Code, no);
                crea.Cell(tag).Value = vname.viq_code_name;

                //説明
                tag = this.CreateTemplateNo(VIQ_Code_Text, no);
                crea.Cell(tag).Value = vname.description;


                //指摘数
                //ﾃﾞｰﾀ数取得
                int count = 0;
                if (odata.DataDic.ContainsKey(vname.viq_code_name_id) == true)
                {
                    count = odata.DataDic[vname.viq_code_name_id].Count;
                }
                tag = this.CreateTemplateNo(VIQ_Code_Count, no);
                crea.Cell(tag).Value = count;



                no++;
            }
        }
       
        /// <summary>
        /// Excel書き込み本体
        /// </summary>
        /// <param name="filename">ファイル名</param>
        /// <param name="idata">ﾊﾟﾗﾒｰﾀ</param>
        /// <param name="odata">書き込みデータ</param>
        private void WriteExcel(string filename, MoiExcelOutputParameter idata, MoiExcelOutputDataCategory odata)
        {
            using (XlsxCreator crea = new XlsxCreator())
            {
                try
                {
                    crea.OpenBook(filename, filename);

                    
                    //ヘッダー書き込み
                    this.WriteHeader(crea, odata);


                    //ﾃﾞｰﾀ書き込み
                    for (int i = 0; i < 2; i++)
                    {
                        crea.SheetNo = i;
                        this.WriteData(crea, odata);
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
        /// Excel書き込み本体
        /// </summary>
        /// <param name="filename">ファイル名</param>
        /// <param name="idata">ﾊﾟﾗﾒｰﾀ</param>
        /// <param name="odata">書き込みデータ</param>
        private void WriteExcel2(string filename, MoiExcelOutputParameter idata, List<MoiExcelOutputDataCategory> odataList)
        {
            using (XlsxCreator crea = new XlsxCreator())
            {
                try
                {
                    crea.OpenBook(filename, filename);

                    int sheetNo = 0;
                    foreach (MoiExcelOutputDataCategory odata in odataList)
                    {
                        // 対象外のシートは削除
                        if (odata.DeleteSheet == true)
                        {
                            // グラフとデータシート削除
                            if (crea.SheetCount > 2)
                            {
                                crea.DeleteSheet(sheetNo, 2);
                                continue;
                            }
                        }

                        //ヘッダー書き込み
                        crea.SheetNo = sheetNo;
                        this.WriteHeader(crea, odata);

                        //ﾃﾞｰﾀ書き込み
                        for (int i = 0; i < 2; i++)
                        {
                            crea.SheetNo = sheetNo;
                            this.WriteData(crea, odata);
                            sheetNo++;
                        }
                    }

                    crea.FullCalcOnLoad = true; // 再計算モード有効
                    crea.ActiveSheet = crea.SheetCount - 2; // 最新の版のシートを選択
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// エクセルの出力 通信します waitestateせよ
        /// </summary>
        /// <param name="filename">出力ファイル名</param>
        /// <param name="idata">入力情報</param>
        public void OutputExcel(string filename, MoiExcelOutputParameter idata)
        {
            try
            {
                // VIQ Versionごとにデータ取得
                List<MoiExcelOutputDataCategory> odataList = new List<MoiExcelOutputDataCategory>();
                List<MsViqVersion> viqVersionList = DcGlobal.Global.DBCache.MsViqVersionList;
                foreach (MsViqVersion ver in viqVersionList)
                {
                    //情報収集
                    MoiExcelOutputDataCategory odata = SvcManager.SvcMana.GetMoiExcelDataCategory2(idata, ver);
                    if (odata == null)
                    {
                        throw new Exception("GetMoiExcelDataCategory NULL");
                    }
                    odataList.Add(odata);
                }

                //テンプレートダウンロード
                this.DownloadTemplateFile(filename);

                //書き込み
                this.WriteExcel2(filename, idata, odataList);
            }
            catch (Exception e)
            {
                //失敗したら保存したテンプレートが残るので消す
                if (File.Exists(filename) == true)
                {
                    File.Delete(filename);
                }

                throw new Exception("MoiExcelCategoryFile Exception", e);
            }

        }


    }
}
