namespace DeficiencyControl.MOI
{
    partial class MoiDetailControl
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
            this.textBoxNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusSelectControlMoiStatus = new DeficiencyControl.Controls.StatusSelectControl();
            this.groupBoxDescription = new System.Windows.Forms.GroupBox();
            this.labelDescriptionControl4 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl3 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl2 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.viqNoControl1 = new DeficiencyControl.MOI.ViqNoControl();
            this.viqCodeControl1 = new DeficiencyControl.MOI.ViqCodeControl();
            this.textBoxObservation = new System.Windows.Forms.TextBox();
            this.groupBoxAction = new System.Windows.Forms.GroupBox();
            this.labelDescriptionControl5 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.groupBoxCorrectiveAction = new System.Windows.Forms.GroupBox();
            this.labelDescriptionControl7 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl6 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.textBoxSpecialNotes = new System.Windows.Forms.TextBox();
            this.textBoxPreventiveAction = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonAttachmentComment2nd = new System.Windows.Forms.Button();
            this.fileViewControlExComment2nd = new DeficiencyControl.Util.FileViewControlEx();
            this.textBoxComment2nd = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonComment2nd = new System.Windows.Forms.RadioButton();
            this.radioButtonComment1st = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonAttachmentComment1st = new System.Windows.Forms.Button();
            this.fileViewControlExComment1st = new DeficiencyControl.Util.FileViewControlEx();
            this.textBoxComment1st = new System.Windows.Forms.TextBox();
            this.textBoxRootCause = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.labelDescriptionControl1 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.groupBoxDescription.SuspendLayout();
            this.groupBoxAction.SuspendLayout();
            this.groupBoxCorrectiveAction.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxNo
            // 
            this.textBoxNo.Location = new System.Drawing.Point(47, 12);
            this.textBoxNo.Name = "textBoxNo";
            this.textBoxNo.ReadOnly = true;
            this.textBoxNo.Size = new System.Drawing.Size(120, 23);
            this.textBoxNo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "No.";
            // 
            // statusSelectControlMoiStatus
            // 
            this.statusSelectControlMoiStatus.AutoSize = true;
            this.statusSelectControlMoiStatus.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statusSelectControlMoiStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusSelectControlMoiStatus.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.statusSelectControlMoiStatus.Location = new System.Drawing.Point(323, 10);
            this.statusSelectControlMoiStatus.Name = "statusSelectControlMoiStatus";
            this.statusSelectControlMoiStatus.Size = new System.Drawing.Size(237, 28);
            this.statusSelectControlMoiStatus.TabIndex = 1;
            // 
            // groupBoxDescription
            // 
            this.groupBoxDescription.Controls.Add(this.labelDescriptionControl4);
            this.groupBoxDescription.Controls.Add(this.labelDescriptionControl3);
            this.groupBoxDescription.Controls.Add(this.labelDescriptionControl2);
            this.groupBoxDescription.Controls.Add(this.viqNoControl1);
            this.groupBoxDescription.Controls.Add(this.viqCodeControl1);
            this.groupBoxDescription.Controls.Add(this.textBoxObservation);
            this.groupBoxDescription.Location = new System.Drawing.Point(3, 54);
            this.groupBoxDescription.Name = "groupBoxDescription";
            this.groupBoxDescription.Size = new System.Drawing.Size(1144, 319);
            this.groupBoxDescription.TabIndex = 2;
            this.groupBoxDescription.TabStop = false;
            this.groupBoxDescription.Text = "Description (指摘概要)";
            // 
            // labelDescriptionControl4
            // 
            this.labelDescriptionControl4.AutoSize = true;
            this.labelDescriptionControl4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl4.DescriptionEnabled = true;
            this.labelDescriptionControl4.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl4.DescriptionText = "(指摘事項)";
            this.labelDescriptionControl4.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl4.Location = new System.Drawing.Point(6, 176);
            this.labelDescriptionControl4.MainText = "Observation";
            this.labelDescriptionControl4.Name = "labelDescriptionControl4";
            this.labelDescriptionControl4.RequiredFlag = true;
            this.labelDescriptionControl4.Size = new System.Drawing.Size(105, 31);
            this.labelDescriptionControl4.TabIndex = 345;
            // 
            // labelDescriptionControl3
            // 
            this.labelDescriptionControl3.AutoSize = true;
            this.labelDescriptionControl3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl3.DescriptionEnabled = false;
            this.labelDescriptionControl3.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl3.DescriptionText = "";
            this.labelDescriptionControl3.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl3.Location = new System.Drawing.Point(6, 80);
            this.labelDescriptionControl3.MainText = "VIQ No.";
            this.labelDescriptionControl3.Name = "labelDescriptionControl3";
            this.labelDescriptionControl3.RequiredFlag = true;
            this.labelDescriptionControl3.Size = new System.Drawing.Size(75, 16);
            this.labelDescriptionControl3.TabIndex = 345;
            // 
            // labelDescriptionControl2
            // 
            this.labelDescriptionControl2.AutoSize = true;
            this.labelDescriptionControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl2.DescriptionEnabled = false;
            this.labelDescriptionControl2.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl2.DescriptionText = "";
            this.labelDescriptionControl2.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl2.Location = new System.Drawing.Point(6, 45);
            this.labelDescriptionControl2.MainText = "VIQ Code";
            this.labelDescriptionControl2.Name = "labelDescriptionControl2";
            this.labelDescriptionControl2.RequiredFlag = true;
            this.labelDescriptionControl2.Size = new System.Drawing.Size(89, 16);
            this.labelDescriptionControl2.TabIndex = 345;
            // 
            // viqNoControl1
            // 
            this.viqNoControl1.AutoSize = true;
            this.viqNoControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.viqNoControl1.Location = new System.Drawing.Point(138, 74);
            this.viqNoControl1.Margin = new System.Windows.Forms.Padding(4);
            this.viqNoControl1.Name = "viqNoControl1";
            this.viqNoControl1.Size = new System.Drawing.Size(991, 95);
            this.viqNoControl1.TabIndex = 1;
            // 
            // viqCodeControl1
            // 
            this.viqCodeControl1.AutoSize = true;
            this.viqCodeControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.viqCodeControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.viqCodeControl1.Location = new System.Drawing.Point(138, 35);
            this.viqCodeControl1.Margin = new System.Windows.Forms.Padding(4);
            this.viqCodeControl1.Name = "viqCodeControl1";
            this.viqCodeControl1.Size = new System.Drawing.Size(534, 31);
            this.viqCodeControl1.TabIndex = 0;
            // 
            // textBoxObservation
            // 
            this.textBoxObservation.Location = new System.Drawing.Point(141, 176);
            this.textBoxObservation.Multiline = true;
            this.textBoxObservation.Name = "textBoxObservation";
            this.textBoxObservation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxObservation.Size = new System.Drawing.Size(988, 120);
            this.textBoxObservation.TabIndex = 2;
            // 
            // groupBoxAction
            // 
            this.groupBoxAction.Controls.Add(this.labelDescriptionControl5);
            this.groupBoxAction.Controls.Add(this.groupBoxCorrectiveAction);
            this.groupBoxAction.Controls.Add(this.textBoxRootCause);
            this.groupBoxAction.Location = new System.Drawing.Point(3, 388);
            this.groupBoxAction.Name = "groupBoxAction";
            this.groupBoxAction.Size = new System.Drawing.Size(1144, 555);
            this.groupBoxAction.TabIndex = 3;
            this.groupBoxAction.TabStop = false;
            this.groupBoxAction.Text = "Action (コメント・是正)";
            // 
            // labelDescriptionControl5
            // 
            this.labelDescriptionControl5.AutoSize = true;
            this.labelDescriptionControl5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl5.DescriptionEnabled = true;
            this.labelDescriptionControl5.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl5.DescriptionText = "(根本原因)";
            this.labelDescriptionControl5.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl5.Location = new System.Drawing.Point(12, 29);
            this.labelDescriptionControl5.MainText = "Root Cause";
            this.labelDescriptionControl5.Name = "labelDescriptionControl5";
            this.labelDescriptionControl5.RequiredFlag = false;
            this.labelDescriptionControl5.Size = new System.Drawing.Size(103, 31);
            this.labelDescriptionControl5.TabIndex = 345;
            // 
            // groupBoxCorrectiveAction
            // 
            this.groupBoxCorrectiveAction.Controls.Add(this.labelDescriptionControl7);
            this.groupBoxCorrectiveAction.Controls.Add(this.labelDescriptionControl6);
            this.groupBoxCorrectiveAction.Controls.Add(this.textBoxSpecialNotes);
            this.groupBoxCorrectiveAction.Controls.Add(this.textBoxPreventiveAction);
            this.groupBoxCorrectiveAction.Controls.Add(this.label4);
            this.groupBoxCorrectiveAction.Controls.Add(this.buttonAttachmentComment2nd);
            this.groupBoxCorrectiveAction.Controls.Add(this.fileViewControlExComment2nd);
            this.groupBoxCorrectiveAction.Controls.Add(this.textBoxComment2nd);
            this.groupBoxCorrectiveAction.Controls.Add(this.panel1);
            this.groupBoxCorrectiveAction.Controls.Add(this.label7);
            this.groupBoxCorrectiveAction.Controls.Add(this.label8);
            this.groupBoxCorrectiveAction.Controls.Add(this.buttonAttachmentComment1st);
            this.groupBoxCorrectiveAction.Controls.Add(this.fileViewControlExComment1st);
            this.groupBoxCorrectiveAction.Controls.Add(this.textBoxComment1st);
            this.groupBoxCorrectiveAction.Location = new System.Drawing.Point(6, 74);
            this.groupBoxCorrectiveAction.Name = "groupBoxCorrectiveAction";
            this.groupBoxCorrectiveAction.Size = new System.Drawing.Size(1132, 473);
            this.groupBoxCorrectiveAction.TabIndex = 290;
            this.groupBoxCorrectiveAction.TabStop = false;
            this.groupBoxCorrectiveAction.Text = "Corrective Action (是正処置)";
            // 
            // labelDescriptionControl7
            // 
            this.labelDescriptionControl7.AutoSize = true;
            this.labelDescriptionControl7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl7.DescriptionEnabled = true;
            this.labelDescriptionControl7.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl7.DescriptionText = "(特記事項)";
            this.labelDescriptionControl7.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl7.Location = new System.Drawing.Point(585, 343);
            this.labelDescriptionControl7.MainText = "Special\r\nNotes";
            this.labelDescriptionControl7.Name = "labelDescriptionControl7";
            this.labelDescriptionControl7.RequiredFlag = false;
            this.labelDescriptionControl7.Size = new System.Drawing.Size(93, 47);
            this.labelDescriptionControl7.TabIndex = 345;
            // 
            // labelDescriptionControl6
            // 
            this.labelDescriptionControl6.AutoSize = true;
            this.labelDescriptionControl6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl6.DescriptionEnabled = true;
            this.labelDescriptionControl6.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl6.DescriptionText = "(再発防止対策)";
            this.labelDescriptionControl6.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl6.Location = new System.Drawing.Point(6, 340);
            this.labelDescriptionControl6.MainText = "Preventive\r\nAction";
            this.labelDescriptionControl6.Name = "labelDescriptionControl6";
            this.labelDescriptionControl6.RequiredFlag = false;
            this.labelDescriptionControl6.Size = new System.Drawing.Size(123, 47);
            this.labelDescriptionControl6.TabIndex = 345;
            // 
            // textBoxSpecialNotes
            // 
            this.textBoxSpecialNotes.Location = new System.Drawing.Point(684, 343);
            this.textBoxSpecialNotes.Multiline = true;
            this.textBoxSpecialNotes.Name = "textBoxSpecialNotes";
            this.textBoxSpecialNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSpecialNotes.Size = new System.Drawing.Size(439, 120);
            this.textBoxSpecialNotes.TabIndex = 8;
            // 
            // textBoxPreventiveAction
            // 
            this.textBoxPreventiveAction.Location = new System.Drawing.Point(135, 340);
            this.textBoxPreventiveAction.Multiline = true;
            this.textBoxPreventiveAction.Name = "textBoxPreventiveAction";
            this.textBoxPreventiveAction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxPreventiveAction.Size = new System.Drawing.Size(444, 120);
            this.textBoxPreventiveAction.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label4.Location = new System.Drawing.Point(599, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 293;
            this.label4.Text = "添付資料";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonAttachmentComment2nd
            // 
            this.buttonAttachmentComment2nd.Location = new System.Drawing.Point(600, 299);
            this.buttonAttachmentComment2nd.Name = "buttonAttachmentComment2nd";
            this.buttonAttachmentComment2nd.Size = new System.Drawing.Size(54, 23);
            this.buttonAttachmentComment2nd.TabIndex = 5;
            this.buttonAttachmentComment2nd.Tag = "1";
            this.buttonAttachmentComment2nd.Text = "...";
            this.buttonAttachmentComment2nd.UseVisualStyleBackColor = true;
            this.buttonAttachmentComment2nd.Click += new System.EventHandler(this.buttonAttachment_Click);
            // 
            // fileViewControlExComment2nd
            // 
            this.fileViewControlExComment2nd.AllowDrop = true;
            this.fileViewControlExComment2nd.EnableDelete = true;
            this.fileViewControlExComment2nd.EnableDragDrop = true;
            this.fileViewControlExComment2nd.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.fileViewControlExComment2nd.Location = new System.Drawing.Point(684, 202);
            this.fileViewControlExComment2nd.Name = "fileViewControlExComment2nd";
            this.fileViewControlExComment2nd.Size = new System.Drawing.Size(439, 120);
            this.fileViewControlExComment2nd.TabIndex = 6;
            this.fileViewControlExComment2nd.FileItemSelected += new DeficiencyControl.Util.FileViewControlEx.FileItemSelectDelegate(this.fileViewControlEx_FileItemSelected);
            // 
            // textBoxComment2nd
            // 
            this.textBoxComment2nd.Location = new System.Drawing.Point(135, 202);
            this.textBoxComment2nd.Multiline = true;
            this.textBoxComment2nd.Name = "textBoxComment2nd";
            this.textBoxComment2nd.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxComment2nd.Size = new System.Drawing.Size(447, 120);
            this.textBoxComment2nd.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonComment2nd);
            this.panel1.Controls.Add(this.radioButtonComment1st);
            this.panel1.Location = new System.Drawing.Point(3, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(111, 279);
            this.panel1.TabIndex = 0;
            // 
            // radioButtonComment2nd
            // 
            this.radioButtonComment2nd.AutoSize = true;
            this.radioButtonComment2nd.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.radioButtonComment2nd.Location = new System.Drawing.Point(7, 164);
            this.radioButtonComment2nd.Name = "radioButtonComment2nd";
            this.radioButtonComment2nd.Size = new System.Drawing.Size(95, 19);
            this.radioButtonComment2nd.TabIndex = 291;
            this.radioButtonComment2nd.TabStop = true;
            this.radioButtonComment2nd.Text = "2nd コメント";
            this.radioButtonComment2nd.UseVisualStyleBackColor = true;
            // 
            // radioButtonComment1st
            // 
            this.radioButtonComment1st.AutoSize = true;
            this.radioButtonComment1st.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.radioButtonComment1st.Location = new System.Drawing.Point(7, 3);
            this.radioButtonComment1st.Name = "radioButtonComment1st";
            this.radioButtonComment1st.Size = new System.Drawing.Size(92, 19);
            this.radioButtonComment1st.TabIndex = 0;
            this.radioButtonComment1st.TabStop = true;
            this.radioButtonComment1st.Text = "1st コメント";
            this.radioButtonComment1st.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label7.ForeColor = System.Drawing.Color.MediumBlue;
            this.label7.Location = new System.Drawing.Point(6, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(759, 15);
            this.label7.TabIndex = 288;
            this.label7.Text = "▼「検船指摘事項に対する改善報告書」に出力するコメントを指定してください。指定がない場合は、1stコメントが出力されます。";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label8.Location = new System.Drawing.Point(599, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 287;
            this.label8.Text = "添付資料";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonAttachmentComment1st
            // 
            this.buttonAttachmentComment1st.Location = new System.Drawing.Point(600, 134);
            this.buttonAttachmentComment1st.Name = "buttonAttachmentComment1st";
            this.buttonAttachmentComment1st.Size = new System.Drawing.Size(54, 23);
            this.buttonAttachmentComment1st.TabIndex = 2;
            this.buttonAttachmentComment1st.Tag = "0";
            this.buttonAttachmentComment1st.Text = "...";
            this.buttonAttachmentComment1st.UseVisualStyleBackColor = true;
            this.buttonAttachmentComment1st.Click += new System.EventHandler(this.buttonAttachment_Click);
            // 
            // fileViewControlExComment1st
            // 
            this.fileViewControlExComment1st.AllowDrop = true;
            this.fileViewControlExComment1st.EnableDelete = true;
            this.fileViewControlExComment1st.EnableDragDrop = true;
            this.fileViewControlExComment1st.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.fileViewControlExComment1st.Location = new System.Drawing.Point(684, 43);
            this.fileViewControlExComment1st.Name = "fileViewControlExComment1st";
            this.fileViewControlExComment1st.Size = new System.Drawing.Size(439, 114);
            this.fileViewControlExComment1st.TabIndex = 3;
            this.fileViewControlExComment1st.FileItemSelected += new DeficiencyControl.Util.FileViewControlEx.FileItemSelectDelegate(this.fileViewControlEx_FileItemSelected);
            // 
            // textBoxComment1st
            // 
            this.textBoxComment1st.Location = new System.Drawing.Point(135, 43);
            this.textBoxComment1st.Multiline = true;
            this.textBoxComment1st.Name = "textBoxComment1st";
            this.textBoxComment1st.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxComment1st.Size = new System.Drawing.Size(444, 114);
            this.textBoxComment1st.TabIndex = 1;
            // 
            // textBoxRootCause
            // 
            this.textBoxRootCause.Location = new System.Drawing.Point(141, 29);
            this.textBoxRootCause.Name = "textBoxRootCause";
            this.textBoxRootCause.Size = new System.Drawing.Size(444, 23);
            this.textBoxRootCause.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelDescriptionControl1
            // 
            this.labelDescriptionControl1.AutoSize = true;
            this.labelDescriptionControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl1.DescriptionEnabled = true;
            this.labelDescriptionControl1.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl1.DescriptionText = "(状態)";
            this.labelDescriptionControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl1.Location = new System.Drawing.Point(229, 7);
            this.labelDescriptionControl1.MainText = "Status";
            this.labelDescriptionControl1.Name = "labelDescriptionControl1";
            this.labelDescriptionControl1.RequiredFlag = true;
            this.labelDescriptionControl1.Size = new System.Drawing.Size(69, 31);
            this.labelDescriptionControl1.TabIndex = 344;
            // 
            // MoiDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDescriptionControl1);
            this.Controls.Add(this.groupBoxAction);
            this.Controls.Add(this.groupBoxDescription);
            this.Controls.Add(this.statusSelectControlMoiStatus);
            this.Controls.Add(this.textBoxNo);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MoiDetailControl";
            this.Size = new System.Drawing.Size(1150, 948);
            this.Load += new System.EventHandler(this.MoiDetailControl_Load);
            this.groupBoxDescription.ResumeLayout(false);
            this.groupBoxDescription.PerformLayout();
            this.groupBoxAction.ResumeLayout(false);
            this.groupBoxAction.PerformLayout();
            this.groupBoxCorrectiveAction.ResumeLayout(false);
            this.groupBoxCorrectiveAction.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxNo;
        private System.Windows.Forms.Label label1;
        private Controls.StatusSelectControl statusSelectControlMoiStatus;
        private System.Windows.Forms.GroupBox groupBoxDescription;
        private System.Windows.Forms.TextBox textBoxObservation;
        private ViqNoControl viqNoControl1;
        private ViqCodeControl viqCodeControl1;
        private System.Windows.Forms.GroupBox groupBoxAction;
        private System.Windows.Forms.TextBox textBoxRootCause;
        private System.Windows.Forms.GroupBox groupBoxCorrectiveAction;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonAttachmentComment1st;
        private Util.FileViewControlEx fileViewControlExComment1st;
        private System.Windows.Forms.TextBox textBoxComment1st;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonComment1st;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonAttachmentComment2nd;
        private Util.FileViewControlEx fileViewControlExComment2nd;
        private System.Windows.Forms.TextBox textBoxComment2nd;
        private System.Windows.Forms.RadioButton radioButtonComment2nd;
        private System.Windows.Forms.TextBox textBoxSpecialNotes;
        private System.Windows.Forms.TextBox textBoxPreventiveAction;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Controls.LabelDescriptionControl labelDescriptionControl4;
        private Controls.LabelDescriptionControl labelDescriptionControl3;
        private Controls.LabelDescriptionControl labelDescriptionControl2;
        private Controls.LabelDescriptionControl labelDescriptionControl1;
        private Controls.LabelDescriptionControl labelDescriptionControl5;
        private Controls.LabelDescriptionControl labelDescriptionControl6;
        private Controls.LabelDescriptionControl labelDescriptionControl7;
    }
}
