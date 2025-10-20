
namespace WTM
{
    partial class 出勤Form
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonStartWork = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label_Now = new System.Windows.Forms.Label();
            this.label日付 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label勤務開始時間説明 = new System.Windows.Forms.Label();
            this.textBox勤務開始時間 = new System.Windows.Forms.TextBox();
            this.label勤務開始時間 = new System.Windows.Forms.Label();
            this.label勤務開始日説明 = new System.Windows.Forms.Label();
            this.textBox勤務開始日 = new System.Windows.Forms.TextBox();
            this.label勤務開始日 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel_Message = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.button_確認 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel_Message.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 570F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(872, 379);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Click += new System.EventHandler(this.ClickEvent);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonCancel);
            this.panel2.Controls.Add(this.buttonStartWork);
            this.panel2.Location = new System.Drawing.Point(154, 283);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(564, 93);
            this.panel2.TabIndex = 3;
            this.panel2.Click += new System.EventHandler(this.ClickEvent);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.HotPink;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(370, 25);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(138, 44);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonStartWork
            // 
            this.buttonStartWork.BackColor = System.Drawing.Color.SlateBlue;
            this.buttonStartWork.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStartWork.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonStartWork.ForeColor = System.Drawing.Color.White;
            this.buttonStartWork.Location = new System.Drawing.Point(44, 25);
            this.buttonStartWork.Name = "buttonStartWork";
            this.buttonStartWork.Size = new System.Drawing.Size(138, 44);
            this.buttonStartWork.TabIndex = 10;
            this.buttonStartWork.Text = "出勤";
            this.buttonStartWork.UseVisualStyleBackColor = false;
            this.buttonStartWork.Click += new System.EventHandler(this.buttonStartWork_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(154, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(564, 64);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "現在の時間";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label_Now, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label日付, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(11, 22);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(542, 33);
            this.tableLayoutPanel2.TabIndex = 4;
            this.tableLayoutPanel2.Click += new System.EventHandler(this.ClickEvent);
            // 
            // label_Now
            // 
            this.label_Now.AutoSize = true;
            this.label_Now.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Now.Location = new System.Drawing.Point(303, 0);
            this.label_Now.Name = "label_Now";
            this.label_Now.Size = new System.Drawing.Size(83, 29);
            this.label_Now.TabIndex = 2;
            this.label_Now.Text = "label1";
            this.label_Now.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_Now.Click += new System.EventHandler(this.ClickEvent);
            // 
            // label日付
            // 
            this.label日付.AutoSize = true;
            this.label日付.Font = new System.Drawing.Font("MS UI Gothic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label日付.Location = new System.Drawing.Point(3, 0);
            this.label日付.Name = "label日付";
            this.label日付.Size = new System.Drawing.Size(83, 29);
            this.label日付.TabIndex = 1;
            this.label日付.Text = "label1";
            this.label日付.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label日付.Click += new System.EventHandler(this.ClickEvent);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label勤務開始時間説明);
            this.groupBox1.Controls.Add(this.textBox勤務開始時間);
            this.groupBox1.Controls.Add(this.label勤務開始時間);
            this.groupBox1.Controls.Add(this.label勤務開始日説明);
            this.groupBox1.Controls.Add(this.textBox勤務開始日);
            this.groupBox1.Controls.Add(this.label勤務開始日);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBox1.Location = new System.Drawing.Point(154, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(564, 204);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "勤務開始";
            // 
            // label勤務開始時間説明
            // 
            this.label勤務開始時間説明.AutoSize = true;
            this.label勤務開始時間説明.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label勤務開始時間説明.ForeColor = System.Drawing.Color.DimGray;
            this.label勤務開始時間説明.Location = new System.Drawing.Point(195, 134);
            this.label勤務開始時間説明.Name = "label勤務開始時間説明";
            this.label勤務開始時間説明.Size = new System.Drawing.Size(224, 16);
            this.label勤務開始時間説明.TabIndex = 5;
            this.label勤務開始時間説明.Text = "時分を入力してください(24HHMM)";
            this.label勤務開始時間説明.Click += new System.EventHandler(this.ClickEvent);
            // 
            // textBox勤務開始時間
            // 
            this.textBox勤務開始時間.Location = new System.Drawing.Point(40, 129);
            this.textBox勤務開始時間.MaxLength = 4;
            this.textBox勤務開始時間.Name = "textBox勤務開始時間";
            this.textBox勤務開始時間.Size = new System.Drawing.Size(149, 26);
            this.textBox勤務開始時間.TabIndex = 4;
            this.textBox勤務開始時間.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBox勤務開始時間.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox勤務終了_KeyPress);
            this.textBox勤務開始時間.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // label勤務開始時間
            // 
            this.label勤務開始時間.AutoSize = true;
            this.label勤務開始時間.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label勤務開始時間.Location = new System.Drawing.Point(37, 109);
            this.label勤務開始時間.Name = "label勤務開始時間";
            this.label勤務開始時間.Size = new System.Drawing.Size(104, 16);
            this.label勤務開始時間.TabIndex = 3;
            this.label勤務開始時間.Text = "勤務開始時間";
            this.label勤務開始時間.Click += new System.EventHandler(this.ClickEvent);
            // 
            // label勤務開始日説明
            // 
            this.label勤務開始日説明.AutoSize = true;
            this.label勤務開始日説明.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label勤務開始日説明.ForeColor = System.Drawing.Color.DimGray;
            this.label勤務開始日説明.Location = new System.Drawing.Point(195, 65);
            this.label勤務開始日説明.Name = "label勤務開始日説明";
            this.label勤務開始日説明.Size = new System.Drawing.Size(242, 16);
            this.label勤務開始日説明.TabIndex = 2;
            this.label勤務開始日説明.Text = "年月日を入力してください(YYMMDD)";
            this.label勤務開始日説明.Click += new System.EventHandler(this.ClickEvent);
            // 
            // textBox勤務開始日
            // 
            this.textBox勤務開始日.Location = new System.Drawing.Point(40, 60);
            this.textBox勤務開始日.MaxLength = 6;
            this.textBox勤務開始日.Name = "textBox勤務開始日";
            this.textBox勤務開始日.Size = new System.Drawing.Size(149, 26);
            this.textBox勤務開始日.TabIndex = 1;
            this.textBox勤務開始日.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBox勤務開始日.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox勤務終了_KeyPress);
            this.textBox勤務開始日.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // label勤務開始日
            // 
            this.label勤務開始日.AutoSize = true;
            this.label勤務開始日.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label勤務開始日.Location = new System.Drawing.Point(37, 40);
            this.label勤務開始日.Name = "label勤務開始日";
            this.label勤務開始日.Size = new System.Drawing.Size(88, 16);
            this.label勤務開始日.TabIndex = 0;
            this.label勤務開始日.Text = "勤務開始日";
            this.label勤務開始日.Click += new System.EventHandler(this.ClickEvent);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel_Message
            // 
            this.panel_Message.BackColor = System.Drawing.Color.White;
            this.panel_Message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Message.Controls.Add(this.label5);
            this.panel_Message.Controls.Add(this.button_確認);
            this.panel_Message.Location = new System.Drawing.Point(281, 123);
            this.panel_Message.Name = "panel_Message";
            this.panel_Message.Size = new System.Drawing.Size(311, 133);
            this.panel_Message.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(55, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(198, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "出勤を受け付けました。";
            // 
            // button_確認
            // 
            this.button_確認.BackColor = System.Drawing.SystemColors.Control;
            this.button_確認.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_確認.Location = new System.Drawing.Point(97, 87);
            this.button_確認.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button_確認.Name = "button_確認";
            this.button_確認.Size = new System.Drawing.Size(117, 31);
            this.button_確認.TabIndex = 10;
            this.button_確認.Text = "確認";
            this.button_確認.UseVisualStyleBackColor = false;
            this.button_確認.Click += new System.EventHandler(this.button_確認_Click);
            // 
            // 出勤Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(872, 379);
            this.Controls.Add(this.panel_Message);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "出勤Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "出勤";
            this.Load += new System.EventHandler(this.出勤Form_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel_Message.ResumeLayout(false);
            this.panel_Message.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label日付;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label勤務開始時間説明;
        private System.Windows.Forms.TextBox textBox勤務開始時間;
        private System.Windows.Forms.Label label勤務開始時間;
        private System.Windows.Forms.Label label勤務開始日説明;
        private System.Windows.Forms.TextBox textBox勤務開始日;
        private System.Windows.Forms.Label label勤務開始日;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonStartWork;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label_Now;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel_Message;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_確認;
    }
}