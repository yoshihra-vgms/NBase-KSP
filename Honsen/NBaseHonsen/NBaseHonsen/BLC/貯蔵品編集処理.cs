using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;
using System.Windows.Forms;
using NBaseData.DS;
using NBaseData.BLC;
using NBaseData.DAC;
using SyncClient;
using NBaseUtil;
using NBaseHonsen.util;

namespace NBaseHonsen.BLC
{
    [Serializable]
    public class 貯蔵品編集処理
    {
        //public********************************
        /// <summary>
        /// 指定データ検索処理
        /// </summary>
        /// <param name="maskeddate"></param>
        /// <param name="vesselid"></param>        
        //public static List<Chozo.貯蔵品TreeInfo> 貯蔵品指定データ検索(int year, int eyear, int sm, int em, int vesselid, int kind)
        public static List<貯蔵品TreeInfo> 貯蔵品指定データ検索(int year, int eyear, int sm, int em, int vesselid, int kind)
        {
            現在表示している種別 = kind;

            string sdate = 貯蔵品編集処理.CreateYearMonthString(year, sm);      //開始
            string sedate = 貯蔵品編集処理.CreateYearMonthString(year, em);     //終了
            string presdate = 貯蔵品編集処理.CreatePrevMonthString(year, sm);   //開始前月
            string presedate = 貯蔵品編集処理.CreatePrevMonthString(year, em);   //終了前月

            

            List<OdChozoShousai> shousailist = new List<OdChozoShousai>();      //今回の検索データ
            List<OdChozoShousai> preshousailist = new List<OdChozoShousai>();   //繰越データ            

            List<OdJryShousaiItem> jrylist = new List<OdJryShousaiItem>();      //受領

            List<OdHachuTanka> hachulolist = new List<OdHachuTanka>();          //潤滑油単価
            List<OdHachuTanka> hachuItemlist = new List<OdHachuTanka>();        //船用品単価
            
            //指定期間内
            DateTime startdate = new DateTime(year, sm, 1);     //開始
            DateTime enddate = new DateTime(eyear, em, 1);       //終了
            enddate = enddate.AddMonths(1);
            enddate = enddate.AddMilliseconds(-1.0);

            DateTime begindate = new DateTime(1800, 1, 1);                //始まりの日(できるだけ古い年月を指定する)

            //手配ＩＤ指定
            string tehai_shubetsu_id = MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.燃料_潤滑油);

            //if (kind == 貯蔵品編集処理.船用品種別NO)
            if (kind != 貯蔵品編集処理.潤滑油種別NO)
            {
                tehai_shubetsu_id = MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.船用品);
            }


            //年月差分を出す
            int yde = eyear - year;
            int endmonth = em + (12 * yde);


            //検索
            //using (WingServer.ServiceClient sc = new Hachu.WingServer.ServiceClient())
            {
                //今回の貯蔵品詳細データの取得
                //最終月データ
                for (int i = endmonth; i >= sm; i--)
                {
                    int plusy = (i / 12);
                    int m = (i % 12);

                    if (m == 0)
                    {
                        plusy -= 1;
                        m = 12;
                    }


                    string sd = 貯蔵品編集処理.CreateYearMonthString(year + plusy, m);
                    shousailist = OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(NBaseCommon.Common.LoginUser, vesselid, sd, kind);

                    //データがあるなら終了
                        if (shousailist.Count > 0)
                    {
                        break;
                    }
                }

                //繰越計算用の貯蔵品詳細データの取得
                preshousailist = OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(NBaseCommon.Common.LoginUser, vesselid, presdate, kind);

                //受領
                jrylist = OdJryShousaiItem.GetRecordsByMsVesselID_JryStatus_ShousaiID_Date
                    (NBaseCommon.Common.LoginUser, tehai_shubetsu_id,
                    1, vesselid, startdate, enddate);

                // 2010.05.18 金額表示なしに変更
                ////発注単価(潤滑油)(最初の日から全部)
                //hachulolist = OdHachuTanka.GetRecordsByMsVesselItemID_Date_LO
                //    (NBaseCommon.Common.LoginUser, vesselid,
                //    begindate, enddate);

                ////発注単価(船用品)
                //hachuItemlist = OdHachuTanka.GetRecordsByMsVesselItemID_Date_VesselItem
                //    (NBaseCommon.Common.LoginUser, vesselid,
                //    begindate, enddate);
                
            }

