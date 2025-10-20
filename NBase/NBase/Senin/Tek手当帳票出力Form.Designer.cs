namespace Senin
{
    partial class Tek手当帳票出力Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.monthPicker1 = new Senin.Utils.MonthPicker();
            this.SuspendLayout();
            // 
            // button出力
            // 
            this.button出力.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button出力.BackColor = System.Drawing.SystemColors.Control;
            this.button出力.Location = new System.Drawing.Point(62, 78);
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
            this.button閉じる.Location = new System.Drawing.Point(143, 78);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 66;
            this.label1.Text = "出力年月";
            // 
            // monthPicker1
            // 
            this.monthPicker1.CustomFormat = "yyyy年MM月";
            this.monthPicker1.Location = new System.Drawing.Point(112, 33);
            this.monthPicker1.Name = "monthPicker1";
            this.monthPicker1.Size = new System.Drawing.Size(131, 19);
            this.monthPicker1.TabIndex = 66;
            // 
            // Tek手当帳票出力Form
            // 
            this.AcceptButton = this.button出力;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(280, 115);
            this.Controls.Add(this.monthPicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button出力);
            this.MaximumSize = new System.Drawing.Size(296, 154);
            this.MinimumSize = new System.Drawing.Size(296, 154);
            this.Name = "Tek手当帳票出力Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "手当帳票出力";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
        private Utils.MonthPicker monthPicker1;
    }
}