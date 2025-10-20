using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
//using NBaseData.BLC;
using SyncClient;
using NBaseHonsen.Document.BLC;
using NBaseCommon;
using NBaseUtil;

namespace NBaseHonsen.Document
{
    public partial class 管理記録登録Form : ExForm, IDataSyncObserver
    {
        private string DIALOG_TITLE = "管理記録登録";
        public HoukokushoKanriKiroku houkokushoKanriKiroku;

        List<DmKanriKiroku> dmKanriKirokus = new List<DmKanriKiroku>();
        public MsDmTemplateFile msDmTemplateFile;

        private DmKanriKiroku dmKanriKiroku = null;
        private DmKanriKirokuFile dmKanriKirokuFile = null;
        public List<DmPublisher> dmPublishers = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;

        //同期処理中かどうか？
        private bool SyncingFlag = false;


        public 管理記録登録Form(HoukokushoKanriKiroku houkokushoKanriKiroku)
        {
            InitializeComponent();
            this.houkokushoKanriKiroku = houkokushoKanriKiroku;
        }

        private void 管理記録登録Form_Load(object sender, EventArgs e)
        {
            InitForm();

            this.ChangeParentFormSize(FormWindowState.Minimized);
            this.Activate();
        }

        private void 管理記録登録Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ChangeParentFormSize(FormWindowState.Normal);
        }

