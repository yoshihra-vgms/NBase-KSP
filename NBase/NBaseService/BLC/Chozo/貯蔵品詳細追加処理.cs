using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
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




using NBaseData.DAC;
using NBaseData.BLC;

namespace NBaseService
{
    public partial interface IService
    {
        [OperationContract]
        bool BLC_貯蔵品詳細保存処理(MsUser loginUser, List<OdChozoShousai> savelist);
        
        //[OperationContract]
        //bool BLC_貯蔵品詳細保存処理_特定品(MsUser loginUser, int vesselId, string yearMonth, List<OdChozoShousai> savelist);
    }

    public partial class Service
    {
        public bool BLC_貯蔵品詳細保存処理(MsUser loginUser, List<OdChozoShousai> savelist)
        {
            bool ret = true;

            using (DBConnect dbcone = new DBConnect())
            {
                dbcone.BeginTransaction();

                //保存リスト
                foreach( OdChozoShousai data in savelist)
                {
                    //最新の状態を取得
                    OdChozoShousai shousai = OdChozoShousai.GetRecord(dbcone, loginUser,data.OdChozoShousaiID);

                    shousai.Count = data.Count;

                    //アップデートする。
                    ret = shousai.UpdateRecord(dbcone, loginUser);

                    if (ret == false)
                    {
                        dbcone.RollBack();
                        return false;
                    }
                }

                dbcone.Commit();
            }

            return true;
        }

        //public bool BLC_貯蔵品詳細保存処理_特定品(MsUser loginUser, int vesselId, string yearMonth, List<OdChozoShousai> shousaiList)
        //{
        //    bool ret = true;

        //    using (DBConnect dbConnect = new DBConnect())
        //    {
        //        dbConnect.BeginTransaction();

        //        OdChozo chozo = OdChozo.GetRecord_Date_VesselID_Kind(dbConnect, loginUser, vesselId, yearMonth, (int)貯蔵品リスト.対象Enum.特定品);
        //        if (chozo == null)
        //        {
        //            chozo = new OdChozo();
        //            chozo.OdChozoID = Guid.NewGuid().ToString();
        //            chozo.MsVesselID = vesselId;
        //            chozo.Nengetsu = yearMonth;
        //            chozo.Shubetsu = (int)貯蔵品リスト.対象Enum.特定品;
        //            chozo.VesselID = vesselId;
        //            chozo.RenewDate = DateTime.Now;
        //            chozo.RenewUserID = loginUser.MsUserID;

        //            ret = chozo.InsertRecord(dbConnect, loginUser);
        //        }

        //        //保存リスト
        //        foreach (OdChozoShousai shousai in shousaiList)
        //        {
        //            if (shousai.OdChozoShousaiID == null || shousai.OdChozoShousaiID.Length == 0)
        //            {
        //                shousai.OdChozoShousaiID = Guid.NewGuid().ToString();

        //                shousai.OdChozoID = chozo.OdChozoID;
        //                shousai.VesselID = vesselId;
        //                shousai.RenewUserID = loginUser.MsUserID;
        //                shousai.RenewDate = DateTime.Now;


        //                ret = shousai.InsertRecord(dbConnect, loginUser);
        //            }
        //            else
        //            {

        //                //最新の状態を取得し、アップデートする
        //                OdChozoShousai saveData = OdChozoShousai.GetRecord(dbConnect, loginUser, shousai.OdChozoShousaiID);
        //                saveData.Count = shousai.Count;
        //                ret = saveData.UpdateRecord(dbConnect, loginUser);
        //            }
        //            if (ret == false)
        //            {
        //                dbConnect.RollBack();
        //                return false;
        //            }
        //        }

        //        dbConnect.Commit();
        //    }

        //    return true;
        //}
    }
}
