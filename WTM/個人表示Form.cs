using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrapeCity.Win.CalendarGrid;
using NBaseData.DAC;
using NBaseData.BLC;
using NBaseData.DS;
using NBaseUtil;
using WtmModelBase;
using WtmModels;
using WtmData;

namespace WTM
{
    public partial class 個人表示Form : Form
    { 
        /// <summary>
        /// 1日当りの労働時間～休憩時間(長い) の列の数
        /// </summary>
        private int Num時間数列 = 7;
        private MsSenin Senin = null;
        private List<MsSenin> SeninViewList = null;//コンボボックスで絞り込まれたリスト

        private List<Work> WorkList = null;

        private List<Appointment> AppointmentList = null;
        private Dictionary<DateTime, List<double>> SeninWorkTime_Dic = null;

        private DateTime TargetMonth_Start = DateTime.MinValue;
        private DateTime TargetMonth_End = DateTime.MinValue;

        private string DateFormat = "yy/M/d" + " (" + "ddd" + ")";

        //Renderと作業内容を関連付ける
        private List<CalendarRender> RenderList = new List<CalendarRender>();
        private Dictionary<int, CalendarRectangleShapeRenderer> DevRenderDic = new Dictionary<int, CalendarRectangleShapeRenderer>();

        private CalendarRectangleShapeRenderer RenderWhite;


        private int InitialVesselId = 0;
        private int InitialSeninId = 0;
        private DateTime InitialMonth { get; set; }


        public 個人表示Form() : this(0, 0, DateTime.Today)
        {
        }

        public 個人表示Form(int vesselId, int seninId, DateTime targetMonth)
        {
            if (WtmCommon.VesselMode)
            {
                this.Font = new System.Drawing.Font(this.Font.FontFamily.Name, Common.VesselFontSize);
            }

            this.InitialVesselId = vesselId;
            this.InitialSeninId = seninId;
            this.InitialMonth = targetMonth;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            panel_SearchMessage.Visible = false;

            if (WtmCommon.FlgSummaryTimes == false) Num時間数列 = 0;

            //今日の日付で月初めとおわりの日付を取得
            Get月初月末(InitialMonth);

            //カレンダー見た目など設定
            InitCalendar();


            Make凡例();
            Make凡例Deviation();

            MakeRender();

            //行ヘッダ列の表示・非表示
            groupBox列表示.Visible = WtmCommon.FlgSummaryTimes;
            if (WtmCommon.FlgSummaryTimes)
            {
                時間数列の表示制御();
            }

            ChangePanelSearchMessageLocation();


            //船を検索、コンボにセット
            if (WtmCommon.VesselMode)
            {
                InitCommbobox船();
            }
            else
            {
                panel_SearchBox.Location = panel_VesselSelecter.Location;
                panel_VesselSelecter.Visible = false;

                船員セット();
            }





            if (WtmCommon.VesselMode && Common.Vessel != null)
            {
                comboBox船.SelectedIndex = 0;
            }
            else
            {
                foreach (var v in comboBox船.Items)
                {
                    if ((v as MsVessel).MsVesselID == InitialVesselId)
                    {
                        InitialVesselId = 0;
                        comboBox船.SelectedItem = v;
                        break;
                    }
                }
            }

            monthPicker1.Value = InitialMonth;

            if (InitialSeninId != 0)
            {
                foreach (var sd in comboBox船員.Items)
                {
                    if ((sd as SeninDisp).Senin.MsSeninID == InitialSeninId)
                    {
                        InitialSeninId = 0;
                        comboBox船員.SelectedItem = sd;
                        break;
                    }
                }
            }

#if HONSEN
            ////System.Windows.Forms.Screen s =
            ////    System.Windows.Forms.Screen.FromControl(this);
            ////if (s.Bounds.Width < this.Width)
            ////    this.Width = s.Bounds.Width;
            ////if (s.Bounds.Height < this.Height)
            ////    this.Height = s.Bounds.Height;

            //int h = System.Windows.Forms.Screen.GetWorkingArea(this).Height;
            //int w = System.Windows.Forms.Screen.GetWorkingArea(this).Width;

            //var ms = this.MinimumSize;
            //if (w < ms.Width)
            //    ms.Width = w;
            //if (h < ms.Height)
            //    ms.Height = h;

            //this.MinimumSize = ms;
            //this.Size = ms;

            //this.WindowState = FormWindowState.Maximized;
#endif
            if (WtmCommon.VesselMode)
            {
                Common.SetFormSize(this);
            }
        }

        #region private void 時間数列の表示制御()
        private void 時間数列の表示制御()
        {
            gcCalendarGrid1.Template.RowHeader.Columns[1].Visible = checkBox時間列1.Checked;
            gcCalendarGrid1.Template.RowHeader.Columns[2].Visible = checkBox時間列2.Checked;
            gcCalendarGrid1.Template.RowHeader.Columns[3].Visible = checkBox時間列3.Checked;
            gcCalendarGrid1.Template.RowHeader.Columns[4].Visible = checkBox時間列4.Checked;
            gcCalendarGrid1.Template.RowHeader.Columns[5].Visible = checkBox時間列5.Checked;
            gcCalendarGrid1.Template.RowHeader.Columns[6].Visible = checkBox時間列6.Checked;
            gcCalendarGrid1.Template.RowHeader.Columns[7].Visible = checkBox時間列7.Checked;
        }
        #endregion

        #region private void InitCommbobox船()
        private void InitCommbobox船()
        {
            if (Common.Vessel == null)
            {
                foreach (MsVessel v in NBaseCommon.Common.VesselList)
                {
                    comboBox船.Items.Add(v);
                }
            }
            else
            {
                comboBox船.Items.Add(Common.Vessel);
            }
        }
        #endregion

