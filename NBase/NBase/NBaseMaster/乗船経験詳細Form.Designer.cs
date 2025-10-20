namespace NBaseMaster
{
    partial class 乗船経験詳細Form
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
            this.comboBox職名 = new System.Windows.Forms.ComboBox();
            this.label職名 = new System.Windows.Forms.Label();
            this.label積荷 = new System.Windows.Forms.Label();
            this.comboBox積荷 = new System.Windows.Forms.ComboBox();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button更新 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox日数 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.label船名 = new System.Windows.Forms.Label();
            this.radioButton_乗船経験 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButton_積荷経験 = new System.Windows.Forms.RadioButton();
            this.radioButton_外航経験 = new System.Windows.Forms.RadioButton();
            this.button削除 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox職名
            // 
            this.comboBox職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名.FormattingEnabled = true;
            this.comboBox職名.Location = new System.Drawing.Point(107, 100);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(197, 20);
            this.comboBox職名.TabIndex = 27;
            // 
            // label職名
            // 
            this.label職名.AutoSize = true;
            this.label職名.Location = new System.Drawing.Point(24, 103);
            this.label職名.Name = "label職名";
            this.label職名.Size = new System.Drawing.Size(41, 12);
            this.label職名.TabIndex = 28;
            this.label職名.Text = "※職名";
            // 
            // label積荷
            // 
            this.label積荷.AutoSize = true;
            this.label積荷.Location = new System.Drawing.Point(24, 132);
            this.label積荷.Name = "label積荷";
            this.label積荷.Size = new System.Drawing.Size(41, 12);
            this.label積荷.TabIndex = 28;
            this.label積荷.Text = "※積荷";
            // 
            // comboBox積荷
            // 
            this.comboBox積荷.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox積荷.FormattingEnabled = true;
            this.comboBox積荷.Location = new System.Drawing.Point(107, 129);
            this.comboBox積荷.Name = "comboBox積荷";
            this.comboBox積荷.Size = new System.Drawing.Size(197, 20);
            this.comboBox積荷.TabIndex = 27;
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(229, 234);
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
            this.button更新.Location = new System.Drawing.Point(67, 234);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 38;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "※回数";
            // 
            // textBox日数
            // 
            this.textBox日数.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox日数.Location = new System.Drawing.Point(107, 174);
            this.textBox日数.MaxLength = 3;
            this.textBox日数.Name = "textBox日数";
            this.textBox日数.Size = new System.Drawing.Size(73, 19);
            this.textBox日数.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "回以上";
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(107, 71);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(197, 20);
            this.comboBox船.TabIndex = 41;
            // 
            // label船名
            // 
            this.label船名.AutoSize = true;
            this.label船名.Location = new System.Drawing.Point(24, 74);
            this.label船名.Name = "label船名";
            this.label船名.Size = new System.Drawing.Size(41, 12);
            this.label船名.TabIndex = 42;
            this.label船名.Text = "※船名";
            // 
            // radioButton_乗船経験
            // 
            this.radioButton_乗船経験.AutoSize = true;
            this.radioButton_乗船経験.Location = new System.Drawing.Point(107, 32);
            this.radioButton_乗船経験.Name = "radioButton_乗船経験";
            this.radioButton_乗船経験.Size = new System.Drawing.Size(71, 16);
            this.radioButton_乗船経験.TabIndex = 43;
            this.radioButton_乗船経験.TabStop = true;
            this.radioButton_乗船経験.Text = "乗船経験";
            this.radioButton_乗船経験.UseVisualStyleBackColor = true;
            this.radioButton_乗船経験.CheckedChanged += new System.EventHandler(this.radioButton_乗船経験_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 42;
            this.label5.Text = "※経験区分";
            // 
            // radioButton_積荷経験
            // 
            this.radioButton_積荷経験.AutoSize = true;
            this.radioButton_積荷経験.Location = new System.Drawing.Point(191, 32);
            this.radioButton_積荷経験.Name = "radioButton_積荷経験";
            this.radioButton_積荷経験.Size = new System.Drawing.Size(71, 16);
            this.radioButton_積荷経験.TabIndex = 43;
            this.radioButton_積荷経験.TabStop = true;
            this.radioButton_積荷経験.Text = "積荷経験";
            this.radioButton_積荷経験.UseVisualStyleBackColor = true;
            this.radioButton_積荷経験.CheckedChanged += new System.EventHandler(this.radioButton_積荷経験_CheckedChanged);
            // 
            // radioButton_外航経験
            // 
            this.radioButton_外航経験.AutoSize = true;
            this.radioButton_外航経験.Location = new System.Drawing.Point(275, 32);
            this.radioButton_外航経験.Name = "radioButton_外航経験";
            this.radioButton_外航経験.Size = new System.Drawing.Size(71, 16);
            this.radioButton_外航経験.TabIndex = 43;
            this.radioButton_外航経験.TabStop = true;
            this.radioButton_外航経験.Text = "外航経験";
            this.radioButton_外航経験.UseVisualStyleBackColor = true;
            this.radioButton_外航経験.CheckedChanged += new System.EventHandler(this.radioButton_外航経験_CheckedChanged);
            // 
            // button削除
            // 
            this.button削除.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(148, 234);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 44;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // 乗船経験詳細Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(370, 269);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.radioButton_外航経験);
            this.Controls.Add(this.radioButton_積荷経験);
            this.Controls.Add(this.radioButton_乗船経験);
            this.Controls.Add(this.comboBox船);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label船名);
            this.Controls.Add(this.textBox日数);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.comboBox積荷);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label積荷);
            this.Controls.Add(this.comboBox職名);
            this.Controls.Add(this.label職名);
            this.Name = "乗船経験詳細Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "乗船経験詳細Form";
            this.Load += new System.EventHandler(this.乗船経験詳細Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox職名;
        private System.Windows.Forms.Label label職名;
        private System.Windows.Forms.Label label積荷;
        private System.Windows.Forms.ComboBox comboBox積荷;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox日数;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label船名;
        private System.Windows.Forms.RadioButton radioButton_乗船経験;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButton_積荷経験;
        private System.Windows.Forms.RadioButton radioButton_外航経験;
        private System.Windows.Forms.Button button削除;
    }
}