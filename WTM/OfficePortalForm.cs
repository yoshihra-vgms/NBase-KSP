using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.BLC;
using NBaseData.DS;
using NBaseUtil;
using WtmModelBase;
using WtmModels;
using WtmData;

namespace WTM
{
    public partial class OfficePortalForm : Form
    {
        private DateTime TargetDate;

        private class PortalData
        {
            public int MsSiShokumeiId;
            public string CrewName;
            public bool BeInWork;
            public DateTime? StartDate;
            public DateTime? StartWork;
            public DateTime? StartWorkAcutual;
            public double WorkMinutes;
            public double RestMinutes;
            public double WorkMinutesWeek;
        }


        public OfficePortalForm()
        {
            InitializeComponent();
        }

        private void OfficePortalForm_Load(object sender, EventArgs e)
        {
            panel_SearchMessage.Visible = false;

            var strTargetDate = WtmCommon.GetString("TargetDate");
            if (string.IsNullOrEmpty(strTargetDate))
            {
                TargetDate = DateTime.Today;
            }
            else
            {
                if (strTargetDate == "TODAY")
                {
                    TargetDate = DateTime.Today;
                }
                else
                {
                    try
                    {
                        TargetDate = DateTime.Parse(strTargetDate);
                    }
                    catch
                    {
                        TargetDate = DateTime.Today;
                    }
                }
            }

            //船を検索、コンボにセット
            InitCommbobox船();

        }

        #region private void InitCommbobox船()
        private void InitCommbobox船()
        {
            foreach (MsVessel v in NBaseCommon.Common.VesselList)
            {
                comboBox船.Items.Add(v);                
            }
            if (comboBox船.Items.Count > 0) comboBox船.SelectedIndex = 0;
        }
        #endregion

        private void comboBox船_TextChanged(object sender, EventArgs e)
        {
            SetData();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            SetData();
        }

