namespace NBaseCommon
{
    partial class 船選択Form
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
            this.checkedListBox_船 = new System.Windows.Forms.CheckedListBox();
            this.button_設定 = new System.Windows.Forms.Button();
            this.button_キャンセル = new System.Windows.Forms.Button();
            this.button選択 = new System.Windows.Forms.Button();
            this.button解除 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox_船
            // 
            this.checkedListBox_船.CheckOnClick = true;
            this.checkedListBox_船.FormattingEnabled = true;
            this.checkedListBox_船.Location = new System.Drawing.Point(11, 40);
            this.checkedListBox_船.Name = "checkedListBox_船";
            this.checkedListBox_船.Size = new System.Drawing.Size(269, 228);
            this.checkedListBox_船.TabIndex = 6;
            // 
            // button_設定
            // 
            this.button_設定.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_設定.Location = new System.Drawing.Point(68, 284);
            this.button_設定.Name = "button_設定";
            this.button_設定.Size = new System.Drawing.Size(75, 23);
            this.button_設定.TabIndex = 7;
            this.button_設定.Text = "選択";
            this.button_設定.UseVisualStyleBackColor = true;
            // 
            // button_キャンセル
            // 
            this.button_キャンセル.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_キャンセル.Location = new System.Drawing.Point(149, 284);
            this.button_キャンセル.Name = "button_キャンセル";
            this.button_キャンセル.Size = new System.Drawing.Size(75, 23);
            this.button_キャンセル.TabIndex = 8;
            this.button_キャンセル.Text = "閉じる";
            this.button_キャンセル.UseVisualStyleBackColor = true;
            // 
            // button選択
            // 
            this.button選択.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button選択.Location = new System.Drawing.Point(11, 12);
            this.button選択.Name = "button選択";
            this.button選択.Size = new System.Drawing.Size(75, 23);
            this.button選択.TabIndex = 10;
            this.button選択.Text = "すべて選択";
            this.button選択.UseVisualStyleBackColor = true;
            this.button選択.Click += new System.EventHandler(this.button選択_Click);
            // 
            // button解除
            // 
            this.button解除.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button解除.Location = new System.Drawing.Point(92, 12);
            this.button解除.Name = "button解除";
            this.button解除.Size = new System.Drawing.Size(75, 23);
            this.button解除.TabIndex = 11;
            this.button解除.Text = "すべて解除";
            this.button解除.UseVisualStyleBackColor = true;
            this.button解除.Click += new System.EventHandler(this.button解除_Click);
            // 
            // 船選択Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 323);
            this.Controls.Add(this.button選択);
            this.Controls.Add(this.button解除);
            this.Controls.Add(this.button_キャンセル);
            this.Controls.Add(this.button_設定);
            this.Controls.Add(this.checkedListBox_船);
            this.Name = "船選択Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "船選択";
            this.Load += new System.EventHandler(this.船選択Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox_船;
        private System.Windows.Forms.Button button_設定;
        private System.Windows.Forms.Button button_キャンセル;
        private System.Windows.Forms.Button button選択;
        private System.Windows.Forms.Button button解除;
    }
}