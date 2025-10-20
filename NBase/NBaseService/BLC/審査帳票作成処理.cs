using System;
using System.Data;
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
using System.IO;
using NBaseData.DAC;
using NBaseData.DS;
using System.Collections.Generic;
using ExcelCreator=AdvanceSoftware.ExcelCreator;
using NBaseData.BLC;
using System.Drawing;

namespace NBaseService
{
    #region Interfaceたち
    public partial interface IService
    {
        [OperationContract]
        byte[] BLC_審査帳票取得(int year, MsUser loginUser);
    }

    public partial class Service
    {
        public byte[] BLC_審査帳票取得(int year, MsUser loginUser)
        {
            //一時的な書き出しファイルを作成
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            //string filename = path + "審査outputファイル.xlsx";
            string filename = path + "outPut_[" + loginUser.FullName + "]_審査帳票.xlsx";
            
            //出力
            審査帳票作成 crea = new 審査帳票作成();
            bool ret = crea.Create審査帳票(filename, year, loginUser);

            if (ret == false)
            {
                return null;
            }

            //なぜか待つ
            System.Threading.Thread.Sleep(3000);


            byte[] bytBuffer;
            #region バイナリーへ変換
            using (FileStream objFileStream = new FileStream(filename, FileMode.Open))
            {
                long lngFileSize = objFileStream.Length;

                bytBuffer = new byte[(int)lngFileSize];
                objFileStream.Read(bytBuffer, 0, (int)lngFileSize);
                objFileStream.Close();
            }
            #endregion

            return bytBuffer;

            
        }
    }
    #endregion

    
    public class 審査帳票作成
    {
        //public**************************************************************
        /// <summary>
        /// 審査帳票作成
        /// 引数：出力ファイル名前、基準年、ユーザー
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public bool Create審査帳票(string filename, int base_year, MsUser loginUser)
        {
            //テンプレートファイル名を作成
            string path = System.Configuration.ConfigurationManager.AppSettings["帳票TemplatePath"];
            string template = path + 審査帳票作成.TemplateFileName;

            //開く
            using (ExcelCreator.Xlsx.XlsxCreator crea = new ExcelCreator.Xlsx.XlsxCreator())
            {
                int ret = crea.OpenBook(filename, template);

                //失敗
                if (ret < 0)
                {
                    return false;
                }


                //書き込みの準備をする
                this.書き込み準備(base_year, loginUser);
                ////////////////////////////////////////////////////////////////////
                

                this.書き込み総括(crea);

                ////////////////////////////////////////////////////////////////////

                //閉じる
                crea.CloseBook(true);
            }
            return true;
        }


        //private*************************************************************
        //メンバ関数========================================================
        private void 書き込み準備(int base_year, MsUser loginUser)
        {
            //開始年を設定
            this.StartYear = base_year + 審査帳票作成.Pre基準年;
            this.EndYear = base_year + 審査帳票作成.After基準年;

            //書き出し船の船検索
			//発注Enable
            this.VesselList = MsVessel.GetRecordsByHachuEnabled(loginUser);

            //書き出し審査の検索
            this.ShinsaList = MsShinsa.GetRecords(loginUser);

            //初期化データを作っておく
            this.DataDic.Clear();            
            foreach (MsVessel ves in this.VesselList)
            {
                List<KsShinsa> shinsalist = new List<KsShinsa>();
                this.DataDic.Add(ves.MsVesselID, shinsalist);
            }

            //審査データの検索
            DateTime start = new DateTime(this.StartYear, 1, 1);
            DateTime end = new DateTime(this.EndYear + 1, 1, 1, 0, 0, 0, 0);
            end = end.AddMilliseconds(-1.0);

            //関連データの検索
            List<KsShinsa> datalist = KsShinsa.GetRecordsBy期間データ(loginUser, start, end);


            //データコンバート
            foreach (KsShinsa shin in datalist)
            {
				try
				{
					this.DataDic[shin.MsVesselID].Add(shin);
				}
				catch
				{
					continue;
				}
            }
                
        }

