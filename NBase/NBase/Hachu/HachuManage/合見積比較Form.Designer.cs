namespace Hachu.HachuManage
{
    partial class 合見積比較Form
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
            LidorSystems.IntegralUI.Style.WatermarkImage watermarkImage1 = new LidorSystems.IntegralUI.Style.WatermarkImage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeListView = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button閉じる = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox船 = new System.Windows.Forms.TextBox();
            this.textBox手配内容 = new System.Windows.Forms.TextBox();
            this.label手配内容 = new System.Windows.Forms.Label();
            this.label船 = new System.Windows.Forms.Label();
            this.buttonファイル出力 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.treeListView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.85989F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.14011F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(862, 541);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // treeListView
            // 
            // 
            // 
            // 
            this.treeListView.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView.ContentPanel.Name = "";
            this.treeListView.ContentPanel.Size = new System.Drawing.Size(850, 460);
            this.treeListView.ContentPanel.TabIndex = 3;
            this.treeListView.ContentPanel.TabStop = false;
            this.treeListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView.Footer = false;
            this.treeListView.Location = new System.Drawing.Point(3, 72);
            this.treeListView.Name = "treeListView";
            this.treeListView.Size = new System.Drawing.Size(856, 466);
            this.treeListView.TabIndex = 1;
            this.treeListView.Text = "treeListView1";
            this.treeListView.WatermarkImage = watermarkImage1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.55608F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.44393F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(856, 63);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button閉じる);
            this.flowLayoutPanel1.Controls.Add(this.buttonファイル出力);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(684, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(169, 57);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(91, 3);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 0;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox船);
            this.panel1.Controls.Add(this.textBox手配内容);
            this.panel1.Controls.Add(this.label手配内容);
            this.panel1.Controls.Add(this.label船);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(675, 57);
            this.panel1.TabIndex = 0;
            // 
            // textBox船
            // 
            this.textBox船.Location = new System.Drawing.Point(65, 7);
            this.textBox船.Name = "textBox船";
            this.textBox船.ReadOnly = true;
            this.textBox船.Size = new System.Drawing.Size(150, 19);
            this.textBox船.TabIndex = 1;
            this.textBox船.TabStop = false;
            // 
            // textBox手配内容
            // 
            this.textBox手配内容.Location = new System.Drawing.Point(65, 32);
            this.textBox手配内容.Name = "textBox手配内容";
            this.textBox手配内容.ReadOnly = true;
            this.textBox手配内容.Size = new System.Drawing.Size(500, 19);
            this.textBox手配内容.TabIndex = 3;
            this.textBox手配内容.TabStop = false;
            // 
            // label手配内容
            // 
            this.label手配内容.AutoSize = true;
            this.label手配内容.Location = new System.Drawing.Point(6, 35);
            this.label手配内容.Name = "label手配内容";
            this.label手配内容.Size = new System.Drawing.Size(53, 12);
            this.label手配内容.TabIndex = 4;
            this.label手配内容.Text = "手配内容";
            // 
            // label船
            // 
            this.label船.AutoSize = true;
            this.label船.Location = new System.Drawing.Point(42, 10);
            this.label船.Name = "label船";
            this.label船.Size = new System.Drawing.Size(17, 12);
            this.label船.TabIndex = 2;
            this.label船.Text = "船";
            // 
            // buttonファイル出力
            // 
            this.buttonファイル出力.BackColor = System.Drawing.SystemColors.Control;
            this.buttonファイル出力.Location = new System.Drawing.Point(10, 3);
            this.buttonファイル出力.Name = "buttonファイル出力";
            this.buttonファイル出力.Size = new System.Drawing.Size(75, 23);
            this.buttonファイル出力.TabIndex = 1;
            this.buttonファイル出力.Text = "ファイル出力";
            this.buttonファイル出力.UseVisualStyleBackColor = false;
            this.buttonファイル出力.Click += new System.EventHandler(this.buttonファイル出力_Click);
            // 
            // 合見積比較Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(862, 541);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "合見積比較Form";
            this.Text = "合見積比較Form";
            this.Load += new System.EventHandler(this.合見積比較Form_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button閉じる;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox船;
        private System.Windows.Forms.TextBox textBox手配内容;
        private System.Windows.Forms.Label label手配内容;
        private System.Windows.Forms.Label label船;
        private System.Windows.Forms.Button buttonファイル出力;
    }
}