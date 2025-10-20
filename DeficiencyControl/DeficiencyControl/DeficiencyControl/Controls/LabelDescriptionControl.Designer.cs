namespace DeficiencyControl.Controls
{
    partial class LabelDescriptionControl
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
            this.labelAster = new System.Windows.Forms.Label();
            this.labelMainText = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.flowLayoutPanelText = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelText.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAster
            // 
            this.labelAster.AutoSize = true;
            this.labelAster.ForeColor = System.Drawing.Color.Red;
            this.labelAster.Location = new System.Drawing.Point(3, -2);
            this.labelAster.Name = "labelAster";
            this.labelAster.Size = new System.Drawing.Size(16, 16);
            this.labelAster.TabIndex = 0;
            this.labelAster.Text = "*";
            // 
            // labelMainText
            // 
            this.labelMainText.AutoSize = true;
            this.labelMainText.Location = new System.Drawing.Point(0, 0);
            this.labelMainText.Margin = new System.Windows.Forms.Padding(0);
            this.labelMainText.Name = "labelMainText";
            this.labelMainText.Size = new System.Drawing.Size(56, 16);
            this.labelMainText.TabIndex = 1;
            this.labelMainText.Text = "項目値";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescription.Location = new System.Drawing.Point(0, 16);
            this.labelDescription.Margin = new System.Windows.Forms.Padding(0);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(37, 15);
            this.labelDescription.TabIndex = 1;
            this.labelDescription.Text = "説明";
            // 
            // flowLayoutPanelText
            // 
            this.flowLayoutPanelText.AutoSize = true;
            this.flowLayoutPanelText.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelText.Controls.Add(this.labelMainText);
            this.flowLayoutPanelText.Controls.Add(this.labelDescription);
            this.flowLayoutPanelText.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelText.Location = new System.Drawing.Point(16, 0);
            this.flowLayoutPanelText.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelText.Name = "flowLayoutPanelText";
            this.flowLayoutPanelText.Size = new System.Drawing.Size(56, 31);
            this.flowLayoutPanelText.TabIndex = 2;
            // 
            // LabelDescriptionControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanelText);
            this.Controls.Add(this.labelAster);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Name = "LabelDescriptionControl";
            this.Size = new System.Drawing.Size(72, 31);
            this.Load += new System.EventHandler(this.LabelRemarkControl_Load);
            this.flowLayoutPanelText.ResumeLayout(false);
            this.flowLayoutPanelText.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAster;
        private System.Windows.Forms.Label labelMainText;
        private System.Windows.Forms.Label labelDescription;
        protected internal System.Windows.Forms.FlowLayoutPanel flowLayoutPanelText;
    }
}
