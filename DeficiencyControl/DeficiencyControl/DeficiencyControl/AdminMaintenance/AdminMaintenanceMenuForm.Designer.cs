namespace DeficiencyControl.AdminMaintenance
{
    partial class AdminMaintenanceMenuForm
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
            this.buttonRegistCount = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonRegistCount
            // 
            this.buttonRegistCount.Location = new System.Drawing.Point(93, 12);
            this.buttonRegistCount.Name = "buttonRegistCount";
            this.buttonRegistCount.Size = new System.Drawing.Size(120, 40);
            this.buttonRegistCount.TabIndex = 1;
            this.buttonRegistCount.Text = "Regist Count";
            this.buttonRegistCount.UseVisualStyleBackColor = true;
            this.buttonRegistCount.Click += new System.EventHandler(this.buttonRegistCount_Click);
            // 
            // AdminMaintenanceMenuForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(316, 63);
            this.Controls.Add(this.buttonRegistCount);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AdminMaintenanceMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Maintenance Menu";
            this.Load += new System.EventHandler(this.AdminMaintenanceMenuForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRegistCount;

    }
}