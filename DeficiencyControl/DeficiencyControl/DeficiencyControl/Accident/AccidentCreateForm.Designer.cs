namespace DeficiencyControl.Accident
{
    partial class AccidentCreateForm
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
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.splitContainerAccident = new System.Windows.Forms.SplitContainer();
            this.panelHeaderControl = new System.Windows.Forms.Panel();
            this.accidentHeaderControl1 = new DeficiencyControl.Accident.AccidentHeaderControl();
            this.flowLayoutPanelDetail = new System.Windows.Forms.FlowLayoutPanel();
            this.accidentDetailControl1 = new DeficiencyControl.Accident.AccidentDetailControl();
            this.splitContainerAccident.Panel1.SuspendLayout();
            this.splitContainerAccident.Panel2.SuspendLayout();
            this.splitContainerAccident.SuspendLayout();
            this.panelHeaderControl.SuspendLayout();
            this.flowLayoutPanelDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(1122, 620);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(130, 50);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close\r\n(閉じる)";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUpdate.Location = new System.Drawing.Point(565, 620);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(130, 50);
            this.buttonUpdate.TabIndex = 1;
            this.buttonUpdate.Text = "Update\r\n(登録)";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // splitContainerAccident
            // 
            this.splitContainerAccident.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerAccident.Location = new System.Drawing.Point(12, 12);
            this.splitContainerAccident.Name = "splitContainerAccident";
            this.splitContainerAccident.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerAccident.Panel1
            // 
            this.splitContainerAccident.Panel1.Controls.Add(this.panelHeaderControl);
            // 
            // splitContainerAccident.Panel2
            // 
            this.splitContainerAccident.Panel2.AutoScroll = true;
            this.splitContainerAccident.Panel2.Controls.Add(this.flowLayoutPanelDetail);
            this.splitContainerAccident.Size = new System.Drawing.Size(1240, 602);
            this.splitContainerAccident.SplitterDistance = 241;
            this.splitContainerAccident.TabIndex = 0;
            // 
            // panelHeaderControl
            // 
            this.panelHeaderControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeaderControl.AutoScroll = true;
            this.panelHeaderControl.Controls.Add(this.accidentHeaderControl1);
            this.panelHeaderControl.Location = new System.Drawing.Point(3, 4);
            this.panelHeaderControl.Name = "panelHeaderControl";
            this.panelHeaderControl.Size = new System.Drawing.Size(1234, 234);
            this.panelHeaderControl.TabIndex = 0;
            // 
            // accidentHeaderControl1
            // 
            this.accidentHeaderControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.accidentHeaderControl1.Location = new System.Drawing.Point(4, 4);
            this.accidentHeaderControl1.Margin = new System.Windows.Forms.Padding(4);
            this.accidentHeaderControl1.Name = "accidentHeaderControl1";
            this.accidentHeaderControl1.Size = new System.Drawing.Size(1150, 229);
            this.accidentHeaderControl1.TabIndex = 0;
            // 
            // flowLayoutPanelDetail
            // 
            this.flowLayoutPanelDetail.AutoScroll = true;
            this.flowLayoutPanelDetail.Controls.Add(this.accidentDetailControl1);
            this.flowLayoutPanelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelDetail.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelDetail.Name = "flowLayoutPanelDetail";
            this.flowLayoutPanelDetail.Size = new System.Drawing.Size(1240, 357);
            this.flowLayoutPanelDetail.TabIndex = 0;
            // 
            // accidentDetailControl1
            // 
            this.accidentDetailControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.accidentDetailControl1.Location = new System.Drawing.Point(4, 4);
            this.accidentDetailControl1.Margin = new System.Windows.Forms.Padding(4);
            this.accidentDetailControl1.Name = "accidentDetailControl1";
            this.accidentDetailControl1.Size = new System.Drawing.Size(1150, 1245);
            this.accidentDetailControl1.TabIndex = 0;
            // 
            // AccidentCreateForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.splitContainerAccident);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AccidentCreateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "事故・トラブル (Create / 新規作成)";
            this.Load += new System.EventHandler(this.AccidentCreateForm_Load);
            this.splitContainerAccident.Panel1.ResumeLayout(false);
            this.splitContainerAccident.Panel2.ResumeLayout(false);
            this.splitContainerAccident.ResumeLayout(false);
            this.panelHeaderControl.ResumeLayout(false);
            this.flowLayoutPanelDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.SplitContainer splitContainerAccident;
        public System.Windows.Forms.Panel panelHeaderControl;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDetail;
        public AccidentHeaderControl accidentHeaderControl1;
        public AccidentDetailControl accidentDetailControl1;
    }
}