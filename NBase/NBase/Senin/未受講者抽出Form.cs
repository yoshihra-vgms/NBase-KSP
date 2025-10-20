using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DS;
using NBaseData.DAC;
using NBaseUtil;
using Senin.util;
using System.IO;

namespace Senin
{
    public partial class 未受講者抽出Form : Form
    {
        private static 未受講者抽出Form instance;

        private List<SiKoushu> koushuList = null;
        private SiKoushuFilter filter = null;

        public 未受講者抽出Form()
        {
            InitializeComponent();
        }

        public static 未受講者抽出Form Instance()
        {
            if (instance == null)
            {
                instance = new 未受講者抽出Form();
            }

            return instance;
        }

        private void 未受講者抽出Form_Load(object sender, EventArgs e)
        {
            Init();
            SetDataGridView(new List<SiKoushu>());
        }

        private void 未受講者抽出Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }

        #region private void Init()
        private void Init()
        {
            InitComboBox講習名();
            InitComboBox職名();
            label対象件数.Text = "      件";

            ResetSearchKey();
        }
        #endregion
        #region private void ResetSearchKey()
        private void ResetSearchKey()
        {
            comboBox講習名.SelectedIndex = 0;
            comboBox職名.SelectedIndex = 0;
            nullableDateTimePicker受講日From.Value = DateTime.Now;
            nullableDateTimePicker受講日From.Value = null;
            nullableDateTimePicker受講日To.Value = DateTime.Now;
            nullableDateTimePicker受講日To.Value = null;

            textBox氏名.Text = null;
            textBox氏名カナ.Text = null;
            textBox氏名コード.Text = null;

            radioButton未受講.Checked = true;
            radioButton受講済.Checked = false;
        }
        #endregion
        #region private void InitComboBox講習名()
        private void InitComboBox講習名()
        {
            comboBox講習名.Items.Add(string.Empty);

            foreach (MsSiKoushu k in SeninTableCache.instance().GetMsSiKoushuList(NBaseCommon.Common.LoginUser))
            {
                comboBox講習名.Items.Add(k);
                comboBox講習名.AutoCompleteCustomSource.Add(k.Name);
            }
            comboBox講習名.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox講習名.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comboBox講習名.SelectedIndex = 0;
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
            ResetSearchKey();
        }
        #endregion

