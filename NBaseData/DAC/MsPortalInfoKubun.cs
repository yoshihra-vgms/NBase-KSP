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
    [TableAttribute("MS_PORTAL_INFO_KUBUN")]
    public class MsPortalInfoKubun : ISyncTable
    {
        #region データメンバ

        //MS_PORTAL_INFO_KUBUN_ID        NVARCHAR2(40) NOT NULL,
        //MS_PORTAL_INFO_KUBUN_NAME      NVARCHAR2(50),
        //DELETE_FLAG                    NUMBER(1,0) NOT NULL,
        //SEND_FLAG                      NUMBER(1,0) NOT NULL,
        //VESSEL_ID                      NUMBER(4,0) NOT NULL,
        //DATA_NO                        NUMBER(13,0),
        //USER_KEY                       VARCHAR2(40),
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),


        /// <summary>
        /// ポータル情報区分マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_PORTAL_INFO_KUBUN_ID", true)]
        public string MsPortalInfoKubunId { get; set; }

        public enum MsPortalInfoKubunIdEnum { 遅延, アラーム90日前, アラーム180日前, 有効期限, 審査, 
            内部審査, レビュー, 点検, 検査, 本船更新, 
            未対応, 未回答, 未承認, 未発注, 未受領, 未作成, 未払, 事務所更新}

        /// <summary>
        /// ポータル情報区分名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_PORTAL_INFO_KUBUN_NAME")]
        public string MsPortalInfoKubunName { get; set; }

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

        public const string 遅延 = "遅延";
        public const string アラーム90日前 = "90日前";
        public const string アラーム180日前 = "180日前";
        public const string 有効期限 = "有効期限";
        public const string 審査 = "審査";
        public const string 内部審査 = "内部審査";
        public const string レビュー = "レビュー";
        public const string 点検 = "点検";
        public const string 検査 = "検査";
        public const string 本船更新 = "本船更新";
        public const string 未対応 = "未対応";
        public const string 未回答 = "未回答";
        public const string 未承認 = "未承認";
        public const string 未発注 = "未発注";
        public const string 未受領 = "未受領";
        public const string 未作成 = "未作成";
        public const string 未払 = "未払"; 

        public MsPortalInfoKubun()
        {
        }

        public static List<MsPortalInfoKubun> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsPortalInfoKubun), MethodBase.GetCurrentMethod());
            List<MsPortalInfoKubun> ret = new List<MsPortalInfoKubun>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsPortalInfoKubun> mapping = new MappingBase<MsPortalInfoKubun>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret; ;
        }

        public static MsPortalInfoKubun GetRecordByPortalInfoSyubetuName(NBaseData.DAC.MsUser loginUser, string name)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsPortalInfoKubun), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsPortalInfoKubun), "ByPortalInfoKubunName");
            List<MsPortalInfoKubun> ret = new List<MsPortalInfoKubun>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_PORTAL_INFO_KUBUN_NAME", name));
            MappingBase<MsPortalInfoKubun> mapping = new MappingBase<MsPortalInfoKubun>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbcone, NBaseData.DAC.MsUser loginUser)
        {
            return true;
        }


        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            return true;
        }

        #region ISyncTable メンバ


        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsPortalInfoKubunId));

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
            Params.Add(new DBParameter("PK", MsPortalInfoKubunId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }
        #endregion
    }
}