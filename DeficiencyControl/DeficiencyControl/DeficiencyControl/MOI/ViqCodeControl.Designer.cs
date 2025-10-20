namespace DeficiencyControl.MOI
{
    partial class ViqCodeControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxViqCodeDescription = new System.Windows.Forms.TextBox();
            this.comboBoxViqCode = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 324;
            this.label1.Text = "章";
            // 
            // textBoxViqCodeDescription
            // 
            this.textBoxViqCodeDescription.Location = new System.Drawing.Point(183, 4);
            this.textBoxViqCodeDescription.Name = "textBoxViqCodeDescription";
            this.textBoxViqCodeDescription.ReadOnly = true;
            this.textBoxViqCodeDescription.Size = new System.Drawing.Size(348, 23);
            this.textBoxViqCodeDescription.TabIndex = 1;
            // 
            // comboBoxViqCode
            // 
            this.comboBoxViqCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxViqCode.FormattingEnabled = true;
            this.comboBoxViqCode.Location = new System.Drawing.Point(3, 4);
            this.comboBoxViqCode.Name = "comboBoxViqCode";
            this.comboBoxViqCode.Size = new System.Drawing.Size(144, 24);
            this.comboBoxViqCode.TabIndex = 0;
            this.comboBoxViqCode.SelectedIndexChanged += new System.EventHandler(this.comboBoxViqCode_SelectedIndexChanged);
            // 
            // ViqCodeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.comboBoxViqCode);
            this.Controls.Add(this.textBoxViqCodeDescription);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ViqCodeControl";
            this.Size = new System.Drawing.Size(534, 31);
            this.Load += new System.EventHandler(this.ViqCodeControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox comboBoxViqCode;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBoxViqCodeDescription;

    }
}
