using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using SqlMapper;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using System.Reflection;
using NBaseData.DAC;

namespace NBaseData.BLC
{
    [DataContract()]
    public class 貯蔵品編集RowData残量
    {
        [DataMember]
        public int 残量 = 0;
        [DataMember]
        public decimal 金額 = 0;

        public 貯蔵品編集RowData残量(int zan, decimal gaku)
        {
            残量 = zan;
            金額 = gaku;
        }
    }
    [DataContract()]
    public class 貯蔵品編集RowData
    {
        [DataMember]
        public string ID = "";
        [DataMember]
        public string 品名 = "";
        [DataMember]
        public string 単位 = "";
        [DataMember]
        public int 繰越 = 0;
        [DataMember]
        public decimal 繰越金額 = 0;
        [DataMember]
        public int 受入 = 0;
        [DataMember]
        public decimal 受入金額 = 0;
        [DataMember]
        public int 消費 = 0;
        [DataMember]
        public decimal 消費金額 = 0;
        [DataMember]
        public int 残量 = 0;
        [DataMember]
        public decimal 残量金額 = 0;

        // 2012.08: 金額計算は貯蔵品リストからするように変更
        //[DataMember]
        //public List<貯蔵品編集RowData残量> 残量s = new List<貯蔵品編集RowData残量>();     
        [DataMember]
        public List<NBaseData.BLC.貯蔵品リスト> リストs = new List<NBaseData.BLC.貯蔵品リスト>();

        // 関連データ
        [DataMember]
        public OdChozoShousai OdChozoShousaiData;

        // 次期改造
        [DataMember]
        public string カテゴリ = "";

        public int CategoryNumber = 0;
    }
    
    [DataContract()]
    public class 貯蔵品管理票
    {
        // 対象の予算（費目のID）
        public const int MS_HIMOKU_潤滑油費_ID = 41;
        public const int MS_HIMOKU_船用品費_ID = 40;

        [DataMember]
        public MsVessel msVessel;
        [DataMember]
        public List<貯蔵品編集RowData> datas = new List<貯蔵品編集RowData>();
        [DataMember]
        public decimal yosan;


        public static 貯蔵品管理票 Get貯蔵品管理票(MsUser loginUser, MsVessel msvessel, 貯蔵品リスト.対象Enum kind, int FromYear, int FromMonth, int ToYear, int ToMonth)
        {
            //return Get貯蔵品管理票(loginUser, msvessel, kind, FromYear, FromMonth, ToYear, ToMonth, true);

            if (kind == 貯蔵品リスト.対象Enum.特定品)
            {
                return Get貯蔵品管理票_特定品(loginUser, msvessel, kind, FromYear, FromMonth, ToYear, ToMonth);
            }
            else
            {
                return Get貯蔵品管理票(loginUser, msvessel, kind, FromYear, FromMonth, ToYear, ToMonth, false);
            }
        }

        public static 貯蔵品管理票 Get貯蔵品管理票_特定品(MsUser loginUser, MsVessel msvessel, 貯蔵品リスト.対象Enum kind, int FromYear, int FromMonth, int ToYear, int ToMonth)
        {
            貯蔵品管理票 chozoInfo = new 貯蔵品管理票();
            chozoInfo.msVessel = msvessel;


            // 指定年月（From）
            DateTime fromYm = new DateTime(FromYear, FromMonth, 1);
            // 指定年月（To）
            DateTime toYm = new DateTime(ToYear, ToMonth, 1);

            int thisYear = DateTime.Today.Year;
            int thisMonth = DateTime.Today.Month;
            if ((ToYear > thisYear) || (ToYear == thisYear && ToMonth > thisMonth))
            {
                if ((FromYear > thisYear) || (FromYear == ToYear && FromMonth > thisMonth))
                {
                    toYm = new DateTime(FromYear, FromMonth, 1);
                }
                else
                {
                    toYm = new DateTime(thisYear, thisMonth, 1);
                }
            }

            // 繰越データ
            // 指定年月（From）の1つ前の月
            DateTime prevYm = fromYm.AddMonths(-1);
            string prevYmStr = prevYm.Year.ToString() + prevYm.Month.ToString("00");
            List<OdChozoShousai> prevShousaiList = new List<OdChozoShousai>();   // 繰越(開始前月)データ            

            // 最終データ 
            string toYmStr = toYm.Year.ToString() + toYm.Month.ToString("00");
            List<OdChozoShousai> endShousaiList = new List<OdChozoShousai>();   // 最終データ            

            // フルデータ
            List<NBaseData.BLC.貯蔵品リスト> 対象貯蔵品List = null;


            //==================================
            // 検索
            //貯蔵品詳細データの取得
            List<OdChozoShousai> tmpShousaiList = new List<OdChozoShousai>();   // 範囲内のLOまたはペイントの全ての貯蔵詳細            
            tmpShousaiList = OdChozoShousai.GetRecordsByVesselID_Period_Shubetsu(loginUser, msvessel.MsVesselID, prevYmStr, toYmStr, (int)kind);
            List<OdChozoShousai> fullShousaiList = tmpShousaiList.OrderBy(obj => obj.MsLoID).OrderBy(obj => obj.MsVesselItemID).Distinct(new OdChozoShousaiCompare()).ToList<OdChozoShousai>();


            // 繰越計算用の貯蔵品詳細データの取得
            prevShousaiList = OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(loginUser, msvessel.MsVesselID, prevYmStr, (int)kind);

            // 最終月計算用の貯蔵品詳細データの取得
            endShousaiList = OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(loginUser, msvessel.MsVesselID, toYmStr, (int)kind);

            // フルデータの取得
            対象貯蔵品List = 貯蔵品編集_取得(loginUser, fromYm.Year, fromYm.Month, toYm.Year, toYm.Month, msvessel.MsVesselID, kind);

            // 次期改造
            List<MsVesselItemCategory> categoryList = MsVesselItemCategory.GetRecords(loginUser);
            List<MsVesselItem> vesselItemList = MsVesselItem.GetRecords(loginUser);

            //==================================
            // 
            foreach (OdChozoShousai shousai in endShousaiList)
            {
                //==================================
                // 繰越分
                var 繰越 = prevShousaiList.Where(obj => obj.MsVesselItemID == shousai.MsVesselItemID);

                //==================================
                // 受入
                var 受入 = 対象貯蔵品List.Where(obj => obj.ID == shousai.MsVesselItemID);

                // データの無いものは、無視する
                if (繰越.Count() == 0 && 受入.Count() == 0)
                {
                    continue;
                }

                貯蔵品編集RowData data = new 貯蔵品編集RowData();
                data.ID = shousai.MsVesselItemID;
                data.品名 = shousai.VesselItemName;
                data.単位 = shousai.TaniName;

                // 次期改造
                if (vesselItemList.Any(obj => obj.MsVesselItemID == shousai.MsVesselItemID))
                {
                    MsVesselItem vi = vesselItemList.Where(obj => obj.MsVesselItemID == shousai.MsVesselItemID).First();
                    if (categoryList.Any(obj => obj.CategoryNumber == vi.CategoryNumber))
                    {
                        data.カテゴリ = categoryList.Where(obj => obj.CategoryNumber == vi.CategoryNumber).First().CategoryName;
                        data.CategoryNumber = vi.CategoryNumber;
                    }
                }

                data.繰越 = 繰越.Count() > 0 ? 繰越.Sum(obj => obj.Count) : 0;

                //==================================
                // 受入
                foreach (var tmp in 受入)
                {
                    if (tmp.支払数 > 0)
                    {
                        if (CheckDate(prevYmStr, tmp.受領年月))
                        {
                            data.受入 += tmp.支払数;
                        }
                    }
                    else
                    {
                        if (CheckDate(prevYmStr, tmp.受領年月))
                        {
                            data.受入 += tmp.受領数;
                        }
                    }
                }

                //==================================
                // 最終分
                data.OdChozoShousaiData = shousai;
                data.残量 = shousai.Count;

                data.消費 = data.繰越 + data.受入 - data.残量;


                // 繰越、受入、残のどれかがあれば、表示対象
                if (data.繰越 > 0 || data.受入 > 0 || data.残量 > 0)
                {
                    chozoInfo.datas.Add(data);
                }
            }

            // 2017.06.08
            chozoInfo.datas = chozoInfo.datas.OrderBy(obj => obj.CategoryNumber).ToList();

            return chozoInfo;
        }


