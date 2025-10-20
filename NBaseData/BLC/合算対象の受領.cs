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
using ORMapping.Atts;

using NBaseData.DAC;
using NBaseData.DS;

namespace NBaseData.BLC
{
    [DataContract()]
    public class 合算対象の受領
    {
        #region データメンバ

        //=================================
        // 受領から
        //=================================
        /// <summary>
        /// 受領ID
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_JRY_ID")]
        public string OdJryID { get; set; }

        /// <summary>
        /// 受領日
        /// </summary>
        [DataMember]
        [ColumnAttribute("JRY_DATE")]
        public DateTime JryDate { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        [ColumnAttribute("AMOUNT")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 値引き
        /// </summary>
        [DataMember]
        [ColumnAttribute("NEBIKI_AMOUNT")]
        public decimal NebikiAmount { get; set; }

        /// <summary>
        /// 消費税額
        /// </summary>
        [DataMember]
        [ColumnAttribute("TAX")]
        public decimal Tax { get; set; }

        /// <summary>
        /// 送料・運搬料
        /// </summary>
        [DataMember]
        [ColumnAttribute("CARRIAGE")]
        public decimal Carriage { get; set; }

        /// <summary>
        /// 科目NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("KAMOKU_NO")]
        public string KamokuNo { get; set; }

        /// <summary>
        /// 科目NO　>> 科目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("KAMOKU_NAME")]
        public string KamokuName { get; set; }

        /// <summary>
        /// 内訳科目NO
        /// </summary>
        [DataMember]
        [ColumnAttribute("UTIWAKE_KAMOKU_NO")]
        public string UtiwakeKamokuNo { get; set; }

        /// <summary>
        /// 内訳科目NO　>> 内訳科目名
        /// </summary>
        [DataMember]
        [ColumnAttribute("UTIWAKE_KAMOKU_NAME")]
        public string UtiwakeKamokuName { get; set; }


        //=================================
        // 手配依頼から
        //=================================
        /// <summary>
        /// 手配依頼内容
        /// </summary>
        [DataMember]
        [ColumnAttribute("OD_THI_NAIYOU")]
        public string ThiNaiyou { get; set; }


        //=================================
        // 発注（見積回答）から
        //=================================
        /// <summary>
        /// 発注日
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_HACHU_DATE")]
        public DateTime HachuDate { get; set; }

        /// <summary>
        /// 発注番号
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_HACHU_NO")]
        public string HachuNo { get; set; }

        /// <summary>
        /// 顧客ID >>  顧客名
        /// </summary>
        [DataMember]
        [ColumnAttribute("MS_CUSTOMER_NAME")]
        public string CustomerName { get; set; }
        
        #endregion


        public static List<合算対象の受領> GetRecordsByFilter(NBaseData.DAC.MsUser loginUser, 合算対象の受領Filter filter)
        {
            List<合算対象の受領> ret = new List<合算対象の受領>();
            ParameterConnection Params = new ParameterConnection();
            MappingBase<合算対象の受領> mapping = new MappingBase<合算対象の受領>();

            string SQL = SqlMapper.SqlMapper.GetSql(typeof(合算対象の受領), "GetRecords");

            Params.Add(new DBParameter("MS_CUSTOMER_ID", filter.MsCustomerID));
            Params.Add(new DBParameter("MS_THI_IRAI_SBT_ID", filter.MsThiIraiSbtID));
            Params.Add(new DBParameter("MS_VESSEL_ID", filter.MsVesselID));
            Params.Add(new DBParameter("JRY_STATUS", (int)OdJry.STATUS.受領承認済み));
            if (filter.MsThiIraiShousaiID != null && filter.MsThiIraiShousaiID.Length > 0)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(合算対象の受領), "FilterByThiIraiShousaiID");
                Params.Add(new DBParameter("MS_THI_IRAI_SHOUSAI_ID", filter.MsThiIraiShousaiID));
            }
            if (filter.JryDateFrom != null && filter.JryDateFrom != DateTime.MinValue &&
                filter.JryDateTo != null && filter.JryDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(合算対象の受領), "FilterByJryDateFromTo");
                Params.Add(new DBParameter("JRY_DATE_FROM", filter.JryDateFrom));
                Params.Add(new DBParameter("JRY_DATE_TO", filter.JryDateTo));
            }
            else if (filter.JryDateFrom != null && filter.JryDateFrom != DateTime.MinValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(合算対象の受領), "FilterByJryDateFrom");
                Params.Add(new DBParameter("JRY_DATE_FROM", filter.JryDateFrom));
            }
            else if (filter.JryDateTo != null && filter.JryDateTo != DateTime.MaxValue)
            {
                SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(合算対象の受領), "FilterByJryDateTo");
                Params.Add(new DBParameter("JRY_DATE_TO", filter.JryDateTo));
            }
            SQL += " " + SqlMapper.SqlMapper.GetSql(typeof(合算対象の受領), "OrderBy");
            ret = mapping.FillRecrods(loginUser.MsUserID, SQL, Params);


            List<MsKamoku> kamokus = MsKamoku.GetRecordsByHachuKamoku(null, loginUser);
            foreach (合算対象の受領 jry in ret)
            {
                foreach (MsKamoku kamoku in kamokus)
                {
                    if (jry.KamokuNo == kamoku.KamokuNo && jry.UtiwakeKamokuNo == kamoku.UtiwakeKamokuNo)
                    {
                        jry.KamokuName = kamoku.KamokuName;
                        jry.UtiwakeKamokuName = kamoku.UtiwakeKamokuName;
                        break;
                    }
                }
            }

            return ret;
        }

    }
}
