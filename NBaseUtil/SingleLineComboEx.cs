using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBaseUtil
{
    public partial class SingleLineComboEx : UserControl
    {

        public class SingleLineComboExItem : Object
        {
            public string Key1;
            public string Key2;
            public string Value;

            public override string ToString()
            {
                return Value;
            }
        }

        private List<SingleLineComboExItem> items;
        public List<SingleLineComboExItem> Items
        {
            get
            {
                if (items == null)
                {
                    items = new List<SingleLineComboExItem>();
                }

                return items;
            }
        }
        public object SelectedItem
        {
            get
            {
                string text = textBox1.Text;
                foreach (object o in Items)
                {
                    if (text == o.ToString())
                    {
                        return o;
                    }
                }
                return null;
            }
        }
        private List<string> autoCompleteCustomSource;
        public List<string> AutoCompleteCustomSource
        {
            get
            {
                if (autoCompleteCustomSource == null)
                {
                    autoCompleteCustomSource = new List<string>();
                }

                return autoCompleteCustomSource;
            }
        }

        public override string Text
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        public override Color BackColor
        {
            get
            {
                return textBox1.BackColor;
            }
            set
            {
                textBox1.BackColor = value;
            }
        }

        public int MaxLength
        {
            get
            {
                return textBox1.MaxLength;
            }
            set
            {
                textBox1.MaxLength = value;
            }
        }

        public new int Width
        {
            get
            {
                return textBox1.Width;
            }
            set
            {
                textBox1.Width = value;
                dataGridView1.Width = value;
            }
        }

        public new int Height
        {
            get
            {
                return textBox1.Height;
            }
            set
            {
                textBox1.Height = value;

                dataGridView1.Location = new Point(0, value);
            }
        }

        public bool ReadOnly
        {
            get
            {
                return textBox1.ReadOnly;
            }
            set
            {
                textBox1.ReadOnly = value;
            }
        }

        public delegate void SelectedEventHandler(object sender, EventArgs e);
        public event SelectedEventHandler selected;
        private bool selectItem;

        public SingleLineComboEx()
        {
            InitializeComponent();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns.Add("column", null);
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.Visible = false;
        }

        private void SetGrid()
        {
            if (textBox1.ReadOnly)
                return;

            if (selectItem)
            {
                selectItem = false;
                return;
            }

            ClearRows();

            if (textBox1.Text.Length == 0)
            {
                // 入力がない場合、何も表示しない
            }
            else
            {

                // ２０１６．０６　全角・半角を無視して検索できるようする場合のコード
                System.Globalization.CompareInfo ci = System.Globalization.CultureInfo.CurrentCulture.CompareInfo;

                foreach (SingleLineComboExItem item in items)
                {
                    //if (item.Key1.IndexOf(textBox1.Text) >= 0 || item.Key2.IndexOf(textBox1.Text) >= 0)
                    //{
                    //    object[] obj = { item };
                    //    AddRow(obj);
                    //}

                    // ２０１６．０６　全角・半角を無視して検索できるようする場合のコード
                    if (ci.IndexOf(item.Key1, textBox1.Text, System.Globalization.CompareOptions.IgnoreWidth | System.Globalization.CompareOptions.IgnoreCase) >= 0)
                    {
                        object[] obj = { item };
                        AddRow(obj);
                    }
                    else if (ci.IndexOf(item.Key2, textBox1.Text, System.Globalization.CompareOptions.IgnoreWidth | System.Globalization.CompareOptions.IgnoreCase) >= 0)
                    {
                        object[] obj = { item };
                        AddRow(obj);
                    }
                }
            }

            dataGridView1.CurrentCell = null;

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Visible = true;
            }
            else
            {
                dataGridView1.Visible = false;
            }

            selectItem = false;
        }

        private void AddRow(object[] row)
        {
            dataGridView1.Rows.Add(row);
        }

        private void ClearRows()
        {
            dataGridView1.Rows.Clear();
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SelectItem();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("dataGridView1_KeyDown");
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                SelectItem();
            }
        }

        private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("dataGridView1_PreviewKeyDown");
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    selectItem = true;
                    dataGridView1.Visible = false;
                    break;
            }
        }




        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //SetGrid();
            if (e.KeyCode == Keys.Enter)
            {
                SetGrid();
            }
            else
            {
                dataGridView1.Visible = false;
            }
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("textBox1_PreviewKeyDown");
            if (textBox1.ReadOnly)
                return;

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    selectItem = true;
                    dataGridView1.Visible = false;
                    break;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            //SetGrid();
        }




        private void SingleLineCombo_Leave(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
        }




        private void SelectItem()
        {
            textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
            textBox1.Select(textBox1.Text.Length, 0);
            dataGridView1.Visible = false;

            if (selected != null)
            {
                selected(this, null);
            }

            selectItem = true;
        }
    }
}
