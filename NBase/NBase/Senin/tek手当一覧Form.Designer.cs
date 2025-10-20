
namespace Senin
{
    partial class tek手当一覧Form
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox手当名 = new System.Windows.Forms.TextBox();
            this.buttonクリア = new System.Windows.Forms.Button();
            this.button検索 = new System.Windows.Forms.Button();
            this.comboBox船名 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox年月From = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button追加 = new System.Windows.Forms.Button();
            this.comboBox年月To = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button出力 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 126);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(764, 364);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox手当名);
            this.groupBox1.Controls.Add(this.buttonクリア);
            this.groupBox1.Controls.Add(this.button出力);
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Controls.Add(this.comboBox船名);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox年月To);
            this.groupBox1.Controls.Add(this.comboBox年月From);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 108);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // textBox手当名
            // 
            this.textBox手当名.Location = new System.Drawing.Point(83, 75);
            this.textBox手当名.Name = "textBox手当名";
            this.textBox手当名.Size = new System.Drawing.Size(173, 19);
            this.textBox手当名.TabIndex = 4;
            // 
            // buttonクリア
            // 
            this.buttonクリア.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonクリア.Location = new System.Drawing.Point(370, 46);
            this.buttonクリア.Name = "buttonクリア";
            this.buttonクリア.Size = new System.Drawing.Size(92, 23);
            this.buttonクリア.TabIndex = 6;
            this.buttonクリア.Text = "検索条件クリア";
            this.buttonクリア.UseVisualStyleBackColor = true;
            this.buttonクリア.Click += new System.EventHandler(this.buttonクリア_Click);
            // 
            // button検索
            // 
            this.button検索.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button検索.Location = new System.Drawing.Point(370, 17);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(92, 23);
            this.button検索.TabIndex = 5;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = true;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // comboBox船名
            // 
            this.comboBox船名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船名.FormattingEnabled = true;
            this.comboBox船名.Location = new System.Drawing.Point(83, 46);
            this.comboBox船名.Name = "comboBox船名";
            this.comboBox船名.Size = new System.Drawing.Size(173, 20);
            this.comboBox船名.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 41;
            this.label10.Text = "船名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 36;
            this.label1.Text = "手当名";
            // 
            // comboBox年月From
            // 
            this.comboBox年月From.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年月From.FormattingEnabled = true;
            this.comboBox年月From.Location = new System.Drawing.Point(83, 20);
            this.comboBox年月From.Name = "comboBox年月From";
            this.comboBox年月From.Size = new System.Drawing.Size(90, 20);
            this.comboBox年月From.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 35;
            this.label6.Text = "対象月";
            // 
            // button追加
            // 
            this.button追加.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button追加.BackColor = System.Drawing.SystemColors.Control;
            this.button追加.Location = new System.Drawing.Point(684, 97);
            this.button追加.Name = "button追加";
            this.button追加.Size = new System.Drawing.Size(92, 23);
            this.button追加.TabIndex = 1;
            this.button追加.Text = "追加";
            this.button追加.UseVisualStyleBackColor = false;
            this.button追加.Click += new System.EventHandler(this.button追加_Click);
            // 
            // comboBox年月To
            // 
            this.comboBox年月To.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年月To.FormattingEnabled = true;
            this.comboBox年月To.Location = new System.Drawing.Point(200, 20);
            this.comboBox年月To.Name = "comboBox年月To";
            this.comboBox年月To.Size = new System.Drawing.Size(90, 20);
            this.comboBox年月To.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 35;
            this.label2.Text = "～";
            // 
            // button出力
            // 
            this.button出力.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button出力.Location = new System.Drawing.Point(370, 79);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(92, 23);
            this.button出力.TabIndex = 7;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = true;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // tek手当一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(788, 502);
            this.Controls.Add(this.button追加);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(757, 541);
            this.Name = "tek手当一覧Form";
            this.Text = "手当一覧";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.tek手当一覧Form_FormClosing);
            this.Load += new System.EventHandler(this.tek手当一覧Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonクリア;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.ComboBox comboBox船名;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox年月From;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button追加;
        private System.Windows.Forms.TextBox textBox手当名;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.ComboBox comboBox年月To;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}