namespace NBaseHonsen
{
    partial class 動静実績登録Form
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_Day = new System.Windows.Forms.Label();
            this.button_登録 = new System.Windows.Forms.Button();
            this.button_閉じる = new System.Windows.Forms.Button();
            this.button_UP = new System.Windows.Forms.Button();
            this.button_Down = new System.Windows.Forms.Button();
            this.label_Count = new System.Windows.Forms.Label();
            this.panel_積揚 = new System.Windows.Forms.Panel();
            this.panel_待機入渠パージ = new System.Windows.Forms.Panel();
            this.textBox_備考 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_基地 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_港 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label_待機入渠パージ = new System.Windows.Forms.Label();
            this.button_削除 = new System.Windows.Forms.Button();
            this.label_DeleteMessage = new System.Windows.Forms.Label();
            this.douseJissekiUserControl1 = new NBaseCommon.DouseiJissekiUserControl();
            this.douseYoteiReadOnlyUserControl1 = new NBaseCommon.DouseYoteiReadOnlyUserControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel_積揚.SuspendLayout();
            this.panel_待機入渠パージ.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.douseYoteiReadOnlyUserControl1);
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 455);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "予定";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.douseJissekiUserControl1);
            this.groupBox2.Location = new System.Drawing.Point(458, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(437, 455);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "実績";
            // 
            // label_Day
            // 
            this.label_Day.AutoSize = true;
            this.label_Day.Font = new System.Drawing.Font("MS UI Gothic", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Day.Location = new System.Drawing.Point(22, 14);
            this.label_Day.Name = "label_Day";
            this.label_Day.Size = new System.Drawing.Size(147, 18);
            this.label_Day.TabIndex = 5;
            this.label_Day.Text = " yyyy年mm月dd日";
            // 
            // button_登録
            // 
            this.button_登録.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_登録.Location = new System.Drawing.Point(814, 5);
            this.button_登録.Name = "button_登録";
            this.button_登録.Size = new System.Drawing.Size(84, 27);
            this.button_登録.TabIndex = 6;
            this.button_登録.Text = "登録";
            this.button_登録.UseVisualStyleBackColor = true;
            this.button_登録.Click += new System.EventHandler(this.button_登録_Click);
            // 
            // button_閉じる
            // 
            this.button_閉じる.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_閉じる.Location = new System.Drawing.Point(814, 71);
            this.button_閉じる.Name = "button_閉じる";
            this.button_閉じる.Size = new System.Drawing.Size(84, 27);
            this.button_閉じる.TabIndex = 7;
            this.button_閉じる.Text = "閉じる";
            this.button_閉じる.UseVisualStyleBackColor = true;
            this.button_閉じる.Click += new System.EventHandler(this.button_閉じる_Click);
            // 
            // button_UP
            // 
            this.button_UP.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_UP.Location = new System.Drawing.Point(252, 5);
            this.button_UP.Name = "button_UP";
            this.button_UP.Size = new System.Drawing.Size(35, 33);
            this.button_UP.TabIndex = 8;
            this.button_UP.Text = "↑";
            this.button_UP.UseVisualStyleBackColor = true;
            this.button_UP.Click += new System.EventHandler(this.button_UP_Click);
            // 
            // button_Down
            // 
            this.button_Down.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Down.Location = new System.Drawing.Point(252, 40);
            this.button_Down.Name = "button_Down";
            this.button_Down.Size = new System.Drawing.Size(35, 33);
            this.button_Down.TabIndex = 9;
            this.button_Down.Text = "↓";
            this.button_Down.UseVisualStyleBackColor = true;
            this.button_Down.Click += new System.EventHandler(this.button_Down_Click);
            // 
            // label_Count
            // 
            this.label_Count.AutoSize = true;
            this.label_Count.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Count.Location = new System.Drawing.Point(191, 14);
            this.label_Count.Name = "label_Count";
            this.label_Count.Size = new System.Drawing.Size(46, 16);
            this.label_Count.TabIndex = 10;
            this.label_Count.Text = "(N/N)";
            // 
            // panel_積揚
            // 
            this.panel_積揚.Controls.Add(this.groupBox2);
            this.panel_積揚.Controls.Add(this.groupBox1);
            this.panel_積揚.Location = new System.Drawing.Point(5, 104);
            this.panel_積揚.Name = "panel_積揚";
            this.panel_積揚.Size = new System.Drawing.Size(904, 465);
            this.panel_積揚.TabIndex = 11;
            // 
            // panel_待機入渠パージ
            // 
            this.panel_待機入渠パージ.Controls.Add(this.textBox_備考);
            this.panel_待機入渠パージ.Controls.Add(this.label1);
            this.panel_待機入渠パージ.Controls.Add(this.comboBox_基地);
            this.panel_待機入渠パージ.Controls.Add(this.label5);
            this.panel_待機入渠パージ.Controls.Add(this.comboBox_港);
            this.panel_待機入渠パージ.Controls.Add(this.label4);
            this.panel_待機入渠パージ.Controls.Add(this.label3);
            this.panel_待機入渠パージ.Controls.Add(this.dateTimePicker2);
            this.panel_待機入渠パージ.Controls.Add(this.dateTimePicker1);
            this.panel_待機入渠パージ.Controls.Add(this.label6);
            this.panel_待機入渠パージ.Controls.Add(this.label_待機入渠パージ);
            this.panel_待機入渠パージ.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel_待機入渠パージ.Location = new System.Drawing.Point(5, 77);
            this.panel_待機入渠パージ.Name = "panel_待機入渠パージ";
            this.panel_待機入渠パージ.Size = new System.Drawing.Size(656, 86);
            this.panel_待機入渠パージ.TabIndex = 12;
            // 
            // textBox_備考
            // 
            this.textBox_備考.Location = new System.Drawing.Point(50, 58);
            this.textBox_備考.MaxLength = 25;
            this.textBox_備考.Name = "textBox_備考";
            this.textBox_備考.Size = new System.Drawing.Size(286, 21);
            this.textBox_備考.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 18;
            this.label1.Text = "備考";
            // 
            // comboBox_基地
            // 
            this.comboBox_基地.FormattingEnabled = true;
            this.comboBox_基地.Location = new System.Drawing.Point(384, 32);
            this.comboBox_基地.Name = "comboBox_基地";
            this.comboBox_基地.Size = new System.Drawing.Size(265, 21);
            this.comboBox_基地.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(342, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 14);
            this.label5.TabIndex = 14;
            this.label5.Text = "基地";
            // 
            // comboBox_港
            // 
            this.comboBox_港.FormattingEnabled = true;
            this.comboBox_港.Location = new System.Drawing.Point(50, 32);
            this.comboBox_港.Name = "comboBox_港";
            this.comboBox_港.Size = new System.Drawing.Size(265, 21);
            this.comboBox_港.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 14);
            this.label4.TabIndex = 12;
            this.label4.Text = "港";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(440, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "～";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(469, 5);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(144, 21);
            this.dateTimePicker2.TabIndex = 10;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(279, 5);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(144, 21);
            this.dateTimePicker1.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(233, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 14);
            this.label6.TabIndex = 8;
            this.label6.Text = "日付";
            // 
            // label_待機入渠パージ
            // 
            this.label_待機入渠パージ.AutoSize = true;
            this.label_待機入渠パージ.Location = new System.Drawing.Point(9, 10);
            this.label_待機入渠パージ.Name = "label_待機入渠パージ";
            this.label_待機入渠パージ.Size = new System.Drawing.Size(147, 14);
            this.label_待機入渠パージ.TabIndex = 0;
            this.label_待機入渠パージ.Text = "種別：待機/入渠/パージ";
            // 
            // button_削除
            // 
            this.button_削除.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_削除.Location = new System.Drawing.Point(814, 38);
            this.button_削除.Name = "button_削除";
            this.button_削除.Size = new System.Drawing.Size(84, 27);
            this.button_削除.TabIndex = 19;
            this.button_削除.Text = "削除";
            this.button_削除.UseVisualStyleBackColor = true;
            this.button_削除.Click += new System.EventHandler(this.button_削除_Click);
            // 
            // label_DeleteMessage
            // 
            this.label_DeleteMessage.AutoSize = true;
            this.label_DeleteMessage.ForeColor = System.Drawing.Color.Red;
            this.label_DeleteMessage.Location = new System.Drawing.Point(25, 60);
            this.label_DeleteMessage.Name = "label_DeleteMessage";
            this.label_DeleteMessage.Size = new System.Drawing.Size(212, 14);
            this.label_DeleteMessage.TabIndex = 20;
            this.label_DeleteMessage.Text = "この動静実績は、既に削除されました";
            // 
            // douseJissekiUserControl1
            // 
            this.douseJissekiUserControl1.BackColor = System.Drawing.Color.White;
            this.douseJissekiUserControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.douseJissekiUserControl1.Location = new System.Drawing.Point(8, 27);
            this.douseJissekiUserControl1.Name = "douseJissekiUserControl1";
            this.douseJissekiUserControl1.Size = new System.Drawing.Size(420, 420);
            this.douseJissekiUserControl1.TabIndex = 1;
            // 
            // douseYoteiReadOnlyUserControl1
            // 
            this.douseYoteiReadOnlyUserControl1.BackColor = System.Drawing.Color.White;
            this.douseYoteiReadOnlyUserControl1.Location = new System.Drawing.Point(8, 27);
            this.douseYoteiReadOnlyUserControl1.Name = "douseYoteiReadOnlyUserControl1";
            this.douseYoteiReadOnlyUserControl1.Size = new System.Drawing.Size(425, 420);
            this.douseYoteiReadOnlyUserControl1.TabIndex = 2;
            // 
            // 動静実績登録Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 576);
            this.Controls.Add(this.label_DeleteMessage);
            this.Controls.Add(this.button_削除);
            this.Controls.Add(this.panel_待機入渠パージ);
            this.Controls.Add(this.panel_積揚);
            this.Controls.Add(this.label_Count);
            this.Controls.Add(this.button_Down);
            this.Controls.Add(this.button_UP);
            this.Controls.Add(this.button_閉じる);
            this.Controls.Add(this.button_登録);
            this.Controls.Add(this.label_Day);
            this.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "動静実績登録Form";
            this.Text = "動静実績登録Form";
            this.Load += new System.EventHandler(this.動静実績登録Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel_積揚.ResumeLayout(false);
            this.panel_待機入渠パージ.ResumeLayout(false);
            this.panel_待機入渠パージ.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NBaseCommon.DouseiJissekiUserControl douseJissekiUserControl1;
        private NBaseCommon.DouseYoteiReadOnlyUserControl douseYoteiReadOnlyUserControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_Day;
        private System.Windows.Forms.Button button_登録;
        private System.Windows.Forms.Button button_閉じる;
        private System.Windows.Forms.Button button_UP;
        private System.Windows.Forms.Button button_Down;
        private System.Windows.Forms.Label label_Count;
        private System.Windows.Forms.Panel panel_積揚;
        private System.Windows.Forms.Panel panel_待機入渠パージ;
        private System.Windows.Forms.Label label_待機入渠パージ;
        private System.Windows.Forms.ComboBox comboBox_基地;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_港;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_削除;
        private System.Windows.Forms.Label label_DeleteMessage;
        private System.Windows.Forms.TextBox textBox_備考;
        private System.Windows.Forms.Label label1;
    }
}