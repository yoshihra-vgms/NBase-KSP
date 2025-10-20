namespace Hachu.HachuManage
{
    partial class HachuListControl
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
            this.treeListView発注一覧 = new LidorSystems.IntegralUI.Lists.TreeListView();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView発注一覧)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListView発注一覧
            // 
            // 
            // 
            // 
            this.treeListView発注一覧.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView発注一覧.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView発注一覧.ContentPanel.Name = "";
            this.treeListView発注一覧.ContentPanel.Size = new System.Drawing.Size(780, 512);
            this.treeListView発注一覧.ContentPanel.TabIndex = 3;
            this.treeListView発注一覧.ContentPanel.TabStop = false;
            this.treeListView発注一覧.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView発注一覧.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView発注一覧.Location = new System.Drawing.Point(0, 0);
            this.treeListView発注一覧.Name = "treeListView発注一覧";
            this.treeListView発注一覧.Size = new System.Drawing.Size(786, 518);
            this.treeListView発注一覧.TabIndex = 1;
            this.treeListView発注一覧.Text = "treeListView1";
            this.treeListView発注一覧.WatermarkImage = watermarkImage1;
            this.treeListView発注一覧.Click += new System.EventHandler(this.treeListView発注一覧_Click);
            // 
            // HachuListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeListView発注一覧);
            this.Name = "HachuListControl";
            this.Size = new System.Drawing.Size(786, 518);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView発注一覧)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LidorSystems.IntegralUI.Lists.TreeListView treeListView発注一覧;
    }
}
