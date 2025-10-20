namespace Senin
{
    partial class 個人予定一覧Form
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label38 = new System.Windows.Forms.Label();
            this.buttonクリア = new System.Windows.Forms.Button();
            this.comboBox職名 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button出力 = new System.Windows.Forms.Button();
            this.button検索 = new System.Windows.Forms.Button();
            this.textBox氏名 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button追加 = new System.Windows.Forms.Button();
            this.nullableDateTimePicker終了日 = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker開始日 = new NBaseUtil.NullableDateTimePicker();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.button閉じる);
            this.panel1.Controls.Add(this.button追加);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(621, 501);
            this.panel1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(8, 146);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(601, 291);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.nullableDateTimePicker終了日);
            this.groupBox1.Controls.Add(this.nullableDateTimePicker開始日);
            this.groupBox1.Controls.Add(this.label38);
            this.groupBox1.Controls.Add(this.buttonクリア);
            this.groupBox1.Controls.Add(this.comboBox職名);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.button出力);
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Controls.Add(this.textBox氏名);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(601, 132);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(198, 85);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(17, 12);
            this.label38.TabIndex = 31;
            this.label38.Text = "～";
            // 
            // buttonクリア
            // 
            this.buttonクリア.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonクリア.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonクリア.Location = new System.Drawing.Point(485, 85);
            this.buttonクリア.Name = "buttonクリア";
            this.buttonクリア.Size = new System.Drawing.Size(92, 23);
            this.buttonクリア.TabIndex = 13;
            this.buttonクリア.Text = "検索条件クリア";
            this.buttonクリア.UseVisualStyleBackColor = false;
            this.buttonクリア.Visible = false;
            // 
            // comboBox職名
            // 
            this.comboBox職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名.FormattingEnabled = true;
            this.comboBox職名.Location = new System.Drawing.Point(69, 23);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(150, 20);
            this.comboBox職名.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "職名";
            // 
            // button出力
            // 
            this.button出力.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button出力.BackColor = System.Drawing.SystemColors.Control;
            this.button出力.Location = new System.Drawing.Point(485, 50);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(92, 23);
            this.button出力.TabIndex = 12;
            this.button出力.Text = "Excel";
            this.button出力.UseVisualStyleBackColor = false;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // button検索
            // 
            this.button検索.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(485, 21);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(92, 23);
            this.button検索.TabIndex = 12;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // textBox氏名
            // 
            this.textBox氏名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox氏名.Location = new System.Drawing.Point(69, 52);
            this.textBox氏名.Name = "textBox氏名";
            this.textBox氏名.Size = new System.Drawing.Size(100, 19);
            this.textBox氏名.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "期間";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "氏名";
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(310, 466);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(92, 23);
            this.button閉じる.TabIndex = 12;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button追加
            // 
            this.button追加.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button追加.BackColor = System.Drawing.SystemColors.Control;
            this.button追加.Location = new System.Drawing.Point(201, 466);
            this.button追加.Name = "button追加";
            this.button追加.Size = new System.Drawing.Size(92, 23);
            this.button追加.TabIndex = 12;
            this.button追加.Text = "追加";
            this.button追加.UseVisualStyleBackColor = false;
            this.button追加.Click += new System.EventHandler(this.button追加_Click);
            // 
            // nullableDateTimePicker終了日
            // 
            this.nullableDateTimePicker終了日.Location = new System.Drawing.Point(221, 80);
            this.nullableDateTimePicker終了日.Name = "nullableDateTimePicker終了日";
            this.nullableDateTimePicker終了日.Size = new System.Drawing.Size(123, 19);
            this.nullableDateTimePicker終了日.TabIndex = 32;
            this.nullableDateTimePicker終了日.Value = new System.DateTime(2017, 11, 1, 10, 6, 34, 715);
            // 
            // nullableDateTimePicker開始日
            // 
            this.nullableDateTimePicker開始日.Location = new System.Drawing.Point(69, 80);
            this.nullableDateTimePicker開始日.Name = "nullableDateTimePicker開始日";
            this.nullableDateTimePicker開始日.Size = new System.Drawing.Size(123, 19);
            this.nullableDateTimePicker開始日.TabIndex = 32;
            this.nullableDateTimePicker開始日.Value = new System.DateTime(2017, 11, 1, 10, 6, 34, 715);
            // 
            // 個人予定一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(621, 501);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(637, 540);
            this.MinimumSize = new System.Drawing.Size(637, 540);
            this.Name = "個人予定一覧Form";
            this.Text = "個人予定一覧";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.個人予定一覧Form_FormClosing);
            this.Load += new System.EventHandler(this.個人予定一覧Form_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonクリア;
        private System.Windows.Forms.ComboBox comboBox職名;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.TextBox textBox氏名;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.DataGridView dataGridView1;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker終了日;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker開始日;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button追加;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

    }
}