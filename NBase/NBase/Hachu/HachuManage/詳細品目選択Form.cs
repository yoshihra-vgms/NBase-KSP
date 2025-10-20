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

namespace Hachu.HachuManage
{
    public partial class 詳細品目選択Form : Form
    {
        // 船ID
        private int MsVesselID;

        // 船用品カテゴリ
        private int MsVesselItemCategoryNumber;
        
        /// <summary>
        /// 選択されている船用品船
        /// </summary>
        public MsVesselItemVessel SelectedVesselItemVessel;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public 詳細品目選択Form(int vesselID, int vesselItemCategoryNumber)
        {
            MsVesselID = vesselID;
            MsVesselItemCategoryNumber = vesselItemCategoryNumber;

            InitializeComponent();
        }

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 詳細品目検索Form_Load(object sender, EventArgs e)
        {
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "詳細品目検索", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

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
            // 2009.09.12 空文字でも検索したい・・・　( by SUZUKI )
            //if (検索文字列.Length == 0)
            //{
            //    return;
            //}

            List<MsVesselItemVessel> MsVesselItemVessels = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsVesselItemVessels = serviceClient.MsVesselItemVessel_GetRecordByMsVesselIDVesselItemName(NBaseCommon.Common.LoginUser, MsVesselID, MsVesselItemCategoryNumber, 検索文字列);
            }

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

        /// <summary>
        /// 「カテゴリ」ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox詳細品目_DoubleClick(object sender, EventArgs e)
        {
            if (listBox詳細品目.SelectedItem is MsVesselItemVessel)
            {
                SelectedVesselItemVessel = listBox詳細品目.SelectedItem as MsVesselItemVessel;

                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
