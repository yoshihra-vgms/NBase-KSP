using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using WtmModelBase;
using WtmModels;
using NBaseData.BLC;
using NBaseData.DAC;
using WtmData;

namespace WTM
{
    public partial class 退勤Form : Form
    {
        private 出退勤登録Form PForm;

        private DateTime KinmuStart = DateTime.MinValue;

        private Dictionary<TextBox, List<Label>> InputText_Dic = new Dictionary<TextBox, List<Label>>();



        public 退勤Form(出退勤登録Form pf)
        {
            InitializeComponent();

            groupBox1.MouseClick += new MouseEventHandler(ClickEvent);

            PForm = pf;

            KinmuStart = DateTime.Now;
        }

        private void 退勤Form_Load(object sender, EventArgs e)
        {
            //フォームの制御
            if (PForm != null)
            {
                PForm.Visible = false;
            }
            this.TopMost = true;

            InputText_Dic.Add(textBox勤務終了日, new List<Label>() { label勤務終了日, label勤務終了日説明 });
            InputText_Dic.Add(textBox勤務終了時間, new List<Label>() { label勤務終了時間, label勤務終了時間説明 });


            // 終了メッセージ非表示
            panel_Message.Visible = false;


            //勤務開始時間
            KinmuStart = PForm.BeInWork.StartWork;
            label勤務開始時間.Text = "勤務開始時間"+"　" + KinmuStart.ToString("yyyy/MM/dd") + " " + KinmuStart.ToString("H:mm:ss");

            //勤務終了日、時間
            textBox勤務終了日.Text = DateTime.Now.ToString("yyMMdd");
            textBox勤務終了時間.Text = DateTime.Now.ToString("HHmm");

            //フォーカス
            this.ActiveControl = textBox勤務終了日;
            
            //MultiRowの設定
            gcMultiRow1.AllowUserToAddRows = false;
            gcMultiRow1.AllowUserToDeleteRows = false;
            gcMultiRow1.AllowUserToResize = false;
            gcMultiRow1.AllowUserToAutoFitColumns = false;

            //multiRowがフォーカス失ったときに選択されているセルの背景色を描画しない
            gcMultiRow1.HideSelection = true;

            //スクロール量　50pixcel
            gcMultiRow1.VerticalScrollMode = ScrollMode.Pixel;
            gcMultiRow1.VerticalScrollCount = 50;
            gcMultiRow1.MouseWheelCount = 50;

            //垂直スクロールバーのみ表示
            gcMultiRow1.ScrollBars = ScrollBars.Vertical;

            //Template変更しても値を保持
            gcMultiRow1.RestoreValue = true;
            gcMultiRow1.RestoreColumnHeaderFooterValue = true;

            Template作成();

            Time行作成();
        }

        private void 退勤Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (PForm != null)
            {
                PForm.Dispose();
            }
        }

