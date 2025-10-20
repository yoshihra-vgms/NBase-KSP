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

namespace NBaseMaster.船員管理
{
    public partial class 明細科目管理詳細Form : Form
    {
        private MsSiKamoku siKamoku;
        private List<MsKamoku> kamokus;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 明細科目管理詳細Form(List<MsKamoku> kamokus)
            : this(new MsSiKamoku(), kamokus)
        {
        }


        public 明細科目管理詳細Form(MsSiKamoku meisai, List<MsKamoku> kamokus)
        {
            this.siKamoku = meisai;
            this.kamokus = kamokus;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox_費用科目名();

            if (!siKamoku.IsNew())
            {
                foreach (MsKamoku m in kamokus)
                {
                    m.viewUtiwakeKamokuNameFlag = true;
                    if (m.MsKamokuId == siKamoku.MsKamokuId)
                    {
                        comboBox科目名.SelectedItem = m;
                        break;
                    }
                }

                textBox明細科目名.Text = siKamoku.KamokuName;

                checkBox課税.Checked = siKamoku.TaxFlag == (int)MsSiKamoku.税金フラグ.課税;

                if (siKamoku.HiyouKind == (int)MsSiKamoku.費用種別.船員費用)
                {
                    radioButton船員費用.Checked = true;
                }
                else
                {
                    radioButton全社費用.Checked = true;
                }
            }
            else
            {
                button削除.Enabled = false;
            }

            this.ChangeFlag = false;
        }


        private void InitComboBox_費用科目名()
        {
            comboBox科目名.Items.Add(string.Empty);

            foreach (MsKamoku m in kamokus)
            {
                m.viewUtiwakeKamokuNameFlag = true;
                comboBox科目名.Items.Add(m);
            }

            comboBox科目名.SelectedIndex = 0;
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

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Dispose();
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private bool ValidateFields()
        {
            if (textBox明細科目名.Text.Length == 0)
            {
                textBox明細科目名.BackColor = Color.Pink;
                MessageBox.Show("明細科目名を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox明細科目名.BackColor = Color.White;
                return false;
            }
            else if (textBox明細科目名.Text.Length > 30)
            {
                textBox明細科目名.BackColor = Color.Pink;
                MessageBox.Show("明細科目名は30文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox明細科目名.BackColor = Color.White;
                return false;
            }
            else if (!radioButton船員費用.Checked && !radioButton全社費用.Checked)
            {
                radioButton船員費用.BackColor = Color.Pink;
                radioButton全社費用.BackColor = Color.Pink;
                MessageBox.Show("費用種別を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                radioButton船員費用.BackColor = Color.White;
                radioButton全社費用.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            if (comboBox科目名.SelectedIndex > 0)
            {
                siKamoku.MsKamokuId = (comboBox科目名.SelectedItem as MsKamoku).MsKamokuId;
            }
            else
            {
                siKamoku.MsKamokuId = int.MinValue;
            }

            siKamoku.KamokuName = textBox明細科目名.Text;

            if (checkBox課税.Checked)
            {
                siKamoku.TaxFlag = (int)MsSiKamoku.税金フラグ.課税;
            }
            else
            {
                siKamoku.TaxFlag = (int)MsSiKamoku.税金フラグ.非課税;
            }

            if (radioButton船員費用.Checked)
            {
                siKamoku.HiyouKind = (int)MsSiKamoku.費用種別.船員費用;
            }
            else
            {
                siKamoku.HiyouKind = (int)MsSiKamoku.費用種別.全社費用;
            }
        }


        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsSiKamoku_InsertOrUpdate(NBaseCommon.Common.LoginUser, siKamoku);
            }

            return result;
        }

        /// <summary>
        /// 削除チェック
        /// 引数：検索データ
        /// 返り値：削除していいtrue ダメfalse
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsSiKamoku data)
        {
            //リンクは
            //MS_SI_MEISAI
            //SI_JUNBIKIN

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region MsSiMeisai
                List<MsSiMeisai> melist =
                    serviceClient.MsSiMeisai_GetRecordsByMsSiKamokuID(NBaseCommon.Common.LoginUser, data.MsSiKamokuId);

                if (melist.Count > 0)
                {
                    return false;
                }
                #endregion


                #region SiJunbikin
                List<SiJunbikin> julist =
                    serviceClient.SiJunbikin_GetRecordsByMsSIKamokuID(NBaseCommon.Common.LoginUser, data.MsSiKamokuId);

                if( julist.Count > 0)
                {
                    return false;
                }

                #endregion
            }

            return true;
        }

        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {

                //使用しているかどうかをチェックする。
                bool result = this.CheckDeleteUsing(this.siKamoku);
                if (result == false)
                {
                    MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                siKamoku.DeleteFlag = 1;
                Save();
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            //編集中に閉じようとした。
            if (this.ChangeFlag == true)
            {
                DialogResult ret = MessageBox.Show(this, "データが編集されていますが、閉じますか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question);

                if (ret == DialogResult.Cancel)
                {
                    return;
                }
            }
            DialogResult = DialogResult.Cancel;
            Dispose();
        }

        //データを編集した時
        private void DataChange(object sender, EventArgs e)
        {
            this.ChangeFlag = true;
        }
    }
}
