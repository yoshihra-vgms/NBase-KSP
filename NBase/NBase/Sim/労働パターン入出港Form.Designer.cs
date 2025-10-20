
namespace Sim
{
    partial class 労働パターン入出港Form
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
            GrapeCity.Win.CalendarGrid.CalendarListView calendarListView2 = new GrapeCity.Win.CalendarGrid.CalendarListView();
            GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyle calendarConditionalCellStyle2 = new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle5 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle6 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle7 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarCellStyle calendarCellStyle8 = new GrapeCity.Win.CalendarGrid.CalendarCellStyle();
            GrapeCity.Win.CalendarGrid.CalendarTemplate calendarTemplate2 = new GrapeCity.Win.CalendarGrid.CalendarTemplate();
            GrapeCity.Win.CalendarGrid.CalendarHeaderCellType calendarHeaderCellType1 = new GrapeCity.Win.CalendarGrid.CalendarHeaderCellType();
            GrapeCity.Win.CalendarGrid.CalendarHeaderCellType calendarHeaderCellType2 = new GrapeCity.Win.CalendarGrid.CalendarHeaderCellType();
            this.gcCalendarGrid1 = new GrapeCity.Win.CalendarGrid.GcCalendarGrid();
            this.calendarTitleCaption1 = new GrapeCity.Win.CalendarGrid.CalendarTitleCaption();
            this.calendarTitleButton1 = new GrapeCity.Win.CalendarGrid.CalendarTitleButton();
            this.calendarTitleButton2 = new GrapeCity.Win.CalendarGrid.CalendarTitleButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_Event = new System.Windows.Forms.ComboBox();
            this.comboBox_Basho = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gcCalendarGrid1)).BeginInit();
            this.gcCalendarGrid1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcCalendarGrid1
            // 
            this.gcCalendarGrid1.AllowDragPageScroll = false;
            this.gcCalendarGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            calendarListView2.DayCount = 1;
            this.gcCalendarGrid1.CalendarView = calendarListView2;
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
            this.gcCalendarGrid1.Location = new System.Drawing.Point(12, 131);
            this.gcCalendarGrid1.Name = "gcCalendarGrid1";
            this.gcCalendarGrid1.Protected = true;
            this.gcCalendarGrid1.Size = new System.Drawing.Size(895, 455);
            calendarCellStyle5.ForeColor = System.Drawing.Color.Red;
            calendarCellStyle6.ForeColor = System.Drawing.Color.Blue;
            calendarCellStyle7.ForeColor = System.Drawing.Color.Red;
            calendarCellStyle8.BackColor = System.Drawing.Color.WhiteSmoke;
            calendarCellStyle8.ForeColor = System.Drawing.Color.Gray;
            calendarConditionalCellStyle2.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle5, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsSunday));
            calendarConditionalCellStyle2.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle6, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsSaturday));
            calendarConditionalCellStyle2.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle7, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsHoliday));
            calendarConditionalCellStyle2.Items.Add(new GrapeCity.Win.CalendarGrid.CalendarConditionalCellStyleItem(calendarCellStyle8, GrapeCity.Win.CalendarGrid.ConditionalStyleOperator.IsTrailingDay));
            calendarConditionalCellStyle2.Name = "defaultStyle";
            this.gcCalendarGrid1.Styles.Add(calendarConditionalCellStyle2);
            this.gcCalendarGrid1.TabIndex = 0;
            calendarTemplate2.ColumnCount = 0;
            calendarTemplate2.ColumnHeaderRowCount = 2;
            calendarTemplate2.RowCount = 0;
            calendarHeaderCellType1.SupportLocalization = true;
            calendarTemplate2.CornerHeader.GetCell(0, 0).CellType = calendarHeaderCellType1;
            calendarTemplate2.CornerHeader.GetCell(0, 0).RowSpan = 2;
            calendarTemplate2.CornerHeader.GetCell(0, 0).ColumnSpan = 1;
            calendarHeaderCellType2.SupportLocalization = true;
            calendarTemplate2.CornerHeader.GetCell(1, 0).CellType = calendarHeaderCellType2;
            calendarTemplate2.ColumnHeader.CellStyleName = "defaultStyle";
            calendarTemplate2.Content.CellStyleName = "defaultStyle";
            this.gcCalendarGrid1.Template = calendarTemplate2;
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(232, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(675, 113);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "イベント：";
            // 
            // comboBox_Event
            // 
            this.comboBox_Event.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Event.FormattingEnabled = true;
            this.comboBox_Event.Location = new System.Drawing.Point(76, 19);
            this.comboBox_Event.Name = "comboBox_Event";
            this.comboBox_Event.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Event.TabIndex = 6;
            this.comboBox_Event.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // comboBox_Basho
            // 
            this.comboBox_Basho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Basho.FormattingEnabled = true;
            this.comboBox_Basho.Location = new System.Drawing.Point(76, 58);
            this.comboBox_Basho.Name = "comboBox_Basho";
            this.comboBox_Basho.Size = new System.Drawing.Size(121, 20);
            this.comboBox_Basho.TabIndex = 7;
            this.comboBox_Basho.SelectedValueChanged += new System.EventHandler(this.comboBox_Basho_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "港：";
            // 
            // 労働パターン入出港Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 598);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_Basho);
            this.Controls.Add(this.comboBox_Event);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.gcCalendarGrid1);
            this.Name = "労働パターン入出港Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "労働パターン";
            this.Activated += new System.EventHandler(this.労働パターン入出港Form_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.労働パターン入出港Form_FormClosed);
            this.Load += new System.EventHandler(this.労働パターン入出港Form_Load);
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
        private System.Windows.Forms.ComboBox comboBox_Event;
        private System.Windows.Forms.ComboBox comboBox_Basho;
        private System.Windows.Forms.Label label2;
    }
}

