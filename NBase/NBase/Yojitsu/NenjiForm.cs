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
using System.Threading;
using NBaseData.DS;

namespace Yojitsu
{
    public partial class NenjiForm : Form
    {
        private BgYosanHead yosanHead;
        private MsVessel vessel;
        private List<BgKadouVessel> kadouVessels;
        // 前年度最終.
        private BgKadouVessel lastKadouVessel;

        private NenjiTreeListViewDelegate treeListViewDelegate;
        private EditTableTitleControl titleControl;

        private MsVessel lastSelectedVessel;


        public NenjiForm(BgYosanHead yosanHead, MsVessel vessel, List<BgKadouVessel> kadouVessels)
        {
            this.yosanHead = yosanHead;
            this.vessel = this.lastSelectedVessel = vessel;
            this.kadouVessels = kadouVessels;

            LoadLastKadouVessel(yosanHead, vessel);

            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("", "予算編集（年次）", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            Init();

            butt閉じる.Focus();
            ActiveControl = butt閉じる;
        }


        private void LoadLastKadouVessel(BgYosanHead yosanHead, MsVessel vessel)
        {
            // 昨年度
            BgYosanHead preYosanHead = DbAccessorFactory.FACTORY.BgYosanHead_GetRecordByYear(NBaseCommon.Common.LoginUser, (yosanHead.Year - 1).ToString());

            if (preYosanHead != null)
            {
                List<BgKadouVessel> kv = DbAccessorFactory.FACTORY.BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselIDAndYearRange(NBaseCommon.Common.LoginUser,
                                                                                                           preYosanHead.YosanHeadID,
                                                                                                           vessel.MsVesselID,
                                                                                                           preYosanHead.Year,
                                                                                                           preYosanHead.Year);

                if (kv != null && kv.Count > 0)
                {
                    lastKadouVessel = kv[0];
                }
            }
        }


        private void Init()
        {
            treeListViewDelegate = new NenjiTreeListViewDelegate(treeListView1);

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
                List<BgYosanItem> items = null;
                List<BgJiseki> jisekis = null;

                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    // 前年度予算
                    BgYosanHead preYosanHead = DbAccessorFactory.FACTORY.BgYosanHead_GetRecordByYear(NBaseCommon.Common.LoginUser, (yosanHead.Year - 1).ToString());

                    if (preYosanHead != null)
                    {
                        items =
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
                        items = new List<BgYosanItem>();
                    }

                    // 2013.08: 
                    // 2013.05: TOP、月次と合わせるために、ダミーを作成しない
                    if (items.Count == 0)
                    {
                        // 前年度予算が存在しないときは、ダミーの空の BgYosanItem を生成する.
                        List<MsHimoku> himokus = DbAccessorFactory.FACTORY.MsHimoku_GetRecords(NBaseCommon.Common.LoginUser);

                        foreach (MsHimoku h in himokus)
                        {
                            BgYosanItem it = new BgYosanItem();
                            it.MsHimokuID = h.MsHimokuID;
                            it.Nengetsu = (yosanHead.Year - 1) + "  ";

                            items.Add(it);
                        }
                    }

                    // 今年度以降予算
                    items.AddRange(
                      DbAccessorFactory.FACTORY.
                      BgYosanItem_GetRecords_年単位_船(NBaseCommon.Common.LoginUser,
                                                    yosanHead.YosanHeadID,
                                                    vessel.MsVesselID,
                                                    yosanHead.Year.ToString(),
                                                    (yosanHead.Year + NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID)).ToString()
                                                   ));
                    // 実績
                    jisekis =
                      DbAccessorFactory.FACTORY.
                      BgJiseki_GetRecords_年単位_船(NBaseCommon.Common.LoginUser,
                                                    vessel.MsVesselID,
                                                    (yosanHead.Year - 1) + NBaseData.DS.Constants.PADDING_MONTHS[0],
                                                    yosanHead.Year + NBaseData.DS.Constants.PADDING_MONTHS[11]
                                                   );

                    // 船稼働
                    kadouVessels = DbAccessorFactory.FACTORY.BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselID(NBaseCommon.Common.LoginUser,
                                                                                                                yosanHead.YosanHeadID,
                                                                                                                vessel.MsVesselID);
                    LoadLastKadouVessel(yosanHead, vessel);
                }, "データ取得中です...");

                progressDialog.ShowDialog();

                if (items != null && items.Count > 0)
                {
                    titleControl.RefreshComponents(yosanHead, vessel.VesselName);
                    treeListViewDelegate.CreateYojitsuTable(yosanHead, kadouVessels, lastKadouVessel, items, jisekis);
                }
                else
                {
                    MessageBox.Show("該当する予算データは存在しません。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    if (this.vessel != this.lastSelectedVessel)
                    {
                        ChangeVessel(lastSelectedVessel.MsVesselID);
                    }
                }
            }

