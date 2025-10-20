namespace Hachu.Chozo
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
            this.SearchVesselCombo = new System.Windows.Forms.ComboBox();
            this.SearchVesselLabel = new System.Windows.Forms.Label();
            this.SearchDateLabel = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.ChozoTreeList = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label入力メッセージ = new System.Windows.Forms.Label();
            this.label月次確定メッセージ = new System.Windows.Forms.Label();
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
            this.SearchCondiGroup.Controls.Add(this.SearchVesselCombo);
            this.SearchCondiGroup.Controls.Add(this.SearchVesselLabel);
            this.SearchCondiGroup.Controls.Add(this.SearchDateLabel);
            this.SearchCondiGroup.Controls.Add(this.SearchButton);
            this.SearchCondiGroup.Location = new System.Drawing.Point(12, 12);
            this.SearchCondiGroup.Name = "SearchCondiGroup";
            this.SearchCondiGroup.Size = new System.Drawing.Size(687, 45);
            this.SearchCondiGroup.TabIndex = 0;
            this.SearchCondiGroup.TabStop = false;
            this.SearchCondiGroup.Text = "検索条件";
            // 
            // SearchMonthCombo
            // 
            this.SearchMonthCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchMonthCombo.FormattingEnabled = true;
            this.SearchMonthCombo.Location = new System.Drawing.Point(256, 16);
            this.SearchMonthCombo.Name = "SearchMonthCombo";
            this.SearchMonthCombo.Size = new System.Drawing.Size(145, 20);
            this.SearchMonthCombo.TabIndex = 9;
            // 
            // SearchYearCombo
            // 
            this.SearchYearCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchYearCombo.FormattingEnabled = true;
            this.SearchYearCombo.Location = new System.Drawing.Point(185, 16);
            this.SearchYearCombo.Name = "SearchYearCombo";
            this.SearchYearCombo.Size = new System.Drawing.Size(65, 20);
            this.SearchYearCombo.TabIndex = 8;
            // 
            // SearchKindCombo
            // 
            this.SearchKindCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchKindCombo.FormattingEnabled = true;
            this.SearchKindCombo.Location = new System.Drawing.Point(41, 16);
            this.SearchKindCombo.Name = "SearchKindCombo";
            this.SearchKindCombo.Size = new System.Drawing.Size(79, 20);
            this.SearchKindCombo.TabIndex = 7;
            // 
            // SearchKindLabel
            // 
            this.SearchKindLabel.AutoSize = true;
            this.SearchKindLabel.Location = new System.Drawing.Point(6, 20);
            this.SearchKindLabel.Name = "SearchKindLabel";
            this.SearchKindLabel.Size = new System.Drawing.Size(29, 12);
            this.SearchKindLabel.TabIndex = 6;
            this.SearchKindLabel.Text = "種別";
            // 
            // SearchVesselCombo
            // 
            this.SearchVesselCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SearchVesselCombo.FormattingEnabled = true;
            this.SearchVesselCombo.Location = new System.Drawing.Point(430, 16);
            this.SearchVesselCombo.Name = "SearchVesselCombo";
            this.SearchVesselCombo.Size = new System.Drawing.Size(170, 20);
            this.SearchVesselCombo.TabIndex = 5;
            // 
            // SearchVesselLabel
            // 
            this.SearchVesselLabel.AutoSize = true;
            this.SearchVesselLabel.Location = new System.Drawing.Point(407, 20);
            this.SearchVesselLabel.Name = "SearchVesselLabel";
            this.SearchVesselLabel.Size = new System.Drawing.Size(17, 12);
            this.SearchVesselLabel.TabIndex = 4;
            this.SearchVesselLabel.Text = "船";
            // 
            // SearchDateLabel
            // 
            this.SearchDateLabel.AutoSize = true;
            this.SearchDateLabel.Location = new System.Drawing.Point(126, 20);
            this.SearchDateLabel.Name = "SearchDateLabel";
            this.SearchDateLabel.Size = new System.Drawing.Size(53, 12);
            this.SearchDateLabel.TabIndex = 2;
            this.SearchDateLabel.Text = "対象年月";
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.SystemColors.Control;
            this.SearchButton.Location = new System.Drawing.Point(606, 14);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 0;
            this.SearchButton.Text = "検索";
            this.SearchButton.UseVisualStyleBackColor = false;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
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
            this.ChozoTreeList.ContentPanel.Size = new System.Drawing.Size(681, 336);
            this.ChozoTreeList.ContentPanel.TabIndex = 3;
            this.ChozoTreeList.ContentPanel.TabStop = false;
            this.ChozoTreeList.Cursor = System.Windows.Forms.Cursors.Default;
            this.ChozoTreeList.Footer = false;
            this.ChozoTreeList.Location = new System.Drawing.Point(12, 110);
            this.ChozoTreeList.Name = "ChozoTreeList";
            this.ChozoTreeList.Size = new System.Drawing.Size(687, 342);
            this.ChozoTreeList.TabIndex = 1;
            this.ChozoTreeList.Text = "treeListView1";
            this.ChozoTreeList.WatermarkImage = watermarkImage1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label入力メッセージ);
            this.panel1.Controls.Add(this.label月次確定メッセージ);
            this.panel1.Controls.Add(this.OutputButton);
            this.panel1.Controls.Add(this.SaveButton);
            this.panel1.Location = new System.Drawing.Point(12, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(687, 41);
            this.panel1.TabIndex = 2;
            // 
            // label入力メッセージ
            // 
            this.label入力メッセージ.AutoSize = true;
            this.label入力メッセージ.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label入力メッセージ.Location = new System.Drawing.Point(533, 15);
            this.label入力メッセージ.Name = "label入力メッセージ";
            this.label入力メッセージ.Size = new System.Drawing.Size(124, 12);
            this.label入力メッセージ.TabIndex = 5;
            this.label入力メッセージ.Text = "残量を入力してください";
            this.label入力メッセージ.Visible = false;
            // 
            // label月次確定メッセージ
            // 
            this.label月次確定メッセージ.AutoSize = true;
            this.label月次確定メッセージ.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label月次確定メッセージ.ForeColor = System.Drawing.Color.Red;
            this.label月次確定メッセージ.Location = new System.Drawing.Point(484, 15);
            this.label月次確定メッセージ.Name = "label月次確定メッセージ";
            this.label月次確定メッセージ.Size = new System.Drawing.Size(193, 12);
            this.label月次確定メッセージ.TabIndex = 0;
            this.label月次確定メッセージ.Text = "月次確定済みのため編集できません";
            this.label月次確定メッセージ.Visible = false;
            // 
            // OutputButton
            // 
            this.OutputButton.BackColor = System.Drawing.SystemColors.Control;
            this.OutputButton.Location = new System.Drawing.Point(89, 10);
            this.OutputButton.Name = "OutputButton";
            this.OutputButton.Size = new System.Drawing.Size(75, 23);
            this.OutputButton.TabIndex = 1;
            this.OutputButton.Text = "管理票出力";
            this.OutputButton.UseVisualStyleBackColor = false;
            this.OutputButton.Click += new System.EventHandler(this.OutputButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.SystemColors.Control;
            this.SaveButton.Location = new System.Drawing.Point(8, 10);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "保存";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // 貯蔵品Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(711, 464);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ChozoTreeList);
            this.Controls.Add(this.SearchCondiGroup);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "貯蔵品Form";
            this.Text = "貯蔵品編集";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.貯蔵品Form_FormClosing);
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
        private System.Windows.Forms.Label label月次確定メッセージ;
        private System.Windows.Forms.Label label入力メッセージ;

    }
}