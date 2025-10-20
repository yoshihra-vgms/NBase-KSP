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

namespace Hachu
{
    public partial class KBAS年月指定出力Form : Form
    {
        public enum 帳票種別 { KBAS出力 }
        private 帳票種別 type;

        private List<MsVessel> vessels = null;

        public KBAS年月指定出力Form(帳票種別 type)
        {
            this.type = type;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                List<MsVessel> wklist = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
                vessels = new List<MsVessel>();//m.yoshihara 2017/05/16 miho
                foreach (MsVessel v in wklist)
                {
                    if (v.HachuEnabled == 1)
                    {
                        vessels.Add(v);
                    }
                }
            }

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox年();
            InitComboBox月();

            if (type == 帳票種別.KBAS出力)
            {
                Text = "K-BAS出力";
            }

            comboBox船.Items.Clear();
            foreach (MsVessel v in vessels)
            {
                comboBox船.Items.Add(v);
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
            //年月取得
            DateTime date = new DateTime((int)comboBox年.SelectedItem, Int32.Parse(comboBox月.SelectedItem as string), 1);

            //船取得
            MsVessel vsl = null;
            if (comboBox船.SelectedItem is MsVessel)
            {
                vsl = comboBox船.SelectedItem as MsVessel;
            }

            saveFileDialog1.FileName = Text + "_" + date.ToString("yyyyMM") + ".xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                byte[] result = null;
                
                //サーバーエラー時の時のフラグ
                bool serverError = false;

                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {
                        try
                        {
                            if (type == 帳票種別.KBAS出力)
                            {
                                result = serviceClient.BLC_KBASデータ出力_取得(NBaseCommon.Common.LoginUser, date, vsl);
                            }
                            else
                            {
                                result = null;
                            }
                        }
                        catch( Exception ex )
                        {
                            ;
                        }

                    }
                }, "データ取得中です...");
                progressDialog.ShowDialog();

                if (serverError == true)
                    return;

                if (result == null)
                {
                    #region エラーメッセージ表示
                    if (type == 帳票種別.KBAS出力)
                    {
                        MessageBox.Show("K=BAS出力に失敗しました\n(テンプレートファイルがありません。管理者に確認してください。)"
                            , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion
                    return;
                }

                System.IO.FileStream filest = new System.IO.FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
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
