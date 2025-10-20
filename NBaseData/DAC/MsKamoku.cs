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
    [TableAttribute("MS_KAMOKU")]
    public class MsKamoku
    {
        #region データメンバ

        /// <summary>
        /// 科目No
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_KAMOKU_ID")]
        public int MsKamokuId { get; set; }

        /// <summary>
        /// 科目No
        /// </summary>
        [DataMember]
        [ColumnAttribute("KAMOKU_NO")]
        public string KamokuNo { get; set; }

        /// <summary>
        /// 科目種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("KAMOKU_NAME")]
        public string KamokuName { get; set; }

        /// <summary>
        /// 内訳科目No
        /// </summary>
        [DataMember]
        [ColumnAttribute("UTIWAKE_KAMOKU_NO")]
        public string UtiwakeKamokuNo { get; set; }

        /// <summary>
        /// 内訳科目種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("UTIWAKE_KAMOKU_NAME")]
        public string UtiwakeKamokuName { get; set; }

        /// <summary>
        /// 符号
        /// </summary>
        [DataMember]
        [ColumnAttribute("FUGOU")]
        public int Fugou { get; set; }

        /// <summary>
        /// ユーザー部門ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIKAN_FLAG")]
        public int KikanFlag { get; set; }

        /////////////////////////////////////////////////////////////
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
        /// ToStringで内訳科目名も表示したい場合、trueをセット
        /// </summary>
        public bool viewUtiwakeKamokuNameFlag = false;
        #endregion

        public MsKamoku()
        {
        }
        ////////////////////////////////////////////////////////
        public static List<MsKamoku> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), MethodBase.GetCurrentMethod());

            List<MsKamoku> ret = new List<MsKamoku>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsKamoku> mapping = new MappingBase<MsKamoku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsKamoku GetRecordByMsKamokuID(MsUser loginUser, int msKamokuID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "ByMsKamokuID");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_KAMOKU_ID", msKamokuID));
            List<MsKamoku> ret = new List<MsKamoku>();
            MappingBase<MsKamoku> mapping = new MappingBase<MsKamoku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }

        public static MsKamoku GetRecord(MsUser loginUser, string kamokuNo, string utiwakeKamokuNo)
        {
            return GetRecord(null, loginUser, kamokuNo, utiwakeKamokuNo);
        }
        public static MsKamoku GetRecord(DBConnect dbConnect, MsUser loginUser, string kamokuNo, string utiwakeKamokuNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "ByKamokuNo");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("KAMOKU_NO", kamokuNo));
            if (utiwakeKamokuNo == null || utiwakeKamokuNo.Length == 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "ByKamokuNoPlus1");
            }
            else
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "ByKamokuNoPlus2");
                Params.Add(new DBParameter("UTIWAKE_KAMOKU_NO", utiwakeKamokuNo));
            }

            List<MsKamoku> ret = new List<MsKamoku>();
            MappingBase<MsKamoku> mapping = new MappingBase<MsKamoku>();
            ret = mapping.FillRecrods(dbConnect,loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }

        public static List<MsKamoku> GetRecordsBy予実連携(MsUser loginUser, string kamokuNo, string utiwakeKamokuNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "GetRecordsBy予実連携");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "ByKamokuNo");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("KAMOKU_NO", kamokuNo));
            if (utiwakeKamokuNo == null || utiwakeKamokuNo.Length == 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "ByKamokuNoPlus1");
            }
            else
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "ByKamokuNoPlus2");
                Params.Add(new DBParameter("UTIWAKE_KAMOKU_NO", utiwakeKamokuNo));
            }

            List<MsKamoku> ret = new List<MsKamoku>();
            MappingBase<MsKamoku> mapping = new MappingBase<MsKamoku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
        public static MsKamoku GetRecordByHachuKamoku(MsUser loginUser, string thiIraiSbtId, string thiIraiShousaiId, string nyukyoKamokuId)
        {
            return GetRecordByHachuKamoku(null, loginUser, thiIraiSbtId, thiIraiShousaiId, nyukyoKamokuId);
        }
        public static MsKamoku GetRecordByHachuKamoku(DBConnect dbConnect, MsUser loginUser, string thiIraiSbtId, string thiIraiShousaiId, string nyukyoKamokuId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "ByHachuKamoku");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", thiIraiSbtId));
            if (thiIraiShousaiId == null || thiIraiShousaiId.Length == 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "ByHachuKamokuPlus1");
            }
            else
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "ByHachuKamokuPlus2");
                Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", thiIraiShousaiId));
            }
            if (nyukyoKamokuId == null || nyukyoKamokuId.Length == 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "ByHachuKamokuPlus3");
            }
            else
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "ByHachuKamokuPlus4");
                Params.Add(new DBParameter("MS_NYUKYO_KAMOKU_ID", nyukyoKamokuId));
            }

            List<MsKamoku> ret = new List<MsKamoku>();
            MappingBase<MsKamoku> mapping = new MappingBase<MsKamoku>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                //return null;
                return GetRecordByHachuKamoku(dbConnect, loginUser, thiIraiSbtId, thiIraiShousaiId, null);
            }

            return ret[0];
        }

        public static List<MsKamoku> GetRecordsByHachuKamoku(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "UsedByHachuKamoku");

            ParameterConnection Params = new ParameterConnection();
            List<MsKamoku> ret = new List<MsKamoku>();
            MappingBase<MsKamoku> mapping = new MappingBase<MsKamoku>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsKamoku GetRecordByFurikaeKamoku(MsUser loginUser, string kamokuNo, string utiwakeKamokuNo)
        {
            MsKamoku kamoku = GetRecord(loginUser, kamokuNo, utiwakeKamokuNo);
            if (kamoku == null)
                return null;

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), "ByFurikaeKamoku");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_KAMOKU_ID", kamoku.MsKamokuId));

            List<MsKamoku> ret = new List<MsKamoku>();
            MappingBase<MsKamoku> mapping = new MappingBase<MsKamoku>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_KAMOKU_ID", this.MsKamokuId));
            Params.Add(new DBParameter("KAMOKU_NO", this.KamokuNo));
            Params.Add(new DBParameter("KAMOKU_NAME", this.KamokuName));
            Params.Add(new DBParameter("UTIWAKE_KAMOKU_NO", this.UtiwakeKamokuNo));
            Params.Add(new DBParameter("UTIWAKE_KAMOKU_NAME", this.UtiwakeKamokuName));
            Params.Add(new DBParameter("FUGOU", this.Fugou));
            Params.Add(new DBParameter("KIKAN_FLAG", this.KikanFlag));

            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }


        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsKamoku), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();


            Params.Add(new DBParameter("KAMOKU_NO", this.KamokuNo));
            Params.Add(new DBParameter("KAMOKU_NAME", this.KamokuName));
            Params.Add(new DBParameter("UTIWAKE_KAMOKU_NO", this.UtiwakeKamokuNo));
            Params.Add(new DBParameter("UTIWAKE_KAMOKU_NAME", this.UtiwakeKamokuName));
            Params.Add(new DBParameter("FUGOU", this.Fugou));
            Params.Add(new DBParameter("KIKAN_FLAG", this.KikanFlag));

            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));

            Params.Add(new DBParameter("MS_KAMOKU_ID", this.MsKamokuId));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }


        public override string ToString()
        {
            string viewName = KamokuName;
            if (viewUtiwakeKamokuNameFlag)
            {
                if (UtiwakeKamokuName != null & UtiwakeKamokuName.Length > 0)
                {
                    viewName += " : " + UtiwakeKamokuName;
                }
            }
            return viewName;
        }
    }   
}