        public static 貯蔵品管理票 Get貯蔵品管理票(MsUser loginUser, MsVessel msvessel, 貯蔵品リスト.対象Enum kind, int FromYear, int FromMonth, int ToYear, int ToMonth,
            bool get予算)
        {
            貯蔵品管理票 chozoInfo = new 貯蔵品管理票();
            chozoInfo.msVessel = msvessel;


            // 指定年月（From）
            DateTime fromYm = new DateTime(FromYear, FromMonth, 1);
            // 指定年月（To）
            DateTime toYm = new DateTime(ToYear, ToMonth, 1);
            
            // 2011.05.25: Add 13Lines
            // 年間や四半期等を指定した場合のToが未来の場合の対応。
            // Fromが未来の場合、ToはFromに置き換える。
            // From < Today < To の場合、 ToはTodayで置き換える。
            int thisYear = DateTime.Today.Year;
            int thisMonth = DateTime.Today.Month;
            //if ( ( ToYear > thisYear ) || ( ToMonth > thisMonth ) )
            if ((ToYear > thisYear) || (ToYear == thisYear && ToMonth > thisMonth))
            {
                if ((FromYear > thisYear) || (FromYear == ToYear && FromMonth > thisMonth))
                {
                    toYm = new DateTime(FromYear, FromMonth, 1);
                }
                else
                {
                    toYm = new DateTime(thisYear, thisMonth, 1);
                }
            }
            // 2011.05.25: Add End

            // 繰越データ
            // 指定年月（From）の1つ前の月
            DateTime prevYm = fromYm.AddMonths(-1);
            string prevYmStr = prevYm.Year.ToString() + prevYm.Month.ToString("00");
            List<OdChozoShousai> prevShousaiList = new List<OdChozoShousai>();   // 繰越(開始前月)データ            
            List<NBaseData.BLC.貯蔵品リスト> 繰越貯蔵品List = null;
            
            // 最終データ 
            string toYmStr = toYm.Year.ToString() + toYm.Month.ToString("00");
            List<OdChozoShousai> endShousaiList = new List<OdChozoShousai>();   // 最終データ            
            List<NBaseData.BLC.貯蔵品リスト> 最終貯蔵品List = null;
            
            // フルデータ
            List<NBaseData.BLC.貯蔵品リスト> 対象貯蔵品List = null;


            //==================================
            // 検索
            //貯蔵品詳細データの取得
            List<OdChozoShousai> tmpShousaiList = new List<OdChozoShousai>();   // 範囲内のLOまたはペイントの全ての貯蔵詳細            
            tmpShousaiList = OdChozoShousai.GetRecordsByVesselID_Period_Shubetsu(loginUser, msvessel.MsVesselID, prevYmStr, toYmStr, (int)kind);
            //List<OdChozoShousai> fullShousaiList = tmpShousaiList.Distinct(new OdChozoShousaiCompare()).ToList<OdChozoShousai>();
            List<OdChozoShousai> fullShousaiList = tmpShousaiList.OrderBy(obj => obj.MsLoID).OrderBy(obj => obj.MsVesselItemID).Distinct(new OdChozoShousaiCompare()).ToList<OdChozoShousai>();
                                                   


            // 繰越計算用の貯蔵品詳細データの取得
            prevShousaiList = OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(loginUser, msvessel.MsVesselID, prevYmStr, (int)kind);
            繰越貯蔵品List = 貯蔵品編集_取得(loginUser, prevYm.Year, prevYm.Month, prevYm.Year, prevYm.Month, msvessel.MsVesselID, kind);

            // 最終月計算用の貯蔵品詳細データの取得
            endShousaiList = OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(loginUser, msvessel.MsVesselID, toYmStr, (int)kind);
            最終貯蔵品List = 貯蔵品編集_最終取得(loginUser, fromYm.Year, fromYm.Month, toYm.Year, toYm.Month, msvessel.MsVesselID, kind);

            // フルデータの取得
            // 2012.08：対象は開始年月から終了年月なので、１つ前の月をのぞくように修正
            //対象貯蔵品List = 貯蔵品編集_取得(loginUser, prevYm.Year, prevYm.Month, toYm.Year, toYm.Month, msvessel.MsVesselID, kind);
            対象貯蔵品List = 貯蔵品編集_取得(loginUser, fromYm.Year, fromYm.Month, toYm.Year, toYm.Month, msvessel.MsVesselID, kind);

            // 予算
            chozoInfo.yosan = 0;
            if (get予算)
            {
                BgYosanHead bgYosanHead = BgYosanHead.GetRecordByYear(loginUser, FromYear.ToString());
                if (bgYosanHead != null)
                {
                    int himokuId = 0;
                    if (kind == 貯蔵品リスト.対象Enum.潤滑油)
                    {
                        himokuId = MS_HIMOKU_潤滑油費_ID;
                    }
                    else
                    {
                        himokuId = MS_HIMOKU_船用品費_ID;
                    }
                    for (DateTime d = fromYm; d <= toYm; d = d.AddMonths(1))
                    {
                        int work_y = d.Year;
                        int work_m = d.Month;
                        BgYosanItem bgYosanItem = BgYosanItem.GetRecordByYearHimokuIDMsVesselID(loginUser, bgYosanHead.YosanHeadID, work_y, work_m, himokuId, msvessel.MsVesselID);
                        if (bgYosanItem != null)
                        {
                            chozoInfo.yosan += bgYosanItem.Amount;
                        }
                    }
                }
            }

            //==================================
            // 
            //foreach (OdChozoShousai chozoShousai in prevShousaiList)
            //foreach (OdChozoShousai chozoShousai in endShousaiList)
            foreach (OdChozoShousai chozoShousai in fullShousaiList)
            {
                貯蔵品編集RowData data = new 貯蔵品編集RowData();

                if (kind == 貯蔵品リスト.対象Enum.潤滑油)
                {
                    data.ID = chozoShousai.MsLoID;
                    data.品名 = chozoShousai.LoName;
                }
                else  // 貯蔵品リスト.対象Enum.船用品
                {
                    data.ID = chozoShousai.MsVesselItemID;
                    data.品名 = chozoShousai.VesselItemName;
                }
                data.単位 = chozoShousai.TaniName;
                

                //==================================
                // 繰越分
                // 2010.05.06:aki 以下１行を、それ以降の７行に変更
                //data.繰越 = chozoShousai.Count;
                var 繰越分 = from p in prevShousaiList
                             where p.MsLoID == chozoShousai.MsLoID && p.MsVesselItemID == chozoShousai.MsVesselItemID
                             select p;
                foreach (var tmp in 繰越分)
                {
                    data.繰越 = tmp.Count;
                }

                var 繰越tmpList = from p in 繰越貯蔵品List
                                where p.ID == data.ID
                                select p;
                foreach (var tmp in 繰越tmpList)
                {
                    decimal 単価 = 0;
                    if (tmp.支払数 > 0)
                    {
                        単価 = tmp.支払単価;
                    }
                    else
                    {
                        単価 = tmp.受領単価;
                    }
                    data.繰越金額 += (int)(tmp.残量 * 単価);
                }


                //==================================
                // 受入
                var 受入tmpList = (from p in 対象貯蔵品List
                                where p.ID == data.ID
                                select p).Distinct();

                ArrayList check済みList = new ArrayList();
                foreach (var tmp in 受入tmpList)
                {
                    bool check済み = false;
                    // 2012.08：受入は発注番号ではなく、受領詳細品目IDまたは支払詳細品目IDが同じ場合は省く
                    foreach (NBaseData.BLC.貯蔵品リスト d in check済みList)
                    {
                        //if (tmp.発注番号 == d.発注番号)
                        if ((tmp.JS_ID != "" && d.JS_ID != "" && tmp.JS_ID == d.JS_ID) || (tmp.SS_ID != "" && d.SS_ID != "" && tmp.SS_ID == d.SS_ID))
                        {
                            check済み = true;
                        }
                    }
                    if (check済み == false)
                    {
                        decimal 単価 = 0;
                        if (tmp.支払数 > 0)
                        {
                            単価 = tmp.支払単価;
                            if (CheckDate(prevYmStr, tmp.受領年月))
                            {
                                data.受入 += tmp.支払数;
                                data.受入金額 += (int)(tmp.支払数 * 単価);

                                check済みList.Add(tmp);
                            }
                        }
                        else
                        {
                            単価 = tmp.受領単価;
                            if (CheckDate(prevYmStr, tmp.受領年月))
                            {
                                data.受入 += tmp.受領数;
                                data.受入金額 += (int)(tmp.受領数 * 単価);

                                check済みList.Add(tmp);
                            }
                        }
                    }
                }


                //==================================
                // 最終分
                var 最終tmpList = from p in 最終貯蔵品List
                                where p.ID == data.ID
                                select p;
                foreach (var tmp in 最終tmpList)
                {
                    decimal 単価 = 0;
                    if (tmp.支払数 > 0)
                    {
                        単価 = tmp.支払単価;
                    }
                    else
                    {
                        単価 = tmp.受領単価;
                    }
                    data.残量 += tmp.残量;
                    data.残量金額 += (int)(tmp.残量 * 単価);

                    // 2012.08: 金額計算は貯蔵品リストからするように変更
                    data.リストs.Add(tmp);
                }

                data.消費 = data.繰越 + data.受入 - data.残量;
                data.消費金額 = data.繰越金額 + data.受入金額 - data.残量金額;

                // 2012.08: 金額計算は貯蔵品リストからするように変更
                #region
                //==================================
                // 残量の根拠
                //var 残量根拠tmpList = (from p in 対象貯蔵品List
                //                  where p.ID == data.ID
                //                  select p).Distinct();
                //check済みList = new ArrayList();
                //foreach (var tmp in 残量根拠tmpList)
                //{
                //    bool check済み = false;
                //    foreach (NBaseData.BLC.貯蔵品リスト d in check済みList)
                //    {
                //        if (tmp.発注番号 == d.発注番号)
                //        {
                //            check済み = true;
                //        }
                //    }
                //    if (check済み == false)
                //    {
                //        decimal 単価 = 0;
                //        if (tmp.支払数 > 0)
                //        {
                //            単価 = tmp.支払単価;
                //        }
                //        else
                //        {
                //            単価 = tmp.受領単価;
                //        }
                //        data.残量s.Add(new 貯蔵品編集RowData残量(tmp.残量, 単価));

                //        check済みList.Add(tmp);
                //    }
                //}
                #endregion

                // 関連データを記憶
                foreach (OdChozoShousai shousai in endShousaiList)
                {
                    if (chozoShousai.MsLoID == shousai.MsLoID &&
                        chozoShousai.MsVesselItemID == shousai.MsVesselItemID)
                    {
                        data.OdChozoShousaiData = shousai;
                        if (data.残量 == 0)
                        {
                            data.残量 = shousai.Count;
                            data.消費 = data.繰越 + data.受入 - data.残量;


                            // 2012.08: 金額計算は貯蔵品リストからするように変更
                            #region
                            //decimal 金額 = 0;
                            //int work数量 = data.消費;
                            //for (int i = 0; i < data.残量s.Count; i++)
                            //{
                            //    NBaseData.BLC.貯蔵品編集RowData残量 残 = data.残量s[i];
                            //    if (残.残量 > work数量)
                            //    {
                            //        data.残量金額 = (残.残量 - work数量) * 残.金額;
                            //        for (int j = i + 1; j < data.残量s.Count; j++)
                            //        {
                            //            残 = data.残量s[j];
                            //            data.残量金額 += 残.残量 * 残.金額;
                            //        }
                            //        break;
                            //    }
                            //    else
                            //    {
                            //        work数量 -= 残.残量;
                            //    }
                            //}
                            #endregion

                        }

                        break;
                    }
                }
                chozoInfo.datas.Add(data);
            }

            return chozoInfo;
        }

