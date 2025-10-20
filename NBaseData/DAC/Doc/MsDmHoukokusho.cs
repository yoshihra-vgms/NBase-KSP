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
    [TableAttribute("MS_DM_HOUKOKUSHO")]
    public class MsDmHoukokusho : ISyncTable
    {
        #region データメンバ
        /// <summary>
        /// 報告書ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_HOUKOKUSHO_ID", true)]
        public string MsDmHoukokushoID { get; set; }
        
        /// <summary>
        /// 分類ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_BUNRUI_ID", true)]
        public string MsDmBunruiID { get; set; }
        
        /// <summary>
        /// 小分類ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_SHOUBUNRUI_ID", true)]
        public string MsDmShoubunruiID { get; set; }

        /// <summary>
        /// 文書番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("BUNSHO_NO")]
        public string BunshoNo { get; set; }

        /// <summary>
        /// 文書名
        /// </summary>
        [DataMember]
        [ColumnAttribute("BUNSHO_NAME")]
        public string BunshoName { get; set; }

        /// <summary>
        /// 提出周期
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHUKI")]
        public string Shuki { get; set; }

        /// <summary>
        /// 提出時期
        /// </summary>
        [DataMember]
        [ColumnAttribute("JIKI")]
        public string Jiki { get; set; }

        /// <summary>
        /// 未提出チェック対象
        /// </summary>
        [DataMember]
        [ColumnAttribute("CHECK_TARGET")]
        public int CheckTarget { get; set; }

        /// <summary>
        /// 雛形ファイル名
        /// </summary>
        [DataMember]
        [ColumnAttribute("TEMPLATE_FILE_NAME")]
        public string TemplateFileName { get; set; }

        /// <summary>
        /// 雛形ファイル更新日
        /// </summary>
        [DataMember]
        [ColumnAttribute("FILE_UPDATE_DATE")]
        public DateTime FileUpdateDate { get; set; }


        #region 共通項目
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
        /// 分類名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_BUNRUI_NAME")]
        public string BunruiName { get; set; }


        /// <summary>
        /// 小分類名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_DM_SHOUBUNRUI_NAME")]
        public string ShoubunruiName { get; set; }

        #endregion

        public MsDmHoukokusho()
        {
        }

        public static List<MsDmHoukokusho> GetRecords(NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), MethodBase.GetCurrentMethod());
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), "OrderBy");
            List<MsDmHoukokusho> ret = new List<MsDmHoukokusho>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsDmHoukokusho> mapping = new MappingBase<MsDmHoukokusho>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsDmHoukokusho> SearchRecords(NBaseData.DAC.MsUser loginUser, string bunruiId, string shoubunruiId, string bunshoNo, string bunshoName)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), "GetRecords");
            List<MsDmHoukokusho> ret = new List<MsDmHoukokusho>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsDmHoukokusho> mapping = new MappingBase<MsDmHoukokusho>();

            if (bunruiId.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), "ByMsDmBunruiID");
                Params.Add(new DBParameter("MS_DM_BUNRUI_ID", bunruiId));
            }
            if (shoubunruiId.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), "ByMsDmShoubunruiID");
                Params.Add(new DBParameter("MS_DM_SHOUBUNRUI_ID", shoubunruiId));
            }
            if (bunshoNo.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), "LikeBunshoNo");
                Params.Add(new DBParameter("BUNSHO_NO", "%" + bunshoNo + "%"));
            }
            if (bunshoName.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), "LikeBunshoName");
                Params.Add(new DBParameter("BUNSHO_NAME", "%" + bunshoName + "%"));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), "OrderBy");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static MsDmHoukokusho GetRecord(NBaseData.DAC.MsUser loginUser, string MsDmHoukokushoID)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), "GetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), "ByMsDmHoukokushoID");
            List<MsDmHoukokusho> ret = new List<MsDmHoukokusho>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<MsDmHoukokusho> mapping = new MappingBase<MsDmHoukokusho>();
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", MsDmHoukokushoID));
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
                return null;
            return ret[0];
        }




        public static List<MsDmHoukokusho> GetTargetRecords(NBaseData.DAC.MsUser loginUser, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), "GetTargetRecords");
            
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.報告書マスタ));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.船));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));
            
            List<MsDmHoukokusho> ret = new List<MsDmHoukokusho>();
            MappingBase<MsDmHoukokusho> mapping = new MappingBase<MsDmHoukokusho>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsDmHoukokusho> GetDataFileNotFoundRecords(NBaseData.DAC.MsUser loginUser, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), "GetTargetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), "AndFileNotFoundRecords");
            
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.報告書マスタ));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.船));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));
            
            List<MsDmHoukokusho> ret = new List<MsDmHoukokusho>();
            MappingBase<MsDmHoukokusho> mapping = new MappingBase<MsDmHoukokusho>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<MsDmHoukokusho> GetDataUpdateDateDiffRecords(NBaseData.DAC.MsUser loginUser, int vesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), "GetTargetRecords");
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), "AndDataUpdateDateDiffRecords");

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("LINK_SAKI", (int)NBaseData.DS.DocConstants.LinkSakiEnum.報告書マスタ));
            Params.Add(new DBParameter("KOUKAI_SAKI", (int)NBaseData.DS.DocConstants.RoleEnum.船));
            Params.Add(new DBParameter("MS_VESSEL_ID", vesselId));

            List<MsDmHoukokusho> ret = new List<MsDmHoukokusho>();
            MappingBase<MsDmHoukokusho> mapping = new MappingBase<MsDmHoukokusho>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }



        #region INSERT
        public bool InsertRecord(NBaseData.DAC.MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }

        public bool InsertRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", MsDmHoukokushoID));
            Params.Add(new DBParameter("MS_DM_BUNRUI_ID", MsDmBunruiID));
            Params.Add(new DBParameter("MS_DM_SHOUBUNRUI_ID", MsDmShoubunruiID));
            Params.Add(new DBParameter("BUNSHO_NO", BunshoNo));
            Params.Add(new DBParameter("BUNSHO_NAME", BunshoName));
            Params.Add(new DBParameter("SHUKI", Shuki));
            Params.Add(new DBParameter("JIKI", Jiki));
            Params.Add(new DBParameter("CHECK_TARGET", CheckTarget));
            Params.Add(new DBParameter("TEMPLATE_FILE_NAME", TemplateFileName));
            Params.Add(new DBParameter("FILE_UPDATE_DATE", FileUpdateDate));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));
            Params.Add(new DBParameter("TS", Ts));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
        #endregion

        #region UPDATE
        public bool UpdateRecord(NBaseData.DAC.MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }

        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(MsDmHoukokusho), MethodBase.GetCurrentMethod());

            int cnt = 0;
            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MS_DM_BUNRUI_ID", MsDmBunruiID));
            Params.Add(new DBParameter("MS_DM_SHOUBUNRUI_ID", MsDmShoubunruiID));
            Params.Add(new DBParameter("BUNSHO_NO", BunshoNo));
            Params.Add(new DBParameter("BUNSHO_NAME", BunshoName));
            Params.Add(new DBParameter("SHUKI", Shuki));
            Params.Add(new DBParameter("JIKI", Jiki));
            Params.Add(new DBParameter("CHECK_TARGET", CheckTarget));
            Params.Add(new DBParameter("TEMPLATE_FILE_NAME", TemplateFileName));
            Params.Add(new DBParameter("FILE_UPDATE_DATE", FileUpdateDate));

            Params.Add(new DBParameter("DELETE_FLAG", DeleteFlag));
            Params.Add(new DBParameter("SEND_FLAG", SendFlag));
            Params.Add(new DBParameter("VESSEL_ID", VesselID));
            Params.Add(new DBParameter("DATA_NO", DataNo));
            Params.Add(new DBParameter("USER_KEY", UserKey));
            Params.Add(new DBParameter("RENEW_DATE", RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", RenewUserID));

            Params.Add(new DBParameter("TS", Ts));
            Params.Add(new DBParameter("MS_DM_HOUKOKUSHO_ID", MsDmHoukokushoID));

            cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);
            if (cnt == 0)
                return false;
            return true;
        }
        #endregion

        #region ISyncTable メンバ

        //public bool Exists(MsUser loginUser)
        //{
        //    string SQL = SqlMapper.SqlMapper.GetSql(GetType(), MethodBase.GetCurrentMethod());

        //    ParameterConnection Params = new ParameterConnection();
        //    Params.Add(new DBParameter("PK", MsDmHoukokushoID));

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
            Params.Add(new DBParameter("PK", MsDmHoukokushoID));

            int count = Convert.ToInt32(DBConnect.ExecuteScalar(dbConnect,loginUser.MsUserID, SQL, Params));

            return count > 0 ? true : false;
        }

        #endregion
    }
}



