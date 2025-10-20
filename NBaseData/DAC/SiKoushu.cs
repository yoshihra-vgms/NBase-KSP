using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using System.Reflection;
using ORMapping;
using System.Runtime.Serialization;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("SI_KOUSHU")]
    public class SiKoushu : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 講習ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_KOUSHU_ID", true)]
        public string SiKoushuID { get; set; }

        
        

        /// <summary>
        /// 講習ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_KOUSHU_ID")]
        public int MsSiKoushuID { get; set; }
        
        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

        /// <summary>
        /// 場所
        /// </summary>
        [DataMember]
        [ColumnAttribute("BASHO")]
        public string Basho { get; set; }

        /// <summary>
        /// 予定開始日
        /// </summary>
        [DataMember]
        [ColumnAttribute("YOTEI_FROM")]
        public DateTime YoteiFrom { get; set; }

        /// <summary>
        /// 予定終了日
        /// </summary>
        [DataMember]
        [ColumnAttribute("YOTEI_TO")]
        public DateTime YoteiTo { get; set; }

        /// <summary>
        /// 実績開始日
        /// </summary>
        [DataMember]
        [ColumnAttribute("JISEKI_FROM")]
        public DateTime JisekiFrom { get; set; }

        /// <summary>
        /// 実績終了日
        /// </summary>
        [DataMember]
        [ColumnAttribute("JISEKI_TO")]
        public DateTime JisekiTo { get; set; }


        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }



        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }

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




        /// <summary>
        /// 講習名 (INNER JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOUSHU_NAME")]
        public string KoushuName { get; set; }

        /// <summary>
        /// 有効期限 (INNER JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOUSHU_YUKOKIGEN_STR")]
        public string KoushuYukokigenStr { get; set; }

        /// <summary>
        /// 有効期限 (INNER JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOUSHU_YUKOKIGEN_DAYS")]
        public int KoushuYukokigenDays { get; set; }


        /// <summary>
        /// 船員名 (INNER JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_NAME")]
        public string SeninName { get; set; }
        
        /// <summary>
        /// 船員名(カナ) (INNER JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_NAME_KANA")]
        public string SeninNameKana { get; set; }

        /// <summary>
        /// 船員氏名コード (INNER JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_SHIMEI_CODE")]
        public string SeninShimeiCode { get; set; }

        /// <summary>
        /// 職名 (INNER JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_SHOKUMEI")]
        public string SeninShokumei { get; set; }

        /// <summary>
        /// 職名ID (INNER JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_SHOKUMEI_ID")]
        public int SeninShokumeiID { get; set; }

        /// <summary>
        /// 添付ファイル
        /// </summary>
        [DataMember]
        public List<SiKoushuAttachFile> AttachFiles { get; set; }


        #endregion

        public SiKoushu()
        {
            this.SiKoushuID = null;
            this.MsSeninID = Int32.MinValue;
            this.MsSiKoushuID = Int32.MinValue;
            this.AttachFiles = new List<SiKoushuAttachFile>();
        }

        public static List<SiKoushu> GetRecordsByFilter(MsUser loginUser, SiKoushuFilter filter)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "GetRecords");

            List<SiKoushu> ret = new List<SiKoushu>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKoushu> mapping = new MappingBase<SiKoushu>();

            if (filter.SiKoushuID != null && filter.SiKoushuID.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterByID");
                Params.Add(new DBParameter("SI_KOUSHU_ID", filter.SiKoushuID));
            }
            if (filter.KoushuName != null && filter.KoushuName.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterByName");
                Params.Add(new DBParameter("NAME", "%" + filter.KoushuName + "%"));
            }
            if (filter.MsSeninID > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterByMsSeninID");
                Params.Add(new DBParameter("MS_SENIN_ID", filter.MsSeninID));
            }



            if (filter.YoteiTo != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterByYoteiFromTo");
                Params.Add(new DBParameter("YOTEI_FROM", filter.YoteiFrom));
                Params.Add(new DBParameter("YOTEI_TO", filter.YoteiTo));
            }
            else if (filter.YoteiFrom != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterByYoteiFrom");
                Params.Add(new DBParameter("YOTEI_FROM", filter.YoteiFrom));
            }



            if (filter.Bikou != null && filter.Bikou.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterByBikou");
                Params.Add(new DBParameter("BIKOU", "%" + filter.Bikou + "%"));
            }

            if (filter.Name != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterBySeninName");
                Params.Add(new DBParameter("SENIN_NAME", "%" + filter.Name + "%"));
            }

            if (filter.NameKana != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterBySeninNameKana");
                Params.Add(new DBParameter("SENIN_NAME_KANA", "%" + filter.NameKana + "%"));
            }

            if (filter.ShimeiCode != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterByShimeiCode");
                Params.Add(new DBParameter("SHIMEI_CODE", filter.ShimeiCode));
            }

            // 2014.02 2013年度改造
            //if (filter.Is受講済み && filter.Is受講予定 == false)
            //{
            //    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterBy受講済み");
            //}
            //if (filter.Is受講予定 && filter.Is受講済み == false)
            //{
            //    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterBy受講予定");
            //    Params.Add(new DBParameter("NOW", DateTime.Today));
            //}
            if (filter.Is未受講)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterBy未受講");
                Params.Add(new DBParameter("NOW", DateTime.Today));
            }
            else
            {
                if (filter.Is受講済み && filter.Is受講予定 == false)
                {
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterBy受講済み");
                }
                if (filter.Is受講予定 && filter.Is受講済み == false)
                {
                    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterBy受講予定");
                    Params.Add(new DBParameter("NOW", DateTime.Today));
                }
            }
            // 2014.02 2013年度改造
            if (filter.JisekiFrom != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterByJisekiFrom");
                Params.Add(new DBParameter("JISEKI_FROM", filter.JisekiFrom));
            }
            // 2014.02 2013年度改造
            if (filter.JisekiTo != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterByJisekiTo");
                Params.Add(new DBParameter("JISEKI_TO", filter.JisekiTo));
            }
            // 2014.02 2013年度改造
            if (filter.MsSiKoushuID > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterByMsSiKoushuID");
                Params.Add(new DBParameter("MS_SI_KOUSHU_ID", filter.MsSiKoushuID));
            }
            // 2014.02 2013年度改造
            if (filter.Is退職者を除く)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterByRetireFlag");
            }

            if (filter.MsSiShokumeiID > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "FilterByMsSiShokumeiID");
                Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", filter.MsSiShokumeiID));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "OrderBy");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            // 2014.02 2013年度改造
            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)//(Common.DBTYPE == Common.DB_TYPE.ORACLE)// 201508 Oracle >> Postgresql対応
            {
                foreach (SiKoushu m in ret)
                {
                    m.AttachFiles = SiKoushuAttachFile.GetRecordByKoushuID(loginUser, m.SiKoushuID);
                }
            }
            return ret;
        }

        public static List<SiKoushu> GetRecordsBashoOnly(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "GetRecordsBashoOnly");
            List<SiKoushu> ret = new List<SiKoushu>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKoushu> mapping = new MappingBase<SiKoushu>();

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }

        public static List<SiKoushu> GetIdByJisekiToMax(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKoushu), "GetIdByJisekiToMax");
            List<SiKoushu> ret = new List<SiKoushu>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKoushu> mapping = new MappingBase<SiKoushu>();

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }

        #region ISyncTable メンバ

        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_KOUSHU_ID", SiKoushuID));

            Params.Add(new DBParameter("MS_SI_KOUSHU_ID", MsSiKoushuID));
            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("BASHO", Basho));

            Params.Add(new DBParameter("YOTEI_FROM", YoteiFrom));
            Params.Add(new DBParameter("YOTEI_TO", YoteiTo));
            Params.Add(new DBParameter("JISEKI_FROM", JisekiFrom));
            Params.Add(new DBParameter("JISEKI_TO", JisekiTo));

            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            
            foreach (SiKoushuAttachFile a in AttachFiles)
            {
                a.SiKoushuID = SiKoushuID;
                a.RenewDate = RenewDate;
                a.RenewUserID = RenewUserID;
                bool ret = a.InsertRecord(dbConnect, loginUser);
                if (ret == false)
                    return false;
            }
            
            return true;
        }

        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }
        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_SI_KOUSHU_ID", MsSiKoushuID));
            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("BASHO", Basho));

            Params.Add(new DBParameter("YOTEI_FROM", YoteiFrom));
            Params.Add(new DBParameter("YOTEI_TO", YoteiTo));
            Params.Add(new DBParameter("JISEKI_FROM", JisekiFrom));
            Params.Add(new DBParameter("JISEKI_TO", JisekiTo));

            Params.Add(new DBParameter("BIKOU", Bikou));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_KOUSHU_ID", SiKoushuID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            
            foreach (SiKoushuAttachFile a in AttachFiles)
            {
                a.RenewDate = RenewDate;
                a.RenewUserID = RenewUserID;

                bool ret = true;
                if (a.SiKoushuID == SiKoushuID)
                {
                    ret = a.UpdateRecord(dbConnect, loginUser);
                }
                else
                {
                    a.SiKoushuID = SiKoushuID;
                    ret = a.InsertRecord(dbConnect, loginUser);
                }
                if (ret == false)
                    return false;
           }
           
           return true;
        }

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", SiKoushuID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect,loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return SiKoushuID == null;
        }
    }
}
