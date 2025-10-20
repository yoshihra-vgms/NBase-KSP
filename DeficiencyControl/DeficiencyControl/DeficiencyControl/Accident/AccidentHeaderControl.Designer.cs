namespace DeficiencyControl.Accident
{
    partial class AccidentHeaderControl
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
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.singleLineComboCountry = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboVessel = new DeficiencyControl.Util.SingleLineCombo();
            this.singleLineComboPort = new DeficiencyControl.Util.SingleLineCombo();
            this.comboBoxAccidentKind = new System.Windows.Forms.ComboBox();
            this.singleLineComboUser = new DeficiencyControl.Util.SingleLineCombo();
            this.comboBoxKindOfAccident = new System.Windows.Forms.ComboBox();
            this.comboBoxAccidentSituation = new System.Windows.Forms.ComboBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.labelDescriptionControl1 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl2 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl3 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl4 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl5 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl6 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl7 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl8 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.labelDescriptionControl9 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.panelDateError = new System.Windows.Forms.Panel();
            this.panelDateError.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Location = new System.Drawing.Point(3, 3);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(250, 23);
            this.dateTimePickerDate.TabIndex = 0;
            // 
            // singleLineComboCountry
            // 
            this.singleLineComboCountry.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboCountry.Location = new System.Drawing.Point(505, 118);
            this.singleLineComboCountry.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboCountry.MaxLength = 32767;
            this.singleLineComboCountry.Name = "singleLineComboCountry";
            this.singleLineComboCountry.ReadOnly = false;
            this.singleLineComboCountry.Size = new System.Drawing.Size(250, 23);
            this.singleLineComboCountry.TabIndex = 7;
            // 
            // singleLineComboVessel
            // 
            this.singleLineComboVessel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboVessel.Location = new System.Drawing.Point(101, 118);
            this.singleLineComboVessel.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboVessel.MaxLength = 32767;
            this.singleLineComboVessel.Name = "singleLineComboVessel";
            this.singleLineComboVessel.ReadOnly = false;
            this.singleLineComboVessel.Size = new System.Drawing.Size(250, 23);
            this.singleLineComboVessel.TabIndex = 6;
            // 
            // singleLineComboPort
            // 
            this.singleLineComboPort.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboPort.Location = new System.Drawing.Point(886, 57);
            this.singleLineComboPort.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboPort.MaxLength = 32767;
            this.singleLineComboPort.Name = "singleLineComboPort";
            this.singleLineComboPort.ReadOnly = false;
            this.singleLineComboPort.Size = new System.Drawing.Size(250, 23);
            this.singleLineComboPort.TabIndex = 5;
            // 
            // comboBoxAccidentKind
            // 
            this.comboBoxAccidentKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAccidentKind.Enabled = false;
            this.comboBoxAccidentKind.FormattingEnabled = true;
            this.comboBoxAccidentKind.Location = new System.Drawing.Point(886, 3);
            this.comboBoxAccidentKind.Name = "comboBoxAccidentKind";
            this.comboBoxAccidentKind.Size = new System.Drawing.Size(250, 24);
            this.comboBoxAccidentKind.TabIndex = 2;
            // 
            // singleLineComboUser
            // 
            this.singleLineComboUser.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboUser.Location = new System.Drawing.Point(505, 4);
            this.singleLineComboUser.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboUser.MaxLength = 32767;
            this.singleLineComboUser.Name = "singleLineComboUser";
            this.singleLineComboUser.ReadOnly = false;
            this.singleLineComboUser.Size = new System.Drawing.Size(250, 23);
            this.singleLineComboUser.TabIndex = 1;
            // 
            // comboBoxKindOfAccident
            // 
            this.comboBoxKindOfAccident.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxKindOfAccident.FormattingEnabled = true;
            this.comboBoxKindOfAccident.Location = new System.Drawing.Point(101, 57);
            this.comboBoxKindOfAccident.Name = "comboBoxKindOfAccident";
            this.comboBoxKindOfAccident.Size = new System.Drawing.Size(250, 24);
            this.comboBoxKindOfAccident.TabIndex = 3;
            // 
            // comboBoxAccidentSituation
            // 
            this.comboBoxAccidentSituation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAccidentSituation.FormattingEnabled = true;
            this.comboBoxAccidentSituation.Location = new System.Drawing.Point(505, 57);
            this.comboBoxAccidentSituation.Name = "comboBoxAccidentSituation";
            this.comboBoxAccidentSituation.Size = new System.Drawing.Size(250, 24);
            this.comboBoxAccidentSituation.TabIndex = 4;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(101, 160);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(654, 23);
            this.textBoxTitle.TabIndex = 8;
            // 
            // labelDescriptionControl1
            // 
            this.labelDescriptionControl1.AutoSize = true;
            this.labelDescriptionControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl1.DescriptionEnabled = true;
            this.labelDescriptionControl1.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl1.DescriptionText = "(発生日)";
            this.labelDescriptionControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl1.Location = new System.Drawing.Point(3, 3);
            this.labelDescriptionControl1.MainText = "Date";
            this.labelDescriptionControl1.Name = "labelDescriptionControl1";
            this.labelDescriptionControl1.RequiredFlag = true;
            this.labelDescriptionControl1.Size = new System.Drawing.Size(78, 31);
            this.labelDescriptionControl1.TabIndex = 311;
            // 
            // labelDescriptionControl2
            // 
            this.labelDescriptionControl2.AutoSize = true;
            this.labelDescriptionControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl2.DescriptionEnabled = true;
            this.labelDescriptionControl2.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl2.DescriptionText = "(事故分類)";
            this.labelDescriptionControl2.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl2.Location = new System.Drawing.Point(0, 40);
            this.labelDescriptionControl2.MainText = "Kind of\r\nAccident";
            this.labelDescriptionControl2.Name = "labelDescriptionControl2";
            this.labelDescriptionControl2.RequiredFlag = true;
            this.labelDescriptionControl2.Size = new System.Drawing.Size(93, 47);
            this.labelDescriptionControl2.TabIndex = 311;
            // 
            // labelDescriptionControl3
            // 
            this.labelDescriptionControl3.AutoSize = true;
            this.labelDescriptionControl3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl3.DescriptionEnabled = true;
            this.labelDescriptionControl3.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl3.DescriptionText = "(船名)";
            this.labelDescriptionControl3.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl3.Location = new System.Drawing.Point(0, 110);
            this.labelDescriptionControl3.MainText = "Vessel";
            this.labelDescriptionControl3.Name = "labelDescriptionControl3";
            this.labelDescriptionControl3.RequiredFlag = true;
            this.labelDescriptionControl3.Size = new System.Drawing.Size(67, 31);
            this.labelDescriptionControl3.TabIndex = 312;
            // 
            // labelDescriptionControl4
            // 
            this.labelDescriptionControl4.AutoSize = true;
            this.labelDescriptionControl4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl4.DescriptionEnabled = true;
            this.labelDescriptionControl4.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl4.DescriptionText = "(タイトル)";
            this.labelDescriptionControl4.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl4.Location = new System.Drawing.Point(3, 158);
            this.labelDescriptionControl4.MainText = "Title";
            this.labelDescriptionControl4.Name = "labelDescriptionControl4";
            this.labelDescriptionControl4.RequiredFlag = false;
            this.labelDescriptionControl4.Size = new System.Drawing.Size(77, 31);
            this.labelDescriptionControl4.TabIndex = 312;
            // 
            // labelDescriptionControl5
            // 
            this.labelDescriptionControl5.AutoSize = true;
            this.labelDescriptionControl5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl5.DescriptionEnabled = true;
            this.labelDescriptionControl5.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl5.DescriptionText = "(担当者)";
            this.labelDescriptionControl5.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl5.Location = new System.Drawing.Point(385, 3);
            this.labelDescriptionControl5.MainText = "P.I.C";
            this.labelDescriptionControl5.Name = "labelDescriptionControl5";
            this.labelDescriptionControl5.RequiredFlag = true;
            this.labelDescriptionControl5.Size = new System.Drawing.Size(78, 31);
            this.labelDescriptionControl5.TabIndex = 311;
            // 
            // labelDescriptionControl6
            // 
            this.labelDescriptionControl6.AutoSize = true;
            this.labelDescriptionControl6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl6.DescriptionEnabled = true;
            this.labelDescriptionControl6.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl6.DescriptionText = "(発生状況)";
            this.labelDescriptionControl6.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl6.Location = new System.Drawing.Point(385, 50);
            this.labelDescriptionControl6.MainText = "Situation";
            this.labelDescriptionControl6.Name = "labelDescriptionControl6";
            this.labelDescriptionControl6.RequiredFlag = true;
            this.labelDescriptionControl6.Size = new System.Drawing.Size(93, 31);
            this.labelDescriptionControl6.TabIndex = 311;
            // 
            // labelDescriptionControl7
            // 
            this.labelDescriptionControl7.AutoSize = true;
            this.labelDescriptionControl7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl7.DescriptionEnabled = true;
            this.labelDescriptionControl7.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl7.DescriptionText = "(国名)";
            this.labelDescriptionControl7.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl7.Location = new System.Drawing.Point(385, 110);
            this.labelDescriptionControl7.MainText = "Country";
            this.labelDescriptionControl7.Name = "labelDescriptionControl7";
            this.labelDescriptionControl7.RequiredFlag = true;
            this.labelDescriptionControl7.Size = new System.Drawing.Size(79, 31);
            this.labelDescriptionControl7.TabIndex = 311;
            // 
            // labelDescriptionControl8
            // 
            this.labelDescriptionControl8.AutoSize = true;
            this.labelDescriptionControl8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl8.DescriptionEnabled = true;
            this.labelDescriptionControl8.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl8.DescriptionText = "(種類)";
            this.labelDescriptionControl8.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl8.Location = new System.Drawing.Point(780, 3);
            this.labelDescriptionControl8.MainText = "Kind";
            this.labelDescriptionControl8.Name = "labelDescriptionControl8";
            this.labelDescriptionControl8.RequiredFlag = true;
            this.labelDescriptionControl8.Size = new System.Drawing.Size(63, 31);
            this.labelDescriptionControl8.TabIndex = 311;
            // 
            // labelDescriptionControl9
            // 
            this.labelDescriptionControl9.AutoSize = true;
            this.labelDescriptionControl9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl9.DescriptionEnabled = true;
            this.labelDescriptionControl9.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl9.DescriptionText = "(発生場所)";
            this.labelDescriptionControl9.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl9.Location = new System.Drawing.Point(780, 50);
            this.labelDescriptionControl9.MainText = "Site";
            this.labelDescriptionControl9.Name = "labelDescriptionControl9";
            this.labelDescriptionControl9.RequiredFlag = true;
            this.labelDescriptionControl9.Size = new System.Drawing.Size(93, 31);
            this.labelDescriptionControl9.TabIndex = 311;
            // 
            // panelDateError
            // 
            this.panelDateError.Controls.Add(this.dateTimePickerDate);
            this.panelDateError.Location = new System.Drawing.Point(98, 6);
            this.panelDateError.Name = "panelDateError";
            this.panelDateError.Size = new System.Drawing.Size(256, 29);
            this.panelDateError.TabIndex = 0;
            // 
            // AccidentHeaderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelDateError);
            this.Controls.Add(this.labelDescriptionControl4);
            this.Controls.Add(this.labelDescriptionControl3);
            this.Controls.Add(this.labelDescriptionControl2);
            this.Controls.Add(this.labelDescriptionControl7);
            this.Controls.Add(this.labelDescriptionControl6);
            this.Controls.Add(this.labelDescriptionControl9);
            this.Controls.Add(this.labelDescriptionControl8);
            this.Controls.Add(this.labelDescriptionControl5);
            this.Controls.Add(this.labelDescriptionControl1);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.comboBoxAccidentSituation);
            this.Controls.Add(this.comboBoxKindOfAccident);
            this.Controls.Add(this.singleLineComboUser);
            this.Controls.Add(this.singleLineComboCountry);
            this.Controls.Add(this.singleLineComboVessel);
            this.Controls.Add(this.singleLineComboPort);
            this.Controls.Add(this.comboBoxAccidentKind);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AccidentHeaderControl";
            this.Size = new System.Drawing.Size(1150, 205);
            this.Load += new System.EventHandler(this.AccidentHeaderControl_Load);
            this.panelDateError.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private Util.SingleLineCombo singleLineComboCountry;
        private Util.SingleLineCombo singleLineComboVessel;
        private Util.SingleLineCombo singleLineComboPort;
        private System.Windows.Forms.ComboBox comboBoxAccidentKind;
        private Util.SingleLineCombo singleLineComboUser;
        private System.Windows.Forms.ComboBox comboBoxKindOfAccident;
        private System.Windows.Forms.ComboBox comboBoxAccidentSituation;
        private System.Windows.Forms.TextBox textBoxTitle;
        private Controls.LabelDescriptionControl labelDescriptionControl1;
        private Controls.LabelDescriptionControl labelDescriptionControl2;
        private Controls.LabelDescriptionControl labelDescriptionControl3;
        private Controls.LabelDescriptionControl labelDescriptionControl4;
        private Controls.LabelDescriptionControl labelDescriptionControl5;
        private Controls.LabelDescriptionControl labelDescriptionControl6;
        private Controls.LabelDescriptionControl labelDescriptionControl7;
        private Controls.LabelDescriptionControl labelDescriptionControl8;
        private Controls.LabelDescriptionControl labelDescriptionControl9;
        private System.Windows.Forms.Panel panelDateError;
    }
}
