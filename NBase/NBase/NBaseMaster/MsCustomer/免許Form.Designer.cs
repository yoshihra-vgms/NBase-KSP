namespace NBaseMaster.MsCustomer
{
    partial class 免許Form
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
            this.textBox学科 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Cancel_Btn = new System.Windows.Forms.Button();
            this.Update_Btn = new System.Windows.Forms.Button();
            this.comboBox免許 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox種別 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox学科
            // 
            this.textBox学科.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox学科.Location = new System.Drawing.Point(109, 23);
            this.textBox学科.MaxLength = 50;
            this.textBox学科.Name = "textBox学科";
            this.textBox学科.ReadOnly = true;
            this.textBox学科.Size = new System.Drawing.Size(275, 19);
            this.textBox学科.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "学部・学科";
            // 
            // Cancel_Btn
            // 
            this.Cancel_Btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Cancel_Btn.BackColor = System.Drawing.Color.White;
            this.Cancel_Btn.Location = new System.Drawing.Point(205, 143);
            this.Cancel_Btn.Name = "Cancel_Btn";
            this.Cancel_Btn.Size = new System.Drawing.Size(95, 25);
            this.Cancel_Btn.TabIndex = 10;
            this.Cancel_Btn.Text = "閉じる";
            this.Cancel_Btn.UseVisualStyleBackColor = false;
            this.Cancel_Btn.Click += new System.EventHandler(this.Cancel_Btn_Click);
            // 
            // Update_Btn
            // 
            this.Update_Btn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Update_Btn.BackColor = System.Drawing.Color.White;
            this.Update_Btn.Location = new System.Drawing.Point(104, 143);
            this.Update_Btn.Name = "Update_Btn";
            this.Update_Btn.Size = new System.Drawing.Size(95, 25);
            this.Update_Btn.TabIndex = 9;
            this.Update_Btn.Text = "更新";
            this.Update_Btn.UseVisualStyleBackColor = false;
            this.Update_Btn.Click += new System.EventHandler(this.Update_Btn_Click);
            // 
            // comboBox免許
            // 
            this.comboBox免許.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox免許.FormattingEnabled = true;
            this.comboBox免許.Location = new System.Drawing.Point(109, 60);
            this.comboBox免許.Name = "comboBox免許";
            this.comboBox免許.Size = new System.Drawing.Size(198, 20);
            this.comboBox免許.TabIndex = 14;
            this.comboBox免許.SelectedIndexChanged += new System.EventHandler(this.comboBox免許_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "免許／免状";
            // 
            // comboBox種別
            // 
            this.comboBox種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox種別.FormattingEnabled = true;
            this.comboBox種別.Location = new System.Drawing.Point(109, 90);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(198, 20);
            this.comboBox種別.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "種別";
            // 
            // 免許Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(404, 180);
            this.Controls.Add(this.comboBox種別);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox免許);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox学科);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel_Btn);
            this.Controls.Add(this.Update_Btn);
            this.Name = "免許Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "免許/免状";
            this.Load += new System.EventHandler(this.免許Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox学科;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancel_Btn;
        private System.Windows.Forms.Button Update_Btn;
        private System.Windows.Forms.ComboBox comboBox免許;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox種別;
        private System.Windows.Forms.Label label2;
    }
}