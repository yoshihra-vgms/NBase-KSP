using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;

namespace NBaseData.BLC
{
    /// <summary>
    /// 表示情報管理
    /// </summary>
    public class 貯蔵品TreeInfo
    {
        //表示するデータ
        public string 品名 = "";
        public int 繰越 = 0;
        public decimal 繰越金額 = 0;
        public int 受け入れ = 0;
        public decimal 受け入れ金額 = 0;
        public int 計 = 0;
        public int 消費 = 0;
        public decimal 消費金額 = 0;

        public int 残量 = 0;
        public decimal 金額 = 0;    //残量金額

        // 2012.08: 金額計算は貯蔵品リストからするように変更
        //public List<NBaseData.BLC.貯蔵品編集RowData残量> 残量s = new List<NBaseData.BLC.貯蔵品編集RowData残量>();
        public List<NBaseData.BLC.貯蔵品リスト> リストs = new List<NBaseData.BLC.貯蔵品リスト>();

        // 次期改造
        public string カテゴリ = "";

        //--------------------------------------
        //持っておくデータ
        public int Tanka;       //品物の単価
        public string ID = "";
        public string 単位 = "";


        //関連データ
        public NBaseData.DAC.OdChozoShousai OdChozoShousaiData;


        public 貯蔵品TreeInfo()
        {
        }

        public 貯蔵品TreeInfo(NBaseData.BLC.貯蔵品編集RowData rd)
        {
            this.ID = rd.ID;
            this.品名 = rd.品名;
            this.繰越 = rd.繰越;
            this.繰越金額 = rd.繰越金額;
            this.受け入れ = rd.受入;
            this.受け入れ金額 = rd.受入金額;
            this.計 = rd.繰越 + rd.受入;
            this.消費 = rd.消費;
            this.消費金額 = rd.消費金額;
            this.残量 = rd.残量;
            this.金額 = rd.残量金額;
            this.OdChozoShousaiData = rd.OdChozoShousaiData;
            // 2012.08: 金額計算は貯蔵品リストからするように変更
            //this.残量s.AddRange(rd.残量s);
            this.リストs.AddRange(rd.リストs);

            // 次期改造
            #region 次期改造
            this.カテゴリ = rd.カテゴリ;
            #endregion
        }
    }

    public class 貯蔵品編集処理
    {
        //public********************************
        /// <summary>
        /// 指定データ検索処理
        /// </summary>
        /// <param name="maskeddate"></param>
        /// <param name="vesselid"></param>        
        public static List<貯蔵品TreeInfo> 貯蔵品指定データ検索(MsUser logiuser, int year, int month, int vesselid, int kind)
        {
            string sdate = 貯蔵品編集処理.CreateYearMonthString(year, month);      //開始
            string sedate = 貯蔵品編集処理.CreateYearMonthString(year, month);     //終了
            string presdate = 貯蔵品編集処理.CreatePrevMonthString(year, month);   //開始前月
            string presedate = 貯蔵品編集処理.CreatePrevMonthString(year, month);   //終了前月
            
            List<OdChozoShousai> shousailist = new List<OdChozoShousai>();      //今回の検索データ
            List<OdChozoShousai> preshousailist = new List<OdChozoShousai>();   //繰越データ            

            List<OdJryShousaiItem> jrylist = new List<OdJryShousaiItem>();      //受領

            List<OdHachuTanka> hachulolist = new List<OdHachuTanka>();          //潤滑油単価
            List<OdHachuTanka> hachuItemlist = new List<OdHachuTanka>();        //船用品単価
            
            //指定期間内
            DateTime startdate = new DateTime(year, month, 1);     //開始
            DateTime enddate = new DateTime(year, month, 1);       //終了
            enddate = enddate.AddMonths(1);
            enddate = enddate.AddMilliseconds(-1.0);

            DateTime begindate = new DateTime();                //始まりの日
            
            //手配ＩＤ指定
            string tehai_shubetsu_id = "";
            if (kind == 0)
            {
                tehai_shubetsu_id = MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.燃料_潤滑油);
            }
            else
            {
                tehai_shubetsu_id = MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品);
            }

