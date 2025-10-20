namespace NBaseCommon
{
    partial class TuminiUserControl
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
            this.label_2 = new System.Windows.Forms.Label();
            this.comboBox_単位 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBox_積荷
            // 
            this.comboBox_積荷.FormattingEnabled = true;
            this.comboBox_積荷.Location = new System.Drawing.Point(96, 0);
            this.comboBox_積荷.Name = "comboBox_積荷";
            this.comboBox_積荷.Size = new System.Drawing.Size(113, 20);
            this.comboBox_積荷.TabIndex = 26;
            // 
            // label_1
            // 
            this.label_1.AutoSize = true;
            this.label_1.Location = new System.Drawing.Point(3, 3);
            this.label_1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label_1.Name = "label_1";
            this.label_1.Size = new System.Drawing.Size(45, 12);
            this.label_1.TabIndex = 25;
            this.label_1.Text = "※ 積荷";
            // 
            // textBox_数量
            // 
            this.textBox_数量.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_数量.Location = new System.Drawing.Point(96, 21);
            this.textBox_数量.MaxLength = 10;
            this.textBox_数量.Name = "textBox_数量";
            this.textBox_数量.Size = new System.Drawing.Size(55, 19);
            this.textBox_数量.TabIndex = 27;
            // 
            // label_2
            // 
            this.label_2.AutoSize = true;
            this.label_2.Location = new System.Drawing.Point(3, 24);
            this.label_2.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label_2.Name = "label_2";
            this.label_2.Size = new System.Drawing.Size(75, 12);
            this.label_2.TabIndex = 24;
            this.label_2.Text = "　　数量・単位";
            // 
            // comboBox_単位
            // 
            this.comboBox_単位.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_単位.FormattingEnabled = true;
            this.comboBox_単位.Location = new System.Drawing.Point(157, 21);
            this.comboBox_単位.Name = "comboBox_単位";
            this.comboBox_単位.Size = new System.Drawing.Size(51, 20);
            this.comboBox_単位.TabIndex = 28;
            // 
            // TuminiUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBox_単位);
            this.Controls.Add(this.comboBox_積荷);
            this.Controls.Add(this.label_1);
            this.Controls.Add(this.textBox_数量);
            this.Controls.Add(this.label_2);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.Name = "TuminiUserControl";
            this.Size = new System.Drawing.Size(230, 42);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_積荷;
        private System.Windows.Forms.Label label_1;
        private System.Windows.Forms.TextBox textBox_数量;
        private System.Windows.Forms.Label label_2;
        private System.Windows.Forms.ComboBox comboBox_単位;

    }
}
