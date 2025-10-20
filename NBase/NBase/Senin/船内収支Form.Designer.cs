namespace Senin
{
    partial class 船内収支Form
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
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button明細追加 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox支払金額 = new System.Windows.Forms.TextBox();
            this.textBox繰越金額 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox受入金額 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button科目別集計表 = new System.Windows.Forms.Button();
            this.button船内収支金報告書 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button検索 = new System.Windows.Forms.Button();
            this.comboBox月 = new System.Windows.Forms.ComboBox();
            this.comboBox年 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button船用金送金 = new System.Windows.Forms.Button();
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(32, 18);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(150, 20);
            this.comboBox船.TabIndex = 1;
            this.comboBox船.SelectedIndexChanged += new System.EventHandler(this.comboBox船_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "船";
            // 
            // button明細追加
            // 
            this.button明細追加.BackColor = System.Drawing.SystemColors.Control;
            this.button明細追加.Location = new System.Drawing.Point(891, 51);
            this.button明細追加.Name = "button明細追加";
            this.button明細追加.Size = new System.Drawing.Size(92, 23);
            this.button明細追加.TabIndex = 6;
            this.button明細追加.Text = "明細追加";
            this.button明細追加.UseVisualStyleBackColor = false;
            this.button明細追加.Click += new System.EventHandler(this.button明細追加_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(202, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "支払金額合計";
            // 
            // textBox支払金額
            // 
            this.textBox支払金額.Location = new System.Drawing.Point(285, 50);
            this.textBox支払金額.Name = "textBox支払金額";
            this.textBox支払金額.ReadOnly = true;
            this.textBox支払金額.Size = new System.Drawing.Size(90, 19);
            this.textBox支払金額.TabIndex = 50;
            this.textBox支払金額.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox繰越金額
            // 
            this.textBox繰越金額.Location = new System.Drawing.Point(495, 50);
            this.textBox繰越金額.Name = "textBox繰越金額";
            this.textBox繰越金額.ReadOnly = true;
            this.textBox繰越金額.Size = new System.Drawing.Size(90, 19);
            this.textBox繰越金額.TabIndex = 60;
            this.textBox繰越金額.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(391, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "差引残額翌月繰越";
            // 
            // textBox受入金額
            // 
            this.textBox受入金額.Location = new System.Drawing.Point(92, 50);
            this.textBox受入金額.Name = "textBox受入金額";
            this.textBox受入金額.ReadOnly = true;
            this.textBox受入金額.Size = new System.Drawing.Size(90, 19);
            this.textBox受入金額.TabIndex = 40;
            this.textBox受入金額.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "受入金額合計";
            // 
            // button科目別集計表
            // 
            this.button科目別集計表.BackColor = System.Drawing.SystemColors.Control;
            this.button科目別集計表.Location = new System.Drawing.Point(731, 51);
            this.button科目別集計表.Name = "button科目別集計表";
            this.button科目別集計表.Size = new System.Drawing.Size(145, 23);
            this.button科目別集計表.TabIndex = 5;
            this.button科目別集計表.Text = "科目別集計表出力";
            this.button科目別集計表.UseVisualStyleBackColor = false;
            this.button科目別集計表.Click += new System.EventHandler(this.button科目別集計表_Click);
            // 
            // button船内収支金報告書
            // 
            this.button船内収支金報告書.BackColor = System.Drawing.SystemColors.Control;
            this.button船内収支金報告書.Location = new System.Drawing.Point(731, 19);
            this.button船内収支金報告書.Name = "button船内収支金報告書";
            this.button船内収支金報告書.Size = new System.Drawing.Size(145, 23);
            this.button船内収支金報告書.TabIndex = 4;
            this.button船内収支金報告書.Text = "船内収支報告書出力";
            this.button船内収支金報告書.UseVisualStyleBackColor = false;
            this.button船内収支金報告書.Click += new System.EventHandler(this.button船内収支報告書_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1061, 631);
            this.tableLayoutPanel1.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.button船内収支金報告書);
            this.panel1.Controls.Add(this.button科目別集計表);
            this.panel1.Controls.Add(this.button船用金送金);
            this.panel1.Controls.Add(this.button明細追加);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1055, 89);
            this.panel1.TabIndex = 30;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Controls.Add(this.textBox支払金額);
            this.groupBox1.Controls.Add(this.comboBox月);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox年);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox繰越金額);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBox船);
            this.groupBox1.Controls.Add(this.textBox受入金額);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(710, 81);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(602, 48);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(96, 23);
            this.button検索.TabIndex = 61;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // comboBox月
            // 
            this.comboBox月.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox月.FormattingEnabled = true;
            this.comboBox月.Location = new System.Drawing.Point(313, 18);
            this.comboBox月.Name = "comboBox月";
            this.comboBox月.Size = new System.Drawing.Size(38, 20);
            this.comboBox月.TabIndex = 3;
            // 
            // comboBox年
            // 
            this.comboBox年.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年.FormattingEnabled = true;
            this.comboBox年.Location = new System.Drawing.Point(241, 18);
            this.comboBox年.Name = "comboBox年";
            this.comboBox年.Size = new System.Drawing.Size(61, 20);
            this.comboBox年.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(202, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "年月";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(302, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 30;
            this.label8.Text = "/";
            // 
            // button船用金送金
            // 
            this.button船用金送金.BackColor = System.Drawing.SystemColors.Control;
            this.button船用金送金.Location = new System.Drawing.Point(891, 18);
            this.button船用金送金.Name = "button船用金送金";
            this.button船用金送金.Size = new System.Drawing.Size(92, 23);
            this.button船用金送金.TabIndex = 6;
            this.button船用金送金.Text = "船用金送金";
            this.button船用金送金.UseVisualStyleBackColor = false;
            this.button船用金送金.Click += new System.EventHandler(this.button船用金送金_Click);
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
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(1049, 524);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Location = new System.Drawing.Point(3, 98);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(1055, 530);
            this.treeListView1.TabIndex = 31;
            this.treeListView1.Text = "treeListView1";
            this.treeListView1.DoubleClick += new System.EventHandler(this.treeListView1_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "船内収支報告書.xlsx";
            this.saveFileDialog1.Filter = "Excel ファイル|*.xlsx";
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.FileName = "科目別集計表.xlsx";
            this.saveFileDialog2.Filter = "Excel ファイル|*.xlsx";
            // 
            // 船内収支Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1085, 655);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "船内収支Form";
            this.Text = "船用金管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.船内準備金Form_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button明細追加;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox支払金額;
        private System.Windows.Forms.TextBox textBox繰越金額;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox受入金額;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button科目別集計表;
        private System.Windows.Forms.Button button船内収支金報告書;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox月;
        private System.Windows.Forms.ComboBox comboBox年;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button船用金送金;
    }
}