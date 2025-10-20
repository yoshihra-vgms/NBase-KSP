using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using SyncClient;
using NBaseData.DS;
using NBaseUtil;
using ORMapping;
using NBaseHonsen.Senin.util;
using NBaseHonsen.util;

namespace NBaseHonsen
{
    public partial class 一括受領確認Form : Form
    {
        private readonly PortalForm portalForm;
        private readonly OdJry odJry;


        public 一括受領確認Form(PortalForm portalForm, OdJry odJry)
        {
            this.portalForm = portalForm;
            this.odJry = odJry;

            InitializeComponent();
            Text = NBaseCommon.Common.WindowTitle("HN010202", "一括受領確認", WcfServiceWrapper.ConnectedServerID);
        }

        private void button_はい_Click(object sender, EventArgs e)
        {
            foreach (OdJryItem ji in odJry.OdJryItems)
            {
                foreach (OdJryShousaiItem si in ji.OdJryShousaiItems)
                {
                    si.JryCount = si.Count;
                    si.Nouhinbi = DateTime.Now;
                }
            }

            // 2014.02 2013年度改造
            odJry.Status = (int)OdJry.STATUS.船受領;

            UpdateRecords();
            portalForm.BuildModel();
            portalForm.UpdateTable();
            DialogResult = DialogResult.Yes;
            Dispose();
        }

        private void UpdateRecords()
        {
            using (DBConnect dbConnect = new DBConnect())
            {
                dbConnect.BeginTransaction();

                try
                {
                    // 2014.02 2013年度改造
                    SyncTableSaver.InsertOrUpdate(odJry, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);

                    foreach (OdJryItem ji in odJry.OdJryItems)
                    {
                        foreach (OdJryShousaiItem si in ji.OdJryShousaiItems)
                        {
                            SyncTableSaver.InsertOrUpdate(si, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, dbConnect);
                        }
                    }

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

        private void button_いいえ_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Dispose();
        }
    }
}
