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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox職名 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker受診日 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.button更新 = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox結果詳細 = new System.Windows.Forms.TextBox();
            this.listBox添付ファイル = new System.Windows.Forms.ListBox();
            this.label17 = new System.Windows.Forms.Label();
            this.button添付削除 = new System.Windows.Forms.Button();
            this.button添付追加 = new System.Windows.Forms.Button();
            this.comboBox結果 = new System.Windows.Forms.ComboBox();
            this.comboBox種別 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker有効期限 = new NBaseUtil.NullableDateTimePicker();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "職名";
            // 
            // comboBox職名
            // 
            this.comboBox職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox職名.FormattingEnabled = true;
            this.comboBox職名.Location = new System.Drawing.Point(105, 48);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(162, 21);
            this.comboBox職名.TabIndex = 1;
            // 
            // dateTimePicker受診日
            // 
            this.dateTimePicker受診日.Location = new System.Drawing.Point(105, 77);
            this.dateTimePicker受診日.Name = "dateTimePicker受診日";
            this.dateTimePicker受診日.Size = new System.Drawing.Size(137, 19);
            this.dateTimePicker受診日.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "受診日";
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(101, 389);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 6;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(189, 389);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 7;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(277, 389);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 8;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "※";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "結果";
            // 
            // textBox結果詳細
            // 
            this.textBox結果詳細.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox結果詳細.Location = new System.Drawing.Point(105, 159);
            this.textBox結果詳細.MaxLength = 250;
            this.textBox結果詳細.Multiline = true;
            this.textBox結果詳細.Name = "textBox結果詳細";
            this.textBox結果詳細.Size = new System.Drawing.Size(314, 89);
            this.textBox結果詳細.TabIndex = 3;
            // 
            // listBox添付ファイル
            // 
            this.listBox添付ファイル.AllowDrop = true;
            this.listBox添付ファイル.FormattingEnabled = true;
            this.listBox添付ファイル.ItemHeight = 12;
            this.listBox添付ファイル.Location = new System.Drawing.Point(105, 272);
            this.listBox添付ファイル.Name = "listBox添付ファイル";
            this.listBox添付ファイル.ScrollAlwaysVisible = true;
            this.listBox添付ファイル.Size = new System.Drawing.Size(233, 64);
            this.listBox添付ファイル.TabIndex = 26;
            this.listBox添付ファイル.DragDrop += new System.Windows.Forms.DragEventHandler(this.listBox添付ファイル_DragDrop);
            this.listBox添付ファイル.DragEnter += new System.Windows.Forms.DragEventHandler(this.listBox添付ファイル_DragEnter);
            this.listBox添付ファイル.DoubleClick += new System.EventHandler(this.listBox添付ファイル_DoubleClick);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(36, 272);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 12);
            this.label17.TabIndex = 25;
            this.label17.Text = "添付ファイル";
            // 
            // button添付削除
            // 
            this.button添付削除.BackColor = System.Drawing.SystemColors.Control;
            this.button添付削除.Location = new System.Drawing.Point(344, 301);
            this.button添付削除.Name = "button添付削除";
            this.button添付削除.Size = new System.Drawing.Size(75, 23);
            this.button添付削除.TabIndex = 28;
            this.button添付削除.Text = "削除";
            this.button添付削除.UseVisualStyleBackColor = false;
            this.button添付削除.Click += new System.EventHandler(this.button添付削除_Click);
            // 
            // button添付追加
            // 
            this.button添付追加.BackColor = System.Drawing.SystemColors.Control;
            this.button添付追加.Location = new System.Drawing.Point(344, 272);
            this.button添付追加.Name = "button添付追加";
            this.button添付追加.Size = new System.Drawing.Size(75, 23);
            this.button添付追加.TabIndex = 27;
            this.button添付追加.Text = "追加";
            this.button添付追加.UseVisualStyleBackColor = false;
            this.button添付追加.Click += new System.EventHandler(this.button添付追加_Click);
            // 
            // comboBox結果
            // 
            this.comboBox結果.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox結果.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox結果.FormattingEnabled = true;
            this.comboBox結果.Location = new System.Drawing.Point(105, 131);
            this.comboBox結果.Name = "comboBox結果";
            this.comboBox結果.Size = new System.Drawing.Size(162, 21);
            this.comboBox結果.TabIndex = 30;
            // 
            // comboBox種別
            // 
            this.comboBox種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox種別.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox種別.FormattingEnabled = true;
            this.comboBox種別.Location = new System.Drawing.Point(105, 19);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(162, 21);
            this.comboBox種別.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "種別";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 33;
            this.label5.Text = "有効期限";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "※";
            // 
            // nullableDateTimePicker有効期限
            // 
            this.nullableDateTimePicker有効期限.Location = new System.Drawing.Point(105, 104);
            this.nullableDateTimePicker有効期限.Name = "nullableDateTimePicker有効期限";
            this.nullableDateTimePicker有効期限.Size = new System.Drawing.Size(137, 19);
            this.nullableDateTimePicker有効期限.TabIndex = 34;
            this.nullableDateTimePicker有効期限.Value = null;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // 健康診断詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(452, 424);
            this.Controls.Add(this.nullableDateTimePicker有効期限);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox種別);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox結果);
            this.Controls.Add(this.listBox添付ファイル);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.button添付削除);
            this.Controls.Add(this.button添付追加);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox結果詳細);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker受診日);
            this.Controls.Add(this.comboBox職名);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "健康診断詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "健康診断詳細Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox職名;
        private System.Windows.Forms.DateTimePicker dateTimePicker受診日;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox結果詳細;
        private System.Windows.Forms.ListBox listBox添付ファイル;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button button添付削除;
        private System.Windows.Forms.Button button添付追加;
        private System.Windows.Forms.ComboBox comboBox結果;
        private System.Windows.Forms.ComboBox comboBox種別;
        private System.Windows.Forms.Label label3;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker有効期限;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}