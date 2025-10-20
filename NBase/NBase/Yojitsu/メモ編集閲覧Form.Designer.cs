namespace Yojitsu
{
    partial class メモ編集閲覧Form
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
            this.button保存 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox前提条件 = new System.Windows.Forms.TextBox();
            this.comboBox予算種別 = new System.Windows.Forms.ComboBox();
            this.comboBox年度 = new System.Windows.Forms.ComboBox();
            this.comboBoxリビジョン = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxリビジョン備考 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button保存
            // 
            this.button保存.BackColor = System.Drawing.SystemColors.Control;
            this.button保存.Enabled = false;
            this.button保存.Location = new System.Drawing.Point(42, 0);
            this.button保存.Name = "button保存";
            this.button保存.Size = new System.Drawing.Size(75, 23);
            this.button保存.TabIndex = 2;
            this.button保存.Text = "保存";
            this.button保存.UseVisualStyleBackColor = false;
            this.button保存.Click += new System.EventHandler(this.button保存_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(123, 0);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 3;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "年度";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "予算種別";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "前提条件";
            // 
            // textBox前提条件
            // 
            this.textBox前提条件.Location = new System.Drawing.Point(67, 59);
            this.textBox前提条件.Multiline = true;
            this.textBox前提条件.Name = "textBox前提条件";
            this.textBox前提条件.ReadOnly = true;
            this.textBox前提条件.Size = new System.Drawing.Size(492, 80);
            this.textBox前提条件.TabIndex = 1;
            // 
            // comboBox予算種別
            // 
            this.comboBox予算種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox予算種別.FormattingEnabled = true;
            this.comboBox予算種別.Location = new System.Drawing.Point(67, 33);
            this.comboBox予算種別.Name = "comboBox予算種別";
            this.comboBox予算種別.Size = new System.Drawing.Size(73, 20);
            this.comboBox予算種別.TabIndex = 5;
            this.comboBox予算種別.SelectionChangeCommitted += new System.EventHandler(this.comboBox予算種別_SelectionChangeCommitted);
            // 
            // comboBox年度
            // 
            this.comboBox年度.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年度.FormattingEnabled = true;
            this.comboBox年度.Location = new System.Drawing.Point(67, 8);
            this.comboBox年度.Name = "comboBox年度";
            this.comboBox年度.Size = new System.Drawing.Size(73, 20);
            this.comboBox年度.TabIndex = 4;
            this.comboBox年度.SelectionChangeCommitted += new System.EventHandler(this.comboBox年度_SelectionChangeCommitted);
            // 
            // comboBoxリビジョン
            // 
            this.comboBoxリビジョン.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxリビジョン.FormattingEnabled = true;
            this.comboBoxリビジョン.Location = new System.Drawing.Point(188, 33);
            this.comboBoxリビジョン.Name = "comboBoxリビジョン";
            this.comboBoxリビジョン.Size = new System.Drawing.Size(131, 20);
            this.comboBoxリビジョン.TabIndex = 7;
            this.comboBoxリビジョン.SelectedIndexChanged += new System.EventHandler(this.comboBoxリビジョン_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(155, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "Rev.";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(568, 609);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.comboBox年度);
            this.panel1.Controls.Add(this.textBoxリビジョン備考);
            this.panel1.Controls.Add(this.textBox前提条件);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.comboBoxリビジョン);
            this.panel1.Controls.Add(this.comboBox予算種別);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 573);
            this.panel1.TabIndex = 0;
            // 
            // textBoxリビジョン備考
            // 
            this.textBoxリビジョン備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBoxリビジョン備考.Location = new System.Drawing.Point(67, 145);
            this.textBoxリビジョン備考.Multiline = true;
            this.textBoxリビジョン備考.Name = "textBoxリビジョン備考";
            this.textBoxリビジョン備考.ReadOnly = true;
            this.textBoxリビジョン備考.Size = new System.Drawing.Size(492, 80);
            this.textBoxリビジョン備考.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "Rev. 備考";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.button保存);
            this.panel2.Controls.Add(this.button閉じる);
            this.panel2.Location = new System.Drawing.Point(365, 582);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 24);
            this.panel2.TabIndex = 1;
            // 
            // メモ編集閲覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(592, 633);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "メモ編集閲覧Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.メモ編集閲覧Form_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button保存;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox前提条件;
        private System.Windows.Forms.ComboBox comboBox予算種別;
        private System.Windows.Forms.ComboBox comboBox年度;
        private System.Windows.Forms.ComboBox comboBoxリビジョン;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxリビジョン備考;
        private System.Windows.Forms.Label label5;
    }
}