namespace NBaseHonsen.Senin
{
    partial class 職名選択Form
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
            this.button乗船 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.checkedListBox職名 = new System.Windows.Forms.CheckedListBox();
            this.comboBox職名 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker乗船日 = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(247, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "さんの乗船時の職名を選択して下さい";
            // 
            // button乗船
            // 
            this.button乗船.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button乗船.BackColor = System.Drawing.SystemColors.Control;
            this.button乗船.Location = new System.Drawing.Point(68, 179);
            this.button乗船.Name = "button乗船";
            this.button乗船.Size = new System.Drawing.Size(120, 38);
            this.button乗船.TabIndex = 2;
            this.button乗船.Text = "乗船";
            this.button乗船.UseVisualStyleBackColor = false;
            this.button乗船.Click += new System.EventHandler(this.button乗船_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(205, 179);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(120, 38);
            this.button閉じる.TabIndex = 3;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // checkedListBox職名
            // 
            this.checkedListBox職名.CheckOnClick = true;
            this.checkedListBox職名.ColumnWidth = 150;
            this.checkedListBox職名.FormattingEnabled = true;
            this.checkedListBox職名.Location = new System.Drawing.Point(16, 77);
            this.checkedListBox職名.MultiColumn = true;
            this.checkedListBox職名.Name = "checkedListBox職名";
            this.checkedListBox職名.Size = new System.Drawing.Size(367, 22);
            this.checkedListBox職名.TabIndex = 4;
            this.checkedListBox職名.Visible = false;
            // 
            // comboBox職名
            // 
            this.comboBox職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名.FormattingEnabled = true;
            this.comboBox職名.Location = new System.Drawing.Point(16, 47);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(363, 24);
            this.comboBox職名.TabIndex = 5;
            // 
            // dateTimePicker乗船日
            // 
            this.dateTimePicker乗船日.Location = new System.Drawing.Point(147, 120);
            this.dateTimePicker乗船日.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker乗船日.Name = "dateTimePicker乗船日";
            this.dateTimePicker乗船日.Size = new System.Drawing.Size(166, 23);
            this.dateTimePicker乗船日.TabIndex = 53;
            this.dateTimePicker乗船日.Value = new System.DateTime(2009, 11, 17, 0, 0, 0, 0);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(80, 124);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 16);
            this.label9.TabIndex = 52;
            this.label9.Text = "乗船日";
            // 
            // 職名選択Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(393, 228);
            this.Controls.Add(this.dateTimePicker乗船日);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBox職名);
            this.Controls.Add(this.checkedListBox職名);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button乗船);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "職名選択Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "職名選択";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button乗船;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.CheckedListBox checkedListBox職名;
        private System.Windows.Forms.ComboBox comboBox職名;
        private System.Windows.Forms.DateTimePicker dateTimePicker乗船日;
        private System.Windows.Forms.Label label9;
    }
}