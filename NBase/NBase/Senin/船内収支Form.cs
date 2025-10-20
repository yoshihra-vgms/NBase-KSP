using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Senin.util;
using LidorSystems.IntegralUI.Lists;
using NBaseUtil;
using System.IO;
using NBaseData.DS;

namespace Senin
{
    public partial class 船内収支Form : Form
    {
        private static 船内収支Form instance;

        private TreeListViewDelegate船内収支 treeListViewDelegate;


        private 船内収支Form()
        {
            InitializeComponent();
            Init();
        }


        public static 船内収支Form Instance()
        {
            if (instance == null)
            {
                instance = new 船内収支Form();
            }

            return instance;
        }


        private void Init()
        {
            InitComboBox船();
            InitComboBox年();
            InitComboBox月();

            treeListViewDelegate = new TreeListViewDelegate船内収支(treeListView1);

            Search船内準備金();
        }



        private void InitComboBox船()
        {
            //m.yoshihara 2017/6/1
            //foreach (MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            //{
            //    comboBox船.Items.Add(v);
            //}

            ////m.yoshihara 2017/6/1
            //foreach (MsVessel v in SeninTableCache.instance().GetMsVesselListBySeninEnabled(NBaseCommon.Common.LoginUser))
            foreach (MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            {
                comboBox船.Items.Add(v);
            }
            comboBox船.SelectedIndex = 0;
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


        private void treeListView1_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                if (me.Y <= 16)
                {
                    return;
                }

                TreeListViewNode selected = treeListView1.GetNodeAt(me.X, me.Y);

                if (selected != null)
                {
                    SiJunbikin s = selected.Tag as SiJunbikin;

                    if (s != null)
                    {
                        船内収支詳細Form form = new 船内収支詳細Form(this, comboBox船.SelectedItem as MsVessel, s);
                        form.ShowDialog();
                    }
                }
            }
        }
        

        private void button明細追加_Click(object sender, EventArgs e)
        {
            if (comboBox船.SelectedItem == null)
            {
                MessageBox.Show("船を選択してください。",
                                                   "確認", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            船内収支詳細Form form = new 船内収支詳細Form(this, comboBox船.SelectedItem as MsVessel);
            form.ShowDialog();
        }

        
        internal void Search船内準備金()
        {
            if (comboBox船.SelectedItem != null)
            {
                DateTime 先月締め日;
                decimal 先月末残高 = 0;
                List<SiJunbikin> result;
                DateTime date = new DateTime((int)comboBox年.SelectedItem, Int32.Parse(comboBox月.SelectedItem as string), 1);
                int msVesselId = (comboBox船.SelectedItem as MsVessel).MsVesselID;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    先月締め日 = serviceClient.SiGetsujiShimeBi_ToFrom_船員_月次締日(NBaseCommon.Common.LoginUser, date).AddDays(-1);
                    先月末残高 = serviceClient.SiJunbikin_Get_先月末残高(NBaseCommon.Common.LoginUser, date, msVesselId);
                    result = serviceClient.SiJunbikin_GetRecordsByDateAndMsVesselID(NBaseCommon.Common.LoginUser, date, msVesselId);
                }

                treeListViewDelegate.SetRows(DateTimeUtils.ToFromMonth(date), 先月締め日, 先月末残高, result);

                textBox受入金額.Text = NBaseCommon.Common.金額出力(treeListViewDelegate.Get_受入金額合計());
                textBox支払金額.Text = NBaseCommon.Common.金額出力(treeListViewDelegate.Get_支払金額合計());
                textBox繰越金額.Text = NBaseCommon.Common.金額出力(treeListViewDelegate.Get_受入金額合計() - treeListViewDelegate.Get_支払金額合計());
            }
        }

        
        private void 船内準備金Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }


        private void button船内収支報告書_Click(object sender, EventArgs e)
        {
            年月指定出力Form form = new 年月指定出力Form(年月指定出力Form.帳票種別.船内収支報告書);
            form.ShowDialog();
        }


        private void button科目別集計表_Click(object sender, EventArgs e)
        {
            年月指定出力Form form = new 年月指定出力Form(年月指定出力Form.帳票種別.科目別集計表);
            form.ShowDialog();
        }


        private void button検索_Click(object sender, EventArgs e)
        {
            Search船内準備金();
        }

        /// <summary>
        /// 船選択時 2017/5/17 m.yoshihara miho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox船_SelectedIndexChanged(object sender, EventArgs e)
        {
            MsVessel v = (MsVessel)((ComboBox)sender).SelectedItem;

            if (v == null) return;

            if (v.SeninEnabled == 1)
            {
                button明細追加.Enabled = true;
            }
            else 
            {
                button明細追加.Enabled = false;
            }
        }



        private void button船用金送金_Click(object sender, EventArgs e)
        {
            船用金送金Form form = 船用金送金Form.Instance();
            form.MdiParent = this.MdiParent;
            form.Show();
            form.WindowState = FormWindowState.Normal;
            form.Activate();
        }
    }
}
