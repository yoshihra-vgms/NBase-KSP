namespace DeficiencyControl.MOI
{
    partial class MoiHeaderControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxInspectionCategory = new System.Windows.Forms.ComboBox();
            this.singleLineComboUser = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboCountry = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboVessel = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboPort = new DeficiencyControl.Util.SingleLineCombo();
            this.textBoxTerminal = new System.Windows.Forms.TextBox();
            this.groupBoxSummaryOfInspection = new System.Windows.Forms.GroupBox();
            this.panelDateError = new System.Windows.Forms.Panel();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.labelDescriptionControl7 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl12 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl11 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl10 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl15 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl14 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl13 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl9 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl8 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl6 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.numericUpDownObservation = new DeficiencyControl.Util.TranquilNumericUpDown();
            this.textBoxAttend = new System.Windows.Forms.TextBox();
            this.textBoxInspectionName = new System.Windows.Forms.TextBox();
            this.singleLineComboInspectionCompany = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboAppointedCompany = new DeficiencyControl.Util.SingleLineCombo();
            this.dateTimePickerReceiptDate = new System.Windows.Forms.DateTimePicker();
            this.buttonAttachmentInspectionReport = new System.Windows.Forms.Button();
            this.fileViewControlExInspectionReport = new DeficiencyControl.Util.FileViewControlEx();
            this.textBoxRemarks = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.labelDescriptionControl2 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl1 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl3 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl4 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl5 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.groupBoxSummaryOfInspection.SuspendLayout();
            this.panelDateError.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownObservation)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxInspectionCategory
            // 
            this.comboBoxInspectionCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInspectionCategory.FormattingEnabled = true;
            this.comboBoxInspectionCategory.Location = new System.Drawing.Point(135, 95);
            this.comboBoxInspectionCategory.Name = "comboBoxInspectionCategory";
            this.comboBoxInspectionCategory.Size = new System.Drawing.Size(240, 24);
            this.comboBoxInspectionCategory.TabIndex = 3;
            // 
            // singleLineComboUser
            // 
            this.singleLineComboUser.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboUser.Location = new System.Drawing.Point(535, 5);
            this.singleLineComboUser.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboUser.MaxLength = 32767;
            this.singleLineComboUser.Name = "singleLineComboUser";
            this.singleLineComboUser.ReadOnly = false;
            this.singleLineComboUser.Size = new System.Drawing.Size(224, 23);
            this.singleLineComboUser.TabIndex = 1;
            // 
            // singleLineComboCountry
            // 
            this.singleLineComboCountry.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboCountry.Location = new System.Drawing.Point(911, 56);
            this.singleLineComboCountry.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboCountry.MaxLength = 32767;
            this.singleLineComboCountry.Name = "singleLineComboCountry";
            this.singleLineComboCountry.ReadOnly = false;
            this.singleLineComboCountry.Size = new System.Drawing.Size(229, 23);
            this.singleLineComboCountry.TabIndex = 4;
            // 
            // singleLineComboVessel
            // 
            this.singleLineComboVessel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboVessel.Location = new System.Drawing.Point(138, 5);
            this.singleLineComboVessel.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboVessel.MaxLength = 32767;
            this.singleLineComboVessel.Name = "singleLineComboVessel";
            this.singleLineComboVessel.ReadOnly = false;
            this.singleLineComboVessel.Size = new System.Drawing.Size(240, 23);
            this.singleLineComboVessel.TabIndex = 0;
            // 
            // singleLineComboPort
            // 
            this.singleLineComboPort.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboPort.Location = new System.Drawing.Point(138, 56);
            this.singleLineComboPort.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboPort.MaxLength = 32767;
            this.singleLineComboPort.Name = "singleLineComboPort";
            this.singleLineComboPort.ReadOnly = false;
            this.singleLineComboPort.Size = new System.Drawing.Size(240, 23);
            this.singleLineComboPort.TabIndex = 2;
            // 
            // textBoxTerminal
            // 
            this.textBoxTerminal.Location = new System.Drawing.Point(535, 56);
            this.textBoxTerminal.Name = "textBoxTerminal";
            this.textBoxTerminal.Size = new System.Drawing.Size(224, 23);
            this.textBoxTerminal.TabIndex = 3;
            // 
            // groupBoxSummaryOfInspection
            // 
            this.groupBoxSummaryOfInspection.Controls.Add(this.panelDateError);
            this.groupBoxSummaryOfInspection.Controls.Add(this.labelDescriptionControl7);
            this.groupBoxSummaryOfInspection.Controls.Add(this.labelDescriptionControl12);
            this.groupBoxSummaryOfInspection.Controls.Add(this.labelDescriptionControl11);
            this.groupBoxSummaryOfInspection.Controls.Add(this.labelDescriptionControl10);
            this.groupBoxSummaryOfInspection.Controls.Add(this.labelDescriptionControl15);
            this.groupBoxSummaryOfInspection.Controls.Add(this.labelDescriptionControl14);
            this.groupBoxSummaryOfInspection.Controls.Add(this.labelDescriptionControl13);
            this.groupBoxSummaryOfInspection.Controls.Add(this.labelDescriptionControl9);
            this.groupBoxSummaryOfInspection.Controls.Add(this.labelDescriptionControl8);
            this.groupBoxSummaryOfInspection.Controls.Add(this.labelDescriptionControl6);
            this.groupBoxSummaryOfInspection.Controls.Add(this.numericUpDownObservation);
            this.groupBoxSummaryOfInspection.Controls.Add(this.textBoxAttend);
            this.groupBoxSummaryOfInspection.Controls.Add(this.textBoxInspectionName);
            this.groupBoxSummaryOfInspection.Controls.Add(this.singleLineComboInspectionCompany);
            this.groupBoxSummaryOfInspection.Controls.Add(this.singleLineComboAppointedCompany);
            this.groupBoxSummaryOfInspection.Controls.Add(this.dateTimePickerReceiptDate);
            this.groupBoxSummaryOfInspection.Controls.Add(this.buttonAttachmentInspectionReport);
            this.groupBoxSummaryOfInspection.Controls.Add(this.fileViewControlExInspectionReport);
            this.groupBoxSummaryOfInspection.Controls.Add(this.comboBoxInspectionCategory);
            this.groupBoxSummaryOfInspection.Controls.Add(this.textBoxRemarks);
            this.groupBoxSummaryOfInspection.Location = new System.Drawing.Point(3, 107);
            this.groupBoxSummaryOfInspection.Name = "groupBoxSummaryOfInspection";
            this.groupBoxSummaryOfInspection.Size = new System.Drawing.Size(1143, 357);
            this.groupBoxSummaryOfInspection.TabIndex = 5;
            this.groupBoxSummaryOfInspection.TabStop = false;
            this.groupBoxSummaryOfInspection.Text = "Summary of Inspection (検船概要)";
            // 
            // panelDateError
            // 
            this.panelDateError.Controls.Add(this.dateTimePickerDate);
            this.panelDateError.Location = new System.Drawing.Point(132, 29);
            this.panelDateError.Name = "panelDateError";
            this.panelDateError.Size = new System.Drawing.Size(246, 29);
            this.panelDateError.TabIndex = 0;
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Location = new System.Drawing.Point(3, 3);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(240, 23);
            this.dateTimePickerDate.TabIndex = 297;
            this.dateTimePickerDate.ValueChanged += new System.EventHandler(this.dateTimePickerDate_ValueChanged);
            // 
            // labelDescriptionControl7
            // 
            this.labelDescriptionControl7.AutoSize = true;
            this.labelDescriptionControl7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl7.DescriptionEnabled = true;
            this.labelDescriptionControl7.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl7.DescriptionText = "(指摘件数)";
            this.labelDescriptionControl7.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl7.Location = new System.Drawing.Point(797, 29);
            this.labelDescriptionControl7.MainText = "Observation";
            this.labelDescriptionControl7.Name = "labelDescriptionControl7";
            this.labelDescriptionControl7.RequiredFlag = true;
            this.labelDescriptionControl7.Size = new System.Drawing.Size(105, 31);
            this.labelDescriptionControl7.TabIndex = 345;
            this.labelDescriptionControl7.TabStop = false;
            // 
            // labelDescriptionControl12
            // 
            this.labelDescriptionControl12.AutoSize = true;
            this.labelDescriptionControl12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl12.DescriptionEnabled = true;
            this.labelDescriptionControl12.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl12.DescriptionText = "(検船員)";
            this.labelDescriptionControl12.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl12.Location = new System.Drawing.Point(6, 293);
            this.labelDescriptionControl12.MainText = "Inspection\r\nName\r\n";
            this.labelDescriptionControl12.Name = "labelDescriptionControl12";
            this.labelDescriptionControl12.RequiredFlag = true;
            this.labelDescriptionControl12.Size = new System.Drawing.Size(92, 47);
            this.labelDescriptionControl12.TabIndex = 344;
            this.labelDescriptionControl12.TabStop = false;
            // 
            // labelDescriptionControl11
            // 
            this.labelDescriptionControl11.AutoSize = true;
            this.labelDescriptionControl11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl11.DescriptionEnabled = true;
            this.labelDescriptionControl11.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl11.DescriptionText = "(検船実施会社)";
            this.labelDescriptionControl11.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl11.Location = new System.Drawing.Point(6, 222);
            this.labelDescriptionControl11.MainText = "Inspection\r\nCompany";
            this.labelDescriptionControl11.Name = "labelDescriptionControl11";
            this.labelDescriptionControl11.RequiredFlag = true;
            this.labelDescriptionControl11.Size = new System.Drawing.Size(123, 47);
            this.labelDescriptionControl11.TabIndex = 344;
            this.labelDescriptionControl11.TabStop = false;
            // 
            // labelDescriptionControl10
            // 
            this.labelDescriptionControl10.AutoSize = true;
            this.labelDescriptionControl10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl10.DescriptionEnabled = true;
            this.labelDescriptionControl10.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl10.DescriptionText = "(申請先)";
            this.labelDescriptionControl10.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl10.Location = new System.Drawing.Point(6, 160);
            this.labelDescriptionControl10.MainText = "Appointed\r\nCompany";
            this.labelDescriptionControl10.Name = "labelDescriptionControl10";
            this.labelDescriptionControl10.RequiredFlag = false;
            this.labelDescriptionControl10.Size = new System.Drawing.Size(91, 47);
            this.labelDescriptionControl10.TabIndex = 344;
            this.labelDescriptionControl10.TabStop = false;
            // 
            // labelDescriptionControl15
            // 
            this.labelDescriptionControl15.AutoSize = true;
            this.labelDescriptionControl15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl15.DescriptionEnabled = true;
            this.labelDescriptionControl15.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl15.DescriptionText = "(立会者)";
            this.labelDescriptionControl15.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl15.Location = new System.Drawing.Point(532, 293);
            this.labelDescriptionControl15.MainText = "Attend";
            this.labelDescriptionControl15.Name = "labelDescriptionControl15";
            this.labelDescriptionControl15.RequiredFlag = false;
            this.labelDescriptionControl15.Size = new System.Drawing.Size(78, 31);
            this.labelDescriptionControl15.TabIndex = 344;
            this.labelDescriptionControl15.TabStop = false;
            // 
            // labelDescriptionControl14
            // 
            this.labelDescriptionControl14.AutoSize = true;
            this.labelDescriptionControl14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl14.DescriptionEnabled = true;
            this.labelDescriptionControl14.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl14.DescriptionText = "(備考)";
            this.labelDescriptionControl14.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl14.Location = new System.Drawing.Point(532, 222);
            this.labelDescriptionControl14.MainText = "Remarks";
            this.labelDescriptionControl14.Name = "labelDescriptionControl14";
            this.labelDescriptionControl14.RequiredFlag = false;
            this.labelDescriptionControl14.Size = new System.Drawing.Size(82, 31);
            this.labelDescriptionControl14.TabIndex = 344;
            this.labelDescriptionControl14.TabStop = false;
            // 
            // labelDescriptionControl13
            // 
            this.labelDescriptionControl13.AutoSize = true;
            this.labelDescriptionControl13.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl13.DescriptionEnabled = true;
            this.labelDescriptionControl13.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl13.DescriptionText = "(報告書添付)";
            this.labelDescriptionControl13.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl13.Location = new System.Drawing.Point(532, 88);
            this.labelDescriptionControl13.MainText = "Inspection\r\nReport\r\n";
            this.labelDescriptionControl13.Name = "labelDescriptionControl13";
            this.labelDescriptionControl13.RequiredFlag = false;
            this.labelDescriptionControl13.Size = new System.Drawing.Size(108, 47);
            this.labelDescriptionControl13.TabIndex = 344;
            this.labelDescriptionControl13.TabStop = false;
            // 
            // labelDescriptionControl9
            // 
            this.labelDescriptionControl9.AutoSize = true;
            this.labelDescriptionControl9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl9.DescriptionEnabled = true;
            this.labelDescriptionControl9.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl9.DescriptionText = "(検船種別)";
            this.labelDescriptionControl9.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl9.Location = new System.Drawing.Point(6, 88);
            this.labelDescriptionControl9.MainText = "Inspection\r\nCategory";
            this.labelDescriptionControl9.Name = "labelDescriptionControl9";
            this.labelDescriptionControl9.RequiredFlag = true;
            this.labelDescriptionControl9.Size = new System.Drawing.Size(93, 47);
            this.labelDescriptionControl9.TabIndex = 344;
            this.labelDescriptionControl9.TabStop = false;
            // 
            // labelDescriptionControl8
            // 
            this.labelDescriptionControl8.AutoSize = true;
            this.labelDescriptionControl8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl8.DescriptionEnabled = true;
            this.labelDescriptionControl8.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl8.DescriptionText = "(受検日)";
            this.labelDescriptionControl8.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl8.Location = new System.Drawing.Point(6, 29);
            this.labelDescriptionControl8.MainText = "Date";
            this.labelDescriptionControl8.Name = "labelDescriptionControl8";
            this.labelDescriptionControl8.RequiredFlag = true;
            this.labelDescriptionControl8.Size = new System.Drawing.Size(78, 31);
            this.labelDescriptionControl8.TabIndex = 344;
            this.labelDescriptionControl8.TabStop = false;
            // 
            // labelDescriptionControl6
            // 
            this.labelDescriptionControl6.AutoSize = true;
            this.labelDescriptionControl6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl6.DescriptionEnabled = true;
            this.labelDescriptionControl6.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl6.DescriptionText = "(レポート受領日)";
            this.labelDescriptionControl6.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl6.Location = new System.Drawing.Point(394, 29);
            this.labelDescriptionControl6.MainText = "Receipt";
            this.labelDescriptionControl6.Name = "labelDescriptionControl6";
            this.labelDescriptionControl6.RequiredFlag = false;
            this.labelDescriptionControl6.Size = new System.Drawing.Size(124, 31);
            this.labelDescriptionControl6.TabIndex = 345;
            this.labelDescriptionControl6.TabStop = false;
            // 
            // numericUpDownObservation
            // 
            this.numericUpDownObservation.InterceptArrowKeys = false;
            this.numericUpDownObservation.Location = new System.Drawing.Point(908, 32);
            this.numericUpDownObservation.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownObservation.Name = "numericUpDownObservation";
            this.numericUpDownObservation.Size = new System.Drawing.Size(54, 23);
            this.numericUpDownObservation.TabIndex = 2;
            this.numericUpDownObservation.ValueChanged += new System.EventHandler(this.numericUpDownObservation_ValueChanged);
            // 
            // textBoxAttend
            // 
            this.textBoxAttend.Location = new System.Drawing.Point(667, 303);
            this.textBoxAttend.Name = "textBoxAttend";
            this.textBoxAttend.Size = new System.Drawing.Size(331, 23);
            this.textBoxAttend.TabIndex = 10;
            // 
            // textBoxInspectionName
            // 
            this.textBoxInspectionName.Location = new System.Drawing.Point(135, 303);
            this.textBoxInspectionName.Name = "textBoxInspectionName";
            this.textBoxInspectionName.Size = new System.Drawing.Size(240, 23);
            this.textBoxInspectionName.TabIndex = 6;
            // 
            // singleLineComboInspectionCompany
            // 
            this.singleLineComboInspectionCompany.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboInspectionCompany.Location = new System.Drawing.Point(135, 222);
            this.singleLineComboInspectionCompany.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboInspectionCompany.MaxLength = 32767;
            this.singleLineComboInspectionCompany.Name = "singleLineComboInspectionCompany";
            this.singleLineComboInspectionCompany.ReadOnly = false;
            this.singleLineComboInspectionCompany.Size = new System.Drawing.Size(240, 23);
            this.singleLineComboInspectionCompany.TabIndex = 5;
            // 
            // singleLineComboAppointedCompany
            // 
            this.singleLineComboAppointedCompany.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboAppointedCompany.Location = new System.Drawing.Point(135, 169);
            this.singleLineComboAppointedCompany.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboAppointedCompany.MaxLength = 32767;
            this.singleLineComboAppointedCompany.Name = "singleLineComboAppointedCompany";
            this.singleLineComboAppointedCompany.ReadOnly = false;
            this.singleLineComboAppointedCompany.Size = new System.Drawing.Size(240, 23);
            this.singleLineComboAppointedCompany.TabIndex = 4;
            // 
            // dateTimePickerReceiptDate
            // 
            this.dateTimePickerReceiptDate.Location = new System.Drawing.Point(532, 32);
            this.dateTimePickerReceiptDate.Name = "dateTimePickerReceiptDate";
            this.dateTimePickerReceiptDate.Size = new System.Drawing.Size(224, 23);
            this.dateTimePickerReceiptDate.TabIndex = 1;
            // 
            // buttonAttachmentInspectionReport
            // 
            this.buttonAttachmentInspectionReport.Location = new System.Drawing.Point(546, 160);
            this.buttonAttachmentInspectionReport.Name = "buttonAttachmentInspectionReport";
            this.buttonAttachmentInspectionReport.Size = new System.Drawing.Size(54, 23);
            this.buttonAttachmentInspectionReport.TabIndex = 7;
            this.buttonAttachmentInspectionReport.Tag = "0";
            this.buttonAttachmentInspectionReport.Text = "...";
            this.buttonAttachmentInspectionReport.UseVisualStyleBackColor = true;
            this.buttonAttachmentInspectionReport.Click += new System.EventHandler(this.buttonAttachment_Click);
            // 
            // fileViewControlExInspectionReport
            // 
            this.fileViewControlExInspectionReport.AllowDrop = true;
            this.fileViewControlExInspectionReport.EnableDelete = true;
            this.fileViewControlExInspectionReport.EnableDragDrop = true;
            this.fileViewControlExInspectionReport.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.fileViewControlExInspectionReport.Location = new System.Drawing.Point(667, 88);
            this.fileViewControlExInspectionReport.Name = "fileViewControlExInspectionReport";
            this.fileViewControlExInspectionReport.Size = new System.Drawing.Size(470, 95);
            this.fileViewControlExInspectionReport.TabIndex = 8;
            this.fileViewControlExInspectionReport.FileItemSelected += new DeficiencyControl.Util.FileViewControlEx.FileItemSelectDelegate(this.fileViewControlEx_FileItemSelected);
            // 
            // textBoxRemarks
            // 
            this.textBoxRemarks.Location = new System.Drawing.Point(667, 222);
            this.textBoxRemarks.Multiline = true;
            this.textBoxRemarks.Name = "textBoxRemarks";
            this.textBoxRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxRemarks.Size = new System.Drawing.Size(470, 64);
            this.textBoxRemarks.TabIndex = 9;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelDescriptionControl2
            // 
            this.labelDescriptionControl2.AutoSize = true;
            this.labelDescriptionControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl2.DescriptionEnabled = true;
            this.labelDescriptionControl2.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl2.DescriptionText = "(港名)";
            this.labelDescriptionControl2.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl2.Location = new System.Drawing.Point(9, 56);
            this.labelDescriptionControl2.MainText = "Port";
            this.labelDescriptionControl2.Name = "labelDescriptionControl2";
            this.labelDescriptionControl2.RequiredFlag = true;
            this.labelDescriptionControl2.Size = new System.Drawing.Size(63, 31);
            this.labelDescriptionControl2.TabIndex = 344;
            this.labelDescriptionControl2.TabStop = false;
            // 
            // labelDescriptionControl1
            // 
            this.labelDescriptionControl1.AutoSize = true;
            this.labelDescriptionControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl1.DescriptionEnabled = true;
            this.labelDescriptionControl1.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl1.DescriptionText = "(船名)";
            this.labelDescriptionControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl1.Location = new System.Drawing.Point(9, 3);
            this.labelDescriptionControl1.MainText = "Vessel";
            this.labelDescriptionControl1.Name = "labelDescriptionControl1";
            this.labelDescriptionControl1.RequiredFlag = true;
            this.labelDescriptionControl1.Size = new System.Drawing.Size(67, 31);
            this.labelDescriptionControl1.TabIndex = 343;
            this.labelDescriptionControl1.TabStop = false;
            // 
            // labelDescriptionControl3
            // 
            this.labelDescriptionControl3.AutoSize = true;
            this.labelDescriptionControl3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl3.DescriptionEnabled = true;
            this.labelDescriptionControl3.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl3.DescriptionText = "(入力者)";
            this.labelDescriptionControl3.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl3.Location = new System.Drawing.Point(397, 3);
            this.labelDescriptionControl3.MainText = "P.I.C";
            this.labelDescriptionControl3.Name = "labelDescriptionControl3";
            this.labelDescriptionControl3.RequiredFlag = true;
            this.labelDescriptionControl3.Size = new System.Drawing.Size(78, 31);
            this.labelDescriptionControl3.TabIndex = 345;
            this.labelDescriptionControl3.TabStop = false;
            // 
            // labelDescriptionControl4
            // 
            this.labelDescriptionControl4.AutoSize = true;
            this.labelDescriptionControl4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl4.DescriptionEnabled = true;
            this.labelDescriptionControl4.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl4.DescriptionText = "(基地)";
            this.labelDescriptionControl4.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl4.Location = new System.Drawing.Point(397, 56);
            this.labelDescriptionControl4.MainText = "Terminal";
            this.labelDescriptionControl4.Name = "labelDescriptionControl4";
            this.labelDescriptionControl4.RequiredFlag = true;
            this.labelDescriptionControl4.Size = new System.Drawing.Size(81, 31);
            this.labelDescriptionControl4.TabIndex = 345;
            this.labelDescriptionControl4.TabStop = false;
            // 
            // labelDescriptionControl5
            // 
            this.labelDescriptionControl5.AutoSize = true;
            this.labelDescriptionControl5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl5.DescriptionEnabled = true;
            this.labelDescriptionControl5.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl5.DescriptionText = "(国名)";
            this.labelDescriptionControl5.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl5.Location = new System.Drawing.Point(800, 56);
            this.labelDescriptionControl5.MainText = "Country";
            this.labelDescriptionControl5.Name = "labelDescriptionControl5";
            this.labelDescriptionControl5.RequiredFlag = true;
            this.labelDescriptionControl5.Size = new System.Drawing.Size(79, 31);
            this.labelDescriptionControl5.TabIndex = 345;
            this.labelDescriptionControl5.TabStop = false;
            // 
            // MoiHeaderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDescriptionControl5);
            this.Controls.Add(this.labelDescriptionControl4);
            this.Controls.Add(this.labelDescriptionControl3);
            this.Controls.Add(this.labelDescriptionControl2);
            this.Controls.Add(this.labelDescriptionControl1);
            this.Controls.Add(this.groupBoxSummaryOfInspection);
            this.Controls.Add(this.textBoxTerminal);
            this.Controls.Add(this.singleLineComboUser);
            this.Controls.Add(this.singleLineComboCountry);
            this.Controls.Add(this.singleLineComboVessel);
            this.Controls.Add(this.singleLineComboPort);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MoiHeaderControl";
            this.Size = new System.Drawing.Size(1150, 472);
            this.Load += new System.EventHandler(this.MoiHeaderControl_Load);
            this.groupBoxSummaryOfInspection.ResumeLayout(false);
            this.groupBoxSummaryOfInspection.PerformLayout();
            this.panelDateError.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownObservation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxInspectionCategory;
        private Util.SingleLineCombo singleLineComboUser;
        private Util.SingleLineCombo singleLineComboCountry;
        private Util.SingleLineCombo singleLineComboVessel;
        private Util.SingleLineCombo singleLineComboPort;
        private System.Windows.Forms.TextBox textBoxTerminal;
        private System.Windows.Forms.GroupBox groupBoxSummaryOfInspection;
        private System.Windows.Forms.DateTimePicker dateTimePickerReceiptDate;
        private System.Windows.Forms.Button buttonAttachmentInspectionReport;
        private Util.FileViewControlEx fileViewControlExInspectionReport;
        private System.Windows.Forms.TextBox textBoxRemarks;
        private Util.SingleLineCombo singleLineComboAppointedCompany;
        private System.Windows.Forms.TextBox textBoxAttend;
        private System.Windows.Forms.TextBox textBoxInspectionName;
        private Util.SingleLineCombo singleLineComboInspectionCompany;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        internal Util.TranquilNumericUpDown numericUpDownObservation;
        private Controls.LabelDescriptionControl labelDescriptionControl2;
        private Controls.LabelDescriptionControl labelDescriptionControl1;
        private Controls.LabelDescriptionControl labelDescriptionControl3;
        private Controls.LabelDescriptionControl labelDescriptionControl4;
        private Controls.LabelDescriptionControl labelDescriptionControl5;
        private Controls.LabelDescriptionControl labelDescriptionControl7;
        private Controls.LabelDescriptionControl labelDescriptionControl12;
        private Controls.LabelDescriptionControl labelDescriptionControl11;
        private Controls.LabelDescriptionControl labelDescriptionControl10;
        private Controls.LabelDescriptionControl labelDescriptionControl15;
        private Controls.LabelDescriptionControl labelDescriptionControl14;
        private Controls.LabelDescriptionControl labelDescriptionControl13;
        private Controls.LabelDescriptionControl labelDescriptionControl9;
        private Controls.LabelDescriptionControl labelDescriptionControl8;
        private Controls.LabelDescriptionControl labelDescriptionControl6;
        private System.Windows.Forms.Panel panelDateError;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
    }
}
