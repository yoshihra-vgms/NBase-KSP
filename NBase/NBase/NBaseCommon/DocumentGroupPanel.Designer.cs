namespace NBaseCommon
{
    partial class DocumentGroupPanel
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
            this.label_Single = new System.Windows.Forms.Label();
            this.label_Multi1 = new System.Windows.Forms.Label();
            this.label_Multi2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Single
            // 
            this.label_Single.AutoSize = true;
            this.label_Single.Location = new System.Drawing.Point(16, 16);
            this.label_Single.Name = "label_Single";
            this.label_Single.Size = new System.Drawing.Size(37, 12);
            this.label_Single.TabIndex = 0;
            this.label_Single.Text = "１行用";
            this.label_Single.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Multi1
            // 
            this.label_Multi1.AutoSize = true;
            this.label_Multi1.Location = new System.Drawing.Point(0, 7);
            this.label_Multi1.Name = "label_Multi1";
            this.label_Multi1.Size = new System.Drawing.Size(69, 12);
            this.label_Multi1.TabIndex = 1;
            this.label_Multi1.Text = "２行用１行目";
            this.label_Multi1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Multi2
            // 
            this.label_Multi2.AutoSize = true;
            this.label_Multi2.Location = new System.Drawing.Point(0, 25);
            this.label_Multi2.Name = "label_Multi2";
            this.label_Multi2.Size = new System.Drawing.Size(69, 12);
            this.label_Multi2.TabIndex = 2;
            this.label_Multi2.Text = "２行用２行目";
            this.label_Multi2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DocumentGroupPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label_Multi2);
            this.Controls.Add(this.label_Multi1);
            this.Controls.Add(this.label_Single);
            this.Name = "DocumentGroupPanel";
            this.Size = new System.Drawing.Size(69, 44);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Single;
        private System.Windows.Forms.Label label_Multi1;
        private System.Windows.Forms.Label label_Multi2;
    }
}
