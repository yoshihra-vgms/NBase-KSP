namespace Hachu.HachuManage
{
    partial class 燃料潤滑油編集Form
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
            LidorSystems.IntegralUI.Style.WatermarkImage watermarkImage1 = new LidorSystems.IntegralUI.Style.WatermarkImage();
            this.button登録 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.treeListView = new LidorSystems.IntegralUI.Lists.TreeListView();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            this.SuspendLayout();
            // 
            // button登録
            // 
            this.button登録.BackColor = System.Drawing.SystemColors.Control;
            this.button登録.Location = new System.Drawing.Point(469, 12);
            this.button登録.Name = "button登録";
            this.button登録.Size = new System.Drawing.Size(75, 23);
            this.button登録.TabIndex = 17;
            this.button登録.Text = "登録";
            this.button登録.UseVisualStyleBackColor = false;
            this.button登録.Click += new System.EventHandler(this.button登録_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(550, 12);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 18;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // treeListView
            // 
            // 
            // 
            // 
            this.treeListView.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView.ContentPanel.Name = "";
            this.treeListView.ContentPanel.Size = new System.Drawing.Size(603, 427);
            this.treeListView.ContentPanel.TabIndex = 3;
            this.treeListView.ContentPanel.TabStop = false;
            this.treeListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView.Footer = false;
            this.treeListView.Location = new System.Drawing.Point(12, 41);
            this.treeListView.Name = "treeListView";
            this.treeListView.Size = new System.Drawing.Size(609, 433);
            this.treeListView.TabIndex = 19;
            this.treeListView.Text = "treeListView1";
            this.treeListView.WatermarkImage = watermarkImage1;
            // 
            // 燃料潤滑油編集Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(633, 486);
            this.Controls.Add(this.treeListView);
            this.Controls.Add(this.button登録);
            this.Controls.Add(this.button閉じる);
            this.Name = "燃料潤滑油編集Form";
            this.Text = "燃料潤滑油編集Form";
            this.Load += new System.EventHandler(this.燃料潤滑油編集Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button登録;
        private System.Windows.Forms.Button button閉じる;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView;
    }
}