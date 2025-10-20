namespace Document
{
    partial class 管理記録一覧Form
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
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_BunshoName = new System.Windows.Forms.TextBox();
            this.comboBox_Shoubunrui = new System.Windows.Forms.ComboBox();
            this.comboBox_Bunrui = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Clear_button = new System.Windows.Forms.Button();
            this.Search_button = new System.Windows.Forms.Button();
            this.textBox_BunshoNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Add_button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.button_閉じる = new System.Windows.Forms.Button();
            this.管理記録登録Form1 = new Document.管理記録登録Form();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "文書名";
            // 
            // textBox_BunshoName
            // 
            this.textBox_BunshoName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_BunshoName.Location = new System.Drawing.Point(94, 83);
            this.textBox_BunshoName.MaxLength = 50;
            this.textBox_BunshoName.Name = "textBox_BunshoName";
            this.textBox_BunshoName.Size = new System.Drawing.Size(300, 19);
            this.textBox_BunshoName.TabIndex = 8;
            // 
            // comboBox_Shoubunrui
            // 
            this.comboBox_Shoubunrui.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Shoubunrui.FormattingEnabled = true;
            this.comboBox_Shoubunrui.Location = new System.Drawing.Point(94, 36);
            this.comboBox_Shoubunrui.Name = "comboBox_Shoubunrui";
            this.comboBox_Shoubunrui.Size = new System.Drawing.Size(300, 20);
            this.comboBox_Shoubunrui.TabIndex = 7;
            // 
            // comboBox_Bunrui
            // 
            this.comboBox_Bunrui.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Bunrui.FormattingEnabled = true;
            this.comboBox_Bunrui.Location = new System.Drawing.Point(94, 12);
            this.comboBox_Bunrui.Name = "comboBox_Bunrui";
            this.comboBox_Bunrui.Size = new System.Drawing.Size(300, 20);
            this.comboBox_Bunrui.TabIndex = 6;
            this.comboBox_Bunrui.SelectedIndexChanged += new System.EventHandler(this.comboBox_Bunrui_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "文書番号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "小分類名";
            // 
            // Clear_button
            // 
            this.Clear_button.Location = new System.Drawing.Point(494, 39);
            this.Clear_button.Name = "Clear_button";
            this.Clear_button.Size = new System.Drawing.Size(96, 23);
            this.Clear_button.TabIndex = 3;
            this.Clear_button.Text = "検索条件クリア";
            this.Clear_button.UseVisualStyleBackColor = true;
            this.Clear_button.Click += new System.EventHandler(this.Clear_button_Click);
            // 
            // Search_button
            // 
            this.Search_button.Location = new System.Drawing.Point(494, 12);
            this.Search_button.Name = "Search_button";
            this.Search_button.Size = new System.Drawing.Size(96, 23);
            this.Search_button.TabIndex = 2;
            this.Search_button.Text = "検索";
            this.Search_button.UseVisualStyleBackColor = true;
            this.Search_button.Click += new System.EventHandler(this.Search_button_Click);
            // 
            // textBox_BunshoNo
            // 
            this.textBox_BunshoNo.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox_BunshoNo.Location = new System.Drawing.Point(94, 60);
            this.textBox_BunshoNo.MaxLength = 15;
            this.textBox_BunshoNo.Name = "textBox_BunshoNo";
            this.textBox_BunshoNo.Size = new System.Drawing.Size(179, 19);
            this.textBox_BunshoNo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "分類名";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 147);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(627, 463);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Add_button
            // 
            this.Add_button.Location = new System.Drawing.Point(12, 118);
            this.Add_button.Name = "Add_button";
            this.Add_button.Size = new System.Drawing.Size(75, 23);
            this.Add_button.TabIndex = 2;
            this.Add_button.Text = "報告書作成";
            this.Add_button.UseVisualStyleBackColor = true;
            this.Add_button.Visible = false;
            this.Add_button.Click += new System.EventHandler(this.Add_button_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "※";
            // 
            // button_閉じる
            // 
            this.button_閉じる.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_閉じる.Location = new System.Drawing.Point(1081, 12);
            this.button_閉じる.Name = "button_閉じる";
            this.button_閉じる.Size = new System.Drawing.Size(96, 23);
            this.button_閉じる.TabIndex = 12;
            this.button_閉じる.Text = "閉じる";
            this.button_閉じる.UseVisualStyleBackColor = true;
            this.button_閉じる.Click += new System.EventHandler(this.button_閉じる_Click);
            // 
            // 管理記録登録Form1
            // 
            this.管理記録登録Form1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.管理記録登録Form1.BackColor = System.Drawing.Color.White;
            this.管理記録登録Form1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.管理記録登録Form1.Location = new System.Drawing.Point(645, 147);
            this.管理記録登録Form1.Name = "管理記録登録Form1";
            this.管理記録登録Form1.Size = new System.Drawing.Size(536, 465);
            this.管理記録登録Form1.TabIndex = 11;
            // 
            // 管理記録一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1189, 622);
            this.Controls.Add(this.button_閉じる);
            this.Controls.Add(this.管理記録登録Form1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Clear_button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Search_button);
            this.Controls.Add(this.textBox_BunshoName);
            this.Controls.Add(this.Add_button);
            this.Controls.Add(this.comboBox_Shoubunrui);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboBox_Bunrui);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_BunshoNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "管理記録一覧Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "管理記録一覧";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.管理記録一覧Form_FormClosed);
            this.Load += new System.EventHandler(this.管理記録一覧Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Clear_button;
        private System.Windows.Forms.Button Search_button;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Add_button;
        private System.Windows.Forms.TextBox textBox_BunshoNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Bunrui;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_BunshoName;
        private System.Windows.Forms.ComboBox comboBox_Shoubunrui;
        private System.Windows.Forms.Label label5;
        private 管理記録登録Form 管理記録登録Form1;
        private System.Windows.Forms.Button button_閉じる;
    }
}