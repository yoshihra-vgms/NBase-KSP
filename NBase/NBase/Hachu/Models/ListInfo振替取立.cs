using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using NBaseData.DAC;
using Hachu.HachuManage;

namespace Hachu.Models
{
    public class ListInfo振替取立 : ListInfoBase
    {
        /// <summary>
        /// 振替取立
        /// </summary>
        public OdFurikaeToritate info;

        /// <summary>
        /// 「振替取立Form」を作成する
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <returns></returns>
        #region public override 詳細BaseForm CreateForm(int windowStyle)
        public override BaseForm CreateForm(int windowStyle)
        {
            BaseForm form = new 振替取立Form(windowStyle, this);
            return form;
        }
        #endregion
    }
}
