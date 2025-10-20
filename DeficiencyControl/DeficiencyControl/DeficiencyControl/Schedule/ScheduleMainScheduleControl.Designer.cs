namespace DeficiencyControl.Schedule
{
    partial class ScheduleMainScheduleControl
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
            this.scheduleListControl1 = new DeficiencyControl.Schedule.ListGrid.ScheduleListControl();
            this.SuspendLayout();
            // 
            // scheduleListControl1
            // 
            this.scheduleListControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scheduleListControl1.AutoScroll = true;
            this.scheduleListControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.scheduleListControl1.Location = new System.Drawing.Point(4, 4);
            this.scheduleListControl1.Margin = new System.Windows.Forms.Padding(4);
            this.scheduleListControl1.Name = "scheduleListControl1";
            this.scheduleListControl1.Size = new System.Drawing.Size(1175, 392);
            this.scheduleListControl1.TabIndex = 0;
            // 
            // ScheduleMainScheduleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.scheduleListControl1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ScheduleMainScheduleControl";
            this.Size = new System.Drawing.Size(1183, 400);
            this.Load += new System.EventHandler(this.ScheduleMainScheduleControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal ListGrid.ScheduleListControl scheduleListControl1;






    }
}
