namespace DeficiencyControl.Controls
{
    partial class StatusSelectControl
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
            this.radioButtonPending = new System.Windows.Forms.RadioButton();
            this.radioButtonComplete = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radioButtonPending
            // 
            this.radioButtonPending.AutoSize = true;
            this.radioButtonPending.Checked = true;
            this.radioButtonPending.Location = new System.Drawing.Point(3, 3);
            this.radioButtonPending.Name = "radioButtonPending";
            this.radioButtonPending.Size = new System.Drawing.Size(78, 20);
            this.radioButtonPending.TabIndex = 0;
            this.radioButtonPending.TabStop = true;
            this.radioButtonPending.Text = "Pending";
            this.radioButtonPending.UseVisualStyleBackColor = true;
            // 
            // radioButtonComplete
            // 
            this.radioButtonComplete.AutoSize = true;
            this.radioButtonComplete.Location = new System.Drawing.Point(142, 3);
            this.radioButtonComplete.Name = "radioButtonComplete";
            this.radioButtonComplete.Size = new System.Drawing.Size(90, 20);
            this.radioButtonComplete.TabIndex = 0;
            this.radioButtonComplete.Text = "Complete";
            this.radioButtonComplete.UseVisualStyleBackColor = true;
            // 
            // StatusSelectControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.radioButtonComplete);
            this.Controls.Add(this.radioButtonPending);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Name = "StatusSelectControl";
            this.Size = new System.Drawing.Size(235, 26);
            this.Load += new System.EventHandler(this.StatusSelectControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonPending;
        private System.Windows.Forms.RadioButton radioButtonComplete;
    }
}
