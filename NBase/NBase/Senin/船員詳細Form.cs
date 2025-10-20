using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Senin.util;
using LidorSystems.IntegralUI.Lists;
using System.Text.RegularExpressions;
using NBaseUtil;
using NBaseData.DS;
using Microsoft.Office.Interop.Excel;

namespace Senin
{
    public partial class 船員詳細Panel : UserControl　//Form
    {
        private 船員Form parentForm;
        public MsSenin senin;

        //データを編集したかどうか？
        public bool ChangeFlag = false;

        private static List<string> _tabNames = null;
        public static List<string> TabNames
        {
            get
            {
                if (_tabNames == null)
                { 
                    _tabNames = new List<string>();
                    _tabNames.Add("基本情報");
                    _tabNames.Add("連絡先");
                    _tabNames.Add("経歴等");
                    _tabNames.Add("乗下船履歴");
                    _tabNames.Add("免許/免状");
                    _tabNames.Add("家族情報");
                    _tabNames.Add("健康診断");
                    _tabNames.Add("履歴");
                    _tabNames.Add("講習");
                    _tabNames.Add("傷病");
                    _tabNames.Add("CrewMatrix");
                    _tabNames.Add("賞罰");
                    _tabNames.Add("特記事項");
                    _tabNames.Add("基本給");
                }
                return _tabNames;
            }
        }

        private bool tabChangeEventCalcel = false;

        private Dictionary<string, string> tabPageDic = new Dictionary<string, string>();

        //2021/08/03 m.yoshihara 追加　現在使わないタブ
        private static List<string> _removeTabNames = null;
        public static List<string> RemoveTabNames
        {
            get
            {
                if (_removeTabNames == null)
                {
                    _removeTabNames = new List<string>();
                    _removeTabNames.Add("非表示");
                    _removeTabNames.Add("アカウント");
                }
                return _removeTabNames;
            }
        }

        /// <summary>
        /// タブをクリックしたかどうかチェックするテーブル
        /// </summary>
        private Dictionary<string, bool> tabClicked = new Dictionary<string, bool>();

        private string SelectedTabName = "";

        public void SelectTab(string name, bool eventCancel = false)
        {
            tabChangeEventCalcel = eventCancel;

            //タブ選択
            tabControl1.SelectedTab = tabControl1.TabPages[tabPageDic[name]];

            tabChangeEventCalcel = false;

            //情報取得、表示
            ChangeTab_Search();
        }

        private string _AddPictureErrMsg = "";

        public 船員詳細Panel(船員Form parentForm)
            : this(parentForm, new MsSenin())
        {
        }


        public 船員詳細Panel(船員Form parentForm, MsSenin senin)
        {
            InitializeComponent();

            this.parentForm = parentForm;
            this.senin = senin;
            Init();
        }

        public void SetSenin(MsSenin senin)
        {
            if (senin == null)
            {
                senin = new MsSenin();
            }
            this.senin = senin;

            ClearFields();
            Clear住所();
            Clear船員カード();
            Clear免状免許();
            Clear家族情報();
            Clear講習();
            Clear傷病();
            Clear既往歴();
            Clear健康診断();
            Clear賞罰();
            Clear特記事項();
            Clear基本給();
            //Clear履歴();
            //ClearCrewMatrix();


            InitFields();

            Search住所();
            Init住所();


            tabClicked = new Dictionary<string, bool>();
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                tabClicked.Add(tabPage.Name, false);
            }
            SelectedTabName = _tabNames[0];
        }


