
namespace Senin
{
    partial class 基本給新規作成Form
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
            this.label3 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker_from = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker_to = new NBaseUtil.NullableDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button新規 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.radioButton部員 = new System.Windows.Forms.RadioButton();
            this.radioButton下級海技士 = new System.Windows.Forms.RadioButton();
            this.radioButton航機通砲手 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "種別";
            // 
            // nullableDateTimePicker_from
            // 
            this.nullableDateTimePicker_from.Location = new System.Drawing.Point(78, 62);
            this.nullableDateTimePicker_from.Name = "nullableDateTimePicker_from";
            this.nullableDateTimePicker_from.Size = new System.Drawing.Size(119, 19);
            this.nullableDateTimePicker_from.TabIndex = 9;
            this.nullableDateTimePicker_from.Value = null;
            // 
            // nullableDateTimePicker_to
            // 
            this.nullableDateTimePicker_to.Location = new System.Drawing.Point(226, 62);
            this.nullableDateTimePicker_to.Name = "nullableDateTimePicker_to";
            this.nullableDateTimePicker_to.Size = new System.Drawing.Size(119, 19);
            this.nullableDateTimePicker_to.TabIndex = 10;
            this.nullableDateTimePicker_to.Value = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "期間";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "〜";
            // 
            // button新規
            // 
            this.button新規.BackColor = System.Drawing.SystemColors.Control;
            this.button新規.Location = new System.Drawing.Point(106, 108);
            this.button新規.Name = "button新規";
            this.button新規.Size = new System.Drawing.Size(75, 23);
            this.button新規.TabIndex = 21;
            this.button新規.Text = "追加";
            this.button新規.UseVisualStyleBackColor = false;
            this.button新規.Click += new System.EventHandler(this.button新規_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(198, 108);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 22;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // radioButton部員
            // 
            this.radioButton部員.AutoSize = true;
            this.radioButton部員.Location = new System.Drawing.Point(278, 30);
            this.radioButton部員.Name = "radioButton部員";
            this.radioButton部員.Size = new System.Drawing.Size(47, 16);
            this.radioButton部員.TabIndex = 24;
            this.radioButton部員.TabStop = true;
            this.radioButton部員.Text = "部員";
            this.radioButton部員.UseVisualStyleBackColor = true;
            // 
            // radioButton下級海技士
            // 
            this.radioButton下級海技士.AutoSize = true;
            this.radioButton下級海技士.Location = new System.Drawing.Point(173, 30);
            this.radioButton下級海技士.Name = "radioButton下級海技士";
            this.radioButton下級海技士.Size = new System.Drawing.Size(93, 16);
            this.radioButton下級海技士.TabIndex = 25;
            this.radioButton下級海技士.TabStop = true;
            this.radioButton下級海技士.Text = "４・５級海技士";
            this.radioButton下級海技士.UseVisualStyleBackColor = true;
            // 
            // radioButton航機通砲手
            // 
            this.radioButton航機通砲手.AutoSize = true;
            this.radioButton航機通砲手.Location = new System.Drawing.Point(78, 30);
            this.radioButton航機通砲手.Name = "radioButton航機通砲手";
            this.radioButton航機通砲手.Size = new System.Drawing.Size(83, 16);
            this.radioButton航機通砲手.TabIndex = 26;
            this.radioButton航機通砲手.TabStop = true;
            this.radioButton航機通砲手.Text = "航機通砲手";
            this.radioButton航機通砲手.UseVisualStyleBackColor = true;
            // 
            // 基本給新規作成Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(379, 160);
            this.Controls.Add(this.radioButton部員);
            this.Controls.Add(this.radioButton下級海技士);
            this.Controls.Add(this.radioButton航機通砲手);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button新規);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nullableDateTimePicker_to);
            this.Controls.Add(this.nullableDateTimePicker_from);
            this.Controls.Add(this.label3);
            this.Name = "基本給新規作成Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基本給新規作成";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker_from;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker_to;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button新規;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.RadioButton radioButton部員;
        private System.Windows.Forms.RadioButton radioButton下級海技士;
        private System.Windows.Forms.RadioButton radioButton航機通砲手;
    }
}