namespace DeficiencyControl.Schedule
{
    partial class ScheduleMainPlanControl
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
            this.flowLayoutPanelControl = new System.Windows.Forms.FlowLayoutPanel();
            this.singleLineComboVessel = new DeficiencyControl.Util.SingleLineCombo();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelDescriptionControl1 = new DeficiencyControl.Controls.LabelDescriptionControl();
            this.SuspendLayout();
            // 
            // flowLayoutPanelControl
            // 
            this.flowLayoutPanelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelControl.AutoScroll = true;
            this.flowLayoutPanelControl.Location = new System.Drawing.Point(3, 91);
            this.flowLayoutPanelControl.Name = "flowLayoutPanelControl";
            this.flowLayoutPanelControl.Size = new System.Drawing.Size(1194, 250);
            this.flowLayoutPanelControl.TabIndex = 4;
            // 
            // singleLineComboVessel
            // 
            this.singleLineComboVessel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.singleLineComboVessel.Location = new System.Drawing.Point(64, 18);
            this.singleLineComboVessel.Margin = new System.Windows.Forms.Padding(4);
            this.singleLineComboVessel.MaxLength = 32767;
            this.singleLineComboVessel.Name = "singleLineComboVessel";
            this.singleLineComboVessel.ReadOnly = false;
            this.singleLineComboVessel.Size = new System.Drawing.Size(330, 23);
            this.singleLineComboVessel.TabIndex = 0;
            this.singleLineComboVessel.selected += new DeficiencyControl.Util.SingleLineCombo.SelectedEventHandler(this.singleLineComboVessel_selected);
            this.singleLineComboVessel.Cleared += new DeficiencyControl.Util.SingleLineCombo.SelectedEventHandler(this.singleLineComboVessel_Cleared);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(401, 3);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(130, 50);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.Text = "Search\r\n(検索)";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(537, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(130, 50);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Add\r\n(追加)";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(673, 3);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(130, 50);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Delete\r\n(削除)";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUpdate.Location = new System.Drawing.Point(537, 347);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(130, 50);
            this.buttonUpdate.TabIndex = 5;
            this.buttonUpdate.Text = "Update\r\n(更新)";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label2.Location = new System.Drawing.Point(31, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 310;
            this.label2.Text = "種別";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label3.Location = new System.Drawing.Point(220, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 310;
            this.label3.Text = "詳細";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label4.Location = new System.Drawing.Point(403, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 310;
            this.label4.Text = "予定日";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label5.Location = new System.Drawing.Point(561, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 310;
            this.label5.Text = "実績日";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label6.Location = new System.Drawing.Point(732, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 310;
            this.label6.Text = "有効期限";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.label7.Location = new System.Drawing.Point(910, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 15);
            this.label7.TabIndex = 310;
            this.label7.Text = "実績メモ";
            // 
            // labelDescriptionControl1
            // 
            this.labelDescriptionControl1.AutoSize = true;
            this.labelDescriptionControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.labelDescriptionControl1.DescriptionEnabled = true;
            this.labelDescriptionControl1.DescriptionFont = new System.Drawing.Font("MS UI Gothic", 11F);
            this.labelDescriptionControl1.DescriptionText = "(船名)";
            this.labelDescriptionControl1.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.labelDescriptionControl1.Location = new System.Drawing.Point(-10, 10);
            this.labelDescriptionControl1.MainText = "Vessel";
            this.labelDescriptionControl1.Name = "labelDescriptionControl1";
            this.labelDescriptionControl1.RequiredFlag = false;
            this.labelDescriptionControl1.Size = new System.Drawing.Size(67, 31);
            this.labelDescriptionControl1.TabIndex = 342;
            // 
            // ScheduleMainPlanControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDescriptionControl1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.singleLineComboVessel);
            this.Controls.Add(this.flowLayoutPanelControl);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ScheduleMainPlanControl";
            this.Size = new System.Drawing.Size(1200, 400);
            this.Load += new System.EventHandler(this.ScheduleMainPlanControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelControl;
        public Util.SingleLineCombo singleLineComboVessel;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private Controls.LabelDescriptionControl labelDescriptionControl1;
    }
}
