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

namespace Hachu.Reports
{
    public partial class データ出力Form : Form
    {
        string BaseFileName = "発注データ";

        /// <summary>
        /// 手配依頼詳細種別
        /// </summary>
        private List<MsThiIraiShousai> thiIraiShousais = null;





        public データ出力Form()
        {
            InitializeComponent();
        }

        private void データ出力Form_Load(object sender, EventArgs e)
        {
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "データ出力", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            検索条件初期化();
        }

        /// <summary>
        /// 検索条件を初期化する
        /// </summary>
        #region private void 検索条件初期化()
        private void 検索条件初期化()
        {
            List<MsVessel> vessels = null;
            List<MsCustomer> customers = null;
            List<MsThiIraiSbt> thiIraiSbts = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                vessels = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
                // 2010.06.28 削除済み顧客にも対応
                //customers = serviceClient.MsCustomer_GetRecords(NBaseCommon.Common.LoginUser);
                customers = serviceClient.MsCustomer_GetRecords削除を含む(NBaseCommon.Common.LoginUser);
                thiIraiSbts = serviceClient.MsThiIraiSbt_GetRecords(NBaseCommon.Common.LoginUser);
                thiIraiShousais = serviceClient.MsThiIraiShousai_GetRecords(NBaseCommon.Common.LoginUser);
            }

            // 船ＤＤＬ初期化
            comboBox船.Items.Clear();
            foreach (MsVessel v in vessels)
            {
                comboBox船.Items.Add(v);
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

            // 手配依頼日
            nullableDateTimePicker手配依頼日From.Value = null;
            nullableDateTimePicker手配依頼日To.Value = null;

            // 発注日
            nullableDateTimePicker発注日From.Value = null;
            nullableDateTimePicker発注日To.Value = null;

            // 受領日
            nullableDateTimePicker受領日From.Value = null;
            nullableDateTimePicker受領日To.Value = null;

            // 手配依頼番号
            textBox手配依頼番号From.Text = "";
            textBox手配依頼番号To.Text = "";

            // 現状
            checkBox未対応.Checked = true;
            checkBox見積中.Checked = true;
            checkBox発注済.Checked = true;
            checkBox受領済.Checked = true;
            checkBox完了.Checked = true;

            // ファイル
            radioButtonヘッダー.Checked = true;
            radioButton詳細品目.Checked = false;
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
            if (comboBox船.SelectedItem is MsVessel)
            {
                MsVessel vessel = comboBox船.SelectedItem as MsVessel;
                検索条件.MsVesselID = vessel.MsVesselID;
            }
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
            if (nullableDateTimePicker手配依頼日From.Value != null)
            {
                検索条件.ThiIraiDateFrom = nullableDateTimePicker手配依頼日From.Value as DateTime?;
            }
            if (nullableDateTimePicker手配依頼日To.Value != null)
            {
                検索条件.ThiIraiDateTo = nullableDateTimePicker手配依頼日To.Value as DateTime?;
            }
            if (nullableDateTimePicker発注日From.Value != null)
            {
                検索条件.HachuDateFrom = nullableDateTimePicker発注日From.Value as DateTime?;
            }
            if (nullableDateTimePicker発注日To.Value != null)
            {
                検索条件.HachuDateTo = nullableDateTimePicker発注日To.Value as DateTime?;
            }
            if (nullableDateTimePicker受領日From.Value != null)
            {
                検索条件.JryDateFrom = nullableDateTimePicker受領日From.Value as DateTime?;
            }
            if (nullableDateTimePicker受領日To.Value != null)
            {
                検索条件.JryDateTo = nullableDateTimePicker受領日To.Value as DateTime?;
            }
            検索条件.ThiIraiNoFrom = textBox手配依頼番号From.Text;
            検索条件.ThiIraiNoTo = textBox手配依頼番号To.Text;
            if (checkBox未対応.Checked)
            {
                検索条件.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_未対応);
            }
            if (checkBox見積中.Checked)
            {
                検索条件.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_見積中);
            }
            if (checkBox発注済.Checked)
            {
                検索条件.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_発注済);
            }
            if (checkBox受領済.Checked)
            {
                検索条件.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_受領済);
            }
            if (checkBox完了.Checked)
            {
                検索条件.MsThiIraiStatusIDs.Add(Hachu.Common.CommonDefine.MsThiIraiStatus_完了);
            }

            if (radioButtonヘッダー.Checked)
            {
                検索条件.type = 発注RowData検索条件.出力タイプEnum.ヘッダー;
            }
            else
            {
                検索条件.type = 発注RowData検索条件.出力タイプEnum.詳細品目;
            }
            #endregion

            if (検索条件.チェック() == false)
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
                    }, "発注データを取得中です...");
                    progressDialog.ShowDialog();
                    if (excelData == null)
                    {
                        MessageBox.Show("発注データの出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("発注データの出力に失敗しました。\n (Err:" + message + ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (検索条件.type == 発注RowData検索条件.出力タイプEnum.ヘッダー)
                {
                    excelData = serviceClient.BLC_発注RowDataヘッダー_取得(NBaseCommon.Common.LoginUser, 検索条件);
                }
                else
                {
                    excelData = serviceClient.BLC_発注RowData詳細品目_取得(NBaseCommon.Common.LoginUser, 検索条件);
                }
            }
            return excelData;
        }
        #endregion
    }
}
