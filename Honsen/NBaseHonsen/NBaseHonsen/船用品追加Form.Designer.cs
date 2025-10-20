namespace NBaseHonsen
{
    partial class 船用品追加Form
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
            this.multiLineCombo詳細品目 = new NBaseUtil.MultiLineCombo();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox単位 = new System.Windows.Forms.ComboBox();
            this.textBox備考 = new System.Windows.Forms.TextBox();
            this.textBox在庫数 = new System.Windows.Forms.TextBox();
            this.textBox依頼数 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button追加 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxカテゴリ = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // multiLineCombo詳細品目
            // 
            this.multiLineCombo詳細品目.AutoSize = true;
            this.multiLineCombo詳細品目.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.multiLineCombo詳細品目.ImeMode = System.Windows.Forms.ImeMode.On;
            this.multiLineCombo詳細品目.Location = new System.Drawing.Point(195, 64);
            this.multiLineCombo詳細品目.Margin = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.multiLineCombo詳細品目.MaxLength = 500;
            this.multiLineCombo詳細品目.Name = "multiLineCombo詳細品目";
            this.multiLineCombo詳細品目.ReadOnly = false;
            this.multiLineCombo詳細品目.Size = new System.Drawing.Size(521, 75);
            this.multiLineCombo詳細品目.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(312, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "在庫数";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(510, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "依頼数";
            // 
            // comboBox単位
            // 
            this.comboBox単位.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox単位.FormattingEnabled = true;
            this.comboBox単位.Location = new System.Drawing.Point(195, 157);
            this.comboBox単位.Name = "comboBox単位";
            this.comboBox単位.Size = new System.Drawing.Size(77, 27);
            this.comboBox単位.TabIndex = 3;
            // 
            // textBox備考
            // 
            this.textBox備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox備考.Location = new System.Drawing.Point(195, 203);
            this.textBox備考.MaxLength = 500;
            this.textBox備考.Multiline = true;
            this.textBox備考.Name = "textBox備考";
            this.textBox備考.Size = new System.Drawing.Size(573, 76);
            this.textBox備考.TabIndex = 6;
            // 
            // textBox在庫数
            // 
            this.textBox在庫数.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox在庫数.Location = new System.Drawing.Point(392, 157);
            this.textBox在庫数.Name = "textBox在庫数";
            this.textBox在庫数.Size = new System.Drawing.Size(70, 26);
            this.textBox在庫数.TabIndex = 4;
            this.textBox在庫数.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox依頼数
            // 
            this.textBox依頼数.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox依頼数.Location = new System.Drawing.Point(592, 157);
            this.textBox依頼数.Name = "textBox依頼数";
            this.textBox依頼数.Size = new System.Drawing.Size(70, 26);
            this.textBox依頼数.TabIndex = 5;
            this.textBox依頼数.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "単位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 38);
            this.label3.TabIndex = 0;
            this.label3.Text = "備考\n（品名、規格等）";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "※詳細品目";
            // 
            // button追加
            // 
            this.button追加.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button追加.BackColor = System.Drawing.SystemColors.Control;
            this.button追加.Location = new System.Drawing.Point(249, 337);
            this.button追加.Name = "button追加";
            this.button追加.Size = new System.Drawing.Size(149, 38);
            this.button追加.TabIndex = 7;
            this.button追加.Text = "追加";
            this.button追加.UseVisualStyleBackColor = false;
            this.button追加.Click += new System.EventHandler(this.button追加_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(414, 337);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(149, 38);
            this.button閉じる.TabIndex = 8;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "※仕様・型式";
            // 
            // comboBoxカテゴリ
            // 
            this.comboBoxカテゴリ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxカテゴリ.FormattingEnabled = true;
            this.comboBoxカテゴリ.Location = new System.Drawing.Point(195, 19);
            this.comboBoxカテゴリ.Name = "comboBoxカテゴリ";
            this.comboBoxカテゴリ.Size = new System.Drawing.Size(521, 27);
            this.comboBoxカテゴリ.TabIndex = 1;
            // 
            // 船用品追加Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(813, 397);
            this.Controls.Add(this.comboBoxカテゴリ);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.multiLineCombo詳細品目);
            this.Controls.Add(this.button追加);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox単位);
            this.Controls.Add(this.textBox備考);
            this.Controls.Add(this.textBox在庫数);
            this.Controls.Add(this.textBox依頼数);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(829, 436);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(829, 436);
            this.Name = "船用品追加Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "詳細品目追加";
            this.Load += new System.EventHandler(this.船用品追加Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NBaseUtil.MultiLineCombo multiLineCombo詳細品目;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox単位;
        private System.Windows.Forms.TextBox textBox備考;
        private System.Windows.Forms.TextBox textBox在庫数;
        private System.Windows.Forms.TextBox textBox依頼数;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button追加;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxカテゴリ;

    }
}