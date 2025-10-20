using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Hachu.BLC;
using NBaseData.DAC;
using NBaseData.BLC;

using Hachu.Chozo;

namespace Hachu.BLC
{
    /// <summary>
    ///
    /// </summary>
    public class 貯蔵品金額計算処理
    {
        /// <summary>
        /// 引数：金額計算をする対象リスト、最終月(金額計算月)
        /// 返り値は対象リストの金額項目にデータをセットする。
        /// </summary>
        /// <param name="datalist"></param>
        /// <param name="sdate"></param>
        //public static void 貯蔵品金額計算総括(List<貯蔵品TreeInfo> datalist, int year, int month)
        //{
        //    /*List<貯蔵品リスト> lolist = new List<貯蔵品リスト>();
        //    List<貯蔵品リスト> itemlist = new List<貯蔵品リスト>();

        //    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
        //    {
        //        lolist = serviceClient.貯蔵品リスト_GetRecords(NBaseCommon.Common.LoginUser, year, month, 貯蔵品リスト.Enum対象.潤滑油);
        //        itemlist = serviceClient.貯蔵品リスト_GetRecords(NBaseCommon.Common.LoginUser, year, month, 貯蔵品リスト.Enum対象.船用品);
        //    }*/

        //    //船のデータだけ抽出する。
        //    //とりあえず全船取得はよろしくないので自作する。
        //    //問題が起きたらこちらに切り替える。



        //    //指定リストだけ回す
        //    for (int i = 0; i < datalist.Count; i++)
        //    {
        //        ///貯蔵品金額計算処理.ChozoShousai金額計算(datalist[i], year, month, out datalist[i].金額);

        //        // 2010.03:aki 使用していない？
        //        //int sdfdsa = 145;
        //    }
        //}


        //引数：月次確定した年月。
        //public static void CalcuUkeireDate(int year, int month)
        //{
        //    DateTime date = new DateTime(year, month, 1);
            
        //    //対象は次の月のレコード
        //    date = date.AddMonths(1);


        //    //作成船の取得
        //    List<MsVessel> veslist = new List<MsVessel>();
        //    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
        //    {
        //        veslist = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
        //    }

        //    //全船のデータ作成する。
        //    foreach (MsVessel ves in veslist)
        //    {

        //        //種別分だけ
        //        for (int i = 0; i < 2; i++)
        //        {
        //            //新たに挿入するデータ
        //            List<貯蔵品TreeInfo> datalist = BLC.貯蔵品編集処理.貯蔵品指定データ検索(date.Year, date.Year, date.Month, date.Month, ves.MsVesselID, i);

        //            if (datalist.Count <= 0)
        //            {
        //                continue;
        //            }

        //            //ひと月前のデータ
        //            List<貯蔵品TreeInfo> prelist = BLC.貯蔵品編集処理.貯蔵品指定データ検索(year, year, month, month, ves.MsVesselID, i);

        //            //データの作成
        //            foreach (貯蔵品TreeInfo trdata in datalist)
        //            {
        //                // 受け入れ年月
        //                DateTime da = DateTime.MinValue;

        //                if (prelist == null || prelist.Count == 0)
        //                {
        //                }
        //                else
        //                {
        //                    foreach (貯蔵品TreeInfo pre in prelist)
        //                    {
        //                        if (pre.OdChozoShousaiData.MsLoID == trdata.OdChozoShousaiData.MsLoID &&
        //                            pre.OdChozoShousaiData.MsVesselItemID == trdata.OdChozoShousaiData.MsVesselItemID)
        //                        {
        //                            //一致したら受け入れ年月を設定して終わり
        //                            int dummy = 0;
        //                            da = 貯蔵品金額計算処理.ChozoShousai計算(pre, year, month, out dummy, false);
        //                            break;
        //                        }
        //                    }
        //                }
        //                if (da == DateTime.MinValue)
        //                {
        //                    trdata.OdChozoShousaiData.UkeireNengetsu = date.Year.ToString() + date.Month.ToString("00");
        //                }
        //                else
        //                {
        //                    trdata.OdChozoShousaiData.UkeireNengetsu = da.ToString("yyyyMM");
        //                }
        //                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
        //                {
        //                    serviceClient.OdChozoShousai_UpdateRecord(NBaseCommon.Common.LoginUser, trdata.OdChozoShousaiData);
        //                }
        //            }                        
        //        }       
        //    }

            
                 
        //}


        ////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////
        //private static void ChozoShousai金額計算(貯蔵品TreeInfo treedata, int year, int month, out int amount)
        //{
        //    貯蔵品金額計算処理.ChozoShousai計算(treedata, year, month, out amount, true);
        //}

        //private static DateTime ChozoShousai計算(貯蔵品TreeInfo treedata, int year, int month, out int amount, bool work)
        //{
            
        //    amount = 0;
        //    //data.UkeireNengetsu;

        //    //odchozoデータの取得
        //    OdChozoShousai data = treedata.OdChozoShousaiData;

        //    //計算期間の作成
        //    //受け入れ年月～計算年月まで
        //    DateTime start = new DateTime(int.Parse(data.UkeireNengetsu.Substring(0, 4)), int.Parse(data.UkeireNengetsu.Substring(4, 2)), 1);

        //    DateTime end = new DateTime(year, month, 1);
        //    end = end.AddMonths(1);
        //    end = end.AddMilliseconds(-1.0);

        //    //検索する対象の判定
        //    /*string thi_state = NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID;
        //    int kind = 貯蔵品編集処理.潤滑油種別NO;

