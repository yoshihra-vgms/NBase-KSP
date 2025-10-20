namespace DeficiencyControl.AdminMaintenance
{
    partial class RegistCountListForm
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
            this.dataGridViewRegistCount = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonRegistCount = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRegistCount)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewRegistCount
            // 
            this.dataGridViewRegistCount.AllowUserToAddRows = false;
            this.dataGridViewRegistCount.AllowUserToDeleteRows = false;
            this.dataGridViewRegistCount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewRegistCount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRegistCount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dataGridViewRegistCount.EnableHeadersVisualStyles = false;
            this.dataGridViewRegistCount.Location = new System.Drawing.Point(12, 50);
            this.dataGridViewRegistCount.Name = "dataGridViewRegistCount";
            this.dataGridViewRegistCount.ReadOnly = true;
            this.dataGridViewRegistCount.RowHeadersVisible = false;
            this.dataGridViewRegistCount.RowTemplate.Height = 21;
            this.dataGridViewRegistCount.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewRegistCount.Size = new System.Drawing.Size(676, 354);
            this.dataGridViewRegistCount.TabIndex = 11;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Data";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "No";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Title";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 250;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Kind";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 250;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "RegistCount";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // buttonRegistCount
            // 
            this.buttonRegistCount.Location = new System.Drawing.Point(12, 12);
            this.buttonRegistCount.Name = "buttonRegistCount";
            this.buttonRegistCount.Size = new System.Drawing.Size(116, 32);
            this.buttonRegistCount.TabIndex = 12;
            this.buttonRegistCount.Text = "Regist Count";
            this.buttonRegistCount.UseVisualStyleBackColor = true;
            this.buttonRegistCount.Click += new System.EventHandler(this.buttonRegistCount_Click);
            // 
            // RegistCountListForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(700, 416);
            this.Controls.Add(this.buttonRegistCount);
            this.Controls.Add(this.dataGridViewRegistCount);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RegistCountListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Regist Count List";
            this.Load += new System.EventHandler(this.RegistCountListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRegistCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewRegistCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button buttonRegistCount;
    }
}