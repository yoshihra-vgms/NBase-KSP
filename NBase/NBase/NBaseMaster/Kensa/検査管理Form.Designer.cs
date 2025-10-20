namespace NBaseMaster.Kensa
{
    partial class 検査管理Form
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
			this.SearchGroup = new System.Windows.Forms.GroupBox();
			this.ClearButton = new System.Windows.Forms.Button();
			this.SearchButton = new System.Windows.Forms.Button();
			this.SearchKensaText = new System.Windows.Forms.TextBox();
			this.KensaNameLabel = new System.Windows.Forms.Label();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.AddButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.SearchGroup.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// SearchGroup
			// 
			this.SearchGroup.Controls.Add(this.ClearButton);
			this.SearchGroup.Controls.Add(this.SearchButton);
			this.SearchGroup.Controls.Add(this.SearchKensaText);
			this.SearchGroup.Controls.Add(this.KensaNameLabel);
			this.SearchGroup.Location = new System.Drawing.Point(12, 12);
			this.SearchGroup.Name = "SearchGroup";
			this.SearchGroup.Size = new System.Drawing.Size(308, 83);
			this.SearchGroup.TabIndex = 0;
			this.SearchGroup.TabStop = false;
			this.SearchGroup.Text = "検索";
			// 
			// ClearButton
			// 
			this.ClearButton.BackColor = System.Drawing.SystemColors.Control;
			this.ClearButton.Location = new System.Drawing.Point(180, 48);
			this.ClearButton.Name = "ClearButton";
			this.ClearButton.Size = new System.Drawing.Size(101, 23);
			this.ClearButton.TabIndex = 3;
			this.ClearButton.Text = "検索条件クリア";
			this.ClearButton.UseVisualStyleBackColor = false;
			this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
			// 
			// SearchButton
			// 
			this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
			this.SearchButton.Location = new System.Drawing.Point(180, 19);
			this.SearchButton.Name = "SearchButton";
			this.SearchButton.Size = new System.Drawing.Size(101, 23);
			this.SearchButton.TabIndex = 2;
			this.SearchButton.Text = "検索";
			this.SearchButton.UseVisualStyleBackColor = false;
			this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
			// 
			// SearchKensaText
			// 
			this.SearchKensaText.Location = new System.Drawing.Point(62, 21);
			this.SearchKensaText.MaxLength = 50;
			this.SearchKensaText.Name = "SearchKensaText";
			this.SearchKensaText.Size = new System.Drawing.Size(100, 19);
			this.SearchKensaText.TabIndex = 1;
			// 
			// KensaNameLabel
			// 
			this.KensaNameLabel.AutoSize = true;
			this.KensaNameLabel.Location = new System.Drawing.Point(15, 24);
			this.KensaNameLabel.Name = "KensaNameLabel";
			this.KensaNameLabel.Size = new System.Drawing.Size(41, 12);
			this.KensaNameLabel.TabIndex = 0;
			this.KensaNameLabel.Text = "検査名";
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(12, 101);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowTemplate.Height = 21;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(308, 150);
			this.dataGridView1.TabIndex = 1;
			this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
			// 
			// AddButton
			// 
			this.AddButton.BackColor = System.Drawing.SystemColors.Control;
			this.AddButton.Location = new System.Drawing.Point(74, 280);
			this.AddButton.Name = "AddButton";
			this.AddButton.Size = new System.Drawing.Size(75, 23);
			this.AddButton.TabIndex = 2;
			this.AddButton.Text = "新規追加";
			this.AddButton.UseVisualStyleBackColor = false;
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// CloseButton
			// 
			this.CloseButton.BackColor = System.Drawing.SystemColors.Control;
			this.CloseButton.Location = new System.Drawing.Point(192, 280);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(75, 23);
			this.CloseButton.TabIndex = 3;
			this.CloseButton.Text = "閉じる";
			this.CloseButton.UseVisualStyleBackColor = false;
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// 検査管理Form
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(332, 315);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.AddButton);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.SearchGroup);
			this.Name = "検査管理Form";
			this.Text = "検査管理Form";
			this.Load += new System.EventHandler(this.検査管理Form_Load);
			this.SearchGroup.ResumeLayout(false);
			this.SearchGroup.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SearchGroup;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox SearchKensaText;
        private System.Windows.Forms.Label KensaNameLabel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button CloseButton;
    }
}