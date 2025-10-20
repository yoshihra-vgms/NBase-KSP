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
using WtmData;
using WtmModelBase;

namespace Sim
{
    public partial class 労働パターン入出港Form : Form
    { 
        /// <summary>
        /// インスタンスを格納するフィールド変数
        /// </summary>
        private static 労働パターン入出港Form instance = null;

        /// <summary>
        /// インスタンスを取得するためのメソッド
        /// Windows フォームをシングルトン対応にするコード
        /// </summary>
        /// <returns></returns>
        public static 労働パターン入出港Form GetInstance()
        {
            if (instance == null)
            {
                instance = new 労働パターン入出港Form();
            }
            return instance;
        }
        private void 労働パターン入出港Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }


        //Renderと作業内容を関連付ける
        private CalendarRectangleShapeRenderer RenderWhite;
        private List<CalendarRender> RenderList = new List<CalendarRender>();

        private List<MsSiShokumei> ShokumeiList = new List<MsSiShokumei>();

        private List<労働パターンForm_Appointment> AppointmentList = new List<労働パターンForm_Appointment>();

        private int minSpan = 15; // １５分刻み
        private int NumperH = 4;// 1時間を4分割 
        private DateTime DefCalendarDateTime = new DateTime(2000, 1, 1); // カレンダの基準とする日時
        private DateTime BaseDateTime = new DateTime(2000, 1, 1, 3,0,0); // 基準とする日時  （６時間の枠で、－３～３としたいので、３時を基準とする）


        List<string> dispList = new List<string>() { "-3h", "-2h", "-1h", "1h", "2h", "3h"};

        private List<WorkContent> WorkContentList = null;
        private List<WorkPattern> WorkPatternList = null;


        List<MsBasho> BashoList = null;


        public MsVessel Vessel { set; get; }

        public int EventIndex { set; get; }

        private 労働パターン入出港詳細Form 詳細form = null;


        //
        //Renderのドラッギングで使用する変数
        //
        int CellDraggingFlg = 0;//バーを伸ばしている時=1
        労働パターンForm_Appointment CellDraggingAppointment = null;//ドラッギングを始めた時のRenderと関連するAppointmentを保持

        HandlePosition DrDragging;//ドラッキングの方向


        private 労働パターン入出港Form()
        {
            InitializeComponent();

            foreach (WorkPattern.WorkPatternEventKind ek in WorkPattern.WorkPatternEventKind.List())
                comboBox_Event.Items.Add(ek);
        }

