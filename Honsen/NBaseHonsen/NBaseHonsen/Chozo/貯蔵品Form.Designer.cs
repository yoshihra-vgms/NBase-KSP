namespace NBaseHonsen.Chozo
{
    partial class 貯蔵品Form
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
            LidorSystems.IntegralUI.Style.WatermarkImage watermarkImage1 = new LidorSystems.IntegralUI.Style.WatermarkImage();
            this.SearchCondiGroup = new System.Windows.Forms.GroupBox();
            this.SearchMonthCombo = new System.Windows.Forms.ComboBox();
            this.SearchYearCombo = new System.Windows.Forms.ComboBox();
            this.SearchKindCombo = new System.Windows.Forms.ComboBox();
            this.SearchKindLabel = new System.Windows.Forms.Label();
            this.SearchDateLabel = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchVesselCombo = new System.Windows.Forms.ComboBox();
            this.SearchVesselLabel = new System.Windows.Forms.Label();
            this.SearchDateBox = new System.Windows.Forms.MaskedTextBox();
            this.ChozoTreeList = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label入力メッセージ = new System.Windows.Forms.Label();
            this.label月次確定メッセージ = new System.Windows.Forms.Label();
            this.OutputYearButton = new System.Windows.Forms.Button();
            this.OutputButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SearchCondiGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChozoTreeList)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchCondiGroup
            // 
            this.SearchCondiGroup.Controls.Add(this.SearchMonthCombo);
            this.SearchCondiGroup.Controls.Add(this.SearchYearCombo);
            this.SearchCondiGroup.Controls.Add(this.SearchKindCombo);
            this.SearchCondiGroup.Controls.Add(this.SearchKindLabel);
            this.SearchCondiGroup.Controls.Add(this.SearchDateLabel);
            this.SearchCondiGroup.Controls.Add(this.SearchButton);
            this.SearchCondiGroup.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SearchCondiGroup.Location = new System.Drawing.Point(12, 12);
            this.SearchCondiGroup.Name = "SearchCondiGroup";
            this.SearchCondiGroup.Size = new System.Drawing.Size(690, 61);
            this.SearchCondiGroup.TabIndex = 0;
            this.SearchCondiGroup.TabStop = false;
            this.SearchCondiGroup.Text = "検索条件";
            // 
            // SearchMonthCombo
            // 
            this.SearchMonthCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchMonthCombo.FormattingEnabled = true;
            this.SearchMonthCombo.Location = new System.Drawing.Point(361, 22);
            this.SearchMonthCombo.Name = "SearchMonthCombo";
            this.SearchMonthCombo.Size = new System.Drawing.Size(221, 27);
            this.SearchMonthCombo.TabIndex = 9;
            // 
            // SearchYearCombo
            // 
            this.SearchYearCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchYearCombo.FormattingEnabled = true;
            this.SearchYearCombo.Location = new System.Drawing.Point(267, 22);
            this.SearchYearCombo.Name = "SearchYearCombo";
            this.SearchYearCombo.Size = new System.Drawing.Size(88, 27);
            this.SearchYearCombo.TabIndex = 8;
            // 
            // SearchKindCombo
            // 
            this.SearchKindCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchKindCombo.FormattingEnabled = true;
            this.SearchKindCombo.Location = new System.Drawing.Point(59, 22);
            this.SearchKindCombo.Name = "SearchKindCombo";
            this.SearchKindCombo.Size = new System.Drawing.Size(111, 27);
            this.SearchKindCombo.TabIndex = 7;
            // 
            // SearchKindLabel
            // 
            this.SearchKindLabel.AutoSize = true;
            this.SearchKindLabel.Location = new System.Drawing.Point(6, 25);
            this.SearchKindLabel.Name = "SearchKindLabel";
            this.SearchKindLabel.Size = new System.Drawing.Size(49, 19);
            this.SearchKindLabel.TabIndex = 6;
            this.SearchKindLabel.Text = "種別";
            // 
            // SearchDateLabel
            // 
            this.SearchDateLabel.AutoSize = true;
            this.SearchDateLabel.Location = new System.Drawing.Point(176, 25);
            this.SearchDateLabel.Name = "SearchDateLabel";
            this.SearchDateLabel.Size = new System.Drawing.Size(89, 19);
            this.SearchDateLabel.TabIndex = 2;
            this.SearchDateLabel.Text = "対象年月";
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchButton.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SearchButton.Location = new System.Drawing.Point(588, 17);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(96, 35);
            this.SearchButton.TabIndex = 0;
            this.SearchButton.Text = "検索";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchVesselCombo
            // 
            this.SearchVesselCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchVesselCombo.FormattingEnabled = true;
            this.SearchVesselCombo.Location = new System.Drawing.Point(435, 17);
            this.SearchVesselCombo.Name = "SearchVesselCombo";
            this.SearchVesselCombo.Size = new System.Drawing.Size(102, 27);
            this.SearchVesselCombo.TabIndex = 5;
            this.SearchVesselCombo.Visible = false;
            // 
            // SearchVesselLabel
            // 
            this.SearchVesselLabel.AutoSize = true;
            this.SearchVesselLabel.Location = new System.Drawing.Point(435, 20);
            this.SearchVesselLabel.Name = "SearchVesselLabel";
            this.SearchVesselLabel.Size = new System.Drawing.Size(28, 19);
            this.SearchVesselLabel.TabIndex = 4;
            this.SearchVesselLabel.Text = "船";
            this.SearchVesselLabel.Visible = false;
            // 
            // SearchDateBox
            // 
            this.SearchDateBox.Enabled = false;
            this.SearchDateBox.Location = new System.Drawing.Point(435, 3);
            this.SearchDateBox.Mask = "0000/00";
            this.SearchDateBox.Name = "SearchDateBox";
            this.SearchDateBox.Size = new System.Drawing.Size(48, 26);
            this.SearchDateBox.TabIndex = 3;
            this.SearchDateBox.Visible = false;
            // 
            // ChozoTreeList
            // 
            this.ChozoTreeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.ChozoTreeList.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.ChozoTreeList.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.ChozoTreeList.ContentPanel.Name = "";
            this.ChozoTreeList.ContentPanel.Size = new System.Drawing.Size(854, 512);
            this.ChozoTreeList.ContentPanel.TabIndex = 3;
            this.ChozoTreeList.ContentPanel.TabStop = false;
            this.ChozoTreeList.Cursor = System.Windows.Forms.Cursors.Default;
            this.ChozoTreeList.Footer = false;
            this.ChozoTreeList.Location = new System.Drawing.Point(12, 132);
            this.ChozoTreeList.Name = "ChozoTreeList";
            this.ChozoTreeList.Size = new System.Drawing.Size(860, 518);
            this.ChozoTreeList.TabIndex = 1;
            this.ChozoTreeList.Text = "treeListView1";
            this.ChozoTreeList.WatermarkImage = watermarkImage1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label入力メッセージ);
            this.panel1.Controls.Add(this.label月次確定メッセージ);
            this.panel1.Controls.Add(this.OutputYearButton);
            this.panel1.Controls.Add(this.OutputButton);
            this.panel1.Controls.Add(this.SaveButton);
            this.panel1.Controls.Add(this.SearchDateBox);
            this.panel1.Controls.Add(this.SearchVesselLabel);
            this.panel1.Controls.Add(this.SearchVesselCombo);
            this.panel1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel1.Location = new System.Drawing.Point(12, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 47);
            this.panel1.TabIndex = 2;
            // 
            // label入力メッセージ
            // 
            this.label入力メッセージ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label入力メッセージ.AutoSize = true;
            this.label入力メッセージ.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label入力メッセージ.Location = new System.Drawing.Point(649, 20);
            this.label入力メッセージ.Name = "label入力メッセージ";
            this.label入力メッセージ.Size = new System.Drawing.Size(165, 16);
            this.label入力メッセージ.TabIndex = 8;
            this.label入力メッセージ.Text = "残量を入力してください";
            this.label入力メッセージ.Visible = false;
            // 
            // label月次確定メッセージ
            // 
            this.label月次確定メッセージ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label月次確定メッセージ.AutoSize = true;
            this.label月次確定メッセージ.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label月次確定メッセージ.ForeColor = System.Drawing.Color.Red;
            this.label月次確定メッセージ.Location = new System.Drawing.Point(585, 20);
            this.label月次確定メッセージ.Name = "label月次確定メッセージ";
            this.label月次確定メッセージ.Size = new System.Drawing.Size(253, 16);
            this.label月次確定メッセージ.TabIndex = 7;
            this.label月次確定メッセージ.Text = "月次確定済みのため編集できません";
            this.label月次確定メッセージ.Visible = false;
            // 
            // OutputYearButton
            // 
            this.OutputYearButton.BackColor = System.Drawing.SystemColors.Control;
            this.OutputYearButton.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OutputYearButton.Location = new System.Drawing.Point(260, 10);
            this.OutputYearButton.Name = "OutputYearButton";
            this.OutputYearButton.Size = new System.Drawing.Size(169, 30);
            this.OutputYearButton.TabIndex = 6;
            this.OutputYearButton.Text = "年間管理票出力";
            this.OutputYearButton.UseVisualStyleBackColor = false;
            this.OutputYearButton.Visible = false;
            this.OutputYearButton.Click += new System.EventHandler(this.OutputYearButton_Click);
            // 
            // OutputButton
            // 
            this.OutputButton.BackColor = System.Drawing.SystemColors.Control;
            this.OutputButton.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OutputButton.Location = new System.Drawing.Point(130, 10);
            this.OutputButton.Name = "OutputButton";
            this.OutputButton.Size = new System.Drawing.Size(124, 30);
            this.OutputButton.TabIndex = 1;
            this.OutputButton.Text = "管理票出力";
            this.OutputButton.UseVisualStyleBackColor = false;
            this.OutputButton.Click += new System.EventHandler(this.OutputButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.SystemColors.Control;
            this.SaveButton.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SaveButton.Location = new System.Drawing.Point(8, 10);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(112, 30);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "保存";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // 貯蔵品Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 662);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ChozoTreeList);
            this.Controls.Add(this.SearchCondiGroup);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "貯蔵品Form";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.貯蔵品Form_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.貯蔵品Form_FormClosed);
            this.Load += new System.EventHandler(this.貯蔵品Form_Load);
            this.SearchCondiGroup.ResumeLayout(false);
            this.SearchCondiGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChozoTreeList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SearchCondiGroup;
        private LidorSystems.IntegralUI.Lists.TreeListView ChozoTreeList;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.MaskedTextBox SearchDateBox;
        private System.Windows.Forms.Label SearchDateLabel;
        private System.Windows.Forms.ComboBox SearchVesselCombo;
        private System.Windows.Forms.Label SearchVesselLabel;
        private System.Windows.Forms.ComboBox SearchKindCombo;
        private System.Windows.Forms.Label SearchKindLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button OutputButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.ComboBox SearchMonthCombo;
        private System.Windows.Forms.ComboBox SearchYearCombo;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button OutputYearButton;
        private System.Windows.Forms.Label label入力メッセージ;
        private System.Windows.Forms.Label label月次確定メッセージ;

    }
}