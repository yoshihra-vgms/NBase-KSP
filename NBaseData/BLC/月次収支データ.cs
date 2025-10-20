using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Attrs;
using System.Reflection;
using ORMapping;
using NBaseData.DAC;

namespace NBaseData.BLC
{
    [DataContract()]
    public class 月次収支データ
    {
        [DataMember]
        [ColumnAttribute("KIKAN_HIMOKU_ID")]
        public string KIKAN_HIMOKU_ID { get; set; }

        [DataMember]
        [ColumnAttribute("実績")]
        public decimal 実績 { get; set; }
        
        [DataMember]
        [ColumnAttribute("計画")]
        public decimal 計画 { get; set; } // WING予算データを使用する
        
        [DataMember]
        [ColumnAttribute("差額")]
        public decimal 差額 { get; set; } // 実績 - WING予算データを使用する（計算式とする）

        [DataMember]
        [ColumnAttribute("実績_USD")]
        public decimal 実績_USD { get; set; }

        [DataMember]
        [ColumnAttribute("計画_USD")]
        public decimal 計画_USD { get; set; } // WING予算データを使用する

        [DataMember]
        [ColumnAttribute("換算後")]
        public decimal 換算後 { get; set; }

        [DataMember]
        [ColumnAttribute("影響額")]
        public decimal 影響額 { get; set; }

        [DataMember]
        [ColumnAttribute("市況その他")]
        public decimal 市況その他 { get; set; }

        [DataMember]
        [ColumnAttribute("MS_HIMOKU_ID")]
        public int MsHimokuID { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public 月次収支データ()
        {
        }

        public static List<月次収支データ> GetRecords(
            NBaseData.DAC.MsUser loginUser,
            string nengetsu,
            int year,
            string kaikeiBumonCode,
            bool isRuikei,
            Dictionary<string, int> himokuKikanHimoku
            )
        {
            List<月次収支データ> ret = new List<月次収支データ>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(月次収支データ), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("NENGETSU", nengetsu));
            Params.Add(new DBParameter("BUMON_CODE","1")); // 固定
            Params.Add(new DBParameter("YEAR", year.ToString()));
            int month = int.Parse(nengetsu.Substring(4));
            if (month >= 4 && month <= 9)
            {
                Params.Add(new DBParameter("YOSAN_KUBUN", "00"));
            }
            else
            {
                Params.Add(new DBParameter("YOSAN_KUBUN", "01"));
            }
            if (isRuikei)
            {
                Params.Add(new DBParameter("TANGETSU_FLAG", ""));
            }
            else
            {
                Params.Add(new DBParameter("TANGETSU_FLAG", "1"));
            }
            Params.Add(new DBParameter("KAIKEI_BUMON_CODE", kaikeiBumonCode));
            Params.Add(new DBParameter("KAMIKI_YOSAN_KUBUN", "0")); // 固定

            MappingBase<月次収支データ> mapping = new MappingBase<月次収支データ>();
            try
            {
                ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            foreach (月次収支データ g in ret)
            {
                if (himokuKikanHimoku.ContainsKey(g.KIKAN_HIMOKU_ID))
                {
                    g.MsHimokuID = himokuKikanHimoku[g.KIKAN_HIMOKU_ID];
                }
                if (g.実績 == decimal.MinValue)
                {
                    g.実績 = 0;
                }
                if (g.計画 == decimal.MinValue)
                {
                    g.計画 = 0;
                }
                if (g.差額 == decimal.MinValue)
                {
                    g.差額 = 0;
                }
                if (g.実績_USD == decimal.MinValue)
                {
                    g.実績_USD = 0;
                }
                if (g.計画_USD == decimal.MinValue)
                {
                    g.計画_USD = 0;
                }
                if (g.換算後 == decimal.MinValue)
                {
                    g.換算後 = 0;
                }
                if (g.影響額 == decimal.MinValue)
                {
                    g.影響額 = 0;
                }
                if (g.市況その他 == decimal.MinValue)
                {
                    g.市況その他 = 0;
                }
            }

            return ret;
        }
   }
}
