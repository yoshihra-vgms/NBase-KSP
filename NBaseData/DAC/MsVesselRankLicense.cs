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
    [TableAttribute("MS_VESSEL_RANK_LICENSE")]
    public class MsVesselRankLicense
    {
        #region データメンバ

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID", true)]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 職名ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID")]
        public int MsSiShokumeiID { get; set; }

        /// <summary>
        /// 免許／免状ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_MENJOU_ID")]
        public int MsSiMenjouID { get; set; }

        /// <summary>
        /// 免許／免状種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_MENJOU_KIND_ID")]
        public int MsSiMenjouKindID { get; set; }



        #region 共通項目

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

        #endregion



        public static List<MsVesselRankLicense> GetRecords(MsUser loginUser)
        {
            return GetRecords(null, loginUser);
        }
        public static List<MsVesselRankLicense> GetRecords(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsVesselRankLicense), MethodBase.GetCurrentMethod());

            List<MsVesselRankLicense> ret = new List<MsVesselRankLicense>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsVesselRankLicense> mapping = new MappingBase<MsVesselRankLicense>();
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

        #region ISyncTable メンバ

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("MS_SI_MENJOU_ID", MsSiMenjouID));
            Params.Add(new DBParameter("MS_SI_MENJOU_KIND_ID", MsSiMenjouKindID));

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

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_VESSEL_ID", MsVesselID));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("MS_SI_MENJOU_ID", MsSiMenjouID));
            Params.Add(new DBParameter("MS_SI_MENJOU_KIND_ID", MsSiMenjouKindID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            return true;
        }

        #endregion

    }
}
