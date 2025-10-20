namespace NBaseMaster.MsVessel
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.headerCell1 = new GrapeCity.Win.MultiRow.HeaderCell();
            this.comboBoxCell_Cargo = new GrapeCity.Win.MultiRow.ComboBoxCell();
            this.headerCell4 = new GrapeCity.Win.MultiRow.HeaderCell();
            this.buttonCell_Remove = new GrapeCity.Win.MultiRow.ButtonCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.comboBoxCell_Cargo);
            this.Row.Cells.Add(this.buttonCell_Remove);
            this.Row.Height = 21;
            this.Row.Width = 224;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.headerCell1);
            this.columnHeaderSection1.Cells.Add(this.headerCell4);
            this.columnHeaderSection1.Height = 36;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 224;
            // 
            // headerCell1
            // 
            this.headerCell1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.headerCell1.Location = new System.Drawing.Point(0, 0);
            this.headerCell1.Name = "headerCell1";
            this.headerCell1.Size = new System.Drawing.Size(157, 36);
            cellStyle5.BackColor = System.Drawing.Color.Gainsboro;
            this.headerCell1.Style = cellStyle5;
            this.headerCell1.TabIndex = 0;
            this.headerCell1.UseVisualStyleBackColor = false;
            this.headerCell1.Value = "貨物";
            // 
            // comboBoxCell_Cargo
            // 
            this.comboBoxCell_Cargo.Location = new System.Drawing.Point(0, 0);
            this.comboBoxCell_Cargo.Name = "comboBoxCell_Cargo";
            this.comboBoxCell_Cargo.Size = new System.Drawing.Size(157, 21);
            this.comboBoxCell_Cargo.TabIndex = 0;
            this.comboBoxCell_Cargo.ValueAsIndex = true;
            // 
            // headerCell4
            // 
            this.headerCell4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.headerCell4.Location = new System.Drawing.Point(157, 0);
            this.headerCell4.Name = "headerCell4";
            this.headerCell4.Size = new System.Drawing.Size(67, 36);
            cellStyle6.BackColor = System.Drawing.Color.Gainsboro;
            this.headerCell4.Style = cellStyle6;
            this.headerCell4.TabIndex = 1;
            this.headerCell4.UseVisualStyleBackColor = false;
            // 
            // buttonCell_Remove
            // 
            this.buttonCell_Remove.Location = new System.Drawing.Point(157, 0);
            this.buttonCell_Remove.Name = "buttonCell_Remove";
            this.buttonCell_Remove.Size = new System.Drawing.Size(67, 21);
            this.buttonCell_Remove.TabIndex = 1;
            this.buttonCell_Remove.Value = "削除";
            // 
            // Template1
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 57;
            this.Width = 224;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.HeaderCell headerCell1;
        private GrapeCity.Win.MultiRow.ComboBoxCell comboBoxCell_Cargo;
        private GrapeCity.Win.MultiRow.HeaderCell headerCell4;
        private GrapeCity.Win.MultiRow.ButtonCell buttonCell_Remove;
    }
}
