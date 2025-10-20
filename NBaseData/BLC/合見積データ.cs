using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseData.BLC
{
    public class 合見積データ
    {
        public List<TreeNode_Header> MakeTreeNodeHeaders(List<OdMmItem> odMmItems, List<List<OdMkItem>> odMkItemsList)
        {
            List<TreeNode_Header> ret = new List<TreeNode_Header>();
            if (odMmItems == null || odMmItems.Count == 0)
                return ret;

            TreeNode_Header th = null;
            TreeNode_Item ti = null;
            Dictionary<string, TreeNode_Header> headerDic = new Dictionary<string, TreeNode_Header>();
            Dictionary<string, OdMmItem> itemIDDic = new Dictionary<string, OdMmItem>();
            Dictionary<string, OdMmShousaiItem> shousaiIDDic = new Dictionary<string, OdMmShousaiItem>();

            Dictionary<string, OdMkItem> checkedItemIDDic = new Dictionary<string, OdMkItem>();

            // 見積依頼時の品目を表示対象リストに追加する
            foreach (OdMmItem odMmItem in odMmItems)
            {
                if (headerDic.ContainsKey(odMmItem.Header))
                {
                    th = headerDic[odMmItem.Header];
                }
                else
                {
                    th = new TreeNode_Header();
                    th.Header = odMmItem.Header;
                    th.Items = new List<TreeNode_Item>();
                    ret.Add(th);

                    headerDic.Add(th.Header, th);
                }
                ti = new TreeNode_Item();
                string itemName = "";
                if (odMmItem.MsItemSbtID != null && odMmItem.MsItemSbtID.Length > 0)
                {
                    itemName += odMmItem.MsItemSbtName + "：";
                }
                itemName += odMmItem.ItemName;
                ti.ItemName = itemName;
                ti.OdMmItemID = odMmItem.OdMmItemID;
                ti.ShousaiItems = new List<TreeNode_ShousaiItem>();
                th.Items.Add(ti);

                itemIDDic.Add(odMmItem.OdMmItemID, odMmItem);

                // 見積依頼時の詳細品目を表示対象リストに追加する
                foreach (OdMmShousaiItem shousaiItem in odMmItem.OdMmShousaiItems)
                {
                    TreeNode_ShousaiItem ts = new TreeNode_ShousaiItem();
                    ts.OdMmShousaiItemID = shousaiItem.OdMmShousaiItemID;
                    ts.ShousaiItemName = shousaiItem.ShousaiItemName;
                    ts.Count = shousaiItem.Count.ToString();
                    ts.TaniName = shousaiItem.MsTaniName;

                    ti.ShousaiItems.Add(ts);
                    shousaiIDDic.Add(shousaiItem.OdMmShousaiItemID, shousaiItem);
                }
            }

            // 見積依頼時になかった詳細品目を表示対象リストに追加する
            // （見積回答で追加された詳細品目）
            foreach (List<OdMkItem> odMkItems in odMkItemsList)
            {
                foreach (OdMkItem odMkItem in odMkItems)
                {
                    //if (checkedItemIDDic.ContainsKey(odMkItem.OdMkItemID))
                    //{
                    //    continue;
                    //}

                    if (headerDic.ContainsKey(odMkItem.Header))
                    {
                        th = headerDic[odMkItem.Header];
                    }
                    else
                    {
                        th = new TreeNode_Header();
                        th.Header = odMkItem.Header;
                        th.Items = new List<TreeNode_Item>();
                        ret.Add(th);

                        headerDic.Add(th.Header, th);
                    }

                    bool hitItem = false;
                    if (itemIDDic.ContainsKey(odMkItem.OdMmItemID))
                    {
                        hitItem = false;
                        OdMmItem hitMmItem = itemIDDic[odMkItem.OdMmItemID];
                        if (hitMmItem.MsItemSbtID == odMkItem.MsItemSbtID && hitMmItem.ItemName == odMkItem.ItemName)
                        {
                            // 同じ、OD_MM_ITEM_ID でも品目名が違う場合、（事務所の見積回答画面で編集が可能）
                            // 合見積画面で、同じ品目としては、扱わない
                            hitItem = true;

                            string itemName = "";
                            if (odMkItem.MsItemSbtID != null && odMkItem.MsItemSbtID.Length > 0)
                            {
                                itemName += odMkItem.MsItemSbtName + "：";
                            }
                            itemName += odMkItem.ItemName;

                            foreach (TreeNode_Item c_ti in th.Items)
                            {
                                if (c_ti.ItemName == itemName && c_ti.OdMmItemID == hitMmItem.OdMmItemID)
                                {
                                    ti = c_ti;
                                    break;
                                }
                            }

                            bool hitShousaiItem = false;
                            foreach (OdMkShousaiItem mkShousaiItem in odMkItem.OdMkShousaiItems)
                            {
                                hitShousaiItem = false;
                                if (shousaiIDDic.ContainsKey(mkShousaiItem.OdMmShousaiItemID))
                                {
                                    // 同じ、OD_MM_SHOUSAI_ITEM_ID でも品目名が違う場合、（事務所の見積回答画面で編集が可能）
                                    // 合見積画面で、同じ品目としては、扱わない
                                    OdMmShousaiItem hitMmShousaiItem = shousaiIDDic[mkShousaiItem.OdMmShousaiItemID];
                                    if (hitMmShousaiItem.ShousaiItemName == mkShousaiItem.ShousaiItemName)
                                    {
                                        hitShousaiItem = true;
                                    }
                                }
                                if (hitShousaiItem == false)
                                {
                                    TreeNode_ShousaiItem ts = new TreeNode_ShousaiItem();
                                    ts.OdMkShousaiItemID = mkShousaiItem.OdMkShousaiItemID;
                                    ts.ShousaiItemName = mkShousaiItem.ShousaiItemName;
                                    ts.Count = "--";
                                    ts.TaniName = mkShousaiItem.MsTaniName;

                                    ti.ShousaiItems.Add(ts);
                                }
                            }

                            checkedItemIDDic.Add(odMkItem.OdMkItemID, odMkItem);
                        }
                    }
                    if (hitItem == false)
                    {
                        // 見積回答時に追加されている品目／詳細品目を表示対象リストに追加する
                        TreeNode_Item c_ti = new TreeNode_Item();
                        //c_ti.ItemName = odMkItem.ItemName;
                        string itemName = "";
                        if (odMkItem.MsItemSbtID != null && odMkItem.MsItemSbtID.Length > 0)
                        {
                            itemName += odMkItem.MsItemSbtName + "：";
                        }
                        itemName += odMkItem.ItemName;
                        c_ti.ItemName = itemName;
                        c_ti.OdMkItemID = odMkItem.OdMkItemID;
                        c_ti.ShousaiItems = new List<TreeNode_ShousaiItem>();
                        th.Items.Add(c_ti);

                        foreach (OdMkShousaiItem mkShousaiItem in odMkItem.OdMkShousaiItems)
                        {
                            TreeNode_ShousaiItem c_ts = new TreeNode_ShousaiItem();
                            c_ts.OdMkShousaiItemID = mkShousaiItem.OdMkShousaiItemID;
                            c_ts.ShousaiItemName = mkShousaiItem.ShousaiItemName;
                            c_ts.Count = "--";
                            c_ts.TaniName = mkShousaiItem.MsTaniName;

                            c_ti.ShousaiItems.Add(c_ts);
                        }

                        checkedItemIDDic.Add(odMkItem.OdMkItemID, odMkItem);
                    }
                }
            }

            // 見積依頼時にないヘッダを見積回答時に追加している場合、こちら。
            foreach (List<OdMkItem> odMkItems in odMkItemsList)
            {
                th = null;
                foreach (OdMkItem odMkItem in odMkItems)
                {
                    if (checkedItemIDDic.ContainsKey(odMkItem.OdMkItemID))
                    {
                        continue;
                    }
                    if (th == null || th.Header != odMkItem.Header)
                    {
                        th = new TreeNode_Header();
                        th.Header = odMkItem.Header;
                        th.Items = new List<TreeNode_Item>();
                        ret.Add(th);
                    }
                    ti = new TreeNode_Item();
                    //ti.ItemName = odMkItem.ItemName;
                    string itemName = "";
                    if (odMkItem.MsItemSbtID != null && odMkItem.MsItemSbtID.Length > 0)
                    {
                        itemName += odMkItem.MsItemSbtName + "：";
                    }
                    itemName += odMkItem.ItemName;
                    ti.ItemName = itemName;
                    ti.OdMkItemID = odMkItem.OdMkItemID;
                    ti.ShousaiItems = new List<TreeNode_ShousaiItem>();
                    th.Items.Add(ti);

                    foreach (OdMkShousaiItem mkShousaiItem in odMkItem.OdMkShousaiItems)
                    {
                        TreeNode_ShousaiItem ts = new TreeNode_ShousaiItem();
                        ts.OdMkShousaiItemID = mkShousaiItem.OdMkShousaiItemID;
                        ts.ShousaiItemName = mkShousaiItem.ShousaiItemName;
                        ts.Count = "--";
                        ts.TaniName = mkShousaiItem.MsTaniName;

                        ti.ShousaiItems.Add(ts);
                    }
                }
            }
            return ret;
        }
        public OdMkShousaiItem GetOdMkShousaiItem(List<OdMkItem> OdMkItems, TreeNode_Item TnItem, TreeNode_ShousaiItem TnShousaiItem)
        {
            OdMkShousaiItem ret = null;
            foreach (OdMkItem mkItem in OdMkItems)
            {
                string itemName = "";
                if (mkItem.MsItemSbtID != null && mkItem.MsItemSbtID.Length > 0)
                {
                    itemName += mkItem.MsItemSbtName + "：";
                }
                itemName += mkItem.ItemName;
                if (TnItem.ItemName == itemName && (TnItem.OdMmItemID == mkItem.OdMmItemID || TnItem.OdMkItemID == mkItem.OdMkItemID))
                {
                    foreach (OdMkShousaiItem mkShousaiItem in mkItem.OdMkShousaiItems)
                    {
                        if (TnShousaiItem.OdMmShousaiItemID != null)
                        {
                            if (mkShousaiItem.OdMmShousaiItemID == TnShousaiItem.OdMmShousaiItemID)
                            {
                                ret = mkShousaiItem;
                                break;
                            }
                        }
                        else
                        {
                            if (mkShousaiItem.OdMkShousaiItemID == TnShousaiItem.OdMkShousaiItemID)
                            {
                                ret = mkShousaiItem;
                                break;
                            }
                        }
                    }
                }
            }
            return ret;
        }
        public class TreeNode_ShousaiItem
        {
            public string OdMmShousaiItemID;
            public string OdMkShousaiItemID;
            public string ShousaiItemName;
            public string Count;
            public string TaniName;
        }
        public class TreeNode_Item
        {
            public string OdMmItemID;
            public string OdMkItemID;
            public string ItemName;

            public List<TreeNode_ShousaiItem> ShousaiItems;
        }
        public class TreeNode_Header
        {
            public string Header;
            public List<TreeNode_Item> Items;
        }
    }
}
