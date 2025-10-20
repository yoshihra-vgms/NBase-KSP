using NBaseHonsen.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseCommon.Senin;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;
using SyncClient;
using NBaseCommon.Senin.Excel;

namespace NBaseHonsen.Senin
{
    public partial class 給与手当申請Form : Form
    {
        private MsVessel selectedVessel;
        private int selectedYear;
        private int selectedMonth;

        public List<SiCard> cards = null;
        public List<SiKyuyoTeate> kyuyoTeateList = null;

        private static 給与手当申請Form instance;

        private 給与手当申請Form()
        {
            InitializeComponent();
            Init();
        }


        public static 給与手当申請Form Instance()
        {
            if (instance == null)
            {
                instance = new 給与手当申請Form();
            }

            return instance;
        }

        private void 給与手当申請Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            instance = null;
        }

        private void Init()
        {
            InitComboBox年();
            InitComboBox月();

            tableLayoutPanel2.Visible = false;
            tableLayoutPanel3.Visible = false;

            SetScrollBarValues();
        }

        private void InitComboBox年()
        {
            int thisYear = DateTime.Now.Year;

            for (int i = 0; i < 3; i++)
            {
                comboBox年.Items.Add(thisYear - i);
            }

            comboBox年.SelectedItem = thisYear;
        }

