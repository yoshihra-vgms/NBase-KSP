namespace NBaseHonsen.Controls
{
    partial class VesselUserControl
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
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

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.船名_label = new System.Windows.Forms.Label();
            this.携帯_label = new System.Windows.Forms.Label();
            this.船長_label = new System.Windows.Forms.Label();
            this.電話_label = new System.Windows.Forms.Label();
            this.営業担当_label = new System.Windows.Forms.Label();
            this.工務監督_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // 船名_label
            // 
            this.船名_label.AutoSize = true;
            this.船名_label.Location = new System.Drawing.Point(3, 1);
            this.船名_label.Name = "船名_label";
            this.船名_label.Size = new System.Drawing.Size(29, 12);
            this.船名_label.TabIndex = 0;
            this.船名_label.Text = "船名";
            // 
            // 携帯_label
            // 
            this.携帯_label.AutoSize = true;
            this.携帯_label.Location = new System.Drawing.Point(3, 25);
            this.携帯_label.Name = "携帯_label";
            this.携帯_label.Size = new System.Drawing.Size(25, 12);
            this.携帯_label.TabIndex = 0;
            this.携帯_label.Text = "TEL";
            // 
            // 船長_label
            // 
            this.船長_label.AutoSize = true;
            this.船長_label.Location = new System.Drawing.Point(3, 13);
            this.船長_label.Name = "船長_label";
            this.船長_label.Size = new System.Drawing.Size(29, 12);
            this.船長_label.TabIndex = 0;
            this.船長_label.Text = "船長";
            // 
            // 電話_label
            // 
            this.電話_label.AutoSize = true;
            this.電話_label.Location = new System.Drawing.Point(3, 37);
            this.電話_label.Name = "電話_label";
            this.電話_label.Size = new System.Drawing.Size(25, 12);
            this.電話_label.TabIndex = 0;
            this.電話_label.Text = "TEL";
            // 
            // 営業担当_label
            // 
            this.営業担当_label.AutoSize = true;
            this.営業担当_label.Location = new System.Drawing.Point(3, 49);
            this.営業担当_label.Name = "営業担当_label";
            this.営業担当_label.Size = new System.Drawing.Size(17, 12);
            this.営業担当_label.TabIndex = 1;
            this.営業担当_label.Text = "営";
            // 
            // 工務監督_label
            // 
            this.工務監督_label.AutoSize = true;
            this.工務監督_label.Location = new System.Drawing.Point(3, 61);
            this.工務監督_label.Name = "工務監督_label";
            this.工務監督_label.Size = new System.Drawing.Size(17, 12);
            this.工務監督_label.TabIndex = 2;
            this.工務監督_label.Text = "工";
            // 
            // VesselUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.工務監督_label);
            this.Controls.Add(this.営業担当_label);
            this.Controls.Add(this.電話_label);
            this.Controls.Add(this.携帯_label);
            this.Controls.Add(this.船長_label);
            this.Controls.Add(this.船名_label);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "VesselUserControl";
            this.Size = new System.Drawing.Size(102, 90);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label 船名_label;
        private System.Windows.Forms.Label 携帯_label;
        private System.Windows.Forms.Label 船長_label;
        private System.Windows.Forms.Label 電話_label;
        private System.Windows.Forms.Label 営業担当_label;
        private System.Windows.Forms.Label 工務監督_label;
    }
}
