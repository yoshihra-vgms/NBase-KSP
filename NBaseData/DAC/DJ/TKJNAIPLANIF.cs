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
///using Oracle.DataAccess.Client;

namespace NBaseData.DAC
{
    /// <summary>
    /// 動静基幹連携：スケジュール明細IFテーブル
    /// </summary>
    [DataContract()]
    [TableAttribute("TKJNAIPLANIF")]
    public class TKJNAIPLANIF
    {
        #region データメンバ
        /// <summary>
        /// 船舶NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("FUNE_NO")]
        public string FuneNo { get; set; }

        /// <summary>
        /// 次航NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("JIKO_NO")]
        public string JikoNo { get; set; }

        /// <summary>
        /// 日付
        /// </summary>
        [DataMember]
        [ColumnAttribute("SC_YMD")]
        public DateTime ScYmd { get; set; }

        /// <summary>
        /// コマ番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("KOMA_NO")]
        public string KomaNo { get; set; }

        /// <summary>
        /// 処理番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("OPE_NO")]
        public decimal OpeNo { get; set; }

        /// <summary>
        /// 処理セットキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("OPE_SET_KEY")]
        public string OpeSetKey { get; set; }

        /// <summary>
        /// 港NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("PORT_NO")]
        public string PortNo { get; set; }

        /// <summary>
        /// 基地NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("BASE_NO")]
        public string BaseNo { get; set; }

        /// <summary>
        /// 作業区分
        /// </summary>
        [DataMember]
        [ColumnAttribute("SG_KBN")]
        public string SgKbn { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        [DataMember]
        [ColumnAttribute("AREA")]
        public string Area { get; set; }

        /// <summary>
        /// 代理店連絡フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("AGENCY_CONTACT_FLG")]
        public string AgencyContactFlag { get; set; }

        /// <summary>
        /// 代理店再連絡フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("AGENCY_RE_CONTACT_FLG")]
        public string AgencyReContactFlag { get; set; }

        /// <summary>
        /// 船長予定連絡フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("CAPTAIN_CONTACT_FLG")]
        public string CaptainContactFlag { get; set; }

        /// <summary>
        /// 揚入力フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DS_INPUT_FLG")]
        public string DsInputFlag { get; set; }

        /// <summary>
        /// 入港時刻
        /// </summary>
        [DataMember]
        [ColumnAttribute("INPORT_TIME")]
        public string InportTime { get; set; }

        /// <summary>
        /// 着桟時刻
        /// </summary>
        [DataMember]
        [ColumnAttribute("ARRIVE_TIME")]
        public string ArriveTime { get; set; }

        /// <summary>
        /// 開始時刻
        /// </summary>
        [DataMember]
        [ColumnAttribute("START_TIME")]
        public string StartTime { get; set; }

        /// <summary>
        /// 終了時刻
        /// </summary>
        [DataMember]
        [ColumnAttribute("END_TIME")]
        public string EndTime { get; set; }

        /// <summary>
        /// 離桟時刻
        /// </summary>
        [DataMember]
        [ColumnAttribute("DEPARTURE_TIME")]
        public string DepartureTime { get; set; }

        /// <summary>
        /// 出港時刻
        /// </summary>
        [DataMember]
        [ColumnAttribute("OUTPORT_TIME")]
        public string OutportTime { get; set; }

        /// <summary>
        /// 通過時刻
        /// </summary>
        [DataMember]
        [ColumnAttribute("THROUGH_TIME")]
        public string ThroughTime { get; set; }

        /// <summary>
        /// 作成日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("CREATE_DATE")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 取込ユーザ
        /// </summary>
        [DataMember]
        [ColumnAttribute("TRKM_TNTSY_CD")]
        public string TrkmTntsyCD { get; set; }

        /// <summary>
        /// 取込日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("TRKM_DATE")]
        public DateTime TrkmDate { get; set; }

        /// <summary>
        /// 状態
        /// </summary>
        [DataMember]
        [ColumnAttribute("SYORI_STATUS")]
        public string SyoriStatus { get; set; }

        /// <summary>
        /// 状態備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("STATUS_REMARK")]
        public string StatusRemark { get; set; }

        /// <summary>
        /// 外部システム識別コード
        /// </summary>
        [DataMember]
        [ColumnAttribute("EXT_SYSTEM_CD")]
        public string ExtSystemCD { get; set; }

        /// <summary>
        /// 更新プログラムID
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDPGMID")]
        public string UpdPgmID { get; set; }

        /// <summary>
        /// 更新ユーザID
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDUSER_ID")]
        public string UpdUserID { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDDATE_YMDHMS")]
        public Int64 UpdateYMDHMS { get; set; }

        public DjDousei djDousei;
        public List<TKJNAIPLAN_AMTIF> TKJNAIPLAN_AMTIFs = new List<TKJNAIPLAN_AMTIF>();
        #endregion




        public static TKJNAIPLANIF GetSameRecord(NBaseData.DAC.MsUser loginUser, TKJNAIPLANIF TKJNAIPLANIF)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLANIF), MethodBase.GetCurrentMethod());

