namespace Senin
{
    partial class ORG乗船履歴詳細Form
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox種別 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox日数 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button更新 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkedListBox職名 = new System.Windows.Forms.CheckedListBox();
            this.nullableDateTimePicker開始 = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker終了 = new NBaseUtil.NullableDateTimePicker();
            this.checkBox転船 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "※開始日";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "終了日";
            // 
            // comboBox種別
            // 
            this.comboBox種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox種別.FormattingEnabled = true;
            this.comboBox種別.Location = new System.Drawing.Point(77, 63);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(162, 21);
            this.comboBox種別.TabIndex = 4;
            this.comboBox種別.SelectionChangeCommitted += new System.EventHandler(this.comboBox種別_SelectionChangeCommitted);
            this.comboBox種別.SelectedIndexChanged += new System.EventHandler(this.comboBox種別_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-1, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "※種別";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "日数";
            // 
            // textBox日数
            // 
            this.textBox日数.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox日数.Location = new System.Drawing.Point(77, 37);
            this.textBox日数.Name = "textBox日数";
            this.textBox日数.ReadOnly = true;
            this.textBox日数.Size = new System.Drawing.Size(39, 20);
            this.textBox日数.TabIndex = 3;
            this.textBox日数.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(122, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "日";
            // 
            // button更新
            // 
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button更新.Location = new System.Drawing.Point(123, 237);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 5;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button閉じる.Location = new System.Drawing.Point(286, 237);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 7;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button削除
            // 
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button削除.Location = new System.Drawing.Point(205, 237);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 6;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.Enabled = false;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(77, 90);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(162, 21);
            this.comboBox船.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "船";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "職名";
            // 
            // checkedListBox職名
            // 
            this.checkedListBox職名.CheckOnClick = true;
            this.checkedListBox職名.Enabled = false;
            this.checkedListBox職名.FormattingEnabled = true;
            this.checkedListBox職名.Location = new System.Drawing.Point(77, 117);
            this.checkedListBox職名.Name = "checkedListBox職名";
            this.checkedListBox職名.Size = new System.Drawing.Size(162, 94);
            this.checkedListBox職名.TabIndex = 8;
            // 
            // nullableDateTimePicker開始
            // 
            this.nullableDateTimePicker開始.Location = new System.Drawing.Point(77, 11);
            this.nullableDateTimePicker開始.Name = "nullableDateTimePicker開始";
            this.nullableDateTimePicker開始.NullValue = "";
            this.nullableDateTimePicker開始.Size = new System.Drawing.Size(132, 20);
            this.nullableDateTimePicker開始.TabIndex = 9;
            this.nullableDateTimePicker開始.Value = null;
            this.nullableDateTimePicker開始.ValueChanged += new System.EventHandler(this.nullableDateTimePicker開始_ValueChanged);
            // 
            // nullableDateTimePicker終了
            // 
            this.nullableDateTimePicker終了.Location = new System.Drawing.Point(303, 11);
            this.nullableDateTimePicker終了.Name = "nullableDateTimePicker終了";
            this.nullableDateTimePicker終了.NullValue = "";
            this.nullableDateTimePicker終了.Size = new System.Drawing.Size(132, 20);
            this.nullableDateTimePicker終了.TabIndex = 9;
            this.nullableDateTimePicker終了.Value = null;
            this.nullableDateTimePicker終了.ValueChanged += new System.EventHandler(this.nullableDateTimePicker終了_ValueChanged);
            // 
            // checkBox転船
            // 
            this.checkBox転船.AutoSize = true;
            this.checkBox転船.Location = new System.Drawing.Point(246, 67);
            this.checkBox転船.Name = "checkBox転船";
            this.checkBox転船.Size = new System.Drawing.Size(215, 17);
            this.checkBox転船.TabIndex = 10;
            this.checkBox転船.Text = "同日転船の場合、チェックしてください";
            this.checkBox転船.UseVisualStyleBackColor = true;
            this.checkBox転船.CheckedChanged += new System.EventHandler(this.checkBox転船_CheckedChanged);
            // 
            // 乗船履歴詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(485, 272);
            this.Controls.Add(this.checkBox転船);
            this.Controls.Add(this.nullableDateTimePicker終了);
            this.Controls.Add(this.nullableDateTimePicker開始);
            this.Controls.Add(this.checkedListBox職名);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.textBox日数);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox船);
            this.Controls.Add(this.comboBox種別);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "乗船履歴詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "乗船履歴詳細Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox種別;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox日数;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox checkedListBox職名;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker開始;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker終了;
        private System.Windows.Forms.CheckBox checkBox転船;
    }
}