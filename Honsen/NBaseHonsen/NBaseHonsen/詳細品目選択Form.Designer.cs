namespace NBaseHonsen
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
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(42, 92);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "見つかった品目";
            // 
            // button選択
            // 
            this.button選択.BackColor = System.Drawing.SystemColors.Control;
            this.button選択.Location = new System.Drawing.Point(158, 440);
            this.button選択.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button選択.Name = "button選択";
            this.button選択.Size = new System.Drawing.Size(138, 36);
            this.button選択.TabIndex = 0;
            this.button選択.Text = "選択";
            this.button選択.UseVisualStyleBackColor = false;
            this.button選択.Click += new System.EventHandler(this.button選択_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(314, 440);
            this.button閉じる.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(138, 36);
            this.button閉じる.TabIndex = 2;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // textBox検索文字
            // 
            this.textBox検索文字.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox検索文字.Location = new System.Drawing.Point(46, 48);
            this.textBox検索文字.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox検索文字.MaxLength = 50;
            this.textBox検索文字.Name = "textBox検索文字";
            this.textBox検索文字.Size = new System.Drawing.Size(360, 26);
            this.textBox検索文字.TabIndex = 1;
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(420, 44);
            this.button検索.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(138, 36);
            this.button検索.TabIndex = 11;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // listBox詳細品目
            // 
            this.listBox詳細品目.FormattingEnabled = true;
            this.listBox詳細品目.ItemHeight = 19;
            this.listBox詳細品目.Location = new System.Drawing.Point(46, 116);
            this.listBox詳細品目.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.listBox詳細品目.Name = "listBox詳細品目";
            this.listBox詳細品目.ScrollAlwaysVisible = true;
            this.listBox詳細品目.Size = new System.Drawing.Size(508, 308);
            this.listBox詳細品目.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(42, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(454, 19);
            this.label2.TabIndex = 13;
            this.label2.Text = "品目名の一部を入力して検索ボタンをクリックしてください";
            // 
            // 詳細品目選択Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(604, 487);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button選択);
            this.Controls.Add(this.listBox詳細品目);
            this.Controls.Add(this.button検索);
            this.Controls.Add(this.textBox検索文字);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "詳細品目選択Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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