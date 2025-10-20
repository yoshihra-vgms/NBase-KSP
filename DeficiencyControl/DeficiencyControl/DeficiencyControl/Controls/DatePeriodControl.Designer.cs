namespace DeficiencyControl.Controls
{
    partial class DatePeriodControl
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
            this.label3 = new System.Windows.Forms.Label();
            this.linkedDatetimePickerDateEnd = new DeficiencyControl.Util.LinkedDatetimePicker();
            this.linkedDatetimePickerDateStart = new DeficiencyControl.Util.LinkedDatetimePicker();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 16);
            this.label3.TabIndex = 59;
            this.label3.Text = "～";
            // 
            // linkedDatetimePickerDateEnd
            // 
            this.linkedDatetimePickerDateEnd.Checked = false;
            this.linkedDatetimePickerDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.linkedDatetimePickerDateEnd.LinkDatetimePicker = this.linkedDatetimePickerDateStart;
            this.linkedDatetimePickerDateEnd.Location = new System.Drawing.Point(214, 3);
            this.linkedDatetimePickerDateEnd.Name = "linkedDatetimePickerDateEnd";
            this.linkedDatetimePickerDateEnd.ShowCheckBox = true;
            this.linkedDatetimePickerDateEnd.Size = new System.Drawing.Size(175, 23);
            this.linkedDatetimePickerDateEnd.TabIndex = 20;
            // 
            // linkedDatetimePickerDateStart
            // 
            this.linkedDatetimePickerDateStart.Checked = false;
            this.linkedDatetimePickerDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.linkedDatetimePickerDateStart.LinkDatetimePicker = this.linkedDatetimePickerDateEnd;
            this.linkedDatetimePickerDateStart.Location = new System.Drawing.Point(3, 3);
            this.linkedDatetimePickerDateStart.Name = "linkedDatetimePickerDateStart";
            this.linkedDatetimePickerDateStart.ShowCheckBox = true;
            this.linkedDatetimePickerDateStart.Size = new System.Drawing.Size(175, 23);
            this.linkedDatetimePickerDateStart.TabIndex = 10;
            // 
            // DatePeriodControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.linkedDatetimePickerDateEnd);
            this.Controls.Add(this.linkedDatetimePickerDateStart);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Name = "DatePeriodControl";
            this.Size = new System.Drawing.Size(393, 29);
            this.Load += new System.EventHandler(this.DatePeriodControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        public Util.LinkedDatetimePicker linkedDatetimePickerDateEnd;
        public Util.LinkedDatetimePicker linkedDatetimePickerDateStart;
    }
}
