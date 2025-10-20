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
using NBaseCommon.Senin.Excel;
using System.IO;

namespace WTM
{
    public partial class 月表示Form : Form
    {
        private List<MsSenin> SeninViewList = null;//絞り込まれた船員リスト

        private List<Appointment> AppointmentList = null;

        //選択されている月の初日末日
        private DateTime TargetMonth_Start = DateTime.MinValue;
        private DateTime TargetMonth_End = DateTime.MinValue;

        //Renderと作業内容を関連付ける
        private List<CalendarRender> RenderList = new List<CalendarRender>();
        private Dictionary<int, CalendarRectangleShapeRenderer> DevRenderDic = new Dictionary<int, CalendarRectangleShapeRenderer>();

        private CalendarRectangleShapeRenderer RenderWhite;


        private DateTime TargetMonth { get; set; }


        public 月表示Form() : this(DateTime.Today)
        {
        }

        public 月表示Form(DateTime targetMonth)
        {
            if (WtmCommon.VesselMode)
            {
                this.Font = new System.Drawing.Font(this.Font.FontFamily.Name, Common.VesselFontSize);
            }

            this.TargetMonth = targetMonth;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel_SearchMessage.Visible = false;
            panel_Approve.Visible = false;

            if (WtmCommon.VesselMode == false)
            {
                Common.Vessel = null;
            }

            //カレンダー見た目など設定
            InitCalendar();

            //船を検索、コンボにセット
            InitCommbobox船();

            Make凡例Deviation();

            MakeRender();

            monthPicker1.Value = TargetMonth;
            Get月初月末(TargetMonth);

            カレンダーの日数セット();

            ChangePanelSearchMessageLocation();
            ChangePanelApproveLocation();

            buttonApproval.Visible = false; // 承認
            groupBoxRankCategory.Visible = false; // 職位フィルタ

            // 船モードのみ
            if (WtmCommon.VesselMode)
            {
                // 承認
                buttonApproval.Visible = WtmCommon.FlgShowApproval;


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
            }

            comboBox船.SelectedIndex = 0;

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

        #region private void Make凡例Deviation()
        private void Make凡例Deviation()
        { 
            DeviationPanel p1 = new DeviationPanel("あらゆる２４時間の労働時間", Color.White,Color.Red);
            DeviationPanel p2 = new DeviationPanel("あらゆる１週間の労働時間", Color.Black, Color.LimeGreen);
            DeviationPanel p3 = new DeviationPanel("あらゆる４週間の時間外労働時間", Color.Black, Color.Orange);
            DeviationPanel p4 = new DeviationPanel("休息時間", Color.White, Color.Blue);

            flowLayoutPanel1.Controls.Add(p1);
            flowLayoutPanel1.Controls.Add(p2);
            flowLayoutPanel1.Controls.Add(p3);
            flowLayoutPanel1.Controls.Add(p4);
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
            gcCalendarGrid1.CalendarView = listView;

            //禁止
            gcCalendarGrid1.AllowDragPageScroll = false;
            gcCalendarGrid1.AllowUserToZoom = false;
            gcCalendarGrid1.AllowClipboard = false;

            //Appointmentドラッグはイベントで回避
            #endregion


            //小さいフォント作成
            Font f = new Font(gcCalendarGrid1.Font.Name, 8f);

            var template = new CalendarTemplate();

            //行ヘッダのカラム数
            template.RowHeaderColumnCount = 2;

            //行ヘッダの表示、非表示
            template.RowHeader.Columns[0].Visible = false;
            template.RowHeader.Columns[1].Width = 100;//カラムの幅
            template.RowHeader.Columns[1].AllowResize = false;

            if (WtmCommon.VesselMode)
            {
                template.RowHeader.Columns[1].Width = 125;
            }

            //カラム数
            template.ColumnCount = 1;

            //カラムの日付フォーマット
            template.ColumnHeader.Rows[0].Cells[0].DateFormatType = CalendarDateFormatType.DotNet;
            template.ColumnHeader.Rows[0].Cells[0].DateFormat = "%d";

            //コンテンツ部分の幅
            template.Content.Columns[0].Width = 45;
            template.Content.Columns[0].AllowResize = false;
            //template.Content.Columns[0].CellStyle.Font = f;

            //カラムヘッダの行数 
            template.ColumnHeaderRowCount = 1;

            //カラムヘッダ　リサイズ禁止
            template.ColumnHeader.Rows[0].AllowResize = false;

            //テンプレートセット
            gcCalendarGrid1.Template = template;

            #region コーナーの見た目
            //自由に変更できるセルタイプ作成
            var headerCellType = new CalendarHeaderCellType();
            headerCellType.FlatStyle = FlatStyle.Flat;
            headerCellType.UseVisualStyleBackColor = false;

            gcCalendarGrid1.CornerHeader.Table[0, 1].CellType = headerCellType.Clone();
            gcCalendarGrid1.CornerHeader.Table[0, 1].Value = "氏名";
            //gcCalendarGrid1.CornerHeader.Table[0, 1].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;
            gcCalendarGrid1.CornerHeader.Table[0, 1].CellStyle.TopBorder = new CalendarBorderLine(Control.DefaultBackColor, BorderLineStyle.None);

            #endregion


            //カレンダーを読み取り専用に
            gcCalendarGrid1.Protected = true;

            //船員名クリア
            int delrowcnt = gcCalendarGrid1.Template.RowCount;

            if (delrowcnt > 0)
            {
                gcCalendarGrid1.Template.RemoveRow(0, delrowcnt);
            }

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
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            panel_SearchMessage.Visible = true;
            panel_SearchMessage.Update();

            if (Common.Vessel == null)
            {
                panel_SearchMessage.Visible = false;
                return;
            }


            if (WtmCommon.VesselMode && WtmCommon.FlgShowApproval)
            {
                if (WtmAccessor.Instance().GetVesselApprovalMonth(Common.Vessel.MsVesselID, TargetMonth_Start.Date) != null)
                {
                    buttonApproval.Text = "承認済み";
                    buttonApproval.Enabled = false;
                }
                else
                {
                    buttonApproval.Text = "承認";

                    int shokumeiId = 0;
                    if (NBaseCommon.Common.siCard != null)
                    {
                        if (NBaseCommon.Common.siCard.SiLinkShokumeiCards != null && NBaseCommon.Common.siCard.SiLinkShokumeiCards.Count > 0)
                        {
                            shokumeiId = NBaseCommon.Common.siCard.SiLinkShokumeiCards[0].MsSiShokumeiID;
                        }
                    }
                    if (shokumeiId == 2701) // 船長のみ利用可
                    {
                        buttonApproval.Enabled = true;
                    }
                    else
                    {
                        buttonApproval.Enabled = false;
                    }
                }
            }


            AppointmentList = new List<Appointment>();

            //Work検索
            List<Work> wklist = WtmAccessor.Instance().GetWorks(TargetMonth_Start, TargetMonth_End, vesselId: Common.Vessel.MsVesselID);
            if (wklist.Count == 0)
            {
                panel_SearchMessage.Visible = false;
                return;
            }


            // 各船員の作業内訳
            foreach (MsSenin s in SeninViewList)
            {
                for (DateTime dt = TargetMonth_Start; dt <= TargetMonth_End; dt = dt.AddDays(1))
                {
                    //対象日付がかかわってるもの
                    //勤務中でないもの
                    var wks = wklist.Where(obj => obj.CrewNo == s.MsSeninID.ToString() && (obj.StartWork.Date == dt || obj.FinishWork.Date == dt) && obj.FinishWork != DateTime.MaxValue);
                    if (wks == null || wks.Count() == 0)
                        continue;

                    Appointment ap = new Appointment();

                    ap.MsSeninID = s.MsSeninID;
                    ap.WorkDate = dt.Date;
                    ap.WorkingTime = dt.Date;


                    double workMinutes = 0;
                    foreach (Work wk in wks)
                    {
                        foreach (WorkContentDetail wd in wk.WorkContentDetails)
                        {
                            var wc = WtmCommon.WorkContentList.Where(o => o.WorkContentID == wd.WorkContentID).FirstOrDefault();
                            if (wc == null || wc.IsIncludeWorkTime == false)
                                continue;

                            DateTime st = wd.WorkDate;
                            DateTime fin = st.AddMinutes(WtmCommon.WorkRange);
                            if (st < wk.StartWork)
                                st = wk.StartWork;
                            if (fin > wk.FinishWork)
                                fin = wk.FinishWork;

                            var m = (fin - st).TotalMinutes;

                            if (st.Date == dt.Date)
                            {
                                workMinutes += m;
                            }
                        }

                        if (wk.Deviations.Where(obj => obj.Kind == 1).Count() > 0)
                        {
                            ap.IsDeviation = true;
                        }
                        if (wk.Deviations.Where(obj => obj.Kind == 2).Count() > 0)
                        {
                            ap.IsDeviation1Week = true;
                        }
                        if (wk.Deviations.Where(obj => obj.Kind == 3).Count() > 0)
                        {
                            ap.IsDeviation4Week = true;
                        }
                        if (wk.Deviations.Where(obj => obj.Kind == 4).Count() > 0)
                        {
                            ap.IsDeviationResttime = true;
                        }
                    }

                    ap.WorkingTime = ap.WorkingTime.AddMinutes(workMinutes);

                    AppointmentList.Add(ap);
                    
                }
 
            }

            if (AppointmentList.Count > 2)
            {
                AppointmentList = AppointmentList.OrderBy(obj => obj.WorkDate).ToList();
            }

            // グリッドに表示
            SetData();

            // フィルタリング
            if (WtmCommon.FlgShowRankCategory)
            {
                Filtering();
            }







            panel_SearchMessage.Visible = false;
        }

        private void SetData()
        {
            gcCalendarGrid1.Content.ClearAll();

            foreach (Appointment ap in AppointmentList)
            {
                PutAppointment(ap);
            }

        }

        /// <summary>
        /// 船変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox船_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(comboBox船.SelectedItem is MsVessel)) return;
            if (WtmCommon.VesselMode == false)
                Common.Vessel = comboBox船.SelectedItem as MsVessel;

            船員セット();
            Search();
        }
      
