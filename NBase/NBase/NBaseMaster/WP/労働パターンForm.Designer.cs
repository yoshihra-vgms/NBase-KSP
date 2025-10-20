
namespace NBaseMaster.WP
{
    partial class 労働パターンForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            GrapeCity.Win.CalendarGrid.CalendarListView calendarListView1 = new GrapeCity.Win.CalendarGrid.CalendarListView();
            GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyle calendarConditionalCellStyle1 = new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle1 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle2 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle3 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle4 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarTemplate calendarTemplate1 = new GrapeCity.Win.CalendarGrid.CalendarTemplate();
            GrapeCity.Win.CalendarGrid.CalendarHeaderCellType calendarHeaderCellType1 = new GrapeCity.Win.CalendarGrid.CalendarHeaderCellType();
            GrapeCity.Win.CalendarGrid.CalendarHeaderCellType calendarHeaderCellType2 = new GrapeCity.Win.CalendarGrid.CalendarHeaderCellType();
            GrapeCity.Win.CalendarGrid.CalendarHeaderCellType calendarHeaderCellType3 = new GrapeCity.Win.CalendarGrid.CalendarHeaderCellType();
            this.gcCalendarGrid1 = new GrapeCity.Win.CalendarGrid.GcCalendarGrid();
            this.calendarTitleCaption1 = new GrapeCity.Win.CalendarGrid.CalendarTitleCaption();
            this.calendarTitleButton1 = new GrapeCity.Win.CalendarGrid.CalendarTitleButton();
            this.calendarTitleButton2 = new GrapeCity.Win.CalendarGrid.CalendarTitleButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Vessel = new System.Windows.Forms.ComboBox();
            this.radioButton_Day1 = new System.Windows.Forms.RadioButton();
            this.radioButton_Day2 = new System.Windows.Forms.RadioButton();
            this.radioButton_Day3 = new System.Windows.Forms.RadioButton();
            this.radioButton_Day4 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcCalendarGrid1)).BeginInit();
            this.gcCalendarGrid1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcCalendarGrid1
            // 
            this.gcCalendarGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            calendarListView1.DayCount = 1;
            this.gcCalendarGrid1.CalendarView = calendarListView1;
            this.gcCalendarGrid1.Commands.AddRange(new GrapeCity.Win.CalendarGrid.CalendarGridCommand[] {
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveLeft, System.Windows.Forms.Keys.Left),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveRight, System.Windows.Forms.Keys.Right),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveUp, System.Windows.Forms.Keys.Up),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveDown, System.Windows.Forms.Keys.Down),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToFirstCell, System.Windows.Forms.Keys.Home),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToLastCell, System.Windows.Forms.Keys.End),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToPreviousPage, System.Windows.Forms.Keys.PageUp),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToNextPage, System.Windows.Forms.Keys.Next),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToPreviousCell, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToNextCell, System.Windows.Forms.Keys.Tab),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToPreviousDate, ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                            | System.Windows.Forms.Keys.Tab)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToNextDate, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectAll, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectLeft, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectRight, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectUp, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectDown, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectToFirstCell, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectToLastCell, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectToPreviousPage, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.SelectToNextPage, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.BeginEdit, System.Windows.Forms.Keys.F2),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.ToggleEdit, System.Windows.Forms.Keys.Return),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Clear, System.Windows.Forms.Keys.Delete),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Copy, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Copy, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Cut, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Cut, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Paste, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Paste, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.CancelEdit, System.Windows.Forms.Keys.Escape),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.ShowDropDown, System.Windows.Forms.Keys.F4),
            new GrapeCity.Win.CalendarGrid.CalendarGridKeyboardCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.ShowDropDown, ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))),
            new GrapeCity.Win.CalendarGrid.CalendarGridMouseCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.Zoom(0.1F), GrapeCity.Win.CalendarGrid.CalendarMouseGesture.CtrlWheel),
            new GrapeCity.Win.CalendarGrid.CalendarGridTouchCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToNextPage, GrapeCity.Win.CalendarGrid.CalendarTouchGesture.SwipeLeft),
            new GrapeCity.Win.CalendarGrid.CalendarGridTouchCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToPreviousPage, GrapeCity.Win.CalendarGrid.CalendarTouchGesture.SwipeRight),
            new GrapeCity.Win.CalendarGrid.CalendarGridTouchCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToNextPage, GrapeCity.Win.CalendarGrid.CalendarTouchGesture.SwipeUp),
            new GrapeCity.Win.CalendarGrid.CalendarGridTouchCommand(GrapeCity.Win.CalendarGrid.CalendarGridActions.MoveToPreviousPage, GrapeCity.Win.CalendarGrid.CalendarTouchGesture.SwipeDown)});
            this.gcCalendarGrid1.CurrentDate = null;
            this.gcCalendarGrid1.Location = new System.Drawing.Point(12, 110);
            this.gcCalendarGrid1.Name = "gcCalendarGrid1";
            this.gcCalendarGrid1.Protected = true;
            this.gcCalendarGrid1.Size = new System.Drawing.Size(1585, 719);
            calendarCellStyle1.ForeColor = System.Drawing.Color.Red;
            calendarCellStyle2.ForeColor = System.Drawing.Color.Blue;
            calendarCellStyle3.ForeColor = System.Drawing.Color.Red;
            calendarCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            calendarCellStyle4.ForeColor = System.Drawing.Color.Gray;
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle1, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsSunday));
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle2, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsSaturday));
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle3, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsHoliday));
            calendarConditionalCellStyle1.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle4, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsTrailingDay));
            calendarConditionalCellStyle1.Name = "defaultStyle";
            this.gcCalendarGrid1.Styles.Add(calendarConditionalCellStyle1);
            this.gcCalendarGrid1.TabIndex = 0;
            calendarTemplate1.ColumnCount = 0;
            calendarTemplate1.ColumnHeaderRowCount = 2;
            calendarHeaderCellType1.SupportLocalization = true;
            calendarTemplate1.CornerHeader.GetCell(0, 0).CellType = calendarHeaderCellType1;
            calendarHeaderCellType2.SupportLocalization = true;
            calendarTemplate1.CornerHeader.GetCell(1, 0).CellType = calendarHeaderCellType2;
            calendarTemplate1.ColumnHeader.CellStyleName = "defaultStyle";
            calendarHeaderCellType3.SupportLocalization = true;
            calendarTemplate1.RowHeader.GetCell(0, 0).CellType = calendarHeaderCellType3;
            calendarTemplate1.Content.CellStyleName = "defaultStyle";
            this.gcCalendarGrid1.Template = calendarTemplate1;
            this.gcCalendarGrid1.TitleFooter.Visible = false;
            this.gcCalendarGrid1.TitleHeader.Children.Add(this.calendarTitleCaption1);
            this.gcCalendarGrid1.TitleHeader.Children.Add(this.calendarTitleButton1);
            this.gcCalendarGrid1.TitleHeader.Children.Add(this.calendarTitleButton2);
            this.gcCalendarGrid1.TitleHeader.Visible = false;
            this.gcCalendarGrid1.CellContentClick += new System.EventHandler<GrapeCity.Win.CalendarGrid.CalendarCellEventArgs>(this.gcCalendarGrid1_CellContentClick);
            this.gcCalendarGrid1.CellMouseClick += new System.EventHandler<GrapeCity.Win.CalendarGrid.CalendarCellMouseEventArgs>(this.gcCalendarGrid1_CellMouseClick);
            this.gcCalendarGrid1.CellMouseDoubleClick += new System.EventHandler<GrapeCity.Win.CalendarGrid.CalendarCellMouseEventArgs>(this.gcCalendarGrid1_CellMouseDoubleClick);
            this.gcCalendarGrid1.CellMouseUp += new System.EventHandler<GrapeCity.Win.CalendarGrid.CalendarCellMouseEventArgs>(this.gcCalendarGrid1_CellMouseUp);
            this.gcCalendarGrid1.AppointmentCellDragging += new System.EventHandler<GrapeCity.Win.CalendarGrid.AppointmentCellDraggingEventArgs>(this.gcCalendarGrid1_AppointmentCellDragging);
            // 
            // calendarTitleCaption1
            // 
            this.calendarTitleCaption1.DateFormat = "{LongDate}({ShortestDayOfWeek})";
            this.calendarTitleCaption1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.calendarTitleCaption1.HorizontalAlignment = GrapeCity.Win.CalendarGrid.CalendarHorizontalAlignment.Center;
            this.calendarTitleCaption1.Name = "calendarTitleCaption1";
            this.calendarTitleCaption1.Text = "{0}";
            // 
            // calendarTitleButton1
            // 
            this.calendarTitleButton1.ButtonBehavior = GrapeCity.Win.CalendarGrid.CalendarTitleButtonBehavior.Next;
            this.calendarTitleButton1.HorizontalAlignment = GrapeCity.Win.CalendarGrid.CalendarHorizontalAlignment.Right;
            this.calendarTitleButton1.Name = "calendarTitleButton1";
            this.calendarTitleButton1.Text = ">";
            this.calendarTitleButton1.ToolTipText = "{0}";
            // 
            // calendarTitleButton2
            // 
            this.calendarTitleButton2.ButtonBehavior = GrapeCity.Win.CalendarGrid.CalendarTitleButtonBehavior.Previous;
            this.calendarTitleButton2.Name = "calendarTitleButton2";
            this.calendarTitleButton2.Text = "<";
            this.calendarTitleButton2.ToolTipText = "{0}";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(339, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1258, 92);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "船：";
            // 
            // comboBox_Vessel
            // 
            this.comboBox_Vessel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Vessel.FormattingEnabled = true;
            this.comboBox_Vessel.Location = new System.Drawing.Point(52, 19);
            this.comboBox_Vessel.Name = "comboBox_Vessel";
            this.comboBox_Vessel.Size = new System.Drawing.Size(190, 20);
            this.comboBox_Vessel.TabIndex = 6;
            this.comboBox_Vessel.SelectedIndexChanged += new System.EventHandler(this.comboBox_Vessel_SelectedIndexChanged);
            // 
            // radioButton_Day1
            // 
            this.radioButton_Day1.AutoSize = true;
            this.radioButton_Day1.Location = new System.Drawing.Point(52, 56);
            this.radioButton_Day1.Name = "radioButton_Day1";
            this.radioButton_Day1.Size = new System.Drawing.Size(55, 16);
            this.radioButton_Day1.TabIndex = 7;
            this.radioButton_Day1.TabStop = true;
            this.radioButton_Day1.Text = "１日目";
            this.radioButton_Day1.UseVisualStyleBackColor = true;
            this.radioButton_Day1.CheckedChanged += new System.EventHandler(this.radioButton_Day1_CheckedChanged);
            // 
            // radioButton_Day2
            // 
            this.radioButton_Day2.AutoSize = true;
            this.radioButton_Day2.Location = new System.Drawing.Point(111, 56);
            this.radioButton_Day2.Name = "radioButton_Day2";
            this.radioButton_Day2.Size = new System.Drawing.Size(55, 16);
            this.radioButton_Day2.TabIndex = 7;
            this.radioButton_Day2.TabStop = true;
            this.radioButton_Day2.Text = "２日目";
            this.radioButton_Day2.UseVisualStyleBackColor = true;
            this.radioButton_Day2.CheckedChanged += new System.EventHandler(this.radioButton_Day2_CheckedChanged);
            // 
            // radioButton_Day3
            // 
            this.radioButton_Day3.AutoSize = true;
            this.radioButton_Day3.Location = new System.Drawing.Point(170, 56);
            this.radioButton_Day3.Name = "radioButton_Day3";
            this.radioButton_Day3.Size = new System.Drawing.Size(55, 16);
            this.radioButton_Day3.TabIndex = 7;
            this.radioButton_Day3.TabStop = true;
            this.radioButton_Day3.Text = "３日目";
            this.radioButton_Day3.UseVisualStyleBackColor = true;
            this.radioButton_Day3.CheckedChanged += new System.EventHandler(this.radioButton_Day3_CheckedChanged);
            // 
            // radioButton_Day4
            // 
            this.radioButton_Day4.AutoSize = true;
            this.radioButton_Day4.Location = new System.Drawing.Point(229, 56);
            this.radioButton_Day4.Name = "radioButton_Day4";
            this.radioButton_Day4.Size = new System.Drawing.Size(55, 16);
            this.radioButton_Day4.TabIndex = 7;
            this.radioButton_Day4.TabStop = true;
            this.radioButton_Day4.Text = "４日目";
            this.radioButton_Day4.UseVisualStyleBackColor = true;
            this.radioButton_Day4.CheckedChanged += new System.EventHandler(this.radioButton_Day4_CheckedChanged);
            // 
            // 労働パターンForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1609, 841);
            this.Controls.Add(this.radioButton_Day4);
            this.Controls.Add(this.radioButton_Day3);
            this.Controls.Add(this.radioButton_Day2);
            this.Controls.Add(this.radioButton_Day1);
            this.Controls.Add(this.comboBox_Vessel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.gcCalendarGrid1);
            this.Name = "労働パターンForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "作業パターン";
            this.Activated += new System.EventHandler(this.労働パターンForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.労働パターンForm_FormClosed);
            this.Load += new System.EventHandler(this.労働パターンForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcCalendarGrid1)).EndInit();
            this.gcCalendarGrid1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GrapeCity.Win.CalendarGrid.GcCalendarGrid gcCalendarGrid1;
        private GrapeCity.Win.CalendarGrid.CalendarTitleCaption calendarTitleCaption1;
        private GrapeCity.Win.CalendarGrid.CalendarTitleButton calendarTitleButton1;
        private GrapeCity.Win.CalendarGrid.CalendarTitleButton calendarTitleButton2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Vessel;
        private System.Windows.Forms.RadioButton radioButton_Day1;
        private System.Windows.Forms.RadioButton radioButton_Day2;
        private System.Windows.Forms.RadioButton radioButton_Day3;
        private System.Windows.Forms.RadioButton radioButton_Day4;
    }
}