        private void Init()
        {
            InitComboBox所属会社();
            InitComboBox所属();
            InitComboBox職名();
            InitComboBox血液型();
            InitComboBox入社区分();
            InitComboBox加入組合();
            InitComboBox保険区分();

            InitFields();

            Search住所();
            Init住所();

            #region コメントアウト タブをクリックしたときに検索するように変更 2021/08/03 m.yoshihara
            //treeListViewDelegate船員カード = new TreeListViewDelegate船員カード(treeListView船員カード);
            //treeListViewDelegate免状免許 = new TreeListViewDelegate免状免許(treeListView免状免許);
            //treeListViewDelegate家族情報 = new TreeListViewDelegate家族情報(treeListView家族情報);
            //treeListViewDelegate履歴 = new TreeListViewDelegate履歴(treeListView履歴);
            //treeListViewDelegate講習 = new TreeListViewDelegate講習(treeListView講習);


            //Search船員カード();
            //Search免状免許();
            //Search家族情報();
            //Search履歴();
            //Search講習();

            ////// 2012.3「アカウント」タブは非表示
            ////tabControl1.TabPages.RemoveAt(7);

            ////// 2018.2「口座」タブは非表示
            ////tabControl1.TabPages.RemoveAt(3);

            //treeListViewDelegate傷病 = new TreeListViewDelegate傷病(treeListView傷病);
            //Search傷病();

            //treeListViewDelegateCrewMatrix = new TreeListViewDelegateCrewMatrix(treeListViewCrewMatrix);
            //SearchCrewMatrix();

            //treeListViewDelegate健康診断 = new TreeListViewDelegate健康診断(treeListView健康診断);
            //Search健康診断();
            #endregion

            //2021/08/03 m.yoshihara 使わないタブを消す
            foreach (string reTabname in RemoveTabNames)
            {
                tabControl1.TabPages.RemoveByKey(reTabname);
            }

            foreach (string tabname in TabNames)
            {
                tabPageDic.Add(tabname, tabname.Replace("/",""));
            }

            //2021/08/03 m.yoshihara テーブル初期化
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                tabClicked.Add(tabPage.Name, false);
            }
            SelectedTabName = _tabNames[0];
        }

        private void InitFields()
        {
            BackColor = Color.White;

            if (!senin.IsNew())
            {

                textBox氏名コード.Text = senin.ShimeiCode.Trim();
                if (senin.Kubun == 1) radioButton派遣.Checked = true;

                textBox姓.Text = senin.Sei;
                textBox名.Text = senin.Mei;
                textBox姓カナ.Text = senin.SeiKana;
                textBox名カナ.Text = senin.MeiKana;
                textBox姓かな.Text = senin.SeiHiragana;
                textBox名かな.Text = senin.MeiHiragana;

                if (senin.MsSeninCompanyID != null && senin.MsSeninCompanyID.Length > 0)
                {
                    foreach (MsSeninCompany s in comboBox所属会社.Items)
                    {
                        if (s.MsSeninCompanyID == senin.MsSeninCompanyID)
                        {
                            comboBox所属会社.SelectedItem = s;
                            break;
                        }
                    }
                }

                if (senin.Department != null && senin.Department.Length > 0)
                {
                    foreach (MsSiOptions s in comboBox所属.Items)
                    {
                        if (s.MsSiOptionsID == senin.Department)
                        {
                            comboBox所属.SelectedItem = s;
                            break;
                        }
                    }
                }

                if (senin.MemberOf == 1) radioButton内航籍.Checked = true;

                if (senin.MsSiShokumeiID != int.MinValue)
                {
                    foreach (MsSiShokumei s in comboBox職名.Items)
                    {
                        if (s.MsSiShokumeiID == senin.MsSiShokumeiID)
                        {
                            comboBox職名.SelectedItem = s;
                            break;
                        }
                    }
                }



                if (senin.Birthday != DateTime.MinValue)
                {
                    maskedTextBox生年月日.Text = senin.Birthday.ToShortDateString();
                    textBox年齢.Text = DateTimeUtils.年齢計算(senin.Birthday).ToString();
                }

                if (senin.Sex == 1) radioButton女.Checked = true;

                if (senin.BloodType != null && senin.BloodType.Length > 0)
                { 
                    foreach (MsSiOptions s in comboBox血液型.Items)
                    {
                        if (s.MsSiOptionsID == senin.BloodType)
                        {
                            comboBox血液型.SelectedItem = s;
                            break;
                        }
                    }
                }

                if (senin.NyuushaDate != DateTime.MinValue)
                {
                    maskedTextBox入社年月日.Text = senin.NyuushaDate.ToShortDateString();
                }
                if (senin.NyuushaCategory != null && senin.NyuushaCategory.Length > 0)
                {
                    foreach (MsSiOptions s in comboBox入社区分.Items)
                    {
                        if (s.MsSiOptionsID == senin.NyuushaCategory)
                        {
                            comboBox入社区分.SelectedItem = s;
                            break;
                        }
                    }
                }


                if (senin.SeamenStartDate != DateTime.MinValue)
                {
                    maskedTextBox船員開始年月日.Text = senin.SeamenStartDate.ToShortDateString();
                }
                if (senin.RetireDate != DateTime.MinValue)
                {
                    maskedTextBox退職年月日.Text = senin.RetireDate.ToShortDateString();
                }

                if (senin.Partnership != null && senin.Partnership.Length > 0)
                {
                    foreach (MsSiOptions s in comboBox加入組合.Items)
                    {
                        if (s.MsSiOptionsID == senin.Partnership)
                        {
                            comboBox加入組合.SelectedItem = s;
                            break;
                        }
                    }
                }
                if (senin.InsuranceCategory != null && senin.InsuranceCategory.Length > 0)
                {
                    foreach (MsSiOptions s in comboBox保険区分.Items)
                    {
                        if (s.MsSiOptionsID == senin.InsuranceCategory)
                        {
                            comboBox保険区分.SelectedItem = s;
                            break;
                        }
                    }
                }


                textBox保険番号.Text = senin.HokenNo.Trim();
                textBox年金番号.Text = senin.NenkinNo.Trim();

                textBox保険等級.Text = senin.InsuranceGrade > 0 ? senin.InsuranceGrade.ToString() : "";
                textBox厚生年金等級.Text = senin.PensionGrade > 0 ? senin.PensionGrade.ToString() : "";

                if (senin.Picture != null && senin.Picture.Length != 0)
                {
                    ImageConverter conv = new ImageConverter();
                    Image img = (Image)conv.ConvertFrom(senin.Picture);

                    pictureBox1.Image = img;
                    label写真更新日.Text = senin.PictureDate.ToShortDateString();
                }

                textBoxその他.Text = senin.Sonota;



                // 連絡先タブ
                maskedTextBoxTEL.Text = senin.Tel.Trim();
                maskedTextBoxFAX.Text = senin.Fax.Trim();
                maskedTextBox携帯.Text = senin.Keitai.Trim();
                textBoxメール.Text = senin.Mail.Trim();

                // アカウントタブ
                //textBoxユーザID.Text = senin.LoginID;
                //textBoxパスワード.Text = senin.Password;



                if (senin.RetireFlag == 0)
                {
                    button退職.Enabled = true;
                }
                else
                {
                    button退職.Enabled = false;
                    BackColor = Color.LightPink;
                }

            }

            dateTimePicker船員カード開始.Value = DateTimeUtils.年度開始日(DateTime.Now);
            dateTimePicker船員カード終了.Value = DateTimeUtils.年度終了日(DateTime.Now);


            this.ChangeFlag = false;
        }


        private void InitComboBox職名()
        {
            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                comboBox職名.Items.Add(s);
            }
        }

        private void InitComboBox所属会社()
        {
            foreach (MsSeninCompany s in SeninTableCache.instance().GetMsSeninCompanyList(NBaseCommon.Common.LoginUser))
            {
                comboBox所属会社.Items.Add(s);
            }
        }
        private void InitComboBox所属()
        {
            foreach (MsSiOptions s in SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.基本_所属))
            {
                comboBox所属.Items.Add(s);
            }
        }
        private void InitComboBox加入組合()
        {
            foreach (MsSiOptions s in SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.基本_加入組合))
            {
                comboBox加入組合.Items.Add(s);
            }
        }
        private void InitComboBox保険区分()
        {
            foreach (MsSiOptions s in SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.基本_保険区分))
            {
                comboBox保険区分.Items.Add(s);
            }
        }
        private void InitComboBox入社区分()
        {
            foreach (MsSiOptions s in SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.基本_入社区分))
            {
                comboBox入社区分.Items.Add(s);
            }
        }
        private void InitComboBox血液型()
        {
            foreach (MsSiOptions s in SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.血液型))
            {
                comboBox血液型.Items.Add(s);
            }
        }

        private void button写真更新_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                label写真更新日.Text = DateTime.Now.ToShortDateString();

                this.ChangeFlag = true;
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
        }


        private void Save(MsSeninAddress seninAddress = null)
        {
            if (ValidateFields())
            {
                FillInstance();

                if (InsertOrUpdate(senin, seninAddress))
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    InitFields();

                    parentForm.詳細更新(senin, seninAddress);
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        #region Validation
        private bool ValidateFields()
        {
            if (!Validateメイン画面()) return false;
            if (!Validate連絡先()) return false;
            //if (!Validateアカウント()) return false;

            return true;
        }


        private bool Validateメイン画面()
        {
            if (textBox氏名コード.Text.Length == 0)
            {
                textBox氏名コード.BackColor = Color.Pink;
                MessageBox.Show("従業員番号を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox氏名コード.BackColor = Color.White;
                return false;
            }
            else if (!StringUtils.isHankaku(textBox氏名コード.Text) || textBox氏名コード.Text.Length > 10)
            {
                textBox氏名コード.BackColor = Color.Pink;
                MessageBox.Show("従業員番号は半角10文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox氏名コード.BackColor = Color.White;
                return false;
            }


            if (textBox姓.Text.Length == 0)
            {
                textBox姓.BackColor = Color.Pink;
                MessageBox.Show("氏名（漢字）姓を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox姓.BackColor = Color.White;
                return false;
            }
            else if (textBox姓.Text.Length > 20)
            {
                textBox姓.BackColor = Color.Pink;
                MessageBox.Show("氏名（漢字）姓は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox姓.BackColor = Color.White;
                return false;
            }
            else if (textBox名.Text.Length > 20)
            {
                textBox名.BackColor = Color.Pink;
                MessageBox.Show("氏名（漢字）名は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox名.BackColor = Color.White;
                return false;
            }

            if (textBox姓カナ.Text.Length == 0)
            {
                textBox姓カナ.BackColor = Color.Pink;
                MessageBox.Show("氏名（カナ）姓を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox姓カナ.BackColor = Color.White;
                return false;
            }
            else if (textBox姓カナ.Text.Length > 20)
            {
                textBox姓カナ.BackColor = Color.Pink;
                MessageBox.Show("氏名（カナ）姓は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox姓カナ.BackColor = Color.White;
                return false;
            }
            else if (textBox名カナ.Text.Length > 20)
            {
                textBox名カナ.BackColor = Color.Pink;
                MessageBox.Show("氏名（カナ）名は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox名カナ.BackColor = Color.White;
                return false;
            }

            if (textBox姓かな.Text.Length == 0)
            {
                textBox姓かな.BackColor = Color.Pink;
                MessageBox.Show("氏名（かな）姓を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox姓かな.BackColor = Color.White;
                return false;
            }
            else if (textBox姓かな.Text.Length > 20)
            {
                textBox姓かな.BackColor = Color.Pink;
                MessageBox.Show("氏名（かな）姓は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox姓かな.BackColor = Color.White;
                return false;
            }
            else if (textBox名かな.Text.Length > 20)
            {
                textBox名かな.BackColor = Color.Pink;
                MessageBox.Show("氏名（かな）名は20文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox名かな.BackColor = Color.White;
                return false;
            }

            if (comboBox所属会社.SelectedItem == null)
            {
                comboBox所属会社.BackColor = Color.Pink;
                MessageBox.Show("所属を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox所属会社.BackColor = Color.White;
                return false;
            }
            //if (comboBox所属.SelectedItem == null)
            //{
            //    comboBox所属.BackColor = Color.Pink;
            //    MessageBox.Show("所属を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    comboBox所属.BackColor = Color.White;
            //    return false;
            //}

            if (comboBox職名.SelectedItem == null)
            {
                comboBox職名.BackColor = Color.Pink;
                MessageBox.Show("職名を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox職名.BackColor = Color.White;
                return false;
            }


            if (!DateTimeUtils.Empty(maskedTextBox生年月日.Text) && !DateTimeUtils.Validate(maskedTextBox生年月日.Text))
            {
                maskedTextBox生年月日.BackColor = Color.Pink;
                MessageBox.Show("生年月日を正しく入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBox生年月日.BackColor = Color.White;
                return false;
            }

            if (!DateTimeUtils.Empty(maskedTextBox入社年月日.Text) && !DateTimeUtils.Validate(maskedTextBox入社年月日.Text))
            {
                maskedTextBox入社年月日.BackColor = Color.Pink;
                MessageBox.Show("入社年月日を正しく入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBox入社年月日.BackColor = Color.White;
                return false;
            }
            if (!DateTimeUtils.Empty(maskedTextBox船員開始年月日.Text) && !DateTimeUtils.Validate(maskedTextBox船員開始年月日.Text))
            {
                maskedTextBox船員開始年月日.BackColor = Color.Pink;
                MessageBox.Show("船員開始年月日を正しく入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBox船員開始年月日.BackColor = Color.White;
                return false;
            }
            if (!DateTimeUtils.Empty(maskedTextBox退職年月日.Text) && !DateTimeUtils.Validate(maskedTextBox退職年月日.Text))
            {
                maskedTextBox退職年月日.BackColor = Color.Pink;
                MessageBox.Show("退職年月日を正しく入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBox退職年月日.BackColor = Color.White;
                return false;
            }


            if (!StringUtils.isHankaku(textBox保険番号.Text) || textBox保険番号.Text.Length > 10)
            {
                textBox保険番号.BackColor = Color.Pink;
                MessageBox.Show("保険番号は半角10文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox保険番号.BackColor = Color.White;
                return false;
            }

            if (!StringUtils.isHankaku(textBox年金番号.Text) || textBox年金番号.Text.Length > 25)
            {
                textBox年金番号.BackColor = Color.Pink;
                MessageBox.Show("年金番号は半角25文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox年金番号.BackColor = Color.White;
                return false;
            }


            if (textBox保険等級.Text.Length > 0)
            {
                int val = 0;
                try
                {
                    val = int.Parse(textBox保険等級.Text);
                }
                catch
                {
                    textBox保険等級.BackColor = Color.Pink;
                    MessageBox.Show("保険等級は数字2文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox保険等級.BackColor = Color.White;
                    return false;
                }
            }

            if (textBox厚生年金等級.Text.Length > 0)
            {
                int val = 0;
                try
                {
                    val = int.Parse(textBox厚生年金等級.Text);
                }
                catch
                {
                    textBox保険等級.BackColor = Color.Pink;
                    MessageBox.Show("厚生年金等級は数字2文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox保険等級.BackColor = Color.White;
                    return false;
                }
            }





            else if (textBoxその他.Text.Length > 500)
            {
                textBoxその他.BackColor = Color.Pink;
                MessageBox.Show("その他必須事項は500文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxその他.BackColor = Color.White;
                return false;
            }
            else if (((byte[])new ImageConverter().ConvertTo(pictureBox1.Image, typeof(byte[]))).Length > ORMapping.Common.MAX_BINARY_SIZE)
            {
                MessageBox.Show("登録できる写真のサイズは " + (ORMapping.Common.MAX_BINARY_SIZE / 1000) + " KB以下です", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            return true;
        }

        private bool Validate連絡先()
        {
            if (!StringUtils.isHankaku(maskedTextBoxTEL.Text) || maskedTextBoxTEL.Text.Length > 25)
            {
                maskedTextBoxTEL.BackColor = Color.Pink;
                MessageBox.Show("TELは半角25文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBoxTEL.BackColor = Color.White;
                return false;
            }
            else if (!StringUtils.isHankaku(maskedTextBoxFAX.Text) || maskedTextBoxFAX.Text.Length > 25)
            {
                maskedTextBoxFAX.BackColor = Color.Pink;
                MessageBox.Show("FAXは半角25文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBoxFAX.BackColor = Color.White;
                return false;
            }
            else if (!StringUtils.isHankaku(maskedTextBox携帯.Text) || maskedTextBox携帯.Text.Length > 25)
            {
                maskedTextBox携帯.BackColor = Color.Pink;
                MessageBox.Show("携帯電話は半角25文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maskedTextBox携帯.BackColor = Color.White;
                return false;
            }
            else if (!StringUtils.isHankaku(textBoxメール.Text) || textBoxメール.Text.Length > 50)
            {
                textBoxメール.BackColor = Color.Pink;
                MessageBox.Show("メールは半角50文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxメール.BackColor = Color.White;
                return false;
            }


            //else if (!StringUtils.isHankaku(maskedTextBox郵便番号_現住所.Text) || maskedTextBox郵便番号_現住所.Text.Length > 8)
            //{
            //    maskedTextBox郵便番号_現住所.BackColor = Color.Pink;
            //    MessageBox.Show("現住所：郵便番号は半角8文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    maskedTextBox郵便番号_現住所.BackColor = Color.White;
            //    return false;
            //}
            //else if (textBox市区町村_現住所.Text.Length > 50)
            //{
            //    textBox市区町村_現住所.BackColor = Color.Pink;
            //    MessageBox.Show("現住所は50文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    textBox市区町村_現住所.BackColor = Color.White;
            //    return false;
            //}
            //else if (textBox番地_現住所.Text.Length > 50)
            //{
            //    textBox番地_現住所.BackColor = Color.Pink;
            //    MessageBox.Show("現住所：番地・建物は100文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    textBox番地_現住所.BackColor = Color.White;
            //    return false;
            //}


            return true;
        }


        #region アカウントタブは未使用

        //private bool Validateアカウント()
        //{
        //    if (textBoxユーザID.Text.Length == 0)
        //    {
        //        tabControl1.SelectedTab = アカウント;
        //        textBoxユーザID.BackColor = Color.Pink;
        //        MessageBox.Show("アカウントタブのユーザIDを入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        textBoxユーザID.BackColor = Color.White;
        //        return false;
        //    }
        //    else if (!StringUtils.isHankaku(textBoxユーザID.Text) || textBoxユーザID.Text.Length > 30)
        //    {
        //        tabControl1.SelectedTab = アカウント;
        //        textBoxユーザID.BackColor = Color.Pink;
        //        MessageBox.Show("アカウントタブのユーザIDは半角30文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        textBoxユーザID.BackColor = Color.White;
        //        return false;
        //    }
        //    else if (textBoxパスワード.Text.Length == 0)
        //    {
        //        tabControl1.SelectedTab = アカウント;
        //        textBoxパスワード.BackColor = Color.Pink;
        //        MessageBox.Show("アカウントタブのパスワードを入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        textBoxパスワード.BackColor = Color.White;
        //        return false;
        //    }
        //    else if (!StringUtils.isHankaku(textBoxパスワード.Text) || textBoxパスワード.Text.Length > 30)
        //    {
        //        tabControl1.SelectedTab = アカウント;
        //        textBoxパスワード.BackColor = Color.Pink;
        //        MessageBox.Show("アカウントタブのパスワードは半角30文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        textBoxパスワード.BackColor = Color.White;
        //        return false;
        //    }
        //    else if (senin.IsNew() && Is_重複_ログインID())
        //    {
        //        tabControl1.SelectedTab = アカウント;
        //        textBoxユーザID.BackColor = Color.Pink;
        //        MessageBox.Show("既に登録されているユーザIDです", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        textBoxユーザID.BackColor = Color.White;
        //        return false;
        //    }

        //    return true;
        //}


        //private bool Is_重複_ログインID()
        //{
        //    MsUser user = null;

        //    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
        //    {
        //        user = serviceClient.MsUser_GetRecordsByLoginID(NBaseCommon.Common.LoginUser, textBoxユーザID.Text.Trim());
        //    }

        //    return user != null;
        //}

        #endregion


        #endregion




        private void ClearFields()
        {
            textBox氏名コード.Text = null;

            radioButton社員.Checked = true;

            textBox姓.Text = null;
            textBox名.Text = null;
            textBox姓カナ.Text = null;
            textBox名カナ.Text = null;
            textBox姓かな.Text = null;
            textBox名かな.Text = null;

            comboBox所属会社.SelectedItem = null;
            comboBox所属.SelectedItem = null;
            radioButtonフェリー籍.Checked = true;
            comboBox職名.SelectedItem = null;
            maskedTextBox生年月日.Text = null;
            textBox年齢.Text = null;
            radioButton男.Checked = true;
            comboBox血液型.SelectedItem = null;

            maskedTextBox入社年月日.Text = null;
            comboBox入社区分.SelectedItem = null;
            maskedTextBox船員開始年月日.Text = null;
            maskedTextBox退職年月日.Text = null;


            comboBox加入組合.SelectedItem = null;
            comboBox保険区分.SelectedItem = null;
            textBox保険番号.Text = null;
            textBox年金番号.Text = null;
            textBox保険等級.Text = null;
            textBox厚生年金等級.Text = null; 

            textBoxその他.Text = null;

            pictureBox1.Image = null;
            label写真更新日.Text = "----/--/--";



            // 連絡先タブ
            maskedTextBoxTEL.Text = null;
            maskedTextBoxFAX.Text = null;
            maskedTextBox携帯.Text = null;
            textBoxメール.Text = null;


            // アカウントタブ
            //textBoxユーザID.Text = null;
            //textBoxパスワード.Text = null;
        }

        private void FillInstance()
        {
            senin.ShimeiCode = textBox氏名コード.Text;

            senin.Kubun = radioButton社員.Checked ? 0 : 1;

            senin.Sei = textBox姓.Text;
            senin.Mei = textBox名.Text;
            senin.SeiKana = textBox姓カナ.Text;
            senin.MeiKana = textBox名カナ.Text;
            senin.SeiHiragana = textBox姓かな.Text;
            senin.MeiHiragana = textBox名かな.Text;

            senin.MsSeninCompanyID = (comboBox所属会社.SelectedItem as MsSeninCompany).MsSeninCompanyID;

            // 山友Verは、所属（部門）は管理しない
            //senin.Department = (comboBox所属.SelectedItem as MsSiOptions).MsSiOptionsID;

            // 山友Verは、籍は管理しない
            //senin.MemberOf = radioButtonフェリー籍.Checked ? 0 : 1;

            senin.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;

            if(DateTimeUtils.Empty(maskedTextBox生年月日.Text))
            {
                senin.Birthday = DateTime.MinValue;
            }
            else
            {
                senin.Birthday = DateTime.Parse(maskedTextBox生年月日.Text);
            }

            senin.Sex = radioButton男.Checked ? 0 : 1;
            senin.BloodType = (comboBox血液型.SelectedItem is MsSiOptions) ? (comboBox血液型.SelectedItem as MsSiOptions).MsSiOptionsID : null;

            if (DateTimeUtils.Empty(maskedTextBox入社年月日.Text))
            {
                senin.NyuushaDate = DateTime.MinValue;
            }
            else
            {
                senin.NyuushaDate = DateTime.Parse(maskedTextBox入社年月日.Text);
            }
            senin.NyuushaCategory = (comboBox入社区分.SelectedItem is MsSiOptions) ? (comboBox入社区分.SelectedItem as MsSiOptions).MsSiOptionsID : null;
            
            if (DateTimeUtils.Empty(maskedTextBox船員開始年月日.Text))
            {
                senin.SeamenStartDate = DateTime.MinValue;
            }
            else
            {
                senin.SeamenStartDate = DateTime.Parse(maskedTextBox船員開始年月日.Text);
            }

            if (DateTimeUtils.Empty(maskedTextBox退職年月日.Text))  // 退職日がクリアされている場合、退職されていれば、復職とする
            {
                senin.RetireFlag = 0;
                senin.RetireDate = DateTime.MinValue;
            }
            else
            {
                senin.RetireDate = DateTime.Parse(maskedTextBox退職年月日.Text);
            }

            senin.Partnership = (comboBox加入組合.SelectedItem is MsSiOptions) ? (comboBox加入組合.SelectedItem as MsSiOptions).MsSiOptionsID : null;
            senin.InsuranceCategory = (comboBox保険区分.SelectedItem is MsSiOptions) ? (comboBox保険区分.SelectedItem as MsSiOptions).MsSiOptionsID : null;

            senin.HokenNo = textBox保険番号.Text;
            senin.NenkinNo = textBox年金番号.Text;

            senin.InsuranceGrade = textBox保険等級.Text.Length > 0 ? int.Parse(textBox保険等級.Text) : 0;
            senin.PensionGrade = textBox厚生年金等級.Text.Length > 0 ? int.Parse(textBox厚生年金等級.Text) : 0;



            ImageConverter conv = new ImageConverter();
            byte[] imgArr = (byte[])conv.ConvertTo(pictureBox1.Image, typeof(byte[]));

            senin.Picture = imgArr;

            if (DateTimeUtils.Validate(label写真更新日.Text))
            {
                senin.PictureDate = DateTime.Parse(label写真更新日.Text);
            }


            senin.Sonota = textBoxその他.Text;


            // 連絡先タブ
            senin.Tel = maskedTextBoxTEL.Text;
            senin.Fax = maskedTextBoxFAX.Text;
            senin.Keitai = maskedTextBox携帯.Text;
            senin.Mail = textBoxメール.Text;


            //// アカウントタブ
            //senin.LoginID = textBoxユーザID.Text.Trim();
            //senin.Password = textBoxパスワード.Text.Trim();
        }




        #region 更新処理メソッド

        public bool InsertOrUpdate_免許免状(SiMenjou mj)
        {
            int msSeninId = -1;
            List<SiMenjou> mjlist = new List<SiMenjou>();
            mjlist.Add(mj);

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msSeninId = serviceClient.BLC_船員登録(NBaseCommon.Common.LoginUser, senin, null, null, mjlist, null, null, null, null, null);
            }

            if (msSeninId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertOrUpdate_家族情報( SiKazoku kz)
        {
            int msSeninId = -1;
            List<SiKazoku> kzlist = new List<SiKazoku>();
            kzlist.Add(kz);

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msSeninId = serviceClient.BLC_船員登録(NBaseCommon.Common.LoginUser, senin, null, null, null, kzlist, null, null, null, null);
            }

            if (msSeninId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertOrUpdate_傷病詳細(SiShobyo sb)
        {
            int msSeninId = -1;
            List<SiShobyo> sblist = new List<SiShobyo>();
            sblist.Add(sb);

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msSeninId = serviceClient.BLC_船員登録(NBaseCommon.Common.LoginUser, senin, null, null, null, null, null, sblist, null, null);
            }

            if (msSeninId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertOrUpdate_賞罰詳細(SiShobatsu sb)
        {
            int msSeninId = -1;
            List<SiShobatsu> sblist = new List<SiShobatsu>();
            sblist.Add(sb);

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msSeninId = serviceClient.BLC_船員登録(NBaseCommon.Common.LoginUser, senin, null, null, null, null, null, null, null, sblist);
            }

            if (msSeninId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool InsertOrUpdate_健康診断(SiKenshin ks)
        {
            int msSeninId = -1;
            List<SiKenshin> kslist = new List<SiKenshin>();
            kslist.Add(ks);

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msSeninId = serviceClient.BLC_船員登録(NBaseCommon.Common.LoginUser, senin, null, null, null, null, null, null, kslist, null);
            }

            if (msSeninId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
 
        public bool InsertOrUpdate_乗船履歴(SiCard cd, SiCard newCd = null)
        {
            int msSeninId = -1;
            List<SiCard> cdlist = new List<SiCard>();
            cdlist.Add(cd);

            if (newCd != null)
                cdlist.Add(newCd);

            BuildLinkShokumei(ref cdlist);

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msSeninId = serviceClient.BLC_船員登録(NBaseCommon.Common.LoginUser, senin, null, cdlist, null, null, null, null, null, null);
            }

            if (msSeninId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertOrUpdate_履歴詳細(SiRireki rk)
        {
            int msSeninId = -1;
            List<SiRireki> rklist = new List<SiRireki>();
            rklist.Add(rk);

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msSeninId = serviceClient.BLC_船員登録(NBaseCommon.Common.LoginUser, senin, null, null, null, null, rklist, null, null, null);
            }

            if (msSeninId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertOrUpdate_基本給(SiSalary s)
        {
            int msSeninId = -1;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msSeninId = serviceClient.BLC_船員基本給登録(NBaseCommon.Common.LoginUser, senin, s);
            }

            if (msSeninId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool InsertOrUpdate_既往歴(SiKenshinPmhKa s)
        {
            int msSeninId = -1;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msSeninId = serviceClient.BLC_船員既往歴登録(NBaseCommon.Common.LoginUser, senin, s);
            }

            if (msSeninId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertOrUpdate_特記事項詳細(SiRemarks r)
        {
            int msSeninId = -1;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msSeninId = serviceClient.BLC_船員特記事項登録(NBaseCommon.Common.LoginUser, senin, r);
            }

            if (msSeninId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool InsertOrUpdate_経歴等(MsSeninCareer c, MsSeninEtc e)
        {
            bool ret = true;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.MsSeninCareer_InsertOrUpdate(NBaseCommon.Common.LoginUser, senin.MsSeninID, seninCareer);
                if (ret)
                    ret = serviceClient.MsSeninEtc_InsertOrUpdate(NBaseCommon.Common.LoginUser, senin.MsSeninID, seninEtc);
            }

            return ret;
        }

        private bool InsertOrUpdate(MsSenin s, MsSeninAddress sa)
        {
            int msSeninId = -1;
            bool ret = true;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msSeninId = serviceClient.BLC_船員登録(NBaseCommon.Common.LoginUser, s, sa, null, null, null, null, null, null, null);
                if (msSeninId > 0)
                {
                    senin = serviceClient.MsSenin_GetRecord(NBaseCommon.Common.LoginUser, msSeninId);
                    seninAddress = serviceClient.MsSeninAddress_GetRecordsByMsSeninID(NBaseCommon.Common.LoginUser, msSeninId);

                    MsSeninFilter filter = parentForm.CreateMsSeninFilter();
                    if (senin.RetireFlag == 1)
                    {
                        filter.RetireFlag = (int)MsSeninFilter.enumRetire.INCLUDE;
                    }
                    filter.SeninID = msSeninId;
                    var results = serviceClient.BLC_船員検索(NBaseCommon.Common.LoginUser, filter);
                    if (results != null && results.Count() > 0)
                    {
                        senin.MsSiShubetsuID = results.First().MsSiShubetsuID; // BLC_船員検索のSQLで取得している分
                        senin.MsVesselID = results.First().MsVesselID;         // BLC_船員検索のSQLで取得している分
                        senin.StartDate = results.First().StartDate;           // BLC_船員検索のSQLで取得している分
                        senin.EndDate = results.First().EndDate;               // BLC_船員検索のSQLで取得している分
                        senin.合計日数 = results.First().合計日数;             // BLC_船員検索で付加している分
                    }

                }
                else
                {
                    ret= false;
                }
            }
            return ret;

        }

        #endregion


        private List<SiCard> BuildSaveCards()
        {
            List<SiCard> cards = new List<SiCard>();

            foreach (SiCard c in newCards.Values)
            {
                c.SiCardID = null;
                cards.Add(c);
            }

            foreach (SiCard c in editedCards.Values)
            {
                cards.Add(c);
            }

            #region コメントアウト BuildLinkShokumei()に移動 2021/07/30  m.yoshihara
            //// 船員カードが乗船以外のときは、船員の職名を船員カードに登録する。
            //// 配乗表の表示時に必要。
            //foreach (SiCard c in cards)
            //{
            //    if (c.IsNew() && !SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, c.MsSiShubetsuID))
            //    {
            //        SiLinkShokumeiCard link = new SiLinkShokumeiCard();

            //        link.MsSiShokumeiID = senin.MsSiShokumeiID;

            //        c.SiLinkShokumeiCards.Add(link);
            //    }
            //}
            #endregion

            BuildLinkShokumei(ref cards);

            return cards;
        }

        /// <summary>
        ///  船員カードが乗船以外のときは、船員の職名を船員カードに登録する。
        ///  配乗表の表示時に必要。
        ///  2021/07/20 m.yoshihara BuildSaveCards()から処理を切り出した
        /// </summary>
        /// <param name="cardlist"></param>
        private void  BuildLinkShokumei(ref  List<SiCard> cardlist)
        {
            List<SiCard> retList = new List<SiCard>();

            foreach (SiCard c in cardlist)
            {
                if (c.IsNew() && !SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, c.MsSiShubetsuID))
                {
                    SiLinkShokumeiCard link = new SiLinkShokumeiCard();

                    link.MsSiShokumeiID = senin.MsSiShokumeiID;

                    c.SiLinkShokumeiCards.Add(link);
                }
            }

        }



        private void button退職_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "本船員を退職としてよろしいですか？",
                                            "退職の確認",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (DateTimeUtils.Empty(maskedTextBox退職年月日.Text) || !DateTimeUtils.Validate(maskedTextBox退職年月日.Text))
                {
                    maskedTextBox退職年月日.BackColor = Color.Pink;
                    MessageBox.Show("退職年月日を正しく入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    maskedTextBox退職年月日.BackColor = Color.White;
                    return;
                }

                senin.RetireFlag = 1;

                Save();
            }
        }

        #region private void button船員カード_Click(object sender, EventArgs e)
        private void button船員カード_Click(object sender, EventArgs e)
        {
            string BaseFileName = "船員カード";

            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = BaseFileName + " (*.xls)|*.xlsx";
            fd.FileName = BaseFileName + "_" + senin.Sei + senin.Mei + ".xlsx";

            if (fd.ShowDialog() == DialogResult.OK)
            {
                string message = "";
                bool outputResult = false;
                try
                {
                    byte[] excelData = null;

                    //2013/12/18 追加 m.y
                    //サーバーエラー時のフラグ
                    bool serverError = false;

                    ProgressDialog progressDialog = new ProgressDialog(delegate
                    {
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            //--------------------------------
                            //2013/12/18 コメントアウト m.y
                            //excelData = serviceClient.BLC_Excel_船員カード出力(NBaseCommon.Common.LoginUser, senin.MsSeninID);
                            //--------------------------------
                            //2013/12/18 変更: ServiceClientのExceptionを受け取る m.y
                            try
                            {
                                excelData = serviceClient.BLC_Excel_船員カード出力(NBaseCommon.Common.LoginUser, senin.MsSeninID);
                            }
                            catch (Exception exp) 
                            {
                                //MessageBox.Show(exp.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //serverError = true;//ここでreturnしても関数から抜け出せないのでフラグを用いる
                            }
                            //--------------------------------
                        }
                    }, "船員カードを作成中です...");
                    progressDialog.ShowDialog();

                    //--------------------------------
                    //2013/12/18 追加 m.y 
                    if (serverError == true)
                        return;
                    //--------------------------------

                    if (excelData == null)
                    {
                        MessageBox.Show("船員カードの出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    System.IO.FileStream filest = new System.IO.FileStream(fd.FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                    filest.Write(excelData, 0, excelData.Length);
                    filest.Close();

//                    try
//                    {
//                        _AddPictureErrMsg = "";// テストコード
////MessageBox.Show("Call _AddPicture(" + fd.FileName + ")");// テストコード
//                        _AddPicture(fd.FileName);                    
//                    }
//                    catch
//                    {
//                        // 写真を張り付けられない場合のエラーはスルーする
//                    }

//                    // テストコード
//                    if (_AddPictureErrMsg != "")
//                    {
//                        MessageBox.Show("写真を張り付けに失敗しました。\n (Err:" + _AddPictureErrMsg + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    }

                    outputResult = true;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    outputResult = false;
                }
                if (outputResult == true)
                {
                    // 成功
                    message = "「" + fd.FileName + "」へ出力しました";
                    MessageBox.Show(message, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // 失敗 
                    MessageBox.Show("船員カードの出力に失敗しました。\n (Err:" + message + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion


        #region private void _AddPicture(string filePath)
        private void _AddPicture(string filePath)
        {
//MessageBox.Show("_AddPicture");// テストコード
            if (senin.Picture == null || senin.Picture.Length == 0)
            {
//MessageBox.Show("senin.Picture is null");// テストコード
                _AddPictureErrMsg = "写真情報がありません";
                return;
            }

            // Excel Object
//MessageBox.Show("ExcelApplication");// テストコード
            ApplicationClass ExcelApplication = new ApplicationClass();
            Workbook thisBook = null;
            Worksheet thisSheet = null;
            Shapes oShapes = null;
            Shape oShape = null;

            try
            {
                // 写真をファイルに保存する
_AddPictureErrMsg = "写真をファイルに保存する";
//MessageBox.Show(_AddPictureErrMsg);// テストコード
                string currentPath = System.IO.Directory.GetCurrentDirectory();
                string pictureFilePath = currentPath + "\\船員写真_[" + NBaseCommon.Common.LoginUser.FullName + "].jpg";
//MessageBox.Show(pictureFilePath);// テストコード
                ImageConverter conv = new ImageConverter();
                Image img = (Image)conv.ConvertFrom(senin.Picture);
                img.Save(pictureFilePath);

                // Excelオープン
_AddPictureErrMsg = "Excelオープン";
//MessageBox.Show(_AddPictureErrMsg);// テストコード
                thisBook = ExcelApplication.Workbooks.Open(filePath,
                    Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

_AddPictureErrMsg = "Excelシート取得";
//MessageBox.Show(_AddPictureErrMsg);// テストコード
                thisSheet = (Worksheet)thisBook.Worksheets.get_Item(1);

                // 写真ファイルをExcelに追加する
_AddPictureErrMsg = "写真貼り付け";
//MessageBox.Show(_AddPictureErrMsg);// テストコード
                oShapes = thisSheet.Shapes;
                oShape = oShapes.Item(1);
                float fx = float.Parse(oShape.Left.ToString()) + 1;
                float fy = float.Parse(oShape.Top.ToString()) + 1;
                float fw = float.Parse(oShape.Width.ToString()) - 1;
                float fh = float.Parse(oShape.Height.ToString()) - 1;
                thisSheet.Shapes.AddPicture(pictureFilePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, fx, fy, fw, fh);

                // Excel保存
_AddPictureErrMsg = "Excelクローズ";
//MessageBox.Show(_AddPictureErrMsg);// テストコード
                thisBook.Save();
                thisBook.Close(true, Type.Missing, Type.Missing);
                ExcelApplication.Quit();

                // 写真ファイルを削除する
_AddPictureErrMsg = "写真ファイルを削除する";
                System.IO.File.Delete(pictureFilePath);

_AddPictureErrMsg = "";
            }
            catch (Exception ex)
            {
                _AddPictureErrMsg = _AddPictureErrMsg + ":" + ex.Message;
                Console.WriteLine("_AddPicture:" + ex.Message);
            }
            finally
            {
                ////開放
                releaseObj(oShape);
                releaseObj(oShapes);
                releaseObj(thisSheet);
                releaseObj(thisBook);
                releaseObj(ExcelApplication);
            }
        }
        #endregion

        /// <summary>
        /// COMオブジェクトのリリース
        /// </summary>
        /// <param name="ExcelObject"></param>
        #region public void releaseObj(object ExcelObject)
        public void releaseObj(object ExcelObject)
        {
            try
            {
                if (ExcelObject != null || System.Runtime.InteropServices.Marshal.IsComObject(ExcelObject))
                {
                    int i;
                    do
                    {
                        i = System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelObject);
                    } while (i > 0);

                }
            }
            finally
            {
                ExcelObject = null;
                GC.Collect();
            }
        }
        #endregion




        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabChangeEventCalcel)
                return;

            NBaseCommon.MessageForm form = null;
            if (SelectedTabName == _tabNames[0]) // 基本情報
            {
                form = new NBaseCommon.MessageForm();
                form.Text = "確認";

                if (senin.IsNew())
                {
                    form.Message = "船員情報が登録されていません。（新規の船員情報です）" + System.Environment.NewLine + "船員情報を登録してください。";
                    form.Buttons = MessageBoxButtons.OK;
                    form.ShowDialog();

                    e.Cancel = true;
                    return;
                }
                else if (this.ChangeFlag)
                {
                    form.Message = "データが編集されています。タブを移動してよろしいですか？" + System.Environment.NewLine + System.Environment.NewLine +
                                   "（「はい」をクリックすると、編集内容を破棄してタブを移動します）";
                    form.Buttons = MessageBoxButtons.YesNo;
                    if (form.ShowDialog() == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }

                    ClearFields();
                    InitFields();

                    Reset連絡先();

                    this.ChangeFlag = false;
                }
            }
            else if (SelectedTabName == _tabNames[1]) // 連絡先
            {
                if (this.ChangeFlag || (seninAddress != null && seninAddress.EditFlag))
                {
                    form = new NBaseCommon.MessageForm();
                    form.Text = "確認";
                    form.Buttons = MessageBoxButtons.YesNo;

                    form.Message = "データが編集されています。タブを移動してよろしいですか？" + System.Environment.NewLine + System.Environment.NewLine +
                                   "（「はい」をクリックすると、編集内容を破棄してタブを移動します）";
                    form.Buttons = MessageBoxButtons.YesNo;
                    if (form.ShowDialog() == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }

                    Reset連絡先();

                    this.ChangeFlag = false;
                    if (seninAddress != null)
                        seninAddress.EditFlag = false;
                }
            }
            else if (SelectedTabName == _tabNames[2]) // 経歴等
            {
                if (this.ChangeFlag || (seninAddress != null && seninAddress.EditFlag))
                {
                    form = new NBaseCommon.MessageForm();
                    form.Text = "確認";
                    form.Buttons = MessageBoxButtons.YesNo;

                    form.Message = "データが編集されています。タブを移動してよろしいですか？" + System.Environment.NewLine + System.Environment.NewLine +
                                   "（「はい」をクリックすると、編集内容を破棄してタブを移動します）";
                    form.Buttons = MessageBoxButtons.YesNo;
                    if (form.ShowDialog() == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }

                    Reset経歴等();

                    this.ChangeFlag = false;
                    if (seninAddress != null)
                        seninAddress.EditFlag = false;
                }
            }
        }

        /// <summary>
        /// タブが切り替わったイベント 2021/08/03 m.yoshihara
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //情報取得、表示
            ChangeTab_Search();
        }
        
        /// <summary>
        /// 選択されているタブの対象情報を取得し表示する  2021/08/03 m.yoshihara
        /// </summary>
        private void ChangeTab_Search()
        {

            SelectedTabName = tabControl1.SelectedTab.Name;


            // 該当タブが既にクリックされていれば、何もしない
            if (tabClicked[tabControl1.SelectedTab.Name] == true)
                return;

            this.Cursor = Cursors.WaitCursor;

            //該当タブをtrueにする
            tabClicked[tabControl1.SelectedTab.Name] = true;

            if (tabControl1.SelectedTab.Name == "基本情報")
            {
                ;

            }
            else if (tabControl1.SelectedTab.Name == "連絡先")
            {
                Search住所();
            }
            else if (tabControl1.SelectedTab.Name == "経歴等")
            {
                Search経歴等();
            }
            else if (tabControl1.SelectedTab.Name == "乗下船履歴")
            {
                if (treeListViewDelegate船員カード == null)
                    treeListViewDelegate船員カード = new TreeListViewDelegate船員カード(treeListView船員カード);
                Search船員カード();
            }
            else if (tabControl1.SelectedTab.Name == "免許免状")
            {
                if (treeListViewDelegate免状免許 == null)
                    treeListViewDelegate免状免許 = new TreeListViewDelegate免状免許(treeListView免状免許);
                Search免状免許();
            }
            else if (tabControl1.SelectedTab.Name == "家族情報")
            {
                if (treeListViewDelegate家族情報 == null)
                    treeListViewDelegate家族情報 = new TreeListViewDelegate家族情報(treeListView家族情報);
                Search家族情報();
            }
            else if (tabControl1.SelectedTab.Name == "講習")
            {
                if (treeListViewDelegate講習 == null)
                    treeListViewDelegate講習 = new TreeListViewDelegate講習(treeListView講習);
                Search講習();
            }
            else if (tabControl1.SelectedTab.Name == "傷病")
            {
                if (treeListViewDelegate傷病 == null)
                    treeListViewDelegate傷病 = new TreeListViewDelegate傷病(treeListView傷病);
                Search傷病();
            }
            else if (tabControl1.SelectedTab.Name == "健康診断")
            {
                Search既往歴();

                if (treeListViewDelegate健康診断 == null)
                    treeListViewDelegate健康診断 = new TreeListViewDelegate健康診断(treeListView健康診断);
                Search健康診断();
            }

            else if (tabControl1.SelectedTab.Name == "賞罰")
            {
                if (treeListViewDelegate賞罰 == null)
                    treeListViewDelegate賞罰 = new TreeListViewDelegate賞罰(treeListView賞罰);
                Search賞罰();
            }
            else if (tabControl1.SelectedTab.Name == "特記事項")
            {
                if (treeListViewDelegate特記事項 == null)
                    treeListViewDelegate特記事項 = new TreeListViewDelegate特記事項(treeListView特記事項);
                Search特記事項();
            }
            else if (tabControl1.SelectedTab.Name == "基本給")
            {
                if (treeListViewDelegate基本給 == null)
                    treeListViewDelegate基本給 = new TreeListViewDelegate基本給(treeListView基本給);
                Search基本給();
            }
            else if (tabControl1.SelectedTab.Name == "CrewMatrix")
            {
                if (treeListViewDelegateCrewMatrix == null)
                    treeListViewDelegateCrewMatrix = new TreeListViewDelegateCrewMatrix(treeListViewCrewMatrix);
                SearchCrewMatrix();
            }
            else if (tabControl1.SelectedTab.Name == "履歴")
            {
                if (treeListViewDelegate履歴 == null)
                    treeListViewDelegate履歴 = new TreeListViewDelegate履歴(treeListView履歴);
                Search履歴();
            }

            this.Cursor = Cursors.Default;
        }

        //データを編集した時
        private void DataChange(object sender, EventArgs e)
        {
            this.ChangeFlag = true;
        }

        private void maskedTextBox生年月日_Leave(object sender, EventArgs e)
        {
            try
            {
                textBox年齢.Text = DateTimeUtils.年齢計算(DateTime.Parse(maskedTextBox生年月日.Text)).ToString();
            }
            catch
            {
                textBox年齢.Text = "";
            }
        }
    }

}
