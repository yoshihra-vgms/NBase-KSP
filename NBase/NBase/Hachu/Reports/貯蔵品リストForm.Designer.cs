namespace Hachu.Reports
{
    partial class 貯蔵品リストForm
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
            this.comboBox年 = new System.Windows.Forms.ComboBox();
            this.button出力 = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.label対象年月 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox月
            // 
            this.comboBox月.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox月.FormattingEnabled = true;
            this.comboBox月.Location = new System.Drawing.Point(155, 9);
            this.comboBox月.Name = "comboBox月";
            this.comboBox月.Size = new System.Drawing.Size(47, 20);
            this.comboBox月.TabIndex = 2;
            // 
            // comboBox年
            // 
            this.comboBox年.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年.FormattingEnabled = true;
            this.comboBox年.Location = new System.Drawing.Point(65, 9);
            this.comboBox年.Name = "comboBox年";
            this.comboBox年.Size = new System.Drawing.Size(73, 20);
            this.comboBox年.TabIndex = 1;
            // 
            // button出力
            // 
            this.button出力.Location = new System.Drawing.Point(158, 124);
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
            // label対象年月
            // 
            this.label対象年月.AutoSize = true;
            this.label対象年月.Location = new System.Drawing.Point(6, 13);
            this.label対象年月.Name = "label対象年月";
            this.label対象年月.Size = new System.Drawing.Size(53, 12);
            this.label対象年月.TabIndex = 0;
            this.label対象年月.Text = "対象年月";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(141, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "/";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox年);
            this.panel1.Controls.Add(this.comboBox月);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label対象年月);
            this.panel1.Location = new System.Drawing.Point(91, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 43);
            this.panel1.TabIndex = 6;
            // 
            // 貯蔵品リストForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(390, 159);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label);
            this.Controls.Add(this.button出力);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "貯蔵品リストForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "貯蔵品リストForm";
            this.Load += new System.EventHandler(this.貯蔵品リストForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox月;
        private System.Windows.Forms.ComboBox comboBox年;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label対象年月;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}