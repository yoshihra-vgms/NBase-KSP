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

namespace NBaseMaster.Doc.ドキュメント分類管理
{
    public partial class ドキュメント分類管理Form : Form
    {
        List<MsDmBunrui> msDmBunruis = new List<MsDmBunrui>();

        public ドキュメント分類管理Form()
        {
            InitializeComponent();
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
                    msDmBunruis = serviceClient.MsDmBunrui_GetRecordsByName(NBaseCommon.Common.LoginUser, BunruiName_textBox.Text);
                }
                    
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("obj", typeof(MsDmBunrui)));
                dt.Columns.Add(new DataColumn("ﾄﾞｷｭﾒﾝﾄ分類ｺｰﾄﾞ", typeof(string)));
                dt.Columns.Add(new DataColumn("ﾄﾞｷｭﾒﾝﾄ分類名", typeof(string)));

                foreach (MsDmBunrui msDmBunrui in msDmBunruis)
                {
                    DataRow row = dt.NewRow();
                    row["obj"] = msDmBunrui;
                    row["ﾄﾞｷｭﾒﾝﾄ分類ｺｰﾄﾞ"] = msDmBunrui.Code;
                    row["ﾄﾞｷｭﾒﾝﾄ分類名"] = msDmBunrui.Name;
                   
                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 80;   //ﾄﾞｷｭﾒﾝﾄ分類ｺｰﾄﾞ
                dataGridView1.Columns[2].Width = 180;   //ﾄﾞｷｭﾒﾝﾄ分類名

            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }


        private void Clear_button_Click(object sender, EventArgs e)
        {
            BunruiName_textBox.Text = "";
        }

        /// <summary>
        /// 新規追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_button_Click(object sender, EventArgs e)
        {
            ドキュメント分類管理詳細Form DetailForm = new ドキュメント分類管理詳細Form();

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
            MsDmBunrui msDmBunrui = dataGridView1.SelectedRows[0].Cells[0].Value as MsDmBunrui;

            ドキュメント分類管理詳細Form DetailForm = new ドキュメント分類管理詳細Form();
            DetailForm.msDmBunrui = msDmBunrui;

            DetailForm.ShowDialog();
            Search();
        }
    }
}
