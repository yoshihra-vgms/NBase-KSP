namespace Dousei.Control
{
    partial class DouseiDetailControl
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
            this.label25 = new System.Windows.Forms.Label();
            this.Clear_button = new System.Windows.Forms.Button();
            this.Qtty2_TextBox = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.Qtty1_TextBox = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.荷役開始_textBox = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.荷役終了_textBox = new System.Windows.Forms.TextBox();
            this.Group_label = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.離桟_textBox = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.Basho_comboBox = new System.Windows.Forms.ComboBox();
            this.出港_textBox = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.Kichi_comboBox = new System.Windows.Forms.ComboBox();
            this.入港_textBox = new System.Windows.Forms.TextBox();
            this.label103 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.着桟_textBox = new System.Windows.Forms.TextBox();
            this.VoyageNo1_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.Cargo2_comboBox.Location = new System.Drawing.Point(50, 123);
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
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(4, 128);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(37, 12);
            this.label25.TabIndex = 0;
            this.label25.Text = "貨物２";
            // 
            // Clear_button
            // 
            this.Clear_button.BackColor = System.Drawing.SystemColors.Control;
            this.Clear_button.Location = new System.Drawing.Point(230, 159);
            this.Clear_button.Name = "Clear_button";
            this.Clear_button.Size = new System.Drawing.Size(75, 23);
            this.Clear_button.TabIndex = 15;
            this.Clear_button.Text = "クリア";
            this.Clear_button.UseVisualStyleBackColor = false;
            this.Clear_button.Click += new System.EventHandler(this.Clear_button_Click);
            // 
            // Qtty2_TextBox
            // 
            this.Qtty2_TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Qtty2_TextBox.Location = new System.Drawing.Point(50, 146);
            this.Qtty2_TextBox.MaxLength = 10;
            this.Qtty2_TextBox.Name = "Qtty2_TextBox";
            this.Qtty2_TextBox.Size = new System.Drawing.Size(45, 19);
            this.Qtty2_TextBox.TabIndex = 6;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(3, 88);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(49, 12);
            this.label26.TabIndex = 0;
            this.label26.Text = "※貨物１";
            // 
            // Qtty1_TextBox
            // 
            this.Qtty1_TextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Qtty1_TextBox.Location = new System.Drawing.Point(51, 102);
            this.Qtty1_TextBox.MaxLength = 10;
            this.Qtty1_TextBox.Name = "Qtty1_TextBox";
            this.Qtty1_TextBox.Size = new System.Drawing.Size(45, 19);
            this.Qtty1_TextBox.TabIndex = 4;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(4, 150);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(37, 12);
            this.label27.TabIndex = 0;
            this.label27.Text = "数量２";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(4, 20);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(41, 12);
            this.label28.TabIndex = 0;
            this.label28.Text = "※日付";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(4, 108);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(49, 12);
            this.label29.TabIndex = 0;
            this.label29.Text = "※数量１";
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
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(169, 128);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(53, 12);
            this.label30.TabIndex = 0;
            this.label30.Text = "出港時間";
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
            // Group_label
            // 
            this.Group_label.AutoSize = true;
            this.Group_label.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Group_label.Location = new System.Drawing.Point(6, 0);
            this.Group_label.Name = "Group_label";
            this.Group_label.Size = new System.Drawing.Size(52, 15);
            this.Group_label.TabIndex = 0;
            this.Group_label.Text = "※区分";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(169, 108);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(53, 12);
            this.label32.TabIndex = 0;
            this.label32.Text = "離桟時間";
            // 
            // 離桟_textBox
            // 
            this.離桟_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.離桟_textBox.Location = new System.Drawing.Point(228, 102);
            this.離桟_textBox.MaxLength = 4;
            this.離桟_textBox.Name = "離桟_textBox";
            this.離桟_textBox.Size = new System.Drawing.Size(77, 19);
            this.離桟_textBox.TabIndex = 13;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(4, 41);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(29, 12);
            this.label33.TabIndex = 0;
            this.label33.Text = "※港";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(169, 85);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(53, 12);
            this.label34.TabIndex = 0;
            this.label34.Text = "荷役終了";
            // 
            // Basho_comboBox
            // 
            this.Basho_comboBox.FormattingEnabled = true;
            this.Basho_comboBox.Location = new System.Drawing.Point(51, 38);
            this.Basho_comboBox.Name = "Basho_comboBox";
            this.Basho_comboBox.Size = new System.Drawing.Size(113, 20);
            this.Basho_comboBox.TabIndex = 1;
            // 
            // 出港_textBox
            // 
            this.出港_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.出港_textBox.Location = new System.Drawing.Point(228, 124);
            this.出港_textBox.MaxLength = 4;
            this.出港_textBox.Name = "出港_textBox";
            this.出港_textBox.Size = new System.Drawing.Size(77, 19);
            this.出港_textBox.TabIndex = 14;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(3, 66);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(41, 12);
            this.label35.TabIndex = 0;
            this.label35.Text = "※基地";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(169, 62);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(53, 12);
            this.label102.TabIndex = 0;
            this.label102.Text = "荷役開始";
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
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(169, 41);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(53, 12);
            this.label103.TabIndex = 0;
            this.label103.Text = "着桟時間";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(169, 20);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(53, 12);
            this.label104.TabIndex = 0;
            this.label104.Text = "入港時間";
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
            // VoyageNo1_textBox
            // 
            this.VoyageNo1_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.VoyageNo1_textBox.Location = new System.Drawing.Point(80, 169);
            this.VoyageNo1_textBox.MaxLength = 10;
            this.VoyageNo1_textBox.Name = "VoyageNo1_textBox";
            this.VoyageNo1_textBox.Size = new System.Drawing.Size(83, 19);
            this.VoyageNo1_textBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "※次航海番号";
            // 
            // DouseiDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PeachPuff;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.VoyageNo1_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DouseDate_dateTimePicker);
            this.Controls.Add(this.Cargo2_comboBox);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.Cargo1_comboBox);
            this.Controls.Add(this.着桟_textBox);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label104);
            this.Controls.Add(this.Clear_button);
            this.Controls.Add(this.label103);
            this.Controls.Add(this.Qtty2_TextBox);
            this.Controls.Add(this.入港_textBox);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.Kichi_comboBox);
            this.Controls.Add(this.Qtty1_TextBox);
            this.Controls.Add(this.label102);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.出港_textBox);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.Basho_comboBox);
            this.Controls.Add(this.荷役開始_textBox);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.荷役終了_textBox);
            this.Controls.Add(this.離桟_textBox);
            this.Controls.Add(this.Group_label);
            this.Controls.Add(this.label32);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DouseiDetailControl";
            this.Size = new System.Drawing.Size(324, 204);
            this.Load += new System.EventHandler(this.DouseiDetailControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Cargo2_comboBox;
        private System.Windows.Forms.ComboBox Cargo1_comboBox;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button Clear_button;
        private System.Windows.Forms.TextBox Qtty2_TextBox;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox Qtty1_TextBox;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox 荷役開始_textBox;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox 荷役終了_textBox;
        private System.Windows.Forms.Label Group_label;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox 離桟_textBox;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox Basho_comboBox;
        private System.Windows.Forms.TextBox 出港_textBox;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.ComboBox Kichi_comboBox;
        private System.Windows.Forms.TextBox 入港_textBox;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.TextBox 着桟_textBox;
        private WingUtil.NullableDateTimePicker DouseDate_dateTimePicker;
        private System.Windows.Forms.TextBox VoyageNo1_textBox;
        private System.Windows.Forms.Label label2;

    }
}
