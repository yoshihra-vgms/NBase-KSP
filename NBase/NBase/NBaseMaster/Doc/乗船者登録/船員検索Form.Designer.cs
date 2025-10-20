namespace NBaseMaster.Doc.乗船者登録
{
    partial class 船員検索Form
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
            this.buttonクリア = new System.Windows.Forms.Button();
            this.button検索 = new System.Windows.Forms.Button();
            this.textBox名カナ = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox姓カナ = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button選択 = new System.Windows.Forms.Button();
            this.buttonキャンセル = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonクリア
            // 
            this.buttonクリア.BackColor = System.Drawing.SystemColors.Control;
            this.buttonクリア.Location = new System.Drawing.Point(344, 40);
            this.buttonクリア.Name = "buttonクリア";
            this.buttonクリア.Size = new System.Drawing.Size(92, 23);
            this.buttonクリア.TabIndex = 4;
            this.buttonクリア.Text = "検索条件クリア";
            this.buttonクリア.UseVisualStyleBackColor = false;
            this.buttonクリア.Click += new System.EventHandler(this.buttonクリア_Click);
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(344, 12);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(92, 23);
            this.button検索.TabIndex = 3;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // textBox名カナ
            // 
            this.textBox名カナ.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox名カナ.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.textBox名カナ.Location = new System.Drawing.Point(222, 15);
            this.textBox名カナ.MaxLength = 20;
            this.textBox名カナ.Name = "textBox名カナ";
            this.textBox名カナ.Size = new System.Drawing.Size(90, 20);
            this.textBox名カナ.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label20.Location = new System.Drawing.Point(168, 18);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 13);
            this.label20.TabIndex = 54;
            this.label20.Text = "名(カナ)";
            // 
            // textBox姓カナ
            // 
            this.textBox姓カナ.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox姓カナ.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.textBox姓カナ.Location = new System.Drawing.Point(71, 15);
            this.textBox姓カナ.MaxLength = 20;
            this.textBox姓カナ.Name = "textBox姓カナ";
            this.textBox姓カナ.Size = new System.Drawing.Size(90, 20);
            this.textBox姓カナ.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "姓(カナ)";
            // 
            // button選択
            // 
            this.button選択.Location = new System.Drawing.Point(144, 294);
            this.button選択.Name = "button選択";
            this.button選択.Size = new System.Drawing.Size(75, 23);
            this.button選択.TabIndex = 6;
            this.button選択.Text = "選択";
            this.button選択.UseVisualStyleBackColor = true;
            this.button選択.Click += new System.EventHandler(this.button選択_Click);
            // 
            // buttonキャンセル
            // 
            this.buttonキャンセル.Location = new System.Drawing.Point(237, 294);
            this.buttonキャンセル.Name = "buttonキャンセル";
            this.buttonキャンセル.Size = new System.Drawing.Size(75, 23);
            this.buttonキャンセル.TabIndex = 7;
            this.buttonキャンセル.Text = "キャンセル";
            this.buttonキャンセル.UseVisualStyleBackColor = true;
            this.buttonキャンセル.Click += new System.EventHandler(this.buttonキャンセル_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 81);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(433, 198);
            this.dataGridView1.TabIndex = 5;
            // 
            // 船員検索Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(457, 329);
            this.Controls.Add(this.buttonキャンセル);
            this.Controls.Add(this.button選択);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox名カナ);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.textBox姓カナ);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonクリア);
            this.Controls.Add(this.button検索);
            this.Name = "船員検索Form";
            this.Text = "船員検索Form";
            this.Load += new System.EventHandler(this.船員検索Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonクリア;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.TextBox textBox名カナ;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox姓カナ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button選択;
        private System.Windows.Forms.Button buttonキャンセル;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}