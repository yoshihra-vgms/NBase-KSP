using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeficiencyControl.Controls
{
    /// <summary>
    /// ラベル表示説明コントロール
    /// </summary>
    public partial class LabelDescriptionControl : UserControl
    {
        public LabelDescriptionControl()
        {
            InitializeComponent();

            this.TabStop = false;
        }

        /// <summary>
        /// 必須項目可否
        /// </summary>
        [Description("必須項目可否を設定します")]
        public bool RequiredFlag
        {
            get
            {
                return this.labelAster.Visible;
            }
            set
            {
                this.labelAster.Visible = value;
            }
        }

        /// <summary>
        /// 項目値を設定します。
        /// </summary>
        [Description("項目値を設定します")]        
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]        
        public virtual string MainText
        {
            get
            {
                return this.labelMainText.Text;
            }
            set
            {
                this.labelMainText.Text = value;
            }
        }


        /// <summary>
        /// 説明値を設定します。
        /// </summary>
        [Description("説明を設定します")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]        
        public string DescriptionText
        {
            get
            {
                return this.labelDescription.Text;
            }
            set
            {
                this.labelDescription.Text = value;
            }
        }

        /// <summary>
        /// 説明表示可否
        /// </summary>
        [Description("説明の表示可否を設定します")]
        public bool DescriptionEnabled
        {
            get
            {
                return this.labelDescription.Visible;
            }
            set
            {
                this.labelDescription.Visible = value;
            }
        }

        [Description("説明のフォントを設定します")]
        public Font DescriptionFont
        {
            get
            {
                return this.labelDescription.Font;
            }
            set
            {
                this.labelDescription.Font = value;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelRemarkControl_Load(object sender, EventArgs e)
        {

        }
    }
}
