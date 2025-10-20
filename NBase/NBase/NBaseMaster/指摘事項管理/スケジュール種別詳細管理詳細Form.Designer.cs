namespace NBaseMaster.指摘事項管理
{
    partial class スケジュール種別詳細管理詳細Form
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
            this.button閉じる = new System.Windows.Forms.Button();
            this.button削除 = new System.Windows.Forms.Button();
            this.button更新 = new System.Windows.Forms.Button();
            this.textBox名 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox種別 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(230, 123);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 6;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(149, 123);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 5;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Location = new System.Drawing.Point(68, 123);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 4;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // textBox名
            // 
            this.textBox名.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBox名.Location = new System.Drawing.Point(106, 40);
            this.textBox名.MaxLength = 50;
            this.textBox名.Name = "textBox名";
            this.textBox名.Size = new System.Drawing.Size(225, 19);
            this.textBox名.TabIndex = 2;
            this.textBox名.TextChanged += new System.EventHandler(this.ChangeDataText);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "※詳細名";
            // 
            // comboBox種別
            // 
            this.comboBox種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox種別.FormattingEnabled = true;
            this.comboBox種別.Location = new System.Drawing.Point(106, 12);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(225, 20);
            this.comboBox種別.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 29;
            this.label3.Text = "※種別";
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(106, 67);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(33, 19);
            this.pictureBox.TabIndex = 38;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 39;
            this.label2.Text = "※背景色";
            // 
            // スケジュール種別詳細管理詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(378, 158);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.comboBox種別);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.textBox名);
            this.Controls.Add(this.label1);
            this.Name = "スケジュール種別詳細管理詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "スケジュール種別詳細管理詳細Form";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button削除;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.TextBox textBox名;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox種別;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}