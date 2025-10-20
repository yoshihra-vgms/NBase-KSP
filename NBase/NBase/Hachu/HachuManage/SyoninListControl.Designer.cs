namespace Hachu.HachuManage
{
    partial class SyoninListControl
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
            LidorSystems.IntegralUI.Style.WatermarkImage watermarkImage1 = new LidorSystems.IntegralUI.Style.WatermarkImage();
            this.treeListView承認一覧 = new LidorSystems.IntegralUI.Lists.TreeListView();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView承認一覧)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListView承認一覧
            // 
            // 
            // 
            // 
            this.treeListView承認一覧.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView承認一覧.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView承認一覧.ContentPanel.Name = "";
            this.treeListView承認一覧.ContentPanel.Size = new System.Drawing.Size(144, 144);
            this.treeListView承認一覧.ContentPanel.TabIndex = 3;
            this.treeListView承認一覧.ContentPanel.TabStop = false;
            this.treeListView承認一覧.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView承認一覧.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView承認一覧.Location = new System.Drawing.Point(0, 0);
            this.treeListView承認一覧.Name = "treeListView承認一覧";
            this.treeListView承認一覧.Size = new System.Drawing.Size(150, 150);
            this.treeListView承認一覧.TabIndex = 1;
            this.treeListView承認一覧.Text = "treeListView1";
            this.treeListView承認一覧.WatermarkImage = watermarkImage1;
            this.treeListView承認一覧.Click += new System.EventHandler(this.treeListView承認一覧_Click);
            // 
            // SyoninListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeListView承認一覧);
            this.Name = "SyoninListControl";
            ((System.ComponentModel.ISupportInitialize)(this.treeListView承認一覧)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LidorSystems.IntegralUI.Lists.TreeListView treeListView承認一覧;
    }
}
