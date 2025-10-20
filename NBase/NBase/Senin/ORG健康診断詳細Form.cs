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
    public partial class 健康診断詳細Form : Form
    {
        private 船員詳細Panel parentForm;
        private SiKenshin kenshin;
        private bool isNew;
        private string seninName;


        public 健康診断詳細Form(船員詳細Panel parentForm, string seninName)
            : this(parentForm, seninName, new SiKenshin(), true)
        {
        }


        public 健康診断詳細Form(船員詳細Panel parentForm, string seninName, SiKenshin kenshin, bool isNew)
        {
            this.parentForm = parentForm;
            this.seninName = seninName;
            this.kenshin = kenshin;
            this.isNew = isNew;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox種別();
            InitComboBox職名();
            InitComboBox結果();
            InitFields();
        }


        private void InitFields()
        {
            if (isNew)
            {
                foreach (MsSiShokumei s in comboBox職名.Items)
                {
                    if (s.MsSiShokumeiID == parentForm.senin.MsSiShokumeiID)
                    {
                        comboBox職名.SelectedItem = s;
                        break;
                    }
                }

            }
            else
            {
                comboBox種別.SelectedIndex = kenshin.Kind;

                if (kenshin.MsSiShokumeiID != int.MinValue)
                {
                    foreach (MsSiShokumei s in comboBox職名.Items)
                    {
                        if (s.MsSiShokumeiID == kenshin.MsSiShokumeiID)
                        {
                            comboBox職名.SelectedItem = s;
                            break;
                        }
                    }
                }
                dateTimePicker受診日.Value = kenshin.ConsultationDate;
                if (kenshin.ExpirationDate != DateTime.MinValue)
                {
                    nullableDateTimePicker有効期限.Value = kenshin.ExpirationDate;
                }
                else
                {
                    nullableDateTimePicker有効期限.Value = null;
                }
                comboBox結果.SelectedIndex = kenshin.Result;
                textBox結果詳細.Text = kenshin.ResultDatail;

                foreach (SiKenshinAttachFile attach in kenshin.AttachFiles)
                {
                    if (attach.DeleteFlag == 0)
                        listBox添付ファイル.Items.Add(attach);
                }
            }
        }


        private void InitComboBox種別()
        {
            foreach (string s in SiKenshin.KIND)
            {
                comboBox種別.Items.Add(s);
            }
        }

        private void InitComboBox職名()
        {
            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                comboBox職名.Items.Add(s);
            }
        }

        private void InitComboBox結果()
        {
            foreach (string s in SiKenshin.RESULT)
            {
                comboBox結果.Items.Add(s);
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

                parentForm.Refresh健康診断(kenshin);
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

            if (comboBox結果.SelectedItem == null)
            {
                comboBox職名.BackColor = Color.Pink;
                MessageBox.Show("結果を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox職名.BackColor = Color.White;
                return false;
            }
             

            return true;
        }


        private void FillInstance()
        {
            kenshin.Kind = comboBox種別.SelectedIndex;

            kenshin.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            kenshin.ConsultationDate  = dateTimePicker受診日.Value;
            if (nullableDateTimePicker有効期限.Value != null)
            {
                kenshin.ExpirationDate = (DateTime)nullableDateTimePicker有効期限.Value;
            }
            else
            {
                kenshin.ExpirationDate = DateTime.MinValue;
            }
            kenshin.Result = comboBox結果.SelectedIndex;
            kenshin.ResultDatail = textBox結果詳細.Text;

            if (parentForm != null)
            {
                if (kenshin.AlarmInfoList == null || kenshin.AlarmInfoList.Count() == 0)
                {
                    kenshin.AlarmInfoList = new List<PtAlarmInfo>();
                    kenshin.AlarmInfoList.Add(new PtAlarmInfo());
                }
                FillInstance_Alarm_新規(kenshin.AlarmInfoList[0]);
            }
            else
            {
                FillInstance_Alarm_更新(kenshin.AlarmInfoList[0]);
                FillInstance_Alarm_新規(kenshin.AlarmInfoList[1]);
            }
        }

        private void FillInstance_Alarm_新規(PtAlarmInfo alarm)
        {
            alarm.MsPortalInfoShubetuId = ((int)MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.船員).ToString();
            alarm.MsPortalInfoKubunId = ((int)MsPortalInfoKubun.MsPortalInfoKubunIdEnum.有効期限).ToString();
            alarm.MsPortalInfoKoumokuId = ((int)MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.健康診断).ToString();

            PtPortalInfoFormat infoFormat = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                infoFormat = serviceClient.PtPortalInfoFormat_GetRecordByShubet_Koumoku_Kubun(NBaseCommon.Common.LoginUser, alarm.MsPortalInfoShubetuId, alarm.MsPortalInfoKoumokuId);
            }

            alarm.Naiyou = infoFormat.Naiyou;
            alarm.Shousai = String.Format(infoFormat.Shousai, seninName, SiKenshin.KIND[kenshin.Kind]);

            if (kenshin.ExpirationDate != DateTime.MinValue)
            {
                alarm.Yuukoukigen = kenshin.ExpirationDate;
                alarm.HasseiDate = alarm.Yuukoukigen.AddDays(-30);
            }
        }


        private void FillInstance_Alarm_更新(PtAlarmInfo alarm)
        {
            alarm.AlarmShowFlag = 1;
            alarm.AlarmStopDate = DateTime.Now;
            alarm.AlarmStopUser = NBaseCommon.Common.LoginUser.MsUserID;
        }


        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                kenshin.DeleteFlag = 1;
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
            if (listBox添付ファイル.SelectedItem is SiKenshinAttachFile)
            {
                if (MessageBox.Show("選択したファイルを削除しますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SiKenshinAttachFile deleteAttach = listBox添付ファイル.SelectedItem as SiKenshinAttachFile;

                    for (int i = 0; i < kenshin.AttachFiles.Count; i++)
                    {
                        SiKenshinAttachFile attach = kenshin.AttachFiles[i];
                        if (attach.SiKenshinAttachFileID == deleteAttach.SiKenshinAttachFileID)
                        {
                            if (attach.SiKenshinID == null)
                            {
                                kenshin.AttachFiles.Remove(attach);
                            }
                            else
                            {
                                attach.DeleteFlag = 1;
                                kenshin.AttachFiles[i] = attach;
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
            if (listBox添付ファイル.SelectedItem is SiKenshinAttachFile)
            {
                SiKenshinAttachFile attach = (listBox添付ファイル.SelectedItem as SiKenshinAttachFile);
                // ファイルの表示
                NBaseCommon.FileView.View(attach.SiKenshinAttachFileID, attach.FileName, attach.Data);
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
            SiKenshinAttachFile attach = new SiKenshinAttachFile();
            attach.SiKenshinAttachFileID = System.Guid.NewGuid().ToString();

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

            kenshin.AttachFiles.Add(attach);
            listBox添付ファイル.Items.Add(attach);

        }
    }
}
