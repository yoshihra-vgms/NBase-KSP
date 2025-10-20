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
using NBaseCommon.Senin.Excel;
using NBaseData.BLC;

namespace NBaseHonsen.Document
{
    public partial class 船員エラーForm : Form
    {
        private TreeListViewDelegate船員2 treeListViewDelegate;
        private List<SiCard> cards = new List<SiCard>();

        public 船員エラーForm(List<SiCard> cards)
        {
            InitializeComponent();

            this.cards = cards;
            Init();
        }

        
        private void Init()
        {
            //treeListView1.CheckBoxes = true;
            treeListViewDelegate = new TreeListViewDelegate船員2(treeListView1);
            treeListViewDelegate.SetRows(cards);
        }

        private void button確認_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
