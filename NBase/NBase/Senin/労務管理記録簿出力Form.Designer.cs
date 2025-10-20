namespace Senin
{
    partial class 労務管理記録簿出力Form
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox船員 = new System.Windows.Forms.ComboBox();
            this.panel_期間 = new System.Windows.Forms.Panel();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_期間.SuspendLayout();
            this.SuspendLayout();
            // 
            // button出力
            // 
            this.button出力.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button出力.BackColor = System.Drawing.SystemColors.Control;
            this.button出力.Location = new System.Drawing.Point(112, 123);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(75, 23);
            this.button出力.TabIndex = 3;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = false;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(193, 123);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 4;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Excel ファイル|*.xlsx";
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "氏名";
            // 
            // comboBox船員
            // 
            this.comboBox船員.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船員.FormattingEnabled = true;
            this.comboBox船員.Location = new System.Drawing.Point(72, 68);
            this.comboBox船員.Name = "comboBox船員";
            this.comboBox船員.Size = new System.Drawing.Size(150, 20);
            this.comboBox船員.TabIndex = 62;
            // 
            // panel_期間
            // 
            this.panel_期間.Controls.Add(this.dateTimePicker2);
            this.panel_期間.Controls.Add(this.dateTimePicker1);
            this.panel_期間.Controls.Add(this.label3);
            this.panel_期間.Controls.Add(this.label1);
            this.panel_期間.Location = new System.Drawing.Point(35, 25);
            this.panel_期間.Name = "panel_期間";
            this.panel_期間.Size = new System.Drawing.Size(322, 37);
            this.panel_期間.TabIndex = 65;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(188, 9);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(131, 19);
            this.dateTimePicker2.TabIndex = 67;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(37, 9);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(131, 19);
            this.dateTimePicker1.TabIndex = 68;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(169, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 65;
            this.label3.Text = "～";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 66;
            this.label1.Text = "期間";
            // 
            // Tek労務管理記録簿出力Form
            // 
            this.AcceptButton = this.button出力;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(381, 160);
            this.Controls.Add(this.panel_期間);
            this.Controls.Add(this.comboBox船員);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button出力);
            this.Name = "Tek労務管理記録簿出力Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "労務管理記録簿出力";
            this.panel_期間.ResumeLayout(false);
            this.panel_期間.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox船員;
        private System.Windows.Forms.Panel panel_期間;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}