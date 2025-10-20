namespace DeficiencyControl.Forms
{
    partial class PscListForm
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
            this.dataGridViewPSC = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.singleLineComboCountry = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboUser = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboVessel = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboPort = new DeficiencyControl.Util.SingleLineCombo();
            this.comboBoxItemKind = new System.Windows.Forms.ComboBox();
            this.textBoxKeyword = new System.Windows.Forms.TextBox();
            this.buttonDetail = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonOutputExcel = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.datePeriodControlDate = new DeficiencyControl.Controls.DatePeriodControl();
            this.deficiencyCodeSelectControl1 = new DeficiencyControl.Controls.CommentItem.DeficiencyCodeSelectControl();
            this.actionCodeControl1 = new DeficiencyControl.Controls.CommentItem.ActionCodeControl();
            this.checkBoxStatusPending = new System.Windows.Forms.CheckBox();
            this.checkBoxStatusComplete = new System.Windows.Forms.CheckBox();
            this.dataCountControl1 = new DeficiencyControl.Controls.DataCountControl();
            this.labelDescriptionControl1 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl2 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl3 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl4 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl5 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl6 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl7 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl8 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl9 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl10 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl11 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.checkBoxDeficiencyZero = new System.Windows.Forms.CheckBox();
            this.checkBoxBlueReverse = new System.Windows.Forms.CheckBox();
            this.buttonBlueUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPSC)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPSC
            // 
            this.dataGridViewPSC.AllowUserToAddRows = false;
            this.dataGridViewPSC.AllowUserToDeleteRows = false;
            this.dataGridViewPSC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPSC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPSC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column12,
            this.Column2,
            this.Column3,
            this.Column7,
            this.Column9,
            this.Column4,
            this.Column10,
            this.Column6,
            this.Column11,
            this.Column8,
            this.Column5});
            this.dataGridViewPSC.EnableHeadersVisualStyles = false;
            this.dataGridViewPSC.Location = new System.Drawing.Point(15, 356);
            this.dataGridViewPSC.MultiSelect = false;
            this.dataGridViewPSC.Name = "dataGridViewPSC";
            this.dataGridViewPSC.ReadOnly = true;
            this.dataGridViewPSC.RowHeadersVisible = false;
            this.dataGridViewPSC.RowTemplate.Height = 21;
            this.dataGridViewPSC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPSC.Size = new System.Drawing.Size(1240, 258);
            this.dataGridViewPSC.TabIndex = 17;
            this.dataGridViewPSC.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPSC_CellDoubleClick);
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
            this.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column12.Width = 40;
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
            this.Column10.HeaderText = "D/C\\n(指摘事項\\nコード)";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 120;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Deficiency (指摘事項)";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 220;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "A/C (アクションコード)\\n1/2/3";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 180;
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
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(1121, 67);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(130, 50);
            this.buttonClear.TabIndex = 13;
            this.buttonClear.Text = "Clear\r\n(クリア)";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(1121, 11);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(130, 50);
            this.buttonSearch.TabIndex = 12;
            this.buttonSearch.Text = "Search\r\n(検索)";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // singleLineComboCountry
            // 
            this.singleLineComboCountry.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboCountry.Location = new System.Drawing.Point(626, 47);
            this.singleLineComboCountry.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboCountry.MaxLength = 32767;
            this.singleLineComboCountry.Name = "singleLineComboCountry";
            this.singleLineComboCountry.ReadOnly = false;
            this.singleLineComboCountry.Size = new System.Drawing.Size(396, 23);
            this.singleLineComboCountry.TabIndex = 3;
            // 
            // singleLineComboUser
            // 
            this.singleLineComboUser.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboUser.Location = new System.Drawing.Point(99, 85);
            this.singleLineComboUser.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboUser.MaxLength = 32767;
            this.singleLineComboUser.Name = "singleLineComboUser";
            this.singleLineComboUser.ReadOnly = false;
            this.singleLineComboUser.Size = new System.Drawing.Size(391, 23);
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
            this.singleLineComboVessel.Size = new System.Drawing.Size(391, 23);
            this.singleLineComboVessel.TabIndex = 0;
            // 
            // singleLineComboPort
            // 
            this.singleLineComboPort.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboPort.Location = new System.Drawing.Point(99, 47);
            this.singleLineComboPort.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboPort.MaxLength = 32767;
            this.singleLineComboPort.Name = "singleLineComboPort";
            this.singleLineComboPort.ReadOnly = false;
            this.singleLineComboPort.Size = new System.Drawing.Size(391, 23);
            this.singleLineComboPort.TabIndex = 2;
            // 
            // comboBoxItemKind
            // 
            this.comboBoxItemKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItemKind.Enabled = false;
            this.comboBoxItemKind.FormattingEnabled = true;
            this.comboBoxItemKind.Location = new System.Drawing.Point(626, 11);
            this.comboBoxItemKind.Name = "comboBoxItemKind";
            this.comboBoxItemKind.Size = new System.Drawing.Size(396, 24);
            this.comboBoxItemKind.TabIndex = 1;
            // 
            // textBoxKeyword
            // 
            this.textBoxKeyword.Location = new System.Drawing.Point(99, 256);
            this.textBoxKeyword.Name = "textBoxKeyword";
            this.textBoxKeyword.Size = new System.Drawing.Size(923, 23);
            this.textBoxKeyword.TabIndex = 11;
            // 
            // buttonDetail
            // 
            this.buttonDetail.Location = new System.Drawing.Point(148, 300);
            this.buttonDetail.Name = "buttonDetail";
            this.buttonDetail.Size = new System.Drawing.Size(130, 50);
            this.buttonDetail.TabIndex = 15;
            this.buttonDetail.Text = "Detail\r\n(編集)";
            this.buttonDetail.UseVisualStyleBackColor = true;
            this.buttonDetail.Click += new System.EventHandler(this.buttonDetail_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(12, 300);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(130, 50);
            this.buttonCreate.TabIndex = 14;
            this.buttonCreate.Text = "Create\r\n(新規作成)";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonOutputExcel
            // 
            this.buttonOutputExcel.Location = new System.Drawing.Point(892, 300);
            this.buttonOutputExcel.Name = "buttonOutputExcel";
            this.buttonOutputExcel.Size = new System.Drawing.Size(130, 50);
            this.buttonOutputExcel.TabIndex = 16;
            this.buttonOutputExcel.Text = "Output Excel\r\n(Excel出力)";
            this.buttonOutputExcel.UseVisualStyleBackColor = true;
            this.buttonOutputExcel.Click += new System.EventHandler(this.buttonOutputExcel_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(1122, 620);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(130, 50);
            this.buttonClose.TabIndex = 18;
            this.buttonClose.Text = "Close\r\n(閉じる)";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // datePeriodControlDate
            // 
            this.datePeriodControlDate.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.datePeriodControlDate.Location = new System.Drawing.Point(99, 126);
            this.datePeriodControlDate.Name = "datePeriodControlDate";
            this.datePeriodControlDate.Size = new System.Drawing.Size(400, 29);
            this.datePeriodControlDate.TabIndex = 7;
            // 
            // deficiencyCodeSelectControl1
            // 
            this.deficiencyCodeSelectControl1.AutoSize = true;
            this.deficiencyCodeSelectControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.deficiencyCodeSelectControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.deficiencyCodeSelectControl1.Location = new System.Drawing.Point(141, 170);
            this.deficiencyCodeSelectControl1.Margin = new System.Windows.Forms.Padding(4);
            this.deficiencyCodeSelectControl1.Name = "deficiencyCodeSelectControl1";
            this.deficiencyCodeSelectControl1.Size = new System.Drawing.Size(884, 31);
            this.deficiencyCodeSelectControl1.TabIndex = 9;
            // 
            // actionCodeControl1
            // 
            this.actionCodeControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.actionCodeControl1.Location = new System.Drawing.Point(117, 212);
            this.actionCodeControl1.Name = "actionCodeControl1";
            this.actionCodeControl1.Size = new System.Drawing.Size(373, 31);
            this.actionCodeControl1.TabIndex = 10;
            // 
            // checkBoxStatusPending
            // 
            this.checkBoxStatusPending.AutoSize = true;
            this.checkBoxStatusPending.Checked = true;
            this.checkBoxStatusPending.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStatusPending.Location = new System.Drawing.Point(626, 88);
            this.checkBoxStatusPending.Name = "checkBoxStatusPending";
            this.checkBoxStatusPending.Size = new System.Drawing.Size(79, 20);
            this.checkBoxStatusPending.TabIndex = 5;
            this.checkBoxStatusPending.Text = "Pending";
            this.checkBoxStatusPending.UseVisualStyleBackColor = true;
            // 
            // checkBoxStatusComplete
            // 
            this.checkBoxStatusComplete.AutoSize = true;
            this.checkBoxStatusComplete.Checked = true;
            this.checkBoxStatusComplete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStatusComplete.Location = new System.Drawing.Point(721, 88);
            this.checkBoxStatusComplete.Name = "checkBoxStatusComplete";
            this.checkBoxStatusComplete.Size = new System.Drawing.Size(91, 20);
            this.checkBoxStatusComplete.TabIndex = 6;
            this.checkBoxStatusComplete.Text = "Complete";
            this.checkBoxStatusComplete.UseVisualStyleBackColor = true;
            // 
            // dataCountControl1
            // 
            this.dataCountControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataCountControl1.AutoSize = true;
            this.dataCountControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dataCountControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.dataCountControl1.Location = new System.Drawing.Point(1170, 333);
            this.dataCountControl1.Margin = new System.Windows.Forms.Padding(4);
            this.dataCountControl1.Name = "dataCountControl1";
            this.dataCountControl1.Size = new System.Drawing.Size(81, 16);
            this.dataCountControl1.TabIndex = 284;
            // 
            // labelDescriptionControl1
            // 
            this.labelDescriptionControl1.AutoSize = true;
            this.labelDescriptionControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl1.DescriptionEnabled = true;
            this.labelDescriptionControl1.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl1.DescriptionText = "(船名)";
            this.labelDescriptionControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl1.Location = new System.Drawing.Point(3, 5);
            this.labelDescriptionControl1.MainText = "Vessel";
            this.labelDescriptionControl1.Name = "labelDescriptionControl1";
            this.labelDescriptionControl1.RequiredFlag = false;
            this.labelDescriptionControl1.Size = new System.Drawing.Size(67, 31);
            this.labelDescriptionControl1.TabIndex = 285;
            this.labelDescriptionControl1.TabStop = false;
            // 
            // labelDescriptionControl2
            // 
            this.labelDescriptionControl2.AutoSize = true;
            this.labelDescriptionControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl2.DescriptionEnabled = true;
            this.labelDescriptionControl2.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl2.DescriptionText = "(港名)";
            this.labelDescriptionControl2.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl2.Location = new System.Drawing.Point(3, 42);
            this.labelDescriptionControl2.MainText = "Port";
            this.labelDescriptionControl2.Name = "labelDescriptionControl2";
            this.labelDescriptionControl2.RequiredFlag = false;
            this.labelDescriptionControl2.Size = new System.Drawing.Size(63, 31);
            this.labelDescriptionControl2.TabIndex = 286;
            this.labelDescriptionControl2.TabStop = false;
            // 
            // labelDescriptionControl3
            // 
            this.labelDescriptionControl3.AutoSize = true;
            this.labelDescriptionControl3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl3.DescriptionEnabled = true;
            this.labelDescriptionControl3.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl3.DescriptionText = "(入力者)";
            this.labelDescriptionControl3.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl3.Location = new System.Drawing.Point(3, 79);
            this.labelDescriptionControl3.MainText = "P.I.C";
            this.labelDescriptionControl3.Name = "labelDescriptionControl3";
            this.labelDescriptionControl3.RequiredFlag = false;
            this.labelDescriptionControl3.Size = new System.Drawing.Size(78, 31);
            this.labelDescriptionControl3.TabIndex = 287;
            this.labelDescriptionControl3.TabStop = false;
            // 
            // labelDescriptionControl4
            // 
            this.labelDescriptionControl4.AutoSize = true;
            this.labelDescriptionControl4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl4.DescriptionEnabled = true;
            this.labelDescriptionControl4.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl4.DescriptionText = "(日付)";
            this.labelDescriptionControl4.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl4.Location = new System.Drawing.Point(3, 124);
            this.labelDescriptionControl4.MainText = "Date";
            this.labelDescriptionControl4.Name = "labelDescriptionControl4";
            this.labelDescriptionControl4.RequiredFlag = false;
            this.labelDescriptionControl4.Size = new System.Drawing.Size(63, 31);
            this.labelDescriptionControl4.TabIndex = 288;
            this.labelDescriptionControl4.TabStop = false;
            // 
            // labelDescriptionControl5
            // 
            this.labelDescriptionControl5.AutoSize = true;
            this.labelDescriptionControl5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl5.DescriptionEnabled = true;
            this.labelDescriptionControl5.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl5.DescriptionText = "(種類)";
            this.labelDescriptionControl5.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl5.Location = new System.Drawing.Point(527, 5);
            this.labelDescriptionControl5.MainText = "Kind";
            this.labelDescriptionControl5.Name = "labelDescriptionControl5";
            this.labelDescriptionControl5.RequiredFlag = false;
            this.labelDescriptionControl5.Size = new System.Drawing.Size(63, 31);
            this.labelDescriptionControl5.TabIndex = 289;
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
            this.labelDescriptionControl6.Location = new System.Drawing.Point(527, 42);
            this.labelDescriptionControl6.MainText = "Country";
            this.labelDescriptionControl6.Name = "labelDescriptionControl6";
            this.labelDescriptionControl6.RequiredFlag = false;
            this.labelDescriptionControl6.Size = new System.Drawing.Size(79, 31);
            this.labelDescriptionControl6.TabIndex = 289;
            this.labelDescriptionControl6.TabStop = false;
            // 
            // labelDescriptionControl7
            // 
            this.labelDescriptionControl7.AutoSize = true;
            this.labelDescriptionControl7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl7.DescriptionEnabled = true;
            this.labelDescriptionControl7.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl7.DescriptionText = "(状態)";
            this.labelDescriptionControl7.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl7.Location = new System.Drawing.Point(527, 79);
            this.labelDescriptionControl7.MainText = "Status";
            this.labelDescriptionControl7.Name = "labelDescriptionControl7";
            this.labelDescriptionControl7.RequiredFlag = false;
            this.labelDescriptionControl7.Size = new System.Drawing.Size(69, 31);
            this.labelDescriptionControl7.TabIndex = 289;
            this.labelDescriptionControl7.TabStop = false;
            // 
            // labelDescriptionControl8
            // 
            this.labelDescriptionControl8.AutoSize = true;
            this.labelDescriptionControl8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl8.DescriptionEnabled = true;
            this.labelDescriptionControl8.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl8.DescriptionText = "(指摘事項コード)";
            this.labelDescriptionControl8.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl8.Location = new System.Drawing.Point(3, 170);
            this.labelDescriptionControl8.MainText = "Deficiency Code";
            this.labelDescriptionControl8.Name = "labelDescriptionControl8";
            this.labelDescriptionControl8.RequiredFlag = false;
            this.labelDescriptionControl8.Size = new System.Drawing.Size(133, 31);
            this.labelDescriptionControl8.TabIndex = 288;
            this.labelDescriptionControl8.TabStop = false;
            // 
            // labelDescriptionControl9
            // 
            this.labelDescriptionControl9.AutoSize = true;
            this.labelDescriptionControl9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl9.DescriptionEnabled = false;
            this.labelDescriptionControl9.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl9.DescriptionText = "";
            this.labelDescriptionControl9.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl9.Location = new System.Drawing.Point(3, 218);
            this.labelDescriptionControl9.MainText = "Action Code";
            this.labelDescriptionControl9.Name = "labelDescriptionControl9";
            this.labelDescriptionControl9.RequiredFlag = false;
            this.labelDescriptionControl9.Size = new System.Drawing.Size(107, 16);
            this.labelDescriptionControl9.TabIndex = 290;
            this.labelDescriptionControl9.TabStop = false;
            // 
            // labelDescriptionControl10
            // 
            this.labelDescriptionControl10.AutoSize = true;
            this.labelDescriptionControl10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl10.DescriptionEnabled = true;
            this.labelDescriptionControl10.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl10.DescriptionText = "(キーワード)";
            this.labelDescriptionControl10.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl10.Location = new System.Drawing.Point(3, 248);
            this.labelDescriptionControl10.MainText = "Keyword";
            this.labelDescriptionControl10.Name = "labelDescriptionControl10";
            this.labelDescriptionControl10.RequiredFlag = false;
            this.labelDescriptionControl10.Size = new System.Drawing.Size(91, 31);
            this.labelDescriptionControl10.TabIndex = 288;
            this.labelDescriptionControl10.TabStop = false;
            // 
            // labelDescriptionControl11
            // 
            this.labelDescriptionControl11.AutoSize = true;
            this.labelDescriptionControl11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl11.DescriptionEnabled = true;
            this.labelDescriptionControl11.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl11.DescriptionText = "(指摘件数)";
            this.labelDescriptionControl11.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl11.Location = new System.Drawing.Point(527, 124);
            this.labelDescriptionControl11.MainText = "Deficiency";
            this.labelDescriptionControl11.Name = "labelDescriptionControl11";
            this.labelDescriptionControl11.RequiredFlag = false;
            this.labelDescriptionControl11.Size = new System.Drawing.Size(93, 31);
            this.labelDescriptionControl11.TabIndex = 289;
            this.labelDescriptionControl11.TabStop = false;
            // 
            // checkBoxDeficiencyZero
            // 
            this.checkBoxDeficiencyZero.AutoSize = true;
            this.checkBoxDeficiencyZero.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.checkBoxDeficiencyZero.Location = new System.Drawing.Point(626, 130);
            this.checkBoxDeficiencyZero.Name = "checkBoxDeficiencyZero";
            this.checkBoxDeficiencyZero.Size = new System.Drawing.Size(90, 19);
            this.checkBoxDeficiencyZero.TabIndex = 8;
            this.checkBoxDeficiencyZero.Text = "0件を表示";
            this.checkBoxDeficiencyZero.UseVisualStyleBackColor = true;
            // 
            // checkBoxBlueReverse
            // 
            this.checkBoxBlueReverse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxBlueReverse.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxBlueReverse.AutoSize = true;
            this.checkBoxBlueReverse.BackColor = System.Drawing.Color.LightSkyBlue;
            this.checkBoxBlueReverse.Location = new System.Drawing.Point(1201, 256);
            this.checkBoxBlueReverse.Name = "checkBoxBlueReverse";
            this.checkBoxBlueReverse.Size = new System.Drawing.Size(50, 26);
            this.checkBoxBlueReverse.TabIndex = 291;
            this.checkBoxBlueReverse.Text = "反転";
            this.checkBoxBlueReverse.UseVisualStyleBackColor = false;
            this.checkBoxBlueReverse.Visible = false;
            this.checkBoxBlueReverse.CheckedChanged += new System.EventHandler(this.checkBoxBlueReverse_CheckedChanged);
            // 
            // buttonBlueUpdate
            // 
            this.buttonBlueUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBlueUpdate.BackColor = System.Drawing.Color.LightSkyBlue;
            this.buttonBlueUpdate.Location = new System.Drawing.Point(1121, 256);
            this.buttonBlueUpdate.Name = "buttonBlueUpdate";
            this.buttonBlueUpdate.Size = new System.Drawing.Size(50, 26);
            this.buttonBlueUpdate.TabIndex = 292;
            this.buttonBlueUpdate.Text = "更新";
            this.buttonBlueUpdate.UseVisualStyleBackColor = false;
            this.buttonBlueUpdate.Visible = false;
            this.buttonBlueUpdate.Click += new System.EventHandler(this.buttonBlueUpdate_Click);
            // 
            // PscListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.buttonBlueUpdate);
            this.Controls.Add(this.checkBoxBlueReverse);
            this.Controls.Add(this.labelDescriptionControl9);
            this.Controls.Add(this.labelDescriptionControl11);
            this.Controls.Add(this.labelDescriptionControl7);
            this.Controls.Add(this.labelDescriptionControl6);
            this.Controls.Add(this.labelDescriptionControl5);
            this.Controls.Add(this.labelDescriptionControl10);
            this.Controls.Add(this.labelDescriptionControl8);
            this.Controls.Add(this.labelDescriptionControl4);
            this.Controls.Add(this.labelDescriptionControl3);
            this.Controls.Add(this.labelDescriptionControl2);
            this.Controls.Add(this.labelDescriptionControl1);
            this.Controls.Add(this.dataCountControl1);
            this.Controls.Add(this.checkBoxStatusComplete);
            this.Controls.Add(this.checkBoxDeficiencyZero);
            this.Controls.Add(this.checkBoxStatusPending);
            this.Controls.Add(this.actionCodeControl1);
            this.Controls.Add(this.deficiencyCodeSelectControl1);
            this.Controls.Add(this.datePeriodControlDate);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOutputExcel);
            this.Controls.Add(this.buttonDetail);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.singleLineComboCountry);
            this.Controls.Add(this.singleLineComboUser);
            this.Controls.Add(this.singleLineComboVessel);
            this.Controls.Add(this.singleLineComboPort);
            this.Controls.Add(this.comboBoxItemKind);
            this.Controls.Add(this.textBoxKeyword);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.dataGridViewPSC);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PscListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PSC (PSC 指摘事項管理)";
            this.Load += new System.EventHandler(this.PscListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPSC)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPSC;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonDetail;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonOutputExcel;
        private System.Windows.Forms.Button buttonClose;
        public Util.SingleLineCombo singleLineComboCountry;
        public Util.SingleLineCombo singleLineComboUser;
        public Util.SingleLineCombo singleLineComboVessel;
        public Util.SingleLineCombo singleLineComboPort;
        public System.Windows.Forms.ComboBox comboBoxItemKind;
        public System.Windows.Forms.TextBox textBoxKeyword;
        public Controls.DatePeriodControl datePeriodControlDate;
        public Controls.CommentItem.DeficiencyCodeSelectControl deficiencyCodeSelectControl1;
        public Controls.CommentItem.ActionCodeControl actionCodeControl1;
        public System.Windows.Forms.CheckBox checkBoxStatusPending;
        public System.Windows.Forms.CheckBox checkBoxStatusComplete;
        public Controls.DataCountControl dataCountControl1;
        private Controls.LabelDescriptionControl labelDescriptionControl1;
        private Controls.LabelDescriptionControl labelDescriptionControl2;
        private Controls.LabelDescriptionControl labelDescriptionControl3;
        private Controls.LabelDescriptionControl labelDescriptionControl4;
        private Controls.LabelDescriptionControl labelDescriptionControl5;
        private Controls.LabelDescriptionControl labelDescriptionControl6;
        private Controls.LabelDescriptionControl labelDescriptionControl7;
        private Controls.LabelDescriptionControl labelDescriptionControl8;
        private Controls.LabelDescriptionControl labelDescriptionControl9;
        private Controls.LabelDescriptionControl labelDescriptionControl10;
        private Controls.LabelDescriptionControl labelDescriptionControl11;
        public System.Windows.Forms.CheckBox checkBoxDeficiencyZero;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.CheckBox checkBoxBlueReverse;
        private System.Windows.Forms.Button buttonBlueUpdate;
    }
}