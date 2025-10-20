using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseHonsen.Senin.util;
using NBaseData.DAC;
using SyncClient;
using LidorSystems.IntegralUI.Lists;
using NBaseUtil;
using NBaseCommon.Senin.Excel;

namespace NBaseHonsen.Senin
{
    public partial class 船内収支Form : Form
    {
        private PortalForm parentForm;

        private TreeListViewDelegate送金 treeListViewDelegate送金;
        private TreeListViewDelegate船内収支 treeListViewDelegate船内収支;
        
        
        public 船内収支Form(PortalForm parentForm)
        {
            this.parentForm = parentForm;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox年();
            InitComboBox月();

            treeListViewDelegate送金 = new TreeListViewDelegate送金(treeListView送金);
            treeListViewDelegate船内収支 = new TreeListViewDelegate船内収支(treeListView船内準備金);

            Search送金();
            Search船内準備金();
        }


        private void InitComboBox年()
        {
            int thisYear = DateTime.Now.Year;

            for (int i = 0; i < 10; i++)
            {
                comboBox年.Items.Add(thisYear - i);
            }

            comboBox年.SelectedItem = thisYear;
        }


        private void InitComboBox月()
        {
            for (int i = 0; i < 12; i++)
            {
                string m = (i + 1).ToString();

                comboBox月.Items.Add(m);

                if (m.Trim() == DateTime.Now.Month.ToString())
                {
                    comboBox月.SelectedItem = m;
                }
            }
        }


        private void Search送金()
        {
            List<SiSoukin> result = SiSoukin.GetRecords_未受入(同期Client.LOGIN_USER, 同期Client.LOGIN_VESSEL.MsVesselID);

            treeListViewDelegate送金.SetRows(result);
        }


        private void button送金受入_Click(object sender, EventArgs e)
        {
            if (treeListView送金.SelectedNodes.Count == 0)
            {
                MessageBox.Show("送金受入を行う行を選択してください。",
                                                      "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("送金を受け入れます。よろしいですか？",
              "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (treeListViewDelegate送金.送金受入())
                {
                    MessageBox.Show("送金を受け入れました。", "送金", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Search送金();
                    Search船内準備金();
                    parentForm.MakeAlarmView();
                }
                else
                {
                    MessageBox.Show("送金の受け入れに失敗しました。", "送金", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        

        private void button明細追加_Click(object sender, EventArgs e)
        {
            船内収支詳細Form 船内準備金詳細Form = new 船内収支詳細Form(this, 同期Client.LOGIN_VESSEL);
            船内準備金詳細Form.ShowDialog();
        }

        
        private void treeListView船内準備金_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                TreeListViewNode selected = treeListView船内準備金.GetNodeAt(me.X, me.Y);

                if (selected != null)
                {
                    SiJunbikin j = selected.Tag as SiJunbikin;

                    if (j != null)
                    {
                        船内収支詳細Form 船内準備金詳細Form = new 船内収支詳細Form(this, 同期Client.LOGIN_VESSEL, j);
                        船内準備金詳細Form.ShowDialog();
                    }
                }
            }
        }


        internal void Search船内準備金()
        {
            DateTime date = new DateTime((int)comboBox年.SelectedItem, Int32.Parse(comboBox月.SelectedItem as string), 1);

            DateTime 先月締め日 = MsSiGetsujiShimeBi.ToFrom_船員_月次締日(NBaseCommon.Common.LoginUser, date).AddDays(-1);
            decimal 先月末残高 = SiJunbikin.Get_先月末残高(NBaseCommon.Common.LoginUser, date, 同期Client.LOGIN_VESSEL.MsVesselID);
            List<SiJunbikin> result = SiJunbikin.GetRecordsByDateAndMsVesselID(NBaseCommon.Common.LoginUser, date, 同期Client.LOGIN_VESSEL.MsVesselID);

            treeListViewDelegate船内収支.SetRows(DateTimeUtils.ToFromMonth(date), 先月締め日, 先月末残高, result);

            textBox受入金額.Text = NBaseCommon.Common.金額出力(treeListViewDelegate船内収支.Get_受入金額合計());
            textBox支払金額.Text = NBaseCommon.Common.金額出力(treeListViewDelegate船内収支.Get_支払金額合計());
            textBox繰越金額.Text = NBaseCommon.Common.金額出力(treeListViewDelegate船内収支.Get_受入金額合計() - treeListViewDelegate船内収支.Get_支払金額合計());
        }


        private void button船内収支金報告書_Click(object sender, EventArgs e)
        {
            string exeDir = System.IO.Directory.GetCurrentDirectory();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DateTime date = new DateTime((int)comboBox年.SelectedItem, Int32.Parse(comboBox月.SelectedItem as string), 1);

                string templateFilePath = exeDir + "\\Template\\Template_船内収支報告書.xlsx";
                new 船内収支報告書出力(templateFilePath, saveFileDialog1.FileName).CreateFile(NBaseCommon.Common.LoginUser, date, 同期Client.LOGIN_VESSEL.MsVesselID);

                MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void button科目別集計表_Click(object sender, EventArgs e)
        {
            string exeDir = System.IO.Directory.GetCurrentDirectory();

            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                DateTime date = new DateTime((int)comboBox年.SelectedItem, Int32.Parse(comboBox月.SelectedItem as string), 1);

                string templateFilePath = exeDir + "\\Template\\Template_科目別集計表.xlsx";
                new 科目別集計表出力(templateFilePath, saveFileDialog2.FileName).CreateFile(NBaseCommon.Common.LoginUser, date, 同期Client.LOGIN_VESSEL.MsVesselID);

                MessageBox.Show(this, "ファイルに出力しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void button検索_Click(object sender, EventArgs e)
        {
            Search送金();
            Search船内準備金();
        }
    }
}
