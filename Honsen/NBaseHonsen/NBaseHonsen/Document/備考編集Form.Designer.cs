namespace NBaseHonsen.Document
{
    partial class 備考編集Form
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
            this.button_閉じる = new System.Windows.Forms.Button();
            this.button_登録 = new System.Windows.Forms.Button();
            this.textBox_Bikou = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_閉じる
            // 
            this.button_閉じる.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_閉じる.Location = new System.Drawing.Point(306, 175);
            this.button_閉じる.Name = "button_閉じる";
            this.button_閉じる.Size = new System.Drawing.Size(100, 32);
            this.button_閉じる.TabIndex = 14;
            this.button_閉じる.Text = "閉じる";
            this.button_閉じる.UseVisualStyleBackColor = true;
            this.button_閉じる.Click += new System.EventHandler(this.button_閉じる_Click);
            // 
            // button_登録
            // 
            this.button_登録.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_登録.Location = new System.Drawing.Point(189, 175);
            this.button_登録.Name = "button_登録";
            this.button_登録.Size = new System.Drawing.Size(100, 32);
            this.button_登録.TabIndex = 13;
            this.button_登録.Text = "登録";
            this.button_登録.UseVisualStyleBackColor = true;
            this.button_登録.Click += new System.EventHandler(this.button_登録_Click);
            // 
            // textBox_Bikou
            // 
            this.textBox_Bikou.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Bikou.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_Bikou.Location = new System.Drawing.Point(31, 47);
            this.textBox_Bikou.MaxLength = 100;
            this.textBox_Bikou.Multiline = true;
            this.textBox_Bikou.Name = "textBox_Bikou";
            this.textBox_Bikou.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Bikou.Size = new System.Drawing.Size(529, 109);
            this.textBox_Bikou.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.Location = new System.Drawing.Point(28, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 16);
            this.label12.TabIndex = 11;
            this.label12.Text = "備考";
            // 
            // 備考編集Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(588, 233);
            this.Controls.Add(this.button_閉じる);
            this.Controls.Add(this.button_登録);
            this.Controls.Add(this.textBox_Bikou);
            this.Controls.Add(this.label12);
            this.Name = "備考編集Form";
            this.Text = "備考編集";
            this.Load += new System.EventHandler(this.備考編集Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_閉じる;
        private System.Windows.Forms.Button button_登録;
        private System.Windows.Forms.TextBox textBox_Bikou;
        private System.Windows.Forms.Label label12;

    }
}