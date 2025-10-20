namespace DeficiencyControl.Forms
{
    partial class CommentItemCreateForm
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
            this.splitContainerItemControl = new System.Windows.Forms.SplitContainer();
            this.panelHeaderControl = new System.Windows.Forms.Panel();
            this.flowLayoutPanelDetail = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.splitContainerItemControl.Panel1.SuspendLayout();
            this.splitContainerItemControl.Panel2.SuspendLayout();
            this.splitContainerItemControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerItemControl
            // 
            this.splitContainerItemControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerItemControl.Location = new System.Drawing.Point(12, 12);
            this.splitContainerItemControl.Name = "splitContainerItemControl";
            this.splitContainerItemControl.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerItemControl.Panel1
            // 
            this.splitContainerItemControl.Panel1.Controls.Add(this.panelHeaderControl);
            // 
            // splitContainerItemControl.Panel2
            // 
            this.splitContainerItemControl.Panel2.AutoScroll = true;
            this.splitContainerItemControl.Panel2.Controls.Add(this.flowLayoutPanelDetail);
            this.splitContainerItemControl.Size = new System.Drawing.Size(1240, 602);
            this.splitContainerItemControl.SplitterDistance = 201;
            this.splitContainerItemControl.TabIndex = 0;
            // 
            // panelHeaderControl
            // 
            this.panelHeaderControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeaderControl.AutoScroll = true;
            this.panelHeaderControl.Location = new System.Drawing.Point(3, 4);
            this.panelHeaderControl.Name = "panelHeaderControl";
            this.panelHeaderControl.Size = new System.Drawing.Size(1234, 194);
            this.panelHeaderControl.TabIndex = 0;
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
            // CommentItemCreateForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.splitContainerItemControl);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CommentItemCreateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PSC Create (新規作成)";
            this.Load += new System.EventHandler(this.CommentItemCreateForm_Load);
            this.splitContainerItemControl.Panel1.ResumeLayout(false);
            this.splitContainerItemControl.Panel2.ResumeLayout(false);
            this.splitContainerItemControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonUpdate;
        public System.Windows.Forms.Panel panelHeaderControl;
        private System.Windows.Forms.SplitContainer splitContainerItemControl;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDetail;
    }
}