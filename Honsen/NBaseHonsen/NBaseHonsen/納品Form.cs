using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using NBaseHonsen;
using NBaseData.DAC;
using SyncClient;
using NBaseData.DS;
using NBaseUtil;
using ORMapping;
using NBaseHonsen.Senin.util;
using NBaseHonsen.util;

namespace NBaseHonsen
{
    public partial class 納品Form : Form
    {
        private readonly PortalForm portalForm;
        private readonly OdJry odJry;
        private readonly OdJryShousaiItem odJryShousaiItem;
        
        
        public 納品Form(PortalForm portalForm, OdJry odJry, OdJryShousaiItem odJryShousaiItem)
        {
            同期Client.SYNC_SUSPEND = true;

            this.portalForm = portalForm;
            this.odJry = odJry;
            this.odJryShousaiItem = odJryShousaiItem;
            
            InitializeComponent();
            Text = NBaseCommon.Common.WindowTitle("HN010202", "受領", WcfServiceWrapper.ConnectedServerID);

            if (odJry.Status != (int)OdJry.STATUS.未受領)
            {
                textBox受領数.ReadOnly = true;
                buttデフォルト受領.Enabled = false;
            }
        }

        private void 納品Form_Load(object sender, EventArgs e)
        {
            textBox手配依頼日.Text = String.Format("{0:yyyy/MM/dd}", odJry.OdThiThiIraiDate);
            textBox出荷予定日.Text = String.Format("{0:yyyy/MM/dd}", odJry.OdMkNouki);
            textBox品目.Text = odJryShousaiItem.ShousaiItemName;
            textBox業者.Text = odJry.MsCustomerCustomerName;
            textBox納品数.Text = StringUtils.ToStr(odJryShousaiItem.Count);
            textBox受領数.Text = StringUtils.ToStr(odJryShousaiItem.JryCount);
            textBox手配依頼者.Text = odJry.MsUserThiUserName;
            textBox備考.Text = odJryShousaiItem.Bikou;
            
            buttデフォルト受領.Text = "受領数 " + textBox納品数.Text + " で受領";
        }

        private void okButt_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                if (textBox受領数.Text.Length == 0)
                {
                    odJryShousaiItem.JryCount = StringUtils.ToNumber(textBox納品数.Text);
                }
                else
                {
                    odJryShousaiItem.JryCount = StringUtils.ToNumber(textBox受領数.Text);
                }

                odJryShousaiItem.Nouhinbi = DateTime.Now;

                UpdateRecord();
                portalForm.BuildModel();
                portalForm.UpdateTable();
                Dispose();
            }
        }

        private void UpdateRecord()
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {

                    // 2014.02 2013年度改造
                    bool isJryAll = true;
                    foreach (OdJryItem ji in odJry.OdJryItems)
                    {
                        foreach (OdJryShousaiItem si in ji.OdJryShousaiItems)
                        {
                            //if (si.Nouhinbi == DateTime.MinValue)
                            if (si.Count > 0 && si.Nouhinbi == DateTime.MinValue) // 2016.03 受領対象は、Countが０以上のもののみ
                            {
                                isJryAll = false;
                                break;
                            }
                        }
                    }
                    if (isJryAll)
                    {
                        odJry.Status = (int)OdJry.STATUS.船受領;
                        SyncTableSaver.InsertOrUpdate(odJry, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                    }
                    //<==


                    SyncTableSaver.InsertOrUpdate(odJryShousaiItem, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期);

                    // 本船更新情報
                    PtHonsenkoushinInfo honsenkoushinInfo = 本船更新情報.CreateInstance(odJry, MsPortalInfoKoumoku.MsPortalInfoKoumokuIdEnum.受領);
                    SyncTableSaver.InsertOrUpdate(honsenkoushinInfo, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);

                    dbConnect.Commit();
                }
                catch (Exception e)
                {
                    dbConnect.RollBack();
                }
            }
        }

        private bool ValidateFields()
        {
            if (textBox受領数.Text.Length == 0)
            {
                return true;
            }

            int cnt;
            bool ret = int.TryParse(textBox受領数.Text, out cnt);

            if (!ret || cnt < 0)
            {
                MessageBox.Show("受領数は数値を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void cancelButt_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void textBox受領数_TextChanged(object sender, EventArgs e)
        {
            if (textBox受領数.Text.Length == 0)
            {
                buttデフォルト受領.Text = "受領数 " + textBox納品数.Text + " で受領";
            }
            else
            {
                buttデフォルト受領.Text = "受領数 " + textBox受領数.Text + " で受領";
            }
        }

        private void 納品Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            同期Client.SYNC_SUSPEND = false;
        }
    }
}
