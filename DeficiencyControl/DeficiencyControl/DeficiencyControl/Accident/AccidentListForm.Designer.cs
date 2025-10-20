namespace DeficiencyControl.Accident
{
    partial class AccidentListForm
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
            this.datePeriodControlDate = new DeficiencyControl.Controls.DatePeriodControl();
            this.singleLineComboCountry = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboUser = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboVessel = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboPort = new DeficiencyControl.Util.SingleLineCombo();
            this.comboBoxAccidentKind = new System.Windows.Forms.ComboBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.comboBoxKindOfAccident = new System.Windows.Forms.ComboBox();
            this.comboBoxSituation = new System.Windows.Forms.ComboBox();
            this.textBoxKeyword = new System.Windows.Forms.TextBox();
            this.buttonOutputExcel = new System.Windows.Forms.Button();
            this.buttonDetail = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.dataGridViewAccident = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.buttonBlueUpdate = new System.Windows.Forms.Button();
            this.checkBoxBlueReverse = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccident)).BeginInit();
            this.SuspendLayout();
            // 
            // datePeriodControlDate
            // 
            this.datePeriodControlDate.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.datePeriodControlDate.Location = new System.Drawing.Point(99, 187);
            this.datePeriodControlDate.Name = "datePeriodControlDate";
            this.datePeriodControlDate.Size = new System.Drawing.Size(393, 29);
            this.datePeriodControlDate.TabIndex = 7;
            // 
            // singleLineComboCountry
            // 
            this.singleLineComboCountry.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboCountry.Location = new System.Drawing.Point(622, 49);
            this.singleLineComboCountry.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboCountry.MaxLength = 32767;
            this.singleLineComboCountry.Name = "singleLineComboCountry";
            this.singleLineComboCountry.ReadOnly = false;
            this.singleLineComboCountry.Size = new System.Drawing.Size(400, 23);
            this.singleLineComboCountry.TabIndex = 3;
            // 
            // singleLineComboUser
            // 
            this.singleLineComboUser.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboUser.Location = new System.Drawing.Point(99, 87);
            this.singleLineComboUser.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboUser.MaxLength = 32767;
            this.singleLineComboUser.Name = "singleLineComboUser";
            this.singleLineComboUser.ReadOnly = false;
            this.singleLineComboUser.Size = new System.Drawing.Size(393, 23);
            this.singleLineComboUser.TabIndex = 4;
            // 
            // singleLineComboVessel
            // 
            this.singleLineComboVessel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboVessel.Location = new System.Drawing.Point(99, 13);
            this.singleLineComboVessel.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboVessel.MaxLength = 32767;
            this.singleLineComboVessel.Name = "singleLineComboVessel";
            this.singleLineComboVessel.ReadOnly = false;
            this.singleLineComboVessel.Size = new System.Drawing.Size(393, 23);
            this.singleLineComboVessel.TabIndex = 0;
            // 
            // singleLineComboPort
            // 
            this.singleLineComboPort.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboPort.Location = new System.Drawing.Point(99, 49);
            this.singleLineComboPort.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboPort.MaxLength = 32767;
            this.singleLineComboPort.Name = "singleLineComboPort";
            this.singleLineComboPort.ReadOnly = false;
            this.singleLineComboPort.Size = new System.Drawing.Size(393, 23);
            this.singleLineComboPort.TabIndex = 2;
            // 
            // comboBoxAccidentKind
            // 
            this.comboBoxAccidentKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAccidentKind.Enabled = false;
            this.comboBoxAccidentKind.FormattingEnabled = true;
            this.comboBoxAccidentKind.Location = new System.Drawing.Point(622, 12);
            this.comboBoxAccidentKind.Name = "comboBoxAccidentKind";
            this.comboBoxAccidentKind.Size = new System.Drawing.Size(400, 24);
            this.comboBoxAccidentKind.TabIndex = 1;
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(1122, 68);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(130, 50);
            this.buttonClear.TabIndex = 10;
            this.buttonClear.Text = "Clear\r\n(クリア)";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(1122, 12);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(130, 50);
            this.buttonSearch.TabIndex = 9;
            this.buttonSearch.Text = "Search\r\n(検索)";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // comboBoxKindOfAccident
            // 
            this.comboBoxKindOfAccident.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKindOfAccident.FormattingEnabled = true;
            this.comboBoxKindOfAccident.Location = new System.Drawing.Point(99, 139);
            this.comboBoxKindOfAccident.Name = "comboBoxKindOfAccident";
            this.comboBoxKindOfAccident.Size = new System.Drawing.Size(390, 24);
            this.comboBoxKindOfAccident.TabIndex = 5;
            // 
            // comboBoxSituation
            // 
            this.comboBoxSituation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSituation.FormattingEnabled = true;
            this.comboBoxSituation.Location = new System.Drawing.Point(622, 139);
            this.comboBoxSituation.Name = "comboBoxSituation";
            this.comboBoxSituation.Size = new System.Drawing.Size(400, 24);
            this.comboBoxSituation.TabIndex = 6;
            // 
            // textBoxKeyword
            // 
            this.textBoxKeyword.Location = new System.Drawing.Point(99, 241);
            this.textBoxKeyword.Name = "textBoxKeyword";
            this.textBoxKeyword.Size = new System.Drawing.Size(923, 23);
            this.textBoxKeyword.TabIndex = 8;
            // 
            // buttonOutputExcel
            // 
            this.buttonOutputExcel.Location = new System.Drawing.Point(892, 287);
            this.buttonOutputExcel.Name = "buttonOutputExcel";
            this.buttonOutputExcel.Size = new System.Drawing.Size(130, 50);
            this.buttonOutputExcel.TabIndex = 13;
            this.buttonOutputExcel.Text = "Output Excel\r\n(Excel出力)";
            this.buttonOutputExcel.UseVisualStyleBackColor = true;
            this.buttonOutputExcel.Click += new System.EventHandler(this.buttonOutputExcel_Click);
            // 
            // buttonDetail
            // 
            this.buttonDetail.Location = new System.Drawing.Point(148, 287);
            this.buttonDetail.Name = "buttonDetail";
            this.buttonDetail.Size = new System.Drawing.Size(130, 50);
            this.buttonDetail.TabIndex = 12;
            this.buttonDetail.Text = "Detail\r\n(編集)";
            this.buttonDetail.UseVisualStyleBackColor = true;
            this.buttonDetail.Click += new System.EventHandler(this.buttonDetail_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(12, 287);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(130, 50);
            this.buttonCreate.TabIndex = 11;
            this.buttonCreate.Text = "Create\r\n(新規作成)";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(1122, 620);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(130, 50);
            this.buttonClose.TabIndex = 15;
            this.buttonClose.Text = "Close\r\n(閉じる)";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // dataGridViewAccident
            // 
            this.dataGridViewAccident.AllowUserToAddRows = false;
            this.dataGridViewAccident.AllowUserToDeleteRows = false;
            this.dataGridViewAccident.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAccident.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAccident.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column11,
            this.Column2,
            this.Column3,
            this.Column7,
            this.Column9,
            this.Column4,
            this.Column10,
            this.Column6,
            this.Column8,
            this.Column5});
            this.dataGridViewAccident.EnableHeadersVisualStyles = false;
            this.dataGridViewAccident.Location = new System.Drawing.Point(12, 343);
            this.dataGridViewAccident.MultiSelect = false;
            this.dataGridViewAccident.Name = "dataGridViewAccident";
            this.dataGridViewAccident.ReadOnly = true;
            this.dataGridViewAccident.RowHeadersVisible = false;
            this.dataGridViewAccident.RowTemplate.Height = 21;
            this.dataGridViewAccident.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAccident.Size = new System.Drawing.Size(1240, 258);
            this.dataGridViewAccident.TabIndex = 14;
            this.dataGridViewAccident.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAccident_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Data";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Chk";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 50;
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
            this.Column7.HeaderText = "Date\\n(日付)";
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
            this.Column4.HeaderText = "Kind (種類)";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 120;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Accident Kind\\n(事故分類)";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 150;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Title(タイトル)";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 300;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Status\\n(状態)";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "P.I.C\\n(入力者)";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 120;
            // 
            // dataCountControl1
            // 
            this.dataCountControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataCountControl1.AutoSize = true;
            this.dataCountControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dataCountControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.dataCountControl1.Location = new System.Drawing.Point(1170, 320);
            this.dataCountControl1.Margin = new System.Windows.Forms.Padding(4);
            this.dataCountControl1.Name = "dataCountControl1";
            this.dataCountControl1.Size = new System.Drawing.Size(81, 16);
            this.dataCountControl1.TabIndex = 307;
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
            this.labelDescriptionControl1.TabIndex = 308;
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
            this.labelDescriptionControl10.Location = new System.Drawing.Point(3, 233);
            this.labelDescriptionControl10.MainText = "Keyword";
            this.labelDescriptionControl10.Name = "labelDescriptionControl10";
            this.labelDescriptionControl10.RequiredFlag = false;
            this.labelDescriptionControl10.Size = new System.Drawing.Size(91, 31);
            this.labelDescriptionControl10.TabIndex = 311;
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
            this.labelDescriptionControl3.Location = new System.Drawing.Point(3, 81);
            this.labelDescriptionControl3.MainText = "P.I.C";
            this.labelDescriptionControl3.Name = "labelDescriptionControl3";
            this.labelDescriptionControl3.RequiredFlag = false;
            this.labelDescriptionControl3.Size = new System.Drawing.Size(78, 31);
            this.labelDescriptionControl3.TabIndex = 310;
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
            this.labelDescriptionControl2.Location = new System.Drawing.Point(3, 44);
            this.labelDescriptionControl2.MainText = "Port";
            this.labelDescriptionControl2.Name = "labelDescriptionControl2";
            this.labelDescriptionControl2.RequiredFlag = false;
            this.labelDescriptionControl2.Size = new System.Drawing.Size(63, 31);
            this.labelDescriptionControl2.TabIndex = 309;
            this.labelDescriptionControl2.TabStop = false;
            // 
            // labelDescriptionControl4
            // 
            this.labelDescriptionControl4.AutoSize = true;
            this.labelDescriptionControl4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl4.DescriptionEnabled = true;
            this.labelDescriptionControl4.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl4.DescriptionText = "(事故分類)";
            this.labelDescriptionControl4.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl4.Location = new System.Drawing.Point(3, 129);
            this.labelDescriptionControl4.MainText = "Kind of\r\nAccident";
            this.labelDescriptionControl4.Name = "labelDescriptionControl4";
            this.labelDescriptionControl4.RequiredFlag = false;
            this.labelDescriptionControl4.Size = new System.Drawing.Size(93, 47);
            this.labelDescriptionControl4.TabIndex = 311;
            this.labelDescriptionControl4.TabStop = false;
            // 
            // labelDescriptionControl5
            // 
            this.labelDescriptionControl5.AutoSize = true;
            this.labelDescriptionControl5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl5.DescriptionEnabled = true;
            this.labelDescriptionControl5.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl5.DescriptionText = "(期間)";
            this.labelDescriptionControl5.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl5.Location = new System.Drawing.Point(3, 185);
            this.labelDescriptionControl5.MainText = "Period";
            this.labelDescriptionControl5.Name = "labelDescriptionControl5";
            this.labelDescriptionControl5.RequiredFlag = false;
            this.labelDescriptionControl5.Size = new System.Drawing.Size(67, 31);
            this.labelDescriptionControl5.TabIndex = 311;
            this.labelDescriptionControl5.TabStop = false;
            // 
            // labelDescriptionControl6
            // 
            this.labelDescriptionControl6.AutoSize = true;
            this.labelDescriptionControl6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl6.DescriptionEnabled = true;
            this.labelDescriptionControl6.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl6.DescriptionText = "(種類)";
            this.labelDescriptionControl6.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl6.Location = new System.Drawing.Point(523, 7);
            this.labelDescriptionControl6.MainText = "Kind";
            this.labelDescriptionControl6.Name = "labelDescriptionControl6";
            this.labelDescriptionControl6.RequiredFlag = false;
            this.labelDescriptionControl6.Size = new System.Drawing.Size(63, 31);
            this.labelDescriptionControl6.TabIndex = 308;
            this.labelDescriptionControl6.TabStop = false;
            // 
            // labelDescriptionControl7
            // 
            this.labelDescriptionControl7.AutoSize = true;
            this.labelDescriptionControl7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl7.DescriptionEnabled = true;
            this.labelDescriptionControl7.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl7.DescriptionText = "(国名)";
            this.labelDescriptionControl7.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl7.Location = new System.Drawing.Point(523, 44);
            this.labelDescriptionControl7.MainText = "Country";
            this.labelDescriptionControl7.Name = "labelDescriptionControl7";
            this.labelDescriptionControl7.RequiredFlag = false;
            this.labelDescriptionControl7.Size = new System.Drawing.Size(79, 31);
            this.labelDescriptionControl7.TabIndex = 308;
            this.labelDescriptionControl7.TabStop = false;
            // 
            // labelDescriptionControl8
            // 
            this.labelDescriptionControl8.AutoSize = true;
            this.labelDescriptionControl8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl8.DescriptionEnabled = true;
            this.labelDescriptionControl8.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl8.DescriptionText = "(発生状況)";
            this.labelDescriptionControl8.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl8.Location = new System.Drawing.Point(523, 132);
            this.labelDescriptionControl8.MainText = "Situation";
            this.labelDescriptionControl8.Name = "labelDescriptionControl8";
            this.labelDescriptionControl8.RequiredFlag = false;
            this.labelDescriptionControl8.Size = new System.Drawing.Size(93, 31);
            this.labelDescriptionControl8.TabIndex = 308;
            this.labelDescriptionControl8.TabStop = false;
            // 
            // buttonBlueUpdate
            // 
            this.buttonBlueUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBlueUpdate.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonBlueUpdate.Location = new System.Drawing.Point(1121, 239);
            this.buttonBlueUpdate.Name = "buttonBlueUpdate";
            this.buttonBlueUpdate.Size = new System.Drawing.Size(50, 26);
            this.buttonBlueUpdate.TabIndex = 313;
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
            this.checkBoxBlueReverse.Location = new System.Drawing.Point(1201, 239);
            this.checkBoxBlueReverse.Name = "checkBoxBlueReverse";
            this.checkBoxBlueReverse.Size = new System.Drawing.Size(50, 26);
            this.checkBoxBlueReverse.TabIndex = 312;
            this.checkBoxBlueReverse.Text = "反転";
            this.checkBoxBlueReverse.UseVisualStyleBackColor = false;
            this.checkBoxBlueReverse.Visible = false;
            this.checkBoxBlueReverse.CheckedChanged += new System.EventHandler(this.checkBoxBlueReverse_CheckedChanged);
            // 
            // AccidentListForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.buttonBlueUpdate);
            this.Controls.Add(this.checkBoxBlueReverse);
            this.Controls.Add(this.labelDescriptionControl4);
            this.Controls.Add(this.labelDescriptionControl5);
            this.Controls.Add(this.labelDescriptionControl10);
            this.Controls.Add(this.labelDescriptionControl3);
            this.Controls.Add(this.labelDescriptionControl2);
            this.Controls.Add(this.labelDescriptionControl8);
            this.Controls.Add(this.labelDescriptionControl7);
            this.Controls.Add(this.labelDescriptionControl6);
            this.Controls.Add(this.labelDescriptionControl1);
            this.Controls.Add(this.dataCountControl1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.dataGridViewAccident);
            this.Controls.Add(this.buttonOutputExcel);
            this.Controls.Add(this.buttonDetail);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.textBoxKeyword);
            this.Controls.Add(this.comboBoxSituation);
            this.Controls.Add(this.comboBoxKindOfAccident);
            this.Controls.Add(this.datePeriodControlDate);
            this.Controls.Add(this.singleLineComboCountry);
            this.Controls.Add(this.singleLineComboUser);
            this.Controls.Add(this.singleLineComboVessel);
            this.Controls.Add(this.singleLineComboPort);
            this.Controls.Add(this.comboBoxAccidentKind);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSearch);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AccidentListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "事故・トラブル";
            this.Load += new System.EventHandler(this.AccidentListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAccident)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Controls.DatePeriodControl datePeriodControlDate;
        public Util.SingleLineCombo singleLineComboCountry;
        public Util.SingleLineCombo singleLineComboUser;
        public Util.SingleLineCombo singleLineComboVessel;
        public Util.SingleLineCombo singleLineComboPort;
        public System.Windows.Forms.ComboBox comboBoxAccidentKind;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonSearch;
        public System.Windows.Forms.ComboBox comboBoxKindOfAccident;
        public System.Windows.Forms.ComboBox comboBoxSituation;
        public System.Windows.Forms.TextBox textBoxKeyword;
        private System.Windows.Forms.Button buttonOutputExcel;
        private System.Windows.Forms.Button buttonDetail;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridView dataGridViewAccident;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button buttonBlueUpdate;
        private System.Windows.Forms.CheckBox checkBoxBlueReverse;
    }
}