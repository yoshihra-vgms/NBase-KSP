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
    [TableAttribute("MS_PORTAL_INFO_KOUMOKU")]
    public class MsPortalInfoKoumoku : ISyncTable
    {
        #region データメンバ

        //MS_PORTAL_INFO_KOUMOKU_ID      NVARCHAR2(40) NOT NULL,
        //PORTAL_INFO_KOUMOKU_NAME       NVARCHAR2(50),
        //DELETE_FLAG                    NUMBER(1,0) NOT NULL,
        //SEND_FLAG                      NUMBER(1,0) NOT NULL,
        //VESSEL_ID                      NUMBER(4,0) NOT NULL,
        //DATA_NO                        NUMBER(13,0),
        //USER_KEY                       VARCHAR2(40),
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),


        /// <summary>
        /// ポータル情報項目マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_PORTAL_INFO_KOUMOKU_ID", true)]
        public string MsPortalInfoKoumokuId { get; set; }

        public enum MsPortalInfoKoumokuIdEnum
        {
            免許免状,
            送金受入,
            手配依頼,
            検査,
            証書,
            審査日,
            救命設備,
            荷役安全設備,
            検船,
            乗船,
            下船,
            交代,
            船用金明細登録,
            船用金明細削除,
            受領,
            貯蔵品登録,
            見積回答,
            承認,
            発注,
            支払,
            管理文書登録,
            公文書_規則登録,
            内容確認,
            コメント登録,
            報告書,
            船用金送金,
            配乗表更新,
            健康診断
        }

        /// <summary>
        /// ポータル情報項目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("PORTAL_INFO_KOUMOKU_NAME")]
        public string PortalInfoKoumokuName { get; set; }

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

        public const string 免許免状 = "免許・免状";
        public const string 送金受入 = "送金受入";
        public const string 手配依頼 = "手配依頼";
        public const string 検査 = "検査";
        public const string 証書 = "証書";
        public const string 審査日 = "審査日";
        public const string 救命設備 = "救命設備";
        public const string 荷役安全設備 = "荷役安全設備";
        public const string 検船 = "検船";
        public const string 乗船 = "乗船";
        public const string 下船 = "下船";
        public const string 交代 = "交代";
        public const string 船内準備金明細登録 = "船内準備金明細登録";
        public const string 船内準備金明細削除 = "船内準備金明細削除";
        public const string 受領 = "受領";
        public const string 貯蔵品登録 = "貯蔵品登録";
        public const string 見積回答 = "見積回答";
        public const string 承認 = "承認";
        public const string 発注 = "発注";
        public const string 支払 = "支払";
        public const string 管理文書登録 = "管理文書登録";
        public const string 公文書_規則登録 = "公文書_規則登録";
        public const string 内容確認 = "内容確認";
        public const string コメント登録 = "コメント登録";

        public MsPortalInfoKoumoku()
        {
        }

        public static List<MsPortalInfoKoumoku> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsPortalInfoKoumoku), MethodBase.GetCurrentMethod());
            List<MsPortalInfoKoumoku> ret = new List<MsPortalInfoKoumoku>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsPortalInfoKoumoku> mapping = new MappingBase<MsPortalInfoKoumoku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret; ;
        }

        public static MsPortalInfoKoumoku GetRecordByPortalInfoSyubetuName(NBaseData.DAC.MsUser loginUser, string name)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsPortalInfoKoumoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsPortalInfoKoumoku), "ByPortalInfoKoumokuName");
            List<MsPortalInfoKoumoku> ret = new List<MsPortalInfoKoumoku>();
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PORTAL_INFO_KOUMOKU_NAME", name));
            MappingBase<MsPortalInfoKoumoku> mapping = new MappingBase<MsPortalInfoKoumoku>();
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
        //    Params.Add(new DBParameter("PK", MsPortalInfoKoumokuId));

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
            Params.Add(new DBParameter("PK", MsPortalInfoKoumokuId));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect,loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}