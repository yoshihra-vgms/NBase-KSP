namespace Yojitsu
{
    partial class 入渠Form
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
            this.components = new System.ComponentModel.Container();
            LidorSystems.IntegralUI.Style.WatermarkImage watermarkImage1 = ((LidorSystems.IntegralUI.Style.WatermarkImage)(new LidorSystems.IntegralUI.Style.WatermarkImage()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label検査指定日 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butt閉じる = new System.Windows.Forms.Button();
            this.buttonファイル出力 = new System.Windows.Forms.Button();
            this.button検査設定 = new System.Windows.Forms.Button();
            this.button設定 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(866, 647);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label検査指定日);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 29);
            this.panel1.TabIndex = 0;
            // 
            // label検査指定日
            // 
            this.label検査指定日.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label検査指定日.AutoSize = true;
            this.label検査指定日.Location = new System.Drawing.Point(824, 14);
            this.label検査指定日.Name = "label検査指定日";
            this.label検査指定日.Size = new System.Drawing.Size(35, 12);
            this.label検査指定日.TabIndex = 6;
            this.label検査指定日.Text = "--/--";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(747, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "検査基準日：";
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
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(854, 565);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Location = new System.Drawing.Point(3, 38);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(860, 571);
            this.treeListView1.TabIndex = 10;
            this.treeListView1.Text = "treeListView1";
            this.treeListView1.WatermarkImage = watermarkImage1;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(3, 615);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(860, 29);
            this.panel3.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.butt閉じる);
            this.panel2.Controls.Add(this.buttonファイル出力);
            this.panel2.Controls.Add(this.button検査設定);
            this.panel2.Controls.Add(this.button設定);
            this.panel2.Location = new System.Drawing.Point(403, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(440, 24);
            this.panel2.TabIndex = 9;
            // 
            // butt閉じる
            // 
            this.butt閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.butt閉じる.Location = new System.Drawing.Point(337, 0);
            this.butt閉じる.Name = "butt閉じる";
            this.butt閉じる.Size = new System.Drawing.Size(100, 23);
            this.butt閉じる.TabIndex = 0;
            this.butt閉じる.Text = "閉じる";
            this.butt閉じる.UseVisualStyleBackColor = false;
            this.butt閉じる.Click += new System.EventHandler(this.butt閉じる_Click);
            // 
            // buttonファイル出力
            // 
            this.buttonファイル出力.BackColor = System.Drawing.SystemColors.Control;
            this.buttonファイル出力.Location = new System.Drawing.Point(125, 0);
            this.buttonファイル出力.Name = "buttonファイル出力";
            this.buttonファイル出力.Size = new System.Drawing.Size(100, 23);
            this.buttonファイル出力.TabIndex = 1;
            this.buttonファイル出力.Text = "ファイル出力";
            this.buttonファイル出力.UseVisualStyleBackColor = false;
            this.buttonファイル出力.Click += new System.EventHandler(this.buttonファイル出力_Click);
            // 
            // button検査設定
            // 
            this.button検査設定.BackColor = System.Drawing.SystemColors.Control;
            this.button検査設定.Location = new System.Drawing.Point(19, 0);
            this.button検査設定.Name = "button検査設定";
            this.button検査設定.Size = new System.Drawing.Size(100, 23);
            this.button検査設定.TabIndex = 1;
            this.button検査設定.Text = "検査設定";
            this.button検査設定.UseVisualStyleBackColor = false;
            this.button検査設定.Click += new System.EventHandler(this.button検査設定_Click);
            // 
            // button設定
            // 
            this.button設定.BackColor = System.Drawing.SystemColors.Control;
            this.button設定.Location = new System.Drawing.Point(231, 0);
            this.button設定.Name = "button設定";
            this.button設定.Size = new System.Drawing.Size(100, 23);
            this.button設定.TabIndex = 1;
            this.button設定.Text = "設定";
            this.button設定.UseVisualStyleBackColor = false;
            this.button設定.Click += new System.EventHandler(this.button設定_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.Controls.Add(this.textBox備考);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(394, 24);
            this.panel4.TabIndex = 0;
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox備考.Location = new System.Drawing.Point(38, 2);
            this.textBox備考.MaxLength = 500;
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.Size = new System.Drawing.Size(353, 19);
            this.textBox備考.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "備考";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "修繕費予算.xls";
            this.saveFileDialog1.Filter = "Excel ファイル|*.xls";
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // 入渠Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(890, 671);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "入渠Form";
            this.Text = "入渠Form";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button butt閉じる;
        private System.Windows.Forms.Button button設定;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button検査設定;
        private System.Windows.Forms.Button buttonファイル出力;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label検査指定日;
    }
}