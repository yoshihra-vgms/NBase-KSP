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
    [Serializable]
    [DataContract()]
    [TableAttribute("SI_KYUYO_TEATE")]
    public class SiKyuyoTeate : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 給与手当ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_KYUYO_TEATE_ID", true)]
        public string SiKyuyoTeateID { get; set; }


        /// <summary>
        /// 船員ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

        /// <summary>
        /// 船員職名ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID")]
        public int MsSiShokumeiID { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 年月
        /// </summary>
        [DataMember]
        [ColumnAttribute("YM")]
        public string YM { get; set; }




        /// <summary>
        /// 給与手当ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_KYUYO_TEATE_ID")]
        public int MsSiKyuyoTeateID { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        [DataMember]
        [ColumnAttribute("TANKA")]
        public int Tanka { get; set; }

        /// <summary>
        /// 開始日
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_DATE")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 終了日
        /// </summary>
        [DataMember]
        [ColumnAttribute("END_DATE")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 日数
        /// </summary>
        [DataMember]
        [ColumnAttribute("DAYS")]
        public int Days { get; set; }

        /// <summary>
        /// 金額（NBaseHonsenでの入力）
        /// </summary>
        [DataMember]
        [ColumnAttribute("HONSEN_KINGAKU")]
        public int HonsenKingaku { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("KINGAKU")]
        public int Kingaku { get; set; }

        /// <summary>
        /// キャンセルフラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("CANCEL_FLAG")]
        public int CancelFlag { get; set; }



        /// <summary>
        /// 船員名 (LEFT JOIN)
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENIN_NAME")]
        public string SeninName { get; set; }


        #region 共通
        /// <summary>
        /// 同期:ユーザキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_KEY")]
        public string UserKey { get; set; }

        /// <summary>
        /// 同期:データNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }
        
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
        /// 更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_USER_ID")]
        public string RenewUserID { get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_DATE")]
        public DateTime RenewDate { get; set; }

        /// <summary>
        /// 排他制御
        /// </summary>
        [DataMember]
        [ColumnAttribute("TS")]
        public string Ts { get; set; }
        
        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }

        #endregion

        #endregion

        public int GetYear
        {
            get
            {
                if (YM == null)
                {
                    return DateTime.Today.Year;
                }
                else
                {
                    string y = YM.Substring(0, 4);
                    return int.Parse(y);
                }
            }
        }
        public int GetMonth
        {
            get
            {
                if (YM == null)
                {
                    return DateTime.Today.Month;
                }
                else
                {
                    string m = YM.Substring(4, 2);
                    return int.Parse(m);
                }
            }
        }
        public bool IsNew()
        {
            return SiKyuyoTeateID == null;
        }

        public SiKyuyoTeate()
        {
            SiKyuyoTeateID = null;
        }

        public static SiKyuyoTeate GetRecord(NBaseData.DAC.MsUser loginUser, string siKyuyoTeateId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKyuyoTeate), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKyuyoTeate), "BySiKyuyoTeateID");
            List<SiKyuyoTeate> ret = new List<SiKyuyoTeate>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_KYUYO_TEATE_ID", siKyuyoTeateId));
            MappingBase<SiKyuyoTeate> mapping = new MappingBase<SiKyuyoTeate>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            if (ret.Count() > 0)
                return ret.First();
            else
                return null;
        }

        public static List<SiKyuyoTeate> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKyuyoTeate), "GetRecords");
            List<SiKyuyoTeate> ret = new List<SiKyuyoTeate>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKyuyoTeate> mapping = new MappingBase<SiKyuyoTeate>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiKyuyoTeate> GetRecordsByYearMonth(MsUser loginUser, string ym)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKyuyoTeate), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKyuyoTeate), "ByYM");

            List<SiKyuyoTeate> ret = new List<SiKyuyoTeate>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YM", ym));
            MappingBase<SiKyuyoTeate> mapping = new MappingBase<SiKyuyoTeate>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<SiKyuyoTeate> GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKyuyoTeate), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKyuyoTeate), "ByMsSeninID");

            List<SiKyuyoTeate> ret = new List<SiKyuyoTeate>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            MappingBase<SiKyuyoTeate> mapping = new MappingBase<SiKyuyoTeate>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKyuyoTeate), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("SI_KYUYO_TEATE_ID", SiKyuyoTeateID));

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("YM", YM));
            Params.Add(new DBParameter("MS_SI_KYUYO_TEATE_ID", MsSiKyuyoTeateID));
            Params.Add(new DBParameter("TANKA", Tanka));
            Params.Add(new DBParameter("START_DATE", StartDate));
            Params.Add(new DBParameter("END_DATE", EndDate));
            Params.Add(new DBParameter("DAYS", Days));
            Params.Add(new DBParameter("HONSEN_KINGAKU", HonsenKingaku));
            Params.Add(new DBParameter("KINGAKU", Kingaku));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKyuyoTeate), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("YM", YM));
            Params.Add(new DBParameter("MS_SI_KYUYO_TEATE_ID", MsSiKyuyoTeateID));
            Params.Add(new DBParameter("TANKA", Tanka));
            Params.Add(new DBParameter("START_DATE", StartDate));
            Params.Add(new DBParameter("END_DATE", EndDate));
            Params.Add(new DBParameter("DAYS", Days));
            Params.Add(new DBParameter("HONSEN_KINGAKU", HonsenKingaku));
            Params.Add(new DBParameter("KINGAKU", Kingaku));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("CANCEL_FLAG", CancelFlag));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("SI_KYUYO_TEATE_ID", SiKyuyoTeateID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        #region ISyncTable メンバ

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", SiKyuyoTeateID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
