
namespace Senin
{
    partial class 配乗計画内航詳細Form
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
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox_職種 = new System.Windows.Forms.ComboBox();
            this.comboBox_予定種別 = new System.Windows.Forms.ComboBox();
            this.button_delete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.textBox_Shokumei = new System.Windows.Forms.TextBox();
            this.dateTimePicker_To = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_From = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_Clear = new System.Windows.Forms.Button();
            this.comboBox_交代者 = new System.Windows.Forms.ComboBox();
            this.comboBox_港 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 52;
            this.label3.Text = "期間";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(68, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 51;
            this.label6.Text = "職名変更";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(68, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 50;
            this.label5.Text = "予定種別";
            // 
            // comboBox_職種
            // 
            this.comboBox_職種.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_職種.FormattingEnabled = true;
            this.comboBox_職種.Location = new System.Drawing.Point(127, 47);
            this.comboBox_職種.Name = "comboBox_職種";
            this.comboBox_職種.Size = new System.Drawing.Size(152, 20);
            this.comboBox_職種.TabIndex = 38;
            this.comboBox_職種.TextChanged += new System.EventHandler(this.comboBox_職種_TextChanged);
            // 
            // comboBox_予定種別
            // 
            this.comboBox_予定種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_予定種別.FormattingEnabled = true;
            this.comboBox_予定種別.Location = new System.Drawing.Point(127, 134);
            this.comboBox_予定種別.Name = "comboBox_予定種別";
            this.comboBox_予定種別.Size = new System.Drawing.Size(152, 20);
            this.comboBox_予定種別.TabIndex = 43;
            this.comboBox_予定種別.TextChanged += new System.EventHandler(this.comboBox_予定種別_TextChanged);
            // 
            // button_delete
            // 
            this.button_delete.Location = new System.Drawing.Point(212, 283);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(80, 24);
            this.button_delete.TabIndex = 46;
            this.button_delete.Text = "削除";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(275, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "～";
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(126, 283);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(80, 24);
            this.button_OK.TabIndex = 44;
            this.button_OK.Text = "登録";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(298, 283);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(80, 24);
            this.button_Cancel.TabIndex = 45;
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
            this.textBox_Name.TabIndex = 48;
            // 
            // textBox_Shokumei
            // 
            this.textBox_Shokumei.Location = new System.Drawing.Point(127, 22);
            this.textBox_Shokumei.Name = "textBox_Shokumei";
            this.textBox_Shokumei.ReadOnly = true;
            this.textBox_Shokumei.Size = new System.Drawing.Size(64, 19);
            this.textBox_Shokumei.TabIndex = 47;
            // 
            // dateTimePicker_To
            // 
            this.dateTimePicker_To.Location = new System.Drawing.Point(298, 82);
            this.dateTimePicker_To.Name = "dateTimePicker_To";
            this.dateTimePicker_To.Size = new System.Drawing.Size(142, 19);
            this.dateTimePicker_To.TabIndex = 40;
            // 
            // dateTimePicker_From
            // 
            this.dateTimePicker_From.Location = new System.Drawing.Point(127, 82);
            this.dateTimePicker_From.Name = "dateTimePicker_From";
            this.dateTimePicker_From.Size = new System.Drawing.Size(142, 19);
            this.dateTimePicker_From.TabIndex = 39;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_Clear);
            this.groupBox1.Controls.Add(this.comboBox_交代者);
            this.groupBox1.Controls.Add(this.comboBox_港);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(61, 171);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 91);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "交代情報";
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(237, 40);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(80, 24);
            this.button_Clear.TabIndex = 58;
            this.button_Clear.Text = "クリア";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // comboBox_交代者
            // 
            this.comboBox_交代者.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_交代者.FormattingEnabled = true;
            this.comboBox_交代者.Location = new System.Drawing.Point(65, 56);
            this.comboBox_交代者.Name = "comboBox_交代者";
            this.comboBox_交代者.Size = new System.Drawing.Size(152, 20);
            this.comboBox_交代者.TabIndex = 60;
            // 
            // comboBox_港
            // 
            this.comboBox_港.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_港.FormattingEnabled = true;
            this.comboBox_港.Location = new System.Drawing.Point(65, 29);
            this.comboBox_港.Name = "comboBox_港";
            this.comboBox_港.Size = new System.Drawing.Size(152, 20);
            this.comboBox_港.TabIndex = 59;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 58;
            this.label4.Text = "交代者";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 57;
            this.label2.Text = "港";
            // 
            // 配乗計画内航詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 336);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox_職種);
            this.Controls.Add(this.comboBox_予定種別);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.textBox_Shokumei);
            this.Controls.Add(this.dateTimePicker_To);
            this.Controls.Add(this.dateTimePicker_From);
            this.Name = "配乗計画内航詳細Form";
            this.Text = "配乗計画内航詳細";
            this.Load += new System.EventHandler(this.配乗計画内航詳細Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox_職種;
        private System.Windows.Forms.ComboBox comboBox_予定種別;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.TextBox textBox_Shokumei;
        private System.Windows.Forms.DateTimePicker dateTimePicker_To;
        private System.Windows.Forms.DateTimePicker dateTimePicker_From;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.ComboBox comboBox_交代者;
        private System.Windows.Forms.ComboBox comboBox_港;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}