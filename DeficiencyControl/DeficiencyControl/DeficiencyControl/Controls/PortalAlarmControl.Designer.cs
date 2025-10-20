namespace DeficiencyControl.Controls
{
    partial class PortalAlarmControl
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
            this.dataGridViewAlarm = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.singleLineComboVessel = new DeficiencyControl.Util.SingleLineCombo();
            this.comboBoxScheduleKind = new System.Windows.Forms.ComboBox();
            this.comboBoxScheduleKindDetail = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataCountControl1 = new DeficiencyControl.Controls.DataCountControl();
            this.labelDescriptionControl6 = new DeficiencyControl.Controls.LabelDescriptionControl();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlarm)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAlarm
            // 
            this.dataGridViewAlarm.AllowUserToAddRows = false;
            this.dataGridViewAlarm.AllowUserToDeleteRows = false;
            this.dataGridViewAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAlarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAlarm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column8,
            this.Column7});
            this.dataGridViewAlarm.EnableHeadersVisualStyles = false;
            this.dataGridViewAlarm.Location = new System.Drawing.Point(3, 140);
            this.dataGridViewAlarm.MultiSelect = false;
            this.dataGridViewAlarm.Name = "dataGridViewAlarm";
            this.dataGridViewAlarm.ReadOnly = true;
            this.dataGridViewAlarm.RowHeadersVisible = false;
            this.dataGridViewAlarm.RowTemplate.Height = 21;
            this.dataGridViewAlarm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAlarm.Size = new System.Drawing.Size(1219, 387);
            this.dataGridViewAlarm.TabIndex = 6;
            this.dataGridViewAlarm.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAlarm_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Data";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "No";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 50;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Vessel(船名)";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 200;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Kind(種別)";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 120;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Detail(詳細)";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 180;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Alarm";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 400;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Scheduled\\n(予定日)";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 120;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Expiry Date\\n(有効期限)";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 120;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label3.Location = new System.Drawing.Point(2, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "種別";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(1091, 4);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(130, 50);
            this.buttonSearch.TabIndex = 3;
            this.buttonSearch.Text = "Search\r\n(検索)";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(1092, 60);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(130, 50);
            this.buttonClear.TabIndex = 4;
            this.buttonClear.Text = "Clear\r\n(クリア)";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // singleLineComboVessel
            // 
            this.singleLineComboVessel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboVessel.Location = new System.Drawing.Point(62, 4);
            this.singleLineComboVessel.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboVessel.MaxLength = 32767;
            this.singleLineComboVessel.Name = "singleLineComboVessel";
            this.singleLineComboVessel.ReadOnly = false;
            this.singleLineComboVessel.Size = new System.Drawing.Size(251, 23);
            this.singleLineComboVessel.TabIndex = 0;
            // 
            // comboBoxScheduleKind
            // 
            this.comboBoxScheduleKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleKind.FormattingEnabled = true;
            this.comboBoxScheduleKind.Location = new System.Drawing.Point(62, 43);
            this.comboBoxScheduleKind.Name = "comboBoxScheduleKind";
            this.comboBoxScheduleKind.Size = new System.Drawing.Size(250, 24);
            this.comboBoxScheduleKind.TabIndex = 1;
            this.comboBoxScheduleKind.SelectedIndexChanged += new System.EventHandler(this.comboBoxScheduleKind_SelectedIndexChanged);
            // 
            // comboBoxScheduleKindDetail
            // 
            this.comboBoxScheduleKindDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScheduleKindDetail.FormattingEnabled = true;
            this.comboBoxScheduleKindDetail.Location = new System.Drawing.Point(405, 43);
            this.comboBoxScheduleKindDetail.Name = "comboBoxScheduleKindDetail";
            this.comboBoxScheduleKindDetail.Size = new System.Drawing.Size(250, 24);
            this.comboBoxScheduleKindDetail.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label2.Location = new System.Drawing.Point(346, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "詳細";
            // 
            // dataCountControl1
            // 
            this.dataCountControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataCountControl1.AutoSize = true;
            this.dataCountControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dataCountControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.dataCountControl1.Location = new System.Drawing.Point(1140, 117);
            this.dataCountControl1.Margin = new System.Windows.Forms.Padding(4);
            this.dataCountControl1.Name = "dataCountControl1";
            this.dataCountControl1.Size = new System.Drawing.Size(81, 16);
            this.dataCountControl1.TabIndex = 5;
            // 
            // labelDescriptionControl6
            // 
            this.labelDescriptionControl6.AutoSize = true;
            this.labelDescriptionControl6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl6.DescriptionEnabled = true;
            this.labelDescriptionControl6.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl6.DescriptionText = "(船名)";
            this.labelDescriptionControl6.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl6.Location = new System.Drawing.Point(-14, 1);
            this.labelDescriptionControl6.MainText = "Vessel";
            this.labelDescriptionControl6.Name = "labelDescriptionControl6";
            this.labelDescriptionControl6.RequiredFlag = false;
            this.labelDescriptionControl6.Size = new System.Drawing.Size(67, 31);
            this.labelDescriptionControl6.TabIndex = 291;
            this.labelDescriptionControl6.TabStop = false;
            // 
            // PortalAlarmControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.labelDescriptionControl6);
            this.Controls.Add(this.dataCountControl1);
            this.Controls.Add(this.singleLineComboVessel);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridViewAlarm);
            this.Controls.Add(this.comboBoxScheduleKindDetail);
            this.Controls.Add(this.comboBoxScheduleKind);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Name = "PortalAlarmControl";
            this.Size = new System.Drawing.Size(1225, 530);
            this.Load += new System.EventHandler(this.PortalAlarmControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlarm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAlarm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonClear;
        private Util.SingleLineCombo singleLineComboVessel;
        private System.Windows.Forms.ComboBox comboBoxScheduleKind;
        private System.Windows.Forms.ComboBox comboBoxScheduleKindDetail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private DataCountControl dataCountControl1;
        private LabelDescriptionControl labelDescriptionControl6;

    }
}
