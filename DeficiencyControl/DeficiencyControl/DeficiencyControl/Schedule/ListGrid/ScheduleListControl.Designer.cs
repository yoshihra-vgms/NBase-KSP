namespace DeficiencyControl.Schedule.ListGrid
{
    partial class ScheduleListControl
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
            this.customFlowLayoutMonthVesselData = new DeficiencyControl.Util.CustomFlowLayout();
            this.scheduleListHeaderControl1 = new DeficiencyControl.Schedule.ListGrid.ScheduleListHeaderControl();
            this.SuspendLayout();
            // 
            // customFlowLayoutMonthVesselData
            // 
            this.customFlowLayoutMonthVesselData.AutoSize = true;
            this.customFlowLayoutMonthVesselData.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.customFlowLayoutMonthVesselData.Location = new System.Drawing.Point(0, 70);
            this.customFlowLayoutMonthVesselData.Margin = new System.Windows.Forms.Padding(0);
            this.customFlowLayoutMonthVesselData.Name = "customFlowLayoutMonthVesselData";
            this.customFlowLayoutMonthVesselData.Size = new System.Drawing.Size(392, 222);
            this.customFlowLayoutMonthVesselData.TabIndex = 0;
            // 
            // scheduleListHeaderControl1
            // 
            this.scheduleListHeaderControl1.AutoSize = true;
            this.scheduleListHeaderControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scheduleListHeaderControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.scheduleListHeaderControl1.Location = new System.Drawing.Point(0, 0);
            this.scheduleListHeaderControl1.Margin = new System.Windows.Forms.Padding(0);
            this.scheduleListHeaderControl1.Name = "scheduleListHeaderControl1";
            this.scheduleListHeaderControl1.Size = new System.Drawing.Size(397, 200);
            this.scheduleListHeaderControl1.TabIndex = 1;
            // 
            // ScheduleListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.scheduleListHeaderControl1);
            this.Controls.Add(this.customFlowLayoutMonthVesselData);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ScheduleListControl";
            this.Size = new System.Drawing.Size(656, 401);
            this.Load += new System.EventHandler(this.ScheduleListControl_Load);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScheduleListControl_Scroll);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Util.CustomFlowLayout customFlowLayoutMonthVesselData;
        private ScheduleListHeaderControl scheduleListHeaderControl1;


    }
}