        //    //関連データは船用品の検索だった
        //    if (data.MsVesselItemID.Length > 0)
        //    {
        //        thi_state = NBaseCommon.Common.MsThiIraiSbt_船用品ID;
        //        kind = 貯蔵品編集処理.船用品種別NO;
        //    }*/



        //    //指定データの検索をかける。
        //    List<OdChozoShousai> chozolist = new List<OdChozoShousai>();        //貯蔵品
        //    List<OdJryShousaiItem> jrylist = new List<OdJryShousaiItem>();      //受領



        //    //引数：年月の文字列、対応タンカ
        //    Dictionary<string, OdChozoShousai> ChozoDic = new Dictionary<string, OdChozoShousai>();
        //    Dictionary<string, OdJryShousaiItem> JryDic = new Dictionary<string, OdJryShousaiItem>();

        //    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
        //    {
        //        //月ごとのデータを取得していく
        //        int span_y = end.Year - start.Year;

        //        //何カ月分のデータかを算出する。
        //        int smon = (span_y * 12) + (end.Month - start.Month);

        //        //月の数だけ回す。
        //        bool flag = false;
        //        //最終月も含めて取得できるように+1
        //        for (int i = 0; i < smon+1; i++)
        //        {
        //            DateTime sd = start.AddMonths(i);

        //            DateTime ed = sd.AddMonths(1);
        //            ed = ed.AddMilliseconds(-1.0);

        //            //潤滑油の検索
        //            #region 潤滑油
        //            if (data.MsLoID.Length > 0)
        //            {
        //                //貯蔵品詳細データの取得
        //                chozolist = serviceClient.OdChozoShousai_GetRecordsByVesselID_Period_MsLoID(NBaseCommon.Common.LoginUser, data.MsVesselID,
        //                    sd.ToString("yyyyMM"), ed.ToString("yyyyMM"), data.MsLoID);

        //                //受領詳細データの取得
        //                jrylist = serviceClient.OdJryShousaiItem_GetRecordsByMsVesselID_JryStatus_ShousaiID_Date_MsLoID(
        //                    NBaseCommon.Common.LoginUser, NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID, 1, data.MsVesselID, sd, ed, data.MsLoID);

        //            }
        //            #endregion


        //            //船用品の検索
        //            #region 船用品
        //            if (data.MsVesselItemID.Length > 0)
        //            {

        //                //貯蔵品詳細データの取得
        //                chozolist = serviceClient.OdChozoShousai_GetRecordsByVesselID_Period_MsVesselItemID(NBaseCommon.Common.LoginUser, data.MsVesselID,
        //                    sd.ToString("yyyyMM"), ed.ToString("yyyyMM"), data.MsVesselItemID);

        //                //受領詳細データの取得
        //                jrylist = serviceClient.OdJryShousaiItem_GetRecordsByMsVesselID_JryStatus_ShousaiID_Date_MsVesselItemID(
        //                    NBaseCommon.Common.LoginUser, NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID, 1, data.MsVesselID, sd, ed, data.MsVesselItemID);
        //            }
        //            #endregion


        //            //データを保存する
        //            string datestring = sd.ToString("yyyyMM");

        //            //月ごとにアクセスしやすくする。
        //            //貯蔵品
        //            if (chozolist.Count > 0)
        //            {
        //                ChozoDic.Add(datestring, chozolist[0]);
        //                flag = true;
        //            }
        //            else
        //            {
        //                ChozoDic.Add(datestring, new OdChozoShousai());
        //            }

        //            //受領
        //            if (jrylist.Count > 0)
        //            {
        //                JryDic.Add(datestring, jrylist[0]);

        //            }
        //            else
        //            {
        //                JryDic.Add(datestring, new OdJryShousaiItem());
        //            }
        //        }

        //        if (flag == false)
        //        {
        //            return end;
        //        }


        //        DateTime pred = end.AddMonths(-1);

        //        //計算月の残量を取得
        //        int rest = data.Count;

        //        //今月の受け入れ量が多き時は今月分しかない。
        //        OdJryShousaiItem jrdata = new OdJryShousaiItem();
        //        if (treedata.受け入れ > rest)
        //        {
        //            //今月のデータで単価計算で終わり                    
        //            bool rt = JryDic.TryGetValue(end.ToString("yyyyMM"), out jrdata);
        //            if (rt == true)
        //            {
        //                amount = (int)((decimal)rest * jrdata.Tanka);                        
        //            }

        //            return end;
        //        }
        //        else
        //        {
        //            //前月へ遡って受領データを計算してく
        //            for (int i = 1; i < smon + 1; i++)
        //            {
        //                DateTime sd = end.AddMonths(-i);
        //                string ssd = sd.ToString("yyyyMM");


        //                bool rt = JryDic.TryGetValue(ssd, out jrdata);

        //                //成功したら比較
        //                if (rt == true)
        //                {
        //                    //引き切れた
        //                    if (jrdata.JryCount > rest)
        //                    {
        //                        amount += (int)((decimal)rest * jrdata.Tanka);
        //                        return sd;
        //                    }
        //                    else
        //                    {
        //                        amount += (int)((decimal)jrdata.JryCount * jrdata.Tanka);

        //                        rest -= jrdata.JryCount;
        //                    }
        //                }
        //                else
        //                {
        //                    continue;
        //                }
        //            }
        //        }
        //    }



        //    return end;

        //}



        


    }
}
