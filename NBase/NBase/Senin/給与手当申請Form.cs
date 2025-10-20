using Senin.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseCommon.Senin;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace Senin
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
            InitComboBox船();
            InitComboBox年();
            InitComboBox月();

            tableLayoutPanel2.Visible = false;
            tableLayoutPanel3.Visible = false;

            SetScrollBarValues();
        }

        private void InitComboBox船()
        {
            foreach (MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            {
                comboBox船.Items.Add(v);
            }
            comboBox船.SelectedIndex = 0;
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
            MsVessel vessel = comboBox船.SelectedItem as MsVessel;
            DateTime start = new DateTime((int)comboBox年.SelectedItem, (int)comboBox月.SelectedItem, 1);
            DateTime end = start.AddMonths(1);

            selectedVessel = vessel;
            selectedYear = (int)comboBox年.SelectedItem;
            selectedMonth = (int)comboBox月.SelectedItem;

            SiCardFilter filter = new SiCardFilter();
            filter.MsVesselIDs.Add(vessel.MsVesselID);
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船));
            filter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船休暇));
            filter.Start = start.AddMonths(-1); // 指定月＋前月としたい
            filter.End = end;
            filter.OrderByStr = "OrderByMsSiShokumeiId";

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    cards = serviceClient.SiCard_GetRecordsByFilter(NBaseCommon.Common.LoginUser, filter);

                    kyuyoTeateList = serviceClient.SiKyuyoTeate_SearchRecords(NBaseCommon.Common.LoginUser, selectedVessel.MsVesselID, selectedYear.ToString() + selectedMonth.ToString("00"));
                }
            }, "データ取得中です...");
            progressDialog.ShowDialog();

            int seninCount = cards.Select(obj => obj.MsSeninID).Distinct().Count();

            tableLayoutPanel2.SuspendLayout();

            tableLayoutPanel2.Controls.Clear();
            tableLayoutPanel2.Height = 70;


            tableLayoutPanel2.RowCount = seninCount;

            List<int> seninIds = new List<int>();
            foreach(SiCard card in cards)
            {
                if (seninIds.Contains(card.MsSeninID))
                    continue;

                string dateStr = " " +card.StartDate.ToString("MM/dd") + "～";
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
            tableLayoutPanel2.Height = tableLayoutPanel2.RowCount * 70;


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
            Dictionary<int, List<SiKyuyoTeate>> kyuyoTeateDic = new Dictionary<int, List<SiKyuyoTeate>>();
            foreach(SiKyuyoTeate kyuyoTeate in kyuyoTeateList)
            {
                if (kyuyoTeateDic.ContainsKey(kyuyoTeate.MsSeninID) == false)
                {
                    kyuyoTeateDic.Add(kyuyoTeate.MsSeninID, new List<SiKyuyoTeate>());
                }
                kyuyoTeateDic[kyuyoTeate.MsSeninID].Add(kyuyoTeate);

            }

            int max = 0;
            foreach(int seninId in kyuyoTeateDic.Keys)
            {
                if (kyuyoTeateDic[seninId].Where(obj => obj.DeleteFlag == 0).Count() > max)
                {
                    max = kyuyoTeateDic[seninId].Where(obj => obj.DeleteFlag == 0).Count();
                }
            }


            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel3.Controls.Clear();
            tableLayoutPanel3.Height = 70;

            tableLayoutPanel3.RowCount = tableLayoutPanel2.RowCount;
            tableLayoutPanel3.ColumnCount = max;


            if (max > 0)
            {
                List<int> seninIds = new List<int>();
                foreach (SiCard card in cards)
                {
                    if (seninIds.Contains(card.MsSeninID))
                        continue;

                    if (kyuyoTeateDic.ContainsKey(card.MsSeninID))
                    {
                        var kyuyoTeateBySenin = kyuyoTeateDic[card.MsSeninID].Where(obj => obj.DeleteFlag == 0);

                        for (int i = 0; i < kyuyoTeateBySenin.Count(); i++)
                        {
                            tableLayoutPanel3.Controls.Add(new TeateUserControl(this, NBaseCommon.Common.LoginUser, SeninTableCache.instance(), (SiKyuyoTeate)kyuyoTeateBySenin.ElementAt(i)), i, seninIds.Count());
                        }
                        for (int i = kyuyoTeateBySenin.Count(); i < max; i++)
                        {
                            tableLayoutPanel3.Controls.Add(new TeateUserControl(this, NBaseCommon.Common.LoginUser, SeninTableCache.instance(), null), i, seninIds.Count());
                        }
                    }
                    else
                    {
                        for (int i = 0; i < max; i++)
                        {
                            tableLayoutPanel3.Controls.Add(new TeateUserControl(this, NBaseCommon.Common.LoginUser, SeninTableCache.instance(), null), i, seninIds.Count());
                        }
                    }

                    seninIds.Add(card.MsSeninID);
                }
                tableLayoutPanel3.Height = tableLayoutPanel3.RowCount * 70;

                tableLayoutPanel3.ResumeLayout();

                tableLayoutPanel3.Visible = true;
            }
            else
            {
                tableLayoutPanel3.Visible = false;
            }
        }




        private void button給与手当追加_Click(object sender, EventArgs e)
        {
            if (cards == null || cards.Count() == 0)
            {
                MessageBox.Show("データが検索されていません。検索を実行後、「給与手当追加」をクリックして下さい。");
                return;
            }
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
            年月指定出力Form form = new 年月指定出力Form(年月指定出力Form.帳票種別.給与手当一覧表);
            form.ShowDialog();
        }

    }
}
