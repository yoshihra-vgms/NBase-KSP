namespace Hachu.HachuManage
{
    partial class 概算計上一覧Form
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button概算データ出力 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox詳細種別 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox種別 = new System.Windows.Forms.ComboBox();
            this.radioButton計上済 = new System.Windows.Forms.RadioButton();
            this.radioButton未計上 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.maskedTextBox対象年月 = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button条件クリア = new System.Windows.Forms.Button();
            this.comboBox事務担当者 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button検索 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label合計金額 = new System.Windows.Forms.Label();
            this.button概算データ出力_管理者 = new System.Windows.Forms.Button();
            this.label基幹連携対象 = new System.Windows.Forms.Label();
            this.label月次締め対象 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button月次計上確定 = new System.Windows.Forms.Button();
            this.button概算計上実行 = new System.Windows.Forms.Button();
            this.dataGridView概算計上一覧 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView概算計上一覧)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView概算計上一覧, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 163F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(725, 427);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(719, 157);
            this.panel1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(719, 157);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(711, 132);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "概算計上検索";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.button概算データ出力);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.comboBox詳細種別);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comboBox種別);
            this.groupBox1.Controls.Add(this.radioButton計上済);
            this.groupBox1.Controls.Add(this.radioButton未計上);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.maskedTextBox対象年月);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button条件クリア);
            this.groupBox1.Controls.Add(this.comboBox事務担当者);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox船);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(705, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // button概算データ出力
            // 
            this.button概算データ出力.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button概算データ出力.BackColor = System.Drawing.SystemColors.Control;
            this.button概算データ出力.Enabled = false;
            this.button概算データ出力.Location = new System.Drawing.Point(601, 80);
            this.button概算データ出力.Name = "button概算データ出力";
            this.button概算データ出力.Size = new System.Drawing.Size(86, 23);
            this.button概算データ出力.TabIndex = 30;
            this.button概算データ出力.Text = "概算ﾃﾞｰﾀ出力";
            this.button概算データ出力.UseVisualStyleBackColor = false;
            this.button概算データ出力.Click += new System.EventHandler(this.button概算データ出力_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(296, 78);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 27;
            this.label9.Text = "詳細種別";
            // 
            // comboBox詳細種別
            // 
            this.comboBox詳細種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox詳細種別.Enabled = false;
            this.comboBox詳細種別.FormattingEnabled = true;
            this.comboBox詳細種別.Items.AddRange(new object[] {
            "船用品",
            "潤滑油",
            "修繕"});
            this.comboBox詳細種別.Location = new System.Drawing.Point(355, 75);
            this.comboBox詳細種別.Name = "comboBox詳細種別";
            this.comboBox詳細種別.Size = new System.Drawing.Size(170, 20);
            this.comboBox詳細種別.TabIndex = 29;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(52, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "種別";
            // 
            // comboBox種別
            // 
            this.comboBox種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox種別.Enabled = false;
            this.comboBox種別.FormattingEnabled = true;
            this.comboBox種別.Items.AddRange(new object[] {
            "船用品",
            "潤滑油",
            "修繕"});
            this.comboBox種別.Location = new System.Drawing.Point(87, 74);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(170, 20);
            this.comboBox種別.TabIndex = 28;
            this.comboBox種別.SelectedIndexChanged += new System.EventHandler(this.comboBox種別_SelectedIndexChanged);
            // 
            // radioButton計上済
            // 
            this.radioButton計上済.AutoSize = true;
            this.radioButton計上済.Enabled = false;
            this.radioButton計上済.Location = new System.Drawing.Point(152, 100);
            this.radioButton計上済.Name = "radioButton計上済";
            this.radioButton計上済.Size = new System.Drawing.Size(59, 16);
            this.radioButton計上済.TabIndex = 9;
            this.radioButton計上済.TabStop = true;
            this.radioButton計上済.Text = "計上済";
            this.radioButton計上済.UseVisualStyleBackColor = true;
            // 
            // radioButton未計上
            // 
            this.radioButton未計上.AutoSize = true;
            this.radioButton未計上.Checked = true;
            this.radioButton未計上.Enabled = false;
            this.radioButton未計上.Location = new System.Drawing.Point(87, 100);
            this.radioButton未計上.Name = "radioButton未計上";
            this.radioButton未計上.Size = new System.Drawing.Size(59, 16);
            this.radioButton未計上.TabIndex = 4;
            this.radioButton未計上.TabStop = true;
            this.radioButton未計上.Text = "未計上";
            this.radioButton未計上.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "計上";
            // 
            // maskedTextBox対象年月
            // 
            this.maskedTextBox対象年月.Enabled = false;
            this.maskedTextBox対象年月.Location = new System.Drawing.Point(87, 19);
            this.maskedTextBox対象年月.Mask = "0000/00";
            this.maskedTextBox対象年月.Name = "maskedTextBox対象年月";
            this.maskedTextBox対象年月.Size = new System.Drawing.Size(51, 19);
            this.maskedTextBox対象年月.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "※ 対象年月";
            // 
            // button条件クリア
            // 
            this.button条件クリア.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button条件クリア.BackColor = System.Drawing.SystemColors.Control;
            this.button条件クリア.Enabled = false;
            this.button条件クリア.Location = new System.Drawing.Point(601, 50);
            this.button条件クリア.Name = "button条件クリア";
            this.button条件クリア.Size = new System.Drawing.Size(86, 23);
            this.button条件クリア.TabIndex = 7;
            this.button条件クリア.Text = "検索条件クリア";
            this.button条件クリア.UseVisualStyleBackColor = false;
            this.button条件クリア.Click += new System.EventHandler(this.button条件クリア_Click);
            // 
            // comboBox事務担当者
            // 
            this.comboBox事務担当者.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox事務担当者.Enabled = false;
            this.comboBox事務担当者.FormattingEnabled = true;
            this.comboBox事務担当者.Location = new System.Drawing.Point(355, 49);
            this.comboBox事務担当者.Name = "comboBox事務担当者";
            this.comboBox事務担当者.Size = new System.Drawing.Size(170, 20);
            this.comboBox事務担当者.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "事務担当者";
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.Enabled = false;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(87, 46);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(170, 20);
            this.comboBox船.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "船";
            // 
            // button検索
            // 
            this.button検索.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Enabled = false;
            this.button検索.Location = new System.Drawing.Point(601, 19);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(86, 23);
            this.button検索.TabIndex = 6;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label合計金額);
            this.tabPage2.Controls.Add(this.button概算データ出力_管理者);
            this.tabPage2.Controls.Add(this.label基幹連携対象);
            this.tabPage2.Controls.Add(this.label月次締め対象);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.button月次計上確定);
            this.tabPage2.Controls.Add(this.button概算計上実行);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(711, 132);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "管理者機能";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(139, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "月次計上を確定します。";
            // 
            // label合計金額
            // 
            this.label合計金額.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label合計金額.AutoSize = true;
            this.label合計金額.BackColor = System.Drawing.Color.Transparent;
            this.label合計金額.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label合計金額.Location = new System.Drawing.Point(528, 105);
            this.label合計金額.Name = "label合計金額";
            this.label合計金額.Size = new System.Drawing.Size(166, 15);
            this.label合計金額.TabIndex = 32;
            this.label合計金額.Text = "合計金額： \\123,456,789";
            // 
            // button概算データ出力_管理者
            // 
            this.button概算データ出力_管理者.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button概算データ出力_管理者.BackColor = System.Drawing.SystemColors.Control;
            this.button概算データ出力_管理者.Enabled = false;
            this.button概算データ出力_管理者.Location = new System.Drawing.Point(608, 13);
            this.button概算データ出力_管理者.Name = "button概算データ出力_管理者";
            this.button概算データ出力_管理者.Size = new System.Drawing.Size(86, 23);
            this.button概算データ出力_管理者.TabIndex = 31;
            this.button概算データ出力_管理者.Text = "概算ﾃﾞｰﾀ出力";
            this.button概算データ出力_管理者.UseVisualStyleBackColor = false;
            this.button概算データ出力_管理者.Click += new System.EventHandler(this.button概算データ出力_Click);
            // 
            // label基幹連携対象
            // 
            this.label基幹連携対象.AutoSize = true;
            this.label基幹連携対象.BackColor = System.Drawing.Color.Transparent;
            this.label基幹連携対象.Location = new System.Drawing.Point(415, 56);
            this.label基幹連携対象.Name = "label基幹連携対象";
            this.label基幹連携対象.Size = new System.Drawing.Size(71, 12);
            this.label基幹連携対象.TabIndex = 5;
            this.label基幹連携対象.Text = "（対象年月：）";
            // 
            // label月次締め対象
            // 
            this.label月次締め対象.AutoSize = true;
            this.label月次締め対象.BackColor = System.Drawing.Color.Transparent;
            this.label月次締め対象.Location = new System.Drawing.Point(267, 24);
            this.label月次締め対象.Name = "label月次締め対象";
            this.label月次締め対象.Size = new System.Drawing.Size(71, 12);
            this.label月次締め対象.TabIndex = 4;
            this.label月次締め対象.Text = "（対象年月：）";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(139, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(270, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "概算データと貯蔵品データを基幹システムへ送信します。";
            // 
            // button月次計上確定
            // 
            this.button月次計上確定.BackColor = System.Drawing.SystemColors.Control;
            this.button月次計上確定.Enabled = false;
            this.button月次計上確定.Location = new System.Drawing.Point(20, 19);
            this.button月次計上確定.Name = "button月次計上確定";
            this.button月次計上確定.Size = new System.Drawing.Size(100, 23);
            this.button月次計上確定.TabIndex = 1;
            this.button月次計上確定.Text = "月次計上確定";
            this.button月次計上確定.UseVisualStyleBackColor = false;
            this.button月次計上確定.Click += new System.EventHandler(this.button月次計上確定_Click);
            // 
            // button概算計上実行
            // 
            this.button概算計上実行.BackColor = System.Drawing.SystemColors.Control;
            this.button概算計上実行.Enabled = false;
            this.button概算計上実行.Location = new System.Drawing.Point(20, 51);
            this.button概算計上実行.Name = "button概算計上実行";
            this.button概算計上実行.Size = new System.Drawing.Size(100, 23);
            this.button概算計上実行.TabIndex = 0;
            this.button概算計上実行.Text = "基幹ｼｽﾃﾑ連携";
            this.button概算計上実行.UseVisualStyleBackColor = false;
            this.button概算計上実行.Click += new System.EventHandler(this.button基幹システム連携_Click);
            // 
            // dataGridView概算計上一覧
            // 
            this.dataGridView概算計上一覧.AllowUserToAddRows = false;
            this.dataGridView概算計上一覧.AllowUserToDeleteRows = false;
            this.dataGridView概算計上一覧.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView概算計上一覧.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView概算計上一覧.Location = new System.Drawing.Point(3, 166);
            this.dataGridView概算計上一覧.MultiSelect = false;
            this.dataGridView概算計上一覧.Name = "dataGridView概算計上一覧";
            this.dataGridView概算計上一覧.ReadOnly = true;
            this.dataGridView概算計上一覧.RowTemplate.Height = 21;
            this.dataGridView概算計上一覧.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView概算計上一覧.Size = new System.Drawing.Size(719, 258);
            this.dataGridView概算計上一覧.TabIndex = 3;
            this.dataGridView概算計上一覧.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView概算計上一覧_CellMouseDoubleClick);
            // 
            // 概算計上一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(725, 427);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "概算計上一覧Form";
            this.Text = "概算計上一覧";
            this.Load += new System.EventHandler(this.概算計上一覧Form_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.概算計上一覧Form_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView概算計上一覧)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button条件クリア;
        private System.Windows.Forms.ComboBox comboBox事務担当者;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox maskedTextBox対象年月;
        private System.Windows.Forms.Button button月次計上確定;
        private System.Windows.Forms.Button button概算計上実行;
        private System.Windows.Forms.RadioButton radioButton計上済;
        private System.Windows.Forms.RadioButton radioButton未計上;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label月次締め対象;
        private System.Windows.Forms.DataGridView dataGridView概算計上一覧;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox詳細種別;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox種別;
        private System.Windows.Forms.Button button概算データ出力;
        private System.Windows.Forms.Button button概算データ出力_管理者;
        private System.Windows.Forms.Label label基幹連携対象;
        private System.Windows.Forms.Label label合計金額;
    }
}