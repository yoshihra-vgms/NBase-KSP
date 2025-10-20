
namespace Senin
{
    partial class 配乗計画詳細ORGForm
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
            this.checkBox_dateToPM = new System.Windows.Forms.CheckBox();
            this.checkBox_dateFromPM = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.textBox_Shokumei = new System.Windows.Forms.TextBox();
            this.dateTimePicker_To = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_From = new System.Windows.Forms.DateTimePicker();
            this.button_delete = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_職種 = new System.Windows.Forms.ComboBox();
            this.comboBox_予定種別 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBox_dateToPM
            // 
            this.checkBox_dateToPM.AutoSize = true;
            this.checkBox_dateToPM.Location = new System.Drawing.Point(392, 112);
            this.checkBox_dateToPM.Name = "checkBox_dateToPM";
            this.checkBox_dateToPM.Size = new System.Drawing.Size(48, 16);
            this.checkBox_dateToPM.TabIndex = 4;
            this.checkBox_dateToPM.Text = "午後";
            this.checkBox_dateToPM.UseVisualStyleBackColor = true;
            // 
            // checkBox_dateFromPM
            // 
            this.checkBox_dateFromPM.AutoSize = true;
            this.checkBox_dateFromPM.Location = new System.Drawing.Point(221, 112);
            this.checkBox_dateFromPM.Name = "checkBox_dateFromPM";
            this.checkBox_dateFromPM.Size = new System.Drawing.Size(48, 16);
            this.checkBox_dateFromPM.TabIndex = 3;
            this.checkBox_dateFromPM.Text = "午後";
            this.checkBox_dateFromPM.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(275, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 27;
            this.label1.Text = "～";
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(126, 193);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(80, 24);
            this.button_OK.TabIndex = 6;
            this.button_OK.Text = "登録";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(298, 193);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(80, 24);
            this.button_Cancel.TabIndex = 7;
            this.button_Cancel.Text = "閉じる";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(197, 22);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.ReadOnly = true;
            this.textBox_Name.Size = new System.Drawing.Size(145, 19);
            this.textBox_Name.TabIndex = 23;
            // 
            // textBox_Shokumei
            // 
            this.textBox_Shokumei.Location = new System.Drawing.Point(127, 22);
            this.textBox_Shokumei.Name = "textBox_Shokumei";
            this.textBox_Shokumei.ReadOnly = true;
            this.textBox_Shokumei.Size = new System.Drawing.Size(64, 19);
            this.textBox_Shokumei.TabIndex = 22;
            // 
            // dateTimePicker_To
            // 
            this.dateTimePicker_To.Location = new System.Drawing.Point(298, 82);
            this.dateTimePicker_To.Name = "dateTimePicker_To";
            this.dateTimePicker_To.Size = new System.Drawing.Size(142, 19);
            this.dateTimePicker_To.TabIndex = 2;
            // 
            // dateTimePicker_From
            // 
            this.dateTimePicker_From.Location = new System.Drawing.Point(127, 82);
            this.dateTimePicker_From.Name = "dateTimePicker_From";
            this.dateTimePicker_From.Size = new System.Drawing.Size(142, 19);
            this.dateTimePicker_From.TabIndex = 1;
            // 
            // button_delete
            // 
            this.button_delete.Location = new System.Drawing.Point(212, 193);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(80, 24);
            this.button_delete.TabIndex = 8;
            this.button_delete.Text = "削除";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(68, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 36;
            this.label6.Text = "職名変更";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(68, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 35;
            this.label5.Text = "予定種別";
            // 
            // comboBox_職種
            // 
            this.comboBox_職種.FormattingEnabled = true;
            this.comboBox_職種.Location = new System.Drawing.Point(127, 47);
            this.comboBox_職種.Name = "comboBox_職種";
            this.comboBox_職種.Size = new System.Drawing.Size(152, 20);
            this.comboBox_職種.TabIndex = 0;
            // 
            // comboBox_予定種別
            // 
            this.comboBox_予定種別.FormattingEnabled = true;
            this.comboBox_予定種別.Location = new System.Drawing.Point(127, 134);
            this.comboBox_予定種別.Name = "comboBox_予定種別";
            this.comboBox_予定種別.Size = new System.Drawing.Size(152, 20);
            this.comboBox_予定種別.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 37;
            this.label3.Text = "期間";
            // 
            // 配乗計画詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 257);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox_職種);
            this.Controls.Add(this.comboBox_予定種別);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.checkBox_dateToPM);
            this.Controls.Add(this.checkBox_dateFromPM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.textBox_Shokumei);
            this.Controls.Add(this.dateTimePicker_To);
            this.Controls.Add(this.dateTimePicker_From);
            this.Name = "配乗計画詳細Form";
            this.Text = "配乗計画詳細";
            this.Load += new System.EventHandler(this.配乗計画詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBox_dateToPM;
        private System.Windows.Forms.CheckBox checkBox_dateFromPM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.TextBox textBox_Shokumei;
        private System.Windows.Forms.DateTimePicker dateTimePicker_To;
        private System.Windows.Forms.DateTimePicker dateTimePicker_From;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_職種;
        private System.Windows.Forms.ComboBox comboBox_予定種別;
        private System.Windows.Forms.Label label3;
    }
}