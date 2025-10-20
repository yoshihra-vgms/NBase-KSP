using NBaseData.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NBaseCommon.Hachu.Excel
{
    public class 発注帳票Common
    {
        public enum kubun発注帳票 { 査定表, 請求書, 注文書, 見積依頼書 };

        public string VesselName;
        public string CustomerName;
        public string Tanto;
        public string Honorific;
        public DateTime HachuDay;

        public List<Item> Items = null;
        public List<ShousaiItem> ShousaiItems = null;

        public Dictionary<string, List<Item>> dic_Items = new Dictionary<string, List<Item>>();
        public Dictionary<string, Dictionary<string, List<ShousaiItem>>> dic_ShousaiItems = new Dictionary<string, Dictionary<string, List<ShousaiItem>>>();

        public class Item
        {
            public string ItemSbtID { set; get; }
            public string Header { set; get; }
            public string ItemName { set; get; }
            public string ItemID { set; get; }
        }


        public class ShousaiItem
        {
            public string ItemID { set; get; }
            public string ShousaiItemName { set; get; }
            public string TaniName { set; get; }
            public int Count { set; get; }
            public int Zaiko { set; get; }
            public int Satei { set; get; }
            public string Bikou { set; get; }
        }




        protected void _BuildData(MsUser loginUser, 発注帳票Common.kubun発注帳票 kubun, string Id)
        {

            if (kubun == 発注帳票Common.kubun発注帳票.注文書 || kubun == kubun発注帳票.見積依頼書)
            {
                OdMk odMk = OdMk.GetRecord(loginUser, Id);
                OdThi odThi = OdThi.GetRecordByOdMkID(loginUser, Id);
                MsCustomer msCustomer = MsCustomer.GetRecord(loginUser, odMk.MsCustomerID);

                _ConvertMk(loginUser, Id);

                VesselName = odThi.VesselName;
                CustomerName = msCustomer.CustomerName;
                Tanto = odMk.Tantousha;
                Honorific = "様";

                if (kubun == 発注帳票Common.kubun発注帳票.注文書)
                    HachuDay = odMk.HachuDate;
                else // kubun == kubun発注帳票.見積依頼書
                    HachuDay = odMk.MmDate;
            }
            else
            {
                OdThi odThi = OdThi.GetRecord(loginUser, Id);

                _ConvertThi(loginUser, Id);

                VesselName = odThi.VesselName;
                CustomerName = null;
                Tanto = "工務監督";
                Honorific = "殿";

                HachuDay = odThi.ThiIraiDate;
            }


            // 区分ごとに分ける
            var itemSbtIds = Items.Select(o => o.ItemSbtID).Distinct().OrderBy(o => o);
            foreach (string itemSbtId in itemSbtIds)
            {
                dic_Items.Add(itemSbtId, Items.Where(o => o.ItemSbtID == itemSbtId).ToList());


                var itemNameList = dic_Items[itemSbtId].Select(o => o.ItemName).Distinct();
                var work_dic = new Dictionary<string, List<ShousaiItem>>();
                foreach (string itemName in itemNameList)
                {
                    var thiItemIdList = dic_Items[itemSbtId].Where(o => o.ItemName == itemName).Select(o => o.ItemID);

                    List<ShousaiItem> work = new List<ShousaiItem>();
                    foreach (string thiItemId in thiItemIdList)
                    {
                        work.AddRange(ShousaiItems.Where(o => o.ItemID == thiItemId));
                    }

                    work_dic.Add(itemName, work);
                }
                dic_ShousaiItems.Add(itemSbtId, work_dic);
            }
        }


        protected void _ConvertThi(MsUser loginUser, string thiId)
        {
            List<OdThiItem> odThiItemList = OdThiItem.GetRecordsByOdThiID(loginUser, thiId);
            List<OdThiShousaiItem> odThiShousaiItemList = OdThiShousaiItem.GetRecordsByOdThiID(loginUser, thiId);


            Items = new List<Item>();
            ShousaiItems = new List<ShousaiItem>();

            foreach (OdThiItem thiItem in odThiItemList)
            {
                var item = new Item();

                item.ItemID = thiItem.OdThiItemID;
                item.Header = thiItem.Header;
                item.ItemName = thiItem.ItemName;
                item.ItemSbtID = thiItem.MsItemSbtID;

                Items.Add(item);
            }

            foreach (OdThiShousaiItem thiShousaiItem in odThiShousaiItemList)
            {
                var shousaiItem = new ShousaiItem();

                shousaiItem.ItemID = thiShousaiItem.OdThiItemID;
                shousaiItem.ShousaiItemName = thiShousaiItem.ShousaiItemName;
                shousaiItem.TaniName = thiShousaiItem.MsTaniName;

                shousaiItem.Count = thiShousaiItem.Count;
                shousaiItem.Zaiko = thiShousaiItem.ZaikoCount;
                shousaiItem.Satei = thiShousaiItem.Sateisu;

                shousaiItem.Bikou = thiShousaiItem.Bikou;

                ShousaiItems.Add(shousaiItem);
            }
        }

        protected void _ConvertMk(MsUser loginUser, string mkId)
        {
            List<OdMkItem> odMkItemList = OdMkItem.GetRecordsByOdMkID(loginUser, mkId);
            List<OdMkShousaiItem> odMkShousaiItemList = OdMkShousaiItem.GetRecordsByOdMkID(loginUser, mkId);


            Items = new List<Item>();
            ShousaiItems = new List<ShousaiItem>();

            foreach (OdMkItem mkItem in odMkItemList)
            {
                var item = new Item();

                item.ItemID = mkItem.OdMkItemID;
                item.Header = mkItem.Header;
                item.ItemName = mkItem.ItemName;
                item.ItemSbtID = mkItem.MsItemSbtID;

                Items.Add(item);
            }

            foreach (OdMkShousaiItem mkShousaiItem in odMkShousaiItemList)
            {
                var shousaiItem = new ShousaiItem();

                shousaiItem.ItemID = mkShousaiItem.OdMkItemID;
                shousaiItem.ShousaiItemName = mkShousaiItem.ShousaiItemName;
                shousaiItem.TaniName = mkShousaiItem.MsTaniName;

                shousaiItem.Count = mkShousaiItem.Count;
                //shousaiItem.Zaiko;
                //shousaiItem.Satei;

                shousaiItem.Bikou = mkShousaiItem.Bikou;

                ShousaiItems.Add(shousaiItem);
            }
        }
    }
}
