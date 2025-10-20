namespace Senin
{
    partial class 傷病詳細Form
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
            this.dateTimePicker対象期間_開始 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox金額 = new System.Windows.Forms.TextBox();
            this.textBox日額 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button更新 = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox等級 = new System.Windows.Forms.TextBox();
            this.nullableDateTimePicker対象期間_終了 = new NBaseUtil.NullableDateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox傷病名 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox口座 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker書類送付日 = new NBaseUtil.NullableDateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker書類返送 = new NBaseUtil.NullableDateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker提出日 = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker通知 = new NBaseUtil.NullableDateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker立替金伝票 = new NBaseUtil.NullableDateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker入金伝票 = new NBaseUtil.NullableDateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker送金 = new NBaseUtil.NullableDateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker本人郵送 = new NBaseUtil.NullableDateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.listBox添付ファイル = new System.Windows.Forms.ListBox();
            this.label17 = new System.Windows.Forms.Label();
            this.button添付削除 = new System.Windows.Forms.Button();
            this.button添付追加 = new System.Windows.Forms.Button();
            this.comboBoxステータス = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.comboBox種別 = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 175);
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
            this.comboBox職名.Location = new System.Drawing.Point(105, 171);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(162, 21);
            this.comboBox職名.TabIndex = 7;
            // 
            // dateTimePicker対象期間_開始
            // 
            this.dateTimePicker対象期間_開始.Location = new System.Drawing.Point(105, 120);
            this.dateTimePicker対象期間_開始.Name = "dateTimePicker対象期間_開始";
            this.dateTimePicker対象期間_開始.Size = new System.Drawing.Size(137, 19);
            this.dateTimePicker対象期間_開始.TabIndex = 4;
            this.dateTimePicker対象期間_開始.ValueChanged += new System.EventHandler(this.dateTimePicker対象期間_開始_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "対象期間";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "金額";
            // 
            // textBox金額
            // 
            this.textBox金額.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox金額.Location = new System.Drawing.Point(105, 248);
            this.textBox金額.MaxLength = 8;
            this.textBox金額.Name = "textBox金額";
            this.textBox金額.Size = new System.Drawing.Size(100, 19);
            this.textBox金額.TabIndex = 10;
            this.textBox金額.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox日額
            // 
            this.textBox日額.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox日額.Location = new System.Drawing.Point(105, 95);
            this.textBox日額.MaxLength = 6;
            this.textBox日額.Name = "textBox日額";
            this.textBox日額.ReadOnly = true;
            this.textBox日額.Size = new System.Drawing.Size(100, 19);
            this.textBox日額.TabIndex = 4;
            this.textBox日額.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "日額";
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(247, 387);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 22;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(335, 387);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 23;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(423, 387);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 24;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "等級";
            // 
            // textBox等級
            // 
            this.textBox等級.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox等級.Location = new System.Drawing.Point(105, 70);
            this.textBox等級.MaxLength = 3;
            this.textBox等級.Name = "textBox等級";
            this.textBox等級.ReadOnly = true;
            this.textBox等級.Size = new System.Drawing.Size(43, 19);
            this.textBox等級.TabIndex = 3;
            this.textBox等級.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nullableDateTimePicker対象期間_終了
            // 
            this.nullableDateTimePicker対象期間_終了.Location = new System.Drawing.Point(268, 120);
            this.nullableDateTimePicker対象期間_終了.Name = "nullableDateTimePicker対象期間_終了";
            this.nullableDateTimePicker対象期間_終了.Size = new System.Drawing.Size(137, 19);
            this.nullableDateTimePicker対象期間_終了.TabIndex = 5;
            this.nullableDateTimePicker対象期間_終了.Value = new System.DateTime(2017, 11, 7, 16, 31, 30, 802);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(248, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "～";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 201);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "傷病名";
            // 
            // textBox傷病名
            // 
            this.textBox傷病名.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox傷病名.Location = new System.Drawing.Point(104, 198);
            this.textBox傷病名.MaxLength = 100;
            this.textBox傷病名.Name = "textBox傷病名";
            this.textBox傷病名.Size = new System.Drawing.Size(300, 19);
            this.textBox傷病名.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(33, 226);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "口座";
            // 
            // textBox口座
            // 
            this.textBox口座.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox口座.Location = new System.Drawing.Point(105, 223);
            this.textBox口座.MaxLength = 127;
            this.textBox口座.Name = "textBox口座";
            this.textBox口座.Size = new System.Drawing.Size(300, 19);
            this.textBox口座.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(475, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "書類送付";
            // 
            // nullableDateTimePicker書類送付日
            // 
            this.nullableDateTimePicker書類送付日.Location = new System.Drawing.Point(557, 41);
            this.nullableDateTimePicker書類送付日.Name = "nullableDateTimePicker書類送付日";
            this.nullableDateTimePicker書類送付日.Size = new System.Drawing.Size(137, 19);
            this.nullableDateTimePicker書類送付日.TabIndex = 11;
            this.nullableDateTimePicker書類送付日.Value = null;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(475, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "書類返送";
            // 
            // nullableDateTimePicker書類返送
            // 
            this.nullableDateTimePicker書類返送.Location = new System.Drawing.Point(557, 68);
            this.nullableDateTimePicker書類返送.Name = "nullableDateTimePicker書類返送";
            this.nullableDateTimePicker書類返送.Size = new System.Drawing.Size(137, 19);
            this.nullableDateTimePicker書類返送.TabIndex = 12;
            this.nullableDateTimePicker書類返送.Value = null;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(475, 98);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 15;
            this.label11.Text = "提出日";
            // 
            // nullableDateTimePicker提出日
            // 
            this.nullableDateTimePicker提出日.Location = new System.Drawing.Point(557, 93);
            this.nullableDateTimePicker提出日.Name = "nullableDateTimePicker提出日";
            this.nullableDateTimePicker提出日.Size = new System.Drawing.Size(137, 19);
            this.nullableDateTimePicker提出日.TabIndex = 13;
            this.nullableDateTimePicker提出日.Value = null;
            // 
            // nullableDateTimePicker通知
            // 
            this.nullableDateTimePicker通知.Location = new System.Drawing.Point(557, 119);
            this.nullableDateTimePicker通知.Name = "nullableDateTimePicker通知";
            this.nullableDateTimePicker通知.Size = new System.Drawing.Size(137, 19);
            this.nullableDateTimePicker通知.TabIndex = 14;
            this.nullableDateTimePicker通知.Value = null;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(475, 123);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 15;
            this.label12.Text = "通知";
            // 
            // nullableDateTimePicker立替金伝票
            // 
            this.nullableDateTimePicker立替金伝票.Location = new System.Drawing.Point(557, 146);
            this.nullableDateTimePicker立替金伝票.Name = "nullableDateTimePicker立替金伝票";
            this.nullableDateTimePicker立替金伝票.Size = new System.Drawing.Size(137, 19);
            this.nullableDateTimePicker立替金伝票.TabIndex = 15;
            this.nullableDateTimePicker立替金伝票.Value = null;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(475, 148);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 17;
            this.label13.Text = "立替金伝票";
            // 
            // nullableDateTimePicker入金伝票
            // 
            this.nullableDateTimePicker入金伝票.Location = new System.Drawing.Point(557, 171);
            this.nullableDateTimePicker入金伝票.Name = "nullableDateTimePicker入金伝票";
            this.nullableDateTimePicker入金伝票.Size = new System.Drawing.Size(137, 19);
            this.nullableDateTimePicker入金伝票.TabIndex = 16;
            this.nullableDateTimePicker入金伝票.Value = null;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(475, 176);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 19;
            this.label14.Text = "入金伝票";
            // 
            // nullableDateTimePicker送金
            // 
            this.nullableDateTimePicker送金.Location = new System.Drawing.Point(557, 196);
            this.nullableDateTimePicker送金.Name = "nullableDateTimePicker送金";
            this.nullableDateTimePicker送金.Size = new System.Drawing.Size(137, 19);
            this.nullableDateTimePicker送金.TabIndex = 17;
            this.nullableDateTimePicker送金.Value = null;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(475, 201);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 12);
            this.label15.TabIndex = 21;
            this.label15.Text = "送金";
            // 
            // nullableDateTimePicker本人郵送
            // 
            this.nullableDateTimePicker本人郵送.Location = new System.Drawing.Point(557, 221);
            this.nullableDateTimePicker本人郵送.Name = "nullableDateTimePicker本人郵送";
            this.nullableDateTimePicker本人郵送.Size = new System.Drawing.Size(137, 19);
            this.nullableDateTimePicker本人郵送.TabIndex = 18;
            this.nullableDateTimePicker本人郵送.Value = null;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(475, 226);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 23;
            this.label16.Text = "本人郵送";
            // 
            // listBox添付ファイル
            // 
            this.listBox添付ファイル.AllowDrop = true;
            this.listBox添付ファイル.FormattingEnabled = true;
            this.listBox添付ファイル.ItemHeight = 12;
            this.listBox添付ファイル.Location = new System.Drawing.Point(104, 284);
            this.listBox添付ファイル.Name = "listBox添付ファイル";
            this.listBox添付ファイル.ScrollAlwaysVisible = true;
            this.listBox添付ファイル.Size = new System.Drawing.Size(233, 64);
            this.listBox添付ファイル.TabIndex = 19;
            this.listBox添付ファイル.DoubleClick += new System.EventHandler(this.listBox添付ファイル_DoubleClick);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(33, 284);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 12);
            this.label17.TabIndex = 25;
            this.label17.Text = "添付ファイル";
            // 
            // button添付削除
            // 
            this.button添付削除.BackColor = System.Drawing.SystemColors.Control;
            this.button添付削除.Location = new System.Drawing.Point(343, 313);
            this.button添付削除.Name = "button添付削除";
            this.button添付削除.Size = new System.Drawing.Size(75, 23);
            this.button添付削除.TabIndex = 21;
            this.button添付削除.Text = "削除";
            this.button添付削除.UseVisualStyleBackColor = false;
            this.button添付削除.Click += new System.EventHandler(this.button添付削除_Click);
            // 
            // button添付追加
            // 
            this.button添付追加.BackColor = System.Drawing.SystemColors.Control;
            this.button添付追加.Location = new System.Drawing.Point(343, 284);
            this.button添付追加.Name = "button添付追加";
            this.button添付追加.Size = new System.Drawing.Size(75, 23);
            this.button添付追加.TabIndex = 20;
            this.button添付追加.Text = "追加";
            this.button添付追加.UseVisualStyleBackColor = false;
            this.button添付追加.Click += new System.EventHandler(this.button添付追加_Click);
            // 
            // comboBoxステータス
            // 
            this.comboBoxステータス.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxステータス.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBoxステータス.FormattingEnabled = true;
            this.comboBoxステータス.Location = new System.Drawing.Point(105, 42);
            this.comboBoxステータス.Name = "comboBoxステータス";
            this.comboBoxステータス.Size = new System.Drawing.Size(162, 21);
            this.comboBoxステータス.TabIndex = 2;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(33, 46);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 12);
            this.label18.TabIndex = 29;
            this.label18.Text = "ステータス";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(33, 150);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(17, 12);
            this.label19.TabIndex = 32;
            this.label19.Text = "船";
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(105, 145);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(162, 20);
            this.comboBox船.TabIndex = 6;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // comboBox種別
            // 
            this.comboBox種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox種別.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox種別.FormattingEnabled = true;
            this.comboBox種別.Location = new System.Drawing.Point(105, 15);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(162, 21);
            this.comboBox種別.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(33, 19);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 12);
            this.label20.TabIndex = 33;
            this.label20.Text = "種別";
            // 
            // 傷病詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(744, 422);
            this.Controls.Add(this.comboBox種別);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.comboBox船);
            this.Controls.Add(this.comboBoxステータス);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.listBox添付ファイル);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.button添付削除);
            this.Controls.Add(this.button添付追加);
            this.Controls.Add(this.nullableDateTimePicker本人郵送);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.nullableDateTimePicker送金);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.nullableDateTimePicker入金伝票);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.nullableDateTimePicker立替金伝票);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.nullableDateTimePicker通知);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.nullableDateTimePicker提出日);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.nullableDateTimePicker書類返送);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nullableDateTimePicker書類送付日);
            this.Controls.Add(this.nullableDateTimePicker対象期間_終了);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.textBox等級);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox日額);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox口座);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox傷病名);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox金額);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker対象期間_開始);
            this.Controls.Add(this.comboBox職名);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "傷病詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "傷病詳細";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox職名;
        private System.Windows.Forms.DateTimePicker dateTimePicker対象期間_開始;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox金額;
        private System.Windows.Forms.TextBox textBox日額;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox等級;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker対象期間_終了;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox傷病名;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox口座;
        private System.Windows.Forms.Label label5;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker書類送付日;
        private System.Windows.Forms.Label label10;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker書類返送;
        private System.Windows.Forms.Label label11;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker提出日;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker通知;
        private System.Windows.Forms.Label label12;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker立替金伝票;
        private System.Windows.Forms.Label label13;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker入金伝票;
        private System.Windows.Forms.Label label14;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker送金;
        private System.Windows.Forms.Label label15;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker本人郵送;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ListBox listBox添付ファイル;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button button添付削除;
        private System.Windows.Forms.Button button添付追加;
        private System.Windows.Forms.ComboBox comboBoxステータス;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox comboBox種別;
        private System.Windows.Forms.Label label20;
    }
}