        //引数：書き込み場所、書き込んだ列数
        private void 不要行の削除(ExcelCreator.Xlsx.XlsxCreator crea, int index)
        {
            //開始行と最終行の場所を取得する
            Point start = crea.GetVarNamePos(審査帳票作成.TEMP_開始列, 0);
            Point end = crea.GetVarNamePos(審査帳票作成.TEMP_最終列, 0);
            
            //削除幅を出す
            int dels = (start.Y + index);
            //2021/09/03 m.yoshihara int w = end.Height - dels;
            int w = (end.Y+1) - dels;
            w += 1; //これないと一行あまる

            //削除する
            crea.RowDelete(dels, w);
        }

        private void 書き込み総括(ExcelCreator.Xlsx.XlsxCreator crea)
        {           
        
            //書き出し日書き込み
            this.Write日付(crea);
            
            //年度書き込み
            this.Write年度(crea);

            //設定色の書き込み
            this.Write設定色(crea);

            //全船データ書き込み
            int index = 0;
            foreach (MsVessel ves in this.VesselList)
            {
                this.WriteData(crea, ves, this.DataDic[ves.MsVesselID], ref index);

                //審査分だけ進める
                //index += this.ShinsaList.Count;
            }


            //後片付け
            this.不要行の削除(crea, index);


        }

        //書き出し日書き込み
        private void Write日付(ExcelCreator.Xlsx.XlsxCreator crea) 
        {
            crea.Cell(審査帳票作成.TMEP_日付).Value = DateTime.Now.ToString("yyyy/MM/dd");
        }

        //書き出し年書き込み
        private void Write年度(ExcelCreator.Xlsx.XlsxCreator crea)
        {
            int index = 0;
            for (int i = this.StartYear; i <= this.EndYear; i++)
            {
                string temp = 審査帳票作成.TEMP_年 + index.ToString();
                crea.Cell(temp).Value = i.ToString() + "年";

                index++;
            }
        }

        /// <summary>
        /// 予定色と実績色を書き込む
        /// </summary>
        /// <param name="crea"></param>
        private void Write設定色(ExcelCreator.Xlsx.XlsxCreator crea)
        {
            //実績色書き込み
            crea.Cell(審査帳票作成.TEMP_実績色).Attr.BackColor = 審査帳票作成.Color実績;

            //予定色書き込み
            crea.Cell(審査帳票作成.TEMP_予定色).Attr.BackColor = 審査帳票作成.Color予定;
            
        }

