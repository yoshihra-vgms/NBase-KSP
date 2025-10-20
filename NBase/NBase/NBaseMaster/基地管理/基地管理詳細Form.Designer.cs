namespace NBaseMaster.基地管理
{
    partial class 基地管理詳細Form
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
            this.KichiNo_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Update_button = new System.Windows.Forms.Button();
            this.Delete_button = new System.Windows.Forms.Button();
            this.Close_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.KichiName_textBox = new System.Windows.Forms.TextBox();
            this.comboBox_Basho = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ForNyukouToChakusan = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_AvailableForChakusanFrom = new System.Windows.Forms.TextBox();
            this.textBox_AvailableForNiyakuFrom = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_ForChakusanToNiyaku = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_ForNiyakuToRisan = new System.Windows.Forms.TextBox();
            this.textBox_AvailableForRisanFrom = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_AvailableForChakusanTo = new System.Windows.Forms.TextBox();
            this.textBox_AvailableForNiyakuTo = new System.Windows.Forms.TextBox();
            this.textBox_AvailableForRisanTo = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.gcMultiRow1 = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.template11 = new NBaseMaster.基地管理.Template1();
            this.textBox_ForRisanToShukou = new System.Windows.Forms.TextBox();
            this.textBox_AvailableForShukouFrom = new System.Windows.Forms.TextBox();
            this.textBox_AvailableForShukouTo = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gcMultiRow1)).BeginInit();
            this.SuspendLayout();
            // 
            // KichiNo_textBox
            // 
            this.KichiNo_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KichiNo_textBox.Location = new System.Drawing.Point(109, 15);
            this.KichiNo_textBox.MaxLength = 4;
            this.KichiNo_textBox.Name = "KichiNo_textBox";
            this.KichiNo_textBox.Size = new System.Drawing.Size(156, 19);
            this.KichiNo_textBox.TabIndex = 0;
            this.KichiNo_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "※基地No";
            // 
            // Update_button
            // 
            this.Update_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Update_button.Location = new System.Drawing.Point(131, 564);
            this.Update_button.Name = "Update_button";
            this.Update_button.Size = new System.Drawing.Size(75, 23);
            this.Update_button.TabIndex = 20;
            this.Update_button.Text = "更新";
            this.Update_button.UseVisualStyleBackColor = true;
            this.Update_button.Click += new System.EventHandler(this.Update_button_Click);
            // 
            // Delete_button
            // 
            this.Delete_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Delete_button.Location = new System.Drawing.Point(212, 564);
            this.Delete_button.Name = "Delete_button";
            this.Delete_button.Size = new System.Drawing.Size(75, 23);
            this.Delete_button.TabIndex = 21;
            this.Delete_button.Text = "削除";
            this.Delete_button.UseVisualStyleBackColor = true;
            this.Delete_button.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // Close_button
            // 
            this.Close_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Close_button.Location = new System.Drawing.Point(293, 564);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(75, 23);
            this.Close_button.TabIndex = 22;
            this.Close_button.Text = "閉じる";
            this.Close_button.UseVisualStyleBackColor = true;
            this.Close_button.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "※基地名";
            // 
            // KichiName_textBox
            // 
            this.KichiName_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.KichiName_textBox.Location = new System.Drawing.Point(109, 40);
            this.KichiName_textBox.MaxLength = 50;
            this.KichiName_textBox.Name = "KichiName_textBox";
            this.KichiName_textBox.Size = new System.Drawing.Size(156, 19);
            this.KichiName_textBox.TabIndex = 1;
            this.KichiName_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // comboBox_Basho
            // 
            this.comboBox_Basho.FormattingEnabled = true;
            this.comboBox_Basho.Location = new System.Drawing.Point(109, 92);
            this.comboBox_Basho.Name = "comboBox_Basho";
            this.comboBox_Basho.Size = new System.Drawing.Size(177, 20);
            this.comboBox_Basho.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "港";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "入港～着桟";
            // 
            // textBox_ForNyukouToChakusan
            // 
            this.textBox_ForNyukouToChakusan.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_ForNyukouToChakusan.Location = new System.Drawing.Point(151, 135);
            this.textBox_ForNyukouToChakusan.MaxLength = 4;
            this.textBox_ForNyukouToChakusan.Name = "textBox_ForNyukouToChakusan";
            this.textBox_ForNyukouToChakusan.Size = new System.Drawing.Size(75, 19);
            this.textBox_ForNyukouToChakusan.TabIndex = 3;
            this.textBox_ForNyukouToChakusan.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(230, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "分";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "着桟可能時刻";
            // 
            // textBox_AvailableForChakusanFrom
            // 
            this.textBox_AvailableForChakusanFrom.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_AvailableForChakusanFrom.Location = new System.Drawing.Point(151, 163);
            this.textBox_AvailableForChakusanFrom.MaxLength = 4;
            this.textBox_AvailableForChakusanFrom.Name = "textBox_AvailableForChakusanFrom";
            this.textBox_AvailableForChakusanFrom.Size = new System.Drawing.Size(75, 19);
            this.textBox_AvailableForChakusanFrom.TabIndex = 4;
            this.textBox_AvailableForChakusanFrom.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // textBox_AvailableForNiyakuFrom
            // 
            this.textBox_AvailableForNiyakuFrom.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_AvailableForNiyakuFrom.Location = new System.Drawing.Point(151, 219);
            this.textBox_AvailableForNiyakuFrom.MaxLength = 4;
            this.textBox_AvailableForNiyakuFrom.Name = "textBox_AvailableForNiyakuFrom";
            this.textBox_AvailableForNiyakuFrom.Size = new System.Drawing.Size(75, 19);
            this.textBox_AvailableForNiyakuFrom.TabIndex = 7;
            this.textBox_AvailableForNiyakuFrom.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 222);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "荷役開始可能時刻";
            // 
            // textBox_ForChakusanToNiyaku
            // 
            this.textBox_ForChakusanToNiyaku.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_ForChakusanToNiyaku.Location = new System.Drawing.Point(151, 191);
            this.textBox_ForChakusanToNiyaku.MaxLength = 4;
            this.textBox_ForChakusanToNiyaku.Name = "textBox_ForChakusanToNiyaku";
            this.textBox_ForChakusanToNiyaku.Size = new System.Drawing.Size(75, 19);
            this.textBox_ForChakusanToNiyaku.TabIndex = 6;
            this.textBox_ForChakusanToNiyaku.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 194);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "着桟～荷役開始";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(230, 194);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 10;
            this.label9.Text = "分";
            // 
            // textBox_ForNiyakuToRisan
            // 
            this.textBox_ForNiyakuToRisan.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_ForNiyakuToRisan.Location = new System.Drawing.Point(151, 247);
            this.textBox_ForNiyakuToRisan.MaxLength = 4;
            this.textBox_ForNiyakuToRisan.Name = "textBox_ForNiyakuToRisan";
            this.textBox_ForNiyakuToRisan.Size = new System.Drawing.Size(75, 19);
            this.textBox_ForNiyakuToRisan.TabIndex = 9;
            this.textBox_ForNiyakuToRisan.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // textBox_AvailableForRisanFrom
            // 
            this.textBox_AvailableForRisanFrom.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_AvailableForRisanFrom.Location = new System.Drawing.Point(151, 275);
            this.textBox_AvailableForRisanFrom.MaxLength = 4;
            this.textBox_AvailableForRisanFrom.Name = "textBox_AvailableForRisanFrom";
            this.textBox_AvailableForRisanFrom.Size = new System.Drawing.Size(75, 19);
            this.textBox_AvailableForRisanFrom.TabIndex = 10;
            this.textBox_AvailableForRisanFrom.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(42, 250);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "荷役終了～離桟";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(42, 278);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "離桟可能時刻";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(232, 250);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 10;
            this.label12.Text = "分";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(366, 372);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "追加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(42, 386);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 10;
            this.label13.Text = "積揚可能貨物";
            // 
            // textBox_AvailableForChakusanTo
            // 
            this.textBox_AvailableForChakusanTo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_AvailableForChakusanTo.Location = new System.Drawing.Point(253, 163);
            this.textBox_AvailableForChakusanTo.MaxLength = 4;
            this.textBox_AvailableForChakusanTo.Name = "textBox_AvailableForChakusanTo";
            this.textBox_AvailableForChakusanTo.Size = new System.Drawing.Size(75, 19);
            this.textBox_AvailableForChakusanTo.TabIndex = 5;
            this.textBox_AvailableForChakusanTo.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // textBox_AvailableForNiyakuTo
            // 
            this.textBox_AvailableForNiyakuTo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_AvailableForNiyakuTo.Location = new System.Drawing.Point(253, 218);
            this.textBox_AvailableForNiyakuTo.MaxLength = 4;
            this.textBox_AvailableForNiyakuTo.Name = "textBox_AvailableForNiyakuTo";
            this.textBox_AvailableForNiyakuTo.Size = new System.Drawing.Size(75, 19);
            this.textBox_AvailableForNiyakuTo.TabIndex = 8;
            this.textBox_AvailableForNiyakuTo.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // textBox_AvailableForRisanTo
            // 
            this.textBox_AvailableForRisanTo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_AvailableForRisanTo.Location = new System.Drawing.Point(253, 275);
            this.textBox_AvailableForRisanTo.MaxLength = 4;
            this.textBox_AvailableForRisanTo.Name = "textBox_AvailableForRisanTo";
            this.textBox_AvailableForRisanTo.Size = new System.Drawing.Size(75, 19);
            this.textBox_AvailableForRisanTo.TabIndex = 11;
            this.textBox_AvailableForRisanTo.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(230, 166);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(17, 12);
            this.label14.TabIndex = 10;
            this.label14.Text = "～";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(230, 222);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 12);
            this.label15.TabIndex = 10;
            this.label15.Text = "～";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(230, 278);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(17, 12);
            this.label16.TabIndex = 10;
            this.label16.Text = "～";
            // 
            // gcMultiRow1
            // 
            this.gcMultiRow1.AllowUserToAddRows = false;
            this.gcMultiRow1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gcMultiRow1.HorizontalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.gcMultiRow1.Location = new System.Drawing.Point(44, 401);
            this.gcMultiRow1.Name = "gcMultiRow1";
            this.gcMultiRow1.Size = new System.Drawing.Size(410, 136);
            this.gcMultiRow1.TabIndex = 16;
            this.gcMultiRow1.Template = this.template11;
            this.gcMultiRow1.Text = "gcMultiRow1";
            this.gcMultiRow1.VerticalScrollBarMode = GrapeCity.Win.MultiRow.ScrollBarMode.Automatic;
            this.gcMultiRow1.CellValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.gcMultiRow1_CellValidating);
            this.gcMultiRow1.CellContentButtonClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.gcMultiRow1_CellContentButtonClick);
            this.gcMultiRow1.CellEditedFormattedValueChanged += new System.EventHandler<GrapeCity.Win.MultiRow.CellEditedFormattedValueChangedEventArgs>(this.gcMultiRow1_CellEditedFormattedValueChanged);
            // 
            // textBox_ForRisanToShukou
            // 
            this.textBox_ForRisanToShukou.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_ForRisanToShukou.Location = new System.Drawing.Point(151, 302);
            this.textBox_ForRisanToShukou.MaxLength = 4;
            this.textBox_ForRisanToShukou.Name = "textBox_ForRisanToShukou";
            this.textBox_ForRisanToShukou.Size = new System.Drawing.Size(75, 19);
            this.textBox_ForRisanToShukou.TabIndex = 12;
            this.textBox_ForRisanToShukou.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // textBox_AvailableForShukouFrom
            // 
            this.textBox_AvailableForShukouFrom.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_AvailableForShukouFrom.Location = new System.Drawing.Point(151, 329);
            this.textBox_AvailableForShukouFrom.MaxLength = 4;
            this.textBox_AvailableForShukouFrom.Name = "textBox_AvailableForShukouFrom";
            this.textBox_AvailableForShukouFrom.Size = new System.Drawing.Size(75, 19);
            this.textBox_AvailableForShukouFrom.TabIndex = 13;
            this.textBox_AvailableForShukouFrom.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // textBox_AvailableForShukouTo
            // 
            this.textBox_AvailableForShukouTo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_AvailableForShukouTo.Location = new System.Drawing.Point(253, 329);
            this.textBox_AvailableForShukouTo.MaxLength = 4;
            this.textBox_AvailableForShukouTo.Name = "textBox_AvailableForShukouTo";
            this.textBox_AvailableForShukouTo.Size = new System.Drawing.Size(75, 19);
            this.textBox_AvailableForShukouTo.TabIndex = 14;
            this.textBox_AvailableForShukouTo.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(42, 305);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 10;
            this.label17.Text = "離桟～出港";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(42, 332);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(77, 12);
            this.label18.TabIndex = 10;
            this.label18.Text = "出港可能時刻";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(232, 305);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(17, 12);
            this.label19.TabIndex = 10;
            this.label19.Text = "分";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(230, 335);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(17, 12);
            this.label20.TabIndex = 10;
            this.label20.Text = "～";
            // 
            // 基地管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(498, 607);
            this.Controls.Add(this.gcMultiRow1);
            this.Controls.Add(this.comboBox_Basho);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Delete_button);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Update_button);
            this.Controls.Add(this.KichiName_textBox);
            this.Controls.Add(this.textBox_AvailableForShukouTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_AvailableForShukouFrom);
            this.Controls.Add(this.textBox_AvailableForRisanTo);
            this.Controls.Add(this.textBox_AvailableForRisanFrom);
            this.Controls.Add(this.textBox_AvailableForNiyakuTo);
            this.Controls.Add(this.textBox_AvailableForNiyakuFrom);
            this.Controls.Add(this.textBox_AvailableForChakusanTo);
            this.Controls.Add(this.textBox_ForRisanToShukou);
            this.Controls.Add(this.textBox_AvailableForChakusanFrom);
            this.Controls.Add(this.textBox_ForNiyakuToRisan);
            this.Controls.Add(this.textBox_ForChakusanToNiyaku);
            this.Controls.Add(this.textBox_ForNyukouToChakusan);
            this.Controls.Add(this.KichiNo_textBox);
            this.Controls.Add(this.label1);
            this.Name = "基地管理詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "基地管理詳細Form";
            this.Load += new System.EventHandler(this.基地管理詳細Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMultiRow1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox KichiNo_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Update_button;
        private System.Windows.Forms.Button Delete_button;
        private System.Windows.Forms.Button Close_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox KichiName_textBox;
        private System.Windows.Forms.ComboBox comboBox_Basho;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ForNyukouToChakusan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_AvailableForChakusanFrom;
        private System.Windows.Forms.TextBox textBox_AvailableForNiyakuFrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_ForChakusanToNiyaku;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_ForNiyakuToRisan;
        private System.Windows.Forms.TextBox textBox_AvailableForRisanFrom;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private Template1 template11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_AvailableForChakusanTo;
        private System.Windows.Forms.TextBox textBox_AvailableForNiyakuTo;
        private System.Windows.Forms.TextBox textBox_AvailableForRisanTo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private System.Windows.Forms.TextBox textBox_ForRisanToShukou;
        private System.Windows.Forms.TextBox textBox_AvailableForShukouFrom;
        private System.Windows.Forms.TextBox textBox_AvailableForShukouTo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
    }
}