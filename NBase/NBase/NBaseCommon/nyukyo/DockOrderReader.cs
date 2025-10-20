using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseData.DAC;

namespace NBaseCommon.Nyukyo
{
    public class DockOrderReader
    {
        private readonly string fileName;
        private static readonly int MAX_ROWS = 65536;
        private static readonly string EOF_STR = "以上";

        private readonly List<MsItemSbt> msItemSbts;
        private readonly List<MsTani> msTanis;

        
        public DockOrderReader(string fileName, IDockOrderDacProxy dacProxy)
        {
            this.fileName = fileName;

            msItemSbts = dacProxy.MsItemSbt_GetRecords(NBaseCommon.Common.LoginUser);
            msTanis = dacProxy.MsTani_GetRecords(NBaseCommon.Common.LoginUser);
        }


        public List<OdThiItem> Read()
        {
            List<OdThiItem> result = new List<OdThiItem>();

            ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator();

            try
            {
                if (xls.ReadBook(fileName) == 0)
                {
                    OdThiItem parentItem = null;

                    for (int i = 0; i < MAX_ROWS; i++)
                    {
                        if (i == 0)
                        {
                            continue;
                        }
                        
                        if ((string)xls.Pos(0, i).Value == EOF_STR)
                        {
                            break;
                        }

                        string[] fields = ReadLine(xls, i);

                        if (fields == null)
                        {
                            continue;
                        }

                        string thiIraiSbtName = fields[0];
                        string itemName = fields[1];
                        string shousaiItemName = fields[2];
                        string tani = fields[3];
                        string zaikoCount = fields[4];
                        string iraiCount = fields[5];
                        string bikou = fields[6];

                        // 区分、仕様・型式カラムのいずれも空でなければ、品目を生成する.
                        if (thiIraiSbtName.Length > 0 && itemName.Length > 0)
                        {
                            parentItem = CreateOdThiItem(i, thiIraiSbtName, thiIraiSbtName, itemName);
                            result.Add(parentItem);
                        }

                        if (parentItem == null)
                        {
                            throw new InvalidFormatException("区分、仕様・型式のいずれかが空です [行番号 " + i + "]。");
                        }

                        // 品目詳細を生成して、親品目に追加する. 
                        OdThiShousaiItem shousaiItem = CreateOdThiShousaiItem(i, shousaiItemName, tani, zaikoCount, iraiCount, bikou);
                        parentItem.OdThiShousaiItems.Add(shousaiItem);
                    }
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
        
            return result;
        }

        
        private OdThiItem CreateOdThiItem(int rowNo, string header, string thiIraiSbtName, string itemName)
        {           
            //if (header.Length > 50)
            //{
            //    throw new InvalidFormatException("ヘッダは50文字以下で入力してください [行番号 " + rowNo + "]。");
            //}

            string thiIraiSbtID = ToMsItemSbtID(thiIraiSbtName);
            
            if (thiIraiSbtID == null)
            {
                throw new InvalidFormatException("無効な区分です [行番号 " + rowNo + "]。");
            }

            if (itemName.Length > 500)
            {
                throw new InvalidFormatException("仕様・型式は500文字以下で入力してください [行番号 " + rowNo + "]。");
            }

            OdThiItem item = new OdThiItem();
            item.OdThiItemID = System.Guid.NewGuid().ToString();
            item.Header = header;
            item.MsItemSbtID = thiIraiSbtID;
            item.MsItemSbtName = thiIraiSbtName;
            item.ItemName = itemName;
            item.RenewDate = DateTime.Now;
            item.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
            
            return item;
        }


        private OdThiShousaiItem CreateOdThiShousaiItem(int rowNo, string shousaiItemName, string tani, string zaikoCountStr, string iraiCountStr, string bikou)
        {
            if (shousaiItemName.Length > 500)
            {
                throw new InvalidFormatException("詳細品目は500文字以下で入力してください [行番号 " + rowNo + "]。");
            }

            string taniID = ToMsTaniID(tani);

            if (taniID == null)
            {
                throw new InvalidFormatException("無効な単位です [行番号 " + rowNo + "]。");
            }

            int zaikoCount;
            Int32.TryParse(zaikoCountStr, out zaikoCount);

            int iraiCount;
            Int32.TryParse(iraiCountStr, out iraiCount);

            if (bikou.Length > 500)
            {
                throw new InvalidFormatException("備考（品名、規格等）は500文字以下で入力してください [行番号 " + rowNo + "]。");
            }

            OdThiShousaiItem shousaiItem = new OdThiShousaiItem();
            shousaiItem.OdThiShousaiItemID = System.Guid.NewGuid().ToString();
            shousaiItem.ShousaiItemName = shousaiItemName;
            shousaiItem.MsTaniID = taniID;
            shousaiItem.MsTaniName = tani;
            shousaiItem.ZaikoCount = zaikoCount;
            shousaiItem.Count = iraiCount;
            shousaiItem.Sateisu = iraiCount;
            shousaiItem.Bikou = bikou;
            shousaiItem.RenewDate = DateTime.Now;
            shousaiItem.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            return shousaiItem;
        }

        
        private string[] ReadLine(ExcelCreator.Xlsx.XlsxCreator xls, int i)
        {
            string[] fields = new string[8];
            bool isNotEmpty = false;
            
            for(int k = 0; k < 8; k++)
            {
                fields[k] = xls.Pos(k, i).Value.ToString();

                if (fields[k] != null && fields[k].Length > 0)
                {
                    isNotEmpty = true;
                }
            }

            if (isNotEmpty)
            {
                return fields;
            }
            else
            {
                return null;
            }
        }

        
        private string ToMsItemSbtID(string thiIraiSbtName)
        {
            foreach (MsItemSbt sbt in msItemSbts)
            {
                if (sbt.ItemSbtName == thiIraiSbtName)
                {
                    return sbt.MsItemSbtID;
                }
            }

            return null;
        }

        
        private string ToMsTaniID(string taniName)
        {
            foreach (MsTani tani in msTanis)
            {
                if (tani.TaniName == taniName)
                {
                    return tani.MsTaniID;
                }
            }

            return null;
        }
    }
}
