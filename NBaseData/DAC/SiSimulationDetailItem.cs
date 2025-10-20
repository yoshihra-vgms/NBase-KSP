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
    [TableAttribute("SI_SIMULATION_DETAIL_ITEM")]
    public class SiSimulationDetailItem : ISyncTable
    {
        #region データメンバ

        /// <summary>
        /// 交代計画詳細項目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_SIMULATION_DETAIL_ITEM_ID", true)]
        public string SiSimulationDetailItemID { get; set; }

        /// <summary>
        /// 交代計画詳細ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("SI_SIMULATION_DETAIL_ID")]
        public string SiSimulationDetailID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_TYPE_ID")]
        //public string MsVesselTypeID { get; set; }
        public int _msVesselTypeID { get; set; }
        public string MsVesselTypeID {
            get { return _msVesselTypeID.ToString(); }
            set { _msVesselTypeID = int.Parse(value);} 
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        [ColumnAttribute("YEARS_ON_THIS_TYPE")]
        public decimal YearsOnThisType{ get; set; }


        /// <summary>
        /// CrewMatrixタイプ名
        /// </summary>
        [DataMember]
        [ColumnAttribute("CREW_MATRIX_NAME")]
        public string CrewMatrixName { get; set; }



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



        public SiSimulationDetailItem()
        {
        }

        
        

        public static List<SiSimulationDetailItem> GetRecords(MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSimulationDetailItem), MethodBase.GetCurrentMethod());

            List<SiSimulationDetailItem> ret = new List<SiSimulationDetailItem>();

            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSimulationDetailItem> mapping = new MappingBase<SiSimulationDetailItem>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }


        public static List<SiSimulationDetailItem> GetRecordsByDetailId(DBConnect dbConnect, MsUser loginUser, string siSimulationDetailId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(SiSimulationDetailItem), "GetRecords");

            List<SiSimulationDetailItem> ret = new List<SiSimulationDetailItem>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<SiSimulationDetailItem> mapping = new MappingBase<SiSimulationDetailItem>();

            if (siSimulationDetailId != null)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(SiSimulationDetailItem), "BySiSimulationDetailID");
                Params.Add(new DBParameter("SI_SIMULATION_DETAIL_ID", siSimulationDetailId));
            }
            ret = mapping.FillRecrods(dbConnect, loginUser.MsUserID, SQL, Params);

            return ret;
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
            string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("SI_SIMULATION_DETAIL_ITEM_ID", SiSimulationDetailItemID));
            Params.Add(new DBParameter("SI_SIMULATION_DETAIL_ID", SiSimulationDetailID));
            
            Params.Add(new DBParameter("MS_VESSEL_TYPE_ID", MsVesselTypeID));
            Params.Add(new DBParameter("YEARS_ON_THIS_TYPE", YearsOnThisType));
            Params.Add(new DBParameter("CREW_MATRIX_NAME", CrewMatrixName));

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

            Params.Add(new DBParameter("SI_SIMULATION_DETAIL_ID", SiSimulationDetailID));
            
            Params.Add(new DBParameter("MS_VESSEL_TYPE_ID", MsVesselTypeID));
            Params.Add(new DBParameter("YEARS_ON_THIS_TYPE", YearsOnThisType));
            Params.Add(new DBParameter("CREW_MATRIX_NAME", CrewMatrixName));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("SI_SIMULATION_DETAIL_ITEM_ID", SiSimulationDetailItemID));
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
            Params.Add(new DBParameter("PK", SiSimulationDetailItemID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect, loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion

        public bool IsNew()
        {
            return SiSimulationDetailItemID == null;
        }
    }
}
