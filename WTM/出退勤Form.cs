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
using NBaseUtil;
using WtmData;

namespace WTM
{
    public partial class 出退勤Form : Form
    {
        public delegate void EditedEventHandler(object sender, EventArgs e);
        public event EditedEventHandler EditedEvent;


        private Work Work;


        private DateTime KinmuStart = DateTime.MinValue;
        private DateTime KinmuFinish = DateTime.MaxValue;


        private Dictionary<TextBox, List<Label>> InputText_Dic = new Dictionary<TextBox, List<Label>>();





        public 出退勤Form(Work Work)
        {
            InitializeComponent();

            groupBox1.MouseClick += new MouseEventHandler(ClickEvent);
            groupBox2.MouseClick += new MouseEventHandler(ClickEvent);

            this.Work = Work;
        }


        private void 出退勤Form_Load(object sender, EventArgs e)
        {
            //フォームの制御
            //this.TopMost = true;

            labelTitle.Text = "勤務開始、終了日時を選択してください";

            // 終了メッセージ非表示
            panel_Message.Visible = false;

            // 削除確認メッセージ非表示
            panel_DeleteConfirm.Visible = false;


            InputText_Dic.Add(textBox勤務開始日, new List<Label>() { label勤務開始日, label勤務開始日説明 });
            InputText_Dic.Add(textBox勤務開始時間, new List<Label>() { label勤務開始時間, label勤務開始時間説明 });
            InputText_Dic.Add(textBox勤務終了日, new List<Label>() { label勤務終了日, label勤務終了日説明 });
            InputText_Dic.Add(textBox勤務終了時間, new List<Label>() { label勤務終了時間, label勤務終了時間説明 });


            //勤務開始時間
            KinmuStart = Work.StartWork;
            textBox勤務開始日.Text = KinmuStart.ToString("yyMMdd");
            textBox勤務開始時間.Text = KinmuStart.ToString("HHmm");

            //勤務終了日、時間
            KinmuFinish = Work.FinishWork;
            if (KinmuFinish != DateTime.MaxValue)
            {
                // 編集時（既に終了時間が登録されている場合）
                textBox勤務終了日.Text = KinmuFinish.ToString("yyMMdd");
                textBox勤務終了時間.Text = KinmuFinish.ToString("HHmm");
            }
            else if (string.IsNullOrEmpty(Work.WorkID))
            {
                // 新規の場合、終了日は開始日と同じ日とする
                textBox勤務終了日.Text = KinmuStart.ToString("yyMMdd");
                textBox勤務終了時間.Text = "";
            }
            else
            {
                // 出勤中の場合、終了日は空とする
                textBox勤務終了日.Text = "";
                textBox勤務終了時間.Text = "";
            }

            //フォーカス
            this.ActiveControl = textBox勤務開始日;

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

            //Template変更しても値を保持
            gcMultiRow1.RestoreValue = true;
            gcMultiRow1.RestoreColumnHeaderFooterValue = true;

            Template作成();

            Time行作成();

            SetWorkContentDetail(Work.WorkContentDetails);


            if (string.IsNullOrEmpty(Work.WorkID))
            {
                // 新規の場合、削除ボタン、コピーボタンはなし
                buttonDelete.Visible = false;
                buttonCopy.Visible = false;
            }
            else
            {
                if (WtmCommon.VesselMode == false)
                {
                    // 船モードでない場合、コピーボタンはなし
                    buttonRegist.Location = new Point(524, 14);
                    buttonCancel.Location = buttonCopy.Location;
                    buttonCopy.Visible = false;
                }
            }
        }


        private void 出退勤Form_FormClosed(object sender, FormClosedEventArgs e)
        {

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

            if (KinmuStart == DateTime.MinValue || KinmuFinish == DateTime.MaxValue)
                return;


            // 表示時間のため、まるめ処理
            DateTime wkStart = (DateTime)KinmuStart;
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
            while (wkdate < (DateTime)KinmuFinish)
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


        private void buttonRegist_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateFields() == false)
                {
                    return;
                }

                var startWork = DateTime.Parse(KinmuStart.ToString("yyyy/MM/dd HH:mm"));
                var finishWork = DateTime.Parse(KinmuFinish.ToString("yyyy/MM/dd HH:mm"));

                var checkWorks = WtmAccessor.Instance().GetWorks(startWork, finishWork, int.Parse(Work.CrewNo));
                if (checkWorks != null && checkWorks.Count() > 0)
                {
                    var workId = Work.WorkID;
                    if (string.IsNullOrEmpty(workId))
                    {
                        workId = "";
                    }
                    var overlaps = checkWorks.Where(o => o.WorkID != workId && ((o.StartWork == startWork && finishWork == o.FinishWork) ||
                                                                                (o.StartWork < startWork && startWork < o.FinishWork) || (o.StartWork < finishWork && finishWork < o.FinishWork) ||
                                                                                (startWork < o.StartWork && o.StartWork < finishWork) || (startWork < o.FinishWork && o.FinishWork < finishWork)));
                    if (overlaps.Count() > 0)
                    {
                        MessageBox.Show("作業の時間が重複しています。");
                        return;
                    }
                }