        private void 労働パターン入出港Form_Load(object sender, EventArgs e)
        {
            gcCalendarGrid1.FirstDateInView = DefCalendarDateTime;

            SearchWorkContent();
            MakeRender();

            #region 土日でも黒色で表示
            gcCalendarGrid1.Styles.Clear();
            var holidayStyle = new CalendarCellStyle();
            holidayStyle.BackColor = Color.White;
            holidayStyle.ForeColor = Color.Black;
            var conditionalCelLStyle1 = new CalendarConditionalCellStyle("defaultDayStyle");
            conditionalCelLStyle1.Items.Add(new CalendarConditionalCellStyleItem(holidayStyle, ConditionalStyleOperator.IsHoliday));
            gcCalendarGrid1.Styles.Add(conditionalCelLStyle1);
            #endregion


            #region 表のヘッダを作成

            int numCol = 6;//6カラム作成
            int colwidth =31;

            gcCalendarGrid1.Template.AddColumn(NumperH * numCol);

            int mperH = 60 / NumperH;

            int indexH = 0;
            int indexM = 0;
            
            for (int i = 0; i < numCol; i++)
            {
                indexH = i * NumperH;
                gcCalendarGrid1.Template.ColumnHeader.Columns[indexH].Width = colwidth * NumperH;
                gcCalendarGrid1.Template.ColumnHeader[0, indexH].Value = dispList[i];
                gcCalendarGrid1.Template.ColumnHeader[0, indexH].ColumnSpan = NumperH;
                gcCalendarGrid1.Template.ColumnHeader[0, indexH].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;

                for (int j = 0; j < NumperH; j++)
                {
                    indexM = indexH + j;

                    int num = j * mperH;
                    gcCalendarGrid1.Template.ColumnHeader.Columns[indexM].Width = colwidth;
                    gcCalendarGrid1.Template.ColumnHeader[1, indexM].Value = num.ToString();
                    gcCalendarGrid1.Template.ColumnHeader[1, indexM].CellStyle.Alignment = CalendarGridContentAlignment.MiddleCenter;

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
            #endregion

            BashoList = new List<MsBasho>();
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                List<MsBasho> wklist = serviceClient.MsBasho_GetRecordsBy港(NBaseCommon.Common.LoginUser);
                if (wklist.Count > 0)
                {
                    BashoList = wklist.Where(obj => obj.GaichiFlag == 0).ToList();
                }
            }
            foreach (MsBasho basho in BashoList)
            {
                comboBox_Basho.Items.Add(basho);
            }



            var shokumeiList = SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser);

            //職名分行を追加
            gcCalendarGrid1.Template.AddRow(shokumeiList.Count);

            //職名セット
            int idx = 0;
            foreach(MsSiShokumei s in shokumeiList)
            {
                gcCalendarGrid1.RowHeader[0][idx, 0].Value = s;
                idx++;

                ShokumeiList.Add(s);
            }

            // イベントは、指定さ入れたイベント
            comboBox_Event.SelectedItem = WorkPattern.WorkPatternEventKind.List().Where(o => o.Kind == EventIndex).First();


            詳細form = new 労働パターン入出港詳細Form(WorkContentList,BaseDateTime);
        }

        private void 労働パターン入出港Form_Activated(object sender, EventArgs e)
        {
            // イベントは、指定さ入れたイベント
            comboBox_Event.SelectedItem = WorkPattern.WorkPatternEventKind.List().Where(o => o.Kind == EventIndex).First();
        }

