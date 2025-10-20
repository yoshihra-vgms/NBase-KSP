using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon;
using DcCommon.DB;
using DcCommon.DB.DAC;

using DeficiencyControl.Util;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Logic;
using CIsl.DB.WingDAC;



namespace DeficiencyControl.Controls
{
    /// <summary>
    /// Status選択コントロール
    /// </summary>
    public partial class StatusSelectControl : UserControl
    {
        public StatusSelectControl()
        {
            InitializeComponent();

            //初期化
            this.InitControl();
        }

        /// <summary>
        /// RadioButtonの管理
        /// </summary>
        private List<RadioButton> RadioList = new List<RadioButton>();

        /// <summary>
        /// コントロールの初期化
        /// </summary>
        private void InitControl()
        {
            //扱いやすいようにcontrolの準備をする。必要ならマスタから自動生成するように改造せよ
            this.radioButtonPending.Tag = EStatus.Pending;
            this.radioButtonComplete.Tag = EStatus.Complete;

            //作成
            this.RadioList = new List<RadioButton>();
            this.RadioList.Add(this.radioButtonPending);
            this.RadioList.Add(this.radioButtonComplete);
        }


        /// <summary>
        /// データ表示
        /// </summary>
        /// <param name="st"></param>
        public void DispControl(EStatus st)
        {
            foreach (RadioButton ra in this.RadioList)
            {
                if ((EStatus)ra.Tag == st)
                {
                    ra.Checked = true;
                }
            }
        }



        /// <summary>
        /// 選択を取得
        /// </summary>
        /// <returns></returns>
        public EStatus GetSelectData()
        {
            foreach (RadioButton ra in this.RadioList)
            {
                if (ra.Checked == true)
                {
                    return (EStatus)ra.Tag;
                }
            }

            return EStatus.Pending;
        }


        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusSelectControl_Load(object sender, EventArgs e)
        {
            
        }
    }
}
