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
using System.Configuration;

namespace Senin
{
    public partial class 労務管理記録簿出力Form : Form
    {

        public 労務管理記録簿出力Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            dateTimePicker1.Value = DateTimeUtils.年度開始日(DateTime.Today);
            dateTimePicker2.Value = DateTimeUtils.年度終了日(DateTime.Today);

            InitComboBox船員();

            panel_期間.Visible = true;
        }



        /// <summary>
        ///  所属会社選択されたとき、船員リストを書き換える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitComboBox船員()
        {
            comboBox船員.Items.Clear();

            List<MsSenin> seninlist = new List<MsSenin>();

            //船員の検索フィルター
            MsSeninFilter filter = new MsSeninFilter();
            filter.船員テーブルのみ対象 = true;
            filter.joinSiCard = MsSeninFilter.JoinSiCard.NOT_JOIN;
            filter.OrderByStr = "OrderByMsSiShokumeiIdSeiMei";
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                seninlist = serviceClient.MsSenin_GetRecordsByFilter(NBaseCommon.Common.LoginUser, filter);
            }

            //comboboxに表示
            //comboBox船員.Items.Add("全員");
            foreach (MsSenin senin in seninlist)
            {
                comboBox船員.Items.Add(senin);
            }
            if (comboBox船員.Items.Count > 0) comboBox船員.SelectedIndex = 0;
        }



        private void button出力_Click(object sender, EventArgs e)
        {
            #region 画面から値取得

            //船員取得
            int seninId = 0;
            if (comboBox船員.SelectedItem is MsSenin)
            {
                seninId = (comboBox船員.SelectedItem as MsSenin).MsSeninID;
            }

            //日付取得
            DateTime fromDate = DateTime.MinValue;
            DateTime toDate = DateTime.MinValue;
            if (panel_期間.Visible)
            {
                fromDate = dateTimePicker1.Value;
                toDate = dateTimePicker2.Value;

                DateTime maxDate = fromDate.AddYears(1); // 最長は開始日から１年

                if (toDate > maxDate)
                {
                    MessageBox.Show("期間は、最長１年で指定してください");
                    return;               
                }
            }

            #endregion

            saveFileDialog1.FileName = "労務管理記録簿.xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                byte[] result = null;
                
                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {

                        System.Diagnostics.Debug.WriteLine($"Start:{DateTime.Now.ToShortTimeString()}");

                        try
                        {
                            var key  = ConfigurationManager.AppSettings["ConnectionKey"];

                            result = serviceClient.BLC_Excel_労務管理記録簿出力(key, NBaseCommon.Common.LoginUser, 0, seninId, fromDate, toDate);
                        }
                        catch( Exception ex )
                        {
                            System.Diagnostics.Debug.WriteLine($"Exception:{DateTime.Now.ToShortTimeString()}");
                            System.Diagnostics.Debug.WriteLine($"Exception:{ex.Message}");
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
                   
                    MessageBox.Show("労務管理記録簿の出力に失敗しました\n(テンプレートファイルがありません。管理者に確認してください。)"
                        , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    #endregion
                    return;
                }
                //--------------------------------
                System.Diagnostics.Debug.WriteLine($"Finish:{DateTime.Now.ToShortTimeString()}");

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
