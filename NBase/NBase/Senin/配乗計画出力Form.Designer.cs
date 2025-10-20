
namespace Senin
{
    partial class 配乗計画出力Form
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
            this.comboBox月 = new System.Windows.Forms.ComboBox();
            this.comboBox年 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button出力 = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.comboBox_RevNo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton計画 = new System.Windows.Forms.RadioButton();
            this.radioButton実績 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // comboBox月
            // 
            this.comboBox月.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox月.FormattingEnabled = true;
            this.comboBox月.Location = new System.Drawing.Point(179, 26);
            this.comboBox月.Name = "comboBox月";
            this.comboBox月.Size = new System.Drawing.Size(38, 20);
            this.comboBox月.TabIndex = 1;
            this.comboBox月.SelectedIndexChanged += new System.EventHandler(this.comboBox年月_SelectedIndexChanged);
            // 
            // comboBox年
            // 
            this.comboBox年.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox年.FormattingEnabled = true;
            this.comboBox年.Location = new System.Drawing.Point(95, 26);
            this.comboBox年.Name = "comboBox年";
            this.comboBox年.Size = new System.Drawing.Size(61, 20);
            this.comboBox年.TabIndex = 0;
            this.comboBox年.SelectedIndexChanged += new System.EventHandler(this.comboBox年月_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 41;
            this.label2.Text = "年月";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(162, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 42;
            this.label8.Text = "/";
            // 
            // button出力
            // 
            this.button出力.Location = new System.Drawing.Point(62, 113);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(94, 35);
            this.button出力.TabIndex = 4;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = true;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(179, 113);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 35);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "閉じる";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // comboBox_RevNo
            // 
            this.comboBox_RevNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_RevNo.FormattingEnabled = true;
            this.comboBox_RevNo.Location = new System.Drawing.Point(269, 3);
            this.comboBox_RevNo.Name = "comboBox_RevNo";
            this.comboBox_RevNo.Size = new System.Drawing.Size(51, 20);
            this.comboBox_RevNo.TabIndex = 52;
            this.comboBox_RevNo.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(203, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 12);
            this.label3.TabIndex = 51;
            this.label3.Text = "非表示：RevNo";
            this.label3.Visible = false;
            // 
            // radioButton計画
            // 
            this.radioButton計画.AutoSize = true;
            this.radioButton計画.Checked = true;
            this.radioButton計画.Location = new System.Drawing.Point(96, 74);
            this.radioButton計画.Name = "radioButton計画";
            this.radioButton計画.Size = new System.Drawing.Size(47, 16);
            this.radioButton計画.TabIndex = 2;
            this.radioButton計画.TabStop = true;
            this.radioButton計画.Text = "計画";
            this.radioButton計画.UseVisualStyleBackColor = true;
            // 
            // radioButton実績
            // 
            this.radioButton実績.AutoSize = true;
            this.radioButton実績.Location = new System.Drawing.Point(170, 74);
            this.radioButton実績.Name = "radioButton実績";
            this.radioButton実績.Size = new System.Drawing.Size(47, 16);
            this.radioButton実績.TabIndex = 3;
            this.radioButton実績.TabStop = true;
            this.radioButton実績.Text = "実績";
            this.radioButton実績.UseVisualStyleBackColor = true;
            // 
            // 配乗計画出力Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 186);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioButton実績);
            this.Controls.Add(this.radioButton計画);
            this.Controls.Add(this.comboBox_RevNo);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.button出力);
            this.Controls.Add(this.comboBox月);
            this.Controls.Add(this.comboBox年);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Name = "配乗計画出力Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "配乗計画出力";
            this.Load += new System.EventHandler(this.配乗計画出力_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox月;
        private System.Windows.Forms.ComboBox comboBox年;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ComboBox comboBox_RevNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton計画;
        private System.Windows.Forms.RadioButton radioButton実績;
    }
}