        #region  private void Get月初月末(DateTime td)
        private void Get月初月末(DateTime td)
        {
            TargetMonth_Start = new DateTime(td.Year, td.Month, 1);
            TargetMonth_End = TargetMonth_Start.AddMonths(1).AddDays(-1);
        }
        #endregion

        #region private void Make凡例()
        private void Make凡例()
        {
            foreach (WorkContent wc in WtmCommon.WorkContentList)
            {
                WorkContentPanel wcp = new WorkContentPanel(wc);
                flowLayoutPanel1.Controls.Add(wcp);
            }
        }
        #endregion

        #region private void Make凡例Deviation()
        private void Make凡例Deviation()
        {
            DeviationPanel p1 = new DeviationPanel("あらゆる２４時間の労働時間", Color.White, Color.Red);
            DeviationPanel p2 = new DeviationPanel("あらゆる１週間の労働時間", Color.Black, Color.LimeGreen);
            DeviationPanel p3 = new DeviationPanel("あらゆる４週間の時間外労働時間", Color.Black, Color.Orange);
            DeviationPanel p4 = new DeviationPanel("休息時間", Color.White, Color.Blue);

            flowLayoutPanel2.Controls.Add(p1);
            flowLayoutPanel2.Controls.Add(p2);
            flowLayoutPanel2.Controls.Add(p3);
            flowLayoutPanel2.Controls.Add(p4);
        }
        #endregion

        #region private void MakeRender()
        private void MakeRender()
        {
            //RenderWhite = new CalendarRoundedRectangleShapeRenderer();
            // RenderWhite.RoundedRadius = 0.4f;
            RenderWhite = new CalendarRectangleShapeRenderer();
            RenderWhite.FillColor = Color.White;
            RenderWhite.LineColor = Color.DarkGray;
            RenderWhite.LineStyle = CalendarShapeLineStyle.Thin;
            RenderWhite.LineWidth = 1;


            // ２４時間
            var ren = new CalendarRectangleShapeRenderer();
            ren.FillColor = Color.Red;
            ren.LineColor = Color.Red;
            ren.LineStyle = CalendarShapeLineStyle.Thin;
            ren.LineWidth = 1;

            DevRenderDic.Add(1, ren);


            // １週間
            ren = new CalendarRectangleShapeRenderer();
            ren.FillColor = Color.LimeGreen;
            ren.LineColor = Color.LimeGreen;
            ren.LineStyle = CalendarShapeLineStyle.Thin;
            ren.LineWidth = 1;

            DevRenderDic.Add(2, ren);

            // ４週間
            ren = new CalendarRectangleShapeRenderer();
            ren.FillColor = Color.Orange;
            ren.LineColor = Color.Orange;
            ren.LineStyle = CalendarShapeLineStyle.Thin;
            ren.LineWidth = 1;

            DevRenderDic.Add(3, ren);

            // 休息時間
            ren = new CalendarRectangleShapeRenderer();
            ren.FillColor = Color.Blue;
            ren.LineColor = Color.Blue;
            ren.LineStyle = CalendarShapeLineStyle.Thin;
            ren.LineWidth = 1;

            DevRenderDic.Add(4, ren);




            foreach (WorkContent wc in WtmCommon.WorkContentList)
            {
                CalendarRender rend = new CalendarRender();

                Color f = ColorTranslator.FromHtml(wc.FgColor);
                Color b = ColorTranslator.FromHtml(wc.BgColor);

                rend.RendarD = new CalendarRoundedRectangleShapeRenderer();
                rend.RendarD.RoundedRadius = 0;
                rend.RendarD.FillColor = b;
                rend.RendarD.LineColor = b;
                rend.RendarD.LineStyle = CalendarShapeLineStyle.Thin;
                rend.RendarD.LineWidth = 1;

                rend.RendarL = new CalendarRoundedRectangleShapeRenderer();
                rend.RendarL.RoundedRadius = 0;
                rend.RendarL.FillColor = ColorExtension.GetLightColor(b);
                rend.RendarL.LineColor = b; //ColorExtension.GetLightColor(b);
                rend.RendarL.LineStyle = CalendarShapeLineStyle.Thin;
                rend.RendarL.LineWidth = 1;

                rend.WorkContentId = wc.WorkContentID;

                RenderList.Add(rend);
            }
        }
        #endregion


