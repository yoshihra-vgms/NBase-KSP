namespace Hachu.HachuManage
{
    partial class 発注状況一覧Form
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
            this.tabControl2 = new LidorSystems.IntegralUI.Containers.TabControl();
            this.button支払合算 = new System.Windows.Forms.Button();
            this.button新規発注 = new System.Windows.Forms.Button();
            this.button新規見積 = new System.Windows.Forms.Button();
            this.button新規手配 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button出力 = new System.Windows.Forms.Button();
            this.button項目設定 = new System.Windows.Forms.Button();
            this.checkBox船受領 = new System.Windows.Forms.CheckBox();
            this.checkBox受領済 = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox詳細種別 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox種別 = new System.Windows.Forms.ComboBox();
            this.button条件クリア = new System.Windows.Forms.Button();
            this.checkBox完了 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.maskedTextBox受領日To = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox受領日From = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox発注済 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.maskedTextBox手配依頼日To = new System.Windows.Forms.MaskedTextBox();
            this.checkBox未対応 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.maskedTextBox手配依頼日From = new System.Windows.Forms.MaskedTextBox();
            this.checkBox見積中 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox事務担当者 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button検索 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.settingListControl1 = new NBaseCommon.SettingListControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 234F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1325, 508);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl2);
            this.panel1.Controls.Add(this.button支払合算);
            this.panel1.Controls.Add(this.button新規発注);
            this.panel1.Controls.Add(this.button新規見積);
            this.panel1.Controls.Add(this.button新規手配);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1319, 228);
            this.panel1.TabIndex = 2;
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Location = new System.Drawing.Point(4, 193);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.ScrollPos = new System.Drawing.Point(0, 0);
            this.tabControl2.Size = new System.Drawing.Size(1315, 32);
            this.tabControl2.TabIndex = 6;
            this.tabControl2.TabShape = LidorSystems.IntegralUI.Containers.TabShape.Trapezoidal;
            this.tabControl2.Text = "tabControl2";
            this.tabControl2.SelectedPageChanged += new LidorSystems.IntegralUI.ObjectEventHandler(this.tabControl2_SelectedPageChanged);
            // 
            // button支払合算
            // 
            this.button支払合算.BackColor = System.Drawing.SystemColors.Control;
            this.button支払合算.Enabled = false;
            this.button支払合算.Location = new System.Drawing.Point(290, 150);
            this.button支払合算.Name = "button支払合算";
            this.button支払合算.Size = new System.Drawing.Size(75, 23);
            this.button支払合算.TabIndex = 4;
            this.button支払合算.Text = "支払合算";
            this.button支払合算.UseVisualStyleBackColor = false;
            this.button支払合算.Visible = false;
            this.button支払合算.Click += new System.EventHandler(this.button支払合算_Click);
            // 
            // button新規発注
            // 
            this.button新規発注.BackColor = System.Drawing.SystemColors.Control;
            this.button新規発注.Enabled = false;
            this.button新規発注.Location = new System.Drawing.Point(166, 150);
            this.button新規発注.Name = "button新規発注";
            this.button新規発注.Size = new System.Drawing.Size(75, 23);
            this.button新規発注.TabIndex = 3;
            this.button新規発注.Text = "新規発注";
            this.button新規発注.UseVisualStyleBackColor = false;
            this.button新規発注.Click += new System.EventHandler(this.button新規発注_Click);
            // 
            // button新規見積
            // 
            this.button新規見積.BackColor = System.Drawing.SystemColors.Control;
            this.button新規見積.Enabled = false;
            this.button新規見積.Location = new System.Drawing.Point(85, 150);
            this.button新規見積.Name = "button新規見積";
            this.button新規見積.Size = new System.Drawing.Size(75, 23);
            this.button新規見積.TabIndex = 2;
            this.button新規見積.Text = "新規見積";
            this.button新規見積.UseVisualStyleBackColor = false;
            this.button新規見積.Click += new System.EventHandler(this.button新規見積_Click);
            // 
            // button新規手配
            // 
            this.button新規手配.BackColor = System.Drawing.SystemColors.Control;
            this.button新規手配.Enabled = false;
            this.button新規手配.Location = new System.Drawing.Point(4, 150);
            this.button新規手配.Name = "button新規手配";
            this.button新規手配.Size = new System.Drawing.Size(75, 23);
            this.button新規手配.TabIndex = 1;
            this.button新規手配.Text = "新規手配";
            this.button新規手配.UseVisualStyleBackColor = false;
            this.button新規手配.Click += new System.EventHandler(this.button新規手配_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button出力);
            this.groupBox1.Controls.Add(this.button項目設定);
            this.groupBox1.Controls.Add(this.checkBox船受領);
            this.groupBox1.Controls.Add(this.checkBox受領済);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.comboBox詳細種別);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comboBox種別);
            this.groupBox1.Controls.Add(this.button条件クリア);
            this.groupBox1.Controls.Add(this.checkBox完了);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.maskedTextBox受領日To);
            this.groupBox1.Controls.Add(this.maskedTextBox受領日From);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.checkBox発注済);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.maskedTextBox手配依頼日To);
            this.groupBox1.Controls.Add(this.checkBox未対応);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.maskedTextBox手配依頼日From);
            this.groupBox1.Controls.Add(this.checkBox見積中);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox事務担当者);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox船);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(857, 140);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // button出力
            // 
            this.button出力.BackColor = System.Drawing.SystemColors.Control;
            this.button出力.Location = new System.Drawing.Point(654, 47);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(92, 23);
            this.button出力.TabIndex = 17;
            this.button出力.Text = "検索結果出力";
            this.button出力.UseVisualStyleBackColor = false;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // button項目設定
            // 
            this.button項目設定.BackColor = System.Drawing.SystemColors.Control;
            this.button項目設定.Location = new System.Drawing.Point(654, 75);
            this.button項目設定.Name = "button項目設定";
            this.button項目設定.Size = new System.Drawing.Size(92, 23);
            this.button項目設定.TabIndex = 18;
            this.button項目設定.Text = "リスト項目設定";
            this.button項目設定.UseVisualStyleBackColor = false;
            this.button項目設定.Click += new System.EventHandler(this.button項目設定_Click);
            // 
            // checkBox船受領
            // 
            this.checkBox船受領.AutoSize = true;
            this.checkBox船受領.Checked = true;
            this.checkBox船受領.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox船受領.Location = new System.Drawing.Point(280, 108);
            this.checkBox船受領.Name = "checkBox船受領";
            this.checkBox船受領.Size = new System.Drawing.Size(60, 16);
            this.checkBox船受領.TabIndex = 12;
            this.checkBox船受領.Text = "船受領";
            this.checkBox船受領.UseVisualStyleBackColor = true;
            // 
            // checkBox受領済
            // 
            this.checkBox受領済.AutoSize = true;
            this.checkBox受領済.Checked = true;
            this.checkBox受領済.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox受領済.Location = new System.Drawing.Point(344, 108);
            this.checkBox受領済.Name = "checkBox受領済";
            this.checkBox受領済.Size = new System.Drawing.Size(60, 16);
            this.checkBox受領済.TabIndex = 13;
            this.checkBox受領済.Text = "受領済";
            this.checkBox受領済.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(284, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "詳細種別";
            // 
            // comboBox詳細種別
            // 
            this.comboBox詳細種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox詳細種別.FormattingEnabled = true;
            this.comboBox詳細種別.Items.AddRange(new object[] {
            "船用品",
            "潤滑油",
            "修繕"});
            this.comboBox詳細種別.Location = new System.Drawing.Point(349, 49);
            this.comboBox詳細種別.Name = "comboBox詳細種別";
            this.comboBox詳細種別.Size = new System.Drawing.Size(170, 20);
            this.comboBox詳細種別.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(46, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "種別";
            // 
            // comboBox種別
            // 
            this.comboBox種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox種別.FormattingEnabled = true;
            this.comboBox種別.Items.AddRange(new object[] {
            "船用品",
            "潤滑油",
            "修繕"});
            this.comboBox種別.Location = new System.Drawing.Point(87, 49);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(170, 20);
            this.comboBox種別.TabIndex = 3;
            this.comboBox種別.SelectedIndexChanged += new System.EventHandler(this.comboBox種別_SelectedIndexChanged);
            // 
            // button条件クリア
            // 
            this.button条件クリア.BackColor = System.Drawing.SystemColors.Control;
            this.button条件クリア.Location = new System.Drawing.Point(752, 19);
            this.button条件クリア.Name = "button条件クリア";
            this.button条件クリア.Size = new System.Drawing.Size(92, 23);
            this.button条件クリア.TabIndex = 16;
            this.button条件クリア.Text = "条件クリア";
            this.button条件クリア.UseVisualStyleBackColor = false;
            this.button条件クリア.Click += new System.EventHandler(this.button条件クリア_Click);
            // 
            // checkBox完了
            // 
            this.checkBox完了.AutoSize = true;
            this.checkBox完了.Location = new System.Drawing.Point(409, 108);
            this.checkBox完了.Name = "checkBox完了";
            this.checkBox完了.Size = new System.Drawing.Size(48, 16);
            this.checkBox完了.TabIndex = 14;
            this.checkBox完了.Text = "完了";
            this.checkBox完了.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(427, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "～";
            // 
            // maskedTextBox受領日To
            // 
            this.maskedTextBox受領日To.Location = new System.Drawing.Point(450, 77);
            this.maskedTextBox受領日To.Mask = "0000/00/00";
            this.maskedTextBox受領日To.Name = "maskedTextBox受領日To";
            this.maskedTextBox受領日To.Size = new System.Drawing.Size(69, 19);
            this.maskedTextBox受領日To.TabIndex = 8;
            this.maskedTextBox受領日To.ValidatingType = typeof(System.DateTime);
            // 
            // maskedTextBox受領日From
            // 
            this.maskedTextBox受領日From.Location = new System.Drawing.Point(349, 77);
            this.maskedTextBox受領日From.Mask = "0000/00/00";
            this.maskedTextBox受領日From.Name = "maskedTextBox受領日From";
            this.maskedTextBox受領日From.Size = new System.Drawing.Size(69, 19);
            this.maskedTextBox受領日From.TabIndex = 7;
            this.maskedTextBox受領日From.ValidatingType = typeof(System.DateTime);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(302, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "受領日";
            // 
            // checkBox発注済
            // 
            this.checkBox発注済.AutoSize = true;
            this.checkBox発注済.Checked = true;
            this.checkBox発注済.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox発注済.Location = new System.Drawing.Point(219, 108);
            this.checkBox発注済.Name = "checkBox発注済";
            this.checkBox発注済.Size = new System.Drawing.Size(60, 16);
            this.checkBox発注済.TabIndex = 11;
            this.checkBox発注済.Text = "発注済";
            this.checkBox発注済.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "～";
            // 
            // maskedTextBox手配依頼日To
            // 
            this.maskedTextBox手配依頼日To.Location = new System.Drawing.Point(188, 77);
            this.maskedTextBox手配依頼日To.Mask = "0000/00/00";
            this.maskedTextBox手配依頼日To.Name = "maskedTextBox手配依頼日To";
            this.maskedTextBox手配依頼日To.Size = new System.Drawing.Size(69, 19);
            this.maskedTextBox手配依頼日To.TabIndex = 6;
            this.maskedTextBox手配依頼日To.ValidatingType = typeof(System.DateTime);
            // 
            // checkBox未対応
            // 
            this.checkBox未対応.AutoSize = true;
            this.checkBox未対応.Checked = true;
            this.checkBox未対応.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox未対応.Location = new System.Drawing.Point(87, 108);
            this.checkBox未対応.Name = "checkBox未対応";
            this.checkBox未対応.Size = new System.Drawing.Size(60, 16);
            this.checkBox未対応.TabIndex = 9;
            this.checkBox未対応.Text = "未対応";
            this.checkBox未対応.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(46, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "状況";
            // 
            // maskedTextBox手配依頼日From
            // 
            this.maskedTextBox手配依頼日From.Location = new System.Drawing.Point(87, 76);
            this.maskedTextBox手配依頼日From.Mask = "0000/00/00";
            this.maskedTextBox手配依頼日From.Name = "maskedTextBox手配依頼日From";
            this.maskedTextBox手配依頼日From.Size = new System.Drawing.Size(69, 19);
            this.maskedTextBox手配依頼日From.TabIndex = 5;
            this.maskedTextBox手配依頼日From.ValidatingType = typeof(System.DateTime);
            // 
            // checkBox見積中
            // 
            this.checkBox見積中.AutoSize = true;
            this.checkBox見積中.Checked = true;
            this.checkBox見積中.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox見積中.Location = new System.Drawing.Point(153, 108);
            this.checkBox見積中.Name = "checkBox見積中";
            this.checkBox見積中.Size = new System.Drawing.Size(60, 16);
            this.checkBox見積中.TabIndex = 10;
            this.checkBox見積中.Text = "見積中";
            this.checkBox見積中.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "手配依頼日";
            // 
            // comboBox事務担当者
            // 
            this.comboBox事務担当者.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox事務担当者.FormattingEnabled = true;
            this.comboBox事務担当者.Location = new System.Drawing.Point(349, 21);
            this.comboBox事務担当者.Name = "comboBox事務担当者";
            this.comboBox事務担当者.Size = new System.Drawing.Size(170, 20);
            this.comboBox事務担当者.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(278, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "事務担当者";
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(87, 21);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(170, 20);
            this.comboBox船.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "船";
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(654, 19);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(92, 23);
            this.button検索.TabIndex = 15;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 237);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.settingListControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1319, 268);
            this.splitContainer1.SplitterDistance = 933;
            this.splitContainer1.TabIndex = 3;
            // 
            // settingListControl1
            // 
            this.settingListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingListControl1.Location = new System.Drawing.Point(0, 0);
            this.settingListControl1.Name = "settingListControl1";
            this.settingListControl1.Size = new System.Drawing.Size(931, 266);
            this.settingListControl1.TabIndex = 18;
            // 
            // 発注状況一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1325, 508);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(836, 290);
            this.Name = "発注状況一覧Form";
            this.Text = "発注状況一覧";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.発注状況一覧Form_FormClosing);
            this.Load += new System.EventHandler(this.発注状況一覧Form_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button新規発注;
        private System.Windows.Forms.Button button新規見積;
        private System.Windows.Forms.Button button新規手配;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button条件クリア;
        private System.Windows.Forms.CheckBox checkBox完了;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox maskedTextBox受領日To;
        private System.Windows.Forms.MaskedTextBox maskedTextBox受領日From;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox発注済;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox maskedTextBox手配依頼日To;
        private System.Windows.Forms.CheckBox checkBox未対応;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox maskedTextBox手配依頼日From;
        private System.Windows.Forms.CheckBox checkBox見積中;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox事務担当者;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox種別;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox詳細種別;
        private System.Windows.Forms.CheckBox checkBox受領済;
        private System.Windows.Forms.Button button支払合算;
        private System.Windows.Forms.CheckBox checkBox船受領;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private LidorSystems.IntegralUI.Containers.TabControl tabControl2;
        private System.Windows.Forms.Button button項目設定;
        private NBaseCommon.SettingListControl settingListControl1;
        private System.Windows.Forms.Button button出力;
    }
}