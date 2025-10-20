using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using Yojitsu.util;
using NBaseData.DAC;
using Yojitsu.DA;

namespace Yojitsu
{
    public partial class 船稼働設定Form : Form
    {
        public enum ConfigType { 船稼働設定, 検査設定 };
        public ConfigType configType { get; private set; }
        
        private BgYosanHead yosanHead;
        public List<BgKadouVessel> KadouVessels { get; private set; }
        private List<BgKadouVessel> workKadouVessels = new List<BgKadouVessel>();
        private TreeListViewDelegate船稼働設定 treeListViewDelegate;

        // 設定ボタン押下時、DB に保存する.
        private readonly bool saveDb;


        public 船稼働設定Form(BgYosanHead yosanHead, ConfigType configType, bool save)
            : this(yosanHead, LoadKadouVessels(yosanHead), configType, save)
        {
        }


        private static List<BgKadouVessel> LoadKadouVessels(BgYosanHead yosanHead)
        {
            return DbAccessorFactory.FACTORY.BgKadouVessel_GetRecordsByYosanHeadID(NBaseCommon.Common.LoginUser,
                                                                                                         yosanHead.YosanHeadID);
        }


        public 船稼働設定Form(BgYosanHead yosanHead, List<BgKadouVessel> kadouVessels, ConfigType configType, bool save)
        {
            this.yosanHead = yosanHead;
            this.KadouVessels = kadouVessels;
            this.configType = configType;
            this.saveDb = save;

            foreach (BgKadouVessel kv in KadouVessels)
            {
                workKadouVessels.Add(kv.Clone());
            }

            InitializeComponent();

            if (configType == ConfigType.船稼働設定)
            {
                this.Text = NBaseCommon.Common.WindowTitle("", "船稼働設定", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            }
            else
            {
                this.Text = NBaseCommon.Common.WindowTitle("", "検査設定", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            }

            Init();
        }

        private void Init()
        {
            labelYosanHead.Text = yosanHead.Year + "年度 [" + BgYosanSbt.ToName(yosanHead.YosanSbtID) + "予算" +
                                              " Rev." + EditTableTitleControl.CreateRevisitionStr(yosanHead) + "] ";

            treeListViewDelegate = new TreeListViewDelegate船稼働設定(treeListView1);
            treeListViewDelegate.CreateTable(yosanHead, workKadouVessels);

            EnableComponents();
        }


        private void EnableComponents()
        {
            if (yosanHead.IsFixed())
            {
                button設定.Enabled = false;
            }
        }


        private void button設定_Click(object sender, EventArgs e)
        {
            if (saveDb)
            {
                bool result = DbAccessorFactory.FACTORY.BgKadouVessel_UpdateRecords(NBaseCommon.Common.LoginUser, workKadouVessels);

                if (!result)
                {
                    MessageBox.Show("既にデータが更新されているので保存できませんでした。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            DialogResult = DialogResult.OK;
            KadouVessels = workKadouVessels;
            Dispose();
        }

        private void butt閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Dispose();
        }

        private void treeListView1_DoubleClick(object sender, EventArgs e)
        {
            if (sender is TreeListView)
            {
                TreeListView treeListView = (TreeListView)sender;
                BgKadouVessel kv = treeListViewDelegate.GetBgKadouVessel(treeListView.SelectedSubItem);

                if (kv != null)
                {
                    船稼働設定日時Form form = new 船稼働設定日時Form(this,
                                                                     treeListView.SelectedSubItem,
                                                                     yosanHead,
                                                                     kv);
                    form.ShowDialog();
                }
            }
        }


        internal static string AmOrPm(DateTime dateTime)
        {
            if (dateTime.Hour == 0)
            {
                return "AM";
            }
            else
            {
                return "PM";
            }
        }
    }
}
