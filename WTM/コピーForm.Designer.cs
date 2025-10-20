
namespace WTM
{
    partial class コピーForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(コピーForm));
            this.panel_Message = new System.Windows.Forms.Panel();
            this.label_Message = new System.Windows.Forms.Label();
            this.button_確認 = new System.Windows.Forms.Button();
            this.panel_Base = new System.Windows.Forms.Panel();
            this.groupBoxRankCategory = new System.Windows.Forms.GroupBox();
            this.comboBoxRankCategory = new System.Windows.Forms.ComboBox();
            this.c1List1 = new C1.Win.C1List.C1List();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label_SrcWorkRange = new System.Windows.Forms.Label();
            this.label_SrcCrew = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel_FinishDay = new System.Windows.Forms.Panel();
            this.label_FinishYear = new System.Windows.Forms.Label();
            this.label_FinishDay = new System.Windows.Forms.Label();
            this.monthCalendar2 = new System.Windows.Forms.MonthCalendar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel_StartDay = new System.Windows.Forms.Panel();
            this.label_StartYear = new System.Windows.Forms.Label();
            this.label_StartDay = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.button_Check = new System.Windows.Forms.Button();
            this.panel_Message.SuspendLayout();
            this.panel_Base.SuspendLayout();
            this.groupBoxRankCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1List1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel_FinishDay.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel_StartDay.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Message
            // 
            this.panel_Message.BackColor = System.Drawing.Color.White;
            this.panel_Message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Message.Controls.Add(this.label_Message);
            this.panel_Message.Controls.Add(this.button_確認);
            this.panel_Message.Location = new System.Drawing.Point(273, 211);
            this.panel_Message.Name = "panel_Message";
            this.panel_Message.Size = new System.Drawing.Size(311, 133);
            this.panel_Message.TabIndex = 16;
            // 
            // label_Message
            // 
            this.label_Message.AutoSize = true;
            this.label_Message.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Message.Location = new System.Drawing.Point(93, 41);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(124, 16);
            this.label_Message.TabIndex = 11;
            this.label_Message.Text = "お疲れ様でした。";
            // 
            // button_確認
            // 
            this.button_確認.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button_確認.BackColor = System.Drawing.SystemColors.Control;
            this.button_確認.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_確認.Location = new System.Drawing.Point(96, 87);
            this.button_確認.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button_確認.Name = "button_確認";
            this.button_確認.Size = new System.Drawing.Size(117, 31);
            this.button_確認.TabIndex = 10;
            this.button_確認.Text = "確認";
            this.button_確認.UseVisualStyleBackColor = false;
            this.button_確認.Click += new System.EventHandler(this.button_確認_Click);
            // 
            // panel_Base
            // 
            this.panel_Base.Controls.Add(this.button_Check);
            this.panel_Base.Controls.Add(this.groupBoxRankCategory);
            this.panel_Base.Controls.Add(this.c1List1);
            this.panel_Base.Controls.Add(this.buttonCopy);
            this.panel_Base.Controls.Add(this.buttonCancel);
            this.panel_Base.Controls.Add(this.label_SrcWorkRange);
            this.panel_Base.Controls.Add(this.label_SrcCrew);
            this.panel_Base.Controls.Add(this.groupBox2);
            this.panel_Base.Controls.Add(this.groupBox1);
            this.panel_Base.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Base.Location = new System.Drawing.Point(0, 0);
            this.panel_Base.Name = "panel_Base";
            this.panel_Base.Size = new System.Drawing.Size(857, 554);
            this.panel_Base.TabIndex = 17;
            // 
            // groupBoxRankCategory
            // 
            this.groupBoxRankCategory.Controls.Add(this.comboBoxRankCategory);
            this.groupBoxRankCategory.Location = new System.Drawing.Point(126, 105);
            this.groupBoxRankCategory.Name = "groupBoxRankCategory";
            this.groupBoxRankCategory.Size = new System.Drawing.Size(168, 40);
            this.groupBoxRankCategory.TabIndex = 23;
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
            // c1List1
            // 
            this.c1List1.AllowColMove = false;
            this.c1List1.AllowSort = false;
            this.c1List1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.c1List1.Caption = "コピー先船員";
            this.c1List1.CaptionHeight = 0;
            this.c1List1.ColumnCaptionHeight = 25;
            this.c1List1.ColumnFooterHeight = 20;
            this.c1List1.ColumnWidth = 100;
            this.c1List1.DeadAreaBackColor = System.Drawing.SystemColors.ControlDark;
            this.c1List1.Images.Add(((System.Drawing.Image)(resources.GetObject("c1List1.Images"))));
            this.c1List1.ItemHeight = 22;
            this.c1List1.Location = new System.Drawing.Point(59, 163);
            this.c1List1.MatchEntryTimeout = ((long)(2000));
            this.c1List1.Name = "c1List1";
            this.c1List1.PreviewInfo.Caption = "印刷プレビューウィンドウ";
            this.c1List1.PreviewInfo.Location = new System.Drawing.Point(0, 0);
            this.c1List1.PreviewInfo.Size = new System.Drawing.Size(0, 0);
            this.c1List1.PreviewInfo.ZoomFactor = 75D;
            this.c1List1.RowSubDividerColor = System.Drawing.Color.DarkGray;
            this.c1List1.SelectionMode = C1.Win.C1List.SelectionModeEnum.CheckBox;
            this.c1List1.Size = new System.Drawing.Size(235, 242);
            this.c1List1.TabIndex = 22;
            this.c1List1.Text = "コピー先船員";
            this.c1List1.PropBag = resources.GetString("c1List1.PropBag");
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCopy.BackColor = System.Drawing.Color.Orange;
            this.buttonCopy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCopy.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonCopy.ForeColor = System.Drawing.Color.White;
            this.buttonCopy.Location = new System.Drawing.Point(275, 484);
            this.buttonCopy.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(138, 36);
            this.buttonCopy.TabIndex = 20;
            this.buttonCopy.Text = "コピー";
            this.buttonCopy.UseVisualStyleBackColor = false;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCancel.BackColor = System.Drawing.Color.HotPink;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(437, 484);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(138, 36);
            this.buttonCancel.TabIndex = 21;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label_SrcWorkRange
            // 
            this.label_SrcWorkRange.AutoSize = true;
            this.label_SrcWorkRange.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_SrcWorkRange.Location = new System.Drawing.Point(175, 55);
            this.label_SrcWorkRange.Name = "label_SrcWorkRange";
            this.label_SrcWorkRange.Size = new System.Drawing.Size(67, 24);
            this.label_SrcWorkRange.TabIndex = 18;
            this.label_SrcWorkRange.Text = "label1";
            // 
            // label_SrcCrew
            // 
            this.label_SrcCrew.AutoSize = true;
            this.label_SrcCrew.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_SrcCrew.Location = new System.Drawing.Point(175, 25);
            this.label_SrcCrew.Name = "label_SrcCrew";
            this.label_SrcCrew.Size = new System.Drawing.Size(67, 24);
            this.label_SrcCrew.TabIndex = 19;
            this.label_SrcCrew.Text = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel_FinishDay);
            this.groupBox2.Controls.Add(this.monthCalendar2);
            this.groupBox2.Location = new System.Drawing.Point(554, 163);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(236, 242);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "終了日";
            // 
            // panel_FinishDay
            // 
            this.panel_FinishDay.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel_FinishDay.Controls.Add(this.label_FinishYear);
            this.panel_FinishDay.Controls.Add(this.label_FinishDay);
            this.panel_FinishDay.Location = new System.Drawing.Point(19, 19);
            this.panel_FinishDay.Name = "panel_FinishDay";
            this.panel_FinishDay.Size = new System.Drawing.Size(199, 50);
            this.panel_FinishDay.TabIndex = 2;
            // 
            // label_FinishYear
            // 
            this.label_FinishYear.AutoSize = true;
            this.label_FinishYear.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_FinishYear.ForeColor = System.Drawing.Color.White;
            this.label_FinishYear.Location = new System.Drawing.Point(27, 3);
            this.label_FinishYear.Name = "label_FinishYear";
            this.label_FinishYear.Size = new System.Drawing.Size(46, 16);
            this.label_FinishYear.TabIndex = 1;
            this.label_FinishYear.Text = "label2";
            // 
            // label_FinishDay
            // 
            this.label_FinishDay.AutoSize = true;
            this.label_FinishDay.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_FinishDay.ForeColor = System.Drawing.Color.White;
            this.label_FinishDay.Location = new System.Drawing.Point(26, 23);
            this.label_FinishDay.Name = "label_FinishDay";
            this.label_FinishDay.Size = new System.Drawing.Size(67, 24);
            this.label_FinishDay.TabIndex = 0;
            this.label_FinishDay.Text = "label1";
            // 
            // monthCalendar2
            // 
            this.monthCalendar2.Location = new System.Drawing.Point(19, 68);
            this.monthCalendar2.MaxSelectionCount = 1;
            this.monthCalendar2.Name = "monthCalendar2";
            this.monthCalendar2.TabIndex = 0;
            this.monthCalendar2.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar2_DateChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel_StartDay);
            this.groupBox1.Controls.Add(this.monthCalendar1);
            this.groupBox1.Location = new System.Drawing.Point(312, 163);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 242);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "開始日";
            // 
            // panel_StartDay
            // 
            this.panel_StartDay.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel_StartDay.Controls.Add(this.label_StartYear);
            this.panel_StartDay.Controls.Add(this.label_StartDay);
            this.panel_StartDay.Location = new System.Drawing.Point(19, 19);
            this.panel_StartDay.Name = "panel_StartDay";
            this.panel_StartDay.Size = new System.Drawing.Size(199, 50);
            this.panel_StartDay.TabIndex = 1;
            // 
            // label_StartYear
            // 
            this.label_StartYear.AutoSize = true;
            this.label_StartYear.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_StartYear.ForeColor = System.Drawing.Color.White;
            this.label_StartYear.Location = new System.Drawing.Point(27, 3);
            this.label_StartYear.Name = "label_StartYear";
            this.label_StartYear.Size = new System.Drawing.Size(46, 16);
            this.label_StartYear.TabIndex = 0;
            this.label_StartYear.Text = "label1";
            // 
            // label_StartDay
            // 
            this.label_StartDay.AutoSize = true;
            this.label_StartDay.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_StartDay.ForeColor = System.Drawing.Color.White;
            this.label_StartDay.Location = new System.Drawing.Point(26, 23);
            this.label_StartDay.Name = "label_StartDay";
            this.label_StartDay.Size = new System.Drawing.Size(67, 24);
            this.label_StartDay.TabIndex = 0;
            this.label_StartDay.Text = "label1";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(19, 68);
            this.monthCalendar1.MaxSelectionCount = 1;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // button_Check
            // 
            this.button_Check.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Check.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_Check.Location = new System.Drawing.Point(61, 168);
            this.button_Check.Margin = new System.Windows.Forms.Padding(0);
            this.button_Check.Name = "button_Check";
            this.button_Check.Size = new System.Drawing.Size(14, 18);
            this.button_Check.TabIndex = 24;
            this.button_Check.UseVisualStyleBackColor = true;
            this.button_Check.Click += new System.EventHandler(this.button_Check_Click);
            // 
            // コピーForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(857, 554);
            this.Controls.Add(this.panel_Message);
            this.Controls.Add(this.panel_Base);
            this.Name = "コピーForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "勤務情報コピー";
            this.Load += new System.EventHandler(this.コピーForm_Load);
            this.panel_Message.ResumeLayout(false);
            this.panel_Message.PerformLayout();
            this.panel_Base.ResumeLayout(false);
            this.panel_Base.PerformLayout();
            this.groupBoxRankCategory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1List1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel_FinishDay.ResumeLayout(false);
            this.panel_FinishDay.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel_StartDay.ResumeLayout(false);
            this.panel_StartDay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_Message;
        private System.Windows.Forms.Label label_Message;
        private System.Windows.Forms.Button button_確認;
        private System.Windows.Forms.Panel panel_Base;
        private System.Windows.Forms.GroupBox groupBoxRankCategory;
        private System.Windows.Forms.ComboBox comboBoxRankCategory;
        private C1.Win.C1List.C1List c1List1;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label_SrcWorkRange;
        private System.Windows.Forms.Label label_SrcCrew;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel_FinishDay;
        private System.Windows.Forms.Label label_FinishYear;
        private System.Windows.Forms.Label label_FinishDay;
        private System.Windows.Forms.MonthCalendar monthCalendar2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel_StartDay;
        private System.Windows.Forms.Label label_StartYear;
        private System.Windows.Forms.Label label_StartDay;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Button button_Check;
    }
}