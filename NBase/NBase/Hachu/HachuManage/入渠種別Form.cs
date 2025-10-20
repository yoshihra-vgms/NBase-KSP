using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Hachu.Common;

namespace Hachu.HachuManage
{
    public partial class 入渠種別Form : Form
    {

        /// <summary>
        /// 選択されている手配依頼詳細種別
        /// </summary>
        public MsThiIraiShousai SelectedThiIraiShousai;

        private List<MsThiIraiShousai> 手配依頼種別詳細s = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public 入渠種別Form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 入渠種別Form_Load(object sender, EventArgs e)
        {
            string titleName = "ドックオーダー出力";
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", titleName, ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                手配依頼種別詳細s = serviceClient.MsThiIraiShousai_GetRecords(NBaseCommon.Common.LoginUser);
            }

            comboBox詳細種別.Items.Clear();
            foreach (MsThiIraiShousai tis in 手配依頼種別詳細s)
            {
                if (tis.MsThiIraiShousaiID != NBaseCommon.Common.MsThiIraiShousai_小修理ID)
                {
                    comboBox詳細種別.Items.Add(tis);
                }
            }

        }

        /// <summary>
        /// 「ＯＫ」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_OK_Click(object sender, EventArgs e)
        private void button_OK_Click(object sender, EventArgs e)
        {
            if (comboBox詳細種別.SelectedItem is MsThiIraiShousai)
            {
                SelectedThiIraiShousai = comboBox詳細種別.SelectedItem as MsThiIraiShousai;
            }
            else
            {
                MessageBox.Show("検査種類を選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion

        /// <summary>
        /// 「キャンセル」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button_Cancel_Click(object sender, EventArgs e)
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion
    }
}
