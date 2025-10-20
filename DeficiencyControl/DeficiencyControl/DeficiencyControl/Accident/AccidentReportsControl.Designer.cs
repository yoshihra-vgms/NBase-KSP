namespace DeficiencyControl.Accident
{
    partial class AccidentReportsControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAttachmentReports = new System.Windows.Forms.Button();
            this.fileViewControlExReports = new DeficiencyControl.Util.FileViewControlEx();
            this.textBoxReports = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // checkBoxDelete
            // 
            this.checkBoxDelete.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label2.Location = new System.Drawing.Point(3, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 297;
            this.label2.Text = "添付資料";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonAttachmentReports
            // 
            this.buttonAttachmentReports.Location = new System.Drawing.Point(3, 143);
            this.buttonAttachmentReports.Name = "buttonAttachmentReports";
            this.buttonAttachmentReports.Size = new System.Drawing.Size(54, 23);
            this.buttonAttachmentReports.TabIndex = 2;
            this.buttonAttachmentReports.Tag = "0";
            this.buttonAttachmentReports.Text = "...";
            this.buttonAttachmentReports.UseVisualStyleBackColor = true;
            this.buttonAttachmentReports.Click += new System.EventHandler(this.buttonAttachmentReports_Click);
            // 
            // fileViewControlExReports
            // 
            this.fileViewControlExReports.AllowDrop = true;
            this.fileViewControlExReports.EnableDelete = true;
            this.fileViewControlExReports.EnableDragDrop = true;
            this.fileViewControlExReports.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.fileViewControlExReports.Location = new System.Drawing.Point(81, 46);
            this.fileViewControlExReports.Name = "fileViewControlExReports";
            this.fileViewControlExReports.Size = new System.Drawing.Size(426, 120);
            this.fileViewControlExReports.TabIndex = 3;
            this.fileViewControlExReports.FileItemSelected += new DeficiencyControl.Util.FileViewControlEx.FileItemSelectDelegate(this.fileViewControlExReports_FileItemSelected);
            // 
            // textBoxReports
            // 
            this.textBoxReports.Location = new System.Drawing.Point(81, 17);
            this.textBoxReports.Name = "textBoxReports";
            this.textBoxReports.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReports.Size = new System.Drawing.Size(426, 23);
            this.textBoxReports.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label3.Location = new System.Drawing.Point(3, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 293;
            this.label3.Text = "提出先";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // AccidentReportsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonAttachmentReports);
            this.Controls.Add(this.fileViewControlExReports);
            this.Controls.Add(this.textBoxReports);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AccidentReportsControl";
            this.Size = new System.Drawing.Size(510, 172);
            this.Load += new System.EventHandler(this.AccidentReportsControl_Load);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.textBoxReports, 0);
            this.Controls.SetChildIndex(this.fileViewControlExReports, 0);
            this.Controls.SetChildIndex(this.buttonAttachmentReports, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.checkBoxDelete, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAttachmentReports;
        private Util.FileViewControlEx fileViewControlExReports;
        private System.Windows.Forms.TextBox textBoxReports;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
