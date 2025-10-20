namespace Dousei
{
    partial class 動静詳細Form
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
            this.VesselName_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Update_button = new System.Windows.Forms.Button();
            this.douseiPanel1 = new Dousei.Control.DouseiPanel();
            this.Delete_button = new System.Windows.Forms.Button();
            this.Close_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // VesselName_textBox
            // 
            this.VesselName_textBox.Location = new System.Drawing.Point(47, 6);
            this.VesselName_textBox.Name = "VesselName_textBox";
            this.VesselName_textBox.ReadOnly = true;
            this.VesselName_textBox.Size = new System.Drawing.Size(157, 19);
            this.VesselName_textBox.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "船名";
            // 
            // Update_button
            // 
            this.Update_button.BackColor = System.Drawing.SystemColors.Control;
            this.Update_button.Location = new System.Drawing.Point(694, 112);
            this.Update_button.Name = "Update_button";
            this.Update_button.Size = new System.Drawing.Size(99, 40);
            this.Update_button.TabIndex = 9;
            this.Update_button.Text = "更新";
            this.Update_button.UseVisualStyleBackColor = false;
            this.Update_button.Click += new System.EventHandler(this.Update_button_Click);
            // 
            // douseiPanel1
            // 
            this.douseiPanel1.Location = new System.Drawing.Point(12, 33);
            this.douseiPanel1.Name = "douseiPanel1";
            this.douseiPanel1.Size = new System.Drawing.Size(991, 644);
            this.douseiPanel1.TabIndex = 25;
            // 
            // Delete_button
            // 
            this.Delete_button.BackColor = System.Drawing.SystemColors.Control;
            this.Delete_button.Location = new System.Drawing.Point(799, 112);
            this.Delete_button.Name = "Delete_button";
            this.Delete_button.Size = new System.Drawing.Size(99, 40);
            this.Delete_button.TabIndex = 9;
            this.Delete_button.Text = "削除";
            this.Delete_button.UseVisualStyleBackColor = false;
            this.Delete_button.Click += new System.EventHandler(this.Delete_button_Click);
            // 
            // Close_button
            // 
            this.Close_button.BackColor = System.Drawing.SystemColors.Control;
            this.Close_button.Location = new System.Drawing.Point(904, 112);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(99, 40);
            this.Close_button.TabIndex = 9;
            this.Close_button.Text = "閉じる";
            this.Close_button.UseVisualStyleBackColor = false;
            this.Close_button.Click += new System.EventHandler(this.Close_button_Click);
            // 
            // 動静詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1016, 678);
            this.Controls.Add(this.Close_button);
            this.Controls.Add(this.Delete_button);
            this.Controls.Add(this.Update_button);
            this.Controls.Add(this.douseiPanel1);
            this.Controls.Add(this.VesselName_textBox);
            this.Controls.Add(this.label2);
            this.Name = "動静詳細Form";
            this.Text = "動静詳細Form";
            this.Load += new System.EventHandler(this.動静詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox VesselName_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Update_button;
        private Dousei.Control.DouseiPanel douseiPanel1;
        private System.Windows.Forms.Button Delete_button;
        private System.Windows.Forms.Button Close_button;

    }
}