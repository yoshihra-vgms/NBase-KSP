using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DcCommon.DB;
using DcCommon.DB.DAC;
using DcCommon.DB.DAC.Search;

using DeficiencyControl.Controls.CommentItem;

using DeficiencyControl.Logic;
using DeficiencyControl.Logic.CommentItem;
using DeficiencyControl.Util;

using DeficiencyControl.Grid;
using DeficiencyControl.Files;

using CIsl.DB.WingDAC;
using DcCommon;
namespace DeficiencyControl.Schedule
{
    public class BaseScheduleMainTab : BaseControl
    {
        /// <summary>
        /// 更新通知Delegate 
        /// </summary>
        /// <param name="cate">更新のあったカテゴリ</param>
        public delegate void UpdateDelegate(EScheduleCategory cate);


        /// <summary>
        /// 更新通知
        /// </summary>
        public UpdateDelegate UpdateDelegateProc = null;


        /// <summary>
        /// 現在の入力コントロール一式を取得する
        /// </summary>
        /// <returns></returns>
        protected List<T> GetInputControlList<T>(FlowLayoutPanel fpane) where T : Control
        {
            List<T> anslist = new List<T>();

            //現在のデータ取得
            foreach (Control c in fpane.Controls)
            {
                T cc = c as T;
                if (cc == null)
                {
                    continue;
                }

                anslist.Add(cc);
            }

            return anslist;
        }

    }
}