        #region private void InitCalendar()
        private void InitCalendar()
        {
            #region プロパティ

            //カレンダーの方向
            var listView = new CalendarListView();
            listView.Orientation = Orientation.Vertical;
            gcCalendarGrid1.CalendarView = listView;

            //禁止
            gcCalendarGrid1.AllowDragPageScroll = false;
            gcCalendarGrid1.AllowUserToZoom = false;
            gcCalendarGrid1.AllowClipboard = false;

            //Appointmentドラッグはイベントで回避

            // カラム等のリサイズを禁止
            gcCalendarGrid1.ResizeMode = CalendarResizeMode.None;
            #endregion


            var template = new CalendarTemplate();
            
            //行ヘッダのカラム
            template.RowHeaderColumnCount = 1 + Num時間数列;



            //行ヘッダカラムの幅
            for (int i = 0; i < template.RowHeader.ColumnCount; i++)
            {
                template.RowHeader.Columns[i].Width = 80;

                if (WtmCommon.VesselMode && i == 0)
                {
                    template.RowHeader.Columns[i].Width = 110;
                }
            }

            //行ヘッダのカラム数
            if (WtmCommon.FlgSummaryTimes)
            {
                for (int i = 0; i < Num時間数列; i++)
                {
                    //幅
                    template.RowHeader.Columns[1 + i].Width = 60;
                    template.RowHeader.Columns[1 + i].AllowResize = false;
                }
            }

            //カラム数
            template.ColumnCount = 24 * 4;

            //カラムヘッダの行数
            template.ColumnHeaderRowCount = 2;

            //1日あたりの行数
            template.RowCount = 5;

            //日付
            template.RowHeader.Rows[0].Cells[0].DateFormatType = CalendarDateFormatType.DotNet;
            template.RowHeader.Rows[0].Cells[0].DateFormat = DateFormat;
            template.RowHeader.Rows[0].Cells[0].RowSpan = 5;
            template.RowHeader.Rows[0].Height = 20;

            //テンプレートセット
            gcCalendarGrid1.Template = template;

            //小さいフォント作成
            Font f = new Font(gcCalendarGrid1.Font.Name, 7f);

            //コーナーの見た目
            var headerCellType = new CalendarHeaderCellType();
            headerCellType.FlatStyle = FlatStyle.Flat;
            headerCellType.UseVisualStyleBackColor = false;

            //gcCalendarGrid1.CornerHeader.Table[0, 0].ColumnSpan = 3;
            //gcCalendarGrid1.CornerHeader.Table[1, 0].ColumnSpan = 3;
            //gcCalendarGrid1.CornerHeader.Table[0, 0].RowSpan = 2;
            gcCalendarGrid1.CornerHeader.Table[0, 0].Value = "時間";
            gcCalendarGrid1.CornerHeader.Table[0, 0].CellStyle.Alignment = CalendarGridContentAlignment.MiddleRight;
            gcCalendarGrid1.CornerHeader.Table[0, 0].CellStyle.BottomBorder = new CalendarBorderLine(Color.White, BorderLineStyle.None);

            gcCalendarGrid1.CornerHeader.Table[1,0].Value = "日付";
            gcCalendarGrid1.CornerHeader.Table[1, 0].CellStyle.Alignment = CalendarGridContentAlignment.MiddleLeft;
            gcCalendarGrid1.CornerHeader.Table[0, 0].CellStyle.TopBorder = new CalendarBorderLine(Color.White, BorderLineStyle.None);


            if (WtmCommon.FlgSummaryTimes)
            {
                int i = 1;
                コーナーヘッダ設定(headerCellType, i, f, "1日当りの", "労働時間");
                i++;

                コーナーヘッダ設定(headerCellType, i, f, "時間外労働", "");
                i++;

                コーナーヘッダ設定(headerCellType, i, f, "手当", "対象時間");
                i++;

                コーナーヘッダ設定(headerCellType, i, f, "1週間当りの", "労働時間");
                i++;

                コーナーヘッダ設定(headerCellType, i, f, "4週間当りの", "時間外労働");
                i++;

                コーナーヘッダ設定(headerCellType, i, f, "休憩時間", "");
                i++;

                コーナーヘッダ設定(headerCellType, i, f, "休憩時間", "(長い)");

            }

            カレンダーの日数セット();

            //Alarm行の高さセット
            for (int i = 0; i < template.RowHeader.RowCount; i++)
            {

                if (i % 5 != 0)
                {
                    template.RowHeader.Rows[i].Height = 8;
                }

                //リサイズ禁止
                template.RowHeader.Rows[i].AllowResize = false;



                //行ヘッダの5行のマージ
                for (int j = 0; j < 1 + Num時間数列; j++)
                {
                    gcCalendarGrid1.Template.RowHeader.Rows[i].Cells[j].RowSpan = 5;
                    gcCalendarGrid1.Template.RowHeader.Rows[i].Cells[j].CellStyle.Alignment = CalendarGridContentAlignment.MiddleLeft;
                }

                #region ボーダーの変更
                //自由に変更できるセルタイプ作成
                headerCellType = new CalendarHeaderCellType();
                headerCellType.FlatStyle = FlatStyle.Flat;
                headerCellType.UseVisualStyleBackColor = false;

                //行ヘッダの変更
                for (int j = 0; j < 1 + Num時間数列; j++)
                {
                    gcCalendarGrid1.Template.RowHeader[i, j].CellType = headerCellType.Clone();
                    gcCalendarGrid1.Template.RowHeader[i, j].CellStyle.BottomBorder = new CalendarBorderLine(Color.DimGray, BorderLineStyle.Thin);
                    gcCalendarGrid1.Template.RowHeader[i, j].CellStyle.LeftBorder = new CalendarBorderLine(Color.DimGray, BorderLineStyle.Thin);
                    gcCalendarGrid1.Template.RowHeader[i, j].CellStyle.RightBorder = new CalendarBorderLine(Color.DimGray, BorderLineStyle.Thin);
                }

                #endregion  
            }


            //データ部分のカラムの幅
            int time0 = 0;

            for (int i = 0; i < template.Content.ColumnCount; i++)
            {

                //セル幅
                template.Content.Columns[i].Width = 15;

                //リサイズ禁止
                template.Content.Columns[i].AllowResize = false;

                //カラムヘッダの1行目のマージと時刻セット
                if (i % 4 == 0)
                {
                    template.ColumnHeader[0, i].ColumnSpan = 4;
                    template.ColumnHeader[0, i].Value = time0;
                    time0++;

                    template.ColumnHeader[1, i].Value = "00";
                    template.ColumnHeader[1, i].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;
                    template.ColumnHeader[1, i].CellStyle.Font = f;


                    //ボーダーの線種類
                    template.ColumnHeader[0, i].CellStyle.LeftBorder = new CalendarBorderLine(Color.DimGray, BorderLineStyle.Thin);
                    template.ColumnHeader[1, i].CellStyle.LeftBorder = new CalendarBorderLine(Color.DimGray, BorderLineStyle.Thin);
                    template.Content.Columns[i].CellStyle.LeftBorder = new CalendarBorderLine(Color.DimGray, BorderLineStyle.Thin);
                }
                else if (i % 2 == 0)
                {
                    template.ColumnHeader[1, i].Value = "30";
                    template.ColumnHeader[1, i].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;
                    template.ColumnHeader[1, i].CellStyle.Font = f;
                }
            }

            //カレンダーを読み取り専用に
            gcCalendarGrid1.Protected = true;


            // 行選択時のカラー
            CalendarNamedCellStyle st = new CalendarNamedCellStyle("selectionstyle");
            st.SelectionBackColor = Color.White;
            this.gcCalendarGrid1.Styles.Add(st);
        }
        #endregion

