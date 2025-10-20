using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.BLC;
using NBaseData.DAC;
using ServiceReferences.NBaseService;
using NBaseCommon;

namespace Document
{
    public partial class コメント登録Form : ExForm
    {
        private string DIALOG_TITLE = "コメント登録";
        public 状況確認一覧Row RowData = null;
        private DmDocComment DocComment = null;
        private DmKanriKirokuFile KanriKirokuFile = null;
        private DmKoubunshoKisokuFile KoubunshoKisokuFile = null;
        
        //データを編集したかどうか？
        private bool ChangeFlag = false;

        public コメント登録Form()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", DIALOG_TITLE, ServiceReferences.WcfServiceWrapper.ConnectedServerID);
        }

        private void コメント登録Form_Load(object sender, EventArgs e)
        {
            InitForm();
            this.ChangeParentFormSize(FormWindowState.Minimized);
            this.Activate();
        }

        private void コメント登録Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ChangeParentFormSize(FormWindowState.Normal);
        }

        /// <summary>
        /// 「コメント登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_コメント登録_Click(object sender, EventArgs e)
        private void button_コメント登録_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                return;
            }

            FillInstance();

            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                ret = serviceClient.BLC_コメント登録処理_登録(NBaseCommon.Common.LoginUser, DocComment, KanriKirokuFile, KoubunshoKisokuFile);
            }

            if (ret == true)
            {
                MessageBox.Show("更新しました", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.DialogResult = DialogResult.OK;
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
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.RestoreDirectory = true;
            ofd.Filter = "記録ファイル(*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.FileName = "";

            if (ofd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                textBox_FileName.Text = ofd.FileName;
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
        /// フォームに情報をセットする
        /// </summary>
        #region private void InitForm()
        private void InitForm()
        {
            label_Bunrui.Text = "： " + RowData.分類名;
            label_Shoubunrui.Text = "： " + RowData.小分類名;
            label_BunshoNo.Text = "： " + RowData.文書番号;
            label_BunshoName.Text = "： " + RowData.文書名;

            nullableDateTimePicker_RegDate.Value = DateTime.Now;
        }
        #endregion

        /// <summary>
        /// 入力値のチェック
        /// </summary>
        /// <returns></returns>
        #region private bool Validation()
        private bool Validation()
        {
            if (nullableDateTimePicker_RegDate.Value == null)
            {
                MessageBox.Show("登録日が設定されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            DateTime regDate = (DateTime)nullableDateTimePicker_RegDate.Value;
            if (DateTime.Parse(regDate.ToShortDateString()) < DateTime.Today)
            {
                MessageBox.Show("過去の登録日が設定されています", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (textBox_FileName.Text.Length == 0)
            {
                MessageBox.Show("登録するファイルが設定されていません", DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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
            return true;
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        #region private void FillInstance()
        private void FillInstance()
        {
            try
            {
                DocComment = new DmDocComment();
                DocComment.RegDate = (DateTime)nullableDateTimePicker_RegDate.Value;
                DocComment.MsUserID = NBaseCommon.Common.LoginUser.MsUserID;

                DocComment.DocFlag_CEO = NBaseCommon.Common.LoginUser.DocFlag_CEO;
                DocComment.DocFlag_Admin = NBaseCommon.Common.LoginUser.DocFlag_Admin;
                DocComment.DocFlag_MsiFerry = NBaseCommon.Common.LoginUser.DocFlag_MsiFerry;
                DocComment.DocFlag_CrewFerry = NBaseCommon.Common.LoginUser.DocFlag_CrewFerry;
                DocComment.DocFlag_TsiFerry = NBaseCommon.Common.LoginUser.DocFlag_TsiFerry;
                //DocComment.DocFlag_MsiCargo = NBaseCommon.Common.LoginUser.DocFlag_MsiCargo;
                //DocComment.DocFlag_CrewCargo = NBaseCommon.Common.LoginUser.DocFlag_CrewCargo;
                //DocComment.DocFlag_TsiCargo = NBaseCommon.Common.LoginUser.DocFlag_TsiCargo;
                DocComment.DocFlag_Officer = NBaseCommon.Common.LoginUser.DocFlag_Officer;
                //DocComment.DocFlag_SdManager = NBaseCommon.Common.LoginUser.DocFlag_SdManager;

                if (RowData.DmKanriKirokuId != null && RowData.DmKanriKirokuId.Length > 0)
                {
                    DocComment.LinkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.管理記録;
                    DocComment.LinkSakiID = RowData.DmKanriKirokuId;

                    KanriKirokuFile = new DmKanriKirokuFile();
                    KanriKirokuFile.DmKanriKirokuID = RowData.DmKanriKirokuId;
                    FillKanriKirokuFile();
                }
                else
                {
                    DocComment.LinkSaki = (int)NBaseData.DS.DocConstants.LinkSakiEnum.公文書_規則;
                    DocComment.LinkSakiID = RowData.DmKoubunshoKisokuId;

                    KoubunshoKisokuFile = new DmKoubunshoKisokuFile();
                    KoubunshoKisokuFile.DmKoubunshoKisokuID = RowData.DmKoubunshoKisokuId;
                    FillKoubunshoKisokuFile();
                }
            }
            catch
            {
                return;
            }
            return;
        }
        #endregion
        #region private void FillKanriKirokuFile()
        private void FillKanriKirokuFile()
        {
            string FullPath = textBox_FileName.Text;
            KanriKirokuFile.FileName = System.IO.Path.GetFileName(FullPath);
            KanriKirokuFile.UpdateDate = System.IO.File.GetLastWriteTime(FullPath); // 更新日時

            System.IO.FileStream fs = new System.IO.FileStream(FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            KanriKirokuFile.Data = new byte[fs.Length];
            fs.Read(KanriKirokuFile.Data, 0, KanriKirokuFile.Data.Length);
            fs.Close();
        }
        #endregion
        #region private void FillKoubunshoKisokuFile()
        private void FillKoubunshoKisokuFile()
        {
            string FullPath = textBox_FileName.Text;
            KoubunshoKisokuFile.FileName = System.IO.Path.GetFileName(FullPath);
            KoubunshoKisokuFile.UpdateDate = System.IO.File.GetLastWriteTime(FullPath); // 更新日時

            System.IO.FileStream fs = new System.IO.FileStream(FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            KoubunshoKisokuFile.Data = new byte[fs.Length];
            fs.Read(KoubunshoKisokuFile.Data, 0, KoubunshoKisokuFile.Data.Length);
            fs.Close();
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
