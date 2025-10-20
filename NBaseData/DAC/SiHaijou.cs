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

namespace NBaseData.DAC
{
    [DataContract()]
    [TableAttribute("SI_HAIJOU")]
    public class SiHaijou : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 配乗表ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_HAIJOU_ID", true)]
        public string SiHaijouID { get; set; }




        /// <summary>
        /// 配信ユーザID (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("HAISHIN_USER_ID")]
        public string HaishinUserID { get; set; }




        /// <summary>
        /// 配信日 (FK)
        /// </summary>
        [DataMember]
        [ColumnAttribute("HAISHIN_DATE")]
        public DateTime HaishinDate { get; set; }

        
        
        
        
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
        /// 配乗表アイテムリスト
        /// </summary>
        [DataMember]
        private List<SiHaijouItem> siHaijouItems;
        public List<SiHaijouItem> SiHaijouItems
        {
            get
            {
                if (siHaijouItems == null)
                {
                    siHaijouItems = new List<SiHaijouItem>();
                }

                return siHaijouItems;
            }
        }




        public static List<SiHaijou> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiHaijou), MethodBase.GetCurrentMethod());

            List<SiHaijou> ret = new List<SiHaijou>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiHaijou> mapping = new MappingBase<SiHaijou>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            JoinSiHaijouItems(loginUser, ret);

            return ret;
        }


        //ユーザーマスタ
        public static List<SiHaijou> GetRecordsByMsUserID(MsUser loginUser, string ms_user_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiHaijou), MethodBase.GetCurrentMethod());

            List<SiHaijou> ret = new List<SiHaijou>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiHaijou> mapping = new MappingBase<SiHaijou>();

            Params.Add(new DBParameter("MS_USER_ID", ms_user_id));


            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            JoinSiHaijouItems(loginUser, ret);

            return ret;
        }


        private static void JoinSiHaijouItems(MsUser loginUser, List<SiHaijou> haijous)
        {
            foreach (SiHaijou c in haijous)
            {
                c.SiHaijouItems.AddRange(SiHaijouItem.GetRecordsBySiHaijouID(loginUser, c.SiHaijouID));
            }
        }


        public static SiHaijou GetRecord_前回配信(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiHaijou), MethodBase.GetCurrentMethod());

            List<SiHaijou> ret = new List<SiHaijou>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiHaijou> mapping = new MappingBase<SiHaijou>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }




        #region ISyncTable メンバ

        public bool InsertRecord(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_HAIJOU_ID", SiHaijouID));

            Params.Add(new DBParameter("HAISHIN_USER_ID", HaishinUserID));

            Params.Add(new DBParameter("HAISHIN_DATE", HaishinDate));

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

            Params.Add(new DBParameter("HAISHIN_USER_ID", HaishinUserID));

            Params.Add(new DBParameter("HAISHIN_DATE", HaishinDate));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_HAIJOU_ID", SiHaijouID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", SiHaijouID));

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
            Params.Add(new DBParameter("PK", SiHaijouID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiHaijouID == null;
        }
    }
}
