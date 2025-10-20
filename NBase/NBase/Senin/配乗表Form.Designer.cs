namespace Senin
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker日付 = new System.Windows.Forms.DateTimePicker();
            this.button配乗表出力 = new System.Windows.Forms.Button();
            this.button検索 = new System.Windows.Forms.Button();
            this.checkedListBox船 = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label前回配信日 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button配信 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePicker日付);
            this.groupBox1.Controls.Add(this.button配乗表出力);
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Controls.Add(this.checkedListBox船);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(577, 93);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // dateTimePicker日付
            // 
            this.dateTimePicker日付.Location = new System.Drawing.Point(330, 19);
            this.dateTimePicker日付.Name = "dateTimePicker日付";
            this.dateTimePicker日付.Size = new System.Drawing.Size(139, 19);
            this.dateTimePicker日付.TabIndex = 3;
            this.dateTimePicker日付.Value = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker日付.ValueChanged += new System.EventHandler(this.dateTimePicker日付_ValueChanged);
            // 
            // button配乗表出力
            // 
            this.button配乗表出力.BackColor = System.Drawing.SystemColors.Control;
            this.button配乗表出力.Location = new System.Drawing.Point(481, 60);
            this.button配乗表出力.Name = "button配乗表出力";
            this.button配乗表出力.Size = new System.Drawing.Size(85, 23);
            this.button配乗表出力.TabIndex = 3;
            this.button配乗表出力.Text = "配乗表出力";
            this.button配乗表出力.UseVisualStyleBackColor = false;
            this.button配乗表出力.Click += new System.EventHandler(this.button配乗表出力_Click);
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(481, 18);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(85, 23);
            this.button検索.TabIndex = 2;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // checkedListBox船
            // 
            this.checkedListBox船.CheckOnClick = true;
            this.checkedListBox船.FormattingEnabled = true;
            this.checkedListBox船.Location = new System.Drawing.Point(48, 18);
            this.checkedListBox船.Name = "checkedListBox船";
            this.checkedListBox船.Size = new System.Drawing.Size(214, 60);
            this.checkedListBox船.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "船";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1076, 672);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label前回配信日);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.button配信);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1070, 104);
            this.panel2.TabIndex = 4;
            // 
            // label前回配信日
            // 
            this.label前回配信日.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label前回配信日.AutoSize = true;
            this.label前回配信日.Location = new System.Drawing.Point(890, 67);
            this.label前回配信日.Name = "label前回配信日";
            this.label前回配信日.Size = new System.Drawing.Size(71, 12);
            this.label前回配信日.TabIndex = 5;
            this.label前回配信日.Text = "yyyy/MM/dd";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(819, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "前回配信日：";
            // 
            // button配信
            // 
            this.button配信.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button配信.BackColor = System.Drawing.SystemColors.Control;
            this.button配信.Location = new System.Drawing.Point(976, 62);
            this.button配信.Name = "button配信";
            this.button配信.Size = new System.Drawing.Size(85, 23);
            this.button配信.TabIndex = 4;
            this.button配信.Text = "船に配信";
            this.button配信.UseVisualStyleBackColor = false;
            this.button配信.Click += new System.EventHandler(this.button配信_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "配乗表.xlsx";
            this.saveFileDialog1.Filter = "Excel ファイル|*.xlsx";
            // 
            // treeListView1
            // 
            // 
            // 
            // 
            this.treeListView1.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView1.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView1.ContentPanel.Name = "";
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(1064, 550);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView1.Location = new System.Drawing.Point(3, 113);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(1070, 556);
            this.treeListView1.TabIndex = 5;
            this.treeListView1.Text = "treeListView1";
            // 
            // 配乗表Form
            // 
            this.AcceptButton = this.button検索;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1076, 672);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "配乗表Form";
            this.Text = "配乗表";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.配乗表Form_FormClosed);
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
        private System.Windows.Forms.Button button配信;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button配乗表出力;
        private System.Windows.Forms.Label label前回配信日;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DateTimePicker dateTimePicker日付;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
    }
}