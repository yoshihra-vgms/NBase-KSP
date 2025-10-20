namespace NBaseMaster.船員管理
{
    partial class 免許免状管理詳細Form
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
            this.button閉じる = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.button更新 = new System.Windows.Forms.Button();
            this.textBox名 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox略称 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox表示順序 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(188, 112);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 6;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button削除
            // 
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(107, 112);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 5;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button更新
            // 
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(26, 112);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 4;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // textBox名
            // 
            this.textBox名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox名.Location = new System.Drawing.Point(107, 12);
            this.textBox名.MaxLength = 20;
            this.textBox名.Name = "textBox名";
            this.textBox名.Size = new System.Drawing.Size(167, 19);
            this.textBox名.TabIndex = 1;
            this.textBox名.TextChanged += new System.EventHandler(this.DataTextChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "※免許／免状名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "略称";
            // 
            // textBox略称
            // 
            this.textBox略称.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox略称.Location = new System.Drawing.Point(107, 39);
            this.textBox略称.MaxLength = 20;
            this.textBox略称.Name = "textBox略称";
            this.textBox略称.Size = new System.Drawing.Size(116, 19);
            this.textBox略称.TabIndex = 2;
            this.textBox略称.TextChanged += new System.EventHandler(this.DataTextChange);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 26;
            this.label3.Text = "表示順序";
            // 
            // textBox表示順序
            // 
            this.textBox表示順序.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox表示順序.Location = new System.Drawing.Point(107, 67);
            this.textBox表示順序.MaxLength = 3;
            this.textBox表示順序.Name = "textBox表示順序";
            this.textBox表示順序.Size = new System.Drawing.Size(39, 19);
            this.textBox表示順序.TabIndex = 3;
            this.textBox表示順序.Text = "0";
            this.textBox表示順序.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox表示順序.TextChanged += new System.EventHandler(this.DataTextChange);
            // 
            // 免許免状管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(286, 147);
            this.Controls.Add(this.textBox表示順序);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.textBox略称);
            this.Controls.Add(this.textBox名);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "免許免状管理詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "免許免状管理詳細Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.TextBox textBox名;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox略称;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox表示順序;
    }
}