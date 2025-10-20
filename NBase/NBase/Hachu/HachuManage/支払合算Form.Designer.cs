namespace Hachu.HachuManage
{
    partial class 支払合算Form
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button詳細内容 = new System.Windows.Forms.Button();
            this.button合算作成 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox支払済 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox取引先 = new System.Windows.Forms.ComboBox();
            this.label発注先 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox詳細種別 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox種別 = new System.Windows.Forms.ComboBox();
            this.button条件クリア = new System.Windows.Forms.Button();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button検索 = new System.Windows.Forms.Button();
            this.treeListView支払合算一覧 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView支払合算一覧)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeListView支払合算一覧, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 185F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 426);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button詳細内容);
            this.panel1.Controls.Add(this.button合算作成);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 179);
            this.panel1.TabIndex = 2;
            // 
            // button詳細内容
            // 
            this.button詳細内容.Location = new System.Drawing.Point(84, 153);
            this.button詳細内容.Name = "button詳細内容";
            this.button詳細内容.Size = new System.Drawing.Size(75, 23);
            this.button詳細内容.TabIndex = 2;
            this.button詳細内容.Text = "詳細内容";
            this.button詳細内容.UseVisualStyleBackColor = true;
            this.button詳細内容.Click += new System.EventHandler(this.button詳細内容_Click);
            // 
            // button合算作成
            // 
            this.button合算作成.Location = new System.Drawing.Point(3, 153);
            this.button合算作成.Name = "button合算作成";
            this.button合算作成.Size = new System.Drawing.Size(75, 23);
            this.button合算作成.TabIndex = 1;
            this.button合算作成.Text = "合算作成";
            this.button合算作成.UseVisualStyleBackColor = true;
            this.button合算作成.Click += new System.EventHandler(this.button合算作成_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox支払済);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox取引先);
            this.groupBox1.Controls.Add(this.label発注先);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.comboBox詳細種別);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comboBox種別);
            this.groupBox1.Controls.Add(this.button条件クリア);
            this.groupBox1.Controls.Add(this.comboBox船);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(774, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // checkBox支払済
            // 
            this.checkBox支払済.AutoSize = true;
            this.checkBox支払済.Location = new System.Drawing.Point(103, 109);
            this.checkBox支払済.Name = "checkBox支払済";
            this.checkBox支払済.Size = new System.Drawing.Size(15, 14);
            this.checkBox支払済.TabIndex = 4;
            this.checkBox支払済.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "状態（支払済）";
            // 
            // comboBox取引先
            // 
            this.comboBox取引先.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox取引先.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBox取引先.DropDownWidth = 200;
            this.comboBox取引先.FormattingEnabled = true;
            this.comboBox取引先.ImeMode = System.Windows.Forms.ImeMode.On;
            this.comboBox取引先.Location = new System.Drawing.Point(103, 27);
            this.comboBox取引先.Name = "comboBox取引先";
            this.comboBox取引先.Size = new System.Drawing.Size(214, 20);
            this.comboBox取引先.TabIndex = 0;
            // 
            // label発注先
            // 
            this.label発注先.AutoSize = true;
            this.label発注先.Location = new System.Drawing.Point(37, 30);
            this.label発注先.Name = "label発注先";
            this.label発注先.Size = new System.Drawing.Size(53, 12);
            this.label発注先.TabIndex = 27;
            this.label発注先.Text = "取引先名";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(319, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 22;
            this.label9.Text = "詳細種別";
            // 
            // comboBox詳細種別
            // 
            this.comboBox詳細種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox詳細種別.FormattingEnabled = true;
            this.comboBox詳細種別.Items.AddRange(new object[] {
            "船用品",
            "潤滑油",
            "修繕"});
            this.comboBox詳細種別.Location = new System.Drawing.Point(378, 55);
            this.comboBox詳細種別.Name = "comboBox詳細種別";
            this.comboBox詳細種別.Size = new System.Drawing.Size(170, 20);
            this.comboBox詳細種別.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(61, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "種別";
            // 
            // comboBox種別
            // 
            this.comboBox種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox種別.FormattingEnabled = true;
            this.comboBox種別.Items.AddRange(new object[] {
            "船用品",
            "潤滑油",
            "修繕"});
            this.comboBox種別.Location = new System.Drawing.Point(103, 55);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(170, 20);
            this.comboBox種別.TabIndex = 1;
            this.comboBox種別.SelectedIndexChanged += new System.EventHandler(this.comboBox種別_SelectedIndexChanged);
            // 
            // button条件クリア
            // 
            this.button条件クリア.BackColor = System.Drawing.SystemColors.Control;
            this.button条件クリア.Location = new System.Drawing.Point(686, 47);
            this.button条件クリア.Name = "button条件クリア";
            this.button条件クリア.Size = new System.Drawing.Size(75, 23);
            this.button条件クリア.TabIndex = 6;
            this.button条件クリア.Text = "条件クリア";
            this.button条件クリア.UseVisualStyleBackColor = false;
            this.button条件クリア.Click += new System.EventHandler(this.button条件クリア_Click);
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(103, 83);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(170, 20);
            this.comboBox船.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "船名";
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(686, 18);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(75, 23);
            this.button検索.TabIndex = 5;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // treeListView支払合算一覧
            // 
            // 
            // 
            // 
            this.treeListView支払合算一覧.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView支払合算一覧.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView支払合算一覧.ContentPanel.Name = "";
            this.treeListView支払合算一覧.ContentPanel.Size = new System.Drawing.Size(780, 229);
            this.treeListView支払合算一覧.ContentPanel.TabIndex = 3;
            this.treeListView支払合算一覧.ContentPanel.TabStop = false;
            this.treeListView支払合算一覧.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView支払合算一覧.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView支払合算一覧.Footer = false;
            this.treeListView支払合算一覧.Location = new System.Drawing.Point(3, 188);
            this.treeListView支払合算一覧.Name = "treeListView支払合算一覧";
            this.treeListView支払合算一覧.Size = new System.Drawing.Size(786, 235);
            this.treeListView支払合算一覧.TabIndex = 0;
            this.treeListView支払合算一覧.Text = "treeListView1";
            this.treeListView支払合算一覧.WatermarkImage = watermarkImage2;
            // 
            // 支払合算Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 426);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "支払合算Form";
            this.Text = "支払合算Form";
            this.Load += new System.EventHandler(this.支払合算Form_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.支払合算Form_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView支払合算一覧)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button合算作成;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox詳細種別;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox種別;
        private System.Windows.Forms.Button button条件クリア;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button検索;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView支払合算一覧;
        private System.Windows.Forms.ComboBox comboBox取引先;
        private System.Windows.Forms.Label label発注先;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox支払済;
        private System.Windows.Forms.Button button詳細内容;

    }
}