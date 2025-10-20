namespace NBaseMaster.基地管理
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class Template1
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
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

        #region MultiRow Template Designer generated code

		/// <summary> 
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
        private void InitializeComponent()
        {
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.headerCell1 = new GrapeCity.Win.MultiRow.HeaderCell();
            this.headerCell2 = new GrapeCity.Win.MultiRow.HeaderCell();
            this.headerCell3 = new GrapeCity.Win.MultiRow.HeaderCell();
            this.headerCell4 = new GrapeCity.Win.MultiRow.HeaderCell();
            this.comboBoxCell_Cargo = new GrapeCity.Win.MultiRow.ComboBoxCell();
            this.textBoxCell_tumi = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.textBoxCell_age = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.buttonCell_Remove = new GrapeCity.Win.MultiRow.ButtonCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.comboBoxCell_Cargo);
            this.Row.Cells.Add(this.textBoxCell_tumi);
            this.Row.Cells.Add(this.textBoxCell_age);
            this.Row.Cells.Add(this.buttonCell_Remove);
            this.Row.Height = 21;
            this.Row.Width = 384;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.BackColor = System.Drawing.Color.LightGray;
            this.columnHeaderSection1.Cells.Add(this.headerCell1);
            this.columnHeaderSection1.Cells.Add(this.headerCell2);
            this.columnHeaderSection1.Cells.Add(this.headerCell3);
            this.columnHeaderSection1.Cells.Add(this.headerCell4);
            this.columnHeaderSection1.Height = 36;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 384;
            // 
            // headerCell1
            // 
            this.headerCell1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.headerCell1.Location = new System.Drawing.Point(0, 0);
            this.headerCell1.Name = "headerCell1";
            this.headerCell1.Size = new System.Drawing.Size(157, 36);
            cellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            this.headerCell1.Style = cellStyle2;
            this.headerCell1.TabIndex = 0;
            this.headerCell1.UseVisualStyleBackColor = false;
            this.headerCell1.Value = "貨物";
            // 
            // headerCell2
            // 
            this.headerCell2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.headerCell2.Location = new System.Drawing.Point(157, 0);
            this.headerCell2.Name = "headerCell2";
            this.headerCell2.Size = new System.Drawing.Size(80, 36);
            cellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            this.headerCell2.Style = cellStyle3;
            this.headerCell2.TabIndex = 1;
            this.headerCell2.UseVisualStyleBackColor = false;
            this.headerCell2.Value = "積み時間\r\n（L／分）";
            // 
            // headerCell3
            // 
            this.headerCell3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.headerCell3.Location = new System.Drawing.Point(237, 0);
            this.headerCell3.Name = "headerCell3";
            this.headerCell3.Size = new System.Drawing.Size(80, 36);
            cellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            this.headerCell3.Style = cellStyle4;
            this.headerCell3.TabIndex = 2;
            this.headerCell3.UseVisualStyleBackColor = false;
            this.headerCell3.Value = "揚げ時間\r\n（L／分）";
            // 
            // headerCell4
            // 
            this.headerCell4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.headerCell4.Location = new System.Drawing.Point(317, 0);
            this.headerCell4.Name = "headerCell4";
            this.headerCell4.Size = new System.Drawing.Size(67, 36);
            cellStyle5.BackColor = System.Drawing.Color.Gainsboro;
            this.headerCell4.Style = cellStyle5;
            this.headerCell4.TabIndex = 3;
            this.headerCell4.UseVisualStyleBackColor = false;
            // 
            // comboBoxCell_Cargo
            // 
            this.comboBoxCell_Cargo.Location = new System.Drawing.Point(0, 0);
            this.comboBoxCell_Cargo.Name = "comboBoxCell_Cargo";
            this.comboBoxCell_Cargo.Size = new System.Drawing.Size(157, 21);
            this.comboBoxCell_Cargo.TabIndex = 0;
            this.comboBoxCell_Cargo.ValueAsIndex = true;
            // 
            // textBoxCell_tumi
            // 
            this.textBoxCell_tumi.Location = new System.Drawing.Point(157, 0);
            this.textBoxCell_tumi.MaxLength = 4;
            this.textBoxCell_tumi.Name = "textBoxCell_tumi";
            cellStyle1.Format = "";
            this.textBoxCell_tumi.Style = cellStyle1;
            this.textBoxCell_tumi.TabIndex = 1;
            // 
            // textBoxCell_age
            // 
            this.textBoxCell_age.Location = new System.Drawing.Point(237, 0);
            this.textBoxCell_age.MaxLength = 4;
            this.textBoxCell_age.Name = "textBoxCell_age";
            this.textBoxCell_age.TabIndex = 2;
            // 
            // buttonCell_Remove
            // 
            this.buttonCell_Remove.Location = new System.Drawing.Point(317, 0);
            this.buttonCell_Remove.Name = "buttonCell_Remove";
            this.buttonCell_Remove.Size = new System.Drawing.Size(67, 21);
            this.buttonCell_Remove.TabIndex = 3;
            this.buttonCell_Remove.Value = "削除";
            // 
            // Template1
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 57;
            this.Width = 384;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ComboBoxCell comboBoxCell_Cargo;
        private GrapeCity.Win.MultiRow.TextBoxCell textBoxCell_tumi;
        private GrapeCity.Win.MultiRow.TextBoxCell textBoxCell_age;
        private GrapeCity.Win.MultiRow.HeaderCell headerCell1;
        private GrapeCity.Win.MultiRow.HeaderCell headerCell2;
        private GrapeCity.Win.MultiRow.HeaderCell headerCell3;
        private GrapeCity.Win.MultiRow.ButtonCell buttonCell_Remove;
        private GrapeCity.Win.MultiRow.HeaderCell headerCell4;
    }
}
