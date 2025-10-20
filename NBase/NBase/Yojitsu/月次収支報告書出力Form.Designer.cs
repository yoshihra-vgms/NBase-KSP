namespace Yojitsu
{
    partial class 月次収支報告書出力Form
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.button出力 = new System.Windows.Forms.Button();
            this.comboBox期間 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox前年度実績を出力 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox単位 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox予算種別 = new System.Windows.Forms.ComboBox();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox年度 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.comboBoxリビジョン = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(175, 186);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 22;
            this.buttonCancel.Text = "閉じる";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // button出力
            // 
            this.button出力.BackColor = System.Drawing.SystemColors.Control;
            this.button出力.Location = new System.Drawing.Point(92, 186);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(75, 23);
            this.button出力.TabIndex = 21;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = false;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // comboBox期間
            // 
            this.comboBox期間.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox期間.FormattingEnabled = true;
            this.comboBox期間.Location = new System.Drawing.Point(119, 34);
            this.comboBox期間.Name = "comboBox期間";
            this.comboBox期間.Size = new System.Drawing.Size(140, 20);
            this.comboBox期間.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 32;
            this.label4.Text = "期間";
            // 
            // checkBox前年度実績を出力
            // 
            this.checkBox前年度実績を出力.AutoSize = true;
            this.checkBox前年度実績を出力.Location = new System.Drawing.Point(213, 155);
            this.checkBox前年度実績を出力.Name = "checkBox前年度実績を出力";
            this.checkBox前年度実績を出力.Size = new System.Drawing.Size(117, 16);
            this.checkBox前年度実績を出力.TabIndex = 31;
            this.checkBox前年度実績を出力.Text = "前年度実績を出力";
            this.checkBox前年度実績を出力.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 28;
            this.label5.Text = "単位";
            // 
            // comboBox単位
            // 
            this.comboBox単位.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox単位.FormattingEnabled = true;
            this.comboBox単位.Location = new System.Drawing.Point(119, 153);
            this.comboBox単位.Name = "comboBox単位";
            this.comboBox単位.Size = new System.Drawing.Size(73, 20);
            this.comboBox単位.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 27;
            this.label1.Text = "予算種別";
            // 
            // comboBox予算種別
            // 
            this.comboBox予算種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox予算種別.FormattingEnabled = true;
            this.comboBox予算種別.Location = new System.Drawing.Point(119, 93);
            this.comboBox予算種別.Name = "comboBox予算種別";
            this.comboBox予算種別.Size = new System.Drawing.Size(73, 20);
            this.comboBox予算種別.TabIndex = 30;
            this.comboBox予算種別.SelectedIndexChanged += new System.EventHandler(this.comboBox予算種別_SelectedIndexChanged);
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(119, 64);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(180, 20);
            this.comboBox船.TabIndex = 33;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 12);
            this.label7.TabIndex = 34;
            this.label7.Text = "全社/グループ/船";
            // 
            // comboBox年度
            // 
            this.comboBox年度.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年度.FormattingEnabled = true;
            this.comboBox年度.Location = new System.Drawing.Point(119, 6);
            this.comboBox年度.Name = "comboBox年度";
            this.comboBox年度.Size = new System.Drawing.Size(73, 20);
            this.comboBox年度.TabIndex = 36;
            this.comboBox年度.SelectionChangeCommitted += new System.EventHandler(this.comboBox年度_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 35;
            this.label6.Text = "年度";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "月次収支報告書.xls";
            this.saveFileDialog1.Filter = "Excel ファイル|*.xls";
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // comboBoxリビジョン
            // 
            this.comboBoxリビジョン.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxリビジョン.FormattingEnabled = true;
            this.comboBoxリビジョン.Location = new System.Drawing.Point(119, 123);
            this.comboBoxリビジョン.Name = "comboBoxリビジョン";
            this.comboBoxリビジョン.Size = new System.Drawing.Size(131, 20);
            this.comboBoxリビジョン.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 12);
            this.label3.TabIndex = 37;
            this.label3.Text = "Rev.";
            // 
            // 月次収支報告書出力Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(342, 215);
            this.Controls.Add(this.comboBoxリビジョン);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox年度);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBox船);
            this.Controls.Add(this.comboBox期間);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox前年度実績を出力);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox単位);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox予算種別);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.button出力);
            this.Name = "月次収支報告書出力Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.ComboBox comboBox期間;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox前年度実績を出力;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox単位;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox予算種別;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox年度;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ComboBox comboBoxリビジョン;
        private System.Windows.Forms.Label label3;

    }
}