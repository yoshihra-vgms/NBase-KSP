namespace NBaseMaster.バース管理
{
    partial class バース管理詳細Form
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
            this.BerthName_textBox = new System.Windows.Forms.TextBox();
            this.BerthCode_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Update_button = new System.Windows.Forms.Button();
            this.Delete_button = new System.Windows.Forms.Button();
            this.Close_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.Kichi_comboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // BerthName_textBox
            // 
            this.BerthName_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.BerthName_textBox.Location = new System.Drawing.Point(100, 37);
            this.BerthName_textBox.MaxLength = 20;
            this.BerthName_textBox.Name = "BerthName_textBox";
            this.BerthName_textBox.Size = new System.Drawing.Size(165, 19);
            this.BerthName_textBox.TabIndex = 1;
            this.BerthName_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // BerthCode_textBox
            // 
            this.BerthCode_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.BerthCode_textBox.Location = new System.Drawing.Point(100, 12);
            this.BerthCode_textBox.MaxLength = 4;
            this.BerthCode_textBox.Name = "BerthCode_textBox";
            this.BerthCode_textBox.Size = new System.Drawing.Size(100, 19);
            this.BerthCode_textBox.TabIndex = 0;
            this.BerthCode_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "※バースコード";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "※バース名";
            // 
            // Update_button
            // 
            this.Update_button.Location = new System.Drawing.Point(28, 105);
            this.Update_button.Name = "Update_button";
            this.Update_button.Size = new System.Drawing.Size(75, 23);
            this.Update_button.TabIndex = 2;
            this.Update_button.Text = "更新";
            this.Update_button.UseVisualStyleBackColor = true;
            this.Update_button.Click += new System.EventHandler(this.Update_button_Click);
            // 
            // Delete_button
            // 
            this.Delete_button.Location = new System.Drawing.Point(109, 105);
            this.Delete_button.Name = "Delete_button";
            this.Delete_button.Size = new System.Drawing.Size(75, 23);
            this.Delete_button.TabIndex = 3;
            this.Delete_button.Text = "削除";
            this.Delete_button.UseVisualStyleBackColor = true;
            this.Delete_button.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // Close_button
            // 
            this.Close_button.Location = new System.Drawing.Point(190, 105);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(75, 23);
            this.Close_button.TabIndex = 4;
            this.Close_button.Text = "閉じる";
            this.Close_button.UseVisualStyleBackColor = true;
            this.Close_button.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "基地";
            // 
            // Kichi_comboBox
            // 
            this.Kichi_comboBox.FormattingEnabled = true;
            this.Kichi_comboBox.Location = new System.Drawing.Point(100, 62);
            this.Kichi_comboBox.Name = "Kichi_comboBox";
            this.Kichi_comboBox.Size = new System.Drawing.Size(165, 20);
            this.Kichi_comboBox.TabIndex = 5;
            this.Kichi_comboBox.SelectedIndexChanged += new System.EventHandler(this.DataChange);
            // 
            // バース管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 153);
            this.Controls.Add(this.Kichi_comboBox);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Delete_button);
            this.Controls.Add(this.Update_button);
            this.Controls.Add(this.BerthName_textBox);
            this.Controls.Add(this.BerthCode_textBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "バース管理詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "バース管理詳細Form";
            this.Load += new System.EventHandler(this.バース管理詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox BerthName_textBox;
        private System.Windows.Forms.TextBox BerthCode_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Update_button;
        private System.Windows.Forms.Button Delete_button;
        private System.Windows.Forms.Button Close_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Kichi_comboBox;
    }
}