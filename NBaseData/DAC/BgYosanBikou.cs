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
    [TableAttribute("BG_YOSAN_BIKOU")]
    public class BgYosanBikou
    {

        #region データメンバ

        /// <summary>
        /// 予算備考ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("BG_YOSAN_BIKOU_ID")]
        public long BgYosanBikouId { get; set; }

        /// <summary>
        /// 予算ヘッダーID
        /// </summary>
        [DataMember]
        [ColumnAttribute("YOSAN_HEAD_ID")]
        public int YosanHeadID { get; set; }

        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        /// <summary>
        /// 削除フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("DELETE_FLAG")]
        public int DeleteFlag { get; set; }


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


        public BgYosanBikou()
        {
        }


        public static BgYosanBikou GetRecordByYosanHeadIDAndMsVesselID(MsUser loginUser, int yosanHeadId, int msVesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanBikou), MethodBase.GetCurrentMethod());

            List<BgYosanBikou> ret = new List<BgYosanBikou>();

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadId));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));

            MappingBase<BgYosanBikou> mapping = new MappingBase<BgYosanBikou>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }

        //船ID
        public static List<BgYosanBikou> GetRecordsByMsVesselID(MsUser loginUser, int ms_vessel_id)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanBikou), MethodBase.GetCurrentMethod());

            List<BgYosanBikou> ret = new List<BgYosanBikou>();

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("MS_VESSEL_ID", ms_vessel_id));

            MappingBase<BgYosanBikou> mapping = new MappingBase<BgYosanBikou>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            
            return ret;
        }


        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanBikou), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("BIKOU", this.Bikou));
            Params.Add(new DBParameter("DELETE_FLAG", this.DeleteFlag));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));
            Params.Add(new DBParameter("TS", this.Ts));

            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }


        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanBikou), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("BIKOU", this.Bikou));
            Params.Add(new DBParameter("DELETE_FLAG", this.DeleteFlag));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));

            Params.Add(new DBParameter("BG_YOSAN_BIKOU_ID", this.BgYosanBikouId));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }


        public static bool InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, BgYosanBikou yosanBikou)
        {
            yosanBikou.RenewDate = DateTime.Now;
            yosanBikou.RenewUserID = loginUser.MsUserID;

            if (yosanBikou.IsNew())
            {
                return yosanBikou.InsertRecord(dbConnect, loginUser);
            }
            else
            {
                return yosanBikou.UpdateRecord(dbConnect, loginUser);
            }
        }


        public static bool InsertRecords_コピー(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser,
                                                                        int lastYosanHeadId,
                                                                        int yosanHeadId,
                                                                        int msVesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanBikou), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadId));
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("LAST_YOSAN_HEAD_ID", lastYosanHeadId));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        
        public bool IsNew()
        {
            return BgYosanBikouId == 0;
        }
    }
}
