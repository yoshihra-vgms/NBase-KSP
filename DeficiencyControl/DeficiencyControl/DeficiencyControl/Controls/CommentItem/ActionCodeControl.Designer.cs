namespace DeficiencyControl.Controls.CommentItem
{
    partial class ActionCodeControl
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
            this.comboBoxActionCode = new System.Windows.Forms.ComboBox();
            this.textBoxActionCodeText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // checkBoxDelete
            // 
            this.checkBoxDelete.TabIndex = 0;
            // 
            // comboBoxActionCode
            // 
            this.comboBoxActionCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxActionCode.FormattingEnabled = true;
            this.comboBoxActionCode.Location = new System.Drawing.Point(24, 3);
            this.comboBoxActionCode.Name = "comboBoxActionCode";
            this.comboBoxActionCode.Size = new System.Drawing.Size(77, 24);
            this.comboBoxActionCode.TabIndex = 1;
            this.comboBoxActionCode.SelectedIndexChanged += new System.EventHandler(this.comboBoxActionCode_SelectedIndexChanged);
            // 
            // textBoxActionCodeText
            // 
            this.textBoxActionCodeText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxActionCodeText.Location = new System.Drawing.Point(110, 3);
            this.textBoxActionCodeText.Name = "textBoxActionCodeText";
            this.textBoxActionCodeText.ReadOnly = true;
            this.textBoxActionCodeText.Size = new System.Drawing.Size(268, 23);
            this.textBoxActionCodeText.TabIndex = 2;
            // 
            // ActionCodeControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.textBoxActionCodeText);
            this.Controls.Add(this.comboBoxActionCode);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Name = "ActionCodeControl";
            this.Size = new System.Drawing.Size(381, 31);
            this.Load += new System.EventHandler(this.ActionCodeControl_Load);
            this.Controls.SetChildIndex(this.comboBoxActionCode, 0);
            this.Controls.SetChildIndex(this.textBoxActionCodeText, 0);
            this.Controls.SetChildIndex(this.checkBoxDelete, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxActionCode;
        private System.Windows.Forms.TextBox textBoxActionCodeText;
    }
}
