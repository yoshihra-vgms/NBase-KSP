namespace NBaseMaster.船員管理
{
    partial class 給与計算複製作成Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(給与計算複製作成Form));
            this.nullableDateTimePicker_from = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker_to = new NBaseUtil.NullableDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button複製 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.textBoxTo = new System.Windows.Forms.TextBox();
            this.textBoxFrom = new System.Windows.Forms.TextBox();
            this.textBoxKubun = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // nullableDateTimePicker_from
            // 
            this.nullableDateTimePicker_from.Location = new System.Drawing.Point(78, 129);
            this.nullableDateTimePicker_from.Name = "nullableDateTimePicker_from";
            this.nullableDateTimePicker_from.Size = new System.Drawing.Size(119, 19);
            this.nullableDateTimePicker_from.TabIndex = 9;
            this.nullableDateTimePicker_from.Value = null;
            this.nullableDateTimePicker_from.ValueChanged += new System.EventHandler(this.nullableDateTimePicker_from_ValueChanged);
            // 
            // nullableDateTimePicker_to
            // 
            this.nullableDateTimePicker_to.Location = new System.Drawing.Point(226, 129);
            this.nullableDateTimePicker_to.Name = "nullableDateTimePicker_to";
            this.nullableDateTimePicker_to.Size = new System.Drawing.Size(119, 19);
            this.nullableDateTimePicker_to.TabIndex = 10;
            this.nullableDateTimePicker_to.Value = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "期間";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "〜";
            // 
            // button複製
            // 
            this.button複製.BackColor = System.Drawing.SystemColors.Control;
            this.button複製.Location = new System.Drawing.Point(106, 175);
            this.button複製.Name = "button複製";
            this.button複製.Size = new System.Drawing.Size(75, 23);
            this.button複製.TabIndex = 21;
            this.button複製.Text = "複製";
            this.button複製.UseVisualStyleBackColor = false;
            this.button複製.Click += new System.EventHandler(this.button複製_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(198, 175);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 22;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // textBoxTo
            // 
            this.textBoxTo.Location = new System.Drawing.Point(226, 57);
            this.textBoxTo.Name = "textBoxTo";
            this.textBoxTo.ReadOnly = true;
            this.textBoxTo.Size = new System.Drawing.Size(119, 19);
            this.textBoxTo.TabIndex = 27;
            this.textBoxTo.TabStop = false;
            // 
            // textBoxFrom
            // 
            this.textBoxFrom.Location = new System.Drawing.Point(78, 57);
            this.textBoxFrom.Name = "textBoxFrom";
            this.textBoxFrom.ReadOnly = true;
            this.textBoxFrom.Size = new System.Drawing.Size(119, 19);
            this.textBoxFrom.TabIndex = 28;
            this.textBoxFrom.TabStop = false;
            // 
            // textBoxKubun
            // 
            this.textBoxKubun.Location = new System.Drawing.Point(78, 26);
            this.textBoxKubun.Name = "textBoxKubun";
            this.textBoxKubun.ReadOnly = true;
            this.textBoxKubun.Size = new System.Drawing.Size(119, 19);
            this.textBoxKubun.TabIndex = 29;
            this.textBoxKubun.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(203, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "〜";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "期間";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 24;
            this.label3.Text = "区分";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(172, 86);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 33);
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // 給与計算複製作成Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(379, 223);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBoxTo);
            this.Controls.Add(this.textBoxFrom);
            this.Controls.Add(this.textBoxKubun);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button複製);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nullableDateTimePicker_to);
            this.Controls.Add(this.nullableDateTimePicker_from);
            this.Name = "給与計算複製作成Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "給与計算-複製";
            this.Load += new System.EventHandler(this.給与計算複製作成Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker_from;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker_to;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button複製;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.TextBox textBoxTo;
        private System.Windows.Forms.TextBox textBoxFrom;
        private System.Windows.Forms.TextBox textBoxKubun;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}