namespace NBaseHonsen
{
    partial class 手配依頼Form
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
            LidorSystems.IntegralUI.Lists.Style.ListColumnFormatStyle listColumnFormatStyle2 = new LidorSystems.IntegralUI.Lists.Style.ListColumnFormatStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox手配内容 = new System.Windows.Forms.TextBox();
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.button品目追加 = new System.Windows.Forms.Button();
            this.button手配依頼 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button保存 = new System.Windows.Forms.Button();
            this.button読込 = new System.Windows.Forms.Button();
            this.dateTimePicker希望納期 = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox希望港 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button削除 = new System.Windows.Forms.Button();
            this.button出力 = new System.Windows.Forms.Button();
            this.openFileDialogドックオーダー読込 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialogドックオーダー出力 = new System.Windows.Forms.SaveFileDialog();
            this.Button請求書出力 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.textBox備考 = new NBaseUtil.MultiLineCombo();
            this.buttonリスト入力 = new System.Windows.Forms.Button();
            this.button査定表出力 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "※手配内容";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "備考";
            // 
            // textBox手配内容
            // 
            this.textBox手配内容.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox手配内容.Location = new System.Drawing.Point(127, 19);
            this.textBox手配内容.MaxLength = 50;
            this.textBox手配内容.Name = "textBox手配内容";
            this.textBox手配内容.ReadOnly = true;
            this.textBox手配内容.Size = new System.Drawing.Size(673, 26);
            this.textBox手配内容.TabIndex = 1;
            this.textBox手配内容.Enter += new System.EventHandler(this.textBox手配内容_Enter);
            this.textBox手配内容.Leave += new System.EventHandler(this.textBox手配内容_Leave);
            // 
            // treeListView1
            // 
            this.treeListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            listColumnFormatStyle2.DataFormatInfo = new System.Globalization.CultureInfo("ja-JP");
            listColumnFormatStyle2.FooterPadding = new System.Windows.Forms.Padding(5);
            listColumnFormatStyle2.HeaderFont = new System.Drawing.Font("ＭＳ Ｐゴシック", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            listColumnFormatStyle2.HeaderPadding = new System.Windows.Forms.Padding(5);
            this.treeListView1.ColumnFormatStyle = listColumnFormatStyle2;
            // 
            // 
            // 
            this.treeListView1.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView1.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView1.ContentPanel.Name = "";
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(1077, 211);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Location = new System.Drawing.Point(3, 3);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(1083, 217);
            this.treeListView1.TabIndex = 2;
            this.treeListView1.Text = "treeListView1";
            this.treeListView1.Click += new System.EventHandler(this.treeListView1_Click);
            // 
            // button品目追加
            // 
            this.button品目追加.BackColor = System.Drawing.SystemColors.Control;
            this.button品目追加.Enabled = false;
            this.button品目追加.Location = new System.Drawing.Point(16, 226);
            this.button品目追加.Name = "button品目追加";
            this.button品目追加.Size = new System.Drawing.Size(149, 38);
            this.button品目追加.TabIndex = 3;
            this.button品目追加.Text = "仕様・型式追加";
            this.button品目追加.UseVisualStyleBackColor = false;
            this.button品目追加.Click += new System.EventHandler(this.addButt_Click);
            // 
            // button手配依頼
            // 
            this.button手配依頼.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button手配依頼.BackColor = System.Drawing.SystemColors.Control;
            this.button手配依頼.Enabled = false;
            this.button手配依頼.Location = new System.Drawing.Point(405, 523);
            this.button手配依頼.Name = "button手配依頼";
            this.button手配依頼.Size = new System.Drawing.Size(149, 38);
            this.button手配依頼.TabIndex = 7;
            this.button手配依頼.Text = "手配依頼";
            this.button手配依頼.UseVisualStyleBackColor = false;
            this.button手配依頼.Click += new System.EventHandler(this.button手配依頼_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(872, 523);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(149, 38);
            this.button閉じる.TabIndex = 10;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 282);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1089, 223);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // button保存
            // 
            this.button保存.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button保存.BackColor = System.Drawing.SystemColors.Control;
            this.button保存.Enabled = false;
            this.button保存.Location = new System.Drawing.Point(560, 523);
            this.button保存.Name = "button保存";
            this.button保存.Size = new System.Drawing.Size(149, 38);
            this.button保存.TabIndex = 8;
            this.button保存.Text = "保存";
            this.button保存.UseVisualStyleBackColor = false;
            this.button保存.Click += new System.EventHandler(this.button保存_Click);
            // 
            // button読込
            // 
            this.button読込.BackColor = System.Drawing.SystemColors.Control;
            this.button読込.Enabled = false;
            this.button読込.Location = new System.Drawing.Point(171, 226);
            this.button読込.Name = "button読込";
            this.button読込.Size = new System.Drawing.Size(153, 38);
            this.button読込.TabIndex = 4;
            this.button読込.Text = "ﾄﾞｯｸｵｰﾀﾞｰ読込";
            this.button読込.UseVisualStyleBackColor = false;
            this.button読込.Visible = false;
            this.button読込.Click += new System.EventHandler(this.button読込_Click);
            // 
            // dateTimePicker希望納期
            // 
            this.dateTimePicker希望納期.Enabled = false;
            this.dateTimePicker希望納期.Location = new System.Drawing.Point(98, 3);
            this.dateTimePicker希望納期.Name = "dateTimePicker希望納期";
            this.dateTimePicker希望納期.Size = new System.Drawing.Size(200, 26);
            this.dateTimePicker希望納期.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox希望港);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dateTimePicker希望納期);
            this.panel1.Location = new System.Drawing.Point(343, 207);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(434, 69);
            this.panel1.TabIndex = 5;
            this.panel1.Visible = false;
            // 
            // textBox希望港
            // 
            this.textBox希望港.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox希望港.Location = new System.Drawing.Point(98, 40);
            this.textBox希望港.MaxLength = 20;
            this.textBox希望港.Name = "textBox希望港";
            this.textBox希望港.ReadOnly = true;
            this.textBox希望港.Size = new System.Drawing.Size(298, 26);
            this.textBox希望港.TabIndex = 2;
            this.textBox希望港.Enter += new System.EventHandler(this.textBox希望港_Enter);
            this.textBox希望港.Leave += new System.EventHandler(this.textBox希望港_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "希望港";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "希望納期";
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Enabled = false;
            this.button削除.Location = new System.Drawing.Point(715, 523);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(149, 38);
            this.button削除.TabIndex = 9;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button出力
            // 
            this.button出力.BackColor = System.Drawing.SystemColors.Control;
            this.button出力.Enabled = false;
            this.button出力.Location = new System.Drawing.Point(330, 226);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(153, 38);
            this.button出力.TabIndex = 4;
            this.button出力.Text = "ﾄﾞｯｸｵｰﾀﾞｰ出力";
            this.button出力.UseVisualStyleBackColor = false;
            this.button出力.Visible = false;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // openFileDialogドックオーダー読込
            // 
            this.openFileDialogドックオーダー読込.Filter = "Excel ファイル|*.xlsx";
            this.openFileDialogドックオーダー読込.RestoreDirectory = true;
            this.openFileDialogドックオーダー読込.Title = "ドックオーダー読込";
            // 
            // saveFileDialogドックオーダー出力
            // 
            this.saveFileDialogドックオーダー出力.FileName = "ドックオーダー.xlsx";
            this.saveFileDialogドックオーダー出力.Filter = "Excel ファイル|*.xlsx";
            this.saveFileDialogドックオーダー出力.RestoreDirectory = true;
            this.saveFileDialogドックオーダー出力.Title = "ドックオーダー出力";
            // 
            // Button請求書出力
            // 
            this.Button請求書出力.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Button請求書出力.BackColor = System.Drawing.SystemColors.Control;
            this.Button請求書出力.Location = new System.Drawing.Point(250, 523);
            this.Button請求書出力.Name = "Button請求書出力";
            this.Button請求書出力.Size = new System.Drawing.Size(149, 38);
            this.Button請求書出力.TabIndex = 12;
            this.Button請求書出力.Text = "請求書出力";
            this.Button請求書出力.UseVisualStyleBackColor = false;
            this.Button請求書出力.Click += new System.EventHandler(this.Button請求書出力_Click);
            // 
            // textBox備考
            // 
            this.textBox備考.AutoSize = true;
            this.textBox備考.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.textBox備考.Location = new System.Drawing.Point(127, 67);
            this.textBox備考.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBox備考.MaxLength = 32767;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.ReadOnly = false;
            this.textBox備考.Size = new System.Drawing.Size(572, 75);
            this.textBox備考.TabIndex = 2;
            this.textBox備考.Enter += new System.EventHandler(this.textBox備考_Enter);
            this.textBox備考.Leave += new System.EventHandler(this.textBox備考_Leave);
            // 
            // buttonリスト入力
            // 
            this.buttonリスト入力.BackColor = System.Drawing.SystemColors.Control;
            this.buttonリスト入力.Enabled = false;
            this.buttonリスト入力.Location = new System.Drawing.Point(16, 182);
            this.buttonリスト入力.Name = "buttonリスト入力";
            this.buttonリスト入力.Size = new System.Drawing.Size(149, 38);
            this.buttonリスト入力.TabIndex = 5;
            this.buttonリスト入力.Text = "リスト入力";
            this.buttonリスト入力.UseVisualStyleBackColor = false;
            this.buttonリスト入力.Visible = false;
            this.buttonリスト入力.Click += new System.EventHandler(this.buttonリスト入力_Click);
            // 
            // button査定表出力
            // 
            this.button査定表出力.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button査定表出力.BackColor = System.Drawing.SystemColors.Control;
            this.button査定表出力.Location = new System.Drawing.Point(96, 523);
            this.button査定表出力.Name = "button査定表出力";
            this.button査定表出力.Size = new System.Drawing.Size(149, 38);
            this.button査定表出力.TabIndex = 11;
            this.button査定表出力.Text = "査定表出力";
            this.button査定表出力.UseVisualStyleBackColor = false;
            this.button査定表出力.Click += new System.EventHandler(this.Button査定表出力_Click);
            // 
            // 手配依頼Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1117, 573);
            this.Controls.Add(this.buttonリスト入力);
            this.Controls.Add(this.textBox備考);
            this.Controls.Add(this.button査定表出力);
            this.Controls.Add(this.Button請求書出力);
            this.Controls.Add(this.button出力);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button読込);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button保存);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button手配依頼);
            this.Controls.Add(this.button品目追加);
            this.Controls.Add(this.textBox手配内容);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "手配依頼Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.手配依頼Form_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.手配依頼Form_FormClosed);
            this.Load += new System.EventHandler(this.手配依頼Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox手配内容;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
        private System.Windows.Forms.Button button品目追加;
        private System.Windows.Forms.Button button手配依頼;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button保存;
        private System.Windows.Forms.Button button読込;
        private System.Windows.Forms.DateTimePicker dateTimePicker希望納期;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox希望港;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.OpenFileDialog openFileDialogドックオーダー読込;
        private System.Windows.Forms.SaveFileDialog saveFileDialogドックオーダー出力;
        private System.Windows.Forms.Button Button請求書出力;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private NBaseUtil.MultiLineCombo textBox備考;
        private System.Windows.Forms.Button buttonリスト入力;
        private System.Windows.Forms.Button button査定表出力;
    }
}