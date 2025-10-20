namespace NBaseCommon
{
    partial class TuminiUserControl2
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
            this.comboBox_積荷 = new System.Windows.Forms.ComboBox();
            this.label_1 = new System.Windows.Forms.Label();
            this.textBox_数量 = new System.Windows.Forms.TextBox();
            this.comboBox_単位 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBox_積荷
            // 
            this.comboBox_積荷.FormattingEnabled = true;
            this.comboBox_積荷.Location = new System.Drawing.Point(96, 0);
            this.comboBox_積荷.Name = "comboBox_積荷";
            this.comboBox_積荷.Size = new System.Drawing.Size(135, 20);
            this.comboBox_積荷.TabIndex = 26;
            this.comboBox_積荷.Enter += new System.EventHandler(this.comboBox_積荷_Enter);
            // 
            // label_1
            // 
            this.label_1.AutoSize = true;
            this.label_1.Location = new System.Drawing.Point(3, 4);
            this.label_1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label_1.Name = "label_1";
            this.label_1.Size = new System.Drawing.Size(45, 12);
            this.label_1.TabIndex = 25;
            this.label_1.Text = "※ 積荷";
            // 
            // textBox_数量
            // 
            this.textBox_数量.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_数量.Location = new System.Drawing.Point(237, 1);
            this.textBox_数量.MaxLength = 10;
            this.textBox_数量.Name = "textBox_数量";
            this.textBox_数量.Size = new System.Drawing.Size(55, 19);
            this.textBox_数量.TabIndex = 27;
            // 
            // comboBox_単位
            // 
            this.comboBox_単位.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_単位.FormattingEnabled = true;
            this.comboBox_単位.Location = new System.Drawing.Point(296, 0);
            this.comboBox_単位.Name = "comboBox_単位";
            this.comboBox_単位.Size = new System.Drawing.Size(51, 20);
            this.comboBox_単位.TabIndex = 28;
            // 
            // TuminiUserControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox_単位);
            this.Controls.Add(this.comboBox_積荷);
            this.Controls.Add(this.label_1);
            this.Controls.Add(this.textBox_数量);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.Name = "TuminiUserControl2";
            this.Size = new System.Drawing.Size(347, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_積荷;
        private System.Windows.Forms.Label label_1;
        private System.Windows.Forms.TextBox textBox_数量;
        private System.Windows.Forms.ComboBox comboBox_単位;

    }
}
