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
using NBaseHonsen.Senin.util;
using LidorSystems.IntegralUI.Lists;
using NBaseData.DS;
using NBaseData.BLC;

namespace NBaseHonsen.Senin
{
    public partial class 乗船Form : Form
    {
        private string seninName_下船;
        private TreeListViewDelegate乗船 treeListViewDelegate;


        public 乗船Form()
            : this(null)
        {
        }


        public 乗船Form(string seninName_下船)
        {
            this.seninName_下船 = seninName_下船;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox職名();

            treeListViewDelegate = new TreeListViewDelegate乗船(treeListView1);
        }


        private void InitComboBox職名()
        {
            comboBox職名.Items.Add(string.Empty);

            foreach (MsSiShokumei s in SeninTableCache.instance().GetMsSiShokumeiList(NBaseCommon.Common.LoginUser))
            {
                comboBox職名.Items.Add(s);
            }

            comboBox職名.SelectedIndex = 0;
        }


        private void treeListView1_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                TreeListViewNode selected = treeListView1.GetNodeAt(me.X, me.Y);

                if (selected != null)
                {
                    MsSenin s = selected.Tag as MsSenin;

                    MsSenin senin = MsSenin.GetRecord(NBaseCommon.Common.LoginUser, s.MsSeninID);

                    船員詳細Form form = new 船員詳細Form(senin, true, seninName_下船);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        Search();
                    }
                }
            }
        }

        
        private void button検索_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            List<MsSenin> result = 船員.BLC_船員検索(同期Client.LOGIN_USER, SeninTableCache.instance(), CreateMsSeninFilter());
            result = Refine_乗船対象(result);
            treeListViewDelegate.SetRows(result);
        }


        private List<MsSenin> Refine_乗船対象(List<MsSenin> senins)
        {
            List<MsSenin> result = new List<MsSenin>();

            HashSet<int> 他種別でアサイン中_SeninIds = new HashSet<int>();

            foreach (MsSenin c in senins)
            {
                if (c.EndDate != DateTime.MinValue)
                {
                    他種別でアサイン中_SeninIds.Add(c.MsSeninID);
                }
            }

            foreach (MsSenin c in senins)
            {
                if (他種別でアサイン中_SeninIds.Contains(c.MsSeninID))
                {
                    continue;
                }

                result.Add(c);
            }

            return result;
        }


        private MsSeninFilter CreateMsSeninFilter()
        {
            MsSeninFilter filter = new MsSeninFilter();

            if (textBox氏名.Text.Length > 0)
            {
                filter.Name = textBox氏名.Text;
            }

            if (textBox氏名カナ.Text.Length > 0)
            {
                filter.NameKana = textBox氏名カナ.Text;
            }

            if (comboBox職名.SelectedItem is MsSiShokumei)
            {
                filter.MsSiShokumeiID = (comboBox職名.SelectedItem as MsSiShokumei).MsSiShokumeiID;
            }

            if (checkBox社員.Checked)
            {
                filter.Kubuns.Add(0);
            }

            if (checkBox派遣.Checked)
            {
                filter.Kubuns.Add(1);
            }

            if (!checkBox社員.Checked && !checkBox派遣.Checked)
            {
                filter.Kubuns.Add(-1);
            }

            filter.joinSiCard = MsSeninFilter.JoinSiCard.LEFT_JOIN;
            filter.種別無し = true;

            foreach (MsSiShubetsu s in SeninTableCache.instance().GetMsSiShubetsuList(NBaseCommon.Common.LoginUser))
            {
                if (!SeninTableCache.instance().Is_休暇管理(NBaseCommon.Common.LoginUser, s.MsSiShubetsuID) && 
                    !SeninTableCache.instance().Is_乗船中(NBaseCommon.Common.LoginUser, s.MsSiShubetsuID))
                {
                    filter.MsSiShubetsuIDs.Add(s.MsSiShubetsuID);
                }
            }

            filter.RetireFlag = 0;

            return filter;
        }
    }
}
