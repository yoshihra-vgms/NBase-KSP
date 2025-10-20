namespace Senin
{
    partial class 給与手当登録Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox職名 = new System.Windows.Forms.ComboBox();
            this.comboBox給与手当 = new System.Windows.Forms.ComboBox();
            this.textBox単価 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox船員 = new System.Windows.Forms.ComboBox();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.button更新 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox金額 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox日数 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.nullableDateTimePicker1 = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker2 = new NBaseUtil.NullableDateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "※氏名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "※職名";
            // 
            // comboBox職名
            // 
            this.comboBox職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名.FormattingEnabled = true;
            this.comboBox職名.Location = new System.Drawing.Point(109, 67);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(150, 20);
            this.comboBox職名.TabIndex = 1;
            this.comboBox職名.SelectedIndexChanged += new System.EventHandler(this.comboBox職名_SelectedIndexChanged);
            // 
            // comboBox給与手当
            // 
            this.comboBox給与手当.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox給与手当.FormattingEnabled = true;
            this.comboBox給与手当.Location = new System.Drawing.Point(109, 144);
            this.comboBox給与手当.Name = "comboBox給与手当";
            this.comboBox給与手当.Size = new System.Drawing.Size(184, 20);
            this.comboBox給与手当.TabIndex = 3;
            this.comboBox給与手当.SelectedIndexChanged += new System.EventHandler(this.comboBox給与手当_SelectedIndexChanged);
            // 
            // textBox単価
            // 
            this.textBox単価.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox単価.Location = new System.Drawing.Point(109, 175);
            this.textBox単価.MaxLength = 6;
            this.textBox単価.Name = "textBox単価";
            this.textBox単価.ReadOnly = true;
            this.textBox単価.Size = new System.Drawing.Size(94, 19);
            this.textBox単価.TabIndex = 0;
            this.textBox単価.TabStop = false;
            this.textBox単価.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "単価";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "※給与手当";
            // 
            // comboBox船員
            // 
            this.comboBox船員.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船員.FormattingEnabled = true;
            this.comboBox船員.Location = new System.Drawing.Point(109, 98);
            this.comboBox船員.Name = "comboBox船員";
            this.comboBox船員.Size = new System.Drawing.Size(150, 20);
            this.comboBox船員.TabIndex = 2;
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(258, 334);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 10;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(177, 334);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 9;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(96, 334);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 8;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "期間";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(241, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "～";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 268);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "※金額";
            // 
            // textBox金額
            // 
            this.textBox金額.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox金額.Location = new System.Drawing.Point(109, 265);
            this.textBox金額.MaxLength = 6;
            this.textBox金額.Name = "textBox金額";
            this.textBox金額.Size = new System.Drawing.Size(94, 19);
            this.textBox金額.TabIndex = 7;
            this.textBox金額.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(48, 238);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "日数";
            // 
            // textBox日数
            // 
            this.textBox日数.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox日数.Location = new System.Drawing.Point(109, 235);
            this.textBox日数.MaxLength = 2;
            this.textBox日数.Name = "textBox日数";
            this.textBox日数.Size = new System.Drawing.Size(61, 19);
            this.textBox日数.TabIndex = 6;
            this.textBox日数.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox日数.TextChanged += new System.EventHandler(this.textBox日数_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(174, 238);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 30;
            this.label9.Text = "日間";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.BackColor = System.Drawing.Color.White;
            this.textBoxTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTitle.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxTitle.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBoxTitle.Location = new System.Drawing.Point(34, 26);
            this.textBoxTitle.MaxLength = 6;
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.ReadOnly = true;
            this.textBoxTitle.Size = new System.Drawing.Size(376, 13);
            this.textBoxTitle.TabIndex = 0;
            this.textBoxTitle.TabStop = false;
            this.textBoxTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nullableDateTimePicker1
            // 
            this.nullableDateTimePicker1.Location = new System.Drawing.Point(109, 205);
            this.nullableDateTimePicker1.Name = "nullableDateTimePicker1";
            this.nullableDateTimePicker1.Size = new System.Drawing.Size(125, 19);
            this.nullableDateTimePicker1.TabIndex = 31;
            this.nullableDateTimePicker1.Value = new System.DateTime(2018, 1, 30, 15, 1, 30, 632);
            this.nullableDateTimePicker1.ValueChanged += new System.EventHandler(this.nullableDateTimePicker_ValueChanged);
            // 
            // nullableDateTimePicker2
            // 
            this.nullableDateTimePicker2.Location = new System.Drawing.Point(266, 206);
            this.nullableDateTimePicker2.Name = "nullableDateTimePicker2";
            this.nullableDateTimePicker2.Size = new System.Drawing.Size(125, 19);
            this.nullableDateTimePicker2.TabIndex = 31;
            this.nullableDateTimePicker2.Value = new System.DateTime(2018, 1, 30, 15, 1, 30, 632);
            this.nullableDateTimePicker2.ValueChanged += new System.EventHandler(this.nullableDateTimePicker_ValueChanged);
            // 
            // 給与手当登録Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(445, 369);
            this.Controls.Add(this.nullableDateTimePicker2);
            this.Controls.Add(this.nullableDateTimePicker1);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.comboBox船員);
            this.Controls.Add(this.comboBox給与手当);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.textBox金額);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox日数);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox単価);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox職名);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "給与手当登録Form";
            this.Text = "給与手当登録Form";
            this.Load += new System.EventHandler(this.給与手当登録Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox職名;
        private System.Windows.Forms.ComboBox comboBox給与手当;
        private System.Windows.Forms.TextBox textBox単価;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox船員;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox金額;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox日数;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxTitle;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker1;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker2;
    }
}