using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hachu.Reports;
using Hachu.HachuManage;
using NBaseData.DS;
using NBaseCommon;


namespace Hachu
{
    public partial class MainForm : ExForm
    {
        /// <summary>
        /// インスタンスを格納するフィールド変数
        /// </summary>
        private static MainForm instance = null;

        private HachuManage.発注状況一覧Form 発注一覧Form = null;
        private HachuManage.承認一覧Form 承認一覧Form = null;
        private Chozo.貯蔵品Form 貯蔵品Form = null;
        //private HachuManage.概算計上一覧Form 概算計上一覧Form = null;
        //private HachuManage.振替取立一覧Form 振替取立一覧Form = null;

        private HachuManage.年度変更一覧Form 年度変更一覧Form = null;
        private HachuManage.日付調整一覧Form 日付調整一覧Form = null;
        private HachuManage.船種別変更一覧Form 船種別変更一覧Form = null;

        /// <summary>
        /// インスタンスを取得するためのメソッド
        /// Windows フォームをシングルトン対応にするコード
        /// </summary>
        /// <returns></returns>
        public static MainForm GetInstance()
        {
            if (instance == null)
            {
                instance = new MainForm();
            }
            return instance;
        }

        private MainForm()
        {
            InitializeComponent();
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "発注管理", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            EnableComponents();
        }

