namespace NBaseHonsen
{
    partial class 一括受領確認Form
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
            this.button_はい = new System.Windows.Forms.Button();
            this.button_いいえ = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(132, 96);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "一括受領を行いますか？";
            // 
            // button_はい
            // 
            this.button_はい.BackColor = System.Drawing.SystemColors.Control;
            this.button_はい.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_はい.Location = new System.Drawing.Point(138, 229);
            this.button_はい.Name = "button_はい";
            this.button_はい.Size = new System.Drawing.Size(119, 38);
            this.button_はい.TabIndex = 1;
            this.button_はい.Text = "はい";
            this.button_はい.UseVisualStyleBackColor = false;
            this.button_はい.Click += new System.EventHandler(this.button_はい_Click);
            // 
            // button_いいえ
            // 
            this.button_いいえ.BackColor = System.Drawing.SystemColors.Control;
            this.button_いいえ.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_いいえ.Location = new System.Drawing.Point(327, 229);
            this.button_いいえ.Name = "button_いいえ";
            this.button_いいえ.Size = new System.Drawing.Size(119, 38);
            this.button_いいえ.TabIndex = 2;
            this.button_いいえ.Text = "いいえ";
            this.button_いいえ.UseVisualStyleBackColor = false;
            this.button_いいえ.Click += new System.EventHandler(this.button_いいえ_Click);
            // 
            // 一括受領確認Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(602, 334);
            this.Controls.Add(this.button_いいえ);
            this.Controls.Add(this.button_はい);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "一括受領確認Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_はい;
        private System.Windows.Forms.Button button_いいえ;
    }
}