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

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("OD_SHR_GASSAN_ITEM")]
    public class OdShrGassanItem
    {
        #region データメンバ

        /// <summary>
        /// 支払合算項目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_SHR_GASSAN_ITEM_ID")]
        public string OdShrGassanItemID { get; set; }

        /// <summary>
        /// 支払合算ヘッダID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_SHR_GASSAN_HEAD_ID")]
        public string OdShrGassanHeadID { get; set; }

        /// <summary>
        /// 受領ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_ID")]
        public string OdJryID { get; set; }

        #region ＤＢ共通項目

        /// <summary>
        /// 同期:送信フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("SEND_FLAG")]
        public int SendFlag { get; set; }

        /// <summary>
        /// 同期:船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_ID")]
        public int VesselID { get; set; }

        /// <summary>
        /// 同期:データNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }

        /// <summary>
        /// 同期:ユーザキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_KEY")]
        public string UserKey { get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_DATE")]
        public DateTime RenewDate { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_USER_ID")]
        public string RenewUserID { get; set; }

        /// <summary>
        /// 排他制御
        /// </summary>
        [DataMember]
        [ColumnAttribute("TS")]
        public string Ts { get; set; }

        #endregion


        //=================================
        // 受領から
        //=================================

        /// <summary>
        /// 受領日
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_JRY_DATE")]
        public DateTime JryDate { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_AMOUNT")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 値引き
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_NEBIKI_AMOUNT")]
        public decimal NebikiAmount { get; set; }

        /// <summary>
        /// 消費税額
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_TAX")]
        public decimal Tax { get; set; }

        /// <summary>
        /// 科目NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("KAMOKU_NO")]
        public string KamokuNo { get; set; }

        /// <summary>
        /// 科目NO　>> 科目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("KAMOKU_NAME")]
        public string KamokuName { get; set; }

        /// <summary>
        /// 内訳科目NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("UTIWAKE_KAMOKU_NO")]
        public string UtiwakeKamokuNo { get; set; }

        /// <summary>
        /// 内訳科目NO　>> 内訳科目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("UTIWAKE_KAMOKU_NAME")]
        public string UtiwakeKamokuName { get; set; }

        /// <summary>
        /// 送料・運搬料
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_CARRIAGE")]
        public decimal Carriage { get; set; }

        //=================================
        // 手配依頼から
        //=================================
        /// <summary>
        /// 手配依頼内容
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_NAIYOU")]
        public string ThiNaiyou { get; set; }

        //=================================
        // 発注（見積回答）から
        //=================================
        /// <summary>
        /// 発注日
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_HACHU_DATE")]
        public DateTime HachuDate { get; set; }

        /// <summary>
        /// 発注番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_HACHU_NO")]
        public string HachuNo { get; set; }

        /// <summary>
        /// 顧客ID >>  顧客名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_NAME")]
        public string CustomerName { get; set; }

        #endregion


        /// <summary>
        /// 支払合算ヘッダＩＤで検索
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="odShrGassanHeadId"></param>
        /// <returns></returns>
        public static List<OdShrGassanItem> GetRecordsByOdShrGassanHeadID(NBaseData.DAC.MsUser loginUser, string odShrGassanHeadId)
        {
            List<OdShrGassanItem> ret = new List<OdShrGassanItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShrGassanItem> mapping = new MappingBase<OdShrGassanItem>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanItem), "ByOdShrGassanHeadId");
            Params.Add(new DBParameter("OD_SHR_GASSAN_HEAD_ID", odShrGassanHeadId));
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanItem), "OrderBy");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            List<MsKamoku> kamokus = MsKamoku.GetRecordsByHachuKamoku(null, loginUser);
            foreach (OdShrGassanItem osgi in ret)
            {
                foreach (MsKamoku kamoku in kamokus)
                {
                    if (osgi.KamokuNo == kamoku.KamokuNo && osgi.UtiwakeKamokuNo == kamoku.UtiwakeKamokuNo)
                    {
                        osgi.KamokuName = kamoku.KamokuName;
                        osgi.UtiwakeKamokuName = kamoku.UtiwakeKamokuName;
                        break;
                    }
                }
            }


            return ret;
        }

        /// <summary>
        /// 支払合算項目ＩＤで検索
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="OdShrGassanItemId"></param>
        /// <returns></returns>
        public static OdShrGassanItem GetRecord(NBaseData.DAC.MsUser loginUser, string OdShrGassanItemId)
        {
            return GetRecord(null, loginUser, OdShrGassanItemId);
        }
        public static OdShrGassanItem GetRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdShrGassanItemId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanItem), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanItem), "ByOdShrGassanItemId");

            List<OdShrGassanItem> ret = new List<OdShrGassanItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShrGassanItem> mapping = new MappingBase<OdShrGassanItem>();
            Params.Add(new DBParameter("OD_SHR_GASSAN_ITEM_ID", OdShrGassanItemId));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


        /// <summary>
        /// 支払合算ヘッダのＯＤ_ＳＨＲ_ＩＤで検索
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="odShrId"></param>
        /// <returns></returns>
        public static List<OdShrGassanItem> GetRecordsByOdShrId(NBaseData.DAC.MsUser loginUser, string odShrId)
        {
            List<OdShrGassanItem> ret = new List<OdShrGassanItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<OdShrGassanItem> mapping = new MappingBase<OdShrGassanItem>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanItem), "GetRecordsByOdShrId");
            Params.Add(new DBParameter("OD_SHR_ID", odShrId));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        /// <summary>
        /// インサート
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_SHR_GASSAN_ITEM_ID", OdShrGassanItemID));
            Params.Add(new DBParameter("OD_SHR_GASSAN_HEAD_ID", OdShrGassanHeadID));
            Params.Add(new DBParameter("OD_JRY_ID", OdJryID));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }


        /// <summary>
        /// 物理削除を実行します
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static bool DeleteRecordByOdShrGassanHeadID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string odShrGassanHeadId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(OdShrGassanItem), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("OD_SHR_GASSAN_HEAD_ID", odShrGassanHeadId));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
    }
}
