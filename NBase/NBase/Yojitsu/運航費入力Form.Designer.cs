namespace Yojitsu
{
    partial class 運航費入力Form
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox稼働情報 = new System.Windows.Forms.TextBox();
            this.textBox貨物 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.YenControl = new Yojitsu.運航費入力Control();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DollarControl = new Yojitsu.運航費入力Control();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.TotalControl = new Yojitsu.運航費入力Control();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxコピー = new System.Windows.Forms.CheckBox();
            this.butt閉じる = new System.Windows.Forms.Button();
            this.button設定 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox月 = new System.Windows.Forms.ComboBox();
            this.comboBox年 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 7);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1007, 696);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox稼働情報);
            this.panel1.Controls.Add(this.textBox貨物);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1001, 29);
            this.panel1.TabIndex = 0;
            // 
            // textBox稼働情報
            // 
            this.textBox稼働情報.Location = new System.Drawing.Point(640, 6);
            this.textBox稼働情報.Name = "textBox稼働情報";
            this.textBox稼働情報.ReadOnly = true;
            this.textBox稼働情報.Size = new System.Drawing.Size(351, 19);
            this.textBox稼働情報.TabIndex = 5;
            this.textBox稼働情報.TabStop = false;
            // 
            // textBox貨物
            // 
            this.textBox貨物.Location = new System.Drawing.Point(535, 6);
            this.textBox貨物.Name = "textBox貨物";
            this.textBox貨物.ReadOnly = true;
            this.textBox貨物.Size = new System.Drawing.Size(99, 19);
            this.textBox貨物.TabIndex = 0;
            this.textBox貨物.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(500, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "貨物";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 64);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1001, 599);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.YenControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(993, 573);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "円";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // YenControl
            // 
            this.YenControl.BackColor = System.Drawing.Color.White;
            this.YenControl.Location = new System.Drawing.Point(0, 0);
            this.YenControl.Name = "YenControl";
            this.YenControl.Size = new System.Drawing.Size(987, 568);
            this.YenControl.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.DollarControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(993, 573);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ドル";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DollarControl
            // 
            this.DollarControl.BackColor = System.Drawing.Color.White;
            this.DollarControl.Location = new System.Drawing.Point(0, 0);
            this.DollarControl.Name = "DollarControl";
            this.DollarControl.Size = new System.Drawing.Size(987, 568);
            this.DollarControl.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.TotalControl);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(993, 573);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "合計";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // TotalControl
            // 
            this.TotalControl.BackColor = System.Drawing.Color.White;
            this.TotalControl.Location = new System.Drawing.Point(0, 0);
            this.TotalControl.Name = "TotalControl";
            this.TotalControl.Size = new System.Drawing.Size(987, 568);
            this.TotalControl.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.checkBoxコピー);
            this.panel2.Controls.Add(this.butt閉じる);
            this.panel2.Controls.Add(this.button設定);
            this.panel2.Location = new System.Drawing.Point(16, 669);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(988, 24);
            this.panel2.TabIndex = 9;
            // 
            // checkBoxコピー
            // 
            this.checkBoxコピー.AutoSize = true;
            this.checkBoxコピー.Location = new System.Drawing.Point(5, 4);
            this.checkBoxコピー.Name = "checkBoxコピー";
            this.checkBoxコピー.Size = new System.Drawing.Size(127, 16);
            this.checkBoxコピー.TabIndex = 35;
            this.checkBoxコピー.Text = "次月以降にコピーする";
            this.checkBoxコピー.UseVisualStyleBackColor = true;
            // 
            // butt閉じる
            // 
            this.butt閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.butt閉じる.Location = new System.Drawing.Point(887, 0);
            this.butt閉じる.Name = "butt閉じる";
            this.butt閉じる.Size = new System.Drawing.Size(100, 23);
            this.butt閉じる.TabIndex = 37;
            this.butt閉じる.Text = "閉じる";
            this.butt閉じる.UseVisualStyleBackColor = false;
            this.butt閉じる.Click += new System.EventHandler(this.butt閉じる_Click);
            // 
            // button設定
            // 
            this.button設定.BackColor = System.Drawing.SystemColors.Control;
            this.button設定.Location = new System.Drawing.Point(781, 0);
            this.button設定.Name = "button設定";
            this.button設定.Size = new System.Drawing.Size(100, 23);
            this.button設定.TabIndex = 36;
            this.button設定.Text = "設定";
            this.button設定.UseVisualStyleBackColor = false;
            this.button設定.Click += new System.EventHandler(this.button設定_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.comboBox月);
            this.panel3.Controls.Add(this.comboBox年);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 32);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1001, 29);
            this.panel3.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(950, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "単位：円";
            // 
            // comboBox月
            // 
            this.comboBox月.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox月.FormattingEnabled = true;
            this.comboBox月.Location = new System.Drawing.Point(114, 5);
            this.comboBox月.Name = "comboBox月";
            this.comboBox月.Size = new System.Drawing.Size(38, 20);
            this.comboBox月.TabIndex = 3;
            // 
            // comboBox年
            // 
            this.comboBox年.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年.FormattingEnabled = true;
            this.comboBox年.Location = new System.Drawing.Point(42, 5);
            this.comboBox年.Name = "comboBox年";
            this.comboBox年.Size = new System.Drawing.Size(61, 20);
            this.comboBox年.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "年月";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(103, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "/";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // 運航費入力Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1023, 709);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "運航費入力Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "運航費入力Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.運航費入力Form_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button butt閉じる;
        private System.Windows.Forms.Button button設定;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox comboBox月;
        private System.Windows.Forms.ComboBox comboBox年;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxコピー;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private 運航費入力Control DollarControl;
        private 運航費入力Control YenControl;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage tabPage3;
        private 運航費入力Control TotalControl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox貨物;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox稼働情報;
    }
}