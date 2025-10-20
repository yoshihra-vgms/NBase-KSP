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
using NBaseUtil;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("SI_JUNBIKIN")]
    public class SiJunbikin : ISyncTable
    {
        #region データメンバ
        
        /// <summary>
        /// 船内準備金ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_JUNBIKIN_ID", true)]
        public string SiJunbikinID { get; set; }

        
        

        /// <summary>
        /// 船ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 費用科目ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_HIYOU_KAMOKU_ID")]
        public int MsSiHiyouKamokuID { get; set; }

        /// <summary>
        /// 大項目ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_DAIKOUMOKU_ID")]
        public int MsSiDaikoumokuID { get; set; }

        /// <summary>
        /// 明細ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_MEISAI_ID")]
        public int MsSiMeisaiID { get; set; }

        /// <summary>
        /// 船員科目No (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_KAMOKU_ID")]
        public int MsSiKamokuId { get; set; }

        /// <summary>
        /// 登録ユーザID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("TOUROKU_USER_ID")]
        public string TourokuUserID { get; set; }

        
        
        
        /// <summary>
        /// 日付
        /// </summary>
        [DataMember]
        [ColumnAttribute("JUNBIKIN_DATE")]
        public DateTime JunbikinDate { get; set; }

        /// <summary>
        /// 支払額
        /// </summary>
        [DataMember]
        [ColumnAttribute("KINGAKU_OUT")]
        public int KingakuOut { get; set; }

        /// <summary>
        /// 支払額税
        /// </summary>
        [DataMember]
        [ColumnAttribute("TAX_OUT")]
        public int TaxOut { get; set; }

        /// <summary>
        /// 受入額
        /// </summary>
        [DataMember]
        [ColumnAttribute("KINGAKU_IN")]
        public int KingakuIn { get; set; }

        /// <summary>
        /// 受入額税
        /// </summary>
        [DataMember]
        [ColumnAttribute("TAX_IN")]
        public int TaxIn { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        /// <summary>
        /// 振替フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("FURIKAE_FLAG")]
        public int FurikaeFlag { get; set; }

        
        
        
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


        // 2019.10.08 追加
        /// <summary>
        /// 税率ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_TAX_ID")]
        public int MsTaxID { get; set; }


        /// <summary>
        /// 登録ユーザ名 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("TOUROKU_USER_NAME")]
        public string TourokuUserName { get; set; }

        /// <summary>
        /// 更新ユーザ名 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOUSHIN_USER_NAME")]
        public string KoushinUserName { get; set; }




        /// <summary>
        /// 科目名 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("KAMOKU_NAME")]
        public string KamokuName { get; set; }

        /// <summary>
        /// 課税フラグ (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("TAX_FLAG")]
        public int TaxFlag { get; set; }

        /// <summary>
        /// 費用種別 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("HIYOU_KIND")]
        public int HiyouKind { get; set; }

        #endregion




        public SiJunbikin()
        {
            this.MsVesselID = Int32.MinValue;
            this.MsSiHiyouKamokuID = Int32.MinValue;
            this.MsSiDaikoumokuID = Int32.MinValue;
            this.MsSiMeisaiID = Int32.MinValue;
            this.MsSiKamokuId = Int32.MinValue;
        }
        
        
        
        
        public static List<SiJunbikin> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), MethodBase.GetCurrentMethod());

            List<SiJunbikin> ret = new List<SiJunbikin>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiJunbikin> mapping = new MappingBase<SiJunbikin>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //船マスタID
        public static List<SiJunbikin> GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), MethodBase.GetCurrentMethod());

            List<SiJunbikin> ret = new List<SiJunbikin>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiJunbikin> mapping = new MappingBase<SiJunbikin>();
            //

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        //ユーザーマスタ
        public static List<SiJunbikin> GetRecordsByMsUserID(MsUser loginUser, string ms_user_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), MethodBase.GetCurrentMethod());

            List<SiJunbikin> ret = new List<SiJunbikin>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiJunbikin> mapping = new MappingBase<SiJunbikin>();
            //

            Params.Add(new DBParameter("MS_USER_ID", ms_user_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<SiJunbikin> GetRecordsByMsSIKamokuID(MsUser loginUser, int ms_si_kamoku_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), MethodBase.GetCurrentMethod());
            

            List<SiJunbikin> ret = new List<SiJunbikin>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiJunbikin> mapping = new MappingBase<SiJunbikin>();

            Params.Add(new DBParameter("MS_SI_KAMOKU_ID", ms_si_kamoku_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            
            return ret;
        }

        public static SiJunbikin GetRecord(MsUser loginUser, string siJunbikinId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), "BySiJunbikinID");

            List<SiJunbikin> ret = new List<SiJunbikin>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiJunbikin> mapping = new MappingBase<SiJunbikin>();
            Params.Add(new DBParameter("SI_JUNBIKIN_ID", siJunbikinId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


        public static List<SiJunbikin> GetRecordsByDateAndMsVesselID(DBConnect dbConnect, MsUser loginUser, DateTime date, int msVesselId)
        {
            return GetRecordsByDateAndMsVesselID(dbConnect, loginUser, date, msVesselId, "ORDER BY JUNBIKIN_DATE");
        }
        public static List<SiJunbikin> GetRecordsByDateAndMsVesselID(MsUser loginUser, DateTime date, int msVesselId)
        {
            return GetRecordsByDateAndMsVesselID(loginUser, date, msVesselId, "ORDER BY JUNBIKIN_DATE");
        }

        public static List<SiJunbikin> GetRecordsByDateAndMsVesselID(MsUser loginUser, DateTime date, int msVesselId, string orderByStr)
        {
            return GetRecordsByDateAndMsVesselID(null, loginUser, date, msVesselId, orderByStr);
        }
        public static List<SiJunbikin> GetRecordsByDateAndMsVesselID(DBConnect dbConnect, MsUser loginUser, DateTime date, int msVesselId, string orderByStr)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), "ByMsVesselID");

            List<SiJunbikin> ret = new List<SiJunbikin>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiJunbikin> mapping = new MappingBase<SiJunbikin>();

            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));

            if (date != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), "ByStartEnd");

                DateTime ds = MsSiGetsujiShimeBi.ToFrom_船員_月次締日(dbConnect, loginUser, date);
                Params.Add(new DBParameter("START_DATE", ds));
                DateTime de = MsSiGetsujiShimeBi.ToTo_船員_月次締日(dbConnect, loginUser, date);
                Params.Add(new DBParameter("END_DATE", de));
            }

            if (orderByStr != null && orderByStr != string.Empty)
            {
                SQL += " " + orderByStr;
            }

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static decimal Get_先月末残高(MsUser loginUser, DateTime date, int msVesselId)
        {
            return Get_先月末残高(null, loginUser, date, msVesselId);
        }
        public static decimal Get_先月末残高(DBConnect dbConnect, MsUser loginUser, DateTime date, int msVesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), MethodBase.GetCurrentMethod());

            //List<SiJunbikin> ret = new List<SiJunbikin>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            DateTime de = MsSiGetsujiShimeBi.ToFrom_船員_月次締日(dbConnect, loginUser, date);
            Params.Add(new DBParameter("END_DATE", de));

            object result = DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params);

            //if (result is decimal)
            //{
            //    return (decimal)result;
            //}
            //else if (result is int)
            //{
            //    return Convert.ToDecimal(result);
            //}
            //else
            //{
            //    return 0;
            //}
            decimal ret = 0;
            try
            {
                ret = (decimal)result;
            }
            catch
            {
                try
                {
                    ret = Convert.ToDecimal(result);
                }
                catch
                {

                }
            }
            return ret;
        }

        public static List<SiJunbikin> Get_振替_修繕費(MsUser loginUser, DateTime date, int msVesselId)
        {
            return Get_振替_修繕費(null, loginUser, date, msVesselId);
        }
        public static List<SiJunbikin> Get_振替_修繕費(DBConnect dbConnect, MsUser loginUser, DateTime date, int msVesselId)
        {
            SeninTableCache seninTableCache = SeninTableCache.instance(false);
            seninTableCache.DacProxy = new DirectSeninDacProxy();
            int 修繕費 = seninTableCache.MsSiKamoku_修繕費ID(loginUser);

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), "Get_振替");

            List<SiJunbikin> ret = new List<SiJunbikin>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiJunbikin> mapping = new MappingBase<SiJunbikin>();

            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("MS_SI_KAMOKU_ID", 修繕費));
            //Params.Add(new DBParameter("START_DATE", DateTimeUtils.年度開始日(date)));
            //Params.Add(new DBParameter("END_DATE", DateTimeUtils.年度終了日(date)));
            DateTime ds = MsSiGetsujiShimeBi.ToFrom_船員_月次締日(loginUser, DateTimeUtils.年度開始日(date));
            Params.Add(new DBParameter("START_DATE", ds));
            DateTime de = MsSiGetsujiShimeBi.ToTo_船員_月次締日(loginUser, DateTimeUtils.年度終了日(date));
            Params.Add(new DBParameter("END_DATE", de));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiJunbikin> Get_振替_船用品費(MsUser loginUser, DateTime date, int msVesselId)
        {
            return Get_振替_船用品費(null, loginUser, date, msVesselId);
        }
        public static List<SiJunbikin> Get_振替_船用品費(DBConnect dbConnect, MsUser loginUser, DateTime date, int msVesselId)
        {
            //SeninTableCache seninTableCache = SeninTableCache.instance(false);
            //seninTableCache.DacProxy = new DirectSeninDacProxy();
            //int 船用品費 = seninTableCache.MsSiKamoku_船用品費ID(loginUser);

            int 船用品費 = -1;
            List<MsSiKamoku> kamokus = MsSiKamoku.GetRecords(dbConnect, loginUser);
            foreach (MsSiKamoku s in kamokus)
            {
                if (s.KamokuName == "船用品費")
                {
                    船用品費 = s.MsSiKamokuId;
                    break;
                }
            }

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), "Get_振替");

            List<SiJunbikin> ret = new List<SiJunbikin>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiJunbikin> mapping = new MappingBase<SiJunbikin>();

            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("MS_SI_KAMOKU_ID", 船用品費));
            //Params.Add(new DBParameter("START_DATE", DateTimeUtils.年度開始日(date)));
            //Params.Add(new DBParameter("END_DATE", DateTimeUtils.年度終了日(date)));
            DateTime ds = MsSiGetsujiShimeBi.ToFrom_船員_月次締日(loginUser, DateTimeUtils.年度開始日(date));
            Params.Add(new DBParameter("START_DATE", ds));
            DateTime de = MsSiGetsujiShimeBi.ToTo_船員_月次締日(loginUser, DateTimeUtils.年度終了日(date));
            Params.Add(new DBParameter("END_DATE", de));
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }



        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public static List<SiJunbikin> GetRecordsByMsSiHiyouKamokuID(MsUser loginUser, int sikamokuid)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), "ByMsSiHiyouKamokuID");

            List<SiJunbikin> ret = new List<SiJunbikin>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiJunbikin> mapping = new MappingBase<SiJunbikin>();

            Params.Add(new DBParameter("MS_SI_HIYOU_KAMOKU_ID", sikamokuid));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<SiJunbikin> GetRecordsByMsSiDaikoumokuID(MsUser loginUser, int sidaikoumoku_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), "ByMsSiDaikoumokuID");

            List<SiJunbikin> ret = new List<SiJunbikin>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiJunbikin> mapping = new MappingBase<SiJunbikin>();

            Params.Add(new DBParameter("MS_SI_DAIKOUMOKU_ID", sidaikoumoku_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        // 2014.03: 2013年度改造
        //public static Dictionary<decimal, List<SiJunbikin>> Get科目別集計表データ(MsUser loginUser, DateTime date, int msVesselId)
        //{
        //    return Get科目別集計表データ(null, loginUser, date, msVesselId);
        //}
        //public static Dictionary<decimal, List<SiJunbikin>> Get科目別集計表データ(DBConnect dbConnect, MsUser loginUser, DateTime date, int msVesselId)
        //{
        //    Dictionary<decimal, List<SiJunbikin>> ret = new Dictionary<decimal, List<SiJunbikin>>();

        //    // 対象月次の開始日、終了日
        //    DateTime ds = MsSiGetsujiShimeBi.ToFrom_船員_月次締日(dbConnect, loginUser, date);
        //    DateTime de = MsSiGetsujiShimeBi.ToTo_船員_月次締日(dbConnect, loginUser, date);

        //    // 
        //    List<MsTax> msTaxList = MsTax.GetRecords(dbConnect, loginUser);

        //    var tmp = msTaxList.Where(obj => obj.StartDate > ds).Where(obj => obj.StartDate < de);
        //    if (tmp.Count() > 0)
        //    {
        //        tmp = msTaxList.Where(obj => obj.StartDate < ds).OrderByDescending(obj => obj.StartDate);
        //        MsTax tax1 = tmp.First();
        //        tmp = msTaxList.Where(obj => obj.StartDate > ds).OrderBy(obj => obj.StartDate);
        //        MsTax tax2 = tmp.First();

        //        ret.Add(tax1.TaxRate, GetRecordsByDateAndMsVesselID(dbConnect, loginUser, ds, tax2.StartDate, msVesselId));

        //        ret.Add(tax2.TaxRate, GetRecordsByDateAndMsVesselID(dbConnect, loginUser, tax2.StartDate, de, msVesselId));

        //    }
        //    else
        //    {
        //        tmp = msTaxList.Where(obj => obj.StartDate < ds).OrderByDescending(obj => obj.StartDate);
        //        MsTax tax = tmp.First();

        //        ret.Add(tax.TaxRate, GetRecordsByDateAndMsVesselID(dbConnect, loginUser, ds, de, msVesselId));
        //    }
        //    return ret;
        //}
        public static SortedDictionary<int, List<SiJunbikin>> Get科目別集計表データ(MsUser loginUser, DateTime date, int msVesselId)
        {
            return Get科目別集計表データ(null, loginUser, date, msVesselId);
        }
        public static SortedDictionary<int, List<SiJunbikin>> Get科目別集計表データ(DBConnect dbConnect, MsUser loginUser, DateTime date, int msVesselId)
        {
            SortedDictionary<int, List<SiJunbikin>> ret = new SortedDictionary<int, List<SiJunbikin>>();

            List<MsTax> msTaxList = MsTax.GetRecords(dbConnect, loginUser);

            // 対象月次の開始日、終了日
            DateTime ds = MsSiGetsujiShimeBi.ToFrom_船員_月次締日(dbConnect, loginUser, date);
            DateTime de = MsSiGetsujiShimeBi.ToTo_船員_月次締日(dbConnect, loginUser, date);

            List<SiJunbikin> all = GetRecordsByDateAndMsVesselID(dbConnect, loginUser, ds, de, msVesselId);
            List<int> taxIds = new List<int>();
            if (all.Count() == 0)
            {
               taxIds.Add(0);
            }
            else
            {
                taxIds = all.Select(obj => obj.MsTaxID).Distinct().OrderBy(obj => obj).ToList();
            }

            foreach(int taxId in taxIds)
            {
                if (taxId <= 0)
                {
                    //var tmp = msTaxList.Where(obj => obj.StartDate > ds).Where(obj => obj.StartDate < de);
                    //if (tmp.Count() > 0)
                    //{
                    //    // 月次の範囲に消費税開始されたものがある場合
                    //    // 切替のタイミング
                    //    tmp = msTaxList.Where(obj => obj.StartDate < ds).OrderByDescending(obj => obj.StartDate);
                    //    MsTax tax1 = tmp.First();
                    //    tmp = msTaxList.Where(obj => obj.StartDate > ds && obj.StartDate < de).OrderBy(obj => obj.StartDate);
                    //    MsTax tax2 = tmp.First();
                    //    ret.Add(tax1.MsTaxID, all.Where(obj => obj.MsTaxID == taxId && obj.JunbikinDate < tax2.StartDate).ToList());

                    //    ret.Add(tax2.MsTaxID, all.Where(obj => obj.MsTaxID == taxId && obj.JunbikinDate >= tax2.StartDate).ToList());
                    //}
                    //else
                    //{
                    //    tmp = msTaxList.Where(obj => obj.StartDate < ds).OrderByDescending(obj => obj.StartDate);
                    //    MsTax tax = tmp.First();

                    //    ret.Add(tax.MsTaxID, all.Where(obj => obj.MsTaxID == taxId).ToList());
                    //}

                    // 後ろ優先でリストを作成
                    DateTime afterTaxStartDate = DateTime.MinValue;
                    var sortedList = msTaxList.Where(obj => obj.StartDate < de).OrderByDescending(obj => obj.StartDate).ThenByDescending(obj => obj.MsTaxID);
                    foreach(MsTax tax in sortedList)
                    {
                        if (afterTaxStartDate == DateTime.MinValue)
                        {
                            // 必ずここを通る
                            if (ret.ContainsKey(tax.MsTaxID))
                            {
                                ret[tax.MsTaxID].AddRange(all.Where(obj => obj.MsTaxID == taxId && obj.JunbikinDate >= tax.StartDate).ToList());
                            }
                            else
                            {
                                ret.Add(tax.MsTaxID, all.Where(obj => obj.MsTaxID == taxId && obj.JunbikinDate >= tax.StartDate).ToList());
                            }
                        }
                        else if (afterTaxStartDate == tax.StartDate)
                        {
                            // 開始日が同じものの場合（2019/10/01対応）
                            if (ret.ContainsKey(tax.MsTaxID) == false)
                                ret.Add(tax.MsTaxID, new List<SiJunbikin>());
                        }
                        else if (afterTaxStartDate < ds)
                        {
                            // 一つ後（処理上はDecendingなので前）の開始日が、
                            // 月次開始より前の場合、終わる
                            break;
                        }
                        else
                        {
                            if (ret.ContainsKey(tax.MsTaxID))
                            {
                                ret[tax.MsTaxID].AddRange(all.Where(obj => obj.MsTaxID == taxId && obj.JunbikinDate >= tax.StartDate && obj.JunbikinDate < afterTaxStartDate).ToList());
                            }
                            else
                            {
                                ret.Add(tax.MsTaxID, all.Where(obj => obj.MsTaxID == taxId && obj.JunbikinDate >= tax.StartDate && obj.JunbikinDate < afterTaxStartDate).ToList());
                            }
                        
                        }
                        afterTaxStartDate = tax.StartDate;
                    }
                }
                else
                {
                    // MsTaxIDセットされている場合
                    if (ret.ContainsKey(taxId))
                    {
                        ret[taxId].AddRange(all.Where(obj => obj.MsTaxID == taxId).ToList());
                    }
                    else
                    {
                        ret.Add(taxId, all.Where(obj => obj.MsTaxID == taxId).ToList());
                    }
                }

            }


            return ret;
        }

        // 2014.03: 2013年度改造
        public static List<SiJunbikin> GetRecordsByDateAndMsVesselID(DBConnect dbConnect, MsUser loginUser, DateTime sd, DateTime ed, int msVesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), "ByMsVesselID");

            List<SiJunbikin> ret = new List<SiJunbikin>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiJunbikin> mapping = new MappingBase<SiJunbikin>();

            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiJunbikin), "ByStartEnd");
            Params.Add(new DBParameter("START_DATE", sd));
            Params.Add(new DBParameter("END_DATE", ed));

            SQL += " ORDER BY JUNBIKIN_DATE";

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }
      


        #region ISyncTable メンバ

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_JUNBIKIN_ID", SiJunbikinID));

            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_SI_HIYOU_KAMOKU_ID", MsSiHiyouKamokuID));
            Params.Add(new DBParameter("MS_SI_DAIKOUMOKU_ID", MsSiDaikoumokuID));
            Params.Add(new DBParameter("MS_SI_MEISAI_ID", MsSiMeisaiID));
            Params.Add(new DBParameter("MS_SI_KAMOKU_ID", MsSiKamokuId));
            Params.Add(new DBParameter("TOUROKU_USER_ID", TourokuUserID));

            Params.Add(new DBParameter("JUNBIKIN_DATE", JunbikinDate));
            Params.Add(new DBParameter("KINGAKU_OUT", KingakuOut));
            Params.Add(new DBParameter("TAX_OUT", TaxOut));
            Params.Add(new DBParameter("KINGAKU_IN", KingakuIn));
            Params.Add(new DBParameter("TAX_IN", TaxIn));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("FURIKAE_FLAG", FurikaeFlag));

            // 2019.10.08 追加
            Params.Add(new DBParameter("MS_TAX_ID", MsTaxID));

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
            return true;
        }

        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_SI_HIYOU_KAMOKU_ID", MsSiHiyouKamokuID));
            Params.Add(new DBParameter("MS_SI_DAIKOUMOKU_ID", MsSiDaikoumokuID));
            Params.Add(new DBParameter("MS_SI_MEISAI_ID", MsSiMeisaiID));
            Params.Add(new DBParameter("MS_SI_KAMOKU_ID", MsSiKamokuId));
            Params.Add(new DBParameter("TOUROKU_USER_ID", TourokuUserID));

            Params.Add(new DBParameter("JUNBIKIN_DATE", JunbikinDate));
            Params.Add(new DBParameter("KINGAKU_OUT", KingakuOut));
            Params.Add(new DBParameter("TAX_OUT", TaxOut));
            Params.Add(new DBParameter("KINGAKU_IN", KingakuIn));
            Params.Add(new DBParameter("TAX_IN", TaxIn));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("FURIKAE_FLAG", FurikaeFlag));

            // 2019.10.08 追加
            Params.Add(new DBParameter("MS_TAX_ID", MsTaxID));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_JUNBIKIN_ID", SiJunbikinID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", SiJunbikinID));

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
            Params.Add(new DBParameter("PK", SiJunbikinID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }
        #endregion

        public bool IsNew()
        {
            return SiJunbikinID == null;
        }
    }
}
