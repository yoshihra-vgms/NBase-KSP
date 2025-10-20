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
using NBaseData.DAC;
using Yojitsu.DA;
using Yojitsu.util;
using NBaseData.DS;

namespace Yojitsu
{
    public partial class GetsujiForm : Form
    {
        private NenjiForm nenjiForm;

        private BgYosanHead yosanHead;
        private MsVessel vessel;
        private BgKadouVessel kadouVessel;
        private BgKadouVessel lastKadouVessel;

        private GetsujiTreeListViewDelegate treeListViewDelegate;
        private EditTableTitleControl titleControl;

        private List<BgYosanItem> yosanItems_前今年度;
        private List<BgYosanItem> yosanItems_今年度_月別;
        private List<BgJiseki> jisekis_前年度;
        private List<BgJiseki> jisekis_今年度_月別;

        private MsVessel lastSelectedVessel;


        public GetsujiForm(NenjiForm nenjiForm, BgYosanHead yosanHead, MsVessel vessel, BgKadouVessel kadouVessel, BgKadouVessel lastKadouVessel)
        {
            this.nenjiForm = nenjiForm;
            this.yosanHead = yosanHead;
            this.vessel = this.lastSelectedVessel = vessel;
            this.kadouVessel = kadouVessel;
            this.lastKadouVessel = lastKadouVessel;

            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("", "予算編集（月次）", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            Init();

            butt年次計画.Focus();
            ActiveControl = butt年次計画;
        }


        private void Init()
        {
            treeListViewDelegate = new GetsujiTreeListViewDelegate(treeListView1);

            titleControl = new EditTableTitleControl(yosanHead, vessel, treeListViewDelegate);
            panel2.Controls.Add(titleControl);

            LoadData();

            treeListViewDelegate.Edited += new YojitsuTreeListViewDelegate.EditEventHandler(treeListViewDelegate_Edited);
            titleControl.ComboBox船.SelectionChangeCommitted += new EventHandler(ComboBox船_SelectionChangeCommitted);
        }


        void treeListViewDelegate_Edited()
        {
            button保存.Enabled = true;
        }


        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;

            EnableComponents();

            if (yosanHead != null)
            {
                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    // 前年度予算
                    BgYosanHead preYosanHead = DbAccessorFactory.FACTORY.BgYosanHead_GetRecordByYear(NBaseCommon.Common.LoginUser, (yosanHead.Year - 1).ToString());

                    if (preYosanHead != null)
                    {
                        yosanItems_前今年度 =
                          DbAccessorFactory.FACTORY.
                          BgYosanItem_GetRecords_年単位_船(NBaseCommon.Common.LoginUser,
                                                        preYosanHead.YosanHeadID,
                                                        vessel.MsVesselID,
                                                        (yosanHead.Year - 1).ToString(),
                                                        (yosanHead.Year - 1).ToString()
                                                       );
                    }
                    else
                    {
                        yosanItems_前今年度 = new List<BgYosanItem>();
                    }

                    // 2013.08: 
                    if (yosanItems_前今年度.Count == 0)
                    {
                        // 前年度予算が存在しないときは、ダミーの空の BgYosanItem を生成する.
                        List<MsHimoku> himokus = DbAccessorFactory.FACTORY.MsHimoku_GetRecords(NBaseCommon.Common.LoginUser);

                        foreach (MsHimoku h in himokus)
                        {
                            BgYosanItem it = new BgYosanItem();
                            it.MsHimokuID = h.MsHimokuID;
                            it.Nengetsu = (yosanHead.Year - 1) + "  ";

                            yosanItems_前今年度.Add(it);
                        }
                    }

 
                    // 今年度予算
                    yosanItems_前今年度.AddRange(
                      DbAccessorFactory.FACTORY.
                      BgYosanItem_GetRecords_年単位_船(NBaseCommon.Common.LoginUser,
                                                    yosanHead.YosanHeadID,
                                                    vessel.MsVesselID,
                                                    yosanHead.Year.ToString(),
                                                    yosanHead.Year.ToString()
                                                   ));
                    // 今年度予算（月別）
                    yosanItems_今年度_月別 =
                      DbAccessorFactory.FACTORY.
                      BgYosanItem_GetRecords_月単位(NBaseCommon.Common.LoginUser,
                                                    yosanHead.YosanHeadID,
                                                    vessel.MsVesselID,
                                                    (yosanHead.Year).ToString(),
                                                    (yosanHead.Year + NBaseData.BLC.予実.GetMonthRange(yosanHead.YosanSbtID) - 1).ToString()
                                                   );
                    // 前年度実績
                    jisekis_前年度 =
                      DbAccessorFactory.FACTORY.
                      BgJiseki_GetRecords_年単位_船(NBaseCommon.Common.LoginUser,
                                                    vessel.MsVesselID,
                                                    (yosanHead.Year - 1) + NBaseData.DS.Constants.PADDING_MONTHS[0],
                                                    yosanHead.Year + NBaseData.DS.Constants.PADDING_MONTHS[11]
                                                   );
                    // 今年度実績（月別）
                    jisekis_今年度_月別 =
                      DbAccessorFactory.FACTORY.
                      BgJiseki_GetRecords_月単位_船(NBaseCommon.Common.LoginUser,
                                                    vessel.MsVesselID,
                                                    yosanHead.Year + NBaseData.DS.Constants.PADDING_MONTHS[0],
                                                    (yosanHead.Year + 1) + NBaseData.DS.Constants.PADDING_MONTHS[11]
                                                   );
                }, "データ取得中です...");