        private void SetData()
        {
            if (!(comboBox船.SelectedItem is MsVessel)) return;
            MsVessel v = comboBox船.SelectedItem as MsVessel;

            panel_SearchMessage.Visible = true;
            panel_SearchMessage.Update();

            var cards = Common.GetOnSigner(v.MsVesselID, TargetDate, TargetDate);

            List<Work> wklist = WtmAccessor.Instance().GetWorks(TargetDate.AddDays(-6), TargetDate, vesselId: v.MsVesselID);

            List<PortalData> pdList = new List<PortalData>();


            foreach (MsSiShokumei shokumei in NBaseCommon.Common.ShokumeiList)
            {
                var targetCards = cards.Where(o => o.SiLinkShokumeiCards[0].MsSiShokumeiID == shokumei.MsSiShokumeiID);
                var onCrewId = targetCards.Select(o => o.MsSeninID).Distinct();


                var senins = NBaseCommon.Common.SeninList.Where(o => onCrewId.Contains(o.MsSeninID));
                if (senins != null)
                {
                    foreach (MsSenin senin in senins)
                    {
                        PortalData pd = new PortalData();

                        var card = targetCards.Where(o => o.MsSeninID == senin.MsSeninID).OrderBy(obj=>obj.StartDate).LastOrDefault();//１つしかないはずだけど念のため

                        pd.MsSiShokumeiId = card.SiLinkShokumeiCards[0].MsSiShokumeiID;
                        pd.CrewName = senin.FullName;
                        pd.StartDate = card.StartDate;

                        var inWork = WtmAccessor.Instance().GetBeInWork(senin.MsSeninID);
                        if (inWork != null && inWork.StartWork.Date <= TargetDate)
                        {
                            wklist.Add(inWork);
                            pd.BeInWork = true;
                        }
                        else
                        {
                            pd.BeInWork = false;
                        }
                        var wks = wklist.Where(o => o.CrewNo == senin.MsSeninID.ToString()).OrderBy(o => o.StartWork);


                        double workMinutes = 0;
                        double workMinute1Weeks = 0;

                        foreach (Work w in wks)
                        {
                            if (w.FinishWork == DateTime.MaxValue)
                            {
                                w.FinishWork = DateTime.Now;
                            }

                            foreach (WorkContentDetail wd in w.WorkContentDetails)
                            {
                                var wc = WtmCommon.WorkContentList.Where(o => o.WorkContentID == wd.WorkContentID).FirstOrDefault();
                                if (wc == null || wc.IsIncludeWorkTime == false)
                                    continue;

                                DateTime st = wd.WorkDate;
                                DateTime fin = st.AddMinutes(WtmCommon.WorkRange);
                                if (st < w.StartWork)
                                    st = w.StartWork;
                                if (fin > w.FinishWork)
                                    fin = w.FinishWork;

                                var m = (fin - st).TotalMinutes;

                                workMinute1Weeks += m;

                                if (st.Date == TargetDate.Date)
                                {
                                    workMinutes += m;
                                }
                            }
                        }

                        if (wks != null && wks.Count() > 0)
                        {
                            pd.StartWork = wks.Last().StartWork;
                            pd.StartWorkAcutual = wks.Last().StartWorkAcutual;
                            pd.WorkMinutes = workMinutes;
                            pd.WorkMinutesWeek = workMinute1Weeks;
                            pd.RestMinutes = (24 * 60) - workMinutes;
                        }


                        pdList.Add(pd);
                    }

                }
            }

            #region カラム作成
            //カラムが無い場合作成する
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                

                dataGridView1.Columns.Add("職位", "職位");
                dataGridView1.Columns["職位"].Width = 80;
                dataGridView1.Columns["職位"].SortMode = DataGridViewColumnSortMode.NotSortable;

                dataGridView1.Columns.Add("氏名", "氏名");
                dataGridView1.Columns["氏名"].Width = 150;
                dataGridView1.Columns["氏名"].SortMode = DataGridViewColumnSortMode.NotSortable;

                dataGridView1.Columns.Add("ステータス", "ステータス");
                dataGridView1.Columns["ステータス"].Width = 80;
                dataGridView1.Columns["ステータス"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns["ステータス"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["ステータス"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dataGridView1.Columns.Add("乗船日", "乗船日");
                dataGridView1.Columns["乗船日"].Width = 100;
                dataGridView1.Columns["乗船日"].SortMode = DataGridViewColumnSortMode.NotSortable;

                dataGridView1.Columns.Add("開始時間S", "開始時間(Stamp)");
                dataGridView1.Columns["開始時間S"].Width = 160;
                dataGridView1.Columns["開始時間S"].SortMode = DataGridViewColumnSortMode.NotSortable;

                dataGridView1.Columns.Add("開始時間A", "開始時間(Actual)");
                dataGridView1.Columns["開始時間A"].Width = 160;
                dataGridView1.Columns["開始時間A"].SortMode = DataGridViewColumnSortMode.NotSortable;

                dataGridView1.Columns.Add("勤務時間合計", "勤務時間" + System.Environment.NewLine + "合計(日)");
                dataGridView1.Columns["勤務時間合計"].Width = 80;
                dataGridView1.Columns["勤務時間合計"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns["勤務時間合計"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["勤務時間合計"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dataGridView1.Columns.Add("休息時間合計", "休息時間" + System.Environment.NewLine + "合計(日)");
                dataGridView1.Columns["休息時間合計"].Width = 80;
                dataGridView1.Columns["休息時間合計"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns["休息時間合計"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["休息時間合計"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dataGridView1.Columns.Add("勤務時間", "勤務時間" + System.Environment.NewLine + "(週)");
                dataGridView1.Columns["勤務時間"].Width = 80;
                dataGridView1.Columns["勤務時間"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns["勤務時間"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["勤務時間"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //
                dataGridView1.RowTemplate.Height = 40;

                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dataGridView1.ColumnHeadersHeight = 50;

                dataGridView1.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            }
            #endregion

            dataGridView1.Rows.Clear();

            int i = 0;
            foreach (PortalData pd in pdList)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["職位"].Value = $" {NBaseCommon.Common.ShokumeiList.Where(obj => obj.MsSiShokumeiID == pd.MsSiShokumeiId).FirstOrDefault().NameAbbr}";
                dataGridView1.Rows[i].Cells["氏名"].Value = $" {pd.CrewName}";
                dataGridView1.Rows[i].Cells["乗船日"].Value = $" {((DateTime)pd.StartDate).ToString("yyyy/MM/dd(ddd)")}";

                if (pd.BeInWork)
                {
                    dataGridView1.Rows[i].Cells["ステータス"].Value = "勤務中";
                    dataGridView1.Rows[i].Cells["ステータス"].Style.ForeColor = Color.Red;
                }
                else
                {
                    dataGridView1.Rows[i].Cells["ステータス"].Value = "休憩中";
                    dataGridView1.Rows[i].Cells["ステータス"].Style.ForeColor = Color.Blue;
                }

                if (pd.StartWork != null)
                    dataGridView1.Rows[i].Cells["開始時間S"].Value = $" {((DateTime)pd.StartWork).ToString("yyyy/MM/dd HH:mm:ss")}";

                if (pd.StartWorkAcutual != null)
                    dataGridView1.Rows[i].Cells["開始時間A"].Value = $" {((DateTime)pd.StartWorkAcutual).ToString("yyyy/MM/dd HH:mm:ss")}";

                dataGridView1.Rows[i].Cells["勤務時間合計"].Value = ToHHMM(pd.WorkMinutes);
                dataGridView1.Rows[i].Cells["休息時間合計"].Value = ToHHMM(pd.RestMinutes);
                dataGridView1.Rows[i].Cells["勤務時間"].Value = ToHHMM(pd.WorkMinutesWeek);

                i++;
            }


            if (TargetDate == DateTime.Today)
            {
                label_Now.Text = $"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} 現在";
            }
            else
            {
                label_Now.Text = $"{TargetDate.ToString("yyyy/MM/dd")} 時点";
            }

            panel_SearchMessage.Visible = false;
        }

        private string ToHHMM(double minutes)
        {
            int hh = (int)(minutes / 60);
            int mm = (int)(minutes % 60);

            return $"{hh}:{mm.ToString("00")}";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
