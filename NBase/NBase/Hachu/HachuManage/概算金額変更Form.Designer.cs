namespace Hachu.HachuManage
{
    partial class 概算金額変更Form
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
            this.textBox概算金額 = new System.Windows.Forms.TextBox();
            this.textBox変更理由 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button変更 = new System.Windows.Forms.Button();
            this.buttonキャンセル = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "概算金額";
            // 
            // textBox概算金額
            // 
            this.textBox概算金額.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox概算金額.Location = new System.Drawing.Point(81, 15);
            this.textBox概算金額.MaxLength = 9;
            this.textBox概算金額.Name = "textBox概算金額";
            this.textBox概算金額.Size = new System.Drawing.Size(100, 19);
            this.textBox概算金額.TabIndex = 1;
            // 
            // textBox変更理由
            // 
            this.textBox変更理由.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox変更理由.Location = new System.Drawing.Point(81, 40);
            this.textBox変更理由.MaxLength = 500;
            this.textBox変更理由.Multiline = true;
            this.textBox変更理由.Name = "textBox変更理由";
            this.textBox変更理由.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox変更理由.Size = new System.Drawing.Size(268, 118);
            this.textBox変更理由.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "変更理由";
            // 
            // button変更
            // 
            this.button変更.Location = new System.Drawing.Point(106, 182);
            this.button変更.Name = "button変更";
            this.button変更.Size = new System.Drawing.Size(75, 23);
            this.button変更.TabIndex = 4;
            this.button変更.Text = "変更";
            this.button変更.UseVisualStyleBackColor = true;
            this.button変更.Click += new System.EventHandler(this.button変更_Click);
            // 
            // buttonキャンセル
            // 
            this.buttonキャンセル.Location = new System.Drawing.Point(203, 182);
            this.buttonキャンセル.Name = "buttonキャンセル";
            this.buttonキャンセル.Size = new System.Drawing.Size(75, 23);
            this.buttonキャンセル.TabIndex = 5;
            this.buttonキャンセル.Text = "キャンセル";
            this.buttonキャンセル.UseVisualStyleBackColor = true;
            this.buttonキャンセル.Click += new System.EventHandler(this.buttonキャンセル_Click);
            // 
            // 概算金額変更Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 225);
            this.Controls.Add(this.buttonキャンセル);
            this.Controls.Add(this.button変更);
            this.Controls.Add(this.textBox変更理由);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox概算金額);
            this.Controls.Add(this.label1);
            this.Name = "概算金額変更Form";
            this.Text = "概算金額変更Form";
            this.Load += new System.EventHandler(this.概算金額変更Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox概算金額;
        private System.Windows.Forms.TextBox textBox変更理由;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button変更;
        private System.Windows.Forms.Button buttonキャンセル;
    }
}