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

namespace NBaseCommon
{
    public partial class 船員検索Form : Form
    {
        private SeninSearchClientForm clientForm;
        private bool userColumnEnabled;
        private MsSenin senin;

        public MsSenin GetSenin()
        {
            return senin;
        }

        public 船員検索Form(SeninSearchClientForm clientForm, bool userColumnEnabled)
        {
            this.clientForm = clientForm;
            this.userColumnEnabled = userColumnEnabled;
            this.senin = null;
            InitializeComponent();
        }

        public void SetSenin(MsSenin senin)
        {
            this.senin = senin;
        }

        private void 船員検索Form_Load(object sender, EventArgs e)
        {
            textBox姓カナ.Text = "";
            textBox名カナ.Text = "";
            textBox氏名コード.Text = "";
            if (this.senin != null)
            {
                textBox氏名コード.Text = senin.ShimeiCode.Trim();
                
                MsSeninFilter filter = CreateFilter();

                List<MsSenin> result = clientForm.SearchMsSenin(filter);

                SetRows(result);
            }
        }

        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox姓カナ.Text = "";
            textBox名カナ.Text = "";
            textBox氏名コード.Text = "";
        }

        private void button検索_Click(object sender, EventArgs e)
        {
            MsSeninFilter filter = CreateFilter();

            List<MsSenin> result = clientForm.SearchMsSenin(filter);

            SetRows(result);
        }

        private void SetRows(List<MsSenin> senins)
        {

            dataGridView1.Rows.Clear();

            #region カラムの設定
            if (dataGridView1.Columns.Count == 0)
            {
                DataGridView船員検索RowColumn column = new DataGridView船員検索RowColumn();
                column.Visible = false;
                dataGridView1.Columns.Add(column);

                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "氏名（姓）";
                textColumn.Width = 90;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "氏名（名）";
                textColumn.Width = 90;
                dataGridView1.Columns.Add(textColumn);

                textColumn = new DataGridViewTextBoxColumn();
                textColumn.HeaderText = "従業員番号";
                textColumn.Width = 90;
                dataGridView1.Columns.Add(textColumn);

                if (userColumnEnabled)
                {
                    textColumn = new DataGridViewTextBoxColumn();
                    textColumn.HeaderText = "紐付け状態";
                    textColumn.Width = 90;
                    dataGridView1.Columns.Add(textColumn);
                }
            }
            #endregion

            foreach (MsSenin s in senins)
            {
                int colNo = 0;
                int colMax = 4;
                if (userColumnEnabled)
                {
                    colMax = 5;
                }
                object[] rowDatas = new object[colMax];
                rowDatas[colNo] = s;
                colNo++;
                rowDatas[colNo] = s.Sei;
                colNo++;
                rowDatas[colNo] = s.Mei;
                colNo++;
                rowDatas[colNo] = s.ShimeiCode;
                colNo++;
                if (userColumnEnabled)
                {
                    if (s.MsUserID != null && s.MsUserID.Length > 0)
                    {
                        rowDatas[colNo] = "済";
                    }
                    else
                    {
                        rowDatas[colNo] = "";
                    }
                }

                dataGridView1.Rows.Add(rowDatas);
            }
        }

        private MsSeninFilter CreateFilter()
        {
            MsSeninFilter filter = new MsSeninFilter();

            filter.種別無し = false;

            if (textBox姓カナ.Text.Length > 0)
            {
                filter.SeiKana = textBox姓カナ.Text;
            }
            if (textBox名カナ.Text.Length > 0)
            {
                filter.MeiKana = textBox名カナ.Text;
            }
            if (textBox氏名コード.Text.Length > 0)
            {
                filter.ShimeiCode = textBox氏名コード.Text;
            }

            filter.船員テーブルのみ対象 = true;
            filter.OrderByStr = " ORDER BY MS_SENIN.SEI_KANA, MS_SENIN.MEI_KANA";

            return filter;
        }

        private void button選択_Click(object sender, EventArgs e)
        {
            try
            {
                senin = dataGridView1.SelectedRows[0].Cells[0].Value as MsSenin;

                clientForm.SetMsSenin(senin, userColumnEnabled);
            }
            catch
            {
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void buttonキャンセル_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        #region 内部クラス
        public class DataGridView船員検索RowColumn : DataGridViewTextBoxColumn
        {
            //コンストラクタ
            public DataGridView船員検索RowColumn()
            {
                this.CellTemplate = new DataGridView船員検索RowCell();
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
                    if (!(value is DataGridView船員検索RowCell))
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
                    return ((DataGridView船員検索RowCell)this.CellTemplate).Maximum;
                }
                set
                {
                    if (this.Maximum == value)
                        return;
                    //セルテンプレートの値を変更する
                    ((DataGridView船員検索RowCell)this.CellTemplate).Maximum =
                        value;
                    //DataGridViewにすでに追加されているセルの値を変更する
                    if (this.DataGridView == null)
                        return;
                    int rowCount = this.DataGridView.RowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataGridViewRow r = this.DataGridView.Rows.SharedRow(i);
                        ((DataGridView船員検索RowCell)r.Cells[this.Index]).Maximum =
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
                    return ((DataGridView船員検索RowCell)this.CellTemplate).Mimimum;
                }
                set
                {
                    if (this.Mimimum == value)
                        return;
                    //セルテンプレートの値を変更する
                    ((DataGridView船員検索RowCell)this.CellTemplate).Mimimum =
                        value;
                    //DataGridViewにすでに追加されているセルの値を変更する
                    if (this.DataGridView == null)
                        return;
                    int rowCount = this.DataGridView.RowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataGridViewRow r = this.DataGridView.Rows.SharedRow(i);
                        ((DataGridView船員検索RowCell)r.Cells[this.Index]).Mimimum =
                            value;
                    }
                }
            }
        }
        public class DataGridView船員検索RowCell : DataGridViewTextBoxCell
        {
            //コンストラクタ
            public DataGridView船員検索RowCell()
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
