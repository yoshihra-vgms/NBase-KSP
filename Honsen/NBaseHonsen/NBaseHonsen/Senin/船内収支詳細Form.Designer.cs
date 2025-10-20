namespace NBaseHonsen.Senin
{
    partial class 船内収支詳細Form
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
            this.dateTimePicker日付 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox明細 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox金額 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.button更新 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox消費税 = new System.Windows.Forms.TextBox();
            this.textBox大項目 = new System.Windows.Forms.TextBox();
            this.textBox費用科目 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "日付";
            // 
            // dateTimePicker日付
            // 
            this.dateTimePicker日付.Location = new System.Drawing.Point(101, 22);
            this.dateTimePicker日付.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker日付.Name = "dateTimePicker日付";
            this.dateTimePicker日付.Size = new System.Drawing.Size(188, 23);
            this.dateTimePicker日付.TabIndex = 1;
            this.dateTimePicker日付.Value = new System.DateTime(2009, 7, 8, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "明細";
            // 
            // comboBox明細
            // 
            this.comboBox明細.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox明細.FormattingEnabled = true;
            this.comboBox明細.Location = new System.Drawing.Point(101, 53);
            this.comboBox明細.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox明細.MaxDropDownItems = 25;
            this.comboBox明細.Name = "comboBox明細";
            this.comboBox明細.Size = new System.Drawing.Size(320, 24);
            this.comboBox明細.TabIndex = 4;
            this.comboBox明細.SelectionChangeCommitted += new System.EventHandler(this.comboBox明細_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 153);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "金額";
            // 
            // textBox金額
            // 
            this.textBox金額.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox金額.Location = new System.Drawing.Point(101, 150);
            this.textBox金額.Margin = new System.Windows.Forms.Padding(4);
            this.textBox金額.MaxLength = 9;
            this.textBox金額.Name = "textBox金額";
            this.textBox金額.Size = new System.Drawing.Size(188, 23);
            this.textBox金額.TabIndex = 5;
            this.textBox金額.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 217);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "備考";
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox備考.Location = new System.Drawing.Point(101, 214);
            this.textBox備考.Margin = new System.Windows.Forms.Padding(4);
            this.textBox備考.MaxLength = 500;
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.Size = new System.Drawing.Size(412, 96);
            this.textBox備考.TabIndex = 7;
            // 
            // button更新
            // 
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button更新.Location = new System.Drawing.Point(79, 343);
            this.button更新.Margin = new System.Windows.Forms.Padding(4);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(138, 38);
            this.button更新.TabIndex = 8;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button閉じる.Location = new System.Drawing.Point(379, 343);
            this.button閉じる.Margin = new System.Windows.Forms.Padding(4);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(138, 38);
            this.button閉じる.TabIndex = 10;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button削除
            // 
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button削除.Location = new System.Drawing.Point(229, 343);
            this.button削除.Margin = new System.Windows.Forms.Padding(4);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(138, 38);
            this.button削除.TabIndex = 9;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 89);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "大項目";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 122);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "費用科目";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 185);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "消費税";
            // 
            // textBox消費税
            // 
            this.textBox消費税.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox消費税.Location = new System.Drawing.Point(101, 182);
            this.textBox消費税.Margin = new System.Windows.Forms.Padding(4);
            this.textBox消費税.MaxLength = 9;
            this.textBox消費税.Name = "textBox消費税";
            this.textBox消費税.ReadOnly = true;
            this.textBox消費税.Size = new System.Drawing.Size(188, 23);
            this.textBox消費税.TabIndex = 6;
            this.textBox消費税.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox大項目
            // 
            this.textBox大項目.Location = new System.Drawing.Point(101, 86);
            this.textBox大項目.Name = "textBox大項目";
            this.textBox大項目.ReadOnly = true;
            this.textBox大項目.Size = new System.Drawing.Size(320, 23);
            this.textBox大項目.TabIndex = 11;
            // 
            // textBox費用科目
            // 
            this.textBox費用科目.Location = new System.Drawing.Point(101, 119);
            this.textBox費用科目.Name = "textBox費用科目";
            this.textBox費用科目.ReadOnly = true;
            this.textBox費用科目.Size = new System.Drawing.Size(320, 23);
            this.textBox費用科目.TabIndex = 11;
            // 
            // 船内収支詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(597, 394);
            this.Controls.Add(this.textBox費用科目);
            this.Controls.Add(this.textBox大項目);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.textBox備考);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox消費税);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox金額);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox明細);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker日付);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "船内収支詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "船内収支詳細";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker日付;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox明細;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox金額;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox消費税;
        private System.Windows.Forms.TextBox textBox大項目;
        private System.Windows.Forms.TextBox textBox費用科目;
    }
}