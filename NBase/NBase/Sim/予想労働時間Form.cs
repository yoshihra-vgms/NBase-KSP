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
using NBaseData.DS;
using NBaseUtil;
using System.IO;
using NBaseCommon.Senin;
using NBaseData.BLC;
using System.Globalization;
using WtmModelBase;
using NBaseCommon;
using WtmData;

namespace Sim
{
    public partial class 予想労働時間Form : ExForm
    {
        private static bool DEBUG = false;

        private DateTime TODAY = DateTime.Today;
        private DateTime NOW = DateTime.Now;
        private void SetNOW()
        {
            //NOW = new DateTime(TODAY.Year, TODAY.Month, TODAY.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            label_DEBUG_DATETIME.Text = $"現在時刻：{NOW.ToString("yyyy/MM/dd HH:mm")}";
        }





        /// <summary>
        /// インスタンスを格納するフィールド変数
        /// </summary>
        private static 予想労働時間Form instance = null;

        /// <summary>
        /// インスタンスを取得するためのメソッド
        /// Windows フォームをシングルトン対応にするコード
        /// </summary>
        /// <returns></returns>
        public static 予想労働時間Form GetInstance()
        {
            if (instance == null)
            {
                instance = new 予想労働時間Form();
            }
            return instance;
        }

        private void 予想労働時間Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }



        //Renderと作業内容を関連付ける
        private List<CalendarRender> RenderList = new List<CalendarRender>();
        private Dictionary<int, CalendarRectangleShapeRenderer> DevRenderDic = new Dictionary<int, CalendarRectangleShapeRenderer>();

        private CalendarRectangleShapeRenderer RenderWhite;

        private List<int> DateLinkColumn = new List<int>();

        private List<Appointment> AppointmentList = new List<Appointment>();

        private List<WorkContent> WorkContentList = null;

        private List<MsSenin> SeninList = null;
        private List<MsSenin> SeninViewList = null;//コンボボックスで絞り込まれたリスト

        private List<Work> WorkList = null;


        //労働パターンForm NavigationPatternForm = null;
        //労働パターン入出港Form EventPatternForm = null;




        private 予想労働時間Form()
        {
            InitializeComponent();

            gcCalendarGrid1.CurrentCellPositionChanged += gcCalendarGrid1_CurrentCellPositionChanged;
        }

        private void 予測労働時間Form_Load(object sender, EventArgs e)
        {
            SetNOW();

            SearchVessel();
            SearchWorkContent();


            MakeRender();

            int numperH = 4;
            int colwidth = 15;
            //int colwidth = 16;

            gcCalendarGrid1.Template.AddColumn(numperH * 24);

            int mperH = 60 / numperH;

            int indexH = 0;
            int indexM = 0;
            for (int i = 0; i < 24; i++)
            {
                indexH = i * numperH;
                gcCalendarGrid1.Template.ColumnHeader.Columns[indexH].Width = colwidth * numperH;
                gcCalendarGrid1.Template.ColumnHeader[0, indexH].Value = i.ToString();
                gcCalendarGrid1.Template.ColumnHeader[0, indexH].ColumnSpan = numperH;

                for (int j = 0; j < numperH; j++)
                {
                    indexM = indexH + j;

                    int num = j * mperH;
                    gcCalendarGrid1.Template.ColumnHeader.Columns[indexM].Width = colwidth;
                    gcCalendarGrid1.Template.ColumnHeader[1, indexM].Value = num.ToString();
                    gcCalendarGrid1.Template.ColumnHeader[1, indexM].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;

                    DateLinkColumn.Add(i);

                    if (j == 0)
                    {
                        gcCalendarGrid1.Template.ColumnHeader[1, indexM].CellStyle.LeftBorder = new CalendarBorderLine(Color.Gray, BorderLineStyle.Thin);
                        gcCalendarGrid1.Template.Content.Columns[indexM].CellStyle.LeftBorder = new CalendarBorderLine(Color.Gray, BorderLineStyle.Thin);
                    }
                }

                if (i != 0)
                {
                    gcCalendarGrid1.Template.ColumnHeader[0, indexH].CellStyle.LeftBorder = new CalendarBorderLine(Color.Gray, BorderLineStyle.Thin);
                    
                }
            }

            dateTimePicker1.Value = TODAY;

            gcCalendarGrid1.Enabled = false;


            label_動静1_港.Visible = textBox_動静1_港.Visible = false;
            label_動静1_入港.Visible = textBox_動静1_入港.Visible = false;
            label_動静1_出港.Visible = textBox_動静1_出港.Visible = false;

            label_動静2_港.Visible = textBox_動静2_港.Visible = false;
            label_動静2_入港.Visible = textBox_動静2_入港.Visible = false;
            label_動静2_出港.Visible = textBox_動静2_出港.Visible = false;

            label1_動静_停泊.Visible = false;



            SearchSenin();
        }

