namespace DeficiencyControl.Forms
{
    partial class Form_ProxySetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Address = new System.Windows.Forms.TextBox();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_UserID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Password1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_Password2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button_Update = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Adress";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "URL";
            // 
            // textBox_Address
            // 
            this.textBox_Address.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_Address.Location = new System.Drawing.Point(46, 31);
            this.textBox_Address.MaxLength = 50;
            this.textBox_Address.Name = "textBox_Address";
            this.textBox_Address.Size = new System.Drawing.Size(301, 23);
            this.textBox_Address.TabIndex = 1;
            // 
            // textBox_Port
            // 
            this.textBox_Port.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_Port.Location = new System.Drawing.Point(353, 31);
            this.textBox_Port.MaxLength = 5;
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(59, 23);
            this.textBox_Port.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(350, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Port";
            // 
            // textBox_UserID
            // 
            this.textBox_UserID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_UserID.Location = new System.Drawing.Point(106, 149);
            this.textBox_UserID.MaxLength = 30;
            this.textBox_UserID.Name = "textBox_UserID";
            this.textBox_UserID.Size = new System.Drawing.Size(210, 23);
            this.textBox_UserID.TabIndex = 3;
            this.textBox_UserID.TabStop = false;
            this.textBox_UserID.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "ユーザID";
            this.label4.Visible = false;
            // 
            // textBox_Password1
            // 
            this.textBox_Password1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_Password1.Location = new System.Drawing.Point(106, 173);
            this.textBox_Password1.MaxLength = 30;
            this.textBox_Password1.Name = "textBox_Password1";
            this.textBox_Password1.PasswordChar = '*';
            this.textBox_Password1.Size = new System.Drawing.Size(210, 23);
            this.textBox_Password1.TabIndex = 4;
            this.textBox_Password1.TabStop = false;
            this.textBox_Password1.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "パスワード";
            this.label5.Visible = false;
            // 
            // textBox_Password2
            // 
            this.textBox_Password2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_Password2.Location = new System.Drawing.Point(106, 199);
            this.textBox_Password2.MaxLength = 30;
            this.textBox_Password2.Name = "textBox_Password2";
            this.textBox_Password2.PasswordChar = '*';
            this.textBox_Password2.Size = new System.Drawing.Size(210, 23);
            this.textBox_Password2.TabIndex = 5;
            this.textBox_Password2.TabStop = false;
            this.textBox_Password2.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "パスワード（確認）";
            this.label6.Visible = false;
            // 
            // button_Update
            // 
            this.button_Update.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_Update.Location = new System.Drawing.Point(128, 67);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(75, 23);
            this.button_Update.TabIndex = 3;
            this.button_Update.Text = "Update";
            this.button_Update.UseVisualStyleBackColor = true;
            this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // button_Close
            // 
            this.button_Close.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_Close.Location = new System.Drawing.Point(222, 67);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 4;
            this.button_Close.Text = "Close";
            this.button_Close.UseVisualStyleBackColor = true;
            this.button_Close.Click += new System.EventHandler(this.button_Close_Click);
            // 
            // Form_ProxySetting
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(424, 102);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.button_Update);
            this.Controls.Add(this.textBox_Password2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_Password1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_UserID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_Address);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_ProxySetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.Form_ProxySetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Address;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_UserID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Password1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_Password2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.Button button_Close;
    }
}