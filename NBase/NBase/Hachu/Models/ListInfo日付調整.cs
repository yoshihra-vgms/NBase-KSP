using System;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using NBaseData.DAC;
using Hachu.HachuManage;

namespace Hachu.Models
{
    public class ListInfo日付調整 : ListInfoBase
    {
        public int no;


        /// <summary>
        /// 手配依頼
        /// </summary>
        public OdThi thi;

        /// <summary>
        /// 見積依頼
        /// </summary>
        public OdMm mm;

        /// <summary>
        /// 見積回答
        /// </summary>
        public OdMk mk;

        /// <summary>
        /// 受領
        /// </summary>
        public OdJry jry;

        /// <summary>
        /// 支払
        /// </summary>
        public OdShr shr;


        /// <summary>
        /// 「日付調整Form」を作成する
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <returns></returns>
        #region public override BaseForm CreateForm(BaseUserControl baseControl)
        public override BaseForm CreateForm(BaseUserControl baseControl)
        {
            BaseForm form = new 日付調整Form((int)BaseForm.WINDOW_STYLE.通常, this);
            return form;
        }
        #endregion

        #region public override BaseUserControl CreatePanel(int windowStyle)
        public override BaseUserControl CreatePanel(int windowStyle)
        {
            return null;
        }
        #endregion
    }
}
