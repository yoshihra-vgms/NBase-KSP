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
    /// コメントアイテムHeader基底
    /// </summary>
    public partial class BaseCommentItemHeaderControl : BaseControl
    {
        public BaseCommentItemHeaderControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 指摘事項件数が変更されたときのイベント ret=変更成功可否
        /// </summary>
        /// <param name="count"></param>
        public delegate int ChangeDeficiencyCountDelegate(int count);


        /// <summary>
        /// 指摘事項件数変更関数
        /// </summary>
        public ChangeDeficiencyCountDelegate ChangeDeficiencyCountDelegateProc = null;


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        



        /// <summary>
        /// 入力の取得
        /// </summary>
        /// <returns></returns>
        public virtual BaseCommentItemControlHeaderData GetInputData()
        {
            throw new NotImplementedException();
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseCommentItemHeaderControl_Load(object sender, EventArgs e)
        {

        }
    }
}
