using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ORMapping.Atts;
using NBaseData.DS;
using ORMapping.Attrs;
using System.Reflection;
using ORMapping;
using System.Runtime.Serialization;

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("SI_KYUKA")]
    public class SiKyuka : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 休暇ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_KYUKA_ID", true)]
        public string SiKyukaID { get; set; }

        
        
        /// <summary>
        /// 船員ID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SENIN_ID")]
        public int MsSeninID { get; set; }

        /// <summary>
        /// 年度
        /// </summary>
        [DataMember]
        [ColumnAttribute("NENDO")]
        public string Nendo { get; set; }

        /// <summary>
        /// 有給休暇
        /// </summary>
        [DataMember]
        [ColumnAttribute("YUKYU＿KYUKA")]
        public int YukyuKyuka { get; set; }

        /// <summary>
        /// 陸上休暇
        /// </summary>
        [DataMember]
        [ColumnAttribute("RIKUJYO＿KYUKA")]
        public int RikujyoKyuka { get; set; }

        /// <summary>
        /// 船内休日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENNAI＿KYUJITSU")]
        public int SennaiKyujitsu { get; set; }

        /// <summary>
        /// 船内休暇
        /// </summary>
        [DataMember]
        [ColumnAttribute("SENNAI＿KYUKA")]
        public int SennaiKyuka { get; set; }



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

        public SiKyuka()
        {
            this.SiKyukaID = null;
            this.MsSeninID = Int32.MinValue;

            this.YukyuKyuka = 0;
            this.RikujyoKyuka = 0;
            this.SennaiKyujitsu = 0;
            this.SennaiKyuka = 0;
        }

        public static SiKyuka GetRecord(MsUser loginUser, string siKyukaId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKyuka), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKyuka), "BySiKyukaID");

            List<SiKyuka> ret = new List<SiKyuka>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKyuka> mapping = new MappingBase<SiKyuka>();

            Params.Add(new DBParameter("SI_KYUKA_ID", siKyukaId));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            if (ret.Count == 0)
                return null;
            return ret[0];
        }

        public static List<SiKyuka> GetRecordsByNendo(MsUser loginUser, string nendo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiKyuka), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKyuka), "ByNendo");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiKyuka), "OrderBy");

            List<SiKyuka> ret = new List<SiKyuka>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiKyuka> mapping = new MappingBase<SiKyuka>();

            Params.Add(new DBParameter("NENDO", nendo));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);
            return ret;
        }


        #region ISyncTable メンバ

        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_KYUKA_ID", SiKyukaID));

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("NENDO", Nendo));

            Params.Add(new DBParameter("YUKYU＿KYUKA", YukyuKyuka));
            Params.Add(new DBParameter("RIKUJYO＿KYUKA", RikujyoKyuka));
            Params.Add(new DBParameter("SENNAI＿KYUJITSU", SennaiKyujitsu));
            Params.Add(new DBParameter("SENNAI＿KYUKA", SennaiKyuka));

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

        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }
        public bool UpdateRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_SENIN_ID", MsSeninID));
            Params.Add(new DBParameter("NENDO", Nendo));

            Params.Add(new DBParameter("YUKYU＿KYUKA", YukyuKyuka));
            Params.Add(new DBParameter("RIKUJYO＿KYUKA", RikujyoKyuka));
            Params.Add(new DBParameter("SENNAI＿KYUJITSU", SennaiKyujitsu));
            Params.Add(new DBParameter("SENNAI＿KYUKA", SennaiKyuka));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_KYUKA_ID", SiKyukaID));
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
            Params.Add(new DBParameter("PK", SiKyukaID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect,loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return SiKyukaID == null;
        }
    }
}
