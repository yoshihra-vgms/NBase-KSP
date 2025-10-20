namespace Senin
{
    partial class 休暇一覧Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.comboBox年度 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox職名 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox氏名 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox氏名コード = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonクリア = new System.Windows.Forms.Button();
            this.button検索 = new System.Windows.Forms.Button();
            this.button登録 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox年度
            // 
            this.comboBox年度.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年度.FormattingEnabled = true;
            this.comboBox年度.Location = new System.Drawing.Point(83, 20);
            this.comboBox年度.Name = "comboBox年度";
            this.comboBox年度.Size = new System.Drawing.Size(61, 20);
            this.comboBox年度.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 35;
            this.label6.Text = "年度";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonクリア);
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Controls.Add(this.comboBox職名);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBox氏名);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox氏名コード);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboBox年度);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(585, 108);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // comboBox職名
            // 
            this.comboBox職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名.FormattingEnabled = true;
            this.comboBox職名.Location = new System.Drawing.Point(83, 46);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(150, 20);
            this.comboBox職名.TabIndex = 40;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 41;
            this.label10.Text = "職名";
            // 
            // textBox氏名
            // 
            this.textBox氏名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox氏名.Location = new System.Drawing.Point(286, 72);
            this.textBox氏名.Name = "textBox氏名";
            this.textBox氏名.Size = new System.Drawing.Size(110, 19);
            this.textBox氏名.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "氏名";
            // 
            // textBox氏名コード
            // 
            this.textBox氏名コード.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox氏名コード.Location = new System.Drawing.Point(83, 72);
            this.textBox氏名コード.Name = "textBox氏名コード";
            this.textBox氏名コード.Size = new System.Drawing.Size(100, 19);
            this.textBox氏名コード.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 12);
            this.label1.TabIndex = 36;
            this.label1.Text = "従業員番号";
            // 
            // buttonクリア
            // 
            this.buttonクリア.Location = new System.Drawing.Point(476, 46);
            this.buttonクリア.Name = "buttonクリア";
            this.buttonクリア.Size = new System.Drawing.Size(92, 23);
            this.buttonクリア.TabIndex = 43;
            this.buttonクリア.Text = "検索条件クリア";
            this.buttonクリア.UseVisualStyleBackColor = true;
            this.buttonクリア.Click += new System.EventHandler(this.buttonクリア_Click);
            // 
            // button検索
            // 
            this.button検索.Location = new System.Drawing.Point(476, 17);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(92, 23);
            this.button検索.TabIndex = 42;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = true;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // button登録
            // 
            this.button登録.Location = new System.Drawing.Point(506, 157);
            this.button登録.Name = "button登録";
            this.button登録.Size = new System.Drawing.Size(92, 23);
            this.button登録.TabIndex = 42;
            this.button登録.Text = "登録";
            this.button登録.UseVisualStyleBackColor = true;
            this.button登録.Click += new System.EventHandler(this.button登録_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Location = new System.Drawing.Point(13, 139);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(484, 55);
            this.dataGridView1.TabIndex = 43;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 211);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 21;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(815, 315);
            this.dataGridView2.TabIndex = 43;
            this.dataGridView2.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValidated);
            this.dataGridView2.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView2_CellValidating);
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEnter);
            // 
            // 休暇一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(839, 538);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button登録);
            this.Name = "休暇一覧Form";
            this.Text = "休暇一覧Form";
            this.Load += new System.EventHandler(this.休暇一覧Form_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.休暇一覧FormForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox年度;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox職名;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox氏名;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox氏名コード;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonクリア;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.Button button登録;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}