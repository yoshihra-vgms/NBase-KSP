namespace Senin
{
    partial class 賞罰詳細Form
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
            this.dateTimePicker年月日 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox賞罰名 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.button更新 = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.listBox添付ファイル = new System.Windows.Forms.ListBox();
            this.label17 = new System.Windows.Forms.Label();
            this.button添付削除 = new System.Windows.Forms.Button();
            this.button添付追加 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // dateTimePicker年月日
            // 
            this.dateTimePicker年月日.Location = new System.Drawing.Point(92, 24);
            this.dateTimePicker年月日.Name = "dateTimePicker年月日";
            this.dateTimePicker年月日.Size = new System.Drawing.Size(162, 19);
            this.dateTimePicker年月日.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "※年月日";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "※賞罰名";
            // 
            // textBox賞罰名
            // 
            this.textBox賞罰名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox賞罰名.Location = new System.Drawing.Point(92, 49);
            this.textBox賞罰名.MaxLength = 50;
            this.textBox賞罰名.Name = "textBox賞罰名";
            this.textBox賞罰名.Size = new System.Drawing.Size(291, 19);
            this.textBox賞罰名.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "備考";
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox備考.Location = new System.Drawing.Point(92, 74);
            this.textBox備考.MaxLength = 500;
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.Size = new System.Drawing.Size(291, 69);
            this.textBox備考.TabIndex = 3;
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(92, 258);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 8;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(180, 258);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 9;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(268, 258);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 10;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // listBox添付ファイル
            // 
            this.listBox添付ファイル.AllowDrop = true;
            this.listBox添付ファイル.FormattingEnabled = true;
            this.listBox添付ファイル.ItemHeight = 12;
            this.listBox添付ファイル.Location = new System.Drawing.Point(92, 149);
            this.listBox添付ファイル.Name = "listBox添付ファイル";
            this.listBox添付ファイル.ScrollAlwaysVisible = true;
            this.listBox添付ファイル.Size = new System.Drawing.Size(233, 64);
            this.listBox添付ファイル.TabIndex = 4;
            this.listBox添付ファイル.DoubleClick += new System.EventHandler(this.listBox添付ファイル_DoubleClick);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(21, 149);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 12);
            this.label17.TabIndex = 29;
            this.label17.Text = "添付ファイル";
            // 
            // button添付削除
            // 
            this.button添付削除.BackColor = System.Drawing.SystemColors.Control;
            this.button添付削除.Location = new System.Drawing.Point(331, 178);
            this.button添付削除.Name = "button添付削除";
            this.button添付削除.Size = new System.Drawing.Size(75, 23);
            this.button添付削除.TabIndex = 6;
            this.button添付削除.Text = "削除";
            this.button添付削除.UseVisualStyleBackColor = false;
            this.button添付削除.Click += new System.EventHandler(this.button添付削除_Click);
            // 
            // button添付追加
            // 
            this.button添付追加.BackColor = System.Drawing.SystemColors.Control;
            this.button添付追加.Location = new System.Drawing.Point(331, 149);
            this.button添付追加.Name = "button添付追加";
            this.button添付追加.Size = new System.Drawing.Size(75, 23);
            this.button添付追加.TabIndex = 5;
            this.button添付追加.Text = "追加";
            this.button添付追加.UseVisualStyleBackColor = false;
            this.button添付追加.Click += new System.EventHandler(this.button添付追加_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // 賞罰詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(435, 293);
            this.Controls.Add(this.listBox添付ファイル);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.button添付削除);
            this.Controls.Add(this.button添付追加);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.textBox備考);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox賞罰名);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker年月日);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "賞罰詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "賞罰詳細";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimePicker年月日;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox賞罰名;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.ListBox listBox添付ファイル;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button button添付削除;
        private System.Windows.Forms.Button button添付追加;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}