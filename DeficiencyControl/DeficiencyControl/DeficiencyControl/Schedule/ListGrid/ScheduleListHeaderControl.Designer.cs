namespace DeficiencyControl.Schedule.ListGrid
{
    partial class ScheduleListHeaderControl
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
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelData
            // 
            this.tableLayoutPanelData.Size = new System.Drawing.Size(101, 133);
            // 
            // labelMonth
            // 
            this.labelMonth.Text = "";
            // 
            // ScheduleListHeaderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ScheduleListHeaderControl";
            this.Size = new System.Drawing.Size(397, 200);
            this.Load += new System.EventHandler(this.ScheduleListHeaderControl_Load);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
