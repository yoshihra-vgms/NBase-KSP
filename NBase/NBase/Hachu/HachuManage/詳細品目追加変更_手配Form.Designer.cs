namespace Hachu.HachuManage
{
    partial class 詳細品目追加変更_手配Form
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
            this.label詳細品目 = new System.Windows.Forms.Label();
            this.comboBox詳細品目 = new System.Windows.Forms.ComboBox();
            this.label依頼数 = new System.Windows.Forms.Label();
            this.label備考 = new System.Windows.Forms.Label();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.button登録 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.label単位 = new System.Windows.Forms.Label();
            this.comboBox単位 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel品目入力 = new System.Windows.Forms.Panel();
            this.panel品目選択 = new System.Windows.Forms.Panel();
            this.button選択 = new System.Windows.Forms.Button();
            this.textBox詳細品目 = new System.Windows.Forms.TextBox();
            this.textBox依頼数 = new System.Windows.Forms.TextBox();
            this.textBox在庫数 = new System.Windows.Forms.TextBox();
            this.panel品目入力.SuspendLayout();
            this.panel品目選択.SuspendLayout();
            this.SuspendLayout();
            // 
            // label詳細品目
            // 
            this.label詳細品目.AutoSize = true;
            this.label詳細品目.Location = new System.Drawing.Point(15, 20);
            this.label詳細品目.Name = "label詳細品目";
            this.label詳細品目.Size = new System.Drawing.Size(53, 12);
            this.label詳細品目.TabIndex = 0;
            this.label詳細品目.Text = "詳細品目";
            // 
            // comboBox詳細品目
            // 
            this.comboBox詳細品目.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox詳細品目.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBox詳細品目.FormattingEnabled = true;
            this.comboBox詳細品目.Location = new System.Drawing.Point(1, 1);
            this.comboBox詳細品目.MaxLength = 50;
            this.comboBox詳細品目.Name = "comboBox詳細品目";
            this.comboBox詳細品目.Size = new System.Drawing.Size(250, 20);
            this.comboBox詳細品目.TabIndex = 1;
            // 
            // label依頼数
            // 
            this.label依頼数.AutoSize = true;
            this.label依頼数.Location = new System.Drawing.Point(162, 72);
            this.label依頼数.Name = "label依頼数";
            this.label依頼数.Size = new System.Drawing.Size(41, 12);
            this.label依頼数.TabIndex = 0;
            this.label依頼数.Text = "依頼数";
            // 
            // label備考
            // 
            this.label備考.AutoSize = true;
            this.label備考.Location = new System.Drawing.Point(39, 98);
            this.label備考.Name = "label備考";
            this.label備考.Size = new System.Drawing.Size(29, 12);
            this.label備考.TabIndex = 0;
            this.label備考.Text = "備考";
            // 
            // textBox備考
            // 
            this.textBox備考.Location = new System.Drawing.Point(74, 95);
            this.textBox備考.MaxLength = 50;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.Size = new System.Drawing.Size(423, 19);
            this.textBox備考.TabIndex = 6;
            // 
            // button登録
            // 
            this.button登録.BackColor = System.Drawing.SystemColors.Control;
            this.button登録.Location = new System.Drawing.Point(142, 135);
            this.button登録.Name = "button登録";
            this.button登録.Size = new System.Drawing.Size(75, 23);
            this.button登録.TabIndex = 10;
            this.button登録.Text = "登録";
            this.button登録.UseVisualStyleBackColor = false;
            this.button登録.Click += new System.EventHandler(this.button登録_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(304, 135);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 12;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button削除
            // 
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(223, 135);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 11;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // label単位
            // 
            this.label単位.AutoSize = true;
            this.label単位.Location = new System.Drawing.Point(39, 46);
            this.label単位.Name = "label単位";
            this.label単位.Size = new System.Drawing.Size(29, 12);
            this.label単位.TabIndex = 0;
            this.label単位.Text = "単位";
            // 
            // comboBox単位
            // 
            this.comboBox単位.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox単位.FormattingEnabled = true;
            this.comboBox単位.Location = new System.Drawing.Point(74, 43);
            this.comboBox単位.Name = "comboBox単位";
            this.comboBox単位.Size = new System.Drawing.Size(77, 20);
            this.comboBox単位.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "在庫数";
            // 
            // panel品目入力
            // 
            this.panel品目入力.Controls.Add(this.comboBox詳細品目);
            this.panel品目入力.Location = new System.Drawing.Point(74, 17);
            this.panel品目入力.Name = "panel品目入力";
            this.panel品目入力.Size = new System.Drawing.Size(252, 22);
            this.panel品目入力.TabIndex = 0;
            // 
            // panel品目選択
            // 
            this.panel品目選択.Controls.Add(this.button選択);
            this.panel品目選択.Controls.Add(this.textBox詳細品目);
            this.panel品目選択.Location = new System.Drawing.Point(332, 18);
            this.panel品目選択.Name = "panel品目選択";
            this.panel品目選択.Size = new System.Drawing.Size(333, 22);
            this.panel品目選択.TabIndex = 0;
            // 
            // button選択
            // 
            this.button選択.BackColor = System.Drawing.SystemColors.Control;
            this.button選択.Location = new System.Drawing.Point(258, 0);
            this.button選択.Name = "button選択";
            this.button選択.Size = new System.Drawing.Size(75, 23);
            this.button選択.TabIndex = 1;
            this.button選択.Text = "選択";
            this.button選択.UseVisualStyleBackColor = false;
            this.button選択.Click += new System.EventHandler(this.button選択_Click);
            // 
            // textBox詳細品目
            // 
            this.textBox詳細品目.Location = new System.Drawing.Point(1, 1);
            this.textBox詳細品目.Name = "textBox詳細品目";
            this.textBox詳細品目.ReadOnly = true;
            this.textBox詳細品目.Size = new System.Drawing.Size(250, 19);
            this.textBox詳細品目.TabIndex = 0;
            this.textBox詳細品目.TabStop = false;
            // 
            // textBox依頼数
            // 
            this.textBox依頼数.Location = new System.Drawing.Point(209, 69);
            this.textBox依頼数.MaxLength = 5;
            this.textBox依頼数.Name = "textBox依頼数";
            this.textBox依頼数.Size = new System.Drawing.Size(50, 19);
            this.textBox依頼数.TabIndex = 5;
            // 
            // textBox在庫数
            // 
            this.textBox在庫数.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox在庫数.Location = new System.Drawing.Point(75, 69);
            this.textBox在庫数.MaxLength = 5;
            this.textBox在庫数.Name = "textBox在庫数";
            this.textBox在庫数.Size = new System.Drawing.Size(50, 19);
            this.textBox在庫数.TabIndex = 4;
            // 
            // 詳細品目追加変更_手配Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(521, 176);
            this.Controls.Add(this.textBox在庫数);
            this.Controls.Add(this.textBox依頼数);
            this.Controls.Add(this.panel品目選択);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.panel品目入力);
            this.Controls.Add(this.button登録);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox単位);
            this.Controls.Add(this.label単位);
            this.Controls.Add(this.textBox備考);
            this.Controls.Add(this.label備考);
            this.Controls.Add(this.label依頼数);
            this.Controls.Add(this.label詳細品目);
            this.Name = "詳細品目追加変更_手配Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "手配品目詳細";
            this.Load += new System.EventHandler(this.詳細品目追加変更_手配Form_Load);
            this.panel品目入力.ResumeLayout(false);
            this.panel品目選択.ResumeLayout(false);
            this.panel品目選択.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label詳細品目;
        private System.Windows.Forms.ComboBox comboBox詳細品目;
        private System.Windows.Forms.Label label依頼数;
        private System.Windows.Forms.Label label備考;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.Button button登録;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Label label単位;
        private System.Windows.Forms.ComboBox comboBox単位;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel品目入力;
        private System.Windows.Forms.Panel panel品目選択;
        private System.Windows.Forms.Button button選択;
        private System.Windows.Forms.TextBox textBox詳細品目;
        private System.Windows.Forms.TextBox textBox依頼数;
        private System.Windows.Forms.TextBox textBox在庫数;
    }
}