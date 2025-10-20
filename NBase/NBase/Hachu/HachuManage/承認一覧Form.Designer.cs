namespace Hachu.HachuManage
{
    partial class 承認一覧Form
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button条件クリア = new System.Windows.Forms.Button();
            this.comboBox事務担当者 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button検索 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.syoninListControl1 = new Hachu.HachuManage.SyoninListControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(742, 271);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(736, 120);
            this.panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button条件クリア);
            this.groupBox1.Controls.Add(this.comboBox事務担当者);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox船);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(736, 120);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // button条件クリア
            // 
            this.button条件クリア.BackColor = System.Drawing.SystemColors.Control;
            this.button条件クリア.Location = new System.Drawing.Point(350, 50);
            this.button条件クリア.Name = "button条件クリア";
            this.button条件クリア.Size = new System.Drawing.Size(75, 23);
            this.button条件クリア.TabIndex = 18;
            this.button条件クリア.Text = "条件クリア";
            this.button条件クリア.UseVisualStyleBackColor = false;
            this.button条件クリア.Click += new System.EventHandler(this.button条件クリア_Click);
            // 
            // comboBox事務担当者
            // 
            this.comboBox事務担当者.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox事務担当者.FormattingEnabled = true;
            this.comboBox事務担当者.Location = new System.Drawing.Point(87, 53);
            this.comboBox事務担当者.Name = "comboBox事務担当者";
            this.comboBox事務担当者.Size = new System.Drawing.Size(170, 20);
            this.comboBox事務担当者.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "事務担当者";
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(87, 21);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(170, 20);
            this.comboBox船.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "船";
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(350, 19);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(75, 23);
            this.button検索.TabIndex = 0;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 129);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.syoninListControl1);
            this.splitContainer1.Size = new System.Drawing.Size(736, 139);
            this.splitContainer1.SplitterDistance = 632;
            this.splitContainer1.TabIndex = 3;
            // 
            // syoninListControl1
            // 
            this.syoninListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.syoninListControl1.Location = new System.Drawing.Point(0, 0);
            this.syoninListControl1.Name = "syoninListControl1";
            this.syoninListControl1.Size = new System.Drawing.Size(630, 137);
            this.syoninListControl1.TabIndex = 0;
            // 
            // 承認一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(742, 271);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "承認一覧Form";
            this.Text = "承認一覧";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.承認一覧Form_FormClosing);
            this.Load += new System.EventHandler(this.承認一覧Form_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button条件クリア;
        private System.Windows.Forms.ComboBox comboBox事務担当者;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private SyoninListControl syoninListControl1;
    }
}