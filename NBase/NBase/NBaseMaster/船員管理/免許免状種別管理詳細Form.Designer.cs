namespace NBaseMaster.船員管理
{
    partial class 免許免状種別管理詳細Form
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
            this.button閉じる = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.button更新 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox略称 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox表示順序 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox種別名 = new System.Windows.Forms.TextBox();
            this.comboBox免許免状名 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox未取得除外対象 = new System.Windows.Forms.ListBox();
            this.button未取得除外対象参照 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(215, 292);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 7;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button削除
            // 
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(134, 292);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 6;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button更新
            // 
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(53, 292);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 5;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "※免許／免状名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "略称";
            // 
            // textBox略称
            // 
            this.textBox略称.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox略称.Location = new System.Drawing.Point(128, 68);
            this.textBox略称.MaxLength = 20;
            this.textBox略称.Name = "textBox略称";
            this.textBox略称.Size = new System.Drawing.Size(116, 19);
            this.textBox略称.TabIndex = 3;
            this.textBox略称.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 26;
            this.label3.Text = "表示順序";
            // 
            // textBox表示順序
            // 
            this.textBox表示順序.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox表示順序.Location = new System.Drawing.Point(128, 93);
            this.textBox表示順序.MaxLength = 3;
            this.textBox表示順序.Name = "textBox表示順序";
            this.textBox表示順序.Size = new System.Drawing.Size(39, 19);
            this.textBox表示順序.TabIndex = 4;
            this.textBox表示順序.Text = "0";
            this.textBox表示順序.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox表示順序.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "※種別名";
            // 
            // textBox種別名
            // 
            this.textBox種別名.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox種別名.Location = new System.Drawing.Point(128, 43);
            this.textBox種別名.MaxLength = 20;
            this.textBox種別名.Name = "textBox種別名";
            this.textBox種別名.Size = new System.Drawing.Size(167, 19);
            this.textBox種別名.TabIndex = 2;
            this.textBox種別名.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // comboBox免許免状名
            // 
            this.comboBox免許免状名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox免許免状名.FormattingEnabled = true;
            this.comboBox免許免状名.Location = new System.Drawing.Point(128, 17);
            this.comboBox免許免状名.Name = "comboBox免許免状名";
            this.comboBox免許免状名.Size = new System.Drawing.Size(184, 20);
            this.comboBox免許免状名.TabIndex = 1;
            this.comboBox免許免状名.SelectedIndexChanged += new System.EventHandler(this.comboBox免許免状名_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "未取得除外対象";
            // 
            // listBox未取得除外対象
            // 
            this.listBox未取得除外対象.FormattingEnabled = true;
            this.listBox未取得除外対象.ItemHeight = 12;
            this.listBox未取得除外対象.Location = new System.Drawing.Point(128, 121);
            this.listBox未取得除外対象.Name = "listBox未取得除外対象";
            this.listBox未取得除外対象.ScrollAlwaysVisible = true;
            this.listBox未取得除外対象.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox未取得除外対象.Size = new System.Drawing.Size(184, 136);
            this.listBox未取得除外対象.TabIndex = 27;
            // 
            // button未取得除外対象参照
            // 
            this.button未取得除外対象参照.BackColor = System.Drawing.SystemColors.Control;
            this.button未取得除外対象参照.Location = new System.Drawing.Point(29, 141);
            this.button未取得除外対象参照.Name = "button未取得除外対象参照";
            this.button未取得除外対象参照.Size = new System.Drawing.Size(75, 23);
            this.button未取得除外対象参照.TabIndex = 28;
            this.button未取得除外対象参照.Text = "参照";
            this.button未取得除外対象参照.UseVisualStyleBackColor = false;
            this.button未取得除外対象参照.Click += new System.EventHandler(this.button未取得除外対象参照_Click);
            // 
            // 免許免状種別管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(343, 327);
            this.Controls.Add(this.button未取得除外対象参照);
            this.Controls.Add(this.listBox未取得除外対象);
            this.Controls.Add(this.comboBox免許免状名);
            this.Controls.Add(this.textBox表示順序);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.textBox略称);
            this.Controls.Add(this.textBox種別名);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "免許免状種別管理詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "免許免状種別管理詳細Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox略称;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox表示順序;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox種別名;
        private System.Windows.Forms.ComboBox comboBox免許免状名;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox未取得除外対象;
        private System.Windows.Forms.Button button未取得除外対象参照;
    }
}