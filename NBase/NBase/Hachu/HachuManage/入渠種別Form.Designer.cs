namespace Hachu.HachuManage
{
    partial class 入渠種別Form
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.panel入渠 = new System.Windows.Forms.Panel();
            this.label0 = new System.Windows.Forms.Label();
            this.comboBox詳細種別 = new System.Windows.Forms.ComboBox();
            this.label_Message = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel入渠.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button_OK);
            this.panel3.Controls.Add(this.button_Cancel);
            this.panel3.Location = new System.Drawing.Point(77, 138);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(179, 29);
            this.panel3.TabIndex = 6;
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
            // panel入渠
            // 
            this.panel入渠.Controls.Add(this.label0);
            this.panel入渠.Controls.Add(this.comboBox詳細種別);
            this.panel入渠.Location = new System.Drawing.Point(28, 67);
            this.panel入渠.Name = "panel入渠";
            this.panel入渠.Size = new System.Drawing.Size(277, 26);
            this.panel入渠.TabIndex = 8;
            // 
            // label0
            // 
            this.label0.AutoSize = true;
            this.label0.Location = new System.Drawing.Point(3, 6);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(53, 12);
            this.label0.TabIndex = 0;
            this.label0.Text = "検査種類";
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
            this.comboBox詳細種別.Size = new System.Drawing.Size(188, 20);
            this.comboBox詳細種別.TabIndex = 1;
            // 
            // label_Message
            // 
            this.label_Message.AutoSize = true;
            this.label_Message.Location = new System.Drawing.Point(28, 34);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(276, 12);
            this.label_Message.TabIndex = 7;
            this.label_Message.Text = "ドックオーダーを出力します。検査種類を選択してください。";
            // 
            // 入渠種別Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(332, 201);
            this.Controls.Add(this.panel入渠);
            this.Controls.Add(this.label_Message);
            this.Controls.Add(this.panel3);
            this.Name = "入渠種別Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ドックオーダー出力";
            this.Load += new System.EventHandler(this.入渠種別Form_Load);
            this.panel3.ResumeLayout(false);
            this.panel入渠.ResumeLayout(false);
            this.panel入渠.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Panel panel入渠;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.ComboBox comboBox詳細種別;
        private System.Windows.Forms.Label label_Message;
    }
}