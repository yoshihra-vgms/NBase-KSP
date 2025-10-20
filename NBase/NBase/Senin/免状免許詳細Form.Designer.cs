namespace Senin
{
    partial class 免状免許詳細Form
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
            this.comboBox免状 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox番号 = new System.Windows.Forms.TextBox();
            this.button更新 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button削除 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox種別 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox帳票出力 = new System.Windows.Forms.CheckBox();
            this.nullableDateTimePicker取得受講日 = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker有効期限 = new NBaseUtil.NullableDateTimePicker();
            this.listBox添付ファイル = new System.Windows.Forms.ListBox();
            this.label16 = new System.Windows.Forms.Label();
            this.button添付削除 = new System.Windows.Forms.Button();
            this.button添付追加 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkBox_筆記 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "※免許／免状";
            // 
            // comboBox免状
            // 
            this.comboBox免状.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox免状.FormattingEnabled = true;
            this.comboBox免状.Location = new System.Drawing.Point(115, 17);
            this.comboBox免状.Name = "comboBox免状";
            this.comboBox免状.Size = new System.Drawing.Size(156, 20);
            this.comboBox免状.TabIndex = 1;
            this.comboBox免状.SelectionChangeCommitted += new System.EventHandler(this.comboBox免状_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "※番号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "有効期限";
            // 
            // textBox番号
            // 
            this.textBox番号.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox番号.Location = new System.Drawing.Point(115, 102);
            this.textBox番号.MaxLength = 20;
            this.textBox番号.Name = "textBox番号";
            this.textBox番号.Size = new System.Drawing.Size(134, 19);
            this.textBox番号.TabIndex = 2;
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(114, 414);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 5;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(276, 414);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 6;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "取得／受講日";
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(195, 414);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 5;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "種別";
            // 
            // comboBox種別
            // 
            this.comboBox種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox種別.FormattingEnabled = true;
            this.comboBox種別.Location = new System.Drawing.Point(115, 45);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(233, 20);
            this.comboBox種別.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "帳票出力";
            // 
            // checkBox帳票出力
            // 
            this.checkBox帳票出力.AutoSize = true;
            this.checkBox帳票出力.Checked = true;
            this.checkBox帳票出力.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox帳票出力.Location = new System.Drawing.Point(112, 58);
            this.checkBox帳票出力.Name = "checkBox帳票出力";
            this.checkBox帳票出力.Size = new System.Drawing.Size(15, 14);
            this.checkBox帳票出力.TabIndex = 8;
            this.checkBox帳票出力.UseVisualStyleBackColor = true;
            // 
            // nullableDateTimePicker取得受講日
            // 
            this.nullableDateTimePicker取得受講日.Location = new System.Drawing.Point(112, 30);
            this.nullableDateTimePicker取得受講日.Name = "nullableDateTimePicker取得受講日";
            this.nullableDateTimePicker取得受講日.Size = new System.Drawing.Size(134, 19);
            this.nullableDateTimePicker取得受講日.TabIndex = 7;
            this.nullableDateTimePicker取得受講日.Value = null;
            // 
            // nullableDateTimePicker有効期限
            // 
            this.nullableDateTimePicker有効期限.Location = new System.Drawing.Point(112, 3);
            this.nullableDateTimePicker有効期限.Name = "nullableDateTimePicker有効期限";
            this.nullableDateTimePicker有効期限.Size = new System.Drawing.Size(134, 19);
            this.nullableDateTimePicker有効期限.TabIndex = 7;
            this.nullableDateTimePicker有効期限.Value = null;
            // 
            // listBox添付ファイル
            // 
            this.listBox添付ファイル.AllowDrop = true;
            this.listBox添付ファイル.FormattingEnabled = true;
            this.listBox添付ファイル.ItemHeight = 12;
            this.listBox添付ファイル.Location = new System.Drawing.Point(115, 213);
            this.listBox添付ファイル.Name = "listBox添付ファイル";
            this.listBox添付ファイル.ScrollAlwaysVisible = true;
            this.listBox添付ファイル.Size = new System.Drawing.Size(233, 64);
            this.listBox添付ファイル.TabIndex = 20;
            this.listBox添付ファイル.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox添付ファイル_DragDrop);
            this.listBox添付ファイル.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox添付ファイル_DragEnter);
            this.listBox添付ファイル.DoubleClick += new System.EventHandler(this.listBox添付ファイル_DoubleClick);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(22, 213);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 12);
            this.label16.TabIndex = 19;
            this.label16.Text = "添付ファイル";
            // 
            // button添付削除
            // 
            this.button添付削除.BackColor = System.Drawing.SystemColors.Control;
            this.button添付削除.Location = new System.Drawing.Point(354, 242);
            this.button添付削除.Name = "button添付削除";
            this.button添付削除.Size = new System.Drawing.Size(75, 23);
            this.button添付削除.TabIndex = 22;
            this.button添付削除.Text = "削除";
            this.button添付削除.UseVisualStyleBackColor = false;
            this.button添付削除.Click += new System.EventHandler(this.button添付削除_Click);
            // 
            // button添付追加
            // 
            this.button添付追加.BackColor = System.Drawing.SystemColors.Control;
            this.button添付追加.Location = new System.Drawing.Point(354, 213);
            this.button添付追加.Name = "button添付追加";
            this.button添付追加.Size = new System.Drawing.Size(75, 23);
            this.button添付追加.TabIndex = 21;
            this.button添付追加.Text = "追加";
            this.button添付追加.UseVisualStyleBackColor = false;
            this.button添付追加.Click += new System.EventHandler(this.button添付追加_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // checkBox_筆記
            // 
            this.checkBox_筆記.AutoSize = true;
            this.checkBox_筆記.Location = new System.Drawing.Point(115, 74);
            this.checkBox_筆記.Name = "checkBox_筆記";
            this.checkBox_筆記.Size = new System.Drawing.Size(48, 16);
            this.checkBox_筆記.TabIndex = 23;
            this.checkBox_筆記.Text = "筆記";
            this.checkBox_筆記.UseVisualStyleBackColor = true;
            this.checkBox_筆記.CheckedChanged += new System.EventHandler(this.checkBox_筆記_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.nullableDateTimePicker有効期限);
            this.panel1.Controls.Add(this.checkBox帳票出力);
            this.panel1.Controls.Add(this.nullableDateTimePicker取得受講日);
            this.panel1.Location = new System.Drawing.Point(3, 129);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 78);
            this.panel1.TabIndex = 24;
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox備考.Location = new System.Drawing.Point(115, 292);
            this.textBox備考.MaxLength = 500;
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.Size = new System.Drawing.Size(233, 67);
            this.textBox備考.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 295);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 25;
            this.label9.Text = "　 備考";
            // 
            // 免状免許詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(464, 449);
            this.Controls.Add(this.textBox備考);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox番号);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox_筆記);
            this.Controls.Add(this.listBox添付ファイル);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.button添付削除);
            this.Controls.Add(this.button添付追加);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.comboBox種別);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox免状);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "免状免許詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "免許／免状詳細";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox免状;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox番号;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button削除;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker有効期限;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker取得受講日;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox種別;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox帳票出力;
        private System.Windows.Forms.ListBox listBox添付ファイル;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button添付削除;
        private System.Windows.Forms.Button button添付追加;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox checkBox_筆記;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.Label label9;
    }
}