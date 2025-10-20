namespace Dousei
{
    partial class 動静詳細Form1
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_閉じる = new System.Windows.Forms.Button();
            this.button_登録 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel_TumiAge = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel_Tumi = new System.Windows.Forms.FlowLayoutPanel();
            this.douseiYoteiUserControl1 = new NBaseCommon.DouseiYoteiUserControl();
            this.douseiYoteiUserControl2 = new NBaseCommon.DouseiYoteiUserControl();
            this.flowLayoutPanel_Age = new System.Windows.Forms.FlowLayoutPanel();
            this.douseiYoteiUserControl3 = new NBaseCommon.DouseiYoteiUserControl();
            this.douseiYoteiUserControl4 = new NBaseCommon.DouseiYoteiUserControl();
            this.douseiYoteiUserControl5 = new NBaseCommon.DouseiYoteiUserControl();
            this.douseiYoteiUserControl6 = new NBaseCommon.DouseiYoteiUserControl();
            this.douseiYoteiUserControl7 = new NBaseCommon.DouseiYoteiUserControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.DouseiJissekiUserControl3 = new NBaseCommon.DouseiJissekiUserControl();
            this.DouseiJissekiUserControl4 = new NBaseCommon.DouseiJissekiUserControl();
            this.DouseiJissekiUserControl5 = new NBaseCommon.DouseiJissekiUserControl();
            this.DouseiJissekiUserControl6 = new NBaseCommon.DouseiJissekiUserControl();
            this.DouseiJissekiUserControl7 = new NBaseCommon.DouseiJissekiUserControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.DouseiJissekiUserControl1 = new NBaseCommon.DouseiJissekiUserControl();
            this.DouseiJissekiUserControl2 = new NBaseCommon.DouseiJissekiUserControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel_TumiAge.SuspendLayout();
            this.flowLayoutPanel_Tumi.SuspendLayout();
            this.flowLayoutPanel_Age.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1261, 730);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_閉じる);
            this.panel1.Controls.Add(this.button_登録);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1255, 34);
            this.panel1.TabIndex = 0;
            // 
            // button_閉じる
            // 
            this.button_閉じる.Location = new System.Drawing.Point(1179, 0);
            this.button_閉じる.Name = "button_閉じる";
            this.button_閉じる.Size = new System.Drawing.Size(75, 32);
            this.button_閉じる.TabIndex = 4;
            this.button_閉じる.Text = "閉じる";
            this.button_閉じる.UseVisualStyleBackColor = true;
            this.button_閉じる.Click += new System.EventHandler(this.button_閉じる_Click);
            // 
            // button_登録
            // 
            this.button_登録.Location = new System.Drawing.Point(1098, 0);
            this.button_登録.Name = "button_登録";
            this.button_登録.Size = new System.Drawing.Size(75, 32);
            this.button_登録.TabIndex = 3;
            this.button_登録.Text = "登録";
            this.button_登録.UseVisualStyleBackColor = true;
            this.button_登録.Click += new System.EventHandler(this.button_登録_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1255, 684);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel_TumiAge);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1247, 658);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "予定";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel_TumiAge
            // 
            this.tableLayoutPanel_TumiAge.ColumnCount = 1;
            this.tableLayoutPanel_TumiAge.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1241F));
            this.tableLayoutPanel_TumiAge.Controls.Add(this.flowLayoutPanel_Tumi, 0, 0);
            this.tableLayoutPanel_TumiAge.Controls.Add(this.flowLayoutPanel_Age, 0, 1);
            this.tableLayoutPanel_TumiAge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_TumiAge.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel_TumiAge.Name = "tableLayoutPanel_TumiAge";
            this.tableLayoutPanel_TumiAge.RowCount = 2;
            this.tableLayoutPanel_TumiAge.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 320F));
            this.tableLayoutPanel_TumiAge.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_TumiAge.Size = new System.Drawing.Size(1241, 652);
            this.tableLayoutPanel_TumiAge.TabIndex = 3;
            // 
            // flowLayoutPanel_Tumi
            // 
            this.flowLayoutPanel_Tumi.AutoScroll = true;
            this.flowLayoutPanel_Tumi.Controls.Add(this.douseiYoteiUserControl1);
            this.flowLayoutPanel_Tumi.Controls.Add(this.douseiYoteiUserControl2);
            this.flowLayoutPanel_Tumi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_Tumi.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel_Tumi.Name = "flowLayoutPanel_Tumi";
            this.flowLayoutPanel_Tumi.Size = new System.Drawing.Size(1235, 314);
            this.flowLayoutPanel_Tumi.TabIndex = 0;
            // 
            // douseiYoteiUserControl1
            // 
            this.douseiYoteiUserControl1.BackColor = System.Drawing.Color.White;
            this.douseiYoteiUserControl1.Location = new System.Drawing.Point(3, 3);
            this.douseiYoteiUserControl1.Name = "douseiYoteiUserControl1";
            this.douseiYoteiUserControl1.Size = new System.Drawing.Size(400, 307);
            this.douseiYoteiUserControl1.TabIndex = 0;
            // 
            // douseiYoteiUserControl2
            // 
            this.douseiYoteiUserControl2.BackColor = System.Drawing.Color.White;
            this.douseiYoteiUserControl2.Location = new System.Drawing.Point(409, 3);
            this.douseiYoteiUserControl2.Name = "douseiYoteiUserControl2";
            this.douseiYoteiUserControl2.Size = new System.Drawing.Size(400, 307);
            this.douseiYoteiUserControl2.TabIndex = 1;
            // 
            // flowLayoutPanel_Age
            // 
            this.flowLayoutPanel_Age.AutoScroll = true;
            this.flowLayoutPanel_Age.Controls.Add(this.douseiYoteiUserControl3);
            this.flowLayoutPanel_Age.Controls.Add(this.douseiYoteiUserControl4);
            this.flowLayoutPanel_Age.Controls.Add(this.douseiYoteiUserControl5);
            this.flowLayoutPanel_Age.Controls.Add(this.douseiYoteiUserControl6);
            this.flowLayoutPanel_Age.Controls.Add(this.douseiYoteiUserControl7);
            this.flowLayoutPanel_Age.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_Age.Location = new System.Drawing.Point(3, 323);
            this.flowLayoutPanel_Age.Name = "flowLayoutPanel_Age";
            this.flowLayoutPanel_Age.Size = new System.Drawing.Size(1235, 326);
            this.flowLayoutPanel_Age.TabIndex = 2;
            // 
            // douseiYoteiUserControl3
            // 
            this.douseiYoteiUserControl3.BackColor = System.Drawing.Color.White;
            this.douseiYoteiUserControl3.Location = new System.Drawing.Point(3, 3);
            this.douseiYoteiUserControl3.Name = "douseiYoteiUserControl3";
            this.douseiYoteiUserControl3.Size = new System.Drawing.Size(400, 307);
            this.douseiYoteiUserControl3.TabIndex = 1;
            // 
            // douseiYoteiUserControl4
            // 
            this.douseiYoteiUserControl4.BackColor = System.Drawing.Color.White;
            this.douseiYoteiUserControl4.Location = new System.Drawing.Point(409, 3);
            this.douseiYoteiUserControl4.Name = "douseiYoteiUserControl4";
            this.douseiYoteiUserControl4.Size = new System.Drawing.Size(400, 307);
            this.douseiYoteiUserControl4.TabIndex = 2;
            // 
            // douseiYoteiUserControl5
            // 
            this.douseiYoteiUserControl5.BackColor = System.Drawing.Color.White;
            this.douseiYoteiUserControl5.Location = new System.Drawing.Point(815, 3);
            this.douseiYoteiUserControl5.Name = "douseiYoteiUserControl5";
            this.douseiYoteiUserControl5.Size = new System.Drawing.Size(400, 307);
            this.douseiYoteiUserControl5.TabIndex = 3;
            // 
            // douseiYoteiUserControl6
            // 
            this.douseiYoteiUserControl6.BackColor = System.Drawing.Color.White;
            this.douseiYoteiUserControl6.Location = new System.Drawing.Point(3, 316);
            this.douseiYoteiUserControl6.Name = "douseiYoteiUserControl6";
            this.douseiYoteiUserControl6.Size = new System.Drawing.Size(400, 307);
            this.douseiYoteiUserControl6.TabIndex = 4;
            // 
            // douseiYoteiUserControl7
            // 
            this.douseiYoteiUserControl7.BackColor = System.Drawing.Color.White;
            this.douseiYoteiUserControl7.Location = new System.Drawing.Point(409, 316);
            this.douseiYoteiUserControl7.Name = "douseiYoteiUserControl7";
            this.douseiYoteiUserControl7.Size = new System.Drawing.Size(400, 307);
            this.douseiYoteiUserControl7.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1247, 658);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "実績";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 399F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1241, 652);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.Controls.Add(this.DouseiJissekiUserControl3);
            this.flowLayoutPanel2.Controls.Add(this.DouseiJissekiUserControl4);
            this.flowLayoutPanel2.Controls.Add(this.DouseiJissekiUserControl5);
            this.flowLayoutPanel2.Controls.Add(this.DouseiJissekiUserControl6);
            this.flowLayoutPanel2.Controls.Add(this.DouseiJissekiUserControl7);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 402);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1235, 247);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // DouseiJissekiUserControl3
            // 
            this.DouseiJissekiUserControl3.BackColor = System.Drawing.Color.White;
            this.DouseiJissekiUserControl3.Location = new System.Drawing.Point(3, 3);
            this.DouseiJissekiUserControl3.Name = "DouseiJissekiUserControl3";
            this.DouseiJissekiUserControl3.Size = new System.Drawing.Size(360, 387);
            this.DouseiJissekiUserControl3.TabIndex = 1;
            // 
            // DouseiJissekiUserControl4
            // 
            this.DouseiJissekiUserControl4.BackColor = System.Drawing.Color.White;
            this.DouseiJissekiUserControl4.Location = new System.Drawing.Point(369, 3);
            this.DouseiJissekiUserControl4.Name = "DouseiJissekiUserControl4";
            this.DouseiJissekiUserControl4.Size = new System.Drawing.Size(360, 387);
            this.DouseiJissekiUserControl4.TabIndex = 2;
            // 
            // DouseiJissekiUserControl5
            // 
            this.DouseiJissekiUserControl5.BackColor = System.Drawing.Color.White;
            this.DouseiJissekiUserControl5.Location = new System.Drawing.Point(735, 3);
            this.DouseiJissekiUserControl5.Name = "DouseiJissekiUserControl5";
            this.DouseiJissekiUserControl5.Size = new System.Drawing.Size(360, 387);
            this.DouseiJissekiUserControl5.TabIndex = 3;
            // 
            // DouseiJissekiUserControl6
            // 
            this.DouseiJissekiUserControl6.BackColor = System.Drawing.Color.White;
            this.DouseiJissekiUserControl6.Location = new System.Drawing.Point(3, 396);
            this.DouseiJissekiUserControl6.Name = "DouseiJissekiUserControl6";
            this.DouseiJissekiUserControl6.Size = new System.Drawing.Size(360, 387);
            this.DouseiJissekiUserControl6.TabIndex = 4;
            // 
            // DouseiJissekiUserControl7
            // 
            this.DouseiJissekiUserControl7.BackColor = System.Drawing.Color.White;
            this.DouseiJissekiUserControl7.Location = new System.Drawing.Point(369, 396);
            this.DouseiJissekiUserControl7.Name = "DouseiJissekiUserControl7";
            this.DouseiJissekiUserControl7.Size = new System.Drawing.Size(360, 387);
            this.DouseiJissekiUserControl7.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.DouseiJissekiUserControl1);
            this.flowLayoutPanel1.Controls.Add(this.DouseiJissekiUserControl2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1235, 393);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // DouseiJissekiUserControl1
            // 
            this.DouseiJissekiUserControl1.BackColor = System.Drawing.Color.White;
            this.DouseiJissekiUserControl1.Location = new System.Drawing.Point(3, 3);
            this.DouseiJissekiUserControl1.Name = "DouseiJissekiUserControl1";
            this.DouseiJissekiUserControl1.Size = new System.Drawing.Size(360, 387);
            this.DouseiJissekiUserControl1.TabIndex = 0;
            // 
            // DouseiJissekiUserControl2
            // 
            this.DouseiJissekiUserControl2.BackColor = System.Drawing.Color.White;
            this.DouseiJissekiUserControl2.Location = new System.Drawing.Point(369, 3);
            this.DouseiJissekiUserControl2.Name = "DouseiJissekiUserControl2";
            this.DouseiJissekiUserControl2.Size = new System.Drawing.Size(360, 387);
            this.DouseiJissekiUserControl2.TabIndex = 1;
            // 
            // 動静詳細Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1261, 730);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "動静詳細Form1";
            this.ShowInTaskbar = false;
            this.Text = "動静詳細Form";
            this.Load += new System.EventHandler(this.動静詳細Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel_TumiAge.ResumeLayout(false);
            this.flowLayoutPanel_Tumi.ResumeLayout(false);
            this.flowLayoutPanel_Age.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button_閉じる;
        private System.Windows.Forms.Button button_登録;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_TumiAge;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Tumi;
        private NBaseCommon.DouseiYoteiUserControl douseiYoteiUserControl1;
        private NBaseCommon.DouseiYoteiUserControl douseiYoteiUserControl2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Age;
        private NBaseCommon.DouseiYoteiUserControl douseiYoteiUserControl3;
        private NBaseCommon.DouseiYoteiUserControl douseiYoteiUserControl4;
        private NBaseCommon.DouseiYoteiUserControl douseiYoteiUserControl5;
        private NBaseCommon.DouseiYoteiUserControl douseiYoteiUserControl6;
        private NBaseCommon.DouseiYoteiUserControl douseiYoteiUserControl7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private NBaseCommon.DouseiJissekiUserControl DouseiJissekiUserControl1;
        private NBaseCommon.DouseiJissekiUserControl DouseiJissekiUserControl2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private NBaseCommon.DouseiJissekiUserControl DouseiJissekiUserControl3;
        private NBaseCommon.DouseiJissekiUserControl DouseiJissekiUserControl4;
        private NBaseCommon.DouseiJissekiUserControl DouseiJissekiUserControl5;
        private NBaseCommon.DouseiJissekiUserControl DouseiJissekiUserControl6;
        private NBaseCommon.DouseiJissekiUserControl DouseiJissekiUserControl7;
    }
}