        /// <summary>
        /// 一覧の行をダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            SiKoushu koushu = dataGridView1.SelectedRows[0].Cells[9].Value as SiKoushu;
            if (koushu.DeleteFlag == 1)
            {
                MessageBox.Show("その情報は削除されています", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            講習管理詳細Form form = new 講習管理詳細Form(this, koushu);
            form.ShowDialog();
        }
        #endregion

        /// <summary>
        /// 詳細画面で変更が発生したときの処理
        /// </summary>
        #region public void ReSearch()
        public void ReSearch()
        {
            Search();
        }
        #endregion
        #region public void SetSiKoushu(SiKoushu koushu) // ReSearch　にする
        //public void SetSiKoushu(SiKoushu koushu)
        //{
        //    int rowNo = 0;
        //    if (dataGridView1.SelectedRows.Count == 0)
        //    {
        //        if (koushuList == null)
        //        {
        //            koushuList = new List<SiKoushu>();
        //        }
        //        koushuList.Add(koushu);
        //        SetDataGridView(koushuList);
        //        return;
        //    }
        //    else
        //    {
        //        SiKoushu selected = dataGridView1.SelectedRows[0].Cells[9].Value as SiKoushu;
        //        if (selected.SiKoushuID == koushu.SiKoushuID)
        //        {
        //            rowNo = dataGridView1.SelectedRows[0].Index;
        //        }
        //        else
        //        {
        //            koushuList.Add(koushu);

        //            dataGridView1.Rows.Add();
        //            rowNo = dataGridView1.Rows.Count - 1;
        //        }
        //        int colNo = 0;
        //        dataGridView1.Rows[rowNo].Cells[colNo++].Value = koushu.SeninShimeiCode;
        //        dataGridView1.Rows[rowNo].Cells[colNo++].Value = koushu.SeninShokumei;
        //        dataGridView1.Rows[rowNo].Cells[colNo++].Value = koushu.SeninName;
        //        dataGridView1.Rows[rowNo].Cells[colNo++].Value = koushu.KoushuName;
        //        dataGridView1.Rows[rowNo].Cells[colNo++].Value = koushu.Basho;
        //        dataGridView1.Rows[rowNo].Cells[colNo++].Value = (koushu.JisekiFrom == DateTime.MinValue ? "" : koushu.JisekiFrom.ToShortDateString());
        //        dataGridView1.Rows[rowNo].Cells[colNo++].Value = (koushu.JisekiTo == DateTime.MinValue ? "" : koushu.JisekiTo.ToShortDateString());
        //        dataGridView1.Rows[rowNo].Cells[colNo++].Value = koushu.KoushuYukokigenStr;
        //        int attachCount = 0;
        //        foreach (SiKoushuAttachFile a in koushu.AttachFiles)
        //        {
        //            if (a.DeleteFlag == 0)
        //                attachCount++;
        //        }
        //        dataGridView1.Rows[rowNo].Cells[colNo++].Value = attachCount.ToString();
        //        dataGridView1.Rows[rowNo].Cells[colNo++].Value = koushu.Bikou;

        //        dataGridView1.Rows[rowNo].Cells[colNo].Value = koushu;

        //    }
        //    dataGridView1.CurrentCell = dataGridView1.Rows[rowNo].Cells[0];
        //}
        #endregion

        /// <summary>
        /// 「Excel出力」クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button出力_Click(object sender, EventArgs e)
        private void button出力_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "受講状況一覧_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    byte[] result = null;
                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            result = serviceClient.BLC_Excel_未受講者一覧出力(NBaseCommon.Common.LoginUser, filter, koushuList);
                        }
                    }, "受講状況一覧を出力中です...");
                    progressDialog.ShowDialog();

                    if (result == null)
                    {
                        MessageBox.Show("受講状況一覧の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "未受講者抽出", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = System.Windows.Forms.Cursors.Default;
                        return;
                    }

                    System.IO.FileStream filest = new System.IO.FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    filest.Write(result, 0, result.Length);
                    filest.Close();

                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    //カーソルを通常に戻す
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    MessageBox.Show("受講状況一覧の出力に失敗しました。\n (Err:" + ex.Message + ")", "未受講者抽出", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        /// <summary>
        /// 検索ロジックをコールする
        /// </summary>
        #region internal void Search()
        internal void Search()
        {
            // 検索条件の確認および取得
            if (CreateFilter() == false)
                return;

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    koushuList = serviceClient.BLC_講習管理_検索(NBaseCommon.Common.LoginUser, filter);
                }
            }, "データ取得中です...");

            progressDialog.ShowDialog();

            label対象件数.Text = String.Format("{0,5}", koushuList.Count()) + " 件";//koushuList.Count().ToString("####") + " 件";

            SetDataGridView(koushuList);
        }
        #endregion

        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="koushus"></param>
        #region private void SetDataGridView(List<SiKoushu> koushus)
        private void SetDataGridView(List<SiKoushu> koushus)
        {
            Cursor = Cursors.WaitCursor;

            dataGridView1.Rows.Clear();

            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "氏名" + Environment.NewLine + "コード";
                textColumn.Width = 65;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "職名";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "氏名";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);
                
                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "講習名";
                textColumn.Width = 120;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "講習場所";
                textColumn.Width = 120;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "受講開始日";
                textColumn.Width = 90;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "受講終了日";
                textColumn.Width = 90;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "有効期限";
                textColumn.Width = 90;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "添付";
                textColumn.Width = 40;
                dataGridView1.Columns.Add(textColumn);

                DataGridView講習RowColumn column = new DataGridView講習RowColumn();
                column.Visible = false;
                dataGridView1.Columns.Add(column);
            }
            #endregion

            int rowNo = 0;
            foreach (SiKoushu row in koushus)
            {
                #region 情報を一覧にセットする

                int colNo = 0;
                object[] rowDatas = new object[10];
                //rowDatas[colNo] = int.Parse(row.SeninShimeiCode);
                rowDatas[colNo] = row.SeninShimeiCode;
                colNo++;
                rowDatas[colNo] = row.SeninShokumei;
                colNo++;
                rowDatas[colNo] = row.SeninName;
                colNo++;
                rowDatas[colNo] = row.KoushuName;
                colNo++;
                rowDatas[colNo] = row.Basho;
                colNo++;
                rowDatas[colNo] = (row.JisekiFrom == DateTime.MinValue ? "" : row.JisekiFrom.ToShortDateString());
                colNo++;
                rowDatas[colNo] = (row.JisekiTo == DateTime.MinValue ? "" : row.JisekiTo.ToShortDateString());
                colNo++;
                rowDatas[colNo] = row.KoushuYukokigenStr;
                colNo++;
                int attachCount = 0;
                foreach (SiKoushuAttachFile a in row.AttachFiles)
                {
                    if (a.DeleteFlag == 0)
                        attachCount++;
                }
                rowDatas[colNo] = attachCount.ToString(); //attachCount == 0 ? "" : attachCount.ToString();
                colNo++;

                rowDatas[colNo] = row;

                dataGridView1.Rows.Add(rowDatas);

                rowNo++;

                #endregion
            }

            Cursor = Cursors.Default;
        }
        #endregion

        /// <summary>
        /// 検索条件の確認および取得
        /// </summary>
        /// <returns></returns>
        #region private bool CreateFilter()
        private bool CreateFilter()
        {
            bool ret = true;

            filter = new SiKoushuFilter();

            if (comboBox講習名.SelectedItem is MsSiKoushu)
            {
                filter.MsSiKoushuID = (comboBox講習名.SelectedItem as MsSiKoushu).MsSiKoushuID;
            }
            if (comboBox講習名.Text.Length > 0 && filter.MsSiKoushuID == int.MinValue)
            {
                MessageBox.Show("指定された講習名はありません", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ret = false;
            }
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
            if (textBox氏名カナ.Text.Length > 0)
            {
                filter.NameKana = textBox氏名カナ.Text;
            }

            if (nullableDateTimePicker受講日From.Value != null)
            {
                filter.JisekiFrom = (DateTime)nullableDateTimePicker受講日From.Value;
            }
            if (nullableDateTimePicker受講日To.Value != null)
            {
                filter.JisekiTo = (DateTime)nullableDateTimePicker受講日To.Value;
            }

            if (radioButton未受講.Checked)
            {
                filter.Is未受講 = true;
            }
            if (radioButton受講済.Checked)
            {
                filter.Is受講済み = true;
            }

            filter.Flag = 1; // 未受講者抽出からの検索
            filter.Is退職者を除く = true;// 退職者を除く

            return ret;
        }
        #endregion

        #region 内部クラス
        public class DataGridView講習RowColumn : DataGridViewTextBoxColumn
        {
            //コンストラクタ
            public DataGridView講習RowColumn()
            {
                this.CellTemplate = new DataGridView講習RowCell();
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
                    if (!(value is DataGridView講習RowCell))
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
                    return ((DataGridView講習RowCell)this.CellTemplate).Maximum;
                }
                set
                {
                    if (this.Maximum == value)
                        return;
                    //セルテンプレートの値を変更する
                    ((DataGridView講習RowCell)this.CellTemplate).Maximum =
                        value;
                    //DataGridViewにすでに追加されているセルの値を変更する
                    if (this.DataGridView == null)
                        return;
                    int rowCount = this.DataGridView.RowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataGridViewRow r = this.DataGridView.Rows.SharedRow(i);
                        ((DataGridView講習RowCell)r.Cells[this.Index]).Maximum =
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
                    return ((DataGridView講習RowCell)this.CellTemplate).Mimimum;
                }
                set
                {
                    if (this.Mimimum == value)
                        return;
                    //セルテンプレートの値を変更する
                    ((DataGridView講習RowCell)this.CellTemplate).Mimimum =
                        value;
                    //DataGridViewにすでに追加されているセルの値を変更する
                    if (this.DataGridView == null)
                        return;
                    int rowCount = this.DataGridView.RowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataGridViewRow r = this.DataGridView.Rows.SharedRow(i);
                        ((DataGridView講習RowCell)r.Cells[this.Index]).Mimimum =
                            value;
                    }
                }
            }
        }
        public class DataGridView講習RowCell : DataGridViewTextBoxCell
        {
            //コンストラクタ
            public DataGridView講習RowCell()
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
    }
}