        private void EnableComponents()
        {
            // "表示" メニュー
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "発注状況一覧", "発注状況一覧"))
            {
                発注一覧ToolStripMenuItem.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "承認一覧", null))
            {
                承認一覧ToolStripMenuItem.Enabled = true;
            }
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "貯蔵品", null))
            {
                貯蔵品ToolStripMenuItem.Enabled = true;
            }

            //if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "取立・振替項目編集", null))
            //{
            //    取立振替項目編集ToolStripMenuItem.Enabled = true;
            //}
            //if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "概算計上一覧", "概算データ一覧"))
            //{
            //    概算計上ToolStripMenuItem.Enabled = true;
            //}


            // "管理表" メニュー
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "部品購入管理表", null))
            {
                部品購入管理表ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "入渠管理表", null))
            {
                入渠管理表ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "船用品管理表", null))
            {
                船用品管理表ToolStripMenuItem.Enabled = true;
            }

            #region 潤滑油
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "潤滑油管理表", null))
            {
                潤滑油管理表ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "潤滑油年間管理表", null))
            {
                潤滑油年間管理表ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "潤滑油費集計表", null))
            {
                潤滑油集計表ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "潤滑油貯蔵品リスト", null))
            {
                潤滑油貯蔵品リストToolStripMenuItem.Enabled = true;
            }
            #endregion

            #region ペイント
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "船用品（ペイント）管理表", null))
            {
                船用品_ペイント_管理表ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "船用品（ペイント）年間管理表", null))
            {
                船用品_ペイント_年間管理表ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "船用品（ペイント）集計表", null))
            {
                船用品_ペイント_集計表ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "船用品（ペイント）貯蔵品リスト", null))
            {
                船用品_ペイント_貯蔵品リストToolStripMenuItem.Enabled = true;
            }
            #endregion
            船用品_ペイント_管理表ToolStripMenuItem.Visible = false;
            船用品_ペイント_年間管理表ToolStripMenuItem.Visible = false;
            船用品_ペイント_集計表ToolStripMenuItem.Visible = false;
            船用品_ペイント_貯蔵品リストToolStripMenuItem.Visible = false;

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "データ出力", null))
            {
                データ出力ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "業者別支払実績", null))
            {
                業者別支払実績ToolStripMenuItem.Enabled = true;
            }


            // "データ修正" メニュー

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "データ修正", "年度跨ぎ調整"))
            {
                年度跨ぎ調整ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "データ修正", "日付調整"))
            {
                日付調整ToolStripMenuItem.Enabled = true;
            }

            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "データ修正", "船／種別変更"))
            {
                船種別変更ToolStripMenuItem.Enabled = true;
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            // 
            if (MsRoleTableCache.instance().Enabled(NBaseCommon.Common.LoginUser, "発注管理", "発注状況一覧", "発注状況一覧"))
            {
                発注一覧Formを開く();
            }
            this.ChangeParentFormSize(FormWindowState.Minimized);
            this.Activate();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }

            this.ChangeParentFormSize(FormWindowState.Normal);

            instance = null;
        }

        //==========================================
        // 表示メニュ
        //==========================================
        #region
        private void 発注一覧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            発注一覧Formを開く();
        }

        private void 承認一覧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 2011.01.24
            //if (NBaseCommon.Common.LoginUser.AdminFlag != (int)NBaseData.DAC.MsUser.ADMIN_FLAG.管理者)
            //{
            //    MessageBox.Show("管理者のみ使用できます。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            貯蔵品Formの全画面表示を解除する();

            承認一覧Formを開く();
        }

        //private void 概算計上ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    貯蔵品Formの全画面表示を解除する();

        //    概算計上一覧Formを開く();
        //}

        private void 貯蔵品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.貯蔵品フォーム作成();
        }

        //private void 取立振替項目編集ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    貯蔵品Formの全画面表示を解除する();

        //    振替取立一覧Formを開く();
        //}


        private void 発注一覧Formを開く()
        {
            発注一覧Form = HachuManage.発注状況一覧Form.GetInstance();
            発注一覧Form.MdiParent = this;
            発注一覧Form.Show();
            発注一覧Form.Location = new Point(0, 0);
            発注一覧Form.Activate();
        }

        private void 承認一覧Formを開く()
        {
            承認一覧Form = HachuManage.承認一覧Form.GetInstance();
            承認一覧Form.MdiParent = this;
            承認一覧Form.Show();
            承認一覧Form.Activate();
        }

        //private void 概算計上一覧Formを開く()
        //{
        //    概算計上一覧Form = HachuManage.概算計上一覧Form.GetInstance();
        //    概算計上一覧Form.MdiParent = this;
        //    概算計上一覧Form.Show();
        //    概算計上一覧Form.Activate();
        //}

        private void 貯蔵品フォーム作成()
        {
            this.貯蔵品Form = Chozo.貯蔵品Form.GetInstance();
            this.貯蔵品Form.MdiParent = this;
            this.貯蔵品Form.Show();
            this.貯蔵品Form.Activate();
        }


        //private void 振替取立一覧Formを開く()
        //{
        //    振替取立一覧Form = HachuManage.振替取立一覧Form.GetInstance();
        //    振替取立一覧Form.MdiParent = this;
        //    振替取立一覧Form.Show();
        //    振替取立一覧Form.Activate();
        //}

        #endregion

        //==========================================
        // 管理表メニュ
        //==========================================
        #region
        private void 部品購入管理表ToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            管理表Form form = new 管理表Form(new Dataランニング("番号不明", "部品購入管理表"));
            form.ShowDialog();
        }

        private void 入渠管理表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            管理表Form form = new 管理表Form(new Data入渠("番号不明", "入渠管理表"));
            form.ShowDialog();
        }

        private void 船用品管理表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            管理表Form form = new 管理表Form(new Data船用品("番号不明", "船用品管理表"));
            form.ShowDialog();
        }

        //==========================================
        private void 潤滑油管理表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            貯蔵品管理表Form form = new 貯蔵品管理表Form(new Data貯蔵品("番号不明", "潤滑油 貯蔵品管理表", NBaseData.BLC.貯蔵品リスト.対象Enum.潤滑油));
            form.ShowDialog();
        }

        private void 潤滑油年間管理表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            貯蔵品年間管理表Form form = new 貯蔵品年間管理表Form(new Data貯蔵品年間管理表("番号不明", "潤滑油 貯蔵品年間管理表", NBaseData.BLC.貯蔵品リスト.対象Enum.潤滑油));
            form.ShowDialog();
        }
        private void 潤滑油集計表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            集計表Form form = new 集計表Form(new Data潤滑油集計("番号不明", "潤滑油費集計"));
            form.ShowDialog();
        }

        private void 潤滑油貯蔵品リストToolStripMenuItem_Click(object sender, EventArgs e)
        {
            貯蔵品リストForm form = new 貯蔵品リストForm("番号不明", "潤滑油 貯蔵品リスト", NBaseData.BLC.貯蔵品リスト.対象Enum.潤滑油);
            form.ShowDialog();
        }

        //==========================================

        private void 船用品_ペイント_管理表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            貯蔵品管理表Form form = new 貯蔵品管理表Form(new Data貯蔵品("番号不明", "船用品(ペイント) 貯蔵品管理表", NBaseData.BLC.貯蔵品リスト.対象Enum.船用品));
            form.ShowDialog();
        }

        private void 船用品_ペイント_年間管理表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            貯蔵品年間管理表Form form = new 貯蔵品年間管理表Form(new Data貯蔵品年間管理表("番号不明", "船用品(ペイント) 貯蔵品年間管理表", NBaseData.BLC.貯蔵品リスト.対象Enum.船用品));
            form.ShowDialog();
        }

        private void 船用品_ペイント_集計表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            集計表Form form = new 集計表Form(new Data船用品集計("番号不明", "船用品(ペイント)費集計"));
            form.ShowDialog();
        }

        private void 船用品_ペイント_貯蔵品リストToolStripMenuItem_Click(object sender, EventArgs e)
        {
            貯蔵品リストForm form = new 貯蔵品リストForm("番号不明", "船用品(ペイント) 貯蔵品リスト", NBaseData.BLC.貯蔵品リスト.対象Enum.船用品);
            form.ShowDialog();
        }

        //==========================================
        private void データ出力ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            データ出力Form form = new データ出力Form();
            form.ShowDialog();
        }

        private void 業者別支払実績ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            業者別支払実績出力Form form = new 業者別支払実績出力Form();
            form.ShowDialog();
        }
        #endregion



        //==========================================
        // ウィンドウメニュ
        //==========================================
        #region
        private void 整列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }
        #endregion



        private void 貯蔵品Formの全画面表示を解除する()
        {
            if (貯蔵品Form != null)
            {
                貯蔵品Form.WindowState = FormWindowState.Normal;
            }
        }

        private void 年度跨ぎ調整ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            貯蔵品Formの全画面表示を解除する();

            年度変更一覧Formを開く();
        }

        private void 年度変更一覧Formを開く()
        {
            年度変更一覧Form = HachuManage.年度変更一覧Form.GetInstance();
            年度変更一覧Form.MdiParent = this;
            年度変更一覧Form.Show();
            年度変更一覧Form.Activate();
        }

        private void 日付調整ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            貯蔵品Formの全画面表示を解除する();

            日付調整一覧Formを開く();
        }

        private void 日付調整一覧Formを開く()
        {
            日付調整一覧Form = HachuManage.日付調整一覧Form.GetInstance();
            日付調整一覧Form.MdiParent = this;
            日付調整一覧Form.Show();
            日付調整一覧Form.Activate();
        }

        private void 船種別変更ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            船種別変更一覧Form = HachuManage.船種別変更一覧Form.GetInstance();
            船種別変更一覧Form.MdiParent = this;
            船種別変更一覧Form.Show();
            船種別変更一覧Form.Location = new Point(0, 0);
            船種別変更一覧Form.Activate();
        }
    }
}
