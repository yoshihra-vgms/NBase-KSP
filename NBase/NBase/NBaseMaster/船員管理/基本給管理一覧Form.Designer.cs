namespace NBaseMaster.船員管理
{
    partial class 基本給管理一覧Form
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox海技士 = new System.Windows.Forms.CheckBox();
            this.checkBox航機通砲手 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button検索 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button複製 = new System.Windows.Forms.Button();
            this.button新規 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.checkBox部員 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(733, 425);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox部員);
            this.groupBox1.Controls.Add(this.checkBox海技士);
            this.groupBox1.Controls.Add(this.checkBox航機通砲手);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button検索);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(513, 52);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "検索条件";
            // 
            // checkBox海技士
            // 
            this.checkBox海技士.AutoSize = true;
            this.checkBox海技士.Checked = true;
            this.checkBox海技士.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox海技士.Location = new System.Drawing.Point(150, 20);
            this.checkBox海技士.Name = "checkBox海技士";
            this.checkBox海技士.Size = new System.Drawing.Size(94, 16);
            this.checkBox海技士.TabIndex = 6;
            this.checkBox海技士.Text = "４・５級海技士";
            this.checkBox海技士.UseVisualStyleBackColor = true;
            // 
            // checkBox航機通砲手
            // 
            this.checkBox航機通砲手.AutoSize = true;
            this.checkBox航機通砲手.Checked = true;
            this.checkBox航機通砲手.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox航機通砲手.Location = new System.Drawing.Point(60, 20);
            this.checkBox航機通砲手.Name = "checkBox航機通砲手";
            this.checkBox航機通砲手.Size = new System.Drawing.Size(84, 16);
            this.checkBox航機通砲手.TabIndex = 6;
            this.checkBox航機通砲手.Text = "航機通砲手";
            this.checkBox航機通砲手.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "種別";
            // 
            // button検索
            // 
            this.button検索.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(401, 16);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(89, 23);
            this.button検索.TabIndex = 3;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 65);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(727, 327);
            this.dataGridView1.TabIndex = 19;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button複製);
            this.panel1.Controls.Add(this.button新規);
            this.panel1.Controls.Add(this.button閉じる);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 398);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(727, 24);
            this.panel1.TabIndex = 20;
            // 
            // button複製
            // 
            this.button複製.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button複製.BackColor = System.Drawing.SystemColors.Control;
            this.button複製.Location = new System.Drawing.Point(326, 1);
            this.button複製.Name = "button複製";
            this.button複製.Size = new System.Drawing.Size(75, 23);
            this.button複製.TabIndex = 22;
            this.button複製.Text = "複製";
            this.button複製.UseVisualStyleBackColor = false;
            this.button複製.Click += new System.EventHandler(this.button複製_Click);
            // 
            // button新規
            // 
            this.button新規.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button新規.BackColor = System.Drawing.SystemColors.Control;
            this.button新規.Location = new System.Drawing.Point(246, 1);
            this.button新規.Name = "button新規";
            this.button新規.Size = new System.Drawing.Size(75, 23);
            this.button新規.TabIndex = 20;
            this.button新規.Text = "追加";
            this.button新規.UseVisualStyleBackColor = false;
            this.button新規.Click += new System.EventHandler(this.button新規_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(406, 1);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 21;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // checkBox部員
            // 
            this.checkBox部員.AutoSize = true;
            this.checkBox部員.Checked = true;
            this.checkBox部員.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox部員.Location = new System.Drawing.Point(250, 20);
            this.checkBox部員.Name = "checkBox部員";
            this.checkBox部員.Size = new System.Drawing.Size(48, 16);
            this.checkBox部員.TabIndex = 7;
            this.checkBox部員.Text = "部員";
            this.checkBox部員.UseVisualStyleBackColor = true;
            // 
            // 基本給管理一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(745, 437);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "基本給管理一覧Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基本給管理Form";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button新規;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button複製;
        private System.Windows.Forms.CheckBox checkBox海技士;
        private System.Windows.Forms.CheckBox checkBox航機通砲手;
        private System.Windows.Forms.CheckBox checkBox部員;
    }
}