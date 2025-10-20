using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseUtil;
using ServiceReferences.NBaseService;

namespace NBaseMaster.Doc.報告書管理
{
    public partial class 報告書管理Form : Form
    {
        List<MsDmBunrui> msDmBunruis = new List<MsDmBunrui>();
        List<MsDmShoubunrui> msDmShoubunruis = new List<MsDmShoubunrui>();
        List<MsDmHoukokusho> msDmHoukokushos = new List<MsDmHoukokusho>();

        public 報告書管理Form()
        {
            InitializeComponent();
        }

        private void 報告書管理Form_Load(object sender, EventArgs e)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msDmBunruis = serviceClient.MsDmBunrui_GetRecords(NBaseCommon.Common.LoginUser);
                msDmShoubunruis = serviceClient.MsDmShoubunrui_GetRecords(NBaseCommon.Common.LoginUser);
            }
            SetBunruiDDL();
            SetShoubunruiDDL();
        }

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
            textBox_bunshoNo.Text = "";
            textBox_bunshoName.Text = "";
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
        /// 「新規追加」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void Add_button_Click(object sender, EventArgs e)
        private void Add_button_Click(object sender, EventArgs e)
        {
            報告書管理詳細Form DetailForm = new 報告書管理詳細Form();

            DetailForm.ShowDialog();

            Search();
        }
        #endregion


        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region Close_button_Click(object sender, EventArgs e)
        private void Close_button_Click(object sender, EventArgs e)
        {
            Close();
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
        /// 検索実行＆一覧表示
        /// </summary>
        #region private void Search()
        private void Search()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                string bunruiId = "";
                string shoubunruiId = "";
                string bunshoNo = "";
                string bunshoName = "";

                if (comboBox_Bunrui.SelectedItem is MsDmBunrui)
                {
                    bunruiId = (comboBox_Bunrui.SelectedItem as MsDmBunrui).MsDmBunruiID;
                }
                if (comboBox_Shoubunrui.SelectedItem is MsDmShoubunrui)
                {
                    shoubunruiId = (comboBox_Shoubunrui.SelectedItem as MsDmShoubunrui).MsDmShoubunruiID;
                }
                bunshoNo = textBox_bunshoNo.Text;
                bunshoName = textBox_bunshoName.Text;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    msDmHoukokushos = serviceClient.MsDmHoukokusho_SearchRecords(NBaseCommon.Common.LoginUser, bunruiId, shoubunruiId, bunshoNo, bunshoName);
                }

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(MsDmHoukokusho)));
                dt.Columns.Add(new DataColumn("ﾄﾞｷｭﾒﾝﾄ分類名", typeof(string)));
                dt.Columns.Add(new DataColumn("ﾄﾞｷｭﾒﾝﾄ小分類名", typeof(string)));
                dt.Columns.Add(new DataColumn("文書番号", typeof(string)));
                dt.Columns.Add(new DataColumn("文書名", typeof(string)));

                foreach (MsDmHoukokusho msDmHoukokusho in msDmHoukokushos)
                {
                    DataRow row = dt.NewRow();
                    row["obj"] = msDmHoukokusho;
                    row["ﾄﾞｷｭﾒﾝﾄ分類名"] = msDmHoukokusho.BunruiName;
                    row["ﾄﾞｷｭﾒﾝﾄ小分類名"] = msDmHoukokusho.ShoubunruiName;
                    row["文書番号"] = msDmHoukokusho.BunshoNo;
                    row["文書名"] = msDmHoukokusho.BunshoName;
                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 200;   //ﾄﾞｷｭﾒﾝﾄ分類名
                dataGridView1.Columns[2].Width = 200;   //ﾄﾞｷｭﾒﾝﾄ小分類名
                dataGridView1.Columns[3].Width = 100;   //文書番号
                dataGridView1.Columns[4].Width = 200;   //文書名

            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion

        /// <summary>
        /// 一覧ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MsDmHoukokusho msDmHoukokusho = dataGridView1.SelectedRows[0].Cells[0].Value as MsDmHoukokusho;

            報告書管理詳細Form DetailForm = new 報告書管理詳細Form();
            DetailForm.msDmHoukokusho = msDmHoukokusho;

            DetailForm.ShowDialog();
            Search();
        }
        #endregion

        private void button出力_Click(object sender, EventArgs e)
        {
            if (msDmHoukokushos == null || msDmHoukokushos.Count == 0)
            {
                MessageBox.Show("データが検索されていません", "報告書管理");
                return;
            }

            FileUtils.SetDesktopFolder(saveFileDialog1);

            saveFileDialog1.FileName = "報告書管理一覧_" + DateTime.Today.ToString("yyyyMMdd") + ".xlsx";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    byte[] result = null;
                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            result = serviceClient.BLC_Excel_報告書管理一覧表出力(NBaseCommon.Common.LoginUser, msDmHoukokushos);
                        }
                    }, "報告書管理一覧を出力中です...");
                    progressDialog.ShowDialog();

                    if (result == null)
                    {
                        MessageBox.Show("報告書管理一覧の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "報告書管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = System.Windows.Forms.Cursors.Default;
                        return;
                    }

                    System.IO.FileStream filest = new System.IO.FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                    filest.Write(result, 0, result.Length);
                    filest.Close();

                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    //カーソルを通常に戻す
                    this.Cursor = System.Windows.Forms.Cursors.Default;
                    MessageBox.Show("報告書管理一覧の出力に失敗しました。\n (Err:" + ex.Message + ")", "報告書管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
