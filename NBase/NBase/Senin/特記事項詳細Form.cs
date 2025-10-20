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
    public partial class 特記事項詳細Form : Form
    {
        private 船員詳細Panel parentForm;
        private SiRemarks remarks;
        private bool isNew;

        public 特記事項詳細Form(船員詳細Panel parentForm)
            : this(parentForm, new SiRemarks(), true)
        {
        }


        public 特記事項詳細Form(船員詳細Panel parentForm, SiRemarks remarks, bool isNew)
        {
            this.parentForm = parentForm;
            this.remarks = remarks;
            this.isNew = isNew;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitFields();
        }


        private void InitFields()
        {
            if (!isNew)
            {
                dateTimePicker年月日.Value = remarks.RemarksDate;
                textBox特記事項名.Text = remarks.RemarksName;
                textBox備考.Text = remarks.Remarks;

                foreach (SiRemarksAttachFile attach in remarks.AttachFiles)
                {
                    if (attach.DeleteFlag == 0)
                        listBox添付ファイル.Items.Add(attach);
                }
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

                if (parentForm.InsertOrUpdate_特記事項詳細(remarks))
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                parentForm.Refresh特記事項();
                Dispose();
            }
        }


        private bool ValidateFields()
        {
            if (textBox特記事項名.Text.Length == 0)
            {
                textBox特記事項名.BackColor = Color.Pink;
                MessageBox.Show("タイトルを入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox特記事項名.BackColor = Color.White;
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
            remarks.RemarksDate = dateTimePicker年月日.Value;
            remarks.RemarksName = StringUtils.Escape(textBox特記事項名.Text);
            remarks.Remarks = StringUtils.Escape(textBox備考.Text);       
        }


        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                remarks.DeleteFlag = 1;
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
            if (listBox添付ファイル.SelectedItem is SiRemarksAttachFile)
            {
                if (MessageBox.Show("選択したファイルを削除しますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SiRemarksAttachFile deleteAttach = listBox添付ファイル.SelectedItem as SiRemarksAttachFile;

                    for (int i = 0; i < remarks.AttachFiles.Count; i++)
                    {
                        SiRemarksAttachFile attach = remarks.AttachFiles[i];
                        if (attach.SiRemarksAttachFileID == deleteAttach.SiRemarksAttachFileID)
                        {
                            if (attach.SiRemarksID == null)
                            {
                                remarks.AttachFiles.Remove(attach);
                            }
                            else
                            {
                                attach.DeleteFlag = 1;
                                remarks.AttachFiles[i] = attach;
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
            if (listBox添付ファイル.SelectedItem is SiRemarksAttachFile)
            {
                SiRemarksAttachFile attach = (listBox添付ファイル.SelectedItem as SiRemarksAttachFile);
                // ファイルの表示
                NBaseCommon.FileView.View(attach.SiRemarksAttachFileID, attach.FileName, attach.Data);
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
            SiRemarksAttachFile attach = new SiRemarksAttachFile();
            attach.SiRemarksAttachFileID = System.Guid.NewGuid().ToString();

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

            remarks.AttachFiles.Add(attach);
            listBox添付ファイル.Items.Add(attach);

        }





    }
}
