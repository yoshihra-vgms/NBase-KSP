using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ORMapping;

namespace NBaseData.BLC
{
    public class 船用品
    {
        public static List<OdThiItem> BLC_船用品品目(MsUser loginUser, string odThiId)
        {
            List<OdThiItem> result = new List<OdThiItem>();

            List<OdThiItem> tehaiItems = OdThiItem.GetRecordsByOdThiID(loginUser, odThiId);
            List<OdThiShousaiItem> tehaiShousaiItems = OdThiShousaiItem.GetRecordsByOdThiID(loginUser, odThiId);

            foreach (OdThiItem ti in tehaiItems)
            {
                foreach (OdThiShousaiItem si in tehaiShousaiItems)
                {
                    if (si.OdThiItemID == ti.OdThiItemID)
                    {
                        ti.OdThiShousaiItems.Add(si);
                    }
                }
                foreach (OdThiShousaiItem si in ti.OdThiShousaiItems)
                {
                    tehaiShousaiItems.Remove(si);
                }
            }
            result.AddRange(tehaiItems);
            return result;
        }


        public static bool BLC_船用品登録(MsUser loginUser, MsVesselItemVessel msVesselItemVessel)
        {
            bool ret = true;

            MsVesselItem vesselItem = MsVesselItem.GetRecord(loginUser, msVesselItemVessel.MsVesselItemID);
            if (vesselItem == null)
            {
                return false;
            }

            int kind = -1;
            if (vesselItem.CategoryNumber == (int)MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント)
            {
                kind = (int)OdChozo.ShubetsuEnum.船用品;
            }
            //else if (vesselItem.CategoryNumber == (int)MsVesselItemCategory.MsVesselItemCategoryEnum.機関部特定品)
            //{
            //    kind = (int)OdChozo.ShubetsuEnum.特定品;
            //}
            //else if (vesselItem.CategoryNumber == (int)MsVesselItemCategory.MsVesselItemCategoryEnum.甲板部特定品)
            //{
            //    kind = (int)OdChozo.ShubetsuEnum.特定品;
            //}


            // 最終月次確定
            OdGetsujiShime shime = OdGetsujiShime.GetRecordByLastDate(loginUser);           
            string ym = null;
            if (shime != null)
            {
                // 最終月次確定の翌月
                ym = DateTime.Parse(shime.NenGetsu.Substring(0,4) + "/" + shime.NenGetsu.Substring(4,2) + "/01").AddMonths(1).ToString("yyyyMM");
            }
            int vesselId = msVesselItemVessel.MsVesselID;
            MsVesselItemVessel orgVesselItemVessel = null;
            OdChozo odChozo1 = null;
            OdChozo odChozo2 = null;
            OdChozoShousai odChozoShousai1 = null;
            OdChozoShousai odChozoShousai2 = null;

            try
            {
                orgVesselItemVessel = MsVesselItemVessel.GetRecord(msVesselItemVessel.MsVesselItemVesselID, loginUser);
                //odChozo1 = OdChozo.GetRecord_Date_VesselID_Kind(loginUser, vesselId, shime.NenGetsu, (int)OdChozo.ShubetsuEnum.特定品);
                odChozo1 = OdChozo.GetRecord_Date_VesselID_Kind(loginUser, vesselId, shime.NenGetsu, kind);
                if (odChozo1 != null)
                {     
                    odChozoShousai1 = OdChozoShousai.GetRecordsByVesselID_Date_VesselItemID(loginUser, vesselId, shime.NenGetsu, msVesselItemVessel.MsVesselItemID);
                }     
                //odChozo2 = OdChozo.GetRecord_Date_VesselID_Kind(loginUser, vesselId, ym, (int)OdChozo.ShubetsuEnum.特定品);
                odChozo2 = OdChozo.GetRecord_Date_VesselID_Kind(loginUser, vesselId, ym, kind);
                if (odChozo2 != null)
                {
                    odChozoShousai2 = OdChozoShousai.GetRecordsByVesselID_Date_VesselItemID(loginUser, vesselId, ym, msVesselItemVessel.MsVesselItemID);
                }
            }
            catch
            {
            }

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    // 船用品船情報の処理
                    if (orgVesselItemVessel == null)
                    {
                        ret = msVesselItemVessel.InsertRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        ret = msVesselItemVessel.UpdateRecord(dbConnect, loginUser);
                    }

                    if (shime != null)
                    {
                        if (ret && odChozo1 == null)
                        {
                            //odChozo1 = 貯蔵品処理.CreateOdChozo(loginUser, vesselId, shime.NenGetsu, (int)OdChozo.ShubetsuEnum.特定品);
                            odChozo1 = 貯蔵品処理.CreateOdChozo(loginUser, vesselId, shime.NenGetsu, kind);
                            ret = odChozo1.InsertRecord(dbConnect, loginUser);

                        }
                        if (ret && odChozoShousai1 == null)
                        {
                            odChozoShousai1 = 貯蔵品処理.CreateOdChozoShousai(loginUser, vesselId, odChozo1.OdChozoID, null, msVesselItemVessel.MsVesselItemID);

                            //
                            if (kind == (int)OdChozo.ShubetsuEnum.船用品)
                                odChozoShousai1.UkeireNengetsu = shime.NenGetsu;

                            ret = odChozoShousai1.InsertRecord(dbConnect, loginUser);
                        }
                        if (ret && odChozo2 == null)
                        {
                            //odChozo2 = 貯蔵品処理.CreateOdChozo(loginUser, vesselId, ym, (int)OdChozo.ShubetsuEnum.特定品);
                            odChozo2 = 貯蔵品処理.CreateOdChozo(loginUser, vesselId, ym, kind);
                            ret = odChozo2.InsertRecord(dbConnect, loginUser);

                        }
                        if (ret && odChozoShousai2 == null)
                        {
                            odChozoShousai2 = 貯蔵品処理.CreateOdChozoShousai(loginUser, vesselId, odChozo2.OdChozoID, null, msVesselItemVessel.MsVesselItemID);

                            //
                            if (kind == (int)OdChozo.ShubetsuEnum.船用品)
                                odChozoShousai2.UkeireNengetsu = ym;

                            ret = odChozoShousai2.InsertRecord(dbConnect, loginUser);
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
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                }
            }
            return ret;
        }


        public static List<OdChozoShousai> BLC_Get特定船用品在庫(MsUser loginUser, int msVesselId)
        {
            List<OdChozoShousai> result = new List<OdChozoShousai>();

            // 最大２ヶ月前までさかのぼり在庫データを取得
            DateTime date = DateTime.Today.AddMonths(-1);
            for (int i = 0; i > -2; i--)
            {
                date = date.AddMonths(i);
                string strDate = date.ToString("yyyyMM");
                result = OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(loginUser, msVesselId, strDate, (int)OdChozo.ShubetsuEnum.特定品);
                if (result != null)
                {
                    break;
                }
            }
            if (result == null)
            {
                return result;
            }

            // 在庫レコード以後の受領データを取得する
            List<貯蔵品リスト> list = new List<貯蔵品リスト>();
            for (DateTime d = date.AddMonths(1); date < DateTime.Today; date = date.AddMonths(1))
            {
                list.AddRange(貯蔵品リスト.GetRecords特定品(loginUser, d.Year, d.Month, msVesselId));
            }

            foreach (OdChozoShousai shousai in result)
            {
                var tmp = list.Where(obj => obj.ID == shousai.MsVesselItemID);

                if (tmp.Count() > 0)
                {
                    shousai.Count += tmp.Sum(obj => obj.受領数);
                }
            }

            return result;
        }
    }
}
