namespace Dousei
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.Renkei_checkBox = new System.Windows.Forms.CheckBox();
            this.Vessel_comboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.To_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.From_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.Clear_button = new System.Windows.Forms.Button();
            this.Search_button = new System.Windows.Forms.Button();
            this.KikanRenkei_button = new System.Windows.Forms.Button();
            this.treeListView1 = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateNew_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_荷主 = new System.Windows.Forms.TextBox();
            this.button_荷主 = new System.Windows.Forms.Button();
            this.textBox_代理店 = new System.Windows.Forms.TextBox();
            this.button_代理店 = new System.Windows.Forms.Button();
            this.textBox_基地 = new System.Windows.Forms.TextBox();
            this.button_基地 = new System.Windows.Forms.Button();
            this.textBox_場所 = new System.Windows.Forms.TextBox();
            this.button_場所 = new System.Windows.Forms.Button();
            this.textBox_貨物 = new System.Windows.Forms.TextBox();
            this.button_貨物 = new System.Windows.Forms.Button();
            this.Main_tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.Main_tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 27;
            this.label1.Text = "船名";
            // 
            // Renkei_checkBox
            // 
            this.Renkei_checkBox.AutoSize = true;
            this.Renkei_checkBox.Location = new System.Drawing.Point(402, 44);
            this.Renkei_checkBox.Name = "Renkei_checkBox";
            this.Renkei_checkBox.Size = new System.Drawing.Size(95, 16);
            this.Renkei_checkBox.TabIndex = 25;
            this.Renkei_checkBox.Text = "連携済み表示";
            this.Renkei_checkBox.UseVisualStyleBackColor = true;
            // 
            // Vessel_comboBox
            // 
            this.Vessel_comboBox.FormattingEnabled = true;
            this.Vessel_comboBox.Location = new System.Drawing.Point(96, 14);
            this.Vessel_comboBox.Name = "Vessel_comboBox";
            this.Vessel_comboBox.Size = new System.Drawing.Size(168, 20);
            this.Vessel_comboBox.TabIndex = 26;
            this.Vessel_comboBox.Text = "NORTH PIONEER";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 24;
            this.label3.Text = "～";
            // 
            // To_dateTimePicker
            // 
            this.To_dateTimePicker.Location = new System.Drawing.Point(235, 42);
            this.To_dateTimePicker.Name = "To_dateTimePicker";
            this.To_dateTimePicker.Size = new System.Drawing.Size(111, 19);
            this.To_dateTimePicker.TabIndex = 22;
            this.To_dateTimePicker.Value = new System.DateTime(2009, 8, 31, 0, 0, 0, 0);
            // 
            // From_dateTimePicker
            // 
            this.From_dateTimePicker.Location = new System.Drawing.Point(96, 41);
            this.From_dateTimePicker.Name = "From_dateTimePicker";
            this.From_dateTimePicker.Size = new System.Drawing.Size(111, 19);
            this.From_dateTimePicker.TabIndex = 23;
            this.From_dateTimePicker.Value = new System.DateTime(2009, 8, 1, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "日付";
            // 
            // Clear_button
            // 
            this.Clear_button.BackColor = System.Drawing.SystemColors.Control;
            this.Clear_button.Enabled = false;
            this.Clear_button.Location = new System.Drawing.Point(665, 38);
            this.Clear_button.Name = "Clear_button";
            this.Clear_button.Size = new System.Drawing.Size(92, 23);
            this.Clear_button.TabIndex = 19;
            this.Clear_button.Text = "検索条件クリア";
            this.Clear_button.UseVisualStyleBackColor = false;
            this.Clear_button.Click += new System.EventHandler(this.Clear_button_Click);
            // 
            // Search_button
            // 
            this.Search_button.BackColor = System.Drawing.SystemColors.Control;
            this.Search_button.Enabled = false;
            this.Search_button.Location = new System.Drawing.Point(665, 12);
            this.Search_button.Name = "Search_button";
            this.Search_button.Size = new System.Drawing.Size(92, 23);
            this.Search_button.TabIndex = 20;
            this.Search_button.Text = "検索";
            this.Search_button.UseVisualStyleBackColor = false;
            this.Search_button.Click += new System.EventHandler(this.Search_button_Click);
            // 
            // KikanRenkei_button
            // 
            this.KikanRenkei_button.BackColor = System.Drawing.SystemColors.Control;
            this.KikanRenkei_button.Enabled = false;
            this.KikanRenkei_button.Location = new System.Drawing.Point(873, 12);
            this.KikanRenkei_button.Name = "KikanRenkei_button";
            this.KikanRenkei_button.Size = new System.Drawing.Size(104, 32);
            this.KikanRenkei_button.TabIndex = 18;
            this.KikanRenkei_button.Text = "基幹連携";
            this.KikanRenkei_button.UseVisualStyleBackColor = false;
            this.KikanRenkei_button.Visible = false;
            this.KikanRenkei_button.Click += new System.EventHandler(this.KikanRenkei_button_Click);
            // 
            // treeListView1
            // 
            // 
            // 
            // 
            this.treeListView1.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView1.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView1.ContentPanel.Name = "";
            this.treeListView1.ContentPanel.Size = new System.Drawing.Size(996, 418);
            this.treeListView1.ContentPanel.TabIndex = 3;
            this.treeListView1.ContentPanel.TabStop = false;
            this.treeListView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView1.Location = new System.Drawing.Point(3, 234);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.Size = new System.Drawing.Size(1002, 424);
            this.treeListView1.TabIndex = 29;
            this.treeListView1.Text = "treeListView1";
            this.treeListView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeListView1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 26);
            this.contextMenuStrip1.Text = "区分編集";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.toolStripMenuItem1.Text = "区分編集";
            // 
            // CreateNew_button
            // 
            this.CreateNew_button.BackColor = System.Drawing.SystemColors.Control;
            this.CreateNew_button.Enabled = false;
            this.CreateNew_button.Location = new System.Drawing.Point(873, 175);
            this.CreateNew_button.Name = "CreateNew_button";
            this.CreateNew_button.Size = new System.Drawing.Size(104, 32);
            this.CreateNew_button.TabIndex = 18;
            this.CreateNew_button.Text = "新規動静追加";
            this.CreateNew_button.UseVisualStyleBackColor = false;
            this.CreateNew_button.Click += new System.EventHandler(this.CreateNew_button_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox_荷主);
            this.panel1.Controls.Add(this.button_荷主);
            this.panel1.Controls.Add(this.textBox_代理店);
            this.panel1.Controls.Add(this.button_代理店);
            this.panel1.Controls.Add(this.textBox_基地);
            this.panel1.Controls.Add(this.button_基地);
            this.panel1.Controls.Add(this.textBox_場所);
            this.panel1.Controls.Add(this.button_場所);
            this.panel1.Controls.Add(this.textBox_貨物);
            this.panel1.Controls.Add(this.button_貨物);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.KikanRenkei_button);
            this.panel1.Controls.Add(this.CreateNew_button);
            this.panel1.Controls.Add(this.Search_button);
            this.panel1.Controls.Add(this.Clear_button);
            this.panel1.Controls.Add(this.Renkei_checkBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.Vessel_comboBox);
            this.panel1.Controls.Add(this.From_dateTimePicker);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.To_dateTimePicker);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1002, 225);
            this.panel1.TabIndex = 31;
            // 
            // textBox_荷主
            // 
            this.textBox_荷主.Location = new System.Drawing.Point(96, 184);
            this.textBox_荷主.Multiline = true;
            this.textBox_荷主.Name = "textBox_荷主";
            this.textBox_荷主.ReadOnly = true;
            this.textBox_荷主.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_荷主.Size = new System.Drawing.Size(661, 23);
            this.textBox_荷主.TabIndex = 37;
            // 
            // button_荷主
            // 
            this.button_荷主.Location = new System.Drawing.Point(14, 184);
            this.button_荷主.Name = "button_荷主";
            this.button_荷主.Size = new System.Drawing.Size(75, 23);
            this.button_荷主.TabIndex = 36;
            this.button_荷主.TabStop = false;
            this.button_荷主.Text = "荷主";
            this.button_荷主.UseVisualStyleBackColor = true;
            this.button_荷主.Click += new System.EventHandler(this.button_荷主_Click);
            // 
            // textBox_代理店
            // 
            this.textBox_代理店.Location = new System.Drawing.Point(96, 155);
            this.textBox_代理店.Multiline = true;
            this.textBox_代理店.Name = "textBox_代理店";
            this.textBox_代理店.ReadOnly = true;
            this.textBox_代理店.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_代理店.Size = new System.Drawing.Size(661, 23);
            this.textBox_代理店.TabIndex = 35;
            // 
            // button_代理店
            // 
            this.button_代理店.Location = new System.Drawing.Point(14, 155);
            this.button_代理店.Name = "button_代理店";
            this.button_代理店.Size = new System.Drawing.Size(75, 23);
            this.button_代理店.TabIndex = 34;
            this.button_代理店.Text = "代理店";
            this.button_代理店.UseVisualStyleBackColor = true;
            this.button_代理店.Click += new System.EventHandler(this.button_代理店_Click);
            // 
            // textBox_基地
            // 
            this.textBox_基地.Location = new System.Drawing.Point(96, 126);
            this.textBox_基地.Multiline = true;
            this.textBox_基地.Name = "textBox_基地";
            this.textBox_基地.ReadOnly = true;
            this.textBox_基地.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_基地.Size = new System.Drawing.Size(661, 23);
            this.textBox_基地.TabIndex = 33;
            // 
            // button_基地
            // 
            this.button_基地.Location = new System.Drawing.Point(14, 126);
            this.button_基地.Name = "button_基地";
            this.button_基地.Size = new System.Drawing.Size(75, 23);
            this.button_基地.TabIndex = 32;
            this.button_基地.Text = "基地";
            this.button_基地.UseVisualStyleBackColor = true;
            this.button_基地.Click += new System.EventHandler(this.button_基地_Click);
            // 
            // textBox_場所
            // 
            this.textBox_場所.Location = new System.Drawing.Point(96, 97);
            this.textBox_場所.Multiline = true;
            this.textBox_場所.Name = "textBox_場所";
            this.textBox_場所.ReadOnly = true;
            this.textBox_場所.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_場所.Size = new System.Drawing.Size(661, 23);
            this.textBox_場所.TabIndex = 31;
            // 
            // button_場所
            // 
            this.button_場所.Location = new System.Drawing.Point(14, 97);
            this.button_場所.Name = "button_場所";
            this.button_場所.Size = new System.Drawing.Size(75, 23);
            this.button_場所.TabIndex = 30;
            this.button_場所.Text = "場所";
            this.button_場所.UseVisualStyleBackColor = true;
            this.button_場所.Click += new System.EventHandler(this.button_場所_Click);
            // 
            // textBox_貨物
            // 
            this.textBox_貨物.Location = new System.Drawing.Point(96, 68);
            this.textBox_貨物.Multiline = true;
            this.textBox_貨物.Name = "textBox_貨物";
            this.textBox_貨物.ReadOnly = true;
            this.textBox_貨物.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_貨物.Size = new System.Drawing.Size(661, 23);
            this.textBox_貨物.TabIndex = 29;
            // 
            // button_貨物
            // 
            this.button_貨物.Location = new System.Drawing.Point(14, 68);
            this.button_貨物.Name = "button_貨物";
            this.button_貨物.Size = new System.Drawing.Size(75, 23);
            this.button_貨物.TabIndex = 28;
            this.button_貨物.Text = "貨物";
            this.button_貨物.UseVisualStyleBackColor = true;
            this.button_貨物.Click += new System.EventHandler(this.button_貨物_Click);
            // 
            // Main_tableLayoutPanel
            // 
            this.Main_tableLayoutPanel.ColumnCount = 1;
            this.Main_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Main_tableLayoutPanel.Controls.Add(this.panel1, 0, 0);
            this.Main_tableLayoutPanel.Controls.Add(this.treeListView1, 0, 1);
            this.Main_tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Main_tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.Main_tableLayoutPanel.Name = "Main_tableLayoutPanel";
            this.Main_tableLayoutPanel.RowCount = 2;
            this.Main_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 231F));
            this.Main_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Main_tableLayoutPanel.Size = new System.Drawing.Size(1008, 661);
            this.Main_tableLayoutPanel.TabIndex = 32;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 661);
            this.Controls.Add(this.Main_tableLayoutPanel);
            this.Name = "MainForm";
            this.Text = "動静実績";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Main_tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Renkei_checkBox;
        private System.Windows.Forms.ComboBox Vessel_comboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker To_dateTimePicker;
        private System.Windows.Forms.DateTimePicker From_dateTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Clear_button;
        private System.Windows.Forms.Button Search_button;
        private System.Windows.Forms.Button KikanRenkei_button;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView1;
        private System.Windows.Forms.Button CreateNew_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel Main_tableLayoutPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TextBox textBox_荷主;
        private System.Windows.Forms.Button button_荷主;
        private System.Windows.Forms.TextBox textBox_代理店;
        private System.Windows.Forms.Button button_代理店;
        private System.Windows.Forms.TextBox textBox_基地;
        private System.Windows.Forms.Button button_基地;
        private System.Windows.Forms.TextBox textBox_場所;
        private System.Windows.Forms.Button button_場所;
        private System.Windows.Forms.TextBox textBox_貨物;
        private System.Windows.Forms.Button button_貨物;
    }
}