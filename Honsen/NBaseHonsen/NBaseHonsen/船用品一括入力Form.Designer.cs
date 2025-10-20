namespace NBaseHonsen
{
    partial class 船用品一括入力Form
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
            this.textBoxヘッダ = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button登録 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button複写 = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.button追加 = new System.Windows.Forms.Button();
            this.button削除_追加詳細 = new System.Windows.Forms.Button();
            this.button複写_追加詳細 = new System.Windows.Forms.Button();
            this.singleLineComboEx1 = new NBaseUtil.SingleLineComboEx();
            this.radioButton_甲板部特定品 = new System.Windows.Forms.RadioButton();
            this.radioButton_機関部特定品 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxヘッダ
            // 
            this.textBoxヘッダ.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBoxヘッダ.Location = new System.Drawing.Point(140, 18);
            this.textBoxヘッダ.MaxLength = 50;
            this.textBoxヘッダ.Name = "textBoxヘッダ";
            this.textBoxヘッダ.Size = new System.Drawing.Size(572, 26);
            this.textBoxヘッダ.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 19);
            this.label8.TabIndex = 2;
            this.label8.Text = "※部署名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "　 詳細品目";
            // 
            // button登録
            // 
            this.button登録.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button登録.BackColor = System.Drawing.SystemColors.Control;
            this.button登録.Enabled = false;
            this.button登録.Location = new System.Drawing.Point(512, 671);
            this.button登録.Name = "button登録";
            this.button登録.Size = new System.Drawing.Size(149, 38);
            this.button登録.TabIndex = 11;
            this.button登録.Text = "登録";
            this.button登録.UseVisualStyleBackColor = false;
            this.button登録.Click += new System.EventHandler(this.button登録_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(677, 671);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(149, 38);
            this.button閉じる.TabIndex = 12;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(26, 126);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1282, 304);
            this.dataGridView1.TabIndex = 13;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEnter);
            this.dataGridView1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValidated);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView_CellValidating);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(26, 514);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 21;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(1282, 135);
            this.dataGridView2.TabIndex = 13;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView2.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellEnter);
            this.dataGridView2.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValidated);
            this.dataGridView2.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView_CellValidating);
            // 
            // button複写
            // 
            this.button複写.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button複写.BackColor = System.Drawing.SystemColors.Control;
            this.button複写.Enabled = false;
            this.button複写.Location = new System.Drawing.Point(1002, 75);
            this.button複写.Name = "button複写";
            this.button複写.Size = new System.Drawing.Size(149, 38);
            this.button複写.TabIndex = 11;
            this.button複写.Text = "複写";
            this.button複写.UseVisualStyleBackColor = false;
            this.button複写.Click += new System.EventHandler(this.button複写_Click);
            // 
            // button削除
            // 
            this.button削除.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Enabled = false;
            this.button削除.Location = new System.Drawing.Point(1159, 75);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(149, 38);
            this.button削除.TabIndex = 11;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button追加
            // 
            this.button追加.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button追加.BackColor = System.Drawing.SystemColors.Control;
            this.button追加.Enabled = false;
            this.button追加.Location = new System.Drawing.Point(1159, 11);
            this.button追加.Name = "button追加";
            this.button追加.Size = new System.Drawing.Size(149, 38);
            this.button追加.TabIndex = 11;
            this.button追加.Text = "追加";
            this.button追加.UseVisualStyleBackColor = false;
            this.button追加.Click += new System.EventHandler(this.button追加_Click);
            // 
            // button削除_追加詳細
            // 
            this.button削除_追加詳細.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button削除_追加詳細.BackColor = System.Drawing.SystemColors.Control;
            this.button削除_追加詳細.Enabled = false;
            this.button削除_追加詳細.Location = new System.Drawing.Point(1159, 460);
            this.button削除_追加詳細.Name = "button削除_追加詳細";
            this.button削除_追加詳細.Size = new System.Drawing.Size(149, 38);
            this.button削除_追加詳細.TabIndex = 14;
            this.button削除_追加詳細.Text = "削除";
            this.button削除_追加詳細.UseVisualStyleBackColor = false;
            this.button削除_追加詳細.Click += new System.EventHandler(this.button削除_追加詳細_Click);
            // 
            // button複写_追加詳細
            // 
            this.button複写_追加詳細.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button複写_追加詳細.BackColor = System.Drawing.SystemColors.Control;
            this.button複写_追加詳細.Enabled = false;
            this.button複写_追加詳細.Location = new System.Drawing.Point(1002, 460);
            this.button複写_追加詳細.Name = "button複写_追加詳細";
            this.button複写_追加詳細.Size = new System.Drawing.Size(149, 38);
            this.button複写_追加詳細.TabIndex = 15;
            this.button複写_追加詳細.Text = "複写";
            this.button複写_追加詳細.UseVisualStyleBackColor = false;
            this.button複写_追加詳細.Click += new System.EventHandler(this.button複写_追加詳細_Click);
            // 
            // singleLineComboEx1
            // 
            this.singleLineComboEx1.AutoSize = true;
            this.singleLineComboEx1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.singleLineComboEx1.Location = new System.Drawing.Point(140, 81);
            this.singleLineComboEx1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.singleLineComboEx1.MaxLength = 32767;
            this.singleLineComboEx1.Name = "singleLineComboEx1";
            this.singleLineComboEx1.ReadOnly = false;
            this.singleLineComboEx1.Size = new System.Drawing.Size(455, 26);
            this.singleLineComboEx1.TabIndex = 4;
            // 
            // radioButton_甲板部特定品
            // 
            this.radioButton_甲板部特定品.AutoCheck = false;
            this.radioButton_甲板部特定品.AutoSize = true;
            this.radioButton_甲板部特定品.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton_甲板部特定品.Location = new System.Drawing.Point(140, 50);
            this.radioButton_甲板部特定品.Name = "radioButton_甲板部特定品";
            this.radioButton_甲板部特定品.Size = new System.Drawing.Size(429, 25);
            this.radioButton_甲板部特定品.TabIndex = 16;
            this.radioButton_甲板部特定品.Text = "甲板部特定品(ギャレー用品、事務用品含む)";
            this.radioButton_甲板部特定品.UseVisualStyleBackColor = true;
            this.radioButton_甲板部特定品.Click += new System.EventHandler(this.radioButton_甲板部特定品_Click);
            // 
            // radioButton_機関部特定品
            // 
            this.radioButton_機関部特定品.AutoCheck = false;
            this.radioButton_機関部特定品.AutoSize = true;
            this.radioButton_機関部特定品.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton_機関部特定品.Location = new System.Drawing.Point(597, 50);
            this.radioButton_機関部特定品.Name = "radioButton_機関部特定品";
            this.radioButton_機関部特定品.Size = new System.Drawing.Size(160, 25);
            this.radioButton_機関部特定品.TabIndex = 16;
            this.radioButton_機関部特定品.Text = "機関部特定品";
            this.radioButton_機関部特定品.UseVisualStyleBackColor = true;
            this.radioButton_機関部特定品.Click += new System.EventHandler(this.radioButton_機関部特定品_Click);
            // 
            // 船用品一括入力Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1334, 721);
            this.Controls.Add(this.radioButton_機関部特定品);
            this.Controls.Add(this.radioButton_甲板部特定品);
            this.Controls.Add(this.singleLineComboEx1);
            this.Controls.Add(this.button削除_追加詳細);
            this.Controls.Add(this.button複写_追加詳細);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button複写);
            this.Controls.Add(this.button追加);
            this.Controls.Add(this.button登録);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.textBoxヘッダ);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MinimumSize = new System.Drawing.Size(1350, 760);
            this.Name = "船用品一括入力Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "特定品リスト入力Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.特定品リスト入力Form_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.特定品リスト入力Form_FormClosed);
            this.Load += new System.EventHandler(this.特定品リスト入力Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxヘッダ;
        private System.Windows.Forms.Label label8;
        private NBaseUtil.SingleLineComboEx singleLineComboEx1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button登録;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button複写;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button追加;
        private System.Windows.Forms.Button button削除_追加詳細;
        private System.Windows.Forms.Button button複写_追加詳細;
        private System.Windows.Forms.RadioButton radioButton_甲板部特定品;
        private System.Windows.Forms.RadioButton radioButton_機関部特定品;

    }
}