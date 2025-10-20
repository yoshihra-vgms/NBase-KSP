using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ORMapping.Atts;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("BG_HANKANHI")]
    public class BgHankanhi
    {
        #region データメンバ
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("BG_HANKANHI_ID")]
        public int BgHankanhiID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("YOSAN_HEAD_ID")]
        public int YosanHeadID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("YEAR")]
        public int Year { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("EIGYO_KISO")]
        public decimal EigyoKiso { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("KANRI_KISO")]
        public decimal KanriKiso { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("NENDO_HANKANHI")]
        public decimal NendoHankanhi { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("KEIEISHIDOURYOU")]
        public decimal Keieishidouryou { get; set; }


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


        public static List<BgHankanhi> GetRecords(MsUser loginUser)
        {
            List<BgHankanhi> ret = new List<BgHankanhi>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgHankanhi), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgHankanhi), "ByAll");

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgHankanhi> mapping = new MappingBase<BgHankanhi>();

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<BgHankanhi> GetRecordsByYosanHeadID(MsUser loginUser, int yosanHeadID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgHankanhi), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgHankanhi), "ByYosanHeadID");

            List<BgHankanhi> ret = new List<BgHankanhi>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgHankanhi> mapping = new MappingBase<BgHankanhi>();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadID));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static BgHankanhi GetRecord(MsUser loginUser, int bgHankanhiID)
        {
            List<BgHankanhi> ret = new List<BgHankanhi>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgHankanhi), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgHankanhi), "ByHankanhiID");

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgHankanhi> mapping = new MappingBase<BgHankanhi>();

            Params.Add(new DBParameter("BG_HANKANHI_ID", bgHankanhiID));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        /// <summary>
        /// 予算ヘッドIDとYearで取得
        /// 引数：ユーザー、予算ヘッドＩＤ，年
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="headid"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static BgHankanhi GetRecordByYosanHeadIDYear(MsUser loginUser, int headid, int year)
        {
            return GetRecordByYosanHeadIDYear(null, loginUser, headid, year);
        }


        /// <summary>
        /// 予算ヘッドIDとYearで取得
        /// 引数：ユーザー、予算ヘッドＩＤ，年
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="headid"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static BgHankanhi GetRecordByYosanHeadIDYear(DBConnect dbConnect, MsUser loginUser, int headid, int year)
        {
            List<BgHankanhi> ret = new List<BgHankanhi>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgHankanhi), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgHankanhi), "ByByYosanHeadIDYear");

            ParameterConnection Params = new ParameterConnection();
            MappingBase<BgHankanhi> mapping = new MappingBase<BgHankanhi>();

            //パラメーター設定
            Params.Add(new DBParameter("YOSAN_HEAD_ID", headid));
            Params.Add(new DBParameter("YEAR", year));

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgHankanhi), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("YEAR", this.Year));
            Params.Add(new DBParameter("EIGYO_KISO", this.EigyoKiso));
            Params.Add(new DBParameter("KANRI_KISO", this.KanriKiso));
            Params.Add(new DBParameter("NENDO_HANKANHI", this.NendoHankanhi));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("TS", this.Ts));

            Params.Add(new DBParameter("KEIEISHIDOURYOU", this.Keieishidouryou));

            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }


        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }
        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgHankanhi), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("YEAR", this.Year));
            Params.Add(new DBParameter("EIGYO_KISO", this.EigyoKiso));
            Params.Add(new DBParameter("KANRI_KISO", this.KanriKiso));
            Params.Add(new DBParameter("NENDO_HANKANHI", this.NendoHankanhi));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));

            Params.Add(new DBParameter("KEIEISHIDOURYOU", this.Keieishidouryou));

            Params.Add(new DBParameter("BG_HANKANHI_ID", this.BgHankanhiID));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        public static bool InsertRecords_コピー(DBConnect dbConnect, MsUser loginUser,
                                                                    int yosanHeadID, int lastYosanHeadId, int minYear)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgHankanhi), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgHankanhi), "ByYear");

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadID));
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("LAST_YOSAN_HEAD_ID", lastYosanHeadId));
            Params.Add(new DBParameter("MIN_YEAR", minYear));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }
    }
}
