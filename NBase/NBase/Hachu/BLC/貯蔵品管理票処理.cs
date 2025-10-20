using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;
using System.Windows.Forms;

using Hachu.Chozo;
using WingData.DAC;

using ExcelCreator;

namespace Hachu.BLC
{
    public class 貯蔵品管理票処理
    {        
        //public**************************************
        /// <summary>
        /// 引数：保存ファイル名前、船、年、終了年、開始月、終了月、表示中のリスト
        /// 返り値：成功したか？
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool 管理票保存総括(string filename, 
            MsVessel msvessel, int kind, int year, int eyear, int smonth, int emonth, List<貯蔵品TreeInfo> datalist)
        {
            XlsCreator creator = new XlsCreator();
            uint id = 0;

            //テンプレートの読み込み
            bool ret = BLC.貯蔵品管理票処理.LoadTemplateFile(ref creator,
                ref id, filename);

            if (ret == false)
            {
                return false;
            }


            //データ書き込み
            ret = BLC.貯蔵品管理票処理.WriteDataMain(ref creator, msvessel, kind,
                year, eyear, smonth, emonth, datalist);

            if (ret == false)
            {
                return false;
            }          
    
            //閉じる
            creator.CloseBookEx(true, id);
            return true;
        }
        

         
        //////////////////////////////////////////////////////
        //メンバ変数======================================        
        
              
        //private**********************************************************
        //メンバ関数===========================================
        /// <summary>
        /// テンプレートのロード 
        /// 引数：ロードする場所、ロードＩＤ、保存ファイル名
        /// </summary>
        /// <returns></returns>
        private static bool LoadTemplateFile(ref XlsCreator crea, ref uint id, string filename)
        {
            //exeのあるディレクトリ取得
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            //string temppath = System.IO.Directory.GetCurrentDirectory() + "/" + 貯蔵品管理票処理.TemplateFileName;
            string temppath = path + "/" + 貯蔵品管理票処理.TemplateFileName;

            int ret = crea.OpenBookEx(filename, temppath, ref id);

            //失敗
            if (ret < 0)
            {
                Console.WriteLine("テンプレートロード失敗：" + temppath);
                return false;
            }

            return true;
        }

        /// <summary>
        /// データ書き込み総括        
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="msvessel"></param>
        /// <param name="kind"></param>
        /// <param name="year"></param>
        /// <param name="smonth"></param>
        /// <param name="emonth"></param>
        /// <param name="datalist"></param>
        /// <returns></returns>
        private static bool WriteDataMain(ref XlsCreator crea,
            MsVessel msvessel, int kind, 
            int year, int eyear, int smonth, int emonth, List<貯蔵品TreeInfo> datalist)
        {
            //本年度期間内予算
            int yosan = 0;

            //種別
            貯蔵品管理票処理.WriteShubetsu(ref crea, kind);

            //船名
            貯蔵品管理票処理.WriteVesselName(ref crea, msvessel);

            //期間
            貯蔵品管理票処理.WriteDate(ref crea, year, eyear, smonth, emonth);

            //アイテム
            貯蔵品管理票処理.WriteItemData(ref crea, datalist);

            //予算たち                  
            貯蔵品管理票処理.WriteYosan(ref crea, kind, yosan);

            return true;
        }

        /// <summary>
        /// 種別を書く
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="kind"></param>
        private static void WriteShubetsu(ref XlsCreator crea, int kind)
        {
            //名前を取得
            string name = BLC.貯蔵品編集処理.KindData[kind];

            crea.Cell(貯蔵品管理票処理.TEMP_SHUBETSU).Value = name;
        }

        /// <summary>
        /// 船名を書く
        /// </summary>
        /// <returns></returns>
        private static void WriteVesselName(ref XlsCreator crea, MsVessel msvessel)
        {
            string data = msvessel.VesselName;
            data += " (";
            data += msvessel.VesselNo;
            data += ")";

            crea.Cell(貯蔵品管理票処理.TEMP_VESSEL_NAME).Value = data;
           
        }

