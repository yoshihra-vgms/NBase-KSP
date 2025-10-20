namespace DeficiencyControl.MOI
{
    partial class MoiListForm
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
            this.dataGridViewMoi = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonOutputExcel = new System.Windows.Forms.Button();
            this.buttonDetail = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.textBoxKeyword = new System.Windows.Forms.TextBox();
            this.comboBoxInspectionCategory = new System.Windows.Forms.ComboBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.textBoxInspectionName = new System.Windows.Forms.TextBox();
            this.comboBoxViqCode = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonOutputReport = new System.Windows.Forms.Button();
            this.singleLineComboViqNo = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboInspectionCompany = new DeficiencyControl.Util.SingleLineCombo();
            this.datePeriodControlDate = new DeficiencyControl.Controls.DatePeriodControl();
            this.singleLineComboCountry = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboUser = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboVessel = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboPort = new DeficiencyControl.Util.SingleLineCombo();
            this.dataCountControl1 = new DeficiencyControl.Controls.DataCountControl();
            this.labelDescriptionControl1 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl10 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl3 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl2 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl4 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl5 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl6 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl7 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl8 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl9 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDescriptionControl11 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.checkBoxObservationZero = new System.Windows.Forms.CheckBox();
            this.buttonBlueUpdate = new System.Windows.Forms.Button();
            this.checkBoxBlueReverse = new System.Windows.Forms.CheckBox();
            this.labelDescriptionControl12 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.comboBoxViqVersion = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMoi)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(1122, 620);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(130, 50);
            this.buttonClose.TabIndex = 19;
            this.buttonClose.Text = "Close\r\n(閉じる)";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // dataGridViewMoi
            // 
            this.dataGridViewMoi.AllowUserToAddRows = false;
            this.dataGridViewMoi.AllowUserToDeleteRows = false;
            this.dataGridViewMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewMoi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMoi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column12,
            this.Column2,
            this.Column3,
            this.Column7,
            this.Column9,
            this.Column4,
            this.Column10,
            this.Column6,
            this.Column5,
            this.Column11,
            this.Column8});
            this.dataGridViewMoi.EnableHeadersVisualStyles = false;
            this.dataGridViewMoi.Location = new System.Drawing.Point(12, 377);
            this.dataGridViewMoi.MultiSelect = false;
            this.dataGridViewMoi.Name = "dataGridViewMoi";
            this.dataGridViewMoi.ReadOnly = true;
            this.dataGridViewMoi.RowHeadersVisible = false;
            this.dataGridViewMoi.RowTemplate.Height = 21;
            this.dataGridViewMoi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMoi.Size = new System.Drawing.Size(1240, 237);
            this.dataGridViewMoi.TabIndex = 18;
            this.dataGridViewMoi.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMoi_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Data";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Chk";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 50;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "No";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Vessel\\n(船名)";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 110;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Date\\n(受検日)";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Port\\n(港名)";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Inspection Category\\n(検船種別)";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 200;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Inspection Company\\n(検船会社)";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 180;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Inspection Name\\n(検船員)";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 170;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "VIQ No.";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Observation\\n(指摘事項)";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Status\\n(状態)";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // buttonOutputExcel
            // 
            this.buttonOutputExcel.Location = new System.Drawing.Point(892, 321);
            this.buttonOutputExcel.Name = "buttonOutputExcel";
            this.buttonOutputExcel.Size = new System.Drawing.Size(130, 50);
            this.buttonOutputExcel.TabIndex = 17;
            this.buttonOutputExcel.Text = "Output Excel\r\n(Excel出力)";
            this.buttonOutputExcel.UseVisualStyleBackColor = true;
            this.buttonOutputExcel.Click += new System.EventHandler(this.buttonOutputExcel_Click);
            // 
            // buttonDetail
            // 
            this.buttonDetail.Location = new System.Drawing.Point(148, 321);
            this.buttonDetail.Name = "buttonDetail";
            this.buttonDetail.Size = new System.Drawing.Size(130, 50);
            this.buttonDetail.TabIndex = 15;
            this.buttonDetail.Text = "Detail\r\n(編集)";
            this.buttonDetail.UseVisualStyleBackColor = true;
            this.buttonDetail.Click += new System.EventHandler(this.buttonDetail_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(12, 321);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(130, 50);
            this.buttonCreate.TabIndex = 14;
            this.buttonCreate.Text = "Create\r\n(新規作成)";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // textBoxKeyword
            // 
            this.textBoxKeyword.Location = new System.Drawing.Point(99, 269);
            this.textBoxKeyword.Name = "textBoxKeyword";
            this.textBoxKeyword.Size = new System.Drawing.Size(923, 23);
            this.textBoxKeyword.TabIndex = 11;
            // 
            // comboBoxInspectionCategory
            // 
            this.comboBoxInspectionCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInspectionCategory.FormattingEnabled = true;
            this.comboBoxInspectionCategory.Location = new System.Drawing.Point(634, 12);
            this.comboBoxInspectionCategory.Name = "comboBoxInspectionCategory";
            this.comboBoxInspectionCategory.Size = new System.Drawing.Size(388, 24);
            this.comboBoxInspectionCategory.TabIndex = 1;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(1121, 68);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(130, 50);
            this.buttonClear.TabIndex = 13;
            this.buttonClear.Text = "Clear\r\n(クリア)";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(1121, 12);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(130, 50);
            this.buttonSearch.TabIndex = 12;
            this.buttonSearch.Text = "Search\r\n(検索)";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // textBoxInspectionName
            // 
            this.textBoxInspectionName.Location = new System.Drawing.Point(634, 105);
            this.textBoxInspectionName.Name = "textBoxInspectionName";
            this.textBoxInspectionName.Size = new System.Drawing.Size(388, 23);
            this.textBoxInspectionName.TabIndex = 5;
            // 
            // comboBoxViqCode
            // 
            this.comboBoxViqCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxViqCode.FormattingEnabled = true;
            this.comboBoxViqCode.Location = new System.Drawing.Point(634, 237);
            this.comboBoxViqCode.Name = "comboBoxViqCode";
            this.comboBoxViqCode.Size = new System.Drawing.Size(62, 24);
            this.comboBoxViqCode.TabIndex = 9;
            this.comboBoxViqCode.SelectedIndexChanged += new System.EventHandler(this.comboBoxViqCode_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(739, 240);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 16);
            this.label11.TabIndex = 338;
            this.label11.Text = "VIQ No.";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonOutputReport
            // 
            this.buttonOutputReport.Location = new System.Drawing.Point(756, 321);
            this.buttonOutputReport.Name = "buttonOutputReport";
            this.buttonOutputReport.Size = new System.Drawing.Size(130, 50);
            this.buttonOutputReport.TabIndex = 16;
            this.buttonOutputReport.Text = "Output Report\r\n(報告書出力)";
            this.buttonOutputReport.UseVisualStyleBackColor = true;
            this.buttonOutputReport.Click += new System.EventHandler(this.buttonOutputReport_Click);
            // 
            // singleLineComboViqNo
            // 
            this.singleLineComboViqNo.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboViqNo.Location = new System.Drawing.Point(805, 238);
            this.singleLineComboViqNo.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboViqNo.MaxLength = 32767;
            this.singleLineComboViqNo.Name = "singleLineComboViqNo";
            this.singleLineComboViqNo.ReadOnly = false;
            this.singleLineComboViqNo.Size = new System.Drawing.Size(217, 23);
            this.singleLineComboViqNo.TabIndex = 10;
            // 
            // singleLineComboInspectionCompany
            // 
            this.singleLineComboInspectionCompany.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboInspectionCompany.Location = new System.Drawing.Point(99, 105);
            this.singleLineComboInspectionCompany.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboInspectionCompany.MaxLength = 32767;
            this.singleLineComboInspectionCompany.Name = "singleLineComboInspectionCompany";
            this.singleLineComboInspectionCompany.ReadOnly = false;
            this.singleLineComboInspectionCompany.Size = new System.Drawing.Size(390, 23);
            this.singleLineComboInspectionCompany.TabIndex = 4;
            // 
            // datePeriodControlDate
            // 
            this.datePeriodControlDate.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.datePeriodControlDate.Location = new System.Drawing.Point(99, 162);
            this.datePeriodControlDate.Name = "datePeriodControlDate";
            this.datePeriodControlDate.Size = new System.Drawing.Size(400, 29);
            this.datePeriodControlDate.TabIndex = 6;
            // 
            // singleLineComboCountry
            // 
            this.singleLineComboCountry.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboCountry.Location = new System.Drawing.Point(634, 60);
            this.singleLineComboCountry.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboCountry.MaxLength = 32767;
            this.singleLineComboCountry.Name = "singleLineComboCountry";
            this.singleLineComboCountry.ReadOnly = false;
            this.singleLineComboCountry.Size = new System.Drawing.Size(388, 23);
            this.singleLineComboCountry.TabIndex = 3;
            // 
            // singleLineComboUser
            // 
            this.singleLineComboUser.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboUser.Location = new System.Drawing.Point(99, 215);
            this.singleLineComboUser.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboUser.MaxLength = 32767;
            this.singleLineComboUser.Name = "singleLineComboUser";
            this.singleLineComboUser.ReadOnly = false;
            this.singleLineComboUser.Size = new System.Drawing.Size(390, 23);
            this.singleLineComboUser.TabIndex = 8;
            // 
            // singleLineComboVessel
            // 
            this.singleLineComboVessel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboVessel.Location = new System.Drawing.Point(99, 12);
            this.singleLineComboVessel.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboVessel.MaxLength = 32767;
            this.singleLineComboVessel.Name = "singleLineComboVessel";
            this.singleLineComboVessel.ReadOnly = false;
            this.singleLineComboVessel.Size = new System.Drawing.Size(390, 23);
            this.singleLineComboVessel.TabIndex = 0;
            // 
            // singleLineComboPort
            // 
            this.singleLineComboPort.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboPort.Location = new System.Drawing.Point(99, 60);
            this.singleLineComboPort.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboPort.MaxLength = 32767;
            this.singleLineComboPort.Name = "singleLineComboPort";
            this.singleLineComboPort.ReadOnly = false;
            this.singleLineComboPort.Size = new System.Drawing.Size(390, 23);
            this.singleLineComboPort.TabIndex = 2;
            // 
            // dataCountControl1
            // 
            this.dataCountControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataCountControl1.AutoSize = true;
            this.dataCountControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dataCountControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.dataCountControl1.Location = new System.Drawing.Point(1170, 354);
            this.dataCountControl1.Margin = new System.Windows.Forms.Padding(4);
            this.dataCountControl1.Name = "dataCountControl1";
            this.dataCountControl1.Size = new System.Drawing.Size(81, 16);
            this.dataCountControl1.TabIndex = 340;
            // 
            // labelDescriptionControl1
            // 
            this.labelDescriptionControl1.AutoSize = true;
            this.labelDescriptionControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl1.DescriptionEnabled = true;
            this.labelDescriptionControl1.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl1.DescriptionText = "(船名)";
            this.labelDescriptionControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl1.Location = new System.Drawing.Point(3, 7);
            this.labelDescriptionControl1.MainText = "Vessel";
            this.labelDescriptionControl1.Name = "labelDescriptionControl1";
            this.labelDescriptionControl1.RequiredFlag = false;
            this.labelDescriptionControl1.Size = new System.Drawing.Size(67, 31);
            this.labelDescriptionControl1.TabIndex = 341;
            this.labelDescriptionControl1.TabStop = false;
            // 
            // labelDescriptionControl10
            // 
            this.labelDescriptionControl10.AutoSize = true;
            this.labelDescriptionControl10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl10.DescriptionEnabled = true;
            this.labelDescriptionControl10.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl10.DescriptionText = "(キーワード)";
            this.labelDescriptionControl10.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl10.Location = new System.Drawing.Point(3, 261);
            this.labelDescriptionControl10.MainText = "Keyword";
            this.labelDescriptionControl10.Name = "labelDescriptionControl10";
            this.labelDescriptionControl10.RequiredFlag = false;
            this.labelDescriptionControl10.Size = new System.Drawing.Size(91, 31);
            this.labelDescriptionControl10.TabIndex = 344;
            this.labelDescriptionControl10.TabStop = false;
            // 
            // labelDescriptionControl3
            // 
            this.labelDescriptionControl3.AutoSize = true;
            this.labelDescriptionControl3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl3.DescriptionEnabled = true;
            this.labelDescriptionControl3.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl3.DescriptionText = "(入力者)";
            this.labelDescriptionControl3.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl3.Location = new System.Drawing.Point(3, 207);
            this.labelDescriptionControl3.MainText = "P.I.C";
            this.labelDescriptionControl3.Name = "labelDescriptionControl3";
            this.labelDescriptionControl3.RequiredFlag = false;
            this.labelDescriptionControl3.Size = new System.Drawing.Size(78, 31);
            this.labelDescriptionControl3.TabIndex = 343;
            this.labelDescriptionControl3.TabStop = false;
            // 
            // labelDescriptionControl2
            // 
            this.labelDescriptionControl2.AutoSize = true;
            this.labelDescriptionControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl2.DescriptionEnabled = true;
            this.labelDescriptionControl2.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl2.DescriptionText = "(港名)";
            this.labelDescriptionControl2.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl2.Location = new System.Drawing.Point(3, 60);
            this.labelDescriptionControl2.MainText = "Port";
            this.labelDescriptionControl2.Name = "labelDescriptionControl2";
            this.labelDescriptionControl2.RequiredFlag = false;
            this.labelDescriptionControl2.Size = new System.Drawing.Size(63, 31);
            this.labelDescriptionControl2.TabIndex = 342;
            this.labelDescriptionControl2.TabStop = false;
            // 
            // labelDescriptionControl4
            // 
            this.labelDescriptionControl4.AutoSize = true;
            this.labelDescriptionControl4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl4.DescriptionEnabled = true;
            this.labelDescriptionControl4.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl4.DescriptionText = "(検船会社)";
            this.labelDescriptionControl4.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl4.Location = new System.Drawing.Point(3, 97);
            this.labelDescriptionControl4.MainText = "Inspection\r\nCompany";
            this.labelDescriptionControl4.Name = "labelDescriptionControl4";
            this.labelDescriptionControl4.RequiredFlag = false;
            this.labelDescriptionControl4.Size = new System.Drawing.Size(93, 47);
            this.labelDescriptionControl4.TabIndex = 345;
            this.labelDescriptionControl4.TabStop = false;
            // 
            // labelDescriptionControl5
            // 
            this.labelDescriptionControl5.AutoSize = true;
            this.labelDescriptionControl5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl5.DescriptionEnabled = true;
            this.labelDescriptionControl5.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl5.DescriptionText = "(検船種別)";
            this.labelDescriptionControl5.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl5.Location = new System.Drawing.Point(523, 7);
            this.labelDescriptionControl5.MainText = "Inspection\r\nCategory";
            this.labelDescriptionControl5.Name = "labelDescriptionControl5";
            this.labelDescriptionControl5.RequiredFlag = false;
            this.labelDescriptionControl5.Size = new System.Drawing.Size(93, 47);
            this.labelDescriptionControl5.TabIndex = 345;
            this.labelDescriptionControl5.TabStop = false;
            // 
            // labelDescriptionControl6
            // 
            this.labelDescriptionControl6.AutoSize = true;
            this.labelDescriptionControl6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl6.DescriptionEnabled = true;
            this.labelDescriptionControl6.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl6.DescriptionText = "(国名)";
            this.labelDescriptionControl6.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl6.Location = new System.Drawing.Point(523, 60);
            this.labelDescriptionControl6.MainText = "Country";
            this.labelDescriptionControl6.Name = "labelDescriptionControl6";
            this.labelDescriptionControl6.RequiredFlag = false;
            this.labelDescriptionControl6.Size = new System.Drawing.Size(79, 31);
            this.labelDescriptionControl6.TabIndex = 342;
            this.labelDescriptionControl6.TabStop = false;
            // 
            // labelDescriptionControl7
            // 
            this.labelDescriptionControl7.AutoSize = true;
            this.labelDescriptionControl7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl7.DescriptionEnabled = true;
            this.labelDescriptionControl7.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl7.DescriptionText = "(検船員)";
            this.labelDescriptionControl7.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl7.Location = new System.Drawing.Point(523, 97);
            this.labelDescriptionControl7.MainText = "Inspection\r\nName";
            this.labelDescriptionControl7.Name = "labelDescriptionControl7";
            this.labelDescriptionControl7.RequiredFlag = false;
            this.labelDescriptionControl7.Size = new System.Drawing.Size(92, 47);
            this.labelDescriptionControl7.TabIndex = 345;
            this.labelDescriptionControl7.TabStop = false;
            // 
            // labelDescriptionControl8
            // 
            this.labelDescriptionControl8.AutoSize = true;
            this.labelDescriptionControl8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl8.DescriptionEnabled = true;
            this.labelDescriptionControl8.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl8.DescriptionText = "(受検日)";
            this.labelDescriptionControl8.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl8.Location = new System.Drawing.Point(3, 160);
            this.labelDescriptionControl8.MainText = "Date";
            this.labelDescriptionControl8.Name = "labelDescriptionControl8";
            this.labelDescriptionControl8.RequiredFlag = false;
            this.labelDescriptionControl8.Size = new System.Drawing.Size(78, 31);
            this.labelDescriptionControl8.TabIndex = 343;
            this.labelDescriptionControl8.TabStop = false;
            // 
            // labelDescriptionControl9
            // 
            this.labelDescriptionControl9.AutoSize = true;
            this.labelDescriptionControl9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl9.DescriptionEnabled = false;
            this.labelDescriptionControl9.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl9.DescriptionText = "(国名)";
            this.labelDescriptionControl9.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl9.Location = new System.Drawing.Point(523, 239);
            this.labelDescriptionControl9.MainText = "VIQ Code";
            this.labelDescriptionControl9.Name = "labelDescriptionControl9";
            this.labelDescriptionControl9.RequiredFlag = false;
            this.labelDescriptionControl9.Size = new System.Drawing.Size(89, 16);
            this.labelDescriptionControl9.TabIndex = 342;
            this.labelDescriptionControl9.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label1.Location = new System.Drawing.Point(702, 241);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 15);
            this.label1.TabIndex = 338;
            this.label1.Text = "章";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelDescriptionControl11
            // 
            this.labelDescriptionControl11.AutoSize = true;
            this.labelDescriptionControl11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl11.DescriptionEnabled = true;
            this.labelDescriptionControl11.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl11.DescriptionText = "(指摘件数)";
            this.labelDescriptionControl11.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl11.Location = new System.Drawing.Point(523, 160);
            this.labelDescriptionControl11.MainText = "Observation";
            this.labelDescriptionControl11.Name = "labelDescriptionControl11";
            this.labelDescriptionControl11.RequiredFlag = false;
            this.labelDescriptionControl11.Size = new System.Drawing.Size(105, 31);
            this.labelDescriptionControl11.TabIndex = 346;
            this.labelDescriptionControl11.TabStop = false;
            // 
            // checkBoxObservationZero
            // 
            this.checkBoxObservationZero.AutoSize = true;
            this.checkBoxObservationZero.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.checkBoxObservationZero.Location = new System.Drawing.Point(634, 165);
            this.checkBoxObservationZero.Name = "checkBoxObservationZero";
            this.checkBoxObservationZero.Size = new System.Drawing.Size(90, 19);
            this.checkBoxObservationZero.TabIndex = 7;
            this.checkBoxObservationZero.Text = "0件を表示";
            this.checkBoxObservationZero.UseVisualStyleBackColor = true;
            // 
            // buttonBlueUpdate
            // 
            this.buttonBlueUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBlueUpdate.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonBlueUpdate.Location = new System.Drawing.Point(1121, 267);
            this.buttonBlueUpdate.Name = "buttonBlueUpdate";
            this.buttonBlueUpdate.Size = new System.Drawing.Size(50, 26);
            this.buttonBlueUpdate.TabIndex = 348;
            this.buttonBlueUpdate.Text = "更新";
            this.buttonBlueUpdate.UseVisualStyleBackColor = false;
            this.buttonBlueUpdate.Visible = false;
            this.buttonBlueUpdate.Click += new System.EventHandler(this.buttonBlueUpdate_Click);
            // 
            // checkBoxBlueReverse
            // 
            this.checkBoxBlueReverse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxBlueReverse.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxBlueReverse.AutoSize = true;
            this.checkBoxBlueReverse.BackColor = System.Drawing.Color.LightSkyBlue;
            this.checkBoxBlueReverse.Location = new System.Drawing.Point(1201, 267);
            this.checkBoxBlueReverse.Name = "checkBoxBlueReverse";
            this.checkBoxBlueReverse.Size = new System.Drawing.Size(50, 26);
            this.checkBoxBlueReverse.TabIndex = 347;
            this.checkBoxBlueReverse.Text = "反転";
            this.checkBoxBlueReverse.UseVisualStyleBackColor = false;
            this.checkBoxBlueReverse.Visible = false;
            this.checkBoxBlueReverse.CheckedChanged += new System.EventHandler(this.checkBoxBlueReverse_CheckedChanged);
            // 
            // labelDescriptionControl12
            // 
            this.labelDescriptionControl12.AutoSize = true;
            this.labelDescriptionControl12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl12.DescriptionEnabled = false;
            this.labelDescriptionControl12.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl12.DescriptionText = "(国名)";
            this.labelDescriptionControl12.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl12.Location = new System.Drawing.Point(523, 207);
            this.labelDescriptionControl12.MainText = "VIQ Version";
            this.labelDescriptionControl12.Name = "labelDescriptionControl12";
            this.labelDescriptionControl12.RequiredFlag = false;
            this.labelDescriptionControl12.Size = new System.Drawing.Size(104, 16);
            this.labelDescriptionControl12.TabIndex = 349;
            this.labelDescriptionControl12.TabStop = false;
            // 
            // comboBoxViqVersion
            // 
            this.comboBoxViqVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxViqVersion.FormattingEnabled = true;
            this.comboBoxViqVersion.Location = new System.Drawing.Point(634, 204);
            this.comboBoxViqVersion.Name = "comboBoxViqVersion";
            this.comboBoxViqVersion.Size = new System.Drawing.Size(90, 24);
            this.comboBoxViqVersion.TabIndex = 9;
            this.comboBoxViqVersion.SelectedIndexChanged += new System.EventHandler(this.comboBoxViqVersion_SelectedIndexChanged);
            // 
            // MoiListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.labelDescriptionControl12);
            this.Controls.Add(this.buttonBlueUpdate);
            this.Controls.Add(this.checkBoxBlueReverse);
            this.Controls.Add(this.checkBoxObservationZero);
            this.Controls.Add(this.labelDescriptionControl11);
            this.Controls.Add(this.labelDescriptionControl5);
            this.Controls.Add(this.labelDescriptionControl7);
            this.Controls.Add(this.labelDescriptionControl4);
            this.Controls.Add(this.labelDescriptionControl10);
            this.Controls.Add(this.labelDescriptionControl8);
            this.Controls.Add(this.labelDescriptionControl3);
            this.Controls.Add(this.labelDescriptionControl9);
            this.Controls.Add(this.labelDescriptionControl6);
            this.Controls.Add(this.labelDescriptionControl2);
            this.Controls.Add(this.labelDescriptionControl1);
            this.Controls.Add(this.dataCountControl1);
            this.Controls.Add(this.singleLineComboViqNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.comboBoxViqVersion);
            this.Controls.Add(this.comboBoxViqCode);
            this.Controls.Add(this.textBoxInspectionName);
            this.Controls.Add(this.singleLineComboInspectionCompany);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.dataGridViewMoi);
            this.Controls.Add(this.buttonOutputReport);
            this.Controls.Add(this.buttonOutputExcel);
            this.Controls.Add(this.buttonDetail);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.textBoxKeyword);
            this.Controls.Add(this.datePeriodControlDate);
            this.Controls.Add(this.singleLineComboCountry);
            this.Controls.Add(this.singleLineComboUser);
            this.Controls.Add(this.singleLineComboVessel);
            this.Controls.Add(this.singleLineComboPort);
            this.Controls.Add(this.comboBoxInspectionCategory);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSearch);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MoiListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "検船";
            this.Load += new System.EventHandler(this.MoiListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMoi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridView dataGridViewMoi;
        private System.Windows.Forms.Button buttonOutputExcel;
        private System.Windows.Forms.Button buttonDetail;
        private System.Windows.Forms.Button buttonCreate;
        public System.Windows.Forms.TextBox textBoxKeyword;
        public Controls.DatePeriodControl datePeriodControlDate;
        public Util.SingleLineCombo singleLineComboCountry;
        public Util.SingleLineCombo singleLineComboUser;
        public Util.SingleLineCombo singleLineComboVessel;
        public Util.SingleLineCombo singleLineComboPort;
        public System.Windows.Forms.ComboBox comboBoxInspectionCategory;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonSearch;
        public Util.SingleLineCombo singleLineComboInspectionCompany;
        public System.Windows.Forms.TextBox textBoxInspectionName;
        public System.Windows.Forms.ComboBox comboBoxViqCode;
        public Util.SingleLineCombo singleLineComboViqNo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonOutputReport;
        public Controls.DataCountControl dataCountControl1;
        private Controls.LabelDescriptionControl labelDescriptionControl1;
        private Controls.LabelDescriptionControl labelDescriptionControl10;
        private Controls.LabelDescriptionControl labelDescriptionControl3;
        private Controls.LabelDescriptionControl labelDescriptionControl2;
        private Controls.LabelDescriptionControl labelDescriptionControl4;
        private Controls.LabelDescriptionControl labelDescriptionControl5;
        private Controls.LabelDescriptionControl labelDescriptionControl6;
        private Controls.LabelDescriptionControl labelDescriptionControl7;
        private Controls.LabelDescriptionControl labelDescriptionControl8;
        private Controls.LabelDescriptionControl labelDescriptionControl9;
        private System.Windows.Forms.Label label1;
        private Controls.LabelDescriptionControl labelDescriptionControl11;
        public System.Windows.Forms.CheckBox checkBoxObservationZero;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Button buttonBlueUpdate;
        private System.Windows.Forms.CheckBox checkBoxBlueReverse;
        private Controls.LabelDescriptionControl labelDescriptionControl12;
        public System.Windows.Forms.ComboBox comboBoxViqVersion;
    }
}