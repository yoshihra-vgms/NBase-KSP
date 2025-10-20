namespace Hachu.HachuManage
{
    partial class 特定品振替Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button変更 = new System.Windows.Forms.Button();
            this.textBox_selectedItem = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_category = new System.Windows.Forms.ComboBox();
            this.singleLineCombo1 = new NBaseUtil.SingleLineCombo();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "詳細品目";
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(196, 364);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 7;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button変更
            // 
            this.button変更.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button変更.BackColor = System.Drawing.SystemColors.Control;
            this.button変更.Location = new System.Drawing.Point(111, 364);
            this.button変更.Name = "button変更";
            this.button変更.Size = new System.Drawing.Size(75, 23);
            this.button変更.TabIndex = 6;
            this.button変更.Text = "変更";
            this.button変更.UseVisualStyleBackColor = false;
            this.button変更.Click += new System.EventHandler(this.button変更_Click);
            // 
            // textBox_selectedItem
            // 
            this.textBox_selectedItem.Location = new System.Drawing.Point(93, 25);
            this.textBox_selectedItem.Name = "textBox_selectedItem";
            this.textBox_selectedItem.ReadOnly = true;
            this.textBox_selectedItem.Size = new System.Drawing.Size(250, 19);
            this.textBox_selectedItem.TabIndex = 0;
            this.textBox_selectedItem.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "仕様・型式";
            // 
            // comboBox_category
            // 
            this.comboBox_category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_category.FormattingEnabled = true;
            this.comboBox_category.Location = new System.Drawing.Point(81, 18);
            this.comboBox_category.Name = "comboBox_category";
            this.comboBox_category.Size = new System.Drawing.Size(250, 20);
            this.comboBox_category.TabIndex = 10;
            this.comboBox_category.SelectedIndexChanged += new System.EventHandler(this.comboBox_category_SelectedIndexChanged);
            // 
            // singleLineCombo1
            // 
            this.singleLineCombo1.AutoSize = true;
            this.singleLineCombo1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.singleLineCombo1.Location = new System.Drawing.Point(81, 45);
            this.singleLineCombo1.MaxLength = 32767;
            this.singleLineCombo1.Name = "singleLineCombo1";
            this.singleLineCombo1.ReadOnly = false;
            this.singleLineCombo1.Size = new System.Drawing.Size(250, 19);
            this.singleLineCombo1.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "詳細品目";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox_category);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.singleLineCombo1);
            this.groupBox1.Location = new System.Drawing.Point(12, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 268);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "変更特定船用品";
            // 
            // 特定品振替Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(382, 399);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox_selectedItem);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button変更);
            this.Controls.Add(this.label1);
            this.Name = "特定品振替Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "特定品振替";
            this.Load += new System.EventHandler(this.特定品振替Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button変更;
        private System.Windows.Forms.TextBox textBox_selectedItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_category;
        private NBaseUtil.SingleLineCombo singleLineCombo1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}