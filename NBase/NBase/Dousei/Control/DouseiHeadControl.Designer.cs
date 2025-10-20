namespace Dousei.Control
{
    partial class DouseiHeadControl
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.DouseDate_dateTimePicker = new WingUtil.NullableDateTimePicker();
            this.Cargo2_comboBox = new System.Windows.Forms.ComboBox();
            this.Cargo1_comboBox = new System.Windows.Forms.ComboBox();
            this.label97 = new System.Windows.Forms.Label();
            this.Clear_button = new System.Windows.Forms.Button();
            this.Qtty2_TextBox = new System.Windows.Forms.TextBox();
            this.label95 = new System.Windows.Forms.Label();
            this.Qtty1_TextBox = new System.Windows.Forms.TextBox();
            this.label96 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.荷役開始_textBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.荷役終了_textBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.離桟_textBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.Basho_comboBox = new System.Windows.Forms.ComboBox();
            this.出港_textBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Kichi_comboBox = new System.Windows.Forms.ComboBox();
            this.入港_textBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.着桟_textBox = new System.Windows.Forms.TextBox();
            this.Group_label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.VoyageNo1_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DouseDate_dateTimePicker
            // 
            this.DouseDate_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DouseDate_dateTimePicker.Location = new System.Drawing.Point(51, 17);
            this.DouseDate_dateTimePicker.Name = "DouseDate_dateTimePicker";
            this.DouseDate_dateTimePicker.Size = new System.Drawing.Size(113, 19);
            this.DouseDate_dateTimePicker.TabIndex = 0;
            this.DouseDate_dateTimePicker.Value = null;
            // 
            // Cargo2_comboBox
            // 
            this.Cargo2_comboBox.FormattingEnabled = true;
            this.Cargo2_comboBox.Location = new System.Drawing.Point(51, 122);
            this.Cargo2_comboBox.Name = "Cargo2_comboBox";
            this.Cargo2_comboBox.Size = new System.Drawing.Size(113, 20);
            this.Cargo2_comboBox.TabIndex = 5;
            // 
            // Cargo1_comboBox
            // 
            this.Cargo1_comboBox.FormattingEnabled = true;
            this.Cargo1_comboBox.Location = new System.Drawing.Point(51, 80);
            this.Cargo1_comboBox.Name = "Cargo1_comboBox";
            this.Cargo1_comboBox.Size = new System.Drawing.Size(113, 20);
            this.Cargo1_comboBox.TabIndex = 3;
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(4, 125);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(37, 12);
            this.label97.TabIndex = 0;
            this.label97.Text = "貨物２";
            // 
            // Clear_button
            // 
            this.Clear_button.BackColor = System.Drawing.SystemColors.Control;
            this.Clear_button.Location = new System.Drawing.Point(230, 159);
            this.Clear_button.Name = "Clear_button";
            this.Clear_button.Size = new System.Drawing.Size(75, 23);
            this.Clear_button.TabIndex = 15;
            this.Clear_button.Text = "クリア";
            this.Clear_button.UseVisualStyleBackColor = true;
            this.Clear_button.Click += new System.EventHandler(this.Clear_button_Click);
            // 
            // Qtty2_TextBox
            // 
            this.Qtty2_TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Qtty2_TextBox.Location = new System.Drawing.Point(51, 144);
            this.Qtty2_TextBox.MaxLength = 10;
            this.Qtty2_TextBox.Name = "Qtty2_TextBox";
            this.Qtty2_TextBox.Size = new System.Drawing.Size(45, 19);
            this.Qtty2_TextBox.TabIndex = 6;
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(3, 83);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(49, 12);
            this.label95.TabIndex = 0;
            this.label95.Text = "※貨物１";
            // 
            // Qtty1_TextBox
            // 
            this.Qtty1_TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Qtty1_TextBox.Location = new System.Drawing.Point(51, 101);
            this.Qtty1_TextBox.MaxLength = 10;
            this.Qtty1_TextBox.Name = "Qtty1_TextBox";
            this.Qtty1_TextBox.Size = new System.Drawing.Size(45, 19);
            this.Qtty1_TextBox.TabIndex = 4;
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(4, 147);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(37, 12);
            this.label96.TabIndex = 0;
            this.label96.Text = "数量２";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "※日付";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(4, 104);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(49, 12);
            this.label94.TabIndex = 0;
            this.label94.Text = "※数量１";
            // 
            // 荷役開始_textBox
            // 
            this.荷役開始_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.荷役開始_textBox.Location = new System.Drawing.Point(228, 59);
            this.荷役開始_textBox.MaxLength = 4;
            this.荷役開始_textBox.Name = "荷役開始_textBox";
            this.荷役開始_textBox.Size = new System.Drawing.Size(77, 19);
            this.荷役開始_textBox.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(169, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "出港時間";
            // 
            // 荷役終了_textBox
            // 
            this.荷役終了_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.荷役終了_textBox.Location = new System.Drawing.Point(228, 80);
            this.荷役終了_textBox.MaxLength = 4;
            this.荷役終了_textBox.Name = "荷役終了_textBox";
            this.荷役終了_textBox.Size = new System.Drawing.Size(77, 19);
            this.荷役終了_textBox.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(169, 104);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "離桟時間";
            // 
            // 離桟_textBox
            // 
            this.離桟_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.離桟_textBox.Location = new System.Drawing.Point(228, 101);
            this.離桟_textBox.MaxLength = 4;
            this.離桟_textBox.Name = "離桟_textBox";
            this.離桟_textBox.Size = new System.Drawing.Size(77, 19);
            this.離桟_textBox.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "※港";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(169, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "荷役終了";
            // 
            // Basho_comboBox
            // 
            this.Basho_comboBox.FormattingEnabled = true;
            this.Basho_comboBox.Location = new System.Drawing.Point(51, 37);
            this.Basho_comboBox.Name = "Basho_comboBox";
            this.Basho_comboBox.Size = new System.Drawing.Size(113, 20);
            this.Basho_comboBox.TabIndex = 1;
            // 
            // 出港_textBox
            // 
            this.出港_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.出港_textBox.Location = new System.Drawing.Point(228, 122);
            this.出港_textBox.MaxLength = 4;
            this.出港_textBox.Name = "出港_textBox";
            this.出港_textBox.Size = new System.Drawing.Size(77, 19);
            this.出港_textBox.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "※基地";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(169, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "荷役開始";
            // 
            // Kichi_comboBox
            // 
            this.Kichi_comboBox.FormattingEnabled = true;
            this.Kichi_comboBox.Location = new System.Drawing.Point(51, 59);
            this.Kichi_comboBox.Name = "Kichi_comboBox";
            this.Kichi_comboBox.Size = new System.Drawing.Size(113, 20);
            this.Kichi_comboBox.TabIndex = 2;
            // 
            // 入港_textBox
            // 
            this.入港_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.入港_textBox.Location = new System.Drawing.Point(228, 17);
            this.入港_textBox.MaxLength = 4;
            this.入港_textBox.Name = "入港_textBox";
            this.入港_textBox.Size = new System.Drawing.Size(77, 19);
            this.入港_textBox.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(169, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "着桟時間";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(169, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "入港時間";
            // 
            // 着桟_textBox
            // 
            this.着桟_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.着桟_textBox.Location = new System.Drawing.Point(228, 38);
            this.着桟_textBox.MaxLength = 4;
            this.着桟_textBox.Name = "着桟_textBox";
            this.着桟_textBox.Size = new System.Drawing.Size(77, 19);
            this.着桟_textBox.TabIndex = 10;
            // 
            // Group_label
            // 
            this.Group_label.AutoSize = true;
            this.Group_label.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Group_label.Location = new System.Drawing.Point(6, 0);
            this.Group_label.Name = "Group_label";
            this.Group_label.Size = new System.Drawing.Size(35, 15);
            this.Group_label.TabIndex = 0;
            this.Group_label.Text = "label";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "※次航海番号";
            // 
            // VoyageNo1_textBox
            // 
            this.VoyageNo1_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.VoyageNo1_textBox.Location = new System.Drawing.Point(81, 165);
            this.VoyageNo1_textBox.MaxLength = 10;
            this.VoyageNo1_textBox.Name = "VoyageNo1_textBox";
            this.VoyageNo1_textBox.Size = new System.Drawing.Size(83, 19);
            this.VoyageNo1_textBox.TabIndex = 7;
            // 
            // DouseiHeadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.DouseDate_dateTimePicker);
            this.Controls.Add(this.Cargo2_comboBox);
            this.Controls.Add(this.Group_label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cargo1_comboBox);
            this.Controls.Add(this.着桟_textBox);
            this.Controls.Add(this.label97);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Clear_button);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.VoyageNo1_textBox);
            this.Controls.Add(this.Qtty2_TextBox);
            this.Controls.Add(this.入港_textBox);
            this.Controls.Add(this.label95);
            this.Controls.Add(this.Kichi_comboBox);
            this.Controls.Add(this.Qtty1_TextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label96);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.出港_textBox);
            this.Controls.Add(this.label94);
            this.Controls.Add(this.Basho_comboBox);
            this.Controls.Add(this.荷役開始_textBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.荷役終了_textBox);
            this.Controls.Add(this.離桟_textBox);
            this.Controls.Add(this.label12);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DouseiHeadControl";
            this.Size = new System.Drawing.Size(324, 204);
            this.Load += new System.EventHandler(this.DouseiHeadControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Cargo2_comboBox;
        private System.Windows.Forms.ComboBox Cargo1_comboBox;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Button Clear_button;
        private System.Windows.Forms.TextBox Qtty2_TextBox;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.TextBox Qtty1_TextBox;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.TextBox 荷役開始_textBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox 荷役終了_textBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox 離桟_textBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox Basho_comboBox;
        private System.Windows.Forms.TextBox 出港_textBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox Kichi_comboBox;
        private System.Windows.Forms.TextBox 入港_textBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox 着桟_textBox;
        private WingUtil.NullableDateTimePicker DouseDate_dateTimePicker;
        private System.Windows.Forms.Label Group_label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox VoyageNo1_textBox;


    }
}
