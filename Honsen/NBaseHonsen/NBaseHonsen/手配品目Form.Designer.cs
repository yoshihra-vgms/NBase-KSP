namespace NBaseHonsen
{
    partial class 手配品目Form
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
            this.comboBox区分 = new System.Windows.Forms.ComboBox();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button登録 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button詳細添付削除 = new System.Windows.Forms.Button();
            this.button詳細添付選択 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.multiLineCombo詳細品目 = new NBaseUtil.MultiLineCombo();
            this.labelMsVesselItemId = new System.Windows.Forms.Label();
            this.textBox船用品詳細品目 = new System.Windows.Forms.TextBox();
            this.button船用品選択 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button詳細品目削除 = new System.Windows.Forms.Button();
            this.comboBox単位 = new System.Windows.Forms.ComboBox();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.textBox在庫数 = new System.Windows.Forms.TextBox();
            this.textBox依頼数 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox詳細添付 = new System.Windows.Forms.TextBox();
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.button品目削除 = new System.Windows.Forms.Button();
            this.buttonカテゴリ選択 = new System.Windows.Forms.Button();
            this.textBox品目 = new System.Windows.Forms.TextBox();
            this.textBox仕様型式添付 = new System.Windows.Forms.TextBox();
            this.label仕様型式添付 = new System.Windows.Forms.Label();
            this.button仕様型式添付選択 = new System.Windows.Forms.Button();
            this.button仕様型式添付削除 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.multiLineCombo品目 = new NBaseUtil.MultiLineCombo();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "※区分";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "※仕様・型式";
            // 
            // comboBox区分
            // 
            this.comboBox区分.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox区分.Enabled = false;
            this.comboBox区分.FormattingEnabled = true;
            this.comboBox区分.Location = new System.Drawing.Point(140, 51);
            this.comboBox区分.Name = "comboBox区分";
            this.comboBox区分.Size = new System.Drawing.Size(121, 27);
            this.comboBox区分.TabIndex = 2;
            this.comboBox区分.Enter += new System.EventHandler(this.Control_Enter);
            this.comboBox区分.Leave += new System.EventHandler(this.Control_Leave);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(525, 646);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(149, 38);
            this.button閉じる.TabIndex = 7;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button登録
            // 
            this.button登録.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button登録.BackColor = System.Drawing.SystemColors.Control;
            this.button登録.Enabled = false;
            this.button登録.Location = new System.Drawing.Point(879, 18);
            this.button登録.Name = "button登録";
            this.button登録.Size = new System.Drawing.Size(149, 38);
            this.button登録.TabIndex = 5;
            this.button登録.Text = "登録";
            this.button登録.UseVisualStyleBackColor = false;
            this.button登録.Click += new System.EventHandler(this.button登録_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 212);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 325F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1170, 428);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button詳細添付削除);
            this.groupBox1.Controls.Add(this.button詳細添付選択);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.multiLineCombo詳細品目);
            this.groupBox1.Controls.Add(this.labelMsVesselItemId);
            this.groupBox1.Controls.Add(this.textBox船用品詳細品目);
            this.groupBox1.Controls.Add(this.button船用品選択);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.button詳細品目削除);
            this.groupBox1.Controls.Add(this.comboBox単位);
            this.groupBox1.Controls.Add(this.textBox備考);
            this.groupBox1.Controls.Add(this.textBox在庫数);
            this.groupBox1.Controls.Add(this.textBox依頼数);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox詳細添付);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1164, 319);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // button詳細添付削除
            // 
            this.button詳細添付削除.BackColor = System.Drawing.SystemColors.Control;
            this.button詳細添付削除.Enabled = false;
            this.button詳細添付削除.Location = new System.Drawing.Point(900, 214);
            this.button詳細添付削除.Name = "button詳細添付削除";
            this.button詳細添付削除.Size = new System.Drawing.Size(149, 38);
            this.button詳細添付削除.TabIndex = 38;
            this.button詳細添付削除.Text = "添付削除";
            this.button詳細添付削除.UseVisualStyleBackColor = false;
            this.button詳細添付削除.Click += new System.EventHandler(this.button詳細添付削除_Click);
            // 
            // button詳細添付選択
            // 
            this.button詳細添付選択.BackColor = System.Drawing.SystemColors.Control;
            this.button詳細添付選択.Enabled = false;
            this.button詳細添付選択.Location = new System.Drawing.Point(742, 214);
            this.button詳細添付選択.Name = "button詳細添付選択";
            this.button詳細添付選択.Size = new System.Drawing.Size(149, 38);
            this.button詳細添付選択.TabIndex = 37;
            this.button詳細添付選択.Text = "添付選択";
            this.button詳細添付選択.UseVisualStyleBackColor = false;
            this.button詳細添付選択.Click += new System.EventHandler(this.button詳細添付選択_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 224);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 19);
            this.label10.TabIndex = 36;
            this.label10.Text = "添付";
            // 
            // multiLineCombo詳細品目
            // 
            this.multiLineCombo詳細品目.AutoSize = true;
            this.multiLineCombo詳細品目.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.multiLineCombo詳細品目.Enabled = false;
            this.multiLineCombo詳細品目.ImeMode = System.Windows.Forms.ImeMode.On;
            this.multiLineCombo詳細品目.Location = new System.Drawing.Point(160, 19);
            this.multiLineCombo詳細品目.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.multiLineCombo詳細品目.MaxLength = 500;
            this.multiLineCombo詳細品目.Name = "multiLineCombo詳細品目";
            this.multiLineCombo詳細品目.ReadOnly = false;
            this.multiLineCombo詳細品目.Size = new System.Drawing.Size(572, 75);
            this.multiLineCombo詳細品目.TabIndex = 0;
            this.multiLineCombo詳細品目.Visible = false;
            this.multiLineCombo詳細品目.Enter += new System.EventHandler(this.Control_Enter);
            this.multiLineCombo詳細品目.Leave += new System.EventHandler(this.multiLineCombo詳細品目_Leave);
            // 
            // labelMsVesselItemId
            // 
            this.labelMsVesselItemId.AutoSize = true;
            this.labelMsVesselItemId.Location = new System.Drawing.Point(693, 145);
            this.labelMsVesselItemId.Name = "labelMsVesselItemId";
            this.labelMsVesselItemId.Size = new System.Drawing.Size(0, 19);
            this.labelMsVesselItemId.TabIndex = 34;
            this.labelMsVesselItemId.Visible = false;
            // 
            // textBox船用品詳細品目
            // 
            this.textBox船用品詳細品目.Location = new System.Drawing.Point(160, 19);
            this.textBox船用品詳細品目.Name = "textBox船用品詳細品目";
            this.textBox船用品詳細品目.ReadOnly = true;
            this.textBox船用品詳細品目.Size = new System.Drawing.Size(571, 26);
            this.textBox船用品詳細品目.TabIndex = 0;
            this.textBox船用品詳細品目.Visible = false;
            // 
            // button船用品選択
            // 
            this.button船用品選択.BackColor = System.Drawing.SystemColors.Control;
            this.button船用品選択.Enabled = false;
            this.button船用品選択.Location = new System.Drawing.Point(741, 63);
            this.button船用品選択.Name = "button船用品選択";
            this.button船用品選択.Size = new System.Drawing.Size(149, 38);
            this.button船用品選択.TabIndex = 1;
            this.button船用品選択.Text = "選択";
            this.button船用品選択.UseVisualStyleBackColor = false;
            this.button船用品選択.Visible = false;
            this.button船用品選択.Click += new System.EventHandler(this.button船用品選択_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(277, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 19);
            this.label5.TabIndex = 33;
            this.label5.Text = "在庫数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(475, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 19);
            this.label6.TabIndex = 32;
            this.label6.Text = "依頼数";
            // 
            // button詳細品目削除
            // 
            this.button詳細品目削除.BackColor = System.Drawing.SystemColors.Control;
            this.button詳細品目削除.Enabled = false;
            this.button詳細品目削除.Location = new System.Drawing.Point(741, 19);
            this.button詳細品目削除.Name = "button詳細品目削除";
            this.button詳細品目削除.Size = new System.Drawing.Size(149, 38);
            this.button詳細品目削除.TabIndex = 6;
            this.button詳細品目削除.Text = "詳細品目削除";
            this.button詳細品目削除.UseVisualStyleBackColor = false;
            this.button詳細品目削除.Click += new System.EventHandler(this.button詳細品目削除_Click);
            // 
            // comboBox単位
            // 
            this.comboBox単位.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox単位.Enabled = false;
            this.comboBox単位.FormattingEnabled = true;
            this.comboBox単位.Location = new System.Drawing.Point(160, 103);
            this.comboBox単位.Name = "comboBox単位";
            this.comboBox単位.Size = new System.Drawing.Size(77, 27);
            this.comboBox単位.TabIndex = 2;
            this.comboBox単位.Enter += new System.EventHandler(this.Control_Enter);
            this.comboBox単位.Leave += new System.EventHandler(this.comboBox単位_Leave);
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox備考.Location = new System.Drawing.Point(160, 139);
            this.textBox備考.MaxLength = 500;
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.ReadOnly = true;
            this.textBox備考.Size = new System.Drawing.Size(573, 76);
            this.textBox備考.TabIndex = 5;
            this.textBox備考.TextChanged += new System.EventHandler(this.textBox備考_TextChanged);
            this.textBox備考.Enter += new System.EventHandler(this.Control_Enter);
            this.textBox備考.Leave += new System.EventHandler(this.textBox備考_Leave);
            this.textBox備考.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.textBox備考_PreviewKeyDown);
            // 
            // textBox在庫数
            // 
            this.textBox在庫数.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox在庫数.Location = new System.Drawing.Point(357, 103);
            this.textBox在庫数.Name = "textBox在庫数";
            this.textBox在庫数.ReadOnly = true;
            this.textBox在庫数.Size = new System.Drawing.Size(70, 26);
            this.textBox在庫数.TabIndex = 3;
            this.textBox在庫数.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox在庫数.Enter += new System.EventHandler(this.Control_Enter);
            this.textBox在庫数.Leave += new System.EventHandler(this.textBox在庫数_Leave);
            // 
            // textBox依頼数
            // 
            this.textBox依頼数.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox依頼数.Location = new System.Drawing.Point(557, 103);
            this.textBox依頼数.Name = "textBox依頼数";
            this.textBox依頼数.ReadOnly = true;
            this.textBox依頼数.Size = new System.Drawing.Size(70, 26);
            this.textBox依頼数.TabIndex = 4;
            this.textBox依頼数.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox依頼数.Enter += new System.EventHandler(this.Control_Enter);
            this.textBox依頼数.Leave += new System.EventHandler(this.textBox依頼数_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 19);
            this.label4.TabIndex = 30;
            this.label4.Text = "単位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 38);
            this.label3.TabIndex = 24;
            this.label3.Text = "備考\n（品名、規格等）";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 19);
            this.label7.TabIndex = 25;
            this.label7.Text = "詳細品目";
            // 
            // textBox詳細添付
            // 
            this.textBox詳細添付.Location = new System.Drawing.Point(159, 221);
            this.textBox詳細添付.Name = "textBox詳細添付";
            this.textBox詳細添付.ReadOnly = true;
            this.textBox詳細添付.Size = new System.Drawing.Size(572, 26);
            this.textBox詳細添付.TabIndex = 35;
            this.textBox詳細添付.TabStop = false;
            // 
            // treeListView1
            // 
            this.treeListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.treeListView1.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView1.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView1.ContentPanel.Name = "";
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(1158, 91);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Location = new System.Drawing.Point(3, 3);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(1164, 97);
            this.treeListView1.TabIndex = 0;
            this.treeListView1.Text = "treeListView1";
            this.treeListView1.AfterSelect += new LidorSystems.IntegralUI.ObjectEventHandler(this.treeListView1_AfterSelect);
            this.treeListView1.BeforeSelect += new LidorSystems.IntegralUI.ObjectCancelEventHandler(this.treeListView1_BeforeSelect);
            this.treeListView1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.treeListView1_PreviewKeyDown);
            // 
            // button品目削除
            // 
            this.button品目削除.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button品目削除.BackColor = System.Drawing.SystemColors.Control;
            this.button品目削除.Enabled = false;
            this.button品目削除.Location = new System.Drawing.Point(1037, 18);
            this.button品目削除.Name = "button品目削除";
            this.button品目削除.Size = new System.Drawing.Size(149, 38);
            this.button品目削除.TabIndex = 6;
            this.button品目削除.Text = "仕様・型式削除";
            this.button品目削除.UseVisualStyleBackColor = false;
            this.button品目削除.Click += new System.EventHandler(this.button品目削除_Click);
            // 
            // buttonカテゴリ選択
            // 
            this.buttonカテゴリ選択.BackColor = System.Drawing.SystemColors.Control;
            this.buttonカテゴリ選択.Location = new System.Drawing.Point(721, 80);
            this.buttonカテゴリ選択.Name = "buttonカテゴリ選択";
            this.buttonカテゴリ選択.Size = new System.Drawing.Size(149, 38);
            this.buttonカテゴリ選択.TabIndex = 4;
            this.buttonカテゴリ選択.Text = "選択";
            this.buttonカテゴリ選択.UseVisualStyleBackColor = false;
            this.buttonカテゴリ選択.Click += new System.EventHandler(this.buttonカテゴリ選択_Click);
            // 
            // textBox品目
            // 
            this.textBox品目.Location = new System.Drawing.Point(140, 86);
            this.textBox品目.Name = "textBox品目";
            this.textBox品目.ReadOnly = true;
            this.textBox品目.Size = new System.Drawing.Size(572, 26);
            this.textBox品目.TabIndex = 3;
            this.textBox品目.TabStop = false;
            // 
            // textBox仕様型式添付
            // 
            this.textBox仕様型式添付.Location = new System.Drawing.Point(140, 169);
            this.textBox仕様型式添付.Name = "textBox仕様型式添付";
            this.textBox仕様型式添付.ReadOnly = true;
            this.textBox仕様型式添付.Size = new System.Drawing.Size(572, 26);
            this.textBox仕様型式添付.TabIndex = 8;
            this.textBox仕様型式添付.TabStop = false;
            // 
            // label仕様型式添付
            // 
            this.label仕様型式添付.AutoSize = true;
            this.label仕様型式添付.Location = new System.Drawing.Point(15, 172);
            this.label仕様型式添付.Name = "label仕様型式添付";
            this.label仕様型式添付.Size = new System.Drawing.Size(49, 19);
            this.label仕様型式添付.TabIndex = 9;
            this.label仕様型式添付.Text = "添付";
            // 
            // button仕様型式添付選択
            // 
            this.button仕様型式添付選択.BackColor = System.Drawing.SystemColors.Control;
            this.button仕様型式添付選択.Enabled = false;
            this.button仕様型式添付選択.Location = new System.Drawing.Point(723, 162);
            this.button仕様型式添付選択.Name = "button仕様型式添付選択";
            this.button仕様型式添付選択.Size = new System.Drawing.Size(149, 38);
            this.button仕様型式添付選択.TabIndex = 10;
            this.button仕様型式添付選択.Text = "添付選択";
            this.button仕様型式添付選択.UseVisualStyleBackColor = false;
            this.button仕様型式添付選択.Click += new System.EventHandler(this.button仕様型式添付選択_Click);
            // 
            // button仕様型式添付削除
            // 
            this.button仕様型式添付削除.BackColor = System.Drawing.SystemColors.Control;
            this.button仕様型式添付削除.Enabled = false;
            this.button仕様型式添付削除.Location = new System.Drawing.Point(881, 162);
            this.button仕様型式添付削除.Name = "button仕様型式添付削除";
            this.button仕様型式添付削除.Size = new System.Drawing.Size(149, 38);
            this.button仕様型式添付削除.TabIndex = 11;
            this.button仕様型式添付削除.Text = "添付削除";
            this.button仕様型式添付削除.UseVisualStyleBackColor = false;
            this.button仕様型式添付削除.Click += new System.EventHandler(this.button仕様型式添付削除_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // multiLineCombo品目
            // 
            this.multiLineCombo品目.AutoSize = true;
            this.multiLineCombo品目.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.multiLineCombo品目.Enabled = false;
            this.multiLineCombo品目.ImeMode = System.Windows.Forms.ImeMode.On;
            this.multiLineCombo品目.Location = new System.Drawing.Point(140, 86);
            this.multiLineCombo品目.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.multiLineCombo品目.MaxLength = 500;
            this.multiLineCombo品目.Name = "multiLineCombo品目";
            this.multiLineCombo品目.ReadOnly = false;
            this.multiLineCombo品目.Size = new System.Drawing.Size(572, 75);
            this.multiLineCombo品目.TabIndex = 3;
            this.multiLineCombo品目.Enter += new System.EventHandler(this.Control_Enter);
            this.multiLineCombo品目.Leave += new System.EventHandler(this.multiLineCombo品目_Leave);
            // 
            // 手配品目Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1198, 696);
            this.Controls.Add(this.button仕様型式添付削除);
            this.Controls.Add(this.button仕様型式添付選択);
            this.Controls.Add(this.label仕様型式添付);
            this.Controls.Add(this.buttonカテゴリ選択);
            this.Controls.Add(this.textBox品目);
            this.Controls.Add(this.button品目削除);
            this.Controls.Add(this.button登録);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.multiLineCombo品目);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.comboBox区分);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox仕様型式添付);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MinimumSize = new System.Drawing.Size(1058, 734);
            this.Name = "手配品目Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.手配品目Form_FormClosing);
            this.Load += new System.EventHandler(this.手配品目Form_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox区分;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button登録;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
        private System.Windows.Forms.Button button品目削除;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox単位;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.TextBox textBox在庫数;
        private System.Windows.Forms.TextBox textBox依頼数;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button詳細品目削除;
        private NBaseUtil.MultiLineCombo multiLineCombo品目;
        private NBaseUtil.MultiLineCombo multiLineCombo詳細品目;
        private System.Windows.Forms.TextBox textBox船用品詳細品目;
        private System.Windows.Forms.Button button船用品選択;
        private System.Windows.Forms.Label labelMsVesselItemId;
        private System.Windows.Forms.Button buttonカテゴリ選択;
        private System.Windows.Forms.TextBox textBox品目;
        private System.Windows.Forms.TextBox textBox仕様型式添付;
        private System.Windows.Forms.Label label仕様型式添付;
        private System.Windows.Forms.Button button仕様型式添付選択;
        private System.Windows.Forms.Button button仕様型式添付削除;
        private System.Windows.Forms.Button button詳細添付削除;
        private System.Windows.Forms.Button button詳細添付選択;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox詳細添付;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}