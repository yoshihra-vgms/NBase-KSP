namespace DeficiencyControl.MOI
{
    partial class MoiOutputExcelSettingForm
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
            this.yearPeriodInputControl1 = new DeficiencyControl.Controls.YearPeriodInputControl();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxKind = new System.Windows.Forms.GroupBox();
            this.radioButtonExcelList = new System.Windows.Forms.RadioButton();
            this.radioButtonExcelCategory = new System.Windows.Forms.RadioButton();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOutputExcel = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxKind.SuspendLayout();
            this.SuspendLayout();
            // 
            // yearPeriodInputControl1
            // 
            this.yearPeriodInputControl1.AutoSize = true;
            this.yearPeriodInputControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.yearPeriodInputControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.yearPeriodInputControl1.Location = new System.Drawing.Point(40, 149);
            this.yearPeriodInputControl1.Margin = new System.Windows.Forms.Padding(4);
            this.yearPeriodInputControl1.Name = "yearPeriodInputControl1";
            this.yearPeriodInputControl1.Size = new System.Drawing.Size(285, 29);
            this.yearPeriodInputControl1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label1.Location = new System.Drawing.Point(21, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 314;
            this.label1.Text = "出力対象年度";
            // 
            // groupBoxKind
            // 
            this.groupBoxKind.Controls.Add(this.radioButtonExcelList);
            this.groupBoxKind.Controls.Add(this.radioButtonExcelCategory);
            this.groupBoxKind.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.groupBoxKind.Location = new System.Drawing.Point(12, 12);
            this.groupBoxKind.Name = "groupBoxKind";
            this.groupBoxKind.Size = new System.Drawing.Size(373, 107);
            this.groupBoxKind.TabIndex = 0;
            this.groupBoxKind.TabStop = false;
            this.groupBoxKind.Text = "出力種別";
            // 
            // radioButtonExcelList
            // 
            this.radioButtonExcelList.AutoSize = true;
            this.radioButtonExcelList.Location = new System.Drawing.Point(28, 73);
            this.radioButtonExcelList.Name = "radioButtonExcelList";
            this.radioButtonExcelList.Size = new System.Drawing.Size(184, 19);
            this.radioButtonExcelList.TabIndex = 1;
            this.radioButtonExcelList.Tag = "2";
            this.radioButtonExcelList.Text = "指摘内容・是正対応リスト";
            this.radioButtonExcelList.UseVisualStyleBackColor = true;
            // 
            // radioButtonExcelCategory
            // 
            this.radioButtonExcelCategory.AutoSize = true;
            this.radioButtonExcelCategory.Checked = true;
            this.radioButtonExcelCategory.Location = new System.Drawing.Point(28, 34);
            this.radioButtonExcelCategory.Name = "radioButtonExcelCategory";
            this.radioButtonExcelCategory.Size = new System.Drawing.Size(185, 19);
            this.radioButtonExcelCategory.TabIndex = 0;
            this.radioButtonExcelCategory.TabStop = true;
            this.radioButtonExcelCategory.Tag = "1";
            this.radioButtonExcelCategory.Text = "検船章別(項目別)指摘数";
            this.radioButtonExcelCategory.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(255, 204);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(130, 50);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Close\r\n(閉じる)";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonOutputExcel
            // 
            this.buttonOutputExcel.Location = new System.Drawing.Point(12, 204);
            this.buttonOutputExcel.Name = "buttonOutputExcel";
            this.buttonOutputExcel.Size = new System.Drawing.Size(130, 50);
            this.buttonOutputExcel.TabIndex = 2;
            this.buttonOutputExcel.Text = "Output Excel\r\n(Excel出力)";
            this.buttonOutputExcel.UseVisualStyleBackColor = true;
            this.buttonOutputExcel.Click += new System.EventHandler(this.buttonOutputExcel_Click);
            // 
            // MoiOutputExcelSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 266);
            this.Controls.Add(this.yearPeriodInputControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxKind);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOutputExcel);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MoiOutputExcelSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "検船（帳票出力設定）";
            this.Load += new System.EventHandler(this.MoiOutputExcelForm_Load);
            this.groupBoxKind.ResumeLayout(false);
            this.groupBoxKind.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.YearPeriodInputControl yearPeriodInputControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxKind;
        private System.Windows.Forms.RadioButton radioButtonExcelList;
        private System.Windows.Forms.RadioButton radioButtonExcelCategory;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonOutputExcel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}