using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeficiencyControl.Util
{
    /// <summary>
    /// AutoComplete付コンボボックス 　　feat.吉原さん
    /// </summary>
    public partial class SingleLineCombo : UserControl
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
                PrevKeyupString = value;
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
                this.Form.Width = value;
                base.Width = value;
                
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
                this.Form.Location = new Point(0, value);
                base.Height = value;

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

        int FormHeight = 150;
        /// <summary>
        /// 選択候補を表示する大きさ
        /// </summary>
        public int ItemSelectHeight
        {
            set
            {
                this.FormHeight = value;
            }
        }

        /// <summary>
        /// Grid画面表示位置の計算
        /// </summary>
        /// <returns></returns>
        private Point CalcuFormPoint()
        {
            Point ans = new Point(0, this.textBox1.Height);

            ans =this.PointToScreen(ans);

            return ans;
        }


        /// <summary>
        /// フォーム表示
        /// </summary>
        /// <param name="f"></param>
        private void ShowGridForm(bool f)
        {
            //閉じる時
            if (f == false)
            {
                this.Form.Hide();
                return;
            }
            //開くとき                        
            this.Form.Location = this.CalcuFormPoint();
            
            this.Form.Width = this.Width;
            this.Form.Height = this.FormHeight;
            

            this.Form.Show();
        }

        /// <summary>
        /// コンボボックスグリッド表示可否設定
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public void AutoCompleteGridVisible(bool f)
        {
            //dataGridView1.Visible = f;
            

            if (f == true)
            {
                base.Height = this.textBox1.Height;

                this.ShowGridForm(f);

            }
            else
            {
                base.Height = this.textBox1.Height;
                this.ShowGridForm(f);
            }
        }

        /// <summary>
        /// アイテムの全クリア
        /// </summary>
        public void Clear()
        {
            this.textBox1.Text = "";
            this.Items.Clear();
            this.AutoCompleteCustomSource.Clear();
        }
        
        public delegate void SelectedEventHandler(object sender, EventArgs e);
        public event SelectedEventHandler selected;

        public event SelectedEventHandler Cleared;

        /// <summary>
        /// グリッドフォーム
        /// </summary>
        private SingleLineComboForm Form = null;

        private bool selectItem;

        public SingleLineCombo()
        {
            InitializeComponent();

            this.Form = new SingleLineComboForm();
            this.Form.SLCombo = this;

            this.Form.dataGridView1.AllowUserToAddRows = false;
            this.Form.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.Form.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.Form.dataGridView1.ColumnHeadersVisible = false;
            this.Form.dataGridView1.MultiSelect = false;
            this.Form.dataGridView1.ReadOnly = true;
            this.Form.dataGridView1.RowHeadersVisible = false;
            this.Form.dataGridView1.Columns.Add("column", null);
            this.Form.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.Form.dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            
            //dataGridView1.Visible = false;
            this.AutoCompleteGridVisible(false);

            this.BackColor = DeficiencyControlColor.SingleLineColorCombo;
        }

        //private static string PrevKeyupString = "";
        private string PrevKeyupString = "";
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //SetGrid();

            ////クリアイベントの発生
            //if (this.textBox1.Text.Length <= 0 && PrevKeyupString.Length != 0)
            //{
            //    if (Cleared != null)
            //    {
            //        Cleared(this, null);
            //    }
            //}

            //PrevKeyupString = this.textBox1.Text;


            if (e.KeyData == Keys.Enter)
            {
                //dataGridView1.Visible = false;
                SetGrid();
                return;

           }
            
            //クリアイベントの発生
            if (this.textBox1.Text.Length <= 0 && PrevKeyupString.Length != 0)
            {
                if (Cleared != null)
                {
                    Cleared(this, null);
                }
                this.AutoCompleteGridVisible(false);
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
                    //if (h.StartsWith(textBox1.Text))
                    if (h.IndexOf(textBox1.Text) >= 0)
                    {
                        object[] obj = { h };
                        AddRow(obj);
                    }
                }
            }

            this.Form.dataGridView1.CurrentCell = null;

            if (this.Form.dataGridView1.Rows.Count > 0)
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
            this.Form.dataGridView1.Rows.Add(row);
        }

        private void ClearRows()
        {
            this.Form.dataGridView1.Rows.Clear();
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

        public void SelectItem()
        {
            if (this.Form.dataGridView1.SelectedCells.Count <= 0)
            {
                return;
            }

            textBox1.Text = this.Form.dataGridView1.SelectedCells[0].Value.ToString();
            textBox1.Select(textBox1.Text.Length, 0);
            //dataGridView1.Visible = false;
            this.AutoCompleteGridVisible(false);

            PrevKeyupString = this.textBox1.Text;

            if (selected != null)
            {
                selected(this, null);
            }

            //selectItem = true;
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
                    //selectItem = true;
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
                    //selectItem = true;
                    //dataGridView1.Visible = false;
                    this.AutoCompleteGridVisible(false);
                    break;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            //SetGrid();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            //this.AutoCompleteGridVisible(false);
        }

        private void SingleLineCombo_Load(object sender, EventArgs e)
        {
            this.AutoCompleteGridVisible(false);
        }
    }
}
