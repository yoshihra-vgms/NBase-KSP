namespace Senin
{
    partial class 年度指定出力Form
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
            this.button出力 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.comboBox年度 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button出力
            // 
            this.button出力.BackColor = System.Drawing.SystemColors.Control;
            this.button出力.Location = new System.Drawing.Point(68, 84);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(75, 23);
            this.button出力.TabIndex = 2;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = false;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(149, 84);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 3;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Excel ファイル|*.xlsx";
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // comboBox年度
            // 
            this.comboBox年度.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年度.FormattingEnabled = true;
            this.comboBox年度.Location = new System.Drawing.Point(135, 28);
            this.comboBox年度.Name = "comboBox年度";
            this.comboBox年度.Size = new System.Drawing.Size(61, 20);
            this.comboBox年度.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(96, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "年度";
            // 
            // 年度指定出力Form
            // 
            this.AcceptButton = this.button出力;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 126);
            this.Controls.Add(this.comboBox年度);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button出力);
            this.Name = "年度指定出力Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "年度指定出力Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ComboBox comboBox年度;
        private System.Windows.Forms.Label label6;
    }
}