        private void SearchWorkPattern()
        {
            int m = 60 / NumperH;

            AppointmentList.Clear();

            string bashoId = null;
            if (comboBox_Basho.SelectedItem is MsBasho)
            {
                bashoId = (comboBox_Basho.SelectedItem as MsBasho).MsBashoId;
            }

            // 労働パターン
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                var eKind = comboBox_Event.SelectedItem as WorkPattern.WorkPatternEventKind;
                WorkPatternList = serviceClient.WorkPattern_GetRecords(NBaseCommon.Common.LoginUser, eKind.Kind, Vessel.MsVesselID, bashoId);

                WorkPatternList.ForEach(o =>
                {
                    if (o.WorkDateDiff > 0)
                        o.WorkDate = BaseDateTime.AddMinutes(minSpan * (o.WorkDateDiff - 1));
                    else
                        o.WorkDate = BaseDateTime.AddMinutes(minSpan * o.WorkDateDiff);
                });
            }
            foreach (MsSiShokumei s in ShokumeiList)
            {
                foreach (WorkContent wc in WorkContentList)
                {
                    //職名、作業内容でわける
                    List<WorkPattern> wklist = WorkPatternList.Where(obj => obj.MsSiShokuemiID == s.MsSiShokumeiID && obj.WorkContentID == wc.WorkContentID).
                        OrderBy(obj => obj.WorkDate).ToList();

                    if (wklist.Count == 0) continue;

                    DateTime wd = DateTime.MinValue;
                    労働パターンForm_Appointment apo = null;

                    foreach (WorkPattern wp in wklist)
                    {
                        if (wp.WorkDate != wd)
                        {
                            if (apo != null)
                            {
                                AppointmentList.Add(apo);
                            }
                            apo = new 労働パターンForm_Appointment();
                            apo.MsSiShokumeiID = s.MsSiShokumeiID;
                            apo.WorkContentID = wc.WorkContentID;
                        }

                        労働パターンForm_DateID di = new 労働パターンForm_DateID(wp.WorkDate, wp.WorkPatternID, wp.GID);
                        apo.DateIDList.Add(di);

                        wd = wp.WorkDate.AddMinutes(m);
                    }
                    if (apo != null)
                    {
                        AppointmentList.Add(apo);
                    }
                }
            }
        }

        /// <summary>
        /// 作業内容検索
        /// </summary>
        #region private void SearchWorkContent()
        private void SearchWorkContent()
        {
            

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


            foreach (WorkContent wc in WorkContentList)
            {
                WorkContentPanel wcp = new WorkContentPanel(wc);
                flowLayoutPanel1.Controls.Add(wcp);
            }

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
                rend.RendarL.LineColor = ColorExtension.GetLightColor(b);
                rend.RendarL.LineStyle = CalendarShapeLineStyle.Thin;
                rend.RendarL.LineWidth = 1;

                rend.WorkContentId = wc.WorkContentID;

                RenderList.Add(rend);
            }
        }
        #endregion


        #region gcCalendarGrid1_CellMouseClick(object sender, CalendarCellMouseEventArgs e)
        private void gcCalendarGrid1_CellMouseClick(object sender, CalendarCellMouseEventArgs e)
        {
            //セル位置
            CalendarCellPosition cp = e.CellPosition;
            DateTime dt = cp.Date;

            //クリックされた場所がコンテンツ以外は抜ける
            if (!(cp.Scope == CalendarTableScope.Content)) return;
            
            //既にアポイントがあれば抜ける
            if (gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value is 労働パターンForm_Appointment)
            {
                return;
            }

            #region Appointment新規作成
            労働パターンForm_Appointment newap = new 労働パターンForm_Appointment();
            
            //職名
            if (!(gcCalendarGrid1.RowHeader[0][cp.RowIndex, 0].Value is MsSiShokumei)) return;
            MsSiShokumei shokumei = gcCalendarGrid1.RowHeader[0][cp.RowIndex, 0].Value as MsSiShokumei;
            
            //時間
            if (!(gcCalendarGrid1.ColumnHeader[0][1, cp.ColumnIndex].Value is string)) return;
            string t = gcCalendarGrid1.ColumnHeader[0][1, cp.ColumnIndex].Value as string;

            //時間とデフォIDを入れる
            労働パターンForm_DateID di = new 労働パターンForm_DateID(GetDateTime(cp.ColumnIndex, t), -1);
            newap.DateIDList.Add(di);
            newap.MsSiShokumeiID = shokumei.MsSiShokumeiID;
            
            #endregion

            //仮置き
            PutAppointment(newap, cp.RowIndex, true);
        }
        #endregion

        #region private void gcCalendarGrid1_CellMouseDoubleClick(object sender, CalendarCellMouseEventArgs e)
        private void gcCalendarGrid1_CellMouseDoubleClick(object sender, CalendarCellMouseEventArgs e)
        {
            //セル位置
            CalendarCellPosition cp = e.CellPosition;
            DateTime dt = cp.Date;

            //クリックされた場所がコンテンツ以外は抜ける
            if (!(cp.Scope == CalendarTableScope.Content)) return;

            //アポインが無いならぬける
            if (!(gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value is 労働パターンForm_Appointment))
            {
                return;
            }

            労働パターンForm_Appointment ap = gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value as 労働パターンForm_Appointment;

            詳細画面を開く(ap, "DoubleClick");
        }
        #endregion


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

                if (gcCalendarGrid1.Content[dt][rowindex, colindex].Value is 労働パターンForm_Appointment)
                    CellDraggingAppointment = gcCalendarGrid1.Content[dt][rowindex, colindex].Value as 労働パターンForm_Appointment;
                else
                    CellDraggingAppointment = new 労働パターンForm_Appointment();
            }

            if (e.DraggingHandle == HandlePosition.Left)
            {
                DrDragging = HandlePosition.Left;
            }
            else if (e.DraggingHandle == HandlePosition.Right)
            {
                DrDragging = HandlePosition.Right;
            }

            CellDraggingFlg = 1;

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
            //Renderをドラッギングしていた場合
            if (CellDraggingFlg == 1)
            {
                詳細画面を開く(CellDraggingAppointment, "MouseUp");
            }
            CellDraggingFlg = 0;
            CellDraggingAppointment = null;
        }
        #endregion

        private void gcCalendarGrid1_CellContentClick(object sender, CalendarCellEventArgs e)
        {
        }




        #region private void 詳細画面を開く(労働パターンForm_Appointment ap, string strmode)
        private void 詳細画面を開く(労働パターンForm_Appointment ap, string strmode)
        {
            //選択されているAppointmentSimの情報
            int rowindex = this.gcCalendarGrid1.SelectedCells[0].RowIndex;
            int colindex = this.gcCalendarGrid1.SelectedCells[0].ColumnIndex;
            int span = this.gcCalendarGrid1[DefCalendarDateTime][rowindex, colindex].ColumnSpan - 1;

            //ドラッギング終わりのmouseUpは期間を書き換える
            if (strmode == "MouseUp")
            {
                var gid = ap.DateIDList.Where(o => o.GID != null).Select(o => o.GID).FirstOrDefault();

                if (DrDragging == HandlePosition.Left)
                {
                    //始まり時間の変更
                    string obj1 = (string)gcCalendarGrid1.ColumnHeader[0][1, colindex].Value;
                    DateTime time1 = GetDateTime(colindex, obj1);

                    if (time1 < ap.DateIDList[0].WorkDate)
                    {
                        労働パターンForm_DateID di = new 労働パターンForm_DateID(time1, -1, gid);
                        ap.DateIDList.Add(di);
                    }
                    else
                    {
                        var list = new List<労働パターンForm_DateID>();
                        foreach (労働パターンForm_DateID p in ap.DateIDList)
                        {
                            if (p.WorkDate >= time1)
                                list.Add(p);
                        }
                        ap.DateIDList = list;
                    }
                }
                else if (DrDragging == HandlePosition.Right)
                {
                    //終わり時間の変更
                    string obj2 = (string)gcCalendarGrid1.ColumnHeader[0][1, colindex + span].Value;
                    DateTime time2 = GetDateTime(colindex + span, obj2);
                    int last = ap.DateIDList.Count - 1;
                    if (time2 > ap.DateIDList[last].WorkDate)
                    {
                        労働パターンForm_DateID di = new 労働パターンForm_DateID(time2, -1, gid);
                        ap.DateIDList.Add(di);
                    }
                    else
                    {
                        var list = new List<労働パターンForm_DateID>();
                        foreach (労働パターンForm_DateID p in ap.DateIDList)
                        {
                            if (p.WorkDate <= time2)
                                list.Add(p);
                        }
                        ap.DateIDList = list;
                    }
                }
                ap.DateIDList = ap.DateIDList.OrderBy(obj => obj.WorkDate).ToList();
            }

            //職名
            if (!(gcCalendarGrid1.RowHeader[0][rowindex, 0].Value is MsSiShokumei)) return;
            MsSiShokumei shokumei = gcCalendarGrid1.RowHeader[0][rowindex, 0].Value as MsSiShokumei;

            MsBasho basho = null;
            if (comboBox_Basho.SelectedItem is MsBasho)
                basho = (comboBox_Basho.SelectedItem as MsBasho);

            詳細form.SetAppointment(ap, shokumei, basho, Vessel, EventIndex);
            DialogResult dialogResult = 詳細form.ShowDialog();

            //再検索、表示
            DrawCalenderGrid();

            //セル選択解除
            if (gcCalendarGrid1.SelectedCells.Count > 0)
            {
                CalendarCellPosition p = gcCalendarGrid1.SelectedCells[0];
                gcCalendarGrid1.RemoveSelection(p.Date);
            }

        }
        #endregion

        /// <summary>
        /// Appointmentをカレンダーに表示
        /// </summary>
        /// <param name="ap"></param>
        /// <param name="仮"></param>
        #region public void PutAppointment(労働パターンForm_Appointment ap, int rowIndex, bool 仮)
        public void PutAppointment(労働パターンForm_Appointment ap, int rowIndex, bool 仮)
        {
            //時間からカラムを割り出す
            int lastindex = ap.DateIDList.Count - 1;
            int startColIndex = ap.DateIDList[0].WorkDate.Hour * NumperH + (ap.DateIDList[0].WorkDate.Minute / (60 / NumperH));
            int endColIndex = ap.DateIDList[lastindex].WorkDate.Hour * NumperH + (ap.DateIDList[lastindex].WorkDate.Minute / (60 / NumperH));
            int colspan = endColIndex - startColIndex + 1;

            if (colspan <= 0) return;


            //セルタイプクローン、必須。おやくそく
            CalendarAppointmentCellType cactype1 = new CalendarAppointmentCellType();
            gcCalendarGrid1.Content[DefCalendarDateTime][rowIndex, startColIndex].CellType = cactype1.Clone();

            if (!仮)
            {
                //太字
                Font baseFont = gcCalendarGrid1.Font;
                Font fnt = new Font(baseFont.FontFamily, baseFont.Size, baseFont.Style | FontStyle.Bold);
                gcCalendarGrid1.Content[DefCalendarDateTime][rowIndex, startColIndex].CellStyle.Font = fnt;
            }

            //計画とAppointmentの紐づけ
            this.gcCalendarGrid1.Content[DefCalendarDateTime][rowIndex, startColIndex].Value = ap;

            //期間
            this.gcCalendarGrid1[DefCalendarDateTime][rowIndex, startColIndex].ColumnSpan = colspan;

            gcCalendarGrid1.Content[DefCalendarDateTime][rowIndex, startColIndex].CellStyle.ForeColor = Color.Black;


            if (仮)//Appointment仮置き 登録されていない白ぬきの
            {
                (this.gcCalendarGrid1[DefCalendarDateTime][rowIndex, startColIndex].CellType as CalendarAppointmentCellType).Renderer = RenderWhite;
            }
            else
            {
                (this.gcCalendarGrid1[DefCalendarDateTime][rowIndex, startColIndex].CellType as CalendarAppointmentCellType).Renderer = RenderList.Where(o => o.WorkContentId == ap.WorkContentID).FirstOrDefault().RendarD;
            }
        }
        #endregion


        private DateTime GetDateTime(int startindex, string value )
        {
            int wky =BaseDateTime.Year;
            int wkM = BaseDateTime.Month;
            int wkd = 1;
            int wkh = startindex / NumperH;
            int wkm = int.Parse(value);

            DateTime ret = new DateTime(wky, wkM, wkd, wkh,wkm, 0);

            return ret;

        }

        private void SetData()
        {
            gcCalendarGrid1.Content.ClearAll();

            for (int i = 0; i < gcCalendarGrid1.RowHeader[0].RowCount; i++) 
            {
                if (!(gcCalendarGrid1.RowHeader[0][i, 0].Value is MsSiShokumei)) return;
                MsSiShokumei shokumei = gcCalendarGrid1.RowHeader[0][i, 0].Value as MsSiShokumei;

                List<労働パターンForm_Appointment> wklist = AppointmentList.Where(obj => obj.MsSiShokumeiID == shokumei.MsSiShokumeiID).ToList();

                foreach (労働パターンForm_Appointment ap in wklist)
                {
                    PutAppointment(ap, i, false);
                }
            }
        }


        private void DrawCalenderGrid()
        {
            SearchWorkPattern();
            SetData();
        }
        
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox_Event.SelectedItem is WorkPattern.WorkPatternEventKind)
            {
                var eKind = comboBox_Event.SelectedItem as WorkPattern.WorkPatternEventKind;

                if (eKind.Pattern24)
                {
                    this.Hide();

                    労働パターン入出港Form form = 労働パターン入出港Form.GetInstance();
                    form.EventIndex = eKind.Kind;
                    form.Vessel = Vessel;
                    form.Show();
                }
                else
                {
                    EventIndex = eKind.Kind;

                    DrawCalenderGrid();
                }
            }
        }

        private void comboBox_Basho_SelectedValueChanged(object sender, EventArgs e)
        {
            DrawCalenderGrid();
        }
    }

   
    
}

