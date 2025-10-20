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
using NBaseData.DAC;

namespace NBaseData.BLC
{
    [DataContract()]
    public class 管理表Info
    {
        #region データメンバ

        #region 手配から
        /// <summary>
        /// 手配 : 船ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_VESSEL_ID")]
        public int MsVesselID { get; set; }

        /// <summary>
        /// 手配 : 手配依頼日
        /// </summary>
        [DataMember]
        [ColumnAttribute("THI_IRAI_DATE")]
        public DateTime 手配依頼日 { get; set; }

        /// <summary>
        /// 手配 : 手配依頼内容
        /// </summary>
        [DataMember]
        [ColumnAttribute("NAIYOU")]
        public string 手配内容 { get; set; }

        /// <summary>
        /// 手配 : 備考
        /// </summary>
        [DataMember]
        [ColumnAttribute("BIKOU")]
        public string 備考 { get; set; }

        /// <summary>
        /// 手配 : 事務担当者名
        /// </summary>
        [DataMember]
        [ColumnAttribute("JIM_TANTOU_NAME")]
        public string 発注者 { get; set; }
        #endregion

        #region 見積依頼から
        /// <summary>
        /// 見積依頼 : 入渠科目ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_NYUKYO_KAMOKU_ID")]
        public string 入渠科目ID { get; set; }
        #endregion

        #region 見積回答から
        /// <summary>
        /// 見積回答 : 顧客名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_NAME")]
        public string 業者 { get; set; }

        /// <summary>
        /// 見積回答 : 発注日
        /// </summary>
        [DataMember]
        [ColumnAttribute("HACHU_DATE")]
        public DateTime 発注日 { get; set; }

        /// <summary>
        /// 見積回答 : 発注番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("HACHU_NO")]
        public string 発注番号 { get; set; }

        /// <summary>
        /// 見積回答 : 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("AMOUNT")]
        public decimal 発注金額 { get; set; }

        /// <summary>
        /// 見積回答 : 見積値引き
        /// </summary>
        [DataMember]
        [ColumnAttribute("MK_AMOUNT")]
        public decimal 見積値引き { get; set; }
        #endregion

        #region 受領から
        /// <summary>
        /// 受領 : ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("J_STATUS")]
        public int 受領ステータス { get; set; }
        
        /// <summary>
        /// 受領 : 受領日
        /// </summary>
        [DataMember]
        [ColumnAttribute("JRY_DATE")]
        public DateTime 受領日 { get; set; }

        /// <summary>
        /// 受領 : 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("J_AMOUNT")]
        public decimal 受領金額 { get; set; }

        /// <summary>
        /// 受領 : 値引き
        /// </summary>
        [DataMember]
        [ColumnAttribute("J_NEBIKI_AMOUNT")]
        public decimal 受領値引き { get; set; }
        #endregion

        #region 落成から
        /// <summary>
        /// 落成 : ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("R_STATUS")]
        public int 落成ステータス { get; set; }

        /// <summary>
        /// 落成 : 落成金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("R_AMOUNT")]
        public decimal 落成金額 { get; set; }

        /// <summary>
        /// 落成 : 請求値引き額
        /// </summary>
        [DataMember]
        [ColumnAttribute("R_NEBIKI_AMOUNT")]
        public decimal 落成値引き { get; set; }

        /// <summary>
        /// 落成 : 落成入手日
        /// </summary>
        [DataMember]
        [ColumnAttribute("R_SHR_DATE")]
        public DateTime 落成入手日 { get; set; }
        #endregion

        #region 支払から
        /// <summary>
        /// 支払 : ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("S_STATUS")]
        public int 支払ステータス { get; set; }

        /// <summary>
        /// 支払 : 処理ステータス
        /// </summary>
        [DataMember]
        [ColumnAttribute("S_SYORI_STATUS")]
        public string 処理ステータス { get; set; }

        /// <summary>
        /// 支払 : 支払金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("S_AMOUNT")]
        public decimal 支払金額 { get; set; }

        /// <summary>
        /// 支払 : 請求値引き額
        /// </summary>
        [DataMember]
        [ColumnAttribute("S_NEBIKI_AMOUNT")]
        public decimal 請求値引き { get; set; }

        /// <summary>
        /// 支払 : 請求書日
        /// </summary>
        [DataMember]
        [ColumnAttribute("SHR_IRAI_DATE")]
        public DateTime 請求書日 { get; set; }

        /// <summary>
        /// 支払 : 支払日
        /// </summary>
        [DataMember]
        [ColumnAttribute("S_SHR_DATE")]
        public DateTime 支払日 { get; set; }

        /// <summary>
        /// 支払 : 計上日
        /// </summary>
        [DataMember]
        [ColumnAttribute("KEIJO_DATE")]
        public DateTime 計上日 { get; set; }

