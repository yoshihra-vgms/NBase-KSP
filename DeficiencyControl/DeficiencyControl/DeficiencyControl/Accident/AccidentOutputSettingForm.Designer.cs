namespace DeficiencyControl.Accident
{
    partial class AccidentOutputSettingForm
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
            this.buttonOutputExcel = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBoxKind = new System.Windows.Forms.GroupBox();
            this.singleLineComboVessel = new DeficiencyControl.Util.SingleLineCombo();
            this.radioButtonKindCharter = new System.Windows.Forms.RadioButton();
            this.radioButtonKindIGTOperate = new System.Windows.Forms.RadioButton();
            this.radioButtonKindIGTOwnner = new System.Windows.Forms.RadioButton();
            this.radioButtonKindVessel = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.yearPeriodInputControl1 = new DeficiencyControl.Controls.YearPeriodInputControl();
            this.groupBoxKind.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOutputExcel
            // 
            this.buttonOutputExcel.Location = new System.Drawing.Point(34, 339);
            this.buttonOutputExcel.Name = "buttonOutputExcel";
            this.buttonOutputExcel.Size = new System.Drawing.Size(130, 50);
            this.buttonOutputExcel.TabIndex = 2;
            this.buttonOutputExcel.Text = "Output Excel\r\n(Excel出力)";
            this.buttonOutputExcel.UseVisualStyleBackColor = true;
            this.buttonOutputExcel.Click += new System.EventHandler(this.buttonOutputExcel_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(338, 339);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(130, 50);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close\r\n(閉じる)";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBoxKind
            // 
            this.groupBoxKind.Controls.Add(this.singleLineComboVessel);
            this.groupBoxKind.Controls.Add(this.radioButtonKindCharter);
            this.groupBoxKind.Controls.Add(this.radioButtonKindIGTOperate);
            this.groupBoxKind.Controls.Add(this.radioButtonKindIGTOwnner);
            this.groupBoxKind.Controls.Add(this.radioButtonKindVessel);
            this.groupBoxKind.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.groupBoxKind.Location = new System.Drawing.Point(34, 34);
            this.groupBoxKind.Name = "groupBoxKind";
            this.groupBoxKind.Size = new System.Drawing.Size(434, 215);
            this.groupBoxKind.TabIndex = 0;
            this.groupBoxKind.TabStop = false;
            this.groupBoxKind.Text = "出力種別";
            // 
            // singleLineComboVessel
            // 
            this.singleLineComboVessel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboVessel.Location = new System.Drawing.Point(24, 63);
            this.singleLineComboVessel.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboVessel.MaxLength = 32767;
            this.singleLineComboVessel.Name = "singleLineComboVessel";
            this.singleLineComboVessel.ReadOnly = false;
            this.singleLineComboVessel.Size = new System.Drawing.Size(381, 23);
            this.singleLineComboVessel.TabIndex = 1;
            // 
            // radioButtonKindCharter
            // 
            this.radioButtonKindCharter.AutoSize = true;
            this.radioButtonKindCharter.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.radioButtonKindCharter.Location = new System.Drawing.Point(34, 180);
            this.radioButtonKindCharter.Name = "radioButtonKindCharter";
            this.radioButtonKindCharter.Size = new System.Drawing.Size(55, 19);
            this.radioButtonKindCharter.TabIndex = 4;
            this.radioButtonKindCharter.TabStop = true;
            this.radioButtonKindCharter.Tag = "3";
            this.radioButtonKindCharter.Text = "傭船";
            this.radioButtonKindCharter.UseVisualStyleBackColor = true;
            this.radioButtonKindCharter.CheckedChanged += new System.EventHandler(this.radioButtonKind_CheckedChanged);
            // 
            // radioButtonKindIGTOperate
            // 
            this.radioButtonKindIGTOperate.AutoSize = true;
            this.radioButtonKindIGTOperate.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.radioButtonKindIGTOperate.Location = new System.Drawing.Point(34, 142);
            this.radioButtonKindIGTOperate.Name = "radioButtonKindIGTOperate";
            this.radioButtonKindIGTOperate.Size = new System.Drawing.Size(326, 19);
            this.radioButtonKindIGTOperate.TabIndex = 3;
            this.radioButtonKindIGTOperate.TabStop = true;
            this.radioButtonKindIGTOperate.Tag = "2";
            this.radioButtonKindIGTOperate.Text = "外航船 (IGTオペレーション船 (内外併用船は除く))";
            this.radioButtonKindIGTOperate.UseVisualStyleBackColor = true;
            this.radioButtonKindIGTOperate.CheckedChanged += new System.EventHandler(this.radioButtonKind_CheckedChanged);
            // 
            // radioButtonKindIGTOwnner
            // 
            this.radioButtonKindIGTOwnner.AutoSize = true;
            this.radioButtonKindIGTOwnner.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.radioButtonKindIGTOwnner.Location = new System.Drawing.Point(34, 103);
            this.radioButtonKindIGTOwnner.Name = "radioButtonKindIGTOwnner";
            this.radioButtonKindIGTOwnner.Size = new System.Drawing.Size(315, 19);
            this.radioButtonKindIGTOwnner.TabIndex = 2;
            this.radioButtonKindIGTOwnner.TabStop = true;
            this.radioButtonKindIGTOwnner.Tag = "1";
            this.radioButtonKindIGTOwnner.Text = "IGT船 (IGTオーナー船 及び　管理船(内航のみ))";
            this.radioButtonKindIGTOwnner.UseVisualStyleBackColor = true;
            this.radioButtonKindIGTOwnner.CheckedChanged += new System.EventHandler(this.radioButtonKind_CheckedChanged);
            // 
            // radioButtonKindVessel
            // 
            this.radioButtonKindVessel.AutoSize = true;
            this.radioButtonKindVessel.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.radioButtonKindVessel.Location = new System.Drawing.Point(34, 36);
            this.radioButtonKindVessel.Name = "radioButtonKindVessel";
            this.radioButtonKindVessel.Size = new System.Drawing.Size(55, 19);
            this.radioButtonKindVessel.TabIndex = 0;
            this.radioButtonKindVessel.TabStop = true;
            this.radioButtonKindVessel.Tag = "0";
            this.radioButtonKindVessel.Text = "船毎";
            this.radioButtonKindVessel.UseVisualStyleBackColor = true;
            this.radioButtonKindVessel.CheckedChanged += new System.EventHandler(this.radioButtonKind_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label1.Location = new System.Drawing.Point(40, 269);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 309;
            this.label1.Text = "出力対象年度";
            // 
            // yearPeriodInputControl1
            // 
            this.yearPeriodInputControl1.AutoSize = true;
            this.yearPeriodInputControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.yearPeriodInputControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.yearPeriodInputControl1.Location = new System.Drawing.Point(34, 289);
            this.yearPeriodInputControl1.Margin = new System.Windows.Forms.Padding(4);
            this.yearPeriodInputControl1.Name = "yearPeriodInputControl1";
            this.yearPeriodInputControl1.Size = new System.Drawing.Size(285, 29);
            this.yearPeriodInputControl1.TabIndex = 1;
            // 
            // AccidentOutputSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 401);
            this.Controls.Add(this.yearPeriodInputControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxKind);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOutputExcel);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AccidentOutputSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "事故・トラブル (帳票出力設定)";
            this.Load += new System.EventHandler(this.AccidentOutputSettingForm_Load);
            this.groupBoxKind.ResumeLayout(false);
            this.groupBoxKind.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOutputExcel;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBoxKind;
        private System.Windows.Forms.RadioButton radioButtonKindCharter;
        private System.Windows.Forms.RadioButton radioButtonKindIGTOperate;
        private System.Windows.Forms.RadioButton radioButtonKindIGTOwnner;
        private System.Windows.Forms.RadioButton radioButtonKindVessel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        public Util.SingleLineCombo singleLineComboVessel;
        private Controls.YearPeriodInputControl yearPeriodInputControl1;
    }
}