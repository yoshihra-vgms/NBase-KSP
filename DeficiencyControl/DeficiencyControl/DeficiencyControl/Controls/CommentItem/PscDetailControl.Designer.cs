namespace DeficiencyControl.Controls.CommentItem
{
    partial class PscDetailControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNo = new System.Windows.Forms.TextBox();
            this.statusSelectControl1 = new DeficiencyControl.Controls.StatusSelectControl();
            this.singleLineComboUser = new DeficiencyControl.Util.SingleLineCombo();
            this.groupBoxActionCode = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelActionCode = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonDeleteActionCode = new System.Windows.Forms.Button();
            this.buttonAddActionCode = new System.Windows.Forms.Button();
            this.groupBoxDeficiency = new System.Windows.Forms.GroupBox();
            this.labelDescriptionLineControl5 = new DeficiencyControl.Controls.LabelDescriptionLineControl();
            this.labelDescriptionLineControl4 = new DeficiencyControl.Controls.LabelDescriptionLineControl();
            this.labelDescriptionLineControl3 = new DeficiencyControl.Controls.LabelDescriptionLineControl();
            this.textBoxCauseOfDeficiency = new System.Windows.Forms.TextBox();
            this.textBoxDeficiency = new System.Windows.Forms.TextBox();
            this.deficiencyCodeSelectControl1 = new DeficiencyControl.Controls.CommentItem.DeficiencyCodeSelectControl();
            this.groupBoxActionTakenByVessel = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonAttachmentActionTakenByVessel = new System.Windows.Forms.Button();
            this.fileViewControlExActionTakenByVessel = new DeficiencyControl.Util.FileViewControlEx();
            this.textBoxActionTakenByVessel = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxActionTakenByCompany = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonAttachmentActionTakenByCompany = new System.Windows.Forms.Button();
            this.fileViewControlExActionTakenByCompany = new DeficiencyControl.Util.FileViewControlEx();
            this.textBoxActionTakenByCompany = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBoxClassInvolved = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxNkName = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxNkDeportment = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonAttachmentClassInvolved = new System.Windows.Forms.Button();
            this.fileViewControlExClassInvolved = new DeficiencyControl.Util.FileViewControlEx();
            this.textBoxClassInvolved = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBoxCorrectiveAction = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.buttonAttachmentCorrectiveAction = new System.Windows.Forms.Button();
            this.fileViewControlExCorrectiveAction = new DeficiencyControl.Util.FileViewControlEx();
            this.textBoxCorrectiveAction = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxRemarks = new System.Windows.Forms.TextBox();
            this.labelDescriptionLineControl1 = new DeficiencyControl.Controls.LabelDescriptionLineControl();
            this.labelDescriptionLineControl2 = new DeficiencyControl.Controls.LabelDescriptionLineControl();
            this.labelDescriptionLineControl6 = new DeficiencyControl.Controls.LabelDescriptionLineControl();
            this.groupBoxActionCode.SuspendLayout();
            this.groupBoxDeficiency.SuspendLayout();
            this.groupBoxActionTakenByVessel.SuspendLayout();
            this.groupBoxActionTakenByCompany.SuspendLayout();
            this.groupBoxClassInvolved.SuspendLayout();
            this.groupBoxCorrectiveAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "No";
            // 
            // textBoxNo
            // 
            this.textBoxNo.Location = new System.Drawing.Point(90, 3);
            this.textBoxNo.Name = "textBoxNo";
            this.textBoxNo.ReadOnly = true;
            this.textBoxNo.Size = new System.Drawing.Size(100, 23);
            this.textBoxNo.TabIndex = 1;
            // 
            // statusSelectControl1
            // 
            this.statusSelectControl1.AutoSize = true;
            this.statusSelectControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statusSelectControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusSelectControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.statusSelectControl1.Location = new System.Drawing.Point(158, 45);
            this.statusSelectControl1.Name = "statusSelectControl1";
            this.statusSelectControl1.Size = new System.Drawing.Size(237, 28);
            this.statusSelectControl1.TabIndex = 2;
            // 
            // singleLineComboUser
            // 
            this.singleLineComboUser.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboUser.Location = new System.Drawing.Point(158, 108);
            this.singleLineComboUser.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboUser.MaxLength = 32767;
            this.singleLineComboUser.Name = "singleLineComboUser";
            this.singleLineComboUser.ReadOnly = false;
            this.singleLineComboUser.Size = new System.Drawing.Size(237, 23);
            this.singleLineComboUser.TabIndex = 3;
            // 
            // groupBoxActionCode
            // 
            this.groupBoxActionCode.Controls.Add(this.flowLayoutPanelActionCode);
            this.groupBoxActionCode.Controls.Add(this.buttonDeleteActionCode);
            this.groupBoxActionCode.Controls.Add(this.buttonAddActionCode);
            this.groupBoxActionCode.Location = new System.Drawing.Point(6, 151);
            this.groupBoxActionCode.Name = "groupBoxActionCode";
            this.groupBoxActionCode.Size = new System.Drawing.Size(560, 238);
            this.groupBoxActionCode.TabIndex = 4;
            this.groupBoxActionCode.TabStop = false;
            this.groupBoxActionCode.Text = "Action Code";
            // 
            // flowLayoutPanelActionCode
            // 
            this.flowLayoutPanelActionCode.AutoScroll = true;
            this.flowLayoutPanelActionCode.Location = new System.Drawing.Point(6, 51);
            this.flowLayoutPanelActionCode.Name = "flowLayoutPanelActionCode";
            this.flowLayoutPanelActionCode.Size = new System.Drawing.Size(548, 177);
            this.flowLayoutPanelActionCode.TabIndex = 2;
            // 
            // buttonDeleteActionCode
            // 
            this.buttonDeleteActionCode.Location = new System.Drawing.Point(87, 22);
            this.buttonDeleteActionCode.Name = "buttonDeleteActionCode";
            this.buttonDeleteActionCode.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteActionCode.TabIndex = 1;
            this.buttonDeleteActionCode.Text = "Del";
            this.buttonDeleteActionCode.UseVisualStyleBackColor = true;
            this.buttonDeleteActionCode.Click += new System.EventHandler(this.buttonDeleteActionCode_Click);
            // 
            // buttonAddActionCode
            // 
            this.buttonAddActionCode.Location = new System.Drawing.Point(6, 22);
            this.buttonAddActionCode.Name = "buttonAddActionCode";
            this.buttonAddActionCode.Size = new System.Drawing.Size(75, 23);
            this.buttonAddActionCode.TabIndex = 0;
            this.buttonAddActionCode.Text = "Add";
            this.buttonAddActionCode.UseVisualStyleBackColor = true;
            this.buttonAddActionCode.Click += new System.EventHandler(this.buttonAddActionCode_Click);
            // 
            // groupBoxDeficiency
            // 
            this.groupBoxDeficiency.Controls.Add(this.labelDescriptionLineControl5);
            this.groupBoxDeficiency.Controls.Add(this.labelDescriptionLineControl4);
            this.groupBoxDeficiency.Controls.Add(this.labelDescriptionLineControl3);
            this.groupBoxDeficiency.Controls.Add(this.textBoxCauseOfDeficiency);
            this.groupBoxDeficiency.Controls.Add(this.textBoxDeficiency);
            this.groupBoxDeficiency.Controls.Add(this.deficiencyCodeSelectControl1);
            this.groupBoxDeficiency.Location = new System.Drawing.Point(587, 3);
            this.groupBoxDeficiency.Name = "groupBoxDeficiency";
            this.groupBoxDeficiency.Size = new System.Drawing.Size(560, 386);
            this.groupBoxDeficiency.TabIndex = 7;
            this.groupBoxDeficiency.TabStop = false;
            this.groupBoxDeficiency.Text = "Deficiency (指摘事項)";
            // 
            // labelDescriptionLineControl5
            // 
            this.labelDescriptionLineControl5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionLineControl5.DescriptionEnabled = true;
            this.labelDescriptionLineControl5.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionLineControl5.DescriptionText = "(原因)";
            this.labelDescriptionLineControl5.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionLineControl5.Location = new System.Drawing.Point(6, 231);
            this.labelDescriptionLineControl5.MainText = "Cause of Deficiency";
            this.labelDescriptionLineControl5.Name = "labelDescriptionLineControl5";
            this.labelDescriptionLineControl5.RequiredFlag = false;
            this.labelDescriptionLineControl5.Size = new System.Drawing.Size(257, 19);
            this.labelDescriptionLineControl5.TabIndex = 298;
            this.labelDescriptionLineControl5.TabStop = false;
            // 
            // labelDescriptionLineControl4
            // 
            this.labelDescriptionLineControl4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionLineControl4.DescriptionEnabled = true;
            this.labelDescriptionLineControl4.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionLineControl4.DescriptionText = "(指摘事項)";
            this.labelDescriptionLineControl4.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionLineControl4.Location = new System.Drawing.Point(6, 80);
            this.labelDescriptionLineControl4.MainText = "Deficiency";
            this.labelDescriptionLineControl4.Name = "labelDescriptionLineControl4";
            this.labelDescriptionLineControl4.RequiredFlag = false;
            this.labelDescriptionLineControl4.Size = new System.Drawing.Size(189, 19);
            this.labelDescriptionLineControl4.TabIndex = 298;
            this.labelDescriptionLineControl4.TabStop = false;
            // 
            // labelDescriptionLineControl3
            // 
            this.labelDescriptionLineControl3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionLineControl3.DescriptionEnabled = true;
            this.labelDescriptionLineControl3.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionLineControl3.DescriptionText = "(指摘事項コード)";
            this.labelDescriptionLineControl3.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionLineControl3.Location = new System.Drawing.Point(6, 22);
            this.labelDescriptionLineControl3.MainText = "Deficiency Code";
            this.labelDescriptionLineControl3.Name = "labelDescriptionLineControl3";
            this.labelDescriptionLineControl3.RequiredFlag = true;
            this.labelDescriptionLineControl3.Size = new System.Drawing.Size(257, 19);
            this.labelDescriptionLineControl3.TabIndex = 297;
            this.labelDescriptionLineControl3.TabStop = false;
            // 
            // textBoxCauseOfDeficiency
            // 
            this.textBoxCauseOfDeficiency.Location = new System.Drawing.Point(23, 256);
            this.textBoxCauseOfDeficiency.Multiline = true;
            this.textBoxCauseOfDeficiency.Name = "textBoxCauseOfDeficiency";
            this.textBoxCauseOfDeficiency.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxCauseOfDeficiency.Size = new System.Drawing.Size(531, 120);
            this.textBoxCauseOfDeficiency.TabIndex = 2;
            // 
            // textBoxDeficiency
            // 
            this.textBoxDeficiency.Location = new System.Drawing.Point(23, 105);
            this.textBoxDeficiency.Multiline = true;
            this.textBoxDeficiency.Name = "textBoxDeficiency";
            this.textBoxDeficiency.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDeficiency.Size = new System.Drawing.Size(531, 120);
            this.textBoxDeficiency.TabIndex = 1;
            // 
            // deficiencyCodeSelectControl1
            // 
            this.deficiencyCodeSelectControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.deficiencyCodeSelectControl1.Location = new System.Drawing.Point(23, 42);
            this.deficiencyCodeSelectControl1.Margin = new System.Windows.Forms.Padding(4);
            this.deficiencyCodeSelectControl1.Name = "deficiencyCodeSelectControl1";
            this.deficiencyCodeSelectControl1.Size = new System.Drawing.Size(536, 31);
            this.deficiencyCodeSelectControl1.TabIndex = 0;
            // 
            // groupBoxActionTakenByVessel
            // 
            this.groupBoxActionTakenByVessel.Controls.Add(this.label6);
            this.groupBoxActionTakenByVessel.Controls.Add(this.buttonAttachmentActionTakenByVessel);
            this.groupBoxActionTakenByVessel.Controls.Add(this.fileViewControlExActionTakenByVessel);
            this.groupBoxActionTakenByVessel.Controls.Add(this.textBoxActionTakenByVessel);
            this.groupBoxActionTakenByVessel.Controls.Add(this.label7);
            this.groupBoxActionTakenByVessel.Location = new System.Drawing.Point(6, 399);
            this.groupBoxActionTakenByVessel.Name = "groupBoxActionTakenByVessel";
            this.groupBoxActionTakenByVessel.Size = new System.Drawing.Size(560, 287);
            this.groupBoxActionTakenByVessel.TabIndex = 5;
            this.groupBoxActionTakenByVessel.TabStop = false;
            this.groupBoxActionTakenByVessel.Text = "Action taken By Vessel (本船対応)";
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
            // buttonAttachmentActionTakenByVessel
            // 
            this.buttonAttachmentActionTakenByVessel.Location = new System.Drawing.Point(9, 251);
            this.buttonAttachmentActionTakenByVessel.Name = "buttonAttachmentActionTakenByVessel";
            this.buttonAttachmentActionTakenByVessel.Size = new System.Drawing.Size(54, 23);
            this.buttonAttachmentActionTakenByVessel.TabIndex = 1;
            this.buttonAttachmentActionTakenByVessel.Tag = "0";
            this.buttonAttachmentActionTakenByVessel.Text = "...";
            this.buttonAttachmentActionTakenByVessel.UseVisualStyleBackColor = true;
            this.buttonAttachmentActionTakenByVessel.Click += new System.EventHandler(this.buttonAttachment_Click);
            // 
            // fileViewControlExActionTakenByVessel
            // 
            this.fileViewControlExActionTakenByVessel.AllowDrop = true;
            this.fileViewControlExActionTakenByVessel.EnableDelete = true;
            this.fileViewControlExActionTakenByVessel.EnableDragDrop = true;
            this.fileViewControlExActionTakenByVessel.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.fileViewControlExActionTakenByVessel.Location = new System.Drawing.Point(84, 154);
            this.fileViewControlExActionTakenByVessel.Name = "fileViewControlExActionTakenByVessel";
            this.fileViewControlExActionTakenByVessel.Size = new System.Drawing.Size(470, 120);
            this.fileViewControlExActionTakenByVessel.TabIndex = 2;
            this.fileViewControlExActionTakenByVessel.FileItemSelected += new DeficiencyControl.Util.FileViewControlEx.FileItemSelectDelegate(this.fileViewControlEx_FileItemSelected);
            // 
            // textBoxActionTakenByVessel
            // 
            this.textBoxActionTakenByVessel.Location = new System.Drawing.Point(84, 28);
            this.textBoxActionTakenByVessel.Multiline = true;
            this.textBoxActionTakenByVessel.Name = "textBoxActionTakenByVessel";
            this.textBoxActionTakenByVessel.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxActionTakenByVessel.Size = new System.Drawing.Size(470, 120);
            this.textBoxActionTakenByVessel.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label7.Location = new System.Drawing.Point(6, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 283;
            this.label7.Text = "対応内容";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBoxActionTakenByCompany
            // 
            this.groupBoxActionTakenByCompany.Controls.Add(this.label8);
            this.groupBoxActionTakenByCompany.Controls.Add(this.buttonAttachmentActionTakenByCompany);
            this.groupBoxActionTakenByCompany.Controls.Add(this.fileViewControlExActionTakenByCompany);
            this.groupBoxActionTakenByCompany.Controls.Add(this.textBoxActionTakenByCompany);
            this.groupBoxActionTakenByCompany.Controls.Add(this.label9);
            this.groupBoxActionTakenByCompany.Location = new System.Drawing.Point(587, 399);
            this.groupBoxActionTakenByCompany.Name = "groupBoxActionTakenByCompany";
            this.groupBoxActionTakenByCompany.Size = new System.Drawing.Size(560, 287);
            this.groupBoxActionTakenByCompany.TabIndex = 8;
            this.groupBoxActionTakenByCompany.TabStop = false;
            this.groupBoxActionTakenByCompany.Text = "Action taken By Company (会社対応)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label8.Location = new System.Drawing.Point(6, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 287;
            this.label8.Text = "添付資料";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonAttachmentActionTakenByCompany
            // 
            this.buttonAttachmentActionTakenByCompany.Location = new System.Drawing.Point(9, 251);
            this.buttonAttachmentActionTakenByCompany.Name = "buttonAttachmentActionTakenByCompany";
            this.buttonAttachmentActionTakenByCompany.Size = new System.Drawing.Size(54, 23);
            this.buttonAttachmentActionTakenByCompany.TabIndex = 1;
            this.buttonAttachmentActionTakenByCompany.Tag = "1";
            this.buttonAttachmentActionTakenByCompany.Text = "...";
            this.buttonAttachmentActionTakenByCompany.UseVisualStyleBackColor = true;
            this.buttonAttachmentActionTakenByCompany.Click += new System.EventHandler(this.buttonAttachment_Click);
            // 
            // fileViewControlExActionTakenByCompany
            // 
            this.fileViewControlExActionTakenByCompany.AllowDrop = true;
            this.fileViewControlExActionTakenByCompany.EnableDelete = true;
            this.fileViewControlExActionTakenByCompany.EnableDragDrop = true;
            this.fileViewControlExActionTakenByCompany.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.fileViewControlExActionTakenByCompany.Location = new System.Drawing.Point(84, 154);
            this.fileViewControlExActionTakenByCompany.Name = "fileViewControlExActionTakenByCompany";
            this.fileViewControlExActionTakenByCompany.Size = new System.Drawing.Size(470, 120);
            this.fileViewControlExActionTakenByCompany.TabIndex = 2;
            this.fileViewControlExActionTakenByCompany.FileItemSelected += new DeficiencyControl.Util.FileViewControlEx.FileItemSelectDelegate(this.fileViewControlEx_FileItemSelected);
            // 
            // textBoxActionTakenByCompany
            // 
            this.textBoxActionTakenByCompany.Location = new System.Drawing.Point(84, 28);
            this.textBoxActionTakenByCompany.Multiline = true;
            this.textBoxActionTakenByCompany.Name = "textBoxActionTakenByCompany";
            this.textBoxActionTakenByCompany.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxActionTakenByCompany.Size = new System.Drawing.Size(470, 120);
            this.textBoxActionTakenByCompany.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label9.Location = new System.Drawing.Point(6, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 283;
            this.label9.Text = "対応内容";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBoxClassInvolved
            // 
            this.groupBoxClassInvolved.Controls.Add(this.label15);
            this.groupBoxClassInvolved.Controls.Add(this.textBoxNkName);
            this.groupBoxClassInvolved.Controls.Add(this.label14);
            this.groupBoxClassInvolved.Controls.Add(this.textBoxNkDeportment);
            this.groupBoxClassInvolved.Controls.Add(this.label13);
            this.groupBoxClassInvolved.Controls.Add(this.label11);
            this.groupBoxClassInvolved.Controls.Add(this.buttonAttachmentClassInvolved);
            this.groupBoxClassInvolved.Controls.Add(this.fileViewControlExClassInvolved);
            this.groupBoxClassInvolved.Controls.Add(this.textBoxClassInvolved);
            this.groupBoxClassInvolved.Controls.Add(this.label12);
            this.groupBoxClassInvolved.Location = new System.Drawing.Point(6, 692);
            this.groupBoxClassInvolved.Name = "groupBoxClassInvolved";
            this.groupBoxClassInvolved.Size = new System.Drawing.Size(560, 431);
            this.groupBoxClassInvolved.TabIndex = 6;
            this.groupBoxClassInvolved.TabStop = false;
            this.groupBoxClassInvolved.Text = "Class Involved (NK対応)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label15.Location = new System.Drawing.Point(81, 63);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(37, 15);
            this.label15.TabIndex = 292;
            this.label15.Text = "氏名";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBoxNkName
            // 
            this.textBoxNkName.Location = new System.Drawing.Point(152, 60);
            this.textBoxNkName.Name = "textBoxNkName";
            this.textBoxNkName.Size = new System.Drawing.Size(402, 23);
            this.textBoxNkName.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label14.Location = new System.Drawing.Point(81, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 15);
            this.label14.TabIndex = 290;
            this.label14.Text = "部署";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBoxNkDeportment
            // 
            this.textBoxNkDeportment.Location = new System.Drawing.Point(152, 22);
            this.textBoxNkDeportment.Name = "textBoxNkDeportment";
            this.textBoxNkDeportment.Size = new System.Drawing.Size(402, 23);
            this.textBoxNkDeportment.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label13.Location = new System.Drawing.Point(6, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 15);
            this.label13.TabIndex = 288;
            this.label13.Text = "NK窓口";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label11.Location = new System.Drawing.Point(6, 305);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 15);
            this.label11.TabIndex = 287;
            this.label11.Text = "添付資料";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonAttachmentClassInvolved
            // 
            this.buttonAttachmentClassInvolved.Location = new System.Drawing.Point(9, 402);
            this.buttonAttachmentClassInvolved.Name = "buttonAttachmentClassInvolved";
            this.buttonAttachmentClassInvolved.Size = new System.Drawing.Size(54, 23);
            this.buttonAttachmentClassInvolved.TabIndex = 3;
            this.buttonAttachmentClassInvolved.Tag = "2";
            this.buttonAttachmentClassInvolved.Text = "...";
            this.buttonAttachmentClassInvolved.UseVisualStyleBackColor = true;
            this.buttonAttachmentClassInvolved.Click += new System.EventHandler(this.buttonAttachment_Click);
            // 
            // fileViewControlExClassInvolved
            // 
            this.fileViewControlExClassInvolved.AllowDrop = true;
            this.fileViewControlExClassInvolved.EnableDelete = true;
            this.fileViewControlExClassInvolved.EnableDragDrop = true;
            this.fileViewControlExClassInvolved.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.fileViewControlExClassInvolved.Location = new System.Drawing.Point(84, 305);
            this.fileViewControlExClassInvolved.Name = "fileViewControlExClassInvolved";
            this.fileViewControlExClassInvolved.Size = new System.Drawing.Size(470, 120);
            this.fileViewControlExClassInvolved.TabIndex = 4;
            this.fileViewControlExClassInvolved.FileItemSelected += new DeficiencyControl.Util.FileViewControlEx.FileItemSelectDelegate(this.fileViewControlEx_FileItemSelected);
            // 
            // textBoxClassInvolved
            // 
            this.textBoxClassInvolved.Location = new System.Drawing.Point(84, 103);
            this.textBoxClassInvolved.Multiline = true;
            this.textBoxClassInvolved.Name = "textBoxClassInvolved";
            this.textBoxClassInvolved.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxClassInvolved.Size = new System.Drawing.Size(470, 165);
            this.textBoxClassInvolved.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label12.Location = new System.Drawing.Point(6, 106);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 15);
            this.label12.TabIndex = 283;
            this.label12.Text = "対応内容";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBoxCorrectiveAction
            // 
            this.groupBoxCorrectiveAction.Controls.Add(this.label19);
            this.groupBoxCorrectiveAction.Controls.Add(this.buttonAttachmentCorrectiveAction);
            this.groupBoxCorrectiveAction.Controls.Add(this.fileViewControlExCorrectiveAction);
            this.groupBoxCorrectiveAction.Controls.Add(this.textBoxCorrectiveAction);
            this.groupBoxCorrectiveAction.Controls.Add(this.label20);
            this.groupBoxCorrectiveAction.Location = new System.Drawing.Point(587, 692);
            this.groupBoxCorrectiveAction.Name = "groupBoxCorrectiveAction";
            this.groupBoxCorrectiveAction.Size = new System.Drawing.Size(560, 280);
            this.groupBoxCorrectiveAction.TabIndex = 9;
            this.groupBoxCorrectiveAction.TabStop = false;
            this.groupBoxCorrectiveAction.Text = "Corrective Action (是正措置)";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label19.Location = new System.Drawing.Point(5, 148);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 15);
            this.label19.TabIndex = 287;
            this.label19.Text = "添付資料";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonAttachmentCorrectiveAction
            // 
            this.buttonAttachmentCorrectiveAction.Location = new System.Drawing.Point(9, 245);
            this.buttonAttachmentCorrectiveAction.Name = "buttonAttachmentCorrectiveAction";
            this.buttonAttachmentCorrectiveAction.Size = new System.Drawing.Size(54, 23);
            this.buttonAttachmentCorrectiveAction.TabIndex = 1;
            this.buttonAttachmentCorrectiveAction.Tag = "3";
            this.buttonAttachmentCorrectiveAction.Text = "...";
            this.buttonAttachmentCorrectiveAction.UseVisualStyleBackColor = true;
            this.buttonAttachmentCorrectiveAction.Click += new System.EventHandler(this.buttonAttachment_Click);
            // 
            // fileViewControlExCorrectiveAction
            // 
            this.fileViewControlExCorrectiveAction.AllowDrop = true;
            this.fileViewControlExCorrectiveAction.EnableDelete = true;
            this.fileViewControlExCorrectiveAction.EnableDragDrop = true;
            this.fileViewControlExCorrectiveAction.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.fileViewControlExCorrectiveAction.Location = new System.Drawing.Point(83, 148);
            this.fileViewControlExCorrectiveAction.Name = "fileViewControlExCorrectiveAction";
            this.fileViewControlExCorrectiveAction.Size = new System.Drawing.Size(470, 120);
            this.fileViewControlExCorrectiveAction.TabIndex = 2;
            this.fileViewControlExCorrectiveAction.FileItemSelected += new DeficiencyControl.Util.FileViewControlEx.FileItemSelectDelegate(this.fileViewControlEx_FileItemSelected);
            // 
            // textBoxCorrectiveAction
            // 
            this.textBoxCorrectiveAction.Location = new System.Drawing.Point(84, 22);
            this.textBoxCorrectiveAction.Multiline = true;
            this.textBoxCorrectiveAction.Name = "textBoxCorrectiveAction";
            this.textBoxCorrectiveAction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxCorrectiveAction.Size = new System.Drawing.Size(470, 120);
            this.textBoxCorrectiveAction.TabIndex = 0;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label20.Location = new System.Drawing.Point(6, 22);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(67, 15);
            this.label20.TabIndex = 283;
            this.label20.Text = "是正結果";
            this.label20.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBoxRemarks
            // 
            this.textBoxRemarks.Location = new System.Drawing.Point(596, 997);
            this.textBoxRemarks.Multiline = true;
            this.textBoxRemarks.Name = "textBoxRemarks";
            this.textBoxRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxRemarks.Size = new System.Drawing.Size(551, 117);
            this.textBoxRemarks.TabIndex = 12;
            // 
            // labelDescriptionLineControl1
            // 
            this.labelDescriptionLineControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionLineControl1.DescriptionEnabled = true;
            this.labelDescriptionLineControl1.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionLineControl1.DescriptionText = "(状態)";
            this.labelDescriptionLineControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionLineControl1.Location = new System.Drawing.Point(3, 50);
            this.labelDescriptionLineControl1.MainText = "Status";
            this.labelDescriptionLineControl1.Name = "labelDescriptionLineControl1";
            this.labelDescriptionLineControl1.RequiredFlag = true;
            this.labelDescriptionLineControl1.Size = new System.Drawing.Size(130, 14);
            this.labelDescriptionLineControl1.TabIndex = 296;
            this.labelDescriptionLineControl1.TabStop = false;
            // 
            // labelDescriptionLineControl2
            // 
            this.labelDescriptionLineControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionLineControl2.DescriptionEnabled = true;
            this.labelDescriptionLineControl2.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionLineControl2.DescriptionText = "(入力者)";
            this.labelDescriptionLineControl2.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionLineControl2.Location = new System.Drawing.Point(3, 112);
            this.labelDescriptionLineControl2.MainText = "P.I.C";
            this.labelDescriptionLineControl2.Name = "labelDescriptionLineControl2";
            this.labelDescriptionLineControl2.RequiredFlag = true;
            this.labelDescriptionLineControl2.Size = new System.Drawing.Size(130, 14);
            this.labelDescriptionLineControl2.TabIndex = 296;
            this.labelDescriptionLineControl2.TabStop = false;
            // 
            // labelDescriptionLineControl6
            // 
            this.labelDescriptionLineControl6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionLineControl6.DescriptionEnabled = true;
            this.labelDescriptionLineControl6.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionLineControl6.DescriptionText = "(備考)";
            this.labelDescriptionLineControl6.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionLineControl6.Location = new System.Drawing.Point(578, 978);
            this.labelDescriptionLineControl6.MainText = "Remarks";
            this.labelDescriptionLineControl6.Name = "labelDescriptionLineControl6";
            this.labelDescriptionLineControl6.RequiredFlag = false;
            this.labelDescriptionLineControl6.Size = new System.Drawing.Size(189, 19);
            this.labelDescriptionLineControl6.TabIndex = 11;
            this.labelDescriptionLineControl6.TabStop = false;
            // 
            // PscDetailControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.labelDescriptionLineControl6);
            this.Controls.Add(this.labelDescriptionLineControl2);
            this.Controls.Add(this.labelDescriptionLineControl1);
            this.Controls.Add(this.textBoxRemarks);
            this.Controls.Add(this.groupBoxCorrectiveAction);
            this.Controls.Add(this.groupBoxClassInvolved);
            this.Controls.Add(this.groupBoxActionTakenByCompany);
            this.Controls.Add(this.groupBoxActionTakenByVessel);
            this.Controls.Add(this.groupBoxDeficiency);
            this.Controls.Add(this.groupBoxActionCode);
            this.Controls.Add(this.singleLineComboUser);
            this.Controls.Add(this.statusSelectControl1);
            this.Controls.Add(this.textBoxNo);
            this.Controls.Add(this.label1);
            this.Name = "PscDetailControl";
            this.Size = new System.Drawing.Size(1150, 1134);
            this.Load += new System.EventHandler(this.PscDetailControl_Load);
            this.groupBoxActionCode.ResumeLayout(false);
            this.groupBoxDeficiency.ResumeLayout(false);
            this.groupBoxDeficiency.PerformLayout();
            this.groupBoxActionTakenByVessel.ResumeLayout(false);
            this.groupBoxActionTakenByVessel.PerformLayout();
            this.groupBoxActionTakenByCompany.ResumeLayout(false);
            this.groupBoxActionTakenByCompany.PerformLayout();
            this.groupBoxClassInvolved.ResumeLayout(false);
            this.groupBoxClassInvolved.PerformLayout();
            this.groupBoxCorrectiveAction.ResumeLayout(false);
            this.groupBoxCorrectiveAction.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNo;
        private StatusSelectControl statusSelectControl1;
        private Util.SingleLineCombo singleLineComboUser;
        private System.Windows.Forms.GroupBox groupBoxActionCode;
        private System.Windows.Forms.GroupBox groupBoxDeficiency;
        private DeficiencyCodeSelectControl deficiencyCodeSelectControl1;
        private System.Windows.Forms.TextBox textBoxDeficiency;
        private System.Windows.Forms.TextBox textBoxCauseOfDeficiency;
        private System.Windows.Forms.GroupBox groupBoxActionTakenByVessel;
        private System.Windows.Forms.TextBox textBoxActionTakenByVessel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonAttachmentActionTakenByVessel;
        private Util.FileViewControlEx fileViewControlExActionTakenByVessel;
        private System.Windows.Forms.GroupBox groupBoxActionTakenByCompany;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonAttachmentActionTakenByCompany;
        private Util.FileViewControlEx fileViewControlExActionTakenByCompany;
        private System.Windows.Forms.TextBox textBoxActionTakenByCompany;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBoxClassInvolved;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxNkName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxNkDeportment;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonAttachmentClassInvolved;
        private Util.FileViewControlEx fileViewControlExClassInvolved;
        private System.Windows.Forms.TextBox textBoxClassInvolved;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBoxCorrectiveAction;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button buttonAttachmentCorrectiveAction;
        private Util.FileViewControlEx fileViewControlExCorrectiveAction;
        private System.Windows.Forms.TextBox textBoxCorrectiveAction;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxRemarks;
        private System.Windows.Forms.Button buttonAddActionCode;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanelActionCode;
        private System.Windows.Forms.Button buttonDeleteActionCode;
        private LabelDescriptionLineControl labelDescriptionLineControl1;
        private LabelDescriptionLineControl labelDescriptionLineControl5;
        private LabelDescriptionLineControl labelDescriptionLineControl4;
        private LabelDescriptionLineControl labelDescriptionLineControl3;
        private LabelDescriptionLineControl labelDescriptionLineControl2;
        private LabelDescriptionLineControl labelDescriptionLineControl6;
    }
}
