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
    public partial class 船選択Form : Form
    {
        public List<ListItem> CheckedList;

        public 船選択Form(List<ListItem> checkedList)
        {
            CheckedList = checkedList;
            InitializeComponent();
        }

        private void 船選択Form_Load(object sender, EventArgs e)
        {
            MakeDropDownList_船();
        }

        /// <summary>
        /// 船のドロップダウンリストを作成する
        /// </summary>
        private void MakeDropDownList_船()
        {
            if (CheckedList == null)
            {
                return;
            }

            foreach (ListItem listItem in CheckedList)
            {
                checkedListBox_船.Items.Add(listItem, listItem.Checked);
            }
        }

        public List<ListItem> GetCheckedList()
        {
            foreach (ListItem listItem in CheckedList)
            {
                if (checkedListBox_船.CheckedItems.Contains(listItem))
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

        public List<int> CheckedVesselIds()
        {
            List<int> vesselIds = new List<int>();
            foreach (ListItem item in checkedListBox_船.CheckedItems)
            {
                vesselIds.Add(item.Value);
            }
            return vesselIds;
        }

        public List<string> CheckedVesselNames()
        {
            List<string> vesselNames = new List<string>();
            foreach (ListItem item in checkedListBox_船.CheckedItems)
            {
                vesselNames.Add(item.Text);
            }
            return vesselNames;
        }

        public void SetCheckBox()
        {
        }

        private void button選択_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox_船.Items.Count; i++)
            {
                checkedListBox_船.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void button解除_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox_船.Items.Count; i++)
            {
                checkedListBox_船.SetItemCheckState(i, CheckState.Unchecked);
            }
        }


        public class ListItem
        {
            public string Text;
            public int Value;
            public bool Checked;

            public ListItem(string text, int value, bool check)
            {
                Text = text;
                Value = value;
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
