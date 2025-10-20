namespace NBaseMaster.Doc.ドキュメント分類管理
{
    partial class ドキュメント分類管理詳細Form
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
            this.textBox_BunruiName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Update_button = new System.Windows.Forms.Button();
            this.Delete_button = new System.Windows.Forms.Button();
            this.Close_button = new System.Windows.Forms.Button();
            this.textBox_BunruiCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_BunruiName
            // 
            this.textBox_BunruiName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_BunruiName.Location = new System.Drawing.Point(134, 43);
            this.textBox_BunruiName.MaxLength = 50;
            this.textBox_BunruiName.Name = "textBox_BunruiName";
            this.textBox_BunruiName.Size = new System.Drawing.Size(180, 19);
            this.textBox_BunruiName.TabIndex = 2;
            this.textBox_BunruiName.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "※ドキュメント分類名";
            // 
            // Update_button
            // 
            this.Update_button.Location = new System.Drawing.Point(49, 86);
            this.Update_button.Name = "Update_button";
            this.Update_button.Size = new System.Drawing.Size(75, 23);
            this.Update_button.TabIndex = 3;
            this.Update_button.Text = "更新";
            this.Update_button.UseVisualStyleBackColor = true;
            this.Update_button.Click += new System.EventHandler(this.Update_button_Click);
            // 
            // Delete_button
            // 
            this.Delete_button.Location = new System.Drawing.Point(130, 86);
            this.Delete_button.Name = "Delete_button";
            this.Delete_button.Size = new System.Drawing.Size(75, 23);
            this.Delete_button.TabIndex = 4;
            this.Delete_button.Text = "削除";
            this.Delete_button.UseVisualStyleBackColor = true;
            this.Delete_button.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // Close_button
            // 
            this.Close_button.Location = new System.Drawing.Point(211, 86);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(75, 23);
            this.Close_button.TabIndex = 5;
            this.Close_button.Text = "閉じる";
            this.Close_button.UseVisualStyleBackColor = true;
            this.Close_button.Click += new System.EventHandler(this.Close_button_Click);
            // 
            // textBox_BunruiCode
            // 
            this.textBox_BunruiCode.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox_BunruiCode.Location = new System.Drawing.Point(134, 18);
            this.textBox_BunruiCode.MaxLength = 4;
            this.textBox_BunruiCode.Name = "textBox_BunruiCode";
            this.textBox_BunruiCode.Size = new System.Drawing.Size(180, 19);
            this.textBox_BunruiCode.TabIndex = 1;
            this.textBox_BunruiCode.TextChanged += new System.EventHandler(this.DataChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "※ドキュメント分類ｺｰﾄﾞ";
            // 
            // ドキュメント分類管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(335, 134);
            this.Controls.Add(this.textBox_BunruiCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Delete_button);
            this.Controls.Add(this.Update_button);
            this.Controls.Add(this.textBox_BunruiName);
            this.Controls.Add(this.label1);
            this.Name = "ドキュメント分類管理詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ドキュメント分類管理詳細Form";
            this.Load += new System.EventHandler(this.ドキュメント分類管理詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_BunruiName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Update_button;
        private System.Windows.Forms.Button Delete_button;
        private System.Windows.Forms.Button Close_button;
        private System.Windows.Forms.TextBox textBox_BunruiCode;
        private System.Windows.Forms.Label label2;
    }
}