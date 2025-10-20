using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseUtil;
using NBaseData.DAC;
using System.IO;
using SyncClient;

namespace NBaseHonsen.Dousei
{
    public partial class 動静報告一覧Form : Form
    {
        private static 動静報告一覧Form instance;

        private List<MsVessel> vesselList = null;
        private List<MsBasho> bashoList = null;
        private List<MsDjTenkou> tenkouList = null;
        private List<MsDjKazamuki> kazamukiList = null;
        private List<MsDjSentaisetsubi> sentaisetsubiList = null;
        private List<MsDjKenkoujyoutai> kenkoujyoutaiList = null;

        private List<DjDouseiHoukoku> douseiHoukokuList = null;

        public 動静報告一覧Form()
        {
            InitializeComponent();
        }

        public static 動静報告一覧Form Instance()
        {
            if (instance == null)
            {
                instance = new 動静報告一覧Form();
            }

            return instance;
        }

        private void 動静報告一覧Form_Load(object sender, EventArgs e)
        {
            dateTimePicker報告日To.Value = DateTime.Today;
            dateTimePicker報告日From.Value = dateTimePicker報告日To.Value.AddDays(-14);
            Search();
            button検索.Focus();
        }

        private void 動静報告一覧Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            instance = null;
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 「検索」クリック
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
        /// データを検索し、一覧にセットする
        /// </summary>
        private void Search()
        {
            DateTime houkokuDateFrom = dateTimePicker報告日From.Value;
            DateTime houkokuDateTo = dateTimePicker報告日To.Value;

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                bashoList = MsBasho.GetRecordsBy港(同期Client.LOGIN_USER);
                tenkouList = MsDjTenkou.GetRecords(同期Client.LOGIN_USER);
                kazamukiList = MsDjKazamuki.GetRecords(同期Client.LOGIN_USER);
                sentaisetsubiList = MsDjSentaisetsubi.GetRecords(同期Client.LOGIN_USER);
                kenkoujyoutaiList = MsDjKenkoujyoutai.GetRecords(同期Client.LOGIN_USER);

                douseiHoukokuList = DjDouseiHoukoku.GetRecordsByHoukokuDate(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID, houkokuDateFrom, houkokuDateTo);
            }, "データ取得中です...");
            progressDialog.ShowDialog();

