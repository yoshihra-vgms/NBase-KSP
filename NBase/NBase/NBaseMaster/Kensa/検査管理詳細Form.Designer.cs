namespace NBaseMaster.Kensa
{
    partial class 検査管理詳細Form
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
            this.KensaNameLabel = new System.Windows.Forms.Label();
            this.KankakuLabel = new System.Windows.Forms.Label();
            this.KensaNameText = new System.Windows.Forms.TextBox();
            this.KankakuText = new System.Windows.Forms.TextBox();
            this.YearLabel = new System.Windows.Forms.Label();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // KensaNameLabel
            // 
            this.KensaNameLabel.AutoSize = true;
            this.KensaNameLabel.Location = new System.Drawing.Point(12, 26);
            this.KensaNameLabel.Name = "KensaNameLabel";
            this.KensaNameLabel.Size = new System.Drawing.Size(53, 12);
            this.KensaNameLabel.TabIndex = 0;
            this.KensaNameLabel.Text = "※検査名";
            // 
            // KankakuLabel
            // 
            this.KankakuLabel.AutoSize = true;
            this.KankakuLabel.Location = new System.Drawing.Point(12, 63);
            this.KankakuLabel.Name = "KankakuLabel";
            this.KankakuLabel.Size = new System.Drawing.Size(41, 12);
            this.KankakuLabel.TabIndex = 1;
            this.KankakuLabel.Text = "※間隔";
            // 
            // KensaNameText
            // 
            this.KensaNameText.Location = new System.Drawing.Point(71, 23);
            this.KensaNameText.MaxLength = 50;
            this.KensaNameText.Name = "KensaNameText";
            this.KensaNameText.Size = new System.Drawing.Size(142, 19);
            this.KensaNameText.TabIndex = 2;
            this.KensaNameText.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // KankakuText
            // 
            this.KankakuText.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KankakuText.Location = new System.Drawing.Point(71, 60);
            this.KankakuText.MaxLength = 9;
            this.KankakuText.Name = "KankakuText";
            this.KankakuText.Size = new System.Drawing.Size(142, 19);
            this.KankakuText.TabIndex = 3;
            this.KankakuText.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // YearLabel
            // 
            this.YearLabel.AutoSize = true;
            this.YearLabel.Location = new System.Drawing.Point(219, 63);
            this.YearLabel.Name = "YearLabel";
            this.YearLabel.Size = new System.Drawing.Size(17, 12);
            this.YearLabel.TabIndex = 4;
            this.YearLabel.Text = "年";
            // 
            // UpdateButton
            // 
            this.UpdateButton.BackColor = System.Drawing.SystemColors.Control;
            this.UpdateButton.Location = new System.Drawing.Point(32, 101);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateButton.TabIndex = 5;
            this.UpdateButton.Text = "更新";
            this.UpdateButton.UseVisualStyleBackColor = false;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.DeleteButton.Location = new System.Drawing.Point(113, 101);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 6;
            this.DeleteButton.Text = "削除";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.SystemColors.Control;
            this.CloseButton.Location = new System.Drawing.Point(194, 101);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 7;
            this.CloseButton.Text = "閉じる";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // 検査管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 136);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.YearLabel);
            this.Controls.Add(this.KankakuText);
            this.Controls.Add(this.KensaNameText);
            this.Controls.Add(this.KankakuLabel);
            this.Controls.Add(this.KensaNameLabel);
            this.Name = "検査管理詳細Form";
            this.Text = "検査管理詳細Form";
            this.Load += new System.EventHandler(this.検査管理詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label KensaNameLabel;
        private System.Windows.Forms.Label KankakuLabel;
        private System.Windows.Forms.TextBox KensaNameText;
        private System.Windows.Forms.TextBox KankakuText;
        private System.Windows.Forms.Label YearLabel;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button CloseButton;
    }
}