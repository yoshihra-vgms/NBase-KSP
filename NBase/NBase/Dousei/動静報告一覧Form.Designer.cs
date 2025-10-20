namespace Dousei
{
    partial class 動静報告一覧Form
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Close = new System.Windows.Forms.Button();
            this.button_Output = new System.Windows.Forms.Button();
            this.button検索 = new System.Windows.Forms.Button();
            this.button_New = new System.Windows.Forms.Button();
            this.dateTimePicker報告日 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.動静実績ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帳票CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.内航船舶輸送実績調査票ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.内航海運輸送実績調査票ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.エネルギー報告書ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.47216F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.52784F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 638);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Close);
            this.panel1.Controls.Add(this.button_Output);
            this.panel1.Controls.Add(this.button検索);
            this.panel1.Controls.Add(this.button_New);
            this.panel1.Controls.Add(this.dateTimePicker報告日);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1178, 73);
            this.panel1.TabIndex = 0;
            // 
            // button_Close
            // 
            this.button_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Close.BackColor = System.Drawing.SystemColors.Control;
            this.button_Close.Location = new System.Drawing.Point(1094, 13);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 15;
            this.button_Close.Text = "閉じる";
            this.button_Close.UseVisualStyleBackColor = false;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // button_Output
            // 
            this.button_Output.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Output.BackColor = System.Drawing.SystemColors.Control;
            this.button_Output.Location = new System.Drawing.Point(978, 13);
            this.button_Output.Name = "button_Output";
            this.button_Output.Size = new System.Drawing.Size(75, 23);
            this.button_Output.TabIndex = 14;
            this.button_Output.Text = "帳票作成";
            this.button_Output.UseVisualStyleBackColor = false;
            this.button_Output.Click += new System.EventHandler(this.button_Output_Click);
            // 
            // button検索
            // 
            this.button検索.BackColor = System.Drawing.SystemColors.Control;
            this.button検索.Location = new System.Drawing.Point(198, 13);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(75, 23);
            this.button検索.TabIndex = 13;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = false;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // button_New
            // 
            this.button_New.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_New.BackColor = System.Drawing.SystemColors.Control;
            this.button_New.Location = new System.Drawing.Point(897, 13);
            this.button_New.Name = "button_New";
            this.button_New.Size = new System.Drawing.Size(75, 23);
            this.button_New.TabIndex = 2;
            this.button_New.Text = "新規作成";
            this.button_New.UseVisualStyleBackColor = false;
            this.button_New.Click += new System.EventHandler(this.button_New_Click);
            // 
            // dateTimePicker報告日
            // 
            this.dateTimePicker報告日.Location = new System.Drawing.Point(65, 15);
            this.dateTimePicker報告日.Name = "dateTimePicker報告日";
            this.dateTimePicker報告日.Size = new System.Drawing.Size(127, 19);
            this.dateTimePicker報告日.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "報告日";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 82);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1178, 553);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.動静実績ToolStripMenuItem,
            this.帳票CToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 29;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 動静実績ToolStripMenuItem
            // 
            this.動静実績ToolStripMenuItem.Name = "動静実績ToolStripMenuItem";
            this.動静実績ToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.動静実績ToolStripMenuItem.Text = "動静実績";
            this.動静実績ToolStripMenuItem.Click += new System.EventHandler(this.動静実績ToolStripMenuItem_Click);
            // 
            // 帳票CToolStripMenuItem
            // 
            this.帳票CToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.内航船舶輸送実績調査票ToolStripMenuItem,
            this.内航海運輸送実績調査票ToolStripMenuItem,
            this.エネルギー報告書ToolStripMenuItem});
            this.帳票CToolStripMenuItem.Name = "帳票CToolStripMenuItem";
            this.帳票CToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.帳票CToolStripMenuItem.Text = "帳票";
            this.帳票CToolStripMenuItem.Visible = false;
            // 
            // 内航船舶輸送実績調査票ToolStripMenuItem
            // 
            this.内航船舶輸送実績調査票ToolStripMenuItem.Name = "内航船舶輸送実績調査票ToolStripMenuItem";
            this.内航船舶輸送実績調査票ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.内航船舶輸送実績調査票ToolStripMenuItem.Text = "内航船舶輸送実績調査票";
            this.内航船舶輸送実績調査票ToolStripMenuItem.Click += new System.EventHandler(this.内航船舶輸送実績調査票ToolStripMenuItem_Click);
            // 
            // 内航海運輸送実績調査票ToolStripMenuItem
            // 
            this.内航海運輸送実績調査票ToolStripMenuItem.Name = "内航海運輸送実績調査票ToolStripMenuItem";
            this.内航海運輸送実績調査票ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.内航海運輸送実績調査票ToolStripMenuItem.Text = "内航海運輸送実績調査票";
            this.内航海運輸送実績調査票ToolStripMenuItem.Click += new System.EventHandler(this.内航海運輸送実績調査票ToolStripMenuItem_Click);
            // 
            // エネルギー報告書ToolStripMenuItem
            // 
            this.エネルギー報告書ToolStripMenuItem.Name = "エネルギー報告書ToolStripMenuItem";
            this.エネルギー報告書ToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.エネルギー報告書ToolStripMenuItem.Text = "エネルギー報告書";
            this.エネルギー報告書ToolStripMenuItem.Click += new System.EventHandler(this.エネルギー報告書ToolStripMenuItem_Click);
            // 
            // 動静報告一覧Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 662);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "動静報告一覧Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "動静報告一覧Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.動静報告一覧Form_FormClosing);
            this.Load += new System.EventHandler(this.動静報告一覧Form_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_New;
        private System.Windows.Forms.DateTimePicker dateTimePicker報告日;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.Button button_Output;
        private System.Windows.Forms.Button button_Close;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 動静実績ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帳票CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 内航船舶輸送実績調査票ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 内航海運輸送実績調査票ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem エネルギー報告書ToolStripMenuItem;
    }
}