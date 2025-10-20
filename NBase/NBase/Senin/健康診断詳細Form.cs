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
        private 船員詳細Panel parentPanel;
        private MsSenin senin;
        private SiKenshin kenshin;
        private bool isNew;



        public 健康診断詳細Form(船員詳細Panel parentForm, MsSenin senin)
            : this(parentForm, senin, new SiKenshin(), true)
        {
        }


        public 健康診断詳細Form(船員詳細Panel parentPanel, MsSenin senin, SiKenshin kenshin, bool isNew)
        {
            this.parentPanel = parentPanel;
            this.senin = senin;
            this.kenshin = kenshin;
            this.isNew = isNew;

            InitializeComponent();
            Init();
        }

        private void Init()
        {
            InitComboBox種別();
            InitComboBox判定();
            InitFields();
        }


        private void InitComboBox種別()
        {
            foreach (MsSiOptions o in SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser,(int)MsSiOptions.EnumSelecterID.検診_検査名))
            {
                comboBox検査名.Items.Add(o);
            }
        }

        private void InitComboBox判定()
        {
            foreach (MsSiOptions o in SeninTableCache.instance().GetMsSiOptionsList(NBaseCommon.Common.LoginUser, (int)MsSiOptions.EnumSelecterID.検診_判定))
            {
                comboBox判定.Items.Add(o);
            }
        }

        private void InitFields()
        {
            if (!isNew)
            {
                textBox病院名.Text = kenshin.Hospital;
                foreach (MsSiOptions o in comboBox検査名.Items)
                {
                    if (o.MsSiOptionsID == kenshin.MsSiOptionsID)
                    {
                        comboBox検査名.SelectedItem = o;
                        break;
                    }
                }
                if (kenshin.KensaDay == DateTime.MinValue)
                {
                    nullableDateTimePicker検査日.Value = null;
                }
                else
                {
                    nullableDateTimePicker検査日.Value = kenshin.KensaDay;
                }
                if (kenshin.ExpiryDate == DateTime.MinValue)
                {
                    nullableDateTimePicker有効期限.Value = null;
                }
                else
                {
                    nullableDateTimePicker有効期限.Value = kenshin.ExpiryDate;
                }
                textBox診断名.Text = kenshin.Diagnosis;
                foreach (MsSiOptions o in comboBox判定.Items)
                {
                    if (o.MsSiOptionsID == kenshin.Results)
                    {
                        comboBox判定.SelectedItem = o;
                        break;
                    }
                }


                foreach (SiKenshinAttachFile attach in kenshin.AttachFiles)
                {
                    if (attach.DeleteFlag == 0)
                        listBox添付ファイル.Items.Add(attach);
                }
               
                textBoxその他.Text = kenshin.Remarks;
            }
            else
            {
                button削除.Enabled = false;
            }

            if (parentPanel == null)
            {
                button削除.Enabled = false;
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

                if (parentPanel.InsertOrUpdate_健康診断(kenshin))
                {
                    MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                parentPanel.Refresh健康診断();

                Dispose();
            }
        }

        private bool ValidateFields()
        {
            if (!(comboBox検査名.SelectedItem is MsSiOptions))
            {
                comboBox検査名.BackColor = Color.Pink;
                MessageBox.Show("種別を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox検査名.BackColor = Color.White;
                return false;
            }

            if (nullableDateTimePicker検査日.Value == null)
            {
                nullableDateTimePicker検査日.BackColor = Color.Pink;
                MessageBox.Show("診断日を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nullableDateTimePicker検査日.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            if (comboBox検査名.SelectedItem is MsSiOptions)
            {
                kenshin.MsSiOptionsID = (comboBox検査名.SelectedItem as MsSiOptions).MsSiOptionsID;
                kenshin.KensaName = (comboBox検査名.SelectedItem as MsSiOptions).Name;
            }
            kenshin.Hospital = textBox病院名.Text;
            if (nullableDateTimePicker検査日.Value is DateTime)
            {
                kenshin.KensaDay = DateTimeUtils.ToFrom((DateTime)nullableDateTimePicker検査日.Value);
            }
            if (nullableDateTimePicker有効期限.Value is DateTime)
            {
                kenshin.ExpiryDate = DateTimeUtils.ToTo((DateTime)nullableDateTimePicker有効期限.Value).AddSeconds(-1);
            }
            else
            {
                kenshin.ExpiryDate = DateTime.MinValue;
            }

            kenshin.Diagnosis = textBox診断名.Text;
            if (comboBox判定.SelectedItem is MsSiOptions)
            {
                kenshin.Results = (comboBox判定.SelectedItem as MsSiOptions).MsSiOptionsID;
            }
            kenshin.Remarks = textBoxその他.Text;


            // PtAlarmInfo
            if (parentPanel != null)
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

            kenshin.EditFlag = true;
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
            alarm.Shousai = String.Format(infoFormat.Shousai, senin.Sei + " " + senin.Mei, kenshin.KensaName);

            if (kenshin.ExpiryDate != DateTime.MinValue)
            {
                alarm.Yuukoukigen = kenshin.ExpiryDate;
                alarm.HasseiDate = alarm.Yuukoukigen.AddDays(-30);
            }
            else
            {
                alarm.Yuukoukigen = DateTime.MinValue;
                alarm.HasseiDate = DateTime.MinValue;
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
                if (kenshin.AlarmInfoList != null)
                {
                    foreach (PtAlarmInfo alarm in kenshin.AlarmInfoList)
                    {
                        alarm.DeleteFlag = 1;
                    }
                }
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

                SiKenshinAttachFile attach = new SiKenshinAttachFile();
                attach.SiKenshinAttachFileID = System.Guid.NewGuid().ToString();

                // ファイル情報をセット
                string FullPath = openFileDialog1.FileName;
                attach.FileName = System.IO.Path.GetFileName(FullPath);
                attach.UpdateDate = System.IO.File.GetLastWriteTime(FullPath); // 更新日時

                // ファイルを読み込む
                System.IO.FileStream fs = new System.IO.FileStream(FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                attach.Data = new byte[fs.Length];
                fs.Read(attach.Data, 0, attach.Data.Length);
                fs.Close();

                if (FileUtils.SizeCheck(attach.Data, ORMapping.Common.MAX_BINARY_SIZE) == true)
                {
                    kenshin.AttachFiles.Add(attach);
                    listBox添付ファイル.Items.Add(attach);
                }
                else
                {
                    MessageBox.Show("登録できるファイルのサイズは " + (ORMapping.Common.MAX_BINARY_SIZE / 1000) + " KB以下です", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
