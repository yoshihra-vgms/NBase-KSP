namespace DeficiencyControl.Controls
{
    partial class CommentDetailAttachmentControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewAttachment = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.dataCountControl1 = new DeficiencyControl.Controls.DataCountControl();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttachment)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAttachment
            // 
            this.dataGridViewAttachment.AllowUserToAddRows = false;
            this.dataGridViewAttachment.AllowUserToDeleteRows = false;
            this.dataGridViewAttachment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAttachment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAttachment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dataGridViewAttachment.EnableHeadersVisualStyles = false;
            this.dataGridViewAttachment.Location = new System.Drawing.Point(3, 27);
            this.dataGridViewAttachment.MultiSelect = false;
            this.dataGridViewAttachment.Name = "dataGridViewAttachment";
            this.dataGridViewAttachment.ReadOnly = true;
            this.dataGridViewAttachment.RowHeadersVisible = false;
            this.dataGridViewAttachment.RowTemplate.Height = 21;
            this.dataGridViewAttachment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAttachment.Size = new System.Drawing.Size(1194, 370);
            this.dataGridViewAttachment.TabIndex = 2;
            this.dataGridViewAttachment.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAttachment_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Data";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "No";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Date(日付)";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Attachment Name (ファイル添付場所)";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 330;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "File Name (ファイル名)";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 450;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "File Update Date\\n(ファイル更新日)";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 200;
            // 
            // dataCountControl1
            // 
            this.dataCountControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataCountControl1.AutoSize = true;
            this.dataCountControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dataCountControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.dataCountControl1.Location = new System.Drawing.Point(1115, 4);
            this.dataCountControl1.Margin = new System.Windows.Forms.Padding(4);
            this.dataCountControl1.Name = "dataCountControl1";
            this.dataCountControl1.Size = new System.Drawing.Size(81, 16);
            this.dataCountControl1.TabIndex = 3;
            // 
            // CommentDetailAttachmentControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.dataCountControl1);
            this.Controls.Add(this.dataGridViewAttachment);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Name = "CommentDetailAttachmentControl";
            this.Size = new System.Drawing.Size(1200, 400);
            this.Load += new System.EventHandler(this.CommentDetailAttachmentControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttachment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAttachment;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private DataCountControl dataCountControl1;
    }
}
