using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using ServiceReferences.NBaseService;
using NBaseCommon;
using NBaseData.DS;

namespace Document
{
    public partial class 管理記録登録Form : UserControl
    {
        private string DIALOG_TITLE = "管理記録登録";
        public HoukokushoKanriKiroku houkokushoKanriKiroku;

        List<DmKanriKiroku> dmKanriKirokus = new List<DmKanriKiroku>();
        public MsDmTemplateFile msDmTemplateFile;

        private DmKanriKiroku dmKanriKiroku = null;
        private DmKanriKirokuFile dmKanriKirokuFile = null;
        public List<DmPublisher> dmPublishers = null;
        public List<DmKoukaiSaki> dmKoukaiSakis = null;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 管理記録登録Form()
        {
            InitializeComponent();
        }

        private void 管理記録登録Form_Load(object sender, EventArgs e)
        {
        }


        public void Clear()
        {
            houkokushoKanriKiroku = null;

            dmKanriKirokus.Clear();
            msDmTemplateFile = null;

            dmKanriKiroku = null;
            dmKanriKirokuFile = null;
            dmPublishers = null;
            dmKoukaiSakis = null;

            comboBox_JikiNen.Items.Clear();
            comboBox_JikiTuki.Items.Clear();

            nullableDateTimePicker_IssueDate.Value = DateTime.Now;
            nullableDateTimePicker_IssueDate.Value = null;

            textBox_FileName.Text = null;
            textBox_Bikou.Text = null;

            dataGridView1.DataSource = null;
       }

        /// <summary>
        /// 
        /// </summary>
        #region public void InitForm(HoukokushoKanriKiroku houkokushoKanriKiroku)
        public void InitForm(HoukokushoKanriKiroku houkokushoKanriKiroku)
        {
            Clear();

            this.houkokushoKanriKiroku = houkokushoKanriKiroku;

            //label_Bunrui.Text = "： " + houkokushoKanriKiroku.BunruiName;
            //label_Shoubunrui.Text = "： " + houkokushoKanriKiroku.ShoubunruiName;
            //label_BunshoNo.Text = "： " + houkokushoKanriKiroku.BunshoNo;
            //label_BunshoName.Text = "： " + houkokushoKanriKiroku.BunshoName;

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

            try
            {
                Cursor = Cursors.WaitCursor;

                int role = 0;
                if (NBaseCommon.Common.LoginUser.DocFlag_CEO == (int)NBaseData.DS.DocConstants.FlagEnum.ON) role = (int)DocConstants.RoleEnum.経営責任者;
                else if (NBaseCommon.Common.LoginUser.DocFlag_Admin == (int)NBaseData.DS.DocConstants.FlagEnum.ON) role = (int)DocConstants.RoleEnum.管理責任者;
                else if (NBaseCommon.Common.LoginUser.DocFlag_MsiFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON) role = (int)DocConstants.RoleEnum.海務監督;
                else if (NBaseCommon.Common.LoginUser.DocFlag_CrewFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON) role = (int)DocConstants.RoleEnum.船員担当者;
                else if (NBaseCommon.Common.LoginUser.DocFlag_TsiFerry == (int)NBaseData.DS.DocConstants.FlagEnum.ON) role = (int)DocConstants.RoleEnum.工務監督;
                else if (NBaseCommon.Common.LoginUser.DocFlag_Admin == (int)NBaseData.DS.DocConstants.FlagEnum.ON) role = (int)DocConstants.RoleEnum.役員;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    if (houkokushoKanriKiroku.TemplateFileName != null && houkokushoKanriKiroku.TemplateFileName.Length > 0)
                    {
                        msDmTemplateFile = serviceClient.MsDmTemplateFile_GetRecordByHoukokushoID(NBaseCommon.Common.LoginUser, houkokushoKanriKiroku.MsDmHoukokushoID);
                    }
                    dmKanriKirokus = serviceClient.DmKanriKiroku_GetPastRecords(NBaseCommon.Common.LoginUser, houkokushoKanriKiroku.MsDmHoukokushoID, NBaseCommon.Common.LoginUser.MsUserID, role);
                }

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
                dataGridView1.Columns[2].Width = 75;   //状況
                dataGridView1.Columns[3].Width = 250;  //備考

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
        /// 「報告書登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_報告書登録_Click(object sender, EventArgs e)
        private void button_報告書登録_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                return;
            }

