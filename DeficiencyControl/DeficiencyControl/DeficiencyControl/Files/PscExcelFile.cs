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
    /// PSC帳票
    /// </summary>
    public class PscExcelFile : BaseExcelFile
    {

        #region Excel変数名定義

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
                byte[] data = SvcManager.SvcMana.GetPSCExcelTemplate();
                if (data == null)
                {
                    throw new Exception("GetPSCExcelTemplate NULL");
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
        /// Excel書き込み本体
        /// </summary>
        /// <param name="filename">書き込みファイル名</param>
        /// <param name="idata">パラメータ</param>
        /// <param name="odata">出力データ</param>
        private void WriteExcel(string filename, PscOutputParameter idata, PscOutputData odata)
        {
            using (XlsxCreator crea = new XlsxCreator())
            {
                try
                {
                    crea.OpenBook(filename, filename);

                    //実績一覧
                    {
                        PscExcelFilePscList psclist = new PscExcelFilePscList(0);
                        psclist.Write(crea, idata, odata);
                    }

                    //DefieicnecyCode集計タブ
                    {
                        PscExcelFileDeficiencyCode code = new PscExcelFileDeficiencyCode(1);
                        code.Write(crea, idata, odata);
                    }
                    //ActionCode集計タブ
                    {
                        PscExcelFileActionCode ac = new PscExcelFileActionCode(2);
                        ac.Write(crea, idata, odata);
                    }


                    //初めのシートを選択
                    crea.SheetNo = 0;


                    //再計算
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


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// エクセルの出力 通信します waitestateせよ
        /// </summary>
        /// <param name="filename">出力ファイル名</param>
        /// <param name="idata">入力情報</param>
        public void OutputExcel(string filename, PscOutputParameter idata)
        {
            try
            {
                //情報収集
                PscOutputData odata = SvcManager.SvcMana.GetPSCOutputData(idata);
                if (odata == null)
                {
                    throw new Exception("GetPSCOutputData NULL");
                }


                //テンプレートダウンロード
                this.DownloadTemplateFile(filename);

                //書き込み
                this.WriteExcel(filename, idata, odata);
            }
            catch (Exception e)
            {
                //失敗したら保存したテンプレートが残るので消す
                if (File.Exists(filename) == true)
                {
                    File.Delete(filename);
                }

                throw new Exception("PscExcelFile Exception", e);
            }

        }

    }
}
