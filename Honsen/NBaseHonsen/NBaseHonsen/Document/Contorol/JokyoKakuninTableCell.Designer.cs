namespace NBaseHonsen.Document.Contorol
{
    partial class JokyoKakuninTableCell
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel_Date = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel_Name = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel_Date, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel_Name, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(240, 70);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel_Date
            // 
            this.flowLayoutPanel_Date.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel_Date.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_Date.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_Date.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel_Date.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel_Date.Name = "flowLayoutPanel_Date";
            this.flowLayoutPanel_Date.Size = new System.Drawing.Size(105, 70);
            this.flowLayoutPanel_Date.TabIndex = 0;
            // 
            // flowLayoutPanel_Name
            // 
            this.flowLayoutPanel_Name.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_Name.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_Name.Location = new System.Drawing.Point(105, 0);
            this.flowLayoutPanel_Name.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel_Name.Name = "flowLayoutPanel_Name";
            this.flowLayoutPanel_Name.Size = new System.Drawing.Size(135, 70);
            this.flowLayoutPanel_Name.TabIndex = 1;
            // 
            // JokyoKakuninTableCell
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "JokyoKakuninTableCell";
            this.Size = new System.Drawing.Size(240, 70);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Date;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Name;
    }
}
