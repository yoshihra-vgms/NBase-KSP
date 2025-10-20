namespace NBaseMaster
{
    partial class ユーザ検索Form
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.Choose_Btn = new System.Windows.Forms.Button();
            this.Cancel_Btn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Clear_button = new System.Windows.Forms.Button();
            this.Search_Btn = new System.Windows.Forms.Button();
            this.sei_textBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Bumon_DropDownList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mei_textBox = new System.Windows.Forms.TextBox();
            this.Jimusho_checkBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Crew_checkBox = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(409, 457);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.Choose_Btn);
            this.panel3.Controls.Add(this.Cancel_Btn);
            this.panel3.Location = new System.Drawing.Point(3, 416);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(403, 38);
            this.panel3.TabIndex = 2;
            // 
            // Choose_Btn
            // 
            this.Choose_Btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Choose_Btn.BackColor = System.Drawing.Color.White;
            this.Choose_Btn.Location = new System.Drawing.Point(110, 8);
            this.Choose_Btn.Name = "Choose_Btn";
            this.Choose_Btn.Size = new System.Drawing.Size(81, 26);
            this.Choose_Btn.TabIndex = 14;
            this.Choose_Btn.Text = "選択";
            this.Choose_Btn.UseVisualStyleBackColor = false;
            this.Choose_Btn.Click += new System.EventHandler(this.Choose_Btn_Click);
            // 
            // Cancel_Btn
            // 
            this.Cancel_Btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Cancel_Btn.BackColor = System.Drawing.Color.White;
            this.Cancel_Btn.Location = new System.Drawing.Point(212, 8);
            this.Cancel_Btn.Name = "Cancel_Btn";
            this.Cancel_Btn.Size = new System.Drawing.Size(81, 26);
            this.Cancel_Btn.TabIndex = 13;
            this.Cancel_Btn.Text = "閉じる";
            this.Cancel_Btn.UseVisualStyleBackColor = false;
            this.Cancel_Btn.Click += new System.EventHandler(this.Cancel_Btn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 144);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(403, 266);
            this.dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Clear_button);
            this.panel1.Controls.Add(this.Search_Btn);
            this.panel1.Controls.Add(this.sei_textBox);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.Bumon_DropDownList);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.mei_textBox);
            this.panel1.Controls.Add(this.Jimusho_checkBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Crew_checkBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(403, 135);
            this.panel1.TabIndex = 0;
            // 
            // Clear_button
            // 
            this.Clear_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Clear_button.BackColor = System.Drawing.Color.White;
            this.Clear_button.Location = new System.Drawing.Point(287, 44);
            this.Clear_button.Name = "Clear_button";
            this.Clear_button.Size = new System.Drawing.Size(96, 23);
            this.Clear_button.TabIndex = 28;
            this.Clear_button.Text = "検索条件クリア";
            this.Clear_button.UseVisualStyleBackColor = false;
            this.Clear_button.Click += new System.EventHandler(this.Clear_button_Click);
            // 
            // Search_Btn
            // 
            this.Search_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Search_Btn.BackColor = System.Drawing.Color.White;
            this.Search_Btn.Location = new System.Drawing.Point(287, 15);
            this.Search_Btn.Name = "Search_Btn";
            this.Search_Btn.Size = new System.Drawing.Size(96, 23);
            this.Search_Btn.TabIndex = 16;
            this.Search_Btn.Text = "検索";
            this.Search_Btn.UseVisualStyleBackColor = false;
            this.Search_Btn.Click += new System.EventHandler(this.Search_Btn_Click);
            // 
            // sei_textBox
            // 
            this.sei_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.sei_textBox.Location = new System.Drawing.Point(81, 17);
            this.sei_textBox.Name = "sei_textBox";
            this.sei_textBox.Size = new System.Drawing.Size(161, 19);
            this.sei_textBox.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "部門";
            // 
            // Bumon_DropDownList
            // 
            this.Bumon_DropDownList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Bumon_DropDownList.FormattingEnabled = true;
            this.Bumon_DropDownList.Location = new System.Drawing.Point(81, 71);
            this.Bumon_DropDownList.Name = "Bumon_DropDownList";
            this.Bumon_DropDownList.Size = new System.Drawing.Size(161, 20);
            this.Bumon_DropDownList.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "氏名(姓)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "氏名(名)";
            // 
            // mei_textBox
            // 
            this.mei_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.mei_textBox.Location = new System.Drawing.Point(81, 44);
            this.mei_textBox.Name = "mei_textBox";
            this.mei_textBox.Size = new System.Drawing.Size(161, 19);
            this.mei_textBox.TabIndex = 27;
            // 
            // Jimusho_checkBox
            // 
            this.Jimusho_checkBox.AutoSize = true;
            this.Jimusho_checkBox.Checked = true;
            this.Jimusho_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Jimusho_checkBox.Location = new System.Drawing.Point(81, 104);
            this.Jimusho_checkBox.Name = "Jimusho_checkBox";
            this.Jimusho_checkBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Jimusho_checkBox.Size = new System.Drawing.Size(60, 16);
            this.Jimusho_checkBox.TabIndex = 19;
            this.Jimusho_checkBox.Text = "事務所";
            this.Jimusho_checkBox.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "区分";
            // 
            // Crew_checkBox
            // 
            this.Crew_checkBox.AutoSize = true;
            this.Crew_checkBox.Location = new System.Drawing.Point(156, 104);
            this.Crew_checkBox.Name = "Crew_checkBox";
            this.Crew_checkBox.Size = new System.Drawing.Size(48, 16);
            this.Crew_checkBox.TabIndex = 20;
            this.Crew_checkBox.Text = "船員";
            this.Crew_checkBox.UseVisualStyleBackColor = true;
            // 
            // ユーザ検索Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(415, 460);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ユーザ検索Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ユーザー検索";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button Choose_Btn;
        private System.Windows.Forms.Button Cancel_Btn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox Bumon_DropDownList;
        private System.Windows.Forms.CheckBox Crew_checkBox;
        private System.Windows.Forms.CheckBox Jimusho_checkBox;
        private System.Windows.Forms.Button Search_Btn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mei_textBox;
        private System.Windows.Forms.TextBox sei_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Clear_button;


    }
}