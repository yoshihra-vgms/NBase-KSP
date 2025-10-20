using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using ORMapping;
using SqlMapper;
using System.Reflection;
using NBaseData.DAC;

namespace NBaseData.BLC
{
    [DataContract()]
    public class 貯蔵品リスト
    {
        //public enum 対象Enum { 潤滑油 = 0, 船用品 };
        public enum 対象Enum { 潤滑油 = 0, 船用品, 特定品 };

        #region データメンバ

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("ID")]
        public string ID { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        [DataMember]
        [ColumnAttribute("品名")]
        public string 品名 { get; set; }

        /// <summary>
        /// 見積回答 : 発注番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("発注番号")]
        public string 発注番号 { get; set; }

        /// <summary>
        /// 見積回答 : 見積依頼先
        /// </summary>
        [DataMember]
        [ColumnAttribute("業者名")]
        public string 業者名 { get; set; }

        /// <summary>
        /// 受領 : 受領日
        /// </summary>
        [DataMember]
        [ColumnAttribute("納品日")]
        public DateTime 納品日 { get; set; }

        /// <summary>
        /// 受領詳細品目 : 受領数
        /// </summary>
        [DataMember]
        [ColumnAttribute("受領数")]
        public int 受領数 { get; set; }

        /// <summary>
        /// 受領詳細品目 : 単価
        /// </summary>
        [DataMember]
        [ColumnAttribute("受領単価")]
        public decimal 受領単価 { get; set; }

        /// <summary>
        /// 支払詳細品目 : 支払数
        /// </summary>
        [DataMember]
        [ColumnAttribute("支払数")]
        public int 支払数 { get; set; }

        /// <summary>
        /// 支払詳細品目 : 単価
        /// </summary>
        [DataMember]
        [ColumnAttribute("支払単価")]
        public decimal 支払単価 { get; set; }

        /// <summary>
        /// 単位
        /// </summary>
        [DataMember]
        [ColumnAttribute("単位")]
        public string 単位 { get; set; }

        /// <summary>
        /// 受領 : 受領日
        /// </summary>
        [DataMember]
        [ColumnAttribute("受領年月")]
        public string 受領年月 { get; set; }

        /// <summary>
        /// MS_VESSEL_ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MS_VESSEL_ID { get; set; }

        /// <summary>
        /// 消費
        /// </summary>
        [DataMember]
        public int 消費 { get; set; }

        /// <summary>
        /// 残量
        /// </summary>
        [DataMember]
        public int 残量 { get; set; }

        /// <summary>
        /// OD_JRY_SHOUSAI_ITEM_ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("JS_ID")]
        public string JS_ID { get; set; }

        /// <summary>
        /// OD_SHR_SHOUSAI_ITEM_ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SS_ID")]
        public string SS_ID { get; set; }

        #endregion


        public static List<貯蔵品リスト> _船_潤滑油_貯蔵品List = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public 貯蔵品リスト()
        {
        }

        #region public static List<貯蔵品リスト> GetRecords(MsUser loginUser, int year, int month, 対象Enum target)
        public static List<貯蔵品リスト> GetRecords(MsUser loginUser, int year, int month, 対象Enum target)
        {
            if (target == 対象Enum.潤滑油)
            {
                return GetRecords潤滑油(loginUser, year, month, -1);
            }
            //else
            else if (target == 対象Enum.船用品)
            {
                return GetRecords船用品(loginUser, year, month, -1);
            }
            else
            {
                return GetRecords特定品(loginUser, year, month, -1);
            }
        }
        #endregion
        
        #region public static List<貯蔵品リスト> GetRecordsByMsVesselId(MsUser loginUser, int year, int month, 対象Enum target, int msVesselId)
        public static List<貯蔵品リスト> GetRecordsByMsVesselId(MsUser loginUser, int year, int month, 対象Enum target, int msVesselId)
        {
            List<貯蔵品リスト> retAll = null;
            if (target == 対象Enum.潤滑油)
            {
                retAll = GetRecords潤滑油(loginUser, year, month, msVesselId);
            }
            //else
            else if (target == 対象Enum.船用品)
            {
                retAll = GetRecords船用品(loginUser, year, month, msVesselId);
            }
            else
            {
                retAll = GetRecords特定品(loginUser, year, month, msVesselId);
            }
            return retAll;
        }
        #endregion



        #region private static List<貯蔵品リスト> GetRecords潤滑油(MsUser loginUser, int year, int month, int msVesselId)
        private static List<貯蔵品リスト> GetRecords潤滑油(MsUser loginUser, int year, int month, int msVesselId)
        {
            // 外部からinit() 呼び出し
            //_船_潤滑油_貯蔵品List = null;

            int target = (int)対象Enum.潤滑油;
            string nengetsu = year.ToString() + month.ToString("00");
            List<貯蔵品リスト> ret貯蔵品リスト = new List<貯蔵品リスト>();

            // 指定月の残量を取得する
            List<OdChozoShousai> chozoShousaiList = null;
            if (msVesselId == -1)
            {
                chozoShousaiList = OdChozoShousai.GetRecordsByDate_Shubetsu(loginUser, nengetsu, target);
            }
            else
            {
                chozoShousaiList = OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(loginUser, msVesselId, nengetsu, target);
            }

            // 最終月 = 指定月
            DateTime EndYearMonth = new DateTime(year, month, 1);

            foreach (OdChozoShousai chozoShousai in chozoShousaiList)
            {
                // 開始月 = 貯蔵詳細の受入開始月
                DateTime StartYearMonth = new DateTime(int.Parse(chozoShousai.UkeireNengetsu.Substring(0, 4)), int.Parse(chozoShousai.UkeireNengetsu.Substring(4, 2)), 1);
                

                //=======================================
                // 対象月
                //=======================================
                int 残量 = chozoShousai.Count;

                // 繰越 = (１つ前の残量)
                DateTime sengetsuDt = EndYearMonth.AddMonths(-1);
                string sengetsu = sengetsuDt.Year.ToString() + sengetsuDt.Month.ToString("00");
                OdChozoShousai preChozoShousai = OdChozoShousai.GetRecordsByVesselID_Date_LoID(loginUser, chozoShousai.MsVesselID, sengetsu, chozoShousai.MsLoID);
                int 繰越 = 0;
                if (preChozoShousai != null)
                {
                    繰越 = preChozoShousai.Count;
                }

                // 対象月の受入合計
                string togetsu = EndYearMonth.Year.ToString() + EndYearMonth.Month.ToString("00");
                int 受入合計 = 0;
                List<貯蔵品リスト> 船_潤滑油_貯蔵品List = 貯蔵品リスト.GetRecords発注潤滑油(loginUser, chozoShousai.MsVesselID, chozoShousai.MsLoID, togetsu);
                foreach (貯蔵品リスト 船_潤滑油_貯蔵品 in 船_潤滑油_貯蔵品List)
                {
                    if (船_潤滑油_貯蔵品.支払数 > 0)
                    {
                        受入合計 += 船_潤滑油_貯蔵品.支払数;
                    }
                    else
                    {
                        受入合計 += 船_潤滑油_貯蔵品.受領数;
                    }
                }

                // 対象月消費量
                int 対象月消費量 = (繰越 + 受入合計 - 残量);

                // 繰越なし、受入なしなら、残量はないはず
                // この品目は、対象から外す
                if (繰越 == 0 && 受入合計 == 0 && 残量 == 0)
                {
                    continue;
                }

                if (対象月消費量 < 繰越)
                {
                    // 当月分の受入からは消費していない
                    #region
                    int 対象月受入数 = 船_潤滑油_貯蔵品List.Count;
                    if (対象月受入数 > 0)
                    {
                        for (int i = 対象月受入数; i > 0; i--)
                        {
                            貯蔵品リスト 船_潤滑油_貯蔵品 = 船_潤滑油_貯蔵品List[i - 1];

                            int 受入 = 0;
                            if (船_潤滑油_貯蔵品.支払数 > 0)
                            {
                                受入 = 船_潤滑油_貯蔵品.支払数;
                            }
                            else
                            {
                                受入 = 船_潤滑油_貯蔵品.受領数;
                            }

                            船_潤滑油_貯蔵品.消費 = 0;
                            船_潤滑油_貯蔵品.残量 = 受入;

                            ret貯蔵品リスト.Add(船_潤滑油_貯蔵品);
                        }
                    }
                    #endregion
                }
                else
                {
                    // 当月分の受入からの消費は、当月消費量-繰越
                    #region
                    int 対象月分からの消費量 = 対象月消費量 - 繰越;
                    int 対象月受入数 = 船_潤滑油_貯蔵品List.Count;
                    if (対象月受入数 > 0)
                    {
                        for (int i = 対象月受入数; i > 0; i--)
                        {
                            貯蔵品リスト 船_潤滑油_貯蔵品 = 船_潤滑油_貯蔵品List[i - 1];

                            int 受入 = 0;
                            if (船_潤滑油_貯蔵品.支払数 > 0)
                            {
                                受入 = 船_潤滑油_貯蔵品.支払数;
                            }
                            else
                            {
                                受入 = 船_潤滑油_貯蔵品.受領数;
                            }

                            if (受入 > 対象月分からの消費量)
                            {
                                船_潤滑油_貯蔵品.消費 = 対象月分からの消費量;
                                船_潤滑油_貯蔵品.残量 = 受入 - 対象月分からの消費量;

                                対象月分からの消費量 = 0;
                            }
                            else
                            {
                                船_潤滑油_貯蔵品.消費 = 受入;
                                船_潤滑油_貯蔵品.残量 = 0;

                                対象月分からの消費量 -= 受入;
                            }
                            ret貯蔵品リスト.Add(船_潤滑油_貯蔵品);
                        }
                    }
                    #endregion
                }

                //=======================================
                // 対象月以前
                //=======================================
                if (繰越 > 0)
                {
                    if (対象月消費量 < 繰越)
                    {
                        // 当月の消費は、すべて以前の月の受入から
                        #region
                        残量 -= 受入合計;
                        int 対象月以前の月からの消費量 = 繰越;

                        for (DateTime ym = EndYearMonth.AddMonths(-1); ym >= StartYearMonth; ym = ym.AddMonths(-1))
                        {
                            // 船、潤滑油ごとの発注情報を取得する
                            togetsu = ym.Year.ToString() + ym.Month.ToString("00");
                            船_潤滑油_貯蔵品List = 貯蔵品リスト.GetRecords発注潤滑油(loginUser, chozoShousai.MsVesselID, chozoShousai.MsLoID, togetsu);

                            int 対象月受入数 = 船_潤滑油_貯蔵品List.Count;
                            if (対象月受入数 > 0)
                            {
                                for (int i = 0; i < 対象月受入数; i++)
                                {
                                    貯蔵品リスト 船_潤滑油_貯蔵品 = 船_潤滑油_貯蔵品List[i];

                                    int 受入 = 0;
                                    if (船_潤滑油_貯蔵品.支払数 > 0)
                                    {
                                        受入 = 船_潤滑油_貯蔵品.支払数;
                                    }
                                    else
                                    {
                                        受入 = 船_潤滑油_貯蔵品.受領数;
                                    }

                                    if (残量 <= 0)
                                    {
                                        船_潤滑油_貯蔵品.消費 = 受入;
                                        船_潤滑油_貯蔵品.残量 = 0;
                                    }

                                    else if (受入 > 残量)
                                    {
                                        船_潤滑油_貯蔵品.消費 = 受入 - 残量;
                                        船_潤滑油_貯蔵品.残量 = 残量;
                                    }
                                    else
                                    {
                                        船_潤滑油_貯蔵品.消費 = 0;
                                        船_潤滑油_貯蔵品.残量 = 受入;
                                    }
                                    ret貯蔵品リスト.Add(船_潤滑油_貯蔵品);

                                    残量 -= 受入;

                                    if (受入 > 対象月以前の月からの消費量)
                                    {
                                        // この受入分から消費開始なので、これ以上さかのぼらない
                                        対象月以前の月からの消費量 = 0;
                                        break;
                                    }
                                    対象月以前の月からの消費量 -= 受入;
                                }
                            }
                            if (対象月以前の月からの消費量 == 0)
                            {
                                break;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        // 繰越分はすべて消費している
                        #region
                        int 対象月以前の月からの消費量 = 繰越;

                        for (DateTime ym = EndYearMonth.AddMonths(-1); ym >= StartYearMonth; ym = ym.AddMonths(-1))
                        {
                            // 船、潤滑油ごとの発注情報を取得する
                            togetsu = ym.Year.ToString() + ym.Month.ToString("00");
                            船_潤滑油_貯蔵品List = 貯蔵品リスト.GetRecords発注潤滑油(loginUser, chozoShousai.MsVesselID, chozoShousai.MsLoID, togetsu);

                            int 対象月受入数 = 船_潤滑油_貯蔵品List.Count;
                            if (対象月受入数 > 0)
                            {
                                for (int i = 対象月受入数; i > 0; i--)
                                {
                                    貯蔵品リスト 船_潤滑油_貯蔵品 = 船_潤滑油_貯蔵品List[i - 1];

                                    int 受入 = 0;
                                    if (船_潤滑油_貯蔵品.支払数 > 0)
                                    {
                                        受入 = 船_潤滑油_貯蔵品.支払数;
                                    }
                                    else
                                    {
                                        受入 = 船_潤滑油_貯蔵品.受領数;
                                    }

                                    船_潤滑油_貯蔵品.消費 = 受入;
                                    船_潤滑油_貯蔵品.残量 = 0;

                                    ret貯蔵品リスト.Add(船_潤滑油_貯蔵品);

                                    if (受入 > 対象月以前の月からの消費量)
                                    {
                                        // この受入分から消費開始なので、これ以上さかのぼらない
                                        対象月以前の月からの消費量 = 0;
                                        break;
                                    }
                                    対象月以前の月からの消費量 -= 受入;
                                }
                            }
                            if (対象月以前の月からの消費量 == 0)
                            {
                                break;
                            }
                        }
                        #endregion
                    }
                }
            }

            return ret貯蔵品リスト;
        }
        #endregion
       
        #region private static List<貯蔵品リスト> GetRecords船用品(MsUser loginUser, int year, int month, int msVesselId)
        private static List<貯蔵品リスト> GetRecords船用品(MsUser loginUser, int year, int month, int msVesselId)
        {
            int target = (int)対象Enum.船用品;
            string nengetsu = year.ToString() + month.ToString("00");
            List<貯蔵品リスト> ret貯蔵品リスト = new List<貯蔵品リスト>();

            // 指定月の残量を取得する
            List<OdChozoShousai> chozoShousaiList = null;
            if (msVesselId == -1)
            {
                chozoShousaiList = OdChozoShousai.GetRecordsByDate_Shubetsu(loginUser, nengetsu, target);
            }
            else
            {
                chozoShousaiList = OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(loginUser, msVesselId, nengetsu, target);
            }
           
            // 最終月 = 指定月
            DateTime EndYearMonth = new DateTime(year, month, 1);

            foreach (OdChozoShousai chozoShousai in chozoShousaiList)
            {
                // 開始月 = 貯蔵詳細の受入開始月
                DateTime StartYearMonth = new DateTime(int.Parse(chozoShousai.UkeireNengetsu.Substring(0, 4)), int.Parse(chozoShousai.UkeireNengetsu.Substring(4, 2)), 1);

                //=======================================
                // 対象月
                //=======================================
                int 残量 = chozoShousai.Count;

                // 繰越 = (１つ前の残量)
                DateTime sengetsuDt = EndYearMonth.AddMonths(-1);
                string sengetsu = sengetsuDt.Year.ToString() + sengetsuDt.Month.ToString("00");
                OdChozoShousai preChozoShousai = OdChozoShousai.GetRecordsByVesselID_Date_VesselItemID(loginUser, chozoShousai.MsVesselID, sengetsu, chozoShousai.MsVesselItemID);
                int 繰越 = 0;
                if (preChozoShousai != null)
                {
                    繰越 = preChozoShousai.Count;
                }

                // 対象月の受入合計
                string togetsu = EndYearMonth.Year.ToString() + EndYearMonth.Month.ToString("00");
                int 受入合計 = 0;
                List<貯蔵品リスト> 船_船用品_貯蔵品List = 貯蔵品リスト.GetRecords発注船用品(loginUser, chozoShousai.MsVesselID, chozoShousai.MsVesselItemID, togetsu);
                foreach (貯蔵品リスト 船_船用品_貯蔵品 in 船_船用品_貯蔵品List)
                {
                    if (船_船用品_貯蔵品.支払数 > 0)
                    {
                        受入合計 += 船_船用品_貯蔵品.支払数;
                    }
                    else
                    {
                        受入合計 += 船_船用品_貯蔵品.受領数;
                    }
                }

                // 対象月消費量
                int 対象月消費量 = (繰越 + 受入合計 - 残量);

                // 繰越なし、受入なしなら、残量はないはず
                // この品目は、対象から外す
                if (繰越 == 0 && 受入合計 == 0 && 残量 == 0)
                {
                    continue;
                }

                if (対象月消費量 < 繰越)
                {
                    // 当月分の受入からは消費していない
                    #region
                    int 対象月受入数 = 船_船用品_貯蔵品List.Count;
                    if (対象月受入数 > 0)
                    {
                        for (int i = 対象月受入数; i > 0; i--)
                        {
                            貯蔵品リスト 船_船用品_貯蔵品 = 船_船用品_貯蔵品List[i - 1];

                            int 受入 = 0;
                            if (船_船用品_貯蔵品.支払数 > 0)
                            {
                                受入 = 船_船用品_貯蔵品.支払数;
                            }
                            else
                            {
                                受入 = 船_船用品_貯蔵品.受領数;
                            }

                            船_船用品_貯蔵品.消費 = 0;
                            船_船用品_貯蔵品.残量 = 受入;

                            ret貯蔵品リスト.Add(船_船用品_貯蔵品);
                        }
                    }
                    #endregion
                }
                else
                {
                    // 当月分の受入からの消費は、当月消費量-繰越
                    #region
                    int 対象月分からの消費量 = 対象月消費量 - 繰越;
                    int 対象月受入数 = 船_船用品_貯蔵品List.Count;
                    if (対象月受入数 > 0)
                    {
                        for (int i = 対象月受入数; i > 0; i--)
                        {
                            貯蔵品リスト 船_船用品_貯蔵品 = 船_船用品_貯蔵品List[i - 1];

                            int 受入 = 0;
                            if (船_船用品_貯蔵品.支払数 > 0)
                            {
                                受入 = 船_船用品_貯蔵品.支払数;
                            }
                            else
                            {
                                受入 = 船_船用品_貯蔵品.受領数;
                            }

                            if (受入 > 対象月分からの消費量)
                            {
                                船_船用品_貯蔵品.消費 = 対象月分からの消費量;
                                船_船用品_貯蔵品.残量 = 受入 - 対象月分からの消費量;

                                対象月分からの消費量 = 0;
                            }
                            else
                            {
                                船_船用品_貯蔵品.消費 = 受入;
                                船_船用品_貯蔵品.残量 = 0;

                                対象月分からの消費量 -= 受入;
                            }
                            ret貯蔵品リスト.Add(船_船用品_貯蔵品);
                        }
                    }
                    #endregion
                }

                //=======================================
                // 対象月以前
                //=======================================
                if (繰越 > 0)
                {
                    if (対象月消費量 < 繰越)
                    {
                        // 当月の消費は、すべて以前の月の受入から
                        #region
                        残量 -= 受入合計;
                        int 対象月以前の月からの消費量 = 繰越;

                        for (DateTime ym = EndYearMonth.AddMonths(-1); ym >= StartYearMonth; ym = ym.AddMonths(-1))
                        {
                            // 船、潤滑油ごとの発注情報を取得する
                            togetsu = ym.Year.ToString() + ym.Month.ToString("00");
                            船_船用品_貯蔵品List = 貯蔵品リスト.GetRecords発注船用品(loginUser, chozoShousai.MsVesselID, chozoShousai.MsVesselItemID, togetsu);

                            int 対象月受入数 = 船_船用品_貯蔵品List.Count;
                            if (対象月受入数 > 0)
                            {
                                for (int i = 0; i < 対象月受入数; i++)
                                {
                                    貯蔵品リスト 船_船用品_貯蔵品 = 船_船用品_貯蔵品List[i];

                                    int 受入 = 0;
                                    if (船_船用品_貯蔵品.支払数 > 0)
                                    {
                                        受入 = 船_船用品_貯蔵品.支払数;
                                    }
                                    else
                                    {
                                        受入 = 船_船用品_貯蔵品.受領数;
                                    }

                                    if (残量 <= 0)
                                    {
                                        船_船用品_貯蔵品.消費 = 受入;
                                        船_船用品_貯蔵品.残量 = 0;
                                    }
                                    else if (受入 > 残量)
                                    {
                                        船_船用品_貯蔵品.消費 = 受入 - 残量;
                                        船_船用品_貯蔵品.残量 = 残量;
                                    }
                                    else
                                    {
                                        船_船用品_貯蔵品.消費 = 0;
                                        船_船用品_貯蔵品.残量 = 受入;
                                    }

                                    ret貯蔵品リスト.Add(船_船用品_貯蔵品);

                                    残量 -= 受入;

                                    if (受入 > 対象月以前の月からの消費量)
                                    {
                                        // この受入分から消費開始なので、これ以上さかのぼらない
                                        対象月以前の月からの消費量 = 0;
                                        break;
                                    }
                                    対象月以前の月からの消費量 -= 受入;
                                }
                            }
                            if (対象月以前の月からの消費量 == 0)
                            {
                                break;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        // 繰越分はすべて消費している
                        #region
                        int 対象月以前の月からの消費量 = 繰越;

                        for (DateTime ym = EndYearMonth.AddMonths(-1); ym >= StartYearMonth; ym = ym.AddMonths(-1))
                        {
                            // 船、潤滑油ごとの発注情報を取得する
                            togetsu = ym.Year.ToString() + ym.Month.ToString("00");
                            船_船用品_貯蔵品List = 貯蔵品リスト.GetRecords発注船用品(loginUser, chozoShousai.MsVesselID, chozoShousai.MsVesselItemID, togetsu);

                            int 対象月受入数 = 船_船用品_貯蔵品List.Count;
                            if (対象月受入数 > 0)
                            {
                                for (int i = 対象月受入数; i > 0; i--)
                                {
                                    貯蔵品リスト 船_船用品_貯蔵品 = 船_船用品_貯蔵品List[i - 1];

                                    int 受入 = 0;
                                    if (船_船用品_貯蔵品.支払数 > 0)
                                    {
                                        受入 = 船_船用品_貯蔵品.支払数;
                                    }
                                    else
                                    {
                                        受入 = 船_船用品_貯蔵品.受領数;
                                    }

                                    船_船用品_貯蔵品.消費 = 受入;
                                    船_船用品_貯蔵品.残量 = 0;

                                    ret貯蔵品リスト.Add(船_船用品_貯蔵品);

                                    if (受入 > 対象月以前の月からの消費量)
                                    {
                                        // この受入分から消費開始なので、これ以上さかのぼらない
                                        対象月以前の月からの消費量 = 0;
                                        break;
                                    }
                                    対象月以前の月からの消費量 -= 受入;
                                }
                            }
                            if (対象月以前の月からの消費量 == 0)
                            {
                                break;
                            }
                        }
                        #endregion
                    }
                }
            }

            return ret貯蔵品リスト;
        }
        #endregion
       
        #region public static List<貯蔵品リスト> GetRecords特定品(MsUser loginUser, int year, int month, int msVesselId)
        public static List<貯蔵品リスト> GetRecords特定品(MsUser loginUser, int year, int month, int msVesselId)
        {
            string nengetsu = year.ToString() + month.ToString("00");
            List<貯蔵品リスト> ret貯蔵品リスト = new List<貯蔵品リスト>();

            // 船用品を取得する
            List<MsVesselItemVessel> vesselItemList = MsVesselItemVessel.GetRecordsByMsVesselID(loginUser, msVesselId);

            // 特定船用品抽出
            var tmp = vesselItemList.Where(obj => obj.SpecificFlag == 1);

            foreach (MsVesselItemVessel vesselItem in tmp)
            {
                ret貯蔵品リスト.AddRange(貯蔵品リスト.GetRecords発注特定品(loginUser, msVesselId, vesselItem.MsVesselItemID, nengetsu));
            }

            return ret貯蔵品リスト;
        }
        #endregion


        //private static List<貯蔵品リスト> GetRecords発注潤滑油(NBaseData.DAC.MsUser loginUser, int msVesselId, string msLoId, string ym)
        //{
        //    List<貯蔵品リスト> ret = new List<貯蔵品リスト>();

        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(貯蔵品リスト), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("SHR_STATUS", (int)OdShr.STATUS.支払済));
        //    Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.燃料_潤滑油)));
        //    Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
        //    Params.Add(new DBParameter("YYYYMM", ym));
        //    Params.Add(new DBParameter("MS_LO_ID", msLoId));

        //    MappingBase<貯蔵品リスト> mapping = new MappingBase<貯蔵品リスト>();
        //    ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

        //    return ret;
        //}
        #region private static List<貯蔵品リスト> GetRecords発注潤滑油(NBaseData.DAC.MsUser loginUser, int msVesselId, string msLoId, string ym)
        private static List<貯蔵品リスト> GetRecords発注潤滑油(NBaseData.DAC.MsUser loginUser, int msVesselId, string msLoId, string ym)
        {
            #region 旧コード
            //if (_船_潤滑油_貯蔵品List == null)
            //{
            //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(貯蔵品リスト), "GetRecords発注潤滑油_2");

            //    ParameterConnection Params = new ParameterConnection();
            //    Params.Add(new DBParameter("SHR_STATUS", (int)OdShr.STATUS.支払済));
            //    Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.燃料_潤滑油)));
            //    Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            //    //Params.Add(new DBParameter("YYYYMM", ym));
            //    //Params.Add(new DBParameter("MS_LO_ID", msLoId));

            //    MappingBase<貯蔵品リスト> mapping = new MappingBase<貯蔵品リスト>();
            //    _船_潤滑油_貯蔵品List = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            //}

            //List<貯蔵品リスト> ret = new List<貯蔵品リスト>();
            //var tmp = _船_潤滑油_貯蔵品List.Where(obj => (obj.ID == msLoId && obj.受領年月 == ym));
            //if (tmp.Count() > 0)
            //{
            //    ret = tmp.ToList();
            //}
            //return ret;
            #endregion

            if (_船_潤滑油_貯蔵品List != null)
            {
                var tmp1 = _船_潤滑油_貯蔵品List.Where(obj => (obj.MS_VESSEL_ID == msVesselId));
                if (tmp1.Count() == 0)
                {
                    _船_潤滑油_貯蔵品List = null;
                }
            }
            if (_船_潤滑油_貯蔵品List == null)
            {
                string SQL = SqlMapper.SqlMapper.GetSql(typeof(貯蔵品リスト), "GetRecords発注潤滑油_2");

                ParameterConnection Params = new ParameterConnection();
                Params.Add(new DBParameter("SHR_STATUS", (int)OdShr.STATUS.支払済));
                Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.燃料_潤滑油)));
                Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
                //Params.Add(new DBParameter("YYYYMM", ym));
                //Params.Add(new DBParameter("MS_LO_ID", msLoId));

                MappingBase<貯蔵品リスト> mapping = new MappingBase<貯蔵品リスト>();
                _船_潤滑油_貯蔵品List = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            }

            List<貯蔵品リスト> ret = new List<貯蔵品リスト>();
            var tmp2 = _船_潤滑油_貯蔵品List.Where(obj => (obj.ID == msLoId && obj.受領年月 == ym));
            if (tmp2.Count() > 0)
            {
                foreach (貯蔵品リスト obj in tmp2)
                {
                    ret.Add(obj.Clone());
                }
            }
            return ret;
        }
        #endregion
        #region private static List<貯蔵品リスト> GetRecords発注船用品(NBaseData.DAC.MsUser loginUser, int msVesselId, string msVesselItemId, string ym)
        private static List<貯蔵品リスト> GetRecords発注船用品(NBaseData.DAC.MsUser loginUser, int msVesselId, string msVesselItemId, string ym)
        {
            List<貯蔵品リスト> ret = new List<貯蔵品リスト>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(貯蔵品リスト), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SHR_STATUS", (int)OdShr.STATUS.支払済));
            Params.Add(new DBParameter("CATEGORY_NUMBER", MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント)));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品)));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("YYYYMM", ym));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", msVesselItemId));

            MappingBase<貯蔵品リスト> mapping = new MappingBase<貯蔵品リスト>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        #endregion
        #region private static List<貯蔵品リスト> GetRecords発注特定品(NBaseData.DAC.MsUser loginUser, int msVesselId, string msVesselItemId, string ym)
        private static List<貯蔵品リスト> GetRecords発注特定品(NBaseData.DAC.MsUser loginUser, int msVesselId, string msVesselItemId, string ym)
        {
            List<貯蔵品リスト> ret = new List<貯蔵品リスト>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(貯蔵品リスト), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SHR_STATUS", (int)OdShr.STATUS.支払済));
            Params.Add(new DBParameter("CATEGORY_NUMBER", MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント)));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品)));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("YYYYMM", ym));
            Params.Add(new DBParameter("MS_VESSEL_ITEM_ID", msVesselItemId));

            MappingBase<貯蔵品リスト> mapping = new MappingBase<貯蔵品リスト>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        #endregion

        #region public static void Init()
        public static void Init()
        {
            _船_潤滑油_貯蔵品List = null;
        }
        #endregion

        #region public 貯蔵品リスト Clone()
        public 貯蔵品リスト Clone()
        {
            貯蔵品リスト ret = new 貯蔵品リスト();

            ret.ID = this.ID;
            ret.品名 = this.品名;
            ret.発注番号 = this.発注番号;
            ret.業者名 = this.業者名;
            ret.納品日 = this.納品日;
            ret.受領数 = this.受領数;
            ret.受領単価 = this.受領単価;
            ret.支払数 = this.支払数;
            ret.支払単価 = this.支払単価;
            ret.単位 = this.単位;
            ret.受領年月 = this.受領年月;
            ret.MS_VESSEL_ID = this.MS_VESSEL_ID;
            ret.消費 = this.消費;
            ret.残量 = this.残量;
            ret.JS_ID = this.JS_ID;
            ret.SS_ID = this.SS_ID;


            return ret;
        }
        #endregion
    }
}