        public static List<NBaseData.BLC.貯蔵品リスト> 貯蔵品編集_取得(NBaseData.DAC.MsUser loginUser, int sy, int sm, int ey, int em, int msVesselId, NBaseData.BLC.貯蔵品リスト.対象Enum outputKind)
        {
            List<NBaseData.BLC.貯蔵品リスト> retList = new List<NBaseData.BLC.貯蔵品リスト>();
            DateTime st = new DateTime(sy, sm, 1);
            DateTime ed = new DateTime(ey, em, 1);

            NBaseData.BLC.貯蔵品リスト.Init(); // 

            for (DateTime tmp_ym = st; tmp_ym <= ed; tmp_ym = tmp_ym.AddMonths(1))
            {
                int tmp_y = tmp_ym.Year;
                int tmp_m = tmp_ym.Month;

                List<NBaseData.BLC.貯蔵品リスト> tmp_list = NBaseData.BLC.貯蔵品リスト.GetRecordsByMsVesselId(loginUser, tmp_y, tmp_m, outputKind, msVesselId);

                retList.AddRange(tmp_list);
            }
            return retList;
        }
        public static List<NBaseData.BLC.貯蔵品リスト> 貯蔵品編集_最終取得(NBaseData.DAC.MsUser loginUser, int sy, int sm, int ey, int em, int msVesselId, NBaseData.BLC.貯蔵品リスト.対象Enum outputKind)
        {
            List<NBaseData.BLC.貯蔵品リスト> retList = new List<NBaseData.BLC.貯蔵品リスト>();
            DateTime st = new DateTime(sy, sm, 1);
            DateTime ed = new DateTime(ey, em, 1);

            NBaseData.BLC.貯蔵品リスト.Init(); // 

            for (DateTime tmp_ym = ed; tmp_ym >= st; tmp_ym = tmp_ym.AddMonths(-1))
            {
                int tmp_y = tmp_ym.Year;
                int tmp_m = tmp_ym.Month;

                List<NBaseData.BLC.貯蔵品リスト> tmp_list = NBaseData.BLC.貯蔵品リスト.GetRecordsByMsVesselId(loginUser, tmp_y, tmp_m, outputKind, msVesselId);

                retList.AddRange(tmp_list);
                if (tmp_list.Count > 0)
                {
                    break;
                }
            }
            return retList;
        }

