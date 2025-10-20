namespace Senin
{
    partial class 講習管理詳細Form
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
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.button更新 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.comboBox講習名 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox場所 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox船員 = new System.Windows.Forms.TextBox();
            this.button船員検索 = new System.Windows.Forms.Button();
            this.nullableDateTimePicker開始予定日 = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker終了予定日 = new NBaseUtil.NullableDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker受講終了日 = new NBaseUtil.NullableDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker受講開始日 = new NBaseUtil.NullableDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox添付ファイル = new System.Windows.Forms.ListBox();
            this.label16 = new System.Windows.Forms.Label();
            this.button添付削除 = new System.Windows.Forms.Button();
            this.button添付追加 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "　 開始予定日";
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox備考.Location = new System.Drawing.Point(92, 234);
            this.textBox備考.MaxLength = 500;
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.Size = new System.Drawing.Size(352, 67);
            this.textBox備考.TabIndex = 8;
            // 
            // button更新
            // 
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(123, 325);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 9;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(285, 325);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 11;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button削除
            // 
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(204, 325);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 10;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // comboBox講習名
            // 
            this.comboBox講習名.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox講習名.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox講習名.FormattingEnabled = true;
            this.comboBox講習名.Location = new System.Drawing.Point(92, 22);
            this.comboBox講習名.MaxDropDownItems = 25;
            this.comboBox講習名.Name = "comboBox講習名";
            this.comboBox講習名.Size = new System.Drawing.Size(250, 20);
            this.comboBox講習名.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "※場所";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "※講習名";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 237);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "　 備考";
            // 
            // textBox場所
            // 
            this.textBox場所.Location = new System.Drawing.Point(92, 48);
            this.textBox場所.MaxLength = 50;
            this.textBox場所.Name = "textBox場所";
            this.textBox場所.Size = new System.Drawing.Size(250, 19);
            this.textBox場所.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 133);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "※船員";
            // 
            // textBox船員
            // 
            this.textBox船員.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox船員.Location = new System.Drawing.Point(92, 130);
            this.textBox船員.MaxLength = 100;
            this.textBox船員.Name = "textBox船員";
            this.textBox船員.ReadOnly = true;
            this.textBox船員.Size = new System.Drawing.Size(127, 19);
            this.textBox船員.TabIndex = 0;
            this.textBox船員.TabStop = false;
            // 
            // button船員検索
            // 
            this.button船員検索.BackColor = System.Drawing.SystemColors.Control;
            this.button船員検索.Location = new System.Drawing.Point(236, 128);
            this.button船員検索.Name = "button船員検索";
            this.button船員検索.Size = new System.Drawing.Size(75, 23);
            this.button船員検索.TabIndex = 7;
            this.button船員検索.Text = "船員検索";
            this.button船員検索.UseVisualStyleBackColor = false;
            this.button船員検索.Click += new System.EventHandler(this.button船員検索_Click);
            // 
            // nullableDateTimePicker開始予定日
            // 
            this.nullableDateTimePicker開始予定日.Location = new System.Drawing.Point(92, 74);
            this.nullableDateTimePicker開始予定日.Name = "nullableDateTimePicker開始予定日";
            this.nullableDateTimePicker開始予定日.Size = new System.Drawing.Size(127, 19);
            this.nullableDateTimePicker開始予定日.TabIndex = 3;
            this.nullableDateTimePicker開始予定日.Value = new System.DateTime(2012, 3, 23, 19, 23, 1, 923);
            // 
            // nullableDateTimePicker終了予定日
            // 
            this.nullableDateTimePicker終了予定日.Location = new System.Drawing.Point(317, 74);
            this.nullableDateTimePicker終了予定日.Name = "nullableDateTimePicker終了予定日";
            this.nullableDateTimePicker終了予定日.Size = new System.Drawing.Size(127, 19);
            this.nullableDateTimePicker終了予定日.TabIndex = 4;
            this.nullableDateTimePicker終了予定日.Value = new System.DateTime(2012, 3, 23, 19, 23, 1, 923);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(246, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "終了予定日";
            // 
            // nullableDateTimePicker受講終了日
            // 
            this.nullableDateTimePicker受講終了日.Location = new System.Drawing.Point(317, 99);
            this.nullableDateTimePicker受講終了日.Name = "nullableDateTimePicker受講終了日";
            this.nullableDateTimePicker受講終了日.Size = new System.Drawing.Size(127, 19);
            this.nullableDateTimePicker受講終了日.TabIndex = 6;
            this.nullableDateTimePicker受講終了日.Value = new System.DateTime(2012, 3, 23, 19, 23, 1, 923);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(246, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "受講終了日";
            // 
            // nullableDateTimePicker受講開始日
            // 
            this.nullableDateTimePicker受講開始日.Location = new System.Drawing.Point(92, 99);
            this.nullableDateTimePicker受講開始日.Name = "nullableDateTimePicker受講開始日";
            this.nullableDateTimePicker受講開始日.Size = new System.Drawing.Size(127, 19);
            this.nullableDateTimePicker受講開始日.TabIndex = 5;
            this.nullableDateTimePicker受講開始日.Value = new System.DateTime(2012, 3, 23, 19, 23, 1, 923);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "　 受講開始日";
            // 
            // listBox添付ファイル
            // 
            this.listBox添付ファイル.AllowDrop = true;
            this.listBox添付ファイル.FormattingEnabled = true;
            this.listBox添付ファイル.ItemHeight = 12;
            this.listBox添付ファイル.Location = new System.Drawing.Point(92, 159);
            this.listBox添付ファイル.Name = "listBox添付ファイル";
            this.listBox添付ファイル.ScrollAlwaysVisible = true;
            this.listBox添付ファイル.Size = new System.Drawing.Size(219, 64);
            this.listBox添付ファイル.TabIndex = 24;
            this.listBox添付ファイル.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox添付ファイル_DragDrop);
            this.listBox添付ファイル.DoubleClick += new System.EventHandler(this.listBox添付ファイル_DoubleClick);
            this.listBox添付ファイル.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox添付ファイル_DragEnter);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 161);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(75, 12);
            this.label16.TabIndex = 23;
            this.label16.Text = "　 添付ファイル";
            // 
            // button添付削除
            // 
            this.button添付削除.BackColor = System.Drawing.SystemColors.Control;
            this.button添付削除.Location = new System.Drawing.Point(317, 188);
            this.button添付削除.Name = "button添付削除";
            this.button添付削除.Size = new System.Drawing.Size(75, 23);
            this.button添付削除.TabIndex = 26;
            this.button添付削除.Text = "削除";
            this.button添付削除.UseVisualStyleBackColor = false;
            this.button添付削除.Click += new System.EventHandler(this.button添付削除_Click);
            // 
            // button添付追加
            // 
            this.button添付追加.BackColor = System.Drawing.SystemColors.Control;
            this.button添付追加.Location = new System.Drawing.Point(317, 159);
            this.button添付追加.Name = "button添付追加";
            this.button添付追加.Size = new System.Drawing.Size(75, 23);
            this.button添付追加.TabIndex = 25;
            this.button添付追加.Text = "追加";
            this.button添付追加.UseVisualStyleBackColor = false;
            this.button添付追加.Click += new System.EventHandler(this.button添付追加_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // 講習管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(482, 365);
            this.Controls.Add(this.listBox添付ファイル);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.button添付削除);
            this.Controls.Add(this.button添付追加);
            this.Controls.Add(this.nullableDateTimePicker受講終了日);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nullableDateTimePicker受講開始日);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nullableDateTimePicker終了予定日);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nullableDateTimePicker開始予定日);
            this.Controls.Add(this.button船員検索);
            this.Controls.Add(this.textBox場所);
            this.Controls.Add(this.comboBox講習名);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.textBox備考);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox船員);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Name = "講習管理詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "講習情報詳細";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.ComboBox comboBox講習名;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox場所;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox船員;
        private System.Windows.Forms.Button button船員検索;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker開始予定日;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker終了予定日;
        private System.Windows.Forms.Label label3;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker受講終了日;
        private System.Windows.Forms.Label label4;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker受講開始日;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox添付ファイル;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button添付削除;
        private System.Windows.Forms.Button button添付追加;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}