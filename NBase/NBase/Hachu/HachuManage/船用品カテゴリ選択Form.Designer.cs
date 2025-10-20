namespace Hachu.HachuManage
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
            this.button選択.Location = new System.Drawing.Point(86, 218);
            this.button選択.Name = "button選択";
            this.button選択.Size = new System.Drawing.Size(75, 23);
            this.button選択.TabIndex = 4;
            this.button選択.Text = "選択";
            this.button選択.UseVisualStyleBackColor = false;
            this.button選択.Click += new System.EventHandler(this.button選択_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(171, 218);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 5;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // listBox船用品カテゴリ
            // 
            this.listBox船用品カテゴリ.FormattingEnabled = true;
            this.listBox船用品カテゴリ.ItemHeight = 12;
            this.listBox船用品カテゴリ.Location = new System.Drawing.Point(25, 20);
            this.listBox船用品カテゴリ.Name = "listBox船用品カテゴリ";
            this.listBox船用品カテゴリ.ScrollAlwaysVisible = true;
            this.listBox船用品カテゴリ.Size = new System.Drawing.Size(279, 184);
            this.listBox船用品カテゴリ.TabIndex = 3;
            this.listBox船用品カテゴリ.DoubleClick += new System.EventHandler(this.listBox船用品カテゴリ_DoubleClick);
            // 
            // 船用品カテゴリ選択Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(332, 253);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button選択);
            this.Controls.Add(this.listBox船用品カテゴリ);
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