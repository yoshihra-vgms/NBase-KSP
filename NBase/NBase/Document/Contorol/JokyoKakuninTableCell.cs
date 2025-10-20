using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Document.Contorol
{
    public partial class JokyoKakuninTableCell : UserControl
    {
        private int Panel高 = 0;
        private const int 最大情報数 = 4;
        private const int 行高 = 17;

        private class KakuninJokyoInfo
        {
            public string 日付;
            public string 氏名;

            public KakuninJokyoInfo(DateTime dt, string name)
            {
                日付 = dt.ToShortDateString();
                氏名 = name;
            }
        }

        private List<KakuninJokyoInfo> kakuninJokyoInfos = new List<KakuninJokyoInfo>();
        public JokyoKakuninTableCell()
        {
            InitializeComponent();

            Panel高 = this.Height;
        }

        public void clearItem()
        {
            kakuninJokyoInfos.Clear();

            flowLayoutPanel_Date.Controls.Clear();
            flowLayoutPanel_Name.Controls.Clear();

            SetHeight();
        }

        public void AddItem(string value)
        {
            Label labelDate = new Label();
            labelDate.Text = value;
            flowLayoutPanel_Date.Controls.Add(labelDate);
           
            SetHeight();
        }

        public void AddItem(DateTime dt, string name)
        {
            KakuninJokyoInfo kakuninJokyoInfo = new KakuninJokyoInfo(dt, name);
            kakuninJokyoInfos.Add(kakuninJokyoInfo);

            Label labelDate = new Label();
            labelDate.Height = 行高;
            labelDate.TextAlign = ContentAlignment.MiddleLeft;
            labelDate.Text = kakuninJokyoInfo.日付;
            flowLayoutPanel_Date.Controls.Add(labelDate);

            Label labelName = new Label();
            labelName.Height = 行高;
            labelName.Width = flowLayoutPanel_Name.Width - 5;
            labelName.TextAlign = ContentAlignment.MiddleLeft;
            labelName.Text = kakuninJokyoInfo.氏名;
            flowLayoutPanel_Name.Controls.Add(labelName);

            SetHeight();
        }

        public void Reflesh()
        {
            flowLayoutPanel_Date.Controls.Clear();
            flowLayoutPanel_Name.Controls.Clear();

            foreach(KakuninJokyoInfo kakuninJokyoInfo in kakuninJokyoInfos)
            {
                Label labelDate = new Label();
                labelDate.Text = kakuninJokyoInfo.日付;
                flowLayoutPanel_Date.Controls.Add(labelDate);

                Label labelName = new Label();
                labelName.Text = kakuninJokyoInfo.氏名;
                flowLayoutPanel_Name.Controls.Add(labelName);
            }
            SetHeight();
        }

        private void SetHeight()
        {
            int rowCount = flowLayoutPanel_Name.Controls.Count;

            if (rowCount > 最大情報数)
            {
                tableLayoutPanel1.Height = 行高 * rowCount + (2 * (rowCount - 1));
            }
            else
            {
                tableLayoutPanel1.Height = Panel高;
            }
        }
    }
}
