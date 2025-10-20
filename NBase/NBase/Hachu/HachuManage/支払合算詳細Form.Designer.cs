namespace Hachu.HachuManage
{
    partial class 支払合算詳細Form
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
            this.treeListView支払合算詳細 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox手配依頼種別 = new System.Windows.Forms.TextBox();
            this.textBox合計金額 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button合算解除 = new System.Windows.Forms.Button();
            this.button支払依頼 = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.button閉じる = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView支払合算詳細)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.treeListView支払合算詳細, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(909, 373);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // treeListView支払合算詳細
            // 
            // 
            // 
            // 
            this.treeListView支払合算詳細.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView支払合算詳細.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView支払合算詳細.ContentPanel.Name = "";
            this.treeListView支払合算詳細.ContentPanel.Size = new System.Drawing.Size(897, 188);
            this.treeListView支払合算詳細.ContentPanel.TabIndex = 3;
            this.treeListView支払合算詳細.ContentPanel.TabStop = false;
            this.treeListView支払合算詳細.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView支払合算詳細.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView支払合算詳細.Footer = false;
            this.treeListView支払合算詳細.Location = new System.Drawing.Point(3, 176);
            this.treeListView支払合算詳細.Name = "treeListView支払合算詳細";
            this.treeListView支払合算詳細.Size = new System.Drawing.Size(903, 194);
            this.treeListView支払合算詳細.TabIndex = 0;
            this.treeListView支払合算詳細.Text = "treeListView1";
            this.treeListView支払合算詳細.WatermarkImage = watermarkImage1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox手配依頼種別);
            this.panel1.Controls.Add(this.textBox合計金額);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox備考);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(903, 127);
            this.panel1.TabIndex = 2;
            // 
            // textBox手配依頼種別
            // 
            this.textBox手配依頼種別.AcceptsTab = true;
            this.textBox手配依頼種別.Location = new System.Drawing.Point(106, 21);
            this.textBox手配依頼種別.Name = "textBox手配依頼種別";
            this.textBox手配依頼種別.ReadOnly = true;
            this.textBox手配依頼種別.Size = new System.Drawing.Size(150, 19);
            this.textBox手配依頼種別.TabIndex = 34;
            this.textBox手配依頼種別.TabStop = false;
            this.textBox手配依頼種別.Text = "種別";
            // 
            // textBox合計金額
            // 
            this.textBox合計金額.Location = new System.Drawing.Point(773, 100);
            this.textBox合計金額.Name = "textBox合計金額";
            this.textBox合計金額.ReadOnly = true;
            this.textBox合計金額.Size = new System.Drawing.Size(115, 19);
            this.textBox合計金額.TabIndex = 33;
            this.textBox合計金額.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(685, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 32;
            this.label3.Text = "合計金額（税抜）";
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox備考.Location = new System.Drawing.Point(106, 50);
            this.textBox備考.MaxLength = 500;
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.ReadOnly = true;
            this.textBox備考.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox備考.Size = new System.Drawing.Size(397, 69);
            this.textBox備考.TabIndex = 31;
            this.textBox備考.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "備考";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(58, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "種別";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.60886F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.39114F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(903, 34);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button合算解除);
            this.flowLayoutPanel1.Controls.Add(this.button支払依頼);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(767, 28);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // button合算解除
            // 
            this.button合算解除.BackColor = System.Drawing.SystemColors.Control;
            this.button合算解除.Location = new System.Drawing.Point(3, 3);
            this.button合算解除.Name = "button合算解除";
            this.button合算解除.Size = new System.Drawing.Size(75, 23);
            this.button合算解除.TabIndex = 2;
            this.button合算解除.Text = "合算解除";
            this.button合算解除.UseVisualStyleBackColor = false;
            this.button合算解除.Click += new System.EventHandler(this.button合算解除_Click);
            // 
            // button支払依頼
            // 
            this.button支払依頼.BackColor = System.Drawing.SystemColors.Control;
            this.button支払依頼.Location = new System.Drawing.Point(84, 3);
            this.button支払依頼.Name = "button支払依頼";
            this.button支払依頼.Size = new System.Drawing.Size(75, 23);
            this.button支払依頼.TabIndex = 1;
            this.button支払依頼.Text = "支払依頼";
            this.button支払依頼.UseVisualStyleBackColor = false;
            this.button支払依頼.Click += new System.EventHandler(this.button支払依頼_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.button閉じる);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(776, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(124, 28);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // button閉じる
            // 
            this.button閉じる.Location = new System.Drawing.Point(46, 3);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 0;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = true;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // 支払合算詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(909, 373);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "支払合算詳細Form";
            this.Text = "支払合算詳細Form";
            this.Load += new System.EventHandler(this.支払合算詳細Form_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView支払合算詳細)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button支払依頼;
        private System.Windows.Forms.Label label8;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView支払合算詳細;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.TextBox textBox合計金額;
        private System.Windows.Forms.TextBox textBox手配依頼種別;
        private System.Windows.Forms.Button button合算解除;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button button閉じる;

    }
}