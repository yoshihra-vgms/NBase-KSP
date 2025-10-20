using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using NBaseData.DAC;
using NBaseData.DS;


namespace NBaseData.DAC
{
    /// <summary>
    /// スケジュール明細金額IFテーブル
    /// </summary>
    [DataContract()]
    [TableAttribute("TKJNAIPLAN_AMT_BILL")]
    public class TKJNAIPLAN_AMT_BILL
    {
        #region データメンバ
        /// <summary>
        /// 船舶NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("FUNE_NO")]
        public string FuneNo { get; set; }

        /// <summary>
        /// 次航NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("JIKO_NO")]
        public string JikoNo { get; set; }

        /// <summary>
        /// 日付
        /// </summary>
        [DataMember]
        [ColumnAttribute("M_SC_YMD")]
        public DateTime MScYmd { get; set; }

        /// <summary>
        /// 日付
        /// </summary>
        [DataMember]
        [ColumnAttribute("SC_YMD")]
        public DateTime ScYmd { get; set; }

        /// <summary>
        /// コマ番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOMA_NO")]
        public string KomaNo { get; set; }

        /// <summary>
        /// 行番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("LINE_NO")]
        public string LineNo { get; set; }

        /// <summary>
        /// 貨物NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("CAG_NO")]
        public string CargoNo { get; set; }

        /// <summary>
        /// 荷主NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("CHRAIT_NO")]
        public string ChartererNo { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        [ColumnAttribute("QTY")]
        public double Qtty { get; set; }

        /// <summary>
        /// 単位(デフォルト)
        /// </summary>
        [DataMember]
        [ColumnAttribute("KZTANI_CD")]
        public string KztaniCd { get; set; }

        /// <summary>
        /// 運賃算定基準区分
        /// </summary>
        [DataMember]
        [ColumnAttribute("USKJN_KBN")]
        public string UskjnKbn { get; set; }

        /// <summary>
        /// 計算区分
        /// </summary>
        [DataMember]
        [ColumnAttribute("KSN_KBN")]
        public string KsnKbn { get; set; }

        /// <summary>
        /// 単価(FreightRate)
        /// </summary>
        [DataMember]
        [ColumnAttribute("FRTANKA")]
        public decimal FrTanka { get; set; }

        /// <summary>
        /// 金額(\)
        /// </summary>
        [DataMember]
        [ColumnAttribute("GAKYEN")]
        public decimal GakYen { get; set; }

        /// <summary>
        /// 消費税区分
        /// </summary>
        [DataMember]
        [ColumnAttribute("SYOHIZEI_KBN")]
        public string SyohizeiKbn { get; set; }

        /// <summary>
        /// 表示税額
        /// </summary>
        [DataMember]
        [ColumnAttribute("SYOHIZEI_GAK")]
        public decimal SyohizeiGak { get; set; }

        /// <summary>
        /// 合計金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("TTLGAKU")]
        public decimal TtlGaku { get; set; }

        /// <summary>
        /// 確定区分
        /// </summary>
        [DataMember]
        [ColumnAttribute("K_KBN")]
        public string KKbn { get; set; }

        [DataMember]
        [ColumnAttribute("LD_DS_SET_NO")]
        public string LdDsSetNo { get; set; }

        /// <summary>
        /// 更新プログラムID
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDPGMID")]
        public string UpdPgmID { get; set; }

        /// <summary>
        /// 更新ユーザID
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDUSERID")]
        public string UpdUserID { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDATE_YMDHMS")]
        public Int64 UpdateYMDHMS { get; set; }


        #endregion

        public static List<TKJNAIPLAN_AMT_BILL> GetRecords(NBaseData.DAC.MsUser loginUser, TKJNAIPLAN TKJNAIPLAN)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLAN_AMT_BILL), MethodBase.GetCurrentMethod());

            List<TKJNAIPLAN_AMT_BILL> ret = new List<TKJNAIPLAN_AMT_BILL>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<TKJNAIPLAN_AMT_BILL> mapping = new MappingBase<TKJNAIPLAN_AMT_BILL>();

            //取得条件設定
            Params.Add(new DBParameter("FUNE_NO", TKJNAIPLAN.FuneNo));
            Params.Add(new DBParameter("JIKO_NO", TKJNAIPLAN.JikoNo));
            Params.Add(new DBParameter("SC_YMD", TKJNAIPLAN.ScYmd));
            Params.Add(new DBParameter("KOMA_NO", TKJNAIPLAN.KomaNo));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static TKJNAIPLAN_AMTIF GetRecord(NBaseData.DAC.MsUser loginUser, TKJNAIPLAN_AMTIF TKJNAIPLAN_AMTIF,DjDousei dousei,DjDouseiCargo cargo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLAN_AMTIF), MethodBase.GetCurrentMethod());

            List<TKJNAIPLAN_AMTIF> ret = new List<TKJNAIPLAN_AMTIF>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<TKJNAIPLAN_AMTIF> mapping = new MappingBase<TKJNAIPLAN_AMTIF>();

            //取得条件設定
            Params.Add(new DBParameter("FUNE_NO", TKJNAIPLAN_AMTIF.FuneNo));
            Params.Add(new DBParameter("JIKO_NO", TKJNAIPLAN_AMTIF.JikoNo));
            Params.Add(new DBParameter("SC_YMD", TKJNAIPLAN_AMTIF.ScYmd));
            Params.Add(new DBParameter("KOMA_NO", dousei.KomaNo));
            Params.Add(new DBParameter("LINE_NO", cargo.LineNo));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }

        public static List<TKJNAIPLAN_AMT_BILL> GetRecordsBy揚げ(NBaseData.DAC.MsUser loginUser, TKJNAIPLAN_AMT_BILL tKJNAIPLAN_AMT_BILL)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLAN_AMT_BILL), MethodBase.GetCurrentMethod());

            List<TKJNAIPLAN_AMT_BILL> ret = new List<TKJNAIPLAN_AMT_BILL>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<TKJNAIPLAN_AMT_BILL> mapping = new MappingBase<TKJNAIPLAN_AMT_BILL>();

            //取得条件設定
            Params.Add(new DBParameter("FUNE_NO", tKJNAIPLAN_AMT_BILL.FuneNo));
            Params.Add(new DBParameter("JIKO_NO", tKJNAIPLAN_AMT_BILL.JikoNo));
            Params.Add(new DBParameter("CAG_NO", tKJNAIPLAN_AMT_BILL.CargoNo));
            Params.Add(new DBParameter("LD_DS_SET_NO", tKJNAIPLAN_AMT_BILL.LdDsSetNo));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

    }
}
