namespace NBaseHonsen
{
    partial class LoginForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
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

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button閉じる = new System.Windows.Forms.Button();
            this.buttonログイン = new System.Windows.Forms.Button();
            this.comboBox船 = new System.Windows.Forms.ComboBox();
            this.textBoxパスワード = new System.Windows.Forms.TextBox();
            this.textBoxユーザID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelServer = new System.Windows.Forms.Label();
            this.labelOffline = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelCustomer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button閉じる.Location = new System.Drawing.Point(347, 525);
            this.button閉じる.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(149, 38);
            this.button閉じる.TabIndex = 4;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.cancelButt_Click);
            // 
            // buttonログイン
            // 
            this.buttonログイン.BackColor = System.Drawing.SystemColors.Control;
            this.buttonログイン.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonログイン.Location = new System.Drawing.Point(169, 525);
            this.buttonログイン.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.buttonログイン.Name = "buttonログイン";
            this.buttonログイン.Size = new System.Drawing.Size(149, 38);
            this.buttonログイン.TabIndex = 3;
            this.buttonログイン.Text = "ログイン";
            this.buttonログイン.UseVisualStyleBackColor = false;
            this.buttonログイン.Click += new System.EventHandler(this.loginButt_Click);
            // 
            // comboBox船
            // 
            this.comboBox船.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox船.FormattingEnabled = true;
            this.comboBox船.Location = new System.Drawing.Point(186, 480);
            this.comboBox船.Name = "comboBox船";
            this.comboBox船.Size = new System.Drawing.Size(384, 27);
            this.comboBox船.TabIndex = 2;
            // 
            // textBoxパスワード
            // 
            this.textBoxパスワード.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxパスワード.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBoxパスワード.Location = new System.Drawing.Point(186, 442);
            this.textBoxパスワード.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBoxパスワード.MaxLength = 30;
            this.textBoxパスワード.Name = "textBoxパスワード";
            this.textBoxパスワード.PasswordChar = '*';
            this.textBoxパスワード.Size = new System.Drawing.Size(384, 26);
            this.textBoxパスワード.TabIndex = 1;
            // 
            // textBoxユーザID
            // 
            this.textBoxユーザID.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxユーザID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.textBoxユーザID.Location = new System.Drawing.Point(186, 406);
            this.textBoxユーザID.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.textBoxユーザID.MaxLength = 30;
            this.textBoxユーザID.Name = "textBoxユーザID";
            this.textBoxユーザID.Size = new System.Drawing.Size(384, 26);
            this.textBoxユーザID.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(145, 483);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "船";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(85, 445);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "パスワード";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(95, 409);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 19);
            this.label1.TabIndex = 10;
            this.label1.Text = "ユーザID";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelVersion.ForeColor = System.Drawing.Color.Black;
            this.labelVersion.Location = new System.Drawing.Point(12, 551);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(44, 12);
            this.labelVersion.TabIndex = 11;
            this.labelVersion.Text = "Version";
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelServer.Location = new System.Drawing.Point(12, 590);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(38, 12);
            this.labelServer.TabIndex = 12;
            this.labelServer.Text = "Server";
            // 
            // labelOffline
            // 
            this.labelOffline.AutoSize = true;
            this.labelOffline.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelOffline.ForeColor = System.Drawing.Color.Red;
            this.labelOffline.Location = new System.Drawing.Point(304, 381);
            this.labelOffline.Name = "labelOffline";
            this.labelOffline.Size = new System.Drawing.Size(57, 15);
            this.labelOffline.TabIndex = 13;
            this.labelOffline.Text = "Offline";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NBaseHonsen.Properties.Resources.NBase_login;
            this.pictureBox1.Location = new System.Drawing.Point(32, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 350);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // labelCustomer
            // 
            this.labelCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCustomer.AutoSize = true;
            this.labelCustomer.BackColor = System.Drawing.Color.Transparent;
            this.labelCustomer.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelCustomer.ForeColor = System.Drawing.Color.Black;
            this.labelCustomer.Location = new System.Drawing.Point(470, 307);
            this.labelCustomer.Name = "labelCustomer";
            this.labelCustomer.Size = new System.Drawing.Size(73, 21);
            this.labelCustomer.TabIndex = 15;
            this.labelCustomer.Text = "顧客名";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.buttonログイン;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(664, 611);
            this.Controls.Add(this.labelCustomer);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelOffline);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.comboBox船);
            this.Controls.Add(this.textBoxパスワード);
            this.Controls.Add(this.textBoxユーザID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.buttonログイン);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.Button buttonログイン;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox船;
        private System.Windows.Forms.TextBox textBoxパスワード;
        private System.Windows.Forms.TextBox textBoxユーザID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label labelOffline;
        private System.Windows.Forms.Label labelCustomer;
    }
}