        private void Template作成()
        {
            //セルの高さ
            int cellHeight = 0;

            //セルの行数
            int rowcnt = 1;


            if (WtmCommon.FlgNightTime)
            {

                Template1 TemplateWorkContent_夜間あり = new Template1();

                //幅
                TemplateWorkContent_夜間あり.Width = GetSectionWidth();

                TemplateWorkContent_夜間あり.Height = 80;

                int px = TemplateWorkContent_夜間あり.BaseCellX;
                int py = TemplateWorkContent_夜間あり.BaseCellY;

                {
                    var name = "休息";

                    CheckBoxCell chkcell = new CheckBoxCell();
                    CellStyle cellStyle = TemplateWorkContent_夜間あり.GetCellStyle_WorkContent(Color.Black, Color.White);

                    // checkBoxCell作成
                    chkcell.Location = new Point(px, py);

                    chkcell.Name = "checkBoxCellRest";
                    chkcell.Size = TemplateWorkContent_夜間あり.GetCellWidth(name);

                    chkcell.Style = cellStyle;

                    chkcell.Text = name;

                    px = px + chkcell.Width + 5;

                    TemplateWorkContent_夜間あり.Row.Cells.Add(chkcell);
                }


                int cnt = 0;
                foreach (WorkContent wkcont in WtmCommon.WorkContentList)
                {
                    CheckBoxCell chkcell = new CheckBoxCell();
                    CellStyle cellStyle = TemplateWorkContent_夜間あり.GetCellStyle_WorkContent(ColorTranslator.FromHtml(wkcont.FgColor), ColorTranslator.FromHtml(wkcont.BgColor));

                    // checkBoxCell作成
                    chkcell.Location = new Point(px, py);

                    chkcell.Name = "checkContentCell" + cnt.ToString();
                    chkcell.Size = TemplateWorkContent_夜間あり.GetCellWidth(wkcont.Name);

                    chkcell.Style = cellStyle;

                    chkcell.Text = wkcont.Name;

                    px = px + chkcell.Width + 5;

                    //Location.Xがパネルの領域を超えてしまった
                    if (px > TemplateWorkContent_夜間あり.Width)
                    {
                        px = TemplateWorkContent_夜間あり.BaseCellX;
                        py = py + chkcell.Height + 5;

                        //次の行にセット
                        chkcell.Location = new Point(px, py);

                        px = px + chkcell.Width + 5;
                    }

                    TemplateWorkContent_夜間あり.Row.Cells.Add(chkcell);

                    //セルの高さ保持
                    if (cellHeight == 0) cellHeight = chkcell.Height;

                    cnt++;
                }

                //ラベルが選択されたときの選択色を無しにする
                TemplateWorkContent_夜間あり.DefaultCellStyle.SelectionBackColor = Color.Transparent;
                TemplateWorkContent_夜間あり.DefaultCellStyle.SelectionForeColor = Color.Black;

                //テンプレートの高さセット
                TemplateWorkContent_夜間あり.Row.Height = GetSectionHeight(rowcnt, cellHeight);

                gcMultiRow1.Template = TemplateWorkContent_夜間あり;
            }
            else
            {
                Template2 TemplateWorkContent_夜間なし = new Template2();

                //幅
                TemplateWorkContent_夜間なし.Width = GetSectionWidth();

                int px = TemplateWorkContent_夜間なし.BaseCellX;
                int py = TemplateWorkContent_夜間なし.BaseCellY;


                {
                    var name = "休息";

                    CheckBoxCell chkcell = new CheckBoxCell();
                    CellStyle cellStyle = TemplateWorkContent_夜間なし.GetCellStyle_WorkContent(Color.Black, Color.White);

                    // checkBoxCell作成
                    chkcell.Location = new Point(px, py);

                    chkcell.Name = "checkBoxCellRest";
                    chkcell.Size = TemplateWorkContent_夜間なし.GetCellWidth(name);

                    chkcell.Style = cellStyle;

                    chkcell.Text = name;

                    px = px + chkcell.Width + 5;

                    TemplateWorkContent_夜間なし.Row.Cells.Add(chkcell);
                }


                int cnt = 0;
                foreach (WorkContent wkcont in WtmCommon.WorkContentList)
                {
                    CheckBoxCell chkcell = new CheckBoxCell();
                    CellStyle cellStyle = TemplateWorkContent_夜間なし.GetCellStyle_WorkContent(ColorTranslator.FromHtml(wkcont.FgColor), ColorTranslator.FromHtml(wkcont.BgColor));

                    // checkBoxCell作成
                    chkcell.Location = new Point(px, py);

                    chkcell.Name = "checkContentCell" + cnt.ToString();
                    chkcell.Size = TemplateWorkContent_夜間なし.GetCellWidth(wkcont.Name);

                    chkcell.Style = cellStyle;

                    chkcell.Text = wkcont.Name;

                    px = px + chkcell.Width + 5;

                    //Location.Xがパネルの領域を超えてしまった
                    if (px > TemplateWorkContent_夜間なし.Width)
                    {
                        px = TemplateWorkContent_夜間なし.BaseCellX;
                        py = py + chkcell.Height + 5;

                        //次の行にセット
                        chkcell.Location = new Point(px, py);

                        px = px + chkcell.Width + 5;
                    }

                    TemplateWorkContent_夜間なし.Row.Cells.Add(chkcell);

                    //セルの高さ保持
                    if (cellHeight == 0) cellHeight = chkcell.Height;

                    cnt++;
                }

                //ラベルが選択されたときの選択色を無しにする
                TemplateWorkContent_夜間なし.DefaultCellStyle.SelectionBackColor = Color.Transparent;
                TemplateWorkContent_夜間なし.DefaultCellStyle.SelectionForeColor = Color.Black;

                TemplateWorkContent_夜間なし.Row.Height = GetSectionHeight(rowcnt, cellHeight);

                gcMultiRow1.Template = TemplateWorkContent_夜間なし;
            }

        }