        /// <summary>
        /// 一列分のデータを書き込む
        /// </summary>
        /// <param name="ves"></param>
        /// <param name="shin"></param>
        private void WriteData(ExcelCreator.Xlsx.XlsxCreator crea, MsVessel ves, List<KsShinsa> datalist, ref int index)
        {
            //開始位置を記憶
            int startindex = index;

            #region 船ごとの区切り線を書き込む
            //太線を上に書き込む
            //string ltemp = 審査帳票作成.TEMP_船名 + startindex.ToString();
            //Size line_start = crea.GetVarNamePos(ltemp, 0);

            //ltemp = 審査帳票作成.TEMP_年月終了セル + startindex.ToString();
            //Size line_end = crea.GetVarNamePos(ltemp, 0);

            ////範囲指定で線を描く 
            //crea.Pos(line_start.Width, line_start.Height, line_end.Width, line_end.Height).Attr.LineTop(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);

            //--------------------------------------------------------
            //2021/09/03 m.yoshihara 罫線の位置が正しいか？ 罫線終了位置が問題？
            string ltemp = 審査帳票作成.TEMP_船名 + startindex.ToString();
            Point line_start = crea.GetVarNamePos(ltemp, 0);

            ltemp = 審査帳票作成.TEMP_年月終了セル + startindex.ToString();
            Point line_end = crea.GetVarNamePos(ltemp, 0);

            //範囲指定で線を描く
            crea.Pos(line_start.X, line_start.Y, line_end.X+1, line_end.Y+1).Attr.LineTop(ExcelCreator.BorderStyle.Medium, ExcelCreator.xlColor.Black);
            //----------------------------------------------------------
            
            #endregion

            //船名を書き込む
            string tempname = 審査帳票作成.TEMP_船名 + index.ToString();
            crea.Cell(tempname).Value = ves.VesselName;

            //船名の最初の位置を計算しておく
            Point vesstart = crea.GetVarNamePos(tempname, 0);
            
                        

            //必要審査分だけ書き込む
            foreach (MsShinsa mshin in this.ShinsaList)
            {
                //審査名の書き込み
                tempname = 審査帳票作成.TEMP_審査名 + index.ToString();
                crea.Cell(tempname).Value = mshin.MsShinsaName;
                Point shisa_start = crea.GetVarNamePos(tempname, 0);     //審査の開始位置を取得する

                ////////////////////////////////////////////////////////////
                int kindno = 0;
                //審査項目の書き込み
                foreach (string shinsa_kind in 審査帳票作成.審査項目名)
                {
                    //審査項目名の書きこみ
                    tempname = 審査帳票作成.TEMP_審査項目 +index.ToString();
                    crea.Cell(tempname).Value = shinsa_kind;


                    //開始場所の計算
                    tempname = 審査帳票作成.TEMP_年月開始セル + index.ToString();
                    Point start = crea.GetVarNamePos(tempname, 0);

                    //全審査を書き込む
                    foreach (KsShinsa shinsa in datalist)
                    {
                        //今回の審査に一致したら書き込む
                        if (mshin.MsshinsaID == shinsa.MsShinsaID)
                        {
                            //this.Write審査(crea, shinsa, start);
                            this.Write審査項目(crea, shinsa, start, kindno);
                        }
                    }

                    kindno++;
                    index++;
                }
                ///////////////////////////////////////////////////////////////

                //回りすぎた分を補正する
                index -= 1;
                //審査の終了位置を取得する                
                tempname = 審査帳票作成.TEMP_審査名 + index.ToString();                               
                Point shisa_end = crea.GetVarNamePos(tempname, 0);

                //審査結合 2021/09/03 m.yoshihara 
                //crea.Pos(shisa_start.Width, shisa_start.Height, shisa_end.Width, shisa_end.Height).Attr.MergeCells = true;
                crea.Pos(shisa_start.X, shisa_start.Y, shisa_end.X+1, shisa_end.Y+1).Attr.MergeCells = true;
                index++;
            }

            //必要なセルを結合する
            //船名を書き込む
            tempname = 審査帳票作成.TEMP_船名 + (index-1).ToString();
            Point vesend = crea.GetVarNamePos(tempname, 0);

            //2021/09/03 m.yoshihara
            //crea.Pos(vesstart.Width, vesstart.Height, vesend.Width, vesend.Height).Attr.MergeCells = true;
            crea.Pos(vesstart.X, vesstart.Y, vesend.X+1, vesend.Y+1).Attr.MergeCells = true;

        }



