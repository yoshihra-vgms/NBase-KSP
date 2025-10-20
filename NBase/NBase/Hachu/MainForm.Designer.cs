namespace Hachu
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.表示VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.発注一覧ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.承認一覧ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.貯蔵品ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.管理表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.部品購入管理表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.入渠管理表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.船用品管理表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.潤滑油管理表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.潤滑油年間管理表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.潤滑油集計表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.潤滑油貯蔵品リストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.船用品_ペイント_管理表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.船用品_ペイント_年間管理表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.船用品_ペイント_集計表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.船用品_ペイント_貯蔵品リストToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.データ出力ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.業者別支払実績ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.データ修正ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.年度跨ぎ調整ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.日付調整ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.船種別変更ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ウィンドウToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.整列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.表示VToolStripMenuItem,
            this.管理表ToolStripMenuItem,
            this.データ修正ToolStripMenuItem,
            this.ウィンドウToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.ウィンドウToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(292, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 表示VToolStripMenuItem
            // 
            this.表示VToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.発注一覧ToolStripMenuItem,
            this.承認一覧ToolStripMenuItem,
            this.貯蔵品ToolStripMenuItem});
            this.表示VToolStripMenuItem.Name = "表示VToolStripMenuItem";
            this.表示VToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.表示VToolStripMenuItem.Text = "表示";
            // 
            // 発注一覧ToolStripMenuItem
            // 
            this.発注一覧ToolStripMenuItem.Enabled = false;
            this.発注一覧ToolStripMenuItem.Name = "発注一覧ToolStripMenuItem";
            this.発注一覧ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.発注一覧ToolStripMenuItem.Text = "発注状況一覧";
            this.発注一覧ToolStripMenuItem.Click += new System.EventHandler(this.発注一覧ToolStripMenuItem_Click);
            // 
            // 承認一覧ToolStripMenuItem
            // 
            this.承認一覧ToolStripMenuItem.Enabled = false;
            this.承認一覧ToolStripMenuItem.Name = "承認一覧ToolStripMenuItem";
            this.承認一覧ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.承認一覧ToolStripMenuItem.Text = "承認一覧";
            this.承認一覧ToolStripMenuItem.Click += new System.EventHandler(this.承認一覧ToolStripMenuItem_Click);
            // 
            // 貯蔵品ToolStripMenuItem
            // 
            this.貯蔵品ToolStripMenuItem.Enabled = false;
            this.貯蔵品ToolStripMenuItem.Name = "貯蔵品ToolStripMenuItem";
            this.貯蔵品ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.貯蔵品ToolStripMenuItem.Text = "貯蔵品";
            this.貯蔵品ToolStripMenuItem.Click += new System.EventHandler(this.貯蔵品ToolStripMenuItem_Click);
            // 
            // 管理表ToolStripMenuItem
            // 
            this.管理表ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.部品購入管理表ToolStripMenuItem,
            this.入渠管理表ToolStripMenuItem,
            this.船用品管理表ToolStripMenuItem,
            this.toolStripSeparator1,
            this.潤滑油管理表ToolStripMenuItem,
            this.潤滑油年間管理表ToolStripMenuItem,
            this.潤滑油集計表ToolStripMenuItem,
            this.潤滑油貯蔵品リストToolStripMenuItem,
            this.toolStripSeparator2,
            this.船用品_ペイント_管理表ToolStripMenuItem,
            this.船用品_ペイント_年間管理表ToolStripMenuItem,
            this.船用品_ペイント_集計表ToolStripMenuItem,
            this.船用品_ペイント_貯蔵品リストToolStripMenuItem,
            this.toolStripSeparator3,
            this.データ出力ToolStripMenuItem,
            this.toolStripSeparator4,
            this.業者別支払実績ToolStripMenuItem});
            this.管理表ToolStripMenuItem.Name = "管理表ToolStripMenuItem";
            this.管理表ToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.管理表ToolStripMenuItem.Text = "管理表";
            // 
            // 部品購入管理表ToolStripMenuItem
            // 
            this.部品購入管理表ToolStripMenuItem.Enabled = false;
            this.部品購入管理表ToolStripMenuItem.Name = "部品購入管理表ToolStripMenuItem";
            this.部品購入管理表ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.部品購入管理表ToolStripMenuItem.Text = "部品購入管理表";
            this.部品購入管理表ToolStripMenuItem.Click += new System.EventHandler(this.部品購入管理表ToolStripMenuItem_Click);
            // 
            // 入渠管理表ToolStripMenuItem
            // 
            this.入渠管理表ToolStripMenuItem.Enabled = false;
            this.入渠管理表ToolStripMenuItem.Name = "入渠管理表ToolStripMenuItem";
            this.入渠管理表ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.入渠管理表ToolStripMenuItem.Text = "入渠管理表";
            this.入渠管理表ToolStripMenuItem.Click += new System.EventHandler(this.入渠管理表ToolStripMenuItem_Click);
            // 
            // 船用品管理表ToolStripMenuItem
            // 
            this.船用品管理表ToolStripMenuItem.Enabled = false;
            this.船用品管理表ToolStripMenuItem.Name = "船用品管理表ToolStripMenuItem";
            this.船用品管理表ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.船用品管理表ToolStripMenuItem.Text = "船用品管理表";
            this.船用品管理表ToolStripMenuItem.Click += new System.EventHandler(this.船用品管理表ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(231, 6);
            // 
            // 潤滑油管理表ToolStripMenuItem
            // 
            this.潤滑油管理表ToolStripMenuItem.Enabled = false;
            this.潤滑油管理表ToolStripMenuItem.Name = "潤滑油管理表ToolStripMenuItem";
            this.潤滑油管理表ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.潤滑油管理表ToolStripMenuItem.Text = "潤滑油 管理表";
            this.潤滑油管理表ToolStripMenuItem.Click += new System.EventHandler(this.潤滑油管理表ToolStripMenuItem_Click);
            // 
            // 潤滑油年間管理表ToolStripMenuItem
            // 
            this.潤滑油年間管理表ToolStripMenuItem.Enabled = false;
            this.潤滑油年間管理表ToolStripMenuItem.Name = "潤滑油年間管理表ToolStripMenuItem";
            this.潤滑油年間管理表ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.潤滑油年間管理表ToolStripMenuItem.Text = "潤滑油 年間管理表";
            this.潤滑油年間管理表ToolStripMenuItem.Visible = false;
            this.潤滑油年間管理表ToolStripMenuItem.Click += new System.EventHandler(this.潤滑油年間管理表ToolStripMenuItem_Click);
            // 
            // 潤滑油集計表ToolStripMenuItem
            // 
            this.潤滑油集計表ToolStripMenuItem.Enabled = false;
            this.潤滑油集計表ToolStripMenuItem.Name = "潤滑油集計表ToolStripMenuItem";
            this.潤滑油集計表ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.潤滑油集計表ToolStripMenuItem.Text = "潤滑油 集計表";
            this.潤滑油集計表ToolStripMenuItem.Visible = false;
            this.潤滑油集計表ToolStripMenuItem.Click += new System.EventHandler(this.潤滑油集計表ToolStripMenuItem_Click);
            // 
            // 潤滑油貯蔵品リストToolStripMenuItem
            // 
            this.潤滑油貯蔵品リストToolStripMenuItem.Enabled = false;
            this.潤滑油貯蔵品リストToolStripMenuItem.Name = "潤滑油貯蔵品リストToolStripMenuItem";
            this.潤滑油貯蔵品リストToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.潤滑油貯蔵品リストToolStripMenuItem.Text = "潤滑油 貯蔵品リスト";
            this.潤滑油貯蔵品リストToolStripMenuItem.Click += new System.EventHandler(this.潤滑油貯蔵品リストToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(231, 6);
            // 
            // 船用品_ペイント_管理表ToolStripMenuItem
            // 
            this.船用品_ペイント_管理表ToolStripMenuItem.Enabled = false;
            this.船用品_ペイント_管理表ToolStripMenuItem.Name = "船用品_ペイント_管理表ToolStripMenuItem";
            this.船用品_ペイント_管理表ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.船用品_ペイント_管理表ToolStripMenuItem.Text = "船用品（ペイント） 管理表";
            this.船用品_ペイント_管理表ToolStripMenuItem.Click += new System.EventHandler(this.船用品_ペイント_管理表ToolStripMenuItem_Click);
            // 
            // 船用品_ペイント_年間管理表ToolStripMenuItem
            // 
            this.船用品_ペイント_年間管理表ToolStripMenuItem.Enabled = false;
            this.船用品_ペイント_年間管理表ToolStripMenuItem.Name = "船用品_ペイント_年間管理表ToolStripMenuItem";
            this.船用品_ペイント_年間管理表ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.船用品_ペイント_年間管理表ToolStripMenuItem.Text = "船用品（ペイント） 年間管理表";
            this.船用品_ペイント_年間管理表ToolStripMenuItem.Visible = false;
            this.船用品_ペイント_年間管理表ToolStripMenuItem.Click += new System.EventHandler(this.船用品_ペイント_年間管理表ToolStripMenuItem_Click);
            // 
            // 船用品_ペイント_集計表ToolStripMenuItem
            // 
            this.船用品_ペイント_集計表ToolStripMenuItem.Enabled = false;
            this.船用品_ペイント_集計表ToolStripMenuItem.Name = "船用品_ペイント_集計表ToolStripMenuItem";
            this.船用品_ペイント_集計表ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.船用品_ペイント_集計表ToolStripMenuItem.Text = "船用品（ペイント） 集計表";
            this.船用品_ペイント_集計表ToolStripMenuItem.Visible = false;
            this.船用品_ペイント_集計表ToolStripMenuItem.Click += new System.EventHandler(this.船用品_ペイント_集計表ToolStripMenuItem_Click);
            // 
            // 船用品_ペイント_貯蔵品リストToolStripMenuItem
            // 
            this.船用品_ペイント_貯蔵品リストToolStripMenuItem.Enabled = false;
            this.船用品_ペイント_貯蔵品リストToolStripMenuItem.Name = "船用品_ペイント_貯蔵品リストToolStripMenuItem";
            this.船用品_ペイント_貯蔵品リストToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.船用品_ペイント_貯蔵品リストToolStripMenuItem.Text = "船用品（ペイント） 貯蔵品リスト";
            this.船用品_ペイント_貯蔵品リストToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.船用品_ペイント_貯蔵品リストToolStripMenuItem.Click += new System.EventHandler(this.船用品_ペイント_貯蔵品リストToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(231, 6);
            // 
            // データ出力ToolStripMenuItem
            // 
            this.データ出力ToolStripMenuItem.Enabled = false;
            this.データ出力ToolStripMenuItem.Name = "データ出力ToolStripMenuItem";
            this.データ出力ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.データ出力ToolStripMenuItem.Text = "データ出力";
            this.データ出力ToolStripMenuItem.Click += new System.EventHandler(this.データ出力ToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(231, 6);
            // 
            // 業者別支払実績ToolStripMenuItem
            // 
            this.業者別支払実績ToolStripMenuItem.Enabled = false;
            this.業者別支払実績ToolStripMenuItem.Name = "業者別支払実績ToolStripMenuItem";
            this.業者別支払実績ToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.業者別支払実績ToolStripMenuItem.Text = "業者別支払実績";
            this.業者別支払実績ToolStripMenuItem.Click += new System.EventHandler(this.業者別支払実績ToolStripMenuItem_Click);
            // 
            // データ修正ToolStripMenuItem
            // 
            this.データ修正ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.年度跨ぎ調整ToolStripMenuItem,
            this.日付調整ToolStripMenuItem,
            this.船種別変更ToolStripMenuItem});
            this.データ修正ToolStripMenuItem.Name = "データ修正ToolStripMenuItem";
            this.データ修正ToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.データ修正ToolStripMenuItem.Text = "データ修正";
            // 
            // 年度跨ぎ調整ToolStripMenuItem
            // 
            this.年度跨ぎ調整ToolStripMenuItem.Enabled = false;
            this.年度跨ぎ調整ToolStripMenuItem.Name = "年度跨ぎ調整ToolStripMenuItem";
            this.年度跨ぎ調整ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.年度跨ぎ調整ToolStripMenuItem.Text = "年度跨ぎ調整";
            this.年度跨ぎ調整ToolStripMenuItem.Click += new System.EventHandler(this.年度跨ぎ調整ToolStripMenuItem_Click);
            // 
            // 日付調整ToolStripMenuItem
            // 
            this.日付調整ToolStripMenuItem.Enabled = false;
            this.日付調整ToolStripMenuItem.Name = "日付調整ToolStripMenuItem";
            this.日付調整ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.日付調整ToolStripMenuItem.Text = "日付調整";
            this.日付調整ToolStripMenuItem.Click += new System.EventHandler(this.日付調整ToolStripMenuItem_Click);
            // 
            // 船種別変更ToolStripMenuItem
            // 
            this.船種別変更ToolStripMenuItem.Enabled = false;
            this.船種別変更ToolStripMenuItem.Name = "船種別変更ToolStripMenuItem";
            this.船種別変更ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.船種別変更ToolStripMenuItem.Text = "船／種別変更";
            this.船種別変更ToolStripMenuItem.Click += new System.EventHandler(this.船種別変更ToolStripMenuItem_Click);
            // 
            // ウィンドウToolStripMenuItem
            // 
            this.ウィンドウToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.整列ToolStripMenuItem});
            this.ウィンドウToolStripMenuItem.Name = "ウィンドウToolStripMenuItem";
            this.ウィンドウToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.ウィンドウToolStripMenuItem.Text = "ウィンドウ";
            // 
            // 整列ToolStripMenuItem
            // 
            this.整列ToolStripMenuItem.Name = "整列ToolStripMenuItem";
            this.整列ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.整列ToolStripMenuItem.Text = "整列";
            this.整列ToolStripMenuItem.Click += new System.EventHandler(this.整列ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 表示VToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 発注一覧ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 貯蔵品ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 承認一覧ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 管理表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 部品購入管理表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 入渠管理表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 船用品管理表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 潤滑油管理表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ウィンドウToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 整列ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem データ出力ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 潤滑油貯蔵品リストToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 潤滑油集計表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 船用品_ペイント_集計表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 潤滑油年間管理表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 船用品_ペイント_管理表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 船用品_ペイント_年間管理表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 船用品_ペイント_貯蔵品リストToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem 業者別支払実績ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem データ修正ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 年度跨ぎ調整ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 日付調整ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 船種別変更ToolStripMenuItem;
    }
}