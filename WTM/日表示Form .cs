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
using System.Globalization;
using WtmData;

namespace WTM
{
    public partial class 日表示Form : Form
    {
        /// <summary>
        /// 1日当りの労働時間～休憩時間(長い) の列の数
        /// </summary>
        private int Num時間数列 = 7;

        private List<MsSenin> SeninViewList = null;//絞り込まれた船員リスト

        private MsSenin EditSenin = null;

        private List<Appointment> AppointmentList = null;
        private Dictionary<int, List<double>> SeninWorkTime_Dic = null;
        private List<VesselApprovalDay> ApprovalList = null;
        private List<SummaryTimes> SummaryTimesList = null;
        private bool IsApprover = false;

        //選択されている日付
        private DateTime SelectDate;

        //Renderと作業内容を関連付ける
        private List<CalendarRender> RenderList = new List<CalendarRender>();
        private Dictionary<int, CalendarRectangleShapeRenderer> DevRenderDic = new Dictionary<int, CalendarRectangleShapeRenderer>();

        private CalendarRectangleShapeRenderer RenderWhite;

        //動静情報
        VesselMovement VesselMovement;


        private int InitialVesselId = 0;
        private int InitialSeninId = 0;
        private DateTime InitialDate { get; set; }



        /// <summary>
        /// 選択されている作業(WorkContent)
        /// </summary>
        private WorkContent SelectWorkContent = null;




        private bool Dragging { set; get; } = false;

        private MouseButtons DownButton { set; get; }


        public 日表示Form() : this(0, DateTime.Today)
        {
        }

        public 日表示Form(int vesselId, DateTime targetDay)
        {
            if (WtmCommon.VesselMode)
            {
                this.Font = new System.Drawing.Font(this.Font.FontFamily.Name, Common.VesselFontSize);
            }

            this.InitialVesselId = vesselId;
            this.InitialDate = targetDay;

            InitializeComponent();
        }


        #region private void Form1_Load(object sender, EventArgs e)
        private void Form1_Load(object sender, EventArgs e)
        {
            panel_SearchMessage.Visible = false;
            panel_EditTime.Visible = false;


            if (WtmCommon.VesselMode == false)
            {
                Common.Vessel = null;
            }

            if (WtmCommon.FlgSummaryTimes == false) Num時間数列 = 0;

            //カレンダー見た目など設定
            InitCalendar();

            //船を検索、コンボにセット
            InitCommbobox船();


            Make凡例();
            Make凡例Deviation();

            MakeRender();


            dateTimePicker1.Value = InitialDate;


            //カレンダーの日付と合わせる
            SelectDate = dateTimePicker1.Value.Date;
            gcCalendarGrid1.FirstDateInView = SelectDate;


            groupBox_Anchorage.Visible = false;
            groupBox_VesselMovement.Visible = false;

            if (WtmCommon.FlgAnchorage || WtmCommon.FlgVesselMovement)
            {
                if (WtmCommon.VesselMode)
                {
                    tableLayoutPanel1.Height = 189 + 25;
                    panel_VesselMovementAnchorage.Height = 117;
                }
                else
                {
                    tableLayoutPanel1.Height = 179 + 23;
                    panel_VesselMovementAnchorage.Height = 107;
                }
                if (WtmCommon.FlgAnchorage && WtmCommon.FlgVesselMovement) //  動静情報 および 停泊情報 有効
                {
                    tableLayoutPanel1.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, (groupBox_VesselMovement.Width + groupBox_VesselMovement.Width + 25));
                    groupBox_Anchorage.Location = new Point(groupBox_VesselMovement.Location.X + groupBox_VesselMovement.Width + 5, groupBox_VesselMovement.Location.Y);

                    groupBox_VesselMovement.Visible = true;
                    groupBox_Anchorage.Visible = true;
                }
                else if (WtmCommon.FlgAnchorage) // 停泊情報のみ有効
                {
                    groupBox_Anchorage.Visible = true;
                }
                else if (WtmCommon.FlgVesselMovement) // 動静情報のみ有効
                {
                    groupBox_VesselMovement.Visible = true;
                }


                var diff = (tableLayoutPanel1.Height - 154);

                panel3.Location = new Point(panel3.Location.X, panel3.Location.Y + diff);

                gcCalendarGrid1.Location = new Point(gcCalendarGrid1.Location.X, gcCalendarGrid1.Location.Y + diff);
                gcCalendarGrid1.Height = gcCalendarGrid1.Height - diff;
            }

            //行ヘッダ列の表示・非表示
            if (WtmCommon.FlgSummaryTimes)
            {
                時間数列の表示制御();
            }
            else
            {
                var diff = groupBox列表示.Height - buttonApproval.Height;

                panel3.Height = panel3.Height - diff;

                gcCalendarGrid1.Location = new Point(gcCalendarGrid1.Location.X, gcCalendarGrid1.Location.Y - diff);
                gcCalendarGrid1.Height = gcCalendarGrid1.Height + diff;
            }
            groupBox列表示.Visible = WtmCommon.FlgSummaryTimes;

            groupBoxRankCategory.Visible = false; // 職位フィルタ
            buttonApproval.Visible = false; // 承認

            // 船モードのみ
            if (WtmCommon.VesselMode)
            {
                // 職位フィルタ
                groupBoxRankCategory.Visible = WtmCommon.FlgShowRankCategory;
                if (WtmCommon.FlgShowRankCategory)
                {
                    foreach (var rc in WtmCommon.RankCategoryList)
                    {
                        comboBoxRankCategory.Items.Add(rc);
                    }
                    comboBoxRankCategory.SelectedIndex = 0;
                }

                //if (Common.Senin != null)
                if (NBaseCommon.Common.siCard != null)
                {
                    // 承認
                    buttonApproval.Visible = WtmCommon.FlgShowApproval;
                    gcCalendarGrid1.Template.RowHeader.Columns[2].Visible = WtmCommon.FlgShowApproval;

                    if (buttonApproval.Visible)
                    {
                        string shokumeiId = "";
                        if (NBaseCommon.Common.siCard.SiLinkShokumeiCards != null && NBaseCommon.Common.siCard.SiLinkShokumeiCards.Count > 0)
                        {
                            shokumeiId = NBaseCommon.Common.siCard.SiLinkShokumeiCards[0].MsSiShokumeiID.ToString();
                        }
                        WtmModelBase.Role role = WtmCommon.RoleList.Where(o => o.Rank == shokumeiId).FirstOrDefault();
                        if (role == null || role.IsApprover == false)
                        {
                            buttonApproval.Enabled = false;
                        }
                        else
                        {
                            IsApprover = true;
                        }
                    }
                }
            }
            else
            {
                buttonApproval.Visible = false;
                gcCalendarGrid1.Template.RowHeader.Columns[2].Visible = false;
            }


            if (buttonApproval.Visible == false)
            {
                groupBox列表示.Location = new Point(buttonApproval.Location.X, groupBox列表示.Location.Y);
            }


            ChangePanelSearchMessageLocation();
            ChangePanelEditTimeLocation();

            if (InitialVesselId == 0)
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

#if HONSEN
            //Common.SetFormSize(this);
#endif
            if (WtmCommon.VesselMode)
            {
                Common.SetFormSize(this);
            }
        }
        #endregion

