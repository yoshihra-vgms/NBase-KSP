using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LidorSystems.IntegralUI.Lists;
using LidorSystems.IntegralUI.Lists.Collections;
using NBaseData.DAC;
using Senin.util;
using NBaseUtil;
using System.IO;
using NBaseData.DS;

namespace Senin
{
    public partial class 船用金送金Form : Form
    {
        private static 船用金送金Form instance;

        private TreeListViewDelegate船用金送金 treeListViewDelegate;


        private 船用金送金Form()
        {
            InitializeComponent();
            Init();
        }


        public static 船用金送金Form Instance()
        {
            if (instance == null)
            {
                instance = new 船用金送金Form();
            }
            
            return instance;
        }


        private void Init()
        {
            InitComboBox船();
            InitComboBox年();
            InitComboBox月();

            treeListViewDelegate = new TreeListViewDelegate船用金送金(treeListView1);

            Search船用金送金();
        }


        private void InitComboBox船()
        {
            comboBox船.Items.Add(string.Empty);

            foreach (MsVessel v in SeninTableCache.instance().GetMsVesselList(NBaseCommon.Common.LoginUser))
            //foreach (MsVessel v in SeninTableCache.instance().GetMsVesselListBySeninEnabled(NBaseCommon.Common.LoginUser))//m.yoshihara 2017/6/1
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
                comboBox開始年.Items.Add(thisYear - i);
                comboBox終了年.Items.Add(thisYear - i);
            }

            comboBox開始年.SelectedItem = thisYear;
            comboBox終了年.SelectedItem = thisYear;
        }


        private void InitComboBox月()
        {
            for (int i = 0; i < 12; i++)
            {
                string m = (i + 1).ToString();

                comboBox開始月.Items.Add(m);
                comboBox終了月.Items.Add(m);

                if (m.Trim() == DateTime.Now.Month.ToString())
                {
                    comboBox開始月.SelectedItem = m;
                    comboBox終了月.SelectedItem = m;
                }
            }
        }

        
        private void button新規送金_Click(object sender, EventArgs e)
        {
            船用金送金詳細Form form = new 船用金送金詳細Form(this);
            form.ShowDialog();
        }


        internal void Search船用金送金()
        {
            List<SiSoukin> result;
            
            DateTime start = new DateTime((int)comboBox開始年.SelectedItem, Int32.Parse(comboBox開始月.SelectedItem as string), 1);
            DateTime end = new DateTime((int)comboBox終了年.SelectedItem, Int32.Parse(comboBox終了月.SelectedItem as string), 1);
            int msVesselId = int.MinValue;

            if (comboBox船.SelectedIndex > 0)
            {
                msVesselId = (comboBox船.SelectedItem as MsVessel).MsVesselID;
            }

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.SiSoukin_GetRecordsByDateAndMsVesselID(NBaseCommon.Common.LoginUser, start, end, msVesselId);
            }

            treeListViewDelegate.SetRows(result);
        }


        private void button検索_Click(object sender, EventArgs e)
        {
            Search船用金送金();
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
                    SiSoukin s = selected.Tag as SiSoukin;

                    船用金送金詳細Form form = new 船用金送金詳細Form(this, s);
                    form.ShowDialog();
                }
            }
        }


        private void buttonクリア_Click(object sender, EventArgs e)
        {
            comboBox船.SelectedIndex = 0;

            comboBox開始年.Items.Clear();
            comboBox開始月.Items.Clear();
            comboBox終了年.Items.Clear();
            comboBox終了月.Items.Clear();
            InitComboBox年();
            InitComboBox月();

            Search船用金送金();
        }

        
        private void 船用金送金Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            instance = null;
        }


        private void button船用金送金表_Click(object sender, EventArgs e)
        {
            年月指定出力Form form = new 年月指定出力Form(年月指定出力Form.帳票種別.船用金送金表);
            form.ShowDialog();
        }
    }
}
