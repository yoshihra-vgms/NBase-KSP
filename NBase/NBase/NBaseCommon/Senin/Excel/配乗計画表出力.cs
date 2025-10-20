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
    public class 配乗計画表出力
    {

        private readonly string templateFilePath;
        private readonly string outputFilePath;
        private int PlanType;

        public 配乗計画表出力(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;

            // この配乗表のタイプ
            PlanType = MsPlanType.PlanTypeOneMonth;
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

        private Dictionary<string, List<MsSenin>> SeninTbl = new Dictionary<string, List<MsSenin>>();
        private Dictionary<DateTime, string> CalenderPosTbl = new Dictionary<DateTime, string>();
        private List<SiCardPlanHead> HeadList = new List<SiCardPlanHead>();

        private List<Appointment> AppointmentList = new List<Appointment>();

        private List<DateTime> CaList = new List<DateTime>();
        private List<MsSiShokumei> ShokumeiList = new List<MsSiShokumei>();
        private List<MsVessel> VesselList = new List<MsVessel>();

        private TimeSpan 半日時間 = new TimeSpan(12, 0, 0);

        private Color EtcColor = Color.FromArgb(137, 137, 137);//グレー
        private List<ColorLinkSyubetsu> ColorSyubeList = new List<ColorLinkSyubetsu>();

        private List<Point> Position凡例List = new List<Point>();


        private void MakeColorList( MsUser loginUser, SeninTableCache seninTableCache)
        {
            //ColorTbl.Add(SyubeVesselID1, Color.FromArgb(254, 70, 71));//レッド
            //ColorTbl.Add(SyubeVesselID2, Color.FromArgb(189, 133, 214));//紫
            //ColorTbl.Add(SyubeVesselID3, Color.FromArgb(0, 128, 255));//ブルー

            //乗船
            foreach (MsVessel vsl in VesselList) 
            {
                ColorLinkSyubetsu cs = new ColorLinkSyubetsu();
                cs.MsSiShubetuID = seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船);
                cs.MsVesselID = vsl.MsVesselID;
                cs.LineColor = Color.FromArgb(vsl.R, vsl.G, vsl.B);
                ColorSyubeList.Add(cs);
            }

            //有給休暇
            {
                ColorLinkSyubetsu cs = new ColorLinkSyubetsu();
                cs.MsSiShubetuID = seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇);
                cs.MsVesselID = 0;
                cs.LineColor = Color.FromArgb(0, 0, 0);
                ColorSyubeList.Add(cs);
            }

            //傷病
            {
                ColorLinkSyubetsu cs = new ColorLinkSyubetsu();
                cs.MsSiShubetuID = seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.傷病);
                cs.MsVesselID = 0;
                cs.LineColor = Color.FromArgb(0, 0, 0);
                ColorSyubeList.Add(cs);
            }

            //ColorTbl.Add(SyubeID有給休暇, Color.FromArgb(0, 0, 0));//黒
            //ColorTbl.Add(SyubeID傷病, Color.FromArgb(0, 0, 0));//黒

            //ColorTbl.Add(SyubeID有給休暇, Color.FromArgb(0, 147, 112));//緑1
            //ColorTbl.Add(SyubeID傷病, Color.FromArgb(67, 161, 47));//緑2

        }

        private void Make凡例(MsUser loginUser, SeninTableCache seninTableCache, ExcelCreator.Xlsx.XlsxCreator xlsx)
        {
            
            for (int i = 0; i < Position凡例List.Count; i++)
            {
                Point p = Position凡例List[i];

                if (i > VesselList.Count - 1)
                {
                    xlsx.Pos(p.X, p.Y).Value = "";
                }
                else
                {
                    //xlsx.Pos(p.X, p.Y).Value = VesselList[i].VesselName;
                    string cellstr = "**VESSEL" + i.ToString().Trim();
                    xlsx.Cell(cellstr).Value = VesselList[i].VesselName;
                    xlsx.Pos(p.X - 2, p.Y+1, p.X - 1, p.Y + 1).Attr.BackColor = GetColor(loginUser, seninTableCache, seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船), VesselList[i].MsVesselID);
                }
            }
        }

        /// <summary>
        /// フェリーの配乗表作成
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="xlsx"></param>
        /// <param name="date"></param>
        private void _CreateFile(MsUser loginUser, SeninTableCache seninTableCache, ExcelCreator.Xlsx.XlsxCreator xlsx, bool isPlan, DateTime date)
        {
            //凡例の場所
            for (int i = 0; i < 3; i++)
            {
                string cellstr = "**VESSEL" + i.ToString().Trim();
                Point p = xlsx.GetVarNamePos(cellstr, 0);
                Position凡例List.Add(p);
            }

            //出力データ取得
            //船リスト
            List<MsVessel> vessels = seninTableCache.GetMsVesselList(loginUser);
            VesselList = new List<MsVessel>();
            foreach (MsVessel v in vessels)
            {
                if (v.IsPlanType(PlanType)) VesselList.Add(v);
            }

            //色作成
            MakeColorList(loginUser, seninTableCache);

            //凡例作成
            Make凡例(loginUser, seninTableCache, xlsx);

            //職名リスト
            ShokumeiList = seninTableCache.GetMsSiShokumeiList(loginUser);
            
            //船員検索
            SeninTbl = new Dictionary<string, List<MsSenin>>();
            List<MsSenin> seninList = MsSenin.GetRecords(loginUser);

            
            //職名グループ作成  
            Dictionary<int, List<int>> shokuGroupTbl = new Dictionary<int, List<int>>();
            foreach (MsSiShokumei shoku in ShokumeiList)
            {
                if (shoku.Department >= 0)
                {
                    if (!shokuGroupTbl.ContainsKey(shoku.Department))
                    {
                        shokuGroupTbl.Add(shoku.Department, new List<int>());
                    }
                    shokuGroupTbl[shoku.Department].Add(shoku.MsSiShokumeiID);
                }
            }

            List<int> seninIDs = new List<int>();
            seninIDs.AddRange(seninList.Select(o => o.MsSeninID));

            //計画か実績か
            if (isPlan)
            {
                //ヘッダのリストを取得
                //HeadList = SiCardPlanHead.GetRecordsByYearMonth(loginUser, date, SiCardPlanHead.VESSEL_KIND_フェリー);
                HeadList = SiCardPlanHead.GetRecordsByYearMonth(loginUser, date, PlanType);

                GetDataP(loginUser, seninTableCache, date);    
            }
            else
            {
                GetDataA(loginUser, seninTableCache, date);
               
            }

            List<MsSenin> SeninViewList = new List<MsSenin>();//表示船員リストクリア

            foreach (int seninId in seninIDs)
            {
                //// 船員の所属が「旅客」の船員を対象とする
                //// ただし、計画や実績がある場合には、他所属でも対象とする
                //if (seninList.Any(o => o.MsSeninID == seninId) &&
                //(seninList.Any(o => o.MsSeninID == seninId && o.Department == 船員.船員_所属_旅客) ||
                //AppointmentList.Any(o => o.MsSeninID == seninId && o.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船))
                //)
                //)
                //{
                //    SeninViewList.Add(seninList.Where(o => o.MsSeninID == seninId).First());
                //}

                // 所属がないので基本的に全船員が対象
                SeninViewList.Add(seninList.Where(o => o.MsSeninID == seninId).First());
            }

            foreach (int dep in shokuGroupTbl.Keys)
            {
                List<MsSenin> slist = new List<MsSenin>();
                foreach (int shokuID in shokuGroupTbl[dep])
                {
                    List<MsSenin> list = new List<MsSenin>();
                    list = SeninViewList.Where(obj => obj.MsSiShokumeiID == shokuID).ToList();
                    if (list.Count > 0)
                    {
                        slist.AddRange(list);
                    }
                }

                //職名グループごとにわける
                string wk = GetShokumeiGroup(dep);
                SeninTbl.Add(wk, slist);
                
            }
            
            #region カレンダー、日付とポジション
            //カレンダー
            List<string> caPos_col_List = new List<string>() { "C", "E", "G", "I", "K", "M", "O", "Q", "S", "U", "W", "Y", "AA", "AC", "AE", "AG", "AI", "AK", "AM", "AO", "AQ", "AS", "AU", "AW", "AY", "BA", "BC", "BE", "BG", "BI", "BK" };

            //カレンダー作成
            CaList = new List<DateTime>();
            int lastday = DateTime.DaysInMonth(date.Year, date.Month);

            for (int i = 0; i < lastday; i++)
            {
                DateTime wkdt = new DateTime(date.Year, date.Month, i + 1);

                CalenderPosTbl.Add(wkdt, caPos_col_List[i]);
            }

            #endregion

            List<string> ordergroups = new List<string>();

            ordergroups.Add(GetShokumeiGroup(4));
            ordergroups.Add(GetShokumeiGroup(1));
            ordergroups.Add(GetShokumeiGroup(2));
            ordergroups.Add(GetShokumeiGroup(0));
            
            foreach (string sname in ordergroups)
            {
  
                //テンプレートコピー
                xlsx.CopySheet(0, 1, sname);

                //コピーしたシートを操作対象にする
                xlsx.SheetNo = 1;

                //
                creat(loginUser, seninTableCache, xlsx,isPlan, date, sname, SeninTbl[sname]);

                
            }

            // 印刷範囲
            //xls.PrintArea(0, 0, 6, startRow + i);
        }

        private string GetShokumeiGroup( int department)
        {
            string ret = "";
            switch(department)
            { 
                case 0:
                    ret = "甲板部（職員）";
                    break;
                case 1:
                    ret = "機関部";
                    break;
                case 2:
                    ret = "甲板部（部員）";
                    break;
                case 4:
                    ret = "事務部";
                    break;
            }
            return ret;
        }

        private void creat(MsUser loginUser, SeninTableCache seninTableCache, ExcelCreator.Xlsx.XlsxCreator xlsx,bool isPlan, DateTime date, string title, List<MsSenin> seninList)
        {
            int caPos_row1 = 12;//日付の行 上段
            int caPos_row2 = 74;//日付の行 下段

            List<string> 曜日 = new List<string>() { "日", "月", "火", "水", "木", "金", "土" };


            #region 日付曜日を入れる
            foreach (DateTime dt in CalenderPosTbl.Keys)
            {
                //上段
                string wkstr1 = CalenderPosTbl[dt] + caPos_row1.ToString();
                int y = (int)dt.DayOfWeek;
                xlsx.Cell(wkstr1).Value = 曜日[y];
                
                string wkstr2 = CalenderPosTbl[dt] + (caPos_row1 + 1).ToString();
                xlsx.Cell(wkstr2).Value = dt.Day;

                //下段
                string wkstr3 = CalenderPosTbl[dt] + caPos_row2.ToString();
                xlsx.Cell(wkstr3).Value = 曜日[y];

                string wkstr4 = CalenderPosTbl[dt] + (caPos_row2 + 1).ToString();
                xlsx.Cell(wkstr4).Value = dt.Day;

                //土日の色
                Color fcolor = Color.Black;
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                {
                    fcolor = Color.Blue;
                }
                else if (dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    fcolor =Color.Red;
                }
                xlsx.Cell(wkstr1).Attr.FontColor = fcolor;
                xlsx.Cell(wkstr2).Attr.FontColor = fcolor;
                xlsx.Cell(wkstr3).Attr.FontColor = fcolor;
                xlsx.Cell(wkstr4).Attr.FontColor = fcolor;
            }
            #endregion

            #region 決まった場所の値をセット
            xlsx.Cell("**YEAR").Value = date.Year;
            xlsx.Cell("**MONTH").Value = date.Month+"月";
            if (isPlan)
            {
                xlsx.Cell("**AMARK").Value = "";
                xlsx.Cell("**PMARK").Value = "○";
            }
            else
            {
                xlsx.Cell("**AMARK").Value = "○";
                xlsx.Cell("**PMARK").Value = "";
            }

            xlsx.Cell("**TITLE").Value =title +  " 乗 下 船 計 画 表";

            #region 更新日
            if (HeadList.Count() > 0)
            {
                xlsx.Cell("**UPDATE0").Value = HeadList[0].RenewDate.ToString("MM月dd日");
                int hnum = HeadList.Count;
                int st = 1;
                if (hnum > 6)
                {
                    st = (hnum - 6) + 1;
                }
                for (int i = 1; i < 7; i++)
                {
                    if (st == hnum) break;
                    xlsx.Cell("**UPDATE"+i.ToString()).Value = HeadList[st].RenewDate.ToString("MM月dd日");
                    st++;
                }
            }
            #endregion

            #endregion

            int chartSeninRow_Max = 20;//表に入る船員数
            //int chartLasRow = 100;//表の最後の行
            Point chartLasPoint = xlsx.GetMaxArea();// データ、属性が設定されたセルの最大行と最大列の交点座標を取得

            #region 表のコピー　船員数多かった時
            //1シートの中の表の数
            int chartCnt = seninList.Count / chartSeninRow_Max;
            if (seninList.Count % chartSeninRow_Max != 0)
            {
                chartCnt++;
            }

            for (int i = 1; i < chartCnt; i++)
            {
                ////表コピー
                //string cellstr = "A1:" + "BL" + chartLasRow.ToString().Trim();
                //xlsx.Cell(cellstr).Copy();

                ////表ペースト
                //int pasteR = chartLasRow * i + 1;
                //string pasteStr = "A" + pasteR.ToString();
                //xlsx.Cell(pasteStr).Paste();

                ////ペーストした表の行高さ調整
                //for (int r = 1; r < chartLasRow; r++)
                //{
                //    string cellStr =  r.ToString();

                //    int row = r + chartLasRow*i;
                //    string cellStr2 =  row.ToString();
                //    double h= xlsx.Cell(cellStr).RowHeight;//一度変数に入れないとHeightが変わらない
                //    xlsx.Cell(cellStr2).RowHeight = h;
                //}
                //表コピー元
                xlsx.Pos(0, 0, chartLasPoint.X, chartLasPoint.Y).Copy();

                //表ペースト
                Point pasteP = new Point(0, (chartLasPoint.Y + 1) * i);
                xlsx.Pos(0, pasteP.Y).Paste();

                //ペーストした表の行高さ調整
                for (int r = 0; r < chartLasPoint.Y; r++)
                {
                    double h = xlsx.Pos(0, r).RowHeight;//一度変数に入れないとHeightが変わらない
                    xlsx.Pos(0, r + pasteP.Y).RowHeight = h;
                }
            }

            // 印刷範囲
            Point prPoint = xlsx.GetMaxArea();// データ、属性が設定されたセルの最大行と最大列の交点座標を取得
            xlsx.PrintArea(0, 0, prPoint.X, prPoint.Y);
            for (int i = 0; i < chartCnt; i++)
            {
                //改ページ
                Point brP = new Point(0, (chartLasPoint.Y + 1) * i);
                xlsx.Pos(brP.X, brP.Y).Break = true;
            }
            #endregion

            #region 船員とポジションの関係テーブル作成
            Dictionary<MsSenin, int> seninPosTbl = new Dictionary<MsSenin, int>();

            int s_cnt = 1;
            int next = 0;
            int seninrow = 14;//14がはじめの行
            foreach (MsSenin senin in seninList)
            {
                seninPosTbl.Add(senin, seninrow);

                if (s_cnt % chartSeninRow_Max == 0)
                {
                    //次の表
                    //next = next + chartLasRow;
                    next = next + (chartLasPoint.Y + 1);
                    seninrow = next + 14;
                }
                else
                {
                    seninrow = seninrow + 3;
                }
                s_cnt++;
                

                //System.Diagnostics.Debug.WriteLine("row=" + seninrow.ToString());
            }
            #endregion

            //
            int cnt = 0;
            foreach (MsSenin key in seninPosTbl.Keys)
            {
                //System.Diagnostics.Debug.WriteLine("だれ=" + key.Sei + " "+ key.Mei);


                string wkstr1 = "A" + seninPosTbl[key].ToString();
                xlsx.Cell(wkstr1).Value = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, key.MsSiShokumeiID);

                string wkstr2 = "B"+seninPosTbl[key].ToString();
                xlsx.Cell(wkstr2).Value = key.Sei + " " + key.Mei;


                List<Appointment> list = AppointmentList.Where(obj => obj.MsSeninID == key.MsSeninID).OrderBy(obj => obj.StartDate).ToList();
                if (list == null || list.Count == 0) continue;

                foreach (Appointment plan in list)
                {
                    ////種別
                    //int syubeNumber = GetColor(loginUser, seninTableCache, plan.MsSiShubetsuID, plan.MsVesselID);

                    //職名セット
                    string shokumei = "";
                    if (plan.MsSiShubetsuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船))
                    {
                        Shokumei shoku = seninTableCache.GetShokumei(loginUser, Shokumei.フェリー, plan.MsSiShokumeiID, plan.MsSiShokumeiShousaiID);

                        if (shoku != null)
                        {
                            shokumei = shoku.NameEng;
                        }
                    }
                    int change = 0;
                    if (plan.MsSiShokumeiID != key.MsSiShokumeiID)
                    {
                        change = 1;
                    }
                    //if (syubeNumber == SyubeID有給休暇 || syubeNumber == SyubeID傷病)
                    //{
                    //    shokumei = "";
                    //}

                    //日にちからカラムを取得
                    //strat
                    DateTime calendarStartDate = CalenderPosTbl.Keys.First();
                    DateTime planStartDate = plan.StartDate;
                    if( plan.StartDate< calendarStartDate)
                    {
                        planStartDate = calendarStartDate;
                    }
                    DateTime wkdate = planStartDate.Date;
                    string colindex = CalenderPosTbl[wkdate];

                    //end
                    DateTime calendarEndDate = CalenderPosTbl.Keys.Last();
                    DateTime planEndDate = plan.EndDate;
                    if (plan.EndDate > calendarEndDate)//午後の場合もあるので、日付が次の日以上が対象
                    {
                        planEndDate = calendarEndDate;
                    }

                    int span = GetSpan(planStartDate, planEndDate);
                    if (plan.PmStart == 1)
                    {
                        span = span - 1;
                    }
                    if (plan.PmEnd == 1)
                    {
                        span = span - 1;
                    }

                    //Appointmentを置く
                    PutAppointment(loginUser, seninTableCache, xlsx, seninPosTbl[key], colindex, plan.PmStart, span, plan.MsSiShubetsuID, plan.MsVesselID, shokumei, change);

                }
                cnt++;
            }

        }

        private void PutAppointment(MsUser loginUser, SeninTableCache seninTableCache, ExcelCreator.Xlsx.XlsxCreator xlsx, int rowindex, string colindex, int frompm, int span, int shubeID, int vslID, string shokumei, int change)
        {
            string cellstr = "";

            //A1形式で表現
            if (shokumei != "")
            {
                cellstr = colindex + rowindex.ToString();
                xlsx.Cell(cellstr, frompm, 0).Value = shokumei;
                if (change == 1)
                {
                    xlsx.Cell(cellstr, frompm, 0).Attr.BackColor = Color.FromArgb(0,255,0);
                    xlsx.Cell(cellstr, frompm+1, 0).Attr.BackColor = Color.FromArgb(0, 255, 0);
                }

            }
            //System.Diagnostics.Debug.WriteLine("職名="+ shokumei + " " +"pos=" + cellstr);

            //色付ける行はひとつ下の行
            int cellindex = rowindex + 1;

            //A1形式で表現
            cellstr = colindex + cellindex.ToString();

            //frompmなら1カラムずらす
            for (int i = 0; i < span; i++)
            {
                Color color = GetColor(loginUser, seninTableCache, shubeID, vslID);

                int cx = frompm + i;
                if (shubeID==seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船))
                {
                    xlsx.Cell(cellstr, cx, 0).Attr.BackColor = color;
                }
                else if (shubeID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇))
                {
                    xlsx.Cell(cellstr, cx, 0).Attr.LineBottom( ExcelCreator.BorderStyle.MediumDashDot, color);
                    //System.Diagnostics.Debug.WriteLine("有給 cellstr(" + cellstr + ") cx=(" + cx.ToString()+ ")");
                }
                else if (shubeID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.傷病))
                {
                    xlsx.Cell(cellstr, cx, 0).Attr.LineBottom(ExcelCreator.BorderStyle.Dashed, color);
                }
                
            }

        }

        /// <summary>
        /// 色を取得
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="seninTableCache"></param>
        /// <param name="shubetuID"></param>
        /// <param name="vesselID"></param>
        /// <returns></returns>
        public Color GetColor(MsUser loginUser, SeninTableCache seninTableCache, int shubetuID, int vesselID)
        {
            Color ret = EtcColor;

            foreach (ColorLinkSyubetsu cs in ColorSyubeList)
            {
                if (shubetuID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船) && cs.MsSiShubetuID == shubetuID && cs.MsVesselID == vesselID)
                {
                    ret = cs.LineColor;
                    break;
                }
                else if (shubetuID != seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船) && shubetuID == cs.MsSiShubetuID) 
                {
                    ret = cs.LineColor;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// 期間(日付)から何マスかを求める
        /// </summary>
        /// <param name="stdt"></param>
        /// <param name="stpm"></param>
        /// <param name="eddt"></param>
        /// <param name="edpm"></param>
        /// <returns></returns>
        #region public int GetSpan(DateTime stdt, DateTime eddt)
        public int GetSpan(DateTime stdt, DateTime eddt)
        {
            DateTime wkenddt = eddt.AddDays(1);
            TimeSpan tspan = wkenddt - stdt;

            int i = 0;
            while (tspan != new TimeSpan(0, 0, 0))
            {
                tspan = tspan - NBaseCommon.配乗計画Func.半日時間;
                i++;
            }

            return i;
        }
        #endregion

        /// <summary>
        /// (実績の場合)日付+半日以上の時間なら繰り上げる 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        #region private DateTime Get乗船計画終了日(DateTime dt)
        private DateTime Get乗船計画終了日(DateTime dt)
        {
            DateTime ret = DateTime.MinValue;

            DateTime 日付のみ = dt.Date;
            TimeSpan 時刻 = dt.TimeOfDay;

            if (時刻 > 半日時間)
            {
                ret = 日付のみ.AddDays(1);
            }
            else
            {
                ret = 日付のみ;
            }
            return ret;
        }
        #endregion

        private void GetDataP(MsUser loginUser, SeninTableCache seninTableCache, DateTime date)
        {

            //前の月の計画ヘッダ検索
            //List<SiCardPlanHead> prevheads = SiCardPlanHead.GetRecordsByYearMonth(loginUser, date.AddMonths(-1));
            //SiCardPlanHead prevhead = new SiCardPlanHead();

            //if ( prevheads.Count > 0)
            //{
            //    prevhead = prevheads.Last();
            //}
          
            //計画検索
            //List<SiCardPlan> plans = SiCardPlan.GetRecords(loginUser);


            //List<SiCardPlan> plans = 配乗計画.BLC_配乗計画_SearchPlan(loginUser, seninTableCache, date, date.AddMonths(1), SiCardPlanHead.VESSEL_KIND_フェリー);
            List<SiCardPlan> plans = 配乗計画.BLC_配乗計画_SearchPlan(loginUser, seninTableCache, date, date.AddMonths(1), PlanType);
            AppointmentList = new List<Appointment>();

            //計画リスト
            foreach (SiCardPlan p in plans)
            {

                //検索したヘッダと同じIDなら対象レコード
                //if (p.SiCardPlanHeadID == head.SiCardPlanHeadID)
                //{
                    Appointment ap = new Appointment(p);

                //ap.EndDate = NBaseCommon.配乗計画Func.乗船計画終了日_Class2Disp(ap.EndDate);

                AppointmentList.Add(ap);
                //}
                //else if (p.SiCardPlanHeadID == prevhead.SiCardPlanHeadID)
                //{
                //    if (p.EndDate > date)
                //    {
                //        Appointment ap = new Appointment(p);

                //        AppointmentList.Add(ap);
                //    }
                //}
            }


        }

        private void GetDataA(MsUser loginUser, SeninTableCache seninTableCache, DateTime date)
        {

            DateTime date1 = date;
            DateTime date2 = date.AddMonths(1);

            SiCardFilter filter = new SiCardFilter();
            filter.Start = date1;
            filter.End = date2;

            #region フェリー乗船の実績
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船));
            List<MsVessel> vlist = seninTableCache.GetMsVesselList(loginUser);
            foreach (MsVessel v in vlist)
            {
                //if (v.MsVesselTypeID == MsVesselType.MS_VESSEL_TYPE_フェリー_ID)
                if (v.IsPlanType(PlanType))
                {
                    filter.MsVesselIDs.Add(v.MsVesselID);
                }
            }

            //船員カード検索
            List<SiCard> actlist_v = SiCard.GetRecordsByFilter(loginUser, filter);
            foreach (SiCard card in actlist_v)
            {
                //Appointment作成
                Appointment ap = new Appointment(card);

                ap.ActFlg = 1;//実績


                if (card.StartDate == null || card.StartDate == DateTime.MinValue) continue;

                ap.StartDate = card.StartDate;

                //終了日がないなら今日にして継続のしるし
                if (card.EndDate == null || card.EndDate == DateTime.MinValue)
                {
                    ap.EndDate = DateTime.Today.Date;
                    ap.OnGoing = true;
                    continue;
                }
                else
                {
                    ap.EndDate = Get乗船計画終了日(card.EndDate);
                }

                //期間のstartとendが逆転するデータ
                if (ap.StartDate >= ap.EndDate) continue;

                ap.MsSeninID = card.MsSeninID;
                ap.SeninName = card.SeninName;


                if (card.SiLinkShokumeiCards.Count > 0)
                {
                    foreach (SiLinkShokumeiCard link in card.SiLinkShokumeiCards)
                    {
                        ap.MsSiShokumeiID = link.MsSiShokumeiID;
                        ap.MsSiShokumeiShousaiID = link.MsSiShokumeiShousaiID;
                        break;
                    }
                }
                else
                {
                    ap.MsSiShokumeiID = card.SeninMsSiShokumeiID;
                }

                ap.MsSiShubetsuID = card.MsSiShubetsuID;
                ap.MsVesselID = card.MsVesselID;

                Shokumei shoku =seninTableCache.GetShokumei(loginUser, Shokumei.フェリー, ap.MsSiShokumeiID, ap.MsSiShokumeiShousaiID);
                ap.ShokuName = shoku.Name;
                ap.ShokuNameAbbr = shoku.NameAbbr;
                ap.ShokuNameEng = shoku.NameEng;

                AppointmentList.Add(ap);
            }
            #endregion
            
            #region その他休暇の実績
            filter = new SiCardFilter();

            filter.Start = date1;
            filter.End = date2;

            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇));
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.傷病));
            List<SiCard> actlist_etc = SiCard.GetRecordsByFilter(loginUser, filter);
            foreach (SiCard card in actlist_etc)
            {
                //Appointment作成
                Appointment ap = new Appointment(card);

                if (card.StartDate == null || card.StartDate == DateTime.MinValue ) continue;

                ap.ActFlg = 1;

                ap.StartDate = card.StartDate;

                //終了日がないなら今日にして継続のしるし
                if (card.EndDate == null || card.EndDate == DateTime.MinValue ) 
                {
                    ap.EndDate = DateTime.Today.Date;
                    ap.OnGoing = true;
                    continue;
                }
                else
                {
                    ap.EndDate = Get乗船計画終了日(card.EndDate);
                }

                //期間のstartとendが逆転するデータ
                if (ap.StartDate >= ap.EndDate) continue;

                ap.MsSeninID = card.MsSeninID;
                ap.SeninName = card.SeninName;


                if (card.SiLinkShokumeiCards.Count > 0)
                {
                    foreach (SiLinkShokumeiCard link in card.SiLinkShokumeiCards)
                    {
                        ap.MsSiShokumeiID = link.MsSiShokumeiID;
                        break;
                    }
                }
                else
                {
                    ap.MsSiShokumeiID = card.SeninMsSiShokumeiID;
                }
                ap.MsSiShubetsuID = card.MsSiShubetsuID;

                ap.ShokuName = "";
                ap.ShokuNameAbbr = "";
                ap.ShokuNameEng = "";

                AppointmentList.Add(ap);
            }
            #endregion

        }

    }

    /// <summary>
    /// Colorと種別の関連付けクラス
    /// </summary>
    public class ColorLinkSyubetsu
    {
        public Color LineColor;
        public int MsSiShubetuID;
        public int MsVesselID;

    }

}