        private static bool CheckDate(string ym1, string ym2)
        {
            int y1 = 0;
            int m1 = 0;
            int y2 = 0;
            int m2 = 0;
            ConverDateString(ym1, ref y1, ref m1);
            ConverDateString(ym2, ref y2, ref m2);
            if (y1 < y2)
            {
                return true;
            }
            if (y1 == y2 && m1 < m2)
            {
                return true;
            }
            return false;
        }

        private static bool ConverDateString(string sdate, ref int year, ref int month)
        {
            string sy = sdate.Substring(0, 4);
            string sm = sdate.Substring(4);

            year = 0;
            month = 0;

            try
            {
                year = Convert.ToInt32(sy);
                month = Convert.ToInt32(sm);
            }
            catch (Exception e)
            {
                //失敗したときはとりあえず0
                year = 0;
                month = 0;
                return false;
            }

            return true;
        }

        public static bool Excel書込(ExcelCreator.Xlsx.XlsxCreator crea,
            MsVessel msvessel, 貯蔵品リスト.対象Enum kind,
            int year, int smonth, int eyear, int emonth, List<貯蔵品編集RowData> datalist, decimal yosan)
        {
            //種別
            貯蔵品管理票.WriteShubetsu(crea, kind);

            //船名
            貯蔵品管理票.WriteVesselName(crea, msvessel);

            //期間
            貯蔵品管理票.WriteDate(crea, year, eyear, smonth, emonth);

            //アイテム
            貯蔵品管理票.WriteItemData(crea, datalist);

            //予算たち                  
            貯蔵品管理票.WriteYosan(crea, kind, yosan);

            // 2016.10 特定品の対応
            int addRows = 0;
            if (kind == 貯蔵品リスト.対象Enum.特定品)
            {
                int stRowNo = 11;
                int counter = 0;

                Dictionary<int, string> categoryDic = new Dictionary<int, string>();

                string category = "";
                foreach (貯蔵品編集RowData inf in datalist)
                {
                    if (category != datalist[counter].カテゴリ)
                    {
                        category = datalist[counter].カテゴリ;

                        categoryDic.Add(counter, category);
                    }

                    counter++;
                }
                addRows = categoryDic.Count(); // この後削除する行の調整数（表示対象となったカテゴリ分だけ、この後削除行の開始、削除行数を調整する）

                if (categoryDic.Count() > 1)
                {
                    int rowOffset = 2;
                    for (int i = 1; i < categoryDic.Count(); i ++)
                    {
                        crea.RowInsert(stRowNo + rowOffset + categoryDic.Keys.ElementAt(i) - 1, 1);
                        crea.RowCopy(stRowNo, stRowNo + rowOffset + categoryDic.Keys.ElementAt(i) - 1);

                        crea.Cell("B" + (stRowNo + rowOffset + categoryDic.Keys.ElementAt(i)).ToString()).Value = categoryDic[categoryDic.Keys.ElementAt(i)];

                        rowOffset++;
                    }
                }

                crea.Cell("B" + (stRowNo + 1).ToString()).Value = categoryDic[categoryDic.Keys.ElementAt(0)];
                crea.RowDelete(10, 1);　// 特定品のカテゴリのためのTemplate行を削除する
            }
            else
            {
                crea.RowDelete(10, 2); // 特定品のカテゴリのためのTemplate行を削除する
            }

            // 余分な行を削除する
            RemoveRows(crea, datalist.Count, addRows);

            return true;
        }

