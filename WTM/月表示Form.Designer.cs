
namespace WTM
{
    partial class 月表示Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(月表示Form));
            this.gcCalendarGrid1 = new GrapeCity.Win.CalendarGrid.GcCalendarGrid();
            this.calendarTitleCaption1 = new GrapeCity.Win.CalendarGrid.CalendarTitleCaption();
            this.buttonSerach = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.monthPicker1 = new NMDUI.MonthPicker();
            this.button_NextDay = new System.Windows.Forms.Button();
            this.button_PrevDay = new System.Windows.Forms.Button();
            this.panel_SearchMessage = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonApproval = new System.Windows.Forms.Button();
            this.groupBoxRankCategory = new System.Windows.Forms.GroupBox();
            this.comboBoxRankCategory = new System.Windows.Forms.ComboBox();
            this.buttonOutput = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel_Approve = new System.Windows.Forms.Panel();
            this.textBox_ApprovePassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_ApproveErrorMessage = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonCancelApprove = new System.Windows.Forms.Button();
            this.buttonExecApprove = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gcCalendarGrid1)).BeginInit();
            this.gcCalendarGrid1.SuspendLayout();
            this.panel_SearchMessage.SuspendLayout();
            this.groupBoxRankCategory.SuspendLayout();
            this.panel_Approve.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.gcCalendarGrid1.Location = new System.Drawing.Point(12, 169);
            this.gcCalendarGrid1.Name = "gcCalendarGrid1";
            this.gcCalendarGrid1.Size = new System.Drawing.Size(1100, 480);
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
            calendarTemplate1.RowHeaderColumnCount = 0;
            calendarTemplate1.ColumnHeader.CellStyleName = "defaultStyle";
            calendarTemplate1.Content.CellStyleName = "defaultStyle";
            this.gcCalendarGrid1.Template = calendarTemplate1;
            this.gcCalendarGrid1.TitleHeader.Children.Add(this.calendarTitleCaption1);
            this.gcCalendarGrid1.CellMouseClick += new System.EventHandler<GrapeCity.Win.CalendarGrid.CalendarCellMouseEventArgs>(this.gcCalendarGrid1_CellMouseClick);
            this.gcCalendarGrid1.AppointmentCellDragging += new System.EventHandler<GrapeCity.Win.CalendarGrid.AppointmentCellDraggingEventArgs>(this.gcCalendarGrid1_AppointmentCellDragging);
            // 
            // calendarTitleCaption1
            // 
            this.calendarTitleCaption1.DateFormat = "yyyy/MM";
            this.calendarTitleCaption1.DateFormatType = GrapeCity.Win.CalendarGrid.CalendarDateFormatType.DotNet;
            this.calendarTitleCaption1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.calendarTitleCaption1.Name = "calendarTitleCaption1";
            this.calendarTitleCaption1.Text = "{0}";
            // 
            // buttonSerach
            // 
            this.buttonSerach.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonSerach.Location = new System.Drawing.Point(234, 89);
            this.buttonSerach.Name = "buttonSerach";
            this.buttonSerach.Size = new System.Drawing.Size(75, 23);
            this.buttonSerach.TabIndex = 3;
            this.buttonSerach.Text = "検索";
            this.buttonSerach.UseVisualStyleBackColor = true;
            this.buttonSerach.Visible = false;
            this.buttonSerach.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "船：";
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(59, 23);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(171, 20);
            this.comboBox船.TabIndex = 6;
            this.comboBox船.SelectedIndexChanged += new System.EventHandler(this.comboBox船_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(882, 11);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(241, 148);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "年月：";
            // 
            // monthPicker1
            // 
            this.monthPicker1.CustomFormat = "yyyy/MM";
            this.monthPicker1.Location = new System.Drawing.Point(59, 53);
            this.monthPicker1.Name = "monthPicker1";
            this.monthPicker1.Size = new System.Drawing.Size(171, 19);
            this.monthPicker1.TabIndex = 12;
            this.monthPicker1.CloseUp += new System.EventHandler(this.monthPicker1_CloseUp);
            // 
            // button_NextDay
            // 
            this.button_NextDay.BackColor = System.Drawing.Color.Transparent;
            this.button_NextDay.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button_NextDay.FlatAppearance.BorderSize = 0;
            this.button_NextDay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button_NextDay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.button_NextDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_NextDay.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_NextDay.Location = new System.Drawing.Point(273, 48);
            this.button_NextDay.Margin = new System.Windows.Forms.Padding(0);
            this.button_NextDay.Name = "button_NextDay";
            this.button_NextDay.Size = new System.Drawing.Size(24, 25);
            this.button_NextDay.TabIndex = 13;
            this.button_NextDay.Text = "▶";
            this.button_NextDay.UseVisualStyleBackColor = false;
            this.button_NextDay.Click += new System.EventHandler(this.button_NextDay_Click);
            // 
            // button_PrevDay
            // 
            this.button_PrevDay.BackColor = System.Drawing.Color.Transparent;
            this.button_PrevDay.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button_PrevDay.FlatAppearance.BorderSize = 0;
            this.button_PrevDay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.button_PrevDay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.button_PrevDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_PrevDay.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_PrevDay.Location = new System.Drawing.Point(249, 48);
            this.button_PrevDay.Margin = new System.Windows.Forms.Padding(0);
            this.button_PrevDay.Name = "button_PrevDay";
            this.button_PrevDay.Size = new System.Drawing.Size(24, 25);
            this.button_PrevDay.TabIndex = 14;
            this.button_PrevDay.Text = "◀";
            this.button_PrevDay.UseVisualStyleBackColor = false;
            this.button_PrevDay.Click += new System.EventHandler(this.button_PrevDay_Click);
            // 
            // panel_SearchMessage
            // 
            this.panel_SearchMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_SearchMessage.Controls.Add(this.label7);
            this.panel_SearchMessage.Location = new System.Drawing.Point(593, 339);
            this.panel_SearchMessage.Name = "panel_SearchMessage";
            this.panel_SearchMessage.Size = new System.Drawing.Size(432, 139);
            this.panel_SearchMessage.TabIndex = 15;
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
            // buttonApproval
            // 
            this.buttonApproval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApproval.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonApproval.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonApproval.Location = new System.Drawing.Point(730, 106);
            this.buttonApproval.Name = "buttonApproval";
            this.buttonApproval.Size = new System.Drawing.Size(79, 40);
            this.buttonApproval.TabIndex = 3;
            this.buttonApproval.Text = "承認";
            this.buttonApproval.UseVisualStyleBackColor = true;
            this.buttonApproval.Visible = false;
            this.buttonApproval.Click += new System.EventHandler(this.buttonApproval_Click);
            // 
            // groupBoxRankCategory
            // 
            this.groupBoxRankCategory.Controls.Add(this.comboBoxRankCategory);
            this.groupBoxRankCategory.Location = new System.Drawing.Point(339, 33);
            this.groupBoxRankCategory.Name = "groupBoxRankCategory";
            this.groupBoxRankCategory.Size = new System.Drawing.Size(168, 40);
            this.groupBoxRankCategory.TabIndex = 16;
            this.groupBoxRankCategory.TabStop = false;
            this.groupBoxRankCategory.Text = "職位フィルタ";
            // 
            // comboBoxRankCategory
            // 
            this.comboBoxRankCategory.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxRankCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRankCategory.FormattingEnabled = true;
            this.comboBoxRankCategory.Location = new System.Drawing.Point(27, 14);
            this.comboBoxRankCategory.Name = "comboBoxRankCategory";
            this.comboBoxRankCategory.Size = new System.Drawing.Size(121, 20);
            this.comboBoxRankCategory.TabIndex = 0;
            this.comboBoxRankCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxRankCategory_SelectedIndexChanged);
            // 
            // buttonOutput
            // 
            this.buttonOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOutput.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonOutput.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonOutput.Image = ((System.Drawing.Image)(resources.GetObject("buttonOutput.Image")));
            this.buttonOutput.Location = new System.Drawing.Point(551, 106);
            this.buttonOutput.Name = "buttonOutput";
            this.buttonOutput.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.buttonOutput.Size = new System.Drawing.Size(173, 40);
            this.buttonOutput.TabIndex = 3;
            this.buttonOutput.Text = "　労務管理記録簿出力";
            this.buttonOutput.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonOutput.UseVisualStyleBackColor = true;
            this.buttonOutput.Click += new System.EventHandler(this.buttonOutput_Click);
            // 
            // panel_Approve
            // 
            this.panel_Approve.BackColor = System.Drawing.Color.White;
            this.panel_Approve.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Approve.Controls.Add(this.textBox_ApprovePassword);
            this.panel_Approve.Controls.Add(this.label1);
            this.panel_Approve.Controls.Add(this.label_ApproveErrorMessage);
            this.panel_Approve.Controls.Add(this.label8);
            this.panel_Approve.Controls.Add(this.buttonCancelApprove);
            this.panel_Approve.Controls.Add(this.buttonExecApprove);
            this.panel_Approve.Location = new System.Drawing.Point(613, 328);
            this.panel_Approve.Name = "panel_Approve";
            this.panel_Approve.Size = new System.Drawing.Size(392, 161);
            this.panel_Approve.TabIndex = 17;
            // 
            // textBox_ApprovePassword
            // 
            this.textBox_ApprovePassword.Location = new System.Drawing.Point(60, 68);
            this.textBox_ApprovePassword.Name = "textBox_ApprovePassword";
            this.textBox_ApprovePassword.Size = new System.Drawing.Size(153, 19);
            this.textBox_ApprovePassword.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(57, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "管理者ログイン";
            // 
            // label_ApproveErrorMessage
            // 
            this.label_ApproveErrorMessage.AutoSize = true;
            this.label_ApproveErrorMessage.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_ApproveErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.label_ApproveErrorMessage.Location = new System.Drawing.Point(219, 72);
            this.label_ApproveErrorMessage.Name = "label_ApproveErrorMessage";
            this.label_ApproveErrorMessage.Size = new System.Drawing.Size(132, 13);
            this.label_ApproveErrorMessage.TabIndex = 11;
            this.label_ApproveErrorMessage.Text = "パスワードが違っています";
            this.label_ApproveErrorMessage.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.Location = new System.Drawing.Point(24, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 19);
            this.label8.TabIndex = 11;
            this.label8.Text = "承認";
            // 
            // buttonCancelApprove
            // 
            this.buttonCancelApprove.BackColor = System.Drawing.Color.Transparent;
            this.buttonCancelApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancelApprove.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonCancelApprove.ForeColor = System.Drawing.Color.Black;
            this.buttonCancelApprove.Location = new System.Drawing.Point(202, 114);
            this.buttonCancelApprove.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonCancelApprove.Name = "buttonCancelApprove";
            this.buttonCancelApprove.Size = new System.Drawing.Size(117, 31);
            this.buttonCancelApprove.TabIndex = 10;
            this.buttonCancelApprove.Text = "キャンセル";
            this.buttonCancelApprove.UseVisualStyleBackColor = false;
            this.buttonCancelApprove.Click += new System.EventHandler(this.buttonCancelApprove_Click);
            // 
            // buttonExecApprove
            // 
            this.buttonExecApprove.BackColor = System.Drawing.Color.Blue;
            this.buttonExecApprove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonExecApprove.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonExecApprove.ForeColor = System.Drawing.Color.White;
            this.buttonExecApprove.Location = new System.Drawing.Point(71, 114);
            this.buttonExecApprove.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonExecApprove.Name = "buttonExecApprove";
            this.buttonExecApprove.Size = new System.Drawing.Size(117, 31);
            this.buttonExecApprove.TabIndex = 10;
            this.buttonExecApprove.Text = "承認";
            this.buttonExecApprove.UseVisualStyleBackColor = false;
            this.buttonExecApprove.Click += new System.EventHandler(this.buttonExecApprove_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.buttonSerach);
            this.panel1.Controls.Add(this.groupBoxRankCategory);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.comboBox船);
            this.panel1.Controls.Add(this.buttonOutput);
            this.panel1.Controls.Add(this.buttonApproval);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button_NextDay);
            this.panel1.Controls.Add(this.monthPicker1);
            this.panel1.Controls.Add(this.button_PrevDay);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1133, 162);
            this.panel1.TabIndex = 18;
            // 
            // 月表示Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1134, 661);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel_Approve);
            this.Controls.Add(this.panel_SearchMessage);
            this.Controls.Add(this.gcCalendarGrid1);
            this.MinimumSize = new System.Drawing.Size(1150, 700);
            this.Name = "月表示Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "月表示";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.月表示Form_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.月表示Form_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.gcCalendarGrid1)).EndInit();
            this.gcCalendarGrid1.ResumeLayout(false);
            this.panel_SearchMessage.ResumeLayout(false);
            this.panel_SearchMessage.PerformLayout();
            this.groupBoxRankCategory.ResumeLayout(false);
            this.panel_Approve.ResumeLayout(false);
            this.panel_Approve.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GrapeCity.Win.CalendarGrid.GcCalendarGrid gcCalendarGrid1;
        private System.Windows.Forms.Button buttonSerach;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private NMDUI.MonthPicker monthPicker1;
        private System.Windows.Forms.Label label2;
        private GrapeCity.Win.CalendarGrid.CalendarTitleCaption calendarTitleCaption1;
        private System.Windows.Forms.Button button_NextDay;
        private System.Windows.Forms.Button button_PrevDay;
        private System.Windows.Forms.Panel panel_SearchMessage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonApproval;
        private System.Windows.Forms.GroupBox groupBoxRankCategory;
        private System.Windows.Forms.ComboBox comboBoxRankCategory;
        private System.Windows.Forms.Button buttonOutput;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panel_Approve;
        private System.Windows.Forms.TextBox textBox_ApprovePassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_ApproveErrorMessage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonCancelApprove;
        private System.Windows.Forms.Button buttonExecApprove;
        private System.Windows.Forms.Panel panel1;
    }
}

