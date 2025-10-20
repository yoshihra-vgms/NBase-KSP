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
    [TableAttribute("MS_VESSEL")]
    public class MsVessel : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID", true)]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 船NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_NO")]
        public string VesselNo { get; set; }

        /// <summary>
        /// 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_NAME")]
        public string VesselName { get; set; }

        /// <summary>
        /// DWT(L/T)
        /// </summary>
        [DataMember]
        [ColumnAttribute("DWT")]
        public int DWT { get; set; }

        /// <summary>
        /// 定員数
        /// </summary>
        [DataMember]
        [ColumnAttribute("CAPACITY")]
        public int Capacity { get; set; }

        /// <summary>
        /// 携帯電話
        /// </summary>
        [DataMember]
        [ColumnAttribute("HP_TEL")]
        public string HpTel { get; set; }

        /// <summary>
        /// 電話番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("TEL")]
        public string Tel { get; set; }

        /// <summary>
        /// 船タイプID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_TYPE_ID")]
        public string MsVesselTypeID { get; set; }

        /// <summary>
        /// オフィシャルナンバー
        /// </summary>
        [DataMember]
        [ColumnAttribute("OFFICIAL_NUMBER")]
        public string OfficialNumber { get; set; }

        /// <summary>
        /// 貨物重量
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARGO_WEIGHT")]
        public decimal CargoWeight { get; set; }

        /// <summary>
        /// グロストン
        /// </summary>
        [DataMember]
        [ColumnAttribute("GRT")]
        public decimal GRT { get; set; }

        /// <summary>
        /// 航行区域
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAVIGATION_AREA")]
        public string NavigationArea { get; set; }

        /// <summary>
        /// 船主
        /// </summary>
        [DataMember]
        [ColumnAttribute("OWNER_NAME")]
        public string OwnerName { get; set; }


        #region 機能権限等


        /// <summary>
        /// NBaseHonsenのログインで使用
        /// </summary>
        [DataMember]
        [ColumnAttribute("HONSEN_ENABLED")]
        public int HonsenEnabled { get; set; }

        /// <summary>
        /// 予実管理で使用
        /// </summary>
        [DataMember]
        [ColumnAttribute("YOJITSU_ENABLED")]
        public int YojitsuEnabled { get; set; }

        /// <summary>
        /// 発注管理で有効
        /// </summary>
        [DataMember]
        [ColumnAttribute("HACHU_ENABLED")]
        public int HachuEnabled { get; set; }
        
        /// <summary>
        /// 船員管理で有効
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_ENABLED")]
        public int SeninEnabled { get; set; }

        /// <summary>
        /// 文書管理で有効
        /// </summary>
        [DataMember]
        [ColumnAttribute("DOCUMENT_ENABLED")]
        public int DocumentEnabled { get; set; }

        /// <summary>
        /// 検査証書管理で有効
        /// </summary>
        [DataMember]
        [ColumnAttribute("KENSA_ENABLED")]
        public int KensaEnabled { get; set; }

        /// <summary>
        /// 動静管理で有効
        /// </summary>
        [DataMember]
        [ColumnAttribute("KANIDOUSEI_ENABLED")]
        public int KanidouseiEnabled { get; set; }

        /// <summary>
        /// 予実（実績表示）
        /// </summary>
        [DataMember]
        [ColumnAttribute("YOJITSU_RESULTS")]
        public int YojitsuResults { get; set; }

        /// <summary>
        /// 発注（実績表示）
        /// </summary>
        [DataMember]
        [ColumnAttribute("HACHU_RESULTS")]
        public int HachuResults { get; set; }

        /// <summary>
        /// 船員（実績表示）
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_RESULTS")]
        public int SeninResults { get; set; }

        /// <summary>
        /// 文書（実績表示）
        /// </summary>
        [DataMember]
        [ColumnAttribute("DOCUMENT_RESULTS")]
        public int DocumentResults { get; set; }

        /// <summary>
        /// 検査（実績表示）
        /// </summary>
        [DataMember]
        [ColumnAttribute("KENSA_RESULTS")]
        public int KensaResults { get; set; }

        /// <summary>
        /// 動静（実績表示）
        /// </summary>
        [DataMember]
        [ColumnAttribute("KANIDOUSEI_RESULTS")]
        public int KanidouseiResults { get; set; }


        /// <summary>
        /// 荷役資材で有効
        /// </summary>
        [DataMember]
        [ColumnAttribute("NIYAKU_ENABLED")]
        public int NiyakuEnabled { get; set; }


        #endregion


        /// <summary>
        /// 表示順序
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOW_ORDER")]
        public int ShowOrder { get; set; }

        /// <summary>
        /// アニバーサリー日付
        /// </summary>
        [DataMember]
        [ColumnAttribute("ANNIVERSARY_DATE")]
        public DateTime AnniversaryDate { get; set; }

        /// <summary>
        /// 竣工日
        /// </summary>
        [DataMember]
        [ColumnAttribute("COMPLETION_DATE")]
        public DateTime CompletionDate { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        [DataMember]
        [ColumnAttribute("NATIONALITY")]
        public string Nationality { get; set; }

        /// <summary>
        /// メールアドレス
        /// </summary>
        [DataMember]
        [ColumnAttribute("MAIL_ADDRESS")]
        public string MailAddress { get; set; }

        /// <summary>
        /// 営業担当者
        /// </summary>
        [DataMember]
        [ColumnAttribute("SALES_PERSON_ID")]
        public string SalesPersonID { get; set; }

        /// <summary>
        /// 工務監督
        /// </summary>
        [DataMember]
        [ColumnAttribute("MARINE_SUPERINTENDENT_ID")]
        public string MarineSuperintendentID { get; set; }

        /// <summary>
        /// CrewMatrixType
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CREW_MATRIX_TYPE_ID")]
        public int MsCrewMatrixTypeID { get; set; }

        #region 指摘事項

        /// <summary>
        /// IMO番号(指摘事項）
        /// </summary>
        [DataMember]
        [ColumnAttribute("IMO_NO")]
        public int ImoNO { get; set; }

        /// <summary>
        /// 船種類(指摘事項）
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_KIND_ID")]
        public string MsVesselKindID { get; set; }

        /// <summary>
        /// 船種類(指摘事項）
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_CATEGORY_ID")]
        public string MsVesselCategoryID { get; set; }

        /// <summary>
        /// 表示順序(指摘事項）
        /// </summary>
        [DataMember]
        [ColumnAttribute("DEFICIENCY_ORDER")]
        public int DeficiencyOrder { get; set; }

        #endregion


        #region 色

        /// <summary>
        /// (配乗計画）色
        /// </summary>
        [DataMember]
        [ColumnAttribute("A")]
        public int A { get; set; }

        /// <summary>
        /// (配乗計画）色
        /// </summary>
        [DataMember]
        [ColumnAttribute("R")]
        public int R { get; set; }


        /// <summary>
        /// (配乗計画）色
        /// </summary>
        [DataMember]
        [ColumnAttribute("G")]
        public int G { get; set; }


        /// <summary>
        /// (配乗計画）色
        /// </summary>
        [DataMember]
        [ColumnAttribute("B")]
        public int B { get; set; }



        #endregion



        /// <summary>
        /// 速力
        /// </summary>
        [DataMember]
        [ColumnAttribute("KNOT")]
        public decimal Knot { get; set; }


        /// <summary>
        /// 積載可能貨物
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARGOS")]
        public string Cargos { get; set; }




        #region 用途不明のためコメント

        ///// <summary>
        ///// 自社船フラグ
        ///// </summary>
        //[DataMember]
        //[ColumnAttribute("OWNER")]
        //public int Owner { get; set; }

        #endregion

        #region 標準モジュールでは未使用

        ///// <summary>
        ///// 改正省エネ法エネルギー報告書
        ///// </summary>
        //[DataMember]
        //[ColumnAttribute("DOUSEI_REPORT1")]
        //public int DouseiReport1 { get; set; }

        ///// <summary>
        ///// 内航海運輸送実績調査票
        ///// </summary>
        //[DataMember]
        //[ColumnAttribute("DOUSEI_REPORT2")]
        //public int DouseiReport2 { get; set; }

        ///// <summary>
        ///// 内航船舶輸送実績調査票
        ///// </summary>
        //[DataMember]
        //[ColumnAttribute("DOUSEI_REPORT3")]
        //public int DouseiReport3 { get; set; }

        ///// <summary>
        ///// 会計部門コード
        ///// </summary>
        //[DataMember]
        //[ColumnAttribute("KAIKEI_BUMON_CODE")]
        //public string KaikeiBumonCode { get; set; }

        ///// <summary>
        ///// 給与連携NO
        ///// </summary>
        //[DataMember]
        //[ColumnAttribute("KYUYO_RENKEI_NO")]
        //public string KyuyoRenkeiNo { get; set; }

        #endregion


        #region 共通メンバー


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

        #endregion




        /// <summary>
        /// 船長名
        /// </summary>
        [DataMember]
        public string CaptainName { get; set; }


        /// <summary>
        /// 営業担当者名
        /// </summary>
        [DataMember]
        [ColumnAttribute("SALES_PERSON_NAME")]
        public string SalesPersonName { get; set; }

        /// <summary>
        /// 工務監督名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MARINE_SUPERINTENDENT_NAME")]
        public string MarineSuperintendentName { get; set; }

        /// <summary>
        /// 船タイプ名(MS_VESSEL_TYPE JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_TYPE_NAME")]
        public string VesselTypeName { get; set; }


        /// <summary>
        /// 配乗計画タイプ(MS_PLAN_TYPE JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("PLAN_TYPE")]
        public int PlanType { get; set; }


        #endregion


        public static List<string> NavigationAreaStrings = new List<string> { "", "限定近海", "限定沿海", "近海区域" };





        public bool IsPlanType(int planType)
        {
            return PlanType == planType;
        }




        public override string ToString()
        {
            string retStr = "";
            if (VesselName.Length > 0)
            {
                retStr = VesselName;
                if (VesselNo.Length > 0)
                {
                    retStr += " (" + VesselNo + ")";
                }
            }
            return retStr;
        }
        
        public MsVessel()
        {
            A = R = G = B = 255;
        }
        
        public static List<MsVessel> GetRecords(MsUser loginUser)
        {
            return GetRecords(null, loginUser);
        }      
        public static List<MsVessel> GetRecords(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "OrderBy");

            List<MsVessel> ret = new List<MsVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVessel> mapping = new MappingBase<MsVessel>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }
        
        public static List<MsVessel> GetRecordsBySeninEnabled(MsUser loginUser)
        {
            return GetRecordsBySeninEnabled(null, loginUser);
        }
        public static List<MsVessel> GetRecordsBySeninEnabled(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "BySeninEnabledResults");//2017/6/1 m.yoshihara
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "OrderBy");

            List<MsVessel> ret = new List<MsVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVessel> mapping = new MappingBase<MsVessel>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsVessel> GetRecordsByKensaEnabled(MsUser loginUser)
        {
            return GetRecordsByKensaEnabled(null, loginUser);
        }
        public static List<MsVessel> GetRecordsByKensaEnabled(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "ByKensaEnabledResults");//2017/6/1 m.yoshihara
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "OrderBy");

            List<MsVessel> ret = new List<MsVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVessel> mapping = new MappingBase<MsVessel>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsVessel> GetRecordsByHachuEnabled(MsUser loginUser)
        {
            return GetRecordsByHachuEnabled(null, loginUser);
        }
        public static List<MsVessel> GetRecordsByHachuEnabled(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "ByHachuEnabledResults");//2017/5/16 m.yoshihara
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "OrderBy");

            List<MsVessel> ret = new List<MsVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVessel> mapping = new MappingBase<MsVessel>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsVessel> GetRecordsByYojitsuEnabled(MsUser loginUser)
        {
            return GetRecordsByYojitsuEnabled(null, loginUser);
        }
        public static List<MsVessel> GetRecordsByYojitsuEnabled(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "ByYojitsuEnabledResults");//2017/5/19 m.yoshihara
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "OrderBy");

            List<MsVessel> ret = new List<MsVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVessel> mapping = new MappingBase<MsVessel>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsVessel> GetRecordsByHonsenEnabled(MsUser loginUser)
        {
            return GetRecordsByHonsenEnabled(null, loginUser);
        }
        public static List<MsVessel> GetRecordsByHonsenEnabled(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "ByHonsenEnabled");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "OrderBy");

            List<MsVessel> ret = new List<MsVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVessel> mapping = new MappingBase<MsVessel>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsVessel> GetRecordsByDocumentEnabled(MsUser loginUser)
        {
            return GetRecordsByDocumentEnabled(null, loginUser);
        }
        public static List<MsVessel> GetRecordsByDocumentEnabled(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "ByDocumentEnabledResults");//2017/5/17 m.yoshihara
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "OrderBy");

            List<MsVessel> ret = new List<MsVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVessel> mapping = new MappingBase<MsVessel>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsVessel> GetRecordsByKanidouseiEnabled(MsUser loginUser)
        {
            return GetRecordsByKanidouseiEnabled(null, loginUser);
        }
        public static List<MsVessel> GetRecordsByKanidouseiEnabled(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "GetRecords2");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "ByKanidouseiEnabledResults");//2017/5/17 m.yoshihara
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "OrderBy");

            List<MsVessel> ret = new List<MsVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVessel> mapping = new MappingBase<MsVessel>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            foreach (MsVessel vessel in ret)
            {
                SiCard card = SiCard.Get_船長(loginUser, vessel.MsVesselID);
                if (card != null)
                {
                    vessel.CaptainName = card.SeninName;
                }
                MsUser user = MsUser.GetRecordsByUserID(loginUser, vessel.SalesPersonID);
                if (user != null)
                {
                    vessel.SalesPersonName = user.FullName;
                }
                user = MsUser.GetRecordsByUserID(loginUser, vessel.MarineSuperintendentID);
                if (user != null)
                {
                    vessel.MarineSuperintendentName = user.FullName;
                }
            }
            return ret;
        }

        public static MsVessel GetRecordByMsVesselID(MsUser loginUser, int msVesselID)
        {
            return GetRecordByMsVesselID(null, loginUser, msVesselID);
        }
        public static MsVessel GetRecordByMsVesselID(DBConnect dbConnect, MsUser loginUser, int msVesselID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "ByMsVesselID");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "OrderBy");

            List<MsVessel> ret = new List<MsVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVessel> mapping = new MappingBase<MsVessel>();
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }
        
        public static MsVessel GetRecordByVesselNo(MsUser loginUser, string vesselNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "ByVesselNo");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "OrderBy");

            List<MsVessel> ret = new List<MsVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVessel> mapping = new MappingBase<MsVessel>();
            Params.Add(new DBParameter("VESSEL_NO", vesselNo));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static MsVessel GetRecordByAkasakaVesselNo(MsUser loginUser, string AkasakaVesselNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "ByAkasakaVesselNo");

            List<MsVessel> ret = new List<MsVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVessel> mapping = new MappingBase<MsVessel>();
            Params.Add(new DBParameter("AKASAKA_VESSEL_NO", AkasakaVesselNo));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<MsVessel> SearchRecords(MsUser loginUser, string vesselNo, string vesselName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "GetRecords");
            List<MsVessel> ret = new List<MsVessel>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVessel> mapping = new MappingBase<MsVessel>();

            if (vesselNo != "")
            {
                SQL += " and MS_VESSEL.VESSEL_NO like :VESSEL_NO";
                Params.Add(new DBParameter("VESSEL_NO", "%" + vesselNo + "%"));
            }
            if (vesselName != "")
            {
                SQL += " and MS_VESSEL.VESSEL_NAME  like :VESSEL_NAME";
                Params.Add(new DBParameter("VESSEL_NAME", "%" + vesselName + "%"));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsVessel), "OrderByVesselNo");

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;

        }

        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            if (ORMapping.Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)//Common.DB_TYPE.SQLSERVER)// 20150902 Postgresql_Client化対応 条件変更
            {
                Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            }
            Params.Add(new DBParameter("VESSEL_NO", VesselNo));
            Params.Add(new DBParameter("VESSEL_NAME", VesselName));

            Params.Add(new DBParameter("DWT", DWT));
            Params.Add(new DBParameter("CAPACITY", Capacity));
            Params.Add(new DBParameter("HP_TEL", HpTel));
            Params.Add(new DBParameter("TEL", Tel));
            Params.Add(new DBParameter("MS_VESSEL_TYPE_ID", MsVesselTypeID));
            Params.Add(new DBParameter("OFFICIAL_NUMBER", OfficialNumber));
            Params.Add(new DBParameter("CARGO_WEIGHT", CargoWeight));

            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));
            Params.Add(new DBParameter("COMPLETION_DATE", CompletionDate));
            Params.Add(new DBParameter("ANNIVERSARY_DATE", AnniversaryDate));
            Params.Add(new DBParameter("NATIONALITY", Nationality));
            Params.Add(new DBParameter("MAIL_ADDRESS", MailAddress));

            Params.Add(new DBParameter("SALES_PERSON_ID", SalesPersonID));
            Params.Add(new DBParameter("MARINE_SUPERINTENDENT_ID", MarineSuperintendentID));
            Params.Add(new DBParameter("MS_CREW_MATRIX_TYPE_ID", MsCrewMatrixTypeID));



            Params.Add(new DBParameter("HONSEN_ENABLED", HonsenEnabled));

            Params.Add(new DBParameter("YOJITSU_ENABLED", YojitsuEnabled));
            Params.Add(new DBParameter("HACHU_ENABLED", HachuEnabled));
            Params.Add(new DBParameter("SENIN_ENABLED", SeninEnabled));
            Params.Add(new DBParameter("DOCUMENT_ENABLED", DocumentEnabled));
            Params.Add(new DBParameter("KENSA_ENABLED", KensaEnabled));
            Params.Add(new DBParameter("KANIDOUSEI_ENABLED", KanidouseiEnabled));

            Params.Add(new DBParameter("YOJITSU_RESULTS", YojitsuResults));
            Params.Add(new DBParameter("HACHU_RESULTS", HachuResults));
            Params.Add(new DBParameter("SENIN_RESULTS", SeninResults));
            Params.Add(new DBParameter("DOCUMENT_RESULTS", DocumentResults));
            Params.Add(new DBParameter("KENSA_RESULTS", KensaResults));
            Params.Add(new DBParameter("KANIDOUSEI_RESULTS", KanidouseiResults));

            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            {
                Params.Add(new DBParameter("IMO_NO", ImoNO));
                Params.Add(new DBParameter("MS_VESSEL_KIND_ID", MsVesselKindID));
                Params.Add(new DBParameter("MS_VESSEL_CATEGORY_ID", MsVesselCategoryID));
                Params.Add(new DBParameter("DEFICIENCY_ORDER", DeficiencyOrder));

                Params.Add(new DBParameter("GRT", GRT));
                Params.Add(new DBParameter("NIYAKU_ENABLED", NiyakuEnabled));

                Params.Add(new DBParameter("NAVIGATION_AREA", NavigationArea));
                Params.Add(new DBParameter("OWNER_NAME", OwnerName));

                Params.Add(new DBParameter("A", A));
                Params.Add(new DBParameter("R", R));
                Params.Add(new DBParameter("G", G));
                Params.Add(new DBParameter("B", B));

                Params.Add(new DBParameter("KNOT", Knot));
                Params.Add(new DBParameter("CARGOS", Cargos));
            }

            //Params.Add(new DBParameter("OWNER", Owner));
            //Params.Add(new DBParameter("DOUSEI_REPORT1", DouseiReport1));
            //Params.Add(new DBParameter("DOUSEI_REPORT2", DouseiReport2));
            //Params.Add(new DBParameter("DOUSEI_REPORT3", DouseiReport3));
            //Params.Add(new DBParameter("KAIKEI_BUMON_CODE", KaikeiBumonCode));
            //Params.Add(new DBParameter("KYUYO_RENKEI_NO", KyuyoRenkeiNo));


            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            return true;
        }

        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("VESSEL_NO", VesselNo));
            Params.Add(new DBParameter("VESSEL_NAME", VesselName));

            Params.Add(new DBParameter("DWT", DWT));
            Params.Add(new DBParameter("CAPACITY", Capacity));
            Params.Add(new DBParameter("HP_TEL", HpTel));
            Params.Add(new DBParameter("TEL", Tel));
            Params.Add(new DBParameter("MS_VESSEL_TYPE_ID", MsVesselTypeID));
            Params.Add(new DBParameter("OFFICIAL_NUMBER", OfficialNumber));
            Params.Add(new DBParameter("CARGO_WEIGHT", CargoWeight));

            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));
            Params.Add(new DBParameter("COMPLETION_DATE", CompletionDate));
            Params.Add(new DBParameter("ANNIVERSARY_DATE", AnniversaryDate));
            Params.Add(new DBParameter("NATIONALITY", Nationality));
            Params.Add(new DBParameter("MAIL_ADDRESS", MailAddress));

            Params.Add(new DBParameter("SALES_PERSON_ID", SalesPersonID));
            Params.Add(new DBParameter("MARINE_SUPERINTENDENT_ID", MarineSuperintendentID));
            Params.Add(new DBParameter("MS_CREW_MATRIX_TYPE_ID", MsCrewMatrixTypeID));



            Params.Add(new DBParameter("HONSEN_ENABLED", HonsenEnabled));

            Params.Add(new DBParameter("YOJITSU_ENABLED", YojitsuEnabled));
            Params.Add(new DBParameter("HACHU_ENABLED", HachuEnabled));
            Params.Add(new DBParameter("SENIN_ENABLED", SeninEnabled));
            Params.Add(new DBParameter("DOCUMENT_ENABLED", DocumentEnabled));
            Params.Add(new DBParameter("KENSA_ENABLED", KensaEnabled));
            Params.Add(new DBParameter("KANIDOUSEI_ENABLED", KanidouseiEnabled));

            Params.Add(new DBParameter("YOJITSU_RESULTS", YojitsuResults));
            Params.Add(new DBParameter("HACHU_RESULTS", HachuResults));
            Params.Add(new DBParameter("SENIN_RESULTS", SeninResults));
            Params.Add(new DBParameter("DOCUMENT_RESULTS", DocumentResults));
            Params.Add(new DBParameter("KENSA_RESULTS", KensaResults));
            Params.Add(new DBParameter("KANIDOUSEI_RESULTS", KanidouseiResults));

            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL)
            {
                Params.Add(new DBParameter("IMO_NO", ImoNO));
                Params.Add(new DBParameter("MS_VESSEL_KIND_ID", MsVesselKindID));
                Params.Add(new DBParameter("MS_VESSEL_CATEGORY_ID", MsVesselCategoryID));
                Params.Add(new DBParameter("DEFICIENCY_ORDER", DeficiencyOrder));

                Params.Add(new DBParameter("GRT", GRT));
                Params.Add(new DBParameter("NIYAKU_ENABLED", NiyakuEnabled));

                Params.Add(new DBParameter("NAVIGATION_AREA", NavigationArea));
                Params.Add(new DBParameter("OWNER_NAME", OwnerName));

                Params.Add(new DBParameter("A", A));
                Params.Add(new DBParameter("R", R));
                Params.Add(new DBParameter("G", G));
                Params.Add(new DBParameter("B", B));

                Params.Add(new DBParameter("KNOT", Knot));
                Params.Add(new DBParameter("CARGOS", Cargos));
            }

            //Params.Add(new DBParameter("OWNER", Owner));
            //Params.Add(new DBParameter("DOUSEI_REPORT1", DouseiReport1));
            //Params.Add(new DBParameter("DOUSEI_REPORT2", DouseiReport2));
            //Params.Add(new DBParameter("DOUSEI_REPORT3", DouseiReport3));
            //Params.Add(new DBParameter("KAIKEI_BUMON_CODE", KaikeiBumonCode));
            //Params.Add(new DBParameter("KYUYO_RENKEI_NO", KyuyoRenkeiNo));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));


            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool DeleteFlagRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVessel), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsVesselID));

        //    int count = Convert.ToInt32(DBConnect.ExecuteScalar(loginUser.MsUserID, SQL, Params));

        //    return count > 0 ? true : false;
        //}
        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", MsVesselID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
}
}
