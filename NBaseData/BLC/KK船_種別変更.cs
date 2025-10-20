using NBaseData.DAC;
using ORMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBaseData.BLC
{
    public class KK船_種別変更
    {
        public static bool 変更(MsUser loginUser, OdThi odThi)
        {
            bool ret = true;


            OdThi orgOdThi = OdThi.GetRecord(loginUser, odThi.OdThiID);


            // 種別が変更されている
            // 修繕　→　船用品 の場合は、データの更新が必要になる
            if (orgOdThi.MsThiIraiSbtID != odThi.MsThiIraiSbtID &&
                orgOdThi.MsThiIraiSbtID == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.修繕))
            {                
                List<OdThiItem> odThiItemList = OdThiItem.GetRecordsByOdThiID(loginUser, odThi.OdThiID);
                List<OdThiShousaiItem> odThiShousaiItemList = OdThiShousaiItem.GetRecordsByOdThiID(loginUser, odThi.OdThiID);


                List<OdMm> odMmList = OdMm.GetRecordsByOdThiID(loginUser, odThi.OdThiID);
                List<OdMmItem> odMmItemList = null;
                List<OdMmShousaiItem> odMmShousaiItemList = null;
                if (odMmList != null && odMmList.Count() > 0)
                {
                    odMmItemList = new List<OdMmItem>();
                    odMmShousaiItemList = new List<OdMmShousaiItem>();

                    foreach (OdMm odMm in odMmList)
                    {
                        odMmItemList.AddRange(OdMmItem.GetRecordsByOdMmID(loginUser, odMm.OdMmID));
                        odMmShousaiItemList.AddRange(OdMmShousaiItem.GetRecordsByOdMmID(loginUser, odMm.OdMmID));
                    }  
                }

                List<OdMk> odMkList = OdMk.GetRecordsByOdThiID(loginUser, odThi.OdThiID);
                List<OdMkItem> odMkItemList = null;
                List<OdMkShousaiItem> odMkShousaiItemList = null;
                if (odMkList != null && odMkList.Count() > 0)
                {
                    odMkItemList = new List<OdMkItem>();
                    odMkShousaiItemList = new List<OdMkShousaiItem>();

                    foreach (OdMk odMk in odMkList)
                    {
                        odMkItemList.AddRange(OdMkItem.GetRecordsByOdMkID(loginUser, odMk.OdMkID));
                        odMkShousaiItemList.AddRange(OdMkShousaiItem.GetRecordsByOdMkID(loginUser, odMk.OdMkID));
                    }
                }

                List<OdJry> odJryList = OdJry.GetRecordsByOdThiId(loginUser, odThi.OdThiID);
                List<OdJryItem> odJryItemList = null;
                List<OdJryShousaiItem> odJryShousaiItemList = null;
                if (odJryList != null && odJryList.Count() > 0)
                {
                    odJryItemList = new List<OdJryItem>();
                    odJryShousaiItemList = new List<OdJryShousaiItem>();

                    foreach (OdJry odJry in odJryList)
                    {
                        odJryItemList.AddRange(OdJryItem.GetRecordsByOdJryID(loginUser, odJry.OdJryID));
                        odJryShousaiItemList.AddRange(OdJryShousaiItem.GetRecordsByOdJryID(loginUser, odJry.OdJryID));
                    }
                }

                List<OdShr> odShrList = OdShr.GetRecordsByOdThiId(loginUser, odThi.OdThiID);
                List<OdShrItem> odShrItemList = null;
                List<OdShrShousaiItem> odShrShousaiItemList = null;
                if (odShrList != null && odShrList.Count() > 0)
                {
                    odShrItemList = new List<OdShrItem>();
                    odShrShousaiItemList = new List<OdShrShousaiItem>();

                    foreach (OdShr odShr in odShrList)
                    {
                        odShrItemList.AddRange(OdShrItem.GetRecordsByOdShrID(loginUser, odShr.OdShrID));
                        odShrShousaiItemList.AddRange(OdShrShousaiItem.GetRecordsByOdShrID(loginUser, odShr.OdShrID));
                    }
                }



                using (DBConnect dbConnect = new DBConnect())
                {
                    dbConnect.BeginTransaction();

                    try
                    {
                        if (odThiShousaiItemList != null && odThiItemList != null)
                        {
                            foreach (OdThiShousaiItem shousaiItem in odThiShousaiItemList)
                            {
                                var item = odThiItemList.Where(o => o.OdThiItemID == shousaiItem.OdThiItemID).First();
                                if (item == null)
                                    continue;

                                if (shousaiItem.Bikou != null && shousaiItem.Bikou.Length > 0)
                                    shousaiItem.Bikou += System.Environment.NewLine;

                                shousaiItem.Bikou += item.ItemName;

                                ret = shousaiItem.UpdateRecord(loginUser);
                                if (ret == false)
                                    break;
                            }
                        }

                        if (ret && odThiItemList != null)
                        {
                            foreach (OdThiItem item in odThiItemList)
                            {
                                item.ItemName = "共用";

                                ret = item.UpdateRecord(loginUser);
                                if (ret == false)
                                    break;
                            }
                        }


                        if (ret && odMmShousaiItemList != null && odMmShousaiItemList != null)
                        {
                            foreach (OdMmShousaiItem shousaiItem in odMmShousaiItemList)
                            {
                                var item = odMmItemList.Where(o => o.OdMmItemID == shousaiItem.OdMmItemID).First();
                                if (item == null)
                                    continue;

                                if (shousaiItem.Bikou != null && shousaiItem.Bikou.Length > 0)
                                    shousaiItem.Bikou += System.Environment.NewLine;

                                shousaiItem.Bikou += item.ItemName;

                                ret = shousaiItem.UpdateRecord(loginUser);
                                if (ret == false)
                                    break;
                            }
                        }
                        if (ret && odMmShousaiItemList != null)
                        {
                            foreach (OdMmItem item in odMmItemList)
                            {
                                item.ItemName = "共用";

                                ret = item.UpdateRecord(loginUser);
                                if (ret == false)
                                    break;
                            }
                        }



                        if (ret && odMkShousaiItemList != null && odMkItemList != null)
                        {
                            foreach (OdMkShousaiItem shousaiItem in odMkShousaiItemList)
                            {
                                var item = odMkItemList.Where(o => o.OdMkItemID == shousaiItem.OdMkItemID).First();
                                if (item == null)
                                    continue;

                                if (shousaiItem.Bikou != null && shousaiItem.Bikou.Length > 0)
                                    shousaiItem.Bikou += System.Environment.NewLine;

                                shousaiItem.Bikou += item.ItemName;

                                ret = shousaiItem.UpdateRecord(loginUser);
                                if (ret == false)
                                    break;
                            }
                        }
                        if (ret && odMkItemList != null)
                        {
                            foreach (OdMkItem item in odMkItemList)
                            {
                                item.ItemName = "共用";

                                ret = item.UpdateRecord(loginUser);
                                if (ret == false)
                                    break;
                            }
                        }


                        if (ret && odJryShousaiItemList != null && odJryItemList != null)
                        {
                            foreach (OdJryShousaiItem shousaiItem in odJryShousaiItemList)
                            {
                                var item = odJryItemList.Where(o => o.OdJryItemID == shousaiItem.OdJryItemID).First();
                                if (item == null)
                                    continue;

                                if (shousaiItem.Bikou != null && shousaiItem.Bikou.Length > 0)
                                    shousaiItem.Bikou += System.Environment.NewLine;

                                shousaiItem.Bikou += item.ItemName;

                                ret = shousaiItem.UpdateRecord(loginUser);
                                if (ret == false)
                                    break;
                            }
                        }
                        if (ret && odJryItemList != null)
                        {
                            foreach (OdJryItem item in odJryItemList)
                            {
                                item.ItemName = "共用";

                                ret = item.UpdateRecord(loginUser);
                                if (ret == false)
                                    break;
                            }
                        }


                        if (ret && odShrShousaiItemList != null && odShrItemList != null)
                        {
                            foreach (OdShrShousaiItem shousaiItem in odShrShousaiItemList)
                            {
                                var item = odShrItemList.Where(o => o.OdShrItemID == shousaiItem.OdShrItemID).First();
                                if (item == null)
                                    continue;

                                if (shousaiItem.Bikou != null && shousaiItem.Bikou.Length > 0)
                                    shousaiItem.Bikou += System.Environment.NewLine;

                                shousaiItem.Bikou += item.ItemName;

                                ret = shousaiItem.UpdateRecord(loginUser);
                                if (ret == false)
                                    break;
                            }
                        }
                        if (ret && odShrItemList != null)
                        {
                            foreach (OdShrItem item in odShrItemList)
                            {
                                item.ItemName = "共用";

                                ret = item.UpdateRecord(loginUser);
                                if (ret == false)
                                    break;
                            }
                        }


                        ret = odThi.UpdateRecord(loginUser);

                        if (ret)
                            dbConnect.Commit();
                        else
                            dbConnect.RollBack();
                    }
                    catch (Exception e)
                    {
                        ret = false;
                        dbConnect.RollBack();
                    }
                }
            }
            else
            {
                ret = odThi.UpdateRecord(loginUser);

            }

            return ret;
        }
    }
}
