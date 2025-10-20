namespace Yojitsu
{
    partial class 予算表出力Form
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
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox単位 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox予算種別 = new System.Windows.Forms.ComboBox();
            this.comboBox年度 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.comboBoxリビジョン = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox範囲 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(175, 166);
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
            this.button出力.Location = new System.Drawing.Point(92, 166);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(75, 23);
            this.button出力.TabIndex = 21;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = false;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 28;
            this.label5.Text = "単位";
            // 
            // comboBox単位
            // 
            this.comboBox単位.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox単位.FormattingEnabled = true;
            this.comboBox単位.Location = new System.Drawing.Point(119, 128);
            this.comboBox単位.Name = "comboBox単位";
            this.comboBox単位.Size = new System.Drawing.Size(73, 20);
            this.comboBox単位.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 27;
            this.label1.Text = "予算種別";
            // 
            // comboBox予算種別
            // 
            this.comboBox予算種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox予算種別.FormattingEnabled = true;
            this.comboBox予算種別.Location = new System.Drawing.Point(119, 41);
            this.comboBox予算種別.Name = "comboBox予算種別";
            this.comboBox予算種別.Size = new System.Drawing.Size(73, 20);
            this.comboBox予算種別.TabIndex = 30;
            this.comboBox予算種別.SelectedIndexChanged += new System.EventHandler(this.comboBox予算種別_SelectedIndexChanged);
            // 
            // comboBox年度
            // 
            this.comboBox年度.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年度.FormattingEnabled = true;
            this.comboBox年度.Location = new System.Drawing.Point(119, 12);
            this.comboBox年度.Name = "comboBox年度";
            this.comboBox年度.Size = new System.Drawing.Size(73, 20);
            this.comboBox年度.TabIndex = 36;
            this.comboBox年度.SelectionChangeCommitted += new System.EventHandler(this.comboBox年度_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 35;
            this.label6.Text = "年度";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "予算表.xls";
            this.saveFileDialog1.Filter = "Excel ファイル|*.xls";
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // comboBoxリビジョン
            // 
            this.comboBoxリビジョン.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxリビジョン.FormattingEnabled = true;
            this.comboBoxリビジョン.Location = new System.Drawing.Point(119, 70);
            this.comboBoxリビジョン.Name = "comboBoxリビジョン";
            this.comboBoxリビジョン.Size = new System.Drawing.Size(131, 20);
            this.comboBoxリビジョン.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 12);
            this.label3.TabIndex = 37;
            this.label3.Text = "Rev.";
            // 
            // comboBox範囲
            // 
            this.comboBox範囲.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox範囲.FormattingEnabled = true;
            this.comboBox範囲.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19"});
            this.comboBox範囲.Location = new System.Drawing.Point(119, 99);
            this.comboBox範囲.Name = "comboBox範囲";
            this.comboBox範囲.Size = new System.Drawing.Size(73, 20);
            this.comboBox範囲.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 40;
            this.label2.Text = "出力範囲（年）";
            // 
            // 予算表出力Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(342, 205);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox範囲);
            this.Controls.Add(this.comboBoxリビジョン);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox年度);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox単位);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox予算種別);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.button出力);
            this.Name = "予算表出力Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox単位;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox予算種別;
        private System.Windows.Forms.ComboBox comboBox年度;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ComboBox comboBoxリビジョン;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox範囲;
        private System.Windows.Forms.Label label2;

    }
}