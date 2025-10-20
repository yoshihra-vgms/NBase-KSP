using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ORMapping;
using System.ServiceModel;

using NBaseData.DAC;

namespace NBaseData.BLC
{
    public class 貯蔵品処理
    {

        /// <summary>
        /// OD_CHOZOとOD_CHOZO_SHOUSAIのデータ船用品と潤滑油マスターより作成する
        /// 引数：作成ユーザー、作成する年月(例：2009年8月→200908)
        /// </summary>
        /// <param name="logiuser"></param>
        /// <param name="sdate"></param>
        public static void 貯蔵品詳細テーブルデータ作成(MsUser logiuser, string sdate, ref List<OdChozo> chozoList, ref List<OdChozoShousai> chozoShousaiList)
        {
            DateTime ym1 = new DateTime(int.Parse(sdate.Substring(0, 4)), int.Parse(sdate.Substring(4, 2)), 1);
            DateTime ym2 = ym1.AddMonths(-1);

            List<MsVessel> msVessels = MsVessel.GetRecordsByHachuEnabled(logiuser);
            foreach (NBaseData.DAC.MsVessel vessel in msVessels)
            {
                int vesselId = vessel.MsVesselID;


//// 2016.06.09 TEST
//if ((vesselId == 5 || vesselId == 47) == false)
//    continue;


                ////潤滑油0、船用品1
                //for (int i = 0; i < 2; i++)
                //潤滑油0、船用品1、特定品2
                for (int i = 0; i < 3; i++)
                {

//// 2016.06.09 TEST
//if ( i != 0 )
//    continue;

                    // OD_CHOZO データの作成
                    OdChozo chozo = 貯蔵品処理.Create貯蔵品Table(logiuser, vesselId, sdate, i);
                    chozoList.Add(chozo);

                    // OD_CHOZO_SHOUSAI データの作成
                    List<OdChozoShousai> shousaiList = null;
                    if (i == 0)
                    {
                        shousaiList = 貯蔵品処理.Create貯蔵品詳細潤滑油Data(logiuser, vesselId, sdate, chozo);
                    }
                    //else
                    else if (i == 1)
                    {
                        shousaiList = 貯蔵品処理.Create貯蔵品詳細船用品Data(logiuser, vesselId, sdate, chozo);
                    }
                    else
                    {
                        shousaiList = 貯蔵品処理.Create貯蔵品詳細特定品Data(logiuser, vesselId, sdate, chozo);
                    }

                    if (i == 2)
                    {
                        // 特定品は追加情報なし
                        chozoShousaiList.AddRange(shousaiList);
                    }
                    else
                    {
                        // 受入年月をセットする処理
                        List<貯蔵品TreeInfo> prelist = 貯蔵品編集処理.貯蔵品指定データ検索(logiuser, ym2.Year, ym2.Month, vesselId, i);
                        //foreach (OdChozoShousai shousai in shousaiList)
                        //{
                        //    DateTime da = DateTime.MinValue;
                        //    if (prelist != null && prelist.Count > 0)
                        //    {
                        //        foreach (貯蔵品TreeInfo pre in prelist)
                        //        {
                        //            if (
                        //                 (pre.OdChozoShousaiData.MsLoID.Length > 0 && pre.OdChozoShousaiData.MsLoID == shousai.MsLoID) ||
                        //                 (pre.OdChozoShousaiData.MsVesselItemID.Length > 0 && pre.OdChozoShousaiData.MsVesselItemID == shousai.MsVesselItemID)
                        //                )
                        //            {
                        //                da = 貯蔵品編集処理.ChozoShousai計算(logiuser, pre, ym2.Year, ym2.Month);
                        //                break;
                        //            }
                        //        }
                        //    }
                        //    if (da == DateTime.MinValue)
                        //    {
                        //        shousai.UkeireNengetsu = sdate;
                        //    }
                        //    else
                        //    {
                        //        shousai.UkeireNengetsu = da.ToString("yyyyMM");
                        //    }
                        //    chozoShousaiList.Add(shousai);
                        //}
                        foreach (OdChozoShousai shousai in shousaiList)
                        {
                            DateTime da = DateTime.MinValue;
                            if (prelist != null && prelist.Count > 0)
                            {
                                foreach (貯蔵品TreeInfo pre in prelist)
                                {
                                    if (
                                         (pre.OdChozoShousaiData.MsLoID.Length > 0 && pre.OdChozoShousaiData.MsLoID == shousai.MsLoID) ||
                                         (pre.OdChozoShousaiData.MsVesselItemID.Length > 0 && pre.OdChozoShousaiData.MsVesselItemID == shousai.MsVesselItemID)
                                        )
                                    {
                                        shousai.UkeireNengetsu = pre.OdChozoShousaiData.UkeireNengetsu;
                                        break;
                                    }
                                }
                            }
                            if (shousai.UkeireNengetsu == null || shousai.UkeireNengetsu.Length == 0)
                            {
                                shousai.UkeireNengetsu = sdate;
                            }

                            chozoShousaiList.Add(shousai);
                        }
                    }

                }
            }
            return;
        }

        //貯蔵品の追加処理。
        //貯蔵品追加
        private static OdChozo Create貯蔵品Table(NBaseData.DAC.MsUser logiuser, int vesselid, string sdate, int kind)
        {
            OdChozo ret = OdChozo.GetRecord_Date_VesselID_Kind(logiuser, vesselid, sdate, kind);

            //すでに追加済み
            if (ret != null)
            {
                return ret;
            }
            //ret = new OdChozo();

            //#region データ作成
            //ret.OdChozoID = Guid.NewGuid().ToString();
            //ret.MsVesselID = vesselid;
            //ret.Nengetsu = sdate;
            //ret.Shubetsu = kind;
            //ret.VesselID = vesselid;
            //ret.UserKey = "0";
            //ret.RenewDate = DateTime.Now;
            //ret.RenewUserID = logiuser.MsUserID;
            //#endregion

            //return ret;

            return CreateOdChozo(logiuser, vesselid, sdate, kind);
        }

