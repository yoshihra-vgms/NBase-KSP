using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ORMapping;
using System.ServiceModel;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_見積回答更新処理_新規(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdMk OdMk,
            List<NBaseData.DAC.OdMkItem> OdMkItems,
            List<NBaseData.DAC.OdMkShousaiItem> OdMkShousaiItems
            );

        [OperationContract]
        bool BLC_見積回答更新処理_更新(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdMk OdMk,
            List<NBaseData.DAC.OdMkItem> newOdMkItems,
            List<NBaseData.DAC.OdMkItem> chgOdMkItems,
            List<NBaseData.DAC.OdMkItem> delOdMkItems,
            List<NBaseData.DAC.OdMkShousaiItem> newOdMkShousaiItems,
            List<NBaseData.DAC.OdMkShousaiItem> chgOdMkShousaiItems,
            List<NBaseData.DAC.OdMkShousaiItem> delOdMkShousaiItems
            );
    }
    public partial class Service
    {
        #region public bool BLC_見積回答更新処理_新規(...)
        public bool BLC_見積回答更新処理_新規(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdMk OdMk,
            List<NBaseData.DAC.OdMkItem> OdMkItems,
            List<NBaseData.DAC.OdMkShousaiItem> OdMkShousaiItems
            )
        {
            foreach (NBaseData.DAC.OdMkItem mkItem in OdMkItems)
            {
                foreach (NBaseData.DAC.OdMkShousaiItem mkShousaiItem in OdMkShousaiItems)
                {
                    if (mkShousaiItem.OdMkItemID == mkItem.OdMkItemID)
                    {
                        mkItem.OdMkShousaiItems.Add(mkShousaiItem);
                    }
                }
            }
            using (DBConnect dbConnect = new DBConnect())
            {
                bool ret = false;

                dbConnect.BeginTransaction();

                ret = 見積回答登録(dbConnect, loginUser, ref OdMk, OdMkItems);
                if (ret)
                {
                    bool alarmRet = false;
                    alarmRet = NBaseData.BLC.発注アラーム処理.見積回答アラーム登録(dbConnect, loginUser, OdMk);
                }

                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }
            }

            return true;
        }
        #endregion
        #region public bool BLC_見積回答更新処理_更新(...)
        public bool BLC_見積回答更新処理_更新(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdMk OdMk,
            List<NBaseData.DAC.OdMkItem> newOdMkItems,
            List<NBaseData.DAC.OdMkItem> chgOdMkItems,
            List<NBaseData.DAC.OdMkItem> delOdMkItems,
            List<NBaseData.DAC.OdMkShousaiItem> newOdMkShousaiItems,
            List<NBaseData.DAC.OdMkShousaiItem> chgOdMkShousaiItems,
            List<NBaseData.DAC.OdMkShousaiItem> delOdMkShousaiItems
            )
        {
            bool ret = false;
            bool alarmRet = false;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    if (OdMk.MkNo.Length == (NBaseData.DAC.OdMk.NoLength見積回答 - 1))
                    {
                        OdMk.MkNo = 見積回答SetMaxNo(dbConnect, loginUser, OdMk.MkNo);
                    }
                    if (OdMk.HachuDate != DateTime.MinValue && (OdMk.Nendo == null || (OdMk.Nendo != null && OdMk.Nendo.Length == 0)))
                    {
                        OdMk.Nendo = NBaseUtil.DateTimeUtils.年度開始日(OdMk.HachuDate).Year.ToString();
                    }
                    ret = OdMk.UpdateRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        throw new Exception();
                    }
                    if (OdMk.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        alarmRet = NBaseData.BLC.発注アラーム処理.見積回答アラーム停止(dbConnect, loginUser, OdMk.OdMkID);
                    }
                    else if (OdMk.Status == (int)NBaseData.DAC.OdMk.STATUS.回答済み)
                    {
                        try
                        {
                            // 未回答→回答済み
                            alarmRet = NBaseData.BLC.発注アラーム処理.見積回答アラーム停止(dbConnect, loginUser, OdMk.OdMkID);
                        }
                        catch
                        {
                        }
                        if (alarmRet == false)
                        {
                            try
                            {
                                // 発注承認依頼中→回答済み
                                alarmRet = NBaseData.BLC.発注アラーム処理.発注承認アラーム停止(dbConnect, loginUser, OdMk.OdMkID);
                            }
                            catch
                            {
                            }
                        }
                    }
                    else if (OdMk.Status == (int)NBaseData.DAC.OdMk.STATUS.発注承認依頼中)
                    {
                        alarmRet = NBaseData.BLC.発注アラーム処理.発注承認アラーム登録(dbConnect, loginUser, OdMk);
                    }
                    else if (OdMk.Status == (int)NBaseData.DAC.OdMk.STATUS.発注承認済み)
                    {
                        alarmRet = NBaseData.BLC.発注アラーム処理.発注承認アラーム停止(dbConnect, loginUser, OdMk.OdMkID);
                        alarmRet = NBaseData.BLC.発注アラーム処理.発注アラーム登録(dbConnect, loginUser, OdMk);
                    }
                    if (ret == false)
                    {
                        throw new Exception();
                    }


                    if (newOdMkItems != null)
                    {
                        foreach (NBaseData.DAC.OdMkItem mkItem in newOdMkItems)
                        {
                            ret = mkItem.InsertRecord(dbConnect, loginUser);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                            if (mkItem.SaveDB == true)
                            {
                                ret = 小修理品目マスタ登録(dbConnect, loginUser, mkItem.VesselID, mkItem.ItemName, mkItem.RenewUserID, mkItem.RenewDate);
                                if (ret == false)
                                {
                                    throw new Exception();
                                }
                            }
                        }
                    }
                    if (chgOdMkItems != null)
                    {
                        foreach (NBaseData.DAC.OdMkItem mkItem in chgOdMkItems)
                        {
                            ret = mkItem.UpdateRecord(dbConnect, loginUser);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                            if (mkItem.SaveDB == true)
                            {
                                ret = 小修理品目マスタ登録(dbConnect, loginUser, mkItem.VesselID, mkItem.ItemName, mkItem.RenewUserID, mkItem.RenewDate);
                                if (ret == false)
                                {
                                    throw new Exception();
                                }
                            }
                        }
                    }
                    if (newOdMkShousaiItems != null)
                    {
                        foreach (NBaseData.DAC.OdMkShousaiItem mkShousaiItem in newOdMkShousaiItems)
                        {
                            ret = mkShousaiItem.InsertRecord(dbConnect, loginUser);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                            if (mkShousaiItem.SaveDB == true)
                            {
                                ret = 小修理詳細品目マスタ登録(dbConnect, loginUser, mkShousaiItem.VesselID, mkShousaiItem.ShousaiItemName, mkShousaiItem.RenewUserID, mkShousaiItem.RenewDate);
                                if (ret == false)
                                {
                                    throw new Exception();
                                }
                            }
                        }
                    }
                    if (chgOdMkShousaiItems != null)
                    {
                        foreach (NBaseData.DAC.OdMkShousaiItem mkShousaiItem in chgOdMkShousaiItems)
                        {
                            ret = mkShousaiItem.UpdateRecord(dbConnect, loginUser);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                            if (mkShousaiItem.SaveDB == true)
                            {
                                ret = 小修理詳細品目マスタ登録(dbConnect, loginUser, mkShousaiItem.VesselID, mkShousaiItem.ShousaiItemName, mkShousaiItem.RenewUserID, mkShousaiItem.RenewDate);
                                if (ret == false)
                                {
                                    throw new Exception();
                                }
                            }
                        }
                    }
                    if (delOdMkShousaiItems != null)
                    {
                        foreach (NBaseData.DAC.OdMkShousaiItem mkShousaiItem in delOdMkShousaiItems)
                        {
                            mkShousaiItem.CancelFlag = 1;
                            ret = mkShousaiItem.UpdateRecord(dbConnect, loginUser);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                        }
                    }
                    if (delOdMkItems != null)
                    {
                        foreach (NBaseData.DAC.OdMkItem mkItem in delOdMkItems)
                        {
                            mkItem.CancelFlag = 1;
                            ret = mkItem.UpdateRecord(dbConnect, loginUser);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                        }
                    }

                    ret = NBaseData.BLC.手配依頼更新処理.発注済(dbConnect, loginUser, OdMk.Status, OdMk.OdThiID);
                }
                catch
                {
                }
                if (ret)
                {
                    dbConnect.Commit();
                }
                else
                {
                    dbConnect.RollBack();
                }
            }

            return true;
        }
        #endregion


        #region private bool 見積回答登録(...)
        private bool 見積回答登録(
            DBConnect dbConnect,
            NBaseData.DAC.MsUser loginUser,
            ref NBaseData.DAC.OdMk OdMk,
            List<NBaseData.DAC.OdMkItem> OdMkItems
            )
        {
            bool ret = false;
            try
            {
                if (OdMk.MkNo != null && OdMk.MkNo.Length == (NBaseData.DAC.OdMk.NoLength見積回答 - 1))
                {
                    OdMk.MkNo = 見積回答SetMaxNo(dbConnect, loginUser, OdMk.MkNo);
                }
                ret = OdMk.InsertRecord(dbConnect, loginUser);
                if (ret == false)
                {
                    throw new Exception();
                }
                if (OdMkItems != null)
                {
                    foreach (NBaseData.DAC.OdMkItem mkItem in OdMkItems)
                    {
                        ret = mkItem.InsertRecord(dbConnect, loginUser);
                        if (ret == false)
                        {
                            throw new Exception();
                        }
                        foreach (NBaseData.DAC.OdMkShousaiItem mkShousaiItem in mkItem.OdMkShousaiItems)
                        {
                            ret = mkShousaiItem.InsertRecord(dbConnect, loginUser);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                        }
                    }
                }


                // 顧客担当者情報の登録
                NBaseData.DAC.MsCustomerTantou customerTantou = null;
                List<NBaseData.DAC.MsCustomerTantou> customerTantous = NBaseData.DAC.MsCustomerTantou.GetRecordsByCustomerIDAndName(dbConnect, loginUser, OdMk.MsCustomerID, OdMk.Tantousha);
                if (customerTantous.Count == 0)
                {
                    customerTantou = new NBaseData.DAC.MsCustomerTantou();
                    customerTantou.MsCustomerID = OdMk.MsCustomerID;
                    customerTantou.Name = OdMk.Tantousha;
                    customerTantou.MailAddress = OdMk.TantouMailAddress;
                    customerTantou.RenewDate = OdMk.RenewDate;
                    customerTantou.RenewUserID = OdMk.RenewUserID;

                    customerTantou.Tel = OdMk.Tel;
                    customerTantou.Fax = OdMk.Fax;

                    ret = customerTantou.InsertRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    customerTantou = customerTantous[0];
                    //if (customerTantou.MailAddress != OdMk.TantouMailAddress)
                    if (customerTantou.MailAddress != OdMk.TantouMailAddress
                        || customerTantou.Tel != OdMk.Tel
                        || customerTantou.Fax != OdMk.Fax)
                   {
                        customerTantou.MailAddress = OdMk.TantouMailAddress;
                        customerTantou.RenewDate = OdMk.RenewDate;
                        customerTantou.RenewUserID = OdMk.RenewUserID;

                        customerTantou.Tel = OdMk.Tel;
                        customerTantou.Fax = OdMk.Fax;

                        ret = customerTantou.UpdateRecord(dbConnect, loginUser);
                        if (ret == false)
                        {
                            throw new Exception();
                        }
                    }
                    //if (customerTantou.MailAddress != OdMk.TantouMailAddress)
                    //{
                    //    customerTantou.MailAddress = OdMk.TantouMailAddress;
                    //}
                    //customerTantou.RenewDate = OdMk.RenewDate;
                    //customerTantou.RenewUserID = OdMk.RenewUserID;

                    //customerTantou.Tel = OdMk.Tel;
                    //customerTantou.Fax = OdMk.Fax;

                    //ret = customerTantou.UpdateRecord(dbConnect, loginUser);
                    //if (ret == false)
                    //{
                    //    throw new Exception();
                    //}
                }
            }
            catch(Exception e)
            {
                ret = false;
            }
            return ret;
        }
        #endregion
    }
 }