        /// <summary>
        /// 
        /// </summary>
        #region private void InitForm()
        private void InitForm()
        {
            label_Bunrui.Text = "： " + houkokushoKanriKiroku.BunruiName;
            label_Shoubunrui.Text = "： " + houkokushoKanriKiroku.ShoubunruiName;
            label_BunshoNo.Text = "： " + houkokushoKanriKiroku.BunshoNo;
            label_BunshoName.Text = "： " + houkokushoKanriKiroku.BunshoName;

            int year = NBaseUtil.DateTimeUtils.年度開始日().Year;
            comboBox_JikiNen.Items.Add("");
            comboBox_JikiNen.Items.Add(year.ToString() + "年度");
            comboBox_JikiNen.Items.Add((--year).ToString() + "年度");
            comboBox_JikiNen.SelectedIndex = 0;
            comboBox_JikiTuki.Items.Add("");
            string checkJiki = houkokushoKanriKiroku.Jiki.Replace("0", "").Trim();

            for (int i = 0; i < NBaseUtil.DateTimeUtils.instance().MONTH.Length; i++)
            {
                if (checkJiki.Length == 0)　// 報告書マスタでチェックが１つもない場合はすべてセット
                {
                    comboBox_JikiTuki.Items.Add(NBaseUtil.DateTimeUtils.instance().MONTH[i] + "月度");
                }
                else if (houkokushoKanriKiroku.Jiki[(NBaseUtil.DateTimeUtils.instance().INT_MONTH[i] - 1)] == '1')　// 報告書マスタでチェックされている時期のみセットする
                {
                    comboBox_JikiTuki.Items.Add(NBaseUtil.DateTimeUtils.instance().MONTH[i] + "月度");
                }
            }
            comboBox_JikiTuki.SelectedIndex = 0;

            nullableDateTimePicker_IssueDate.Value = DateTime.Now;
            nullableDateTimePicker_IssueDate.Value = null;


            dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, 12);
            try
            {
                Cursor = Cursors.WaitCursor;

                if (houkokushoKanriKiroku.TemplateFileName != null && houkokushoKanriKiroku.TemplateFileName.Length > 0)
                {
                    msDmTemplateFile = MsDmTemplateFile.GetRecordByHoukokushoID(同期Client.LOGIN_USER, houkokushoKanriKiroku.MsDmHoukokushoID);
                }
                dmKanriKirokus = DmKanriKiroku.GetPastRecords(同期Client.LOGIN_USER, houkokushoKanriKiroku.MsDmHoukokushoID, 同期Client.LOGIN_VESSEL.MsVesselID);

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(DmKanriKiroku)));
                dt.Columns.Add(new DataColumn("発行日", typeof(string)));
                dt.Columns.Add(new DataColumn("状況", typeof(string)));
                dt.Columns.Add(new DataColumn("備考", typeof(string)));

                foreach (DmKanriKiroku kanriKiroku in dmKanriKirokus)
                {
                    DataRow row = dt.NewRow();
                    row["obj"] = kanriKiroku;
                    row["発行日"] = kanriKiroku.IssueDate.ToShortDateString();
                    row["状況"] = kanriKiroku.StatusStr;
                    row["備考"] = kanriKiroku.Bikou;
                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 100;  //発行日
                dataGridView1.Columns[2].Width = 120;  //状況
                dataGridView1.Columns[3].Width = 500;  //備考

            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        /// <summary>
        /// 「参照」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_参照_Click(object sender, EventArgs e)
        private void button_参照_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "管理記録ファイル(*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                textBox_FileName.Text = openFileDialog1.FileName;
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
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_閉じる_Click(object sender, EventArgs e)
        private void button_閉じる_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        /// <summary>
        /// 「報告書登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_報告書登録_Click(object sender, EventArgs e)
        private void button_報告書登録_Click(object sender, EventArgs e)
        {
            if (SyncingFlag)
            {
                MessageBox.Show("データ同期中です。しばらくお待ちください。", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 入力確認
            if (Validation() == false)
            {
                return;
            }

            // 「乗船リスト」画面を表示し、乗船者の確認（登録）を実施する
            船員リストForm form = new 船員リストForm();
            form.ShowDialog();
            List<string> checkedUserIds = form.CheckedUserIds;

            // 情報をセットする
            FillInstance(checkedUserIds);

            // 登録処理
            bool ret = true;
            if (dmKanriKiroku.DmKanriKirokuID != null && dmKanriKiroku.DmKanriKirokuID.Length > 0)
            {
                ret = 管理記録処理.Honsen更新(同期Client.LOGIN_USER, dmKanriKiroku, dmKanriKirokuFile, dmPublishers);
            }
            else
            {
                ret = 管理記録処理.Honsen登録(同期Client.LOGIN_USER, dmKanriKiroku, dmKanriKirokuFile, dmPublishers);
            }

            if (ret == true)
            {
                MessageBox.Show("更新しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("更新に失敗しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.ChangeFlag = false;

        }
        #endregion

        /// <summary>
        /// 「雛形ファイルダウンロード」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_雛形ダウンロード_Click(object sender, EventArgs e)
        private void button_雛形ダウンロード_Click(object sender, EventArgs e)
        {
            // "雛形ファイル"の確認
            if (msDmTemplateFile == null)
            {
                MessageBox.Show("雛形ファイルが存在しません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // "雛形ファイル"を保存する
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "雛形ファイル(*.*)|*.*";
            fd.FileName = msDmTemplateFile.TemplateFileName;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream filest = new System.IO.FileStream(fd.FileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                filest.Write(msDmTemplateFile.Data, 0, msDmTemplateFile.Data.Length);
                filest.Close();

                filest.Dispose();
            }
        }
        #endregion

        /// <summary>
        /// 「報告書ダウンロード」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_報告書ダウンロード_Click(object sender, EventArgs e)
        private void button_報告書ダウンロード_Click(object sender, EventArgs e)
        {
            // 「過去のファイル」一覧で、行が選択されているかの確認
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("報告書が選択されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 選択された"管理記録ファイル"をＤＢから取得する
            DmKanriKiroku kanriKiroku = dataGridView1.SelectedRows[0].Cells[0].Value as DmKanriKiroku;
            DmKanriKirokuFile kanriKirokuFile = null;
            kanriKirokuFile = DmKanriKirokuFile.GetRecordByKanriKirokuID(同期Client.LOGIN_USER, kanriKiroku.DmKanriKirokuID);
            if (kanriKirokuFile == null)
            {
                MessageBox.Show("報告書ファイルが存在しません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // "管理記録ファイル"を保存する
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "報告書ファイル(*.*)|*.*";
            fd.FileName = kanriKirokuFile.FileName;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream filest = new System.IO.FileStream(fd.FileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                filest.Write(kanriKirokuFile.Data, 0, kanriKirokuFile.Data.Length);
                filest.Close();
            }
        }
        #endregion

        /// <summary>
        /// 入力値のチェック
        /// </summary>
        /// <returns></returns>
        #region private bool Validation()
        private bool Validation()
        {
            if (comboBox_JikiNen.SelectedIndex == 0)
            {
                MessageBox.Show("提出時期が設定されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (comboBox_JikiTuki.SelectedIndex == 0)
            {
                MessageBox.Show("提出時期が設定されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (nullableDateTimePicker_IssueDate.Value == null)
            {
                MessageBox.Show("発行日が設定されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox_FileName.Text.Length == 0)
            {
                MessageBox.Show("ファイルが選択されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox_FileName.Text.Length > 0)
            {
                if (System.IO.File.Exists(textBox_FileName.Text) == false)
                {
                    MessageBox.Show("指定されたファイルが存在しません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (NBaseCommon.FileView.CheckFileNameLength(textBox_FileName.Text) == false)
            {
                MessageBox.Show("指定されたファイルのファイル名が長すぎます", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            int MaxSize = 1;
            SnParameter snParameter = SnParameter.GetRecord(同期Client.LOGIN_USER);
            MaxSize = int.Parse(snParameter.Prm3);
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(textBox_FileName.Text, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                if (fs.Length > (MaxSize * 1024 * 1024))
                {
                    MessageBox.Show("指定されたファイルのサイズが制限値 " + MaxSize + " MByteを超えています", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (fs.Length == 0)
                {
                    MessageBox.Show("指定されたファイルのサイズが 0 Byteです", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("対象ファイルを開けません：" + Ex.Message, DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //
            //　残件：上書き登録条件の確認
            //        ①同一ファイル名かつ、同一ファイル更新日
            //        ②自部署（NBaseHonsenの場合、自船の確認状況しかないこと
            //        上書きした場合、確認状況の削除フラグを立てること
            //
            string issueDate = ((DateTime)nullableDateTimePicker_IssueDate.Value).ToShortDateString();
            string fileName = System.IO.Path.GetFileName(textBox_FileName.Text);
            DmKanriKiroku sameKanriKiroku = null;
            foreach (DmKanriKiroku kanriKiroku in dmKanriKirokus)
            {
                if (kanriKiroku.IssueDate.ToShortDateString() == issueDate && kanriKiroku.FileName == fileName)
                {
                    // 同一文書番号、同一発行日、同一ファイル名のデータあり
                    sameKanriKiroku = kanriKiroku;
                    break;
                }
            }
            if (sameKanriKiroku != null)
            {
                // 自部門以外の確認がある場合、登録はできない
                List<DmKakuninJokyo> check = DmKakuninJokyo.GetRecordsByLinkSaki(同期Client.LOGIN_USER, (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録, sameKanriKiroku.DmKanriKirokuID);
                bool isChecked = false;
                foreach (DmKakuninJokyo kj in check)
                {
                    if (kj.KakuninDate == DateTime.MinValue)
                    {
                        continue;
                    }
                    if (kj.MsVesselID == 0 || kj.MsVesselID != 同期Client.LOGIN_VESSEL.MsVesselID)
                    {
                        isChecked = true;
                        break;
                    }
                }
                if (isChecked)
                {
                    MessageBox.Show("既にファイルが登録されています", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (MessageBox.Show("同一文書が存在します。上書きを実施しますか？", DIALOG_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return false;
                }


                dmKanriKiroku = sameKanriKiroku;
            }

            return true;
        }
        #endregion


        /// <summary>
        /// "管理記録(DmKanriKiroku)"情報をセットする
        /// </summary>
        #region private void FillInstance(List<string> uids)
        private void FillInstance(List<string> uids)
        {
            try
            {
                if (dmKanriKiroku == null)
                {
                    dmKanriKiroku = new DmKanriKiroku();
                    dmKanriKirokuFile = new DmKanriKirokuFile();
                    dmPublishers = new List<DmPublisher>();
                }
                else
                {
                    dmKanriKirokuFile = DmKanriKirokuFile.GetRecordByKanriKirokuID(同期Client.LOGIN_USER, dmKanriKiroku.DmKanriKirokuID);
                    if (dmKanriKirokuFile == null)
                    {
                        dmKanriKirokuFile = new DmKanriKirokuFile();
                    }
                    dmPublishers = new List<DmPublisher>();
                }
                FillKanriKirokuFile();
                FillPublishers(uids);

                dmKanriKiroku.MsDmHoukokushoID = houkokushoKanriKiroku.MsDmHoukokushoID;
                dmKanriKiroku.Status = (int)NBaseData.DS.DocConstants.StatusEnum.未完了;
                dmKanriKiroku.JikiNen = int.Parse((comboBox_JikiNen.SelectedItem as string).Replace("年度", ""));
                dmKanriKiroku.JikiTuki = int.Parse((comboBox_JikiTuki.SelectedItem as string).Replace("月度","").Trim());
                dmKanriKiroku.IssueDate = (DateTime)nullableDateTimePicker_IssueDate.Value;
                //dmKanriKiroku.Bikou = textBox_Bikou.Text;
                dmKanriKiroku.Bikou = StringUtils.Escape(textBox_Bikou.Text);
                dmKanriKiroku.FileName = dmKanriKirokuFile.FileName;
                dmKanriKiroku.FileUpdateDate = dmKanriKirokuFile.UpdateDate;


                dmKanriKiroku.BunruiName = houkokushoKanriKiroku.BunruiName;
                dmKanriKiroku.ShoubunruiName = houkokushoKanriKiroku.ShoubunruiName;
                dmKanriKiroku.BunshoNo = houkokushoKanriKiroku.BunshoNo;
                dmKanriKiroku.BunshoName = houkokushoKanriKiroku.BunshoName;
            }
            catch
            {
                return;
            }
            return;
        }
        #endregion
        /// <summary>
        /// "管理記録ファイル(DmKanriKirokuFile)"情報をセットする
        /// </summary>
        #region private void FillKanriKirokuFile()
        private void FillKanriKirokuFile()
        {
            string FullPath = textBox_FileName.Text;

            // ファイル情報をセット
            dmKanriKirokuFile.FileName = System.IO.Path.GetFileName(FullPath);
            dmKanriKirokuFile.UpdateDate = System.IO.File.GetLastWriteTime(FullPath); // 更新日時

            // ファイルを読み込む
            System.IO.FileStream fs = new System.IO.FileStream(FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            dmKanriKirokuFile.Data = new byte[fs.Length];
            fs.Read(dmKanriKirokuFile.Data, 0, dmKanriKirokuFile.Data.Length);
            fs.Close();
        }
        #endregion
        /// <summary>
        /// "登録者(DmPublisher)"情報をセットする
        /// </summary>
        /// <param name="uids">乗船リストでチェックされた人のMsUserId</param>
        #region private void FillPublishers(List<string> uids)
        private void FillPublishers(List<string> uids)
        {
            bool isExistsCheckedUser = false;
            int showOrder = 1;
            foreach (string uid in uids)
            {
                if (uid == 同期Client.LOGIN_USER.MsUserID)
                {
                    isExistsCheckedUser = true;
                }
                DmPublisher publisher = new DmPublisher();
                publisher.LinkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船;
                publisher.MsVesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
                publisher.MsUserID = uid;
                publisher.ShowOrder = showOrder;
                dmPublishers.Add(publisher);

                showOrder++;
            }
            if (isExistsCheckedUser == false)
            {
                DmPublisher publisher = new DmPublisher();
                publisher.LinkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船;
                publisher.MsVesselID = 同期Client.LOGIN_VESSEL.MsVesselID;
                publisher.MsUserID = 同期Client.LOGIN_USER.MsUserID;
                publisher.ShowOrder = showOrder;
                dmPublishers.Add(publisher);
            }
        }
        #endregion


        #region IDataSyncObserver メンバ

        public void SyncStart()
        {
            try
            {
                Invoke(new MethodInvoker(
                    delegate()
                    {
                        SyncingFlag = true;
                    }
                ));
            }
            catch (InvalidOperationException e)
            {
            }
        }

        public void SyncFinish()
        {
            try
            {
                Invoke(new MethodInvoker(
                    delegate()
                    {
                        SyncingFlag = false;
                    }
                ));
            }
            catch (InvalidOperationException e)
            {
            }
        }

        public void Online()
        {
        }

        public void Offline()
        {
        }

        public void Message(string message)
        {
        }

        public void Message2(string message)
        {
        }

        public void Message3(string message)
        {
        }

        #endregion

        private void textBox_FileName_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void textBox_FileName_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    textBox_FileName.Text = fileName;
                }
            }
        }
    }
}
