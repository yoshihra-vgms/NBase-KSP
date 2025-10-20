using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using System.IO;

namespace NBaseHonsen.BLC
{
    public class 簡易動静情報出力処理
    {
        //テンプレートファイル名
        private const string TemplateFileName = "Template/Template_動静表.xlsx";

        //public static byte[] BLC_Excel動静表_取得(NBaseData.DAC.MsUser loginUser, DateTime today)
        //{
        //    string BaseFileName = "動静表";

        //    #region 元になるファイルの確認と出力ファイル生成
        //    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        //    path = path + "/Template/";
        //    string templateName = path + "Template_" + BaseFileName + ".xls";
        //    string outPutFileName = path + "outPut_" + BaseFileName + ".xls";

        //    bool exists = System.IO.File.Exists(templateName);
        //    if (exists == false)
        //    {
        //        //return null;
        //    }
        //    #endregion

        //    using (ExcelCreator.XlsCreator xls = new ExcelCreator.XlsCreator())
        //    {

        //        if (BaseFileName != null && BaseFileName.Length != 0)
        //        {
        //            // 指定されたテンプレートを元にファイルを作成
        //            xls.OpenBook(outPutFileName, templateName);
        //        }

        //        int offset_y = 0;

        //        // ヘッダー出力
        //        DateTime headColDateTime = today.AddDays(-1);
        //        xls.Cell("B2").Value = "船名";
        //        for (int n = 0; n < 11; n++)
        //        {
        //            xls.Cell("C2", n, offset_y).Value = headColDateTime.ToString("MM/dd(ddd)");
        //            headColDateTime = headColDateTime.AddDays(1);
        //        }
        //        xls.Cell("**date").Value = today.ToString("yyyy年MM月dd日");

        //        // 簡易動静情報の対象となる船を取得
        //        List<MsVessel> msVesselList = MsVessel.GetRecordsByKanidouseiEnabled(new MsUser());
        //        foreach (MsVessel msVessel in msVesselList)
        //        {
        //            xls.Cell("A3", 0, offset_y).Value = (offset_y + 1).ToString();

        //            string strVesselInfo = "";
        //            strVesselInfo = msVessel.VesselName;
        //            strVesselInfo += "\n";
        //            strVesselInfo += "飯野 タロウ";
        //            strVesselInfo += "\n";
        //            strVesselInfo += msVessel.Tel;
        //            strVesselInfo += "\n";
        //            strVesselInfo += msVessel.HpTel;
        //            xls.Cell("B3", 0, offset_y).Value = strVesselInfo;

        //            // 日付ごとの予定を出す
        //            DateTime colDateTime = today.AddDays(-1);
        //            for (int n = 0; n < 11; n++)
        //            {
        //                // データ取得
        //                PtKanidouseiInfo ptKanidouseiInfo = PtKanidouseiInfo.GetRecordByVesselAndEventDate(new MsUser(), msVessel.MsVesselID, colDateTime);

        //                // 予定無し
        //                if (ptKanidouseiInfo == null)
        //                {

        //                }
        //                // 予定有り
        //                else
        //                {
        //                    string strPortInfo = "";
        //                    strPortInfo = ptKanidouseiInfo.BashoName1;
        //                    if (ptKanidouseiInfo.KitiName1 != "")
        //                    {
        //                        strPortInfo += "：" + ptKanidouseiInfo.KitiName1;
        //                    }
        //                    strPortInfo += "\n";
        //                    strPortInfo += ptKanidouseiInfo.KanidouseiInfoShubetuName1;
        //                    strPortInfo += "\n";
        //                    strPortInfo += "\n";
        //                    strPortInfo += ptKanidouseiInfo.BashoName2;
        //                    if (ptKanidouseiInfo.KitiName2 != "")
        //                    {
        //                        strPortInfo += "：" + ptKanidouseiInfo.KitiName2;
        //                    }
        //                    strPortInfo += "\n";
        //                    strPortInfo += ptKanidouseiInfo.KanidouseiInfoShubetuName2;
        //                    xls.Cell("C3", n, offset_y).Value = strPortInfo;
        //                }

        //                colDateTime = colDateTime.AddDays(1);
        //            }

        //            offset_y++;
        //        }

        //        #region 罫線を出力
        //        for (int i = 0; i < (offset_y + 2); i++)
        //        {
        //            xls.Cell("A1:M1", 0, i).Attr.LineBottom = ExcelCreator.xlLineStyle.lsNormal;
        //        }

        //        string toA = "A" + (offset_y + 2).ToString();
        //        for (int i = 0; i < 13; i++)
        //        {
        //            xls.Cell("A2:" + toA, i, 0).Attr.LineRight = ExcelCreator.xlLineStyle.lsNormal;
        //        }

        //        #endregion

        //        xls.CloseBook(true);

        //        if (xls.ErrorNo == ExcelCreator.xlErrorNo.errNoError)
        //        {
        //            //res = true;
        //        }
        //    }

        //    byte[] bytBuffer;
        //    #region バイナリーへ変換
        //    using (FileStream objFileStream = new FileStream(outPutFileName, FileMode.Open))
        //    {
        //        long lngFileSize = objFileStream.Length;

        //        bytBuffer = new byte[(int)lngFileSize];
        //        objFileStream.Read(bytBuffer, 0, (int)lngFileSize);
        //        objFileStream.Close();
        //    }
        //    #endregion

        //    return bytBuffer;
        //}
    }
}