        /// <summary>
        /// 種別を書く
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="kind"></param>
        private static void WriteShubetsu(ExcelCreator.Xlsx.XlsxCreator crea, 貯蔵品リスト.対象Enum kind)
        {
            //名前を取得
            string name = 貯蔵品管理票.KindData[(int)kind];

            crea.Cell(貯蔵品管理票.TEMP_SHUBETSU).Value = name;
        }

        /// <summary>
        /// 船名を書く
        /// </summary>
        /// <returns></returns>
        private static void WriteVesselName(ExcelCreator.Xlsx.XlsxCreator crea, MsVessel msvessel)
        {
            string data = msvessel.VesselName;
            data += " (";
            data += msvessel.VesselNo;
            data += ")";

            crea.Cell(貯蔵品管理票.TEMP_VESSEL_NAME).Value = data;

        }

        /// <summary>
        /// 期間を書く
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="year"></param>
        /// <param name="sm"></param>
        /// <param name="em"></param>
        private static void WriteDate(ExcelCreator.Xlsx.XlsxCreator crea, int year, int eyear, int sm, int em)
        {
            string sdate = year.ToString();
            sdate += "年";
            sdate += sm.ToString();
            sdate += "月 ～ ";

            sdate += eyear.ToString();
            sdate += "年";
            sdate += em.ToString();
            sdate += "月";

            crea.Cell(貯蔵品管理票.TEMP_DATE).Value = sdate;


        }

        /// <summary>
        /// アイテムデータを書く
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="datalist"></param>
        private static void WriteItemData(ExcelCreator.Xlsx.XlsxCreator crea, List<貯蔵品編集RowData> datalist)
        {
            int i = 1;

            foreach (貯蔵品編集RowData inf in datalist)
            {
                string itemtemp = 貯蔵品管理票.ITEM_TEMP;
                itemtemp += i.ToString();

                //品名
                crea.Cell(itemtemp).Value = inf.品名;

                //単位
                string stani = itemtemp + 貯蔵品管理票.ITEM_単位;
                crea.Cell(stani).Value = inf.単位;

                //繰越
                string skurikoshi = itemtemp + 貯蔵品管理票.ITEM_繰越;
                crea.Cell(skurikoshi).Value = inf.繰越;

                //受け入れ
                string sukeire = itemtemp + 貯蔵品管理票.TEMP_受け入れ;
                crea.Cell(sukeire).Value = inf.受入;

                //受け入れ金額
                string sukeireamount = itemtemp + 貯蔵品管理票.TEMP_受け入れ金額;
                crea.Cell(sukeireamount).Value = inf.受入金額;

                //残量
                string szanryou = itemtemp + 貯蔵品管理票.TEMP_残量;
                crea.Cell(szanryou).Value = inf.残量;

                //残量金額
                string szanryouamount = itemtemp + 貯蔵品管理票.TEMP_残量金額;
                crea.Cell(szanryouamount).Value = inf.残量金額;

                //消費金額
                string sshouhiamount = itemtemp + 貯蔵品管理票.TEMP_消費金額;
                crea.Cell(sshouhiamount).Value = inf.消費金額;
                
                //-------------------------------------------
                i++;
            }

        }

