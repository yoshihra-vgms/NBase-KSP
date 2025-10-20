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
    [TableAttribute("TKJNAIPLAN_AMTIF")]
    public class TKJNAIPLAN_AMTIF
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
        [ColumnAttribute("SC_YMD")]
        public DateTime ScYmd { get; set; }

        /// <summary>
        /// コマ番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOMA_NO")]
        public string KomaNo { get; set; }

        /// <summary>
        /// 処理番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("OPE_NO")]
        public decimal OpeNo { get; set; }

        /// <summary>
        /// 行番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("LINE_NO")]
        public string LineNo { get; set; }

        /// <summary>
        /// 処理セットキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("OPE_SET_KEY")]
        public string OpeSetKey { get; set; }

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

        /// <summary>
        /// 作成日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 取込ユーザ
        /// </summary>
        [DataMember]
        [ColumnAttribute("TRKM_TNTSY_CD")]
        public string TrkmTntsyCD { get; set; }

        /// <summary>
        /// 取込日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("TRKM_DATE")]
        public DateTime TrkmDate { get; set; }

        /// <summary>
        /// 状態
        /// </summary>
        [DataMember]
        [ColumnAttribute("SYORI_STATUS")]
        public string SyoriStatus { get; set; }

        /// <summary>
        /// 状態備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("STATUS_REMARK")]
        public string StatusRemark { get; set; }

        /// <summary>
        /// 外部システム識別コード
        /// </summary>
        [DataMember]
        [ColumnAttribute("EXT_SYSTEM_CD")]
        public string ExtSystemCD { get; set; }

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

        public DjDouseiCargo DouseiCargo;

        #endregion

        public static List<TKJNAIPLAN_AMTIF> GetRecords(NBaseData.DAC.MsUser loginUser, TKJNAIPLANIF TKJNAIPLANIF)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLAN_AMTIF), MethodBase.GetCurrentMethod());

            List<TKJNAIPLAN_AMTIF> ret = new List<TKJNAIPLAN_AMTIF>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<TKJNAIPLAN_AMTIF> mapping = new MappingBase<TKJNAIPLAN_AMTIF>();

            //取得条件設定
            Params.Add(new DBParameter("FUNE_NO", TKJNAIPLANIF.FuneNo));
            Params.Add(new DBParameter("JIKO_NO", TKJNAIPLANIF.JikoNo));
            Params.Add(new DBParameter("SC_YMD", TKJNAIPLANIF.ScYmd));
            Params.Add(new DBParameter("KOMA_NO", TKJNAIPLANIF.KomaNo));
            Params.Add(new DBParameter("EXT_SYSTEM_CD", TKJNAIPLANIF.ExtSystemCD));

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
            Params.Add(new DBParameter("EXT_SYSTEM_CD", TKJNAIPLAN_AMTIF.ExtSystemCD));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }

        public static int GetRecordLineNo(MsUser loginUser, TKJNAIPLAN_AMTIF TKJNAIPLAN_AMTIF)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLAN_AMTIF), "GetRecordLineNo");

            ParameterConnection Params = new ParameterConnection();
            //取得条件設定
            Params.Add(new DBParameter("FUNE_NO", TKJNAIPLAN_AMTIF.FuneNo));
            Params.Add(new DBParameter("SC_YMD", TKJNAIPLAN_AMTIF.ScYmd.ToString("yyyyMMdd")));
            Params.Add(new DBParameter("KOMA_NO", TKJNAIPLAN_AMTIF.KomaNo));

            object obj = DBConnect.ExecuteScalar(loginUser.MsUserID, SQL, Params);

            if (obj == null)
            {
                return 1;
            }

            short cnt = (short)obj;
            return (int)(cnt + 1);
        }

        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null,loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLAN_AMTIF), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("FUNE_NO",FuneNo));
            Params.Add(new DBParameter("JIKO_NO",JikoNo));
            Params.Add(new DBParameter("SC_YMD",ScYmd));
            Params.Add(new DBParameter("KOMA_NO",KomaNo));
            Params.Add(new DBParameter("OPE_NO",OpeNo));
            Params.Add(new DBParameter("LINE_NO",LineNo));
            Params.Add(new DBParameter("OPE_SET_KEY",OpeSetKey));
            Params.Add(new DBParameter("CAG_NO",CargoNo));
            Params.Add(new DBParameter("CHRTAIT_NO",ChartererNo));
            Params.Add(new DBParameter("QTY",Qtty));
            Params.Add(new DBParameter("KZTANI_CD",KztaniCd));
            Params.Add(new DBParameter("USKJN_KBN",UskjnKbn));
            Params.Add(new DBParameter("KSN_KBN",KsnKbn));
            Params.Add(new DBParameter("FRTANKA",FrTanka));
            Params.Add(new DBParameter("GAKYEN",GakYen));
            Params.Add(new DBParameter("SYOHIZEI_KBN",SyohizeiKbn));
            Params.Add(new DBParameter("SYOHIZEI_GAK",SyohizeiGak));
            Params.Add(new DBParameter("TTLGAKU",TtlGaku));
            Params.Add(new DBParameter("K_KBN",KKbn));
            Params.Add(new DBParameter("CREATE_DATE",CreateDate));
            Params.Add(new DBParameter("TRKM_TNTSY_CD",TrkmTntsyCD));
            Params.Add(new DBParameter("TRKM_DATE",TrkmDate));
            Params.Add(new DBParameter("SYORI_STATUS",SyoriStatus));
            Params.Add(new DBParameter("STATUS_REMARK",StatusRemark));
            Params.Add(new DBParameter("EXT_SYSTEM_CD",ExtSystemCD));
            Params.Add(new DBParameter("UPDPGMID", UpdPgmID));
            Params.Add(new DBParameter("UPDUSERID",UpdUserID));
            Params.Add(new DBParameter("UPDDATE_YMDHMS", UpdateYMDHMS));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLAN_AMTIF), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("FUNE_NO", FuneNo));
            Params.Add(new DBParameter("JIKO_NO", JikoNo));
            Params.Add(new DBParameter("SC_YMD", ScYmd));
            Params.Add(new DBParameter("KOMA_NO", KomaNo));
            Params.Add(new DBParameter("LINE_NO", LineNo));
            Params.Add(new DBParameter("EXT_SYSTEM_CD", ExtSystemCD));

            using (DBConnect dbConnect = new DBConnect())
            {
                int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
                if (cnt == 0)
                {
                    return false;
                }
                return true;
            }
        }

    }
}
