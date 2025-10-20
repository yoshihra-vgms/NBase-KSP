namespace Hachu.Reports
{
    partial class 貯蔵品管理表Form
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
            this.label年度 = new System.Windows.Forms.Label();
            this.button出力 = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.label期間 = new System.Windows.Forms.Label();
            this.label船 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox月
            // 
            this.comboBox月.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox月.FormattingEnabled = true;
            this.comboBox月.Location = new System.Drawing.Point(199, 49);
            this.comboBox月.Name = "comboBox月";
            this.comboBox月.Size = new System.Drawing.Size(143, 20);
            this.comboBox月.TabIndex = 2;
            // 
            // comboBox年
            // 
            this.comboBox年.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年.FormattingEnabled = true;
            this.comboBox年.Location = new System.Drawing.Point(87, 49);
            this.comboBox年.Name = "comboBox年";
            this.comboBox年.Size = new System.Drawing.Size(73, 20);
            this.comboBox年.TabIndex = 1;
            // 
            // label年度
            // 
            this.label年度.AutoSize = true;
            this.label年度.Location = new System.Drawing.Point(161, 53);
            this.label年度.Name = "label年度";
            this.label年度.Size = new System.Drawing.Size(29, 12);
            this.label年度.TabIndex = 0;
            this.label年度.Text = "年度";
            // 
            // button出力
            // 
            this.button出力.Location = new System.Drawing.Point(158, 98);
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
            this.label.Location = new System.Drawing.Point(49, 18);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(273, 12);
            this.label.TabIndex = 0;
            this.label.Text = "出力したい期間を選択して出力ボタンをクリックしてください";
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(87, 139);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(255, 20);
            this.comboBox船.TabIndex = 3;
            this.comboBox船.Visible = false;
            // 
            // label期間
            // 
            this.label期間.AutoSize = true;
            this.label期間.Location = new System.Drawing.Point(49, 53);
            this.label期間.Name = "label期間";
            this.label期間.Size = new System.Drawing.Size(29, 12);
            this.label期間.TabIndex = 0;
            this.label期間.Text = "期間";
            // 
            // label船
            // 
            this.label船.AutoSize = true;
            this.label船.Location = new System.Drawing.Point(61, 142);
            this.label船.Name = "label船";
            this.label船.Size = new System.Drawing.Size(17, 12);
            this.label船.TabIndex = 0;
            this.label船.Text = "船";
            this.label船.Visible = false;
            // 
            // 貯蔵品管理表Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(390, 135);
            this.Controls.Add(this.label船);
            this.Controls.Add(this.label期間);
            this.Controls.Add(this.comboBox船);
            this.Controls.Add(this.label);
            this.Controls.Add(this.button出力);
            this.Controls.Add(this.comboBox月);
            this.Controls.Add(this.comboBox年);
            this.Controls.Add(this.label年度);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "貯蔵品管理表Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "管理表Form";
            this.Load += new System.EventHandler(this.管理表Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox月;
        private System.Windows.Forms.ComboBox comboBox年;
        private System.Windows.Forms.Label label年度;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label期間;
        private System.Windows.Forms.Label label船;
    }
}