namespace DeficiencyControl.Util
{
    partial class FileViewControlEx
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
            this.components = new System.ComponentModel.Container();
            this.listViewFile = new System.Windows.Forms.ListView();
            this.imageListFile = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // listViewFile
            // 
            this.listViewFile.AllowDrop = true;
            this.listViewFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFile.LargeImageList = this.imageListFile;
            this.listViewFile.Location = new System.Drawing.Point(0, 0);
            this.listViewFile.MultiSelect = false;
            this.listViewFile.Name = "listViewFile";
            this.listViewFile.Size = new System.Drawing.Size(150, 150);
            this.listViewFile.SmallImageList = this.imageListFile;
            this.listViewFile.TabIndex = 0;
            this.listViewFile.UseCompatibleStateImageBehavior = false;
            this.listViewFile.View = System.Windows.Forms.View.List;
            this.listViewFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewFile_DragDrop);
            this.listViewFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewFile_DragEnter);
            this.listViewFile.DoubleClick += new System.EventHandler(this.listViewFile_DoubleClick);
            this.listViewFile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewFile_KeyDown);
            // 
            // imageListFile
            // 
            this.imageListFile.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListFile.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListFile.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FileViewControlEx
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.listViewFile);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Name = "FileViewControlEx";
            this.Load += new System.EventHandler(this.FileViewControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewFile;
        private System.Windows.Forms.ImageList imageListFile;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
