namespace NBaseMaster.場所管理
{
    partial class 場所管理詳細Form
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
            this.BashoNo_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Update_button = new System.Windows.Forms.Button();
            this.Delete_button = new System.Windows.Forms.Button();
            this.Close_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.BashoName_textBox = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BashoKubun_comboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // BashoNo_textBox
            // 
            this.BashoNo_textBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.BashoNo_textBox.Location = new System.Drawing.Point(101, 12);
            this.BashoNo_textBox.MaxLength = 4;
            this.BashoNo_textBox.Name = "BashoNo_textBox";
            this.BashoNo_textBox.Size = new System.Drawing.Size(156, 19);
            this.BashoNo_textBox.TabIndex = 1;
            this.BashoNo_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "※港No";
            // 
            // Update_button
            // 
            this.Update_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Update_button.Location = new System.Drawing.Point(28, 103);
            this.Update_button.Name = "Update_button";
            this.Update_button.Size = new System.Drawing.Size(75, 23);
            this.Update_button.TabIndex = 7;
            this.Update_button.Text = "更新";
            this.Update_button.UseVisualStyleBackColor = true;
            this.Update_button.Click += new System.EventHandler(this.Update_button_Click);
            // 
            // Delete_button
            // 
            this.Delete_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Delete_button.Location = new System.Drawing.Point(109, 103);
            this.Delete_button.Name = "Delete_button";
            this.Delete_button.Size = new System.Drawing.Size(75, 23);
            this.Delete_button.TabIndex = 8;
            this.Delete_button.Text = "削除";
            this.Delete_button.UseVisualStyleBackColor = true;
            this.Delete_button.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // Close_button
            // 
            this.Close_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Close_button.Location = new System.Drawing.Point(190, 103);
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
            this.label2.Text = "※港名";
            // 
            // BashoName_textBox
            // 
            this.BashoName_textBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.BashoName_textBox.Location = new System.Drawing.Point(101, 37);
            this.BashoName_textBox.MaxLength = 50;
            this.BashoName_textBox.Name = "BashoName_textBox";
            this.BashoName_textBox.Size = new System.Drawing.Size(156, 19);
            this.BashoName_textBox.TabIndex = 2;
            this.BashoName_textBox.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(101, 88);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "内航";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Visible = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.DataChange);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(154, 88);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "外航";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Visible = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.DataChange);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "※区分";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "※場所区分";
            this.label4.Visible = false;
            // 
            // BashoKubun_comboBox
            // 
            this.BashoKubun_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BashoKubun_comboBox.FormattingEnabled = true;
            this.BashoKubun_comboBox.Location = new System.Drawing.Point(101, 62);
            this.BashoKubun_comboBox.Name = "BashoKubun_comboBox";
            this.BashoKubun_comboBox.Size = new System.Drawing.Size(156, 20);
            this.BashoKubun_comboBox.TabIndex = 5;
            this.BashoKubun_comboBox.Visible = false;
            this.BashoKubun_comboBox.SelectedIndexChanged += new System.EventHandler(this.DataChange);
            // 
            // 場所管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 140);
            this.Controls.Add(this.BashoKubun_comboBox);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Delete_button);
            this.Controls.Add(this.Update_button);
            this.Controls.Add(this.BashoName_textBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BashoNo_textBox);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(308, 212);
            this.Name = "場所管理詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "港管理詳細Form";
            this.Load += new System.EventHandler(this.場所管理詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox BashoNo_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Update_button;
        private System.Windows.Forms.Button Delete_button;
        private System.Windows.Forms.Button Close_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox BashoName_textBox;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox BashoKubun_comboBox;
    }
}