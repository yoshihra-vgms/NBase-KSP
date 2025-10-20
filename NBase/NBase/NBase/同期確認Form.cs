using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ServiceReferences.NBaseService;
using NBaseData.DAC;

namespace NBase
{
    public partial class 同期確認Form : Form
    {
        public 同期確認Form()
        {
            InitializeComponent();

            label表示時刻.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH：mm 現在");
        }

        private void button更新_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                List<SnSyncInfo> syncInfos = null;
                List<MsVessel> vessles = null;
                List<MsUser> users = null;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    syncInfos = serviceClient.SnSyncInfo_GetRecords(NBaseCommon.Common.LoginUser);
                    vessles = serviceClient.MsVessel_GetRecords(NBaseCommon.Common.LoginUser);
                    users = serviceClient.MsUser_GetRecords(NBaseCommon.Common.LoginUser);
                }

                label表示時刻.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH：mm 現在");
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("船No", typeof(string)));
                dt.Columns.Add(new DataColumn("船名", typeof(string)));
                dt.Columns.Add(new DataColumn("同期日時", typeof(string)));
                dt.Columns.Add(new DataColumn("ユーザ", typeof(string)));
                dt.Columns.Add(new DataColumn("ｺﾝﾋﾟｭｰﾀ名", typeof(string)));
                dt.Columns.Add(new DataColumn("船ﾃﾞｰﾀ", typeof(string)));
                dt.Columns.Add(new DataColumn("ｻｰﾊﾞﾃﾞｰﾀ", typeof(string)));
                dt.Columns.Add(new DataColumn("同期状況", typeof(string)));
                dt.Columns.Add(new DataColumn("モジュールVer", typeof(string)));
                dt.Columns.Add(new DataColumn("管理番号(共通)", typeof(string)));
                dt.Columns.Add(new DataColumn("管理番号(個別)", typeof(string)));

                var sortedList = syncInfos.OrderByDescending(o => o.VesselDate);
                foreach (SnSyncInfo syncInfo in sortedList)
                {
                    MsVessel v = null;
                    MsUser u = null;
                    try
                    {
                        v = (from V in vessles
                             where V.MsVesselID == syncInfo.MsVesselID
                             select V).Single<MsVessel>();
                    }
                    catch
                    {
                    }
                    try
                    {
                        u = (from U in users
                             where U.MsUserID == syncInfo.MsUserID
                             select U).Single<MsUser>();
                    }
                    catch
                    {
                    }

                    DataRow row = dt.NewRow();
                    row["船No"] = v != null ? v.VesselNo : "";
                    row["船名"] = v != null ? v.VesselName : "";
                    row["同期日時"] = syncInfo.VesselDate != DateTime.MinValue ? syncInfo.VesselDate.ToString("yyyy/MM/dd HH:mm") : "";
                    row["ユーザ"] = u != null ? u.Sei + " " + u.Mei : "";
                    row["ｺﾝﾋﾟｭｰﾀ名"] = syncInfo.HostName;
                    row["船ﾃﾞｰﾀ"] = SnSyncInfo.FormatデータFlag(syncInfo.FromVesselFlag);
                    row["ｻｰﾊﾞﾃﾞｰﾀ"] = SnSyncInfo.FormatデータFlag(syncInfo.FromServerFlag);
                    row["同期状況"] = SnSyncInfo.Format同期進捗(syncInfo.SyncStep);
                    row["モジュールVer"] = syncInfo.ModuleVersion;
                    row["管理番号(共通)"] = syncInfo.MaxDataNoOfVesselIdZero.ToString();
                    row["管理番号(個別)"] = syncInfo.MaxDataNo.ToString();

                    dt.Rows.Add(row);
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Width = 60;    //船No
                dataGridView1.Columns[1].Width = 200;   //船名
                dataGridView1.Columns[2].Width = 110;   //同期日時
                dataGridView1.Columns[3].Width = 120;   //ユーザ
                dataGridView1.Columns[4].Width = 120;   //ｺﾝﾋﾟｭｰﾀ名
                dataGridView1.Columns[5].Width = 80;    //船ﾃﾞｰﾀ
                dataGridView1.Columns[6].Width = 80;    //ｻｰﾊﾞﾃﾞｰﾀ
                dataGridView1.Columns[7].Width = 100;    //同期状況
                dataGridView1.Columns[8].Width = 95;   //モジュールVer
                dataGridView1.Columns[9].Width = 110;   //管理番号(共通)
                dataGridView1.Columns[10].Width = 110;  //管理番号(個別)

            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}
