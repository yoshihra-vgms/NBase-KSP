namespace DeficiencyControl.Controls.CommentItem
{
    partial class DeficiencyCodeSelectControl
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
            this.singleLineComboDeficiencyCodeText = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboDeficiencyCode = new DeficiencyControl.Util.SingleLineCombo();
            this.SuspendLayout();
            // 
            // singleLineComboDeficiencyCodeText
            // 
            this.singleLineComboDeficiencyCodeText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.singleLineComboDeficiencyCodeText.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboDeficiencyCodeText.Location = new System.Drawing.Point(85, 4);
            this.singleLineComboDeficiencyCodeText.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboDeficiencyCodeText.MaxLength = 32767;
            this.singleLineComboDeficiencyCodeText.Name = "singleLineComboDeficiencyCodeText";
            this.singleLineComboDeficiencyCodeText.ReadOnly = false;
            this.singleLineComboDeficiencyCodeText.Size = new System.Drawing.Size(622, 23);
            this.singleLineComboDeficiencyCodeText.TabIndex = 276;
            this.singleLineComboDeficiencyCodeText.selected += new DeficiencyControl.Util.SingleLineCombo.SelectedEventHandler(this.singleLineComboDeficiencyCodeText_selected);
            this.singleLineComboDeficiencyCodeText.Cleared += new DeficiencyControl.Util.SingleLineCombo.SelectedEventHandler(this.singleLineComboDeficiencyCode_Cleared);
            // 
            // singleLineComboDeficiencyCode
            // 
            this.singleLineComboDeficiencyCode.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboDeficiencyCode.Location = new System.Drawing.Point(0, 4);
            this.singleLineComboDeficiencyCode.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboDeficiencyCode.MaxLength = 32767;
            this.singleLineComboDeficiencyCode.Name = "singleLineComboDeficiencyCode";
            this.singleLineComboDeficiencyCode.ReadOnly = false;
            this.singleLineComboDeficiencyCode.Size = new System.Drawing.Size(77, 23);
            this.singleLineComboDeficiencyCode.TabIndex = 275;
            this.singleLineComboDeficiencyCode.selected += new DeficiencyControl.Util.SingleLineCombo.SelectedEventHandler(this.singleLineComboDeficiencyCode_selected);
            this.singleLineComboDeficiencyCode.Cleared += new DeficiencyControl.Util.SingleLineCombo.SelectedEventHandler(this.singleLineComboDeficiencyCode_Cleared);
            // 
            // DeficiencyCodeSelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.singleLineComboDeficiencyCodeText);
            this.Controls.Add(this.singleLineComboDeficiencyCode);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DeficiencyCodeSelectControl";
            this.Size = new System.Drawing.Size(711, 31);
            this.Load += new System.EventHandler(this.DeficiencyCodeSelectControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Util.SingleLineCombo singleLineComboDeficiencyCodeText;
        private Util.SingleLineCombo singleLineComboDeficiencyCode;
    }
}
