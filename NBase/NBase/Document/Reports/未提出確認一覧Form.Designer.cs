namespace Document.Reports
{
    partial class 未提出確認一覧Form
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
            this.label年度 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.button出力 = new System.Windows.Forms.Button();
            this.comboBox年 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label年度
            // 
            this.label年度.AutoSize = true;
            this.label年度.Location = new System.Drawing.Point(123, 60);
            this.label年度.Name = "label年度";
            this.label年度.Size = new System.Drawing.Size(29, 12);
            this.label年度.TabIndex = 9;
            this.label年度.Text = "年度";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(42, 26);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(273, 12);
            this.label.TabIndex = 6;
            this.label.Text = "出力したい年度を選択して出力ボタンをクリックしてください";
            // 
            // button出力
            // 
            this.button出力.Location = new System.Drawing.Point(152, 101);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(75, 23);
            this.button出力.TabIndex = 8;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = true;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // comboBox年
            // 
            this.comboBox年.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年.FormattingEnabled = true;
            this.comboBox年.Location = new System.Drawing.Point(161, 57);
            this.comboBox年.Name = "comboBox年";
            this.comboBox年.Size = new System.Drawing.Size(73, 20);
            this.comboBox年.TabIndex = 7;
            // 
            // 未提出確認一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(356, 159);
            this.Controls.Add(this.label年度);
            this.Controls.Add(this.label);
            this.Controls.Add(this.button出力);
            this.Controls.Add(this.comboBox年);
            this.Name = "未提出確認一覧Form";
            this.Text = "未提出確認一覧Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label年度;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.ComboBox comboBox年;
    }
}