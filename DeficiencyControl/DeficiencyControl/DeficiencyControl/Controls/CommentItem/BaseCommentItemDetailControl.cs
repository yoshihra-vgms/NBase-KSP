using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DeficiencyControl.Logic.CommentItem;

namespace DeficiencyControl.Controls.CommentItem
{

    

    /// <summary>
    /// コメントアイテム詳細コントロール基底
    /// </summary>
    public partial class BaseCommentItemDetailControl : BaseControl
    {
        public BaseCommentItemDetailControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// これの番号
        /// </summary>
        protected int DeficinecyNo = 0;


        /// <summary>
        /// これの番号を設定する
        /// </summary>
        /// <param name="no"></param>
        public virtual void SetDeficiencyNo(int no)
        {
            this.DeficinecyNo = no;
        }


        /// <summary>
        /// 入力の取得
        /// </summary>
        /// <returns></returns>
        public virtual BaseCommentItemControlDetailData GetInputData()
        {
            throw new NotImplementedException();
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseCommentItemDetailControl_Load(object sender, EventArgs e)
        {

        }
    }
}
