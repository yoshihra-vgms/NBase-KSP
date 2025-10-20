using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Hachu.Utils;
using NBaseData.DAC;

namespace Hachu.HachuManage
{
    public partial class 支払合算Form : Form
    {
        /// <summary>
        /// 手配依頼詳細種別
        /// </summary>
        private List<MsThiIraiShousai> thiIraiShousais = null;

        /// <summary>
        /// 
        /// </summary>
        private ItemTreeListView支払合算一覧 TreeList = null;

        /// <summary>
        /// インスタンスを格納するフィールド変数
        /// </summary>
        private static 支払合算Form instance = null;

        /// <summary>
        /// インスタンスを取得するためのメソッド
        /// Windows フォームをシングルトン対応にするコード
        /// </summary>
        /// <returns></returns>
        #region public static 支払合算Form GetInstance()
        public static 支払合算Form GetInstance()
        {
            if (instance == null)
            {
                instance = new 支払合算Form();
            }
            return instance;
        }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// Windows フォームをシングルトン対応にするため private
        /// </summary>
        #region private 支払合算Form()
        private 支払合算Form()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "支払合算一覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
        }
        #endregion

        /// <summary>
        /// フォームを閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 支払合算Form_FormClosing(object sender, FormClosingEventArgs e)
        private void 支払合算Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            instance = null;
        }
        #endregion

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 支払合算Form_Load(object sender, EventArgs e)
        {
            検索条件初期化();
            一覧初期化();
        }

        /// <summary>
        /// 検索条件を初期化する
        /// </summary>
        #region private void 検索条件初期化()
        private void 検索条件初期化()
        {
            List<MsCustomer> customers = null;
            List<MsThiIraiSbt> thiIraiSbts = null;
            List<MsVessel> vessels = null;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                // 2010.06.28 削除済み顧客にも対応
                //customers = serviceClient.MsCustomer_GetRecords(NBaseCommon.Common.LoginUser);
                customers = serviceClient.MsCustomer_GetRecords削除を含む(NBaseCommon.Common.LoginUser);
                thiIraiSbts = serviceClient.MsThiIraiSbt_GetRecords(NBaseCommon.Common.LoginUser);
                thiIraiShousais = serviceClient.MsThiIraiShousai_GetRecords(NBaseCommon.Common.LoginUser);
                vessels = serviceClient.MsVessel_GetRecordsByHachuEnabled(NBaseCommon.Common.LoginUser);
            }

            // 取引先
            comboBox取引先.Items.Clear();
            foreach (MsCustomer c in customers)
            {
                // 2013.08.07 : 取引先のみセットする
                //comboBox取引先.Items.Add(c);
                //comboBox取引先.AutoCompleteCustomSource.Add(c.CustomerName);
                //if (c.Shubetsu == (int)MsCustomer.種別.取引先)
                if (c.Is取引先())
                {
                    comboBox取引先.Items.Add(c);
                    comboBox取引先.AutoCompleteCustomSource.Add(c.CustomerName);
                }
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

            // 状態（支払済）
            checkBox支払済.Checked = false;
        }
        #endregion

        /// <summary>
        /// 一覧（TreeListView）を初期化する
        /// </summary>
        #region private void 一覧初期化()
        private void 一覧初期化()
        {
            object[,] columns = new object[,] {
                                           {"状況", 75, null, null},
                                           {"種別", 95, null, null},
                                           {"代表発注番号", 100, null, null},
                                           {"業者名", 200, null, null},
                                           {"金額", 90, null, HorizontalAlignment.Right},
                                       　　{"備考", 200, null, null}
                                         };
            TreeList = new ItemTreeListView支払合算一覧(treeListView支払合算一覧);
            TreeList.SetColumns(-2, columns);

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

        /// <summary>
        /// 「条件クリア」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button条件クリア_Click(object sender, EventArgs e)
        private void button条件クリア_Click(object sender, EventArgs e)
        {
            // 取引先
            comboBox取引先.Text = "";

            // 手配依頼種別
            comboBox種別.SelectedIndex = 0;

            // 手配依頼詳細種別
            comboBox詳細種別.Items.Clear();

            // 船
            comboBox船.SelectedIndex = 0;

            // 状態（支払済）
            checkBox支払済.Checked = false;
        }
        #endregion


        private void button検索_Click(object sender, EventArgs e)
        {
            一覧表示();
        }

        private void 一覧表示()
        {
            TreeList.NodesClear();

            string msCustomerId = null;
            if (comboBox取引先.SelectedItem is MsCustomer)
            {
                MsCustomer customer = comboBox取引先.SelectedItem as MsCustomer;
                msCustomerId = customer.MsCustomerID;
            }
            string msThiIraiSbtId = null;
            if (comboBox種別.SelectedItem is MsThiIraiSbt)
            {
                MsThiIraiSbt thiIraiSbt  = comboBox種別.SelectedItem as MsThiIraiSbt;
                msThiIraiSbtId = thiIraiSbt.MsThiIraiSbtID;
            }
            string msThiIraiShousaiId = null;
            if (comboBox詳細種別.SelectedItem is MsThiIraiShousai)
            {
                MsThiIraiShousai thiIraiShousai = comboBox詳細種別.SelectedItem as MsThiIraiShousai;
                msThiIraiShousaiId = thiIraiShousai.MsThiIraiShousaiID;
            }
            int msVesselId = -1;
            if (comboBox船.SelectedItem is MsVessel)
            {
                MsVessel vessel = comboBox船.SelectedItem as MsVessel;
            }
            int status = -1;
            if (checkBox支払済.Checked)
            {
                status = (int)OdShrGassanHead.StatusEnum.支払済;
            }

            this.Cursor = Cursors.WaitCursor;

            List<OdShrGassanHead> gassanHeads = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                {
                    gassanHeads = serviceClient.OdShrGassanHead_GetRecords(NBaseCommon.Common.LoginUser, msCustomerId, msThiIraiSbtId, msThiIraiShousaiId, msVesselId, status);
                }, "データ取得中です...");
                progressDialog.ShowDialog();
            }

            TreeList.NodesClear();
            if (gassanHeads != null)
            {
                TreeList.AddNodes(gassanHeads);
            }

            this.Cursor = Cursors.Default;
        }


        private void button合算作成_Click(object sender, EventArgs e)
        {
            支払合算作成Form form = new 支払合算作成Form();
            if (form.ShowDialog(this.MdiParent) == DialogResult.OK)
            {
                一覧表示();
            }
        }


        private void button詳細内容_Click(object sender, EventArgs e)
        {
            OdShrGassanHead shrGassanHead = TreeList.選択データ取得();
            if (shrGassanHead == null)
            {
                MessageBox.Show("受領データが選択されていません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            支払合算詳細Form form = new 支払合算詳細Form(shrGassanHead);
            if (form.ShowDialog(this.MdiParent) == DialogResult.OK)
            {
                一覧表示();
            }
        }
    }
}