        #region private void カレンダーの日数セット()
        private void カレンダーの日数セット()
        {
            var listView = (CalendarListView)gcCalendarGrid1.CalendarView;

            //表示する日数
            listView.DayCount = (TargetMonth_End - TargetMonth_Start).Days + 1;

            //カレンダーの最初の日付セット
            gcCalendarGrid1.FirstDateInView = TargetMonth_Start;

        }
        #endregion

        #region private void コーナーヘッダ設定(CalendarHeaderCellType headerCellType, int colindex, Font f, string val1, string val2)
        private void コーナーヘッダ設定(CalendarHeaderCellType headerCellType, int colindex, Font f, string val1, string val2)
        {
            //フォント小さく
            gcCalendarGrid1.CornerHeader.Table[0, colindex].CellStyle.Font = f;
            gcCalendarGrid1.CornerHeader.Table[1, colindex].CellStyle.Font = f;

            //おやくそく
            gcCalendarGrid1.CornerHeader.Table[0, colindex].CellType = headerCellType.Clone();
            gcCalendarGrid1.CornerHeader.Table[1, colindex].CellType = headerCellType.Clone();

            //ボーダー消す
            gcCalendarGrid1.CornerHeader.Table[0, colindex].CellStyle.BottomBorder = new CalendarBorderLine(Control.DefaultForeColor, BorderLineStyle.None);
            gcCalendarGrid1.CornerHeader.Table[1, colindex].CellStyle.TopBorder = new CalendarBorderLine(Control.DefaultForeColor, BorderLineStyle.None);

            //ボーダー書く
            gcCalendarGrid1.CornerHeader.Table[0, colindex].CellStyle.LeftBorder = new CalendarBorderLine(Control.DefaultForeColor, BorderLineStyle.Thin);
            gcCalendarGrid1.CornerHeader.Table[1, colindex].CellStyle.LeftBorder = new CalendarBorderLine(Control.DefaultForeColor, BorderLineStyle.Thin);

            gcCalendarGrid1.CornerHeader.Table[0, colindex].CellStyle.RightBorder = new CalendarBorderLine(Control.DefaultForeColor, BorderLineStyle.Thin);
            gcCalendarGrid1.CornerHeader.Table[1, colindex].CellStyle.RightBorder = new CalendarBorderLine(Control.DefaultForeColor, BorderLineStyle.Thin);

            //文字位置
            gcCalendarGrid1.CornerHeader.Table[0, colindex].CellStyle.Alignment = CalendarGridContentAlignment.BottomCenter;
            gcCalendarGrid1.CornerHeader.Table[1, colindex].CellStyle.Alignment = CalendarGridContentAlignment.TopCenter;


            gcCalendarGrid1.CornerHeader.Table[0, colindex].Value = val1;
            gcCalendarGrid1.CornerHeader.Table[1, colindex].Value = val2;
        }
        #endregion


        //------------------------------------------------------------------------------------------------------------------
        //-- 画面イベント
        //------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Work検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonSearch_Click(object sender, EventArgs e)
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        #endregion


        private void Search()
        {
            //NBaseCommon.LogFile.Write("個人表示", $"Search() Start");


            panel_SearchMessage.Visible = true;
            panel_SearchMessage.Update();

            AppointmentList = new List<Appointment>();
            SeninWorkTime_Dic = new Dictionary<DateTime, List<double>>();

            if (!(comboBox船員.SelectedItem is SeninDisp))
            {
                SetData();
                panel_SearchMessage.Visible = false;
                return;
            }

            MsSenin s = (comboBox船員.SelectedItem as SeninDisp).Senin;

            //NBaseCommon.LogFile.Write("個人表示", $"  GetWorks Call([{s.FullName}], from[{TargetMonth_Start.AddDays(-6)}], to[{TargetMonth_End}]");
            var t1 = DateTime.Now;

            //Work検索
            WorkList = WtmAccessor.Instance().GetWorks(TargetMonth_Start, TargetMonth_End, seninId: s.MsSeninID);

            var t2 = DateTime.Now;
            //NBaseCommon.LogFile.Write("個人表示", $"  GetWorks End({WorkList.Count}) : ({(t2 - t1).TotalSeconds})");

            // 作業内訳
            foreach (Work w in WorkList)
            {
                // 作業内訳
                foreach (WorkContentDetail wd in w.WorkContentDetails)
                {
                    if (wd.WorkDate.Date < TargetMonth_Start.Date)
                        continue;

                    Appointment apo = new Appointment();
                    apo.MsSeninID = int.Parse(w.CrewNo);
                    apo.WorkContentID = wd.WorkContentID;
                    apo.WorkDate = wd.WorkDate;

                    apo.Work = w;

                    AppointmentList.Add(apo);
                }

                // 船員のDeviation
                foreach (Deviation dev in w.Deviations)
                {
                    if (dev.WorkDate.Date < TargetMonth_Start.Date)
                        continue;

                    Appointment apo = new Appointment();
                    apo.MsSeninID = int.Parse(w.CrewNo);
                    apo.DeviationKind = dev.Kind;
                    apo.WorkDate = dev.WorkDate;

                    apo.Work = w;

                    AppointmentList.Add(apo);
                }
            }

            if (AppointmentList.Count > 2)
            {
                AppointmentList = AppointmentList.OrderBy(obj => obj.WorkDate).ToList();
            }

            // 集計データ
            if (WtmCommon.FlgSummaryTimes)
            {
                //NBaseCommon.LogFile.Write("個人表示", $"  GetWorkSummaries Call([{s.FullName}], from[{TargetMonth_Start.AddDays(-27)}], to[{TargetMonth_End}]");
                t1 = DateTime.Now;

                List<WorkSummary> summaries = WtmAccessor.Instance().GetWorkSummaries(TargetMonth_Start.AddDays(-27), TargetMonth_End, seninId: s.MsSeninID);

                t2 = DateTime.Now;
                //NBaseCommon.LogFile.Write("個人表示", $"  GetWorkSummaries End({summaries.Count}) : ({(t2 - t1).TotalSeconds})");

                for (DateTime dt = TargetMonth_Start.AddDays(-27); dt <= TargetMonth_End; dt = dt.AddDays(1))
                {
                    if (summaries.Any(o => o.Date == dt))
                    {
                        var summary = summaries.Where(o => o.Date == dt).First();

                        SeninWorkTime_Dic.Add(dt, new List<double>() { summary.WorkMinutes, summary.WorkMinutes1Week, summary.RestMinutes, summary.OverTimes });
                    }
                }
            }

            //グリッドに表示
            SetData();

            panel_SearchMessage.Visible = false;

            //NBaseCommon.LogFile.Write("個人表示", $"Search() End");
        }

