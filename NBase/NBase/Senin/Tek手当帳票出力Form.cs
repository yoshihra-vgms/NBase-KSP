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
using NBaseData.DAC;
using NBaseData.DS;
using NBaseCommon.Senin.Excel;

namespace Senin
{
    public partial class Tek手当帳票出力Form : Form
    {

        public Tek手当帳票出力Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            monthPicker1.Value = DateTimeUtils.ToFromMonth(DateTime.Today);
        }

        private void button出力_Click(object sender, EventArgs e)
        {
            DateTime ym = monthPicker1.Value;

            saveFileDialog1.FileName = $"手当帳票_{ym.ToString("yyyyMM")}.xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                byte[] result = null;
                
                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        
                        try
                        {
                            result = serviceClient.BLC_Excel_手当帳票出力(NBaseCommon.Common.LoginUser, ym);
                        }
                        catch( Exception ex )
                        {
                            ;
                        }
                        //--------------------------------

                    }
                }, "データ取得中です...");
                progressDialog.ShowDialog();

                //--------------------------------
                if (result == null)
                {
                    #region エラーメッセージ表示
                   
                    MessageBox.Show("手当帳票の出力に失敗しました\n(テンプレートファイルがありません。管理者に確認してください。)"
                        , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    #endregion
                    return;
                }
                //--------------------------------

                System.IO.FileStream filest = new System.IO.FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                filest.Write(result, 0, result.Length);
                filest.Close();

                MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Dispose();
            }

        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
