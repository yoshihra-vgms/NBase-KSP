namespace NBaseMaster.輸送品目管理
{
    partial class 輸送品目管理詳細Form
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
            this.YusoItemCode_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Update_button = new System.Windows.Forms.Button();
            this.Delete_button = new System.Windows.Forms.Button();
            this.Close_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.YusoItemName_textBox = new System.Windows.Forms.TextBox();
            this.SenshuName_textBox = new System.Windows.Forms.TextBox();
            this.SenshuCode_textBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // YusoItemCode_textBox
            // 
            this.YusoItemCode_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.YusoItemCode_textBox.Location = new System.Drawing.Point(119, 18);
            this.YusoItemCode_textBox.MaxLength = 10;
            this.YusoItemCode_textBox.Name = "YusoItemCode_textBox";
            this.YusoItemCode_textBox.Size = new System.Drawing.Size(100, 19);
            this.YusoItemCode_textBox.TabIndex = 0;
            this.YusoItemCode_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "※輸送品目コード";
            // 
            // Update_button
            // 
            this.Update_button.Location = new System.Drawing.Point(75, 136);
            this.Update_button.Name = "Update_button";
            this.Update_button.Size = new System.Drawing.Size(75, 23);
            this.Update_button.TabIndex = 5;
            this.Update_button.Text = "更新";
            this.Update_button.UseVisualStyleBackColor = true;
            this.Update_button.Click += new System.EventHandler(this.Update_button_Click);
            // 
            // Delete_button
            // 
            this.Delete_button.Location = new System.Drawing.Point(156, 136);
            this.Delete_button.Name = "Delete_button";
            this.Delete_button.Size = new System.Drawing.Size(75, 23);
            this.Delete_button.TabIndex = 6;
            this.Delete_button.Text = "削除";
            this.Delete_button.UseVisualStyleBackColor = true;
            this.Delete_button.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // Close_button
            // 
            this.Close_button.Location = new System.Drawing.Point(237, 136);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(75, 23);
            this.Close_button.TabIndex = 7;
            this.Close_button.Text = "閉じる";
            this.Close_button.UseVisualStyleBackColor = true;
            this.Close_button.Click += new System.EventHandler(this.Close_button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "※輸送品目名";
            // 
            // YusoItemName_textBox
            // 
            this.YusoItemName_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.YusoItemName_textBox.Location = new System.Drawing.Point(119, 43);
            this.YusoItemName_textBox.MaxLength = 50;
            this.YusoItemName_textBox.Name = "YusoItemName_textBox";
            this.YusoItemName_textBox.Size = new System.Drawing.Size(250, 19);
            this.YusoItemName_textBox.TabIndex = 1;
            this.YusoItemName_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // SenshuName_textBox
            // 
            this.SenshuName_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.SenshuName_textBox.Location = new System.Drawing.Point(119, 93);
            this.SenshuName_textBox.MaxLength = 50;
            this.SenshuName_textBox.Name = "SenshuName_textBox";
            this.SenshuName_textBox.Size = new System.Drawing.Size(250, 19);
            this.SenshuName_textBox.TabIndex = 3;
            this.SenshuName_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // SenshuCode_textBox
            // 
            this.SenshuCode_textBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SenshuCode_textBox.Location = new System.Drawing.Point(119, 68);
            this.SenshuCode_textBox.MaxLength = 10;
            this.SenshuCode_textBox.Name = "SenshuCode_textBox";
            this.SenshuCode_textBox.Size = new System.Drawing.Size(100, 19);
            this.SenshuCode_textBox.TabIndex = 2;
            this.SenshuCode_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "※船種名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "※船種コード";
            // 
            // 輸送品目管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(387, 179);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SenshuName_textBox);
            this.Controls.Add(this.SenshuCode_textBox);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Delete_button);
            this.Controls.Add(this.Update_button);
            this.Controls.Add(this.YusoItemName_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.YusoItemCode_textBox);
            this.Controls.Add(this.label1);
            this.Name = "輸送品目管理詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "輸送品目管理詳細Form";
            this.Load += new System.EventHandler(this.輸送品目管理詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox YusoItemCode_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Update_button;
        private System.Windows.Forms.Button Delete_button;
        private System.Windows.Forms.Button Close_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox YusoItemName_textBox;
        private System.Windows.Forms.TextBox SenshuName_textBox;
        private System.Windows.Forms.TextBox SenshuCode_textBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}