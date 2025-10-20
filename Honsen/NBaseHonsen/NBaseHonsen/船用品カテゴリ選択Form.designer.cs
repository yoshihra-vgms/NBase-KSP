namespace NBaseHonsen
{
    partial class 船用品カテゴリ選択Form
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
            this.button選択 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.listBox船用品カテゴリ = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button選択
            // 
            this.button選択.BackColor = System.Drawing.SystemColors.Control;
            this.button選択.Location = new System.Drawing.Point(158, 345);
            this.button選択.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button選択.Name = "button選択";
            this.button選択.Size = new System.Drawing.Size(138, 36);
            this.button選択.TabIndex = 4;
            this.button選択.Text = "選択";
            this.button選択.UseVisualStyleBackColor = false;
            this.button選択.Click += new System.EventHandler(this.button選択_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(314, 345);
            this.button閉じる.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(138, 36);
            this.button閉じる.TabIndex = 5;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // listBox船用品カテゴリ
            // 
            this.listBox船用品カテゴリ.FormattingEnabled = true;
            this.listBox船用品カテゴリ.ItemHeight = 19;
            this.listBox船用品カテゴリ.Location = new System.Drawing.Point(46, 32);
            this.listBox船用品カテゴリ.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.listBox船用品カテゴリ.Name = "listBox船用品カテゴリ";
            this.listBox船用品カテゴリ.ScrollAlwaysVisible = true;
            this.listBox船用品カテゴリ.Size = new System.Drawing.Size(508, 289);
            this.listBox船用品カテゴリ.TabIndex = 3;
            // 
            // 船用品カテゴリ選択Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(609, 401);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button選択);
            this.Controls.Add(this.listBox船用品カテゴリ);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "船用品カテゴリ選択Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "品目選択";
            this.Load += new System.EventHandler(this.船用品カテゴリ選択Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button選択;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.ListBox listBox船用品カテゴリ;
    }
}