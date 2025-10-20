using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseUtil;
using NBaseData.DS;
using System.IO;

namespace NBaseCommon.Senin.Excel
{
    public class クルーリスト出力
    {
        private readonly string templateFilePath;
        private readonly string outputFilePath;


        public クルーリスト出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;
        }


        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, DateTime date, int msVesselId)
        {
           
            using (ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator())
            {
                //-----------------------
                //2013/12/17 コメントアウト m.y
                //xls.OpenBook(outputFilePath, templateFilePath);                
                //-----------------------
                //2013/12/17 変更:OpenBookエラーをなげる m.y
                int xlsRet = xls.OpenBook(outputFilePath, templateFilePath);
                if (xlsRet < 0)
                {
                    Exception xlsEx = null;
                    if (xls.ErrorNo == ExcelCreator.ErrorNo.TempCreate)
                    {
                        xlsEx = new Exception("ディスクに十分な空き領域がありません。");
                    }
                    else
                    {
                        xlsEx = new Exception("サーバでエラーが発生しました。");
                    }
                    throw xlsEx;
                }
                //-----------------------

                // 全船
                if (msVesselId == int.MinValue)
                {
                    int i = 0;
                    foreach (MsVessel v in MsVessel.GetRecordsBySeninEnabled(loginUser))
                    {
                        i++;
                        xls.CopySheet(0, i, "");
                        xls.SheetNo = i;
                        xls.SheetName = v.VesselName;

                        _CreateFile(loginUser, xls, seninTableCache, date, v);
                    }
                    xls.DeleteSheet(0, 1);
                }
                // 各船
                else
                {
                    MsVessel vessel = MsVessel.GetRecordByMsVesselID(loginUser, msVesselId);
                    xls.SheetName = vessel.VesselName;

                    _CreateFile(loginUser, xls, seninTableCache, date, vessel);
                }

                xls.CloseBook(true);
            }
        }


        private void _CreateFile(MsUser loginUser, ExcelCreator.Xlsx.XlsxCreator xls, SeninTableCache seninTableCache, DateTime date, MsVessel vessel)
        {
            SiCardFilter filter = new SiCardFilter();
            filter.MsVesselIDs.Add(vessel.MsVesselID);
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船));
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船休暇));
            filter.Start = date;
            filter.End = date;
            filter.OrderByStr = "OrderByMsSiShokumeiId";
            //filter.RetireFlag = 0; // 2013.09.26 : クルーリストは退職していても出力する

            List<SiCard> cards = SiCard.GetRecordsByFilter(loginUser, filter);

            xls.Cell("**TODAY").Value = "日付: " + DateTime.Now.ToShortDateString();
            xls.Cell("**VESSEL").Value = vessel.VesselName;
            xls.Cell("**NATIONALITY").Value = vessel.Nationality;

            List<TreeListViewUtils.SiCardRow> rows = TreeListViewUtils.CreateRowData(cards, loginUser, seninTableCache);

            int startRow = 21;
            int i = 0;

            foreach (TreeListViewUtils.SiCardRow r in rows)
            {
                // 最大24名まで出力.
                if (i > 23)
                {
                    break;
                }

                // 番号
                xls.Cell("A" + (startRow + i)).Value = (i + 1).ToString();

                // 氏名
                xls.Cell("C" + (startRow + i)).Value = r.card.SeninName;

                // 国籍
                xls.Cell("O" + (startRow + i)).Value = "日本";

                // 職名
                xls.Cell("K" + (startRow + i)).Value = seninTableCache.ToTopShokumeiAbbrStr(loginUser, r.card.SiLinkShokumeiCards);

                // 生年月日
                xls.Cell("S" + (startRow + i)).Value = r.card.SeninBirthday.ToShortDateString();

                // 船員手帳番号
                List<SiMenjou> menjous = SiMenjou.GetRecordsByMsSeninIDAndMsSiMenjouID(loginUser, r.card.MsSeninID, seninTableCache.MsSiMenjou_船員手帳ID(loginUser));

                if (menjous.Count > 0)
                {
                    xls.Cell("Z" + (startRow + i)).Value = menjous[0].No;
                }


                //===================================================
                // 2023.2 船員写真をリストに貼り付ける
                //===================================================
                //var senin = MsSenin.GetRecord(loginUser, r.card.MsSeninID);
                //if (senin.Picture.Length > 0)
                //{
                //    var templatePath = SaveWorkFile(loginUser, senin.Picture);
                //    System.Drawing.Point p1 = xls.GetVarNamePos("**写真" + (i+1).ToString(), 0); ;
                //    xls.Pos(p1.X, p1.Y, p1.X, p1.Y).Drawing.AddImage(templatePath);
                //}


                i++;
            }
        }



        public static string SaveWorkFile(MsUser loginUser, byte[] fileData)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            var workFilePath = path + "\\船員写真_[" + loginUser.FullName + "].jpg";
            using (var fs = new FileStream(workFilePath, FileMode.Create))
            {
                fs.Write(fileData, 0, fileData.Length);
            }
            return workFilePath;
        }
    }
}
