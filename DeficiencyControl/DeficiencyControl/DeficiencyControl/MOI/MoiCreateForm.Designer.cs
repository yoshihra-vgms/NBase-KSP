namespace DeficiencyControl.MOI
{
    partial class MoiCreateForm
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
            this.splitContainerMoiControl = new System.Windows.Forms.SplitContainer();
            this.panelHeaderControl = new System.Windows.Forms.Panel();
            this.moiHeaderControl1 = new DeficiencyControl.MOI.MoiHeaderControl();
            this.flowLayoutPanelDetail = new System.Windows.Forms.FlowLayoutPanel();
            this.splitContainerMoiControl.Panel1.SuspendLayout();
            this.splitContainerMoiControl.Panel2.SuspendLayout();
            this.splitContainerMoiControl.SuspendLayout();
            this.panelHeaderControl.SuspendLayout();
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
            // splitContainerMoiControl
            // 
            this.splitContainerMoiControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMoiControl.Location = new System.Drawing.Point(12, 12);
            this.splitContainerMoiControl.Name = "splitContainerMoiControl";
            this.splitContainerMoiControl.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMoiControl.Panel1
            // 
            this.splitContainerMoiControl.Panel1.Controls.Add(this.panelHeaderControl);
            // 
            // splitContainerMoiControl.Panel2
            // 
            this.splitContainerMoiControl.Panel2.AutoScroll = true;
            this.splitContainerMoiControl.Panel2.Controls.Add(this.flowLayoutPanelDetail);
            this.splitContainerMoiControl.Size = new System.Drawing.Size(1240, 602);
            this.splitContainerMoiControl.SplitterDistance = 201;
            this.splitContainerMoiControl.TabIndex = 0;
            // 
            // panelHeaderControl
            // 
            this.panelHeaderControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeaderControl.AutoScroll = true;
            this.panelHeaderControl.Controls.Add(this.moiHeaderControl1);
            this.panelHeaderControl.Location = new System.Drawing.Point(3, 4);
            this.panelHeaderControl.Name = "panelHeaderControl";
            this.panelHeaderControl.Size = new System.Drawing.Size(1234, 194);
            this.panelHeaderControl.TabIndex = 0;
            // 
            // moiHeaderControl1
            // 
            this.moiHeaderControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.moiHeaderControl1.Location = new System.Drawing.Point(4, 4);
            this.moiHeaderControl1.Margin = new System.Windows.Forms.Padding(4);
            this.moiHeaderControl1.Name = "moiHeaderControl1";
            this.moiHeaderControl1.Size = new System.Drawing.Size(1150, 508);
            this.moiHeaderControl1.TabIndex = 0;
            // 
            // flowLayoutPanelDetail
            // 
            this.flowLayoutPanelDetail.AutoScroll = true;
            this.flowLayoutPanelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelDetail.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelDetail.Name = "flowLayoutPanelDetail";
            this.flowLayoutPanelDetail.Size = new System.Drawing.Size(1240, 397);
            this.flowLayoutPanelDetail.TabIndex = 0;
            // 
            // MoiCreateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.splitContainerMoiControl);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MoiCreateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "検船 (Create/新規作成)";
            this.Load += new System.EventHandler(this.MoiCreateForm_Load);
            this.splitContainerMoiControl.Panel1.ResumeLayout(false);
            this.splitContainerMoiControl.Panel2.ResumeLayout(false);
            this.splitContainerMoiControl.ResumeLayout(false);
            this.panelHeaderControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.SplitContainer splitContainerMoiControl;
        public System.Windows.Forms.Panel panelHeaderControl;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDetail;
        public MoiHeaderControl moiHeaderControl1;
    }
}