        #region private double LongRestTime(DateTime day, IEnumerable<Work> Works)
        private double LongRestTime(DateTime day, IEnumerable<Work> Works)
        {
            TimeSpan longRestTime = new TimeSpan(0, 0, 0);
            if (Works.Count() != 0)
            {
                for (int i = 0; i <= Works.Count(); i++)
                {
                    if (i == 0)
                    {
                        if (Works.ElementAt(i).StartWork.Date == day.Date)
                        {
                            longRestTime = TimeSpan.Parse(Works.ElementAt(i).StartWork.ToString("HH:mm"));
                        }
                        else
                        {
                            longRestTime = TimeSpan.Parse(day.ToString("HH:mm"));
                        }
                    }
                    else
                    {
                        DateTime workTo = Works.ElementAt(i - 1).FinishWork;
                        if (i == Works.Count())
                        {
                            if (workTo.Day == Works.ElementAt(i - 1).StartWork.Day)
                            {
                                if ((new TimeSpan(24, 0, 0) - TimeSpan.Parse(workTo.ToString("HH:mm"))) > longRestTime)
                                {
                                    longRestTime = new TimeSpan(24, 0, 0) - TimeSpan.Parse(workTo.ToString("HH:mm"));
                                }
                            }
                        }
                        else
                        {
                            DateTime workFrom = Works.ElementAt(i).StartWork;
                            if ((workFrom - workTo) > longRestTime)
                            {
                                longRestTime = workFrom - workTo;
                            }
                        }
                    }
                }
            }

            return longRestTime.TotalMinutes;
        }
        #endregion

        #region private void SetData()
        private void SetData()
        {
            gcCalendarGrid1.Content.ClearAll();

            foreach (Appointment ap in AppointmentList)
            {
                PutAppointment(ap);
            }


            for (DateTime dt = TargetMonth_Start; dt <= TargetMonth_End; dt = dt.AddDays(1))
            {

                for (int k = 0; k < gcCalendarGrid1.Template.Content.Columns.Count(); k++)
                {
                    gcCalendarGrid1.Template.Content.Rows[0].Cells[k].CellStyleName = "selectionstyle";
                    gcCalendarGrid1.Template.Content.Rows[1].Cells[k].CellStyleName = "selectionstyle";
                    gcCalendarGrid1.Template.Content.Rows[2].Cells[k].CellStyleName = "selectionstyle";
                    gcCalendarGrid1.Template.Content.Rows[3].Cells[k].CellStyleName = "selectionstyle";
                    gcCalendarGrid1.Template.Content.Rows[4].Cells[k].CellStyleName = "selectionstyle";
                }
            }

            //集計データの表示
            if (WtmCommon.FlgSummaryTimes)
            {
                var workingMinutes = 60 * WtmCommon.WorkingHours;

                for (DateTime dt = TargetMonth_Start; dt <= TargetMonth_End; dt = dt.AddDays(1))
                {
                    int index = GetIndexRowByDate(dt);

                    if (SeninWorkTime_Dic.ContainsKey(dt))
                    {
                        // １日の労働時間
                        var minutes = SeninWorkTime_Dic[dt][0];
                        gcCalendarGrid1.RowHeader[index][0, 1].Value = ToHHMM(minutes);

                        // 時間外労働
                        if (minutes > workingMinutes)
                        {
                            gcCalendarGrid1.RowHeader[index][0, 2].Value = ToHHMM((minutes - workingMinutes));
                        }
                        else
                        {
                            gcCalendarGrid1.RowHeader[index][0, 2].Value = "0:00";
                        }

                        // 手当対象時間
                        gcCalendarGrid1.RowHeader[index][0, 3].Value = "0:00";


                        // １週間の労働時間
                        minutes = 0;
                        for (DateTime wdt = dt.AddDays(-6); wdt <= dt; wdt = wdt.AddDays(1))
                        {
                            if (SeninWorkTime_Dic.ContainsKey(wdt))
                            {
                                minutes += SeninWorkTime_Dic[wdt][0];
                            }
                        }
                        if (minutes > 0)
                        {
                            gcCalendarGrid1.RowHeader[index][0, 4].Value = ToHHMM(minutes);
                        }
                        else
                        {
                            gcCalendarGrid1.RowHeader[index][0, 4].Value = "0:00";
                        }

                        // ４週間の時間外労働
                        minutes = 0;
                        for (DateTime wdt = dt.AddDays(-27); wdt <= dt; wdt = wdt.AddDays(1))
                        {
                            if (SeninWorkTime_Dic.ContainsKey(wdt))
                            {
                                if (SeninWorkTime_Dic[wdt][0] > workingMinutes)
                                {
                                    minutes += (SeninWorkTime_Dic[wdt][0] - workingMinutes);
                                }
                            }
                        }
                        if (minutes > 0)
                        {
                            gcCalendarGrid1.RowHeader[index][0, 5].Value = ToHHMM(minutes);
                        }
                        else
                        {
                            gcCalendarGrid1.RowHeader[index][0, 5].Value = "0:00";
                        }


                        // 休息時間
                        var resttime = (24 * 60) - SeninWorkTime_Dic[dt][0];
                        gcCalendarGrid1.RowHeader[index][0, 6].Value = ToHHMM(resttime);


                        // 休息時間（長い方）
                        var longresttime = SeninWorkTime_Dic[dt][2];
                        gcCalendarGrid1.RowHeader[index][0, 7].Value = ToHHMM(longresttime);
                    }
                    else
                    {
                        gcCalendarGrid1.RowHeader[index][0, 1].Value = null;
                        gcCalendarGrid1.RowHeader[index][0, 2].Value = null;
                        gcCalendarGrid1.RowHeader[index][0, 3].Value = null;
                        gcCalendarGrid1.RowHeader[index][0, 4].Value = null;
                        gcCalendarGrid1.RowHeader[index][0, 5].Value = null;
                        gcCalendarGrid1.RowHeader[index][0, 6].Value = null;
                        gcCalendarGrid1.RowHeader[index][0, 7].Value = null;
                    }
                }
            }
        }
        #endregion

