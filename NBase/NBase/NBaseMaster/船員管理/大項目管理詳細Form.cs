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
    public partial class 大項目管理詳細Form : Form
    {
        private MsSiDaikoumoku daikoumoku;
        private List<MsSiHiyouKamoku> hiyouKamokus;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 大項目管理詳細Form(List<MsSiHiyouKamoku> hiyouKamokus)
            : this(new MsSiDaikoumoku(), hiyouKamokus)
        {
        }


        public 大項目管理詳細Form(MsSiDaikoumoku daikoumoku, List<MsSiHiyouKamoku> hiyouKamokus)
        {
            this.daikoumoku = daikoumoku;
            this.hiyouKamokus = hiyouKamokus;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox_費用科目名();

            if (!daikoumoku.IsNew())
            {
                foreach (MsSiHiyouKamoku m in hiyouKamokus)
                {
                    if (m.MsSiHiyouKamokuID == daikoumoku.MsSiHiyouKamokuID)
                    {
                        comboBox費用科目名.SelectedItem = m;
                        break;
                    }
                }
                
                textBox大項目名.Text = daikoumoku.Name;
            }
            else
            {
                button削除.Enabled = false;
            }

            this.ChangeFlag = false;
        }


        private void InitComboBox_費用科目名()
        {
            comboBox費用科目名.Items.Add(string.Empty);

            foreach (MsSiHiyouKamoku m in hiyouKamokus)
            {
                comboBox費用科目名.Items.Add(m);
            }

            comboBox費用科目名.SelectedIndex = 0;
        }


        private void button更新_Click(object sender, EventArgs e)
        {
            Save(false);
        }


        private void Save(bool dele)
        {
            if (ValidateFields())
            {
                FillInstance();

                if (InsertOrUpdate())
                {
                    if (dele == false)
                    {
                        MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(this, "削除しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    DialogResult = DialogResult.OK;
                    Dispose();
                }
                else
                {
                    if (dele == false)
                    {
                        MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(this, "削除に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private bool ValidateFields()
        {
            if (comboBox費用科目名.SelectedIndex == 0)
            {
                comboBox費用科目名.BackColor = Color.Pink;
                MessageBox.Show("費用科目名を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox費用科目名.BackColor = Color.White;
                return false;
            }
            else if (textBox大項目名.Text.Length == 0)
            {
                textBox大項目名.BackColor = Color.Pink;
                MessageBox.Show("大項目名を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox大項目名.BackColor = Color.White;
                return false;
            }
            else if (textBox大項目名.Text.Length > 20)
            {
                textBox大項目名.BackColor = Color.Pink;
                MessageBox.Show("大項目名は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox大項目名.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            daikoumoku.MsSiHiyouKamokuID = (comboBox費用科目名.SelectedItem as MsSiHiyouKamoku).MsSiHiyouKamokuID;
            daikoumoku.Name = textBox大項目名.Text;
        }


        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsSiDaikoumoku_InsertOrUpdate(NBaseCommon.Common.LoginUser, daikoumoku);
            }

            return result;
        }


        private void button削除_Click(object sender, EventArgs e)
        {
            //使用しているかをチェック
            bool ret = this.CheckDeleteUsing(this.daikoumoku);            
            if (ret == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                daikoumoku.DeleteFlag = 1;
                Save(true);
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

        //
        /// <summary>
        /// 削除時、他で利用されていないかをチェックする
        /// 引数：確認するデータ
        /// 返り値：true→削除していい。　false→使用されいる
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsSiDaikoumoku data)
        {
            //MsSiDaikoumokuは
            //MS_SI_MEISAIとSI_JUNBIKINにつながっている。(ER図曰く)
            //この二つのテーブルをチェックする。
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region SI_JUNBIKIN
                List<SiJunbikin> junlist = new List<SiJunbikin>();

                //関連しているデータを取得
                junlist = serviceClient.SiJunbikin_GetRecordsByMsSiDaikoumokuID(NBaseCommon.Common.LoginUser, data.MsSiDaikoumokuID);

                if (junlist.Count > 0)
                {
                    return false;
                }

                #endregion

                #region MS_SI_MEISAI
                List<MsSiMeisai> melist = new List<MsSiMeisai>();

                melist = serviceClient.MsSiMeisai_GetRecordsByMsSiDaikoumokuID(NBaseCommon.Common.LoginUser, data.MsSiDaikoumokuID);

                if (melist.Count > 0)
                {
                    return false;
                }

                #endregion
            }

            return true;
        }

    }
}
