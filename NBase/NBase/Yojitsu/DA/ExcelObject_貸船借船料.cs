using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;

namespace Yojitsu.DA
{
    public class ExcelObject_貸船借船料
    {
        public enum ConfigType { 貸船料, 借船料 }

        private static readonly int YEAR_ROW = 1;


        private static readonly int[] 貸船料_HIMOKU_IDS = { Constants.MS_HIMOKU_ID_貸船料, Constants.MS_HIMOKU_ID_ADDCOMM, Constants.MS_HIMOKU_ID_その他海運業費用 };
        private static readonly int[] 貸船料_ROWS = { 4, 8, 12 };

        private static readonly int[] 借船料_HIMOKU_IDS = { Constants.MS_HIMOKU_ID_借船料 };
        private static readonly int[] 借船料_ROWS = { 4 };

        private static readonly int START_COL = 3;
        private static readonly int WIDTH = 13;


        internal static ExcelObject_貸船借船料 Create(string fileName,
                                                          ExcelCreator.Xlsx.XlsxCreator xls,
                                                          ConfigType configType,
                                                          Excelファイル読込Form.YosanObjectCollection yosanObject,
                                                          BgYosanHead yosanHead,
                                                          int msVesselId)
        {
            ExcelObject_貸船借船料 obj = new ExcelObject_貸船借船料();

            int[] himokuIds = null;
            int[] rows = null;

            if (configType == ConfigType.貸船料)
            {
                xls.SheetNo = 1;
                himokuIds = 貸船料_HIMOKU_IDS;
                rows = 貸船料_ROWS;
            }
            else if (configType == ConfigType.借船料)
            {
                xls.SheetNo = 2;
                himokuIds = 借船料_HIMOKU_IDS;
                rows = 借船料_ROWS;
            }

            int col = START_COL;
            while (true)
            {
                if (xls.Pos(col, YEAR_ROW).Value.ToString().Trim().Length == 0)
                {
                    break;
                }

                DateTime date = DateTime.FromOADate(Convert.ToDouble(xls.Pos(col, YEAR_ROW).Value));
                int year = int.Parse(date.ToString("yyyy/MM/dd").Substring(0, 4));

                for (int k = 0; k < rows.Length; k++)
                {
                    // 初年度.
                     if (year == yosanHead.Year)
                    {
                        SetYosan_月次(xls, yosanObject, msVesselId, himokuIds[k], col, rows[k], year, yosanHead);
                    }
                    // 次年度以降.
                     else
                    {
                        SetYosan_年次(xls, yosanObject, msVesselId, himokuIds[k], col, rows[k], year);
                    }
                }

                col += WIDTH;
            }

            return obj;
        }


        private static void SetYosan_月次(ExcelCreator.Xlsx.XlsxCreator xls, Excelファイル読込Form.YosanObjectCollection yosanObject, int msVesselId, int himokuId, int col, int row, int year, BgYosanHead yosanHead)
        {
            for (int i = 0; i < NBaseData.DS.Constants.PADDING_MONTHS.Length; i++)
            {
                decimal amount = decimal.Parse(xls.Pos(col + i, row + 1).Value.ToString().Trim());
                decimal dollarAmount = decimal.Parse(xls.Pos(col + i, row + 2).Value.ToString().Trim());

                string nengetsu;

                if (i < 9)
                {
                    nengetsu = year.ToString() + NBaseData.DS.Constants.PADDING_MONTHS[i];
                }
                else
                {
                    nengetsu = (year + 1).ToString() + NBaseData.DS.Constants.PADDING_MONTHS[i];
                }

                if (yosanHead.YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.見直し && Constants.IsKamiki(nengetsu))
                {
                    continue;
                }

                yosanObject.AddAmount(msVesselId, himokuId, nengetsu, amount * 1000, dollarAmount);
            }
        }


        private static void SetYosan_年次(ExcelCreator.Xlsx.XlsxCreator xls, Excelファイル読込Form.YosanObjectCollection yosanObject, int msVesselId, int himokuId, int col, int row, int year)
        {
            decimal amount = 0;
            decimal dollarAmount = 0;

            string nengetsu = year.ToString();

            for (int i = 0; i < NBaseData.DS.Constants.PADDING_MONTHS.Length; i++)
            {
                amount += decimal.Parse(xls.Pos(col + i, row + 1).Value.ToString().Trim());
                dollarAmount += decimal.Parse(xls.Pos(col + i, row + 2).Value.ToString().Trim());
            }

            yosanObject.AddAmount(msVesselId, himokuId, nengetsu, amount * 1000, dollarAmount);
        }
    }
}
