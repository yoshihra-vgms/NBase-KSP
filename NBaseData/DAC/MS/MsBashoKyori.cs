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
    /// 距離マスタ
    /// </summary>
    [DataContract()]
    [TableAttribute("MS_BASHO_KYORI")]
    public class MsBashoKyori
    {
        #region データメンバ
        /// <summary>
        /// 場所距離ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BASHO_KYORI_ID")]
        public string MsBashoKyoriID { get; set; }

        /// <summary>
        /// 場所1ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BASHO_ID1")]
        public string MsBashoID1 { get; set; }

        /// <summary>
        /// 場所1名
        /// </summary>
        [DataMember]
        [ColumnAttribute("BASHO_NAME1")]
        public string BashoName1 { get; set; }

        /// <summary>
        /// 場所2ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_BASHO_ID2")]
        public string MsBashoID2 { get; set; }

        /// <summary>
        /// 場所2名
        /// </summary>
        [DataMember]
        [ColumnAttribute("BASHO_NAME2")]
        public string BashoName2 { get; set; }

        /// <summary>
        /// 距離
        /// </summary>
        [DataMember]
        [ColumnAttribute("KYORI")]
        public double Kyori { get; set; }

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

        public static MsBashoKyori GetRecord(MsUser loginUser, string BashoNo1,string BashoNo2)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBashoKyori), "GetRecord");

            List<MsBashoKyori> ret = new List<MsBashoKyori>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("B1", BashoNo1));
            Params.Add(new DBParameter("B2", BashoNo2));

            MappingBase<MsBashoKyori> mapping = new MappingBase<MsBashoKyori>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count > 0)
            {
                return ret[0];
            }
            return null;
        }

        public static List<MsBashoKyori> GetRecordsByKyori1Kyori2(MsUser loginUser, string BashoNo1, string BashoNo2)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBashoKyori), "GetRecords");
            SQL += SqlMapper.SqlMapper.GetSql(typeof(MsBashoKyori), "GetRecordsByKyori1Kyori2");

            ParameterConnection Params = new ParameterConnection();
            if (BashoNo1 != "")
            {
                SQL += "and (BASHO1.MS_BASHO_ID = :B1 or BASHO2.MS_BASHO_ID = :B1)";
                Params.Add(new DBParameter("B1", BashoNo1));
            }
            if (BashoNo2 != "")
            {
                SQL += "and (BASHO1.MS_BASHO_ID = :B2 or BASHO2.MS_BASHO_ID = :B2)";
                Params.Add(new DBParameter("B2", BashoNo2));
            }

            // 2014.12.26 コメントあり
            //SQL += "and BASHO1.MS_BASHO_ID != BASHO2.MS_BASHO_ID";

            List<MsBashoKyori> ret = new List<MsBashoKyori>();

            MappingBase<MsBashoKyori> mapping = new MappingBase<MsBashoKyori>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        //MS_BASHO_ID1とMS_BASHO_ID2の両方をチェックする
        public static List<MsBashoKyori> GetRecordsByMsBashoID(MsUser loginUser, string ms_basho_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBashoKyori), "GetRecords");
            SQL += SqlMapper.SqlMapper.GetSql(typeof(MsBashoKyori), "ByMsBashoID");

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_BASHO_ID1", ms_basho_id));
            Params.Add(new DBParameter("MS_BASHO_ID2", ms_basho_id));


            List<MsBashoKyori> ret = new List<MsBashoKyori>();

            MappingBase<MsBashoKyori> mapping = new MappingBase<MsBashoKyori>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBashoKyori), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_BASHO_KYORI_ID", MsBashoKyoriID));
            Params.Add(new DBParameter("MS_BASHO_ID1", MsBashoID1));
            Params.Add(new DBParameter("MS_BASHO_ID2", MsBashoID2));
            Params.Add(new DBParameter("KYORI", Kyori));

            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsBashoKyori), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("KYORI", Kyori));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_BASHO_KYORI_ID", MsBashoKyoriID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

    }
}
