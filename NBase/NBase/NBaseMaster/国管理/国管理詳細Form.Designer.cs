namespace NBaseMaster
{
    partial class 国管理詳細Form
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
            this.RegionalCode_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Update_button = new System.Windows.Forms.Button();
            this.Delete_button = new System.Windows.Forms.Button();
            this.Close_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.RegionalName_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // RegionalCode_textBox
            // 
            this.RegionalCode_textBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.RegionalCode_textBox.Location = new System.Drawing.Point(101, 12);
            this.RegionalCode_textBox.MaxLength = 4;
            this.RegionalCode_textBox.Name = "RegionalCode_textBox";
            this.RegionalCode_textBox.Size = new System.Drawing.Size(156, 19);
            this.RegionalCode_textBox.TabIndex = 1;
            this.RegionalCode_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "※No";
            // 
            // Update_button
            // 
            this.Update_button.Location = new System.Drawing.Point(20, 83);
            this.Update_button.Name = "Update_button";
            this.Update_button.Size = new System.Drawing.Size(75, 23);
            this.Update_button.TabIndex = 7;
            this.Update_button.Text = "更新";
            this.Update_button.UseVisualStyleBackColor = true;
            this.Update_button.Click += new System.EventHandler(this.Update_button_Click);
            // 
            // Delete_button
            // 
            this.Delete_button.Location = new System.Drawing.Point(101, 83);
            this.Delete_button.Name = "Delete_button";
            this.Delete_button.Size = new System.Drawing.Size(75, 23);
            this.Delete_button.TabIndex = 8;
            this.Delete_button.Text = "削除";
            this.Delete_button.UseVisualStyleBackColor = true;
            this.Delete_button.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // Close_button
            // 
            this.Close_button.Location = new System.Drawing.Point(182, 83);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(75, 23);
            this.Close_button.TabIndex = 9;
            this.Close_button.Text = "閉じる";
            this.Close_button.UseVisualStyleBackColor = true;
            this.Close_button.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "※国名";
            // 
            // RegionalName_textBox
            // 
            this.RegionalName_textBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.RegionalName_textBox.Location = new System.Drawing.Point(101, 37);
            this.RegionalName_textBox.MaxLength = 50;
            this.RegionalName_textBox.Name = "RegionalName_textBox";
            this.RegionalName_textBox.Size = new System.Drawing.Size(156, 19);
            this.RegionalName_textBox.TabIndex = 2;
            this.RegionalName_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // 国管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(278, 127);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Delete_button);
            this.Controls.Add(this.Update_button);
            this.Controls.Add(this.RegionalName_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RegionalCode_textBox);
            this.Controls.Add(this.label1);
            this.Name = "国管理詳細Form";
            this.Text = "国管理詳細Form";
            this.Load += new System.EventHandler(this.国管理詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox RegionalCode_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Update_button;
        private System.Windows.Forms.Button Delete_button;
        private System.Windows.Forms.Button Close_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RegionalName_textBox;
    }
}