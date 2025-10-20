using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using SyncClient;

namespace NBaseHonsen
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
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", titleName, WcfServiceWrapper.ConnectedServerID);

            手配依頼種別詳細s = MsThiIraiShousai.GetRecords(同期Client.LOGIN_USER);

            comboBox詳細種別.Items.Clear();
            foreach (MsThiIraiShousai tis in 手配依頼種別詳細s)
            {
                if (tis.MsThiIraiShousaiID != MsThiIraiShousai.ToId(MsThiIraiShousai.ThiIraiShousaiEnum.部品購入))
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
                MessageBox.Show("検査種類を選択してください。", "エラー");
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
