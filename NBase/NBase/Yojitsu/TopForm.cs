using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseUtil;
using LidorSystems.IntegralUI.Lists;
using Yojitsu.DA;
using NBaseData.DAC;
using Yojitsu.util;
using NBaseData.DS;

namespace Yojitsu
{
    public partial class TopForm : Form
    {
        private List<BgYosanHead> yosanHeads;

        private BgYosanHead selectedYosanHead;
        private MsVessel selectedVessel;
        private List<BgKadouVessel> kadouVessels;

        private TreeListViewDelegate入力完了 treeListViewDelegate入力完了;
        private TopTreeListViewDelegate treeListViewDelegate予実;

        private EditTableTitleControl titleControl;

        private int lastSelectedComboBox年度Index;
        private int lastSelectedCombobox船Index;
        private int lastSelectedComboBox予算種別Index;
        private int lastSelectedComboBoxリビジョンIndex;

        //m.yoshihara 2017/5/29
        private string  str未Fix = " (未 Fix)";
        private int flg未Fix;

        public TopForm()
        {
            InitializeComponent();

            this.Text = NBaseCommon.Common.WindowTitle("", "予実トップ", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            DbAccessorFactory.InitFactory("wcf");

            Init();

            butt閉じる.Focus();
            ActiveControl = butt閉じる;

        }


        private void Init()
        {
            // 入力完了テーブル
            treeListViewDelegate入力完了 = new TreeListViewDelegate入力完了(treeListView2);

            // 予実テーブル
            treeListViewDelegate予実 = new TopTreeListViewDelegate(treeListView1);

            titleControl = new EditTableTitleControl(treeListViewDelegate予実);
            panel2.Controls.Add(titleControl);

            RefreshComponents();
        }

        /// <summary>
        /// 再描画
        /// </summary>
        #region private void RefreshComponents()
        private void RefreshComponents()
        {
            yosanHeads = DbAccessorFactory.FACTORY.BgYosanHead_GetRecords(NBaseCommon.Common.LoginUser);

            // 年度
            InitComboBox年度();
            // 船
            InitComboBox船();
            // 予算種別
            InitComboBox予算種別();
            // リビジョン
            InitComboBoxリビジョン();

            LoadData();
        }
        #endregion


        /// <summary>
        /// データを取得し、画面にセットする
        /// </summary>
        #region private void LoadData()
        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;

            foreach (BgYosanHead h in yosanHeads)
            {
                if (h.Year == Int32.Parse(comboBox年度.Text) &&
                    BgYosanSbt.ToName(h.YosanSbtID) == comboBox予算種別.Text &&
                    h.Revision == Int32.Parse(comboBoxリビジョン.Text.Split(' ')[0]))
                {
                    selectedYosanHead = h;
                    break;
                }
            }

            EnableComponents();

            if (selectedYosanHead != null)
            {
                // 入力完了
                treeListViewDelegate入力完了.CreateTable(selectedYosanHead);

                List<BgYosanItem> items = null;
                List<BgJiseki> jisekis = null;

                // 全社
                if (comboBox船.SelectedIndex == 0)
                {
                    ProgressDialog progressDialog = new ProgressDialog(delegate
                   {
                       // 予算（20年）
                       items =
                         DbAccessorFactory.FACTORY.
                         BgYosanItem_GetRecords_年単位_全社(NBaseCommon.Common.LoginUser,
                                                       selectedYosanHead.YosanHeadID,
                                                       (selectedYosanHead.Year).ToString(),
                                                       (selectedYosanHead.Year + NBaseData.BLC.予実.GetYearRange(selectedYosanHead.YosanSbtID) - 1).ToString()
                                                      );
                       // 実績（1年）
                       jisekis =
                         DbAccessorFactory.FACTORY.
                         BgJiseki_GetRecords_年単位_全社(NBaseCommon.Common.LoginUser,
                                                       selectedYosanHead.Year + NBaseData.DS.Constants.PADDING_MONTHS[0],
                                                       (selectedYosanHead.Year + 1) + NBaseData.DS.Constants.PADDING_MONTHS[11]
                                                      );

                       selectedVessel = null;
                       kadouVessels = null;
                   }, "データ取得中です...");

                    progressDialog.ShowDialog();
                }
                // グループ
                else if (comboBox船.SelectedItem is MsVesselType)
                {
                    MsVesselType vesselType = comboBox船.SelectedItem as MsVesselType;

                    ProgressDialog progressDialog = new ProgressDialog(delegate
                   {
                       // 予算（20年）
                       items =
                         DbAccessorFactory.FACTORY.
                         BgYosanItem_GetRecords_年単位_グループ(NBaseCommon.Common.LoginUser,
                                                       selectedYosanHead.YosanHeadID,
                                                       vesselType.MsVesselTypeID,
                                                       (selectedYosanHead.Year).ToString(),
                                                       (selectedYosanHead.Year + NBaseData.BLC.予実.GetYearRange(selectedYosanHead.YosanSbtID) - 1).ToString()
                                                      );
                       // 実績（1年）
                       jisekis =
                         DbAccessorFactory.FACTORY.
                         BgJiseki_GetRecords_年単位_グループ(NBaseCommon.Common.LoginUser,
                                                       vesselType.MsVesselTypeID,
                                                       selectedYosanHead.Year + NBaseData.DS.Constants.PADDING_MONTHS[0],
                                                       (selectedYosanHead.Year + 1) + NBaseData.DS.Constants.PADDING_MONTHS[11]
                                                      );

                       selectedVessel = null;
                       kadouVessels = null;
                   }, "データ取得中です...");

                    progressDialog.ShowDialog();
                }
                // 船
                else if (comboBox船.SelectedItem is MsVessel)
                {
                    selectedVessel = comboBox船.SelectedItem as MsVessel;

                    ProgressDialog progressDialog = new ProgressDialog(delegate
                  {
                      // 予算（20年）
                      items =
                        DbAccessorFactory.FACTORY.
                        BgYosanItem_GetRecords_年単位_船(NBaseCommon.Common.LoginUser,
                                                      selectedYosanHead.YosanHeadID,
                                                      selectedVessel.MsVesselID,
                                                      (selectedYosanHead.Year).ToString(),
                                                      (selectedYosanHead.Year + 19).ToString()
                                                     );
                      // 実績（1年）
                      jisekis =
                        DbAccessorFactory.FACTORY.
                        BgJiseki_GetRecords_年単位_船(NBaseCommon.Common.LoginUser,
                                                      selectedVessel.MsVesselID,
                                                      selectedYosanHead.Year + NBaseData.DS.Constants.PADDING_MONTHS[0],
                                                      (selectedYosanHead.Year + 1) + NBaseData.DS.Constants.PADDING_MONTHS[11]
                                                     );

                      kadouVessels = DbAccessorFactory.FACTORY.BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselID(NBaseCommon.Common.LoginUser,
                                                                                                                 selectedYosanHead.YosanHeadID,
                                                                                                                 selectedVessel.MsVesselID);
                  }, "データ取得中です...");

                    progressDialog.ShowDialog();
                }
                else
                {
                    MessageBox.Show("「全社/グループ/船」を選択してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Cursor = Cursors.Default;
                    return;
                }

                if (items != null && items.Count > 0)
                {
                    titleControl.RefreshComponents(selectedYosanHead, comboBox船.Text);
                    treeListViewDelegate予実.CreateYojitsuTable(selectedYosanHead, kadouVessels, items, jisekis);

                    lastSelectedComboBox年度Index = comboBox年度.SelectedIndex;
                    lastSelectedCombobox船Index = comboBox船.SelectedIndex;
                    lastSelectedComboBox予算種別Index = comboBox予算種別.SelectedIndex;
                    lastSelectedComboBoxリビジョンIndex = comboBoxリビジョン.SelectedIndex;
                }
                else
                {
                    MessageBox.Show("該当する予算データは存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                    // 元の状態に戻す
                    if (comboBox船.SelectedIndex != lastSelectedCombobox船Index)
                    {
                        comboBox船.SelectedIndex = lastSelectedCombobox船Index;
                    }
                    comboBox年度.SelectedIndex = lastSelectedComboBox年度Index;
                    InitComboBox予算種別();

                    comboBox予算種別.SelectedIndex = lastSelectedComboBox予算種別Index;
                    InitComboBoxリビジョン();

                    comboBoxリビジョン.SelectedIndex = lastSelectedComboBoxリビジョンIndex;
                    LoadData();
                }
            }

            this.Cursor = Cursors.Default;
        }
        #endregion


        /// <summary>
        /// ボタンの有効/無効を設定する
        /// </summary>
        #region private void EnableComponents()
        private void EnableComponents()
        {
            //if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "予実トップ", "当初予算作成"))
            //{
            //    if (yosanHeads.Count == 0)
            //    {
            //        butt当初予算作成.Enabled = true;
            //    }
            //}

            //BgYosanHead newYosanHead = GetNewYosanHead();

            //if (yosanHeads.Count == 0 || yosanHeads[0].IsFixed())
            //{
            //    if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "予実トップ", "当初予算作成") &&
            //        newYosanHead.YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.当初)
            //    {
            //        butt当初予算作成.Enabled = true;
            //        butt見直し予算作成.Enabled = false;
            //    }
            //    else if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "予実トップ", "見直し予算作成") &&
            //        newYosanHead.YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.見直し)
            //    {
            //        butt当初予算作成.Enabled = false;
            //        butt見直し予算作成.Enabled = true;
            //    }
            //}
            butt当初予算作成.Enabled = false;
            butt見直し予算作成.Enabled = false;
            butt編集.Enabled = false;
            butt予算Fix.Enabled = false;
            butt予算Revアップ.Enabled = false;
            buttメモ編集閲覧.Enabled = false;

            butt経常収支.Enabled = false;
            butt月次収支.Enabled = false;
            buttonダイジェスト出力.Enabled = false;

            if (yosanHeads.Count == 0 || yosanHeads[0].IsFixed())
            {
                if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "予実トップ", "当初予算作成") &&
                    (yosanHeads.Count == 0 || yosanHeads[0].YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.見直し))
                {
                    butt当初予算作成.Enabled = true;
                    butt見直し予算作成.Enabled = false;
                }
                else if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "予実トップ", "見直し予算作成") &&
                    (yosanHeads.Count == 0 || yosanHeads[0].YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.当初))
                {
                    butt当初予算作成.Enabled = false;
                    butt見直し予算作成.Enabled = true;
                }
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "予実トップ", "編集"))
            {
                if (selectedYosanHead != null)
                {
                    butt編集.Enabled = true;
                }
            }

            if (selectedYosanHead != null && !selectedYosanHead.IsFixed())
            {
                butt編集.Text = "編集";

                if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "予実トップ", "予算Fix"))
                {
                    butt予算Fix.Enabled = true;
                }
            }
            else
            {
                butt編集.Text = "閲覧";
                butt予算Fix.Enabled = false;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "予実トップ", "予算Revアップ"))
            {
                if (yosanHeads.Count > 0 && yosanHeads[0].IsFixed())
                {
                    butt予算Revアップ.Enabled = true;
                }
                else
                {
                    butt予算Revアップ.Enabled = false;
                }
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "予実トップ", "メモ編集・閲覧"))
            {
                buttメモ編集閲覧.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "予実トップ", "経常収支実績出力"))
            {
                butt経常収支.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "予実トップ", "月次収支報告書出力"))
            {
                butt月次収支.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "予実トップ", "ダイジェスト出力"))
            {
                buttonダイジェスト出力.Enabled = true;
            }

        }
        #endregion




        /// <summary>
        /// 「編集」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void editButt_Click(object sender, EventArgs e)
        private void editButt_Click(object sender, EventArgs e)
        {
            NenjiForm form;

            if (selectedVessel == null)
            {
                MsVessel vessel = null;

                foreach (object obj in comboBox船.Items)
                {
                    if (obj is MsVessel)
                    {
                        vessel = obj as MsVessel;
                        break;
                    }
                }

                kadouVessels = DbAccessorFactory.FACTORY.BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselID(NBaseCommon.Common.LoginUser,
                                                                                                           selectedYosanHead.YosanHeadID,
                                                                                                           vessel.MsVesselID);
                form = new NenjiForm(selectedYosanHead, vessel, kadouVessels);
            }
            else
            {
                form = new NenjiForm(selectedYosanHead, selectedVessel, kadouVessels);
            }

            form.ShowDialog();
            LoadData();
        }
        #endregion


        /// <summary>
        /// 「当初予算作成」ボタンクリック
        /// 「見直し予算作成」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void butt予算作成_Click(object sender, EventArgs e)
        private void butt予算作成_Click(object sender, EventArgs e)
        {
            予算作成Form form = new 予算作成Form(GetNewYosanHead());

            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshComponents();

                butt当初予算作成.Enabled = false;
                butt見直し予算作成.Enabled = false;
            }
        }
        #endregion

        /// <summary>
        /// 「予算Fix」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void butt予算Fix_Click(object sender, EventArgs e)
        private void butt予算Fix_Click(object sender, EventArgs e)
        {
            string message = yosanHeads[0].Year + "年度 [" + BgYosanSbt.ToName(yosanHeads[0].YosanSbtID) + "予算" +
                                " Rev." + yosanHeads[0].Revision + "] ";

            if (MessageBox.Show(message + "\nを Fix します。よろしいですか？",
                                "Wing - 予算 Fix",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question) == DialogResult.OK)
            {
                yosanHeads[0].FixDate = DateTime.Now;
                DbAccessorFactory.FACTORY.BLC_予算Fix(NBaseCommon.Common.LoginUser, yosanHeads[0]);

                if (MessageBox.Show(message + "を Fix しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    RefreshComponents();
                }
            }
        }
        #endregion

        /// <summary>
        /// 「予算Revアップ」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void butt予算Revアップ_Click(object sender, EventArgs e)
        private void butt予算Revアップ_Click(object sender, EventArgs e)
        {
            予算RevアップForm form = new 予算RevアップForm(yosanHeads[0]);

            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshComponents();
            }
        }
        #endregion

        /// <summary>
        /// 「メモ編集閲覧」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void buttメモ編集閲覧_Click(object sender, EventArgs e)
        private void buttメモ編集閲覧_Click(object sender, EventArgs e)
        {
            メモ編集閲覧Form form = new メモ編集閲覧Form(yosanHeads);
            form.ShowDialog();
        }
        #endregion



        /// <summary>
        /// 「予算表出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void button1_Click(object sender, EventArgs e)
        {
            予算表出力Form form = new 予算表出力Form(yosanHeads);
            form.ShowDialog();
        }
        #endregion

        /// <summary>
        /// 「予算対比表出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void button2_Click(object sender, EventArgs e)
        {
            予算対比表出力Form form = new 予算対比表出力Form(yosanHeads);
            form.ShowDialog();
        }
        #endregion

        /// <summary>
        /// 「経常収支実績出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void butt経常収支_Click(object sender, EventArgs e)
        {
            //経常収支実績出力Form form = new 経常収支実績出力Form(yosanHeads);
            //form.ShowDialog();
        }
        #endregion

        /// <summary>
        /// 「月次収支報告書出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void butt月次収支_Click(object sender, EventArgs e)
        {
            //月次収支報告書出力Form form = new 月次収支報告書出力Form(yosanHeads);
            月次収支資料出力Form form = new 月次収支資料出力Form(yosanHeads);
            form.ShowDialog();
        }
        #endregion

        /// <summary>
        /// 「ダイジェスト出力」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void buttonダイジェスト出力_Click(object sender, EventArgs e)
        {
            //ダイジェスト表示Form form = new ダイジェスト表示Form(yosanHeads);
            //form.ShowDialog();
        }
        #endregion


        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void butt閉じる_Click(object sender, EventArgs e)
        private void butt閉じる_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion


        /// <summary>
        /// 「年度」ドロップダウン
        /// </summary>
        #region private void InitComboBox年度()
        private void InitComboBox年度()
        {
            comboBox年度.Items.Clear();

            List<int> years = new List<int>();

            foreach (BgYosanHead h in yosanHeads)
            {
                if (!years.Contains(h.Year))
                {
                    comboBox年度.Items.Add(h.Year);
                    years.Add(h.Year);
                }
            }

            if (comboBox年度.Items.Count > 0)
            {
                comboBox年度.SelectedIndex = 0;
            }
        }
        #endregion

        private void comboBox年度_SelectionChangeCommitted(object sender, EventArgs e)
        {
            InitComboBox予算種別();
            InitComboBoxリビジョン();
        }

        /// <summary>
        /// 「全社/グループ/船」ドロップダウン
        /// </summary>
        #region private void InitComboBox船()
        private void InitComboBox船()
        {
            comboBox船.Items.Clear();

            comboBox船.Items.Add("全社");
            comboBox船.Items.Add("--------------------");

            foreach (MsVesselType v in DbAccessorFactory.FACTORY.MsVesselType_GetRecords(NBaseCommon.Common.LoginUser))
            {
                comboBox船.Items.Add(v);
            }

            comboBox船.Items.Add("--------------------");

            //m.yoshihara コメントアウト
            //foreach (MsVessel v in DbAccessorFactory.FACTORY.MsVessel_GetRecords(NBaseCommon.Common.LoginUser))
            //{
            //    comboBox船.Items.Add(v);
            //}

            //-----------------------------------------------------------------------
            //m.yoshihara 2017/6/1
            foreach (MsVessel v in DbAccessorFactory.FACTORY.MsVessel_GetRecords(NBaseCommon.Common.LoginUser))
            {
                if (flg未Fix == 1)
                {
                    if (v.YojitsuEnabled == 0)
                    {
                        System.Diagnostics.Debug.WriteLine("v="+v.VesselName);
                        continue;
                    }
                }
                comboBox船.Items.Add(v);
            }
            //-----------------------------------------------------------------------

            comboBox船.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// 「予算種別」ドロップダウン
        /// </summary>
        #region private void InitComboBox予算種別()
        private void InitComboBox予算種別()
        {
            comboBox予算種別.Items.Clear();

            foreach (BgYosanHead h in yosanHeads)
            {
                if (h.Year == Int32.Parse(comboBox年度.Text))
                {
                    if (!comboBox予算種別.Items.Contains(BgYosanSbt.ToName(h.YosanSbtID)))
                    {
                        comboBox予算種別.Items.Add(BgYosanSbt.ToName(h.YosanSbtID));
                    }
                }
            }

            if (comboBox予算種別.Items.Count > 0)
            {
                comboBox予算種別.SelectedIndex = 0;
            }
        }
        #endregion

        private void comboBox予算種別_SelectionChangeCommitted(object sender, EventArgs e)
        {
            InitComboBoxリビジョン();
        }

        /// <summary>
        /// 「Rev.」ドロップダウン
        /// </summary>
        #region private void InitComboBoxリビジョン()
        private void InitComboBoxリビジョン()
        {
            comboBoxリビジョン.Items.Clear();

            foreach (BgYosanHead h in yosanHeads)
            {
                if (h.Year == Int32.Parse(comboBox年度.Text) &&
                    BgYosanSbt.ToName(h.YosanSbtID) == comboBox予算種別.Text)
                {
                    string revStr = h.Revision.ToString();

                    if (!h.IsFixed())
                    {
                        revStr += str未Fix;// " (未 Fix)";m.yoshihara 2017/5/29
                    }
                    else
                    {
                        revStr += " (" + h.FixDate.ToString("yyyy/MM/dd") + " Fix)";
                    }

                    comboBoxリビジョン.Items.Add(revStr);
                }
            }


            if (comboBoxリビジョン.Items.Count > 0)
            {
                comboBoxリビジョン.SelectedIndex = 0;
            }
        }
        #endregion

        /// <summary>
        /// 「表示」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button表示_Click(object sender, EventArgs e)
        private void button表示_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        #endregion



        private BgYosanHead GetNewYosanHead()
        {
            BgYosanHead head = new BgYosanHead();

            if (yosanHeads.Count > 0)
            {
                BgYosanHead last = yosanHeads[0];

                if (last.YosanSbtID == (int)(BgYosanSbt.BgYosanSbtEnum.当初))
                {
                    head.Year = last.Year;
                    head.YosanSbtID = (int)(BgYosanSbt.BgYosanSbtEnum.見直し);
                }
                else
                {
                    head.Year = last.Year + 1;
                    head.YosanSbtID = (int)(BgYosanSbt.BgYosanSbtEnum.当初);
                }

            }
            else
            {
                head.Year = DateTime.Now.Year;
                head.YosanSbtID = 0;
            }

            head.Revision = 0;

            return head;
        }

        //m.yoshihara 2017/5/29
        private void comboBoxリビジョン_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxリビジョン.Text.Contains( str未Fix) )
            {
                flg未Fix = 1;
            }
            else 
            {
                flg未Fix = 0;
            }
            InitComboBox船();
        }

    }
}
