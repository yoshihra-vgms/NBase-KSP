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
    [TableAttribute("PT_KANIDOUSEI_INFO")]
    public class PtKanidouseiInfo : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 本船更新情報ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("PT_KANIDOUSEI_INFO_ID", true)]
        public string PtKanidouseiInfoId { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 日付
        /// </summary>
        [DataMember]
        [ColumnAttribute("EVENT_DATE")]
        public DateTime EventDate { get; set; }

        /// <summary>
        /// コマ
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOMA")]
        public int Koma { get; set; }

        /// <summary>
        /// 場所ID_1
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BASHO_ID")]
        public string MsBashoId { get; set; }

        /// <summary>
        /// 基地ID_1
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KICHI_ID")]
        public string MsKitiId { get; set; }

        /// <summary>
        /// 簡易動静情報種別ID_1
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KANIDOUSEI_INFO_SHUBETU_ID")]
        public string MsKanidouseiInfoShubetuId { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }
        
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
        public Int64 DataNo { get; set; }       //090731 anahara

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
        /// 連携フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENKEI_FLAG")]
        public int RenkeiFlag { get; set; }

        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }
        /// <summary>
        /// 船マスタ >> 船名
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_NAME")]
        public string VesselName { get; set; }

        /// <summary>
        /// 簡易動静情報種別マスタ >> 種別名_1
        /// </summary>
        [DataMember]
        [ColumnAttribute("KANIDOUSEI_INFO_SHUBETU_NAME")]
        public string KanidouseiInfoShubetuName { get; set; }

        /// <summary>
        /// 場所マスタ >> 場所名_1
        /// </summary>
        [DataMember]
        [ColumnAttribute("BASHO_NAME")]
        public string BashoName { get; set; }

        /// <summary>
        /// 基地マスタ >> 基地名_1
        /// </summary>
        [DataMember]
        [ColumnAttribute("KICHI_NAME")]
        public string KitiName { get; set; }

        [DataMember]
        [ColumnAttribute("DJ_DOUSEI_ID")]
        public string DjDouseiID { get; set; }

        /// <summary>
        /// Honsen確認日付
        /// </summary>
        [DataMember]
        [ColumnAttribute("HONSEN_CHECK_DATE")]
        public DateTime HonsenCheckDate { get; set; }

        /// <summary>
        /// 貨物ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CARGO_ID")]
        public int MsCargoID { get; set; }

        /// <summary>
        /// 貨物名
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARGO_NAME")]
        public string MsCargoName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        [ColumnAttribute("QTTY")]
        public decimal Qtty { get; set; }

        #endregion

        public PtKanidouseiInfo()
        {
            PtKanidouseiInfoId = "";
            Bikou = "";
        }

        public override int GetHashCode()
        {
            StringBuilder sb = new StringBuilder();

            PropertyInfo[] propertyInfos = typeof(PtKanidouseiInfo).GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object obj;
                obj = propertyInfo.GetValue(this, null);
                if (obj != null)
                {
                    sb.Append(obj.ToString());
                }
            }


            return sb.ToString().GetHashCode();
        }

        public static List<PtKanidouseiInfo> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), MethodBase.GetCurrentMethod());
            List<PtKanidouseiInfo> ret = new List<PtKanidouseiInfo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<PtKanidouseiInfo> mapping = new MappingBase<PtKanidouseiInfo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        //船ID
        public static List<PtKanidouseiInfo> GetRecordsByMsVesselID(NBaseData.DAC.MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), MethodBase.GetCurrentMethod());
            List<PtKanidouseiInfo> ret = new List<PtKanidouseiInfo>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<PtKanidouseiInfo> mapping = new MappingBase<PtKanidouseiInfo>();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }




        public static PtKanidouseiInfo GetRecordByPtKanidouseiInfoId(NBaseData.DAC.MsUser loginUser, string PtKanidouseiInfoId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "ByPtKanidouseiInfoId");
            List<PtKanidouseiInfo> ret = new List<PtKanidouseiInfo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PT_KANIDOUSEI_INFO_ID", PtKanidouseiInfoId));
            MappingBase<PtKanidouseiInfo> mapping = new MappingBase<PtKanidouseiInfo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<PtKanidouseiInfo> GetRecordByEventDate(NBaseData.DAC.MsUser loginUser, DateTime fromDatetime, DateTime toDatetime, int vesselId = -1)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "GetRecords");
            if (vesselId > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "ByEventDateVessel");
            }
            else
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "ByEventDate");
            }

            // 2013.05 : 
            //SQL += " order by PT_KANIDOUSEI_INFO.EVENT_DATE";
            SQL += " order by PT_KANIDOUSEI_INFO.EVENT_DATE, PT_KANIDOUSEI_INFO.DATA_NO";

            List<PtKanidouseiInfo> work = new List<PtKanidouseiInfo>();
            List<PtKanidouseiInfo> ret = new List<PtKanidouseiInfo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("EVENT_DATE1", fromDatetime));
            Params.Add(new DBParameter("EVENT_DATE2", toDatetime));
            if (vesselId > 0)
            {
                Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));
            }
            MappingBase<PtKanidouseiInfo> mapping = new MappingBase<PtKanidouseiInfo>();
            work = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            foreach (PtKanidouseiInfo info in work)
            {
                if (info.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.積みID ||
                    info.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.揚げID)
                {
                    //DjDouseiCargo cargo = DjDouseiCargo.GetRecordsByDjDouseiIdAtFirst(loginUser, info.DjDouseiID);
                    //if (cargo != null)
                    //{
                    //    info.MsCargoName = cargo.MsCargoName;
                    //    info.Qtty = cargo.Qtty;
                    //}
                    DjDousei dousei = DjDousei.GetRecord(loginUser, info.DjDouseiID);
                    if (dousei != null)
                    {
                        if (dousei.DeleteFlag == 1)
                        {
                            // 動静情報が削除されている場合、簡易動静を表示対象としない
                        }
                        else
                        {
                            if (dousei.DjDouseiCargos != null && dousei.DjDouseiCargos.Count > 0)
                            {
                                info.MsCargoName = dousei.DjDouseiCargos[0].MsCargoName;
                                info.Qtty = dousei.DjDouseiCargos[0].Qtty;
                            }
                            if (dousei.ResultDjDouseiCargos != null && dousei.ResultDjDouseiCargos.Count > 0)
                            {
                                info.MsCargoName = dousei.ResultDjDouseiCargos[0].MsCargoName;
                                info.Qtty = dousei.ResultDjDouseiCargos[0].Qtty;
                            }
                            ret.Add(info);
                        }
                    }
                    else
                    {
                        ret.Add(info);
                    }
                }
                else
                {
                    ret.Add(info);
                }
            }

            return ret;
        }

        ////public static List<PtKanidouseiInfo> GetRecordsByEventDateVessel(NBaseData.DAC.MsUser loginUser,int MsVesselID, DateTime eventDate)
        //public static List<PtKanidouseiInfo> GetRecordsByEventDateVessel(NBaseData.DAC.MsUser loginUser, int MsVesselID, DateTime fromDatetime, DateTime toDatetime)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "GetRecords");
        //    SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "ByEventDateVessel");

        //    List<PtKanidouseiInfo> work = new List<PtKanidouseiInfo>();
        //    List<PtKanidouseiInfo> ret = new List<PtKanidouseiInfo>();
        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
        //    //Params.Add(new DBParameter("EVENT_DATE", eventDate));
        //    Params.Add(new DBParameter("EVENT_DATE1", fromDatetime));
        //    Params.Add(new DBParameter("EVENT_DATE2", toDatetime));
        //    MappingBase<PtKanidouseiInfo> mapping = new MappingBase<PtKanidouseiInfo>();
        //    work = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

        //    foreach (PtKanidouseiInfo info in work)
        //    {
        //        if (info.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.積みID ||
        //            info.MsKanidouseiInfoShubetuId == MsKanidouseiInfoShubetu.揚げID)
        //        {
        //            //DjDouseiCargo cargo = DjDouseiCargo.GetRecordsByDjDouseiIdAtFirst(loginUser, info.DjDouseiID);
        //            //if (cargo != null)
        //            //{
        //            //    info.MsCargoName = cargo.MsCargoName;
        //            //    info.Qtty = cargo.Qtty;
        //            //}
        //            DjDousei dousei = DjDousei.GetRecord(loginUser, info.DjDouseiID);
        //            if (dousei != null)
        //            {
        //                if (dousei.DeleteFlag == 1)
        //                {

        //                }
        //                else
        //                {
        //                    if (dousei.DjDouseiCargos != null && dousei.DjDouseiCargos.Count > 0)
        //                    {
        //                        info.MsCargoName = dousei.DjDouseiCargos[0].MsCargoName;
        //                        info.Qtty = dousei.DjDouseiCargos[0].Qtty;
        //                    }
        //                    if (dousei.ResultDjDouseiCargos != null && dousei.ResultDjDouseiCargos.Count > 0)
        //                    {
        //                        info.MsCargoName = dousei.ResultDjDouseiCargos[0].MsCargoName;
        //                        info.Qtty = dousei.ResultDjDouseiCargos[0].Qtty;
        //                    }
        //                    ret.Add(info);
        //                }

        //            }
        //            else
        //            {
        //                ret.Add(info);
        //            }
        //        }
        //        else
        //        {
        //            ret.Add(info);
        //        }
        //    }

        //    return ret;
        //}

        public static PtKanidouseiInfo GetRecordByDjDouseiID(NBaseData.DAC.MsUser loginUser, string djDouseiID)
        {
            return GetRecordByDjDouseiID(null, loginUser, djDouseiID);
        }
        public static PtKanidouseiInfo GetRecordByDjDouseiID(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string djDouseiID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "ByDjDouseiID");

            List<PtKanidouseiInfo> ret = new List<PtKanidouseiInfo>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("DJ_DOUSEI_ID", djDouseiID));
            MappingBase<PtKanidouseiInfo> mapping = new MappingBase<PtKanidouseiInfo>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count > 0)
            {
                return ret[0];
            }
            return null;
        }



        public static List<PtKanidouseiInfo> GetRecordsByMsKichiID(NBaseData.DAC.MsUser loginUser, string ms_kichi_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "ByMsKichiID");

            List<PtKanidouseiInfo> ret = new List<PtKanidouseiInfo>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_KICHI_ID", ms_kichi_id));

            MappingBase<PtKanidouseiInfo> mapping = new MappingBase<PtKanidouseiInfo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<PtKanidouseiInfo> GetRecordsByMsBashoID(NBaseData.DAC.MsUser loginUser, string ms_basho_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "ByMsBashoID");

            List<PtKanidouseiInfo> ret = new List<PtKanidouseiInfo>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_BASHO_ID", ms_basho_id));

            MappingBase<PtKanidouseiInfo> mapping = new MappingBase<PtKanidouseiInfo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        /// <summary>
        /// 重複チェックを行う
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="msVesselID"></param>
        /// <param name="eventDate"></param>
        /// <param name="Koma"></param>
        /// <returns>重複している場合はtrueを返す</returns>
        public static bool 重複チェック(NBaseData.DAC.MsUser loginUser, string PtKanidouseiInfoId, int msVesselID, DateTime eventDate, int Koma)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "重複チェック");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));
            Params.Add(new DBParameter("EVENT_DATE", eventDate));
            Params.Add(new DBParameter("KOMA", Koma));
            Params.Add(new DBParameter("PT_KANIDOUSEI_INFO_ID", PtKanidouseiInfoId));

            List<PtKanidouseiInfo> ret = new List<PtKanidouseiInfo>();
            MappingBase<PtKanidouseiInfo> mapping = new MappingBase<PtKanidouseiInfo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return false;
            return true;
        }
        
        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbcone, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PT_KANIDOUSEI_INFO_ID", PtKanidouseiInfoId));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("EVENT_DATE", EventDate));
            Params.Add(new DBParameter("KOMA", Koma));
            Params.Add(new DBParameter("MS_BASHO_ID", MsBashoId));
            Params.Add(new DBParameter("MS_KICHI_ID", MsKitiId));
            Params.Add(new DBParameter("MS_KANIDOUSEI_INFO_SHUBETU_ID", MsKanidouseiInfoShubetuId));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("DJ_DOUSEI_ID", DjDouseiID));
            Params.Add(new DBParameter("HONSEN_CHECK_DATE", HonsenCheckDate));
            Params.Add(new DBParameter("MS_CARGO_ID", MsCargoID));
            Params.Add(new DBParameter("QTTY", Qtty));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("RENKEI_FLAG",RenkeiFlag));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            //cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            cnt = DBConnect.ExecuteNonQuery(dbcone, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }


        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }

        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();          
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("EVENT_DATE", EventDate));
            Params.Add(new DBParameter("KOMA", Koma));
            Params.Add(new DBParameter("MS_BASHO_ID", MsBashoId));
            Params.Add(new DBParameter("MS_KICHI_ID", MsKitiId));
            Params.Add(new DBParameter("MS_KANIDOUSEI_INFO_SHUBETU_ID", MsKanidouseiInfoShubetuId));
            Params.Add(new DBParameter("BIKOU", Bikou));
            Params.Add(new DBParameter("DJ_DOUSEI_ID", DjDouseiID));
            Params.Add(new DBParameter("HONSEN_CHECK_DATE", HonsenCheckDate));
            Params.Add(new DBParameter("MS_CARGO_ID", MsCargoID));
            Params.Add(new DBParameter("QTTY", Qtty));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("RENKEI_FLAG",RenkeiFlag));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("PT_KANIDOUSEI_INFO_ID", PtKanidouseiInfoId));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool DeleteRecord(NBaseData.DAC.MsUser loginUser)
        {
            return DeleteRecord(null, loginUser);
        }


        public bool DeleteRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("PT_KANIDOUSEI_INFO_ID", PtKanidouseiInfoId));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ


        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", PtKanidouseiInfoId));

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
            Params.Add(new DBParameter("PK", PtKanidouseiInfoId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
        public static bool DeleteByVoyageNo(DBConnect dbConnect, MsUser loginUser, int msVesselID, string voyageNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));
            Params.Add(new DBParameter("VOYAGE_NO", voyageNo));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;

        }

        public static List<PtKanidouseiInfo> GetRecordsByVoyageNo(DBConnect dbConnect, MsUser loginUser, int msVesselID, string voyageNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(PtKanidouseiInfo), "ByVoyageNo");

            List<PtKanidouseiInfo> ret = new List<PtKanidouseiInfo>();
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselID));
            Params.Add(new DBParameter("VOYAGE_NO", voyageNo));

            MappingBase<PtKanidouseiInfo> mapping = new MappingBase<PtKanidouseiInfo>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }
    }
}
