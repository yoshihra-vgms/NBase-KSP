using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Senin
{
    public partial class tek手当詳細Form : Form
    {
        private bool isEdit = false;

        public SiAllowance siAllowance = null;
        public List<SiAllowanceDetail> siAllowanceDetails = null;

        private List<SiCard> cards = null;
        private int captain_senin_id = -1;

        private Dictionary<int, List<int>> msSiAllowanceVessel = new Dictionary<int, List<int>>();


        private int CheckCellNo = 0;
        private int DaysCellNo = 3;
        private int AllowanceCellNo = 4;
        private int ObjCellNo = 5;

        private class YearMonth
        {
            public string ym { set; get; }
            public DateTime from { set; get; }
            public DateTime to { set; get; }

            public override string ToString()
            {
                return from.ToString("yyyy年MM月");
            }
            public string Range()
            {
                return $"{from.ToString("yyyy年MM月dd日")}～{to.ToString("yyyy年MM月dd日")}";
            }
            public string Value()
            {
                return ym;
            }

            public YearMonth(DateTime d)
            {
                ym = d.ToString("yyyyMM");
                from = DateTimeUtils.ToFromMonth(d);
                to = DateTimeUtils.ToToMonth(d).AddDays(-1);
            }
        }


        public tek手当詳細Form() : this(null)
        {
        }

        public tek手当詳細Form(SiAllowance allowance)
        {
            siAllowance = allowance;
            InitializeComponent();
        }

        private void tek手当詳細Form_Load(object sender, EventArgs e)
        {
            InitComboBox手当名();
            InitComboBox作業期間();

            if (siAllowance != null)
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    siAllowanceDetails = serviceClient.SiAllowanceDetail_GetRecords(NBaseCommon.Common.LoginUser, siAllowance.SiAllowanceID);
                }

                foreach(MsSiAllowance item in comboBox手当名.Items)
                {
                    if (item.MsSiAllowanceID == siAllowance.MsSiAllowanceID)
                    {
                        comboBox手当名.SelectedItem = item;
                        break;
                    }
                }
                foreach (MsVessel item in comboBox船名.Items)
                {
                    if (item.MsVesselID == siAllowance.MsVesselID)
                    {
                        comboBox船名.SelectedItem = item;
                        break;
                    }
                }
                foreach(YearMonth ym in comboBox作業期間.Items)
                {
                    if (ym.Value() == siAllowance.YearMonth)
                    {
                        comboBox作業期間.SelectedItem = ym;
                        break;
                    }
                }

                var captainCard = cards.Where(o => o.MsSeninID == siAllowance.CaptainSeninID).FirstOrDefault();
                if (captainCard != null)
                {
                    textBox船長.Text = captainCard.SeninName;
                }

                textBox作業内容.Text = siAllowance.Contents;
                textBox数量.Text = siAllowance.Quantity.ToString();
                textBox金額.Text = NBaseCommon.Common.金額出力(siAllowance.Allowance);

                textBox作業責任者.Text = siAllowance.PersonInCharge;


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    SiCard c = row.Cells[ObjCellNo].Value as SiCard;

                    var detail = siAllowanceDetails.Where(o => o.SiCardID == c.SiCardID).FirstOrDefault();
                    if (detail != null)
                    {
                        row.Cells[CheckCellNo].Value = detail.IsTarget == 1 ? true : false;
                        row.Cells[AllowanceCellNo].Value = NBaseCommon.Common.金額出力(detail.Allowance);
                    }
                }

                button削除.Enabled = true;
            }
            else
            {
                button削除.Enabled = false;
            }

            isEdit = false;
        }

        private void button出力_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                bool ret = true;
                if (isEdit)
                {
                    ret = Save();
                }
                if (ret && siAllowance != null && siAllowance.IsNew() == false)
                {
                    isEdit = false;
                    Output();
                }
            }
        }


        private void button登録_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                if (Save())
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Dispose();
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                siAllowance.DeleteFlag = 1;
                siAllowanceDetails.ForEach(o => o.DeleteFlag = 1);
                if (Save())
                {
                    MessageBox.Show(this, "削除しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Dispose();
                }
                else
                {
                    MessageBox.Show(this, "削除に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button分割_Click(object sender, EventArgs e)
        {
            if (StringUtils.Empty(textBox金額.Text))
                return;

            if ((comboBox手当名.SelectedItem is MsSiAllowance) == false)
                return;

            var distributionFlag = (comboBox手当名.SelectedItem as MsSiAllowance).DistributionFlag;

            int denominator = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[CheckCellNo].Value))
                {
                    if (distributionFlag == 0)
                    {
                        denominator++;
                    }
                    else
                    {
                        denominator += Convert.ToInt32(row.Cells[DaysCellNo].Value);
                    }
                }
            }

            var totalAllowance = NBaseCommon.Common.金額表示を数値へ変換(textBox金額.Text);

            if (denominator > 0)
            {
                var unitPrice = Convert.ToInt32(Math.Floor(totalAllowance / denominator));  // 切り捨て
                var rest = totalAllowance % denominator;

                var allowance = 0;
                var total = 0;


                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[CheckCellNo].Value))
                    {
                        if (distributionFlag == 0)
                        {
                            allowance = unitPrice;
                        }
                        else
                        {
                            var days = Convert.ToInt32(row.Cells[DaysCellNo].Value);
                            allowance = (unitPrice * days); ;
                        }
                        total += allowance;

                        if ((total + rest) == totalAllowance)
                        {
                            row.Cells[AllowanceCellNo].Value = NBaseCommon.Common.金額出力(allowance + rest);
                        }
                        else
                        {
                            row.Cells[AllowanceCellNo].Value = NBaseCommon.Common.金額出力(allowance);
                        }
                    }
                    else
                    {
                        row.Cells[AllowanceCellNo].Value = NBaseCommon.Common.金額出力(0);
                    }
                }
            }
        }




        private void InitComboBox手当名()
        {
            foreach (MsSiAllowance a in SeninTableCache.instance().GetMsSiAllowanceList(NBaseCommon.Common.LoginUser))
            {
                comboBox手当名.Items.Add(a);

                List<int> vesselIds = new List<int>();
                var strVesselIds = a.TargetVessel.Split(',');
                foreach(string id in strVesselIds)
                {
                    vesselIds.Add(int.Parse(id));
                }
                msSiAllowanceVessel.Add(a.MsSiAllowanceID, vesselIds);
            }
            if (comboBox手当名.Items.Count > 0)
                comboBox手当名.SelectedIndex = 0;
        }


        private void InitComboBox作業期間()
        {
            DateTime d = DateTime.Today;
            for (int i = 0; i > -6; i--)
            {
                var ym = new YearMonth(d.AddMonths(i));
                comboBox作業期間.Items.Add(ym);
            }
            comboBox作業期間.SelectedIndex = 0;
        }



        private void comboBox手当名_SelectedIndexChanged(object sender, EventArgs e)
        {
            var msSiAllowance = comboBox手当名.SelectedItem as MsSiAllowance;

            textBox作業内容.Text = msSiAllowance.Contents;
            textBox数量.Text = "1";
            textBox金額.Text = NBaseCommon.Common.金額出力(msSiAllowance.Allowance);


            comboBox船名.Items.Clear();
            foreach (MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            {
                if (msSiAllowanceVessel[msSiAllowance.MsSiAllowanceID].Contains(v.MsVesselID))
                    comboBox船名.Items.Add(v);
            }
            comboBox船名.SelectedIndex = 0;

            isEdit = true;
        }

        private void comboBox船名_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind();

            isEdit = true;
        }

        private void comboBox作業期間_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ym = comboBox作業期間.SelectedItem as YearMonth;
            label作業期間.Text = ym.Range();

            Bind();

            isEdit = true;
        }

        private void textBox作業内容_TextChanged(object sender, EventArgs e)
        {
            isEdit = true;
        }

        private void textBox数量_TextChanged(object sender, EventArgs e)
        {
            int quantity = 0;
            if (int.TryParse((sender as TextBox).Text, out quantity) == false)
            {
                MessageBox.Show("数量は正の整数を入力してください");
                return;
            }
            var msSiAllowance = comboBox手当名.SelectedItem as MsSiAllowance;
            textBox金額.Text = NBaseCommon.Common.金額出力(msSiAllowance.Allowance * quantity);

            isEdit = true;
        }


        private void Bind()
        {
            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "　";
                checkBoxColumn.Width = 50;
                dataGridView1.Columns.Add(checkBoxColumn);

                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "職名";
                textColumn.Width = 90;
                textColumn.ReadOnly = true;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "氏名";
                textColumn.Width = 125;
                textColumn.ReadOnly = true;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "日数";
                textColumn.Width = 45;
                textColumn.ReadOnly = true;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "金額";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "Obj";
                textColumn.Visible = false;
                dataGridView1.Columns.Add(textColumn);


                dataGridView1.Columns[CheckCellNo].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[DaysCellNo].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[AllowanceCellNo].SortMode = DataGridViewColumnSortMode.NotSortable;

                dataGridView1.Columns[DaysCellNo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[AllowanceCellNo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            #endregion

            dataGridView1.Rows.Clear();



            var vessel = (MsVessel)null;
            var ym = (YearMonth)null;

            if (comboBox船名.SelectedItem is MsVessel)
                vessel = comboBox船名.SelectedItem as MsVessel;

            if (comboBox作業期間.SelectedItem is YearMonth)
                ym = comboBox作業期間.SelectedItem as YearMonth;

            if (vessel == null || ym == null)
            {
                return;
            }

            var allowance = comboBox手当名.SelectedItem as MsSiAllowance;

            var cardFilter = new SiCardFilter();
            cardFilter.MsVesselIDs.Add(vessel.MsVesselID);
            cardFilter.MsSiShubetsuIDs.Add(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser, MsSiShubetsu.SiShubetsu.乗船));
            cardFilter.Start = ym.from;
            //cardFilter.End = ym.to;
            cardFilter.End = ym.to.AddDays(2); // 月末日のプロモーションを考慮する

            var tmpcards = new List<SiCard>();
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                tmpcards = serviceClient.SiCard_GetRecordsByFilter(NBaseCommon.Common.LoginUser, cardFilter);

                tmpcards.ForEach(o => { if (o.SiLinkShokumeiCards != null && o.SiLinkShokumeiCards.Count() > 0) o.CardMsSiShokumeiID = o.SiLinkShokumeiCards[0].MsSiShokumeiID; });

                cards = tmpcards.Where(o => o.StartDate <= ym.to).ToList(); // 実際に対象にしたいのは、開始日が月末日までの乗船
            }

            var captainId = SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser).Where(o => o.Name == "船長").First().MsSiShokumeiID;
            var captainCards = cards.Where(o => o.CardMsSiShokumeiID == captainId && (o.EndDate == DateTime.MinValue || o.EndDate > ym.to));
            if (captainCards.Count() == 1)
            {
                captain_senin_id = captainCards.First().MsSeninID;
                textBox船長.Text = captainCards.First().SeninName;
            }
            else if (captainCards.Count() > 1)
            {
                var sorted = captainCards.OrderBy(o => o.StartDate);
                if (sorted.Last().EndDate == DateTime.MinValue || sorted.Last().EndDate > ym.to)
                {
                    captain_senin_id = sorted.Last().MsSeninID;
                    textBox船長.Text = sorted.Last().SeninName;
                }
                else
                {
                    captain_senin_id = sorted.First().MsSeninID;
                    textBox船長.Text = sorted.First().SeninName;
                }
            }

            var picCards = cards.Where(o => o.CardMsSiShokumeiID == allowance.MsSiShokumeiID);
            if (picCards.Count() > 0)
            {
                var sorted = picCards.OrderBy(o => o.StartDate);
                textBox作業責任者.Text = sorted.Last().SeninName;
            }


            List<int> targetDepartments = new List<int>();
            if (allowance.Department == 0) // 全員
            {
                targetDepartments.Add(0); // Officer
                targetDepartments.Add(2); // 甲板部

                targetDepartments.Add(1); // 機関部
                targetDepartments.Add(3); // 機関部(部員)

                targetDepartments.Add(4); // 司厨部
            }
            if (allowance.Department == 1) // 甲板部
            {
                targetDepartments.Add(0); // Officer
                targetDepartments.Add(2); // 甲板部
            }
            if (allowance.Department == 2) // 機関部
            {
                targetDepartments.Add(1); // 機関部
                targetDepartments.Add(3); // 機関部(部員)
            }


            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                if (targetDepartments.Contains(s.Department))
                {
                    var rankCards = cards.Where(o => o.CardMsSiShokumeiID == s.MsSiShokumeiID);
                    foreach(SiCard c in rankCards)
                    {
                        int colNo = 0;
                        object[] rowDatas = new object[6];
                        rowDatas[colNo] = false;
                        colNo++;
                        rowDatas[colNo] = s.NameAbbr;
                        colNo++;
                        rowDatas[colNo] = c.SeninName;


                        int days = 0;
                        DateTime start = c.StartDate;
                        DateTime end = c.EndDate;

                        if (start < ym.from)
                        {
                            start = ym.from;
                        }

                        if (end == DateTime.MinValue || end > ym.to)
                        {
                            if (ym.to > DateTime.Today)
                                end = DateTime.Today;
                            else
                                end = ym.to;

                        }
                        else if (end.ToString("yyyyMM") == ym.to.ToString("yyyyMM")) // 下船 or プロモーション
                        {
                            var checkCards = tmpcards.Where(o => o.SiCardID != c.SiCardID && o.MsSeninID == c.MsSeninID);
                            if (checkCards == null|| checkCards.Count() == 0)
                            {
                                end = end.AddDays(-1); // 下船日は除く
                            }
                            else
                            {
                                checkCards = checkCards.Where(o => DateTimeUtils.ToFrom(o.StartDate) == DateTimeUtils.ToFrom(end.AddDays(1)));
                                if (checkCards == null || checkCards.Count() == 0)
                                {
                                    end = end.AddDays(-1); // 下船日は除く
                                }
                            }
                        }

                        days = DateTimeUtils.日数計算(start, end);

                        colNo++;
                        rowDatas[colNo] = days;


                        colNo++;
                        rowDatas[colNo] = NBaseCommon.Common.金額出力(0);
                        colNo++;
                        rowDatas[colNo] = c;

                        dataGridView1.Rows.Add(rowDatas);
                    }
                }
            }
            dataGridView1.CurrentCell = null;
        }




        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == AllowanceCellNo)
            {
                decimal val = NBaseCommon.Common.金額表示を数値へ変換(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = ((int)val).ToString();
                SendKeys.Send("{F2}");
            }
            else if (e.ColumnIndex != 0)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            if (e.RowIndex == dgv.NewRowIndex || !dgv.IsCurrentCellDirty)
            {
                return;
            }

            int inputValue = 0;
            if (e.ColumnIndex == AllowanceCellNo)
            {
                if (e.FormattedValue.ToString() != "" && int.TryParse(e.FormattedValue.ToString(), out inputValue) == false)
                {
                    //行にエラーテキストを設定
                    dgv.Rows[e.RowIndex].ErrorText = "数値を入力してください。";

                    //入力した値をキャンセルして元に戻すには、次のようにする
                    dgv.CancelEdit();

                    //キャンセルする
                    e.Cancel = true;
                }
                if (inputValue > 99999)
                {
                    //行にエラーテキストを設定
                    dgv.Rows[e.RowIndex].ErrorText = "半角数字5桁を入力してください。";

                    //入力した値をキャンセルして元に戻すには、次のようにする
                    dgv.CancelEdit();

                    //キャンセルする
                    e.Cancel = true;
                }
            }
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == AllowanceCellNo)
            {
                decimal val = NBaseCommon.Common.金額表示を数値へ変換(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = NBaseCommon.Common.金額出力(val);
            }
            支給額算出();
        }

        private void DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCellAddress.X == 0 &&
                dataGridView1.IsCurrentCellDirty)
            {
                //コミットする
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            支給額算出();
        }


        private void 支給額算出()
        {
            var total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[CheckCellNo].Value))
                {
                    var val = NBaseCommon.Common.金額表示を数値へ変換(row.Cells[AllowanceCellNo].Value.ToString());
                    total += (int)val;
                }
            }
            textBox支給額.Text = NBaseCommon.Common.金額出力(total);

            isEdit = true;
        }





        private bool Save()
        {
            FillInstance();
            return InsertOrUpdate();
        }

        /// <summary>
        /// 入力値の確認
        /// </summary>
        /// <returns></returns>
        #region private bool ValidateFields()
        private bool ValidateFields()
        {
            if (!(comboBox手当名.SelectedItem is MsSiAllowance))
            {
                comboBox手当名.BackColor = Color.Pink;
                MessageBox.Show("手当を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox手当名.BackColor = Color.White;
                return false;
            }
            else if (!(comboBox船名.SelectedItem is MsVessel))
            {
                comboBox船名.BackColor = Color.Pink;
                MessageBox.Show("船名を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox船名.BackColor = Color.White;
                return false;
            }
            else if (!(comboBox作業期間.SelectedItem is YearMonth))
            {
                comboBox作業期間.BackColor = Color.Pink;
                MessageBox.Show("作業期間を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox作業期間.BackColor = Color.White;
                return false;
            }



            if (textBox金額.Text != textBox支給額.Text)
            {
                Color orgColor = textBox金額.BackColor;

                textBox金額.BackColor = Color.Pink;
                textBox支給額.BackColor = Color.Pink;
                MessageBox.Show("金額と支給額が異なっています。" + System.Environment.NewLine + "支給額を確認ください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox金額.BackColor = orgColor;
                textBox支給額.BackColor = orgColor;
                return false;
            }



            return true;
        }
        #endregion

        /// <summary>
        /// 入力値を取得し、手当情報にセットする
        /// </summary>
        #region FillInstance()
        private void FillInstance()
        {
            if (siAllowance == null)
            {
                siAllowance = new SiAllowance();
            }

            siAllowance.MsSiAllowanceID = (comboBox手当名.SelectedItem as MsSiAllowance).MsSiAllowanceID;
            siAllowance.MsVesselID = (comboBox船名.SelectedItem as MsVessel).MsVesselID;
            siAllowance.CaptainSeninID = captain_senin_id;
            siAllowance.YearMonth = (comboBox作業期間.SelectedItem as YearMonth).Value();
            siAllowance.Contents = textBox作業内容.Text;

            int val = 0;
            int.TryParse(textBox数量.Text, out val);
            siAllowance.Quantity = val;

            siAllowance.Allowance = (int)NBaseCommon.Common.金額表示を数値へ変換(textBox金額.Text);

            siAllowance.PersonInCharge = textBox作業責任者.Text;


            siAllowance.VesselID = siAllowance.MsVesselID; // 同期対象は、対象の船のみ


            if (siAllowanceDetails == null)
            {
                siAllowanceDetails = new List<SiAllowanceDetail>();
            }
            else
            {
                foreach (SiAllowanceDetail detail in siAllowanceDetails)
                {
                    detail.DeleteFlag = 1;
                }
            }


            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                SiCard c = row.Cells[ObjCellNo].Value as SiCard;

                SiAllowanceDetail detail = null;
                if (siAllowanceDetails.Any(o => o.SiCardID == c.SiCardID))
                {
                    detail = siAllowanceDetails.Where(o => o.SiCardID == c.SiCardID).FirstOrDefault();
                    detail.DeleteFlag = 0;
                }
                else
                {
                    detail = new SiAllowanceDetail();
                    siAllowanceDetails.Add(detail);

                    detail.MsSeninID = c.MsSeninID;
                    detail.MsSiShokumeiID = c.CardMsSiShokumeiID;
                    detail.SiCardID = c.SiCardID;
                }
                var allowanceVal = NBaseCommon.Common.金額表示を数値へ変換(row.Cells[AllowanceCellNo].Value.ToString());
                detail.Allowance = (int)allowanceVal;
                detail.IsTarget = Convert.ToBoolean(row.Cells[CheckCellNo].Value) ? 1 : 0;

                detail.VesselID = siAllowance.MsVesselID; // 同期対象は、対象の船のみ
            }
        }
        #endregion


        #region private bool InsertOrUpdate()
        private bool InsertOrUpdate()
        {
            bool result = true;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                string id = siAllowance.SiAllowanceID;
                if (siAllowance.IsNew())
                {
                    id = System.Guid.NewGuid().ToString();
                }
                result = serviceClient.SiAllowance_InsertOrUpdate(NBaseCommon.Common.LoginUser, id, siAllowance, siAllowanceDetails);

                if (result && siAllowance.DeleteFlag != 1)
                {
                    siAllowance = serviceClient.SiAllowance_GetRecord(NBaseCommon.Common.LoginUser, id);
                    siAllowanceDetails = serviceClient.SiAllowanceDetail_GetRecords(NBaseCommon.Common.LoginUser, id);
                }
            }

            return result;
        }

        #endregion



        private void Output()
        {
            saveFileDialog1.FileName = "手当支給依頼書.xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                byte[] result = null;

                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        result = serviceClient.BLC_Excel_手当支給依頼書出力(NBaseCommon.Common.LoginUser, siAllowance, siAllowanceDetails);
                    }
                }, "データ取得中です...");
                progressDialog.ShowDialog();

                //--------------------------------
                if (result == null)
                {
                    #region エラーメッセージ表示

                    MessageBox.Show("手当支給依頼書の出力に失敗しました"
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


    }
}
