namespace DeficiencyControl.Forms
{
    partial class CommentItemDetailForm
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
            this.tabControlDetail = new System.Windows.Forms.TabControl();
            this.tabPageItem = new System.Windows.Forms.TabPage();
            this.commentDetailItemControl1 = new DeficiencyControl.Controls.CommentDetailItemControl();
            this.tabPageAttachment = new System.Windows.Forms.TabPage();
            this.commentDetailAttachmentControl1 = new DeficiencyControl.Controls.CommentDetailAttachmentControl();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.splitContainerItemControl.Panel1.SuspendLayout();
            this.splitContainerItemControl.Panel2.SuspendLayout();
            this.splitContainerItemControl.SuspendLayout();
            this.tabControlDetail.SuspendLayout();
            this.tabPageItem.SuspendLayout();
            this.tabPageAttachment.SuspendLayout();
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
            this.splitContainerItemControl.Panel2.Controls.Add(this.tabControlDetail);
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
            this.tabControlDetail.Size = new System.Drawing.Size(1234, 391);
            this.tabControlDetail.TabIndex = 10;
            // 
            // tabPageItem
            // 
            this.tabPageItem.AutoScroll = true;
            this.tabPageItem.Controls.Add(this.commentDetailItemControl1);
            this.tabPageItem.Location = new System.Drawing.Point(4, 26);
            this.tabPageItem.Name = "tabPageItem";
            this.tabPageItem.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageItem.Size = new System.Drawing.Size(1226, 361);
            this.tabPageItem.TabIndex = 0;
            this.tabPageItem.Text = "Item";
            this.tabPageItem.UseVisualStyleBackColor = true;
            // 
            // commentDetailItemControl1
            // 
            this.commentDetailItemControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.commentDetailItemControl1.Location = new System.Drawing.Point(7, 4);
            this.commentDetailItemControl1.Margin = new System.Windows.Forms.Padding(4);
            this.commentDetailItemControl1.Name = "commentDetailItemControl1";
            this.commentDetailItemControl1.Size = new System.Drawing.Size(225, 200);
            this.commentDetailItemControl1.TabIndex = 0;
            // 
            // tabPageAttachment
            // 
            this.tabPageAttachment.Controls.Add(this.commentDetailAttachmentControl1);
            this.tabPageAttachment.Location = new System.Drawing.Point(4, 22);
            this.tabPageAttachment.Name = "tabPageAttachment";
            this.tabPageAttachment.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAttachment.Size = new System.Drawing.Size(1226, 365);
            this.tabPageAttachment.TabIndex = 1;
            this.tabPageAttachment.Text = "Attachment";
            this.tabPageAttachment.UseVisualStyleBackColor = true;
            // 
            // commentDetailAttachmentControl1
            // 
            this.commentDetailAttachmentControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commentDetailAttachmentControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.commentDetailAttachmentControl1.Location = new System.Drawing.Point(6, 6);
            this.commentDetailAttachmentControl1.Name = "commentDetailAttachmentControl1";
            this.commentDetailAttachmentControl1.Size = new System.Drawing.Size(1214, 352);
            this.commentDetailAttachmentControl1.TabIndex = 0;
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
            // buttonUpdate
            // 
            this.buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpdate.Location = new System.Drawing.Point(502, 620);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(130, 50);
            this.buttonUpdate.TabIndex = 1;
            this.buttonUpdate.Text = "Update\r\n(更新)";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Location = new System.Drawing.Point(638, 620);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(130, 50);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Delete\r\n(削除)";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // CommentItemDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.splitContainerItemControl);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CommentItemDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PSC (Detail/編集)";
            this.Load += new System.EventHandler(this.CommentItemDetailForm_Load);
            this.splitContainerItemControl.Panel1.ResumeLayout(false);
            this.splitContainerItemControl.Panel2.ResumeLayout(false);
            this.splitContainerItemControl.ResumeLayout(false);
            this.tabControlDetail.ResumeLayout(false);
            this.tabPageItem.ResumeLayout(false);
            this.tabPageAttachment.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerItemControl;
        public System.Windows.Forms.Panel panelHeaderControl;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.TabControl tabControlDetail;
        private System.Windows.Forms.TabPage tabPageItem;
        private System.Windows.Forms.TabPage tabPageAttachment;
        public Controls.CommentDetailItemControl commentDetailItemControl1;
        public Controls.CommentDetailAttachmentControl commentDetailAttachmentControl1;
    }
}