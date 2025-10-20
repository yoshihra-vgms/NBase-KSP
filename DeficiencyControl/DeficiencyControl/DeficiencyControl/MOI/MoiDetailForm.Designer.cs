namespace DeficiencyControl.MOI
{
    partial class MoiDetailForm
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
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.splitContainerMoi = new System.Windows.Forms.SplitContainer();
            this.panelHeaderControl = new System.Windows.Forms.Panel();
            this.moiHeaderControl1 = new DeficiencyControl.MOI.MoiHeaderControl();
            this.tabControlDetail = new System.Windows.Forms.TabControl();
            this.tabPageItem = new System.Windows.Forms.TabPage();
            this.moiDetailControl1 = new DeficiencyControl.MOI.MoiDetailControl();
            this.tabPageAttachment = new System.Windows.Forms.TabPage();
            this.moiDetailAttachmentControl1 = new DeficiencyControl.MOI.MoiDetailAttachmentControl();
            this.splitContainerMoi.Panel1.SuspendLayout();
            this.splitContainerMoi.Panel2.SuspendLayout();
            this.splitContainerMoi.SuspendLayout();
            this.panelHeaderControl.SuspendLayout();
            this.tabControlDetail.SuspendLayout();
            this.tabPageItem.SuspendLayout();
            this.tabPageAttachment.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.Location = new System.Drawing.Point(638, 620);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(130, 50);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Delete\r\n(削除)";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUpdate.Location = new System.Drawing.Point(502, 620);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(130, 50);
            this.buttonUpdate.TabIndex = 1;
            this.buttonUpdate.Text = "Update\r\n(更新)";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(1122, 620);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(130, 50);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close\r\n(閉じる)";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // splitContainerMoi
            // 
            this.splitContainerMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMoi.Location = new System.Drawing.Point(12, 12);
            this.splitContainerMoi.Name = "splitContainerMoi";
            this.splitContainerMoi.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMoi.Panel1
            // 
            this.splitContainerMoi.Panel1.Controls.Add(this.panelHeaderControl);
            // 
            // splitContainerMoi.Panel2
            // 
            this.splitContainerMoi.Panel2.AutoScroll = true;
            this.splitContainerMoi.Panel2.Controls.Add(this.tabControlDetail);
            this.splitContainerMoi.Size = new System.Drawing.Size(1240, 602);
            this.splitContainerMoi.SplitterDistance = 241;
            this.splitContainerMoi.TabIndex = 0;
            // 
            // panelHeaderControl
            // 
            this.panelHeaderControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeaderControl.AutoScroll = true;
            this.panelHeaderControl.Controls.Add(this.moiHeaderControl1);
            this.panelHeaderControl.Location = new System.Drawing.Point(3, 3);
            this.panelHeaderControl.Name = "panelHeaderControl";
            this.panelHeaderControl.Size = new System.Drawing.Size(1234, 235);
            this.panelHeaderControl.TabIndex = 0;
            // 
            // moiHeaderControl1
            // 
            this.moiHeaderControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.moiHeaderControl1.Location = new System.Drawing.Point(11, 4);
            this.moiHeaderControl1.Margin = new System.Windows.Forms.Padding(4);
            this.moiHeaderControl1.Name = "moiHeaderControl1";
            this.moiHeaderControl1.Size = new System.Drawing.Size(1150, 508);
            this.moiHeaderControl1.TabIndex = 0;
            // 
            // tabControlDetail
            // 
            this.tabControlDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlDetail.Controls.Add(this.tabPageItem);
            this.tabControlDetail.Controls.Add(this.tabPageAttachment);
            this.tabControlDetail.Location = new System.Drawing.Point(3, 3);
            this.tabControlDetail.Name = "tabControlDetail";
            this.tabControlDetail.SelectedIndex = 0;
            this.tabControlDetail.Size = new System.Drawing.Size(1234, 351);
            this.tabControlDetail.TabIndex = 0;
            // 
            // tabPageItem
            // 
            this.tabPageItem.AutoScroll = true;
            this.tabPageItem.Controls.Add(this.moiDetailControl1);
            this.tabPageItem.Location = new System.Drawing.Point(4, 26);
            this.tabPageItem.Name = "tabPageItem";
            this.tabPageItem.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageItem.Size = new System.Drawing.Size(1226, 321);
            this.tabPageItem.TabIndex = 0;
            this.tabPageItem.Text = "Item";
            this.tabPageItem.UseVisualStyleBackColor = true;
            // 
            // moiDetailControl1
            // 
            this.moiDetailControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.moiDetailControl1.Location = new System.Drawing.Point(7, 7);
            this.moiDetailControl1.Margin = new System.Windows.Forms.Padding(4);
            this.moiDetailControl1.Name = "moiDetailControl1";
            this.moiDetailControl1.Size = new System.Drawing.Size(1150, 974);
            this.moiDetailControl1.TabIndex = 0;
            // 
            // tabPageAttachment
            // 
            this.tabPageAttachment.Controls.Add(this.moiDetailAttachmentControl1);
            this.tabPageAttachment.Location = new System.Drawing.Point(4, 22);
            this.tabPageAttachment.Name = "tabPageAttachment";
            this.tabPageAttachment.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAttachment.Size = new System.Drawing.Size(1226, 325);
            this.tabPageAttachment.TabIndex = 1;
            this.tabPageAttachment.Text = "Attachment";
            this.tabPageAttachment.UseVisualStyleBackColor = true;
            // 
            // moiDetailAttachmentControl1
            // 
            this.moiDetailAttachmentControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moiDetailAttachmentControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.moiDetailAttachmentControl1.Location = new System.Drawing.Point(3, 3);
            this.moiDetailAttachmentControl1.Margin = new System.Windows.Forms.Padding(4);
            this.moiDetailAttachmentControl1.Name = "moiDetailAttachmentControl1";
            this.moiDetailAttachmentControl1.Size = new System.Drawing.Size(1220, 319);
            this.moiDetailAttachmentControl1.TabIndex = 0;
            // 
            // MoiDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.splitContainerMoi);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MoiDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "検船 (Detail/編集)";
            this.Load += new System.EventHandler(this.MoiDetailForm_Load);
            this.splitContainerMoi.Panel1.ResumeLayout(false);
            this.splitContainerMoi.Panel2.ResumeLayout(false);
            this.splitContainerMoi.ResumeLayout(false);
            this.panelHeaderControl.ResumeLayout(false);
            this.tabControlDetail.ResumeLayout(false);
            this.tabPageItem.ResumeLayout(false);
            this.tabPageAttachment.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.SplitContainer splitContainerMoi;
        public System.Windows.Forms.Panel panelHeaderControl;
        private System.Windows.Forms.TabControl tabControlDetail;
        private System.Windows.Forms.TabPage tabPageItem;
        private System.Windows.Forms.TabPage tabPageAttachment;
        public MoiHeaderControl moiHeaderControl1;
        public MoiDetailControl moiDetailControl1;
        public MoiDetailAttachmentControl moiDetailAttachmentControl1;
    }
}