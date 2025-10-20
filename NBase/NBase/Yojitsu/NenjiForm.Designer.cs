namespace Yojitsu
{
    partial class NenjiForm
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
            LidorSystems.IntegralUI.Style.WatermarkImage watermarkImage1 = new LidorSystems.IntegralUI.Style.WatermarkImage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button船稼働設定 = new System.Windows.Forms.Button();
            this.button共通割掛船員入力 = new System.Windows.Forms.Button();
            this.button特修金入力 = new System.Windows.Forms.Button();
            this.button修繕費予算入力 = new System.Windows.Forms.Button();
            this.button貸船借船料入力 = new System.Windows.Forms.Button();
            this.button運航費入力 = new System.Windows.Forms.Button();
            this.button販管割掛入力 = new System.Windows.Forms.Button();
            this.buttonレート入力 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butt閉じる = new System.Windows.Forms.Button();
            this.button保存 = new System.Windows.Forms.Button();
            this.button月次展開 = new System.Windows.Forms.Button();
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butt入力完了 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.panel2.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1043, 578);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(981, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "単位：千円";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button船稼働設定);
            this.flowLayoutPanel1.Controls.Add(this.button共通割掛船員入力);
            this.flowLayoutPanel1.Controls.Add(this.button特修金入力);
            this.flowLayoutPanel1.Controls.Add(this.button修繕費予算入力);
            this.flowLayoutPanel1.Controls.Add(this.button貸船借船料入力);
            this.flowLayoutPanel1.Controls.Add(this.button運航費入力);
            this.flowLayoutPanel1.Controls.Add(this.button販管割掛入力);
            this.flowLayoutPanel1.Controls.Add(this.buttonレート入力);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 511);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1037, 34);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // button船稼働設定
            // 
            this.button船稼働設定.BackColor = System.Drawing.SystemColors.Control;
            this.button船稼働設定.Enabled = false;
            this.button船稼働設定.Location = new System.Drawing.Point(934, 3);
            this.button船稼働設定.Name = "button船稼働設定";
            this.button船稼働設定.Size = new System.Drawing.Size(100, 23);
            this.button船稼働設定.TabIndex = 1;
            this.button船稼働設定.Text = "船稼働設定";
            this.button船稼働設定.UseVisualStyleBackColor = false;
            this.button船稼働設定.Click += new System.EventHandler(this.butt船稼働設定_Click);
            // 
            // button共通割掛船員入力
            // 
            this.button共通割掛船員入力.BackColor = System.Drawing.SystemColors.Control;
            this.button共通割掛船員入力.Enabled = false;
            this.button共通割掛船員入力.Location = new System.Drawing.Point(803, 3);
            this.button共通割掛船員入力.Name = "button共通割掛船員入力";
            this.button共通割掛船員入力.Size = new System.Drawing.Size(125, 23);
            this.button共通割掛船員入力.TabIndex = 1;
            this.button共通割掛船員入力.Text = "共通割掛船員費入力";
            this.button共通割掛船員入力.UseVisualStyleBackColor = false;
            this.button共通割掛船員入力.Click += new System.EventHandler(this.butt共通割掛船員入力_Click);
            // 
            // button特修金入力
            // 
            this.button特修金入力.BackColor = System.Drawing.SystemColors.Control;
            this.button特修金入力.Enabled = false;
            this.button特修金入力.Location = new System.Drawing.Point(667, 3);
            this.button特修金入力.Name = "button特修金入力";
            this.button特修金入力.Size = new System.Drawing.Size(130, 23);
            this.button特修金入力.TabIndex = 1;
            this.button特修金入力.Text = "特別修繕引当金入力";
            this.button特修金入力.UseVisualStyleBackColor = false;
            this.button特修金入力.Click += new System.EventHandler(this.button特修金入力_Click);
            // 
            // button修繕費予算入力
            // 
            this.button修繕費予算入力.BackColor = System.Drawing.SystemColors.Control;
            this.button修繕費予算入力.Enabled = false;
            this.button修繕費予算入力.Location = new System.Drawing.Point(561, 3);
            this.button修繕費予算入力.Name = "button修繕費予算入力";
            this.button修繕費予算入力.Size = new System.Drawing.Size(100, 23);
            this.button修繕費予算入力.TabIndex = 2;
            this.button修繕費予算入力.Text = "修繕費予算入力";
            this.button修繕費予算入力.UseVisualStyleBackColor = false;
            this.button修繕費予算入力.Click += new System.EventHandler(this.button修繕費予算入力_Click);
            // 
            // button貸船借船料入力
            // 
            this.button貸船借船料入力.BackColor = System.Drawing.SystemColors.Control;
            this.button貸船借船料入力.Enabled = false;
            this.button貸船借船料入力.Location = new System.Drawing.Point(435, 3);
            this.button貸船借船料入力.Name = "button貸船借船料入力";
            this.button貸船借船料入力.Size = new System.Drawing.Size(120, 23);
            this.button貸船借船料入力.TabIndex = 1;
            this.button貸船借船料入力.Text = "貸船・借船料入力";
            this.button貸船借船料入力.UseVisualStyleBackColor = false;
            this.button貸船借船料入力.Click += new System.EventHandler(this.button貸船借船料入力_Click);
            // 
            // button運航費入力
            // 
            this.button運航費入力.BackColor = System.Drawing.SystemColors.Control;
            this.button運航費入力.Enabled = false;
            this.button運航費入力.Location = new System.Drawing.Point(322, 3);
            this.button運航費入力.Name = "button運航費入力";
            this.button運航費入力.Size = new System.Drawing.Size(107, 23);
            this.button運航費入力.TabIndex = 1;
            this.button運航費入力.Text = "運賃・運航費入力";
            this.button運航費入力.UseVisualStyleBackColor = false;
            this.button運航費入力.Click += new System.EventHandler(this.button運航費入力_Click);
            // 
            // button販管割掛入力
            // 
            this.button販管割掛入力.BackColor = System.Drawing.SystemColors.Control;
            this.button販管割掛入力.Enabled = false;
            this.button販管割掛入力.Location = new System.Drawing.Point(216, 3);
            this.button販管割掛入力.Name = "button販管割掛入力";
            this.button販管割掛入力.Size = new System.Drawing.Size(100, 23);
            this.button販管割掛入力.TabIndex = 1;
            this.button販管割掛入力.Text = "販管費割掛入力";
            this.button販管割掛入力.UseVisualStyleBackColor = false;
            this.button販管割掛入力.Click += new System.EventHandler(this.butt販管割掛入力_Click);
            // 
            // buttonレート入力
            // 
            this.buttonレート入力.BackColor = System.Drawing.SystemColors.Control;
            this.buttonレート入力.Enabled = false;
            this.buttonレート入力.Location = new System.Drawing.Point(110, 3);
            this.buttonレート入力.Name = "buttonレート入力";
            this.buttonレート入力.Size = new System.Drawing.Size(100, 23);
            this.buttonレート入力.TabIndex = 2;
            this.buttonレート入力.Text = "換算レート入力";
            this.buttonレート入力.UseVisualStyleBackColor = false;
            this.buttonレート入力.Click += new System.EventHandler(this.buttonレート_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.butt閉じる);
            this.panel1.Controls.Add(this.button保存);
            this.panel1.Controls.Add(this.button月次展開);
            this.panel1.Location = new System.Drawing.Point(676, 551);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(364, 24);
            this.panel1.TabIndex = 2;
            // 
            // butt閉じる
            // 
            this.butt閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.butt閉じる.Location = new System.Drawing.Point(264, 0);
            this.butt閉じる.Name = "butt閉じる";
            this.butt閉じる.Size = new System.Drawing.Size(100, 23);
            this.butt閉じる.TabIndex = 0;
            this.butt閉じる.Text = "閉じる";
            this.butt閉じる.UseVisualStyleBackColor = false;
            this.butt閉じる.Click += new System.EventHandler(this.butt閉じる_Click);
            // 
            // button保存
            // 
            this.button保存.BackColor = System.Drawing.SystemColors.Control;
            this.button保存.Enabled = false;
            this.button保存.Location = new System.Drawing.Point(158, 0);
            this.button保存.Name = "button保存";
            this.button保存.Size = new System.Drawing.Size(100, 23);
            this.button保存.TabIndex = 1;
            this.button保存.Text = "保存";
            this.button保存.UseVisualStyleBackColor = false;
            this.button保存.Click += new System.EventHandler(this.button保存_Click);
            // 
            // button月次展開
            // 
            this.button月次展開.BackColor = System.Drawing.SystemColors.Control;
            this.button月次展開.Location = new System.Drawing.Point(52, 0);
            this.button月次展開.Name = "button月次展開";
            this.button月次展開.Size = new System.Drawing.Size(100, 23);
            this.button月次展開.TabIndex = 1;
            this.button月次展開.Text = "月次展開";
            this.button月次展開.UseVisualStyleBackColor = false;
            this.button月次展開.Click += new System.EventHandler(this.button月次展開_Click);
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
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(1031, 441);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Location = new System.Drawing.Point(3, 58);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(1037, 447);
            this.treeListView1.TabIndex = 2;
            this.treeListView1.Text = "treeListView1";
            this.treeListView1.WatermarkImage = watermarkImage1;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.butt入力完了);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1037, 29);
            this.panel2.TabIndex = 3;
            // 
            // butt入力完了
            // 
            this.butt入力完了.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.butt入力完了.BackColor = System.Drawing.Color.Pink;
            this.butt入力完了.Location = new System.Drawing.Point(962, 6);
            this.butt入力完了.Name = "butt入力完了";
            this.butt入力完了.Size = new System.Drawing.Size(75, 23);
            this.butt入力完了.TabIndex = 1;
            this.butt入力完了.Text = "入力完了";
            this.butt入力完了.UseVisualStyleBackColor = false;
            this.butt入力完了.Click += new System.EventHandler(this.butt入力完了_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // NenjiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1067, 602);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "NenjiForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NenjiForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
        private System.Windows.Forms.Button button月次展開;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button保存;
        private System.Windows.Forms.Button butt入力完了;
        private System.Windows.Forms.Button butt閉じる;
        private System.Windows.Forms.Button button特修金入力;
        private System.Windows.Forms.Button button共通割掛船員入力;
        private System.Windows.Forms.Button button販管割掛入力;
        private System.Windows.Forms.Button button船稼働設定;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button修繕費予算入力;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonレート入力;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button貸船借船料入力;
        private System.Windows.Forms.Button button運航費入力;
    }
}

