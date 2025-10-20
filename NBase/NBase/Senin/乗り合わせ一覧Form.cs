using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace Senin
{
    public partial class 乗り合わせ一覧Form : Form
    {
        List<SiFellowPassengers> fellowPassengersList = null;


        private static 乗り合わせ一覧Form instance;

        private 乗り合わせ一覧Form()
        {
            InitializeComponent();
        }

        public static 乗り合わせ一覧Form Instance()
        {
            if (instance == null)
            {
                instance = new 乗り合わせ一覧Form();
            }

            return instance;
        }

        public void Show(int msSiShokumeiId)
        {
            Show();

            foreach(object item in comboBox職名.Items)
            {
                if (item is MsSiShokumei && (item as MsSiShokumei).MsSiShokumeiID == msSiShokumeiId)
                {
                    comboBox職名.SelectedItem = item;
                    break;
                }
            }
            Search();
        }

        private void 乗り合わせ一覧Form_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            InitComboBox職名();
            textBox氏名.Text = "";

            SetRows(null);
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
        /// 「追加」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button追加_Click(object sender, EventArgs e)
        {
            乗り合わせ詳細Form form = new 乗り合わせ詳細Form();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button閉じる_Click(object sender, EventArgs e)
        {
            Close();
            instance = null;
        }

        /// <summary>
        /// 「×」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 乗り合わせ一覧Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            instance = null;
        }


        /// <summary>
        /// 「Excel」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button出力_Click(object sender, EventArgs e)
        {
            if (fellowPassengersList == null || fellowPassengersList.Count == 0)
            {
                MessageBox.Show("データが検索されていません", "乗り合わせ一覧");
                return;
            }

            FileUtils.SetDesktopFolder(saveFileDialog1);

            saveFileDialog1.FileName = "乗り合わせ一覧表_" + DateTime.Today.ToString("yyyyMMdd") + ".xlsx";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    byte[] result = null;
                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            result = serviceClient.BLC_Excel_乗り合わせ一覧表出力(NBaseCommon.Common.LoginUser, fellowPassengersList);
                        }
                    }, "乗り合わせ一覧表を出力中です...");
                    progressDialog.ShowDialog();

                    if (result == null)
                    {
                        MessageBox.Show("乗り合わせ一覧表の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "乗り合わせ一覧", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("乗り合わせ一覧表の出力に失敗しました。\n (Err:" + ex.Message + ")", "乗り合わせ一覧", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }





        /// <summary>
        /// 検索ロジックをコールする
        /// </summary>
        internal void Search()
        {
            int msSiShokumeiId = -1;
            if (comboBox職名.SelectedItem is MsSiShokumei)
            {
                msSiShokumeiId = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }
            string name = null;
            if (textBox氏名.Text.Length > 0)
            {
                name = textBox氏名.Text;
            }

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    fellowPassengersList = serviceClient.SiFellowPassengers_SearchRecords(NBaseCommon.Common.LoginUser, msSiShokumeiId, name);
                }
            }, "データ取得中です...");

            progressDialog.ShowDialog();

            SetRows(fellowPassengersList);
        }


        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="fellowPassengersList"></param>
        private void SetRows(List<SiFellowPassengers> fellowPassengersList)
        {
            Cursor = Cursors.WaitCursor;

            dataGridView1.Rows.Clear();

            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "職名";
                textColumn.Width = 100;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "氏名";
                textColumn.Width = 100;
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
                textColumn.HeaderText = "備考";
                textColumn.Width = 200;
                dataGridView1.Columns.Add(textColumn);

                DataGridView乗り合わせRowColumn column = new DataGridView乗り合わせRowColumn();
                column.Visible = false;
                dataGridView1.Columns.Add(column);
            }
            #endregion

            if (fellowPassengersList != null)
            {
                int rowNo = 0;
                foreach (SiFellowPassengers row in fellowPassengersList)
                {
                    #region 情報を一覧にセットする

                    int colNo = 0;
                    object[] rowDatas = new object[6];
                    rowDatas[colNo] = SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, row.MsSiShokumeiID1);
                    colNo++;
                    rowDatas[colNo] = row.Name1;
                    colNo++;
                    rowDatas[colNo] = SeninTableCache.instance().GetMsSiShokumeiNameAbbr(NBaseCommon.Common.LoginUser, row.MsSiShokumeiID2);
                    colNo++;
                    rowDatas[colNo] = row.Name2;
                    colNo++;
                    rowDatas[colNo] = row.Bikou;
                    colNo++;
                    rowDatas[colNo] = row;

                    dataGridView1.Rows.Add(rowDatas);

                    rowNo++;

                    #endregion
                }
            }


            Cursor = Cursors.Default;

        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }
            SiFellowPassengers fellowPassengers = dataGridView1.SelectedRows[0].Cells[5].Value as SiFellowPassengers;
            乗り合わせ詳細Form form = new 乗り合わせ詳細Form(fellowPassengers);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }

        #region 内部クラス

        public class DataGridView乗り合わせRowColumn : DataGridViewTextBoxColumn
        {
            //コンストラクタ
            public DataGridView乗り合わせRowColumn()
            {
                this.CellTemplate = new DataGridView免許免状RowCell();
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
                    if (!(value is DataGridView免許免状RowCell))
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
                    return ((DataGridView免許免状RowCell)this.CellTemplate).Maximum;
                }
                set
                {
                    if (this.Maximum == value)
                        return;
                    //セルテンプレートの値を変更する
                    ((DataGridView免許免状RowCell)this.CellTemplate).Maximum =
                        value;
                    //DataGridViewにすでに追加されているセルの値を変更する
                    if (this.DataGridView == null)
                        return;
                    int rowCount = this.DataGridView.RowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataGridViewRow r = this.DataGridView.Rows.SharedRow(i);
                        ((DataGridView免許免状RowCell)r.Cells[this.Index]).Maximum =
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
                    return ((DataGridView免許免状RowCell)this.CellTemplate).Mimimum;
                }
                set
                {
                    if (this.Mimimum == value)
                        return;
                    //セルテンプレートの値を変更する
                    ((DataGridView免許免状RowCell)this.CellTemplate).Mimimum =
                        value;
                    //DataGridViewにすでに追加されているセルの値を変更する
                    if (this.DataGridView == null)
                        return;
                    int rowCount = this.DataGridView.RowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataGridViewRow r = this.DataGridView.Rows.SharedRow(i);
                        ((DataGridView免許免状RowCell)r.Cells[this.Index]).Mimimum =
                            value;
                    }
                }
            }
        }
        public class DataGridView免許免状RowCell : DataGridViewTextBoxCell
        {
            //コンストラクタ
            public DataGridView免許免状RowCell()
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
