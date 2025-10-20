
namespace Hachu
{
    partial class 発注修正Form
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
            this.textBox_手配依頼番号 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_区分 = new System.Windows.Forms.ComboBox();
            this.comboBox_仕様型式 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "手配依頼番号";
            // 
            // textBox_手配依頼番号
            // 
            this.textBox_手配依頼番号.Location = new System.Drawing.Point(155, 43);
            this.textBox_手配依頼番号.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox_手配依頼番号.Name = "textBox_手配依頼番号";
            this.textBox_手配依頼番号.Size = new System.Drawing.Size(276, 22);
            this.textBox_手配依頼番号.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 136);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "区分";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 169);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "仕様・型式";
            // 
            // comboBox_区分
            // 
            this.comboBox_区分.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_区分.FormattingEnabled = true;
            this.comboBox_区分.Location = new System.Drawing.Point(155, 132);
            this.comboBox_区分.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_区分.Name = "comboBox_区分";
            this.comboBox_区分.Size = new System.Drawing.Size(276, 23);
            this.comboBox_区分.TabIndex = 2;
            this.comboBox_区分.SelectedIndexChanged += new System.EventHandler(this.comboBox_区分_SelectedIndexChanged);
            // 
            // comboBox_仕様型式
            // 
            this.comboBox_仕様型式.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_仕様型式.FormattingEnabled = true;
            this.comboBox_仕様型式.Location = new System.Drawing.Point(155, 166);
            this.comboBox_仕様型式.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox_仕様型式.Name = "comboBox_仕様型式";
            this.comboBox_仕様型式.Size = new System.Drawing.Size(276, 23);
            this.comboBox_仕様型式.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(188, 236);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 29);
            this.button1.TabIndex = 3;
            this.button1.Text = "変更";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // 発注修正Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 308);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox_仕様型式);
            this.Controls.Add(this.comboBox_区分);
            this.Controls.Add(this.textBox_手配依頼番号);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "発注修正Form";
            this.Text = "発注修正Form";
            this.Load += new System.EventHandler(this.発注修正Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_手配依頼番号;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_区分;
        private System.Windows.Forms.ComboBox comboBox_仕様型式;
        private System.Windows.Forms.Button button1;
    }
}