namespace NBaseMaster.船員管理
{
    partial class 標令給Form
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
            this.textBox標令 = new System.Windows.Forms.TextBox();
            this.textBox支給額 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button更新 = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox加算額 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "標令";
            // 
            // textBox標令
            // 
            this.textBox標令.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox標令.Location = new System.Drawing.Point(116, 18);
            this.textBox標令.MaxLength = 2;
            this.textBox標令.Name = "textBox標令";
            this.textBox標令.Size = new System.Drawing.Size(117, 19);
            this.textBox標令.TabIndex = 1;
            this.textBox標令.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // textBox支給額
            // 
            this.textBox支給額.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox支給額.Location = new System.Drawing.Point(116, 43);
            this.textBox支給額.MaxLength = 25;
            this.textBox支給額.Name = "textBox支給額";
            this.textBox支給額.Size = new System.Drawing.Size(117, 19);
            this.textBox支給額.TabIndex = 2;
            this.textBox支給額.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "支給額";
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(197, 120);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 6;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(27, 120);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 4;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(112, 120);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 5;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "加算額";
            // 
            // textBox加算額
            // 
            this.textBox加算額.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox加算額.Location = new System.Drawing.Point(116, 68);
            this.textBox加算額.MaxLength = 25;
            this.textBox加算額.Name = "textBox加算額";
            this.textBox加算額.Size = new System.Drawing.Size(117, 19);
            this.textBox加算額.TabIndex = 3;
            this.textBox加算額.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // 標令給Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(299, 155);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.textBox加算額);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox支給額);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox標令);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(315, 194);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(315, 194);
            this.Name = "標令給Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "標令給";
            this.Load += new System.EventHandler(this.評価Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox標令;
        private System.Windows.Forms.TextBox textBox支給額;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox加算額;
    }
}