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
    public partial class 免状免許詳細Form : Form
    {
        private Form parentForm;
        private 船員詳細Panel parentPanel;

        private string seninName;
        private SiMenjou menjou;
        private bool isNew;

        //public 免状免許詳細Form(Form parentForm, string seninName)
        //    : this(parentForm, seninName, new SiMenjou(), true)
        //{
        //}


        public 免状免許詳細Form(船員詳細Panel parentpanel, string seninName)
            : this(parentpanel, seninName, new SiMenjou(), true)
        {
        }

        public 免状免許詳細Form(船員詳細Panel p, string seninName, SiMenjou menjou, bool isNew)
        {
            this.parentPanel = p;
            this.seninName = seninName;
            this.menjou = menjou;
            this.isNew = isNew;

            InitializeComponent();
            Init();
        }

        public 免状免許詳細Form(Form parentForm, string seninName, SiMenjou menjou, bool isNew)
        {
            this.parentForm = parentForm;
            this.seninName = seninName;
            this.menjou = menjou;
            this.isNew = isNew;

            InitializeComponent();
            Init();
        }

        public 免状免許詳細Form(string seninName, SiMenjou menjou, bool isNew)
        {
            this.seninName = seninName;
            this.menjou = menjou;
            this.isNew = isNew;

            InitializeComponent();
            Init();
        }

        private void Init()
        {
            InitComboBox免状();
            InitFields();
        }


        private void InitFields()
        {
            if (!isNew)
            {
                foreach (MsSiMenjou m in comboBox免状.Items)
                {
                    if (m.MsSiMenjouID == menjou.MsSiMenjouID)
                    {
                        comboBox免状.SelectedItem = m;
                        break;
                    }
                }

                Detect種別();

                for (int i = 0; i < comboBox種別.Items.Count; i++)
                {
                    if (comboBox種別.Items[i] is MsSiMenjouKind)
                    {
                        MsSiMenjouKind m = comboBox種別.Items[i] as MsSiMenjouKind;

                        if (m.MsSiMenjouKindID == menjou.MsSiMenjouKindID)
                        {
                            comboBox種別.SelectedItem = m;
                            break;
                        }
                    }
                }

                textBox番号.Text = menjou.No;

                if (menjou.Kigen != DateTime.MinValue)
                {
                    nullableDateTimePicker有効期限.Value = menjou.Kigen;
                }
                else
                {
                    nullableDateTimePicker有効期限.Value = null;
                }

                if (menjou.ShutokuDate != DateTime.MinValue)
                {
                    nullableDateTimePicker取得受講日.Value = menjou.ShutokuDate;
                }
                else
                {
                    nullableDateTimePicker取得受講日.Value = null;
                }

                if (menjou.ChouhyouFlag == 1)
                {
                    checkBox帳票出力.Checked = true;
                }
                else
                {
                    checkBox帳票出力.Checked = false;
                }

                foreach (SiMenjouAttachFile attach in menjou.AttachFiles)
                {
                    if (attach.DeleteFlag == 0)
                        listBox添付ファイル.Items.Add(attach);
                }

                if (menjou.WrittenTest == 1)
                {
                    checkBox_筆記.Checked = true;
                    label2.Visible = false;
                    panel1.Visible = false;
                }

                textBox備考.Text = menjou.Bikou;
            }
            else
            {
                menjou.AlarmInfoList = new List<PtAlarmInfo>();
                menjou.AlarmInfoList.Add(new PtAlarmInfo());

                nullableDateTimePicker有効期限.Value = null;
                nullableDateTimePicker取得受講日.Value = null;
                button削除.Enabled = false;
            }

            //if (parentForm == null)
            if(parentPanel == null && parentForm == null)
            {
                comboBox免状.Enabled = false;
                comboBox種別.Enabled = false;
                
                button削除.Enabled = false;
            }
            else if (parentForm is 免許免状一覧Form)
            {
                comboBox免状.Enabled = false;
                comboBox種別.Enabled = false;

                button削除.Enabled = false;
            }
        }


        #region private void InitComboBox免状()
        private void InitComboBox免状()
        {
            foreach (MsSiMenjou m in SeninTableCache.instance().GetMsSiMenjouList(NBaseCommon.Common.LoginUser))
            {
                comboBox免状.Items.Add(m);
            }
        }
        #endregion

        #region private void Detect種別()
        private void Detect種別()
        {
            comboBox種別.Items.Clear();

            int msSiMenjouID = (comboBox免状.SelectedItem as MsSiMenjou).MsSiMenjouID;

            foreach (MsSiMenjouKind k in SeninTableCache.instance().GetMsSiMenjouKindList(NBaseCommon.Common.LoginUser, msSiMenjouID))
            {
                comboBox種別.Items.Add(k);
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

        #region private void Save()
        private void Save()
        {
            if (ValidateFields())
            {
                FillInstance();

                #region 2021/07/21 m.yoshihara コメントアウト
                //if (parentForm != null)
                //{
                //    //// 2014.02 2013年度改造
                //    ////parentForm.Refresh免状免許(menjou);
                //    //if (parentForm is 船員詳細Panel)
                //    //{
                //    //    ((船員詳細Panel)parentForm).Refresh免状免許(menjou);
                //    //}
                //    if (parentForm is 免許免状一覧Form)
                //    {
                //        if (InsertOrUpdate())
                //        {
                //            MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            Dispose();
                //            ((免許免状一覧Form)parentForm).ReSearch();
                //        }
                //        else
                //        {
                //            MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        }
                //    }
                //}
                //else
                //{
                //    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                //    {
                //        bool result = serviceClient.BLC_免状免許_アラーム削除(NBaseCommon.Common.LoginUser, menjou);
                //    }
                //}
                
                #endregion


                if (parentPanel != null)
                {
                    if (parentPanel.InsertOrUpdate_免許免状(menjou))
                    {
                        MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    this.Cursor = Cursors.WaitCursor;

                    parentPanel.Refresh免状免許();

                    this.Cursor = Cursors.Default;

                    Dispose();
                }
                else if (parentForm != null && parentForm is 免許免状一覧Form)
                {
                    if (InsertOrUpdate())
                    {
                        MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Dispose();
                        ((免許免状一覧Form)parentForm).ReSearch();
                    }
                    else
                    {
                        MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        bool result = serviceClient.BLC_免状免許_アラーム削除(NBaseCommon.Common.LoginUser, menjou);
                    }
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
                SiMenjou retMenjou = serviceClient.BLC_免許免状管理_更新(NBaseCommon.Common.LoginUser, menjou);
                
                if (retMenjou != null)
                {
                    menjou = retMenjou;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }
        #endregion

        #region private bool ValidateFields()
        private bool ValidateFields()
        {
            if (comboBox免状.SelectedItem == null)
            {
                comboBox免状.BackColor = Color.Pink;
                MessageBox.Show("免許／免状を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox免状.BackColor = Color.White;
                return false;
            }

            if (comboBox種別.Items.Count > 0 && comboBox種別.SelectedItem == null)
            {
                comboBox種別.BackColor = Color.Pink;
                MessageBox.Show("種別を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox種別.BackColor = Color.White;
                return false;
            }
            if (checkBox_筆記.Checked == false)
            {
                if (textBox番号.Text.Length == 0)
                {
                    textBox番号.BackColor = Color.Pink;
                    MessageBox.Show("番号を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox番号.BackColor = Color.White;
                    return false;
                }

                if (textBox番号.Text.Length > 25)
                {
                    textBox番号.BackColor = Color.Pink;
                    MessageBox.Show("番号は25文字以下で入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox番号.BackColor = Color.White;
                    return false;
                }

                // 20220127 必須ではなくす
                //if (nullableDateTimePicker取得受講日.Value == null)
                //{
                //    nullableDateTimePicker取得受講日.BackColor = Color.Pink;
                //    MessageBox.Show("取得／受講日を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    nullableDateTimePicker取得受講日.BackColor = Color.White;
                //    return false;
                //}
            }


            return true;
        }
        #endregion

        private void FillInstance()
        {
            // SiMenjou
            menjou.MsSiMenjouID = (comboBox免状.SelectedItem as MsSiMenjou).MsSiMenjouID;

            if (comboBox種別.SelectedItem != null)
            {
                menjou.MsSiMenjouKindID = (comboBox種別.SelectedItem as MsSiMenjouKind).MsSiMenjouKindID;
            }

            menjou.WrittenTest = checkBox_筆記.Checked ? 1 : 0;
           
            menjou.No = textBox番号.Text;

            if (menjou.WrittenTest == 1)
            {
                menjou.Kigen = DateTime.MinValue;
                menjou.ShutokuDate = DateTime.MinValue;
                menjou.ChouhyouFlag = 0;

                if (menjou.AlarmInfoList != null && menjou.AlarmInfoList.Count() > 0)
                {
                    FillInstance_Alarm_更新(menjou.AlarmInfoList[0]);
                }
            }
            else
            {
                if (nullableDateTimePicker有効期限.Value != null)
                {
                    menjou.Kigen = (DateTime)nullableDateTimePicker有効期限.Value;
                }
                else
                {
                    menjou.Kigen = DateTime.MinValue;
                }

                if (nullableDateTimePicker取得受講日.Value != null)
                {
                    menjou.ShutokuDate = (DateTime)nullableDateTimePicker取得受講日.Value;
                }
                else
                {
                    menjou.ShutokuDate = DateTime.MinValue;
                }

                menjou.ChouhyouFlag = checkBox帳票出力.Checked ? 1 : 0;


                if(parentPanel != null || parentForm != null)
                {
                    if (menjou.AlarmInfoList == null || menjou.AlarmInfoList.Count() == 0)
                    {
                        menjou.AlarmInfoList = new List<PtAlarmInfo>();
                        menjou.AlarmInfoList.Add(new PtAlarmInfo());
                    }
                    FillInstance_Alarm_新規(menjou.AlarmInfoList[0]);
                }
                else
                {
                    FillInstance_Alarm_更新(menjou.AlarmInfoList[0]);
                    FillInstance_Alarm_新規(menjou.AlarmInfoList[1]);
                }
            }

            menjou.Bikou = StringUtils.Escape(textBox備考.Text);
        }

        #region private void FillInstance_Alarm_新規(PtAlarmInfo alarm)
        private void FillInstance_Alarm_新規(PtAlarmInfo alarm)
        {
            alarm.MsPortalInfoShubetuId = ((int)MsPortalInfoShubetu.MsPortalInfoShubetuIdEnum.船員).ToString();
            alarm.MsPortalInfoKubunId = ((int)MsPortalInfoKubun.MsPortalInfoKubunIdEnum.有効期限).ToString();
            alarm.MsPortalInfoKoumokuId = ((int)MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.免許免状).ToString();

            PtPortalInfoFormat infoFormat = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                infoFormat = serviceClient.PtPortalInfoFormat_GetRecordByShubet_Koumoku_Kubun(NBaseCommon.Common.LoginUser, alarm.MsPortalInfoShubetuId, alarm.MsPortalInfoKoumokuId);
            }

            alarm.Naiyou = infoFormat.Naiyou;
            string menjouName = SeninTableCache.instance().GetMsSiMenjouName(NBaseCommon.Common.LoginUser, menjou.MsSiMenjouID);
            string menjouKindName = SeninTableCache.instance().GetMsSiMenjouKindName(NBaseCommon.Common.LoginUser, menjou.MsSiMenjouKindID);
            alarm.Shousai = String.Format(infoFormat.Shousai, seninName, menjouName, menjouKindName);

            if (menjou.Kigen != DateTime.MinValue)
            {
                alarm.Yuukoukigen = menjou.Kigen;
                alarm.HasseiDate = alarm.Yuukoukigen.AddDays(-30);
            }
            else
            {
                alarm.Yuukoukigen = DateTime.MinValue;
                alarm.HasseiDate = DateTime.MinValue;
            }
        }
        #endregion

        #region private void FillInstance_Alarm_更新(PtAlarmInfo alarm)
        private void FillInstance_Alarm_更新(PtAlarmInfo alarm)
        {
            alarm.AlarmShowFlag = 1;
            alarm.AlarmStopDate = DateTime.Now;
            alarm.AlarmStopUser = NBaseCommon.Common.LoginUser.MsUserID;
        }
        #endregion

        #region private void button閉じる_Click(object sender, EventArgs e)
        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        #endregion

        #region private void button削除_Click(object sender, EventArgs e)
        private void button削除_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                menjou.DeleteFlag = 1;
                menjou.AlarmInfoList[0].DeleteFlag = 1;

                Save();
            }
        }
        #endregion

        #region private void comboBox免状_SelectionChangeCommitted(object sender, EventArgs e)
        private void comboBox免状_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Detect種別();
        }
        #endregion


        #region 添付ファイル操作

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
                //SiMenjouAttachFile attach = new SiMenjouAttachFile();
                //attach.SiMenjouAttachFileID = System.Guid.NewGuid().ToString();

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
                //    menjou.AttachFiles.Add(attach);
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
            if (listBox添付ファイル.SelectedItem is SiMenjouAttachFile)
            {
                if (MessageBox.Show("選択したファイルを削除しますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SiMenjouAttachFile deleteAttach = listBox添付ファイル.SelectedItem as SiMenjouAttachFile;

                    for (int i = 0; i < menjou.AttachFiles.Count; i++)
                    {
                        SiMenjouAttachFile attach = menjou.AttachFiles[i];
                        if (attach.SiMenjouAttachFileID == deleteAttach.SiMenjouAttachFileID)
                        {
                            if (attach.SiMenjouID == null)
                            {
                                menjou.AttachFiles.Remove(attach);
                            }
                            else
                            {
                                attach.DeleteFlag = 1;
                                menjou.AttachFiles[i] = attach;
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
            if (listBox添付ファイル.SelectedItem is SiMenjouAttachFile)
            {
                SiMenjouAttachFile attach = (listBox添付ファイル.SelectedItem as SiMenjouAttachFile);
                // ファイルの表示
                NBaseCommon.FileView.View(attach.SiMenjouAttachFileID, attach.FileName, attach.Data);
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
            SiMenjouAttachFile attach = new SiMenjouAttachFile();
            attach.SiMenjouAttachFileID = System.Guid.NewGuid().ToString();

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
                menjou.AttachFiles.Add(attach);
                listBox添付ファイル.Items.Add(attach);
            //}
            //else
            //{
            //    MessageBox.Show(attach.FileName + System.Environment.NewLine + "登録できるファイルのサイズは " + (ORMapping.Common.MAX_BINARY_SIZE / 1000) + " KB以下です", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        #endregion


        private void checkBox_筆記_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_筆記.Checked)
            {
                label2.Visible = false;
                panel1.Visible = false;
            }
            else
            {
                label2.Visible = true;
                panel1.Visible = true;
            }
        }
    }
}
