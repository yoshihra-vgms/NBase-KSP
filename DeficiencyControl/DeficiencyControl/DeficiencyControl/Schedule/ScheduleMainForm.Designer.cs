namespace DeficiencyControl.Schedule
{
    partial class ScheduleMainForm
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
            this.tabControlSchedule = new System.Windows.Forms.TabControl();
            this.tabPageSchedule = new System.Windows.Forms.TabPage();
            this.scheduleMainScheduleControl1 = new DeficiencyControl.Schedule.ScheduleMainScheduleControl();
            this.tabPagePlan = new System.Windows.Forms.TabPage();
            this.scheduleMainPlanControl1 = new DeficiencyControl.Schedule.ScheduleMainPlanControl();
            this.tabPageCompany = new System.Windows.Forms.TabPage();
            this.scheduleMainCompanyControl1 = new DeficiencyControl.Schedule.ScheduleMainCompanyControl();
            this.tabPageOther = new System.Windows.Forms.TabPage();
            this.scheduleMainOtherControl1 = new DeficiencyControl.Schedule.ScheduleMainOtherControl();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonOutput = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabControlSchedule.SuspendLayout();
            this.tabPageSchedule.SuspendLayout();
            this.tabPagePlan.SuspendLayout();
            this.tabPageCompany.SuspendLayout();
            this.tabPageOther.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSchedule
            // 
            this.tabControlSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSchedule.Controls.Add(this.tabPageSchedule);
            this.tabControlSchedule.Controls.Add(this.tabPagePlan);
            this.tabControlSchedule.Controls.Add(this.tabPageCompany);
            this.tabControlSchedule.Controls.Add(this.tabPageOther);
            this.tabControlSchedule.Location = new System.Drawing.Point(12, 68);
            this.tabControlSchedule.Name = "tabControlSchedule";
            this.tabControlSchedule.SelectedIndex = 0;
            this.tabControlSchedule.Size = new System.Drawing.Size(1240, 546);
            this.tabControlSchedule.TabIndex = 3;
            // 
            // tabPageSchedule
            // 
            this.tabPageSchedule.AutoScroll = true;
            this.tabPageSchedule.Controls.Add(this.scheduleMainScheduleControl1);
            this.tabPageSchedule.Location = new System.Drawing.Point(4, 26);
            this.tabPageSchedule.Name = "tabPageSchedule";
            this.tabPageSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSchedule.Size = new System.Drawing.Size(1232, 516);
            this.tabPageSchedule.TabIndex = 0;
            this.tabPageSchedule.Tag = "1";
            this.tabPageSchedule.Text = "スケジュール";
            this.tabPageSchedule.UseVisualStyleBackColor = true;
            // 
            // scheduleMainScheduleControl1
            // 
            this.scheduleMainScheduleControl1.AutoScroll = true;
            this.scheduleMainScheduleControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scheduleMainScheduleControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.scheduleMainScheduleControl1.Location = new System.Drawing.Point(3, 3);
            this.scheduleMainScheduleControl1.Margin = new System.Windows.Forms.Padding(4);
            this.scheduleMainScheduleControl1.Name = "scheduleMainScheduleControl1";
            this.scheduleMainScheduleControl1.Size = new System.Drawing.Size(1226, 510);
            this.scheduleMainScheduleControl1.TabIndex = 0;
            // 
            // tabPagePlan
            // 
            this.tabPagePlan.Controls.Add(this.scheduleMainPlanControl1);
            this.tabPagePlan.Location = new System.Drawing.Point(4, 26);
            this.tabPagePlan.Name = "tabPagePlan";
            this.tabPagePlan.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlan.Size = new System.Drawing.Size(1232, 516);
            this.tabPagePlan.TabIndex = 1;
            this.tabPagePlan.Tag = "2";
            this.tabPagePlan.Text = "予定/実績";
            this.tabPagePlan.UseVisualStyleBackColor = true;
            // 
            // scheduleMainPlanControl1
            // 
            this.scheduleMainPlanControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scheduleMainPlanControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.scheduleMainPlanControl1.Location = new System.Drawing.Point(3, 3);
            this.scheduleMainPlanControl1.Margin = new System.Windows.Forms.Padding(4);
            this.scheduleMainPlanControl1.Name = "scheduleMainPlanControl1";
            this.scheduleMainPlanControl1.Size = new System.Drawing.Size(1226, 510);
            this.scheduleMainPlanControl1.TabIndex = 0;
            // 
            // tabPageCompany
            // 
            this.tabPageCompany.Controls.Add(this.scheduleMainCompanyControl1);
            this.tabPageCompany.Location = new System.Drawing.Point(4, 26);
            this.tabPageCompany.Name = "tabPageCompany";
            this.tabPageCompany.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCompany.Size = new System.Drawing.Size(1232, 516);
            this.tabPageCompany.TabIndex = 2;
            this.tabPageCompany.Tag = "3";
            this.tabPageCompany.Text = "会社";
            this.tabPageCompany.UseVisualStyleBackColor = true;
            // 
            // scheduleMainCompanyControl1
            // 
            this.scheduleMainCompanyControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scheduleMainCompanyControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.scheduleMainCompanyControl1.Location = new System.Drawing.Point(3, 3);
            this.scheduleMainCompanyControl1.Margin = new System.Windows.Forms.Padding(4);
            this.scheduleMainCompanyControl1.Name = "scheduleMainCompanyControl1";
            this.scheduleMainCompanyControl1.Size = new System.Drawing.Size(1226, 510);
            this.scheduleMainCompanyControl1.TabIndex = 0;
            // 
            // tabPageOther
            // 
            this.tabPageOther.Controls.Add(this.scheduleMainOtherControl1);
            this.tabPageOther.Location = new System.Drawing.Point(4, 26);
            this.tabPageOther.Name = "tabPageOther";
            this.tabPageOther.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOther.Size = new System.Drawing.Size(1232, 516);
            this.tabPageOther.TabIndex = 3;
            this.tabPageOther.Tag = "4";
            this.tabPageOther.Text = "その他";
            this.tabPageOther.UseVisualStyleBackColor = true;
            // 
            // scheduleMainOtherControl1
            // 
            this.scheduleMainOtherControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scheduleMainOtherControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.scheduleMainOtherControl1.Location = new System.Drawing.Point(3, 3);
            this.scheduleMainOtherControl1.Margin = new System.Windows.Forms.Padding(4);
            this.scheduleMainOtherControl1.Name = "scheduleMainOtherControl1";
            this.scheduleMainOtherControl1.Size = new System.Drawing.Size(1226, 510);
            this.scheduleMainOtherControl1.TabIndex = 0;
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(81, 26);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(334, 24);
            this.comboBoxYear.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label4.Location = new System.Drawing.Point(25, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 300;
            this.label4.Text = "年度";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(1122, 620);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(130, 50);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Close\r\n(閉じる)";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(421, 12);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(130, 50);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.Text = "Search\r\n(検索)";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonOutput
            // 
            this.buttonOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOutput.Location = new System.Drawing.Point(1122, 12);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Size = new System.Drawing.Size(130, 50);
            this.buttonOutput.TabIndex = 2;
            this.buttonOutput.Text = "Output\r\n(Excel)";
            this.buttonOutput.UseVisualStyleBackColor = true;
            this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
            // 
            // ScheduleMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.buttonOutput);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.comboBoxYear);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tabControlSchedule);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ScheduleMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "スケジュール";
            this.Load += new System.EventHandler(this.ScheduleMainForm_Load);
            this.tabControlSchedule.ResumeLayout(false);
            this.tabPageSchedule.ResumeLayout(false);
            this.tabPagePlan.ResumeLayout(false);
            this.tabPageCompany.ResumeLayout(false);
            this.tabPageOther.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSchedule;
        private System.Windows.Forms.TabPage tabPageSchedule;
        private System.Windows.Forms.TabPage tabPagePlan;
        private System.Windows.Forms.TabPage tabPageCompany;
        private System.Windows.Forms.TabPage tabPageOther;
        public System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSearch;
        public ScheduleMainPlanControl scheduleMainPlanControl1;
        public ScheduleMainScheduleControl scheduleMainScheduleControl1;
        public ScheduleMainCompanyControl scheduleMainCompanyControl1;
        public ScheduleMainOtherControl scheduleMainOtherControl1;
        private System.Windows.Forms.Button buttonOutput;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}