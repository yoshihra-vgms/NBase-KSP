namespace NBaseHonsen.Senin
{
    partial class 船内収支Form
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
            LidorSystems.IntegralUI.Style.WatermarkImage watermarkImage2 = new LidorSystems.IntegralUI.Style.WatermarkImage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button検索 = new System.Windows.Forms.Button();
            this.button船内収支金報告書 = new System.Windows.Forms.Button();
            this.button科目別集計表 = new System.Windows.Forms.Button();
            this.button明細追加 = new System.Windows.Forms.Button();
            this.textBox受入金額 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox繰越金額 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox支払金額 = new System.Windows.Forms.TextBox();
            this.comboBox月 = new System.Windows.Forms.ComboBox();
            this.comboBox年 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeListView送金 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.button送金受入 = new System.Windows.Forms.Button();
            this.treeListView船内準備金 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView送金)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView船内準備金)).BeginInit();
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
            this.tableLayoutPanel1.Controls.Add(this.treeListView船内準備金, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(18, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(983, 870);
            this.tableLayoutPanel1.TabIndex = 31;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button検索);
            this.panel1.Controls.Add(this.button船内収支金報告書);
            this.panel1.Controls.Add(this.button科目別集計表);
            this.panel1.Controls.Add(this.button明細追加);
            this.panel1.Controls.Add(this.textBox受入金額);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox繰越金額);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox支払金額);
            this.panel1.Controls.Add(this.comboBox月);
            this.panel1.Controls.Add(this.comboBox年);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(975, 222);
            this.panel1.TabIndex = 0;
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(267, 9);
            this.button検索.Margin = new System.Windows.Forms.Padding(4);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(196, 38);
            this.button検索.TabIndex = 67;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // button船内収支金報告書
            // 
            this.button船内収支金報告書.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button船内収支金報告書.BackColor = System.Drawing.SystemColors.Control;
            this.button船内収支金報告書.Location = new System.Drawing.Point(267, 55);
            this.button船内収支金報告書.Margin = new System.Windows.Forms.Padding(4);
            this.button船内収支金報告書.Name = "button船内収支金報告書";
            this.button船内収支金報告書.Size = new System.Drawing.Size(196, 38);
            this.button船内収支金報告書.TabIndex = 67;
            this.button船内収支金報告書.Text = "船内収支報告書出力";
            this.button船内収支金報告書.UseVisualStyleBackColor = false;
            this.button船内収支金報告書.Click += new System.EventHandler(this.button船内収支金報告書_Click);
            // 
            // button科目別集計表
            // 
            this.button科目別集計表.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button科目別集計表.BackColor = System.Drawing.SystemColors.Control;
            this.button科目別集計表.Location = new System.Drawing.Point(267, 101);
            this.button科目別集計表.Margin = new System.Windows.Forms.Padding(4);
            this.button科目別集計表.Name = "button科目別集計表";
            this.button科目別集計表.Size = new System.Drawing.Size(196, 38);
            this.button科目別集計表.TabIndex = 68;
            this.button科目別集計表.Text = "科目別集計表出力";
            this.button科目別集計表.UseVisualStyleBackColor = false;
            this.button科目別集計表.Click += new System.EventHandler(this.button科目別集計表_Click);
            // 
            // button明細追加
            // 
            this.button明細追加.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button明細追加.BackColor = System.Drawing.SystemColors.Control;
            this.button明細追加.Location = new System.Drawing.Point(267, 147);
            this.button明細追加.Margin = new System.Windows.Forms.Padding(4);
            this.button明細追加.Name = "button明細追加";
            this.button明細追加.Size = new System.Drawing.Size(196, 38);
            this.button明細追加.TabIndex = 69;
            this.button明細追加.Text = "明細追加";
            this.button明細追加.UseVisualStyleBackColor = false;
            this.button明細追加.Click += new System.EventHandler(this.button明細追加_Click);
            // 
            // textBox受入金額
            // 
            this.textBox受入金額.Location = new System.Drawing.Point(129, 195);
            this.textBox受入金額.Margin = new System.Windows.Forms.Padding(4);
            this.textBox受入金額.Name = "textBox受入金額";
            this.textBox受入金額.ReadOnly = true;
            this.textBox受入金額.Size = new System.Drawing.Size(100, 23);
            this.textBox受入金額.TabIndex = 64;
            this.textBox受入金額.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 198);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 16);
            this.label5.TabIndex = 63;
            this.label5.Text = "受入金額合計";
            // 
            // textBox繰越金額
            // 
            this.textBox繰越金額.Location = new System.Drawing.Point(651, 195);
            this.textBox繰越金額.Margin = new System.Windows.Forms.Padding(4);
            this.textBox繰越金額.Name = "textBox繰越金額";
            this.textBox繰越金額.ReadOnly = true;
            this.textBox繰越金額.Size = new System.Drawing.Size(100, 23);
            this.textBox繰越金額.TabIndex = 66;
            this.textBox繰越金額.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(507, 198);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 16);
            this.label4.TabIndex = 62;
            this.label4.Text = "差引残額翌月繰越";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(260, 198);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 61;
            this.label3.Text = "支払金額合計";
            // 
            // textBox支払金額
            // 
            this.textBox支払金額.Location = new System.Drawing.Point(372, 195);
            this.textBox支払金額.Margin = new System.Windows.Forms.Padding(4);
            this.textBox支払金額.Name = "textBox支払金額";
            this.textBox支払金額.ReadOnly = true;
            this.textBox支払金額.Size = new System.Drawing.Size(100, 23);
            this.textBox支払金額.TabIndex = 65;
            this.textBox支払金額.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // comboBox月
            // 
            this.comboBox月.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox月.FormattingEnabled = true;
            this.comboBox月.Location = new System.Drawing.Point(184, 17);
            this.comboBox月.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox月.Name = "comboBox月";
            this.comboBox月.Size = new System.Drawing.Size(55, 24);
            this.comboBox月.TabIndex = 32;
            // 
            // comboBox年
            // 
            this.comboBox年.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年.FormattingEnabled = true;
            this.comboBox年.Location = new System.Drawing.Point(76, 17);
            this.comboBox年.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox年.Name = "comboBox年";
            this.comboBox年.Size = new System.Drawing.Size(90, 24);
            this.comboBox年.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 21);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 33;
            this.label6.Text = "年月";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(168, 21);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 16);
            this.label8.TabIndex = 34;
            this.label8.Text = "/";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.treeListView送金);
            this.groupBox1.Controls.Add(this.button送金受入);
            this.groupBox1.Location = new System.Drawing.Point(471, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(502, 172);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "送金";
            // 
            // treeListView送金
            // 
            // 
            // 
            // 
            this.treeListView送金.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView送金.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView送金.ContentPanel.Margin = new System.Windows.Forms.Padding(4);
            this.treeListView送金.ContentPanel.Name = "";
            this.treeListView送金.ContentPanel.Size = new System.Drawing.Size(479, 88);
            this.treeListView送金.ContentPanel.TabIndex = 3;
            this.treeListView送金.ContentPanel.TabStop = false;
            this.treeListView送金.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView送金.Location = new System.Drawing.Point(9, 24);
            this.treeListView送金.Margin = new System.Windows.Forms.Padding(4);
            this.treeListView送金.Name = "treeListView送金";
            this.treeListView送金.Size = new System.Drawing.Size(485, 94);
            this.treeListView送金.TabIndex = 30;
            this.treeListView送金.Text = "treeListView2";
            this.treeListView送金.WatermarkImage = watermarkImage1;
            // 
            // button送金受入
            // 
            this.button送金受入.BackColor = System.Drawing.SystemColors.Control;
            this.button送金受入.Location = new System.Drawing.Point(343, 126);
            this.button送金受入.Margin = new System.Windows.Forms.Padding(4);
            this.button送金受入.Name = "button送金受入";
            this.button送金受入.Size = new System.Drawing.Size(151, 38);
            this.button送金受入.TabIndex = 29;
            this.button送金受入.Text = "送金受入";
            this.button送金受入.UseVisualStyleBackColor = false;
            this.button送金受入.Click += new System.EventHandler(this.button送金受入_Click);
            // 
            // treeListView船内準備金
            // 
            this.treeListView船内準備金.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.treeListView船内準備金.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView船内準備金.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView船内準備金.ContentPanel.Margin = new System.Windows.Forms.Padding(4);
            this.treeListView船内準備金.ContentPanel.Name = "";
            this.treeListView船内準備金.ContentPanel.Size = new System.Drawing.Size(969, 626);
            this.treeListView船内準備金.ContentPanel.TabIndex = 3;
            this.treeListView船内準備金.ContentPanel.TabStop = false;
            this.treeListView船内準備金.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView船内準備金.Location = new System.Drawing.Point(4, 234);
            this.treeListView船内準備金.Margin = new System.Windows.Forms.Padding(4);
            this.treeListView船内準備金.Name = "treeListView船内準備金";
            this.treeListView船内準備金.Size = new System.Drawing.Size(975, 632);
            this.treeListView船内準備金.TabIndex = 1;
            this.treeListView船内準備金.Text = "treeListView1";
            this.treeListView船内準備金.WatermarkImage = watermarkImage2;
            this.treeListView船内準備金.Click += new System.EventHandler(this.treeListView船内準備金_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "船内収支報告書.xlsx";
            this.saveFileDialog1.Filter = "Excel ファイル|*.xlsx";
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.FileName = "科目別集計表.xlsx";
            this.saveFileDialog2.Filter = "Excel ファイル|*.xlsx";
            this.saveFileDialog2.RestoreDirectory = true;
            // 
            // 船内収支Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1014, 902);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "船内収支Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "船用金管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView送金)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView船内準備金)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox受入金額;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox繰越金額;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox支払金額;
        private System.Windows.Forms.ComboBox comboBox月;
        private System.Windows.Forms.ComboBox comboBox年;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button送金受入;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView船内準備金;
        private System.Windows.Forms.Button button船内収支金報告書;
        private System.Windows.Forms.Button button科目別集計表;
        private System.Windows.Forms.Button button明細追加;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView送金;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.Button button検索;
    }
}