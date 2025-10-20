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
using NBaseCommon;

namespace Senin
{
    public partial class 講習管理詳細Form : SeninSearchClientForm
    {
        // 2014.02 2013年度改造
        //private 講習管理Form parentForm;
        private Form parentForm;

        private SiKoushu koushu;
        private MsSenin senin;

        private List<SiKoushuAttachFile> orgAttachFile = new List<SiKoushuAttachFile>();


        public 講習管理詳細Form(Form parentForm)
        //public 講習管理詳細Form(講習管理Form parentForm)
            : this(parentForm, new SiKoushu())
        {
        }


        //public 講習管理詳細Form(講習管理Form parentForm, SiKoushu koushu)
        public 講習管理詳細Form(Form parentForm, SiKoushu koushu)
        {
            this.parentForm = parentForm;
            this.koushu = koushu;
            this.senin = null;

            if (this.koushu.AttachFiles.Count > 0)
            {
                SiKoushuAttachFile[] tmp = new SiKoushuAttachFile[this.koushu.AttachFiles.Count];
                this.koushu.AttachFiles.CopyTo(tmp);
                orgAttachFile = tmp.ToList<SiKoushuAttachFile>();
            }

            InitializeComponent();
            Init();
        }

        #region private void Init()
        private void Init()
        {
            InitComboBox講習名();
            InitTextBox場所();
            InitFields();
        }
        #endregion

        #region private void InitComboBox講習名()
        private void InitComboBox講習名()
        {
            comboBox講習名.Items.Add(string.Empty);

            foreach (MsSiKoushu k in SeninTableCache.instance().GetMsSiKoushuList(NBaseCommon.Common.LoginUser))
            {
                comboBox講習名.Items.Add(k);
                comboBox講習名.AutoCompleteCustomSource.Add(k.Name);
            }
            comboBox講習名.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox講習名.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comboBox講習名.SelectedIndex = 0;
        }
        #endregion

        #region private void InitTextBox場所()
        private void InitTextBox場所()
        {
            List<SiKoushu> koushus = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                koushus = serviceClient.SiKoushu_GetRecordsBashoOnly(NBaseCommon.Common.LoginUser);
            }
            foreach (SiKoushu k in koushus)
            {
                textBox場所.AutoCompleteCustomSource.Add(k.Basho);
            }
            textBox場所.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox場所.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        #endregion

        #region private void InitFields()
        private void InitFields()
        {
            //if (!koushu.IsNew())
            if (!koushu.IsNew() || (parentForm is 未受講者抽出Form))
            {
                comboBox講習名.Text = koushu.KoushuName;
                textBox場所.Text = koushu.Basho;

                if (koushu.YoteiFrom != DateTime.MinValue)
                {
                    nullableDateTimePicker開始予定日.Value = koushu.YoteiFrom;
                }
                else
                {
                    nullableDateTimePicker開始予定日.Value = DateTime.Today;
                    nullableDateTimePicker開始予定日.Value = null;
                }
                if (koushu.YoteiTo != DateTime.MinValue)
                {
                    nullableDateTimePicker終了予定日.Value = koushu.YoteiTo;
                }
                else
                {
                    nullableDateTimePicker終了予定日.Value = DateTime.Today;
                    nullableDateTimePicker終了予定日.Value = null;
                }
                if (koushu.JisekiFrom != DateTime.MinValue)
                {
                    nullableDateTimePicker受講開始日.Value = koushu.JisekiFrom;
                }
                else
                {
                    nullableDateTimePicker受講開始日.Value = DateTime.Today;
                    nullableDateTimePicker受講開始日.Value = null;
                }
                if (koushu.JisekiTo != DateTime.MinValue)
                {
                    nullableDateTimePicker受講終了日.Value = koushu.JisekiTo;
                }
                else
                {
                    nullableDateTimePicker受講終了日.Value = DateTime.Today;
                    nullableDateTimePicker受講終了日.Value = null;
                }

                textBox船員.Text = koushu.SeninName;

                foreach (SiKoushuAttachFile attach in koushu.AttachFiles)
                {
                    if (attach.DeleteFlag == 0)
                        listBox添付ファイル.Items.Add(attach);
                }

                textBox備考.Text = koushu.Bikou;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    senin = serviceClient.MsSenin_GetRecord(NBaseCommon.Common.LoginUser, koushu.MsSeninID);
                }
                textBox船員.Text = senin.Sei + " " + senin.Mei;



                // 実績日が入力されている場合、変更、削除はできないようにする
                if (koushu.JisekiFrom != DateTime.MinValue || koushu.JisekiTo != DateTime.MinValue)
                {
                    //button添付追加.Enabled = false;
                    //button添付削除.Enabled = false;

                    //button更新.Enabled = false;
                    //button削除.Enabled = false;

                    comboBox講習名.Enabled = false;
                    textBox場所.Enabled = false;
                    nullableDateTimePicker開始予定日.Enabled = false;
                    nullableDateTimePicker終了予定日.Enabled = false;
                    nullableDateTimePicker受講開始日.Enabled = false;
                    nullableDateTimePicker受講終了日.Enabled = false;
                    button船員検索.Enabled = false;
                    textBox備考.Enabled = false;

                    button削除.Enabled = false;
                }

                // 2014.02 2013年度改造
                if (koushu.IsNew())
                {
                    button削除.Enabled = false;
                }
                if (parentForm is 未受講者抽出Form)
                {
                    // 未受講者抽出Formからの場合、講習名、船員の変更は不可とする
                    comboBox講習名.Enabled = false;
                    button船員検索.Enabled = false;
                }
            }
            else
            {
                nullableDateTimePicker開始予定日.Value = DateTime.Today;
                nullableDateTimePicker開始予定日.Value = null;
                nullableDateTimePicker終了予定日.Value = DateTime.Today;
                nullableDateTimePicker終了予定日.Value = null;
                nullableDateTimePicker受講開始日.Value = DateTime.Today;
                nullableDateTimePicker受講開始日.Value = null;
                nullableDateTimePicker受講終了日.Value = DateTime.Today;
                nullableDateTimePicker受講終了日.Value = null;

                button削除.Enabled = false;
            }
        }
        #endregion

