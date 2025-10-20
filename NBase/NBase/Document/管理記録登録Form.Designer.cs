namespace Document
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
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_雛形ダウンロード
            // 
            this.button_雛形ダウンロード.Location = new System.Drawing.Point(341, 11);
            this.button_雛形ダウンロード.Name = "button_雛形ダウンロード";
            this.button_雛形ダウンロード.Size = new System.Drawing.Size(90, 23);
            this.button_雛形ダウンロード.TabIndex = 0;
            this.button_雛形ダウンロード.Text = "テンプレート";
            this.button_雛形ダウンロード.UseVisualStyleBackColor = true;
            this.button_雛形ダウンロード.Click += new System.EventHandler(this.button_雛形ダウンロード_Click);
            // 
            // button_報告書登録
            // 
            this.button_報告書登録.Location = new System.Drawing.Point(437, 11);
            this.button_報告書登録.Name = "button_報告書登録";
            this.button_報告書登録.Size = new System.Drawing.Size(90, 23);
            this.button_報告書登録.TabIndex = 1;
            this.button_報告書登録.Text = "報告書登録";
            this.button_報告書登録.UseVisualStyleBackColor = true;
            this.button_報告書登録.Click += new System.EventHandler(this.button_報告書登録_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "※提出時期";
            // 
            // comboBox_JikiNen
            // 
            this.comboBox_JikiNen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_JikiNen.FormattingEnabled = true;
            this.comboBox_JikiNen.Location = new System.Drawing.Point(85, 14);
            this.comboBox_JikiNen.Name = "comboBox_JikiNen";
            this.comboBox_JikiNen.Size = new System.Drawing.Size(84, 20);
            this.comboBox_JikiNen.TabIndex = 1;
            // 
            // comboBox_JikiTuki
            // 
            this.comboBox_JikiTuki.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_JikiTuki.FormattingEnabled = true;
            this.comboBox_JikiTuki.Location = new System.Drawing.Point(175, 14);
            this.comboBox_JikiTuki.Name = "comboBox_JikiTuki";
            this.comboBox_JikiTuki.Size = new System.Drawing.Size(84, 20);
            this.comboBox_JikiTuki.TabIndex = 2;
            // 
            // nullableDateTimePicker_IssueDate
            // 
            this.nullableDateTimePicker_IssueDate.Location = new System.Drawing.Point(85, 40);
            this.nullableDateTimePicker_IssueDate.Name = "nullableDateTimePicker_IssueDate";
            this.nullableDateTimePicker_IssueDate.Size = new System.Drawing.Size(126, 19);
            this.nullableDateTimePicker_IssueDate.TabIndex = 3;
            this.nullableDateTimePicker_IssueDate.Value = new System.DateTime(2010, 8, 23, 2, 9, 20, 546);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "※発行日";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "※ファイル";
            // 
            // button_参照
            // 
            this.button_参照.BackColor = System.Drawing.Color.White;
            this.button_参照.Location = new System.Drawing.Point(437, 64);
            this.button_参照.Name = "button_参照";
            this.button_参照.Size = new System.Drawing.Size(75, 23);
            this.button_参照.TabIndex = 5;
            this.button_参照.Text = "参照";
            this.button_参照.UseVisualStyleBackColor = false;
            this.button_参照.Click += new System.EventHandler(this.button_参照_Click);
            // 
            // textBox_FileName
            // 
            this.textBox_FileName.AllowDrop = true;
            this.textBox_FileName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox_FileName.Location = new System.Drawing.Point(85, 66);
            this.textBox_FileName.Name = "textBox_FileName";
            this.textBox_FileName.ReadOnly = true;
            this.textBox_FileName.Size = new System.Drawing.Size(346, 19);
            this.textBox_FileName.TabIndex = 4;
            this.textBox_FileName.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_FileName_DragDrop);
            this.textBox_FileName.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_FileName_DragEnter);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "備考";
            // 
            // textBox_Bikou
            // 
            this.textBox_Bikou.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_Bikou.Location = new System.Drawing.Point(85, 96);
            this.textBox_Bikou.MaxLength = 100;
            this.textBox_Bikou.Multiline = true;
            this.textBox_Bikou.Name = "textBox_Bikou";
            this.textBox_Bikou.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Bikou.Size = new System.Drawing.Size(427, 82);
            this.textBox_Bikou.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.button_報告書ダウンロード);
            this.groupBox1.Location = new System.Drawing.Point(13, 184);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(511, 181);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "過去のファイル";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(7, 47);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(496, 123);
            this.dataGridView1.TabIndex = 0;
            // 
            // button_報告書ダウンロード
            // 
            this.button_報告書ダウンロード.Location = new System.Drawing.Point(6, 18);
            this.button_報告書ダウンロード.Name = "button_報告書ダウンロード";
            this.button_報告書ダウンロード.Size = new System.Drawing.Size(108, 23);
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
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button_報告書登録);
            this.Controls.Add(this.button_雛形ダウンロード);
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
            this.Name = "管理記録登録Form";
            this.Size = new System.Drawing.Size(536, 375);
            this.Load += new System.EventHandler(this.管理記録登録Form_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_雛形ダウンロード;
        private System.Windows.Forms.Button button_報告書登録;
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