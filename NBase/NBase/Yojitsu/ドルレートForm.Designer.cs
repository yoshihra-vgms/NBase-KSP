namespace Yojitsu
{
    partial class ドルレートForm
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
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.button保存 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.labelYosanHead = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeListView1
            // 
            // 
            // 
            // 
            this.treeListView1.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView1.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView1.ContentPanel.Name = "";
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(265, 352);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Footer = false;
            this.treeListView1.Location = new System.Drawing.Point(9, 26);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(271, 358);
            this.treeListView1.TabIndex = 0;
            this.treeListView1.Text = "treeListView1";
            this.treeListView1.WatermarkImage = watermarkImage1;
            // 
            // button保存
            // 
            this.button保存.BackColor = System.Drawing.SystemColors.Control;
            this.button保存.Location = new System.Drawing.Point(66, 390);
            this.button保存.Name = "button保存";
            this.button保存.Size = new System.Drawing.Size(75, 23);
            this.button保存.TabIndex = 1;
            this.button保存.Text = "保存";
            this.button保存.UseVisualStyleBackColor = false;
            this.button保存.Click += new System.EventHandler(this.button保存_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(147, 390);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 2;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // labelYosanHead
            // 
            this.labelYosanHead.AutoSize = true;
            this.labelYosanHead.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelYosanHead.Location = new System.Drawing.Point(4, 9);
            this.labelYosanHead.Name = "labelYosanHead";
            this.labelYosanHead.Size = new System.Drawing.Size(0, 12);
            this.labelYosanHead.TabIndex = 0;
            // 
            // ドルレートForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(289, 425);
            this.Controls.Add(this.labelYosanHead);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button保存);
            this.Controls.Add(this.treeListView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ドルレートForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ドルレートForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ドルレートForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
        private System.Windows.Forms.Button button保存;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Label labelYosanHead;
    }
}