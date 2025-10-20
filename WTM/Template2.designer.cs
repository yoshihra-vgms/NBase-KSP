namespace WTM
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class Template2
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
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.labelCell1 = new GrapeCity.Win.MultiRow.LabelCell();
            this.labelCell3 = new GrapeCity.Win.MultiRow.LabelCell();
            this.labelCellDate = new GrapeCity.Win.MultiRow.LabelCell();
            this.labelCellTime = new GrapeCity.Win.MultiRow.LabelCell();
            // 
            // Row
            // 
            border1.Bottom = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            this.Row.Border = border1;
            this.Row.Cells.Add(this.labelCellDate);
            this.Row.Cells.Add(this.labelCellTime);
            this.Row.Height = 122;
            this.Row.Width = 1102;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.BackColor = System.Drawing.Color.LightGray;
            border4.Bottom = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            this.columnHeaderSection1.Border = border4;
            this.columnHeaderSection1.Cells.Add(this.labelCell1);
            this.columnHeaderSection1.Cells.Add(this.labelCell3);
            this.columnHeaderSection1.Height = 28;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 1102;
            // 
            // labelCell1
            // 
            this.labelCell1.Location = new System.Drawing.Point(0, 0);
            this.labelCell1.Name = "labelCell1";
            this.labelCell1.Size = new System.Drawing.Size(49, 28);
            cellStyle3.Border = border5;
            cellStyle3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.labelCell1.Style = cellStyle3;
            this.labelCell1.TabIndex = 0;
            this.labelCell1.Value = "時間";
            // 
            // labelCell3
            // 
            this.labelCell3.Location = new System.Drawing.Point(122, 0);
            this.labelCell3.Name = "labelCell3";
            this.labelCell3.Size = new System.Drawing.Size(97, 28);
            cellStyle4.Border = border6;
            cellStyle4.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.labelCell3.Style = cellStyle4;
            this.labelCell3.TabIndex = 2;
            this.labelCell3.Value = "作業内容";
            // 
            // labelCellDate
            // 
            this.labelCellDate.Location = new System.Drawing.Point(10, 19);
            this.labelCellDate.Name = "labelCellDate";
            this.labelCellDate.Selectable = false;
            cellStyle1.Border = border2;
            cellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.labelCellDate.Style = cellStyle1;
            this.labelCellDate.TabIndex = 0;
            // 
            // labelCellTime
            // 
            this.labelCellTime.Location = new System.Drawing.Point(10, 49);
            this.labelCellTime.Name = "labelCellTime";
            this.labelCellTime.Selectable = false;
            cellStyle2.Border = border3;
            cellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.labelCellTime.Style = cellStyle2;
            this.labelCellTime.TabIndex = 0;
            // 
            // Template2
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 150;
            this.Width = 1102;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.LabelCell labelCellDate;
        private GrapeCity.Win.MultiRow.LabelCell labelCellTime;
        private GrapeCity.Win.MultiRow.LabelCell labelCell1;
        private GrapeCity.Win.MultiRow.LabelCell labelCell3;
    }
}