                button_確認.Visible = false;
                label_Message.Text = "処理中です...";
                panel_Message.Visible = true;
                panel_Message.Update();


                Work.StartWork = startWork;
                if (Work.StartWorkAcutual == DateTime.MinValue)
                {
                    Work.StartWorkAcutual = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                }
                Work.FinishWork = finishWork;
                if (Work.FinishWorkAcutual == DateTime.MaxValue)
                {
                    Work.FinishWorkAcutual = DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
                }


                string wcdStr = null;

                string stime = null;
                string wcId = null;
                int count = 0;
                foreach (var row in gcMultiRow1.Rows)
                {
                    if (stime == null)
                    {
                        wcId = GetWorkContent(row);
                        if (wcId != null)
                        {
                            stime = "20" + (string)row.Cells["labelCellDate"].Value + " " + (string)row.Cells["labelCellTime"].Value + ":00";
                            count++;
                        }
                    }
                    else if (wcId == GetWorkContent(row))
                    {
                        count++;
                    }
                    else
                    {
                        wcdStr += stime + "," + wcId + "," + count.ToString() + ";";

                        wcId = GetWorkContent(row);
                        if (wcId != null)
                        {
                            stime = "20" + (string)row.Cells["labelCellDate"].Value + " " + (string)row.Cells["labelCellTime"].Value + ":00";
                            count = 1;
                        }
                    }
                }
                wcdStr += stime + "," + wcId + "," + count.ToString() + ";";
                Work.WorkContentDetail = wcdStr;


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
                Work.NightTime = ntStr;

                Work.UpdateDate = DateTime.Now;


                var ret = WtmAccessor.Instance().FinshWork(Work);

                if (ret)
                {
                    if (EditedEvent != null)
                    {
                        EditedEvent(this, null);
                    }

                    tableLayoutPanel1.Enabled = false;

                    label_Message.Text = "登録しました。";
                    button_確認.Visible = true;
                    panel_Message.Update();
                }
                else
                {
                    label_Message.Text = "登録が失敗しました。";
                    button_確認.Visible = true;
                    panel_Message.Update();
                }
            }
            catch 
            {
                if (panel_Message.Visible)
                {
                    panel_Message.Visible = false;
                    button_確認.Visible = true;
                    label_Message.Text = "お疲れ様でした。";
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

        /// <summary>
        /// 「削除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonDelete_Click(object sender, EventArgs e)
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (ApprovalCheck())
            {
                MessageBox.Show("承認済の日もしくは月が含まれているため、削除できません。");
                return;
            }

            // 削除確認パネルを開く
            panel_DeleteConfirm.Visible = true;
            tableLayoutPanel1.Enabled = false;
        }
        #endregion

        /// <summary>
        /// 削除確認パネル「削除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonExecDelete_Click(object sender, EventArgs e)
        private void buttonExecDelete_Click(object sender, EventArgs e)
        {
            // 削除
            var ret = WtmAccessor.Instance().DeleteWork(Work);

            if (ret)
            {
                if (EditedEvent != null)
                {
                    EditedEvent(this, null);
                }

                panel_DeleteConfirm.Visible = false;
                label_Message.Text = "削除しました。";
                panel_Message.Visible = true;
            }
        }
        #endregion

        /// <summary>
        /// 削除確認パネル「キャンセル」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonCancelDelete_Click(object sender, EventArgs e)
        private void buttonCancelDelete_Click(object sender, EventArgs e)
        {
            panel_DeleteConfirm.Visible = false;
            tableLayoutPanel1.Enabled = true;
        }
        #endregion


        private void buttonCopy_Click(object sender, EventArgs e)
        {
            コピーForm f = new コピーForm(Work);
            var ret = f.ShowDialog();
            if (ret == DialogResult.OK)
            {
                if (EditedEvent != null)
                {
                    EditedEvent(this, null);
                }
                Close();
            }
        }

        /// <summary>
        /// 「キャンセル」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonCancel_Click(object sender, EventArgs e)
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        /// <summary>
        /// 「確認」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_確認_Click(object sender, EventArgs e)
        private void button_確認_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #endregion


        #region 日時Textbox　イベント

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

            DateTime? inputStartDate = GetInputDate(textBox勤務開始日, textBox勤務開始時間);
            DateTime? inputFinishDate = GetInputDate(textBox勤務終了日, textBox勤務終了時間);

            if (inputStartDate != null && inputFinishDate != null &&
                KinmuStart == (DateTime)inputStartDate && KinmuFinish == (DateTime)inputFinishDate)
                    return;

            KinmuStart = inputStartDate != null ? (DateTime)inputStartDate : DateTime.MinValue;
            KinmuFinish = inputFinishDate != null ? (DateTime)inputFinishDate : DateTime.MaxValue;

            Time行作成();
        }

