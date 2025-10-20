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
    [TableAttribute("MS_CARGO")]
    public class MsCargo : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 貨物ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CARGO_ID", true)]
        public int MsCargoID { get; set; }

        /// <summary>
        /// 貨物NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARGO_NO")]
        public string CargoNo { get; set; }

        /// <summary>
        /// 貨物名
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARGO_NAME")]
        public string CargoName { get; set; }

        /// <summary>
        /// 更新者
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
        /// 主な荷主
        /// </summary>
        [DataMember]
        [ColumnAttribute("NINUSHI")]
        public string Ninushi { get; set; }


        /// <summary>
        /// 輸送品目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_YUSO_ITEM_ID")]
        public int MsYusoItemID { get; set; }

        /// <summary>
        /// 輸送品目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("YUSO_ITEM_NAME")]
        public string MsYusoItemName { get; set; }

        /// <summary>
        /// 積荷グループID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CARGO_GROUP_ID")]
        public int MsCargoGroupID { get; set; }

        /// <summary>
        /// 貨物名
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARGO_GROUP_NAME")]
        public string CargoGroupName { get; set; }

        #endregion

        public override string ToString()
        {
            return CargoName;
        }

        public static MsCargo GetRecord(MsUser loginUser,string CargoNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargo), MethodBase.GetCurrentMethod());

            List<MsCargo> ret = new List<MsCargo>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCargo> mapping = new MappingBase<MsCargo>();
            Params.Add(new DBParameter("CARGO_NO", CargoNo));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count > 0)
            {
                return ret[0];
            }
            return null;
        }

        public static List<MsCargo> SearchRecords(MsUser loginUser,string CargoNo,string CargoName,int MsYusoItemID)
        {
            string SQL = "";
            if (MsYusoItemID > 0)
            {
                SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargo), "SearchRecords1");
            }
            else
            {
                SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargo), "SearchRecords2");
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCargo), "WhereCargoNoAndCargoName");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsCargo), "OrderBy");

            List<MsCargo> ret = new List<MsCargo>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCargo> mapping = new MappingBase<MsCargo>();
            if (MsYusoItemID > 0)
            {
                Params.Add(new DBParameter("MS_YUSO_ITEM_ID", MsYusoItemID.ToString()));
            }
            Params.Add(new DBParameter("CARGO_NO", "%" + CargoNo + "%"));
            Params.Add(new DBParameter("CARGO_NAME", "%" + CargoName + "%"));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsCargo> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargo), MethodBase.GetCurrentMethod());

            List<MsCargo> ret = new List<MsCargo>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCargo> mapping = new MappingBase<MsCargo>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        /// <summary>
        /// 重複チェック 重複→true
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="CargoNo"></param>
        /// <returns></returns>
        public static bool ExistCargo(MsUser loginUser, string CargoNo)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargo), MethodBase.GetCurrentMethod());

            List<MsCargo> ret = new List<MsCargo>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsCargo> mapping = new MappingBase<MsCargo>();
            Params.Add(new DBParameter("CARGO_NO", CargoNo));

            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return false;
            }
            return true;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargo), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            if (Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)//Common.DB_TYPE.SQLSERVER)// 20150902 Postgresql_Client化対応 条件変更
            {
                Params.Add(new DBParameter("MS_CARGO_ID", MsCargoID));
            }
            Params.Add(new DBParameter("CARGO_NO", CargoNo));
            Params.Add(new DBParameter("CARGO_NAME", CargoName));
            Params.Add(new DBParameter("NINUSHI", Ninushi));
            
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = 0;
            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;

        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }

        public bool UpdateRecord(DBConnect dbConnect, MsUser loginUser)
        {

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsCargo), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("CARGO_NO", CargoNo));
            Params.Add(new DBParameter("CARGO_NAME", CargoName));
            Params.Add(new DBParameter("NINUSHI", Ninushi));

            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("DATA_NO", DataNo));

            Params.Add(new DBParameter("MS_CARGO_ID", MsCargoID));
            Params.Add(new DBParameter("TS", Ts));

            int cnt = 0;
            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 論理削除
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool DeleteRecord(MsUser loginUser)
        {
            DeleteFlag = 1;
            return UpdateRecord(loginUser);
        }

        #region ISyncTable メンバ

        public bool Exists(MsUser loginUser)
        {
            return Exists(null, loginUser);
        }
        public bool Exists(ORMapping.DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("PK", MsCargoID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
