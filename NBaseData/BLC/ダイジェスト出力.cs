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
    public class ダイジェスト出力
    {
        #region データメンバ

        /// <summary>
        /// BUMON_CD
        /// </summary>
        [DataMember]
        [ColumnAttribute("BUMON_CD")]
        public string BUMON_CD { get; set; }

        /// <summary>
        /// FNAME
        /// </summary>
        [DataMember]
        [ColumnAttribute("FNAME")]
        public string FNAME { get; set; }

        /// <summary>
        /// 運賃
        /// </summary>
        [DataMember]
        [ColumnAttribute("運賃")]
        public decimal 運賃 { get; set; }

        /// <summary>
        /// 貸船料
        /// </summary>
        [DataMember]
        [ColumnAttribute("貸船料")]
        public decimal 貸船料 { get; set; }

        /// <summary>
        /// 他船取扱手数料
        /// </summary>
        [DataMember]
        [ColumnAttribute("他船取扱手数料")]
        public decimal 他船取扱手数料 { get; set; }

        /// <summary>
        /// その他海運業収益
        /// </summary>
        [DataMember]
        [ColumnAttribute("その他海運業収益")]
        public decimal その他海運業収益 { get; set; }

        /// <summary>
        /// 海運業収益
        /// </summary>
        [DataMember]
        [ColumnAttribute("海運業収益")]
        public decimal 海運業収益 { get; set; }

        /// <summary>
        /// ｳﾁ過月度分
        /// </summary>
        [DataMember]
        [ColumnAttribute("ｳﾁ過月度分_収益")]
        public decimal ｳﾁ過月度分_収益 { get; set; }

        /// <summary>
        /// ｳﾁ為替分
        /// </summary>
        [DataMember]
        [ColumnAttribute("ｳﾁ為替分_収益")]
        public decimal ｳﾁ為替分_収益 { get; set; }

        /// <summary>
        /// 運航費
        /// </summary>
        [DataMember]
        [ColumnAttribute("運航費")]
        public decimal 運航費 { get; set; }

        /// <summary>
        /// 船費
        /// </summary>
        [DataMember]
        [ColumnAttribute("船費")]
        public decimal 船費 { get; set; }

        /// <summary>
        /// 借船料
        /// </summary>
        [DataMember]
        [ColumnAttribute("借船料")]
        public decimal 借船料 { get; set; }

        /// <summary>
        /// その他海運業費用
        /// </summary>
        [DataMember]
        [ColumnAttribute("その他海運業費用")]
        public decimal その他海運業費用 { get; set; }

        /// <summary>
        /// 海運業費用
        /// </summary>
        [DataMember]
        [ColumnAttribute("海運業費用")]
        public decimal 海運業費用 { get; set; }

        /// <summary>
        /// ｳﾁ過月度分
        /// </summary>
        [DataMember]
        [ColumnAttribute("ｳﾁ過月度分_費用")]
        public decimal ｳﾁ過月度分_費用 { get; set; }

        /// <summary>
        /// ｳﾁ為替分
        /// </summary>
        [DataMember]
        [ColumnAttribute("ｳﾁ為替分_費用")]
        public decimal ｳﾁ為替分_費用 { get; set; }

        /// <summary>
        /// 販管費
        /// </summary>
        [DataMember]
        [ColumnAttribute("販管費")]
        public decimal 販管費 { get; set; }

        #endregion




        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ダイジェスト出力()
        {
        }

        public static List<ダイジェスト出力> GetRecords単月(NBaseData.DAC.MsUser loginUser, string year, string yearMonth)
        {
            List<ダイジェスト出力> ret = new List<ダイジェスト出力>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(ダイジェスト出力), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YEAR", year));
            Params.Add(new DBParameter("YEARMONTH", yearMonth));

            MappingBase<ダイジェスト出力> mapping = new MappingBase<ダイジェスト出力>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<ダイジェスト出力> GetRecords累計(NBaseData.DAC.MsUser loginUser, string year, string yearMonth)
        {
            List<ダイジェスト出力> ret = new List<ダイジェスト出力>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(ダイジェスト出力), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YEAR", year));
            Params.Add(new DBParameter("YEARMONTH", yearMonth));

            MappingBase<ダイジェスト出力> mapping = new MappingBase<ダイジェスト出力>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
    }
}