        private DateTime? GetInputDate(TextBox dt, TextBox tt)
        {
            DateTime? ret = null;

            if (dt.Text.Length != 6 || tt.Text.Length != 4)
            {
                return ret;
            }

            string str = dt.Text.Substring(0, 2) + "/" + dt.Text.Substring(2, 2) + "/" + dt.Text.Substring(4, 2);
            DateTime outDateTime = DateTime.MinValue;

            DateTime work;
            if (DateTime.TryParse(str, out work))
            {; }
            else
            {
                return ret;
            }
            ret = work.Date;

            str = tt.Text.Substring(0, 2) + ":" + tt.Text.Substring(2, 2);
            work = DateTime.MinValue;
            if (DateTime.TryParse(str, out work))
            {; }
            else
            {
                return ret;
            }

            ret = ((DateTime)ret).AddHours(work.Hour).AddMinutes(work.Minute);

            return ret;
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
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
            gcMultiRow1.Focus();
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
                自分以外のチェックを外す(ck,rowindex);
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
                if (gcMultiRow1.Rows[rowindex].Cells[i].Name==ck.Name) continue;

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

        private bool ValidateFields()
        {
            if (ValidateDateTime(textBox勤務開始日, textBox勤務開始時間) == false)
                return false;

            if (ValidateDateTime(textBox勤務終了日, textBox勤務終了時間) == false)
                return false;

            if (KinmuStart >= KinmuFinish)
            {
                MessageBox.Show("終了時間は、開始時間より後を入力してください。");
                return false;
            }

            if (ApprovalCheck())
            {
                MessageBox.Show("承認済の日もしくは月が含まれているため、登録できません。");
                return false;
            }

            if (ValidationWorkContentDetails() == false)
                return false;

            return true;
        }

        private bool ValidateDateTime(TextBox td, TextBox tt)
        {
            if (td.Text.Length != 6)
            {
                MessageBox.Show("正しい年月日を入力してください。");
                return false;
            }
            if (tt.Text.Length != 4)
            {
                MessageBox.Show("正しい時間を入力してください。");
                return false;
            }

            string str = td.Text.Substring(0, 2) + "/" + td.Text.Substring(2, 2) + "/" + td.Text.Substring(4, 2);
            DateTime outDateTime = DateTime.MinValue;

            if (DateTime.TryParse(str, out outDateTime))
            {; }
            else
            {
                MessageBox.Show("正しい年月日を入力してください。");
                return false;
            }

            str = tt.Text.Substring(0, 2) + ":" + tt.Text.Substring(2, 2);
            outDateTime = DateTime.MinValue;
            if (DateTime.TryParse(str, out outDateTime))
            {; }
            else
            {
                MessageBox.Show("正しい時間を入力してください。");
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

        private void 出退勤Form_ResizeBegin(object sender, EventArgs e)
        {
            FlgResizeBegin = 1;
        }
        private void 出退勤Form_ResizeEnd(object sender, EventArgs e)
        {
            RebuildTemplate();
            ChangePanelMessageLocation();

            FlgResizeBegin = 0;
        }

        private void 出退勤Form_SizeChanged(object sender, EventArgs e)
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

            l = (this.Width - panel_DeleteConfirm.Width) / 2;
            t = (this.Height - panel_DeleteConfirm.Height) / 2;

            panel_DeleteConfirm.Location = new Point(l, t);
        }


        private bool ApprovalCheck()
        {
            bool ret = false;

            var startWork = DateTime.Parse(KinmuStart.ToString("yyyy/MM/dd HH:mm"));
            var finishWork = DateTime.Parse(KinmuFinish.ToString("yyyy/MM/dd HH:mm"));

            if (WtmCommon.VesselMode && WtmCommon.FlgShowApproval)
            {
                // 月次承認済みの確認
                if (WtmAccessor.Instance().GetVesselApprovalMonth(Common.Vessel.MsVesselID, DateTimeUtils.ToFromMonth(startWork)) != null)
                {
                    ret = true;
                }
                else if (WtmAccessor.Instance().GetVesselApprovalMonth(Common.Vessel.MsVesselID, DateTimeUtils.ToFromMonth(finishWork)) != null)
                {
                    ret = true;
                }

                var startDayList = WtmAccessor.Instance().GetVesselApprovalDay(Common.Vessel.MsVesselID, startWork.Date);
                var vad = startDayList.Where(o => o.ApprovedCrewNo == Work.CrewNo).FirstOrDefault();
                if (vad != null)
                {
                    ret = true;
                }

                var finishDayList = WtmAccessor.Instance().GetVesselApprovalDay(Common.Vessel.MsVesselID, finishWork.Date);
                vad = finishDayList.Where(o => o.ApprovedCrewNo == Work.CrewNo).FirstOrDefault();
                if (vad != null)
                {
                    ret = true;
                }
            }

            return ret;
        }
    }
}
