namespace Yojitsu
{
    partial class 特別修繕引当金入力Form
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
            this.button設定 = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox総額 = new System.Windows.Forms.TextBox();
            this.comboBox月 = new System.Windows.Forms.ComboBox();
            this.comboBox年 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button設定
            // 
            this.button設定.BackColor = System.Drawing.SystemColors.Control;
            this.button設定.Location = new System.Drawing.Point(77, 93);
            this.button設定.Name = "button設定";
            this.button設定.Size = new System.Drawing.Size(75, 23);
            this.button設定.TabIndex = 2;
            this.button設定.Text = "設定";
            this.button設定.UseVisualStyleBackColor = false;
            this.button設定.Click += new System.EventHandler(this.button入力_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(158, 93);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "閉じる";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "費用総額";
            // 
            // textBox総額
            // 
            this.textBox総額.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox総額.Location = new System.Drawing.Point(170, 47);
            this.textBox総額.Name = "textBox総額";
            this.textBox総額.Size = new System.Drawing.Size(87, 19);
            this.textBox総額.TabIndex = 7;
            this.textBox総額.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // comboBox月
            // 
            this.comboBox月.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox月.FormattingEnabled = true;
            this.comboBox月.Location = new System.Drawing.Point(219, 16);
            this.comboBox月.Name = "comboBox月";
            this.comboBox月.Size = new System.Drawing.Size(38, 20);
            this.comboBox月.TabIndex = 11;
            // 
            // comboBox年
            // 
            this.comboBox年.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年.FormattingEnabled = true;
            this.comboBox年.Location = new System.Drawing.Point(147, 16);
            this.comboBox年.Name = "comboBox年";
            this.comboBox年.Size = new System.Drawing.Size(61, 20);
            this.comboBox年.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "定期検査年月";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(208, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "/";
            // 
            // 特別修繕引当金入力Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(310, 128);
            this.Controls.Add(this.comboBox月);
            this.Controls.Add(this.comboBox年);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox総額);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.button設定);
            this.Name = "特別修繕引当金入力Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "特別修繕引当金入力";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button設定;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox総額;
        private System.Windows.Forms.ComboBox comboBox月;
        private System.Windows.Forms.ComboBox comboBox年;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
    }
}