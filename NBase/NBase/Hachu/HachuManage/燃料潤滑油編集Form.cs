using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hachu.Utils;
using Hachu.Models;

namespace Hachu.HachuManage
{
    public partial class 燃料潤滑油編集Form : Form
    {
        private readonly 燃料潤滑油編集FormController formController;

        public 燃料潤滑油編集Form(List<Item手配依頼品目> 品目s)
        {
            InitializeComponent();
            formController = new 燃料潤滑油編集FormController手配依頼(this, 品目s);
        }

        public 燃料潤滑油編集Form(List<Item見積回答品目> 品目s)
        {
            InitializeComponent();
            formController = new 燃料潤滑油編集FormController見積回答(this, 品目s);
        }

        public 燃料潤滑油編集Form(List<Item受領品目> 品目s)
        {
            InitializeComponent();
            formController = new 燃料潤滑油編集FormController受領(this, 品目s);
        }

        public 燃料潤滑油編集Form(List<Item支払品目> 品目s)
        {
            InitializeComponent();
            formController = new 燃料潤滑油編集FormController支払(this, 品目s);
        }

        private void 燃料潤滑油編集Form_Load(object sender, EventArgs e)
        {
            formController.InitializeForm();
        }

        /// <summary>
        /// 「登録」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button登録_Click(object sender, EventArgs e)
        private void button登録_Click(object sender, EventArgs e)
        {
            // Formを閉じる
            DialogResult = DialogResult.OK;
            Close();
        }
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button閉じる_Click(object sender, EventArgs e)
        private void button閉じる_Click(object sender, EventArgs e)
        {
            // Formを閉じる
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion


        abstract private class 燃料潤滑油編集FormController
        {
            abstract public void InitializeForm();
       }

        private class 燃料潤滑油編集FormController手配依頼 : 燃料潤滑油編集FormController
        {
            燃料潤滑油編集Form form = null;
            ItemTreeListView手配依頼 品目TreeList = null;
            private List<Item手配依頼品目> 品目s = null;

            public 燃料潤滑油編集FormController手配依頼(燃料潤滑油編集Form form, List<Item手配依頼品目> 品目s)
            {
                this.form = form;
                this.品目s = 品目s;
            }

            public override void InitializeForm()
            {
                品目TreeList = new ItemTreeListView手配依頼(NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID, form.treeListView, false);
                品目TreeList.Enum表示方式 = ItemTreeListView手配依頼.表示方式enum.Zeroを表示;
                品目TreeList.Enabled = true;

                if (品目TreeList != null)
                    品目TreeList.Clear();

                object[,] columns = new object[,] {
                                               {"No", 65, null, HorizontalAlignment.Right},
                                               {"仕様・型式 /　詳細品目", 275, null, null},
                                               {"単位", 45, null, null},
                                               {"数量", 50, null, HorizontalAlignment.Right},
                                             };


                品目TreeList.SetColumns(0, columns);

                品目TreeList.AddNodes(false, 品目s);

            }
        }

        private class 燃料潤滑油編集FormController見積回答 : 燃料潤滑油編集FormController
        {
            燃料潤滑油編集Form form = null;
            ItemTreeListView見積回答 品目TreeList = null;
            private List<Item見積回答品目> 品目s = null;

            public 燃料潤滑油編集FormController見積回答(燃料潤滑油編集Form form, List<Item見積回答品目> 品目s)
            {
                this.form = form;
                this.品目s = 品目s;
            }

            public override void InitializeForm()
            {
                品目TreeList = new ItemTreeListView見積回答(NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID, form.treeListView);
                品目TreeList.Enum表示方式 = ItemTreeListView見積回答.表示方式enum.Zeroを表示;
                品目TreeList.Enabled = true;

                if (品目TreeList != null)
                    品目TreeList.Clear();

                object[,] columns = new object[,] {
                                               {"No", 65, null, HorizontalAlignment.Right},
                                               {"仕様・型式 /　詳細品目", 275, null, null},
                                               {"単位", 45, null, null},
                                               {"数量", 50, null, HorizontalAlignment.Right},
                                             };


                品目TreeList.SetColumns(0, columns);

                品目TreeList.AddNodes(false, 品目s);

            }
        }

        private class 燃料潤滑油編集FormController受領 : 燃料潤滑油編集FormController
        {
            燃料潤滑油編集Form form = null;
            ItemTreeListView受領 品目TreeList = null;
            private List<Item受領品目> 品目s = null;

            public 燃料潤滑油編集FormController受領(燃料潤滑油編集Form form, List<Item受領品目> 品目s)
            {
                this.form = form;
                this.品目s = 品目s;
            }

            public override void InitializeForm()
            {
                品目TreeList = new ItemTreeListView受領(NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID, form.treeListView);
                品目TreeList.Enum表示方式 = ItemTreeListView受領.表示方式enum.Zeroを表示;
                品目TreeList.Enabled = true;

                if (品目TreeList != null)
                    品目TreeList.Clear();

                object[,] columns = new object[,] {
                                               {"No", 65, null, HorizontalAlignment.Right},
                                               {"仕様・型式 /　詳細品目", 275, null, null},
                                               {"単位", 45, null, null},
                                               {"数量", 50, null, HorizontalAlignment.Right},
                                             };


                品目TreeList.SetColumns(0, columns);

                品目TreeList.AddNodes(false, 品目s);

            }
        }

        private class 燃料潤滑油編集FormController支払 : 燃料潤滑油編集FormController
        {
            燃料潤滑油編集Form form = null;
            ItemTreeListView支払 品目TreeList = null;
            private List<Item支払品目> 品目s = null;

            public 燃料潤滑油編集FormController支払(燃料潤滑油編集Form form, List<Item支払品目> 品目s)
            {
                this.form = form;
                this.品目s = 品目s;
            }

            public override void InitializeForm()
            {
                品目TreeList = new ItemTreeListView支払(NBaseCommon.Common.MsThiIraiSbt_燃料潤滑油ID, form.treeListView);
                品目TreeList.Enum表示方式 = ItemTreeListView支払.表示方式enum.Zeroを表示;
                品目TreeList.Enabled = true;

                if (品目TreeList != null)
                    品目TreeList.Clear();

                object[,] columns = new object[,] {
                                               {"No", 65, null, HorizontalAlignment.Right},
                                               {"仕様・型式 /　詳細品目", 275, null, null},
                                               {"単位", 45, null, null},
                                               {"数量", 50, null, HorizontalAlignment.Right},
                                             };


                品目TreeList.SetColumns(0, columns);

                品目TreeList.AddNodes(false, 品目s);

            }
        }
    }
}
