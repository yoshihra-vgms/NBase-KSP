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
    public partial class 講習管理Form : Form
    {
        private static 講習管理Form instance;

        private List<SiKoushu> koushuList = null;

        public 講習管理Form()
        {
            InitializeComponent();
        }

        public static 講習管理Form Instance()
        {
            if (instance == null)
            {
                instance = new 講習管理Form();
            }

            return instance;
        }

        public void Show(int shokumeiId, DateTime fromDate, DateTime toDate)
        {
            Show();

            SiKoushuFilter filter = new SiKoushuFilter();
            filter.MsSiShokumeiID = shokumeiId;
            filter.YoteiFrom = fromDate;
            filter.YoteiTo = toDate;

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    koushuList = serviceClient.BLC_講習管理_検索(NBaseCommon.Common.LoginUser, filter);
                }
            }, "データ取得中です...");

            progressDialog.ShowDialog();

            SetRows(koushuList);
        }

        private void 講習管理Form_Load(object sender, EventArgs e)
        {
            Init();
            SetRows(new List<SiKoushu>());
        }

        private void 講習管理Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }

        private void Init()
        {
            InitComboBox職名();
            Reset();
        }
        private void Reset()
        {
            textBox講習名.Text = "";
            comboBox職名.SelectedIndex = 0;
            nullableDateTimePicker開始予定.Value = DateTime.Now;
            nullableDateTimePicker開始予定.Value = null;
            textBox備考.Text = null;

            textBox氏名.Text = null;
            textBox氏名カナ.Text = null;
            textBox氏名コード.Text = null;

            checkBox受講済み.Checked = true;
            checkBox未受講.Checked = true;
            checkBox期限切れ.Checked = false;
        }
        private void InitComboBox職名()
        {
            comboBox職名.Items.Add(string.Empty);

            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                comboBox職名.Items.Add(s);
            }

            comboBox職名.SelectedIndex = 0;
        }

        /// <summary>
        /// 「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }

        /// <summary>
        /// 「検索条件クリア」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonクリア_Click(object sender, EventArgs e)
        {
            Reset();
        }

        /// <summary>
        /// 「追加」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button追加_Click(object sender, EventArgs e)
        {
            講習管理詳細Form form = new 講習管理詳細Form(this);
            form.ShowDialog();
        }

        /// <summary>
        /// 「参照」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button参照_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            SiKoushu koushu = dataGridView1.SelectedRows[0].Cells[12].Value as SiKoushu;
            if (koushu.DeleteFlag == 1)
            {
                MessageBox.Show("その情報は削除されています", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            講習管理詳細Form form = new 講習管理詳細Form(this, koushu);
            form.ShowDialog();
        }

        /// <summary>
        /// 一覧の行をダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            SiKoushu koushu = dataGridView1.SelectedRows[0].Cells[12].Value as SiKoushu;
            if (koushu.DeleteFlag == 1)
            {
                MessageBox.Show("その情報は削除されています", "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            講習管理詳細Form form = new 講習管理詳細Form(this, koushu);
            form.ShowDialog();
        }

        /// <summary>
        /// 「一覧出力」クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button出力_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "講習一覧_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    byte[] result = null;
                    
                    //2013/12/18 追加 m.y
                    //サーバーエラー時のフラグ
                    bool serverError = false;

                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            //--------------------------------
                            //2013/12/18 コメントアウト m.y
                            //result = serviceClient.BLC_Excel_講習一覧出力(NBaseCommon.Common.LoginUser, koushuList);
                            //--------------------------------
                            //2013/12/18 変更: ServiceClientのExceptionを受け取る m.y
                            try
                            {
                                result = serviceClient.BLC_Excel_講習一覧出力(NBaseCommon.Common.LoginUser, koushuList);
                            }
                            catch (Exception exp)
                            {
                                //MessageBox.Show(exp.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //serverError = true;//ここでreturnしても関数から抜け出せないのでフラグを用いる
                            }
                            //--------------------------------
                        }
                    }, "講習管理一覧表を出力中です...");
                    progressDialog.ShowDialog();

                    //--------------------------------
                    //2013/12/18 追加 m.y 
                    if (serverError == true)
                        return;
                    //--------------------------------

                    if (result == null)
                    {
                        MessageBox.Show("講習管理一覧表の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "講習管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("講習管理一覧表の出力に失敗しました。\n (Err:" + ex.Message + ")", "講習管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

       
        /// <summary>
        /// 検索ロジックをコールする
        /// </summary>
        internal void Search()
        {
            SiKoushuFilter filter = CreateSiKoushuFilter();

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    koushuList = serviceClient.BLC_講習管理_検索(NBaseCommon.Common.LoginUser, filter);
                }
            }, "データ取得中です...");

            progressDialog.ShowDialog();

            SetRows(koushuList);
        }

        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="koushus"></param>
        private void SetRows(List<SiKoushu> koushus)
        {
            Cursor = Cursors.WaitCursor;

            dataGridView1.Rows.Clear();

            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "講習名";
                textColumn.Width = 120;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "講習場所";
                textColumn.Width = 120;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "開始予定日";
                textColumn.Width = 90;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "終了予定日";
                textColumn.Width = 90;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "コード";
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

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "備考";
                textColumn.Width = 150;
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
                object[] rowDatas = new object[13];
                rowDatas[colNo] = row.KoushuName;
                colNo++;
                rowDatas[colNo] = row.Basho;
                colNo++;
                rowDatas[colNo] = (row.YoteiFrom == DateTime.MinValue ? "" : row.YoteiFrom.ToShortDateString());
                colNo++;
                rowDatas[colNo] = (row.YoteiTo == DateTime.MinValue ? "" : row.YoteiTo.ToShortDateString());
                colNo++;
                rowDatas[colNo] = row.SeninShimeiCode;
                colNo++;
                rowDatas[colNo] = row.SeninShokumei;
                colNo++;
                rowDatas[colNo] = row.SeninName;
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
                rowDatas[colNo] = attachCount.ToString();
                colNo++;
                rowDatas[colNo] = row.Bikou;
                colNo++;
                rowDatas[colNo] = row;

                dataGridView1.Rows.Add(rowDatas);

                rowNo++;

                #endregion
            }

            Cursor = Cursors.Default;

        }

        /// <summary>
        /// 検索条件を取得する
        /// </summary>
        /// <returns></returns>
        #region private SiKoushuFilter CreateSiKoushuFilter()
        private SiKoushuFilter CreateSiKoushuFilter()
        {
            SiKoushuFilter filter = new SiKoushuFilter();

            if (textBox講習名.Text.Length > 0)
            {
                filter.KoushuName = textBox講習名.Text;
            }
            if (comboBox職名.SelectedItem is MsSiShokumei)
            {
                filter.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }
            if (nullableDateTimePicker開始予定.Value != null)
            {
                filter.YoteiFrom = (DateTime)nullableDateTimePicker開始予定.Value;
            }
            if (textBox備考.Text.Length > 0)
            {
                filter.Bikou = StringUtils.Escape(textBox備考.Text);
            }

            if (textBox氏名.Text.Length > 0)
            {
                filter.Name = textBox氏名.Text;
            }
            if (textBox氏名カナ.Text.Length > 0)
            {
                filter.NameKana = textBox氏名カナ.Text;
            }
            if (textBox氏名コード.Text.Length > 0)
            {
                filter.ShimeiCode = textBox氏名コード.Text;
            }

            if (checkBox受講済み.Checked)
            {
                filter.Is受講済み = true;
            }
            if (checkBox未受講.Checked)
            {
                filter.Is受講予定 = true;
            }
            if (checkBox期限切れ.Checked)
            {
                filter.Is期限切れ = true;
            }

            // 2013年度改造
            filter.Flag = 0; // 講習管理および船員詳細からの検索

            return filter;
        }
        #endregion


        public void SetSiKoushu(SiKoushu koushu)
        {
            int rowNo = 0;
            if (dataGridView1.SelectedRows.Count == 0)
            {
                if (koushuList == null)
                {
                    koushuList = new List<SiKoushu>();
                }
                koushuList.Add(koushu);
                SetRows(koushuList);
                return;
            }
            else
            {
                SiKoushu selected = dataGridView1.SelectedRows[0].Cells[12].Value as SiKoushu;
                if (selected.SiKoushuID == koushu.SiKoushuID)
                {
                    rowNo = dataGridView1.SelectedRows[0].Index;
                }
                else
                {
                    koushuList.Add(koushu);

                    dataGridView1.Rows.Add();
                    rowNo = dataGridView1.Rows.Count - 1;
                }
                int colNo = 0;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = koushu.KoushuName;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = koushu.Basho;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = (koushu.YoteiFrom == DateTime.MinValue ? "" : koushu.YoteiFrom.ToShortDateString());
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = (koushu.YoteiTo == DateTime.MinValue ? "" : koushu.YoteiTo.ToShortDateString());
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = koushu.SeninShimeiCode;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = koushu.SeninShokumei;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = koushu.SeninName;
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = (koushu.JisekiFrom == DateTime.MinValue ? "" : koushu.JisekiFrom.ToShortDateString());
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = (koushu.JisekiTo == DateTime.MinValue ? "" : koushu.JisekiTo.ToShortDateString());
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = koushu.KoushuYukokigenStr;
                int attachCount = 0;
                foreach (SiKoushuAttachFile a in koushu.AttachFiles)
                {
                    if (a.DeleteFlag == 0)
                        attachCount++;
                }
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = attachCount.ToString();
                dataGridView1.Rows[rowNo].Cells[colNo++].Value = koushu.Bikou;

                dataGridView1.Rows[rowNo].Cells[colNo].Value = koushu;

            }
            dataGridView1.CurrentCell = dataGridView1.Rows[rowNo].Cells[0];
        }


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

        // 2014.02 2013年度改造
        private void button未受講者抽出_Click(object sender, EventArgs e)
        {
            未受講者抽出Form form = 未受講者抽出Form.Instance();
            form.MdiParent = this.MdiParent;
            form.Show();
            form.WindowState = FormWindowState.Normal;
            form.Activate();
        }
    }
}
