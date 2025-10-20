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
using NBaseUtil;
using NBaseData.DS;

namespace Yojitsu
{
    public partial class 運航費入力Form : Form
    {
        private NenjiForm nenjiForm;
        private BgYosanHead yosanHead;
        private MsVessel vessel;
        private List<BgKadouVessel> kadouVessels;

        private BgUnkouhi unkouhi;
        private BlobUnkouhiList unkouhiData;

        private EditTableTitleControl titleControl;

        private bool updated;


        public 運航費入力Form(NenjiForm nenjiForm, BgYosanHead yosanHead, MsVessel vessel)
        {
            this.nenjiForm = nenjiForm;
            this.yosanHead = yosanHead;
            this.vessel = vessel;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            titleControl = new EditTableTitleControl(yosanHead, vessel, null);
            titleControl.ComboBox船.TabIndex = 1;
            titleControl.RefreshComponents(yosanHead, vessel.VesselName);

            panel1.Controls.Add(titleControl);

            YenControl.SetAmountType(運航費入力Control.Amount.円);
            DollarControl.SetAmountType(運航費入力Control.Amount.ドル);
            TotalControl.SetAmountType(運航費入力Control.Amount.円);
            TotalControl.SetReadOnly();

            年次月次Checked();

            LoadData();

            titleControl.ComboBox船.SelectionChangeCommitted += new EventHandler(SelectionChangeCommitted);
            comboBox年.SelectionChangeCommitted += new EventHandler(SelectionChangeCommitted);
            comboBox月.SelectionChangeCommitted += new EventHandler(SelectionChangeCommitted);

            EnableComponents();
        }


        private void EnableComponents()
        {
            if (yosanHead.IsFixed())
            {
                YenControl.SetReadOnly();
                DollarControl.SetReadOnly();

                checkBoxコピー.Enabled = false;
                button設定.Enabled = false;
            }
        }


        private void 年次月次Checked()
        {
            comboBox年.Items.Clear();
            comboBox月.Items.Clear();

            int yearRange = NBaseData.BLC.予実.GetYearRange(yosanHead.YosanSbtID);
            for (int i = 0; i < yearRange; i++)
            {
                comboBox年.Items.Add(yosanHead.Year + i);
            }

            comboBox年.SelectedIndex = 0;

            for (int i = 0; i < DateTimeUtils.instance().MONTH.Length; i++)
            {
                comboBox月.Items.Add(DateTimeUtils.instance().MONTH[i]);
            }

            comboBox月.SelectedIndex = 0;
        }


        private void SetUpdated(object sender, EventArgs e)
        {
            if ((sender as 運航費入力Control) == YenControl)
            {
                DollarControl.Set項目(YenControl, null);
                TotalControl.Set項目(YenControl, null);
            }
            else
            {
                YenControl.Set項目(null, DollarControl);
                TotalControl.Set項目(null, DollarControl);
            }

            TotalControl.CalcTotal(YenControl, DollarControl, DetectRate());
            updated = true;
        }


        private decimal DetectRate()
        {
            List<BgRate> rates = DbTableCache.instance().GetBgRateList(yosanHead);

            BgRate rate = null;
            foreach (BgRate r in rates)
            {
                if(r.Year == int.Parse(comboBox年.SelectedItem.ToString()))
                {
                    rate = r;
                    break;
                }
            }

            if (Yojitsu.DA.Constants.IsKamiki(int.Parse(comboBox月.SelectedItem.ToString())))
            {
                return rate.KamikiRate;
            }
            else
            {
                return rate.ShimokiRate;
            }
        }


        private void SelectionChangeCommitted(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Interval = 500;
            timer1.Enabled = true;
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

            this.vessel = titleControl.ComboBox船.SelectedItem as MsVessel;

            LoadData();
        }


        private void 運航費入力Form_FormClosing(object sender, FormClosingEventArgs e)
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


        private bool IsUpdated()
        {
            return updated;
        }


        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;

            if (yosanHead != null)
            {
                string year = comboBox年.SelectedItem.ToString();
                string month = comboBox月.SelectedItem.ToString();

                ProgressDialog progressDialog = new ProgressDialog(delegate
                {
                    unkouhi =
                      DbAccessorFactory.FACTORY.
                      BgUnkouhi_GetRecord(NBaseCommon.Common.LoginUser,
                                                    yosanHead.YosanHeadID,
                                                    vessel.MsVesselID,
                                                    int.Parse(year)
                                                   );
                    // 船稼働
                    kadouVessels = DbAccessorFactory.FACTORY.BgKadouVessel_GetRecordsByYosanHeadIDAndMsVesselID(NBaseCommon.Common.LoginUser,
                                                                                                                yosanHead.YosanHeadID,
                                                                                                                vessel.MsVesselID);
                }, "データ取得中です...");

                progressDialog.ShowDialog();

                titleControl.RefreshComponents(yosanHead, vessel.VesselName);
                unkouhiData = BlobUnkouhiList.ToObject(unkouhi.ObjectData);

                YenControl.updated -= new 運航費入力Control.UpdateEventHandler(SetUpdated);
                DollarControl.updated -= new 運航費入力Control.UpdateEventHandler(SetUpdated);

                BlobUnkouhi u = unkouhiData.List[comboBox月.SelectedIndex];
                YenControl.SetData(u.円データ);
                DollarControl.SetData(u.ドルデータ);

                YenControl.updated += new 運航費入力Control.UpdateEventHandler(SetUpdated);
                DollarControl.updated += new 運航費入力Control.UpdateEventHandler(SetUpdated);

                updated = false;

                textBox貨物.Text = vessel.VesselTypeName;

                nenjiForm.ChangeVessel(vessel.MsVesselID);

                TotalControl.CalcTotal(YenControl, DollarControl, DetectRate());

                textBox稼働情報.Text = Create船稼働String(kadouVessels[comboBox年.SelectedIndex]);
            }

