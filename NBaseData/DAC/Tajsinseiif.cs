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
using NBaseData.DS;
using ORMapping.Atts;
using NBaseData.BLC;
//using Oracle.DataAccess.Client;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("TAJSINSEIIF")]
    public class Tajsinseiif
    {
        #region データメンバ

        /// <summary>
        /// 集約管理番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("SUM_KANRI_NO", true)]
        public string SumKanriNo { get; set; }

        /// <summary>
        /// 申請識別
        /// </summary>
        [DataMember]
        [ColumnAttribute("SINSEI_KANRI_NO", true)]
        public string SinseiKanriNo { get; set; }

        /// <summary>
        /// サブシステムコード
        /// </summary>
        [DataMember]    
        [ColumnAttribute("SUBSYSTEM_CD", true)]
        public string SubsystemCd { get; set; }

        /// <summary>
        /// 業務コード
        /// </summary>
        [DataMember]          
        [ColumnAttribute("GYOMU_CD", true)]
        public string GyomuCd { get; set; }

        /// <summary>
        /// 取引コード
        /// </summary>
        [DataMember]             
        [ColumnAttribute("TRHK_CD", true)]
        public string TrhkCd { get; set; }

        /// <summary>
        /// 処理コード
        /// </summary>
        [DataMember]               
        [ColumnAttribute("SYORI_CD", true)]
        public string SyoriCd { get; set; }

        /// <summary>
        /// 運用会社コード
        /// </summary>
        [DataMember]             
        [ColumnAttribute("KISY_CD", true)]
        public string KisyCd { get; set; }

        /// <summary>
        /// 作成担当コード
        /// </summary>
        [DataMember]               
        [ColumnAttribute("DATAMK_TNTSY_CD", true)]
        public string DatamkTntsyCd { get; set; }

        /// <summary>
        /// 船舶NO
        /// </summary>
        [DataMember]       
        [ColumnAttribute("FUNE_NO", true)]
        public string FuneNo { get; set; }

        /// <summary>
        /// 申請担当者コード
        /// </summary>
        [DataMember]               
        [ColumnAttribute("TNTSY_CD", true)]
        public string TnsyCd { get; set; }

        /// <summary>
        /// 立替先コード
        /// </summary>
        [DataMember]              
        [ColumnAttribute("TATESK_CD", true)]
        public string TateskCd { get; set; }

        /// <summary>
        /// 立替先担当者コード
        /// </summary>
        [DataMember]
        [ColumnAttribute("TATESK_TNTSY_CD", true)]
        public string TateskTntsyCd { get; set; }

        /// <summary>
        /// 計上日付
        /// </summary>
        [DataMember]       
        [ColumnAttribute("KEIJO_YMD", true)]
        public DateTime KeijoYmd { get; set; }

        /// <summary>
        /// 入出金予定年月日
        /// </summary>
        [DataMember]             
        [ColumnAttribute("KIN_YOTEI_YMD", true)]
        public DateTime KinYoteiYmd { get; set; }

        /// <summary>
        /// 取引先コード
        /// </summary>
        [DataMember]         
        [ColumnAttribute("TRHKSK_CD", true)]
        public string TrhkskCd { get; set; }

        /// <summary>
        /// 取引先担当者コード
        /// </summary>
        [DataMember]             
        [ColumnAttribute("TRHKSK_TNTSY_CD", true)]
        public string TrhkskTntsyCd { get; set; }

        /// <summary>
        /// 通貨コード
        /// </summary>
        [DataMember]       
        [ColumnAttribute("TUKA_CD", true)]
        public string TukaCd { get; set; }

        /// <summary>
        /// 基本摘要
        /// </summary>
        [DataMember]               
        [ColumnAttribute("KIHON_TEKIYO", true)]
        public string KihonTekiyo { get; set; }

        /// <summary>
        /// 請求日付
        /// </summary>
        [DataMember]          
        [ColumnAttribute("SEIKYU_YMD", true)]
        public DateTime SeikyuYmd { get; set; }

        /// <summary>
        /// 承認パターンコード
        /// </summary>
        [DataMember]            
        [ColumnAttribute("SYONIN_PATTERN_CD", true)]
        public string SyoninPatternCd { get; set; }

        /// <summary>
        /// 行番号
        /// </summary>
        [DataMember]     
        [ColumnAttribute("LINE_NO", true)]
        public string LineNo { get; set; }

        /// <summary>
        /// 契約番号
        /// </summary>
        [DataMember]               
        [ColumnAttribute("KEIYAKU_NO", true)]
        public string KeiyakuNo { get; set; }

        /// <summary>
        /// ファンド区分
        /// </summary>
        [DataMember]            
        [ColumnAttribute("FUND_KBN", true)]
        public string FundKbn { get; set; }

        /// <summary>
        /// 費目区分
        /// </summary>
        [DataMember]              
        [ColumnAttribute("HIMOKU_KBN", true)]
        public string HimokuKbn { get; set; }

        /// <summary>
        /// 勘定科目
        /// </summary>
        [DataMember]            
        [ColumnAttribute("KANJO_KMK_CD", true)]
        public string KanjoKmkCd { get; set; }

        /// <summary>
        /// 内訳科目
        /// </summary>
        [DataMember]          
        [ColumnAttribute("UTIWAKE_KMK_CD", true)]
        public string UtiwakeKmkCd { get; set; }

        /// <summary>
        /// 相手勘定科目
        /// </summary>
        [DataMember]
        [ColumnAttribute("AITE_KANJO_KMK_CD", true)]
        public string AiteKanjoKmkCd { get; set; }

        /// <summary>
        /// 相手内訳科目
        /// </summary>
        [DataMember]     
        [ColumnAttribute("AITE_UTIWAKE_KMK_CD", true)]
        public string AiteUtiwakeKmkCd { get; set; }

        /// <summary>
        /// 消費税コード
        /// </summary>
        [DataMember]   
        [ColumnAttribute("SYHZ_CD", true)]
        public string SyhzCd { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]               
        [ColumnAttribute("GAK", true)]
        public decimal Gaku { get; set; }

        /// <summary>
        /// 消費税額
        /// </summary>
        [DataMember]                   
        [ColumnAttribute("SYOHIZEI_GAK", true)]
        public decimal SyohizeiGaku { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]          
        [ColumnAttribute("MEISAI_TEKIYO", true)]
        public string MeisaiTekiyo { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]         
        [ColumnAttribute("SURYO", true)]
        public double Suryo { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        [DataMember]                 
        [ColumnAttribute("TANKA", true)]
        public decimal Tanka { get; set; }

        /// <summary>
        /// 単位
        /// </summary>
        [DataMember]                 
        [ColumnAttribute("TANI", true)]
        public string Tani { get; set; }

        /// <summary>
        /// 発生期間（開始）
        /// </summary>
        [DataMember]                  
        [ColumnAttribute("HSI_KIKAN_FROM", true)]
        public string HsiKikanFrom { get; set; }

        /// <summary>
        /// 発生期間（終了）
        /// </summary>
        [DataMember]        
        [ColumnAttribute("HSI_KIKAN_TO", true)]
        public string HsiKikanTo { get; set; }

        /// <summary>
        /// 作成日時
        /// </summary>
        [DataMember]          
        [ColumnAttribute("CREATE_DATE", true)]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 取込ユーザー
        /// </summary>
        [DataMember]           
        [ColumnAttribute("TRKM_TNTSY_CD", true)]
        public string TrkmTntsyCd { get; set; }

        /// <summary>
        /// 取込日時
        /// </summary>
        [DataMember]         
        [ColumnAttribute("TRKM_DATE", true)]
        public DateTime TrkmDate { get; set; }

        /// <summary>
        /// 処理番号
        /// </summary>
        [DataMember]             
        [ColumnAttribute("OPE_NO", true)]
        public decimal OpeNo { get; set; }

        /// <summary>
        /// 基幹申請会社コード
        /// </summary>
        [DataMember]                
        [ColumnAttribute("KIKAN_KISY_CD", true)]
        public string KikanKisyCd { get; set; }

        /// <summary>
        /// 基幹申請番号
        /// </summary>
        [DataMember]         
        [ColumnAttribute("KIKAN_SINSEI_NO", true)]
        public string KikanSinseiNo { get; set; }

        /// <summary>
        /// 基幹明細番号
        /// </summary>
        [DataMember]       
        [ColumnAttribute("KIKAN_MEISAI_NO", true)]
        public string KikanMeisaiNo { get; set; }

        /// <summary>
        /// 状態
        /// </summary>
        [DataMember]       
        [ColumnAttribute("SYORI_STATUS", true)]
        public string SyoriStatus { get; set; }

        /// <summary>
        /// 状態備考
        /// </summary>
        [DataMember]          
        [ColumnAttribute("STATUS_REMARK", true)]
        public string StatusRemark { get; set; }

        /// <summary>
        /// 外部システム識別コード
        /// </summary>
        [DataMember]         
        [ColumnAttribute("EXT_SYSTEM_CD", true)]
        public string ExtSystemCd { get; set; }

        /// <summary>
        /// 更新プログラムID
        /// </summary>
        [DataMember]       
        [ColumnAttribute("UPDPGMID", true)]
        public string Updpgmid { get; set; }

        /// <summary>
        /// 更新ユーザーID
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDUSERID", true)]
        public string Upduserid { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        [DataMember]             
        [ColumnAttribute("UPDDATE_YMDHMS", true)]
        public Int64 UpddateYmdhms { get; set; }



        /// <summary>
        /// 申請担当者のログインID
        /// </summary>
        [DataMember]
        public string TnsyLoginId { get; set; }

        #endregion

        public string 支払_サブシステムコード = "CP";
        public string 支払_業務コード = "PYR";
        public string 支払_取引コード = "01";
        public string 支払_処理コード = "010";
        public string 支払_運用会社コード = "180";
        public string 支払_通貨コード = "057";
        public string 支払_承認パターンコード = "00001";
        public string 支払_消費税コード = "40001";
        public string 支払_外部システム識別コード = "WING";
        public string 支払_更新プログラムID = "WING";

        public string 概算_サブシステムコード = "FC";
        public string 概算_業務コード = "FTR";
        public string 概算_取引コード = "01";
        public string 概算_発注_処理コード = "900";
        public string 概算_貯蔵品_処理コード = "900";
        public string 概算_船内収支_処理コード = "010";
        public string 概算_運用会社コード = "180";
        public string 概算_通貨コード = "057";
        public string 概算_取引先コード = "9990000";
        public string 概算_承認パターンコード = "00001";
public string 概算_消費税コード_外税 = "10102";
        public string 概算_消費税コード_対象外 = "40001";
        public string 概算_消費税コード_免税 = "30001";

        public string 棚卸_勘定科目コード = "12701";
        public string 棚卸_内訳科目コード_潤滑油 = "12701020";
        public string 棚卸_内訳科目コード_船用品 = "12701030";
        public string 棚卸_相手勘定科目コード_潤滑油 = "80635";
        public string 棚卸_相手勘定科目コード_船用品 = "80630";

        public string 概算_外部システム識別コード = "WING";
        public string 概算_更新プログラムID = "WING";

        public enum SYORI_STATUS
        {
            作成済み = 0,
            取込済み = 10,
            項目エラー = 90,
            マスタエラー = 91,
            締めエラー = 92,
            申請済みエラー = 93,
            その他エラー = 99
        };

        //public static string SP_申請データ取込CMD = "KYDTI01010";
        public static string SP_申請データ取込CMD = "CALL_KYDTI01010";
        public static string SP_ERROR_入力パラメータエラー = "3";


        public static string 全社共通船コード = "ZZZZ";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Tajsinseiif()
        {
            SyohizeiGaku = decimal.MinValue;
            Suryo = double.MinValue;
            Tanka = decimal.MinValue;
            OpeNo = decimal.MinValue;
        }

        public static List<Tajsinseiif> GetRecords(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(Tajsinseiif), MethodBase.GetCurrentMethod());
            List<Tajsinseiif> ret = new List<Tajsinseiif>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<Tajsinseiif> mapping = new MappingBase<Tajsinseiif>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static Tajsinseiif GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, 
            string SumKanriNo, string SinseiKanriNo, string SubsystemCd, string GyomuCd, 
            string TrhkCd, string SyoriCd, string LineNo, string ExtSystemCd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(Tajsinseiif), MethodBase.GetCurrentMethod());

            List<Tajsinseiif> ret = new List<Tajsinseiif>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<Tajsinseiif> mapping = new MappingBase<Tajsinseiif>();
            Params.Add(new DBParameter("SUM_KANRI_NO", SumKanriNo));    
            Params.Add(new DBParameter("SINSEI_KANRI_NO", SinseiKanriNo));
            Params.Add(new DBParameter("SUBSYSTEM_CD", SubsystemCd));
            Params.Add(new DBParameter("GYOMU_CD", GyomuCd));
            Params.Add(new DBParameter("TRHK_CD", TrhkCd));
            Params.Add(new DBParameter("SYORI_CD", SyoriCd));  
            Params.Add(new DBParameter("LINE_NO", LineNo));
            Params.Add(new DBParameter("EXT_SYSTEM_CD", ExtSystemCd));

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static string GetSumKanriNo(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string SumKanriNo)
        {
            string  ret = "";
            string strSeq = "0001";

            GetMaxNo getMaxNo = GetMaxNo.GetMaxSumKanriNo(dbConnect, loginUser, 9, SumKanriNo);
            if (getMaxNo.MaxNo != "")
            {
                strSeq = getMaxNo.MaxNo.Substring(9, 4);
                int iSeq = int.Parse(strSeq);
                strSeq = String.Format("{0:0000}", (iSeq + 1));
            }
    
            ret = SumKanriNo + strSeq;

            return ret;
        }

        //public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        //{
        //    return InsertRecord(null, loginUser);
        //}

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(Tajsinseiif), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SUM_KANRI_NO", SumKanriNo));
            Params.Add(new DBParameter("SINSEI_KANRI_NO", SinseiKanriNo));
            Params.Add(new DBParameter("SUBSYSTEM_CD", SubsystemCd));
            Params.Add(new DBParameter("GYOMU_CD", GyomuCd));
            Params.Add(new DBParameter("TRHK_CD", TrhkCd));
            Params.Add(new DBParameter("SYORI_CD", SyoriCd));
            Params.Add(new DBParameter("KISY_CD", KisyCd));
            Params.Add(new DBParameter("DATAMK_TNTSY_CD", DatamkTntsyCd));
            Params.Add(new DBParameter("FUNE_NO", FuneNo));
            Params.Add(new DBParameter("TNTSY_CD", TnsyCd));
            Params.Add(new DBParameter("TATESK_CD", TateskCd));
            Params.Add(new DBParameter("TATESK_TNTSY_CD", TateskTntsyCd));
            Params.Add(new DBParameter("KEIJO_YMD", KeijoYmd));
            Params.Add(new DBParameter("KIN_YOTEI_YMD", KinYoteiYmd));
            Params.Add(new DBParameter("TRHKSK_CD", TrhkskCd));
            Params.Add(new DBParameter("TRHKSK_TNTSY_CD", TrhkskTntsyCd));
            Params.Add(new DBParameter("TUKA_CD", TukaCd));
            //Params.Add(new DBParameter("KIHON_TEKIYO", KihonTekiyo)); // I/Fにたいしてパラメータ渡しではORA-01461が発生する
            if (KihonTekiyo == null || KihonTekiyo.Length == 0)
            {
                SQL = SQL.Replace(":KIHON_TEKIYO", "null");
            }
            else
            {
                SQL = SQL.Replace(":KIHON_TEKIYO", "'" + KihonTekiyo + "'");
            }
            Params.Add(new DBParameter("SEIKYU_YMD", SeikyuYmd));
            Params.Add(new DBParameter("SYONIN_PATTERN_CD", SyoninPatternCd));
            Params.Add(new DBParameter("LINE_NO", LineNo));
            Params.Add(new DBParameter("KEIYAKU_NO", KeiyakuNo));
            Params.Add(new DBParameter("FUND_KBN", FundKbn));
            Params.Add(new DBParameter("HIMOKU_KBN", HimokuKbn));
            Params.Add(new DBParameter("KANJO_KMK_CD", KanjoKmkCd));
            Params.Add(new DBParameter("UTIWAKE_KMK_CD", UtiwakeKmkCd));
            Params.Add(new DBParameter("AITE_KANJO_KMK_CD", AiteKanjoKmkCd));
            Params.Add(new DBParameter("AITE_UTIWAKE_KMK_CD", AiteUtiwakeKmkCd));
            Params.Add(new DBParameter("SYHZ_CD", SyhzCd));
            Params.Add(new DBParameter("GAK", Gaku));
            if (SyohizeiGaku == decimal.MinValue)
            {
                Params.Add(new DBParameter("SYOHIZEI_GAK", DBNull.Value));
            }
            else
            {
                Params.Add(new DBParameter("SYOHIZEI_GAK", SyohizeiGaku));
            }
            //Params.Add(new DBParameter("MEISAI_TEKIYO", MeisaiTekiyo)); // I/Fにたいしてパラメータ渡しではORA-01461が発生する
            if (MeisaiTekiyo == null || MeisaiTekiyo.Length == 0)
            {
                SQL = SQL.Replace(":MEISAI_TEKIYO", "null");
            }
            else
            {
                SQL = SQL.Replace(":MEISAI_TEKIYO", "'" + MeisaiTekiyo + "'");
            }
            if (Suryo == double.MinValue)
            {
                Params.Add(new DBParameter("SURYO", DBNull.Value));
            }
            else
            {
                Params.Add(new DBParameter("SURYO", Suryo));
            }
            if (Tanka == decimal.MinValue)
            {
                Params.Add(new DBParameter("TANKA", DBNull.Value));
            }
            else
            {
                Params.Add(new DBParameter("TANKA", Tanka));
            }
            Params.Add(new DBParameter("TANI", Tani));
            Params.Add(new DBParameter("HSI_KIKAN_FROM", HsiKikanFrom));
            Params.Add(new DBParameter("HSI_KIKAN_TO", HsiKikanTo));
            Params.Add(new DBParameter("CREATE_DATE", CreateDate));
            Params.Add(new DBParameter("TRKM_TNTSY_CD", TrkmTntsyCd));
            Params.Add(new DBParameter("TRKM_DATE", TrkmDate));
            if (OpeNo == decimal.MinValue)
            {
                Params.Add(new DBParameter("OPE_NO", DBNull.Value));
            }
            else
            {
                Params.Add(new DBParameter("OPE_NO", OpeNo));
            }
            Params.Add(new DBParameter("KIKAN_KISY_CD", KikanKisyCd));
            Params.Add(new DBParameter("KIKAN_SINSEI_NO", KikanSinseiNo));
            Params.Add(new DBParameter("KIKAN_MEISAI_NO", KikanMeisaiNo));
            Params.Add(new DBParameter("SYORI_STATUS", SyoriStatus));
            Params.Add(new DBParameter("STATUS_REMARK", StatusRemark));
            Params.Add(new DBParameter("EXT_SYSTEM_CD", ExtSystemCd));
            Params.Add(new DBParameter("UPDPGMID", Updpgmid));
            Params.Add(new DBParameter("UPDUSERID", Upduserid));
            Params.Add(new DBParameter("UPDDATE_YMDHMS", UpddateYmdhms));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        public static string CallSP_支払い(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, Tajsinseiif tajsinseiif)
        {
            string ret = "";

            //MsUser tntsy = MsUser.GetRecordsByUserID(loginUser, tajsinseiif.TnsyCd);
            //if (tntsy == null)
            //{
            //    // 支払いの申請担当者が取得できない場合
            //    // SPのエラーの入力パラメータエラーとする
            //    return Tajsinseiif.SP_ERROR_入力パラメータエラー;
            //}
            if (tajsinseiif.TnsyLoginId == null)
            {
                // 支払いの申請担当者が取得できない場合
                // SPのエラーの入力パラメータエラーとする
                return Tajsinseiif.SP_ERROR_入力パラメータエラー;
            }


            //string SQL = "KYDTI01010";
            string SQL = Tajsinseiif.SP_申請データ取込CMD;

            //using (OracleCommand cmd = new OracleCommand(SQL, dbConnect.OracleConnection))
            //{
            //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //    cmd.Parameters.Add(new OracleParameter("p_KISY_CD", OracleDbType.Varchar2, 20, System.Data.ParameterDirection.Input));
            //    cmd.Parameters.Add(new OracleParameter("p_TNTSY_CD", OracleDbType.Varchar2, 20, System.Data.ParameterDirection.Input));
            //    cmd.Parameters.Add(new OracleParameter("p_UPDUSERID", OracleDbType.Varchar2, 20, System.Data.ParameterDirection.Input));
            //    cmd.Parameters.Add(new OracleParameter("ret", OracleDbType.Varchar2, 20, "", System.Data.ParameterDirection.Output));

            //    // 2009.10.28:aki ハードコーディングされているので、修正します
            //    //cmd.Parameters["p_KISY_CD"].Value = "180";
            //    //cmd.Parameters["p_TNTSY_CD"].Value = "990389";
            //    //cmd.Parameters["p_UPDUSERID"].Value = "horiok-y3";
            //    cmd.Parameters["p_KISY_CD"].Value = tajsinseiif.KisyCd;
            //    cmd.Parameters["p_TNTSY_CD"].Value = tajsinseiif.TnsyCd;
            //    cmd.Parameters["p_UPDUSERID"].Value = tntsy.LoginID;

            //    cmd.ExecuteNonQuery();

            //    ret = cmd.Parameters["ret"].Value.ToString();
            //}
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("p_KISY_CD", tajsinseiif.KisyCd));
            Params.Add(new DBParameter("p_TNTSY_CD", tajsinseiif.TnsyCd));
            Params.Add(new DBParameter("p_UPDUSERID", tajsinseiif.TnsyLoginId));

            ret = DBConnect.ExecuteProcedure(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

    }
}
