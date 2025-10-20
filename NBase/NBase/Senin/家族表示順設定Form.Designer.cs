namespace Senin
{
    partial class 家族表示順設定Form
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
            this.button更新 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button上 = new System.Windows.Forms.Button();
            this.button下 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
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
            this.dataGridView1.Location = new System.Drawing.Point(13, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(420, 236);
            this.dataGridView1.TabIndex = 0;
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.Location = new System.Drawing.Point(158, 272);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 1;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = true;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.Location = new System.Drawing.Point(239, 272);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 1;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = true;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button上
            // 
            this.button上.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button上.Location = new System.Drawing.Point(439, 89);
            this.button上.Name = "button上";
            this.button上.Size = new System.Drawing.Size(42, 23);
            this.button上.TabIndex = 1;
            this.button上.Text = "△";
            this.button上.UseVisualStyleBackColor = true;
            this.button上.Click += new System.EventHandler(this.button上_Click);
            // 
            // button下
            // 
            this.button下.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button下.Location = new System.Drawing.Point(439, 118);
            this.button下.Name = "button下";
            this.button下.Size = new System.Drawing.Size(42, 23);
            this.button下.TabIndex = 1;
            this.button下.Text = "▽";
            this.button下.UseVisualStyleBackColor = true;
            this.button下.Click += new System.EventHandler(this.button下_Click);
            // 
            // 家族表示順設定Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(489, 310);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button下);
            this.Controls.Add(this.button上);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.dataGridView1);
            this.Name = "家族表示順設定Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "家族表示順設定";
            this.Load += new System.EventHandler(this.家族表示順設定Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button上;
        private System.Windows.Forms.Button button下;
    }
}