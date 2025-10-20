namespace Hachu.HachuManage
{
    partial class 注文書出力Form
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
            this.textBox発注先 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox納品場所 = new System.Windows.Forms.TextBox();
            this.textBoxFaxNo = new System.Windows.Forms.TextBox();
            this.label送り先 = new System.Windows.Forms.Label();
            this.dateTimePicker希望納期 = new System.Windows.Forms.DateTimePicker();
            this.label希望納期 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxTelNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox発注番号 = new System.Windows.Forms.TextBox();
            this.label発注番号 = new System.Windows.Forms.Label();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button出力 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label発注先
            // 
            this.label発注先.AutoSize = true;
            this.label発注先.Location = new System.Drawing.Point(58, 86);
            this.label発注先.Name = "label発注先";
            this.label発注先.Size = new System.Drawing.Size(41, 12);
            this.label発注先.TabIndex = 0;
            this.label発注先.Text = "発注先";
            // 
            // textBox発注先
            // 
            this.textBox発注先.Location = new System.Drawing.Point(106, 83);
            this.textBox発注先.Name = "textBox発注先";
            this.textBox発注先.ReadOnly = true;
            this.textBox発注先.Size = new System.Drawing.Size(225, 19);
            this.textBox発注先.TabIndex = 0;
            this.textBox発注先.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(46, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "注文書を出力します。";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox備考);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox納品場所);
            this.panel1.Controls.Add(this.textBoxFaxNo);
            this.panel1.Controls.Add(this.label送り先);
            this.panel1.Controls.Add(this.dateTimePicker希望納期);
            this.panel1.Controls.Add(this.label希望納期);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBoxTelNo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox発注番号);
            this.panel1.Controls.Add(this.label発注番号);
            this.panel1.Controls.Add(this.button閉じる);
            this.panel1.Controls.Add(this.button出力);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox発注先);
            this.panel1.Controls.Add(this.label発注先);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 347);
            this.panel1.TabIndex = 1;
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox備考.Location = new System.Drawing.Point(106, 218);
            this.textBox備考.MaxLength = 250;
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.Size = new System.Drawing.Size(306, 53);
            this.textBox備考.TabIndex = 41;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 40;
            this.label2.Text = "備考";
            // 
            // textBox納品場所
            // 
            this.textBox納品場所.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox納品場所.Location = new System.Drawing.Point(106, 159);
            this.textBox納品場所.MaxLength = 100;
            this.textBox納品場所.Multiline = true;
            this.textBox納品場所.Name = "textBox納品場所";
            this.textBox納品場所.Size = new System.Drawing.Size(306, 53);
            this.textBox納品場所.TabIndex = 39;
            // 
            // textBoxFaxNo
            // 
            this.textBoxFaxNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxFaxNo.Location = new System.Drawing.Point(292, 108);
            this.textBoxFaxNo.MaxLength = 25;
            this.textBoxFaxNo.Name = "textBoxFaxNo";
            this.textBoxFaxNo.Size = new System.Drawing.Size(120, 19);
            this.textBoxFaxNo.TabIndex = 38;
            // 
            // label送り先
            // 
            this.label送り先.AutoSize = true;
            this.label送り先.Location = new System.Drawing.Point(46, 162);
            this.label送り先.Name = "label送り先";
            this.label送り先.Size = new System.Drawing.Size(53, 12);
            this.label送り先.TabIndex = 37;
            this.label送り先.Text = "納品場所";
            // 
            // dateTimePicker希望納期
            // 
            this.dateTimePicker希望納期.Location = new System.Drawing.Point(106, 133);
            this.dateTimePicker希望納期.Name = "dateTimePicker希望納期";
            this.dateTimePicker希望納期.Size = new System.Drawing.Size(120, 19);
            this.dateTimePicker希望納期.TabIndex = 35;
            // 
            // label希望納期
            // 
            this.label希望納期.AutoSize = true;
            this.label希望納期.Location = new System.Drawing.Point(46, 138);
            this.label希望納期.Name = "label希望納期";
            this.label希望納期.Size = new System.Drawing.Size(53, 12);
            this.label希望納期.TabIndex = 36;
            this.label希望納期.Text = "希望納期";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(235, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 12);
            this.label6.TabIndex = 31;
            this.label6.Text = "FAX番号";
            // 
            // textBoxTelNo
            // 
            this.textBoxTelNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxTelNo.Location = new System.Drawing.Point(106, 108);
            this.textBoxTelNo.MaxLength = 25;
            this.textBoxTelNo.Name = "textBoxTelNo";
            this.textBoxTelNo.Size = new System.Drawing.Size(120, 19);
            this.textBoxTelNo.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 29;
            this.label5.Text = "電話番号";
            // 
            // textBox発注番号
            // 
            this.textBox発注番号.Location = new System.Drawing.Point(106, 58);
            this.textBox発注番号.Name = "textBox発注番号";
            this.textBox発注番号.ReadOnly = true;
            this.textBox発注番号.Size = new System.Drawing.Size(225, 19);
            this.textBox発注番号.TabIndex = 27;
            this.textBox発注番号.TabStop = false;
            // 
            // label発注番号
            // 
            this.label発注番号.AutoSize = true;
            this.label発注番号.Location = new System.Drawing.Point(46, 61);
            this.label発注番号.Name = "label発注番号";
            this.label発注番号.Size = new System.Drawing.Size(53, 12);
            this.label発注番号.TabIndex = 26;
            this.label発注番号.Text = "発注番号";
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button閉じる.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button閉じる.Location = new System.Drawing.Point(245, 295);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(100, 31);
            this.button閉じる.TabIndex = 25;
            this.button閉じる.Text = "キャンセル";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button出力
            // 
            this.button出力.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button出力.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button出力.Location = new System.Drawing.Point(128, 295);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(100, 31);
            this.button出力.TabIndex = 24;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = false;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // 注文書出力Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(472, 347);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "注文書出力Form";
            this.Text = "注文書出力";
            this.Load += new System.EventHandler(this.注文書出力Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label発注先;
        private System.Windows.Forms.TextBox textBox発注先;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.TextBox textBox発注番号;
        private System.Windows.Forms.Label label発注番号;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxTelNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker希望納期;
        private System.Windows.Forms.Label label希望納期;
        private System.Windows.Forms.TextBox textBox納品場所;
        private System.Windows.Forms.TextBox textBoxFaxNo;
        private System.Windows.Forms.Label label送り先;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.Label label2;

    }
}