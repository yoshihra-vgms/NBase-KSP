using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ORMapping;

namespace NBaseData.BLC
{
    public class 支払合算処理
    {

        private static string 新規ID()
        {
            return System.Guid.NewGuid().ToString();
        }

        
        #region public static bool 合算作成(MsUser loginUser, OdShrGassanHead odShrGassanHead, List<合算対象の受領> jrys)
        public static bool 合算作成(MsUser loginUser, OdShrGassanHead odShrGassanHead, List<合算対象の受領> jrys)
        {           
            bool ret = true;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                ret = odShrGassanHead.InsertRecord(dbConnect, loginUser);
                if ( ret == true )
                {
                    foreach (合算対象の受領 jry in jrys)
                    {
                        OdShrGassanItem gassanItem = new OdShrGassanItem();

                        gassanItem.OdShrGassanItemID = 新規ID();
                        gassanItem.OdShrGassanHeadID = odShrGassanHead.OdShrGassanHeadID;
                        gassanItem.OdJryID = jry.OdJryID;

                        gassanItem.RenewDate = odShrGassanHead.RenewDate;
                        gassanItem.RenewUserID = odShrGassanHead.RenewUserID;

                        ret = gassanItem.InsertRecord(dbConnect, loginUser);
                        if (ret == false)
                        {
                            break;
                        }
                    }
                }
                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }
            }
            return ret;
        }
        #endregion

        #region public static bool 合算解除(MsUser loginUser, OdShrGassanHead odShrGassanHead)
        public static bool 合算解除(MsUser loginUser, OdShrGassanHead odShrGassanHead)
        {
            bool ret = true;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                ret = OdShrGassanItem.DeleteRecordByOdShrGassanHeadID(dbConnect, loginUser, odShrGassanHead.OdShrGassanHeadID);
                
                if (ret == true)
                {
                    ret = odShrGassanHead.UpdateRecord(dbConnect, loginUser);
                }
                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }
            }
            return ret;
        }
        #endregion

        #region public static bool 支払依頼作成(MsUser loginUser, OdShr odShr, OdShrGassanHead odShrGassanHead, List<OdShrGassanItem> odShrGassanItems)
        public static bool 支払依頼作成(MsUser loginUser, OdShr odShr, OdShrGassanHead odShrGassanHead, List<OdShrGassanItem> odShrGassanItems)
        {
            bool ret = true;

            DateTime RenewDate = DateTime.Now;
            string RenewUserID = loginUser.MsUserID;
            OdJry 代表受領 = OdJry.GetRecord(loginUser, odShrGassanHead.OdJryID);
            OdMk 見積回答 = OdMk.GetRecord(loginUser, 代表受領.OdMkID);
            List<OdJryItem> jryItems = new List<OdJryItem>();
            List<OdShrItem> shrItems = new List<OdShrItem>();
            List<OdShrShousaiItem> shrShousaiItems = new List<OdShrShousaiItem>();

            odShr.ShrNo = 代表受領.JryNo + OdShr.Prefix支払;
            odShr.OdJryID = 代表受領.OdJryID;
            odShr.Naiyou = 見積回答.OdThiNaiyou;
            odShr.Bikou = 見積回答.OdThiBikou;
            odShr.KamokuNo = 代表受領.KamokuNo;
            odShr.UtiwakeKamokuNo = 代表受領.UtiwakeKamokuNo;
            odShr.VesselID = 代表受領.VesselID;
            odShr.RenewDate = RenewDate;
            odShr.RenewUserID = RenewUserID;
            odShr.SyoriStatus = "--";

            foreach (OdShrGassanItem gassanItem in odShrGassanItems)
            {
                OdJry jry = OdJry.GetRecord(loginUser, gassanItem.OdJryID);

                odShr.Amount       += jry.Amount;
                odShr.NebikiAmount += jry.NebikiAmount;
                odShr.Tax          += jry.Tax;

                odShr.Carriage     += jry.Carriage;

                jryItems.AddRange(GetJryItem(loginUser, gassanItem.OdJryID));
            }
            int itemShowOrder = 0;
            int shousaiItemShowOrder = 0;
            foreach (OdJryItem jryItem in jryItems)
            {
                OdShrItem shrItem = new OdShrItem();
                shrItem.OdShrItemID = 新規ID();
                shrItem.OdShrID = odShr.OdShrID;
                shrItem.Header = jryItem.Header;
                shrItem.MsItemSbtID = jryItem.MsItemSbtID;
                shrItem.ItemName = jryItem.ItemName;
                shrItem.Bikou = jryItem.Bikou;
                shrItem.VesselID = odShr.VesselID;
                shrItem.RenewDate = RenewDate;
                shrItem.RenewUserID = RenewUserID;
                shrItem.ShowOrder = ++itemShowOrder;
                // 2011.05 Add
                shrItem.OdJryItemID = jryItem.OdJryItemID;

                shrItems.Add(shrItem);

                foreach (OdJryShousaiItem jryShousaiItem in jryItem.OdJryShousaiItems)
                {
                    OdShrShousaiItem shrShousaiItem = new OdShrShousaiItem();
                    shrShousaiItem.OdShrShousaiItemID = 新規ID();
                    shrShousaiItem.OdShrItemID = shrItem.OdShrItemID;
                    shrShousaiItem.ShousaiItemName = jryShousaiItem.ShousaiItemName;
                    shrShousaiItem.MsVesselItemID = jryShousaiItem.MsVesselItemID;
                    shrShousaiItem.MsLoID = jryShousaiItem.MsLoID;
                    shrShousaiItem.Count = jryShousaiItem.JryCount;
                    shrShousaiItem.MsTaniID = jryShousaiItem.MsTaniID;
                    shrShousaiItem.Tanka = jryShousaiItem.Tanka;
                    shrShousaiItem.Bikou = jryShousaiItem.Bikou;
                    shrShousaiItem.VesselID = odShr.VesselID;
                    shrShousaiItem.RenewDate = RenewDate;
                    shrShousaiItem.RenewUserID = RenewUserID;
                    shrShousaiItem.ShowOrder = ++shousaiItemShowOrder;
                    // 2011.05 Add
                    shrShousaiItem.OdJryShousaiItemID = jryShousaiItem.OdJryShousaiItemID;

                    shrShousaiItems.Add(shrShousaiItem);
                }
            }

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                ret = 支払登録(dbConnect, loginUser, odShr, shrItems, shrShousaiItems);
                if (ret)
                {
                    odShrGassanHead.OdShrID = odShr.OdShrID;
                    odShrGassanHead.Status = (int)OdShrGassanHead.StatusEnum.支払作成済;
                    ret = odShrGassanHead.UpdateRecord(dbConnect, loginUser);
                    if (ret)
                    {
                        bool alarmRet = false;
                        foreach (OdShrGassanItem gassanItem in odShrGassanItems)
                        {
                            alarmRet = NBaseData.BLC.発注アラーム処理.支払作成アラーム停止(dbConnect, loginUser, gassanItem.OdJryID);
                        }
                        alarmRet = NBaseData.BLC.発注アラーム処理.支払依頼アラーム登録(dbConnect, loginUser, odShr);
                    }
                }
                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }
            }
            return ret;
        }
        #endregion

        #region private static bool 支払登録(....)
        private static bool 支払登録(
            DBConnect dbConnect,
            MsUser loginUser,
            OdShr odShr,
            List<OdShrItem> odShrItems,
            List<OdShrShousaiItem> odShrShousaiItems
           )
        {
            bool ret = false;
            odShr.ShrNo = 支払SetMaxNo(dbConnect, loginUser, odShr.ShrNo);
            ret = odShr.InsertRecord(dbConnect, loginUser);
            if (ret)
            {
                foreach (OdShrItem shrItem in odShrItems)
                {
                    ret = shrItem.InsertRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        break;
                    }
                }
                if (ret)
                {
                    foreach (OdShrShousaiItem shrShousaiItem in odShrShousaiItems)
                    {
                        ret = shrShousaiItem.InsertRecord(dbConnect, loginUser);
                        if (ret == false)
                        {
                            break;
                        }
                    }
                }
            }
            return ret;
        }
        #endregion

        private static List<OdJryItem> GetJryItem(MsUser loginUser, string odJryId)
        {
            List<OdJryItem> retJryItems = new List<OdJryItem>();

            List<OdJryItem> jryItems = OdJryItem.GetRecordsByOdJryID(loginUser, odJryId);
            List<OdJryShousaiItem> jryShousaiItems = OdJryShousaiItem.GetRecordsByOdJryID(loginUser, odJryId);
            foreach (OdJryItem jryItem in jryItems)
            {
                foreach (OdJryShousaiItem jryShousaiItem in jryShousaiItems)
                {
                    if (jryShousaiItem.OdJryItemID == jryItem.OdJryItemID)
                    {
                        jryItem.OdJryShousaiItems.Add(jryShousaiItem);
                    }
                }
                foreach (OdJryShousaiItem jryShousaiItem in jryItem.OdJryShousaiItems)
                {
                    jryShousaiItems.Remove(jryShousaiItem);
                }
                retJryItems.Add(jryItem);
            }
            return retJryItems;
        }

        private static string 支払SetMaxNo(DBConnect dbConnect, MsUser loginUser, string ShrNo)
        {
            GetMaxNo maxNo = GetMaxNo.GetMaxShrNo(dbConnect, loginUser, OdShr.NoLength支払 - 1, ShrNo);
            string currentMaxNo = maxNo.MaxNo.Replace(ShrNo, "");
            int currentMaxNoDec = NBaseData.DS.NoConvert.NToInt(currentMaxNo, 36);
            string nextMaxNo = NBaseData.DS.NoConvert.IntToN(currentMaxNoDec + 1, 36);
            return ShrNo += nextMaxNo;
        }
    }
}

