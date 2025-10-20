
namespace Senin.util
{
    partial class NightSettingPanelUserControl
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            NBaseData.DAC.SiNightSetting siNightSetting1 = new NBaseData.DAC.SiNightSetting();
            NBaseData.DAC.SiNightSetting siNightSetting2 = new NBaseData.DAC.SiNightSetting();
            NBaseData.DAC.SiNightSetting siNightSetting3 = new NBaseData.DAC.SiNightSetting();
            NBaseData.DAC.SiNightSetting siNightSetting4 = new NBaseData.DAC.SiNightSetting();
            NBaseData.DAC.SiNightSetting siNightSetting5 = new NBaseData.DAC.SiNightSetting();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBox_所属会社 = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.nightSettingUserControl1 = new Senin.util.NightSettingDetailUserControl();
            this.nightSettingUserControl2 = new Senin.util.NightSettingDetailUserControl();
            this.nightSettingUserControl3 = new Senin.util.NightSettingDetailUserControl();
            this.nightSettingUserControl4 = new Senin.util.NightSettingDetailUserControl();
            this.nightSettingUserControl5 = new Senin.util.NightSettingDetailUserControl();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.nightSettingUserControl1);
            this.flowLayoutPanel1.Controls.Add(this.nightSettingUserControl2);
            this.flowLayoutPanel1.Controls.Add(this.nightSettingUserControl3);
            this.flowLayoutPanel1.Controls.Add(this.nightSettingUserControl4);
            this.flowLayoutPanel1.Controls.Add(this.nightSettingUserControl5);
            this.flowLayoutPanel1.Enabled = false;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(172, 25);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(342, 155);
            this.flowLayoutPanel1.TabIndex = 71;
            // 
            // comboBox_所属会社
            // 
            this.comboBox_所属会社.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_所属会社.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox_所属会社.FormattingEnabled = true;
            this.comboBox_所属会社.Location = new System.Drawing.Point(0, 30);
            this.comboBox_所属会社.Name = "comboBox_所属会社";
            this.comboBox_所属会社.Size = new System.Drawing.Size(162, 20);
            this.comboBox_所属会社.TabIndex = 68;
            this.comboBox_所属会社.TextChanged += new System.EventHandler(this.comboBox_所属会社_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.Location = new System.Drawing.Point(246, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(17, 12);
            this.label16.TabIndex = 69;
            this.label16.Text = "船";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.Location = new System.Drawing.Point(53, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 70;
            this.label14.Text = "所属会社";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Enabled = false;
            this.buttonAdd.Location = new System.Drawing.Point(439, 0);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 72;
            this.buttonAdd.Text = "追加";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // nightSettingUserControl1
            // 
            this.nightSettingUserControl1.Location = new System.Drawing.Point(3, 3);
            this.nightSettingUserControl1.Name = "nightSettingUserControl1";
            siNightSetting1.DataNo = ((long)(0));
            siNightSetting1.DeleteFlag = 0;
            siNightSetting1.EndTime = 0;
            siNightSetting1.MsSeninCompanyID = null;
            siNightSetting1.MsVesselID = -1;
            siNightSetting1.RenewDate = new System.DateTime(((long)(0)));
            siNightSetting1.RenewUserID = null;
            siNightSetting1.SendFlag = 0;
            siNightSetting1.SiNightSettingID = null;
            siNightSetting1.StartTime = 0;
            siNightSetting1.Ts = null;
            siNightSetting1.UserKey = null;
            siNightSetting1.VesselID = 0;
            this.nightSettingUserControl1.NightSetting = siNightSetting1;
            this.nightSettingUserControl1.Size = new System.Drawing.Size(297, 25);
            this.nightSettingUserControl1.TabIndex = 0;
            // 
            // nightSettingUserControl2
            // 
            this.nightSettingUserControl2.Location = new System.Drawing.Point(3, 34);
            this.nightSettingUserControl2.Name = "nightSettingUserControl2";
            siNightSetting2.DataNo = ((long)(0));
            siNightSetting2.DeleteFlag = 0;
            siNightSetting2.EndTime = 0;
            siNightSetting2.MsSeninCompanyID = null;
            siNightSetting2.MsVesselID = -1;
            siNightSetting2.RenewDate = new System.DateTime(((long)(0)));
            siNightSetting2.RenewUserID = null;
            siNightSetting2.SendFlag = 0;
            siNightSetting2.SiNightSettingID = null;
            siNightSetting2.StartTime = 0;
            siNightSetting2.Ts = null;
            siNightSetting2.UserKey = null;
            siNightSetting2.VesselID = 0;
            this.nightSettingUserControl2.NightSetting = siNightSetting2;
            this.nightSettingUserControl2.Size = new System.Drawing.Size(297, 25);
            this.nightSettingUserControl2.TabIndex = 0;
            // 
            // nightSettingUserControl3
            // 
            this.nightSettingUserControl3.Location = new System.Drawing.Point(3, 65);
            this.nightSettingUserControl3.Name = "nightSettingUserControl3";
            siNightSetting3.DataNo = ((long)(0));
            siNightSetting3.DeleteFlag = 0;
            siNightSetting3.EndTime = 0;
            siNightSetting3.MsSeninCompanyID = null;
            siNightSetting3.MsVesselID = -1;
            siNightSetting3.RenewDate = new System.DateTime(((long)(0)));
            siNightSetting3.RenewUserID = null;
            siNightSetting3.SendFlag = 0;
            siNightSetting3.SiNightSettingID = null;
            siNightSetting3.StartTime = 0;
            siNightSetting3.Ts = null;
            siNightSetting3.UserKey = null;
            siNightSetting3.VesselID = 0;
            this.nightSettingUserControl3.NightSetting = siNightSetting3;
            this.nightSettingUserControl3.Size = new System.Drawing.Size(297, 25);
            this.nightSettingUserControl3.TabIndex = 0;
            // 
            // nightSettingUserControl4
            // 
            this.nightSettingUserControl4.Location = new System.Drawing.Point(3, 96);
            this.nightSettingUserControl4.Name = "nightSettingUserControl4";
            siNightSetting4.DataNo = ((long)(0));
            siNightSetting4.DeleteFlag = 0;
            siNightSetting4.EndTime = 0;
            siNightSetting4.MsSeninCompanyID = null;
            siNightSetting4.MsVesselID = -1;
            siNightSetting4.RenewDate = new System.DateTime(((long)(0)));
            siNightSetting4.RenewUserID = null;
            siNightSetting4.SendFlag = 0;
            siNightSetting4.SiNightSettingID = null;
            siNightSetting4.StartTime = 0;
            siNightSetting4.Ts = null;
            siNightSetting4.UserKey = null;
            siNightSetting4.VesselID = 0;
            this.nightSettingUserControl4.NightSetting = siNightSetting4;
            this.nightSettingUserControl4.Size = new System.Drawing.Size(297, 25);
            this.nightSettingUserControl4.TabIndex = 0;
            // 
            // nightSettingUserControl5
            // 
            this.nightSettingUserControl5.Location = new System.Drawing.Point(3, 127);
            this.nightSettingUserControl5.Name = "nightSettingUserControl5";
            siNightSetting5.DataNo = ((long)(0));
            siNightSetting5.DeleteFlag = 0;
            siNightSetting5.EndTime = 0;
            siNightSetting5.MsSeninCompanyID = null;
            siNightSetting5.MsVesselID = -1;
            siNightSetting5.RenewDate = new System.DateTime(((long)(0)));
            siNightSetting5.RenewUserID = null;
            siNightSetting5.SendFlag = 0;
            siNightSetting5.SiNightSettingID = null;
            siNightSetting5.StartTime = 0;
            siNightSetting5.Ts = null;
            siNightSetting5.UserKey = null;
            siNightSetting5.VesselID = 0;
            this.nightSettingUserControl5.NightSetting = siNightSetting5;
            this.nightSettingUserControl5.Size = new System.Drawing.Size(297, 25);
            this.nightSettingUserControl5.TabIndex = 0;
            // 
            // NightSettingPanelUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.comboBox_所属会社);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label14);
            this.Name = "NightSettingPanelUserControl";
            this.Size = new System.Drawing.Size(517, 183);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private NightSettingDetailUserControl nightSettingUserControl1;
        private NightSettingDetailUserControl nightSettingUserControl2;
        private NightSettingDetailUserControl nightSettingUserControl3;
        private NightSettingDetailUserControl nightSettingUserControl4;
        private NightSettingDetailUserControl nightSettingUserControl5;
        private System.Windows.Forms.ComboBox comboBox_所属会社;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button buttonAdd;
    }
}