        private void monthPicker1_CloseUp(object sender, EventArgs e)
        {
            ChangeDate();
        }

        private void button_PrevDay_Click(object sender, EventArgs e)
        {
            monthPicker1.Value = monthPicker1.Value.Date.AddMonths(-1);
            ChangeDate();
        }

        private void button_NextDay_Click(object sender, EventArgs e)
        {
            monthPicker1.Value = monthPicker1.Value.Date.AddMonths(1);
            ChangeDate();
        }

        private void ChangeDate()
        {
            DateTime d = monthPicker1.Value;
            Get月初月末(new DateTime(d.Year, d.Month, 1));
            カレンダーの日数セット();

            船員セット();
            Search();
        }

        private void 月表示Form_Resize(object sender, EventArgs e)
        {
            ChangePanelSearchMessageLocation();
            ChangePanelApproveLocation();
        }

        private void ChangePanelSearchMessageLocation()
        {
            var l = (this.Width - panel_SearchMessage.Width) / 2;
            var t = (this.Height - panel_SearchMessage.Height) / 2;

            panel_SearchMessage.Location = new Point(l, t);
        }
        private void ChangePanelApproveLocation()
        {
            var l = (this.Width - panel_Approve.Width) / 2;
            var t = (this.Height - panel_Approve.Height) / 2;

            panel_Approve.Location = new Point(l, t);
        }


