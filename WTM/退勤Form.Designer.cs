
namespace WTM
{
    partial class 退勤Form
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gcMultiRow1 = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.label勤務開始時間 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label勤務終了時間説明 = new System.Windows.Forms.Label();
            this.textBox勤務終了時間 = new System.Windows.Forms.TextBox();
            this.label勤務終了時間 = new System.Windows.Forms.Label();
            this.label勤務終了日説明 = new System.Windows.Forms.Label();
            this.textBox勤務終了日 = new System.Windows.Forms.TextBox();
            this.label勤務終了日 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonFinishWork = new System.Windows.Forms.Button();
            this.panel_Message = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.button_確認 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMultiRow1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel_Message.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label勤務開始時間, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1193, 719);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.gcMultiRow1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 343);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 373F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1187, 373);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // gcMultiRow1
            // 
            this.gcMultiRow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMultiRow1.Location = new System.Drawing.Point(3, 3);
            this.gcMultiRow1.Name = "gcMultiRow1";
            this.gcMultiRow1.Size = new System.Drawing.Size(1181, 367);
            this.gcMultiRow1.TabIndex = 0;
            this.gcMultiRow1.Text = "gcMultiRow1";
            this.gcMultiRow1.CellEditedFormattedValueChanged += new System.EventHandler<GrapeCity.Win.MultiRow.CellEditedFormattedValueChangedEventArgs>(this.gcMultiRow1_CellEditedFormattedValueChanged);
            // 
            // label勤務開始時間
            // 
            this.label勤務開始時間.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label勤務開始時間.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label勤務開始時間.Location = new System.Drawing.Point(3, 0);
            this.label勤務開始時間.Name = "label勤務開始時間";
            this.label勤務開始時間.Size = new System.Drawing.Size(1187, 70);
            this.label勤務開始時間.TabIndex = 1;
            this.label勤務開始時間.Text = "label1";
            this.label勤務開始時間.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label勤務開始時間.Click += new System.EventHandler(this.ClickEvent);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1187, 194);
            this.panel1.TabIndex = 2;
            this.panel1.Click += new System.EventHandler(this.ClickEvent);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.label勤務終了時間説明);
            this.groupBox1.Controls.Add(this.textBox勤務終了時間);
            this.groupBox1.Controls.Add(this.label勤務終了時間);
            this.groupBox1.Controls.Add(this.label勤務終了日説明);
            this.groupBox1.Controls.Add(this.textBox勤務終了日);
            this.groupBox1.Controls.Add(this.label勤務終了日);
            this.groupBox1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.Location = new System.Drawing.Point(355, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 191);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "勤務終了";
            // 
            // label勤務終了時間説明
            // 
            this.label勤務終了時間説明.AutoSize = true;
            this.label勤務終了時間説明.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label勤務終了時間説明.ForeColor = System.Drawing.Color.DimGray;
            this.label勤務終了時間説明.Location = new System.Drawing.Point(195, 134);
            this.label勤務終了時間説明.Name = "label勤務終了時間説明";
            this.label勤務終了時間説明.Size = new System.Drawing.Size(224, 16);
            this.label勤務終了時間説明.TabIndex = 5;
            this.label勤務終了時間説明.Text = "時分を入力してください(24HHMM)";
            this.label勤務終了時間説明.Click += new System.EventHandler(this.ClickEvent);
            // 
            // textBox勤務終了時間
            // 
            this.textBox勤務終了時間.Location = new System.Drawing.Point(40, 129);
            this.textBox勤務終了時間.MaxLength = 4;
            this.textBox勤務終了時間.Name = "textBox勤務終了時間";
            this.textBox勤務終了時間.Size = new System.Drawing.Size(149, 26);
            this.textBox勤務終了時間.TabIndex = 4;
            this.textBox勤務終了時間.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBox勤務終了時間.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox勤務終了_KeyPress);
            this.textBox勤務終了時間.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // label勤務終了時間
            // 
            this.label勤務終了時間.AutoSize = true;
            this.label勤務終了時間.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label勤務終了時間.Location = new System.Drawing.Point(37, 109);
            this.label勤務終了時間.Name = "label勤務終了時間";
            this.label勤務終了時間.Size = new System.Drawing.Size(104, 16);
            this.label勤務終了時間.TabIndex = 3;
            this.label勤務終了時間.Text = "勤務終了時間";
            this.label勤務終了時間.Click += new System.EventHandler(this.ClickEvent);
            // 
            // label勤務終了日説明
            // 
            this.label勤務終了日説明.AutoSize = true;
            this.label勤務終了日説明.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label勤務終了日説明.ForeColor = System.Drawing.Color.DimGray;
            this.label勤務終了日説明.Location = new System.Drawing.Point(195, 65);
            this.label勤務終了日説明.Name = "label勤務終了日説明";
            this.label勤務終了日説明.Size = new System.Drawing.Size(242, 16);
            this.label勤務終了日説明.TabIndex = 2;
            this.label勤務終了日説明.Text = "年月日を入力してください(YYMMDD)";
            this.label勤務終了日説明.Click += new System.EventHandler(this.ClickEvent);
            // 
            // textBox勤務終了日
            // 
            this.textBox勤務終了日.Location = new System.Drawing.Point(40, 60);
            this.textBox勤務終了日.MaxLength = 6;
            this.textBox勤務終了日.Name = "textBox勤務終了日";
            this.textBox勤務終了日.Size = new System.Drawing.Size(149, 26);
            this.textBox勤務終了日.TabIndex = 1;
            this.textBox勤務終了日.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBox勤務終了日.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox勤務終了_KeyPress);
            this.textBox勤務終了日.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // label勤務終了日
            // 
            this.label勤務終了日.AutoSize = true;
            this.label勤務終了日.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label勤務終了日.Location = new System.Drawing.Point(37, 40);
            this.label勤務終了日.Name = "label勤務終了日";
            this.label勤務終了日.Size = new System.Drawing.Size(88, 16);
            this.label勤務終了日.TabIndex = 0;
            this.label勤務終了日.Text = "勤務終了日";
            this.label勤務終了日.Click += new System.EventHandler(this.ClickEvent);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonCancel);
            this.panel2.Controls.Add(this.buttonFinishWork);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 273);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1187, 64);
            this.panel2.TabIndex = 3;
            this.panel2.Click += new System.EventHandler(this.ClickEvent);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCancel.BackColor = System.Drawing.Color.HotPink;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(752, 14);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(138, 36);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonFinishWork
            // 
            this.buttonFinishWork.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonFinishWork.BackColor = System.Drawing.Color.SlateBlue;
            this.buttonFinishWork.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonFinishWork.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonFinishWork.ForeColor = System.Drawing.Color.White;
            this.buttonFinishWork.Location = new System.Drawing.Point(332, 14);
            this.buttonFinishWork.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonFinishWork.Name = "buttonFinishWork";
            this.buttonFinishWork.Size = new System.Drawing.Size(138, 36);
            this.buttonFinishWork.TabIndex = 10;
            this.buttonFinishWork.Text = "退勤";
            this.buttonFinishWork.UseVisualStyleBackColor = false;
            this.buttonFinishWork.Click += new System.EventHandler(this.buttonFinishWork_Click);
            // 
            // panel_Message
            // 
            this.panel_Message.BackColor = System.Drawing.Color.White;
            this.panel_Message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Message.Controls.Add(this.label5);
            this.panel_Message.Controls.Add(this.button_確認);
            this.panel_Message.Location = new System.Drawing.Point(444, 293);
            this.panel_Message.Name = "panel_Message";
            this.panel_Message.Size = new System.Drawing.Size(311, 133);
            this.panel_Message.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(81, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "お疲れ様でした。";
            // 
            // button_確認
            // 
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
            // 退勤Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1193, 719);
            this.Controls.Add(this.panel_Message);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "退勤Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "退勤";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.退勤Form_FormClosed);
            this.Load += new System.EventHandler(this.退勤Form_Load);
            this.ResizeBegin += new System.EventHandler(this.退勤Form_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.退勤Form_ResizeEnd);
            this.SizeChanged += new System.EventHandler(this.退勤Form_SizeChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMultiRow1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel_Message.ResumeLayout(false);
            this.panel_Message.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private GrapeCity.Win.MultiRow.GcMultiRow gcMultiRow1;
        private System.Windows.Forms.Label label勤務開始時間;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label勤務終了時間説明;
        private System.Windows.Forms.TextBox textBox勤務終了時間;
        private System.Windows.Forms.Label label勤務終了時間;
        private System.Windows.Forms.Label label勤務終了日説明;
        private System.Windows.Forms.TextBox textBox勤務終了日;
        private System.Windows.Forms.Label label勤務終了日;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonFinishWork;
        private System.Windows.Forms.Panel panel_Message;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_確認;
    }
}