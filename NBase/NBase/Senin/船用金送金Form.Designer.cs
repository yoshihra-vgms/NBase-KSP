namespace Senin
{
    partial class 船用金送金Form
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox終了月 = new System.Windows.Forms.ComboBox();
            this.comboBox開始月 = new System.Windows.Forms.ComboBox();
            this.comboBox終了年 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox開始年 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonクリア = new System.Windows.Forms.Button();
            this.button検索 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.button新規送金 = new System.Windows.Forms.Button();
            this.button船用金送金表 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(40, 17);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(150, 20);
            this.comboBox船.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "船";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox終了月);
            this.groupBox1.Controls.Add(this.comboBox開始月);
            this.groupBox1.Controls.Add(this.comboBox終了年);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox開始年);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.buttonクリア);
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.comboBox船);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(746, 69);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // comboBox終了月
            // 
            this.comboBox終了月.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox終了月.FormattingEnabled = true;
            this.comboBox終了月.Location = new System.Drawing.Point(496, 17);
            this.comboBox終了月.Name = "comboBox終了月";
            this.comboBox終了月.Size = new System.Drawing.Size(38, 20);
            this.comboBox終了月.TabIndex = 32;
            // 
            // comboBox開始月
            // 
            this.comboBox開始月.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox開始月.FormattingEnabled = true;
            this.comboBox開始月.Location = new System.Drawing.Point(317, 17);
            this.comboBox開始月.Name = "comboBox開始月";
            this.comboBox開始月.Size = new System.Drawing.Size(38, 20);
            this.comboBox開始月.TabIndex = 32;
            // 
            // comboBox終了年
            // 
            this.comboBox終了年.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox終了年.FormattingEnabled = true;
            this.comboBox終了年.Location = new System.Drawing.Point(424, 17);
            this.comboBox終了年.Name = "comboBox終了年";
            this.comboBox終了年.Size = new System.Drawing.Size(61, 20);
            this.comboBox終了年.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(385, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "年月";
            // 
            // comboBox開始年
            // 
            this.comboBox開始年.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox開始年.FormattingEnabled = true;
            this.comboBox開始年.Location = new System.Drawing.Point(245, 17);
            this.comboBox開始年.Name = "comboBox開始年";
            this.comboBox開始年.Size = new System.Drawing.Size(61, 20);
            this.comboBox開始年.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(485, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 34;
            this.label1.Text = "/";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(206, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "年月";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(306, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 34;
            this.label8.Text = "/";
            // 
            // buttonクリア
            // 
            this.buttonクリア.BackColor = System.Drawing.SystemColors.Control;
            this.buttonクリア.Location = new System.Drawing.Point(606, 41);
            this.buttonクリア.Name = "buttonクリア";
            this.buttonクリア.Size = new System.Drawing.Size(96, 23);
            this.buttonクリア.TabIndex = 28;
            this.buttonクリア.Text = "検索条件クリア";
            this.buttonクリア.UseVisualStyleBackColor = false;
            this.buttonクリア.Click += new System.EventHandler(this.buttonクリア_Click);
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(606, 15);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(96, 23);
            this.button検索.TabIndex = 27;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(361, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "～";
            // 
            // button新規送金
            // 
            this.button新規送金.BackColor = System.Drawing.SystemColors.Control;
            this.button新規送金.Location = new System.Drawing.Point(673, 78);
            this.button新規送金.Name = "button新規送金";
            this.button新規送金.Size = new System.Drawing.Size(75, 23);
            this.button新規送金.TabIndex = 20;
            this.button新規送金.Text = "新規送金";
            this.button新規送金.UseVisualStyleBackColor = false;
            this.button新規送金.Click += new System.EventHandler(this.button新規送金_Click);
            // 
            // button船用金送金表
            // 
            this.button船用金送金表.BackColor = System.Drawing.SystemColors.Control;
            this.button船用金送金表.Location = new System.Drawing.Point(544, 78);
            this.button船用金送金表.Name = "button船用金送金表";
            this.button船用金送金表.Size = new System.Drawing.Size(119, 23);
            this.button船用金送金表.TabIndex = 21;
            this.button船用金送金表.Text = "船用金送金表出力";
            this.button船用金送金表.UseVisualStyleBackColor = false;
            this.button船用金送金表.Click += new System.EventHandler(this.button船用金送金表_Click);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(771, 542);
            this.tableLayoutPanel1.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.button船用金送金表);
            this.panel1.Controls.Add(this.button新規送金);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(765, 114);
            this.panel1.TabIndex = 0;
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
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(753, 399);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Location = new System.Drawing.Point(3, 123);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(765, 416);
            this.treeListView1.TabIndex = 1;
            this.treeListView1.Text = "treeListView1";
            this.treeListView1.DoubleClick += new System.EventHandler(this.treeListView1_Click);
            // 
            // 船用金送金Form
            // 
            this.AcceptButton = this.button検索;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(797, 566);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "船用金送金Form";
            this.Text = "船用金送金";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.船用金送金Form_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonクリア;
        private System.Windows.Forms.Button button新規送金;
        private System.Windows.Forms.Button button船用金送金表;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox終了月;
        private System.Windows.Forms.ComboBox comboBox開始月;
        private System.Windows.Forms.ComboBox comboBox終了年;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox開始年;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
    }
}