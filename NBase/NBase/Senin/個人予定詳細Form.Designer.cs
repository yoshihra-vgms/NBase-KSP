namespace Senin
{
    partial class 個人予定詳細Form
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
            this.textBox内容 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button船員検索 = new System.Windows.Forms.Button();
            this.textBox船員 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker終了日 = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker開始日 = new NBaseUtil.NullableDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.comboBox職名 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button削除 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button更新 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox内容
            // 
            this.textBox内容.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox内容.Location = new System.Drawing.Point(120, 104);
            this.textBox内容.MaxLength = 250;
            this.textBox内容.Multiline = true;
            this.textBox内容.Name = "textBox内容";
            this.textBox内容.Size = new System.Drawing.Size(268, 67);
            this.textBox内容.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 104);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 9;
            this.label9.Text = "内容";
            // 
            // button船員検索
            // 
            this.button船員検索.BackColor = System.Drawing.SystemColors.Control;
            this.button船員検索.Location = new System.Drawing.Point(279, 41);
            this.button船員検索.Name = "button船員検索";
            this.button船員検索.Size = new System.Drawing.Size(75, 23);
            this.button船員検索.TabIndex = 13;
            this.button船員検索.Text = "船員検索";
            this.button船員検索.UseVisualStyleBackColor = false;
            this.button船員検索.Click += new System.EventHandler(this.button船員検索_Click);
            // 
            // textBox船員
            // 
            this.textBox船員.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox船員.Location = new System.Drawing.Point(120, 43);
            this.textBox船員.MaxLength = 100;
            this.textBox船員.Name = "textBox船員";
            this.textBox船員.ReadOnly = true;
            this.textBox船員.Size = new System.Drawing.Size(150, 19);
            this.textBox船員.TabIndex = 11;
            this.textBox船員.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "※氏名";
            // 
            // nullableDateTimePicker終了日
            // 
            this.nullableDateTimePicker終了日.Location = new System.Drawing.Point(265, 73);
            this.nullableDateTimePicker終了日.Name = "nullableDateTimePicker終了日";
            this.nullableDateTimePicker終了日.Size = new System.Drawing.Size(123, 19);
            this.nullableDateTimePicker終了日.TabIndex = 16;
            this.nullableDateTimePicker終了日.Value = new System.DateTime(2012, 3, 23, 19, 23, 1, 923);
            // 
            // nullableDateTimePicker開始日
            // 
            this.nullableDateTimePicker開始日.Location = new System.Drawing.Point(120, 73);
            this.nullableDateTimePicker開始日.Name = "nullableDateTimePicker開始日";
            this.nullableDateTimePicker開始日.Size = new System.Drawing.Size(123, 19);
            this.nullableDateTimePicker開始日.TabIndex = 15;
            this.nullableDateTimePicker開始日.Value = new System.DateTime(2012, 3, 23, 19, 23, 1, 923);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "※乗船不可期間";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(246, 76);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(17, 12);
            this.label38.TabIndex = 32;
            this.label38.Text = "～";
            // 
            // comboBox職名
            // 
            this.comboBox職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名.FormattingEnabled = true;
            this.comboBox職名.Location = new System.Drawing.Point(120, 12);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(150, 20);
            this.comboBox職名.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 34;
            this.label1.Text = "※職名";
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(180, 210);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 36;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(261, 210);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 37;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(99, 210);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 35;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // 個人予定詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(435, 245);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.comboBox職名);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.nullableDateTimePicker終了日);
            this.Controls.Add(this.nullableDateTimePicker開始日);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button船員検索);
            this.Controls.Add(this.textBox船員);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox内容);
            this.Controls.Add(this.label9);
            this.Name = "個人予定詳細Form";
            this.Text = "個人予定詳細Form";
            this.Load += new System.EventHandler(this.個人予定詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox内容;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button船員検索;
        private System.Windows.Forms.TextBox textBox船員;
        private System.Windows.Forms.Label label10;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker終了日;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker開始日;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.ComboBox comboBox職名;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button更新;
    }
}