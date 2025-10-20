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
using NBaseCommon;
using ServiceReferences.NBaseService;

namespace Document
{
    public partial class 個別文書登録Form : ExForm
    {
        private string DIALOG_TITLE = "個別文書登録";

        public DmKoubunshoKisoku dmKoubunshoKisoku = null;
        public DmKoubunshoKisokuFile dmKoubunshoKisokuFile = null;
        public List<DmKoukaiSaki> dmKoukaiSakis = null;
        public List<DmPublisher> dmPublishers = null;

        List<MsDmBunrui> msDmBunruis = new List<MsDmBunrui>();
        List<MsDmShoubunrui> msDmShoubunruis = new List<MsDmShoubunrui>();
        List<船選択Form.ListItem> 公開先s = new List<船選択Form.ListItem>();

        //データを編集したかどうか？
        private bool ChangeFlag = false;

        public 個別文書登録Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", DIALOG_TITLE, ServiceReferences.WcfServiceWrapper.ConnectedServerID);
        }

        private void 個別文書登録Form_Load(object sender, EventArgs e)
        {
             InitForm();

             this.ChangeParentFormSize(FormWindowState.Minimized);
             this.Activate();
        }

        private void 個別文書登録Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ChangeParentFormSize(FormWindowState.Normal);
        }

        /// <summary>
        /// 
        /// </summary>
        #region private void InitForm()
        private void InitForm()
        {
            this.ChangeFlag = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msDmBunruis = serviceClient.MsDmBunrui_GetRecords(NBaseCommon.Common.LoginUser);
                msDmShoubunruis = serviceClient.MsDmShoubunrui_GetRecords(NBaseCommon.Common.LoginUser);
            }
            SetBunruiDDL();
            SetShoubunruiDDL();
            nullableDateTimePicker_IssueDate.Value = DateTime.Now;
            nullableDateTimePicker_IssueDate.Value = null;

            SetVessel();
            documentGroupCheckBox1.Text = "※公開先";
            documentGroupCheckBox1.Set船List = 公開先s;

        }
        #endregion


        /// <summary>
        /// 「登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_登録_Click(object sender, EventArgs e)
        private void button_登録_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                return;
            }

            FillInstance();

            bool ret;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_公文書規則処理_登録(NBaseCommon.Common.LoginUser, dmKoubunshoKisoku, dmKoubunshoKisokuFile, dmPublishers, dmKoukaiSakis);
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
        /// 「参照」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_参照_Click(object sender, EventArgs e)
        private void button_参照_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Filter = "公文書_規則ファイル(*.*)|*.*";
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
        /// 公開先の船情報の初期化
        /// </summary>
        #region private void SetVessel()
        private void SetVessel()
        {
            bool checkInit = true;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                //-----------------------------------------------------------------------------------
                //コメントアウト　m.yoshihara 2017/5/30
                //=========================
                // 船
                //=========================
                //List<NBaseData.DAC.MsVessel> msVessel_list = serviceClient.MsVessel_GetRecordsByDocumentEnabled(NBaseCommon.Common.LoginUser);

                //-----------------------------------------------------
                // 公開先
                //-----------------------------------------------------
                //foreach (NBaseData.DAC.MsVessel vessel in msVessel_list)
                //{
                //    船選択Form.ListItem item = new 船選択Form.ListItem(vessel.VesselName, vessel.MsVesselID, checkInit);
                //    公開先s.Add(item);
                //}
                //-----------------------------------------------------------------------------------

                //-----------------------------------------------------------------------------------
                //m.yoshihara 2017/5/30
                List<NBaseData.DAC.MsVessel> wklist = serviceClient.MsVessel_GetRecordsByDocumentEnabled(NBaseCommon.Common.LoginUser);
                List<NBaseData.DAC.MsVessel> msVessel_list = new List<MsVessel>();

                foreach (NBaseData.DAC.MsVessel vessel in wklist)
                {
                    if (vessel.DocumentEnabled == 1)
                    {
                        msVessel_list.Add(vessel);

                        船選択Form.ListItem item = new 船選択Form.ListItem(vessel.VesselName, vessel.MsVesselID, checkInit);
                        公開先s.Add(item);
                    }
                }
                //-----------------------------------------------------------------------------------
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
            try
            {
                MsDmBunrui msDmBunrui = comboBox_Bunrui.SelectedItem as MsDmBunrui;
                if (msDmBunrui.MsDmBunruiID == "")
                {
                    MessageBox.Show("分類が設定されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("分類が設定されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox_BunshoNo.Text.Length == 0)
            {
                MessageBox.Show("文書番号が設定されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //try
            //{
            //    if (NBaseUtil.StringUtils.isHankaku(textBox_BunshoNo.Text) == false)
            //    {
            //        MessageBox.Show("文書番号は半角で入力して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return false;
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("文書番号は半角で入力して下さい", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return false;
            //}
            if (textBox_BunshoName.Text.Length == 0)
            {
                MessageBox.Show("文書名が設定されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (nullableDateTimePicker_IssueDate.Value == null)
            {
                MessageBox.Show("発行日が設定されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            bool isChecked = false;
            foreach (DocumentGroupCheckBox.CheckItem item in documentGroupCheckBox1.Items)
            {
                if (item.Checked)
                {
                    isChecked = true;
                    break;
                }
            }
            if (isChecked == false)
            {
                MessageBox.Show("公開先が選択されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            isChecked = false;
            if (documentGroupCheckBox1.Items[DocumentGroupCheckBox.船Pos].Checked)
            {
                foreach (船選択Form.ListItem item in documentGroupCheckBox1.船List)
                {
                    if (item.Checked)
                    {
                        isChecked = true;
                        break;
                    }
                }
                if (isChecked == false)
                {
                    MessageBox.Show("公開先（船）が選択されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
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

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsDmBunrui msDmBunrui = comboBox_Bunrui.SelectedItem as MsDmBunrui;
                MsDmShoubunrui msDmShoubunrui = null;
                if (comboBox_Shoubunrui.SelectedItem is MsDmShoubunrui)
                {
                    msDmShoubunrui = comboBox_Shoubunrui.SelectedItem as MsDmShoubunrui;
                }
                List<DmKoubunshoKisoku> check = null;
                if (msDmShoubunrui != null && msDmShoubunrui.MsDmShoubunruiID.Length > 0)
                {
                    check = serviceClient.DmKoubunshoKisoku_GetRecordsByShoubunruiID(NBaseCommon.Common.LoginUser, msDmShoubunrui.MsDmShoubunruiID);
                }
                else
                {
                    check = serviceClient.DmKoubunshoKisoku_GetRecordsByBunruiID(NBaseCommon.Common.LoginUser, msDmBunrui.MsDmBunruiID);
                }
                if (check != null || check.Count > 0)
                {
                    //var sameNo = from p in check
                    //               where p.BunshoNo == textBox_BunshoNo.Text
                    //               select p;
                    //if (sameNo.Count<DmKoubunshoKisoku>() > 0)
                    //{
                    //    MessageBox.Show("同一の文書番号が存在します", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return false;
                    //}
                    //var sameName = from p in check
                    //               where p.BunshoName == textBox_BunshoName.Text
                    //               select p;
                    //if (sameName.Count<DmKoubunshoKisoku>() > 0)
                    //{
                    //    MessageBox.Show("同一の文書名が存在します", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return false;
                    //}

                    if (check.Any(o => o.BunshoNo == textBox_BunshoNo.Text && o.BunshoName == textBox_BunshoName.Text))
                    {
                        MessageBox.Show("同一の文書番号および文書名が存在します", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion


        /// <summary>
        /// "公文書_規則(DmKoubunshoKisoku)"情報をセットする
        /// </summary>  
        #region private void FillInstance()
        private void FillInstance()
        {
            try
            {
                if (dmKoubunshoKisoku == null)
                {
                    dmKoubunshoKisoku = new DmKoubunshoKisoku();
                    dmKoubunshoKisokuFile = new DmKoubunshoKisokuFile();
                    dmKoukaiSakis = new List<DmKoukaiSaki>();
                    dmPublishers = new List<DmPublisher>();
                }

                FillKoubunshoKisokuFile();
                FillKoukaiSakis();
                FillPublishers();

                MsDmBunrui msDmBunrui = comboBox_Bunrui.SelectedItem as MsDmBunrui;
                dmKoubunshoKisoku.MsDmBunruiID = msDmBunrui.MsDmBunruiID;
                MsDmShoubunrui msDmShoubunrui = comboBox_Shoubunrui.SelectedItem as MsDmShoubunrui;
                dmKoubunshoKisoku.MsDmShoubunruiID = msDmShoubunrui.MsDmShoubunruiID;
                dmKoubunshoKisoku.BunshoNo = textBox_BunshoNo.Text;
                dmKoubunshoKisoku.BunshoName = textBox_BunshoName.Text;
                dmKoubunshoKisoku.Status = (int)NBaseData.DS.DocConstants.StatusEnum.未完了;
                dmKoubunshoKisoku.IssueDate = (DateTime)nullableDateTimePicker_IssueDate.Value;
                dmKoubunshoKisoku.Bikou = textBox_Bikou.Text;
                dmKoubunshoKisoku.FileName = dmKoubunshoKisokuFile.FileName;
                dmKoubunshoKisoku.FileUpdateDate = dmKoubunshoKisokuFile.UpdateDate;
                
                dmKoubunshoKisoku.BunruiName = msDmBunrui.Name;
            }
            catch
            {
                return;
            }
            return;
        }
        #endregion

        /// <summary>
        /// "公文書_規則ファイル(DmKoubunshoKisokuFile)"情報をセットする
        /// </summary>
        #region private void FillKoubunshoKisokuFile()
        private void FillKoubunshoKisokuFile()
        {
            string FullPath = textBox_FileName.Text;

            // ファイル情報をセット
            dmKoubunshoKisokuFile.FileName = System.IO.Path.GetFileName(FullPath);
            dmKoubunshoKisokuFile.UpdateDate = System.IO.File.GetLastWriteTime(FullPath); // 更新日時

            // ファイルを読み込む
            System.IO.FileStream fs = new System.IO.FileStream(FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            dmKoubunshoKisokuFile.Data = new byte[fs.Length];
            fs.Read(dmKoubunshoKisokuFile.Data, 0, dmKoubunshoKisokuFile.Data.Length);
            fs.Close();
        }
        #endregion

        /// <summary>
        /// "公開先(DmKoukaiSaki)"情報をセットする
        /// </summary>
        #region private void FillKoukaiSakis()
        private void FillKoukaiSakis()
        {
            dmKoukaiSakis = documentGroupCheckBox1.ConvertKoukaiSakiList(NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則);
        }
        #endregion

        /// <summary>
        /// "登録者(DmPublisher)"情報をセットする
        /// </summary>
        #region private void FillPublishers()
        private void FillPublishers()
        {
            DmPublisher publisher = new DmPublisher();
            publisher.LinkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;


            // 登録は以下のグループを優先する
            if (NBaseCommon.Common.LoginUser.BumonID == ((int)MsBumon.MsBumonIdEnum.海務部).ToString() ||
                NBaseCommon.Common.LoginUser.BumonID == ((int)MsBumon.MsBumonIdEnum.工務部).ToString() ||
                NBaseCommon.Common.LoginUser.BumonID == ((int)MsBumon.MsBumonIdEnum.船員部).ToString())
            {
                publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.部門;
                publisher.MsBumonID = NBaseCommon.Common.LoginUser.BumonID;
            }
            else if (NBaseCommon.Common.LoginUser.DocFlag_CEO == 1)
            {
                publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.経営責任者;
            }
            else if (NBaseCommon.Common.LoginUser.DocFlag_Admin == 1)
            {
                publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.管理責任者;
            }
            else if (NBaseCommon.Common.LoginUser.DocFlag_MsiFerry == 1)
            {
                publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.海務監督;
            }
            else if (NBaseCommon.Common.LoginUser.DocFlag_CrewFerry == 1)
            {
                publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.船員担当者;
            }
            else if (NBaseCommon.Common.LoginUser.DocFlag_TsiFerry == 1)
            {
                publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.工務監督;
            }
            else if (NBaseCommon.Common.LoginUser.DocFlag_Officer == 1)
            {
                publisher.KoukaiSaki = (int)NBaseData.DS.DocConstants.RoleEnum.役員;
            }
            else  // 残件：上記のどれでもない場合はどうするのか？
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
