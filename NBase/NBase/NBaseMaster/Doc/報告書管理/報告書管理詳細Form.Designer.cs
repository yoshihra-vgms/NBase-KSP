namespace NBaseMaster.Doc.報告書管理
{
    partial class 報告書管理詳細Form
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
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_BunshoName = new System.Windows.Forms.TextBox();
            this.comboBox_Shoubunrui = new System.Windows.Forms.ComboBox();
            this.comboBox_Bunrui = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_BunshoNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_更新 = new System.Windows.Forms.Button();
            this.button_閉じる = new System.Windows.Forms.Button();
            this.button_削除 = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox_Shuki = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.checkBox_Jiki1 = new System.Windows.Forms.CheckBox();
            this.checkBox_Jiki2 = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBox_Jiki3 = new System.Windows.Forms.CheckBox();
            this.checkBox_Jiki4 = new System.Windows.Forms.CheckBox();
            this.checkBox_Jiki5 = new System.Windows.Forms.CheckBox();
            this.checkBox_Jiki6 = new System.Windows.Forms.CheckBox();
            this.checkBox_Jiki7 = new System.Windows.Forms.CheckBox();
            this.checkBox_Jiki8 = new System.Windows.Forms.CheckBox();
            this.checkBox_Jiki9 = new System.Windows.Forms.CheckBox();
            this.checkBox_Jiki10 = new System.Windows.Forms.CheckBox();
            this.checkBox_Jiki11 = new System.Windows.Forms.CheckBox();
            this.checkBox_Jiki12 = new System.Windows.Forms.CheckBox();
            this.checkBox_CheckTarget = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox_TemplateFileName = new System.Windows.Forms.TextBox();
            this.button_参照 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.documentGroupCheckBox2 = new NBaseCommon.DocumentGroupCheckBox();
            this.documentGroupCheckBox1 = new NBaseCommon.DocumentGroupCheckBox();
            this.button_クリア = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "※文書名";
            // 
            // textBox_BunshoName
            // 
            this.textBox_BunshoName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_BunshoName.Location = new System.Drawing.Point(131, 107);
            this.textBox_BunshoName.MaxLength = 50;
            this.textBox_BunshoName.Name = "textBox_BunshoName";
            this.textBox_BunshoName.Size = new System.Drawing.Size(346, 19);
            this.textBox_BunshoName.TabIndex = 4;
            // 
            // comboBox_Shoubunrui
            // 
            this.comboBox_Shoubunrui.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Shoubunrui.FormattingEnabled = true;
            this.comboBox_Shoubunrui.Location = new System.Drawing.Point(131, 58);
            this.comboBox_Shoubunrui.Name = "comboBox_Shoubunrui";
            this.comboBox_Shoubunrui.Size = new System.Drawing.Size(179, 20);
            this.comboBox_Shoubunrui.TabIndex = 2;
            // 
            // comboBox_Bunrui
            // 
            this.comboBox_Bunrui.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Bunrui.FormattingEnabled = true;
            this.comboBox_Bunrui.Location = new System.Drawing.Point(131, 32);
            this.comboBox_Bunrui.Name = "comboBox_Bunrui";
            this.comboBox_Bunrui.Size = new System.Drawing.Size(179, 20);
            this.comboBox_Bunrui.TabIndex = 1;
            this.comboBox_Bunrui.SelectedIndexChanged += new System.EventHandler(this.comboBox_Bunrui_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "※文書番号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "ﾄﾞｷｭﾒﾝﾄ小分類名";
            // 
            // textBox_BunshoNo
            // 
            this.textBox_BunshoNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_BunshoNo.Location = new System.Drawing.Point(131, 84);
            this.textBox_BunshoNo.MaxLength = 15;
            this.textBox_BunshoNo.Name = "textBox_BunshoNo";
            this.textBox_BunshoNo.Size = new System.Drawing.Size(179, 19);
            this.textBox_BunshoNo.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "※ﾄﾞｷｭﾒﾝﾄ分類名";
            // 
            // button_更新
            // 
            this.button_更新.Location = new System.Drawing.Point(319, 558);
            this.button_更新.Name = "button_更新";
            this.button_更新.Size = new System.Drawing.Size(75, 23);
            this.button_更新.TabIndex = 2;
            this.button_更新.Text = "更新";
            this.button_更新.UseVisualStyleBackColor = true;
            this.button_更新.Click += new System.EventHandler(this.button_更新_Click);
            // 
            // button_閉じる
            // 
            this.button_閉じる.Location = new System.Drawing.Point(513, 558);
            this.button_閉じる.Name = "button_閉じる";
            this.button_閉じる.Size = new System.Drawing.Size(75, 23);
            this.button_閉じる.TabIndex = 3;
            this.button_閉じる.Text = "閉じる";
            this.button_閉じる.UseVisualStyleBackColor = true;
            this.button_閉じる.Click += new System.EventHandler(this.button_閉じる_Click);
            // 
            // button_削除
            // 
            this.button_削除.Location = new System.Drawing.Point(419, 558);
            this.button_削除.Name = "button_削除";
            this.button_削除.Size = new System.Drawing.Size(75, 23);
            this.button_削除.TabIndex = 10;
            this.button_削除.Text = "削除";
            this.button_削除.UseVisualStyleBackColor = true;
            this.button_削除.Click += new System.EventHandler(this.button_削除_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(19, 479);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 16;
            this.label18.Text = "提出周期";
            // 
            // textBox_Shuki
            // 
            this.textBox_Shuki.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_Shuki.Location = new System.Drawing.Point(88, 476);
            this.textBox_Shuki.MaxLength = 50;
            this.textBox_Shuki.Name = "textBox_Shuki";
            this.textBox_Shuki.Size = new System.Drawing.Size(179, 19);
            this.textBox_Shuki.TabIndex = 15;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(19, 505);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 17;
            this.label19.Text = "提出時期";
            // 
            // checkBox_Jiki1
            // 
            this.checkBox_Jiki1.AutoSize = true;
            this.checkBox_Jiki1.Location = new System.Drawing.Point(3, 3);
            this.checkBox_Jiki1.Name = "checkBox_Jiki1";
            this.checkBox_Jiki1.Size = new System.Drawing.Size(54, 16);
            this.checkBox_Jiki1.TabIndex = 18;
            this.checkBox_Jiki1.Text = "4月度";
            this.checkBox_Jiki1.UseVisualStyleBackColor = true;
            // 
            // checkBox_Jiki2
            // 
            this.checkBox_Jiki2.AutoSize = true;
            this.checkBox_Jiki2.Location = new System.Drawing.Point(63, 3);
            this.checkBox_Jiki2.Name = "checkBox_Jiki2";
            this.checkBox_Jiki2.Size = new System.Drawing.Size(54, 16);
            this.checkBox_Jiki2.TabIndex = 19;
            this.checkBox_Jiki2.Text = "5月度";
            this.checkBox_Jiki2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.checkBox_Jiki1);
            this.flowLayoutPanel1.Controls.Add(this.checkBox_Jiki2);
            this.flowLayoutPanel1.Controls.Add(this.checkBox_Jiki3);
            this.flowLayoutPanel1.Controls.Add(this.checkBox_Jiki4);
            this.flowLayoutPanel1.Controls.Add(this.checkBox_Jiki5);
            this.flowLayoutPanel1.Controls.Add(this.checkBox_Jiki6);
            this.flowLayoutPanel1.Controls.Add(this.checkBox_Jiki7);
            this.flowLayoutPanel1.Controls.Add(this.checkBox_Jiki8);
            this.flowLayoutPanel1.Controls.Add(this.checkBox_Jiki9);
            this.flowLayoutPanel1.Controls.Add(this.checkBox_Jiki10);
            this.flowLayoutPanel1.Controls.Add(this.checkBox_Jiki11);
            this.flowLayoutPanel1.Controls.Add(this.checkBox_Jiki12);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(88, 501);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(738, 22);
            this.flowLayoutPanel1.TabIndex = 20;
            // 
            // checkBox_Jiki3
            // 
            this.checkBox_Jiki3.AutoSize = true;
            this.checkBox_Jiki3.Location = new System.Drawing.Point(123, 3);
            this.checkBox_Jiki3.Name = "checkBox_Jiki3";
            this.checkBox_Jiki3.Size = new System.Drawing.Size(54, 16);
            this.checkBox_Jiki3.TabIndex = 20;
            this.checkBox_Jiki3.Text = "6月度";
            this.checkBox_Jiki3.UseVisualStyleBackColor = true;
            // 
            // checkBox_Jiki4
            // 
            this.checkBox_Jiki4.AutoSize = true;
            this.checkBox_Jiki4.Location = new System.Drawing.Point(183, 3);
            this.checkBox_Jiki4.Name = "checkBox_Jiki4";
            this.checkBox_Jiki4.Size = new System.Drawing.Size(54, 16);
            this.checkBox_Jiki4.TabIndex = 21;
            this.checkBox_Jiki4.Text = "7月度";
            this.checkBox_Jiki4.UseVisualStyleBackColor = true;
            // 
            // checkBox_Jiki5
            // 
            this.checkBox_Jiki5.AutoSize = true;
            this.checkBox_Jiki5.Location = new System.Drawing.Point(243, 3);
            this.checkBox_Jiki5.Name = "checkBox_Jiki5";
            this.checkBox_Jiki5.Size = new System.Drawing.Size(54, 16);
            this.checkBox_Jiki5.TabIndex = 22;
            this.checkBox_Jiki5.Text = "8月度";
            this.checkBox_Jiki5.UseVisualStyleBackColor = true;
            // 
            // checkBox_Jiki6
            // 
            this.checkBox_Jiki6.AutoSize = true;
            this.checkBox_Jiki6.Location = new System.Drawing.Point(303, 3);
            this.checkBox_Jiki6.Name = "checkBox_Jiki6";
            this.checkBox_Jiki6.Size = new System.Drawing.Size(54, 16);
            this.checkBox_Jiki6.TabIndex = 23;
            this.checkBox_Jiki6.Text = "9月度";
            this.checkBox_Jiki6.UseVisualStyleBackColor = true;
            // 
            // checkBox_Jiki7
            // 
            this.checkBox_Jiki7.AutoSize = true;
            this.checkBox_Jiki7.Location = new System.Drawing.Point(363, 3);
            this.checkBox_Jiki7.Name = "checkBox_Jiki7";
            this.checkBox_Jiki7.Size = new System.Drawing.Size(60, 16);
            this.checkBox_Jiki7.TabIndex = 24;
            this.checkBox_Jiki7.Text = "10月度";
            this.checkBox_Jiki7.UseVisualStyleBackColor = true;
            // 
            // checkBox_Jiki8
            // 
            this.checkBox_Jiki8.AutoSize = true;
            this.checkBox_Jiki8.Location = new System.Drawing.Point(429, 3);
            this.checkBox_Jiki8.Name = "checkBox_Jiki8";
            this.checkBox_Jiki8.Size = new System.Drawing.Size(60, 16);
            this.checkBox_Jiki8.TabIndex = 25;
            this.checkBox_Jiki8.Text = "11月度";
            this.checkBox_Jiki8.UseVisualStyleBackColor = true;
            // 
            // checkBox_Jiki9
            // 
            this.checkBox_Jiki9.AutoSize = true;
            this.checkBox_Jiki9.Location = new System.Drawing.Point(495, 3);
            this.checkBox_Jiki9.Name = "checkBox_Jiki9";
            this.checkBox_Jiki9.Size = new System.Drawing.Size(60, 16);
            this.checkBox_Jiki9.TabIndex = 26;
            this.checkBox_Jiki9.Text = "12月度";
            this.checkBox_Jiki9.UseVisualStyleBackColor = true;
            // 
            // checkBox_Jiki10
            // 
            this.checkBox_Jiki10.AutoSize = true;
            this.checkBox_Jiki10.Location = new System.Drawing.Point(561, 3);
            this.checkBox_Jiki10.Name = "checkBox_Jiki10";
            this.checkBox_Jiki10.Size = new System.Drawing.Size(54, 16);
            this.checkBox_Jiki10.TabIndex = 27;
            this.checkBox_Jiki10.Text = "1月度";
            this.checkBox_Jiki10.UseVisualStyleBackColor = true;
            // 
            // checkBox_Jiki11
            // 
            this.checkBox_Jiki11.AutoSize = true;
            this.checkBox_Jiki11.Location = new System.Drawing.Point(621, 3);
            this.checkBox_Jiki11.Name = "checkBox_Jiki11";
            this.checkBox_Jiki11.Size = new System.Drawing.Size(54, 16);
            this.checkBox_Jiki11.TabIndex = 28;
            this.checkBox_Jiki11.Text = "2月度";
            this.checkBox_Jiki11.UseVisualStyleBackColor = true;
            // 
            // checkBox_Jiki12
            // 
            this.checkBox_Jiki12.AutoSize = true;
            this.checkBox_Jiki12.Location = new System.Drawing.Point(681, 3);
            this.checkBox_Jiki12.Name = "checkBox_Jiki12";
            this.checkBox_Jiki12.Size = new System.Drawing.Size(54, 16);
            this.checkBox_Jiki12.TabIndex = 29;
            this.checkBox_Jiki12.Text = "3月度";
            this.checkBox_Jiki12.UseVisualStyleBackColor = true;
            // 
            // checkBox_CheckTarget
            // 
            this.checkBox_CheckTarget.AutoSize = true;
            this.checkBox_CheckTarget.Location = new System.Drawing.Point(591, 108);
            this.checkBox_CheckTarget.Name = "checkBox_CheckTarget";
            this.checkBox_CheckTarget.Size = new System.Drawing.Size(15, 14);
            this.checkBox_CheckTarget.TabIndex = 5;
            this.checkBox_CheckTarget.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(492, 110);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(96, 12);
            this.label20.TabIndex = 7;
            this.label20.Text = "未提出チェック対象";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(33, 135);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(63, 12);
            this.label21.TabIndex = 23;
            this.label21.Text = "雛形ファイル";
            // 
            // textBox_TemplateFileName
            // 
            this.textBox_TemplateFileName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_TemplateFileName.Location = new System.Drawing.Point(131, 132);
            this.textBox_TemplateFileName.Name = "textBox_TemplateFileName";
            this.textBox_TemplateFileName.ReadOnly = true;
            this.textBox_TemplateFileName.Size = new System.Drawing.Size(346, 19);
            this.textBox_TemplateFileName.TabIndex = 24;
            // 
            // button_参照
            // 
            this.button_参照.BackColor = System.Drawing.Color.White;
            this.button_参照.Location = new System.Drawing.Point(483, 130);
            this.button_参照.Name = "button_参照";
            this.button_参照.Size = new System.Drawing.Size(75, 23);
            this.button_参照.TabIndex = 6;
            this.button_参照.Text = "参照";
            this.button_参照.UseVisualStyleBackColor = false;
            this.button_参照.Click += new System.EventHandler(this.button_参照_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // documentGroupCheckBox2
            // 
            this.documentGroupCheckBox2.BackColor = System.Drawing.Color.White;
            this.documentGroupCheckBox2.Location = new System.Drawing.Point(21, 316);
            this.documentGroupCheckBox2.Name = "documentGroupCheckBox2";
            this.documentGroupCheckBox2.Size = new System.Drawing.Size(851, 150);
            this.documentGroupCheckBox2.TabIndex = 25;
            // 
            // documentGroupCheckBox1
            // 
            this.documentGroupCheckBox1.BackColor = System.Drawing.Color.White;
            this.documentGroupCheckBox1.Location = new System.Drawing.Point(20, 159);
            this.documentGroupCheckBox1.Name = "documentGroupCheckBox1";
            this.documentGroupCheckBox1.Size = new System.Drawing.Size(852, 150);
            this.documentGroupCheckBox1.TabIndex = 23;
            // 
            // button_クリア
            // 
            this.button_クリア.BackColor = System.Drawing.Color.White;
            this.button_クリア.Location = new System.Drawing.Point(564, 130);
            this.button_クリア.Name = "button_クリア";
            this.button_クリア.Size = new System.Drawing.Size(75, 23);
            this.button_クリア.TabIndex = 26;
            this.button_クリア.Text = "クリア";
            this.button_クリア.UseVisualStyleBackColor = false;
            this.button_クリア.Click += new System.EventHandler(this.button_クリア_Click);
            // 
            // 報告書管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(907, 601);
            this.Controls.Add(this.button_クリア);
            this.Controls.Add(this.documentGroupCheckBox2);
            this.Controls.Add(this.documentGroupCheckBox1);
            this.Controls.Add(this.button_参照);
            this.Controls.Add(this.textBox_TemplateFileName);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.checkBox_CheckTarget);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.textBox_Shuki);
            this.Controls.Add(this.button_削除);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_閉じる);
            this.Controls.Add(this.textBox_BunshoName);
            this.Controls.Add(this.button_更新);
            this.Controls.Add(this.comboBox_Shoubunrui);
            this.Controls.Add(this.comboBox_Bunrui);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_BunshoNo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "報告書管理詳細Form";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "報告書管理";
            this.Load += new System.EventHandler(this.報告書管理詳細Form_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_更新;
        private System.Windows.Forms.Button button_閉じる;
        private System.Windows.Forms.TextBox textBox_BunshoNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Bunrui;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_BunshoName;
        private System.Windows.Forms.ComboBox comboBox_Shoubunrui;
        private System.Windows.Forms.Button button_削除;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox_Shuki;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox checkBox_Jiki1;
        private System.Windows.Forms.CheckBox checkBox_Jiki2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBox_Jiki3;
        private System.Windows.Forms.CheckBox checkBox_Jiki4;
        private System.Windows.Forms.CheckBox checkBox_Jiki5;
        private System.Windows.Forms.CheckBox checkBox_Jiki6;
        private System.Windows.Forms.CheckBox checkBox_Jiki7;
        private System.Windows.Forms.CheckBox checkBox_Jiki8;
        private System.Windows.Forms.CheckBox checkBox_Jiki9;
        private System.Windows.Forms.CheckBox checkBox_Jiki10;
        private System.Windows.Forms.CheckBox checkBox_Jiki11;
        private System.Windows.Forms.CheckBox checkBox_Jiki12;
        private System.Windows.Forms.CheckBox checkBox_CheckTarget;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox_TemplateFileName;
        private System.Windows.Forms.Button button_参照;
        
        private NBaseCommon.DocumentGroupCheckBox documentGroupCheckBox1;
        private NBaseCommon.DocumentGroupCheckBox documentGroupCheckBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button_クリア;
    }
}