        private int GetSectionWidth()
        {
            return gcMultiRow1.Width - 20;
        }

        private int GetSectionHeight(int rowcnt, int cellheight)
        {
            if (rowcnt < 2)
            {
                return 2 * cellheight + 20;
            }

            return rowcnt * cellheight + (rowcnt - 1) * 5 + 20;
        }

        private void Time行作成()
        {
            gcMultiRow1.Rows.Clear();

            DateTime wkFinish = DateTime.MinValue;

            if (ValidateFields(out wkFinish))
            {
                // 表示時間のため、まるめ処理
                DateTime wkStart = KinmuStart;
                int min = wkStart.Minute;
                if (min == 0)
                {
                }
                else if (min < 15)
                {
                    wkStart = DateTime.Parse(wkStart.ToString("yyyy/MM/dd HH:") + "00");
                }
                else if (min < 30)
                {
                    wkStart = DateTime.Parse(wkStart.ToString("yyyy/MM/dd HH:") + "15");
                }
                else if (min < 45)
                {
                    wkStart = DateTime.Parse(wkStart.ToString("yyyy/MM/dd HH:") + "30");
                }
                else
                {
                    wkStart = DateTime.Parse(wkStart.ToString("yyyy/MM/dd HH:") + "45");

                }

                //行数求める
                int num = 0;
                DateTime wkdate = wkStart;
                while (wkdate < wkFinish)
                {
                    wkdate = wkdate.AddMinutes(WtmCommon.WorkRange);
                    num++;
                }
                gcMultiRow1.RowCount = num;

                wkdate = wkStart;
                for (int i = 0; i < num; i++)
                {
                    gcMultiRow1.Rows[i].Cells["labelCellDate"].Value = wkdate.ToString("yy/MM/dd");
                    gcMultiRow1.Rows[i].Cells["labelCellTime"].Value = wkdate.ToString("HH:mm");
                    wkdate = wkdate.AddMinutes(WtmCommon.WorkRange);

                    //偶数行に背景色をつける
                    if (i % 2 == 0)
                    {
                        gcMultiRow1.Rows[i].BackColor = Color.WhiteSmoke;
                    }

                    //MouseOverの色
                    gcMultiRow1.Rows[i].MouseOverBackColor = Color.Gainsboro;
                }
            }


        }


        private List<WorkContentDetail> GetWorkContentDetails()
        {
            int count = 0;

            List<WorkContentDetail> ret = new List<WorkContentDetail>();
            foreach (var row in gcMultiRow1.Rows)
            {
                WorkContentDetail wcd = new WorkContentDetail();
                if (WtmCommon.FlgNightTime)
                {
                    if (row["checkBoxCellNight"].Value != null && (bool)row["checkBoxCellNight"].Value)
                    {
                        wcd.NightTime = true;
                    }
                }
                wcd.WorkContentID = GetWorkContent(row);

                ret.Add(wcd);

                count++;
            }

            return ret;
        }



