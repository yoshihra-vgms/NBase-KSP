using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using System.IO;

namespace Hachu.Reports
{
    class 注文書
    {
        string BaseFileName = "注文書";

        public void Output(
            string odMkId,
            string telNo,
            string faxNo,
            string bikou
            )
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = BaseFileName + " (*.pdf)|*.pdf";
            fd.FileName = BaseFileName + ".pdf";

            if (fd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            string message = "";
            bool outputResult = false;
            try
            {
                byte[] excelData = null;
                NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                {
                    excelData = MakeFile(odMkId, telNo, faxNo, bikou);
                }, "注文書を作成中です...");
                progressDialog.ShowDialog();
                if (excelData == null)
                {
                    MessageBox.Show("注文書の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                System.IO.FileStream filest = new System.IO.FileStream(fd.FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
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
            }
            else
            {
                // 失敗 
                MessageBox.Show("注文書の出力に失敗しました。\n (Err:" + message + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private byte[] MakeFile(string odMkId, string telNo, string faxNo,string bikou)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                byte[] excelData = serviceClient.BLC_PDF注文書_取得(odMkId, telNo, faxNo, bikou);

                return excelData;
            }
        }
    }
}
