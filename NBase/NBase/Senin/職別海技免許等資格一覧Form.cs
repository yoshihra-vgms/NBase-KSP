using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseCommon;
using NBaseData.DAC;
using NBaseData.DS;
using NBaseUtil;

namespace Senin
{
    public partial class 職別海技免許等資格一覧Form : SeninSearchClientForm
    {
        private int selectedSeninId;


        public 職別海技免許等資格一覧Form()
        {
            InitializeComponent();
        }

        private void 職別海技免許等資格一覧Form_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void Init()
        {
            selectedSeninId = -1;

            InitComboBox職名(comboBox職名);
            textBox船員.Text = "";
        }

        private void InitComboBox職名(ComboBox combo)
        {
            combo.Items.Add(string.Empty);

            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                combo.Items.Add(s);
            }
        }

        private void button船員検索_Click(object sender, EventArgs e)
        {
            船員検索Form form = new 船員検索Form(this, false);
            form.ShowDialog();
        }

        private void button船員クリア_Click(object sender, EventArgs e)
        {
            selectedSeninId = -1;
            textBox船員.Text = "";
        }

        private void button出力_Click(object sender, EventArgs e)
        {
            int selectedShokumeiId = -1;
            if (comboBox職名.SelectedItem is MsSiShokumei)
            {
                selectedShokumeiId = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }

            saveFileDialog1.FileName = "職別海技免状等資格一覧.xlsx";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                byte[] result = null;

                bool serverError = false;

                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                    {

                        try
                        {

                            result = serviceClient.BLC_Excel_職別海技免許等資格一覧出力(NBaseCommon.Common.LoginUser, selectedShokumeiId, selectedSeninId);
 
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            serverError = true;
                        }
                    }
                }, "データ取得中です...");
                progressDialog.ShowDialog();

                //-----------------------
                //2013/12/17 追加 m.y 
                if (serverError == true)
                    return;

                if (result == null)
                {
                    MessageBox.Show("職別海技免状等資格一覧の出力に失敗しました\n(テンプレートファイルがありません。管理者に確認してください。)"
                            , "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button閉じる_Click(object sender, EventArgs e)
        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        #endregion

        /// <summary>
        /// 船員検索からコールされる船員検索の実処理
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        #region public override List<MsSenin> SearchMsSenin(MsSeninFilter filter)
        public override List<MsSenin> SearchMsSenin(MsSeninFilter filter)
        {
            List<MsSenin> result = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                if (comboBox職名.SelectedItem is MsSiShokumei)
                    filter.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;

                result = serviceClient.MsSenin_GetRecordsByFilter(NBaseCommon.Common.LoginUser, filter);
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 船員検索からコールされる船員選択の実処理
        /// </summary>
        /// <param name="senin"></param>
        #region public override bool SetMsSenin(MsSenin senin, bool check)
        public override bool SetMsSenin(MsSenin senin, bool check)
        {
            selectedSeninId = senin.MsSeninID;

            comboBox職名.SelectedItem = SeninTableCache.instance().GetMsSiShokumei(NBaseCommon.Common.LoginUser, senin.MsSiShokumeiID);
            textBox船員.Text = senin.Sei + " " + senin.Mei;

            return true;
        }
        #endregion



    }
}
