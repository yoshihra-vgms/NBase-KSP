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
        bool BLC_見積依頼更新処理_新規(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdMm OdMm,
            List<NBaseData.DAC.OdMmItem> OdMmItems,
            List<NBaseData.DAC.OdMmShousaiItem> OdMmShousaiItems
            );

        [OperationContract]
        bool BLC_見積依頼更新処理_更新(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdMm OdMm
            );
    }
    public partial class Service
    {
        #region public bool BLC_見積依頼更新処理_新規(...)
        public bool BLC_見積依頼更新処理_新規(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdMm OdMm,
            List<NBaseData.DAC.OdMmItem> OdMmItems,
            List<NBaseData.DAC.OdMmShousaiItem> OdMmShousaiItems
            )
        {
            foreach (NBaseData.DAC.OdMmItem mmItem in OdMmItems)
            {
                foreach (NBaseData.DAC.OdMmShousaiItem mmShousaiItem in OdMmShousaiItems)
                {
                    if (mmShousaiItem.OdMmItemID == mmItem.OdMmItemID)
                    {
                        mmItem.OdMmShousaiItems.Add(mmShousaiItem);
                    }
                }
            }

            using (DBConnect dbConnect = new DBConnect())
            {
                bool ret = false;

                dbConnect.BeginTransaction();

                ret = 見積依頼登録(dbConnect, loginUser, ref OdMm, OdMmItems);
                if (ret == true)
                {
                    ret = NBaseData.BLC.手配依頼更新処理.見積中(dbConnect, loginUser, OdMm.OdThiID);
                }
                if (ret == true)
                {
                    ret = NBaseData.BLC.事務所更新情報処理.見積依頼登録(dbConnect, loginUser, OdMm.OdThiID);
                }

                bool alarmRet = false;
                alarmRet = NBaseData.BLC.発注アラーム処理.手配アラーム停止(dbConnect, loginUser, OdMm.OdThiID);

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
        #region public bool BLC_見積依頼更新処理_更新(...)
        public bool BLC_見積依頼更新処理_更新(
            NBaseData.DAC.MsUser loginUser,
            NBaseData.DAC.OdMm OdMm
            )
        {
            bool ret = false;
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                if (OdMm.MmNo.Length == (NBaseData.DAC.OdMm.NoLength見積依頼 - 1))
                {
                    OdMm.MmNo = 見積依頼SetMaxNo(dbConnect, loginUser, OdMm.MmNo);
                }
                ret = OdMm.UpdateRecord(dbConnect, loginUser);
                if (ret)
                {
                    if (OdMm.CancelFlag == NBaseCommon.Common.CancelFlag_キャンセル)
                    {
                        ret = NBaseData.BLC.手配依頼更新処理.未対応(dbConnect, loginUser, OdMm.OdThiID);
                    }
                }
                if (ret)
                {
                    ret = 科目更新(dbConnect, loginUser, OdMm.OdThiID, OdMm.OdMmID, OdMm.MsNyukyoKamokuID);
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

        #region private bool 見積依頼登録(...)
        private bool 見積依頼登録(
            DBConnect dbConnect,
            NBaseData.DAC.MsUser loginUser,
            ref NBaseData.DAC.OdMm OdMm,
            List<NBaseData.DAC.OdMmItem> OdMmItems
            )
        {
            bool ret = false;
            if (OdMm.MmNo != null && OdMm.MmNo.Length == (NBaseData.DAC.OdMm.NoLength見積依頼 - 1))
            {
                OdMm.MmNo = 見積依頼SetMaxNo(dbConnect, loginUser, OdMm.MmNo);
            }
            ret = OdMm.InsertRecord(dbConnect, loginUser);
            if (ret == false)
            {
                return ret;
            }
            if (OdMmItems == null)
            {
                return ret;
            }
            foreach (NBaseData.DAC.OdMmItem mmItem in OdMmItems)
            {
                ret = mmItem.InsertRecord(dbConnect, loginUser);
                if (ret == false)
                {
                    break;
                }

                foreach (NBaseData.DAC.OdMmShousaiItem mmShousaiItem in mmItem.OdMmShousaiItems)
                {
                    ret = mmShousaiItem.InsertRecord(dbConnect, loginUser);
                    if (ret == false)
                    {
                        break;
                    }
                }
            }
            return ret;
        }
        #endregion

        private bool 科目更新(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdThiId, string OdMmId, string MsNyukyoKamokuId)
        {

            NBaseData.DAC.OdThi thi = NBaseData.DAC.OdThi.GetRecord(dbConnect, loginUser, OdThiId);
            NBaseData.DAC.MsKamoku kamoku = NBaseData.DAC.MsKamoku.GetRecordByHachuKamoku(loginUser, thi.MsThiIraiSbtID, thi.MsThiIraiShousaiID, MsNyukyoKamokuId);
            if (kamoku == null)
            {
                kamoku = NBaseData.DAC.MsKamoku.GetRecordByHachuKamoku(loginUser, thi.MsThiIraiSbtID, thi.MsThiIraiShousaiID, null);
            }

            return 受領の科目更新(dbConnect, loginUser, OdMmId, kamoku);
        }

        private static bool 受領の科目更新(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdMmId, NBaseData.DAC.MsKamoku kamoku)
        {
            //================================
            // 受領
            //================================
            List<NBaseData.DAC.OdJry> jrys = NBaseData.DAC.OdJry.GetRecordsByOdMmId(dbConnect, loginUser, OdMmId);
            if (jrys == null)
            {
                return true;
            }

            bool ret = true;
            foreach (NBaseData.DAC.OdJry jry in jrys)
            {
                // 変更されていない場合、なにもしない
                if (jry.KamokuNo == kamoku.KamokuNo && jry.UtiwakeKamokuNo == kamoku.UtiwakeKamokuNo)
                    continue;

                jry.KamokuNo = kamoku.KamokuNo;
                jry.UtiwakeKamokuNo = kamoku.UtiwakeKamokuNo;

                ret = jry.UpdateRecord(dbConnect, loginUser);
                if (ret == false)
                {
                    break;
                }

                //================================
                // 支払
                //================================
                ret = 支払の科目更新(dbConnect, loginUser, jry.OdJryID, kamoku);
                if (ret == false)
                {
                    break;
                }
            }

            return ret;
        }

        private static bool 支払の科目更新(DBConnect dbConnect, NBaseData.DAC.MsUser loginUser, string OdJryID, NBaseData.DAC.MsKamoku kamoku)
        {
            List<NBaseData.DAC.OdShr> shrs = NBaseData.DAC.OdShr.GetRecordByOdJryID(dbConnect, loginUser, OdJryID);
            if (shrs == null)
                return true;

            bool ret = true;
            foreach (NBaseData.DAC.OdShr shr in shrs)
            {
                // 変更されていない場合、なにもしない
                if (shr.KamokuNo == kamoku.KamokuNo && shr.UtiwakeKamokuNo == kamoku.UtiwakeKamokuNo)
                    continue;

                shr.KamokuNo = kamoku.KamokuNo;
                shr.UtiwakeKamokuNo = kamoku.UtiwakeKamokuNo;

                ret = shr.UpdateRecord(dbConnect, loginUser);
                if (ret == false)
                {
                    break;
                }
            }
            return ret;
        }
    }
 }
