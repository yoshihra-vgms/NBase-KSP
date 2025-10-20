namespace Senin
{
    partial class 配乗シミュレーションForm
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
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker_乗船予定日 = new NBaseUtil.NullableDateTimePicker();
            this.button_交代者決定 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button乗船者_検索条件クリア = new System.Windows.Forms.Button();
            this.button乗船者検索 = new System.Windows.Forms.Button();
            this.checkedListBox種別 = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button経験 = new System.Windows.Forms.Button();
            this.checkBox乗船者_経験 = new System.Windows.Forms.CheckBox();
            this.button乗り合わせ = new System.Windows.Forms.Button();
            this.checkBox乗船者_乗り合わせ = new System.Windows.Forms.CheckBox();
            this.button乗船者_個人予定 = new System.Windows.Forms.Button();
            this.checkBox乗船者_個人予定 = new System.Windows.Forms.CheckBox();
            this.button講習 = new System.Windows.Forms.Button();
            this.checkBox講習 = new System.Windows.Forms.CheckBox();
            this.checkBox休暇日数 = new System.Windows.Forms.CheckBox();
            this.checkBox乗船者_職名 = new System.Windows.Forms.CheckBox();
            this.button免許免状 = new System.Windows.Forms.Button();
            this.checkBox乗船者_船 = new System.Windows.Forms.CheckBox();
            this.checkBox免許免状 = new System.Windows.Forms.CheckBox();
            this.textBox休暇日数 = new System.Windows.Forms.TextBox();
            this.comboBox乗船者_職名 = new System.Windows.Forms.ComboBox();
            this.comboBox乗船者_船 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button下船者_検索条件クリア = new System.Windows.Forms.Button();
            this.button下船者_個人予定 = new System.Windows.Forms.Button();
            this.checkBox下船者_個人予定 = new System.Windows.Forms.CheckBox();
            this.button下船者検索 = new System.Windows.Forms.Button();
            this.textBox乗船日数 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker基準日 = new System.Windows.Forms.DateTimePicker();
            this.comboBox下船者_職名 = new System.Windows.Forms.ComboBox();
            this.comboBox下船者_船 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_交代者解除 = new System.Windows.Forms.Button();
            this.button_船員交代予定表 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1784, 941);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.dataGridView2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(895, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(886, 935);
            this.tableLayoutPanel3.TabIndex = 27;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.nullableDateTimePicker_乗船予定日);
            this.panel2.Controls.Add(this.button_交代者決定);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 838);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(880, 94);
            this.panel2.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(551, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "乗船予定日";
            // 
            // nullableDateTimePicker_乗船予定日
            // 
            this.nullableDateTimePicker_乗船予定日.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nullableDateTimePicker_乗船予定日.Location = new System.Drawing.Point(622, 36);
            this.nullableDateTimePicker_乗船予定日.Name = "nullableDateTimePicker_乗船予定日";
            this.nullableDateTimePicker_乗船予定日.Size = new System.Drawing.Size(135, 19);
            this.nullableDateTimePicker_乗船予定日.TabIndex = 1;
            this.nullableDateTimePicker_乗船予定日.Value = new System.DateTime(2017, 10, 18, 18, 22, 59, 102);
            // 
            // button_交代者決定
            // 
            this.button_交代者決定.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_交代者決定.Location = new System.Drawing.Point(772, 36);
            this.button_交代者決定.Name = "button_交代者決定";
            this.button_交代者決定.Size = new System.Drawing.Size(75, 23);
            this.button_交代者決定.TabIndex = 0;
            this.button_交代者決定.Text = "交代者決定";
            this.button_交代者決定.UseVisualStyleBackColor = true;
            this.button_交代者決定.Click += new System.EventHandler(this.button_交代者決定_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 303);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 21;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(880, 529);
            this.dataGridView2.TabIndex = 2;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button乗船者_検索条件クリア);
            this.groupBox2.Controls.Add(this.button乗船者検索);
            this.groupBox2.Controls.Add(this.checkedListBox種別);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.button経験);
            this.groupBox2.Controls.Add(this.checkBox乗船者_経験);
            this.groupBox2.Controls.Add(this.button乗り合わせ);
            this.groupBox2.Controls.Add(this.checkBox乗船者_乗り合わせ);
            this.groupBox2.Controls.Add(this.button乗船者_個人予定);
            this.groupBox2.Controls.Add(this.checkBox乗船者_個人予定);
            this.groupBox2.Controls.Add(this.button講習);
            this.groupBox2.Controls.Add(this.checkBox講習);
            this.groupBox2.Controls.Add(this.checkBox休暇日数);
            this.groupBox2.Controls.Add(this.checkBox乗船者_職名);
            this.groupBox2.Controls.Add(this.button免許免状);
            this.groupBox2.Controls.Add(this.checkBox乗船者_船);
            this.groupBox2.Controls.Add(this.checkBox免許免状);
            this.groupBox2.Controls.Add(this.textBox休暇日数);
            this.groupBox2.Controls.Add(this.comboBox乗船者_職名);
            this.groupBox2.Controls.Add(this.comboBox乗船者_船);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(880, 294);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "乗船者選択";
            // 
            // button乗船者_検索条件クリア
            // 
            this.button乗船者_検索条件クリア.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button乗船者_検索条件クリア.BackColor = System.Drawing.SystemColors.Control;
            this.button乗船者_検索条件クリア.Location = new System.Drawing.Point(754, 65);
            this.button乗船者_検索条件クリア.Name = "button乗船者_検索条件クリア";
            this.button乗船者_検索条件クリア.Size = new System.Drawing.Size(93, 23);
            this.button乗船者_検索条件クリア.TabIndex = 43;
            this.button乗船者_検索条件クリア.Text = "検索条件クリア";
            this.button乗船者_検索条件クリア.UseVisualStyleBackColor = false;
            // 
            // button乗船者検索
            // 
            this.button乗船者検索.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button乗船者検索.BackColor = System.Drawing.SystemColors.Control;
            this.button乗船者検索.Location = new System.Drawing.Point(754, 33);
            this.button乗船者検索.Name = "button乗船者検索";
            this.button乗船者検索.Size = new System.Drawing.Size(93, 23);
            this.button乗船者検索.TabIndex = 28;
            this.button乗船者検索.Text = "検索";
            this.button乗船者検索.UseVisualStyleBackColor = false;
            this.button乗船者検索.Click += new System.EventHandler(this.button乗船者検索_Click);
            // 
            // checkedListBox種別
            // 
            this.checkedListBox種別.CheckOnClick = true;
            this.checkedListBox種別.FormattingEnabled = true;
            this.checkedListBox種別.Location = new System.Drawing.Point(366, 39);
            this.checkedListBox種別.Name = "checkedListBox種別";
            this.checkedListBox種別.Size = new System.Drawing.Size(184, 228);
            this.checkedListBox種別.TabIndex = 41;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(331, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 42;
            this.label8.Text = "種別";
            // 
            // button経験
            // 
            this.button経験.Location = new System.Drawing.Point(118, 231);
            this.button経験.Name = "button経験";
            this.button経験.Size = new System.Drawing.Size(75, 23);
            this.button経験.TabIndex = 40;
            this.button経験.Text = "詳細";
            this.button経験.UseVisualStyleBackColor = true;
            this.button経験.Click += new System.EventHandler(this.button経験_Click);
            // 
            // checkBox乗船者_経験
            // 
            this.checkBox乗船者_経験.AutoSize = true;
            this.checkBox乗船者_経験.Location = new System.Drawing.Point(28, 237);
            this.checkBox乗船者_経験.Name = "checkBox乗船者_経験";
            this.checkBox乗船者_経験.Size = new System.Drawing.Size(48, 16);
            this.checkBox乗船者_経験.TabIndex = 39;
            this.checkBox乗船者_経験.Text = "経験";
            this.checkBox乗船者_経験.UseVisualStyleBackColor = true;
            // 
            // button乗り合わせ
            // 
            this.button乗り合わせ.Location = new System.Drawing.Point(118, 205);
            this.button乗り合わせ.Name = "button乗り合わせ";
            this.button乗り合わせ.Size = new System.Drawing.Size(75, 23);
            this.button乗り合わせ.TabIndex = 38;
            this.button乗り合わせ.Text = "詳細";
            this.button乗り合わせ.UseVisualStyleBackColor = true;
            this.button乗り合わせ.Click += new System.EventHandler(this.button乗り合わせ_Click);
            // 
            // checkBox乗船者_乗り合わせ
            // 
            this.checkBox乗船者_乗り合わせ.AutoSize = true;
            this.checkBox乗船者_乗り合わせ.Location = new System.Drawing.Point(28, 210);
            this.checkBox乗船者_乗り合わせ.Name = "checkBox乗船者_乗り合わせ";
            this.checkBox乗船者_乗り合わせ.Size = new System.Drawing.Size(76, 16);
            this.checkBox乗船者_乗り合わせ.TabIndex = 37;
            this.checkBox乗船者_乗り合わせ.Text = "乗り合わせ";
            this.checkBox乗船者_乗り合わせ.UseVisualStyleBackColor = true;
            // 
            // button乗船者_個人予定
            // 
            this.button乗船者_個人予定.Location = new System.Drawing.Point(118, 179);
            this.button乗船者_個人予定.Name = "button乗船者_個人予定";
            this.button乗船者_個人予定.Size = new System.Drawing.Size(75, 23);
            this.button乗船者_個人予定.TabIndex = 36;
            this.button乗船者_個人予定.Text = "詳細";
            this.button乗船者_個人予定.UseVisualStyleBackColor = true;
            this.button乗船者_個人予定.Click += new System.EventHandler(this.button個人予定_Click);
            // 
            // checkBox乗船者_個人予定
            // 
            this.checkBox乗船者_個人予定.AutoSize = true;
            this.checkBox乗船者_個人予定.Location = new System.Drawing.Point(28, 183);
            this.checkBox乗船者_個人予定.Name = "checkBox乗船者_個人予定";
            this.checkBox乗船者_個人予定.Size = new System.Drawing.Size(72, 16);
            this.checkBox乗船者_個人予定.TabIndex = 35;
            this.checkBox乗船者_個人予定.Text = "個人予定";
            this.checkBox乗船者_個人予定.UseVisualStyleBackColor = true;
            // 
            // button講習
            // 
            this.button講習.Location = new System.Drawing.Point(118, 153);
            this.button講習.Name = "button講習";
            this.button講習.Size = new System.Drawing.Size(75, 23);
            this.button講習.TabIndex = 34;
            this.button講習.Text = "詳細";
            this.button講習.UseVisualStyleBackColor = true;
            this.button講習.Click += new System.EventHandler(this.button講習_Click);
            // 
            // checkBox講習
            // 
            this.checkBox講習.AutoSize = true;
            this.checkBox講習.Location = new System.Drawing.Point(28, 156);
            this.checkBox講習.Name = "checkBox講習";
            this.checkBox講習.Size = new System.Drawing.Size(48, 16);
            this.checkBox講習.TabIndex = 33;
            this.checkBox講習.Text = "講習";
            this.checkBox講習.UseVisualStyleBackColor = true;
            // 
            // checkBox休暇日数
            // 
            this.checkBox休暇日数.AutoSize = true;
            this.checkBox休暇日数.Location = new System.Drawing.Point(28, 92);
            this.checkBox休暇日数.Name = "checkBox休暇日数";
            this.checkBox休暇日数.Size = new System.Drawing.Size(72, 16);
            this.checkBox休暇日数.TabIndex = 32;
            this.checkBox休暇日数.Text = "休暇日数";
            this.checkBox休暇日数.UseVisualStyleBackColor = true;
            // 
            // checkBox乗船者_職名
            // 
            this.checkBox乗船者_職名.AutoSize = true;
            this.checkBox乗船者_職名.Location = new System.Drawing.Point(28, 66);
            this.checkBox乗船者_職名.Name = "checkBox乗船者_職名";
            this.checkBox乗船者_職名.Size = new System.Drawing.Size(72, 16);
            this.checkBox乗船者_職名.TabIndex = 31;
            this.checkBox乗船者_職名.Text = "対象職名";
            this.checkBox乗船者_職名.UseVisualStyleBackColor = true;
            // 
            // button免許免状
            // 
            this.button免許免状.Location = new System.Drawing.Point(118, 127);
            this.button免許免状.Name = "button免許免状";
            this.button免許免状.Size = new System.Drawing.Size(75, 23);
            this.button免許免状.TabIndex = 30;
            this.button免許免状.Text = "詳細";
            this.button免許免状.UseVisualStyleBackColor = true;
            this.button免許免状.Click += new System.EventHandler(this.button免許免状_Click);
            // 
            // checkBox乗船者_船
            // 
            this.checkBox乗船者_船.AutoSize = true;
            this.checkBox乗船者_船.Location = new System.Drawing.Point(28, 40);
            this.checkBox乗船者_船.Name = "checkBox乗船者_船";
            this.checkBox乗船者_船.Size = new System.Drawing.Size(72, 16);
            this.checkBox乗船者_船.TabIndex = 29;
            this.checkBox乗船者_船.Text = "対象船名";
            this.checkBox乗船者_船.UseVisualStyleBackColor = true;
            // 
            // checkBox免許免状
            // 
            this.checkBox免許免状.AutoSize = true;
            this.checkBox免許免状.Location = new System.Drawing.Point(28, 129);
            this.checkBox免許免状.Name = "checkBox免許免状";
            this.checkBox免許免状.Size = new System.Drawing.Size(84, 16);
            this.checkBox免許免状.TabIndex = 29;
            this.checkBox免許免状.Text = "免許／免状";
            this.checkBox免許免状.UseVisualStyleBackColor = true;
            // 
            // textBox休暇日数
            // 
            this.textBox休暇日数.Location = new System.Drawing.Point(118, 90);
            this.textBox休暇日数.Name = "textBox休暇日数";
            this.textBox休暇日数.Size = new System.Drawing.Size(73, 19);
            this.textBox休暇日数.TabIndex = 27;
            // 
            // comboBox乗船者_職名
            // 
            this.comboBox乗船者_職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox乗船者_職名.FormattingEnabled = true;
            this.comboBox乗船者_職名.Location = new System.Drawing.Point(118, 64);
            this.comboBox乗船者_職名.Name = "comboBox乗船者_職名";
            this.comboBox乗船者_職名.Size = new System.Drawing.Size(179, 20);
            this.comboBox乗船者_職名.TabIndex = 22;
            // 
            // comboBox乗船者_船
            // 
            this.comboBox乗船者_船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox乗船者_船.FormattingEnabled = true;
            this.comboBox乗船者_船.Location = new System.Drawing.Point(118, 39);
            this.comboBox乗船者_船.Name = "comboBox乗船者_船";
            this.comboBox乗船者_船.Size = new System.Drawing.Size(179, 20);
            this.comboBox乗船者_船.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(197, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "日以上";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 225F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(886, 935);
            this.tableLayoutPanel2.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button下船者_検索条件クリア);
            this.groupBox1.Controls.Add(this.button下船者_個人予定);
            this.groupBox1.Controls.Add(this.checkBox下船者_個人予定);
            this.groupBox1.Controls.Add(this.button下船者検索);
            this.groupBox1.Controls.Add(this.textBox乗船日数);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dateTimePicker基準日);
            this.groupBox1.Controls.Add(this.comboBox下船者_職名);
            this.groupBox1.Controls.Add(this.comboBox下船者_船);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(880, 219);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "下船者選択";
            // 
            // button下船者_検索条件クリア
            // 
            this.button下船者_検索条件クリア.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button下船者_検索条件クリア.BackColor = System.Drawing.SystemColors.Control;
            this.button下船者_検索条件クリア.Location = new System.Drawing.Point(746, 59);
            this.button下船者_検索条件クリア.Name = "button下船者_検索条件クリア";
            this.button下船者_検索条件クリア.Size = new System.Drawing.Size(93, 23);
            this.button下船者_検索条件クリア.TabIndex = 29;
            this.button下船者_検索条件クリア.Text = "検索条件クリア";
            this.button下船者_検索条件クリア.UseVisualStyleBackColor = false;
            // 
            // button下船者_個人予定
            // 
            this.button下船者_個人予定.Location = new System.Drawing.Point(103, 158);
            this.button下船者_個人予定.Name = "button下船者_個人予定";
            this.button下船者_個人予定.Size = new System.Drawing.Size(75, 23);
            this.button下船者_個人予定.TabIndex = 30;
            this.button下船者_個人予定.Text = "詳細";
            this.button下船者_個人予定.UseVisualStyleBackColor = true;
            this.button下船者_個人予定.Click += new System.EventHandler(this.button個人予定_Click);
            // 
            // checkBox下船者_個人予定
            // 
            this.checkBox下船者_個人予定.AutoSize = true;
            this.checkBox下船者_個人予定.Location = new System.Drawing.Point(25, 162);
            this.checkBox下船者_個人予定.Name = "checkBox下船者_個人予定";
            this.checkBox下船者_個人予定.Size = new System.Drawing.Size(72, 16);
            this.checkBox下船者_個人予定.TabIndex = 29;
            this.checkBox下船者_個人予定.Text = "個人予定";
            this.checkBox下船者_個人予定.UseVisualStyleBackColor = true;
            // 
            // button下船者検索
            // 
            this.button下船者検索.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button下船者検索.BackColor = System.Drawing.SystemColors.Control;
            this.button下船者検索.Location = new System.Drawing.Point(746, 29);
            this.button下船者検索.Name = "button下船者検索";
            this.button下船者検索.Size = new System.Drawing.Size(93, 23);
            this.button下船者検索.TabIndex = 28;
            this.button下船者検索.Text = "検索";
            this.button下船者検索.UseVisualStyleBackColor = false;
            this.button下船者検索.Click += new System.EventHandler(this.button下船者検索_Click);
            // 
            // textBox乗船日数
            // 
            this.textBox乗船日数.Location = new System.Drawing.Point(103, 129);
            this.textBox乗船日数.Name = "textBox乗船日数";
            this.textBox乗船日数.Size = new System.Drawing.Size(73, 19);
            this.textBox乗船日数.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "基準日";
            // 
            // dateTimePicker基準日
            // 
            this.dateTimePicker基準日.Location = new System.Drawing.Point(103, 37);
            this.dateTimePicker基準日.Name = "dateTimePicker基準日";
            this.dateTimePicker基準日.Size = new System.Drawing.Size(152, 19);
            this.dateTimePicker基準日.TabIndex = 25;
            // 
            // comboBox下船者_職名
            // 
            this.comboBox下船者_職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox下船者_職名.FormattingEnabled = true;
            this.comboBox下船者_職名.Location = new System.Drawing.Point(103, 98);
            this.comboBox下船者_職名.Name = "comboBox下船者_職名";
            this.comboBox下船者_職名.Size = new System.Drawing.Size(150, 20);
            this.comboBox下船者_職名.TabIndex = 22;
            // 
            // comboBox下船者_船
            // 
            this.comboBox下船者_船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox下船者_船.FormattingEnabled = true;
            this.comboBox下船者_船.Location = new System.Drawing.Point(103, 67);
            this.comboBox下船者_船.Name = "comboBox下船者_船";
            this.comboBox下船者_船.Size = new System.Drawing.Size(179, 20);
            this.comboBox下船者_船.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(182, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 24;
            this.label3.Text = "日以上";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "乗船日数";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(44, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 24;
            this.label10.Text = "職名";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 23;
            this.label7.Text = "船名";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 228);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(880, 604);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_交代者解除);
            this.panel1.Controls.Add(this.button_船員交代予定表);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 838);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(880, 94);
            this.panel1.TabIndex = 2;
            // 
            // button_交代者解除
            // 
            this.button_交代者解除.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_交代者解除.Location = new System.Drawing.Point(782, 36);
            this.button_交代者解除.Name = "button_交代者解除";
            this.button_交代者解除.Size = new System.Drawing.Size(75, 23);
            this.button_交代者解除.TabIndex = 0;
            this.button_交代者解除.Text = "交代者解除";
            this.button_交代者解除.UseVisualStyleBackColor = true;
            this.button_交代者解除.Click += new System.EventHandler(this.button_交代者解除_Click);
            // 
            // button_船員交代予定表
            // 
            this.button_船員交代予定表.Location = new System.Drawing.Point(25, 36);
            this.button_船員交代予定表.Name = "button_船員交代予定表";
            this.button_船員交代予定表.Size = new System.Drawing.Size(75, 23);
            this.button_船員交代予定表.TabIndex = 0;
            this.button_船員交代予定表.Text = "Excel出力";
            this.button_船員交代予定表.UseVisualStyleBackColor = true;
            this.button_船員交代予定表.Click += new System.EventHandler(this.button_船員交代予定表_Click);
            // 
            // 配乗シミュレーションForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1784, 941);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "配乗シミュレーションForm";
            this.Text = "配乗シミュレーションForm";
            this.Load += new System.EventHandler(this.配乗シミュレーションForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox乗船日数;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker基準日;
        private System.Windows.Forms.ComboBox comboBox下船者_職名;
        private System.Windows.Forms.ComboBox comboBox下船者_船;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button下船者検索;
        private System.Windows.Forms.Button button下船者_個人予定;
        private System.Windows.Forms.CheckBox checkBox下船者_個人予定;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox checkedListBox種別;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button経験;
        private System.Windows.Forms.CheckBox checkBox乗船者_経験;
        private System.Windows.Forms.Button button乗り合わせ;
        private System.Windows.Forms.CheckBox checkBox乗船者_乗り合わせ;
        private System.Windows.Forms.Button button乗船者_個人予定;
        private System.Windows.Forms.CheckBox checkBox乗船者_個人予定;
        private System.Windows.Forms.Button button講習;
        private System.Windows.Forms.CheckBox checkBox講習;
        private System.Windows.Forms.CheckBox checkBox休暇日数;
        private System.Windows.Forms.CheckBox checkBox乗船者_職名;
        private System.Windows.Forms.Button button免許免状;
        private System.Windows.Forms.CheckBox checkBox乗船者_船;
        private System.Windows.Forms.CheckBox checkBox免許免状;
        private System.Windows.Forms.Button button乗船者検索;
        private System.Windows.Forms.TextBox textBox休暇日数;
        private System.Windows.Forms.ComboBox comboBox乗船者_職名;
        private System.Windows.Forms.ComboBox comboBox乗船者_船;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker_乗船予定日;
        private System.Windows.Forms.Button button_交代者決定;
        private System.Windows.Forms.Button button_交代者解除;
        private System.Windows.Forms.Button button_船員交代予定表;
        private System.Windows.Forms.Button button乗船者_検索条件クリア;
        private System.Windows.Forms.Button button下船者_検索条件クリア;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

    }
}