        private void SetWorkContentDetail(List<WorkContentDetail> details)
        {
            for (int i = 0; i < gcMultiRow1.RowCount; i++)
            {
                var wcd = details[i];

                if (WtmCommon.FlgNightTime)
                {
                    if (wcd.NightTime)
                    {
                        if (gcMultiRow1.Rows[i].Cells.Any(o => o.Name == "checkBoxCellNight"))
                        {
                            gcMultiRow1.Rows[i]["checkBoxCellNight"].Value = true;
                        }
                    }
                }

                int cnt = -1;
                foreach (WorkContent wkcont in WtmCommon.WorkContentList)
                {
                    cnt++;
                    if (wkcont.WorkContentID == wcd.WorkContentID)
                    {
                        var cellName = "checkContentCell" + cnt.ToString();
                        gcMultiRow1.Rows[i][cellName].Value = true;
                        break;
                    }
                }
            }
        }


        #region ボタンクリックイベント
        private void buttonFinishWork_Click(object sender, EventArgs e)
        {
            DateTime dateEnd = DateTime.MinValue;
            if (ValidateFields(out dateEnd) && ValidationWorkContentDetails())
            {

                PForm.BeInWork.FinishWork = DateTime.Parse(dateEnd.ToString("yyyy/MM/dd HH:mm"));
                PForm.BeInWork.FinishWorkAcutual = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));

                string wcdStr = null;

                string stime = null;
                string wcId = null;
                int count = 0;
                foreach(var row in gcMultiRow1.Rows)
                {
                    var id = GetWorkContent(row);

                    if (id == null)
                    {
                        if (stime != null)
                        {
                            wcdStr += stime + "," + wcId + "," + count.ToString() + ";";
                        }
                        stime = null;
                        wcId = null;
                        count = 0;
                    }
                    else
                    {
                        if (stime == null)
                        {
                            stime = "20" + (string)row.Cells["labelCellDate"].Value + " " + (string)row.Cells["labelCellTime"].Value + ":00";
                            wcId = id;
                            count++;
                        }
                        else if (wcId == id)
                        {
                            count++;
                        }
                        else
                        {
                            wcdStr += stime + "," + wcId + "," + count.ToString() + ";";

                            stime = "20" + (string)row.Cells["labelCellDate"].Value + " " + (string)row.Cells["labelCellTime"].Value + ":00";
                            wcId = id;
                            count = 1;
                        }
                    }
                }
                wcdStr += stime + "," + wcId + "," + count.ToString() + ";";
                PForm.BeInWork.WorkContentDetail = wcdStr;

                string ntStr = null;
                if (WtmCommon.FlgNightTime)
                {
                    stime = null;
                    DateTime preWd = DateTime.MinValue;
                    DateTime wd = DateTime.MinValue;
                    count = 0;
                    foreach (var row in gcMultiRow1.Rows)
                    {
                        if (row["checkBoxCellNight"].Value != null && (bool)row["checkBoxCellNight"].Value)
                        {
                            wd = DateTime.Parse("20" + (string)row.Cells["labelCellDate"].Value + " " + (string)row.Cells["labelCellTime"].Value + ":00");

                            if (preWd.AddMinutes(WtmCommon.WorkRange) == wd)
                            {
                                count++;

                                preWd = wd;
                            }
                            else
                            {
                                if (preWd != DateTime.MinValue)
                                {
                                    ntStr += stime + "," + count.ToString() + ";";
                                }

                                stime = wd.ToString("yyyy/MM/dd HH:mm:ss");
                                count = 1;

                                preWd = wd;
                            }
                        }

                    }
                    if (string.IsNullOrEmpty(stime) == false)
                    {
                        ntStr += stime + "," + count.ToString() + ";";
                    }
                }
                PForm.BeInWork.NightTime = ntStr;

