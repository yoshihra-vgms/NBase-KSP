namespace NBaseMaster.貨物管理
{
    partial class 貨物管理詳細Form
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
            this.CargoName_textBox = new System.Windows.Forms.TextBox();
            this.CargoNo_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Update_button = new System.Windows.Forms.Button();
            this.Delete_button = new System.Windows.Forms.Button();
            this.Close_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Ninushi_textBox = new System.Windows.Forms.TextBox();
            this.YusoItem_comboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CargoName_textBox
            // 
            this.CargoName_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.CargoName_textBox.Location = new System.Drawing.Point(95, 43);
            this.CargoName_textBox.MaxLength = 50;
            this.CargoName_textBox.Name = "CargoName_textBox";
            this.CargoName_textBox.Size = new System.Drawing.Size(250, 19);
            this.CargoName_textBox.TabIndex = 1;
            this.CargoName_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // CargoNo_textBox
            // 
            this.CargoNo_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CargoNo_textBox.Location = new System.Drawing.Point(95, 18);
            this.CargoNo_textBox.MaxLength = 3;
            this.CargoNo_textBox.Name = "CargoNo_textBox";
            this.CargoNo_textBox.Size = new System.Drawing.Size(100, 19);
            this.CargoNo_textBox.TabIndex = 0;
            this.CargoNo_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "※貨物No";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "※貨物名";
            // 
            // Update_button
            // 
            this.Update_button.Location = new System.Drawing.Point(75, 133);
            this.Update_button.Name = "Update_button";
            this.Update_button.Size = new System.Drawing.Size(75, 23);
            this.Update_button.TabIndex = 3;
            this.Update_button.Text = "更新";
            this.Update_button.UseVisualStyleBackColor = true;
            this.Update_button.Click += new System.EventHandler(this.Update_button_Click);
            // 
            // Delete_button
            // 
            this.Delete_button.Location = new System.Drawing.Point(156, 133);
            this.Delete_button.Name = "Delete_button";
            this.Delete_button.Size = new System.Drawing.Size(75, 23);
            this.Delete_button.TabIndex = 4;
            this.Delete_button.Text = "削除";
            this.Delete_button.UseVisualStyleBackColor = true;
            this.Delete_button.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // Close_button
            // 
            this.Close_button.Location = new System.Drawing.Point(237, 133);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(75, 23);
            this.Close_button.TabIndex = 5;
            this.Close_button.Text = "閉じる";
            this.Close_button.UseVisualStyleBackColor = true;
            this.Close_button.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "荷主名";
            // 
            // Ninushi_textBox
            // 
            this.Ninushi_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Ninushi_textBox.Location = new System.Drawing.Point(95, 68);
            this.Ninushi_textBox.MaxLength = 50;
            this.Ninushi_textBox.Name = "Ninushi_textBox";
            this.Ninushi_textBox.Size = new System.Drawing.Size(250, 19);
            this.Ninushi_textBox.TabIndex = 2;
            this.Ninushi_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // YusoItem_comboBox
            // 
            this.YusoItem_comboBox.BackColor = System.Drawing.Color.DimGray;
            this.YusoItem_comboBox.FormattingEnabled = true;
            this.YusoItem_comboBox.Location = new System.Drawing.Point(95, 93);
            this.YusoItem_comboBox.Name = "YusoItem_comboBox";
            this.YusoItem_comboBox.Size = new System.Drawing.Size(268, 20);
            this.YusoItem_comboBox.TabIndex = 6;
            this.YusoItem_comboBox.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(25, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "輸送品目";
            this.label4.Visible = false;
            // 
            // 貨物管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(387, 175);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.YusoItem_comboBox);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Delete_button);
            this.Controls.Add(this.Update_button);
            this.Controls.Add(this.Ninushi_textBox);
            this.Controls.Add(this.CargoName_textBox);
            this.Controls.Add(this.CargoNo_textBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "貨物管理詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "貨物管理詳細Form";
            this.Load += new System.EventHandler(this.貨物管理詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CargoName_textBox;
        private System.Windows.Forms.TextBox CargoNo_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Update_button;
        private System.Windows.Forms.Button Delete_button;
        private System.Windows.Forms.Button Close_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Ninushi_textBox;
        private System.Windows.Forms.ComboBox YusoItem_comboBox;
        private System.Windows.Forms.Label label4;
    }
}