        /// <summary>
        /// 本年度予算と消費/予算を書く
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="yosan"></param>
        private static void WriteYosan(ExcelCreator.Xlsx.XlsxCreator crea, 貯蔵品リスト.対象Enum kind, decimal yosan)
        {
            crea.Cell(貯蔵品管理票.TEMP_YOSAN).Value = yosan.ToString();

            ////潤滑油のときは予算を書く
            //if (kind == 貯蔵品リスト.対象Enum.潤滑油)
            //{
            //    crea.Cell(貯蔵品管理票.TEMP_YOSAN).Value = yosan.ToString();
            //}
            ////船用品のときは-を書く
            //if (kind == 貯蔵品リスト.対象Enum.船用品)
            //{
            //    crea.Cell(貯蔵品管理票.TEMP_YOSAN).Value = "-";
            //    crea.Cell(貯蔵品管理票.消費予算Cell).Value = "-";
            //}
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="dataCount"></param>
        private static void RemoveRows(ExcelCreator.Xlsx.XlsxCreator crea, int dataCount, int addCount)
        {
            if (dataCount >= 100)
                return;

            int removeRowCount = 0;
            if (dataCount <= 30)
            {
                // ３０行以下の場合、３０行まで出力
                crea.RowDelete(40, 70);
                removeRowCount = 70;
            }
            else
            {
                // ３０行以上の場合、データ数分出力
                crea.RowDelete(10 + dataCount + addCount, 100 - dataCount);　
                removeRowCount = 100 - dataCount - addCount;
            }

            // 印刷範囲の設定
            crea.PrintArea(0, 0, 13, 113 - removeRowCount);
        }


        //-------------------------------------------------------------
        //メンバ変数===========================================
        //表示数
        private const int MAX_ITEM = 30;

        //テンプレートファイル名
        private const string TemplateFileName = "Template/貯蔵品管理票Template.xlsx";

        //各列の名前たち
        //アイテム関連
        private const string ITEM_TEMP = "**ITEM_";
        private const string ITEM_単位 = "_TANI";
        private const string ITEM_繰越 = "_KURIKOSHI";
        private const string TEMP_受け入れ = "_UKEIRE";
        private const string TEMP_受け入れ金額 = "_UKEIRE_AMOUNT";
        private const string TEMP_残量 = "_ZANRYO";
        private const string TEMP_残量金額 = "_ZANRYO_AMOUNT";
        private const string TEMP_消費金額 = "_SHOUHI_AMOUNT";

        //種別
        private const string TEMP_SHUBETSU = "**SHUBETSU";

        //船
        private const string TEMP_VESSEL_NAME = "**VESSEL_NAME";

        //日付
        private const string TEMP_DATE = "**NENGETSU";

        //期間内予算
        private const string TEMP_YOSAN = "**YOSAN";

        //消費/予算の場所
        private const string 消費予算Cell = "K44";

        //public static string[] KindData = { "潤滑油", "船用品" };
        public static string[] KindData = { "潤滑油", "船用品", "特定品" };
    }


    [DataContract()]
    public class 貯蔵品年間管理表_UNIT
    {
        [DataMember]
        public int 数量 = 0;
        [DataMember]
        public decimal 合計金額 = 0;
    }

    [DataContract()]
    public class 貯蔵品年間管理表RowData
    {
        [DataMember]
        public string ID = "";
        [DataMember]
        public string 品名 = "";
        [DataMember]
        public string 単位 = "";
        [DataMember]
        public Dictionary<string, 貯蔵品年間管理表_UNIT> datas = new Dictionary<string, 貯蔵品年間管理表_UNIT>();


        public static List<string> Keys
        {
            get
            {
                List<string> keys = new List<string>();
                keys.Add("4月");
                keys.Add("5月");
                keys.Add("6月");
                keys.Add("7月");
                keys.Add("8月");
                keys.Add("9月");
                keys.Add("10月");
                keys.Add("11月");
                keys.Add("12月");
                keys.Add("1月");
                keys.Add("2月");
                keys.Add("3月");
                keys.Add("期首残");
                keys.Add("上期末残");
                keys.Add("期末残");

               return keys;
            }
        }

        public static string ConvertKey(int month)
        {
            if (month < 1 && month > 12)
                return null;
            if (month > 3)
                return Keys[month - 4];
            else
                return Keys[month + 8];
        }

        public 貯蔵品年間管理表_UNIT get期首残Unit()
        {
            return datas["期首残"];
        }
        public 貯蔵品年間管理表_UNIT get上期末残Unit()
        {
            return datas["上期末残"];
        }
        public 貯蔵品年間管理表_UNIT get期末残Unit()
        {
            return datas["期末残"];
        }

        public 貯蔵品年間管理表RowData()
        {
            foreach (string key in Keys)
            {
                datas.Add(key, new 貯蔵品年間管理表_UNIT());
            }
        }

    
    }

    [DataContract()]
    public class 貯蔵品年間管理表
    {
        [DataMember]
        public MsVessel msVessel;
        [DataMember]
        public List<貯蔵品年間管理表RowData> datas = new List<貯蔵品年間管理表RowData>();


