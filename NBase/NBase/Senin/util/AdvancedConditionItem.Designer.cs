namespace Senin.util
{
    partial class AdvancedConditionItem
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBox_Tab = new System.Windows.Forms.ComboBox();
            this.comboBox_Item1 = new System.Windows.Forms.ComboBox();
            this.checkedComboBox_Item1 = new NBaseUtil.CheckedComboBox();
            this.comboBox_Item2 = new System.Windows.Forms.ComboBox();
            this.checkedComboBox_Item2 = new NBaseUtil.CheckedComboBox();
            this.comboBox_Item3 = new System.Windows.Forms.ComboBox();
            this.comboBox_Value = new System.Windows.Forms.ComboBox();
            this.textBox_Value1 = new System.Windows.Forms.TextBox();
            this.textBox_Value2 = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkedComboBox_Value = new NBaseUtil.CheckedComboBox();
            this.flowLayoutPanel_Date = new System.Windows.Forms.FlowLayoutPanel();
            this.nullableDateTimePicker1 = new NBaseUtil.NullableDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker2 = new NBaseUtil.NullableDateTimePicker();
            this.flowLayoutPanel_Value = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Value = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel_Date.SuspendLayout();
            this.flowLayoutPanel_Value.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.comboBox_Tab);
            this.flowLayoutPanel1.Controls.Add(this.comboBox_Item1);
            this.flowLayoutPanel1.Controls.Add(this.checkedComboBox_Item1);
            this.flowLayoutPanel1.Controls.Add(this.comboBox_Item2);
            this.flowLayoutPanel1.Controls.Add(this.checkedComboBox_Item2);
            this.flowLayoutPanel1.Controls.Add(this.comboBox_Item3);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(700, 25);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // comboBox_Tab
            // 
            this.comboBox_Tab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Tab.FormattingEnabled = true;
            this.comboBox_Tab.Location = new System.Drawing.Point(3, 3);
            this.comboBox_Tab.Name = "comboBox_Tab";
            this.comboBox_Tab.Size = new System.Drawing.Size(125, 20);
            this.comboBox_Tab.TabIndex = 0;
            this.comboBox_Tab.SelectedIndexChanged += new System.EventHandler(this.comboBox_Tab_SelectedIndexChanged);
            // 
            // comboBox_Item1
            // 
            this.comboBox_Item1.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox_Item1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Item1.FormattingEnabled = true;
            this.comboBox_Item1.Location = new System.Drawing.Point(134, 3);
            this.comboBox_Item1.Name = "comboBox_Item1";
            this.comboBox_Item1.Size = new System.Drawing.Size(212, 20);
            this.comboBox_Item1.TabIndex = 0;
            this.comboBox_Item1.Visible = false;
            this.comboBox_Item1.SelectedIndexChanged += new System.EventHandler(this.comboBox_Item1_SelectedIndexChanged);
            // 
            // checkedComboBox_Item1
            // 
            this.checkedComboBox_Item1.BackColor = System.Drawing.SystemColors.Window;
            this.checkedComboBox_Item1.CheckOnClick = true;
            this.checkedComboBox_Item1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.checkedComboBox_Item1.DropDownHeight = 1;
            this.checkedComboBox_Item1.FormattingEnabled = true;
            this.checkedComboBox_Item1.IntegralHeight = false;
            this.checkedComboBox_Item1.Location = new System.Drawing.Point(352, 3);
            this.checkedComboBox_Item1.Name = "checkedComboBox_Item1";
            this.checkedComboBox_Item1.Size = new System.Drawing.Size(212, 20);
            this.checkedComboBox_Item1.TabIndex = 3;
            this.checkedComboBox_Item1.ValueSeparator = ", ";
            this.checkedComboBox_Item1.Visible = false;
            this.checkedComboBox_Item1.DropDownClosed += new System.EventHandler(this.checkedComboBox_Item1_DropDownClosed);
            // 
            // comboBox_Item2
            // 
            this.comboBox_Item2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Item2.FormattingEnabled = true;
            this.comboBox_Item2.Location = new System.Drawing.Point(3, 29);
            this.comboBox_Item2.Name = "comboBox_Item2";
            this.comboBox_Item2.Size = new System.Drawing.Size(212, 20);
            this.comboBox_Item2.TabIndex = 0;
            this.comboBox_Item2.Visible = false;
            this.comboBox_Item2.SelectedIndexChanged += new System.EventHandler(this.comboBox_Item2_SelectedIndexChanged);
            // 
            // checkedComboBox_Item2
            // 
            this.checkedComboBox_Item2.CheckOnClick = true;
            this.checkedComboBox_Item2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.checkedComboBox_Item2.DropDownHeight = 1;
            this.checkedComboBox_Item2.FormattingEnabled = true;
            this.checkedComboBox_Item2.IntegralHeight = false;
            this.checkedComboBox_Item2.Location = new System.Drawing.Point(221, 29);
            this.checkedComboBox_Item2.Name = "checkedComboBox_Item2";
            this.checkedComboBox_Item2.Size = new System.Drawing.Size(212, 20);
            this.checkedComboBox_Item2.TabIndex = 4;
            this.checkedComboBox_Item2.ValueSeparator = ", ";
            this.checkedComboBox_Item2.Visible = false;
            // 
            // comboBox_Item3
            // 
            this.comboBox_Item3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Item3.FormattingEnabled = true;
            this.comboBox_Item3.Location = new System.Drawing.Point(439, 29);
            this.comboBox_Item3.Name = "comboBox_Item3";
            this.comboBox_Item3.Size = new System.Drawing.Size(125, 20);
            this.comboBox_Item3.TabIndex = 0;
            this.comboBox_Item3.Visible = false;
            this.comboBox_Item3.SelectedIndexChanged += new System.EventHandler(this.comboBox_Item3_SelectedIndexChanged);
            // 
            // comboBox_Value
            // 
            this.comboBox_Value.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Value.FormattingEnabled = true;
            this.comboBox_Value.Location = new System.Drawing.Point(221, 3);
            this.comboBox_Value.Name = "comboBox_Value";
            this.comboBox_Value.Size = new System.Drawing.Size(212, 20);
            this.comboBox_Value.TabIndex = 0;
            this.comboBox_Value.Visible = false;
            // 
            // textBox_Value1
            // 
            this.textBox_Value1.Location = new System.Drawing.Point(0, 0);
            this.textBox_Value1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_Value1.MaxLength = 4;
            this.textBox_Value1.Name = "textBox_Value1";
            this.textBox_Value1.Size = new System.Drawing.Size(100, 19);
            this.textBox_Value1.TabIndex = 1;
            // 
            // textBox_Value2
            // 
            this.textBox_Value2.Location = new System.Drawing.Point(134, 0);
            this.textBox_Value2.Margin = new System.Windows.Forms.Padding(0);
            this.textBox_Value2.MaxLength = 4;
            this.textBox_Value2.Name = "textBox_Value2";
            this.textBox_Value2.Size = new System.Drawing.Size(100, 19);
            this.textBox_Value2.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.checkedComboBox_Value);
            this.flowLayoutPanel2.Controls.Add(this.comboBox_Value);
            this.flowLayoutPanel2.Controls.Add(this.flowLayoutPanel_Date);
            this.flowLayoutPanel2.Controls.Add(this.flowLayoutPanel_Value);
            this.flowLayoutPanel2.Controls.Add(this.textBox_Value);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(701, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(481, 25);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // checkedComboBox_Value
            // 
            this.checkedComboBox_Value.CheckOnClick = true;
            this.checkedComboBox_Value.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.checkedComboBox_Value.DropDownHeight = 1;
            this.checkedComboBox_Value.FormattingEnabled = true;
            this.checkedComboBox_Value.IntegralHeight = false;
            this.checkedComboBox_Value.Location = new System.Drawing.Point(3, 3);
            this.checkedComboBox_Value.Name = "checkedComboBox_Value";
            this.checkedComboBox_Value.Size = new System.Drawing.Size(212, 20);
            this.checkedComboBox_Value.TabIndex = 5;
            this.checkedComboBox_Value.ValueSeparator = ", ";
            this.checkedComboBox_Value.Visible = false;
            // 
            // flowLayoutPanel_Date
            // 
            this.flowLayoutPanel_Date.Controls.Add(this.nullableDateTimePicker1);
            this.flowLayoutPanel_Date.Controls.Add(this.label1);
            this.flowLayoutPanel_Date.Controls.Add(this.nullableDateTimePicker2);
            this.flowLayoutPanel_Date.Location = new System.Drawing.Point(3, 29);
            this.flowLayoutPanel_Date.Name = "flowLayoutPanel_Date";
            this.flowLayoutPanel_Date.Size = new System.Drawing.Size(385, 20);
            this.flowLayoutPanel_Date.TabIndex = 7;
            this.flowLayoutPanel_Date.Visible = false;
            // 
            // nullableDateTimePicker1
            // 
            this.nullableDateTimePicker1.Location = new System.Drawing.Point(0, 0);
            this.nullableDateTimePicker1.Margin = new System.Windows.Forms.Padding(0);
            this.nullableDateTimePicker1.Name = "nullableDateTimePicker1";
            this.nullableDateTimePicker1.Size = new System.Drawing.Size(175, 19);
            this.nullableDateTimePicker1.TabIndex = 2;
            this.nullableDateTimePicker1.Value = new System.DateTime(2015, 11, 11, 15, 36, 1, 242);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(178, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "～";
            // 
            // nullableDateTimePicker2
            // 
            this.nullableDateTimePicker2.Location = new System.Drawing.Point(209, 0);
            this.nullableDateTimePicker2.Margin = new System.Windows.Forms.Padding(0);
            this.nullableDateTimePicker2.Name = "nullableDateTimePicker2";
            this.nullableDateTimePicker2.Size = new System.Drawing.Size(175, 19);
            this.nullableDateTimePicker2.TabIndex = 2;
            this.nullableDateTimePicker2.Value = new System.DateTime(2015, 11, 11, 15, 36, 1, 242);
            // 
            // flowLayoutPanel_Value
            // 
            this.flowLayoutPanel_Value.Controls.Add(this.textBox_Value1);
            this.flowLayoutPanel_Value.Controls.Add(this.label2);
            this.flowLayoutPanel_Value.Controls.Add(this.textBox_Value2);
            this.flowLayoutPanel_Value.Location = new System.Drawing.Point(3, 55);
            this.flowLayoutPanel_Value.Name = "flowLayoutPanel_Value";
            this.flowLayoutPanel_Value.Size = new System.Drawing.Size(236, 19);
            this.flowLayoutPanel_Value.TabIndex = 8;
            this.flowLayoutPanel_Value.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(103, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "～";
            // 
            // textBox_Value
            // 
            this.textBox_Value.Location = new System.Drawing.Point(245, 55);
            this.textBox_Value.Name = "textBox_Value";
            this.textBox_Value.Size = new System.Drawing.Size(177, 19);
            this.textBox_Value.TabIndex = 1;
            this.textBox_Value.Visible = false;
            // 
            // AdvancedConditionItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AdvancedConditionItem";
            this.Size = new System.Drawing.Size(1184, 27);
            this.Load += new System.EventHandler(this.AdvancedConditionItem_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel_Date.ResumeLayout(false);
            this.flowLayoutPanel_Date.PerformLayout();
            this.flowLayoutPanel_Value.ResumeLayout(false);
            this.flowLayoutPanel_Value.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBox_Tab;
        private System.Windows.Forms.ComboBox comboBox_Item1;
        private System.Windows.Forms.ComboBox comboBox_Item2;
        private System.Windows.Forms.ComboBox comboBox_Item3;
        private System.Windows.Forms.ComboBox comboBox_Value;
        private System.Windows.Forms.TextBox textBox_Value1;
        private NBaseUtil.CheckedComboBox checkedComboBox_Item1;
        private NBaseUtil.CheckedComboBox checkedComboBox_Item2;
        private NBaseUtil.CheckedComboBox checkedComboBox_Value;
        private System.Windows.Forms.TextBox textBox_Value2;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker1;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Date;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Value;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Value;
    }
}
