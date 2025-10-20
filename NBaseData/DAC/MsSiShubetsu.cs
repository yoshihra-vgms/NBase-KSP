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
    [TableAttribute("MS_SI_SHUBETSU")]
    public class MsSiShubetsu : ISyncTable
    {
        public enum SiShubetsu
        {
            乗船 = 1,
            有給休暇,
            待機,
            移動日,
            艤装員,
            研修,
            休職,
            傷病,
            陸上勤務,
            請暇,
            乗船休暇,
            休暇買上, 
            本年度休暇日数, 
            前年度休暇繰越日数,
            転船,
            旅行日,
            派遣,
            陸上休暇,
            特別休暇
        }

        public enum KyuukaFlagKind { 休暇以外, 休暇, 休暇買上, 本年度休暇日数, 前年度休暇繰越日数 }

        #region データメンバ

        /// <summary>
        /// 種別ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHUBETSU_ID", true)]
        public int MsSiShubetsuID { get; set; }

        
        
        
        /// <summary>
        /// 種別名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 休暇フラグ
        /// </summary>
        [DataMember]
        [ColumnAttribute("KYUUKA_FLAG")]
        public int KyuukaFlag { get; set; }

        /// <summary>
        /// 表示順序（職位）
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




        public static List<MsSiShubetsu> GetRecords(MsUser loginUser)
        {
            return GetRecords(null, loginUser);
        }


        public static List<MsSiShubetsu> GetRecords(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiShubetsu), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiShubetsu), "OrderBy"); 

            List<MsSiShubetsu> ret = new List<MsSiShubetsu>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiShubetsu> mapping = new MappingBase<MsSiShubetsu>();
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
        }


        internal static MsSiShubetsu GetRecordByName(MsUser loginUser, string name)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiShubetsu), "GetRecords");

            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsSiShubetsu), "ByName");

            List<MsSiShubetsu> ret = new List<MsSiShubetsu>();

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("NAME", name));
            MappingBase<MsSiShubetsu> mapping = new MappingBase<MsSiShubetsu>();
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

            if (!IsNew())
            {
                Params.Add(new DBParameter("MS_SI_SHUBETSU_ID", MsSiShubetsuID));
            }
            
            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("KYUUKA_FLAG", KyuukaFlag));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

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

            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("KYUUKA_FLAG", KyuukaFlag));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("MS_SI_SHUBETSU_ID", MsSiShubetsuID));
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
        //    Params.Add(new DBParameter("PK", MsSiShubetsuID));

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
            Params.Add(new DBParameter("PK", MsSiShubetsuID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion


        public bool IsNew()
        {
            return MsSiShubetsuID == 0;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
