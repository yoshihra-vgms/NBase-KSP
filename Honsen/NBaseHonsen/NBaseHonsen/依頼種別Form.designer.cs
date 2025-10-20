namespace NBaseHonsen
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
            this.button閉じる = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_Message = new System.Windows.Forms.Label();
            this.panel_詳細種別_修繕 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox詳細種別 = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel_詳細種別_船用品 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxカテゴリ = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel_詳細種別_修繕.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel_詳細種別_船用品.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 19);
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
            this.comboBox種別.Location = new System.Drawing.Point(139, 5);
            this.comboBox種別.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(272, 27);
            this.comboBox種別.TabIndex = 1;
            this.comboBox種別.SelectedIndexChanged += new System.EventHandler(this.comboBox_Syubetsu_SelectedIndexChanged);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(192, 5);
            this.button閉じる.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(138, 36);
            this.button閉じる.TabIndex = 5;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.SystemColors.Control;
            this.buttonOK.Location = new System.Drawing.Point(23, 5);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(138, 36);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox種別);
            this.panel1.Location = new System.Drawing.Point(53, 82);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(438, 41);
            this.panel1.TabIndex = 6;
            // 
            // label_Message
            // 
            this.label_Message.AutoSize = true;
            this.label_Message.Location = new System.Drawing.Point(53, 41);
            this.label_Message.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(403, 19);
            this.label_Message.TabIndex = 7;
            this.label_Message.Text = "手配依頼を作成します。種別を選択してください。";
            // 
            // panel_詳細種別_修繕
            // 
            this.panel_詳細種別_修繕.Controls.Add(this.label2);
            this.panel_詳細種別_修繕.Controls.Add(this.comboBox詳細種別);
            this.panel_詳細種別_修繕.Location = new System.Drawing.Point(31, 116);
            this.panel_詳細種別_修繕.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panel_詳細種別_修繕.Name = "panel_詳細種別_修繕";
            this.panel_詳細種別_修繕.Size = new System.Drawing.Size(438, 41);
            this.panel_詳細種別_修繕.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 19);
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
            this.comboBox詳細種別.Location = new System.Drawing.Point(139, 5);
            this.comboBox詳細種別.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.comboBox詳細種別.Name = "comboBox詳細種別";
            this.comboBox詳細種別.Size = new System.Drawing.Size(272, 27);
            this.comboBox詳細種別.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel3.Controls.Add(this.buttonOK);
            this.panel3.Controls.Add(this.button閉じる);
            this.panel3.Location = new System.Drawing.Point(97, 198);
            this.panel3.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(352, 46);
            this.panel3.TabIndex = 8;
            // 
            // panel_詳細種別_船用品
            // 
            this.panel_詳細種別_船用品.Controls.Add(this.label3);
            this.panel_詳細種別_船用品.Controls.Add(this.comboBoxカテゴリ);
            this.panel_詳細種別_船用品.Location = new System.Drawing.Point(15, 152);
            this.panel_詳細種別_船用品.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panel_詳細種別_船用品.Name = "panel_詳細種別_船用品";
            this.panel_詳細種別_船用品.Size = new System.Drawing.Size(438, 41);
            this.panel_詳細種別_船用品.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "詳細種別";
            // 
            // comboBoxカテゴリ
            // 
            this.comboBoxカテゴリ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxカテゴリ.FormattingEnabled = true;
            this.comboBoxカテゴリ.Items.AddRange(new object[] {
            "船用品",
            "潤滑油",
            "修繕"});
            this.comboBoxカテゴリ.Location = new System.Drawing.Point(139, 5);
            this.comboBoxカテゴリ.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.comboBoxカテゴリ.Name = "comboBoxカテゴリ";
            this.comboBoxカテゴリ.Size = new System.Drawing.Size(272, 27);
            this.comboBoxカテゴリ.TabIndex = 1;
            // 
            // 依頼種別Form
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(546, 274);
            this.Controls.Add(this.panel_詳細種別_船用品);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel_詳細種別_修繕);
            this.Controls.Add(this.label_Message);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "依頼種別Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.手配依頼種別Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_詳細種別_修繕.ResumeLayout(false);
            this.panel_詳細種別_修繕.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel_詳細種別_船用品.ResumeLayout(false);
            this.panel_詳細種別_船用品.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox種別;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_Message;
        private System.Windows.Forms.Panel panel_詳細種別_修繕;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox詳細種別;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel_詳細種別_船用品;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxカテゴリ;
    }
}