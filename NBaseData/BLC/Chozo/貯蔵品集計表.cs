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
    public class 月末集計表データ
    {
        #region データメンバ

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public int VesselID { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        public Dictionary<int, decimal> Amounts { get; set; }

        #endregion

        public 月末集計表データ()
        {
            Amounts = new Dictionary<int, decimal>();    
            for (int i = 1; i <= 12; i++)
            {
                Amounts.Add(i, 0);
            }
        }
    }

    [DataContract()]
    public class 期末集計表データ
    {
        public enum 期末Enum { 繰越 = 3, 第一 = 6, 第二 = 9, 第三 = 12, 第四 = 15 }
        #region データメンバ

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public int VesselID { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public Dictionary<期末Enum, int> Counts { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        public Dictionary<期末Enum, decimal> Amounts { get; set; }

        /// <summary>
        /// 年度予算
        /// </summary>
        [DataMember]
        public decimal Yosan { get; set; }

        #endregion

        public 期末集計表データ()
        {
            Counts = new Dictionary<期末Enum, int>();
            Counts.Add(期末Enum.繰越, 0);
            Counts.Add(期末Enum.第一, 0);
            Counts.Add(期末Enum.第二, 0);
            Counts.Add(期末Enum.第三, 0);
            Counts.Add(期末Enum.第四, 0);
            Amounts = new Dictionary<期末Enum, decimal>();
            Amounts.Add(期末Enum.繰越, 0);
            Amounts.Add(期末Enum.第一, 0);
            Amounts.Add(期末Enum.第二, 0);
            Amounts.Add(期末Enum.第三, 0);
            Amounts.Add(期末Enum.第四, 0);
        }
    }

    [DataContract()]
    public class 貯蔵品集計表
    {
        public enum Enum対象 { 潤滑油, 船用品 };
        
        // 対象の予算（費目のID）
        public const int MS_HIMOKU_潤滑油費_ID = 41;
        public const int MS_HIMOKU_船用品費_ID = 40;


        //==================================================
        // 月末集計表
        //==================================================
        private class inner月末集計表データ
        {
            #region データメンバ

            /// <summary>
            /// ID
            /// </summary>
            [DataMember]
            [ColumnAttribute("MS_VESSEL_ID")]
            public int VesselID { get; set; }

            /// <summary>
            /// 受領年月
            /// </summary>
            [DataMember]
            [ColumnAttribute("受領年月")]
            public string YyyyMm { get; set; }

            /// <summary>
            /// 合計金額
            /// </summary>
            [DataMember]
            [ColumnAttribute("合計金額")]
            public decimal Amount { get; set; }

            public int 年()
            {
                string mmStr = YyyyMm.Substring(4, 2);
                int mm = 0;
                try
                {
                    int.TryParse(mmStr, out mm);
                }
                catch
                {
                }
                return mm;
            }

            #endregion
        }

        public static Dictionary<int, 月末集計表データ> GetRecords潤滑油_受入(NBaseData.DAC.MsUser loginUser, int year)
        {
            Dictionary<int, 月末集計表データ> ret貯蔵品集計表 = new Dictionary<int, 月末集計表データ>();

            List<inner月末集計表データ> innerDatas = GetRecords潤滑油_受入合計(loginUser, year);
            foreach (inner月末集計表データ innerData in innerDatas)
            {
                月末集計表データ d = null;
                if (ret貯蔵品集計表.ContainsKey(innerData.VesselID))
                {
                    d = ret貯蔵品集計表[innerData.VesselID];
                    if (innerData.Amount > 0)
                        d.Amounts[innerData.年()]= innerData.Amount;
                }
                else
                {
                    d = new 月末集計表データ();
                    d.VesselID = innerData.VesselID;
                    if (innerData.Amount > 0)
                        d.Amounts[innerData.年()] = innerData.Amount;


                    ret貯蔵品集計表.Add(d.VesselID, d);
                }
            }

            return ret貯蔵品集計表;
        }
        public static Dictionary<int, 月末集計表データ> GetRecords船用品_受入(NBaseData.DAC.MsUser loginUser, int year)
        {
            Dictionary<int, 月末集計表データ> ret貯蔵品集計表 = new Dictionary<int, 月末集計表データ>();

            List<inner月末集計表データ> innerDatas = GetRecords船用品_受入合計(loginUser, year);
            foreach (inner月末集計表データ innerData in innerDatas)
            {
                月末集計表データ d = null;
                if (ret貯蔵品集計表.ContainsKey(innerData.VesselID))
                {
                    d = ret貯蔵品集計表[innerData.VesselID];
                    if (innerData.Amount > 0)
                        d.Amounts[innerData.年()] = innerData.Amount;
                }
                else
                {
                    d = new 月末集計表データ();
                    d.VesselID = innerData.VesselID;
                    if (innerData.Amount > 0)
                        d.Amounts[innerData.年()] = innerData.Amount;


                    ret貯蔵品集計表.Add(d.VesselID, d);
                }
            }

            return ret貯蔵品集計表;
        }

        private static List<inner月末集計表データ> GetRecords潤滑油_受入合計(NBaseData.DAC.MsUser loginUser, int year)
        {
            DateTime tmp = new DateTime(year, 4, 1);
            DateTime st = NBaseUtil.DateTimeUtils.年度開始日(tmp);
            DateTime ed = NBaseUtil.DateTimeUtils.年度終了日(tmp);

            List<inner月末集計表データ> ret = new List<inner月末集計表データ>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(貯蔵品集計表), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SHR_STATUS", (int)OdShr.STATUS.支払済));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.燃料_潤滑油)));
            Params.Add(new DBParameter("FROM_YYYYMM", st.Year.ToString() + st.Month.ToString("00")));
            Params.Add(new DBParameter("TO_YYYYMM", ed.Year.ToString() + ed.Month.ToString("00")));

            MappingBase<inner月末集計表データ> mapping = new MappingBase<inner月末集計表データ>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        private static List<inner月末集計表データ> GetRecords船用品_受入合計(NBaseData.DAC.MsUser loginUser, int year)
        {
            DateTime tmp = new DateTime(year, 4, 1);
            DateTime st = NBaseUtil.DateTimeUtils.年度開始日(tmp);
            DateTime ed = NBaseUtil.DateTimeUtils.年度終了日(tmp);

            List<inner月末集計表データ> ret = new List<inner月末集計表データ>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(貯蔵品集計表), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SHR_STATUS", (int)OdShr.STATUS.支払済));
            Params.Add(new DBParameter("CATEGORY_NUMBER", MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント)));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品)));
            Params.Add(new DBParameter("FROM_YYYYMM", st.Year.ToString() + st.Month.ToString("00")));
            Params.Add(new DBParameter("TO_YYYYMM", ed.Year.ToString() + ed.Month.ToString("00")));

            MappingBase<inner月末集計表データ> mapping = new MappingBase<inner月末集計表データ>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static Dictionary<int, 月末集計表データ> GetRecords潤滑油_月末(NBaseData.DAC.MsUser loginUser, int year, List<bool> targets)
        {
            Dictionary<int, 月末集計表データ> ret貯蔵品集計表 = new Dictionary<int, 月末集計表データ>();

            // 船
            List<MsVessel> msVesselList = MsVessel.GetRecordsByHachuEnabled(loginUser);

            // 月末残量
            if (targets[0])
            {
                月末データセット(loginUser, year, 4, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表);
                月末データセット(loginUser, year, 5, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表);
                月末データセット(loginUser, year, 6, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表);
            }
            if (targets[1])
            {
                月末データセット(loginUser, year, 7, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表);
                月末データセット(loginUser, year, 8, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表);
                月末データセット(loginUser, year, 9, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表);
            }

            if (targets[2])
            {
                月末データセット(loginUser, year, 10, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表);
                月末データセット(loginUser, year, 11, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表);
                月末データセット(loginUser, year, 12, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表);
            }

            if (targets[3])
            {
                月末データセット(loginUser, year + 1, 1, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表);
                月末データセット(loginUser, year + 1, 2, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表);
                月末データセット(loginUser, year + 1, 3, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表);
            }

            return ret貯蔵品集計表;
        }
        public static Dictionary<int, 月末集計表データ> GetRecords船用品_月末(NBaseData.DAC.MsUser loginUser, int year, List<bool> targets)
        {
            Dictionary<int, 月末集計表データ> ret貯蔵品集計表 = new Dictionary<int, 月末集計表データ>();

            // 船
            List<MsVessel> msVesselList = MsVessel.GetRecordsByHachuEnabled(loginUser);

            // 月末残量
            if (targets[0])
            {
                月末データセット(loginUser, year, 4, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表);
                月末データセット(loginUser, year, 5, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表);
                月末データセット(loginUser, year, 6, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表);
            }
            if (targets[1])
            {
                月末データセット(loginUser, year, 7, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表);
                月末データセット(loginUser, year, 8, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表);
                月末データセット(loginUser, year, 9, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表);
            }

            if (targets[2])
            {
                月末データセット(loginUser, year, 10, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表);
                月末データセット(loginUser, year, 11, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表);
                月末データセット(loginUser, year, 12, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表);
            }

            if (targets[3])
            {
                月末データセット(loginUser, year + 1, 1, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表);
                月末データセット(loginUser, year + 1, 2, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表);
                月末データセット(loginUser, year + 1, 3, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表);
            }

            return ret貯蔵品集計表;
        }

        private static void 月末データセット(NBaseData.DAC.MsUser loginUser, int year, int month, List<MsVessel> msVesselList, 貯蔵品リスト.対象Enum 対象, ref Dictionary<int, 月末集計表データ> ret貯蔵品集計表)
        {
            貯蔵品リスト.Init();
            List<貯蔵品リスト> 潤滑油リストALL = 貯蔵品リスト.GetRecords(loginUser, year, month, 対象);
            foreach (MsVessel msVessel in msVesselList)
            {
                var 潤滑油リスト = from p in 潤滑油リストALL
                             where p.MS_VESSEL_ID == msVessel.MsVesselID
                             select p;
                foreach (var row in 潤滑油リスト)
                {
                    int 数量 = 0;
                    decimal 単価 = 0;

                    if (row.支払単価 > 0)
                    {
                        数量 = row.支払数;
                        単価 = row.支払単価;
                    }
                    else
                    {
                        数量 = row.受領数;
                        単価 = row.受領単価;
                    }
                    decimal 金額 = (数量 - row.消費) * 単価;

                    月末集計表データ d = null;
                    if (ret貯蔵品集計表.ContainsKey(msVessel.MsVesselID))
                    {
                        d = ret貯蔵品集計表[msVessel.MsVesselID];
                        d.Amounts[month] += 金額;
                    }
                    else
                    {
                        d = new 月末集計表データ();
                        d.VesselID = msVessel.MsVesselID;
                        d.Amounts[month] += 金額;

                        ret貯蔵品集計表.Add(d.VesselID, d);
                    }
                }
            }
        }

        //==================================================
        // 期末集計表
        //==================================================
        public static Dictionary<int, 期末集計表データ> GetRecords潤滑油_期末(NBaseData.DAC.MsUser loginUser, int year, List<bool> targets)
        {
            Dictionary<int, 期末集計表データ> ret貯蔵品集計表 = new Dictionary<int, 期末集計表データ>();

            // 船
            List<MsVessel> msVesselList = MsVessel.GetRecordsByHachuEnabled(loginUser);

            // 予算
            Dictionary<int, decimal> yosans = new Dictionary<int, decimal>();
            BgYosanHead bgYosanHead = BgYosanHead.GetRecordByYear(loginUser, year.ToString());
            if (bgYosanHead != null)
            {
                foreach (MsVessel msVessel in msVesselList)
                {
                    BgYosanItem bgYosanItem = BgYosanItem.GetRecordByYearHimokuIDMsVesselID(loginUser, bgYosanHead.YosanHeadID, year, MS_HIMOKU_潤滑油費_ID, msVessel.MsVesselID);
                    if (bgYosanItem != null)
                    {
                        期末集計表データ d = null;
                        if (ret貯蔵品集計表.ContainsKey(msVessel.MsVesselID))
                        {
                            d = ret貯蔵品集計表[msVessel.MsVesselID];
                            if (bgYosanItem.Amount > 0)
                                d.Yosan = bgYosanItem.Amount;
                        }
                        else
                        {
                            d = new 期末集計表データ();
                            d.VesselID = msVessel.MsVesselID;
                            if (bgYosanItem.Amount > 0)
                                d.Yosan = bgYosanItem.Amount;

                            ret貯蔵品集計表.Add(d.VesselID, d);
                        }
                    }
                }
            }
            // 期末残量
            if (targets[0])
            {
                期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.繰越);
                期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.第一);
            }
            if (targets[1])
            {
                if (!targets[0])
                {
                    期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.第一);
                }
                期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.第二);
            }

            if (targets[2])
            {
                if (!targets[1])
                {
                    期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.第二);
                }
                期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.第三);
            }

            if (targets[3])
            {
                if (!targets[2])
                {
                    期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.第三);
                }
                期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.潤滑油, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.第四);
            }



            return ret貯蔵品集計表;
        }
        public static Dictionary<int, 期末集計表データ> GetRecords船用品_期末(NBaseData.DAC.MsUser loginUser, int year, List<bool> targets)
        {
            Dictionary<int, 期末集計表データ> ret貯蔵品集計表 = new Dictionary<int, 期末集計表データ>();

            // 船
            List<MsVessel> msVesselList = MsVessel.GetRecordsByHachuEnabled(loginUser);

            // 予算
            Dictionary<int, decimal> yosans = new Dictionary<int, decimal>();
            BgYosanHead bgYosanHead = BgYosanHead.GetRecordByYear(loginUser, year.ToString());
            if (bgYosanHead != null)
            {
                foreach (MsVessel msVessel in msVesselList)
                {
                    BgYosanItem bgYosanItem = BgYosanItem.GetRecordByYearHimokuIDMsVesselID(loginUser, bgYosanHead.YosanHeadID, year, MS_HIMOKU_船用品費_ID, msVessel.MsVesselID);
                    if (bgYosanItem != null)
                    {
                        期末集計表データ d = null;
                        if (ret貯蔵品集計表.ContainsKey(msVessel.MsVesselID))
                        {
                            d = ret貯蔵品集計表[msVessel.MsVesselID];
                            if (bgYosanItem.Amount > 0)
                                d.Yosan = bgYosanItem.Amount;
                        }
                        else
                        {
                            d = new 期末集計表データ();
                            d.VesselID = msVessel.MsVesselID;
                            if (bgYosanItem.Amount > 0)
                                d.Yosan = bgYosanItem.Amount;

                            ret貯蔵品集計表.Add(d.VesselID, d);
                        }
                    }
                }
            }
            // 期末残量
            if (targets[0])
            {
                期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.繰越);
                期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.第一);
            }
            if (targets[1])
            {
                if (!targets[0])
                {
                    期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.第一);
                }
                期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.第二);
            }

            if (targets[2])
            {
                if (!targets[1])
                {
                    期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.第二);
                }
                期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.第三);
            }

            if (targets[3])
            {
                if (!targets[2])
                {
                    期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.第三);
                }
                期末データセット(loginUser, year, msVesselList, 貯蔵品リスト.対象Enum.船用品, ref ret貯蔵品集計表, 期末集計表データ.期末Enum.第四);
            }



            return ret貯蔵品集計表;
        }

        private static void 期末データセット(NBaseData.DAC.MsUser loginUser, int year, List<MsVessel> msVesselList, 貯蔵品リスト.対象Enum 対象, ref Dictionary<int, 期末集計表データ> ret貯蔵品集計表, 期末集計表データ.期末Enum 期)
        {
            int month = (int)期;
            if (month > 12)
            {
                year++;
                month -= 12;
            }
            List<貯蔵品リスト> 潤滑油リストALL = 貯蔵品リスト.GetRecords(loginUser, year, month, 対象);
            foreach (MsVessel msVessel in msVesselList)
            {
                var 潤滑油リスト = from p in 潤滑油リストALL
                             where p.MS_VESSEL_ID == msVessel.MsVesselID
                             select p;
                foreach (var row in 潤滑油リスト)
                {
                    int 数量 = 0;
                    decimal 単価 = 0;

                    if (row.支払単価 > 0)
                    {
                        数量 = row.支払数;
                        単価 = row.支払単価;
                    }
                    else
                    {
                        数量 = row.受領数;
                        単価 = row.受領単価;
                    }
                    decimal 金額 = (数量 - row.消費) * 単価;

                    期末集計表データ d = null;
                    if (ret貯蔵品集計表.ContainsKey(msVessel.MsVesselID))
                    {
                        d = ret貯蔵品集計表[msVessel.MsVesselID];
                        d.Counts[期] = (数量 - row.消費);
                        d.Amounts[期] += 金額;
                    }
                    else
                    {
                        d = new 期末集計表データ();
                        d.VesselID = msVessel.MsVesselID;
                        d.Counts[期] = (数量 - row.消費);
                        d.Amounts[期] += 金額;

                        ret貯蔵品集計表.Add(d.VesselID, d);
                    }
                }
            }
        }
        
    }
}
