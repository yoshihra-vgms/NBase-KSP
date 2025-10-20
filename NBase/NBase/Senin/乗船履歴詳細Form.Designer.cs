namespace Senin
{
    partial class 乗船履歴詳細Form
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
            this.checkBox転船 = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBox船名 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox会社名 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.comboBoxCrewMatrixType = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.checkBox兼務通信長 = new System.Windows.Forms.CheckBox();
            this.panel_乗船 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button乗船場所クリア = new System.Windows.Forms.Button();
            this.button乗船場所 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox乗船場所 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button交代者クリア = new System.Windows.Forms.Button();
            this.button交代者検索 = new System.Windows.Forms.Button();
            this.textBox交代者 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox下船場所 = new System.Windows.Forms.TextBox();
            this.button下船場所クリア = new System.Windows.Forms.Button();
            this.button下船場所 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBoxSignOffReason = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.comboBox_NavigationArea = new System.Windows.Forms.ComboBox();
            this.comboBox船タイプ = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxGRT = new System.Windows.Forms.TextBox();
            this.textBoxオーナー = new System.Windows.Forms.TextBox();
            this.comboBox職名 = new System.Windows.Forms.ComboBox();
            this.nullableDateTimePicker兼務通信長開始 = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker兼務通信長終了 = new NBaseUtil.NullableDateTimePicker();
            this.radioButton労働区分乗船_労働 = new System.Windows.Forms.RadioButton();
            this.radioButton労働区分乗船_半休 = new System.Windows.Forms.RadioButton();
            this.radioButton労働区分乗船_全休 = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel労働区分乗船 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel労働区分下船 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButton労働区分下船_労働 = new System.Windows.Forms.RadioButton();
            this.radioButton労働区分下船_半休 = new System.Windows.Forms.RadioButton();
            this.radioButton労働区分下船_全休 = new System.Windows.Forms.RadioButton();
            this.comboBox種別詳細 = new System.Windows.Forms.ComboBox();
            this.panel事務所 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.panel日程 = new System.Windows.Forms.Panel();
            this.nullableDateTimePicker開始 = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker終了 = new NBaseUtil.NullableDateTimePicker();
            this.button役職変更 = new System.Windows.Forms.Button();
            this.panel_乗船.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.flowLayoutPanel労働区分乗船.SuspendLayout();
            this.flowLayoutPanel労働区分下船.SuspendLayout();
            this.panel事務所.SuspendLayout();
            this.panel日程.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "※開始日";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "終了日";
            // 
            // comboBox種別
            // 
            this.comboBox種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox種別.FormattingEnabled = true;
            this.comboBox種別.Location = new System.Drawing.Point(100, 12);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(216, 21);
            this.comboBox種別.TabIndex = 1;
            this.comboBox種別.SelectedIndexChanged += new System.EventHandler(this.comboBox種別_SelectedIndexChanged);
            this.comboBox種別.SelectionChangeCommitted += new System.EventHandler(this.comboBox種別_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "※種別";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "日数";
            // 
            // textBox日数
            // 
            this.textBox日数.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox日数.Location = new System.Drawing.Point(91, 59);
            this.textBox日数.Name = "textBox日数";
            this.textBox日数.ReadOnly = true;
            this.textBox日数.Size = new System.Drawing.Size(39, 20);
            this.textBox日数.TabIndex = 7;
            this.textBox日数.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(136, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "日";
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button更新.Location = new System.Drawing.Point(141, 510);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 10;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button閉じる.Location = new System.Drawing.Point(304, 510);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 12;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button削除.Location = new System.Drawing.Point(223, 510);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 11;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(85, 3);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(162, 21);
            this.comboBox船.TabIndex = 1;
            this.comboBox船.SelectedIndexChanged += new System.EventHandler(this.comboBox船_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "船";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(268, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "職名";
            // 
            // checkedListBox職名
            // 
            this.checkedListBox職名.CheckOnClick = true;
            this.checkedListBox職名.FormattingEnabled = true;
            this.checkedListBox職名.Location = new System.Drawing.Point(433, 35);
            this.checkedListBox職名.Name = "checkedListBox職名";
            this.checkedListBox職名.Size = new System.Drawing.Size(85, 34);
            this.checkedListBox職名.TabIndex = 8;
            this.checkedListBox職名.Visible = false;
            // 
            // checkBox転船
            // 
            this.checkBox転船.AutoSize = true;
            this.checkBox転船.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.checkBox転船.Location = new System.Drawing.Point(101, 39);
            this.checkBox転船.Name = "checkBox転船";
            this.checkBox転船.Size = new System.Drawing.Size(215, 17);
            this.checkBox転船.TabIndex = 10;
            this.checkBox転船.Text = "同日転船の場合、チェックしてください";
            this.checkBox転船.UseVisualStyleBackColor = false;
            this.checkBox転船.Visible = false;
            this.checkBox転船.CheckedChanged += new System.EventHandler(this.checkBox転船_CheckedChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 38);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(20, 13);
            this.label19.TabIndex = 24;
            this.label19.Text = "船";
            // 
            // textBox船名
            // 
            this.textBox船名.Location = new System.Drawing.Point(85, 35);
            this.textBox船名.MaxLength = 30;
            this.textBox船名.Name = "textBox船名";
            this.textBox船名.Size = new System.Drawing.Size(162, 20);
            this.textBox船名.TabIndex = 2;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(9, 65);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(46, 13);
            this.label21.TabIndex = 25;
            this.label21.Text = "会社名";
            // 
            // textBox会社名
            // 
            this.textBox会社名.Location = new System.Drawing.Point(85, 62);
            this.textBox会社名.MaxLength = 100;
            this.textBox会社名.Name = "textBox会社名";
            this.textBox会社名.Size = new System.Drawing.Size(162, 20);
            this.textBox会社名.TabIndex = 3;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(9, 91);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(71, 13);
            this.label22.TabIndex = 26;
            this.label22.Text = "CrewMatrix";
            // 
            // comboBoxCrewMatrixType
            // 
            this.comboBoxCrewMatrixType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCrewMatrixType.FormattingEnabled = true;
            this.comboBoxCrewMatrixType.Location = new System.Drawing.Point(85, 88);
            this.comboBoxCrewMatrixType.Name = "comboBoxCrewMatrixType";
            this.comboBoxCrewMatrixType.Size = new System.Drawing.Size(162, 21);
            this.comboBoxCrewMatrixType.TabIndex = 4;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(261, 154);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(20, 13);
            this.label32.TabIndex = 30;
            this.label32.Text = "～";
            // 
            // checkBox兼務通信長
            // 
            this.checkBox兼務通信長.AutoSize = true;
            this.checkBox兼務通信長.Location = new System.Drawing.Point(27, 154);
            this.checkBox兼務通信長.Name = "checkBox兼務通信長";
            this.checkBox兼務通信長.Size = new System.Drawing.Size(91, 17);
            this.checkBox兼務通信長.TabIndex = 6;
            this.checkBox兼務通信長.Text = "兼務通信長";
            this.checkBox兼務通信長.UseVisualStyleBackColor = true;
            // 
            // panel_乗船
            // 
            this.panel_乗船.Controls.Add(this.tabControl1);
            this.panel_乗船.Controls.Add(this.label45);
            this.panel_乗船.Controls.Add(this.comboBox_NavigationArea);
            this.panel_乗船.Controls.Add(this.comboBox船タイプ);
            this.panel_乗船.Controls.Add(this.label12);
            this.panel_乗船.Controls.Add(this.label11);
            this.panel_乗船.Controls.Add(this.label13);
            this.panel_乗船.Controls.Add(this.textBoxGRT);
            this.panel_乗船.Controls.Add(this.textBoxオーナー);
            this.panel_乗船.Controls.Add(this.comboBox職名);
            this.panel_乗船.Controls.Add(this.checkBox兼務通信長);
            this.panel_乗船.Controls.Add(this.label19);
            this.panel_乗船.Controls.Add(this.label32);
            this.panel_乗船.Controls.Add(this.textBox船名);
            this.panel_乗船.Controls.Add(this.nullableDateTimePicker兼務通信長開始);
            this.panel_乗船.Controls.Add(this.label21);
            this.panel_乗船.Controls.Add(this.nullableDateTimePicker兼務通信長終了);
            this.panel_乗船.Controls.Add(this.textBox会社名);
            this.panel_乗船.Controls.Add(this.checkedListBox職名);
            this.panel_乗船.Controls.Add(this.comboBox船);
            this.panel_乗船.Controls.Add(this.label22);
            this.panel_乗船.Controls.Add(this.label6);
            this.panel_乗船.Controls.Add(this.comboBoxCrewMatrixType);
            this.panel_乗船.Controls.Add(this.label7);
            this.panel_乗船.Location = new System.Drawing.Point(15, 167);
            this.panel_乗船.Name = "panel_乗船";
            this.panel_乗船.Size = new System.Drawing.Size(484, 321);
            this.panel_乗船.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(16, 192);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(455, 126);
            this.tabControl1.TabIndex = 182;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button乗船場所クリア);
            this.tabPage1.Controls.Add(this.button乗船場所);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.textBox乗船場所);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(447, 99);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "乗船";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button乗船場所クリア
            // 
            this.button乗船場所クリア.BackColor = System.Drawing.SystemColors.Control;
            this.button乗船場所クリア.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button乗船場所クリア.Location = new System.Drawing.Point(310, 12);
            this.button乗船場所クリア.Name = "button乗船場所クリア";
            this.button乗船場所クリア.Size = new System.Drawing.Size(62, 21);
            this.button乗船場所クリア.TabIndex = 40;
            this.button乗船場所クリア.Text = "クリア";
            this.button乗船場所クリア.UseVisualStyleBackColor = false;
            this.button乗船場所クリア.Click += new System.EventHandler(this.button乗船場所クリア_Click);
            // 
            // button乗船場所
            // 
            this.button乗船場所.BackColor = System.Drawing.SystemColors.Control;
            this.button乗船場所.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button乗船場所.Location = new System.Drawing.Point(242, 12);
            this.button乗船場所.Name = "button乗船場所";
            this.button乗船場所.Size = new System.Drawing.Size(62, 21);
            this.button乗船場所.TabIndex = 33;
            this.button乗船場所.Text = "検索";
            this.button乗船場所.UseVisualStyleBackColor = false;
            this.button乗船場所.Click += new System.EventHandler(this.button乗船場所_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "港";
            // 
            // textBox乗船場所
            // 
            this.textBox乗船場所.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox乗船場所.Location = new System.Drawing.Point(80, 13);
            this.textBox乗船場所.Name = "textBox乗船場所";
            this.textBox乗船場所.ReadOnly = true;
            this.textBox乗船場所.Size = new System.Drawing.Size(143, 20);
            this.textBox乗船場所.TabIndex = 32;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button交代者クリア);
            this.tabPage2.Controls.Add(this.button交代者検索);
            this.tabPage2.Controls.Add(this.textBox交代者);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.textBox下船場所);
            this.tabPage2.Controls.Add(this.button下船場所クリア);
            this.tabPage2.Controls.Add(this.button下船場所);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.comboBoxSignOffReason);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(447, 99);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "下船";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button交代者クリア
            // 
            this.button交代者クリア.BackColor = System.Drawing.SystemColors.Control;
            this.button交代者クリア.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button交代者クリア.Location = new System.Drawing.Point(323, 70);
            this.button交代者クリア.Name = "button交代者クリア";
            this.button交代者クリア.Size = new System.Drawing.Size(62, 21);
            this.button交代者クリア.TabIndex = 39;
            this.button交代者クリア.Text = "クリア";
            this.button交代者クリア.UseVisualStyleBackColor = false;
            this.button交代者クリア.Click += new System.EventHandler(this.button交代者クリア_Click);
            // 
            // button交代者検索
            // 
            this.button交代者検索.BackColor = System.Drawing.SystemColors.Control;
            this.button交代者検索.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button交代者検索.Location = new System.Drawing.Point(255, 70);
            this.button交代者検索.Name = "button交代者検索";
            this.button交代者検索.Size = new System.Drawing.Size(62, 21);
            this.button交代者検索.TabIndex = 39;
            this.button交代者検索.Text = "検索";
            this.button交代者検索.UseVisualStyleBackColor = false;
            this.button交代者検索.Click += new System.EventHandler(this.button交代者検索_Click);
            // 
            // textBox交代者
            // 
            this.textBox交代者.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox交代者.Location = new System.Drawing.Point(80, 70);
            this.textBox交代者.Name = "textBox交代者";
            this.textBox交代者.ReadOnly = true;
            this.textBox交代者.Size = new System.Drawing.Size(164, 20);
            this.textBox交代者.TabIndex = 37;
            this.textBox交代者.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(17, 73);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 13);
            this.label14.TabIndex = 38;
            this.label14.Text = "交代者";
            // 
            // textBox下船場所
            // 
            this.textBox下船場所.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox下船場所.Location = new System.Drawing.Point(80, 42);
            this.textBox下船場所.Name = "textBox下船場所";
            this.textBox下船場所.ReadOnly = true;
            this.textBox下船場所.Size = new System.Drawing.Size(164, 20);
            this.textBox下船場所.TabIndex = 35;
            // 
            // button下船場所クリア
            // 
            this.button下船場所クリア.BackColor = System.Drawing.SystemColors.Control;
            this.button下船場所クリア.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button下船場所クリア.Location = new System.Drawing.Point(323, 43);
            this.button下船場所クリア.Name = "button下船場所クリア";
            this.button下船場所クリア.Size = new System.Drawing.Size(62, 21);
            this.button下船場所クリア.TabIndex = 36;
            this.button下船場所クリア.Text = "クリア";
            this.button下船場所クリア.UseVisualStyleBackColor = false;
            this.button下船場所クリア.Click += new System.EventHandler(this.button下船場所クリア_Click);
            // 
            // button下船場所
            // 
            this.button下船場所.BackColor = System.Drawing.SystemColors.Control;
            this.button下船場所.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button下船場所.Location = new System.Drawing.Point(255, 43);
            this.button下船場所.Name = "button下船場所";
            this.button下船場所.Size = new System.Drawing.Size(62, 21);
            this.button下船場所.TabIndex = 36;
            this.button下船場所.Text = "検索";
            this.button下船場所.UseVisualStyleBackColor = false;
            this.button下船場所.Click += new System.EventHandler(this.button下船場所_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 45);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(20, 13);
            this.label15.TabIndex = 34;
            this.label15.Text = "港";
            // 
            // comboBoxSignOffReason
            // 
            this.comboBoxSignOffReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSignOffReason.FormattingEnabled = true;
            this.comboBoxSignOffReason.Location = new System.Drawing.Point(80, 15);
            this.comboBoxSignOffReason.Name = "comboBoxSignOffReason";
            this.comboBoxSignOffReason.Size = new System.Drawing.Size(162, 21);
            this.comboBoxSignOffReason.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "下船理由";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(9, 118);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(59, 13);
            this.label45.TabIndex = 181;
            this.label45.Text = "航行区域";
            // 
            // comboBox_NavigationArea
            // 
            this.comboBox_NavigationArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_NavigationArea.FormattingEnabled = true;
            this.comboBox_NavigationArea.Location = new System.Drawing.Point(85, 115);
            this.comboBox_NavigationArea.Name = "comboBox_NavigationArea";
            this.comboBox_NavigationArea.Size = new System.Drawing.Size(162, 21);
            this.comboBox_NavigationArea.TabIndex = 180;
            // 
            // comboBox船タイプ
            // 
            this.comboBox船タイプ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船タイプ.FormattingEnabled = true;
            this.comboBox船タイプ.Location = new System.Drawing.Point(319, 34);
            this.comboBox船タイプ.Name = "comboBox船タイプ";
            this.comboBox船タイプ.Size = new System.Drawing.Size(162, 21);
            this.comboBox船タイプ.TabIndex = 37;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(265, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "GRT";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(265, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 13);
            this.label11.TabIndex = 35;
            this.label11.Text = "船タイプ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(264, 92);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(33, 13);
            this.label13.TabIndex = 36;
            this.label13.Text = "船主";
            // 
            // textBoxGRT
            // 
            this.textBoxGRT.Location = new System.Drawing.Point(319, 63);
            this.textBoxGRT.MaxLength = 100;
            this.textBoxGRT.Name = "textBoxGRT";
            this.textBoxGRT.Size = new System.Drawing.Size(162, 20);
            this.textBoxGRT.TabIndex = 32;
            // 
            // textBoxオーナー
            // 
            this.textBoxオーナー.Location = new System.Drawing.Point(319, 89);
            this.textBoxオーナー.MaxLength = 100;
            this.textBoxオーナー.Name = "textBoxオーナー";
            this.textBoxオーナー.Size = new System.Drawing.Size(162, 20);
            this.textBoxオーナー.TabIndex = 33;
            // 
            // comboBox職名
            // 
            this.comboBox職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名.FormattingEnabled = true;
            this.comboBox職名.Location = new System.Drawing.Point(319, 3);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(162, 21);
            this.comboBox職名.TabIndex = 5;
            // 
            // nullableDateTimePicker兼務通信長開始
            // 
            this.nullableDateTimePicker兼務通信長開始.Location = new System.Drawing.Point(124, 151);
            this.nullableDateTimePicker兼務通信長開始.Name = "nullableDateTimePicker兼務通信長開始";
            this.nullableDateTimePicker兼務通信長開始.NullValue = "";
            this.nullableDateTimePicker兼務通信長開始.Size = new System.Drawing.Size(132, 20);
            this.nullableDateTimePicker兼務通信長開始.TabIndex = 7;
            this.nullableDateTimePicker兼務通信長開始.Value = null;
            // 
            // nullableDateTimePicker兼務通信長終了
            // 
            this.nullableDateTimePicker兼務通信長終了.Location = new System.Drawing.Point(286, 151);
            this.nullableDateTimePicker兼務通信長終了.Name = "nullableDateTimePicker兼務通信長終了";
            this.nullableDateTimePicker兼務通信長終了.NullValue = "";
            this.nullableDateTimePicker兼務通信長終了.Size = new System.Drawing.Size(132, 20);
            this.nullableDateTimePicker兼務通信長終了.TabIndex = 8;
            this.nullableDateTimePicker兼務通信長終了.Value = null;
            // 
            // radioButton労働区分乗船_労働
            // 
            this.radioButton労働区分乗船_労働.AutoSize = true;
            this.radioButton労働区分乗船_労働.Location = new System.Drawing.Point(3, 3);
            this.radioButton労働区分乗船_労働.Name = "radioButton労働区分乗船_労働";
            this.radioButton労働区分乗船_労働.Size = new System.Drawing.Size(51, 17);
            this.radioButton労働区分乗船_労働.TabIndex = 35;
            this.radioButton労働区分乗船_労働.TabStop = true;
            this.radioButton労働区分乗船_労働.Text = "労働";
            this.radioButton労働区分乗船_労働.UseVisualStyleBackColor = true;
            // 
            // radioButton労働区分乗船_半休
            // 
            this.radioButton労働区分乗船_半休.AutoSize = true;
            this.radioButton労働区分乗船_半休.Location = new System.Drawing.Point(60, 3);
            this.radioButton労働区分乗船_半休.Name = "radioButton労働区分乗船_半休";
            this.radioButton労働区分乗船_半休.Size = new System.Drawing.Size(51, 17);
            this.radioButton労働区分乗船_半休.TabIndex = 35;
            this.radioButton労働区分乗船_半休.TabStop = true;
            this.radioButton労働区分乗船_半休.Text = "半休";
            this.radioButton労働区分乗船_半休.UseVisualStyleBackColor = true;
            // 
            // radioButton労働区分乗船_全休
            // 
            this.radioButton労働区分乗船_全休.AutoSize = true;
            this.radioButton労働区分乗船_全休.Location = new System.Drawing.Point(117, 3);
            this.radioButton労働区分乗船_全休.Name = "radioButton労働区分乗船_全休";
            this.radioButton労働区分乗船_全休.Size = new System.Drawing.Size(51, 17);
            this.radioButton労働区分乗船_全休.TabIndex = 35;
            this.radioButton労働区分乗船_全休.TabStop = true;
            this.radioButton労働区分乗船_全休.Text = "全休";
            this.radioButton労働区分乗船_全休.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel労働区分乗船
            // 
            this.flowLayoutPanel労働区分乗船.Controls.Add(this.radioButton労働区分乗船_労働);
            this.flowLayoutPanel労働区分乗船.Controls.Add(this.radioButton労働区分乗船_半休);
            this.flowLayoutPanel労働区分乗船.Controls.Add(this.radioButton労働区分乗船_全休);
            this.flowLayoutPanel労働区分乗船.Location = new System.Drawing.Point(229, 3);
            this.flowLayoutPanel労働区分乗船.Name = "flowLayoutPanel労働区分乗船";
            this.flowLayoutPanel労働区分乗船.Size = new System.Drawing.Size(180, 27);
            this.flowLayoutPanel労働区分乗船.TabIndex = 4;
            // 
            // flowLayoutPanel労働区分下船
            // 
            this.flowLayoutPanel労働区分下船.Controls.Add(this.radioButton労働区分下船_労働);
            this.flowLayoutPanel労働区分下船.Controls.Add(this.radioButton労働区分下船_半休);
            this.flowLayoutPanel労働区分下船.Controls.Add(this.radioButton労働区分下船_全休);
            this.flowLayoutPanel労働区分下船.Location = new System.Drawing.Point(229, 33);
            this.flowLayoutPanel労働区分下船.Name = "flowLayoutPanel労働区分下船";
            this.flowLayoutPanel労働区分下船.Size = new System.Drawing.Size(180, 27);
            this.flowLayoutPanel労働区分下船.TabIndex = 6;
            // 
            // radioButton労働区分下船_労働
            // 
            this.radioButton労働区分下船_労働.AutoSize = true;
            this.radioButton労働区分下船_労働.Location = new System.Drawing.Point(3, 3);
            this.radioButton労働区分下船_労働.Name = "radioButton労働区分下船_労働";
            this.radioButton労働区分下船_労働.Size = new System.Drawing.Size(51, 17);
            this.radioButton労働区分下船_労働.TabIndex = 35;
            this.radioButton労働区分下船_労働.TabStop = true;
            this.radioButton労働区分下船_労働.Text = "労働";
            this.radioButton労働区分下船_労働.UseVisualStyleBackColor = true;
            // 
            // radioButton労働区分下船_半休
            // 
            this.radioButton労働区分下船_半休.AutoSize = true;
            this.radioButton労働区分下船_半休.Location = new System.Drawing.Point(60, 3);
            this.radioButton労働区分下船_半休.Name = "radioButton労働区分下船_半休";
            this.radioButton労働区分下船_半休.Size = new System.Drawing.Size(51, 17);
            this.radioButton労働区分下船_半休.TabIndex = 35;
            this.radioButton労働区分下船_半休.TabStop = true;
            this.radioButton労働区分下船_半休.Text = "半休";
            this.radioButton労働区分下船_半休.UseVisualStyleBackColor = true;
            // 
            // radioButton労働区分下船_全休
            // 
            this.radioButton労働区分下船_全休.AutoSize = true;
            this.radioButton労働区分下船_全休.Location = new System.Drawing.Point(117, 3);
            this.radioButton労働区分下船_全休.Name = "radioButton労働区分下船_全休";
            this.radioButton労働区分下船_全休.Size = new System.Drawing.Size(51, 17);
            this.radioButton労働区分下船_全休.TabIndex = 35;
            this.radioButton労働区分下船_全休.TabStop = true;
            this.radioButton労働区分下船_全休.Text = "全休";
            this.radioButton労働区分下船_全休.UseVisualStyleBackColor = true;
            // 
            // comboBox種別詳細
            // 
            this.comboBox種別詳細.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox種別詳細.FormattingEnabled = true;
            this.comboBox種別詳細.Location = new System.Drawing.Point(85, 3);
            this.comboBox種別詳細.Name = "comboBox種別詳細";
            this.comboBox種別詳細.Size = new System.Drawing.Size(162, 21);
            this.comboBox種別詳細.TabIndex = 2;
            this.comboBox種別詳細.SelectedIndexChanged += new System.EventHandler(this.comboBox種別_SelectedIndexChanged);
            this.comboBox種別詳細.SelectionChangeCommitted += new System.EventHandler(this.comboBox種別_SelectionChangeCommitted);
            // 
            // panel事務所
            // 
            this.panel事務所.Controls.Add(this.label8);
            this.panel事務所.Controls.Add(this.comboBox種別詳細);
            this.panel事務所.Location = new System.Drawing.Point(15, 163);
            this.panel事務所.Name = "panel事務所";
            this.panel事務所.Size = new System.Drawing.Size(471, 28);
            this.panel事務所.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "事務所";
            // 
            // panel日程
            // 
            this.panel日程.Controls.Add(this.label1);
            this.panel日程.Controls.Add(this.label4);
            this.panel日程.Controls.Add(this.label5);
            this.panel日程.Controls.Add(this.label2);
            this.panel日程.Controls.Add(this.textBox日数);
            this.panel日程.Controls.Add(this.flowLayoutPanel労働区分下船);
            this.panel日程.Controls.Add(this.nullableDateTimePicker開始);
            this.panel日程.Controls.Add(this.flowLayoutPanel労働区分乗船);
            this.panel日程.Controls.Add(this.nullableDateTimePicker終了);
            this.panel日程.Location = new System.Drawing.Point(12, 67);
            this.panel日程.Name = "panel日程";
            this.panel日程.Size = new System.Drawing.Size(426, 90);
            this.panel日程.TabIndex = 14;
            // 
            // nullableDateTimePicker開始
            // 
            this.nullableDateTimePicker開始.Location = new System.Drawing.Point(91, 3);
            this.nullableDateTimePicker開始.Name = "nullableDateTimePicker開始";
            this.nullableDateTimePicker開始.NullValue = "";
            this.nullableDateTimePicker開始.Size = new System.Drawing.Size(132, 20);
            this.nullableDateTimePicker開始.TabIndex = 3;
            this.nullableDateTimePicker開始.Value = null;
            this.nullableDateTimePicker開始.ValueChanged += new System.EventHandler(this.nullableDateTimePicker開始_ValueChanged);
            // 
            // nullableDateTimePicker終了
            // 
            this.nullableDateTimePicker終了.Location = new System.Drawing.Point(91, 33);
            this.nullableDateTimePicker終了.Name = "nullableDateTimePicker終了";
            this.nullableDateTimePicker終了.NullValue = "";
            this.nullableDateTimePicker終了.Size = new System.Drawing.Size(132, 20);
            this.nullableDateTimePicker終了.TabIndex = 5;
            this.nullableDateTimePicker終了.Value = null;
            this.nullableDateTimePicker終了.ValueChanged += new System.EventHandler(this.nullableDateTimePicker終了_ValueChanged);
            // 
            // button役職変更
            // 
            this.button役職変更.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button役職変更.BackColor = System.Drawing.SystemColors.Control;
            this.button役職変更.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button役職変更.Location = new System.Drawing.Point(424, 139);
            this.button役職変更.Name = "button役職変更";
            this.button役職変更.Size = new System.Drawing.Size(75, 22);
            this.button役職変更.TabIndex = 53;
            this.button役職変更.Text = "役職変更";
            this.button役職変更.UseVisualStyleBackColor = false;
            this.button役職変更.Click += new System.EventHandler(this.button役職変更_Click);
            // 
            // 乗船履歴詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(522, 545);
            this.Controls.Add(this.button役職変更);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.panel日程);
            this.Controls.Add(this.panel_乗船);
            this.Controls.Add(this.checkBox転船);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox種別);
            this.Controls.Add(this.panel事務所);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "乗船履歴詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "乗下船履歴詳細";
            this.Load += new System.EventHandler(this.乗船履歴詳細Form_Load);
            this.panel_乗船.ResumeLayout(false);
            this.panel_乗船.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.flowLayoutPanel労働区分乗船.ResumeLayout(false);
            this.flowLayoutPanel労働区分乗船.PerformLayout();
            this.flowLayoutPanel労働区分下船.ResumeLayout(false);
            this.flowLayoutPanel労働区分下船.PerformLayout();
            this.panel事務所.ResumeLayout(false);
            this.panel事務所.PerformLayout();
            this.panel日程.ResumeLayout(false);
            this.panel日程.PerformLayout();
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
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox船名;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox会社名;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox comboBoxCrewMatrixType;
        private System.Windows.Forms.Label label32;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker兼務通信長終了;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker兼務通信長開始;
        private System.Windows.Forms.CheckBox checkBox兼務通信長;
        private System.Windows.Forms.Panel panel_乗船;
        private System.Windows.Forms.ComboBox comboBox職名;
        private System.Windows.Forms.RadioButton radioButton労働区分乗船_労働;
        private System.Windows.Forms.RadioButton radioButton労働区分乗船_半休;
        private System.Windows.Forms.RadioButton radioButton労働区分乗船_全休;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel労働区分乗船;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel労働区分下船;
        private System.Windows.Forms.RadioButton radioButton労働区分下船_労働;
        private System.Windows.Forms.RadioButton radioButton労働区分下船_半休;
        private System.Windows.Forms.RadioButton radioButton労働区分下船_全休;
        private System.Windows.Forms.ComboBox comboBox種別詳細;
        private System.Windows.Forms.Panel panel事務所;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel日程;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxSignOffReason;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ComboBox comboBox_NavigationArea;
        private System.Windows.Forms.ComboBox comboBox船タイプ;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxGRT;
        private System.Windows.Forms.TextBox textBoxオーナー;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button乗船場所クリア;
        private System.Windows.Forms.Button button乗船場所;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox乗船場所;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button交代者クリア;
        private System.Windows.Forms.Button button交代者検索;
        private System.Windows.Forms.TextBox textBox交代者;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox下船場所;
        private System.Windows.Forms.Button button下船場所クリア;
        private System.Windows.Forms.Button button下船場所;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button役職変更;
    }
}