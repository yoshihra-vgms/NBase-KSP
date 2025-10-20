namespace NBaseMaster.船員管理
{
    partial class 明細科目管理詳細Form
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
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox課税 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton全社費用 = new System.Windows.Forms.RadioButton();
            this.radioButton船員費用 = new System.Windows.Forms.RadioButton();
            this.comboBox科目名 = new System.Windows.Forms.ComboBox();
            this.textBox明細科目名 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(262, 122);
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
            this.button削除.Location = new System.Drawing.Point(181, 122);
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
            this.button更新.Location = new System.Drawing.Point(100, 122);
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
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "勘定科目名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "費用種別";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "課税";
            // 
            // checkBox課税
            // 
            this.checkBox課税.AutoSize = true;
            this.checkBox課税.Location = new System.Drawing.Point(101, 65);
            this.checkBox課税.Name = "checkBox課税";
            this.checkBox課税.Size = new System.Drawing.Size(15, 14);
            this.checkBox課税.TabIndex = 3;
            this.checkBox課税.UseVisualStyleBackColor = true;
            this.checkBox課税.CheckedChanged += new System.EventHandler(this.DataChange);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton全社費用);
            this.panel1.Controls.Add(this.radioButton船員費用);
            this.panel1.Location = new System.Drawing.Point(99, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 25);
            this.panel1.TabIndex = 4;
            // 
            // radioButton全社費用
            // 
            this.radioButton全社費用.AutoSize = true;
            this.radioButton全社費用.Location = new System.Drawing.Point(92, 5);
            this.radioButton全社費用.Name = "radioButton全社費用";
            this.radioButton全社費用.Size = new System.Drawing.Size(71, 16);
            this.radioButton全社費用.TabIndex = 1;
            this.radioButton全社費用.TabStop = true;
            this.radioButton全社費用.Text = "全社費用";
            this.radioButton全社費用.UseVisualStyleBackColor = true;
            this.radioButton全社費用.CheckedChanged += new System.EventHandler(this.DataChange);
            // 
            // radioButton船員費用
            // 
            this.radioButton船員費用.AutoSize = true;
            this.radioButton船員費用.Location = new System.Drawing.Point(3, 5);
            this.radioButton船員費用.Name = "radioButton船員費用";
            this.radioButton船員費用.Size = new System.Drawing.Size(71, 16);
            this.radioButton船員費用.TabIndex = 0;
            this.radioButton船員費用.TabStop = true;
            this.radioButton船員費用.Text = "船員費用";
            this.radioButton船員費用.UseVisualStyleBackColor = true;
            this.radioButton船員費用.CheckedChanged += new System.EventHandler(this.DataChange);
            // 
            // comboBox科目名
            // 
            this.comboBox科目名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox科目名.FormattingEnabled = true;
            this.comboBox科目名.Location = new System.Drawing.Point(101, 12);
            this.comboBox科目名.Name = "comboBox科目名";
            this.comboBox科目名.Size = new System.Drawing.Size(308, 20);
            this.comboBox科目名.TabIndex = 1;
            this.comboBox科目名.SelectedIndexChanged += new System.EventHandler(this.DataChange);
            // 
            // textBox明細科目名
            // 
            this.textBox明細科目名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox明細科目名.Location = new System.Drawing.Point(101, 38);
            this.textBox明細科目名.MaxLength = 30;
            this.textBox明細科目名.Name = "textBox明細科目名";
            this.textBox明細科目名.Size = new System.Drawing.Size(308, 19);
            this.textBox明細科目名.TabIndex = 2;
            this.textBox明細科目名.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "※明細科目名";
            // 
            // 明細科目管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(436, 157);
            this.Controls.Add(this.textBox明細科目名);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox課税);
            this.Controls.Add(this.comboBox科目名);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "明細科目管理詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "明細科目管理詳細Form";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox課税;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton全社費用;
        private System.Windows.Forms.RadioButton radioButton船員費用;
        private System.Windows.Forms.ComboBox comboBox科目名;
        private System.Windows.Forms.TextBox textBox明細科目名;
        private System.Windows.Forms.Label label3;
    }
}