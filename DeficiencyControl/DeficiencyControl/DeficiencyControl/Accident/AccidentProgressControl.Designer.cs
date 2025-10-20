namespace DeficiencyControl.Accident
{
    partial class AccidentProgressControl
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
            this.buttonAttachmentProgress = new System.Windows.Forms.Button();
            this.fileViewControlExProgress = new DeficiencyControl.Util.FileViewControlEx();
            this.textBoxProgress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
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
            this.label2.Location = new System.Drawing.Point(3, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 292;
            this.label2.Text = "添付資料";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonAttachmentProgress
            // 
            this.buttonAttachmentProgress.Location = new System.Drawing.Point(3, 274);
            this.buttonAttachmentProgress.Name = "buttonAttachmentProgress";
            this.buttonAttachmentProgress.Size = new System.Drawing.Size(54, 23);
            this.buttonAttachmentProgress.TabIndex = 3;
            this.buttonAttachmentProgress.Tag = "0";
            this.buttonAttachmentProgress.Text = "...";
            this.buttonAttachmentProgress.UseVisualStyleBackColor = true;
            this.buttonAttachmentProgress.Click += new System.EventHandler(this.buttonAttachmentProgress_Click);
            // 
            // fileViewControlExProgress
            // 
            this.fileViewControlExProgress.AllowDrop = true;
            this.fileViewControlExProgress.EnableDelete = true;
            this.fileViewControlExProgress.EnableDragDrop = true;
            this.fileViewControlExProgress.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.fileViewControlExProgress.Location = new System.Drawing.Point(81, 177);
            this.fileViewControlExProgress.Name = "fileViewControlExProgress";
            this.fileViewControlExProgress.Size = new System.Drawing.Size(426, 120);
            this.fileViewControlExProgress.TabIndex = 4;
            this.fileViewControlExProgress.FileItemSelected += new DeficiencyControl.Util.FileViewControlEx.FileItemSelectDelegate(this.fileViewControlExProgress_FileItemSelected);
            // 
            // textBoxProgress
            // 
            this.textBoxProgress.Location = new System.Drawing.Point(81, 51);
            this.textBoxProgress.Multiline = true;
            this.textBoxProgress.Name = "textBoxProgress";
            this.textBoxProgress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxProgress.Size = new System.Drawing.Size(426, 120);
            this.textBoxProgress.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label3.Location = new System.Drawing.Point(3, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 288;
            this.label3.Text = "報告内容";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label1.Location = new System.Drawing.Point(3, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 293;
            this.label1.Text = "更新日";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Location = new System.Drawing.Point(81, 22);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(200, 23);
            this.dateTimePickerDate.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // AccidentProgressControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dateTimePickerDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonAttachmentProgress);
            this.Controls.Add(this.fileViewControlExProgress);
            this.Controls.Add(this.textBoxProgress);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AccidentProgressControl";
            this.Size = new System.Drawing.Size(510, 300);
            this.Load += new System.EventHandler(this.AccidentProgressControl_Load);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.textBoxProgress, 0);
            this.Controls.SetChildIndex(this.fileViewControlExProgress, 0);
            this.Controls.SetChildIndex(this.buttonAttachmentProgress, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dateTimePickerDate, 0);
            this.Controls.SetChildIndex(this.checkBoxDelete, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAttachmentProgress;
        private Util.FileViewControlEx fileViewControlExProgress;
        private System.Windows.Forms.TextBox textBoxProgress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
