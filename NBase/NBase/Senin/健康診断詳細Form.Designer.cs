namespace Senin
{
    partial class 健康診断詳細Form
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
            this.textBoxその他 = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.button更新 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.button添付削除 = new System.Windows.Forms.Button();
            this.button添付追加 = new System.Windows.Forms.Button();
            this.textBox病院名 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.listBox添付ファイル = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.comboBox検査名 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker有効期限 = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker検査日 = new NBaseUtil.NullableDateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox診断名 = new System.Windows.Forms.TextBox();
            this.comboBox判定 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxその他
            // 
            this.textBoxその他.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxその他.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBoxその他.Location = new System.Drawing.Point(108, 170);
            this.textBoxその他.MaxLength = 500;
            this.textBoxその他.Multiline = true;
            this.textBoxその他.Name = "textBoxその他";
            this.textBoxその他.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxその他.Size = new System.Drawing.Size(247, 52);
            this.textBoxその他.TabIndex = 15;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label35.Location = new System.Drawing.Point(31, 170);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(33, 13);
            this.label35.TabIndex = 0;
            this.label35.Text = "備考";
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(277, 376);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 18;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(196, 376);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 17;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(115, 376);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 16;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(32, 242);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 12);
            this.label16.TabIndex = 0;
            this.label16.Text = "添付";
            // 
            // button添付削除
            // 
            this.button添付削除.BackColor = System.Drawing.SystemColors.Control;
            this.button添付削除.Location = new System.Drawing.Point(363, 269);
            this.button添付削除.Name = "button添付削除";
            this.button添付削除.Size = new System.Drawing.Size(75, 23);
            this.button添付削除.TabIndex = 14;
            this.button添付削除.Text = "削除";
            this.button添付削除.UseVisualStyleBackColor = false;
            this.button添付削除.Click += new System.EventHandler(this.button添付削除_Click);
            // 
            // button添付追加
            // 
            this.button添付追加.BackColor = System.Drawing.SystemColors.Control;
            this.button添付追加.Location = new System.Drawing.Point(363, 242);
            this.button添付追加.Name = "button添付追加";
            this.button添付追加.Size = new System.Drawing.Size(75, 23);
            this.button添付追加.TabIndex = 13;
            this.button添付追加.Text = "追加";
            this.button添付追加.UseVisualStyleBackColor = false;
            this.button添付追加.Click += new System.EventHandler(this.button添付追加_Click);
            // 
            // textBox病院名
            // 
            this.textBox病院名.Location = new System.Drawing.Point(110, 39);
            this.textBox病院名.MaxLength = 50;
            this.textBox病院名.Name = "textBox病院名";
            this.textBox病院名.Size = new System.Drawing.Size(258, 19);
            this.textBox病院名.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "病院";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "診断日";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(34, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "種別";
            // 
            // listBox添付ファイル
            // 
            this.listBox添付ファイル.FormattingEnabled = true;
            this.listBox添付ファイル.ItemHeight = 12;
            this.listBox添付ファイル.Location = new System.Drawing.Point(108, 242);
            this.listBox添付ファイル.Name = "listBox添付ファイル";
            this.listBox添付ファイル.Size = new System.Drawing.Size(244, 64);
            this.listBox添付ファイル.TabIndex = 12;
            this.listBox添付ファイル.DoubleClick += new System.EventHandler(this.listBox添付ファイル_DoubleClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // comboBox検査名
            // 
            this.comboBox検査名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox検査名.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox検査名.FormattingEnabled = true;
            this.comboBox検査名.Location = new System.Drawing.Point(110, 13);
            this.comboBox検査名.Name = "comboBox検査名";
            this.comboBox検査名.Size = new System.Drawing.Size(258, 21);
            this.comboBox検査名.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(32, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 19;
            this.label12.Text = "有効期限";
            // 
            // nullableDateTimePicker有効期限
            // 
            this.nullableDateTimePicker有効期限.Location = new System.Drawing.Point(110, 89);
            this.nullableDateTimePicker有効期限.Name = "nullableDateTimePicker有効期限";
            this.nullableDateTimePicker有効期限.NullValue = "";
            this.nullableDateTimePicker有効期限.Size = new System.Drawing.Size(132, 19);
            this.nullableDateTimePicker有効期限.TabIndex = 20;
            this.nullableDateTimePicker有効期限.Value = null;
            // 
            // nullableDateTimePicker検査日
            // 
            this.nullableDateTimePicker検査日.Location = new System.Drawing.Point(110, 64);
            this.nullableDateTimePicker検査日.Name = "nullableDateTimePicker検査日";
            this.nullableDateTimePicker検査日.NullValue = "";
            this.nullableDateTimePicker検査日.Size = new System.Drawing.Size(132, 19);
            this.nullableDateTimePicker検査日.TabIndex = 3;
            this.nullableDateTimePicker検査日.Value = null;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.Location = new System.Drawing.Point(17, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 12);
            this.label13.TabIndex = 21;
            this.label13.Text = "※";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.Location = new System.Drawing.Point(17, 69);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(18, 12);
            this.label14.TabIndex = 21;
            this.label14.Text = "※";
            // 
            // textBox診断名
            // 
            this.textBox診断名.Location = new System.Drawing.Point(108, 115);
            this.textBox診断名.MaxLength = 50;
            this.textBox診断名.Name = "textBox診断名";
            this.textBox診断名.Size = new System.Drawing.Size(258, 19);
            this.textBox診断名.TabIndex = 2;
            // 
            // comboBox判定
            // 
            this.comboBox判定.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox判定.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox判定.FormattingEnabled = true;
            this.comboBox判定.Location = new System.Drawing.Point(108, 140);
            this.comboBox判定.Name = "comboBox判定";
            this.comboBox判定.Size = new System.Drawing.Size(258, 21);
            this.comboBox判定.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "診断名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "判定";
            // 
            // 健康診断詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(466, 416);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.nullableDateTimePicker有効期限);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.comboBox判定);
            this.Controls.Add(this.comboBox検査名);
            this.Controls.Add(this.listBox添付ファイル);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox診断名);
            this.Controls.Add(this.textBox病院名);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nullableDateTimePicker検査日);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.button添付削除);
            this.Controls.Add(this.button添付追加);
            this.Controls.Add(this.textBoxその他);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button更新);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "健康診断詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "健康診断";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxその他;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button添付削除;
        private System.Windows.Forms.Button button添付追加;
        private System.Windows.Forms.TextBox textBox病院名;
        private System.Windows.Forms.Label label3;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker検査日;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox listBox添付ファイル;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox comboBox検査名;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker有効期限;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox診断名;
        private System.Windows.Forms.ComboBox comboBox判定;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
    }
}