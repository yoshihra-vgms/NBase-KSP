namespace Senin
{
    partial class 基本給詳細Form
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
            this.dateTimePicker開始日 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox標令 = new System.Windows.Forms.TextBox();
            this.textBox職務給 = new System.Windows.Forms.TextBox();
            this.button更新 = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox基本給 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox職務 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox標令給 = new System.Windows.Forms.TextBox();
            this.textBox組合費 = new System.Windows.Forms.TextBox();
            this.textBox経験年数 = new System.Windows.Forms.TextBox();
            this.textBox資格給 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_SharedRemarks = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_FiscalYearRemarks = new System.Windows.Forms.TextBox();
            this.nullableDateTimePicker終了日 = new NBaseUtil.NullableDateTimePicker();
            this.SuspendLayout();
            // 
            // dateTimePicker開始日
            // 
            this.dateTimePicker開始日.Location = new System.Drawing.Point(90, 33);
            this.dateTimePicker開始日.Name = "dateTimePicker開始日";
            this.dateTimePicker開始日.Size = new System.Drawing.Size(127, 19);
            this.dateTimePicker開始日.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "※期間";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "※標令";
            // 
            // textBox標令
            // 
            this.textBox標令.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox標令.Location = new System.Drawing.Point(90, 77);
            this.textBox標令.MaxLength = 7;
            this.textBox標令.Name = "textBox標令";
            this.textBox標令.Size = new System.Drawing.Size(104, 19);
            this.textBox標令.TabIndex = 4;
            this.textBox標令.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox標令.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Decimal_KeyPress);
            this.textBox標令.Leave += new System.EventHandler(this.textBox標令_Leave);
            // 
            // textBox職務給
            // 
            this.textBox職務給.BackColor = System.Drawing.SystemColors.ControlDark;
            this.textBox職務給.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox職務給.Location = new System.Drawing.Point(271, 103);
            this.textBox職務給.Name = "textBox職務給";
            this.textBox職務給.Size = new System.Drawing.Size(127, 19);
            this.textBox職務給.TabIndex = 8;
            this.textBox職務給.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox職務給.Visible = false;
            this.textBox職務給.Leave += new System.EventHandler(this.textBox職務給_Leave);
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(90, 473);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 20;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(178, 473);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 21;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(266, 473);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 22;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(217, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "基本給";
            // 
            // textBox基本給
            // 
            this.textBox基本給.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox基本給.Location = new System.Drawing.Point(271, 166);
            this.textBox基本給.MaxLength = 6;
            this.textBox基本給.Name = "textBox基本給";
            this.textBox基本給.Size = new System.Drawing.Size(127, 19);
            this.textBox基本給.TabIndex = 10;
            this.textBox基本給.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox基本給.Leave += new System.EventHandler(this.textBox基本給_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(217, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "組合費";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(223, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "～";
            // 
            // comboBox職務
            // 
            this.comboBox職務.BackColor = System.Drawing.SystemColors.ControlDark;
            this.comboBox職務.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職務.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox職務.FormattingEnabled = true;
            this.comboBox職務.Location = new System.Drawing.Point(90, 102);
            this.comboBox職務.Name = "comboBox職務";
            this.comboBox職務.Size = new System.Drawing.Size(103, 21);
            this.comboBox職務.TabIndex = 6;
            this.comboBox職務.Visible = false;
            this.comboBox職務.SelectedIndexChanged += new System.EventHandler(this.comboBox職務_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label8.Location = new System.Drawing.Point(17, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "※職務";
            this.label8.Visible = false;
            // 
            // textBox標令給
            // 
            this.textBox標令給.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox標令給.Location = new System.Drawing.Point(271, 77);
            this.textBox標令給.Name = "textBox標令給";
            this.textBox標令給.Size = new System.Drawing.Size(127, 19);
            this.textBox標令給.TabIndex = 5;
            this.textBox標令給.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox標令給.Leave += new System.EventHandler(this.textBox標令給_Leave);
            // 
            // textBox組合費
            // 
            this.textBox組合費.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox組合費.Location = new System.Drawing.Point(271, 191);
            this.textBox組合費.MaxLength = 6;
            this.textBox組合費.Name = "textBox組合費";
            this.textBox組合費.Size = new System.Drawing.Size(127, 19);
            this.textBox組合費.TabIndex = 11;
            this.textBox組合費.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox組合費.Leave += new System.EventHandler(this.textBox組合費_Leave);
            // 
            // textBox経験年数
            // 
            this.textBox経験年数.BackColor = System.Drawing.SystemColors.ControlDark;
            this.textBox経験年数.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox経験年数.Location = new System.Drawing.Point(199, 104);
            this.textBox経験年数.MaxLength = 6;
            this.textBox経験年数.Name = "textBox経験年数";
            this.textBox経験年数.Size = new System.Drawing.Size(59, 19);
            this.textBox経験年数.TabIndex = 7;
            this.textBox経験年数.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox経験年数.Visible = false;
            this.textBox経験年数.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Decimal_KeyPress);
            this.textBox経験年数.Leave += new System.EventHandler(this.textBox経験年数_Leave);
            // 
            // textBox資格給
            // 
            this.textBox資格給.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox資格給.Location = new System.Drawing.Point(271, 141);
            this.textBox資格給.MaxLength = 6;
            this.textBox資格給.Name = "textBox資格給";
            this.textBox資格給.Size = new System.Drawing.Size(127, 19);
            this.textBox資格給.TabIndex = 9;
            this.textBox資格給.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox資格給.Leave += new System.EventHandler(this.textBox資格給_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "資格給";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 221);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "共有備考";
            // 
            // textBox_SharedRemarks
            // 
            this.textBox_SharedRemarks.AcceptsTab = true;
            this.textBox_SharedRemarks.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_SharedRemarks.Location = new System.Drawing.Point(31, 236);
            this.textBox_SharedRemarks.MaxLength = 200;
            this.textBox_SharedRemarks.Multiline = true;
            this.textBox_SharedRemarks.Name = "textBox_SharedRemarks";
            this.textBox_SharedRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_SharedRemarks.Size = new System.Drawing.Size(369, 76);
            this.textBox_SharedRemarks.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(29, 328);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "対象年度備考";
            // 
            // textBox_FiscalYearRemarks
            // 
            this.textBox_FiscalYearRemarks.AcceptsTab = true;
            this.textBox_FiscalYearRemarks.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_FiscalYearRemarks.Location = new System.Drawing.Point(31, 343);
            this.textBox_FiscalYearRemarks.MaxLength = 200;
            this.textBox_FiscalYearRemarks.Multiline = true;
            this.textBox_FiscalYearRemarks.Name = "textBox_FiscalYearRemarks";
            this.textBox_FiscalYearRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_FiscalYearRemarks.Size = new System.Drawing.Size(369, 76);
            this.textBox_FiscalYearRemarks.TabIndex = 13;
            // 
            // nullableDateTimePicker終了日
            // 
            this.nullableDateTimePicker終了日.Location = new System.Drawing.Point(246, 33);
            this.nullableDateTimePicker終了日.Name = "nullableDateTimePicker終了日";
            this.nullableDateTimePicker終了日.Size = new System.Drawing.Size(127, 19);
            this.nullableDateTimePicker終了日.TabIndex = 3;
            this.nullableDateTimePicker終了日.Value = new System.DateTime(2021, 11, 17, 17, 16, 4, 451);
            // 
            // 基本給詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(430, 508);
            this.Controls.Add(this.textBox_FiscalYearRemarks);
            this.Controls.Add(this.textBox_SharedRemarks);
            this.Controls.Add(this.nullableDateTimePicker終了日);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox組合費);
            this.Controls.Add(this.textBox経験年数);
            this.Controls.Add(this.textBox資格給);
            this.Controls.Add(this.textBox基本給);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.textBox標令給);
            this.Controls.Add(this.textBox職務給);
            this.Controls.Add(this.textBox標令);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker開始日);
            this.Controls.Add(this.comboBox職務);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "基本給詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "基本給詳細";
            this.Load += new System.EventHandler(this.基本給詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimePicker開始日;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox標令;
        private System.Windows.Forms.TextBox textBox職務給;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox基本給;
        private System.Windows.Forms.Label label7;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker終了日;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox職務;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox標令給;
        private System.Windows.Forms.TextBox textBox組合費;
        private System.Windows.Forms.TextBox textBox経験年数;
        private System.Windows.Forms.TextBox textBox資格給;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_SharedRemarks;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_FiscalYearRemarks;
    }
}