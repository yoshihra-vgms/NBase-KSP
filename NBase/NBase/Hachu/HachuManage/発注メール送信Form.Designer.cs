namespace Hachu.HachuManage
{
    partial class 発注メール送信Form
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
            this.label発注先 = new System.Windows.Forms.Label();
            this.labelメールアドレス = new System.Windows.Forms.Label();
            this.labelメール件名 = new System.Windows.Forms.Label();
            this.textBoxメール件名 = new System.Windows.Forms.TextBox();
            this.label担当者 = new System.Windows.Forms.Label();
            this.textBoxメールアドレス = new System.Windows.Forms.TextBox();
            this.textBox発注先 = new System.Windows.Forms.TextBox();
            this.comboBox担当者 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox納品場所 = new System.Windows.Forms.TextBox();
            this.label送り先 = new System.Windows.Forms.Label();
            this.textBoxFaxNo = new System.Windows.Forms.TextBox();
            this.dateTimePicker希望納期 = new System.Windows.Forms.DateTimePicker();
            this.label希望納期 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxTelNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox発注番号 = new System.Windows.Forms.TextBox();
            this.label発注番号 = new System.Windows.Forms.Label();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button送信 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label発注先
            // 
            this.label発注先.AutoSize = true;
            this.label発注先.Location = new System.Drawing.Point(61, 71);
            this.label発注先.Name = "label発注先";
            this.label発注先.Size = new System.Drawing.Size(41, 12);
            this.label発注先.TabIndex = 0;
            this.label発注先.Text = "発注先";
            // 
            // labelメールアドレス
            // 
            this.labelメールアドレス.AutoSize = true;
            this.labelメールアドレス.Location = new System.Drawing.Point(33, 291);
            this.labelメールアドレス.Name = "labelメールアドレス";
            this.labelメールアドレス.Size = new System.Drawing.Size(69, 12);
            this.labelメールアドレス.TabIndex = 0;
            this.labelメールアドレス.Text = "メールアドレス";
            // 
            // labelメール件名
            // 
            this.labelメール件名.AutoSize = true;
            this.labelメール件名.Location = new System.Drawing.Point(46, 317);
            this.labelメール件名.Name = "labelメール件名";
            this.labelメール件名.Size = new System.Drawing.Size(57, 12);
            this.labelメール件名.TabIndex = 0;
            this.labelメール件名.Text = "メール件名";
            // 
            // textBoxメール件名
            // 
            this.textBoxメール件名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBoxメール件名.Location = new System.Drawing.Point(106, 314);
            this.textBoxメール件名.MaxLength = 50;
            this.textBoxメール件名.Name = "textBoxメール件名";
            this.textBoxメール件名.Size = new System.Drawing.Size(425, 19);
            this.textBoxメール件名.TabIndex = 7;
            // 
            // label担当者
            // 
            this.label担当者.AutoSize = true;
            this.label担当者.Location = new System.Drawing.Point(62, 97);
            this.label担当者.Name = "label担当者";
            this.label担当者.Size = new System.Drawing.Size(41, 12);
            this.label担当者.TabIndex = 0;
            this.label担当者.Text = "担当者";
            // 
            // textBoxメールアドレス
            // 
            this.textBoxメールアドレス.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxメールアドレス.Location = new System.Drawing.Point(106, 289);
            this.textBoxメールアドレス.MaxLength = 50;
            this.textBoxメールアドレス.Name = "textBoxメールアドレス";
            this.textBoxメールアドレス.Size = new System.Drawing.Size(225, 19);
            this.textBoxメールアドレス.TabIndex = 6;
            // 
            // textBox発注先
            // 
            this.textBox発注先.Location = new System.Drawing.Point(106, 68);
            this.textBox発注先.Name = "textBox発注先";
            this.textBox発注先.ReadOnly = true;
            this.textBox発注先.Size = new System.Drawing.Size(225, 19);
            this.textBox発注先.TabIndex = 0;
            this.textBox発注先.TabStop = false;
            // 
            // comboBox担当者
            // 
            this.comboBox担当者.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox担当者.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBox担当者.FormattingEnabled = true;
            this.comboBox担当者.ImeMode = System.Windows.Forms.ImeMode.On;
            this.comboBox担当者.Location = new System.Drawing.Point(106, 94);
            this.comboBox担当者.MaxLength = 50;
            this.comboBox担当者.Name = "comboBox担当者";
            this.comboBox担当者.Size = new System.Drawing.Size(225, 20);
            this.comboBox担当者.TabIndex = 3;
            this.comboBox担当者.SelectedIndexChanged += new System.EventHandler(this.comboBox担当者_SelectedIndexChanged);
            this.comboBox担当者.TextChanged += new System.EventHandler(this.comboBox担当者_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(46, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "発注メールを送信します。";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(43, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "※";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(27, 317);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "※";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox備考);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBox納品場所);
            this.panel1.Controls.Add(this.label送り先);
            this.panel1.Controls.Add(this.textBoxFaxNo);
            this.panel1.Controls.Add(this.dateTimePicker希望納期);
            this.panel1.Controls.Add(this.label希望納期);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBoxTelNo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox発注番号);
            this.panel1.Controls.Add(this.label発注番号);
            this.panel1.Controls.Add(this.button閉じる);
            this.panel1.Controls.Add(this.button送信);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox担当者);
            this.panel1.Controls.Add(this.textBox発注先);
            this.panel1.Controls.Add(this.textBoxメールアドレス);
            this.panel1.Controls.Add(this.label担当者);
            this.panel1.Controls.Add(this.textBoxメール件名);
            this.panel1.Controls.Add(this.labelメール件名);
            this.panel1.Controls.Add(this.labelメールアドレス);
            this.panel1.Controls.Add(this.label発注先);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(574, 430);
            this.panel1.TabIndex = 1;
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox備考.Location = new System.Drawing.Point(106, 229);
            this.textBox備考.MaxLength = 250;
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.Size = new System.Drawing.Size(306, 53);
            this.textBox備考.TabIndex = 45;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 232);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 44;
            this.label7.Text = "備考";
            // 
            // textBox納品場所
            // 
            this.textBox納品場所.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox納品場所.Location = new System.Drawing.Point(106, 170);
            this.textBox納品場所.MaxLength = 100;
            this.textBox納品場所.Multiline = true;
            this.textBox納品場所.Name = "textBox納品場所";
            this.textBox納品場所.Size = new System.Drawing.Size(306, 53);
            this.textBox納品場所.TabIndex = 43;
            // 
            // label送り先
            // 
            this.label送り先.AutoSize = true;
            this.label送り先.Location = new System.Drawing.Point(46, 173);
            this.label送り先.Name = "label送り先";
            this.label送り先.Size = new System.Drawing.Size(53, 12);
            this.label送り先.TabIndex = 42;
            this.label送り先.Text = "納品場所";
            // 
            // textBoxFaxNo
            // 
            this.textBoxFaxNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxFaxNo.Location = new System.Drawing.Point(309, 120);
            this.textBoxFaxNo.MaxLength = 25;
            this.textBoxFaxNo.Name = "textBoxFaxNo";
            this.textBoxFaxNo.Size = new System.Drawing.Size(120, 19);
            this.textBoxFaxNo.TabIndex = 38;
            // 
            // dateTimePicker希望納期
            // 
            this.dateTimePicker希望納期.Location = new System.Drawing.Point(106, 145);
            this.dateTimePicker希望納期.Name = "dateTimePicker希望納期";
            this.dateTimePicker希望納期.Size = new System.Drawing.Size(120, 19);
            this.dateTimePicker希望納期.TabIndex = 35;
            // 
            // label希望納期
            // 
            this.label希望納期.AutoSize = true;
            this.label希望納期.Location = new System.Drawing.Point(48, 148);
            this.label希望納期.Name = "label希望納期";
            this.label希望納期.Size = new System.Drawing.Size(53, 12);
            this.label希望納期.TabIndex = 36;
            this.label希望納期.Text = "希望納期";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(252, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 12);
            this.label6.TabIndex = 31;
            this.label6.Text = "FAX番号";
            // 
            // textBoxTelNo
            // 
            this.textBoxTelNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxTelNo.Location = new System.Drawing.Point(106, 120);
            this.textBoxTelNo.MaxLength = 25;
            this.textBoxTelNo.Name = "textBoxTelNo";
            this.textBoxTelNo.Size = new System.Drawing.Size(120, 19);
            this.textBoxTelNo.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 29;
            this.label5.Text = "電話番号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(14, 292);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "※";
            // 
            // textBox発注番号
            // 
            this.textBox発注番号.Location = new System.Drawing.Point(106, 43);
            this.textBox発注番号.Name = "textBox発注番号";
            this.textBox発注番号.ReadOnly = true;
            this.textBox発注番号.Size = new System.Drawing.Size(225, 19);
            this.textBox発注番号.TabIndex = 27;
            this.textBox発注番号.TabStop = false;
            // 
            // label発注番号
            // 
            this.label発注番号.AutoSize = true;
            this.label発注番号.Location = new System.Drawing.Point(50, 46);
            this.label発注番号.Name = "label発注番号";
            this.label発注番号.Size = new System.Drawing.Size(53, 12);
            this.label発注番号.TabIndex = 26;
            this.label発注番号.Text = "発注番号";
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button閉じる.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button閉じる.Location = new System.Drawing.Point(295, 370);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(100, 31);
            this.button閉じる.TabIndex = 25;
            this.button閉じる.Text = "キャンセル";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button送信
            // 
            this.button送信.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button送信.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button送信.Location = new System.Drawing.Point(171, 370);
            this.button送信.Name = "button送信";
            this.button送信.Size = new System.Drawing.Size(100, 31);
            this.button送信.TabIndex = 24;
            this.button送信.Text = "送信";
            this.button送信.UseVisualStyleBackColor = false;
            this.button送信.Click += new System.EventHandler(this.button送信_Click);
            // 
            // 発注メール送信Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(574, 430);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "発注メール送信Form";
            this.Text = "発注メール送信";
            this.Load += new System.EventHandler(this.発注メール送信Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label発注先;
        private System.Windows.Forms.Label labelメールアドレス;
        private System.Windows.Forms.Label labelメール件名;
        private System.Windows.Forms.TextBox textBoxメール件名;
        private System.Windows.Forms.Label label担当者;
        private System.Windows.Forms.TextBox textBoxメールアドレス;
        private System.Windows.Forms.TextBox textBox発注先;
        private System.Windows.Forms.ComboBox comboBox担当者;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button送信;
        private System.Windows.Forms.TextBox textBox発注番号;
        private System.Windows.Forms.Label label発注番号;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxTelNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker希望納期;
        private System.Windows.Forms.Label label希望納期;
        private System.Windows.Forms.TextBox textBoxFaxNo;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox納品場所;
        private System.Windows.Forms.Label label送り先;

    }
}