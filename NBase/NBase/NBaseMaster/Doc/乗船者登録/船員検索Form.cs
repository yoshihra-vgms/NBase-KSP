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

namespace NBaseMaster.Doc.乗船者登録
{
    public partial class 船員検索Form : Form
    {
        public NBaseData.DAC.MsUser user;

        public 船員検索Form()
        {
            this.user = null;
            InitializeComponent();
        }

        private void 船員検索Form_Load(object sender, EventArgs e)
        {
            textBox姓カナ.Text = "";
            textBox名カナ.Text = "";
        }

        private void buttonクリア_Click(object sender, EventArgs e)
        {
            textBox姓カナ.Text = "";
            textBox名カナ.Text = "";
        }

        private void button検索_Click(object sender, EventArgs e)
        {
            string seiKana = textBox姓カナ.Text;
            string meiKana = textBox名カナ.Text;

            List<NBaseData.DAC.MsUser> users = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                users = serviceClient.MsUser_SearchRecords2(NBaseCommon.Common.LoginUser,seiKana,meiKana);
            }
            SetRows(users);
        }

        private void SetRows(List<NBaseData.DAC.MsUser> users)
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
            }
            #endregion

            foreach (NBaseData.DAC.MsUser s in users)
            {
                int colNo = 0;
                int colMax = 4;
                object[] rowDatas = new object[colMax];
                rowDatas[colNo] = s;
                colNo++;
                rowDatas[colNo] = s.Sei;
                colNo++;
                rowDatas[colNo] = s.Mei;
                colNo++;

                dataGridView1.Rows.Add(rowDatas);
            }
        }

        private void button選択_Click(object sender, EventArgs e)
        {
            try
            {
                user = dataGridView1.SelectedRows[0].Cells[0].Value as NBaseData.DAC.MsUser;
            }
            catch
            {
            }
            this.Close();
        }

        private void buttonキャンセル_Click(object sender, EventArgs e)
        {
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
