using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hachu.Reports
{
    public class Data船用品 : BaseData
    {
        string BaseFileName = "船用品発注管理表";
        string ErrMessage = "";
        bool outputBusy = false;

        public Data船用品(string fn, string cn)
        {
            formNumber = fn;
            className = cn;
        }
        public override void Output(int FromYear, int FromMonth, int ToYear, int ToMonth)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = BaseFileName + " (*.xlsx)|*.xlsx";
            fd.FileName = BaseFileName + ".xlsx";
            NBaseUtil.FileUtils.SetDesktopFolder(fd);

            if (fd.ShowDialog() == DialogResult.OK)
            {
                ErrMessage = "";
                string message = "";
                bool outputResult = false;
                try
                {
                    byte[] excelData = null;
                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        excelData = MakeFile(FromYear, FromMonth, ToYear, ToMonth);
                    }, "船用品管理表を作成中です...");
                    progressDialog.ShowDialog();

                    if (ErrMessage != "")
                    {
                        if (outputBusy)
                        {
                            // 情報 
                            message = ErrMessage;
                            MessageBox.Show(ErrMessage, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // 失敗 
                            message = "船用品管理表の出力に失敗しました。\n (Err:" + ErrMessage + ")";
                            MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return;
                    }
                    if (excelData == null)
                    {
                        MessageBox.Show("船用品管理表の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    System.IO.FileStream filest = new System.IO.FileStream(fd.FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                    filest.Write(excelData, 0, excelData.Length);
                    filest.Close();

                    outputResult = true;
                }
                catch (Exception ex)
                {
                    message = "船用品管理表の出力に失敗しました。\n (Err:" + ex.InnerException.Message + ")";
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
                    if (outputBusy)
                    {
                        // 情報 
                        MessageBox.Show(message, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // 失敗 
                        MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private byte[] MakeFile(int FromYear, int FromMonth, int ToYear, int ToMonth)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                try
                {
                    byte[] excelData = serviceClient.BLC_Excel船用品発注管理表_取得(NBaseCommon.Common.LoginUser, FromYear, FromMonth, ToYear, ToMonth);

                    return excelData;
                }
                catch (System.ServiceModel.FaultException fex)
                {
                    // WCFｻｰﾋﾞｽ内で発生した例外は、すべて System.ServiceModel.FaultException として返される
                    if (fex.Message == "")
                    {
                        outputBusy = true;
                        ErrMessage = "帳票出力が込み合っています。時間をおいて再度出力して下さい。";
                    }
                    else
                    {
                        outputBusy = false;
                        ErrMessage = fex.Message;
                    }
                    throw new Exception(ErrMessage);
                }
            }
        }
    }
}
