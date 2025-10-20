namespace NBaseHonsen.Document
{
    partial class 管理記録登録Form
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
            this.button_雛形ダウンロード = new System.Windows.Forms.Button();
            this.button_報告書登録 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button_閉じる = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_Bunrui = new System.Windows.Forms.Label();
            this.label_Shoubunrui = new System.Windows.Forms.Label();
            this.label_BunshoNo = new System.Windows.Forms.Label();
            this.label_BunshoName = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_JikiNen = new System.Windows.Forms.ComboBox();
            this.comboBox_JikiTuki = new System.Windows.Forms.ComboBox();
            this.nullableDateTimePicker_IssueDate = new NBaseUtil.NullableDateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button_参照 = new System.Windows.Forms.Button();
            this.textBox_FileName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_Bikou = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_報告書ダウンロード = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_雛形ダウンロード
            // 
            this.button_雛形ダウンロード.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_雛形ダウンロード.Location = new System.Drawing.Point(414, 3);
            this.button_雛形ダウンロード.Name = "button_雛形ダウンロード";
            this.button_雛形ダウンロード.Size = new System.Drawing.Size(135, 30);
            this.button_雛形ダウンロード.TabIndex = 0;
            this.button_雛形ダウンロード.Text = "雛形ダウンロード";
            this.button_雛形ダウンロード.UseVisualStyleBackColor = true;
            this.button_雛形ダウンロード.Click += new System.EventHandler(this.button_雛形ダウンロード_Click);
            // 
            // button_報告書登録
            // 
            this.button_報告書登録.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_報告書登録.Location = new System.Drawing.Point(555, 3);
            this.button_報告書登録.Name = "button_報告書登録";
            this.button_報告書登録.Size = new System.Drawing.Size(135, 30);
            this.button_報告書登録.TabIndex = 1;
            this.button_報告書登録.Text = "報告書登録";
            this.button_報告書登録.UseVisualStyleBackColor = true;
            this.button_報告書登録.Click += new System.EventHandler(this.button_報告書登録_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button_閉じる);
            this.flowLayoutPanel1.Controls.Add(this.button_報告書登録);
            this.flowLayoutPanel1.Controls.Add(this.button_雛形ダウンロード);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(834, 41);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // button_閉じる
            // 
            this.button_閉じる.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_閉じる.Location = new System.Drawing.Point(696, 3);
            this.button_閉じる.Name = "button_閉じる";
            this.button_閉じる.Size = new System.Drawing.Size(135, 30);
            this.button_閉じる.TabIndex = 3;
            this.button_閉じる.Text = "閉じる";
            this.button_閉じる.UseVisualStyleBackColor = true;
            this.button_閉じる.Click += new System.EventHandler(this.button_閉じる_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(30, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "分類";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(30, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "小分類";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(30, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "文書番号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(30, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "文書名";
            // 
            // label_Bunrui
            // 
            this.label_Bunrui.AutoSize = true;
            this.label_Bunrui.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Bunrui.Location = new System.Drawing.Point(118, 44);
            this.label_Bunrui.Name = "label_Bunrui";
            this.label_Bunrui.Size = new System.Drawing.Size(16, 16);
            this.label_Bunrui.TabIndex = 0;
            this.label_Bunrui.Text = "：";
            // 
            // label_Shoubunrui
            // 
            this.label_Shoubunrui.AutoSize = true;
            this.label_Shoubunrui.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Shoubunrui.Location = new System.Drawing.Point(118, 69);
            this.label_Shoubunrui.Name = "label_Shoubunrui";
            this.label_Shoubunrui.Size = new System.Drawing.Size(16, 16);
            this.label_Shoubunrui.TabIndex = 0;
            this.label_Shoubunrui.Text = "：";
            // 
            // label_BunshoNo
            // 
            this.label_BunshoNo.AutoSize = true;
            this.label_BunshoNo.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_BunshoNo.Location = new System.Drawing.Point(118, 94);
            this.label_BunshoNo.Name = "label_BunshoNo";
            this.label_BunshoNo.Size = new System.Drawing.Size(16, 16);
            this.label_BunshoNo.TabIndex = 0;
            this.label_BunshoNo.Text = "：";
            // 
            // label_BunshoName
            // 
            this.label_BunshoName.AutoSize = true;
            this.label_BunshoName.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_BunshoName.Location = new System.Drawing.Point(118, 119);
            this.label_BunshoName.Name = "label_BunshoName";
            this.label_BunshoName.Size = new System.Drawing.Size(16, 16);
            this.label_BunshoName.TabIndex = 0;
            this.label_BunshoName.Text = "：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(12, 147);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 16);
            this.label9.TabIndex = 0;
            this.label9.Text = "※提出時期";
            // 
            // comboBox_JikiNen
            // 
            this.comboBox_JikiNen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_JikiNen.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox_JikiNen.FormattingEnabled = true;
            this.comboBox_JikiNen.Location = new System.Drawing.Point(118, 144);
            this.comboBox_JikiNen.Name = "comboBox_JikiNen";
            this.comboBox_JikiNen.Size = new System.Drawing.Size(93, 24);
            this.comboBox_JikiNen.TabIndex = 1;
            // 
            // comboBox_JikiTuki
            // 
            this.comboBox_JikiTuki.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_JikiTuki.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox_JikiTuki.FormattingEnabled = true;
            this.comboBox_JikiTuki.Location = new System.Drawing.Point(217, 144);
            this.comboBox_JikiTuki.Name = "comboBox_JikiTuki";
            this.comboBox_JikiTuki.Size = new System.Drawing.Size(84, 24);
            this.comboBox_JikiTuki.TabIndex = 2;
            // 
            // nullableDateTimePicker_IssueDate
            // 
            this.nullableDateTimePicker_IssueDate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nullableDateTimePicker_IssueDate.Location = new System.Drawing.Point(118, 177);
            this.nullableDateTimePicker_IssueDate.Name = "nullableDateTimePicker_IssueDate";
            this.nullableDateTimePicker_IssueDate.Size = new System.Drawing.Size(158, 23);
            this.nullableDateTimePicker_IssueDate.TabIndex = 3;
            this.nullableDateTimePicker_IssueDate.Value = new System.DateTime(2010, 8, 23, 2, 9, 20, 546);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(17, 180);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 16);
            this.label10.TabIndex = 0;
            this.label10.Text = "※発行日";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(17, 212);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 16);
            this.label11.TabIndex = 0;
            this.label11.Text = "※ファイル";
            // 
            // button_参照
            // 
            this.button_参照.BackColor = System.Drawing.Color.White;
            this.button_参照.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_参照.Location = new System.Drawing.Point(598, 205);
            this.button_参照.Name = "button_参照";
            this.button_参照.Size = new System.Drawing.Size(75, 30);
            this.button_参照.TabIndex = 5;
            this.button_参照.Text = "参照";
            this.button_参照.UseVisualStyleBackColor = false;
            this.button_参照.Click += new System.EventHandler(this.button_参照_Click);
            // 
            // textBox_FileName
            // 
            this.textBox_FileName.AllowDrop = true;
            this.textBox_FileName.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_FileName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox_FileName.Location = new System.Drawing.Point(118, 209);
            this.textBox_FileName.Name = "textBox_FileName";
            this.textBox_FileName.ReadOnly = true;
            this.textBox_FileName.Size = new System.Drawing.Size(474, 23);
            this.textBox_FileName.TabIndex = 4;
            this.textBox_FileName.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_FileName_DragDrop);
            this.textBox_FileName.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_FileName_DragEnter);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.Location = new System.Drawing.Point(36, 244);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 16);
            this.label12.TabIndex = 0;
            this.label12.Text = "備考";
            // 
            // textBox_Bikou
            // 
            this.textBox_Bikou.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Bikou.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_Bikou.Location = new System.Drawing.Point(118, 241);
            this.textBox_Bikou.MaxLength = 100;
            this.textBox_Bikou.Multiline = true;
            this.textBox_Bikou.Name = "textBox_Bikou";
            this.textBox_Bikou.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Bikou.Size = new System.Drawing.Size(694, 118);
            this.textBox_Bikou.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.button_報告書ダウンロード);
            this.groupBox1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.Location = new System.Drawing.Point(17, 367);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(795, 267);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "過去のファイル";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 58);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(783, 189);
            this.dataGridView1.TabIndex = 0;
            // 
            // button_報告書ダウンロード
            // 
            this.button_報告書ダウンロード.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_報告書ダウンロード.Location = new System.Drawing.Point(6, 22);
            this.button_報告書ダウンロード.Name = "button_報告書ダウンロード";
            this.button_報告書ダウンロード.Size = new System.Drawing.Size(147, 30);
            this.button_報告書ダウンロード.TabIndex = 0;
            this.button_報告書ダウンロード.Text = "報告書ダウンロード";
            this.button_報告書ダウンロード.UseVisualStyleBackColor = true;
            this.button_報告書ダウンロード.Click += new System.EventHandler(this.button_報告書ダウンロード_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // 管理記録登録Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(834, 646);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox_Bikou);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button_参照);
            this.Controls.Add(this.textBox_FileName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.nullableDateTimePicker_IssueDate);
            this.Controls.Add(this.comboBox_JikiTuki);
            this.Controls.Add(this.comboBox_JikiNen);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label_BunshoName);
            this.Controls.Add(this.label_BunshoNo);
            this.Controls.Add(this.label_Shoubunrui);
            this.Controls.Add(this.label_Bunrui);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "管理記録登録Form";
            this.Text = "管理記録登録";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.管理記録登録Form_FormClosed);
            this.Load += new System.EventHandler(this.管理記録登録Form_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_雛形ダウンロード;
        private System.Windows.Forms.Button button_報告書登録;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button_閉じる;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_Bunrui;
        private System.Windows.Forms.Label label_Shoubunrui;
        private System.Windows.Forms.Label label_BunshoNo;
        private System.Windows.Forms.Label label_BunshoName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox_JikiNen;
        private System.Windows.Forms.ComboBox comboBox_JikiTuki;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker_IssueDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button_参照;
        private System.Windows.Forms.TextBox textBox_FileName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_Bikou;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_報告書ダウンロード;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}