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
    public class 配乗計画表出力長期
    {

        private readonly string templateFilePath;
        private readonly string outputFilePath;
        private int PlanType;

        public 配乗計画表出力長期(string templateFilePath, string outputFilePath)
        {
            this.templateFilePath = templateFilePath;
            this.outputFilePath = outputFilePath;

            // この配乗表のタイプ
            PlanType = MsPlanType.PlanTypeHarfPeriod;
        }

        public void CreateFile(MsUser loginUser, SeninTableCache seninTableCache, DateTime date, bool isPlan)
        {
            int months = 6;

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

                _CreateFile(loginUser, seninTableCache, xlsx, isPlan, date, months);

                xlsx.DeleteSheet(0, 1);


                xlsx.CloseBook(true);
            }
            System.Threading.Thread.Sleep(1000);
            xlsx.Dispose();
        }

        private Dictionary<string, List<MsSenin>> SeninTbl = new Dictionary<string, List<MsSenin>>();
        private Dictionary<DateTime, Point> CalenderPosTbl = new Dictionary<DateTime, Point>();
        private List<SiCardPlanHead> HeadList = new List<SiCardPlanHead>();

        private List<Appointment> AppointmentList = new List<Appointment>();

        private List<DateTime> CaList = new List<DateTime>();
        private List<MsSiShokumei> ShokumeiList = new List<MsSiShokumei>();
        private List<MsVessel> VesselList = new List<MsVessel>();

        private TimeSpan 半日時間 = new TimeSpan(12, 0, 0);

        private Color EtcColor = Color.FromArgb(137, 137, 137);//グレー
        private List<ColorLinkSyubetsu> ColorSyubeList = new List<ColorLinkSyubetsu>();

        private List<Point> Position凡例List = new List<Point>();
        
        private int 上の月表示位置 = 14;//カラムヘッダの位置
        private int 下の月表示位置 = 75;//表の下の月表示の位置

        private void MakeColorList(MsUser loginUser, SeninTableCache seninTableCache)
        {

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
                    xlsx.Pos(p.X - 5, p.Y + 1, p.X - 1, p.Y + 1).Attr.BackColor = GetColor(loginUser, seninTableCache, seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船), VesselList[i].MsVesselID);
                }
            }
        }

        /// <summary>
        /// フェリーの配乗表作成
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="xlsx"></param>
        /// <param name="date"></param>
        private void _CreateFile(MsUser loginUser, SeninTableCache seninTableCache, ExcelCreator.Xlsx.XlsxCreator xlsx, bool isPlan, DateTime date, int months)
        {
            //凡例の場所
            for (int i = 0; i < 4; i++)
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
                HeadList = SiCardPlanHead.GetRecordsByYearMonth(loginUser, date, PlanType);

                GetDataP(loginUser, seninTableCache, date, months);
            }
            else
            {
                GetDataA(loginUser, seninTableCache, date, months);

            }

            List<MsSenin> SeninViewList = new List<MsSenin>();//表示船員リストクリア

            foreach (int seninId in seninIDs)
            {

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
            //List<string> caPos_col_List = new List<string>() { "C", "E", "G", "I", "K", "M", "O", "Q", "S", "U", "W", "Y", "AA", "AC", "AE", "AG", "AI", "AK", "AM", "AO", "AQ", "AS", "AU", "AW", "AY", "BA", "BC", "BE", "BG", "BI", "BK" };
            List<Point> caPos_col_List = new List<Point>();

            //カレンダー作成
            CaList = new List<DateTime>();

            DateTime date2 = date.AddMonths(months);
            int lastday = 0;

            lastday = DateTime.DaysInMonth(date2.Year, date2.Month);
            int startX = 2;//列の位置。[0]職名　[1]船員名 [2]表の領域スタート
            //for (int i = 0; i < lastday; i++)
            //最後の日
            date2 = new DateTime(date2.Year, date2.Month, lastday);

            DateTime wkdt = new DateTime(date.Year, date.Month, 1);
            int daycnt = 0;
            while (wkdt <= date2)
            {
                Point pt = new Point();
                pt.X = startX + daycnt;
                pt.Y = 上の月表示位置;
                CalenderPosTbl.Add(wkdt, pt);

                wkdt = wkdt.AddDays(1);
                daycnt++;
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
                creat(loginUser, seninTableCache, xlsx, isPlan, date, months, sname, SeninTbl[sname]);


            }

        }

        private string GetShokumeiGroup(int department)
        {
            string ret = "";
            switch (department)
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

        private void creat(MsUser loginUser, SeninTableCache seninTableCache, ExcelCreator.Xlsx.XlsxCreator xlsx, bool isPlan, DateTime date, int months, string title, List<MsSenin> seninList)
        {

            #region カレンダー作成、罫線、月をかく

            DateTime datebef = date;            
            foreach (DateTime wkdate in CalenderPosTbl.Keys)
            {
                if (wkdate.Month == datebef.Month)
                {
                    continue;
                }
                //wkdateが次の月になった

                //セル結合
                Point p1 = CalenderPosTbl[datebef];//月はじめのポジション

                DateTime enddate = datebef.AddMonths(1).AddDays(-1);

                Point p2 = CalenderPosTbl[enddate];

                xlsx.Pos(p1.X, 上の月表示位置, p2.X, 上の月表示位置).Attr.MergeCells = true; 
                xlsx.Pos(p1.X, 上の月表示位置, p2.X, 上の月表示位置).Value = datebef.Month;

                xlsx.Pos(p1.X, 下の月表示位置, p2.X, 下の月表示位置).Attr.MergeCells = true;
                xlsx.Pos(p1.X, 下の月表示位置, p2.X, 下の月表示位置).Value = datebef.Month;

                //罫線
                xlsx.Pos(p2.X, 上の月表示位置, p2.X, 下の月表示位置).Attr.LineRight(ExcelCreator.BorderStyle.Thin, Color.Black);

                datebef = wkdate;
            }

            #endregion

            #region 決まった場所の値をセット
            xlsx.Cell("**YEAR").Value = date.Year;
            DateTime lasmonth = date.AddMonths(months);
            xlsx.Cell("**MONTH").Value = date.Month.ToString() + "月～" + lasmonth.AddMonths(-1).Month.ToString() + "月";
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

            xlsx.Cell("**TITLE").Value = title + " 乗 下 船 計 画 表";

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
                    xlsx.Cell("**UPDATE" + i.ToString()).Value = HeadList[st].RenewDate.ToString("MM月dd日");
                    st++;
                }
            }
            #endregion

            #endregion

            int chartSeninRow_Max = 20;//表に入る船員数
            //int chartLasRow = 85;//表の最後の行
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
                //string cellstr = "A1:" + "GL" + chartLasRow.ToString().Trim();
                //xlsx.Cell(cellstr).Copy();

                ////表ペースト
                //int pasteR = chartLasRow * i + 1;
                //string pasteStr = "A" + pasteR.ToString();
                //xlsx.Cell(pasteStr).Paste();

                ////ペーストした表の行高さ調整
                //for (int r = 1; r < chartLasRow; r++)
                //{
                //    string cellStr = r.ToString();

                //    int row = r + chartLasRow * i;
                //    string cellStr2 = row.ToString();
                //    double h = xlsx.Cell(cellStr).RowHeight;//一度変数に入れないとHeightが変わらない
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
            int seninstartrow = 15;//15が船員表のはじめの行
            int seninrow = seninstartrow;
            foreach (MsSenin senin in seninList)
            {
                seninPosTbl.Add(senin, seninrow);

                if (s_cnt % chartSeninRow_Max == 0)
                {
                    //次の表
                    //next = next + chartLasRow;
                    next = next + (chartLasPoint.Y + 1);
                    seninrow = next + seninstartrow;
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
                xlsx.Pos(0, seninPosTbl[key]).Value = seninTableCache.GetMsSiShokumeiNameAbbr(loginUser, key.MsSiShokumeiID);
                xlsx.Pos(1, seninPosTbl[key]).Value = key.Sei + " " + key.Mei;


                List<Appointment> list = AppointmentList.Where(obj => obj.MsSeninID == key.MsSeninID).OrderBy(obj => obj.StartDate).ToList();
                if (list == null || list.Count == 0) continue;

                foreach (Appointment plan in list)
                {

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

                    //stratがカレンダーより小さい
                    DateTime calendarStartDate = CalenderPosTbl.Keys.First();
                    DateTime planStartDate = plan.StartDate;
                    if (plan.StartDate < calendarStartDate)
                    {
                        planStartDate = calendarStartDate;
                    }
                    DateTime wkdate = planStartDate.Date;


                    //endがカレンダーより小さい
                    DateTime calendarEndDate = CalenderPosTbl.Keys.Last();
                    DateTime planEndDate = plan.EndDate;
                    if (plan.EndDate > calendarEndDate)//午後の場合もあるので、日付が次の日以上が対象
                    {
                        planEndDate = calendarEndDate;
                    }

                    //日にちからカラムを取得
                    int span = GetSpan(planStartDate, planEndDate);

                    //Appointmentを置く
                    PutAppointment(loginUser, seninTableCache, xlsx, seninPosTbl[key], wkdate, plan.PmStart, span, plan.MsSiShubetsuID, plan.MsVesselID, shokumei, change);

                }
                cnt++;
            }

        }

        private void PutAppointment(MsUser loginUser, SeninTableCache seninTableCache, ExcelCreator.Xlsx.XlsxCreator xlsx, int rowindex, DateTime date, int frompm, int span, int shubeID, int vslID, string shokumei, int change)
        {
            //string cellstr = "";

            Point point = CalenderPosTbl[date];

            //start日付表示
            xlsx.Pos(point.X, rowindex).Value = date.ToShortDateString() + (frompm == 1 ? "PM" : "");
            xlsx.Pos(point.X, rowindex).Attr.FontStyle = ExcelCreator.FontStyle.Normal;

            if (change == 1)
            {
                xlsx.Pos(point.X, rowindex).Attr.BackColor = Color.FromArgb(0, 255, 0);
                xlsx.Pos(point.X + 1, rowindex).Attr.BackColor = Color.FromArgb(0, 255, 0);
            }

            //色付ける行はひとつ下の行
            int cellindex = rowindex + 1;

            //frompmなら1カラムずらす
            for (int i = 0; i < span; i++)
            {
                Color color = GetColor(loginUser, seninTableCache, shubeID, vslID);

                if (shubeID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船))
                {
                    xlsx.Pos(point.X + i, rowindex + 1).Attr.BackColor = color;
                    //xlsx.Cell(cellstr, cx, 0).Attr.BackColor = color;
                }
                else if (shubeID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.有給休暇))
                {
                    //xlsx.Cell(cellstr, cx, 0).Attr.LineBottom( ExcelCreator.BorderStyle.MediumDashDot, color);

                    xlsx.Pos(point.X + i, rowindex + 1).Attr.LineBottom(ExcelCreator.BorderStyle.MediumDashDot, color);
                    //System.Diagnostics.Debug.WriteLine("有給 cellstr(" + cellstr + ") cx=(" + cx.ToString()+ ")");
                }
                else if (shubeID == seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.傷病))
                {
                    //xlsx.Cell(cellstr, cx, 0).Attr.LineBottom(ExcelCreator.BorderStyle.Dashed, color);
                    xlsx.Pos(point.X + i, rowindex + 1).Attr.LineBottom(ExcelCreator.BorderStyle.Dashed, color);
                }

            }

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

            return tspan.Days;
        }
        #endregion

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

        private void GetDataP(MsUser loginUser, SeninTableCache seninTableCache, DateTime date, int months)
        {

            List<SiCardPlan> plans = 配乗計画.BLC_配乗計画_SearchPlan(loginUser, seninTableCache, date, date.AddMonths(months), PlanType);
            AppointmentList = new List<Appointment>();

            //計画リスト
            foreach (SiCardPlan p in plans)
            {

                Appointment ap = new Appointment(p);

                AppointmentList.Add(ap);

            }

        }

        private void GetDataA(MsUser loginUser, SeninTableCache seninTableCache, DateTime date, int months)
        {

            DateTime date1 = date;
            DateTime date2 = date.AddMonths(months);

            SiCardFilter filter = new SiCardFilter();
            filter.Start = date1;
            filter.End = date2;

            #region フェリー乗船の実績
            filter.MsSiShubetsuIDs.Add(seninTableCache.MsSiShubetsu_GetID(loginUser,MsSiShubetsu.SiShubetsu.乗船));
            List<MsVessel> vlist = seninTableCache.GetMsVesselList(loginUser);
            foreach (MsVessel v in vlist)
            {
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
                //else
                //{
                //    ap.EndDate = Get乗船計画終了日(card.EndDate);
                //}

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

                Shokumei shoku = seninTableCache.GetShokumei(loginUser, Shokumei.フェリー, ap.MsSiShokumeiID, ap.MsSiShokumeiShousaiID);
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

                if (card.StartDate == null || card.StartDate == DateTime.MinValue) continue;

                ap.ActFlg = 1;

                ap.StartDate = card.StartDate;

                //終了日がないなら今日にして継続のしるし
                if (card.EndDate == null || card.EndDate == DateTime.MinValue)
                {
                    ap.EndDate = DateTime.Today.Date;
                    ap.OnGoing = true;
                    continue;
                }
                //else
                //{
                //    ap.EndDate = Get乗船計画終了日(card.EndDate);
                //}

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

 

}
