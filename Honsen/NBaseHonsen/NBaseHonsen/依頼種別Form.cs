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
    public partial class 依頼種別Form : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private 手配依頼一覧Form.SHOW_WINDOW windowType;

        /// <summary>
        /// 選択されている手配依頼種別
        /// </summary>
        public MsThiIraiSbt SelectedSyubetsu;

        /// <summary>
        /// 選択されている手配依頼詳細種別
        /// </summary>
        public MsThiIraiShousai SelectedSyousaiSyubetsu;

        /// <summary>
        /// 選択されている見積もり有無
        /// </summary>
        public int SelectedMitsumoriUmu;


        public int SelectedCategoryNumber = -1;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public 依頼種別Form(手配依頼一覧Form.SHOW_WINDOW ShowWindow)
        {
            windowType = ShowWindow;
            SelectedSyubetsu = null;
            SelectedSyousaiSyubetsu = null;
            InitializeComponent();
            Text = NBaseCommon.Common.WindowTitle("", "手配依頼種別", WcfServiceWrapper.ConnectedServerID);
        }

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 手配依頼種別Form_Load(object sender, EventArgs e)
        {
            // Windowタイトル、メッセージ
            if (windowType == 手配依頼一覧Form.SHOW_WINDOW.手配依頼)
            {
                label_Message.Text = "手配依頼を作成します。種別を選択してください。";

                panel_詳細種別_修繕.Visible = false;
            }
            else
            {
                label_Message.Text = "見積依頼を作成します。種別を選択してください。";

                panel_詳細種別_修繕.Visible = false;
            }

            // 手配依頼種別ComboBox初期化
            List<MsThiIraiSbt> iraiSyubetsu = MsThiIraiSbt.GetRecords(同期Client.LOGIN_USER);
            comboBox種別.Items.Clear();
            MsThiIraiSbt selectedTehaiIraiSyubetsu = null;

            foreach (MsThiIraiSbt tis in iraiSyubetsu)
            {
                if (selectedTehaiIraiSyubetsu == null)
                {
                    selectedTehaiIraiSyubetsu = tis;
                }
                comboBox種別.Items.Add(tis);
            }
            
            comboBox種別.SelectedIndex = 0;

            // 手配依頼詳細種別ComboBox初期化
            List<MsThiIraiShousai> iraiShousai = MsThiIraiShousai.GetRecords(同期Client.LOGIN_USER);
            comboBox詳細種別.Items.Clear();
            foreach (MsThiIraiShousai tis in iraiShousai)
            {
                if (tis.MsThiIraiShousaiID == MsThiIraiShousai.ToId(MsThiIraiShousai.ThiIraiShousaiEnum.荷役資材) && 同期Client.LOGIN_VESSEL.NiyakuEnabled != 1)
                    continue;

                comboBox詳細種別.Items.Add(tis);
            }
            comboBox詳細種別.SelectedIndex = 0;

            // 船用品カテゴリComboBox初期化
            comboBoxカテゴリ.Items.Clear();
            comboBoxカテゴリ.Items.Add("ペイント");
            comboBoxカテゴリ.Items.Add("ペイント以外");
            comboBoxカテゴリ.SelectedIndex = 0;


            comboBox_Syubetsu_SelectedIndexChanged(selectedTehaiIraiSyubetsu);
        }

        /// <summary>
        /// 「ＯＫ」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_OK_Click(object sender, EventArgs e)
        {
            SelectedSyubetsu = null;
            SelectedSyousaiSyubetsu = null;
            SelectedMitsumoriUmu = -1;

            // 選択値の確認
            if (comboBox種別.SelectedItem is MsThiIraiSbt)
            {
                SelectedSyubetsu = comboBox種別.SelectedItem as MsThiIraiSbt;
            }
            
            if (SelectedSyubetsu == null)
            {
                MessageBox.Show("種別を選択してください。", "エラー");
                return;
            }

            if (SelectedSyubetsu.MsThiIraiSbtID.Equals("1"))
            {
                SelectedSyousaiSyubetsu = comboBox詳細種別.SelectedItem as MsThiIraiShousai;
            }
            //else if (SelectedSyubetsu.MsThiIraiSbtID.Equals("3"))
            //{
            //    if (comboBoxカテゴリ.SelectedIndex == 0)
            //    {
            //        SelectedCategoryNumber = MsVesselItemCategory.ToNumber(MsVesselItemCategory.MsVesselItemCategoryEnum.ペイント);
            //    }
            //}
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 「キャンセル」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 手配依頼種別ComboBoxを選択したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Syubetsu_SelectedIndexChanged(object sender, EventArgs e)
        {
            MsThiIraiSbt SelectedSyubetsu = comboBox種別.SelectedItem as MsThiIraiSbt;

            comboBox_Syubetsu_SelectedIndexChanged(SelectedSyubetsu);
        }

        private void comboBox_Syubetsu_SelectedIndexChanged(MsThiIraiSbt SelectedSyubetsu)
        {
            if (SelectedSyubetsu.MsThiIraiSbtID == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.修繕))
            {
                // 修繕費
                panel_詳細種別_修繕.Location = new Point(53, 128);
                panel_詳細種別_修繕.Visible = true;
                panel_詳細種別_船用品.Visible = false;
            }
            else if (SelectedSyubetsu.MsThiIraiSbtID == MsThiIraiSbt.ToId(MsThiIraiSbt.ThiIraiSbtEnum.燃料_潤滑油))
            {
                // 潤滑油
                panel_詳細種別_修繕.Visible = false;
                panel_詳細種別_船用品.Visible = false;
            }
            else
            {
                // 船用品
                panel_詳細種別_修繕.Visible = false;
                //panel_詳細種別_船用品.Location = new Point(53, 128);
                //panel_詳細種別_船用品.Visible = true;
                panel_詳細種別_船用品.Visible = false;
            }

        }
    }
}