            // 次期改造
            List<MsVesselItemCategory> categoryList = MsVesselItemCategory.GetRecords(NBaseCommon.Common.LoginUser);
            List<MsVesselItem> vesselItemList = MsVesselItem.GetRecords(NBaseCommon.Common.LoginUser);

            //最後に表示形式に変換
            //////List<Chozo.貯蔵品TreeInfo> tlist = 
            //////    貯蔵品編集処理.ConvertDataClass(
            //////    shousailist, preshousailist, 
            //////    jrylist, hachulolist, hachuItemlist);
            ////List<貯蔵品TreeInfo> tlist =
            ////    貯蔵品編集処理.ConvertDataClass(
            ////    shousailist, preshousailist,
            ////    jrylist);
            //List<貯蔵品TreeInfo> tlist =
            //    貯蔵品編集処理.ConvertDataClass(
            //    shousailist, preshousailist,
            //    jrylist, kind);
            List<貯蔵品TreeInfo> tlist =
                貯蔵品編集処理.ConvertDataClass(
                shousailist, preshousailist,
                jrylist, kind,
                categoryList,
                vesselItemList);

            return tlist;
        }

        /// <summary>
        /// 指定年月を指定し、保存ができるかどうかを確認する
        /// 引数：年、月
        /// 返り値：保存可能かどうか？
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static bool Check保存(int year, int month)
        {
            OdGetsujiShime shime = null;

            string sdate = 貯蔵品編集処理.CreateYearMonthString(year, month);

            //最終月を取得
            //using (WingServer.ServiceClient sc = new Hachu.WingServer.ServiceClient())
            {
                shime = OdGetsujiShime.GetRecordByLastDate(NBaseCommon.Common.LoginUser);
            }

            //取得できない時も保存可能
            if (shime == null)
            {
                return true;
            }

            int shimey = 0;
            int shimem = 0;

            //年と月に分解
            BLC.貯蔵品編集処理.ConverDateString(shime.NenGetsu, ref shimey, ref shimem);

            //年が前の時は編集できない
            if (year < shimey)
            {
                return false;
            }

            //年が同じで月が若かった時も編集不可
            if (year == shimey && month <= shimem)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// 指定データリストを保存する。
        /// 引数：保存したい表示リスト
        /// </summary>
        /// <param name="savedata"></param>
        public static bool 保存処理(List<貯蔵品TreeInfo> savedatalsit)
        {
            if (savedatalsit == null)
            {
                return false;
            }

            using (ORMapping.DBConnect cone = new ORMapping.DBConnect())
            {
                bool flag = true;

                /////////////////////////////////////////////////////
                //トランザクション開始
                cone.BeginTransaction();

                foreach (貯蔵品TreeInfo data in savedatalsit)
                {
                    SyncTableSaver.InsertOrUpdate(data.OdChozoShousaiData, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, cone);

                    //失敗した
                    if (flag == false)
                    {
                        cone.RollBack();
                        return false;
                    }
                }

                // 本船更新情報
                OdChozo odChozo = new OdChozo();
                odChozo.Shubetsu = 現在表示している種別;
                PtHonsenkoushinInfo honsenkoushinInfo = 本船更新情報.CreateInstance(odChozo, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.貯蔵品登録);
                SyncTableSaver.InsertOrUpdate(honsenkoushinInfo, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, cone);
                
                /////////////////////////////////////////////////////
                cone.Commit();

            }

            return true;
        }


        public static void 検索年ComboBox初期化(ref ComboBox combo, int kind)
        {
            List<OdChozo> datalist = new List<OdChozo>();
            
            //指定種別のデータを取得
            //using (WingServer.ServiceClient sc = new Hachu.WingServer.ServiceClient())
            {
                datalist = OdChozo.GetRecordsByShubetsu(NBaseCommon.Common.LoginUser, kind);
            }

            if (datalist == null)
            {
                return;
            }

            int y = 0;
            int m = 0;
            string sdate = "";
            
            //アイテムクリア
            combo.Items.Clear();

            int now = 0;
            int selectindex = 0;

            foreach (OdChozo data in datalist)
            {
                sdate = data.Nengetsu;
                
                貯蔵品編集処理.ConverDateString(sdate, ref y, ref m);

                //降順取得なのでnowより小さい=ＡＤＤ済み
                if (y <= now)
                {
                    continue;                
                }

                now = y;

                combo.Items.Add(y);

                ////////////////////////////////////////////////
                //今年を選択する
                if (y == DateTime.Now.Year)
                {
                    selectindex = combo.Items.Count - 1;
                }               

                combo.SelectedIndex = selectindex;
                ////////////////////////////////////////////////
                
                
            }

            if (datalist.Count == 0)
            {
                return;
            }

            //最後に開始年を覚える
            貯蔵品編集処理.ConverDateString(datalist[0].Nengetsu, ref y, ref m);
            貯蔵品編集処理.StartYear = y;

            

        }
        

        //private*******************************
        /// <summary>
        /// 渡されてきたマスクデータのコンバート
        /// (例)2009/4→200904、845/12→084512        
        /// 引数：変換文字列
        /// 返り値：変換後の文字列
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        private static string ConvertMaskedDateString(string mask)
        {
            
            // /区切りのデータを取得
            string[] data = mask.Split('/');

            //空白の削除
            string year = data[0].Trim();
            string month = data[1].Trim();

            //右詰めにする
            year = year.PadLeft(4, '0');
            month = month.PadLeft(2, '0');


            string ans = year + month;                  
            return ans;
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
        /// CreateYearMonthStringで作ったものを年と月に分けて元に戻す
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        private static bool ConverDateString(string sdate, ref int year, ref int month)
        {
            string sy = sdate.Substring(0, 4);
            string sm = sdate.Substring(4);

            year = 0;
            month = 0;

            try
            {
                year = Convert.ToInt32(sy);
                month = Convert.ToInt32(sm);
            }
            catch (Exception e)
            {
                //失敗したときはとりあえず0
                year = 0;
                month = 0;
                return false;
            }

            return true;
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

        /// <summary>
        /// 指定TreeListの中身を再計算する。
        /// </summary>
        /// <param name="?"></param>
        public static void TreeInfo再計算(ref List<貯蔵品TreeInfo> datalist)
        {           

            //必要な部分の再計算を行う。
            foreach (貯蔵品TreeInfo data in datalist)
            {

                //金額
                data.金額 = data.残量 * data.Tanka;


                //計算箇所のデータ計算
                data.計 = data.繰越 + data.受け入れ;
                data.消費 = data.計 - data.残量;

                //Dataクラスにデータを入れる
                data.OdChozoShousaiData.Count = data.残量;

            }
        }
    

        //メンバ関数=================================
        /// <summary>
        /// 検索データより貯蔵品を表示しやすくするデータに変換する
        /// </summary>
        /// <param name="shousailist"></param>
        /// <returns></returns>
        ////// 2015.03 特定品対応
        ////// 2010.05.18 金額表示なしに変更
        //////private static List<Chozo.貯蔵品TreeInfo> ConvertDataClass(
        //////    List<NBaseData.DAC.OdChozoShousai> shousailist,
        //////    List<NBaseData.DAC.OdChozoShousai> preshousailist,            
        //////    List<OdJryShousaiItem> jrylist,
        //////    List<OdHachuTanka> hachulolist,
        //////    List<OdHachuTanka> hachuitemlist
        //////    )
        ////private static List<貯蔵品TreeInfo> ConvertDataClass(
        ////    List<NBaseData.DAC.OdChozoShousai> shousailist,
        ////    List<NBaseData.DAC.OdChozoShousai> preshousailist,
        ////    List<OdJryShousaiItem> jrylist
        ////    )
        //private static List<貯蔵品TreeInfo> ConvertDataClass(
        //    List<NBaseData.DAC.OdChozoShousai> shousailist,
        //    List<NBaseData.DAC.OdChozoShousai> preshousailist,
        //    List<OdJryShousaiItem> jrylist, int kind
        //    )
        private static List<貯蔵品TreeInfo> ConvertDataClass(
            List<NBaseData.DAC.OdChozoShousai> shousailist,
            List<NBaseData.DAC.OdChozoShousai> preshousailist,
            List<OdJryShousaiItem> jrylist, int kind,
             List<MsVesselItemCategory> categoryList,
            List<MsVesselItem> vesselItemList)
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
                        // 2009.11.10:aki 受け入れは、受領数で！
                        //data.受け入れ += jryshousai.Count;
                        if (jryshousai.JryCount > 0)
                            data.受け入れ += jryshousai.JryCount;
                        //break;
                    }
                }

                //--------------------------------------------------------
                //残量                
                data.残量 = matome.Count;


                // 2010.05.18 金額表示なしに変更
                ////--------------------------------------------------------
                ////金額
                ////潤滑油のとき
                //if (matome.MsLoID.Length > 0)
                //{
                //    foreach (OdHachuTanka hachu in hachulolist)
                //    {
                //        //一致したものの金額
                //        if (matome.MsLoID == hachu.MsLoID)
                //        {
                //            data.金額 = (int)hachu.Tanka * data.残量;

                //            data.Tanka = (int)hachu.Tanka;
                //            break;
                //        }
                //    }
                //}
                ////船用品の時
                //if (matome.MsVesselItemID.Length > 0)
                //{
                //    foreach (OdHachuTanka hachu in hachuitemlist)
                //    {
                //        //一致したものの金額
                //        if (matome.MsVesselItemID == hachu.MsVesselItemID)
                //        {
                //            data.金額 = (int)hachu.Tanka * data.残量;

                //            data.Tanka = (int)hachu.Tanka;
                //            break;
                //        }
                //    }
                //}

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
                
                //単位
                data.単位 = matome.Taniname;




                ////////////////////////////////////////////////
                //作成データのADD
                //tlist.Add(data);
                if (kind == BLC.貯蔵品編集処理.潤滑油種別NO || kind == BLC.貯蔵品編集処理.船用品種別NO)
                {
                    tlist.Add(data);
                }
                else
                {
                    // 次期改造
                    if (vesselItemList.Any(obj => obj.MsVesselItemID == matome.MsVesselItemID))
                    {
                        MsVesselItem vi = vesselItemList.Where(obj => obj.MsVesselItemID == matome.MsVesselItemID).First();
                        if (categoryList.Any(obj => obj.CategoryNumber == vi.CategoryNumber))
                        {
                            data.カテゴリ = categoryList.Where(obj => obj.CategoryNumber == vi.CategoryNumber).First().CategoryName;
                        }
                    }

                    if (data.繰越 > 0 || data.受け入れ > 0 || data.残量 > 0)
                    {
                        tlist.Add(data);
                    }
                }
            }

            #region 前のソース
            /*
            //詳細品目から取得する
            foreach (OdChozoShousai shousai in shousailist)
            {
                Chozo.貯蔵品TreeInfo data = new Chozo.貯蔵品TreeInfo();

                //関連付けされている方の名前を取得する--------------------
                //品名
                if (shousai.MsLoID.Length > 0)
                {
                    data.品名 = shousai.LoName;
                }
                if (shousai.MsVesselItemID.Length > 0)
                {
                    data.品名 = shousai.VesselItemName;
                }
                //--------------------------------------------------------
                //繰越
                //前月から品名IDの一致するものをとる
                foreach( OdChozoShousai pred in preshousailist)
                {
                    //潤滑油
                    if (pred.MsLoID == shousai.MsLoID && shousai.MsLoID.Length > 0 )
                    {
                        data.繰越 = pred.Count;
                        break;
                    }


                    //船用品
                    if (pred.MsVesselItemID == shousai.MsVesselItemID && shousai.MsVesselItemID.Length > 0)
                    {
                        data.繰越 = pred.Count;
                        break;
                    }
                }

                //--------------------------------------------------------
                //受け入れ
                foreach (OdJryShousaiItem jryshousai in jrylist)
                {
                    if (jryshousai.MsLoID == shousai.MsLoID && shousai.MsVesselItemID == jryshousai.MsVesselItemID)
                    {
                        data.受け入れ += jryshousai.Count;
                        break;
                    }              
                }
                
                //--------------------------------------------------------
                //残量                
                data.残量 = shousai.Count;

                //--------------------------------------------------------
                //金額
                //潤滑油のとき
                if (shousai.MsLoID.Length > 0)
                {
                    foreach (OdHachuTanka hachu in hachulolist)
                    {
                        //一致したものの金額
                        if (shousai.MsLoID == hachu.MsLoID)
                        {
                            data.金額 = (int)hachu.Tanka * data.残量;

                            data.Tanka = (int)hachu.Tanka;
                            break;
                        }
                    }
                }
                //船用品の時
                if (shousai.MsVesselItemID.Length > 0)
                {
                    foreach (OdHachuTanka hachu in hachuitemlist)
                    {
                        //一致したものの金額
                        if (shousai.MsVesselItemID == hachu.MsVesselItemID)
                        {
                            data.金額 = (int)hachu.Tanka * data.残量;

                            data.Tanka = (int)hachu.Tanka;
                            break;
                        }
                    }
                }

                //--------------------------------------------------------


                //計算箇所のデータ計算
                data.計 = data.繰越 + data.受け入れ;
                data.消費 = data.計 - data.残量;

                //データを記憶
                data.OdChozoShousaiData = shousai;


                ////////////////////////////////////////////////
                //作成データのADD
                tlist.Add(data);
            }
             * */
            #endregion

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
                    data.Count,
                    data.TaniName);

                ans.Add(adddata);
            }

            return ans;
            
        }


        //--------------------------------------------------------
        //メンバ変数************************************
        private static int StartYear = 0;

        public static int START_YEAR
        {
            get { return StartYear; }
        }
        
        //-------------------------------
        //検索用に使うデータ
        public static string[] KindData = {
                                               "潤滑油",        //SHUBETSU 0
                                               "船用品",        //SHUBETSU 1
                                               "特定品",        //SHUBETSU 2
                                           };

        public const int 潤滑油種別NO = 0;
        public const int 船用品種別NO = 1;

        public static int 現在表示している種別;


        //ADDの月日データ                                                   表示文字列          開始    終了    年
        //public static Chozo.検索月管理[] SearchMothData = {         
        //                                                 new Chozo.検索月管理( "4月",             4,      4,    0),
        //                                                 new Chozo.検索月管理( "5月",             5,      5,    0),                                                         
        //                                                 new Chozo.検索月管理( "6月",             6,      6,    0),
        //                                                 new Chozo.検索月管理( "7月",             7,      7,    0),
        //                                                 new Chozo.検索月管理( "8月",             8,      8,    0),
        //                                                 new Chozo.検索月管理( "9月",             9,      9,    0),
        //                                                 new Chozo.検索月管理( "10月",            10,     10,    0),
        //                                                 new Chozo.検索月管理( "11月",          11,     11,    0),
        //                                                 new Chozo.検索月管理( "12月",          12,     12,    0),
        //                                                 new Chozo.検索月管理( "1月",             1,      1,    0),
        //                                                 new Chozo.検索月管理( "2月",             2,      2,    0),                                                         
        //                                                 new Chozo.検索月管理( "3月",             3,      3,    0),
        //                                                 new Chozo.検索月管理( "第1四半期(4～6月)",      4,      6,    0),
        //                                                 new Chozo.検索月管理( "第2四半期(7～9月)",      7,      9,    0),
        //                                                 new Chozo.検索月管理( "第3四半期(10～12月)",    10,     12,    0),
        //                                                 new Chozo.検索月管理( "第4四半期(1～3月)",      1,      3,    1),   
                                                         
        //                                             };
        public static Chozo.検索月管理[] SearchMothData = {         
                                                         new Chozo.検索月管理( "4月",             4,      4,    0),
                                                         new Chozo.検索月管理( "5月",             5,      5,    0),                                                         
                                                         new Chozo.検索月管理( "6月",             6,      6,    0),
                                                         new Chozo.検索月管理( "7月",             7,      7,    0),
                                                         new Chozo.検索月管理( "8月",             8,      8,    0),
                                                         new Chozo.検索月管理( "9月",             9,      9,    0),
                                                         new Chozo.検索月管理( "10月",            10,     10,    0),
                                                         new Chozo.検索月管理( "11月",          11,     11,    0),
                                                         new Chozo.検索月管理( "12月",          12,     12,    0),
                                                         new Chozo.検索月管理( "1月",             1,      1,    0),
                                                         new Chozo.検索月管理( "2月",             2,      2,    0),                                                         
                                                         new Chozo.検索月管理( "3月",             3,      3,    0),                                                         
                                                     };


        /////////////////////////////////////////////////////
        //定義クラス
        private class 貯蔵品詳細まとめ
        {
            public 貯蔵品詳細まとめ(string loname, string itemname, string msloid, string itemid, int count, string taniname)
            {
                this.LoName = loname;
                this.VesselItemName = itemname;
                
                this.MsLoID = msloid;
                this.MsVesselItemID = itemid;
                this.Count = count;
                this.Taniname = taniname;

            }

            public string LoName = "";
            public string VesselItemName = "";

            public string MsLoID = "";
            public string MsVesselItemID = "";

            public int Count = 0;

            public string Taniname = "";
            //public OdChozoShousai dataclass = null;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="eyear"></param>
        /// <param name="sm"></param>
        /// <param name="em"></param>
        /// <param name="vesselid"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        //public static List<Chozo.貯蔵品TreeInfo> 貯蔵品指定データ検索2(int year, int eyear, int sm, int em, int vesselid, int kind)
        //{
        //    List<NBaseHonsen.Chozo.貯蔵品TreeInfo> tlist = new List<NBaseHonsen.Chozo.貯蔵品TreeInfo>();
        //    List<NBaseData.BLC.貯蔵品リスト> 繰越貯蔵品List = null;
        //    List<NBaseData.BLC.貯蔵品リスト> 対象貯蔵品List = null;
        //    List<NBaseData.BLC.貯蔵品リスト> 最終貯蔵品List = null;

        //    string presdate = 貯蔵品編集処理.CreatePrevMonthString(year, sm);   // 開始前月
        //    int prey = 0;
        //    int prem = 0;
        //    ConverDateString(presdate, ref prey, ref prem);
        //    List<OdChozoShousai> preshousailist = new List<OdChozoShousai>();   // 繰越(開始前月)データ            
        //    string enddate = 貯蔵品編集処理.CreateYearMonthString(eyear, em);   // 最終月
        //    List<OdChozoShousai> endshousailist = new List<OdChozoShousai>();   // 最終データ            

        //    //検索
        //        NBaseData.BLC.貯蔵品リスト.対象Enum 対象 = NBaseData.BLC.貯蔵品リスト.対象Enum.潤滑油;
        //        if (kind == 貯蔵品編集処理.船用品種別NO)
        //        {
        //            対象 = NBaseData.BLC.貯蔵品リスト.対象Enum.船用品;
        //        }

        //        //繰越計算用の貯蔵品詳細データの取得
        //        preshousailist = OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(NBaseCommon.Common.LoginUser, vesselid, presdate, kind);
        //        繰越貯蔵品List = BLC_貯蔵品編集_取得(NBaseCommon.Common.LoginUser, prey, prem, prey, prem, vesselid, 対象);
               
        //        // 対象
        //        対象貯蔵品List = BLC_貯蔵品編集_取得(NBaseCommon.Common.LoginUser, year, sm, eyear, em, vesselid, 対象);
                
        //        //最終月計算用の貯蔵品詳細データの取得
        //        endshousailist = OdChozoShousai.GetRecordsByVesselID_Date_Shubetsu(NBaseCommon.Common.LoginUser, vesselid, enddate, kind);
        //        最終貯蔵品List = BLC_貯蔵品編集_取得(NBaseCommon.Common.LoginUser, eyear, em, eyear, em, vesselid, 対象);

        //    //
        //    foreach (OdChozoShousai chozoShousai in preshousailist)
        //    {
        //        Chozo.貯蔵品TreeInfo data = new Chozo.貯蔵品TreeInfo();

        //        string id = "";
        //        if (kind == 貯蔵品編集処理.潤滑油種別NO)
        //        {
        //            id = chozoShousai.MsLoID;
        //            data.ID = id;
        //            data.品名 = chozoShousai.LoName;
        //        }
        //        else
        //        {
        //            id = chozoShousai.MsVesselItemID;
        //            data.ID = id;
        //            data.品名 = chozoShousai.VesselItemName;
        //        }
        //        data.単位 = chozoShousai.TaniName;

        //        //==================================
        //        // 繰越分
        //        data.繰越 = chozoShousai.Count;
        //        var 繰越tmpList = from p in 繰越貯蔵品List
        //                      where p.ID == id
        //                      select p;
        //        foreach (var tmp in 繰越tmpList)
        //        {
        //            decimal 単価 = 0;
        //            if (tmp.支払数 > 0)
        //            {
        //                単価 = tmp.支払単価;
        //            }
        //            else
        //            {
        //                単価 = tmp.受領単価;
        //            }
        //            data.繰越金額 += (int)(tmp.残量 * 単価);
        //        }



        //        //==================================
        //        // 受入
        //        var 受入tmpList = from p in 対象貯蔵品List
        //                      where p.ID == id
        //                      select p;
        //        foreach (var tmp in 受入tmpList)
        //        {
        //            decimal 単価 = 0;
        //            if (tmp.支払数 > 0)
        //            {
        //                単価 = tmp.支払単価;
        //                if (CheckDate(presdate, tmp.受領年月))
        //                {
        //                    data.受け入れ += tmp.支払数;
        //                    data.受け入れ金額 += (int)(tmp.支払数 * 単価);
        //                }
        //            }
        //            else
        //            {
        //                単価 = tmp.受領単価;
        //                if (CheckDate(presdate, tmp.受領年月))
        //                {
        //                    data.受け入れ += tmp.受領数;
        //                    data.受け入れ金額 += (int)(tmp.受領数 * 単価);
        //                }
        //            }
        //        }

        //        //==================================
        //        // 繰越分
        //        var 最終tmpList = from p in 最終貯蔵品List
        //                        where p.ID == id
        //                        select p;
        //        foreach (var tmp in 最終tmpList)
        //        {
        //            decimal 単価 = 0;
        //            if (tmp.支払数 > 0)
        //            {
        //                単価 = tmp.支払単価;
        //            }
        //            else
        //            {
        //                単価 = tmp.受領単価;
        //            }
        //            data.残量 += tmp.残量;
        //            data.金額 += (int)(tmp.残量 * 単価);
        //        }

        //        data.計 = data.繰越 + data.受け入れ;
        //        data.消費 = data.計 - data.残量;
        //        data.消費金額 = data.繰越金額 + data.受け入れ金額 - data.金額;

        //        //データを記憶
        //        foreach (OdChozoShousai shousai in endshousailist)
        //        {
        //            if (chozoShousai.MsLoID == shousai.MsLoID &&
        //                chozoShousai.MsVesselItemID == shousai.MsVesselItemID)
        //            {
        //                data.OdChozoShousaiData = shousai;
        //                break;
        //            }
        //        }

        //        tlist.Add(data);

        //    }

        //    return tlist;
        //}
        //private static bool CheckDate(string ym1, string ym2)
        //{
        //    int y1 = 0;
        //    int m1 = 0;
        //    int y2 = 0;
        //    int m2 = 0;
        //    ConverDateString(ym1,ref y1, ref m1);
        //    ConverDateString(ym2,ref y2, ref m2);
        //    if (y1 < y2)
        //    {
        //        return true;
        //    }
        //    if (y1 == y2 && m1 < m2)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public static List<NBaseData.BLC.貯蔵品リスト> BLC_貯蔵品編集_取得(NBaseData.DAC.MsUser loginUser, int sy, int sm, int ey, int em, int msVesselId, NBaseData.BLC.貯蔵品リスト.対象Enum outputKind)
        //{
        //    List<NBaseData.BLC.貯蔵品リスト> retList = new List<NBaseData.BLC.貯蔵品リスト>();
        //    DateTime st = new DateTime(sy, sm, 1);
        //    DateTime ed = new DateTime(ey, em, 1);

        //    for (DateTime tmp_ym = st; tmp_ym <= ed; tmp_ym = tmp_ym.AddMonths(1))
        //    {
        //        int tmp_y = tmp_ym.Year;
        //        int tmp_m = tmp_ym.Month;

        //        List<NBaseData.BLC.貯蔵品リスト> tmp_list = NBaseData.BLC.貯蔵品リスト.GetRecordsByMsVesselId(loginUser, tmp_y, tmp_m, outputKind, msVesselId);

        //        retList.AddRange(tmp_list);
        //    }
        //    return retList;
        //}





    }
}
