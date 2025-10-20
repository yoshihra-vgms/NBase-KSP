namespace DeficiencyControl.Accident
{
    partial class AccidentDetailControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxAccidentReportNo = new System.Windows.Forms.TextBox();
            this.groupBoxAccident = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonAttachmentAccident = new System.Windows.Forms.Button();
            this.fileViewControlExAccident = new DeficiencyControl.Util.FileViewControlEx();
            this.textBoxAccident = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxSpotReport = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAttachmentSpotReport = new System.Windows.Forms.Button();
            this.fileViewControlExSpotReport = new DeficiencyControl.Util.FileViewControlEx();
            this.textBoxSpotReport = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxAccidentImportance = new System.Windows.Forms.ComboBox();
            this.statusSelectControl1 = new DeficiencyControl.Controls.StatusSelectControl();
            this.groupBoxSurveyResults = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonAttachPreventiveAction = new System.Windows.Forms.Button();
            this.fileViewControlExPreventiveAction = new DeficiencyControl.Util.FileViewControlEx();
            this.textBoxPreventiveAction = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonAttachmentCauseOfAccident = new System.Windows.Forms.Button();
            this.fileViewControlExCauseOfAccident = new DeficiencyControl.Util.FileViewControlEx();
            this.textBoxCauseOfAccident = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxProgress = new System.Windows.Forms.GroupBox();
            this.buttonProgressDelete = new System.Windows.Forms.Button();
            this.flowLayoutPanelProgress = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonProgressAdd = new System.Windows.Forms.Button();
            this.groupBoxReports = new System.Windows.Forms.GroupBox();
            this.buttonReportsDelete = new System.Windows.Forms.Button();
            this.buttonReportsAdd = new System.Windows.Forms.Button();
            this.flowLayoutPanelReports = new System.Windows.Forms.FlowLayoutPanel();
            this.textBoxInfluence = new System.Windows.Forms.TextBox();
            this.textBoxRemarks = new System.Windows.Forms.TextBox();
            this.labelDescriptionControl1 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl5 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl2 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionLineControl1 = new DeficiencyControl.Controls.LabelDescriptionLineControl();
            this.labelDescriptionLineControl2 = new DeficiencyControl.Controls.LabelDescriptionLineControl();
            this.groupBoxAccident.SuspendLayout();
            this.groupBoxSpotReport.SuspendLayout();
            this.groupBoxSurveyResults.SuspendLayout();
            this.groupBoxProgress.SuspendLayout();
            this.groupBoxReports.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxAccidentReportNo
            // 
            this.textBoxAccidentReportNo.Location = new System.Drawing.Point(102, 13);
            this.textBoxAccidentReportNo.Name = "textBoxAccidentReportNo";
            this.textBoxAccidentReportNo.ReadOnly = true;
            this.textBoxAccidentReportNo.Size = new System.Drawing.Size(250, 23);
            this.textBoxAccidentReportNo.TabIndex = 0;
            // 
            // groupBoxAccident
            // 
            this.groupBoxAccident.Controls.Add(this.label8);
            this.groupBoxAccident.Controls.Add(this.buttonAttachmentAccident);
            this.groupBoxAccident.Controls.Add(this.fileViewControlExAccident);
            this.groupBoxAccident.Controls.Add(this.textBoxAccident);
            this.groupBoxAccident.Controls.Add(this.label9);
            this.groupBoxAccident.Location = new System.Drawing.Point(3, 60);
            this.groupBoxAccident.Name = "groupBoxAccident";
            this.groupBoxAccident.Size = new System.Drawing.Size(560, 287);
            this.groupBoxAccident.TabIndex = 3;
            this.groupBoxAccident.TabStop = false;
            this.groupBoxAccident.Text = "Accident (事故概要)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label8.Location = new System.Drawing.Point(15, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 287;
            this.label8.Text = "添付資料";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonAttachmentAccident
            // 
            this.buttonAttachmentAccident.Location = new System.Drawing.Point(9, 251);
            this.buttonAttachmentAccident.Name = "buttonAttachmentAccident";
            this.buttonAttachmentAccident.Size = new System.Drawing.Size(54, 23);
            this.buttonAttachmentAccident.TabIndex = 1;
            this.buttonAttachmentAccident.Tag = "0";
            this.buttonAttachmentAccident.Text = "...";
            this.buttonAttachmentAccident.UseVisualStyleBackColor = true;
            this.buttonAttachmentAccident.Click += new System.EventHandler(this.buttonAttachment_Click);
            // 
            // fileViewControlExAccident
            // 
            this.fileViewControlExAccident.AllowDrop = true;
            this.fileViewControlExAccident.EnableDelete = true;
            this.fileViewControlExAccident.EnableDragDrop = true;
            this.fileViewControlExAccident.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.fileViewControlExAccident.Location = new System.Drawing.Point(84, 154);
            this.fileViewControlExAccident.Name = "fileViewControlExAccident";
            this.fileViewControlExAccident.Size = new System.Drawing.Size(470, 120);
            this.fileViewControlExAccident.TabIndex = 2;
            this.fileViewControlExAccident.FileItemSelected += new DeficiencyControl.Util.FileViewControlEx.FileItemSelectDelegate(this.fileViewControlEx_FileItemSelected);
            // 
            // textBoxAccident
            // 
            this.textBoxAccident.Location = new System.Drawing.Point(84, 28);
            this.textBoxAccident.Multiline = true;
            this.textBoxAccident.Name = "textBoxAccident";
            this.textBoxAccident.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxAccident.Size = new System.Drawing.Size(470, 120);
            this.textBoxAccident.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label9.Location = new System.Drawing.Point(15, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 15);
            this.label9.TabIndex = 283;
            this.label9.Text = "概要";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBoxSpotReport
            // 
            this.groupBoxSpotReport.Controls.Add(this.label2);
            this.groupBoxSpotReport.Controls.Add(this.buttonAttachmentSpotReport);
            this.groupBoxSpotReport.Controls.Add(this.fileViewControlExSpotReport);
            this.groupBoxSpotReport.Controls.Add(this.textBoxSpotReport);
            this.groupBoxSpotReport.Controls.Add(this.label3);
            this.groupBoxSpotReport.Location = new System.Drawing.Point(587, 60);
            this.groupBoxSpotReport.Name = "groupBoxSpotReport";
            this.groupBoxSpotReport.Size = new System.Drawing.Size(560, 287);
            this.groupBoxSpotReport.TabIndex = 6;
            this.groupBoxSpotReport.TabStop = false;
            this.groupBoxSpotReport.Text = "Spot Report (現場報告)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label2.Location = new System.Drawing.Point(6, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 287;
            this.label2.Text = "添付資料";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonAttachmentSpotReport
            // 
            this.buttonAttachmentSpotReport.Location = new System.Drawing.Point(9, 251);
            this.buttonAttachmentSpotReport.Name = "buttonAttachmentSpotReport";
            this.buttonAttachmentSpotReport.Size = new System.Drawing.Size(54, 23);
            this.buttonAttachmentSpotReport.TabIndex = 1;
            this.buttonAttachmentSpotReport.Tag = "1";
            this.buttonAttachmentSpotReport.Text = "...";
            this.buttonAttachmentSpotReport.UseVisualStyleBackColor = true;
            this.buttonAttachmentSpotReport.Click += new System.EventHandler(this.buttonAttachment_Click);
            // 
            // fileViewControlExSpotReport
            // 
            this.fileViewControlExSpotReport.AllowDrop = true;
            this.fileViewControlExSpotReport.EnableDelete = true;
            this.fileViewControlExSpotReport.EnableDragDrop = true;
            this.fileViewControlExSpotReport.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.fileViewControlExSpotReport.Location = new System.Drawing.Point(84, 154);
            this.fileViewControlExSpotReport.Name = "fileViewControlExSpotReport";
            this.fileViewControlExSpotReport.Size = new System.Drawing.Size(470, 120);
            this.fileViewControlExSpotReport.TabIndex = 2;
            this.fileViewControlExSpotReport.FileItemSelected += new DeficiencyControl.Util.FileViewControlEx.FileItemSelectDelegate(this.fileViewControlEx_FileItemSelected);
            // 
            // textBoxSpotReport
            // 
            this.textBoxSpotReport.Location = new System.Drawing.Point(84, 28);
            this.textBoxSpotReport.Multiline = true;
            this.textBoxSpotReport.Name = "textBoxSpotReport";
            this.textBoxSpotReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSpotReport.Size = new System.Drawing.Size(470, 120);
            this.textBoxSpotReport.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label3.Location = new System.Drawing.Point(6, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 283;
            this.label3.Text = "報告内容";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comboBoxAccidentImportance
            // 
            this.comboBoxAccidentImportance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAccidentImportance.FormattingEnabled = true;
            this.comboBoxAccidentImportance.Location = new System.Drawing.Point(505, 13);
            this.comboBoxAccidentImportance.Name = "comboBoxAccidentImportance";
            this.comboBoxAccidentImportance.Size = new System.Drawing.Size(250, 24);
            this.comboBoxAccidentImportance.TabIndex = 1;
            // 
            // statusSelectControl1
            // 
            this.statusSelectControl1.AutoSize = true;
            this.statusSelectControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statusSelectControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusSelectControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.statusSelectControl1.Location = new System.Drawing.Point(886, 8);
            this.statusSelectControl1.Name = "statusSelectControl1";
            this.statusSelectControl1.Size = new System.Drawing.Size(237, 28);
            this.statusSelectControl1.TabIndex = 2;
            // 
            // groupBoxSurveyResults
            // 
            this.groupBoxSurveyResults.Controls.Add(this.label10);
            this.groupBoxSurveyResults.Controls.Add(this.buttonAttachPreventiveAction);
            this.groupBoxSurveyResults.Controls.Add(this.fileViewControlExPreventiveAction);
            this.groupBoxSurveyResults.Controls.Add(this.textBoxPreventiveAction);
            this.groupBoxSurveyResults.Controls.Add(this.label11);
            this.groupBoxSurveyResults.Controls.Add(this.label6);
            this.groupBoxSurveyResults.Controls.Add(this.buttonAttachmentCauseOfAccident);
            this.groupBoxSurveyResults.Controls.Add(this.fileViewControlExCauseOfAccident);
            this.groupBoxSurveyResults.Controls.Add(this.textBoxCauseOfAccident);
            this.groupBoxSurveyResults.Controls.Add(this.label7);
            this.groupBoxSurveyResults.Location = new System.Drawing.Point(587, 362);
            this.groupBoxSurveyResults.Name = "groupBoxSurveyResults";
            this.groupBoxSurveyResults.Size = new System.Drawing.Size(560, 563);
            this.groupBoxSurveyResults.TabIndex = 7;
            this.groupBoxSurveyResults.TabStop = false;
            this.groupBoxSurveyResults.Text = "Survey results (調査結果)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label10.Location = new System.Drawing.Point(6, 426);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 15);
            this.label10.TabIndex = 292;
            this.label10.Text = "添付資料";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonAttachPreventiveAction
            // 
            this.buttonAttachPreventiveAction.Location = new System.Drawing.Point(9, 523);
            this.buttonAttachPreventiveAction.Name = "buttonAttachPreventiveAction";
            this.buttonAttachPreventiveAction.Size = new System.Drawing.Size(54, 23);
            this.buttonAttachPreventiveAction.TabIndex = 4;
            this.buttonAttachPreventiveAction.Tag = "3";
            this.buttonAttachPreventiveAction.Text = "...";
            this.buttonAttachPreventiveAction.UseVisualStyleBackColor = true;
            this.buttonAttachPreventiveAction.Click += new System.EventHandler(this.buttonAttachment_Click);
            // 
            // fileViewControlExPreventiveAction
            // 
            this.fileViewControlExPreventiveAction.AllowDrop = true;
            this.fileViewControlExPreventiveAction.EnableDelete = true;
            this.fileViewControlExPreventiveAction.EnableDragDrop = true;
            this.fileViewControlExPreventiveAction.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.fileViewControlExPreventiveAction.Location = new System.Drawing.Point(84, 426);
            this.fileViewControlExPreventiveAction.Name = "fileViewControlExPreventiveAction";
            this.fileViewControlExPreventiveAction.Size = new System.Drawing.Size(470, 120);
            this.fileViewControlExPreventiveAction.TabIndex = 5;
            this.fileViewControlExPreventiveAction.FileItemSelected += new DeficiencyControl.Util.FileViewControlEx.FileItemSelectDelegate(this.fileViewControlEx_FileItemSelected);
            // 
            // textBoxPreventiveAction
            // 
            this.textBoxPreventiveAction.Location = new System.Drawing.Point(84, 300);
            this.textBoxPreventiveAction.Multiline = true;
            this.textBoxPreventiveAction.Name = "textBoxPreventiveAction";
            this.textBoxPreventiveAction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxPreventiveAction.Size = new System.Drawing.Size(470, 120);
            this.textBoxPreventiveAction.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label11.Location = new System.Drawing.Point(6, 303);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 30);
            this.label11.TabIndex = 288;
            this.label11.Text = "再発防止\r\n対策";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label6.Location = new System.Drawing.Point(6, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 287;
            this.label6.Text = "添付資料";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonAttachmentCauseOfAccident
            // 
            this.buttonAttachmentCauseOfAccident.Location = new System.Drawing.Point(9, 251);
            this.buttonAttachmentCauseOfAccident.Name = "buttonAttachmentCauseOfAccident";
            this.buttonAttachmentCauseOfAccident.Size = new System.Drawing.Size(54, 23);
            this.buttonAttachmentCauseOfAccident.TabIndex = 1;
            this.buttonAttachmentCauseOfAccident.Tag = "2";
            this.buttonAttachmentCauseOfAccident.Text = "...";
            this.buttonAttachmentCauseOfAccident.UseVisualStyleBackColor = true;
            this.buttonAttachmentCauseOfAccident.Click += new System.EventHandler(this.buttonAttachment_Click);
            // 
            // fileViewControlExCauseOfAccident
            // 
            this.fileViewControlExCauseOfAccident.AllowDrop = true;
            this.fileViewControlExCauseOfAccident.EnableDelete = true;
            this.fileViewControlExCauseOfAccident.EnableDragDrop = true;
            this.fileViewControlExCauseOfAccident.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.fileViewControlExCauseOfAccident.Location = new System.Drawing.Point(84, 154);
            this.fileViewControlExCauseOfAccident.Name = "fileViewControlExCauseOfAccident";
            this.fileViewControlExCauseOfAccident.Size = new System.Drawing.Size(470, 120);
            this.fileViewControlExCauseOfAccident.TabIndex = 2;
            this.fileViewControlExCauseOfAccident.FileItemSelected += new DeficiencyControl.Util.FileViewControlEx.FileItemSelectDelegate(this.fileViewControlEx_FileItemSelected);
            // 
            // textBoxCauseOfAccident
            // 
            this.textBoxCauseOfAccident.Location = new System.Drawing.Point(84, 28);
            this.textBoxCauseOfAccident.Multiline = true;
            this.textBoxCauseOfAccident.Name = "textBoxCauseOfAccident";
            this.textBoxCauseOfAccident.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxCauseOfAccident.Size = new System.Drawing.Size(470, 120);
            this.textBoxCauseOfAccident.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label7.Location = new System.Drawing.Point(6, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 15);
            this.label7.TabIndex = 283;
            this.label7.Text = "原因";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBoxProgress
            // 
            this.groupBoxProgress.Controls.Add(this.buttonProgressDelete);
            this.groupBoxProgress.Controls.Add(this.flowLayoutPanelProgress);
            this.groupBoxProgress.Controls.Add(this.buttonProgressAdd);
            this.groupBoxProgress.Location = new System.Drawing.Point(3, 362);
            this.groupBoxProgress.Name = "groupBoxProgress";
            this.groupBoxProgress.Size = new System.Drawing.Size(560, 563);
            this.groupBoxProgress.TabIndex = 4;
            this.groupBoxProgress.TabStop = false;
            this.groupBoxProgress.Text = "Progress (進捗状況)";
            // 
            // buttonProgressDelete
            // 
            this.buttonProgressDelete.Location = new System.Drawing.Point(90, 24);
            this.buttonProgressDelete.Name = "buttonProgressDelete";
            this.buttonProgressDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonProgressDelete.TabIndex = 1;
            this.buttonProgressDelete.Text = "Del";
            this.buttonProgressDelete.UseVisualStyleBackColor = true;
            this.buttonProgressDelete.Click += new System.EventHandler(this.buttonProgressDelete_Click);
            // 
            // flowLayoutPanelProgress
            // 
            this.flowLayoutPanelProgress.AutoScroll = true;
            this.flowLayoutPanelProgress.Location = new System.Drawing.Point(6, 51);
            this.flowLayoutPanelProgress.Name = "flowLayoutPanelProgress";
            this.flowLayoutPanelProgress.Size = new System.Drawing.Size(548, 506);
            this.flowLayoutPanelProgress.TabIndex = 2;
            // 
            // buttonProgressAdd
            // 
            this.buttonProgressAdd.Location = new System.Drawing.Point(9, 24);
            this.buttonProgressAdd.Name = "buttonProgressAdd";
            this.buttonProgressAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonProgressAdd.TabIndex = 0;
            this.buttonProgressAdd.Text = "Add";
            this.buttonProgressAdd.UseVisualStyleBackColor = true;
            this.buttonProgressAdd.Click += new System.EventHandler(this.buttonProgressAdd_Click);
            // 
            // groupBoxReports
            // 
            this.groupBoxReports.Controls.Add(this.buttonReportsDelete);
            this.groupBoxReports.Controls.Add(this.buttonReportsAdd);
            this.groupBoxReports.Controls.Add(this.flowLayoutPanelReports);
            this.groupBoxReports.Location = new System.Drawing.Point(3, 941);
            this.groupBoxReports.Name = "groupBoxReports";
            this.groupBoxReports.Size = new System.Drawing.Size(560, 292);
            this.groupBoxReports.TabIndex = 5;
            this.groupBoxReports.TabStop = false;
            this.groupBoxReports.Text = "Reports (報告書提出先)";
            // 
            // buttonReportsDelete
            // 
            this.buttonReportsDelete.Location = new System.Drawing.Point(90, 22);
            this.buttonReportsDelete.Name = "buttonReportsDelete";
            this.buttonReportsDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonReportsDelete.TabIndex = 1;
            this.buttonReportsDelete.Text = "Del";
            this.buttonReportsDelete.UseVisualStyleBackColor = true;
            this.buttonReportsDelete.Click += new System.EventHandler(this.buttonReportsDelete_Click);
            // 
            // buttonReportsAdd
            // 
            this.buttonReportsAdd.Location = new System.Drawing.Point(9, 22);
            this.buttonReportsAdd.Name = "buttonReportsAdd";
            this.buttonReportsAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonReportsAdd.TabIndex = 0;
            this.buttonReportsAdd.Text = "Add";
            this.buttonReportsAdd.UseVisualStyleBackColor = true;
            this.buttonReportsAdd.Click += new System.EventHandler(this.buttonReportsAdd_Click);
            // 
            // flowLayoutPanelReports
            // 
            this.flowLayoutPanelReports.AutoScroll = true;
            this.flowLayoutPanelReports.Location = new System.Drawing.Point(6, 51);
            this.flowLayoutPanelReports.Name = "flowLayoutPanelReports";
            this.flowLayoutPanelReports.Size = new System.Drawing.Size(548, 235);
            this.flowLayoutPanelReports.TabIndex = 2;
            // 
            // textBoxInfluence
            // 
            this.textBoxInfluence.Location = new System.Drawing.Point(596, 960);
            this.textBoxInfluence.Multiline = true;
            this.textBoxInfluence.Name = "textBoxInfluence";
            this.textBoxInfluence.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInfluence.Size = new System.Drawing.Size(545, 120);
            this.textBoxInfluence.TabIndex = 8;
            // 
            // textBoxRemarks
            // 
            this.textBoxRemarks.Location = new System.Drawing.Point(596, 1113);
            this.textBoxRemarks.Multiline = true;
            this.textBoxRemarks.Name = "textBoxRemarks";
            this.textBoxRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxRemarks.Size = new System.Drawing.Size(545, 120);
            this.textBoxRemarks.TabIndex = 9;
            // 
            // labelDescriptionControl1
            // 
            this.labelDescriptionControl1.AutoSize = true;
            this.labelDescriptionControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl1.DescriptionEnabled = true;
            this.labelDescriptionControl1.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl1.DescriptionText = "(報告書No.)";
            this.labelDescriptionControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl1.Location = new System.Drawing.Point(3, 3);
            this.labelDescriptionControl1.MainText = "Accident\r\nReoirt No.";
            this.labelDescriptionControl1.Name = "labelDescriptionControl1";
            this.labelDescriptionControl1.RequiredFlag = true;
            this.labelDescriptionControl1.Size = new System.Drawing.Size(99, 47);
            this.labelDescriptionControl1.TabIndex = 318;
            this.labelDescriptionControl1.TabStop = false;
            // 
            // labelDescriptionControl5
            // 
            this.labelDescriptionControl5.AutoSize = true;
            this.labelDescriptionControl5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl5.DescriptionEnabled = true;
            this.labelDescriptionControl5.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl5.DescriptionText = "(重要度)";
            this.labelDescriptionControl5.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl5.Location = new System.Drawing.Point(385, 8);
            this.labelDescriptionControl5.MainText = "Importance";
            this.labelDescriptionControl5.Name = "labelDescriptionControl5";
            this.labelDescriptionControl5.RequiredFlag = true;
            this.labelDescriptionControl5.Size = new System.Drawing.Size(100, 31);
            this.labelDescriptionControl5.TabIndex = 319;
            this.labelDescriptionControl5.TabStop = false;
            // 
            // labelDescriptionControl2
            // 
            this.labelDescriptionControl2.AutoSize = true;
            this.labelDescriptionControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl2.DescriptionEnabled = true;
            this.labelDescriptionControl2.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl2.DescriptionText = "(状態)";
            this.labelDescriptionControl2.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl2.Location = new System.Drawing.Point(780, 8);
            this.labelDescriptionControl2.MainText = "Status";
            this.labelDescriptionControl2.Name = "labelDescriptionControl2";
            this.labelDescriptionControl2.RequiredFlag = true;
            this.labelDescriptionControl2.Size = new System.Drawing.Size(69, 31);
            this.labelDescriptionControl2.TabIndex = 319;
            this.labelDescriptionControl2.TabStop = false;
            // 
            // labelDescriptionLineControl1
            // 
            this.labelDescriptionLineControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionLineControl1.DescriptionEnabled = true;
            this.labelDescriptionLineControl1.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionLineControl1.DescriptionText = "(運航への影響)";
            this.labelDescriptionLineControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionLineControl1.Location = new System.Drawing.Point(578, 937);
            this.labelDescriptionLineControl1.MainText = "Influence";
            this.labelDescriptionLineControl1.Name = "labelDescriptionLineControl1";
            this.labelDescriptionLineControl1.RequiredFlag = false;
            this.labelDescriptionLineControl1.Size = new System.Drawing.Size(215, 17);
            this.labelDescriptionLineControl1.TabIndex = 320;
            this.labelDescriptionLineControl1.TabStop = false;
            // 
            // labelDescriptionLineControl2
            // 
            this.labelDescriptionLineControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionLineControl2.DescriptionEnabled = true;
            this.labelDescriptionLineControl2.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionLineControl2.DescriptionText = "(備考)";
            this.labelDescriptionLineControl2.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionLineControl2.Location = new System.Drawing.Point(578, 1090);
            this.labelDescriptionLineControl2.MainText = "Remarks";
            this.labelDescriptionLineControl2.Name = "labelDescriptionLineControl2";
            this.labelDescriptionLineControl2.RequiredFlag = false;
            this.labelDescriptionLineControl2.Size = new System.Drawing.Size(215, 17);
            this.labelDescriptionLineControl2.TabIndex = 320;
            this.labelDescriptionLineControl2.TabStop = false;
            // 
            // AccidentDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDescriptionLineControl2);
            this.Controls.Add(this.labelDescriptionLineControl1);
            this.Controls.Add(this.labelDescriptionControl2);
            this.Controls.Add(this.labelDescriptionControl5);
            this.Controls.Add(this.labelDescriptionControl1);
            this.Controls.Add(this.textBoxRemarks);
            this.Controls.Add(this.textBoxInfluence);
            this.Controls.Add(this.groupBoxReports);
            this.Controls.Add(this.groupBoxProgress);
            this.Controls.Add(this.groupBoxSurveyResults);
            this.Controls.Add(this.statusSelectControl1);
            this.Controls.Add(this.comboBoxAccidentImportance);
            this.Controls.Add(this.groupBoxSpotReport);
            this.Controls.Add(this.groupBoxAccident);
            this.Controls.Add(this.textBoxAccidentReportNo);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AccidentDetailControl";
            this.Size = new System.Drawing.Size(1150, 1245);
            this.Load += new System.EventHandler(this.AccidentDetailControl_Load);
            this.groupBoxAccident.ResumeLayout(false);
            this.groupBoxAccident.PerformLayout();
            this.groupBoxSpotReport.ResumeLayout(false);
            this.groupBoxSpotReport.PerformLayout();
            this.groupBoxSurveyResults.ResumeLayout(false);
            this.groupBoxSurveyResults.PerformLayout();
            this.groupBoxProgress.ResumeLayout(false);
            this.groupBoxReports.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAccidentReportNo;
        private System.Windows.Forms.GroupBox groupBoxAccident;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonAttachmentAccident;
        private Util.FileViewControlEx fileViewControlExAccident;
        private System.Windows.Forms.TextBox textBoxAccident;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBoxSpotReport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonAttachmentSpotReport;
        private Util.FileViewControlEx fileViewControlExSpotReport;
        private System.Windows.Forms.TextBox textBoxSpotReport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxAccidentImportance;
        private DeficiencyControl.Controls.StatusSelectControl statusSelectControl1;
        private System.Windows.Forms.GroupBox groupBoxSurveyResults;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonAttachmentCauseOfAccident;
        private Util.FileViewControlEx fileViewControlExCauseOfAccident;
        private System.Windows.Forms.TextBox textBoxCauseOfAccident;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonAttachPreventiveAction;
        private Util.FileViewControlEx fileViewControlExPreventiveAction;
        private System.Windows.Forms.TextBox textBoxPreventiveAction;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBoxProgress;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProgress;
        private System.Windows.Forms.Button buttonProgressAdd;
        private System.Windows.Forms.GroupBox groupBoxReports;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanelReports;
        private System.Windows.Forms.TextBox textBoxInfluence;
        private System.Windows.Forms.TextBox textBoxRemarks;
        private System.Windows.Forms.Button buttonProgressDelete;
        private System.Windows.Forms.Button buttonReportsDelete;
        private System.Windows.Forms.Button buttonReportsAdd;
        private Controls.LabelDescriptionControl labelDescriptionControl1;
        private Controls.LabelDescriptionControl labelDescriptionControl5;
        private Controls.LabelDescriptionControl labelDescriptionControl2;
        private Controls.LabelDescriptionLineControl labelDescriptionLineControl1;
        private Controls.LabelDescriptionLineControl labelDescriptionLineControl2;
    }
}
