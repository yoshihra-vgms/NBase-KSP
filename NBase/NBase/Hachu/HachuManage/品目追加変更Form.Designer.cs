namespace Hachu.HachuManage
{
    partial class 品目追加変更Form
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
            LidorSystems.IntegralUI.Lists.Style.ListColumnFormatStyle listColumnFormatStyle1 = new LidorSystems.IntegralUI.Lists.Style.ListColumnFormatStyle();
            LidorSystems.IntegralUI.Lists.Style.TreeListViewFormatStyle treeListViewFormatStyle1 = new LidorSystems.IntegralUI.Lists.Style.TreeListViewFormatStyle();
            LidorSystems.IntegralUI.Lists.Style.ListItemFormatStyle listItemFormatStyle1 = new LidorSystems.IntegralUI.Lists.Style.ListItemFormatStyle();
            LidorSystems.IntegralUI.Lists.Style.ListItemFormatStyle listItemFormatStyle2 = new LidorSystems.IntegralUI.Lists.Style.ListItemFormatStyle();
            LidorSystems.IntegralUI.Style.WatermarkImage watermarkImage1 = new LidorSystems.IntegralUI.Style.WatermarkImage();
            this.label品目 = new System.Windows.Forms.Label();
            this.comboBox区分 = new System.Windows.Forms.ComboBox();
            this.label区分 = new System.Windows.Forms.Label();
            this.button詳細品目編集 = new System.Windows.Forms.Button();
            this.treeListView = new LidorSystems.IntegralUI.Lists.TreeListView();
            this.button登録 = new System.Windows.Forms.Button();
            this.button閉じる = new System.Windows.Forms.Button();
            this.comboBox品目 = new System.Windows.Forms.ComboBox();
            this.textBoxヘッダ = new System.Windows.Forms.TextBox();
            this.labelヘッダ = new System.Windows.Forms.Label();
            this.button削除 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).BeginInit();
            this.SuspendLayout();
            // 
            // label品目
            // 
            this.label品目.AutoSize = true;
            this.label品目.Location = new System.Drawing.Point(19, 70);
            this.label品目.Name = "label品目";
            this.label品目.Size = new System.Drawing.Size(29, 12);
            this.label品目.TabIndex = 1;
            this.label品目.Text = "品目";
            // 
            // comboBox区分
            // 
            this.comboBox区分.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox区分.FormattingEnabled = true;
            this.comboBox区分.Location = new System.Drawing.Point(60, 41);
            this.comboBox区分.Name = "comboBox区分";
            this.comboBox区分.Size = new System.Drawing.Size(121, 20);
            this.comboBox区分.TabIndex = 2;
            // 
            // label区分
            // 
            this.label区分.AutoSize = true;
            this.label区分.Location = new System.Drawing.Point(19, 44);
            this.label区分.Name = "label区分";
            this.label区分.Size = new System.Drawing.Size(29, 12);
            this.label区分.TabIndex = 3;
            this.label区分.Text = "区分";
            // 
            // button詳細品目編集
            // 
            this.button詳細品目編集.BackColor = System.Drawing.SystemColors.Control;
            this.button詳細品目編集.Location = new System.Drawing.Point(15, 108);
            this.button詳細品目編集.Name = "button詳細品目編集";
            this.button詳細品目編集.Size = new System.Drawing.Size(89, 23);
            this.button詳細品目編集.TabIndex = 4;
            this.button詳細品目編集.Text = "詳細品目編集";
            this.button詳細品目編集.UseVisualStyleBackColor = false;
            this.button詳細品目編集.Click += new System.EventHandler(this.button詳細品目編集_Click);
            // 
            // treeListView
            // 
            this.treeListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            listColumnFormatStyle1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView.ColumnFormatStyle = listColumnFormatStyle1;
            // 
            // 
            // 
            this.treeListView.ContentPanel.BackColor = System.Drawing.Color.Transparent;
            this.treeListView.ContentPanel.Location = new System.Drawing.Point(3, 3);
            this.treeListView.ContentPanel.Name = "";
            this.treeListView.ContentPanel.Size = new System.Drawing.Size(583, 135);
            this.treeListView.ContentPanel.TabIndex = 3;
            this.treeListView.ContentPanel.TabStop = false;
            this.treeListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView.Footer = false;
            treeListViewFormatStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeListViewFormatStyle1.LineStyle = LidorSystems.IntegralUI.Style.LineStyle.Dot;
            this.treeListView.FormatStyle = treeListViewFormatStyle1;
            this.treeListView.Location = new System.Drawing.Point(15, 139);
            this.treeListView.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.treeListView.Name = "treeListView";
            listItemFormatStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView.NodeFormatStyle = listItemFormatStyle1;
            this.treeListView.Size = new System.Drawing.Size(589, 141);
            listItemFormatStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView.SubItemFormatStyle = listItemFormatStyle2;
            this.treeListView.TabIndex = 5;
            this.treeListView.WatermarkImage = watermarkImage1;
            this.treeListView.DoubleClick += new System.EventHandler(this.treeListView_DoubleClick);
            // 
            // button登録
            // 
            this.button登録.BackColor = System.Drawing.SystemColors.Control;
            this.button登録.Location = new System.Drawing.Point(191, 288);
            this.button登録.Name = "button登録";
            this.button登録.Size = new System.Drawing.Size(75, 23);
            this.button登録.TabIndex = 6;
            this.button登録.Text = "登録";
            this.button登録.UseVisualStyleBackColor = false;
            this.button登録.Click += new System.EventHandler(this.button登録_Click);
            // 
            // button閉じる
            // 
            this.button閉じる.BackColor = System.Drawing.SystemColors.Control;
            this.button閉じる.Location = new System.Drawing.Point(371, 288);
            this.button閉じる.Name = "button閉じる";
            this.button閉じる.Size = new System.Drawing.Size(75, 23);
            this.button閉じる.TabIndex = 7;
            this.button閉じる.Text = "閉じる";
            this.button閉じる.UseVisualStyleBackColor = false;
            this.button閉じる.Click += new System.EventHandler(this.button閉じる_Click);
            // 
            // comboBox品目
            // 
            this.comboBox品目.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox品目.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBox品目.FormattingEnabled = true;
            this.comboBox品目.Location = new System.Drawing.Point(60, 67);
            this.comboBox品目.MaxLength = 50;
            this.comboBox品目.Name = "comboBox品目";
            this.comboBox品目.Size = new System.Drawing.Size(229, 20);
            this.comboBox品目.TabIndex = 3;
            // 
            // textBoxヘッダ
            // 
            this.textBoxヘッダ.Location = new System.Drawing.Point(60, 16);
            this.textBoxヘッダ.Name = "textBoxヘッダ";
            this.textBoxヘッダ.Size = new System.Drawing.Size(229, 19);
            this.textBoxヘッダ.TabIndex = 1;
            // 
            // labelヘッダ
            // 
            this.labelヘッダ.AutoSize = true;
            this.labelヘッダ.Location = new System.Drawing.Point(19, 19);
            this.labelヘッダ.Name = "labelヘッダ";
            this.labelヘッダ.Size = new System.Drawing.Size(31, 12);
            this.labelヘッダ.TabIndex = 8;
            this.labelヘッダ.Text = "ヘッダ";
            // 
            // button削除
            // 
            this.button削除.BackColor = System.Drawing.SystemColors.Control;
            this.button削除.Location = new System.Drawing.Point(281, 288);
            this.button削除.Name = "button削除";
            this.button削除.Size = new System.Drawing.Size(75, 23);
            this.button削除.TabIndex = 9;
            this.button削除.Text = "削除";
            this.button削除.UseVisualStyleBackColor = false;
            this.button削除.Click += new System.EventHandler(this.button削除_Click);
            // 
            // 品目追加変更Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(619, 321);
            this.Controls.Add(this.button削除);
            this.Controls.Add(this.labelヘッダ);
            this.Controls.Add(this.textBoxヘッダ);
            this.Controls.Add(this.comboBox品目);
            this.Controls.Add(this.button閉じる);
            this.Controls.Add(this.button登録);
            this.Controls.Add(this.treeListView);
            this.Controls.Add(this.button詳細品目編集);
            this.Controls.Add(this.label区分);
            this.Controls.Add(this.comboBox区分);
            this.Controls.Add(this.label品目);
            this.Name = "品目追加変更Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "手配品目";
            this.Load += new System.EventHandler(this.品目追加変更Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label品目;
        private System.Windows.Forms.ComboBox comboBox区分;
        private System.Windows.Forms.Label label区分;
        private System.Windows.Forms.Button button詳細品目編集;
        private LidorSystems.IntegralUI.Lists.TreeListView treeListView;
        private System.Windows.Forms.Button button登録;
        private System.Windows.Forms.Button button閉じる;
        private System.Windows.Forms.ComboBox comboBox品目;
        private System.Windows.Forms.TextBox textBoxヘッダ;
        private System.Windows.Forms.Label labelヘッダ;
        private System.Windows.Forms.Button button削除;
    }
}