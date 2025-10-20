namespace Senin
{
    partial class 船員詳細検索Form
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
            this.button_Open = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Delete = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.settingListControl1 = new NBaseCommon.SettingListControl();
            this.button項目設定 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox_OrCondition = new System.Windows.Forms.GroupBox();
            this.advancedConditionTitle2 = new Senin.util.AdvancedConditionTitle();
            this.panel_OrCondition = new System.Windows.Forms.Panel();
            this.button_OrAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_AndCondition = new System.Windows.Forms.GroupBox();
            this.advancedConditionTitle1 = new Senin.util.AdvancedConditionTitle();
            this.panel_AddCondition = new System.Windows.Forms.Panel();
            this.button_AndAdd = new System.Windows.Forms.Button();
            this.button_Search = new System.Windows.Forms.Button();
            this.button_Clear = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.singleLineCombo_ConditionName = new NBaseUtil.SingleLineCombo2();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox_OrCondition.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox_AndCondition.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Open
            // 
            this.button_Open.Location = new System.Drawing.Point(273, 21);
            this.button_Open.Name = "button_Open";
            this.button_Open.Size = new System.Drawing.Size(99, 23);
            this.button_Open.TabIndex = 4;
            this.button_Open.Text = "検索条件を開く";
            this.button_Open.UseVisualStyleBackColor = true;
            this.button_Open.Click += new System.EventHandler(this.button_Open_Click);
            // 
            // button_Save
            // 
            this.button_Save.Location = new System.Drawing.Point(378, 21);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(99, 23);
            this.button_Save.TabIndex = 5;
            this.button_Save.Text = "検索条件保存";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Delete
            // 
            this.button_Delete.Location = new System.Drawing.Point(378, 50);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(99, 23);
            this.button_Delete.TabIndex = 6;
            this.button_Delete.Text = "検索条件削除";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 89);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 163F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1224, 562);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.settingListControl1);
            this.panel3.Controls.Add(this.button項目設定);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 421);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1218, 138);
            this.panel3.TabIndex = 2;
            // 
            // settingListControl1
            // 
            this.settingListControl1.Location = new System.Drawing.Point(10, 46);
            this.settingListControl1.Name = "settingListControl1";
            this.settingListControl1.Size = new System.Drawing.Size(1195, 60);
            this.settingListControl1.TabIndex = 14;
            // 
            // button項目設定
            // 
            this.button項目設定.BackColor = System.Drawing.SystemColors.Control;
            this.button項目設定.Location = new System.Drawing.Point(77, 13);
            this.button項目設定.Name = "button項目設定";
            this.button項目設定.Size = new System.Drawing.Size(92, 23);
            this.button項目設定.TabIndex = 13;
            this.button項目設定.Text = "リスト項目設定";
            this.button項目設定.UseVisualStyleBackColor = false;
            this.button項目設定.Click += new System.EventHandler(this.button項目設定_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "リスト項目";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.groupBox_OrCondition);
            this.panel2.Controls.Add(this.button_OrAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 258);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1218, 157);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "OR  条件";
            // 
            // groupBox_OrCondition
            // 
            this.groupBox_OrCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_OrCondition.Controls.Add(this.advancedConditionTitle2);
            this.groupBox_OrCondition.Controls.Add(this.panel_OrCondition);
            this.groupBox_OrCondition.Location = new System.Drawing.Point(4, 31);
            this.groupBox_OrCondition.Name = "groupBox_OrCondition";
            this.groupBox_OrCondition.Size = new System.Drawing.Size(1207, 116);
            this.groupBox_OrCondition.TabIndex = 3;
            this.groupBox_OrCondition.TabStop = false;
            // 
            // advancedConditionTitle2
            // 
            this.advancedConditionTitle2.Location = new System.Drawing.Point(6, 15);
            this.advancedConditionTitle2.Margin = new System.Windows.Forms.Padding(0);
            this.advancedConditionTitle2.Name = "advancedConditionTitle2";
            this.advancedConditionTitle2.Size = new System.Drawing.Size(1098, 12);
            this.advancedConditionTitle2.TabIndex = 0;
            // 
            // panel_OrCondition
            // 
            this.panel_OrCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_OrCondition.AutoScroll = true;
            this.panel_OrCondition.Location = new System.Drawing.Point(6, 30);
            this.panel_OrCondition.Name = "panel_OrCondition";
            this.panel_OrCondition.Size = new System.Drawing.Size(1195, 80);
            this.panel_OrCondition.TabIndex = 3;
            // 
            // button_OrAdd
            // 
            this.button_OrAdd.Location = new System.Drawing.Point(174, 8);
            this.button_OrAdd.Name = "button_OrAdd";
            this.button_OrAdd.Size = new System.Drawing.Size(75, 23);
            this.button_OrAdd.TabIndex = 4;
            this.button_OrAdd.Text = "条件追加";
            this.button_OrAdd.UseVisualStyleBackColor = true;
            this.button_OrAdd.Click += new System.EventHandler(this.button_OrAdd_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBox_AndCondition);
            this.panel1.Controls.Add(this.button_AndAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1218, 249);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "AND 条件";
            // 
            // groupBox_AndCondition
            // 
            this.groupBox_AndCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_AndCondition.Controls.Add(this.advancedConditionTitle1);
            this.groupBox_AndCondition.Controls.Add(this.panel_AddCondition);
            this.groupBox_AndCondition.Location = new System.Drawing.Point(4, 30);
            this.groupBox_AndCondition.Name = "groupBox_AndCondition";
            this.groupBox_AndCondition.Size = new System.Drawing.Size(1207, 216);
            this.groupBox_AndCondition.TabIndex = 0;
            this.groupBox_AndCondition.TabStop = false;
            // 
            // advancedConditionTitle1
            // 
            this.advancedConditionTitle1.Location = new System.Drawing.Point(6, 15);
            this.advancedConditionTitle1.Margin = new System.Windows.Forms.Padding(0);
            this.advancedConditionTitle1.Name = "advancedConditionTitle1";
            this.advancedConditionTitle1.Size = new System.Drawing.Size(1098, 12);
            this.advancedConditionTitle1.TabIndex = 0;
            // 
            // panel_AddCondition
            // 
            this.panel_AddCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_AddCondition.AutoScroll = true;
            this.panel_AddCondition.Location = new System.Drawing.Point(6, 30);
            this.panel_AddCondition.Name = "panel_AddCondition";
            this.panel_AddCondition.Size = new System.Drawing.Size(1195, 180);
            this.panel_AddCondition.TabIndex = 2;
            // 
            // button_AndAdd
            // 
            this.button_AndAdd.Location = new System.Drawing.Point(174, 7);
            this.button_AndAdd.Name = "button_AndAdd";
            this.button_AndAdd.Size = new System.Drawing.Size(75, 23);
            this.button_AndAdd.TabIndex = 1;
            this.button_AndAdd.Text = "条件追加";
            this.button_AndAdd.UseVisualStyleBackColor = true;
            this.button_AndAdd.Click += new System.EventHandler(this.button_AndAdd_Click);
            // 
            // button_Search
            // 
            this.button_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Search.Location = new System.Drawing.Point(506, 679);
            this.button_Search.Name = "button_Search";
            this.button_Search.Size = new System.Drawing.Size(99, 23);
            this.button_Search.TabIndex = 1;
            this.button_Search.Text = "検索";
            this.button_Search.UseVisualStyleBackColor = true;
            this.button_Search.Click += new System.EventHandler(this.button_Search_Click);
            // 
            // button_Clear
            // 
            this.button_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Clear.Location = new System.Drawing.Point(1112, 50);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(99, 23);
            this.button_Clear.TabIndex = 2;
            this.button_Clear.Text = "検索条件クリア";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.Location = new System.Drawing.Point(624, 679);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(99, 23);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "閉じる";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // singleLineCombo_ConditionName
            // 
            this.singleLineCombo_ConditionName.AutoSize = true;
            this.singleLineCombo_ConditionName.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.singleLineCombo_ConditionName.Location = new System.Drawing.Point(29, 23);
            this.singleLineCombo_ConditionName.MaxLength = 32767;
            this.singleLineCombo_ConditionName.Name = "singleLineCombo_ConditionName";
            this.singleLineCombo_ConditionName.ReadOnly = false;
            this.singleLineCombo_ConditionName.Size = new System.Drawing.Size(226, 19);
            this.singleLineCombo_ConditionName.TabIndex = 7;
            this.singleLineCombo_ConditionName.WindowHeight = 100;
            // 
            // 船員詳細検索Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1228, 714);
            this.Controls.Add(this.singleLineCombo_ConditionName);
            this.Controls.Add(this.button_Open);
            this.Controls.Add(this.button_Delete);
            this.Controls.Add(this.button_Search);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Clear);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(1244, 672);
            this.Name = "船員詳細検索Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "詳細検索";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.船員詳細検索Form_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox_OrCondition.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox_AndCondition.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Open;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox_OrCondition;
        private System.Windows.Forms.Button button_OrAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_AndCondition;
        private System.Windows.Forms.Button button_AndAdd;
        private System.Windows.Forms.Button button_Search;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Panel panel_AddCondition;
        private System.Windows.Forms.Panel panel_OrCondition;
        private NBaseUtil.SingleLineCombo2 singleLineCombo_ConditionName;
        private util.AdvancedConditionTitle advancedConditionTitle2;
        private util.AdvancedConditionTitle advancedConditionTitle1;
        private System.Windows.Forms.Button button項目設定;
        private NBaseCommon.SettingListControl settingListControl1;
    }
}