using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Yojitsu.DA;

namespace Yojitsu
{
    public partial class メモ編集閲覧Form : Form
    {
        private readonly List<BgYosanHead> yosanHeads;

        private BgYosanHead selectedYosanHead;

        private Dictionary<string, TextBox> textBoxDic;
        private Dictionary<TextBox, BgYosanMemo> yosanMemoDic;

        private BgYosanMemo editedYosanMemo;
        
       
        public メモ編集閲覧Form(List<BgYosanHead> yosanHeads)
        {
            this.yosanHeads = yosanHeads;
            
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("", "メモ編集閲覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            Init();
        }

        
        private void Init()
        {
            // 年度
            InitComboBox年度();
            // 予算種別
            InitComboBox予算種別();
            // リビジョン
            InitComboBoxリビジョン();

            InitTextBoxsメモ();
            
            LoadData();
        }

        
        private void InitTextBoxsメモ()
        {
            textBoxDic = new Dictionary<string, TextBox>();
            List<MsBumon> bumons = DbAccessorFactory.FACTORY.MsBumon_GetRecords(NBaseCommon.Common.LoginUser);

            Point labelPoint = new Point(24, 234);
            Point textBoxPoint = new Point(67, 231);
            
            foreach (MsBumon b in bumons)
            {
                if (Constants.IncludeHimoku(b.MsBumonID))
                {
                    Label label = new Label();
                    label.Text = b.BumonName;
                    label.Location = labelPoint;
                    label.AutoSize = true;
                    
                    TextBox textBox = new TextBox();
                    
                    textBox.Multiline = true;
                    textBox.Location = textBoxPoint;
                    textBox.Size = new Size(492, 80);
                    textBox.ImeMode = ImeMode.On;
                    textBox.MaxLength = 2000;
                    
                    panel1.Controls.Add(label);
                    panel1.Controls.Add(textBox);

                    labelPoint.Y += 85;
                    textBoxPoint.Y += 85;

                    textBoxDic[b.MsBumonID] = textBox;
                    
                    textBox.TextChanged += new EventHandler(textBox_TextChanged);
                }
            }
        }


        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (yosanMemoDic.Count == 0)
            {
                return;
            }
            
            TextBox textBox = sender as TextBox;
            
            button保存.Enabled = true;
            editedYosanMemo = yosanMemoDic[textBox];
            editedYosanMemo.Memo = textBox.Text;
        }
        
        
        private void LoadData()
        {
            if (textBoxDic == null)
            {
                return;
            }
            
            foreach (BgYosanHead h in yosanHeads)
            {
                if (h.Year == Int32.Parse(comboBox年度.Text) &&
                    BgYosanSbt.ToName(h.YosanSbtID) == comboBox予算種別.Text &&
                    h.Revision == Int32.Parse(comboBoxリビジョン.Text.Split(' ')[0]))
                {
                    selectedYosanHead = h;
                    break;
                }
            }
            
            if (selectedYosanHead != null)
            {
                textBox前提条件.Text = selectedYosanHead.ZenteiJouken;
                textBoxリビジョン備考.Text = selectedYosanHead.RevisionBikou;
                
                List<BgYosanMemo> memos = DbAccessorFactory.FACTORY.BgYosanMemo_GetRecordsByYosanHeadID(NBaseCommon.Common.LoginUser,
                                                                                                        selectedYosanHead.YosanHeadID);

                yosanMemoDic = new Dictionary<TextBox, BgYosanMemo>();

                foreach (BgYosanMemo m in memos)
                {
                    if (Constants.IncludeHimoku(m.MsBumonID))
                    {
                        textBoxDic[m.MsBumonID].TextChanged -= new EventHandler(textBox_TextChanged);
                        textBoxDic[m.MsBumonID].Text = m.Memo;
                        textBoxDic[m.MsBumonID].TextChanged += new EventHandler(textBox_TextChanged);

                        if (Constants.BelongMsBumon(m.MsBumonID) && !selectedYosanHead.IsFixed())
                        {
                            textBoxDic[m.MsBumonID].ReadOnly = false;
                        }
                        else
                        {
                            textBoxDic[m.MsBumonID].ReadOnly = true;
                        }

                        yosanMemoDic[textBoxDic[m.MsBumonID]] = m;
                    }
                }
            }
        }


        private void InitComboBox年度()
        {
            comboBox年度.Items.Clear();

            List<int> years = new List<int>();

            foreach (BgYosanHead h in yosanHeads)
            {
                if (!years.Contains(h.Year))
                {
                    comboBox年度.Items.Add(h.Year);
                    years.Add(h.Year);
                }
            }

            if (comboBox年度.Items.Count > 0)
            {
                comboBox年度.SelectedIndex = 0;
            }
        }


        private void InitComboBox予算種別()
        {
            comboBox予算種別.Items.Clear();

            foreach (BgYosanHead h in yosanHeads)
            {
                if (h.Year == Int32.Parse(comboBox年度.Text))
                {
                    if (!comboBox予算種別.Items.Contains(BgYosanSbt.ToName(h.YosanSbtID)))
                    {
                        comboBox予算種別.Items.Add(BgYosanSbt.ToName(h.YosanSbtID));
                    }
                }
            }

            if (comboBox予算種別.Items.Count > 0)
            {
                comboBox予算種別.SelectedIndex = 0;
            }
        }


        private void InitComboBoxリビジョン()
        {
            comboBoxリビジョン.Items.Clear();

            foreach (BgYosanHead h in yosanHeads)
            {
                if (h.Year == Int32.Parse(comboBox年度.Text) &&
                    BgYosanSbt.ToName(h.YosanSbtID) == comboBox予算種別.Text)
                {
                    string revStr = h.Revision.ToString();

                    if (!h.IsFixed())
                    {
                        revStr += " (未 Fix)";
                    }
                    else
                    {
                        revStr += " (" + h.FixDate.ToString("yyyy/MM/dd") + " Fix)";
                    }

                    comboBoxリビジョン.Items.Add(revStr);
                }
            }


            if (comboBoxリビジョン.Items.Count > 0)
            {
                comboBoxリビジョン.SelectedIndex = 0;
            }
        }
        

        private void button保存_Click(object sender, EventArgs e)
        {
            if (ValidateFields() && MessageBox.Show("メモを保存します。よろしいですか？",
                                "",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (Save())
                {
                    MessageBox.Show("メモを保存しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool Save()
        {
            bool result = DbAccessorFactory.FACTORY.BgYosanMemo_UpdateRecord(NBaseCommon.Common.LoginUser, editedYosanMemo);

            if (!result)
            {
                MessageBox.Show("既にデータが更新されているので保存できませんでした。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                button保存.Enabled = false;
                editedYosanMemo = null;
                return true;
            }
        }

        
        private bool ValidateFields()
        {
            if (editedYosanMemo.Memo.Length > 2000)
            {
                MessageBox.Show("メモは2000文字以内で入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        
        private void button閉じる_Click(object sender, EventArgs e)
        {
            Close();
        }

        
        private void comboBox年度_SelectionChangeCommitted(object sender, EventArgs e)
        {
            InitComboBox予算種別();
            InitComboBoxリビジョン();

            LoadData();
        }

        
        private void comboBox予算種別_SelectionChangeCommitted(object sender, EventArgs e)
        {
            InitComboBoxリビジョン();

            LoadData();
        }

        
        private void comboBoxリビジョン_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        
        private void メモ編集閲覧Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (editedYosanMemo != null)
            {
                DialogResult result = MessageBox.Show("メモが変更されています。保存しますか？",
                                                      "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Save();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
