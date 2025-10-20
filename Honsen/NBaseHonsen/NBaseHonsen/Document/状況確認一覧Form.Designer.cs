namespace NBaseHonsen.Document
{
    partial class 状況確認一覧Form
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
            this.components = new System.ComponentModel.Container();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_BunshoName = new System.Windows.Forms.TextBox();
            this.comboBox_Shoubunrui = new System.Windows.Forms.ComboBox();
            this.comboBox_Bunrui = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_BunshoNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker_IssueDate1 = new NBaseUtil.NullableDateTimePicker();
            this.Clear_button = new System.Windows.Forms.Button();
            this.Search_button = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker_IssueDate2 = new NBaseUtil.NullableDateTimePicker();
            this.comboBox_Publisher = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.comboBox_Status = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.checkBox_Status = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.button_コメント登録 = new System.Windows.Forms.Button();
            this.button_内容確認 = new System.Windows.Forms.Button();
            this.button_確認状況一覧 = new System.Windows.Forms.Button();
            this.button_文書同期情報 = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_回覧状況 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_Keyword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_Bikou = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(38, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "文書名";
            // 
            // textBox_BunshoName
            // 
            this.textBox_BunshoName.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_BunshoName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_BunshoName.Location = new System.Drawing.Point(116, 99);
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
            this.comboBox_Shoubunrui.Location = new System.Drawing.Point(116, 40);
            this.comboBox_Shoubunrui.Name = "comboBox_Shoubunrui";
            this.comboBox_Shoubunrui.Size = new System.Drawing.Size(425, 24);
            this.comboBox_Shoubunrui.TabIndex = 2;
            // 
            // comboBox_Bunrui
            // 
            this.comboBox_Bunrui.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Bunrui.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox_Bunrui.FormattingEnabled = true;
            this.comboBox_Bunrui.Location = new System.Drawing.Point(116, 10);
            this.comboBox_Bunrui.Name = "comboBox_Bunrui";
            this.comboBox_Bunrui.Size = new System.Drawing.Size(425, 24);
            this.comboBox_Bunrui.TabIndex = 1;
            this.comboBox_Bunrui.SelectedIndexChanged += new System.EventHandler(this.comboBox_Bunrui_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(38, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "文書番号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(38, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "小分類";
            // 
            // textBox_BunshoNo
            // 
            this.textBox_BunshoNo.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_BunshoNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_BunshoNo.Location = new System.Drawing.Point(116, 70);
            this.textBox_BunshoNo.MaxLength = 15;
            this.textBox_BunshoNo.Name = "textBox_BunshoNo";
            this.textBox_BunshoNo.Size = new System.Drawing.Size(200, 23);
            this.textBox_BunshoNo.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(38, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "分類";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label20.Location = new System.Drawing.Point(563, 13);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 16);
            this.label20.TabIndex = 27;
            this.label20.Text = "発行日";
            // 
            // nullableDateTimePicker_IssueDate1
            // 
            this.nullableDateTimePicker_IssueDate1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nullableDateTimePicker_IssueDate1.Location = new System.Drawing.Point(655, 10);
            this.nullableDateTimePicker_IssueDate1.Name = "nullableDateTimePicker_IssueDate1";
            this.nullableDateTimePicker_IssueDate1.Size = new System.Drawing.Size(155, 23);
            this.nullableDateTimePicker_IssueDate1.TabIndex = 7;
            this.nullableDateTimePicker_IssueDate1.Value = new System.DateTime(2010, 8, 23, 2, 9, 20, 546);
            // 
            // Clear_button
            // 
            this.Clear_button.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Clear_button.Location = new System.Drawing.Point(1125, 42);
            this.Clear_button.Name = "Clear_button";
            this.Clear_button.Size = new System.Drawing.Size(120, 25);
            this.Clear_button.TabIndex = 12;
            this.Clear_button.Text = "検索条件クリア";
            this.Clear_button.UseVisualStyleBackColor = true;
            this.Clear_button.Click += new System.EventHandler(this.Clear_button_Click);
            // 
            // Search_button
            // 
            this.Search_button.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Search_button.Location = new System.Drawing.Point(1125, 11);
            this.Search_button.Name = "Search_button";
            this.Search_button.Size = new System.Drawing.Size(120, 25);
            this.Search_button.TabIndex = 11;
            this.Search_button.Text = "検索";
            this.Search_button.UseVisualStyleBackColor = true;
            this.Search_button.Click += new System.EventHandler(this.Search_button_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label18.Location = new System.Drawing.Point(814, 17);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(24, 16);
            this.label18.TabIndex = 30;
            this.label18.Text = "～";
            // 
            // nullableDateTimePicker_IssueDate2
            // 
            this.nullableDateTimePicker_IssueDate2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nullableDateTimePicker_IssueDate2.Location = new System.Drawing.Point(840, 11);
            this.nullableDateTimePicker_IssueDate2.Name = "nullableDateTimePicker_IssueDate2";
            this.nullableDateTimePicker_IssueDate2.Size = new System.Drawing.Size(155, 23);
            this.nullableDateTimePicker_IssueDate2.TabIndex = 8;
            this.nullableDateTimePicker_IssueDate2.Value = new System.DateTime(2010, 8, 23, 2, 9, 20, 546);
            // 
            // comboBox_Publisher
            // 
            this.comboBox_Publisher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Publisher.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox_Publisher.FormattingEnabled = true;
            this.comboBox_Publisher.Location = new System.Drawing.Point(655, 40);
            this.comboBox_Publisher.Name = "comboBox_Publisher";
            this.comboBox_Publisher.Size = new System.Drawing.Size(254, 24);
            this.comboBox_Publisher.TabIndex = 9;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label19.Location = new System.Drawing.Point(563, 43);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(72, 16);
            this.label19.TabIndex = 32;
            this.label19.Text = "発行部署";
            // 
            // comboBox_Status
            // 
            this.comboBox_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Status.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox_Status.FormattingEnabled = true;
            this.comboBox_Status.Location = new System.Drawing.Point(655, 69);
            this.comboBox_Status.Name = "comboBox_Status";
            this.comboBox_Status.Size = new System.Drawing.Size(254, 24);
            this.comboBox_Status.TabIndex = 10;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label21.Location = new System.Drawing.Point(563, 72);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(72, 16);
            this.label21.TabIndex = 34;
            this.label21.Text = "確認状況";
            // 
            // checkBox_Status
            // 
            this.checkBox_Status.AutoSize = true;
            this.checkBox_Status.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_Status.Location = new System.Drawing.Point(985, 71);
            this.checkBox_Status.Name = "checkBox_Status";
            this.checkBox_Status.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Status.TabIndex = 10;
            this.checkBox_Status.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label22.Location = new System.Drawing.Point(939, 69);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(40, 16);
            this.label22.TabIndex = 37;
            this.label22.Text = "完了";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel7.ColumnCount = 9;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // button_コメント登録
            // 
            this.button_コメント登録.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_コメント登録.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_コメント登録.Location = new System.Drawing.Point(713, 156);
            this.button_コメント登録.Name = "button_コメント登録";
            this.button_コメント登録.Size = new System.Drawing.Size(120, 25);
            this.button_コメント登録.TabIndex = 13;
            this.button_コメント登録.Text = "コメント登録";
            this.button_コメント登録.UseVisualStyleBackColor = true;
            this.button_コメント登録.Click += new System.EventHandler(this.button_コメント登録_Click);
            // 
            // button_内容確認
            // 
            this.button_内容確認.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_内容確認.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_内容確認.Location = new System.Drawing.Point(839, 156);
            this.button_内容確認.Name = "button_内容確認";
            this.button_内容確認.Size = new System.Drawing.Size(120, 25);
            this.button_内容確認.TabIndex = 14;
            this.button_内容確認.Text = "内容確認";
            this.button_内容確認.UseVisualStyleBackColor = true;
            this.button_内容確認.Click += new System.EventHandler(this.button_内容確認_Click);
            // 
            // button_確認状況一覧
            // 
            this.button_確認状況一覧.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_確認状況一覧.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_確認状況一覧.Location = new System.Drawing.Point(1091, 156);
            this.button_確認状況一覧.Name = "button_確認状況一覧";
            this.button_確認状況一覧.Size = new System.Drawing.Size(120, 25);
            this.button_確認状況一覧.TabIndex = 16;
            this.button_確認状況一覧.Text = "確認状況一覧";
            this.button_確認状況一覧.UseVisualStyleBackColor = true;
            this.button_確認状況一覧.Click += new System.EventHandler(this.button_確認状況一覧_Click);
            // 
            // button_文書同期情報
            // 
            this.button_文書同期情報.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_文書同期情報.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_文書同期情報.Location = new System.Drawing.Point(1217, 156);
            this.button_文書同期情報.Name = "button_文書同期情報";
            this.button_文書同期情報.Size = new System.Drawing.Size(120, 25);
            this.button_文書同期情報.TabIndex = 17;
            this.button_文書同期情報.Text = "文書同期情報";
            this.button_文書同期情報.UseVisualStyleBackColor = true;
            this.button_文書同期情報.Click += new System.EventHandler(this.button_文書同期情報_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label25.Location = new System.Drawing.Point(11, 14);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(24, 16);
            this.label25.TabIndex = 0;
            this.label25.Text = "※";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(15, 199);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(15, 3, 15, 15);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1326, 531);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // button_回覧状況
            // 
            this.button_回覧状況.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_回覧状況.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_回覧状況.Location = new System.Drawing.Point(965, 156);
            this.button_回覧状況.Name = "button_回覧状況";
            this.button_回覧状況.Size = new System.Drawing.Size(120, 25);
            this.button_回覧状況.TabIndex = 15;
            this.button_回覧状況.Text = "回覧状況";
            this.button_回覧状況.UseVisualStyleBackColor = true;
            this.button_回覧状況.Click += new System.EventHandler(this.button_回覧状況_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 196F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1356, 745);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox_Keyword);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBox_Bikou);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button_回覧状況);
            this.panel1.Controls.Add(this.textBox_BunshoNo);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button_文書同期情報);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button_確認状況一覧);
            this.panel1.Controls.Add(this.comboBox_Bunrui);
            this.panel1.Controls.Add(this.button_内容確認);
            this.panel1.Controls.Add(this.comboBox_Shoubunrui);
            this.panel1.Controls.Add(this.button_コメント登録);
            this.panel1.Controls.Add(this.textBox_BunshoName);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.checkBox_Status);
            this.panel1.Controls.Add(this.nullableDateTimePicker_IssueDate1);
            this.panel1.Controls.Add(this.comboBox_Status);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.Search_button);
            this.panel1.Controls.Add(this.comboBox_Publisher);
            this.panel1.Controls.Add(this.Clear_button);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.nullableDateTimePicker_IssueDate2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1350, 190);
            this.panel1.TabIndex = 0;
            // 
            // textBox_Keyword
            // 
            this.textBox_Keyword.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Keyword.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_Keyword.Location = new System.Drawing.Point(116, 157);
            this.textBox_Keyword.MaxLength = 100;
            this.textBox_Keyword.Name = "textBox_Keyword";
            this.textBox_Keyword.Size = new System.Drawing.Size(425, 23);
            this.textBox_Keyword.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(38, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 39;
            this.label6.Text = "キーワード";
            // 
            // textBox_Bikou
            // 
            this.textBox_Bikou.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_Bikou.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_Bikou.Location = new System.Drawing.Point(116, 128);
            this.textBox_Bikou.MaxLength = 100;
            this.textBox_Bikou.Name = "textBox_Bikou";
            this.textBox_Bikou.Size = new System.Drawing.Size(425, 23);
            this.textBox_Bikou.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(38, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 39;
            this.label5.Text = "備考";
            // 
            // 状況確認一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1356, 745);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "状況確認一覧Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "状況確認一覧";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.状況確認一覧Form_FormClosed);
            this.Load += new System.EventHandler(this.状況確認一覧Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_BunshoNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Bunrui;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_BunshoName;
        private System.Windows.Forms.ComboBox comboBox_Shoubunrui;
        private System.Windows.Forms.Label label20;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker_IssueDate1;
        private System.Windows.Forms.Button Clear_button;
        private System.Windows.Forms.Button Search_button;
        private System.Windows.Forms.Label label18;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker_IssueDate2;
        private System.Windows.Forms.ComboBox comboBox_Publisher;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox comboBox_Status;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox checkBox_Status;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button button_コメント登録;
        private System.Windows.Forms.Button button_内容確認;
        private System.Windows.Forms.Button button_確認状況一覧;
        private System.Windows.Forms.Button button_文書同期情報;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_回覧状況;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_Bikou;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_Keyword;
        private System.Windows.Forms.Label label6;
    }
}