using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseUtil;
using System.IO;

namespace Senin
{
    public partial class 給与連携出力Form : Form
    {

        public 給与連携出力Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox年();
            InitComboBox月();
        }


        private void InitComboBox年()
        {
            int thisYear = DateTime.Now.Year;

            for (int i = 0; i < 3; i++)
            {
                comboBox年.Items.Add(thisYear - i);
            }

            comboBox年.SelectedItem = thisYear;
        }


        private void InitComboBox月()
        {
            for (int i = 0; i < 12; i++)
            {
                string m = (i + 1).ToString();

                comboBox月.Items.Add(m);

                if (m.Trim() == DateTime.Now.Month.ToString())
                {
                    comboBox月.SelectedItem = m;
                }
            }
        }


        private void button出力_Click(object sender, EventArgs e)
        {
            DateTime fromDate = new DateTime((int)comboBox年.SelectedItem, Int32.Parse(comboBox月.SelectedItem as string), 1);
            DateTime toDate = fromDate.AddMonths(1).AddDays(-1);

            byte[] result = null;

            bool serverError = false;

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {

                    try
                    {
                        result = serviceClient.BLC_CSV_給与連携出力(NBaseCommon.Common.LoginUser, fromDate, toDate);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        serverError = true;
                    }
                }
            }, "データ取得中です...");
            progressDialog.ShowDialog();

            //-----------------------
            //2013/12/17 追加 m.y 
            if (serverError == true)
                return;

            if (result == null)
            {
                MessageBox.Show("給与連携ファイルの出力に失敗しました"
                        , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //-----------------------
            if (result != null)
            {

                string basePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                string zipPath = basePath + "\\" + "給与連携_" + fromDate.ToString("yyyyMMdd") + "_" + toDate.ToString("yyyyMMdd") + ".zip";

                System.IO.FileStream filest = new System.IO.FileStream(zipPath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                filest.Write(result, 0, result.Length);
                filest.Close();
            }

            MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Dispose();

        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
