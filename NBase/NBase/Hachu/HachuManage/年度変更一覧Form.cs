using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using Hachu.Controllers;
using Hachu.Models;
using NBaseData.DAC;
using NBaseUtil;

namespace Hachu.HachuManage
{
    public partial class 年度変更一覧Form : Form
    {
        /// <summary>
        /// インスタンスを格納するフィールド変数
        /// </summary>
        private static 年度変更一覧Form instance = null;

        /// <summary>
        /// 手配依頼詳細種別
        /// </summary>
        private List<MsThiIraiShousai> thiIraiShousais = null;

        /// <summary>
        /// 年度変更一覧情報リスト
        /// </summary>
        private 年度変更一覧情報Controller 年度変更一覧情報 = null;

        /// <summary>
        /// インスタンスを取得するためのメソッド
        /// Windows フォームをシングルトン対応にするコード
        /// </summary>
        /// <returns></returns>
        public static 年度変更一覧Form GetInstance()
        {
            if (instance == null)
            {
                instance = new 年度変更一覧Form();
            }
            return instance;
        }
        
        /// <summary>
        /// コンストラクタ
        /// Windows フォームをシングルトン対応にするため private
        /// </summary>
        private 年度変更一覧Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("", "年度変更", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
        }


        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 年度変更一覧Form_Load(object sender, EventArgs e)
        private void 年度変更一覧Form_Load(object sender, EventArgs e)
        {
            //==================================
            // 初期化
            //==================================

            this.Location = new Point(0, 0);

            // Form幅を親Formの幅にする
            this.Width = Parent.ClientSize.Width;
            this.Height = Parent.ClientSize.Height;

            検索条件初期化();

            年度変更一覧情報 = new 年度変更一覧情報Controller(this.MdiParent, treeListView年度変更一覧);
            年度変更一覧情報.初期化(this.Width + 100);

            this.WindowState = FormWindowState.Maximized;
        }
        #endregion

        /// <summary>
        /// フォームを閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 年度変更一覧Form_FormClosing(object sender, FormClosingEventArgs e)
        private void 年度変更一覧Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            年度変更一覧情報.終了();
            instance = null;
        }
        #endregion

        /// <summary>
        /// 検索条件を初期化する
        /// </summary>
        #region private void 検索条件初期化()
        private void 検索条件初期化()
        {
            List<MsVessel> vessels = null;
            List<MsThiIraiSbt> thiIraiSbts = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                vessels = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
                thiIraiSbts = serviceClient.MsThiIraiSbt_GetRecords(NBaseCommon.Common.LoginUser);
                thiIraiShousais = serviceClient.MsThiIraiShousai_GetRecords(NBaseCommon.Common.LoginUser);
            }

            // 船ComboBox初期化
            MsVessel dmyVessel = new MsVessel();
            dmyVessel.MsVesselID = -1;
            dmyVessel.VesselName = "";
            comboBox船.Items.Clear();
            comboBox船.Items.Add(dmyVessel);
            foreach (MsVessel v in vessels)
            {
                comboBox船.Items.Add(v);
            }


            // 手配依頼種別
            MsThiIraiSbt dmyThiIraiSbt = new MsThiIraiSbt();
            dmyThiIraiSbt.MsThiIraiSbtID = "";
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
            dmyThiIraiShousai.MsThiIraiShousaiID = "";
            dmyThiIraiShousai.ThiIraiShousaiName = "";
            comboBox詳細種別.Items.Clear();
            comboBox詳細種別.Items.Add(dmyThiIraiShousai);
            comboBox詳細種別.SelectedIndex = 0;

            // 年ComboBox初期化
            int thisYear = NBaseUtil.DateTimeUtils.年度開始日().Year;
            comboBox年度.Items.Clear();
            for (int y = thisYear; y >= (thisYear - 2); y--)
            {
                Hachu.Reports.集計表Form.YearValue yearValue = new Hachu.Reports.集計表Form.YearValue();
                yearValue.Year = y;
                comboBox年度.Items.Add(yearValue);

            }
            comboBox年度.SelectedIndex = 0;

        }
        #endregion

        /// <summary>
        /// 「検索」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button検索_Click(object sender, EventArgs e)
        private void button検索_Click(object sender, EventArgs e)
        {
            検索();
        }
        #endregion

        #region private void 検索()
        private void 検索()
        {
            this.Cursor = Cursors.WaitCursor;

            // 検索条件
            発注一覧検索条件 検索条件 = new 発注一覧検索条件();
            if (comboBox船.SelectedItem is MsVessel)
                検索条件.Vessel = comboBox船.SelectedItem as MsVessel;
            if (comboBox種別.SelectedItem is MsThiIraiSbt)
                検索条件.ThiIraiSbt = comboBox種別.SelectedItem as MsThiIraiSbt;
            if (comboBox詳細種別.SelectedItem is MsThiIraiShousai)
                検索条件.ThiIraiShousai = comboBox詳細種別.SelectedItem as MsThiIraiShousai;

            int year = (comboBox年度.SelectedItem as Hachu.Reports.集計表Form.YearValue).Year;
            検索条件.nendo = year.ToString() ;


            // 検索条件で、ＤＢを検索し、一覧に表示する
            年度変更一覧情報.一覧更新(検索条件);

            this.Cursor = Cursors.Default;

            if (年度変更一覧情報.検索結果数 == 0)
            {
                MessageBox.Show("対象データがありません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        /// <summary>
        /// 「条件クリア」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button条件クリア_Click(object sender, EventArgs e)
        private void button条件クリア_Click(object sender, EventArgs e)
        {
            // 船
            comboBox船.SelectedIndex = 0;

            // 手配依頼種別
            comboBox種別.SelectedIndex = 0;

            // 手配依頼詳細種別
            comboBox詳細種別.Items.Clear();

            // 発注日
            comboBox年度.SelectedIndex = 0;
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
                dmyThiIraiShousai.MsThiIraiShousaiID = "";
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



        #region rivate void treeListView発注一覧_AfterExpand(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        private void treeListView発注一覧_AfterExpand(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            try
            {
                // Expand された Node
                TreeListViewNode node = e.Object as TreeListViewNode;
            }
            catch
            {
            }
        }
        #endregion


        private void button年度変更_Click(object sender, EventArgs e)
        {
            List<OdMk> odmkList = 年度変更一覧情報.GetCheckedOdMk();

            if (odmkList.Count() == 0)
            {
                MessageBox.Show("年度変更対象のデータがありません。");
                return;
            }

            if (MessageBox.Show("選択されている発注データの年度を変更します。よろしいですか？") == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }


            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                {
                    foreach (OdMk odmk in odmkList)
                    {
                        int nendo = DateTimeUtils.年度開始日(odmk.HachuDate).AddYears(1).Year;
                        System.Diagnostics.Debug.WriteLine("No:" + odmk.HachuNo + ", HachuDate:" + odmk.HachuDate + " => Nendo:" + nendo);

                        odmk.Nendo = nendo.ToString();

                        bool ret = serviceClient.OdMk_Update(NBaseCommon.Common.LoginUser, odmk);
                    }

                }, "処理中です...");
                progressDialog.ShowDialog();
            }

            検索();
            MessageBox.Show("年度を変更しました");
        }

    }
}
