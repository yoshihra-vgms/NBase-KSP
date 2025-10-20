using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseUtil;
using Yojitsu.DA;
using Yojitsu.util;

namespace Yojitsu
{
    public partial class ドルレートForm : Form
    {
        private NenjiForm nenjiForm;
        private BgYosanHead yosanHead;
        private TreeListViewDelegateドルレート treeListViewDelegate;

        private bool canEdit;


        public ドルレートForm(NenjiForm nenjiForm, BgYosanHead yosanHead, bool canEdit)
        {
            this.nenjiForm = nenjiForm;
            this.yosanHead = yosanHead;
            this.canEdit = canEdit;

            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("", "ドルレート編集閲覧", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            Init(yosanHead);

        }

        private void Init(BgYosanHead yosanHead)
        {
            labelYosanHead.Text = yosanHead.Year + "年度 [" + BgYosanSbt.ToName(yosanHead.YosanSbtID) + "予算" +
                     " Rev." + yosanHead.Revision + "] ";
            
            treeListViewDelegate = new TreeListViewDelegateドルレート(treeListView1);
            treeListViewDelegate.CreateTable(yosanHead, canEdit);
        }


        private void button保存_Click(object sender, EventArgs e)
        {
            Save();
        }


        private void Save()
        {
            bool result = DbAccessorFactory.FACTORY.BgRate_UpdateRecords(NBaseCommon.Common.LoginUser,
                                                                treeListViewDelegate.GetEditedBgRates());

            if (!result)
            {
                MessageBox.Show("既にデータが更新されているので保存できませんでした。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                nenjiForm.Set換算レート(treeListViewDelegate.GetEditedBgRates());
                MessageBox.Show("ドルレートデータを保存しました", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


        private void ドルレートForm_Load(object sender, EventArgs e)
        {
            button保存.Visible = canEdit;

            if (!canEdit)
            {
                button閉じる.Location = new Point(107, 390);
            }
        }


        private void ドルレートForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.Cancel && treeListViewDelegate.GetEditedBgRates().Count > 0)
            {
                DialogResult result = MessageBox.Show("換算レートが変更されています。保存しますか？",
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
            
            DbTableCache.instance().ClearBgRateDic();
        }
    }
}
