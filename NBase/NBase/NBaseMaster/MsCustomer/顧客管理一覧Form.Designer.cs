namespace NBaseMaster.MsCustomer
{
    partial class 顧客管理一覧Form
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Inspection_checkBox = new System.Windows.Forms.CheckBox();
            this.Appointed_checkBox = new System.Windows.Forms.CheckBox();
            this.button出力 = new System.Windows.Forms.Button();
            this.School_checkBox = new System.Windows.Forms.CheckBox();
            this.Company_checkBox = new System.Windows.Forms.CheckBox();
            this.Consignor_checkBox = new System.Windows.Forms.CheckBox();
            this.Clear_button = new System.Windows.Forms.Button();
            this.Agency_checkBox = new System.Windows.Forms.CheckBox();
            this.Client_checkBox = new System.Windows.Forms.CheckBox();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.CustomerID_textBox = new System.Windows.Forms.TextBox();
            this.CustomerName_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Cancel_Btn = new System.Windows.Forms.Button();
            this.Add_Btn = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(881, 529);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Inspection_checkBox);
            this.groupBox1.Controls.Add(this.Appointed_checkBox);
            this.groupBox1.Controls.Add(this.button出力);
            this.groupBox1.Controls.Add(this.School_checkBox);
            this.groupBox1.Controls.Add(this.Company_checkBox);
            this.groupBox1.Controls.Add(this.Consignor_checkBox);
            this.groupBox1.Controls.Add(this.Clear_button);
            this.groupBox1.Controls.Add(this.Agency_checkBox);
            this.groupBox1.Controls.Add(this.Client_checkBox);
            this.groupBox1.Controls.Add(this.SearchBtn);
            this.groupBox1.Controls.Add(this.CustomerID_textBox);
            this.groupBox1.Controls.Add(this.CustomerName_textBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(875, 134);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索";
            // 
            // Inspection_checkBox
            // 
            this.Inspection_checkBox.AutoSize = true;
            this.Inspection_checkBox.Checked = true;
            this.Inspection_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Inspection_checkBox.Location = new System.Drawing.Point(471, 101);
            this.Inspection_checkBox.Name = "Inspection_checkBox";
            this.Inspection_checkBox.Size = new System.Drawing.Size(96, 16);
            this.Inspection_checkBox.TabIndex = 32;
            this.Inspection_checkBox.Text = "検船実施会社";
            this.Inspection_checkBox.UseVisualStyleBackColor = true;
            // 
            // Appointed_checkBox
            // 
            this.Appointed_checkBox.AutoSize = true;
            this.Appointed_checkBox.Checked = true;
            this.Appointed_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Appointed_checkBox.Location = new System.Drawing.Point(401, 101);
            this.Appointed_checkBox.Name = "Appointed_checkBox";
            this.Appointed_checkBox.Size = new System.Drawing.Size(60, 16);
            this.Appointed_checkBox.TabIndex = 32;
            this.Appointed_checkBox.Text = "申請先";
            this.Appointed_checkBox.UseVisualStyleBackColor = true;
            // 
            // button出力
            // 
            this.button出力.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button出力.BackColor = System.Drawing.Color.White;
            this.button出力.Location = new System.Drawing.Point(763, 101);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(92, 23);
            this.button出力.TabIndex = 31;
            this.button出力.Text = "Excel";
            this.button出力.UseVisualStyleBackColor = false;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // School_checkBox
            // 
            this.School_checkBox.AutoSize = true;
            this.School_checkBox.Checked = true;
            this.School_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.School_checkBox.Location = new System.Drawing.Point(343, 101);
            this.School_checkBox.Name = "School_checkBox";
            this.School_checkBox.Size = new System.Drawing.Size(48, 16);
            this.School_checkBox.TabIndex = 30;
            this.School_checkBox.Text = "学校";
            this.School_checkBox.UseVisualStyleBackColor = true;
            // 
            // Company_checkBox
            // 
            this.Company_checkBox.AutoSize = true;
            this.Company_checkBox.Checked = true;
            this.Company_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Company_checkBox.Location = new System.Drawing.Point(285, 101);
            this.Company_checkBox.Name = "Company_checkBox";
            this.Company_checkBox.Size = new System.Drawing.Size(48, 16);
            this.Company_checkBox.TabIndex = 30;
            this.Company_checkBox.Text = "企業";
            this.Company_checkBox.UseVisualStyleBackColor = true;
            // 
            // Consignor_checkBox
            // 
            this.Consignor_checkBox.AutoSize = true;
            this.Consignor_checkBox.Checked = true;
            this.Consignor_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Consignor_checkBox.Location = new System.Drawing.Point(227, 101);
            this.Consignor_checkBox.Name = "Consignor_checkBox";
            this.Consignor_checkBox.Size = new System.Drawing.Size(48, 16);
            this.Consignor_checkBox.TabIndex = 30;
            this.Consignor_checkBox.Text = "荷主";
            this.Consignor_checkBox.UseVisualStyleBackColor = true;
            // 
            // Clear_button
            // 
            this.Clear_button.BackColor = System.Drawing.Color.White;
            this.Clear_button.Location = new System.Drawing.Point(339, 47);
            this.Clear_button.Name = "Clear_button";
            this.Clear_button.Size = new System.Drawing.Size(96, 23);
            this.Clear_button.TabIndex = 29;
            this.Clear_button.Text = "検索条件クリア";
            this.Clear_button.UseVisualStyleBackColor = false;
            this.Clear_button.Click += new System.EventHandler(this.Clear_button_Click);
            // 
            // Agency_checkBox
            // 
            this.Agency_checkBox.AutoSize = true;
            this.Agency_checkBox.Checked = true;
            this.Agency_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Agency_checkBox.Location = new System.Drawing.Point(157, 101);
            this.Agency_checkBox.Name = "Agency_checkBox";
            this.Agency_checkBox.Size = new System.Drawing.Size(60, 16);
            this.Agency_checkBox.TabIndex = 9;
            this.Agency_checkBox.Text = "代理店";
            this.Agency_checkBox.UseVisualStyleBackColor = true;
            // 
            // Client_checkBox
            // 
            this.Client_checkBox.AutoSize = true;
            this.Client_checkBox.Checked = true;
            this.Client_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Client_checkBox.Location = new System.Drawing.Point(87, 101);
            this.Client_checkBox.Name = "Client_checkBox";
            this.Client_checkBox.Size = new System.Drawing.Size(60, 16);
            this.Client_checkBox.TabIndex = 8;
            this.Client_checkBox.Text = "取引先";
            this.Client_checkBox.UseVisualStyleBackColor = true;
            // 
            // SearchBtn
            // 
            this.SearchBtn.BackColor = System.Drawing.Color.White;
            this.SearchBtn.Location = new System.Drawing.Point(339, 16);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(96, 23);
            this.SearchBtn.TabIndex = 7;
            this.SearchBtn.Text = "検索";
            this.SearchBtn.UseVisualStyleBackColor = false;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // CustomerID_textBox
            // 
            this.CustomerID_textBox.Location = new System.Drawing.Point(87, 18);
            this.CustomerID_textBox.Name = "CustomerID_textBox";
            this.CustomerID_textBox.Size = new System.Drawing.Size(209, 19);
            this.CustomerID_textBox.TabIndex = 4;
            // 
            // CustomerName_textBox
            // 
            this.CustomerName_textBox.Location = new System.Drawing.Point(87, 49);
            this.CustomerName_textBox.Name = "CustomerName_textBox";
            this.CustomerName_textBox.Size = new System.Drawing.Size(209, 19);
            this.CustomerName_textBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "種別";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "顧客No";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "顧客名";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 143);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(875, 334);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Cancel_Btn);
            this.panel1.Controls.Add(this.Add_Btn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 483);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(875, 43);
            this.panel1.TabIndex = 2;
            // 
            // Cancel_Btn
            // 
            this.Cancel_Btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Cancel_Btn.BackColor = System.Drawing.Color.White;
            this.Cancel_Btn.Location = new System.Drawing.Point(450, 15);
            this.Cancel_Btn.Name = "Cancel_Btn";
            this.Cancel_Btn.Size = new System.Drawing.Size(75, 23);
            this.Cancel_Btn.TabIndex = 1;
            this.Cancel_Btn.Text = "閉じる";
            this.Cancel_Btn.UseVisualStyleBackColor = false;
            this.Cancel_Btn.Click += new System.EventHandler(this.Cancel_Btn_Click);
            // 
            // Add_Btn
            // 
            this.Add_Btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Add_Btn.BackColor = System.Drawing.Color.White;
            this.Add_Btn.Location = new System.Drawing.Point(350, 15);
            this.Add_Btn.Name = "Add_Btn";
            this.Add_Btn.Size = new System.Drawing.Size(75, 23);
            this.Add_Btn.TabIndex = 0;
            this.Add_Btn.Text = "新規追加";
            this.Add_Btn.UseVisualStyleBackColor = false;
            this.Add_Btn.Click += new System.EventHandler(this.Add_Btn_Click);
            // 
            // 顧客管理一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(881, 529);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "顧客管理一覧Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "顧客管理一覧Form";
            this.Load += new System.EventHandler(this.顧客管理一覧Form_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.TextBox CustomerID_textBox;
        private System.Windows.Forms.TextBox CustomerName_textBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Cancel_Btn;
        private System.Windows.Forms.Button Add_Btn;
        private System.Windows.Forms.CheckBox Agency_checkBox;
        private System.Windows.Forms.CheckBox Client_checkBox;
        private System.Windows.Forms.Button Clear_button;
        private System.Windows.Forms.CheckBox Consignor_checkBox;
        private System.Windows.Forms.CheckBox School_checkBox;
        private System.Windows.Forms.CheckBox Company_checkBox;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.CheckBox Inspection_checkBox;
        private System.Windows.Forms.CheckBox Appointed_checkBox;
    }
}