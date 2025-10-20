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
    [TableAttribute("MS_THI_IRAI_SHOUSAI")]
    public class MsThiIraiShousai : ISyncTable
    {
        // DB要素(memo)
        //MS_THI_IRAI_SHOUSAI_ID         VARCHAR2(40) NOT NULL,
        //THI_IRAI_SHOUSAI_NAME          NVARCHAR2(10),
        //SEND_FLAG                      NUMBER(1,0) DEFAULT 0 NOT NULL,
        //VESSEL_ID                      NUMBER(4,0) NOT NULL,
        //DATA_NO                        NUMBER(13,0),
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             TIMESTAMP(6),
        #region データメンバ

        /// <summary>
        /// 手配依頼詳細種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_THI_IRAI_SHOUSAI_ID", true)]
        public string MsThiIraiShousaiID { get; set; }

        /// <summary>
        /// 手配依頼詳細種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("THI_IRAI_SHOUSAI_NAME")]
        public string ThiIraiShousaiName { get; set; }

        /// <summary>
        /// 手配依頼種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_THI_IRAI_SBT_ID")]
        public string MsThiIraiSbtID { get; set; }

        /// <summary>
        /// 表示順
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOW_ORDER")]
        public int ShowOrder { get; set; }

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
        /// 更新者(UserID)
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

        public enum ThiIraiShousaiEnum { 部品購入 = 1, 入渠, 荷役資材 };
        private static readonly Dictionary<string, string> idToName =
                                                            new Dictionary<string, string>
                                                                {{((int)ThiIraiShousaiEnum.部品購入).ToString(), ThiIraiShousaiEnum.部品購入.ToString()},
                                                                 {((int)ThiIraiShousaiEnum.入渠).ToString(), ThiIraiShousaiEnum.入渠.ToString()},
                                                                 {((int)ThiIraiShousaiEnum.荷役資材).ToString(), ThiIraiShousaiEnum.荷役資材.ToString()},
                                                                };

        public static string ToName(string id)
        {
            return idToName[id];
        }

        public static string ToId(ThiIraiShousaiEnum en)
        {
            return ((int)en).ToString();
        }

        public override string ToString()
        {
            return ThiIraiShousaiName;
        }

        public static List<MsThiIraiShousai> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsThiIraiShousai), MethodBase.GetCurrentMethod());
            List<MsThiIraiShousai> ret = new List<MsThiIraiShousai>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsThiIraiShousai> mapping = new MappingBase<MsThiIraiShousai>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsThiIraiShousai GetRecord(NBaseData.DAC.MsUser loginUser, string id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsThiIraiShousai), MethodBase.GetCurrentMethod());
            List<MsThiIraiShousai> ret = new List<MsThiIraiShousai>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsThiIraiShousai> mapping = new MappingBase<MsThiIraiShousai>();
            Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", id));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsThiIraiShousai), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", MsThiIraiShousaiID));
            Params.Add(new DBParameter("THI_IRAI_SHOUSAI_NAME", ThiIraiShousaiName));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbtID));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsThiIraiShousai), MethodBase.GetCurrentMethod());

            #region パラメタ
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("THI_IRAI_SHOUSAI_NAME", ThiIraiShousaiName));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbtID));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", MsThiIraiShousaiID));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            bool rv = true;
            if (cnt == 0) rv = false;
            return rv;
        }

        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsThiIraiShousaiID));

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
            Params.Add(new DBParameter("PK", MsThiIraiShousaiID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
