using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseUtil;
using NBaseData.DS;

namespace Senin
{
    public partial class 履歴詳細Form : Form
    {
        private 船員詳細Panel parentForm;
        private SiRireki rireki;
        private bool isNew;

        public 履歴詳細Form(船員詳細Panel parentForm)
            : this(parentForm, new SiRireki(), true)
        {
        }


        public 履歴詳細Form(船員詳細Panel parentForm, SiRireki rireki, bool isNew)
        {
            this.parentForm = parentForm;
            this.rireki = rireki;
            this.isNew = isNew;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox職名();
            InitFields();
        }


        private void InitFields()
        {
            if (!isNew)
            {
                if (rireki.MsSiShokumeiID != int.MinValue)
                {
                    foreach (MsSiShokumei s in comboBox職名.Items)
                    {
                        if (s.MsSiShokumeiID == rireki.MsSiShokumeiID)
                        {
                            comboBox職名.SelectedItem = s;
                            break;
                        }
                    }
                }

                dateTimePicker年月日.Value = rireki.RirekiDate;
                textBox本給.Text = rireki.Honkyu.ToString();
                textBox月給.Text = rireki.Gekkyu.ToString();
                textBox備考.Text = rireki.Bikou;

                textBox等級.Text = rireki.Tokyu != int.MinValue ? rireki.Tokyu.ToString() : "";
                textBox日額.Text = rireki.Nitigaku != int.MinValue ? rireki.Nitigaku.ToString() : "";
            }
        }


        private void InitComboBox職名()
        {
            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                comboBox職名.Items.Add(s);
            }
        }


        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
        }


        private void Save()
        {
            if (ValidateFields())
            {
                FillInstance();

                //2021/07/29　m.yoshihara 追加　DB更新処理追加
                if (parentForm.InsertOrUpdate_履歴詳細(rireki))
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //2021/07/29 引数無しに変更
                //parentForm.RefreshRefresh履歴(rireki); 
                parentForm.Refresh履歴();
                Dispose();
            }
        }


        private bool ValidateFields()
        {
            if (comboBox職名.SelectedItem == null)
            {
                comboBox職名.BackColor = Color.Pink;
                MessageBox.Show("職名を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox職名.BackColor = Color.White;
                return false;
            }
            else if ((textBox本給.Text.Length != 0 && !NumberUtils.Validate(textBox本給.Text)) || textBox本給.Text.Length > 9)
            {
                textBox本給.BackColor = Color.Pink;
                MessageBox.Show("本給は半角数字9文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox本給.BackColor = Color.White;
                return false;
            }
            else if ((textBox本給.Text.Length != 0 && !NumberUtils.Validate(textBox月給.Text)) || textBox月給.Text.Length > 9)
            {
                textBox月給.BackColor = Color.Pink;
                MessageBox.Show("月額給与は半角数字9文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox月給.BackColor = Color.White;
                return false;
            }
            else if ((textBox等級.Text.Length != 0 && !NumberUtils.Validate(textBox等級.Text)) || textBox等級.Text.Length > 3)
            {
                textBox等級.BackColor = Color.Pink;
                MessageBox.Show("等級は半角数字3文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox等級.BackColor = Color.White;
                return false;
            }
            else if ((textBox日額.Text.Length != 0 && !NumberUtils.Validate(textBox日額.Text)) || textBox日額.Text.Length > 6)
            {
                textBox日額.BackColor = Color.Pink;
                MessageBox.Show("日額は半角数字6文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox日額.BackColor = Color.White;
                return false;
            }
            else if (textBox備考.Text.Length > 500)
            {
                textBox備考.BackColor = Color.Pink;
                MessageBox.Show("備考は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox備考.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            rireki.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            rireki.RirekiDate = dateTimePicker年月日.Value;
            int amount;
            int.TryParse(textBox本給.Text, out amount);
            rireki.Honkyu = amount;
            int.TryParse(textBox月給.Text, out amount);
            rireki.Gekkyu = amount;
            rireki.Bikou = StringUtils.Escape(textBox備考.Text);

            int work;
            int.TryParse(textBox等級.Text, out work);
            rireki.Tokyu = work;
            int.TryParse(textBox日額.Text, out amount);
            rireki.Nitigaku = amount;
            
        }


        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                rireki.DeleteFlag = 1;
                Save();
            }
        }

        
        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
