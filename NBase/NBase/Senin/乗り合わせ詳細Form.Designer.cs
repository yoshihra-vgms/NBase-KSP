namespace Senin
{
    partial class 乗り合わせ詳細Form
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
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button船員検索1 = new System.Windows.Forms.Button();
            this.textBox船員1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox職名1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button削除 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button更新 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox船員2 = new System.Windows.Forms.TextBox();
            this.button船員検索2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox職名2 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox備考.Location = new System.Drawing.Point(120, 145);
            this.textBox備考.MaxLength = 500;
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.Size = new System.Drawing.Size(268, 67);
            this.textBox備考.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 9;
            this.label9.Text = "備考";
            // 
            // button船員検索1
            // 
            this.button船員検索1.BackColor = System.Drawing.SystemColors.Control;
            this.button船員検索1.Location = new System.Drawing.Point(279, 41);
            this.button船員検索1.Name = "button船員検索1";
            this.button船員検索1.Size = new System.Drawing.Size(75, 23);
            this.button船員検索1.TabIndex = 13;
            this.button船員検索1.Text = "船員検索";
            this.button船員検索1.UseVisualStyleBackColor = false;
            this.button船員検索1.Click += new System.EventHandler(this.button船員検索_Click);
            // 
            // textBox船員1
            // 
            this.textBox船員1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox船員1.Location = new System.Drawing.Point(120, 43);
            this.textBox船員1.MaxLength = 100;
            this.textBox船員1.Name = "textBox船員1";
            this.textBox船員1.ReadOnly = true;
            this.textBox船員1.Size = new System.Drawing.Size(150, 19);
            this.textBox船員1.TabIndex = 11;
            this.textBox船員1.TabStop = false;
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
            // comboBox職名1
            // 
            this.comboBox職名1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名1.FormattingEnabled = true;
            this.comboBox職名1.Location = new System.Drawing.Point(120, 12);
            this.comboBox職名1.Name = "comboBox職名1";
            this.comboBox職名1.Size = new System.Drawing.Size(150, 20);
            this.comboBox職名1.TabIndex = 33;
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
            this.button削除.Location = new System.Drawing.Point(180, 233);
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
            this.button閉じる.Location = new System.Drawing.Point(261, 233);
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
            this.button更新.Location = new System.Drawing.Point(99, 233);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 35;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "※氏名";
            // 
            // textBox船員2
            // 
            this.textBox船員2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox船員2.Location = new System.Drawing.Point(120, 109);
            this.textBox船員2.MaxLength = 100;
            this.textBox船員2.Name = "textBox船員2";
            this.textBox船員2.ReadOnly = true;
            this.textBox船員2.Size = new System.Drawing.Size(150, 19);
            this.textBox船員2.TabIndex = 11;
            this.textBox船員2.TabStop = false;
            // 
            // button船員検索2
            // 
            this.button船員検索2.BackColor = System.Drawing.SystemColors.Control;
            this.button船員検索2.Location = new System.Drawing.Point(279, 107);
            this.button船員検索2.Name = "button船員検索2";
            this.button船員検索2.Size = new System.Drawing.Size(75, 23);
            this.button船員検索2.TabIndex = 13;
            this.button船員検索2.Text = "船員検索";
            this.button船員検索2.UseVisualStyleBackColor = false;
            this.button船員検索2.Click += new System.EventHandler(this.button船員検索_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 34;
            this.label3.Text = "※職名";
            // 
            // comboBox職名2
            // 
            this.comboBox職名2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名2.FormattingEnabled = true;
            this.comboBox職名2.Location = new System.Drawing.Point(120, 78);
            this.comboBox職名2.Name = "comboBox職名2";
            this.comboBox職名2.Size = new System.Drawing.Size(150, 20);
            this.comboBox職名2.TabIndex = 33;
            // 
            // 乗り合わせ詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(435, 268);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.comboBox職名2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox職名1);
            this.Controls.Add(this.button船員検索2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox船員2);
            this.Controls.Add(this.button船員検索1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox船員1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox備考);
            this.Controls.Add(this.label9);
            this.Name = "乗り合わせ詳細Form";
            this.Text = "乗り合わせ詳細Form";
            this.Load += new System.EventHandler(this.乗り合わせ詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button船員検索1;
        private System.Windows.Forms.TextBox textBox船員1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox職名1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox船員2;
        private System.Windows.Forms.Button button船員検索2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox職名2;
    }
}