using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;
using System.Windows.Forms;

using NBaseHonsen.Chozo;
using NBaseData.DAC;
using NBaseData.BLC;

using ExcelCreator=AdvanceSoftware.ExcelCreator;


namespace NBaseHonsen.BLC
{
    public class 貯蔵品年間管理票処理
    {
        public 貯蔵品年間管理票処理()
        {

        }

        //public**************************************************
        //メンバ関数=============================================
        /// <summary>
        ///　貯蔵品年間の管理票出力
        ///　引数：出力ファイル名、出力船、出力年、種別
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static bool 貯蔵品年間管理票出力総括(string filename, MsVessel msvessel, int year, int kind)
        {
            ExcelCreator.Xlsx.XlsxCreator crea = new ExcelCreator.Xlsx.XlsxCreator();
            //ulong id = 0;// ExcelCreatorDLL置き換えによる関数対応：uintからulongへ変更
            object id = 0;//さらに2021/09/08 v10置き換え対応 m.yoshihara

            bool ret = false;
            
            //検索を書ける
            List<貯蔵品年間管理票まとめ> datalist =
                貯蔵品年間管理票処理.SearchSelectYear(msvessel, year, kind);

            //データあらず
            if (datalist.Count <= 0)
            {
                return false;
            }

            //-----------------------------------------------------------
            ret = 貯蔵品年間管理票処理.LoadTemplateFile(ref crea, out id, filename);
            if (ret == false)
            {
                return false;
            }

            //船関係を書く
            BLC.貯蔵品年間管理票処理.WriteVessel(ref crea, msvessel);

            //納品業者を書く
            //BLC.貯蔵品年間管理票処理.WritwGyousya(ref crea, "");

            //年月を書く
            //BLC.貯蔵品年間管理票処理.WriteNengetsu(ref crea, year);
                
            //アイテムデータを書く
            BLC.貯蔵品年間管理票処理.WriteItemData(ref crea, datalist);


            //-----------------------------------------------------------
            crea.CloseBookEx(true, id);
            return true;
        }