        #region private void 時間数列の表示制御()
        private void 時間数列の表示制御()
        {
            int columnCount = 3;

            gcCalendarGrid1.Template.RowHeader.Columns[columnCount].Visible = checkBox時間列1.Checked;
            gcCalendarGrid1.Template.RowHeader.Columns[columnCount+1].Visible = checkBox時間列2.Checked;
            gcCalendarGrid1.Template.RowHeader.Columns[columnCount+2].Visible = checkBox時間列3.Checked;
            gcCalendarGrid1.Template.RowHeader.Columns[columnCount+3].Visible = checkBox時間列4.Checked;
            gcCalendarGrid1.Template.RowHeader.Columns[columnCount+4].Visible = checkBox時間列5.Checked;
            gcCalendarGrid1.Template.RowHeader.Columns[columnCount+5].Visible = checkBox時間列6.Checked;
            gcCalendarGrid1.Template.RowHeader.Columns[columnCount+6].Visible = checkBox時間列7.Checked;
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

        #region private void Make凡例()
        private void Make凡例()
        {
            foreach (WorkContent wc in WtmCommon.WorkContentList)
            {
                //WorkContentPanel wcp = new WorkContentPanel(wc);
                //flowLayoutPanel1.Controls.Add(wcp);

                WorkContentLabel wcl = new WorkContentLabel(wc);
                // ユーザーコントロールで発火するイベントハンドラを追加
                wcl.OnSelected += WorkContent_Select;
                flowLayoutPanel1.Controls.Add(wcl);
            }

            // 出勤中
            {
                WorkContent wc = new WorkContent();
                wc.Name = "出勤中";
                wc.DspName = "";
                wc.FgColor = $"#{Color.White.R.ToString("X2")}{Color.White.G.ToString("X2")}{Color.White.B.ToString("X2")}";
                wc.BgColor = $"#{Color.DarkBlue.R.ToString("X2")}{Color.DarkBlue.G.ToString("X2")}{Color.DarkBlue.B.ToString("X2")}";

                //WorkContentPanel wcp = new WorkContentPanel(wc);
                //flowLayoutPanel1.Controls.Add(wcp);

                WorkContentLabel wcl = new WorkContentLabel(wc);
                // ユーザーコントロールで発火するイベントハンドラを追加
                wcl.OnSelected += WorkContent_Select;
                flowLayoutPanel1.Controls.Add(wcl);
            }
        }
        #endregion 
        
        /// <summary>
        /// 作業(WorkCont)をクリックした時
        /// </summary>
        /// <param name="e"></param>
        #region public void WorkContent_Select(WorkContentLabel.OnSelectedEventArgs e)
        public void WorkContent_Select(WorkContentLabel.OnSelectedEventArgs e)
        {
            if (e.workcontent.WorkContentID == null)
            {
                作業選択解除();
            }
            else
            {
                for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
                {
                    if (flowLayoutPanel1.Controls[i] is WorkContentLabel)
                    {
                        if ((flowLayoutPanel1.Controls[i] as WorkContentLabel).WC == e.workcontent)
                        {
                            (flowLayoutPanel1.Controls[i] as WorkContentLabel).SetSelectContent(true);
                            SelectWorkContent = (flowLayoutPanel1.Controls[i] as WorkContentLabel).WC;
                        }
                        else
                        {
                            (flowLayoutPanel1.Controls[i] as WorkContentLabel).SetSelectContent(false);
                        }
                    }
                }
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

            // 出勤中
            {
                CalendarRender rend = new CalendarRender();

                rend.RendarD = new CalendarRoundedRectangleShapeRenderer();
                rend.RendarD.RoundedRadius = 0;
                rend.RendarD.FillColor = Color.DarkBlue;
                rend.RendarD.LineColor = Color.DarkBlue;
                rend.RendarD.LineStyle = CalendarShapeLineStyle.Thin;
                rend.RendarD.LineWidth = 1;

                rend.RendarL = new CalendarRoundedRectangleShapeRenderer();
                rend.RendarL.RoundedRadius = 0;
                rend.RendarL.FillColor = ColorExtension.GetLightColor(Color.DarkBlue);
                rend.RendarL.LineColor = Color.DarkBlue;
                rend.RendarL.LineStyle = CalendarShapeLineStyle.Thin;
                rend.RendarL.LineWidth = 1;

                rend.WorkContentId = "9999";

                RenderList.Add(rend);
            }
        }
        #endregion

        #region private void InitCalendar()
        private void InitCalendar()
        {
            #region プロパティ

            //カレンダーの日数を1日に
            var listView = new CalendarListView();
            listView.DayCount = 1;
            gcCalendarGrid1.CalendarView = listView;

            //禁止
            gcCalendarGrid1.AllowDragPageScroll = false;
            gcCalendarGrid1.AllowUserToZoom = false;
            gcCalendarGrid1.AllowClipboard = false;

            //Appointmentドラッグはイベントで回避
            #endregion


            //小さいフォント作成
            Font f = new Font(gcCalendarGrid1.Font.Name, 7f);

            var template = new CalendarTemplate();

            int columnCount = 3;

            template.RowHeaderColumnCount = columnCount + Num時間数列;


            //行ヘッダの表示、非表示
            template.RowHeader.Columns[0].Visible = false;
            template.RowHeader.Columns[1].Width = 80;//カラムの幅
            template.RowHeader.Columns[1].AllowResize = false;

            if (WtmCommon.VesselMode)
            {
                template.RowHeader.Columns[1].Width = 125;
            }

            //行ヘッダのカラム数
            if (WtmCommon.FlgSummaryTimes)
            {                
                for (int i = 0; i < Num時間数列; i++)
                {
                    //幅
                    template.RowHeader.Columns[columnCount + i].Width = 60;
                    template.RowHeader.Columns[columnCount + i].AllowResize = false;
                }

                if (Common.Vessel != null && WtmCommon.FlgSummaryEdit)
                {
                    //ボーダー付きフォント作成
                    Font f2 = new Font(gcCalendarGrid1.Font.Name, gcCalendarGrid1.Font.Size, FontStyle.Underline);
                    template.RowHeader.Columns[columnCount + 2].CellStyle.ForeColor = Color.Blue;//時間手当
                    template.RowHeader.Columns[columnCount + 2].CellStyle.Font = f2;
                }
            }
            //if (WtmCommon.VesselMode && WtmCommon.FlgShowApproval)
            //{
            //    WtmModelBase.Role role = WtmCommon.RoleList.Where(o => o.Rank == Common.Senin.MsSiShokumeiID.ToString()).FirstOrDefault();
            //    if (role != null && role.IsApprover)
            //    {
            //        //ボーダー付きフォント作成
            //        Font f2 = new Font(gcCalendarGrid1.Font.Name, gcCalendarGrid1.Font.Size, FontStyle.Underline);
            //        template.RowHeader.Columns[2].CellStyle.ForeColor = Color.Blue;//承認
            //        template.RowHeader.Columns[2].CellStyle.Font = f2;
            //    }
            //}

            //カラム数
            template.ColumnCount = 24 * 4;

            //カラムヘッダの行数 
            template.ColumnHeaderRowCount = 2;

            //カラムヘッダの1行目、2行目　リサイズ禁止
            template.ColumnHeader.Rows[0].AllowResize = false;
            template.ColumnHeader.Rows[1].AllowResize = false;


            // 行選択時のカラー
            CalendarNamedCellStyle st = new CalendarNamedCellStyle("selectionstyle");
            st.SelectionBackColor = Color.White;
            this.gcCalendarGrid1.Styles.Add(st);


            //テンプレートセット
            gcCalendarGrid1.Template = template;

            #region コーナーの見た目
            //自由に変更できるセルタイプ作成
            var headerCellType = new CalendarHeaderCellType();
            headerCellType.FlatStyle = FlatStyle.Flat;
            headerCellType.UseVisualStyleBackColor = false;

            {
                gcCalendarGrid1.CornerHeader.Table[0, 1].CellType = headerCellType.Clone();
                gcCalendarGrid1.CornerHeader.Table[1, 1].CellType = headerCellType.Clone();

                gcCalendarGrid1.CornerHeader.Table[1, 1].Value = "氏名";
                gcCalendarGrid1.CornerHeader.Table[1, 1].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;
                gcCalendarGrid1.CornerHeader.Table[1, 1].CellStyle.TopBorder = new CalendarBorderLine(Control.DefaultBackColor, BorderLineStyle.None);
            }

            コーナーヘッダ設定(headerCellType, 2, f, "承認者", "");

            if (WtmCommon.FlgSummaryTimes)
            {
                int i = columnCount;
                コーナーヘッダ設定(headerCellType, i, f, "1日当りの","労働時間");
                i++;

                コーナーヘッダ設定(headerCellType, i, f, "時間外労働","");
                i++;

                コーナーヘッダ設定(headerCellType, i, f, "手当","対象時間");
                i++;

                コーナーヘッダ設定(headerCellType, i, f, "1週間当りの","労働時間");
                i++;

                コーナーヘッダ設定(headerCellType, i, f, "4週間当りの", "時間外労働");
                i++;

                コーナーヘッダ設定(headerCellType, i, f, "休憩時間","");
                i++;

                コーナーヘッダ設定(headerCellType, i, f, "休憩時間", "(長い)");
            }
            #endregion


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

            //船員名クリア
            int delrowcnt = gcCalendarGrid1.Template.RowCount;

            if (delrowcnt > 0)
            {
                gcCalendarGrid1.Template.RemoveRow(0, delrowcnt);
            }

        }

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
        #region private void Search()
        private void Search()
        {
            //System.Diagnostics.Debug.WriteLine("日表示Form:Search()");

            NBaseCommon.LogFile.Write("日表示", $"Search() Start");

            panel_SearchMessage.Visible = true;
            panel_SearchMessage.Update();


            AppointmentList = new List<Appointment>();

            DateTime startDay = SelectDate;

            //NBaseCommon.LogFile.Write("日表示", $"  GetWorks Call(vessel[{Common.Vessel.VesselName}], from[{startDay}], to[{SelectDate}]");
            var t1 = DateTime.Now;

            List<Work> wklist = WtmAccessor.Instance().GetWorks(startDay, SelectDate, vesselId: Common.Vessel.MsVesselID);

            var t2 = DateTime.Now;
            //NBaseCommon.LogFile.Write("日表示", $"  GetWorks End({wklist.Count}) : ({(t2 - t1).TotalSeconds})");



            //NBaseCommon.LogFile.Write("日表示", $"  GetBeInWorks Call(vessel[{Common.Vessel.VesselName}]");
            t1 = DateTime.Now;
            List<Work> beinWorkList = WtmAccessor.Instance().GetBeInWorks(Common.Vessel.MsVesselID);
            if (beinWorkList == null)
                beinWorkList = new List<Work>();
            t2 = DateTime.Now;
            //NBaseCommon.LogFile.Write("日表示", $"  GetBeInWorks End({beinWorkList.Count}) : ({(t2 - t1).TotalSeconds})");


            //====================================================================
            //NBaseCommon.LogFile.Write("日表示", $"  Appointment作成 Start");
            // 各船員の作業内訳
            foreach (MsSenin s in SeninViewList)
            {
                //Work検索
                var wks = wklist.Where(o => o.CrewNo == s.MsSeninID.ToString() && (o.StartWork.ToString("yyyyMMdd") == SelectDate.ToString("yyyyMMdd") || o.FinishWork.ToString("yyyyMMdd") == SelectDate.ToString("yyyyMMdd"))).OrderBy(o => o.StartWork);

                foreach (Work w in wks)
                {
                    // 船員の作業内訳
                    foreach (WorkContentDetail wd in w.WorkContentDetails)
                    {
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
                        Appointment apo = new Appointment();
                        apo.MsSeninID = int.Parse(w.CrewNo);
                        apo.DeviationKind = dev.Kind;
                        apo.WorkDate = dev.WorkDate;

                        apo.Work = w;

                        AppointmentList.Add(apo);
                    }
                }


                // 出勤中
                //var beinWork = WtmAccessor.Instance().GetBeInWork(s.MsSeninID);
                var beinWork = beinWorkList.Where(o => o.CrewNo == s.MsSeninID.ToString()).FirstOrDefault();
                if (beinWork != null)
                {
                    DateTime startWork = beinWork.StartWork;
                    int min = startWork.Minute;
                    if (min == 0)
                    {
                    }
                    else if (min < 15)
                    {
                        startWork = DateTime.Parse(startWork.ToString("yyyy/MM/dd HH:") + "00");
                    }
                    else if (min < 30)
                    {
                        startWork = DateTime.Parse(startWork.ToString("yyyy/MM/dd HH:") + "15");
                    }
                    else if (min < 45)
                    {
                        startWork = DateTime.Parse(startWork.ToString("yyyy/MM/dd HH:") + "30");
                    }
                    else
                    {
                        startWork = DateTime.Parse(startWork.ToString("yyyy/MM/dd HH:") + "45");

                    }
                    if (startWork < SelectDate)
                        startWork = SelectDate;
                    DateTime finishDate = SelectDate.AddDays(1);
                    for (DateTime dt = startWork; dt < finishDate; dt = dt.AddMinutes(WtmCommon.WorkRange))
                    {
                        Appointment apo = new Appointment();
                        apo.MsSeninID = s.MsSeninID;
                        apo.WorkContentID = "9999";
                        apo.WorkDate = dt;

                        apo.Work = beinWork;

                        AppointmentList.Add(apo);
                    }
                }

            }

            if (AppointmentList.Count > 2)
            {
                AppointmentList = AppointmentList.OrderBy(obj => obj.WorkDate).ToList();
            }
            //NBaseCommon.LogFile.Write("日表示", $"  Appointment作成 End");


            // 集計データ
            if (WtmCommon.FlgSummaryTimes)
            {
                startDay = SelectDate.AddDays(-27); // ４週間分（２８日分）のデータを取得する必要あり

                //NBaseCommon.LogFile.Write("日表示", $"  GetWorkSummaries Call(vessel[{Common.Vessel.VesselName}], from[{startDay}], to[{SelectDate}]");
                t1 = DateTime.Now;

                List<WorkSummary> summaries = WtmAccessor.Instance().GetWorkSummaries(startDay, SelectDate, vesselId: Common.Vessel.MsVesselID);

                t2 = DateTime.Now;
                //NBaseCommon.LogFile.Write("日表示", $"  GetWorkSummaries End({summaries.Count}) : ({(t2 - t1).TotalSeconds})");

                var workingMinutes = 60 * WtmCommon.WorkingHours;

                SeninWorkTime_Dic = new Dictionary<int, List<double>>();

                foreach (MsSenin s in SeninViewList)
                {
                    if (summaries.Any(o => o.CrewNo == s.MsSeninID.ToString()))
                    {
                        var summary = summaries.Where(o => o.CrewNo == s.MsSeninID.ToString()).First();

                        SeninWorkTime_Dic.Add(s.MsSeninID, new List<double>() { summary.WorkMinutes, summary.WorkMinutes1Week, summary.RestMinutes, summary.OverTimes });
                    }
                }
            }


            // 承認データ
            if (WtmCommon.VesselMode && WtmCommon.FlgShowApproval)
            {
                ApprovalList = WtmAccessor.Instance().GetVesselApprovalDay(Common.Vessel.MsVesselID, SelectDate.Date);
            }


            if (WtmCommon.FlgSummaryTimes)
            {
                SummaryTimesList = WtmAccessor.Instance().GetSummaryTimes(Common.Vessel.MsVesselID, SelectDate.Date);
            }

            //グリッドに表示
            SetData();

            // フィルタリング
            Filtering();

            // 承認ボタン
            SetApproval();

            panel_SearchMessage.Visible = false;

            //NBaseCommon.LogFile.Write("日表示", $"Search() End");
        }
        #endregion

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
            NBaseCommon.LogFile.Write("日表示", $"  SetData Start");
            gcCalendarGrid1.Content.ClearAll();

            //ワークの表示
            foreach (Appointment ap in AppointmentList)
            {
                PutAppointment(ap);
            }

            //承認データの表示
            if (WtmCommon.VesselMode && WtmCommon.FlgShowApproval)
            {
                foreach (var senin in SeninViewList)
                {
                    //船員IDから行数を求める
                    int rowindex = GetIndexRowBySeninID(senin.MsSeninID);

                    //ボーダー付きフォント作成
                    Font f = new Font(gcCalendarGrid1.Font.Name, gcCalendarGrid1.Font.Size);
                    gcCalendarGrid1.RowHeader[0][rowindex, 2].CellStyle.ForeColor = Color.Black;
                    gcCalendarGrid1.RowHeader[0][rowindex, 2].CellStyle.Font = f;

                    if (WtmAccessor.Instance().GetVesselApprovalMonth(Common.Vessel.MsVesselID, DateTimeUtils.ToFromMonth(SelectDate)) != null)
                    {
                        gcCalendarGrid1.RowHeader[0][rowindex, 2].Value = "月承認済み";
                    }
                    else if (ApprovalList != null && ApprovalList.Any(o => o.ApprovedCrewNo == senin.MsSeninID.ToString()))
                    {
                        var vad = ApprovalList.Where(o => o.ApprovedCrewNo == senin.MsSeninID.ToString()).First();
                        var approver = NBaseCommon.Common.SeninList.Where(o => o.MsSeninID.ToString() == vad.ApproverCrewNo).FirstOrDefault();
                        if (approver != null)
                        {
                            gcCalendarGrid1.RowHeader[0][rowindex, 2].Value = approver.FullName;


                            if (Common.Senin != null && vad.ApproverCrewNo == Common.Senin.MsSeninID.ToString()) // ログイン船員が承認者の場合、リンク表示
                            {
                                //ボーダー付きフォント作成
                                Font f2 = new Font(gcCalendarGrid1.Font.Name, gcCalendarGrid1.Font.Size, FontStyle.Underline);
                                gcCalendarGrid1.RowHeader[0][rowindex, 2].CellStyle.ForeColor = Color.Blue;//承認
                                gcCalendarGrid1.RowHeader[0][rowindex, 2].CellStyle.Font = f2;
                            }

                        }
                        else
                        {
                            gcCalendarGrid1.RowHeader[0][rowindex, 2].Value = "承認者不明";
                        }

                    }
                    else
                    {
                        gcCalendarGrid1.RowHeader[0][rowindex, 2].Value = "未承認";
                    }

                }
            }

            //集計データの表示
            if (WtmCommon.FlgSummaryTimes)
            {
                var percentage = radioButton_100.Checked ? 100 : radioButton_80.Checked ? 80 : 50;

                var workingMinutes = 60 * WtmCommon.WorkingHours;

                var devMax = (WtmCommon.Deviation * 60) * percentage / 100;
                var dev1weekMax = (WtmCommon.Deviation1Week * 60) * percentage / 100;
                var dev4weekMax = (WtmCommon.Deviation4Week * 60) * percentage / 100;

                var restMax = (WtmCommon.DeviationRestTime * 60) + ((24 - WtmCommon.DeviationRestTime) * 60) - ((24 - WtmCommon.DeviationRestTime) * 60) * percentage / 100;

                foreach (var senin in SeninViewList)
                {
                    //船員IDから行数を求める
                    int rowindex = GetIndexRowBySeninID(senin.MsSeninID);



                    if (SeninWorkTime_Dic.ContainsKey(senin.MsSeninID))
                    {
                        var seninId = senin.MsSeninID;

                        // １日の労働時間
                        var minutes = SeninWorkTime_Dic[seninId][0];
                        gcCalendarGrid1.RowHeader[0][rowindex, 3].Value = ToHHMM(minutes);

                        {
                            if (minutes > devMax)
                            {
                                gcCalendarGrid1.RowHeader[0][rowindex, 3].CellStyle.ForeColor = Color.White;
                                gcCalendarGrid1.RowHeader[0][rowindex, 3].CellStyle.BackColor = DevRenderDic[1].FillColor;
                            }
                            else
                            {
                                gcCalendarGrid1.RowHeader[0][rowindex, 3].CellStyle.ForeColor = Color.Black;
                                gcCalendarGrid1.RowHeader[0][rowindex, 3].CellStyle.BackColor = Color.White;
                            }
                        }


                        // 時間外労働
                        if (minutes > workingMinutes)
                        {
                            gcCalendarGrid1.RowHeader[0][rowindex, 4].Value = ToHHMM((minutes - workingMinutes));
                        }
                        else
                        {
                            gcCalendarGrid1.RowHeader[0][rowindex, 4].Value = "0:00";
                        }


                        // 手当対象時間
                        string allowanceTime = "00:00";
                        if (SummaryTimesList != null)
                        {
                            var sts = SummaryTimesList.Where(o => o.CrewNo == seninId.ToString()).FirstOrDefault();
                            if (sts != null)
                            {
                                allowanceTime = sts.AllowanceTime;
                            }
                        }
                        gcCalendarGrid1.RowHeader[0][rowindex, 5].Value = allowanceTime;


                        // １週間の労働時間
                        minutes = SeninWorkTime_Dic[seninId][1];
                        gcCalendarGrid1.RowHeader[0][rowindex, 6].Value = ToHHMM(minutes);
                        {
                            if (minutes > dev1weekMax)
                            {
                                gcCalendarGrid1.RowHeader[0][rowindex, 6].CellStyle.BackColor = DevRenderDic[2].FillColor;
                            }
                            else
                            {
                                gcCalendarGrid1.RowHeader[0][rowindex, 6].CellStyle.BackColor = Color.White;
                            }
                        }

                        // ４週間の時間外労働
                        minutes = SeninWorkTime_Dic[seninId][3];
                        if (minutes > 0)
                        {
                            gcCalendarGrid1.RowHeader[0][rowindex, 7].Value = ToHHMM(minutes);

                            {
                                if (minutes > dev4weekMax)
                                {
                                    gcCalendarGrid1.RowHeader[0][rowindex, 7].CellStyle.BackColor = DevRenderDic[3].FillColor;
                                }
                                else
                                {
                                    gcCalendarGrid1.RowHeader[0][rowindex, 7].CellStyle.BackColor = Color.White;
                                }
                            }
                        }
                        else
                        {
                            gcCalendarGrid1.RowHeader[0][rowindex, 7].Value = "0:00";
                        }

                        // 休息時間
                        var resttime = (24 * 60) - SeninWorkTime_Dic[seninId][0];
                        gcCalendarGrid1.RowHeader[0][rowindex, 8].Value = ToHHMM(resttime);
                        {
                            if (resttime < restMax)
                            {
                                gcCalendarGrid1.RowHeader[0][rowindex, 8].CellStyle.ForeColor = Color.White;
                                gcCalendarGrid1.RowHeader[0][rowindex, 8].CellStyle.BackColor = DevRenderDic[4].FillColor;
                            }
                            else
                            {
                                gcCalendarGrid1.RowHeader[0][rowindex, 8].CellStyle.ForeColor = Color.Black;
                                gcCalendarGrid1.RowHeader[0][rowindex, 8].CellStyle.BackColor = Color.White;
                            }
                        }

                        // 休息時間（長い方）
                        var longresttime = SeninWorkTime_Dic[seninId][2];
                        gcCalendarGrid1.RowHeader[0][rowindex, 9].Value = ToHHMM(longresttime);
                    }
                    else
                    {
                        
                        gcCalendarGrid1.RowHeader[0][rowindex, 3].Value = "0:00"; // １日の労働時間
                        gcCalendarGrid1.RowHeader[0][rowindex, 4].Value = "0:00"; // 時間外労働
                        gcCalendarGrid1.RowHeader[0][rowindex, 5].Value = "0:00"; // 手当対象時間
                        gcCalendarGrid1.RowHeader[0][rowindex, 6].Value = "0:00"; // １週間の労働時間
                        gcCalendarGrid1.RowHeader[0][rowindex, 7].Value = "0:00"; // ４週間の時間外労働
                        gcCalendarGrid1.RowHeader[0][rowindex, 8].Value = "24:00"; // 休息時間
                        gcCalendarGrid1.RowHeader[0][rowindex, 9].Value = "0:00"; // 休息時間（長い方）

                    }
                }
            }
            NBaseCommon.LogFile.Write("日表示", $"  SetData End");
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
            //船
            if (!(comboBox船.SelectedItem is MsVessel)) return;
            if (WtmCommon.VesselMode == false)
                Common.Vessel = comboBox船.SelectedItem as MsVessel;


            動静情報or完全停泊セット();

            船員セット();

            Search();
        }
        #endregion

        #region private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            ChangeDate();
        }
        #endregion