        /// <summary>
        /// 支払 : 起票日
        /// </summary>
        [DataMember]
        [ColumnAttribute("KIHYOUBI")]
        public DateTime 起票日 { get; set; }
        #endregion

        #endregion




        /// <summary>
        /// コンストラクタ
        /// </summary>
        public 管理表Info()
        {
        }

        public static List<管理表Info> GetRecordsForランニング管理表(NBaseData.DAC.MsUser loginUser, DateTime hachuDateFrom, DateTime hachuDateTo)
        {
            List<管理表Info> ret = new List<管理表Info>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(管理表Info), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MK_STATUS", (int)OdMk.STATUS.発注済み));
            Params.Add(new DBParameter("HACHU_DATE_FROM", hachuDateFrom.ToShortDateString()));
            Params.Add(new DBParameter("HACHU_DATE_TO", hachuDateTo.ToShortDateString()));
            Params.Add(new DBParameter("SHR_SBT_RKS", (int)OdShr.SBT.落成));
            Params.Add(new DBParameter("SHR_SBT_SHR", (int)OdShr.SBT.支払));
            Params.Add(new DBParameter("SHR_STATUS_IRAIZUMI", (int)OdShr.STATUS.支払依頼済み));
            Params.Add(new DBParameter("SHR_STATUS_SUMI", (int)OdShr.STATUS.支払済));
            Params.Add(new DBParameter("SYORI_STATUS_JISSEKI", (int)支払実績連携IF.STATUS.実績));
            Params.Add(new DBParameter("SYORI_STATUS_SUMI", (int)支払実績連携IF.STATUS.支払済み));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.修繕)));
            Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", MsThiIraiShousai.ToId(MsThiIraiShousai.ThiIraiShousaiEnum.部品購入)));
            

            MappingBase<管理表Info> mapping = new MappingBase<管理表Info>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<管理表Info> GetRecordsFor入渠管理表(NBaseData.DAC.MsUser loginUser, DateTime hachuDateFrom, DateTime hachuDateTo)
        {
            List<管理表Info> ret = new List<管理表Info>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(管理表Info), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MK_STATUS", (int)OdMk.STATUS.発注済み));
            Params.Add(new DBParameter("HACHU_DATE_FROM", hachuDateFrom.ToShortDateString()));
            Params.Add(new DBParameter("HACHU_DATE_TO", hachuDateTo.ToShortDateString()));
            Params.Add(new DBParameter("SHR_SBT_RKS", (int)OdShr.SBT.落成));
            Params.Add(new DBParameter("SHR_SBT_SHR", (int)OdShr.SBT.支払));
            Params.Add(new DBParameter("SHR_STATUS_IRAIZUMI", (int)OdShr.STATUS.支払依頼済み));
            Params.Add(new DBParameter("SHR_STATUS_SUMI", (int)OdShr.STATUS.支払済));
            Params.Add(new DBParameter("SYORI_STATUS_JISSEKI", (int)支払実績連携IF.STATUS.実績));
            Params.Add(new DBParameter("SYORI_STATUS_SUMI", (int)支払実績連携IF.STATUS.支払済み));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.修繕)));
            Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", MsThiIraiShousai.ToId(MsThiIraiShousai.ThiIraiShousaiEnum.部品購入)));


            MappingBase<管理表Info> mapping = new MappingBase<管理表Info>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }

        public static List<管理表Info> GetRecordsFor船用品管理表(NBaseData.DAC.MsUser loginUser, DateTime hachuDateFrom, DateTime hachuDateTo)
        {
            List<管理表Info> ret = new List<管理表Info>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(管理表Info), MethodBase.GetCurrentMethod());

            ParameterConnection Params = new ParameterConnection();
            Params.Add(new DBParameter("MK_STATUS", (int)OdMk.STATUS.発注済み));
            Params.Add(new DBParameter("HACHU_DATE_FROM", hachuDateFrom.ToShortDateString()));
            Params.Add(new DBParameter("HACHU_DATE_TO", hachuDateTo.ToShortDateString()));
            Params.Add(new DBParameter("SHR_SBT_SHR", (int)OdShr.SBT.支払));
            Params.Add(new DBParameter("SHR_STATUS_IRAIZUMI", (int)OdShr.STATUS.支払依頼済み));
            Params.Add(new DBParameter("SHR_STATUS_SUMI", (int)OdShr.STATUS.支払済));
            Params.Add(new DBParameter("SYORI_STATUS_JISSEKI", (int)支払実績連携IF.STATUS.実績));
            Params.Add(new DBParameter("SYORI_STATUS_SUMI", (int)支払実績連携IF.STATUS.支払済み));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品)));


            MappingBase<管理表Info> mapping = new MappingBase<管理表Info>();
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);

            return ret;
        }
    }
}