        //書き込み場所、審査、書き込み位置、書き込むもの0→内部、1→審査
        //private void Write審査項目(ExcelCreator.Xlsx.XlsxCreator crea, KsShinsa shin, Size si, int flag)
        private void Write審査項目(ExcelCreator.Xlsx.XlsxCreator crea, KsShinsa shin, Point si, int flag)
        {
            Point start = si;
            Point end = si;
            System.Drawing.Color color = 審査帳票作成.Color予定;
            
            DateTime date = new DateTime();		//書き込み位置日
            DateTime wdate = new DateTime();	//書き込みする文字の日	
            bool jiseki = false;

            //内部審査だった
            if (flag == 0)
            {
                date = shin.NaibuDate;
				wdate = shin.NaibuDate;

                //内部実績登録されてる？
                jiseki = false;
                if (shin.NaibuJisekitourokuUser.Length > 0)
                {
                    jiseki = true;      //実績ok

					//書き込み日を実績枠へ移動
					date = shin.NaibuJisekitourokuDate;
					wdate = shin.NaibuJisekitourokuDate;   //実績日
                }
            }
            //審査だった
            if (flag == 1)
            {
                date = shin.ShinsaDate;
				wdate = shin.ShinsaDate; 

                //実績登録されてる？
                jiseki = false;
                if (shin.ShinsaJisekiTourokuUser.Length > 0)
                {					
                    jiseki = true;          //実績ok

					//書き込み日を実績枠へ移動
					date = shin.ShinsaJisekiTourokuDate;
					wdate = shin.ShinsaJisekiTourokuDate;  //実績日
                }
            }
            

            #region 書き込み開始終了割り出し
            //---------------------------------------------------------------------
            //開始位置横を割り出す
            int sy = date.Year - this.StartYear;
            int xs = (sy * 12) + date.Month;

            //開始位置の前だった
            if (xs < 0)
            {
                xs = 1;
            }
            //最終書き込み位置より大きい場所だった時
            if (xs > (審査帳票作成.YearWriteLenge * 12))
            {
                xs = (審査帳票作成.YearWriteLenge * 12);
            }

            //開始位置を割り出す
            start.X += (xs - 1);

            //-------------------------------------------------------------------
            //終了位置を割り出す
            int ey = date.Year - this.StartYear;
            int xe = (ey * 12) + date.Month;


            //開始位置の前だった
            if (xe < 0)
            {
                xe = 0;
            }

            //最終書き込み位置より大きい場所だった時
            if (xe > (審査帳票作成.YearWriteLenge * 12))
            {
                xe = (審査帳票作成.YearWriteLenge * 12);
            }
            //2021/09/03 m.yoshihara
            //end.Width += (xe - 1);
            end.X += (xe - 1)+1;
            #endregion

            ///////////////////////////////////////////////////////////////////////
            //書き込み
            //文字列作成
            string s = "";
            //s += shin.MsShinsaName;

			//書き込み日を書き込む
			s += wdate.ToString("M/d");

            //実績登録済みなら・・・・
            if (jiseki == true)
            {
                //s += " ";				
                //s += 審査帳票作成.済マーク;

                color = 審査帳票作成.Color実績;
            }


            //開始場所に文字を書き込む 2021/09/03 m.yoshihara
            //crea.Cell("A1", start.Width, start.Height).Value = s;
            crea.Cell("A1", start.X, start.Y).Value = s;

            //終了位置まで色を塗りつぶす 
            for (int i = start.X; i <= end.X; i++)
            {
                crea.Cell("A1", i, start.Y).Attr.BackColor = color;
            }
        }

        //引数：書き込み場所、書き込み審査、開始位置
        private void Write審査(ExcelCreator.Xlsx.XlsxCreator crea, KsShinsa shin, Size si)
        {
            Size start = si;
            Size end = si;
            System.Drawing.Color color = 審査帳票作成.Color予定;

            #region 書き込み開始終了割り出し
            //---------------------------------------------------------------------
            //開始位置横を割り出す
            int sy = shin.NaibuDate.Year - this.StartYear;
            int xs = (sy * 12) + shin.NaibuDate.Month;

            //開始位置の前だった
            if( xs < 0)
            {
                xs = 1;
            }
            //最終書き込み位置より大きい場所だった時
            if (xs > (審査帳票作成.YearWriteLenge * 12))
            {
                xs = (審査帳票作成.YearWriteLenge * 12);
            }

            //開始位置を割り出す
            start.Width += (xs - 1);

            //-------------------------------------------------------------------
            //終了位置を割り出す
            int ey = shin.ShinsaDate.Year - this.StartYear;
            int xe = (ey * 12) + shin.ShinsaDate.Month;


            //開始位置の前だった
            if (xe < 0)
            {
                xe = 0;
            }

            //最終書き込み位置より大きい場所だった時
            if (xe > (審査帳票作成.YearWriteLenge * 12))
            {
                xe = (審査帳票作成.YearWriteLenge * 12);
            }

            end.Width += (xe - 1);
            #endregion

            ///////////////////////////////////////////////////////////////////////
            //書き込み
            //文字列作成
            string s = "";
            //s += shin.MsShinsaName;

            //実績登録済みなら・・・・
            if (shin.ShinsaJisekiTourokuUser.Length > 0)
            {
                //s += " ";
                s += shin.ShinsaJisekiTourokuDate.ToString("M/d");
                //s += 審査帳票作成.済マーク;

                color = 審査帳票作成.Color実績;
            }


            //開始場所に文字を書き込む
            crea.Cell("A1", start.Width, start.Height).Value = s;


            //終了位置まで色を塗りつぶす
            for (int i = start.Width; i <= end.Width; i++)
            {
                crea.Cell("A1", i, start.Height).Attr.BackColor = color;
            }           
        }