        /// <summary>
        /// 期間を書く
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="year"></param>
        /// <param name="sm"></param>
        /// <param name="em"></param>
        private static void WriteDate(ref XlsCreator crea, int year, int eyear, int sm, int em)
        {
            string sdate = year.ToString();
            sdate += "年";
            sdate += sm.ToString();
            sdate += "月 ～ ";

            sdate += eyear.ToString();
            sdate += "年";
            sdate += em.ToString();
            sdate += "月";

            crea.Cell(貯蔵品管理票処理.TEMP_DATE).Value = sdate;

                
        }

        /// <summary>
        /// アイテムデータを書く
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="datalist"></param>
        private static void WriteItemData(ref XlsCreator crea, List<貯蔵品TreeInfo> datalist)
        {
            int i = 1;
            
            foreach (貯蔵品TreeInfo inf in datalist)
            {
                string itemtemp = BLC.貯蔵品管理票処理.ITEM_TEMP;
                itemtemp += i.ToString();

                //品名
                crea.Cell(itemtemp).Value = inf.品名;
                
                //単位
                string stani = itemtemp + BLC.貯蔵品管理票処理.ITEM_単位;
                crea.Cell(stani).Value = inf.OdChozoShousaiData.TaniName;

                //単価
                string stanka = itemtemp + BLC.貯蔵品管理票処理.TEMP_単価;
                crea.Cell(stanka).Value = inf.Tanka;

                //繰越
                string skurikoshi = itemtemp + BLC.貯蔵品管理票処理.ITEM_繰越;
                crea.Cell(skurikoshi).Value = inf.繰越;

                //受け入れ
                string sukeire = itemtemp + BLC.貯蔵品管理票処理.TEMP_受け入れ;
                crea.Cell(sukeire).Value = inf.受け入れ;

                //残量
                string szanryou = itemtemp + BLC.貯蔵品管理票処理.TEMP_残量;
                crea.Cell(szanryou).Value = inf.残量;                  
                //-------------------------------------------
                i++;
            }

        }



        /// <summary>
        /// 本年度予算と消費/予算を書く
        /// </summary>
        /// <param name="crea"></param>
        /// <param name="yosan"></param>
        private static void WriteYosan(ref XlsCreator crea, int kind, int yosan)
        {
            //潤滑油のときは予算を書く
            if (kind == BLC.貯蔵品編集処理.潤滑油種別NO)
            {
                crea.Cell(BLC.貯蔵品管理票処理.TEMP_YOSAN).Value = yosan.ToString();
            }
            //船用品のときは-を書く
            if( kind == BLC.貯蔵品編集処理.船用品種別NO)
            {
                crea.Cell(BLC.貯蔵品管理票処理.TEMP_YOSAN).Value = "-";
                crea.Cell(BLC.貯蔵品管理票処理.消費予算Cell).Value = "-";
                
            }


            
        }


        //-------------------------------------------------------------
        //メンバ変数===========================================
        //表示数
        private const int MAX_ITEM = 30;
        
        //テンプレートファイル名
        private const string TemplateFileName = "Template/貯蔵品管理票Template.xls";

        //各列の名前たち
        //アイテム関連
        private const string ITEM_TEMP = "**ITEM_";
        private const string ITEM_単位 = "_TANI";
        private const string ITEM_繰越 = "_KURIKOSHI";
        private const string TEMP_受け入れ = "_UKEIRE";
        private const string TEMP_単価 = "_TANKA";
        private const string TEMP_残量 = "_ZANRYO";

        //種別
        private const string TEMP_SHUBETSU = "**SHUBETSU";

        //船
        private const string TEMP_VESSEL_NAME = "**VESSEL_NAME";

        //日付
        private const string TEMP_DATE = "**NENGETSU";

        //期間内予算
        private const string TEMP_YOSAN = "**YOSAN";
       

        //消費/予算の場所
        private const string 消費予算Cell = "K44";

    }
}
