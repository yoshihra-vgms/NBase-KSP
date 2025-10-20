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
    public partial class 船用品カテゴリ選択Form : Form
    {
        private 手配品目Form tehaiHinmokuForm;
        private int shousaiCount;
            
        /// <summary>
        /// 選択されている船用品カテゴリ
        /// </summary>
        public MsVesselItemCategory SelectedVesselItemCategory;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public 船用品カテゴリ選択Form(手配品目Form tehaiHinmokuForm)
        {
            this.tehaiHinmokuForm = tehaiHinmokuForm;
            
            InitializeComponent();
        }
        public 船用品カテゴリ選択Form(手配品目Form tehaiHinmokuForm, int shousaiCount)
        {
            this.tehaiHinmokuForm = tehaiHinmokuForm;
            this.shousaiCount = shousaiCount;

            InitializeComponent();
        }

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 船用品カテゴリ選択Form_Load(object sender, EventArgs e)
        {
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "品目選択", WcfServiceWrapper.ConnectedServerID);

            SelectedVesselItemCategory = null;
            listBox船用品カテゴリ.Items.Clear();
            List<MsVesselItemCategory> MsVesselItemCategorys = MsVesselItemCategory.GetRecords();
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
                if (this.shousaiCount > 1)
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
                MessageBox.Show("品目を選択してください。", "エラー");
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
