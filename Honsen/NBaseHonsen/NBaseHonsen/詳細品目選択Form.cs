using System;
using System.Collections.Generic;
using System.Collections;
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
    public partial class 詳細品目選択Form : Form
    {
        /// <summary>
        /// 選択されている船用品船
        /// </summary>
        public MsVesselItemVessel SelectedVesselItemVessel;

        private int vesselItemCategoryNumber;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public 詳細品目選択Form(int vesselItemCategoryNumber)
        {
            this.vesselItemCategoryNumber = vesselItemCategoryNumber;
            
            InitializeComponent();
            Text = NBaseCommon.Common.WindowTitle("", "詳細品目選択", WcfServiceWrapper.ConnectedServerID);
        }

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 詳細品目検索Form_Load(object sender, EventArgs e)
        {
            SelectedVesselItemVessel = null;
            textBox検索文字.Text = "";
            listBox詳細品目.Items.Clear();
        }

        /// <summary>
        /// 「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button検索_Click(object sender, EventArgs e)
        {
            string 検索文字列 = textBox検索文字.Text;

            List<MsVesselItemVessel> MsVesselItemVessels = null;
            //MsVesselItemVessels = MsVesselItemVessel.GetRecordsByMsVesselIDVesselItemName(NBaseCommon.Common.LoginUser, 同期Client.LOGIN_VESSEL.MsVesselID, vesselItemCategoryNumber, 検索文字列);
            MsVesselItemVessels = MsVesselItemVessel.GetRecordsByMsVesselIDVesselItemName(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID, vesselItemCategoryNumber, 検索文字列);
            
            listBox詳細品目.Items.Clear();
            foreach (MsVesselItemVessel vesselItemVessel in MsVesselItemVessels)
            {
                listBox詳細品目.Items.Add(vesselItemVessel);
            }
        }

        /// <summary>
        /// 「選択」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button選択_Click(object sender, EventArgs e)
        {
            if (listBox詳細品目.SelectedItem is MsVesselItemVessel)
            {
                SelectedVesselItemVessel = listBox詳細品目.SelectedItem as MsVesselItemVessel;
            }
            else
            {
                MessageBox.Show("詳細品目を選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 「キャンセル」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
