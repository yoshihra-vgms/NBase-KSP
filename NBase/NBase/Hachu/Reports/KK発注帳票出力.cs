using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using System.IO;
using NBaseCommon.Hachu.Excel;

namespace Hachu.Reports
{
    class KK発注帳票出力
    {
        private static 発注帳票Common.kubun発注帳票 Kubun;
        private static string BaseFileName = null;


        public static void 査定表Output(string odThiId)
        {
            Kubun = 発注帳票Common.kubun発注帳票.査定表;
            BaseFileName = "査定表";

            Output(odThiId);
        }

        public static void 請求書Output(string odThiId)
        {
            Kubun = 発注帳票Common.kubun発注帳票.請求書;
            BaseFileName = "請求書";

            Output(odThiId);
        }

        public static void 注文書Output(string odMkId)
        {
            Kubun = 発注帳票Common.kubun発注帳票.注文書;
            BaseFileName = "注文書";

            Output(odMkId);
        }

        public static void 見積依頼書Output(string odMkId)
        {
            Kubun = 発注帳票Common.kubun発注帳票.見積依頼書;
            BaseFileName = "見積り依頼書";

            Output(odMkId);
        }


        public static void Output(string Id)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = BaseFileName + " (*.xlsx)|*.xlsx";
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
                    excelData = MakeFile(Id);
                }, BaseFileName + "を作成中です...");
                progressDialog.ShowDialog();
                if (excelData == null)
                {
                    MessageBox.Show(BaseFileName + "の出力に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(BaseFileName + "の出力に失敗しました。\n (Err:" + message + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static byte[] MakeFile(string Id)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                byte[] excelData = serviceClient.BLC_KK発注帳票_取得(NBaseCommon.Common.LoginUser, Kubun, Id);

                return excelData;
            }
        }
    }
}
