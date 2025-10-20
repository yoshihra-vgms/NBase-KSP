namespace Senin
{
    partial class 講習管理Form
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
            this.button未受講者抽出 = new System.Windows.Forms.Button();
            this.button出力 = new System.Windows.Forms.Button();
            this.button参照 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox氏名コード = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonクリア = new System.Windows.Forms.Button();
            this.comboBox職名 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button検索 = new System.Windows.Forms.Button();
            this.checkBox未受講 = new System.Windows.Forms.CheckBox();
            this.checkBox期限切れ = new System.Windows.Forms.CheckBox();
            this.checkBox受講済み = new System.Windows.Forms.CheckBox();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox氏名カナ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox氏名 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox講習名 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button追加 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.nullableDateTimePicker開始予定 = new NBaseUtil.NullableDateTimePicker();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 175F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1159, 595);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button未受講者抽出);
            this.panel1.Controls.Add(this.button出力);
            this.panel1.Controls.Add(this.button参照);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.button追加);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1153, 169);
            this.panel1.TabIndex = 0;
            // 
            // button未受講者抽出
            // 
            this.button未受講者抽出.BackColor = System.Drawing.SystemColors.Control;
            this.button未受講者抽出.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button未受講者抽出.Location = new System.Drawing.Point(297, 140);
            this.button未受講者抽出.Name = "button未受講者抽出";
            this.button未受講者抽出.Size = new System.Drawing.Size(92, 23);
            this.button未受講者抽出.TabIndex = 17;
            this.button未受講者抽出.Text = "未受講者抽出";
            this.button未受講者抽出.UseVisualStyleBackColor = false;
            this.button未受講者抽出.Click += new System.EventHandler(this.button未受講者抽出_Click);
            // 
            // button出力
            // 
            this.button出力.BackColor = System.Drawing.SystemColors.Control;
            this.button出力.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button出力.Location = new System.Drawing.Point(199, 140);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(92, 23);
            this.button出力.TabIndex = 16;
            this.button出力.Text = "一覧出力";
            this.button出力.UseVisualStyleBackColor = false;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // button参照
            // 
            this.button参照.BackColor = System.Drawing.SystemColors.Control;
            this.button参照.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button参照.Location = new System.Drawing.Point(101, 140);
            this.button参照.Name = "button参照";
            this.button参照.Size = new System.Drawing.Size(92, 23);
            this.button参照.TabIndex = 15;
            this.button参照.Text = "講習情報参照";
            this.button参照.UseVisualStyleBackColor = false;
            this.button参照.Click += new System.EventHandler(this.button参照_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nullableDateTimePicker開始予定);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox氏名コード);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.buttonクリア);
            this.groupBox1.Controls.Add(this.comboBox職名);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Controls.Add(this.checkBox未受講);
            this.groupBox1.Controls.Add(this.checkBox期限切れ);
            this.groupBox1.Controls.Add(this.checkBox受講済み);
            this.groupBox1.Controls.Add(this.textBox備考);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox氏名カナ);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox氏名);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox講習名);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(872, 131);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 12);
            this.label5.TabIndex = 28;
            this.label5.Text = "予定(開始）";
            // 
            // textBox氏名コード
            // 
            this.textBox氏名コード.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox氏名コード.Location = new System.Drawing.Point(377, 70);
            this.textBox氏名コード.MaxLength = 10;
            this.textBox氏名コード.Name = "textBox氏名コード";
            this.textBox氏名コード.Size = new System.Drawing.Size(100, 19);
            this.textBox氏名コード.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(315, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "従業員番号";
            // 
            // buttonクリア
            // 
            this.buttonクリア.BackColor = System.Drawing.SystemColors.Control;
            this.buttonクリア.Location = new System.Drawing.Point(707, 48);
            this.buttonクリア.Name = "buttonクリア";
            this.buttonクリア.Size = new System.Drawing.Size(92, 23);
            this.buttonクリア.TabIndex = 13;
            this.buttonクリア.Text = "検索条件クリア";
            this.buttonクリア.UseVisualStyleBackColor = false;
            this.buttonクリア.Click += new System.EventHandler(this.buttonクリア_Click);
            // 
            // comboBox職名
            // 
            this.comboBox職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名.FormattingEnabled = true;
            this.comboBox職名.Location = new System.Drawing.Point(78, 42);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(150, 20);
            this.comboBox職名.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "職名";
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(707, 17);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(92, 23);
            this.button検索.TabIndex = 12;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // checkBox未受講
            // 
            this.checkBox未受講.AutoSize = true;
            this.checkBox未受講.Checked = true;
            this.checkBox未受講.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox未受講.Location = new System.Drawing.Point(561, 48);
            this.checkBox未受講.Name = "checkBox未受講";
            this.checkBox未受講.Size = new System.Drawing.Size(72, 16);
            this.checkBox未受講.TabIndex = 8;
            this.checkBox未受講.Text = "受講予定";
            this.checkBox未受講.UseVisualStyleBackColor = true;
            // 
            // checkBox期限切れ
            // 
            this.checkBox期限切れ.AutoSize = true;
            this.checkBox期限切れ.BackColor = System.Drawing.Color.White;
            this.checkBox期限切れ.Location = new System.Drawing.Point(561, 74);
            this.checkBox期限切れ.Name = "checkBox期限切れ";
            this.checkBox期限切れ.Size = new System.Drawing.Size(71, 16);
            this.checkBox期限切れ.TabIndex = 7;
            this.checkBox期限切れ.Text = "期限切れ";
            this.checkBox期限切れ.UseVisualStyleBackColor = false;
            // 
            // checkBox受講済み
            // 
            this.checkBox受講済み.AutoSize = true;
            this.checkBox受講済み.BackColor = System.Drawing.Color.White;
            this.checkBox受講済み.Checked = true;
            this.checkBox受講済み.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox受講済み.Location = new System.Drawing.Point(561, 22);
            this.checkBox受講済み.Name = "checkBox受講済み";
            this.checkBox受講済み.Size = new System.Drawing.Size(60, 16);
            this.checkBox受講済み.TabIndex = 7;
            this.checkBox受講済み.Text = "受講済";
            this.checkBox受講済み.UseVisualStyleBackColor = false;
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox備考.Location = new System.Drawing.Point(78, 100);
            this.textBox備考.MaxLength = 50;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.Size = new System.Drawing.Size(199, 19);
            this.textBox備考.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "備考";
            // 
            // textBox氏名カナ
            // 
            this.textBox氏名カナ.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.textBox氏名カナ.Location = new System.Drawing.Point(377, 44);
            this.textBox氏名カナ.MaxLength = 41;
            this.textBox氏名カナ.Name = "textBox氏名カナ";
            this.textBox氏名カナ.Size = new System.Drawing.Size(151, 19);
            this.textBox氏名カナ.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(315, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "氏名(カナ)";
            // 
            // textBox氏名
            // 
            this.textBox氏名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox氏名.Location = new System.Drawing.Point(377, 19);
            this.textBox氏名.MaxLength = 41;
            this.textBox氏名.Name = "textBox氏名";
            this.textBox氏名.Size = new System.Drawing.Size(151, 19);
            this.textBox氏名.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "氏名";
            // 
            // textBox講習名
            // 
            this.textBox講習名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox講習名.Location = new System.Drawing.Point(78, 16);
            this.textBox講習名.MaxLength = 20;
            this.textBox講習名.Name = "textBox講習名";
            this.textBox講習名.Size = new System.Drawing.Size(199, 19);
            this.textBox講習名.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "講習名";
            // 
            // button追加
            // 
            this.button追加.BackColor = System.Drawing.SystemColors.Control;
            this.button追加.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button追加.Location = new System.Drawing.Point(3, 140);
            this.button追加.Name = "button追加";
            this.button追加.Size = new System.Drawing.Size(92, 23);
            this.button追加.TabIndex = 14;
            this.button追加.Text = "講習情報追加";
            this.button追加.UseVisualStyleBackColor = false;
            this.button追加.Click += new System.EventHandler(this.button追加_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 178);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1153, 414);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // nullableDateTimePicker開始予定
            // 
            this.nullableDateTimePicker開始予定.Location = new System.Drawing.Point(78, 70);
            this.nullableDateTimePicker開始予定.Name = "nullableDateTimePicker開始予定";
            this.nullableDateTimePicker開始予定.Size = new System.Drawing.Size(151, 19);
            this.nullableDateTimePicker開始予定.TabIndex = 29;
            this.nullableDateTimePicker開始予定.Value = new System.DateTime(2012, 3, 23, 17, 31, 16, 901);
            // 
            // 講習管理Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1159, 595);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "講習管理Form";
            this.Text = "講習管理Form";
            this.Load += new System.EventHandler(this.講習管理Form_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.講習管理Form_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
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
        private System.Windows.Forms.CheckBox checkBox未受講;
        private System.Windows.Forms.CheckBox checkBox期限切れ;
        private System.Windows.Forms.CheckBox checkBox受講済み;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox氏名カナ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox氏名;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox講習名;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button追加;
        private System.Windows.Forms.TextBox textBox氏名コード;
        private System.Windows.Forms.Label label6;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker開始予定;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button参照;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button未受講者抽出;
    }
}