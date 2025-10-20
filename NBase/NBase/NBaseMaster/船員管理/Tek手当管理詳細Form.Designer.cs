namespace NBaseMaster.船員管理
{
    partial class Tek手当管理詳細Form
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
            this.button閉じる = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.button更新 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox金額 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox職名 = new System.Windows.Forms.ComboBox();
            this.comboBox部署 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox手当名 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox作業内容 = new System.Windows.Forms.TextBox();
            this.checkedListBox対象船 = new System.Windows.Forms.CheckedListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_表示順 = new System.Windows.Forms.TextBox();
            this.checkBox_DestributionFlag = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(253, 463);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 14;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(172, 463);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 13;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(91, 463);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 12;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 325);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "※部署";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 351);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "※金額";
            // 
            // textBox金額
            // 
            this.textBox金額.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox金額.Location = new System.Drawing.Point(120, 348);
            this.textBox金額.MaxLength = 6;
            this.textBox金額.Name = "textBox金額";
            this.textBox金額.Size = new System.Drawing.Size(167, 19);
            this.textBox金額.TabIndex = 5;
            this.textBox金額.TextChanged += new System.EventHandler(this.Edited);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 376);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "※作業担当者";
            // 
            // comboBox職名
            // 
            this.comboBox職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名.FormattingEnabled = true;
            this.comboBox職名.Location = new System.Drawing.Point(120, 373);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(184, 20);
            this.comboBox職名.TabIndex = 6;
            // 
            // comboBox部署
            // 
            this.comboBox部署.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox部署.FormattingEnabled = true;
            this.comboBox部署.Location = new System.Drawing.Point(120, 322);
            this.comboBox部署.Name = "comboBox部署";
            this.comboBox部署.Size = new System.Drawing.Size(184, 20);
            this.comboBox部署.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "※手当名";
            // 
            // textBox手当名
            // 
            this.textBox手当名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox手当名.Location = new System.Drawing.Point(120, 31);
            this.textBox手当名.MaxLength = 50;
            this.textBox手当名.Name = "textBox手当名";
            this.textBox手当名.Size = new System.Drawing.Size(225, 19);
            this.textBox手当名.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "作業内容";
            // 
            // textBox作業内容
            // 
            this.textBox作業内容.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox作業内容.Location = new System.Drawing.Point(120, 56);
            this.textBox作業内容.MaxLength = 500;
            this.textBox作業内容.Multiline = true;
            this.textBox作業内容.Name = "textBox作業内容";
            this.textBox作業内容.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox作業内容.Size = new System.Drawing.Size(243, 124);
            this.textBox作業内容.TabIndex = 2;
            // 
            // checkedListBox対象船
            // 
            this.checkedListBox対象船.FormattingEnabled = true;
            this.checkedListBox対象船.Location = new System.Drawing.Point(120, 186);
            this.checkedListBox対象船.Name = "checkedListBox対象船";
            this.checkedListBox対象船.Size = new System.Drawing.Size(243, 130);
            this.checkedListBox対象船.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "※対象船";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 402);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "　表示順";
            // 
            // textBox_表示順
            // 
            this.textBox_表示順.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox_表示順.Location = new System.Drawing.Point(120, 399);
            this.textBox_表示順.MaxLength = 3;
            this.textBox_表示順.Name = "textBox_表示順";
            this.textBox_表示順.Size = new System.Drawing.Size(46, 19);
            this.textBox_表示順.TabIndex = 7;
            this.textBox_表示順.TextChanged += new System.EventHandler(this.Edited);
            // 
            // checkBox_DestributionFlag
            // 
            this.checkBox_DestributionFlag.AutoSize = true;
            this.checkBox_DestributionFlag.Location = new System.Drawing.Point(120, 425);
            this.checkBox_DestributionFlag.Name = "checkBox_DestributionFlag";
            this.checkBox_DestributionFlag.Size = new System.Drawing.Size(96, 16);
            this.checkBox_DestributionFlag.TabIndex = 9;
            this.checkBox_DestributionFlag.Text = "乗船日数按分";
            this.checkBox_DestributionFlag.UseVisualStyleBackColor = true;
            // 
            // Tek手当管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(419, 498);
            this.Controls.Add(this.checkBox_DestributionFlag);
            this.Controls.Add(this.checkedListBox対象船);
            this.Controls.Add(this.textBox作業内容);
            this.Controls.Add(this.textBox手当名);
            this.Controls.Add(this.comboBox職名);
            this.Controls.Add(this.comboBox部署);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.textBox_表示順);
            this.Controls.Add(this.textBox金額);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "Tek手当管理詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "手当管理詳細Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox金額;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox職名;
        private System.Windows.Forms.ComboBox comboBox部署;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox手当名;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox作業内容;
        private System.Windows.Forms.CheckedListBox checkedListBox対象船;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_表示順;
        private System.Windows.Forms.CheckBox checkBox_DestributionFlag;
    }
}