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
    [TableAttribute("BG_UCHIWAKE_YOSAN_ITEM")]
    public class BgUchiwakeYosanItem
    {

        #region データメンバ

        /// <summary>
        /// 内訳予算項目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("BG_UCHIWAKE_YOSAN_ITEM_ID")]
        public long BgUchiwakeYosanItemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("VESSEL_YOSAN_ID")]
        public int VesselYosanID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("NENGETSU")]
        public string Nengetsu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_HIMOKU_ID")]
        public int MsHimokuID { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("AMOUNT")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string Bikou { get; set; }

        /// <summary>
        /// 表示順序
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOW_ORDER")]
        public int ShowOrder { get; set; }

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


        public BgUchiwakeYosanItem()
        {
        }


        public static List<BgUchiwakeYosanItem> GetRecords_入渠(MsUser loginUser, int yosanHeadId, int msVesselId, string yearStart, string yearEnd)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgUchiwakeYosanItem), MethodBase.GetCurrentMethod());

            List<BgUchiwakeYosanItem> ret = new List<BgUchiwakeYosanItem>();

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadId));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("YEAR_START", yearStart));
            Params.Add(new DBParameter("YEAR_END", yearEnd));

            MappingBase<BgUchiwakeYosanItem> mapping = new MappingBase<BgUchiwakeYosanItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgUchiwakeYosanItem), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("VESSEL_YOSAN_ID", this.VesselYosanID));
            Params.Add(new DBParameter("NENGETSU", this.Nengetsu));
            Params.Add(new DBParameter("MS_HIMOKU_ID", this.MsHimokuID));
            Params.Add(new DBParameter("AMOUNT", this.Amount));
            Params.Add(new DBParameter("BIKOU", this.Bikou));
            Params.Add(new DBParameter("SHOW_ORDER", this.ShowOrder));
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
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgUchiwakeYosanItem), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("VESSEL_YOSAN_ID", this.VesselYosanID));
            Params.Add(new DBParameter("NENGETSU", this.Nengetsu));
            Params.Add(new DBParameter("MS_HIMOKU_ID", this.MsHimokuID));
            Params.Add(new DBParameter("AMOUNT", this.Amount));
            Params.Add(new DBParameter("BIKOU", this.Bikou));
            Params.Add(new DBParameter("SHOW_ORDER", this.ShowOrder));
            Params.Add(new DBParameter("DELETE_FLAG", this.DeleteFlag));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));

            Params.Add(new DBParameter("BG_UCHIWAKE_YOSAN_ITEM_ID", this.BgUchiwakeYosanItemId));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }


        public static bool InsertOrUpdate(DBConnect dbConnect, MsUser loginUser, List<BgUchiwakeYosanItem> items)
        {
            try
            {
                foreach (BgUchiwakeYosanItem i in items)
                {
                    i.RenewDate = DateTime.Now;
                    i.RenewUserID = loginUser.MsUserID;

                    if (i.IsNew())
                    {
                        i.InsertRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        i.UpdateRecord(dbConnect, loginUser);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static bool InsertRecords_コピー(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser,
                                                                         int vesselYosanId, int lastYosanHeadId,
                                                                        int msVesselId, int vesselYosanYear)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgUchiwakeYosanItem), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("VESSEL_YOSAN_ID", vesselYosanId));
            Params.Add(new DBParameter("RENEW_DATE", DateTime.Now));
            Params.Add(new DBParameter("RENEW_USER_ID", loginUser.MsUserID));

            Params.Add(new DBParameter("LAST_YOSAN_HEAD_ID", lastYosanHeadId));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("VESSEL_YOSAN_YEAR", vesselYosanYear));
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
            return BgUchiwakeYosanItemId == 0;
        }

        public static bool ExistsLastRecord(DBConnect dbConnect, MsUser loginUser, int lastYosanHeadId, int msVesselId, int vesselYosanYear)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgUchiwakeYosanItem), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("LAST_YOSAN_HEAD_ID", lastYosanHeadId));
            Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            Params.Add(new DBParameter("VESSEL_YOSAN_YEAR", vesselYosanYear));
            #endregion

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }
    }
}
