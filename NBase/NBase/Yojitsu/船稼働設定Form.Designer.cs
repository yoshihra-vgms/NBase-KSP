namespace Yojitsu
{
    partial class 船稼働設定Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LidorSystems.IntegralUI.Style.WatermarkImage watermarkImage2 = new LidorSystems.IntegralUI.Style.WatermarkImage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butt閉じる = new System.Windows.Forms.Button();
            this.button設定 = new System.Windows.Forms.Button();
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelYosanHead = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(816, 547);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.butt閉じる);
            this.panel1.Controls.Add(this.button設定);
            this.panel1.Location = new System.Drawing.Point(449, 520);
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
            // button設定
            // 
            this.button設定.BackColor = System.Drawing.SystemColors.Control;
            this.button設定.Location = new System.Drawing.Point(147, 0);
            this.button設定.Name = "button設定";
            this.button設定.Size = new System.Drawing.Size(100, 23);
            this.button設定.TabIndex = 1;
            this.button設定.Text = "設定";
            this.button設定.UseVisualStyleBackColor = false;
            this.button設定.Click += new System.EventHandler(this.button設定_Click);
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
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(804, 475);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Location = new System.Drawing.Point(3, 33);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(810, 481);
            this.treeListView1.TabIndex = 2;
            this.treeListView1.Text = "treeListView1";
            this.treeListView1.WatermarkImage = watermarkImage2;
            this.treeListView1.DoubleClick += new System.EventHandler(this.treeListView1_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.labelYosanHead);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(810, 24);
            this.panel2.TabIndex = 4;
            // 
            // labelYosanHead
            // 
            this.labelYosanHead.AutoSize = true;
            this.labelYosanHead.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelYosanHead.Location = new System.Drawing.Point(3, 6);
            this.labelYosanHead.Name = "labelYosanHead";
            this.labelYosanHead.Size = new System.Drawing.Size(0, 12);
            this.labelYosanHead.TabIndex = 1;
            // 
            // 船稼働設定Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(840, 571);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "船稼働設定Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button butt閉じる;
        private System.Windows.Forms.Button button設定;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
        private System.Windows.Forms.Label labelYosanHead;
    }
}