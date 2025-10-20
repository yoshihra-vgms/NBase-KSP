using System;
using System.Collections;
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
using NBaseData.DS;

namespace NBaseData.BLC
{
    [DataContract()]
    public class 概算計上一覧Row
    {
        #region データメンバ
        /// <summary>
        /// 船ID
        /// </summary>
        [DataMember]
        public int MsVesselId = -1;
        
        /// <summary>
        /// 船
        /// </summary>
        [DataMember]
        public string 船 = "";

        /// <summary>
        /// 種別
        /// </summary>
        [DataMember]
        public string 種別 = "";

        /// <summary>
        /// 詳細種別
        /// </summary>
        [DataMember]
        public string 詳細種別 = "";

        /// <summary>
        /// 件名
        /// </summary>
        [DataMember]
        public string 件名 = "";

        /// <summary>
        /// 金額
        /// </summary>
        [DataMember]
        public decimal 金額 = 0;

        /// <summary>
        /// 受領日
        /// </summary>
        [DataMember]
        public string 受領日 = "";

        /// <summary>
        /// 受領番号
        /// </summary>
        [DataMember]
        public string 受領番号 = "";
        
        /// <summary>
        /// ステータス
        /// </summary>
        [DataMember]
        public string Status = "";

        /// <summary>
        /// ステータスの表示順序
        /// </summary>
        [DataMember]
        public int StatusOrder = (int)STATUS_ORDER.受領;
        enum STATUS_ORDER { 受領, 落成, 支払 };

        /// <summary>
        /// 取引先
        /// </summary>
        [DataMember]
        public string 取引先 = "";

        /// <summary>
        /// 事務担当者
        /// </summary>
        [DataMember]
        public string 事務担当者 = "";

        /// <summary>
        /// 受領
        /// </summary>
        [DataMember]
        public OdJry odjry = null;

        /// <summary>
        /// 落成 または 支払
        /// </summary>
        [DataMember]
        public OdShr odshr = null;

        #endregion

