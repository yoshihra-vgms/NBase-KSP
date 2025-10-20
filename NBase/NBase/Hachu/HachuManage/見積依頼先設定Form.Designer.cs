namespace Hachu.HachuManage
{
    partial class 見積依頼先設定Form
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
            LidorSystems.IntegralUI.Style.WatermarkImage watermarkImage2 = new LidorSystems.IntegralUI.Style.WatermarkImage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeListView = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxFAX番号 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox電話番号 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker希望納期 = new System.Windows.Forms.DateTimePicker();
            this.label希望納期 = new System.Windows.Forms.Label();
            this.label必須_件名 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox担当者 = new System.Windows.Forms.ComboBox();
            this.textBox見積回答期限 = new System.Windows.Forms.TextBox();
            this.label見積回答期限 = new System.Windows.Forms.Label();
            this.textBox見積依頼先 = new System.Windows.Forms.TextBox();
            this.textBoxメールアドレス = new System.Windows.Forms.TextBox();
            this.dateTimePicker見積依頼日 = new System.Windows.Forms.DateTimePicker();
            this.label担当者 = new System.Windows.Forms.Label();
            this.textBoxメール件名 = new System.Windows.Forms.TextBox();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.label備考 = new System.Windows.Forms.Label();
            this.textBox手配内容 = new System.Windows.Forms.TextBox();
            this.label手配内容 = new System.Windows.Forms.Label();
            this.comboBox見積依頼先 = new System.Windows.Forms.ComboBox();
            this.labelメール件名 = new System.Windows.Forms.Label();
            this.labelメールアドレス = new System.Windows.Forms.Label();
            this.label見積依頼先 = new System.Windows.Forms.Label();
            this.label見積依頼日 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button選択 = new System.Windows.Forms.Button();
            this.button解除 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonキャンセル = new System.Windows.Forms.Button();
            this.button見積依頼作成 = new System.Windows.Forms.Button();
            this.singleLineCombo見積依頼先 = new NBaseUtil.SingleLineCombo();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.treeListView, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 249F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 176F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(642, 522);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // treeListView
            // 
            this.treeListView.CheckBoxes = true;
            // 
            // 
            // 
            this.treeListView.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView.ContentPanel.Name = "";
            this.treeListView.ContentPanel.Size = new System.Drawing.Size(630, 164);
            this.treeListView.ContentPanel.TabIndex = 3;
            this.treeListView.ContentPanel.TabStop = false;
            this.treeListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView.Footer = false;
            this.treeListView.Location = new System.Drawing.Point(3, 292);
            this.treeListView.Name = "treeListView";
            this.treeListView.Size = new System.Drawing.Size(636, 170);
            this.treeListView.TabIndex = 3;
            this.treeListView.Text = "treeListView1";
            this.treeListView.WatermarkImage = watermarkImage2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.singleLineCombo見積依頼先);
            this.panel1.Controls.Add(this.textBoxFAX番号);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBox電話番号);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dateTimePicker希望納期);
            this.panel1.Controls.Add(this.label希望納期);
            this.panel1.Controls.Add(this.label必須_件名);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox担当者);
            this.panel1.Controls.Add(this.textBox見積回答期限);
            this.panel1.Controls.Add(this.label見積回答期限);
            this.panel1.Controls.Add(this.textBox見積依頼先);
            this.panel1.Controls.Add(this.textBoxメールアドレス);
            this.panel1.Controls.Add(this.dateTimePicker見積依頼日);
            this.panel1.Controls.Add(this.label担当者);
            this.panel1.Controls.Add(this.textBoxメール件名);
            this.panel1.Controls.Add(this.textBox備考);
            this.panel1.Controls.Add(this.label備考);
            this.panel1.Controls.Add(this.textBox手配内容);
            this.panel1.Controls.Add(this.label手配内容);
            this.panel1.Controls.Add(this.comboBox見積依頼先);
            this.panel1.Controls.Add(this.labelメール件名);
            this.panel1.Controls.Add(this.labelメールアドレス);
            this.panel1.Controls.Add(this.label見積依頼先);
            this.panel1.Controls.Add(this.label見積依頼日);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(636, 243);
            this.panel1.TabIndex = 1;
            // 
            // textBoxFAX番号
            // 
            this.textBoxFAX番号.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxFAX番号.Location = new System.Drawing.Point(292, 66);
            this.textBoxFAX番号.MaxLength = 25;
            this.textBoxFAX番号.Name = "textBoxFAX番号";
            this.textBoxFAX番号.Size = new System.Drawing.Size(120, 19);
            this.textBoxFAX番号.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(235, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 12);
            this.label6.TabIndex = 44;
            this.label6.Text = "FAX番号";
            // 
            // textBox電話番号
            // 
            this.textBox電話番号.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox電話番号.Location = new System.Drawing.Point(106, 66);
            this.textBox電話番号.MaxLength = 25;
            this.textBox電話番号.Name = "textBox電話番号";
            this.textBox電話番号.Size = new System.Drawing.Size(120, 19);
            this.textBox電話番号.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 42;
            this.label5.Text = "電話番号";
            // 
            // dateTimePicker希望納期
            // 
            this.dateTimePicker希望納期.Location = new System.Drawing.Point(414, 40);
            this.dateTimePicker希望納期.Name = "dateTimePicker希望納期";
            this.dateTimePicker希望納期.Size = new System.Drawing.Size(120, 19);
            this.dateTimePicker希望納期.TabIndex = 4;
            // 
            // label希望納期
            // 
            this.label希望納期.AutoSize = true;
            this.label希望納期.Location = new System.Drawing.Point(356, 43);
            this.label希望納期.Name = "label希望納期";
            this.label希望納期.Size = new System.Drawing.Size(53, 12);
            this.label希望納期.TabIndex = 23;
            this.label希望納期.Text = "希望納期";
            // 
            // label必須_件名
            // 
            this.label必須_件名.AutoSize = true;
            this.label必須_件名.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label必須_件名.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label必須_件名.Location = new System.Drawing.Point(22, 150);
            this.label必須_件名.Name = "label必須_件名";
            this.label必須_件名.Size = new System.Drawing.Size(17, 12);
            this.label必須_件名.TabIndex = 22;
            this.label必須_件名.Text = "※";
            this.label必須_件名.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(43, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "※";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(19, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 20;
            this.label1.Text = "※";
            // 
            // comboBox担当者
            // 
            this.comboBox担当者.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox担当者.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBox担当者.FormattingEnabled = true;
            this.comboBox担当者.ImeMode = System.Windows.Forms.ImeMode.On;
            this.comboBox担当者.Location = new System.Drawing.Point(106, 40);
            this.comboBox担当者.MaxLength = 50;
            this.comboBox担当者.Name = "comboBox担当者";
            this.comboBox担当者.Size = new System.Drawing.Size(214, 20);
            this.comboBox担当者.TabIndex = 3;
            this.comboBox担当者.SelectedIndexChanged += new System.EventHandler(this.comboBox担当者_SelectedIndexChanged);
            this.comboBox担当者.TextChanged += new System.EventHandler(this.comboBox担当者_TextChanged);
            // 
            // textBox見積回答期限
            // 
            this.textBox見積回答期限.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox見積回答期限.Location = new System.Drawing.Point(106, 96);
            this.textBox見積回答期限.MaxLength = 50;
            this.textBox見積回答期限.Name = "textBox見積回答期限";
            this.textBox見積回答期限.Size = new System.Drawing.Size(150, 19);
            this.textBox見積回答期限.TabIndex = 7;
            // 
            // label見積回答期限
            // 
            this.label見積回答期限.AutoSize = true;
            this.label見積回答期限.Location = new System.Drawing.Point(22, 99);
            this.label見積回答期限.Name = "label見積回答期限";
            this.label見積回答期限.Size = new System.Drawing.Size(77, 12);
            this.label見積回答期限.TabIndex = 18;
            this.label見積回答期限.Text = "見積回答期限";
            // 
            // textBox見積依頼先
            // 
            this.textBox見積依頼先.Location = new System.Drawing.Point(346, 96);
            this.textBox見積依頼先.Name = "textBox見積依頼先";
            this.textBox見積依頼先.ReadOnly = true;
            this.textBox見積依頼先.Size = new System.Drawing.Size(225, 19);
            this.textBox見積依頼先.TabIndex = 8;
            this.textBox見積依頼先.TabStop = false;
            // 
            // textBoxメールアドレス
            // 
            this.textBoxメールアドレス.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxメールアドレス.Location = new System.Drawing.Point(106, 122);
            this.textBoxメールアドレス.MaxLength = 50;
            this.textBoxメールアドレス.Name = "textBoxメールアドレス";
            this.textBoxメールアドレス.Size = new System.Drawing.Size(225, 19);
            this.textBoxメールアドレス.TabIndex = 9;
            this.textBoxメールアドレス.TextChanged += new System.EventHandler(this.textBoxメールアドレス_TextChanged);
            // 
            // dateTimePicker見積依頼日
            // 
            this.dateTimePicker見積依頼日.Location = new System.Drawing.Point(414, 15);
            this.dateTimePicker見積依頼日.Name = "dateTimePicker見積依頼日";
            this.dateTimePicker見積依頼日.Size = new System.Drawing.Size(120, 19);
            this.dateTimePicker見積依頼日.TabIndex = 2;
            // 
            // label担当者
            // 
            this.label担当者.AutoSize = true;
            this.label担当者.Location = new System.Drawing.Point(60, 43);
            this.label担当者.Name = "label担当者";
            this.label担当者.Size = new System.Drawing.Size(41, 12);
            this.label担当者.TabIndex = 0;
            this.label担当者.Text = "担当者";
            // 
            // textBoxメール件名
            // 
            this.textBoxメール件名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBoxメール件名.Location = new System.Drawing.Point(106, 147);
            this.textBoxメール件名.MaxLength = 50;
            this.textBoxメール件名.Name = "textBoxメール件名";
            this.textBoxメール件名.Size = new System.Drawing.Size(500, 19);
            this.textBoxメール件名.TabIndex = 11;
            // 
            // textBox備考
            // 
            this.textBox備考.Location = new System.Drawing.Point(106, 197);
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.ReadOnly = true;
            this.textBox備考.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox備考.Size = new System.Drawing.Size(500, 43);
            this.textBox備考.TabIndex = 13;
            this.textBox備考.TabStop = false;
            // 
            // label備考
            // 
            this.label備考.AutoSize = true;
            this.label備考.Location = new System.Drawing.Point(72, 200);
            this.label備考.Name = "label備考";
            this.label備考.Size = new System.Drawing.Size(29, 12);
            this.label備考.TabIndex = 0;
            this.label備考.Text = "備考";
            // 
            // textBox手配内容
            // 
            this.textBox手配内容.Location = new System.Drawing.Point(106, 172);
            this.textBox手配内容.Name = "textBox手配内容";
            this.textBox手配内容.ReadOnly = true;
            this.textBox手配内容.Size = new System.Drawing.Size(500, 19);
            this.textBox手配内容.TabIndex = 12;
            this.textBox手配内容.TabStop = false;
            // 
            // label手配内容
            // 
            this.label手配内容.AutoSize = true;
            this.label手配内容.Location = new System.Drawing.Point(48, 175);
            this.label手配内容.Name = "label手配内容";
            this.label手配内容.Size = new System.Drawing.Size(53, 12);
            this.label手配内容.TabIndex = 0;
            this.label手配内容.Text = "手配内容";
            // 
            // comboBox見積依頼先
            // 
            this.comboBox見積依頼先.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox見積依頼先.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBox見積依頼先.DropDownWidth = 200;
            this.comboBox見積依頼先.FormattingEnabled = true;
            this.comboBox見積依頼先.ImeMode = System.Windows.Forms.ImeMode.On;
            this.comboBox見積依頼先.Location = new System.Drawing.Point(106, 15);
            this.comboBox見積依頼先.Name = "comboBox見積依頼先";
            this.comboBox見積依頼先.Size = new System.Drawing.Size(214, 20);
            this.comboBox見積依頼先.TabIndex = 1;
            this.comboBox見積依頼先.Visible = false;
            this.comboBox見積依頼先.SelectedIndexChanged += new System.EventHandler(this.comboBox見積依頼先_SelectedIndexChanged);
            // 
            // labelメール件名
            // 
            this.labelメール件名.AutoSize = true;
            this.labelメール件名.Location = new System.Drawing.Point(44, 150);
            this.labelメール件名.Name = "labelメール件名";
            this.labelメール件名.Size = new System.Drawing.Size(57, 12);
            this.labelメール件名.TabIndex = 0;
            this.labelメール件名.Text = "メール件名";
            // 
            // labelメールアドレス
            // 
            this.labelメールアドレス.AutoSize = true;
            this.labelメールアドレス.Location = new System.Drawing.Point(31, 126);
            this.labelメールアドレス.Name = "labelメールアドレス";
            this.labelメールアドレス.Size = new System.Drawing.Size(69, 12);
            this.labelメールアドレス.TabIndex = 0;
            this.labelメールアドレス.Text = "メールアドレス";
            // 
            // label見積依頼先
            // 
            this.label見積依頼先.AutoSize = true;
            this.label見積依頼先.Location = new System.Drawing.Point(36, 18);
            this.label見積依頼先.Name = "label見積依頼先";
            this.label見積依頼先.Size = new System.Drawing.Size(65, 12);
            this.label見積依頼先.TabIndex = 0;
            this.label見積依頼先.Text = "見積依頼先";
            // 
            // label見積依頼日
            // 
            this.label見積依頼日.AutoSize = true;
            this.label見積依頼日.Location = new System.Drawing.Point(344, 18);
            this.label見積依頼日.Name = "label見積依頼日";
            this.label見積依頼日.Size = new System.Drawing.Size(65, 12);
            this.label見積依頼日.TabIndex = 0;
            this.label見積依頼日.Text = "見積依頼日";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button選択);
            this.flowLayoutPanel1.Controls.Add(this.button解除);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 252);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(636, 34);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // button選択
            // 
            this.button選択.Location = new System.Drawing.Point(3, 3);
            this.button選択.Name = "button選択";
            this.button選択.Size = new System.Drawing.Size(75, 23);
            this.button選択.TabIndex = 0;
            this.button選択.Text = "すべて選択";
            this.button選択.UseVisualStyleBackColor = true;
            this.button選択.Click += new System.EventHandler(this.button選択_Click);
            // 
            // button解除
            // 
            this.button解除.Location = new System.Drawing.Point(84, 3);
            this.button解除.Name = "button解除";
            this.button解除.Size = new System.Drawing.Size(75, 23);
            this.button解除.TabIndex = 1;
            this.button解除.Text = "すべて解除";
            this.button解除.UseVisualStyleBackColor = true;
            this.button解除.Click += new System.EventHandler(this.button解除_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonキャンセル);
            this.panel2.Controls.Add(this.button見積依頼作成);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 468);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(636, 51);
            this.panel2.TabIndex = 3;
            // 
            // buttonキャンセル
            // 
            this.buttonキャンセル.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonキャンセル.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonキャンセル.Location = new System.Drawing.Point(330, 9);
            this.buttonキャンセル.Name = "buttonキャンセル";
            this.buttonキャンセル.Size = new System.Drawing.Size(100, 31);
            this.buttonキャンセル.TabIndex = 4;
            this.buttonキャンセル.Text = "キャンセル";
            this.buttonキャンセル.UseVisualStyleBackColor = false;
            this.buttonキャンセル.Click += new System.EventHandler(this.buttonキャンセル_Click);
            // 
            // button見積依頼作成
            // 
            this.button見積依頼作成.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button見積依頼作成.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button見積依頼作成.Location = new System.Drawing.Point(206, 9);
            this.button見積依頼作成.Name = "button見積依頼作成";
            this.button見積依頼作成.Size = new System.Drawing.Size(100, 31);
            this.button見積依頼作成.TabIndex = 3;
            this.button見積依頼作成.Text = "見積依頼作成";
            this.button見積依頼作成.UseVisualStyleBackColor = false;
            this.button見積依頼作成.Click += new System.EventHandler(this.button見積依頼作成_Click);
            // 
            // singleLineCombo見積依頼先
            // 
            this.singleLineCombo見積依頼先.AutoSize = true;
            this.singleLineCombo見積依頼先.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.singleLineCombo見積依頼先.Location = new System.Drawing.Point(106, 15);
            this.singleLineCombo見積依頼先.MaxLength = 32767;
            this.singleLineCombo見積依頼先.Name = "singleLineCombo見積依頼先";
            this.singleLineCombo見積依頼先.ReadOnly = false;
            this.singleLineCombo見積依頼先.Size = new System.Drawing.Size(302, 19);
            this.singleLineCombo見積依頼先.TabIndex = 45;
            // 
            // 見積依頼先設定Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(642, 522);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "見積依頼先設定Form";
            this.Text = "見積依頼先設定";
            this.Load += new System.EventHandler(this.見積依頼先設定Form_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.Label label備考;
        private System.Windows.Forms.TextBox textBox手配内容;
        private System.Windows.Forms.Label label手配内容;
        private System.Windows.Forms.ComboBox comboBox見積依頼先;
        private System.Windows.Forms.Label labelメール件名;
        private System.Windows.Forms.Label labelメールアドレス;
        private System.Windows.Forms.Label label見積依頼先;
        private System.Windows.Forms.Label label見積依頼日;
        private System.Windows.Forms.TextBox textBoxメール件名;
        private System.Windows.Forms.Label label担当者;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker見積依頼日;
        private System.Windows.Forms.TextBox textBoxメールアドレス;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button見積依頼作成;
        private System.Windows.Forms.Button buttonキャンセル;
        private System.Windows.Forms.Button button選択;
        private System.Windows.Forms.Button button解除;
        private System.Windows.Forms.TextBox textBox見積依頼先;
        private System.Windows.Forms.TextBox textBox見積回答期限;
        private System.Windows.Forms.Label label見積回答期限;
        private System.Windows.Forms.ComboBox comboBox担当者;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label必須_件名;
        private System.Windows.Forms.DateTimePicker dateTimePicker希望納期;
        private System.Windows.Forms.Label label希望納期;
        private System.Windows.Forms.TextBox textBoxFAX番号;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox電話番号;
        private System.Windows.Forms.Label label5;
        private NBaseUtil.SingleLineCombo singleLineCombo見積依頼先;
    }
}