
namespace Senin
{
    partial class 配乗計画BaseForm
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
            this.label7 = new System.Windows.Forms.Label();
            this.label_HeadTotal = new System.Windows.Forms.Label();
            this.button検索 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button月締 = new System.Windows.Forms.Button();
            this.button出力 = new System.Windows.Forms.Button();
            this.buttonRevUp = new System.Windows.Forms.Button();
            this.comboBox月 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox選択職種のみ表示 = new System.Windows.Forms.CheckBox();
            this.checkBox選択種別のみ表示 = new System.Windows.Forms.CheckBox();
            this.checkBox_実績 = new System.Windows.Forms.CheckBox();
            this.checkBox_計画 = new System.Windows.Forms.CheckBox();
            this.comboBox_RevNo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_職種 = new System.Windows.Forms.ComboBox();
            this.comboBox_計画種別 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox年 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView_種別 = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_種別)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(388, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 75;
            this.label7.Text = "船員数=";
            this.label7.Visible = false;
            // 
            // label_HeadTotal
            // 
            this.label_HeadTotal.AutoSize = true;
            this.label_HeadTotal.Location = new System.Drawing.Point(947, 9);
            this.label_HeadTotal.Name = "label_HeadTotal";
            this.label_HeadTotal.Size = new System.Drawing.Size(11, 12);
            this.label_HeadTotal.TabIndex = 74;
            this.label_HeadTotal.Text = "1";
            this.label_HeadTotal.Visible = false;
            // 
            // button検索
            // 
            this.button検索.Location = new System.Drawing.Point(205, 6);
            this.button検索.Name = "button検索";
            this.button検索.Size = new System.Drawing.Size(94, 33);
            this.button検索.TabIndex = 2;
            this.button検索.Text = "検索";
            this.button検索.UseVisualStyleBackColor = true;
            this.button検索.Click += new System.EventHandler(this.button検索_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(836, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 12);
            this.label4.TabIndex = 73;
            this.label4.Text = "年月　Rev数 締めか";
            this.label4.Visible = false;
            // 
            // button月締
            // 
            this.button月締.Location = new System.Drawing.Point(890, 45);
            this.button月締.Name = "button月締";
            this.button月締.Size = new System.Drawing.Size(94, 33);
            this.button月締.TabIndex = 5;
            this.button月締.Text = "確定";
            this.button月締.UseVisualStyleBackColor = true;
            this.button月締.Click += new System.EventHandler(this.button月締_Click);
            // 
            // button出力
            // 
            this.button出力.Location = new System.Drawing.Point(890, 90);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(94, 33);
            this.button出力.TabIndex = 6;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = true;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // buttonRevUp
            // 
            this.buttonRevUp.Location = new System.Drawing.Point(781, 45);
            this.buttonRevUp.Name = "buttonRevUp";
            this.buttonRevUp.Size = new System.Drawing.Size(94, 33);
            this.buttonRevUp.TabIndex = 4;
            this.buttonRevUp.Text = "Revision Up";
            this.buttonRevUp.UseVisualStyleBackColor = true;
            this.buttonRevUp.Click += new System.EventHandler(this.buttonRevUp_Click);
            // 
            // comboBox月
            // 
            this.comboBox月.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox月.FormattingEnabled = true;
            this.comboBox月.Location = new System.Drawing.Point(154, 12);
            this.comboBox月.Name = "comboBox月";
            this.comboBox月.Size = new System.Drawing.Size(38, 20);
            this.comboBox月.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox選択職種のみ表示);
            this.groupBox1.Controls.Add(this.checkBox選択種別のみ表示);
            this.groupBox1.Controls.Add(this.checkBox_実績);
            this.groupBox1.Controls.Add(this.checkBox_計画);
            this.groupBox1.Controls.Add(this.comboBox_RevNo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox_職種);
            this.groupBox1.Controls.Add(this.comboBox_計画種別);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(745, 78);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // checkBox選択職種のみ表示
            // 
            this.checkBox選択職種のみ表示.AutoSize = true;
            this.checkBox選択職種のみ表示.Enabled = false;
            this.checkBox選択職種のみ表示.Location = new System.Drawing.Point(289, 52);
            this.checkBox選択職種のみ表示.Name = "checkBox選択職種のみ表示";
            this.checkBox選択職種のみ表示.Size = new System.Drawing.Size(117, 16);
            this.checkBox選択職種のみ表示.TabIndex = 3;
            this.checkBox選択職種のみ表示.Text = "選択職名のみ表示";
            this.checkBox選択職種のみ表示.UseVisualStyleBackColor = true;
            this.checkBox選択職種のみ表示.CheckedChanged += new System.EventHandler(this.checkBox職種_CheckedChanged);
            // 
            // checkBox選択種別のみ表示
            // 
            this.checkBox選択種別のみ表示.AutoSize = true;
            this.checkBox選択種別のみ表示.Enabled = false;
            this.checkBox選択種別のみ表示.Location = new System.Drawing.Point(57, 52);
            this.checkBox選択種別のみ表示.Name = "checkBox選択種別のみ表示";
            this.checkBox選択種別のみ表示.Size = new System.Drawing.Size(117, 16);
            this.checkBox選択種別のみ表示.TabIndex = 1;
            this.checkBox選択種別のみ表示.Text = "選択種別のみ表示";
            this.checkBox選択種別のみ表示.UseVisualStyleBackColor = true;
            this.checkBox選択種別のみ表示.CheckedChanged += new System.EventHandler(this.checkBox種別_CheckedChanged);
            // 
            // checkBox_実績
            // 
            this.checkBox_実績.AutoSize = true;
            this.checkBox_実績.Location = new System.Drawing.Point(624, 45);
            this.checkBox_実績.Name = "checkBox_実績";
            this.checkBox_実績.Size = new System.Drawing.Size(48, 16);
            this.checkBox_実績.TabIndex = 43;
            this.checkBox_実績.Text = "実績";
            this.checkBox_実績.UseVisualStyleBackColor = true;
            this.checkBox_実績.CheckedChanged += new System.EventHandler(this.checkBox_計画実績_CheckedChanged);
            // 
            // checkBox_計画
            // 
            this.checkBox_計画.AutoSize = true;
            this.checkBox_計画.Checked = true;
            this.checkBox_計画.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_計画.Location = new System.Drawing.Point(624, 23);
            this.checkBox_計画.Name = "checkBox_計画";
            this.checkBox_計画.Size = new System.Drawing.Size(48, 16);
            this.checkBox_計画.TabIndex = 5;
            this.checkBox_計画.Text = "計画";
            this.checkBox_計画.UseVisualStyleBackColor = true;
            this.checkBox_計画.CheckedChanged += new System.EventHandler(this.checkBox_計画実績_CheckedChanged);
            // 
            // comboBox_RevNo
            // 
            this.comboBox_RevNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_RevNo.FormattingEnabled = true;
            this.comboBox_RevNo.Location = new System.Drawing.Point(534, 21);
            this.comboBox_RevNo.Name = "comboBox_RevNo";
            this.comboBox_RevNo.Size = new System.Drawing.Size(51, 20);
            this.comboBox_RevNo.TabIndex = 4;
            this.comboBox_RevNo.TextChanged += new System.EventHandler(this.comboBox_RevNo_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(489, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 12);
            this.label3.TabIndex = 56;
            this.label3.Text = "RevNo";
            // 
            // comboBox_職種
            // 
            this.comboBox_職種.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_職種.FormattingEnabled = true;
            this.comboBox_職種.Location = new System.Drawing.Point(289, 21);
            this.comboBox_職種.Name = "comboBox_職種";
            this.comboBox_職種.Size = new System.Drawing.Size(152, 20);
            this.comboBox_職種.TabIndex = 2;
            this.comboBox_職種.TextChanged += new System.EventHandler(this.comboBox_職種_TextChanged);
            // 
            // comboBox_計画種別
            // 
            this.comboBox_計画種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_計画種別.FormattingEnabled = true;
            this.comboBox_計画種別.Location = new System.Drawing.Point(57, 21);
            this.comboBox_計画種別.Name = "comboBox_計画種別";
            this.comboBox_計画種別.Size = new System.Drawing.Size(152, 20);
            this.comboBox_計画種別.TabIndex = 0;
            this.comboBox_計画種別.TextChanged += new System.EventHandler(this.comboBox_種別_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "種別";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(254, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 27;
            this.label6.Text = "職名";
            // 
            // comboBox年
            // 
            this.comboBox年.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年.FormattingEnabled = true;
            this.comboBox年.Location = new System.Drawing.Point(70, 12);
            this.comboBox年.Name = "comboBox年";
            this.comboBox年.Size = new System.Drawing.Size(61, 20);
            this.comboBox年.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1135, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 69;
            this.label1.Text = "凡例";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 71;
            this.label2.Text = "年月";
            // 
            // dataGridView_種別
            // 
            this.dataGridView_種別.AllowUserToAddRows = false;
            this.dataGridView_種別.AllowUserToResizeColumns = false;
            this.dataGridView_種別.AllowUserToResizeRows = false;
            this.dataGridView_種別.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView_種別.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_種別.ColumnHeadersVisible = false;
            this.dataGridView_種別.Location = new System.Drawing.Point(1170, 12);
            this.dataGridView_種別.Name = "dataGridView_種別";
            this.dataGridView_種別.ReadOnly = true;
            this.dataGridView_種別.RowHeadersVisible = false;
            this.dataGridView_種別.RowTemplate.Height = 21;
            this.dataGridView_種別.Size = new System.Drawing.Size(197, 113);
            this.dataGridView_種別.TabIndex = 68;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(137, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 72;
            this.label8.Text = "/";
            // 
            // 配乗計画BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1866, 701);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label_HeadTotal);
            this.Controls.Add(this.button検索);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button月締);
            this.Controls.Add(this.button出力);
            this.Controls.Add(this.buttonRevUp);
            this.Controls.Add(this.comboBox月);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox年);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView_種別);
            this.Controls.Add(this.label8);
            this.MinimumSize = new System.Drawing.Size(1318, 740);
            this.Name = "配乗計画BaseForm";
            this.Text = "配乗計画内航";
            this.Load += new System.EventHandler(this.配乗計画BaseForm_Load);
            this.Shown += new System.EventHandler(this.配乗計画BaseForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_種別)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_HeadTotal;
        private System.Windows.Forms.Button button検索;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button月締;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.Button buttonRevUp;
        private System.Windows.Forms.ComboBox comboBox月;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox選択職種のみ表示;
        private System.Windows.Forms.CheckBox checkBox選択種別のみ表示;
        private System.Windows.Forms.CheckBox checkBox_実績;
        private System.Windows.Forms.CheckBox checkBox_計画;
        private System.Windows.Forms.ComboBox comboBox_RevNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_職種;
        private System.Windows.Forms.ComboBox comboBox_計画種別;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox年;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView_種別;
        private System.Windows.Forms.Label label8;
    }
}