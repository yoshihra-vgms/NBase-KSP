namespace NBaseMaster
{
    partial class 乗船資格Form
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
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox職名 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button免許追加 = new System.Windows.Forms.Button();
            this.button免許削除 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(46, 22);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(179, 20);
            this.comboBox船.TabIndex = 13;
            this.comboBox船.SelectedIndexChanged += new System.EventHandler(this.comboBox船_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "船";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "職名";
            // 
            // listBox職名
            // 
            this.listBox職名.FormattingEnabled = true;
            this.listBox職名.ItemHeight = 12;
            this.listBox職名.Location = new System.Drawing.Point(25, 82);
            this.listBox職名.Name = "listBox職名";
            this.listBox職名.Size = new System.Drawing.Size(120, 352);
            this.listBox職名.TabIndex = 15;
            this.listBox職名.SelectedIndexChanged += new System.EventHandler(this.listBox職名_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "免許/免状";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(169, 82);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(465, 352);
            this.dataGridView1.TabIndex = 20;
            // 
            // button免許追加
            // 
            this.button免許追加.Location = new System.Drawing.Point(640, 82);
            this.button免許追加.Name = "button免許追加";
            this.button免許追加.Size = new System.Drawing.Size(101, 23);
            this.button免許追加.TabIndex = 21;
            this.button免許追加.Text = "免許/免状 追加";
            this.button免許追加.UseVisualStyleBackColor = true;
            this.button免許追加.Click += new System.EventHandler(this.button免許追加_Click);
            // 
            // button免許削除
            // 
            this.button免許削除.Location = new System.Drawing.Point(640, 111);
            this.button免許削除.Name = "button免許削除";
            this.button免許削除.Size = new System.Drawing.Size(101, 23);
            this.button免許削除.TabIndex = 21;
            this.button免許削除.Text = "免許/免状 削除";
            this.button免許削除.UseVisualStyleBackColor = true;
            this.button免許削除.Click += new System.EventHandler(this.button免許削除_Click);
            // 
            // 乗船資格Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(767, 467);
            this.Controls.Add(this.button免許削除);
            this.Controls.Add(this.button免許追加);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.listBox職名);
            this.Controls.Add(this.comboBox船);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Name = "乗船資格Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "乗船資格Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.乗船資格Form_FormClosing);
            this.Load += new System.EventHandler(this.乗船資格Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox職名;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button免許追加;
        private System.Windows.Forms.Button button免許削除;
    }
}