            List<TKJNAIPLANIF> ret = new List<TKJNAIPLANIF>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<TKJNAIPLANIF> mapping = new MappingBase<TKJNAIPLANIF>();

            //取得条件設定
            Params.Add(new DBParameter("FUNE_NO", TKJNAIPLANIF.FuneNo));
            Params.Add(new DBParameter("SC_YMD", TKJNAIPLANIF.ScYmd));
            Params.Add(new DBParameter("SG_KBN", TKJNAIPLANIF.SgKbn));
            Params.Add(new DBParameter("EXT_SYSTEM_CD", TKJNAIPLANIF.ExtSystemCD));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }




        public static TKJNAIPLANIF GetRecord(NBaseData.DAC.MsUser loginUser,TKJNAIPLANIF TKJNAIPLANIF,DjDousei dousei)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLANIF), MethodBase.GetCurrentMethod());

            List<TKJNAIPLANIF> ret = new List<TKJNAIPLANIF>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<TKJNAIPLANIF> mapping = new MappingBase<TKJNAIPLANIF>();

            //取得条件設定
            Params.Add(new DBParameter("FUNE_NO", TKJNAIPLANIF.FuneNo));
            Params.Add(new DBParameter("JIKO_NO", TKJNAIPLANIF.JikoNo));
            Params.Add(new DBParameter("SC_YMD", TKJNAIPLANIF.ScYmd));
            Params.Add(new DBParameter("KOMA_NO", dousei.KomaNo));
            Params.Add(new DBParameter("EXT_SYSTEM_CD", TKJNAIPLANIF.ExtSystemCD));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }

        public static int GetKoma(MsUser loginUser,TKJNAIPLANIF TKJNAIPLANIF)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLANIF), "GetRecordKoma");

            ParameterConnection Params = new ParameterConnection();
            //取得条件設定
            Params.Add(new DBParameter("FUNE_NO", TKJNAIPLANIF.FuneNo));
            Params.Add(new DBParameter("SC_YMD", TKJNAIPLANIF.ScYmd.ToString("yyyyMMdd")));

            object obj = DBConnect.ExecuteScalar(loginUser.MsUserID, SQL, Params);

            if (obj == null)
            {
                return 1;
            }

            short cnt = (short)obj;
            return (int)(cnt + 1);
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLANIF), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("FUNE_NO",FuneNo));
            Params.Add(new DBParameter("JIKO_NO",JikoNo));
            Params.Add(new DBParameter("SC_YMD",ScYmd));
            Params.Add(new DBParameter("KOMA_NO",KomaNo));
            Params.Add(new DBParameter("OPE_NO",OpeNo));
            Params.Add(new DBParameter("OPE_SET_KEY",OpeSetKey));
            Params.Add(new DBParameter("PORT_NO",PortNo));
            Params.Add(new DBParameter("BASE_NO",BaseNo));
            Params.Add(new DBParameter("SG_KBN",SgKbn));
            Params.Add(new DBParameter("AREA",Area));
            Params.Add(new DBParameter("AGENCY_CONTACT_FLG",AgencyContactFlag));
            Params.Add(new DBParameter("AGENCY_RE_CONTACT_FLG",AgencyReContactFlag));
            Params.Add(new DBParameter("CAPTAIN_CONTACT_FLG",CaptainContactFlag));
            Params.Add(new DBParameter("DS_INPUT_FLG",DsInputFlag));
            Params.Add(new DBParameter("INPORT_TIME",InportTime));
            Params.Add(new DBParameter("ARRIVE_TIME",ArriveTime));
            Params.Add(new DBParameter("START_TIME",StartTime));
            Params.Add(new DBParameter("END_TIME",EndTime));
            Params.Add(new DBParameter("DEPARTURE_TIME",DepartureTime));
            Params.Add(new DBParameter("OUTPORT_TIME",OutportTime));
            Params.Add(new DBParameter("THROUGH_TIME",ThroughTime));
            Params.Add(new DBParameter("CREATE_DATE",CreateDate));
            Params.Add(new DBParameter("TRKM_TNTSY_CD",TrkmTntsyCD));
            Params.Add(new DBParameter("TRKM_DATE",TrkmDate));
            Params.Add(new DBParameter("SYORI_STATUS",SyoriStatus));
            Params.Add(new DBParameter("STATUS_REMARK",StatusRemark));
            Params.Add(new DBParameter("EXT_SYSTEM_CD",ExtSystemCD));
            Params.Add(new DBParameter("UPDPGMID",UpdPgmID));
            Params.Add(new DBParameter("UPDUSERID",UpdUserID));
            Params.Add(new DBParameter("UPDDATE_YMDHMS", UpdateYMDHMS));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        public bool DeleteRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLANIF), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("FUNE_NO", FuneNo));
            Params.Add(new DBParameter("JIKO_NO", JikoNo));
            Params.Add(new DBParameter("SC_YMD", ScYmd));
            Params.Add(new DBParameter("KOMA_NO", KomaNo));
            Params.Add(new DBParameter("EXT_SYSTEM_CD", ExtSystemCD));

            using (DBConnect dbConnect = new DBConnect())
            {
                int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
                if (cnt == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public static string 内航動静データ取込ストアド呼び出し(NBaseData.DAC.MsUser loginUser,MsUser OperationUser, string FuneNO)
        {
            string ret = "";

            //string SQL = "KJVYM02035";
            string SQL = "CALL_KJVYM02035";

            using (DBConnect dbConnect = new DBConnect())
            {
                //using (OracleCommand cmd = new OracleCommand(SQL, dbConnect.OracleConnection))// 201508 Oracle >> Postgresql対応
                //{
                //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //    cmd.Parameters.Add(new OracleParameter("p_FUNE_NO", OracleDbType.Varchar2, 20, System.Data.ParameterDirection.Input));
                //    cmd.Parameters.Add(new OracleParameter("p_TRKM_TNTSY_CD", OracleDbType.Varchar2, 20, System.Data.ParameterDirection.Input));
                //    cmd.Parameters.Add(new OracleParameter("ret", OracleDbType.Varchar2, 20, "", System.Data.ParameterDirection.Output));

                //    cmd.Parameters["p_FUNE_NO"].Value = FuneNO;
                //    // 2013.06.03: ユーザIDではなく、ログインIDをわたして欲しいとコメントを受けたので修正
                //    //cmd.Parameters["p_TRKM_TNTSY_CD"].Value = OperationUser.MsUserID;
                //    cmd.Parameters["p_TRKM_TNTSY_CD"].Value = OperationUser.LoginID;

                //    cmd.ExecuteNonQuery();

                //    ret = cmd.Parameters["ret"].Value.ToString();
                //}

                ParameterConnection Params = new ParameterConnection();

                Params.Add(new DBParameter("p_FUNE_NO", FuneNO));
                Params.Add(new DBParameter("p_TRKM_TNTSY_CD", OperationUser.LoginID));

                ret = DBConnect.ExecuteProcedure(dbConnect, loginUser.MsUserID, SQL, Params);
            }


            return ret;
        }

    }
}