            SetRows();
        }


        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="koushus"></param>
        private void SetRows()
        {
            Cursor = Cursors.WaitCursor;

            dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, 12);
            dataGridView1.Rows.Clear();

            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "報告日時";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "現在地";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "出港地";
                dataGridView1.Columns.Add(textColumn);
 
                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "出港日時";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "仕向地";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "入港" + System.Environment.NewLine + "予定日時";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "天候";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "風向";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "風速" + System.Environment.NewLine + "(m/s)";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "波高" + System.Environment.NewLine + "(m)";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "うねり" + System.Environment.NewLine + "(m)";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "視程" + System.Environment.NewLine + "(mile)";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "針路";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "速力" + System.Environment.NewLine + "(knot)";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "船体" + System.Environment.NewLine + "設備状況";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "乗務員" + System.Environment.NewLine + "健康状態";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "乗組員数";
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "備考";
                dataGridView1.Columns.Add(textColumn);

                DataGridView動静報告RowColumn column = new DataGridView動静報告RowColumn();
                column.Visible = false;
                dataGridView1.Columns.Add(column);

                int colIdx = 0;
                dataGridView1.Columns[colIdx].Width = 90;    //報告日時
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 130;   //現在地
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 130;   //出港地
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 90;    //出港日時
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 130;   //仕向地
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 90;    //入港予定日時
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 65;    //天候
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 65;    //風向
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 65;    //風速
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 65;   //波高
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 65;   //うねり
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 65;   //視程
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 65;   //針路
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 65;   //速力
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 80;   //船体設備状況
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 80;   //乗組員健康状態
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 60;   //乗組員数
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
                dataGridView1.Columns[colIdx].Width = 200;  //備考
                dataGridView1.Columns[colIdx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[colIdx].SortMode = DataGridViewColumnSortMode.NotSortable;
                colIdx++;
            }
            #endregion

            int rowNo = 0;
            foreach (DjDouseiHoukoku houkoku in douseiHoukokuList)
            {
                #region 情報を一覧にセットする

                int colNo = 0;
                object[] rowDatas = new object[19];
                rowDatas[colNo] = GetDateStr(houkoku.HoukokuDate);
                colNo++;
                rowDatas[colNo] = houkoku.CurrentPlace;
                colNo++;
                rowDatas[colNo] = GetPort(houkoku.LeavePortID);
                colNo++;
                rowDatas[colNo] = GetDateStr(houkoku.LeaveDate);
                colNo++;
                rowDatas[colNo] = GetPort(houkoku.DestinationPortID);
                colNo++;
                rowDatas[colNo] = GetDateStr(houkoku.ArrivalDate);
                colNo++;
                rowDatas[colNo] = GetTenkou(houkoku.MsDjTenkouID);
                colNo++;
                rowDatas[colNo] = GetKazamuki(houkoku.MsDjKazamukiID);
                colNo++;
                rowDatas[colNo] = houkoku.Fusoku;
                colNo++;
                rowDatas[colNo] = houkoku.Nami;
                colNo++;
                rowDatas[colNo] = houkoku.Uneri;
                colNo++;
                rowDatas[colNo] = houkoku.Sitei;
                colNo++;
                rowDatas[colNo] = houkoku.Sinro;
                colNo++;
                rowDatas[colNo] = houkoku.Sokuryoku;
                colNo++;
                rowDatas[colNo] = GetSentaisetsubi(houkoku.MsDjSentaisetsubiID);
                colNo++;
                rowDatas[colNo] = GetKenkoujyoutai(houkoku.MsDjKenkoujyoutaiID);
                colNo++;
                rowDatas[colNo] = houkoku.Norikumiinsu;
                colNo++;
                rowDatas[colNo] = houkoku.Bikou;
                colNo++;
                rowDatas[colNo] = houkoku;

                dataGridView1.Rows.Add(rowDatas);

                rowNo++;

                #endregion
            }

            Cursor = Cursors.Default;

        }

        private string GetDateStr(DateTime date)
        {
            if (date == DateTime.MinValue)
                return "";

            return date.ToString("MM/dd HH:mm");
        }

        private string GetPort(string portId)
        {
            if (portId == null || portId.Length == 0)
                return "";

            foreach (MsBasho basho in bashoList)
            {
                if (basho.MsBashoId == portId)
                    return basho.BashoName;
            }
            return "";
        }

        private string GetTenkou(string tenkouId)
        {
            if (tenkouId == null || tenkouId.Length == 0)
                return "";

            foreach (MsDjTenkou tenkou in tenkouList)
            {
                if (tenkou.MsDjTenkouId == tenkouId)
                    return tenkou.Tenkou;
            }
            return "";
        }

        private string GetKazamuki(string kazamukiId)
        {
            if (kazamukiId == null || kazamukiId.Length == 0)
                return "";

            foreach (MsDjKazamuki kazamuki in kazamukiList)
            {
                if (kazamuki.MsDjKazamukiId == kazamukiId)
                    return kazamuki.Kazamuki;
            }
            return "";
        }

        private string GetSentaisetsubi(string sentaisetsubiId)
        {
            if (sentaisetsubiId == null || sentaisetsubiId.Length == 0)
                return "";

            foreach (MsDjSentaisetsubi sentaisetsubi in sentaisetsubiList)
            {
                if (sentaisetsubi.MsDjSentaisetsubiId == sentaisetsubiId)
                    return sentaisetsubi.Sentaisetsubi;
            }
            return "";
        }

        private string GetKenkoujyoutai(string kenkoujyoutaiId)
        {
            if (kenkoujyoutaiId == null || kenkoujyoutaiId.Length == 0)
                return "";

            foreach (MsDjKenkoujyoutai kenkoujyoutai in kenkoujyoutaiList)
            {
                if (kenkoujyoutai.MsDjKenkoujyoutaiId == kenkoujyoutaiId)
                    return kenkoujyoutai.Kenkoujyoutai;
            }
            return "";
        }

        #region 内部クラス
        public class DataGridView動静報告RowColumn : DataGridViewTextBoxColumn
        {
            //コンストラクタ
            public DataGridView動静報告RowColumn()
            {
                this.CellTemplate = new DataGridView動静報告RowCell();
            }

            //CellTemplateの取得と設定
            public override DataGridViewCell CellTemplate
            {
                get
                {
                    return base.CellTemplate;
                }
                set
                {
                    //DataGridViewProgressBarCell以外はホストしない
                    if (!(value is DataGridView動静報告RowCell))
                    {
                        throw new InvalidCastException(
                            "DataGridViewProgressBarCellオブジェクトを" +
                            "指定してください。");
                    }
                    base.CellTemplate = value;
                }
            }

            /// <summary>
            /// ProgressBarの最大値
            /// </summary>
            public int Maximum
            {
                get
                {
                    return ((DataGridView動静報告RowCell)this.CellTemplate).Maximum;
                }
                set
                {
                    if (this.Maximum == value)
                        return;
                    //セルテンプレートの値を変更する
                    ((DataGridView動静報告RowCell)this.CellTemplate).Maximum =
                        value;
                    //DataGridViewにすでに追加されているセルの値を変更する
                    if (this.DataGridView == null)
                        return;
                    int rowCount = this.DataGridView.RowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataGridViewRow r = this.DataGridView.Rows.SharedRow(i);
                        ((DataGridView動静報告RowCell)r.Cells[this.Index]).Maximum =
                            value;
                    }
                }
            }

            /// <summary>
            /// ProgressBarの最小値
            /// </summary>
            public int Mimimum
            {
                get
                {
                    return ((DataGridView動静報告RowCell)this.CellTemplate).Mimimum;
                }
                set
                {
                    if (this.Mimimum == value)
                        return;
                    //セルテンプレートの値を変更する
                    ((DataGridView動静報告RowCell)this.CellTemplate).Mimimum =
                        value;
                    //DataGridViewにすでに追加されているセルの値を変更する
                    if (this.DataGridView == null)
                        return;
                    int rowCount = this.DataGridView.RowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataGridViewRow r = this.DataGridView.Rows.SharedRow(i);
                        ((DataGridView動静報告RowCell)r.Cells[this.Index]).Mimimum =
                            value;
                    }
                }
            }
        }
        public class DataGridView動静報告RowCell : DataGridViewTextBoxCell
        {
            //コンストラクタ
            public DataGridView動静報告RowCell()
            {
                this.maximumValue = 100;
                this.mimimumValue = 0;
            }

            private int maximumValue;
            public int Maximum
            {
                get
                {
                    return this.maximumValue;
                }
                set
                {
                    this.maximumValue = value;
                }
            }

            private int mimimumValue;
            public int Mimimum
            {
                get
                {
                    return this.mimimumValue;
                }
                set
                {
                    this.mimimumValue = value;
                }
            }

            //セルの値のデータ型を指定する
            //ここでは、整数型とする
            public override Type ValueType
            {
                get
                {
                    return typeof(int);
                }
            }

        }
        #endregion

        private void button_New_Click(object sender, EventArgs e)
        {
            動静報告Form form = new 動静報告Form(this);
            form.BashoList = bashoList;
            form.TenkouList = tenkouList;
            form.KazamukiList = kazamukiList;
            form.SentaisetsubiList = sentaisetsubiList;
            form.KenkoujyoutaiList = kenkoujyoutaiList;

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            DjDouseiHoukoku houkoku = dataGridView1.SelectedRows[0].Cells[18].Value as DjDouseiHoukoku;
            if (houkoku == null)
            {
                return;
            }
            if (houkoku.DeleteFlag == 1)
            {
                MessageBox.Show("その情報は削除されています", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            動静報告Form form = new 動静報告Form(this,houkoku);
            form.BashoList = bashoList;
            form.TenkouList = tenkouList;
            form.KazamukiList = kazamukiList;
            form.SentaisetsubiList = sentaisetsubiList;
            form.KenkoujyoutaiList = kenkoujyoutaiList;

            if (form.ShowDialog() == DialogResult.OK)
            {
                Search();
            }
        }


        public void Set(DjDouseiHoukoku houkoku)
        {
            int rowNo = 0;
            if (dataGridView1.SelectedRows.Count == 0)
            {
                if (douseiHoukokuList == null)
                {
                    douseiHoukokuList = new List<DjDouseiHoukoku>();
                }
                douseiHoukokuList.Add(houkoku);
                SetRows();
                return;
            }
            else
            {
                DjDouseiHoukoku selected = dataGridView1.SelectedRows[0].Cells[18].Value as DjDouseiHoukoku;
                if (selected.DjDouseiHoukokuID == houkoku.DjDouseiHoukokuID)
                {
                    rowNo = dataGridView1.SelectedRows[0].Index;
                }
                else
                {
                    douseiHoukokuList.Add(houkoku);

                    dataGridView1.Rows.Add();
                    rowNo = dataGridView1.Rows.Count - 1;
                }
                int colNo = 0;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = GetDateStr(houkoku.HoukokuDate);
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = houkoku.CurrentPlace;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = GetPort(houkoku.LeavePortID);
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = GetDateStr(houkoku.LeaveDate);
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = GetPort(houkoku.DestinationPortID);
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = GetDateStr(houkoku.ArrivalDate);
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = GetTenkou(houkoku.MsDjTenkouID);
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = GetKazamuki(houkoku.MsDjKazamukiID);
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = houkoku.Fusoku;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = houkoku.Nami;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = houkoku.Uneri;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = houkoku.Sitei;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = houkoku.Sinro;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = houkoku.Sokuryoku;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = GetSentaisetsubi(houkoku.MsDjSentaisetsubiID);
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = GetKenkoujyoutai(houkoku.MsDjKenkoujyoutaiID);
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = houkoku.Norikumiinsu;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = houkoku.Bikou;

                dataGridView1.Rows[rowNo].Cells[colNo].Value = houkoku;

            }
            dataGridView1.CurrentCell = dataGridView1.Rows[rowNo].Cells[0];
        }

        private void button_Output_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dateTimePicker報告日From.Value;
            DateTime toDate = dateTimePicker報告日To.Value;
            saveFileDialog1.FileName = "動静報告一覧_" + fromDate.ToString("yyyyMMdd") + "-" + toDate.ToString("yyyyMMdd") + ".xlsx";

            //string exeDir = System.IO.Directory.GetCurrentDirectory();
            string exeDir = 同期Client.BASE_FOLDER;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                NBaseData.BLC.動静帳票.動静報告一覧 report = new NBaseData.BLC.動静帳票.動静報告一覧();
                bool ret = report.動静報告一覧取得(同期Client.LOGIN_USER, exeDir + "\\Template\\", saveFileDialog1.FileName, 同期Client.LOGIN_VESSEL, fromDate, toDate, douseiHoukokuList);

                if (ret)
                {
                    MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //MessageBox.Show(this, "ファイルの出力に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    string Err = report.ErrorMsg;
                    MessageBox.Show(this, "ファイルの出力に失敗しました。(" + Err + ")", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
