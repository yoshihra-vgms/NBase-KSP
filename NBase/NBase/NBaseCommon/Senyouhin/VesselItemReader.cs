using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseData.DAC;
using NBaseCommon.Nyukyo;

namespace NBaseCommon.Senyouhin
{
    public class VesselItemReader
    {
        private int FieldLength = 7;
        private DateTime RenewDate;
        private string RenewUserId;

        private readonly string fileName;
        private static readonly int START_ROW = 10;
        private static readonly int MAX_ROWS = 65536;

        private readonly List<MsVesselItemCategory> msVesselItemCategorys;
        private readonly List<MsVesselItemVessel> msVesselItemVessels;
        private readonly List<MsTani> msTanis;


        public VesselItemReader(string fileName, IVesselItemDacProxy dacProxy, int msVesselId)
        {
            this.fileName = fileName;

            msVesselItemCategorys = dacProxy.MsVesselItemCategory_GetRecords(NBaseCommon.Common.LoginUser);
            msVesselItemVessels = dacProxy.MsVesselItemVessel_GetRecords(NBaseCommon.Common.LoginUser, msVesselId);
            msTanis = dacProxy.MsTani_GetRecords(NBaseCommon.Common.LoginUser);
        }


        public List<OdThiItem> Read()
        {
            RenewDate = DateTime.Now;
            RenewUserId = NBaseCommon.Common.LoginUser.MsUserID;

            bool bushoIsNull = true;
            bool shiyouKatashikiIsNull = true;

            List<OdThiItem> result = new List<OdThiItem>();

            ExcelCreator.Xlsx.XlsxCreator xls = new ExcelCreator.Xlsx.XlsxCreator();

            try
            {
                if (xls.ReadBook(fileName) == 0)
                {
                    OdThiItem parentItem = null;

                    string busho = "";
                    string shiyouKatashiki = "";

                    for (int i = START_ROW; i < MAX_ROWS; i++)
                    {                        
                        string[] fields = ReadLine(xls, i);

                        if (fields == null)
                        {
                            if (shiyouKatashikiIsNull == true && bushoIsNull == true)
                            {
                                break;
                            }
                            if (shiyouKatashikiIsNull == false)
                            {
                                shiyouKatashikiIsNull = true;
                                parentItem = null;
                            }
                            else if (bushoIsNull == false)
                            {
                                bushoIsNull = true;
                                parentItem = null;
                            }
                            continue;
                        }

                        string no = fields[0];
                        string name = fields[1];
                        string tani = fields[2];
                        string zaikoCount = fields[3];
                        string iraiCount = fields[4];
                        string sateiCount = fields[5];
                        string bikou = fields[6];

                        if (bushoIsNull == true)
                        {
                            if (no.Length == 0 && name.Length > 0)
                            {
                                busho = name;
                                bushoIsNull = false;

                                continue;
                            }
                            else
                            {
                                throw new InvalidFormatException("区分、仕様・型式名、詳細品目名のいずれかが空です [行番号 " + (i + 1).ToString() + "]。");
                            }
                        }
                        if (shiyouKatashikiIsNull == true)
                        {
                            if (no.Length == 0 && name.Length > 0)
                            {
                                shiyouKatashiki = name;
                                shiyouKatashikiIsNull = false;

                                continue;
                            }
                            else
                            {
                                throw new InvalidFormatException("区分、仕様・型式名、詳細品目名のいずれかが空です [行番号 " + (i + 1).ToString() + "]。");
                            }
                        }


                        // 部署名、区分、仕様・型式カラムのいずれも空でなければ、品目を生成する.
                        if (parentItem == null && no.Length > 0 && name.Length > 0)
                        {
                            parentItem = CreateOdThiItem(i, busho, shiyouKatashiki);
                            result.Add(parentItem);
                        }

                        if (parentItem == null)
                        {
                            throw new InvalidFormatException("番号、詳細品目名、単位のいずれかが空です。または、フォーマットの不正です。 [行番号 " + (i + 1).ToString() + "]。");
                        }

                        // 空行がないまま、部署や仕様・型式を記載している場合、エラーとする
                        if (no.Length == 0 || name.Length == 0 || tani.Length == 0)
                        {
                            throw new InvalidFormatException("番号、詳細品目名、単位のいずれかが空です。または、フォーマットの不正です。 [行番号 " + (i + 1).ToString() + "]。");
                        }

                        // 品目詳細を生成して、親品目に追加する. 
                        OdThiShousaiItem shousaiItem = CreateOdThiShousaiItem(i, shiyouKatashiki, name, tani, zaikoCount, iraiCount, sateiCount, bikou);
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

        
        private OdThiItem CreateOdThiItem(int rowNo, string busho, string shiyouKatashiki)
        {

            if (busho.Length > 50)
            {
                throw new InvalidFormatException("区分は50文字以下で入力してください [行番号 " + (rowNo + 1).ToString() + "]。");
            }

            int categoryNo = ToMsVesselItemCategoryID(shiyouKatashiki);

            if (categoryNo < 0)
            {
                throw new InvalidFormatException("無効な仕様・型式です [行番号 " + (rowNo + 1).ToString() + "]。");
            }

            OdThiItem item = new OdThiItem();
            item.OdThiItemID = System.Guid.NewGuid().ToString();
            item.Header = busho;
            //item.MsItemSbtID = ;
            //item.MsItemSbtName = ;
            item.ItemName = shiyouKatashiki;
            item.RenewDate = RenewDate;
            item.RenewUserID = RenewUserId;
            
            return item;
        }


        private OdThiShousaiItem CreateOdThiShousaiItem(int rowNo, string shiyouKatashiki, string shousaiItemName, string tani, string zaikoCountStr, string iraiCountStr, string sateiCountStr, string bikou)
        {
            string msVesselItemId = null;
            int categoryNo = ToMsVesselItemCategoryID(shiyouKatashiki);
            if (categoryNo == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
            {
                msVesselItemId = ToMsVesselItemVesselID(shousaiItemName);
                if (msVesselItemId == null)
                {
                    throw new InvalidFormatException("無効なペイントです [行番号 " + (rowNo + 1).ToString() + "]。");
                }
            }
            else
            {
                if (shousaiItemName.Length > 500)
                {
                    throw new InvalidFormatException("詳細品目は500文字以下で入力してください [行番号 " + (rowNo + 1).ToString() + "]。");
                }
            }

            string taniID = ToMsTaniID(tani);
            //if (taniID == null)
            if (taniID == null && (tani != null && tani.Length > 0)) 
            {
                throw new InvalidFormatException("無効な単位です [行番号 " + (rowNo + 1).ToString() + "]。");
            }

            int zaikoCount;
            Int32.TryParse(zaikoCountStr, out zaikoCount);

            int iraiCount;
            Int32.TryParse(iraiCountStr, out iraiCount);
            
            int sateiCount;
            Int32.TryParse(sateiCountStr, out sateiCount);

            if (bikou.Length > 500)
            {
                throw new InvalidFormatException("備考（品名、規格等）は500文字以下で入力してください [行番号 " + (rowNo + 1).ToString() + "]。");
            }

            OdThiShousaiItem shousaiItem = new OdThiShousaiItem();
            shousaiItem.OdThiShousaiItemID = System.Guid.NewGuid().ToString();
            shousaiItem.ShousaiItemName = shousaiItemName;
            shousaiItem.MsVesselItemID = msVesselItemId;
            shousaiItem.MsTaniID = taniID;
            shousaiItem.MsTaniName = tani;
            shousaiItem.ZaikoCount = zaikoCount;
            shousaiItem.Count = iraiCount;
            shousaiItem.Sateisu = sateiCount;
            shousaiItem.Bikou = bikou;
            shousaiItem.RenewDate = RenewDate;
            shousaiItem.RenewUserID = RenewUserId;

            return shousaiItem;
        }

        
        private string[] ReadLine(ExcelCreator.Xlsx.XlsxCreator xls, int i)
        {
            string[] fields = new string[FieldLength];
            bool isNotEmpty = false;

            for (int k = 0; k < FieldLength; k++)
            {
                //fields[k] = xls.Pos(k, i).Value.ToString();
                if (xls.Pos(k, i).Value != null)
                {
                    if (xls.Pos(k, i).Value is string)
                        fields[k] = (string)xls.Pos(k, i).Value;
                    else
                    {
                        fields[k] = xls.Pos(k, i).Value.ToString();
                    }
                }
                else
                {
                    fields[k] = "";
                }

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


        private int ToMsVesselItemCategoryID(string name)
        {
            foreach (MsVesselItemCategory cat in msVesselItemCategorys)
            {
                if (cat.CategoryName == name)
                {
                    return cat.CategoryNumber;
                }
            }

            return -1;
        }

        private string ToMsVesselItemVesselID(string name)
        {
            foreach (MsVesselItemVessel item in msVesselItemVessels)
            {
                if (item.CategoryNumber == MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント))
                {
                    if (item.VesselItemName == name)
                    {
                        return item.MsVesselItemID;
                    }
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
