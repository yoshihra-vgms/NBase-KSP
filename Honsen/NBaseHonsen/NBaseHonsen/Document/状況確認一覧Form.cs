using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using NBaseData.DAC;
using NBaseData.BLC;
using SyncClient;
using ORMapping;
using NBaseHonsen.Document.BLC;
using NBaseCommon;
using NBaseData.DS;

namespace NBaseHonsen.Document
{
    public partial class 状況確認一覧Form : ExForm
    {
        private string DIALOG_TITLE = "確認状況一覧";
        List<MsDmBunrui> msDmBunruis = new List<MsDmBunrui>();
        List<MsDmShoubunrui> msDmShoubunruis = new List<MsDmShoubunrui>();
        List<状況確認一覧Row> 状況確認一覧Rows = null;
        private const int col文書名 = 6;
        private const int colObj = 10;

        状況確認詳細Form 状況確認詳細form = null;
        private static bool isCallBack = true;

        public 状況確認一覧Form()
        {
            InitializeComponent();
        }

        private void 状況確認一覧Form_Load(object sender, EventArgs e)
        {
            msDmBunruis = MsDmBunrui.GetRecords(同期Client.LOGIN_USER);
            msDmShoubunruis = MsDmShoubunrui.GetRecords(同期Client.LOGIN_USER);
            SetBunruiDDL();
            SetShoubunruiDDL();
            SetPublisherDDL();
            SetStatusDDL();

            nullableDateTimePicker_IssueDate1.Value = DateTime.Now;
            nullableDateTimePicker_IssueDate1.Value = null;
            nullableDateTimePicker_IssueDate2.Value = DateTime.Now;
            nullableDateTimePicker_IssueDate2.Value = null;
            SetButton();

            this.ChangeParentFormSize(FormWindowState.Minimized);
            this.Activate();
        }

        private void 状況確認一覧Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ChangeParentFormSize(FormWindowState.Normal);
        }

