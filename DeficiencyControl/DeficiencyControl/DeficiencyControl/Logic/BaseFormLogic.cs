using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeficiencyControl.Logic
{
    /// <summary>
    /// ロジック基底
    /// </summary>
    public class BaseFormLogic
    {
        /// <summary>
        /// エラーリスト
        /// </summary>
        public List<Control> ErList = new List<Control>();


        //メンバ関数
        /// <summary>
        /// エラーチェック関数 falseでエラー有
        /// </summary>
        /// <param name="chcol">コントロール色変更可否</param>
        /// <returns></returns>
        public virtual bool CheckError(bool chcol)
        {
            return false;
        }

        /// <summary>
        /// エラー表示
        /// </summary>
        /// <returns></returns>
        public virtual bool DispError()
        {
            return Logic.ComLogic.ChangeColor(this.ErList, DeficiencyControlColor.EColor);
        }

        /// <summary>
        /// エラー表示リセット
        /// </summary>
        /// <returns></returns>
        public virtual bool ResetError()
        {
            return Logic.ComLogic.ResetColor(this.ErList);
        }
    }
}