        //private*************************************************
        //メンバ関数=============================================
        /// <summary>
        /// テンプレートのロード 
        /// 引数：ロードする場所、ロードＩＤ、保存ファイル名
        /// </summary>
        /// <returns></returns>
        private static bool LoadTemplateFile(ref ExcelCreator.Xlsx.XlsxCreator crea, out object id, string filename)// ExcelCreatorDLL置き換えによる関数対応：第3引数をuintからulongへ変更
        {
            //exeのあるディレクトリ取得
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string temppath = path + "/" + 貯蔵品年間管理票処理.TemplateFileName;

            int ret = crea.OpenBookEx(filename, temppath, out id);

            //失敗
            if (ret < 0)
            {
                Console.WriteLine("テンプレートロード失敗：" + temppath);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 船を書く
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="vessel"></param>
        private static void WriteVessel(ref ExcelCreator.Xlsx.XlsxCreator crea, MsVessel msvessel)
        {
            //書き込み文字列作成
            string s = msvessel.VesselName;
            s += " (";
            s += msvessel.VesselNo;
            s += ")";

            crea.Cell(貯蔵品年間管理票処理.TEMP_船名).Value = s;
        }

        /// <summary>
        /// 業者を書く
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="name"></param>
        private static void WritwGyousya(ref ExcelCreator.Xlsx.XlsxCreator crea, string name)
        {
            crea.Cell(BLC.貯蔵品年間管理票処理.TEMP_業者).Value = name;
        }

        /// <summary>
        /// 年を書く
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="year"></param>
        private static void WriteNengetsu(ref ExcelCreator.Xlsx.XlsxCreator crea, int year)
        {
            string s = year.ToString();
            s += "年度";

            crea.Cell(貯蔵品年間管理票処理.TEMP_年月).Value = s;
        }

        /// <summary>
        /// アイテムデータを書く        
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="datalist"></param>
        private static void WriteItemData(ref ExcelCreator.Xlsx.XlsxCreator crea, List<貯蔵品年間管理票まとめ> datalist)
        {
            int no = 1;

            foreach( 貯蔵品年間管理票まとめ data in datalist)
            {
                //大本ID作成
                string itemid = ITEM_TEMP + no.ToString();

                //品名
                crea.Cell(itemid).Value = data.名前;

                //単位
                string taniid = itemid + TEMP_単位;
                crea.Cell(taniid).Value = data.単位;

                //単価
                string tankaid = itemid + TEMP_単価;
                crea.Cell(tankaid).Value = data.単価;

                //期首残
                string kisyuzanid = itemid + TEMP_期首残;
                crea.Cell(kisyuzanid).Value = data.期首残;

                //上期末残
                string kamikimatsuzanid = itemid + TEMP_上期末残;
                crea.Cell(kamikimatsuzanid).Value = data.上期末残;

                //期末残
                string kimatsuid = itemid + TEMP_期末残;
                crea.Cell(kimatsuid).Value = data.期末残;


                //各月のデータを入れる
                for (int i = 1; i <= 12; i++)
                {
                    //id作成
                    string valueid = itemid + TEMP_数量 + i.ToString();

                    //指定月のデータを書きこむ
                    decimal value = data.GetSelectMonthData(i);
                    crea.Cell(valueid).Value = value;
                }

                //////////////////////////////////////////////
                no++;
            }
        }
            

        /// <summary>
        /// 検索をかける
        /// 2008と指定されたら2008/4～2009/3までを検索しまとめる
        /// 引数：船、検索年、種別
        /// 返り値：まとめたデータ
        /// </summary>
        /// <param name="msvessel"></param>
        /// <param name="year"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static List<貯蔵品年間管理票まとめ> SearchSelectYear(MsVessel msvessel, int year, int kind)
        {
            List<貯蔵品年間管理票まとめ> anslist = new List<貯蔵品年間管理票まとめ>();
            //------------------------------------------------------------------------
            #region 年データ作成(計算を回避するため)
            int[] y = {        
                          0,            
                          year + 1,     //1
                          year + 1,     //2
                          year + 1,     //3
                          year,         //4
                          year,         //5
                          year,         //6
                          year,         //7
                          year,         //8
                          year,         //9
                          year,         //10
                          year,         //11
                          year,         //12                          
                      };
            #endregion                       


            //最終単価＝三月の単価
            //4→3で設定していけば必然的に最終単価になるはず

            //一年分
            for (int i = 4; i < 12 + 4; i++)
            {   
                int m = i % 12;

                if( m == 0)
                {
                    m = 12;
                }
                
   
                // 2010.05.18 コメントにしました
                ////品名検索
                ////List<Chozo.貯蔵品TreeInfo> datalist = BLC.貯蔵品編集処理.貯蔵品指定データ検索(
                ////    y[m], y[m], 
                ////    m, m,
                ////    msvessel.MsVesselID, kind);
                //List<Chozo.貯蔵品TreeInfo> datalist = BLC.貯蔵品編集処理.貯蔵品指定データ検索2(
                //    y[m], y[m],
                //    m, m,
                //    msvessel.MsVesselID, kind);

                ////まとめます
                //貯蔵品年間管理票処理.ConvertDataList(ref anslist, datalist, kind, m);
            
            }
            
            //------------------------------------------------------------------------
            return anslist;
        }

        /// <summary>
        /// 検索データをまとめる
        /// ないときは新たにADD        
        /// </summary>
        /// <param name="datalsit"></param>
        /// <param name="src"></param>        
        private static void ConvertDataList(ref List<貯蔵品年間管理票まとめ> datalist,
            List<貯蔵品TreeInfo> src, int kind, int month)
        {
            //期首残
            int startmonth = 4;

            //上期末残
            int kamiendmonth = 9;

            //期末残
            int lastmonth = 3;

            foreach (貯蔵品TreeInfo data in src)
            {
                bool ret = false;

                //ID取得
                //string id = data.OdChozoShousaiData.MsLoID;                
                //if (kind == BLC.貯蔵品編集処理.船用品種別NO)
                //{
                //    id = data.OdChozoShousaiData.MsVesselItemID;
                //}
                string id = data.ID;
                

                //同じものがあるかをチェック
                foreach (貯蔵品年間管理票まとめ d in datalist)
                {
                    //同じものがある
                    if (d.ID == id)
                    {
                        //設定
                        d.SetSelectMonthData(data.受け入れ, month);
                        ret = true;                        
                        
                        d.単価 = data.Tanka;

                        //期首残
                        if (month == startmonth)
                        {
                            d.期首残 = data.繰越;
                        }
                        //上期末残
                        if (month == kamiendmonth)
                        {
                            d.上期末残 = data.残量;
                        }
                        //期末残
                        if (month == lastmonth)
                        {
                            d.期末残 = data.残量;
                        }

                        break;
                    }
                    
                }

                if (ret == true)
                {
                    continue;
                }

                //ここまで来てないなら新たにADDする
                貯蔵品年間管理票まとめ newdata = new 貯蔵品年間管理票まとめ();
                newdata.ID = id;

                //名前を設定
                //newdata.名前 = data.OdChozoShousaiData.LoName;
                //if (kind == BLC.貯蔵品編集処理.船用品種別NO)
                //{
                //    newdata.名前 = data.OdChozoShousaiData.VesselItemName;
                //}                
                newdata.名前 = data.品名;

                //newdata.単位 = data.OdChozoShousaiData.TaniName;                
                //newdata.単価 = data.Tanka;
                newdata.単位 = data.単位;            
                newdata.単価 = data.Tanka;


                //指定月にデータを入れる
                newdata.SetSelectMonthData(data.受け入れ, month);

                //期首残
                if (month == startmonth)
                {
                    newdata.期首残 = data.繰越;
                }            
                //上期末残
                if (month == kamiendmonth)
                {
                    newdata.上期末残 = data.残量;
                }
                //期末残
                if (month == lastmonth)
                {
                    newdata.期末残 = data.残量;
                }

                //ADD
                datalist.Add(newdata);
                
            }
            
        }

        //メンバ変数=============================================

        //定数--------------------------------------------------
        //テンプレートファイル名
        const string TemplateFileName = "Template/貯蔵品年間管理票Template.xlsx";
        
        //各種名前を定義する------------------------------------

        //各列の名前たち
        private const string ITEM_TEMP = "**ITEM";
        private const string TEMP_単位 = "_TANI";
        private const string TEMP_単価 = "_TANKA";
        private const string TEMP_数量 = "_VALUE";
        private const string TEMP_期首残 = "_KISYUZAN";
        private const string TEMP_上期末残 = "_KAMIMATSUZAN";
        private const string TEMP_期末残 = "_KIMATSUZAN";

        private const string TEMP_船名 = "**VESSEL_NAME";
        private const string TEMP_業者 = "**GYOUSYA";

        private const string TEMP_年月 = "**NENGETSU";


        //================================================================
        //内包クラス
        //管理票を出力しやすくするクラス
        public class 貯蔵品年間管理票まとめ
        {
            //指定月設定
            public bool SetSelectMonthData(int value, int month)
            {
                /*int[] data = {
                                -999,
                                数量1月,
                                数量2月,
                                数量3月,
                                数量4月,
                                数量5月,
                                数量6月,
                                数量7月,
                                数量8月,
                                数量9月,
                                数量10月,
                                数量11月,
                                数量12月                           
                      
                             };

                //範囲外
                if (month > 12 || month < 1)
                {
                    return false;
                }

                data[month] += value;*/

                switch (month)
                {
                    case 1:
                        this.数量1月 = value;
                        break;
                    case 2:
                        this.数量2月 = value;
                        break;
                    case 3:
                        this.数量3月 = value;
                        break;
                    case 4:
                        this.数量4月 = value;
                        break;
                    case 5:
                        this.数量5月 = value;
                        break;
                    case 6:
                        this.数量6月 = value;
                        break;
                    case 7:
                        this.数量7月 = value;
                        break;
                    case 8:
                        this.数量8月 = value;
                        break;
                    case 9:
                        this.数量9月 = value;
                        break;
                    case 10:
                        this.数量10月 = value;
                        break;
                    case 11:
                        this.数量11月 = value;
                        break;
                    case 12:
                        this.数量12月 = value;
                        break;

                    default:
                        return false;
                        break;
                }
             
                return true;
            }

            //指定月取得
            public decimal GetSelectMonthData(int month)
            {
                decimal[] data = {
                                0,
                                数量1月,
                                数量2月,
                                数量3月,
                                数量4月,
                                数量5月,
                                数量6月,
                                数量7月,
                                数量8月,
                                数量9月,
                                数量10月,
                                数量11月,
                                数量12月                           
                             };

                //範囲外
                if (month > 12 || month < 1)
                {
                    return 0;
                }

                return data[month];

            }
            
            public string ID = "";
            public string 名前 = "";
            public decimal 単価 = 0.0m;
            public string 単位 = "";

            public decimal 期首残 = 0;      //四月の繰越である
            public decimal 上期末残 = 0;    //九月の残量である
            public decimal 期末残 = 0;      //三月の残量である

            public decimal 数量4月 = 0;
            public decimal 数量5月 = 0;
            public decimal 数量6月 = 0;
            public decimal 数量7月 = 0;
            public decimal 数量8月 = 0;
            public decimal 数量9月 = 0;
            public decimal 数量10月 = 0;
            public decimal 数量11月 = 0;
            public decimal 数量12月 = 0;
            public decimal 数量1月 = 0;
            public decimal 数量2月 = 0;
            public decimal 数量3月 = 0;
        }
    }

    
}
