namespace Hachu.Reports
{
    partial class 管理表Form
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
            this.comboBox月 = new System.Windows.Forms.ComboBox();
            this.label月 = new System.Windows.Forms.Label();
            this.button出力 = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox年度 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBox月
            // 
            this.comboBox月.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox月.FormattingEnabled = true;
            this.comboBox月.Location = new System.Drawing.Point(218, 60);
            this.comboBox月.Name = "comboBox月";
            this.comboBox月.Size = new System.Drawing.Size(53, 20);
            this.comboBox月.TabIndex = 2;
            // 
            // label月
            // 
            this.label月.AutoSize = true;
            this.label月.Location = new System.Drawing.Point(277, 63);
            this.label月.Name = "label月";
            this.label月.Size = new System.Drawing.Size(17, 12);
            this.label月.TabIndex = 0;
            this.label月.Text = "月";
            // 
            // button出力
            // 
            this.button出力.Location = new System.Drawing.Point(158, 121);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(75, 23);
            this.button出力.TabIndex = 4;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = true;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(59, 18);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(273, 12);
            this.label.TabIndex = 0;
            this.label.Text = "出力したい年月を選択して出力ボタンをクリックしてください";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(172, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "年度";
            // 
            // comboBox年度
            // 
            this.comboBox年度.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年度.FormattingEnabled = true;
            this.comboBox年度.Location = new System.Drawing.Point(93, 60);
            this.comboBox年度.Name = "comboBox年度";
            this.comboBox年度.Size = new System.Drawing.Size(73, 20);
            this.comboBox年度.TabIndex = 6;
            // 
            // 管理表Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(390, 159);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox年度);
            this.Controls.Add(this.label);
            this.Controls.Add(this.button出力);
            this.Controls.Add(this.comboBox月);
            this.Controls.Add(this.label月);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "管理表Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "管理表Form";
            this.Load += new System.EventHandler(this.管理表Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox月;
        private System.Windows.Forms.Label label月;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox年度;
    }
}