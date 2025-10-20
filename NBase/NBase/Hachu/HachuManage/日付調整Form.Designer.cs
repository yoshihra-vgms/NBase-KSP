namespace Hachu.HachuManage
{
    partial class 日付調整Form
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
            this.button更新 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.label手配依頼種別 = new System.Windows.Forms.Label();
            this.textBox手配依頼種 = new System.Windows.Forms.TextBox();
            this.label手配依頼詳細種別 = new System.Windows.Forms.Label();
            this.textBox手配依頼詳細種別 = new System.Windows.Forms.TextBox();
            this.label船 = new System.Windows.Forms.Label();
            this.textBox船 = new System.Windows.Forms.TextBox();
            this.label発注日 = new System.Windows.Forms.Label();
            this.dateTimePicker発注日 = new System.Windows.Forms.DateTimePicker();
            this.label回答日 = new System.Windows.Forms.Label();
            this.dateTimePicker回答日 = new System.Windows.Forms.DateTimePicker();
            this.label受領日 = new System.Windows.Forms.Label();
            this.dateTimePicker受領日 = new System.Windows.Forms.DateTimePicker();
            this.label支払日 = new System.Windows.Forms.Label();
            this.label支払済み = new System.Windows.Forms.Label();
            this.label見積依頼日 = new System.Windows.Forms.Label();
            this.dateTimePicker見積依頼日 = new System.Windows.Forms.DateTimePicker();
            this.textBox手配内容 = new System.Windows.Forms.TextBox();
            this.label手配内容 = new System.Windows.Forms.Label();
            this.label手配依頼日 = new System.Windows.Forms.Label();
            this.dateTimePicker手配依頼日 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker納品日 = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.nullableDateTimePicker請求書日 = new NBaseUtil.NullableDateTimePicker();
            this.nullableDateTimePicker支払日 = new NBaseUtil.NullableDateTimePicker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button更新
            // 
            this.button更新.Location = new System.Drawing.Point(12, 12);
            this.button更新.Name = "button更新";
            this.button更新.Size = new System.Drawing.Size(100, 23);
            this.button更新.TabIndex = 1;
            this.button更新.Text = "更新";
            this.button更新.UseVisualStyleBackColor = true;
            this.button更新.Click += new System.EventHandler(this.button更新_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(135, 12);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(100, 23);
            this.button閉じる.TabIndex = 2;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // label手配依頼種別
            // 
            this.label手配依頼種別.AutoSize = true;
            this.label手配依頼種別.Location = new System.Drawing.Point(48, 50);
            this.label手配依頼種別.Name = "label手配依頼種別";
            this.label手配依頼種別.Size = new System.Drawing.Size(77, 12);
            this.label手配依頼種別.TabIndex = 0;
            this.label手配依頼種別.Text = "手配依頼種別";
            // 
            // textBox手配依頼種
            // 
            this.textBox手配依頼種.Location = new System.Drawing.Point(134, 47);
            this.textBox手配依頼種.Name = "textBox手配依頼種";
            this.textBox手配依頼種.ReadOnly = true;
            this.textBox手配依頼種.Size = new System.Drawing.Size(150, 19);
            this.textBox手配依頼種.TabIndex = 0;
            this.textBox手配依頼種.TabStop = false;
            this.textBox手配依頼種.Text = "種別";
            // 
            // label手配依頼詳細種別
            // 
            this.label手配依頼詳細種別.AutoSize = true;
            this.label手配依頼詳細種別.Location = new System.Drawing.Point(27, 75);
            this.label手配依頼詳細種別.Name = "label手配依頼詳細種別";
            this.label手配依頼詳細種別.Size = new System.Drawing.Size(101, 12);
            this.label手配依頼詳細種別.TabIndex = 0;
            this.label手配依頼詳細種別.Text = "手配依頼詳細種別";
            // 
            // textBox手配依頼詳細種別
            // 
            this.textBox手配依頼詳細種別.Location = new System.Drawing.Point(134, 72);
            this.textBox手配依頼詳細種別.Name = "textBox手配依頼詳細種別";
            this.textBox手配依頼詳細種別.ReadOnly = true;
            this.textBox手配依頼詳細種別.Size = new System.Drawing.Size(150, 19);
            this.textBox手配依頼詳細種別.TabIndex = 0;
            this.textBox手配依頼詳細種別.TabStop = false;
            this.textBox手配依頼詳細種別.Text = "詳細種別";
            // 
            // label船
            // 
            this.label船.AutoSize = true;
            this.label船.Location = new System.Drawing.Point(108, 25);
            this.label船.Name = "label船";
            this.label船.Size = new System.Drawing.Size(17, 12);
            this.label船.TabIndex = 0;
            this.label船.Text = "船";
            // 
            // textBox船
            // 
            this.textBox船.Location = new System.Drawing.Point(134, 22);
            this.textBox船.Name = "textBox船";
            this.textBox船.ReadOnly = true;
            this.textBox船.Size = new System.Drawing.Size(150, 19);
            this.textBox船.TabIndex = 0;
            this.textBox船.TabStop = false;
            // 
            // label発注日
            // 
            this.label発注日.AutoSize = true;
            this.label発注日.Location = new System.Drawing.Point(84, 220);
            this.label発注日.Name = "label発注日";
            this.label発注日.Size = new System.Drawing.Size(41, 12);
            this.label発注日.TabIndex = 0;
            this.label発注日.Text = "発注日";
            // 
            // dateTimePicker発注日
            // 
            this.dateTimePicker発注日.Location = new System.Drawing.Point(134, 215);
            this.dateTimePicker発注日.Name = "dateTimePicker発注日";
            this.dateTimePicker発注日.Size = new System.Drawing.Size(123, 19);
            this.dateTimePicker発注日.TabIndex = 4;
            // 
            // label回答日
            // 
            this.label回答日.AutoSize = true;
            this.label回答日.Location = new System.Drawing.Point(84, 194);
            this.label回答日.Name = "label回答日";
            this.label回答日.Size = new System.Drawing.Size(41, 12);
            this.label回答日.TabIndex = 0;
            this.label回答日.Text = "回答日";
            // 
            // dateTimePicker回答日
            // 
            this.dateTimePicker回答日.Location = new System.Drawing.Point(134, 189);
            this.dateTimePicker回答日.Name = "dateTimePicker回答日";
            this.dateTimePicker回答日.Size = new System.Drawing.Size(123, 19);
            this.dateTimePicker回答日.TabIndex = 3;
            // 
            // label受領日
            // 
            this.label受領日.AutoSize = true;
            this.label受領日.Location = new System.Drawing.Point(84, 246);
            this.label受領日.Name = "label受領日";
            this.label受領日.Size = new System.Drawing.Size(41, 12);
            this.label受領日.TabIndex = 0;
            this.label受領日.Text = "受領日";
            // 
            // dateTimePicker受領日
            // 
            this.dateTimePicker受領日.Location = new System.Drawing.Point(134, 241);
            this.dateTimePicker受領日.Name = "dateTimePicker受領日";
            this.dateTimePicker受領日.Size = new System.Drawing.Size(123, 19);
            this.dateTimePicker受領日.TabIndex = 6;
            // 
            // label支払日
            // 
            this.label支払日.AutoSize = true;
            this.label支払日.Location = new System.Drawing.Point(307, 271);
            this.label支払日.Name = "label支払日";
            this.label支払日.Size = new System.Drawing.Size(41, 12);
            this.label支払日.TabIndex = 0;
            this.label支払日.Text = "支払日";
            // 
            // label支払済み
            // 
            this.label支払済み.AutoSize = true;
            this.label支払済み.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label支払済み.ForeColor = System.Drawing.Color.Red;
            this.label支払済み.Location = new System.Drawing.Point(132, 299);
            this.label支払済み.Name = "label支払済み";
            this.label支払済み.Size = new System.Drawing.Size(204, 15);
            this.label支払済み.TabIndex = 0;
            this.label支払済み.Text = "支払済みのため編集できません";
            // 
            // label見積依頼日
            // 
            this.label見積依頼日.AutoSize = true;
            this.label見積依頼日.Location = new System.Drawing.Point(60, 168);
            this.label見積依頼日.Name = "label見積依頼日";
            this.label見積依頼日.Size = new System.Drawing.Size(65, 12);
            this.label見積依頼日.TabIndex = 0;
            this.label見積依頼日.Text = "見積依頼日";
            // 
            // dateTimePicker見積依頼日
            // 
            this.dateTimePicker見積依頼日.Location = new System.Drawing.Point(134, 163);
            this.dateTimePicker見積依頼日.Name = "dateTimePicker見積依頼日";
            this.dateTimePicker見積依頼日.Size = new System.Drawing.Size(123, 19);
            this.dateTimePicker見積依頼日.TabIndex = 2;
            // 
            // textBox手配内容
            // 
            this.textBox手配内容.Location = new System.Drawing.Point(134, 97);
            this.textBox手配内容.Name = "textBox手配内容";
            this.textBox手配内容.ReadOnly = true;
            this.textBox手配内容.Size = new System.Drawing.Size(500, 19);
            this.textBox手配内容.TabIndex = 0;
            this.textBox手配内容.TabStop = false;
            // 
            // label手配内容
            // 
            this.label手配内容.AutoSize = true;
            this.label手配内容.Location = new System.Drawing.Point(72, 100);
            this.label手配内容.Name = "label手配内容";
            this.label手配内容.Size = new System.Drawing.Size(53, 12);
            this.label手配内容.TabIndex = 0;
            this.label手配内容.Text = "手配内容";
            // 
            // label手配依頼日
            // 
            this.label手配依頼日.AutoSize = true;
            this.label手配依頼日.Location = new System.Drawing.Point(60, 142);
            this.label手配依頼日.Name = "label手配依頼日";
            this.label手配依頼日.Size = new System.Drawing.Size(65, 12);
            this.label手配依頼日.TabIndex = 0;
            this.label手配依頼日.Text = "手配依頼日";
            // 
            // dateTimePicker手配依頼日
            // 
            this.dateTimePicker手配依頼日.Location = new System.Drawing.Point(134, 137);
            this.dateTimePicker手配依頼日.Name = "dateTimePicker手配依頼日";
            this.dateTimePicker手配依頼日.Size = new System.Drawing.Size(123, 19);
            this.dateTimePicker手配依頼日.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "請求書日";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(305, 220);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "納品日";
            // 
            // dateTimePicker納品日
            // 
            this.dateTimePicker納品日.Location = new System.Drawing.Point(355, 215);
            this.dateTimePicker納品日.Name = "dateTimePicker納品日";
            this.dateTimePicker納品日.Size = new System.Drawing.Size(123, 19);
            this.dateTimePicker納品日.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nullableDateTimePicker請求書日);
            this.panel1.Controls.Add(this.nullableDateTimePicker支払日);
            this.panel1.Controls.Add(this.dateTimePicker納品日);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateTimePicker手配依頼日);
            this.panel1.Controls.Add(this.label手配依頼日);
            this.panel1.Controls.Add(this.label手配内容);
            this.panel1.Controls.Add(this.textBox手配内容);
            this.panel1.Controls.Add(this.dateTimePicker見積依頼日);
            this.panel1.Controls.Add(this.label見積依頼日);
            this.panel1.Controls.Add(this.label支払済み);
            this.panel1.Controls.Add(this.label支払日);
            this.panel1.Controls.Add(this.dateTimePicker受領日);
            this.panel1.Controls.Add(this.label受領日);
            this.panel1.Controls.Add(this.dateTimePicker回答日);
            this.panel1.Controls.Add(this.label回答日);
            this.panel1.Controls.Add(this.dateTimePicker発注日);
            this.panel1.Controls.Add(this.label発注日);
            this.panel1.Controls.Add(this.textBox船);
            this.panel1.Controls.Add(this.label船);
            this.panel1.Controls.Add(this.textBox手配依頼詳細種別);
            this.panel1.Controls.Add(this.label手配依頼詳細種別);
            this.panel1.Controls.Add(this.textBox手配依頼種);
            this.panel1.Controls.Add(this.label手配依頼種別);
            this.panel1.Location = new System.Drawing.Point(12, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(670, 334);
            this.panel1.TabIndex = 0;
            // 
            // nullableDateTimePicker請求書日
            // 
            this.nullableDateTimePicker請求書日.Location = new System.Drawing.Point(135, 266);
            this.nullableDateTimePicker請求書日.Name = "nullableDateTimePicker請求書日";
            this.nullableDateTimePicker請求書日.Size = new System.Drawing.Size(125, 19);
            this.nullableDateTimePicker請求書日.TabIndex = 7;
            this.nullableDateTimePicker請求書日.Value = new System.DateTime(2021, 8, 28, 16, 0, 49, 818);
            // 
            // nullableDateTimePicker支払日
            // 
            this.nullableDateTimePicker支払日.Location = new System.Drawing.Point(355, 266);
            this.nullableDateTimePicker支払日.Name = "nullableDateTimePicker支払日";
            this.nullableDateTimePicker支払日.Size = new System.Drawing.Size(125, 19);
            this.nullableDateTimePicker支払日.TabIndex = 8;
            this.nullableDateTimePicker支払日.Value = new System.DateTime(2021, 8, 28, 16, 0, 49, 818);
            // 
            // 日付調整Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(690, 384);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button更新);
            this.Controls.Add(this.panel1);
            this.Name = "日付調整Form";
            this.Text = "日付調整";
            this.Load += new System.EventHandler(this.日付調整Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button button更新;
        private System.Windows.Forms.Label label手配依頼種別;
        private System.Windows.Forms.TextBox textBox手配依頼種;
        private System.Windows.Forms.Label label手配依頼詳細種別;
        private System.Windows.Forms.TextBox textBox手配依頼詳細種別;
        private System.Windows.Forms.Label label船;
        private System.Windows.Forms.TextBox textBox船;
        private System.Windows.Forms.Label label発注日;
        private System.Windows.Forms.DateTimePicker dateTimePicker発注日;
        private System.Windows.Forms.Label label回答日;
        private System.Windows.Forms.DateTimePicker dateTimePicker回答日;
        private System.Windows.Forms.Label label受領日;
        private System.Windows.Forms.DateTimePicker dateTimePicker受領日;
        private System.Windows.Forms.Label label支払日;
        private System.Windows.Forms.Label label支払済み;
        private System.Windows.Forms.Label label見積依頼日;
        private System.Windows.Forms.DateTimePicker dateTimePicker見積依頼日;
        private System.Windows.Forms.TextBox textBox手配内容;
        private System.Windows.Forms.Label label手配内容;
        private System.Windows.Forms.Label label手配依頼日;
        private System.Windows.Forms.DateTimePicker dateTimePicker手配依頼日;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker納品日;
        private System.Windows.Forms.Panel panel1;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker請求書日;
        private NBaseUtil.NullableDateTimePicker nullableDateTimePicker支払日;
    }
}