        public static List<概算計上一覧Row> SearchRecords(NBaseData.DAC.MsUser loginUser, OdJryFilter filter)
        {
            List<概算計上一覧Row> ret = new List<概算計上一覧Row>();
            List<OdJry> 受領s = null;
            List<OdShr> 落成s = null;
            List<OdShr> 支払s = null;


            if (filter.MiKeijyo == true && filter.KeijyoZumi == true)
            {
                支払s = OdShr.GetRecordsBy計上月(loginUser, filter);
            }
            else
            {
                受領s = OdJry.GetRecordsByFilter(loginUser, filter);
                落成s = OdShr.GetRecords落成済み未払い(loginUser, filter);
                支払s = OdShr.GetRecords未払い(loginUser, filter);
            }
            List<MsThiIraiSbt> 種別s = MsThiIraiSbt.GetRecords(loginUser);
            List<MsThiIraiShousai> 詳細種別s = MsThiIraiShousai.GetRecords(loginUser);
            List<MsCustomer> 取引先s = MsCustomer.GetRecords(loginUser);
            List<MsUser> ユーザs = MsUser.GetAllRecords(loginUser);

            if (受領s != null)
            {
                foreach (OdJry jry in 受領s)
                {
                    概算計上一覧Row row = new 概算計上一覧Row();

                    OdJryGaisan jryGaisan = OdJryGaisan.GetRecordByOdJryID(loginUser, jry.OdJryID);

                    row.MsVesselId = jry.OdThiMsVesselID;
                    row.船 = jry.OdThiVesselName;
                    var sbt = from p in 種別s
                              where p.MsThiIraiSbtID == jry.MsThiIraiSbtID
                              select p;
                    if (sbt != null && sbt.Count<MsThiIraiSbt>() > 0)
                    {
                        row.種別 = sbt.First<MsThiIraiSbt>().ThiIraiSbtName;
                    }
                    var shousai = from p in 詳細種別s
                                  where p.MsThiIraiShousaiID == jry.OdThi_MsThiIraiShousaiID
                                  select p;
                    if (shousai != null && shousai.Count<MsThiIraiShousai>() > 0)
                    {
                        row.詳細種別 = shousai.First<MsThiIraiShousai>().ThiIraiShousaiName;
                    }
                    row.件名 = jry.OdThiNaiyou;
                    //row.金額 = jry.Amount - jry.NebikiAmount; // +jry.Tax;
                    if (jryGaisan != null)
                    {
                        row.金額 = jryGaisan.Amount;
                    }
                    else
                    {
                        //row.金額 = jry.Amount - jry.NebikiAmount; // +jry.Tax;
                        row.金額 = jry.Amount - jry.NebikiAmount + jry.Carriage; // +jry.Tax;
                    }
                    row.受領日 = jry.JryDate.ToShortDateString();
                    row.受領番号 = jry.JryNo;
                    row.Status = "受領";
                    row.StatusOrder = (int)概算計上一覧Row.STATUS_ORDER.受領;
                    row.取引先 = jry.MsCustomerCustomerName;
                    var usr = from p in ユーザs
                              where p.MsUserID == jry.OdThi_JimTantouID
                              select p;
                    if (usr != null && usr.Count<MsUser>() > 0)
                    {
                        row.事務担当者 = usr.First<MsUser>().Sei + " " + usr.First<MsUser>().Mei;
                    }

                    row.odjry = jry;

                    if (row.金額 <= 0)
                        continue;

                    ret.Add(row);
                }
            }
            if (落成s != null)
            {
                foreach (OdShr shr in 落成s)
                {
                    概算計上一覧Row row = new 概算計上一覧Row();

                    row.MsVesselId = shr.OdThi_MsVesselID;
                    row.船 = shr.MsVessel_VesselName;
                    var sbt = from p in 種別s
                              where p.MsThiIraiSbtID == shr.OdThi_MsThiIraiSbtID
                              select p;
                    if (sbt != null && sbt.Count<MsThiIraiSbt>() > 0)
                    {
                        row.種別 = sbt.First<MsThiIraiSbt>().ThiIraiSbtName;
                    }
                    var shousai = from p in 詳細種別s
                                  where p.MsThiIraiShousaiID == shr.OdThi_MsThiIraiShousaiID
                                  select p;
                    if (shousai != null && shousai.Count<MsThiIraiShousai>() > 0)
                    {
                        row.詳細種別 = shousai.First<MsThiIraiShousai>().ThiIraiShousaiName;
                    }
                    row.件名 = shr.OdThiNaiyou;
                    //row.金額 = shr.Amount - shr.NebikiAmount; // +shr.Tax;
                    row.金額 = shr.Amount - shr.NebikiAmount + shr.Carriage; // +shr.Tax;
                    row.受領日 = shr.OdJry_JryDate.ToShortDateString();
                    row.受領番号 = shr.OdJry_JryNo;
                    row.Status = "落成";
                    row.StatusOrder = (int)概算計上一覧Row.STATUS_ORDER.落成;
                    var cstm = from p in 取引先s
                               where p.MsCustomerID == shr.OdMk_MsCustomerID
                               select p;
                    if (cstm != null && cstm.Count<MsCustomer>() > 0)
                    {
                        row.取引先 = cstm.First<MsCustomer>().CustomerName;
                    }
                    var usr = from p in ユーザs
                              where p.MsUserID == shr.OdThi_JimTantouID
                              select p;
                    if (usr != null && usr.Count<MsUser>() > 0)
                    {
                        row.事務担当者 = usr.First<MsUser>().Sei + " " + usr.First<MsUser>().Mei;
                    }

                    row.odshr = shr;

                    if (row.金額 <= 0)
                        continue;
                    ret.Add(row);
                }
            }
            if (支払s != null)
            {
                foreach (OdShr shr in 支払s)
                {
                    概算計上一覧Row row = new 概算計上一覧Row();

                    row.MsVesselId = shr.OdThi_MsVesselID;
                    row.船 = shr.MsVessel_VesselName;
                    var sbt = from p in 種別s
                              where p.MsThiIraiSbtID == shr.OdThi_MsThiIraiSbtID
                              select p;
                    if (sbt != null && sbt.Count<MsThiIraiSbt>() > 0)
                    {
                        row.種別 = sbt.First<MsThiIraiSbt>().ThiIraiSbtName;
                    }
                    var shousai = from p in 詳細種別s
                                  where p.MsThiIraiShousaiID == shr.OdThi_MsThiIraiShousaiID
                                  select p;
                    if (shousai != null && shousai.Count<MsThiIraiShousai>() > 0)
                    {
                        row.詳細種別 = shousai.First<MsThiIraiShousai>().ThiIraiShousaiName;
                    }
                    row.件名 = shr.OdThiNaiyou;
                    //row.金額 = shr.Amount - shr.NebikiAmount; // +shr.Tax;
                    row.金額 = shr.Amount - shr.NebikiAmount + shr.Carriage; // +shr.Tax;
                    row.受領日 = shr.OdJry_JryDate.ToShortDateString();
                    row.受領番号 = shr.OdJry_JryNo;
                    row.Status = "支払";
                    row.StatusOrder = (int)概算計上一覧Row.STATUS_ORDER.支払;
                    var cstm = from p in 取引先s
                               where p.MsCustomerID == shr.OdMk_MsCustomerID
                               select p;
                    if (cstm != null && cstm.Count<MsCustomer>() > 0)
                    {
                        row.取引先 = cstm.First<MsCustomer>().CustomerName;
                    }
                    var usr = from p in ユーザs
                              where p.MsUserID == shr.OdThi_JimTantouID
                              select p;
                    if (usr != null && usr.Count<MsUser>() > 0)
                    {
                        row.事務担当者 = usr.First<MsUser>().Sei + " " + usr.First<MsUser>().Mei;
                    }

                    row.odshr = shr;

                    if (row.金額 <= 0)
                        continue;
                    ret.Add(row);
                }
            }

            return ret;
        }
    }
}