            this.Cursor = Cursors.Default;
        }


        private void EnableComponents()
        {
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "年次", "換算レート入力"))
            {
                buttonレート入力.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "年次", "販管割掛入力"))
            {
                button販管割掛入力.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "年次", "運賃・運航費入力"))
            {
                button運航費入力.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "年次", "貸船・借船料入力"))
            {
                button貸船借船料入力.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "年次", "修繕費予算入力"))
            {
                button修繕費予算入力.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "年次", "特別修繕引当金入力"))
            {
                if (!yosanHead.IsFixed())
                {
                    button特修金入力.Enabled = true;
                }
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "年次", "共通割掛船員入力"))
            {
                button共通割掛船員入力.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "予実管理", "年次", "船稼働設定"))
            {
                button船稼働設定.Enabled = true;
            }
        }


        private void ComboBox船_SelectionChangeCommitted(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Interval = 500;
            timer1.Enabled = true;
        }


        private void button月次展開_Click(object sender, EventArgs e)
        {
            ShowGetsujiForm();
        }


        private void ShowGetsujiForm()
        {
            if (IsUpdated())
            {
                Save();
            }

            GetsujiForm form = new GetsujiForm(this, yosanHead, vessel, kadouVessels[0], lastKadouVessel);

            form.ShowDialog();
            LoadData();
        }


        private void butt入力完了_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("入力完了します。よろしいですか？",
                                "Wing - 入力完了",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question) == DialogResult.OK)
            {
                BgNrkKanryou nk = DbTableCache.instance().GetBgNrkKanryou(yosanHead);
                nk.NrkKanryo = nk.RenewDate = DateTime.Now;
                nk.NrkKanryoUserID = nk.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;

                DbAccessorFactory.FACTORY.BgNrkKanryou_UpdateRecord(NBaseCommon.Common.LoginUser, nk);

                MessageBox.Show("入力完了しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void butt閉じる_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void butt販管割掛入力_Click(object sender, EventArgs e)
        {
            // 売上高を取得する
            Dictionary<int, decimal> uriagedakaList = Get売上高();

            販管割掛入力Form form = new 販管割掛入力Form(this, yosanHead, uriagedakaList);

            //予算ヘッドデータ設定
            form.YOSAN_HEAD = this.yosanHead;

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (Save())
                {
                    LoadData();
                }
            }
        }


        private void butt共通割掛船員入力_Click(object sender, EventArgs e)
        {
            Excelファイル読込Form form = new Excelファイル読込Form(this, yosanHead, BgYosanExcel.ShubetsuEnum.共通割掛船員);
            form.ShowDialog();
        }


        private void butt船稼働設定_Click(object sender, EventArgs e)
        {
            船稼働設定Form form = new 船稼働設定Form(yosanHead, 船稼働設定Form.ConfigType.船稼働設定, true);

            form.ShowDialog();
            LoadData();
        }


        private bool IsUpdated()
        {
            return treeListViewDelegate.GetEditedBgYosanItems().Count > 0;
        }


        private void button保存_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                LoadData();
            }
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


        private void NenjiForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void buttonレート_Click(object sender, EventArgs e)
        {
            if (yosanHead != null)
            {
                bool canEdit = true;

                if (yosanHead.IsFixed())
                {
                    canEdit = false;
                }

                ドルレートForm ドルレートForm = new ドルレートForm(this, yosanHead, canEdit);

                if (ドルレートForm.ShowDialog() == DialogResult.OK)
                {
                    titleControl.RefreshComponents(yosanHead, vessel.VesselName);
                }
            }
        }


        private void button運航費入力_Click(object sender, EventArgs e)
        {
            運航費入力Form form = new 運航費入力Form(this, yosanHead, vessel);
            form.ShowDialog();
        }


        private void button貸船借船料入力_Click(object sender, EventArgs e)
        {
            Excelファイル読込Form form = new Excelファイル読込Form(this, yosanHead, vessel, BgYosanExcel.ShubetsuEnum.貸船借船料);
            form.ShowDialog();
        }


        private void button修繕費予算入力_Click(object sender, EventArgs e)
        {
            入渠Form form = new 入渠Form(this, yosanHead, vessel, kadouVessels);
            form.ShowDialog();
        }


        private void button特修金入力_Click(object sender, EventArgs e)
        {
            特別修繕引当金入力Form form = new 特別修繕引当金入力Form(this, yosanHead);
            form.ShowDialog();
        }


        public void ChangeVessel(int msVesselId)
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


        public void SetUnkouhi(BgUnkouhi unkouhi, bool doCopy)
        {
            if (unkouhi.Year == yosanHead.Year)
            {
                GetsujiForm form = new GetsujiForm(this, yosanHead, vessel, kadouVessels[0], lastKadouVessel);
                form.Set運航費(unkouhi);
                form.Dispose();
            }

            treeListViewDelegate.Set運航費(unkouhi, doCopy);

            if (Save())
            {
                LoadData();
            }
        }


        internal void Set修繕費(Dictionary<int, Dictionary<string, decimal>> amountDic)
        {
            treeListViewDelegate.Set修繕費(amountDic);

            if (Save())
            {
                LoadData();
            }
        }


        internal void Set特別修繕引当金(string year, string month, decimal totalAmount)
        {
            GetsujiForm form = new GetsujiForm(this, yosanHead, vessel, kadouVessels[0], lastKadouVessel);
            form.Set特別修繕引当金(year, month, totalAmount);
            form.Dispose();

            treeListViewDelegate.Set特別修繕引当金(year, month, totalAmount);

            if (Save())
            {
                LoadData();
            }
        }


        internal void SetExcelData(Excelファイル読込Form.YosanObjectCollection yosanObject)
        {
            foreach (KeyValuePair<int, Dictionary<int, Dictionary<string, Excelファイル読込Form.YosanObject>>> pair in yosanObject.YosanDic)
            {
                int msVesselId = pair.Key;

                ChangeVessel(msVesselId);

                GetsujiForm form = new GetsujiForm(this, yosanHead, vessel, kadouVessels[0], lastKadouVessel);
                form.SetExcelData(yosanObject.YosanDic[msVesselId]);
                form.Dispose();

                treeListViewDelegate.SetExcelData(yosanObject.YosanDic[msVesselId]);

                if (Save())
                {
                    LoadData();
                }
            }
        }


        internal void Set販管費(int year, List<int> msVesselIds, List<decimal> amounts)
        {
            for (int i = 0; i < msVesselIds.Count; i++)
            {
                ChangeVessel(msVesselIds[i]);
                treeListViewDelegate.Set販管費(year, amounts[i]);

                if (Save())
                {
                    LoadData();
                }
            }
        }


        internal void Set換算レート(List<BgRate> rates)
        {
            foreach (MsVessel v in titleControl.ComboBox船.Items)
            {
                ChangeVessel(v.MsVesselID);

                GetsujiForm form = new GetsujiForm(this, yosanHead, v, kadouVessels[0], lastKadouVessel);
                form.Set換算レート(rates);
                form.Dispose();

                treeListViewDelegate.Set換算レート(rates);

                if (Save())
                {
                    LoadData();
                }
            }
        }


        public Dictionary<int, decimal> Get売上高()
        {
            this.Cursor = Cursors.WaitCursor;

            Dictionary<int, decimal> uriagedakaList = new Dictionary<int, decimal>();

            // 自船分の売上高
            //Dictionary<int, decimal> uriagedakaList = treeListViewDelegate.Get売上高();

            // 全社
            List<BgYosanItem> items = null;

            ProgressDialog progressDialog = new ProgressDialog(delegate
            {
                // 予算（1年）
                items =
                  DbAccessorFactory.FACTORY.
                  BgYosanItem_GetRecords_年単位_全社(NBaseCommon.Common.LoginUser,
                                                yosanHead.YosanHeadID,
                                                (yosanHead.Year).ToString(),
                                                (yosanHead.Year).ToString()
                                               );
            }, "データ取得中です...");
            progressDialog.ShowDialog();


            List<long> yosanItemIdList = new List<long>();

            foreach (BgYosanItem i in items)
            {
                long yosanItemId = i.YsanItemID;
                int msHimokuId = i.MsHimokuID;
                string nengetsu = i.Nengetsu;

                if (yosanItemIdList.Contains(yosanItemId))
                    continue;

                yosanItemIdList.Add(yosanItemId);

                if (msHimokuId == Yojitsu.DA.Constants.MS_HIMOKU_ID_売上高 && nengetsu.Trim().Length == 4)
                {
                    int nen = int.Parse(nengetsu);
                    if (uriagedakaList.ContainsKey(nen))
                    {
                        uriagedakaList[nen] += i.YenAmount;
                    }
                    else
                    {
                        uriagedakaList.Add(nen, i.YenAmount);
                    }
                }
            }


            this.Cursor = Cursors.Default;

            return uriagedakaList;
        }


        // 2014.05.21 : 船稼動設定を実施後、入渠設定しなかった場合、年次画面で稼動設定が更新されない不具合
        public void ReloadKadouVessel()
        {
            LoadData();
        }
    }
}
