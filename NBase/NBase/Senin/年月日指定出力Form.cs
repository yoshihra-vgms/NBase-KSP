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
    public partial class 年月日指定出力Form : Form
    {
        public enum 帳票種別 { 個人情報一覧, クルーリスト }
        private 帳票種別 type;


        public 年月日指定出力Form(帳票種別 type)
        {
            this.type = type;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            dateTimePicker1.Value = DateTime.Now;

            if (type == 帳票種別.個人情報一覧)
            {
                Text = "個人情報一覧";
            }
            else if (type == 帳票種別.クルーリスト)
            {
                Text = "クルーリスト";
            }
        }


        private void button出力_Click(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;

            saveFileDialog1.FileName = Text + "_" + date.ToString("yyyyMMdd") + ".xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                byte[] result = null;

                //2013/12/17 追加 m.y
                //サーバーエラー時の時のフラグ
                bool serverError = false;

                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        //-----------------------
                        //2013/12/17 コメントアウト m.y
                        #region 
                        //if (type == 帳票種別.個人情報一覧)
                        //{
                        //    result = serviceClient.BLC_Excel_個人情報一覧出力(NBaseCommon.Common.LoginUser, date, int.MinValue);
                        //}
                        //else if (type == 帳票種別.クルーリスト)
                        //{
                        //    result = serviceClient.BLC_Excel_クルーリスト出力(NBaseCommon.Common.LoginUser, date, int.MinValue);
                        //}
                        #endregion
                        //-----------------------
                        //013/12/17 変更: ServiceClientのExceptionを受け取る m.y
                        try
                        {
                            if (type == 帳票種別.個人情報一覧)
                            {
                                result = serviceClient.BLC_Excel_個人情報一覧出力(NBaseCommon.Common.LoginUser, date, int.MinValue);
                            }
                            else if (type == 帳票種別.クルーリスト)
                            {
                                result = serviceClient.BLC_Excel_クルーリスト出力(NBaseCommon.Common.LoginUser, date, int.MinValue);
                            }
                        }
                        catch( Exception ex )
                        {
                            //MessageBox.Show( ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
                            //serverError = true;//ここでreturnしても関数から抜け出せないのでフラグを用いる
                        }
                        //-----------------------
                    }
                }, "データ取得中です...");
                progressDialog.ShowDialog();

                //-----------------------
                //2013/12/17 追加 m.y 
                if (serverError == true)
                    return;

                if (result == null)
                {
                    #region エラーメッセージ表示
                    if (type == 帳票種別.個人情報一覧)
                    {
                        MessageBox.Show("個人情報一覧の出力に失敗しました\n(テンプレートファイルがありません。管理者に確認してください。)"
                            , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("クルーリストの出力に失敗しました\n(テンプレートファイルがありません。管理者に確認してください。)"
                            , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion
                    return;
                }
                //-----------------------

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
