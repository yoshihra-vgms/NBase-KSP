namespace NBaseCommon
{
    partial class DouseiYoteiUserControl
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
            this.button_Clear = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.textBox_出港 = new System.Windows.Forms.TextBox();
            this.comboBox_基地 = new System.Windows.Forms.ComboBox();
            this.comboBox_港 = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel_Tumini = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox_着桟 = new System.Windows.Forms.TextBox();
            this.textBox_荷役開始 = new System.Windows.Forms.TextBox();
            this.textBox_離桟 = new System.Windows.Forms.TextBox();
            this.textBox_入港 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_Base = new System.Windows.Forms.Panel();
            this.singleLineCombo_代理店 = new NBaseUtil.SingleLineCombo();
            this.singleLineCombo_荷主 = new NBaseUtil.SingleLineCombo();
            this.textBox_備考 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label_Header = new System.Windows.Forms.Label();
            this.dateTimePicker = new NBaseUtil.NullableDateTimePicker();
            this.singleLineCombo_港 = new NBaseUtil.SingleLineCombo();
            this.panel_Base.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(162, 277);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(75, 23);
            this.button_Clear.TabIndex = 13;
            this.button_Clear.Text = "クリア";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(262, 185);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(75, 23);
            this.button_Add.TabIndex = 12;
            this.button_Add.Text = "積荷追加";
            this.button_Add.UseVisualStyleBackColor = false;
            this.button_Add.Visible = false;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // textBox_出港
            // 
            this.textBox_出港.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_出港.Location = new System.Drawing.Point(315, 93);
            this.textBox_出港.MaxLength = 4;
            this.textBox_出港.Name = "textBox_出港";
            this.textBox_出港.Size = new System.Drawing.Size(77, 19);
            this.textBox_出港.TabIndex = 7;
            // 
            // comboBox_基地
            // 
            this.comboBox_基地.FormattingEnabled = true;
            this.comboBox_基地.Location = new System.Drawing.Point(101, 70);
            this.comboBox_基地.Name = "comboBox_基地";
            this.comboBox_基地.Size = new System.Drawing.Size(125, 20);
            this.comboBox_基地.TabIndex = 2;
            // 
            // comboBox_港
            // 
            this.comboBox_港.FormattingEnabled = true;
            this.comboBox_港.Location = new System.Drawing.Point(267, 234);
            this.comboBox_港.Name = "comboBox_港";
            this.comboBox_港.Size = new System.Drawing.Size(125, 20);
            this.comboBox_港.TabIndex = 1;
            this.comboBox_港.Visible = false;
            // 
            // flowLayoutPanel_Tumini
            // 
            this.flowLayoutPanel_Tumini.AutoScroll = true;
            this.flowLayoutPanel_Tumini.Location = new System.Drawing.Point(7, 183);
            this.flowLayoutPanel_Tumini.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel_Tumini.Name = "flowLayoutPanel_Tumini";
            this.flowLayoutPanel_Tumini.Size = new System.Drawing.Size(245, 90);
            this.flowLayoutPanel_Tumini.TabIndex = 11;
            // 
            // textBox_着桟
            // 
            this.textBox_着桟.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_着桟.Location = new System.Drawing.Point(315, 49);
            this.textBox_着桟.MaxLength = 4;
            this.textBox_着桟.Name = "textBox_着桟";
            this.textBox_着桟.Size = new System.Drawing.Size(77, 19);
            this.textBox_着桟.TabIndex = 5;
            // 
            // textBox_荷役開始
            // 
            this.textBox_荷役開始.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_荷役開始.Location = new System.Drawing.Point(128, 93);
            this.textBox_荷役開始.MaxLength = 4;
            this.textBox_荷役開始.Name = "textBox_荷役開始";
            this.textBox_荷役開始.Size = new System.Drawing.Size(77, 19);
            this.textBox_荷役開始.TabIndex = 3;
            // 
            // textBox_離桟
            // 
            this.textBox_離桟.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_離桟.Location = new System.Drawing.Point(315, 71);
            this.textBox_離桟.MaxLength = 4;
            this.textBox_離桟.Name = "textBox_離桟";
            this.textBox_離桟.Size = new System.Drawing.Size(77, 19);
            this.textBox_離桟.TabIndex = 6;
            // 
            // textBox_入港
            // 
            this.textBox_入港.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_入港.Location = new System.Drawing.Point(315, 27);
            this.textBox_入港.MaxLength = 4;
            this.textBox_入港.Name = "textBox_入港";
            this.textBox_入港.Size = new System.Drawing.Size(77, 19);
            this.textBox_入港.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(216, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "　  出港予定時刻";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(216, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "　  離桟予定時刻";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "　　荷役開始予定時刻";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(216, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "　  着桟予定時刻";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(216, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "　  入港予定時刻";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "　　基地";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "※ 港";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "※ 日付";
            // 
            // panel_Base
            // 
            this.panel_Base.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Base.Controls.Add(this.singleLineCombo_港);
            this.panel_Base.Controls.Add(this.singleLineCombo_代理店);
            this.panel_Base.Controls.Add(this.singleLineCombo_荷主);
            this.panel_Base.Controls.Add(this.textBox_備考);
            this.panel_Base.Controls.Add(this.label11);
            this.panel_Base.Controls.Add(this.label10);
            this.panel_Base.Controls.Add(this.label9);
            this.panel_Base.Controls.Add(this.label_Header);
            this.panel_Base.Controls.Add(this.button_Clear);
            this.panel_Base.Controls.Add(this.button_Add);
            this.panel_Base.Controls.Add(this.label1);
            this.panel_Base.Controls.Add(this.textBox_出港);
            this.panel_Base.Controls.Add(this.label2);
            this.panel_Base.Controls.Add(this.dateTimePicker);
            this.panel_Base.Controls.Add(this.label3);
            this.panel_Base.Controls.Add(this.comboBox_基地);
            this.panel_Base.Controls.Add(this.comboBox_港);
            this.panel_Base.Controls.Add(this.label6);
            this.panel_Base.Controls.Add(this.flowLayoutPanel_Tumini);
            this.panel_Base.Controls.Add(this.textBox_着桟);
            this.panel_Base.Controls.Add(this.textBox_荷役開始);
            this.panel_Base.Controls.Add(this.textBox_入港);
            this.panel_Base.Controls.Add(this.textBox_離桟);
            this.panel_Base.Controls.Add(this.label4);
            this.panel_Base.Controls.Add(this.label5);
            this.panel_Base.Controls.Add(this.label7);
            this.panel_Base.Controls.Add(this.label8);
            this.panel_Base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Base.Location = new System.Drawing.Point(0, 0);
            this.panel_Base.Name = "panel_Base";
            this.panel_Base.Size = new System.Drawing.Size(407, 307);
            this.panel_Base.TabIndex = 0;
            // 
            // singleLineCombo_代理店
            // 
            this.singleLineCombo_代理店.AutoSize = true;
            this.singleLineCombo_代理店.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.singleLineCombo_代理店.Location = new System.Drawing.Point(101, 115);
            this.singleLineCombo_代理店.MaxLength = 32767;
            this.singleLineCombo_代理店.Name = "singleLineCombo_代理店";
            this.singleLineCombo_代理店.ReadOnly = false;
            this.singleLineCombo_代理店.Size = new System.Drawing.Size(250, 19);
            this.singleLineCombo_代理店.TabIndex = 8;
            // 
            // singleLineCombo_荷主
            // 
            this.singleLineCombo_荷主.AutoSize = true;
            this.singleLineCombo_荷主.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.singleLineCombo_荷主.Location = new System.Drawing.Point(101, 137);
            this.singleLineCombo_荷主.MaxLength = 32767;
            this.singleLineCombo_荷主.Name = "singleLineCombo_荷主";
            this.singleLineCombo_荷主.ReadOnly = false;
            this.singleLineCombo_荷主.Size = new System.Drawing.Size(250, 19);
            this.singleLineCombo_荷主.TabIndex = 9;
            // 
            // textBox_備考
            // 
            this.textBox_備考.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_備考.Location = new System.Drawing.Point(101, 160);
            this.textBox_備考.MaxLength = 25;
            this.textBox_備考.Name = "textBox_備考";
            this.textBox_備考.Size = new System.Drawing.Size(250, 19);
            this.textBox_備考.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 163);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 12);
            this.label11.TabIndex = 21;
            this.label11.Text = "　　備考";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 140);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "　　荷主";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 118);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "　　代理店";
            // 
            // label_Header
            // 
            this.label_Header.AutoSize = true;
            this.label_Header.Location = new System.Drawing.Point(9, 4);
            this.label_Header.Name = "label_Header";
            this.label_Header.Size = new System.Drawing.Size(70, 12);
            this.label_Header.TabIndex = 14;
            this.label_Header.Text = "【荷役：積み】";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker.Location = new System.Drawing.Point(101, 25);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(125, 19);
            this.dateTimePicker.TabIndex = 0;
            this.dateTimePicker.Value = null;
            // 
            // singleLineCombo_港
            // 
            this.singleLineCombo_港.AutoSize = true;
            this.singleLineCombo_港.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.singleLineCombo_港.Location = new System.Drawing.Point(101, 48);
            this.singleLineCombo_港.MaxLength = 32767;
            this.singleLineCombo_港.Name = "singleLineCombo_港";
            this.singleLineCombo_港.ReadOnly = false;
            this.singleLineCombo_港.Size = new System.Drawing.Size(250, 19);
            this.singleLineCombo_港.TabIndex = 26;
            // 
            // DouseiYoteiUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel_Base);
            this.Name = "DouseiYoteiUserControl";
            this.Size = new System.Drawing.Size(407, 307);
            this.panel_Base.ResumeLayout(false);
            this.panel_Base.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_入港;
        private System.Windows.Forms.TextBox textBox_着桟;
        private System.Windows.Forms.TextBox textBox_出港;
        private System.Windows.Forms.TextBox textBox_荷役開始;
        private System.Windows.Forms.TextBox textBox_離桟;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Tumini;
        private NBaseUtil.NullableDateTimePicker dateTimePicker;
        private System.Windows.Forms.ComboBox comboBox_基地;
        private System.Windows.Forms.ComboBox comboBox_港;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Panel panel_Base;
        private System.Windows.Forms.Label label_Header;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_備考;
        private System.Windows.Forms.Label label11;
        private NBaseUtil.SingleLineCombo singleLineCombo_代理店;
        private NBaseUtil.SingleLineCombo singleLineCombo_荷主;
        private NBaseUtil.SingleLineCombo singleLineCombo_港;
    }
}
