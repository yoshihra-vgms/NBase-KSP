namespace NBaseHonsen.Document
{
    partial class コメント登録Form
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
            this.button_コメント登録 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button_閉じる = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_Bunrui = new System.Windows.Forms.Label();
            this.label_Shoubunrui = new System.Windows.Forms.Label();
            this.label_BunshoNo = new System.Windows.Forms.Label();
            this.label_BunshoName = new System.Windows.Forms.Label();
            this.nullableDateTimePicker_RegDate = new NBaseUtil.NullableDateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button_参照 = new System.Windows.Forms.Button();
            this.textBox_FileName = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_コメント登録
            // 
            this.button_コメント登録.Location = new System.Drawing.Point(590, 3);
            this.button_コメント登録.Name = "button_コメント登録";
            this.button_コメント登録.Size = new System.Drawing.Size(110, 30);
            this.button_コメント登録.TabIndex = 1;
            this.button_コメント登録.Text = "コメント登録";
            this.button_コメント登録.UseVisualStyleBackColor = true;
            this.button_コメント登録.Click += new System.EventHandler(this.button_コメント登録_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.button_閉じる);
            this.flowLayoutPanel1.Controls.Add(this.button_コメント登録);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(819, 35);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // button_閉じる
            // 
            this.button_閉じる.Location = new System.Drawing.Point(706, 3);
            this.button_閉じる.Name = "button_閉じる";
            this.button_閉じる.Size = new System.Drawing.Size(110, 30);
            this.button_閉じる.TabIndex = 3;
            this.button_閉じる.Text = "閉じる";
            this.button_閉じる.UseVisualStyleBackColor = true;
            this.button_閉じる.Click += new System.EventHandler(this.button_閉じる_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(24, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "分類";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(24, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "小分類";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(24, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "文書番号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(24, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "文書名";
            // 
            // label_Bunrui
            // 
            this.label_Bunrui.AutoSize = true;
            this.label_Bunrui.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Bunrui.Location = new System.Drawing.Point(99, 38);
            this.label_Bunrui.Name = "label_Bunrui";
            this.label_Bunrui.Size = new System.Drawing.Size(16, 16);
            this.label_Bunrui.TabIndex = 7;
            this.label_Bunrui.Text = "：";
            // 
            // label_Shoubunrui
            // 
            this.label_Shoubunrui.AutoSize = true;
            this.label_Shoubunrui.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Shoubunrui.Location = new System.Drawing.Point(99, 63);
            this.label_Shoubunrui.Name = "label_Shoubunrui";
            this.label_Shoubunrui.Size = new System.Drawing.Size(16, 16);
            this.label_Shoubunrui.TabIndex = 8;
            this.label_Shoubunrui.Text = "：";
            // 
            // label_BunshoNo
            // 
            this.label_BunshoNo.AutoSize = true;
            this.label_BunshoNo.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_BunshoNo.Location = new System.Drawing.Point(99, 89);
            this.label_BunshoNo.Name = "label_BunshoNo";
            this.label_BunshoNo.Size = new System.Drawing.Size(16, 16);
            this.label_BunshoNo.TabIndex = 9;
            this.label_BunshoNo.Text = "：";
            // 
            // label_BunshoName
            // 
            this.label_BunshoName.AutoSize = true;
            this.label_BunshoName.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_BunshoName.Location = new System.Drawing.Point(99, 117);
            this.label_BunshoName.Name = "label_BunshoName";
            this.label_BunshoName.Size = new System.Drawing.Size(16, 16);
            this.label_BunshoName.TabIndex = 10;
            this.label_BunshoName.Text = "：";
            // 
            // nullableDateTimePicker_RegDate
            // 
            this.nullableDateTimePicker_RegDate.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nullableDateTimePicker_RegDate.Location = new System.Drawing.Point(102, 142);
            this.nullableDateTimePicker_RegDate.Name = "nullableDateTimePicker_RegDate";
            this.nullableDateTimePicker_RegDate.Size = new System.Drawing.Size(142, 23);
            this.nullableDateTimePicker_RegDate.TabIndex = 14;
            this.nullableDateTimePicker_RegDate.Value = new System.DateTime(2010, 8, 23, 2, 9, 20, 546);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(8, 145);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 16);
            this.label10.TabIndex = 15;
            this.label10.Text = "※登録日";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(8, 173);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 16);
            this.label11.TabIndex = 16;
            this.label11.Text = "※ファイル";
            // 
            // button_参照
            // 
            this.button_参照.BackColor = System.Drawing.Color.White;
            this.button_参照.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_参照.Location = new System.Drawing.Point(593, 166);
            this.button_参照.Name = "button_参照";
            this.button_参照.Size = new System.Drawing.Size(100, 30);
            this.button_参照.TabIndex = 27;
            this.button_参照.Text = "参照";
            this.button_参照.UseVisualStyleBackColor = false;
            this.button_参照.Click += new System.EventHandler(this.button_参照_Click);
            // 
            // textBox_FileName
            // 
            this.textBox_FileName.AllowDrop = true;
            this.textBox_FileName.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_FileName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox_FileName.Location = new System.Drawing.Point(102, 170);
            this.textBox_FileName.Name = "textBox_FileName";
            this.textBox_FileName.ReadOnly = true;
            this.textBox_FileName.Size = new System.Drawing.Size(475, 23);
            this.textBox_FileName.TabIndex = 26;
            this.textBox_FileName.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_FileName_DragDrop);
            this.textBox_FileName.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_FileName_DragEnter);
            // 
            // コメント登録Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(819, 220);
            this.Controls.Add(this.button_参照);
            this.Controls.Add(this.textBox_FileName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.nullableDateTimePicker_RegDate);
            this.Controls.Add(this.label_BunshoName);
            this.Controls.Add(this.label_BunshoNo);
            this.Controls.Add(this.label_Shoubunrui);
            this.Controls.Add(this.label_Bunrui);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "コメント登録Form";
            this.Text = "コメント登録";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.コメント登録Form_FormClosed);
            this.Load += new System.EventHandler(this.コメント登録Form_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_コメント登録;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button_閉じる;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_Bunrui;
        private System.Windows.Forms.Label label_Shoubunrui;
        private System.Windows.Forms.Label label_BunshoNo;
        private System.Windows.Forms.Label label_BunshoName;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker_RegDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button_参照;
        private System.Windows.Forms.TextBox textBox_FileName;
    }
}