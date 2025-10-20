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
    public class 発注RowData
    {
        #region データメンバ

        #region 手配から

        /// <summary>
        /// 手配 : ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_ID")]
        public string OD_THI_ID { get; set; }

        /// <summary>
        /// 手配 : 状況
        /// </summary>
        [DataMember]
        [ColumnAttribute("状況")]
        public string 状況 { get; set; }

        /// <summary>
        /// 手配 : 手配依頼種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("手配依頼種別")]
        public string 手配依頼種別 { get; set; }

        /// <summary>
        /// 手配 : 手配依頼詳細種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("手配依頼詳細種別")]
        public string 手配依頼詳細種別 { get; set; }

        /// <summary>
        /// 手配 : 手配依頼番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("手配依頼番号")]
        public string 手配依頼番号 { get; set; }
        
        /// <summary>
        /// 手配 : 船
        /// </summary>
        [DataMember]
        [ColumnAttribute("船")]
        public string 船 { get; set; }

        /// <summary>
        /// 手配 : 場所
        /// </summary>
        [DataMember]
        [ColumnAttribute("場所")]
        public string 場所 { get; set; }

        /// <summary>
        /// 手配 : 手配依頼者
        /// </summary>
        [DataMember]
        [ColumnAttribute("手配依頼者")]
        public string 手配依頼者 { get; set; }

        /// <summary>
        /// 手配 : 事務担当者
        /// </summary>
        [DataMember]
        [ColumnAttribute("事務担当者")]
        public string 事務担当者 { get; set; }

        /// <summary>
        /// 手配 : 手配依頼内容
        /// </summary>
        [DataMember]
        [ColumnAttribute("手配内容")]
        public string 手配内容 { get; set; }

        /// <summary>
        /// 手配 : 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("備考")]
        public string 備考 { get; set; }

        #endregion

        #region 見積依頼から

        /// <summary>
        /// 見積依頼 : ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_MM_ID")]
        public string OD_MM_ID { get; set; }

        /// <summary>
        /// 見積依頼 : 見積番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("見積番号")]
        public string 見積番号 { get; set; }

        /// <summary>
        /// 見積依頼 : 支払条件
        /// </summary>
        [DataMember]
        [ColumnAttribute("支払条件")]
        public string 支払条件 { get; set; }

        /// <summary>
        /// 見積依頼 : 入渠科目
        /// </summary>
        [DataMember]
        [ColumnAttribute("入渠科目")]
        public string 入渠科目 { get; set; }

        /// <summary>
        /// 見積依頼 : 送り先
        /// </summary>
        [DataMember]
        [ColumnAttribute("送り先")]
        public string 送り先 { get; set; }

        /// <summary>
        /// 見積依頼 : 内容
        /// </summary>
        [DataMember]
        [ColumnAttribute("内容")]
        public string 内容 { get; set; }

        #endregion

        #region 見積回答から

        /// <summary>
        /// 見積回答 : ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_MK_ID")]
        public string OD_MK_ID { get; set; }

        /// <summary>
        /// 見積回答 : 見積回答番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("見積回答番号")]
        public string 見積回答番号 { get; set; }

        /// <summary>
        /// 見積回答 : 見積依頼先
        /// </summary>
        [DataMember]
        [ColumnAttribute("見積依頼先")]
        public string 見積依頼先 { get; set; }

        /// <summary>
        /// 見積回答 : 担当者
        /// </summary>
        [DataMember]
        [ColumnAttribute("見積依頼先_担当者")]
        public string 見積依頼先_担当者 { get; set; }

        /// <summary>
        /// 見積回答 : メール送信状況
        /// </summary>
        [DataMember]
        [ColumnAttribute("メール送信状況")]
        public string メール送信状況 { get; set; }

        /// <summary>
        /// 見積回答 : 送信日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("送信日時")]
        public DateTime 送信日時 { get; set; }

        /// <summary>
        /// 見積回答 : 見積回答期限
        /// </summary>
        [DataMember]
        [ColumnAttribute("見積回答期限")]
        public string 見積回答期限 { get; set; }

        /// <summary>
        /// 見積回答 : 見積有効期限
        /// </summary>
        [DataMember]
        [ColumnAttribute("見積有効期限")]
        public string 見積有効期限 { get; set; }

        /// <summary>
        /// 見積回答 : 納期
        /// </summary>
        [DataMember]
        [ColumnAttribute("納期")]
        public DateTime 納期 { get; set; }

        /// <summary>
        /// 見積回答 : 工期
        /// </summary>
        [DataMember]
        [ColumnAttribute("工期")]
        public string 工期 { get; set; }


        /// <summary>
        /// 見積回答 : 見積回答日
        /// </summary>
        [DataMember]
        [ColumnAttribute("見積回答日")]
        public DateTime 見積回答日 { get; set; }
       
        /// <summary>
        /// 見積回答 : 見積_金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("見積_金額")]
        public decimal 見積_金額 { get; set; }

        /// <summary>
        /// 見積回答 : 見積_消費税
        /// </summary>
        [DataMember]
        [ColumnAttribute("見積_消費税")]
        public decimal 見積_消費税 { get; set; }

        /// <summary>
        /// 見積回答 : 見積_送料運搬料
        /// </summary>
        [DataMember]
        [ColumnAttribute("見積_送料運搬料")]
        public decimal 見積_送料運搬料 { get; set; }

        /// <summary>
        /// 見積回答 : 見積_値引
        /// </summary>
        [DataMember]
        [ColumnAttribute("見積_値引")]
        public decimal 見積_値引 { get; set; }

        /// <summary>
        /// 見積回答 : 見積_合計金額(税抜)
        /// </summary>
        [DataMember]
        [ColumnAttribute("見積_合計金額(税抜)")]
        public decimal 見積_合計金額 { get; set; }

        /// <summary>
        /// 見積回答 : 発注番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("発注番号")]
        public string 発注番号 { get; set; }

        /// <summary>
        /// 見積回答 : 発注日
        /// </summary>
        [DataMember]
        [ColumnAttribute("発注日")]
        public DateTime 発注日 { get; set; }

        #endregion

        #region 受領から

        /// <summary>
        /// 受領 : ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_ID")]
        public string OD_JRY_ID { get; set; }

        /// <summary>
        /// 受領 : ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("受領ステータス")]
        public int 受領ステータス { get; set; }
        
        /// <summary>
        /// 受領 : 受領番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("受領番号")]
        public string 受領番号 { get; set; }
        
        /// <summary>
        /// 受領 : 受領日
        /// </summary>
        [DataMember]
        [ColumnAttribute("受領日")]
        public DateTime 受領日 { get; set; }

        /// <summary>
        /// 受領 : 納品額
        /// </summary>
        [DataMember]
        [ColumnAttribute("納品額")]
        public decimal 納品額 { get; set; }

        /// <summary>
        /// 受領 : 納品_消費税
        /// </summary>
        [DataMember]
        [ColumnAttribute("納品_消費税")]
        public decimal 納品_消費税 { get; set; }

        /// <summary>
        /// 受領 : 納品_送料運搬料
        /// </summary>
        [DataMember]
        [ColumnAttribute("納品_送料運搬料")]
        public decimal 納品_送料運搬料 { get; set; }

        /// <summary>
        /// 受領 : 納品_値引
        /// </summary>
        [DataMember]
        [ColumnAttribute("納品_値引")]
        public decimal 納品_値引 { get; set; }

        /// <summary>
        /// 受領 : 納品_合計金額(税抜)
        /// </summary>
        [DataMember]
        [ColumnAttribute("納品_合計金額(税抜)")]
        public decimal 納品_合計金額 { get; set; }

        #endregion

        #region 落成から

        /// <summary>
        /// 落成 : 落成_合計金額(税抜)
        /// </summary>
        [DataMember]
        [ColumnAttribute("落成_合計金額(税抜)")]
        public decimal 落成_合計金額 { get; set; }

        #endregion

        #region 支払から

        /// <summary>
        /// 支払 : ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_SHR_ID")]
        public string OD_SHR_ID { get; set; }

        /// <summary>
        /// 支払 : ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("支払ステータス")]
        public int 支払ステータス { get; set; }

        /// <summary>
        /// 支払 : 支払番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("支払番号")]
        public string 支払番号 { get; set; }

        /// <summary>
        /// 支払 : 摘要
        /// </summary>
        [DataMember]
        [ColumnAttribute("摘要")]
        public string 摘要 { get; set; }

        /// <summary>
        /// 支払 : 申請担当者
        /// </summary>
        [DataMember]
        [ColumnAttribute("申請担当者")]
        public string 申請担当者 { get; set; }

        /// <summary>
        /// 支払 : 請求書日
        /// </summary>
        [DataMember]
        [ColumnAttribute("請求書日")]
        public DateTime 請求書日 { get; set; }

        /// <summary>
        /// 支払 : 起票日
        /// </summary>
        [DataMember]
        [ColumnAttribute("起票日")]
        public DateTime 起票日 { get; set; }

        /// <summary>
        /// 支払 : 計上日
        /// </summary>
        [DataMember]
        [ColumnAttribute("計上日")]
        public DateTime 計上日 { get; set; }

        /// <summary>
        /// 支払 : 支払日
        /// </summary>
        [DataMember]
        [ColumnAttribute("支払日")]
        public DateTime 支払日 { get; set; }

        /// <summary>
        /// 支払 : 基幹システム支払番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("基幹システム支払番号")]
        public string 基幹システム支払番号 { get; set; }

        /// <summary>
        /// 支払 : 支払額
        /// </summary>
        [DataMember]
        [ColumnAttribute("支払額")]
        public decimal 支払額 { get; set; }

        /// <summary>
        /// 支払 : 支払_消費税
        /// </summary>
        [DataMember]
        [ColumnAttribute("支払_消費税")]
        public decimal 支払_消費税 { get; set; }

        /// <summary>
        /// 支払 : 支払_送料運搬料
        /// </summary>
        [DataMember]
        [ColumnAttribute("支払_送料運搬料")]
        public decimal 支払_送料運搬料 { get; set; }

        /// <summary>
        /// 支払 : 請求値引
        /// </summary>
        [DataMember]
        [ColumnAttribute("請求値引")]
        public decimal 請求値引 { get; set; }

        /// <summary>
        /// 支払 : 支払_合計金額(税抜)
        /// </summary>
        [DataMember]
        [ColumnAttribute("支払_合計金額(税抜)")]
        public decimal 支払_合計金額 { get; set; }

        #endregion

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public 発注RowData()
        {
        }

        public static List<発注RowData> GetRecordsヘッダー(NBaseData.DAC.MsUser loginUser, 発注RowData検索条件 filter)
        {
            List<発注RowData> ret = new List<発注RowData>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(発注RowData), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            // 条件：船
            if (filter.MsVesselID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByMsVesselId");
                Params.Add(new DBParameter("MS_VESSEL_ID", filter.MsVesselID));
            }
            // 条件：取引先
            if (filter.MsCustomerID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByMsCustomerId");
                Params.Add(new DBParameter("MS_CUSTOMER_ID", filter.MsCustomerID));
            }
            // 条件：種別
            if (filter.MsThiIraiSbtID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiSbtID");
                Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", filter.MsThiIraiSbtID));
            }
            // 条件：詳細種別
            if (filter.MsThiIraiShousaiID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiShousaiID");
                Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", filter.MsThiIraiShousaiID));
            }
            // 条件：手配依頼日
            if (filter.ThiIraiDateFrom != null && filter.ThiIraiDateTo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiDateFromTo");
                Params.Add(new DBParameter("THI_IRAI_DATE_FROM", filter.ThiIraiDateFrom.ToString()));
                Params.Add(new DBParameter("THI_IRAI_DATE_TO", filter.ThiIraiDateTo.ToString()));
            }
            else if (filter.ThiIraiDateFrom != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiDateFrom");
                Params.Add(new DBParameter("THI_IRAI_DATE_FROM", filter.ThiIraiDateFrom.ToString()));
            }
            else if (filter.ThiIraiDateTo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiDateTo");
                Params.Add(new DBParameter("THI_IRAI_DATE_TO", filter.ThiIraiDateTo.ToString()));
            }
            // 条件：発注日
            if (filter.HachuDateFrom != null && filter.HachuDateTo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByHachuDateFromTo");
                Params.Add(new DBParameter("HACHU_DATE_FROM", filter.HachuDateFrom.ToString()));
                Params.Add(new DBParameter("HACHU_DATE_TO", filter.HachuDateTo.ToString()));
            }
            else if (filter.HachuDateFrom != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByHachuDateFrom");
                Params.Add(new DBParameter("HACHU_DATE_FROM", filter.HachuDateFrom.ToString()));
            }
            else if (filter.HachuDateTo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByHachuDateTo");
                Params.Add(new DBParameter("HACHU_DATE_TO", filter.HachuDateTo.ToString()));
            }
            // 条件：受領日
            if (filter.JryDateFrom != null && filter.JryDateTo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByJryDateFromTo");
                Params.Add(new DBParameter("JRY_DATE_FROM", filter.JryDateFrom.ToString()));
                Params.Add(new DBParameter("JRY_DATE_TO", filter.JryDateTo.ToString()));
            }
            else if (filter.JryDateFrom != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByJryDateFrom");
                Params.Add(new DBParameter("JRY_DATE_FROM", filter.JryDateFrom.ToString()));
            }
            else if (filter.JryDateTo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByJryDateTo");
                Params.Add(new DBParameter("JRY_DATE_TO", filter.JryDateTo.ToString()));
            }
            // 条件：支払日
            if (filter.ShrDateFrom != null && filter.ShrDateTo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByShrDateFromTo");
                Params.Add(new DBParameter("SHR_DATE_FROM", filter.ShrDateFrom.ToString()));
                Params.Add(new DBParameter("SHR_DATE_TO", filter.ShrDateTo.ToString()));
            }
            else if (filter.ShrDateFrom != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByShrDateFrom");
                Params.Add(new DBParameter("SHR_DATE_FROM", filter.ShrDateFrom.ToString()));
            }
            else if (filter.ShrDateTo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByShrDateTo");
                Params.Add(new DBParameter("SHR_DATE_TO", filter.ShrDateTo.ToString()));
            }
            // 条件：手配依頼番号
            if (filter.ThiIraiNoFrom != null && filter.ThiIraiNoFrom != ""
                && filter.ThiIraiNoTo != null && filter.ThiIraiNoTo != "")
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiNoFromTo");
                Params.Add(new DBParameter("TEHAI_IRAI_NO_FROM", filter.ThiIraiNoFrom));
                Params.Add(new DBParameter("TEHAI_IRAI_NO_TO", filter.ThiIraiNoTo));
            }
            else if (filter.ThiIraiNoFrom != null && filter.ThiIraiNoFrom != "")
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiNoFrom");
                Params.Add(new DBParameter("TEHAI_IRAI_NO_FROM", filter.ThiIraiNoFrom));
            }
            else if (filter.ThiIraiNoTo != null && filter.ThiIraiNoTo != "")
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiNoTo");
                Params.Add(new DBParameter("TEHAI_IRAI_NO_TO", filter.ThiIraiNoTo));
            }
            // 条件：状況
            if (filter.MsThiIraiStatusIDs.Count > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByMsThiIraiStatusID");
                string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("p", filter.MsThiIraiStatusIDs.Count);
                SQL = SQL.Replace("#INNER_SQL_MS_THI_IRAI_STATUS_IDS#", innerSQLStr);
                Params.AddInnerParams("p", filter.MsThiIraiStatusIDs.ToArray());
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByHeaderOrder");

System.Diagnostics.Debug.Write("SQL = " + SQL);
            MappingBase<発注RowData> mapping = new MappingBase<発注RowData>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<発注RowData> GetRecords詳細品目(NBaseData.DAC.MsUser loginUser, 発注RowData検索条件 filter)
        {
            List<発注RowData> ret = new List<発注RowData>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(発注RowData), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            // 条件：船
            if (filter.MsVesselID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByMsVesselId");
                Params.Add(new DBParameter("MS_VESSEL_ID", filter.MsVesselID));
            }
            // 条件：取引先
            if (filter.MsCustomerID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByMsCustomerId");
                Params.Add(new DBParameter("MS_CUSTOMER_ID", filter.MsCustomerID));
            }
            // 条件：種別
            if (filter.MsThiIraiSbtID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiSbtID");
                Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", filter.MsThiIraiSbtID));
            }
            // 条件：詳細種別
            if (filter.MsThiIraiShousaiID != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiShousaiID");
                Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", filter.MsThiIraiShousaiID));
            }
            // 条件：手配依頼日
            if (filter.ThiIraiDateFrom != null && filter.ThiIraiDateTo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiDateFromTo");
                Params.Add(new DBParameter("THI_IRAI_DATE_FROM", filter.ThiIraiDateFrom.ToString()));
                Params.Add(new DBParameter("THI_IRAI_DATE_TO", filter.ThiIraiDateTo.ToString()));
            }
            else if (filter.ThiIraiDateFrom != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiDateFrom");
                Params.Add(new DBParameter("THI_IRAI_DATE_FROM", filter.ThiIraiDateFrom.ToString()));
            }
            else if (filter.ThiIraiDateTo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiDateTo");
                Params.Add(new DBParameter("THI_IRAI_DATE_TO", filter.ThiIraiDateTo.ToString()));
            }
            // 条件：発注日
            if (filter.HachuDateFrom != null && filter.HachuDateTo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByHachuDateFromTo");
                Params.Add(new DBParameter("HACHU_DATE_FROM", filter.HachuDateFrom.ToString()));
                Params.Add(new DBParameter("HACHU_DATE_TO", filter.HachuDateTo.ToString()));
            }
            else if (filter.HachuDateFrom != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByHachuDateFrom");
                Params.Add(new DBParameter("HACHU_DATE_FROM", filter.HachuDateFrom.ToString()));
            }
            else if (filter.HachuDateTo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByHachuDateTo");
                Params.Add(new DBParameter("HACHU_DATE_TO", filter.HachuDateTo.ToString()));
            }
            // 条件：受領日
            if (filter.JryDateFrom != null && filter.JryDateTo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByJryDateFromTo");
                Params.Add(new DBParameter("JRY_DATE_FROM", filter.JryDateFrom.ToString()));
                Params.Add(new DBParameter("JRY_DATE_TO", filter.JryDateTo.ToString()));
            }
            else if (filter.JryDateFrom != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByJryDateFrom");
                Params.Add(new DBParameter("JRY_DATE_FROM", filter.JryDateFrom.ToString()));
            }
            else if (filter.JryDateTo != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByJryDateTo");
                Params.Add(new DBParameter("JRY_DATE_TO", filter.JryDateTo.ToString()));
            }
            // 条件：手配依頼番号
            if (filter.ThiIraiNoFrom != null && filter.ThiIraiNoFrom != ""
                && filter.ThiIraiNoTo != null && filter.ThiIraiNoTo != "")
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiNoFromTo");
                Params.Add(new DBParameter("TEHAI_IRAI_NO_FROM", filter.ThiIraiNoFrom));
                Params.Add(new DBParameter("TEHAI_IRAI_NO_TO", filter.ThiIraiNoTo));
            }
            else if (filter.ThiIraiNoFrom != null && filter.ThiIraiNoFrom != "")
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiNoFrom");
                Params.Add(new DBParameter("TEHAI_IRAI_NO_FROM", filter.ThiIraiNoFrom));
            }
            else if (filter.ThiIraiNoTo != null && filter.ThiIraiNoTo != "")
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByThiIraiNoTo");
                Params.Add(new DBParameter("TEHAI_IRAI_NO_TO", filter.ThiIraiNoTo));
            }
            // 条件：状況
            if (filter.MsThiIraiStatusIDs.Count > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByMsThiIraiStatusID");
                string innerSQLStr = SqlMapper.SqlMapper.CreateInnerSql("p", filter.MsThiIraiStatusIDs.Count);
                SQL = SQL.Replace("#INNER_SQL_MS_THI_IRAI_STATUS_IDS#", innerSQLStr);
                Params.AddInnerParams("p", filter.MsThiIraiStatusIDs.ToArray());
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(発注RowData), "FilterByHeaderOrder");

            MappingBase<発注RowData> mapping = new MappingBase<発注RowData>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
    }

    [DataContract()]
    public class 発注RowData検索条件
    {
        public enum 出力タイプEnum { ヘッダー, 詳細品目 };

        [DataMember]
        public 出力タイプEnum type;
        [DataMember]
        public int? MsVesselID { get; set; }
        [DataMember]
        public string MsCustomerID { get; set; }
        [DataMember]
        public string MsThiIraiSbtID { get; set; }
        [DataMember]
        public string MsThiIraiShousaiID { get; set; }
        [DataMember]
        public DateTime? ThiIraiDateFrom { get; set; }
        [DataMember]
        public DateTime? ThiIraiDateTo { get; set; }
        [DataMember]
        public DateTime? HachuDateFrom { get; set; }
        [DataMember]
        public DateTime? HachuDateTo { get; set; }
        [DataMember]
        public DateTime? JryDateFrom { get; set; }
        [DataMember]
        public DateTime? JryDateTo { get; set; }
        [DataMember]
        public DateTime? ShrDateFrom { get; set; }
        [DataMember]
        public DateTime? ShrDateTo { get; set; }
        [DataMember]
        public string ThiIraiNoFrom = "";
        [DataMember]
        public string ThiIraiNoTo = "";
        [DataMember]
        public List<string> MsThiIraiStatusIDs { get; set; }

        // チェック時のエラーメッセージ
        public string ErrMsg { get; set; }

        public 発注RowData検索条件()
        {
            MsVesselID = null;
            MsCustomerID = null;
            MsThiIraiSbtID = null;
            MsThiIraiShousaiID = null;
            ThiIraiDateFrom = null;
            ThiIraiDateTo = null;
            HachuDateFrom = null;
            HachuDateTo = null;
            JryDateFrom = null;
            JryDateTo = null;
            ShrDateFrom = null;
            ShrDateTo = null;
            ThiIraiNoFrom = "";
            ThiIraiNoTo = "";
            MsThiIraiStatusIDs = new List<string>();
        }

        public bool チェック()
        {
            if (ThiIraiDateFrom != null && ThiIraiDateTo != null && ThiIraiDateFrom > ThiIraiDateTo)
            {
                ErrMsg = "手配依頼日(from～to)が不正です。正しく入力してください。";
                return false;
            }
            if (HachuDateFrom != null && HachuDateTo != null && HachuDateFrom > HachuDateTo)
            {
                ErrMsg = "発注日(from～to)が不正です。正しく入力してください。";
                return false;
            }
            if (JryDateFrom != null && JryDateTo != null && JryDateFrom > JryDateTo)
            {
                ErrMsg = "受領日(from～to)が不正です。正しく入力してください。";
                return false;
            }
            if (ShrDateFrom != null && ShrDateTo != null && ShrDateFrom > ShrDateTo)
            {
                ErrMsg = "支払日(from～to)が不正です。正しく入力してください。";
                return false;
            } 
            if (MsThiIraiStatusIDs.Count == 0)
            {
                ErrMsg = "状況を１つ以上チェックしてください。";
                return false;
            }
            return true;
        }
        public bool 業者別支払実績出力時チェック()
        {
            if (HachuDateFrom != null && HachuDateTo != null && HachuDateFrom > HachuDateTo)
            {
                ErrMsg = "発注日(from～to)が不正です。正しく入力してください。";
                return false;
            }
            if (JryDateFrom != null && JryDateTo != null && JryDateFrom > JryDateTo)
            {
                ErrMsg = "受領日(from～to)が不正です。正しく入力してください。";
                return false;
            }
            if (ShrDateFrom != null && ShrDateTo != null && ShrDateFrom > ShrDateTo)
            {
                ErrMsg = "支払日(from～to)が不正です。正しく入力してください。";
                return false;
            }
            return true;
        }
    }

    [DataContract()]
    public class 発注RowData詳細Base : Object
    {
        #region データメンバ

        /// <summary>
        /// 部署名
        /// </summary>
        [DataMember]
        [ColumnAttribute("HEADER")]
        public string Header { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_ITEM_SBT_NAME")]
        public string MsItemSbtName { get; set; }

        /// <summary>
        /// 仕様・型式
        /// </summary>
        [DataMember]
        [ColumnAttribute("ITEM_NAME")]
        public string ItemName { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("ITEM_BIKOU")]
        public string ItemBikou { get; set; }

        /// <summary>
        /// 詳細品目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOUSAI_ITEM_NAME")]
        public string ShousaiItemName { get; set; }

        /// <summary>
        /// 船用品ID >> 船用品名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ITEM_NAME")]
        public string MsVesselItemName { get; set; }

        /// <summary>
        /// 潤滑油ID >> 潤滑油名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_LO_NAME")]
        public string MsLoName { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOUSAI_BIKOU")]
        public string ShousaiBikou { get; set; }

        /// <summary>
        /// 単位ID >> 単位名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TANI_NAME")]
        public string MsTaniName { get; set; }

        #endregion

        public override int GetHashCode()
        {
            int hash = this.Header.GetHashCode();
            hash += this.MsItemSbtName.GetHashCode();
            hash += this.ItemName.GetHashCode();
            if (this.ShousaiItemName == null)
                this.ShousaiItemName = "";
            hash += this.ShousaiItemName.GetHashCode();
            hash += this.ShousaiBikou.GetHashCode();
            return hash;
        }
        public override bool Equals(object obj)
        {
            発注RowData詳細Base syousai = obj as 発注RowData詳細Base;

            if (this.Header == syousai.Header &&
                this.MsItemSbtName == syousai.MsItemSbtName &&
                this.ItemName == syousai.ItemName &&
                this.ShousaiItemName == syousai.ShousaiItemName &&
                this.ShousaiBikou == syousai.ShousaiBikou)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
   
    [DataContract()]
    public class 発注RowData詳細 : 発注RowData詳細Base
    {
        #region データメンバ

        /// <summary>
        /// 在庫数
        /// </summary>
        [DataMember]
        public int ZaikoCount { get; set; }

        /// <summary>
        /// 査定数
        /// </summary>
        [DataMember]
        public int ThiCount { get; set; }

        /// <summary>
        /// 見積数量
        /// </summary>
        [DataMember]
        public int MmCount { get; set; }

        /// <summary>
        /// 回答数量
        /// </summary>
        [DataMember]
        public int MkCount { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        [DataMember]
        public decimal MkTanka { get; set; }

        /// <summary>
        /// 見積金額
        /// </summary>
        [DataMember]
        public decimal MkAmount { get; set; }

        /// <summary>
        /// 発注日
        /// </summary>
        [DataMember]
        public DateTime HachuDate { get; set; }

        /// <summary>
        /// 受領日
        /// </summary>
        [DataMember]
        public DateTime JryDate { get; set; }

        /// <summary>
        /// 受領数量
        /// </summary>
        [DataMember]
        public int JryCount { get; set; }

        /// <summary>
        /// 受領金額
        /// </summary>
        [DataMember]
        public decimal JryAmount { get; set; }

        /// <summary>
        /// 支払数量
        /// </summary>
        [DataMember]
        public int ShrCount { get; set; }

        /// <summary>
        /// 支払金額
        /// </summary>
        [DataMember]
        public decimal ShrAmount { get; set; }

        #endregion

        private void Set詳細Base(発注RowData詳細Base syousai)
        {
            if (this.Header == null || this.Header.Length == 0)
            {
                this.Header = syousai.Header;
            }
            if (this.MsItemSbtName == null || this.MsItemSbtName.Length == 0)
            {
                this.MsItemSbtName = syousai.MsItemSbtName;
            }
            if (this.ItemName == null || this.ItemName.Length == 0)
            {
                this.ItemName = syousai.ItemName;
            }
            if (this.ShousaiItemName == null || this.ShousaiItemName.Length == 0)
            {
                if (syousai.ShousaiItemName.Length > 0)
                {
                    this.ShousaiItemName = syousai.ShousaiItemName;
                }
                else if (syousai.MsVesselItemName.Length > 0)
                {
                    this.ShousaiItemName = syousai.MsVesselItemName;
                }
                else if (syousai.MsLoName.Length > 0)
                {
                    this.ShousaiItemName = syousai.MsLoName;
                }
            }
            if (this.ShousaiBikou == null || this.ShousaiBikou.Length == 0)
            {
                this.ShousaiBikou = syousai.ShousaiBikou;
            }
            if (this.MsTaniName == null || this.MsTaniName.Length == 0)
            {
                this.MsTaniName = syousai.MsTaniName;
            }
        }

        public void Set手配詳細(発注RowData手配詳細 syousai)
        {
            Set詳細Base(syousai);
            this.ZaikoCount = syousai.ZaikoCount;
            this.ThiCount = syousai.Sateisu;
        }

        public void Set見積詳細(発注RowData見積詳細 syousai)
        {
            Set詳細Base(syousai);
            this.MmCount = syousai.Count;
        }

        public void Set回答詳細(発注RowData回答詳細 syousai)
        {
            Set詳細Base(syousai);
            if (syousai.Count > 0)
            {
                this.MkCount = syousai.Count;
            }
            if (syousai.Tanka > 0)
            {
                this.MkTanka = syousai.Tanka;
            }
            this.MkAmount = syousai.Count * syousai.Tanka;
        }

        public void Set受領詳細(発注RowData受領詳細 syousai)
        {
            Set詳細Base(syousai);
            if (syousai.Count > 0)
            {
                this.JryCount = syousai.Count;
                this.JryAmount = syousai.Count * syousai.Tanka;
            }
        }

        public void Set支払詳細(発注RowData支払詳細 syousai)
        {
            Set詳細Base(syousai);
            if (syousai.Count > 0)
            {
                this.ShrCount = syousai.Count;
                this.ShrAmount = syousai.Count * syousai.Tanka;
            }
        }
    }

    [DataContract()]
    public class 発注RowData手配詳細 : 発注RowData詳細Base
    {
        #region データメンバ

        /// <summary>
        /// 在庫数
        /// </summary>
        [DataMember]
        [ColumnAttribute("ZAIKO_COUNT")]
        public int ZaikoCount { get; set; }

        /// <summary>
        /// 査定数
        /// </summary>
        [DataMember]
        [ColumnAttribute("SATEISU")]
        public int Sateisu { get; set; }

        #endregion

        public static List<発注RowData手配詳細> GetRecords手配詳細(NBaseData.DAC.MsUser loginUser, string odThiId)
        {
            List<発注RowData手配詳細> ret = new List<発注RowData手配詳細>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(発注RowData), MethodBase.GetCurrentMethod());
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_THI_ID", odThiId));

            MappingBase<発注RowData手配詳細> mapping = new MappingBase<発注RowData手配詳細>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }
    }

    [DataContract()]
    public class 発注RowData見積詳細 : 発注RowData詳細Base
    {
        #region データメンバ

        /// <summary>
        /// 見積数
        /// </summary>
        [DataMember]
        [ColumnAttribute("COUNT")]
        public int Count { get; set; }

        #endregion

        public static List<発注RowData見積詳細> GetRecords見積詳細(NBaseData.DAC.MsUser loginUser, string odMmId)
        {
            List<発注RowData見積詳細> ret = new List<発注RowData見積詳細>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(発注RowData), MethodBase.GetCurrentMethod());
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MM_ID", odMmId));

            MappingBase<発注RowData見積詳細> mapping = new MappingBase<発注RowData見積詳細>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }
    }

    [DataContract()]
    public class 発注RowData回答詳細 : 発注RowData詳細Base
    {
        #region データメンバ

        /// <summary>
        /// 回答数
        /// </summary>
        [DataMember]
        [ColumnAttribute("COUNT")]
        public int Count { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANKA")]
        public decimal Tanka { get; set; }

        #endregion

        public static List<発注RowData回答詳細> GetRecords回答詳細(NBaseData.DAC.MsUser loginUser, string odMkId)
        {
            List<発注RowData回答詳細> ret = new List<発注RowData回答詳細>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(発注RowData), MethodBase.GetCurrentMethod());
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_MK_ID", odMkId));

            MappingBase<発注RowData回答詳細> mapping = new MappingBase<発注RowData回答詳細>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }
    }

    [DataContract()]
    public class 発注RowData受領詳細 : 発注RowData詳細Base
    {
        #region データメンバ

        /// <summary>
        /// 受領数
        /// </summary>
        [DataMember]
        [ColumnAttribute("JRY_COUNT")]
        public int Count { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANKA")]
        public decimal Tanka { get; set; }

        #endregion

        public static List<発注RowData受領詳細> GetRecords受領詳細(NBaseData.DAC.MsUser loginUser, string odJryId)
        {
            List<発注RowData受領詳細> ret = new List<発注RowData受領詳細>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(発注RowData), MethodBase.GetCurrentMethod());
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_JRY_ID", odJryId));

            MappingBase<発注RowData受領詳細> mapping = new MappingBase<発注RowData受領詳細>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }
    }

    [DataContract()]
    public class 発注RowData支払詳細 : 発注RowData詳細Base
    {
        #region データメンバ

        /// <summary>
        /// 支払数
        /// </summary>
        [DataMember]
        [ColumnAttribute("COUNT")]
        public int Count { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANKA")]
        public decimal Tanka { get; set; }

        #endregion

        public static List<発注RowData支払詳細> GetRecords支払詳細(NBaseData.DAC.MsUser loginUser, string odShrId)
        {
            List<発注RowData支払詳細> ret = new List<発注RowData支払詳細>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(発注RowData), MethodBase.GetCurrentMethod());
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_SHR_ID", odShrId));

            MappingBase<発注RowData支払詳細> mapping = new MappingBase<発注RowData支払詳細>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }
    }
}