        /// <summary>
        /// 「更新」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button更新_Click(object sender, EventArgs e)
        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
        }
        #endregion


        /// <summary>
        /// 「削除」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button削除_Click(object sender, EventArgs e)
        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                koushu.DeleteFlag = 1;

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, "削除しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dispose();

                    // 2014.02 2013年度改造
                    //parentForm.SetSiKoushu(koushu);
                    if (parentForm is 講習管理Form)
                    {
                        ((講習管理Form)parentForm).SetSiKoushu(koushu);
                    }
                    if (parentForm is 未受講者抽出Form)
                    {
                        //((未受講者抽出Form)parentForm).SetSiKoushu(koushu);
                        ((未受講者抽出Form)parentForm).ReSearch();
                    }
                }
                else
                {
                    MessageBox.Show(this, "削除に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button閉じる_Click(object sender, EventArgs e)
        private void button閉じる_Click(object sender, EventArgs e)
        {
            // 「閉じる」クリック時には
            // 元々持っていた添付ファイルに戻す
            koushu.AttachFiles.Clear();
            if (orgAttachFile.Count > 0)
            {
                foreach (SiKoushuAttachFile attach in orgAttachFile)
                {
                    attach.DeleteFlag = 0; // 削除されている場合、戻す
                }
                koushu.AttachFiles.AddRange(orgAttachFile);
            }
            Dispose();
        }
        #endregion


        /// <summary>
        /// 登録ロジック
        /// </summary>
        #region private void Save()
        private void Save()
        {
            if (ValidateFields())
            {
                FillInstance();

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dispose();
                    // 2014.02 2013年度改造
                    //parentForm.SetSiKoushu(koushu);
                    if (parentForm is 講習管理Form)
                    {
                        ((講習管理Form)parentForm).SetSiKoushu(koushu);
                    }
                    if (parentForm is 未受講者抽出Form)
                    {
                        //((未受講者抽出Form)parentForm).SetSiKoushu(koushu);
                        ((未受講者抽出Form)parentForm).ReSearch();
                    }
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region private bool InsertOrUpdate()
        private bool InsertOrUpdate()
        {
            bool result = true;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                SiKoushu retKoushu = serviceClient.SiKoushu_InsertOrUpdate(NBaseCommon.Common.LoginUser, koushu);
                if (retKoushu != null)
                {
                    koushu = retKoushu;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }
        #endregion


        /// <summary>
        /// 入力値の確認
        /// </summary>
        /// <returns></returns>
        #region private bool ValidateFields()
        private bool ValidateFields()
        {
            if (!(comboBox講習名.SelectedItem is MsSiKoushu))
            {
                comboBox講習名.BackColor = Color.Pink;
                MessageBox.Show("講習名を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox講習名.BackColor = Color.White;
                return false;
            }
            else if (textBox場所.Text == null || textBox場所.Text.Length == 0 || textBox場所.Text.Length > 50)
            {
                textBox場所.BackColor = Color.Pink;
                MessageBox.Show("場所は50文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox場所.BackColor = Color.White;
                return false;
            }
            else if ( nullableDateTimePicker開始予定日.Value != null 
                      && nullableDateTimePicker終了予定日.Value != null
                      && (DateTime)nullableDateTimePicker開始予定日.Value > (DateTime)nullableDateTimePicker終了予定日.Value)
            {
                nullableDateTimePicker開始予定日.BackColor = Color.Pink;
                nullableDateTimePicker終了予定日.BackColor = Color.Pink;
                MessageBox.Show("開始予定日が終了予定日より後の日付です", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nullableDateTimePicker開始予定日.BackColor = Color.White;
                nullableDateTimePicker終了予定日.BackColor = Color.White;
                return false;
            }
            else if (nullableDateTimePicker受講開始日.Value != null
                      && nullableDateTimePicker受講終了日.Value != null
                      && (DateTime)nullableDateTimePicker受講開始日.Value > (DateTime)nullableDateTimePicker受講終了日.Value)
            {
                nullableDateTimePicker受講開始日.BackColor = Color.Pink;
                nullableDateTimePicker受講終了日.BackColor = Color.Pink;
                MessageBox.Show("受講開始日が受講終了日より後の日付です", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nullableDateTimePicker受講開始日.BackColor = Color.White;
                nullableDateTimePicker受講終了日.BackColor = Color.White;
                return false;
            }
            else if (textBox船員.Text == null || textBox船員.Text.Length == 0)
            {
                textBox船員.BackColor = Color.Pink;
                MessageBox.Show("船員を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox船員.BackColor = Color.White;
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
        #endregion

        /// <summary>
        /// 入力値を取得し、講習情報にセットする
        /// </summary>
        #region FillInstance()
        private void FillInstance()
        {
            MsSiKoushu msSiKoushu = comboBox講習名.SelectedItem as MsSiKoushu;
            koushu.MsSiKoushuID        = msSiKoushu.MsSiKoushuID;
            koushu.KoushuName          = msSiKoushu.Name;
            koushu.KoushuYukokigenStr  = msSiKoushu.YukokigenStr;
            koushu.KoushuYukokigenDays = msSiKoushu.YukokigenDays;
            koushu.Basho = textBox場所.Text;
            if (nullableDateTimePicker開始予定日.Value != null)
            {
                koushu.YoteiFrom = (DateTime)nullableDateTimePicker開始予定日.Value;
            }
            if (nullableDateTimePicker終了予定日.Value != null)
            {
                koushu.YoteiTo = (DateTime)nullableDateTimePicker終了予定日.Value;
            }
            if (nullableDateTimePicker受講開始日.Value != null)
            {
                koushu.JisekiFrom = (DateTime)nullableDateTimePicker受講開始日.Value;
            }
            if (nullableDateTimePicker受講終了日.Value != null)
            {
                koushu.JisekiTo = (DateTime)nullableDateTimePicker受講終了日.Value;
            }
            koushu.MsSeninID = senin.MsSeninID;
            koushu.SeninName = senin.Sei + " " + senin.Mei;
            koushu.SeninShimeiCode = senin.ShimeiCode;
            koushu.SeninShokumei = SeninTableCache.instance().GetMsSiShokumeiName(NBaseCommon.Common.LoginUser, senin.MsSiShokumeiID);
            koushu.Bikou = StringUtils.Escape(textBox備考.Text);

            koushu.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
            koushu.RenewDate = DateTime.Now;
        }
        #endregion

        /// <summary>
        /// 「船員検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button船員検索_Click(object sender, EventArgs e)
        private void button船員検索_Click(object sender, EventArgs e)
        {
            船員検索Form form = new 船員検索Form(this, false);
            form.ShowDialog();
        }
        #endregion

        /// <summary>
        /// 船員検索からコールされる船員検索の実処理
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        #region public override List<MsSenin> SearchMsSenin(MsSeninFilter filter)
        public override List<MsSenin> SearchMsSenin(MsSeninFilter filter)
        {
            List<MsSenin> result = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsSenin_GetRecordsByFilter(NBaseCommon.Common.LoginUser, filter);
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 船員検索からコールされる船員選択の実処理
        /// </summary>
        /// <param name="senin"></param>
        #region public override bool SetMsSenin(MsSenin senin, bool check)
        public override bool SetMsSenin(MsSenin senin, bool check)
        {
            this.senin = senin;
            textBox船員.Text = senin.Sei + " " + senin.Mei;

            return true;
        }
        #endregion



        #region 添付ファイルの処理

        private void button添付追加_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "Attach File(*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.FileName = "";
            FileUtils.SetDesktopFolder(openFileDialog1);

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                // 2014.02 2013年度改造
                #region
                //SiKoushuAttachFile attach = new SiKoushuAttachFile();
                //attach.SiKoushuAttachFileID = System.Guid.NewGuid().ToString();

                //// ファイル情報をセット
                //string FullPath = openFileDialog1.FileName;
                //attach.FileName = System.IO.Path.GetFileName(FullPath);
                //attach.UpdateDate = System.IO.File.GetLastWriteTime(FullPath); // 更新日時

                //// ファイルを読み込む
                //System.IO.FileStream fs = new System.IO.FileStream(FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                //attach.Data = new byte[fs.Length];
                //fs.Read(attach.Data, 0, attach.Data.Length);
                //fs.Close();

                //if (FileUtils.SizeCheck(attach.Data, ORMapping.Common.MAX_BINARY_SIZE) == true)
                //{
                //    koushu.AttachFiles.Add(attach);
                //    listBox添付ファイル.Items.Add(attach);
                //}
                //else
                //{
                //    MessageBox.Show("登録できるファイルのサイズは " + (ORMapping.Common.MAX_BINARY_SIZE / 1000) + " KB以下です", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                #endregion
                foreach (string fileName in openFileDialog1.FileNames)
                {
                    Add添付ファイル(fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private void button添付削除_Click(object sender, EventArgs e)
        {
            if (listBox添付ファイル.Items.Count == 0)
            {
                return;
            }
            if (listBox添付ファイル.SelectedItem is SiKoushuAttachFile)
            {
                if (MessageBox.Show("選択したファイルを削除しますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SiKoushuAttachFile deleteAttach = listBox添付ファイル.SelectedItem as SiKoushuAttachFile;

                    for (int i = 0; i < koushu.AttachFiles.Count; i++)
                    {
                        SiKoushuAttachFile attach = koushu.AttachFiles[i];
                        if (attach.SiKoushuAttachFileID == deleteAttach.SiKoushuAttachFileID)
                        {
                            if (attach.SiKoushuID == null)
                            {
                                koushu.AttachFiles.Remove(attach);
                            }
                            else
                            {
                                attach.DeleteFlag = 1;
                                koushu.AttachFiles[i] = attach;
                            }
                            break;
                        }
                    }

                    listBox添付ファイル.Items.Remove(deleteAttach);
                }
            }

        }

        private void listBox添付ファイル_DoubleClick(object sender, EventArgs e)
        {
            if (listBox添付ファイル.SelectedItem is SiKoushuAttachFile)
            {
                SiKoushuAttachFile attach = (listBox添付ファイル.SelectedItem as SiKoushuAttachFile);
                // ファイルの表示
                NBaseCommon.FileView.View(attach.SiKoushuAttachFileID, attach.FileName, attach.Data);
            }
        }

        private void listBox添付ファイル_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void listBox添付ファイル_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    Add添付ファイル(fileName);
                }
            }
        }

        private void Add添付ファイル(string fullpath)
        {
            SiKoushuAttachFile attach = new SiKoushuAttachFile();
            attach.SiKoushuAttachFileID = System.Guid.NewGuid().ToString();

            // ファイル情報をセット
            attach.FileName = System.IO.Path.GetFileName(fullpath);
            attach.UpdateDate = System.IO.File.GetLastWriteTime(fullpath); // 更新日時

            // ファイルを読み込む
            System.IO.FileStream fs = new System.IO.FileStream(fullpath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            
            // 2014.02 2013年度改造：読み込む前にサイズのチェックをするように変更
            if (fs.Length > ORMapping.Common.MAX_BINARY_SIZE)
            {
                fs.Close();
                MessageBox.Show(attach.FileName + System.Environment.NewLine + "登録できるファイルのサイズは " + (ORMapping.Common.MAX_BINARY_SIZE / 1000) + " KB以下です", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            attach.Data = new byte[fs.Length];
            fs.Read(attach.Data, 0, attach.Data.Length);
            fs.Close();

            //if (FileUtils.SizeCheck(attach.Data, ORMapping.Common.MAX_BINARY_SIZE) == true)
            //{
                koushu.AttachFiles.Add(attach);
                listBox添付ファイル.Items.Add(attach);
            //}
            //else
            //{
            //    MessageBox.Show(attach.FileName + System.Environment.NewLine + "登録できるファイルのサイズは " + (ORMapping.Common.MAX_BINARY_SIZE / 1000) + " KB以下です", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        #endregion
    }
}
