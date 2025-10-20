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
        bool BLC_手配依頼更新処理_新規(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdThi OdThi,
            List<NBaseData.DAC.OdThiItem> newOdThiItems,
            List<NBaseData.DAC.OdThiShousaiItem> newOdThiShousaiItems,
            List<NBaseData.DAC.OdAttachFile> newOdAttachFiles
            );

        [OperationContract]
        bool BLC_手配依頼更新処理_更新(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdThi OdThi,
            List<NBaseData.DAC.OdThiItem> newOdThiItems,
            List<NBaseData.DAC.OdThiItem> chgOdThiItems,
            List<NBaseData.DAC.OdThiItem> delOdThiItems,
            List<NBaseData.DAC.OdThiShousaiItem> newOdThiShousaiItems,
            List<NBaseData.DAC.OdThiShousaiItem> chgOdThiShousaiItems,
            List<NBaseData.DAC.OdThiShousaiItem> delOdThiShousaiItems,
            List<NBaseData.DAC.OdAttachFile> odAttachFiles
            );
    }
    public partial class Service
    {
        #region public bool BLC_手配依頼更新処理_新規(...)
        public bool BLC_手配依頼更新処理_新規(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdThi OdThi,
            List<NBaseData.DAC.OdThiItem> OdThiItems,
            List<NBaseData.DAC.OdThiShousaiItem> OdThiShousaiItems,
            List<NBaseData.DAC.OdAttachFile> newOdAttachFiles
            )
        {
            foreach (NBaseData.DAC.OdThiItem thiItem in OdThiItems)
            {
                thiItem.OdThiShousaiItems.Clear();
                foreach (NBaseData.DAC.OdThiShousaiItem thiShousaiItem in OdThiShousaiItems)
                {
                    if (thiShousaiItem.OdThiItemID == thiItem.OdThiItemID)
                    {
                        thiItem.OdThiShousaiItems.Add(thiShousaiItem);
                    }
                }
            }

            bool ret = false;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                ret = 手配依頼登録(dbConnect, loginUser, ref OdThi, OdThiItems, newOdAttachFiles);
                if (ret)
                {
                    if (OdThi.Status == (int)NBaseData.DAC.OdThi.STATUS.手配依頼済)
                    {
                        bool alarmRet = false;
                        alarmRet = NBaseData.BLC.発注アラーム処理.手配アラーム処理(dbConnect, loginUser, OdThi);
                    }
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

            return ret;
        }
        #endregion
        #region public bool BLC_手配依頼更新処理_更新(...)
        public bool BLC_手配依頼更新処理_更新(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdThi OdThi,
            List<NBaseData.DAC.OdThiItem> newOdThiItems,
            List<NBaseData.DAC.OdThiItem> chgOdThiItems,
            List<NBaseData.DAC.OdThiItem> delOdThiItems,
            List<NBaseData.DAC.OdThiShousaiItem> newOdThiShousaiItems,
            List<NBaseData.DAC.OdThiShousaiItem> chgOdThiShousaiItems,
            List<NBaseData.DAC.OdThiShousaiItem> delOdThiShousaiItems,
            List<NBaseData.DAC.OdAttachFile> odAttachFiles
            )
        {
            bool ret = false;

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    bool alarmRet = false;
                    if (OdThi.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        alarmRet = NBaseData.BLC.発注アラーム処理.手配アラーム削除(dbConnect, loginUser, OdThi.OdThiID);
                    }
                    else if (OdThi.Status == (int)NBaseData.DAC.OdThi.STATUS.手配依頼済)
                    {
                        if (OdThi.MsThiIraiStatusID == NBaseData.DAC.MsThiIraiStatus.ToId(NBaseData.DAC.MsThiIraiStatus.THI_IRAI_STATUS.未対応))
                        {
                            alarmRet = NBaseData.BLC.発注アラーム処理.手配アラーム処理(dbConnect, loginUser, OdThi);
                        }
                        else
                        {
                            alarmRet = NBaseData.BLC.発注アラーム処理.手配アラーム停止(dbConnect, loginUser, OdThi.OdThiID);
                        }
                    }

                    //if (OdThi.TehaiIraiNo.Length == (NBaseData.DAC.OdThi.NoLength手配依頼 - 1))
                    if (OdThi.TehaiIraiNo.Length == (NBaseData.DAC.OdThi.NoLength手配依頼 - 2))
                    {
                        OdThi.TehaiIraiNo = 手配依頼SetMaxNo(dbConnect, loginUser, OdThi.TehaiIraiNo);
                    }
                    ret = OdThi.UpdateRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        throw new Exception();
                    }
                    foreach (NBaseData.DAC.OdThiItem thiItem in newOdThiItems)
                    {
                        ret = thiItem.InsertRecord(dbConnect, loginUser);
                        if (ret == false)
                        {
                            throw new Exception();
                        }
                        if (thiItem.SaveDB == true)
                        {
                            ret = 小修理品目マスタ登録(dbConnect, loginUser, thiItem.VesselID, thiItem.ItemName, thiItem.RenewUserID, thiItem.RenewDate);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                        }
                    }
                    foreach (NBaseData.DAC.OdThiItem thiItem in chgOdThiItems)
                    {
                        ret = thiItem.UpdateRecord(dbConnect, loginUser);
                        if (ret == false)
                        {
                            throw new Exception();
                        }
                        if (thiItem.SaveDB == true)
                        {
                            ret = 小修理品目マスタ登録(dbConnect, loginUser, thiItem.VesselID, thiItem.ItemName, thiItem.RenewUserID, thiItem.RenewDate);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                        }
                    }
                    foreach (NBaseData.DAC.OdThiShousaiItem thiShousaiItem in newOdThiShousaiItems)
                    {
                        ret = thiShousaiItem.InsertRecord(dbConnect, loginUser);
                        if (ret == false)
                        {
                            throw new Exception();
                        }
                        if (thiShousaiItem.SaveDB == true)
                        {
                            ret = 小修理詳細品目マスタ登録(dbConnect, loginUser, thiShousaiItem.VesselID, thiShousaiItem.ShousaiItemName, thiShousaiItem.RenewUserID, thiShousaiItem.RenewDate);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                        }
                    }
                    foreach (NBaseData.DAC.OdThiShousaiItem thiShousaiItem in chgOdThiShousaiItems)
                    {
                        ret = thiShousaiItem.UpdateRecord(dbConnect, loginUser);
                        if (ret == false)
                        {
                            throw new Exception();
                        }
                        if (thiShousaiItem.SaveDB == true)
                        {
                            ret = 小修理詳細品目マスタ登録(dbConnect, loginUser, thiShousaiItem.VesselID, thiShousaiItem.ShousaiItemName, thiShousaiItem.RenewUserID, thiShousaiItem.RenewDate);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                        }
                    }
                    foreach (NBaseData.DAC.OdThiShousaiItem thiShousaiItem in delOdThiShousaiItems)
                    {
                        thiShousaiItem.CancelFlag = 1;
                        ret = thiShousaiItem.UpdateRecord(dbConnect, loginUser);
                        if (ret == false)
                        {
                            throw new Exception();
                        }
                    }
                    foreach (NBaseData.DAC.OdThiItem thiItem in delOdThiItems)
                    {
                        thiItem.CancelFlag = 1;
                        ret = thiItem.UpdateRecord(dbConnect, loginUser);
                        if (ret == false)
                        {
                            throw new Exception();
                        }
                    }
                    foreach (NBaseData.DAC.OdAttachFile attachFile in odAttachFiles)
                    {

                        NBaseCommon.LogFile.Write("", $"attachFile size [{attachFile.Data.Length.ToString()}]");


                        if (attachFile.Exists(dbConnect,loginUser))
                        {
                            ret = attachFile.UpdateRecord(dbConnect, loginUser);
                        }
                        else
                        {
                            ret = attachFile.InsertRecord(dbConnect, loginUser);
                        }
                        if (ret == false)
                        {
                            throw new Exception();
                        }
                    }
                }
                catch
                {
                    ret = false;
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

        #region private bool 手配依頼登録(...)
        private bool 手配依頼登録(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, ref NBaseData.DAC.OdThi OdThi, List<NBaseData.DAC.OdThiItem> OdThiItems, List<NBaseData.DAC.OdAttachFile> OdAttachFiles)
        {
            bool ret = false;
            //if (OdThi.TehaiIraiNo != null && OdThi.TehaiIraiNo.Length == (NBaseData.DAC.OdThi.NoLength手配依頼 - 1))
            if (OdThi.TehaiIraiNo != null && OdThi.TehaiIraiNo.Length == (NBaseData.DAC.OdThi.NoLength手配依頼 - 2))
            {
                OdThi.TehaiIraiNo = 手配依頼SetMaxNo(dbConnect, loginUser, OdThi.TehaiIraiNo);
            }
            ret = OdThi.InsertRecord(dbConnect, loginUser);
            if (ret == false)
            {
                return ret;
            }
            if (OdThiItems == null)
            {
                return ret;
            }
            foreach (NBaseData.DAC.OdThiItem thiItem in OdThiItems)
            {
                ret = thiItem.InsertRecord(dbConnect, loginUser);
                if (ret == false)
                {
                    break;
                }
                if (thiItem.SaveDB == true)
                {
                    ret = 小修理品目マスタ登録(dbConnect, loginUser, thiItem.VesselID, thiItem.ItemName, thiItem.RenewUserID, thiItem.RenewDate);
                    if (ret == false)
                    {
                        break;
                    }
                }

                foreach (NBaseData.DAC.OdThiShousaiItem thiShousaiItem in thiItem.OdThiShousaiItems)
                {
                    ret = thiShousaiItem.InsertRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        break;
                    }
                    if (thiShousaiItem.SaveDB == true)
                    {
                        ret = 小修理詳細品目マスタ登録(dbConnect, loginUser, thiShousaiItem.VesselID, thiShousaiItem.ShousaiItemName, thiShousaiItem.RenewUserID, thiShousaiItem.RenewDate);
                        if (ret == false)
                        {
                            break;
                        }
                    }
                }
                if (ret == false)
                {
                    break;
                }
            }
            if (OdAttachFiles != null)
            {
                foreach (NBaseData.DAC.OdAttachFile attachFile in OdAttachFiles)
                {
                    ret = attachFile.InsertRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        break;
                    }
                }
            }
            return ret;
        }
        #endregion
        #region private bool 手配依頼更新(...)
        private bool 手配依頼更新(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, ref NBaseData.DAC.OdThi OdThi, List<NBaseData.DAC.OdThiItem> OdThiItems)
        {
            bool ret = false;
            //if (OdThi.TehaiIraiNo.Length == (NBaseData.DAC.OdThi.NoLength手配依頼 - 1))
            if (OdThi.TehaiIraiNo.Length == (NBaseData.DAC.OdThi.NoLength手配依頼 - 2))
            {
                OdThi.TehaiIraiNo = 手配依頼SetMaxNo(dbConnect, loginUser, OdThi.TehaiIraiNo);
            }
            ret = OdThi.UpdateRecord(dbConnect, loginUser);
            if (ret == false)
            {
                return ret;
            }


            ret = NBaseData.DAC.OdThiItem.CancelByOdThiID(dbConnect, loginUser, OdThi.OdThiID);
            ret = NBaseData.DAC.OdThiShousaiItem.CancelByOdThiID(dbConnect, loginUser, OdThi.OdThiID);

            if (OdThiItems == null)
            {
                return ret;
            }
            foreach (NBaseData.DAC.OdThiItem thiItem in OdThiItems)
            {
                ret = thiItem.InsertRecord(dbConnect, loginUser);
                if (ret == false)
                {
                    break;
                }
                if (thiItem.SaveDB == true)
                {
                    ret = 小修理品目マスタ登録(dbConnect, loginUser, thiItem.VesselID, thiItem.ItemName, thiItem.RenewUserID, thiItem.RenewDate);
                    if (ret == false)
                    {
                        break;
                    }
                }

                foreach (NBaseData.DAC.OdThiShousaiItem thiShousaiItem in thiItem.OdThiShousaiItems)
                {
                    ret = thiShousaiItem.InsertRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        break;
                    }
                    if (thiShousaiItem.SaveDB == true)
                    {
                        ret = 小修理詳細品目マスタ登録(dbConnect, loginUser, thiShousaiItem.VesselID, thiShousaiItem.ShousaiItemName, thiShousaiItem.RenewUserID, thiShousaiItem.RenewDate);
                        if (ret == false)
                        {
                            break;
                        }
                    }
                }
                if (ret == false)
                {
                    break;
                }
            }
            return ret;
        }
        #endregion
    }
 }