                progressDialog.ShowDialog();


                if (yosanItems_前今年度 != null && yosanItems_前今年度.Count > 0 && yosanItems_今年度_月別 != null && yosanItems_今年度_月別.Count > 0)
                {
                    titleControl.RefreshComponents(yosanHead, vessel.VesselName);
                    treeListViewDelegate.CreateYojitsuTable(yosanHead, kadouVessel, lastKadouVessel, yosanItems_前今年度, yosanItems_今年度_月別, jisekis_前年度, jisekis_今年度_月別);
                    treeListViewDelegate.ShowJisseki(checkBox実績表示.Checked);
                }
                else
                {
                    MessageBox.Show("該当する予算データは存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    if (this.vessel != this.lastSelectedVessel)
                    {
                        ChangeVessel(lastSelectedVessel.MsVesselID);
                    }
                }

                nenjiForm.ChangeVessel(vessel.MsVesselID);
            }

            this.Cursor = Cursors.Default;
        }


        private void EnableComponents()
        {
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "月次", "実績取込"))
            {
                button実績取込.Enabled = true;
            }

            if (yosanHead.IsFixed() || yosanHead.YosanSbtID == (int)BgYosanSbt.BgYosanSbtEnum.見直し)
            {
                butt振分.Enabled = false;
            }
        }

        private void ChangeVessel(int msVesselId)
        {
            foreach (MsVessel v in titleControl.ComboBox船.Items)
            {
                if (v.MsVesselID == msVesselId && titleControl.ComboBox船.SelectedItem != v)
                {
                    if (IsUpdated())
                    {
                        Save();
                    }

                    titleControl.ComboBox船.SelectedItem = v;

                    this.lastSelectedVessel = this.vessel;
                    this.vessel = titleControl.ComboBox船.SelectedItem as MsVessel;

                    LoadData();
                    break;
                }
            }
        }


        private void ComboBox船_SelectionChangeCommitted(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Interval = 500;
            timer1.Enabled = true;
        }


        private void butt年次計画_Click(object sender, EventArgs e)
        {
            Close();
        }


        private bool IsUpdated()
        {
            return treeListViewDelegate.GetEditedBgYosanItems().Count > 0;
        }


        private bool Save()
        {
            BgNrkKanryou nk = DbTableCache.instance().GetBgNrkKanryou(yosanHead);
            nk.NrkKanryo = DateTime.MinValue;
            nk.NrkKanryoUserID = null;
            nk.RenewDate = DateTime.Now;
            nk.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

            bool result = DbAccessorFactory.FACTORY.BLC_予算保存(NBaseCommon.Common.LoginUser,
                                                                treeListViewDelegate.GetEditedBgYosanItems(), nk);

            if (!result)
            {
                if (MessageBox.Show("既にデータが更新されているので保存できませんでした。最新のデータを読み込みますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.No)
                {
                    return false;
                }
            }

            button保存.Enabled = false;
            return true;
        }


        private void checkBox実績表示_CheckedChanged(object sender, EventArgs e)
        {
            treeListViewDelegate.ShowJisseki(checkBox実績表示.Checked);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (IsUpdated())
            {
                DialogResult result = MessageBox.Show("予算が変更されています。保存しますか？",
                                                      "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Save();
                }
            }

            this.lastSelectedVessel = this.vessel;
            this.vessel = titleControl.ComboBox船.SelectedItem as MsVessel;

            LoadData();
        }


        private void GetsujiForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsUpdated())
            {
                DialogResult result = MessageBox.Show("予算が変更されています。保存しますか？",
                                                      "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Save();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }


        private void butt振分_Click(object sender, EventArgs e)
        {
            if (treeListView1.SelectedNodes.Count == 0)
            {
                MessageBox.Show("振り分けを行う費目行を選択してください。",
                                                      "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            treeListViewDelegate.Furiwake();
        }


        private void button保存_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                LoadData();
            }
        }


        public void Set運航費(BgUnkouhi unkouhi)
        {
            treeListViewDelegate.Set運航費(unkouhi);
            Save();
        }


        internal void Set特別修繕引当金(string year, string month, decimal totalAmount)
        {
            treeListViewDelegate.Set特別修繕引当金(year, month, totalAmount);
            Save();
        }


        internal void SetExcelData(Dictionary<int, Dictionary<string, Excelファイル読込Form.YosanObject>> amountDic)
        {
            treeListViewDelegate.SetExcelData(amountDic);
            Save();
        }


        private void button実績取込_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("実績取込を実行します。よろしいですか？",
                                                  "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                bool result2 = DbAccessorFactory.FACTORY.BLC_実績取込(NBaseCommon.Common.LoginUser);

                if (result2)
                {
                    MessageBox.Show(this, "実績を取り込みました。", "実績取込", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show(this, "実績取込に失敗しました。", "実績取込", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        internal void Set換算レート(List<BgRate> rates)
        {
            treeListViewDelegate.Set換算レート(rates);
            Save();
        }
    }
}