        private void SetDataSenin()
        {
            //一人分の行数
            int rowPerSenin = 6;

            //クリア
            int delrowcnt = gcCalendarGrid1.Template.RowCount;

            if (delrowcnt > 0)
            {
                gcCalendarGrid1.Template.RemoveRow(0, delrowcnt);
            }

            if (SeninViewList.Count() > 0)
            {
                //行数用意
                gcCalendarGrid1.Template.AddRow(SeninViewList.Count() * rowPerSenin);

                //グリッドに職名、名前セット
                for( int i = 0;  i < SeninViewList.Count(); i++)
                {
                    int index = i * rowPerSenin;


                    //職名、名前
                    gcCalendarGrid1.RowHeader[0][index, 0].RowSpan = rowPerSenin;//船員のクラス
                    gcCalendarGrid1.RowHeader[0][index, 1].RowSpan = rowPerSenin;//職名
                    gcCalendarGrid1.RowHeader[0][index, 2].RowSpan = rowPerSenin;//名前

                    string shokumei = SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, SeninViewList[i].MsSiShokumeiID);

                    gcCalendarGrid1.RowHeader[0][index, 0].Value = SeninViewList[i];//船員クラス
                    gcCalendarGrid1.RowHeader[0][index, 1].Value = shokumei;
                    gcCalendarGrid1.RowHeader[0][index, 2].Value =SeninViewList[i].Sei + " " + SeninViewList[i].Mei;

                    //職名、名前セルの書式
                    gcCalendarGrid1.RowHeader[0][index, 1].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;
                    gcCalendarGrid1.RowHeader[0][index, 2].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;

                    //項目に値書く
                    gcCalendarGrid1.RowHeader[0][index, 3].ColumnSpan = 2;
                    gcCalendarGrid1.RowHeader[0][index+1, 3].ColumnSpan = 2;

                    gcCalendarGrid1.RowHeader[0][index, 3].Value = "予定";
                    gcCalendarGrid1.RowHeader[0][index+1, 3].Value = "実績";

                    gcCalendarGrid1.RowHeader[0][index + 2, 3].Value = "D";
                    gcCalendarGrid1.RowHeader[0][index + 3, 3].Value = "D";
                    gcCalendarGrid1.RowHeader[0][index + 4, 3].Value = "D";
                    gcCalendarGrid1.RowHeader[0][index + 5, 3].Value = "D";

                    gcCalendarGrid1.RowHeader[0][index + 2, 4].Value = "24";
                    gcCalendarGrid1.RowHeader[0][index + 3, 4].Value = "1W";
                    gcCalendarGrid1.RowHeader[0][index + 4, 4].Value = "4W";
                    gcCalendarGrid1.RowHeader[0][index + 5, 4].Value = "休";

                    //項目のセルの書式
                    gcCalendarGrid1.RowHeader[0][index, 3].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;
                    gcCalendarGrid1.RowHeader[0][index+1, 3].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;

                    gcCalendarGrid1.RowHeader[0][index + 2, 3].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;
                    gcCalendarGrid1.RowHeader[0][index + 3, 3].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;
                    gcCalendarGrid1.RowHeader[0][index + 4, 3].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;
                    gcCalendarGrid1.RowHeader[0][index + 5, 3].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;

                    gcCalendarGrid1.RowHeader[0][index + 2, 4].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;
                    gcCalendarGrid1.RowHeader[0][index + 3, 4].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;
                    gcCalendarGrid1.RowHeader[0][index + 4, 4].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;
                    gcCalendarGrid1.RowHeader[0][index + 5, 4].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;

                    //行の高さ
                    gcCalendarGrid1.Template.Content.Rows[index].Height = 21;
                    gcCalendarGrid1.Template.Content.Rows[index + 1].Height = 21;
                    gcCalendarGrid1.Template.Content.Rows[index + 2].Height = 11;
                    gcCalendarGrid1.Template.Content.Rows[index + 3].Height = 11;
                    gcCalendarGrid1.Template.Content.Rows[index + 4].Height = 11;
                    gcCalendarGrid1.Template.Content.Rows[index + 5].Height = 11;

                    //実績だけ背景色
                    //gcCalendarGrid1.Template.Content.Rows[index + 1].CellStyle.BackColor = Color.FromArgb(247, 247, 247);

                    //実線引く
                    gcCalendarGrid1.RowHeader[0][index, 1].CellStyle.BottomBorder = new CalendarBorderLine(Color.Black, BorderLineStyle.Thin);
                    gcCalendarGrid1.RowHeader[0][index, 2].CellStyle.BottomBorder = new CalendarBorderLine(Color.Black, BorderLineStyle.Thin);
                    gcCalendarGrid1.RowHeader[0].Rows[index + 5].CellStyle.BottomBorder = new CalendarBorderLine(Color.Black, BorderLineStyle.Thin);
                    gcCalendarGrid1.Template.Content.Rows[index + 5].CellStyle.BottomBorder = new CalendarBorderLine(Color.Black, BorderLineStyle.Thin);
                }
            }

        }

        private void SearchVessel()
        {
            var vesselList = (List<MsVessel>)null;

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    vesselList = serviceClient.MsVessel_GetRecordsBySeninEnabled(NBaseCommon.Common.LoginUser);

                }
            }, "データ取得中です...");

            progressDialog.ShowDialog();


            comboBox_Vessel.Items.Clear();
            vesselList.ForEach(o => { comboBox_Vessel.Items.Add(o); });
        }

        /// <summary>
        /// 作業内容検索
        /// </summary>
        #region private void SearchWorkContent()
        private void SearchWorkContent()
        {
            WorkContentList = new List<WorkContent>();

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    String appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                    //WorkContentList = serviceClient.BLC_GetWorkContents(NBaseCommon.Common.LoginUser, appName);
                    WorkContentList = WtmAccessor.Instance().GetWorkContents();
                }
            }, "データ取得中です...");

            progressDialog.ShowDialog();


            foreach(WorkContent wc in WorkContentList)
            {
                WorkContentPanel wcp = new WorkContentPanel(wc);
                flowLayoutPanel1.Controls.Add(wcp);
            }

        }
        #endregion

        /// <summary>
        /// 船員リスト検索
        /// </summary>
        #region private void SearchSenin()
        private void SearchSenin()
        {
            SeninList = new List<MsSenin>();

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    SeninList = serviceClient.MsSenin_GetRecords(NBaseCommon.Common.LoginUser);
                }
            }, "データ取得中です...");

            progressDialog.ShowDialog();

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




            foreach (WorkContent wc in WorkContentList)
            {
                CalendarRender rend = new CalendarRender();

                Color f = ColorTranslator.FromHtml(wc.FgColor);
                Color b = ColorTranslator.FromHtml(wc.BgColor);

                rend.RendarD = new CalendarRoundedRectangleShapeRenderer();
                //rend.RendarD.RoundedRadius = 0.4f;
                rend.RendarD.FillColor = b;
                rend.RendarD.LineColor = b;
                rend.RendarD.LineStyle = CalendarShapeLineStyle.Thin;
                rend.RendarD.LineWidth = 1;

                rend.RendarL = new CalendarRoundedRectangleShapeRenderer();
                //rend.RendarL.RoundedRadius = 0.4f;
                rend.RendarL.FillColor = ColorExtension.GetLightColor(b);
                rend.RendarL.LineColor = b; //ColorExtension.GetLightColor(b);
                rend.RendarL.LineStyle = CalendarShapeLineStyle.Thin;
                rend.RendarL.LineWidth = 1;

                rend.WorkContentId = wc.WorkContentID;

                RenderList.Add(rend);
            }
        }
        #endregion




        CalendarAppointmentCellType Cactype1 = new CalendarAppointmentCellType();

        private void gcCalendarGrid1_CellMouseClick(object sender, CalendarCellMouseEventArgs e)
        {
#if いらないイベント
            //セル位置
            CalendarCellPosition cp = e.CellPosition;
            DateTime dt = cp.Date;

            //クリックされた場所がコンテンツ以外は抜ける
            if (!(cp.Scope == CalendarTableScope.Content)) return;

            //既にアポイントがあれば抜ける
            if (gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value is Appointment)
            {
                return;
            }

            Appointment newap = new Appointment();
            newap.StartColumnIndex = cp.ColumnIndex;
            newap.EndColumnIndex = cp.ColumnIndex;
            newap.RowIndex = cp.RowIndex;
            newap.CurDate = dt;
            gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value = newap;

            PutAppointment(newap, true);


            //System.Diagnostics.Debug.WriteLine("MouseClick:cp.ColumnIndex="+cp.ColumnIndex.ToString() );
#endif
        }

        private void gcCalendarGrid1_CellMouseDoubleClick(object sender, CalendarCellMouseEventArgs e)
        {
#if いらないイベント
            //セル位置
            CalendarCellPosition cp = e.CellPosition;
            DateTime dt = cp.Date;

            //クリックされた場所がコンテンツ以外は抜ける
            if (!(cp.Scope == CalendarTableScope.Content)) return;

            //アポインが無いならぬける
            if (!(gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value is Appointment))
            {
                return;
            }

            Appointment ap = gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value as Appointment;

            詳細画面を開く(ap);
#endif
        }

        //
        //Renderのドラッギングで使用する変数
        //
        int CellDraggingFlg = 0;//バーを伸ばしている時=1
        Appointment CellDraggingAppointment = null;//ドラッギングを始めた時のRenderと関連するAppointmentを保持

        CalendarCellPosition CellPositionLastDragging = new CalendarCellPosition();

        /// <summary>
        /// Renderのドラッギング アポイントメント伸ばしている時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region protected void gcCalendarGrid1_AppointmentCellDragging(object sender, AppointmentCellDraggingEventArgs e)
        protected void gcCalendarGrid1_AppointmentCellDragging(object sender, AppointmentCellDraggingEventArgs e)
        {

            //最初にRenderをつかんだ時に、Appointmentをキープしておく
            if (CellDraggingFlg == 0)
            {
                DateTime dt = e.StartCellPosition.Date;
                int colindex = e.StartCellPosition.ColumnIndex;
                int rowindex = e.StartCellPosition.RowIndex;

                //string t = gcCalendarGrid1.Content[startdt][rowindex, colindex].Value.GetType().ToString();
                //System.Diagnostics.Debug.WriteLine("gcCalendarGrid1_AppointmentCellDragging():" + t);

                if (gcCalendarGrid1.Content[dt][rowindex, colindex].Value is Appointment)
                    CellDraggingAppointment = gcCalendarGrid1.Content[dt][rowindex, colindex].Value as Appointment;
                else
                    CellDraggingAppointment = new Appointment();
            }

            CellDraggingFlg = 1;
            CellPositionLastDragging = e.TargetCellPosition;

        }
        #endregion

        /// <summary>
        /// カレンダーのセルマウスアップ  Appointmentを離した時に詳細開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region protected void gcCalendarGrid1_CellMouseUp(object sender, CalendarCellMouseEventArgs e)
        protected void gcCalendarGrid1_CellMouseUp(object sender, CalendarCellMouseEventArgs e)
        {
#if いらないイベント
            //Renderをドラッギングしていた場合
            if (CellDraggingFlg == 1)
            {
                //System.Diagnostics.Debug.WriteLine("gcCalendarGrid1_CellMouseUp():" + CellDraggingAppointment.SiCardPlanID);
                //最初につかんだRenderのAppointmentを渡す
                int colspan = CellDraggingAppointment.EndColumnIndex + 1 - CellDraggingAppointment.StartColumnIndex;
                gcCalendarGrid1.Content[CellDraggingAppointment.CurDate][CellDraggingAppointment.RowIndex, CellDraggingAppointment.StartColumnIndex].ColumnSpan = colspan;

                詳細画面を開く(CellDraggingAppointment);
            }
            CellDraggingFlg = 0;
            CellDraggingAppointment = null;
#endif
        }
        #endregion


        private void 詳細画面を開く( Appointment ap)
        {
#if いらないイベント
           //選択されているAppointmentの情報
            {
                int rowindex = ap.RowIndex;
                int colindex = ap.StartColumnIndex;
                DateTime dt = ap.CurDate;

                int a = gcCalendarGrid1[dt][rowindex, colindex].ColumnSpan;
            }

            Appointment targetP = null;


            #region 対象となるAppointmentを得る、変更可能か調べる
            if (ap == null) //ダブルクリックはここにくる。計画の場合、実績の場合がある
            {
                int rowindex = gcCalendarGrid1.SelectedCells[0].RowIndex;
                int colindex = gcCalendarGrid1.SelectedCells[0].ColumnIndex;
                DateTime dt = gcCalendarGrid1.SelectedCells[0].Date;

                targetP = gcCalendarGrid1.Content[dt][rowindex, colindex].Value as Appointment;
                
            }
            else//ドラッグでRederを離したときはここにくる。計画のみ
            {
                targetP = ap;
                targetP.EndColumnIndex = CellPositionLastDragging.ColumnIndex;

                int span = (ap.EndColumnIndex + 1) - ap.StartColumnIndex;
                gcCalendarGrid1[ap.CurDate][ap.RowIndex, ap.StartColumnIndex].ColumnSpan = span;


            }
            #endregion

            //詳細画面表示
            string strstart = gcCalendarGrid1.Template.ColumnHeader[1, targetP.StartColumnIndex].Value.ToString();
            string strend = gcCalendarGrid1.Template.ColumnHeader[1, targetP.EndColumnIndex].Value.ToString();

            strstart = strstart.Length == 1 ? "0" + strstart : strstart;
            strend = strend.Length == 1 ? "0" + strend : strend;

            strstart = DateLinkColumn[targetP.StartColumnIndex].ToString("00") + ":" + strstart;
            strend = DateLinkColumn[targetP.EndColumnIndex].ToString("00") + ":" + strend;

            //Form3 frm = new Form3(targetP.CurDate, strstart, strend);
            //frm.ShowDialog();

            AppointmentList.Add(targetP);

            SetDataPlan();
#endif
        }






        public void PutAppointment(Appointment ap, int seninId = 0)
        {
            int index = -1;
            
            if (ap.MsSeninID > 0)
            {
                index = GetIndexRowSenin(ap.MsSeninID);
            }
            else if (seninId > 0)
            {
                index = GetIndexRowSenin(seninId);
            }
            if (index == -1)
                return;


            if (ap.MsSeninID > 0 && ap.WorkContentID != null)
            {
                index += 1; // 実績
            }
            else if (ap.DeviationKind == 1)
            {
                index += 2;
            }
            else if (ap.DeviationKind == 2)
            {
                index += 3;
            }
            else if (ap.DeviationKind == 3)
            {
                index += 4;
            }
            else if (ap.DeviationKind == 4)
            {
                index += 5;
            }

            int colIndex = 0;

            int h = ap.WorkDate.Hour;
            colIndex = h * 4;

            int t = ap.WorkDate.Minute;
            colIndex += t / 15;

            CalendarAppointmentCellType cactype1 = new CalendarAppointmentCellType();

            //セルタイプクローン、必須。おやくそく
            gcCalendarGrid1.Content[ap.WorkDate][index, colIndex].CellType = cactype1.Clone();

            //太字
            Font baseFont = gcCalendarGrid1.Font;
            Font fnt = new Font(baseFont.FontFamily, baseFont.Size, baseFont.Style | FontStyle.Bold);
            gcCalendarGrid1.Content[ap.WorkDate][index, colIndex].CellStyle.Font = fnt;

            //計画とAppointmentの紐づけ
            this.gcCalendarGrid1.Content[ap.WorkDate][index, colIndex].Value = ap;


            gcCalendarGrid1.Content[ap.WorkDate][index, colIndex].CellStyle.ForeColor = Color.Black;

            if (ap.DeviationKind > 0)
            {
                (this.gcCalendarGrid1[ap.WorkDate][index, colIndex].CellType as CalendarAppointmentCellType).Renderer = DevRenderDic[ap.DeviationKind];
            }
            else
            {
                //(this.gcCalendarGrid1[ap.WorkDate][index, colIndex].CellType as CalendarAppointmentCellType).Renderer = RenderList.Where(o => o.WorkContentId == ap.WorkContentID).FirstOrDefault().RendarD;
                if (ap.CheckTarget)
                {
                    // 予定パターンからの内訳
                    (this.gcCalendarGrid1[ap.WorkDate][index, colIndex].CellType as CalendarAppointmentCellType).Renderer = RenderList.Where(o => o.WorkContentId == ap.WorkContentID).FirstOrDefault().RendarL;
                }
                else
                {
                    // 実績内訳
                    (this.gcCalendarGrid1[ap.WorkDate][index, colIndex].CellType as CalendarAppointmentCellType).Renderer = RenderList.Where(o => o.WorkContentId == ap.WorkContentID).FirstOrDefault().RendarD;
                }
            }
        }



        private void SetDataPlan()
        {
            gcCalendarGrid1.Content.ClearAll();

            foreach (MsSenin senin in SeninViewList)
            {
                int shokumeiId = SeninViewList.Where(o => o.MsSeninID == senin.MsSeninID).First().MsSiShokumeiID;
                var patternList = AppointmentList.Where(o => o.MsSiShokumeiID == shokumeiId);

                if (patternList != null)
                {
                    foreach (Appointment pap in patternList)
                    {
                        PutAppointment(pap, senin.MsSeninID);
                    };
                }

                var seninList = AppointmentList.Where(o => o.MsSeninID > 0);
                if (seninList != null)
                {
                    foreach (Appointment ap in seninList)
                    {
                        if (ap.Visibled)
                            PutAppointment(ap);
                    };
                }


                // ２０２３／０３／０２に川崎さん要望があったので、出力するメソッド作成
                //Output(senin, patternList, seninList);
            }

        }



        private void Output(MsSenin senin, IEnumerable<Appointment> patternList, IEnumerable<Appointment> seninList)
        {
            //　該当日＋船名をファイル名にする
            DateTime date = dateTimePicker1.Value;
            MsVessel v = comboBox_Vessel.SelectedItem as MsVessel;
            string fileName = date.Year + date.Month.ToString("00") + date.Day.ToString("00") + "_" + v.VesselName + ".txt";

            string basePath = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string filePath = basePath + "\\" + fileName;


            StreamWriter fsw = new StreamWriter(new FileStream(filePath, FileMode.Append));


            string yStr = "";
            string jStr = "";
            foreach (WorkContent wc in WorkContentList)
            {
                var yCount = patternList.Where(o => o.WorkContentID == wc.WorkContentID).Count();
                yStr += wc.Name + ":" + yCount.ToString() + ",";

                var jCount = seninList.Where(o => o.WorkContentID == wc.WorkContentID && o.DeviationKind == 0 && o.MsSeninID == senin.MsSeninID).Count();
                jStr += wc.Name + ":" + jCount.ToString() + ",";
            }


            fsw.WriteLine($"{date.Month}月{date.Day}日,{v.VesselName},{SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, senin.MsSiShokumeiID)},{senin.FullName},予定,{yStr}");
            fsw.WriteLine($"{date.Month}月{date.Day}日,{v.VesselName},{SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, senin.MsSiShokumeiID)},{senin.FullName},実績,{jStr}");


            fsw.Close();

        }



        private void gcCalendarGrid1_CurrentCellPositionChanged(object sender, CalendarCellMoveEventArgs e)
        {
            //this.gcCalendarGrid2.CurrentDate = e.CellPosition.Date;
            dateTimePicker1.Value = e.CellPosition.Date;
        }

        private void gcCalendarGrid2_CurrentCellPositionChanged(object sender, CalendarCellMoveEventArgs e)
        {
            this.gcCalendarGrid1.CurrentDate = e.CellPosition.Date;
        }

        private void gcCalendarGrid1_CellContentClick(object sender, CalendarCellEventArgs e)
        {
            ////セル位置
            //CalendarCellPosition cp = e.CellPosition;
            //DateTime dt = cp.Date;

            //Appointment p = gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value as Appointment;
            //if (p != null)
            //{
            //    string str = "日付：" + p.CurDate.ToShortDateString() + "\n";
            //    str =str + "情報１:" + p.StartColumnIndex.ToString()+"\n";
            //    str = str + "備考：";
                
            //    gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].ToolTipText = str;
            //}
        }





        /// <summary>
        /// 「日付」選択操作時のコールバック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.gcCalendarGrid1.CurrentDate = dateTimePicker1.Value;
            ValueChange();
        }

        private void button_PrevDay_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-1);
        }

        private void button_NextDay_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1);
        }

        /// <summary>
        /// 「船」選択操作時のコールバック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Vessel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValueChange();
        }



        /// <summary>
        /// 日付、船をもとに検索、描画をする
        /// </summary>
        private void ValueChange()
        {

            SetNOW();



            if (!(comboBox_Vessel.SelectedItem is MsVessel))
                return;

            gcCalendarGrid1.Enabled = true;

            DateTime d = dateTimePicker1.Value;
            MsVessel v = comboBox_Vessel.SelectedItem as MsVessel;


            this.Cursor = Cursors.WaitCursor;

            try
            {
                SeninViewList = new List<MsSenin>();


                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    if (DateTimeUtils.ToFrom(d) > TODAY)
                    {
                        // 乗船計画(SiCardPlan)から
                    }

                    if (SeninViewList.Count() == 0)
                    {
                        // 該当日が、当日を含む過去、または、乗船計画がない場合
                        // 乗船実績(SiCard）から
                        SiCardFilter filter = new SiCardFilter();
                        filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser, MsSiShubetsu.SiShubetsu.乗船));
                        filter.Start = DateTimeUtils.ToFrom(d);
                        filter.End = DateTimeUtils.ToTo(d);
                        filter.MsVesselIDs.Add(v.MsVesselID);

                        var cards = serviceClient.BLC_船員カード検索(NBaseCommon.Common.LoginUser, filter);


                        // 乗船職に置き換える
                        var sortedShokumeiList = SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser).OrderBy(o => o.ShowOrder);
                        foreach (MsSiShokumei shokumei in sortedShokumeiList)
                        {
                            var targetCards = cards.Where(o => o.SiLinkShokumeiCards[0].MsSiShokumeiID == shokumei.MsSiShokumeiID);
                            var onCrewId = targetCards.Select(o => o.MsSeninID).Distinct();

                            var senins = SeninList.Where(o => onCrewId.Contains(o.MsSeninID));
                            if (senins != null)
                            {
                                foreach(MsSenin senin in senins)
                                {
                                    senin.MsSiShokumeiID = shokumei.MsSiShokumeiID;

                                    SeninViewList.Add(senin);
                                }

                            }
                        }
                    }

                    String appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                    //WorkList = serviceClient.BLC_GetWorks(NBaseCommon.Common.LoginUser, appName, v.MsVesselID, d, DateTime.MinValue);
                    WorkList = WtmAccessor.Instance().GetWorks(d, d, vesselId: v.MsVesselID);
                }




                // 該当日の動静情報
                var douseiInfos = GetDouseiInfos(v, d);
                SetDouseiInfo(douseiInfos);



                // 作業パターン
                var list = makePatternList(v, d, douseiInfos);

                AppointmentList = new List<Appointment>();
                AppointmentList.AddRange(list);




                // 各船員の作業内訳
                foreach (Work w in WorkList)
                {
                    // 船員の作業内訳
                    foreach (WorkContentDetail wd in w.WorkContentDetails)
                    {
                        // 現在時刻までの実績を有効データとする
                        if ((d == TODAY && wd.WorkDate < NOW) || (d < TODAY && DateTimeUtils.ToFrom(wd.WorkDate) == d))
                        {
                            Appointment apo = new Appointment();
                            apo.MsSeninID = int.Parse(w.CrewNo);
                            apo.WorkContentID = wd.WorkContentID;
                            apo.WorkDate = wd.WorkDate;

                            AppointmentList.Add(apo);
                        }
                    }
                    
                    // 船員のDeviation
                    if (DEBUG)
                    {
                    }
                    else
                    {
                        foreach (Deviation dev in w.Deviations)
                        {
                            // 現在時刻までの実績を有効データとする
                            if ((d == TODAY && dev.WorkDate < NOW) || (d < TODAY && DateTimeUtils.ToFrom(dev.WorkDate) == d))
                            {
                                Appointment apo = new Appointment();
                                apo.MsSeninID = int.Parse(w.CrewNo);
                                apo.DeviationKind = dev.Kind;
                                apo.WorkDate = dev.WorkDate;

                                AppointmentList.Add(apo);
                            }
                        }
                    }
                }


                if (DEBUG)
                {
                    System.Diagnostics.Debug.WriteLine($"  ４週分のWork:検索開始：{DateTime.Now.ToString("HH:mm:ss")}");
                    // deviation判定のため、４週間分のデータを取得する
                    List<Work> devationCheckWorkList = null;
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        DateTime d1 = d.AddMonths(-1).AddDays(-2);
                        DateTime d2 = d;

                        String appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                        //devationCheckWorkList = serviceClient.BLC_GetWorks(NBaseCommon.Common.LoginUser, appName, v.MsVesselID, d1, d2);
                        devationCheckWorkList = WtmAccessor.Instance().GetWorks(d1, d2, vesselId: v.MsVesselID);

                        devationCheckWorkList = devationCheckWorkList.Where(o => o.StartWork < NOW).ToList(); // 現在時刻までに開始しているもの
                        devationCheckWorkList.ForEach(o => { if (o.FinishWork > NOW) o.FinishWork = NOW; });     // 終了が現在時刻以降の場合、現在時刻で置き換える
                    }
                    System.Diagnostics.Debug.WriteLine($"  ４週分のWork:検索終了：{DateTime.Now.ToString("HH:mm:ss")}");

                    // Deviation
                    deviationProc(v, d, devationCheckWorkList);
                }
                else
                {
                    if (DateTimeUtils.ToFrom(d) >= TODAY)
                    {
                        System.Diagnostics.Debug.WriteLine($"  ４週分のWork:検索開始：{DateTime.Now.ToString("HH:mm:ss")}");
                        // deviation判定のため、４週間分のデータを取得する
                        List<Work> devationCheckWorkList = null;
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            DateTime d1 = d.AddMonths(-1).AddDays(-2);
                            DateTime d2 = d;

                            String appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                            //devationCheckWorkList = serviceClient.BLC_GetWorks(NBaseCommon.Common.LoginUser, appName, v.MsVesselID, d1, d2);
                            devationCheckWorkList = WtmAccessor.Instance().GetWorks(d1, d2, vesselId: v.MsVesselID);

                            devationCheckWorkList = devationCheckWorkList.Where(o => o.StartWork < NOW).ToList(); // 現在時刻までに開始しているもの
                            devationCheckWorkList.ForEach(o =>{if (o.FinishWork > NOW) o.FinishWork = NOW;});     // 終了が現在時刻以降の場合、現在時刻で置き換える
                        }
                        System.Diagnostics.Debug.WriteLine($"  ４週分のWork:検索終了：{DateTime.Now.ToString("HH:mm:ss")}");



                        // 今日～該当日までを構築
                        int days = (int)(DateTimeUtils.ToFrom(d) - TODAY).TotalDays;
                        for (int i = 0; i <= days; i++)
                        {
                            DateTime diffDate = TODAY.AddDays(i);

                            var dinfos = GetDouseiInfos(v, diffDate);
                            var diffList = makePatternList(v, diffDate, dinfos);
                            diffList = diffList.Where(o => o.WorkDate.AddMinutes(15) >= NOW).ToList();

                            // Deviation算出のため、パターンを、作業、作業内訳として設定
                            foreach (MsSenin senin in SeninViewList)
                            {
                                int shokumeiId = SeninViewList.Where(o => o.MsSeninID == senin.MsSeninID).First().MsSiShokumeiID;

                                var lastApo = (Appointment)null;
                                if (AppointmentList.Any(o => o.MsSeninID == senin.MsSeninID))
                                    lastApo = AppointmentList.Where(o => o.MsSeninID == senin.MsSeninID).OrderBy(o => o.WorkDate).Last();

                                var patternList = (IEnumerable<Appointment>)null;
                                if (lastApo != null)
                                {
                                    patternList = diffList.Where(o => o.MsSiShokumeiID == shokumeiId && o.WorkDate > lastApo.WorkDate);
                                }
                                else
                                {
                                    patternList = diffList.Where(o => o.MsSiShokumeiID == shokumeiId);
                                }

                                foreach (Appointment patternApo in patternList)
                                {
                                    DateTime wkDate = new DateTime(diffDate.Year, diffDate.Month, diffDate.Day, patternApo.WorkDate.Hour, patternApo.WorkDate.Minute, patternApo.WorkDate.Second);
                                    Work wk = new Work();
                                    wk.CrewNo = senin.MsSeninID.ToString();
                                    wk.StartWork = wkDate;
                                    wk.FinishWork = wkDate.AddMinutes(15);

                                    devationCheckWorkList.Add(wk);



                                    Appointment apo = new Appointment();

                                    //apo.Visibled = false; // Deviationを算出するためなので、表示しない
                                    apo.CheckTarget = true;

                                    apo.MsSeninID = senin.MsSeninID;
                                    apo.WorkContentID = patternApo.WorkContentID;
                                    apo.WorkDate = wkDate;

                                    AppointmentList.Add(apo);
                                }
                            }
                        }

                        // Deviation
                        deviationProc(v, d, devationCheckWorkList);


                    }
                }


                // グリッドに表示
                SetDataSenin();
                SetDataPlan();

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }



        }


        private List<DouseiInfo> GetDouseiInfos(MsVessel vessel, DateTime date)
        {
            List<DouseiInfo> douseiInfos = new List<DouseiInfo>();

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                // 該当日の動静情報
                var djDousei = serviceClient.DjDousei_GetRecords(NBaseCommon.Common.LoginUser, vessel.MsVesselID, date);
                djDousei = djDousei.OrderBy(o => o.DouseiDate).ToList();


                bool isPast = date < TODAY;
                string dateStr = date.ToShortDateString();

                foreach (DjDousei ds in djDousei)
                {
                    if (ds.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.積みID || ds.MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.揚げID)
                    {
                        if (isPast && (ds.ResultNyuko.ToShortDateString() == dateStr || ds.ResultShukou.ToShortDateString() == dateStr))
                        {
                            DouseiInfo info = new DouseiInfo();

                            info.BashoId = ds.ResultMsBashoID;
                            info.BashoName = ds.ResultBashoName;

                            if (ds.ResultNyuko.ToShortDateString() == dateStr)
                                info.InTime = ds.ResultNyuko;

                            if (ds.ResultChakusan.ToShortDateString() == dateStr)
                                info.Docking = ds.ResultChakusan;

                            if (ds.ResultShukou.ToShortDateString() == dateStr)
                                info.OutTime = ds.ResultShukou;

                            douseiInfos.Add(info);

                        }
                        else if((ds.PlanNyuko.ToShortDateString() == dateStr || ds.PlanShukou.ToShortDateString() == dateStr))
                        {
                            DouseiInfo info = new DouseiInfo();

                            info.BashoId = ds.MsBashoID;
                            info.BashoName = ds.BashoName;

                            if (ds.PlanNyuko.ToShortDateString() == dateStr)
                                info.InTime = ds.PlanNyuko;

                            if (ds.PlanChakusan.ToShortDateString() == dateStr)
                                info.Docking = ds.PlanChakusan;

                            if (ds.PlanShukou.ToShortDateString() == dateStr)
                                info.OutTime = ds.PlanShukou;

                            douseiInfos.Add(info);
                        }


                    }
                    else if (djDousei.Count == 1 && djDousei[0].MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.待機ID)
                    {
                        DouseiInfo info = new DouseiInfo();
                        info.WaitingInfo = true;

                        info.BashoId = ds.MsBashoID;
                        info.BashoName = ds.BashoName;

                        douseiInfos.Add(info);
                    }
                }
            }

            return douseiInfos;
        }

        private void SetDouseiInfo(List<DouseiInfo> douseiInfos)
        {
            textBox_動静1_港.Text = "";
            textBox_動静1_入港.Text = "";
            textBox_動静1_出港.Text = "";

            textBox_動静2_港.Text = "";
            textBox_動静2_入港.Text = "";
            textBox_動静2_出港.Text = "";


            if (douseiInfos.Count() == 1 && douseiInfos[0].WaitingInfo)
            {
                label_動静1_港.Visible = textBox_動静1_港.Visible = true;
                label_動静1_入港.Visible = textBox_動静1_入港.Visible = false;
                label_動静1_出港.Visible = textBox_動静1_出港.Visible = false;

                label_動静2_港.Visible = textBox_動静2_港.Visible = false;
                label_動静2_入港.Visible = textBox_動静2_入港.Visible = false;
                label_動静2_出港.Visible = textBox_動静2_出港.Visible = false;

                textBox_動静1_港.Text = douseiInfos[0].BashoName;
                label1_動静_停泊.Visible = true;
            }
            else
            {
                int count = 0;
                foreach (DouseiInfo dinfo in douseiInfos)
                {
                    if (count == 0)
                    {
                        textBox_動静1_港.Text = dinfo.BashoName;
                        if (dinfo.InTime != null)
                        {
                            textBox_動静1_入港.Text = ((DateTime)dinfo.InTime).ToShortTimeString();
                        }
                        if (dinfo.OutTime != null)
                        {
                            textBox_動静1_出港.Text = ((DateTime)dinfo.OutTime).ToShortTimeString();
                        }

                        count++;

                    }
                    else if (count == 1)
                    {
                        textBox_動静2_港.Text = dinfo.BashoName;
                        if (dinfo.InTime != null)
                        {
                            textBox_動静2_入港.Text = ((DateTime)dinfo.InTime).ToShortTimeString();
                        }
                        if (dinfo.OutTime != null)
                        {
                            textBox_動静2_出港.Text = ((DateTime)dinfo.OutTime).ToShortTimeString();
                        }

                        count++;

                    }
                }
                if (count == 0)
                {
                    label_動静1_港.Visible = textBox_動静1_港.Visible = false;
                    label_動静1_入港.Visible = textBox_動静1_入港.Visible = false;
                    label_動静1_出港.Visible = textBox_動静1_出港.Visible = false;

                    label_動静2_港.Visible = textBox_動静2_港.Visible = false;
                    label_動静2_入港.Visible = textBox_動静2_入港.Visible = false;
                    label_動静2_出港.Visible = textBox_動静2_出港.Visible = false;
                }
                else
                {
                    label_動静1_港.Visible = textBox_動静1_港.Visible = true;
                    label_動静1_入港.Visible = textBox_動静1_入港.Visible = true;
                    label_動静1_出港.Visible = textBox_動静1_出港.Visible = true;

                    label_動静2_港.Visible = textBox_動静2_港.Visible = true;
                    label_動静2_入港.Visible = textBox_動静2_入港.Visible = true;
                    label_動静2_出港.Visible = textBox_動静2_出港.Visible = true;

                }
                label1_動静_停泊.Visible = false;
            }


            //



            //==========================================================================================
            //==========================================================================================
            //==========================================================================================
            //==========================================================================================
            //==========================================================================================
            clearDousei();
            foreach (DouseiInfo dinfo in douseiInfos)
            {
                if (dinfo.WaitingInfo)
                    continue;

                if (dinfo.InTime != null)
                    SetDousei((DateTime)dinfo.InTime, "入");
                if (dinfo.Docking != null)
                    SetDousei((DateTime)dinfo.Docking, "着");
                if (dinfo.OutTime != null)
                    SetDousei((DateTime)dinfo.OutTime, "出");

            }
            //==========================================================================================
            //==========================================================================================
            //==========================================================================================
            //==========================================================================================
            //==========================================================================================

        }


        private void clearDousei()
        {
            for (int i = 0; i < (4 * 24); i++)
            {
                gcCalendarGrid1.ColumnHeader[0][2, i].Value = "";
            }
        }

        private void SetDousei(DateTime date, string label)
        {
            var hour = date.Hour;
            var minute = date.Minute;

            var index = hour * 4 + (minute < 14 ? 0 : minute < 29 ? 1 : minute < 44 ? 2 : 3);

            gcCalendarGrid1.ColumnHeader[0][2, index].Value = label;
            gcCalendarGrid1.ColumnHeader[0][2, index].CellStyle.Font = new Font(gcCalendarGrid1.Font.OriginalFontName, 6);
            gcCalendarGrid1.ColumnHeader[0][2, index].CellStyle.Alignment = CalendarGridContentAlignment.MiddleLeft;
        }






        private List<Appointment> makePatternList(MsVessel vessel, DateTime date, List<DouseiInfo> douseiInfos)
        {
            List<Appointment> retList = new List<Appointment>();

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {

                bool ToDayOnTheVoyage = false;
                bool ToDayWaiting = false;
                if (douseiInfos.Count == 0)
                {
                    // 航海中
                    ToDayOnTheVoyage = true;
                }
                else if (douseiInfos.Count == 1 && douseiInfos[0].WaitingInfo)
                {
                    // 待機
                    ToDayWaiting = true;
                }

                // 前日の動静情報
                bool BeforeOnTheVoyage = true;
                var BeforeDousei = serviceClient.DjDousei_GetRecords(NBaseCommon.Common.LoginUser, vessel.MsVesselID, date.AddDays(-1));
                if (BeforeDousei.Count == 1 && BeforeDousei[0].MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.待機ID)
                {
                    // 待機
                    BeforeOnTheVoyage = false;
                }

                // 翌日の動静情報
                bool NextDayOnTheVoyage = true;
                var NextDousei = serviceClient.DjDousei_GetRecords(NBaseCommon.Common.LoginUser, vessel.MsVesselID, date.AddDays(1));
                if (NextDousei.Count == 1 && NextDousei[0].MsKanidouseiInfoShubetuID == MsKanidouseiInfoShubetu.待機ID)
                {
                    // 待機
                    NextDayOnTheVoyage = false;
                }

                //====================================================================
                //
                // パターン作成
                //
                //====================================================================
                var waitingPatternList = (List<WorkPattern>)null;
                var voyagePatternList = (List<WorkPattern>)null;
                var patternList1 = (List<WorkPattern>)null;
                if (ToDayWaiting || BeforeOnTheVoyage == false || NextDayOnTheVoyage == false)
                {
                    // 待機中
                    waitingPatternList = serviceClient.WorkPattern_GetRecords(NBaseCommon.Common.LoginUser, WorkPattern.WorkPatternEventKind.Kind停泊中, vessel.MsVesselID, null);
                }
                else
                {
                    // 航海中
                    voyagePatternList = serviceClient.WorkPattern_GetRecords(NBaseCommon.Common.LoginUser, WorkPattern.WorkPatternEventKind.Kind航海中, vessel.MsVesselID, null);
                }



                // 当日のパターン
                if (ToDayWaiting)
                {
                    // 待機中
                    patternList1 = waitingPatternList;
                }
                else //if (ToDayOnTheVoyage)
                {
                    // 航海中
                    patternList1 = voyagePatternList;
                }
                if (patternList1 != null)
                {
                    foreach (WorkPattern w in patternList1)
                    {
                        Appointment apo = new Appointment();
                        apo.MsSiShokumeiID = w.MsSiShokuemiID;
                        apo.WorkContentID = w.WorkContentID;
                        apo.WorkDate = new DateTime(date.Year, date.Month, date.Day, w.WorkDate.Hour, w.WorkDate.Minute, 0);

                        retList.Add(apo);
                    }
                }


                if (douseiInfos.Count > 0)
                {
                    if (BeforeOnTheVoyage == false && douseiInfos.First().InTime != null)
                    {
                        // 入港までを待機パターンとする
                        var inTime = douseiInfos.First().InTime;

                        foreach (WorkPattern w in waitingPatternList)
                        {
                            var workDate = new DateTime(date.Year, date.Month, date.Day, w.WorkDate.Hour, w.WorkDate.Minute, 0);

                            if (workDate < inTime)
                            {
                                var apo = retList.Where(o => o.MsSiShokumeiID == w.MsSiShokuemiID && o.WorkDate == workDate).FirstOrDefault();
                                if (apo != null)
                                {
                                    apo.WorkContentID = w.WorkContentID;
                                }
                                else
                                {
                                    apo = new Appointment();
                                    apo.MsSiShokumeiID = w.MsSiShokuemiID;
                                    apo.WorkContentID = w.WorkContentID;
                                    apo.WorkDate = new DateTime(date.Year, date.Month, date.Day, w.WorkDate.Hour, w.WorkDate.Minute, 0);

                                    retList.Add(apo);
                                }
                            }
                        }
                    }

                    if (NextDayOnTheVoyage == false && douseiInfos.Last().OutTime != null)
                    {
                        // 出港以降を待機パターンとする
                        var outTime = douseiInfos.Last().OutTime;

                        foreach (WorkPattern w in waitingPatternList)
                        {
                            var workDate = new DateTime(date.Year, date.Month, date.Day, w.WorkDate.Hour, w.WorkDate.Minute, 0);

                            if (workDate > outTime)
                            {
                                var apo = retList.Where(o => o.MsSiShokumeiID == w.MsSiShokuemiID && o.WorkDate == workDate).FirstOrDefault();
                                if (apo != null)
                                {
                                    apo.WorkContentID = w.WorkContentID;
                                }
                                else
                                {
                                    apo = new Appointment();
                                    apo.MsSiShokumeiID = w.MsSiShokuemiID;
                                    apo.WorkContentID = w.WorkContentID;
                                    apo.WorkDate = new DateTime(date.Year, date.Month, date.Day, w.WorkDate.Hour, w.WorkDate.Minute, 0);

                                    retList.Add(apo);
                                }
                            }
                        }
                    }
                }


                foreach (DouseiInfo dinfo in douseiInfos)
                {
                    //====================================================================
                    // 入港と出港の間のパターンをクリアする
                    DateTime inTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                    DateTime outTime = inTime.AddDays(1);
                    if (dinfo.InTime != null)
                    {
                        var timeStr = ((DateTime)dinfo.InTime).ToShortTimeString();
                        int h = int.Parse(timeStr.Split(':')[0]);
                        int m = int.Parse(timeStr.Split(':')[1]);
                        if (m > 45)
                            m = 45;
                        else if (m > 30)
                            m = 30;
                        else if (m > 15)
                            m = 15;
                        else
                            m = 0;
                        inTime = new DateTime(date.Year, date.Month, date.Day, h, m, 0);
                    }
                    if (dinfo.OutTime != null)
                    {
                        var timeStr = ((DateTime)dinfo.OutTime).ToShortTimeString();
                        int h = int.Parse(timeStr.Split(':')[0]);
                        int m = int.Parse(timeStr.Split(':')[1]);
                        outTime = new DateTime(date.Year, date.Month, date.Day, h, m, 0);
                    }

                    for (DateTime d = inTime; d < outTime; d = d.AddMinutes(15))
                    {
                        var delAppts = retList.Where(o => o.WorkDate == d).ToList();
                        if (delAppts != null)
                        {
                            foreach (Appointment ap in delAppts)
                                retList.Remove(ap);
                        }
                    }

                    DateTime docking = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                    if (dinfo.Docking != null)
                    {
                        var timeStr = ((DateTime)dinfo.Docking).ToShortTimeString();
                        int h = int.Parse(timeStr.Split(':')[0]);
                        int m = int.Parse(timeStr.Split(':')[1]);
                        docking = new DateTime(date.Year, date.Month, date.Day, h, m, 0);
                    }

                    //====================================================================
                    // 入港のパターンを重ねる
                    if (dinfo.InTime != null)
                    {
                        var patternList = serviceClient.WorkPattern_GetRecords(NBaseCommon.Common.LoginUser, WorkPattern.WorkPatternEventKind.Kind入港, vessel.MsVesselID, dinfo.BashoId);

                        foreach (WorkPattern w in patternList)
                        {
                            Appointment apo = new Appointment();
                            apo.MsSiShokumeiID = w.MsSiShokuemiID;
                            apo.WorkContentID = w.WorkContentID;

                            if (w.WorkDateDiff > 0)
                                apo.WorkDate = inTime.AddMinutes(15 * (w.WorkDateDiff - 1));
                            else
                                apo.WorkDate = inTime.AddMinutes(15 * (w.WorkDateDiff));

                            if (retList.Any(o => o.MsSiShokumeiID == apo.MsSiShokumeiID && o.WorkDate == apo.WorkDate))
                            {
                                retList.Where(o => o.MsSiShokumeiID == apo.MsSiShokumeiID && o.WorkDate == apo.WorkDate).First().WorkContentID = w.WorkContentID;
                            }
                            else
                            {
                                retList.Add(apo);
                            }
                        }
                    }
                    //====================================================================
                    // 着桟のパターンを重ねる
                    if (dinfo.Docking != null)
                    {
                        var patternList = serviceClient.WorkPattern_GetRecords(NBaseCommon.Common.LoginUser, WorkPattern.WorkPatternEventKind.Kind着桟, vessel.MsVesselID, dinfo.BashoId);

                        foreach (WorkPattern w in patternList)
                        {
                            Appointment apo = new Appointment();
                            apo.MsSiShokumeiID = w.MsSiShokuemiID;
                            apo.WorkContentID = w.WorkContentID;

                            if (w.WorkDateDiff > 0)
                                apo.WorkDate = docking.AddMinutes(15 * (w.WorkDateDiff - 1));
                            else
                                apo.WorkDate = docking.AddMinutes(15 * (w.WorkDateDiff));

                            if (retList.Any(o => o.MsSiShokumeiID == apo.MsSiShokumeiID && o.WorkDate == apo.WorkDate))
                            {
                                retList.Where(o => o.MsSiShokumeiID == apo.MsSiShokumeiID && o.WorkDate == apo.WorkDate).First().WorkContentID = w.WorkContentID;
                            }
                            else
                            {
                                retList.Add(apo);
                            }
                        }
                    }
                    //====================================================================
                    // 出港のパターンを重ねる
                    if (dinfo.OutTime != null)
                    {
                        var patternList = serviceClient.WorkPattern_GetRecords(NBaseCommon.Common.LoginUser, WorkPattern.WorkPatternEventKind.Kind出港, vessel.MsVesselID, dinfo.BashoId);

                        foreach (WorkPattern w in patternList)
                        {
                            Appointment apo = new Appointment();
                            apo.MsSiShokumeiID = w.MsSiShokuemiID;
                            apo.WorkContentID = w.WorkContentID;

                            if (w.WorkDateDiff > 0)
                                apo.WorkDate = outTime.AddMinutes(15 * (w.WorkDateDiff - 1));
                            else
                                apo.WorkDate = outTime.AddMinutes(15 * (w.WorkDateDiff));

                            if (retList.Any(o => o.MsSiShokumeiID == apo.MsSiShokumeiID && o.WorkDate == apo.WorkDate))
                            {
                                retList.Where(o => o.MsSiShokumeiID == apo.MsSiShokumeiID && o.WorkDate == apo.WorkDate).First().WorkContentID = w.WorkContentID;
                            }
                            else
                            {
                                retList.Add(apo);
                            }
                        }
                    }

                }

            }

            return retList;
        }






        private void deviationProc(MsVessel vessel, DateTime date, List<Work> devationCheckWorkList)
        {
            foreach (MsSenin senin in SeninViewList)
            {
                System.Diagnostics.Debug.WriteLine($"  船員のDeviation:開始：{DateTime.Now.ToString("HH:mm:ss")}");

                string crewNo = senin.MsSeninID.ToString();
                var seninList = AppointmentList.Where(o => o.MsSeninID == senin.MsSeninID);

                var checkList = (List<Appointment>)null;
                if (DEBUG)
                {
                    checkList = seninList.ToList();
                }
                else
                {
                    checkList = seninList.Where(o => o.CheckTarget == true).ToList();
                }


                //// 日ごとの作業時間を算出（残業計算のため）
                //Dictionary<DateTime, int> workDic = new Dictionary<DateTime, int>();
                //var list = devationCheckWorkList.Where(o => o.CrewNo == crewNo).OrderBy(o => o.StartWork).ToList();

                //list.ForEach(o => {

                //            DateTime targetDate = DateTime.Parse(o.StartWork.ToShortDateString());
                //            int minutes = 0;

                //            if (o.StartWork.ToShortDateString() == o.FinishWork.ToShortDateString())
                //            {
                //                targetDate = DateTime.Parse(o.StartWork.ToShortDateString());
                //                minutes = (int)(o.FinishWork - o.StartWork).TotalMinutes;

                //                if (workDic.ContainsKey(targetDate) == false)
                //                {
                //                    workDic.Add(targetDate, 0);
                //                }
                //                workDic[targetDate] += minutes;
                //            }
                //            else
                //            {
                //                targetDate = DateTime.Parse(o.StartWork.ToShortDateString());
                //                minutes = (int)(targetDate.AddDays(1) - o.StartWork).TotalMinutes;

                //                if (workDic.ContainsKey(targetDate) == false)
                //                {
                //                    workDic.Add(targetDate, 0);
                //                }
                //                workDic[targetDate] += minutes;

                //                targetDate = DateTime.Parse(o.FinishWork.ToShortDateString());
                //                minutes = (int)(o.FinishWork - targetDate).TotalMinutes;

                //                if (workDic.ContainsKey(targetDate) == false)
                //                {
                //                    workDic.Add(targetDate, 0);
                //                }
                //                workDic[targetDate] += minutes;
                //            }
                //    });
                    


                foreach (Appointment ap in checkList)
                {
                    //
                    // あらゆる２４時間の労働時間：１４時間
                    //
                    //
                    //
                    var devCheckStart = ap.WorkDate.AddDays(-1);
                    var checkWorks = devationCheckWorkList.Where(o => o.CrewNo == crewNo && o.FinishWork >= devCheckStart && o.StartWork <= ap.WorkDate);
                    var tmpStart = devCheckStart;
                    checkWorks = checkWorks.OrderBy(o => o.StartWork).ToList();
                    var totalWork = 0;
                    foreach (Work wk in checkWorks)
                    {
                        if (wk.StartWork < devCheckStart)
                        {
                            totalWork += ((int)(wk.FinishWork - devCheckStart).TotalMinutes);
                            //if (senin.MsSeninID == 12 && ap.WorkDate.ToString("HH:mm") == "07:45") // 若山貴志
                            //{
                            //    System.Diagnostics.Debug.WriteLine($"Start:[{devCheckStart.ToString("yyyy/MM/dd HH:mm")}] ～ finish:[{wk.FinishWork.ToString("yyyy/MM/dd HH:mm")}]");
                            //}
                        }
                        else if (wk.FinishWork > ap.WorkDate)
                        {
                            totalWork += ((int)(ap.WorkDate - wk.StartWork).TotalMinutes);
                            //if (senin.MsSeninID == 12 && ap.WorkDate.ToString("HH:mm") == "07:45") // 若山貴志
                            //{
                            //    System.Diagnostics.Debug.WriteLine($"Start:[{wk.StartWork.ToString("yyyy/MM/dd HH:mm")}] ～ finish:[{ap.WorkDate.ToString("yyyy/MM/dd HH:mm")}]");
                            //}
                        }
                        else
                        {
                            totalWork += ((int)(wk.FinishWork - wk.StartWork).TotalMinutes);
                            //if (senin.MsSeninID == 12 && ap.WorkDate.ToString("HH:mm") == "07:45") // 若山貴志
                            //{
                            //    System.Diagnostics.Debug.WriteLine($"Start:[{wk.StartWork.ToString("yyyy/MM/dd HH:mm")}] ～ finish:[{wk.FinishWork.ToString("yyyy/MM/dd HH:mm")}]");
                            //}
                        }
                    }
                    //if (senin.MsSeninID == 12 && ap.WorkDate.ToString("HH:mm") == "07:45") // 若山貴志
                    //{
                    //    System.Diagnostics.Debug.WriteLine($"totalWork => :[{totalWork.ToString()}]");
                    //}
                    if (totalWork >= (60 * 14))　// １４時間
                    {
                        Appointment apo = new Appointment();
                        apo.MsSeninID = senin.MsSeninID;
                        apo.DeviationKind = 1; // あらゆる２４時間の労働時間
                        apo.WorkDate = ap.WorkDate;
                        System.Diagnostics.Debug.WriteLine($"  あらゆる２４時間の労働時間：{apo.MsSeninID}:{apo.WorkDate}");

                        AppointmentList.Add(apo);
                    }




                    //
                    // あらゆる１週間の労働時間：７２時間
                    //
                    //
                    //
                    var dev1CheckStart = ap.WorkDate.AddDays(-7);
                    checkWorks = devationCheckWorkList.Where(o => o.CrewNo == crewNo && o.FinishWork >= dev1CheckStart && o.StartWork <= ap.WorkDate);
                    tmpStart = dev1CheckStart;
                    checkWorks = checkWorks.OrderBy(o => o.StartWork).ToList();
                    totalWork = 0;
                    foreach (Work wk in checkWorks)
                    {
                        if (wk.StartWork < dev1CheckStart)
                        {
                            totalWork += ((int)(wk.FinishWork - dev1CheckStart).TotalMinutes);
                        }
                        else if (wk.FinishWork > ap.WorkDate)
                        {
                            totalWork += ((int)(ap.WorkDate - wk.StartWork).TotalMinutes);
                        }
                        else
                        {
                            totalWork += ((int)(wk.FinishWork - wk.StartWork).TotalMinutes);
                        }
                    }
                    if (totalWork >= (60 * 72))　// ７２時間
                    {
                        Appointment apo = new Appointment();
                        apo.MsSeninID = senin.MsSeninID;
                        apo.DeviationKind = 2; // あらゆる１週間の労働時間
                        apo.WorkDate = ap.WorkDate;

                        AppointmentList.Add(apo);
                    }





                    //
                    // あらゆる４週間の時間外労働時間間：５６時間
                    //
                    //
                    //
                    var dev4CheckStart = DateTimeUtils.ToFrom(ap.WorkDate).AddMonths(-1);
                    //var dic = workDic.Where(o => o.Key >= dev4CheckStart && o.Key < dev4CheckStart.AddMonths(1));
                    int overTimes = 0;
                    //foreach(var val in dic)
                    //{
                    //    if (val.Value > (60 * 8))　// １日の作業時間が８時間を超えているか
                    //    {
                    //        overTimes += (val.Value - (60 * 8));
                    //    }
                    //}
                    checkWorks = devationCheckWorkList.Where(o => o.CrewNo == crewNo && o.FinishWork >= dev4CheckStart && o.StartWork <= ap.WorkDate);
                    tmpStart = dev4CheckStart;
                    var dayStart = tmpStart;
                    var dayEnd = dayStart.AddDays(1);
                    checkWorks = checkWorks.OrderBy(o => o.StartWork).ToList();
                    totalWork = 0;
                    foreach (Work wk in checkWorks)
                    {
                        if (wk.StartWork > tmpStart)
                        {
                            tmpStart = wk.StartWork;
                        }
                        if (wk.FinishWork > dayEnd)
                        {
                            totalWork += ((int)(dayEnd - tmpStart).TotalMinutes);

                            if (totalWork > (60 * 8)) // １日の作業時間が８時間を超えているか
                            {
                                overTimes += (totalWork - (60 * 8));
                            }

                            dayStart = dayEnd;
                            dayEnd.AddDays(1);
                            if (dayEnd > ap.WorkDate)
                            {
                                dayEnd = ap.WorkDate;
                            }
                        }
                        else
                        {
                            totalWork += ((int)(wk.FinishWork - tmpStart).TotalMinutes);
                            tmpStart = wk.FinishWork;
                        }
                    }

                    if (overTimes > (60 * 56))　// ５６時間
                    {
                        Appointment apo = new Appointment();
                        apo.MsSeninID = senin.MsSeninID;
                        apo.DeviationKind = 3; // あらゆる４週間の時間外労働時間
                        apo.WorkDate = ap.WorkDate;

                        AppointmentList.Add(apo);
                    }






                    //
                    // ２４時間の休息時間
                    //
                    //
                    //
                    var restCheckStart = ap.WorkDate.AddDays(-1).AddMinutes(15);
                    checkWorks = devationCheckWorkList.Where(o => o.CrewNo == crewNo && o.FinishWork >= restCheckStart && o.StartWork < ap.WorkDate.AddMinutes(15));
                    var restMinutes = new List<int>();
                    tmpStart = restCheckStart;
                    checkWorks = checkWorks.OrderBy(o => o.StartWork).ToList();
                    foreach (Work wk in checkWorks)
                    {
                        if (wk.StartWork > tmpStart)
                        {
                            restMinutes.Add((int)(wk.StartWork - tmpStart).TotalMinutes);
                            tmpStart = wk.FinishWork;
                        }
                        else if (checkWorks.First() == wk)
                        {
                            tmpStart = wk.FinishWork;
                        }
                        else if (wk.StartWork == tmpStart)
                        {
                            tmpStart = wk.FinishWork;
                        }
                    }
                    if (restMinutes.Count > 0)
                    {
                        restMinutes = restMinutes.OrderByDescending(o => o).ToList();

                        int restTimes = restMinutes.First();
                        if (restMinutes.Count > 1)
                        {
                            restTimes += restMinutes[1];
                        }
                        if (restMinutes.Count > 2)
                        {
                            restTimes += restMinutes[2];
                        }

                        if (restTimes < (60 * 10)) // 60分×10時間
                        {
                            Appointment apo = new Appointment();
                            apo.MsSeninID = senin.MsSeninID;
                            apo.DeviationKind = 4; // ２４時間の休息時間
                            apo.WorkDate = ap.WorkDate;

                            AppointmentList.Add(apo);
                        }
                        else if (restMinutes.First() < (60 * 6)) // 60分×6時間
                        {
                            Appointment apo = new Appointment();
                            apo.MsSeninID = senin.MsSeninID;
                            apo.DeviationKind = 4; // ２４時間の休息時間
                            apo.WorkDate = ap.WorkDate;

                            AppointmentList.Add(apo);
                        }
                    }
                }


                System.Diagnostics.Debug.WriteLine($"  船員のDeviation:終了：{DateTime.Now.ToString("HH:mm:ss")}");
            }
        }


        private void gcCalendarGrid1_FirstDateInViewChanged(object sender, EventArgs e)
        {
        }




        /// <summary>
        /// 船員IDで該当行(Grid)のインデックスを求める
        /// </summary>
        /// <param name="seninid"></param>
        /// <returns></returns>
        #region public int GetIndexRowSenin(int seninid)
        public int GetIndexRowSenin(int seninid)
        {
            int index = -1;

            for (int i = 0; i < gcCalendarGrid1.RowHeader[0].RowCount; i++)
            {
                if (gcCalendarGrid1.RowHeader[0][i, 0].Value is MsSenin)
                {
                    MsSenin tsenin = gcCalendarGrid1.RowHeader[0][i, 0].Value as MsSenin;
                    if (tsenin.MsSeninID == seninid)
                    {
                        index = i;
                        break;
                    }
                }
            }
            return index;
        }



        #endregion





        private void button_パターン_Click(object sender, EventArgs e)
        {
            if (!(comboBox_Vessel.SelectedItem is MsVessel))
            {
                MessageBox.Show("船を選択してください");
                return;
            }


            労働パターンForm form = 労働パターンForm.GetInstance();
            form.Vessel = (comboBox_Vessel.SelectedItem as MsVessel);
            form.Show();
        }



        private void button_勤怠データ取得_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            //try
            //{
            //    String appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

            //    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            //    {
            //        serviceClient.WTM_GetWorks_Exec(NBaseCommon.Common.LoginUser, appName);
            //    }
            //}
            //finally
            //{
            //    MessageBox.Show("勤怠データを取得しました。");
            //    this.Cursor = Cursors.Default;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime testDate = DateTime.MinValue;
            if (DateTime.TryParse(textBox1.Text, out testDate))
            {
                TODAY = new DateTime(testDate.Year, testDate.Month, testDate.Day);
                NOW = testDate;

                SetNOW();
            }
        }
    }



    /// <summary>
    /// Renderと種別の関連付けクラス
    /// </summary>
    public class CalendarRender
    {
        public CalendarRoundedRectangleShapeRenderer RendarD;
        public CalendarRoundedRectangleShapeRenderer RendarL;
        public string WorkContentId;
    }

    public class Appointment
    {
        public int MsSiShokumeiID { get; set; }
        
        public bool Visibled { get; set; }　　// 表示するかしないか：予定の場合、表示しない
        public bool CheckTarget { get; set; }　　// Deviationチェックの対象か

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

        public override string ToString()
        {
            return "";
        }

        public Appointment()
        {
            Visibled = true;
            CheckTarget = false;
        }

    }

    class DouseiInfo
    {
        public bool WaitingInfo { set; get; }
        public string BashoId { set; get; }
        public string BashoName { set; get; }

        public DateTime? InTime { set; get; }
        public DateTime? Docking { set; get; }
        public DateTime? OutTime { set; get; }


        public DouseiInfo()
        {
            WaitingInfo = false;
            BashoId = null;
            BashoName = null;
            InTime = null;
            Docking = null;
            OutTime = null;
        }
    }

}

