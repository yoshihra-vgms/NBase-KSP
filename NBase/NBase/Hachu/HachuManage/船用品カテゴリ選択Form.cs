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
    public partial class 船用品カテゴリ選択Form : Form
    {
        /// <summary>
        /// 現在登録されている詳細品目数
        /// </summary>
        public int ShousaiCount = 0;

        /// <summary>
        /// 選択されている船用品カテゴリ
        /// </summary>
        public MsVesselItemCategory SelectedVesselItemCategory;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public 船用品カテゴリ選択Form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 船用品カテゴリ選択Form_Load(object sender, EventArgs e)
        {
            List<MsVesselItemCategory> MsVesselItemCategorys = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                MsVesselItemCategorys = serviceClient.MsVesselItemCategory_GetRecords(NBaseCommon.Common.LoginUser);
            }

            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "仕様・型式選択", ServiceReferences.WcfServiceWrapper.ConnectedServerID);

            SelectedVesselItemCategory = null;
            listBox船用品カテゴリ.Items.Clear();
            foreach (MsVesselItemCategory vic in MsVesselItemCategorys)
            {
                listBox船用品カテゴリ.Items.Add(vic);
            }
        }

        /// <summary>
        /// 「選択」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button選択_Click(object sender, EventArgs e)
        {
            if (listBox船用品カテゴリ.SelectedItem is MsVesselItemCategory)
            {
                if (ShousaiCount > 1)
                {
                    if (MessageBox.Show("仕様・型式を変更するには、詳細品目を削除してよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                SelectedVesselItemCategory = listBox船用品カテゴリ.SelectedItem as MsVesselItemCategory;
            }
            else
            {
                MessageBox.Show("品目を選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void listBox船用品カテゴリ_DoubleClick(object sender, EventArgs e)
        {
            if (listBox船用品カテゴリ.SelectedItem is MsVesselItemCategory)
            {
                if (ShousaiCount > 1)
                {
                    if (MessageBox.Show("仕様・型式を変更するには、詳細品目を削除してよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                SelectedVesselItemCategory = listBox船用品カテゴリ.SelectedItem as MsVesselItemCategory;
                
                DialogResult = DialogResult.OK;
                Close();
            }
        }

    }
}
