namespace DeficiencyControl.Schedule
{
    partial class ScheduleInputControlOther
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
            this.textBoxEventMemo = new System.Windows.Forms.TextBox();
            this.panelErrorScheduleKind.SuspendLayout();
            this.panelErrorScheduleKindDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxScheduleKind
            // 
            this.comboBoxScheduleKind.Location = new System.Drawing.Point(461, 33);
            this.comboBoxScheduleKind.Size = new System.Drawing.Size(181, 24);
            this.comboBoxScheduleKind.Visible = false;
            // 
            // comboBoxScheduleKindDetail
            // 
            this.comboBoxScheduleKindDetail.Location = new System.Drawing.Point(648, 33);
            this.comboBoxScheduleKindDetail.Size = new System.Drawing.Size(181, 24);
            this.comboBoxScheduleKindDetail.Visible = false;
            // 
            // dateTimePickerEstimateDate
            // 
            this.dateTimePickerEstimateDate.TabIndex = 2;
            // 
            // dateTimePickerInspectionDate
            // 
            this.dateTimePickerInspectionDate.TabIndex = 3;
            // 
            // dateTimePickerExpiryDate
            // 
            this.dateTimePickerExpiryDate.TabIndex = 4;
            // 
            // textBoxRecordMemo
            // 
            this.textBoxRecordMemo.TabIndex = 5;
            // 
            // checkBoxDelete
            // 
            this.checkBoxDelete.TabIndex = 0;
            // 
            // textBoxEventMemo
            // 
            this.textBoxEventMemo.Location = new System.Drawing.Point(24, 7);
            this.textBoxEventMemo.Name = "textBoxEventMemo";
            this.textBoxEventMemo.Size = new System.Drawing.Size(368, 23);
            this.textBoxEventMemo.TabIndex = 1;
            // 
            // ScheduleInputControlOther
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxEventMemo);
            this.Name = "ScheduleInputControlOther";
            this.Load += new System.EventHandler(this.ScheduleInputControlOther_Load);
            this.Controls.SetChildIndex(this.panelErrorScheduleKind, 0);
            this.Controls.SetChildIndex(this.panelErrorScheduleKindDetail, 0);
            this.Controls.SetChildIndex(this.checkBoxDelete, 0);
            this.Controls.SetChildIndex(this.dateTimePickerEstimateDate, 0);
            this.Controls.SetChildIndex(this.dateTimePickerInspectionDate, 0);
            this.Controls.SetChildIndex(this.dateTimePickerExpiryDate, 0);
            this.Controls.SetChildIndex(this.textBoxRecordMemo, 0);
            this.Controls.SetChildIndex(this.textBoxEventMemo, 0);
            this.panelErrorScheduleKind.ResumeLayout(false);
            this.panelErrorScheduleKindDetail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxEventMemo;
    }
}
