using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using ORMapping;
using System.Reflection;
using System.Runtime.Serialization;
using NBaseUtil;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("MS_SENIN_CAREER")]
    public class MsSeninCareer : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }


        //==================================
        // 学歴
        //==================================

        /// <summary>
        /// 最終学歴
        /// </summary>
        [DataMember]
        [ColumnAttribute("ACADEMIC_BACKGROUND")]
        public string AcademicBackground { get; set; }

        /// <summary>
        /// 卒業年度
        /// </summary>
        [DataMember]
        [ColumnAttribute("YEAR_OF_GRADUATION")]
        public string YearOfGraduation { get; set; }



        //==================================
        // 他社歴
        //==================================

        /// <summary>
        /// 他社（会社名）
        /// </summary>
        [DataMember]
        [ColumnAttribute("COMPANY")]
        public string Company { get; set; }

        /// <summary>
        /// 入社
        /// </summary>
        [DataMember]
        [ColumnAttribute("JOINED")]
        public string Joined { get; set; }

        /// <summary>
        /// 退社
        /// </summary>
        [DataMember]
        [ColumnAttribute("LEAVE")]
        public string Leave { get; set; }





        //==================================
        // 共通
        //==================================
        #region
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


        [DataMember]
        private List<string> _companyList = null;

        [DataMember]
        private List<string> _joinedList = null;

        [DataMember]
        private List<string> _leaveList = null;

        #endregion

        public bool EditFlag = false;


        public List<string> CompanyList
        {
            get
            {
                if (_companyList == null)
                {
                    if (Company != null)
                        _companyList = Company.Split(',').ToList();
                    else
                        _companyList = new List<string>();
                }
                return _companyList;
            }
        }
        public List<string> JoinedList
        {
            get
            {
                if (_joinedList == null)
                {
                    if (Joined != null)
                        _joinedList = Joined.Split(',').ToList();
                    else
                        _joinedList = new List<string>();
                }
                return _joinedList;
            }
        }
        public List<string> LeaveList
        {
            get
            {
                if (_leaveList == null)
                {
                    if (Leave != null)
                        _leaveList = Leave.Split(',').ToList();
                    else
                        _leaveList = new List<string>();
                }
                return _leaveList;
            }
        }



        public string Company1
        {
            get
            {
                return getIndexOf(CompanyList, 0);
            }
        }
        public string Company2
        {
            get
            {
                return getIndexOf(CompanyList, 1);
            }
        }
        public string Company3
        {
            get
            {
                return getIndexOf(CompanyList, 2);
            }
        }
        public string Company4
        {
            get
            {
                return getIndexOf(CompanyList, 3);
            }
        }
        public string Company5
        {
            get
            {
                return getIndexOf(CompanyList, 4);
            }
        }
        public string Joined1
        {
            get
            {
                return getIndexOf(JoinedList, 0);
            }
        }
        public string Joined2
        {
            get
            {
                return getIndexOf(JoinedList, 1);
            }
        }
        public string Joined3
        {
            get
            {
                return getIndexOf(JoinedList, 2);
            }
        }
        public string Joined4
        {
            get
            {
                return getIndexOf(JoinedList, 3);
            }
        }
        public string Joined5
        {
            get
            {
                return getIndexOf(JoinedList, 4);
            }
        }
        public string Leave1
        {
            get
            {
                return getIndexOf(LeaveList, 0);
            }
        }
        public string Leave2
        {
            get
            {
                return getIndexOf(LeaveList, 1);
            }
        }
        public string Leave3
        {
            get
            {
                return getIndexOf(LeaveList, 2);
            }
        }
        public string Leave4
        {
            get
            {
                return getIndexOf(LeaveList, 3);
            }
        }
        public string Leave5
        {
            get
            {
                return getIndexOf(LeaveList, 4);
            }
        }
        private string getIndexOf(List<string> list, int index)
        {
            if (list.Count > index)
                return list[index];
            else
                return "";
        }



        public static List<MsSeninCareer> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSeninCareer), "GetRecords");

            List<MsSeninCareer> ret = new List<MsSeninCareer>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSeninCareer> mapping = new MappingBase<MsSeninCareer>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }




        public static MsSeninCareer GetRecordsByMsSeninID(MsUser loginUser, int msSeninId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSeninCareer), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSeninCareer), "ByMsSeninID");

            List<MsSeninCareer> ret = new List<MsSeninCareer>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSeninCareer> mapping = new MappingBase<MsSeninCareer>();
            Params.Add(new DBParameter("MS_SENIN_ID", msSeninId));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
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

            Company = string.Join(",", CompanyList);
            Joined = string.Join(",", JoinedList);
            Leave = string.Join(",", LeaveList);


            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));

            Params.Add(new DBParameter("ACADEMIC_BACKGROUND", AcademicBackground));
            Params.Add(new DBParameter("YEAR_OF_GRADUATION", YearOfGraduation));

            Params.Add(new DBParameter("COMPANY", Company));
            Params.Add(new DBParameter("JOINED", Joined));
            Params.Add(new DBParameter("LEAVE", Leave));

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

            Company = string.Join(",", _companyList);
            Joined = string.Join(",", _joinedList);
            Leave = string.Join(",", _leaveList);

            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("ACADEMIC_BACKGROUND", AcademicBackground));
            Params.Add(new DBParameter("YEAR_OF_GRADUATION", YearOfGraduation));

            Params.Add(new DBParameter("COMPANY", Company));
            Params.Add(new DBParameter("JOINED", Joined));
            Params.Add(new DBParameter("LEAVE", Leave));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
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
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", MsSeninID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return MsSeninID == 0;
        }
    }
}
