using System;
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
        bool BLC_受領更新処理_新規(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdJry OdJry,
            List<NBaseData.DAC.OdJryItem> OdJryItems,
            List<NBaseData.DAC.OdJryShousaiItem> OdJryShousaiItems
            );

        [OperationContract]
        bool BLC_受領更新処理_更新(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdJry OdJry,
            List<NBaseData.DAC.OdJryItem> newOdJryItems,
            List<NBaseData.DAC.OdJryItem> chgOdJryItems,
            List<NBaseData.DAC.OdJryItem> delOdJryItems,
            List<NBaseData.DAC.OdJryShousaiItem> newOdJryShousaiItems,
            List<NBaseData.DAC.OdJryShousaiItem> chgOdJryShousaiItems,
            List<NBaseData.DAC.OdJryShousaiItem> delOdJryShousaiItems
            );
    }
    public partial class Service
    {
        #region public bool BLC_受領更新処理_新規(...)
        public bool BLC_受領更新処理_新規(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdJry OdJry,
            List<NBaseData.DAC.OdJryItem> OdJryItems,
            List<NBaseData.DAC.OdJryShousaiItem> OdJryShousaiItems
            )
        {
            foreach (NBaseData.DAC.OdJryItem jryItem in OdJryItems)
            {
                foreach (NBaseData.DAC.OdJryShousaiItem jryShousaiItem in OdJryShousaiItems)
                {
                    if (jryShousaiItem.OdJryItemID == jryItem.OdJryItemID)
                    {
                        jryItem.OdJryShousaiItems.Add(jryShousaiItem);
                    }
                }
            }
            bool ret = false;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                ret = 受領登録(dbConnect, loginUser, ref OdJry, OdJryItems);
                if (ret)
                {
                    bool alarmRet = false;
                    alarmRet = NBaseData.BLC.発注アラーム処理.発注アラーム停止(dbConnect, loginUser, OdJry.OdMkID);
                    alarmRet = NBaseData.BLC.発注アラーム処理.受領アラーム登録(dbConnect, loginUser, OdJry);
                }
                if (ret == true)
                {
                    ret = NBaseData.BLC.事務所更新情報処理.発注登録(dbConnect, loginUser, OdJry);
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
        #region public bool BLC_受領更新処理_更新(...)
        public bool BLC_受領更新処理_更新(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdJry OdJry,
            List<NBaseData.DAC.OdJryItem> newOdJryItems,
            List<NBaseData.DAC.OdJryItem> chgOdJryItems,
            List<NBaseData.DAC.OdJryItem> delOdJryItems,
            List<NBaseData.DAC.OdJryShousaiItem> newOdJryShousaiItems,
            List<NBaseData.DAC.OdJryShousaiItem> chgOdJryShousaiItems,
            List<NBaseData.DAC.OdJryShousaiItem> delOdJryShousaiItems
            )
        {
            bool ret = false;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    if (OdJry.JryNo.Length == (NBaseData.DAC.OdJry.NoLength受領 - 1))
                    {
                        OdJry.JryNo = 受領SetMaxNo(dbConnect, loginUser, OdJry.JryNo);
                    }
                    ret = OdJry.UpdateRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        throw new Exception();
                    }
                    if (OdJry.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        ret = 受領取消しにともなうロールバック(dbConnect, loginUser, OdJry);
                        if (ret)
                        {
                            bool alarmRet = false;
                            alarmRet = NBaseData.BLC.発注アラーム処理.受領アラーム削除(dbConnect, loginUser, OdJry.OdJryID);
                        }
                    }
                    // 2011.05 Add 9Lines
                    else if (OdJry.Status == (int)NBaseData.DAC.OdJry.STATUS.未受領)
                    {
                        ret = BLC_手配状況変更_受領解除(dbConnect, loginUser, OdJry.OdJryID);
                        if (ret)
                        {
                            bool alarmRet = false;
                            alarmRet = NBaseData.BLC.発注アラーム処理.支払作成アラーム削除(dbConnect, loginUser, OdJry.OdJryID);
                        }
                    }
                    else
                    {
                        if (OdJry.Status == (int)NBaseData.DAC.OdJry.STATUS.受領承認済み)
                        {
                            bool alarmRet = false;
                            alarmRet = NBaseData.BLC.発注アラーム処理.受領アラーム停止(dbConnect, loginUser, OdJry.OdJryID);
                            alarmRet = NBaseData.BLC.発注アラーム処理.支払作成アラーム登録(dbConnect, loginUser, OdJry);
                            if (ret == true)
                            {
                                ret = NBaseData.BLC.事務所更新情報処理.受領登録(dbConnect, loginUser, OdJry);
                            }
                        }

                    }
                    if (ret == false)
                    {
                        throw new Exception();
                    }

                    if (newOdJryItems != null)
                    {
                        foreach (NBaseData.DAC.OdJryItem jryItem in newOdJryItems)
                        {
                            ret = jryItem.InsertRecord(dbConnect, loginUser);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                            if (jryItem.SaveDB == true)
                            {
                                ret = 小修理品目マスタ登録(dbConnect, loginUser, jryItem.VesselID, jryItem.ItemName, jryItem.RenewUserID, jryItem.RenewDate);
                                if (ret == false)
                                {
                                    throw new Exception();
                                }
                            }
                        }
                    }
                    if (chgOdJryItems != null)
                    {
                        foreach (NBaseData.DAC.OdJryItem jryItem in chgOdJryItems)
                        {
                            ret = jryItem.UpdateRecord(dbConnect, loginUser);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                            if (jryItem.SaveDB == true)
                            {
                                ret = 小修理品目マスタ登録(dbConnect, loginUser, jryItem.VesselID, jryItem.ItemName, jryItem.RenewUserID, jryItem.RenewDate);
                                if (ret == false)
                                {
                                    throw new Exception();
                                }
                            }
                        }
                    }
                    if (newOdJryShousaiItems != null)
                    {
                        foreach (NBaseData.DAC.OdJryShousaiItem jryShousaiItem in newOdJryShousaiItems)
                        {
                            ret = jryShousaiItem.InsertRecord(dbConnect, loginUser);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                            if (jryShousaiItem.SaveDB == true)
                            {
                                ret = 小修理詳細品目マスタ登録(dbConnect, loginUser, jryShousaiItem.VesselID, jryShousaiItem.ShousaiItemName, jryShousaiItem.RenewUserID, jryShousaiItem.RenewDate);
                                if (ret == false)
                                {
                                    throw new Exception();
                                }
                            }
                        }
                    }
                    if (chgOdJryShousaiItems != null)
                    {
                        foreach (NBaseData.DAC.OdJryShousaiItem jryShousaiItem in chgOdJryShousaiItems)
                        {
                            ret = jryShousaiItem.UpdateRecord(dbConnect, loginUser);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                            if (jryShousaiItem.SaveDB == true)
                            {
                                ret = 小修理詳細品目マスタ登録(dbConnect, loginUser, jryShousaiItem.VesselID, jryShousaiItem.ShousaiItemName, jryShousaiItem.RenewUserID, jryShousaiItem.RenewDate);
                                if (ret == false)
                                {
                                    throw new Exception();
                                }
                            }
                        }
                    }
                    if (delOdJryShousaiItems != null)
                    {
                        foreach (NBaseData.DAC.OdJryShousaiItem jryShousaiItem in delOdJryShousaiItems)
                        {
                            jryShousaiItem.CancelFlag = 1;
                            ret = jryShousaiItem.UpdateRecord(dbConnect, loginUser);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                        }
                    }
                    if (delOdJryItems != null)
                    {
                        foreach (NBaseData.DAC.OdJryItem jryItem in delOdJryItems)
                        {
                            jryItem.CancelFlag = 1;
                            ret = jryItem.UpdateRecord(dbConnect, loginUser);
                            if (ret == false)
                            {
                                throw new Exception();
                            }
                        }
                    }
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

        #region private bool 受領登録(...)
        private bool 受領登録(
           DBConnect dbConnect,
           NBaseData.DAC.MsUser loginUser,
           ref NBaseData.DAC.OdJry OdJry,
           List<NBaseData.DAC.OdJryItem> OdJryItems
           )
        {
            bool ret = false;
            if (OdJry.JryNo.Length == (NBaseData.DAC.OdJry.NoLength受領 - 1))
            {
                OdJry.JryNo = 受領SetMaxNo(dbConnect, loginUser, OdJry.JryNo);
            }
            ret = OdJry.InsertRecord(dbConnect, loginUser);
            if (OdJryItems != null)
            {
                foreach (NBaseData.DAC.OdJryItem jryItem in OdJryItems)
                {
                    ret = jryItem.InsertRecord(dbConnect, loginUser);

                    foreach (NBaseData.DAC.OdJryShousaiItem jryShousaiItem in jryItem.OdJryShousaiItems)
                    {
                        ret = jryShousaiItem.InsertRecord(dbConnect, loginUser);
                    }
                }
            }
            return ret;
        }
        #endregion

        #region private bool 受領取消しにともなうロールバック(...)
        private bool 受領取消しにともなうロールバック(
                        DBConnect dbConnect,
                        NBaseData.DAC.MsUser loginUser,
                        NBaseData.DAC.OdJry OdJry)
        {
            bool ret = false;

            NBaseData.DAC.OdMk 見積回答 = NBaseData.DAC.OdMk.GetRecord(dbConnect, loginUser, OdJry.OdMkID);
            if (見積回答 == null)
            {
                // 見積回答がないのはおかしい
                return false;
            }
            NBaseData.DAC.OdThi 手配依頼 = NBaseData.DAC.OdThi.GetRecord(dbConnect, loginUser, 見積回答.OdThiID);
            if (手配依頼 == null)
            {
                // 手配依頼がないのはおかしい
                return false;
            }

            int jryCount = 0;
            List<NBaseData.DAC.OdJry> 兄弟の受領データ = NBaseData.DAC.OdJry.GetRecordsByOdMkId(dbConnect, loginUser, OdJry.OdMkID);         
            foreach (NBaseData.DAC.OdJry jry in 兄弟の受領データ)
            {
                if (jry.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    continue;
                if (jry.OdJryID == OdJry.OdJryID)
                    continue;
                jryCount++;
            }
            if (jryCount == 0)
            {
                // 兄弟の受領がない場合、１つ親の見積回答を更新する
                if (見積回答.MkNo.Substring(0, 7) == "Enabled")
                {
                    見積回答.CancelFlag = NBaseCommon.Common.CancelFlag_キャンセル;
                }
                else
                {
                    見積回答.Status = 見積回答.OdStatusValue.Values[(int)NBaseData.DAC.OdMk.STATUS.発注承認済み].Value;
                }
                ret = 見積回答.UpdateRecord(dbConnect, loginUser);
                if (ret == false)
                {
                    return false;
                }

                if (見積回答.MkNo.Substring(0, 7) == "Enabled")
                {
                    //
                    // 「新規発注」により生成された受領の取消しの場合。
                    //
                    NBaseData.DAC.OdMm 見積依頼 = NBaseData.DAC.OdMm.GetRecord(dbConnect, loginUser, 見積回答.OdMmID);
                    if (見積依頼 == null)
                    {
                        // 見積依頼がないのはおかしい
                        return false;
                    }
                    見積依頼.CancelFlag = NBaseCommon.Common.CancelFlag_キャンセル;
                    ret = 見積依頼.UpdateRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        return false;
                    }
                    手配依頼.CancelFlag = NBaseCommon.Common.CancelFlag_キャンセル;
                    ret = 手配依頼.UpdateRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        return false;
                    }
                }
            }
            else
            {
                // 手配依頼ステータスを更新する
                ret = BLC_手配状況変更_受領の取消し(dbConnect, loginUser, OdJry.OdJryID);
            }

            return ret;
        }
        #endregion



    }
}