        #region private string ToHHMM(double minutes)
        private string ToHHMM(double minutes)
        {
            int hh = (int)(minutes / 60);
            int mm = (int)(minutes % 60);

            return $"{hh}:{mm.ToString("00")}";
        }
        #endregion




        /// <summary>
        /// 船変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox船_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox船_SelectedIndexChanged(object sender, EventArgs e)
        {
            Senin = null;

            船員セット();
        }
        #endregion


        /// <summary>
        /// 船員変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox船_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox船員_SelectedIndexChanged(object sender, EventArgs e)
        {
            Senin = null;

            Search();
        }
        #endregion


        /// <summary>
        /// 年月変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void monthPicker1_ValueChanged(object sender, EventArgs e)
        private void monthPicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime d = monthPicker1.Value;
            Get月初月末(new DateTime(d.Year, d.Month, 1));
            カレンダーの日数セット();

            Senin = null;
            if ((comboBox船員.SelectedItem is SeninDisp))
                Senin = (comboBox船員.SelectedItem as SeninDisp).Senin;

            船員セット();

            if (Senin != null)
            {
                foreach (var item in comboBox船員.Items)
                {
                    if ((item as SeninDisp).Senin.MsSeninID == Senin.MsSeninID)
                    {
                        comboBox船員.SelectedItem = item;
                        Senin = null;
                        return;
                    }
                }
            }
            Search();
        }
        #endregion


        /// <summary>
        /// 年月変更（前月ボタンクリック）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_PrevMonth_Click(object sender, EventArgs e)
        private void button_PrevMonth_Click(object sender, EventArgs e)
        {
            monthPicker1.Value = monthPicker1.Value.Date.AddMonths(-1);

            DateTime d = monthPicker1.Value;
            Get月初月末(new DateTime(d.Year, d.Month, 1));
            カレンダーの日数セット();

            Senin = null;
            if ((comboBox船員.SelectedItem is SeninDisp))
                Senin = (comboBox船員.SelectedItem as SeninDisp).Senin;

            船員セット();

            if (Senin != null)
            {
                foreach (var item in comboBox船員.Items)
                {
                    if ((item as SeninDisp).Senin.MsSeninID == Senin.MsSeninID)
                    {
                        comboBox船員.SelectedItem = item;
                        Senin = null;
                        return;
                    }
                }
            }
            Search();
        }
        #endregion


        /// <summary>
        /// 年月変更（翌月ボタンクリック）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_NextMonth_Click(object sender, EventArgs e)
        private void button_NextMonth_Click(object sender, EventArgs e)
        {
            monthPicker1.Value = monthPicker1.Value.Date.AddMonths(1);

            DateTime d = monthPicker1.Value;
            Get月初月末(new DateTime(d.Year, d.Month, 1));
            カレンダーの日数セット();

            Senin = null;
            if ((comboBox船員.SelectedItem is SeninDisp))
                Senin = (comboBox船員.SelectedItem as SeninDisp).Senin;

            船員セット();

            if (Senin != null)
            {
                foreach (var item in comboBox船員.Items)
                {
                    if ((item as SeninDisp).Senin.MsSeninID == Senin.MsSeninID)
                    {
                        comboBox船員.SelectedItem = item;
                        Senin = null;
                        return;
                    }
                }
            }
            Search();
        }
        #endregion


