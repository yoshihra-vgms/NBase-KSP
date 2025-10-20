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

namespace NBaseMaster.Doc.ドキュメント小分類管理
{
    public partial class ドキュメント小分類管理Form : Form
    {
        List<MsDmShoubunrui> msDmShoubunruis = new List<MsDmShoubunrui>();

        public ドキュメント小分類管理Form()
        {
            InitializeComponent();
        }

        private void ドキュメント小分類管理Form_Load(object sender, EventArgs e)
        {
            #region ドキュメント分類
            List<MsDmBunrui> msDmBunruis = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                msDmBunruis = serviceClient.MsDmBunrui_GetRecords(NBaseCommon.Common.LoginUser);
            }
            Bunrui_comboBox.Items.Clear();
            MsDmBunrui dmy = new MsDmBunrui();
            dmy.MsDmBunruiID = "";
            dmy.Name = "";
            Bunrui_comboBox.Items.Add(dmy);
            foreach (MsDmBunrui msDmBunrui in msDmBunruis)
            {
                Bunrui_comboBox.Items.Add(msDmBunrui);
            }
            #endregion
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ドキュメント小分類管理詳細Form form = new ドキュメント小分類管理詳細Form();
            form.ShowDialog();
        }

        private void Search_button_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    string bunruiId = "";
                    MsDmBunrui bunrui = Bunrui_comboBox.SelectedItem as MsDmBunrui;
                    if (bunrui != null)
                    {
                        bunruiId = bunrui.MsDmBunruiID;
                    }
                    msDmShoubunruis = serviceClient.MsDmShoubunrui_GetRecordsByNameAndBunruiID(NBaseCommon.Common.LoginUser, Name_textBox.Text, bunruiId);
                }

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(MsDmShoubunrui)));
                dt.Columns.Add(new DataColumn("ﾄﾞｷｭﾒﾝﾄ分類名", typeof(string)));
                dt.Columns.Add(new DataColumn("ﾄﾞｷｭﾒﾝﾄ小分類ｺｰﾄﾞ", typeof(string)));
                dt.Columns.Add(new DataColumn("ﾄﾞｷｭﾒﾝﾄ小分類名", typeof(string)));

                foreach (MsDmShoubunrui msDmShoubunrui in msDmShoubunruis)
                {
                    DataRow row = dt.NewRow();
                    row["obj"] = msDmShoubunrui;
                    row["ﾄﾞｷｭﾒﾝﾄ分類名"] = msDmShoubunrui.BunruiName;
                    row["ﾄﾞｷｭﾒﾝﾄ小分類ｺｰﾄﾞ"] = msDmShoubunrui.Code;
                    row["ﾄﾞｷｭﾒﾝﾄ小分類名"] = msDmShoubunrui.Name;

                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 180;   //ﾄﾞｷｭﾒﾝﾄ分類名
                dataGridView1.Columns[2].Width = 80;   //ﾄﾞｷｭﾒﾝﾄ小分類ｺｰﾄﾞ
                dataGridView1.Columns[3].Width = 250;   //ﾄﾞｷｭﾒﾝﾄ小分類名

            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }


        private void Clear_button_Click(object sender, EventArgs e)
        {
            Name_textBox.Text = "";
            Bunrui_comboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_button_Click(object sender, EventArgs e)
        {
            ドキュメント小分類管理詳細Form DetailForm = new ドキュメント小分類管理詳細Form();

            DetailForm.ShowDialog();

            Search();
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_button_Click(object sender, EventArgs e)
        {
            Close();
        }


        //選択
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MsDmShoubunrui msDmShoubunrui = dataGridView1.SelectedRows[0].Cells[0].Value as MsDmShoubunrui;

            ドキュメント小分類管理詳細Form DetailForm = new ドキュメント小分類管理詳細Form();
            DetailForm.msDmShoubunrui = msDmShoubunrui;

            DetailForm.ShowDialog();
            Search();
        }
    }
}
