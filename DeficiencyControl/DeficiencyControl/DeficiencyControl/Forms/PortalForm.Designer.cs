namespace DeficiencyControl.Forms
{
    partial class PortalForm
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
            this.buttonAccidentReport = new System.Windows.Forms.Button();
            this.buttonMaster = new System.Windows.Forms.Button();
            this.tabControlPortal = new System.Windows.Forms.TabControl();
            this.tabPageAlarm = new System.Windows.Forms.TabPage();
            this.portalAlarmControl1 = new DeficiencyControl.Controls.PortalAlarmControl();
            this.buttonPSC = new System.Windows.Forms.Button();
            this.buttonMOI = new System.Windows.Forms.Button();
            this.buttonSchedule = new System.Windows.Forms.Button();
            this.tabControlPortal.SuspendLayout();
            this.tabPageAlarm.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAccidentReport
            // 
            this.buttonAccidentReport.Location = new System.Drawing.Point(148, 12);
            this.buttonAccidentReport.Name = "buttonAccidentReport";
            this.buttonAccidentReport.Size = new System.Drawing.Size(130, 46);
            this.buttonAccidentReport.TabIndex = 1;
            this.buttonAccidentReport.Text = "事故・トラブル";
            this.buttonAccidentReport.UseVisualStyleBackColor = true;
            this.buttonAccidentReport.Click += new System.EventHandler(this.buttonAccidentReport_Click);
            // 
            // buttonMaster
            // 
            this.buttonMaster.Location = new System.Drawing.Point(1108, 12);
            this.buttonMaster.Name = "buttonMaster";
            this.buttonMaster.Size = new System.Drawing.Size(130, 46);
            this.buttonMaster.TabIndex = 4;
            this.buttonMaster.Text = "Master";
            this.buttonMaster.UseVisualStyleBackColor = true;
            this.buttonMaster.Click += new System.EventHandler(this.buttonMaster_Click);
            // 
            // tabControlPortal
            // 
            this.tabControlPortal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlPortal.Controls.Add(this.tabPageAlarm);
            this.tabControlPortal.Location = new System.Drawing.Point(12, 64);
            this.tabControlPortal.Name = "tabControlPortal";
            this.tabControlPortal.SelectedIndex = 0;
            this.tabControlPortal.Size = new System.Drawing.Size(1240, 606);
            this.tabControlPortal.TabIndex = 5;
            // 
            // tabPageAlarm
            // 
            this.tabPageAlarm.Controls.Add(this.portalAlarmControl1);
            this.tabPageAlarm.Location = new System.Drawing.Point(4, 26);
            this.tabPageAlarm.Name = "tabPageAlarm";
            this.tabPageAlarm.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAlarm.Size = new System.Drawing.Size(1232, 576);
            this.tabPageAlarm.TabIndex = 0;
            this.tabPageAlarm.Text = "Alarm";
            this.tabPageAlarm.UseVisualStyleBackColor = true;
            // 
            // portalAlarmControl1
            // 
            this.portalAlarmControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portalAlarmControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.portalAlarmControl1.Location = new System.Drawing.Point(0, 0);
            this.portalAlarmControl1.Name = "portalAlarmControl1";
            this.portalAlarmControl1.Size = new System.Drawing.Size(1225, 562);
            this.portalAlarmControl1.TabIndex = 0;
            // 
            // buttonPSC
            // 
            this.buttonPSC.Location = new System.Drawing.Point(12, 12);
            this.buttonPSC.Name = "buttonPSC";
            this.buttonPSC.Size = new System.Drawing.Size(130, 46);
            this.buttonPSC.TabIndex = 0;
            this.buttonPSC.Text = "PSC";
            this.buttonPSC.UseVisualStyleBackColor = true;
            this.buttonPSC.Click += new System.EventHandler(this.buttonPSC_Click);
            // 
            // buttonMOI
            // 
            this.buttonMOI.Location = new System.Drawing.Point(284, 12);
            this.buttonMOI.Name = "buttonMOI";
            this.buttonMOI.Size = new System.Drawing.Size(130, 46);
            this.buttonMOI.TabIndex = 2;
            this.buttonMOI.Text = "検船";
            this.buttonMOI.UseVisualStyleBackColor = true;
            this.buttonMOI.Click += new System.EventHandler(this.buttonMOI_Click);
            // 
            // buttonSchedule
            // 
            this.buttonSchedule.Location = new System.Drawing.Point(420, 12);
            this.buttonSchedule.Name = "buttonSchedule";
            this.buttonSchedule.Size = new System.Drawing.Size(130, 46);
            this.buttonSchedule.TabIndex = 3;
            this.buttonSchedule.Text = "スケジュール";
            this.buttonSchedule.UseVisualStyleBackColor = true;
            this.buttonSchedule.Click += new System.EventHandler(this.buttonSchedule_Click);
            // 
            // PortalForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.buttonSchedule);
            this.Controls.Add(this.buttonMOI);
            this.Controls.Add(this.buttonPSC);
            this.Controls.Add(this.tabControlPortal);
            this.Controls.Add(this.buttonMaster);
            this.Controls.Add(this.buttonAccidentReport);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PortalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Portal";
            this.Load += new System.EventHandler(this.PortalForm_Load);
            this.tabControlPortal.ResumeLayout(false);
            this.tabPageAlarm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAccidentReport;
        private System.Windows.Forms.Button buttonMaster;
        private System.Windows.Forms.TabControl tabControlPortal;
        private System.Windows.Forms.TabPage tabPageAlarm;
        private Controls.PortalAlarmControl portalAlarmControl1;
        private System.Windows.Forms.Button buttonPSC;
        private System.Windows.Forms.Button buttonMOI;
        private System.Windows.Forms.Button buttonSchedule;

    }
}