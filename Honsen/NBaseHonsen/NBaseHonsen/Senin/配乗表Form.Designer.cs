namespace NBaseHonsen.Senin
{
    partial class 配乗表Form
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button検索 = new System.Windows.Forms.Button();
            this.checkedListBox船 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button配乗表 = new System.Windows.Forms.Button();
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label前回配信日 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Controls.Add(this.checkedListBox船);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(18, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(587, 124);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button検索.Location = new System.Drawing.Point(431, 62);
            this.button検索.Margin = new System.Windows.Forms.Padding(4);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(138, 38);
            this.button検索.TabIndex = 2;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // checkedListBox船
            // 
            this.checkedListBox船.CheckOnClick = true;
            this.checkedListBox船.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkedListBox船.FormattingEnabled = true;
            this.checkedListBox船.Location = new System.Drawing.Point(56, 24);
            this.checkedListBox船.Margin = new System.Windows.Forms.Padding(4);
            this.checkedListBox船.Name = "checkedListBox船";
            this.checkedListBox船.Size = new System.Drawing.Size(367, 76);
            this.checkedListBox船.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "船";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 147F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(988, 645);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label前回配信日);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.button配乗表);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(980, 139);
            this.panel2.TabIndex = 4;
            // 
            // button配乗表
            // 
            this.button配乗表.BackColor = System.Drawing.SystemColors.Control;
            this.button配乗表.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button配乗表.Location = new System.Drawing.Point(613, 71);
            this.button配乗表.Margin = new System.Windows.Forms.Padding(4);
            this.button配乗表.Name = "button配乗表";
            this.button配乗表.Size = new System.Drawing.Size(162, 38);
            this.button配乗表.TabIndex = 3;
            this.button配乗表.Text = "配乗表出力";
            this.button配乗表.UseVisualStyleBackColor = false;
            this.button配乗表.Click += new System.EventHandler(this.button配乗表_Click);
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
            this.treeListView1.ContentPanel.Margin = new System.Windows.Forms.Padding(4);
            this.treeListView1.ContentPanel.Name = "";
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(974, 484);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Location = new System.Drawing.Point(4, 151);
            this.treeListView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(980, 490);
            this.treeListView1.TabIndex = 5;
            this.treeListView1.Text = "treeListView1";
            this.treeListView1.WatermarkImage = watermarkImage2;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "配乗表";
            this.saveFileDialog1.Filter = "Excel ファイル|*.xlsx";
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // label前回配信日
            // 
            this.label前回配信日.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label前回配信日.AutoSize = true;
            this.label前回配信日.Location = new System.Drawing.Point(877, 82);
            this.label前回配信日.Name = "label前回配信日";
            this.label前回配信日.Size = new System.Drawing.Size(96, 16);
            this.label前回配信日.TabIndex = 7;
            this.label前回配信日.Text = "yyyy/MM/dd";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(807, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "配信日：";
            // 
            // 配乗表Form
            // 
            this.AcceptButton = this.button検索;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1014, 671);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "配乗表Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "配乗表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox checkedListBox船;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button配乗表;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label前回配信日;
        private System.Windows.Forms.Label label3;

    }
}