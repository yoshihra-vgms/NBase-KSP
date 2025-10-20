namespace NBaseHonsen.Controls
{
    partial class PortUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortUserControl));
            this.PortName_label = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // PortName_label
            // 
            this.PortName_label.AutoSize = true;
            this.PortName_label.Location = new System.Drawing.Point(23, 6);
            this.PortName_label.Name = "PortName_label";
            this.PortName_label.Size = new System.Drawing.Size(35, 12);
            this.PortName_label.TabIndex = 0;
            this.PortName_label.Text = "label1";
            this.PortName_label.MouseLeave += new System.EventHandler(this.PortUserControl_MouseLeave);
            this.PortName_label.DoubleClick += new System.EventHandler(this.PortUserControl_DoubleClick);
            this.PortName_label.Click += new System.EventHandler(this.PortUserControl_Click);
            this.PortName_label.MouseEnter += new System.EventHandler(this.PortUserControl_MouseEnter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(15, 15);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.PortUserControl_DoubleClick);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.PortUserControl_MouseLeave);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.PortUserControl_MouseEnter);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(5, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(15, 15);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            this.pictureBox2.DoubleClick += new System.EventHandler(this.PortUserControl_DoubleClick);
            // 
            // PortUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.PortName_label);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PortUserControl";
            this.Size = new System.Drawing.Size(102, 25);
            this.Load += new System.EventHandler(this.PortUserControl_Load);
            this.DoubleClick += new System.EventHandler(this.PortUserControl_DoubleClick);
            this.MouseLeave += new System.EventHandler(this.PortUserControl_MouseLeave);
            this.Click += new System.EventHandler(this.PortUserControl_Click);
            this.MouseEnter += new System.EventHandler(this.PortUserControl_MouseEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PortName_label;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
