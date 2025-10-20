namespace DeficiencyControl.Schedule.ListGrid
{
    partial class ScheduleVesselMonthControl
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
            this.labelMonth = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.tableLayoutPanelHeader = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelData = new DeficiencyControl.Util.CustomTableLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelMonth
            // 
            this.labelMonth.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelMonth.Font = new System.Drawing.Font("MS UI Gothic", 28F);
            this.labelMonth.Location = new System.Drawing.Point(0, 0);
            this.labelMonth.Name = "labelMonth";
            this.labelMonth.Size = new System.Drawing.Size(95, 200);
            this.labelMonth.TabIndex = 1;
            this.labelMonth.Text = "24月";
            this.labelMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelHeader
            // 
            this.panelHeader.AutoSize = true;
            this.panelHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHeader.Controls.Add(this.tableLayoutPanelHeader);
            this.panelHeader.Controls.Add(this.labelMonth);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(295, 200);
            this.panelHeader.TabIndex = 2;
            // 
            // tableLayoutPanelHeader
            // 
            this.tableLayoutPanelHeader.ColumnCount = 1;
            this.tableLayoutPanelHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelHeader.Location = new System.Drawing.Point(94, 0);
            this.tableLayoutPanelHeader.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelHeader.Name = "tableLayoutPanelHeader";
            this.tableLayoutPanelHeader.RowCount = 1;
            this.tableLayoutPanelHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelHeader.Size = new System.Drawing.Size(201, 200);
            this.tableLayoutPanelHeader.TabIndex = 2;
            // 
            // tableLayoutPanelData
            // 
            this.tableLayoutPanelData.Location = new System.Drawing.Point(296, 0);
            this.tableLayoutPanelData.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelData.Name = "tableLayoutPanelData";
            this.tableLayoutPanelData.Size = new System.Drawing.Size(404, 133);
            this.tableLayoutPanelData.TabIndex = 3;
            // 
            // ScheduleVesselMonthControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.tableLayoutPanelData);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ScheduleVesselMonthControl";
            this.Size = new System.Drawing.Size(700, 200);
            this.Load += new System.EventHandler(this.ScheduleVesselMonth_Load);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Util.CustomTableLayout tableLayoutPanelData;
        public System.Windows.Forms.Label labelMonth;
        public System.Windows.Forms.Panel panelHeader;
        public System.Windows.Forms.TableLayoutPanel tableLayoutPanelHeader;
    }
}