        /// <summary>
        /// 　船員名の書き換え
        /// </summary>
        private void 船員セット()
        {
            //船
            if (Common.Vessel == null) return;

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
            var cards = Common.GetOnSigner(Common.Vessel.MsVesselID, TargetMonth_Start, TargetMonth_End);

            WtmModelBase.Role role = null;
            //if (WtmCommon.VesselMode && Common.Senin != null)
            //{
            //    role = WtmCommon.RoleList.Where(o => o.Rank == Common.Senin.MsSiShokumeiID.ToString()).FirstOrDefault();
            //}
            if (WtmCommon.VesselMode && NBaseCommon.Common.siCard != null)
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

                        if (SeninViewList.Any(o => o.MsSeninID == senin.MsSeninID) == false)
                        {
                            SeninViewList.Add(senin);
                        }
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

                //1船員あたり5行でマージ
                gcCalendarGrid1.Template.RowHeader[rowindex, 1].RowSpan = 5;

                #region ボーダーの変更
                //自由に変更できるセルタイプ作成
                var headerCellType = new CalendarHeaderCellType();
                headerCellType.FlatStyle = FlatStyle.Flat;
                headerCellType.UseVisualStyleBackColor = false;

                gcCalendarGrid1.Template.RowHeader[rowindex, 1].CellStyle.BottomBorder = new CalendarBorderLine(Color.DimGray, BorderLineStyle.Thin);

                //コンテンツ部分の変更
                gcCalendarGrid1.Template.Content.Rows[rowindex+4].CellStyle.BottomBorder = new CalendarBorderLine(Color.DimGray, BorderLineStyle.Thin);

                //Alarm行のフォーカス移動禁止
                for (int k = 0; k < gcCalendarGrid1.Template.Content.Columns.Count(); k++)
                {
                    //1船員あたりの行
                    for (int j = 0; j < 5; j++)
                    {
                        gcCalendarGrid1.Template.Content.Rows[rowindex + j].Cells[k].CanFocus = false;
                    }
                }

                #endregion
            }
            #endregion
        }

