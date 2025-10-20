namespace Hachu.Reports
{
    partial class データ出力Form
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
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.label船 = new System.Windows.Forms.Label();
            this.comboBox取引先 = new System.Windows.Forms.ComboBox();
            this.label取引先 = new System.Windows.Forms.Label();
            this.label詳細種別 = new System.Windows.Forms.Label();
            this.comboBox詳細種別 = new System.Windows.Forms.ComboBox();
            this.label種別 = new System.Windows.Forms.Label();
            this.comboBox種別 = new System.Windows.Forms.ComboBox();
            this.label手配依頼日 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label発注日 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label受領日 = new System.Windows.Forms.Label();
            this.checkBox受領済 = new System.Windows.Forms.CheckBox();
            this.checkBox完了 = new System.Windows.Forms.CheckBox();
            this.checkBox発注済 = new System.Windows.Forms.CheckBox();
            this.checkBox未対応 = new System.Windows.Forms.CheckBox();
            this.label状況 = new System.Windows.Forms.Label();
            this.checkBox見積中 = new System.Windows.Forms.CheckBox();
            this.labelファイル = new System.Windows.Forms.Label();
            this.radioButtonヘッダー = new System.Windows.Forms.RadioButton();
            this.radioButton詳細品目 = new System.Windows.Forms.RadioButton();
            this.label手配依頼番号 = new System.Windows.Forms.Label();
            this.textBox手配依頼番号From = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox手配依頼番号To = new System.Windows.Forms.TextBox();
            this.nullableDateTimePicker受領日To = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker受領日From = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker発注日To = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker発注日From = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker手配依頼日To = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker手配依頼日From = new NBaseUtil.NullableDateTimePicker();
            this.SuspendLayout();
            // 
            // button出力
            // 
            this.button出力.Location = new System.Drawing.Point(242, 298);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(75, 23);
            this.button出力.TabIndex = 4;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = true;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(93, 23);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(250, 20);
            this.comboBox船.TabIndex = 3;
            // 
            // label船
            // 
            this.label船.AutoSize = true;
            this.label船.Location = new System.Drawing.Point(70, 26);
            this.label船.Name = "label船";
            this.label船.Size = new System.Drawing.Size(17, 12);
            this.label船.TabIndex = 0;
            this.label船.Text = "船";
            // 
            // comboBox取引先
            // 
            this.comboBox取引先.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox取引先.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBox取引先.DropDownWidth = 200;
            this.comboBox取引先.FormattingEnabled = true;
            this.comboBox取引先.ImeMode = System.Windows.Forms.ImeMode.On;
            this.comboBox取引先.Location = new System.Drawing.Point(93, 52);
            this.comboBox取引先.Name = "comboBox取引先";
            this.comboBox取引先.Size = new System.Drawing.Size(250, 20);
            this.comboBox取引先.TabIndex = 6;
            // 
            // label取引先
            // 
            this.label取引先.AutoSize = true;
            this.label取引先.Location = new System.Drawing.Point(46, 55);
            this.label取引先.Name = "label取引先";
            this.label取引先.Size = new System.Drawing.Size(41, 12);
            this.label取引先.TabIndex = 5;
            this.label取引先.Text = "取引先";
            // 
            // label詳細種別
            // 
            this.label詳細種別.AutoSize = true;
            this.label詳細種別.Location = new System.Drawing.Point(296, 84);
            this.label詳細種別.Name = "label詳細種別";
            this.label詳細種別.Size = new System.Drawing.Size(53, 12);
            this.label詳細種別.TabIndex = 27;
            this.label詳細種別.Text = "詳細種別";
            // 
            // comboBox詳細種別
            // 
            this.comboBox詳細種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox詳細種別.FormattingEnabled = true;
            this.comboBox詳細種別.Items.AddRange(new object[] {
            "船用品",
            "潤滑油",
            "修繕"});
            this.comboBox詳細種別.Location = new System.Drawing.Point(355, 81);
            this.comboBox詳細種別.Name = "comboBox詳細種別";
            this.comboBox詳細種別.Size = new System.Drawing.Size(170, 20);
            this.comboBox詳細種別.TabIndex = 29;
            // 
            // label種別
            // 
            this.label種別.AutoSize = true;
            this.label種別.Location = new System.Drawing.Point(58, 84);
            this.label種別.Name = "label種別";
            this.label種別.Size = new System.Drawing.Size(29, 12);
            this.label種別.TabIndex = 26;
            this.label種別.Text = "種別";
            // 
            // comboBox種別
            // 
            this.comboBox種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox種別.FormattingEnabled = true;
            this.comboBox種別.Items.AddRange(new object[] {
            "船用品",
            "潤滑油",
            "修繕"});
            this.comboBox種別.Location = new System.Drawing.Point(93, 81);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(170, 20);
            this.comboBox種別.TabIndex = 28;
            this.comboBox種別.SelectedIndexChanged += new System.EventHandler(this.comboBox種別_SelectedIndexChanged);
            // 
            // label手配依頼日
            // 
            this.label手配依頼日.AutoSize = true;
            this.label手配依頼日.Location = new System.Drawing.Point(22, 113);
            this.label手配依頼日.Name = "label手配依頼日";
            this.label手配依頼日.Size = new System.Drawing.Size(65, 12);
            this.label手配依頼日.TabIndex = 31;
            this.label手配依頼日.Text = "手配依頼日";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "～";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(222, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 37;
            this.label3.Text = "～";
            // 
            // label発注日
            // 
            this.label発注日.AutoSize = true;
            this.label発注日.Location = new System.Drawing.Point(46, 142);
            this.label発注日.Name = "label発注日";
            this.label発注日.Size = new System.Drawing.Size(41, 12);
            this.label発注日.TabIndex = 35;
            this.label発注日.Text = "発注日";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(222, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 41;
            this.label5.Text = "～";
            // 
            // label受領日
            // 
            this.label受領日.AutoSize = true;
            this.label受領日.Location = new System.Drawing.Point(46, 171);
            this.label受領日.Name = "label受領日";
            this.label受領日.Size = new System.Drawing.Size(41, 12);
            this.label受領日.TabIndex = 39;
            this.label受領日.Text = "受領日";
            // 
            // checkBox受領済
            // 
            this.checkBox受領済.AutoSize = true;
            this.checkBox受領済.Checked = true;
            this.checkBox受領済.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox受領済.Location = new System.Drawing.Point(291, 221);
            this.checkBox受領済.Name = "checkBox受領済";
            this.checkBox受領済.Size = new System.Drawing.Size(60, 16);
            this.checkBox受領済.TabIndex = 47;
            this.checkBox受領済.Text = "受領済";
            this.checkBox受領済.UseVisualStyleBackColor = true;
            // 
            // checkBox完了
            // 
            this.checkBox完了.AutoSize = true;
            this.checkBox完了.Checked = true;
            this.checkBox完了.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox完了.Location = new System.Drawing.Point(356, 221);
            this.checkBox完了.Name = "checkBox完了";
            this.checkBox完了.Size = new System.Drawing.Size(48, 16);
            this.checkBox完了.TabIndex = 46;
            this.checkBox完了.Text = "完了";
            this.checkBox完了.UseVisualStyleBackColor = true;
            // 
            // checkBox発注済
            // 
            this.checkBox発注済.AutoSize = true;
            this.checkBox発注済.Checked = true;
            this.checkBox発注済.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox発注済.Location = new System.Drawing.Point(225, 221);
            this.checkBox発注済.Name = "checkBox発注済";
            this.checkBox発注済.Size = new System.Drawing.Size(60, 16);
            this.checkBox発注済.TabIndex = 45;
            this.checkBox発注済.Text = "発注済";
            this.checkBox発注済.UseVisualStyleBackColor = true;
            // 
            // checkBox未対応
            // 
            this.checkBox未対応.AutoSize = true;
            this.checkBox未対応.Checked = true;
            this.checkBox未対応.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox未対応.Location = new System.Drawing.Point(93, 221);
            this.checkBox未対応.Name = "checkBox未対応";
            this.checkBox未対応.Size = new System.Drawing.Size(60, 16);
            this.checkBox未対応.TabIndex = 43;
            this.checkBox未対応.Text = "未対応";
            this.checkBox未対応.UseVisualStyleBackColor = true;
            // 
            // label状況
            // 
            this.label状況.AutoSize = true;
            this.label状況.Location = new System.Drawing.Point(58, 222);
            this.label状況.Name = "label状況";
            this.label状況.Size = new System.Drawing.Size(29, 12);
            this.label状況.TabIndex = 42;
            this.label状況.Text = "状況";
            // 
            // checkBox見積中
            // 
            this.checkBox見積中.AutoSize = true;
            this.checkBox見積中.Checked = true;
            this.checkBox見積中.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox見積中.Location = new System.Drawing.Point(159, 221);
            this.checkBox見積中.Name = "checkBox見積中";
            this.checkBox見積中.Size = new System.Drawing.Size(60, 16);
            this.checkBox見積中.TabIndex = 44;
            this.checkBox見積中.Text = "見積中";
            this.checkBox見積中.UseVisualStyleBackColor = true;
            // 
            // labelファイル
            // 
            this.labelファイル.AutoSize = true;
            this.labelファイル.Location = new System.Drawing.Point(48, 251);
            this.labelファイル.Name = "labelファイル";
            this.labelファイル.Size = new System.Drawing.Size(39, 12);
            this.labelファイル.TabIndex = 48;
            this.labelファイル.Text = "ファイル";
            // 
            // radioButtonヘッダー
            // 
            this.radioButtonヘッダー.AutoSize = true;
            this.radioButtonヘッダー.Checked = true;
            this.radioButtonヘッダー.Location = new System.Drawing.Point(93, 249);
            this.radioButtonヘッダー.Name = "radioButtonヘッダー";
            this.radioButtonヘッダー.Size = new System.Drawing.Size(59, 16);
            this.radioButtonヘッダー.TabIndex = 49;
            this.radioButtonヘッダー.TabStop = true;
            this.radioButtonヘッダー.Text = "ヘッダー";
            this.radioButtonヘッダー.UseVisualStyleBackColor = true;
            // 
            // radioButton詳細品目
            // 
            this.radioButton詳細品目.AutoSize = true;
            this.radioButton詳細品目.Location = new System.Drawing.Point(160, 249);
            this.radioButton詳細品目.Name = "radioButton詳細品目";
            this.radioButton詳細品目.Size = new System.Drawing.Size(71, 16);
            this.radioButton詳細品目.TabIndex = 50;
            this.radioButton詳細品目.Text = "詳細品目";
            this.radioButton詳細品目.UseVisualStyleBackColor = true;
            // 
            // label手配依頼番号
            // 
            this.label手配依頼番号.AutoSize = true;
            this.label手配依頼番号.Location = new System.Drawing.Point(10, 197);
            this.label手配依頼番号.Name = "label手配依頼番号";
            this.label手配依頼番号.Size = new System.Drawing.Size(77, 12);
            this.label手配依頼番号.TabIndex = 51;
            this.label手配依頼番号.Text = "手配依頼番号";
            // 
            // textBox手配依頼番号From
            // 
            this.textBox手配依頼番号From.Location = new System.Drawing.Point(93, 194);
            this.textBox手配依頼番号From.MaxLength = 7;
            this.textBox手配依頼番号From.Name = "textBox手配依頼番号From";
            this.textBox手配依頼番号From.Size = new System.Drawing.Size(120, 19);
            this.textBox手配依頼番号From.TabIndex = 52;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(223, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 53;
            this.label1.Text = "～";
            // 
            // textBox手配依頼番号To
            // 
            this.textBox手配依頼番号To.Location = new System.Drawing.Point(245, 194);
            this.textBox手配依頼番号To.MaxLength = 7;
            this.textBox手配依頼番号To.Name = "textBox手配依頼番号To";
            this.textBox手配依頼番号To.Size = new System.Drawing.Size(120, 19);
            this.textBox手配依頼番号To.TabIndex = 54;
            // 
            // nullableDateTimePicker受領日To
            // 
            this.nullableDateTimePicker受領日To.Location = new System.Drawing.Point(245, 168);
            this.nullableDateTimePicker受領日To.Name = "nullableDateTimePicker受領日To";
            this.nullableDateTimePicker受領日To.Size = new System.Drawing.Size(120, 19);
            this.nullableDateTimePicker受領日To.TabIndex = 40;
            this.nullableDateTimePicker受領日To.Value = null;
            // 
            // nullableDateTimePicker受領日From
            // 
            this.nullableDateTimePicker受領日From.Location = new System.Drawing.Point(93, 168);
            this.nullableDateTimePicker受領日From.Name = "nullableDateTimePicker受領日From";
            this.nullableDateTimePicker受領日From.Size = new System.Drawing.Size(120, 19);
            this.nullableDateTimePicker受領日From.TabIndex = 38;
            this.nullableDateTimePicker受領日From.Value = null;
            // 
            // nullableDateTimePicker発注日To
            // 
            this.nullableDateTimePicker発注日To.Location = new System.Drawing.Point(245, 139);
            this.nullableDateTimePicker発注日To.Name = "nullableDateTimePicker発注日To";
            this.nullableDateTimePicker発注日To.Size = new System.Drawing.Size(120, 19);
            this.nullableDateTimePicker発注日To.TabIndex = 36;
            this.nullableDateTimePicker発注日To.Value = null;
            // 
            // nullableDateTimePicker発注日From
            // 
            this.nullableDateTimePicker発注日From.Location = new System.Drawing.Point(93, 139);
            this.nullableDateTimePicker発注日From.Name = "nullableDateTimePicker発注日From";
            this.nullableDateTimePicker発注日From.Size = new System.Drawing.Size(120, 19);
            this.nullableDateTimePicker発注日From.TabIndex = 34;
            this.nullableDateTimePicker発注日From.Value = null;
            // 
            // nullableDateTimePicker手配依頼日To
            // 
            this.nullableDateTimePicker手配依頼日To.Location = new System.Drawing.Point(245, 111);
            this.nullableDateTimePicker手配依頼日To.Name = "nullableDateTimePicker手配依頼日To";
            this.nullableDateTimePicker手配依頼日To.Size = new System.Drawing.Size(120, 19);
            this.nullableDateTimePicker手配依頼日To.TabIndex = 32;
            this.nullableDateTimePicker手配依頼日To.Value = null;
            // 
            // nullableDateTimePicker手配依頼日From
            // 
            this.nullableDateTimePicker手配依頼日From.Location = new System.Drawing.Point(93, 110);
            this.nullableDateTimePicker手配依頼日From.Name = "nullableDateTimePicker手配依頼日From";
            this.nullableDateTimePicker手配依頼日From.Size = new System.Drawing.Size(120, 19);
            this.nullableDateTimePicker手配依頼日From.TabIndex = 30;
            this.nullableDateTimePicker手配依頼日From.Value = null;
            // 
            // データ出力Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(546, 344);
            this.Controls.Add(this.textBox手配依頼番号To);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox手配依頼番号From);
            this.Controls.Add(this.label手配依頼番号);
            this.Controls.Add(this.radioButton詳細品目);
            this.Controls.Add(this.radioButtonヘッダー);
            this.Controls.Add(this.labelファイル);
            this.Controls.Add(this.checkBox受領済);
            this.Controls.Add(this.checkBox完了);
            this.Controls.Add(this.checkBox発注済);
            this.Controls.Add(this.checkBox未対応);
            this.Controls.Add(this.label状況);
            this.Controls.Add(this.checkBox見積中);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nullableDateTimePicker受領日To);
            this.Controls.Add(this.label受領日);
            this.Controls.Add(this.nullableDateTimePicker受領日From);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nullableDateTimePicker発注日To);
            this.Controls.Add(this.label発注日);
            this.Controls.Add(this.nullableDateTimePicker発注日From);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nullableDateTimePicker手配依頼日To);
            this.Controls.Add(this.label手配依頼日);
            this.Controls.Add(this.nullableDateTimePicker手配依頼日From);
            this.Controls.Add(this.label詳細種別);
            this.Controls.Add(this.comboBox詳細種別);
            this.Controls.Add(this.label種別);
            this.Controls.Add(this.comboBox種別);
            this.Controls.Add(this.comboBox取引先);
            this.Controls.Add(this.label取引先);
            this.Controls.Add(this.label船);
            this.Controls.Add(this.comboBox船);
            this.Controls.Add(this.button出力);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "データ出力Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CSV出力Form";
            this.Load += new System.EventHandler(this.データ出力Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label船;
        private System.Windows.Forms.ComboBox comboBox取引先;
        private System.Windows.Forms.Label label取引先;
        private System.Windows.Forms.Label label詳細種別;
        private System.Windows.Forms.ComboBox comboBox詳細種別;
        private System.Windows.Forms.Label label種別;
        private System.Windows.Forms.ComboBox comboBox種別;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker手配依頼日From;
        private System.Windows.Forms.Label label手配依頼日;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker手配依頼日To;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker発注日To;
        private System.Windows.Forms.Label label発注日;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker発注日From;
        private System.Windows.Forms.Label label5;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker受領日To;
        private System.Windows.Forms.Label label受領日;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker受領日From;
        private System.Windows.Forms.CheckBox checkBox受領済;
        private System.Windows.Forms.CheckBox checkBox完了;
        private System.Windows.Forms.CheckBox checkBox発注済;
        private System.Windows.Forms.CheckBox checkBox未対応;
        private System.Windows.Forms.Label label状況;
        private System.Windows.Forms.CheckBox checkBox見積中;
        private System.Windows.Forms.Label labelファイル;
        private System.Windows.Forms.RadioButton radioButtonヘッダー;
        private System.Windows.Forms.RadioButton radioButton詳細品目;
        private System.Windows.Forms.Label label手配依頼番号;
        private System.Windows.Forms.TextBox textBox手配依頼番号From;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox手配依頼番号To;
    }
}