namespace Hachu.Reports
{
    partial class 業者別支払実績出力Form
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
            this.button出力 = new System.Windows.Forms.Button();
            this.comboBox取引先 = new System.Windows.Forms.ComboBox();
            this.label取引先 = new System.Windows.Forms.Label();
            this.label詳細種別 = new System.Windows.Forms.Label();
            this.comboBox詳細種別 = new System.Windows.Forms.ComboBox();
            this.label種別 = new System.Windows.Forms.Label();
            this.comboBox種別 = new System.Windows.Forms.ComboBox();
            this.label手配依頼日 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nullableDateTimePicker期間To = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker期間From = new NBaseUtil.NullableDateTimePicker();
            this.comboBox期間 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button出力
            // 
            this.button出力.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button出力.Location = new System.Drawing.Point(233, 166);
            this.button出力.Name = "button出力";
            this.button出力.Size = new System.Drawing.Size(75, 23);
            this.button出力.TabIndex = 4;
            this.button出力.Text = "出力";
            this.button出力.UseVisualStyleBackColor = true;
            this.button出力.Click += new System.EventHandler(this.button出力_Click);
            // 
            // comboBox取引先
            // 
            this.comboBox取引先.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox取引先.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBox取引先.DropDownWidth = 200;
            this.comboBox取引先.FormattingEnabled = true;
            this.comboBox取引先.ImeMode = System.Windows.Forms.ImeMode.On;
            this.comboBox取引先.Location = new System.Drawing.Point(93, 25);
            this.comboBox取引先.Name = "comboBox取引先";
            this.comboBox取引先.Size = new System.Drawing.Size(250, 20);
            this.comboBox取引先.TabIndex = 6;
            // 
            // label取引先
            // 
            this.label取引先.AutoSize = true;
            this.label取引先.Location = new System.Drawing.Point(46, 28);
            this.label取引先.Name = "label取引先";
            this.label取引先.Size = new System.Drawing.Size(41, 12);
            this.label取引先.TabIndex = 5;
            this.label取引先.Text = "取引先";
            // 
            // label詳細種別
            // 
            this.label詳細種別.AutoSize = true;
            this.label詳細種別.Location = new System.Drawing.Point(34, 89);
            this.label詳細種別.Name = "label詳細種別";
            this.label詳細種別.Size = new System.Drawing.Size(53, 12);
            this.label詳細種別.TabIndex = 27;
            this.label詳細種別.Text = "詳細種別";
            // 
            // comboBox詳細種別
            // 
            this.comboBox詳細種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox詳細種別.FormattingEnabled = true;
            this.comboBox詳細種別.Items.AddRange(new object[] {
            "船用品",
            "潤滑油",
            "修繕"});
            this.comboBox詳細種別.Location = new System.Drawing.Point(93, 87);
            this.comboBox詳細種別.Name = "comboBox詳細種別";
            this.comboBox詳細種別.Size = new System.Drawing.Size(170, 20);
            this.comboBox詳細種別.TabIndex = 29;
            // 
            // label種別
            // 
            this.label種別.AutoSize = true;
            this.label種別.Location = new System.Drawing.Point(58, 59);
            this.label種別.Name = "label種別";
            this.label種別.Size = new System.Drawing.Size(29, 12);
            this.label種別.TabIndex = 26;
            this.label種別.Text = "種別";
            // 
            // comboBox種別
            // 
            this.comboBox種別.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox種別.FormattingEnabled = true;
            this.comboBox種別.Items.AddRange(new object[] {
            "船用品",
            "潤滑油",
            "修繕"});
            this.comboBox種別.Location = new System.Drawing.Point(93, 56);
            this.comboBox種別.Name = "comboBox種別";
            this.comboBox種別.Size = new System.Drawing.Size(170, 20);
            this.comboBox種別.TabIndex = 28;
            this.comboBox種別.SelectedIndexChanged += new System.EventHandler(this.comboBox種別_SelectedIndexChanged);
            // 
            // label手配依頼日
            // 
            this.label手配依頼日.AutoSize = true;
            this.label手配依頼日.Location = new System.Drawing.Point(58, 122);
            this.label手配依頼日.Name = "label手配依頼日";
            this.label手配依頼日.Size = new System.Drawing.Size(29, 12);
            this.label手配依頼日.TabIndex = 31;
            this.label手配依頼日.Text = "期間";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(365, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "～";
            // 
            // nullableDateTimePicker期間To
            // 
            this.nullableDateTimePicker期間To.Location = new System.Drawing.Point(384, 120);
            this.nullableDateTimePicker期間To.Name = "nullableDateTimePicker期間To";
            this.nullableDateTimePicker期間To.Size = new System.Drawing.Size(120, 19);
            this.nullableDateTimePicker期間To.TabIndex = 32;
            this.nullableDateTimePicker期間To.Value = null;
            // 
            // nullableDateTimePicker期間From
            // 
            this.nullableDateTimePicker期間From.Location = new System.Drawing.Point(242, 120);
            this.nullableDateTimePicker期間From.Name = "nullableDateTimePicker期間From";
            this.nullableDateTimePicker期間From.Size = new System.Drawing.Size(120, 19);
            this.nullableDateTimePicker期間From.TabIndex = 30;
            this.nullableDateTimePicker期間From.Value = null;
            // 
            // comboBox期間
            // 
            this.comboBox期間.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox期間.FormattingEnabled = true;
            this.comboBox期間.Items.AddRange(new object[] {
            "発注日",
            "受領日",
            "支払日"});
            this.comboBox期間.Location = new System.Drawing.Point(93, 118);
            this.comboBox期間.Name = "comboBox期間";
            this.comboBox期間.Size = new System.Drawing.Size(143, 20);
            this.comboBox期間.TabIndex = 28;
            this.comboBox期間.SelectedIndexChanged += new System.EventHandler(this.comboBox種別_SelectedIndexChanged);
            // 
            // 業者別支払実績出力Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(540, 201);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nullableDateTimePicker期間To);
            this.Controls.Add(this.label手配依頼日);
            this.Controls.Add(this.nullableDateTimePicker期間From);
            this.Controls.Add(this.label詳細種別);
            this.Controls.Add(this.comboBox詳細種別);
            this.Controls.Add(this.label種別);
            this.Controls.Add(this.comboBox期間);
            this.Controls.Add(this.comboBox種別);
            this.Controls.Add(this.comboBox取引先);
            this.Controls.Add(this.label取引先);
            this.Controls.Add(this.button出力);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "業者別支払実績出力Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "業者別支払実績出力Form";
            this.Load += new System.EventHandler(this.業者別支払実績出力Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button出力;
        private System.Windows.Forms.ComboBox comboBox取引先;
        private System.Windows.Forms.Label label取引先;
        private System.Windows.Forms.Label label詳細種別;
        private System.Windows.Forms.ComboBox comboBox詳細種別;
        private System.Windows.Forms.Label label種別;
        private System.Windows.Forms.ComboBox comboBox種別;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker期間From;
        private System.Windows.Forms.Label label手配依頼日;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker期間To;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox期間;
    }
}