        //潤滑油詳細品の追加
        private static List<OdChozoShousai> Create貯蔵品詳細潤滑油Data(NBaseData.DAC.MsUser logiuser, int vesselid, string sdate, OdChozo chozo)
        {
            List<OdChozoShousai> retList = new List<OdChozoShousai>();

            //関連するデータを取得
            List<MsLoVessel> ret = MsLoVessel.GetRecordsByMsVesselID(logiuser, vesselid);

            foreach (MsLoVessel data in ret)
            {
                OdChozoShousai shousai = new OdChozoShousai();

                #region データ作成
                //IDの生成
                shousai.OdChozoShousaiID = Guid.NewGuid().ToString();

                shousai.OdChozoID = chozo.OdChozoID;
                shousai.MsLoID = data.MsLoID;

                shousai.Count = 0;          //数量

                shousai.ItemName = "";
                shousai.SendFlag = 0;
                shousai.DataNo = 0;

                shousai.VesselID = vesselid;
                shousai.RenewDate = DateTime.Now;
                shousai.RenewUserID = logiuser.MsUserID;
                #endregion

                retList.Add(shousai);
            }

            return retList;
        }

        //船用品詳細の追加
        private static List<OdChozoShousai> Create貯蔵品詳細船用品Data(NBaseData.DAC.MsUser logiuser, int vesselid, string sdate, OdChozo chozo)
        {
            List<OdChozoShousai> retList = new List<OdChozoShousai>();

            //指定船の関連船用品を取得する
            List<MsVesselItemVessel> datalist = MsVesselItemVessel.GetRecordsByMsVesselID(logiuser, vesselid, (int)MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント);

            foreach (MsVesselItemVessel data in datalist)
            {
                OdChozoShousai shousai = new OdChozoShousai();

                #region データ作成
                //IDの生成
                shousai.OdChozoShousaiID = Guid.NewGuid().ToString();

                shousai.OdChozoID = chozo.OdChozoID;
                shousai.MsVesselItemID = data.MsVesselItemID;

                shousai.Count = 0;          //数量

                shousai.ItemName = "";
                shousai.SendFlag = 0;
                shousai.DataNo = 0;

                shousai.VesselID = vesselid;
                shousai.RenewUserID = logiuser.MsUserID;
                shousai.RenewDate = DateTime.Now;
                #endregion

                retList.Add(shousai);
            }

            return retList;

        }

        //特定品詳細の追加
        private static List<OdChozoShousai> Create貯蔵品詳細特定品Data(NBaseData.DAC.MsUser logiuser, int vesselid, string sdate, OdChozo chozo)
        {
            List<OdChozoShousai> retList = new List<OdChozoShousai>();

            //指定船の関連船用品を取得する
            List<MsVesselItemVessel> datalist = MsVesselItemVessel.GetRecordsByMsVesselID(logiuser, vesselid);
            var list = datalist.Where(obj => (obj.CategoryNumber != (int)MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント && obj.SpecificFlag == 1));
            if (list.Count() > 0)
            {
                foreach (MsVesselItemVessel data in list)
                {
                    //OdChozoShousai shousai = new OdChozoShousai();

                    //#region データ作成
                    ////IDの生成
                    //shousai.OdChozoShousaiID = Guid.NewGuid().ToString();

                    //shousai.OdChozoID = chozo.OdChozoID;
                    //shousai.MsVesselItemID = data.MsVesselItemID;

                    //shousai.Count = 0;          //数量

                    //shousai.ItemName = "";
                    //shousai.SendFlag = 0;
                    //shousai.DataNo = 0;

                    //shousai.VesselID = vesselid;
                    //shousai.RenewUserID = logiuser.MsUserID;
                    //shousai.RenewDate = DateTime.Now;
                    //#endregion

                    OdChozoShousai shousai = CreateOdChozoShousai(logiuser, vesselid, chozo.OdChozoID, null, data.MsVesselItemID);
                    retList.Add(shousai);
                }
            }

            return retList;

        }

        public static OdChozo CreateOdChozo(NBaseData.DAC.MsUser logiuser, int vesselid, string sdate, int kind)
        {
            OdChozo chozo = new OdChozo();

            chozo.OdChozoID = Guid.NewGuid().ToString();
            chozo.MsVesselID = vesselid;
            chozo.Nengetsu = sdate;
            chozo.Shubetsu = kind;
            chozo.VesselID = vesselid;
            chozo.UserKey = "0";
            chozo.RenewDate = DateTime.Now;
            chozo.RenewUserID = logiuser.MsUserID;

            return chozo;
        }


        public static OdChozoShousai CreateOdChozoShousai(NBaseData.DAC.MsUser logiuser, int vesselid, string odChozoID, string msLoID, string msVesselItemID)
        {
            OdChozoShousai shousai = new OdChozoShousai();

            #region データ作成
            //IDの生成
            shousai.OdChozoShousaiID = Guid.NewGuid().ToString();

            shousai.OdChozoID = odChozoID;
            shousai.MsLoID = msLoID;
            shousai.MsVesselItemID = msVesselItemID;

            shousai.Count = 0;          //数量

            shousai.ItemName = "";
            shousai.SendFlag = 0;
            shousai.DataNo = 0;

            shousai.VesselID = vesselid;
            shousai.RenewUserID = logiuser.MsUserID;
            shousai.RenewDate = DateTime.Now;
            #endregion

            return shousai;
        }

    }
}
