namespace NBaseMaster.船員管理
{
    partial class 明細管理詳細Form
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
            this.textBox明細名 = new System.Windows.Forms.TextBox();
            this.comboBox費用科目名 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox大項目名 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox明細科目名 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(191, 122);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 7;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button削除
            // 
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(110, 122);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 6;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button更新
            // 
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(29, 122);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 5;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "※費用科目名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "※明細名";
            // 
            // textBox明細名
            // 
            this.textBox明細名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox明細名.Location = new System.Drawing.Point(101, 63);
            this.textBox明細名.MaxLength = 20;
            this.textBox明細名.Name = "textBox明細名";
            this.textBox明細名.Size = new System.Drawing.Size(167, 19);
            this.textBox明細名.TabIndex = 3;
            this.textBox明細名.TextChanged += new System.EventHandler(this.ComboChange);
            // 
            // comboBox費用科目名
            // 
            this.comboBox費用科目名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox費用科目名.FormattingEnabled = true;
            this.comboBox費用科目名.Location = new System.Drawing.Point(101, 12);
            this.comboBox費用科目名.Name = "comboBox費用科目名";
            this.comboBox費用科目名.Size = new System.Drawing.Size(184, 20);
            this.comboBox費用科目名.TabIndex = 1;
            this.comboBox費用科目名.SelectedIndexChanged += new System.EventHandler(this.comboBox費用科目名_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "※大項目名";
            // 
            // comboBox大項目名
            // 
            this.comboBox大項目名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox大項目名.FormattingEnabled = true;
            this.comboBox大項目名.Location = new System.Drawing.Point(101, 37);
            this.comboBox大項目名.Name = "comboBox大項目名";
            this.comboBox大項目名.Size = new System.Drawing.Size(184, 20);
            this.comboBox大項目名.TabIndex = 2;
            this.comboBox大項目名.SelectedIndexChanged += new System.EventHandler(this.ComboChange);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 26;
            this.label3.Text = "明細科目名";
            // 
            // comboBox明細科目名
            // 
            this.comboBox明細科目名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox明細科目名.FormattingEnabled = true;
            this.comboBox明細科目名.Location = new System.Drawing.Point(101, 88);
            this.comboBox明細科目名.Name = "comboBox明細科目名";
            this.comboBox明細科目名.Size = new System.Drawing.Size(184, 20);
            this.comboBox明細科目名.TabIndex = 4;
            this.comboBox明細科目名.SelectedIndexChanged += new System.EventHandler(this.ComboChange);
            // 
            // 明細管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(294, 157);
            this.Controls.Add(this.comboBox明細科目名);
            this.Controls.Add(this.comboBox大項目名);
            this.Controls.Add(this.comboBox費用科目名);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.textBox明細名);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "明細管理詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "明細管理詳細Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox明細名;
        private System.Windows.Forms.ComboBox comboBox費用科目名;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox大項目名;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox明細科目名;
    }
}