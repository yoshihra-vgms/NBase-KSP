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
    [TableAttribute("BG_YOSAN_EXCEL")]
    public class BgYosanExcel
    {
        #region データメンバ

        /// <summary>
        /// 予算エクセルID
        /// </summary>
        [DataMember]
        [ColumnAttribute("BG_YOSAN_EXCEL_ID")]
        public long BgYosanExcelId { get; set; }

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
        /// 種別
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHUBETSU")]
        public int Shubetsu { get; set; }

        /// <summary>
        /// ファイル名
        /// </summary>
        [DataMember]
        [ColumnAttribute("FILE_NAME")]
        public string FileName { get; set; }

        /// <summary>
        /// ファイルデータ
        /// </summary>
        [DataMember]
        [ColumnAttribute("FILE_DATA")]
        public byte[] FileData { get; set; }


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




        /// <summary>
        /// 最終更新者
        /// </summary>
        [DataMember]
        [ColumnAttribute("RENEW_USER_NAME")]
        public string RenewUserName { get; set; }
        #endregion


        public enum ShubetsuEnum { 共通割掛船員, 貸船借船料 };


        public BgYosanExcel()
        {
            MsVesselID = int.MinValue;
        }


        public static BgYosanExcel GetRecordByYosanHeadIDAndMsVesselIdAndShubetsu(MsUser loginUser, int yosanHeadId, int msVesselId, int shubetsu)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanExcel), "GetRecordByYosanHeadIDAndShubetsu");

            List<BgYosanExcel> ret = new List<BgYosanExcel>();

            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", yosanHeadId));
            Params.Add(new DBParameter("SHUBETSU", shubetsu));

            if (msVesselId != int.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(BgYosanExcel), "ByMsVesselId");
                Params.Add(new DBParameter("MS_VESSEL_ID", msVesselId));
            }

            MappingBase<BgYosanExcel> mapping = new MappingBase<BgYosanExcel>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            if (ret.Count == 0)
            {
                return null;
            }

            return ret[0];
        }


        public bool InsertRecord(MsUser loginUser)
        {
            return InsertRecord(null, loginUser);
        }
            

        public bool InsertRecord(DBConnect dbConnect, MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanExcel), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("SHUBETSU", this.Shubetsu));
            Params.Add(new DBParameter("FILE_NAME", this.FileName));
            Params.Add(new DBParameter("FILE_DATA", this.FileData));
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


        public bool UpdateRecord(MsUser loginUser)
        {
            return UpdateRecord(null, loginUser);
        }


        public bool UpdateRecord(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanExcel), MethodBase.GetCurrentMethod());

            #region パラメーター設定
            ParameterConnection Params = new ParameterConnection();

            Params.Add(new DBParameter("YOSAN_HEAD_ID", this.YosanHeadID));
            Params.Add(new DBParameter("MS_VESSEL_ID", this.MsVesselID));
            Params.Add(new DBParameter("SHUBETSU", this.Shubetsu));
            Params.Add(new DBParameter("FILE_NAME", this.FileName));
            Params.Add(new DBParameter("FILE_DATA", this.FileData));
            Params.Add(new DBParameter("RENEW_DATE", this.RenewDate));
            Params.Add(new DBParameter("RENEW_USER_ID", this.RenewUserID));

            Params.Add(new DBParameter("BG_YOSAN_EXCEL_ID", this.BgYosanExcelId));
            Params.Add(new DBParameter("TS", this.Ts));
            #endregion

            int cnt = DBConnect.ExecuteNonQuery(dbConnect, loginUser.MsUserID, SQL, Params);

            if (cnt == 0)
            {
                return false;
            }
            return true;
        }


        public static bool InsertOrUpdate(MsUser loginUser, BgYosanExcel yosanExcel)
        {
            yosanExcel.RenewDate = DateTime.Now;
            yosanExcel.RenewUserID = loginUser.MsUserID;

            if (yosanExcel.IsNew())
            {
                return yosanExcel.InsertRecord(loginUser);
            }
            else
            {
                return yosanExcel.UpdateRecord(loginUser);
            }
        }


        public static bool InsertRecords_コピー(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser,
                                                                        int lastYosanHeadId,
                                                                        int yosanHeadId,
                                                                        int msVesselId)
        {
            string SQL = SqlMapper.SqlMapper.GetSql(typeof(BgYosanExcel), MethodBase.GetCurrentMethod());

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
            return BgYosanExcelId == 0;
        }
    }
}
