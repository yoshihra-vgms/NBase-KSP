
namespace Senin
{
    partial class 役職変更Form
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
            this.comboBox職名 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button乗船場所 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox乗船場所 = new System.Windows.Forms.TextBox();
            this.button閉じる = new System.Windows.Forms.Button();
            this.button更新 = new System.Windows.Forms.Button();
            this.nullableDateTimePicker開始 = new NBaseUtil.NullableDateTimePicker();
            this.SuspendLayout();
            // 
            // comboBox職名
            // 
            this.comboBox職名.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox職名.FormattingEnabled = true;
            this.comboBox職名.Location = new System.Drawing.Point(116, 53);
            this.comboBox職名.Name = "comboBox職名";
            this.comboBox職名.Size = new System.Drawing.Size(162, 20);
            this.comboBox職名.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "職名";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "※変更日";
            // 
            // button乗船場所
            // 
            this.button乗船場所.BackColor = System.Drawing.SystemColors.Control;
            this.button乗船場所.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button乗船場所.Location = new System.Drawing.Point(278, 78);
            this.button乗船場所.Name = "button乗船場所";
            this.button乗船場所.Size = new System.Drawing.Size(62, 21);
            this.button乗船場所.TabIndex = 43;
            this.button乗船場所.Text = "検索";
            this.button乗船場所.UseVisualStyleBackColor = false;
            this.button乗船場所.Click += new System.EventHandler(this.button乗船場所_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(41, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 41;
            this.label10.Text = "港";
            // 
            // textBox乗船場所
            // 
            this.textBox乗船場所.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBox乗船場所.Location = new System.Drawing.Point(116, 79);
            this.textBox乗船場所.Name = "textBox乗船場所";
            this.textBox乗船場所.ReadOnly = true;
            this.textBox乗船場所.Size = new System.Drawing.Size(143, 19);
            this.textBox乗船場所.TabIndex = 42;
            // 
            // button閉じる
            // 
            this.button閉じる.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button閉じる.Location = new System.Drawing.Point(199, 130);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 49;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // button更新
            // 
            this.button更新.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button更新.BackColor = System.Drawing.SystemColors.Control;
            this.button更新.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button更新.Location = new System.Drawing.Point(106, 130);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(75, 23);
            this.button更新.TabIndex = 48;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = false;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // nullableDateTimePicker開始
            // 
            this.nullableDateTimePicker開始.Location = new System.Drawing.Point(116, 24);
            this.nullableDateTimePicker開始.Name = "nullableDateTimePicker開始";
            this.nullableDateTimePicker開始.NullValue = "";
            this.nullableDateTimePicker開始.Size = new System.Drawing.Size(132, 19);
            this.nullableDateTimePicker開始.TabIndex = 9;
            this.nullableDateTimePicker開始.Value = null;
            // 
            // 役職変更Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(381, 165);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.button乗船場所);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox乗船場所);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nullableDateTimePicker開始);
            this.Controls.Add(this.comboBox職名);
            this.Controls.Add(this.label7);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(397, 204);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(397, 204);
            this.Name = "役職変更Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "役職変更";
            this.Load += new System.EventHandler(this.役職変更Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox職名;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker開始;
        private System.Windows.Forms.Button button乗船場所;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox乗船場所;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button更新;
    }
}