using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;

namespace NBaseCommon
{
    public partial class 複数選択Form : Form
    {
        public string Title;
        public List<ListItem> CheckedList;

        public 複数選択Form(string title, List<ListItem> checkedList)
        {
            Title = title;
            CheckedList = checkedList;
            InitializeComponent();
        }

        private void 複数選択Form_Load(object sender, EventArgs e)
        {
            this.Text = Title;
            MakeCheckedList();
        }

        /// <summary>
        /// チェックドリストを作成する
        /// </summary>
        private void MakeCheckedList()
        {
            if (CheckedList == null)
            {
                return;
            }

            foreach (ListItem listItem in CheckedList)
            {
                checkedListBox.Items.Add(listItem, listItem.Checked);
            }
        }

        public List<ListItem> GetCheckedList()
        {
            foreach (ListItem listItem in CheckedList)
            {
                if (checkedListBox.CheckedItems.Contains(listItem))
                {
                    listItem.Checked = true;
                }
                else
                {
                    listItem.Checked = false;
                }
            }
            return CheckedList;
        }

        //public List<int> CheckedIds()
        //{
        //    List<int> ids = new List<int>();
        //    foreach (ListItem item in checkedListBox.CheckedItems)
        //    {
        //        ids.Add(item.Value);
        //    }
        //    return ids;
        //}

        //public List<string> CheckedIds()
        //{
        //    List<string> ids = new List<string>();
        //    foreach (ListItem item in checkedListBox.CheckedItems)
        //    {
        //        ids.Add(item.StrValue);
        //    }
        //    return ids;
        //}

        public List<string> CheckedNames()
        {
            List<string> names = new List<string>();
            foreach (ListItem item in checkedListBox.CheckedItems)
            {
                names.Add(item.Text);
            }
            return names;
        }

        public void SetCheckBox()
        {
        }

        private void button選択_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                checkedListBox.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void button解除_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                checkedListBox.SetItemCheckState(i, CheckState.Unchecked);
            }
        }


        public class ListItem
        {
            public string Text;
            public int Value;
            public string StrValue;
            public bool Checked;

            public ListItem(string text, int value, bool check)
            {
                Text = text;
                Value = value;
                Checked = check;
            }

            public ListItem(string text, string value, bool check)
            {
                Text = text;
                StrValue = value;
                Checked = check;
            }

            //オーバーライドしたメソッド
            //これがコンボボックスに表示される
            public override string ToString()
            {
                return Text;
            }
        }

    }
}