        /// <summary>
        /// 船変更、年月変更時処理
        /// 　船員コンボの書き換え
        /// </summary>
        #region private void 船員セット()
        private void 船員セット()
        {
            if (WtmCommon.VesselMode && !(comboBox船.SelectedItem is MsVessel)) return;

            gcCalendarGrid1.Content.ClearAll();

            SeninViewList = new List<MsSenin>();

            if (WtmCommon.VesselMode)
            {
                // 乗船者検索
                MsVessel v = comboBox船.SelectedItem as MsVessel;
                var cards = Common.GetOnSigner(v.MsVesselID, TargetMonth_Start, TargetMonth_End);

                WtmModelBase.Role role = null;
                //if (Common.Senin != null)
                //{
                //    role = WtmCommon.RoleList.Where(o => o.Rank == Common.Senin.MsSiShokumeiID.ToString()).FirstOrDefault();
                //}
                if (NBaseCommon.Common.siCard != null)
                {
                    string shokumeiId = "";
                    if (NBaseCommon.Common.siCard.SiLinkShokumeiCards != null && NBaseCommon.Common.siCard.SiLinkShokumeiCards.Count > 0)
                    {
                        shokumeiId = NBaseCommon.Common.siCard.SiLinkShokumeiCards[0].MsSiShokumeiID.ToString();
                    }
                    role = WtmCommon.RoleList.Where(o => o.Rank == shokumeiId).FirstOrDefault();
                }

                // 乗船職に置き換える
                foreach (MsSiShokumei shokumei in NBaseCommon.Common.ShokumeiList)
                {
                    var targetCards = cards.Where(o => o.SiLinkShokumeiCards[0].MsSiShokumeiID == shokumei.MsSiShokumeiID);

                    var onCrewId = targetCards.Select(o => o.MsSeninID).Distinct();

                    var senins = NBaseCommon.Common.SeninList.Where(o => onCrewId.Contains(o.MsSeninID)).OrderBy(o => o.Sei).ThenBy(o => o.Mei);
                    if (senins != null)
                    {
                        foreach (MsSenin senin in senins)
                        {
                            if (Common.Senin != null)
                            {
                                if (senin.MsSeninID != Common.Senin.MsSeninID)
                                {
                                    if (role == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (role.RankList.Any(o => o == shokumei.MsSiShokumeiID) == false)
                                            continue;
                                    }
                                }
                            }

                            senin.MsSiShokumeiID = shokumei.MsSiShokumeiID;

                            if (SeninViewList.Any(o => o.MsSeninID == senin.MsSeninID) == false)
                                SeninViewList.Add(senin);
                        }
                    }
                }
            }
            else
            {
                foreach (MsSiShokumei shokumei in NBaseCommon.Common.ShokumeiList)
                {
                    var senins = NBaseCommon.Common.SeninList.Where(o => o.MsSiShokumeiID == shokumei.MsSiShokumeiID).OrderBy(o => o.Sei).ThenBy(o => o.Mei);
                    if (senins != null)
                    {
                        foreach (MsSenin senin in senins)
                        {
                            if (senin.RetireFlag == 1 && senin.RetireDate.Date < TargetMonth_Start)
                                continue;
                            if (senin.NyuushaDate.Date > TargetMonth_End)
                                continue;

                            if (SeninViewList.Any(o => o.MsSeninID == senin.MsSeninID) == false)
                                SeninViewList.Add(senin);
                        }

                    }
                }
            }

            //船員コンボボックスにセット
            comboBox船員.Items.Clear();
            foreach(MsSenin s in SeninViewList)
            {
                SeninDisp sd = new SeninDisp();
                sd.Senin = s;
                sd.ShokumeiAbbr = SeninTableCache.instance().GetMsSiShokumeiNameAbbr(Common.LoginUser, s.MsSiShokumeiID);
                comboBox船員.Items.Add(sd);
            }

        }
        #endregion


        #region private void checkBox時間列_CheckedChanged(object sender, EventArgs e)
        private void checkBox時間列_CheckedChanged(object sender, EventArgs e)
        {
            時間数列の表示制御();
        }
        #endregion


        #region private void 個人表示Form_Resize(object sender, EventArgs e)
        private void 個人表示Form_Resize(object sender, EventArgs e)
        {
            ChangePanelSearchMessageLocation();
        }
        #endregion

        #region private void ChangePanelSearchMessageLocation()
        private void ChangePanelSearchMessageLocation()
        {
            var l = (this.Width - panel_SearchMessage.Width) / 2;
            var t = (this.Height - panel_SearchMessage.Height) / 2;

            panel_SearchMessage.Location = new Point(l, t);
        }
        #endregion


        /// <summary>
        /// 「月表示」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button月表示_Click(object sender, EventArgs e)
        private void button月表示_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //this.Close();

            //月表示Form frm = new 月表示Form(monthPicker1.Value);
            //frm.ShowDialog();

