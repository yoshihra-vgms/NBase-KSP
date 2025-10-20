namespace Yojitsu
{
    partial class 修繕費出力選択Form
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.button出力 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.radioButton各船 = new System.Windows.Forms.RadioButton();
            this.radioButton全船 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonCancel.Location = new System.Drawing.Point(149, 90);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 40;
            this.buttonCancel.Text = "閉じる";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // button出力
            // 
            this.button出力.BackColor = System.Drawing.SystemColors.Control;
            this.button出力.Location = new System.Drawing.Point(68, 90);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(75, 23);
            this.button出力.TabIndex = 39;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = false;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "ダイジェスト表示.xls";
            this.saveFileDialog1.Filter = "Excel ファイル|*.xls";
            // 
            // radioButton各船
            // 
            this.radioButton各船.AutoSize = true;
            this.radioButton各船.Checked = true;
            this.radioButton各船.Location = new System.Drawing.Point(83, 27);
            this.radioButton各船.Name = "radioButton各船";
            this.radioButton各船.Size = new System.Drawing.Size(127, 16);
            this.radioButton各船.TabIndex = 41;
            this.radioButton各船.TabStop = true;
            this.radioButton各船.Text = "選択されている船のみ";
            this.radioButton各船.UseVisualStyleBackColor = true;
            // 
            // radioButton全船
            // 
            this.radioButton全船.AutoSize = true;
            this.radioButton全船.Location = new System.Drawing.Point(83, 49);
            this.radioButton全船.Name = "radioButton全船";
            this.radioButton全船.Size = new System.Drawing.Size(47, 16);
            this.radioButton全船.TabIndex = 41;
            this.radioButton全船.Text = "全船";
            this.radioButton全船.UseVisualStyleBackColor = true;
            // 
            // ダイジェスト表示Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 125);
            this.Controls.Add(this.radioButton全船);
            this.Controls.Add(this.radioButton各船);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.button出力);
            this.Name = "ダイジェスト表示Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修繕費出力選択";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.RadioButton radioButton各船;
        private System.Windows.Forms.RadioButton radioButton全船;
    }
}