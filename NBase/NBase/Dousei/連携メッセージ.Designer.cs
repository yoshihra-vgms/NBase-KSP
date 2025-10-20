namespace Dousei
{
    partial class 連携メッセージ
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
            this.Message_textBox = new System.Windows.Forms.TextBox();
            this.Close_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "動静基幹連携エラーメッセージ";
            // 
            // Message_textBox
            // 
            this.Message_textBox.Location = new System.Drawing.Point(14, 24);
            this.Message_textBox.Multiline = true;
            this.Message_textBox.Name = "Message_textBox";
            this.Message_textBox.Size = new System.Drawing.Size(435, 269);
            this.Message_textBox.TabIndex = 1;
            // 
            // Close_button
            // 
            this.Close_button.BackColor = System.Drawing.SystemColors.Control;
            this.Close_button.Location = new System.Drawing.Point(198, 305);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(75, 23);
            this.Close_button.TabIndex = 2;
            this.Close_button.Text = "閉じる";
            this.Close_button.UseVisualStyleBackColor = false;
            this.Close_button.Click += new System.EventHandler(this.Close_button_Click);
            // 
            // 連携メッセージ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(467, 336);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Message_textBox);
            this.Controls.Add(this.label1);
            this.Name = "連携メッセージ";
            this.Text = "連携メッセージ";
            this.Load += new System.EventHandler(this.連携メッセージ_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Message_textBox;
        private System.Windows.Forms.Button Close_button;
    }
}