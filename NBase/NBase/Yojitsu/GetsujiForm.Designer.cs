namespace Yojitsu
{
    partial class GetsujiForm
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

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            LidorSystems.IntegralUI.Style.WatermarkImage watermarkImage2 = new LidorSystems.IntegralUI.Style.WatermarkImage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox実績表示 = new System.Windows.Forms.CheckBox();
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button保存 = new System.Windows.Forms.Button();
            this.butt振分 = new System.Windows.Forms.Button();
            this.butt年次計画 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button実績取込 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(766, 547);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(704, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "単位：千円";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.checkBox実績表示);
            this.panel2.Location = new System.Drawing.Point(3, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(760, 29);
            this.panel2.TabIndex = 5;
            // 
            // checkBox実績表示
            // 
            this.checkBox実績表示.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.checkBox実績表示.AutoSize = true;
            this.checkBox実績表示.Location = new System.Drawing.Point(685, 10);
            this.checkBox実績表示.Name = "checkBox実績表示";
            this.checkBox実績表示.Size = new System.Drawing.Size(72, 16);
            this.checkBox実績表示.TabIndex = 0;
            this.checkBox実績表示.Text = "実績表示";
            this.checkBox実績表示.UseVisualStyleBackColor = true;
            this.checkBox実績表示.CheckedChanged += new System.EventHandler(this.checkBox実績表示_CheckedChanged);
            // 
            // treeListView1
            // 
            this.treeListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.treeListView1.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView1.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView1.ContentPanel.Name = "";
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(754, 450);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Location = new System.Drawing.Point(3, 58);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(760, 456);
            this.treeListView1.TabIndex = 3;
            this.treeListView1.Text = "treeListView1";
            this.treeListView1.WatermarkImage = watermarkImage2;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.button実績取込);
            this.panel3.Controls.Add(this.button保存);
            this.panel3.Controls.Add(this.butt振分);
            this.panel3.Controls.Add(this.butt年次計画);
            this.panel3.Location = new System.Drawing.Point(321, 520);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(442, 24);
            this.panel3.TabIndex = 2;
            // 
            // button保存
            // 
            this.button保存.BackColor = System.Drawing.SystemColors.Control;
            this.button保存.Enabled = false;
            this.button保存.Location = new System.Drawing.Point(24, 1);
            this.button保存.Name = "button保存";
            this.button保存.Size = new System.Drawing.Size(100, 23);
            this.button保存.TabIndex = 2;
            this.button保存.Text = "保存";
            this.button保存.UseVisualStyleBackColor = false;
            this.button保存.Click += new System.EventHandler(this.button保存_Click);
            // 
            // butt振分
            // 
            this.butt振分.BackColor = System.Drawing.SystemColors.Control;
            this.butt振分.Location = new System.Drawing.Point(236, 1);
            this.butt振分.Name = "butt振分";
            this.butt振分.Size = new System.Drawing.Size(100, 23);
            this.butt振分.TabIndex = 1;
            this.butt振分.Text = "振分";
            this.butt振分.UseVisualStyleBackColor = false;
            this.butt振分.Click += new System.EventHandler(this.butt振分_Click);
            // 
            // butt年次計画
            // 
            this.butt年次計画.BackColor = System.Drawing.SystemColors.Control;
            this.butt年次計画.Location = new System.Drawing.Point(342, 1);
            this.butt年次計画.Name = "butt年次計画";
            this.butt年次計画.Size = new System.Drawing.Size(100, 23);
            this.butt年次計画.TabIndex = 0;
            this.butt年次計画.Text = "年次計画";
            this.butt年次計画.UseVisualStyleBackColor = false;
            this.butt年次計画.Click += new System.EventHandler(this.butt年次計画_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button実績取込
            // 
            this.button実績取込.BackColor = System.Drawing.SystemColors.Control;
            this.button実績取込.Enabled = false;
            this.button実績取込.Location = new System.Drawing.Point(130, 0);
            this.button実績取込.Name = "button実績取込";
            this.button実績取込.Size = new System.Drawing.Size(100, 23);
            this.button実績取込.TabIndex = 2;
            this.button実績取込.Text = "実績取込";
            this.button実績取込.UseVisualStyleBackColor = false;
            this.button実績取込.Click += new System.EventHandler(this.button実績取込_Click);
            // 
            // GetsujiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(790, 571);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GetsujiForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GetsujiForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button butt年次計画;
        private System.Windows.Forms.Button butt振分;
        private System.Windows.Forms.CheckBox checkBox実績表示;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button保存;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button実績取込;
    }
}

