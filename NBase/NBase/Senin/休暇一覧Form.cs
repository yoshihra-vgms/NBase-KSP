using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace Senin
{
    public partial class 休暇一覧Form : Form
    {
        private MsSeninFilter filter = null;
        private List<MsSenin> seninList = null;
        private List<SiKyuka> kyukaList = null;

        private Dictionary<int, int> totalList = new Dictionary<int, int>();
        private enum ENUM_INDEX { 年度休暇, 休暇, 有給休暇, 陸上休暇, 船内休日, 船内休暇 };

        private static 休暇一覧Form instance;

        public 休暇一覧Form()
        {
            InitializeComponent();
        }

        public static 休暇一覧Form Instance()
        {
            if (instance == null)
            {
                instance = new 休暇一覧Form();
            }

            return instance;
        }

        private void 休暇一覧FormForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }

        private void 休暇一覧Form_Load(object sender, EventArgs e)
        {
            InitComboBox年度();
            InitComboBox職名();
        }

        #region private void InitComboBox年度()
        private void InitComboBox年度()
        {
            int thisYear = DateTime.Now.Year;

            if (1 <= DateTime.Now.Month && DateTime.Now.Month <= 3)
            {
                thisYear--;
            }

            for (int i = 0; i < 10; i++)
            {
                comboBox年度.Items.Add(thisYear - i);
            }

            comboBox年度.SelectedItem = thisYear;
        }
        #endregion

        #region private void InitComboBox職名()
        private void InitComboBox職名()
        {
            comboBox職名.Items.Add(string.Empty);

            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                comboBox職名.Items.Add(s);
            }

            comboBox職名.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// 「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button検索_Click(object sender, EventArgs e)
        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }
        #endregion

        /// <summary>
        /// 「検索条件クリア」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttonクリア_Click(object sender, EventArgs e)
        private void buttonクリア_Click(object sender, EventArgs e)
        {
            Reset();
        }
        #endregion

        /// <summary>
        /// 「登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button登録_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                SiKyuka siKyuka = null;

                int msSeninId = (int)row.Cells[0].Value;

                var tmp = from obj in kyukaList
                          where obj.MsSeninID == msSeninId
                          select obj;
                if (tmp.Count<SiKyuka>() > 0)
                {
                    siKyuka = tmp.First<SiKyuka>();
                }
                else
                {
                    siKyuka = new SiKyuka();
                    siKyuka.MsSeninID = msSeninId;
                    siKyuka.Nendo = filter.Nendo;

                    kyukaList.Add(siKyuka);
                }

                siKyuka.YukyuKyuka = getInputValue((string)row.Cells[8].Value);
                siKyuka.RikujyoKyuka = getInputValue((string)row.Cells[9].Value);
                siKyuka.SennaiKyujitsu = getInputValue((string)row.Cells[10].Value);
                siKyuka.SennaiKyuka = getInputValue((string)row.Cells[11].Value);


            }

            bool ret = true;

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.SiKyuka_InsertOrUpdate(NBaseCommon.Common.LoginUser, kyukaList);
                }
            }, "登録処理中です...");

            progressDialog.ShowDialog();

            if (ret)
            {
                MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Search();
            }
            else
            {
                MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int getInputValue(string value)
        {
            int inputValue = -1;
            int.TryParse(value, out inputValue);
            return inputValue;
        }





        #region private void Reset()
        private void Reset()
        {
            comboBox年度.SelectedItem = DateTime.Now.Year;
            comboBox職名.SelectedIndex = 0;
            textBox氏名コード.Text = null;
            textBox氏名.Text = null;
        }
        #endregion

        /// <summary>
        /// 検索ロジックをコールする
        /// </summary>
        #region internal void Search()
        internal void Search()
        {
            filter = CreateMsSeninFilter();

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    seninList = serviceClient.BLC_船員検索(NBaseCommon.Common.LoginUser, filter);
                    kyukaList = serviceClient.SiKyuka_GetRecordsByNendo(NBaseCommon.Common.LoginUser, filter.Nendo);
                    
                        
                    totalList.Clear();
                    totalList.Add((int)ENUM_INDEX.年度休暇, 0);
                    totalList.Add((int)ENUM_INDEX.休暇, 0);
                    totalList.Add((int)ENUM_INDEX.有給休暇, 0);
                    totalList.Add((int)ENUM_INDEX.陸上休暇, 0);
                    totalList.Add((int)ENUM_INDEX.船内休日, 0);
                    totalList.Add((int)ENUM_INDEX.船内休暇, 0);
                }
            }, "データ取得中です...");

            progressDialog.ShowDialog();


            Cursor = Cursors.WaitCursor;

            SetRows(seninList);
            SetTotalDataGrid();

            Cursor = Cursors.Default;
        }
        #endregion

        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="koushus"></param>
        #region private void SetRows(List<MsSenin> seninList)
        private void SetRows(List<MsSenin> seninList)
        {
            dataGridView2.Rows.Clear();

            #region カラムの設定
            if (dataGridView2.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "船員ID";
                textColumn.Visible = false;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "氏名" + Environment.NewLine + "コード";
                textColumn.Width = 65;
                textColumn.ReadOnly = true;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "職名";
                textColumn.Width = 100;
                textColumn.ReadOnly = true;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "氏名";
                textColumn.Width = 100;
                textColumn.ReadOnly = true;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "入社年月日";
                textColumn.Width = 90;
                textColumn.ReadOnly = true;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "乗船日数";
                textColumn.Width = 60;
                textColumn.ReadOnly = true;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "年度" + Environment.NewLine + "休暇数";
                textColumn.Width = 70;
                textColumn.ReadOnly = true;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "休暇数";
                textColumn.Width = 70;
                textColumn.ReadOnly = true;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "有給" + Environment.NewLine + "休暇";
                textColumn.Width = 60;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "陸上" + Environment.NewLine + "休暇";
                textColumn.Width = 60;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "船内" + Environment.NewLine + "休日";
                textColumn.Width = 60;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "船内" + Environment.NewLine + "休暇";
                textColumn.Width = 60;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.Columns.Add(textColumn);

                //DataGridView免許免状RowColumn column = new DataGridView免許免状RowColumn();
                //column.Visible = false;
                //dataGridView2.Columns.Add(column);
            }
            #endregion

            int rowNo = 0;
            foreach (MsSenin row in seninList)
            {
                #region 情報を一覧にセットする

                int colNo = 0;
                object[] rowDatas = new object[12];

                rowDatas[colNo] = row.MsSeninID;
                colNo++;
                rowDatas[colNo] = int.Parse(row.ShimeiCode);
                colNo++;
                rowDatas[colNo] = SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, row.MsSiShokumeiID);
                colNo++;
                rowDatas[colNo] = row.Sei + " " + row.Mei;
                colNo++;

                // 入社年月日
                rowDatas[colNo] = (row.NyuushaDate == DateTime.MinValue ? "" : row.NyuushaDate.ToShortDateString());
                colNo++;

                // 乗船日数
                int 合計乗船 = 0;
                if (row.合計日数 != null && row.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船)))
                {
                    合計乗船 = row.合計日数[SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.乗船)];
                }
                rowDatas[colNo] = 合計乗船;
                colNo++;

                // 年度休暇数
                int 年度休暇 = 0;
                if (row.合計日数 != null && row.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.本年度休暇日数)))
                {
                    年度休暇 = row.合計日数[SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.本年度休暇日数)];
                }
                if (row.合計日数 != null && row.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.前年度休暇繰越日数)))
                {
                    年度休暇 += row.合計日数[SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser,MsSiShubetsu.SiShubetsu.前年度休暇繰越日数)];
                }
                totalList[(int)ENUM_INDEX.年度休暇] += 年度休暇;
                rowDatas[colNo] = 年度休暇;
                colNo++;

                // 有給休暇数
                int 合計休暇 = 0;
                int 有給休暇 = 0;
                int 陸上休暇 = 0;
                if (row.合計日数 != null && row.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser, MsSiShubetsu.SiShubetsu.有給休暇)))
                {
                    有給休暇 = row.合計日数[SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser, MsSiShubetsu.SiShubetsu.有給休暇)];
                }
                if (row.合計日数 != null && row.合計日数.ContainsKey(SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser, MsSiShubetsu.SiShubetsu.陸上休暇)))
                {
                    陸上休暇 += row.合計日数[SeninTableCache.instance().MsSiShubetsu_GetID(NBaseCommon.Common.LoginUser, MsSiShubetsu.SiShubetsu.陸上休暇)];
                }
                合計休暇 = 有給休暇 + 陸上休暇;
                totalList[(int)ENUM_INDEX.休暇] += 合計休暇;
                rowDatas[colNo] = 合計休暇;
                colNo++;


                SiKyuka siKyuka = null;
                var tmp = from obj in kyukaList
                              where obj.MsSeninID == row.MsSeninID
                              select obj;
                if (tmp.Count<SiKyuka>() == 0)
                {
                    siKyuka = new SiKyuka();
                    siKyuka.MsSeninID = row.MsSeninID;
                    siKyuka.Nendo = filter.Nendo;
                    kyukaList.Add(siKyuka);
                }
                else
                {
                    siKyuka = tmp.First<SiKyuka>();
                }

                // 有給休暇
                totalList[(int)ENUM_INDEX.有給休暇] += siKyuka.IsNew() ? 0 : siKyuka.YukyuKyuka;
                rowDatas[colNo] = siKyuka.IsNew() ? "" : siKyuka.YukyuKyuka.ToString();
                colNo++;

                // 陸上休暇
                totalList[(int)ENUM_INDEX.陸上休暇] += siKyuka.IsNew() ? 0 : siKyuka.RikujyoKyuka;
                rowDatas[colNo] = siKyuka.IsNew() ? "" : siKyuka.RikujyoKyuka.ToString();
                colNo++;

                // 船内休日
                totalList[(int)ENUM_INDEX.船内休日] += siKyuka.IsNew() ? 0 : siKyuka.SennaiKyujitsu;
                rowDatas[colNo] = siKyuka.IsNew() ? "" : siKyuka.SennaiKyujitsu.ToString();
                colNo++;

                // 船内休暇
                totalList[(int)ENUM_INDEX.船内休暇] += siKyuka.IsNew() ? 0 : siKyuka.SennaiKyuka;
                rowDatas[colNo] = siKyuka.IsNew() ? "" : siKyuka.SennaiKyuka.ToString();
                colNo++;

                dataGridView2.Rows.Add(rowDatas);

                rowNo++;

                #endregion
            }

            // 選択行はなしにしておく
            dataGridView2.CurrentCell = null;
        }
        #endregion
        #region private void SetTotalDataGrid()
        private void SetTotalDataGrid()
        {
            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "合計年度" + Environment.NewLine + "休暇数";
                textColumn.Width = 80;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "合計" + Environment.NewLine + "休暇数";
                textColumn.Width = 80;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "合計" + Environment.NewLine + "有給休暇";
                textColumn.Width = 80;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "合計" + Environment.NewLine + "陸上休暇";
                textColumn.Width = 80;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "合計" + Environment.NewLine + "船内休日";
                textColumn.Width = 80;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "合計" + Environment.NewLine + "船内休暇";
                textColumn.Width = 80;
                textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                textColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns.Add(textColumn);
            }
            #endregion


            dataGridView1.Rows.Clear();


            object[] rowDatas = new object[11];

            rowDatas[(int)ENUM_INDEX.年度休暇] = totalList[(int)ENUM_INDEX.年度休暇];
            rowDatas[(int)ENUM_INDEX.休暇] = totalList[(int)ENUM_INDEX.休暇];
            rowDatas[(int)ENUM_INDEX.有給休暇] = totalList[(int)ENUM_INDEX.有給休暇];
            rowDatas[(int)ENUM_INDEX.陸上休暇] = totalList[(int)ENUM_INDEX.陸上休暇];
            rowDatas[(int)ENUM_INDEX.船内休日] = totalList[(int)ENUM_INDEX.船内休日];
            rowDatas[(int)ENUM_INDEX.船内休暇] = totalList[(int)ENUM_INDEX.船内休暇];

            dataGridView1.Rows.Add(rowDatas);
        }
        #endregion


        /// <summary>
        /// 検索条件
        /// </summary>
        /// <returns></returns>
        #region private MsSeninFilter CreateMsSeninFilter()
        private MsSeninFilter CreateMsSeninFilter()
        {
            MsSeninFilter filter = new MsSeninFilter();

            filter.IncludeKyuka = true;
            filter.Nendo = ((int)comboBox年度.SelectedItem).ToString();

            if (comboBox職名.SelectedItem is MsSiShokumei)
            {
                filter.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }

            if (textBox氏名コード.Text.Length > 0)
            {
                filter.ShimeiCode = textBox氏名コード.Text;
            }

            if (textBox氏名.Text.Length > 0)
            {
                filter.Name = textBox氏名.Text;
            }

            filter.joinSiCard = MsSeninFilter.JoinSiCard.LEFT_JOIN;
            //filter.種別無し = false;
            filter.種別無し = true;
            foreach (MsSiShubetsu s in SeninTableCache.instance().GetMsSiShubetsuList(NBaseCommon.Common.LoginUser))
            {
                filter.MsSiShubetsuIDs.Add(s.MsSiShubetsuID);
            }
            return filter;
        }
        #endregion



        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;

            if (e.ColumnIndex > 7)
            {
                if (gridView.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn)
                {
                    SendKeys.Send("{F2}");
                }
            }
        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;

            if (e.ColumnIndex > 7)
            {
                if (gridView.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn)
                {
                    SendKeys.Send("{F2}");
                }
            }
            else
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            //新しい行のセルでなく、セルの内容が変更されている時だけ検証する
            if (e.RowIndex == dgv.NewRowIndex || !dgv.IsCurrentCellDirty)
            {
                return;
            }
            int inputValue = 0;
            if (e.FormattedValue.ToString() != "" && int.TryParse(e.FormattedValue.ToString(), out inputValue) == false)
            {
                //行にエラーテキストを設定
                dgv.Rows[e.RowIndex].ErrorText = "数値を入力してください。";
                
                //入力した値をキャンセルして元に戻すには、次のようにする
                dgv.CancelEdit();
                
                //キャンセルする
                e.Cancel = true;
            }
            if (inputValue > 999)
            {
                //行にエラーテキストを設定
                dgv.Rows[e.RowIndex].ErrorText = "半角数字３桁を入力してください。";

                //入力した値をキャンセルして元に戻すには、次のようにする
                dgv.CancelEdit();

                //キャンセルする
                e.Cancel = true;
            }

            int currentValue = 0;
            if (dgv.CurrentCell.Value != null && dgv.CurrentCell.Value.ToString() != "")
            {
                currentValue = int.Parse(dgv.CurrentCell.Value.ToString());
            }

            int totalValue = (int)dataGridView1.Rows[0].Cells[e.ColumnIndex - 6].Value;
            totalValue = totalValue - currentValue + inputValue;
            dataGridView1.Rows[0].Cells[e.ColumnIndex - 6].Value = totalValue;

        }

        private void dataGridView2_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            ////エラーテキストを消す
            //dgv.Rows[e.RowIndex].ErrorText = null;
        }
    }
}
