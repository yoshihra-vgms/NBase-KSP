namespace NBaseMaster.船員管理
{
    partial class 職務給Form2
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
            this.textBox支給額0 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button更新 = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.comboBox職名 = new System.Windows.Forms.ComboBox();
            this.textBox支給額1 = new System.Windows.Forms.TextBox();
            this.textBox支給額2 = new System.Windows.Forms.TextBox();
            this.textBox支給額3 = new System.Windows.Forms.TextBox();
            this.textBox支給額4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox支給額5 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "職務";
            // 
            // textBox支給額0
            // 
            this.textBox支給額0.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox支給額0.Location = new System.Drawing.Point(78, 92);
            this.textBox支給額0.MaxLength = 25;
            this.textBox支給額0.Name = "textBox支給額0";
            this.textBox支給額0.Size = new System.Drawing.Size(75, 19);
            this.textBox支給額0.TabIndex = 1;
            this.textBox支給額0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox支給額0.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "支給額";
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(344, 146);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 9;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(174, 146);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 7;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(259, 146);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 8;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // comboBox職名
            // 
            this.comboBox職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名.FormattingEnabled = true;
            this.comboBox職名.Location = new System.Drawing.Point(78, 27);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(128, 20);
            this.comboBox職名.TabIndex = 0;
            this.comboBox職名.SelectedIndexChanged += new System.EventHandler(this.DataChange);
            // 
            // textBox支給額1
            // 
            this.textBox支給額1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox支給額1.Location = new System.Drawing.Point(159, 92);
            this.textBox支給額1.MaxLength = 25;
            this.textBox支給額1.Name = "textBox支給額1";
            this.textBox支給額1.Size = new System.Drawing.Size(75, 19);
            this.textBox支給額1.TabIndex = 2;
            this.textBox支給額1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox支給額1.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // textBox支給額2
            // 
            this.textBox支給額2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox支給額2.Location = new System.Drawing.Point(240, 92);
            this.textBox支給額2.MaxLength = 25;
            this.textBox支給額2.Name = "textBox支給額2";
            this.textBox支給額2.Size = new System.Drawing.Size(75, 19);
            this.textBox支給額2.TabIndex = 3;
            this.textBox支給額2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox支給額2.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // textBox支給額3
            // 
            this.textBox支給額3.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox支給額3.Location = new System.Drawing.Point(321, 92);
            this.textBox支給額3.MaxLength = 25;
            this.textBox支給額3.Name = "textBox支給額3";
            this.textBox支給額3.Size = new System.Drawing.Size(75, 19);
            this.textBox支給額3.TabIndex = 4;
            this.textBox支給額3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox支給額3.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // textBox支給額4
            // 
            this.textBox支給額4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox支給額4.Location = new System.Drawing.Point(402, 92);
            this.textBox支給額4.MaxLength = 25;
            this.textBox支給額4.Name = "textBox支給額4";
            this.textBox支給額4.Size = new System.Drawing.Size(75, 19);
            this.textBox支給額4.TabIndex = 5;
            this.textBox支給額4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox支給額4.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "０年";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(184, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "１年";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(265, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "２年";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(346, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "３年";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(427, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "４年";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(494, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "５年以上";
            // 
            // textBox支給額5
            // 
            this.textBox支給額5.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox支給額5.Location = new System.Drawing.Point(481, 92);
            this.textBox支給額5.MaxLength = 25;
            this.textBox支給額5.Name = "textBox支給額5";
            this.textBox支給額5.Size = new System.Drawing.Size(75, 19);
            this.textBox支給額5.TabIndex = 6;
            this.textBox支給額5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox支給額5.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "勤続年数";
            // 
            // 職務給Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(592, 181);
            this.Controls.Add(this.comboBox職名);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.textBox支給額5);
            this.Controls.Add(this.textBox支給額4);
            this.Controls.Add(this.textBox支給額3);
            this.Controls.Add(this.textBox支給額2);
            this.Controls.Add(this.textBox支給額1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox支給額0);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(608, 220);
            this.MinimumSize = new System.Drawing.Size(608, 220);
            this.Name = "職務給Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "職務給";
            this.Load += new System.EventHandler(this.職務Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox支給額0;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.ComboBox comboBox職名;
        private System.Windows.Forms.TextBox textBox支給額1;
        private System.Windows.Forms.TextBox textBox支給額2;
        private System.Windows.Forms.TextBox textBox支給額3;
        private System.Windows.Forms.TextBox textBox支給額4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox支給額5;
        private System.Windows.Forms.Label label9;
    }
}