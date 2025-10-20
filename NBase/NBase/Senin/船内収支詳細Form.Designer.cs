namespace Senin
{
    partial class 船内収支詳細Form
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
            this.dateTimePicker日付 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox金額 = new System.Windows.Forms.TextBox();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.button更新 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox明細 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox消費税 = new System.Windows.Forms.TextBox();
            this.checkBox振替 = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox大項目 = new System.Windows.Forms.TextBox();
            this.textBox費用科目 = new System.Windows.Forms.TextBox();
            this.comboBox勘定科目 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox金額税抜 = new System.Windows.Forms.TextBox();
            this.button内税計算 = new System.Windows.Forms.Button();
            this.panel_内税計算 = new System.Windows.Forms.Panel();
            this.panel_軽減税率対応 = new System.Windows.Forms.Panel();
            this.button_内税8 = new System.Windows.Forms.Button();
            this.button_内税10 = new System.Windows.Forms.Button();
            this.textBox_taxId = new System.Windows.Forms.TextBox();
            this.panel_内税計算.SuspendLayout();
            this.panel_軽減税率対応.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "日付";
            // 
            // dateTimePicker日付
            // 
            this.dateTimePicker日付.Location = new System.Drawing.Point(79, 16);
            this.dateTimePicker日付.Name = "dateTimePicker日付";
            this.dateTimePicker日付.Size = new System.Drawing.Size(127, 19);
            this.dateTimePicker日付.TabIndex = 1;
            this.dateTimePicker日付.Value = new System.DateTime(2009, 11, 17, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "勘定科目";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "金額";
            // 
            // textBox金額
            // 
            this.textBox金額.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox金額.Location = new System.Drawing.Point(79, 141);
            this.textBox金額.MaxLength = 9;
            this.textBox金額.Name = "textBox金額";
            this.textBox金額.ReadOnly = true;
            this.textBox金額.Size = new System.Drawing.Size(127, 19);
            this.textBox金額.TabIndex = 5;
            this.textBox金額.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox備考.Location = new System.Drawing.Point(79, 212);
            this.textBox備考.MaxLength = 500;
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.Size = new System.Drawing.Size(276, 67);
            this.textBox備考.TabIndex = 7;
            // 
            // button更新
            // 
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(79, 318);
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
            this.button閉じる.Location = new System.Drawing.Point(241, 318);
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
            this.button削除.Location = new System.Drawing.Point(160, 318);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 10;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "費用科目";
            // 
            // comboBox明細
            // 
            this.comboBox明細.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox明細.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox明細.FormattingEnabled = true;
            this.comboBox明細.Location = new System.Drawing.Point(79, 40);
            this.comboBox明細.MaxDropDownItems = 25;
            this.comboBox明細.Name = "comboBox明細";
            this.comboBox明細.Size = new System.Drawing.Size(250, 20);
            this.comboBox明細.TabIndex = 4;
            this.comboBox明細.TextUpdate += new System.EventHandler(this.comboBox明細_TextUpdate);
            this.comboBox明細.SelectedValueChanged += new System.EventHandler(this.comboBox明細_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "大項目";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "明細";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "消費税";
            // 
            // textBox消費税
            // 
            this.textBox消費税.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox消費税.Location = new System.Drawing.Point(79, 189);
            this.textBox消費税.MaxLength = 9;
            this.textBox消費税.Name = "textBox消費税";
            this.textBox消費税.Size = new System.Drawing.Size(127, 19);
            this.textBox消費税.TabIndex = 6;
            this.textBox消費税.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox消費税.TextChanged += new System.EventHandler(this.textBox消費税_TextChanged);
            // 
            // checkBox振替
            // 
            this.checkBox振替.AutoSize = true;
            this.checkBox振替.Location = new System.Drawing.Point(79, 285);
            this.checkBox振替.Name = "checkBox振替";
            this.checkBox振替.Size = new System.Drawing.Size(15, 14);
            this.checkBox振替.TabIndex = 8;
            this.checkBox振替.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "振替";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 215);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "備考";
            // 
            // textBox大項目
            // 
            this.textBox大項目.Location = new System.Drawing.Point(79, 66);
            this.textBox大項目.Name = "textBox大項目";
            this.textBox大項目.ReadOnly = true;
            this.textBox大項目.Size = new System.Drawing.Size(250, 19);
            this.textBox大項目.TabIndex = 18;
            // 
            // textBox費用科目
            // 
            this.textBox費用科目.Location = new System.Drawing.Point(79, 91);
            this.textBox費用科目.Name = "textBox費用科目";
            this.textBox費用科目.ReadOnly = true;
            this.textBox費用科目.Size = new System.Drawing.Size(250, 19);
            this.textBox費用科目.TabIndex = 18;
            // 
            // comboBox勘定科目
            // 
            this.comboBox勘定科目.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox勘定科目.FormattingEnabled = true;
            this.comboBox勘定科目.Location = new System.Drawing.Point(79, 116);
            this.comboBox勘定科目.MaxDropDownItems = 25;
            this.comboBox勘定科目.Name = "comboBox勘定科目";
            this.comboBox勘定科目.Size = new System.Drawing.Size(250, 20);
            this.comboBox勘定科目.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 168);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "金額（税抜）";
            // 
            // textBox金額税抜
            // 
            this.textBox金額税抜.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox金額税抜.Location = new System.Drawing.Point(79, 165);
            this.textBox金額税抜.MaxLength = 9;
            this.textBox金額税抜.Name = "textBox金額税抜";
            this.textBox金額税抜.Size = new System.Drawing.Size(127, 19);
            this.textBox金額税抜.TabIndex = 5;
            this.textBox金額税抜.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox金額税抜.TextChanged += new System.EventHandler(this.textBox金額税抜_TextChanged);
            // 
            // button内税計算
            // 
            this.button内税計算.BackColor = System.Drawing.SystemColors.Control;
            this.button内税計算.Location = new System.Drawing.Point(0, 0);
            this.button内税計算.Name = "button内税計算";
            this.button内税計算.Size = new System.Drawing.Size(75, 23);
            this.button内税計算.TabIndex = 20;
            this.button内税計算.Text = "内税計算";
            this.button内税計算.UseVisualStyleBackColor = false;
            this.button内税計算.Click += new System.EventHandler(this.button内税計算_Click);
            // 
            // panel_内税計算
            // 
            this.panel_内税計算.Controls.Add(this.button内税計算);
            this.panel_内税計算.Location = new System.Drawing.Point(212, 165);
            this.panel_内税計算.Name = "panel_内税計算";
            this.panel_内税計算.Size = new System.Drawing.Size(91, 29);
            this.panel_内税計算.TabIndex = 21;
            // 
            // panel_軽減税率対応
            // 
            this.panel_軽減税率対応.Controls.Add(this.button_内税10);
            this.panel_軽減税率対応.Controls.Add(this.button_内税8);
            this.panel_軽減税率対応.Location = new System.Drawing.Point(212, 165);
            this.panel_軽減税率対応.Name = "panel_軽減税率対応";
            this.panel_軽減税率対応.Size = new System.Drawing.Size(160, 29);
            this.panel_軽減税率対応.TabIndex = 21;
            // 
            // button_内税8
            // 
            this.button_内税8.BackColor = System.Drawing.SystemColors.Control;
            this.button_内税8.Location = new System.Drawing.Point(0, 0);
            this.button_内税8.Name = "button_内税8";
            this.button_内税8.Size = new System.Drawing.Size(75, 23);
            this.button_内税8.TabIndex = 20;
            this.button_内税8.Text = "内税8%";
            this.button_内税8.UseVisualStyleBackColor = false;
            this.button_内税8.Click += new System.EventHandler(this.button_内税8_Click);
            // 
            // button_内税10
            // 
            this.button_内税10.BackColor = System.Drawing.SystemColors.Control;
            this.button_内税10.Location = new System.Drawing.Point(81, 0);
            this.button_内税10.Name = "button_内税10";
            this.button_内税10.Size = new System.Drawing.Size(75, 23);
            this.button_内税10.TabIndex = 20;
            this.button_内税10.Text = "内税10%";
            this.button_内税10.UseVisualStyleBackColor = false;
            this.button_内税10.Click += new System.EventHandler(this.button_内税10_Click);
            // 
            // textBox_taxId
            // 
            this.textBox_taxId.BackColor = System.Drawing.Color.DarkGray;
            this.textBox_taxId.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_taxId.Location = new System.Drawing.Point(212, 141);
            this.textBox_taxId.MaxLength = 9;
            this.textBox_taxId.Name = "textBox_taxId";
            this.textBox_taxId.Size = new System.Drawing.Size(45, 19);
            this.textBox_taxId.TabIndex = 5;
            this.textBox_taxId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_taxId.Visible = false;
            this.textBox_taxId.TextChanged += new System.EventHandler(this.textBox金額税抜_TextChanged);
            // 
            // 船内収支詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(396, 353);
            this.Controls.Add(this.panel_内税計算);
            this.Controls.Add(this.comboBox勘定科目);
            this.Controls.Add(this.checkBox振替);
            this.Controls.Add(this.textBox費用科目);
            this.Controls.Add(this.textBox大項目);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox明細);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.textBox備考);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox消費税);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_taxId);
            this.Controls.Add(this.textBox金額税抜);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox金額);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker日付);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel_軽減税率対応);
            this.Name = "船内収支詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "船内収支詳細";
            this.panel_内税計算.ResumeLayout(false);
            this.panel_軽減税率対応.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker日付;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox金額;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox明細;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox消費税;
        private System.Windows.Forms.CheckBox checkBox振替;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox大項目;
        private System.Windows.Forms.TextBox textBox費用科目;
        private System.Windows.Forms.ComboBox comboBox勘定科目;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox金額税抜;
        private System.Windows.Forms.Button button内税計算;
        private System.Windows.Forms.Panel panel_内税計算;
        private System.Windows.Forms.Panel panel_軽減税率対応;
        private System.Windows.Forms.Button button_内税10;
        private System.Windows.Forms.Button button_内税8;
        private System.Windows.Forms.TextBox textBox_taxId;
    }
}