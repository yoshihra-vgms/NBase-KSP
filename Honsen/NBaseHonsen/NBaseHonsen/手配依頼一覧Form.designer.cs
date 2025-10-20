namespace NBaseHonsen
{
    partial class 手配依頼一覧Form
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
            LidorSystems.IntegralUI.Style.WatermarkImage watermarkImage1 = new LidorSystems.IntegralUI.Style.WatermarkImage();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button新規作成 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox船受領 = new System.Windows.Forms.CheckBox();
            this.checkBox受領済 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox完了 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.maskedTextBox手配依頼日終了 = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox発注済 = new System.Windows.Forms.CheckBox();
            this.checkBox見積中 = new System.Windows.Forms.CheckBox();
            this.checkBox未対応 = new System.Windows.Forms.CheckBox();
            this.maskedTextBox受領日終了 = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.maskedTextBox手配依頼日開始 = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.maskedTextBox受領日開始 = new System.Windows.Forms.MaskedTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.button検索 = new System.Windows.Forms.Button();
            this.button条件クリア = new System.Windows.Forms.Button();
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button閉じる.Location = new System.Drawing.Point(460, 495);
            this.button閉じる.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(149, 38);
            this.button閉じる.TabIndex = 8;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.closeButt_Click);
            // 
            // button新規作成
            // 
            this.button新規作成.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button新規作成.BackColor = System.Drawing.SystemColors.Control;
            this.button新規作成.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button新規作成.Location = new System.Drawing.Point(274, 495);
            this.button新規作成.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button新規作成.Name = "button新規作成";
            this.button新規作成.Size = new System.Drawing.Size(149, 38);
            this.button新規作成.TabIndex = 7;
            this.button新規作成.Text = "新規作成";
            this.button新規作成.UseVisualStyleBackColor = false;
            this.button新規作成.Click += new System.EventHandler(this.newButt_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(858, 475);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(852, 94);
            this.panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(852, 94);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.6974F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.3026F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(846, 72);
            this.tableLayoutPanel2.TabIndex = 45;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBox船受領);
            this.panel2.Controls.Add(this.checkBox受領済);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.checkBox完了);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.maskedTextBox手配依頼日終了);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.checkBox発注済);
            this.panel2.Controls.Add(this.checkBox見積中);
            this.panel2.Controls.Add(this.checkBox未対応);
            this.panel2.Controls.Add(this.maskedTextBox受領日終了);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.maskedTextBox手配依頼日開始);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.maskedTextBox受領日開始);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(719, 66);
            this.panel2.TabIndex = 46;
            // 
            // checkBox船受領
            // 
            this.checkBox船受領.AutoSize = true;
            this.checkBox船受領.Checked = true;
            this.checkBox船受領.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox船受領.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox船受領.Location = new System.Drawing.Point(392, 37);
            this.checkBox船受領.Name = "checkBox船受領";
            this.checkBox船受領.Size = new System.Drawing.Size(78, 20);
            this.checkBox船受領.TabIndex = 43;
            this.checkBox船受領.Text = "船受領";
            this.checkBox船受領.UseVisualStyleBackColor = true;
            // 
            // checkBox受領済
            // 
            this.checkBox受領済.AutoSize = true;
            this.checkBox受領済.Checked = true;
            this.checkBox受領済.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox受領済.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox受領済.Location = new System.Drawing.Point(481, 37);
            this.checkBox受領済.Name = "checkBox受領済";
            this.checkBox受領済.Size = new System.Drawing.Size(78, 20);
            this.checkBox受領済.TabIndex = 44;
            this.checkBox受領済.Text = "受領済";
            this.checkBox受領済.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(26, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 16);
            this.label3.TabIndex = 31;
            this.label3.Text = "手配依頼日";
            // 
            // checkBox完了
            // 
            this.checkBox完了.AutoSize = true;
            this.checkBox完了.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox完了.Location = new System.Drawing.Point(570, 37);
            this.checkBox完了.Name = "checkBox完了";
            this.checkBox完了.Size = new System.Drawing.Size(61, 20);
            this.checkBox完了.TabIndex = 45;
            this.checkBox完了.Text = "完了";
            this.checkBox完了.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(227, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 16);
            this.label4.TabIndex = 34;
            this.label4.Text = "～";
            // 
            // maskedTextBox手配依頼日終了
            // 
            this.maskedTextBox手配依頼日終了.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.maskedTextBox手配依頼日終了.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.maskedTextBox手配依頼日終了.Location = new System.Drawing.Point(253, 8);
            this.maskedTextBox手配依頼日終了.Mask = "0000/00/00";
            this.maskedTextBox手配依頼日終了.Name = "maskedTextBox手配依頼日終了";
            this.maskedTextBox手配依頼日終了.Size = new System.Drawing.Size(100, 23);
            this.maskedTextBox手配依頼日終了.TabIndex = 33;
            this.maskedTextBox手配依頼日終了.ValidatingType = typeof(System.DateTime);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(549, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 16);
            this.label5.TabIndex = 38;
            this.label5.Text = "～";
            // 
            // checkBox発注済
            // 
            this.checkBox発注済.AutoSize = true;
            this.checkBox発注済.Checked = true;
            this.checkBox発注済.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox発注済.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox発注済.Location = new System.Drawing.Point(303, 37);
            this.checkBox発注済.Name = "checkBox発注済";
            this.checkBox発注済.Size = new System.Drawing.Size(78, 20);
            this.checkBox発注済.TabIndex = 42;
            this.checkBox発注済.Text = "発注済";
            this.checkBox発注済.UseVisualStyleBackColor = true;
            // 
            // checkBox見積中
            // 
            this.checkBox見積中.AutoSize = true;
            this.checkBox見積中.Checked = true;
            this.checkBox見積中.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox見積中.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox見積中.Location = new System.Drawing.Point(214, 37);
            this.checkBox見積中.Name = "checkBox見積中";
            this.checkBox見積中.Size = new System.Drawing.Size(78, 20);
            this.checkBox見積中.TabIndex = 41;
            this.checkBox見積中.Text = "見積中";
            this.checkBox見積中.UseVisualStyleBackColor = true;
            // 
            // checkBox未対応
            // 
            this.checkBox未対応.AutoSize = true;
            this.checkBox未対応.Checked = true;
            this.checkBox未対応.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox未対応.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox未対応.Location = new System.Drawing.Point(125, 37);
            this.checkBox未対応.Name = "checkBox未対応";
            this.checkBox未対応.Size = new System.Drawing.Size(78, 20);
            this.checkBox未対応.TabIndex = 40;
            this.checkBox未対応.Text = "未対応";
            this.checkBox未対応.UseVisualStyleBackColor = true;
            // 
            // maskedTextBox受領日終了
            // 
            this.maskedTextBox受領日終了.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.maskedTextBox受領日終了.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.maskedTextBox受領日終了.Location = new System.Drawing.Point(575, 8);
            this.maskedTextBox受領日終了.Mask = "0000/00/00";
            this.maskedTextBox受領日終了.Name = "maskedTextBox受領日終了";
            this.maskedTextBox受領日終了.Size = new System.Drawing.Size(100, 23);
            this.maskedTextBox受領日終了.TabIndex = 37;
            this.maskedTextBox受領日終了.ValidatingType = typeof(System.DateTime);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(382, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 16);
            this.label6.TabIndex = 35;
            this.label6.Text = "受領日";
            // 
            // maskedTextBox手配依頼日開始
            // 
            this.maskedTextBox手配依頼日開始.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.maskedTextBox手配依頼日開始.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.maskedTextBox手配依頼日開始.Location = new System.Drawing.Point(125, 8);
            this.maskedTextBox手配依頼日開始.Mask = "0000/00/00";
            this.maskedTextBox手配依頼日開始.Name = "maskedTextBox手配依頼日開始";
            this.maskedTextBox手配依頼日開始.Size = new System.Drawing.Size(100, 23);
            this.maskedTextBox手配依頼日開始.TabIndex = 0;
            this.maskedTextBox手配依頼日開始.ValidatingType = typeof(System.DateTime);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(77, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 16);
            this.label7.TabIndex = 39;
            this.label7.Text = "状況";
            // 
            // maskedTextBox受領日開始
            // 
            this.maskedTextBox受領日開始.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.maskedTextBox受領日開始.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.maskedTextBox受領日開始.Location = new System.Drawing.Point(447, 7);
            this.maskedTextBox受領日開始.Mask = "0000/00/00";
            this.maskedTextBox受領日開始.Name = "maskedTextBox受領日開始";
            this.maskedTextBox受領日開始.Size = new System.Drawing.Size(100, 23);
            this.maskedTextBox受領日開始.TabIndex = 36;
            this.maskedTextBox受領日開始.ValidatingType = typeof(System.DateTime);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.button検索, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.button条件クリア, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(728, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(115, 66);
            this.tableLayoutPanel3.TabIndex = 47;
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Dock = System.Windows.Forms.DockStyle.Right;
            this.button検索.Location = new System.Drawing.Point(27, 3);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(85, 27);
            this.button検索.TabIndex = 44;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // button条件クリア
            // 
            this.button条件クリア.BackColor = System.Drawing.SystemColors.Control;
            this.button条件クリア.Dock = System.Windows.Forms.DockStyle.Right;
            this.button条件クリア.Location = new System.Drawing.Point(27, 36);
            this.button条件クリア.Name = "button条件クリア";
            this.button条件クリア.Size = new System.Drawing.Size(85, 27);
            this.button条件クリア.TabIndex = 45;
            this.button条件クリア.Text = "条件クリア";
            this.button条件クリア.UseVisualStyleBackColor = false;
            this.button条件クリア.Click += new System.EventHandler(this.button条件クリア_Click);
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
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(846, 363);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Location = new System.Drawing.Point(3, 103);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(852, 369);
            this.treeListView1.TabIndex = 1;
            this.treeListView1.Text = "dddddddddddddd";
            this.treeListView1.WatermarkImage = watermarkImage1;
            this.treeListView1.AfterCollapse += new LidorSystems.IntegralUI.ObjectEventHandler(this.treeListView1_AfterCollapse);
            this.treeListView1.AfterExpand += new LidorSystems.IntegralUI.ObjectEventHandler(this.treeListView1_AfterExpand);
            this.treeListView1.Click += new System.EventHandler(this.treeListView1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 551);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(882, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // 手配依頼一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(882, 573);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button新規作成);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "手配依頼一覧Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button新規作成;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox完了;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox maskedTextBox受領日終了;
        private System.Windows.Forms.MaskedTextBox maskedTextBox受領日開始;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox発注済;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox maskedTextBox手配依頼日終了;
        private System.Windows.Forms.CheckBox checkBox未対応;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox maskedTextBox手配依頼日開始;
        private System.Windows.Forms.CheckBox checkBox見積中;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.Button button条件クリア;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.CheckBox checkBox受領済;
        private System.Windows.Forms.CheckBox checkBox船受領;
    }
}