        /// <summary>
        /// 「分類」DDL選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox_Bunrui_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox_Bunrui_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetShoubunruiDDL();
        }
        #endregion

        /// <summary>
        /// 「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Search_button_Click(object sender, EventArgs e)
        private void Search_button_Click(object sender, EventArgs e)
        {
            Search();
        }
        #endregion

        /// <summary>
        /// 「検索条件クリア」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Clear_button_Click(object sender, EventArgs e)
        private void Clear_button_Click(object sender, EventArgs e)
        {
            comboBox_Bunrui.SelectedIndex = 0;
            comboBox_Shoubunrui.SelectedIndex = 0;
            textBox_BunshoNo.Text = "";
            textBox_BunshoName.Text = "";
            textBox_Bikou.Text = "";
            textBox_Keyword.Text = "";
            nullableDateTimePicker_IssueDate1.Value = DateTime.Now;
            nullableDateTimePicker_IssueDate1.Value = null;
            nullableDateTimePicker_IssueDate2.Value = DateTime.Now;
            nullableDateTimePicker_IssueDate2.Value = null;
            comboBox_Publisher.SelectedIndex = 0;
            comboBox_Status.SelectedIndex = 0;
            checkBox_Status.Checked = false;
        }
        #endregion

        /// <summary>
        /// 「内容確認」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_内容確認_Click(object sender, EventArgs e)
        private void button_内容確認_Click(object sender, EventArgs e)
        {
            if (選択行確認() == false)
            {
                return;
            }
            if (表示確認() == false)
            {
                return;
            }

            // 「乗船リスト」画面を表示して、確認者を取得する
            船員リストForm form = new 船員リストForm();
            form.ShowDialog();
            List<string> checkedUserIds = form.CheckedUserIds;

            // 「乗船リスト」画面でチェックされたユーザすべてに確認状況情報(DmKakuninJokyo)を作成する
            状況確認一覧Row row = dataGridView1.SelectedRows[0].Cells[colObj].Value as 状況確認一覧Row;

            int linkSaki = -1;
            string linkSakiId = "";
            if (row.DmKanriKirokuId != null && row.DmKanriKirokuId.Length > 0)
            {
                linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                linkSakiId = row.DmKanriKirokuId;
            }
            else
            {
                linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                linkSakiId = row.DmKoubunshoKisokuId;
            }

            bool ret = 内容確認処理.Honsen登録(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID, linkSaki, linkSakiId, checkedUserIds);
            if (ret == true)
            {
                MessageBox.Show("内容確認しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                //「内容確認」処理成功の場合
                // 再検索を実施する
                Search();
            }
            else
            {
                MessageBox.Show("内容確認に失敗しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        /// <summary>
        /// 「コメント登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_コメント登録_Click(object sender, EventArgs e)
        private void button_コメント登録_Click(object sender, EventArgs e)
        {
            if (選択行確認() == false)
            {
                return;
            }

            if (表示確認() == false)
            {
                return;
            }

            状況確認一覧Row rowdata = dataGridView1.SelectedRows[0].Cells[colObj].Value as 状況確認一覧Row;
            コメント登録Form form = new コメント登録Form();
            form.parentForm = this; // 2012.02 : Add
            form.RowData = rowdata;
            if (form.ShowDialog() == DialogResult.OK)
            {
                this.WindowState = FormWindowState.Normal; // 2012.02 : Add

                //「コメント登録」画面で「コメント登録」処理成功の場合
                // 再検索を実施する
                Search();
            }
        }
        #endregion

        /// <summary>
        /// 「回覧状況」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_回覧状況_Click(object sender, EventArgs e)
        private void button_回覧状況_Click(object sender, EventArgs e)
        {
            if (選択行確認() == false)
            {
                return;
            }
            状況確認一覧Row rowdata = dataGridView1.SelectedRows[0].Cells[colObj].Value as 状況確認一覧Row;
            
            状況確認詳細form = new 状況確認詳細Form(this);
            状況確認詳細form.parentForm = this; // 2012.02 : Add
            状況確認詳細form.RowData = rowdata;
            状況確認詳細form.ShowDialog();

            this.WindowState = FormWindowState.Normal; // 2012.02 : Add
        }
        #endregion

        /// <summary>
        /// 「確認状況一覧」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_確認状況一覧_Click(object sender, EventArgs e)
        private void button_確認状況一覧_Click(object sender, EventArgs e)
        {
            if (状況確認一覧Rows == null || 状況確認一覧Rows.Count() == 0)
            {
                MessageBox.Show("確認状況が検索されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Reports.状況確認一覧 reporter = new Document.Reports.状況確認一覧();

            //// 対象データだけを取り出す
            //List<状況確認一覧Row> 状況確認一覧Rows_対象 = new List<状況確認一覧Row>();
            //foreach (状況確認一覧Row row in 状況確認一覧Rows)
            //{
            //    if (row.PublisherVesselId != 同期Client.LOGIN_VESSEL.MsVesselID && row.KoukaiSakiVesselId != 同期Client.LOGIN_VESSEL.MsVesselID)
            //    {
            //        continue;
            //    }
            //    状況確認一覧Rows_対象.Add(row);
            //}
            //reporter.Output(同期Client.LOGIN_USER, 状況確認一覧Rows_対象);
            reporter.Output(同期Client.LOGIN_USER, 状況確認一覧Rows);
        }
        #endregion

        /// <summary>
        /// 「文書同期情報」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_文書同期情報_Click(object sender, EventArgs e)
        private void button_文書同期情報_Click(object sender, EventArgs e)
        {
            文書同期状況Form form = new 文書同期状況Form();
            form.ShowDialog();
        }
        #endregion

        /// <summary>
        /// "文書名"クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = "";
            string fileName = "";
            byte[] fileData = null;
            int linkSaki = -1;
            string linkSakiId = "";
            
            try
            {
                Cursor = Cursors.WaitCursor;

                if (IsANonHeaderLinkCell(e))
                {
                    状況確認一覧Row rowData = dataGridView1.SelectedRows[0].Cells[colObj].Value as 状況確認一覧Row;

                    // 選択行のファイルデータの確認
                    #region
                    if (rowData.DmKanriKirokuId != null && rowData.DmKanriKirokuId.Length > 0)
                    {
                        linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                        linkSakiId = rowData.DmKanriKirokuId;

                        DmKanriKirokuFile kanriKirokuFile = DmKanriKirokuFile.GetRecordByKanriKirokuID(同期Client.LOGIN_USER, rowData.DmKanriKirokuId);
                        if (kanriKirokuFile == null)
                        {
                            MessageBox.Show("管理記録ファイルが存在しません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                        fileName = kanriKirokuFile.FileName;
                        fileData = kanriKirokuFile.Data;
                        id = kanriKirokuFile.DmKanriKirokuFileID;
                    }
                    else
                    {
                        linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                        linkSakiId = rowData.DmKoubunshoKisokuId;

                        DmKoubunshoKisokuFile koubunshoKisokuFile = DmKoubunshoKisokuFile.GetRecordByKoubunshoKisokuID(同期Client.LOGIN_USER, rowData.DmKoubunshoKisokuId);
                        if (koubunshoKisokuFile == null)
                        {
                            MessageBox.Show("公文書_規則ファイルが存在しません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        fileName = koubunshoKisokuFile.FileName;
                        fileData = koubunshoKisokuFile.Data;
                        id = koubunshoKisokuFile.DmKoubunshoKisokuFileID;
                    }
                    #endregion

                    // ファイルの表示
                    NBaseCommon.FileView.View(id, fileName, fileData);

                    // 表示したことを確認状況（表示のみ）としてＤＢに登録
                    #region
                    DmKakuninJokyo kakuninJokyo = DmKakuninJokyo.GetRecordByLinkSakiKoukaiSakiUser(同期Client.LOGIN_USER, linkSaki, linkSakiId, (int)NBaseData.DS.DocConstants.RoleEnum.船, 同期Client.LOGIN_USER.MsUserID);
                    if (kakuninJokyo == null)
                    {
                        kakuninJokyo = new DmKakuninJokyo();
                        kakuninJokyo.ViewDate = DateTime.Now;
                        kakuninJokyo.MsUserID = 同期Client.LOGIN_USER.MsUserID;

                        kakuninJokyo.DocFlag_CEO = 同期Client.LOGIN_USER.DocFlag_CEO;
                        kakuninJokyo.DocFlag_Admin = 同期Client.LOGIN_USER.DocFlag_Admin;
                        kakuninJokyo.DocFlag_MsiFerry = 同期Client.LOGIN_USER.DocFlag_MsiFerry;
                        kakuninJokyo.DocFlag_CrewFerry = 同期Client.LOGIN_USER.DocFlag_CrewFerry;
                        kakuninJokyo.DocFlag_TsiFerry = 同期Client.LOGIN_USER.DocFlag_TsiFerry;
                        //kakuninJokyo.DocFlag_MsiCargo = 同期Client.LOGIN_USER.DocFlag_MsiCargo;
                        //kakuninJokyo.DocFlag_CrewCargo = 同期Client.LOGIN_USER.DocFlag_CrewCargo;
                        //kakuninJokyo.DocFlag_TsiCargo = 同期Client.LOGIN_USER.DocFlag_TsiCargo;
                        kakuninJokyo.DocFlag_Officer = 同期Client.LOGIN_USER.DocFlag_Officer;
                        //kakuninJokyo.DocFlag_SdManager = 同期Client.LOGIN_USER.DocFlag_SdManager;
                        kakuninJokyo.DocFlag_GL = 同期Client.LOGIN_USER.DocFlag_GL;
                        kakuninJokyo.DocFlag_TL = 同期Client.LOGIN_USER.DocFlag_TL;

                        kakuninJokyo.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船;
                        kakuninJokyo.MsVesselID = 同期Client.LOGIN_VESSEL.MsVesselID;

                        if (rowData.DmKanriKirokuId != null && rowData.DmKanriKirokuId.Length > 0)
                        {
                            kakuninJokyo.LinkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                            kakuninJokyo.LinkSakiID = rowData.DmKanriKirokuId;
                        }
                        else
                        {
                            kakuninJokyo.LinkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                            kakuninJokyo.LinkSakiID = rowData.DmKoubunshoKisokuId;
                        }

                        kakuninJokyo.DmKakuninJokyoID = System.Guid.NewGuid().ToString();
                        kakuninJokyo.RenewDate = DateTime.Now;
                        kakuninJokyo.RenewUserID = 同期Client.LOGIN_USER.MsUserID;

                        bool ret = kakuninJokyo.InsertRecord(同期Client.LOGIN_USER);
                    }
                    #endregion
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("対象ファイルを開けません：" + Ex.Message, DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        /// <summary>
        /// "文書名"カラムのイベントかを判定する
        /// </summary>
        /// <param name="cellEvent"></param>
        /// <returns></returns>
        #region private bool IsANonHeaderLinkCell(DataGridViewCellEventArgs cellEvent)
        private bool IsANonHeaderLinkCell(DataGridViewCellEventArgs cellEvent)
        {
            if (cellEvent.ColumnIndex == -1)
                return false;

            if (dataGridView1.Columns[cellEvent.ColumnIndex] is
                DataGridViewLinkColumn &&
                cellEvent.RowIndex != -1)
            { return true; }
            else { return false; }
        }
        #endregion

        /// <summary>
        /// 行を選択したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetButton();
        }
        #endregion

        /// <summary>
        /// 行の選択が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (isCallBack)
            {
                SetButton();
            }
        }
        #endregion

        /// <summary>
        /// 行がダブルクリックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            状況確認一覧Row rowdata = dataGridView1.SelectedRows[0].Cells[colObj].Value as 状況確認一覧Row;
            
            状況確認詳細form = new 状況確認詳細Form(this);
            状況確認詳細form.parentForm = this; // 2012.02 : Add
            状況確認詳細form.RowData = rowdata;
            状況確認詳細form.ShowDialog();

            this.WindowState = FormWindowState.Normal; // 2012.02 : Add
        }
        #endregion



        /// <summary>
        /// 「分類」DDL構築
        /// </summary>
        #region private void SetBunruiDDL()
        private void SetBunruiDDL()
        {
            MsDmBunrui dmy = new MsDmBunrui();
            dmy.MsDmBunruiID = "";
            dmy.Name = "";

            comboBox_Bunrui.Items.Clear();
            comboBox_Bunrui.Items.Add(dmy);
            foreach (MsDmBunrui msDmBunrui in msDmBunruis)
            {
                comboBox_Bunrui.Items.Add(msDmBunrui);
            }
            comboBox_Bunrui.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// 「小分類」DDL構築
        /// </summary>
        #region private void SetShoubunruiDDL()
        private void SetShoubunruiDDL()
        {
            string bunruiId = (comboBox_Bunrui.SelectedItem as MsDmBunrui).MsDmBunruiID;
            var msDmSoubunruisBybunruiId = from p in msDmShoubunruis
                                           where p.MsDmBunruiID == bunruiId
                                           orderby p.Code, p.Name
                                           select p;

            MsDmShoubunrui dmy = new MsDmShoubunrui();
            dmy.MsDmShoubunruiID = "";
            dmy.Name = "";

            comboBox_Shoubunrui.Items.Clear();
            comboBox_Shoubunrui.Items.Add(dmy);
            foreach (MsDmShoubunrui msDmShoubunrui in msDmSoubunruisBybunruiId)
            {
                comboBox_Shoubunrui.Items.Add(msDmShoubunrui);
            }
            comboBox_Shoubunrui.SelectedIndex = 0;
        }
        #endregion
        
        /// <summary>
        /// 「発行部署」DDL構築
        /// </summary>
        #region private void SetPublisherDDL()
        private void SetPublisherDDL()
        {
            comboBox_Publisher.Items.Clear();


            List<MsVessel> msVessels = MsVessel.GetRecordsByDocumentEnabled(同期Client.LOGIN_USER);
            MsVessel msVesselDmy = new MsVessel();
            msVesselDmy.MsVesselID = -1;
            msVesselDmy.VesselNo = "";
            msVesselDmy.VesselName = "---------------";
            comboBox_Publisher.Items.Add(msVesselDmy);
            foreach (MsVessel msVessel in msVessels)
            {
                comboBox_Publisher.Items.Add(msVessel);
            }

            //AddUserItem(comboBox_Publisher, "-1", "---------------", 0, 0);
            //AddUserItem(comboBox_Publisher, "", "会長_社長", 1, 0);
            //AddUserItem(comboBox_Publisher, "", "管理責任者", 0, 1);
            AddUserItem(comboBox_Publisher);

            //AddBumonItem(comboBox_Publisher, "", "---------------");
            //AddBumonItem(comboBox_Publisher, "0", "安全運航・海技Ｔ");
            //AddBumonItem(comboBox_Publisher, "1", "船舶管理・技術Ｔ");
            //AddBumonItem(comboBox_Publisher, "2", "船員Ｔ");
            //AddBumonItem(comboBox_Publisher, "3", "IT・DX推進室");
            AddBumonItem(comboBox_Publisher);


            comboBox_Publisher.SelectedIndex = 0;
        }
        //private void AddUserItem(ComboBox cb, string userId, string userName, int kaichoFlag, int sekininshaFlag)
        //{
        //    MsUser msUser = new MsUser();
        //    msUser.MsUserID = userId;
        //    msUser.Sei = userName;
        //    msUser.KaichoFlag = kaichoFlag;
        //    msUser.SekininshaFlag = sekininshaFlag;
        //    cb.Items.Add(msUser);
        //}
        private void AddUserItem(ComboBox cb)
        {
            MsUser msUser = new MsUser();
            msUser.MsUserID = "-1";
            msUser.Sei = "---------------";
            cb.Items.Add(msUser);


            var list = DocConstants.ClassItemList().Where(o => o.enumClass == DocConstants.ClassEnum.USER);
            foreach (DocConstants.ClassItem item in list)
            {
                msUser = new MsUser();
                msUser.MsUserID = "";
                msUser.Sei = item.menuName;

                if (item.enumRole == DocConstants.RoleEnum.経営責任者)
                    msUser.DocFlag_CEO = 1;
                if (item.enumRole == DocConstants.RoleEnum.管理責任者)
                    msUser.DocFlag_Admin = 1;
                if (item.enumRole == DocConstants.RoleEnum.海務監督)
                    msUser.DocFlag_MsiFerry = 1;
                if (item.enumRole == DocConstants.RoleEnum.船員担当者)
                    msUser.DocFlag_CrewFerry = 1;
                if (item.enumRole == DocConstants.RoleEnum.工務監督)
                    msUser.DocFlag_TsiFerry = 1;
                if (item.enumRole == DocConstants.RoleEnum.役員)
                    msUser.DocFlag_Officer = 1;

                cb.Items.Add(msUser);
            }
        }

        //private void AddBumonItem(ComboBox cb, string bumonId, string bumonName)
        //{
        //    MsBumon msBumon = new MsBumon();
        //    msBumon.MsBumonID = bumonId;
        //    msBumon.BumonName = bumonName;
        //    cb.Items.Add(msBumon);
        //}
        private void AddBumonItem(ComboBox cb)
        {
            MsBumon msBumon = new MsBumon();
            msBumon.MsBumonID = "";
            msBumon.BumonName = "---------------";
            cb.Items.Add(msBumon);

            var list = DocConstants.ClassItemList().Where(o => o.enumClass == DocConstants.ClassEnum.BUMON);
            foreach (DocConstants.ClassItem item in list)
            {
                msBumon = new MsBumon();
                msBumon.MsBumonID = item.bumonId;
                msBumon.BumonName = item.menuName;
                cb.Items.Add(msBumon);
            }
        }
        #endregion

        /// <summary>
        /// 「確認状況」DDL構築
        /// </summary>
        #region private void SetStatusDDL()
        private void SetStatusDDL()
        {
            comboBox_Status.Items.Clear();
            comboBox_Status.Items.Add(new KakuninStatus(KakuninStatus.StatusEum.すべて));
            comboBox_Status.Items.Add(new KakuninStatus(KakuninStatus.StatusEum.未確認));
            comboBox_Status.Items.Add(new KakuninStatus(KakuninStatus.StatusEum.確認不要を除く));
            comboBox_Status.Items.Add(new KakuninStatus(KakuninStatus.StatusEum.確認済み));
            comboBox_Status.Items.Add(new KakuninStatus(KakuninStatus.StatusEum.他グループ未確認));

            comboBox_Status.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// ボタンの有効/無効化
        /// </summary>
        #region private void SetButton()
        private void SetButton()
        {
            // 以下のボタンは常に有効
            button_回覧状況.Enabled = true;
            button_確認状況一覧.Enabled = true;
            button_文書同期情報.Enabled = true;

            // 一旦、無効にする
            button_コメント登録.Enabled = false;
            button_内容確認.Enabled = false;

            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            状況確認一覧Row row = dataGridView1.SelectedRows[0].Cells[colObj].Value as 状況確認一覧Row;

            // 完了している場合、何もしない
            if (row.Status == (int)NBaseData.DS.DocConstants.StatusEnum.完了)
            {
                return;
            }

            // 公開先に設定されていれば使用できる
            foreach (DmKoukaiSaki koukaisaki in row.KoukaisakiList)
            {
                if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.船)
                {
                    if (同期Client.LOGIN_VESSEL.MsVesselID == koukaisaki.MsVesselID)
                    {
                        button_コメント登録.Enabled = true;
                        button_内容確認.Enabled = true;
                        break;
                    }
                }
                //if (koukaisaki.KoukaiSaki != (int)NBaseData.DS.DocConstants.RoleEnum.船)
                //{
                //    if (同期Client.LOGIN_USER.UserFlag == (int)NBaseData.DS.DocConstants.UserFlagEnum.会長 &&
                //        koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.会長_社長)
                //    {
                //        button_コメント登録.Enabled = true;
                //        button_内容確認.Enabled = true;
                //    }
                //    if (同期Client.LOGIN_USER.UserFlag == (int)NBaseData.DS.DocConstants.UserFlagEnum.社長 &&
                //        koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.会長_社長)
                //    {
                //        button_コメント登録.Enabled = true;
                //        button_内容確認.Enabled = true;
                //    }
                //    if (同期Client.LOGIN_USER.UserFlag == (int)NBaseData.DS.DocConstants.UserFlagEnum.管理責任者 &&
                //        koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者)
                //    {
                //        button_コメント登録.Enabled = true;
                //        button_内容確認.Enabled = true;
                //    }
                //    if (koukaisaki.KoukaiSaki == (int)NBaseData.DS.DocConstants.RoleEnum.部門 &&
                //        koukaisaki.MsBumonID == 同期Client.LOGIN_USER.BumonID)
                //    {
                //        button_コメント登録.Enabled = true;
                //        button_内容確認.Enabled = true;
                //    }
                //}
            }
        }
        #endregion

        /// <summary>
        /// 一覧の文書（行）選択の確認
        /// </summary>
        /// <returns></returns>
        #region private bool 選択行確認()
        private bool 選択行確認()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("文書が選択されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 選択行の文書を表示しているかの確認
        /// </summary>
        /// <returns></returns>
        #region private bool 表示確認()
        private bool 表示確認()
        {

            // 必ずこのメソッドを呼ぶ前に、"選択行確認()" を呼んで選択行があることを確認して下さい
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return false;
            }
            状況確認一覧Row rowdata = dataGridView1.SelectedRows[0].Cells[colObj].Value as 状況確認一覧Row;

            int linkSaki = -1;
            string linkSakiId = "";
            if (rowdata.DmKanriKirokuId != null && rowdata.DmKanriKirokuId.Length > 0)
            {
                linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                linkSakiId = rowdata.DmKanriKirokuId;
            }
            else
            {
                linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                linkSakiId = rowdata.DmKoubunshoKisokuId;
            }
            DmKakuninJokyo kakuninJokyo = DmKakuninJokyo.GetRecordByLinkSakiKoukaiSakiUser(同期Client.LOGIN_USER, linkSaki, linkSakiId, (int)NBaseData.DS.DocConstants.RoleEnum.船, 同期Client.LOGIN_USER.MsUserID);
            if (kakuninJokyo == null)
            {
                MessageBox.Show("対象となる文書は表示していません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 検索＆一覧表示
        /// </summary>
        #region private void Search()
        private void Search()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                dataGridView1.Font = new Font(dataGridView1.Font.FontFamily, 12);
                dataGridView1.Rows.Clear();
                
                #region 検索条件を取得する

                string bunruiId = "";
                string shoubunruiId = "";
                string bunshoNo = "";
                string bunshoName = "";
                string bikou = "";
                int publisherMsVesselId = 0;
                int publisherRole = 0;
                string publisherBumonId = "";
                DateTime issueFrom = DateTime.MinValue;
                DateTime issueTo = DateTime.MaxValue;
                KakuninStatus kakuninStatus = null;
                bool kanryoFlag = false;
                string keywords = "";

                // 分類
                if (comboBox_Bunrui.SelectedItem is MsDmBunrui)
                {
                    bunruiId = (comboBox_Bunrui.SelectedItem as MsDmBunrui).MsDmBunruiID;
                }
                if (bunruiId == null || bunruiId.Length == 0)
                {
                    MessageBox.Show("分類名を選択して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 小分類
                if (comboBox_Shoubunrui.SelectedItem is MsDmShoubunrui)
                {
                    shoubunruiId = (comboBox_Shoubunrui.SelectedItem as MsDmShoubunrui).MsDmShoubunruiID;

                    if (shoubunruiId != null && shoubunruiId.Length > 0)
                        bunruiId = ""; // 小分類の指定がある場合、分類は検索キーとしない
                }

                // 文書番号
                bunshoNo = textBox_BunshoNo.Text;

                // 文書名
                bunshoName = textBox_BunshoName.Text;

                // 備考
                bikou = textBox_Bikou.Text;

                // キーワード（対象は、文書番号、文書名、備考）
                keywords = textBox_Keyword.Text;

                // 発行日
                try
                {
                    if (nullableDateTimePicker_IssueDate1.Value != null)
                        issueFrom = (DateTime)nullableDateTimePicker_IssueDate1.Value;
                }
                catch
                {
                }
                try
                {
                    if (nullableDateTimePicker_IssueDate2.Value != null)
                        issueTo = (DateTime)nullableDateTimePicker_IssueDate2.Value;
                }
                catch
                {
                }


                // 発行部署：船
                if (comboBox_Publisher.SelectedItem is MsVessel)
                {
                    publisherMsVesselId = (comboBox_Publisher.SelectedItem as MsVessel).MsVesselID;
                }

                // 発行部署：要員
                if (comboBox_Publisher.SelectedItem is MsUser)
                {
                    MsUser user = comboBox_Publisher.SelectedItem as MsUser;
                    if (user.DocFlag_CEO == 1)
                        publisherRole = (int)DocConstants.RoleEnum.経営責任者;
                    else if (user.DocFlag_Admin == 1)
                        publisherRole = (int)DocConstants.RoleEnum.管理責任者;
                    else if (user.DocFlag_MsiFerry == 1)
                        publisherRole = (int)DocConstants.RoleEnum.海務監督;
                    else if (user.DocFlag_CrewFerry == 1)
                        publisherRole = (int)DocConstants.RoleEnum.船員担当者;
                    else if (user.DocFlag_TsiFerry == 1)
                        publisherRole = (int)DocConstants.RoleEnum.工務監督;
                    else if (user.DocFlag_Officer == 1)
                        publisherRole = (int)DocConstants.RoleEnum.役員;
                }

                // 発行部署：部門
                if (comboBox_Publisher.SelectedItem is MsBumon)
                {
                    publisherBumonId = (comboBox_Publisher.SelectedItem as MsBumon).MsBumonID;
                }

                // 確認状況
                if (comboBox_Status.SelectedItem is KakuninStatus)
                {
                    kakuninStatus = comboBox_Status.SelectedItem as KakuninStatus;
                }

                // 完了
                kanryoFlag = checkBox_Status.Checked;


                #endregion

                #region 検索処理

                List<string> bunruiIds = new List<string>();
                bunruiIds.Add(bunruiId);
                List<string> shoubunruiIds = new List<string>();
                shoubunruiIds.Add(shoubunruiId);
                List<int> vesselIds = new List<int>();

                List<状況確認一覧Row> 状況確認一覧Rows_All = 状況確認一覧Row.SearchRecords(同期Client.LOGIN_USER, bunruiIds, shoubunruiIds, vesselIds, bunshoNo, bunshoName, bikou, issueFrom, issueTo, publisherMsVesselId, publisherRole, publisherBumonId, (int)kakuninStatus.Status, kanryoFlag, keywords);
                
                // 対象データだけを取り出す
                if (状況確認一覧Rows == null)
                {
                    状況確認一覧Rows = new List<状況確認一覧Row>();
                }
                else
                {
                    状況確認一覧Rows.Clear();
                }
                foreach (状況確認一覧Row row in 状況確認一覧Rows_All)
                {
                    if (row.PublisherVesselId != 同期Client.LOGIN_VESSEL.MsVesselID && row.KoukaiSakiVesselId != 同期Client.LOGIN_VESSEL.MsVesselID)
                    {
                        continue;
                    }
                    状況確認一覧Rows.Add(row);
                }

                #endregion

                #region カラムの設定
                if (dataGridView1.Columns.Count == 0)
                {
                    DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
                    textColumn.HeaderText = "発行元";
                    textColumn.Width = 150;
                    dataGridView1.Columns.Add(textColumn);

                    textColumn = new DataGridViewTextBoxColumn();
                    textColumn.HeaderText = "船名";
                    textColumn.Width = 150;
                    dataGridView1.Columns.Add(textColumn);

                    textColumn = new DataGridViewTextBoxColumn();
                    textColumn.HeaderText = "発行日";
                    textColumn.Width = 100;
                    dataGridView1.Columns.Add(textColumn);

                    textColumn = new DataGridViewTextBoxColumn();
                    textColumn.HeaderText = "分類";
                    textColumn.Width = 120;
                    dataGridView1.Columns.Add(textColumn);

                    textColumn = new DataGridViewTextBoxColumn();
                    textColumn.HeaderText = "小分類";
                    textColumn.Width = 200;
                    dataGridView1.Columns.Add(textColumn);

                    textColumn = new DataGridViewTextBoxColumn();
                    textColumn.HeaderText = "文書番号";
                    textColumn.Width = 110;
                    dataGridView1.Columns.Add(textColumn);

                    DataGridViewLinkColumn linkColumn = new DataGridViewLinkColumn();
                    linkColumn.HeaderText = "文書名";
                    linkColumn.Width = 250;
                    //linkColumn.LinkColor
                    //linkColumn.VisitedLinkColor
                    //linkColumn.ActiveLinkColor
                    dataGridView1.Columns.Add(linkColumn);

                    DataGridViewImageColumn imageColum = new DataGridViewImageColumn();
                    imageColum.HeaderText = "";
                    imageColum.Width = 30;
                    dataGridView1.Columns.Add(imageColum);

                    textColumn = new DataGridViewTextBoxColumn();
                    textColumn.HeaderText = "備考";
                    textColumn.Width = 150;
                    dataGridView1.Columns.Add(textColumn);
                   
                    textColumn = new DataGridViewTextBoxColumn();
                    textColumn.HeaderText = "完了";
                    textColumn.Width = 150;
                    dataGridView1.Columns.Add(textColumn);

                    DataGridView状況確認一覧RowColumn column = new DataGridView状況確認一覧RowColumn();
                    column.Visible = false;
                    dataGridView1.Columns.Add(column);
                }
                #endregion

                int rowNo = 0;
                foreach (状況確認一覧Row row in 状況確認一覧Rows)
                {
                    #region 情報を一覧にセットする

                    int colNo = 0;
                    object[] rowDatas = new object[11];
                    rowDatas[colNo] = row.発行元;
                    colNo++;
                    rowDatas[colNo] = row.船名;
                    colNo++;
                    rowDatas[colNo] = row.発行日.ToShortDateString();
                    colNo++;
                    rowDatas[colNo] = row.分類名;
                    colNo++;
                    rowDatas[colNo] = row.小分類名;
                    colNo++;
                    rowDatas[colNo] = row.文書番号;
                    colNo++;
                    rowDatas[colNo] = row.文書名;
                    colNo++;


                    rowDatas[colNo] = row.GetFileImg();
                    colNo++;

                    rowDatas[colNo] = row.備考;
                    colNo++;


                    rowDatas[colNo] = "";
                    if (row.Status == (int)NBaseData.DS.DocConstants.StatusEnum.完了)
                    {
                        rowDatas[colNo] = row.完了日.ToShortDateString() + " " + row.完了者;
                    }
                    colNo++;
                    rowDatas[colNo] = row;

                    dataGridView1.Rows.Add(rowDatas);
                    if (row.備考 == null || row.備考.Length == 0)
                    {
                        dataGridView1[col文書名, rowNo].ToolTipText = "(備考なし)";
                    }
                    else
                    {
                        dataGridView1[col文書名, rowNo].ToolTipText = row.備考;
                    }

                    rowNo++;

                    #endregion
                }

                SetButton();
            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }
        #endregion

        /// <summary>
        /// "状況確認詳細"画面で「↑（１つ戻る）」ボタンがクリックされたときのコールバック
        /// </summary>
        #region public void 戻る()
        public void 戻る()
        {
            int selectedRowIndex = dataGridView1.SelectedRows[0].Index;

            if (selectedRowIndex > 0)
            {
                isCallBack = false;

                dataGridView1.Rows[selectedRowIndex].Selected = false;
                dataGridView1.Rows[selectedRowIndex - 1].Selected = true;

                状況確認一覧Row rowdata = dataGridView1.SelectedRows[0].Cells[colObj].Value as 状況確認一覧Row;
                状況確認詳細form.RowData = rowdata;
                状況確認詳細form.Refresh();

                isCallBack = true;
                SetButton();
            }
        }
        #endregion

        /// <summary>
        /// "状況確認詳細"画面で「↓（１つ進む）」ボタンがクリックされたときのコールバック
        /// </summary>
        #region public void 進む()
        public void 進む()
        {
            int selectedRowIndex = dataGridView1.SelectedRows[0].Index;

            if ((selectedRowIndex + 1) < dataGridView1.Rows.Count)
            {
                isCallBack = false;

                dataGridView1.Rows[selectedRowIndex].Selected = false;
                dataGridView1.Rows[selectedRowIndex + 1].Selected = true;

                状況確認一覧Row rowdata = dataGridView1.SelectedRows[0].Cells[colObj].Value as 状況確認一覧Row;
                状況確認詳細form.RowData = rowdata;
                状況確認詳細form.Refresh();

                isCallBack = true;
                SetButton();
            }
        }
        #endregion

        /// <summary>
        /// "状況確認詳細"画面で「回覧終了」ボタンがクリックされたときのコールバック
        /// </summary>
        #region public void 回覧終了(状況確認一覧Row rowData)
        public void 回覧終了(状況確認一覧Row rowData)
        {
            dataGridView1.SelectedRows[0].Cells[colObj].Value = rowData;

            int colIdx = colObj - 1;
            string 完了日 = rowData.完了日.ToShortDateString();
            string 完了者 = rowData.完了者;

            foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
            {
                状況確認一覧Row row = dgvRow.Cells[colObj].Value as 状況確認一覧Row;
                bool isEdit = false;
                if (rowData.DmKanriKirokuId != null && rowData.DmKanriKirokuId.Length > 0)
                {
                    if (row.DmKanriKirokuId == rowData.DmKanriKirokuId)
                    {
                        isEdit = true;
                    }
                }
                else
                {
                    if (row.DmKoubunshoKisokuId == rowData.DmKoubunshoKisokuId)
                    {
                        isEdit = true;
                    }
                }
                if (isEdit)
                {
                    // リストを更新
                    dgvRow.Cells[colIdx].Value = 完了日 + " " + 完了者;

                    // 保持しているデータを更新
                    row.Status = rowData.Status;
                    row.完了日 = rowData.完了日;
                    row.完了者 = rowData.完了者;
                    dgvRow.Cells[colObj].Value = row;
                }
            }

            SetButton();
        }
        #endregion

        /// <summary>
        /// "状況確認詳細"画面を閉じるときにコールしてもらうコールバック
        /// </summary>
        #region public void 再検索()
        public void 再検索()
        {
            // グリッドの一番上の行
            int firstRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;

            // 現在の選択
            DataGridViewCell currentCell = dataGridView1.CurrentCell;
            int selectRowIndex = currentCell.RowIndex;

            // 検索＆描画
            Search();

            if (dataGridView1.Rows.Count > 0)
            {
                // グリッドの一番上の行
                if (dataGridView1.Rows.Count <= firstRowIndex)
                {
                    dataGridView1.FirstDisplayedCell = dataGridView1[0, dataGridView1.Rows.Count - 1];
                }
                else
                {
                    dataGridView1.FirstDisplayedCell = dataGridView1[0, firstRowIndex];
                }

                // 選択
                if (dataGridView1.Rows.Count <= selectRowIndex)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
                }
                else
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[selectRowIndex].Cells[0];
                }
            }
        }
        #endregion

        /// <summary>
        /// "状況確認詳細"画面で「備考編集」ボタンがクリックされたときのコールバック
        /// </summary>
        #region public void 備考編集(状況確認一覧Row rowData)
        public void 備考編集(状況確認一覧Row rowData)
        {
            int colIdx = 8; // 備考のカラム番号

            dataGridView1.SelectedRows[0].Cells[colObj].Value = rowData;
            dataGridView1.SelectedRows[0].Cells[colIdx].Value = rowData.備考;
        }
        #endregion



        private bool IsANonHeaderImageCell(DataGridViewCellMouseEventArgs cellEvent)
        {
            if (cellEvent.ColumnIndex == -1)
                return false;

            if (dataGridView1.Columns[cellEvent.ColumnIndex] is
                DataGridViewImageColumn &&
                cellEvent.RowIndex != -1)
            { return true; }
            else { return false; }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (IsANonHeaderImageCell(e))
            {
                string id = "";
                string fileName = "";
                byte[] fileData = null;
                string linkSakiId = "";
                //int linkSaki = -1;
                string filePath = "";

                try
                {
                    // 選択行のファイルデータの確認
                    #region
                    //状況確認一覧Row rowData = dataGridView1.SelectedRows[0].Cells[colObj].Value as 状況確認一覧Row;
                    状況確認一覧Row rowData = dataGridView1.Rows[e.RowIndex].Cells[colObj].Value as 状況確認一覧Row;

                    if (rowData.DmKanriKirokuId != null && rowData.DmKanriKirokuId.Length > 0)
                    {
                        //linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                        linkSakiId = rowData.DmKanriKirokuId;

                        DmKanriKirokuFile kanriKirokuFile = DmKanriKirokuFile.GetRecordByKanriKirokuID(同期Client.LOGIN_USER, rowData.DmKanriKirokuId);
                        if (kanriKirokuFile == null)
                        {
                            MessageBox.Show("管理記録ファイルが存在しません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        fileName = kanriKirokuFile.FileName;
                        fileData = kanriKirokuFile.Data;
                        id = kanriKirokuFile.DmKanriKirokuFileID;
                    }
                    else
                    {
                        //linkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                        linkSakiId = rowData.DmKoubunshoKisokuId;

                        DmKoubunshoKisokuFile koubunshoKisokuFile = DmKoubunshoKisokuFile.GetRecordByKoubunshoKisokuID(同期Client.LOGIN_USER, rowData.DmKoubunshoKisokuId);
                        if (koubunshoKisokuFile == null)
                        {
                            MessageBox.Show("公文書_規則ファイルが存在しません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        fileName = koubunshoKisokuFile.FileName;
                        fileData = koubunshoKisokuFile.Data;
                        id = koubunshoKisokuFile.DmKoubunshoKisokuFileID;
                    }
                    #endregion

                    // ファイルの表示
                    filePath = NBaseCommon.FileView.CreateFile(id, fileName, fileData);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("対象ファイルを開けません：" + Ex.Message, DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //ファイルドロップ形式でDataObjectオブジェクトを作成する
                DataObject dataObj = new DataObject();
                string[] fileNames = new string[1];
                fileNames[0] = filePath;
                dataObj.SetData(DataFormats.FileDrop, fileNames);

                //ドラッグを開始する
                dataGridView1.DoDragDrop(dataObj, DragDropEffects.Copy);
            }
        }


        //=======================================================================================
        // 
        // 内部で使用しているクラス等
        // 
        //=======================================================================================
        #region

        private class KakuninStatus
        {
            public enum StatusEum { すべて, 未確認, 確認済み, 他グループ未確認, 確認不要を除く };
            public StatusEum Status;

            public override string ToString()
            {
                string StatusStr = "";
                if (Status == StatusEum.すべて)
                {
                }
                else if (Status == StatusEum.未確認)
                {
                    StatusStr = "未確認";
                }
                else if (Status == StatusEum.確認済み)
                {
                    StatusStr = "確認済み";
                }
                else if (Status == StatusEum.他グループ未確認)
                {
                    StatusStr = "他グループ未確認";
                }
                else if (Status == StatusEum.確認不要を除く)
                {
                    StatusStr = "未確認（確認不要を除く）";
                }

                return StatusStr;
            }

            public KakuninStatus(StatusEum se)
            {
                Status = se;
            }
        }

        public class DataGridView状況確認一覧RowColumn : DataGridViewTextBoxColumn
        {
            //コンストラクタ
            public DataGridView状況確認一覧RowColumn()
            {
                this.CellTemplate = new DataGridView状況確認一覧RowCell();
            }

            //CellTemplateの取得と設定
            public override DataGridViewCell CellTemplate
            {
                get
                {
                    return base.CellTemplate;
                }
                set
                {
                    //DataGridViewProgressBarCell以外はホストしない
                    if (!(value is DataGridView状況確認一覧RowCell))
                    {
                        throw new InvalidCastException(
                            "DataGridViewProgressBarCellオブジェクトを" +
                            "指定してください。");
                    }
                    base.CellTemplate = value;
                }
            }

            /// <summary>
            /// ProgressBarの最大値
            /// </summary>
            public int Maximum
            {
                get
                {
                    return ((DataGridView状況確認一覧RowCell)this.CellTemplate).Maximum;
                }
                set
                {
                    if (this.Maximum == value)
                        return;
                    //セルテンプレートの値を変更する
                    ((DataGridView状況確認一覧RowCell)this.CellTemplate).Maximum =
                        value;
                    //DataGridViewにすでに追加されているセルの値を変更する
                    if (this.DataGridView == null)
                        return;
                    int rowCount = this.DataGridView.RowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataGridViewRow r = this.DataGridView.Rows.SharedRow(i);
                        ((DataGridView状況確認一覧RowCell)r.Cells[this.Index]).Maximum =
                            value;
                    }
                }
            }

            /// <summary>
            /// ProgressBarの最小値
            /// </summary>
            public int Mimimum
            {
                get
                {
                    return ((DataGridView状況確認一覧RowCell)this.CellTemplate).Mimimum;
                }
                set
                {
                    if (this.Mimimum == value)
                        return;
                    //セルテンプレートの値を変更する
                    ((DataGridView状況確認一覧RowCell)this.CellTemplate).Mimimum =
                        value;
                    //DataGridViewにすでに追加されているセルの値を変更する
                    if (this.DataGridView == null)
                        return;
                    int rowCount = this.DataGridView.RowCount;
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataGridViewRow r = this.DataGridView.Rows.SharedRow(i);
                        ((DataGridView状況確認一覧RowCell)r.Cells[this.Index]).Mimimum =
                            value;
                    }
                }
            }
        }
        public class DataGridView状況確認一覧RowCell : DataGridViewTextBoxCell
        {
            //コンストラクタ
            public DataGridView状況確認一覧RowCell()
            {
                this.maximumValue = 100;
                this.mimimumValue = 0;
            }

            private int maximumValue;
            public int Maximum
            {
                get
                {
                    return this.maximumValue;
                }
                set
                {
                    this.maximumValue = value;
                }
            }

            private int mimimumValue;
            public int Mimimum
            {
                get
                {
                    return this.mimimumValue;
                }
                set
                {
                    this.mimimumValue = value;
                }
            }

            //セルの値のデータ型を指定する
            //ここでは、整数型とする
            public override Type ValueType
            {
                get
                {
                    return typeof(int);
                }
            }

        }

        #endregion

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
                return;

            if (dataGridView1[e.ColumnIndex, e.RowIndex] is DataGridViewLinkCell)
            {
                DataGridViewLinkCell c = (dataGridView1[e.ColumnIndex, e.RowIndex] as DataGridViewLinkCell);

                if (c.Selected)
                {
                    c.LinkColor = Color.White;
                }
                else
                {
                    c.LinkColor = Color.Blue;
                }
                e.FormattingApplied = true;
            }
        }
    }
}
