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
using Senin.util;
using NBaseData.DS;
using NBaseUtil;
using System.IO;
using NBaseCommon.Senin;
using NBaseData.BLC;


namespace Senin
{
   
    public partial class 配乗計画Form : Form
    {
        private static 配乗計画Form instance;

        private static int PlanType = -1;

        //船のリスト
        private List<MsVessel> VesselList = null;

        //船員のリスト
        private List<MsSenin> SeninList = null;
        private List<MsSenin> SeninViewList = null;//コンボボックスで絞り込まれたリスト

        //計画のリスト
        private List<SiCardPlan> PlanList = new List<SiCardPlan>();
        private List<Appointment> PlanViewList = new List<Appointment>();

        //Revisionのリスト
        public Dictionary<DateTime, List<SiCardPlanHead>> HeadTbl = new Dictionary<DateTime, List<SiCardPlanHead>>();

        private SiCardPlanHead LastRevision = null;

        //実績のリスト
        //private List<SiCardPlan> ActList = new List<SiCardPlan>();
        private List<Appointment> ActList = new List<Appointment>();

        //Renderと種別を関連付ける
        private List<RenderLinkSyubetsu> RenderShubetsuList = new List<RenderLinkSyubetsu>();

        #region Render固定
        private CalendarRoundedRectangleShapeRenderer renderWhite;
        private CalendarRoundedRectangleShapeRenderer renderGray;
        #endregion

        #region 色
        private Color C_White = Color.FromArgb(250, 250, 250);

        private List<Color> ColorList = new List<Color>();
        private List<Color> ColorList_L = new List<Color>();

        #endregion

        //種別コンボボックスの中身
        private List<object> Combobox種別List = new List<object>();

        //年コンボボックスの期間 今年を中心に前後2年の5年間
        int Periodyear = 3;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public 配乗計画Form()
        {
            InitializeComponent();

        }

        /// <summary>
        /// インスタンス
        /// </summary>
        /// <returns></returns>
        public static 配乗計画Form Instance()
        {
            if (instance == null)
            {
                instance = new 配乗計画Form();
            }

            return instance;
        }

        /// <summary>
        /// 画面閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 配乗計画Form_FormClosed(object sender, FormClosedEventArgs e)
        private void 配乗計画Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }
        #endregion

        /// <summary>
        /// 画面起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 配乗計画Form_Load(object sender, EventArgs e)
        private void 配乗計画Form_Load(object sender, EventArgs e)
        {
            // この配乗表画面のタイプ
            PlanType = MsPlanType.PlanTypeOneMonth;
            this.Text = "配乗計画（" + MsPlanType.GetRecords()[MsPlanType.PlanTypeOneMonth].Name + "）";

            //カレンダーの最小日付最大日付セット 年月コンボボックスと合わせる
            gcCalendarGrid1.MinDate = new DateTime(DateTime.Today.Year - Periodyear + 1, 1, 1);
            gcCalendarGrid1.MaxDate = new DateTime(DateTime.Today.Year + Periodyear - 1, 12, 31);

            gcCalendarGrid1.FirstDateInView = NBaseUtil.DateTimeUtils.ToFromMonth(DateTime.Today);



            // 対象の船を抽出
            VesselList = new List<MsVessel>();
            SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser).ForEach(o => { if (o.IsPlanType(PlanType)) VesselList.Add(o); });


            //色定義
            MakeColorList();

            //バーの色形作る
            MakeRender();

            #region コンボボックス初期化
            InitCombobox種別();

            InitCombobox職種();

            InitComboBox年();//今の日付セット

            InitComboBox月();//今の日付セット

            #endregion


            //船員検索
            SearchSenin();

            //ヘッダ検索
            SearchRevision();

            //ヘッダセット
            SetRevision();

            //計画実績検索
            SearchPlan();

            //船員表示リストを作成
            GetSeninViewList();

            //船員表示リストをセット
            SetDataSenin();

            //Appointmentをセット
            SetDataPlan();

            //グリッドの動作
            gcCalendarGrid1.Protected = true;//編集不可
            gcCalendarGrid1.ResizeMode = CalendarResizeMode.None;//列幅等変更不可