        private void InitComboBox月()
        {
            for (int i = 0; i < 12; i++)
            {
                int m = i + 1;
                comboBox月.Items.Add(m);

                if (m == DateTime.Now.Month)
                {
                    comboBox月.SelectedItem = m;
                }
            }
        }



        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            tableLayoutPanel2.Location = new Point(tableLayoutPanel2.Location.X, -e.NewValue);
            tableLayoutPanel3.Location = new Point(tableLayoutPanel3.Location.X, -e.NewValue);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            tableLayoutPanel3.Location = new Point(-e.NewValue, tableLayoutPanel3.Location.Y);
        }

        private void tableLayoutPanel4_Resize(object sender, EventArgs e)
        {
            SetScrollBarValues();
        }

        private void SetScrollBarValues()
        {
            vScrollBar1.Maximum = tableLayoutPanel2.Height;
            vScrollBar1.LargeChange = panel1.Height;

            hScrollBar1.Maximum = tableLayoutPanel3.Width;
            hScrollBar1.LargeChange = panel2.Width;

        }




        private void button検索_Click(object sender, EventArgs e)
        {
            MsVessel vessel = 同期Client.LOGIN_VESSEL;
            DateTime start = new DateTime((int)comboBox年.SelectedItem, (int)comboBox月.SelectedItem, 1);
            DateTime end = start.AddMonths(1);

            selectedVessel = vessel;
            selectedYear = (int)comboBox年.SelectedItem;
            selectedMonth = (int)comboBox月.SelectedItem;

            SiCardFilter filter = new SiCardFilter();
            filter.MsVesselIDs.Add(vessel.MsVesselID);
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(同期Client.LOGIN_USER, MsSiShubetsu.SiShubetsu.乗船));
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(同期Client.LOGIN_USER, MsSiShubetsu.SiShubetsu.乗船休暇));
            filter.Start = start.AddMonths(-1); // 指定月＋前月としたい
            filter.End = end;
            filter.OrderByStr = "OrderByMsSiShokumeiId";

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                cards = SiCard.GetRecordsByFilter(同期Client.LOGIN_USER, filter);

                List<NBaseData.DAC.SiKyuyoTeate> all = NBaseData.DAC.SiKyuyoTeate.GetRecords(同期Client.LOGIN_USER);

                var ret = all.Where(obj => obj.MsVesselID == selectedVessel.MsVesselID).ToList();
                string ym = selectedYear.ToString() + selectedMonth.ToString("00");

                if (ret.Any(obj => obj.YM == ym))
                {
                    ret = ret.Where(obj => obj.YM == ym).ToList();
                }
                else
                {
                    // 指定月のデータがない場合、１ヵ月前のデータをコピーする
                    string lastMonth = DateTime.Parse(ym.Substring(0, 4) + "/" + ym.Substring(4, 2) + "/01").AddMonths(-1).ToString("yyyyMM");
                    var tmp = ret.Where(obj => obj.YM == lastMonth).ToList();
                    ret.Clear();

                    foreach (NBaseData.DAC.SiKyuyoTeate info in tmp)
                    {
                        info.StartDate = DateTime.MinValue;
                        info.EndDate = DateTime.MinValue;
                        info.Days = int.MinValue;
                        info.HonsenKingaku = int.MinValue;
                        info.Kingaku = int.MinValue;
                        ret.Add(info);
                    }
                }
                kyuyoTeateList = ret.ToList();

            }, "データ取得中です...");
            progressDialog.ShowDialog();

            int seninCount = cards.Select(obj => obj.MsSeninID).Distinct().Count();

            tableLayoutPanel2.SuspendLayout();

            tableLayoutPanel2.Controls.Clear();
            tableLayoutPanel2.Height = 94;
            tableLayoutPanel2.Width = 280;


            tableLayoutPanel2.RowCount = seninCount;

            List<int> seninIds = new List<int>();
            foreach(SiCard card in cards)
            {
                if (seninIds.Contains(card.MsSeninID))
                    continue;

                string dateStr = " " + card.StartDate.ToString("MM/dd") + "～";
                if (card.EndDate != DateTime.MinValue)
                {
                    dateStr += card.EndDate.ToString("MM/dd");
                }
                DateTime endDay = card.EndDate;
                if (endDay == DateTime.MinValue)
                {
                    if (end > DateTime.Today)
                    {
                        endDay = DateTime.Today;
                    }
                    else
                    {
                        endDay = end;
                    }
                }
                else
                {
                    if (endDay > end)
                    {
                        endDay = end;
                    }
                }
                string daysStr = (DateTimeUtils.ToTo(endDay) - DateTimeUtils.ToFrom(card.StartDate)).Days.ToString() + " ";

                tableLayoutPanel2.Controls.Add(new SeninUserControl(SeninTableCache.instance().ToTopShokumeiAbbrStr(NBaseCommon.Common.LoginUser, card.SiLinkShokumeiCards), card.SeninName, dateStr, daysStr), 0, seninIds.Count());
                                               
                seninIds.Add(card.MsSeninID);

            }

            tableLayoutPanel2.Height = tableLayoutPanel2.RowCount * 94;
            tableLayoutPanel2.Width = tableLayoutPanel2.ColumnCount * 280;

            tableLayoutPanel2.ResumeLayout();
            tableLayoutPanel3.ResumeLayout();


            if (seninIds.Count() > 0)
                tableLayoutPanel2.Visible = true;
            else
                tableLayoutPanel2.Visible = false;


            SetKyuyoTeateControl();

            SetScrollBarValues();
        }

        private void SetKyuyoTeateControl()
        {
            string debugStr = "";
            try
            {
                debugStr = "1:";
                if (kyuyoTeateList == null)
                    debugStr = "kyuyoTeateList is null" ;

                Dictionary<int, List<SiKyuyoTeate>> kyuyoTeateDic = new Dictionary<int, List<SiKyuyoTeate>>();
                if (kyuyoTeateList != null)
                {
                    foreach (SiKyuyoTeate kyuyoTeate in kyuyoTeateList)
                    {
                        if (kyuyoTeateDic.ContainsKey(kyuyoTeate.MsSeninID) == false)
                        {
                            kyuyoTeateDic.Add(kyuyoTeate.MsSeninID, new List<SiKyuyoTeate>());
                        }
                        kyuyoTeateDic[kyuyoTeate.MsSeninID].Add(kyuyoTeate);

                    }
                }

                debugStr += "2:";

                int max = 0;
                foreach (int seninId in kyuyoTeateDic.Keys)
                {
                    if (kyuyoTeateDic[seninId].Where(obj => obj.DeleteFlag == 0).Count() > max)
                    {
                        max = kyuyoTeateDic[seninId].Where(obj => obj.DeleteFlag == 0).Count();
                    }
                }
                debugStr += "3:";


                tableLayoutPanel3.SuspendLayout();
                tableLayoutPanel3.Controls.Clear();
                tableLayoutPanel3.Height = 94;

                tableLayoutPanel3.RowCount = tableLayoutPanel2.RowCount;
                tableLayoutPanel3.ColumnCount = max;


                if (max > 0)
                {
                    debugStr += "4:";

                    List<int> seninIds = new List<int>();
                    foreach (SiCard card in cards)
                    {
                        if (seninIds.Contains(card.MsSeninID))
                            continue;

                        if (kyuyoTeateDic.ContainsKey(card.MsSeninID))
                        {
                            var kyuyoTeateBySenin = kyuyoTeateDic[card.MsSeninID].Where(obj => obj.DeleteFlag == 0 && obj.CancelFlag == 0);

                            for (int i = 0; i < kyuyoTeateBySenin.Count(); i++)
                            {
                                tableLayoutPanel3.Controls.Add(new TeateUserControl(this, 同期Client.LOGIN_USER, SeninTableCache.instance(), (SiKyuyoTeate)kyuyoTeateBySenin.ElementAt(i)), i, seninIds.Count());
                            }
                            for (int i = kyuyoTeateBySenin.Count(); i < max; i++)
                            {
                                tableLayoutPanel3.Controls.Add(new TeateUserControl(this, 同期Client.LOGIN_USER, SeninTableCache.instance(), null), i, seninIds.Count());
                            }
                        }
                        else
                        {
                            for (int i = 0; i < max; i++)
                            {
                                tableLayoutPanel3.Controls.Add(new TeateUserControl(this, 同期Client.LOGIN_USER, SeninTableCache.instance(), null), i, seninIds.Count());
                            }
                        }

                        seninIds.Add(card.MsSeninID);
                    }

                    tableLayoutPanel3.Height = tableLayoutPanel3.RowCount * 94;

                    tableLayoutPanel3.ResumeLayout();

                    tableLayoutPanel3.Visible = true;
                }
                else
                {
                    tableLayoutPanel3.Visible = false;
                }
            }
            catch(Exception ex)
            {
                NBaseCommon.LogFile.LocalLogWrite("", debugStr);
                NBaseCommon.LogFile.LocalLogWrite("", ex.Message);
            }

        }




        private void button給与手当追加_Click(object sender, EventArgs e)
        {
            if (cards == null)
                return;

            給与手当登録Form form = new 給与手当登録Form(this, selectedVessel, selectedYear, selectedMonth, cards);

            form.ShowDialog();
        }

        public void SetSiKyuyoTeate(SiKyuyoTeate kyuyoTeate)
        {
            if (kyuyoTeateList.Any(obj => obj.SiKyuyoTeateID == kyuyoTeate.SiKyuyoTeateID))
            {
                for(int i = 0; i < kyuyoTeateList.Count(); i++)
                {
                    if (kyuyoTeateList[i].SiKyuyoTeateID == kyuyoTeate.SiKyuyoTeateID)
                    {
                        kyuyoTeateList[i] = kyuyoTeate;
                        break;
                    }
                }
            }
            else
            {
                kyuyoTeateList.Add(kyuyoTeate);
            }

            SetKyuyoTeateControl();
        }

        private void button出力_Click(object sender, EventArgs e)
        {

            FileUtils.SetDesktopFolder(saveFileDialog1);

            DateTime date = new DateTime((int)comboBox年.SelectedItem, (int)comboBox月.SelectedItem, 1);
            saveFileDialog1.FileName = "給与手当一覧表_" + date.ToString("yyyyMM") + ".xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string exeDir = System.IO.Directory.GetCurrentDirectory();
                string templateFilePath = exeDir + "\\Template\\Template_給与手当一覧表.xlsx";

                new 給与手当一覧表出力(templateFilePath, saveFileDialog1.FileName).CreateFile(同期Client.LOGIN_USER, SeninTableCache.instance(), date, 同期Client.LOGIN_VESSEL.MsVesselID);

                MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
