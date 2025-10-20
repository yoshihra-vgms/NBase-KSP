using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using System.IO;
using NBaseData.BLC;

namespace Document.Reports
{
    public class 状況確認一覧
    {
        string BaseFileName = "確認状況一覧";

        public void Output(List<状況確認一覧Row> 状況確認一覧Rows)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = BaseFileName + " (*.xls)|*.xlsx";
            fd.FileName = BaseFileName + ".xlsx";

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
                    excelData = MakeFile(状況確認一覧Rows);
                }, "確認状況一覧を作成中です...");
                progressDialog.ShowDialog();
                if (excelData == null)
                {
                    MessageBox.Show("確認状況一覧の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("確認状況一覧の出力に失敗しました。\n (Err:" + message + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private byte[] MakeFile(List<状況確認一覧Row> 状況確認一覧Rows)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                byte[] excelData = serviceClient.BLC_Excel確認状況一覧_取得(NBaseCommon.Common.LoginUser, 状況確認一覧Rows);

                return excelData;
            }
        }
    }
}