        #region private void button_PrevDay_Click(object sender, EventArgs e)
        private void button_PrevDay_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker1.Value.Date.AddDays(-1);
            ChangeDate();
        }
        #endregion

        #region private void button_NextDay_Click(object sender, EventArgs e)
        private void button_NextDay_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker1.Value.Date.AddDays(1);
            ChangeDate();
        }
        #endregion

        #region private void ChangeDate()
        private void ChangeDate()
        {
            //日付け
            SelectDate = dateTimePicker1.Value.Date;

            //カレンダーの日付と合わせる
            gcCalendarGrid1.FirstDateInView = SelectDate;

            動静情報or完全停泊セット();

            船員セット();

            Search();
        }
        #endregion


        #region private void comboBoxRankCategory_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBoxRankCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtering();
        }
        #endregion

        #region private void Filtering()
        private void Filtering()
        {
            if (WtmCommon.FlgShowRankCategory == false)
            {
                return;
            }

            if (!(comboBoxRankCategory.SelectedItem is RankCategory))
            {
                return;
            }

            var rc = (comboBoxRankCategory.SelectedItem as RankCategory);

            for (int i = 0; i < gcCalendarGrid1.Template.RowCount; i++)
            {
                if (gcCalendarGrid1.RowHeader[0][i, 0].Value is MsSenin)
                {
                    var s = (gcCalendarGrid1.RowHeader[0][i, 0].Value as MsSenin);

                    if (rc.RankList.Contains(s.MsSiShokumeiID))
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            gcCalendarGrid1.Template.Content.Rows[i + j].Visible = true;
                        }
                    }
                    else
                    {
                        if (Common.Senin != null && Common.Senin.MsSeninID == s.MsSeninID)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                gcCalendarGrid1.Template.Content.Rows[i + j].Visible = true;
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                gcCalendarGrid1.Template.Content.Rows[i + j].Visible = false;
                            }
                        }
                    }
                }
            }
        }
        #endregion


        #region private void 日表示Form_Resize(object sender, EventArgs e)
        private void 日表示Form_Resize(object sender, EventArgs e)
        {
            ChangePanelSearchMessageLocation();
            ChangePanelEditTimeLocation();
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

        #region private void ChangePanelEditTimeLocation()
        private void ChangePanelEditTimeLocation()
        {
            var l = (this.Width - panel_EditTime.Width) / 2;
            var t = (this.Height - panel_EditTime.Height) / 2;

            panel_EditTime.Location = new Point(l, t);
        }
        #endregion

        /// <summary>
        /// 動静情報または完全停泊の書き換え
        /// </summary>
        #region private void 動静情報or完全停泊セット()
        private void 動静情報or完全停泊セット()
        {
            List<VesselMovement> vesselMovementList = WtmAccessor.Instance().GetVesselMovementDispRecord(SelectDate, SelectDate, Common.Vessel.MsVesselID);
            VesselMovement = vesselMovementList.FirstOrDefault();
            if (VesselMovement == null)
            {
                VesselMovement = new VesselMovement();
            }

            if (WtmCommon.FlgAnchorage)
            {
                if (VesselMovement != null)
                {
                    //停泊
                    watermarkTextbox_完全停泊開始1.Text = VesselMovement.AnchorageS1;
                    watermarkTextbox_完全停泊終了1.Text = VesselMovement.AnchorageF1;

                    watermarkTextbox_完全停泊開始2.Text = VesselMovement.AnchorageS2;
                    watermarkTextbox_完全停泊終了2.Text = VesselMovement.AnchorageF2;
                }
                else
                {
                    watermarkTextbox_完全停泊開始1.Text = null;
                    watermarkTextbox_完全停泊終了1.Text = null;

                    watermarkTextbox_完全停泊開始2.Text = null;
                    watermarkTextbox_完全停泊終了2.Text = null;
                }

            }
            if (WtmCommon.FlgVesselMovement)
            {
                if (VesselMovement != null)
                {
                    checkBox_完全停泊.Checked = VesselMovement.FullAnchorage;

                    watermarkTextbox_動静港1.Text = VesselMovement.MovementInfoP1;
                    watermarkTextbox_動静イベント1.Text = VesselMovement.MovementInfoE1;

                    watermarkTextbox_動静港2.Text = VesselMovement.MovementInfoP2;
                    watermarkTextbox_動静イベント2.Text = VesselMovement.MovementInfoE2;
                }
                else
                {
                    checkBox_完全停泊.Checked = false;

                    watermarkTextbox_動静港1.Text = null;
                    watermarkTextbox_動静イベント1.Text = null;

                    watermarkTextbox_動静港2.Text = null;
                    watermarkTextbox_動静イベント2.Text = null;

                }
            }
        }
        #endregion


        /// <summary>
        /// 　船員名の書き換え
        /// </summary>
        #region private void 船員セット()
        private void 船員セット()
        {
            //クリア
            gcCalendarGrid1.Content.ClearAll();

            //船員名クリア
            int delrowcnt = gcCalendarGrid1.Template.RowCount;

            if (delrowcnt > 0)
            {
                gcCalendarGrid1.Template.RemoveRow(0, delrowcnt);
            }

            #region 船員の取得
            SeninViewList = new List<MsSenin>();

            // 乗船者検索
            var cards = Common.GetOnSigner(Common.Vessel.MsVesselID, SelectDate, SelectDate);

            WtmModelBase.Role role = null;
            //if (WtmCommon.VesselMode && Common.Senin != null)
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

                var senins = NBaseCommon.Common.SeninList.Where(o => onCrewId.Contains(o.MsSeninID));
                if (senins != null)
                {
                    foreach (MsSenin senin in senins)
                    {
                        //if (WtmCommon.VesselMode)
                        if (WtmCommon.VesselMode && Common.Senin != null)
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
            #endregion

            if (SeninViewList == null) return;

            #region 船員名を行ヘッダにいれる
            //船員数 1行あたりの行数=5
            gcCalendarGrid1.Template.RowCount = SeninViewList.Count * 5;

            for (int i = 0; i < SeninViewList.Count; i++)
            {
                int rowindex = i * 5;

                MsSenin s = SeninViewList[i] as MsSenin;

                gcCalendarGrid1.RowHeader[0][rowindex, 0].Value = s;
                gcCalendarGrid1.RowHeader[0][rowindex, 1].Value = s.FullName;

                //リサイズ禁止
                gcCalendarGrid1.Template.RowHeader.Rows[rowindex].AllowResize = false;

                //Alarm行の高さセット
                for (int j = 1; j < 5; j++)
                {
                    gcCalendarGrid1.Template.RowHeader.Rows[rowindex + j].Height = 8;

                    //リサイズ禁止
                    gcCalendarGrid1.Template.RowHeader.Rows[rowindex+j].AllowResize = false;
                }
                
                //行ヘッダの5行のマージ
                for (int j = 0; j < 3 + Num時間数列; j++)
                {
                    gcCalendarGrid1.Template.RowHeader.Rows[rowindex].Cells[j].RowSpan = 5;
                    gcCalendarGrid1.Template.RowHeader.Rows[rowindex].Cells[j].CellStyle.Alignment = CalendarGridContentAlignment.MiddleLeft;
                }

                #region ボーダーの変更
                //自由に変更できるセルタイプ作成
                var headerCellType = new CalendarHeaderCellType();
                headerCellType.FlatStyle = FlatStyle.Flat;
                headerCellType.UseVisualStyleBackColor = false;

                //行ヘッダの変更
                for (int j = 0; j < 3 + Num時間数列; j++)
                {
                    gcCalendarGrid1.Template.RowHeader[rowindex, j].CellType = headerCellType.Clone();
                    gcCalendarGrid1.Template.RowHeader[rowindex, j].CellStyle.BottomBorder = new CalendarBorderLine(Color.DimGray, BorderLineStyle.Thin);
                    gcCalendarGrid1.Template.RowHeader[rowindex, j].CellStyle.LeftBorder = new CalendarBorderLine(Color.DimGray, BorderLineStyle.Thin);
                    gcCalendarGrid1.Template.RowHeader[rowindex, j].CellStyle.RightBorder = new CalendarBorderLine(Color.DimGray, BorderLineStyle.Thin);
                }

                //コンテンツ部分の変更
                gcCalendarGrid1.Template.Content.Rows[rowindex+4].CellStyle.BottomBorder = new CalendarBorderLine(Color.DimGray, BorderLineStyle.Thin);

                //Alarm行のフォーカス移動禁止
                for (int k = 0; k < gcCalendarGrid1.Template.Content.Columns.Count(); k++)
                {
                    gcCalendarGrid1.Template.Content.Rows[rowindex].Cells[k].CellStyleName = "selectionstyle";

                    //時間表示の頭のカラム
                    if (k % 4 == 0)
                    {
                        //1船員あたりの行のAlarm部分
                        for (int j = 1; j < 5; j++)
                        {
                            //1時間を分割したカラム
                            gcCalendarGrid1.Template.Content.Rows[rowindex + j].Cells[k + 0].CanFocus = false;
                            gcCalendarGrid1.Template.Content.Rows[rowindex + j].Cells[k + 1].CanFocus = false;
                            gcCalendarGrid1.Template.Content.Rows[rowindex + j].Cells[k + 2].CanFocus = false;
                            gcCalendarGrid1.Template.Content.Rows[rowindex + j].Cells[k + 3].CanFocus = false;

                            gcCalendarGrid1.Template.Content.Rows[rowindex + j].Cells[k + 0].CellStyleName = "selectionstyle";
                            gcCalendarGrid1.Template.Content.Rows[rowindex + j].Cells[k + 1].CellStyleName = "selectionstyle";
                            gcCalendarGrid1.Template.Content.Rows[rowindex + j].Cells[k + 2].CellStyleName = "selectionstyle";
                            gcCalendarGrid1.Template.Content.Rows[rowindex + j].Cells[k + 3].CellStyleName = "selectionstyle";
                        }
                    }
                }

                #endregion
            }
            #endregion
        }
        #endregion



        #region private void checkBox時間列_CheckedChanged(object sender, EventArgs e)
        private void checkBox時間列_CheckedChanged(object sender, EventArgs e)
        {
            時間数列の表示制御();
        }
        #endregion


        /// <summary>
        /// KeyPress 終了日、時間の入力の制限をする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void textBox完全停泊時刻_KeyPress(object sender, KeyPressEventArgs e)
        private void textBox完全停泊時刻_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                //押されたキーが 0～9でない場合は、イベントをキャンセルする
                e.Handled = true;
            }
        }
        #endregion


        #region 完全停泊保存処理
        /// <summary>
        /// 完全停泊「保存」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_完全停泊保存_Click(object sender, EventArgs e)
        private void button_完全停泊保存_Click(object sender, EventArgs e)
        {
            if (!WtmCommon.FlgAnchorage) return;

            読み替え(watermarkTextbox_完全停泊開始1);
            読み替え(watermarkTextbox_完全停泊終了1);
            読み替え(watermarkTextbox_完全停泊開始2);
            読み替え(watermarkTextbox_完全停泊終了2);

            if (ValidateFields完全停泊())
            {
                //新規登録の場合
                if (VesselMovement == null || string.IsNullOrEmpty(VesselMovement.SiteID))
                {
                    if (VesselMovement == null)
                        VesselMovement = new VesselMovement();
                    VesselMovement.SiteID = WtmDac.SITE_ID;
                    VesselMovement.VesselID = Common.Vessel.MsVesselID.ToString();
                    VesselMovement.DateInfo = SelectDate.ToString("yyyyMMdd");
                    VesselMovement.TargetDate = SelectDate.Date;
                }
                VesselMovement.AnchorageS1 = watermarkTextbox_完全停泊開始1.Text;
                VesselMovement.AnchorageF1 = watermarkTextbox_完全停泊終了1.Text;
                VesselMovement.AnchorageS2 = watermarkTextbox_完全停泊開始2.Text;
                VesselMovement.AnchorageF2 = watermarkTextbox_完全停泊終了2.Text;

                if (!WtmAccessor.Instance().InsertUpdateVesselMovement(VesselMovement))
                {
                    MessageBox.Show("登録失敗です");
                    return;
                }
                MessageBox.Show("登録しました");
            }
        }
        #endregion

        /// <summary>
        /// 完全停泊のバリデーション
        /// </summary>
        /// <returns></returns>
        #region private bool ValidateFields完全停泊()
        private bool ValidateFields完全停泊()
        {
            string errstr = "完全停泊の入力が正しくありません";

            //正しい時刻か 空白は正しい
            if (!時刻チェック(watermarkTextbox_完全停泊開始1) || !時刻チェック(watermarkTextbox_完全停泊終了1) ||
                !時刻チェック(watermarkTextbox_完全停泊開始2) || !時刻チェック(watermarkTextbox_完全停泊終了2))
            {
                MessageBox.Show(errstr);
                return false;
            }

            //開始終了が揃っているか
            if (!(watermarkTextbox_完全停泊開始1.Text == "" && watermarkTextbox_完全停泊終了1.Text == "") &&
                !(watermarkTextbox_完全停泊開始1.Text != "" && watermarkTextbox_完全停泊終了1.Text != ""))
            {
                MessageBox.Show(errstr);
                return false;
            }

            //開始終了が揃っているか
            if (!(watermarkTextbox_完全停泊開始2.Text == "" && watermarkTextbox_完全停泊終了2.Text == "") &&
                !(watermarkTextbox_完全停泊開始2.Text != "" && watermarkTextbox_完全停泊終了2.Text != ""))
            {
                MessageBox.Show(errstr);
                return false;
            }

            //開始終了が逆転していないか
            if (string.IsNullOrEmpty(watermarkTextbox_完全停泊開始1.Text) == false && string.IsNullOrEmpty(watermarkTextbox_完全停泊終了1.Text) == false)
            {
                if (int.Parse(watermarkTextbox_完全停泊開始1.Text) > int.Parse(watermarkTextbox_完全停泊終了1.Text))
                {
                    MessageBox.Show(errstr);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(watermarkTextbox_完全停泊開始2.Text) == false && string.IsNullOrEmpty(watermarkTextbox_完全停泊終了2.Text) == false)
            {
                if (int.Parse(watermarkTextbox_完全停泊開始2.Text) > int.Parse(watermarkTextbox_完全停泊終了2.Text))
                {
                    MessageBox.Show(errstr);
                    return false;
                }
            }

            return true;
        }
        #endregion

        #endregion

        #region 動静保存処理
        private void button_動静保存_Click(object sender, EventArgs e)
        {
            if (!WtmCommon.FlgVesselMovement) return;

            //新規登録の場合
            if (VesselMovement == null || string.IsNullOrEmpty(VesselMovement.SiteID))
            {
                if (VesselMovement == null)
                    VesselMovement = new VesselMovement();
                VesselMovement.SiteID = WtmDac.SITE_ID;
                VesselMovement.VesselID = Common.Vessel.MsVesselID.ToString();
                VesselMovement.DateInfo = SelectDate.ToString("yyyyMMdd");
                VesselMovement.TargetDate = SelectDate.Date;
            }

            //完全停泊チェックボックス
            VesselMovement.FullAnchorage = checkBox_完全停泊.Checked;

            VesselMovement.MovementInfoP1 = watermarkTextbox_動静港1.Text;
            VesselMovement.MovementInfoE1 = watermarkTextbox_動静イベント1.Text;

            VesselMovement.MovementInfoP2 = watermarkTextbox_動静港2.Text;
            VesselMovement.MovementInfoE2 = watermarkTextbox_動静イベント2.Text;

            if (!WtmAccessor.Instance().InsertUpdateVesselMovement(VesselMovement))
            {
                MessageBox.Show("登録失敗です");
                return;
            }
            MessageBox.Show("登録しました");
        }

        #endregion


        /// <summary>
        /// 空白または正しい時刻はtrue
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        #region private bool 時刻チェック(TextBox txtbox)
        private bool 時刻チェック(TextBox txtbox)
        {
            if (txtbox.Text == "") return true;

            string checkstr = txtbox.Text.Insert(2, ":");

            string fmt = "HH:mm";
            CultureInfo ci = CultureInfo.CurrentCulture;
            DateTimeStyles dts = DateTimeStyles.None;
            DateTime dt;

            if (DateTime.TryParseExact(checkstr, fmt, ci, dts, out dt))
            {

                return true;
            }

            return false;
        }
        #endregion


        private void 読み替え(TextBox txtbox)
        {
            if (txtbox.Text == "2400") txtbox.Text = "2359";
        }



        /// <summary>
        /// 承認ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonApproval_Click(object sender, EventArgs e)
        private void buttonApproval_Click(object sender, EventArgs e)
        {
            DateTime approvalDay = DateTimeUtils.ToFrom(SelectDate);
            var ret = WtmAccessor.Instance().InsertOrUpdateApprovalDay(Common.Vessel.MsVesselID, approvalDay, Common.Senin.MsSeninID, SeninViewList.Select(o => o.MsSeninID).ToList());

            if (ret > 0)
            {
                MessageBox.Show($"{ret}件の勤怠を承認しました。");
                Search();
            }
        }
        #endregion





        /// <summary>
        /// 「作業解除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_作業選択解除_Click(object sender, EventArgs e)
        private void button_作業選択解除_Click(object sender, EventArgs e)
        {
            作業選択解除();
        }

        private void 作業選択解除()
        {
            SelectWorkContent = null;
            for (int i = 0; i < flowLayoutPanel1.Controls.Count; i++)
            {
                (flowLayoutPanel1.Controls[i] as WorkContentLabel).SetSelectContent(false);
            }
        }
        #endregion



        /// <summary>
        /// 「アラーム切替」
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void radioButton_CheckedChanged(object sender, EventArgs e)
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            //グリッドに表示
            SetData();

            // フィルタリング
            Filtering();

            // 承認ボタン
            SetApproval();
        }
        #endregion


        /// <summary>
        /// 「承認」状況の設定
        /// </summary>
        #region private void SetApproval()
        private void SetApproval()
        {
            if (WtmCommon.VesselMode && WtmCommon.FlgShowApproval)
            {
                if (WtmAccessor.Instance().GetVesselApprovalMonth(Common.Vessel.MsVesselID, DateTimeUtils.ToFromMonth(SelectDate)) != null)
                {
                    buttonApproval.Text = "承認済み";
                    if (IsApprover)
                        buttonApproval.Enabled = false;
                }
                else if (ApprovalList != null && ApprovalList.Count() == SeninViewList.Count())
                {
                    buttonApproval.Text = "承認済み";
                    if (IsApprover)
                        buttonApproval.Enabled = false;
                }
                else
                {
                    buttonApproval.Text = "承認";
                    if (IsApprover)
                        buttonApproval.Enabled = true;
                }
            }
        }
        #endregion





        //------------------------------------------------------------------------------------------------------------------
        //-- カレンダー関連　イベント
        //------------------------------------------------------------------------------------------------------------------
        #region private void gcCalendarGrid1_AppointmentCellDragging(object sender, AppointmentCellDraggingEventArgs e)
        private void gcCalendarGrid1_AppointmentCellDragging(object sender, AppointmentCellDraggingEventArgs e)
        {
            //伸ばすの禁止
            e.AllowDrop = false;
            Dragging = true;
            return;
        }
        #endregion

        #region private void gcCalendarGrid1_CellMouseClick(object sender, CalendarCellMouseEventArgs e)
        private void gcCalendarGrid1_CellMouseClick(object sender, CalendarCellMouseEventArgs e)
        {
            if (DownButton != MouseButtons.Left)
                return;

            //セル位置
            CalendarCellPosition cp = e.CellPosition;
            DateTime dt = cp.Date;

            if (cp.Scope == CalendarTableScope.RowHeader)
            {
                //クリックされた場所が行ヘッダ

                var senin = gcCalendarGrid1.RowHeader[0][cp.RowIndex, 0].Value as MsSenin;

                if (cp.ColumnIndex == 1)
                {
                    // 船員選択　⇒　個人表示へ切替
                    var vessel = comboBox船.SelectedItem as MsVessel;

                    gcCalendarGrid1.ClearSelection();

                    WtmFormController.Show_個人表示Form(vessel.MsVesselID, senin.MsSeninID, (dateTimePicker1.Value));
                }
                else
                {
                    if (WtmCommon.VesselMode && Common.Senin != null)
                    {
                        if (WtmCommon.FlgShowApproval && cp.ColumnIndex == 2)
                        {
                            // 承認解除処理
                            var vad = ApprovalList.Where(o => o.ApprovedCrewNo == senin.MsSeninID.ToString()).FirstOrDefault();
                            if (vad == null)
                                return;

                            if (vad.ApproverCrewNo != Common.Senin.MsSeninID.ToString()) // ログイン船員が承認者でない場合、何もしない
                                return;

                            if (MessageBox.Show("選択した勤怠の承認を取り消しますか？", "確認", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                if (WtmAccessor.Instance().DeleteApprovalDay(Common.Vessel.MsVesselID, vad.ApprovalDay, senin.MsSeninID))
                                {
                                    MessageBox.Show("選択した勤怠の承認を取り消しました。");
                                    Search();
                                }
                            }
                            return;
                        }
                        else if (WtmCommon.FlgSummaryEdit && cp.ColumnIndex == 5)
                        {
                            // 手当対象時間処理
                            var allowanceTime = gcCalendarGrid1.RowHeader[0][cp.RowIndex, 5].Value as string;

                            EditSenin = senin;
                            maskedTextBox_Time.Text = allowanceTime;

                            tableLayoutPanel1.Enabled = false;
                            gcCalendarGrid1.Enabled = false;
                            panel_SearchMessage.Enabled = false;
                            panel3.Enabled = false;

                            panel_EditTime.Visible = true;
                            buttonExecUpdate.Enabled = false;
                            label_EditTimeErrorMessage.Visible = false;
                            maskedTextBox_Time.Focus();

                            return;
                        }
                    }
                }
            }
            else if (cp.Scope == CalendarTableScope.Content)
            {
                // 出退勤処理

                Appointment ap = null;
                if (gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value != null)
                {
                    ap = gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value as Appointment;

                    出退勤Form frm = new 出退勤Form(ap.Work);
                    frm.EditedEvent += new 出退勤Form.EditedEventHandler(EditAfterSearch);
                    frm.ShowDialog();

                    DownButton = MouseButtons.None;
                }
            }

        }
        #endregion


        private void gcCalendarGrid1_CellMouseDown(object sender, CalendarCellMouseEventArgs e)
        {
            DownButton = e.Button;
        }


        private void gcCalendarGrid1_CellMouseUp(object sender, CalendarCellMouseEventArgs e)
        {
            if (DownButton != MouseButtons.Left)
                return;

            // ドラッグの場合、無視
            if (Dragging)
            {
                Dragging = false;
                return;
            }

            //選択されたセルのインデックス取得
            int curRow = e.CellPosition.RowIndex;
            int curCol = e.CellPosition.ColumnIndex;

            if (curRow < 0 || curCol < 0)
            {
                return;
            }

            //ヘッダでのイベントは無効
            if (e.CellPosition.Scope != CalendarTableScope.Content)
            {
                return;
            }

            //フォーカスを許していない場所では無効
            if (gcCalendarGrid1.Content[SelectDate].Rows[curRow].Cells[curCol].CanFocus == false)
            {
                return;
            }

            //既にデータがある
            if (gcCalendarGrid1.Content[SelectDate].Rows[curRow].Cells[curCol].Value != null)
            {
                return;
            }



            //選択している先頭のセルのインデックス取得
            int stRow = gcCalendarGrid1.SelectedCells[0].RowIndex;
            int stCol = gcCalendarGrid1.SelectedCells[0].ColumnIndex;

            //選択している最後のセルインデックス取得
            int endRow = gcCalendarGrid1.SelectedCells[gcCalendarGrid1.SelectedCells.Count - 1].RowIndex;
            int endCol = gcCalendarGrid1.SelectedCells[gcCalendarGrid1.SelectedCells.Count - 1].ColumnIndex;


            //選択範囲のチェック
            if (!check同じ船員の行か(stRow, endRow))
            {
                gcCalendarGrid1.ClearSelection();
                return;
            }


            //選択している間のどこかに既にデータがある
            List<Appointment> aplist = new List<Appointment>();
            for (int i = stCol; i <= endCol; i++)
            {
                if (gcCalendarGrid1.Content[SelectDate].Rows[stRow].Cells[i].Value != null)
                {
                    aplist.Add(gcCalendarGrid1.Content[SelectDate].Rows[stRow].Cells[i].Value as Appointment);
                }
            }
            if (aplist.Count > 0)
            {
                string msgstr = $@"作業の時間が重複しています。";

                MessageBox.Show(msgstr);
                return;
            }





            //開始時刻、終了時刻求める
            DateTime start = GetTimeByColumnIndex(stCol);
            DateTime end = GetTimeByColumnIndex(endCol);


            if (SelectWorkContent != null)
            {
                if (SelectWorkContent.WorkContentID == null) // 作業中が選択されている場合
                    return;


                // 作業が選択されている場合

                var work = new Work();
                work.CrewNo = getMsSeninByRowIndex(stRow).MsSeninID.ToString();
                work.VesselID = Common.Vessel.MsVesselID.ToString();
                work.StartWork = start;
                work.StartWorkAcutual = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                work.FinishWork = end.AddMinutes(WtmCommon.WorkRange);
                work.FinishWorkAcutual = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));

                work.WorkContentDetail = $"{ start.ToString("yyyy/MM/dd HH:mm:ss")},{SelectWorkContent.WorkContentID},{endCol - stCol + 1};" ;
                
                work.UpdateDate = DateTime.Now;


                if (ApprovalCheck(start, work.CrewNo))
                {
                    MessageBox.Show("承認済の日もしくは月が含まれているため、登録できません。");
                    return;
                }

                var ret = WtmAccessor.Instance().FinshWork(work);
                if (ret)
                {
                    Search();
                }
            }
            else
            {
                // 作業は選択されていない

                CalendarCellPosition cp = e.CellPosition;

                Appointment ap = null;
                if (gcCalendarGrid1.Content[start][cp.RowIndex, cp.ColumnIndex].Value == null)
                {
                    var work = new Work();
                    work.CrewNo = (gcCalendarGrid1.RowHeader[0][cp.RowIndex, 0].Value as MsSenin).MsSeninID.ToString();
                    work.VesselID = Common.Vessel.MsVesselID.ToString();
                    work.StartWork = start;
                    work.FinishWork = end.AddMinutes(WtmCommon.WorkRange);

                    work.WorkContentDetails = new List<WorkContentDetail>();
                    for(var dt = start; dt <= end; dt = dt.AddMinutes(WtmCommon.WorkRange))
                    {
                        var wd = new WorkContentDetail();
                        wd.WorkDate = dt;
                        work.WorkContentDetails.Add(new WorkContentDetail());
                    }

                    ap = new Appointment();
                    ap.Work = work;
                }
                else
                {
                    ap = gcCalendarGrid1.Content[start][cp.RowIndex, cp.ColumnIndex].Value as Appointment;
                }


                出退勤Form frm = new 出退勤Form(ap.Work);
                frm.EditedEvent += new 出退勤Form.EditedEventHandler(EditAfterSearch);
                frm.ShowDialog();

                gcCalendarGrid1.ClearSelection();
            }

        }

        /// <summary>
        /// 行から船員IDを取得する
        /// </summary>
        /// <param name="rowindex"></param>
        /// <returns></returns>
        #region private MsSenin getMsSeninByRowIndex(int rowindex)
        private MsSenin getMsSeninByRowIndex(int rowindex)
        {

            if (gcCalendarGrid1.RowHeader[0][rowindex, 0].Value is MsSenin)
            {
                return gcCalendarGrid1.RowHeader[0][rowindex, 0].Value as MsSenin;
            }
            return null;
        }
        #endregion

        /// <summary>
        /// row1、row2が同じ船員の範囲かどうか調べる
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        /// <returns></returns>
        #region private bool check同じ船員の行か(int row1, int row2)
        private bool check同じ船員の行か(int row1, int row2)
        {
            //1船員あたり5行なので
            if ((int)(row1 / 5) == (int)(row2 / 5)) return true;

            return false;
        }
        #endregion

        /// <summary>
        /// カラムインデックスから時間を求める
        /// </summary>
        /// <param name="dt">時間</param>
        /// <returns></returns>
        #region public DateTime GetTimeByColumnIndex(int colindex)
        public DateTime GetTimeByColumnIndex(int colindex)
        {
            DateTime ret = new DateTime(SelectDate.Year, SelectDate.Month, SelectDate.Day);

            int h = (int)colindex / 4;
            int t = (colindex - h * 4) * (int)WtmCommon.WorkRange;

            ret = ret.AddHours(h).AddMinutes(t);

            return ret;

        }
        #endregion


        #region private void EditAfterSearch(object sender, EventArgs e)
        private void EditAfterSearch(object sender, EventArgs e)
        {
            Search();
        }
        #endregion



        private bool ApprovalCheck(DateTime day, string crewNo)
        {
            bool ret = false;

            if (WtmCommon.VesselMode && WtmCommon.FlgShowApproval)
            {
                // 月次承認済みの確認
                if (WtmAccessor.Instance().GetVesselApprovalMonth(Common.Vessel.MsVesselID, DateTimeUtils.ToFromMonth(day)) != null)
                {
                    ret = true;
                }

                var startDayList = WtmAccessor.Instance().GetVesselApprovalDay(Common.Vessel.MsVesselID, day.Date);
                var vad = startDayList.Where(o => o.ApprovedCrewNo == crewNo).FirstOrDefault();
                if (vad != null)
                {
                    ret = true;
                }
            }

            return ret;
        }



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

            //月表示Form frm = new 月表示Form(dateTimePicker1.Value);
            //frm.ShowDialog();

            WtmFormController.Show_月表示Form(dateTimePicker1.Value);
        }
        #endregion








        //------------------------------------------------------------------------------------------------------------------
        //-- カレンダー関連　処理
        //------------------------------------------------------------------------------------------------------------------
        #region private void PutAppointment(Appointment ap)
        private void PutAppointment(Appointment ap)
        {
            int index = 0;

            if (ap.WorkDate != DateTime.MinValue)
            {
                index = GetIndexRowBySeninID(ap.MsSeninID);
            }

            if (index == -1)
                return;


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
        /// 船員IDで該当行のIndexを求める
        /// </summary>
        /// <param name="s_id"></param>
        /// <returns></returns>
        #region public int GetIndexRowBySeninID(int s_id)
        public int GetIndexRowBySeninID(int s_id)
        {
            for (int i = 0; i < gcCalendarGrid1.Template.RowCount; i++)
            {
                if (gcCalendarGrid1.RowHeader[0][i, 0].Value is MsSenin)
                {
                    if ((gcCalendarGrid1.RowHeader[0][i, 0].Value as MsSenin).MsSeninID == s_id)
                        return i;
                }
            }
            return -1;
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
        //-- 手当対象時間　関連　処理
        //------------------------------------------------------------------------------------------------------------------
        #region private void buttonExecUpdate_Click(object sender, EventArgs e)
        private void buttonExecUpdate_Click(object sender, EventArgs e)
        {
            var val = maskedTextBox_Time.Text;

            bool ret = WtmAccessor.Instance().InsertOrUpdateSummaryTimes(EditSenin.MsSeninID, Common.Vessel.MsVesselID, SelectDate.Date, val);

            if (ret)
                Search();

            FinishEditTime();
        }
        #endregion

        #region private void buttonCancelDelete_Click(object sender, EventArgs e)
        private void buttonCancelDelete_Click(object sender, EventArgs e)
        {
            FinishEditTime();
        }
        #endregion

        #region private void FinishEditTime()
        private void FinishEditTime()
        {
            panel_EditTime.Visible = false;
            tableLayoutPanel1.Enabled = true;
            gcCalendarGrid1.Enabled = true;
            panel_SearchMessage.Enabled = true;
            panel3.Enabled = true;

            EditSenin = null;
        }
        #endregion

        #region private void maskedTextBox_Time_KeyUp(object sender, KeyEventArgs e)
        private void maskedTextBox_Time_KeyUp(object sender, KeyEventArgs e)
        {
            bool isOk = true;
            var val = maskedTextBox_Time.Text;
            try
            {
                if (val.Length == 5)
                {
                    var splitVal = val.Split(':');
                    int hour = int.Parse(splitVal[0]);
                    int min = int.Parse(splitVal[1]);

                    if (hour > 23 || min > 59)
                    {
                        isOk = false;
                        label_EditTimeErrorMessage.Visible = true;
                    }
                }
                else
                {
                    isOk = false;
                    label_EditTimeErrorMessage.Visible = false;
                }
            }
            catch
            {
                isOk = false;
                label_EditTimeErrorMessage.Visible = true;
            }

            buttonExecUpdate.Enabled = isOk;
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

        private void 日表示Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            WtmFormController.DisposeForms();
        }

        private void button更新_Click(object sender, EventArgs e)
        {
            船員セット();
            Search();
        }
    }

}
