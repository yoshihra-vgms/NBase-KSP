namespace NBaseHonsen
{
    partial class 特定船用品選択Form
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
            this.button選択 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_カテゴリ = new System.Windows.Forms.TextBox();
            this.button_全選択 = new System.Windows.Forms.Button();
            this.button_全て解除 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button選択
            // 
            this.button選択.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button選択.BackColor = System.Drawing.SystemColors.Control;
            this.button選択.Location = new System.Drawing.Point(253, 512);
            this.button選択.Margin = new System.Windows.Forms.Padding(5);
            this.button選択.Name = "button選択";
            this.button選択.Size = new System.Drawing.Size(125, 36);
            this.button選択.TabIndex = 4;
            this.button選択.Text = "選択";
            this.button選択.UseVisualStyleBackColor = false;
            this.button選択.Click += new System.EventHandler(this.button選択_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(394, 512);
            this.button閉じる.Margin = new System.Windows.Forms.Padding(5);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(125, 36);
            this.button閉じる.TabIndex = 5;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "選択されているカテゴリ";
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
            this.dataGridView1.Location = new System.Drawing.Point(20, 117);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(744, 379);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValidated);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEnter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 88);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "特定船用品";
            // 
            // textBox_カテゴリ
            // 
            this.textBox_カテゴリ.BackColor = System.Drawing.Color.White;
            this.textBox_カテゴリ.Location = new System.Drawing.Point(207, 14);
            this.textBox_カテゴリ.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_カテゴリ.Name = "textBox_カテゴリ";
            this.textBox_カテゴリ.ReadOnly = true;
            this.textBox_カテゴリ.Size = new System.Drawing.Size(351, 26);
            this.textBox_カテゴリ.TabIndex = 0;
            this.textBox_カテゴリ.TabStop = false;
            // 
            // button_全選択
            // 
            this.button_全選択.BackColor = System.Drawing.SystemColors.Control;
            this.button_全選択.Location = new System.Drawing.Point(139, 71);
            this.button_全選択.Margin = new System.Windows.Forms.Padding(5);
            this.button_全選択.Name = "button_全選択";
            this.button_全選択.Size = new System.Drawing.Size(125, 36);
            this.button_全選択.TabIndex = 2;
            this.button_全選択.Text = "全て選択";
            this.button_全選択.UseVisualStyleBackColor = false;
            this.button_全選択.Click += new System.EventHandler(this.button_全選択_Click);
            // 
            // button_全て解除
            // 
            this.button_全て解除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_全て解除.BackColor = System.Drawing.SystemColors.Control;
            this.button_全て解除.Location = new System.Drawing.Point(274, 71);
            this.button_全て解除.Margin = new System.Windows.Forms.Padding(5);
            this.button_全て解除.Name = "button_全て解除";
            this.button_全て解除.Size = new System.Drawing.Size(125, 36);
            this.button_全て解除.TabIndex = 3;
            this.button_全て解除.Text = "全て解除";
            this.button_全て解除.UseVisualStyleBackColor = false;
            this.button_全て解除.Click += new System.EventHandler(this.button_全て解除_Click);
            // 
            // 特定船用品選択Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.textBox_カテゴリ);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button_全て解除);
            this.Controls.Add(this.button_全選択);
            this.Controls.Add(this.button選択);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "特定船用品選択Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "特定船用品選択";
            this.Load += new System.EventHandler(this.特定船用品選択Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button選択;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_カテゴリ;
        private System.Windows.Forms.Button button_全選択;
        private System.Windows.Forms.Button button_全て解除;
    }
}