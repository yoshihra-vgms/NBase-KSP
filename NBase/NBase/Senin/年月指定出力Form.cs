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
    public partial class 年月指定出力Form : Form
    {
        public enum 帳票種別 { 船内収支報告書, 科目別集計表, 船用金送金表, 給与手当一覧表 }
        private 帳票種別 type;


        public 年月指定出力Form(帳票種別 type)
        {
            this.type = type;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox年();
            InitComboBox月();

            if (type == 帳票種別.船内収支報告書)
            {
                Text = "船内収支報告書";
            }
            else if (type == 帳票種別.科目別集計表)
            {
                Text = "科目別集計表";
            }
            else if (type == 帳票種別.船用金送金表)
            {
                Text = "船用金送金表";
            }
            else if (type == 帳票種別.給与手当一覧表)
            {
                Text = "給与手当一覧表";
            }
        }


        private void InitComboBox年()
        {
            int thisYear = DateTime.Now.Year;

            for (int i = 0; i < 10; i++)
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
            DateTime date = new DateTime((int)comboBox年.SelectedItem, Int32.Parse(comboBox月.SelectedItem as string), 1);

            saveFileDialog1.FileName = Text + "_" + date.ToString("yyyyMM") + ".xlsx";

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
                        //--------------------------------
                        //2013/12/17 コメントアウト m.y
                        #region
                        //if (type == 帳票種別.船内収支報告書)
                        //{
                        //    result = serviceClient.BLC_Excel_船内収支報告書出力(NBaseCommon.Common.LoginUser, date, int.MinValue);
                        //}
                        //else if (type == 帳票種別.科目別集計表)
                        //{
                        //    result = serviceClient.BLC_Excel_科目別集計表出力(NBaseCommon.Common.LoginUser, date, int.MinValue);
                        //}
                        //else if (type == 帳票種別.船用金送金表)
                        //{
                        //    result = serviceClient.BLC_Excel_船用金送金表出力(NBaseCommon.Common.LoginUser, date, int.MinValue);
                        //}
                        //else
                        //{
                        //    result = null;
                        //}
                        #endregion
                        //--------------------------------
                        //2013/12/17 変更: ServiceClientのExceptionを受け取る m.y
                        try
                        {
                            if (type == 帳票種別.船内収支報告書)
                            {
                                result = serviceClient.BLC_Excel_船内収支報告書出力(NBaseCommon.Common.LoginUser, date, int.MinValue);
                            }
                            else if (type == 帳票種別.科目別集計表)
                            {
                                result = serviceClient.BLC_Excel_科目別集計表出力(NBaseCommon.Common.LoginUser, date, int.MinValue);
                            }
                            else if (type == 帳票種別.船用金送金表)
                            {
                                result = serviceClient.BLC_Excel_船用金送金表出力(NBaseCommon.Common.LoginUser, date, int.MinValue);
                            }
                            else if (type == 帳票種別.給与手当一覧表)
                            {
                                result = serviceClient.BLC_Excel_給与手当一覧表出力(NBaseCommon.Common.LoginUser, date, int.MinValue);
                            }
                            else
                            {
                                result = null;
                            }
                        }
                        catch( Exception ex )
                        {
                            //MessageBox.Show( ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error );
                            //serverError = true;//ここでreturnしても関数から抜け出せないのでフラグを用いる
                        }
                        //--------------------------------

                    }
                }, "データ取得中です...");
                progressDialog.ShowDialog();

                //--------------------------------
                //2013/12/17 追加 m.y 
                if (serverError == true)
                    return;

                if (result == null)
                {
                    #region エラーメッセージ表示
                    if (type == 帳票種別.船内収支報告書)
                    {
                        MessageBox.Show("船内収支報告書の出力に失敗しました\n(テンプレートファイルがありません。管理者に確認してください。)"
                            , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (type == 帳票種別.科目別集計表)
                    {
                        MessageBox.Show("科目別集計表の出力に失敗しました\n(テンプレートファイルがありません。管理者に確認してください。)"
                            , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (type == 帳票種別.船用金送金表)
                    {
                        MessageBox.Show("船用金送金表の出力に失敗しました\n(テンプレートファイルがありません。管理者に確認してください。)"
                            , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
