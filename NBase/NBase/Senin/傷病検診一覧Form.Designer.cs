namespace Senin
{
    partial class 傷病検診一覧Form
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
            this.button出力 = new System.Windows.Forms.Button();
            this.comboBox職名 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button船員検索 = new System.Windows.Forms.Button();
            this.textBox船員 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button船員クリア = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker期間To = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker期間From = new NBaseUtil.NullableDateTimePicker();
            this.SuspendLayout();
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(226, 167);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 6;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button出力
            // 
            this.button出力.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button出力.BackColor = System.Drawing.SystemColors.Control;
            this.button出力.Location = new System.Drawing.Point(145, 167);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(75, 23);
            this.button出力.TabIndex = 5;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = false;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // comboBox職名
            // 
            this.comboBox職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名.FormattingEnabled = true;
            this.comboBox職名.Location = new System.Drawing.Point(89, 63);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(150, 20);
            this.comboBox職名.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "職名";
            // 
            // button船員検索
            // 
            this.button船員検索.BackColor = System.Drawing.SystemColors.Control;
            this.button船員検索.Location = new System.Drawing.Point(245, 94);
            this.button船員検索.Name = "button船員検索";
            this.button船員検索.Size = new System.Drawing.Size(75, 23);
            this.button船員検索.TabIndex = 3;
            this.button船員検索.Text = "船員検索";
            this.button船員検索.UseVisualStyleBackColor = false;
            this.button船員検索.Click += new System.EventHandler(this.button船員検索_Click);
            // 
            // textBox船員
            // 
            this.textBox船員.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox船員.Location = new System.Drawing.Point(89, 96);
            this.textBox船員.MaxLength = 100;
            this.textBox船員.Name = "textBox船員";
            this.textBox船員.ReadOnly = true;
            this.textBox船員.Size = new System.Drawing.Size(150, 19);
            this.textBox船員.TabIndex = 2;
            this.textBox船員.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(45, 99);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "船員";
            // 
            // button船員クリア
            // 
            this.button船員クリア.BackColor = System.Drawing.SystemColors.Control;
            this.button船員クリア.Location = new System.Drawing.Point(326, 94);
            this.button船員クリア.Name = "button船員クリア";
            this.button船員クリア.Size = new System.Drawing.Size(75, 23);
            this.button船員クリア.TabIndex = 4;
            this.button船員クリア.Text = "船員クリア";
            this.button船員クリア.UseVisualStyleBackColor = false;
            this.button船員クリア.Click += new System.EventHandler(this.button船員クリア_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "～";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "期間";
            // 
            // nullableDateTimePicker期間To
            // 
            this.nullableDateTimePicker期間To.Location = new System.Drawing.Point(241, 30);
            this.nullableDateTimePicker期間To.Name = "nullableDateTimePicker期間To";
            this.nullableDateTimePicker期間To.Size = new System.Drawing.Size(120, 19);
            this.nullableDateTimePicker期間To.TabIndex = 35;
            this.nullableDateTimePicker期間To.Value = null;
            // 
            // nullableDateTimePicker期間From
            // 
            this.nullableDateTimePicker期間From.Location = new System.Drawing.Point(89, 30);
            this.nullableDateTimePicker期間From.Name = "nullableDateTimePicker期間From";
            this.nullableDateTimePicker期間From.Size = new System.Drawing.Size(120, 19);
            this.nullableDateTimePicker期間From.TabIndex = 34;
            this.nullableDateTimePicker期間From.Value = null;
            // 
            // 傷病検診一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(447, 202);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nullableDateTimePicker期間To);
            this.Controls.Add(this.nullableDateTimePicker期間From);
            this.Controls.Add(this.comboBox職名);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button船員クリア);
            this.Controls.Add(this.button船員検索);
            this.Controls.Add(this.textBox船員);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button出力);
            this.Name = "傷病検診一覧Form";
            this.Text = "傷病検診一覧Form";
            this.Load += new System.EventHandler(this.傷病検診一覧Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.ComboBox comboBox職名;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button船員検索;
        private System.Windows.Forms.TextBox textBox船員;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button船員クリア;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker期間To;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker期間From;
    }
}