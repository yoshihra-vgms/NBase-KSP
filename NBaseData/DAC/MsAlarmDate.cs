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
    [TableAttribute("MS_ALARM_DATE")]
    public class MsAlarmDate
    {
        //MS_ALARM_DATE_ID               NUMBER(4,0) NOT NULL,
        //MS_ALARM_DATE_NAME             VARCHAR2(20),
        //DAY_OFFSET                     NUMBER(4,0) NOT NULL,

        //DELETE_FLAG                    NUMBER(1,0) NOT NULL,
        //SEND_FLAG                      NUMBER(1,0) NOT NULL,
        //VESSEL_ID                      NUMBER(4,0) NOT NULL,
        //DATA_NO                        NUMBER(13,0),
        //USER_KEY                       VARCHAR2(40) NOT NULL,
        //RENEW_DATE                     DATE NOT NULL,
        //RENEW_USER_ID                  VARCHAR2(40) NOT NULL,
        //TS                             VARCHAR2(20),

        #region データメンバ
        /// <summary>
        /// アラーム日マスタID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_ALARM_DATE_ID")]
        public int MsAlarmDateID { get; set; }

        /// <summary>
        /// アラーム名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_ALARM_DATE_NAME")]
        public string MsAlarmDateName { get; set; }


        /// <summary>
        /// アラーム日オフセット
        /// </summary>
        [DataMember]
        [ColumnAttribute("DAY_OFFSET")]
        public int DayOffset { get; set; }


        //-----------------------------------------------------------

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

        /// <summary>
        /// アラーム日のマスタID
        /// </summary>
        public enum MsAlarmDateIDNo
        {
            証書アラームID = 1,     //証書
            検船アラームID,         //検船
            内部審査アラームID,     //内部審査
            レビューアラームID,     //レビュー
            審査日アラームID,       //審査
            救命設備アラームID,     //救命設備
            荷役安全アラームID,     //荷役安全設備
            
            免許_免状アラームID,
            送金受入アラームID,

            手配依頼アラームID,
            見積中アラームID,
            承認依頼アラームID,
            承認済みアラームID,
            発注済みアラームID,
            受領済みアラームID,
            支払作成済みアラームID,

            検査アラーム1アラームID,
            検査アラーム2アラームID,

            文書管理アラームID
        }

        ////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////
        public static List<MsAlarmDate> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsAlarmDate), MethodBase.GetCurrentMethod());
            List<MsAlarmDate> ret = new List<MsAlarmDate>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsAlarmDate> mapping = new MappingBase<MsAlarmDate>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsAlarmDate GetRecord(MsUser loginUser, MsAlarmDateIDNo id)
        {
            return GetRecord(null, loginUser, id);
        }
        public static MsAlarmDate GetRecord(DBConnect dbConnect, MsUser loginUser, MsAlarmDateIDNo id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsAlarmDate), MethodBase.GetCurrentMethod());
            List<MsAlarmDate> ret = new List<MsAlarmDate>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsAlarmDate> mapping = new MappingBase<MsAlarmDate>();
            //条件挿入
            Params.Add(new DBParameter("MS_ALARM_DATE_ID", (int)id));

            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }


        public bool InsertRecord(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsAlarmDate), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_ALARM_DATE_ID", this.MsAlarmDateID));
            Params.Add(new DBParameter("MS_ALARM_DATE_NAME", this.MsAlarmDateName));
            Params.Add(new DBParameter("DAY_OFFSET", this.DayOffset));

            Params.Add(new DBParameter("DELETE_FLAG", this.DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", this.SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", this.VesselID));
            Params.Add(new DBParameter("DATA_NO", this.DataNo));
            Params.Add(new DBParameter("USER_KEY", this.UserKey));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("TS", this.Ts));

            DBConnect.ExecuteNonQuery(loginUser.MsUserID, SQL, Params);
            return true;
        }

        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }
        public bool UpdateRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsAlarmDate), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            int cnt = 0;
            Params.Add(new DBParameter("MS_ALARM_DATE_ID", this.MsAlarmDateID));
            Params.Add(new DBParameter("MS_ALARM_DATE_NAME", this.MsAlarmDateName));
            Params.Add(new DBParameter("DAY_OFFSET", this.DayOffset));

            Params.Add(new DBParameter("DELETE_FLAG", this.DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", this.SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", this.VesselID));
            Params.Add(new DBParameter("DATA_NO", this.DataNo));
            Params.Add(new DBParameter("USER_KEY", this.UserKey));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("TS", this.Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

    }
}
