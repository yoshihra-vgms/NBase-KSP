namespace Hachu.HachuManage
{
    partial class 見積依頼Form
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel編集コマンド = new System.Windows.Forms.FlowLayoutPanel();
            this.button更新 = new System.Windows.Forms.Button();
            this.button取消 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox見積回答期限 = new System.Windows.Forms.TextBox();
            this.label見積回答期限 = new System.Windows.Forms.Label();
            this.button見積依頼書出力 = new System.Windows.Forms.Button();
            this.label見積依頼先 = new System.Windows.Forms.Label();
            this.treeListView見積依頼先 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.textBox送り先 = new System.Windows.Forms.TextBox();
            this.textBox作成者 = new System.Windows.Forms.TextBox();
            this.label作成者 = new System.Windows.Forms.Label();
            this.button見積依頼作成 = new System.Windows.Forms.Button();
            this.panel入渠 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox入渠科目 = new System.Windows.Forms.ComboBox();
            this.label内容 = new System.Windows.Forms.Label();
            this.textBox内容 = new System.Windows.Forms.TextBox();
            this.label入渠科目 = new System.Windows.Forms.Label();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.label備考 = new System.Windows.Forms.Label();
            this.textBox手配内容 = new System.Windows.Forms.TextBox();
            this.label手配内容 = new System.Windows.Forms.Label();
            this.label送り先 = new System.Windows.Forms.Label();
            this.comboBox支払条件 = new System.Windows.Forms.ComboBox();
            this.label支払条件 = new System.Windows.Forms.Label();
            this.textBox場所 = new System.Windows.Forms.TextBox();
            this.label場所 = new System.Windows.Forms.Label();
            this.textBox船 = new System.Windows.Forms.TextBox();
            this.textBox見積依頼番号 = new System.Windows.Forms.TextBox();
            this.label見積依頼番号 = new System.Windows.Forms.Label();
            this.label船 = new System.Windows.Forms.Label();
            this.treeListView = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel編集コマンド.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView見積依頼先)).BeginInit();
            this.panel入渠.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.treeListView, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 346F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(884, 616);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(878, 34);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel編集コマンド);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(872, 28);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel編集コマンド
            // 
            this.flowLayoutPanel編集コマンド.Controls.Add(this.button更新);
            this.flowLayoutPanel編集コマンド.Controls.Add(this.button取消);
            this.flowLayoutPanel編集コマンド.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel編集コマンド.Name = "flowLayoutPanel編集コマンド";
            this.flowLayoutPanel編集コマンド.Size = new System.Drawing.Size(319, 28);
            this.flowLayoutPanel編集コマンド.TabIndex = 5;
            // 
            // button更新
            // 
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Enabled = false;
            this.button更新.Location = new System.Drawing.Point(3, 3);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(101, 23);
            this.button更新.TabIndex = 3;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button取消
            // 
            this.button取消.BackColor = System.Drawing.SystemColors.Control;
            this.button取消.Enabled = false;
            this.button取消.Location = new System.Drawing.Point(110, 3);
            this.button取消.Name = "button取消";
            this.button取消.Size = new System.Drawing.Size(100, 23);
            this.button取消.TabIndex = 4;
            this.button取消.Text = "取消";
            this.button取消.UseVisualStyleBackColor = false;
            this.button取消.Click += new System.EventHandler(this.button取消_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox見積回答期限);
            this.panel1.Controls.Add(this.label見積回答期限);
            this.panel1.Controls.Add(this.button見積依頼書出力);
            this.panel1.Controls.Add(this.label見積依頼先);
            this.panel1.Controls.Add(this.treeListView見積依頼先);
            this.panel1.Controls.Add(this.textBox送り先);
            this.panel1.Controls.Add(this.textBox作成者);
            this.panel1.Controls.Add(this.label作成者);
            this.panel1.Controls.Add(this.button見積依頼作成);
            this.panel1.Controls.Add(this.panel入渠);
            this.panel1.Controls.Add(this.textBox備考);
            this.panel1.Controls.Add(this.label備考);
            this.panel1.Controls.Add(this.textBox手配内容);
            this.panel1.Controls.Add(this.label手配内容);
            this.panel1.Controls.Add(this.label送り先);
            this.panel1.Controls.Add(this.comboBox支払条件);
            this.panel1.Controls.Add(this.label支払条件);
            this.panel1.Controls.Add(this.textBox場所);
            this.panel1.Controls.Add(this.label場所);
            this.panel1.Controls.Add(this.textBox船);
            this.panel1.Controls.Add(this.textBox見積依頼番号);
            this.panel1.Controls.Add(this.label見積依頼番号);
            this.panel1.Controls.Add(this.label船);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(878, 340);
            this.panel1.TabIndex = 1;
            // 
            // textBox見積回答期限
            // 
            this.textBox見積回答期限.Enabled = false;
            this.textBox見積回答期限.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox見積回答期限.Location = new System.Drawing.Point(116, 155);
            this.textBox見積回答期限.MaxLength = 50;
            this.textBox見積回答期限.Name = "textBox見積回答期限";
            this.textBox見積回答期限.Size = new System.Drawing.Size(150, 19);
            this.textBox見積回答期限.TabIndex = 1;
            // 
            // label見積回答期限
            // 
            this.label見積回答期限.AutoSize = true;
            this.label見積回答期限.Location = new System.Drawing.Point(32, 158);
            this.label見積回答期限.Name = "label見積回答期限";
            this.label見積回答期限.Size = new System.Drawing.Size(77, 12);
            this.label見積回答期限.TabIndex = 0;
            this.label見積回答期限.Text = "見積回答期限";
            // 
            // button見積依頼書出力
            // 
            this.button見積依頼書出力.BackColor = System.Drawing.SystemColors.Control;
            this.button見積依頼書出力.Enabled = false;
            this.button見積依頼書出力.Location = new System.Drawing.Point(599, 89);
            this.button見積依頼書出力.Name = "button見積依頼書出力";
            this.button見積依頼書出力.Size = new System.Drawing.Size(100, 23);
            this.button見積依頼書出力.TabIndex = 11;
            this.button見積依頼書出力.Text = "見積依頼書出力";
            this.button見積依頼書出力.UseVisualStyleBackColor = false;
            this.button見積依頼書出力.Click += new System.EventHandler(this.button見積依頼書出力_Click);
            // 
            // label見積依頼先
            // 
            this.label見積依頼先.AutoSize = true;
            this.label見積依頼先.Location = new System.Drawing.Point(44, 65);
            this.label見積依頼先.Name = "label見積依頼先";
            this.label見積依頼先.Size = new System.Drawing.Size(65, 12);
            this.label見積依頼先.TabIndex = 14;
            this.label見積依頼先.Text = "見積依頼先";
            // 
            // treeListView見積依頼先
            // 
            // 
            // 
            // 
            this.treeListView見積依頼先.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView見積依頼先.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView見積依頼先.ContentPanel.Name = "";
            this.treeListView見積依頼先.ContentPanel.Size = new System.Drawing.Size(472, 79);
            this.treeListView見積依頼先.ContentPanel.TabIndex = 3;
            this.treeListView見積依頼先.ContentPanel.TabStop = false;
            this.treeListView見積依頼先.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView見積依頼先.Footer = false;
            this.treeListView見積依頼先.Location = new System.Drawing.Point(115, 61);
            this.treeListView見積依頼先.Name = "treeListView見積依頼先";
            this.treeListView見積依頼先.Size = new System.Drawing.Size(478, 85);
            this.treeListView見積依頼先.TabIndex = 0;
            this.treeListView見積依頼先.Text = "treeListView1";
            // 
            // textBox送り先
            // 
            this.textBox送り先.Enabled = false;
            this.textBox送り先.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox送り先.Location = new System.Drawing.Point(116, 206);
            this.textBox送り先.MaxLength = 50;
            this.textBox送り先.Multiline = true;
            this.textBox送り先.Name = "textBox送り先";
            this.textBox送り先.Size = new System.Drawing.Size(306, 53);
            this.textBox送り先.TabIndex = 3;
            // 
            // textBox作成者
            // 
            this.textBox作成者.Location = new System.Drawing.Point(418, 10);
            this.textBox作成者.Name = "textBox作成者";
            this.textBox作成者.ReadOnly = true;
            this.textBox作成者.Size = new System.Drawing.Size(150, 19);
            this.textBox作成者.TabIndex = 0;
            this.textBox作成者.TabStop = false;
            // 
            // label作成者
            // 
            this.label作成者.AutoSize = true;
            this.label作成者.Location = new System.Drawing.Point(373, 13);
            this.label作成者.Name = "label作成者";
            this.label作成者.Size = new System.Drawing.Size(41, 12);
            this.label作成者.TabIndex = 0;
            this.label作成者.Text = "作成者";
            // 
            // button見積依頼作成
            // 
            this.button見積依頼作成.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button見積依頼作成.Enabled = false;
            this.button見積依頼作成.Location = new System.Drawing.Point(599, 60);
            this.button見積依頼作成.Name = "button見積依頼作成";
            this.button見積依頼作成.Size = new System.Drawing.Size(100, 23);
            this.button見積依頼作成.TabIndex = 10;
            this.button見積依頼作成.Text = "見積依頼作成";
            this.button見積依頼作成.UseVisualStyleBackColor = false;
            this.button見積依頼作成.Click += new System.EventHandler(this.button見積依頼作成_Click);
            // 
            // panel入渠
            // 
            this.panel入渠.Controls.Add(this.label1);
            this.panel入渠.Controls.Add(this.comboBox入渠科目);
            this.panel入渠.Controls.Add(this.label内容);
            this.panel入渠.Controls.Add(this.textBox内容);
            this.panel入渠.Controls.Add(this.label入渠科目);
            this.panel入渠.Location = new System.Drawing.Point(322, 152);
            this.panel入渠.Name = "panel入渠";
            this.panel入渠.Size = new System.Drawing.Size(271, 48);
            this.panel入渠.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "※";
            // 
            // comboBox入渠科目
            // 
            this.comboBox入渠科目.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox入渠科目.Enabled = false;
            this.comboBox入渠科目.FormattingEnabled = true;
            this.comboBox入渠科目.Location = new System.Drawing.Point(88, 3);
            this.comboBox入渠科目.Name = "comboBox入渠科目";
            this.comboBox入渠科目.Size = new System.Drawing.Size(150, 20);
            this.comboBox入渠科目.TabIndex = 4;
            // 
            // label内容
            // 
            this.label内容.AutoSize = true;
            this.label内容.Location = new System.Drawing.Point(50, 31);
            this.label内容.Name = "label内容";
            this.label内容.Size = new System.Drawing.Size(29, 12);
            this.label内容.TabIndex = 0;
            this.label内容.Text = "内容";
            // 
            // textBox内容
            // 
            this.textBox内容.Enabled = false;
            this.textBox内容.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox内容.Location = new System.Drawing.Point(88, 28);
            this.textBox内容.MaxLength = 20;
            this.textBox内容.Name = "textBox内容";
            this.textBox内容.Size = new System.Drawing.Size(150, 19);
            this.textBox内容.TabIndex = 5;
            // 
            // label入渠科目
            // 
            this.label入渠科目.AutoSize = true;
            this.label入渠科目.Location = new System.Drawing.Point(27, 7);
            this.label入渠科目.Name = "label入渠科目";
            this.label入渠科目.Size = new System.Drawing.Size(53, 12);
            this.label入渠科目.TabIndex = 0;
            this.label入渠科目.Text = "入渠科目";
            // 
            // textBox備考
            // 
            this.textBox備考.Location = new System.Drawing.Point(115, 291);
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.ReadOnly = true;
            this.textBox備考.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox備考.Size = new System.Drawing.Size(500, 43);
            this.textBox備考.TabIndex = 0;
            this.textBox備考.TabStop = false;
            // 
            // label備考
            // 
            this.label備考.AutoSize = true;
            this.label備考.Location = new System.Drawing.Point(80, 294);
            this.label備考.Name = "label備考";
            this.label備考.Size = new System.Drawing.Size(29, 12);
            this.label備考.TabIndex = 0;
            this.label備考.Text = "備考";
            // 
            // textBox手配内容
            // 
            this.textBox手配内容.Location = new System.Drawing.Point(115, 266);
            this.textBox手配内容.Name = "textBox手配内容";
            this.textBox手配内容.ReadOnly = true;
            this.textBox手配内容.Size = new System.Drawing.Size(500, 19);
            this.textBox手配内容.TabIndex = 0;
            this.textBox手配内容.TabStop = false;
            // 
            // label手配内容
            // 
            this.label手配内容.AutoSize = true;
            this.label手配内容.Location = new System.Drawing.Point(56, 269);
            this.label手配内容.Name = "label手配内容";
            this.label手配内容.Size = new System.Drawing.Size(53, 12);
            this.label手配内容.TabIndex = 0;
            this.label手配内容.Text = "手配内容";
            // 
            // label送り先
            // 
            this.label送り先.AutoSize = true;
            this.label送り先.Location = new System.Drawing.Point(72, 209);
            this.label送り先.Name = "label送り先";
            this.label送り先.Size = new System.Drawing.Size(37, 12);
            this.label送り先.TabIndex = 0;
            this.label送り先.Text = "送り先";
            // 
            // comboBox支払条件
            // 
            this.comboBox支払条件.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox支払条件.Enabled = false;
            this.comboBox支払条件.FormattingEnabled = true;
            this.comboBox支払条件.Location = new System.Drawing.Point(115, 180);
            this.comboBox支払条件.Name = "comboBox支払条件";
            this.comboBox支払条件.Size = new System.Drawing.Size(201, 20);
            this.comboBox支払条件.TabIndex = 2;
            // 
            // label支払条件
            // 
            this.label支払条件.AutoSize = true;
            this.label支払条件.Location = new System.Drawing.Point(56, 183);
            this.label支払条件.Name = "label支払条件";
            this.label支払条件.Size = new System.Drawing.Size(53, 12);
            this.label支払条件.TabIndex = 0;
            this.label支払条件.Text = "支払条件";
            // 
            // textBox場所
            // 
            this.textBox場所.Location = new System.Drawing.Point(418, 35);
            this.textBox場所.Name = "textBox場所";
            this.textBox場所.ReadOnly = true;
            this.textBox場所.Size = new System.Drawing.Size(150, 19);
            this.textBox場所.TabIndex = 0;
            this.textBox場所.TabStop = false;
            // 
            // label場所
            // 
            this.label場所.AutoSize = true;
            this.label場所.Location = new System.Drawing.Point(385, 38);
            this.label場所.Name = "label場所";
            this.label場所.Size = new System.Drawing.Size(29, 12);
            this.label場所.TabIndex = 0;
            this.label場所.Text = "場所";
            // 
            // textBox船
            // 
            this.textBox船.Location = new System.Drawing.Point(115, 35);
            this.textBox船.Name = "textBox船";
            this.textBox船.ReadOnly = true;
            this.textBox船.Size = new System.Drawing.Size(150, 19);
            this.textBox船.TabIndex = 0;
            this.textBox船.TabStop = false;
            // 
            // textBox見積依頼番号
            // 
            this.textBox見積依頼番号.Location = new System.Drawing.Point(115, 10);
            this.textBox見積依頼番号.Name = "textBox見積依頼番号";
            this.textBox見積依頼番号.ReadOnly = true;
            this.textBox見積依頼番号.Size = new System.Drawing.Size(190, 19);
            this.textBox見積依頼番号.TabIndex = 0;
            this.textBox見積依頼番号.TabStop = false;
            this.textBox見積依頼番号.Text = "見積依頼作成時に自動採番します";
            // 
            // label見積依頼番号
            // 
            this.label見積依頼番号.AutoSize = true;
            this.label見積依頼番号.Location = new System.Drawing.Point(32, 13);
            this.label見積依頼番号.Name = "label見積依頼番号";
            this.label見積依頼番号.Size = new System.Drawing.Size(77, 12);
            this.label見積依頼番号.TabIndex = 0;
            this.label見積依頼番号.Text = "見積依頼番号";
            // 
            // label船
            // 
            this.label船.AutoSize = true;
            this.label船.Location = new System.Drawing.Point(92, 38);
            this.label船.Name = "label船";
            this.label船.Size = new System.Drawing.Size(17, 12);
            this.label船.TabIndex = 0;
            this.label船.Text = "船";
            // 
            // treeListView
            // 
            // 
            // 
            // 
            this.treeListView.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView.ContentPanel.Name = "";
            this.treeListView.ContentPanel.Size = new System.Drawing.Size(872, 218);
            this.treeListView.ContentPanel.TabIndex = 3;
            this.treeListView.ContentPanel.TabStop = false;
            this.treeListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView.Footer = false;
            this.treeListView.Location = new System.Drawing.Point(3, 389);
            this.treeListView.Name = "treeListView";
            this.treeListView.Size = new System.Drawing.Size(878, 224);
            this.treeListView.TabIndex = 3;
            this.treeListView.Text = "treeListView1";
            // 
            // 見積依頼Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "見積依頼Form";
            this.Size = new System.Drawing.Size(884, 616);
            this.Load += new System.EventHandler(this.見積依頼Form_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel編集コマンド.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView見積依頼先)).EndInit();
            this.panel入渠.ResumeLayout(false);
            this.panel入渠.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView;
        private System.Windows.Forms.Button button見積依頼作成;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Button button取消;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.Label label備考;
        private System.Windows.Forms.TextBox textBox手配内容;
        private System.Windows.Forms.Label label手配内容;
        private System.Windows.Forms.Label label送り先;
        private System.Windows.Forms.ComboBox comboBox支払条件;
        private System.Windows.Forms.Label label支払条件;
        private System.Windows.Forms.TextBox textBox場所;
        private System.Windows.Forms.Label label場所;
        private System.Windows.Forms.TextBox textBox船;
        private System.Windows.Forms.TextBox textBox見積依頼番号;
        private System.Windows.Forms.Label label見積依頼番号;
        private System.Windows.Forms.Label label船;
        private System.Windows.Forms.Label label入渠科目;
        private System.Windows.Forms.TextBox textBox内容;
        private System.Windows.Forms.Label label内容;
        private System.Windows.Forms.Panel panel入渠;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel編集コマンド;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox textBox作成者;
        private System.Windows.Forms.Label label作成者;
        private System.Windows.Forms.ComboBox comboBox入渠科目;
        private System.Windows.Forms.TextBox textBox送り先;
        private System.Windows.Forms.Label label見積依頼先;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView見積依頼先;
        private System.Windows.Forms.Button button見積依頼書出力;
        private System.Windows.Forms.TextBox textBox見積回答期限;
        private System.Windows.Forms.Label label見積回答期限;
        private System.Windows.Forms.Label label1;
    }
}