        public static 貯蔵品年間管理表 Get貯蔵品年間管理表(MsUser loginUser, MsVessel msvessel, 貯蔵品リスト.対象Enum kind, int year)
        {
            貯蔵品年間管理表 chozoInfo = new 貯蔵品年間管理表();
            chozoInfo.msVessel = msvessel;

            // 指定年
            DateTime fromYm = new DateTime(year, 4, 1);
            // 期末
            DateTime toYm = fromYm.AddMonths(12-1); // 当月からの計算なので１１を足す
            // 前期末
            DateTime prevYm = fromYm.AddMonths(-1);
            // 上期末
            DateTime kamikiYm = fromYm.AddMonths(6 - 1); // 当月からの計算なので５を足す

            // 前期末データ
            string prevYmStr = prevYm.Year.ToString() + prevYm.Month.ToString("00");
            List<OdChozoShousai> prevShousaiList = new List<OdChozoShousai>();           
            List<NBaseData.BLC.貯蔵品リスト> 前期末_貯蔵品List = null;

            // 上期末データ
            string kamikiYmStr = kamikiYm.Year.ToString() + kamikiYm.Month.ToString("00");
            List<NBaseData.BLC.貯蔵品リスト> 上期末_貯蔵品List = null;

            // 期末データ 
            string toYmStr = toYm.Year.ToString() + toYm.Month.ToString("00");
            List<NBaseData.BLC.貯蔵品リスト> 期末_貯蔵品List = null;

            // フルデータ
            //List<NBaseData.BLC.貯蔵品リスト> 対象貯蔵品List = null;

            // 受入データ
            Dictionary<string, 年間管理表受入データ> 受入Dic = null;

            //==================================
            // 検索

            List<OdChozoShousai> tmpShousaiList = new List<OdChozoShousai>();   // 範囲内のLOまたはペイントの全ての貯蔵詳細            
            tmpShousaiList = OdChozoShousai.GetRecordsByVesselID_Period_Shubetsu(loginUser, msvessel.MsVesselID, prevYmStr, toYmStr, (int)kind);
            List<OdChozoShousai> fullShousaiList = tmpShousaiList.OrderBy(obj => obj.MsLoID).OrderBy(obj => obj.MsVesselItemID).Distinct(new OdChozoShousaiCompare()).ToList<OdChozoShousai>();

            // 前期末計算用の貯蔵品詳細データの取得
            List<OdChozoShousai> tmpList = OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(loginUser, msvessel.MsVesselID, prevYmStr, (int)kind);
            if (kind == 貯蔵品リスト.対象Enum.潤滑油)
            {
                List<MsLoVessel> msLoVessels = MsLoVessel.GetRecordsByMsVesselID(loginUser, msvessel.MsVesselID);
                foreach (MsLoVessel loVessel in msLoVessels)
                {
                    OdChozoShousai ocs = null;
                    foreach (OdChozoShousai chozoShousai in tmpList)
                    {
                        if (chozoShousai.MsLoID == loVessel.MsLoID)
                        {
                            ocs = chozoShousai;
                            break;
                        }
                    }
                    if (ocs != null)
                    {
                        prevShousaiList.Add(ocs);
                        tmpList.Remove(ocs);
                    }
                    else
                    {
                        ocs = new OdChozoShousai();
                        ocs.MsLoID = loVessel.MsLoID;
                        ocs.LoName = loVessel.MsLoName;
                        ocs.TaniName = loVessel.MsTaniName;

                        prevShousaiList.Add(ocs);
                    }
                }
            }
            else
            {
                List<MsVesselItemVessel> msVesselItemVessels = MsVesselItemVessel.GetRecordsByMsVesselID(null, loginUser, msvessel.MsVesselID, (int)MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント);
                foreach (MsVesselItemVessel vesselItemVessel in msVesselItemVessels)
                {
                    OdChozoShousai ocs = null;
                    foreach (OdChozoShousai chozoShousai in tmpList)
                    {
                        if (chozoShousai.MsVesselItemID == vesselItemVessel.MsVesselItemID)
                        {
                            ocs = chozoShousai;
                            break;
                        }
                    }
                    if (ocs != null)
                    {
                        prevShousaiList.Add(ocs);
                        tmpList.Remove(ocs);
                    }
                    else
                    {
                        ocs = new OdChozoShousai();
                        ocs.MsVesselItemID = vesselItemVessel.MsVesselItemID;
                        ocs.VesselItemName = vesselItemVessel.VesselItemName;
                        ocs.TaniName = vesselItemVessel.MsTaniName;

                        prevShousaiList.Add(ocs);
                    }
                }
            }
            // 前期末計算用の貯蔵品詳細データの取得
            前期末_貯蔵品List = NBaseData.BLC.貯蔵品リスト.GetRecordsByMsVesselId(loginUser, prevYm.Year, prevYm.Month, kind, msvessel.MsVesselID);

            // 上期末計算用の貯蔵品詳細データの取得
            上期末_貯蔵品List = NBaseData.BLC.貯蔵品リスト.GetRecordsByMsVesselId(loginUser, kamikiYm.Year, kamikiYm.Month, kind, msvessel.MsVesselID);

            // 期末計算用の貯蔵品詳細データの取得
            期末_貯蔵品List = NBaseData.BLC.貯蔵品リスト.GetRecordsByMsVesselId(loginUser, toYm.Year, toYm.Month, kind, msvessel.MsVesselID);

            // 受入データ
            if (kind == 貯蔵品リスト.対象Enum.潤滑油)
            {
                受入Dic = NBaseData.BLC.貯蔵品年間管理表データ.GetRecords潤滑油_受入(loginUser, msvessel.MsVesselID, year);
            }
            else
            {
                受入Dic = NBaseData.BLC.貯蔵品年間管理表データ.GetRecords船用品_受入(loginUser, msvessel.MsVesselID, year);
            }

            //==================================
            // 
            //foreach (OdChozoShousai chozoShousai in prevShousaiList)
            foreach (OdChozoShousai chozoShousai in fullShousaiList)
            {
                貯蔵品年間管理表RowData data = new 貯蔵品年間管理表RowData();

                if (kind == 貯蔵品リスト.対象Enum.潤滑油)
                {
                    data.ID = chozoShousai.MsLoID;
                    data.品名 = chozoShousai.LoName;
                }
                else
                {
                    data.ID = chozoShousai.MsVesselItemID;
                    data.品名 = chozoShousai.VesselItemName;
                }
                data.単位 = chozoShousai.TaniName;


                //==================================
                // 前期末分
                貯蔵品年間管理表_UNIT 前期末 = data.get期首残Unit();
                var 前期末tmpList = from p in 前期末_貯蔵品List
                                where p.ID == data.ID
                                select p;
                foreach (var tmp in 前期末tmpList)
                {
                    int 数量 = 0;
                    decimal 単価 = 0;
                    if (tmp.支払数 > 0)
                    {
                        数量 = tmp.支払数;
                        単価 = tmp.支払単価;
                    }
                    else
                    {
                        数量 = tmp.受領数;
                        単価 = tmp.受領単価;
                    }
                    前期末.数量 += tmp.残量;
                    前期末.合計金額 += (int)(tmp.残量 * 単価);
                }


                //==================================
                // 上期末分
                貯蔵品年間管理表_UNIT 上期末 = data.get上期末残Unit();
                var 上期末tmpList = from p in 上期末_貯蔵品List
                                where p.ID == data.ID
                                select p;
                foreach (var tmp in 上期末tmpList)
                {
                    int 数量 = 0;
                    decimal 単価 = 0;
                    if (tmp.支払数 > 0)
                    {
                        数量 = tmp.支払数;
                        単価 = tmp.支払単価;
                    }
                    else
                    {
                        数量 = tmp.受領数;
                        単価 = tmp.受領単価;
                    }
                    上期末.数量 += tmp.残量;
                    上期末.合計金額 += (int)(tmp.残量 * 単価);
                }

                //==================================
                // 期末分
                貯蔵品年間管理表_UNIT 期末 = data.get期末残Unit();
                var 期末tmpList = from p in 期末_貯蔵品List
                                 where p.ID == data.ID
                                 select p;
                foreach (var tmp in 期末tmpList)
                {
                    int 数量 = 0;
                    decimal 単価 = 0;
                    if (tmp.支払数 > 0)
                    {
                        数量 = tmp.支払数;
                        単価 = tmp.支払単価;
                    }
                    else
                    {
                        数量 = tmp.受領数;
                        単価 = tmp.受領単価;
                    }
                    期末.数量 += tmp.残量;
                    期末.合計金額 += (int)(tmp.残量 * 単価);
                }


                //==================================
                // 受入
                if (受入Dic.ContainsKey(data.ID))
                {
                    年間管理表受入データ ukeire = 受入Dic[data.ID];

                    for ( int i = 0; i < 12; i ++ )
                    {
                        貯蔵品年間管理表_UNIT unit = data.datas[貯蔵品年間管理表RowData.ConvertKey(i+1)];

                        unit.数量 = ukeire.Counts[i];
                        unit.合計金額 = ukeire.Amounts[i];
                    }
                }

                chozoInfo.datas.Add(data);
            }

            return chozoInfo;
        }

