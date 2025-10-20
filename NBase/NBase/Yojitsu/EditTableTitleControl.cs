using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Yojitsu.DA;
using Yojitsu.util;

namespace Yojitsu
{
    partial class EditTableTitleControl : UserControl
    {
        private BgYosanHead yosanHead;
        private YojitsuTreeListViewDelegate treeListViewDelegate;
        
        private ComboBox comboBox船;
        public ComboBox ComboBox船 { get { return comboBox船; } }

        private Label label船;

        private Label labelYosanHead;

        private List<CheckBox> msBumonCheckBoxes = new List<CheckBox>();
        
        private Label labelレート;


        public EditTableTitleControl(YojitsuTreeListViewDelegate treeListViewDelegate)
            : this(null, null, treeListViewDelegate)
        {
        }

        public EditTableTitleControl(BgYosanHead yosanHead, MsVessel vessel, YojitsuTreeListViewDelegate treeListViewDelegate)
        {
            this.yosanHead = yosanHead;
            this.treeListViewDelegate = treeListViewDelegate;

            InitializeComponent();
            Init(vessel);
        }

        private void Init(MsVessel vessel)
        {
            flowLayoutPanel1.Controls.Clear();

            labelYosanHead = new Label();
            labelYosanHead.AutoSize = true;
            labelYosanHead.Anchor = AnchorStyles.None;
            labelYosanHead.Font = new Font(labelYosanHead.Font, FontStyle.Bold);
            flowLayoutPanel1.Controls.Add(labelYosanHead);

            if (vessel != null)
            {
                comboBox船 = new ComboBox();
                comboBox船.DropDownStyle = ComboBoxStyle.DropDownList;
                comboBox船.Anchor = AnchorStyles.None;
                comboBox船.Size = new Size(180, 20);
                flowLayoutPanel1.Controls.Add(comboBox船);
            }
            else
            {
                label船 = new Label();
                label船.AutoSize = true;
                label船.Anchor = AnchorStyles.None;
                label船.Font = new Font(label船.Font, FontStyle.Bold);
                flowLayoutPanel1.Controls.Add(label船);
            }

            if (treeListViewDelegate != null)
            {
                List<MsBumon> bumons = DbAccessorFactory.FACTORY.MsBumon_GetRecords(NBaseCommon.Common.LoginUser);

                foreach (MsBumon b in bumons)
                {
                    if (Constants.IncludeHimoku(b.MsBumonID))
                    {
                        CheckBox c = new CheckBox();
                        c.Anchor = AnchorStyles.None;
                        c.AutoSize = true;
                        c.Text = b.BumonName;
                        c.Tag = b;

                        msBumonCheckBoxes.Add(c);

                        if (!Constants.BUMON_VISIBILITY.ContainsKey(b.MsBumonID))
                        {
                            Constants.BUMON_VISIBILITY[b.MsBumonID] = true;
                        }

                        c.Checked = Constants.BUMON_VISIBILITY[b.MsBumonID];

                        flowLayoutPanel1.Controls.Add(c);
                        c.CheckStateChanged += new EventHandler(c_CheckStateChanged);
                    }
                }

                labelレート = new Label();
                labelレート.AutoSize = true;
                labelレート.Anchor = AnchorStyles.None;
                flowLayoutPanel1.Controls.Add(labelレート);

                Button rateButton = new Button();
                rateButton.Text = "詳細";
                rateButton.Anchor = AnchorStyles.None;
                rateButton.Size = new Size(75, 23);
                rateButton.BackColor = SystemColors.Control;
                rateButton.Font = new Font(new FontFamily("MS UI Gothic"), 9f);
                flowLayoutPanel1.Controls.Add(rateButton);

                rateButton.Click += new System.EventHandler(this.buttonドルレート_Click);
            }
            
            if (vessel != null)
            {
                InitComboBox船(comboBox船, vessel);
            }
        }

        
        void c_CheckStateChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            MsBumon bumon = checkBox.Tag as MsBumon;

            Constants.BUMON_VISIBILITY[bumon.MsBumonID] = checkBox.Checked;
            treeListViewDelegate.NodesVisible(bumon.MsBumonID, checkBox.Checked);
        }


        internal void RefreshComponents(BgYosanHead yosanHead, string vesselStr)
        {
            this.yosanHead = yosanHead;

            foreach (CheckBox checkBox in msBumonCheckBoxes)
            {
                MsBumon bumon = checkBox.Tag as MsBumon;
                checkBox.Checked = Constants.BUMON_VISIBILITY[bumon.MsBumonID];
            }

            if (label船 != null)
            {
                label船.Text = vesselStr;
            }
            
            labelYosanHead.Text = yosanHead.Year + "年度 [" + BgYosanSbt.ToName(yosanHead.YosanSbtID) + "予算" +
                                               " Rev." + CreateRevisitionStr(yosanHead) + "] ";

            if (treeListViewDelegate != null)
            {
                List<BgRate> rates = DbTableCache.instance().GetBgRateList(yosanHead);
                //labelレート.Text = "[換算レート 上期:\\" + rates[0].KamikiRate + " 下期:\\" + rates[0].ShimokiRate + "]";
                labelレート.Text = "[換算レート 上期:\\" + rates[1].KamikiRate + " 下期:\\" + rates[1].ShimokiRate + "]";
            }
        }


        internal static string CreateRevisitionStr(BgYosanHead yosanHead)
        {
            string revStr = yosanHead.Revision.ToString();
            
            if (!yosanHead.IsFixed())
            {
                revStr += " (未 Fix)";
            }
            else
            {
                revStr += " (" + yosanHead.FixDate.ToString("yyyy/MM/dd") + " Fix)";
            }

            return revStr;
        }

        
        private void InitComboBox船(ComboBox comboBox船, MsVessel vessel)
        {
            foreach (MsVessel v in DbAccessorFactory.FACTORY.MsVessel_GetRecords(NBaseCommon.Common.LoginUser))
            {
                comboBox船.Items.Add(v);

                if (v.MsVesselID == vessel.MsVesselID)
                {
                    comboBox船.SelectedItem = v;
                }
            }
        }

        internal void Add(Control control)
        {
            flowLayoutPanel1.Controls.Add(control);
        }


        private void buttonドルレート_Click(object sender, EventArgs e)
        {
            if (yosanHead != null)
            {
                ドルレートForm ドルレートForm = new ドルレートForm(null, yosanHead, false);
                ドルレートForm.ShowDialog();
            }
        }
    }
}