            calendarTitleCaption1.DateFormatType = CalendarDateFormatType.DotNet;
            calendarTitleCaption2.DateFormatType = CalendarDateFormatType.DotNet;
        }
        #endregion

        /// <summary>
        /// 画面表示時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 配乗計画Form_Shown(object sender, EventArgs e)
        private void 配乗計画Form_Shown(object sender, EventArgs e)
        {
            InitGridView色見本();
        }
        #endregion

        //------------------------------------------------------
        //-----初期設定-----
        //
        /// <summary>
        /// 色定義
        /// </summary>
        #region private void MakeColorList()
        private void MakeColorList()
        {
            //ColorList.Add(Color.FromArgb(254, 70, 71));//レッド
            //ColorList.Add(Color.FromArgb(189, 133, 214));//紫
            //ColorList.Add(Color.FromArgb(0, 128, 255));//ブルー
            VesselList.ForEach(o => ColorList.Add(Color.FromArgb(o.R, o.G, o.B)));
            
            
            ColorList.Add( Color.FromArgb(0, 147, 112));//緑1 濃い
            ColorList.Add(Color.FromArgb(185, 221, 100));//緑2　薄い

            ColorList.Add(Color.FromArgb(255, 143, 34));//橙
            ColorList.Add(Color.FromArgb(137, 137, 137));//グレー

            //薄い色
            //ColorList_L.Add(Color.FromArgb(255, 234, 234));//レッド
            //ColorList_L.Add(Color.FromArgb(244, 236, 249));//紫
            //ColorList_L.Add(Color.FromArgb(232, 243, 255));//ブルー

            ColorList.ForEach(o => ColorList_L.Add(ColorExtension.GetLightColor(o)));


            ColorList_L.Add(Color.FromArgb(232, 255, 250));//緑1
            ColorList_L.Add(Color.FromArgb(243, 250, 228));//緑2

            ColorList_L.Add(Color.FromArgb(255, 241, 227));//橙
            ColorList_L.Add(Color.FromArgb(243, 243, 243));//グレー

        }
        #endregion

        /// <summary>
        /// バー作成
        /// </summary>
        #region private void MakeRender()
        private void MakeRender()
        {
            renderWhite = new CalendarRoundedRectangleShapeRenderer();
            renderWhite.RoundedRadius = 0.4f;
            renderWhite.FillColor = C_White;
            renderWhite.LineColor = Color.DarkGray;
            renderWhite.LineStyle = CalendarShapeLineStyle.Thin;
            renderWhite.LineWidth = 1;

            renderGray = new CalendarRoundedRectangleShapeRenderer();
            renderGray.RoundedRadius = 0.4f;
            renderGray.FillColor = Color.DarkGray;
            renderGray.LineColor = Color.DarkGray;
            renderGray.LineStyle = CalendarShapeLineStyle.Thin;
            renderGray.LineWidth = 1;


            int i = 0;
            foreach (Color c in ColorList)
            {
                RenderLinkSyubetsu ry = new RenderLinkSyubetsu();

                ry.RendarD = new CalendarRoundedRectangleShapeRenderer();
                ry.RendarD.RoundedRadius = 0.4f;
                ry.RendarD.FillColor = c;
                ry.RendarD.LineColor = c;
                ry.RendarD.LineStyle = CalendarShapeLineStyle.Thin;
                ry.RendarD.LineWidth = 1;

                ry.RendarL = new CalendarRoundedRectangleShapeRenderer();
                ry.RendarL.RoundedRadius = 0.4f;
                ry.RendarL.FillColor = ColorList_L[i];
                ry.RendarL.LineColor = c;
                ry.RendarL.LineStyle = CalendarShapeLineStyle.Thin;
                ry.RendarL.LineWidth = 1;

                ry.MsSiShubetuID = -1;
                ry.MsVesselID = 1;
                
                RenderShubetsuList.Add(ry);
                i++;
            }

            //周り点線
            //RenderYoteiList[3].RendarD.LineStyle = CalendarShapeLineStyle.Dashed;
            //RenderYoteiList[4].RendarD.LineStyle = CalendarShapeLineStyle.Dashed;
            //RenderYoteiList[3].RendarD.LineWidth = 2;
            //RenderYoteiList[4].RendarD.LineWidth = 2;
            //RenderYoteiList[3].RendarL.LineStyle = CalendarShapeLineStyle.Dashed;
            //RenderYoteiList[4].RendarL.LineStyle = CalendarShapeLineStyle.Dashed;
            //RenderYoteiList[3].RendarL.LineWidth = 2;
            //RenderYoteiList[4].RendarL.LineWidth = 2;
        }
        #endregion

        /// <summary>
        /// 年コンボボックス作成
        /// </summary>
        #region private void InitComboBox年()
        private void InitComboBox年()
        {       
            int thisYear = DateTime.Now.Year;

            for (int i = 1; i < Periodyear; i++)
            {
                comboBox年.Items.Add(thisYear - Periodyear + i);
            }
            for (int i = 0; i < Periodyear; i++)
            {
                comboBox年.Items.Add(thisYear + i);
            }
           
            comboBox年.SelectedItem = thisYear;
        }
        #endregion

        /// <summary>
        /// 月コンボボックス作成
        /// </summary>
        #region private void InitComboBox月()
        private void InitComboBox月()
        {
            int thisMonth = DateTime.Now.Month;

            for (int i = 0; i < 12; i++)
            {

                comboBox月.Items.Add(i+1);

            }

            comboBox月.SelectedItem = thisMonth;
        }
        #endregion

        /// <summary>
        /// 種別コンボボックス作成
        /// </summary>
        #region private void InitCombobox種別()
        private void InitCombobox種別()
        {
           
            List<MsVessel> vlist = SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser);

            int id2 = SeninTableCache.instance().MsSiShubetsu_有給休暇ID(NBaseCommon.Common.LoginUser);
            int id3 = SeninTableCache.instance().MsSiShubetsu_傷病ID(NBaseCommon.Common.LoginUser);

            MsSiShubetsu s2 = SeninTableCache.instance().GetMsSiShubetsu(NBaseCommon.Common.LoginUser, id2);
            MsSiShubetsu s3 = SeninTableCache.instance().GetMsSiShubetsu(NBaseCommon.Common.LoginUser, id3);

            //フェリーだけにわける
            comboBox_計画種別.Items.Add("");
            int i = 0;
            //foreach (MsVessel v in vlist)
            //{
            //    if (v.MsVesselTypeID == MsVesselType.MS_VESSEL_TYPE_フェリー_ID)
            //    {
            //        comboBox_計画種別.Items.Add(v);

            //        //Renderと種別の関係リスト
            //        RenderShubetsuList[i].MsVesselID = v.MsVesselID;
            //        RenderShubetsuList[i].MsSiShubetuID = SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser);
            //        i++;
            //    }
            //}
            foreach (MsVessel v in VesselList)
            {
                comboBox_計画種別.Items.Add(v);

                //Renderと種別の関係リスト
                RenderShubetsuList[i].MsVesselID = v.MsVesselID;
                RenderShubetsuList[i].MsSiShubetuID = SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser);
                i++;
            }

            comboBox_計画種別.Items.Add(s2);
            RenderShubetsuList[i].MsSiShubetuID = s2.MsSiShubetsuID;
            i++;

            comboBox_計画種別.Items.Add(s3);
            RenderShubetsuList[i].MsSiShubetuID = s3.MsSiShubetsuID;

            comboBox_計画種別.SelectedIndex = 0;

            foreach (object obj in comboBox_計画種別.Items)
            {
                if (!(obj is MsVessel) && !(obj is MsSiShubetsu)) continue;
                Combobox種別List.Add(obj);
            }
        }
        #endregion

        /// <summary>
        /// 職種コンボボックス作成
        /// </summary>
        #region private void InitCombobox職種()
        private void InitCombobox職種()
        {

            List<Shokumei> shokulist = SeninTableCache.instance().GetShokumeiList(NBaseCommon.Common.LoginUser, Shokumei.フェリー);
            comboBox_職種.Items.Add("");
            foreach (Shokumei s in shokulist)
            {
                if (s.MsSiShokumeiShousaiID > 0) continue;

                comboBox_職種.Items.Add(s);
            }
        }
        #endregion

        /// <summary>
        /// 色見本グリッド
        /// </summary>
        #region private void InitGridView色見本()
        private void InitGridView色見本()
        {
            DataTable dt色見本 = new DataTable();
            
            dt色見本.Columns.Add(new DataColumn("種別", typeof(string)));

            dataGridView_種別.DataSource = dt色見本;

            dataGridView_種別.Columns[0].Width = dataGridView_種別.Width-5;
            //dataGridView_種別.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView_種別.Columns[0].ReadOnly = true;


            foreach (object obj in comboBox_計画種別.Items)
            {

                DataRow row = dt色見本.NewRow();

                if (obj is MsVessel)
                {
                    MsVessel v = obj as MsVessel;
                    row[0] = v.VesselName;

                }
                else if (obj is MsSiShubetsu)
                {
                    MsSiShubetsu s = obj as MsSiShubetsu;
                    row[0] = s.Name;
                }
                else 
                {
                    continue;
                }
                dt色見本.Rows.Add(row);
            }

            for (int i = 0; i < dataGridView_種別.Rows.Count; i++)
            {

                //背景色を付ける
                dataGridView_種別.Rows[i].DefaultCellStyle.BackColor = ColorList[i];
                dataGridView_種別.Rows[i].DefaultCellStyle.SelectionBackColor = ColorList[i];

                dataGridView_種別.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                dataGridView_種別.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Black;
            }

            //周りの線
            dataGridView_種別.BorderStyle = BorderStyle.None;
            dataGridView_種別.ClearSelection();

            dataGridView_種別.Refresh();

        }
        #endregion

        //------------------------------------------------------
        //-----画面に値セット
        /// <summary>
        /// 船員データをグリッドにセット
        /// </summary>
        #region private void SetDataSenin()
        private void SetDataSenin()
        {
            //クリア
            int delrowcnt = gcCalendarGrid1.Template.RowCount;

            if (delrowcnt > 0)
            {
                gcCalendarGrid1.Template.RemoveRow(0, delrowcnt);
            }

            //人数分の行数用意
            if (SeninViewList.Count() > 0)
            {
                gcCalendarGrid1.Template.AddRow(SeninViewList.Count() * 2);
            
                //グリッドに職名、名前セット
                for (int i = 0; i < SeninViewList.Count; i++)
                {
                    int index = i * 2;

                    gcCalendarGrid1.RowHeader[0][index, 0].RowSpan = 2;
                    gcCalendarGrid1.RowHeader[0][index, 1].RowSpan = 2;
                    gcCalendarGrid1.RowHeader[0][index, 2].RowSpan = 2;

                    gcCalendarGrid1.RowHeader[0][index, 0].Value = SeninViewList[i];

                    string shokumei = SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, SeninViewList[i].MsSiShokumeiID);

                    gcCalendarGrid1.RowHeader[0][index, 1].Value = shokumei;
                    gcCalendarGrid1.RowHeader[0][index, 2].Value = SeninViewList[i].Sei + " " + SeninViewList[i].Mei;

                    gcCalendarGrid1.Template.Content.Rows[index].Height = 25;
                    gcCalendarGrid1.Template.Content.Rows[index+1].Height = 25;

                    //実績だけ背景色
                    gcCalendarGrid1.Template.Content.Rows[index + 1].CellStyle.BackColor = Color.FromArgb(247,247,247);

                }
            }

        }
        #endregion

        /// <summary>
        /// 計画、実績をグリッドにセット
        /// </summary>
        #region private void SetDataPlan()
        private void SetDataPlan()
        {
            gcCalendarGrid1.Content.ClearAll();

            #region 計画表示
            if (checkBox_計画.Checked == true)
            {

                //計画をセット
                foreach (Appointment plan in PlanViewList)
                {

                    //船員のIDから該当行のインデックスを求める
                    int index = GetIndexRowSenin(plan.MsSeninID);
                    if (index == -1) continue;//見つからないものはとばす

                    if (plan.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser))
                    {
                        //職名セット
                        Shokumei shoku = SeninTableCache.instance().GetShokumei(NBaseCommon.Common.LoginUser, Shokumei.フェリー, plan.MsSiShokumeiID, plan.MsSiShokumeiShousaiID);
                        plan.ShokuName = shoku.Name;
                        plan.ShokuNameAbbr = shoku.NameAbbr;
                        plan.ShokuNameEng = shoku.NameEng;
                    }
                    else
                    {
                        plan.ShokuNameEng = " ";
                    }

                    //何マスか取得
                    int span = GetSpan(plan.StartDate, plan.EndDate);
                    if (plan.PmStart == 1)
                    {
                        span = span - 1;
                    }
                    if (plan.PmEnd ==1)
                    {
                        span = span - 1;
                    }

                    //Appointmentを置く
                    PutAppointment(plan, index, plan.StartDate, plan.PmStart, span, false);
                }

            }
            #endregion

            #region 実績を表示
            if (checkBox_実績.Checked == true)
            {
                //実績をセット
                //foreach (SiCardPlan plan in ActList)
                foreach (Appointment ac in ActList)
                {
                   
                    //船員のIDから該当行のインデックスを求める
                    int index = GetIndexRowSenin(ac.MsSeninID);
                    if (index == -1) continue;//見つからないものはとばす

                    //実績は下の行
                    index++;

                    int span = GetSpan(ac.StartDate, ac.EndDate);

                    if (ac.PmStart == 1)
                    {
                        span = span - 1;
                    }
                    if(ac.PmEnd ==1)
                    {
                        span = span - 1;
                    }

                    //Appointmentを置く
                    PutAppointment(ac, index, ac.StartDate, ac.PmStart, span, false);
                }
            }
            #endregion
        }
        #endregion

        /// <summary>
        /// コンボボックスにリビジョンをセットする
        /// </summary>
        #region private void SetRevision()
        private void SetRevision()
        {
            //年月コンボで指定されたもの取得
            DateTime dt = GetDateTimeCombobox();

            //選択された月のヘッダをセット、最新を表示
            comboBox_RevNo.Items.Clear();
            foreach (SiCardPlanHead head in HeadTbl[dt])
            {
                comboBox_RevNo.Items.Add(head);
            }
            LastRevision = HeadTbl[dt].Last();

            comboBox_RevNo.SelectedItem = LastRevision;

            string msg = LastRevision.ShimeFlag == 1 ? "締" : "";

            label_HeadTotal.Text = dt.ToShortDateString() + ":" + HeadTbl[dt].Count.ToString() + " " + msg;

        }
        #endregion

        //------------------------------------------------------
        //-----画面コントロールイベント
        /// <summary>
        /// 計画・実績チェックボックス　チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void checkBox_計画実績_CheckedChanged(object sender, EventArgs e)
        private void checkBox_計画実績_CheckedChanged(object sender, EventArgs e)
        {
            //グリッドにAppointmentをセット
            SetDataPlan();
        }
        #endregion

        /// <summary>
        /// 種別コンボボックスの選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox_種別_TextChanged(object sender, EventArgs e)
        private void comboBox_種別_TextChanged(object sender, EventArgs e)
        {
            if (SeninList != null) 
            {
                if (comboBox_計画種別.SelectedItem is MsVessel || comboBox_計画種別.SelectedItem is MsSiShubetsu)
                {
                    checkBox選択種別のみ表示.Enabled = true;
                }
                else
                {
                    checkBox選択種別のみ表示.Checked = false;
                    checkBox選択種別のみ表示.Enabled = false;
                }

                //船員リスト絞り込み
                GetSeninViewList();

                //船員をグリッドにセット
                SetDataSenin();

                //Appointmentをセット
                SetDataPlan();
            }
        }
        #endregion

        /// <summary>
        /// 職種コンボボックスの選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox_職種_TextChanged(object sender, EventArgs e)
        private void comboBox_職種_TextChanged(object sender, EventArgs e)
        {
            if (SeninList != null)
            {
                if (comboBox_職種.SelectedItem is Shokumei)
                {
                    checkBox選択職種のみ表示.Enabled = true;
                }
                else
                {
                    checkBox選択職種のみ表示.Checked = false;
                    checkBox選択職種のみ表示.Enabled = false;
                }

                //船員リスト絞り込み
                GetSeninViewList();

                //船員をグリッドにセット
                SetDataSenin();

                //Appointmentをセット
                SetDataPlan();
            }
        }
        #endregion

        /// <summary>
        /// リビジョンコンボボックスの選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox_RevNo_TextChanged(object sender, EventArgs e)
        private void comboBox_RevNo_TextChanged(object sender, EventArgs e)
        {
            if (SeninList != null)
            {
                SiCardPlanHead head = comboBox_RevNo.SelectedItem as SiCardPlanHead;

                //古いリビジョンが選択された場合はその月だけを表示
                SearchPlan();

                //船員リスト絞り込み
                GetSeninViewList();

                //船員をグリッドにセット
                SetDataSenin();

                //Appointmentをセット
                SetDataPlan();
                
            }
        }
        #endregion


        private void checkBox種別_CheckedChanged(object sender, EventArgs e)
        {
            if (SeninList != null)
            {
                //船員リスト絞り込み
                GetSeninViewList();

                //船員をグリッドにセット
                SetDataSenin();

                //Appointmentをセット
                SetDataPlan();
            }
        }

        private void checkBox職種_CheckedChanged(object sender, EventArgs e)
        {
            if (SeninList != null)
            {
                //船員リスト絞り込み
                GetSeninViewList();

                //船員をグリッドにセット
                SetDataSenin();

                //Appointmentをセット
                SetDataPlan();
            }
        }

        //検索ボタンクリック時に2重でmoveDateが処理されないように
        int Button検索Flg = 0; 
        /// <summary>
        /// 検索ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button検索_Click(object sender, EventArgs e)
        private void button検索_Click(object sender, EventArgs e)
        {

            DateTime dt = GetDateTimeCombobox();

            #region コンボボックスと同期、日付移動

            //画面を更新しておく
            gcCalendarGrid1.PerformRender();

            //その日付までスクロール。ただし先頭日付まできちんとセットされない
            Button検索Flg = 1;//moveDate()処理したくない
            //gcCalendarGrid1.ScrollIntoView(dt.AddDays(1));//2022/01/14　確認したところ現象出なかったのでコメントに
            Button検索Flg = 0;

            //カレンダーの先頭日付セット。
            gcCalendarGrid1.FirstDateInView = dt;

            gcCalendarGrid1.Refresh();
            #endregion

            System.Diagnostics.Debug.WriteLine("検索button: comboBox = " + dt);
            System.Diagnostics.Debug.WriteLine("検索button: FirstDateInView = " + gcCalendarGrid1.FirstDateInView.ToShortDateString());
        }
        #endregion

        /// <summary>
        /// Revision Upボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonRevUp_Click(object sender, EventArgs e)
        private void buttonRevUp_Click(object sender, EventArgs e)
        {
            string msg = "";
            DateTime dt = GetDateTimeCombobox();
            if (HeadTbl[dt].Count == 1 && (HeadTbl[dt].First().SiCardPlanHeadID == null || HeadTbl[dt].First().SiCardPlanHeadID == ""))
            {
                msg = "Revisionがありません。";
            }
            else if (HeadTbl[dt].Last().ShimeFlag == 1)
            {
                msg = "既に確定されています。";
            }
            if (msg != "")
            {
                MessageBox.Show(msg, "配乗計画");
                return;
            }

            msg = dt.Year + "/" + dt.Month + "の計画を";

            if (MessageBox.Show(msg + "Revision Upしますか？", "配乗計画", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            bool ret = true;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //リビジョンをあげる
                //ret = serviceClient.BLC_配乗計画_RevisionUp(NBaseCommon.Common.LoginUser, dt, SiCardPlanHead.VESSEL_KIND_フェリー);
                ret = serviceClient.BLC_配乗計画_RevisionUp(NBaseCommon.Common.LoginUser, dt, PlanType);
            }

            if (ret == false)
            {
                MessageBox.Show("失敗しました。");
            }
            else
            {
                MessageBox.Show("Revision Upしました。");
            }

            //リビジョン再検索
            SearchRevision();

            //リビジョンセット
            SetRevision();

            //計画実績検索
            SearchPlan();

            //船員表示リスト作成
            GetSeninViewList();

            //船員をグリッドにセット
            SetDataSenin();

            //Appointmentをセット
            SetDataPlan();
        }
        #endregion

        /// <summary>
        /// 締めボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button月締_Click(object sender, EventArgs e)
        private void button月締_Click(object sender, EventArgs e)
        {
            string msg = "";
            DateTime dt = GetDateTimeCombobox();

            if (HeadTbl[dt].Last().ShimeFlag == 1)
            {
                msg = "既に確定されています。";
            }
            if (msg != "")
            {
                MessageBox.Show(msg, "配乗計画");
                return;
            }

            msg = dt.Year + "/" + dt.Month + "の";

            if (MessageBox.Show(msg + "計画を確定しますか？", "配乗計画", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            bool ret = true;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //締める
                //ret = serviceClient.BLC_配乗計画_Shime(NBaseCommon.Common.LoginUser, dt, SiCardPlanHead.VESSEL_KIND_フェリー);
                ret = serviceClient.BLC_配乗計画_Shime(NBaseCommon.Common.LoginUser, dt, PlanType);
            }

            if (ret == false)
            {
                MessageBox.Show("失敗しました。");
            }
            else
            {
                MessageBox.Show("確定しました。");
            }

            //リビジョン検索
            SearchRevision();

            //リビジョンを画面にセット

            //計画実績検索
            SearchPlan();

            //船員表示リスト作成
            GetSeninViewList();

            //船員をグリッドにセット
            SetDataSenin();

            //Appointmentをセット
            SetDataPlan();

        }
        #endregion

        /// <summary>
        /// 出力ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button出力_Click(object sender, EventArgs e)
        private void button出力_Click(object sender, EventArgs e)
        {
            配乗計画出力Form frm = new 配乗計画出力Form(PlanType, Periodyear);
            frm.ShowDialog();

        }
        #endregion

        /// <summary>
        /// カレンダーのセルマウスクリック　Appointmentを置く白抜きの仮計画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void gcCalendarGrid1_CellMouseClick(object sender, CalendarCellMouseEventArgs e)
        private void gcCalendarGrid1_CellMouseClick(object sender, CalendarCellMouseEventArgs e)
        {
            //計画にチェックが無い場合はおけないようにする
            if (!checkBox_計画.Checked) return;
           
            //セル位置
            CalendarCellPosition cp = e.CellPosition;
            DateTime dt = cp.Date;

            //クリックされた場所がコンテンツ以外は抜ける
            if (!(cp.Scope == CalendarTableScope.Content)) return;

            //Revisionが最新か
            if (comboBox_RevNo.SelectedItem is SiCardPlanHead)
            {
                SiCardPlanHead head = comboBox_RevNo.SelectedItem as SiCardPlanHead;

                //Revisionが最新でない　または　最新Revisionが締められているなら変更不可
                if ((head.SiCardPlanHeadID != LastRevision.SiCardPlanHeadID) || (LastRevision.ShimeFlag == 1))
                    return;
            }

           
            //既にアポイントがあれば抜ける
            if (gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value != null)
            {
                return;
            }

            //奇数行は実績なので抜ける
            if (cp.RowIndex % 2 != 0) 
            {
                return;
            }

            //行から船員を取得
            int index = cp.RowIndex/2;
            MsSenin senin = SeninViewList[index];

            //船員がフェリー籍かどうか
            //if (senin.Department != 船員.船員_所属_旅客)
            //{
            //    MessageBox.Show("所属がフェリーではありません。");
            //    return;
            //}

            //新規Appointment作成
            Appointment newy = new Appointment();
            newy.MsSeninID = senin.MsSeninID;
            newy.SeninName = senin.Sei + " " + senin.Mei;
            newy.MsSiShokumeiID = senin.MsSiShokumeiID;
            newy.ShokuName = SeninTableCache.instance().GetMsSiShokumeiName(NBaseCommon.Common.LoginUser, senin.MsSiShokumeiID);
            newy.ShokuNameAbbr = SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, senin.MsSiShokumeiID);
            newy.ShokuNameEng = SeninTableCache.instance().GetMsSiShokumeiNameEng(NBaseCommon.Common.LoginUser, senin.MsSiShokumeiID);

            //期間
            newy.StartDate = dt;
            newy.EndDate = dt;

            if (cp.ColumnIndex == 0)
            {
                newy.PmStart = 0;
                newy.PmEnd = 1;
            }
            else
            {
                newy.PmStart = 1;
                newy.PmEnd = 0;
            }

            newy.MsSiShubetsuID = -1;//ここではまだ決まらない

            gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value= newy;
            PlanViewList.Add(newy);

            //Appointmentを置く白抜き仮計画 半日
            PutAppointment(newy, cp.RowIndex, newy.StartDate, cp.ColumnIndex, 1, true);
        }
        #endregion

        //
        //Renderのドラッギングで使用する変数
        //
        int CellDraggingFlg = 0;//バーを伸ばしている時=1
        Appointment CellDraggingAppointment = null;//ドラッギングを始めた時のRenderと関連するAppointmentを保持

        /// <summary>
        /// Renderのドラッギング アポイントメント伸ばしている時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void gcCalendarGrid1_AppointmentCellDragging(object sender, AppointmentCellDraggingEventArgs e)
        private void gcCalendarGrid1_AppointmentCellDragging(object sender, AppointmentCellDraggingEventArgs e)
        {
            //奇数行は実績なので抜ける
            //セル位置
            CalendarCellPosition cp = e.StartCellPosition;
            if (cp.RowIndex % 2 != 0)
            {
                e.AllowDrop = false;
                return;
            }

            //Revisionが最新じゃないなら抜ける　締めでも抜ける
            if (comboBox_RevNo.SelectedItem is SiCardPlanHead)
            {
                SiCardPlanHead head = comboBox_RevNo.SelectedItem as SiCardPlanHead;

                //Revisionが最新でない　または　最新Revisionが締められているなら変更不可
                if ((head.SiCardPlanHeadID != LastRevision.SiCardPlanHeadID) || ( LastRevision.ShimeFlag == 1))
                {
                    e.AllowDrop = false;
                    return;
                }
            }

            //最初にRenderをつかんだ時に、Appointmentをキープしておく
            if( CellDraggingFlg == 0)
            {
                DateTime startdt = e.StartCellPosition.Date;
                int colindex = e.StartCellPosition.ColumnIndex;
                int rowindex = e.StartCellPosition.RowIndex;

                string t = gcCalendarGrid1.Content[startdt][rowindex, colindex].Value.GetType().ToString();
                //System.Diagnostics.Debug.WriteLine("gcCalendarGrid1_AppointmentCellDragging():" + t);

                if (gcCalendarGrid1.Content[startdt][rowindex, colindex].Value is Appointment)
                    CellDraggingAppointment = gcCalendarGrid1.Content[startdt][rowindex, colindex].Value as Appointment;
                else
                    CellDraggingAppointment = new Appointment();
            }

            CellDraggingFlg = 1;

        }
        #endregion

        /// <summary>
        /// カレンダーのセルマウスアップ  Appointmentを離した時に詳細開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void gcCalendarGrid1_CellMouseUp(object sender, CalendarCellMouseEventArgs e)
        private void gcCalendarGrid1_CellMouseUp(object sender, CalendarCellMouseEventArgs e)
        {
            //Renderをドラッギングしていた場合
            if (CellDraggingFlg == 1)
            {
                //System.Diagnostics.Debug.WriteLine("gcCalendarGrid1_CellMouseUp():" + CellDraggingAppointment.SiCardPlanID);
                //最初につかんだRenderのAppointmentを渡す
                詳細画面を開く(false, CellDraggingAppointment);
               
            }
            CellDraggingFlg = 0;
            CellDraggingAppointment = null;
        }
        #endregion

        /// <summary>
        /// カレンダーのセルダブルクリック　Appointmentがあれば詳細開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void gcCalendarGrid1_CellMouseDoubleClick(object sender, CalendarCellMouseEventArgs e)
        private void gcCalendarGrid1_CellMouseDoubleClick(object sender, CalendarCellMouseEventArgs e)
        {
            //セル位置
            CalendarCellPosition cp = e.CellPosition;
            DateTime dt = cp.Date;

            //アポインが無いならぬける
            if (gcCalendarGrid1.Content[dt][cp.RowIndex, cp.ColumnIndex].Value == null)
            {
                return;
            }

            bool jisseki = false;

            //実績なので変更不可
            if (cp.RowIndex % 2 != 0)
            {
                jisseki = true;
            }

            詳細画面を開く(jisseki, null);
            
        }
        #endregion


        /// <summary>
        /// カレンダーの先頭日付が変わる時発生する　[<][>][<<][>>][今日へ移動]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcCalendarGrid1_FirstDateInViewChanged(object sender, EventArgs e)
        {
            moveDate();
        }
        //------------------------------------------------------
        //-----処理
        /// <summary>
        /// Revision検索 
        /// </summary
        #region private void SearchRevision()
        private void SearchRevision()
        {
            //カレンダーの範囲
            DateTime date1 = gcCalendarGrid1.MinDate;
            date1 = NBaseUtil.DateTimeUtils.ToFromMonth(date1);
            DateTime date2 = gcCalendarGrid1.MaxDate;
            date2 = NBaseUtil.DateTimeUtils.ToFromMonth(date2);

            HeadTbl = new Dictionary<DateTime, List<SiCardPlanHead>>();

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //HeadTbl = serviceClient.BLC_配乗計画_SearchRevisions(NBaseCommon.Common.LoginUser, date1, date2, SiCardPlanHead.VESSEL_KIND_フェリー);
                HeadTbl = serviceClient.BLC_配乗計画_SearchRevisions(NBaseCommon.Common.LoginUser, date1, date2, PlanType);
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

        /// <summary>
        /// 計画、実績を検索
        /// </summary>
        #region private void SearchPlan()
        private void SearchPlan()
        {
            //年月　今表示されている日の先頭
            DateTime dt = gcCalendarGrid1.FirstDateInView;
            dt = NBaseUtil.DateTimeUtils.ToFromMonth(dt);

            //リビジョン取得
            SiCardPlanHead head = new SiCardPlanHead();
            if (comboBox_RevNo.SelectedItem is SiCardPlanHead)
            {
                head = comboBox_RevNo.SelectedItem as SiCardPlanHead;
            }

            //検索日時期間
            DateTime start = gcCalendarGrid1.FirstDateInView;
            DateTime end = gcCalendarGrid1.LastDateInView.AddDays(1);

            PlanList = new List<SiCardPlan>();
            PlanViewList = new List<Appointment>();

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region 計画検索
                if (head.SiCardPlanHeadID == LastRevision.SiCardPlanHeadID)
                {
                    //PlanList = serviceClient.BLC_配乗計画_SearchPlan(NBaseCommon.Common.LoginUser, start, end, SiCardPlanHead.VESSEL_KIND_フェリー);
                    PlanList = serviceClient.BLC_配乗計画_SearchPlan(NBaseCommon.Common.LoginUser, start, end, PlanType);
                }
                else
                {
                    //そのヘッダだけ取得
                    //PlanList = serviceClient.BLC_配乗計画_SearchPlanByHeder(NBaseCommon.Common.LoginUser, head, SiCardPlanHead.VESSEL_KIND_フェリー);
                    PlanList = serviceClient.BLC_配乗計画_SearchPlanByHeder(NBaseCommon.Common.LoginUser, head, PlanType);
                }

                //表示リストにいれる
                foreach (SiCardPlan plan in PlanList)
                {
                    Appointment ap = new Appointment(plan);

                    PlanViewList.Add(ap);
                }

                #endregion

                #region 実績検索 
                //ActList初期化
                ActList = new List<Appointment>();

                SiCardFilter filter = new SiCardFilter();
                filter.Start = start;
                filter.End = end;

                #region フェリー乗船の実績
                List<SiCard> actlist_v = null;
                filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser));
                List<MsVessel> vlist = SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser);
                foreach (MsVessel v in vlist)
                {
                    //if (v.MsVesselTypeID == MsVesselType.MS_VESSEL_TYPE_フェリー_ID)
                    //{
                    //    filter.MsVesselIDs.Add(v.MsVesselID);
                    //}
                    if (v.IsPlanType(PlanType))
                    {
                        filter.MsVesselIDs.Add(v.MsVesselID);
                    }
                }
                actlist_v = serviceClient.BLC_船員カード検索(NBaseCommon.Common.LoginUser, filter);
                ActListに入れる(actlist_v);
                #endregion

                #region その他休暇の実績
                List<SiCard> actlist_etc = null;
                filter = new SiCardFilter();

                filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_有給休暇ID(NBaseCommon.Common.LoginUser));
                filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_傷病ID(NBaseCommon.Common.LoginUser));
                actlist_etc = serviceClient.BLC_船員カード検索(NBaseCommon.Common.LoginUser, filter);
                ActListに入れる(actlist_etc);

                //foreach (SiCard card in actlist_etc)
                //{
                //    //Appointment作成
                //    Appointment ap = new Appointment(card);

                //    if (card.StartDate == null || card.StartDate == DateTime.MinValue) continue;

                //    ap.ActFlg = 1;

                //    ap.StartDate = card.StartDate;

                //    //終了日がないなら今日にして継続のしるし
                //    if (card.EndDate == null || card.EndDate == DateTime.MinValue)
                //    {
                //        ap.EndDate = DateTime.Today.Date;
                //        ap.OnGoing = true;
                //    }
                //    else
                //    {
                //        ap.EndDate = NBaseCommon.配乗計画Func.乗船計画終了日_Class2Disp(card.EndDate);
                //    }

                //    //期間のstartとendが逆転するデータ
                //    if (ap.StartDate >= ap.EndDate) continue;

                //    ap.MsSeninID = card.MsSeninID;
                //    ap.SeninName = card.SeninName;


                //    if (card.SiLinkShokumeiCards.Count > 0)
                //    {
                //        foreach (SiLinkShokumeiCard link in card.SiLinkShokumeiCards)
                //        {
                //            ap.MsSiShokumeiID = link.MsSiShokumeiID;
                //            break;
                //        }
                //    }
                //    else
                //    {
                //        ap.MsSiShokumeiID = card.SeninMsSiShokumeiID;
                //    }
                //    ap.MsSiShubetsuID = card.MsSiShubetsuID;

                //    ap.ShokuName = "";
                //    ap.ShokuNameAbbr = "";
                //    ap.ShokuNameEng = "";

                //    ActList.Add(ap);
                //}
                #endregion

                #endregion
            }

        }

        /// <summary>
        /// 実績リストにいれる
        /// </summary>
        /// <param name="catdlist"></param>]
        #region private void ActListに入れる(List<SiCard> catdlist )
        private void ActListに入れる(List<SiCard> catdlist )
        {
            foreach (SiCard card in catdlist)
            {
                //Appointment作成
                Appointment ap = new Appointment(card);

                ap.ActFlg = 1;//実績

                if (card.StartDate == null || card.StartDate == DateTime.MinValue) continue;

                if (card.EndDate == null || card.EndDate == DateTime.MinValue)
                {
                    //終了日がないなら今日にして継続のしるし
                    ap.EndDate = DateTime.Today.Date;
                    ap.OnGoing = true;
                }
               
                //期間のstartとendが逆転するデータはとばす
                if (ap.StartDate > ap.EndDate) continue;

                if (card.EndDate.TimeOfDay == new TimeSpan(23, 59, 59))
                {
                    DateTime wkdate = card.EndDate.Date;

                    ap.EndDate = wkdate.AddDays(1);
                }

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

                if (card.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser))
                {
                    Shokumei shoku = SeninTableCache.instance().GetShokumei(NBaseCommon.Common.LoginUser, Shokumei.フェリー, ap.MsSiShokumeiID, ap.MsSiShokumeiShousaiID);
                    ap.ShokuName = shoku.Name;
                    ap.ShokuNameAbbr = shoku.NameAbbr;
                    ap.ShokuNameEng = shoku.NameEng;
                }
                else 
                {
                    ap.ShokuName = "";
                    ap.ShokuNameAbbr = "";
                    ap.ShokuNameEng = "";
                }
                ActList.Add(ap);
            }
        }
        #endregion

        #endregion


        /// <summary>
        /// 詳細画面を開く
        /// </summary>
        /// <param name="is実績">実績ならtrue</param>
        /// <param name="ap">ダブルクリックから呼ばれた時はnull、ドラッギング後から呼ばれた時はAppointment</param>
        #region private void 詳細画面を開く( bool is実績, Appointment ap)
        private void 詳細画面を開く( bool is実績, Appointment ap)
        {
            
            //選択されているAppointmentの情報
            int rowindex = this.gcCalendarGrid1.SelectedCells[0].RowIndex;
            int colindex = this.gcCalendarGrid1.SelectedCells[0].ColumnIndex;
            DateTime dateFrom = this.gcCalendarGrid1.SelectedCells[0].Date;//開始日
            int span = this.gcCalendarGrid1[dateFrom][rowindex, colindex].ColumnSpan;

            //ポジション保持
            CalendarCellPosition sv_cp =   this.gcCalendarGrid1.SelectedCells[0];

            Appointment targetP = null;
            bool enable = true;

            #region 対象となるAppointmentを得る
            if (ap == null) //ダブルクリックはここにくる。計画の場合、実績の場合がある
            {
                targetP = gcCalendarGrid1.Content[dateFrom][rowindex, colindex].Value as Appointment;
                if (is実績)
                {
                    enable = false;//実績なら詳細画面で変更不可
                }
                else
                {//計画で締められていた場合
                    if (targetP.HeadShimeFlag == 1)
                    {
                        enable = false;//Appointが締められてると詳細画面では変更不可
                    }
                }
            }
            else//ドラッグでRederを離したときはここにくる。計画のみ
            {
                //
                targetP = ap;
                if (ap.HeadShimeFlag == 1)
                    enable = false;
                else
                    enable = true;
            }
            #endregion

            //詳細画面に渡すAppointmentを作成
            Appointment newP = targetP.Clone();

            #region 開始日 終了日取得

            //開始日時求める
            DateTime startdatetime = dateFrom;
            if (colindex == 1)
            {
                startdatetime = startdatetime + NBaseCommon.配乗計画Func.半日時間;
            }
            //終了日時求める　伸ばしたカラム数から取得する 1カラム=半日
            TimeSpan tspan = new TimeSpan(0, 0, 0);
            for (int i = 0; i < span; i++)
            {
                tspan += NBaseCommon.配乗計画Func.半日時間;
            }
            DateTime enddatetime = startdatetime + tspan;

            //開始日と午後フラグ
            newP.StartDate = startdatetime.Date;
            newP.PmStart = colindex;

            //終了日と午後フラグ
            newP.EndDate = enddatetime.Date;
            if (enddatetime.TimeOfDay == NBaseCommon.配乗計画Func.半日時間)
            {
                newP.PmEnd = 1;
            }
            else
            {
                newP.EndDate = newP.EndDate.AddDays(-1);
                newP.PmEnd = 0;
            }
            
            #endregion

            //期間
            int oldSpan = gcCalendarGrid1.Content[dateFrom][rowindex, colindex].ColumnSpan;

            //詳細画面表示
            //配乗計画詳細Form frm = new 配乗計画詳細Form(newP, Combobox種別List, enable);
            //配乗計画詳細Form frm = new 配乗計画詳細Form(PlanType, newP, Combobox種別List, enable);
            配乗計画詳細Form frm = null;
            DialogResult dialogResult = frm.ShowDialog();

            //実績ならそのままぬける
            if (is実績) return;

            bool result = true;
            if (dialogResult == DialogResult.OK)
            {
                //詳細画面OKボタン
                result = 登録更新(targetP, newP);
            }
            else if (frm.DialogResult == DialogResult.Cancel)
            {
                //詳細画面キャンセルボタン
                ;
            }
            else if (frm.DialogResult == DialogResult.No)
            {
                //DB登録されていないものなら表示のみ削除、登録されてればデータも削除
                result = 削除(targetP, oldSpan, rowindex, colindex, newP);
                
            }

            //グリッドの表示位置
            gcCalendarGrid1.ScrollIntoView(sv_cp);
        }
        #endregion

        /// <summary>
        /// 登録更新
        /// </summary>
        /// <param name="targetplan">対象plan</param>
        /// <param name="oldSpan">詳細画面開く前のspan</param>
        /// <param name="rowindex">対象行</param>
        /// <param name="colindex">詳細画面開く前のcolindex 0か1</param>
        /// <param name="newPlan"></param>
        /// <returns></returns>
        #region private bool 登録更新(Appointment targetplan, Appointment newPlan)
        private bool 登録更新(Appointment targetplan, Appointment newPlan)
        {
            bool ret = true;

            #region データ登録

            //詳細画面で変更された値をいれる
            targetplan.StartDate = newPlan.StartDate;
            targetplan.EndDate = newPlan.EndDate;
            targetplan.PmStart = newPlan.PmStart;
            targetplan.PmEnd = newPlan.PmEnd;
            targetplan.MsSiShubetsuID = newPlan.MsSiShubetsuID;
            if (newPlan.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser))
            {
                targetplan.MsVesselID = newPlan.MsVesselID;
            }
            targetplan.MsSiShokumeiID = newPlan.MsSiShokumeiID;
            targetplan.MsSiShokumeiShousaiID = newPlan.MsSiShokumeiShousaiID;
            targetplan.ShokuName = newPlan.ShokuName;
            targetplan.ShokuNameAbbr = newPlan.ShokuNameAbbr;
            targetplan.ShokuNameEng = newPlan.ShokuNameEng;
            
            //データの登録更新
            SiCardPlan  plan = InsertUpdate(targetplan);
            if (plan == null) return false;
            #endregion

            //新規にリビジョン作成されたなら、リビジョン検索、コンボボックスにセット
            if (LastRevision.SiCardPlanHeadID != plan.SiCardPlanHeadID)
            {
                SearchRevision();
                SetRevision();
            }

            #region 計画再検索、再表示
            //計画実績検索
            SearchPlan();

            //船員表示リストを作成
            GetSeninViewList();

            //グリッドに船員表示リストをセット
            SetDataSenin();

            //グリッドにAppointmentをセット
            SetDataPlan();
            #endregion


            return ret;
        }
        #endregion

        /// <summary>
        /// DB登録更新
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        #region private SiCardPlan InsertUpdate(Appointment ap)
        private SiCardPlan InsertUpdate(Appointment ap)
        {
            SiCardPlan plan = null;

            //登録月を取得
            DateTime dt = NBaseUtil.DateTimeUtils.ToFromMonth(ap.StartDate);

            //登録するSiCardPlan
            SiCardPlan targetP = null;

            //Appontmentクラス→SiCardPlanクラスに変換
            if (ap.SiCardPlanID == null || ap.SiCardPlanID =="" )
            {
                targetP = ap.MakeSiCardPlan(); 
            }
            else
            {
                targetP = PlanList.Where(obj => obj.SiCardPlanID == ap.SiCardPlanID).First();
                targetP.MsSiShokumeiID = ap.MsSiShokumeiID;
                targetP.MsSiShokumeiShousaiID = ap.MsSiShokumeiShousaiID;
                targetP.MsSiShubetsuID = ap.MsSiShubetsuID;
                targetP.MsVesselID = ap.MsVesselID;
                targetP.StartDate = ap.StartDate;
                targetP.EndDate = ap.EndDate;
                targetP.LaborOnBoarding = ap.PmStart==1 ? (int)SiCardPlan.LABOR.半休:0;
                targetP.LaborOnDisembarking = ap.PmEnd==1 ? (int)SiCardPlan.LABOR.半休:0;
            }

            //DBに登録
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //登録更新したデータが戻る
                //plan = serviceClient.BLC_配乗計画_InsertOrUpdate(NBaseCommon.Common.LoginUser, targetP, dt, SiCardPlanHead.VESSEL_KIND_フェリー);
                plan = serviceClient.BLC_配乗計画_InsertOrUpdate(NBaseCommon.Common.LoginUser, targetP, dt, PlanType);
            }

            return plan;
        }
        #endregion

        /// <summary>
        /// 計画削除
        /// </summary>
        /// <param name="plan"></param>
        /// <param name="oldSpan"></param>
        /// <param name="rowindex"></param>
        /// <param name="colindex"></param>
        /// <param name="isMsg">メッセージ表示がいるかどうか</param>
        /// <returns></returns>
        #region private bool 削除(Appointment plan, int oldSpan, int rowindex, int colindex, Appointment delP)
        private bool 削除(Appointment plan, int oldSpan, int rowindex, int colindex, Appointment delP)
        {
            bool ret = true;

            SiCardPlan targetP = null;

            //IDがついているものはデータ削除
            if (!plan.IsNew())
            {
                if (MessageBox.Show("削除します。よろしいですか？", "配乗計画", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return ret;

                targetP = PlanList.Where(obj => obj.SiCardPlanID == plan.SiCardPlanID).First();


                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    //登録更新（削除フラグ）したデータが戻る
                    ret = serviceClient.SiCardPlan_DeleteRecord(NBaseCommon.Common.LoginUser, targetP);
                }
                if (ret == false) return false;

                MessageBox.Show("削除しました", "配乗計画"); 
            }

            #region 再検索、再表示 これだと白抜きがすべて消えてしまう
            //計画実績検索
            SearchPlan();

            //船員表示リストを作成
            GetSeninViewList();

            //グリッドに船員表示リストをセット
            SetDataSenin();

            //グリッドにAppointmentをセット
            SetDataPlan();
            #endregion

            #region 表示を消す　対象のAppointmentを消す
            ////詳細画面を表示する前に置いてあったAppointmentの日付
            //DateTime dt = delP.StartDate;

            ////Appointmentを消す
            //DeleteAppointment(rowindex, colindex, dt, oldSpan);

            ////リストから削除
            //PlanList.Remove(targetP);

            ////表示リストから削除
            //PlanViewList.Remove(plan);
            #endregion

            return ret;
        
        }
        #endregion

        /// <summary>
        /// 種別のコンボボックスで該当するIndexを求める（種別ID,VEsselIDで
        /// </summary>
        /// <param name="shubeID"></param>
        /// <param name="vslID"></param>
        /// <returns></returns>
        #region int GetIndexCombobox種別(int shubeID, int vslID)
        private int GetIndexCombobox種別(int shubeID, int vslID)
        {
            int ret = 0;

            //乗船中
            if (shubeID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser))
            {
                //乗船の時はvesselを探す
                for (int i = 0; i < comboBox_計画種別.Items.Count; i++)
                {
                    if (comboBox_計画種別.Items[i] is MsVessel)
                    {
                        MsVessel vessel = comboBox_計画種別.Items[i] as MsVessel;
                        //該当MsVesselID
                        if (vessel.MsVesselID == vslID)
                        {
                            ret = i;
                            break;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            {
                //乗船の以外なら種別を探す
                for (int i = 0; i < comboBox_計画種別.Items.Count; i++)
                {
                    if (comboBox_計画種別.Items[i] is MsSiShubetsu)
                    {
                        MsSiShubetsu shube = comboBox_計画種別.Items[i] as MsSiShubetsu;
                        
                        //該当種別
                        if (shube.MsSiShubetsuID == shubeID)
                        {
                            ret = i;
                            break;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

            }

            return ret;
        }
        #endregion

        /// <summary>
        /// 年月コンボボックスから年月+ついたち 0:00の　DateTimeを取得
        /// </summary>
        /// <returns></returns>
        #region private DateTime GetDateTimeCombobox()
        private DateTime GetDateTimeCombobox()
        {
            //年月コンボボックスから取得
            int year = 0;
            int month = 0;

            if (comboBox年.SelectedItem is int)
            {
                year = (int)comboBox年.SelectedItem;
            }
            if (comboBox月.SelectedItem is int)
            {
                month = (int)comboBox月.SelectedItem;
            }

            //年月作成
            DateTime dt = new DateTime(year, month, 1);

            return dt;
        }
        #endregion

        
        /// <summary>
        /// 職名、種別で船員リストにフィルターかける
        /// </summary>
        #region  private void GetSeninViewList()
        private void GetSeninViewList()
        {
            SeninViewList = new List<MsSenin>();//表示船員リストクリア

            List<int> seninIDs0 = new List<int>();

            List<int> seninIDs1 = new List<int>();
            
            #region 職種
            if (checkBox選択職種のみ表示.Checked)
            {
                if (comboBox_職種.SelectedItem is Shokumei)
                {
                    Shokumei shoku = comboBox_職種.SelectedItem as Shokumei;
                    if (SeninList.Any(o => o.MsSiShokumeiID == shoku.MsSiShokumeiID))
                    {
                        seninIDs0.AddRange(SeninList.Where(o => o.MsSiShokumeiID == shoku.MsSiShokumeiID).Select(o => o.MsSeninID));
                    }
                }
            }
            else
            {
                seninIDs0.AddRange(SeninList.Select(o => o.MsSeninID));
            }

            //// 船員の所属が「旅客」の船員を対象とする
            //// ただし、計画や実績がある場合には、他所属でも対象とする
            //foreach (int seninId in seninIDs0)
            //{
            //    if ((senin.Department == 船員.船員_所属_旅客) ||
            //        PlanList.Any(o => o.MsSeninID == seninId && o.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser)) ||
            //        ActList.Any(o => o.MsSeninID == seninId && o.MsSiShubetsuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser)))
            //    {
            //        if (!seninIDs1.Contains(seninId))
            //        {
            //            seninIDs1.Add(seninId);
            //        }
            //    }
            //}

            // 所属がないので基本的に全船員が対象
            seninIDs1.AddRange(seninIDs0);

            #endregion

            List<int> seninIDs2 = new List<int>();
            #region 種別
            if (checkBox選択種別のみ表示.Checked)
            {
                //コンボボックスで何が選択されているか。表示は何か
                object cmboboselobj = comboBox_計画種別.SelectedItem;

                if (cmboboselobj is MsVessel)
                {
                    MsVessel vessel = cmboboselobj as MsVessel;

                    foreach (int seninId in seninIDs1)
                    {
                        if (SeninList.Any(o => o.MsSeninID == seninId) &&
                            (PlanList.Any(o => o.MsSeninID == seninId && o.MsVesselID == vessel.MsVesselID) || ActList.Any(o => o.MsSeninID == seninId && o.MsVesselID == vessel.MsVesselID)))
                        {
                            if (!seninIDs2.Contains(seninId))
                            {
                                //seninIDs2.AddRange(SeninList.Where(o => o.MsSeninID == seninId).Select(o => o.MsSeninID));
                                seninIDs2.Add(seninId);
                            }
                        }

                    }
                }
                else if (cmboboselobj is MsSiShubetsu)
                {
                    MsSiShubetsu shube = cmboboselobj as MsSiShubetsu;

                    foreach (int seninId in seninIDs1)
                    {
                        if (SeninList.Any(o => o.MsSeninID == seninId) &&
                            (PlanList.Any(o => o.MsSeninID == seninId && o.MsSiShubetsuID == shube.MsSiShubetsuID) || ActList.Any(o => o.MsSeninID == seninId && o.MsSiShubetsuID == shube.MsSiShubetsuID)))
                        {
                            if (!seninIDs2.Contains(seninId))
                            {
                                //seninIDs2.AddRange(SeninList.Where(o => o.MsSeninID == seninId).Select(o => o.MsSeninID));
                                seninIDs2.Add(seninId);
                            }
                        }

                    }
                }
            }
            else
            {
                seninIDs2.AddRange(seninIDs1);
            }
            #endregion

            foreach (int id in seninIDs2)
            {
                SeninViewList.AddRange(SeninList.Where(o => o.MsSeninID==id).ToList());
            }
            label7.Text = "船員数=" + SeninViewList.Count();

        }
        #endregion

        ///// <summary>
        ///// 計画の表示リスト
        ///// </summary>
        //#region  private void GetPlanViewList()
        //private void GetPlanViewList()
        //{
        //    DateTime dt = gcCalendarGrid1.FirstDateInView;
        //    dt = NBaseUtil.DateTimeUtils.ToFromMonth(dt);

        //    //表示リストにいれる
        //    PlanViewList = new List<Appointment>();
        //    foreach (SiCardPlan p in PlanList)
        //    {
        //        Appointment ap = new Appointment(p);

        //        ap.DispEndDate = NBaseCommon.配乗計画Func.乗船計画終了日_Class2Disp(ap.EndDate);
        //        PlanViewList.Add(ap);
        //    }

        //}
        //#endregion

       
        

        /// <summary>
        /// あらかじめ作成してあるRenderを種別、職種で取得
        /// </summary>
        /// <param name="shubetuID"></param>
        /// /// <param name="vesselID"></param>
        /// <returns></returns>
        #region public CalendarRoundedRectangleShapeRenderer GetRender(int shubetuID, int vesselID, int shokumeiID)
        public CalendarRoundedRectangleShapeRenderer GetRender(int shubetuID, int vesselID, int shokumeiID)
        {
            CalendarRoundedRectangleShapeRenderer ret = new CalendarRoundedRectangleShapeRenderer();

            //コンボボックスで何が選択されているか。表示は何か
            object cmboboselobj = comboBox_計画種別.SelectedItem;

            if( cmboboselobj is MsVessel )//種別が船を選択している
            {
                MsVessel vsl = cmboboselobj as MsVessel;

                //Renderと種別関係リストから該当Renderを探す
                foreach (RenderLinkSyubetsu ry in RenderShubetsuList)
                {
                    //種別IDが乗船でvesselIDが同じもの
                    if (shubetuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser) && ry.MsSiShubetuID == shubetuID && ry.MsVesselID == vesselID)
                    {
                        //種別コンボで描画しようとしている船と同じものを選択している
                        if (vsl.MsVesselID == vesselID)
                        {
                            ret = ry.RendarD;//濃い色
                        }
                        else
                        {
                            ret = ry.RendarL;//薄い色
                        }
                        break;
                    }
                    else if(!(shubetuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser)) && ry.MsSiShubetuID == shubetuID)
                    {
                        ret = ry.RendarL;//薄い色
                        break;
                    }
                }
            }
            else if (cmboboselobj is MsSiShubetsu)//その他の種別
            {
                MsSiShubetsu shube = cmboboselobj as MsSiShubetsu;

                //Renderと種別関係リストから該当Renderを探す
                foreach (RenderLinkSyubetsu ry in RenderShubetsuList)
                {
                    //種別IDが乗船でvesselIDが同じもの
                    if (shubetuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser) && ry.MsSiShubetuID == shubetuID && ry.MsVesselID == vesselID)
                    {  
                        ret = ry.RendarL;//薄い色
                        break;
                    }
                    else if (!(shubetuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser)) && ry.MsSiShubetuID == shubetuID)
                    {
                        //種別コンボで描画しようとしている同じものを選択している
                        if (shube.MsSiShubetsuID == shubetuID)
                        {
                            ret = ry.RendarD;//濃い色
                        }
                        else
                        {
                            ret = ry.RendarL;//薄い色
                        }
                        break;
                    }
                }
            }
            else//全部の種別を表示する
            {
                //コンボボックスで何も選択されていないなら全部濃い色で描く
                foreach (RenderLinkSyubetsu ry in RenderShubetsuList)
                {
                    if (shubetuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser) && ry.MsSiShubetuID == shubetuID && ry.MsVesselID == vesselID)
                    {
                        ret = ry.RendarD;
                        break;
                    }
                    if (shubetuID != SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser) && ry.MsSiShubetuID == shubetuID)
                    {
                        ret = ry.RendarD;
                        break;
                    }
                }
            }
            if (ret == null)
            {
                ret = renderGray;
            }

            //選択職名でなければうすくする
            if (comboBox_職種.SelectedItem is Shokumei)
            {
                Shokumei shoku = comboBox_職種.SelectedItem as Shokumei;
                if (shoku.MsSiShokumeiID != shokumeiID)
                {
                    foreach (RenderLinkSyubetsu ry in RenderShubetsuList)
                    {
                        if (ret == ry.RendarD)
                        {
                            ret = ry.RendarL;
                            break;
                        }
                    }
                }
            }

            return ret;
        }

        public CalendarRoundedRectangleShapeRenderer GetRenderAct(int shubetuID, int vesselID)
        {
            CalendarRoundedRectangleShapeRenderer ret = new CalendarRoundedRectangleShapeRenderer();


            foreach (RenderLinkSyubetsu ry in RenderShubetsuList)
            {
                if (shubetuID == SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser) && ry.MsSiShubetuID == shubetuID && ry.MsVesselID == vesselID)
                {
                    ret = ry.RendarD;
                    break;
                }
                if (shubetuID != SeninTableCache.instance().MsSiShubetsu_乗船ID(NBaseCommon.Common.LoginUser) && ry.MsSiShubetuID == shubetuID)
                {
                    ret = ry.RendarD;
                    break;
                }
            }
            if (ret == null)
            {
                ret = renderGray;
            }
            return ret;
        }


        #endregion

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
        /// Appointmentを置く
        /// </summary>
        /// <param name="ap"></param>
        /// <param name="rowindex"></param>
        /// <param name="startdate"></param>
        /// <param name="colindex"></param>
        /// <param name="colspan"></param>
        /// <param name="仮">まだ登録されていないものはtrue</param>
        #region public void PutAppointment( Appointment ap, int rowindex, DateTime startdate,  int colindex, int colspan, bool 仮)
        public void PutAppointment( Appointment ap, int rowindex, DateTime startdate, int colindex, int colspan, bool 仮)
        {
            //System.Diagnostics.Debug.WriteLine("----putAppointment()-----");

            CalendarAppointmentCellType cactype1 = new CalendarAppointmentCellType();
            //cactype1.Orientation = Orientation.Horizontal;

            //セルタイプクローン、必須。おやくそく
            gcCalendarGrid1.Content[startdate][rowindex, colindex].CellType = cactype1.Clone();

            //太字
            Font baseFont = gcCalendarGrid1.Font;
            Font fnt = new Font(baseFont.FontFamily, baseFont.Size, baseFont.Style | FontStyle.Bold);
            gcCalendarGrid1.Content[startdate][rowindex, colindex].CellStyle.Font = fnt;


            DateTime dt = startdate;
            int pm = colindex;

            //計画とAppointmentの紐づけ
            this.gcCalendarGrid1.Content[dt][rowindex, pm].Value = ap;

            #region 
            //先頭だけ紐づければ大丈夫
            //for (int i = 0; i < colspan; i++)
            //{
            //    //すべてのカラムに定を紐づけ
            //    if( i == 0)
            //        this.gcCalendarGrid1.Content[dt][rowindex, pm].Value = ap;

            //    //次のカラムインデックスを求める
            //    if (pm == 0)
            //    {
            //        pm = 1;
            //    }
            //    else
            //    {
            //        dt = dt.AddDays(1);//午後→午前の時は日付変わる
            //        pm = 0;
            //    }
            //}

            if (colspan > 0)
            {
                this.gcCalendarGrid1[startdate][rowindex, colindex].ColumnSpan = colspan;
            }
            #endregion

            gcCalendarGrid1.Content[startdate][rowindex, colindex].CellStyle.ForeColor = Color.Black;
            gcCalendarGrid1.Content[startdate][rowindex, colindex].ToolTipText = ap.SeninName;

            #region デバッグ表示
            //DateTime wdt=startdate;
            //int wcol = colindex;
            //for (int i = 0; i < colspan; i++)
            //{
            //    string t = gcCalendarGrid1.Content[wdt][rowindex, wcol].Value.GetType().ToString();
            //    System.Diagnostics.Debug.WriteLine("  " + wdt.ToString() + "," + wcol.ToString() + ","+ t);

            //    if (wcol == 0)
            //    {
            //        wcol = 1;
            //    }
            //    else
            //    {
            //        wcol = 0;
            //        wdt = wdt.AddDays(1);
            //    }

            //}
            #endregion

            if (仮)//Appointment仮置き 登録されていない白ぬきの
            {
                (this.gcCalendarGrid1[startdate][rowindex, colindex].CellType as CalendarAppointmentCellType).Renderer = renderWhite;
            }
            else
            {
                //if (ap.ActFlg == 0)
                //{
                    CalendarRoundedRectangleShapeRenderer rend = GetRender(ap.MsSiShubetsuID, ap.MsVesselID,ap.MsSiShokumeiID);
                    (this.gcCalendarGrid1[startdate][rowindex, colindex].CellType as CalendarAppointmentCellType).Renderer = rend;
                    if (rend.LineColor != rend.FillColor)
                    {
                        //薄い色バーの時は文字も薄くする
                        gcCalendarGrid1.Content[startdate][rowindex, colindex].CellStyle.ForeColor = Color.Gray;
                    }
                //}
                //else
                //{
                //    CalendarRoundedRectangleShapeRenderer rend = GetRenderAct(ap.MsSiShubetsuID, ap.MsVesselID);
                //    (this.gcCalendarGrid1[startdate][rowindex, colindex].CellType as CalendarAppointmentCellType).Renderer = rend;
                //}
               
            }
        }
        #endregion

        /// <summary>
        /// Appointmentを消す
        /// </summary>
        /// <param name="rowindex"></param>
        /// <param name="colindex"></param>
        /// <param name="startdate"></param>
        #region private void DeleteAppointment(int rowindex, int colindex, DateTime startdate, int span)
        private void DeleteAppointment(int rowindex, int colindex, DateTime startdate, int span)
        {

            //セル初期化
            //gcCalendarGrid1.Content[dt][rowindex, colindex].Value = null;
            int pm = colindex;
            DateTime dt = startdate;
            for (int i = 0; i < span; i++)
            {
                gcCalendarGrid1.Content[dt][rowindex, pm].Value = null;
                
                //セルの選択解除
                gcCalendarGrid1.RemoveSelection(dt, rowindex, pm);

                if (pm == 0)
                {
                    pm = 1;
                }
                else
                {
                    pm = 0;
                    dt = dt.AddDays(1);
                }
            }

            //セル結合解除
            gcCalendarGrid1.Content[startdate][rowindex, colindex].ColumnSpan = 1;
        }

        #endregion

        /// <summary>
        /// カレンダーの日付が変更した際の処理
        /// </summary>]
        #region private void moveDate()
        private void moveDate()
        {
            if (Button検索Flg == 1)
            {
                System.Diagnostics.Debug.WriteLine("moveDate() flg=1");
                return;
            }
            DateTime dt = gcCalendarGrid1.FirstDateInView;
            dt = NBaseUtil.DateTimeUtils.ToFromMonth(dt);

            if (HeadTbl.ContainsKey(dt))
            {
                System.Diagnostics.Debug.WriteLine("moveDate() flg=0: date="+dt.ToShortDateString());

                comboBox年.SelectedItem = dt.Year;
                comboBox月.SelectedItem = dt.Month;

                //移動した年月のリビジョンをセット
                SetRevision();

                //カレンダーのセルポジション保持
                CalendarCellPosition sv_cp = this.gcCalendarGrid1.CurrentCellPosition;

                //再検索
                SearchPlan();

                //船員表示リスト作成
                GetSeninViewList();

                //船員をグリッドにセット
                SetDataSenin();

                //Appointmentをセット
                SetDataPlan();

                //ポジション戻す
                this.gcCalendarGrid1.CurrentCellPosition = sv_cp;
            }
        }
        #endregion

       
    }

    /// <summary>
    /// Renderと種別の関連付けクラス
    /// </summary>
    public class RenderLinkSyubetsu
    {
        public CalendarRoundedRectangleShapeRenderer RendarD;
        public CalendarRoundedRectangleShapeRenderer RendarL;
        public int MsSiShubetuID;
        public int MsVesselID;

    }

}