                PForm.BeInWork.UpdateDate = DateTime.Now;

                var ret = WtmAccessor.Instance().FinshWork(PForm.BeInWork);


                if (ret)
                {
                    tableLayoutPanel1.Enabled = false;
                    panel_Message.Visible = true;
                }
            }
        }

        private string GetWorkContent(Row row)
        {
            string id = null;
            int cnt = 0;

            foreach (WorkContent wkcont in WtmCommon.WorkContentList)
            {
                var name = "checkContentCell" + cnt.ToString();
                if (row[name].Value != null && (bool)row[name].Value)
                {
                    id = wkcont.WorkContentID;
                    break;
                }
                cnt++;
            }
            return id;
        }




        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_確認_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region 終了日時間Textbox　イベント
        private void textBox_Enter(object sender, EventArgs e)
        {
            foreach (var tb in InputText_Dic.Keys)
            {
                if ((sender as TextBox) == tb)
                {
                    InputText_Dic[tb].ForEach(o => { o.ForeColor = Color.Blue; });
                }
                else
                {
                    InputText_Dic[tb][0].ForeColor = SystemColors.ControlText;
                    InputText_Dic[tb][1].ForeColor = Color.DimGray;
                }
            }
        }
        private void textBox_Leave(object sender, EventArgs e)
        {
            InputText_Dic[(sender as TextBox)][0].ForeColor = SystemColors.ControlText;
            InputText_Dic[(sender as TextBox)][1].ForeColor = Color.DimGray;

            Time行作成();
        }

        private void textBox勤務終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                //押されたキーが 0～9でない場合は、イベントをキャンセルする
                e.Handled = true;
            }
        }

        #endregion

        #region クリックイベント
        private void ClickEvent(object sender, EventArgs e)
        {
            buttonFinishWork.Focus();
        }
        #endregion

        #region MultiRowセルイベント
        private void gcMultiRow1_CellEditedFormattedValueChanged(object sender, GrapeCity.Win.MultiRow.CellEditedFormattedValueChangedEventArgs e)
        {
            Cell currentCell = gcMultiRow1.Rows[e.RowIndex].Cells[e.CellIndex];

            if (e.Scope == CellScope.Row)
            {
                if (currentCell is CheckBoxCell)
                {
                    CheckBoxCell ck = currentCell as CheckBoxCell;
                    CheckBoxValueChenge(ck, currentCell.RowIndex);
                }
            }
        }

        private void CheckBoxValueChenge(CheckBoxCell ck, int rowindex)
        {
            if (ck.Name == "checkBoxCellNight") return;

            if ((bool)ck.EditedFormattedValue)
            {
                自分以外のチェックを外す(ck, rowindex);
                自分行以下のチェックを変更(ck, rowindex, true);
            }
            else
            {
                自分行以下のチェックを変更(ck, rowindex, false);
            }
        }

        private void 自分以外のチェックを外す(CheckBoxCell ck, int rowindex)
        {
            for (int i = 0; i < gcMultiRow1.Rows[rowindex].Cells.Count; i++)
            {
                //ワークコンテンツ以外は対象外
                //if (!(gcMultiRow1.Rows[rowindex].Cells[i].Name.Contains("checkContentCell"))) continue;

                if (!(gcMultiRow1.Rows[rowindex].Cells[i] is CheckBoxCell)) continue;

                if (gcMultiRow1.Rows[rowindex].Cells[i].Name == "checkBoxCellNight") continue;

                //自分は対象外
                if (gcMultiRow1.Rows[rowindex].Cells[i].Name == ck.Name) continue;

                string name = gcMultiRow1.Rows[rowindex].Cells[i].Name;
                gcMultiRow1.Rows[rowindex][name].Value = false;

            }
        }

        private void 自分行以下のチェックを変更(CheckBoxCell ck, int rowindex, bool ischeck)
        {
            for (int i = 0; i < gcMultiRow1.RowCount; i++)
            {
                if (i > rowindex)
                {
                    gcMultiRow1.Rows[i][ck.Name].Value = ischeck;
                    自分以外のチェックを外す((CheckBoxCell)gcMultiRow1.Rows[i][ck.Name], i);
                }
            }
        }

        #endregion


        #region Validation
        private bool ValidateFields(out DateTime dt)
        {
            dt = DateTime.MinValue;

            if (textBox勤務終了日.Text.Length != 6)
            {
                MessageBox.Show("正しい年月日を入力してください。");
                return false;
            }
            if (textBox勤務終了時間.Text.Length != 4)
            {
                MessageBox.Show("正しい時間を入力してください。");
                return false;
            }

            string str = textBox勤務終了日.Text.Substring(0, 2) + "/" + textBox勤務終了日.Text.Substring(2, 2) + "/" + textBox勤務終了日.Text.Substring(4, 2);
            DateTime outDateTime = DateTime.MinValue;

            if (DateTime.TryParse(str, out outDateTime))
            {; }
            else
            {
                MessageBox.Show("正しい年月日を入力してください。");
                return false;
            }
            dt = outDateTime.Date;

            str = textBox勤務終了時間.Text.Substring(0, 2) + ":" + textBox勤務終了時間.Text.Substring(2, 2);
            outDateTime = DateTime.MinValue;
            if (DateTime.TryParse(str, out outDateTime))
            {; }
            else
            {
                MessageBox.Show("正しい時間を入力してください。");
                return false;
            }

            dt = dt.AddHours(outDateTime.Hour).AddMinutes(outDateTime.Minute);

            if (KinmuStart >= dt)
            {
                MessageBox.Show("終了時間は、開始時間より後を入力してください。");
                return false;
            }
            return true;
        }


        private bool ValidationWorkContentDetails()
        {
            bool ret = false;
            int restCnt = 0;

            foreach (var row in gcMultiRow1.Rows)
            {
                var name = "checkBoxCellRest";
                if (row[name].Value != null && (bool)row[name].Value == true)
                {
                    restCnt++; // 「休息」がチェックされている
                }
                else
                {
                    int cnt = 0;
                    foreach (WorkContent wkcont in WtmCommon.WorkContentList)
                    {
                        name = "checkContentCell" + cnt.ToString();

                        if (row[name].Value != null && (bool)row[name].Value == true)
                        {
                            ret = true; // いずれかの作業がチェックされている
                            break;
                        }
                        cnt++;
                    }
                }

                if (ret)
                    break;
            }

            if (ret == false)
            {
                if (restCnt == gcMultiRow1.Rows.Count)
                {
                    MessageBox.Show("すべて休息のため登録できません。");
                }
                else
                {
                    MessageBox.Show("作業内容をチェックしてください。");
                }
                return false;
            }

            return true;
        }
        #endregion



        private int FlgResizeBegin;

        private void 退勤Form_ResizeBegin(object sender, EventArgs e)
        {
            FlgResizeBegin = 1;
        }
        private void 退勤Form_ResizeEnd(object sender, EventArgs e)
        {
            RebuildTemplate();
            ChangePanelMessageLocation();

            FlgResizeBegin = 0;
        }

        private void 退勤Form_SizeChanged(object sender, EventArgs e)
        {
            if (FlgResizeBegin == 1) return;

            RebuildTemplate();
            ChangePanelMessageLocation();
        }

        private void RebuildTemplate()
        {
            gcMultiRow1.EndEdit();
            var list = GetWorkContentDetails();
            Template作成();
            Time行作成();
            SetWorkContentDetail(list);
        }


        private void ChangePanelMessageLocation()
        {
            var l = (this.Width - panel_Message.Width) / 2;
            var t = (this.Height - panel_Message.Height) / 2;

            panel_Message.Location = new Point(l, t);
        }
    }
}
