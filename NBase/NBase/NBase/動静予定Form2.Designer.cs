namespace NBase
{
    partial class 動静予定Form2
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
            this.comboBox_船 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_Hihaku = new System.Windows.Forms.RadioButton();
            this.radioButton_Purge = new System.Windows.Forms.RadioButton();
            this.radioButton_Etc = new System.Windows.Forms.RadioButton();
            this.radioButton_Taiki = new System.Windows.Forms.RadioButton();
            this.radioButton_TumiAge = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_複製 = new System.Windows.Forms.Button();
            this.button_閉じる = new System.Windows.Forms.Button();
            this.button_削除 = new System.Windows.Forms.Button();
            this.button_登録 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage積み = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.douseiJissekiUserControl2Tumi1 = new NBaseCommon.DouseiJissekiUserControl2();
            this.douseiJissekiUserControl2Tumi2 = new NBaseCommon.DouseiJissekiUserControl2();
            this.douseiJissekiUserControl2Tumi3 = new NBaseCommon.DouseiJissekiUserControl2();
            this.douseiJissekiUserControl2Tumi4 = new NBaseCommon.DouseiJissekiUserControl2();
            this.tabPage揚げ = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.douseiJissekiUserControl2Age1 = new NBaseCommon.DouseiJissekiUserControl2();
            this.douseiJissekiUserControl2Age2 = new NBaseCommon.DouseiJissekiUserControl2();
            this.douseiJissekiUserControl2Age3 = new NBaseCommon.DouseiJissekiUserControl2();
            this.douseiJissekiUserControl2Age4 = new NBaseCommon.DouseiJissekiUserControl2();
            this.panel_TaikiNyukyoParge = new System.Windows.Forms.Panel();
            this.textBox_備考 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_基地 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_港 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage積み.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabPage揚げ.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel_TaikiNyukyoParge.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "船：";
            // 
            // comboBox_船
            // 
            this.comboBox_船.FormattingEnabled = true;
            this.comboBox_船.Location = new System.Drawing.Point(38, 24);
            this.comboBox_船.Name = "comboBox_船";
            this.comboBox_船.Size = new System.Drawing.Size(228, 20);
            this.comboBox_船.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_Hihaku);
            this.groupBox1.Controls.Add(this.radioButton_Purge);
            this.groupBox1.Controls.Add(this.radioButton_Etc);
            this.groupBox1.Controls.Add(this.radioButton_Taiki);
            this.groupBox1.Controls.Add(this.radioButton_TumiAge);
            this.groupBox1.Location = new System.Drawing.Point(295, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 49);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "種別";
            // 
            // radioButton_Hihaku
            // 
            this.radioButton_Hihaku.AutoSize = true;
            this.radioButton_Hihaku.Location = new System.Drawing.Point(146, 19);
            this.radioButton_Hihaku.Name = "radioButton_Hihaku";
            this.radioButton_Hihaku.Size = new System.Drawing.Size(47, 16);
            this.radioButton_Hihaku.TabIndex = 7;
            this.radioButton_Hihaku.TabStop = true;
            this.radioButton_Hihaku.Text = "避泊";
            this.radioButton_Hihaku.UseVisualStyleBackColor = true;
            this.radioButton_Hihaku.CheckedChanged += new System.EventHandler(this.radioButton_TaikiNyukyoParge_CheckedChanged);
            // 
            // radioButton_Purge
            // 
            this.radioButton_Purge.AutoSize = true;
            this.radioButton_Purge.Location = new System.Drawing.Point(208, 19);
            this.radioButton_Purge.Name = "radioButton_Purge";
            this.radioButton_Purge.Size = new System.Drawing.Size(53, 16);
            this.radioButton_Purge.TabIndex = 6;
            this.radioButton_Purge.TabStop = true;
            this.radioButton_Purge.Text = "パージ";
            this.radioButton_Purge.UseVisualStyleBackColor = true;
            this.radioButton_Purge.CheckedChanged += new System.EventHandler(this.radioButton_TaikiNyukyoParge_CheckedChanged);
            // 
            // radioButton_Etc
            // 
            this.radioButton_Etc.AutoSize = true;
            this.radioButton_Etc.Location = new System.Drawing.Point(276, 19);
            this.radioButton_Etc.Name = "radioButton_Etc";
            this.radioButton_Etc.Size = new System.Drawing.Size(54, 16);
            this.radioButton_Etc.TabIndex = 5;
            this.radioButton_Etc.TabStop = true;
            this.radioButton_Etc.Text = "その他";
            this.radioButton_Etc.UseVisualStyleBackColor = true;
            this.radioButton_Etc.CheckedChanged += new System.EventHandler(this.radioButton_TaikiNyukyoParge_CheckedChanged);
            // 
            // radioButton_Taiki
            // 
            this.radioButton_Taiki.AutoSize = true;
            this.radioButton_Taiki.Location = new System.Drawing.Point(84, 19);
            this.radioButton_Taiki.Name = "radioButton_Taiki";
            this.radioButton_Taiki.Size = new System.Drawing.Size(47, 16);
            this.radioButton_Taiki.TabIndex = 4;
            this.radioButton_Taiki.TabStop = true;
            this.radioButton_Taiki.Text = "待機";
            this.radioButton_Taiki.UseVisualStyleBackColor = true;
            this.radioButton_Taiki.CheckedChanged += new System.EventHandler(this.radioButton_TaikiNyukyoParge_CheckedChanged);
            // 
            // radioButton_TumiAge
            // 
            this.radioButton_TumiAge.AutoSize = true;
            this.radioButton_TumiAge.Location = new System.Drawing.Point(16, 19);
            this.radioButton_TumiAge.Name = "radioButton_TumiAge";
            this.radioButton_TumiAge.Size = new System.Drawing.Size(53, 16);
            this.radioButton_TumiAge.TabIndex = 3;
            this.radioButton_TumiAge.TabStop = true;
            this.radioButton_TumiAge.Text = "積/揚";
            this.radioButton_TumiAge.UseVisualStyleBackColor = true;
            this.radioButton_TumiAge.CheckedChanged += new System.EventHandler(this.radioButton_TumiAge_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1489, 672);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.button_複製);
            this.panel1.Controls.Add(this.button_閉じる);
            this.panel1.Controls.Add(this.button_削除);
            this.panel1.Controls.Add(this.button_登録);
            this.panel1.Controls.Add(this.comboBox_船);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1483, 62);
            this.panel1.TabIndex = 0;
            // 
            // button_複製
            // 
            this.button_複製.Location = new System.Drawing.Point(674, 15);
            this.button_複製.Name = "button_複製";
            this.button_複製.Size = new System.Drawing.Size(75, 23);
            this.button_複製.TabIndex = 6;
            this.button_複製.Text = "複製";
            this.button_複製.UseVisualStyleBackColor = true;
            this.button_複製.Click += new System.EventHandler(this.button_複製_Click);
            // 
            // button_閉じる
            // 
            this.button_閉じる.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_閉じる.Location = new System.Drawing.Point(1399, 15);
            this.button_閉じる.Name = "button_閉じる";
            this.button_閉じる.Size = new System.Drawing.Size(75, 23);
            this.button_閉じる.TabIndex = 5;
            this.button_閉じる.Text = "閉じる";
            this.button_閉じる.UseVisualStyleBackColor = true;
            this.button_閉じる.Click += new System.EventHandler(this.button_閉じる_Click);
            // 
            // button_削除
            // 
            this.button_削除.Location = new System.Drawing.Point(837, 15);
            this.button_削除.Name = "button_削除";
            this.button_削除.Size = new System.Drawing.Size(75, 23);
            this.button_削除.TabIndex = 4;
            this.button_削除.Text = "削除";
            this.button_削除.UseVisualStyleBackColor = true;
            this.button_削除.Click += new System.EventHandler(this.button_削除_Click);
            // 
            // button_登録
            // 
            this.button_登録.Location = new System.Drawing.Point(755, 15);
            this.button_登録.Name = "button_登録";
            this.button_登録.Size = new System.Drawing.Size(75, 23);
            this.button_登録.TabIndex = 3;
            this.button_登録.Text = "登録";
            this.button_登録.UseVisualStyleBackColor = true;
            this.button_登録.Click += new System.EventHandler(this.button_登録_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Controls.Add(this.panel_TaikiNyukyoParge);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1483, 598);
            this.panel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage積み);
            this.tabControl1.Controls.Add(this.tabPage揚げ);
            this.tabControl1.Location = new System.Drawing.Point(3, 74);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1480, 524);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage積み
            // 
            this.tabPage積み.Controls.Add(this.flowLayoutPanel1);
            this.tabPage積み.Location = new System.Drawing.Point(4, 22);
            this.tabPage積み.Name = "tabPage積み";
            this.tabPage積み.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage積み.Size = new System.Drawing.Size(1472, 498);
            this.tabPage積み.TabIndex = 0;
            this.tabPage積み.Text = "積み";
            this.tabPage積み.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.douseiJissekiUserControl2Tumi1);
            this.flowLayoutPanel1.Controls.Add(this.douseiJissekiUserControl2Tumi2);
            this.flowLayoutPanel1.Controls.Add(this.douseiJissekiUserControl2Tumi3);
            this.flowLayoutPanel1.Controls.Add(this.douseiJissekiUserControl2Tumi4);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1466, 492);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // douseiJissekiUserControl2Tumi1
            // 
            this.douseiJissekiUserControl2Tumi1.BackColor = System.Drawing.Color.White;
            this.douseiJissekiUserControl2Tumi1.Location = new System.Drawing.Point(3, 3);
            this.douseiJissekiUserControl2Tumi1.Name = "douseiJissekiUserControl2Tumi1";
            this.douseiJissekiUserControl2Tumi1.Size = new System.Drawing.Size(360, 476);
            this.douseiJissekiUserControl2Tumi1.TabIndex = 0;
            // 
            // douseiJissekiUserControl2Tumi2
            // 
            this.douseiJissekiUserControl2Tumi2.BackColor = System.Drawing.Color.White;
            this.douseiJissekiUserControl2Tumi2.Location = new System.Drawing.Point(369, 3);
            this.douseiJissekiUserControl2Tumi2.Name = "douseiJissekiUserControl2Tumi2";
            this.douseiJissekiUserControl2Tumi2.Size = new System.Drawing.Size(360, 476);
            this.douseiJissekiUserControl2Tumi2.TabIndex = 0;
            // 
            // douseiJissekiUserControl2Tumi3
            // 
            this.douseiJissekiUserControl2Tumi3.BackColor = System.Drawing.Color.White;
            this.douseiJissekiUserControl2Tumi3.Location = new System.Drawing.Point(735, 3);
            this.douseiJissekiUserControl2Tumi3.Name = "douseiJissekiUserControl2Tumi3";
            this.douseiJissekiUserControl2Tumi3.Size = new System.Drawing.Size(360, 476);
            this.douseiJissekiUserControl2Tumi3.TabIndex = 0;
            // 
            // douseiJissekiUserControl2Tumi4
            // 
            this.douseiJissekiUserControl2Tumi4.BackColor = System.Drawing.Color.White;
            this.douseiJissekiUserControl2Tumi4.Location = new System.Drawing.Point(1101, 3);
            this.douseiJissekiUserControl2Tumi4.Name = "douseiJissekiUserControl2Tumi4";
            this.douseiJissekiUserControl2Tumi4.Size = new System.Drawing.Size(360, 476);
            this.douseiJissekiUserControl2Tumi4.TabIndex = 0;
            // 
            // tabPage揚げ
            // 
            this.tabPage揚げ.Controls.Add(this.flowLayoutPanel2);
            this.tabPage揚げ.Location = new System.Drawing.Point(4, 22);
            this.tabPage揚げ.Name = "tabPage揚げ";
            this.tabPage揚げ.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage揚げ.Size = new System.Drawing.Size(1472, 498);
            this.tabPage揚げ.TabIndex = 1;
            this.tabPage揚げ.Text = "揚げ";
            this.tabPage揚げ.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.douseiJissekiUserControl2Age1);
            this.flowLayoutPanel2.Controls.Add(this.douseiJissekiUserControl2Age2);
            this.flowLayoutPanel2.Controls.Add(this.douseiJissekiUserControl2Age3);
            this.flowLayoutPanel2.Controls.Add(this.douseiJissekiUserControl2Age4);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1466, 492);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // douseiJissekiUserControl2Age1
            // 
            this.douseiJissekiUserControl2Age1.BackColor = System.Drawing.Color.White;
            this.douseiJissekiUserControl2Age1.Location = new System.Drawing.Point(3, 3);
            this.douseiJissekiUserControl2Age1.Name = "douseiJissekiUserControl2Age1";
            this.douseiJissekiUserControl2Age1.Size = new System.Drawing.Size(360, 476);
            this.douseiJissekiUserControl2Age1.TabIndex = 1;
            // 
            // douseiJissekiUserControl2Age2
            // 
            this.douseiJissekiUserControl2Age2.BackColor = System.Drawing.Color.White;
            this.douseiJissekiUserControl2Age2.Location = new System.Drawing.Point(369, 3);
            this.douseiJissekiUserControl2Age2.Name = "douseiJissekiUserControl2Age2";
            this.douseiJissekiUserControl2Age2.Size = new System.Drawing.Size(360, 476);
            this.douseiJissekiUserControl2Age2.TabIndex = 2;
            // 
            // douseiJissekiUserControl2Age3
            // 
            this.douseiJissekiUserControl2Age3.BackColor = System.Drawing.Color.White;
            this.douseiJissekiUserControl2Age3.Location = new System.Drawing.Point(735, 3);
            this.douseiJissekiUserControl2Age3.Name = "douseiJissekiUserControl2Age3";
            this.douseiJissekiUserControl2Age3.Size = new System.Drawing.Size(360, 476);
            this.douseiJissekiUserControl2Age3.TabIndex = 3;
            // 
            // douseiJissekiUserControl2Age4
            // 
            this.douseiJissekiUserControl2Age4.BackColor = System.Drawing.Color.White;
            this.douseiJissekiUserControl2Age4.Location = new System.Drawing.Point(1101, 3);
            this.douseiJissekiUserControl2Age4.Name = "douseiJissekiUserControl2Age4";
            this.douseiJissekiUserControl2Age4.Size = new System.Drawing.Size(360, 476);
            this.douseiJissekiUserControl2Age4.TabIndex = 4;
            // 
            // panel_TaikiNyukyoParge
            // 
            this.panel_TaikiNyukyoParge.Controls.Add(this.textBox_備考);
            this.panel_TaikiNyukyoParge.Controls.Add(this.label6);
            this.panel_TaikiNyukyoParge.Controls.Add(this.comboBox_基地);
            this.panel_TaikiNyukyoParge.Controls.Add(this.label5);
            this.panel_TaikiNyukyoParge.Controls.Add(this.comboBox_港);
            this.panel_TaikiNyukyoParge.Controls.Add(this.label4);
            this.panel_TaikiNyukyoParge.Controls.Add(this.label3);
            this.panel_TaikiNyukyoParge.Controls.Add(this.dateTimePicker2);
            this.panel_TaikiNyukyoParge.Controls.Add(this.dateTimePicker1);
            this.panel_TaikiNyukyoParge.Controls.Add(this.label2);
            this.panel_TaikiNyukyoParge.Location = new System.Drawing.Point(0, 0);
            this.panel_TaikiNyukyoParge.Name = "panel_TaikiNyukyoParge";
            this.panel_TaikiNyukyoParge.Size = new System.Drawing.Size(1218, 68);
            this.panel_TaikiNyukyoParge.TabIndex = 1;
            // 
            // textBox_備考
            // 
            this.textBox_備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_備考.Location = new System.Drawing.Point(48, 37);
            this.textBox_備考.MaxLength = 25;
            this.textBox_備考.Name = "textBox_備考";
            this.textBox_備考.Size = new System.Drawing.Size(287, 19);
            this.textBox_備考.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "備考";
            // 
            // comboBox_基地
            // 
            this.comboBox_基地.FormattingEnabled = true;
            this.comboBox_基地.Location = new System.Drawing.Point(658, 9);
            this.comboBox_基地.Name = "comboBox_基地";
            this.comboBox_基地.Size = new System.Drawing.Size(228, 20);
            this.comboBox_基地.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(624, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "基地";
            // 
            // comboBox_港
            // 
            this.comboBox_港.FormattingEnabled = true;
            this.comboBox_港.Location = new System.Drawing.Point(375, 9);
            this.comboBox_港.Name = "comboBox_港";
            this.comboBox_港.Size = new System.Drawing.Size(228, 20);
            this.comboBox_港.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(354, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "港";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "～";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(211, 9);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(124, 19);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(48, 9);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(124, 19);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "日付";
            // 
            // 動静予定Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1489, 672);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "動静予定Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "運航予定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.動静予定Form2_FormClosing);
            this.Load += new System.EventHandler(this.動静予定Form2_Load);
            this.Shown += new System.EventHandler(this.動静予定Form2_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage積み.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabPage揚げ.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.panel_TaikiNyukyoParge.ResumeLayout(false);
            this.panel_TaikiNyukyoParge.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_船;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_Taiki;
        private System.Windows.Forms.RadioButton radioButton_TumiAge;
        private System.Windows.Forms.RadioButton radioButton_Purge;
        private System.Windows.Forms.RadioButton radioButton_Etc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel_TaikiNyukyoParge;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_港;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.ComboBox comboBox_基地;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_閉じる;
        private System.Windows.Forms.Button button_削除;
        private System.Windows.Forms.Button button_登録;
        private System.Windows.Forms.RadioButton radioButton_Hihaku;
        private System.Windows.Forms.TextBox textBox_備考;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_複製;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage積み;
        private System.Windows.Forms.TabPage tabPage揚げ;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private NBaseCommon.DouseiJissekiUserControl2 douseiJissekiUserControl2Tumi1;
        private NBaseCommon.DouseiJissekiUserControl2 douseiJissekiUserControl2Tumi2;
        private NBaseCommon.DouseiJissekiUserControl2 douseiJissekiUserControl2Tumi3;
        private NBaseCommon.DouseiJissekiUserControl2 douseiJissekiUserControl2Tumi4;
        private NBaseCommon.DouseiJissekiUserControl2 douseiJissekiUserControl2Age1;
        private NBaseCommon.DouseiJissekiUserControl2 douseiJissekiUserControl2Age2;
        private NBaseCommon.DouseiJissekiUserControl2 douseiJissekiUserControl2Age3;
        private NBaseCommon.DouseiJissekiUserControl2 douseiJissekiUserControl2Age4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}