        public static void Excel書込(ExcelCreator.Xlsx.XlsxCreator crea, MsVessel msvessel, 貯蔵品リスト.対象Enum kind, List<貯蔵品年間管理表RowData> datalist)
        {
            if (kind == 貯蔵品リスト.対象Enum.潤滑油)
            {
                crea.Header("", "潤滑油年間管理票", "&D");
            }
            else
            {
                crea.Header("", "船用品年間管理票", "&D");
            }

            //船関係を書く
            貯蔵品年間管理表.WriteVessel(crea, msvessel);

            //アイテムデータを書く
            貯蔵品年間管理表.WriteItemData(crea, datalist);

            RemoveRows(crea, datalist.Count);
        }

        /// <summary>
        /// 船を書く
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="vessel"></param>
        private static void WriteVessel(ExcelCreator.Xlsx.XlsxCreator crea, MsVessel msvessel)
        {
            //書き込み文字列作成
            string s = msvessel.VesselName;
            s += " (";
            s += msvessel.VesselNo;
            s += ")";

            crea.Cell(TEMP_船名).Value = s;
        }

        /// <summary>
        /// アイテムデータを書く        
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="datalist"></param>
        private static void WriteItemData(ExcelCreator.Xlsx.XlsxCreator crea, List<貯蔵品年間管理表RowData> datalist)
        {
            int no = 1;

            foreach (貯蔵品年間管理表RowData data in datalist)
            {
                //大本ID作成
                string itemid = ITEM_TEMP + no.ToString();
                string id = "";

                //品名
                crea.Cell(itemid).Value = data.品名;

                //単位
                id = itemid + TEMP_単位;
                crea.Cell(id).Value = data.単位;

                //期首残
                貯蔵品年間管理表_UNIT ksz_unit = data.datas["期首残"];
                id = itemid + TEMP_期首残;
                crea.Cell(id).Value = ksz_unit.数量;
                id = itemid + TEMP_期首残金額;
                crea.Cell(id).Value = ksz_unit.合計金額;

                //上期末残
                貯蔵品年間管理表_UNIT kkz_unit = data.datas["上期末残"];
                id = itemid + TEMP_上期末残;
                crea.Cell(id).Value = kkz_unit.数量;
                id = itemid + TEMP_上期末残金額;
                crea.Cell(id).Value = kkz_unit.合計金額;

                //期末残
                貯蔵品年間管理表_UNIT kmz_unit = data.datas["期末残"];
                id = itemid + TEMP_期末残;
                crea.Cell(id).Value = kmz_unit.数量;
                id = itemid + TEMP_期末残金額;
                crea.Cell(id).Value = kmz_unit.合計金額;


                //各月のデータを入れる
                for (int i = 1; i <= 12; i++)
                {
                    string key = 貯蔵品年間管理表RowData.ConvertKey(i);
                    if (key != null)
                    {
                        貯蔵品年間管理表_UNIT unit = data.datas[key];

                        //指定月の数量
                        id = itemid + TEMP_数量 + i.ToString();
                        crea.Cell(id).Value = unit.数量;

                        //指定月の合計金額
                        id = itemid + TEMP_金額 + i.ToString();
                        crea.Cell(id).Value = unit.合計金額;
                    }
                }

                //////////////////////////////////////////////
                no++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="dataCount"></param>
        private static void RemoveRows(ExcelCreator.Xlsx.XlsxCreator crea, int dataCount)
        {
            if (dataCount >= 50)
                return;

            int removeRowCount = 0;
            if (dataCount <= 30)
            {
                crea.RowDelete(35, 20);
                removeRowCount = 20;
            }
            else
            {
                crea.RowDelete(5 + dataCount, 50 - dataCount);
                removeRowCount = 50 - dataCount;
            }

            //// 印刷範囲の設定
            //crea.PrintArea(0, 0, 13, 63 - removeRowCount);
        }


        //メンバ変数=============================================

        //定数--------------------------------------------------
        //テンプレートファイル名
        const string TemplateFileName = "Template/貯蔵品年間管理票Template.xlsx";

        //各種名前を定義する------------------------------------

        private const string TEMP_船名 = "**VESSEL_NAME";

        //各列の名前たち
        private const string ITEM_TEMP = "**ITEM";
        private const string TEMP_単位 = "_TANI";
        private const string TEMP_数量 = "_VALUE";
        private const string TEMP_金額 = "_AMOUNT";
        private const string TEMP_期首残 = "_KISYUZAN";
        private const string TEMP_期首残金額 = "_KISYUZAN_AMOUNT";
        private const string TEMP_上期末残 = "_KAMIMATSUZAN";
        private const string TEMP_上期末残金額 = "_KAMIMATSUZAN_AMOUNT";
        private const string TEMP_期末残 = "_KIMATSUZAN";
        private const string TEMP_期末残金額 = "_KIMATSUZAN_AMOUNT";
    }

    class OdChozoShousaiCompare : EqualityComparer<OdChozoShousai>
    {
        public override bool Equals(OdChozoShousai o1, OdChozoShousai o2)
        {
            if (o1.MsLoID != null && o1.MsLoID.Length >0)
            {
                return (o1.MsLoID == o2.MsLoID);
            }
            else
            {
                return (o1.MsVesselItemID == o2.MsVesselItemID);
            }
        }

        public override int GetHashCode(OdChozoShousai o)
        {
            if (o.MsLoID != null && o.MsLoID.Length > 0)
                return o.MsLoID.GetHashCode();
            else
                return o.MsVesselItemID.GetHashCode();
        }
    }
}