            //年月差分を出す
            int yde = year - year;
            int endmonth = month + (12 * yde);

            //今回の貯蔵品詳細データの取得
            //最終月データ
            for (int i = endmonth; i >= month; i--)
            {
                int plusy = (i / 12);
                int m = (i % 12);

                if (m == 0)
                {
                    plusy -= 1;
                    m = 12;
                }
                string sd = 貯蔵品編集処理.CreateYearMonthString(year + plusy, m);
                shousailist = OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(logiuser, vesselid, sd, kind);

                //データがあるなら終了
                if (shousailist.Count > 0)
                {
                    break;
                }
            }

            //繰越計算用の貯蔵品詳細データの取得
            preshousailist = OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(logiuser, vesselid, presdate, kind);

            //受領
            jrylist = OdJryShousaiItem.GetRecordsByMsVesselID_JryStatus_ShousaiID_Date(logiuser, tehai_shubetsu_id,1, vesselid, startdate, enddate);
            
            //最後に表示形式に変換
            List<貯蔵品TreeInfo> tlist = 貯蔵品編集処理.ConvertDataClass(shousailist, preshousailist, jrylist);

            return tlist;
        }

        /// <summary>
        /// 指定年月の文字列を作成する        
        /// </summary>
        /// <param name="y"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        private static string CreateYearMonthString(int y, int m)
        {
            string ansy = y.ToString();
            string ansm = m.ToString();

            //右詰めにする
            ansy = ansy.PadLeft(4, '0');
            ansm = ansm.PadLeft(2, '0');

            string ans = ansy + ansm;

            return ans;
        }

        /// <summary>
        /// 前月のデータ文字列を作成する。
        /// 例）2008 12 →200811 2009 1 →200812
        /// 引数：今月の年、今月の月
        /// 返り値：変換後の文字列
        /// </summary>
        /// <param name="nowy"></param>
        /// <param name="nowm"></param>
        /// <returns></returns>
        private static string CreatePrevMonthString(int nowy, int nowm)
        {
            int y = nowy;
            int m = nowm;

            //前月
            m -= 1;

            //0以下になった時は一年前の十二月
            if (m <= 0)
            {
                y -= 1;
                m = 12;
            }

            string ans = 貯蔵品編集処理.CreateYearMonthString(y, m);

            return ans;
        }
   

        //メンバ関数=================================
        /// <summary>
        /// 検索データより貯蔵品を表示しやすくするデータに変換する
        /// </summary>
        /// <param name="shousailist"></param>
        /// <returns></returns>
        private static List<貯蔵品TreeInfo> ConvertDataClass(
            List<NBaseData.DAC.OdChozoShousai> shousailist,
            List<NBaseData.DAC.OdChozoShousai> preshousailist,            
            List<OdJryShousaiItem> jrylist            )
        {
            List<貯蔵品TreeInfo> tlist = new List<貯蔵品TreeInfo>();


            List<貯蔵品詳細まとめ> matomelist = new List<貯蔵品詳細まとめ>();
            //もし四半期の時が三カ月の総合を出すなら
            //ここでshousailistをItemIDかLoIDでまとめる処理が必要。
            matomelist = BLC.貯蔵品編集処理.ConvertShousaiData(shousailist);

            foreach (貯蔵品詳細まとめ matome in matomelist)
            {
                貯蔵品TreeInfo data = new 貯蔵品TreeInfo();

                //関連付けされている方の名前を取得する--------------------
                //品名
                if (matome.MsLoID.Length > 0)
                {
                    data.品名 = matome.LoName;
                }
                if (matome.MsVesselItemID.Length > 0)
                {
                    data.品名 = matome.VesselItemName;
                }
                //--------------------------------------------------------
                //繰越
                //前月から品名IDの一致するものをとる
                foreach (OdChozoShousai pred in preshousailist)
                {
                    //潤滑油
                    if (pred.MsLoID == matome.MsLoID && matome.MsLoID.Length > 0)
                    {
                        data.繰越 += pred.Count;
                    }


                    //船用品
                    if (pred.MsVesselItemID == matome.MsVesselItemID && matome.MsVesselItemID.Length > 0)
                    {
                        data.繰越 = pred.Count;
                    }
                }
                //--------------------------------------------------------
                //受け入れ
                foreach (OdJryShousaiItem jryshousai in jrylist)
                {
                    if (jryshousai.MsLoID == matome.MsLoID && matome.MsVesselItemID == jryshousai.MsVesselItemID)
                    {
                        if (jryshousai.JryCount > 0)
                        {
                            data.受け入れ += jryshousai.JryCount;
                            data.受け入れ金額 += (int)(jryshousai.JryCount * jryshousai.Tanka);
                        }
                    }
                }

                //--------------------------------------------------------
                //残量                
                data.残量 = matome.Count;

                //--------------------------------------------------------
                //計算箇所のデータ計算
                data.計 = data.繰越 + data.受け入れ;
                data.消費 = data.計 - data.残量;

                //データを記憶
                foreach (OdChozoShousai shousai in shousailist)
                {
                    if (matome.MsLoID == shousai.MsLoID &&
                        matome.MsVesselItemID == shousai.MsVesselItemID)
                    {
                        data.OdChozoShousaiData = shousai;
                        break;
                    }
                }
                
                ////////////////////////////////////////////////
                //作成データのADD
                tlist.Add(data);
            }
            return tlist;
        }

        /// <summary>
        /// IDが同じものをまとめる。
        /// </summary>
        /// <param name="shousailist"></param>
        /// <returns></returns>
        private static List<貯蔵品詳細まとめ> ConvertShousaiData(List<OdChozoShousai> shousailist)
        {
            List<貯蔵品詳細まとめ> ans = new List<貯蔵品詳細まとめ>();
            ans.Clear();

            foreach (OdChozoShousai data in shousailist)
            {
                bool flag = false;                

                //今回のIDがあるかをチェック
                foreach (貯蔵品詳細まとめ dm in ans)
                {
                    //同じものがあった。
                    if (dm.MsLoID == data.MsLoID && dm.MsVesselItemID == data.MsVesselItemID)
                    {
                        //今回のデータを追加する
                        dm.Count += data.Count;

                        flag = true;
                        break;
                    }

                }

                //あったのでもう一回
                if (flag == true)
                {
                    continue;
                }

                //ここまできたら新しくデータを追加する
                貯蔵品詳細まとめ adddata = new 貯蔵品詳細まとめ(
                    data.LoName,
                    data.VesselItemName,
                    data.MsLoID,
                    data.MsVesselItemID,
                    data.Count);

                ans.Add(adddata);
            }

            return ans;
            
        }

        public static DateTime ChozoShousai計算(MsUser logiuser, 貯蔵品TreeInfo treedata, int year, int month)
        {
            //odchozoデータの取得
            OdChozoShousai data = treedata.OdChozoShousaiData;

            //計算期間の作成
            //受け入れ年月～計算年月まで
            DateTime start = new DateTime(int.Parse(data.UkeireNengetsu.Substring(0, 4)), int.Parse(data.UkeireNengetsu.Substring(4, 2)), 1);

            DateTime end = new DateTime(year, month, 1);
            end = end.AddMonths(1);
            end = end.AddMilliseconds(-1.0);

            //指定データの検索をかける。
            List<OdChozoShousai> chozolist = new List<OdChozoShousai>();        //貯蔵品
            List<OdJryShousaiItem> jrylist = new List<OdJryShousaiItem>();      //受領

            //引数：年月の文字列、対応タンカ
            Dictionary<string, OdChozoShousai> ChozoDic = new Dictionary<string, OdChozoShousai>();
            Dictionary<string, OdJryShousaiItem> JryDic = new Dictionary<string, OdJryShousaiItem>();

            //月ごとのデータを取得していく
            int span_y = end.Year - start.Year;

            //何カ月分のデータかを算出する。
            int smon = (span_y * 12) + (end.Month - start.Month);

            //月の数だけ回す。
            bool flag = false;
            //最終月も含めて取得できるように+1
            for (int i = 0; i < smon + 1; i++)
            {
                DateTime sd = start.AddMonths(i);

                DateTime ed = sd.AddMonths(1);
                ed = ed.AddMilliseconds(-1.0);

                //潤滑油の検索
                #region 潤滑油
                if (data.MsLoID.Length > 0)
                {
                    //貯蔵品詳細データの取得
                    chozolist = OdChozoShousai.GetRecordsByVesselID_Period_MsLoID(logiuser, data.MsVesselID,
                        sd.ToString("yyyyMM"), ed.ToString("yyyyMM"), data.MsLoID);

                    //受領詳細データの取得
                    jrylist = OdJryShousaiItem.GetRecordsByMsVesselID_JryStatus_ShousaiID_Date_MsLoID(
                        logiuser, MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.燃料_潤滑油), 1, data.MsVesselID, sd, ed, data.MsLoID);

                }
                #endregion


                //船用品の検索
                #region 船用品
                if (data.MsVesselItemID.Length > 0)
                {

                    //貯蔵品詳細データの取得
                    chozolist = OdChozoShousai.GetRecordsByVesselID_Period_MsVesselItemID(logiuser, data.MsVesselID,
                        sd.ToString("yyyyMM"), ed.ToString("yyyyMM"), data.MsVesselItemID);

                    //受領詳細データの取得
                    jrylist = OdJryShousaiItem.GetRecordsByMsVesselID_JryStatus_ShousaiID_Date_MsVesselItemID(
                        logiuser, MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品), 1, data.MsVesselID, sd, ed, data.MsVesselItemID);
                }
                #endregion


                //データを保存する
                string datestring = sd.ToString("yyyyMM");

                //月ごとにアクセスしやすくする。
                //貯蔵品
                if (chozolist.Count > 0)
                {
                    ChozoDic.Add(datestring, chozolist[0]);
                    flag = true;
                }
                else
                {
                    ChozoDic.Add(datestring, new OdChozoShousai());
                }

                //受領
                if (jrylist.Count > 0)
                {
                    //JryDic.Add(datestring, jrylist[0]);

                    OdJryShousaiItem js = new OdJryShousaiItem();
                    foreach (OdJryShousaiItem j in jrylist)
                    {
                        // 2014.08.01:Add
                        // 2013年度改造により、受領数が入らなくなったレコードへの対応
                        //js.JryCount += j.JryCount;
                        if(j.JryCount > 0)
                            js.JryCount += j.JryCount;
                    }
                    JryDic.Add(datestring, js);
                }
                else
                {
                    JryDic.Add(datestring, new OdJryShousaiItem());
                }
            }

            if (flag == false)
            {
                return end;
            }


            DateTime pred = end.AddMonths(-1);

            //計算月の残量を取得
            int rest = data.Count;

            //今月の受け入れ量が多き時は今月分しかない。
            OdJryShousaiItem jrdata = new OdJryShousaiItem();
            if (treedata.受け入れ > rest)
            {
                //今月のデータで単価計算で終わり                    
                return end;
            }
            else
            {
                //前月へ遡って受領データを計算してく
                //for (int i = 1; i < smon + 1; i++)
                for (int i = 0; i < smon + 1; i++)
                {
                    DateTime sd = end.AddMonths(-i);
                    string ssd = sd.ToString("yyyyMM");


                    bool rt = JryDic.TryGetValue(ssd, out jrdata);

                    //成功したら比較
                    if (rt == true)
                    {
                        // 2014.06.10:Add
                        // 2013年度改造により、受領数が入らなくなったレコードへの対応
                        if (jrdata.JryCount < 0)
                            jrdata.JryCount = 0;

                        //引き切れた
                        if (jrdata.JryCount >= rest)
                        {
                            return sd;
                        }
                        else
                        {
                            rest -= jrdata.JryCount;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return end;
        }

        /////////////////////////////////////////////////////
        //定義クラス
        private class 貯蔵品詳細まとめ
        {
            public 貯蔵品詳細まとめ(string loname, string itemname, string msloid, string itemid, int count)
            {
                this.LoName = loname;
                this.VesselItemName = itemname;

                this.MsLoID = msloid;
                this.MsVesselItemID = itemid;
                this.Count = count;
            }

            public string LoName = "";
            public string VesselItemName = "";

            public string MsLoID = "";
            public string MsVesselItemID = "";

            public int Count = 0;
        }
    }
}
