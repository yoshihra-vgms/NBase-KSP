namespace NBaseMaster.船員管理
{
    partial class 基本給管理詳細Form1
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
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.標令 = new System.Windows.Forms.TabPage();
            this.button追加_標令 = new System.Windows.Forms.Button();
            this.dataGridView標令 = new System.Windows.Forms.DataGridView();
            this.職務 = new System.Windows.Forms.TabPage();
            this.button追加_職務 = new System.Windows.Forms.Button();
            this.dataGridView職務 = new System.Windows.Forms.DataGridView();
            this.label区分 = new System.Windows.Forms.Label();
            this.button更新 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker_to = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker_from = new NBaseUtil.NullableDateTimePicker();
            this.tabControl1.SuspendLayout();
            this.標令.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView標令)).BeginInit();
            this.職務.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView職務)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "区分 ：";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.標令);
            this.tabControl1.Controls.Add(this.職務);
            this.tabControl1.Location = new System.Drawing.Point(12, 74);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(344, 334);
            this.tabControl1.TabIndex = 14;
            // 
            // 標令
            // 
            this.標令.Controls.Add(this.button追加_標令);
            this.標令.Controls.Add(this.dataGridView標令);
            this.標令.Location = new System.Drawing.Point(4, 22);
            this.標令.Name = "標令";
            this.標令.Padding = new System.Windows.Forms.Padding(3);
            this.標令.Size = new System.Drawing.Size(336, 308);
            this.標令.TabIndex = 1;
            this.標令.Text = "標令";
            this.標令.UseVisualStyleBackColor = true;
            // 
            // button追加_標令
            // 
            this.button追加_標令.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button追加_標令.BackColor = System.Drawing.SystemColors.Control;
            this.button追加_標令.Location = new System.Drawing.Point(255, 8);
            this.button追加_標令.Name = "button追加_標令";
            this.button追加_標令.Size = new System.Drawing.Size(75, 23);
            this.button追加_標令.TabIndex = 24;
            this.button追加_標令.Text = "追加";
            this.button追加_標令.UseVisualStyleBackColor = false;
            this.button追加_標令.Click += new System.EventHandler(this.button追加_標令_Click);
            // 
            // dataGridView標令
            // 
            this.dataGridView標令.AllowUserToAddRows = false;
            this.dataGridView標令.AllowUserToDeleteRows = false;
            this.dataGridView標令.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView標令.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView標令.Location = new System.Drawing.Point(6, 37);
            this.dataGridView標令.MultiSelect = false;
            this.dataGridView標令.Name = "dataGridView標令";
            this.dataGridView標令.ReadOnly = true;
            this.dataGridView標令.RowTemplate.Height = 21;
            this.dataGridView標令.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView標令.Size = new System.Drawing.Size(324, 265);
            this.dataGridView標令.TabIndex = 23;
            this.dataGridView標令.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView標令_CellDoubleClick);
            // 
            // 職務
            // 
            this.職務.Controls.Add(this.button追加_職務);
            this.職務.Controls.Add(this.dataGridView職務);
            this.職務.Location = new System.Drawing.Point(4, 22);
            this.職務.Name = "職務";
            this.職務.Size = new System.Drawing.Size(336, 308);
            this.職務.TabIndex = 5;
            this.職務.Text = "職務";
            this.職務.UseVisualStyleBackColor = true;
            // 
            // button追加_職務
            // 
            this.button追加_職務.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button追加_職務.BackColor = System.Drawing.SystemColors.Control;
            this.button追加_職務.Location = new System.Drawing.Point(255, 8);
            this.button追加_職務.Name = "button追加_職務";
            this.button追加_職務.Size = new System.Drawing.Size(75, 23);
            this.button追加_職務.TabIndex = 24;
            this.button追加_職務.Text = "追加";
            this.button追加_職務.UseVisualStyleBackColor = false;
            this.button追加_職務.Click += new System.EventHandler(this.button追加_職務_Click);
            // 
            // dataGridView職務
            // 
            this.dataGridView職務.AllowUserToAddRows = false;
            this.dataGridView職務.AllowUserToDeleteRows = false;
            this.dataGridView職務.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView職務.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView職務.Location = new System.Drawing.Point(6, 37);
            this.dataGridView職務.MultiSelect = false;
            this.dataGridView職務.Name = "dataGridView職務";
            this.dataGridView職務.ReadOnly = true;
            this.dataGridView職務.RowTemplate.Height = 21;
            this.dataGridView職務.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView職務.Size = new System.Drawing.Size(324, 265);
            this.dataGridView職務.TabIndex = 23;
            this.dataGridView職務.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView職務_CellDoubleClick);
            // 
            // label区分
            // 
            this.label区分.AutoSize = true;
            this.label区分.Location = new System.Drawing.Point(75, 20);
            this.label区分.Name = "label区分";
            this.label区分.Size = new System.Drawing.Size(29, 12);
            this.label区分.TabIndex = 15;
            this.label区分.Text = "区分";
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(105, 422);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 22;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(186, 422);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 23;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "〜";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "期間 ：";
            // 
            // nullableDateTimePicker_to
            // 
            this.nullableDateTimePicker_to.Location = new System.Drawing.Point(219, 40);
            this.nullableDateTimePicker_to.Name = "nullableDateTimePicker_to";
            this.nullableDateTimePicker_to.Size = new System.Drawing.Size(119, 19);
            this.nullableDateTimePicker_to.TabIndex = 25;
            this.nullableDateTimePicker_to.Value = null;
            // 
            // nullableDateTimePicker_from
            // 
            this.nullableDateTimePicker_from.Location = new System.Drawing.Point(73, 40);
            this.nullableDateTimePicker_from.Name = "nullableDateTimePicker_from";
            this.nullableDateTimePicker_from.Size = new System.Drawing.Size(119, 19);
            this.nullableDateTimePicker_from.TabIndex = 24;
            this.nullableDateTimePicker_from.Value = null;
            // 
            // 基本給管理詳細Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(366, 457);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nullableDateTimePicker_to);
            this.Controls.Add(this.nullableDateTimePicker_from);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.label区分);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label3);
            this.Name = "基本給管理詳細Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基本給管理詳細";
            this.Load += new System.EventHandler(this.基本給管理詳細Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.標令.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView標令)).EndInit();
            this.職務.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView職務)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage 標令;
        private System.Windows.Forms.Button button追加_標令;
        private System.Windows.Forms.DataGridView dataGridView標令;
        private System.Windows.Forms.Label label区分;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker_to;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker_from;
        private System.Windows.Forms.TabPage 職務;
        private System.Windows.Forms.Button button追加_職務;
        private System.Windows.Forms.DataGridView dataGridView職務;
    }
}