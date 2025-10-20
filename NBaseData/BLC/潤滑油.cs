using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using ORMapping;

namespace NBaseData.BLC
{
    public class 潤滑油
    {
        public static bool BLC_潤滑油登録(MsUser loginUser, MsLoVessel msLoVessel)
        {
            bool ret = true;

            // 最終月次確定
            OdGetsujiShime shime = OdGetsujiShime.GetRecordByLastDate(loginUser);           
            string ym = null;
            if (shime != null)
            {
                // 最終月次確定の翌月
                ym = DateTime.Parse(shime.NenGetsu.Substring(0,4) + "/" + shime.NenGetsu.Substring(4,2) + "/01").AddMonths(1).ToString("yyyyMM");
            }
            int vesselId = msLoVessel.MsVesselID;
            MsLoVessel orgLoVessel = null;
            OdChozo odChozo1 = null;
            OdChozo odChozo2 = null;
            OdChozoShousai odChozoShousai1 = null;
            OdChozoShousai odChozoShousai2 = null;

            try
            {
                orgLoVessel = MsLoVessel.GetRecord(loginUser, msLoVessel.MsLoVesselID);
                odChozo1 = OdChozo.GetRecord_Date_VesselID_Kind(loginUser, vesselId, shime.NenGetsu, (int)OdChozo.ShubetsuEnum.潤滑油);
                if (odChozo1 != null)
                {     
                    odChozoShousai1 = OdChozoShousai.GetRecordsByVesselID_Date_LoID(loginUser, vesselId, shime.NenGetsu, msLoVessel.MsLoID);
                }
                odChozo2 = OdChozo.GetRecord_Date_VesselID_Kind(loginUser, vesselId, ym, (int)OdChozo.ShubetsuEnum.潤滑油);
                if (odChozo2 != null)
                {
                    odChozoShousai2 = OdChozoShousai.GetRecordsByVesselID_Date_LoID(loginUser, vesselId, ym, msLoVessel.MsLoID);
                }
            }
            catch
            {
            }

            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    if (orgLoVessel == null)
                    {
                        ret = msLoVessel.InsertRecord(dbConnect, loginUser);
                    }
                    else
                    {
                        ret = msLoVessel.UpdateRecord(dbConnect, loginUser);
                    }

                    if (shime != null)
                    {
                        if (ret && odChozo1 == null)
                        {
                            odChozo1 = 貯蔵品処理.CreateOdChozo(loginUser, vesselId, shime.NenGetsu, (int)OdChozo.ShubetsuEnum.潤滑油);
                            ret = odChozo1.InsertRecord(dbConnect, loginUser);

                        }
                        if (ret && odChozoShousai1 == null)
                        {
                            odChozoShousai1 = 貯蔵品処理.CreateOdChozoShousai(loginUser, vesselId, odChozo1.OdChozoID, msLoVessel.MsLoID, null);

                            odChozoShousai1.UkeireNengetsu = shime.NenGetsu;

                            ret = odChozoShousai1.InsertRecord(dbConnect, loginUser);
                        }
                        if (ret && odChozo2 == null)
                        {
                            odChozo2 = 貯蔵品処理.CreateOdChozo(loginUser, vesselId, ym, (int)OdChozo.ShubetsuEnum.潤滑油);
                            ret = odChozo2.InsertRecord(dbConnect, loginUser);

                        }
                        if (ret && odChozoShousai2 == null)
                        {
                            odChozoShousai2 = 貯蔵品処理.CreateOdChozoShousai(loginUser, vesselId, odChozo2.OdChozoID, msLoVessel.MsLoID, null);

                            odChozoShousai2.UkeireNengetsu = ym;

                            ret = odChozoShousai2.InsertRecord(dbConnect, loginUser);
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
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    dbConnect.RollBack();
                }
            }
            return ret;
        }

    }
}
