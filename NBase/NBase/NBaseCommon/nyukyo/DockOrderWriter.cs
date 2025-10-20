using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseData.BLC;

namespace NBaseCommon.Nyukyo
{
    public class DockOrderWriter
    {

        private readonly string fileName;
        private readonly IDockOrderDacProxy dacProxy;
  
        private readonly List<MsItemSbt> msItemSbts;
        private readonly List<MsTani> msTanis;

        
        public DockOrderWriter(string fileName, IDockOrderDacProxy dacProxy)
        {
            this.fileName = fileName;
            this.dacProxy = dacProxy;

            msItemSbts = dacProxy.MsItemSbt_GetRecords(NBaseCommon.Common.LoginUser);
            msTanis = dacProxy.MsTani_GetRecords(NBaseCommon.Common.LoginUser);
        }


        public void Write(int msVesselID, string msThiIraiShousaiID)
        {
            List<OdThiItem> odThiItems = dacProxy.BLC_直近ドックオーダー品目(NBaseCommon.Common.LoginUser,
                                                                             msVesselID,
                                                                             msThiIraiShousaiID);
            if (odThiItems == null || odThiItems.Count == 0)
            {
                throw new NoDataException("対象データがありません");
            }
            WriteFile(odThiItems);
        }

        
        private void WriteFile(List<OdThiItem> odThiItems)
        {
            ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator();

            try
            {
                //if (xls.CreateBook(fileName, 1, xlVersion.ver2003) == 0)
                if (xls.CreateBook(fileName, 1, ExcelCreator.xlsxVersion.ver2013) == 0)
                {
                    WriteLine(xls, 0, "区分", "仕様・型式", "詳細品目", "単位", "在庫数", "依頼数", "備考（品名、規格等）");

                    int i = 1;
                    foreach (OdThiItem item in odThiItems)
                    {
                        WriteLine(xls, i, ToMsItemSbtName(item.MsItemSbtID), item.ItemName);
                        
                        foreach (OdThiShousaiItem si in item.OdThiShousaiItems)
                        {
                            WriteLine(xls, i++, 2, si.ShousaiItemName, ToMsTaniName(si.MsTaniID), si.ZaikoCount.ToString(), si.Count.ToString(), si.Bikou);
                        }

                        i++;
                    }

                    WriteLine(xls, i, "以上");
                }
            }
            catch (InvalidFormatException e)
            {
                throw e;
            }
            finally
            {
                xls.CloseBook(true);
            }
        }

        
        private void WriteLine(ExcelCreator.Xlsx.XlsxCreator xls, int rowNo, params string[] fields)
        {
            WriteLine(xls, rowNo, 0, fields);
        }

        
        private void WriteLine(ExcelCreator.Xlsx.XlsxCreator xls, int rowNo, int columnNo, params string[] fields)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                int num;
                bool isNumber = Int32.TryParse(fields[i], out num);

                if (isNumber)
                {
                    xls.Pos(i + columnNo, rowNo).Value = num;
                }
                else
                {
                    xls.Pos(i + columnNo, rowNo).Value = fields[i];
                }
            }
        }


        private string ToMsItemSbtName(string id)
        {
            foreach (MsItemSbt sbt in msItemSbts)
            {
                if (sbt.MsItemSbtID == id)
                {
                    return sbt.ItemSbtName;
                }
            }

            return null;
        }


        private string ToMsTaniName(string id)
        {
            foreach (MsTani tani in msTanis)
            {
                if (tani.MsTaniID == id)
                {
                    return tani.TaniName;
                }
            }

            return null;
        }
    }
}
