namespace NBaseMaster.MsCustomer
{
    partial class 顧客管理詳細Form
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
            this.Cancel_Btn = new System.Windows.Forms.Button();
            this.Delete_Btn = new System.Windows.Forms.Button();
            this.Update_Btn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.listBox学科 = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox検船実施会社 = new System.Windows.Forms.CheckBox();
            this.checkBox申請先 = new System.Windows.Forms.CheckBox();
            this.checkBox取引先 = new System.Windows.Forms.CheckBox();
            this.checkBox学校 = new System.Windows.Forms.CheckBox();
            this.checkBox代理店 = new System.Windows.Forms.CheckBox();
            this.checkBox企業 = new System.Windows.Forms.CheckBox();
            this.checkBox荷主 = new System.Windows.Forms.CheckBox();
            this.button免許削除 = new System.Windows.Forms.Button();
            this.button学科削除 = new System.Windows.Forms.Button();
            this.button免許追加 = new System.Windows.Forms.Button();
            this.button学科追加 = new System.Windows.Forms.Button();
            this.AccountId_textBox = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.AccountNo_textBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox進路指導部先生名 = new System.Windows.Forms.TextBox();
            this.BranchName_textBox = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.textBox校長先生名 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.BankName_textBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CustomerId_textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Password_textBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.LoginID_textBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BuildingName_textBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Address2_textBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Address1_textBox = new System.Windows.Forms.TextBox();
            this.ZipCode_textBox = new System.Windows.Forms.TextBox();
            this.CustomerName_textBox = new System.Windows.Forms.TextBox();
            this.Fax_textBox = new System.Windows.Forms.TextBox();
            this.Tel_textBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cancel_Btn
            // 
            this.Cancel_Btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Cancel_Btn.BackColor = System.Drawing.Color.White;
            this.Cancel_Btn.Location = new System.Drawing.Point(680, 526);
            this.Cancel_Btn.Name = "Cancel_Btn";
            this.Cancel_Btn.Size = new System.Drawing.Size(95, 25);
            this.Cancel_Btn.TabIndex = 3;
            this.Cancel_Btn.Text = "閉じる";
            this.Cancel_Btn.UseVisualStyleBackColor = false;
            this.Cancel_Btn.Click += new System.EventHandler(this.Cancel_Btn_Click);
            // 
            // Delete_Btn
            // 
            this.Delete_Btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Delete_Btn.BackColor = System.Drawing.Color.White;
            this.Delete_Btn.Location = new System.Drawing.Point(566, 526);
            this.Delete_Btn.Name = "Delete_Btn";
            this.Delete_Btn.Size = new System.Drawing.Size(95, 25);
            this.Delete_Btn.TabIndex = 2;
            this.Delete_Btn.Text = "削除";
            this.Delete_Btn.UseVisualStyleBackColor = false;
            this.Delete_Btn.Click += new System.EventHandler(this.Delete_Btn_Click);
            // 
            // Update_Btn
            // 
            this.Update_Btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Update_Btn.BackColor = System.Drawing.Color.White;
            this.Update_Btn.Location = new System.Drawing.Point(452, 526);
            this.Update_Btn.Name = "Update_Btn";
            this.Update_Btn.Size = new System.Drawing.Size(95, 25);
            this.Update_Btn.TabIndex = 1;
            this.Update_Btn.Text = "更新";
            this.Update_Btn.UseVisualStyleBackColor = false;
            this.Update_Btn.Click += new System.EventHandler(this.Update_Btn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.listBox学科);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button免許削除);
            this.groupBox1.Controls.Add(this.button学科削除);
            this.groupBox1.Controls.Add(this.button免許追加);
            this.groupBox1.Controls.Add(this.button学科追加);
            this.groupBox1.Controls.Add(this.AccountId_textBox);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.AccountNo_textBox);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.textBox進路指導部先生名);
            this.groupBox1.Controls.Add(this.BranchName_textBox);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.textBox備考);
            this.groupBox1.Controls.Add(this.textBox校長先生名);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.BankName_textBox);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.CustomerId_textBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.Password_textBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.LoginID_textBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.BuildingName_textBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.Address2_textBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.Address1_textBox);
            this.groupBox1.Controls.Add(this.ZipCode_textBox);
            this.groupBox1.Controls.Add(this.CustomerName_textBox);
            this.groupBox1.Controls.Add(this.Fax_textBox);
            this.groupBox1.Controls.Add(this.Tel_textBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1209, 492);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(831, 288);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(226, 170);
            this.dataGridView1.TabIndex = 21;
            // 
            // listBox学科
            // 
            this.listBox学科.FormattingEnabled = true;
            this.listBox学科.ItemHeight = 12;
            this.listBox学科.Location = new System.Drawing.Point(831, 207);
            this.listBox学科.Name = "listBox学科";
            this.listBox学科.ScrollAlwaysVisible = true;
            this.listBox学科.Size = new System.Drawing.Size(226, 64);
            this.listBox学科.TabIndex = 18;
            this.listBox学科.SelectedIndexChanged += new System.EventHandler(this.listBox学科_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox検船実施会社);
            this.groupBox2.Controls.Add(this.checkBox申請先);
            this.groupBox2.Controls.Add(this.checkBox取引先);
            this.groupBox2.Controls.Add(this.checkBox学校);
            this.groupBox2.Controls.Add(this.checkBox代理店);
            this.groupBox2.Controls.Add(this.checkBox企業);
            this.groupBox2.Controls.Add(this.checkBox荷主);
            this.groupBox2.Location = new System.Drawing.Point(19, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(616, 50);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "種別";
            // 
            // checkBox検船実施会社
            // 
            this.checkBox検船実施会社.AutoSize = true;
            this.checkBox検船実施会社.Location = new System.Drawing.Point(466, 18);
            this.checkBox検船実施会社.Name = "checkBox検船実施会社";
            this.checkBox検船実施会社.Size = new System.Drawing.Size(96, 16);
            this.checkBox検船実施会社.TabIndex = 0;
            this.checkBox検船実施会社.Text = "検船実施会社";
            this.checkBox検船実施会社.UseVisualStyleBackColor = true;
            // 
            // checkBox申請先
            // 
            this.checkBox申請先.AutoSize = true;
            this.checkBox申請先.Location = new System.Drawing.Point(386, 18);
            this.checkBox申請先.Name = "checkBox申請先";
            this.checkBox申請先.Size = new System.Drawing.Size(60, 16);
            this.checkBox申請先.TabIndex = 6;
            this.checkBox申請先.Text = "申請先";
            this.checkBox申請先.UseVisualStyleBackColor = true;
            // 
            // checkBox取引先
            // 
            this.checkBox取引先.AutoSize = true;
            this.checkBox取引先.Location = new System.Drawing.Point(22, 18);
            this.checkBox取引先.Name = "checkBox取引先";
            this.checkBox取引先.Size = new System.Drawing.Size(60, 16);
            this.checkBox取引先.TabIndex = 1;
            this.checkBox取引先.Text = "取引先";
            this.checkBox取引先.UseVisualStyleBackColor = true;
            // 
            // checkBox学校
            // 
            this.checkBox学校.AutoSize = true;
            this.checkBox学校.Location = new System.Drawing.Point(318, 18);
            this.checkBox学校.Name = "checkBox学校";
            this.checkBox学校.Size = new System.Drawing.Size(48, 16);
            this.checkBox学校.TabIndex = 5;
            this.checkBox学校.Text = "学校";
            this.checkBox学校.UseVisualStyleBackColor = true;
            // 
            // checkBox代理店
            // 
            this.checkBox代理店.AutoSize = true;
            this.checkBox代理店.Location = new System.Drawing.Point(102, 18);
            this.checkBox代理店.Name = "checkBox代理店";
            this.checkBox代理店.Size = new System.Drawing.Size(60, 16);
            this.checkBox代理店.TabIndex = 2;
            this.checkBox代理店.Text = "代理店";
            this.checkBox代理店.UseVisualStyleBackColor = true;
            // 
            // checkBox企業
            // 
            this.checkBox企業.AutoSize = true;
            this.checkBox企業.Location = new System.Drawing.Point(250, 18);
            this.checkBox企業.Name = "checkBox企業";
            this.checkBox企業.Size = new System.Drawing.Size(48, 16);
            this.checkBox企業.TabIndex = 4;
            this.checkBox企業.Text = "企業";
            this.checkBox企業.UseVisualStyleBackColor = true;
            // 
            // checkBox荷主
            // 
            this.checkBox荷主.AutoSize = true;
            this.checkBox荷主.Location = new System.Drawing.Point(182, 18);
            this.checkBox荷主.Name = "checkBox荷主";
            this.checkBox荷主.Size = new System.Drawing.Size(48, 16);
            this.checkBox荷主.TabIndex = 3;
            this.checkBox荷主.Text = "荷主";
            this.checkBox荷主.UseVisualStyleBackColor = true;
            // 
            // button免許削除
            // 
            this.button免許削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button免許削除.BackColor = System.Drawing.Color.White;
            this.button免許削除.Location = new System.Drawing.Point(1063, 316);
            this.button免許削除.Name = "button免許削除";
            this.button免許削除.Size = new System.Drawing.Size(95, 25);
            this.button免許削除.TabIndex = 23;
            this.button免許削除.Text = "免許/免状削除";
            this.button免許削除.UseVisualStyleBackColor = false;
            this.button免許削除.Click += new System.EventHandler(this.button免許削除_Click);
            // 
            // button学科削除
            // 
            this.button学科削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button学科削除.BackColor = System.Drawing.Color.White;
            this.button学科削除.Location = new System.Drawing.Point(1063, 235);
            this.button学科削除.Name = "button学科削除";
            this.button学科削除.Size = new System.Drawing.Size(95, 25);
            this.button学科削除.TabIndex = 20;
            this.button学科削除.Text = "学科削除";
            this.button学科削除.UseVisualStyleBackColor = false;
            this.button学科削除.Click += new System.EventHandler(this.button学科削除_Click);
            // 
            // button免許追加
            // 
            this.button免許追加.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button免許追加.BackColor = System.Drawing.Color.White;
            this.button免許追加.Location = new System.Drawing.Point(1063, 288);
            this.button免許追加.Name = "button免許追加";
            this.button免許追加.Size = new System.Drawing.Size(95, 25);
            this.button免許追加.TabIndex = 22;
            this.button免許追加.Text = "免許/免状追加";
            this.button免許追加.UseVisualStyleBackColor = false;
            this.button免許追加.Click += new System.EventHandler(this.button免許追加_Click);
            // 
            // button学科追加
            // 
            this.button学科追加.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button学科追加.BackColor = System.Drawing.Color.White;
            this.button学科追加.Location = new System.Drawing.Point(1063, 207);
            this.button学科追加.Name = "button学科追加";
            this.button学科追加.Size = new System.Drawing.Size(95, 25);
            this.button学科追加.TabIndex = 19;
            this.button学科追加.Text = "学科追加";
            this.button学科追加.UseVisualStyleBackColor = false;
            this.button学科追加.Click += new System.EventHandler(this.button学科追加_Click);
            // 
            // AccountId_textBox
            // 
            this.AccountId_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.AccountId_textBox.Location = new System.Drawing.Point(433, 303);
            this.AccountId_textBox.MaxLength = 30;
            this.AccountId_textBox.Name = "AccountId_textBox";
            this.AccountId_textBox.Size = new System.Drawing.Size(226, 19);
            this.AccountId_textBox.TabIndex = 14;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(363, 306);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 31;
            this.label16.Text = "口座名義";
            // 
            // AccountNo_textBox
            // 
            this.AccountNo_textBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.AccountNo_textBox.Location = new System.Drawing.Point(433, 269);
            this.AccountNo_textBox.MaxLength = 30;
            this.AccountNo_textBox.Name = "AccountNo_textBox";
            this.AccountNo_textBox.Size = new System.Drawing.Size(226, 19);
            this.AccountNo_textBox.TabIndex = 13;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(363, 272);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 29;
            this.label17.Text = "口座番号";
            // 
            // textBox進路指導部先生名
            // 
            this.textBox進路指導部先生名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox進路指導部先生名.Location = new System.Drawing.Point(831, 168);
            this.textBox進路指導部先生名.MaxLength = 30;
            this.textBox進路指導部先生名.Name = "textBox進路指導部先生名";
            this.textBox進路指導部先生名.Size = new System.Drawing.Size(226, 19);
            this.textBox進路指導部先生名.TabIndex = 17;
            // 
            // BranchName_textBox
            // 
            this.BranchName_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.BranchName_textBox.Location = new System.Drawing.Point(433, 235);
            this.BranchName_textBox.MaxLength = 30;
            this.BranchName_textBox.Name = "BranchName_textBox";
            this.BranchName_textBox.Size = new System.Drawing.Size(226, 19);
            this.BranchName_textBox.TabIndex = 12;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(708, 170);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(101, 12);
            this.label19.TabIndex = 27;
            this.label19.Text = "進路指導部先生名";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(363, 238);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 27;
            this.label15.Text = "支店名";
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox備考.Location = new System.Drawing.Point(96, 377);
            this.textBox備考.MaxLength = 250;
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.Size = new System.Drawing.Size(563, 81);
            this.textBox備考.TabIndex = 15;
            // 
            // textBox校長先生名
            // 
            this.textBox校長先生名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox校長先生名.Location = new System.Drawing.Point(831, 134);
            this.textBox校長先生名.MaxLength = 30;
            this.textBox校長先生名.Name = "textBox校長先生名";
            this.textBox校長先生名.Size = new System.Drawing.Size(226, 19);
            this.textBox校長先生名.TabIndex = 16;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(40, 380);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 12);
            this.label20.TabIndex = 25;
            this.label20.Text = "備考";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(714, 288);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 12);
            this.label21.TabIndex = 25;
            this.label21.Text = "免許/免状";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(708, 207);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 25;
            this.label10.Text = "学部・学科";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(708, 137);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 25;
            this.label18.Text = "校長先生名";
            // 
            // BankName_textBox
            // 
            this.BankName_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.BankName_textBox.Location = new System.Drawing.Point(433, 201);
            this.BankName_textBox.MaxLength = 30;
            this.BankName_textBox.Name = "BankName_textBox";
            this.BankName_textBox.Size = new System.Drawing.Size(226, 19);
            this.BankName_textBox.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(363, 204);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 25;
            this.label14.Text = "銀行名";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(40, 104);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 12);
            this.label11.TabIndex = 21;
            this.label11.Text = "顧客No";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(17, 104);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 12);
            this.label13.TabIndex = 24;
            this.label13.Text = "※";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "顧客名";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(17, 137);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 23;
            this.label12.Text = "※";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "電話番号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 203);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "FAX";
            // 
            // CustomerId_textBox
            // 
            this.CustomerId_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CustomerId_textBox.Location = new System.Drawing.Point(96, 101);
            this.CustomerId_textBox.MaxLength = 7;
            this.CustomerId_textBox.Name = "CustomerId_textBox";
            this.CustomerId_textBox.Size = new System.Drawing.Size(226, 19);
            this.CustomerId_textBox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 236);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "郵便番号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 269);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "住所１";
            // 
            // Password_textBox
            // 
            this.Password_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Password_textBox.Location = new System.Drawing.Point(433, 167);
            this.Password_textBox.MaxLength = 30;
            this.Password_textBox.Name = "Password_textBox";
            this.Password_textBox.Size = new System.Drawing.Size(226, 19);
            this.Password_textBox.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 302);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "住所２";
            // 
            // LoginID_textBox
            // 
            this.LoginID_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.LoginID_textBox.Location = new System.Drawing.Point(433, 134);
            this.LoginID_textBox.MaxLength = 30;
            this.LoginID_textBox.Name = "LoginID_textBox";
            this.LoginID_textBox.Size = new System.Drawing.Size(226, 19);
            this.LoginID_textBox.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 335);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "建物名";
            // 
            // BuildingName_textBox
            // 
            this.BuildingName_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.BuildingName_textBox.Location = new System.Drawing.Point(96, 332);
            this.BuildingName_textBox.MaxLength = 50;
            this.BuildingName_textBox.Name = "BuildingName_textBox";
            this.BuildingName_textBox.Size = new System.Drawing.Size(226, 19);
            this.BuildingName_textBox.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(363, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "ログインID";
            // 
            // Address2_textBox
            // 
            this.Address2_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Address2_textBox.Location = new System.Drawing.Point(96, 299);
            this.Address2_textBox.MaxLength = 50;
            this.Address2_textBox.Name = "Address2_textBox";
            this.Address2_textBox.Size = new System.Drawing.Size(226, 19);
            this.Address2_textBox.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(363, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "パスワード";
            // 
            // Address1_textBox
            // 
            this.Address1_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Address1_textBox.Location = new System.Drawing.Point(96, 266);
            this.Address1_textBox.MaxLength = 50;
            this.Address1_textBox.Name = "Address1_textBox";
            this.Address1_textBox.Size = new System.Drawing.Size(226, 19);
            this.Address1_textBox.TabIndex = 6;
            // 
            // ZipCode_textBox
            // 
            this.ZipCode_textBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.ZipCode_textBox.Location = new System.Drawing.Point(96, 233);
            this.ZipCode_textBox.MaxLength = 10;
            this.ZipCode_textBox.Name = "ZipCode_textBox";
            this.ZipCode_textBox.Size = new System.Drawing.Size(226, 19);
            this.ZipCode_textBox.TabIndex = 5;
            // 
            // CustomerName_textBox
            // 
            this.CustomerName_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.CustomerName_textBox.Location = new System.Drawing.Point(96, 134);
            this.CustomerName_textBox.MaxLength = 50;
            this.CustomerName_textBox.Name = "CustomerName_textBox";
            this.CustomerName_textBox.Size = new System.Drawing.Size(226, 19);
            this.CustomerName_textBox.TabIndex = 2;
            // 
            // Fax_textBox
            // 
            this.Fax_textBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Fax_textBox.Location = new System.Drawing.Point(96, 200);
            this.Fax_textBox.MaxLength = 25;
            this.Fax_textBox.Name = "Fax_textBox";
            this.Fax_textBox.Size = new System.Drawing.Size(226, 19);
            this.Fax_textBox.TabIndex = 4;
            // 
            // Tel_textBox
            // 
            this.Tel_textBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Tel_textBox.Location = new System.Drawing.Point(96, 167);
            this.Tel_textBox.MaxLength = 25;
            this.Tel_textBox.Name = "Tel_textBox";
            this.Tel_textBox.Size = new System.Drawing.Size(226, 19);
            this.Tel_textBox.TabIndex = 3;
            // 
            // 顧客管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1236, 563);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Cancel_Btn);
            this.Controls.Add(this.Delete_Btn);
            this.Controls.Add(this.Update_Btn);
            this.Name = "顧客管理詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "顧客管理詳細Form";
            this.Load += new System.EventHandler(this.顧客管理詳細Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox Password_textBox;
        private System.Windows.Forms.TextBox LoginID_textBox;
        private System.Windows.Forms.TextBox BuildingName_textBox;
        private System.Windows.Forms.TextBox Address2_textBox;
        private System.Windows.Forms.TextBox Address1_textBox;
        private System.Windows.Forms.TextBox ZipCode_textBox;
        private System.Windows.Forms.TextBox Fax_textBox;
        private System.Windows.Forms.TextBox Tel_textBox;
        private System.Windows.Forms.TextBox CustomerName_textBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancel_Btn;
        private System.Windows.Forms.Button Delete_Btn;
        private System.Windows.Forms.Button Update_Btn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox CustomerId_textBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox AccountId_textBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox AccountNo_textBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox BranchName_textBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox BankName_textBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBox荷主;
        private System.Windows.Forms.CheckBox checkBox代理店;
        private System.Windows.Forms.CheckBox checkBox取引先;
        private System.Windows.Forms.CheckBox checkBox学校;
        private System.Windows.Forms.CheckBox checkBox企業;
        private System.Windows.Forms.TextBox textBox進路指導部先生名;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.TextBox textBox校長先生名;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListBox listBox学科;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button免許削除;
        private System.Windows.Forms.Button button学科削除;
        private System.Windows.Forms.Button button免許追加;
        private System.Windows.Forms.Button button学科追加;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox検船実施会社;
        private System.Windows.Forms.CheckBox checkBox申請先;
    }
}