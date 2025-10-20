namespace Yojitsu
{
    partial class TopForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button表示 = new System.Windows.Forms.Button();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.comboBoxリビジョン = new System.Windows.Forms.ComboBox();
            this.comboBox予算種別 = new System.Windows.Forms.ComboBox();
            this.comboBox年度 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.treeListView2 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.butt閉じる = new System.Windows.Forms.Button();
            this.buttonダイジェスト出力 = new System.Windows.Forms.Button();
            this.butt月次収支 = new System.Windows.Forms.Button();
            this.butt経常収支 = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.butt編集 = new System.Windows.Forms.Button();
            this.buttメモ編集閲覧 = new System.Windows.Forms.Button();
            this.butt予算Revアップ = new System.Windows.Forms.Button();
            this.butt予算Fix = new System.Windows.Forms.Button();
            this.butt見直し予算作成 = new System.Windows.Forms.Button();
            this.butt当初予算作成 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView2)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button表示);
            this.groupBox1.Controls.Add(this.comboBox船);
            this.groupBox1.Controls.Add(this.comboBoxリビジョン);
            this.groupBox1.Controls.Add(this.comboBox予算種別);
            this.groupBox1.Controls.Add(this.comboBox年度);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索";
            // 
            // button表示
            // 
            this.button表示.BackColor = System.Drawing.SystemColors.Control;
            this.button表示.Location = new System.Drawing.Point(452, 46);
            this.button表示.Name = "button表示";
            this.button表示.Size = new System.Drawing.Size(75, 23);
            this.button表示.TabIndex = 2;
            this.button表示.Text = "表示";
            this.button表示.UseVisualStyleBackColor = false;
            this.button表示.Click += new System.EventHandler(this.button表示_Click);
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(259, 22);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(180, 20);
            this.comboBox船.TabIndex = 1;
            // 
            // comboBoxリビジョン
            // 
            this.comboBoxリビジョン.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxリビジョン.FormattingEnabled = true;
            this.comboBoxリビジョン.Location = new System.Drawing.Point(259, 48);
            this.comboBoxリビジョン.Name = "comboBoxリビジョン";
            this.comboBoxリビジョン.Size = new System.Drawing.Size(142, 20);
            this.comboBoxリビジョン.TabIndex = 1;
            this.comboBoxリビジョン.SelectedValueChanged += new System.EventHandler(this.comboBoxリビジョン_SelectedValueChanged);
            // 
            // comboBox予算種別
            // 
            this.comboBox予算種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox予算種別.FormattingEnabled = true;
            this.comboBox予算種別.Location = new System.Drawing.Point(75, 48);
            this.comboBox予算種別.Name = "comboBox予算種別";
            this.comboBox予算種別.Size = new System.Drawing.Size(73, 20);
            this.comboBox予算種別.TabIndex = 1;
            this.comboBox予算種別.SelectionChangeCommitted += new System.EventHandler(this.comboBox予算種別_SelectionChangeCommitted);
            // 
            // comboBox年度
            // 
            this.comboBox年度.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年度.FormattingEnabled = true;
            this.comboBox年度.Location = new System.Drawing.Point(75, 22);
            this.comboBox年度.Name = "comboBox年度";
            this.comboBox年度.Size = new System.Drawing.Size(73, 20);
            this.comboBox年度.TabIndex = 1;
            this.comboBox年度.SelectionChangeCommitted += new System.EventHandler(this.comboBox年度_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(162, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "全社/グループ/船";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(162, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Rev.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "予算種別";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "年度";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1052, 549);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.treeListView2);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1046, 144);
            this.panel3.TabIndex = 3;
            // 
            // treeListView2
            // 
            this.treeListView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.treeListView2.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView2.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView2.ContentPanel.Name = "";
            this.treeListView2.ContentPanel.Size = new System.Drawing.Size(299, 128);
            this.treeListView2.ContentPanel.TabIndex = 3;
            this.treeListView2.ContentPanel.TabStop = false;
            this.treeListView2.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView2.Location = new System.Drawing.Point(741, 10);
            this.treeListView2.Name = "treeListView2";
            this.treeListView2.Size = new System.Drawing.Size(305, 134);
            this.treeListView2.TabIndex = 1;
            this.treeListView2.Text = "treeListView2";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.butt閉じる);
            this.panel1.Controls.Add(this.buttonダイジェスト出力);
            this.panel1.Controls.Add(this.butt月次収支);
            this.panel1.Controls.Add(this.butt経常収支);
            this.panel1.Location = new System.Drawing.Point(194, 522);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(855, 24);
            this.panel1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(251, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "予算対比表出力";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(128, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "予算表出力";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // butt閉じる
            // 
            this.butt閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.butt閉じる.Location = new System.Drawing.Point(755, 0);
            this.butt閉じる.Name = "butt閉じる";
            this.butt閉じる.Size = new System.Drawing.Size(97, 23);
            this.butt閉じる.TabIndex = 0;
            this.butt閉じる.Text = "閉じる";
            this.butt閉じる.UseVisualStyleBackColor = false;
            this.butt閉じる.Click += new System.EventHandler(this.butt閉じる_Click);
            // 
            // buttonダイジェスト出力
            // 
            this.buttonダイジェスト出力.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonダイジェスト出力.Enabled = false;
            this.buttonダイジェスト出力.Location = new System.Drawing.Point(626, 0);
            this.buttonダイジェスト出力.Name = "buttonダイジェスト出力";
            this.buttonダイジェスト出力.Size = new System.Drawing.Size(123, 23);
            this.buttonダイジェスト出力.TabIndex = 0;
            this.buttonダイジェスト出力.Text = "ダイジェスト出力";
            this.buttonダイジェスト出力.UseVisualStyleBackColor = false;
            this.buttonダイジェスト出力.Visible = false;
            this.buttonダイジェスト出力.Click += new System.EventHandler(this.buttonダイジェスト出力_Click);
            // 
            // butt月次収支
            // 
            this.butt月次収支.BackColor = System.Drawing.SystemColors.Control;
            this.butt月次収支.Enabled = false;
            this.butt月次収支.Location = new System.Drawing.Point(497, 0);
            this.butt月次収支.Name = "butt月次収支";
            this.butt月次収支.Size = new System.Drawing.Size(123, 23);
            this.butt月次収支.TabIndex = 0;
            this.butt月次収支.Text = "月次収支報告書出力";
            this.butt月次収支.UseVisualStyleBackColor = false;
            this.butt月次収支.Click += new System.EventHandler(this.butt月次収支_Click);
            // 
            // butt経常収支
            // 
            this.butt経常収支.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.butt経常収支.Enabled = false;
            this.butt経常収支.Location = new System.Drawing.Point(374, 0);
            this.butt経常収支.Name = "butt経常収支";
            this.butt経常収支.Size = new System.Drawing.Size(117, 23);
            this.butt経常収支.TabIndex = 0;
            this.butt経常収支.Text = "経常収支実績出力";
            this.butt経常収支.UseVisualStyleBackColor = false;
            this.butt経常収支.Visible = false;
            this.butt経常収支.Click += new System.EventHandler(this.butt経常収支_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.treeListView1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 153);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1046, 333);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // treeListView1
            // 
            this.treeListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.treeListView1.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView1.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView1.ContentPanel.Name = "";
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(1034, 286);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Location = new System.Drawing.Point(3, 38);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(1040, 292);
            this.treeListView1.TabIndex = 1;
            this.treeListView1.Text = "treeListView1";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.label7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1040, 29);
            this.panel2.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(978, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "単位：千円";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.butt編集);
            this.panel4.Controls.Add(this.buttメモ編集閲覧);
            this.panel4.Controls.Add(this.butt予算Revアップ);
            this.panel4.Controls.Add(this.butt予算Fix);
            this.panel4.Controls.Add(this.butt見直し予算作成);
            this.panel4.Controls.Add(this.butt当初予算作成);
            this.panel4.Location = new System.Drawing.Point(473, 492);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(576, 24);
            this.panel4.TabIndex = 1;
            // 
            // butt編集
            // 
            this.butt編集.BackColor = System.Drawing.SystemColors.Control;
            this.butt編集.Enabled = false;
            this.butt編集.Location = new System.Drawing.Point(3, 1);
            this.butt編集.Name = "butt編集";
            this.butt編集.Size = new System.Drawing.Size(75, 23);
            this.butt編集.TabIndex = 0;
            this.butt編集.Text = "編集";
            this.butt編集.UseVisualStyleBackColor = false;
            this.butt編集.Click += new System.EventHandler(this.editButt_Click);
            // 
            // buttメモ編集閲覧
            // 
            this.buttメモ編集閲覧.BackColor = System.Drawing.SystemColors.Control;
            this.buttメモ編集閲覧.Enabled = false;
            this.buttメモ編集閲覧.Location = new System.Drawing.Point(476, 1);
            this.buttメモ編集閲覧.Name = "buttメモ編集閲覧";
            this.buttメモ編集閲覧.Size = new System.Drawing.Size(97, 23);
            this.buttメモ編集閲覧.TabIndex = 0;
            this.buttメモ編集閲覧.Text = "メモ編集・閲覧";
            this.buttメモ編集閲覧.UseVisualStyleBackColor = false;
            this.buttメモ編集閲覧.Click += new System.EventHandler(this.buttメモ編集閲覧_Click);
            // 
            // butt予算Revアップ
            // 
            this.butt予算Revアップ.BackColor = System.Drawing.SystemColors.Control;
            this.butt予算Revアップ.Enabled = false;
            this.butt予算Revアップ.Location = new System.Drawing.Point(373, 1);
            this.butt予算Revアップ.Name = "butt予算Revアップ";
            this.butt予算Revアップ.Size = new System.Drawing.Size(97, 23);
            this.butt予算Revアップ.TabIndex = 0;
            this.butt予算Revアップ.Text = "予算Revアップ";
            this.butt予算Revアップ.UseVisualStyleBackColor = false;
            this.butt予算Revアップ.Click += new System.EventHandler(this.butt予算Revアップ_Click);
            // 
            // butt予算Fix
            // 
            this.butt予算Fix.BackColor = System.Drawing.SystemColors.Control;
            this.butt予算Fix.Enabled = false;
            this.butt予算Fix.Location = new System.Drawing.Point(292, 1);
            this.butt予算Fix.Name = "butt予算Fix";
            this.butt予算Fix.Size = new System.Drawing.Size(75, 23);
            this.butt予算Fix.TabIndex = 0;
            this.butt予算Fix.Text = "予算Fix";
            this.butt予算Fix.UseVisualStyleBackColor = false;
            this.butt予算Fix.Click += new System.EventHandler(this.butt予算Fix_Click);
            // 
            // butt見直し予算作成
            // 
            this.butt見直し予算作成.BackColor = System.Drawing.SystemColors.Control;
            this.butt見直し予算作成.Enabled = false;
            this.butt見直し予算作成.Location = new System.Drawing.Point(188, 1);
            this.butt見直し予算作成.Name = "butt見直し予算作成";
            this.butt見直し予算作成.Size = new System.Drawing.Size(98, 23);
            this.butt見直し予算作成.TabIndex = 0;
            this.butt見直し予算作成.Text = "見直し予算作成";
            this.butt見直し予算作成.UseVisualStyleBackColor = false;
            this.butt見直し予算作成.Click += new System.EventHandler(this.butt予算作成_Click);
            // 
            // butt当初予算作成
            // 
            this.butt当初予算作成.BackColor = System.Drawing.SystemColors.Control;
            this.butt当初予算作成.Enabled = false;
            this.butt当初予算作成.Location = new System.Drawing.Point(84, 1);
            this.butt当初予算作成.Name = "butt当初予算作成";
            this.butt当初予算作成.Size = new System.Drawing.Size(98, 23);
            this.butt当初予算作成.TabIndex = 0;
            this.butt当初予算作成.Text = "当初予算作成";
            this.butt当初予算作成.UseVisualStyleBackColor = false;
            this.butt当初予算作成.Click += new System.EventHandler(this.butt予算作成_Click);
            // 
            // TopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1076, 573);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TopForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox予算種別;
        private System.Windows.Forms.ComboBox comboBox年度;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.ComboBox comboBoxリビジョン;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button表示;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button butt編集;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button butt閉じる;
        private System.Windows.Forms.Button butt月次収支;
        private System.Windows.Forms.Button butt経常収支;
        private System.Windows.Forms.Panel panel3;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button buttメモ編集閲覧;
        private System.Windows.Forms.Button butt予算Revアップ;
        private System.Windows.Forms.Button butt予算Fix;
        private System.Windows.Forms.Button butt当初予算作成;
        private System.Windows.Forms.Button butt見直し予算作成;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonダイジェスト出力;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}