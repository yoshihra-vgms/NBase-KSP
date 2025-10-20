namespace Yojitsu
{
    partial class 運航費入力Control_単数
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
            this.textBox小計 = new System.Windows.Forms.TextBox();
            this.textBox数量 = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label数量 = new System.Windows.Forms.Label();
            this.textBox単価 = new System.Windows.Forms.TextBox();
            this.label単価 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox小計
            // 
            this.textBox小計.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox小計.Location = new System.Drawing.Point(261, 0);
            this.textBox小計.Name = "textBox小計";
            this.textBox小計.ReadOnly = true;
            this.textBox小計.Size = new System.Drawing.Size(84, 19);
            this.textBox小計.TabIndex = 14;
            this.textBox小計.Text = "0";
            this.textBox小計.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox数量
            // 
            this.textBox数量.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox数量.Location = new System.Drawing.Point(174, 0);
            this.textBox数量.MaxLength = 10;
            this.textBox数量.Name = "textBox数量";
            this.textBox数量.Size = new System.Drawing.Size(64, 19);
            this.textBox数量.TabIndex = 13;
            this.textBox数量.Text = "0";
            this.textBox数量.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(244, 4);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(11, 12);
            this.label35.TabIndex = 11;
            this.label35.Text = "=";
            // 
            // label数量
            // 
            this.label数量.AutoSize = true;
            this.label数量.Location = new System.Drawing.Point(127, 4);
            this.label数量.Name = "label数量";
            this.label数量.Size = new System.Drawing.Size(43, 12);
            this.label数量.TabIndex = 10;
            this.label数量.Text = "数量(1)";
            // 
            // textBox単価
            // 
            this.textBox単価.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox単価.Location = new System.Drawing.Point(52, 0);
            this.textBox単価.MaxLength = 9;
            this.textBox単価.Name = "textBox単価";
            this.textBox単価.Size = new System.Drawing.Size(64, 19);
            this.textBox単価.TabIndex = 12;
            this.textBox単価.Text = "0";
            this.textBox単価.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label単価
            // 
            this.label単価.AutoSize = true;
            this.label単価.Location = new System.Drawing.Point(3, 3);
            this.label単価.Name = "label単価";
            this.label単価.Size = new System.Drawing.Size(43, 12);
            this.label単価.TabIndex = 8;
            this.label単価.Text = "単価(1)";
            // 
            // 運航費入力Control_単数
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.textBox小計);
            this.Controls.Add(this.textBox数量);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label数量);
            this.Controls.Add(this.textBox単価);
            this.Controls.Add(this.label単価);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "運航費入力Control_単数";
            this.Size = new System.Drawing.Size(348, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox小計;
        private System.Windows.Forms.TextBox textBox数量;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label数量;
        private System.Windows.Forms.TextBox textBox単価;
        private System.Windows.Forms.Label label単価;

    }
}
