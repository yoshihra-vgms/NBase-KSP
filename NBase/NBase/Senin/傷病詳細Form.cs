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
    public partial class 傷病詳細Form : Form
    {
        private 船員詳細Panel parentPanel;
        private SiShobyo shobyo;
        private bool isNew;

        private List<SiRireki> rirekiList = null;
        public void Set履歴(List<SiRireki> rirekiList)
        {
            this.rirekiList = rirekiList;
            Set等級日額();
        }

        public 傷病詳細Form(船員詳細Panel p)
            : this(p, new SiShobyo(), true)
        {
        }


        public 傷病詳細Form(船員詳細Panel pp, SiShobyo shobyo, bool isNew)
        {
            this.parentPanel = pp;
            this.shobyo = shobyo;
            this.isNew = isNew;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox種別();
            InitComboBoxStatus();
            InitComboBox船名();
            InitComboBox職名();

            InitFields();
        }


        private void InitFields()
        {
            nullableDateTimePicker対象期間_終了.Value = null;

            nullableDateTimePicker書類送付日.Value = null;
            nullableDateTimePicker書類返送.Value = null;
            nullableDateTimePicker提出日.Value = null;
            nullableDateTimePicker通知.Value = null;
            nullableDateTimePicker立替金伝票.Value = null;
            nullableDateTimePicker入金伝票.Value = null;
            nullableDateTimePicker送金.Value = null;
            nullableDateTimePicker本人郵送.Value = null;

            if (isNew)
            {
                dateTimePicker対象期間_開始.Value = DateTime.Today;
                nullableDateTimePicker対象期間_終了.Value = DateTime.Today;

                foreach (MsSiShokumei s in comboBox職名.Items)
                {
                    if (s.MsSiShokumeiID == parentPanel.senin.MsSiShokumeiID)
                    {
                        comboBox職名.SelectedItem = s;
                        break;
                    }
                }

                // 2018.02.07 口座は空でよいとコメントあり
                //textBox口座.Text = parentForm.senin.BankName1 + " " + parentForm.senin.BranchName1 + " " + parentForm.senin.AccountNo1.Trim();
                textBox口座.Text = "";
            }
            else
            {
                comboBox種別.SelectedIndex = shobyo.Kind;
                comboBoxステータス.SelectedIndex = shobyo.Status;
                textBox等級.Text = shobyo.Tokyu.ToString();
                textBox日額.Text = shobyo.Nitigaku.ToString();
                if (shobyo.FromDate != DateTime.MinValue)
                    dateTimePicker対象期間_開始.Value = shobyo.FromDate;
                if (shobyo.ToDate != DateTime.MinValue)
                    nullableDateTimePicker対象期間_終了.Value = shobyo.ToDate;
                if (shobyo.MsVesselID != 0)
                {
                    comboBox船.Text = SeninTableCache.instance().GetMsVessel(NBaseCommon.Common.LoginUser, shobyo.MsVesselID).ToString();
                } 
                if (shobyo.MsSiShokumeiID != 0)
                {
                    comboBox職名.Text = SeninTableCache.instance().GetMsSiShokumeiName(NBaseCommon.Common.LoginUser, shobyo.MsSiShokumeiID);
                }
                textBox傷病名.Text = shobyo.ShobyoName;
                textBox口座.Text = shobyo.Kouza;
                textBox金額.Text = shobyo.Kingaku.ToString();

                if (shobyo.SendDocument != DateTime.MinValue)
                    nullableDateTimePicker書類送付日.Value = shobyo.SendDocument;
                if (shobyo.DocumentReturn != DateTime.MinValue)
                    nullableDateTimePicker書類返送.Value = shobyo.DocumentReturn;
                if (shobyo.FilingDate != DateTime.MinValue)
                    nullableDateTimePicker提出日.Value = shobyo.FilingDate;
                if (shobyo.Notification != DateTime.MinValue)
                    nullableDateTimePicker通知.Value = shobyo.Notification;
                if (shobyo.AdvanceVoucher != DateTime.MinValue)
                    nullableDateTimePicker立替金伝票.Value = shobyo.AdvanceVoucher;
                if (shobyo.DepositSlip != DateTime.MinValue)
                    nullableDateTimePicker入金伝票.Value = shobyo.DepositSlip;
                if (shobyo.MoneyTransfer != DateTime.MinValue)
                    nullableDateTimePicker送金.Value = shobyo.MoneyTransfer;
                if (shobyo.MailToPrincipal != DateTime.MinValue)
                    nullableDateTimePicker本人郵送.Value = shobyo.MailToPrincipal;


                foreach (SiShobyoAttachFile attach in shobyo.AttachFiles)
                {
                    if (attach.DeleteFlag == 0)
                        listBox添付ファイル.Items.Add(attach);
                }
            }

            Set等級日額();
        }

        private void InitComboBox種別()
        {
            foreach (string s in SiShobyo.KIND)
            {
                comboBox種別.Items.Add(s);
            }
            comboBox種別.SelectedIndex = 0;
        }

        private void InitComboBoxStatus()
        {
            foreach (string s in SiShobyo.STATUS)
            {
                comboBoxステータス.Items.Add(s);
            }
            comboBoxステータス.SelectedIndex = 0;
        }

        private void InitComboBox船名()
        {
            comboBox船.Items.Add("");
            foreach (MsVessel s in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            {
                comboBox船.Items.Add(s);
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

                //2021/07/21　m.yoshihara 追加　DB更新処理追加
                if (parentPanel.InsertOrUpdate_傷病詳細(shobyo))
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //2021/07/29 引数無しに変更
                //parentPanel.Refresh傷病(shobyo);
                parentPanel.Refresh傷病();

                Dispose();
            }
        }


        private bool ValidateFields()
        {
            //if ((textBox等級.Text.Length != 0 && !NumberUtils.Validate(textBox等級.Text)) || textBox等級.Text.Length > 3)
            //{
            //    textBox等級.BackColor = Color.Pink;
            //    MessageBox.Show("等級は半角数字3文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    textBox等級.BackColor = Color.White;
            //    return false;
            //}
            //else if ((textBox日額.Text.Length != 0 && !NumberUtils.Validate(textBox日額.Text)) || textBox日額.Text.Length > 6)
            //{
            //    textBox日額.BackColor = Color.Pink;
            //    MessageBox.Show("日額は半角数字6文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    textBox日額.BackColor = Color.White;
            //    return false;
            //}
            if ((textBox金額.Text.Length != 0 && !NumberUtils.Validate(textBox金額.Text)) || textBox金額.Text.Length > 8)
            {
                textBox金額.BackColor = Color.Pink;
                MessageBox.Show("金額は半角数字8文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox金額.BackColor = Color.White;
                return false;
            }
            else if (comboBox職名.SelectedItem == null)
            {
                comboBox職名.BackColor = Color.Pink;
                MessageBox.Show("職名を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox職名.BackColor = Color.White;
                return false;
            }             

            return true;
        }


        private void FillInstance()
        {
            int work;

            shobyo.Kind = comboBox種別.SelectedIndex;
            shobyo.Status = comboBoxステータス.SelectedIndex;
            int.TryParse(textBox等級.Text, out work);
            shobyo.Tokyu = work;
            int.TryParse(textBox日額.Text, out work);
            shobyo.Nitigaku = work;
            shobyo.FromDate = dateTimePicker対象期間_開始.Value;
            if (nullableDateTimePicker対象期間_終了.Value != null)
                shobyo.ToDate = (DateTime)nullableDateTimePicker対象期間_終了.Value;
            else
                shobyo.ToDate = DateTime.MinValue;
            if (comboBox船.SelectedItem is MsVessel)
            {
                shobyo.MsVesselID = (comboBox船.SelectedItem as MsVessel).MsVesselID;
            }
            else
            {
                shobyo.MsVesselID = 0;
            }
            if (comboBox職名.SelectedItem is MsSiShokumei)
            {
                shobyo.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }
            else
            {
                shobyo.MsSiShokumeiID = 0;
            }
            shobyo.ShobyoName = textBox傷病名.Text;
            shobyo.Kouza = textBox口座.Text;
            int.TryParse(textBox金額.Text, out work);
            shobyo.Kingaku = work;

            if (nullableDateTimePicker書類送付日.Value != null)
                shobyo.SendDocument = (DateTime)nullableDateTimePicker書類送付日.Value;
            if (nullableDateTimePicker書類返送.Value != null)
                shobyo.DocumentReturn = (DateTime)nullableDateTimePicker書類返送.Value;
            if (nullableDateTimePicker提出日.Value != null)
                shobyo.FilingDate = (DateTime)nullableDateTimePicker提出日.Value;
            if (nullableDateTimePicker通知.Value != null)
                shobyo.Notification = (DateTime)nullableDateTimePicker通知.Value;
            if (nullableDateTimePicker立替金伝票.Value != null)
                shobyo.AdvanceVoucher = (DateTime)nullableDateTimePicker立替金伝票.Value;
            if (nullableDateTimePicker入金伝票.Value != null)
                shobyo.DepositSlip = (DateTime)nullableDateTimePicker入金伝票.Value;
            if (nullableDateTimePicker送金.Value != null)
                shobyo.MoneyTransfer = (DateTime)nullableDateTimePicker送金.Value;
            if (nullableDateTimePicker本人郵送.Value != null)
                shobyo.MailToPrincipal = (DateTime)nullableDateTimePicker本人郵送.Value;
        }


        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                shobyo.DeleteFlag = 1;
                Save();
            }
        }

        
        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }



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
            if (listBox添付ファイル.SelectedItem is SiShobyoAttachFile)
            {
                if (MessageBox.Show("選択したファイルを削除しますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SiShobyoAttachFile deleteAttach = listBox添付ファイル.SelectedItem as SiShobyoAttachFile;

                    for (int i = 0; i < shobyo.AttachFiles.Count; i++)
                    {
                        SiShobyoAttachFile attach = shobyo.AttachFiles[i];
                        if (attach.SiShobyoAttachFileID == deleteAttach.SiShobyoAttachFileID)
                        {
                            if (attach.SiShobyoID == null)
                            {
                                shobyo.AttachFiles.Remove(attach);
                            }
                            else
                            {
                                attach.DeleteFlag = 1;
                                shobyo.AttachFiles[i] = attach;
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
            if (listBox添付ファイル.SelectedItem is SiShobyoAttachFile)
            {
                SiShobyoAttachFile attach = (listBox添付ファイル.SelectedItem as SiShobyoAttachFile);
                // ファイルの表示
                NBaseCommon.FileView.View(attach.SiShobyoAttachFileID, attach.FileName, attach.Data);
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
            SiShobyoAttachFile attach = new SiShobyoAttachFile();
            attach.SiShobyoAttachFileID = System.Guid.NewGuid().ToString();

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

            shobyo.AttachFiles.Add(attach);
            listBox添付ファイル.Items.Add(attach);

        }





        private void dateTimePicker対象期間_開始_ValueChanged(object sender, EventArgs e)
        {
            Set等級日額();
        }


        private void Set等級日額()
        {
            if (rirekiList == null)
                return;

            DateTime startDate = dateTimePicker対象期間_開始.Value;
            if (rirekiList.Any(obj => obj.RirekiDate <= startDate))
            {
                var rireki = rirekiList.Where(obj => obj.RirekiDate <= startDate).OrderByDescending(obj => obj.RirekiDate).First();


                textBox等級.Text = rireki.Tokyu != int.MinValue ? rireki.Tokyu.ToString() : "";
                textBox日額.Text = rireki.Nitigaku != int.MinValue ? rireki.Nitigaku.ToString() : "";

            }
            else
            {
                textBox等級.Text = "";
                textBox日額.Text = "";
            }
        }

    }
}
