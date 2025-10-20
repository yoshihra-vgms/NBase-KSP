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
        bool BLC_支払更新処理_新規(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdShr OdShr,
            List<NBaseData.DAC.OdShrItem> OdShrItems,
            List<NBaseData.DAC.OdShrShousaiItem> OdShrShousaiItems
            );

        [OperationContract]
        bool BLC_支払更新処理_更新(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdShr OdShr,
            List<NBaseData.DAC.OdShrItem> newOdShrItems,
            List<NBaseData.DAC.OdShrItem> chgOdShrItems,
            List<NBaseData.DAC.OdShrItem> delOdShrItems,
            List<NBaseData.DAC.OdShrShousaiItem> newOdShrShousaiItems,
            List<NBaseData.DAC.OdShrShousaiItem> chgOdShrShousaiItems,
            List<NBaseData.DAC.OdShrShousaiItem> delOdShrShousaiItems
            );

        [OperationContract]
        bool BLC_支払更新処理_落成(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdShr OdShr_R,
            NBaseData.DAC.OdShr OdShr_S,
            List<NBaseData.DAC.OdShrItem> OdShrItems,
            List<NBaseData.DAC.OdShrShousaiItem> OdShrShousaiItems
            );
    }
    public partial class Service
    {
        #region public bool BLC_支払更新処理_新規(...)
        public bool BLC_支払更新処理_新規(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdShr OdShr,
            List<NBaseData.DAC.OdShrItem> OdShrItems,
            List<NBaseData.DAC.OdShrShousaiItem> OdShrShousaiItems
            )
        {
            bool ret = false;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                ret = 支払登録(dbConnect, loginUser, OdShr, OdShrItems, OdShrShousaiItems);
                if (ret)
                {
                    bool alarmRet = false;
                    alarmRet = NBaseData.BLC.発注アラーム処理.支払作成アラーム停止(dbConnect, loginUser, OdShr.OdJryID);
                    if (OdShr.Sbt == (int)NBaseData.DAC.OdShr.SBT.支払)
                    {
                        alarmRet = NBaseData.BLC.発注アラーム処理.支払依頼アラーム登録(dbConnect, loginUser, OdShr);
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
        #region public bool BLC_支払更新処理_更新(...)
        public bool BLC_支払更新処理_更新(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdShr OdShr,
            List<NBaseData.DAC.OdShrItem> newOdShrItems,
            List<NBaseData.DAC.OdShrItem> chgOdShrItems,
            List<NBaseData.DAC.OdShrItem> delOdShrItems,
            List<NBaseData.DAC.OdShrShousaiItem> newOdShrShousaiItems,
            List<NBaseData.DAC.OdShrShousaiItem> chgOdShrShousaiItems,
            List<NBaseData.DAC.OdShrShousaiItem> delOdShrShousaiItems
            )
        {
            if (OdShr.CancelFlag == 1)
            {
                return BLC_支払更新処理_Inner取消(loginUser, OdShr);
            }
            else
            {
                return BLC_支払更新処理_Inner更新(loginUser, OdShr, newOdShrItems, chgOdShrItems, delOdShrItems, newOdShrShousaiItems, chgOdShrShousaiItems, delOdShrShousaiItems);
            }
        }
        #endregion
        #region public bool BLC_支払更新処理_Inner更新(...)
        public bool BLC_支払更新処理_Inner更新(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdShr OdShr,
            List<NBaseData.DAC.OdShrItem> newOdShrItems,
            List<NBaseData.DAC.OdShrItem> chgOdShrItems,
            List<NBaseData.DAC.OdShrItem> delOdShrItems,
            List<NBaseData.DAC.OdShrShousaiItem> newOdShrShousaiItems,
            List<NBaseData.DAC.OdShrShousaiItem> chgOdShrShousaiItems,
            List<NBaseData.DAC.OdShrShousaiItem> delOdShrShousaiItems
            )
        {
            bool ret = false;
            //bool 支払連携 = false;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                #region 支払いデータの更新処理

                if (newOdShrItems != null)
                {
                    foreach (NBaseData.DAC.OdShrItem shrItem in newOdShrItems)
                    {
                        ret = shrItem.InsertRecord(dbConnect, loginUser);
                        if (shrItem.SaveDB == true)
                        {
                            ret = 小修理品目マスタ登録(dbConnect, loginUser, shrItem.VesselID, shrItem.ItemName, shrItem.RenewUserID, shrItem.RenewDate);
                        }
                    }
                }
                if (chgOdShrItems != null)
                {
                    foreach (NBaseData.DAC.OdShrItem shrItem in chgOdShrItems)
                    {
                        ret = shrItem.UpdateRecord(dbConnect, loginUser);
                        if (shrItem.SaveDB == true)
                        {
                            ret = 小修理品目マスタ登録(dbConnect, loginUser, shrItem.VesselID, shrItem.ItemName, shrItem.RenewUserID, shrItem.RenewDate);
                        }
                    }
                }
                if (newOdShrShousaiItems != null)
                {
                    foreach (NBaseData.DAC.OdShrShousaiItem shrShousaiItem in newOdShrShousaiItems)
                    {
                        ret = shrShousaiItem.InsertRecord(dbConnect, loginUser);
                        if (shrShousaiItem.SaveDB == true)
                        {
                            ret = 小修理詳細品目マスタ登録(dbConnect, loginUser, shrShousaiItem.VesselID, shrShousaiItem.ShousaiItemName, shrShousaiItem.RenewUserID, shrShousaiItem.RenewDate);
                        }
                    }
                }
                if (chgOdShrShousaiItems != null)
                {
                    foreach (NBaseData.DAC.OdShrShousaiItem shrShousaiItem in chgOdShrShousaiItems)
                    {
                        ret = shrShousaiItem.UpdateRecord(dbConnect, loginUser);
                        if (shrShousaiItem.SaveDB == true)
                        {
                            ret = 小修理詳細品目マスタ登録(dbConnect, loginUser, shrShousaiItem.VesselID, shrShousaiItem.ShousaiItemName, shrShousaiItem.RenewUserID, shrShousaiItem.RenewDate);
                        }
                    }
                }
                if (delOdShrShousaiItems != null)
                {
                    foreach (NBaseData.DAC.OdShrShousaiItem shrShousaiItem in delOdShrShousaiItems)
                    {
                        shrShousaiItem.CancelFlag = 1;
                        ret = shrShousaiItem.UpdateRecord(dbConnect, loginUser);
                    }
                }
                if (delOdShrItems != null)
                {
                    foreach (NBaseData.DAC.OdShrItem shrItem in delOdShrItems)
                    {
                        shrItem.CancelFlag = 1;
                        ret = shrItem.UpdateRecord(dbConnect, loginUser);
                    }
                }

                if (OdShr.ShrNo.Length == (NBaseData.DAC.OdShr.NoLength支払 - 1))
                {
                    OdShr.ShrNo = 支払SetMaxNo(dbConnect, loginUser, OdShr.ShrNo);
                }
                ret = OdShr.UpdateRecord(dbConnect, loginUser);

                #endregion

                if (ret)
                {
                    OdShr = NBaseData.DAC.OdShr.GetRecord(dbConnect, loginUser, OdShr.OdShrID);
                    dbConnect.Commit();


                    if (OdShr.Status == OdShr.OdStatusValue.Values[(int)NBaseData.DAC.OdShr.STATUS.支払済].Value)
                    {
                        NBaseData.BLC.手配依頼のステータスを完了にする 完了Logic = new NBaseData.BLC.手配依頼のステータスを完了にする();
                        List<NBaseData.DAC.OdShr> 支払s = new List<NBaseData.DAC.OdShr>();
                        支払s.Add(OdShr);
                        完了Logic.Kick(dbConnect, loginUser, 支払s, NBaseCommon.Common.Is発注承認ON);
                    }

                }
                else
                {
                    dbConnect.RollBack();
                }
            }
            //if (ret && OdShr != null)
            //{
            //    if (OdShr.Status == OdShr.OdStatusValue.Values[(int)NBaseData.DAC.OdShr.STATUS.支払依頼済み].Value)
            //    {
            //        // BLC_基幹システム連携書き込み処理_支払連携のなかで
            //        // ストアドプロシージャをコールするので、Commit or RollBack します
            //        string SyoriStatus = BLC_基幹システム連携書き込み処理_支払連携(loginUser, OdShr.OdShrID);
            //        int IntSyoriStatus = int.Parse(SyoriStatus);
            //        if (IntSyoriStatus == (int)NBaseData.DAC.Tajsinseiif.SYORI_STATUS.取込済み)
            //        {
            //            OdShr.Status = OdShr.OdStatusValue.Values[(int)NBaseData.DAC.OdShr.STATUS.支払依頼基幹連携済み].Value;
            //        }
            //        else
            //        {
            //            // 2009.11.18:aki 基幹に取り込めないときはエラーとする
            //            // 基幹で、取込ができていない場合、支払のステータスは、戻す
            //            //OdShr.Status = OdShr.OdStatusValue.Values[(int)NBaseData.DAC.OdShr.STATUS.支払作成済み].Value;
            //        }
            //        OdShr.SyoriStatus = SyoriStatus;

            //        using (DBConnect dbConnect = new DBConnect())
            //        {
            //            ret = OdShr.UpdateRecord(dbConnect, loginUser);

            //            // 2009.11.18:aki 基幹に取り込めないときは、支払いのコピーを作成する
            //            if (IntSyoriStatus != (int)NBaseData.DAC.Tajsinseiif.SYORI_STATUS.取込済み)
            //            {
            //                支払いデータを再登録(dbConnect, loginUser, OdShr);
            //            }
            //            else
            //            {
            //                bool alarmRet = false;
            //                alarmRet = NBaseData.BLC.発注アラーム処理.支払依頼アラーム停止(dbConnect, loginUser, OdShr.OdShrID);
            //            }

            //        }
            //    }
            //}
            return ret;
        }
        #endregion
        #region public bool BLC_支払更新処理_Inner取消(...)
        public bool BLC_支払更新処理_Inner取消(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdShr OdShr)
        {
            bool ret = false;
            //bool 支払連携 = false;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                ret = OdShr.UpdateRecord(dbConnect, loginUser);
                if (ret)
                {
                    // 支払合算ヘッダがある場合、ステータスを戻す
                    NBaseData.DAC.OdShrGassanHead odShrGassanHead = NBaseData.DAC.OdShrGassanHead.GetRecordByOdShrId(dbConnect, loginUser, OdShr.OdShrID);
                    if (odShrGassanHead != null)
                    {
                        odShrGassanHead.Status = (int)NBaseData.DAC.OdShrGassanHead.StatusEnum.支払未作成;
                        ret = odShrGassanHead.UpdateRecord(dbConnect, loginUser);
                    }
                    if (ret)
                    {
                        bool alarmRet = false;
                        alarmRet = NBaseData.BLC.発注アラーム処理.支払依頼アラーム削除(dbConnect, loginUser, OdShr.OdShrID);
                    }
                }

                if (ret)
                {
                    OdShr = NBaseData.DAC.OdShr.GetRecord(dbConnect, loginUser, OdShr.OdShrID);
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

        #region public bool BLC_支払更新処理_落成(....)
        public bool BLC_支払更新処理_落成(
           NBaseData.DAC.MsUser loginUser,
           NBaseData.DAC.OdShr OdShr_R,
           NBaseData.DAC.OdShr OdShr_S,
           List<NBaseData.DAC.OdShrItem> OdShrItems,
           List<NBaseData.DAC.OdShrShousaiItem> OdShrShousaiItems
           )
        {
            bool ret = false;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                // 落成情報を更新する
                ret = OdShr_R.UpdateRecord(dbConnect, loginUser);

                if (ret)
                {
                    // 支払情報を登録する
                    ret = 支払登録(dbConnect, loginUser, OdShr_S, OdShrItems, OdShrShousaiItems);
                    if (ret)
                    {
                        bool alarmRet = false;
                        alarmRet = NBaseData.BLC.発注アラーム処理.支払依頼アラーム登録(dbConnect, loginUser, OdShr_S);
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


        #region private bool 支払登録(....)
        private bool 支払登録(
            DBConnect dbConnect,
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdShr OdShr,
            List<NBaseData.DAC.OdShrItem> OdShrItems,
            List<NBaseData.DAC.OdShrShousaiItem> OdShrShousaiItems
           )
        {
            bool ret = false;
            if (OdShr.ShrNo.Length == (NBaseData.DAC.OdShr.NoLength支払 - 1))
            {
                OdShr.ShrNo = 支払SetMaxNo(dbConnect, loginUser, OdShr.ShrNo);
            }
            ret = OdShr.InsertRecord(dbConnect, loginUser);
            if (ret)
            {
                foreach (NBaseData.DAC.OdShrItem shrItem in OdShrItems)
                {
                    ret = shrItem.InsertRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        break;
                    }
                }
                if (ret)
                {
                    foreach (NBaseData.DAC.OdShrShousaiItem shrShousaiItem in OdShrShousaiItems)
                    {
                        ret = shrShousaiItem.InsertRecord(dbConnect, loginUser);
                        if (ret == false)
                        {
                            break;
                        }
                    }
                }
            }
            return ret;
        }
        #endregion

        private void 支払いデータを再登録(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, NBaseData.DAC.OdShr odShr)
        {
            // 現在、登録されている品目、詳細品目を取得
            List<NBaseData.DAC.OdShrItem> 再登録OdShrItems = NBaseData.DAC.OdShrItem.GetRecordsByOdShrID(dbConnect, loginUser, odShr.OdShrID);
            List<NBaseData.DAC.OdShrShousaiItem> 再登録OdShrShousaiItems = NBaseData.DAC.OdShrShousaiItem.GetRecordsByOdShrID(dbConnect, loginUser, odShr.OdShrID);

            // コピーを登録する
            NBaseData.DAC.OdShr 再登録OdShr = odShr.Clone();
            再登録OdShr.OdShrID = 新規ID();
            再登録OdShr.ShrNo = 支払SetMaxNo(dbConnect, loginUser, odShr.ShrNo.Substring(0, NBaseData.DAC.OdShr.NoLength支払 - 1));
            再登録OdShr.Status = (int)NBaseData.DAC.OdShr.STATUS.支払作成済み;
            再登録OdShr.SyoriStatus = "--";
            再登録OdShr.RenewDate = DateTime.Now;

            再登録OdShr.InsertRecord(dbConnect, loginUser);

            foreach (NBaseData.DAC.OdShrItem shrItem in 再登録OdShrItems)
            {
                string newID = 新規ID();
                string oldID = shrItem.OdShrItemID;

                shrItem.OdShrItemID = newID;
                shrItem.OdShrID = 再登録OdShr.OdShrID;
                shrItem.InsertRecord(dbConnect, loginUser);

                foreach (NBaseData.DAC.OdShrShousaiItem shrShousaiItem in 再登録OdShrShousaiItems)
                {
                    if (shrShousaiItem.OdShrItemID == oldID)
                    {
                        shrShousaiItem.OdShrShousaiItemID = 新規ID();
                        shrShousaiItem.OdShrItemID = newID;
                        shrShousaiItem.InsertRecord(dbConnect, loginUser);
                    }
                }

            }


            // 2015.11.30 支払が失敗した場合で、合算されている場合、合算データを書き換える
            NBaseData.DAC.OdShrGassanHead gassanHead = NBaseData.DAC.OdShrGassanHead.GetRecordByOdShrId(dbConnect, loginUser, odShr.OdShrID);
            if (gassanHead != null)
            {
                gassanHead.OdShrID = 再登録OdShr.OdShrID;
                gassanHead.RenewDate = DateTime.Now;
                gassanHead.RenewUserID = loginUser.MsUserID;

                gassanHead.UpdateRecord(dbConnect, loginUser);
            }

        }

    }
}
