namespace Hachu.HachuManage
{
    partial class 日付調整一覧Form
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox詳細種別 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox種別 = new System.Windows.Forms.ComboBox();
            this.button条件クリア = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.maskedTextBox発注日To = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox発注日From = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button検索 = new System.Windows.Forms.Button();
            this.treeListView日付調整一覧 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.checkBox_日付逆転 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView日付調整一覧)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_日付逆転);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.comboBox詳細種別);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comboBox種別);
            this.groupBox1.Controls.Add(this.button条件クリア);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.maskedTextBox発注日To);
            this.groupBox1.Controls.Add(this.maskedTextBox発注日From);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox船);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(744, 114);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(284, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
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
            this.comboBox詳細種別.Location = new System.Drawing.Point(349, 49);
            this.comboBox詳細種別.Name = "comboBox詳細種別";
            this.comboBox詳細種別.Size = new System.Drawing.Size(170, 20);
            this.comboBox詳細種別.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 0;
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
            this.comboBox種別.Location = new System.Drawing.Point(87, 49);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(170, 20);
            this.comboBox種別.TabIndex = 3;
            this.comboBox種別.SelectedIndexChanged += new System.EventHandler(this.comboBox種別_SelectedIndexChanged);
            // 
            // button条件クリア
            // 
            this.button条件クリア.BackColor = System.Drawing.SystemColors.Control;
            this.button条件クリア.Location = new System.Drawing.Point(654, 50);
            this.button条件クリア.Name = "button条件クリア";
            this.button条件クリア.Size = new System.Drawing.Size(75, 23);
            this.button条件クリア.TabIndex = 16;
            this.button条件クリア.Text = "条件クリア";
            this.button条件クリア.UseVisualStyleBackColor = false;
            this.button条件クリア.Click += new System.EventHandler(this.button条件クリア_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "～";
            // 
            // maskedTextBox発注日To
            // 
            this.maskedTextBox発注日To.Location = new System.Drawing.Point(188, 77);
            this.maskedTextBox発注日To.Mask = "0000/00/00";
            this.maskedTextBox発注日To.Name = "maskedTextBox発注日To";
            this.maskedTextBox発注日To.Size = new System.Drawing.Size(69, 19);
            this.maskedTextBox発注日To.TabIndex = 6;
            this.maskedTextBox発注日To.ValidatingType = typeof(System.DateTime);
            // 
            // maskedTextBox発注日From
            // 
            this.maskedTextBox発注日From.Location = new System.Drawing.Point(87, 76);
            this.maskedTextBox発注日From.Mask = "0000/00/00";
            this.maskedTextBox発注日From.Name = "maskedTextBox発注日From";
            this.maskedTextBox発注日From.Size = new System.Drawing.Size(69, 19);
            this.maskedTextBox発注日From.TabIndex = 5;
            this.maskedTextBox発注日From.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "発注日";
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(87, 21);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(170, 20);
            this.comboBox船.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "船";
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(654, 19);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(75, 23);
            this.button検索.TabIndex = 15;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // treeListView日付調整一覧
            // 
            this.treeListView日付調整一覧.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.treeListView日付調整一覧.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView日付調整一覧.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView日付調整一覧.ContentPanel.Name = "";
            this.treeListView日付調整一覧.ContentPanel.Size = new System.Drawing.Size(733, 21);
            this.treeListView日付調整一覧.ContentPanel.TabIndex = 3;
            this.treeListView日付調整一覧.ContentPanel.TabStop = false;
            this.treeListView日付調整一覧.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView日付調整一覧.Footer = false;
            this.treeListView日付調整一覧.Location = new System.Drawing.Point(12, 132);
            this.treeListView日付調整一覧.Name = "treeListView日付調整一覧";
            this.treeListView日付調整一覧.Size = new System.Drawing.Size(739, 27);
            this.treeListView日付調整一覧.TabIndex = 2;
            this.treeListView日付調整一覧.Text = "treeListView1";
            this.treeListView日付調整一覧.WatermarkImage = watermarkImage1;
            this.treeListView日付調整一覧.DoubleClick += new System.EventHandler(this.treeListView日付調整一覧_DoubleClick);
            // 
            // checkBox_日付逆転
            // 
            this.checkBox_日付逆転.AutoSize = true;
            this.checkBox_日付逆転.Checked = true;
            this.checkBox_日付逆転.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_日付逆転.Location = new System.Drawing.Point(349, 23);
            this.checkBox_日付逆転.Name = "checkBox_日付逆転";
            this.checkBox_日付逆転.Size = new System.Drawing.Size(137, 16);
            this.checkBox_日付逆転.TabIndex = 17;
            this.checkBox_日付逆転.Text = "日付逆転しているデータ";
            this.checkBox_日付逆転.UseVisualStyleBackColor = true;
            // 
            // 日付調整一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(765, 174);
            this.Controls.Add(this.treeListView日付調整一覧);
            this.Controls.Add(this.groupBox1);
            this.Name = "日付調整一覧Form";
            this.Text = "日付調整";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.日付調整一覧Form_FormClosing);
            this.Load += new System.EventHandler(this.日付調整一覧Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView日付調整一覧)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox詳細種別;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox種別;
        private System.Windows.Forms.Button button条件クリア;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox maskedTextBox発注日To;
        private System.Windows.Forms.MaskedTextBox maskedTextBox発注日From;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button検索;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView日付調整一覧;
        private System.Windows.Forms.CheckBox checkBox_日付逆転;
    }
}