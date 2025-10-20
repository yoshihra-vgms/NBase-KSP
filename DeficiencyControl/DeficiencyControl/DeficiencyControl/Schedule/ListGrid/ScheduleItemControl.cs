using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeficiencyControl.Schedule.ListGrid
{
    /// <summary>
    /// 最小単位のスケジュールコントロール
    /// </summary>
    public partial class ScheduleItemControl : UserControl
    {
        public ScheduleItemControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ヘッダー背景色
        /// </summary>
        [Description("タイトルの背景色を設定します")]
        public Color TitleColor
        {
            get
            {
                return this.labelTitle.BackColor;
            }
            set
            {
                this.labelTitle.BackColor = value;
            }
        }


        /// <summary>
        /// アイテム背景色
        /// </summary>
        [Description("アイテムの背景色を設定します")]
        public Color DetailColor
        {
            get
            {
                return this.labelDetail.BackColor;
            }
            set
            {
                this.labelDetail.BackColor = value;
            }
        }


        /// <summary>
        /// タイトルの設定
        /// </summary>
        [Description("タイトル文字列を設定します")]
        public string TitleText
        {
            get
            {
                return this.labelTitle.Text;
            }
            set
            {
                this.labelTitle.Text = value;
            }
        }


        /// <summary>
        /// 詳細の設定
        /// </summary>
        [Description("詳細文字列を設定します")]
        public string DetailText
        {
            get
            {
                return this.labelDetail.Text;
            }
            set
            {
                this.labelDetail.Text = value;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleItemControl_Load(object sender, EventArgs e)
        {

        }

       
    }
}
