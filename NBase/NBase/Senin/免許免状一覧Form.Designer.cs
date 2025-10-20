namespace Senin
{
    partial class 免許免状一覧Form
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
            LidorSystems.IntegralUI.Lists.Style.ListColumnFormatStyle listColumnFormatStyle1 = new LidorSystems.IntegralUI.Lists.Style.ListColumnFormatStyle();
            LidorSystems.IntegralUI.Style.BorderCornerShape borderCornerShape1 = new LidorSystems.IntegralUI.Style.BorderCornerShape();
            LidorSystems.IntegralUI.Style.WatermarkImage watermarkImage2 = new LidorSystems.IntegralUI.Style.WatermarkImage();
            this.treeListView免許免状 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.label1 = new System.Windows.Forms.Label();
            this.treeListView職名 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button検索 = new System.Windows.Forms.Button();
            this.buttonクリア = new System.Windows.Forms.Button();
            this.button出力 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox氏名 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox氏名コード = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox有効期限 = new System.Windows.Forms.ComboBox();
            this.radioButton取得済 = new System.Windows.Forms.RadioButton();
            this.radioButton未取得 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.label対象件数 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView免許免状)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView職名)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeListView免許免状
            // 
            this.treeListView免許免状.CheckBoxes = true;
            // 
            // 
            // 
            this.treeListView免許免状.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView免許免状.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView免許免状.ContentPanel.Name = "";
            this.treeListView免許免状.ContentPanel.Size = new System.Drawing.Size(330, 180);
            this.treeListView免許免状.ContentPanel.TabIndex = 3;
            this.treeListView免許免状.ContentPanel.TabStop = false;
            this.treeListView免許免状.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView免許免状.Footer = false;
            this.treeListView免許免状.Location = new System.Drawing.Point(18, 33);
            this.treeListView免許免状.Name = "treeListView免許免状";
            this.treeListView免許免状.Size = new System.Drawing.Size(336, 186);
            this.treeListView免許免状.TabIndex = 0;
            this.treeListView免許免状.Text = "treeListView1";
            this.treeListView免許免状.WatermarkImage = watermarkImage1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "免許・免状 / 種別";
            // 
            // treeListView職名
            // 
            this.treeListView職名.CheckBoxes = true;
            borderCornerShape1.BottomLeft = LidorSystems.IntegralUI.Style.CornerShape.Squared;
            borderCornerShape1.BottomRight = LidorSystems.IntegralUI.Style.CornerShape.Squared;
            borderCornerShape1.TopLeft = LidorSystems.IntegralUI.Style.CornerShape.Squared;
            borderCornerShape1.TopRight = LidorSystems.IntegralUI.Style.CornerShape.Squared;
            listColumnFormatStyle1.FooterBorderCornerShape = borderCornerShape1;
            this.treeListView職名.ColumnFormatStyle = listColumnFormatStyle1;
            // 
            // 
            // 
            this.treeListView職名.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView職名.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView職名.ContentPanel.Name = "";
            this.treeListView職名.ContentPanel.Size = new System.Drawing.Size(159, 180);
            this.treeListView職名.ContentPanel.TabIndex = 3;
            this.treeListView職名.ContentPanel.TabStop = false;
            this.treeListView職名.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView職名.Footer = false;
            this.treeListView職名.Location = new System.Drawing.Point(372, 33);
            this.treeListView職名.Name = "treeListView職名";
            this.treeListView職名.Size = new System.Drawing.Size(165, 186);
            this.treeListView職名.TabIndex = 1;
            this.treeListView職名.Text = "treeListView1";
            this.treeListView職名.WatermarkImage = watermarkImage2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(370, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "職名";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 267);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(965, 308);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // button検索
            // 
            this.button検索.Location = new System.Drawing.Point(780, 22);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(92, 23);
            this.button検索.TabIndex = 7;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = true;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // buttonクリア
            // 
            this.buttonクリア.Location = new System.Drawing.Point(780, 53);
            this.buttonクリア.Name = "buttonクリア";
            this.buttonクリア.Size = new System.Drawing.Size(92, 23);
            this.buttonクリア.TabIndex = 8;
            this.buttonクリア.Text = "検索条件クリア";
            this.buttonクリア.UseVisualStyleBackColor = true;
            this.buttonクリア.Click += new System.EventHandler(this.buttonクリア_Click);
            // 
            // button出力
            // 
            this.button出力.Location = new System.Drawing.Point(12, 240);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(92, 23);
            this.button出力.TabIndex = 1;
            this.button出力.Text = "Excel出力";
            this.button出力.UseVisualStyleBackColor = true;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(558, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "従業員番号";
            // 
            // textBox氏名
            // 
            this.textBox氏名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox氏名.Location = new System.Drawing.Point(627, 55);
            this.textBox氏名.Name = "textBox氏名";
            this.textBox氏名.Size = new System.Drawing.Size(121, 19);
            this.textBox氏名.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(558, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "氏名";
            // 
            // textBox氏名コード
            // 
            this.textBox氏名コード.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox氏名コード.Location = new System.Drawing.Point(627, 24);
            this.textBox氏名コード.Name = "textBox氏名コード";
            this.textBox氏名コード.Size = new System.Drawing.Size(100, 19);
            this.textBox氏名コード.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(558, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "有効期限";
            // 
            // comboBox有効期限
            // 
            this.comboBox有効期限.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox有効期限.FormattingEnabled = true;
            this.comboBox有効期限.Location = new System.Drawing.Point(627, 86);
            this.comboBox有効期限.Name = "comboBox有効期限";
            this.comboBox有効期限.Size = new System.Drawing.Size(121, 20);
            this.comboBox有効期限.TabIndex = 4;
            // 
            // radioButton取得済
            // 
            this.radioButton取得済.AutoSize = true;
            this.radioButton取得済.Location = new System.Drawing.Point(560, 125);
            this.radioButton取得済.Name = "radioButton取得済";
            this.radioButton取得済.Size = new System.Drawing.Size(70, 16);
            this.radioButton取得済.TabIndex = 5;
            this.radioButton取得済.Text = "取得済み";
            this.radioButton取得済.UseVisualStyleBackColor = true;
            // 
            // radioButton未取得
            // 
            this.radioButton未取得.AutoSize = true;
            this.radioButton未取得.Checked = true;
            this.radioButton未取得.Location = new System.Drawing.Point(647, 125);
            this.radioButton未取得.Name = "radioButton未取得";
            this.radioButton未取得.Size = new System.Drawing.Size(59, 16);
            this.radioButton未取得.TabIndex = 6;
            this.radioButton未取得.TabStop = true;
            this.radioButton未取得.Text = "未取得";
            this.radioButton未取得.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioButton未取得);
            this.groupBox1.Controls.Add(this.buttonクリア);
            this.groupBox1.Controls.Add(this.treeListView免許免状);
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Controls.Add(this.radioButton取得済);
            this.groupBox1.Controls.Add(this.treeListView職名);
            this.groupBox1.Controls.Add(this.comboBox有効期限);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox氏名);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox氏名コード);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(887, 228);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(845, 248);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "件数：";
            // 
            // label対象件数
            // 
            this.label対象件数.AutoSize = true;
            this.label対象件数.BackColor = System.Drawing.Color.White;
            this.label対象件数.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label対象件数.Location = new System.Drawing.Point(890, 248);
            this.label対象件数.Name = "label対象件数";
            this.label対象件数.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label対象件数.Size = new System.Drawing.Size(70, 13);
            this.label対象件数.TabIndex = 4;
            this.label対象件数.Text = "99999 件";
            // 
            // 免許免状一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(994, 587);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label対象件数);
            this.Controls.Add(this.button出力);
            this.Controls.Add(this.dataGridView1);
            this.MinimumSize = new System.Drawing.Size(1010, 625);
            this.Name = "免許免状一覧Form";
            this.Text = "免許免状一覧Form";
            this.Load += new System.EventHandler(this.免許免状一覧Form_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.免許免状一覧Form_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView免許免状)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView職名)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LidorSystems.IntegralUI.Lists.TreeListView treeListView免許免状;
        private System.Windows.Forms.Label label1;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView職名;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.Button buttonクリア;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox氏名;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox氏名コード;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox有効期限;
        private System.Windows.Forms.RadioButton radioButton取得済;
        private System.Windows.Forms.RadioButton radioButton未取得;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label対象件数;
    }
}