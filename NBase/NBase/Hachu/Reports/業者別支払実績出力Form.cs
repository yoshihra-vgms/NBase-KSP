using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseData.BLC;
using NBaseUtil;

namespace Hachu.Reports
{
    public partial class 業者別支払実績出力Form : Form
    {
        string BaseFileName = "業者別支払実績";

        /// <summary>
        /// 手配依頼詳細種別
        /// </summary>
        private List<MsThiIraiShousai> thiIraiShousais = null;





        public 業者別支払実績出力Form()
        {
            InitializeComponent();
        }

        private void 業者別支払実績出力Form_Load(object sender, EventArgs e)
        {
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "業者別支払実績出力", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            検索条件初期化();
        }

        /// <summary>
        /// 検索条件を初期化する
        /// </summary>
        #region private void 検索条件初期化()
        private void 検索条件初期化()
        {
            List<MsCustomer> customers = null;
            List<MsThiIraiSbt> thiIraiSbts = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                customers = serviceClient.MsCustomer_GetRecords削除を含む(NBaseCommon.Common.LoginUser);
                thiIraiSbts = serviceClient.MsThiIraiSbt_GetRecords(NBaseCommon.Common.LoginUser);
                thiIraiShousais = serviceClient.MsThiIraiShousai_GetRecords(NBaseCommon.Common.LoginUser);
            }


            // 取引先ＤＤＬ初期化
            comboBox取引先.Items.Clear();
            foreach (MsCustomer c in customers)
            {
                comboBox取引先.Items.Add(c);
                comboBox取引先.AutoCompleteCustomSource.Add(c.CustomerName);
            }

            // 手配依頼種別
            MsThiIraiSbt dmyThiIraiSbt = new MsThiIraiSbt();
            dmyThiIraiSbt.MsThiIraiSbtID = null;
            dmyThiIraiSbt.ThiIraiSbtName = "";
            comboBox種別.Items.Clear();
            comboBox種別.Items.Add(dmyThiIraiSbt);
            foreach (MsThiIraiSbt sbt in thiIraiSbts)
            {
                comboBox種別.Items.Add(sbt);
            }
            comboBox種別.SelectedIndex = 0;

            // 手配依頼詳細種別
            MsThiIraiShousai dmyThiIraiShousai = new MsThiIraiShousai();
            dmyThiIraiShousai.MsThiIraiShousaiID = null;
            dmyThiIraiShousai.ThiIraiShousaiName = "";
            comboBox詳細種別.Items.Clear();
            comboBox詳細種別.Items.Add(dmyThiIraiShousai);
            comboBox詳細種別.SelectedIndex = 0;

            // 期間
            comboBox期間.SelectedIndex = 2;
            nullableDateTimePicker期間From.Value = DateTimeUtils.年度開始日();
            nullableDateTimePicker期間To.Value = DateTimeUtils.年度終了日();

        }
        #endregion

        /// <summary>
        /// 「手配依頼種別」ＤＤＬが選択された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void comboBox種別_SelectedIndexChanged(object sender, EventArgs e)
        private void comboBox種別_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 詳細種別ＤＤＬをクリア
            comboBox詳細種別.Items.Clear();

            // 選択された種別が「修繕」の場合、詳細種別ＤＤＬを再構築
            MsThiIraiSbt selected = comboBox種別.SelectedItem as MsThiIraiSbt;
            if (selected.MsThiIraiSbtID == NBaseCommon.Common.MsThiIraiSbt_修繕ID)
            {
                MsThiIraiShousai dmyThiIraiShousai = new MsThiIraiShousai();
                dmyThiIraiShousai.MsThiIraiShousaiID = null;
                dmyThiIraiShousai.ThiIraiShousaiName = "";
                comboBox詳細種別.Items.Add(dmyThiIraiShousai);

                foreach (MsThiIraiShousai shousai in thiIraiShousais)
                {
                    comboBox詳細種別.Items.Add(shousai);
                }
                comboBox詳細種別.SelectedIndex = 0;
            }
        }
        #endregion

        /// <summary>
        /// 「出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button出力_Click(object sender, EventArgs e)
        private void button出力_Click(object sender, EventArgs e)
        {
            発注RowData検索条件 検索条件 = new 発注RowData検索条件();

            #region 検索条件を検索条件クラスにセット

            if (comboBox取引先.SelectedItem is MsCustomer)
            {
                MsCustomer customer = comboBox取引先.SelectedItem as MsCustomer;
                検索条件.MsCustomerID = customer.MsCustomerID;
            }
            if (comboBox種別.SelectedItem is MsThiIraiSbt)
            {
                MsThiIraiSbt thiIraiSbt = comboBox種別.SelectedItem as MsThiIraiSbt;
                検索条件.MsThiIraiSbtID = thiIraiSbt.MsThiIraiSbtID;
            }
            if (comboBox詳細種別.SelectedItem is MsThiIraiShousai)
            {
                MsThiIraiShousai thiIraiShousai = comboBox詳細種別.SelectedItem as MsThiIraiShousai;
                検索条件.MsThiIraiShousaiID = thiIraiShousai.MsThiIraiShousaiID;
            }


            string txt = comboBox期間.SelectedItem as string;
            if (txt == "発注日")
            {
                if (nullableDateTimePicker期間From.Value != null)
                {
                    検索条件.HachuDateFrom = nullableDateTimePicker期間From.Value as DateTime?;
                }
                if (nullableDateTimePicker期間To.Value != null)
                {
                    検索条件.HachuDateTo = nullableDateTimePicker期間To.Value as DateTime?;
                }
            }
            else if (txt == "受領日")
            {
                if (nullableDateTimePicker期間From.Value != null)
                {
                    検索条件.JryDateFrom = nullableDateTimePicker期間From.Value as DateTime?;
                }
                if (nullableDateTimePicker期間To.Value != null)
                {
                    検索条件.JryDateTo = nullableDateTimePicker期間To.Value as DateTime?;
                }
            }
            else if (txt == "支払日")
            {
                if (nullableDateTimePicker期間From.Value != null)
                {
                    検索条件.ShrDateFrom = nullableDateTimePicker期間From.Value as DateTime?;
                }
                if (nullableDateTimePicker期間To.Value != null)
                {
                    検索条件.ShrDateTo = nullableDateTimePicker期間To.Value as DateTime?;
                }
            }
            #endregion

            if (検索条件.業者別支払実績出力時チェック() == false)
            {
                MessageBox.Show(検索条件.ErrMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Output(検索条件);

            Close();
        }
        #endregion

        /// <summary>
        /// 出力処理
        /// </summary>
        /// <param name="検索条件"></param>
        #region public void Output(発注RowData検索条件 検索条件)
        public void Output(発注RowData検索条件 検索条件)
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
                        excelData = MakeFile(検索条件);
                    }, "業者別支払実績を取得中です...");
                    progressDialog.ShowDialog();
                    if (excelData == null)
                    {
                        MessageBox.Show("業者別支払実績の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("業者別支払実績の出力に失敗しました。\n (Err:" + message + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        /// <summary>
        /// サーバに出力依頼、出力結果を取得する
        /// </summary>
        /// <param name="検索条件"></param>
        /// <returns></returns>
        #region private byte[] MakeFile(発注RowData検索条件 検索条件)
        private byte[] MakeFile(発注RowData検索条件 検索条件)
        {
            byte[] excelData = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {

                excelData = serviceClient.BLC_業者別支払実績帳票_取得(NBaseCommon.Common.LoginUser, 検索条件);

            }
            return excelData;
        }
        #endregion
    }
}
