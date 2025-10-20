namespace DeficiencyControl.Forms
{
    partial class PscOutputSettingForm
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOutputExcel = new System.Windows.Forms.Button();
            this.yearPeriodInputControl1 = new DeficiencyControl.Controls.YearPeriodInputControl();
            this.label1 = new System.Windows.Forms.Label();
            this.singleLineComboVessel = new DeficiencyControl.Util.SingleLineCombo();
            this.label2 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(280, 244);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(130, 50);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close\r\n(閉じる)";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonOutputExcel
            // 
            this.buttonOutputExcel.Location = new System.Drawing.Point(29, 244);
            this.buttonOutputExcel.Name = "buttonOutputExcel";
            this.buttonOutputExcel.Size = new System.Drawing.Size(130, 50);
            this.buttonOutputExcel.TabIndex = 2;
            this.buttonOutputExcel.Text = "Output Excel\r\n(Excel出力)";
            this.buttonOutputExcel.UseVisualStyleBackColor = true;
            this.buttonOutputExcel.Click += new System.EventHandler(this.buttonOutputExcel_Click);
            // 
            // yearPeriodInputControl1
            // 
            this.yearPeriodInputControl1.AutoSize = true;
            this.yearPeriodInputControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.yearPeriodInputControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.yearPeriodInputControl1.Location = new System.Drawing.Point(29, 61);
            this.yearPeriodInputControl1.Margin = new System.Windows.Forms.Padding(4);
            this.yearPeriodInputControl1.Name = "yearPeriodInputControl1";
            this.yearPeriodInputControl1.Size = new System.Drawing.Size(285, 29);
            this.yearPeriodInputControl1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label1.Location = new System.Drawing.Point(38, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 311;
            this.label1.Text = "出力対象年度";
            // 
            // singleLineComboVessel
            // 
            this.singleLineComboVessel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboVessel.Location = new System.Drawing.Point(29, 144);
            this.singleLineComboVessel.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboVessel.MaxLength = 32767;
            this.singleLineComboVessel.Name = "singleLineComboVessel";
            this.singleLineComboVessel.ReadOnly = false;
            this.singleLineComboVessel.Size = new System.Drawing.Size(381, 23);
            this.singleLineComboVessel.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label2.Location = new System.Drawing.Point(38, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 314;
            this.label2.Text = "出力対象船名";
            // 
            // PscOutputSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 306);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.singleLineComboVessel);
            this.Controls.Add(this.yearPeriodInputControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOutputExcel);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PscOutputSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PSC (帳票出力設定)";
            this.Load += new System.EventHandler(this.PscOutputSettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonOutputExcel;
        private Controls.YearPeriodInputControl yearPeriodInputControl1;
        private System.Windows.Forms.Label label1;
        public Util.SingleLineCombo singleLineComboVessel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}