namespace DeficiencyControl.MasterMaintenance
{
    partial class MasterMenuForm
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonMsAlarmColor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(142, 58);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(120, 40);
            this.buttonClose.TabIndex = 50;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonMsAlarmColor
            // 
            this.buttonMsAlarmColor.Location = new System.Drawing.Point(12, 12);
            this.buttonMsAlarmColor.Name = "buttonMsAlarmColor";
            this.buttonMsAlarmColor.Size = new System.Drawing.Size(250, 40);
            this.buttonMsAlarmColor.TabIndex = 1;
            this.buttonMsAlarmColor.Text = "Alarm Color";
            this.buttonMsAlarmColor.UseVisualStyleBackColor = true;
            this.buttonMsAlarmColor.Click += new System.EventHandler(this.buttonMsAlarmColor_Click);
            // 
            // MasterMenuForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(274, 108);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonMsAlarmColor);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MasterMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Master Menu";
            this.Load += new System.EventHandler(this.MasterMenuForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MasterMenuForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonMsAlarmColor;
        private System.Windows.Forms.Button buttonClose;
    }
}