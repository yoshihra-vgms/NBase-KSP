namespace NBase
{
    partial class 動静予定
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
            this.tableLayoutPanel_TumiAge = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel_Tumi = new System.Windows.Forms.FlowLayoutPanel();
            this.douseiYoteiUserControl1 = new NBaseCommon.DouseiYoteiUserControl();
            this.douseiYoteiUserControl2 = new NBaseCommon.DouseiYoteiUserControl();
            this.flowLayoutPanel_Age = new System.Windows.Forms.FlowLayoutPanel();
            this.douseiYoteiUserControl3 = new NBaseCommon.DouseiYoteiUserControl();
            this.douseiYoteiUserControl4 = new NBaseCommon.DouseiYoteiUserControl();
            this.douseiYoteiUserControl5 = new NBaseCommon.DouseiYoteiUserControl();
            this.douseiYoteiUserControl6 = new NBaseCommon.DouseiYoteiUserControl();
            this.douseiYoteiUserControl7 = new NBaseCommon.DouseiYoteiUserControl();
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
            this.tableLayoutPanel_TumiAge.SuspendLayout();
            this.flowLayoutPanel_Tumi.SuspendLayout();
            this.flowLayoutPanel_Age.SuspendLayout();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1247, 742);
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
            this.panel1.Size = new System.Drawing.Size(1241, 62);
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
            this.button_閉じる.Location = new System.Drawing.Point(1157, 15);
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
            this.panel2.Controls.Add(this.tableLayoutPanel_TumiAge);
            this.panel2.Controls.Add(this.panel_TaikiNyukyoParge);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1241, 668);
            this.panel2.TabIndex = 1;
            // 
            // tableLayoutPanel_TumiAge
            // 
            this.tableLayoutPanel_TumiAge.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_TumiAge.ColumnCount = 1;
            this.tableLayoutPanel_TumiAge.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1242F));
            this.tableLayoutPanel_TumiAge.Controls.Add(this.flowLayoutPanel_Tumi, 0, 0);
            this.tableLayoutPanel_TumiAge.Controls.Add(this.flowLayoutPanel_Age, 0, 1);
            this.tableLayoutPanel_TumiAge.Location = new System.Drawing.Point(0, 62);
            this.tableLayoutPanel_TumiAge.Name = "tableLayoutPanel_TumiAge";
            this.tableLayoutPanel_TumiAge.RowCount = 2;
            this.tableLayoutPanel_TumiAge.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 319F));
            this.tableLayoutPanel_TumiAge.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_TumiAge.Size = new System.Drawing.Size(1242, 623);
            this.tableLayoutPanel_TumiAge.TabIndex = 2;
            // 
            // flowLayoutPanel_Tumi
            // 
            this.flowLayoutPanel_Tumi.AutoScroll = true;
            this.flowLayoutPanel_Tumi.Controls.Add(this.douseiYoteiUserControl1);
            this.flowLayoutPanel_Tumi.Controls.Add(this.douseiYoteiUserControl2);
            this.flowLayoutPanel_Tumi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_Tumi.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel_Tumi.Name = "flowLayoutPanel_Tumi";
            this.flowLayoutPanel_Tumi.Size = new System.Drawing.Size(1236, 313);
            this.flowLayoutPanel_Tumi.TabIndex = 0;
            // 
            // douseiYoteiUserControl1
            // 
            this.douseiYoteiUserControl1.BackColor = System.Drawing.Color.White;
            this.douseiYoteiUserControl1.Location = new System.Drawing.Point(3, 3);
            this.douseiYoteiUserControl1.Name = "douseiYoteiUserControl1";
            this.douseiYoteiUserControl1.Size = new System.Drawing.Size(400, 305);
            this.douseiYoteiUserControl1.TabIndex = 0;
            // 
            // douseiYoteiUserControl2
            // 
            this.douseiYoteiUserControl2.BackColor = System.Drawing.Color.White;
            this.douseiYoteiUserControl2.Location = new System.Drawing.Point(409, 3);
            this.douseiYoteiUserControl2.Name = "douseiYoteiUserControl2";
            this.douseiYoteiUserControl2.Size = new System.Drawing.Size(400, 305);
            this.douseiYoteiUserControl2.TabIndex = 1;
            // 
            // flowLayoutPanel_Age
            // 
            this.flowLayoutPanel_Age.AutoScroll = true;
            this.flowLayoutPanel_Age.Controls.Add(this.douseiYoteiUserControl3);
            this.flowLayoutPanel_Age.Controls.Add(this.douseiYoteiUserControl4);
            this.flowLayoutPanel_Age.Controls.Add(this.douseiYoteiUserControl5);
            this.flowLayoutPanel_Age.Controls.Add(this.douseiYoteiUserControl6);
            this.flowLayoutPanel_Age.Controls.Add(this.douseiYoteiUserControl7);
            this.flowLayoutPanel_Age.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_Age.Location = new System.Drawing.Point(3, 322);
            this.flowLayoutPanel_Age.Name = "flowLayoutPanel_Age";
            this.flowLayoutPanel_Age.Size = new System.Drawing.Size(1236, 298);
            this.flowLayoutPanel_Age.TabIndex = 2;
            // 
            // douseiYoteiUserControl3
            // 
            this.douseiYoteiUserControl3.BackColor = System.Drawing.Color.White;
            this.douseiYoteiUserControl3.Location = new System.Drawing.Point(3, 3);
            this.douseiYoteiUserControl3.Name = "douseiYoteiUserControl3";
            this.douseiYoteiUserControl3.Size = new System.Drawing.Size(400, 305);
            this.douseiYoteiUserControl3.TabIndex = 1;
            // 
            // douseiYoteiUserControl4
            // 
            this.douseiYoteiUserControl4.BackColor = System.Drawing.Color.White;
            this.douseiYoteiUserControl4.Location = new System.Drawing.Point(409, 3);
            this.douseiYoteiUserControl4.Name = "douseiYoteiUserControl4";
            this.douseiYoteiUserControl4.Size = new System.Drawing.Size(400, 305);
            this.douseiYoteiUserControl4.TabIndex = 2;
            // 
            // douseiYoteiUserControl5
            // 
            this.douseiYoteiUserControl5.BackColor = System.Drawing.Color.White;
            this.douseiYoteiUserControl5.Location = new System.Drawing.Point(815, 3);
            this.douseiYoteiUserControl5.Name = "douseiYoteiUserControl5";
            this.douseiYoteiUserControl5.Size = new System.Drawing.Size(400, 305);
            this.douseiYoteiUserControl5.TabIndex = 3;
            // 
            // douseiYoteiUserControl6
            // 
            this.douseiYoteiUserControl6.BackColor = System.Drawing.Color.White;
            this.douseiYoteiUserControl6.Location = new System.Drawing.Point(3, 314);
            this.douseiYoteiUserControl6.Name = "douseiYoteiUserControl6";
            this.douseiYoteiUserControl6.Size = new System.Drawing.Size(400, 305);
            this.douseiYoteiUserControl6.TabIndex = 4;
            // 
            // douseiYoteiUserControl7
            // 
            this.douseiYoteiUserControl7.BackColor = System.Drawing.Color.White;
            this.douseiYoteiUserControl7.Location = new System.Drawing.Point(409, 314);
            this.douseiYoteiUserControl7.Name = "douseiYoteiUserControl7";
            this.douseiYoteiUserControl7.Size = new System.Drawing.Size(400, 305);
            this.douseiYoteiUserControl7.TabIndex = 5;
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
            // 動静予定
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1247, 742);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "動静予定";
            this.Text = "運航予定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.動静予定_FormClosing);
            this.Load += new System.EventHandler(this.動静予定_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel_TumiAge.ResumeLayout(false);
            this.flowLayoutPanel_Tumi.ResumeLayout(false);
            this.flowLayoutPanel_Age.ResumeLayout(false);
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_TumiAge;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Tumi;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Age;
        private NBaseCommon.DouseiYoteiUserControl douseiYoteiUserControl1;
        private NBaseCommon.DouseiYoteiUserControl douseiYoteiUserControl2;
        private NBaseCommon.DouseiYoteiUserControl douseiYoteiUserControl3;
        private NBaseCommon.DouseiYoteiUserControl douseiYoteiUserControl4;
        private NBaseCommon.DouseiYoteiUserControl douseiYoteiUserControl5;
        private NBaseCommon.DouseiYoteiUserControl douseiYoteiUserControl6;
        private NBaseCommon.DouseiYoteiUserControl douseiYoteiUserControl7;
        private System.Windows.Forms.RadioButton radioButton_Hihaku;
        private System.Windows.Forms.TextBox textBox_備考;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_複製;
    }
}