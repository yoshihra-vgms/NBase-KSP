namespace DeficiencyControl.Schedule
{
    partial class BaseScheduleInputControl
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
            this.comboBoxScheduleKind = new System.Windows.Forms.ComboBox();
            this.comboBoxScheduleKindDetail = new System.Windows.Forms.ComboBox();
            this.dateTimePickerEstimateDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerInspectionDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxRecordMemo = new System.Windows.Forms.TextBox();
            this.panelErrorScheduleKind = new System.Windows.Forms.Panel();
            this.panelErrorScheduleKindDetail = new System.Windows.Forms.Panel();
            this.panelErrorScheduleKind.SuspendLayout();
            this.panelErrorScheduleKindDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxDelete
            // 
            this.checkBoxDelete.TabIndex = 0;
            // 
            // comboBoxScheduleKind
            // 
            this.comboBoxScheduleKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleKind.FormattingEnabled = true;
            this.comboBoxScheduleKind.Location = new System.Drawing.Point(3, 3);
            this.comboBoxScheduleKind.Name = "comboBoxScheduleKind";
            this.comboBoxScheduleKind.Size = new System.Drawing.Size(174, 24);
            this.comboBoxScheduleKind.TabIndex = 302;
            this.comboBoxScheduleKind.SelectedIndexChanged += new System.EventHandler(this.comboBoxScheduleKind_SelectedIndexChanged);
            // 
            // comboBoxScheduleKindDetail
            // 
            this.comboBoxScheduleKindDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleKindDetail.FormattingEnabled = true;
            this.comboBoxScheduleKindDetail.Location = new System.Drawing.Point(3, 3);
            this.comboBoxScheduleKindDetail.Name = "comboBoxScheduleKindDetail";
            this.comboBoxScheduleKindDetail.Size = new System.Drawing.Size(174, 24);
            this.comboBoxScheduleKindDetail.TabIndex = 303;
            this.comboBoxScheduleKindDetail.SelectedIndexChanged += new System.EventHandler(this.comboBoxScheduleKindDetail_SelectedIndexChanged);
            // 
            // dateTimePickerEstimateDate
            // 
            this.dateTimePickerEstimateDate.Location = new System.Drawing.Point(396, 7);
            this.dateTimePickerEstimateDate.Name = "dateTimePickerEstimateDate";
            this.dateTimePickerEstimateDate.Size = new System.Drawing.Size(151, 23);
            this.dateTimePickerEstimateDate.TabIndex = 3;
            // 
            // dateTimePickerInspectionDate
            // 
            this.dateTimePickerInspectionDate.Checked = false;
            this.dateTimePickerInspectionDate.Location = new System.Drawing.Point(553, 7);
            this.dateTimePickerInspectionDate.Name = "dateTimePickerInspectionDate";
            this.dateTimePickerInspectionDate.ShowCheckBox = true;
            this.dateTimePickerInspectionDate.Size = new System.Drawing.Size(169, 23);
            this.dateTimePickerInspectionDate.TabIndex = 4;
            this.dateTimePickerInspectionDate.ValueChanged += new System.EventHandler(this.dateTimePickerInspectionDate_ValueChanged);
            // 
            // dateTimePickerExpiryDate
            // 
            this.dateTimePickerExpiryDate.Checked = false;
            this.dateTimePickerExpiryDate.Location = new System.Drawing.Point(728, 7);
            this.dateTimePickerExpiryDate.Name = "dateTimePickerExpiryDate";
            this.dateTimePickerExpiryDate.Size = new System.Drawing.Size(169, 23);
            this.dateTimePickerExpiryDate.TabIndex = 5;
            // 
            // textBoxRecordMemo
            // 
            this.textBoxRecordMemo.Location = new System.Drawing.Point(903, 7);
            this.textBoxRecordMemo.Name = "textBoxRecordMemo";
            this.textBoxRecordMemo.Size = new System.Drawing.Size(242, 23);
            this.textBoxRecordMemo.TabIndex = 6;
            // 
            // panelErrorScheduleKind
            // 
            this.panelErrorScheduleKind.Controls.Add(this.comboBoxScheduleKind);
            this.panelErrorScheduleKind.Location = new System.Drawing.Point(24, 3);
            this.panelErrorScheduleKind.Name = "panelErrorScheduleKind";
            this.panelErrorScheduleKind.Size = new System.Drawing.Size(180, 30);
            this.panelErrorScheduleKind.TabIndex = 1;
            // 
            // panelErrorScheduleKindDetail
            // 
            this.panelErrorScheduleKindDetail.Controls.Add(this.comboBoxScheduleKindDetail);
            this.panelErrorScheduleKindDetail.Location = new System.Drawing.Point(210, 3);
            this.panelErrorScheduleKindDetail.Name = "panelErrorScheduleKindDetail";
            this.panelErrorScheduleKindDetail.Size = new System.Drawing.Size(180, 30);
            this.panelErrorScheduleKindDetail.TabIndex = 2;
            // 
            // BaseScheduleInputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panelErrorScheduleKindDetail);
            this.Controls.Add(this.panelErrorScheduleKind);
            this.Controls.Add(this.textBoxRecordMemo);
            this.Controls.Add(this.dateTimePickerExpiryDate);
            this.Controls.Add(this.dateTimePickerInspectionDate);
            this.Controls.Add(this.dateTimePickerEstimateDate);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Name = "BaseScheduleInputControl";
            this.Size = new System.Drawing.Size(1148, 36);
            this.Load += new System.EventHandler(this.BaseScheduleInputControl_Load);
            this.Controls.SetChildIndex(this.checkBoxDelete, 0);
            this.Controls.SetChildIndex(this.dateTimePickerEstimateDate, 0);
            this.Controls.SetChildIndex(this.dateTimePickerInspectionDate, 0);
            this.Controls.SetChildIndex(this.dateTimePickerExpiryDate, 0);
            this.Controls.SetChildIndex(this.textBoxRecordMemo, 0);
            this.Controls.SetChildIndex(this.panelErrorScheduleKind, 0);
            this.Controls.SetChildIndex(this.panelErrorScheduleKindDetail, 0);
            this.panelErrorScheduleKind.ResumeLayout(false);
            this.panelErrorScheduleKindDetail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBoxScheduleKind;
        public System.Windows.Forms.ComboBox comboBoxScheduleKindDetail;
        public System.Windows.Forms.DateTimePicker dateTimePickerEstimateDate;
        public System.Windows.Forms.DateTimePicker dateTimePickerInspectionDate;
        public System.Windows.Forms.DateTimePicker dateTimePickerExpiryDate;
        public System.Windows.Forms.TextBox textBoxRecordMemo;
        protected System.Windows.Forms.Panel panelErrorScheduleKind;
        protected System.Windows.Forms.Panel panelErrorScheduleKindDetail;
    }
}