            FillInstance();

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (dmKanriKiroku.DmKanriKirokuID != null && dmKanriKiroku.DmKanriKirokuID.Length > 0)
                {
                    ret = serviceClient.BLC_管理記録処理_更新(NBaseCommon.Common.LoginUser, dmKanriKiroku, dmKanriKirokuFile);
                }
                else
                {
                    ret = serviceClient.BLC_管理記録処理_登録(NBaseCommon.Common.LoginUser, dmKanriKiroku, dmKanriKirokuFile, dmPublishers);
                }
            }
            this.ChangeFlag = false;

            if (ret == true)
            {
                MessageBox.Show("更新しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);


                // 自身を再表示
                InitForm(houkokushoKanriKiroku);
            }
            else
            {
                MessageBox.Show("更新に失敗しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        #endregion

        /// <summary>
        /// 「雛形ダウンロード」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_雛形ダウンロード_Click(object sender, EventArgs e)
        private void button_雛形ダウンロード_Click(object sender, EventArgs e)
        {
            if (msDmTemplateFile == null)
            {
                MessageBox.Show("雛形ファイルが存在しません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "雛形ファイル(*.*)|*.*";
            fd.FileName = msDmTemplateFile.TemplateFileName;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream filest = new System.IO.FileStream(fd.FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                filest.Write(msDmTemplateFile.Data, 0, msDmTemplateFile.Data.Length);
                filest.Close();
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
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("報告書が選択されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DmKanriKiroku kanriKiroku = dataGridView1.SelectedRows[0].Cells[0].Value as DmKanriKiroku;
            DmKanriKirokuFile kanriKirokuFile = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                kanriKirokuFile = serviceClient.DmKanriKirokuFile_GetRecordByKanriKirokuID(NBaseCommon.Common.LoginUser, kanriKiroku.DmKanriKirokuID);
            }
            if (kanriKirokuFile == null)
            {
                MessageBox.Show("報告書ファイルが存在しません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "報告書ファイル(*.*)|*.*";
            fd.FileName = kanriKirokuFile.FileName;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream filest = new System.IO.FileStream(fd.FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
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
                if (NBaseCommon.FileView.CheckFileNameLength(textBox_FileName.Text) == false)
                {
                    MessageBox.Show("指定されたファイルのファイル名が長すぎます", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                int MaxSize = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    SnParameter snParameter = serviceClient.SnParameter_GetRecord(NBaseCommon.Common.LoginUser);
                    MaxSize = int.Parse(snParameter.Prm3);
                }
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
            }

            //
            //　上書き登録条件の確認
            //        ①同一ファイル名かつ、同一ファイル更新日
            //        ②自部署（HonsenWingの場合、自船の確認状況しかないこと
            //        上書きした場合、確認状況の削除フラグを立てること
            //
            // 同一文書番号、同一発行日、同一ファイル名のデータがある場合、登録は不可
            string issueDate = ((DateTime)nullableDateTimePicker_IssueDate.Value).ToShortDateString();
            string fileName = System.IO.Path.GetFileName(textBox_FileName.Text);
            DmKanriKiroku sameKanriKiroku = null;
            foreach(DmKanriKiroku kanriKiroku in dmKanriKirokus)
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
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    List<DmKakuninJokyo> check = serviceClient.DmKakuninJokyo_GetRecordsByLinkSaki(NBaseCommon.Common.LoginUser, (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録, sameKanriKiroku.DmKanriKirokuID);
                    bool isChecked = false;
                    foreach (DmKakuninJokyo kj in check)
                    {
                        if (kj.KakuninDate == DateTime.MinValue)
                        {   
                            continue;
                        }
                        if (kj.MsVesselID > 0 || kj.MsBumonID != NBaseCommon.Common.LoginUser.BumonID)
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
                }
                dmKanriKiroku = sameKanriKiroku;
            }
            return true;
        }
        #endregion


        /// <summary>
        /// "管理記録(DmKanrikiroku)"情報をセットする
        /// </summary>  
        #region private void FillInstance()
        private void FillInstance()
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
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        dmKanriKirokuFile = serviceClient.DmKanriKirokuFile_GetRecordByKanriKirokuID(NBaseCommon.Common.LoginUser, dmKanriKiroku.DmKanriKirokuID);
                    }
                    if (dmKanriKirokuFile == null)
                    {
                        dmKanriKirokuFile = new DmKanriKirokuFile();
                    }
                    dmPublishers = new List<DmPublisher>();
                }
                dmKanriKiroku.MsDmHoukokushoID = houkokushoKanriKiroku.MsDmHoukokushoID;
                dmKanriKiroku.Status = (int)NBaseData.DS.DocConstants.StatusEnum.未完了;
                dmKanriKiroku.JikiNen = int.Parse((comboBox_JikiNen.SelectedItem as string).Replace("年度", ""));
                dmKanriKiroku.JikiTuki = int.Parse((comboBox_JikiTuki.SelectedItem as string).Replace("月度","").Trim());
                dmKanriKiroku.IssueDate = (DateTime)nullableDateTimePicker_IssueDate.Value;
                dmKanriKiroku.Bikou = textBox_Bikou.Text;

                dmKanriKiroku.BunruiName = houkokushoKanriKiroku.BunruiName;
                dmKanriKiroku.ShoubunruiName = houkokushoKanriKiroku.ShoubunruiName;
                dmKanriKiroku.BunshoNo = houkokushoKanriKiroku.BunshoNo;
                dmKanriKiroku.BunshoName = houkokushoKanriKiroku.BunshoName;
                
                FillKanriKirokuFile();
                FillKoukaiSakis();
                FillPublishers();
            }
            catch
            {
                return;
            }
            return;
        }
        #endregion

        /// <summary>
        /// "管理記録ファイル(DmKanrikirokuFile)"情報をセットする
        /// </summary>
        #region private void FillKanriKirokuFile()
        private void FillKanriKirokuFile()
        {
            string FullPath = textBox_FileName.Text;
            dmKanriKiroku.FileName = System.IO.Path.GetFileName(FullPath);
            dmKanriKiroku.FileUpdateDate = System.IO.File.GetLastWriteTime(FullPath); // 更新日時

            dmKanriKirokuFile.FileName = dmKanriKiroku.FileName;
            dmKanriKirokuFile.UpdateDate = dmKanriKiroku.FileUpdateDate;
            System.IO.FileStream fs = new System.IO.FileStream(FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            dmKanriKirokuFile.Data = new byte[fs.Length];
            fs.Read(dmKanriKirokuFile.Data, 0, dmKanriKirokuFile.Data.Length);
            fs.Close();
        }
        #endregion

        /// <summary>
        /// "公開先(DmKoukaiSaki)"情報をセットする
        /// </summary>
        #region private void FillKoukaiSakis()
        private void FillKoukaiSakis()
        {
            // 公開先は、サーバ側処理内で、報告書に設定されている公開先をコピーする
        }
        #endregion

        /// <summary>
        /// "登録者(DmPublisher)"情報をセットする
        /// </summary>
        #region private void FillPublishers()
        private void FillPublishers()
        {
            DmPublisher publisher = new DmPublisher();
            publisher.LinkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;

            //==============================================
            // 発行元を決定する
            //==============================================
            bool setKoukaisaki = false;

            {
                List<DmPublisher> houkokushoPublishers = null;
                try
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        houkokushoPublishers = serviceClient.DmPublisher_GetRecordsByLinkSakiID(NBaseCommon.Common.LoginUser, houkokushoKanriKiroku.MsDmHoukokushoID);
                    }
                }
                catch
                {
                }

                if (houkokushoPublishers.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者) && NBaseCommon.Common.LoginUser.DocFlag_Admin == 1)
                {
                    // 管理責任者が優先度は一番上
                    publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者;
                    setKoukaisaki = true;
                }
                else if (houkokushoPublishers.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.経営責任者) && NBaseCommon.Common.LoginUser.DocFlag_CEO == 1)
                {
                    publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.経営責任者;
                    setKoukaisaki = true;
                }
                else if (houkokushoPublishers.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.海務監督) && NBaseCommon.Common.LoginUser.DocFlag_MsiFerry == 1)
                {
                    publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.海務監督;
                    setKoukaisaki = true;
                }
                else if (houkokushoPublishers.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船員担当者) && NBaseCommon.Common.LoginUser.DocFlag_CrewFerry == 1)
                {
                    publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船員担当者;
                    setKoukaisaki = true;
                }
                else if (houkokushoPublishers.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.工務監督) && NBaseCommon.Common.LoginUser.DocFlag_TsiFerry == 1)
                {
                    publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.工務監督;
                    setKoukaisaki = true;
                }
                else if (houkokushoPublishers.Any(o => o.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.役員) && NBaseCommon.Common.LoginUser.DocFlag_Officer == 1)
                {
                    publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.役員;
                    setKoukaisaki = true;
                }
            }

            // 管理記録の発行元がセットされなかった場合、"部門" とする
            if (setKoukaisaki == false)
            {
                publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.部門;
                publisher.MsBumonID = NBaseCommon.Common.LoginUser.BumonID;
            }
            publisher.MsUserID = NBaseCommon.Common.LoginUser.MsUserID;
            dmPublishers.Add(publisher);
        }
        #endregion

        private void textBox_FileName_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void textBox_FileName_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                foreach (string fileName in (string[])e.Data.GetData(DataFormats.FileDrop)) {
                    textBox_FileName.Text = fileName;
                }
            }
        }
    }
}
