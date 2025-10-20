namespace DeficiencyControl.MOI
{
    partial class MoiOutputReportSettingForm
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
            this.groupBoxKind = new System.Windows.Forms.GroupBox();
            this.panelReportObservation = new System.Windows.Forms.Panel();
            this.textBoxInspectionDetail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.singleLineComboUser = new DeficiencyControl.Util.SingleLineCombo();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDestGroup = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDestCompany = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonReportObservation = new System.Windows.Forms.RadioButton();
            this.radioButtonReportCommentList = new System.Windows.Forms.RadioButton();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOutputReport = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxKind.SuspendLayout();
            this.panelReportObservation.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxKind
            // 
            this.groupBoxKind.Controls.Add(this.panelReportObservation);
            this.groupBoxKind.Controls.Add(this.radioButtonReportObservation);
            this.groupBoxKind.Controls.Add(this.radioButtonReportCommentList);
            this.groupBoxKind.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.groupBoxKind.Location = new System.Drawing.Point(12, 18);
            this.groupBoxKind.Name = "groupBoxKind";
            this.groupBoxKind.Size = new System.Drawing.Size(487, 271);
            this.groupBoxKind.TabIndex = 0;
            this.groupBoxKind.TabStop = false;
            this.groupBoxKind.Text = "出力種別";
            // 
            // panelReportObservation
            // 
            this.panelReportObservation.Controls.Add(this.textBoxInspectionDetail);
            this.panelReportObservation.Controls.Add(this.label5);
            this.panelReportObservation.Controls.Add(this.singleLineComboUser);
            this.panelReportObservation.Controls.Add(this.label3);
            this.panelReportObservation.Controls.Add(this.textBoxDestGroup);
            this.panelReportObservation.Controls.Add(this.label2);
            this.panelReportObservation.Controls.Add(this.textBoxDestCompany);
            this.panelReportObservation.Controls.Add(this.label1);
            this.panelReportObservation.Location = new System.Drawing.Point(15, 96);
            this.panelReportObservation.Name = "panelReportObservation";
            this.panelReportObservation.Size = new System.Drawing.Size(466, 165);
            this.panelReportObservation.TabIndex = 2;
            // 
            // textBoxInspectionDetail
            // 
            this.textBoxInspectionDetail.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.textBoxInspectionDetail.Location = new System.Drawing.Point(101, 124);
            this.textBoxInspectionDetail.Name = "textBoxInspectionDetail";
            this.textBoxInspectionDetail.Size = new System.Drawing.Size(345, 23);
            this.textBoxInspectionDetail.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 321;
            this.label5.Text = "検船詳細";
            // 
            // singleLineComboUser
            // 
            this.singleLineComboUser.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboUser.Location = new System.Drawing.Point(102, 84);
            this.singleLineComboUser.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboUser.MaxLength = 32767;
            this.singleLineComboUser.Name = "singleLineComboUser";
            this.singleLineComboUser.ReadOnly = false;
            this.singleLineComboUser.Size = new System.Drawing.Size(344, 23);
            this.singleLineComboUser.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "作成者";
            // 
            // textBoxDestGroup
            // 
            this.textBoxDestGroup.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.textBoxDestGroup.Location = new System.Drawing.Point(102, 41);
            this.textBoxDestGroup.Name = "textBoxDestGroup";
            this.textBoxDestGroup.Size = new System.Drawing.Size(345, 23);
            this.textBoxDestGroup.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "宛先 部署名";
            // 
            // textBoxDestCompany
            // 
            this.textBoxDestCompany.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.textBoxDestCompany.Location = new System.Drawing.Point(102, 6);
            this.textBoxDestCompany.Name = "textBoxDestCompany";
            this.textBoxDestCompany.Size = new System.Drawing.Size(345, 23);
            this.textBoxDestCompany.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "宛先 会社名";
            // 
            // radioButtonReportObservation
            // 
            this.radioButtonReportObservation.AutoSize = true;
            this.radioButtonReportObservation.Location = new System.Drawing.Point(6, 70);
            this.radioButtonReportObservation.Name = "radioButtonReportObservation";
            this.radioButtonReportObservation.Size = new System.Drawing.Size(241, 19);
            this.radioButtonReportObservation.TabIndex = 1;
            this.radioButtonReportObservation.Tag = "2";
            this.radioButtonReportObservation.Text = "検船指摘事項に対する改善報告書";
            this.radioButtonReportObservation.UseVisualStyleBackColor = true;
            this.radioButtonReportObservation.CheckedChanged += new System.EventHandler(this.radioButtonReportObservation_CheckedChanged);
            // 
            // radioButtonReportCommentList
            // 
            this.radioButtonReportCommentList.AutoSize = true;
            this.radioButtonReportCommentList.Checked = true;
            this.radioButtonReportCommentList.Location = new System.Drawing.Point(6, 31);
            this.radioButtonReportCommentList.Name = "radioButtonReportCommentList";
            this.radioButtonReportCommentList.Size = new System.Drawing.Size(128, 19);
            this.radioButtonReportCommentList.TabIndex = 0;
            this.radioButtonReportCommentList.TabStop = true;
            this.radioButtonReportCommentList.Tag = "1";
            this.radioButtonReportCommentList.Text = "検船コメントリスト";
            this.radioButtonReportCommentList.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(369, 310);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(130, 50);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close\r\n(閉じる)";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonOutputReport
            // 
            this.buttonOutputReport.Location = new System.Drawing.Point(12, 310);
            this.buttonOutputReport.Name = "buttonOutputReport";
            this.buttonOutputReport.Size = new System.Drawing.Size(130, 50);
            this.buttonOutputReport.TabIndex = 1;
            this.buttonOutputReport.Text = "Output Report\r\n(報告書出力)";
            this.buttonOutputReport.UseVisualStyleBackColor = true;
            this.buttonOutputReport.Click += new System.EventHandler(this.buttonOutputReport_Click);
            // 
            // MoiOutputReportSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 372);
            this.Controls.Add(this.groupBoxKind);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOutputReport);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MoiOutputReportSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "検船 (報告書出力設定)";
            this.Load += new System.EventHandler(this.MoiOutputReportSettingForm_Load);
            this.groupBoxKind.ResumeLayout(false);
            this.groupBoxKind.PerformLayout();
            this.panelReportObservation.ResumeLayout(false);
            this.panelReportObservation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxKind;
        private System.Windows.Forms.RadioButton radioButtonReportObservation;
        private System.Windows.Forms.RadioButton radioButtonReportCommentList;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonOutputReport;
        private System.Windows.Forms.Panel panelReportObservation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDestGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDestCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxInspectionDetail;
        private System.Windows.Forms.Label label5;
        public Util.SingleLineCombo singleLineComboUser;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}