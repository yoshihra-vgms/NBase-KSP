namespace NBaseMaster.Doc.乗船者登録
{
    partial class 乗船者登録詳細Form
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
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxVessel = new System.Windows.Forms.TextBox();
            this.checkedListBox職名 = new System.Windows.Forms.CheckedListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button船員検索 = new System.Windows.Forms.Button();
            this.textBox船員 = new System.Windows.Forms.TextBox();
            this.button削除 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button更新 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "船";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "氏名";
            // 
            // textBoxVessel
            // 
            this.textBoxVessel.Location = new System.Drawing.Point(59, 23);
            this.textBoxVessel.Name = "textBoxVessel";
            this.textBoxVessel.ReadOnly = true;
            this.textBoxVessel.Size = new System.Drawing.Size(162, 19);
            this.textBoxVessel.TabIndex = 3;
            // 
            // checkedListBox職名
            // 
            this.checkedListBox職名.CheckOnClick = true;
            this.checkedListBox職名.FormattingEnabled = true;
            this.checkedListBox職名.Location = new System.Drawing.Point(59, 82);
            this.checkedListBox職名.Name = "checkedListBox職名";
            this.checkedListBox職名.Size = new System.Drawing.Size(162, 88);
            this.checkedListBox職名.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "職名";
            // 
            // button船員検索
            // 
            this.button船員検索.BackColor = System.Drawing.SystemColors.Control;
            this.button船員検索.Location = new System.Drawing.Point(206, 53);
            this.button船員検索.Name = "button船員検索";
            this.button船員検索.Size = new System.Drawing.Size(75, 23);
            this.button船員検索.TabIndex = 12;
            this.button船員検索.Text = "船員検索";
            this.button船員検索.UseVisualStyleBackColor = false;
            this.button船員検索.Click += new System.EventHandler(this.button船員検索_Click);
            // 
            // textBox船員
            // 
            this.textBox船員.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox船員.Location = new System.Drawing.Point(59, 51);
            this.textBox船員.MaxLength = 100;
            this.textBox船員.Name = "textBox船員";
            this.textBox船員.ReadOnly = true;
            this.textBox船員.Size = new System.Drawing.Size(127, 19);
            this.textBox船員.TabIndex = 11;
            this.textBox船員.TabStop = false;
            // 
            // button削除
            // 
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(123, 194);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 14;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(204, 194);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 15;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button更新
            // 
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(42, 194);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 13;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // 乗船者登録詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(320, 229);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.button船員検索);
            this.Controls.Add(this.textBox船員);
            this.Controls.Add(this.checkedListBox職名);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxVessel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "乗船者登録詳細Form";
            this.Text = "乗船者登録詳細Form";
            this.Load += new System.EventHandler(this.乗船者登録詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxVessel;
        private System.Windows.Forms.CheckedListBox checkedListBox職名;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button船員検索;
        private System.Windows.Forms.TextBox textBox船員;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button更新;
    }
}