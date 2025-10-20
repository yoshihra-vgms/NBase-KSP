
namespace Senin
{
    partial class tek手当詳細Form
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
            this.comboBox手当名 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox船名 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox船長 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox作業期間 = new System.Windows.Forms.ComboBox();
            this.label作業期間 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox作業内容 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox金額 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox支給額 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox作業責任者 = new System.Windows.Forms.TextBox();
            this.textBox数量 = new System.Windows.Forms.TextBox();
            this.button分割 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button登録 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button出力 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button削除 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox手当名
            // 
            this.comboBox手当名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox手当名.FormattingEnabled = true;
            this.comboBox手当名.Location = new System.Drawing.Point(42, 24);
            this.comboBox手当名.Name = "comboBox手当名";
            this.comboBox手当名.Size = new System.Drawing.Size(248, 20);
            this.comboBox手当名.TabIndex = 1;
            this.comboBox手当名.SelectedIndexChanged += new System.EventHandler(this.comboBox手当名_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(296, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "手当支給依頼書";
            // 
            // comboBox船名
            // 
            this.comboBox船名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船名.FormattingEnabled = true;
            this.comboBox船名.Location = new System.Drawing.Point(83, 74);
            this.comboBox船名.Name = "comboBox船名";
            this.comboBox船名.Size = new System.Drawing.Size(141, 20);
            this.comboBox船名.TabIndex = 3;
            this.comboBox船名.SelectedIndexChanged += new System.EventHandler(this.comboBox船名_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 12);
            this.label10.TabIndex = 44;
            this.label10.Text = "船名　：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(278, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 12);
            this.label2.TabIndex = 44;
            this.label2.Text = "船長　：";
            // 
            // textBox船長
            // 
            this.textBox船長.Location = new System.Drawing.Point(342, 77);
            this.textBox船長.Name = "textBox船長";
            this.textBox船長.ReadOnly = true;
            this.textBox船長.Size = new System.Drawing.Size(120, 19);
            this.textBox船長.TabIndex = 4;
            this.textBox船長.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 12);
            this.label3.TabIndex = 44;
            this.label3.Text = "作業期間　：";
            // 
            // comboBox作業期間
            // 
            this.comboBox作業期間.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox作業期間.FormattingEnabled = true;
            this.comboBox作業期間.Location = new System.Drawing.Point(97, 113);
            this.comboBox作業期間.Name = "comboBox作業期間";
            this.comboBox作業期間.Size = new System.Drawing.Size(127, 20);
            this.comboBox作業期間.TabIndex = 5;
            this.comboBox作業期間.SelectedIndexChanged += new System.EventHandler(this.comboBox作業期間_SelectedIndexChanged);
            // 
            // label作業期間
            // 
            this.label作業期間.AutoSize = true;
            this.label作業期間.Location = new System.Drawing.Point(95, 146);
            this.label作業期間.Name = "label作業期間";
            this.label作業期間.Size = new System.Drawing.Size(67, 12);
            this.label作業期間.TabIndex = 6;
            this.label作業期間.Text = "作業期間　：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "作業種類および内容　：";
            // 
            // textBox作業内容
            // 
            this.textBox作業内容.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox作業内容.Location = new System.Drawing.Point(51, 203);
            this.textBox作業内容.MaxLength = 500;
            this.textBox作業内容.Multiline = true;
            this.textBox作業内容.Name = "textBox作業内容";
            this.textBox作業内容.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox作業内容.Size = new System.Drawing.Size(387, 82);
            this.textBox作業内容.TabIndex = 8;
            this.textBox作業内容.TextChanged += new System.EventHandler(this.textBox作業内容_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 310);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 12);
            this.label6.TabIndex = 44;
            this.label6.Text = "数量　：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(183, 310);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 12);
            this.label7.TabIndex = 44;
            this.label7.Text = "金額　：";
            // 
            // textBox金額
            // 
            this.textBox金額.ForeColor = System.Drawing.Color.Black;
            this.textBox金額.Location = new System.Drawing.Point(232, 307);
            this.textBox金額.Name = "textBox金額";
            this.textBox金額.ReadOnly = true;
            this.textBox金額.Size = new System.Drawing.Size(85, 19);
            this.textBox金額.TabIndex = 10;
            this.textBox金額.TabStop = false;
            this.textBox金額.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(36, 344);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 12);
            this.label8.TabIndex = 44;
            this.label8.Text = "支給額　：";
            // 
            // textBox支給額
            // 
            this.textBox支給額.Location = new System.Drawing.Point(97, 341);
            this.textBox支給額.Name = "textBox支給額";
            this.textBox支給額.ReadOnly = true;
            this.textBox支給額.Size = new System.Drawing.Size(85, 19);
            this.textBox支給額.TabIndex = 45;
            this.textBox支給額.TabStop = false;
            this.textBox支給額.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(272, 344);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 12);
            this.label9.TabIndex = 44;
            this.label9.Text = "作業責任者　：";
            // 
            // textBox作業責任者
            // 
            this.textBox作業責任者.Location = new System.Drawing.Point(357, 341);
            this.textBox作業責任者.Name = "textBox作業責任者";
            this.textBox作業責任者.Size = new System.Drawing.Size(120, 19);
            this.textBox作業責任者.TabIndex = 12;
            this.textBox作業責任者.TabStop = false;
            // 
            // textBox数量
            // 
            this.textBox数量.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox数量.Location = new System.Drawing.Point(89, 307);
            this.textBox数量.MaxLength = 6;
            this.textBox数量.Name = "textBox数量";
            this.textBox数量.Size = new System.Drawing.Size(55, 19);
            this.textBox数量.TabIndex = 9;
            this.textBox数量.TabStop = false;
            this.textBox数量.TextChanged += new System.EventHandler(this.textBox数量_TextChanged);
            // 
            // button分割
            // 
            this.button分割.Location = new System.Drawing.Point(402, 305);
            this.button分割.Name = "button分割";
            this.button分割.Size = new System.Drawing.Size(75, 23);
            this.button分割.TabIndex = 11;
            this.button分割.Text = "分割";
            this.button分割.UseVisualStyleBackColor = true;
            this.button分割.Click += new System.EventHandler(this.button分割_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(38, 380);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(439, 320);
            this.dataGridView1.TabIndex = 13;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            this.dataGridView1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValidated);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellValueChanged);
            this.dataGridView1.CurrentCellDirtyStateChanged += new System.EventHandler(this.DataGridView1_CurrentCellDirtyStateChanged);
            // 
            // button登録
            // 
            this.button登録.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button登録.Location = new System.Drawing.Point(151, 720);
            this.button登録.Name = "button登録";
            this.button登録.Size = new System.Drawing.Size(75, 23);
            this.button登録.TabIndex = 20;
            this.button登録.Text = "登録";
            this.button登録.UseVisualStyleBackColor = true;
            this.button登録.Click += new System.EventHandler(this.button登録_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.Location = new System.Drawing.Point(313, 720);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 22;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = true;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button出力
            // 
            this.button出力.Location = new System.Drawing.Point(411, 12);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(75, 23);
            this.button出力.TabIndex = 47;
            this.button出力.Text = "帳票出力";
            this.button出力.UseVisualStyleBackColor = true;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.Location = new System.Drawing.Point(232, 720);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 20;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = true;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // tek手当詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(523, 755);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button出力);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button登録);
            this.Controls.Add(this.button分割);
            this.Controls.Add(this.textBox作業内容);
            this.Controls.Add(this.textBox数量);
            this.Controls.Add(this.textBox作業責任者);
            this.Controls.Add(this.textBox支給額);
            this.Controls.Add(this.textBox金額);
            this.Controls.Add(this.textBox船長);
            this.Controls.Add(this.comboBox作業期間);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBox船名);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label作業期間);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox手当名);
            this.MinimumSize = new System.Drawing.Size(539, 794);
            this.Name = "tek手当詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "手当登録";
            this.Load += new System.EventHandler(this.tek手当詳細Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox手当名;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox船名;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox船長;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox作業期間;
        private System.Windows.Forms.Label label作業期間;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox作業内容;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox金額;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox支給額;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox作業責任者;
        private System.Windows.Forms.TextBox textBox数量;
        private System.Windows.Forms.Button button分割;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button登録;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button削除;
    }
}