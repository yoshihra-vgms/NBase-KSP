using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.BLC.動静帳票;

namespace Dousei
{
    public partial class エネルギー報告書Form : Form
    {
        public エネルギー報告書Form()
        {
            InitializeComponent();
        }

        private void エネルギー報告書Form_Load(object sender, EventArgs e)
        {
            Year_comboBox.Items.Clear();
            int Start = 2009;
            for (; Start <= DateTime.Today.Year; Start++)
            {
                Year_comboBox.Items.Add(Start.ToString());
            }
            Year_comboBox.SelectedItem = DateTime.Today.Year.ToString();
            Month_comboBox.SelectedItem = DateTime.Today.Month.ToString("d2");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = 改正省エネ法エネルギー報告書.TemplateName + " (*.xlsx)|*.xlsx";
            fd.FileName = 改正省エネ法エネルギー報告書.TemplateName + ".xlsx";


            DateTime DateMonth = DateTime.Parse(Year_comboBox.SelectedItem.ToString() + "/" + Month_comboBox.SelectedItem.ToString() + "/1");

            if (fd.ShowDialog() == DialogResult.OK)
            {
                string message = "";
                bool outputResult = false;
                try
                {
                    byte[] excelData = null;
                    //using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    //{
                    //    excelData = serviceClient.BLC_動静帳票_改正省エネ法エネルギー報告書_取得(NBaseCommon.Common.LoginUser, DateMonth);
                    //}
                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                        {
                            excelData = serviceClient.BLC_動静帳票_改正省エネ法エネルギー報告書_取得(NBaseCommon.Common.LoginUser, DateMonth);
                        }
                    }, "改正省エネ法エネルギー報告書を作成中です...");
                    progressDialog.ShowDialog();


                    if (excelData == null)
                    {
                        MessageBox.Show("改正省エネ法エネルギー報告書の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    System.IO.FileStream filest = new System.IO.FileStream(fd.FileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                    filest.Write(excelData, 0, excelData.Length);
                    filest.Close();

                    outputResult = true;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    outputResult = false;
                }
                if (outputResult == true)
                {
                    // 成功
                    message = "「" + fd.FileName + "」へ出力しました";
                    MessageBox.Show(message, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    // 失敗 
                    MessageBox.Show("改正省エネ法エネルギー報告書の出力に失敗しました。\n (Err:" + message + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
