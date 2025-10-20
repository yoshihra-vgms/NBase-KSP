namespace DeficiencyControl.Accident
{
    partial class AccidentDetailForm
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
            this.splitContainerAccident = new System.Windows.Forms.SplitContainer();
            this.panelHeaderControl = new System.Windows.Forms.Panel();
            this.accidentHeaderControl1 = new DeficiencyControl.Accident.AccidentHeaderControl();
            this.tabControlDetail = new System.Windows.Forms.TabControl();
            this.tabPageItem = new System.Windows.Forms.TabPage();
            this.accidentDetailControl1 = new DeficiencyControl.Accident.AccidentDetailControl();
            this.tabPageAttachment = new System.Windows.Forms.TabPage();
            this.accidentDetailAttachmentControl1 = new DeficiencyControl.Accident.AccidentDetailAttachmentControl();
            this.splitContainerAccident.Panel1.SuspendLayout();
            this.splitContainerAccident.Panel2.SuspendLayout();
            this.splitContainerAccident.SuspendLayout();
            this.panelHeaderControl.SuspendLayout();
            this.tabControlDetail.SuspendLayout();
            this.tabPageItem.SuspendLayout();
            this.tabPageAttachment.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainerAccident.Panel2.Controls.Add(this.tabControlDetail);
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
            this.panelHeaderControl.Location = new System.Drawing.Point(3, 3);
            this.panelHeaderControl.Name = "panelHeaderControl";
            this.panelHeaderControl.Size = new System.Drawing.Size(1234, 235);
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
            this.tabPageItem.Controls.Add(this.accidentDetailControl1);
            this.tabPageItem.Location = new System.Drawing.Point(4, 26);
            this.tabPageItem.Name = "tabPageItem";
            this.tabPageItem.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageItem.Size = new System.Drawing.Size(1226, 321);
            this.tabPageItem.TabIndex = 0;
            this.tabPageItem.Text = "Item";
            this.tabPageItem.UseVisualStyleBackColor = true;
            // 
            // accidentDetailControl1
            // 
            this.accidentDetailControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.accidentDetailControl1.Location = new System.Drawing.Point(7, 7);
            this.accidentDetailControl1.Margin = new System.Windows.Forms.Padding(4);
            this.accidentDetailControl1.Name = "accidentDetailControl1";
            this.accidentDetailControl1.Size = new System.Drawing.Size(1150, 1245);
            this.accidentDetailControl1.TabIndex = 0;
            // 
            // tabPageAttachment
            // 
            this.tabPageAttachment.Controls.Add(this.accidentDetailAttachmentControl1);
            this.tabPageAttachment.Location = new System.Drawing.Point(4, 22);
            this.tabPageAttachment.Name = "tabPageAttachment";
            this.tabPageAttachment.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAttachment.Size = new System.Drawing.Size(1226, 325);
            this.tabPageAttachment.TabIndex = 1;
            this.tabPageAttachment.Text = "Attachment";
            this.tabPageAttachment.UseVisualStyleBackColor = true;
            // 
            // accidentDetailAttachmentControl1
            // 
            this.accidentDetailAttachmentControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.accidentDetailAttachmentControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.accidentDetailAttachmentControl1.Location = new System.Drawing.Point(7, 7);
            this.accidentDetailAttachmentControl1.Margin = new System.Windows.Forms.Padding(4);
            this.accidentDetailAttachmentControl1.Name = "accidentDetailAttachmentControl1";
            this.accidentDetailAttachmentControl1.Size = new System.Drawing.Size(1212, 306);
            this.accidentDetailAttachmentControl1.TabIndex = 0;
            // 
            // AccidentDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.splitContainerAccident);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AccidentDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "事故・トラブル (Detail/編集)";
            this.Load += new System.EventHandler(this.AccidentDetailForm_Load);
            this.splitContainerAccident.Panel1.ResumeLayout(false);
            this.splitContainerAccident.Panel2.ResumeLayout(false);
            this.splitContainerAccident.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer splitContainerAccident;
        private System.Windows.Forms.TabControl tabControlDetail;
        private System.Windows.Forms.TabPage tabPageItem;
        private System.Windows.Forms.TabPage tabPageAttachment;
        public System.Windows.Forms.Panel panelHeaderControl;
        public AccidentHeaderControl accidentHeaderControl1;
        public AccidentDetailControl accidentDetailControl1;
        public AccidentDetailAttachmentControl accidentDetailAttachmentControl1;
    }
}