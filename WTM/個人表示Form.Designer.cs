
namespace WTM
{
    partial class 個人表示Form
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
            this.gcCalendarGrid1 = new GrapeCity.Win.CalendarGrid.GcCalendarGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSerach = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox船員 = new System.Windows.Forms.ComboBox();
            this.label船 = new System.Windows.Forms.Label();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.button_NextMonth = new System.Windows.Forms.Button();
            this.button_PrevMonth = new System.Windows.Forms.Button();
            this.panel_SearchMessage = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox列表示 = new System.Windows.Forms.GroupBox();
            this.checkBox時間列7 = new System.Windows.Forms.CheckBox();
            this.checkBox時間列6 = new System.Windows.Forms.CheckBox();
            this.checkBox時間列5 = new System.Windows.Forms.CheckBox();
            this.checkBox時間列4 = new System.Windows.Forms.CheckBox();
            this.checkBox時間列3 = new System.Windows.Forms.CheckBox();
            this.checkBox時間列2 = new System.Windows.Forms.CheckBox();
            this.checkBox時間列1 = new System.Windows.Forms.CheckBox();
            this.panel_VesselSelecter = new System.Windows.Forms.Panel();
            this.panel_SearchBox = new System.Windows.Forms.Panel();
            this.monthPicker1 = new NMDUI.MonthPicker();
            this.button月表示 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gcCalendarGrid1)).BeginInit();
            this.gcCalendarGrid1.SuspendLayout();
            this.panel_SearchMessage.SuspendLayout();
            this.groupBox列表示.SuspendLayout();
            this.panel_VesselSelecter.SuspendLayout();
            this.panel_SearchBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcCalendarGrid1
            // 
            this.gcCalendarGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcCalendarGrid1.AutoGenerateCellType = true;
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
            this.gcCalendarGrid1.Location = new System.Drawing.Point(12, 183);
            this.gcCalendarGrid1.Name = "gcCalendarGrid1";
            this.gcCalendarGrid1.Size = new System.Drawing.Size(1100, 466);
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
            this.gcCalendarGrid1.TabIndex = 3;
            calendarTemplate1.ColumnCount = 0;
            calendarTemplate1.RowHeaderColumnCount = 0;
            calendarTemplate1.ColumnHeader.CellStyleName = "defaultStyle";
            calendarTemplate1.Content.CellStyleName = "defaultStyle";
            this.gcCalendarGrid1.Template = calendarTemplate1;
            this.gcCalendarGrid1.CellMouseClick += new System.EventHandler<GrapeCity.Win.CalendarGrid.CalendarCellMouseEventArgs>(this.gcCalendarGrid1_CellMouseClick);
            this.gcCalendarGrid1.AppointmentCellDragging += new System.EventHandler<GrapeCity.Win.CalendarGrid.AppointmentCellDraggingEventArgs>(this.gcCalendarGrid1_AppointmentCellDragging);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "年月：";
            // 
            // buttonSerach
            // 
            this.buttonSerach.Location = new System.Drawing.Point(294, 5);
            this.buttonSerach.Name = "buttonSerach";
            this.buttonSerach.Size = new System.Drawing.Size(75, 23);
            this.buttonSerach.TabIndex = 3;
            this.buttonSerach.Text = "検索";
            this.buttonSerach.UseVisualStyleBackColor = true;
            this.buttonSerach.Visible = false;
            this.buttonSerach.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "船員：";
            // 
            // comboBox船員
            // 
            this.comboBox船員.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船員.FormattingEnabled = true;
            this.comboBox船員.Location = new System.Drawing.Point(48, 7);
            this.comboBox船員.Name = "comboBox船員";
            this.comboBox船員.Size = new System.Drawing.Size(171, 20);
            this.comboBox船員.TabIndex = 1;
            this.comboBox船員.SelectedIndexChanged += new System.EventHandler(this.comboBox船員_SelectedIndexChanged);
            // 
            // label船
            // 
            this.label船.AutoSize = true;
            this.label船.Location = new System.Drawing.Point(19, 8);
            this.label船.Name = "label船";
            this.label船.Size = new System.Drawing.Size(23, 12);
            this.label船.TabIndex = 0;
            this.label船.Text = "船：";
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(48, 5);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(171, 20);
            this.comboBox船.TabIndex = 1;
            this.comboBox船.SelectedIndexChanged += new System.EventHandler(this.comboBox船_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(425, 27);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(443, 91);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(871, 27);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(241, 117);
            this.flowLayoutPanel2.TabIndex = 7;
            // 
            // button_NextMonth
            // 
            this.button_NextMonth.BackColor = System.Drawing.Color.Transparent;
            this.button_NextMonth.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button_NextMonth.FlatAppearance.BorderSize = 0;
            this.button_NextMonth.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button_NextMonth.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.button_NextMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_NextMonth.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_NextMonth.Location = new System.Drawing.Point(256, 40);
            this.button_NextMonth.Margin = new System.Windows.Forms.Padding(0);
            this.button_NextMonth.Name = "button_NextMonth";
            this.button_NextMonth.Size = new System.Drawing.Size(24, 25);
            this.button_NextMonth.TabIndex = 5;
            this.button_NextMonth.Text = "▶";
            this.button_NextMonth.UseVisualStyleBackColor = false;
            this.button_NextMonth.Click += new System.EventHandler(this.button_NextMonth_Click);
            // 
            // button_PrevMonth
            // 
            this.button_PrevMonth.BackColor = System.Drawing.Color.Transparent;
            this.button_PrevMonth.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button_PrevMonth.FlatAppearance.BorderSize = 0;
            this.button_PrevMonth.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button_PrevMonth.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.button_PrevMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_PrevMonth.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_PrevMonth.Location = new System.Drawing.Point(232, 40);
            this.button_PrevMonth.Margin = new System.Windows.Forms.Padding(0);
            this.button_PrevMonth.Name = "button_PrevMonth";
            this.button_PrevMonth.Size = new System.Drawing.Size(24, 25);
            this.button_PrevMonth.TabIndex = 4;
            this.button_PrevMonth.Text = "◀";
            this.button_PrevMonth.UseVisualStyleBackColor = false;
            this.button_PrevMonth.Click += new System.EventHandler(this.button_PrevMonth_Click);
            // 
            // panel_SearchMessage
            // 
            this.panel_SearchMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_SearchMessage.Controls.Add(this.label7);
            this.panel_SearchMessage.Location = new System.Drawing.Point(593, 339);
            this.panel_SearchMessage.Name = "panel_SearchMessage";
            this.panel_SearchMessage.Size = new System.Drawing.Size(432, 139);
            this.panel_SearchMessage.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(59, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "検索中・・・";
            // 
            // groupBox列表示
            // 
            this.groupBox列表示.Controls.Add(this.checkBox時間列7);
            this.groupBox列表示.Controls.Add(this.checkBox時間列6);
            this.groupBox列表示.Controls.Add(this.checkBox時間列5);
            this.groupBox列表示.Controls.Add(this.checkBox時間列4);
            this.groupBox列表示.Controls.Add(this.checkBox時間列3);
            this.groupBox列表示.Controls.Add(this.checkBox時間列2);
            this.groupBox列表示.Controls.Add(this.checkBox時間列1);
            this.groupBox列表示.Location = new System.Drawing.Point(12, 115);
            this.groupBox列表示.Name = "groupBox列表示";
            this.groupBox列表示.Size = new System.Drawing.Size(586, 62);
            this.groupBox列表示.TabIndex = 2;
            this.groupBox列表示.TabStop = false;
            this.groupBox列表示.Text = "列表示";
            // 
            // checkBox時間列7
            // 
            this.checkBox時間列7.AutoSize = true;
            this.checkBox時間列7.Checked = true;
            this.checkBox時間列7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox時間列7.Location = new System.Drawing.Point(475, 25);
            this.checkBox時間列7.Name = "checkBox時間列7";
            this.checkBox時間列7.Size = new System.Drawing.Size(102, 16);
            this.checkBox時間列7.TabIndex = 6;
            this.checkBox時間列7.Text = "休憩時間(長い)";
            this.checkBox時間列7.UseVisualStyleBackColor = true;
            this.checkBox時間列7.CheckedChanged += new System.EventHandler(this.checkBox時間列_CheckedChanged);
            // 
            // checkBox時間列6
            // 
            this.checkBox時間列6.AutoSize = true;
            this.checkBox時間列6.Checked = true;
            this.checkBox時間列6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox時間列6.Location = new System.Drawing.Point(397, 25);
            this.checkBox時間列6.Name = "checkBox時間列6";
            this.checkBox時間列6.Size = new System.Drawing.Size(72, 16);
            this.checkBox時間列6.TabIndex = 5;
            this.checkBox時間列6.Text = "休憩時間";
            this.checkBox時間列6.UseVisualStyleBackColor = true;
            this.checkBox時間列6.CheckedChanged += new System.EventHandler(this.checkBox時間列_CheckedChanged);
            // 
            // checkBox時間列5
            // 
            this.checkBox時間列5.AutoSize = true;
            this.checkBox時間列5.Checked = true;
            this.checkBox時間列5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox時間列5.Location = new System.Drawing.Point(283, 18);
            this.checkBox時間列5.Name = "checkBox時間列5";
            this.checkBox時間列5.Size = new System.Drawing.Size(108, 28);
            this.checkBox時間列5.TabIndex = 4;
            this.checkBox時間列5.Text = "4週間当たりの\r\n時間外労働時間";
            this.checkBox時間列5.UseVisualStyleBackColor = true;
            this.checkBox時間列5.CheckedChanged += new System.EventHandler(this.checkBox時間列_CheckedChanged);
            // 
            // checkBox時間列4
            // 
            this.checkBox時間列4.AutoSize = true;
            this.checkBox時間列4.Checked = true;
            this.checkBox時間列4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox時間列4.Location = new System.Drawing.Point(184, 18);
            this.checkBox時間列4.Name = "checkBox時間列4";
            this.checkBox時間列4.Size = new System.Drawing.Size(93, 28);
            this.checkBox時間列4.TabIndex = 3;
            this.checkBox時間列4.Text = "1週間当たりの\r\n労働時間";
            this.checkBox時間列4.UseVisualStyleBackColor = true;
            this.checkBox時間列4.CheckedChanged += new System.EventHandler(this.checkBox時間列_CheckedChanged);
            // 
            // checkBox時間列3
            // 
            this.checkBox時間列3.AutoSize = true;
            this.checkBox時間列3.Location = new System.Drawing.Point(583, 24);
            this.checkBox時間列3.Name = "checkBox時間列3";
            this.checkBox時間列3.Size = new System.Drawing.Size(96, 16);
            this.checkBox時間列3.TabIndex = 2;
            this.checkBox時間列3.Text = "手当対象時間";
            this.checkBox時間列3.UseVisualStyleBackColor = true;
            this.checkBox時間列3.Visible = false;
            this.checkBox時間列3.CheckedChanged += new System.EventHandler(this.checkBox時間列_CheckedChanged);
            // 
            // checkBox時間列2
            // 
            this.checkBox時間列2.AutoSize = true;
            this.checkBox時間列2.Checked = true;
            this.checkBox時間列2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox時間列2.Location = new System.Drawing.Point(102, 18);
            this.checkBox時間列2.Name = "checkBox時間列2";
            this.checkBox時間列2.Size = new System.Drawing.Size(72, 28);
            this.checkBox時間列2.TabIndex = 1;
            this.checkBox時間列2.Text = "時間外\r\n労働時間";
            this.checkBox時間列2.UseVisualStyleBackColor = true;
            this.checkBox時間列2.CheckedChanged += new System.EventHandler(this.checkBox時間列_CheckedChanged);
            // 
            // checkBox時間列1
            // 
            this.checkBox時間列1.AutoSize = true;
            this.checkBox時間列1.Checked = true;
            this.checkBox時間列1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox時間列1.Location = new System.Drawing.Point(17, 18);
            this.checkBox時間列1.Name = "checkBox時間列1";
            this.checkBox時間列1.Size = new System.Drawing.Size(72, 28);
            this.checkBox時間列1.TabIndex = 0;
            this.checkBox時間列1.Text = "1日当りの\r\n労働時間";
            this.checkBox時間列1.UseVisualStyleBackColor = true;
            this.checkBox時間列1.CheckedChanged += new System.EventHandler(this.checkBox時間列_CheckedChanged);
            // 
            // panel_VesselSelecter
            // 
            this.panel_VesselSelecter.Controls.Add(this.comboBox船);
            this.panel_VesselSelecter.Controls.Add(this.label船);
            this.panel_VesselSelecter.Location = new System.Drawing.Point(12, 9);
            this.panel_VesselSelecter.Name = "panel_VesselSelecter";
            this.panel_VesselSelecter.Size = new System.Drawing.Size(293, 28);
            this.panel_VesselSelecter.TabIndex = 0;
            // 
            // panel_SearchBox
            // 
            this.panel_SearchBox.Controls.Add(this.comboBox船員);
            this.panel_SearchBox.Controls.Add(this.label1);
            this.panel_SearchBox.Controls.Add(this.label2);
            this.panel_SearchBox.Controls.Add(this.monthPicker1);
            this.panel_SearchBox.Controls.Add(this.button_NextMonth);
            this.panel_SearchBox.Controls.Add(this.button_PrevMonth);
            this.panel_SearchBox.Controls.Add(this.buttonSerach);
            this.panel_SearchBox.Location = new System.Drawing.Point(12, 43);
            this.panel_SearchBox.Name = "panel_SearchBox";
            this.panel_SearchBox.Size = new System.Drawing.Size(378, 66);
            this.panel_SearchBox.TabIndex = 1;
            // 
            // monthPicker1
            // 
            this.monthPicker1.CustomFormat = "yyyy/MM";
            this.monthPicker1.Location = new System.Drawing.Point(48, 45);
            this.monthPicker1.Name = "monthPicker1";
            this.monthPicker1.Size = new System.Drawing.Size(171, 19);
            this.monthPicker1.TabIndex = 2;
            this.monthPicker1.ValueChanged += new System.EventHandler(this.monthPicker1_ValueChanged);
            // 
            // button月表示
            // 
            this.button月表示.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button月表示.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button月表示.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button月表示.Location = new System.Drawing.Point(991, 149);
            this.button月表示.Name = "button月表示";
            this.button月表示.Size = new System.Drawing.Size(104, 28);
            this.button月表示.TabIndex = 4;
            this.button月表示.Text = "月表示";
            this.button月表示.UseVisualStyleBackColor = true;
            this.button月表示.Click += new System.EventHandler(this.button月表示_Click);
            // 
            // 個人表示Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1134, 661);
            this.Controls.Add(this.button月表示);
            this.Controls.Add(this.panel_SearchBox);
            this.Controls.Add(this.panel_VesselSelecter);
            this.Controls.Add(this.groupBox列表示);
            this.Controls.Add(this.panel_SearchMessage);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.gcCalendarGrid1);
            this.MinimumSize = new System.Drawing.Size(1150, 700);
            this.Name = "個人表示Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "個人表示";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.個人表示Form_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.個人表示Form_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.gcCalendarGrid1)).EndInit();
            this.gcCalendarGrid1.ResumeLayout(false);
            this.panel_SearchMessage.ResumeLayout(false);
            this.panel_SearchMessage.PerformLayout();
            this.groupBox列表示.ResumeLayout(false);
            this.groupBox列表示.PerformLayout();
            this.panel_VesselSelecter.ResumeLayout(false);
            this.panel_VesselSelecter.PerformLayout();
            this.panel_SearchBox.ResumeLayout(false);
            this.panel_SearchBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GrapeCity.Win.CalendarGrid.GcCalendarGrid gcCalendarGrid1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSerach;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox船員;
        private System.Windows.Forms.Label label船;
        private System.Windows.Forms.ComboBox comboBox船;
        private NMDUI.MonthPicker monthPicker1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button button_NextMonth;
        private System.Windows.Forms.Button button_PrevMonth;
        private System.Windows.Forms.Panel panel_SearchMessage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox列表示;
        private System.Windows.Forms.CheckBox checkBox時間列7;
        private System.Windows.Forms.CheckBox checkBox時間列6;
        private System.Windows.Forms.CheckBox checkBox時間列4;
        private System.Windows.Forms.CheckBox checkBox時間列3;
        private System.Windows.Forms.CheckBox checkBox時間列2;
        private System.Windows.Forms.CheckBox checkBox時間列1;
        private System.Windows.Forms.Panel panel_VesselSelecter;
        private System.Windows.Forms.Panel panel_SearchBox;
        private System.Windows.Forms.Button button月表示;
        private System.Windows.Forms.CheckBox checkBox時間列5;
    }
}

