namespace Senin
{
    partial class 未受講者抽出Form
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label対象件数 = new System.Windows.Forms.Label();
            this.button出力 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox講習名 = new System.Windows.Forms.ComboBox();
            this.radioButton受講済 = new System.Windows.Forms.RadioButton();
            this.radioButton未受講 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox氏名コード = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonクリア = new System.Windows.Forms.Button();
            this.comboBox職名 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button検索 = new System.Windows.Forms.Button();
            this.textBox氏名カナ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox氏名 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.nullableDateTimePicker受講日To = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker受講日From = new NBaseUtil.NullableDateTimePicker();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 173F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(883, 595);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label対象件数);
            this.panel1.Controls.Add(this.button出力);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 167);
            this.panel1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(739, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "件数：";
            // 
            // label対象件数
            // 
            this.label対象件数.AutoSize = true;
            this.label対象件数.BackColor = System.Drawing.Color.White;
            this.label対象件数.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label対象件数.Location = new System.Drawing.Point(784, 147);
            this.label対象件数.Name = "label対象件数";
            this.label対象件数.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label対象件数.Size = new System.Drawing.Size(70, 13);
            this.label対象件数.TabIndex = 17;
            this.label対象件数.Text = "99999 件";
            // 
            // button出力
            // 
            this.button出力.BackColor = System.Drawing.SystemColors.Control;
            this.button出力.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button出力.Location = new System.Drawing.Point(8, 141);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(92, 23);
            this.button出力.TabIndex = 0;
            this.button出力.Text = "Excel出力";
            this.button出力.UseVisualStyleBackColor = false;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox講習名);
            this.groupBox1.Controls.Add(this.radioButton受講済);
            this.groupBox1.Controls.Add(this.radioButton未受講);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.nullableDateTimePicker受講日To);
            this.groupBox1.Controls.Add(this.nullableDateTimePicker受講日From);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox氏名コード);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.buttonクリア);
            this.groupBox1.Controls.Add(this.comboBox職名);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Controls.Add(this.textBox氏名カナ);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox氏名);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(860, 133);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // comboBox講習名
            // 
            this.comboBox講習名.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox講習名.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox講習名.FormattingEnabled = true;
            this.comboBox講習名.Location = new System.Drawing.Point(77, 19);
            this.comboBox講習名.MaxDropDownItems = 25;
            this.comboBox講習名.Name = "comboBox講習名";
            this.comboBox講習名.Size = new System.Drawing.Size(250, 20);
            this.comboBox講習名.TabIndex = 0;
            // 
            // radioButton受講済
            // 
            this.radioButton受講済.AutoSize = true;
            this.radioButton受講済.Location = new System.Drawing.Point(571, 45);
            this.radioButton受講済.Name = "radioButton受講済";
            this.radioButton受講済.Size = new System.Drawing.Size(59, 16);
            this.radioButton受講済.TabIndex = 8;
            this.radioButton受講済.TabStop = true;
            this.radioButton受講済.Text = "受講済";
            this.radioButton受講済.UseVisualStyleBackColor = true;
            // 
            // radioButton未受講
            // 
            this.radioButton未受講.AutoSize = true;
            this.radioButton未受講.Checked = true;
            this.radioButton未受講.Location = new System.Drawing.Point(571, 20);
            this.radioButton未受講.Name = "radioButton未受講";
            this.radioButton未受講.Size = new System.Drawing.Size(59, 16);
            this.radioButton未受講.TabIndex = 7;
            this.radioButton未受講.TabStop = true;
            this.radioButton未受講.Text = "未受講";
            this.radioButton未受講.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "～";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 28;
            this.label5.Text = "受講日";
            // 
            // textBox氏名コード
            // 
            this.textBox氏名コード.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox氏名コード.Location = new System.Drawing.Point(380, 46);
            this.textBox氏名コード.MaxLength = 10;
            this.textBox氏名コード.Name = "textBox氏名コード";
            this.textBox氏名コード.Size = new System.Drawing.Size(100, 19);
            this.textBox氏名コード.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(308, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "従業員番号";
            // 
            // buttonクリア
            // 
            this.buttonクリア.BackColor = System.Drawing.SystemColors.Control;
            this.buttonクリア.Location = new System.Drawing.Point(752, 42);
            this.buttonクリア.Name = "buttonクリア";
            this.buttonクリア.Size = new System.Drawing.Size(92, 23);
            this.buttonクリア.TabIndex = 10;
            this.buttonクリア.Text = "検索条件クリア";
            this.buttonクリア.UseVisualStyleBackColor = false;
            this.buttonクリア.Click += new System.EventHandler(this.buttonクリア_Click);
            // 
            // comboBox職名
            // 
            this.comboBox職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名.FormattingEnabled = true;
            this.comboBox職名.Location = new System.Drawing.Point(78, 44);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(150, 20);
            this.comboBox職名.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "職名";
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(752, 16);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(92, 23);
            this.button検索.TabIndex = 9;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // textBox氏名カナ
            // 
            this.textBox氏名カナ.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.textBox氏名カナ.Location = new System.Drawing.Point(380, 73);
            this.textBox氏名カナ.MaxLength = 41;
            this.textBox氏名カナ.Name = "textBox氏名カナ";
            this.textBox氏名カナ.Size = new System.Drawing.Size(151, 19);
            this.textBox氏名カナ.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "氏名(カナ)";
            // 
            // textBox氏名
            // 
            this.textBox氏名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox氏名.Location = new System.Drawing.Point(78, 73);
            this.textBox氏名.MaxLength = 41;
            this.textBox氏名.Name = "textBox氏名";
            this.textBox氏名.Size = new System.Drawing.Size(151, 19);
            this.textBox氏名.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "氏名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "講習名";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 176);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(877, 416);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // nullableDateTimePicker受講日To
            // 
            this.nullableDateTimePicker受講日To.Location = new System.Drawing.Point(257, 102);
            this.nullableDateTimePicker受講日To.Name = "nullableDateTimePicker受講日To";
            this.nullableDateTimePicker受講日To.Size = new System.Drawing.Size(151, 19);
            this.nullableDateTimePicker受講日To.TabIndex = 6;
            this.nullableDateTimePicker受講日To.Value = new System.DateTime(2012, 3, 23, 17, 31, 16, 901);
            // 
            // nullableDateTimePicker受講日From
            // 
            this.nullableDateTimePicker受講日From.Location = new System.Drawing.Point(77, 101);
            this.nullableDateTimePicker受講日From.Name = "nullableDateTimePicker受講日From";
            this.nullableDateTimePicker受講日From.Size = new System.Drawing.Size(151, 19);
            this.nullableDateTimePicker受講日From.TabIndex = 5;
            this.nullableDateTimePicker受講日From.Value = new System.DateTime(2012, 3, 23, 17, 31, 16, 901);
            // 
            // 未受講者抽出Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(883, 595);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "未受講者抽出Form";
            this.Text = "未受講者抽出Form";
            this.Load += new System.EventHandler(this.未受講者抽出Form_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.未受講者抽出Form_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonクリア;
        private System.Windows.Forms.ComboBox comboBox職名;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.TextBox textBox氏名カナ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox氏名;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox氏名コード;
        private System.Windows.Forms.Label label6;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker受講日From;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker受講日To;
        private System.Windows.Forms.RadioButton radioButton未受講;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButton受講済;
        private System.Windows.Forms.ComboBox comboBox講習名;
        private System.Windows.Forms.Label label対象件数;
        private System.Windows.Forms.Label label7;
    }
}