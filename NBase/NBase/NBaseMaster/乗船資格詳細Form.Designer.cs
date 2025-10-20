namespace NBaseMaster
{
    partial class 乗船資格詳細Form
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
            this.comboBox免許免状 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox免許免状種別 = new System.Windows.Forms.ComboBox();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button更新 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox免許免状
            // 
            this.comboBox免許免状.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox免許免状.FormattingEnabled = true;
            this.comboBox免許免状.Location = new System.Drawing.Point(99, 21);
            this.comboBox免許免状.Name = "comboBox免許免状";
            this.comboBox免許免状.Size = new System.Drawing.Size(184, 20);
            this.comboBox免許免状.TabIndex = 27;
            this.comboBox免許免状.SelectedIndexChanged += new System.EventHandler(this.comboBox免許免状_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 28;
            this.label1.Text = "※免許／免状";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "　 種別";
            // 
            // comboBox免許免状種別
            // 
            this.comboBox免許免状種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox免許免状種別.FormattingEnabled = true;
            this.comboBox免許免状種別.Location = new System.Drawing.Point(99, 50);
            this.comboBox免許免状種別.Name = "comboBox免許免状種別";
            this.comboBox免許免状種別.Size = new System.Drawing.Size(184, 20);
            this.comboBox免許免状種別.TabIndex = 27;
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(154, 103);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 39;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(73, 103);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 38;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // 乗船資格詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(302, 138);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.comboBox免許免状種別);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox免許免状);
            this.Controls.Add(this.label1);
            this.Name = "乗船資格詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "乗船資格詳細Form";
            this.Load += new System.EventHandler(this.乗船資格詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox免許免状;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox免許免状種別;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button更新;
    }
}