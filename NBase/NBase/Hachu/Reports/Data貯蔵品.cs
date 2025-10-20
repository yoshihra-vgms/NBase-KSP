using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hachu.Reports
{
    public class Data貯蔵品 : BaseData
    {
        //------------------------------------------------------------------------------
        // add Miura start
        string BaseFileName = "貯蔵品管理表";
        NBaseData.BLC.貯蔵品リスト.対象Enum kind;

        public Data貯蔵品(string fn, string cn, NBaseData.BLC.貯蔵品リスト.対象Enum kind)
        {
            this.formNumber = fn;
            this.className = cn;
            this.kind = kind;
        }
        public override void Output(NBaseData.DAC.MsVessel MsVessel, int FromYear, int FromMonth, int ToYear, int ToMonth)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = BaseFileName + " (*.xlsx)|*.xlsx";
            fd.FileName = BaseFileName + ".xlsx";
            NBaseUtil.FileUtils.SetDesktopFolder(fd);

            if (fd.ShowDialog() == DialogResult.OK)
            {
                string message = "";
                bool outputResult = false;
                try
                {
                    byte[] excelData = null;
                    NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                    {
                        excelData = MakeFile(MsVessel, FromYear, FromMonth, ToYear, ToMonth);
                    }, "貯蔵品管理表を作成中です...");
                    progressDialog.ShowDialog();
                    if (excelData == null)
                    {
                        MessageBox.Show("貯蔵品管理表の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("貯蔵品管理表の出力に失敗しました。\n (Err:" + message + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private byte[] MakeFile(NBaseData.DAC.MsVessel MsVessel, int FromYear, int FromMonth, int ToYear, int ToMonth)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                byte[] excelData = serviceClient.BLC_Excel貯蔵品管理表_出力(NBaseCommon.Common.LoginUser, MsVessel, kind, FromYear, FromMonth, ToYear, ToMonth);

                return excelData;
            }
        }
    }
}
