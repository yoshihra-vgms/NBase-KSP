namespace Yojitsu
{
    partial class 予算作成Form
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
            this.textBox前提条件 = new System.Windows.Forms.TextBox();
            this.textBox作成者 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox予算種別 = new System.Windows.Forms.TextBox();
            this.textBox年度 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button作成 = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button船稼働設定 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(568, 249);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.textBox前提条件);
            this.panel1.Controls.Add(this.textBox作成者);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBox予算種別);
            this.panel1.Controls.Add(this.textBox年度);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 183);
            this.panel1.TabIndex = 0;
            // 
            // textBox前提条件
            // 
            this.textBox前提条件.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox前提条件.Location = new System.Drawing.Point(67, 59);
            this.textBox前提条件.MaxLength = 2000;
            this.textBox前提条件.Multiline = true;
            this.textBox前提条件.Name = "textBox前提条件";
            this.textBox前提条件.Size = new System.Drawing.Size(492, 80);
            this.textBox前提条件.TabIndex = 1;
            // 
            // textBox作成者
            // 
            this.textBox作成者.Location = new System.Drawing.Point(240, 33);
            this.textBox作成者.Name = "textBox作成者";
            this.textBox作成者.ReadOnly = true;
            this.textBox作成者.Size = new System.Drawing.Size(102, 19);
            this.textBox作成者.TabIndex = 0;
            this.textBox作成者.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(185, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "作成者";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "年度";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "予算種別";
            // 
            // textBox予算種別
            // 
            this.textBox予算種別.Location = new System.Drawing.Point(67, 33);
            this.textBox予算種別.Name = "textBox予算種別";
            this.textBox予算種別.ReadOnly = true;
            this.textBox予算種別.Size = new System.Drawing.Size(102, 19);
            this.textBox予算種別.TabIndex = 0;
            this.textBox予算種別.TabStop = false;
            // 
            // textBox年度
            // 
            this.textBox年度.Location = new System.Drawing.Point(67, 8);
            this.textBox年度.Name = "textBox年度";
            this.textBox年度.ReadOnly = true;
            this.textBox年度.Size = new System.Drawing.Size(63, 19);
            this.textBox年度.TabIndex = 0;
            this.textBox年度.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "前提条件";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.button作成);
            this.panel2.Controls.Add(this.buttonCancel);
            this.panel2.Location = new System.Drawing.Point(365, 222);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 24);
            this.panel2.TabIndex = 1;
            // 
            // button作成
            // 
            this.button作成.BackColor = System.Drawing.SystemColors.Control;
            this.button作成.Location = new System.Drawing.Point(42, 0);
            this.button作成.Name = "button作成";
            this.button作成.Size = new System.Drawing.Size(75, 23);
            this.button作成.TabIndex = 2;
            this.button作成.Text = "作成";
            this.button作成.UseVisualStyleBackColor = false;
            this.button作成.Click += new System.EventHandler(this.button作成_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(123, 0);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "閉じる";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.button船稼働設定);
            this.panel3.Location = new System.Drawing.Point(365, 192);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 24);
            this.panel3.TabIndex = 2;
            // 
            // button船稼働設定
            // 
            this.button船稼働設定.BackColor = System.Drawing.SystemColors.Control;
            this.button船稼働設定.Location = new System.Drawing.Point(123, 1);
            this.button船稼働設定.Name = "button船稼働設定";
            this.button船稼働設定.Size = new System.Drawing.Size(75, 23);
            this.button船稼働設定.TabIndex = 2;
            this.button船稼働設定.Text = "船稼働設定";
            this.button船稼働設定.UseVisualStyleBackColor = false;
            this.button船稼働設定.Click += new System.EventHandler(this.button船稼働設定_Click);
            // 
            // 予算作成Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(592, 273);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "予算作成Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox前提条件;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button作成;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBox作成者;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox予算種別;
        private System.Windows.Forms.TextBox textBox年度;
        private System.Windows.Forms.Button button船稼働設定;
        private System.Windows.Forms.Panel panel3;
    }
}