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
    public partial class 年度指定出力Form : Form
    {
        public enum 帳票種別 { 休日付与簿, 休暇消化状況, 乗下船記録書, 乗下船カード }
        private 帳票種別 type;


        public 年度指定出力Form(帳票種別 type)
        {
            this.type = type;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox年度();

            if (type == 帳票種別.休日付与簿)
            {
                Text = "休日付与簿";
            }
            else if (type == 帳票種別.休暇消化状況)
            {
                Text = "休暇消化状況";
            }
            else if (type == 帳票種別.乗下船記録書)
            {
                Text = "乗下船記録書";
            }
            else if (type == 帳票種別.乗下船カード)
            {
                Text = "乗下船カード";
            }
        }


        private void InitComboBox年度()
        {
            int thisYear = DateTime.Now.Year;

            if (1 <= DateTime.Now.Month && DateTime.Now.Month <= 3)
            {
                thisYear--;
            }

            for (int i = 0; i < 10; i++)
            {
                comboBox年度.Items.Add(thisYear - i);
            }

            comboBox年度.SelectedItem = thisYear;
        }


        private void button出力_Click(object sender, EventArgs e)
        {
            DateTime date = new DateTime((int)comboBox年度.SelectedItem, 4, 1);

            saveFileDialog1.FileName = Text + "_" + date.ToString("yyyy") + ".xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                byte[] result = null;

                //2013/12/17 追加 m.y
                //サーバーエラー時のフラグ
                bool serverError = false;

                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                  using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                  {
                      //-----------------------
                      //2013/12/18 コメントアウト m.y
                      #region
                      //if (type == 帳票種別.休日付与簿)
                      //{
                      //    result = serviceClient.BLC_Excel_休日付与簿出力(NBaseCommon.Common.LoginUser, date);
                      //}
                      //else if (type == 帳票種別.休暇消化状況)
                      //{
                      //    result = serviceClient.BLC_Excel_休暇消化状況出力(NBaseCommon.Common.LoginUser, date);
                      //}
                      //else if (type == 帳票種別.乗下船記録書)
                      //{
                      //    result = serviceClient.BLC_Excel_乗下船記録書出力(NBaseCommon.Common.LoginUser, date);
                      //}
                      //else if (type == 帳票種別.乗下船カード)
                      //{
                      //    result = serviceClient.BLC_Excel_乗下船カード出力(NBaseCommon.Common.LoginUser, date);
                      //}
                      #endregion
                      //-----------------------
                      try
                      {
                          if (type == 帳票種別.休日付与簿)
                          {
                              result = serviceClient.BLC_Excel_休日付与簿出力(NBaseCommon.Common.LoginUser, date);
                          }
                          else if (type == 帳票種別.休暇消化状況)
                          {
                              result = serviceClient.BLC_Excel_休暇消化状況出力(NBaseCommon.Common.LoginUser, date);
                          }
                          else if (type == 帳票種別.乗下船記録書)
                          {
                              result = serviceClient.BLC_Excel_乗下船記録書出力(NBaseCommon.Common.LoginUser, date);
                          }
                          else if (type == 帳票種別.乗下船カード)
                          {
                              result = serviceClient.BLC_Excel_乗下船カード出力(NBaseCommon.Common.LoginUser, date);
                          }
                      }
                      catch (Exception exp) 
                      {
                          //MessageBox.Show(exp.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                          //serverError = true;//ここでreturnしても関数から抜け出せないのでフラグを用いる
                      }
                  }
                }, "データ取得中です...");
                progressDialog.ShowDialog();

                //-----------------------
                //2013/12/18 追加 m.y 
                if (serverError == true)
                    return;

                if (result == null)
                {
                    #region エラーメッセージ表示
                    if (type == 帳票種別.休日付与簿)
                    {
                         MessageBox.Show("休日付与簿の出力に失敗しました\n(テンプレートファイルがありません。管理者に確認してください。)"
                            , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (type == 帳票種別.休暇消化状況)
                    {
                        MessageBox.Show("休暇消化状況の出力に失敗しました\n(テンプレートファイルがありません。管理者に確認してください。)"
                            , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (type == 帳票種別.乗下船記録書)
                    {
                        MessageBox.Show("乗下船記録書の出力に失敗しました\n(テンプレートファイルがありません。管理者に確認してください。)"
                            , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (type == 帳票種別.乗下船カード)
                    {
                        MessageBox.Show("乗下船カードの出力に失敗しました\n(テンプレートファイルがありません。管理者に確認してください。)"
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
