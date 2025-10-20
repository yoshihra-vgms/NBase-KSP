namespace NBaseHonsen.Document
{
    partial class 個別文書登録Form
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
            this.button_登録 = new System.Windows.Forms.Button();
            this.button_閉じる = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox_Bikou = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox_FileName = new System.Windows.Forms.TextBox();
            this.button_参照 = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.nullableDateTimePicker_IssueDate = new NBaseUtil.NullableDateTimePicker();
            this.documentGroupCheckBox1 = new NBaseCommon.DocumentGroupCheckBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(18, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "※文書名";
            // 
            // textBox_BunshoName
            // 
            this.textBox_BunshoName.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_BunshoName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_BunshoName.Location = new System.Drawing.Point(116, 103);
            this.textBox_BunshoName.MaxLength = 50;
            this.textBox_BunshoName.Name = "textBox_BunshoName";
            this.textBox_BunshoName.Size = new System.Drawing.Size(425, 23);
            this.textBox_BunshoName.TabIndex = 4;
            // 
            // comboBox_Shoubunrui
            // 
            this.comboBox_Shoubunrui.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Shoubunrui.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox_Shoubunrui.FormattingEnabled = true;
            this.comboBox_Shoubunrui.Location = new System.Drawing.Point(116, 44);
            this.comboBox_Shoubunrui.Name = "comboBox_Shoubunrui";
            this.comboBox_Shoubunrui.Size = new System.Drawing.Size(425, 24);
            this.comboBox_Shoubunrui.TabIndex = 2;
            // 
            // comboBox_Bunrui
            // 
            this.comboBox_Bunrui.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Bunrui.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox_Bunrui.FormattingEnabled = true;
            this.comboBox_Bunrui.Location = new System.Drawing.Point(116, 14);
            this.comboBox_Bunrui.Name = "comboBox_Bunrui";
            this.comboBox_Bunrui.Size = new System.Drawing.Size(425, 24);
            this.comboBox_Bunrui.TabIndex = 1;
            this.comboBox_Bunrui.SelectedIndexChanged += new System.EventHandler(this.comboBox_Bunrui_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(18, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "※文書番号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(34, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "小分類名";
            // 
            // textBox_BunshoNo
            // 
            this.textBox_BunshoNo.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_BunshoNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_BunshoNo.Location = new System.Drawing.Point(116, 74);
            this.textBox_BunshoNo.MaxLength = 15;
            this.textBox_BunshoNo.Name = "textBox_BunshoNo";
            this.textBox_BunshoNo.Size = new System.Drawing.Size(200, 23);
            this.textBox_BunshoNo.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(18, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "※分類名";
            // 
            // button_登録
            // 
            this.button_登録.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_登録.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_登録.Location = new System.Drawing.Point(970, 17);
            this.button_登録.Name = "button_登録";
            this.button_登録.Size = new System.Drawing.Size(92, 30);
            this.button_登録.TabIndex = 10;
            this.button_登録.Text = "登録";
            this.button_登録.UseVisualStyleBackColor = true;
            this.button_登録.Click += new System.EventHandler(this.button_登録_Click);
            // 
            // button_閉じる
            // 
            this.button_閉じる.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_閉じる.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_閉じる.Location = new System.Drawing.Point(1067, 17);
            this.button_閉じる.Name = "button_閉じる";
            this.button_閉じる.Size = new System.Drawing.Size(92, 30);
            this.button_閉じる.TabIndex = 11;
            this.button_閉じる.Text = "閉じる";
            this.button_閉じる.UseVisualStyleBackColor = true;
            this.button_閉じる.Click += new System.EventHandler(this.button_閉じる_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label18.Location = new System.Drawing.Point(30, 409);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(40, 16);
            this.label18.TabIndex = 0;
            this.label18.Text = "備考";
            // 
            // textBox_Bikou
            // 
            this.textBox_Bikou.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Bikou.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_Bikou.Location = new System.Drawing.Point(112, 406);
            this.textBox_Bikou.MaxLength = 100;
            this.textBox_Bikou.Multiline = true;
            this.textBox_Bikou.Name = "textBox_Bikou";
            this.textBox_Bikou.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Bikou.Size = new System.Drawing.Size(989, 120);
            this.textBox_Bikou.TabIndex = 9;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label21.Location = new System.Drawing.Point(18, 164);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(69, 16);
            this.label21.TabIndex = 0;
            this.label21.Text = "※ファイル";
            // 
            // textBox_FileName
            // 
            this.textBox_FileName.AllowDrop = true;
            this.textBox_FileName.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_FileName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_FileName.Location = new System.Drawing.Point(116, 161);
            this.textBox_FileName.Name = "textBox_FileName";
            this.textBox_FileName.ReadOnly = true;
            this.textBox_FileName.Size = new System.Drawing.Size(546, 23);
            this.textBox_FileName.TabIndex = 6;
            this.textBox_FileName.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_FileName_DragDrop);
            this.textBox_FileName.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_FileName_DragEnter);
            // 
            // button_参照
            // 
            this.button_参照.BackColor = System.Drawing.Color.White;
            this.button_参照.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_参照.Location = new System.Drawing.Point(668, 157);
            this.button_参照.Name = "button_参照";
            this.button_参照.Size = new System.Drawing.Size(75, 30);
            this.button_参照.TabIndex = 7;
            this.button_参照.Text = "参照";
            this.button_参照.UseVisualStyleBackColor = false;
            this.button_参照.Click += new System.EventHandler(this.button_参照_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label20.Location = new System.Drawing.Point(18, 135);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 16);
            this.label20.TabIndex = 0;
            this.label20.Text = "※発行日";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // nullableDateTimePicker_IssueDate
            // 
            this.nullableDateTimePicker_IssueDate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nullableDateTimePicker_IssueDate.Location = new System.Drawing.Point(116, 132);
            this.nullableDateTimePicker_IssueDate.Name = "nullableDateTimePicker_IssueDate";
            this.nullableDateTimePicker_IssueDate.Size = new System.Drawing.Size(164, 23);
            this.nullableDateTimePicker_IssueDate.TabIndex = 5;
            this.nullableDateTimePicker_IssueDate.Value = new System.DateTime(2010, 8, 23, 2, 9, 20, 546);
            // 
            // documentGroupCheckBox1
            // 
            this.documentGroupCheckBox1.BackColor = System.Drawing.Color.White;
            this.documentGroupCheckBox1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.documentGroupCheckBox1.Location = new System.Drawing.Point(21, 196);
            this.documentGroupCheckBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.documentGroupCheckBox1.Name = "documentGroupCheckBox1";
            this.documentGroupCheckBox1.Size = new System.Drawing.Size(1127, 183);
            this.documentGroupCheckBox1.TabIndex = 8;
            // 
            // 個別文書登録Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1180, 569);
            this.Controls.Add(this.documentGroupCheckBox1);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.nullableDateTimePicker_IssueDate);
            this.Controls.Add(this.button_参照);
            this.Controls.Add(this.textBox_FileName);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.textBox_Bikou);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_閉じる);
            this.Controls.Add(this.textBox_BunshoName);
            this.Controls.Add(this.button_登録);
            this.Controls.Add(this.comboBox_Shoubunrui);
            this.Controls.Add(this.comboBox_Bunrui);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_BunshoNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "個別文書登録Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "個別文書登録";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.個別文書登録Form_FormClosed);
            this.Load += new System.EventHandler(this.個別文書登録Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_登録;
        private System.Windows.Forms.Button button_閉じる;
        private System.Windows.Forms.TextBox textBox_BunshoNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Bunrui;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_BunshoName;
        private System.Windows.Forms.ComboBox comboBox_Shoubunrui;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox_Bikou;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox_FileName;
        private System.Windows.Forms.Button button_参照;
        private System.Windows.Forms.Label label20;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker_IssueDate;
        private NBaseCommon.DocumentGroupCheckBox documentGroupCheckBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}