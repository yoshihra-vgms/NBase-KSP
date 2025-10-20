namespace DeficiencyControl.MOI
{
    partial class ViqNoControl
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
            this.textBoxViqNoDescription = new System.Windows.Forms.TextBox();
            this.singleLineComboViqNo = new DeficiencyControl.Util.SingleLineCombo();
            this.SuspendLayout();
            // 
            // textBoxViqNoDescription
            // 
            this.textBoxViqNoDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxViqNoDescription.Location = new System.Drawing.Point(183, 4);
            this.textBoxViqNoDescription.Multiline = true;
            this.textBoxViqNoDescription.Name = "textBoxViqNoDescription";
            this.textBoxViqNoDescription.ReadOnly = true;
            this.textBoxViqNoDescription.Size = new System.Drawing.Size(685, 88);
            this.textBoxViqNoDescription.TabIndex = 1;
            // 
            // singleLineComboViqNo
            // 
            this.singleLineComboViqNo.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboViqNo.Location = new System.Drawing.Point(4, 4);
            this.singleLineComboViqNo.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboViqNo.MaxLength = 32767;
            this.singleLineComboViqNo.Name = "singleLineComboViqNo";
            this.singleLineComboViqNo.ReadOnly = false;
            this.singleLineComboViqNo.Size = new System.Drawing.Size(144, 23);
            this.singleLineComboViqNo.TabIndex = 0;
            this.singleLineComboViqNo.selected += new DeficiencyControl.Util.SingleLineCombo.SelectedEventHandler(this.singleLineComboViqNo_selected);
            this.singleLineComboViqNo.Cleared += new DeficiencyControl.Util.SingleLineCombo.SelectedEventHandler(this.singleLineComboViqNo_Cleared);
            // 
            // ViqNoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.textBoxViqNoDescription);
            this.Controls.Add(this.singleLineComboViqNo);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ViqNoControl";
            this.Size = new System.Drawing.Size(871, 95);
            this.Load += new System.EventHandler(this.ViqNoControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBoxViqNoDescription;
        public Util.SingleLineCombo singleLineComboViqNo;

    }
}
