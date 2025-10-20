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
//using Oracle.DataAccess.Client;

namespace NBaseData.DAC
{
    /// <summary>
    /// 動静基幹連携：スケジュール明細IFテーブル
    /// </summary>
    [DataContract()]
    [TableAttribute("TKJNAIPLAN")]
    public class TKJNAIPLAN
    {
        public enum 作業区分Enum { 積み = 1, 揚げ };
        public static string 作業区分Str( 作業区分Enum kbn )
        {
            return ((int)kbn).ToString();
        }

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

        [DataMember]
        [ColumnAttribute("LD_DS_SET_NO")]
        public string LdDsSetNo { get; set; }

        [DataMember]
        [ColumnAttribute("TSENTORI_NM")]
        public string TsentoriNM { get; set; }

        [DataMember]
        [ColumnAttribute("DS_INPUT_FLG")]
        public string DsInputFlg { get; set; }

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
        [ColumnAttribute("UPDUSERID")]
        public string UpdUserID { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        [DataMember]
        [ColumnAttribute("UPDDATE_YMDHMS")]
        public Int64 UpdateYMDHMS { get; set; }

        public List<TKJNAIPLAN_AMT_BILL> TKJNAIPLAN_AMT_BILLs = new List<TKJNAIPLAN_AMT_BILL>();

        /// <summary>
        /// 外地フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("GAICHI_FLAG")]
        public int GaichiFlag { get; set; }

        #endregion

        public static List<TKJNAIPLAN> GetRecordsByVesselVoyageNo(NBaseData.DAC.MsUser loginUser, string FuneNo,string VoyageNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLAN), "GetRecordsByVesselVoyageNo");

            List<TKJNAIPLAN> ret = new List<TKJNAIPLAN>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<TKJNAIPLAN> mapping = new MappingBase<TKJNAIPLAN>();

            //取得条件設定
            Params.Add(new DBParameter("FUNE_NO", FuneNo));
            Params.Add(new DBParameter("JIKO_NO", VoyageNo));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<TKJNAIPLAN> GetRecordsByLdDsSetNo(NBaseData.DAC.MsUser loginUser, string FuneNo,string LdDsSetNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLAN), "GetRecordsByLdDsSetNo");

            List<TKJNAIPLAN> ret = new List<TKJNAIPLAN>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<TKJNAIPLAN> mapping = new MappingBase<TKJNAIPLAN>();

            //取得条件設定
            Params.Add(new DBParameter("FUNE_NO", FuneNo));
            Params.Add(new DBParameter("LD_DS_SET_NO", LdDsSetNo));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static TKJNAIPLAN GetRecordBy前月港(MsUser loginUser, string FuneNo, string VoyageNo, List<string> targetBashoNos)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLAN), MethodBase.GetCurrentMethod());

            List<TKJNAIPLAN> ret = new List<TKJNAIPLAN>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<TKJNAIPLAN> mapping = new MappingBase<TKJNAIPLAN>();

            //取得条件設定
            Params.Add(new DBParameter("FUNE_NO", FuneNo));
            Params.Add(new DBParameter("JIKO_NO", VoyageNo));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count > 0)
            {
                //return ret[0];
                var tmp = ret.Where(obj => targetBashoNos.Contains(obj.PortNo));
                if (tmp.Count() > 0)
                {
                    return tmp.First();
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        //public static List<TKJNAIPLAN> GetRecordsByAllVesselJikoNo(MsUser loginUser, string JikoNo)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(typeof(TKJNAIPLAN), MethodBase.GetCurrentMethod());

        //    List<TKJNAIPLAN> ret = new List<TKJNAIPLAN>();

        //    ParameterConnection Params = new ParameterConnection();
        //    MappingBase<TKJNAIPLAN> mapping = new MappingBase<TKJNAIPLAN>();

        //    //取得条件設定
        //    Params.Add(new DBParameter("JIKO_NO", JikoNo));

        //    ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

        //    return ret;
        //}


   }
}
