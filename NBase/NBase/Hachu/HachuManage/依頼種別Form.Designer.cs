namespace Hachu.HachuManage
{
    partial class 依頼種別Form
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
            this.comboBox種別 = new System.Windows.Forms.ComboBox();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_OK = new System.Windows.Forms.Button();
            this.panel種別 = new System.Windows.Forms.Panel();
            this.label_Message = new System.Windows.Forms.Label();
            this.panel詳細種別 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox詳細種別 = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel見積有無 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox見積有無 = new System.Windows.Forms.ComboBox();
            this.panel船 = new System.Windows.Forms.Panel();
            this.label0 = new System.Windows.Forms.Label();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel種別.SuspendLayout();
            this.panel詳細種別.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel見積有無.SuspendLayout();
            this.panel船.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "種別";
            // 
            // comboBox種別
            // 
            this.comboBox種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox種別.FormattingEnabled = true;
            this.comboBox種別.Items.AddRange(new object[] {
            "船用品",
            "潤滑油",
            "修繕"});
            this.comboBox種別.Location = new System.Drawing.Point(76, 3);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(188, 20);
            this.comboBox種別.TabIndex = 1;
            this.comboBox種別.SelectedIndexChanged += new System.EventHandler(this.comboBox種別_SelectedIndexChanged);
            // 
            // button_Cancel
            // 
            this.button_Cancel.BackColor = System.Drawing.SystemColors.Control;
            this.button_Cancel.Location = new System.Drawing.Point(104, 3);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 1;
            this.button_Cancel.Text = "キャンセル";
            this.button_Cancel.UseVisualStyleBackColor = false;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // button_OK
            // 
            this.button_OK.BackColor = System.Drawing.SystemColors.Control;
            this.button_OK.Location = new System.Drawing.Point(0, 3);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 0;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = false;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // panel種別
            // 
            this.panel種別.Controls.Add(this.label1);
            this.panel種別.Controls.Add(this.comboBox種別);
            this.panel種別.Location = new System.Drawing.Point(29, 78);
            this.panel種別.Name = "panel種別";
            this.panel種別.Size = new System.Drawing.Size(277, 26);
            this.panel種別.TabIndex = 2;
            // 
            // label_Message
            // 
            this.label_Message.AutoSize = true;
            this.label_Message.Location = new System.Drawing.Point(48, 26);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(238, 12);
            this.label_Message.TabIndex = 0;
            this.label_Message.Text = "手配依頼を作成します。種別を選択してください。";
            // 
            // panel詳細種別
            // 
            this.panel詳細種別.Controls.Add(this.label2);
            this.panel詳細種別.Controls.Add(this.comboBox詳細種別);
            this.panel詳細種別.Location = new System.Drawing.Point(29, 107);
            this.panel詳細種別.Name = "panel詳細種別";
            this.panel詳細種別.Size = new System.Drawing.Size(278, 26);
            this.panel詳細種別.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "詳細種別";
            // 
            // comboBox詳細種別
            // 
            this.comboBox詳細種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox詳細種別.FormattingEnabled = true;
            this.comboBox詳細種別.Items.AddRange(new object[] {
            "船用品",
            "潤滑油",
            "修繕"});
            this.comboBox詳細種別.Location = new System.Drawing.Point(76, 3);
            this.comboBox詳細種別.Name = "comboBox詳細種別";
            this.comboBox詳細種別.Size = new System.Drawing.Size(189, 20);
            this.comboBox詳細種別.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button_OK);
            this.panel3.Controls.Add(this.button_Cancel);
            this.panel3.Location = new System.Drawing.Point(78, 151);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(179, 29);
            this.panel3.TabIndex = 5;
            // 
            // panel見積有無
            // 
            this.panel見積有無.Controls.Add(this.label3);
            this.panel見積有無.Controls.Add(this.comboBox見積有無);
            this.panel見積有無.Location = new System.Drawing.Point(29, 136);
            this.panel見積有無.Name = "panel見積有無";
            this.panel見積有無.Size = new System.Drawing.Size(277, 26);
            this.panel見積有無.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "見積もり";
            // 
            // comboBox見積有無
            // 
            this.comboBox見積有無.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox見積有無.FormattingEnabled = true;
            this.comboBox見積有無.Items.AddRange(new object[] {
            "する",
            "しない"});
            this.comboBox見積有無.Location = new System.Drawing.Point(76, 3);
            this.comboBox見積有無.Name = "comboBox見積有無";
            this.comboBox見積有無.Size = new System.Drawing.Size(188, 20);
            this.comboBox見積有無.TabIndex = 1;
            // 
            // panel船
            // 
            this.panel船.Controls.Add(this.label0);
            this.panel船.Controls.Add(this.comboBox船);
            this.panel船.Location = new System.Drawing.Point(29, 49);
            this.panel船.Name = "panel船";
            this.panel船.Size = new System.Drawing.Size(277, 26);
            this.panel船.TabIndex = 1;
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.Location = new System.Drawing.Point(3, 6);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(17, 12);
            this.label0.TabIndex = 0;
            this.label0.Text = "船";
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Items.AddRange(new object[] {
            "船用品",
            "潤滑油",
            "修繕"});
            this.comboBox船.Location = new System.Drawing.Point(76, 3);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(188, 20);
            this.comboBox船.TabIndex = 1;
            this.comboBox船.SelectedIndexChanged += new System.EventHandler(this.comboBox船_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(12, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "※";
            // 
            // 依頼種別Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(334, 203);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel船);
            this.Controls.Add(this.panel見積有無);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel詳細種別);
            this.Controls.Add(this.label_Message);
            this.Controls.Add(this.panel種別);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "依頼種別Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "手配依頼種別";
            this.Load += new System.EventHandler(this.手配依頼種別Form_Load);
            this.panel種別.ResumeLayout(false);
            this.panel種別.PerformLayout();
            this.panel詳細種別.ResumeLayout(false);
            this.panel詳細種別.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel見積有無.ResumeLayout(false);
            this.panel見積有無.PerformLayout();
            this.panel船.ResumeLayout(false);
            this.panel船.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox種別;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Panel panel種別;
        private System.Windows.Forms.Label label_Message;
        private System.Windows.Forms.Panel panel詳細種別;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox詳細種別;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel見積有無;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox見積有無;
        private System.Windows.Forms.Panel panel船;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.Label label4;
    }
}