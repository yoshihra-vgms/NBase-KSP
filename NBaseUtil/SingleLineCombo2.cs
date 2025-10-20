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
    public partial class SingleLineCombo2 : UserControl
    {
        private List<object> items;
        public List<object> Items
        {
            get
            {
                if (items == null)
                {
                    items = new List<object>();
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
                SetGrid();
                dataGridView1.Visible = false;
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

        public new int WindowHeight
        {
            get
            {
                return dataGridView1.Height;
            }
            set
            {
                dataGridView1.Height = value;
            }
        }

        public new int BaseHeight
        {
            get
            {
                return base.Height;
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

        /// <summary>
        /// コンボボックスグリッド表示可否設定
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public void AutoCompleteGridVisible(bool f)
        {
            dataGridView1.Visible = f;

            if (f == true)
            {
                base.Height = this.textBox1.Height + this.dataGridView1.Height;
            }
            else
            {
                base.Height = this.textBox1.Height;
            }

            if (Resized != null)
            {
                Resized(this, null);
            }
        }

        /// <summary>
        /// アイテムの全クリア
        /// </summary>
        public void Clear()
        {
            this.Items.Clear();
            this.AutoCompleteCustomSource.Clear();
        }    

        public delegate void SelectedEventHandler(object sender, EventArgs e);
        public event SelectedEventHandler Selected;
        public event SelectedEventHandler Cleared;

        public delegate void ResizedEventHandler(object sender, EventArgs e);
        public event ResizedEventHandler Resized;

        private bool selectItem;

        public SingleLineCombo2()
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
            //dataGridView1.Visible = false;
            this.AutoCompleteGridVisible(false);
        }

        private static string PrevKeyupString = "";
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            SetGrid();

            //クリアイベントの発生
            if (this.textBox1.Text.Length <= 0 && PrevKeyupString.Length != 0)
            {
                if (Cleared != null)
                {
                    Cleared(this, null);
                }
            }

            PrevKeyupString = this.textBox1.Text;   
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

            //if (textBox1.Text.Length == 0)
            //{
            //    dataGridView1.Visible = false;
            //    return;
            //}
            if (textBox1.Text.Length == 0)
            {
                foreach (string h in AutoCompleteCustomSource)
                {
                    object[] obj = { h };
                    AddRow(obj);
                }
            }
            else
            {
                foreach (string h in AutoCompleteCustomSource)
                {
                    //if (h.IndexOf(textBox1.Text) >= 0)
                    if (h.ToUpper().IndexOf(textBox1.Text.ToUpper()) >= 0)
                    {
                        object[] obj = { h };
                        AddRow(obj);
                    }
                }
            }

            dataGridView1.CurrentCell = null;

            if (dataGridView1.Rows.Count > 0)
            {
                //dataGridView1.Visible = true;
                this.AutoCompleteGridVisible(true);
            }
            else
            {
                //dataGridView1.Visible = false;
                this.AutoCompleteGridVisible(false);
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
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                SelectItem();
            }
        }

        private void SelectItem()
        {
            textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
            textBox1.Select(textBox1.Text.Length, 0);
            //dataGridView1.Visible = false;
            this.AutoCompleteGridVisible(false);

            PrevKeyupString = this.textBox1.Text;

            if (Selected != null)
            {
                Selected(this, null);
            }

            selectItem = true;
        }

        private void MultiLineCombo_Leave(object sender, EventArgs e)
        {
            //dataGridView1.Visible = false;
            this.AutoCompleteGridVisible(false);
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (textBox1.ReadOnly)
                return;

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    selectItem = true;
                    //dataGridView1.Visible = false;
                    this.AutoCompleteGridVisible(false);
                    break;
            }
        }

        private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    selectItem = true;
                    //dataGridView1.Visible = false;
                    this.AutoCompleteGridVisible(false);
                    break;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            SetGrid();
        }
    }
}

