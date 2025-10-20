namespace Yojitsu
{
    partial class 船稼働設定日時Form
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
            this.textBox船名 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.maskedTextBox稼働開始 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox稼働終了 = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox点検種別 = new System.Windows.Forms.ComboBox();
            this.comboBox稼働開始 = new System.Windows.Forms.ComboBox();
            this.comboBox稼働終了 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox出渠後 = new System.Windows.Forms.TextBox();
            this.textBox入渠 = new System.Windows.Forms.TextBox();
            this.textBox入渠前 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox入渠月 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBox不稼働 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox管理基礎 = new System.Windows.Forms.CheckBox();
            this.checkBox営業基礎 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox船名
            // 
            this.textBox船名.Location = new System.Drawing.Point(75, 12);
            this.textBox船名.Name = "textBox船名";
            this.textBox船名.ReadOnly = true;
            this.textBox船名.Size = new System.Drawing.Size(190, 19);
            this.textBox船名.TabIndex = 5;
            this.textBox船名.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "船名";
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(229, 165);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "閉じる";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.SystemColors.Control;
            this.buttonOK.Location = new System.Drawing.Point(148, 165);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "稼働期間";
            // 
            // maskedTextBox稼働開始
            // 
            this.maskedTextBox稼働開始.Location = new System.Drawing.Point(75, 54);
            this.maskedTextBox稼働開始.Mask = "0000/00/00";
            this.maskedTextBox稼働開始.Name = "maskedTextBox稼働開始";
            this.maskedTextBox稼働開始.Size = new System.Drawing.Size(66, 19);
            this.maskedTextBox稼働開始.TabIndex = 1;
            this.maskedTextBox稼働開始.ValidatingType = typeof(System.DateTime);
            // 
            // maskedTextBox稼働終了
            // 
            this.maskedTextBox稼働終了.Location = new System.Drawing.Point(223, 54);
            this.maskedTextBox稼働終了.Mask = "0000/00/00";
            this.maskedTextBox稼働終了.Name = "maskedTextBox稼働終了";
            this.maskedTextBox稼働終了.Size = new System.Drawing.Size(66, 19);
            this.maskedTextBox稼働終了.TabIndex = 3;
            this.maskedTextBox稼働終了.ValidatingType = typeof(System.DateTime);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "～";
            // 
            // comboBox点検種別
            // 
            this.comboBox点検種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox点検種別.FormattingEnabled = true;
            this.comboBox点検種別.Items.AddRange(new object[] {
            "",
            "AS",
            "AS/AF",
            "AS/DS",
            "SS",
            "IS"});
            this.comboBox点検種別.Location = new System.Drawing.Point(63, 18);
            this.comboBox点検種別.Name = "comboBox点検種別";
            this.comboBox点検種別.Size = new System.Drawing.Size(66, 20);
            this.comboBox点検種別.TabIndex = 1;
            this.comboBox点検種別.SelectionChangeCommitted += new System.EventHandler(this.comboBox点検種別_SelectionChangeCommitted);
            // 
            // comboBox稼働開始
            // 
            this.comboBox稼働開始.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox稼働開始.FormattingEnabled = true;
            this.comboBox稼働開始.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.comboBox稼働開始.Location = new System.Drawing.Point(147, 54);
            this.comboBox稼働開始.Name = "comboBox稼働開始";
            this.comboBox稼働開始.Size = new System.Drawing.Size(47, 20);
            this.comboBox稼働開始.TabIndex = 2;
            // 
            // comboBox稼働終了
            // 
            this.comboBox稼働終了.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox稼働終了.FormattingEnabled = true;
            this.comboBox稼働終了.Items.AddRange(new object[] {
            "AM",
            "PM"});
            this.comboBox稼働終了.Location = new System.Drawing.Point(295, 54);
            this.comboBox稼働終了.Name = "comboBox稼働終了";
            this.comboBox稼働終了.Size = new System.Drawing.Size(47, 20);
            this.comboBox稼働終了.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox出渠後);
            this.groupBox1.Controls.Add(this.textBox入渠);
            this.groupBox1.Controls.Add(this.textBox入渠前);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBox入渠月);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox点検種別);
            this.groupBox1.Location = new System.Drawing.Point(12, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 76);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検査";
            // 
            // textBox出渠後
            // 
            this.textBox出渠後.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox出渠後.Location = new System.Drawing.Point(352, 44);
            this.textBox出渠後.MaxLength = 5;
            this.textBox出渠後.Name = "textBox出渠後";
            this.textBox出渠後.Size = new System.Drawing.Size(42, 19);
            this.textBox出渠後.TabIndex = 5;
            this.textBox出渠後.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox入渠
            // 
            this.textBox入渠.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox入渠.Location = new System.Drawing.Point(256, 44);
            this.textBox入渠.MaxLength = 5;
            this.textBox入渠.Name = "textBox入渠";
            this.textBox入渠.Size = new System.Drawing.Size(42, 19);
            this.textBox入渠.TabIndex = 4;
            this.textBox入渠.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox入渠前
            // 
            this.textBox入渠前.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox入渠前.Location = new System.Drawing.Point(173, 44);
            this.textBox入渠前.MaxLength = 5;
            this.textBox入渠前.Name = "textBox入渠前";
            this.textBox入渠前.Size = new System.Drawing.Size(42, 19);
            this.textBox入渠前.TabIndex = 3;
            this.textBox入渠前.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "入渠月";
            // 
            // comboBox入渠月
            // 
            this.comboBox入渠月.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox入渠月.FormattingEnabled = true;
            this.comboBox入渠月.Items.AddRange(new object[] {
            "",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "1",
            "2",
            "3"});
            this.comboBox入渠月.Location = new System.Drawing.Point(63, 44);
            this.comboBox入渠月.Name = "comboBox入渠月";
            this.comboBox入渠月.Size = new System.Drawing.Size(47, 20);
            this.comboBox入渠月.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(224, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "入渠";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(308, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "出渠後";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(130, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "入渠前";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "検査種別";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(362, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "不稼働";
            // 
            // checkBox不稼働
            // 
            this.checkBox不稼働.AutoSize = true;
            this.checkBox不稼働.Location = new System.Drawing.Point(409, 57);
            this.checkBox不稼働.Name = "checkBox不稼働";
            this.checkBox不稼働.Size = new System.Drawing.Size(15, 14);
            this.checkBox不稼働.TabIndex = 5;
            this.checkBox不稼働.UseVisualStyleBackColor = true;
            this.checkBox不稼働.CheckedChanged += new System.EventHandler(this.checkBox不稼働_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox管理基礎);
            this.groupBox2.Controls.Add(this.checkBox営業基礎);
            this.groupBox2.Location = new System.Drawing.Point(271, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 36);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "販管費設定（個別）";
            // 
            // checkBox管理基礎
            // 
            this.checkBox管理基礎.AutoSize = true;
            this.checkBox管理基礎.Location = new System.Drawing.Point(91, 14);
            this.checkBox管理基礎.Name = "checkBox管理基礎";
            this.checkBox管理基礎.Size = new System.Drawing.Size(72, 16);
            this.checkBox管理基礎.TabIndex = 0;
            this.checkBox管理基礎.Text = "管理基礎";
            this.checkBox管理基礎.UseVisualStyleBackColor = true;
            // 
            // checkBox営業基礎
            // 
            this.checkBox営業基礎.AutoSize = true;
            this.checkBox営業基礎.Location = new System.Drawing.Point(13, 14);
            this.checkBox営業基礎.Name = "checkBox営業基礎";
            this.checkBox営業基礎.Size = new System.Drawing.Size(72, 16);
            this.checkBox営業基礎.TabIndex = 0;
            this.checkBox営業基礎.Text = "営業基礎";
            this.checkBox営業基礎.UseVisualStyleBackColor = true;
            // 
            // 船稼働設定日時Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(453, 197);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.checkBox不稼働);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox稼働終了);
            this.Controls.Add(this.comboBox稼働開始);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.maskedTextBox稼働終了);
            this.Controls.Add(this.maskedTextBox稼働開始);
            this.Controls.Add(this.textBox船名);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "船稼働設定日時Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox船名;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox稼働開始;
        private System.Windows.Forms.MaskedTextBox maskedTextBox稼働終了;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox点検種別;
        private System.Windows.Forms.ComboBox comboBox稼働開始;
        private System.Windows.Forms.ComboBox comboBox稼働終了;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox入渠月;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox入渠;
        private System.Windows.Forms.TextBox textBox入渠前;
        private System.Windows.Forms.TextBox textBox出渠後;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBox不稼働;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox管理基礎;
        private System.Windows.Forms.CheckBox checkBox営業基礎;
    }
}