        //メンバ変数========================================================

        /// <summary>
        /// データをまとめたもの
        /// Key=VesselID Value=関連審査たち
        /// </summary>
        private Dictionary<int, List<KsShinsa>> DataDic = new Dictionary<int, List<KsShinsa>>();

        //関連船リスト
        private List<MsVessel> VesselList;

        //関連審査リスト
        private List<MsShinsa> ShinsaList;

        /// <summary>
        /// 開始年
        /// </summary>
        private int StartYear = 0;

        /// <summary>
        /// 終了年
        /// </summary>
        private int EndYear = 0;

        //定義--------------------------------------------------------------      
        
        /// <summary>
        /// テンプレートファイル名
        /// </summary>
        private static readonly string TemplateFileName = "Template_審査帳票.xlsx";

        /// <summary>
        /// 基準年から前の出力年
        /// </summary>
        private static readonly int Pre基準年 = -3;

        /// <summary>
        /// 基準年から後の出力年
        /// </summary>
        private static readonly int After基準年 = 2;

        /// <summary>
        /// Start年からの書き込み幅
        /// </summary>
        private static readonly int YearWriteLenge = 6;
        
        /// <summary>
        /// 検査済み文字
        /// </summary>
        private static readonly string 済マーク = "済";

        /// <summary>
        /// 期間を塗りつぶす色
        /// </summary>
        //2021/09/03 m.yoshihara 定数があったがxlColor でなく背景色はsystem.drawing.Colorで指定
        //private static readonly ExcelCreator.xlColor 塗りつぶし色 = ExcelCreator.xlColor.Pink;
        private static readonly System.Drawing.Color 塗りつぶし色 = System.Drawing.Color.Pink;

        //2021/09/03 m.yoshihara 定数があったがxlColor でなく背景色はsystem.drawing.Colorで指定
        //private static readonly ExcelCreator.xlColor Color実績 = ExcelCreator.xlColor.Green;
        //private static readonly ExcelCreator.xlColor Color予定 = ExcelCreator.xlColor.Yellow;
        private static readonly System.Drawing.Color Color実績 = System.Drawing.Color.Green;
        private static readonly System.Drawing.Color Color予定 = System.Drawing.Color.Yellow;


        //審査項目
        private static readonly string[] 審査項目名 = {
                                                     "内部審査",        //0
                                                     "審査"            //1                                      
                                                 };
                                                 

        //テンプレートの参照文字列
        private static readonly string TEMP_年 = "**YEAR";
        private static readonly string TMEP_日付 = "**DATE";
        private static readonly string TEMP_船名 = "**VESSEL_NAME";
        //private static readonly string TEMP_基準日 = "**KIJYUN_DATE";
        private static readonly string TEMP_年月開始セル = "**YEAR0_START";
		private static readonly string TEMP_年月終了セル = "**YEAR_END";

        private static readonly string TEMP_開始列 = "**START";
        private static readonly string TEMP_最終列 = "**LAST";


        private static readonly string TEMP_実績色 = "**JISEKI_COLOR";
        private static readonly string TEMP_予定色 = "**YOTEI_COLOR";

        private static readonly string TEMP_審査名 = "**SHINSA_NAME";
        private static readonly string TEMP_審査項目 = "**SHINSA_KIND";


    }
}