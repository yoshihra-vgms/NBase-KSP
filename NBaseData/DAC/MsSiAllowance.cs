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
    [Serializable]
    [DataContract()]
    [TableAttribute("MS_SI_ALLOWANCE")]
    public class MsSiAllowance : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 手当ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_ALLOWANCE_ID", true)]
        public int MsSiAllowanceID { get; set; }

        /// <summary>
        /// 手当名
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 作業内容
        /// </summary>
        [DataMember]
        [ColumnAttribute("CONTENTS")]
        public string Contents { get; set; }

        /// <summary>
        /// 対象船
        /// </summary>
        [DataMember]
        [ColumnAttribute("TARGET_VESSEL")]
        public string TargetVessel { get; set; }

        /// <summary>
        /// 部署
        /// </summary>
        [DataMember]
        [ColumnAttribute("DEPARTMENT")]
        public int Department { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("ALLOWANCE")]
        public int Allowance { get; set; }

        /// <summary>
        /// 職名ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_SI_SHOKUMEI_ID")]
        public int MsSiShokumeiID { get; set; }


        /// <summary>
        /// 表示順序
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHOW_ORDER")]
        public int ShowOrder { get; set; }


        /// <summary>
        ///　按分方法 ０：人数割、１：乗船日数割
        /// </summary>
        [DataMember]
        [ColumnAttribute("DISTRIBUTION_FLAG")]
        public int DistributionFlag { get; set; }




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
        /// 同期:ユーザキー
        /// </summary>
        [DataMember]
        [ColumnAttribute("USER_KEY")]
        public string UserKey { get; set; }

        /// <summary>
        /// 同期:データNO
        /// </summary>
        [DataMember]
        [ColumnAttribute("DATA_NO")]
        public Int64 DataNo { get; set; }

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

        public override string ToString()
        {
            return Name;
        }

        public bool IsNew()
        {
            return MsSiAllowanceID == -1;
        }

        public MsSiAllowance()
        {
            MsSiAllowanceID = -1;
        }

        public static List<MsSiAllowance> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiAllowance), MethodBase.GetCurrentMethod());
            List<MsSiAllowance> ret = new List<MsSiAllowance>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsSiAllowance> mapping = new MappingBase<MsSiAllowance>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }


        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiAllowance), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            if (ORMapping.Common.DBTYPE == Common.DB_TYPE.POSTGRESQL_CLIENT)
            {
	            Params.Add(new DBParameter("MS_SI_ALLOWANCE_ID", MsSiAllowanceID));
            }
            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("CONTENTS", Contents));
            Params.Add(new DBParameter("TARGET_VESSEL", TargetVessel));
            Params.Add(new DBParameter("DEPARTMENT", Department));
            Params.Add(new DBParameter("ALLOWANCE", Allowance));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder)); 
            Params.Add(new DBParameter("DISTRIBUTION_FLAG", DistributionFlag)); 

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }

        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsSiAllowance), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("NAME", Name));
            Params.Add(new DBParameter("CONTENTS", Contents));
            Params.Add(new DBParameter("TARGET_VESSEL", TargetVessel));
            Params.Add(new DBParameter("DEPARTMENT", Department));
            Params.Add(new DBParameter("ALLOWANCE", Allowance));
            Params.Add(new DBParameter("MS_SI_SHOKUMEI_ID", MsSiShokumeiID));
            Params.Add(new DBParameter("SHOW_ORDER", ShowOrder));
            Params.Add(new DBParameter("DISTRIBUTION_FLAG", DistributionFlag));

            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_SI_ALLOWANCE_ID", MsSiAllowanceID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
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
            Params.Add(new DBParameter("PK", MsSiAllowanceID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}
