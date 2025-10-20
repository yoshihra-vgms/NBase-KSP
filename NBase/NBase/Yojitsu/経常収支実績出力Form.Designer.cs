namespace Yojitsu
{
    partial class 経常収支実績出力Form
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
            this.button出力 = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox予算種別 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox期間 = new System.Windows.Forms.ComboBox();
            this.comboBox単位 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox年度 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // button出力
            // 
            this.button出力.BackColor = System.Drawing.SystemColors.Control;
            this.button出力.Location = new System.Drawing.Point(92, 138);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(75, 23);
            this.button出力.TabIndex = 2;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = false;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(175, 138);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "閉じる";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "予算種別";
            // 
            // comboBox予算種別
            // 
            this.comboBox予算種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox予算種別.FormattingEnabled = true;
            this.comboBox予算種別.Location = new System.Drawing.Point(139, 70);
            this.comboBox予算種別.Name = "comboBox予算種別";
            this.comboBox予算種別.Size = new System.Drawing.Size(73, 20);
            this.comboBox予算種別.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "期間";
            // 
            // comboBox期間
            // 
            this.comboBox期間.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox期間.FormattingEnabled = true;
            this.comboBox期間.Location = new System.Drawing.Point(139, 38);
            this.comboBox期間.Name = "comboBox期間";
            this.comboBox期間.Size = new System.Drawing.Size(140, 20);
            this.comboBox期間.TabIndex = 10;
            // 
            // comboBox単位
            // 
            this.comboBox単位.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox単位.FormattingEnabled = true;
            this.comboBox単位.Location = new System.Drawing.Point(139, 102);
            this.comboBox単位.Name = "comboBox単位";
            this.comboBox単位.Size = new System.Drawing.Size(73, 20);
            this.comboBox単位.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "単位";
            // 
            // comboBox年度
            // 
            this.comboBox年度.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年度.FormattingEnabled = true;
            this.comboBox年度.Location = new System.Drawing.Point(139, 9);
            this.comboBox年度.Name = "comboBox年度";
            this.comboBox年度.Size = new System.Drawing.Size(73, 20);
            this.comboBox年度.TabIndex = 12;
            this.comboBox年度.SelectionChangeCommitted += new System.EventHandler(this.comboBox年度_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(74, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "年度";
            // 
            // 経常収支実績出力Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(342, 176);
            this.Controls.Add(this.comboBox年度);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox期間);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox単位);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox予算種別);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.button出力);
            this.Name = "経常収支実績出力Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox予算種別;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox期間;
        private System.Windows.Forms.ComboBox comboBox単位;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox年度;
        private System.Windows.Forms.Label label6;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}