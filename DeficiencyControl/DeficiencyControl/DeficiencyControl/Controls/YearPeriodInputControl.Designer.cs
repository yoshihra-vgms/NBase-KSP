namespace DeficiencyControl.Controls
{
    partial class YearPeriodInputControl
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxEndYear = new System.Windows.Forms.TextBox();
            this.textBoxStartYear = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label3.Location = new System.Drawing.Point(245, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 316;
            this.label3.Text = "年度";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 315;
            this.label2.Text = "～";
            // 
            // textBoxEndYear
            // 
            this.textBoxEndYear.Location = new System.Drawing.Point(139, 3);
            this.textBoxEndYear.Name = "textBoxEndYear";
            this.textBoxEndYear.Size = new System.Drawing.Size(100, 23);
            this.textBoxEndYear.TabIndex = 1;
            // 
            // textBoxStartYear
            // 
            this.textBoxStartYear.Location = new System.Drawing.Point(3, 3);
            this.textBoxStartYear.Name = "textBoxStartYear";
            this.textBoxStartYear.Size = new System.Drawing.Size(100, 23);
            this.textBoxStartYear.TabIndex = 0;
            // 
            // YearPeriodInputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxEndYear);
            this.Controls.Add(this.textBoxStartYear);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "YearPeriodInputControl";
            this.Size = new System.Drawing.Size(285, 29);
            this.Load += new System.EventHandler(this.YearPeriodInputControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxEndYear;
        private System.Windows.Forms.TextBox textBoxStartYear;
    }
}