            WtmFormController.Show_月表示Form(monthPicker1.Value);
        }
        #endregion


        //------------------------------------------------------------------------------------------------------------------
        //-- カレンダー関連　イベント
        //------------------------------------------------------------------------------------------------------------------
        private void gcCalendarGrid1_AppointmentCellDragging(object sender, AppointmentCellDraggingEventArgs e)
        {
            //伸ばすの禁止
            e.AllowDrop = false;
            return;
        }

        private void gcCalendarGrid1_CellMouseClick(object sender, CalendarCellMouseEventArgs e)
        {
            //セル位置
            CalendarCellPosition cp = e.CellPosition;
            DateTime dt = cp.Date;


            if (e.CellPosition.Scope == CalendarTableScope.RowHeader)
            {
                //日付カラム以外は抜ける
                if (cp.ColumnIndex != 0)
                    return;

                #region 日付の選択
                var item = gcCalendarGrid1.SelectedCells[0];

                int vesselId = 0;
                if (comboBox船.Visible)
                {
                    vesselId = (comboBox船.SelectedItem as MsVessel).MsVesselID;
                }
                else
                {
                    MsSenin s = (comboBox船員.SelectedItem as SeninDisp).Senin;

                    SiCardFilter filter = new SiCardFilter();
                    filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(Common.LoginUser, MsSiShubetsu.SiShubetsu.乗船));
                    filter.Start = item.Date;
                    filter.End = DateTimeUtils.ToTo(item.Date);
                    filter.MsSeninID = s.MsSeninID;

                    List<SiCard> cards = null;
#if HONSEN
                    cards = 船員.BLC_船員カード検索(Common.LoginUser, SeninTableCache.instance(), filter);

#else
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        cards = serviceClient.BLC_船員カード検索(Common.LoginUser, filter);
                    }
#endif
                    if (cards == null || cards.Count == 0)
                    {
                        gcCalendarGrid1.ClearSelection();
                        MessageBox.Show($"船員：[{s.FullName}]は、{item.Date.ToShortDateString()}に乗船していません");
                        return;
                    }
                    vesselId = cards.First().MsVesselID;
                }

                gcCalendarGrid1.ClearSelection();

                //this.Hide();
                //this.Close();

                //日表示Form frm = new 日表示Form(vesselId, item.Date);
                //frm.ShowDialog();

                WtmFormController.Show_日表示Form(vesselId, item.Date);

                #endregion
            }         
            else if (cp.Scope == CalendarTableScope.Content)  //クリックされた場所がコンテンツ
            {
#if 個人表示では編集不可
                //アポインが無いならぬける
                if (gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value == null)
                {
                    return;
                }

                Appointment ap = gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value as Appointment;

                if (ap.DeviationKind > 0)
                {
                    // Deviation行なら抜ける
                    return;
                }

                出退勤Form frm = new 出退勤Form(ap.Work);
                frm.EditedEvent += new 出退勤Form.EditedEventHandler(EditAfterSearch);
                frm.ShowDialog();
#endif
            }

        }


        private void EditAfterSearch(object sender, EventArgs e)
        {
            Search();
        }



        //------------------------------------------------------------------------------------------------------------------
        //-- カレンダー関連　処理
        //------------------------------------------------------------------------------------------------------------------
        #region private void PutAppointment(Appointment ap)
        private void PutAppointment(Appointment ap)
        {
            int index = 0;


            if (ap.MsSeninID > 0 && ap.WorkContentID != null)
            {
                ;
            }
            else if (ap.DeviationKind == 1)
            {
                index += 1;
            }
            else if (ap.DeviationKind == 2)
            {
                index += 2;
            }
            else if (ap.DeviationKind == 3)
            {
                index += 3;
            }
            else if (ap.DeviationKind == 4)
            {
                index += 4;
            }

            int colIndex = 0;
            colIndex = GetIndexColumnByTime(ap.WorkDate);

            if (colIndex == -1) return;

            //セルタイプクローン、必須。おやくそく
            CalendarAppointmentCellType cactype1 = new CalendarAppointmentCellType();
            cactype1.ResizeHandlerVisibility = ResizeHandlerVisibility.NotShown;

            gcCalendarGrid1.Content[ap.WorkDate][index, colIndex].CellType = cactype1.Clone();

            //カレンダーにセット
            this.gcCalendarGrid1.Content[ap.WorkDate][index, colIndex].Value = ap;
            

            if (ap.DeviationKind > 0)
            {
                (this.gcCalendarGrid1[ap.WorkDate][index, colIndex].CellType as CalendarAppointmentCellType).Renderer = DevRenderDic[ap.DeviationKind];
            }
            else
            {
                (this.gcCalendarGrid1[ap.WorkDate][index, colIndex].CellType as CalendarAppointmentCellType).Renderer = RenderList.Where(o => o.WorkContentId == ap.WorkContentID).FirstOrDefault().RendarD;
            }
        }
        #endregion

        /// <summary>
        /// 日付で該当行(Grid)のインデックスを求める
        /// </summary>
        /// <param name="dt">日付</param>
        /// <returns></returns>
        #region public int GetIndexRowByDate(DateTime dt)
        public int GetIndexRowByDate(DateTime dt)
        {
           
            return dt.Day - 1;
        }
        #endregion


        /// <summary>
        /// 時間で該当列(Grid)のインデックスを求める
        /// </summary>
        /// <param name="dt">時間</param>
        /// <returns></returns>
        #region public int GetIndexColumnByTime(DateTime dt)
        public int GetIndexColumnByTime(DateTime dt)
        {
            int index = -1;

            //日付から時間取り出す
            int hidx = dt.Hour;
            int midx = -1;
            
            switch (dt.Minute) 
            {
                case 0:
                    midx = 0;
                    break;
                case 15:
                    midx = 1;
                    break;
                case 30:
                    midx = 2;
                    break;
                case 45:
                    midx = 3;
                    break;
            }

            if (midx == -1) return -1;

            return (hidx * 4) + midx;  

        }
        #endregion






        //------------------------------------------------------------------------------------------------------------------
        //-- クラス
        //------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Renderと種別の関連付けクラス
        /// </summary>
        public class CalendarRender
        {
            public CalendarRoundedRectangleShapeRenderer RendarD;
            public CalendarRoundedRectangleShapeRenderer RendarL;
            public string WorkContentId;
        }

        /// <summary>
        /// 船員コンボ表示クラス
        /// </summary>
        private class SeninDisp
        {
            public MsSenin Senin;
            public string Disp;
            public string ShokumeiAbbr;

            public override string ToString()
            {
               
                Disp = Senin.Sei + Senin.Mei + "(" + ShokumeiAbbr + ")";
                return Disp;
            }
        }

        private class Appointment
        {
            public int MsSiShokumeiID { get; set; }

            /// <summary>
            /// 船員ID 
            /// </summary>
            public int MsSeninID { get; set; }

            /// <summary>
            /// 作業内容ID
            /// </summary>
            public string WorkContentID = null;

            /// <summary>
            /// Deviation区分
            /// </summary>
            public int DeviationKind = 0;

            /// <summary>
            /// 時間
            /// </summary>
            public DateTime WorkDate = DateTime.MinValue;



            public Work Work { set; get; }


            public override string ToString()
            {
                return "";
            }


            public Appointment()
            {
            }
        }


        private void 個人表示Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            WtmFormController.DisposeForms();
        }

        private void button更新_Click(object sender, EventArgs e)
        {
            int seninId = -1;

            if (comboBox船員.SelectedItem is SeninDisp)
            {
                seninId = (comboBox船員.SelectedItem as SeninDisp).Senin.MsSeninID;
            }

            船員セット();

            if (seninId > 0)
            {
                foreach(var item in comboBox船員.Items)
                {
                    if ((item is SeninDisp) == false)
                    {
                        continue;
                    }
                    if ((item as SeninDisp).Senin.MsSeninID == seninId)
                    {
                        comboBox船員.SelectedItem = item;
                        break;
                    }
                }
            }

            Senin = null;
            Search();
        }
    }
}