            this.Cursor = Cursors.Default;
        }


        private void button設定_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }


        private bool Save()
        {
            BuildData();

            string year = comboBox年.SelectedItem.ToString();

            bool result = DbAccessorFactory.FACTORY.BLC_運航費保存(NBaseCommon.Common.LoginUser,
                                                                      yosanHead.YosanHeadID,
                                                                      vessel.MsVesselID,
                                                                      int.Parse(year),
                                                                      unkouhi, checkBoxコピー.Checked);

            if (!result)
            {
                MessageBox.Show("既にデータが更新されているので保存できませんでした。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                nenjiForm.SetUnkouhi(unkouhi, checkBoxコピー.Checked);
                MessageBox.Show("運航費データを保存しました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                updated = false;
                return true;
            }
        }


        private void CopyMonths()
        {
            unkouhiData.Copy(comboBox月.SelectedIndex);
        }


        private void BuildData()
        {
            YenControl.BuildData();
            DollarControl.BuildData();

            if (checkBoxコピー.Checked)
            {
                CopyMonths();
            }

            unkouhi.ObjectData = BlobUnkouhiList.ToBytes(unkouhiData);
        }


        private void butt閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        internal static string Create船稼働String(BgKadouVessel kadouVessel)
        {
            if (kadouVessel == null)
            {
                return "";
            }
            else if (kadouVessel.KadouStartDate == DateTime.MinValue || kadouVessel.KadouEndDate == DateTime.MinValue)
            {
                return "不稼働";
            }
            else
            {
                return Create稼働期間Str(kadouVessel) + " " + Create定期点検Str(kadouVessel);
            }
        }

        private static string Create稼働期間Str(BgKadouVessel bgKadouVessel)
        {
            if (bgKadouVessel.KadouStartDate.Month == 4 && bgKadouVessel.KadouStartDate.Day == 1 &&
                    船稼働設定Form.AmOrPm(bgKadouVessel.KadouStartDate) == "AM" &&
                    bgKadouVessel.KadouEndDate.Month == 3 && bgKadouVessel.KadouEndDate.Day == 31 &&
                    船稼働設定Form.AmOrPm(bgKadouVessel.KadouEndDate) == "PM")
            {
                return "　";
            }

            StringBuilder buff = new StringBuilder();

            if (bgKadouVessel.KadouStartDate.Month != 4 || bgKadouVessel.KadouStartDate.Day != 1 ||
                    船稼働設定Form.AmOrPm(bgKadouVessel.KadouStartDate) != "AM")
            {
                buff.Append(bgKadouVessel.KadouStartDate.ToString("MM/dd"));
                buff.Append(船稼働設定Form.AmOrPm(bgKadouVessel.KadouStartDate));
            }

            buff.Append(" ～ ");

            if (bgKadouVessel.KadouEndDate.Month != 3 || bgKadouVessel.KadouEndDate.Day != 31 ||
                    船稼働設定Form.AmOrPm(bgKadouVessel.KadouEndDate) != "PM")
            {
                buff.Append(bgKadouVessel.KadouEndDate.ToString("MM/dd"));
                buff.Append(船稼働設定Form.AmOrPm(bgKadouVessel.KadouEndDate));
            }

            return buff.ToString();
        }

        private static string Create定期点検Str(BgKadouVessel kadouVessel)
        {
            if (kadouVessel.NyukyoKind == null || kadouVessel.NyukyoKind == string.Empty)
            {
                return "　";
            }

            StringBuilder buff = new StringBuilder();

            buff.Append(kadouVessel.NyukyoKind);
            buff.Append("/");
            buff.Append(kadouVessel.NyukyoMonth);
            buff.Append("/");
            buff.Append(kadouVessel.Fukadoubi1);
            buff.Append("/");
            buff.Append(kadouVessel.Fukadoubi2);
            buff.Append("/");
            buff.Append(kadouVessel.Fukadoubi3);

            return buff.ToString();
        }
    }
}
