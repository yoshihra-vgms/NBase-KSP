using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Yojitsu.util;
using Yojitsu.DA;
using LidorSystems.IntegralUI.Lists;
using System.Text.RegularExpressions;
using NBaseUtil;

namespace Yojitsu
{
    public partial class 入渠Form : Form
    {
        private NenjiForm nenjiForm;
        
        private BgYosanHead yosanHead;
        private MsVessel vessel;
        private List<BgKadouVessel> kadouVessels;

        private EditTableTitleControl titleControl;
        private TreeListViewDelegate入渠 treeListViewDelegate;

        private BgYosanBikou yosanBikou;

        private MsVessel lastSelectedVessel;


        // 2014.05.21 : 船稼動設定を実施後、入渠設定しなかった場合、年次画面で稼動設定が更新されない不具合
        private bool isUpdateKadouVessel = false;


        public 入渠Form(NenjiForm nenjiForm, BgYosanHead yosanHead, MsVessel vessel, List<BgKadouVessel> kadouVessels)
        {
            this.nenjiForm = nenjiForm;
            this.yosanHead = yosanHead;
            this.vessel = this.lastSelectedVessel = vessel;
            this.kadouVessels = kadouVessels;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            titleControl = new EditTableTitleControl(yosanHead, vessel, null);
            titleControl.RefreshComponents(yosanHead, vessel.VesselName);
            
            panel1.Controls.Add(titleControl);

            treeListViewDelegate = new TreeListViewDelegate入渠(treeListView1);

            LoadData();

            titleControl.ComboBox船.SelectionChangeCommitted += new EventHandler(SelectionChangeCommitted);

            EnableComponents();
        }


        private void EnableComponents()
        {
            if (yosanHead.IsFixed())
            {
                textBox備考.ReadOnly = true;
                button設定.Enabled = false;
            }
        }


        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;

            if (yosanHead != null)
            {
                List<BgUchiwakeYosanItem> items = null;

                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    // 予算
                    items =
                      DbAccessorFactory.FACTORY.
                      BgUchiwakeYosanItem_GetRecords_入渠(NBaseCommon.Common.LoginUser,
                                                    yosanHead.YosanHeadID,
                                                    vessel.MsVesselID,
                                                    yosanHead.Year.ToString(),
                                                    (yosanHead.Year + NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID)).ToString()
                                                   );

                    // 船稼働
                    kadouVessels = DbAccessorFactory.FACTORY.BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselID(NBaseCommon.Common.LoginUser,
                                                                                                                yosanHead.YosanHeadID,
                                                                                                                vessel.MsVesselID);

                    // 備考
                    yosanBikou = DbAccessorFactory.FACTORY.BgYosanBikou_GetRecoreByYosanHeadIDAndMsVesselID(NBaseCommon.Common.LoginUser,
                                                                                                                yosanHead.YosanHeadID,
                                                                                                                vessel.MsVesselID);
                    
                }, "データ取得中です...");

                progressDialog.ShowDialog();

                titleControl.RefreshComponents(yosanHead, vessel.VesselName);
                treeListViewDelegate.CreateTable(yosanHead, vessel.MsVesselID, kadouVessels, items);

                if (vessel.AnniversaryDate != DateTime.MinValue)
                {
                    label検査指定日.Text = vessel.AnniversaryDate.ToString("MM/dd");
                }
                else
                {
                    label検査指定日.Text = "--/--";
                }

                if (yosanBikou != null)
                {
                    textBox備考.Text = yosanBikou.Bikou;
                }
                else
                {
                    textBox備考.Text = string.Empty;
                }

                nenjiForm.ChangeVessel(vessel.MsVesselID);
            }

            this.Cursor = Cursors.Default;
        }


        private void SelectionChangeCommitted(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Interval = 500;
            timer1.Enabled = true;
        }


        private void button検査設定_Click(object sender, EventArgs e)
        {
            if (treeListViewDelegate.IsUpdated())
            {
                Save();
            }

            船稼働設定Form form = new 船稼働設定Form(yosanHead, 船稼働設定Form.ConfigType.検査設定, true);

            // 2014.05.21 : 船稼動設定を実施後、入渠設定しなかった場合、年次画面で稼動設定が更新されない不具合
            //form.ShowDialog();
            //LoadData();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();

                isUpdateKadouVessel = true;
            }
        }


        private void buttonファイル出力_Click(object sender, EventArgs e)
        {
            修繕費出力選択Form form = new 修繕費出力選択Form();

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    int msVesselId = int.MinValue;

                    if (form.Kind == 修繕費出力選択Form.出力種別.各船)
                    {
                        msVesselId = vessel.MsVesselID;
                    }
                    
                    byte[] excelData = null;

                    this.Cursor = Cursors.WaitCursor;

			        try
			        {
                        NBaseUtil.ProgressDialog progressDialog = new NBaseUtil.ProgressDialog(delegate
                        {
                            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                            {
                                excelData = serviceClient.BLC_Excel修繕費予算出力(NBaseCommon.Common.LoginUser, yosanHead, msVesselId);
                            }
                        }, "修繕費予算を出力中です...");
                        progressDialog.ShowDialog();

                        if (excelData == null)
                        {
                            MessageBox.Show("修繕費予算の出力に失敗しました。\n (テンプレートファイルがありません。管理者に確認してください。)", "修繕費予算", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Cursor = System.Windows.Forms.Cursors.Default;
                            return;
                        }
                        using (System.IO.FileStream stream = new System.IO.FileStream(saveFileDialog1.FileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite))
                        {
                            stream.Write(excelData, 0, excelData.Length);
                        }
                        this.Cursor = System.Windows.Forms.Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        //カーソルを通常に戻す
                        this.Cursor = System.Windows.Forms.Cursors.Default;
                        MessageBox.Show("修繕費予算の出力に失敗しました。\n (Err:" + ex.Message + ")", "修繕費予算", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("ファイルを出力しました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void button設定_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                MessageBox.Show("入渠データを保存しました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
        }


        private bool Save()
        {
            List<BgUchiwakeYosanItem> uchiwakeYosanItems = treeListViewDelegate.GetEditedBgUchiwakeYosanItems();
            BuildYosanBikou();

            bool result = DbAccessorFactory.FACTORY.BLC_修繕費保存(NBaseCommon.Common.LoginUser, uchiwakeYosanItems, yosanBikou);

            if (!result)
            {
                MessageBox.Show("既にデータが更新されているので保存できませんでした。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                nenjiForm.Set修繕費(treeListViewDelegate.Get修繕費Dic());
                return true;
            }
        }


        private void BuildYosanBikou()
        {
            if (textBox備考.Modified)
            {
                if (yosanBikou == null)
                {
                    yosanBikou = new BgYosanBikou();
                    yosanBikou.YosanHeadID = yosanHead.YosanHeadID;
                    yosanBikou.MsVesselID = vessel.MsVesselID;
                }

                //yosanBikou.Bikou = textBox備考.Text;
                yosanBikou.Bikou = StringUtils.Escape(textBox備考.Text);
            }
        }

        
        private void butt閉じる_Click(object sender, EventArgs e)
        {
            // 2014.05.21 : 船稼動設定を実施後、入渠設定しなかった場合、年次画面で稼動設定が更新されない不具合
            //Dispose();

            if (isUpdateKadouVessel)
            {
                nenjiForm.ReloadKadouVessel();
            }
            Dispose();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (treeListViewDelegate.IsUpdated() || textBox備考.Modified)
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
    }
}
