using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBaseData.DAC;
using NBaseData.BLC;
using ExcelCreator = AdvanceSoftware.ExcelCreator;
using NBaseData.DS;
using System.Drawing;


namespace NBaseCommon.Senin.Excel
{
    public class 配乗計画表内航乗下船出力
    {

        private readonly string templateFilePath;
        private readonly string outputFilePath;
        private int PlanType;

        public 配乗計画表内航乗下船出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;

            // この配乗計画のタイプ
            PlanType = MsPlanType.PlanTypeHarfPeriod;
        }

        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, DateTime date, bool isPlan)
        {
            ExcelCreator.Xlsx.XlsxCreator xlsx = new ExcelCreator.Xlsx.XlsxCreator();
            //using (ExcelCreator.Xlsx.XlsxCreator xlsx = new ExcelCreator.Xlsx.XlsxCreator())
            {

                int xlsxret = xlsx.OpenBook(outputFilePath, templateFilePath);
                if (xlsxret < 0)
                {
                    Exception xlsEx = null;
                    if (xlsx.ErrorNo == ExcelCreator.ErrorNo.TempCreate)
                    {
                        xlsEx = new Exception("ディスクに十分な空き領域がありません。");
                    }
                    else
                    {
                        xlsEx = new Exception("サーバでエラーが発生しました。");
                    }
                    throw xlsEx;
                }
                //-----------------------------------------

                _CreateFile(loginUser, seninTableCache, xlsx, isPlan, date);
                
                xlsx.DeleteSheet(0, 1);


                xlsx.CloseBook(true);
            }
            System.Threading.Thread.Sleep(1000);
            xlsx.Dispose();
        }

        //private List<乗下船Data> 乗下船List = new List<乗下船Data>();
        private List<MsVessel> VesselList = new List<MsVessel>();
        private List<SiCardPlanHead> HeadList = new List<SiCardPlanHead>();
        private List<MsBasho> PortList = new List<MsBasho>();

        /// <summary>
        /// 内航の配乗計画表作成
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="xlsx"></param>
        /// <param name="date"></param>
        private void _CreateFile(MsUser loginUser, SeninTableCache seninTableCache, ExcelCreator.Xlsx.XlsxCreator xlsx, bool isPlan, DateTime date)
        {
            //出力データ取得
            //船リスト
            List<MsVessel> vessels = seninTableCache.GetMsVesselList(loginUser);
            VesselList = new List<MsVessel>();
            foreach (MsVessel v in vessels)
            {
                //if (v.Is内航船)
                if (v.IsPlanType(PlanType))
                    VesselList.Add(v);
            }
            //シートコピー時に逆になるので
            if (VesselList.Count > 0)
            {
                VesselList = VesselList.OrderByDescending(obj => obj.VesselNo).ToList();
            }

            //港リスト
            List<MsBasho> ports = MsBasho.GetRecords(loginUser);
            if (ports.Count > 0)
            {
                PortList = ports.Where(obj => obj.GaichiFlag == 0).ToList();
            }

            //
            if (isPlan)
            {
                //ヘッダのリストを取得
                //HeadList = SiCardPlanHead.GetRecordsByYearMonth(loginUser, date, SiCardPlanHead.VESSEL_KIND_内航);
                HeadList = SiCardPlanHead.GetRecordsByYearMonth(loginUser, date, PlanType);
            }
            
            foreach (MsVessel vessel in VesselList)
            {
                //テンプレートコピー
                xlsx.CopySheet(0, 1, vessel.VesselName);

                //コピーしたシートを操作対象にする
                xlsx.SheetNo = 1;

                List<乗下船Data> wklist = null;

                if (isPlan)
                {
                    wklist = GetDataP(loginUser, seninTableCache, date, vessel.MsVesselID);
                }
                else
                {
                    wklist = GetDataA(loginUser, seninTableCache, date, vessel.MsVesselID);
                }
                //交代日順に並び替え
                wklist = wklist.OrderBy(obj => obj.交代日).ThenBy(obj => obj.JosenShimei).ThenBy(obj => obj.PortName).ToList();

                creat(loginUser, seninTableCache, xlsx, isPlan, date, vessel, wklist);
            }

            // 印刷範囲
            //xls.PrintArea(0, 0, 6, startRow + i);
        }


        private void creat(MsUser loginUser, SeninTableCache seninTableCache, ExcelCreator.Xlsx.XlsxCreator xlsx,bool isPlan, DateTime date, MsVessel vessel, List<乗下船Data> vesselごとList)
        {

            List<string> 曜日 = new List<string>() { "日", "月", "火", "水", "木", "金", "土" };


            #region 決まった場所の値をセット
            if (isPlan)
            {
                xlsx.Cell("**TITLE").Value = date.Year + "年" + date.Month + "月乗下船計画表";
            }
            else
            {
                xlsx.Cell("**TITLE").Value = date.Year + "年" + date.Month + "月乗下船実績表";
            }

            #region 改定
            int revno = 0;
            if (HeadList.Count() > 0)
            {
                revno = HeadList.Last().RevNo;
            }
            #endregion

            if (revno > 0)
            {
                xlsx.Cell("**改定").Value = "（改―" + revno.ToString() + "）";
            }
            else
            {
                xlsx.Cell("**改定").Value = "";
            }
            xlsx.Cell("**出力日").Value = DateTime.Today.ToString("yyyy年MM月dd日");

            //船名セット
            xlsx.Cell("**船名").Value = "   "+vessel.VesselName;

            #endregion

            int chartRow_Max = 26;//表に入る行数
            int chartLasRow = 100;//表の最後の行
            

            #region 表のコピー データ数が26より大きい場合、あらかじめ表を用意しておく
            //1シートの中の表の数
            int chartCnt = vesselごとList.Count / chartRow_Max;
            if (vesselごとList.Count % chartRow_Max != 0)
            {
                chartCnt++;
            }

            for (int i = 1; i < chartCnt; i++)
            {
                //表コピー
                string cellstr = "A1:" + "Q" + chartLasRow.ToString().Trim();
                xlsx.Cell(cellstr).Copy();

                //表ペースト
                int pasteR = chartLasRow * i + 1;
                string pasteStr = "A" + pasteR.ToString();
                xlsx.Cell(pasteStr).Paste();

                //ペーストした表の行高さ調整
                for (int r = 1; r < chartLasRow; r++)
                {
                    string cellStr = r.ToString();

                    int row = r + chartLasRow * i;
                    string cellStr2 = row.ToString();
                    double h = xlsx.Cell(cellStr).RowHeight;//一度変数に入れないとHeightが変わらない
                    xlsx.Cell(cellStr2).RowHeight = h;
                }
            }

            #endregion


            int dt_cnt = 1;
            int next = 0;
            int startRow = 6;//データの始まり位置。
            int posy = startRow-1;//0はじまりなので1引く

            foreach (乗下船Data rec in vesselごとList)
            {  
                int posx = 2;
                xlsx.Pos(posx, posy).Value = rec.JosenShokumei; posx++;
                xlsx.Pos(posx, posy).Value = rec.JosenShimei; posx++; posx++; posx++; posx++;//乗船通知列(3カラム分)とばす
                xlsx.Pos(posx, posy).Value = rec.PortName; posx++;
                string datestr = rec.交代日.Day.ToString() + "日(" + 曜日[(int)rec.交代日.DayOfWeek] + ")";
                xlsx.Pos(posx, posy).Value = datestr; posx++;
                xlsx.Pos(posx, posy).Value = rec.GesenShokumei; posx++;
                xlsx.Pos(posx, posy).Value = rec.GesenShimei;

                if (dt_cnt % chartRow_Max == 0)
                {
                    //次の表
                    next = next + chartLasRow;
                    posy = (next + startRow)-1;//0はじまりなので1引く
                }
                else
                {
                    posy++;
                }
                dt_cnt++;
            }
            
        }

        /// <summary>
        /// 計画データ取得
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="seninTableCache"></param>
        /// <param name="date"></param>
        /// <param name="msvesselID"></param>
        /// <returns></returns>
        private List<乗下船Data> GetDataP(MsUser loginUser, SeninTableCache seninTableCache, DateTime date, int msvesselID)
        { 
            //List<SiCardPlan> plans = 配乗計画.BLC_配乗計画_SearchPlan(loginUser, seninTableCache, date, date.AddMonths(1), SiCardPlanHead.VESSEL_KIND_内航);
            List<SiCardPlan> plans = 配乗計画.BLC_配乗計画_SearchPlan(loginUser, seninTableCache, date, date.AddMonths(1), PlanType);

            if (plans.Count > 0)
            {
                plans = plans.Where(obj => obj.MsVesselID == msvesselID).ToList();
                plans = plans.OrderBy(obj => obj.StartDate).ToList();
            }

            //乗船リスト作成
            List<乗下船Data> wklist乗船 = new List<乗下船Data>();
            foreach (SiCardPlan p in plans)
            {
                //月内で乗下船がない場合はとばす
                if (p.StartDate < date && p.EndDate > date.AddMonths(1)) continue;

                //月内で乗船した人取得する
                if (p.StartDate >= date && p.StartDate < date.AddMonths(1))
                {
                    乗下船Data rec = new 乗下船Data();
                    乗下船Data rec2 = null;//交代があった場合にだぶり日数があった時のレコード
                    rec.SiCardPlanID = p.SiCardPlanID;
                    rec.交代日 = p.StartDate;
                    MsSenin jousensenin = MsSenin.GetRecord(loginUser, p.MsSeninID);
                    string jousenshokumei = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, jousensenin.MsSiShokumeiID);
                    rec.JosenShimei = jousensenin.FullName;
                    rec.JosenShokumei = jousenshokumei;

                    //降りた人がいるか調べる
                    SiCardPlan gesencard = null;
                    gesencard = SiCardPlan.GetRecordParent(loginUser, p.SiCardPlanID);

                    if (gesencard == null)
                    {
                        //いない
                        rec.GesenShimei = "";
                        rec.GesenShokumei = "";
                    }
                    else
                    {
                        //降りた人
                        MsSenin gesensenin = MsSenin.GetRecord(loginUser, gesencard.MsSeninID);
                        string gesenshokumei = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, gesensenin.MsSiShokumeiID);
                        string portname = Get港名(gesencard.MsBashoID);

                        rec.GesenShimei = gesensenin.FullName;
                        rec.GesenShokumei = gesenshokumei;
                        rec.PortName = portname;

                        DateTime enddate補正 = gesencard.EndDate.AddDays(1);

                        if (p.StartDate == enddate補正)
                        {
                            ;
                        }
                        else
                        {
                            rec2 = new 乗下船Data();

                            if (p.StartDate < enddate補正)
                            {
                                rec2.JosenShimei = jousensenin.FullName;
                                rec2.JosenShokumei = jousenshokumei;
                                rec2.交代日 = p.StartDate;

                                rec.交代日 = gesencard.EndDate;
                            }
                            else if (p.StartDate > enddate補正)
                            {
                                rec2.GesenShimei = gesensenin.FullName;
                                rec2.GesenShokumei = gesenshokumei;
                                rec2.交代日 = gesencard.EndDate;

                            }
                        }
                    }
                    wklist乗船.Add(rec);
                    if (rec2 != null)
                    {
                        wklist乗船.Add(rec2);
                    }
                }
            }

            List<乗下船Data> wklist下船 = new List<乗下船Data>();
            foreach (SiCardPlan p in plans)
            {
                //月内で乗下船がない場合はとばす
                if (p.StartDate < date && p.EndDate > date.AddMonths(1)) continue;

                //月内で下船だけしたひと
                if (p.StartDate < date && p.EndDate >= date && p.EndDate < date.AddMonths(1))
                {
                    //降りた人
                    MsSenin gesensenin = MsSenin.GetRecord(loginUser, p.MsSeninID);
                    string shokumei = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, gesensenin.MsSiShokumeiID);
                    string portname = Get港名(p.MsBashoID);

                    if (p.Replacement == null || p.Replacement == "")//交代者いなくてただ降りる人
                    {
                        乗下船Data rec = new 乗下船Data();

                        rec.GesenShimei = gesensenin.FullName;
                        rec.GesenShokumei = shokumei;
                        rec.PortName = portname;
                        rec.交代日 = p.EndDate;

                        wklist下船.Add(rec);
                    }
                }

            }

            #region デバッグ表示
            //System.Diagnostics.Debug.WriteLine("乗船");

            //foreach (乗下船Data data in wklist乗船)
            //{
            //    string str = data.JosenShokumei + ":" + data.JosenShimei + "/" + data.PortName + "/" + data.交代日.Day.ToString() + "    " + data.GesenShokumei + ":" + data.GesenShimei;
            //    System.Diagnostics.Debug.WriteLine(str);

            //}

            //System.Diagnostics.Debug.WriteLine("下船");
            //foreach (乗下船Data data in wklist下船)
            //{
            //    string str = data.JosenShokumei + ":" + data.JosenShimei + "/" + data.PortName + "/" + data.交代日.Day.ToString() + "    " + data.GesenShokumei + ":" + data.GesenShimei;
            //    System.Diagnostics.Debug.WriteLine(str);

            //}
            #endregion

            List<乗下船Data> 乗下船List = new List<乗下船Data>();

            //乗船と下船を合わせてリスト作成
            乗下船List = wklist乗船.Concat(wklist下船).ToList();

            return 乗下船List;

        }

        /// <summary>
        /// 実績データ取得
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="seninTableCache"></param>
        /// <param name="date"></param>
        private List<乗下船Data> GetDataA(MsUser loginUser, SeninTableCache seninTableCache, DateTime date, int msvesselID)
        {
            SiCardFilter filter = new SiCardFilter();
            filter.Start = date;
            filter.End = date.AddMonths(1);

            List<SiCard> actives = null;
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船));

            filter.MsVesselIDs.Add(msvesselID);

            actives = NBaseData.BLC.船員.BLC_船員カード検索(loginUser, seninTableCache, filter);

            if (actives.Count > 0)
            {
                actives = actives.OrderBy(obj => obj.StartDate).ToList();
            }

            //乗船リスト作成
            List<乗下船Data> wklist乗船 = new List<乗下船Data>();

            foreach (SiCard a in actives)
            {
                //月内で乗下船がない場合はとばす
                if (a.StartDate < date && a.EndDate > date.AddMonths(1)) continue;

                //月内で乗船した人取得する
                if (a.StartDate >= date && a.StartDate < date.AddMonths(1))
                {
                    乗下船Data rec = new 乗下船Data();
                    乗下船Data rec2 = null;//交代があった場合にだぶり日数があった時のレコード
                    rec.SiCardPlanID = a.SiCardID;
                    rec.交代日 = a.StartDate;
                    MsSenin jousensenin = MsSenin.GetRecord(loginUser, a.MsSeninID);
                    string jousenshokumei = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, jousensenin.MsSiShokumeiID);
                    rec.JosenShimei = jousensenin.FullName;
                    rec.JosenShokumei = jousenshokumei;

                    //降りた人がいるか調べる
                    SiCard gesencard = null;
                    gesencard = SiCard.GetRecordParent(loginUser, a.SiCardID);

                    if (gesencard == null)
                    {
                        //いない
                        rec.GesenShimei = "";
                        rec.GesenShokumei = "";
                    }
                    else
                    {
                        //降りた人
                        MsSenin gesensenin = MsSenin.GetRecord(loginUser, gesencard.MsSeninID);
                        string gesenshokumei = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, gesensenin.MsSiShokumeiID);
                        string portname = Get港名(gesencard.SignOffBashoID);

                        rec.GesenShimei = gesensenin.FullName;
                        rec.GesenShokumei = gesenshokumei;
                        rec.PortName = portname;

                        DateTime enddate補正 = gesencard.EndDate.AddDays(1);

                        if (a.StartDate == enddate補正)
                        {
                            ;
                        }
                        else
                        {
                            rec2 = new 乗下船Data();

                            if (a.StartDate < enddate補正)
                            {
                                rec2.JosenShimei = jousensenin.FullName;
                                rec2.JosenShokumei = jousenshokumei;
                                rec2.交代日 = a.StartDate;

                                rec.交代日 = gesencard.EndDate;
                            }
                            else if (a.StartDate > enddate補正)
                            {
                                rec2.GesenShimei = gesensenin.FullName;
                                rec2.GesenShokumei = gesenshokumei;
                                rec2.交代日 = gesencard.EndDate;

                            }
                        }
                    }
                    wklist乗船.Add(rec);
                    if (rec2 != null)
                    {
                        wklist乗船.Add(rec2);
                    }
                }
            }

            List<乗下船Data> wklist下船 = new List<乗下船Data>();
            foreach (SiCard a in actives)
            {
                //月内で乗下船がない場合はとばす
                if (a.StartDate < date && a.EndDate > date.AddMonths(1)) continue;

                //月内で下船だけしたひと
                if (a.StartDate < date && a.EndDate >= date && a.EndDate < date.AddMonths(1))
                {
                    //降りた人
                    MsSenin gesensenin = MsSenin.GetRecord(loginUser, a.MsSeninID);
                    string shokumei = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, gesensenin.MsSiShokumeiID);
                    string portname = Get港名(a.SignOffBashoID);

                    if (a.ReplacementID == null || a.ReplacementID == "")//交代者いなくてただ降りる人
                    {
                        乗下船Data rec = new 乗下船Data();

                        rec.GesenShimei = gesensenin.FullName;
                        rec.GesenShokumei = shokumei;
                        rec.PortName = portname;
                        rec.交代日 = a.EndDate;

                        wklist下船.Add(rec);
                    }
                }

            }

            #region デバッグ表示
            //System.Diagnostics.Debug.WriteLine("乗船");

            //foreach (乗下船Data data in wklist乗船)
            //{
            //    string str = data.JosenShokumei + ":" + data.JosenShimei + "/" + data.PortName + "/" + data.交代日.Day.ToString() + "    " + data.GesenShokumei + ":" + data.GesenShimei;
            //    System.Diagnostics.Debug.WriteLine(str);

            //}

            //System.Diagnostics.Debug.WriteLine("下船");
            //foreach (乗下船Data data in wklist下船)
            //{
            //    string str = data.JosenShokumei + ":" + data.JosenShimei + "/" + data.PortName + "/" + data.交代日.Day.ToString() + "    " + data.GesenShokumei + ":" + data.GesenShimei;
            //    System.Diagnostics.Debug.WriteLine(str);

            //}
            #endregion

            List<乗下船Data> 乗下船List = new List<乗下船Data>();

            //乗船と下船を合わせてリスト作成
            乗下船List = wklist乗船.Concat(wklist下船).ToList();

            return 乗下船List;

        }

        /// <summary>
        /// 港名取得
        /// </summary>
        /// <param name="bashoid"></param>
        /// <returns></returns>
        private string Get港名(string bashoid)
        {
            string ret = "";

            MsBasho basho = PortList.Where(obj => obj.MsBashoId == bashoid).FirstOrDefault();

            if (basho != null)
            {
                ret = basho.BashoName;
            }
            return ret;
        }
    }

    public class 乗下船Data
    {
        public string JosenShokumei;
        public string JosenShimei;
        public string GesenShokumei;
        public string GesenShimei;
        public string PortName;
        public DateTime 交代日;
        public string SiCardPlanID;

        public 乗下船Data()
        {
            JosenShokumei = "";
            JosenShimei = "";
            GesenShokumei = "";
            GesenShimei = "";
            PortName = "";
            交代日 = DateTime.MinValue;
            SiCardPlanID = "";

        }

        public override bool Equals(object obj)
        {
            var d1 = obj as 乗下船Data;
            if (d1 == null)
                return false;

            int flg = 0;

            if (JosenShokumei != d1.JosenShokumei) flg = 1;
            if (JosenShimei != d1.JosenShimei) flg = 1;
            if (GesenShokumei != d1.GesenShokumei) flg = 1;
            if (GesenShimei != d1.GesenShimei) flg = 1;
            if (PortName != d1.PortName) flg = 1;
            if (交代日 != d1.交代日) flg = 1;
            if (SiCardPlanID != d1.SiCardPlanID) flg = 1;

            if (flg == 1) return false;
            return true;

        }
        public override int GetHashCode()
    => JosenShokumei.GetHashCode() ^ JosenShimei.GetHashCode() ^ GesenShokumei.GetHashCode() ^ GesenShimei.GetHashCode() ^ PortName.GetHashCode() ^ 交代日.GetHashCode() ^ SiCardPlanID.GetHashCode();

    }
}
