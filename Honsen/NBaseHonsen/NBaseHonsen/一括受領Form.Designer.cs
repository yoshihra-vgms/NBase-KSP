namespace NBaseHonsen
{
    partial class 一括受領Form
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancelButt = new System.Windows.Forms.Button();
            this.butt一括受領 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(768, 549);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // treeListView1
            // 
            this.treeListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.treeListView1.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView1.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView1.ContentPanel.Name = "";
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(756, 487);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Location = new System.Drawing.Point(3, 3);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(762, 493);
            this.treeListView1.TabIndex = 1;
            this.treeListView1.Text = "treeListView1";
            this.treeListView1.WatermarkImage = watermarkImage1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.cancelButt);
            this.panel1.Controls.Add(this.butt一括受領);
            this.panel1.Location = new System.Drawing.Point(3, 502);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(762, 44);
            this.panel1.TabIndex = 0;
            // 
            // cancelButt
            // 
            this.cancelButt.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cancelButt.BackColor = System.Drawing.SystemColors.Control;
            this.cancelButt.Location = new System.Drawing.Point(392, 3);
            this.cancelButt.Name = "cancelButt";
            this.cancelButt.Size = new System.Drawing.Size(170, 38);
            this.cancelButt.TabIndex = 5;
            this.cancelButt.Text = "閉じる";
            this.cancelButt.UseVisualStyleBackColor = false;
            this.cancelButt.Click += new System.EventHandler(this.cancelButt_Click);
            // 
            // butt一括受領
            // 
            this.butt一括受領.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.butt一括受領.BackColor = System.Drawing.SystemColors.Control;
            this.butt一括受領.Location = new System.Drawing.Point(201, 3);
            this.butt一括受領.Name = "butt一括受領";
            this.butt一括受領.Size = new System.Drawing.Size(170, 38);
            this.butt一括受領.TabIndex = 4;
            this.butt一括受領.Text = "一括受領";
            this.butt一括受領.UseVisualStyleBackColor = false;
            this.butt一括受領.Click += new System.EventHandler(this.butt一括受領_Click);
            // 
            // 一括受領Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "一括受領Form";
            this.Text = "NBaseHonsen - 一括受領";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.一括受領Form_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.一括受領Form_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
        private System.Windows.Forms.Button cancelButt;
        private System.Windows.Forms.Button butt一括受領;
    }
}