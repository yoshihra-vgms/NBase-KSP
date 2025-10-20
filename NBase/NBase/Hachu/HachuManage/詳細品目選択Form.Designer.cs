namespace Hachu.HachuManage
{
    partial class 詳細品目選択Form
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
            this.button選択 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.textBox検索文字 = new System.Windows.Forms.TextBox();
            this.button検索 = new System.Windows.Forms.Button();
            this.listBox詳細品目 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "見つかった詳細品目";
            // 
            // button選択
            // 
            this.button選択.BackColor = System.Drawing.SystemColors.Control;
            this.button選択.Location = new System.Drawing.Point(86, 278);
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
            this.button閉じる.Location = new System.Drawing.Point(171, 278);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 5;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // textBox検索文字
            // 
            this.textBox検索文字.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox検索文字.Location = new System.Drawing.Point(25, 30);
            this.textBox検索文字.MaxLength = 50;
            this.textBox検索文字.Name = "textBox検索文字";
            this.textBox検索文字.Size = new System.Drawing.Size(198, 19);
            this.textBox検索文字.TabIndex = 1;
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(229, 28);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(75, 23);
            this.button検索.TabIndex = 2;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // listBox詳細品目
            // 
            this.listBox詳細品目.FormattingEnabled = true;
            this.listBox詳細品目.ItemHeight = 12;
            this.listBox詳細品目.Location = new System.Drawing.Point(25, 73);
            this.listBox詳細品目.Name = "listBox詳細品目";
            this.listBox詳細品目.ScrollAlwaysVisible = true;
            this.listBox詳細品目.Size = new System.Drawing.Size(279, 196);
            this.listBox詳細品目.TabIndex = 3;
            this.listBox詳細品目.DoubleClick += new System.EventHandler(this.listBox詳細品目_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(291, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "詳細品目名の一部を入力して検索ボタンをクリックしてください";
            // 
            // 詳細品目選択Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(332, 313);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button選択);
            this.Controls.Add(this.listBox詳細品目);
            this.Controls.Add(this.button検索);
            this.Controls.Add(this.textBox検索文字);
            this.Controls.Add(this.label1);
            this.Name = "詳細品目選択Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "品目選択";
            this.Load += new System.EventHandler(this.詳細品目検索Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button選択;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.TextBox textBox検索文字;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.ListBox listBox詳細品目;
        private System.Windows.Forms.Label label2;
    }
}