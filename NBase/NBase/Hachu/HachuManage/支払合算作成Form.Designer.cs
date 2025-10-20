namespace Hachu.HachuManage
{
    partial class 支払合算作成Form
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
            this.textBox合計金額 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button合算 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker受領日To = new NBaseUtil.NullableDateTimePicker();
            this.label受領日 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker受領日From = new NBaseUtil.NullableDateTimePicker();
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
            this.treeListView支払合算作成 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView支払合算作成)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeListView支払合算作成, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(923, 463);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox合計金額);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox備考);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button合算);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(917, 234);
            this.panel1.TabIndex = 2;
            // 
            // textBox合計金額
            // 
            this.textBox合計金額.Location = new System.Drawing.Point(784, 203);
            this.textBox合計金額.Name = "textBox合計金額";
            this.textBox合計金額.ReadOnly = true;
            this.textBox合計金額.Size = new System.Drawing.Size(115, 19);
            this.textBox合計金額.TabIndex = 0;
            this.textBox合計金額.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(696, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "合計金額（税抜）";
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox備考.Location = new System.Drawing.Point(106, 155);
            this.textBox備考.MaxLength = 500;
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox備考.Size = new System.Drawing.Size(397, 69);
            this.textBox備考.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "備考";
            // 
            // button合算
            // 
            this.button合算.BackColor = System.Drawing.SystemColors.Control;
            this.button合算.Location = new System.Drawing.Point(808, 158);
            this.button合算.Name = "button合算";
            this.button合算.Size = new System.Drawing.Size(75, 23);
            this.button合算.TabIndex = 2;
            this.button合算.Text = "合算";
            this.button合算.UseVisualStyleBackColor = false;
            this.button合算.Click += new System.EventHandler(this.button合算_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.nullableDateTimePicker受領日To);
            this.groupBox1.Controls.Add(this.label受領日);
            this.groupBox1.Controls.Add(this.nullableDateTimePicker受領日From);
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
            this.groupBox1.Size = new System.Drawing.Size(905, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(40, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 54;
            this.label7.Text = "※";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(41, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 53;
            this.label6.Text = "※";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(17, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 52;
            this.label4.Text = "※";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(232, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 45;
            this.label5.Text = "～";
            // 
            // nullableDateTimePicker受領日To
            // 
            this.nullableDateTimePicker受領日To.Location = new System.Drawing.Point(255, 108);
            this.nullableDateTimePicker受領日To.Name = "nullableDateTimePicker受領日To";
            this.nullableDateTimePicker受領日To.Size = new System.Drawing.Size(120, 19);
            this.nullableDateTimePicker受領日To.TabIndex = 6;
            this.nullableDateTimePicker受領日To.Value = null;
            // 
            // label受領日
            // 
            this.label受領日.AutoSize = true;
            this.label受領日.Location = new System.Drawing.Point(49, 111);
            this.label受領日.Name = "label受領日";
            this.label受領日.Size = new System.Drawing.Size(41, 12);
            this.label受領日.TabIndex = 43;
            this.label受領日.Text = "受領日";
            // 
            // nullableDateTimePicker受領日From
            // 
            this.nullableDateTimePicker受領日From.Location = new System.Drawing.Point(103, 108);
            this.nullableDateTimePicker受領日From.Name = "nullableDateTimePicker受領日From";
            this.nullableDateTimePicker受領日From.Size = new System.Drawing.Size(120, 19);
            this.nullableDateTimePicker受領日From.TabIndex = 5;
            this.nullableDateTimePicker受領日From.Value = null;
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
            this.comboBox取引先.TabIndex = 1;
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
            this.label9.Location = new System.Drawing.Point(319, 58);
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
            this.comboBox詳細種別.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(61, 58);
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
            this.comboBox種別.Location = new System.Drawing.Point(103, 54);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(170, 20);
            this.comboBox種別.TabIndex = 2;
            this.comboBox種別.SelectedIndexChanged += new System.EventHandler(this.comboBox種別_SelectedIndexChanged);
            // 
            // button条件クリア
            // 
            this.button条件クリア.BackColor = System.Drawing.SystemColors.Control;
            this.button条件クリア.Location = new System.Drawing.Point(805, 48);
            this.button条件クリア.Name = "button条件クリア";
            this.button条件クリア.Size = new System.Drawing.Size(75, 23);
            this.button条件クリア.TabIndex = 8;
            this.button条件クリア.Text = "条件クリア";
            this.button条件クリア.UseVisualStyleBackColor = false;
            this.button条件クリア.Click += new System.EventHandler(this.button条件クリア_Click);
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(103, 81);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(170, 20);
            this.comboBox船.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "船名";
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(805, 19);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(75, 23);
            this.button検索.TabIndex = 7;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // treeListView支払合算作成
            // 
            // 
            // 
            // 
            this.treeListView支払合算作成.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView支払合算作成.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView支払合算作成.ContentPanel.Name = "";
            this.treeListView支払合算作成.ContentPanel.Size = new System.Drawing.Size(911, 211);
            this.treeListView支払合算作成.ContentPanel.TabIndex = 3;
            this.treeListView支払合算作成.ContentPanel.TabStop = false;
            this.treeListView支払合算作成.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView支払合算作成.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView支払合算作成.Footer = false;
            this.treeListView支払合算作成.Location = new System.Drawing.Point(3, 243);
            this.treeListView支払合算作成.Name = "treeListView支払合算作成";
            this.treeListView支払合算作成.Size = new System.Drawing.Size(917, 217);
            this.treeListView支払合算作成.TabIndex = 0;
            this.treeListView支払合算作成.Text = "treeListView1";
            this.treeListView支払合算作成.WatermarkImage = watermarkImage2;
            // 
            // 支払合算作成Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(923, 463);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "支払合算作成Form";
            this.Text = "支払合算作成Form";
            this.Load += new System.EventHandler(this.支払合算作成Form_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView支払合算作成)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button合算;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBox詳細種別;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox種別;
        private System.Windows.Forms.Button button条件クリア;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button検索;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView支払合算作成;
        private System.Windows.Forms.ComboBox comboBox取引先;
        private System.Windows.Forms.Label label発注先;
        private System.Windows.Forms.Label label5;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker受領日To;
        private System.Windows.Forms.Label label受領日;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker受領日From;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.TextBox textBox合計金額;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;

    }
}