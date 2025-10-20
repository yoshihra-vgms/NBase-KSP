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
    public class 年間管理表受入データ
    {
        #region データメンバ

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public string ID { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public List<int> Counts { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        public List<decimal> Amounts { get; set; }

        #endregion

        public 年間管理表受入データ()
        {
            Counts = new List<int>();
            Amounts = new List<decimal>();
            for (int i = 0; i < 12; i++)
            {
                Counts.Add(0);
                Amounts.Add(0);
            }
        }
    }

    [DataContract()]
    public class 貯蔵品年間管理表データ
    {
        public enum Enum対象 { 潤滑油, 船用品 };
       
        private class inner年間管理表受入データ
        {
            #region データメンバ

            /// <summary>
            /// ID
            /// </summary>
            [DataMember]
            [ColumnAttribute("ID")]
            public string ID { get; set; }

            /// <summary>
            /// 数量
            /// </summary>
            [DataMember]
            [ColumnAttribute("受領月")]
            public string Month { get; set; }

            /// <summary>
            /// 数量
            /// </summary>
            [DataMember]
            [ColumnAttribute("合計数量")]
            public decimal Count { get; set; }

            /// <summary>
            /// 金額
            /// </summary>
            [DataMember]
            [ColumnAttribute("合計金額")]
            public decimal Amount { get; set; }

            #endregion

        }

        public static Dictionary<string, 年間管理表受入データ> GetRecords潤滑油_受入(NBaseData.DAC.MsUser loginUser, int msVesselId, int year)
        {
            Dictionary<string, 年間管理表受入データ> ret年間管理表受入データ = new Dictionary<string, 年間管理表受入データ>();

            List<inner年間管理表受入データ> innerDatas = GetRecords潤滑油_受入合計(loginUser, msVesselId, year);
            foreach (inner年間管理表受入データ innerData in innerDatas)
            {
                年間管理表受入データ d = null;
                if (ret年間管理表受入データ.ContainsKey(innerData.ID))
                {
                    d = ret年間管理表受入データ[innerData.ID];
                    int m = int.Parse(innerData.Month);
                    if (innerData.Count > 0)
                        d.Counts[m - 1] = (int)innerData.Count;
                    if (innerData.Amount > 0)
                        d.Amounts[m - 1] = innerData.Amount;
                }
                else
                {
                    d = new 年間管理表受入データ();
                    d.ID = innerData.ID;
                    int m = int.Parse(innerData.Month);
                    if (innerData.Count > 0)
                        d.Counts[m - 1] = (int)innerData.Count;
                    if (innerData.Amount > 0)
                        d.Amounts[m - 1] = innerData.Amount;


                    ret年間管理表受入データ.Add(d.ID, d);
                }
            }

            return ret年間管理表受入データ;
        }
        public static Dictionary<string, 年間管理表受入データ> GetRecords船用品_受入(NBaseData.DAC.MsUser loginUser, int msVesselId, int year)
        {
            Dictionary<string, 年間管理表受入データ> ret年間管理表受入データ = new Dictionary<string, 年間管理表受入データ>();

            List<inner年間管理表受入データ> innerDatas = GetRecords船用品_受入合計(loginUser, msVesselId, year);
            foreach (inner年間管理表受入データ innerData in innerDatas)
            {
                年間管理表受入データ d = null;
                if (ret年間管理表受入データ.ContainsKey(innerData.ID))
                {
                    d = ret年間管理表受入データ[innerData.ID];
                    int m = int.Parse(innerData.Month);
                    if (innerData.Count > 0)
                        d.Counts[m - 1] = (int)innerData.Count;
                    if (innerData.Amount > 0)
                        d.Amounts[m - 1] = innerData.Amount;
                }
                else
                {
                    d = new 年間管理表受入データ();
                    d.ID = innerData.ID;
                    int m = int.Parse(innerData.Month);
                    if (innerData.Count > 0)
                        d.Counts[m - 1] = (int)innerData.Count;
                    if (innerData.Amount > 0)
                        d.Amounts[m - 1] = innerData.Amount;


                    ret年間管理表受入データ.Add(d.ID, d);
                }
            }

            return ret年間管理表受入データ;
        }

        private static List<inner年間管理表受入データ> GetRecords潤滑油_受入合計(NBaseData.DAC.MsUser loginUser, int msVesselId, int year)
        {
            DateTime tmp = new DateTime(year, 4, 1);
            DateTime st = NBaseUtil.DateTimeUtils.年度開始日(tmp);
            DateTime ed = NBaseUtil.DateTimeUtils.年度終了日(tmp);

            List<inner年間管理表受入データ> ret = new List<inner年間管理表受入データ>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(貯蔵品年間管理表), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("SHR_STATUS", (int)OdShr.STATUS.支払済));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.燃料_潤滑油)));
            Params.Add(new DBParameter("FROM_YYYYMM", st.Year.ToString() + st.Month.ToString("00")));
            Params.Add(new DBParameter("TO_YYYYMM", ed.Year.ToString() + ed.Month.ToString("00")));

            MappingBase<inner年間管理表受入データ> mapping = new MappingBase<inner年間管理表受入データ>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        private static List<inner年間管理表受入データ> GetRecords船用品_受入合計(NBaseData.DAC.MsUser loginUser, int msVesselId, int year)
        {
            DateTime tmp = new DateTime(year, 4, 1);
            DateTime st = NBaseUtil.DateTimeUtils.年度開始日(tmp);
            DateTime ed = NBaseUtil.DateTimeUtils.年度終了日(tmp);

            List<inner年間管理表受入データ> ret = new List<inner年間管理表受入データ>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(貯蔵品年間管理表), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("SHR_STATUS", (int)OdShr.STATUS.支払済));
            Params.Add(new DBParameter("CATEGORY_NUMBER", MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント)));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品)));
            Params.Add(new DBParameter("FROM_YYYYMM", st.Year.ToString() + st.Month.ToString("00")));
            Params.Add(new DBParameter("TO_YYYYMM", ed.Year.ToString() + ed.Month.ToString("00")));

            MappingBase<inner年間管理表受入データ> mapping = new MappingBase<inner年間管理表受入データ>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }        
    }
}