        private void comboBoxRankCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtering();
        }

        private void Filtering()
        {
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


        /// <summary>
        /// 「労務管理記録簿出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonOutput_Click(object sender, EventArgs e)
        private void buttonOutput_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "労務管理記録簿.xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                byte[] result = null;

                ProgressDialog progressDialog = new ProgressDialog(delegate
                {

                    System.Diagnostics.Debug.WriteLine($"Start:{DateTime.Now.ToShortTimeString()}");

                    try
                    {
#if HONSEN
                        string baseFileName = "労務管理記録簿";
                        string path = @"Template\";
                        string templateFilePath = path + "Template_" + baseFileName + ".xlsx";
                        string outputFilePath = path + "outPut_[" + Common.LoginUser.FullName + "]_" + baseFileName + ".xlsx";

                        SeninTableCache seninTableCache = SeninTableCache.instance(false);
                        seninTableCache.DacProxy = new DirectSeninDacProxy();

                        new 労務管理記録簿出力(templateFilePath, outputFilePath).CreateFile(Common.LoginUser, seninTableCache, WtmAccessor.Instance(), Common.Vessel.MsVesselID, 0, TargetMonth_Start.Date, TargetMonth_End.Date);

                        result = FileUtils.ToBytes(outputFilePath);
#else
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            result = serviceClient.BLC_Excel_労務管理記録簿出力(WtmCommon.ConnectionKey, Common.LoginUser, Common.Vessel.MsVesselID, 0, TargetMonth_Start.Date, TargetMonth_End.Date);
                        }
#endif
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Exception:{DateTime.Now.ToShortTimeString()}");
                        System.Diagnostics.Debug.WriteLine($"Exception:{ex.Message}");
                        ;
                    }
                    //--------------------------------

                }, "データ取得中です...");
                progressDialog.ShowDialog();

                //--------------------------------
                if (result == null)
                {
                    #region エラーメッセージ表示

                    MessageBox.Show("労務管理記録簿の出力に失敗しました。"
                        , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    #endregion
                    return;
                }
                //--------------------------------
                System.Diagnostics.Debug.WriteLine($"Finish:{DateTime.Now.ToShortTimeString()}");

                System.IO.FileStream filest = new System.IO.FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                filest.Write(result, 0, result.Length);
                filest.Close();

                MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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
            if (e.CellPosition.Scope == CalendarTableScope.CornerHeader || e.CellPosition.Scope == CalendarTableScope.Content)
            {
                //コーナーヘッダーまた内部のとき
                gcCalendarGrid1.ClearSelection();
                return;
            }
            else if (e.CellPosition.Scope == CalendarTableScope.ColumnHeader)
            {
                #region 日付の選択
                var vessel = comboBox船.SelectedItem as MsVessel;
                var dates = e.CellPosition.RelativeDates;

                //this.Hide();
                //this.Close();

                //日表示Form frm = new 日表示Form(vessel.MsVesselID, dates[0]);
                //frm.ShowDialog();

                WtmFormController.Show_日表示Form(vessel.MsVesselID, dates[0]);

                #endregion
            }
            else if (e.CellPosition.Scope == CalendarTableScope.RowHeader)
            {
                #region 船員の選択
                if (gcCalendarGrid1.RowHeader[0][e.CellPosition.RowIndex, 0].Value is MsSenin)
                {
                    var vessel = comboBox船.SelectedItem as MsVessel;
                    var senin = gcCalendarGrid1.RowHeader[0][e.CellPosition.RowIndex, 0].Value as MsSenin;

                    gcCalendarGrid1.ClearSelection();

                    //this.Hide();
                    //this.Close();

                    //個人表示Form frm = new 個人表示Form(vessel.MsVesselID, senin.MsSeninID, (monthPicker1.Value));
                    //frm.ShowDialog();

                    WtmFormController.Show_個人表示Form(vessel.MsVesselID, senin.MsSeninID, (monthPicker1.Value));
                }
                #endregion
            }
        }

        //------------------------------------------------------------------------------------------------------------------
        //-- カレンダー関連　処理
        //------------------------------------------------------------------------------------------------------------------
        private void PutAppointment(Appointment ap)
        {
            int colindex = 0;
            int rowindex = 0;


            if (ap.WorkDate != DateTime.MinValue)
            {
                rowindex = GetIndexRowBySeninID(ap.MsSeninID);
            }

            if (rowindex == -1)
                return;

            //カレンダーにセット 不要かも
            //this.gcCalendarGrid1.Content[ap.WorkDate][rowindex, colIndex].Value = ap;

            //時間表示
            gcCalendarGrid1[ap.WorkDate][rowindex, colindex].Value = ap.WorkingTime.ToString("HH:mm");

            //AppointCellType作成
            CalendarAppointmentCellType cactype1 = new CalendarAppointmentCellType();

            rowindex++;
            if (ap.IsDeviation)
            {
                Divisionを書く(rowindex, colindex, cactype1, ap, 1);
            }
            rowindex++;
            if (ap.IsDeviation1Week)
            {
                Divisionを書く(rowindex, colindex, cactype1, ap, 2);
            }
            rowindex++;
            if (ap.IsDeviation4Week)
            {
                Divisionを書く(rowindex, colindex, cactype1, ap, 3);
            }
            rowindex++;
            if (ap.IsDeviationResttime)
            {
                Divisionを書く(rowindex, colindex, cactype1, ap, 4);
            }
        }

        private void Divisionを書く(int rowindex, int colindex, CalendarAppointmentCellType cactype, Appointment ap, int deviatKind)
        {
            this.gcCalendarGrid1.Content[ap.WorkDate][rowindex, colindex].Value = ap;

            CalendarAppointmentCellType cactype1 = new CalendarAppointmentCellType();

            //セルタイプクローン、必須。おやくそく
            gcCalendarGrid1.Content[ap.WorkDate][rowindex, colindex].CellType = cactype1.Clone();

            (this.gcCalendarGrid1[ap.WorkDate][rowindex, colindex].CellType as CalendarAppointmentCellType).Renderer = DevRenderDic[deviatKind];
        }


        /// <summary>
        /// 船員IDで該当行のIndexを求める
        /// </summary>
        /// <param name="s_id"></param>
        /// <returns></returns>
        private int GetIndexRowBySeninID(int s_id)
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






        //------------------------------------------------------------------------------------------------------------------
        //-- 「承認」関連
        //------------------------------------------------------------------------------------------------------------------
        #region

        private void buttonApproval_Click(object sender, EventArgs e)
        {
            // 20250709 ボタンクリックで「承認」してしまってよいとのこと
            //panel_Approve.Visible = true;
            //panel_Approve.Update();

            //panel1.Enabled = false;
            //gcCalendarGrid1.Enabled = false;

            buttonExecApprove_Click(sender, e);
        }


        private void buttonExecApprove_Click(object sender, EventArgs e)
        {
            // 本当はパスワードの確認が必要


            // パスワード不一致
            //label_ApproveErrorMessage.Visible = true;

            DateTime d = monthPicker1.Value;
            DateTime approvalMonth = DateTimeUtils.ToFromMonth(d);
            int seninId = 0;
            if (Common.Senin != null)
                seninId = Common.Senin.MsSeninID;
            if (WtmAccessor.Instance().InsertOrUpdateApprovalMonth(Common.Vessel.MsVesselID, seninId, approvalMonth))
            {
                buttonApproval.Text = "承認済み";
                buttonApproval.Enabled = false;
            }

            //CloseApprovePanel();
        }

        private void buttonCancelApprove_Click(object sender, EventArgs e)
        {
            CloseApprovePanel();
        }

        private void CloseApprovePanel()
        {
            panel1.Enabled = true;
            gcCalendarGrid1.Enabled = true;

            panel_Approve.Visible = false;
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



            public bool IsDeviation;
            public bool IsDeviation1Week;
            public bool IsDeviation4Week;
            public bool IsDeviationResttime;


            /// <summary>
            /// 日付
            /// </summary>
            public DateTime WorkDate = DateTime.MinValue;

            /// <summary>
            /// 作業時間
            /// </summary>
            public DateTime WorkingTime = DateTime.MinValue;

            public override string ToString()
            {
                return "";
            }

            public Appointment()
            {
                IsDeviation = false;
                IsDeviation1Week = false;
                IsDeviation4Week = false;
                IsDeviationResttime = false;
            }

        }

        private void 月表示Form_FormClosed(object sender, FormClosedEventArgs e)
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
